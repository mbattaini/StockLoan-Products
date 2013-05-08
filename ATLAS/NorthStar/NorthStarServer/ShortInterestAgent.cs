using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using StockLoan.Common;

namespace StockLoan.NorthStar
{
	public class ShortInterestAgent : MarshalByRefObject, IShortInterest
	{
		private string dbCnStr;

		public ShortInterestAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}

		public DataSet ShortInterestGet(
			string mpid,
			string symbol,
			string quantityGreaterThan,
			string quantityLessThan,
			string priceLessThan,
			string shortInterestMidMonthGreaterThan,
			string shortInterestMonthEndGreaterThan)
		{
			DataSet dsShortInterest = new DataSet();

			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				dbCmd = new SqlCommand("dbo.spNSShortInterestReport", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = int.Parse(KeyValue.Get("NSShortInterestReportTimeOut", "300", dbCnStr));

				if (!mpid.Equals(""))
				{
					SqlParameter paramMpid = dbCmd.Parameters.Add("@MPID", SqlDbType.Char, 5);
					paramMpid.Value = mpid;
				}

				if (!symbol.Equals(""))
				{
					SqlParameter paramSymbol = dbCmd.Parameters.Add("@SymbolCUSIP", SqlDbType.Char, 12);
					paramSymbol.Value = symbol;
				}

				if (!quantityGreaterThan.Equals(""))
				{
					SqlParameter paramQuantityGreaterThan = dbCmd.Parameters.Add("@SDQuantityMinimum", SqlDbType.Decimal);
					paramQuantityGreaterThan.Value = quantityGreaterThan;
				}

				if (!quantityLessThan.Equals(""))
				{
					SqlParameter paramQuantityLessThan = dbCmd.Parameters.Add("@SDQuantityMaximum", SqlDbType.Decimal);
					paramQuantityLessThan.Value = quantityLessThan;
				}

				if (!priceLessThan.Equals(""))
				{
					SqlParameter paramPriceLessThan = dbCmd.Parameters.Add("@PriceMaximum", SqlDbType.Decimal);
					paramPriceLessThan.Value = priceLessThan;
				}

				if (!shortInterestMidMonthGreaterThan.Equals(""))
				{
					SqlParameter paramShortInterestMidMonthGreaterThan = dbCmd.Parameters.Add("@ShortInterestMidMonthMininum_MIL", SqlDbType.Decimal);
					paramShortInterestMidMonthGreaterThan.Value = shortInterestMidMonthGreaterThan;
				}

				if (!shortInterestMonthEndGreaterThan.Equals(""))
				{
					SqlParameter paramShortInterestMonthEndGreaterThan = dbCmd.Parameters.Add("@ShortInterestMonthEndMininum_MIL", SqlDbType.Decimal);
					paramShortInterestMonthEndGreaterThan.Value = shortInterestMonthEndGreaterThan;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsShortInterest, "ShortInterest");
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortInterestAgent.ShortInterestGet]", Log.Error, 1);
			}

			return dsShortInterest;
		}

		public string ShortInterestDataDateGet()
		{
			string dataDate = "";

			try
			{				
				dataDate = KeyValue.Get("XFLShortInterestDataDate", "2001-01-01", dbCnStr);				
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortInterestAgent.ShortInterestDataDateGet]", Log.Error, 1);
			}

			return dataDate;
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
