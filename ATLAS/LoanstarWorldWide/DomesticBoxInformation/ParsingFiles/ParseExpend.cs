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
    class ParseExpend
    {
        public event EventHandler<ProgressEventArgs> ProgressChanged = delegate { };

        public DataSet dsExpend;
        private string dbCnStr;
        private string bookGroup;       
        private int interval = 0;

        public ParseExpend(string dbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;
            this.bookGroup = bookGroup;
            this.interval = 100;
        }

        public void Load(string filePath, string bizDatePrior, string bizDate)
        {
            BoxExpendPurge(bizDate);

            TextReader textReader = new StreamReader(filePath);
            
            string line = "";
            string sign = "";            
            long quantity = 0;
            long netFailQuantity = 0;

            dsExpend = new DataSet();
            dsExpend.Tables.Add("Expend");
            dsExpend.Tables["Expend"].Columns.Add("SecId");
            dsExpend.Tables["Expend"].Columns.Add("Excess");
            dsExpend.Tables["Expend"].Columns.Add("SiacReceive");
            dsExpend.Tables["Expend"].Columns.Add("SiacDelivery");
            dsExpend.AcceptChanges();

            
            if (!CheckFileDate(filePath, bizDatePrior))
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
                        DataRow drTemp = dsExpend.Tables["Expend"].NewRow();

                        quantity = long.Parse(line.Substring(168, 13));
                        netFailQuantity = long.Parse(line.Substring(123, 14));

                        drTemp["SecId"] = line.Substring(13, 9);
                        drTemp["Excess"] = (line.Substring(167, 1).Equals("+") ? quantity : quantity * -1);
                        drTemp["SiacReceive"] = (line.Substring(122, 1).Equals("+") ? 0 : netFailQuantity);
                        drTemp["SiacDelivery"] = (line.Substring(122, 1).Equals("+") ? netFailQuantity : 0);

                        dsExpend.Tables["Expend"].Rows.Add(drTemp);
                    }
                    else
                    {
                        break;
                    }
                }

                if (dsExpend.Tables["Expend"].Rows.Count > 0)
                {
                    LoadDatabase(bizDate);
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [ParseExpend.Load]", 1);
                throw;
            }
            finally
            {
                textReader.Close();
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

            foreach (DataRow drExpendItem in dsExpend.Tables["Expend"].Rows)
            {
                BoxExpendItemSet(
                    bizDate,
                    bookGroup,
                    drExpendItem["SecId"].ToString(),
                    drExpendItem["Excess"].ToString(),
                    drExpendItem["SiacReceive"].ToString(),
                    drExpendItem["SiacDelivery"].ToString());

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

        private void BoxExpendItemSet(
            string bizDate, 
            string bookGroup, 
            string secId,
            string excess,
            string siacReceive,
            string siacDelivery)
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxExpendItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramExcess = dbCmd.Parameters.Add("@Excess", SqlDbType.BigInt);
                paramExcess.Value = long.Parse(excess);

                SqlParameter paramSiacReceive = dbCmd.Parameters.Add("@SiacReceive", SqlDbType.BigInt);
                paramSiacReceive.Value = long.Parse(siacReceive);

                SqlParameter paramSiacDelivery = dbCmd.Parameters.Add("@SiacDelivery", SqlDbType.BigInt);
                paramSiacDelivery.Value = long.Parse(siacDelivery);
                
                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseExpend.BoxExpendItemSet]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public void BoxExpendPurge(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxExpendPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseExpend.BoxExpendPurge]");
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
                return dsExpend.Tables["Expend"].Rows.Count;
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
