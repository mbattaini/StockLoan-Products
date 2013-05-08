using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class BroadRidgeShortAccountsLoad
    {
        private string dbCnStr;
        private string externalDbCnStr;
        private string currentDate;

        public BroadRidgeShortAccountsLoad(string currentDate, string dbCnStr, string externalDbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.externalDbCnStr = externalDbCnStr;
            this.currentDate = currentDate;
        }

        public void Load()
        {
            DataSet dsPositions = new DataSet();

            DatabaseFunctions.BroadRidgeShortAccountsPurge(currentDate, dbCnStr);
            
            string sql =    "select	(branch_cd + account_cd) As AccountNumber,\n" +
                            "substring(cusip_intrl_nbr,1, 9) As SecId,\n" +
                            "settlement_dt_qty,\n" +
                            "updt_last_tmstp\n" +
                            "from	taccount_sec_hldr\n" +
                            "where	type_account_cd in ('1','2','5')\n" +
                            "and		branch_cd >= '200'\n" +
                            "and		branch_cd <= '999'\n" +
                            "and		settlement_dt_qty < 0\n" +
                            "and        SUBSTRING(cusip_intrl_nbr,1,2) <> 'XX'\n" +
                            "and        RTrim(LTrim(account_cd)) <> '99999' \n";    //Exclude Wash Accounts 

            SqlConnection dbCn = new SqlConnection(externalDbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand(sql, dbCn);
                dbCmd.CommandType = CommandType.Text;

                SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
                dAdapter.Fill(dsPositions);

                foreach (DataRow item in dsPositions.Tables[0].Rows)
                {
                    DatabaseFunctions.BroadRidgeShortAccountItemSet(currentDate, item["AccountNumber"].ToString(), item["SecId"].ToString(), decimal.Parse(item["settlement_dt_qty"].ToString()).ToString("#000"), item["updt_last_tmstp"].ToString(), dbCnStr);
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
