// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using Anetics.Common;

namespace Anetics.Loanet
{
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
  
  public class Recalls
  {
    private Filer filer;

    private string bizDate = "0001-01-01";
    private string clientId = "0000";

    private ArrayList recallList = null;

    public Recalls()
    {
      filer = new Filer(LoanetMain.TempPath);
      recallList = new ArrayList();
    }

    ~Recalls()
    {
      filer.Dispose();
    }

    public void Load(string remotePathName, string hostName, string userId, string password)
    {
      RecallItem recallItem;
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
            recallItem = new RecallItem();

            recallItem.ClientId = clientId;
            recallItem.ContractId = new String(c, 20, 9);
            recallItem.ContractType = new String(c, 0, 1);
            recallItem.ContraClientId = new String(c, 1, 4);
            recallItem.SecId = new String(c, 5, 9);
            recallItem.Quantity = long.Parse(new String(c, 29, 9));
            recallItem.RecallDate = "20" + new String(c, 18, 2) + "-" + new String(c, 14, 2) + "-" + new String(c, 16, 2);
            recallItem.BuyInDate = "20" + new String(c, 42, 2) + "-" + new String(c, 38, 2) + "-" + new String(c, 40, 2);
            recallItem.Status = new String(c, 44, 1);
            recallItem.ReasonCode = new String(c, 45, 2);
            recallItem.RecallId = new String(c, 47, 16);
            recallItem.SequenceNumber = short.Parse(new String(c, 63, 6));
            recallItem.Comment = new String(c, 69, 11).Trim();
       
            recallList.Add(recallItem);
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
            throw (new Exception("Format error in data file. [Recalls.Load]"));
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

      if (!itemCount.Equals(recallList.Count))
      {
        throw (new Exception("Parity error: Loaded " + recallList.Count + " items while expecting "  + itemCount + ". [Recalls.Load]"));
      }
    }

    public RecallItem RecallItem(int index)
    {
      return (RecallItem)recallList[index];
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
        return recallList.Count;
      }
    }
  }
}
