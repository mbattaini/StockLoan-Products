// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using StockLoan.Common;

namespace StockLoan.Process
{
  public struct ClientLongItem
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
  
  public class ClientsLong
  {
    private Filer filer;

    private string bizDate = "0001-01-01";
    private string clientId = "0000";

    private ArrayList clientList;

    public ClientsLong()
    {
      filer = new Filer(ProcessMain.TempPath);
      clientList = new ArrayList();
    }

    ~ClientsLong()
    {
      filer.Dispose();
    }

    public void Load(string remotePathName, string hostName, string userId, string password)
    {
      ClientLongItem clientLongItem = new ClientLongItem();
      int itemCount = 0;
      
      BinaryReader binaryReader;
      char[] c = new char[540];
      
      binaryReader = new BinaryReader(filer.StreamGet(remotePathName, hostName, userId, password), System.Text.Encoding.ASCII);
      binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
     
      try
      {
        while(binaryReader.Read(c, 0, 540) > 0)
        {
          if (c[0].Equals('1')) // Header record.
          {
            clientId = new String(c, 1, 4);
            bizDate = new String(c, 5, 4) + "-" + new String(c, 9, 2) + "-" + new String(c, 11, 2);
          }
          else if (c[0].Equals('9')) // Trailer record.
          {
            itemCount = int.Parse(new String(c, 5, 9));
          }
          else if (c[0].Equals('2')) // Detail record.
          {
            clientLongItem = new ClientLongItem();

            clientLongItem.ContraClientId = new String(c, 1, 4);
            clientLongItem.AccountName = new String(c, 5, 30).Trim();
            clientLongItem.AddressLine1 = new String(c, 35, 30).Trim();
            clientLongItem.AddressLine2 = new String(c, 65, 30).Trim();
            clientLongItem.AddressLine3 = new String(c, 95, 30).Trim();
            clientLongItem.AddressLine4 = new String(c, 125, 30).Trim();
            clientLongItem.AddressLine5 = new String(c, 155, 30).Trim();
                    
            clientLongItem.Phone = new String(c, 185, 14);
            clientLongItem.TaxId = new String(c, 199, 9).Trim();
            clientLongItem.ContraClientDtc = new String(c, 208, 4);
            clientLongItem.ThirdPartyInstructions = new String(c, 213, 17).Trim();
            clientLongItem.DeliveryInstructions = new String(c, 229, 30).Trim();
            clientLongItem.MarkDtc = new String(c, 259, 4);
            clientLongItem.MarkInstructions = new String(c, 263, 30).Trim();
            clientLongItem.RecallDtc = new String(c, 293, 5);
            clientLongItem.CdxCuId = new String(c, 298, 4);
            
            clientLongItem.OccDelivery = new String(c, 302, 1);

            clientLongItem.ParentAccount = new String(c, 303, 4);
            clientLongItem.AssociatedAccount = new String(c, 307, 4);
            clientLongItem.CreditLimitAccount = new String(c, 311, 4);
            clientLongItem.AssociatedCbAccount = new String(c, 315, 4);
            
            clientLongItem.BorrowLimit = new String(c, 319, 14);
            clientLongItem.BorrowDateChange = new String(c, 333, 8);
            clientLongItem.BorrowSecLimit = new String(c, 341, 14);
            clientLongItem.BorrowSecDateChange = new String(c, 355, 8);
            clientLongItem.LoanLimit = new String(c, 363, 14);
            clientLongItem.LoanDateChange = new String(c, 377, 8);
            clientLongItem.LoanSecLimit = new String(c, 385, 14);
            clientLongItem.LoanSecDateChange = new String(c, 399, 8);
            
            clientLongItem.MinMarkAmount = new String(c, 407, 4);
            clientLongItem.MinMarkPrice = new String(c, 411, 1);
            clientLongItem.MarkRoundHouse = new String(c, 412, 1);
            clientLongItem.MarkValueHouse = new String(c, 413, 4);
            clientLongItem.MarkRoundInstitution = new String(c, 417, 1);
            clientLongItem.MarkValueInstitution = new String(c, 418, 4);

            clientLongItem.BorrowMarkCode = new String(c, 422, 1);
            clientLongItem.BorrowCollateral = new String(c, 423, 3);
            clientLongItem.LoanMarkCode = new String(c, 426, 1);
            clientLongItem.LoanCollateral = new String(c, 427, 3);

            clientLongItem.IncludeAccrued = new String(c, 430, 1);

            clientLongItem.StandardMark = new String(c, 431, 1);
            clientLongItem.OmnibusMark = new String(c, 432, 1);

            clientLongItem.StockBorrowRate = new String(c, 433, 5);
            clientLongItem.StockLoanRate = new String(c, 438, 5);
            clientLongItem.BondBorrowRate = new String(c, 443, 5);
            clientLongItem.BondLoanRate = new String(c, 448, 5);

            clientLongItem.BusinessIndex = new String(c, 453, 2);
            clientLongItem.BusinessAmount = new String(c, 455, 18);
            clientLongItem.InstitutionalCashPool = new String(c, 473, 1);
            clientLongItem.InstitutionalFeeType = new String(c, 474, 1);
            clientLongItem.InstitutionalFeeRate = new String(c, 475, 5);

            clientLongItem.LoanEquity = new String(c, 480, 1);
            clientLongItem.LoanDebt = new String(c, 481, 1);
            clientLongItem.ReturnEquity = new String(c, 482, 1);
            clientLongItem.ReturnDebt = new String(c, 483, 1);
            clientLongItem.IncomeEquity = new String(c, 484, 1);
            clientLongItem.IncomeDebt = new String(c, 485, 1);

            clientLongItem.DayBasis = new String(c, 486, 1);
            clientLongItem.AccountClass = new String(c, 487, 3);

            clientList.Add(clientLongItem);
          }
          else
          {
            throw (new Exception("Format error in data file. [ClientsLong.Load]"));      
          }
        }
      }
      catch
      {
        throw;
      }
      finally
      {
        binaryReader.Close();
      }

      if (!itemCount.Equals(clientList.Count))
      {
        throw (new Exception("Parity error: Loaded " + clientList.Count + " items while expecting "  + itemCount + ". [ClientsLong.Load]"));
      }
    }

    public ClientLongItem ClientLongItem(int index)
    {
      return (ClientLongItem) clientList[index];
    }

    public string BizDate
    {
      get 
      {
        return bizDate; 
      }
    }

    public string ClientId
    {
      get 
      {
        return clientId; 
      }
    }

    public int Count
    {
      get 
      { 
        return clientList.Count;
      }
    }
  }
}