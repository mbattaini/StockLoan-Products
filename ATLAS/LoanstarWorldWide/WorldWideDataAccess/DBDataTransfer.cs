using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBDataTransfer
    {
        // Note: DBDataTransfer class methods are calling store_proc in [Sendero] database. 
 
		private static string dbCnStr = DBStandardFunctions.DbCnStr;    //LoanStar database, this is ignored in DBDataTransfer class

        public static DataSet InventoryRateItemCountGet(string bizDate, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();
            string sql = ""; 

            try
            {   // Sendero [tbInventoryRateControl] table does Not have Desk info: Aztec Rate data only. 
                sql = "Use Sendero " +
                      "Select BizDate, Count(*) As [COUNT] From dbo.tbInventoryRateControl With (NOLOCK) " +
                      "Where BizDate = '" + bizDate + "' " +
                      "Group by BizDate ";

                SqlCommand dbCmd = new SqlCommand(sql, dbCn);               // Sendero database 
                dbCmd.CommandType = CommandType.Text; 
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "600"));

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "InventoryRateItemCount");

            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void InventoryRateSet(string bizDate, string secId, string rate, string actUserId, string dbCnStr)
        {   
			SqlConnection dbCn = new SqlConnection(dbCnStr);
             
			try
			{
                SqlCommand dbCmd = new SqlCommand("dbo.spInventoryRateSet", dbCn);          // Sendero database
				dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);    //In Sendero: @Rate Float,
				paramRate.Value = decimal.Parse(rate);

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

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

        public static void InventoryItemSet(string bizDate, string desk, string account, string secId, 
                                            string rate, string modeCode, string quantity, bool incrementCurrentQuantity, string dbCnStr)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
                SqlCommand dbCmd = new SqlCommand("dbo.spInventoryItemSet", dbCn);          // Sendero database 
				dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "600"));

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
                paramDesk.Value = desk;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                if (!quantity.Equals(""))
                {
                    SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                    paramQuantity.Value = quantity;
                }
                SqlParameter paramIncrementCurrentQuantity = dbCmd.Parameters.Add("@IncrementCurrentQuantity", SqlDbType.Bit);
                paramIncrementCurrentQuantity.Value = incrementCurrentQuantity;            

                if (!account.Equals(""))
                {
                    SqlParameter paramAccount = dbCmd.Parameters.Add("@Account", SqlDbType.VarChar, 15);
                    paramAccount.Value = account;
                }
                if (!rate.Equals(""))
                {
                    SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);      // .float 
                    paramRate.Value = decimal.Parse(rate);
                }
                if (!modeCode.Equals(""))
                {
                    SqlParameter paramModeCode = dbCmd.Parameters.Add("@ModeCode", SqlDbType.Char, 1);
                    paramModeCode.Value = modeCode;
                }

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

        public static DataSet InventorySubscriberGet(string desk, short utcOffSet, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {   
                SqlCommand dbCmd = new SqlCommand("dbo.spInventorySubscriberGet", dbCn);    // Sendero database 
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "600"));

                if (!desk.Equals(""))
                {
                    SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                    paramDesk.Value = desk;
                }
                SqlParameter paramUtcOffSet = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffSet.Value = utcOffSet;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "InventorySubscriber");

            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void InventorySubscriberSet(string desk, string filePathName, string fileHost, string fileUserName, string filePassword,
                                        string fileCheckTime, string fileTime, string fileStatus, bool usePgp, string loadExtensionPgp,
                                        bool isBizDatePrior, string mailAddress, string mailSubject, string bizDate, string loadTime, 
                                        string loadCount, string loadStatus, string comment, string actUserId, bool isActive, string dbCnStr) 
        { 
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spInventorySubscriberSet", dbCn);            // Sendero database 
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "500"));

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                paramDesk.Value = desk;

                if (!filePathName.Equals(""))
                {
                    SqlParameter paramFileName = dbCmd.Parameters.Add("@FilePathName", SqlDbType.VarChar, 100);
                    paramFileName.Value = filePathName;
                }
                if (!fileHost.Equals(""))
                {
                    SqlParameter paramFileHost = dbCmd.Parameters.Add("@FileHost", SqlDbType.VarChar, 100);
                    paramFileHost.Value = fileHost;
                }
                if (!fileUserName.Equals(""))
                {
                    SqlParameter paramFileUserName = dbCmd.Parameters.Add("@FileUserName", SqlDbType.VarChar, 50);
                    paramFileUserName.Value = fileUserName;
                }
                if (!filePassword.Equals(""))
                {
                    SqlParameter paramFilePassword = dbCmd.Parameters.Add("@FilePassword", SqlDbType.VarChar, 25);
                    paramFilePassword.Value = filePassword;
                }
                if (!fileCheckTime.Equals(""))
                {
                    SqlParameter paramFileCheckTime = dbCmd.Parameters.Add("@FileCheckTime", SqlDbType.DateTime);
                    paramFileCheckTime.Value = DateTime.Parse(fileCheckTime);
                }
                if (!fileTime.Equals(""))
                {
                    SqlParameter paramFileTime = dbCmd.Parameters.Add("@FileTime", SqlDbType.DateTime);
                    paramFileTime.Value = DateTime.Parse(fileTime);
                }
                if (!fileStatus.Equals(""))
                {
                    SqlParameter paramFileStatus = dbCmd.Parameters.Add("@FileStatus", SqlDbType.VarChar, 100);
                    paramFileStatus.Value = fileStatus;
                }
                SqlParameter paramUsePgp = dbCmd.Parameters.Add("@UsePgp", SqlDbType.Bit);
                paramUsePgp.Value = usePgp;

                if (!loadExtensionPgp.Equals(""))
                {
                    SqlParameter paramLoadExtensionPgp = dbCmd.Parameters.Add("@LoadExtensionPgp", SqlDbType.VarChar, 50);
                    paramLoadExtensionPgp.Value = loadExtensionPgp;
                }
                SqlParameter paramIsBizDatePrior = dbCmd.Parameters.Add("@IsBizDatePrior", SqlDbType.Bit);
                paramIsBizDatePrior.Value = isBizDatePrior;

                if (!mailAddress.Equals(""))
                {
                    SqlParameter paramMailAddress = dbCmd.Parameters.Add("@MailAddress", SqlDbType.Char, 50);
                    paramMailAddress.Value = mailAddress;
                }
                if (!mailSubject.Equals(""))
                {
                    SqlParameter paramMailSubject = dbCmd.Parameters.Add("@MailSubject", SqlDbType.VarChar, 100);
                    paramMailSubject.Value = mailSubject;
                }
                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = DateTime.Parse(bizDate);
                }
                if (!loadTime.Equals(""))
                {
                    SqlParameter paramLoadTime = dbCmd.Parameters.Add("@LoadTime", SqlDbType.DateTime);
                    paramLoadTime.Value = DateTime.Parse(loadTime);
                }
                if (!loadCount.Equals(""))
                {
                    SqlParameter paramLoadCount = dbCmd.Parameters.Add("@LoadCount", SqlDbType.Int);
                    paramLoadCount.Value = int.Parse(loadCount);
                }
                if (!loadStatus.Equals(""))
                {
                    SqlParameter paramLoadStatus = dbCmd.Parameters.Add("@LoadStatus", SqlDbType.VarChar, 200);
                    paramLoadStatus.Value = loadStatus;
                }
                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.Char, 200);
                    paramComment.Value = comment;
                }
                if (!actUserId.Equals(""))
                {
                    SqlParameter paramactUserId = dbCmd.Parameters.Add("@actUserId", SqlDbType.Char, 50);
                    paramactUserId.Value = actUserId;
                }
                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
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

        public static DataSet BoxPositionDataTransferGet(string bizDate, string bookGroup, string secId, string dbCnStr)
        { 
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBoxPositionDataTransferGet", dbCn);    // LoanStar database 
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "900"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = DateTime.Parse(bizDate);

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

        public static void BoxPositionDataTransferSet(string bizDate, string bookGroup, string secId, string customerLongSettled,
                                string customerLongTraded, string customerShortSettled, string customerShortTraded, string firmLongSettled, string firmLongTraded, 
                                string firmShortSettled, string firmShortTraded, string customerPledgeSettled, string customerPledgeTraded, string firmPledgeSettled, 
                                string firmPledgeTraded, string dvpFailInSettled, string dvpFailInTraded, string dvpFailOutSettled, string dvpFailOutTraded, 
                                string brokerFailInSettled, string brokerFailInTraded, string brokerFailOutSettled, string brokerFailOutTraded, string clearingFailInSettled,
                                string clearingFailInTraded, string clearingFailOutSettled, string clearingFailOutTraded, string otherFailInSettled, string otherFailInTraded, 
                                string otherFailOutSettled, string otherFailOutTraded, string exDeficitSettled, string exDeficitTraded, string dvpFailInDayCount, 
                                string dvpFailOutDayCount, string brokerFailInDayCount, string brokerFailOutDayCount, string clearingFailInDayCount, 
                                string clearingFailOutDayCount, string otherFailInDayCount, string otherFailOutDayCount, string deficitDayCount,
                                bool state, string segReqSettled, string netPositionSettled, string netPositionTraded, string netPositionSettledDayCount, 
                                string rockLongSettled, string rockShortSettled, string rockLongTraded, string rockShortTraded, string jboLongSettled,
                                string jboLongTraded, string jboShortSettled, string jboShortTraded, string dbCnStr 
            )
        {   

            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try 
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBoxPositionDataTransferSet", dbCn);            // Sendero database 
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "900"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = DateTime.Parse(bizDate);

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramCustomerLongSettled = dbCmd.Parameters.Add("@CustomerLongSettled", SqlDbType.BigInt);
                paramCustomerLongSettled.Value = long.Parse(customerLongSettled);

                SqlParameter paramCustomerLongTraded = dbCmd.Parameters.Add("@CustomerLongTraded", SqlDbType.BigInt);
                paramCustomerLongTraded.Value = long.Parse(customerLongTraded);

                SqlParameter paramCustomerShortSettled = dbCmd.Parameters.Add("@CustomerShortSettled", SqlDbType.BigInt);
                paramCustomerShortSettled.Value = long.Parse(customerShortSettled);

                SqlParameter paramCustomerShortTraded = dbCmd.Parameters.Add("@CustomerShortTraded", SqlDbType.BigInt);
                paramCustomerShortTraded.Value = long.Parse(customerShortTraded);
                
                SqlParameter paramFirmLongSettled = dbCmd.Parameters.Add("@FirmLongSettled", SqlDbType.BigInt);
                paramFirmLongSettled.Value = long.Parse(firmLongSettled);

                SqlParameter paramFirmLongTraded = dbCmd.Parameters.Add("@FirmLongTraded", SqlDbType.BigInt);
                paramFirmLongTraded.Value = long.Parse(firmLongTraded);

                SqlParameter paramFirmShortSettled = dbCmd.Parameters.Add("@FirmShortSettled", SqlDbType.BigInt);
                paramFirmShortSettled.Value = long.Parse(firmShortSettled);

                SqlParameter paramFirmShortTraded = dbCmd.Parameters.Add("@FirmShortTraded", SqlDbType.BigInt);
                paramFirmShortTraded.Value = long.Parse(firmShortTraded);

                SqlParameter paramCustomerPledgeSettled = dbCmd.Parameters.Add("@CustomerPledgeSettled", SqlDbType.BigInt);
                paramCustomerPledgeSettled.Value = long.Parse(customerPledgeSettled);

                SqlParameter paramCustomerPledgeTraded = dbCmd.Parameters.Add("@CustomerPledgeTraded", SqlDbType.BigInt);
                paramCustomerPledgeTraded.Value = long.Parse(customerPledgeTraded);

                SqlParameter paramFirmPledgeSettled = dbCmd.Parameters.Add("@FirmPledgeSettled", SqlDbType.BigInt);
                paramFirmPledgeSettled.Value = long.Parse(firmPledgeSettled);

                SqlParameter paramFirmPledgeTraded = dbCmd.Parameters.Add("@FirmPledgeTraded", SqlDbType.BigInt);
                paramFirmPledgeTraded.Value = long.Parse(firmPledgeTraded);

                SqlParameter paramDvpFailInSettled = dbCmd.Parameters.Add("@DvpFailInSettled", SqlDbType.BigInt);
                paramDvpFailInSettled.Value = long.Parse(dvpFailInSettled);

                SqlParameter paramDvpFailInTraded = dbCmd.Parameters.Add("@DvpFailInTraded", SqlDbType.BigInt);
                paramDvpFailInTraded.Value = long.Parse(dvpFailInTraded);

                SqlParameter paramDvpFailOutSettled = dbCmd.Parameters.Add("@DvpFailOutSettled", SqlDbType.BigInt);
                paramDvpFailOutSettled.Value = long.Parse(dvpFailOutSettled);

                SqlParameter paramDvpFailOutTraded = dbCmd.Parameters.Add("@DvpFailOutTraded", SqlDbType.BigInt);
                paramDvpFailOutTraded.Value = long.Parse(dvpFailOutTraded);

                SqlParameter paramBrokerFailInSettled = dbCmd.Parameters.Add("@BrokerFailInSettled", SqlDbType.BigInt);
                paramBrokerFailInSettled.Value = long.Parse(brokerFailInSettled);

                SqlParameter paramBrokerFailInTraded = dbCmd.Parameters.Add("@BrokerFailInTraded", SqlDbType.BigInt);
                paramBrokerFailInTraded.Value = long.Parse(brokerFailInTraded);

                SqlParameter paramBrokerFailOutSettled = dbCmd.Parameters.Add("@BrokerFailOutSettled", SqlDbType.BigInt);
                paramBrokerFailOutSettled.Value = long.Parse(brokerFailOutSettled);

                SqlParameter paramBrokerFailOutTraded = dbCmd.Parameters.Add("@BrokerFailOutTraded", SqlDbType.BigInt);
                paramBrokerFailOutTraded.Value = long.Parse(brokerFailOutTraded);

                SqlParameter paramClearingFailInSettled = dbCmd.Parameters.Add("@ClearingFailInSettled", SqlDbType.BigInt);
                paramClearingFailInSettled.Value = long.Parse(clearingFailInSettled);

                SqlParameter paramClearingFailInTraded = dbCmd.Parameters.Add("@ClearingFailInTraded", SqlDbType.BigInt);
                paramClearingFailInTraded.Value = long.Parse(clearingFailInTraded);

                SqlParameter paramClearingFailOutSettled = dbCmd.Parameters.Add("@ClearingFailOutSettled", SqlDbType.BigInt);
                paramClearingFailOutSettled.Value = int.Parse(clearingFailOutSettled);

                SqlParameter paramClearingFailOutTraded = dbCmd.Parameters.Add("@ClearingFailOutTraded", SqlDbType.BigInt);
                paramClearingFailOutTraded.Value = long.Parse(clearingFailOutTraded);

                SqlParameter paramOtherFailInSettled = dbCmd.Parameters.Add("@OtherFailInSettled", SqlDbType.BigInt);
                paramOtherFailInSettled.Value = int.Parse(otherFailInSettled);

                SqlParameter paramOtherFailInTraded = dbCmd.Parameters.Add("@OtherFailInTraded", SqlDbType.BigInt);
                paramOtherFailInTraded.Value = long.Parse(otherFailInTraded);

                SqlParameter paramOtherFailOutSettled = dbCmd.Parameters.Add("@OtherFailOutSettled", SqlDbType.BigInt);
                paramOtherFailOutSettled.Value = long.Parse(otherFailOutSettled);

                SqlParameter paramOtherFailOutTraded = dbCmd.Parameters.Add("@OtherFailOutTraded", SqlDbType.BigInt);
                paramOtherFailOutTraded.Value = long.Parse(otherFailOutTraded);

                SqlParameter paramExDeficitSettled = dbCmd.Parameters.Add("@ExDeficitSettled", SqlDbType.BigInt);
                paramExDeficitSettled.Value = long.Parse(exDeficitSettled);

                SqlParameter paramExDeficitTraded = dbCmd.Parameters.Add("@ExDeficitTraded", SqlDbType.BigInt);
                paramExDeficitTraded.Value = long.Parse(exDeficitTraded);

                SqlParameter paramDvpFailInDayCount = dbCmd.Parameters.Add("@DvpFailInDayCount", SqlDbType.Int);
                paramDvpFailInDayCount.Value = int.Parse(dvpFailInDayCount);

                SqlParameter paramDvpFailOutDayCount = dbCmd.Parameters.Add("@DvpFailOutDayCount", SqlDbType.Int);
                paramDvpFailOutDayCount.Value = int.Parse(dvpFailOutDayCount);

                SqlParameter paramBrokerFailInDayCount = dbCmd.Parameters.Add("@BrokerFailInDayCount", SqlDbType.Int);
                paramBrokerFailInDayCount.Value = int.Parse(brokerFailInDayCount);

                SqlParameter paramBrokerFailOutDayCount = dbCmd.Parameters.Add("@BrokerFailOutDayCount", SqlDbType.Int);
                paramBrokerFailOutDayCount.Value = int.Parse(brokerFailOutDayCount);

                SqlParameter paramClearingFailInDayCount = dbCmd.Parameters.Add("@ClearingFailInDayCount", SqlDbType.Int);
                paramClearingFailInDayCount.Value = int.Parse(clearingFailInDayCount);

                SqlParameter paramClearingFailOutDayCount = dbCmd.Parameters.Add("@ClearingFailOutDayCount", SqlDbType.Int);
                paramClearingFailOutDayCount.Value = int.Parse(clearingFailOutDayCount);

                SqlParameter paramOtherFailInDayCount = dbCmd.Parameters.Add("@OtherFailInDayCount", SqlDbType.Int);
                paramOtherFailInDayCount.Value = int.Parse(otherFailInDayCount);

                SqlParameter paramOtherFailOutDayCount = dbCmd.Parameters.Add("@OtherFailOutDayCount", SqlDbType.Int);
                paramOtherFailOutDayCount.Value = int.Parse(otherFailOutDayCount);

                SqlParameter paramDeficitDayCount = dbCmd.Parameters.Add("@DeficitDayCount", SqlDbType.Int);
                paramDeficitDayCount.Value = int.Parse(deficitDayCount);

                SqlParameter paramState = dbCmd.Parameters.Add("@State", SqlDbType.Bit);
                paramState.Value = state;

                if (!segReqSettled.Equals(""))
                {
                    SqlParameter paramSegReqSettled = dbCmd.Parameters.Add("@SegReqSettled", SqlDbType.BigInt);
                    paramSegReqSettled.Value = long.Parse(segReqSettled);
                }
                if (!netPositionSettled.Equals(""))
                {
                    SqlParameter paramNetPositionSettled = dbCmd.Parameters.Add("@NetPositionSettled", SqlDbType.BigInt);
                    paramNetPositionSettled.Value = long.Parse(netPositionSettled);
                }
                if (!netPositionTraded.Equals(""))
                {
                    SqlParameter paramNetPositionTraded = dbCmd.Parameters.Add("@NetPositionTraded", SqlDbType.BigInt);
                    paramNetPositionTraded.Value = long.Parse(netPositionTraded);
                }
                if (!netPositionSettledDayCount.Equals(""))
                {
                    SqlParameter paramNetPositionSettledDayCount = dbCmd.Parameters.Add("@NetPositionSettledDayCount", SqlDbType.Int);
                    paramNetPositionSettledDayCount.Value = int.Parse(netPositionSettledDayCount);
                }
                if (!rockLongSettled.Equals(""))
                {
                    SqlParameter paramRockLongSettled = dbCmd.Parameters.Add("@RockLongSettled", SqlDbType.BigInt);
                    paramRockLongSettled.Value = long.Parse(rockLongSettled);
                }
                if (!rockShortSettled.Equals(""))
                {
                    SqlParameter paramRockShortSettled = dbCmd.Parameters.Add("@RockShortSettled", SqlDbType.BigInt);
                    paramRockShortSettled.Value = long.Parse(rockShortSettled);
                }
                if (!rockLongTraded.Equals(""))
                {
                    SqlParameter paramRockLongTraded = dbCmd.Parameters.Add("@RockLongTraded", SqlDbType.BigInt);
                    paramRockLongTraded.Value = long.Parse(rockLongTraded);
                }
                if (!rockShortTraded.Equals(""))
                {
                    SqlParameter paramRockShortTraded = dbCmd.Parameters.Add("@RockShortTraded", SqlDbType.BigInt);
                    paramRockShortTraded.Value = long.Parse(rockShortTraded);
                }
                if (!jboLongSettled.Equals(""))
                {
                    SqlParameter paramJBOLongSettled = dbCmd.Parameters.Add("@JBOLongSettled", SqlDbType.BigInt);
                    paramJBOLongSettled.Value = long.Parse(jboLongSettled);
                }
                if (!jboLongTraded.Equals(""))
                {
                    SqlParameter paramJBOLongTraded = dbCmd.Parameters.Add("@JBOLongTraded", SqlDbType.BigInt);
                    paramJBOLongTraded.Value = long.Parse(jboLongTraded);
                }
                if (!jboShortSettled.Equals(""))
                {
                    SqlParameter paramJBOShortSettled = dbCmd.Parameters.Add("@JBOShortSettled", SqlDbType.BigInt);
                    paramJBOShortSettled.Value = long.Parse(jboShortSettled);
                }
                if (!jboShortTraded.Equals(""))
                {
                    SqlParameter paramJBOShortTraded = dbCmd.Parameters.Add("@JBOShortTraded", SqlDbType.BigInt);
                    paramJBOShortTraded.Value = long.Parse(jboShortTraded);
                }

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
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

    } 
}
