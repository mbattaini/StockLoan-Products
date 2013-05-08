using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using StockLoan.Common;

namespace StockLoan.Process
{
  public struct MarkItem
  {
    public string ClientId;
    public string ContractId;
    public string ContractType;
    public decimal Amount;
    public string Direction;
  }
  
  public class Marks
  {
    private Filer filer;

    private string bizDate = "0001-01-01";
    private string clientId = "0000";

    private ArrayList markList = null;

    public Marks()
    {
      filer = new Filer(ProcessMain.TempPath);
      markList = new ArrayList();
    }

    ~Marks()
    {
      filer.Dispose();
    }

    public void Load(string remotePathName, string hostName, string userId, string password)
    {
      MarkItem markItem;
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
            markItem = new MarkItem();

            markItem.ClientId = clientId;
            markItem.ContractId = new String(c, 1, 9);
            markItem.ContractType = new String(c, 0, 1);
            markItem.Amount = decimal.Parse(new String(c, 10, 12)) / 100;
            markItem.Direction = new String(c, 22, 1);
       
            markList.Add(markItem);
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
            throw (new Exception("Format error in data file. [Marks.Load]"));
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

      if (!itemCount.Equals(markList.Count))
      {
        throw (new Exception("Parity error: Loaded " + markList.Count + " items while expecting "  + itemCount + ". [Marks.Load]"));
      }
    }

    public MarkItem MarkItem(int index)
    {
      return (MarkItem) markList[index];
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
        return markList.Count;
      }
    }
  }
}
