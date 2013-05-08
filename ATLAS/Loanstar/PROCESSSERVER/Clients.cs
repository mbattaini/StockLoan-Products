// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using StockLoan.Common;

namespace StockLoan.Process
{
  public struct ClientItem
  {
    public string ContraClientId;
    public string ContraClientDtc;
    public string MinMarkAmount;
    public string MinMarkPrice;
    public string MarkRoundHouse;
    public string MarkRoundInstitution;
    public string AccountName;
    public string AddressLine2;

    public string ParamsApply;
    public string BorrowMarkCode;
    public string BorrowCollateralCode;
    public string LoanMarkCode;
    public string LoanCollateralCode;
    public string AddressLine3;
    public string AddressLine4;
    public string RelatedAccount;

    public string BorrowLimit;
    public string BorrowDateChange;
    public string LoanLimit;
    public string LoanDateChange;
    public string BorrowSecLimit;
    public string BorrowSecDateChange;
    public string LoanSecLimit;
    public string LoanSecDateChange;
    public string TaxId;

    public string StockBorrowRate;
    public string StockLoanRate;
    public string BondBorrowRate;
    public string BondLoanRate;
    public string DtcMarkNumber;
    public string CreditLimitAccount;
    public string CallbackAccount; 
    public string ThirdPartyInstructions;
    public string AdditionalAddress;
    public string OccAccountFlag;
  }
  
  public class Clients
  {
    private Filer filer;

    private string bizDate = "0001-01-01";
    private string clientId = "0000";

    private ArrayList clientList;

    public Clients()
    {
      filer = new Filer(ProcessMain.TempPath);
      clientList = new ArrayList();
    }

    ~Clients()
    {
      filer.Dispose();
    }

    public void Load(string remotePathName, string hostName, string userId, string password)
    {
      ClientItem clientItem = new ClientItem();
      int itemCount = 0;
      
      BinaryReader binaryReader;
      char[] c = new char[80];
      
      binaryReader = new BinaryReader(filer.StreamGet(remotePathName, hostName, userId, password), System.Text.Encoding.ASCII);
      binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
     
      try
      {
        while(binaryReader.Read(c, 0, 80) > 0)
        {
          if (c[0].Equals('*') && c[1].Equals('H')) // Header record.
          {
            clientId = new String(c, 4, 4);
            bizDate = "20" + new String(c, 12, 2) + "-" + new String(c, 8, 2) + "-" + new String(c, 10, 2);
          }
          else if (c[0].Equals('*') && c[1].Equals('T')) // Trailer record.
          {
            itemCount = int.Parse(new String(c, 8, 6)) / 4; // Four detail records per client item.
          }
          else if (c[4].Equals('1')) // Detail record #1.
          {
            clientItem = new ClientItem();

            clientItem.ContraClientId = new String(c, 0, 4);
            clientItem.ContraClientDtc = new String(c, 5, 4);
            clientItem.MinMarkAmount = new String(c, 9, 4);
            clientItem.MinMarkPrice = new String(c, 13, 1);
            clientItem.MarkRoundHouse = new String(c, 14, 3);
            clientItem.MarkRoundInstitution = new String(c, 17, 3);
            clientItem.AccountName = new String(c, 20, 30).Trim();
            clientItem.AddressLine2 = new String(c,50, 30).Trim();
          }
          else if (c[4].Equals('2')) // Detail record #2.
          {
            clientItem.ParamsApply = new String(c, 5, 1);
            clientItem.BorrowMarkCode = new String(c, 6, 1);
            clientItem.BorrowCollateralCode = new String(c, 7, 3);
            clientItem.LoanMarkCode = new String(c, 10, 1);
            clientItem.LoanCollateralCode = new String(c, 11, 3);
            clientItem.AddressLine3 = new String(c, 14, 30).Trim();
            clientItem.AddressLine4 = new String(c, 44, 30).Trim();
            clientItem.RelatedAccount = new String(c, 74, 4).Trim();
          }
          else if (c[4].Equals('3')) // Detail record #3.
          {
            clientItem.BorrowLimit = new String(c, 5, 10);
            clientItem.BorrowDateChange = new String(c, 15, 6);
            clientItem.LoanLimit = new String(c, 21, 10);
            clientItem.LoanDateChange = new String(c, 31, 6);
            clientItem.BorrowSecLimit = new String(c, 37, 10);
            clientItem.BorrowSecDateChange = new String(c, 47, 6);
            clientItem.LoanSecLimit = new String(c, 54, 10);
            clientItem.LoanSecDateChange = new String(c, 64, 6);
            clientItem.TaxId = new String(c, 69, 9).Trim();
          }
          else if (c[4].Equals('4')) // Detail record #4.
          {
            clientItem.StockBorrowRate = new String(c, 5, 5);
            clientItem.StockLoanRate = new String(c, 10, 5);
            clientItem.BondBorrowRate = new String(c, 15, 5);
            clientItem.BondLoanRate = new String(c, 20, 5);
            clientItem.DtcMarkNumber = new String(c, 25, 4);
            clientItem.CreditLimitAccount = new String(c, 29, 4).Trim();
            clientItem.CallbackAccount = new String(c, 33, 4).Trim();
            clientItem.ThirdPartyInstructions = new String(c, 37, 17).Trim();
            clientItem.AdditionalAddress = new String(c, 54, 25).Trim();
            clientItem.OccAccountFlag = new String(c, 79, 1).Trim();
            
            clientList.Add(clientItem);
          }
          else
          {
            throw (new Exception("Format error in data file. [Clients.Load]"));      
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
        throw (new Exception("Parity error: Loaded " + clientList.Count + " items while expecting "  + itemCount + ". [Clients.Load]"));
      }
    }

    public ClientItem ClientItem(int index)
    {
      return (ClientItem) clientList[index];
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