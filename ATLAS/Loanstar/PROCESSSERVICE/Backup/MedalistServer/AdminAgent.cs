// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace StockLoan.Medalist
{
  public class AdminAgent : MarshalByRefObject, IAdmin
  {
    private string dbCnStr;    

    public AdminAgent(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;
    }

    public bool MayView(string userId, string functionPath)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserViewEdit", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar,(50));
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar,(50));
        paramFunctionPath.Value = functionPath;

        SqlParameter paramMayView  = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
        paramMayView.Direction = ParameterDirection.Output;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        return (bool)paramMayView.Value;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.MayView]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }
    
    public bool MayEdit(string userId, string functionPath)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserViewEdit", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar,50);
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar,50);
        paramFunctionPath.Value = functionPath;
        
        SqlParameter paramMayEdit  = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
        paramMayEdit.Direction = ParameterDirection.Output;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        return (bool)paramMayEdit.Value;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.MayEdit]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }

    public string UserEmailGet(string userId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      SqlDataReader dataReader = null;

      string userEmail = "";

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUsersGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar,(50));
        paramUserId.Value = userId;

        dbCn.Open();
        dataReader = dbCmd.ExecuteReader();

        while (dataReader.Read()) // Expect one row.
        {
          userEmail = dataReader.GetValue(2).ToString().Trim();
        }
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.UserEmailGet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if ((dataReader != null)) 
        {
          dataReader.Close();
        }

        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
      
      return userEmail;
    }
    
    public bool MayViewBookGroup(string userId, string functionPath, string bookGroup)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserViewEdit", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar,(50));
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar,(50));
        paramFunctionPath.Value = functionPath;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar,(10));
        paramBookGroup.Value = bookGroup;

        SqlParameter paramMayView  = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
        paramMayView.Direction = ParameterDirection.Output;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      
        return (bool)paramMayView.Value;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.MayView]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }
    
    public bool MayEditBookGroup(string userId, string functionPath, string bookGroup)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserViewEditBookGroup", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar,50);
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar,50);
        paramFunctionPath.Value = functionPath;
        
        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar,(10));
        paramBookGroup.Value = bookGroup;

        SqlParameter paramMayEdit  = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
        paramMayEdit.Direction = ParameterDirection.Output;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        return (bool)paramMayEdit.Value;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.MayEdit]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }

    public void UserSet(string userId, string shortName, string email, string comment, string actUserId, bool isActive)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      
      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar,50);
        paramUserId.Value = userId;

        SqlParameter paramShortName = dbCmd.Parameters.Add("@ShortName", SqlDbType.VarChar, 15);
        paramShortName.Value = shortName;

        SqlParameter paramEmail = dbCmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
        paramEmail.Value = email;

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
        paramComment.Value = comment;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;                 
        
        SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;     
        
        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.UserSet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }
    
    public void RoleSet(string roleCode, string role, string comment, string actUserId, bool   delete)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      
      try
      {
        SqlCommand dbCmd = new SqlCommand("spRoleSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramRoleCode = dbCmd.Parameters.Add("@RoleCode", SqlDbType.VarChar, 5);
        paramRoleCode.Value = roleCode;
        
        SqlParameter paramRole = dbCmd.Parameters.Add("@Role", SqlDbType.VarChar, 50);
        paramRole.Value = role;

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar,50);
        paramComment.Value = comment;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;         
        
        SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
        paramDelete.Value = delete;         

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.RoleSet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }
  
    public void UserRoleSet(string userId, string roleCode, string comment, string actUserId,  bool delete)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      
      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserRoleSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
        paramUserId.Value = userId;
        
        SqlParameter paramRoleCode = dbCmd.Parameters.Add("@RoleCode", SqlDbType.VarChar, 5);
        paramRoleCode.Value = roleCode;

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar,50);
        paramComment.Value = comment;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;                 

        SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
        paramDelete.Value = delete;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.UserRoleSet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }

    public void RoleFunctionSet(string roleCode, string functionPath, bool mayView, bool mayEdit, string bookGroupList, string comment, string actUserId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
 
      try
      {
        SqlCommand dbCmd = new SqlCommand("spRoleFunctionSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramRoleCode = dbCmd.Parameters.Add("@RoleCode", SqlDbType.VarChar, 5);
        paramRoleCode.Value = roleCode;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
        paramFunctionPath.Value = functionPath;

        SqlParameter paramMayView = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
        paramMayView.Value = mayView;

        SqlParameter paramMayEdit = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
        paramMayEdit.Value = mayEdit;

        SqlParameter paramBookGroupList = dbCmd.Parameters.Add("@BookGroupList", SqlDbType.VarChar, 100);
        paramBookGroupList.Value = bookGroupList;

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
        paramComment.Value = comment;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;                 

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.RoleFunctionSet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }

    public DataSet UserRolesGet(short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUsersGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
        paramUtcOffset.Value = utcOffset;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "Users");

        dbCmd.CommandText = "spUserRolesGet";
        dataAdapter.Fill(dataSet, "UserRoles");

        dataSet.Relations.Add("UsersUserRoles",
          dataSet.Tables["Users"].Columns["UserId"],
          dataSet.Tables["UserRoles"].Columns["UserId"],
					false);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.UserRolesGet]", Log.Error, 1);
      }

      return dataSet;
    }

    public DataSet UserRoleFunctionsGet(short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spRolesGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
        paramUtcOffset.Value = utcOffset;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Roles");
                                                    
        dbCmd.CommandText = "spRoleFunctionsGet";        
        dataAdapter.Fill(dataSet, "RoleFunctions");
        
        dataSet.Relations.Add("RolesRoleFunctions",
          dataSet.Tables["Roles"].Columns["RoleCode"],
          dataSet.Tables["RoleFunctions"].Columns["RoleCode"]);
                                                  
        dbCmd.CommandText = "spUserRolesGet";
        dataAdapter.Fill(dataSet, "UserRoles");

        dataSet.Relations.Add("RolesUserRoles",
          dataSet.Tables["Roles"].Columns["RoleCode"],
          dataSet.Tables["UserRoles"].Columns["RoleCode"]);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.UserRoleFunctionsGet]", Log.Error, 1);
      }

      return dataSet;
    }

    public DataSet HolidaysGet()
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spHolidayGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "Holidays");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.HolidaysGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public void HolidaysSet(
      string date,
      string countryCode,
      bool   isBankHoliday,
      bool   isExchangeHoliday,
      bool   isActive)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      
      try
      {
        SqlCommand dbCmd = new SqlCommand("spHolidaySet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramDateTime = dbCmd.Parameters.Add("@Date", SqlDbType.DateTime);
        paramDateTime.Value = date;

        SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
        paramCountryCode.Value = countryCode;
        
        SqlParameter paramIsBankHoliday = dbCmd.Parameters.Add("@IsBankHoliday", SqlDbType.Bit);
        paramIsBankHoliday.Value = isBankHoliday;     

        SqlParameter paramIsExhangeHoliday = dbCmd.Parameters.Add("@IsExchangeHoliday", SqlDbType.Bit);
        paramIsExhangeHoliday.Value = isExchangeHoliday;     

        SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;
        
        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.HolidaysSet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }
    
    public DataSet BookDataGet(short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookParentGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        
        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "BookParents");

        dbCmd.CommandText = "spBookGet";
        dataAdapter.Fill(dataSet, "Books");

        DataColumn[] parentColumns= new DataColumn[2];
        DataColumn[] childColumns= new DataColumn[2];

        parentColumns[0] = dataSet.Tables["BookParents"].Columns["BookGroup"];
        parentColumns[1] = dataSet.Tables["BookParents"].Columns["BookParent"];
        
        childColumns[0] = dataSet.Tables["Books"].Columns["BookGroup"];
        childColumns[1] = dataSet.Tables["Books"].Columns["BookParent"];
        
        dataSet.Relations.Add("BookParentsBooks", parentColumns, childColumns, false);

        dbCmd.CommandText = "spFirmGet";
        dataAdapter.Fill(dataSet, "Firms");

        dbCmd.CommandText = "spCountryGet";
        dataAdapter.Fill(dataSet, "Countries");

        dbCmd.CommandText = "spDeskTypeGet";
        dataAdapter.Fill(dataSet, "DeskTypes");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookDataGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public void BookDataSet(string bookGroup, string book, long amountLimitBorrow, long amountLimitLoan, string faxNumber, string firm, string country, string deskType, string actUserId, string comment)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
  
        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        paramBookGroup.Value = bookGroup;

        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        paramBook.Value = book;
        
        SqlParameter paramAmountLimitBorrow = dbCmd.Parameters.Add("@AmountLimitBorrow", SqlDbType.BigInt);
        paramAmountLimitBorrow.Value = amountLimitBorrow;     
        
        SqlParameter paramAmountLimitLoan = dbCmd.Parameters.Add("@AmountLimitLoan", SqlDbType.BigInt);
        paramAmountLimitLoan.Value = amountLimitLoan;

        SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.VarChar, 5);
        paramFirm.Value = firm;

        SqlParameter paramCountry = dbCmd.Parameters.Add("@Country", SqlDbType.VarChar, 2);
        paramCountry.Value = country;

        SqlParameter paramDeskType = dbCmd.Parameters.Add("@DeskType", SqlDbType.VarChar, 3);
        paramDeskType.Value = deskType;

        SqlParameter paramFaxNumber = dbCmd.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 25);
        paramFaxNumber.Value = faxNumber;
    
        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        SqlParameter paramComments = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
        paramComments.Value = comment;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BooksSet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed) 
        {
          dbCn.Close();
        }
      }
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
