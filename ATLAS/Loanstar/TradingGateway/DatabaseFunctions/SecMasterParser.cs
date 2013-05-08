using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using StockLoan.Common;

namespace BroadRidge.BusinessFiles
{
    class SecMasterParser
    {
        private string filePath = "";
        private string bizDate = "";

        public SecMasterParser(string filePath, string bizDate)
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


        public DataSet Load()
        {
            long counter = 0;
            string line = "-1";

            DataSet dsSecMaster = new DataSet();

            dsSecMaster.Tables.Add("SecMaster");
            dsSecMaster.Tables["SecMaster"].Columns.Add("Cusip");
            dsSecMaster.Tables["SecMaster"].Columns.Add("Symbol");
            dsSecMaster.Tables["SecMaster"].Columns.Add("SecDesc");
            dsSecMaster.Tables["SecMaster"].Columns.Add("Isin");
            dsSecMaster.Tables["SecMaster"].Columns.Add("Sedol");
            dsSecMaster.Tables["SecMaster"].Columns.Add("CommonCode");
            dsSecMaster.Tables["SecMaster"].Columns.Add("ClassCode");
            dsSecMaster.Tables["SecMaster"].Columns.Add("SpRating");
            dsSecMaster.Tables["SecMaster"].Columns.Add("MoodyRating");
            dsSecMaster.Tables["SecMaster"].Columns.Add("ClosingPrice");
            dsSecMaster.Tables["SecMaster"].Columns.Add("PriceDate");
            dsSecMaster.Tables["SecMaster"].Columns.Add("Currency");
            dsSecMaster.Tables["SecMaster"].Columns.Add("IsoCountryCode");
            dsSecMaster.Tables["SecMaster"].Columns.Add("CountryOrigin");
            dsSecMaster.AcceptChanges();


            if (!CheckFileHeaderDate())
            {
                return dsSecMaster;
            }


            TextReader textReader = new StreamReader(filePath);
            Log.Write("Will start loading security master items for " + bizDate + ". [SecMasterParser.Load]", 1);

            while (!line.Equals(""))
            {
                line = textReader.ReadLine();

                try
                {
                    if (line.ToString().Equals(""))
                    {
                        break;
                    }


                    if (line.Length > 215)
                    {
                        if (line.Substring(8, 12).Trim().Length >= 9)
                        {
                            DataRow tempDr = dsSecMaster.Tables["SecMaster"].NewRow();

                            tempDr["Cusip"] = line.Substring(8, 12);
                            tempDr["Symbol"] = line.Substring(20, 12);
                            tempDr["SecDesc"] = line.Substring(33, 90);
                            tempDr["Isin"] = line.Substring(123, 12);
                            tempDr["Sedol"] = line.Substring(135, 8);
                            tempDr["CommonCode"] = line.Substring(143, 10);
                            tempDr["ClassCode"] = line.Substring(163, 7);
                            tempDr["SpRating"] = line.Substring(296, 5);
                            tempDr["MoodyRating"] = line.Substring(291, 5);
                            tempDr["ClosingPrice"] = line.Substring(200, 14);
                            tempDr["PriceDate"] = line.Substring(214, 6);
                            tempDr["Currency"] = line.Substring(220, 3);
                            tempDr["IsoCountryCode"] = line.Substring(224, 2);
                            tempDr["CountryOrigin"] = line.Substring(226, 2);

                            dsSecMaster.Tables["SecMaster"].Rows.Add(tempDr);
                            dsSecMaster.AcceptChanges();

                            counter++;

                            if ((counter % 1000) == 0)
                            {
                                Log.Write("Read : " + counter + " security master items. [SecMasterParser.Load]", 1);
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

            Log.Write("Loaded " + counter.ToString("#,##0") + " security master items. [SecMasterParser.Load]", 1);
            return dsSecMaster;
        }
    }
}
