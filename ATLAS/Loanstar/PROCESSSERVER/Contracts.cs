// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using StockLoan.Common;

namespace StockLoan.Process
{
  public struct Contract
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
  
  public class Contracts
  {
    private Filer filer;

    private string bizDate = "0001-01-01";
    private string clientId = "0000";
    private float fundingRate = 0F;

    private ArrayList contracts;

    public Contracts()
    {
      filer = new Filer(ProcessMain.TempPath);
      contracts = new ArrayList();
    }

    ~Contracts()
    {
      filer.Dispose();
    }

    public void Load(string remotePathName, string hostName, string userId, string password)
    {
      Contract contract;
      int contractCount = 0;

      BinaryReader binaryReader;
      char[] c = new char[140];

      binaryReader = new BinaryReader(filer.StreamGet(remotePathName, hostName, userId, password), System.Text.Encoding.ASCII);
      binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
     
      try
      {
        while(binaryReader.Read(c, 0, 140) > 0)
        {
          if (c[0].Equals('B') || c[0].Equals('L'))
          {
            contract = new Contract();

            contract.ContractId = new String(c, 52, 9);
            contract.ContractType = new String(c, 0, 1);
            contract.ContraClientId = new String(c, 1, 4);
            contract.SecId = new String(c, 5, 9).Trim();
            contract.Quantity = long.Parse(new String(c, 18, 9));
            contract.Amount = (double)(long.Parse(new String(c, 30, 12)) / 100D);
            contract.CollateralCode = new String(c, 69, 1).Trim();
            contract.ValueDate = new String(c, 110, 4) + "-" + new String(c, 106, 2) + "-" + new String(c, 108, 2);
            contract.SettleDate = new String(c, 48, 4) + "-" + new String(c, 44, 2) + "-" + new String(c, 46, 2);
            contract.TermDate = new String(c, 74, 4) + "-" + new String(c, 70, 2) + "-" + new String(c, 72, 2);
            contract.Rate = (double)(int.Parse(new String(c, 61, 5)) / 1000D);
            
            if (c[68].Equals('N'))
            {
              contract.Rate = contract.Rate * (-1);
            }
            
            contract.RateCode = new String(c, 68, 1).Trim();
            contract.StatusFlag = new String(c, 78, 1).Trim();
            contract.PoolCode = new String(c, 79, 1).Trim();
            contract.DivRate = (decimal)(int.Parse(new String(c, 80, 6)) / 1000D);
            contract.DivCallable = c[86].Equals('C');
            contract.IncomeTracked = !c[94].Equals('N');
            contract.MarginCode = new String(c, 95, 1).Trim();
            
            if (c[96].Equals(' '))
            {
              contract.Margin = 1.00D;
            }
            else
            {
              contract.Margin = (double)(int.Parse(new String(c, 96, 3)) / 100D);
            }
            
            if (c[99].Equals(' '))
            {
              contract.CurrencyIso = "USD";
            }
            else
            {
              contract.CurrencyIso = new String(c, 99, 3);
            }
            
            contract.SecurityDepot = new String(c, 102, 2).Trim();
            contract.CashDepot = new String(c, 104, 2).Trim();
            contract.OtherClientId = new String(c, 134, 4).Trim();
            contract.Comment = new String(c, 114, 20).Trim();
      
            contracts.Add(contract);
          }
          else if (c[0].Equals('*') && c[1].Equals('H'))
          {
            clientId = new String(c, 4, 4);
            bizDate = new String(c, 12, 4) + "-" + new String(c, 8, 2) + "-" + new String(c, 10, 2);
            fundingRate = float.Parse(new String(c, 19, 2) + "." + new String(c, 21, 3));
          }
          else if (c[0].Equals('*') && c[1].Equals('T'))
          {
            contractCount = int.Parse(new String(c, 8, 6));
          }
          else
          {
            throw (new Exception("Format error in data file. [Position.Load]"));
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

      if (!contractCount.Equals(contracts.Count))
      {
        throw (new Exception("Parity error: loaded " + contracts.Count + " contracts while expecting "  + contractCount + ". [Position.Load]"));
      }
    }

    public Contract Contract(int index)
    {
      return (Contract)contracts[index];
    }

    public string ClientId
    {
      get 
      {
        return clientId; 
      }
    }

    public string BizDate
    {
      get 
      {
        return bizDate; 
      }
    }

    public int Count
    {
      get 
      {
        return contracts.Count;
      }
    }

    public float FundingRate
    {
      get 
      {
        return fundingRate;
      }
    }
  }  
}
