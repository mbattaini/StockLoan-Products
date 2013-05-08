using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class Books
    {

        public static DataSet BooksGet(string bookGroup, string book)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBBooks.BooksGet(bookGroup, book);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void BookClearingInstructionSet(
            string bookGroup, 
            string book, 
            string countryCode, 
            string currencyIso, 
            string divRate,
            string cashInstructions, 
            string securityInstructions, 
            string ActUserId, 
            bool isActive)
        {
            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group value is required");
                }
                
                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }
            
                if (countryCode.Equals(""))
                {
                    throw new Exception("Country Code value is required");
                }
                
                if (currencyIso.Equals(""))
                {
                    throw new Exception("Currency Iso value is required");
                }
                
                if (divRate.ToString().Equals("") || divRate.Equals(null))
                {
                    throw new Exception("Div Rate is required");
                }

                DBBooks.BookClearingInstructionSet(
                    bookGroup, 
                    book, 
                    countryCode, 
                    currencyIso, 
                    divRate, 
                    cashInstructions, 
                    securityInstructions, 
                    ActUserId, 
                    isActive);
            }
            catch 
            {
                throw;
            }
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

            try
            {

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group value is required");
                }
                
                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }
                
                if (firstName.Equals(""))
                {
                    throw new Exception("First Name value is required");
                }
                
                if (lastName.Equals(""))
                {
                    throw new Exception("Last Name value is required");
                }
                
                if (function.Equals(""))
                {
                    throw new Exception("Function Value is required");
                }

                DBBooks.BookContactSet(
                    bookGroup, 
                    book, 
                    firstName, 
                    lastName, 
                    function, 
                    phoneNumber, 
                    faxNumber, 
                    comment, 
                    actUserId, 
                    isActive);

            }
            catch 
            {
                throw;
            }
        }

        public static DataSet BookClearingInstructionsGet(string bookGroup, string book)
        {
            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group value is required");
                }
                
                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }

                DataSet dsTemp = new DataSet();
                dsTemp = DBBooks.BookClearingInstructionsGet(bookGroup, book);

                return dsTemp;
            }
            catch 
            {
                throw;
            }

        }

        public static DataSet BookContactsGet(string bookGroup, string book, short utcOffSet)
        {

            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group value is required");
                }

                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }

                DataSet dsTemp = new DataSet();
                dsTemp = DBBooks.BookContactsGet(bookGroup, book, utcOffSet);

                return dsTemp;
            }
            catch 
            {
                throw;
            }

        }

        public static DataSet BookCreditLimitsGet(string bizDate, string bookGroup, string bookParent, string book, short utcOffSet)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = DBBooks.BookCreditLimitsGet(bizDate, bookGroup, bookParent, book, utcOffSet);

                return dsTemp;
            }
            catch 
            {
                throw;
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
            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group value is required");
                }

                if (bookParent.Equals(""))
                {
                    throw new Exception("Book Parent value is required");
                }

                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID value is required");
                }

                DBBooks.BookCreditLimitSet(
                    bizDate, 
                    bookGroup, 
                    bookParent, 
                    book, 
                    borrowLimitAmount, 
                    loanLimitAmount, 
                    actUserId);

            }
            catch
            {
                throw;
            }
        }        
            
        public static void BookFundSet(string bookGroup, string book, string actUserId, string currencyIso, string fund, bool isActive)
        {
            try
            {

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group value is required");
                }

                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID value is required");
                }

                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID value is required");
                }

                DBBooks.BookFundSet(bookGroup, book, currencyIso, fund, actUserId, isActive);

            }
            catch 
            {
                throw;
            }
        }

        public static DataSet BookGroupsGet(string bookGroup, string bizDate)
        {
            try
            {
                DataSet dsBook = new DataSet();
                
                dsBook = DBBooks.BookGroupsGet(bookGroup, bizDate);

                return dsBook;
            }
            catch 
            {
                throw;
            }
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

            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to set Book Group");
                }

                DBBooks.BookGroupSet(
                    bookGroup, 
                    bookName, 
                    timeZoneId, 
                    bizDate, 
                    bizDateContract, 
                    bizDateBank, 
                    bizDateExchange, 
                    bizDatePrior,
                    bizDatePriorBank, 
                    bizDatePriorExchange, 
                    bizDateNext, 
                    bizDateNextBank, 
                    bizDateExchange, 
                    useWeekends, 
                    settlementType);
            }
            catch
            {
                throw;
            }
        }
 
        public static DataSet BookFundsGet(string bookGroup, string book, string currencyIso)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBBooks.BookFundsGet(bookGroup, book, currencyIso);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void BookGroupRoll(string bizDate, string bizDatePrior)
        {

            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date value is required");
                }

                if (bizDatePrior.Equals(""))
                {
                    throw new Exception("Prior Biz Date value is required");
                }

                DBBooks.BookGroupsRoll(bizDate, bizDatePrior);

            }
            catch 
            {
                throw;
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
            string roundInstitution, 
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
            try
            {

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group value is required");
                }

                if (bookParent.Equals(""))
                {
                    throw new Exception("Book Parent value is required");
                }
                
                if (book.Equals(""))
                {
                    throw new Exception("Book value is required");
                }
                
                DBBooks.BookSet(
                    bookGroup, 
                    bookParent, 
                    book, 
                    bookName, 
                    addressLine1, 
                    addressLine2, 
                    addressLine3, 
                    phoneNumber, 
                    faxNumber, 
                    marginBorrow, 
                    marginLoan, 
                    markRoundHouse,
                    roundInstitution, 
                    rateStockBorrow, 
                    rateStockLoan, 
                    rateBondBorrow, 
                    rateBondLoan, 
                    countryCode, 
                    fundDefault, 
                    priceMin, 
                    amountMin, 
                    actUserId, 
                    isActive);

            }
            catch 
            {
                throw;
            }
        }

    }

}
