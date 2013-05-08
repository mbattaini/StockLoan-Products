// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2005  All rights reserved.

using System;
using System.Data;

namespace Anetics.Process
{
  public delegate void HeartbeatEventHandler(HeartbeatEventArgs e); 
  public delegate void ProcessStatusEventHandler(ProcessStatusEventArgs e);
  
  public enum HeartbeatStatus
  {
    Alert,
    Normal,
    Stopping,
    Unknown
  };

	public interface IProcess
	{
		event HeartbeatEventHandler HeartbeatEvent;    
		event ProcessStatusEventHandler ProcessStatusEvent;
	  
		void	AutoBorrowOrder(			
			string  bookGroup,            
			string  book,
			string  secId,						
			long    quantity,			
			string	collateralCode,
			string	comments,
			decimal	rateMin,
			string  rateMinCode,
			string  quantityMin,
			string  priceMax,
			string  waitTime,
			bool		incomeTracked,
			decimal	divRate,
			bool		bookFlag,
			string  batchCode,
			string  poolCode,
			string  marginCode,
			decimal	margin,
			string	actUserId);
		
		void BankLoanPledgeRelease(	
			string  messageId,
			string  bookGroup,            
			string  book,
			string  loanDate,
			string  recordType,
			string  secId,						
			long    quantity,			
			decimal amount,
			string  loanPurpose,
			string  releaseType,
			string  hypothecation,
			string  preventPendIndicator,
			string  cnsIndicator,
			string  ipoIndicator,
			string  ptaIndicator,
			string  occParticipantNumber,
			string  occNumber,
			string  comment,
			string  dtcInputSequence,
			string	actUserId);

		void ContractAdd(
			string  dealId,
			string  bookGroup,
			string  dealType,
			string  secId,
			string  book,
			long    quantity,
			decimal amount,
			string  collateralCode,
			string  expiryDate,
			decimal rate,
			string  rateCode,
			string  poolCode,
			string  marginCode,
			decimal margin,
			decimal negotiatedNewRate,
			string  comment,
			string  otherBook,
			decimal fixedInvesmtmentRate,
			bool    incomeTracked,
			decimal divRate,
			string  actUserId);
    
		void ContractAdd(
			string  dealId,
			string  bookGroup,
			string  dealType,
			string  secId,
			string  book,      
			long    quantity,
			decimal amount,
			string  collateralCode,
			string  expiryDate,
			decimal rate,
			string  rateCode,
			string  poolCode,
			string  marginCode,
			decimal margin,
			decimal negotiatedNewRate,
			string  comment,
			string  otherBook,
			decimal fixedInvesmtmentRate,
			string  deliveryLocation,
			string  deliveryDate,
			bool    incomeTracked,
			decimal divRate,
			bool    divCallable,
			string  currencyIso,
			string  cashDepot,
			string  exchange,
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
			string  poolCode,
			string  effectiveDate,
			string  actUserId);

		void Return(
			string  bookGroup,
			string  contractType,
			string  contractId,
			long    returnQuantity,            
			decimal returnAmount,
			string  callbackRequired,
			string  recDelLocation,
			string  cashDepot,
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

		void Recall(
			string bookGroup,
			string contractType,
			string book,
			string contractId,
			string recallDate,
			int    recallSequence,
			long   recallQuantity,
			string buyinDate,
			string zeroInterestDate,
			string terminationIndicator,
			string recallReasonCode,
			string recallId,
			string comment,      
			string actUserId,
			bool   delete);

		void MemoSegUpdate(
			string  messageId,
			string  bookGroup,            
			string  secId,
			long		quantity,
			string	actionCode,
			string	dtcSerialNumber,
			string	dtcInputSequence,
			string	comments,
			string  actUserId);
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
    private string  book;
    private string  contractId;
    private string  contractType;
    private string  secId;
    private string  symbol;
		private string	quantity;
		private	string	amount;
    private string  status;
    private string  statusTime;
    private string  tag;

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
		string	quantity,
		string	amount,
        string  status,
        string  statusTime,
        string  tag
      )
    { 
      this.processId    = processId.Trim();
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
	  this.quantity		= quantity;
	  this.amount		= amount;
      this.symbol       = symbol; 
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
        return actTime;
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
        return statusTime;
      }
    }

    public string Tag
    {
      get
      {
        return tag;
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
  public class HeartbeatEventArgs : EventArgs
  {
    private HeartbeatStatus status;
    private string alert;

    public HeartbeatEventArgs(HeartbeatStatus status, string alert)
    {
      this.status = status;
      this.alert = alert;
    }

    public HeartbeatStatus Status
    {
      get
      {
        return status;
      }
    }

    public string Alert
    {
      get
      {
        return alert;
      }
    }
  }
  
  public class HeartbeatEventWrapper : MarshalByRefObject
  {
    public event HeartbeatEventHandler HeartbeatEvent;
  
    public void DoEvent(HeartbeatEventArgs e)
    {
      HeartbeatEvent(e);      
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}