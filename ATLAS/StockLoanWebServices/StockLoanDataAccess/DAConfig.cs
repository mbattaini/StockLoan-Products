using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration; 
using StockLoan.Common;

namespace StockLoanDataAccess
{
    public class DAConfig
    {
        private static string dbCnStr = DAConfig.DbCnStr;

        public static string DbCnStr
        {
            get
            {
                return ("Trusted_Connection=yes; " +
                 "Data Source=" + DAConfig.ConfigValue("MainDatabaseHost", "") + "; " +
                 "Initial Catalog=" + DAConfig.ConfigValue("MainDatabaseName", "") + ";");
            }
        }

        public static string ConfigValue(string key, string defaultValue)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[key];
                if (s == null)
                {
                    return defaultValue;
                }
                else
                {
                    return s;
                }
            }
            catch
            {
                throw;
            }
        }

        public static string KeyValuesGet(string keyId, string keyValue)
        {
            try
            {
                keyValue = KeyValue.Get(keyId, keyValue, dbCnStr);
            }
            catch
            {
                throw;
            }

            return keyValue;
        }

        public static DataSet KeyValuesGet()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsKeyValues = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spKeyValueGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsKeyValues, "KeyValues");
            }
            catch
            {
                throw;
            }

            return dsKeyValues;
        }

        public static void KeyValueSet(string keyId, string keyValue)
        {
            if (keyId.Trim().Equals(""))
            {
                throw new Exception("Key ID is required");
            }

            try
            {
                KeyValue.Set(keyId, keyValue, dbCnStr);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet ReportValueGet(string reportName)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsReportValues = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spReportValuesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsReportValues, "KeyValues");
            }
            catch
            {
                throw;
            }

            return dsReportValues;
        }

        public static void ReportValueSet(string reportName, int reportRecipientNumber, string reportRecipient, string lastDeliveredDate, string format, string justify)
        {
            if (reportName.Trim().Equals(""))
            {
                throw new Exception("Report Name is required");
            }

            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spReportValueSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

                SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
                paramReportName.Value = reportName;

                SqlParameter paramReportRecipientNumber = dbCmd.Parameters.Add("@ReportRecipientNumber", SqlDbType.BigInt);
                paramReportRecipientNumber.Value = reportRecipientNumber;

                SqlParameter paramReportRecipient = dbCmd.Parameters.Add("@ReportRecipient", SqlDbType.VarChar, 50);
                paramReportRecipient.Value = reportRecipient;

                SqlParameter paramLastDeliveredDate = dbCmd.Parameters.Add("@LastDeliveredDate", SqlDbType.DateTime);
                paramLastDeliveredDate.Value = lastDeliveredDate;

                SqlParameter paramFormat = dbCmd.Parameters.Add("@Format", SqlDbType.VarChar, 50);
                paramFormat.Value = format;

                SqlParameter paramJustify = dbCmd.Parameters.Add("@Justify", SqlDbType.VarChar, 10);
                paramJustify.Value = justify;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch
            {
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




    }
}
