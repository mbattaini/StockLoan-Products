using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class StraddlesLoad
    {
        private string dbCnStr;
        private string externalDbCnStr;
        private string currentDate;

        public StraddlesLoad(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
        }

        public string Load()
        {
            string fileName = "";
            DataSet dsPositions = new DataSet();

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBPSStraddlesget", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 1200;

                SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
                dAdapter.Fill(dsPositions, "Positions");

                fileName = Excel.ExportDataSetToExcel(ref dsPositions, "Positions", "Straddles", true);             
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.StraddlesLoad.Load]", 1);
                throw;
            }

            return fileName;
        }
    }
}
