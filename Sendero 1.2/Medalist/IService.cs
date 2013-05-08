// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Data;

namespace Anetics.Medalist
{
  public delegate void HeartbeatEventHandler(HeartbeatEventArgs e);
  public delegate void DeskQuipEventHandler(DeskQuipEventArgs e);
  
  public enum HeartbeatStatus
  {
    Alert,
    Normal,
    Stopping,
    Unknown
  };

  public interface IService
  {  
    event HeartbeatEventHandler HeartbeatEvent;
    event DeskQuipEventHandler DeskQuipEvent;
    
    string BizDate();
    string BizDateNext();
    string BizDatePrior();    
    
    string BizDateBank();
    string BizDateNextBank();
    string BizDatePriorBank();    
    
    string BizDateExchange();
    string BizDateNextExchange();
    string BizDatePriorExchange();    
    
		bool	 IsSubstitutionActive();

    string ContractsBizDate();

    string NewProcessId(string prefix);
        
    void KeyValueSet(string keyId, string keyValue);
    string KeyValueGet(string keyId, string keyValueDefault);
  
    DataSet KeyValueGet();
    
    DataSet ProcessStatusGet(short utcOffset);
    
    DataSet FirmGet();
    DataSet CountryGet();
    DataSet DeskTypeGet();
        
    DataSet DeskGet();
    DataSet DeskGet(string desk);    
    DataSet DeskGet(bool isNotSubscriber);    
  
    DataSet BookGroupGet();
    DataSet BookGroupGet(string userId, string functionPath);

    DataSet SecMasterLookup(string secId);
    DataSet SecMasterLookup(string secId, bool withBox);    
    DataSet SecMasterLookup(string secId, bool withBox, bool withDeskQuips, short utcOffset, string since);    

    DataSet DeskQuipGet(short utcOffset);
    DataSet DeskQuipGet(short utcOffset, string secId);

    void DeskQuipSet(
      string secId,
      string deskQuip,
      string actUserId);    

    DataSet InventoryDataMaskGet(string desk);

    void    InventoryDataMaskSet(
      string  desk, 
      short   recordLength, 
      char    headerFlag, 
      char    dataFlag, 
      char    trailerFlag, 
      short   accountLocation, 
      char    delimiter, 
      short   accountOrdinal, 
      short   secIdOrdinal, 
      short   quantityOrdinal, 
      short   recordCountOrdinal, 
      short   accountPosition, 
      short   accountLength, 
      short   bizDateDD, 
      short   bizDateMM, 
      short   bizDateYY, 
      short   secIdPosition, 
      short   secIdLength, 
      short   quantityPosition, 
      short   quantityLength, 
      short   recordCountPosition, 
      short   recordCountLength,
      string  actUserId);  
    
    DataSet SubscriberListGet(short utcOffset);
    
    void    SubscriberListSet(
      string desk,  
      string ftpPath, 
      string ftpHost,
      string ftpUserName,
      string ftpPassword,
      string loadExPGP, 
      string comment, 
      string mailAddress, 
      string mailSubject, 
      string isActive, 
      string usePGP,
			bool	 isBizDatePrior,
      string actUserId);

    DataSet PublisherListGet(short utcOffset);

		void PublisherListSet(
			string	desk, 
			string	ftpPath, 
			string	ftpHost,
			string	ftpUserName,
			string	ftpPassword,
			string	loadExPGP, 
			string	comment, 
			string	mailAddress, 
			string	mailSubject, 
			string	isActive, 
			string	usePGP,
			string	reportName,
			string	reportFrequency,
			string	reportWaitUntil,
			string	actUserId);    		
		
		void BorrowHardSet(string secId, string actUserId, bool delete);
    void BorrowNoSet(string secId, string actUserId, bool delete);
  
    DataSet InventoryDeskInputDataGet();
  }
  
  [Serializable]
  public class DeskQuipEventArgs : EventArgs
  {
    private string secId;
    private string symbol;
    private string deskQuip;
    private string actUserShortName;
    private string actTime;
    private short  utcOffset = 0;

    public DeskQuipEventArgs(
      string secId,
      string symbol,
      string deskQuip,    
      string actUserShortName,
      string actTime)
    {
      this.secId = secId;
      this.symbol = symbol;
      this.deskQuip = deskQuip;
      this.actUserShortName = actUserShortName;
      this.actTime = actTime;
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

    public string DeskQuip
    {
      get
      {
        return deskQuip;
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
        object[] values = new object[5];
          
        values[0] = SecId;
        values[1] = Symbol;          
        values[2] = DeskQuip;
        values[3] = ActUserShortName;
        values[4] = ActTime;

        return values;
      }
    }
  }

  public class DeskQuipEventWrapper : MarshalByRefObject
  {
    public event DeskQuipEventHandler DeskQuipEvent;
  
    public void DoEvent(DeskQuipEventArgs e)
    {
      DeskQuipEvent(e);
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }

  [Serializable]
  public class HeartbeatEventArgs : EventArgs
  {
    private HeartbeatStatus mainStatus;
    private string mainAlert;
    private HeartbeatStatus processStatus;
    private string processAlert;

    public HeartbeatEventArgs(HeartbeatStatus mainStatus, string mainAlert,
      HeartbeatStatus processStatus, string processAlert)
    {
      this.mainStatus = mainStatus;
      this.mainAlert = mainAlert;
      this.processStatus = processStatus;
      this.processAlert = processAlert;
    }

    public HeartbeatStatus MainStatus
    {
      get
      {
        return mainStatus;
      }
    }

    public string MainAlert
    {
      get
      {
        return mainAlert;
      }
    }

    public HeartbeatStatus ProcessStatus
    {
      get
      {
        return processStatus;
      }
    }

    public string ProcessAlert
    {
      get
      {
        return processAlert;
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
