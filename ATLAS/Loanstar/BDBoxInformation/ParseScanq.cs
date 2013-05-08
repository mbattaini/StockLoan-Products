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
    class ParseScanq
    {
        public event EventHandler<ProgressEventArgs> ProgressChanged = delegate { };

        private DataSet dsScanq;
        private string dbCnStr;
        private string bookGroup;
        private int lineNumber = 0;
        private int interval;

        public ParseScanq(string dbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;
            this.bookGroup = bookGroup;
            this.interval = 100;
        }

        public void Load(string filePath, string bizDatePrior, string bizDate)
        {
            BoxScanqPurge(bizDate);

            TextReader textReader = new StreamReader(filePath);
            string line1 = "";
            string line2 = "";

            dsScanq = new DataSet();
            dsScanq.Tables.Add("Scanq");
            dsScanq.Tables["Scanq"].Columns.Add("SecId");
            dsScanq.Tables["Scanq"].Columns.Add("Deliveries");
            dsScanq.AcceptChanges();

            if (!CheckFileDate(filePath, bizDatePrior))
            {
                throw new Exception("File is not for today.");
            }

            while (true)
            {
                line1 = textReader.ReadLine();

                if (line1 != null)
                {
                    lineNumber++;

                    if (!line1.Contains(Standard.ConfigValue("ScanqExceptions", "*** BOOK")))
                    {
                        line2 = textReader.ReadLine();
                        lineNumber++;

                        line2 = line2.Replace('\0', ' ');
                        if (line2 != null && !line2.Trim().Equals(""))
                        {

                            string cusip = line2.Substring(42, 9);
                            string code = line1.ToString().Substring(16, 5).Trim();

                            if (!cusip.Trim().Equals("") && code.Equals("TACAT"))
                            {
                                DataRow drTemp = dsScanq.Tables["Scanq"].NewRow();
                                drTemp["SecId"] = cusip;
                                drTemp["Deliveries"] = long.Parse(line1.Substring(22, 15).Replace(",", ""));
                                dsScanq.Tables["Scanq"].Rows.Add(drTemp);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            if (dsScanq.Tables["Scanq"].Rows.Count > 0)
            {
                LoadDatabase(bizDate);
            }
        }

        public bool CheckFileDate(string filePath, string bizDatePrior)
        {
            if (File.GetLastWriteTime(filePath).Date > DateTime.Parse(bizDatePrior).Date)
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

            foreach (DataRow drScanqItem in dsScanq.Tables["Scanq"].Rows)
            {
                BoxScanqItemSet(
                    bizDate,
                    bookGroup,
                    drScanqItem["SecId"].ToString(),
                    drScanqItem["Deliveries"].ToString());

                count++;
                
                if ((count % interval) == 0)
                {
                    UpdateProgress(count);
                }
            }
        }

        private void BoxScanqItemSet(string bizDate, string bookGroup, string secId, string deliveries)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxScanqItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramDeliveries = dbCmd.Parameters.Add("@Deliveries", SqlDbType.BigInt);
                paramDeliveries.Value = deliveries;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseScanq.BoxScanqItemSet]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        private void BoxScanqPurge(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxScanqPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseScanq.BoxScanqPurge]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public void UpdateProgress(long count)
        {
            EventHandler<ProgressEventArgs> progressEvent = ProgressChanged;

            progressEvent(null, new ProgressEventArgs(count));
        }

        public int Count
        {
            get
            {
                return dsScanq.Tables["Scanq"].Rows.Count;
            }
        }

        public int LineNumber
        {
            get
            {
                return lineNumber;
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
