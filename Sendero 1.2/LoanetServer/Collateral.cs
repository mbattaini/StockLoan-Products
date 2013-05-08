// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using Anetics.Common;

namespace Anetics.Loanet
{
  public struct CollateralItem
  {
    public string ClientId;
    public string ContraClientId;
    public string ContractType;
    public string CollateralType;
    public string SecId;
    public long Quantity;
    public double Amount;
    public string CurrencyIso;
    public string ContractId;
    public string ExpiryDate;
  }
  
  public class Collateral
  {
    private Filer filer;

    private string bizDate = "0001-01-01";
    private string clientId = "0000";

    private ArrayList collateralList;

    public Collateral()
    {
      filer = new Filer(LoanetMain.TempPath);
      collateralList = new ArrayList();
    }

    ~Collateral()
    {
      filer.Dispose();
    }

    public void Load(string remotePathName, string hostName, string userId, string password)
    {
      CollateralItem collateralItem;
      int itemCount = 0;

      BinaryReader binaryReader;
      char[] c = new char[80];

      binaryReader = new BinaryReader(filer.StreamGet(remotePathName, hostName, userId, password), System.Text.Encoding.ASCII);
      binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
     
      try
      {
        while(binaryReader.Read(c, 0, 80) > 0)
        {
          if (c[0].Equals('B') || c[0].Equals('L'))
          {
            collateralItem = new CollateralItem();

            collateralItem.ClientId = clientId;
            collateralItem.ContraClientId = new String(c, 2, 4);
            collateralItem.ContractType = new String(c, 0, 1);
            collateralItem.CollateralType = new String(c, 1, 1);
            collateralItem.SecId = new String(c, 10, 9);
            collateralItem.Quantity = long.Parse(new String(c, 19, 9));
            collateralItem.Amount = (double)(long.Parse(new String(c, 31, 14)) / 100D);
            
            if (c[75].Equals(' '))
            {
              collateralItem.CurrencyIso = "USD";
            }
            else
            {
              collateralItem.CurrencyIso = new String(c, 75, 3);
            }
            
            collateralItem.ContractId = new String(c, 41, 9);
            collateralItem.ExpiryDate = "20" + new String(c, 58, 2) + "-" + new String(c, 54, 2) + "-" + new String(c, 56, 2);

            collateralList.Add(collateralItem);
          }
          else if (c[0].Equals('*') && c[1].Equals('H'))
          {
            clientId = new String(c, 4, 4);
            bizDate = "20" + new String(c, 12, 2) + "-" + new String(c, 8, 2) + "-" + new String(c, 10, 2);
          }
          else if (c[0].Equals('*') && c[1].Equals('T'))
          {
            itemCount += int.Parse(new String(c, 8, 6));
          }
          else
          {
            throw (new Exception("Format error in data file. [Collateral.Load]"));
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

      if (!itemCount.Equals(collateralList.Count))
      {
        throw (new Exception("Parity error: Loaded " + collateralList.Count + " items while expecting "  + itemCount + ". [Collateral.Load]"));
      }
    }

    public CollateralItem CollateralItem(int index)
    {
      return (CollateralItem) collateralList[index];
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
        return collateralList.Count;
      }
    }
  }
}
