// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Data;

namespace Anetics.Medalist
{
  public delegate void LocateEventHandler(LocateEventArgs locateEventArgs);
    
	public interface IShortSale
	{
		event LocateEventHandler LocateEvent;

		string TradeDate();
		string BizDatePrior(string bizDate);

		// PreBorrow Functions

		DataSet LocatesPreBorrowGet(
				string bizDate,
				string groupCode,
				short utcOffset);
		
		void LocatesPreBorrowSet(
				string	bizDate,
				string	groupCode,
				string	secId,
				string	quantity,
				string	rebateRate,
				string	actUserId);
		
		void LocatePreBorrowSubmit(
				long locateId,
				string groupCode,
				string secId,
				string quantity,
				string rate,
				string actUserId);
		
		DataSet TradingGroupsGet(string tradeDate,  short utcOffset);
		void TradingGroupSet (
			string groupCode, 
			string groupName, 
			string minPrice, 
			string autoApprovalMax, 
			string premiumMin, 
			string premiumMax,
			bool	 autoEmail,			
			string emailAddress, 
			string lastEmailDate,	
			string actUserId);

		DataSet TradingGroupsAccountMarkGet(string tradingGroup, short utcOffset);
		void TradingGroupsAccountMarkSet(
			string groupCode,
			string accountNumber,
			string negativeRebateMarkUp,
			string positiveRebateMarkUp,
			string fedFundsMarkUp,
			string liborFundsMarkUp,
			bool delete,
			string actUserId);
	
		
		DataSet TradingGroupsOfficeCodeMarkGet(string tradingGroup, short utcOffset);
		void TradingGroupsOfficeCodeMarkSet(
			string groupCode,
			string officeCode,
			string negativeRebateMarkUp,
			string positiveRebateMarkUp,
			string fedFundsMarkUp,
			string liborFundsMarkUp,
			string actUserId);
	

		DataSet LocateDataGet(string status, short utcOffset);
    
		// For Penson.
		DataSet LocateListGet(short utcOffset, string tradeDate);		
		DataSet LocateListGet(short utcOffset, string tradeDateMin, string tradeDateMax, string groupCode, string secId);

		string LocateSet(string clientId, string groupCode, string clientComment, string secId, string quantity);
		// For Penson end.
    
		string LocateListSubmit(string clientId, string groupCode, string clientComment, string list);

		DataSet LocateDatesGet();    
		DataSet LocateDatesGet(string clientId);    
  
		DataSet LocatesGet(string tradeDate, string clientId, string status, short utcOffset);
		DataSet LocatesGet(string tradeDate, short utcOffset);
		DataSet LocatesGet(string tradeDate, string clientId, short utcOffset);
		DataSet LocatesGet(string tradeDateMin, string tradeDateMax, string groupCode, string secId, short utcOffset);

		DataSet LocateItemGet(string locateId, short utcOffset);
		DataSet LocateItemGet(string groupCode, string locateId, short utcOffset);
		void LocateItemSet(
			long locateId,
			string quantity,
			string source,
			string feeRate,
			string preBorrow,
			string comment,
			string actUserId);
  
		DataSet BorrowHardGet(bool showHistory, short utcOffset);
		void BorrowHardSet(string secId, bool delete, string actUserId);

		DataSet BorrowNoGet(bool showHistory, short utcOffset);
		void BorrowNoSet(string secId, bool delete, string actUserId);

		DataSet ThresholdList(string effectDate);
		DataSet BorrowEasyList(string effectDate, short utcOffset);

		void BorrowEasyListSet(string secId, string actUserId, bool isShortSaleEasy);
		
		string InventoryListSubmit(string desk, string account, string list, string quip, string actUserId);

		DataSet InventoryRatesGet(string bizDate);
		void InventoryRateSet(string secId, string rate, string actUserId);

		
		DataSet InventoryFundingRatesHistoryGet(int listCount, short utcOffset);
		DataSet InventoryFundingRatesGet (string bizDate, short utcOffset);
		void	  InventoryFundingRateSet (string fedFundingRate, string liborFundingRate, string actUserId);

		DataSet InventoryGet(string secId, short utcOffset);
		DataSet InventoryGet(string secId, short utcOffset, bool withHistory);
		DataSet InventoryGet(string groupCode, string secId, short utcOffset);
		DataSet InventoryHistoryLookupGet(string bizDate, string secId);

		DataTable InventoryDeskListGet(string bizDate, string desk);

//----------------------------------------------------------------------------------------------------------------------------------
	
	}

  [Serializable]
  public class LocateEventArgs: EventArgs
  {
    private long   locateId;
    private string tradeDate;
    private string clientId;
    private string groupCode;
    private string secId;
    private string symbol;
    private string clientQuantity;
    private string clientComment;
    private string openTime;
    private string quantity;
    private string feeRate;
    private string comment;
    private string preBorrow;
    private string actUserShortName;
    private string actTime;
    private string source;
    private string status;
    private string groupName;
    private short  utcOffset = 0;
    
    public LocateEventArgs(      
      long   locateId,
      string tradeDate,
      string clientId,    
      string groupCode,
      string secId,
      string symbol,
      string clientQuantity,
      string clientComment,
      string openTime,
      string quantity,
      string feeRate,
      string comment,
      string preBorrow,
      string actUserShortName,
      string actTime,
      string source,
      string status,
      string groupName)
    {
      this.locateId         = locateId;
      this.tradeDate        = tradeDate;    
      this.clientId         = clientId;    
      this.groupCode        = groupCode;    
      this.secId            = secId;
      this.symbol           = symbol;
      this.clientQuantity   = clientQuantity;
      this.clientComment    = clientComment; 
      this.openTime         = openTime;
      this.quantity         = quantity;
      this.feeRate          = feeRate;
      this.comment          = comment;
      this.preBorrow        = preBorrow;
      this.actUserShortName = actUserShortName;
      this.actTime          = actTime;
      this.source           = source;
      this.status           = status;
      this.groupName        = groupName;
    }
  
    public long LocateId
    {
      get
      {
        return locateId;
      }
    }

    public long LocateIdTail
    {
      get
      {
        return locateId % 100000;
      }
    }

    public string TradeDate
    {
      get
      {
        return tradeDate;
      }
    }
   
    public string ClientId
    {
      get
      {
        return clientId;
      }
    }
   
    public string GroupCode
    {
      get
      {
        return groupCode;
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
      
    public string ClientQuantity
    {
      get
      {
        return clientQuantity;
      }
    }
          
    public string ClientComment
    {
      get
      {
        return clientComment;
      }
    }
          
    public string OpenTime
    {
      get
      {
        return openTime;
      }
    }
    
    public   string Quantity
    {
      get
      {
        return quantity;
      }
    }
   
    public string FeeRate
    {
      get
      {
        return feeRate;
      }
    }
                  
    public string Comment
    {
      get
      {
        return comment;
      }
    }

    public string PreBorrow
    {
      get
      {
        return preBorrow;
      }
    }
    
    public string ActUserShortName
    {
      get
      {
        return actUserShortName;
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
                    
    public string Source
    {
      get
      {
        return source;
      }
    }
                       
    public string Status
    {
      get
      {
        return status;
      }
    }                                 

    public string GroupName
    {
      get
      {
        return groupName;
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
        object[] values = new object[19];
          
        values[0] = LocateId;
        values[1] = LocateIdTail;          
        values[2] = TradeDate;
        values[3] = ClientId;
        values[4] = GroupCode;
        values[5] = SecId;
        values[6] = Symbol;          
        values[7] = ClientQuantity;
        values[8] = ClientComment;
        values[9] = OpenTime;

        if (quantity.Equals(""))
        {
          values[10] = DBNull.Value;
        }
        else
        {
          values[10] = Quantity;
        }

        if (feeRate.Equals(""))
        {
          values[11] = DBNull.Value;
        }
        else
        {
          values[11] = FeeRate;
        }

        values[12] = Comment;
        values[13] = PreBorrow;
        values[14] = ActUserShortName;
        
        if (actTime.Equals(""))
        {
          values[15] = DBNull.Value;
        }
        else
        {
          values[15] = ActTime;
        }
          
        values[16] = Source;          
        values[17] = Status;
        values[18] = GroupName;
        
        return values;
      }
    }
  }
  
  public class LocateEventWrapper : MarshalByRefObject
  {
    public event LocateEventHandler LocateEvent;
  
    public void DoEvent(LocateEventArgs e)
    {
      LocateEvent(e);
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
