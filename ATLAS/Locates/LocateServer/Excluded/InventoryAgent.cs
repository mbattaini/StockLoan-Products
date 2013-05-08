using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Medalist
{
	public class InventoryAgent : MarshalByRefObject, IInventory
	{
		private string dbCnStr = "";

		public InventoryAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}

		public DataSet InventoryGet(string desk, string secId, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsInventory = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!desk.Equals(""))
				{
					SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
					paramDesk.Value = desk;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsInventory, "Inventory");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [InventoryAgent.InventoryGet]", Log.Error, 1);
			}

			return dsInventory;
		}

		public DataSet InventoryControlGet(string bizDate)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsInventory = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryControlGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter parmaBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				parmaBizDate.Value = bizDate;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsInventory, "InventoryControl");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [InventoryAgent.InventoryControlGet]", Log.Error, 1);
			}

			return dsInventory;
		}

		public void InventoryItemSet(string bizDate, string desk, string secId, string quantity)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryItemSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter parmaBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				parmaBizDate.Value = bizDate;

				SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
				paramDesk.Value = desk;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [InventoryAgent.InventoryItemSet]", Log.Error, 1);
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

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
