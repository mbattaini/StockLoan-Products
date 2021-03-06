using System;
using System.Data;
using System.Data.SqlClient;

namespace StockLoan.Common
{
  /// <summary>
  /// Sets and gets key values by section from tbKeyValues in the application database.
  /// </summary>
  public class KeyValue
  {
    /// <summary>
    /// Sets a key value in tbKeyValues in the application database using a connection string.
    /// </summary>
    public static void Set(string keyId, string keyValue, string sqlCnStr)
    {
      SqlConnection sqlCn = new SqlConnection(sqlCnStr);
      Set(keyId, keyValue, sqlCn);
    }

    /// <summary>
    /// Sets a key value in tbKeyValues in the application database using a connection.
    /// </summary>
    public static void Set(string keyId,  string keyValue, SqlConnection sqlCn)
    {  
      SqlCommand sqlDbCmd = new SqlCommand("spKeyValueSet", sqlCn);
      sqlDbCmd.CommandType = CommandType.StoredProcedure;

      SqlParameter paramKeyId = sqlDbCmd.Parameters.Add("@KeyId", SqlDbType.VarChar, 50);			
      paramKeyId.Value = keyId;

      SqlParameter paramKeyValue = sqlDbCmd.Parameters.Add("@KeyValue", SqlDbType.VarChar, 250);			
      if (keyValue.Length > 0)
      {
        paramKeyValue.Value = keyValue;
      }
      else  
      {
        paramKeyValue.Value = DBNull.Value;
      }

      try 
      {
        sqlCn.Open();
        sqlDbCmd.ExecuteNonQuery();
      }
      catch(Exception e) 
      {
        Log.Write(e.Message + " [KeyValue.Set]", Log.Error, 1);
      }
      finally
      {
        if (sqlCn.State != ConnectionState.Closed)
        {
          sqlCn.Close();
        }
      }
    }

    /// <summary>
    /// Gets a key value from tbKeyValues in the application database using a connection string.
    /// </summary>
    public static string Get(string keyId, string keyValueDefault, string sqlCnStr)
    {
      SqlConnection sqlCn = new SqlConnection(sqlCnStr);
      return Get(keyId, keyValueDefault, sqlCn);
    }
        
    public static string Get(string keyId, string keyValueDefault, SqlConnection sqlCn)
    {
      SqlCommand sqlDbCmd = new SqlCommand("spKeyValueGet", sqlCn);
      sqlDbCmd.CommandType = CommandType.StoredProcedure;

      SqlParameter paramKeyId = sqlDbCmd.Parameters.Add("@KeyId", SqlDbType.VarChar, 50);			
      paramKeyId.Value = keyId;

      SqlParameter paramKeyValue = sqlDbCmd.Parameters.Add("@KeyValue", SqlDbType.VarChar, 250);			
      paramKeyValue.Direction = ParameterDirection.InputOutput;
      if (keyValueDefault.Length > 0)
      {
        paramKeyValue.Value = keyValueDefault;
      }
      else  
      {
        paramKeyValue.Value = DBNull.Value;
      }

      try 
      {
        sqlCn.Open();
        sqlDbCmd.ExecuteNonQuery();
      }
      catch(Exception e) 
      {
        Log.Write(e.Message + " [KeyValue.Get]", Log.Error, 1);
      }
      finally
      {
        if (sqlCn.State != ConnectionState.Closed)
        {
          sqlCn.Close();
        }
      }

      return paramKeyValue.Value.ToString();
    }
  }
}