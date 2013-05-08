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
    public class BoxPositionDayCountLoad
    {
        private string dbCnStr;
        private string bizDate;

        public BoxPositionDayCountLoad(string bizDate, string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.bizDate = bizDate;
        }

        public void Load()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                Log.Write("Will load Box Position Fail Day Count  for " + bizDate + ". [StockLoan.ComplianceData.BoxPositionDayCountLoad.Load]", 2);

                SqlCommand dbCmd = new SqlCommand("spBoxPositionFailDayCountSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(KeyValue.Get("SecurityDataLoadTimeout", "9000", dbCn));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                Log.Write("Loaded load Box Position Fail Day Count  for " + bizDate + ". [StockLoan.ComplianceData.BoxPositionDayCountLoad.Load]", 2);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + "  [StockLoan.ComplianceData.BoxPositionDayCountLoad.Load]", Log.Error, 1);
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
