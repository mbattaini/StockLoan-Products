using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Courier
{
  public class Threshold
  {
    private SqlConnection dbCn;

    private int itemCount = 0;
    private string fileTimeStamp = "";

    public Threshold(string dbCnStr) : this(new SqlConnection(dbCnStr)) {}
    public Threshold(SqlConnection dbCn)
    {
      this.dbCn = dbCn;
    }

    public void ListGet(string bizDate, string exchange, string fileHost, string filePathName)
    {
      string line;
      string [] fields;
      bool atLastRow = false;

      Filer filer = new Filer(CourierMain.TempPath + "threshold" + exchange + ".txt");
      filer.Timeout = 90000;

      char [] delimiter = new char[1];
      delimiter[0] = '|';

      SqlCommand dbCmd;       
      StreamReader streamReader = null;

      Log.Write("Opening file " + filePathName + " on host " + fileHost + ". [Threshold.ListGet]", 2);

      try
      {
        streamReader = new StreamReader(
          filer.StreamGet(
          filePathName,
          fileHost,
          "anonymous",
          "support@anetics.com"),
          System.Text.Encoding.ASCII);     
        streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

        dbCmd = new SqlCommand("spThresholdPurge", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = bizDate;

        SqlParameter paramExchange = dbCmd.Parameters.Add("@Exchange", SqlDbType.Char, 4);
        paramExchange.Value = exchange;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        dbCmd.CommandText = "spThresholdItemSet";

        SqlParameter paramSymbol = dbCmd.Parameters.Add("@Symbol", SqlDbType.VarChar, 12);
        SqlParameter paramDescription = dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
				SqlParameter paramRule = dbCmd.Parameters.Add("@Rule", SqlDbType.VarChar, 1);

        itemCount = 0;

        while ((line = streamReader.ReadLine()) != null)
        {
          fields = line.Split(delimiter, 6);

          if (fields.Length > 5)
          {
						if (fields[3].Equals("Y") || fields[4].Equals("Y")) // This list item is a threshold security. 
						{
							paramSymbol.Value = fields[0].Trim().ToUpper();
							paramDescription.Value = fields[1].Trim().ToUpper();
							paramRule.Value = fields[4].Trim().ToUpper();

							dbCmd.ExecuteNonQuery();
							itemCount += 1;
						}
          }
          else if ((fields.Length == 1) && (fields[0].Trim().Length == 14)) // We're at the time stamp.
          {
            atLastRow = true;
            TimeStampSet(fields[0].Trim());
            break;
          }
        }
        
        if (!atLastRow)
        {
          Log.Write("There is risk that we may not have read all rows in data file. [Threshold.ListGet]", Log.Error, 1);
        }
        else
        {
          dbCmd.CommandText = "spThresholdControlSet";
        
          dbCmd.Parameters.Remove(paramSymbol);
          dbCmd.Parameters.Remove(paramDescription);
          dbCmd.Parameters.Remove(paramRule);

          SqlParameter paramItemCount = dbCmd.Parameters.Add("@ItemCount", SqlDbType.Int);
          paramItemCount.Value = itemCount;

          SqlParameter paramFileTimeStamp = dbCmd.Parameters.Add("@FileTimeStamp", SqlDbType.DateTime);
          paramFileTimeStamp.Value = fileTimeStamp;

          dbCmd.ExecuteNonQuery();
        }
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
          
        if (streamReader != null)
        {
          streamReader.Close(); 
        }
      }
    }

    public string FileTimeStamp
    {
      get
      {
        return fileTimeStamp;
      }
    }
     
    public int ItemCount
    {
      get
      {
        return itemCount;
      }
    }
     
    private void TimeStampSet(string yyyyMMddHHnnss)
    {
      DateTime dateTime;

      try
      {
        dateTime = new DateTime(
          int.Parse(yyyyMMddHHnnss.Substring(0, 4)),
          int.Parse(yyyyMMddHHnnss.Substring(4, 2)),
          int.Parse(yyyyMMddHHnnss.Substring(6, 2)),
          int.Parse(yyyyMMddHHnnss.Substring(8, 2)),
          int.Parse(yyyyMMddHHnnss.Substring(10, 2)),
          int.Parse(yyyyMMddHHnnss.Substring(12, 2)));

        fileTimeStamp = dateTime.ToString(Standard.DateTimeFileFormat);
      }
      catch
      {
        Log.Write("Unable to parse " + yyyyMMddHHnnss + " into a valid date/time value. [Threshold.TimeStampSet]", Log.Error, 1);
        fileTimeStamp = "";
      }
    }
  }
}
