using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;
using StockLoan.WebServices.ToolFunctions;

namespace StockLoan.WebServices.LocatesService 
{
    public partial class LocatesService : ILocatesService
    {

        public byte[] TradingGroupsGet(string bookGroup, short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spTradingGroupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBooKGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 20);
                paramBooKGroup.Value = bookGroup;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "TradingGroups");

                Log.Write("Returning a 'TradingGroups' table with " + dataSet.Tables["TradingGroups"].Rows.Count + " rows. [LocatesService.TradingGroupsGet]", 3);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.TradingGroupsGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);
        }

        public void TradingGroupSet(
                        string bookGroup,
                        string groupCode,
                        string groupName,
                        string minPrice,
                        string autoApprovalMax,
                        string premiumMin,
                        string premiumMax,
                        string autoEmail,
                        string emailAddress,
                        string lastEmailAddress,
                        string lastEmailDate,
                        string actUserId,
                        string isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());

            try
            {
                SqlCommand dbCmd = new SqlCommand("spTradingGroupSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBooKGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 20);
                paramBooKGroup.Value = bookGroup;

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

                SqlParameter paramAccountParametersEdit = dbCmd.Parameters.Add("@AccountParametersEdit", SqlDbType.Bit);
                paramAccountParametersEdit.Value = 1;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.TradingGroupSet]", Log.Error, 1);
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

        public string LocateListSubmit(string clientId, string groupCode, string clientComment, string locateList)
        {
            List list = new List();

            if (list.Parse(locateList).Equals("OK"))
            {
                SqlDataReader dataReader = null;
                SqlConnection dbCn = new SqlConnection(dbCnStr());

                SqlCommand dbCmd = new SqlCommand("dbo.spShortSaleLocateRequest", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                paramTradeDate.Value = KeyValue.Get("BizDateExchange", "", dbCnStr());

                SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 25);
                paramClientId.Value = clientId;

                SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;

                SqlParameter paramClientComment = dbCmd.Parameters.Add("@ClientComment", SqlDbType.VarChar, 50);
                paramClientComment.Value = clientComment;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                SqlParameter paramClientQuantity = dbCmd.Parameters.Add("@ClientQuantity", SqlDbType.BigInt);

                SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
                paramReturnData.Value = 0;

                int n = 0;

                try
                {
                    dbCn.Open();

                    for (int i = 0; i < list.Count; i++)
                    {
                        paramSecId.Value = list.ItemGet(i).SecId;
                        paramClientQuantity.Value = list.ItemGet(i).Quantity;

                        dataReader = dbCmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            n += 1;
                        }

                        dataReader.Close();
                    }

                    if (n.Equals(list.Count))
                    {
                        Log.Write("Processed " + (list.Count) + " item[s] for " + clientId + "|" + groupCode + ". [LocatesService.LocateListSubmit]", 2);
                    }
                    else
                    {
                        Log.Write("Error: Processed " + n + " item[s] for " + clientId + "|" + groupCode +
                            " out of an expected " + (list.Count) + " item[s]. [LocatesService.LocateListSubmit]", 2);

                        return "Error: Processed " + n + " item[s] out of an expected " + (list.Count) + " item[s].";
                    }

                    Log.Write("Processed " + (list.Count) + " item[s] for " + clientId + "|" + groupCode + ". [LocatesService.LocateListSubmit]", 2);
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [LocatesService.LocateListSubmit]", Log.Error, 1);

                    return "Error processing request.";
                }
                finally
                {
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                    if (dbCn.State != ConnectionState.Closed)
                    {
                        dbCn.Close();
                    }
                }

                return "List of " + list.Count.ToString("#,##0") + " item[s] has been submitted.";
            }
            else
            {
                return "List was not in a correct format!";
            }
        }

        public byte[] InventoryGet(string groupCode, string secId, short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spInventoryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 12);
                    paramGroupCode.Value = groupCode;
                }

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "Inventory");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.InventoryGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);
        }

        public byte[] LocateItemGet(string groupCode, string locateId, short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleLocateNoInvLookupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = groupCode;
                }

                if (!locateId.Equals(""))
                {
                    SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
                    paramLocateId.Value = locateId;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                
               dataAdapter.Fill(dataSet, "Locates");

               dataSet.Tables["Locates"].Columns.Add("IsFill", typeof(bool));
               dataSet.AcceptChanges();

                Log.Write("Get item: " + locateId + " [LocatesService.LocateItemGet]", 2);
            }
            catch (Exception e)
            {
                Log.Write(e.StackTrace + " [LocatesService.LocateItemGet]", Log.Error, 1);
            }
            finally
            {
                dbCn.Close();
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);
        }

        public void LocateItemSet(
            long locateId,
            string quantity,
            string source,
            string feeRate,
            string preBorrow,
            string comment,
            string actUserId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());

            SqlCommand dbCmd = new SqlCommand("spShortSaleLocateSet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
            paramLocateId.Value = locateId;

            if (!quantity.Equals(""))
            {
                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = long.Parse(quantity);
            }

            if (!feeRate.Equals(""))
            {
                SqlParameter paramFeeRate = dbCmd.Parameters.Add("@FeeRate", SqlDbType.Float);
                paramFeeRate.Value = feeRate;
            }

            if (!comment.Equals(""))
            {
                SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                paramComment.Value = comment;
            }

            if (!preBorrow.Equals(""))
            {
                SqlParameter paramPreBorrow = dbCmd.Parameters.Add("@PreBorrow", SqlDbType.Bit);
                paramPreBorrow.Value = preBorrow;
            }

            if (!actUserId.Equals(""))
            {
                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;
            }

            if (!source.Equals(""))
            {
                SqlParameter paramSource = dbCmd.Parameters.Add("@Source", SqlDbType.VarChar, 50);
                paramSource.Value = source;
            }

            SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
            paramReturnData.Value = 1;

            try
            {
                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                Log.Write("Set item: " + locateId.ToString() + " [LocatesService.LocateItemSet]", 2);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.LocateItemSet]", Log.Error, 1);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public byte[] LocatesGet(string tradeDate, string groupCode, string clientId, short utcOffset, string status)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleLocateNoInvLookupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                if (!tradeDate.Equals(""))
                {
                    SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                    paramTradeDate.Value = tradeDate;
                }

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10);
                    paramGroupCode.Value = groupCode;
                }

                if (!clientId.Equals(""))
                {
                    SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 25);
                    paramClientId.Value = clientId;
                }

                if (!status.Equals(""))
                {
                    SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 10);
                    paramStatus.Value = status;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(dataSet, "Locates");

                dataSet.Tables["Locates"].Columns.Add("IsFill", typeof(bool));
                dataSet.AcceptChanges();

                Log.Write("Returning a 'Locates' table with " + dataSet.Tables["Locates"].Rows.Count + " rows. [LocatesService.LocatesGet]", 2);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.LocatesGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);
        }

        public byte[] LocateSummaryGet(string tradeDate, string secId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleLocateSummaryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!tradeDate.Equals(""))
                {
                    SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                    paramTradeDate.Value = tradeDate;
                }

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }
             
                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(dataSet, "LocateSummary");                
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.LocateSummaryGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);
        }

        public byte[] LocateGroupCodeSummaryGet(string tradeDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleGroupCodeSummaryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!tradeDate.Equals(""))
                {
                    SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                    paramTradeDate.Value = tradeDate;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(dataSet, "LocateGroupCodeSummary");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.LocateGroupCodeSummaryGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);
        }

        public byte[] SecMasterItemGet(string secId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spSecMasterItemGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(dataSet, "SecMasterItem");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.SecMasterItemGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);            
        }

        public byte[] BookGroupsGet()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBookGroupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = KeyValue.Get("BizDate", "", dbCnStr());               

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(dataSet, "BookGroups");      
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.BookGroupsGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);      
        }

        public byte[] BoxPositionItemGet(string bookGroup, string secId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionLocatesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = KeyValue.Get("BizDate", "", dbCnStr());

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;
                
                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(dataSet, "BoxPositionItem");

                foreach (DataRow drRow in dataSet.Tables["BoxPositionItem"].Rows)
                {
                    if (!drRow["BookGroup"].ToString().Equals(bookGroup))
                    {
                        drRow.Delete();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.BoxPositionItemGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);         
        }
       
        public string dbCnStr()
        {
            return "Trusted_Connection=yes; " +
           "Data Source=" + Standard.ConfigValue("MainDatabaseHost") + "; " +
           "Initial Catalog=" + Standard.ConfigValue("MainDatabaseName") + ";";
        }

        public bool WebUserAuthorize(string userId, string password)
        {
            bool isFound = false;

            DataSet dsWebUser = new DataSet();

            SqlConnection dbCn = new SqlConnection(dbCnStr());

            try
            {
                SqlCommand dbCmd = new SqlCommand("spWebUsersGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
                dAdapter.Fill(dsWebUser);
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [LocatesService.WebUserAuthorize]", Log.Error, 1);
            }

            foreach (DataRow drItem in dsWebUser.Tables[0].Rows)
            {
                if (userId.Equals(drItem["UserId"].ToString()) &&
                    password.Equals(drItem["Password"].ToString()))
                {
                    isFound = true;
                }
            }


            return isFound;
        }

        public byte[] LocatesMessageGet()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleLocatesMessagesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = KeyValue.Get("BizDate", "", dbCnStr());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(dataSet, "Messages");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.LocatesMessageGet]", Log.Error, 1);
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dataSet);
        }

        public void LocatesMessageSet(string userId, string message)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr());

            SqlCommand dbCmd = new SqlCommand("dbo.spShortSaleLocatesMessageSet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
            paramUserId.Value = userId;

            SqlParameter paramMessage = dbCmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000);
            paramMessage.Value = message;

            try
            {
                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                Log.Write("Set item: " + message + " [LocatesService.LocatesMessageSet]", 2);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocatesService.LocatesMessageSet]", Log.Error, 1);
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
