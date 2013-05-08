using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBContracts
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet ContractBillingsGet(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractBillingsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "ContractBillings");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet ContractsGet(string bizDate, string bookGroup, string contractId, string contractType)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!contractId.Equals(""))
                {
                    SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                    paramContractId.Value = contractId;
                }

                if (!contractType.Equals(""))
                {
                    SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 3);
                    paramContractType.Value = contractType;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Contracts");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet ContractDetailsGet(string bizDate, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractDetailsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "ContractDetails");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet ContractResearchGet(
            string bizDate,
            string startDate,
            string stopDate,
            string bookGroup,
            string book,
            string contractId,
            string secId,
            string amount,
            string logicId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractResearchGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }
                if (!startDate.Equals(""))
                {
                    SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                    paramStartDate.Value = bizDate;
                }
                if (!stopDate.Equals(""))
                {
                    SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                    paramStopDate.Value = stopDate;
                }
                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = bookGroup;
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
                if (!amount.Equals(""))
                {
                    SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                    paramAmount.Value = amount;
                }
                if (!logicId.Equals(""))
                {
                    SqlParameter paramLogicId = dbCmd.Parameters.Add("@LogicId", SqlDbType.Char, 1);
                    paramLogicId.Value = logicId;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "ContractResearch");
            }
            catch
            {
                throw;
            }

            return dsTemp;

        }

        public static int ContractBizDateProcessRoll(string bizDate, string bizDatePrior)
        {

            SqlConnection dbCn = new SqlConnection(dbCnStr);
            int rowCount = 0;

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractBizDateProcessRoll", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramRecordCount = dbCmd.Parameters.Add("@RecordCount", SqlDbType.Int);
                paramRecordCount.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                rowCount = (int)paramRecordCount.Value;
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

            return rowCount;
        }

        public static int ContractBizDateSystemRoll(string bizDate, string bizDatePrior)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            int rowCount = 0;

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractBizDateSystemRoll", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramRecordCount = dbCmd.Parameters.Add("@RecordCount", SqlDbType.Int);
                paramRecordCount.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                rowCount = (int)paramRecordCount.Value;
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

            return rowCount;
        }

        public static int ContractRateChangeAsOfSet(
            string startDate,
            string endDate,
            string bookGroup,
            string book,
            string contractId,
            string oldRate,
            string newRate,
            string actUserId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            int rowCount = 0;

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRetroRateChangeSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramStartDate.Value = startDate;

                SqlParameter paramEndDate = dbCmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
                paramEndDate.Value = endDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
                }

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                paramContractId.Value = contractId;

                SqlParameter paramOldRate = dbCmd.Parameters.Add("@OldRate", SqlDbType.Decimal);
                paramOldRate.Value = oldRate;

                SqlParameter paramNewRate = dbCmd.Parameters.Add("@NewRate", SqlDbType.Decimal);
                paramNewRate.Value = newRate;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramRecordCount = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.Int);
                paramRecordCount.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                rowCount = (int)paramRecordCount.Value;
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

            return rowCount;
        }

        public static void ContractSet(
            string bizDate,
            string bookGroup,
            string contractId,
            string contractType,
            string book,
            string secId,
            string quantity,
            string quantitySettled,
            string amount,
            string amountSettled,
            string collateralCode,
            string valueDate,
            string settleDate,
            string termDate,
            string rate,
            string rateCode,
            string statusFlag,
            string poolCode,
            string divRate,
            bool divCallable,
            bool incomeTracked,
            string marginCode,
            string margin,
            string currencyIso,
            string securityDepot,
            string cashDepot,
            string otherBook,
            string comment,
            string fund,
            string tradeRefId,
            string feeAmount,
            string feeCurrencyIso,
            string feeType,
            bool returnData,
            bool isIncremental,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                paramContractId.Value = contractId;

                SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 3);
                paramContractType.Value = contractType;

                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
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

                if (!quantitySettled.Equals(""))
                {
                    SqlParameter paramQuantitySettled = dbCmd.Parameters.Add("@QuantitySettled", SqlDbType.BigInt);
                    paramQuantitySettled.Value = quantitySettled;
                }

                if (!amount.Equals(""))
                {
                    SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                    paramAmount.Value = amount;
                }

                if (!amountSettled.Equals(""))
                {
                    SqlParameter paramAmountSettled = dbCmd.Parameters.Add("@AmountSettled", SqlDbType.Decimal);
                    paramAmountSettled.Value = amountSettled;
                }

                SqlParameter paramCollateralCode = dbCmd.Parameters.Add("@CollateralCode", SqlDbType.VarChar, 1);
                paramCollateralCode.Value = collateralCode;

                if (!valueDate.Equals("") && !valueDate.Equals("0000-00-00"))
                {
                    SqlParameter paramValueDate = dbCmd.Parameters.Add("@ValueDate", SqlDbType.DateTime);
                    paramValueDate.Value = valueDate;
                }
                if (!settleDate.Equals("") && !settleDate.Equals("0000-00-00"))
                {
                    SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
                    paramSettleDate.Value = settleDate;
                }
                if (!termDate.Equals("") && !termDate.Equals("0000-00-00"))
                {
                    SqlParameter paramTermDate = dbCmd.Parameters.Add("@TermDate", SqlDbType.DateTime);
                    paramTermDate.Value = termDate;
                }
                if (!rate.Equals(""))
                {
                    SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
                    paramRate.Value = rate;
                }
                else
                {
                    SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
                    paramRate.Value = 0.0;
                }
                if (!rateCode.Equals(""))
                {
                    SqlParameter paramRateCode = dbCmd.Parameters.Add("@RateCode", SqlDbType.VarChar, 1);
                    paramRateCode.Value = rateCode;
                }
                if (!statusFlag.Equals(""))
                {
                    SqlParameter paramStatusFlag = dbCmd.Parameters.Add("@StatusFlag", SqlDbType.VarChar, 1);
                    paramStatusFlag.Value = statusFlag;
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
                else
                {
                    SqlParameter paramDivRate = dbCmd.Parameters.Add("@DivRate", SqlDbType.Decimal);
                    paramDivRate.Value = 0.0;
                }

                SqlParameter paramDivCallable = dbCmd.Parameters.Add("@DivCallable", SqlDbType.Bit);
                paramDivCallable.Value = divCallable;

                SqlParameter paramIncomeTracked = dbCmd.Parameters.Add("@IncomeTracked", SqlDbType.Bit);
                paramIncomeTracked.Value = incomeTracked;

                SqlParameter paramMarginCode = dbCmd.Parameters.Add("@MarginCode", SqlDbType.VarChar, 1);
                paramMarginCode.Value = marginCode;

                if (!margin.Equals(""))
                {
                    SqlParameter paramMargin = dbCmd.Parameters.Add("@Margin", SqlDbType.Decimal);
                    paramMargin.Value = margin;
                }
                else
                {
                    SqlParameter paramMargin = dbCmd.Parameters.Add("@Margin", SqlDbType.Decimal);
                    paramMargin.Value = 0.0;
                }

                SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                paramCurrencyIso.Value = currencyIso;

                SqlParameter paramSecurityDepot = dbCmd.Parameters.Add("@SecurityDepot", SqlDbType.VarChar, 2);
                paramSecurityDepot.Value = securityDepot;

                SqlParameter paramCashDepot = dbCmd.Parameters.Add("@CashDepot", SqlDbType.VarChar, 2);
                paramCashDepot.Value = cashDepot;

                SqlParameter paramOtherBook = dbCmd.Parameters.Add("@OtherBook", SqlDbType.VarChar, 10);
                paramOtherBook.Value = otherBook;

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

                if (!tradeRefId.Equals(""))
                {
                    SqlParameter paramTradeRefId = dbCmd.Parameters.Add("@TradeRefId", SqlDbType.VarChar, 50);
                    paramTradeRefId.Value = tradeRefId;
                }

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

                SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
                paramReturnData.Value = returnData;

                SqlParameter paramIsIncremental = dbCmd.Parameters.Add("@IsIncremental", SqlDbType.Bit);
                paramIsIncremental.Value = isIncremental;

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
    }
}
