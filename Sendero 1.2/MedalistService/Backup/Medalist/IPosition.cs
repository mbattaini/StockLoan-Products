// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Data;

namespace Anetics.Medalist
{
  public delegate void DealEventHandler(DealEventArgs e);
  public delegate void BankLoanActivityEventHandler (BankLoanActivityEventArgs e);
  public delegate void ContractEventHandler(ContractEventArgs e);
  public delegate void RecallEventHandler(RecallEventArgs e);
  public delegate void ProcessStatusEventHandler(ProcessStatusEventArgs e);
	
	public interface IPosition
	{
		event DealEventHandler DealEvent;
		event BankLoanActivityEventHandler  BankLoanActivityEvent;
		event ContractEventHandler ContractEvent;
		event RecallEventHandler  RecallEvent;
		event ProcessStatusEventHandler ProcessStatusEvent;		
				
		bool BlockedSecId(string secId);
		
		DataSet AccountPositionGet(string secId, bool isActive);
		DataSet AccountPositionGet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, string secId, bool isActive);

		DataSet	AccountsGet(string groupCode);
		DataSet	AccountsGet(int utcOffset);
		DataSet AccountsGet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, int utcOffset);
		void		AccountSet(string firm, string locMemo, string accountType, string accountNumber,  string currencyCode, string actUserId, bool isActive);

		string AutoBorrowListSend(string bizDate, string bookGroup, string listName);
		DataSet	AutoBorrowDataGet(string bizDate,	short utcOffset, string userId, string functionPath);
		
		DataSet	AutoBorrowItemsGet(int utcOffset, string bizDate);
		void		AutoBorrowItemSet(string bookGroup, string listName, string secId, string quantity, string collateralCode, string rateMin, string rateMinCode, string priceMax, bool incomeTracked, string margin, string marginCode, string divRate, string comment, string actUserId);
		
		DataSet	AutoBorrowListsGet(int utcOffSet, string bizDate);
		void		AutoBorrowListSet(string bookGroup, string listName, string books, string waitTime, bool bookContract, string batchCode, string poolCode, int itemCount, int filled, string listStatus, string actUserId);
		
		DataSet	AutoBorrowResultsGet(int utcOffSet, string bizDate);		
		
		DataSet BankLoanReportsGet(int utcOffset);
		void BankLoanReportSet(
			string bookGroup, 
			string reportName,
			string reportType,
			string reportHost, 
			string reportHostUserId, 
			string reportHostPassword, 
			string reportPath,
			string fileLoadDate,
			string actUserId);
		
		DataSet BankLoanReportsDataGet();
		
		DataSet	BankLoanReportsDataMaskGet(string bookGroup, string reportName, int utcOffset);
		void	BankLoanReportsDataMaskSet(
			string bookGroup, 
			string reportName, 
			string headerFlag,
			string dataFlag,
			string trailerFlag,
			int reportNamePosition,
			int reportNameLength,
			int	reportDatePosition,
			int reportDateLength,
			int	secIdPosition,
			int	secIdLength,
			int quantityPosition,
			int quantityLength,
			int activityPosition,
			int activitylength,
			string ignoreItems,
			int lineLength,
			string actUserId);

		DataSet BankLoanPledgeSummaryGet(string bizDate);
		DataSet BankLoanReleaseSummaryGet(string secId);

		DataSet BankLoanActivityGet(int utcOffset);
		DataSet BankLoansGet(int utcOffset);
		void BankLoanSet(string bookGroup, string book, string loanDate, string loanType, string activityType, string hairCut, string spMin, string moodyMin, string priceMin, string loanAmount, string comment, string actUserId, bool isActive);			
		
		DataSet BankLoanDataGet(short utcOffset, string userId, string functionPath);
		void BankSet(string bookGroup, string book, string name, string contact, string phone, string fax, string actUserId, bool isActive);
		
		void BankLoanPledgeSet(
			string bookGroup, 
			string book,	
			string loanDate, 		  
			string processId, 
			string loanType,
			string activityType,
			string secId, 
			string quantity, 
			string flag,
			string status, 
			string actUserId);
		
		void BankLoanReleaseSet( 
			string bookGroup, 		  
			string book, 
			string processId, 
			string loanDate, 
			string loanType, 
			string activityType,
			string secId, 
			string quantity, 
			string flag,
			string status, 
			string actUserId);
		
		
		DataSet	ContractRateHistoryGet(string bookGroup, string contractType, string secId);
		
		bool  FaxEnabled();	
		
		string FutureBizDate(int numberOfDays);        
    
		DataSet ContractRateComparisonDataGet(string userId, string functionPath);
		DataSet ContractRateComparisonGet(string bizDate, string bookGroup, string book, string contractType);
		
		long CreditGet(string bookGroup, string book, string contractType);
		bool CreditCheck(string bookGroup, string book, decimal amount, string contractType);
        
		DataSet DealDataGet(short utcOffset, string userId, string functionPath, string isActive);
		DataSet DealDataGet(short utcOffset, string dealIdPrefix, string userId, string functionPath, string isActive);
		DataSet DealDataGet(string bookGroup, string book, string dealType);


		string DealListSubmit(
			string bookGroup,
			string book,
			string bookContact,
			string dealType,
			string dealIdPrefix,
			string comment,
			string list,
			string actUserId);
    
		void DealSet(
			string dealId,     
			string dealStatus,
			string actUserId,
			bool   isActive    
			);

		void DealSet(
			string  dealId,
			string  bookGroup,
			string  dealType,
			string  book,
			string  bookContact,
			string  secId,
			string  quantity,
			string  amount,
			string  collateralCode,
			string  valueDate,
			string  settleDate,
			string  termDate,
			string  rate,
			string  rateCode,
			string  poolCode,
			string  divRate,
			string  divCallable,
			string  incomeTracked,
			string  margin,
			string  marginCode,
			string  currencyIso,
			string  securityDepot,
			string  cashDepot,
			string  comment,
			string  dealStatus,
			string  actUserId,
			bool    isActive
			);

		void BoxSummarySet(string bookGroup, string secId, bool doNotRecall, string comment, string actUserId);
		DataSet BoxSummaryDataGet(short utcOffset, string userId, string functionPath);
        DataSet BoxSummaryDataGet(short utcOffset, string userId, string functionPath, string bizDate);

		DataSet BoxFailHistoryGet(string secId);

		DataSet DealsDetailDataGet(string BookGroup, string SecId, short UtcOffset);			
	
		DataSet ContractDataGet(short utcOffset);
		DataSet ContractDataGet(short utcOffset, string bizDate, string userId, string functionPath);
		DataSet ContractDataGet(short utcOffset, string bookGroup, string contractId);
		DataSet ContractDataGet(string bookGroup, string book, string contractType);


		void ContractSet( 
			string  bizDate,
			string  bookGroup,
			string  contractId,
			string  contractType);

		void ContractSet(
			string  bizDate,
			string  bookGroup,
			string  contractId,
			string  contractType,
			string  book,
			string  secId,
			string  quantity,
			string  quantitySettled,
			string  amount,
			string  amountSettled,
			string  collateralCode,
			string  valueDate,  
			string  settleDate,
			string  termDate,
			string  rate,
			string  rateCode,
			string  statusFlag,
			string  poolCode,
			string  divRate,
			string  divCallable,
			string  incomeTracked,
			string  marginCode,
			string  margin,
			string  currencyIso,
			string  securityDepot,
			string  cashDepot,
			string  otherBook,
			string  comment);    
    
		string	RecallTermNoticeDocument(string recallId);
		
		DataSet RecallDataGet(short utcOffset);
		DataSet RecallDataGet(short utcOffset, string bizDate, string userId, string functionPath);		
		
		DataSet RecallActivityGet(short utcOffset, string recallId);
		DataSet RecallIndicatorsGet();
    
		DataSet RecallReasonsGet();
		DataSet RecallReasonsGet(string reasonId, string reasonCode);

		void RecallNew(      
			string bookGroup, 
			string contractId, 
			string contractType, 
			string book, 
			string bookContact,
			string secId, 
			string quantity, 
			string indicator,
			string baseDueDate, 
			string moveToDate, 
			string openDateTime, 
			string reasonCode,
			string sequenceNumber,
			string faxStatus,
			string comment,      
			string actUserId);
        
		void RecallDelete(
			string bookGroup,
			string contractType,
			string book,
			string contractId,
			string recallDate,
			int    recallSequence,
			string lenderReference,
			string comment,
			string actUserId);
    
		void RecallSet(
			string recallId, 
			string bookContact,
			string moveToDate, 
			string action,
			string faxId,
			string faxStatus,
			string deliveredToday,
			string willNeed,
			string actUserId,
			string status);

		void RecallBookContactSet(
			string bookGroup,
			string book,
			string bookContact,
			string actUserId);

		string ContractAdd(                        
			string  bookGroup,
			string  contractType,
			string  secId,
			string  book,
			string  quantity,
			string  amount,      
			string  collateralCode,
			string  expiryDate,
			string  rate,
			string  rateCode,
			string  poolCode,
			string  marginCode,
			string  margin,
			string  negotiatedNewRate,
			string  comment,
			string  otherBook,
			string  fixedInvesmtmentRate,
			string  incomeTracked,
			string  divRate,
			string  actUserId);

		void RateChange(      
			string  bookGroup,
			string  contractType,
			string  book,
			string  securityType,
			string  contractId,
			decimal rateOld,
			string  rateCodeOld,
			decimal rateNew,      
			string  rateCodeNew,           
			string  profitCenter,
			string  effectiveDate,
			string  actUserId);

		void Return(
			string bookGroup,
			string contractType,
			string contractId,
			string secId,
			long   returnQuantity,           
			decimal returnAmount,
			string callbackRequired,
			string recDelLocation,
			string cashDepot,
			string actUserId);
  
		void ContractMaintenance(
			string bookGroup,
			string contractId,
			string contractType,
			string book,      
			string poolCode,
			string effectiveDate,
			string deliveryDate,
			string marginCode,
			string margin,
			string divRate,
			string incomeTracked,
			string expiryDate,
			string comment,
			string actUserId);

		void ProcessStatusEventInvoke(ProcessStatusEventArgs processStatusEventArg);
	
	}

  [Serializable]
  public class DealEventArgs : EventArgs
  {
    private string  dealId;    
    private string  bookGroup;
    private string  dealType;
    private string  book;
    private string  bookContact;
    private string  secId;
    private string  symbol;
    private string  quantity;
    private string  amount;
    private string  collateralCode;
    private string  valueDate;
    private string  settleDate;
    private string  termDate;
    private string  rate;
    private string  rateCode;
    private string  poolCode;
    private string  divRate;
    private string  divCallable;
    private string  incomeTracked;
    private string  margin;
    private string  marginCode;
    private string  currencyIso;
    private string  securityDepot;
    private string  cashDepot;
    private string  comment;
    private string  dealStatus;
    private string  actUserId;
    private string  actTime;
    private bool    isActive;
    private short   utcOffset = 0;
  
    public DealEventArgs(      
      string  dealId,      
      string  bookGroup,
      string  dealType,
      string  book,
      string  bookContact,
      string  secId,
      string  symbol,
      string  quantity,
      string  amount,
      string  collateralCode,
      string  valueDate,
      string  settleDate,
      string  termDate,
      string  rate,
      string  rateCode,
      string  poolCode,
      string  divRate,
      string  divCallable,
      string  incomeTracked,
      string  margin,
      string  marginCode,
      string  currencyIso,
      string  securityDepot,
      string  cashDepot,
      string  comment,
      string  dealStatus,
      string  actUserId,
      string  actTime,
      bool    isActive)
    {
      this.dealId         = dealId;      
      this.bookGroup      = bookGroup;
      this.dealType       = dealType;
      this.book           = book;
      this.bookContact    = bookContact;
      this.secId          = secId;
      this.symbol         = symbol;
      this.quantity       = quantity;
      this.amount         = amount;
      this.collateralCode =	collateralCode;
      this.valueDate      = valueDate;
      this.settleDate     = settleDate;
      this.termDate       = termDate;
      this.rate           = rate;
      this.rateCode       = rateCode;
      this.poolCode       =	poolCode;
      this.divRate        = divRate;
      this.divCallable    = divCallable;
      this.incomeTracked  = incomeTracked;
      this.margin         = margin;
      this.marginCode     = marginCode;
      this.currencyIso    = currencyIso;
      this.securityDepot  = securityDepot;
      this.cashDepot      =	cashDepot;
      this.comment        = comment;
      this.dealStatus     = dealStatus;
      this.actUserId      = actUserId;
      this.actTime        = actTime;
      this.isActive       = isActive;
    }
  
    public string DealId
    {
      get
      {
        return dealId;
      }
    }
    
    public string BookGroup
    {
      get
      {
        return bookGroup;
      }
    }

    public string DealType
    {
      get
      {
        return dealType;
      }
    }

    public string Book
    {
      get
      {
        return book;
      }
    }
      
    public string BookContact
    {
      get
      {
        return bookContact;
      }
    }

    public string SecId
    {
      get
      {
        return secId;
      }
    }
    
    public string Symbol
    {
      get
      {
        return symbol;
      }
    }

    public string Quantity
    {
      get
      {
        return quantity;
      }
    }
   
    public string Amount
    {
      get
      {
        return amount;
      }
    }
                  
    public string CollateralCode
    {
      get
      {
        return collateralCode;
      }
    }

    public string ValueDate
    {
      get
      {
        return valueDate;
      }
    }
    
    public string SettleDate
    {
      get
      {
        return settleDate;
      }
    }
    
    public string TermDate
    {
      get
      {
        return termDate;
      }
    }
      
    public string Rate
    {
      get
      {
        return rate;
      }
    }
                    
    public string RateCode
    {
      get
      {
        return rateCode;
      }
    }
                       
    public string PoolCode
    {
      get
      {
        return poolCode;
      }
    }
    
    public string DivRate
    {
      get
      {
        return divRate;
      }
    }     
    
    public string DivCallable
    {
      get
      {
        return divCallable;
      }
    }
    
    public string IncomeTracked
    {
      get
      {
        return incomeTracked;
      }
    }

    public string Margin
    {
      get
      {
        return margin;
      }
    }     
    
    public string MarginCode
    {
      get
      {
        return marginCode;
      }
    }

    public string CurrencyIso
    {
      get
      {
        return currencyIso;
      }
    } 
    
    public string SecurityDepot
    {
      get
      {
        return securityDepot;
      }
    }
    
    public string CashDepot
    {
      get
      {
        return cashDepot;
      }
    }     

    public string Comment
    {
      get
      {
        return comment;
      }
    }     

    public string DealStatus
    {
      get
      {
        return dealStatus;
      }

      set
      {
        dealStatus = value;
      }
    }     

    public string ActUserId
    {
      get
      {
        return actUserId;
      }
    }
    
    public string ActTime
    {
      get
      {
        try
        {
          return DateTime.Parse(actTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        catch
        {
          return actTime;        
        }
      }
    }
    
    public short UtcOffset
    {
      set
      {
        utcOffset = value;
      }
    }

    public bool IsActive
    {
      get
      {
        return isActive;
      }
    }   

    public object [] Values
    {
      get
      {
        object[] rowValues = new object[29];
          
        rowValues[0]  = dealId;        
          
        if(bookGroup.Equals(""))
        {
          rowValues[1] = DBNull.Value;
        }
        else
        {
          rowValues[1] = bookGroup;
        }
          
        if(dealType.Equals(""))
        {
          rowValues[2]  = DBNull.Value;
        }
        else
        {
          rowValues[2]  = dealType;
        }

        if(book.Equals(""))
        {
          rowValues[3]  = DBNull.Value;
        }
        else
        {
          rowValues[3]  = book;
        }
            
        if(bookContact.Equals(""))
        {
          rowValues[4]  = DBNull.Value;
        }
        else
        {
          rowValues[4]  = bookContact;
        }
          
        if(secId.Equals(""))
        {
          rowValues[5] = DBNull.Value;
        }
        else
        {
          rowValues[5]  = secId;
        }
          
        if(symbol.Equals(""))
        {
          rowValues[6]  = DBNull.Value;
        }
        else
        {
          rowValues[6]  = symbol;
        }

        if(quantity.Equals(""))
        {
          rowValues[7]  = DBNull.Value;
        }
        else
        {
          rowValues[7]  = quantity;
        }
          
        if(amount.Equals(""))
        {
          rowValues[8]  = DBNull.Value;
        }
        else
        {
          rowValues[8]  = amount;
        }

        if(collateralCode.Equals(DBNull.Value))
        {
          rowValues[9]  = DBNull.Value;
        }
        else
        {
          rowValues[9]  = collateralCode;
        }

        if(valueDate.Equals(""))
        {
          rowValues[10] = DBNull.Value;
        }
        else
        {
          rowValues[10] = valueDate;
        }
          
        if(settleDate.Equals(""))
        {
          rowValues[11] = DBNull.Value;
        }
        else
        {
          rowValues[11] = settleDate;
        }
          
        if(termDate.Equals(""))
        {
          rowValues[12] = DBNull.Value;
        }
        else
        {
          rowValues[12] = termDate;
        }
                 
        if(rate.Equals(""))
        {
          rowValues[13] = DBNull.Value;
        }
        else
        {
          rowValues[13] = rate;
        }

        if(rateCode.Equals(""))
        {
          rowValues[14] = DBNull.Value;
        }
        else
        {
          rowValues[14] = rateCode;
        }

        if(poolCode.Equals(""))
        {
          rowValues[15] = DBNull.Value;
        }
        else
        {
          rowValues[15] = poolCode;
        }

        if(divRate.Equals(""))
        {
          rowValues[16] = DBNull.Value;
        }
        else
        {
          rowValues[16] = divRate;
        }
            
        if(divCallable.Equals(""))
        {
          rowValues[17] = DBNull.Value;
        }
        else
        {
          rowValues[17] = divCallable;
        }
            
        if(incomeTracked.Equals(""))
        {
          rowValues[18] = DBNull.Value;
        }
        else
        {
          rowValues[18] = incomeTracked;
        }
            
        if(margin.Equals(""))
        {
          rowValues[19] = DBNull.Value;     
        }
        else
        {
          rowValues[19] = margin;
        }

        if(marginCode.Equals(""))
        {
          rowValues[20] = DBNull.Value;
        }
        else
        { 
          rowValues[20] = marginCode;
        }
            
        if(currencyIso.Equals(""))
        {
          rowValues[21] = DBNull.Value;
        }
        else
        {
          rowValues[21] = currencyIso;
        }
            
        if(securityDepot.Equals(""))
        {
          rowValues[22] = DBNull.Value;
        }
        else
        {
          rowValues[22] = securityDepot;
        }
            
        if(cashDepot.Equals(""))
        {
          rowValues[23] = DBNull.Value;
        }
        else
        {
          rowValues[23] = cashDepot;
        }
            
        if(comment.Equals(""))
        {
          rowValues[24] = DBNull.Value;
        }
        else
        {
          rowValues[24] = comment;
        }
          
        rowValues[25] = dealStatus;
        rowValues[26] = actUserId;
        rowValues[27] = ActTime;
        rowValues[28] = isActive;          

        return rowValues;
      }
    }   
  }

  public class DealEventWrapper : MarshalByRefObject
  {
    public event DealEventHandler DealEvent;
  
    public void DoEvent(DealEventArgs e)
    {
      DealEvent(e);
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
  
	[Serializable]
	public class BankLoanActivityEventArgs : EventArgs
	{
		private string bookGroup;
		private string book;
		private string loanDate;
		private string processId;				
		private string secId;
		private string symbol;
		private long   quantity;
		private decimal amount;
		private string status;
		private string actUserId;
		private string flag;
		private string actTime;
		private short  utcOffset = 0;	
  
		public BankLoanActivityEventArgs(
			string bookGroup,
			string book,
			string loanDate,			
			string processId,
			string secId,
			string symbol,
			long   quantity,
			decimal amount,
			string flag,
			string status,
			string actUserId,
			string actTime)
		{
			this.bookGroup		= bookGroup;
			this.processId		= processId;
			this.book					= book;
			this.loanDate			= loanDate;
			this.secId				= secId;
			this.symbol				= symbol;
			this.quantity			= quantity;
			this.amount				= amount;
			this.flag					= flag;
			this.status				= status;
			this.actUserId		= actUserId;
			this.actTime			= actTime;
		}

		public string BookGroup
		{
			get
			{
				return bookGroup;
			}
		}
		
		public string ProcessId
		{
			get
			{
				return processId;
			}
		}
		
		public string Book
		{
			get
			{
				return book;
			}
		}

		public string LoanDate
		{
			get
			{
				return loanDate;
			}
		}
		
		public string SecId
		{
			get
			{
				return secId;
			}
		}

		public string Symbol
		{
			get
			{
				return symbol;
			}
		}

		public long Quantity
		{
			get
			{
				return quantity;
			}
		}

		public decimal Amount
		{
			get
			{
				return amount;
			}
		}

		public string Flag
		{
			get
			{
				return flag;
			}
		}

		public string Status
		{
			get
			{
				return status;
			}

			set
			{
				status = value;
			}
		}

		public string ActUserId
		{
			get
			{
				return actUserId;
			}
		}
    
		public string ActTime
		{
			get
			{
				try
				{
					return DateTime.Parse(actTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");
				}
				catch
				{
					return actTime;        
				}
			}
		}
    
		public short UtcOffset
		{
			set
			{
				utcOffset = value;
			}
		}	

		public object [] Values
		{
			get
			{
				object[] rowValues = new object[12];
          
				rowValues[0] = BookGroup;
				rowValues[1] = Book;
				rowValues[2] = LoanDate;
				rowValues[3] = ProcessId;								
				rowValues[4] = SecId;
				rowValues[5] = Symbol;
				rowValues[6] = Quantity;
				rowValues[7] = Amount;
				rowValues[8] = Flag;
				rowValues[9] = Status;
				rowValues[10] = ActUserId;
				rowValues[11] = ActTime;
              
				return rowValues;
			}
		}   
	}

	public class BankLoanActivityWrapper : MarshalByRefObject
	{
		public event BankLoanActivityEventHandler BankLoanActivityEvent;
  
		public void DoEvent(BankLoanActivityEventArgs e)
		{
			BankLoanActivityEvent(e);
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}

  [Serializable]
  public class ContractEventArgs : EventArgs
  {
    private string  bizDate;
    private string  bookGroup;
    private string  bookParent;
    private string  contractId;
    private string  contractType;
    private string  book;
    private string  secId;
    private string  symbol;
    private string  classCode;
    private string  className;
    private string  quantity;
    private string  quantitySettled;
    private string  isSettledQuantity;
    private string  quantityRecalled;
    private string  hasRecall;
    private string  amount;
    private string  amountSettled;
    private string  isSettledAmount;
    private string  collateralCode;
    private string  valueDate;
    private string  settleDate;
    private string  termDate;
    private string  rebateRate;
    private string  feeRate;
    private string  rate;
    private string  rateCode;
    private string  statusFlag;
    private string  poolCode;
    private string  divRate;
    private string  divCallable;
    private string  incomeTracked;
    private string  marginCode;
    private string  margin;
    private string  currencyIso;
    private string  securityDepot;
    private string  cashDepot;
    private string  otherBook;
    private string  comment;
    private string  isEasy;
    private string  isHard;
    private string  isNoLend;
    private string  isThreshold;
    private string  valueAmount;
    private string  valueIsEstimate;
    private string  valueRatio;
    private string  income;

    public ContractEventArgs(      
      string  bizDate,
      string  bookGroup,
      string  bookParent,
      string  contractId,
      string  contractType,
      string  book,
      string  secId,
      string  symbol,
      string  classCode,
      string  className,
      string  quantity,
      string  quantitySettled,
      string  isSettledQuantity,
      string  quantityRecalled,
      string  hasRecall,
      string  amount,
      string  amountSettled,
      string  isSettledAmount,
      string  collateralCode,
      string  valueDate,
      string  settleDate,
      string  termDate,
      string  rebateRate,
      string  feeRate,
      string  rate,
      string  rateCode,
      string  statusFlag,
      string  poolCode,
      string  divRate,
      string  divCallable,
      string  incomeTracked,
      string  marginCode,
      string  margin,
      string  currencyIso,
      string  securityDepot,
      string  cashDepot,
      string  otherBook,
      string  comment,
      string  isEasy,
      string  isHard,
      string  isNoLend,
      string  isThreshold,
      string  valueAmount,
      string  valueIsEstimate,
      string  valueRatio,
      string  income
      )
    { 
      this.bizDate            = bizDate;
      this.bookGroup          = bookGroup;
      this.bookParent         = bookParent;
      this.contractId         = contractId;
      this.contractType       = contractType;
      this.book               = book;
      this.secId              = secId; 
      this.symbol             = symbol;
      this.classCode          = classCode; 
      this.className          = className; 
      this.quantity           = quantity;
      this.quantitySettled    = quantitySettled;
      this.isSettledQuantity  = isSettledQuantity;
      this.quantityRecalled   = quantityRecalled;
      this.hasRecall          = hasRecall;
      this.amount             = amount;
      this.amountSettled      = amountSettled;
      this.isSettledAmount    = isSettledAmount;
      this.collateralCode     =	collateralCode;
      this.valueDate          = valueDate;
      this.settleDate         = settleDate;
      this.termDate           = termDate;
      this.rebateRate         = rebateRate;
      this.feeRate            = feeRate;
      this.rate               = rate;
      this.rateCode           = rateCode;
      this.statusFlag         = statusFlag;
      this.poolCode           =	poolCode;
      this.divRate            = divRate;
      this.divCallable        = divCallable;
      this.incomeTracked      = incomeTracked;
      this.marginCode         = marginCode;
      this.margin             = margin;
      this.currencyIso        = currencyIso;
      this.securityDepot      = securityDepot;
      this.cashDepot          =	cashDepot;
      this.otherBook          = otherBook;
      this.comment            = comment;      
      this.isEasy             = isEasy;
      this.isHard             = isHard;
      this.isNoLend           = isNoLend;
      this.isThreshold        = isThreshold;
      this.valueAmount        = valueAmount;
      this.valueIsEstimate    = valueIsEstimate;
      this.valueRatio         = valueRatio;
      this.income             = income;
    }
  
    public string BizDate
    {
      get
      {
        return bizDate;
      }
    }

    public string BookGroup
    {
      get
      {
        return bookGroup;
      }
    }

    public string BookParent
    {
      get
      {
        return bookParent;
      }
    }

    public string ContractId
    {
      get
      {
        return contractId;
      }
    }
   
    public string ContractType
    {
      get
      {
        return contractType;
      }
    }

    public string Book
    {
      get
      {
        return book;
      }
    }
      
    public string SecId
    {
      get
      {
        return secId;
      }
    }

    public string Symbol
    {
      get
      {
        return symbol;
      }
    }

    public string ClassCode
    {
      get
      {
        return classCode;
      }
    }

    public string ClassName
    {
      get
      {
        return className;
      }
    }

    public string Quantity
    {
      get
      {
        return quantity;
      }
    }

    public string QuantitySettled
    {
      get
      {
        return quantitySettled;
      }
    }
   
    public string IsSettledQuantity
    {
      get
      {
        return isSettledQuantity;
      }
    }
   
    public string QuantityRecalled
    {
      get
      {
        return quantityRecalled;
      }
    }
   
    public string HasRecall
    {
      get
      {
        return hasRecall;
      }
    }
   
    public string Amount
    {
      get
      {
        return amount;
      }
    }
     
    public string AmountSettled
    {
      get
      {
        return amountSettled;
      }
    }
         
    public string IsSettledAmount
    {
      get
      {
        return isSettledAmount;
      }
    }
         
    public string CollateralCode
    {
      get
      {
        return collateralCode;
      }
    }

    public string ValueDate
    {
      get
      {
        return valueDate;
      }
    }
    
    public string SettleDate
    {
      get
      {
        return settleDate;
      }
    }
    
    public string TermDate
    {
      get
      {
        return termDate;
      }
    }
      
    public string RebateRate
    {
      get
      {
        return rebateRate;
      }
    }
                    
    public string FeeRate
    {
      get
      {
        return feeRate;
      }
    }
                    
    public string Rate
    {
      get
      {
        return rate;
      }
    }
                    
    public string RateCode
    {
      get
      {
        return rateCode;
      }
    }

    public string StatusFlag
    {
      get
      {
        return statusFlag;
      }
  
      set
      {
        statusFlag = value;
      }
    }
                       
    public string PoolCode
    {
      get
      {
        return poolCode;
      }
    }
    
    public string DivRate
    {
      get
      {
        return divRate;
      }
    }     
    
    public string DivCallable
    {
      get
      {
        return divCallable;
      }
    }
    
    public string IncomeTracked
    {
      get
      {
        return incomeTracked;
      }
    }

    public string MarginCode
    {
      get
      {
        return marginCode;
      }
    }

    public string Margin
    {
      get
      {
        return margin;
      }
    }     
    
    public string CurrencyIso
    {
      get
      {
        return currencyIso;
      }
    } 
    
    public string SecurityDepot
    {
      get
      {
        return securityDepot;
      }
    }
    
    public string CashDepot
    {
      get
      {
        return cashDepot;
      }
    }     

    public string OtherBook
    {
      get
      {
        return otherBook;
      }
    }     

    public string Comment
    {
      get
      {
        return comment;
      }
    }     

    public string IsEasy
    {
      get
      {
        return isEasy;
      }
    }     

    public string IsHard
    {
      get
      {
        return isHard;
      }
    }     

    public string IsNoLend
    {
      get
      {
        return isNoLend;
      }
    }     

    public string IsThreshold
    {
      get
      {
        return isThreshold;
      }
    }     

    public string ValueAmount
    {
      get
      {
        return valueAmount;
      }
    }     

    public string ValueIsEstimate
    {
      get
      {
        return valueIsEstimate;
      }
    }     

    public string ValueRatio
    {
      get
      {
        return valueRatio;
      }
    }     

    public string Income
    {
      get
      {
        return income;
      }
    }

    public object [] Values
    {
      get
      {
        object[] values = new object[46];
          
        values[0]  = bizDate;
        values[1]  = bookGroup;
        values[2]  = bookParent;
        values[3]  = contractId;
        values[4]  = contractType;
        values[5]  = book;
        values[6]  = secId;
        values[7]  = symbol;
        values[8]  = classCode;
        values[9]  = className;
        values[10]  = quantity;
        values[11]  = quantitySettled;
        values[12]  = isSettledQuantity;
        values[13]  = quantityRecalled;
        values[14]  = hasRecall;
        values[15]  = amount;
        values[16]  = amountSettled;
        values[17]  = isSettledAmount;
        values[18]  = collateralCode;

        if(valueDate.Equals(""))
        {
          values[19] = DBNull.Value;
        }
        else
        {
          values[19] = valueDate;
        }

        if(settleDate.Equals(""))
        {
          values[20] = DBNull.Value;
        }
        else
        {
          values[20] = settleDate;
        }

        if(termDate.Equals(""))
        {
          values[21] = DBNull.Value;
        }
        else
        {
          values[21] = termDate;
        }

        values[22]  = rebateRate;
        values[23]  = feeRate;
        values[24]  = rate;
        values[25]  = rateCode;
        values[26]  = statusFlag;
        values[27]  = poolCode;
        values[28]  = divRate;
        values[29]  = divCallable;
        values[30]  = incomeTracked;
        values[31]  = marginCode;
        values[32]  = margin;
        values[33]  = currencyIso;
        values[34]  = securityDepot;
        values[35]  = cashDepot;
        values[36]  = otherBook;
        values[37]  = comment;
        values[38]  = isEasy;
        values[39]  = isHard;
        values[40]  = isNoLend;
        values[41]  = isThreshold;
        values[42]  = valueAmount;
        values[43]  = valueIsEstimate;
        values[44]  = valueRatio;
        values[45]  = income;
          
        return values;
      }
    }   
  }

  public class ContractEventWrapper : MarshalByRefObject
  {
    public event ContractEventHandler ContractEvent;
  
    public void DoEvent(ContractEventArgs e)
    {
      ContractEvent(e);
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
    
	[Serializable]
	public class ProcessStatusEventArgs : EventArgs
	{
		private string  processId;
		private string  systemCode;
		private string  actCode;
		private string  act;
		private string  actTime;
		private string  actUser;
		private bool    hasError;
		private string  bookGroup;
		private string  contractId;
		private string  contractType;
		private string  book;
		private string  secId;
		private string  symbol;
		private string	quantity;
		private string	amount;
		private string  status;
		private string  statusTime;
		private string  tag;
		private short   utcOffset = 0;
  
		public ProcessStatusEventArgs(
			string  processId,
			string  systemCode,
			string  actCode,
			string  act,
			string  actTime,
			string  actUser,
			bool    hasError,
			string  bookGroup,
			string  contractId,
			string  contractType,
			string  book,
			string  secId,
			string  symbol,
			string  quantity,
			string  amount,
			string  status,
			string  statusTime,
			string  tag
			)
		{ 
			this.processId    = processId;
			this.systemCode   = systemCode; 
			this.actCode      = actCode; 
			this.act          = act;   
			this.actTime      = actTime; 
			this.actUser      = actUser; 
			this.hasError     = hasError; 
			this.bookGroup    = bookGroup; 
			this.contractId   = contractId;
			this.contractType = contractType;
			this.book         = book;
			this.secId        = secId; 
			this.symbol       = symbol;
			this.quantity		= quantity;
			this.amount		= amount;
			this.status       = status; 
			this.statusTime   = statusTime; 
			this.tag          = tag;
		}
  
		public string ProcessId
		{
			get
			{
				return processId;
			}
		}

		public string SystemCode
		{
			get
			{
				return systemCode;
			}
		}
   
		public string ActCode
		{
			get
			{
				return actCode;
			}
		}
    
		public string Act
		{
			get
			{
				return act;
			}
		}

		public string ActTime
		{
			get
			{
				try
				{
					return DateTime.Parse(actTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");
				}
				catch
				{
					return actTime;        
				}
			}
		}
      
		public string ActUser
		{
			get
			{
				return actUser;
			}
		}

		public bool HasError
		{
			get
			{
				return hasError;
			}
		}

		public string BookGroup
		{
			get
			{
				return bookGroup;
			}
		}

		public string ContractId
		{
			get
			{
				return contractId;
			}
		}

		public string ContractType
		{
			get
			{
				return contractType;
			}
		}

		public string Book
		{
			get
			{
				return book;
			}
		}

		public string SecId
		{
			get
			{
				return secId;
			}
		}

		public string Symbol
		{
			get
			{
				return symbol;
			}
		}

		public string Quantity
		{
			get
			{
				return quantity;
			}
		}

		public string Amount
		{
			get
			{
				return amount;
			}
		}

		public string Status
		{
			get
			{
				return status;
			}
		}
    
		public string StatusTime
		{
			get
			{
				try
				{
					return DateTime.Parse(statusTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");
				}
				catch
				{
					return statusTime;        
				}
			}
		}

		public string Tag
		{
			get
			{
				return tag;
			}
		}

		public short UtcOffset
		{
			set
			{
				utcOffset = value;
			}
		}

		public object[] Values
		{
			get
			{
				object[] values = new object[18];
          
				values[0] = ProcessId;
				values[1] = SystemCode;          
				values[2] = ActCode;
				values[3] = Act;
 
				if (actTime.Equals(""))
				{
					values[4] = DBNull.Value;
				}
				else
				{
					values[4] = ActTime;
				}
          
				values[5] = ActUser;
				values[6] = HasError;
				values[7] = BookGroup;          
				values[8] = ContractId;
				values[9] = ContractType;
				values[10] = Book;
				values[11] = SecId;
				values[12] = Symbol;
		
				if (quantity.Equals(""))
				{
					values[13] = DBNull.Value;
				}
				else
				{
					values[13] = Quantity;
				}
		  
	    
				if (amount.Equals(""))
				{
					values[14] = DBNull.Value;
				}
				else
				{
					values[14] = Amount;
				}
		          		
				values[15] = Status;
        
				if (statusTime.Equals(""))
				{
					values[16] = DBNull.Value;
				}
				else
				{
					values[16] = StatusTime;
				}

				values[17] = Tag;
        
				return values;
			}
		}
	}

  public class ProcessStatusEventWrapper : MarshalByRefObject
  {
    public event ProcessStatusEventHandler ProcessStatusEvent;
  
    public void DoEvent(ProcessStatusEventArgs e)
    {
      ProcessStatusEvent(e);
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }

  [Serializable]
  public class RecallEventArgs : EventArgs
  {
    private string    recallId;
    private string    bookGroup;
    private string    contractId;
    private string    contractType;   
    private string    book;
		private string		bookContact;
    private string    secId;
    private string    symbol;
    private bool      isEasy;
    private bool      isHard;
    private bool      isThreshold;
    private bool      isNo;
    private string    days;
    private string    quantity;
    private string    openDateTime;
    private string    baseDueDate;
    private string    deferToDate;
    private string    reasonCode;
    private string    sequenceNumber;
    private string    comment;
    private string    action;
    private string    faxId;
    private string    faxStatus;
    private string    faxStatusTime;
    private string    deliveredToday;
    private string    willNeed;
    private string    actUserShortName;
    private string	  actTime;
    private string	  status;		
    private string [] recallActivity;
    private short     utcOffset = 0;    

    public RecallEventArgs(      
      string   recallId,
      string   bookGroup,
      string   contractId,
      string   contractType,
      string   book,
			string	 bookContact,
      string   secId,
      string   symbol,
      bool     isEasy,
      bool     isHard,
      bool     isThreshold,
      bool     isNo,
      string   days,
      string   quantity,
      string   baseDueDate,
      string   deferToDate,     
      string   openDateTime,
      string   reasonCode,
      string   sequenceNumber,
      string   comment,
      string   action,
      string   faxId,
      string   faxStatus,
      string   faxStatusTime,
      string   deliveredToday,
      string   willNeed,
      string   actUserShortName,
      string   actTime,
      string   status      
    )     
    { 
      this.recallId		      = recallId;
      this.bookGroup		    = bookGroup;
      this.contractId		    = contractId;
      this.contractType	    = contractType;
      this.book			        = book;
      this.bookContact			= bookContact;
			this.secId			      = secId;
      this.symbol			      = symbol;
      this.isEasy			      = isEasy;
      this.isHard			      = isHard;
      this.isThreshold	    = isThreshold;
      this.isNo			        = isNo;
      this.days             = days;
      this.quantity		      = quantity;
      this.openDateTime	    = openDateTime;
      this.baseDueDate	    = baseDueDate;
      this.deferToDate	    = deferToDate;
      this.reasonCode		    = reasonCode;
      this.sequenceNumber   = sequenceNumber;
      this.comment		      = comment;
      this.action           = action;
      this.faxId            = faxId;
      this.faxStatus        = faxStatus;
      this.faxStatusTime    = faxStatusTime;
      this.deliveredToday   = deliveredToday;
      this.willNeed         = willNeed;
      this.actUserShortName = actUserShortName;
      this.actTime		      = actTime;
      this.status			      = status;      
    }
  
    public string   RecallId
    {
      get
      {
        return recallId;
      }
    }

    public string BookGroup
    {
      get
      {
        return bookGroup;
      }
    }

    public string   ContractId
    {
      get
      {
        return contractId;
      }
    }

    public string   ContractType
    {
      get
      {
        return contractType;
      }
    }
    
    public string   Book
    {
      get
      {
        return book;
      }
    }

		public string   BookContact
		{
			get
			{
				return bookContact;
			}
		}

		
		public string  SecId
    {
      get
      {
        return secId;
      }
    }

    public string   Symbol
    {
      get
      {
        return symbol;
      }
    }

    public bool   IsEasy
    {
      get
      {
        return isEasy;
      }
    }

    public bool   IsHard
    {
      get
      {
        return isHard;
      }
    }

    public bool   IsThreshold
    {
      get
      {
        return isThreshold;
      }
    }
     
    public bool   IsNo
    {
      get
      {
        return isNo;
      }
    }

    public string Days
    {
		get
		{          
			return days;               
		}
    }

    public string   Quantity
    {
      get
      {
        return quantity;
      }
    }

    public string   OpenDateTime
    {
      get
      {
        return openDateTime;
      }
    }

    public string   BaseDueDate
    {
      get
      {
        return baseDueDate;
      }
    }

    public string   DeferToDate
    {
      get
      {
        return deferToDate;
      }
    }

		public string   ReasonCode
		{
			get
			{
				return reasonCode;
			}
		}

    public string SequenceNumber
    {
      get
      {      
          return sequenceNumber;       
      }
    }

		public string   ActUserShortName
		{
			get
			{
				return actUserShortName;
			}
		}

		public short UtcOffset
		{
			set
			{
				utcOffset = value;
			}
		}

		public string   Comment
		{
			get
			{
				return comment;
			}
		}

    public string Action
    {
      get
      {
        return action;
      }
    }
		
    public string FaxId
    {
      get
      {
          return faxId;        
      }
    }    

    public string FaxStatus
    {
      get
      {
        return faxStatus;
      }
    }
    
    public string Status
		{
			get
			{
				return status;
			}
		}  

    public string FaxStatusTime
    {
      get
      {
        try
        {
          return DateTime.Parse(faxStatusTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");           
        }
        catch
        {
          return actTime;    
        }		  
      }
    }
    
    public string DeliveredToday
    {
      get
      {
        return deliveredToday;
      }
    }

    public string WillNeed
    {
      get
      {      
          return willNeed;       
      }
    }

		public string ActTime
		{
			get
			{
				try
				{
					return DateTime.Parse(actTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");           
				}
				catch
				{
					return actTime;    
				}		  
			}
		}
	  
		public object [] Values
		{
			get
			{
				object[] values = new object[29];
          
				values[0]   = RecallId;
				values[1]   = BookGroup;        
				values[2]   = ContractId;
				values[3]   = ContractType;
				values[4]   = Book;
				values[5]		= BookContact;
				values[6]   = SecId;
				values[7]   = Symbol;
				values[8]   = IsEasy;
				values[9]   = IsHard;
				values[10]   = IsThreshold;
				values[11]  = IsNo;						

				if (Days.Equals(""))
				{
					values[12]  = DBNull.Value;
				}
				else
				{
					values[12]  = Days;
				}
			
				values[13]  = Quantity;
        
				if(BaseDueDate.Equals(""))
				{
					values[14] = DBNull.Value;
				}
				else
				{
					values[14] = BaseDueDate;
				}

				if(DeferToDate.Equals(""))
				{
					values[15] = DBNull.Value;
				}
				else
				{
					values[15] = DeferToDate;
				}

				if(OpenDateTime.Equals(""))
				{
					values[16] = DBNull.Value;
				}
				else
				{
					values[16] = OpenDateTime;
				}
				
				values[17]  = ReasonCode;

				if (SequenceNumber.Equals(""))
				{
					values[18]  = DBNull.Value;
				}
				else
				{
					values[18]  = SequenceNumber;
				}
			
				values[19]  = Comment;
				values[20]  = Action;
				
				
				if (FaxId.Equals(""))
				{
					values[21]  = DBNull.Value;
				}
				else
				{
					values[21]  = FaxId;
				}
			
				values[22]  = FaxStatus;
				values[23]  = FaxStatusTime;
        
								
				if (DeliveredToday.Equals(""))
				{
					values[24]  = DBNull.Value;
				}
				else
				{
					values[24]  = DeliveredToday;
				}
				       
				if (WillNeed.Equals(""))
				{
					values[25]  = DBNull.Value;
				}
				else
				{
					values[25]  = WillNeed;
				}

				values[26]  = ActUserShortName;
				values[27]  = ActTime;        
				values[28]  = Status;		    
				return values;
			}
		}
  
		public string[] RecallActivity
		{
			get
			{
				return recallActivity;
			}
			set
			{
				recallActivity = value;
			}
		}
	}
  public class RecallEventWrapper : MarshalByRefObject
  {
    public event RecallEventHandler RecallEvent;
  
    public void DoEvent(RecallEventArgs e)
    {
      RecallEvent(e);
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }

}
