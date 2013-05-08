using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class SnapshotWrapperLoad
    {
        private string bookGroup;
        private string dbCnStr;
        private string externalDbCnStr;
        private string currentDate;
        private string bizDatePrior;

        public SnapshotWrapperLoad(string currentDate, string bizDatePrior, string bookGroup, string dbCnStr)
        {
            this.dbCnStr = dbCnStr;            
            this.currentDate = currentDate;
            this.bizDatePrior = bizDatePrior;
            this.bookGroup = bookGroup;
        }

        public void Load()
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryBPSOvernightSnapShotControlWrapper", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 120000;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = currentDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.SnapshotWrapperLoad.Load]", 1);
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
