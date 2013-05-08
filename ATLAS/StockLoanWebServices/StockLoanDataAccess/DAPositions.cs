using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using StockLoan.Common;

namespace StockLoanDataAccess
{
    public class DAPositions
    {
        private static string dbCnStr = DAConfig.DbCnStr;

        public static DataSet AccountPositionGet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, string secId, bool isActive)
		{
            SqlConnection dbCn = new SqlConnection(dbCnStr);
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAccountPositionGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!firm.Equals(""))
				{
					SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.Char, 2);
					paramFirm.Value = firm;   			
				}

				if (!locMemo.Equals(""))
				{
					SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.Char, 1);
					paramLocMemo.Value = locMemo;   			
				}
				
				if (!accountType.Equals(""))
				{
					SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.Char, 1);
					paramAccountType.Value = accountType;   			
				}
				
				if (!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.Char, 2);
					paramAccountNumber.Value = accountNumber;   			
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;   			
				}
							
				if (!currencyCode.Equals(""))
				{
					SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 3);
					paramCurrencyCode.Value = currencyCode;   			
				}		

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;   							
				  
				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "AccountPosition");
        
				dataSet.Tables["AccountPosition"].PrimaryKey = new DataColumn[4]
                {
                    dataSet.Tables["AccountPosition"].Columns["Firm"],
                    dataSet.Tables["AccountPosition"].Columns["LocMemo"],
                    dataSet.Tables["AccountPosition"].Columns["AccountType"],
                    dataSet.Tables["AccountPosition"].Columns["AccountNumber"]
                };
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AccountPositionGet]", Log.Error, 1);
				throw;
			}

            return dataSet;
		}


    }

}
