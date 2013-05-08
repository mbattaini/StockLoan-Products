using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using StockLoan.Common;
using DatabaseFunctions;

namespace BroadRidge.BusinessFiles
{
    class StockRecordParser
    {
        private string filePath = "";
        private string bizDate = "";
        private string bizDatePrior = "";
        private string dbCnStr = "";
        private DataSet dsStockRecord;

        public StockRecordParser(string filePath, string bizDatePrior, string bizDate, string dbCnStr)
        {
            this.filePath = filePath;
            this.bizDatePrior = bizDatePrior;
            this.bizDate = bizDate;
            this.dbCnStr = dbCnStr;
        }

        public bool CheckFileHeaderDate()
        {
            bool successful = false;

            string line;
            string fileHeaderDate = "";

            if (File.GetLastWriteTime(filePath) <= DateTime.Parse(bizDatePrior))
            {
                return false;
            }

            // read one line

            TextReader textReader = new StreamReader(filePath);

            line = textReader.ReadLine();

            if (!line.Equals(""))
            {
                fileHeaderDate = line.Substring(1, 6);
            }



            try
            {
                if (DateTime.ParseExact(fileHeaderDate, "MMddyy", null).ToString("yyyy-MM-dd").Equals(bizDatePrior))
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


        /*public void Load()
        {
            long counter = 0;
            string line = "-1";

            dsStockRecord = new DataSet();

            dsStockRecord.Tables.Add("StockRecord");
            dsStockRecord.Tables["StockRecord"].Columns.Add("BizDate");            
            dsStockRecord.Tables["StockRecord"].Columns.Add("AccountNumber");            
            dsStockRecord.Tables["StockRecord"].Columns.Add("SecurityId");
            dsStockRecord.Tables["StockRecord"].Columns.Add("CurrencyCode");
            dsStockRecord.Tables["StockRecord"].Columns.Add("AccountType");
            dsStockRecord.Tables["StockRecord"].Columns.Add("AccountTypeDesc");
            dsStockRecord.Tables["StockRecord"].Columns.Add("LocLocation");
            dsStockRecord.Tables["StockRecord"].Columns.Add("LocMemo");
            dsStockRecord.Tables["StockRecord"].Columns.Add("TradeQuantity");
            dsStockRecord.Tables["StockRecord"].Columns.Add("SettlementQuantity");            
            dsStockRecord.Tables["StockRecord"].AcceptChanges();
            dsStockRecord.AcceptChanges();


            if (!CheckFileHeaderDate())
            {
                return;
            }


            DatabaseFunctions.DatabaseFunctions.StockRecordBPSPurge(dbCnStr);
            Log.Write("Purged stock record items. [StockRecordParser.Load]", 1);

            TextReader textReader = new StreamReader(filePath);
            Log.Write("Will start loading stock record items for " + bizDate + ". [StockRecordParser.Load]", 1);

            
            
            
            while (!line.Equals(""))
            {
                FileStream
                StreamReader streamReader = new StreamReader();
                streamReader.ReadLine();
                
                line = textReader.ReadLine();

                try
                {
                    if ((line.ToString().Equals("") || (line[0] == 9)))
                    {
                        break;
                    }


                    if (line.Length >= 69)
                    {
                        if (line.Substring(11, 9).Trim().Length >= 9)
                        {
                            /*DatabaseFunctions.DatabaseFunctions.StockRecordItemSet(line.Substring(1, 9),
                            line.Substring(24, 1),
                            AccountTypeLookup(line.Substring(24, 1)),
                            line.Substring(12, 9),
                            line.Substring(25, 1),
                            line.Substring(26, 1),
                            line.Substring(21, 3),
                            QuantityParse(line.Substring(44, 1), line.Substring(27, 17)),
                            QuantityParse(line.Substring(61, 1), line.Substring(45, 17)),
                            dbCnStr);

                            DataRow drTemp = dsStockRecord.Tables["StockRecord"].NewRow();

                            drTemp["BizDate"] = KeyValue.Get("BizDate", "", dbCnStr);
                            drTemp["AccountNumber"] = line.Substring(1, 9);                            
                            drTemp["SecurityId"] = line.Substring(12, 9);
                            drTemp["CurrencyCode"] = line.Substring(21, 3);
                            drTemp["AccountType"] = line.Substring(24, 1);
                            drTemp["AccountTypeDesc"] = AccountTypeLookup(drTemp["AccountType"].ToString());                            
                            drTemp["LocLocation"] = line.Substring(25, 1);
                            drTemp["LocMemo"] = line.Substring(26, 1);
                            drTemp["TradeQuantity"] =  QuantityParse(line.Substring(44, 1), line.Substring(27, 17));
                            drTemp["SettlementQuantity"] = QuantityParse(line.Substring(61, 1), line.Substring(45, 17));                            
                            dsStockRecord.Tables["StockRecord"].Rows.Add(drTemp);
           
                            counter++;

                            if ((counter % 100000) == 0)
                            {
                                Log.Write("Read : " + counter + " stock record items. [StockRecordParser.Load]", 1);
                            }
                        }
                    }
                }
                catch
                {
                    textReader.Close();
                    line = "";
                }
            }

            Log.Write("Loaded " + counter.ToString("#,##0") + " stock record items. [StockRecordParser.Load]", 1);            
        }*/

        public void Load()
        {
            long counter = 0;
            string line = "-1";
            string dateTimeParse = "";

            dsStockRecord = new DataSet();
            dsStockRecord.RemotingFormat = SerializationFormat.Binary;

            dsStockRecord.Tables.Add("StockRecord");
            dsStockRecord.Tables["StockRecord"].Columns.Add("Identity");
            dsStockRecord.Tables["StockRecord"].Columns.Add("BizDate");
            dsStockRecord.Tables["StockRecord"].Columns.Add("AccountNumber");
            dsStockRecord.Tables["StockRecord"].Columns.Add("SecurityId");
            dsStockRecord.Tables["StockRecord"].Columns.Add("CurrencyCode");
            dsStockRecord.Tables["StockRecord"].Columns.Add("AccountType");
            dsStockRecord.Tables["StockRecord"].Columns.Add("AccountTypeDesc");
            dsStockRecord.Tables["StockRecord"].Columns.Add("LocLocation");
            dsStockRecord.Tables["StockRecord"].Columns.Add("LocMemo");
            dsStockRecord.Tables["StockRecord"].Columns.Add("TradeQuantity");
            dsStockRecord.Tables["StockRecord"].Columns.Add("SettlementQuantity");
            dsStockRecord.Tables["StockRecord"].Columns.Add("LastUpdated");
            dsStockRecord.Tables["StockRecord"].AcceptChanges();
            dsStockRecord.AcceptChanges();

            dsStockRecord.Tables["StockRecord"].Columns["Identity"].AutoIncrement = true;
            dsStockRecord.Tables["StockRecord"].Columns["Identity"].AutoIncrementStep = 1;



            dsStockRecord.Tables["StockRecord"].PrimaryKey = new DataColumn[] { dsStockRecord.Tables["StockRecord"].Columns["Identity"]};
            dsStockRecord.AcceptChanges();


            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }



            DatabaseFunctions.SenderoDatabaseFunctions.StockRecordBPSPurge(dbCnStr);
            Log.Write("Purged stock record items. [StockRecordParser.Load]", 1);


            string[] fileContents = File.ReadAllLines(filePath);
            Log.Write("Will start loading stock record items for " + bizDate + ". [StockRecordParser.Load]", 1);

            for (long index = 0; index < fileContents.LongLength; index ++)
            {
                try
               {                   
                    if ((fileContents[index].Equals("") || (fileContents[index][0] == 9)))
                    {
                        break;
                    }


                    if (fileContents[index].Length >= 69)
                    {
                        if (fileContents[index].Substring(11, 9).Trim().Length >= 9)
                        {
                            DataRow drTemp = dsStockRecord.Tables["StockRecord"].NewRow();

                            drTemp["BizDate"] = bizDate;
                            drTemp["AccountNumber"] = fileContents[index].Substring(1, 9);
                            drTemp["SecurityId"] = fileContents[index].Substring(12, 9);
                            drTemp["CurrencyCode"] = fileContents[index].Substring(21, 3);
                            drTemp["AccountType"] = fileContents[index].Substring(24, 1);
                            drTemp["AccountTypeDesc"] = "";
                            drTemp["LocLocation"] = fileContents[index].Substring(25, 1);
                            drTemp["LocMemo"] = fileContents[index].Substring(26, 1);
                            drTemp["TradeQuantity"] = QuantityParse(fileContents[index].Substring(44, 1), fileContents[index].Substring(27, 17));
                            drTemp["SettlementQuantity"] = QuantityParse(fileContents[index].Substring(62, 1), fileContents[index].Substring(45, 17));

                            dateTimeParse = fileContents[index].Substring(63, 6);


                            if (dateTimeParse.Equals("000000"))
                            {
                                drTemp["LastUpdated"] = DBNull.Value;
                            }
                            else
                            {
                                drTemp["LastUpdated"] = DateTime.ParseExact(dateTimeParse, "MMddyy", null).ToString(Standard.DateFormat);
                            }
                            
                            dsStockRecord.Tables["StockRecord"].Rows.Add(drTemp);

                            counter++;

                            if ((counter % 100000) == 0)
                            {
                                Log.Write("Read : " + counter + " stock record items. [StockRecordParser.Load]", 1);
                                
                                LoadDatabase();

                                dsStockRecord.Tables["StockRecord"].Rows.Clear();
                            }
                        }
                    }

                    fileContents[index] = null;
                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);
                    throw;
                }
            }
            
            LoadDatabase();

            Log.Write("Loaded " + counter.ToString("#,##0") + " stock record items. [StockRecordParser.Load]", 1);

            try
            {
                StockLoan.BDData.Email.Send(KeyValue.Get("BroadRigdeStockRecordEmailTo", "mbattaini@penson.com;bhall@penson.com;", dbCnStr),
                                        KeyValue.Get("BroadRigdeStockRecordEmailFrom", "stockloan@penson.com", dbCnStr),
                                        KeyValue.Get("BroadRigdeStockRecordEmailSubject", "Broad Ridge Stock Record Upload", dbCnStr),
                                        "Loaded " + counter.ToString("#,##0") + " stock record items.",
                                        dbCnStr);
            }
            catch { }
        }
                
        public void LoadDatabase()
        {            
            try
            {
                DatabaseFunctions.SenderoDatabaseFunctions.StockRecordBulkCopy(dsStockRecord.Tables["StockRecord"], dbCnStr);
            }
            catch
            {
                throw;
            }

        }    
       
        private decimal QuantityParse(string sign, string decimalValue)
        {
            string parsedDecimal = "";

            if (sign.Equals("-"))
            {
                parsedDecimal = "-" + decimalValue.Substring(0, 12) + "." + decimalValue.Substring(12, 5);
            }
            else
            {
                parsedDecimal = decimalValue.Substring(0, 12) + "." + decimalValue.Substring(12, 5);
            }

            return decimal.Parse(parsedDecimal);
        }
    }
}
