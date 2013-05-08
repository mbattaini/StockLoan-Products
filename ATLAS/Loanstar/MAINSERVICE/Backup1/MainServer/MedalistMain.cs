using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Main
{
	public class MainMain
	{
		private string countryCode;

		private string dbCnStr;
		private SqlConnection dbCn = null;

		private Thread mainThread = null;

		private static bool isStopped = true;
		private static string tempPath;		

		public MainMain(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MainMain.MainMain]", Log.Error, 1);
			}

			countryCode = Standard.ConfigValue("CountryCode", "US");
			Log.Write("Using country code: " + countryCode + " [MainMain.MainMain]", 2);

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [MainMain.MainMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [MainMain.MainMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [MainMain.MainMain]", 2);


			switch (Standard.ConfigValue("UseSystemSettlementEngine").ToUpper())
			{
				case ("YES"):
					Master.UseSystemSettlementEngine = true;
					Log.Write(" System Settlements Engine enabled. [MainMain.MainMain]", 2);
					break;

				case ("NO"):
					Master.UseSystemSettlementEngine = false;
					Log.Write(" System Settlements Engine disabled. [MainMain.MainMain]", 2);
					break;

				default:
					Master.UseSystemSettlementEngine = false;
					Log.Write(" System Settlements Engine disabled. [MainMain.MainMain]", 2);
					break;
			}

		}

		~MainMain()
		{
			if (dbCn != null)
			{
				dbCn.Dispose();
			}
		}

		public void Start()
		{
			isStopped = false;

			if ((mainThread == null) || (!mainThread.IsAlive)) // Must create new thread.
			{
				mainThread = new Thread(new ThreadStart(MainMainLoop));
				mainThread.Name = "Main";
				mainThread.Start();

				Log.Write("Start command issued with new main thread. [MainMain.Start]", 3);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with main thread already running. [MainMain.Start]", 3);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (mainThread == null)
			{
				Log.Write("Stop command issued, main thread never started. [MainMain.Stop]", 3);
			}
			else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				mainThread.Abort();
				Log.Write("Stop command issued, sleeping main thread aborted. [MainMain.Stop]", 3);
			}
			else
			{
				Log.Write("Stop command issued, main thread is still active. [MainMain.Stop]", 3);
			}
		}

		private void MainMainLoop()
		{
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
				Log.Write("Start of cycle. [MainMain.MainMainLoop]", 2);
				KeyValue.Set("MainMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

				BizDatesSet(Standard.HolidayType.Any);
				BizDatesSet(Standard.HolidayType.Bank);
				BizDatesSet(Standard.HolidayType.Exchange);
				if (isStopped) break;

				BookGroupBizDateRoll();
				if (isStopped) break;

				FundingRatesBizDateRoll();
				if (isStopped) break;

				if (Master.UseSystemSettlementEngine)
				{
					SettlementSystsemProcess();
					ContractBizDateSystemRoll();
				}
				else
				{
					ContractBizDateProcessRoll();
				}
				if (isStopped) break;


				KeyValue.Set("MainMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End of cycle. [MainMain.MainMainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}


		public void SettlementSystsemProcess()
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			if (!KeyValue.Get("SettlementSystsemBizDate", "", dbCnStr).Equals(Master.BizDate))
			{
				Log.Write("Internal Settlement System runnning for : " + Master.BizDate + " [MainMain.SettlementSystsemProcess]", 1);

				try
				{
					SqlCommand dbCmd = new SqlCommand("spSettlementSystemProcess", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;

					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDatePrior;

					dbCn.Open();
					dbCmd.ExecuteNonQuery();

					KeyValue.Set("SettlementSystsemBizDate", Master.BizDate, dbCnStr);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MainMain.SettlementSystsemProcess]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}

				Log.Write("Internal Settlement System completed for : " + Master.BizDate + " [MainMain.SettlementSystsemProcess]", 1);
			}
			else
			{
				Log.Write("Internal Settlement System already completed for : " + Master.BizDate + " [MainMain.SettlementSystsemProcess]", 1);
			}
		}

		public void BookGroupBizDateRoll()
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			if (!KeyValue.Get("BizDateBookGroupRoll", "", dbCnStr).Equals(Master.BizDate))
			{
				Log.Write("BizDate bookgroup roll runnning for : " + Master.BizDate + " [MainMain.BookGroupBizDateRoll]", 1);

				try
				{
					SqlCommand dbCmd = new SqlCommand("spBookGroupRoll", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;

					SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
					paramBizDatePrior.Value = Master.BizDatePrior;

					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDate;

					dbCn.Open();
					dbCmd.ExecuteNonQuery();

					KeyValue.Set("BizDateBookGroupRoll", Master.BizDate, dbCnStr);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MainMain.BookGroupBizDateRoll]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}

				Log.Write("BizDate bookgroup roll completed for : " + Master.BizDate + " [MainMain.BookGroupBizDateRoll]", 1);
			}
			else
			{
				Log.Write("BizDate bookgroup roll already completed for : " + Master.BizDate + " [MainMain.BookGroupBizDateRoll]", 1);
			}
		}


		private void ContractBizDateSystemRoll()
		{
			Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "2001-01-01", dbCn);

			if (Master.ContractsBizDate.Equals(Master.BizDate))
			{
				Log.Write("Contracts have already been rolled to " + Master.BizDate + ". [MainMain.ContractBizDateSystemRoll]", 3);
				return;
			}

			SqlCommand sqlCommand;

			try
			{
				sqlCommand = new SqlCommand("spContractBizDateSystemRoll", dbCn);
				sqlCommand.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
				paramBizDatePrior.Value = Master.BizDatePrior;

				SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;

				SqlParameter paramRecordCount = sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int);
				paramRecordCount.Direction = ParameterDirection.Output;

				Log.Write("Rolling contract records from " + Master.BizDatePrior + " to " + Master.BizDate + ". [MainMain.ContractBizDateSystemRoll]", 2);

				dbCn.Open();
				sqlCommand.ExecuteNonQuery();
				dbCn.Close();

				Master.ContractsBizDate = Master.BizDate;
				KeyValue.Set("ContractsBizDate", Master.ContractsBizDate, dbCn);

				int n = (int)paramRecordCount.Value;
				Log.Write("Rolled " + n.ToString("#,##0") + " contract records. [MainMain.ContractBizDateSystemRoll]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MainMain.ContractBizDateSystemRoll]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		private void ContractBizDateProcessRoll()
		{
			Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "2001-01-01", dbCn);

			if (Master.ContractsBizDate.Equals(Master.BizDate))
			{
				Log.Write("Contracts have already been rolled to " + Master.BizDate + ". [MainMain.ContractsBizDateProcessRoll]", 3);
				return;
			}

			if (Standard.ConfigValueExists("BookGroupList"))
			{
				string[] clientIdList = Standard.ConfigValue("BookGroupList").Split(';');

				foreach (string clientId in clientIdList)
				{
					if (clientId.Length.Equals(4) && !isStopped)
					{
						if (!KeyValue.Get("ProcessPositionBizDate" + clientId, "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
						{
							Log.Write("Waiting on Process position data for " + clientId + " for " + Master.BizDatePriorBank + ". [MainMain.ContractsBizDateProcessRoll]", 2);
							return;
						}
						else
						{
							Log.Write("Process position data is current for " + clientId + " for " + Master.BizDatePriorBank + ". [MainMain.ContractsBizDateProcessRoll]", 3);
						}
					}
				}

				SqlCommand sqlCommand;

				try
				{
					sqlCommand = new SqlCommand("spContractBizDateProcessRoll", dbCn);
					sqlCommand.CommandType = CommandType.StoredProcedure;

					SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
					paramBizDatePrior.Value = Master.BizDatePrior;

					SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDate;

					SqlParameter paramRecordCount = sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int);
					paramRecordCount.Direction = ParameterDirection.Output;

					Log.Write("Rolling contract records from " + Master.BizDatePrior + " to " + Master.BizDate + ". [MainMain.ContractsBizDateProcessRoll]", 2);

					dbCn.Open();
					sqlCommand.ExecuteNonQuery();
					dbCn.Close();

					Master.ContractsBizDate = Master.BizDate;
					KeyValue.Set("ContractsBizDate", Master.ContractsBizDate, dbCn);

					int n = (int)paramRecordCount.Value;
					Log.Write("Rolled " + n.ToString("#,##0") + " contract records. [MainMain.ContractsBizDateProcessRoll]", 2);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MainMain.ContractsBizDateRoll]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
			}
			else
			{
				Log.Write("There is no configuration value for BookGroupList. [MainMain.ContractsBizDateProcessRoll]", Log.Warning, 1);
			}
		}

		public void FundingRatesBizDateRoll()
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			if (!KeyValue.Get("BizDateFundingRatesRoll", "", dbCnStr).Equals(Master.BizDate))
			{
				Log.Write("BizDate funding rates roll runnning for : " + Master.BizDate + " [MainMain.FundingRatesBizDateRoll]", 1);

				try
				{
					SqlCommand dbCmd = new SqlCommand("spFundingRatesRoll", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;

					SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
					paramBizDatePrior.Value = Master.BizDatePrior;

					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDate;

					dbCn.Open();
					dbCmd.ExecuteNonQuery();

					KeyValue.Set("BizDateFundingRatesRoll", Master.BizDate, dbCnStr);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MainMain.FundingRatesBizDateRoll]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}

				Log.Write("BizDate funding rates roll completed for : " + Master.BizDate + " [MainMain.FundingRatesBizDateRoll]", 1);
			}
			else
			{
				Log.Write("BizDate funding rates roll already completed for : " + Master.BizDate + " [MainMain.FundingRatesBizDateRoll]", 1);
			}
		}

		private TimeSpan RecycleInterval()
		{
			string recycleInterval;
			string[] values;

			int hours;
			int minutes;

			bool isBizDay = Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn);
			TimeSpan timeSpan;

			char[] delimiter = new char[1];
			delimiter[0] = ':';

			if (isBizDay)
			{
				recycleInterval = KeyValue.Get("MainMainLoopRecycleIntervalBizDay", "0:15", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("MainMainLoopRecycleIntervalNonBizDay", "4:00", dbCn);
			}

			try
			{
				values = recycleInterval.Split(delimiter, 2);
				hours = int.Parse(values[0]);
				minutes = int.Parse(values[1]);
				timeSpan = new TimeSpan(hours, minutes, 0);
			}
			catch
			{
				if (isBizDay)
				{
					KeyValue.Set("MainMainRecycleIntervalBizDay", "0:15", dbCn);
					hours = 0;
					minutes = 30;
					timeSpan = new TimeSpan(hours, minutes, 0);
					Log.Write("MainMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [MainMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("MainMainRecycleIntervalNonBizDay", "4:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan(hours, minutes, 0);
					Log.Write("MainMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [MainMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("MainMain will recycle in " + hours + " hours, " + minutes + " minutes. [MainMain.RecycleInterval]", 2);
			return timeSpan;
		}

		private void BizDatesSet(Standard.HolidayType holidayType)
		{
			double utcOffset;
			
			try
			{
				utcOffset = double.Parse(KeyValue.Get("BizDateRollUtcOffsetMinutes", "0", dbCn));
			}
			catch
			{
				Log.Write("Unable to parse BizDateRollUtcOffsetMinutes key value. [MainMain.BizDatesSet]", Log.Error, 2);
				return;
			}

			DateTime bizDate;
			DateTime bizDateNext;
			DateTime bizDatePrior;

			bizDate = DateTime.UtcNow.AddMinutes(utcOffset).Date;
			while (!Standard.IsBizDate(bizDate, countryCode, holidayType, dbCn))
			{
				bizDate = bizDate.AddDays(1.0);
			}

			bizDateNext = bizDate.AddDays(1.0);
			while (!Standard.IsBizDate(bizDateNext, countryCode, holidayType, dbCn))
			{
				bizDateNext = bizDateNext.AddDays(1.0);
			}

			bizDatePrior = bizDate.AddDays(-1.0);
			while (!Standard.IsBizDate(bizDatePrior, countryCode, holidayType, dbCn))
			{
				bizDatePrior = bizDatePrior.AddDays(-1.0);
			}

			switch (holidayType)
			{
				case Standard.HolidayType.Any:
					Master.BizDate = bizDate.ToString(Standard.DateFormat);
					Master.BizDateNext = bizDateNext.ToString(Standard.DateFormat);
					Master.BizDatePrior = bizDatePrior.ToString(Standard.DateFormat);

					if (!KeyValue.Get("BizDate", "2001-01-01", dbCn).Equals(Master.BizDate))
					{
						KeyValue.Set("BizDate", Master.BizDate, dbCn);
						Log.Write("BizDate has been set to: " + Master.BizDate + " [MainMain.BizDatesSet]", Log.Information, 2);

						KeyValue.Set("ContractsBizDate", "2001-01-01", dbCn);
						Log.Write("ContractsBizDate has been set to: 2001-01-01 [MainMain.BizDatesSet]", Log.Information, 2);
					}

					if (!KeyValue.Get("BizDateNext", "2001-01-01", dbCn).Equals(Master.BizDateNext))
					{
						KeyValue.Set("BizDateNext", Master.BizDateNext, dbCn);
						Log.Write("BizDateNext has been set to: " + Master.BizDateNext + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					if (!KeyValue.Get("BizDatePrior", "2001-01-01", dbCn).Equals(Master.BizDatePrior))
					{
						KeyValue.Set("BizDatePrior", Master.BizDatePrior, dbCn);
						Log.Write("BizDatePrior has been set to: " + Master.BizDatePrior + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					break;
				case Standard.HolidayType.Bank:
					Master.BizDateBank = bizDate.ToString(Standard.DateFormat);
					Master.BizDateNextBank = bizDateNext.ToString(Standard.DateFormat);
					Master.BizDatePriorBank = bizDatePrior.ToString(Standard.DateFormat);

					if (!KeyValue.Get("BizDateBank", "2001-01-01", dbCn).Equals(Master.BizDateBank))
					{
						KeyValue.Set("BizDateBank", Master.BizDateBank, dbCn);
						Log.Write("BizDateBank has been set to: " + Master.BizDateBank + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					if (!KeyValue.Get("BizDateNextBank", "2001-01-01", dbCn).Equals(Master.BizDateNextBank))
					{
						KeyValue.Set("BizDateNextBank", Master.BizDateNextBank, dbCn);
						Log.Write("BizDateNextBank has been set to: " + Master.BizDateNextBank + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					if (!KeyValue.Get("BizDatePriorBank", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
					{
						KeyValue.Set("BizDatePriorBank", Master.BizDatePriorBank, dbCn);
						Log.Write("BizDatePriorBank has been set to: " + Master.BizDatePriorBank + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					break;
				case Standard.HolidayType.Exchange:
					Master.BizDateExchange = bizDate.ToString(Standard.DateFormat);
					Master.BizDateNextExchange = bizDateNext.ToString(Standard.DateFormat);
					Master.BizDatePriorExchange = bizDatePrior.ToString(Standard.DateFormat);

					if (!KeyValue.Get("BizDateExchange", "2001-01-01", dbCn).Equals(Master.BizDateExchange))
					{
						KeyValue.Set("BizDateExchange", Master.BizDateExchange, dbCn);
						Log.Write("BizDateExchange has been set to: " + Master.BizDateExchange + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					if (!KeyValue.Get("BizDateNextExchange", "2001-01-01", dbCn).Equals(Master.BizDateNextExchange))
					{
						KeyValue.Set("BizDateNextExchange", Master.BizDateNextExchange, dbCn);
						Log.Write("BizDateNextExchange has been set to: " + Master.BizDateNextExchange + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					if (!KeyValue.Get("BizDatePriorExchange", "2001-01-01", dbCn).Equals(Master.BizDatePriorExchange))
					{
						KeyValue.Set("BizDatePriorExchange", Master.BizDatePriorExchange, dbCn);
						Log.Write("BizDatePriorExchange has been set to: " + Master.BizDatePriorExchange + " [MainMain.BizDatesSet]", Log.Information, 2);
					}

					break;
			}
		}


		public static string TempPath
		{
			get
			{
				return tempPath;
			}
		}

		public static bool IsStopped
		{
			get
			{
				return isStopped;
			}
		}
	}
}

