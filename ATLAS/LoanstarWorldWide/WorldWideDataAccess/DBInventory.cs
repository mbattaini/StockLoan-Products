using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBInventory
    {
		private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet DesksGet(string desk, string bookGroup, bool isNotSubscriber)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDesksGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!desk.Equals(""))
                {
                    SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
                    paramDesk.Value = desk;
                }
                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                SqlParameter paramIsNotSubscriber = dbCmd.Parameters.Add("@IsNotSubscriber", SqlDbType.Bit);
                paramIsNotSubscriber.Value = isNotSubscriber;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Desks");

            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void DeskSet(string desk, string firmCode, string deskTypeCode, string countryCode)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {

                SqlCommand dbCmd = new SqlCommand("dbo.spDeskSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
                paramDesk.Value = desk;

                SqlParameter paramFirmCode = dbCmd.Parameters.Add("@FirmCode", SqlDbType.VarChar, 10);
                paramFirmCode.Value = firmCode;

                SqlParameter paramDeskTypeCode = dbCmd.Parameters.Add("@DeskTypeCode", SqlDbType.VarChar, 3);
                paramDeskTypeCode.Value = deskTypeCode;

                SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                paramCountryCode.Value = countryCode;

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

        public static DataSet DeskTypesGet(string deskTypeCode, string deskType, string sIsActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDeskTypesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!deskTypeCode.Equals(""))
                {
                    SqlParameter paramDeskTypeCode = dbCmd.Parameters.Add("@DeskTypeCode", SqlDbType.VarChar, 3);
                    paramDeskTypeCode.Value = deskTypeCode;
                }
                if (!deskType.Equals(""))
                {
                    SqlParameter paramDeskType = dbCmd.Parameters.Add("@DeskType", SqlDbType.VarChar, 50);
                    paramDeskType.Value = deskType;
                }
                if (!sIsActive.Equals(""))
                {
                    SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                    paramIsActive.Value = short.Parse(sIsActive);
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "DeskTypes");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet FirmsGet(string firmCode)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spFirmsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!firmCode.Equals(""))
                {
                    SqlParameter paramFirmCode = dbCmd.Parameters.Add("@FirmCode", SqlDbType.VarChar, 10);
                    paramFirmCode.Value = firmCode;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Firms");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void FirmSet(string firmCode, string firm, bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spFirmSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramFirmCode = dbCmd.Parameters.Add("@FirmCode", SqlDbType.VarChar, 5);
                paramFirmCode.Value = firmCode;

                SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.VarChar, 50);
                paramFirm.Value = firm;

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

        public static DataSet InventoryGet(
            string bizDate, 
            string bookGroup, 
            string desk, 
            string secId, 
            string version, 
            string source, 
            string sourceActor)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsTemp = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("dbo.spInventoryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = DateTime.Parse(bizDate);
                }
                
                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                
                if (!desk.Equals(""))
				{
					SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
					paramDesk.Value = desk;
				}
				
                if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}
				
                if (!version.Equals(""))
				{
					SqlParameter paramVersion = dbCmd.Parameters.Add("@Version", SqlDbType.Int);
					paramVersion.Value = int.Parse(version);
				}
                
                if (!source.Equals(""))
                {
                    SqlParameter paramSource = dbCmd.Parameters.Add("@Source", SqlDbType.VarChar, 25);
                    paramSource.Value = source;
                }

                if (!sourceActor.Equals(""))
                {
                    SqlParameter paramSourceActor = dbCmd.Parameters.Add("@SourceActor", SqlDbType.VarChar, 50);
                    paramSourceActor.Value = sourceActor;
                }

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsTemp, "Inventory");
			}
			catch
			{
				throw;
			}

			return dsTemp;		
		}

        public static DataSet InventoryHistoryGet(           
           string bookGroup,         
           string secId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spInventoryHistoryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

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
                dataAdapter.Fill(dsTemp, "InventoryHistory");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }


        public static void InventoryItemSet(
            string bizDate, 
            string bookGroup, 
            string desk, 
            string secId, 
            string quantity, 
            string rate, 
            string source, 
            string sourceActor)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
                SqlCommand dbCmd = new SqlCommand("dbo.spInventoryItemSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

				SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
				paramDesk.Value = desk;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

                if (!quantity.Equals(""))
                {
                    SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                    paramQuantity.Value = quantity;
                }
            
                if (!rate.Equals(""))
				{
					SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
					paramRate.Value = decimal.Parse(rate);
				}

				SqlParameter paramSource = dbCmd.Parameters.Add("@Source", SqlDbType.VarChar, 25);
				paramSource.Value = source;

				SqlParameter paramSourceActor = dbCmd.Parameters.Add("@SourceActor", SqlDbType.VarChar, 50);
				paramSourceActor.Value = sourceActor;

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

        public static DataSet InventoryFileLayoutGet(string bookGroup, string desk, string inventoryType)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spInventoryFileLayoutGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                if (!desk.Equals(""))
                {
                    SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
                    paramDesk.Value = desk;
                }
                if (!inventoryType.Equals(""))
                {
                    SqlParameter paramInventoryType = dbCmd.Parameters.Add("@InventoryType", SqlDbType.VarChar, 1);
                    paramInventoryType.Value = inventoryType;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "InventoryFileLayouts");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void InventoryFileLayoutSet(
            string bookGroup, 
            string desk, 
            string inventoryType, 
            string recordLength, 
            string headerFlag,
            string dataFlag, 
            string trailerFlag, 
            string delimiter, 
            string accountLocale,
            string accountOrdinal,
            string accountPosition,
            string accountLength,
            string secIdOrdinal,
            string secIdPosition,
            string secIdLength,
            string quantityOrdinal,
            string quantityPosition,
            string quantityLength,
            string rateOrdinal,
            string ratePosition,
            string rateLength,
            string recordCountOrdinal,
            string recordCountPosition,
            string recordCountLength, 
            string bizDateDD, 
            string bizDateMM, 
            string bizDateYY, 
            string actor)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spInventoryFileLayoutSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
                paramDesk.Value = desk;

                SqlParameter paramInventoryType = dbCmd.Parameters.Add("@InventoryType", SqlDbType.VarChar, 1);
                paramInventoryType.Value = inventoryType;

                if (!recordLength.Equals(""))
                {
                    SqlParameter paramRecordLength = dbCmd.Parameters.Add("@RecordLength", SqlDbType.SmallInt);
                    paramRecordLength.Value = short.Parse(recordLength);
                }
                
                if (!headerFlag.Equals(""))
                {
                    SqlParameter paramHeaderFlag = dbCmd.Parameters.Add("@HeaderFlag", SqlDbType.VarChar, 10);
                    paramHeaderFlag.Value = headerFlag;
                }
                
                if (!dataFlag.Equals(""))
                {
                    SqlParameter paramDataFlag = dbCmd.Parameters.Add("@DataFlag", SqlDbType.VarChar, 10);
                    paramDataFlag.Value = dataFlag;
                }
                
                if (!trailerFlag.Equals(""))
                {
                    SqlParameter paramTrailerFlag = dbCmd.Parameters.Add("@TrailerFlag", SqlDbType.VarChar, 10);
                    paramTrailerFlag.Value = trailerFlag;
                }
                
                if (!delimiter.Equals(""))
                {
                    SqlParameter paramDelimiter = dbCmd.Parameters.Add("@Delimiter", SqlDbType.VarChar, 1);
                    paramDelimiter.Value = delimiter;
                }
                
                if (!accountLocale.Equals(""))
                {
                    SqlParameter paramAccountLocale = dbCmd.Parameters.Add("@AccountLocale", SqlDbType.SmallInt);
                    paramAccountLocale.Value = short.Parse(accountLocale);
                }
                
                if (!accountOrdinal.Equals(""))
                {
                    SqlParameter paramAccountOrdinal = dbCmd.Parameters.Add("@AccountOrdinal", SqlDbType.SmallInt);
                    paramAccountOrdinal.Value = short.Parse(accountOrdinal);
                }
                
                if (!accountPosition.Equals(""))
                {
                    SqlParameter paramAccountPosition = dbCmd.Parameters.Add("@AccountPosition", SqlDbType.SmallInt);
                    paramAccountPosition.Value = short.Parse(accountPosition);
                }
                
                if (!accountLength.Equals(""))
                {
                    SqlParameter paramAccountLength = dbCmd.Parameters.Add("@AccountLength", SqlDbType.SmallInt);
                    paramAccountLength.Value = short.Parse(accountLength);
                }
                
                if (!secIdOrdinal.Equals(""))
                {
                    SqlParameter paramSecIdOrdinal = dbCmd.Parameters.Add("@SecIdOrdinal", SqlDbType.SmallInt);
                    paramSecIdOrdinal.Value = short.Parse(secIdOrdinal);
                }
                
                if (!secIdPosition.Equals(""))
                {
                    SqlParameter paramSecIdPosition = dbCmd.Parameters.Add("@SecIdPosition", SqlDbType.SmallInt);
                    paramSecIdPosition.Value = short.Parse(secIdPosition);
                }
                
                if (!secIdLength.Equals(""))
                {
                    SqlParameter paramSecIdLength = dbCmd.Parameters.Add("@SecIdLength", SqlDbType.SmallInt);
                    paramSecIdLength.Value = short.Parse(secIdLength);
                }
                
                if (!quantityOrdinal.Equals(""))
                {
                    SqlParameter paramQuantityOrdinal = dbCmd.Parameters.Add("@QuantityOrdinal", SqlDbType.SmallInt);
                    paramQuantityOrdinal.Value = short.Parse(quantityOrdinal);
                }
                
                if (!quantityPosition.Equals(""))
                {
                    SqlParameter paramQuantityPosition = dbCmd.Parameters.Add("@QuantityPosition", SqlDbType.SmallInt);
                    paramQuantityPosition.Value = short.Parse(quantityPosition);
                }
                
                if (!quantityLength.Equals(""))
                {
                    SqlParameter paramQuantityLength = dbCmd.Parameters.Add("@QuantityLength", SqlDbType.SmallInt);
                    paramQuantityLength.Value = short.Parse(quantityLength);
                }
                
                if (!rateOrdinal.Equals(""))
                {
                    SqlParameter paramRateOrdinal = dbCmd.Parameters.Add("@RateOrdinal", SqlDbType.SmallInt);
                    paramRateOrdinal.Value = short.Parse(rateOrdinal);
                }
                
                if (!ratePosition.Equals(""))
                {
                    SqlParameter paramRatePosition = dbCmd.Parameters.Add("@RatePosition", SqlDbType.SmallInt);
                    paramRatePosition.Value = short.Parse(ratePosition);
                }
                
                if (!rateLength.Equals(""))
                {
                    SqlParameter paramRateLength = dbCmd.Parameters.Add("@RateLength", SqlDbType.SmallInt);
                    paramRateLength.Value = short.Parse(rateLength);
                }
                
                if (!recordCountOrdinal.Equals(""))
                {
                    SqlParameter paramRecordCountOrdinal = dbCmd.Parameters.Add("@RecordCountOrdinal", SqlDbType.SmallInt);
                    paramRecordCountOrdinal.Value = short.Parse(recordCountOrdinal);
                }
                
                if (!recordCountPosition.Equals(""))
                {
                    SqlParameter paramRecordCountPosition = dbCmd.Parameters.Add("@RecordCountPosition", SqlDbType.SmallInt);
                    paramRecordCountPosition.Value = short.Parse(recordCountPosition);
                }
                
                if (!recordCountLength.Equals(""))
                {
                    SqlParameter paramRecordCountLength = dbCmd.Parameters.Add("@RecordCountLength", SqlDbType.SmallInt);
                    paramRecordCountLength.Value = short.Parse(recordCountLength);
                }
                
                if (!bizDateDD.Equals(""))
                {
                    SqlParameter paramBizDateDD = dbCmd.Parameters.Add("@BizDateDD", SqlDbType.SmallInt);
                    paramBizDateDD.Value = short.Parse(bizDateDD);
                }
                
                if (!bizDateMM.Equals(""))
                {
                    SqlParameter paramBizDateMM = dbCmd.Parameters.Add("@BizDateMM", SqlDbType.SmallInt);
                    paramBizDateMM.Value = short.Parse(bizDateMM);
                }
                
                if (!bizDateYY.Equals(""))
                {
                    SqlParameter paramBizDateYY = dbCmd.Parameters.Add("@BizDateYY", SqlDbType.SmallInt);
                    paramBizDateYY.Value = short.Parse(bizDateYY);
                }
                
                SqlParameter paramActor = dbCmd.Parameters.Add("@Actor", SqlDbType.VarChar, 50);
                paramActor.Value = actor;

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

        public static DataSet InventoryRatesGet(
            string bizDate, 
            string bookGroup, 
            string desk, 
            string secId, 
            string version, 
            string source,
            string sourceActor)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spInventoryRatesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = DateTime.Parse(bizDate);
                }
                
                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                
                if (!desk.Equals(""))
                {
                    SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
                    paramDesk.Value = desk;
                }
                
                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }
                
                if (!version.Equals(""))
                {
                    SqlParameter paramVersion = dbCmd.Parameters.Add("@Version", SqlDbType.Int);
                    paramVersion.Value = int.Parse(version);
                }
                
                if (!source.Equals(""))
                {
                    SqlParameter paramSource = dbCmd.Parameters.Add("@Source", SqlDbType.VarChar, 25);
                    paramSource.Value = source;
                }
                
                if (!sourceActor.Equals(""))
                {
                    SqlParameter paramSourceActor = dbCmd.Parameters.Add("@SourceActor", SqlDbType.VarChar, 50);
                    paramSourceActor.Value = sourceActor;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "InventoryRates");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet InventorySubscriptionsGet(string bookGroup, string desk, string inventoryType, short utcOffSet)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spInventorySubscriptionsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                
                if (!desk.Equals(""))
                {
                    SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                    paramDesk.Value = desk;
                }

                if (!inventoryType.Equals(""))
                {
                    SqlParameter paramInventoryType = dbCmd.Parameters.Add("@InventoryType", SqlDbType.VarChar, 1);
                    paramInventoryType.Value = inventoryType;
                }

                SqlParameter paramUtcOffSet = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffSet.Value = utcOffSet;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "InventorySubscriptions");

            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void InventorySubscriptionSet(
            string bookGroup, 
            string desk, 
            string inventoryType, 
            string bizDate, 
            string loadTime,
            string loadStatus,
            string items,
            string lastLoadedTime,
            string lastLoadedVersion,
            string loadBizDatePrior,
            string fileTime,
            string fileChecktime,
            string fileStatus,
            string fileName,
            string fileHost,
            string fileUserId,
            string filePassword,
            string mailAddress,
            string mailSubject,
            string comment,
            string actor,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spInventorySubscriptionSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 25);
                paramDesk.Value = desk;

                SqlParameter paramInventoryType = dbCmd.Parameters.Add("@InventoryType", SqlDbType.Char, 1);
                paramInventoryType.Value = inventoryType;

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
                
                if (!loadStatus.Equals(""))
                {
                    SqlParameter paramLoadStatus = dbCmd.Parameters.Add("@LoadStatus", SqlDbType.VarChar, 200);
                    paramLoadStatus.Value = loadStatus;
                }
                
                if (!items.Equals(""))
                {
                    SqlParameter paramItems = dbCmd.Parameters.Add("@Items", SqlDbType.BigInt);
                    paramItems.Value = int.Parse(items);
                }
                
                if (!lastLoadedTime.Equals(""))
                {
                    SqlParameter paramLastLoadedTime = dbCmd.Parameters.Add("@LastLoadedTime", SqlDbType.DateTime);
                    paramLastLoadedTime.Value = DateTime.Parse(lastLoadedTime);
                }
                
                if (!lastLoadedVersion.Equals(""))
                {
                    SqlParameter paramLastLoadedVersion = dbCmd.Parameters.Add("@LastLoadedVersion", SqlDbType.Int);
                    paramLastLoadedVersion.Value = int.Parse(lastLoadedVersion);
                }
                
                if (!loadBizDatePrior.Equals(""))
                {
                    SqlParameter paramLoadBizDatePrior = dbCmd.Parameters.Add("@LoadBizDatePrior", SqlDbType.Bit);
                    paramLoadBizDatePrior.Value = short.Parse(loadBizDatePrior);
                }
                
                if (!fileTime.Equals(""))
                {
                    SqlParameter paramFileTime = dbCmd.Parameters.Add("@FileTime", SqlDbType.DateTime);
                    paramFileTime.Value = DateTime.Parse(fileTime);
                }
                
                if (!fileChecktime.Equals(""))
                {
                    SqlParameter paramFileCheckTime = dbCmd.Parameters.Add("@FileCheckTime", SqlDbType.DateTime);
                    paramFileCheckTime.Value = DateTime.Parse(fileChecktime);
                }
                
                if (!fileStatus.Equals(""))
                {
                    SqlParameter paramFileStatus = dbCmd.Parameters.Add("@FileStatus", SqlDbType.VarChar, 100);
                    paramFileStatus.Value = fileStatus;
                }
                
                if (!fileName.Equals(""))
                {
                    SqlParameter paramFileName = dbCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 200);
                    paramFileName.Value = fileName;
                }
                
                if (!fileHost.Equals(""))
                {
                    SqlParameter paramFileHost = dbCmd.Parameters.Add("@FileHost", SqlDbType.VarChar, 100);
                    paramFileHost.Value = fileHost;
                }
                
                if (!fileUserId.Equals(""))
                {
                    SqlParameter paramFileUserId = dbCmd.Parameters.Add("@FileUserId", SqlDbType.VarChar, 50);
                    paramFileUserId.Value = fileUserId;
                }
                
                if (!filePassword.Equals(""))
                {
                    SqlParameter paramFilePassword = dbCmd.Parameters.Add("@FilePassword", SqlDbType.VarChar, 50);
                    paramFilePassword.Value = filePassword;
                }
                
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
                
                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.Char, 200);
                    paramComment.Value = comment;
                }
                
                SqlParameter paramActor = dbCmd.Parameters.Add("@Actor", SqlDbType.Char, 50);
                paramActor.Value = actor;

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
    } 
}
