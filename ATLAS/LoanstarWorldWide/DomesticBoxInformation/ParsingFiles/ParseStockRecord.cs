using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using StockLoan.Common;

namespace StockLoan.ParsingFiles
{
    class ParseStockRecord
    {
        private DataSet dsStockRecord;
        private string dbCnStr;
        private string bookGroup;       
        private int interval = 0;

        public ParseStockRecord(string dbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;
            this.bookGroup = bookGroup;
            this.interval = 100;
        }

        public void Load(string filePath, string bizDatePrior, string bizDate)
        {
            StockRecordPurge();
            TextReader textReader = new StreamReader(filePath);
                        
            long counter = 0;
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
            
            if (!CheckFileDate(filePath, bizDatePrior))
            {
                throw new Exception("File is not for today.");
            }

            string[] fileContents = File.ReadAllLines(filePath);
            Log.Write("Will start loading stock record items for " + bizDate + ". [StockRecordParser.Load]", 1);

            for (long index = 0; index < fileContents.LongLength; index++)
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
            

            if (dsStockRecord.Tables["StockRecord"].Rows.Count > 0)
            {
                LoadDatabase();
            }
        }

        public bool CheckFileDate(string filePath, string bizDatePrior)
        {
            if (File.GetLastWriteTime(filePath).Date >= DateTime.Parse(bizDatePrior).Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadDatabase()
        {
            int count = 0;

            StockRecordBulkCopy(dsStockRecord.Tables["StockRecord"], dbCnStr);           
        }        

        public void StockRecordPurge()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spStockRecordBPSPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static void StockRecordBulkCopy(DataTable sourceTable, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            using (SqlBulkCopy s = new SqlBulkCopy(dbCn))
            {
                s.DestinationTableName = "tbStockRecordBPS";
                s.NotifyAfter = 10000;                
                s.SqlRowsCopied += new SqlRowsCopiedEventHandler(s_SqlRowsCopied);

                s.ColumnMappings.Add("BizDate", "BizDate");
                s.ColumnMappings.Add("AccountNumber", "AccountNumber");
                s.ColumnMappings.Add("AccountType", "AccountType");
                s.ColumnMappings.Add("AccountTypeDesc", "AccountTypeDesc");
                s.ColumnMappings.Add("LocLocation", "LocLocation");
                s.ColumnMappings.Add("LocMemo", "LocMemo");
                s.ColumnMappings.Add("CurrencyCode", "CurrencyCode");
                s.ColumnMappings.Add("SecurityId", "SecId");
                s.ColumnMappings.Add("TradeQuantity", "TradeQuantity");
                s.ColumnMappings.Add("SettlementQuantity", "SettlementQuantity");
                s.ColumnMappings.Add("LastUpdated", "LastUpdated");

                dbCn.Open();
                s.BulkCopyTimeout = 360;
                s.WriteToServer(sourceTable);
                s.Close();
            }
        }

        static void s_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Log.Write("Stock Record Rows copied so far: " + e.RowsCopied.ToString(), 1);
        }

        public int Count
        {
            get
            {
                return dsStockRecord.Tables["StockRecord"].Rows.Count;
            }
        }

        public int Interval
        {
            set
            {
                interval = value;
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
