using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBBooks
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet BooksGet(string bookGroup, string book)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBooksGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Books");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet BookClearingInstructionsGet(string bookGroup, string book)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookClearingInstructionsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "BookClearingInstructions");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void BookClearingInstructionSet(
            string bookGroup,
            string book,
            string countryCode,
            string currencyIso,
            string divRate,
            string cashInstructions,
            string securityInstructions,
            string actUserId,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookClearingInstructionSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                paramCountryCode.Value = countryCode;

                SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                paramCurrencyIso.Value = currencyIso;

                SqlParameter paramDivRate = dbCmd.Parameters.Add("@DivRate", SqlDbType.Decimal);
                paramDivRate.Value = divRate;

                if (!cashInstructions.Equals(""))
                {
                    SqlParameter paramCashInstructions = dbCmd.Parameters.Add("@CashInstructions", SqlDbType.VarChar, 250);
                    paramCashInstructions.Value = cashInstructions;
                }

                if (!securityInstructions.Equals(""))
                {
                    SqlParameter paramSecurityInstructions = dbCmd.Parameters.Add("@SecurityInstructions", SqlDbType.VarChar, 250);
                    paramSecurityInstructions.Value = securityInstructions;
                }

                if (!actUserId.Equals(""))
                {
                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = actUserId;
                }

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

        public static DataSet BookContactsGet(string bookGroup, string book, short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookContactsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "BookContacts");
            }
            catch
            {
                throw;
            }

            return dsTemp;

        }

        public static void BookContactSet(
            string bookGroup,
            string book,
            string firstName,
            string lastName,
            string function,
            string phoneNumber,
            string faxNumber,
            string comment,
            string actUserId,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookContactSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlParameter paramFirstName = dbCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50);
                paramFirstName.Value = firstName;

                SqlParameter paramLastName = dbCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50);
                paramLastName.Value = lastName;

                SqlParameter paramFunction = dbCmd.Parameters.Add("@Function", SqlDbType.VarChar, 50);
                paramFunction.Value = function;

                if (!phoneNumber.Equals(""))
                {
                    SqlParameter paramPhoneNumber = dbCmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50);
                    paramPhoneNumber.Value = phoneNumber;
                }

                if (!faxNumber.Equals(""))
                {
                    SqlParameter paramFaxNumber = dbCmd.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 50);
                    paramFaxNumber.Value = faxNumber;
                }

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                    paramComment.Value = comment;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

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

        public static void BookCreditLimitSet(
            string bizDate,
            string bookGroup,
            string bookParent,
            string book,
            string borrowLimitAmount,
            string loanLimitAmount,
            string actUserId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookCreditLimitSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBookParent = dbCmd.Parameters.Add("@BookParent", SqlDbType.VarChar, 10);
                paramBookParent.Value = bookParent;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                if (!borrowLimitAmount.Equals(""))
                {
                    SqlParameter paramBorrowLimitAmount = dbCmd.Parameters.Add("@BorrowLimitAmount", SqlDbType.BigInt);
                    paramBorrowLimitAmount.Value = borrowLimitAmount;
                }

                if (!loanLimitAmount.Equals(""))
                {
                    SqlParameter paramLoanLimitAmount = dbCmd.Parameters.Add("@LoanLimitAmount", SqlDbType.BigInt);
                    paramLoanLimitAmount.Value = loanLimitAmount;
                }

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

        public static DataSet BookCreditLimitsGet(
            string bizDate,
            string bookGroup,
            string bookParent,
            string book,
            short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookCreditLimitsGet", dbCn);
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

                if (!bookParent.Equals(""))
                {
                    SqlParameter paramBookParent = dbCmd.Parameters.Add("@BookParent", SqlDbType.VarChar, 10);
                    paramBookParent.Value = bookParent;
                }

                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
                }

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "BookCreditLimits");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void BookGroupSet(
            string bookGroup,
            string bookName,
            string timeZoneId,
            string bizDate,
            string bizDateContract,
            string bizDateBank,
            string bizDateExchange,
            string bizDatePrior,
            string bizDatePriorBank,
            string bizDatePriorExchange,
            string bizDateNext,
            string bizDateNextBank,
            string bizDateNextExchange,
            bool useWeekends,
            string settlementType)
        {

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookGroupSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                if (!bookName.Equals(""))
                {
                    SqlParameter paramBookName = dbCmd.Parameters.Add("@BookName", SqlDbType.VarChar, 50);
                    paramBookName.Value = bookName;
                }

                if (!timeZoneId.Equals(""))
                {
                    SqlParameter paramTimeZoneId = dbCmd.Parameters.Add("@TimeZoneId", SqlDbType.Int);
                    paramTimeZoneId.Value = int.Parse(timeZoneId);
                }

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }

                if (!bizDateContract.Equals(""))
                {
                    SqlParameter paramBizDateContract = dbCmd.Parameters.Add("@BizDateContract", SqlDbType.DateTime);
                    paramBizDateContract.Value = bizDateContract;
                }

                if (!bizDateBank.Equals(""))
                {
                    SqlParameter paramBizDateBank = dbCmd.Parameters.Add("@BizDateBank", SqlDbType.DateTime);
                    paramBizDateBank.Value = bizDateBank;
                }

                if (!bizDateExchange.Equals(""))
                {
                    SqlParameter paramBizDateExchange = dbCmd.Parameters.Add("@BizDateExchange", SqlDbType.DateTime);
                    paramBizDateExchange.Value = bizDateExchange;
                }

                if (!bizDatePrior.Equals(""))
                {
                    SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                    paramBizDatePrior.Value = bizDatePrior;
                }

                if (!bizDatePriorBank.Equals(""))
                {
                    SqlParameter paramBizDatePriorBank = dbCmd.Parameters.Add("@BizDatePriorBank", SqlDbType.DateTime);
                    paramBizDatePriorBank.Value = bizDatePriorBank;
                }

                if (!bizDatePriorExchange.Equals(""))
                {
                    SqlParameter paramBizDatePriorExchange = dbCmd.Parameters.Add("@BizDatePriorExchange", SqlDbType.DateTime);
                    paramBizDatePriorExchange.Value = bizDatePriorExchange;
                }

                if (!bizDateNext.Equals(""))
                {
                    SqlParameter paramBizDateNext = dbCmd.Parameters.Add("@BizDateNext", SqlDbType.DateTime);
                    paramBizDateNext.Value = bizDateNext;
                }

                if (!bizDateNextBank.Equals(""))
                {
                    SqlParameter paramBizDateNextBank = dbCmd.Parameters.Add("@BizDateNextBank", SqlDbType.DateTime);
                    paramBizDateNextBank.Value = bizDateNextBank;
                }

                if (!bizDateNextExchange.Equals(""))
                {
                    SqlParameter paramBizDateNextExchange = dbCmd.Parameters.Add("@BizDateNextExchange", SqlDbType.DateTime);
                    paramBizDateNextExchange.Value = bizDateNextExchange;
                }

                SqlParameter paramUseWeekends = dbCmd.Parameters.Add("@UseWeekends", SqlDbType.Bit);
                paramUseWeekends.Value = useWeekends;

                if (!settlementType.Equals(""))
                {
                    SqlParameter paramSettlementType = dbCmd.Parameters.Add("@SettlementType", SqlDbType.VarChar, 10);
                    paramSettlementType.Value = settlementType;
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

        public static void BookFundSet(string bookGroup, string book, string currencyIso, string fund, string actUserId, bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookFundSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                if (!currencyIso.Equals(""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                if (!fund.Equals(""))
                {
                    SqlParameter paramFund = dbCmd.Parameters.Add("@Fund", SqlDbType.VarChar, 6);
                    paramFund.Value = fund;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

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

        public static DataSet BookFundsGet(string bookGroup, string book, string currencyIso)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookFundsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
                }

                if (!currencyIso.Equals(""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "BookFunds");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet BookGroupsGet(string bookGroup, string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookGroupsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "BookGroups");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void BookGroupsRoll(string bizDate, string bizDatePrior)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookGroupsRoll", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

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

        public static void BookSet(
            string bookGroup,
            string bookParent,
            string book,
            string bookName,
            string addressLine1,
            string addressLine2,
            string addressLine3,
            string phoneNumber,
            string faxNumber,
            string marginBorrow,
            string marginLoan,
            string markRoundHouse,
            string markRoundInstitution,
            string rateStockBorrow,
            string rateStockLoan,
            string rateBondBorrow,
            string rateBondLoan,
            string countryCode,
            string fundDefault,
            string priceMin,
            string amountMin,
            string actUserId,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBookParent = dbCmd.Parameters.Add("@BookParent", SqlDbType.VarChar, 10);
                paramBookParent.Value = bookParent;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                if (!bookName.Equals(""))
                {
                    SqlParameter paramBookName = dbCmd.Parameters.Add("@BookName", SqlDbType.VarChar, 50);
                    paramBookName.Value = bookName;
                }
                if (!addressLine1.Equals(""))
                {
                    SqlParameter paramAddressLine1 = dbCmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 50);
                    paramAddressLine1.Value = addressLine1;
                }
                if (!addressLine2.Equals(""))
                {
                    SqlParameter paramAddressLine2 = dbCmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 50);
                    paramAddressLine2.Value = addressLine2;
                }
                if (!addressLine3.Equals(""))
                {
                    SqlParameter paramAddressLine3 = dbCmd.Parameters.Add("@AddressLine3", SqlDbType.VarChar, 50);
                    paramAddressLine3.Value = addressLine3;
                }
                if (!phoneNumber.Equals(""))
                {
                    SqlParameter paramPhoneNumber = dbCmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 25);
                    paramPhoneNumber.Value = phoneNumber;
                }
                if (!faxNumber.Equals(""))
                {
                    SqlParameter paramFaxNumber = dbCmd.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 25);
                    paramFaxNumber.Value = faxNumber;
                }
                if (!marginBorrow.Equals(""))
                {
                    SqlParameter paramMarginBorrow = dbCmd.Parameters.Add("@MarginBorrow", SqlDbType.Float);
                    paramMarginBorrow.Value = marginBorrow;
                }
                if (!marginLoan.Equals(""))
                {
                    SqlParameter paramMarginLoan = dbCmd.Parameters.Add("@MarginLoan", SqlDbType.Float);
                    paramMarginLoan.Value = marginLoan;
                }
                if (!markRoundHouse.Equals(""))
                {
                    SqlParameter paramMarkRoundHouse = dbCmd.Parameters.Add("@MarkRoundHouse", SqlDbType.Char, 3);
                    paramMarkRoundHouse.Value = markRoundHouse;
                }
                if (!markRoundInstitution.Equals(""))
                {
                    SqlParameter paramMarkRoundInstitution = dbCmd.Parameters.Add("@MarkRoundInstitution", SqlDbType.Char, 3);
                    paramMarkRoundInstitution.Value = markRoundInstitution;
                }
                if (!rateStockBorrow.Equals(""))
                {
                    SqlParameter paramRateStockBorrow = dbCmd.Parameters.Add("@RateStockBorrow", SqlDbType.Decimal);
                    paramRateStockBorrow.Value = rateStockBorrow;
                }
                if (!rateStockLoan.Equals(""))
                {
                    SqlParameter paramRateStockLoan = dbCmd.Parameters.Add("@RateStockLoan", SqlDbType.Decimal);
                    paramRateStockLoan.Value = rateStockLoan;
                }
                if (!rateBondBorrow.Equals(""))
                {
                    SqlParameter paramRateBondBorrow = dbCmd.Parameters.Add("@RateBondBorrow", SqlDbType.Decimal);
                    paramRateBondBorrow.Value = rateBondBorrow;
                }
                if (!rateBondLoan.Equals(""))
                {
                    SqlParameter paramRateBondLoan = dbCmd.Parameters.Add("@RateBondLoan", SqlDbType.Decimal);
                    paramRateBondLoan.Value = rateBondLoan;
                }
                if (!countryCode.Equals(""))
                {
                    SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                    paramCountryCode.Value = countryCode;
                }
                if (!fundDefault.Equals(""))
                {
                    SqlParameter paramFundDefault = dbCmd.Parameters.Add("@FundDefault", SqlDbType.VarChar, 6);
                    paramFundDefault.Value = fundDefault;
                }
                if (!priceMin.Equals(""))
                {
                    SqlParameter paramPriceMin = dbCmd.Parameters.Add("@PriceMin", SqlDbType.Float);
                    paramPriceMin.Value = priceMin;
                }
                if (!amountMin.Equals(""))
                {
                    SqlParameter paramAmountMin = dbCmd.Parameters.Add("@AmountMin", SqlDbType.Float);
                    paramAmountMin.Value = amountMin;
                }
                if (!actUserId.Equals(""))
                {
                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = actUserId;
                }

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
