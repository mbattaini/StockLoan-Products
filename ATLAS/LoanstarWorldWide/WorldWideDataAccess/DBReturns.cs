using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBReturns
    { 
		private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static int ReturnAsOfSet(
            string tradeDate, 
            string settleDate, 
            string bookGroup, 
            string book, 
            string contractId,
			string contractType, 
            string returnId, 
            string quantity, 
            string actUserId)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			int rowCount = 0;

			try
			{
				SqlCommand dbCmd = new SqlCommand("dbo.spRetroReturnSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = tradeDate;

				if (!settleDate.Equals(""))
				{
					SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
					paramSettleDate.Value = settleDate;
				}

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
				paramContractId.Value = contractId;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 3);
				paramContractType.Value = contractType;

				SqlParameter paramReturnId = dbCmd.Parameters.Add("@ReturnId", SqlDbType.VarChar, 16);
				paramReturnId.Value = returnId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.Int);
				paramRecordsUpdated.Direction = ParameterDirection.Output;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
                dbCn.Close();

				rowCount = (int)paramRecordsUpdated.Value;
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

			return rowCount;

        }

        public static void ReturnSet(
            string returnId, 
            string bizDate, 
            string bookGroup,
            string book, 
            string contractId, 
            string contractType,
		    string quantity, 
            string settleDateProjected, 
            string settleDateActual,
            string actUserId,
            bool isActive)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("dbo.spReturnSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

				SqlParameter paramReturnId = dbCmd.Parameters.Add("@ReturnId", SqlDbType.VarChar, 16);
				paramReturnId.Value = returnId;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
				paramContractId.Value = contractId;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 3);
				paramContractType.Value = contractType;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;

				if (!settleDateProjected.Equals(""))
				{
					SqlParameter paramSettleDateProjected = dbCmd.Parameters.Add("@SettleDateProjected", SqlDbType.DateTime);
					paramSettleDateProjected.Value = settleDateProjected;
				}
				
                if (!settleDateActual.Equals(""))
				{
					SqlParameter paramSettleDateActual = dbCmd.Parameters.Add("@SettleDateActual", SqlDbType.DateTime);
					paramSettleDateActual.Value = settleDateActual;
				}
				
                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;

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

        public static DataSet ReturnsGet(
            string returnId, 
            string bizDate, 
            string bookGroup, 
            string contractId, 
            short utcOffset)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsTemp = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("dbo.spReturnsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

				if (!returnId.Equals(""))
				{
					SqlParameter paramReturnId = dbCmd.Parameters.Add("@ReturnId", SqlDbType.VarChar, 16);
					paramReturnId.Value = returnId;
				}
				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;
				}
				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}
				if (!contractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
					paramContractId.Value = contractId;
				}
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsTemp, "Returns");
			}
			catch
			{
				throw;
			}

			return dsTemp;
        }
    }
}
