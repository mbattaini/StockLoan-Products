using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.S3
{
  public struct InventoryItem
  {
    public string Account;
    public string SecId;
    public long Quantity;
  }
    
	public class FileReader
	{
    private Filer filer;
    private SqlConnection dbCn;
    
    private string account = "";
    private string bizDate = "";
    private string streamPathName;

    private InventoryItem inventoryItem;
    private ArrayList inventoryList;

    public FileReader(string dbCnStr) : this(new SqlConnection(dbCnStr)) {}
    public FileReader(SqlConnection dbCn)
    {
      this.dbCn = dbCn;

      streamPathName = Standard.ConfigValue("TempPath", @"C:\") + "fileReader";

      inventoryList = new ArrayList();
      filer = new Filer(Standard.ConfigValue("TempPath", @"C:\"));      
    }
    
    public void Load(string desk, string remotePath, string hostName, string userName, string password, bool usePgp, string loadExtensionPgp)
    {
      int n;
      short recordLength;
      char headerFlag;
      char dataFlag;
      char trailerFlag;
      short accountLocale;
      char delimiter;
      short accountOrdinal;
      short secIdOrdinal;
      short quantityOrdinal;
      short recordCountOrdinal;
      short accountPosition;
      short accountLength;
      short bizDateDD;
      short bizDateMM;
      short bizDateYY;
      short secIdPosition;
      short secIdLength;
      short quantityPosition;
      short quantityLength;
      short recordCountPosition;
      short recordCountLength;

      SqlCommand dbCmd = new SqlCommand("spInventoryFileDataMaskGet", dbCn);
      dbCmd.CommandType = CommandType.StoredProcedure;

      SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);			
      paramDesk.Value = desk;

      SqlParameter paramRecordLength = dbCmd.Parameters.Add("@RecordLength", SqlDbType.SmallInt);			
      paramRecordLength.Direction = ParameterDirection.Output;
      
      SqlParameter paramHeaderFlag = dbCmd.Parameters.Add("@HeaderFlag", SqlDbType.Char, 1);			
      paramHeaderFlag.Direction = ParameterDirection.Output;
      
      SqlParameter paramDataFlag = dbCmd.Parameters.Add("@DataFlag", SqlDbType.Char, 1);			
      paramDataFlag.Direction = ParameterDirection.Output;
      
      SqlParameter paramTrailerFlag = dbCmd.Parameters.Add("@TrailerFlag", SqlDbType.Char, 1);			
      paramTrailerFlag.Direction = ParameterDirection.Output;
      
      SqlParameter paramAccountLocale = dbCmd.Parameters.Add("@AccountLocale", SqlDbType.SmallInt);			
      paramAccountLocale.Direction = ParameterDirection.Output;

      SqlParameter paramDelimiter = dbCmd.Parameters.Add("@Delimiter", SqlDbType.Char, 1);			
      paramDelimiter.Direction = ParameterDirection.Output;
      
      SqlParameter paramAccountOrdinal = dbCmd.Parameters.Add("@AccountOrdinal", SqlDbType.SmallInt);			
      paramAccountOrdinal.Direction = ParameterDirection.Output;

      SqlParameter paramSecIdOrdinal = dbCmd.Parameters.Add("@SecIdOrdinal", SqlDbType.SmallInt);			
      paramSecIdOrdinal.Direction = ParameterDirection.Output;
      
      SqlParameter paramQuantityOrdinal = dbCmd.Parameters.Add("@QuantityOrdinal", SqlDbType.SmallInt);			
      paramQuantityOrdinal.Direction = ParameterDirection.Output;
      
      SqlParameter paramRecordCountOrdinal = dbCmd.Parameters.Add("@RecordCountOrdinal", SqlDbType.SmallInt);			
      paramRecordCountOrdinal.Direction = ParameterDirection.Output;
      
      SqlParameter paramAccountPosition = dbCmd.Parameters.Add("@AccountPosition", SqlDbType.SmallInt);			
      paramAccountPosition.Direction = ParameterDirection.Output;
      
      SqlParameter paramAccountLength = dbCmd.Parameters.Add("@AccountLength", SqlDbType.SmallInt);			
      paramAccountLength.Direction = ParameterDirection.Output;
      
      SqlParameter paramBizDateDD = dbCmd.Parameters.Add("@BizDateDD", SqlDbType.SmallInt);			
      paramBizDateDD.Direction = ParameterDirection.Output;
      
      SqlParameter paramBizDateMM = dbCmd.Parameters.Add("@BizDateMM", SqlDbType.SmallInt);			
      paramBizDateMM.Direction = ParameterDirection.Output;
      
      SqlParameter paramBizDateYY = dbCmd.Parameters.Add("@BizDateYY", SqlDbType.SmallInt);			
      paramBizDateYY.Direction = ParameterDirection.Output;
      
      SqlParameter paramSecIdPosition = dbCmd.Parameters.Add("@SecIdPosition", SqlDbType.SmallInt);			
      paramSecIdPosition.Direction = ParameterDirection.Output;
      
      SqlParameter paramSecIdLength = dbCmd.Parameters.Add("@SecIdLength", SqlDbType.SmallInt);			
      paramSecIdLength.Direction = ParameterDirection.Output;
      
      SqlParameter paramQuantityPosition = dbCmd.Parameters.Add("@QuantityPosition", SqlDbType.SmallInt);			
      paramQuantityPosition.Direction = ParameterDirection.Output;
      
      SqlParameter paramQuantityLength = dbCmd.Parameters.Add("@QuantityLength", SqlDbType.SmallInt);			
      paramQuantityLength.Direction = ParameterDirection.Output;
      
      SqlParameter paramRecordCountPosition= dbCmd.Parameters.Add("@RecordCountPosition", SqlDbType.SmallInt);			
      paramRecordCountPosition.Direction = ParameterDirection.Output;
      
      SqlParameter paramRecordCountLength = dbCmd.Parameters.Add("@RecordCountLength", SqlDbType.SmallInt);			
      paramRecordCountLength.Direction = ParameterDirection.Output;
      
      try 
      {
        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        if (paramRecordLength.Value.Equals(DBNull.Value)) // No record.
        {
          throw (new Exception("The inventory file data mask for Desk " + desk + " is missing."));
        }

        recordLength = (short)paramRecordLength.Value;

        headerFlag = paramHeaderFlag.Value.ToString().ToCharArray()[0];
        dataFlag = paramDataFlag.Value.ToString().ToCharArray()[0];
        trailerFlag = paramTrailerFlag.Value.ToString().ToCharArray()[0];

        accountLocale = (short)paramAccountLocale.Value;

        delimiter = paramDelimiter.Value.ToString().ToCharArray()[0];

        accountOrdinal = (short)paramAccountOrdinal.Value;
        secIdOrdinal = (short)paramSecIdOrdinal.Value;
        quantityOrdinal = (short)paramQuantityOrdinal.Value;
        recordCountOrdinal = (short)paramRecordCountOrdinal.Value;

        accountPosition = (short)paramAccountPosition.Value;
        accountLength = (short)paramAccountLength.Value;
        bizDateDD = (short)paramBizDateDD.Value;
        bizDateMM = (short)paramBizDateMM.Value;
        bizDateYY = (short)paramBizDateYY.Value;
        secIdPosition = (short)paramSecIdPosition.Value;
        secIdLength = (short)paramSecIdLength.Value;
        quantityPosition = (short)paramQuantityPosition.Value;
        quantityLength = (short)paramQuantityLength.Value;
        recordCountPosition = (short)paramRecordCountPosition.Value;
        recordCountLength = (short)paramRecordCountLength.Value;
      }
      catch
      {
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed)
        {
          dbCn.Close();
        }
      }

      StreamReader streamReader = null;

      try
      {
        if (usePgp)
        {
          filer.FileGet(remotePath, hostName, userName, password, streamPathName + ".gpg");

          GnuPG gnuPG = new GnuPG(KeyValue.Get("GnuPGHomePath", @"C:\Anetics\GnuPG\", dbCn), loadExtensionPgp);
          gnuPG.Originator = KeyValue.Get("GnuPGOriginator", "medalist@anetics.com", dbCn);
          gnuPG.PassPhrase = KeyValue.Get("GnuPGPassword(PassPhrase)", "medalist 2004", dbCn);
          gnuPG.Verbosity = (VerbosityLevels)int.Parse(KeyValue.Get("GnuPGVerbosity", "0", dbCn));
          
          gnuPG.Command = Commands.Decrypt;          
          gnuPG.DoCommand(streamPathName + ".gpg");
       
          if (!gnuPG.Verbosity.Equals(VerbosityLevels.None))
          {
            Log.Write("GnuPG " + gnuPG.ProcessInfo + " [FileReader.Load]", Log.Information, 1);     
          }

          streamReader = new StreamReader(streamPathName);      
        }
        else
        {
          streamReader = new StreamReader(filer.StreamGet(remotePath, hostName, userName, password));
          streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        inventoryItem = new InventoryItem();
        inventoryItem.SecId = "";

        if (recordLength == -1) // Each record is terminated with control character[s].
        {
          if (delimiter == '0') // Each field is defined by position and length.
          {
            char [] c;
            while (streamReader.Peek() > -1) // There is a line to read.
            {
              c = streamReader.ReadLine().Trim().ToCharArray();

              try
              {
                if(c[0].Equals(headerFlag))
                {
                  if ((accountPosition > -1) && (accountLocale.Equals((short)0)))
                  {
                    account = new String(c, accountPosition, accountLength);                
                  }

                  if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                  {
                    bizDate = MakeDate(new String(c, bizDateYY, 2), new String(c, bizDateMM, 2), new String(c, bizDateDD, 2));
                  }
                }
                else if (c[0].Equals(dataFlag) || (dataFlag.Equals('=')))
                {
                  if ((accountPosition > -1) && (accountLocale.Equals((short)1)))
                  {
                    account = new String(c, accountPosition, accountLength).Trim();                
                  }

                  inventoryItem = new InventoryItem();

                  inventoryItem.Account = account;
                  inventoryItem.SecId = new String(c, secIdPosition, secIdLength).Trim().ToUpper();
                  inventoryItem.Quantity = Tools.ParseLong(new String(c, quantityPosition, quantityLength).Trim());

                  inventoryList.Add(inventoryItem);
                }
                else if(c[0].Equals(trailerFlag))
                {
                  if (recordCountPosition > -1)
                  {
                    n = int.Parse(new String(c, recordCountPosition, recordCountLength));
                  
                    if (!inventoryList.Count.Equals(n))
                    {
                      Log.Write("Record count parity fails with " + inventoryList.Count + " items loaded while trailer record anticipates " + n + ". [FileReader.Load]", 2);     
                    }
                  }
                }
              }
              catch {}
            }
          }
          else // Each field is delimited by the delimiter.
          {
            string line;
            string [] fields;

            while ((line = streamReader.ReadLine()) != null)
            {
              fields = line.Trim().Split(delimiter);

              if (fields.Length > 1)
              {
                try
                {
                  if(fields[0].Equals(headerFlag.ToString()))
                  {
                    if ((accountOrdinal > -1) && (accountLocale.Equals((short)0)))
                    {
                      account = fields[accountOrdinal].Replace("\"","").Trim();
                    }

                    if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                    {
                      bizDate = MakeDate(line.Substring(bizDateYY, 2), line.Substring(bizDateMM, 2), line.Substring(bizDateDD, 2));
                    }
                  }
                  else if(fields[0].Equals(dataFlag.ToString()) || dataFlag.Equals('='))
                  {
                    if ((accountOrdinal > -1) && (accountLocale.Equals((short)1)))
                    {
                      account = fields[accountOrdinal].Replace("\"","").Trim();
                    }

                    inventoryItem = new InventoryItem();
                  
                    inventoryItem.Account = account;
                    inventoryItem.SecId = fields[secIdOrdinal].Replace("\"","").ToUpper().Trim();

                    string quantity = fields[quantityOrdinal].Replace("\"","").ToUpper().Trim();
                    inventoryItem.Quantity = Tools.ParseLong(quantity.Replace("K", "000").Replace("MM", "000000").Replace("M", "000000"));
                  
                    inventoryList.Add(inventoryItem);
                  }
                  else if(fields[0].Equals(trailerFlag.ToString()))
                  {
                    if (recordCountOrdinal > -1)
                    {
                      n = int.Parse(fields[recordCountOrdinal].Trim());
                      if (!inventoryList.Count.Equals(n))
                      {
                        Log.Write("Record count parity fails with " + inventoryList.Count + " items loaded while trailer record anticipates " + n + ". [FileReader.Load]", 2);     
                      }
                    }
                  }
                }
                catch {}
              }
            }          
          }
        }
        else
        {
          Log.Write("FileReader does not handle fixed row length. [FileReader.Load]", Log.Error, 1);     
        }
      }
      catch
      {
        throw;
      }
      finally 
      {
        if (streamReader != null)
        {
          streamReader.Close(); 
        }
      }
    }

    public InventoryItem InventoryItem(int index)
    {
      return (InventoryItem)inventoryList[index];
    }

    public string BizDate
    {
      get 
      {
        if (bizDate.Equals(""))
        {
          return Master.BizDate;
        }
        else
        {
          return bizDate;
        }
      }
    }

    public int ItemCount
    {
      get 
      {
        return inventoryList.Count;
      }
    }

    private string MakeDate(string yy, string mm, string dd)
    {
      DateTime dt;

      try
      {
        dt = new DateTime(
          int.Parse(yy.Replace(" ", "0")) + 2000,
          int.Parse(mm.Replace(" ", "0")),
          int.Parse(dd.Replace(" ", "0"))
          );
      }
      catch
      {
        Log.Write("Unable to create a valid date from " + yy + "-" + mm + "-" + dd + ". [FileReader.MakeDate]", Log.Error, 1);
        return "";
      }

      return dt.ToString(Standard.DateFormat);
    }
  }
}
