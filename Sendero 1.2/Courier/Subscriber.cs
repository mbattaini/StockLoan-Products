using System;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Courier
{
  public class Subscriber
  {
    private Filer filer;
    private Email email;

    private string dbCnStr;
    private SqlConnection dbCn;

    public Subscriber(string dbCnStr, SqlConnection dbCn)
    {
      this.dbCnStr = dbCnStr;
      this.dbCn = dbCn;

      filer = new Filer(CourierMain.TempPath);
    }
 
    public void SubscribeInventory()
    {
      string fileStatus;
      string fileTime;

      DataTable subscriberList = SubscriberList();

      if (subscriberList == null) // Likely no database connection.
      {
        Log.Write("Unable to get subscriber list from database. [Subscriber.SubscribeInventory]", Log.Error, 1);
        return;
      }

      try 
      {
      /*  email = new Email(
          Standard.ConfigValue("EmailHost"),
          Standard.ConfigValue("EmailUserId"),
          Standard.ConfigValue("EmailPassword"));*/
      }
      catch (Exception e) 
      {
        Log.Write("Email: " + e.Message + " [Subscriber.SubscribeInventory]", Log.Error, 1);
        email = null;
      }

      foreach (DataRow row in subscriberList.Rows)
      {
        if (CourierMain.IsStopped) // Bail if service has been stopped.
        {
          Log.Write("The service was stopped before completion of this cycle. [Subscriber.SubscribeInventory]", 2);
          return;
        }

        try 
        {
          fileStatus = "";
          fileTime = "";

          if (!row["FilePathName"].ToString().Equals("") && !row["FileHost"].ToString().Equals("")) // We have a file to monitor.
          {
            Log.Write("Subscriber will check data for " + row["Desk"].ToString() + ". [Subscriber.SubscribeInventory]", 2);
            
						/*if (!row["MailAddress"].ToString().Equals("")) // We have e-mail to look for.
						{
							if (email != null && email.ExtractData( row["MailAddress"].ToString(),
								row["MailSubject"].ToString(),
								row["FilePathName"].ToString(),
								row["FileHost"].ToString(),
								row["FileUserName"].ToString(),
								row["FilePassword"].ToString()))
							{
								Log.Write("Subscriber processed e-mail for " + row["Desk"].ToString() + ". [Subscriber.SubscribeInventory]", 2);
							}              
							else
							{
								Log.Write("Subscriber did not process e-mail for " + row["Desk"].ToString() + ". [Subscriber.SubscribeInventory]", 2);
							}
						}*/

            try
            {
              fileTime = filer.FileTime(
                CourierMain.DatePartSet(row["FilePathName"].ToString(), Master.BizDate),
                row["FileHost"].ToString(),
                row["FileUserName"].ToString(),
                row["FilePassword"].ToString());
              
              if (fileTime.Equals(""))
              {
                fileStatus = "File does not exist.";                
              }
              else
              {
                fileStatus = "OK";  
              }
            }
            catch (Exception e)
            {
              fileStatus = e.Message;
            }

            if (fileStatus.Equals("OK") // File exists.
              &&(!Tools.FormatDate(row["FileTime"].ToString(), Standard.DateTimeFileFormat).Equals(fileTime) // File is new.
              ||(!row["FileStatus"].ToString().Equals("OK")) // Or last transfer attempt failed.
              ||(!row["LoadStatus"].ToString().Equals("OK")))) // Or last load attempt failed.
            {
              Log.Write("Will load inventory for " + row["Desk"].ToString() + ", PGP " + row["UsePgp"].ToString() + ". [Subscriber.SubscribeInventory]", 2);
              Load(row, fileTime);
            }
            else if (!fileStatus.Equals("OK"))
            {
              Log.Write("File status for " + row["Desk"].ToString() + ": " + fileStatus + " [Subscriber.SubscribeInventory]", 2);

              SqlCommand dbCmd = new SqlCommand("spInventorySubscriberSet", dbCn);
              dbCmd.CommandType = CommandType.StoredProcedure;

              SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);			
              paramDesk.Value = row["Desk"].ToString();

              SqlParameter paramFileCheckTime = dbCmd.Parameters.Add("@FileCheckTime", SqlDbType.DateTime);			
              paramFileCheckTime.Value = DateTime.UtcNow;

              SqlParameter paramFileStatus = dbCmd.Parameters.Add("@FileStatus", SqlDbType.VarChar, 100);			
              paramFileStatus.Value = fileStatus;

              try
              {        
                dbCn.Open();
                dbCmd.ExecuteNonQuery();
              }
              catch (Exception e)
              {
                Log.Write(e.Message + " [Subscriber.SubscribeInventory]", Log.Error, 1);
              }
              finally
              {
                if (dbCn.State != ConnectionState.Closed)
                {
                  dbCn.Close();
                }
              }
            }
            else
            {
              Log.Write("Inventory is current for " + row["Desk"].ToString() + ". [Subscriber.SubscribeInventory]", 2);
            }
          }
          else
          {
            Log.Write("A FilePathName or FileHost has not been provided for Desk " + row["Desk"].ToString() + ". [Subscriber.SubscribeInventory]", 2);
          }
        }
        catch (Exception e)
        {
          Log.Write(e.Message + " [Subscriber.SubscribeInventory]", Log.Error, 1);
        }
      }

      /*try 
      {
        if (email != null && bool.Parse(KeyValue.Get("EmailPurge", "False", dbCn))) 
        {
          email.Purge();
        }
      }
      catch(Exception e)
      {
        Log.Write("Email purge: " + e.Message + " [Subscriber.SubscribeInventory]", Log.Error, 1);
      }*/
    }
    
    private void Load(DataRow row, string fileTime)
    {
      SqlCommand dbCmd;

      string fileStatus = "";
      string loadStatus = "";

      FileReader fileReader = new FileReader(dbCn);
      
      try
      {
        fileReader.Load(
                  row["Desk"].ToString(),
                  CourierMain.DatePartSet(row["FilePathName"].ToString(), Master.BizDate),
                  row["FileHost"].ToString(),
                  row["FileUserName"].ToString(),
                  row["FilePassword"].ToString(),
                  (bool)row["UsePgp"],
                  row["LoadExtensionPgp"].ToString()); // Load inventory data into FileReader object.
        fileStatus = "OK";
      }
      catch(Exception e)
      {
        fileStatus = e.Message;
      }

      if (fileStatus.Equals("OK") && (fileReader.ItemCount > 0)) // Inventory load was successful.
      {
        dbCmd = new SqlCommand("spInventoryItemSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        try
        {
          SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);			
          paramBizDate.Value = fileReader.BizDate;

          SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);			
          paramDesk.Value = row["Desk"];

          SqlParameter paramAccount = dbCmd.Parameters.Add("@Account", SqlDbType.VarChar, 15);	
          SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);			
          SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);			
          
          SqlParameter paramIncrementCurrentQuantity = dbCmd.Parameters.Add("@IncrementCurrentQuantity", SqlDbType.Bit);			
          paramIncrementCurrentQuantity.Value = 1;

          dbCn.Open();

          for (int i = 0; i < fileReader.ItemCount; i++) // Add each new record.
          {
            paramAccount.Value = fileReader.InventoryItem(i).Account;
            paramSecId.Value = fileReader.InventoryItem(i).SecId;
            paramQuantity.Value = fileReader.InventoryItem(i).Quantity;

            dbCmd.ExecuteNonQuery();
          }

          dbCn.Close();

          dbCmd.Parameters.Remove(paramAccount);
          dbCmd.Parameters.Remove(paramSecId);
          dbCmd.Parameters.Remove(paramQuantity);
          dbCmd.Parameters.Remove(paramIncrementCurrentQuantity);
          
          loadStatus = "OK";

          KeyValue.Set("InventoryIsNew", "True", dbCn); // Set True to trigger a new inventory output file.
        }
        catch (Exception e)
        {
          Log.Write(e.Message + " [Subscriber.Load]", Log.Error, 1);          
          loadStatus = e.Message;
        }
        finally
        {
          if (dbCn.State != ConnectionState.Closed)
          {
            dbCn.Close();
          }
        }

        try
        {
          dbCmd.CommandText = "spInventorySubscriberSet";

          SqlParameter paramFileCheckTime = dbCmd.Parameters.Add("@FileCheckTime", SqlDbType.DateTime);			
          paramFileCheckTime.Value = DateTime.UtcNow;

          SqlParameter paramFileTime = dbCmd.Parameters.Add("@FileTime", SqlDbType.DateTime);			
          paramFileTime.Value = fileTime;

          SqlParameter paramFileStatus = dbCmd.Parameters.Add("@FileStatus", SqlDbType.VarChar, 100);			
          paramFileStatus.Value = fileStatus;

          SqlParameter paramLoadTime = dbCmd.Parameters.Add("@LoadTime", SqlDbType.DateTime);			
          paramLoadTime.Value = DateTime.UtcNow;

          SqlParameter paramLoadCount = dbCmd.Parameters.Add("@LoadCount", SqlDbType.Int);			
          paramLoadCount.Value = fileReader.ItemCount;

          SqlParameter paramLoadStatus = dbCmd.Parameters.Add("@LoadStatus", SqlDbType.VarChar, 100);			
          paramLoadStatus.Value = loadStatus;

          dbCn.Open();
          dbCmd.ExecuteNonQuery(); // Set SubscriberList field values.
        
          Log.Write("Loaded " + fileReader.ItemCount + " items for " + row["Desk"].ToString() + ". [Subscriber.Load]", 2);
        }
        catch (Exception e)
        {
          Log.Write(e.Message + " [Subscriber.Load]", Log.Error, 1);
        }
        finally
        {
          if (dbCn.State != ConnectionState.Closed)
          {
            dbCn.Close();
          }
        }
      }      
      else // We had an error getting file time or file parse did not generate any items to load.
      {
        if (fileStatus.Equals("OK") && (fileReader.ItemCount.Equals(0)))
        {
          fileStatus = "FileReader did not load any inventory items.";
        }

        dbCmd = new SqlCommand("spInventorySubscriberSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);			
        paramDesk.Value = row["Desk"];

        SqlParameter paramFileStatus = dbCmd.Parameters.Add("@FileStatus", SqlDbType.VarChar, 100);			
        paramFileStatus.Value = fileStatus;

        try
        {
          dbCn.Open();
          dbCmd.ExecuteNonQuery();

          Log.Write("File status for " + row["Desk"].ToString() + ": " + fileStatus + " [Subscriber.Load]", 2);
        }
        catch (Exception e)
        {
          Log.Write(e.Message + " [Subscriber.Load]", Log.Error, 1);
        }
        finally
        {
          dbCn.Close();
        }
      }
    }
    
    private DataTable SubscriberList()
    {
      SqlCommand sqlCmd = new SqlCommand("spInventorySubscriberGet", dbCn);
      sqlCmd.CommandType = CommandType.StoredProcedure;

      DataSet dataSet = new DataSet();
      
      try
      {
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        dataAdapter.Fill(dataSet, "SubscriberList");
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Subscriber.SubscriberList]", Log.Error, 1);
        return null;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed)
        {
          dbCn.Close();
        }
      }

      Log.Write("Returned table with " + dataSet.Tables["SubscriberList"].Rows.Count.ToString() + " rows. [Subscriber.SubscriberList]", 2);
      return dataSet.Tables["SubscriberList"];
    }
  }
}
