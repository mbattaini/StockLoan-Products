// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;
using Anetics.Documents;

namespace Anetics.Rebate
{
	public class RebateMain
	{
    private string dbCnStr;
    private SqlConnection dbCn = null;

		private static string countryCode = "";

    private Thread workerThread = null;
    private static bool isStopped = true;
    private static string tempPath;
    
    public RebateMain(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;

      try
      {
        dbCn = new SqlConnection(dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [RebateMain.RebateMain]", Log.Error, 1);
      }

      countryCode = Standard.ConfigValue("CountryCode", "US");

			if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [RebateMain.RebateMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [RebateMain.RebateMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [RebateMain.RebateMain]", 2);
    }

    ~RebateMain()
    {
      if (dbCn != null)
      {
        dbCn.Dispose();
      }
    }

    public void Start()
    {
      isStopped = false;

      if ((workerThread == null)||(!workerThread.IsAlive)) // Must create new thread.
      {
        workerThread = new Thread(new ThreadStart(RebateMainLoop));
        workerThread.Name = "Worker";
        workerThread.Start();

        Log.Write("Start command issued with new worker thread. [RebateMain.Start]", 2);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with worker thread already running. [RebateMain.Start]", 2);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (workerThread == null)
      {
        Log.Write("Stop command issued, worker thread never started. [RebateMain.Stop]", 2);
      }
      else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        workerThread.Abort();
        Log.Write("Stop command issued, sleeping worker thread aborted. [RebateMain.Stop]", 2);
      }
      else
      {
        Log.Write("Stop command issued, worker thread is still active. [RebateMain.Stop]", 2);
      }
    }

    private void RebateMainLoop()
    {
      while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
      {
        Log.Write("Start-of-cycle. [RebateMain.RebateMainLoop]", 2);
        KeyValue.Set("RebateMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
        Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
        Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
        
				ShortSaleNegativeRebateBillingSnapShot();
				if (isStopped) break;
				
				MailSendShortSaleAccountsCovered();
				if (isStopped) break;	
				
				ShortSalePositiveRebateBillingSnapShot();
				if (isStopped) break;									

        KeyValue.Set("RebateMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        Log.Write("End-of-cycle. [RebateMain.RebateMainLoop]", 2);

        if (!isStopped)
        {
          Thread.Sleep(RecycleInterval());
        }
      }
    }

		private void MailSendShortSaleAccountsCovered()
		{
			
			string mailContent = "";
			string snapShotDate = KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCn);

			if (!snapShotDate.Equals(Master.ContractsBizDate))
			{
				Log.Write("Short Sale Billing Snapshot has not run for today yet. [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				return;
			}
			else if (KeyValue.Get("ShortSaleAccountsCoveredMailFrom", "stockloan@penson.com", dbCn).Equals("")) // Do not send mail.
			{
				Log.Write("There is no key value for mail sender (ShortSaleAccountsCoveredMailFrom). [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				return;
			}
			else if (KeyValue.Get("ShortSaleAccountsCoveredMailDate", "2001-01-01", dbCn).Equals(Master.BizDate))
			{
				Log.Write("Short sale accounts charged  notice has already been sent for " + Master.BizDate + ". [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
			}		
			else  
			{
				Log.Write("Will do the short sale accounts charged notice for " + Master.BizDatePrior + ". [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				try
				{
					ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, Master.ContractsBizDate, Master.ContractsBizDate, "");
					mailContent = shortSaleBill.AccountsBorrowedEmailBill();
				}
				catch (Exception e)
				{
					KeyValue.Set("ShortSaleAccountsCoveredMailStatus", e.Message, dbCn);
					Log.Write(e.Message + " [MedalistMain.MailSendShortSaleAccountsCovered]", Log.Error, 1);
					return;
				}
				

				try
				{
					Email email = new Email(dbCnStr);

					email.Send(
						KeyValue.Get("ShortSaleAccountsCoveredMailTo", "mbattaini@penson.com", dbCn),
						KeyValue.Get("ShortSaleAccountsCoveredMailFrom", "stockloan@penson.com", dbCn),
						"Covered Shorts Do Not Buyin! For " + Tools.FormatDate(Master.ContractsBizDate, "dddd, d MMMM yyyy"),
						mailContent);

					KeyValue.Set("ShortSaleAccountsCoveredMailStatus", "OK", dbCn);
					KeyValue.Set("ShortSaleAccountsCoveredMailDate", Master.BizDate, dbCn);
        
					Log.Write("Short Sale Accouts Covered e-mail notice has been sent for " + Master.BizDate + ". [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				}
				catch (Exception e)
				{
					KeyValue.Set("ShortSaleAccountsCoveredMailStatus", e.Message, dbCn);
					Log.Write(e.Message + " [MedalistMain.MailSendShortSaleAccountsCharged]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
			}
		}

		private void ShortSaleNegativeRebateBillingSnapShot()
		{
			string listMakeTime;

			if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn))
			{
				if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCn).Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
				{
					Log.Write("Short Sale Negative Rebate Billing Weekend Snapshot is current for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else // do weekend snapshot
				{
					SqlCommand sqlCommand;
      
					Log.Write("Doing Short Sale Negative Rebate Billing Weekend Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);

					try
					{
						sqlCommand = new SqlCommand("spShortSaleBillingSummaryOvernightWeekendSnapShot", dbCn);
						sqlCommand.CommandType = CommandType.StoredProcedure;

						SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
						paramBizDatePrior.Value = Master.BizDatePrior;

						SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = DateTime.UtcNow.ToString(Standard.DateFormat);			
          
						dbCn.Open();
						sqlCommand.ExecuteNonQuery();
						dbCn.Close();								

						KeyValue.Set("ShortSaleNegRebateBillingSnapShotBizDate", DateTime.UtcNow.ToString(Standard.DateFormat), dbCn);
						Log.Write("ShortSale Negative Rebate Billing Weekend Snap Shot done for:  " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
					}
					catch (Exception e)
					{
						Log.Write(e.Message + " [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", Log.Error, 1);
					}
					finally
					{
						if (dbCn.State != ConnectionState.Closed)
						{
							dbCn.Close();
						}
					}
				}
			}
			else
			{			
				if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCn).Equals(Master.BizDate))
				{
					Log.Write("Short Sale Negative Rebate Billing Snapshot is current for " + Master.ContractsBizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else if (!Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					Log.Write("Contracts data has not been loaded for " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else if (!KeyValue.Get("PensonSecurityStaticBizDate", "", dbCn).Equals(Master.BizDatePrior))
				{
					Log.Write("Penson security static data has not loaded yet. [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);       
				}
				else if (!DateTime.Parse(KeyValue.Get("BoxPositionLoadDateTime", "", dbCn)).ToString(Standard.DateFormat).Equals(Master.ContractsBizDate))
				{
					Log.Write("Penson box position data has not been loaded yet. [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else 
				{
					if (Master.ContractsBizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) 
					{
						listMakeTime = KeyValue.Get("ShortSaleNegRebateBillingSnapShotTime", "23:00", dbCn);

						if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
						{
							Log.Write("Doing Short Sale Negative Rebate Billing  Buisness Day Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);

							SqlCommand sqlDbCmd = new SqlCommand("spShortSaleBillingSummaryOvernightSnapShotControl", dbCn);
							sqlDbCmd.CommandType = CommandType.StoredProcedure;
							sqlDbCmd.CommandTimeout = int.Parse(KeyValue.Get("ShortSaleNegRebateBillingSnapShotTimeOut", "300", dbCn));

							try 
							{
								dbCn.Open();
								sqlDbCmd.ExecuteNonQuery();
								dbCn.Close();
              
								KeyValue.Set("ShortSaleNegRebateBillingSnapShotBizDate", Master.BizDate, dbCn);
								KeyValue.Set("ShortSaleNegRebateBillingSnapShotDateTime", DateTime.Now.ToString(Standard.DateTimeFormat), dbCn);
								Log.Write("ShortSale Negative Rebate Billing Snap Shot done for:  " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
							}
							catch(Exception e) 
							{
								Log.Write(e.Message + " [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", Log.Error, 1);            
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
							Log.Write("Must wait until after " + listMakeTime + " UTC to do snapshopt for " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
						}
					}
					else
					{
						Log.Write("Must wait until trade date, " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
					}
				}
			}
		}

		private void ShortSalePositiveRebateBillingSnapShot()
		{
			string listMakeTime;

			if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn))
			{
				if (KeyValue.Get("ShortSalePosRebateBillingSnapShotBizDate", "", dbCn).Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
				{
					Log.Write("Short Sale Rebate Billing Weekend Snapshot is current for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else // do weekend snapshot
				{
					SqlCommand sqlCommand;
      
					Log.Write("Doing Short Sale Positive Rebate Billing Weekend Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);

					try
					{
						sqlCommand = new SqlCommand("spShortSaleBillingPositiveRebatesSummaryOvernightWeekendSnapShot", dbCn);
						sqlCommand.CommandType = CommandType.StoredProcedure;

						SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
						paramBizDatePrior.Value = Master.BizDatePrior;

						SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = DateTime.UtcNow.ToString(Standard.DateFormat);			
          
						dbCn.Open();
						sqlCommand.ExecuteNonQuery();
						dbCn.Close();								

						KeyValue.Set("ShortSalePosRebateBillingSnapShotBizDate", DateTime.UtcNow.ToString(Standard.DateFormat), dbCn);
						Log.Write("ShortSale Positive Rebate Billing Weekend Snap Shot done for:  " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
					}
					catch (Exception e)
					{
						Log.Write(e.Message + " [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", Log.Error, 1);
					}
					finally
					{
						if (dbCn.State != ConnectionState.Closed)
						{
							dbCn.Close();
						}
					}
				}
			}
			else //if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Bank, dbCn))
			{			
				if (KeyValue.Get("ShortSalePosRebateBillingSnapShotBizDate", "", dbCn).Equals(Master.ContractsBizDate))
				{
					Log.Write("Short Sale Positive Rebate Billing Snapshot is current for " + Master.ContractsBizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else if (!Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					Log.Write("Contracts data has not been loaded for " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else if (!KeyValue.Get("PensonSecurityStaticBizDate", "", dbCn).Equals(Master.BizDatePrior))
				{
					Log.Write("Penson security static data has not loaded yet. [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);       
				}
				else if (!DateTime.Parse(KeyValue.Get("BoxPositionLoadDateTime", "", dbCn)).ToString(Standard.DateFormat).Equals(Master.ContractsBizDate))
				{
					Log.Write("Penson box position data has not been loaded yet. [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else 
				{
					if (Master.ContractsBizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) 
					{
						listMakeTime = KeyValue.Get("ShortSalePosRebateBillingSnapShotTime", "23:00", dbCn);

						if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
						{
							Log.Write("Doing Short Sale Rebate Billing  Buisness Day Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
							try 
							{
								
								Log.Write("Doing SnapShot Control.", 1);
								SqlCommand sqlDbCmd = new SqlCommand("spShortSaleBillingPositiveRebatesSummaryOvernightSnapShotControl", dbCn);
								sqlDbCmd.CommandType = CommandType.StoredProcedure;
								sqlDbCmd.CommandTimeout = int.Parse(KeyValue.Get("ShortSalePosRebateBillingSnapShotTimeOut", "300", dbCn));
							
								dbCn.Open();
								sqlDbCmd.ExecuteNonQuery();
								
								Log.Write("Doing Borrow Allocation.", 1);
								sqlDbCmd.CommandText = "spShortSaleBillingPositiveRebatesSummaryOvernightBorrowAllocationSet";
								sqlDbCmd.CommandType = CommandType.StoredProcedure;
								SqlParameter paramBizDate =  sqlDbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
								paramBizDate.Value = Master.ContractsBizDate; 
								
								SqlParameter paramBookGroup =  sqlDbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
								paramBookGroup.Value = "0234";
								
								sqlDbCmd.ExecuteNonQuery();

								Log.Write("Doing Load Remaining Shorts.", 1);
								sqlDbCmd.CommandText = "spShortSaleBillingPositiveRebatesSummaryOvernightLoadRemainingShortsSet";
								sqlDbCmd.CommandType = CommandType.StoredProcedure;
								sqlDbCmd.ExecuteNonQuery();
							
								Log.Write("Doing Rate Set.", 1);
								sqlDbCmd.CommandText = "spShortSaleBillingPositiveRebatesSummaryOvernightRateSet";								
								sqlDbCmd.CommandType = CommandType.StoredProcedure;
								SqlParameter paramContractType =  sqlDbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
								paramContractType.Value = "B";								
								sqlDbCmd.ExecuteNonQuery();						
								
								Log.Write("Doing Price Set.", 1);
								sqlDbCmd.CommandText = "spShortSaleBillingPositiveRebatesSummaryOvernightPriceSet";																
								sqlDbCmd.CommandType = CommandType.StoredProcedure;
								sqlDbCmd.Parameters.Remove(paramContractType);								
								sqlDbCmd.ExecuteNonQuery();						
								
								Log.Write("Doing Original Charge Set.", 1);
								sqlDbCmd.CommandText = "spShortSaleBillingPositiveRebatesSummaryOvernightOriginalChargeSet";																
								sqlDbCmd.CommandType = CommandType.StoredProcedure;
								sqlDbCmd.Parameters.Remove(paramBookGroup);																
								sqlDbCmd.ExecuteNonQuery();		

								Log.Write("Doing Modified Charge Set.", 1);
								sqlDbCmd.CommandText = "spShortSaleBillingPositiveRebatesSummaryOvernightModifiedChargeSet";																
								sqlDbCmd.CommandType = CommandType.StoredProcedure;
								sqlDbCmd.Parameters.Remove(paramBizDate);
								
								SqlParameter paramStartDate = sqlDbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
								paramStartDate.Value = Master.ContractsBizDate;

								SqlParameter paramStopDate = sqlDbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
								paramStopDate.Value = Master.ContractsBizDate;
								
								SqlParameter paramActUserId = sqlDbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
								paramActUserId.Value = "ADMIN";
																
								SqlParameter paramRecordsUpdated = sqlDbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
								paramRecordsUpdated.Direction = ParameterDirection.Output;

								sqlDbCmd.ExecuteNonQuery();																
								dbCn.Close();

								KeyValue.Set("ShortSalePosRebateBillingSnapShotBizDate", Master.BizDate, dbCn);
								KeyValue.Set("ShortSalePosRebateBillingSnapShotDateTime", DateTime.Now.ToString(Standard.DateTimeFormat), dbCn);
								Log.Write("ShortSale Positive Rebate Billing Snap Shot done for:  " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
							}
							catch(Exception e) 
							{
								Log.Write(e.Message + " [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", Log.Error, 1);            
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
							Log.Write("Must wait until after " + listMakeTime + " UTC to do snapshopt for " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
						}
					}
					else
					{
						Log.Write("Must wait until trade date, " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
					}
				}
			}
		}

    private TimeSpan RecycleInterval()
    {
      string recycleInterval;
      string [] values;

      int hours;
      int minutes;

      bool isBizDay = (DateTime.UtcNow.DayOfWeek != DayOfWeek.Saturday) && (DateTime.UtcNow.DayOfWeek != DayOfWeek.Sunday);
      TimeSpan timeSpan;

      char [] delimiter = new char[1];
      delimiter[0] = ':';

      if (isBizDay)
      {
        recycleInterval = KeyValue.Get("RebateMainRecycleIntervalBizDay", "0:20", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("RebateMainRecycleIntervalNonBizDay", "6:00", dbCn);
      }

      try
      {
        values = recycleInterval.Split(delimiter, 2);
        hours = int.Parse(values[0]);
        minutes = int.Parse(values[1]);
        timeSpan = new TimeSpan (hours, minutes, 0);
      }
      catch
      {
        if (isBizDay)
        {
          KeyValue.Set("RebateMainRecycleIntervalBizDay", "0:20", dbCn);
          hours = 0;
          minutes = 20;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [RebateMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("RebateMainRecycleIntervalNonBizDay", "6:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [RebateMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("RebateMain will recycle in " + hours + " hours, " + minutes + " minutes. [RebateMain.RecycleInterval]", 2);
      return timeSpan;
    }

    public static string DatePartSet(string inputString, string dateValue)
    {
      int leftBracketIndex = inputString.IndexOf('{');
      int rightBracketIndex = inputString.IndexOf('}');
            
      if ((leftBracketIndex > -1) && (rightBracketIndex > leftBracketIndex))
      {
        string formatString = inputString.Substring(leftBracketIndex + 1, rightBracketIndex - leftBracketIndex - 1);
 
        return inputString.Substring(0, leftBracketIndex) + Tools.FormatDate(dateValue, formatString) + 
          inputString.Substring(rightBracketIndex + 1, inputString.Length - rightBracketIndex - 1);
      }
      else
      {
        return inputString;
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
