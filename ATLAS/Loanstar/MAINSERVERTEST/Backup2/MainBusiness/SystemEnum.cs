using System;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.MainBusiness
{
    public enum BalanceTypes
    {
        Credit,
        Debit
    }

    public enum BillingType
    {
        Fee,
        Rebate
    }

    public enum InformationType
    {
        None,
        Deals,
        Contracts,
        Recalls,
        Returns,
        Marks
    }

    public enum SettlementType
    {
        Pending,
        Settled,
        SettledToday,
        None,
        All
    }

    public enum ContractType
    {
        Borrows,
        Loans,
        None,
        All
    }

    public enum InformationTimeType
    {
        Asof,
        Current
    }

    public enum UserAllowances
    {
        AddNew,
        Update,
        Delete,
        View,
        All,
        None
    }

    public enum SecurityTypes
    {
        Isin,
        Sedol,
        Cusip,
        Symbol,
        Unknown
    }
}
