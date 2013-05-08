// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using Anetics.Common;

namespace Anetics.Medalist
{
    public class MedalistMain
    {
        private string countryCode;

        private string dbCnStr;
        private SqlConnection dbCn = null;

        private Thread mainThread = null;

        private static bool isStopped = true;
        private static bool isDrone = false;
        private static string tempPath;

        private PositionAgent positionAgent;
        private SubstitutionAgent substitutionAgent;

        public MedalistMain(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;

            try
            {
                dbCn = new SqlConnection(dbCnStr);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MedalistMain.MedalistMain]", Log.Error, 1);
            }

            countryCode = Standard.ConfigValue("CountryCode", "US");
            Log.Write("Using country code: " + countryCode + " [MedalistMain.MedalistMain]", 2);

            if (Standard.ConfigValueExists("TempPath"))
            {
                tempPath = Standard.ConfigValue("TempPath");

                if (!Directory.Exists(tempPath))
                {
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [MedalistMain.MedalistMain]", Log.Error, 1);
                    tempPath = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                Log.Write("A configuration value for TempPath has not been provided. [MedalistMain.MedalistMain]", Log.Information, 1);
                tempPath = Directory.GetCurrentDirectory();
            }

            Log.Write("Temporary files will be staged at " + tempPath + ". [MedalistMain.MedalistMain]", 2);
        }

        ~MedalistMain()
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
                mainThread = new Thread(new ThreadStart(MedalistMainLoop));
                mainThread.Name = "Main";
                mainThread.Start();

                Log.Write("Start command issued with new main thread. [MedalistMain.Start]", 3);
            }
            else // Old thread will be just fine.
            {
                Log.Write("Start command issued with main thread already running. [MedalistMain.Start]", 3);
            }
        }

        public void Stop()
        {
            isStopped = true;

            if (mainThread == null)
            {
                Log.Write("Stop command issued, main thread never started. [MedalistMain.Stop]", 3);
            }
            else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                mainThread.Abort();
                Log.Write("Stop command issued, sleeping main thread aborted. [MedalistMain.Stop]", 3);
            }
            else
            {
                Log.Write("Stop command issued, main thread is still active. [MedalistMain.Stop]", 3);
            }
        }

        private void MedalistMainLoop()
        {
            while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
            {
                Log.Write("Start of cycle. [MedalistMain.MedalistMainLoop]", 2);
                KeyValue.Set("MedalistMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

                isDrone = bool.Parse(Standard.ConfigValue("IsServerDrone", "false"));
                                
                BizDatesSet(Standard.HolidayType.Any);
                BizDatesSet(Standard.HolidayType.Bank);
                BizDatesSet(Standard.HolidayType.Exchange);

                if (!isDrone)
                {
                    ContractBizDateRoll();
                    if (isStopped) break;

                    RecallBizDateSet();
                    if (isStopped) break;

                    BankLoanBizDateRoll();
                    if (isStopped) break;

                    EasyBorrowListMake();
                    if (isStopped) break;

                    EasyBorrowFileDo();
                    if (isStopped) break;

                    InventoryFundingRatesRoll();
                    if (isStopped) break;

                    MailSendEasy();
                    if (isStopped) break;

                    MailSendThreshold();
                    if (isStopped) break;

                    ShortSaleDailyQuantitiesPurge();
                    if (isStopped) break;
                }

                KeyValue.Set("MedalistMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
                Log.Write("End of cycle. [MedalistMain.MedalistMainLoop]", 2);

                if (!isStopped)
                {
                    Thread.Sleep(RecycleInterval());
                }
            }
        }

        private void ContractBizDateRoll()
        {
            Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "2001-01-01", dbCn);

            if (Master.ContractsBizDate.Equals(Master.BizDate))
            {
                Log.Write("Contracts have already been rolled to " + Master.BizDate + ". [MedalistMain.ContractBizDateRoll]", 3);
                return;
            }

            if (Standard.ConfigValueExists("BookGroupList"))
            {
                string[] clientIdList = Standard.ConfigValue("BookGroupList").Split(';');

                foreach (string clientId in clientIdList)
                {
                    if (clientId.Length.Equals(4) && !isStopped)
                    {
                        if (!KeyValue.Get("LoanetPositionBizDate" + clientId, "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
                        {
                            Log.Write("Waiting on Loanet position data for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.ContractBizDateRoll]", 2);
                            return;
                        }
                        else
                        {
                            Log.Write("Loanet position data is current for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.ContractBizDateRoll]", 3);
                        }
                    }
                }

                SqlCommand sqlCommand;

                try
                {
                    sqlCommand = new SqlCommand("spContractBizDateRoll", dbCn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                    paramBizDatePrior.Value = Master.BizDatePrior;

                    SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = Master.BizDate;

                    SqlParameter paramDayCount = sqlCommand.Parameters.Add("@DayCount", SqlDbType.SmallInt);
                    paramDayCount.Value = DateTime.Compare(DateTime.Parse(Master.BizDateNext), DateTime.Parse(Master.BizDate));

                    SqlParameter paramRecordCount = sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int);
                    paramRecordCount.Direction = ParameterDirection.Output;

                    Log.Write("Rolling contract records from " + Master.BizDatePrior + " to " + Master.BizDate + ". [MedalistMain.ContractsBizDateRoll]", 2);

                    dbCn.Open();
                    sqlCommand.ExecuteNonQuery();
                    dbCn.Close();

                    Master.ContractsBizDate = Master.BizDate;
                    KeyValue.Set("ContractsBizDate", Master.ContractsBizDate, dbCn);

                    int n = (int)paramRecordCount.Value;
                    Log.Write("Rolled " + n.ToString("#,##0") + " contract records. [Loanet.ContractBizDateRoll]", 2);
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [MedalistMain.ContractsBizDateRoll]", Log.Error, 1);
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
                Log.Write("There is no configuration value for BookGroupList. [MedalistMain.ContractsBizDateRoll]", Log.Warning, 1);
            }
        }
     
        public void BankLoanBizDateRoll()
        {
            if (KeyValue.Get("BankLoanRollBizDate", "0000-01-01", dbCn).Equals(Master.BizDateBank))
            {
                Log.Write("BankLoan BizDate Roll has already run for " + Master.BizDateBank + ". [MedalistMain.BankLoanBizDateRoll]", 2);
                return;
            }
            else
            {
                SqlCommand dbCmd = null;

                try
                {
                    dbCn = new SqlConnection(dbCnStr);
                    dbCmd = new SqlCommand("spBankLoanBizDateRoll", dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
                    paramRecordsUpdated.Direction = ParameterDirection.Output;
                    paramRecordsUpdated.Value = 0;

                    dbCn.Open();
                    dbCmd.ExecuteNonQuery();
                    dbCn.Close();

                    Log.Write("BankLoan BizDate Roll updated: " + long.Parse(paramRecordsUpdated.Value.ToString()).ToString("#,##0") + " older then " + Master.BizDateBank + ". [MedalistMain.BankLoanBizDateRoll]", 2);
                    KeyValue.Set("BankLoanRollBizDate", Master.BizDateBank, dbCn);
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [PositionAgent.BankLoanBizDateRoll]", Log.Error, 1);
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

        private void RecallBizDateSet()
        {
            string RecallBizDate = KeyValue.Get("RecallBizDate", "2001-01-01", dbCn);

            if (RecallBizDate.Equals(Master.BizDate))
            {
                Log.Write("Recalls have already been set for " + Master.BizDate + ". [MedalistMain.RecallBizDateSet]", 3);
                return;
            }

            if (Standard.ConfigValueExists("BookGroupList"))
            {
                string[] clientIdList = Standard.ConfigValue("BookGroupList").Split(';');

                foreach (string clientId in clientIdList)
                {
                    if (clientId.Length.Equals(4) && !isStopped)
                    {
                        if (!KeyValue.Get("LoanetRecallsBizDate" + clientId, "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
                        {
                            Log.Write("Waiting on Loanet recall data for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.RecallBizDateSet]", 2);
                            return;
                        }
                        else
                        {
                            Log.Write("Loanet recall data is current for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.RecallBizDateSet]", 3);
                        }
                    }
                }
            }

            SqlCommand sqlCommand;

            try
            {
                sqlCommand = new SqlCommand("spRecallBizDateSet", dbCn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter paramRecordCount = sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int);
                paramRecordCount.Direction = ParameterDirection.Output;

                Log.Write("Setting recall records for " + Master.BizDate + ". [MedalistMain.RecallBizDateSet]", 2);

                dbCn.Open();
                sqlCommand.ExecuteNonQuery();
                dbCn.Close();

                KeyValue.Set("RecallBizDate", Master.BizDate, dbCn);

                int n = (int)paramRecordCount.Value;
                Log.Write("Set " + n.ToString("#,##0") + " recall records. [MedalistMain.RecallBizDateSet]", 2);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + "  [MedalistMain.RecallBizDateSet]", Log.Error, 1);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        private void EasyBorrowListMake()
        {
            string listMakeTime;

            if (KeyValue.Get("EasyBorrowListDate", "", dbCn).Equals(Master.BizDateExchange))
            {
                Log.Write("Easy borrow list is current for " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
            }
            else
            {
                if (Master.BizDateExchange.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Is the day to do it.
                {
                    listMakeTime = KeyValue.Get("EasyBorrowListMakeTime", "11:00", dbCn);

                    if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // It is time to do it.
                    {
                        SqlCommand sqlDbCmd = new SqlCommand("spBorrowEasySet", dbCn);
                        sqlDbCmd.CommandType = CommandType.StoredProcedure;
                        sqlDbCmd.CommandTimeout = int.Parse(KeyValue.Get("EasyBorrowListMakeTimeout", "300", dbCn));

                        SqlParameter paramTradeDate = sqlDbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                        paramTradeDate.Value = Master.BizDateExchange;

                        try
                        {
                            dbCn.Open();
                            sqlDbCmd.ExecuteNonQuery();
                            dbCn.Close();

                            KeyValue.Set("EasyBorrowListDate", Master.BizDateExchange, dbCn);
                            Log.Write("Easy Borrow list has been set for " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
                        }
                        catch (Exception e)
                        {
                            Log.Write(e.Message + " [MedalistMain.EasyBorrowListMake]", Log.Error, 1);
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
                        Log.Write("Must wait until after " + listMakeTime + " UTC to do list for " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
                    }
                }
                else
                {
                    Log.Write("Must wait until trade date, " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
                }
            }
        }

        private void EasyBorrowFileDo()
        {
            int n = 0;
            string status = "OK";
            string easyBorrowListDate = KeyValue.Get("EasyBorrowListDate", "", dbCn);
            Anetics.Common.Filer filer;

            if (!KeyValue.Get("EasyBorrowFileDate", "", dbCn).Equals(easyBorrowListDate)) // Will need to do the file.
            {
                Log.Write("Will do the easy borrow file for " + easyBorrowListDate + ". [MedalistMain.EasyBorrowFileDo]", 2);

                StreamWriter streamWriter = null;
                SqlDataReader dataReader = null;

                SqlCommand dbCmd = new SqlCommand("spBorrowEasyList", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.VarChar, 25);
                paramTradeDate.Value = easyBorrowListDate;

                try
                {
                    dbCn.Open();
                    dataReader = dbCmd.ExecuteReader();

                    streamWriter = new StreamWriter(
                        TempPath + "easyBorrows.dat",
                        false,
                        System.Text.Encoding.ASCII);

                    streamWriter.WriteLine("H|EasyBorrowList|" + easyBorrowListDate + "|" +
                        KeyValue.Get("EasyBorrowFileSource", "PensonFinancial", dbCnStr));

                    while (dataReader.Read())
                    {
                        streamWriter.WriteLine("D|" +
                            dataReader.GetValue(0).ToString().Trim() + "|" +
                            dataReader.GetValue(1).ToString().Trim() + "|" +
                            dataReader.GetValue(2).ToString().Trim());

                        n += 1;
                    }

                    streamWriter.WriteLine("T|DataItemCount|" + n + "|EOF");

                    dataReader.Close();
                    dbCn.Close();

                    streamWriter.Flush();
                    streamWriter.Close();

                    filer = new Anetics.Common.Filer(TempPath);

                    filer.FilePut(
                        KeyValue.Get("EasyBorrowFilePathName", @"C:\Anetics\Logs\easyBorrowList.txt", dbCn),
                        KeyValue.Get("EasyBorrowFileHostName", "localHost", dbCn),
                        KeyValue.Get("EasyBorrowFileUserName", "anonymous", dbCn),
                        KeyValue.Get("EasyBorrowFilePassword", "medalist", dbCn),
                        TempPath + "easyBorrows.dat");

                    KeyValue.Set("EasyBorrowFileDate", easyBorrowListDate, dbCnStr);
                }
                catch (Exception e)
                {
                    status = e.Message;
                    Log.Write(e.Message + " [MedalistMain.EasyBorrowFileDo]", Log.Error, 1);
                }
                finally
                {
                    if ((dataReader != null) && (!dataReader.IsClosed))
                    {
                        dataReader.Close();
                    }

                    if (dbCn.State != ConnectionState.Closed)
                    {
                        dbCn.Close();
                    }
                }

                KeyValue.Set("EasyBorrowFileStatus", status, dbCn);
            }
            else
            {
                Log.Write("Easy borrow file is current for " + easyBorrowListDate + ". [MedalistMain.EasyBorrowFileDo]", 2);
            }
        }

        private void MailSendEasy()
        {
            SqlDataReader dataReader = null;
            int itemCount = 0;
            string easyBorrowFileDate = KeyValue.Get("EasyBorrowFileDate", "", dbCn);

            if (KeyValue.Get("EasyBorrowMailFrom", "support@anetics.com", dbCn).Equals("")) // Do not send mail.
            {
                Log.Write("There is no key value for mail sender (EasyBorrowMailFrom). [MedalistMain.MailSendEasy]", 2);
                return;
            }

            if (KeyValue.Get("EasyBorrowMailDate", "2001-01-01", dbCn).Equals(easyBorrowFileDate))
            {
                Log.Write("Easy borrow e-mail notice has already been sent for " + easyBorrowFileDate + ". [MedalistMain.MailSendEasy]", 2);
            }
            else
            {
                Log.Write("Will do the easy borrow e-mail notice for " + easyBorrowFileDate + ". [MedalistMain.MailSendEasy]", 2);

                SqlCommand sqlCmd = new SqlCommand("spBorrowEasyList", dbCn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 600;

                string mailContent = "SecId".PadRight(15, ' ') + "Symbol\n";
                mailContent += "-----".PadRight(15, ' ') + "------\n";

                try
                {
                    dbCn.Open();
                    dataReader = sqlCmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        mailContent += dataReader.GetValue(0).ToString().PadRight(15, ' ') +
                            dataReader.GetValue(1).ToString() + "\n";

                        ++itemCount;
                    }
                }
                catch (Exception e)
                {
                    KeyValue.Set("EasyBorrowMailStatus", e.Message, dbCn);
                    Log.Write(e.Message + " [MedalistMain.MailSendEasy]", Log.Error, 1);
                    return;
                }
                finally
                {
                    if ((dataReader != null) && (!dataReader.IsClosed))
                    {
                        dataReader.Close();
                    }

                    if (dbCn.State != ConnectionState.Closed)
                    {
                        dbCn.Close();
                    }
                }

                if (itemCount == 0)
                {
                    mailContent += "[None for today.]\n";
                }

                Log.Write("Listed " + itemCount + " easy borrow items as added in e-mail notification. [MedalistMain.MailSendEasy]", 2);

                try
                {
                    Email email = new Email(dbCnStr);

                    email.Send(
                        KeyValue.Get("EasyBorrowMailTo", "support@anetics.com", dbCn),
                        KeyValue.Get("EasyBorrowMailFrom", "support@anetics.com", dbCn),
                        "Easy Borrow List for " + Tools.FormatDate(Master.BizDateExchange, "dddd, d MMMM yyyy"),
                        mailContent);

                    KeyValue.Set("EasyBorrowMailStatus", "OK", dbCn);
                    KeyValue.Set("EasyBorrowMailDate", easyBorrowFileDate, dbCn);

                    Log.Write("Easy borrow e-mail notice has been sent for " + easyBorrowFileDate + ". [MedalistMain.MailSendEasy]", 2);
                }
                catch (Exception e)
                {
                    KeyValue.Set("EasyBorrowMailStatus", e.Message, dbCn);
                    Log.Write(e.Message + " [MedalistMain.MailSendEasy]", Log.Error, 1);
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

        private void MailSendThreshold()
        {
            if (KeyValue.Get("ThresholdMailFrom", "support@stockloan.net", dbCn).Equals("")) // Do not send mail.
            {
                Log.Write("There is no key value for mail sender (ThresholdMailFrom). [MedalistMain.MailSendThreshold]", 2);
                return;
            }

            if (KeyValue.Get("ThresholdMailDate", "2001-01-01", dbCn).Equals(Master.BizDateBank))
            {
                Log.Write("Threshold list e-mail notice has already been sent for " + Master.BizDateBank + ". [MedalistMain.MailSendThreshold]", 2);
                return;
            }

            if (!KeyValue.Get("ThresholdListBizDateAmse", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
            {
                Log.Write("Waiting for the threshold list from AMSE for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
                return;
            }

            if (!KeyValue.Get("ThresholdListBizDateArca", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
            {
                Log.Write("Waiting for the threshold list from ARCA for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
                return;
            }

            if (!KeyValue.Get("ThresholdListBizDateChse", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
            {
                Log.Write("Waiting for the threshold list from CHSE for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
                return;
            }

            if (!KeyValue.Get("ThresholdListBizDateNsdq", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
            {
                Log.Write("Waiting for the threshold list from NSDQ for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
                return;
            }

            if (!KeyValue.Get("ThresholdListBizDateNyse", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
            {
                Log.Write("Waiting for the threshold list from NYSE for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
                return;
            }

            SqlCommand sqlCmd = new SqlCommand("spThresholdList", dbCn);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            int itemCount = 0;
            string mailContent = "List Date".PadRight(14, ' ') +
                "Exchange".PadRight(10, ' ') +
                "SecId".PadRight(12, ' ') +
                "Symbol".PadRight(10, ' ') +
                "Description".PadRight(75, ' ') +
                "Day Count".ToString() + "\n";

            mailContent += "---------".PadRight(14, ' ') +
                "--------".PadRight(10, ' ') +
                "-----".PadRight(12, ' ') +
                "------".PadRight(10, ' ') +
                "-----------".PadRight(75, ' ') +
                "---------".ToString() + "\n";

            SqlDataReader sqlDataReader = null;

            try
            {
                dbCn.Open();
                sqlDataReader = sqlCmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    mailContent += sqlDataReader.GetValue(0).ToString().PadRight(14, ' ') +
                        sqlDataReader.GetValue(1).ToString().PadRight(10, ' ') +
                        sqlDataReader.GetValue(2).ToString().PadRight(12, ' ') +
                        sqlDataReader.GetValue(3).ToString().PadRight(10, ' ') +
                        sqlDataReader.GetValue(4).ToString().PadRight(75, ' ') +
                        sqlDataReader.GetValue(5).ToString() + "\n";

                    ++itemCount;
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MedalistMain.MailSendThreshold]", Log.Error, 1);
                return;
            }
            finally
            {
                if ((sqlDataReader != null) && (!sqlDataReader.IsClosed))
                {
                    sqlDataReader.Close();
                }

                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            try
            {
                Email email = new Email(dbCnStr);

                email.Send(
                    KeyValue.Get("ThresholdMailTo", "support@stockloan.net", dbCn),
                    KeyValue.Get("ThresholdMailFrom", "support@stockloan.net", dbCn),
                    "Threshold List for " + Tools.FormatDate(Master.BizDateBank, "dddd, d MMMM yyyy"),
                    mailContent);

                KeyValue.Set("ThresholdMailStatus", "OK", dbCn);
                KeyValue.Set("ThresholdMailDate", Master.BizDateBank, dbCn);

                Log.Write("Threshold e-mail notice has been sent for " + Master.BizDateBank + ". [MedalistMain.MailSendThreshold]", 2);
            }
            catch (Exception e)
            {
                KeyValue.Set("ThresholdMailStatus", e.Message, dbCn);
                Log.Write(e.Message + " [MedalistMain.MailSendThreshold]", Log.Error, 1);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }
    
        private void InventoryFundingRatesRoll()
        {
            string bizDate = KeyValue.Get("InventoryFundingRateBizDate", "2001-01-01", dbCn);

            if (bizDate.Equals(Master.BizDate))
            {
                Log.Write("Inventory funding rates have already been rolled to " + Master.BizDate + ". [MedalistMain.InventoryFundingRatesRoll]", 3);
                return;
            }

            SqlCommand sqlCommand;

            try
            {
                sqlCommand = new SqlCommand("spInventoryFundingRatesRoll", dbCn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                Log.Write("Rolling inventory funding rates records from " + Master.BizDatePrior + " to " + Master.BizDate + ". [MedalistMain.InventoryFundingRatesRoll]", 2);

                dbCn.Open();
                sqlCommand.ExecuteNonQuery();
                dbCn.Close();

                KeyValue.Set("InventoryFundingRateBizDate", Master.BizDate, dbCn);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MedalistMain.InventoryFundingRatesRoll]", Log.Error, 1);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
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
                Log.Write("Unable to parse BizDateRollUtcOffsetMinutes key value. [MedalistMain.BizDatesSet]", Log.Error, 2);
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
                        Log.Write("BizDate has been set to: " + Master.BizDate + " [MedalistMain.BizDatesSet]", Log.Information, 2);

                        KeyValue.Set("ContractsBizDate", "2001-01-01", dbCn);
                        Log.Write("ContractsBizDate has been set to: 2001-01-01 [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDateNext", "2001-01-01", dbCn).Equals(Master.BizDateNext))
                    {
                        KeyValue.Set("BizDateNext", Master.BizDateNext, dbCn);
                        Log.Write("BizDateNext has been set to: " + Master.BizDateNext + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDatePrior", "2001-01-01", dbCn).Equals(Master.BizDatePrior))
                    {
                        KeyValue.Set("BizDatePrior", Master.BizDatePrior, dbCn);
                        Log.Write("BizDatePrior has been set to: " + Master.BizDatePrior + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    break;
                case Standard.HolidayType.Bank:
                    Master.BizDateBank = bizDate.ToString(Standard.DateFormat);
                    Master.BizDateNextBank = bizDateNext.ToString(Standard.DateFormat);
                    Master.BizDatePriorBank = bizDatePrior.ToString(Standard.DateFormat);

                    if (!KeyValue.Get("BizDateBank", "2001-01-01", dbCn).Equals(Master.BizDateBank))
                    {
                        KeyValue.Set("BizDateBank", Master.BizDateBank, dbCn);
                        Log.Write("BizDateBank has been set to: " + Master.BizDateBank + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDateNextBank", "2001-01-01", dbCn).Equals(Master.BizDateNextBank))
                    {
                        KeyValue.Set("BizDateNextBank", Master.BizDateNextBank, dbCn);
                        Log.Write("BizDateNextBank has been set to: " + Master.BizDateNextBank + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDatePriorBank", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
                    {
                        KeyValue.Set("BizDatePriorBank", Master.BizDatePriorBank, dbCn);
                        Log.Write("BizDatePriorBank has been set to: " + Master.BizDatePriorBank + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    break;
                case Standard.HolidayType.Exchange:
                    Master.BizDateExchange = bizDate.ToString(Standard.DateFormat);
                    Master.BizDateNextExchange = bizDateNext.ToString(Standard.DateFormat);
                    Master.BizDatePriorExchange = bizDatePrior.ToString(Standard.DateFormat);

                    if (!KeyValue.Get("BizDateExchange", "2001-01-01", dbCn).Equals(Master.BizDateExchange))
                    {
                        KeyValue.Set("BizDateExchange", Master.BizDateExchange, dbCn);
                        Log.Write("BizDateExchange has been set to: " + Master.BizDateExchange + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDateNextExchange", "2001-01-01", dbCn).Equals(Master.BizDateNextExchange))
                    {
                        KeyValue.Set("BizDateNextExchange", Master.BizDateNextExchange, dbCn);
                        Log.Write("BizDateNextExchange has been set to: " + Master.BizDateNextExchange + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDatePriorExchange", "2001-01-01", dbCn).Equals(Master.BizDatePriorExchange))
                    {
                        KeyValue.Set("BizDatePriorExchange", Master.BizDatePriorExchange, dbCn);
                        Log.Write("BizDatePriorExchange has been set to: " + Master.BizDatePriorExchange + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    break;
            }
        }

        public void ShortSaleDailyQuantitiesPurge()
        {
            if (KeyValue.Get("ShortSaleDailyQuantitiesPurgeBizDate", "0000-01-01", dbCn).Equals(Master.BizDateBank))
            {
                Log.Write("ShortSale Daily Quantities purge already completed for " + Master.BizDateBank + ". [MedalistMain.ShortSaleDailyQuantitiesPurge]", Log.Information, 2);
                return;
            }
            else
            {
                SqlCommand dbCmd = null;

                try
                {
                    dbCn = new SqlConnection(dbCnStr);
                    dbCmd = new SqlCommand("spShortSaleDailyQuantitiesPurge", dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;

                    dbCn.Open();
                    dbCmd.ExecuteNonQuery();
                    dbCn.Close();

                    Log.Write("ShortSale Daily Quantities purge completed for " + Master.BizDateBank + ". [MedalistMain.ShortSaleDailyQuantitiesPurge]", Log.Information, 2);
                    KeyValue.Set("ShortSaleDailyQuantitiesPurgeBizDate", Master.BizDateBank, dbCn);
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [MedalistMain.ShortSaleDailyQuantitiesPurge]", Log.Error, 1);
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

        void TradingGroupSet(
            string groupCode,
            string groupName,
            string minPrice,
            string autoApprovalMax,
            string premiumMin,
            string premiumMax,
            bool autoEmail,
            string emailAddress,
            string lastEmailDate,
            string actUserId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spTradingGroupSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;


                SqlParameter paramGroupName = dbCmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50);
                if (!groupName.Equals(""))
                {
                    paramGroupName.Value = groupName;
                }

                SqlParameter paramMinPrice = dbCmd.Parameters.Add("@MinPrice", SqlDbType.Float);
                if (!minPrice.Equals(""))
                {
                    paramMinPrice.Value = minPrice;
                }

                SqlParameter paramAutoApprovalMax = dbCmd.Parameters.Add("@AutoApprovalMax", SqlDbType.BigInt);
                if (!autoApprovalMax.Equals("") && (Tools.IsNumeric(autoApprovalMax)))
                {
                    paramAutoApprovalMax.Value = autoApprovalMax;
                }

                SqlParameter paramPremiumMin = dbCmd.Parameters.Add("@PremiumMin", SqlDbType.BigInt);
                if (!premiumMin.Equals(""))
                {
                    paramPremiumMin.Value = premiumMin;
                }

                SqlParameter paramPremiumMax = dbCmd.Parameters.Add("@PremiumMax", SqlDbType.BigInt);
                if (!premiumMax.Equals(""))
                {
                    paramPremiumMax.Value = premiumMax;
                }

                SqlParameter paramAutoEmail = dbCmd.Parameters.Add("@AutoEmail", SqlDbType.Bit);
                paramAutoEmail.Value = autoEmail;

                SqlParameter paramEmailAddress = dbCmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 100);
                if (!emailAddress.Equals(""))
                {
                    paramEmailAddress.Value = emailAddress;
                }

                SqlParameter paramLastEmailDate = dbCmd.Parameters.Add("@LastEmailDate", SqlDbType.DateTime);
                if (!lastEmailDate.Equals(""))
                {
                    paramLastEmailDate.Value = lastEmailDate;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MedalistMain.TradingGroupSet]", Log.Error, 1);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
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
                recycleInterval = KeyValue.Get("MedalistMainLoopRecycleIntervalBizDay", "0:15", dbCn);
            }
            else
            {
                recycleInterval = KeyValue.Get("MedalistMainLoopRecycleIntervalNonBizDay", "4:00", dbCn);
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
                    KeyValue.Set("MedalistMainRecycleIntervalBizDay", "0:15", dbCn);
                    hours = 0;
                    minutes = 30;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("MedalistMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", Log.Error, 1);
                }
                else
                {
                    KeyValue.Set("MedalistMainRecycleIntervalNonBizDay", "4:00", dbCn);
                    hours = 6;
                    minutes = 0;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("MedalistMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", Log.Error, 1);
                }
            }

            Log.Write("MedalistMain will recycle in " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", 2);
            return timeSpan;
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

        public PositionAgent MedalistMainPositionAgent
        {
            set
            {
                positionAgent = value;
            }
        }

        public SubstitutionAgent MedalistMainSubstitutionAgent
        {
            set
            {
                substitutionAgent = value;
            }
        }
    }
}

