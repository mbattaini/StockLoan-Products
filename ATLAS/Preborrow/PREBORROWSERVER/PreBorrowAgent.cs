using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using StockLoan.Common;

namespace StockLoan.PreBorrow
{
  public class PreBorrowAgent : MarshalByRefObject, IPreBorrow
  {
    private string dbCnStr;

    public PreBorrowAgent(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;
    }

    public string BizDate()
    {
      return Master.BizDate;
    }

    public string KeyValueGet(string keyId, string keyValue)
    {
      return KeyValue.Get(keyId, keyValue, dbCnStr);
    }

    public void KeyValueSet(string keyId, string keyValue)
    {
      KeyValue.Set(keyId, keyValue, dbCnStr);
    }

    public void PreBorrowStartOfDaySnapShot()
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBStartOfDaySnapShot", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
     
        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = Master.BizDate;
        
        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        Log.Write("Ran start of day preborrow snapshot for " + Master.BizDate + " . [PreBorrow.PreBorrowStartOfDaySnapShot]", 3);
        KeyValue.Set("PreBorrowBillingSnapShotBizDate", Master.BizDate, dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowStartOfDaySnapShot]", Log.Error, 1);
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

    public DataSet PreBorrowRecordGet(string identity, string startDate, string stopDate, string enterTime, string groupCode, string secId, short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBRecordGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;


        SqlParameter paramIdentity = dbCmd.Parameters.Add("@Identity", SqlDbType.BigInt);
        if (!identity.Equals(""))
        {
          paramIdentity.Value = identity;
        }

        SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
        if (!startDate.Equals(""))
        {
          paramStartDate.Value = startDate;
        }


        SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
        if (!stopDate.Equals(""))
        {
          paramStopDate.Value = stopDate;
        }

        SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
        if (!groupCode.Equals(""))
        {
          paramGroupCode.Value = groupCode;
        }

        SqlParameter paramEnterTime = dbCmd.Parameters.Add("@OpenTime", SqlDbType.DateTime);
        if (!enterTime.Equals(""))
        {
          paramEnterTime.Value = enterTime;
        }

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        if (!secId.Equals(""))
        {
          paramSecId.Value = secId;
        }

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "PreBorrowRecords");

        dataSet.Tables["PreBorrowRecords"].PrimaryKey = new DataColumn[1]
			{
				dataSet.Tables["PreBorrowRecords"].Columns["Identity"]
			};
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowRecordGet]", Log.Error, 1);
        throw;
      }

      return dataSet;      
    }

    public void PreBorrowRecordSet(
      string identity,
      string bizDate, 
      string openTime,
      string groupCode, 
      string secId, 
      string tradeDateShortQuantity,
      string requestedQuantity, 
      string coveredQuantity, 
      string coveredAmount, 
      string rate, 
      string modifiedRate, 
      string charge, 
      string  modifiedCharge, 
      string contactName, 
      string contactPhoneNumber, 
      string contactEmailAddress,       
      string actUserId, 
      bool isContacted)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBRecordSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramIdentity = dbCmd.Parameters.Add("@Identity", SqlDbType.BigInt);
        paramIdentity.Value = identity;
        
        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = bizDate;

        SqlParameter paramOpenTime = dbCmd.Parameters.Add("@OpenTime", SqlDbType.DateTime);
        paramOpenTime.Value = openTime;

        SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
        paramGroupCode.Value = groupCode;

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        paramSecId.Value = secId;

        SqlParameter paramTradeDateShortQuantity = dbCmd.Parameters.Add("@TradeDateShortQuantity", SqlDbType.BigInt);
        paramTradeDateShortQuantity.Value = tradeDateShortQuantity;

        SqlParameter paramRequestedQuantity = dbCmd.Parameters.Add("@RequestedQuantity", SqlDbType.BigInt);
        paramRequestedQuantity.Value = requestedQuantity;

        SqlParameter paramCoveredQuantity = dbCmd.Parameters.Add("@CoveredQuantity", SqlDbType.BigInt);
        paramCoveredQuantity.Value = coveredQuantity;

        SqlParameter paramCoveredAmount = dbCmd.Parameters.Add("@CoveredAmount", SqlDbType.Decimal);
        paramCoveredAmount.Value = coveredAmount;

        SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
        paramRate.Value = rate;

        SqlParameter paramModifiedRate = dbCmd.Parameters.Add("@ModifiedRate", SqlDbType.Decimal);
        paramModifiedRate.Value = modifiedRate;

        SqlParameter paramCharge = dbCmd.Parameters.Add("@Charge", SqlDbType.Decimal);
        paramCharge.Value = charge;

        SqlParameter paramModifiedCharge = dbCmd.Parameters.Add("@ModifiedCharge", SqlDbType.Decimal);
        paramModifiedCharge.Value = modifiedCharge;

        SqlParameter paramContactName = dbCmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 50);
        paramContactName.Value = contactName;

        SqlParameter paramContactPhoneNumber = dbCmd.Parameters.Add("@ContactPhoneNumber", SqlDbType.VarChar, 50);
        paramContactPhoneNumber.Value = contactPhoneNumber;

        SqlParameter paramContactEmailAddress = dbCmd.Parameters.Add("@ContactEmailAddress", SqlDbType.VarChar, 50);
        paramContactEmailAddress.Value = contactEmailAddress;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        SqlParameter paramIsContacted = dbCmd.Parameters.Add("@IsContacted", SqlDbType.Bit);
        paramIsContacted.Value = isContacted;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        Log.Write("Set a preborrow for " + secId + " . [PreBorrow.PreBorrowRecordSet]", 3);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowRecordSet]", Log.Error, 1);
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

    public DataSet PreBorrowGroupCodeMarkupGet(string groupCode, short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBGroupCodeMarkupGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        
        SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
        if (!groupCode.Equals(""))
        {
          paramGroupCode.Value = groupCode;
        }

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Marks");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowGroupCodeMarkupGet]", Log.Error, 1);
        throw;
      }

      return dataSet; 
    }

    public void PreBorrowGroupCodeMarkupSet(string groupCode, string markup, string actUserId, bool isActive)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBGroupCodeMarkUpSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
    
        SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
        paramGroupCode.Value = groupCode;

        SqlParameter paramMarkup = dbCmd.Parameters.Add("@Markup", SqlDbType.Decimal);
        paramMarkup.Value = markup;
         
        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        Log.Write("Submit a markup  for " + groupCode + ". [PreBorrow.PreBorrowGroupCodeMarkupSet]", 3);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowGroupCodeMarkupSet]", Log.Error, 1);
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

    public void PreBorrowRequest(string groupCode, string secId, long requestedQuantity, string contactName, string contactPhoneNumber, string contactEmailAddress, string actUserId)
    {
      string firstName = "";
      string lastName = "";

      if (contactName.IndexOf(' ') > -1)
      {
        firstName = contactName.Substring(0, contactName.IndexOf(' ')).Trim();
        lastName = contactName.Substring(contactName.IndexOf(' ') , (contactName.Length - firstName.Length)).Trim();

        PreBorrowContactSet(groupCode, firstName, lastName, contactPhoneNumber, contactEmailAddress, actUserId, true);
      }           
      
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBRequest", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = Master.BizDate;

        SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
        paramGroupCode.Value = groupCode;

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        paramSecId.Value = secId;

        SqlParameter paramRequestedQuantity = dbCmd.Parameters.Add("@RequestedQuantity", SqlDbType.BigInt);
        paramRequestedQuantity.Value = requestedQuantity;

        SqlParameter paramContactName = dbCmd.Parameters.Add("@ContactName", SqlDbType.VarChar, 50);
        paramContactName.Value = contactName;

        SqlParameter paramContactPhoneNumber = dbCmd.Parameters.Add("@ContactPhoneNumber", SqlDbType.VarChar, 50);
        paramContactPhoneNumber.Value = contactPhoneNumber;

        SqlParameter paramContactEmailAddress = dbCmd.Parameters.Add("@ContactEmailAddress", SqlDbType.VarChar, 50);
        paramContactEmailAddress.Value = contactEmailAddress;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        Log.Write("Submit a preborrow for " + secId + " . [PreBorrow.PreBorrowRequest]", 3);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowRequest]", Log.Error, 1);
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


    public DataSet PreBorrowContactGet(string groupCode, short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBContactsGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
        if (!groupCode.Equals(""))
        {
          paramGroupCode.Value = groupCode;
        }

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Contacts");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowContactGet]", Log.Error, 1);
        throw;
      }

      return dataSet; 
    }

    public void PreBorrowContactSet(string groupCode, string firstName, string lastName, string phoneNumber, string emailAddress, string actUserId, bool isActive)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spPBContactsSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
   
        SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
        paramGroupCode.Value = groupCode;

        SqlParameter paramFirstName = dbCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 24);
        paramFirstName.Value = firstName;


        SqlParameter paramLastName = dbCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 25);
        paramLastName.Value = lastName;
 
        SqlParameter paramContactPhoneNumber = dbCmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50);
        paramContactPhoneNumber.Value = phoneNumber;

        SqlParameter paramContactEmailAddress = dbCmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50);
        paramContactEmailAddress.Value = emailAddress;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        Log.Write("Submit a contact for " + groupCode + " . [PreBorrow.PreBorrowContactSet]", 3);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.PreBorrowContactSet]", Log.Error, 1);
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


    public DataSet SecMasterLookup(string secId, bool withBox, bool withDeskQuips, short utcOffset, string since)
    {
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
        Log.Write(e.Message + " [PreBorrowAgent.SecMasterLookup]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public DataSet InventoryGet(string secId, short utcOffset, bool withHistory)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spInventoryGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        paramSecId.Value = secId;

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Inventory");

        if (withHistory)
        {
          if (dataSet.Tables["Inventory"].Rows.Count > 0) // Switch to the resolved SecId.
          {
            paramSecId.Value = dataSet.Tables["Inventory"].Select()[0]["SecId"];
          }

          dbCmd.Parameters.Remove(paramUtcOffset);

          dbCmd.CommandText = "spInventoryHistoryGet";
          dataAdapter.Fill(dataSet, "History");
        }
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.InventoryGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public DataSet ContractDataGet(short utcOffset, string bizDate, string userId, string functionPath)
    {
      SqlConnection dbCn = null;
      SqlCommand dbCmd = null;

      SqlDataAdapter dataAdapter;
      SqlParameter paramUserId = null;
      SqlParameter paramFunctionPath = null;

      DataSet dataSet = new DataSet();

      try
      {
        dbCn = new SqlConnection(dbCnStr);
        dbCmd = new SqlCommand("spContractGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        dataAdapter = new SqlDataAdapter(dbCmd);

        if (bizDate == null)
        {
          dataAdapter.Fill(dataSet, "Contracts");

          dbCmd.CommandText = "spContractBizDateList";
          dataAdapter.Fill(dataSet, "BizDates");

          if (!userId.Equals(""))
          {
            paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
            paramUserId.Value = userId;

            paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
            paramFunctionPath.Value = functionPath;
          }

          dbCmd.CommandText = "spBookGroupGet";
          dataAdapter.Fill(dataSet, "BookGroups");

          if (!userId.Equals(""))
          {
            dbCmd.Parameters.Remove(paramUserId);
            dbCmd.Parameters.Remove(paramFunctionPath);
          }

          SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
          paramUtcOffset.Value = utcOffset;

          dbCmd.CommandText = "spBooksGet";
          dataAdapter.Fill(dataSet, "Books");

          dataSet.Tables["Books"].PrimaryKey = new DataColumn[2]
			{
				dataSet.Tables["Books"].Columns["BookGroup"],
				dataSet.Tables["Books"].Columns["Book"]
			};
        }
        else
        {
          SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
          paramBizDate.Value = bizDate;

          dataAdapter.Fill(dataSet, "Contracts");

          dbCmd.Parameters.Remove(paramBizDate);

          if (!userId.Equals(""))
          {
            paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
            paramUserId.Value = userId;

            paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
            paramFunctionPath.Value = functionPath;
          }

          dbCmd.CommandText = "spBookGroupGet";
          dataAdapter.Fill(dataSet, "BookGroups");

          if (!userId.Equals(""))
          {
            dbCmd.Parameters.Remove(paramUserId);
            dbCmd.Parameters.Remove(paramFunctionPath);
          }

          dbCmd.CommandText = "spBooksGet";
          dataAdapter.Fill(dataSet, "Books");

          dataSet.Tables["Books"].PrimaryKey = new DataColumn[2]
			{
				dataSet.Tables["Books"].Columns["BookGroup"],
				dataSet.Tables["Books"].Columns["Book"]
			};
        }

        dataSet.Tables["Contracts"].PrimaryKey = new DataColumn[4]
			{
				dataSet.Tables["Contracts"].Columns["BizDate"],
				dataSet.Tables["Contracts"].Columns["BookGroup"],
				dataSet.Tables["Contracts"].Columns["ContractId"],
				dataSet.Tables["Contracts"].Columns["ContractType"]
			};
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PreBorrowAgent.ContractDataGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public DataSet TradingGroupsGet(string tradeDate,  short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();  

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				if (!tradeDate.Equals(""))
				{
					SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);			
					paramTradeDate.Value = tradeDate;
				}
				else
				{
					SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);			
					paramTradeDate.Value = Master.BizDateExchange;
				}
      	
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
				paramUtcOffset.Value = utcOffset;
						
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "TradingGroups");

        Log.Write("Returning a 'TradingGroups' table with " + dataSet.Tables["TradingGroups"].Rows.Count + " rows. [PreBorrowAgent.TradingGroupsGet]", 3);
			}
			catch (Exception e)
			{
        Log.Write(e.Message + " [PreBorrowAgent.TradingGroupsGet]", Log.Error, 1);
        throw;
      }

			return dataSet;
		}
    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
