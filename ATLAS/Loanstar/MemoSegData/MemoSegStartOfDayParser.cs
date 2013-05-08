using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using StockLoan.Common;
using DatabaseFunctions;

namespace BroadRidge.MemoSegFiles
{
    class MemoSegStartOfDayParser
    {
        private string filePath = "";
        private string bizDate = "";
        private string dbCnStr = "";
        private string system = "";

        public MemoSegStartOfDayParser(string system, string filePath, string bizDate, string dbCnStr)
        {
            this.filePath = filePath;
            this.bizDate = bizDate;
            this.dbCnStr = dbCnStr;
            this.system = system;
        }

        public bool CheckFileHeaderDate()
        {
            bool successful = false;

            string line;
            string fileHeaderDate = "";

            if (File.GetLastWriteTime(filePath) <= DateTime.Parse(bizDate))
            {
                return false;
            }

            // read one line

            TextReader textReader = new StreamReader(filePath);

            line = textReader.ReadLine();

            if (!line.Equals(""))
            {
                fileHeaderDate = line.Substring(11, 8);
            }



            try
            {
                if (DateTime.ParseExact(fileHeaderDate, "yyyyMMdd", null).ToString("yyyy-MM-dd").Equals(bizDate))
                {
                    successful = true;
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                successful = false;
            }
            finally
            {
                textReader.Close();
            }

            return successful;
        }


        public void Load()
        {
            long counter = 0;
            string line = "-1";

            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }


            DatabaseFunctions.MemoSegDatabaseFunctions.MemoSegExDeficitPurge(system, dbCnStr);
            Log.Write("Purged exdeficit file items for " + bizDate + " for " + system + " . [MemoSegStartOfDayParser.Load]", 1);

            string[] fileContents = File.ReadAllLines(filePath);
            Log.Write("Will start loading exdeficit file items for " + bizDate + " for " + system + " . [MemoSegStartOfDayParser.Load]", 1);

            for (int index = 0; index < fileContents.Length; index++)
            {
                try
                {
                    if (fileContents[index].Substring(0, 1).Equals("5"))
                    {
                        if (!fileContents[index].Substring(16, 9).Trim().Equals(""))
                        {
                            DatabaseFunctions.MemoSegDatabaseFunctions.MemoSegExDeficitInsert(
                                "",
                                system,
                                fileContents[index].Substring(16, 9),
                                fileContents[index].Substring(1, 12),
                                (fileContents[index].Substring(32, 1).Equals("-") ? "-" : "+") + fileContents[index].Substring(33, 12) + "." + fileContents[index].Substring(45, 5),
                                dbCnStr);


                            counter++;

                            if ((counter % 10000) == 0)
                            {
                                Log.Write("Loaded : " + counter + " exdeficit items. [MemoSegStartOfDayParser.Load]", 1);
                            }
                        }
                    }
                }
                catch
                {
                    line = "";
                }
            }

            Log.Write("Loaded " + counter.ToString("#,##0") + " memo seg items. [MemoSegStartOfDayParser.Load]", 1);

            try
            {
                StockLoan.MemoSegData.Email.Send(KeyValue.Get("BroadRigdeSecurityMasterEmailTo", "mbattaini@penson.com;bhall@penson.com;", dbCnStr),
                                            KeyValue.Get("BroadRigdeSecurityMasterEmailFrom", "stockloan@penson.com", dbCnStr),
                                            system + " start of day exdeficit file uploaded",
                                            "Loaded " + counter.ToString("#,##0") + " exdeficit items.",
                                            dbCnStr);
            }
            catch { }
        }
    }
}
