using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class Deals
    {
        
        public static DataSet DealsGet(
            string bizDate, 
            string dealId, 
            string dealIdPrefix, 
            bool isActive, 
            short utcOffSet)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBDeals.DealsGet(
                    dealId, 
                    dealIdPrefix, 
                    bizDate, 
                    isActive, 
                    utcOffSet);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void DealToContract(string dealId, string bizDate)
        {
            try
            {
                if (dealId.Equals(""))
                {
                    throw new Exception("Deal ID is required");
                }

                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                DBDeals.DealToContract(dealId, bizDate);

            }
            catch 
            {
                throw;
            }
        }

        public static void DealSet(
            string dealId, 
            string bookGroup,
            string dealType, 
            string book,
            string bookContact,
            string contractId, 
            string secId, 
            string quantity, 
            string amount, 
            string collateralCode, 
            string valueDate, 
            string settleDate,
            string termDate, 
            string rate, 
            string rateCode, 
            string poolCode, 
            string divRate, 
            bool divCallable, 
            bool incomeTracked, 
            string marginCode, 
            string margin, 
            string currencyIso, 
            string securityDepot, 
            string cashDepot, 
            string comment, 
            string fund, 
            string dealStatus, 
            bool isActive, 
            string actUserId, 
            bool returnData, 
            string feeAmount, 
            string feeCurrencyIso, 
            string feeType)
        {
            try
            {
                if (dealId.Trim().Equals(""))
                {
                    throw new Exception("Deal ID is required");
                }

                DBDeals.DealSet(
                    dealId, 
                    bookGroup, 
                    dealType, 
                    book, 
                    bookContact, 
                    contractId, 
                    secId, 
                    quantity, 
                    amount,
                    collateralCode, 
                    valueDate, 
                    settleDate, 
                    termDate, 
                    rate, 
                    rateCode, 
                    poolCode, 
                    divRate, 
                    divCallable, 
                    incomeTracked,
                    marginCode, 
                    margin, 
                    currencyIso, 
                    securityDepot, 
                    cashDepot,
                    comment, 
                    fund, 
                    dealStatus, 
                    isActive, 
                    actUserId,
                    returnData, 
                    feeAmount, 
                    feeCurrencyIso, 
                    feeType);
                
            }
            catch 
            {
                throw;
            }
        }

        public static void DealValidateData(DataRowView dr)
        {
            if (dr["Quantity"].ToString().Equals(""))
            {
                throw new Exception("Quantity field cannot be empty");
            }

            if (dr["Amount"].ToString().Equals(""))
            {
                throw new Exception("Amount field cannot be empty");
            }

            if (!dr["ValueDate"].ToString().Equals("") && !dr["SettleDate"].ToString().Equals(""))
            {
                if (DateTime.Parse(dr["ValueDate"].ToString()) > DateTime.Parse(dr["SettleDate"].ToString()))
                {
                    throw new Exception("Value Date cannot be greater then Settle Date");
                }
            }

            if (dr["Book"].ToString().Equals(""))
            {
                throw new Exception("Book field cannot be empty");
            }

            if (dr["Rate"].ToString().Equals(""))
            {
                throw new Exception("Rate field cannot be empty");
            }

            if (dr["Margin"].ToString().Equals(""))
            {
                throw new Exception("Margin field cannot be empty");
            }
        }
    }
}
