using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBDeals
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet DealsGet(string dealId, string dealIdPrefix, string bizDate, bool isActive, short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDealsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!dealId.Equals(""))
                {
                    SqlParameter paramDealId = dbCmd.Parameters.Add("@DealId", SqlDbType.VarChar, 16);
                    paramDealId.Value = dealId;
                }
                if (!dealIdPrefix.Equals(""))
                {
                    SqlParameter paramDealIdPrefix = dbCmd.Parameters.Add("@DealIdPrefix", SqlDbType.Char, 1);
                    paramDealIdPrefix.Value = dealIdPrefix;
                }
                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;


                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Deals");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void DealToContract(string dealId, string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDealToContract", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!dealId.Equals(""))
                {
                    SqlParameter paramDealId = dbCmd.Parameters.Add("@DealId", SqlDbType.VarChar, 16);
                    paramDealId.Value = dealId;
                }

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

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
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDealSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramDealId = dbCmd.Parameters.Add("@DealId", SqlDbType.VarChar, 16);
                paramDealId.Value = dealId;

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!dealType.Equals(""))
                {
                    SqlParameter paramDealType = dbCmd.Parameters.Add("@DealType", SqlDbType.Char, 1);
                    paramDealType.Value = dealType;
                }

                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
                }

                if (!bookContact.Equals(""))
                {
                    SqlParameter paramBookContact = dbCmd.Parameters.Add("@BookContact", SqlDbType.VarChar, 15);
                    paramBookContact.Value = bookContact;
                }

                if (!contractId.Equals(""))
                {
                    SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                    paramContractId.Value = contractId;
                }

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }

                if (!quantity.Equals(""))
                {
                    SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                    paramQuantity.Value = quantity;
                }

                if (!amount.Equals(""))
                {
                    SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                    paramAmount.Value = amount;
                }

                if (!collateralCode.Equals(""))
                {
                    SqlParameter paramCollateralCode = dbCmd.Parameters.Add("@CollateralCode", SqlDbType.VarChar, 1);
                    paramCollateralCode.Value = collateralCode;
                }

                if (!valueDate.Equals(""))
                {
                    SqlParameter paramValueDate = dbCmd.Parameters.Add("@ValueDate", SqlDbType.DateTime);
                    paramValueDate.Value = valueDate;
                }

                if (!settleDate.Equals(""))
                {
                    SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
                    paramSettleDate.Value = settleDate;
                }

                if (!termDate.Equals(""))
                {
                    SqlParameter paramTermDate = dbCmd.Parameters.Add("@TermDate", SqlDbType.DateTime);
                    paramTermDate.Value = termDate;
                }

                if (!rate.Equals(""))
                {
                    SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
                    paramRate.Value = rate;
                }

                if (!rateCode.Equals(""))
                {
                    SqlParameter paramRateCode = dbCmd.Parameters.Add("@RateCode", SqlDbType.VarChar, 1);
                    paramRateCode.Value = rateCode;
                }

                if (!poolCode.Equals(""))
                {
                    SqlParameter paramPoolCode = dbCmd.Parameters.Add("@PoolCode", SqlDbType.VarChar, 1);
                    paramPoolCode.Value = poolCode;
                }

                if (!divRate.Equals(""))
                {
                    SqlParameter paramDivRate = dbCmd.Parameters.Add("@DivRate", SqlDbType.Decimal);
                    paramDivRate.Value = divRate;
                }

                SqlParameter paramDivCallable = dbCmd.Parameters.Add("@DivCallable", SqlDbType.Bit);
                paramDivCallable.Value = divCallable;

                SqlParameter paramIncomeTracked = dbCmd.Parameters.Add("@IncomeTracked", SqlDbType.Bit);
                paramIncomeTracked.Value = incomeTracked;

                if (!marginCode.Equals(""))
                {
                    SqlParameter paramMarginCode = dbCmd.Parameters.Add("@MarginCode", SqlDbType.VarChar, 1);
                    paramMarginCode.Value = marginCode;
                }

                if (!margin.Equals(""))
                {
                    SqlParameter paramMargin = dbCmd.Parameters.Add("@Margin", SqlDbType.Decimal);
                    paramMargin.Value = margin;
                }

                if (!currencyIso.Equals(""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                if (!securityDepot.Equals(""))
                {
                    SqlParameter paramSecurityDepot = dbCmd.Parameters.Add("@SecurityDepot", SqlDbType.VarChar, 2);
                    paramSecurityDepot.Value = securityDepot;
                }

                if (!cashDepot.Equals(""))
                {
                    SqlParameter paramCashDepot = dbCmd.Parameters.Add("@CashDepot", SqlDbType.VarChar, 2);
                    paramCashDepot.Value = cashDepot;
                }

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                    paramComment.Value = comment;
                }

                if (!fund.Equals(""))
                {
                    SqlParameter paramFund = dbCmd.Parameters.Add("@Fund", SqlDbType.VarChar, 6);
                    paramFund.Value = fund;
                }

                if (!dealStatus.Equals(""))
                {
                    SqlParameter paramDealStatus = dbCmd.Parameters.Add("@DealStatus", SqlDbType.Char, 1);
                    paramDealStatus.Value = dealStatus;
                }

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                if (!actUserId.Equals(""))
                {
                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = actUserId;
                }

                SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
                paramReturnData.Value = returnData;

                if (!feeAmount.Equals(""))
                {
                    SqlParameter paramFeeAmount = dbCmd.Parameters.Add("@FeeAmount", SqlDbType.Decimal);
                    paramFeeAmount.Value = feeAmount;
                }

                if (!feeCurrencyIso.Equals(""))
                {
                    SqlParameter paramFeeCurrencyIso = dbCmd.Parameters.Add("@FeeCurrencyIso", SqlDbType.VarChar, 3);
                    paramFeeCurrencyIso.Value = feeCurrencyIso;
                }

                if (!feeType.Equals(""))
                {
                    SqlParameter paramFeeType = dbCmd.Parameters.Add("@FeeType", SqlDbType.Char, 1);
                    paramFeeType.Value = feeType;
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
    }
}
