using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using StockLoan.Common;

namespace LocateService
{
       public class LocateService : ILocateService
    {

        public byte[] LocateGet(string tradeDate, string groupCode, string locateId, string status, short utcOffset)
        {
            DataSet dsLocate = new DataSet();

            SqlConnection dbCn = new SqlConnection(Standard.ConfigValue("SenderoDatabase"));

            SqlCommand dbCmd = new SqlCommand("spShortSaleLocateGet");
            dbCmd.CommandType = CommandType.StoredProcedure;

            if (!locateId.Equals(""))
            {
                SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
                paramLocateId.Value = locateId;
            }

            if (!tradeDate.Equals(""))
            {
                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                paramTradeDate.Value = tradeDate;
            }

            if (!groupCode.Equals(""))
            {
                SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;
            }

            if (!status.Equals(""))
            {
                SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 10);
                paramStatus.Value = status;
            }

            SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffSet", SqlDbType.SmallInt);
            paramUtcOffset.Value = utcOffset;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
            dataAdapter.Fill(dsLocate, "Locates");

            return WCFFunctions.Functions.ConvertDataSet(dsLocate);
        }


        public void LocateSet(string locateId, string comment, string quantity, string userId)
        {
        }


        public void SeucrityIdInformationGet(string secId)
        {
        }

        public byte[] LocateResearchGet(string locateId,
                              string startDate,
                              string stopDate,
                              string clientId,
                              string groupCode,
                              string secId,
                              string source,
                              string status,
                              string actor,
                              string comment,
                              string clientQuantity,
                              string clientQuantityOperator,
                              string quantity,
                              string quantityOperator,
                              string openTime,
                              string openTimeOperator,
                              short utcOffset)
        {
            DataSet dsLocate = new DataSet();

            SqlConnection dbCn = new SqlConnection(Standard.ConfigValue("SenderoDatabase"));

            SqlCommand dbCmd = new SqlCommand("spShortSaleLocateResearchGet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            if (!locateId.Equals(""))
            {
                SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
                paramLocateId.Value = locateId;
            }

            if (!startDate.Equals(""))
            {
                SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramStartDate.Value = startDate;
            }

            if (!stopDate.Equals(""))
            {
                SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;
            }

            if (!clientId.Equals(""))
            {
                SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 25);
                paramClientId.Value = clientId;
            }


            if (!groupCode.Equals(""))
            {
                SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;
            }

            if (!secId.Equals(""))
            {
                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;
            }

            if (!source.Equals(""))
            {
                SqlParameter paramSource = dbCmd.Parameters.Add("@Source", SqlDbType.VarChar, 50);
                paramSource.Value = source;
            }
            

            if (!status.Equals(""))
            {
                SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 10);
                paramStatus.Value = status;
            }

            if (!actor.Equals(""))
            {
                SqlParameter paramActor = dbCmd.Parameters.Add("@Actor", SqlDbType.VarChar, 50);
                paramActor.Value = actor;
            }

            if (!comment.Equals(""))
            {
                SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                paramComment.Value = comment;
            }

            if (!clientQuantity.Equals(""))
            {
                SqlParameter paramClientQuantity = dbCmd.Parameters.Add("@ClientQuantity", SqlDbType.BigInt);
                paramClientQuantity.Value = clientQuantity;
            }

            if (!clientQuantity.Equals("") && !clientQuantityOperator.Equals(""))
            {
                SqlParameter paramClientQuantityOperator = dbCmd.Parameters.Add("@ClientQuantityOperator", SqlDbType.VarChar, 1);
                paramClientQuantityOperator.Value = clientQuantityOperator;
            }

            if (!quantity.Equals(""))
            {
                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;
            }

            if (!quantity.Equals("") && !quantityOperator.Equals(""))
            {
                SqlParameter paramQuantityOperator = dbCmd.Parameters.Add("@QuantityOperator", SqlDbType.VarChar, 1);
                paramQuantityOperator.Value = quantityOperator;
            }

            if (!openTime.Equals(""))
            {
                SqlParameter paramOpenTime = dbCmd.Parameters.Add("@OpenTime", SqlDbType.DateTime);
                paramOpenTime.Value = openTime;
            }

            if (!openTime.Equals("") && !openTimeOperator.Equals(""))
            {
                SqlParameter paramOpenTimeOperator = dbCmd.Parameters.Add("@OpenTimeOperator", SqlDbType.VarChar, 1);
                paramOpenTimeOperator.Value = openTimeOperator;
            }

            SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffSet", SqlDbType.SmallInt);
            paramUtcOffset.Value = utcOffset;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
            dataAdapter.Fill(dsLocate, "Locates");

            return WCFFunctions.Functions.ConvertDataSet(dsLocate);
        }
    }
}
