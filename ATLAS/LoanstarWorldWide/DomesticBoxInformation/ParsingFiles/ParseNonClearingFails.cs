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
    class ParseNonClearingFails
    {
        public event EventHandler<ProgressEventArgs> ProgressChanged = delegate { };

        private DataSet dsFails;
        private string dbCnStr;
        private string bookGroup;
        private int interval;

        public ParseNonClearingFails(string dbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;
            this.bookGroup = bookGroup;
            this.interval = 100;
        }

        public void Load(string filePath, string bizDatePrior, string bizDate)
        {            
            TextReader textReader = new StreamReader(filePath);
            
            string line = "";
            string spinCode = "";
            DateTime settleDate;
            decimal quantity;

            dsFails = new DataSet();
            dsFails.Tables.Add("Fails");
            dsFails.Tables["Fails"].Columns.Add("SecId");            
            dsFails.Tables["Fails"].Columns.Add("DvpFTDSettled");
            dsFails.Tables["Fails"].Columns.Add("DvpFTDTraded");
            dsFails.Tables["Fails"].Columns.Add("DvpFTRSettled");
            dsFails.Tables["Fails"].Columns.Add("DvpFTRTraded");
            dsFails.Tables["Fails"].Columns.Add("BrokerFTDSettled");
            dsFails.Tables["Fails"].Columns.Add("BrokerFTDTraded");
            dsFails.Tables["Fails"].Columns.Add("BrokerFTRSettled");
            dsFails.Tables["Fails"].Columns.Add("BrokerFTRTraded");
            dsFails.AcceptChanges();

            line = textReader.ReadLine();

            if (!CheckFileDate( line.Substring(24, 6), bizDate))
            {
                throw new Exception("File is not for today.");
            }

            try
            {
                while (true)
                {
                    line = textReader.ReadLine();
                    if (line != null)
                    {
                        if (line.Substring(116, 2).Trim().Equals("U$"))
                        {
                            try
                            {
                                spinCode = line.Substring(3, 1);
                                settleDate = DateTime.ParseExact(line.Substring(23, 8), "yyyyMMdd", null);
                                quantity = decimal.Parse(line.Substring(57, 12));

                                DataRow drTemp = dsFails.Tables["Fails"].NewRow();

                                drTemp["SecId"] = line.Substring(13, 9);

                                switch (spinCode)
                                {
                                    case "1":
                                        drTemp["DvpFTDSettled"] = 0;
                                        drTemp["DvpFTDTraded"] = 0;
                                        drTemp["DvpFTRSettled"] = (settleDate <= DateTime.Parse(bizDate)) ? quantity : 0;
                                        drTemp["DvpFTRTraded"] = (settleDate > DateTime.Parse(bizDate)) ? quantity : 0;
                                        drTemp["BrokerFTDSettled"] = 0;
                                        drTemp["BrokerFTDTraded"] = 0;
                                        drTemp["BrokerFTRSettled"] = 0;
                                        drTemp["BrokerFTRTraded"] = 0;
                                        break;

                                    case "3":
                                        drTemp["DvpFTDSettled"] = 0;
                                        drTemp["DvpFTDTraded"] = 0;
                                        drTemp["DvpFTRSettled"] = 0;
                                        drTemp["DvpFTRTraded"] = 0;
                                        drTemp["BrokerFTDSettled"] = 0;
                                        drTemp["BrokerFTDTraded"] = 0;
                                        drTemp["BrokerFTRSettled"] = (settleDate <= DateTime.Parse(bizDate)) ? quantity : 0;
                                        drTemp["BrokerFTRTraded"] = (settleDate > DateTime.Parse(bizDate)) ? quantity : 0;
                                        break;

                                    case "4":
                                        drTemp["DvpFTDSettled"] = (settleDate <= DateTime.Parse(bizDate)) ? quantity : 0;
                                        drTemp["DvpFTDTraded"] = (settleDate > DateTime.Parse(bizDate)) ? quantity : 0;
                                        drTemp["DvpFTRSettled"] = 0;
                                        drTemp["DvpFTRTraded"] = 0;
                                        drTemp["BrokerFTDSettled"] = 0;
                                        drTemp["BrokerFTDTraded"] = 0;
                                        drTemp["BrokerFTRSettled"] = 0;
                                        drTemp["BrokerFTRTraded"] = 0;
                                        break;

                                    case "6":
                                        drTemp["DvpFTDSettled"] = 0;
                                        drTemp["DvpFTDTraded"] = 0;
                                        drTemp["DvpFTRSettled"] = 0;
                                        drTemp["DvpFTRTraded"] = 0;
                                        drTemp["BrokerFTDSettled"] = (settleDate <= DateTime.Parse(bizDate)) ? quantity : 0;
                                        drTemp["BrokerFTDTraded"] = (settleDate > DateTime.Parse(bizDate)) ? quantity : 0;
                                        drTemp["BrokerFTRSettled"] = 0;
                                        drTemp["BrokerFTRTraded"] = 0;
                                        break;
                                }

                                dsFails.Tables["Fails"].Rows.Add(drTemp);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (dsFails.Tables["Fails"].Rows.Count > 0)
                {
                    LoadDatabase(bizDate);
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [ParseNonClearingFails.Load]", 1);
                throw;
            }
            finally
            {
                textReader.Close();
            }
        }

        public bool CheckFileDate(string fileDate, string bizDate)
        {
            if (DateTime.ParseExact(fileDate, "MMddyy", null).ToString("yyyy-MM-dd").Equals(bizDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadDatabase(string bizDate)
        {
            int count = 0;

            foreach (DataRow drFailItem in dsFails.Tables["Fails"].Rows)
            {
                    BoxFailsItemSet(
                        bizDate,
                        bookGroup,
                        drFailItem["SecId"].ToString(),
                        drFailItem["DvpFTDTraded"].ToString(),
                        drFailItem["DvpFTRTraded"].ToString(),
                        drFailItem["DvpFTDSettled"].ToString(),
                        drFailItem["DvpFTRSettled"].ToString(),
                        drFailItem["BrokerFTDTraded"].ToString(),
                        drFailItem["BrokerFTRTraded"].ToString(),
                        drFailItem["BrokerFTDSettled"].ToString(),
                        drFailItem["BrokerFTRSettled"].ToString());

                    count++;

                    if ((count % interval) == 0)
                    {
                        UpdateProgress(count);
                    }
            }
        }

        public void UpdateProgress(long count)
        {
            EventHandler<ProgressEventArgs> progressEvent = ProgressChanged;

            progressEvent(null, new ProgressEventArgs(count));
        }

        private void BoxFailsItemSet(
            string bizDate, 
            string bookGroup, 
            string secId,
            string dvpFTDTraded,
            string dvpFTRTraded,
            string dvpFTDSettled,
            string dvpFTRSettled,
            string brokerFTDTraded,
            string brokerFTRTraded,
            string brokerFTDSettled,
            string brokerFTRSettled)
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxFailsItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramDvpFTDTraded = dbCmd.Parameters.Add("@DvpFTDTraded", SqlDbType.BigInt);
                paramDvpFTDTraded.Value = long.Parse(dvpFTDTraded);

                SqlParameter paramDvpFTRTraded = dbCmd.Parameters.Add("@DvpFTRTraded", SqlDbType.BigInt);
                paramDvpFTRTraded.Value = long.Parse(dvpFTRTraded);

                SqlParameter paramDvpFTDSettled = dbCmd.Parameters.Add("@DvpFTDSettled", SqlDbType.BigInt);
                paramDvpFTDSettled.Value = long.Parse(dvpFTDSettled);

                SqlParameter paramDvpFTRSettled = dbCmd.Parameters.Add("@DvpFTRSettled", SqlDbType.BigInt);
                paramDvpFTRSettled.Value = long.Parse(dvpFTRSettled);

                SqlParameter paramBrokerFTDTraded = dbCmd.Parameters.Add("@BrokerFTDTraded", SqlDbType.BigInt);
                paramBrokerFTDTraded.Value = long.Parse(brokerFTDTraded);

                SqlParameter paramBrokerFTRTraded = dbCmd.Parameters.Add("@BrokerFTRTraded", SqlDbType.BigInt);
                paramBrokerFTRTraded.Value = long.Parse(brokerFTRTraded);

                SqlParameter paramBrokerFTDSettled = dbCmd.Parameters.Add("@BrokerFTDSettled", SqlDbType.BigInt);
                paramBrokerFTDSettled.Value = long.Parse(brokerFTDSettled);

                SqlParameter paramBrokerFTRSettled = dbCmd.Parameters.Add("@BrokerFTRSettled", SqlDbType.BigInt);
                paramBrokerFTRSettled.Value = long.Parse(brokerFTRSettled);


                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseNonClearingFails.BoxFailsItemSet]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public void BoxFailsPurge(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxFailsPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseNonClearingFails.BoxFailsPurge]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public int Count
        {
            get
            {
                return dsFails.Tables["Fails"].Rows.Count;
            }
        }

        public int Interval
        {
            set
            {
                interval = value;
            }
        }
    }
}
