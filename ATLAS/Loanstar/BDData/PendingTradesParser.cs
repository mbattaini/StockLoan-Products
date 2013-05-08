using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using StockLoan.BDData;
using StockLoan.Common;

namespace BroadRidge.BusinessFiles
{
    class PendingTradesParser
    {
        private string filePath = "";
        private string bizDate = "";
        private string dbCnStr = "";
        private DataSet dsPendingTrades;
        //private byte bookMark = "";

        public PendingTradesParser(string filePath, string bizDate, string dbCnStr)
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
                fileHeaderDate = line.Substring(8, 8);
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


        public void LoadPendingTrades()
        {
            long counter = 0;
            string line = "-1";
            bool endOfFile = false;

            Reader.ReaderOutput readerOutput;
            Reader reader = new Reader(filePath);
            
            
            dsPendingTrades = new DataSet();

            dsPendingTrades.Tables.Add("PendingTrades");
            dsPendingTrades.Tables["PendingTrades"].Columns.Add("AccountNumber");
            dsPendingTrades.Tables["PendingTrades"].Columns.Add("SecId");
            dsPendingTrades.Tables["PendingTrades"].Columns.Add("BlotterCode");
            dsPendingTrades.Tables["PendingTrades"].Columns.Add("Quantity");            
            dsPendingTrades.AcceptChanges();


            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }

            DatabaseFunctions.SenderoDatabaseFunctions.PendingTradesBPSPurge(dbCnStr);
            Log.Write("Purged pending trades items. [StockRecordParser.LoadPendingTrades]", 1);            
            
            Log.Write("Will start loading pending trade items for " + bizDate + ". [PendingTradesParser.LoadPendingTrades]", 1);



            while(!endOfFile)
            {
                readerOutput = reader.ReadLines(10000);
                endOfFile = readerOutput.endOfFile;

                try
                {
                    foreach (object item in readerOutput.file)
                    {
                        line = (string)item;

                        if (line.Length >= 44)
                        {
                            DataRow drTemp = dsPendingTrades.Tables["PendingTrades"].NewRow();

                            drTemp["AccountNumber"] = line.Substring(32, 10);
                            drTemp["SecId"] = line.Substring(20, 12);
                            drTemp["BlotterCode"] = line.Substring(39, 2);
                            drTemp["Quantity"] = line.Substring(3, 17);

                            dsPendingTrades.Tables["PendingTrades"].Rows.Add(drTemp);

                            counter++;

                            if ((counter % 10000) == 0)
                            {
                                LoadDatabase();
                                dsPendingTrades.Tables["PendingTrades"].Rows.Clear();
                                dsPendingTrades.Tables["PendingTrades"].AcceptChanges();
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);
                    throw;
                }
                finally
                {
                    reader.Terminate();
                }
            }

            LoadDatabase();
            Log.Write("Loaded " + counter.ToString("#,##0") + " pending trade items. [PendingTradesParser.LoadPendingTrades]", 1);

            StockLoan.BDData.Email.Send(KeyValue.Get("BroadRigdePendingTradesEmailTo", "mbattaini@penson.com;bhall@penson.com;", dbCnStr),
                                        KeyValue.Get("BroadRigdePendingTradesEmailFrom", "stockloan@penson.com", dbCnStr),
                                        KeyValue.Get("BroadRigdePendingTradesEmailSubject", "Broad Ridge Pending Trades Upload", dbCnStr),
                                        "Loaded " + counter.ToString("#,##0") + " pending trades items.",
                                        dbCnStr);
        }

        public void LoadDatabase()
        {
            try
            {
                DatabaseFunctions.SenderoDatabaseFunctions.PendingTradsBulkCopy(dsPendingTrades.Tables["PendingTrades"], dbCnStr);
            }
            catch
            {
                throw;
            }
        }    
    }
}
