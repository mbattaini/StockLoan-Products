using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Main
{
	public class ProcessAgent : MarshalByRefObject, IProcess
	{
		private string dbCnStr = "";

		public ProcessAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}


		public DataSet ProcessStatusGet(string bizDate, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsProcessStatuses = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spProcessStatusGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter parmaBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				parmaBizDate.Value = bizDate;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsProcessStatuses, "ProcessStatuses");

                dsProcessStatuses.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ProcessAgent.ProcessStatusGet]", Log.Error, 1);
			}

			return dsProcessStatuses;
		}

		public DataSet ProcessMessageGet(string bizDate, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsProcessMessages = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spProcessMessageGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter parmaBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				parmaBizDate.Value = bizDate;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsProcessMessages, "ProcessMessages");

                dsProcessMessages.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ProcessAgent.ProcessMessageGet]", Log.Error, 1);
			}

			return dsProcessMessages;
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
