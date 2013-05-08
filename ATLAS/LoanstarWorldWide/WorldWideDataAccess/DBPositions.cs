using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBPositions
    {
		private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet BoxPositionsGet(string bizDate, string bookGroup, string secId)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsTemp = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("dbo.spBoxPositionGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsTemp, "BoxPosition");
			}
			catch
			{
				throw;
			}

			return dsTemp;
        }

        public static DataSet BoxSummaryGet(string bizDate, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBoxSummaryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;


                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "BoxSummary");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }
    }
}
