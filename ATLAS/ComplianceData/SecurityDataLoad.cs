using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class SecurityDataLoad
    {
        private string dbCnStr;
        private string externalDbCnStr;
        private string bizDate;
        private string bizDatePrior;

        public SecurityDataLoad(string bizDate, string bizDatePrior, string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.bizDate = bizDate;
            this.bizDatePrior = bizDatePrior;
        }

        public void Load()
        {   // Ported from Penson Service, 2012-03-15 

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                Log.Write("Will load Security static data for " + bizDatePrior + ". [StockLoan.ComplianceData.SecurityDataLoad.Load]", 2);

                SqlCommand dbCmd = new SqlCommand("spSecurityDataLoad", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(KeyValue.Get("SecurityDataLoadTimeout", "9000", dbCn));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDatePrior;

                SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
                paramRecordsUpdated.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                Log.Write("Final Security Update count: " + paramRecordsUpdated.Value.ToString() + "  [StockLoan.ComplianceData.SecurityDataLoad.Load]", 2);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + "  [StockLoan.ComplianceData.SecurityDataLoad.Load]", Log.Error, 1);
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
