using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockLoan.WorldWideTradeDataService
{
    public struct ContractItem
    {
        public string ContractId;
        public string ContractType;
        public string ContraClientId;
        public string SecId;
        public long Quantity;
        public double Amount;
        public string CollateralCode;
        public string ValueDate;
        public string SettleDate;
        public string TermDate;
        public double Rate;
        public string RateCode;
        public string StatusFlag;
        public string PoolCode;
        public decimal DivRate;
        public bool DivCallable;
        public bool IncomeTracked;
        public string MarginCode;
        public double Margin;
        public string CurrencyIso;
        public string SecurityDepot;
        public string CashDepot;
        public string OtherClientId;
        public string Comment;
    }

    public struct RecallItem
    {
        public string ClientId;
        public string ContractId;
        public string ContractType;
        public string ContraClientId;
        public string SecId;
        public long Quantity;
        public string RecallDate;
        public string BuyInDate;
        public string Status;
        public string ReasonCode;
        public string RecallId;
        public short SequenceNumber;
        public string Comment;
    }

    public struct MarkItem
    {
        public string ClientId;
        public string ContractId;
        public string ContractType;
        public decimal Amount;
        public string Direction;
    }

    public struct ClientItem
    {
        public string ContraClientId;
        public string AccountName;
        public string AddressLine1;
        public string AddressLine2;
        public string AddressLine3;
        public string AddressLine4;
        public string AddressLine5;

        public string Phone;
        public string TaxId;
        public string ContraClientDtc;
        public string ThirdPartyInstructions;
        public string DeliveryInstructions;
        public string MarkDtc;
        public string MarkInstructions;
        public string RecallDtc;
        public string CdxCuId;

        public string OccDelivery;

        public string ParentAccount;
        public string AssociatedAccount;
        public string CreditLimitAccount;
        public string AssociatedCbAccount;

        public string BorrowLimit;
        public string BorrowDateChange;
        public string BorrowSecLimit;
        public string BorrowSecDateChange;
        public string LoanLimit;
        public string LoanDateChange;
        public string LoanSecLimit;
        public string LoanSecDateChange;

        public string MinMarkAmount;
        public string MinMarkPrice;
        public string MarkRoundHouse;
        public string MarkRoundInstitution;
        public string MarkValueHouse;
        public string MarkValueInstitution;

        public string BorrowMarkCode;
        public string BorrowCollateral;
        public string LoanMarkCode;
        public string LoanCollateral;

        public string IncludeAccrued;

        public string StandardMark;
        public string OmnibusMark;

        public string StockBorrowRate;
        public string StockLoanRate;
        public string BondBorrowRate;
        public string BondLoanRate;

        public string BusinessIndex;
        public string BusinessAmount;
        public string InstitutionalCashPool;
        public string InstitutionalFeeType;
        public string InstitutionalFeeRate;

        public string LoanEquity;
        public string LoanDebt;
        public string ReturnEquity;
        public string ReturnDebt;
        public string IncomeEquity;
        public string IncomeDebt;

        public string DayBasis;
        public string AccountClass;
    }
}
