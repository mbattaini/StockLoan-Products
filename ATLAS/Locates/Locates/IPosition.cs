// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2005  All rights reserved.

using System;
using System.Data;

namespace StockLoan.Locates 
{
    public interface IPosition
    {
        long CreditGet(string bookGroup, string book, string contractType);
        DataSet CurrenciesGet(string bizDate);
       
        DataSet DealDataGet(short utcOffset, string userId, string functionPath, string isActive);
        DataSet DealDataGet(short utcOffset, string dealIdPrefix, string userId, string functionPath, string isActive);
        DataSet DealDataGet(string bookGroup, string book, string dealType);

        void DealSet(
            string dealId,
            string dealStatus,
            string actUserId,
            bool isActive
            );

        void DealSet(
            string dealId,
            string bookGroup,
            string dealType,
            string book,
            string bookContact,
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
            string divCallable,
            string incomeTracked,
            string margin,
            string marginCode,
            string currencyIso,
            string securityDepot,
            string cashDepot,
            string comment,
            string dealStatus,
            string actUserId,
            bool isActive
            );

        DataSet ContractDataGet(short utcOffset);
        DataSet ContractDataGet(short utcOffset, string bizDate, string userId, string functionPath);
        DataSet ContractDataGet(short utcOffset, string bookGroup, string contractId);

        void ContractSet(
            string bizDate,
            string bookGroup,
            string contractId,
            string contractType);

        void ContractSet(
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
            string divCallable,
            string incomeTracked,
            string marginCode,
            string margin,
            string currencyIso,
            string securityDepot,
            string cashDepot,
            string otherBook,
            string comment);
    }
}
