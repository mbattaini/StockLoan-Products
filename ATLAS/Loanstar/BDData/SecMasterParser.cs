using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using StockLoan.Common;
using DatabaseFunctions;

namespace BroadRidge.BusinessFiles
{
    class SecMasterParser
    {
        private string filePath = "";
        private string bizDate = "";
        private string dbCnStr = "";

        public SecMasterParser(string filePath, string bizDate, string dbCnStr)
        {
            this.filePath = filePath;
            this.bizDate = bizDate;
            this.dbCnStr = dbCnStr;
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
                fileHeaderDate = line.Substring(11, 6);
            }



            try
            {
                if (DateTime.ParseExact(fileHeaderDate, "MMddyy", null).ToString("yyyy-MM-dd").Equals(bizDate))
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


            string[] fileContents = File.ReadAllLines(filePath);
            Log.Write("Will start loading security master items for " + bizDate + ". [SecMasterParser.Load]", 1);

            for (int index = 0; index < fileContents.Length; index++)
            {
                try
                {              
                    if (fileContents[index].Length > 215)
                    {
                        if (fileContents[index].Substring(8, 12).Trim().Length >= 9)
                        {

                            DatabaseFunctions.SenderoDatabaseFunctions.SecMasterItemSet(fileContents[index].Substring(8, 9),
                                            fileContents[index].Substring(20, 12),
                                            fileContents[index].Substring(123, 12),
                                            fileContents[index].Substring(135, 8),
                                            dbCnStr);

                            DatabaseFunctions.SenderoDatabaseFunctions.SecMasterPriceBPSSet(
                                            fileContents[index].Substring(8, 9),
                                            fileContents[index].Substring(200, 7) + "." + fileContents[index].Substring(207, 7),
                                            fileContents[index].Substring(214, 6),
                                            dbCnStr);

                            DatabaseFunctions.SenderoDatabaseFunctions.BPSSecMasterItemSet(
                                            fileContents[index].Substring(0, 7),
                                            fileContents[index].Substring(8, 9),
                                            dbCnStr);

                            counter++;

                            if ((counter % 10000) == 0)
                            {
                                Log.Write("Loaded : " + counter + " security master items. [SecMasterParser.Load]", 1);
                            }
                        }
                    }
                }
                catch
                {            
                    line = "";
                }
            }

            Log.Write("Loaded " + counter.ToString("#,##0") + " security master items. [SecMasterParser.Load]", 1);

            try
            {
                StockLoan.BDData.Email.Send(KeyValue.Get("BroadRigdeSecurityMasterEmailTo", "mbattaini@penson.com", dbCnStr),
                                            KeyValue.Get("BroadRigdeSecurityMasterEmailFrom", "stockloan@penson.com", dbCnStr),
                                            KeyValue.Get("BroadRigdeSecurityMasterEmailSubject", "Broad Ridge Security Master Upload", dbCnStr),
                                            "Loaded " + counter.ToString("#,##0") + " security master items.",
                                            dbCnStr);
            }
            catch { }
        }
    }
}
