using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class SantanderPositionsLoad
    {
        private string bookGroup;
        private string dbCnStr;
        private string externalDbCnStr;
        private string currentDate;

        public SantanderPositionsLoad(string currentDate, string bookGroup, string dbCnStr, string externalDbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.externalDbCnStr = externalDbCnStr;
            this.currentDate = currentDate;
            this.bookGroup = bookGroup;
        }

        public void Load()
        {
            long quantity = 0;
            long dvpFtd = 0;
            long dvpFtr = 0;
            string secId = "";

            DataSet dsPositions = new DataSet();            

            string sql = "select	(Select cross_Reference_cd from tsec_xref_key where security_adp_nbr = a.security_adp_nbr and type_xref_cd = 'CU' ) AS CUSP,\n" +
                           "        settlement_dt_qty\n" +
                           "from	dbo.taccount_sec_hldr a\n" +
                           "where	a.branch_cd = '250'\n" +
                           "And		a.account_cd = '47601'\n" +
                           "And		a.settlement_dt_qty <> 0";


            SqlConnection dbCn = new SqlConnection(externalDbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand(sql, dbCn);
                dbCmd.CommandType = CommandType.Text;

                SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
                dAdapter.Fill(dsPositions);

                foreach (DataRow item in dsPositions.Tables[0].Rows)
                {
                    quantity = long.Parse(item["settlement_dt_qty"].ToString().Replace(".00000", ""));

                    if (quantity < 0)
                    {
                        dvpFtd = 0;
                        dvpFtr = Math.Abs(quantity);
                    }
                    else
                    {
                        dvpFtr = 0;
                        dvpFtd = quantity;
                    }

                    Log.Write("Security ID: " + item["CUSP"].ToString().Trim() + " w/Position: " + quantity.ToString("#,##0"), 1);

                   DatabaseFunctions.BroadRidgeSatanderPositionItemSet(
                        currentDate,
                        bookGroup,
                        item["CUSP"].ToString().Trim(),
                        dvpFtr.ToString(),
                        dvpFtd.ToString(),
                        dbCnStr);
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.BroadRidgeShortAccountsLoad.Load]", 1);
                throw;
            }
        }
    }
}
