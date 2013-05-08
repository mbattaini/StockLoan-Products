using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.ParsingFiles
{
    class LoadBoxPosition
    {     
        private string dbCnStr;

        public LoadBoxPosition(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
        }

        public void Purge(string bizDate, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[LoadBoxPosition.Purge]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public void Load(string bizDatePrior, string bizDate, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionLoad", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[LoadBoxPosition.Load]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public void LoadPre(string bizDate, string bizDatePrior, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionPreSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[LoadBoxPosition.AgeFails]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public void AgeFails(string bizDate, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionFailDayCountSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[LoadBoxPosition.AgeFails]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public void IntradDay(string bizDate, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSDomesticIntraDayCalc", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[LoadBoxPosition.IntradDay]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }        
    }
}
