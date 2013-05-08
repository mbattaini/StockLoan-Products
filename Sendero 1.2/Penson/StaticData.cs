using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Penson
{
  public class StaticData
  {
    private string bizDate;
    private string bizDatePrior;
    private string contractsBizDate;

    private string dbCnStr = "";
    private SqlConnection dbCn = null;
    
    private string dbCnStrPenson = "";
    private SqlConnection dbCnPenson = null;

    public StaticData(SqlConnection dbCn, string dbCnStr, SqlConnection dbCnPenson, string dbCnStrPenson)
    {
      this.dbCnStr = dbCnStr;
      this.dbCn = dbCn;

      this.dbCnStrPenson = dbCnStrPenson;
      this.dbCnPenson = dbCnPenson;

      bizDate = KeyValue.Get("BizDate", "0000-00-00", dbCn);
      bizDatePrior = KeyValue.Get("BizDatePrior", "0000-00-00", dbCn);
      contractsBizDate = KeyValue.Get("ContractsBizDate", "0000-00-00", dbCn);
    }

    public void BoxPositionLoad(string index, string bookGroup, string firm)
    {
      string dayTime;

      if (bizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Today is the business date.
      {
        dayTime = KeyValue.Get("BoxPositionLoadStartTime", "10:00", dbCn);

        if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // Time now is after the start time.
        {
          dayTime = KeyValue.Get("BoxPositionLoadEndTime", "24:00", dbCn);

          if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) > 0) // Time now is before the end time.
          {
            int n = 0;
            SqlDataReader dataReader = null;

            Log.Write("Will load box position data now. [StaticData.BoxPositionLoad]", 2);

            try
            {        
              SqlCommand pensonDbCmd = new SqlCommand(BoxPositionSql(index, firm), dbCnPenson);
              pensonDbCmd.CommandType = CommandType.Text;
              pensonDbCmd.CommandTimeout = int.Parse(KeyValue.Get("BoxPositionLoadTimeout", "300", dbCn));

              SqlCommand dbCmd = new SqlCommand("spBoxPositionStateSet", dbCn);
              dbCmd.CommandType = CommandType.StoredProcedure;

              SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
              paramBizDate.Value = contractsBizDate;

              SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
              paramBookGroup.Value = bookGroup;

              SqlParameter paramRowCount = dbCmd.Parameters.Add("@RowCount", SqlDbType.Int);
              paramRowCount.Direction = ParameterDirection.ReturnValue;

              dbCn.Open();
              dbCmd.ExecuteNonQuery();

              Log.Write("Set " + paramRowCount.Value.ToString() + " items to await update. [StaticData.BoxPositionLoad]", 2);
              
              dbCmd.CommandText = "spBoxPositionItemSet";

              SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
              paramBizDatePrior.Value = bizDatePrior;

              SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
              SqlParameter paramCustomerLongSettled = dbCmd.Parameters.Add("@CustomerLongSettled", SqlDbType.BigInt);
              SqlParameter paramCustomerLongTraded = dbCmd.Parameters.Add("@CustomerLongTraded", SqlDbType.BigInt);
              SqlParameter paramCustomerShortSettled = dbCmd.Parameters.Add("@CustomerShortSettled", SqlDbType.BigInt);
              SqlParameter paramCustomerShortTraded = dbCmd.Parameters.Add("@CustomerShortTraded", SqlDbType.BigInt);
              SqlParameter paramFirmLongSettled = dbCmd.Parameters.Add("@FirmLongSettled", SqlDbType.BigInt);
              SqlParameter paramFirmLongTraded = dbCmd.Parameters.Add("@FirmLongTraded", SqlDbType.BigInt);
              SqlParameter paramFirmShortSettled = dbCmd.Parameters.Add("@FirmShortSettled", SqlDbType.BigInt);
              SqlParameter paramFirmShortTraded = dbCmd.Parameters.Add("@FirmShortTraded", SqlDbType.BigInt);
              SqlParameter paramCustomerPledgeSettled = dbCmd.Parameters.Add("@CustomerPledgeSettled", SqlDbType.BigInt);
              SqlParameter paramCustomerPledgeTraded = dbCmd.Parameters.Add("@CustomerPledgeTraded", SqlDbType.BigInt);
              SqlParameter paramFirmPledgeSettled = dbCmd.Parameters.Add("@FirmPledgeSettled", SqlDbType.BigInt);
              SqlParameter paramFirmPledgeTraded = dbCmd.Parameters.Add("@FirmPledgeTraded", SqlDbType.BigInt);
              SqlParameter paramDvpFailInSettled = dbCmd.Parameters.Add("@DvpFailInSettled", SqlDbType.BigInt);
              SqlParameter paramDvpFailInTraded = dbCmd.Parameters.Add("@DvpFailInTraded", SqlDbType.BigInt);
              SqlParameter paramDvpFailOutSettled = dbCmd.Parameters.Add("@DvpFailOutSettled", SqlDbType.BigInt);
              SqlParameter paramDvpFailOutTraded = dbCmd.Parameters.Add("@DvpFailOutTraded", SqlDbType.BigInt);
              SqlParameter paramBrokerFailInSettled = dbCmd.Parameters.Add("@BrokerFailInSettled", SqlDbType.BigInt);
              SqlParameter paramBrokerFailInTraded = dbCmd.Parameters.Add("@BrokerFailInTraded", SqlDbType.BigInt);
              SqlParameter paramBrokerFailOutSettled = dbCmd.Parameters.Add("@BrokerFailOutSettled", SqlDbType.BigInt);
              SqlParameter paramBrokerFailOutTraded = dbCmd.Parameters.Add("@BrokerFailOutTraded", SqlDbType.BigInt);
              SqlParameter paramClearingFailInSettled = dbCmd.Parameters.Add("@ClearingFailInSettled", SqlDbType.BigInt);
              SqlParameter paramClearingFailInTraded = dbCmd.Parameters.Add("@ClearingFailInTraded", SqlDbType.BigInt);
              SqlParameter paramClearingFailOutSettled = dbCmd.Parameters.Add("@ClearingFailOutSettled", SqlDbType.BigInt);
              SqlParameter paramClearingFailOutTraded = dbCmd.Parameters.Add("@ClearingFailOutTraded", SqlDbType.BigInt);
              SqlParameter paramOtherFailInSettled = dbCmd.Parameters.Add("@OtherFailInSettled", SqlDbType.BigInt);
              SqlParameter paramOtherFailInTraded = dbCmd.Parameters.Add("@OtherFailInTraded", SqlDbType.BigInt);
              SqlParameter paramOtherFailOutSettled = dbCmd.Parameters.Add("@OtherFailOutSettled", SqlDbType.BigInt);
              SqlParameter paramOtherFailOutTraded = dbCmd.Parameters.Add("@OtherFailOutTraded", SqlDbType.BigInt);
							SqlParameter paramSegRequirement = dbCmd.Parameters.Add("@SegReqSettled", SqlDbType.BigInt);

              dbCnPenson.Open();
              dataReader = pensonDbCmd.ExecuteReader();
              dayTime = DateTime.UtcNow.ToString(Standard.DateTimeFileFormat);

              Log.Write("Box position results returned, load commencing. [StaticData.BoxPositionLoad]", 2);

              while (dataReader.Read())
              {
                paramSecId.Value = dataReader.GetValue(0);
                paramCustomerLongSettled.Value = Tools.ParseLong(dataReader.GetValue(1).ToString());
                paramCustomerLongTraded.Value = Tools.ParseLong(dataReader.GetValue(2).ToString());
                paramCustomerShortSettled.Value = Tools.ParseLong(dataReader.GetValue(3).ToString());
                paramCustomerShortTraded.Value = Tools.ParseLong(dataReader.GetValue(4).ToString());
                paramFirmLongSettled.Value = Tools.ParseLong(dataReader.GetValue(5).ToString());
                paramFirmLongTraded.Value = Tools.ParseLong(dataReader.GetValue(6).ToString());
                paramFirmShortSettled.Value = Tools.ParseLong(dataReader.GetValue(7).ToString());
                paramFirmShortTraded.Value = Tools.ParseLong(dataReader.GetValue(8).ToString());
                paramCustomerPledgeSettled.Value = Tools.ParseLong(dataReader.GetValue(9).ToString());
                paramCustomerPledgeTraded.Value = Tools.ParseLong(dataReader.GetValue(10).ToString());
                paramFirmPledgeSettled.Value = Tools.ParseLong(dataReader.GetValue(11).ToString());
                paramFirmPledgeTraded.Value = Tools.ParseLong(dataReader.GetValue(12).ToString());
                paramDvpFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(13).ToString());
                paramDvpFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(14).ToString());
                paramDvpFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(15).ToString());
                paramDvpFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(16).ToString());
                paramBrokerFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(17).ToString());
                paramBrokerFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(18).ToString());
                paramBrokerFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(19).ToString());
                paramBrokerFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(20).ToString());
                paramClearingFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(21).ToString());
                paramClearingFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(22).ToString());
                paramClearingFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(23).ToString());
                paramClearingFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(24).ToString());
                paramOtherFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(25).ToString());
                paramOtherFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(26).ToString());
                paramOtherFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(27).ToString());
                paramOtherFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(28).ToString());
								paramSegRequirement.Value = Tools.ParseLong(dataReader.GetValue(29).ToString());

                dbCmd.ExecuteNonQuery();
          
                if (((++n % 100) == 0) && PensonMain.IsStopped)
                {
                  break;
                }

                if ((n % 5000) == 0)
                {
                  Log.Write("Interim box position insert/update count: " + n + " [StaticData.BoxPositionLoad]", 2);            
                }
              }

              Log.Write("Final box position insert/update count: " + n + " [StaticData.BoxPositionLoad]", 2);            

              dbCmd.CommandText = "spBoxPositionPurge";

              dbCmd.Parameters.Remove(paramBizDatePrior);
              dbCmd.Parameters.Remove(paramSecId);
              dbCmd.Parameters.Remove(paramCustomerLongSettled);
              dbCmd.Parameters.Remove(paramCustomerLongTraded);
              dbCmd.Parameters.Remove(paramCustomerShortSettled);
              dbCmd.Parameters.Remove(paramCustomerShortTraded);
              dbCmd.Parameters.Remove(paramFirmLongSettled);
              dbCmd.Parameters.Remove(paramFirmLongTraded);
              dbCmd.Parameters.Remove(paramFirmShortSettled);
              dbCmd.Parameters.Remove(paramFirmShortTraded);
              dbCmd.Parameters.Remove(paramCustomerPledgeSettled);
              dbCmd.Parameters.Remove(paramCustomerPledgeTraded);
              dbCmd.Parameters.Remove(paramFirmPledgeSettled);
              dbCmd.Parameters.Remove(paramFirmPledgeTraded);
              dbCmd.Parameters.Remove(paramDvpFailInSettled);
              dbCmd.Parameters.Remove(paramDvpFailInTraded);
              dbCmd.Parameters.Remove(paramDvpFailOutSettled);
              dbCmd.Parameters.Remove(paramDvpFailOutTraded);
              dbCmd.Parameters.Remove(paramBrokerFailInSettled);
              dbCmd.Parameters.Remove(paramBrokerFailInTraded);
              dbCmd.Parameters.Remove(paramBrokerFailOutSettled);
              dbCmd.Parameters.Remove(paramBrokerFailOutTraded);
              dbCmd.Parameters.Remove(paramClearingFailInSettled);
              dbCmd.Parameters.Remove(paramClearingFailInTraded);
              dbCmd.Parameters.Remove(paramClearingFailOutSettled);
              dbCmd.Parameters.Remove(paramClearingFailOutTraded);
              dbCmd.Parameters.Remove(paramOtherFailInSettled);
              dbCmd.Parameters.Remove(paramOtherFailInTraded);
              dbCmd.Parameters.Remove(paramOtherFailOutSettled);
              dbCmd.Parameters.Remove(paramOtherFailOutTraded);
							dbCmd.Parameters.Remove(paramSegRequirement);

              dbCmd.ExecuteNonQuery();
              dbCn.Close();

              KeyValue.Set("BoxPositionLoadDateTime", dayTime, dbCn);

              Log.Write("Purged " + paramRowCount.Value.ToString() + " items as no longer in position. [StaticData.BoxPositionLoad]", 2);
            }
            catch (Exception e)
            {
              Log.Write(e.Message + " [StaticData.BoxPositionLoad]", Log.Error, 1);
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

              if (dbCnPenson.State != ConnectionState.Closed)
              {
                dbCnPenson.Close();
              }
            }
          }
          else
          {
            Log.Write("Time now is after " + dayTime + " UTC, will wait until next business day. [StaticData.BoxPositionLoad]", 2);
          }
        }
        else
        {
          Log.Write("Time now is before " + dayTime + " UTC, will wait until later. [StaticData.BoxPositionLoad]", 2);
        }
      }
      else
      {
        Log.Write("Today is not the current business day, will wait. [StaticData.BoxPositionLoad]", 2);
      }
    }

		public void RockPositionLoad()
		{
			// <add key="BoxPositionRockPositions[001]" value="07; 0234; STRD;47010000; 47019999;; O;" />
			
			
			string dayTime,
						 temp = "",
						 correspondentFirm = "",
						 correspondentBookGroup = "",
						 correspondentCode = "",
						 correspondentAccountMin = "",
						 correspondentAccountMax = "",
						 correspondentLocationMemo = "",
						 correspondentLocMemo = "";


			temp = Standard.ConfigValue("BoxPositionRockPositions[001]", "");

			if (bizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Today is the business date.
			{
				dayTime = KeyValue.Get("RockPositionLoadStartTime", "10:00", dbCn);

				if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // Time now is after the start time.
				{
					dayTime = KeyValue.Get("RockPositionLoadEndTime", "24:00", dbCn);

					if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) > 0) // Time now is before the end time.
					{
						Log.Write("Will load rock position data now. [StaticData.RockPositionLoad]", 2);
						
						correspondentFirm = Tools.SplitItem(temp, ";", 0).Trim();
						correspondentBookGroup = Tools.SplitItem(temp, ";", 1).Trim();
						correspondentCode = Tools.SplitItem(temp, ";", 2).Trim();
						correspondentAccountMin =  Tools.SplitItem(temp, ";", 3).Trim();
						correspondentAccountMax =  Tools.SplitItem(temp, ";", 4).Trim();
						correspondentLocationMemo =  Tools.SplitItem(temp, ";", 5).Trim();
						correspondentLocMemo = Tools.SplitItem(temp, ";", 6).Trim();
							
						if (correspondentFirm.Equals(""))
						{
								Log.Write("Correspondent Firm is missing. [StaticData.RockPositionLoad]", 2);
						}
						else if (correspondentBookGroup.Equals(""))
						{
							Log.Write("Correspondent BookGroup is missing. [StaticData.RockPositionLoad]", 2);
						}
						else if (correspondentCode.Equals(""))
						{
							Log.Write("Correspondent Code is missing. [StaticData.RockPositionLoad]", 2);
						}
						else
						{
							try
							{        																				
								SqlCommand dbCmd = new SqlCommand("spRockPositionLoad", dbCn);
								dbCmd.CommandType = CommandType.StoredProcedure;
								dbCmd.CommandTimeout = int.Parse(KeyValue.Get("BoxPositionLoadTimeout", "300", dbCn));
							
								SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
								paramBizDate.Value = bizDate;
								

								SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.VarChar, 2);
								if (!correspondentFirm.Equals(""))
								{
									paramFirm.Value = correspondentFirm;
								}
							
								SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 5);
								if (!correspondentBookGroup.Equals(""))
								{
									paramBookGroup.Value = correspondentBookGroup;
								}

								SqlParameter paramCorrespondentCode = dbCmd.Parameters.Add("@CorrespondentCode", SqlDbType.VarChar, 5);
								if (!correspondentCode.Equals(""))
								{
									paramCorrespondentCode.Value = correspondentCode;
								}

								SqlParameter paramAccountMin = dbCmd.Parameters.Add("@AccountMin", SqlDbType.VarChar, 8);
								if (!correspondentAccountMin.Equals(""))
								{
									paramAccountMin.Value = correspondentAccountMin;
								}
							
								SqlParameter paramAccountMax = dbCmd.Parameters.Add("@AccountMax", SqlDbType.VarChar, 8);
								if (!correspondentAccountMax.Equals(""))
								{
									paramAccountMax.Value = correspondentAccountMax;
								}

								SqlParameter paramLocationMemo = dbCmd.Parameters.Add("@Location", SqlDbType.VarChar, 1);
								if (!correspondentLocationMemo.Equals(""))
								{
									paramLocationMemo.Value = correspondentLocationMemo;
								}

								SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.VarChar, 1);
								if (!correspondentLocMemo.Equals(""))
								{
									paramLocMemo.Value = correspondentLocMemo;
								}

								SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
								paramRecordsUpdated.Direction = ParameterDirection.Output;
							
								dbCn.Open();
								dbCmd.ExecuteNonQuery();


								Log.Write("Final rock position insert/update count: " + paramRecordsUpdated.Value.ToString() + " [StaticData.RockPositionLoad]", 2);            
							
								KeyValue.Set("RockPositionLoadDateTime", dayTime, dbCnStr);

								Log.Write("Purged " + paramRecordsUpdated.Value.ToString() + " items as no longer in position. [StaticData.RockPositionLoad]", 2);
							}
							catch (Exception e)
							{
								Log.Write(e.Message + " [StaticData.RockPositionLoad]", Log.Error, 1);
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
						Log.Write("Time now is after " + dayTime + " UTC, will wait until next business day. [StaticData.RockPositionLoad]", 2);
					}
				}
				else
				{
					Log.Write("Time now is before " + dayTime + " UTC, will wait until later. [StaticData.RockPositionLoad]", 2);
				}
			}
			else
			{
				Log.Write("Today is not the current business day, will wait. [StaticData.RockPositionLoad]", 2);
			}
		}

		public void JBOPositionLoad()
		{
			// <add key="BoxPositionJBOPositions[001]" value="07;0234;C;;(OPUS,TRIL,SPEC);" />
			
			
			string dayTime,
				temp = "",
				correspondentFirm = "",
				correspondentBookGroup = "",
				correspondentCodeList = "",
				correspondentLocationMemo = "",
				correspondentLocMemo = "";


			temp = Standard.ConfigValue("BoxPositionJBOPositions[001]", "");

			if (bizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Today is the business date.
			{
				dayTime = KeyValue.Get("JBOPositionLoadStartTime", "10:00", dbCn);

				if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // Time now is after the start time.
				{
					dayTime = KeyValue.Get("JBOPositionLoadEndTime", "24:00", dbCn);

					if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) > 0) // Time now is before the end time.
					{
						Log.Write("Will load JBO position data now. [StaticData.JBOPositionLoad]", 2);
						
						correspondentFirm = Tools.SplitItem(temp, ";", 0).Trim();
						correspondentBookGroup = Tools.SplitItem(temp, ";", 1).Trim();
						correspondentLocMemo = Tools.SplitItem(temp, ";", 2).Trim();
						correspondentLocationMemo =  Tools.SplitItem(temp, ";", 3).Trim();
						correspondentCodeList = Tools.SplitItem(temp,";", 4).Trim();
						
						
							
						if (correspondentFirm.Equals(""))
						{
							Log.Write("Correspondent Firm is missing. [StaticData.JBOPositionLoad]", 2);
						}
						else if (correspondentBookGroup.Equals(""))
						{
							Log.Write("Correspondent BookGroup is missing. [StaticData.JBOPositionLoad]", 2);
						}
						else if (correspondentCodeList.Equals(""))
						{
							Log.Write("Correspondent Code List is missing. [StaticData.JBOPositionLoad]", 2);
						}
						else
						{
							try
							{        																				
								SqlCommand dbCmd = new SqlCommand("spJBOPositionLoad", dbCn);
								dbCmd.CommandType = CommandType.StoredProcedure;
								dbCmd.CommandTimeout = int.Parse(KeyValue.Get("BoxPositionLoadTimeout", "300", dbCn));
							
								SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
								paramBizDate.Value = bizDate;
								

								SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.VarChar, 2);
								if (!correspondentFirm.Equals(""))
								{
									paramFirm.Value = correspondentFirm;
								}
							
								SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 5);
								if (!correspondentBookGroup.Equals(""))
								{
									paramBookGroup.Value = correspondentBookGroup;
								}

								SqlParameter paramCorrespondentCodeList = dbCmd.Parameters.Add("@CorrespondentCodeList", SqlDbType.VarChar, 255);
								if (!correspondentCodeList.Equals(""))
								{
									paramCorrespondentCodeList.Value = correspondentCodeList;
								}

								SqlParameter paramLocationMemo = dbCmd.Parameters.Add("@Location", SqlDbType.VarChar, 1);
								if (!correspondentLocationMemo.Equals(""))
								{
									paramLocationMemo.Value = correspondentLocationMemo;
								}

								SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.VarChar, 1);
								if (!correspondentLocMemo.Equals(""))
								{
									paramLocMemo.Value = correspondentLocMemo;
								}

								SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
								paramRecordsUpdated.Direction = ParameterDirection.Output;
							
								dbCn.Open();
								dbCmd.ExecuteNonQuery();

								Log.Write("Final JBO position insert/update count: " + paramRecordsUpdated.Value.ToString() + " [StaticData.JBOPositionLoad]", 2);            
							
								KeyValue.Set("JBOPositionLoadDateTime", dayTime, dbCnStr);								
							}
							catch (Exception e)
							{
								Log.Write(e.Message + " [StaticData.JBOPositionLoad]", Log.Error, 1);
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
						Log.Write("Time now is after " + dayTime + " UTC, will wait until next business day. [StaticData.JBOPositionLoad]", 2);
					}
				}
				else
				{
					Log.Write("Time now is before " + dayTime + " UTC, will wait until later. [StaticData.JBOPositionLoad]", 2);
				}
			}
			else
			{
				Log.Write("Today is not the current business day, will wait. [StaticData.JBOPositionLoad]", 2);
			}
		}

    public void BoxLocationLoad()
    {
      string dayTime;

      if (bizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Today is the business date.
      {
        dayTime = KeyValue.Get("BoxLocationLoadStartTime", "10:00", dbCn);

        if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // Time now is after the start time.
        {
          dayTime = KeyValue.Get("BoxLocationLoadEndTime", "22:00", dbCn);

          if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) > 0) // Time now is before the end time.
          {
            string configValue;
            
            int i = 0;
            
            SqlCommand pensonDbCmd = null;
            SqlDataReader dataReader = null;

            Log.Write("Will load box location data now. [StaticData.BoxLocationLoad]", 2);

            try
            {        
              SqlCommand dbCmd = new SqlCommand("spBoxLocationStateSet", dbCn);
              dbCmd.CommandType = CommandType.StoredProcedure;

              SqlParameter paramRowCount = dbCmd.Parameters.Add("@RowCount", SqlDbType.Int);
              paramRowCount.Direction = ParameterDirection.ReturnValue;

              dbCn.Open();
              dbCmd.ExecuteNonQuery();

              dbCmd.CommandText = "spBoxLocationItemSet";
              SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
              SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
              SqlParameter paramCustodian = dbCmd.Parameters.Add("@Custodian", SqlDbType.VarChar, 25);
              SqlParameter paramQuantitySettled = dbCmd.Parameters.Add("@QuantitySettled", SqlDbType.BigInt);
              SqlParameter paramQuantityTraded = dbCmd.Parameters.Add("@QuantityTraded", SqlDbType.BigInt);

              Log.Write("Set " + paramRowCount.Value.ToString() + " items to await update. [StaticData.BoxLocationLoad]", 2);

              while ((configValue = Standard.ConfigValue("BoxLocation[" + (++i).ToString("00#") + "]", "")) != "")            
              {
                int n = 0;

                paramBookGroup.Value = Tools.SplitItem(configValue, ";", 0);
                paramCustodian.Value = Tools.SplitItem(configValue, ";", 2);

                pensonDbCmd = new SqlCommand(BoxLocationSql(Tools.SplitItem(configValue, ";", 1),
                                                            Tools.SplitItem(configValue, ";", 3),
                                                            Tools.SplitItem(configValue, ";", 4),
                                                            Tools.SplitItem(configValue, ";", 5)), dbCnPenson);
                pensonDbCmd.CommandType = CommandType.Text;
                pensonDbCmd.CommandTimeout = int.Parse(KeyValue.Get("BoxLocationLoadTimeout", "300", dbCnStr));

                dbCnPenson.Open();
                dataReader = pensonDbCmd.ExecuteReader();
                dayTime = DateTime.UtcNow.ToString(Standard.DateTimeFileFormat);

                Log.Write("Box location results returned, load commencing for: " + configValue + " [StaticData.BoxLocationLoad]", 2);

                while (dataReader.Read())
                {
                  paramSecId.Value = dataReader.GetValue(0);
                  paramQuantitySettled.Value = Tools.ParseLong(dataReader.GetValue(1).ToString());
                  paramQuantityTraded.Value = Tools.ParseLong(dataReader.GetValue(2).ToString());

                  dbCmd.ExecuteNonQuery();
          
                  if (((++n % 100) == 0) && PensonMain.IsStopped)
                  {
                    break;
                  }

                  if ((n % 5000) == 0)
                  {
                    Log.Write("Interim box location insert/update count: " + n + " [StaticData.BoxLocationLoad]", 2);            
                  }
                }
                dbCnPenson.Close();

                Log.Write("Final box location insert/update count: " + n + " [StaticData.BoxLocationLoad]", 2);            
              }

              dbCmd.CommandText = "spBoxLocationPurge";

              dbCmd.Parameters.Remove(paramBookGroup);
              dbCmd.Parameters.Remove(paramSecId);
              dbCmd.Parameters.Remove(paramCustodian);
              dbCmd.Parameters.Remove(paramQuantitySettled);
              dbCmd.Parameters.Remove(paramQuantityTraded);

              dbCmd.ExecuteNonQuery();
              dbCn.Close();
              
              KeyValue.Set("BoxLocationLoadDateTime", dayTime, dbCn);

              Log.Write("Purged " + paramRowCount.Value.ToString() + " items as no longer at a location. [StaticData.BoxLocationLoad]", 2);
            }
            catch (Exception e)
            {
              Log.Write(e.Message + " [StaticData.BoxLocationLoad]", Log.Error, 1);
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

              if (dbCnPenson.State != ConnectionState.Closed)
              {
                dbCnPenson.Close();
              }
            }
          }
          else
          {
            Log.Write("Time now is after " + dayTime + " UTC, will wait until next business day. [StaticData.BoxLocationLoad]", 2);
          }
        }
        else
        {
          Log.Write("Time now is before " + dayTime + " UTC, will wait until later. [StaticData.BoxLocationLoad]", 2);
        }
      }
      else
      {
        Log.Write("Today is not the current business day, will wait. [StaticData.BoxLocationLoad]", 2);
      }
    }

		public void SecurityDataLoad()
		{
			string waitUntil;

			if (KeyValue.Get("PensonSecurityStaticBizDate", "", dbCn).Equals(bizDatePrior)) // Already done for today.
			{
				Log.Write("Penson security static data already loaded for " + bizDatePrior + ". [StaticData.SecurityDataLoad]", 2);       
				return;
			}

			if (bizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Calendar date is business date.
			{
				waitUntil = KeyValue.Get("PensonSecurityStaticWaitUntil", "10:00", dbCn);

				if (waitUntil.CompareTo(DateTime.UtcNow.ToString("HH:mm")) > 0) // Too early.
				{
					Log.Write("Penson security static data will load after " + waitUntil + " UTC. [StaticData.SecurityDataLoad]", 2);       
					return;
				}
			}
			else
			{
				Log.Write("Penson security static data will load tomorrow. [StaticData.SecurityDataLoad]", 2);       
				return;
			}

     
			SqlCommand dbCmd = new SqlCommand("spSecurityDataLoad", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;
			dbCmd.CommandTimeout = int.Parse(KeyValue.Get("SecurityDataLoadTimeout", "300", dbCn));
			
			SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
			paramBizDate.Value = bizDatePrior;

			SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
			paramRecordsUpdated.Direction = ParameterDirection.Output;

			Log.Write("Will load Security static data for " + bizDatePrior + ". [StaticData.SecurityDataLoad]", 2);

			try
			{                
				dbCn.Open();

				dbCmd.ExecuteNonQuery();
          
				Log.Write("Final security update count: " + paramRecordsUpdated.Value.ToString() + " [StaticData.SecurityDataLoad]", 2);            
        
				KeyValue.Set("PensonSecurityStaticBizDate", bizDatePrior, dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [StaticData.SecurityDataLoad]", Log.Error, 1);
			}
			finally
			{			  
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

    public void CorrespondentDataLoad()
    {
      if (KeyValue.Get("PensonCorrespondentStaticBizDate", "", dbCn).Equals(bizDatePrior)) // Already done for today.
      {
        Log.Write("Penson correspondent static data already loaded for " + bizDatePrior + ". [StaticData.CorrespondentDataLoad]", 2);       
        return;
      }

      int n = 0;
      SqlDataReader dataReader = null;

			Log.Write("Will load Correspondent static data for " + bizDatePrior + ". [StaticData.CorrespondentDataLoad]", 2);

      try
      {        
        dbCnPenson.Open();       

        SqlCommand dbCmd = new SqlCommand("spTradingGroupReset", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        dbCmd.CommandText = "spCorrespondentDataLoad";
				dbCmd.CommandType = CommandType.StoredProcedure;
					
				dbCmd.ExecuteNonQuery();

				dbCmd.CommandText = "spCorrespondentOfficeDataLoad";
				dbCmd.CommandType = CommandType.StoredProcedure;
	
				dbCmd.ExecuteNonQuery();
        
        KeyValue.Set("PensonCorrespondentStaticBizDate", bizDatePrior, dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [StaticData.CorrespondentDataLoad]", Log.Error, 1);
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

        if (dbCnPenson.State != ConnectionState.Closed)
        {
          dbCnPenson.Close();
        }
      }
    }
  
		public void BoxPositionFailDayCountSet()
		{
			string dayTime;
			string pensonFailDayCountBizDate;
				
			if (bizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Today is the business date.
			{			
				dayTime = KeyValue.Get("BoxPositionFailDayCountSetTime", "23:00", dbCn);
				pensonFailDayCountBizDate = KeyValue.Get("BoxPositionFailDayCountBizDate", "2001-01-01", dbCn);

				if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // Time now is after the start time.
				{
					if (!pensonFailDayCountBizDate.Equals(bizDate)) //done today?
					{
						try
						{
							Log.Write("Box position fail day set starting for " + bizDate + ". [StaticData.BoxPositionFailDayCountSet]", 2);
							SqlCommand dbCmd = new SqlCommand("spBoxPositionFailDayCountSet", dbCn);
							dbCmd.CommandType = CommandType.StoredProcedure;

							dbCn.Open();
							dbCmd.ExecuteNonQuery();					
							dbCn.Close();

							KeyValue.Set("BoxPositionFailDayCountBizDate", bizDate, dbCn);
							Log.Write("Box position fail day set finished for " + bizDate + ". [StaticData.BoxPositionFailDayCountSet]", 2);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [StaticData.BoxPositionLoad]", Log.Error, 1);
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
						Log.Write("Box position fail day count is current for " + bizDate + ". [StaticData.BoxPositionFailDayCountSet]", 2);
					}
				}
				else
				{
					Log.Write("Time now is before " + dayTime + " UTC, will wait until later. [StaticData.BoxPositionFailDayCountSet]", 2);
				}
			}
			else
			{				
				Log.Write("Today is not the current business day, will wait. [StaticData.BoxPositionFailDayCountSet]", 2);
			}
		}

    private string BoxPositionSql(string index, string firm)
    {
      string configValue;
      string accountFilter;
      string locationFilter;
      string memoFilter;

      string sql =               
     
        "Set NoCount On \n\n" +

        "Create Table  #Results (\n" +
        "      SecId varchar(12) Not Null, \n" +
        "      CustomerLongSettled decimal (19,5) default 0.0,\n" +
        "      CustomerLongTraded decimal (19,5) default 0.0,\n" +
        "      CustomerShortSettled decimal (19,5) default 0.0,\n" +
        "      CustomerShortTraded decimal (19,5) default 0.0,\n" +
        "      FirmLongSettled decimal (19,5) default 0.0,\n" +
        "      FirmLongTraded decimal (19,5) default 0.0,\n" +
        "      FirmShortSettled decimal (19,5) default 0.0,\n" +
        "      FirmShortTraded decimal (19,5) default 0.0,\n" +
        "      CustomerPledgeSettled decimal (19,5) default 0.0,\n" +
        "      CustomerPledgeTraded decimal (19,5) default 0.0,\n" +
        "      FirmPledgeSettled decimal (19,5) default 0.0,\n" +
        "      FirmPledgeTraded decimal (19,5) default 0.0,\n" +
        "      DvpFailInSettled decimal (19,5) default 0.0,\n" +
        "      DvpFailInTraded decimal (19,5) default 0.0,\n" +
        "      DvpFailOutSettled decimal (19,5) default 0.0,\n" +
        "      DvpFailOutTraded decimal (19,5) default 0.0,\n" +
        "      BrokerFailInSettled decimal (19,5) default 0.0,\n" +
        "      BrokerFailInTraded decimal (19,5) default 0.0,\n" +
        "      BrokerFailOutSettled decimal (19,5) default 0.0,\n" +
        "      BrokerFailOutTraded decimal (19,5) default 0.0,\n" +
        "      ClearingFailInSettled decimal (19,5) default 0.0,\n" +
        "      ClearingFailInTraded decimal (19,5) default 0.0,\n" +
        "      ClearingFailOutSettled decimal (19,5) default 0.0,\n" +
        "      ClearingFailOutTraded decimal (19,5) default 0.0,\n" +
        "      OtherFailInSettled decimal (19,5) default 0.0,\n" +
        "      OtherFailInTraded decimal (19,5) default 0.0,\n" +
        "      OtherFailOutSettled decimal (19,5) default 0.0,\n" +
        "      OtherFailOutTraded decimal (19,5) default 0.0,\n" +
				"			 SegReq decimal (19,5) default 0.0,\n" + 
        "      HasValue bit default 0)\n\n" +

        "Insert   #Results (SecId)\n" +
        "Select   SLC.CUSIP\n" +
        "From     dbo.StockLocationCurrent SLC (nolock),\n" +
        "         dbo.SecurityBase SB (nolock)\n" +
        "Where    SLC.Firm = '" + firm + "'\n" +
        " And     SB.CUSIP = SLC.CUSIP\n" +
        " And     SB.Firm = SLC.Firm\n" +
        " And     SB.SecurityTypeCode In " + Standard.ConfigValue("SecurityTypeCodeList") + "\n" +
        "Group By SLC.CUSIP\n\n" +

        "Create Unique Clustered Index ResultsIndex On #Results (SecId)\n\n";

      // Customer section.
      //-------------------------------------------------
      
      configValue = Standard.ConfigValue("BoxPositionCustomerAvailable[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
      sql +=

        // CustomerLongSettled
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp01\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";

      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      CustomerLongSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp01 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp01 \n\n" +

        // CustomerLongTraded
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp02\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";

      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n" +

        "Update   #Results\n" +
        "Set      CustomerLongTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp02 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp02\n\n" +

        // CustomerShortSettled

        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp03\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";

      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent < 0\n" +
        "Group By CUSIP \n\n" +

        "Update   #Results\n" +
        "Set      CustomerShortSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp03 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp03\n\n" +

        // CustomerShortTraded
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp04\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";

      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent < 0\n" +
        "Group By CUSIP \n\n" +

        "Update   #Results\n" +
        "Set      CustomerShortTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp04 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp04\n\n";

      //-------------------------------------------------
      // Customer section end.

      // Firm section.
      //-------------------------------------------------

      configValue = Standard.ConfigValue("BoxPositionFirmAvailable[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();

      sql +=

        // FirmLongSettled 
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp05\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      FirmLongSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp05 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp05\n\n" +

        // FirmLongTraded

        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp06\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        "And      TradeDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      FirmLongTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp06 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp06\n\n" +

        //  FirmShortSettled

        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp07\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        "And      SettlementDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      FirmShortSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp07 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp07\n\n" +

        // FirmShortTraded

        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp08\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      FirmShortTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp08 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp08\n\n";

      //-------------------------------------------------
      // Firm section end.

      // CustomerPledge section.
      //-------------------------------------------------

      configValue = Standard.ConfigValue("BoxPositionCustomerPledge[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();

      sql +=

        // CustomerPledgeSettled
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp15\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent != 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      CustomerPledgeSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp15 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp15\n\n" +

        //CustomerPledgeTraded
        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp16\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent != 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      CustomerPledgeTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp16 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp16\n\n";

      //-------------------------------------------------
      // CustomerPledge section end.

      // FirmPledge section.
      //-------------------------------------------------

      configValue = Standard.ConfigValue("BoxPositionFirmPledge[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
      sql +=

        // FirmPledgeSettled
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp17\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent != 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      FirmPledgeSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp17 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp17\n\n" +

        // FirmPledgeTraded
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp18\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent != 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      FirmPledgeTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp18 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp18\n\n";

      //-------------------------------------------------
      // FirmPledge section end.

      // DvpFail section.
      //-------------------------------------------------

      configValue = Standard.ConfigValue("BoxPositionDvpFail[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
      sql +=

        // DvpFailInSettled
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp19\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      DvpFailInSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp19 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp19\n\n" +

        // DvpFailInTraded
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp20\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      DvpFailInTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp20 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp20\n\n" +

        // DvpFailOutSettled
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp21\n" + 
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      DvpFailOutSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp21 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp21\n\n" +

        // DvpFailOutTraded
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp22\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      DvpFailOutTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp22 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp22\n\n";

      //-------------------------------------------------
      // DvpFail section end.

      // BrokerFail section.
      //-------------------------------------------------

      configValue = Standard.ConfigValue("BoxPositionBrokerFail[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
      sql +=

        // BrokerFailInSettled
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp23\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      BrokerFailInSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp23 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp23\n\n" +

        // BrokerFailInTraded
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp24\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      BrokerFailInTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp24 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp24\n\n" +

        // BrokerFailOutSettled
        
        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp25\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      BrokerFailOutSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp25 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp25\n\n" +

        // BrokerFailOutTraded

        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp26\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      BrokerFailOutTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp26 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp26\n\n";

      //-------------------------------------------------
      // BrokerFail section end.

      // ClearingFail section.
      //-------------------------------------------------

      configValue = Standard.ConfigValue("BoxPositionClearingFail[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
      sql +=

        // ClearingFailInSettled

        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp27\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      ClearingFailInSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp27 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp27\n\n\n" +

        // ClearingFailInTraded

        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp28\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      ClearingFailInTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp28 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp28\n\n" +

        // ClearingFailOutSettled

        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp29\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      ClearingFailOutSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp29 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp29\n\n" +

        // ClearingFailOutTraded

        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp30\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      ClearingFailOutTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp30 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp30\n\n";

      //-------------------------------------------------
      // ClearingFail section end.

      // OtherFail section.
      //-------------------------------------------------

      configValue = Standard.ConfigValue("BoxPositionOtherFail[" + index + "]", ";;;");

      accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
      locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
      memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
      sql +=

        // OtherFailInSettled

        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp31\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      OtherFailInSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp31 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp31\n\n\n" +

        // OtherFailInTraded

        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp32\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent < 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      OtherFailInTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp32 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp32\n\n" +

        // OtherFailOutSettled

        "Select   CUSIP As SecId,\n" +
        "         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp33\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     SettlementDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      OtherFailOutSettled = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp33 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp33\n\n" +

        // OtherFailOutTraded

        "Select   CUSIP As SecId,\n" +
        "         Sum(TradeDateQuantityCurrent) As Quantity\n" +
        "Into     #Temp34\n" +
        "From     dbo.StockLocationCurrent (nolock)\n" +
        "Where    Firm = '" + firm + "'\n";
      
      if (!accountFilter.Equals(""))  
      {
        sql += " And     " + accountFilter + "\n";
      }
      
      if (!locationFilter.Equals(""))  
      {
        sql += " And     " + locationFilter + "\n";
      }
      
      if (!memoFilter.Equals(""))  
      {
        sql += " And     " + memoFilter + "\n";
      }

      sql +=

        " And     TradeDateQuantityCurrent > 0\n" +
        "Group By CUSIP\n\n" +

        "Update   #Results\n" +
        "Set      OtherFailOutTraded = Abs(Quantity),\n" +
        "         HasValue = 1\n" +
        "From     #Temp34 T,\n" +
        "         #Results R\n" +
        "Where    R.SecId = T.SecId\n\n" +

        "Drop Table #Temp34\n\n";

      //-------------------------------------------------
      // OtherFail section end.

			// Auto DTC Account section.
			//-------------------------------------------------

			configValue = Standard.ConfigValue("BoxPositionAutoDTCAccount[" + index + "]", ";;;");

			accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
			locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
			memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
			sql +=

				// OtherFailInSettled

				"Select   CUSIP As SecId,\n" +
				"         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp35\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     SettlementDateQuantityCurrent < 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailInSettled = IsNull(OtherFailInSettled, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp35 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp35\n\n\n" +

				// OtherFailInTraded

				"Select   CUSIP As SecId,\n" +
				"         Sum(TradeDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp36\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     TradeDateQuantityCurrent < 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailInTraded = IsNull(OtherFailInTraded, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp36 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp36\n\n" +

				// OtherFailOutSettled

				"Select   CUSIP As SecId,\n" +
				"         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp37\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     SettlementDateQuantityCurrent > 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailOutSettled = IsNull(OtherFailOutSettled, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp37 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp37\n\n" +

				// OtherFailOutTraded

				"Select   CUSIP As SecId,\n" +
				"         Sum(TradeDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp38\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     TradeDateQuantityCurrent > 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailOutTraded = IsNull(OtherFailOutTraded, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp38 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp38\n\n";

			//-------------------------------------------------
			// Auto DTC Account section end.
			
			
			// Reorg Account section.
			//-------------------------------------------------

			configValue = Standard.ConfigValue("BoxPositionReorgAccount[" + index + "]", ";;;");

			accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
			locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
			memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
			sql +=

				// OtherFailInSettled

				"Select   CUSIP As SecId,\n" +
				"         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp39\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     SettlementDateQuantityCurrent < 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailInSettled = IsNull(OtherFailInSettled, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp39 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp39\n\n\n" +

				// OtherFailInTraded

				"Select   CUSIP As SecId,\n" +
				"         Sum(TradeDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp40\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     TradeDateQuantityCurrent < 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailInTraded = IsNull(OtherFailInTraded, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp40 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp40\n\n" +

				// OtherFailOutSettled

				"Select   CUSIP As SecId,\n" +
				"         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp41\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     SettlementDateQuantityCurrent > 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailOutSettled = IsNull(OtherFailOutSettled, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp41 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp41\n\n" +

				// OtherFailOutTraded

				"Select   CUSIP As SecId,\n" +
				"         Sum(TradeDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp42\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     TradeDateQuantityCurrent > 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailOutTraded = IsNull(OtherFailOutTraded, 0) +  Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp42 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp42\n\n";

			//-------------------------------------------------
			// Reorg Account section end.			
			
			
				
			// Stock Split Account section.
			//-------------------------------------------------

			configValue = Standard.ConfigValue("BoxPositionStockSplitAccount[" + index + "]", ";;;");

			accountFilter = Tools.SplitItem(configValue, ";", 0).Trim(); 
			locationFilter = Tools.SplitItem(configValue, ";", 1).Trim();
			memoFilter = Tools.SplitItem(configValue, ";", 2).Trim();
      
			sql +=

				// OtherFailInSettled

				"Select   CUSIP As SecId,\n" +
				"         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp43\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     SettlementDateQuantityCurrent < 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailInSettled = IsNull(OtherFailInSettled, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp43 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp43\n\n\n" +

				// OtherFailInTraded

				"Select   CUSIP As SecId,\n" +
				"         Sum(TradeDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp44\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     TradeDateQuantityCurrent < 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailInTraded = IsNull(OtherFailInTraded, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp44 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp44\n\n" +

				// OtherFailOutSettled

				"Select   CUSIP As SecId,\n" +
				"         Sum(SettlementDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp45\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     SettlementDateQuantityCurrent > 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailOutSettled = IsNull(OtherFailOutSettled, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp45 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp45\n\n" +

				// OtherFailOutTraded

				"Select   CUSIP As SecId,\n" +
				"         Sum(TradeDateQuantityCurrent) As Quantity\n" +
				"Into     #Temp46\n" +
				"From     dbo.StockLocationCurrent (nolock)\n" +
				"Where    Firm = '" + firm + "'\n";
      
			if (!accountFilter.Equals(""))  
			{
				sql += " And     " + accountFilter + "\n";
			}
      
			if (!locationFilter.Equals(""))  
			{
				sql += " And     " + locationFilter + "\n";
			}
      
			if (!memoFilter.Equals(""))  
			{
				sql += " And     " + memoFilter + "\n";
			}

			sql +=

				" And     TradeDateQuantityCurrent > 0\n" +
				"Group By CUSIP\n\n" +

				"Update   #Results\n" +
				"Set      OtherFailOutTraded = IsNull(OtherFailOutTraded, 0) + Abs(Quantity),\n" +
				"         HasValue = 1\n" +
				"From     #Temp46 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n" +

				"Drop Table #Temp46\n\n";

			//-------------------------------------------------
			// Stock Split Account section end.
			
			
			
			//
			// Seg Requirement
			//-------------------------------------------------

			sql += 

				"Select		CUSIP As SecId,\n" +
				"					Sum(IsNull(SettlementDateQuantityCurrent, 0)) AS SegReq\n" +
				"Into			#Temp47\n" +
				"From			dbo.StockLocationCurrent (nolock)\n" + 			
				"Where  	Firm = '07'\n" +
				"And			SettlementDateQuantityCurrent > 0\n" +
				"And			LocMemo = 'S'\n" + 
				"Group by Cusip\n";		
				

			sql +=
				"Update   #Results\n" +
				"Set      SegReq = Abs(T.SegReq),\n" +
				"         HasValue = 1\n" +
				"From     #Temp47 T,\n" +
				"         #Results R\n" +
				"Where    R.SecId = T.SecId\n\n Drop Table #Temp47\n\n";
			
			//-------------------------------------------------
			// Seg Req section end.
	
			
			
			
			
			sql +=

        "Select   *\n" +
        "From     #Results\n" +
        "Where    HasValue = 1\n" +
        "Order By 1\n\n" +

        "Drop Table #Results\n\n" +

        "Set NoCount Off\n";

      Log.Write("SQL Statement:\n\n" + sql + "\n[StaticData.BoxPositionSql]\n", 3);

      return sql;
    }

    private string BoxLocationSql(string firm, string account, string location, string memo)
    {
      string sql =

      "Select   CUSIP,\n" +
      "Sum(SettlementDateQuantityCurrent * (-1)) As QuantitySettled,\n" +
      "Sum(TradeDateQuantityCurrent * (-1)) As QuantityTraded\n" +
      "From     StockLocationCurrent\n" +
      "Where    AccountNumber = '" + account + "'\n" +
      "And      Firm = '" + firm + "'\n";
      
      if (!location.Trim().Equals(""))
      {
        sql += "And      LocLocation = '" + location + "'\n";
      }

      if (!memo.Trim().Equals(""))
      {
        sql += "And      LocMemo = '" + memo + "'\n";
      }

      sql += "Group By CUSIP\n";

      Log.Write("SQL Statement:\n\n" + sql + "\n[StaticData.BoxLocationSql]\n", 3);

      return sql;
    }
  }
}
