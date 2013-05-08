using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class ShortSaleBpsBillingBreakdownLoad
    {
        private string dbCnStr;
        private string bizDatePrior;
        private string bizDate;

        public ShortSaleBpsBillingBreakdownLoad(string bizDatePrior, string bizDate, string dbCnStr)
        {
            this.bizDatePrior = bizDatePrior;
            this.bizDate = bizDate;
            this.dbCnStr = dbCnStr;
        }

        public string Load()
        {
            string fileName = "";
            DataSet dsReport = new DataSet();

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spShortSaleBPSComparisonReport", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 1200;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
                dAdapter.Fill(dsReport, "Report");

                fileName = Excel.ExportDataSetToExcel(ref dsReport, "Report", "Billing Breakdown", true);             
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.ShortSaleBpsBillingBreakdownLoad.Load]", 1);
                throw;
            }

            return fileName;
        }
    }
}
