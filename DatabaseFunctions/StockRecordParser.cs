using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using StockLoan.Common;

namespace BroadRidge.BusinessFiles
{
    class StockRecordParser
    {
        private string filePath = "";
        private string bizDate = "";

        public StockRecordParser(string filePath, string bizDate)
        {
            this.filePath = filePath;
            this.bizDate = bizDate;
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
                fileHeaderDate = line.Substring(1, 6);
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


        public DataSet Load()
        {
            long counter = 0;
            string line = "-1";

            DataSet dsStockRecord = new DataSet();

            dsStockRecord.Tables.Add("StockRecord");
            dsStockRecord.Tables["StockRecord"].Columns.Add("AccountNumber");
            dsStockRecord.Tables["StockRecord"].Columns.Add("Firm");
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
                return dsStockRecord;
            }


            TextReader textReader = new StreamReader(filePath);
            Log.Write("Will start loading stock record items for " + bizDate + ". [StockRecordParser.Load]", 1);

            while (!line.Equals(""))
            {
                line = textReader.ReadLine();

                try
                {
                    if ((line.ToString().Equals("") || (line[0] == 9)) )
                    {
                        break;
                    }


                    if (line.Length >= 69)
                    {
                        if (line.Substring(11, 9).Trim().Length >= 9)
                        {
                            DataRow drTemp = dsStockRecord.Tables["StockRecord"].NewRow();

                            drTemp["AccountNumber"] = line.Substring(1, 9);
                            drTemp["Firm"] = line.Substring(9, 2);
                            drTemp["SecurityId"] = line.Substring(12, 9);
                            drTemp["CurrencyCode"] = line.Substring(21, 3);
                            drTemp["AccountType"] = line.Substring(24, 1);

                            switch (drTemp["AccountType"].ToString())
                            {
                                case "0":
                                    drTemp["AccountTypeDesc"] = "Other";
                                    break;

                                case "1":
                                    drTemp["AccountTypeDesc"] = "CASH";
                                    break;

                                case "2":
                                    drTemp["AccountTypeDesc"] = "MARGIN";
                                    break;

                                case "3":
                                    drTemp["AccountTypeDesc"] = "INCOME";
                                    break;


                                case "5":
                                    drTemp["AccountTypeDesc"] = "SHORT";
                                    break;

                                case "4":
                                case "6":
                                case "7":
                                case "8":
                                    drTemp["AccountTypeDesc"] = "OPEN";
                                    break;

                                case "9":
                                    drTemp["AccountTypeDesc"] = "DVP/RVP (COD/COR)";
                                    break;

                            }


                            drTemp["LocLocation"] = line.Substring(25, 1);
                            drTemp["LocMemo"] = line.Substring(26, 1);

                            if (line.Substring(44, 1).Equals("-"))
                            {
                                drTemp["TradeQuantity"] = long.Parse(line.Substring(27, 17)) * -1;
                            }
                            else
                            {
                                drTemp["TradeQuantity"] = long.Parse(line.Substring(27, 17));
                            }

                            if (line.Substring(61, 1).Equals("-"))
                            {
                                drTemp["SettlementQuantity"] = long.Parse(line.Substring(45, 17)) * -1;
                            }
                            else
                            {
                                drTemp["SettlementQuantity"] = long.Parse(line.Substring(45, 17));
                            }

                            dsStockRecord.Tables["StockRecord"].Rows.Add(drTemp);

                            counter++;

                            if ((counter % 100000) == 0)
                            {
                                Log.Write("Read : " + counter + " security master items. [StockRecordParser.Load]", 1);
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
            return dsStockRecord;
        }
    }
}
