using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//-------------------------------------
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common; 

namespace StockLoanWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceAgent" in code, svc and config file together.
    public class ServiceAgent : IService
    {
        private string GetDatabaseConnection(string system)
        {
            switch (system.ToLower())
            {
                case "sendero":
                    return Standard.ConfigValue("SenderoDatabase", "");

                case "loanstar":
                    return Standard.ConfigValue("LoanstarDatabase", "");

                default:
                    return "";

            }
        }

        //------ Ported from Medalist ServiceAgent.cs ----------

        public string BizDate()
        {
            return Master.BizDate;
        }
        public string BizDateNext()
        {
            return Master.BizDateNext;
        }
        public string BizDatePrior()
        {
            return Master.BizDatePrior;
        }
        public string BizDateBank()
        {
            return Master.BizDateBank;
        }
        public string BizDateNextBank()
        {
            return Master.BizDateNextBank;
        }
        public string BizDatePriorBank()
        {
            return Master.BizDatePriorBank;
        }
        public string BizDateExchange()
        {
            return Master.BizDateExchange;
        }
        public string BizDateNextExchange()
        {
            return Master.BizDateNextExchange;
        }
        public string BizDatePriorExchange()
        {
            return Master.BizDatePriorExchange;
        }
        public string ContractsBizDate()
        {
            return Master.ContractsBizDate;
        }
        public bool IsSubstitutionActive()
        {
            return Master.IsSubstitutionActive;
        }

        public byte[] KeyValueGet()
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spKeyValueGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "KeyValues");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.KeyValueGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public string KeyValueGet(string keyId, string keyValueDefault)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            return KeyValue.Get(keyId, keyValueDefault, dbCnStr);
        }

        public void KeyValueSet(string keyId, string keyValue)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            KeyValue.Set(keyId, keyValue, dbCnStr);
        }

        public string NewProcessId(string prefix)
        {
            return Standard.ProcessId(prefix);
        }

        public byte[] ProcessStatusGet(short utcOffset)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spProcessStatusGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = Master.ContractsBizDate;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "ProcessStatus");

                dataSet.Tables["ProcessStatus"].PrimaryKey = new DataColumn[3]
          { 
            dataSet.Tables["ProcessStatus"].Columns["ProcessId"],
            dataSet.Tables["ProcessStatus"].Columns["SystemCode"],
            dataSet.Tables["ProcessStatus"].Columns["ActCode"] };
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.ProcessStatusGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public byte[] FirmGet()
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spFirmGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "Firms");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.FirmGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public byte[] CountryGet()
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spCountryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "Countries");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.CountryGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public byte[] DeskTypeGet()
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spDeskTypeGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "DeskTypes");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.DeskTypeGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public byte[] DeskGet()
        {
            return DeskGet("", false);
        }

        public byte[] DeskGet(string desk)
        {
            return DeskGet(desk, false);
        }

        public byte[] DeskGet(bool isNotSubscriber)
        {
            return DeskGet("", isNotSubscriber);
        }

        private byte[] DeskGet(string desk, bool isNotSubscriber)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spDeskGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!desk.Equals(""))
                {
                    SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                    paramDesk.Value = desk;
                }

                SqlParameter paramNoSubscription = dbCmd.Parameters.Add("@IsNotSubscriber", SqlDbType.Bit);
                paramNoSubscription.Value = isNotSubscriber;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "Desks");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.DeskGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public byte[] BookGroupGet()
        {
            return BookGroupGet(null, null);
        }

        public byte[] BookGroupGet(string userId, string functionPath)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBookGroupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if ((userId != null) && (functionPath != null))
                {
                    SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                    paramUserId.Value = userId;

                    SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                    paramFunctionPath.Value = functionPath;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "BookGroups");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.BookGroupGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public byte[] SecMasterLookup(string secId)
        {
            return SecMasterLookup(secId, false, false, 0, "");
        }

        public byte[] SecMasterLookup(string secId, bool withBox)
        {
            return SecMasterLookup(secId, withBox, false, 0, "");
        }

        public byte[] SecMasterLookup(string secId, bool withBox, bool withDeskQuips, short utcOffset, string since)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            Log.Write("Doing a security data lookup on " + secId + ", withBox = " + withBox.ToString() +
              ", withDeskQuips = " + withDeskQuips.ToString() + ", utcOffset = " + utcOffset +
              ", since = " + since + ". [ServiceAgent.SecMasterLookup]", 3);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spSecMasterItemGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "SecMasterItem");

                if (withBox)
                {
                    if (dataSet.Tables["SecMasterItem"].Rows.Count.Equals(1)) // Switch to the resolved SecId.
                    {
                        paramSecId.Value = dataSet.Tables["SecMasterItem"].Select()[0]["SecId"];
                    }

                    dbCmd.CommandText = "spBoxLocationGet";
                    dataAdapter.Fill(dataSet, "BoxLocation");

                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = Master.BizDate;

                    dbCmd.CommandText = "spBoxPositionGet";
                    dataAdapter.Fill(dataSet, "BoxPosition");

                    dbCmd.Parameters.Remove(paramBizDate);

                    dataSet.ExtendedProperties.Add("LoadDateTime",
                      KeyValue.Get("BoxPositionLoadDateTime", "0001-01-01 00:00:00", dbCn));
                }

                if (withDeskQuips)
                {
                    dbCmd.Parameters.Remove(paramSecId);

                    SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                    paramUtcOffset.Value = utcOffset;

                    SqlParameter paramSince = dbCmd.Parameters.Add("@Since", SqlDbType.DateTime);
                    paramSince.Value = since;

                    dbCmd.CommandText = "spDeskQuipGet";
                    dataAdapter.Fill(dataSet, "DeskQuips");
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.SecMasterLookup]", Log.Error, 1);
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public byte[] DeskQuipGet(short utcOffset)
        {
            return DeskQuipGet(utcOffset, "");
        }

        public byte[] DeskQuipGet(short utcOffset, string secId)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            Log.Write("Returning desk quips for parameters: " + utcOffset + "|" + secId + ". [ServiceAgent.DeskQuipGet]", 3);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spDeskQuipGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "DeskQuips");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + ". [ServiceAgent.DeskQuipGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public void DeskQuipSet(string secId, string deskQuip, string actUserId)
        {
            Log.Write("Setting new desk quip: " + secId + "|" + actUserId + "|" + deskQuip + " [ServiceAgent.DeskQuipSet]", 3);

            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            SqlCommand dbCmd = new SqlCommand("spDeskQuipSet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
            paramSecId.Value = secId;

            SqlParameter paramDeskQuip = dbCmd.Parameters.Add("@DeskQuip", SqlDbType.VarChar, 50);
            paramDeskQuip.Value = deskQuip;

            SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
            paramActUserId.Value = actUserId;

            SqlParameter paramActTime = dbCmd.Parameters.Add("@ActTime", SqlDbType.DateTime);
            paramActTime.Direction = ParameterDirection.Output;

            SqlParameter paramSymbol = dbCmd.Parameters.Add("@Symbol", SqlDbType.VarChar, 12);
            paramSymbol.Direction = ParameterDirection.Output;

            SqlParameter paramActUserShortName = dbCmd.Parameters.Add("@ActUserShortName", SqlDbType.VarChar, 15);
            paramActUserShortName.Direction = ParameterDirection.Output;

            try
            {
                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                DeskQuipEventArgs deskQuipEventArgs = new DeskQuipEventArgs(
                  secId,
                  paramSymbol.Value.ToString(),
                  deskQuip,
                  paramActUserShortName.Value.ToString(),
                  Tools.FormatDate(paramActTime.Value.ToString(), Standard.DateTimeFormat));

                DeskQuipEventHandler deskQuipEventHandler = new DeskQuipEventHandler(DeskQuipEventInvoke);
                deskQuipEventHandler.BeginInvoke(deskQuipEventArgs, null, null);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.DeskQuipSet]", Log.Error, 1);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        private void DeskQuipEventInvoke(DeskQuipEventArgs deskQuipEventArgs)
        {
            DeskQuipEventHandler deskQuipEventHandler = null;

            Log.Write(deskQuipEventArgs.ActUserShortName + ": " + deskQuipEventArgs.SecId + " [ServiceAgent.DeskQuipEventInvoke]", 3);

            try
            {
                if (DeskQuipEvent == null)
                {
                    Log.Write("There are no DeskQuipEvent delegates. [ServiceAgent.DeskQuipEventInvoke]", 3);
                }
                else
                {
                    int n = 0;

                    Delegate[] eventDelegates = DeskQuipEvent.GetInvocationList();
                    Log.Write("DeskQuipEvent has " + eventDelegates.Length + " delegate[s]. [ServiceAgent.DeskQuipEventInvoke]", 3);

                    foreach (Delegate eventDelegate in eventDelegates)
                    {
                        Log.Write("Invoking DeskQuipEvent delegate [" + (++n) + "]. [ServiceAgent.DeskQuipEventInvoke]", 3);

                        try
                        {
                            deskQuipEventHandler = (DeskQuipEventHandler)eventDelegate;
                            deskQuipEventHandler(deskQuipEventArgs);
                        }
                        catch (System.Net.Sockets.SocketException)
                        {
                            DeskQuipEvent -= deskQuipEventHandler;
                            Log.Write("DeskQuipEvent delegate [" + n + "] has been removed from the invocation list. [ServiceAgent.DeskQuipEventInvoke]", 3);
                        }
                        catch (Exception e)
                        {
                            Log.Write(e.Message + " [ServiceAgent.DeskQuipEventInvoke]", Log.Error, 1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.DeskQuipEventInvoke]", Log.Error, 1);
            }
        }

        public byte[] InventoryDataMaskGet(string desk)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand sqlDbCmd = new SqlCommand("spInventoryFileDataMaskList", dbCn);
                sqlDbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesk = sqlDbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                paramDesk.Value = desk;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlDbCmd);
                dataAdapter.Fill(dataSet, "InventoryDataMasks");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.InventoryDataMaskGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public void InventoryDataMaskSet(string desk, short recordLength, char headerFlag, char dataFlag, char trailerFlag, short accountLocale,
                                        char delimiter, short accountOrdinal, short secIdOrdinal, short quantityOrdinal, short recordCountOrdinal, 
                                        short accountPosition, short accountLength, short bizDateDD, short bizDateMM, short bizDateYY, short secIdPosition, 
                                        short secIdLength, short quantityPosition, short quantityLength, short recordCountPosition, short recordCountLength, string actUserId)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            SqlCommand sqlDbCmd = null;

            try
            {
                sqlDbCmd = new SqlCommand("spInventoryFileDataMaskSet", dbCn);
                sqlDbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesk = sqlDbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                paramDesk.Value = desk;

                SqlParameter paramRecordLength = sqlDbCmd.Parameters.Add("@RecordLength", SqlDbType.SmallInt);
                paramRecordLength.Value = recordLength;

                SqlParameter paramHeaderFlag = sqlDbCmd.Parameters.Add("@HeaderFlag", SqlDbType.Char, 1);
                paramHeaderFlag.Value = headerFlag;

                SqlParameter paramDataFlag = sqlDbCmd.Parameters.Add("@DataFlag", SqlDbType.Char, 1);
                paramDataFlag.Value = dataFlag;

                SqlParameter paramTrailerFlag = sqlDbCmd.Parameters.Add("@TrailerFlag", SqlDbType.Char, 1);
                paramTrailerFlag.Value = trailerFlag;

                SqlParameter paramAccountLocale = sqlDbCmd.Parameters.Add("@AccountLocale", SqlDbType.SmallInt, 1);
                paramAccountLocale.Value = accountLocale;

                SqlParameter paramDelimiter = sqlDbCmd.Parameters.Add("@Delimiter", SqlDbType.Char, 1);
                paramDelimiter.Value = delimiter;

                SqlParameter paramAccountOrdinal = sqlDbCmd.Parameters.Add("@AccountOrdinal", SqlDbType.SmallInt, 1);
                paramAccountOrdinal.Value = accountOrdinal;

                SqlParameter paramSecIdOrdinal = sqlDbCmd.Parameters.Add("@SecIdOrdinal", SqlDbType.SmallInt);
                paramSecIdOrdinal.Value = secIdOrdinal;

                SqlParameter paramQuantityOrdinal = sqlDbCmd.Parameters.Add("@QuantityOrdinal", SqlDbType.SmallInt);
                paramQuantityOrdinal.Value = quantityOrdinal;

                SqlParameter paramRecordCountOrdinal = sqlDbCmd.Parameters.Add("@RecordCountOrdinal", SqlDbType.SmallInt);
                paramRecordCountOrdinal.Value = recordCountOrdinal;

                SqlParameter paramAccountPosition = sqlDbCmd.Parameters.Add("@AccountPosition", SqlDbType.SmallInt);
                paramAccountPosition.Value = accountPosition;

                SqlParameter paramAccountLength = sqlDbCmd.Parameters.Add("@AccountLength", SqlDbType.SmallInt);
                paramAccountLength.Value = accountLength;

                SqlParameter paramBizDateDD = sqlDbCmd.Parameters.Add("@BizDateDD", SqlDbType.SmallInt);
                paramBizDateDD.Value = bizDateDD;

                SqlParameter paramBizDateMM = sqlDbCmd.Parameters.Add("@BizDateMM", SqlDbType.SmallInt);
                paramBizDateMM.Value = bizDateMM;

                SqlParameter paramBizDateYY = sqlDbCmd.Parameters.Add("@BizDateYY", SqlDbType.SmallInt);
                paramBizDateYY.Value = bizDateYY;

                SqlParameter paramSecIdPosition = sqlDbCmd.Parameters.Add("@SecIdPosition", SqlDbType.SmallInt);
                paramSecIdPosition.Value = secIdPosition;

                SqlParameter paramSecIdLength = sqlDbCmd.Parameters.Add("@SecIdLength", SqlDbType.SmallInt);
                paramSecIdLength.Value = secIdLength;

                SqlParameter paramQuantityPosition = sqlDbCmd.Parameters.Add("@QuantityPosition", SqlDbType.SmallInt);
                paramQuantityPosition.Value = quantityPosition;

                SqlParameter paramQuantityLength = sqlDbCmd.Parameters.Add("@QuantityLength", SqlDbType.SmallInt);
                paramQuantityLength.Value = quantityLength;

                SqlParameter paramRecordCountPosition = sqlDbCmd.Parameters.Add("@RecordCountPosition", SqlDbType.SmallInt);
                paramRecordCountPosition.Value = recordCountPosition;

                SqlParameter paramRecordCountLength = sqlDbCmd.Parameters.Add("@RecordCountLength", SqlDbType.SmallInt);
                paramRecordCountLength.Value = recordCountLength;

                SqlParameter paramActUserId = sqlDbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 25);
                paramActUserId.Value = actUserId;

                dbCn.Open();
                sqlDbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.InventoryDataMaskSet]", Log.Error, 1);
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

        public byte[] SubscriberListGet(short utcOffset)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spInventorySubscriberGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "SubscriberList");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.SubscriberListGet]", Log.Error, 1);
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public void SubscriberListSet(string desk,
                                    string ftpPath,
                                    string ftpHost,
                                    string ftpUserName,
                                    string ftpPassword,
                                    string loadExPGP,
                                    string comment,
                                    string mailAddress,
                                    string mailSubject,
                                    string isActive,
                                    string usePGP,
                                    bool isBizDatePrior,
                                    string actUserId)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spInventorySubscriberSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, (12));
                paramDesk.Value = desk;

                SqlParameter paramFtpPath = dbCmd.Parameters.Add("@FilePathName", SqlDbType.VarChar, (100));
                paramFtpPath.Value = ftpPath;

                SqlParameter paramFtpHost = dbCmd.Parameters.Add("@FileHost", SqlDbType.VarChar, (50));
                paramFtpHost.Value = ftpHost;

                SqlParameter paramFtpUserName = dbCmd.Parameters.Add("@FileUserName", SqlDbType.VarChar, (50));
                paramFtpUserName.Value = ftpUserName;

                SqlParameter paramFtpPassword = dbCmd.Parameters.Add("@FilePassword", SqlDbType.VarChar, (25));
                paramFtpPassword.Value = ftpPassword;

                SqlParameter paramLoadExPGP = dbCmd.Parameters.Add("@LoadExtensionPgp", SqlDbType.VarChar, (50));
                paramLoadExPGP.Value = loadExPGP;

                SqlParameter paramIsBizDatePrior = dbCmd.Parameters.Add("@IsBizDatePrior", SqlDbType.Bit);
                paramIsBizDatePrior.Value = isBizDatePrior;

                SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, (50));
                paramComment.Value = comment;

                SqlParameter paramMailAddress = dbCmd.Parameters.Add("@MailAddress", SqlDbType.VarChar, (50));
                paramMailAddress.Value = mailAddress;

                SqlParameter paramMailSubject = dbCmd.Parameters.Add("@MailSubject", SqlDbType.VarChar, (25));
                paramMailSubject.Value = mailSubject;

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                SqlParameter paramUsePGP = dbCmd.Parameters.Add("@UsePgp", SqlDbType.Bit);
                paramUsePGP.Value = usePGP;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                if (dbCn.State != ConnectionState.Open)
                {
                    dbCn.Open();
                }

                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.SubscriberListSet]", Log.Error, 1);
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

        public byte[] PublisherListGet(short utcOffset)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spInventoryPublisherGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "PublisherList");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.PublisherListGet]", Log.Error, 1);
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public void PublisherListSet(string desk,
                                    string ftpPath,
                                    string ftpHost,
                                    string ftpUserName,
                                    string ftpPassword,
                                    string loadExPGP,
                                    string comment,
                                    string mailAddress,
                                    string mailSubject,
                                    string isActive,
                                    string usePGP,
                                    string reportName,
                                    string reportFrequency,
                                    string reportWaitUntil,
                                    string actUserId)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spInventoryPublisherSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, (12));
                paramDesk.Value = desk;

                SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
                paramReportName.Value = reportName;

                SqlParameter paramReportFrequency = dbCmd.Parameters.Add("@ReportFrequency", SqlDbType.VarChar, 10);
                paramReportFrequency.Value = reportFrequency;

                SqlParameter paramReportWaitUntil = dbCmd.Parameters.Add("@ReportWaitUntil", SqlDbType.VarChar, 5);
                paramReportWaitUntil.Value = reportWaitUntil;

                SqlParameter paramFtpPath = dbCmd.Parameters.Add("@FilePathName", SqlDbType.VarChar, (100));
                paramFtpPath.Value = ftpPath;

                SqlParameter paramFtpHost = dbCmd.Parameters.Add("@FileHost", SqlDbType.VarChar, (50));
                paramFtpHost.Value = ftpHost;

                SqlParameter paramFtpUserName = dbCmd.Parameters.Add("@FileUserName", SqlDbType.VarChar, (50));
                paramFtpUserName.Value = ftpUserName;

                SqlParameter paramFtpPassword = dbCmd.Parameters.Add("@FilePassword", SqlDbType.VarChar, (25));
                paramFtpPassword.Value = ftpPassword;

                SqlParameter paramLoadExPGP = dbCmd.Parameters.Add("@LoadExtensionPgp", SqlDbType.VarChar, (50));
                paramLoadExPGP.Value = loadExPGP;

                SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, (50));
                paramComment.Value = comment;

                SqlParameter paramMailAddress = dbCmd.Parameters.Add("@MailAddress", SqlDbType.VarChar, (50));
                paramMailAddress.Value = mailAddress;

                SqlParameter paramMailSubject = dbCmd.Parameters.Add("@MailSubject", SqlDbType.VarChar, (25));
                paramMailSubject.Value = mailSubject;

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                SqlParameter paramUsePGP = dbCmd.Parameters.Add("@UsePgp", SqlDbType.Bit);
                paramUsePGP.Value = usePGP;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                if (dbCn.State != ConnectionState.Open)
                {
                    dbCn.Open();
                }

                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.PublisherListSet]", Log.Error, 1);
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

        public byte[] PublisherReportsGet(string reportName)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spInventoryPublisherReportGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!reportName.Equals(""))
                {
                    SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
                    paramReportName.Value = reportName;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "Reports");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.PublisherReportsGet]", Log.Error, 1);
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        public void PublisherReportSet(string reportName, string reportStoredProc, string reportDescription )
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spInventoryPublisherReportSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
                paramReportName.Value = reportName;

                if (!reportStoredProc.Equals(""))
                {
                    SqlParameter paramReportStoredProc = dbCmd.Parameters.Add("@ReportStoredProc", SqlDbType.VarChar, 100);
                    paramReportStoredProc.Value = reportStoredProc;
                }

                if (!reportDescription.Equals(""))
                {
                    SqlParameter paramReportDescription = dbCmd.Parameters.Add("@ReportDescription", SqlDbType.VarChar, 100);
                    paramReportDescription.Value = reportDescription;
                }

                if (dbCn.State != ConnectionState.Open)
                {
                    dbCn.Open();
                }

                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.PublisherReportSet]", Log.Error, 1);
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

        public void BorrowHardSet(string secId, string actUserId, bool delete)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            SqlCommand dbCmd = new SqlCommand("spBorrowHardSet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
                paramDelete.Value = delete;

                if (dbCn.State != ConnectionState.Open)
                {
                    dbCn.Open();
                }

                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.BororowHardSet]", Log.Error, 1);
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

        public void BorrowNoSet(string secId, string actUserId, bool delete)
        {
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            SqlCommand dbCmd = new SqlCommand("spBorrowNoSet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
                paramDelete.Value = delete;

                if (dbCn.State != ConnectionState.Open)
                {
                    dbCn.Open();
                }

                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.BororowNoSet]", Log.Error, 1);
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

        public byte[] InventoryDeskInputDataGet()
        {
            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet();
            string dbCnStr = GetDatabaseConnection("Sendero");  		//new

            try
            {
                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spDeskGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "Desks");

                dbCmd.CommandText = "spBooksGet";
                dataAdapter.Fill(dataSet, "Books");

                dbCmd.CommandText = "spBookGroupGet";
                dataAdapter.Fill(dataSet, "BookGroups");

                dbCmd.CommandText = "spCountryGet";
                dataAdapter.Fill(dataSet, "Countries");

                dbCmd.CommandText = "spDeskTypeGet";
                dataAdapter.Fill(dataSet, "DeskTypes");

                dbCmd.CommandText = "spFirmGet";
                dataAdapter.Fill(dataSet, "Firms");

                dataSet.Tables["Books"].PrimaryKey = new DataColumn[2]
          { 
            dataSet.Tables["Books"].Columns["BookGroup"],
            dataSet.Tables["Books"].Columns["Book"]
          };

                dataSet.Tables["Countries"].PrimaryKey = new DataColumn[1]
          { 
            dataSet.Tables["Countries"].Columns["CountryCode"]
          };

                dataSet.Tables["DeskTypes"].PrimaryKey = new DataColumn[1]
          { 
            dataSet.Tables["DeskTypes"].Columns["DeskTypeCode"]
          };

                dataSet.Tables["Firms"].PrimaryKey = new DataColumn[1]
          { 
            dataSet.Tables["Firms"].Columns["FirmCode"]
          };
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ServiceAgent.InventoryDeskInputDataGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dataSet); 		//new
        }

        /******
        public override object InitializeLifetimeService()
        {
            return null;
        }
        ******/ 

    }
}
