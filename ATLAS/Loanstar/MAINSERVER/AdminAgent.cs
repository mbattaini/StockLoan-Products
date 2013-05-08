using System;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace StockLoan.Main
{
  public class AdminAgent : MarshalByRefObject, IAdmin
  {
    private string dbCnStr;

    public AdminAgent(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;
    }

	  public bool UserPasswordValidate(string userId, string password)
	  {
		  bool valid = false;

		  DataSet dsUsers = new DataSet();
		  SqlConnection dbCn = new SqlConnection(dbCnStr);

		  try
		  {
			  SqlCommand dbCmd = new SqlCommand("spUsersGet", dbCn);
			  dbCmd.CommandType = CommandType.StoredProcedure;

			  SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, (50));
			  paramUserId.Value = userId;

			  SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
			  dataAdapter.Fill(dsUsers, "Users");		  		  
		  }
		  catch (Exception error)
		  {
			  Log.Write(error.Message + " [AdminAgent.UserPasswordValidate]", Log.Error, 1);
			  throw;
		  }

		  if (dsUsers.Tables["Users"].Rows.Count > 0)
		  {
			  if (dsUsers.Tables["Users"].Rows[0]["UserId"].ToString().ToUpper().Equals(userId.ToUpper()))
			  {
				  if (dsUsers.Tables["Users"].Rows[0]["Password"].ToString().Equals(password))
				  {
					  valid = true;
				  }
			  }
		  }


		  return valid;
	  }
	  
	  
	public bool MayView(string userId, string functionPath)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserViewEdit", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, (50));
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, (50));
        paramFunctionPath.Value = functionPath;

        SqlParameter paramMayView = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
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

        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
        paramFunctionPath.Value = functionPath;

        SqlParameter paramMayEdit = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
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

    public bool MayViewBookGroup(string userId, string functionPath, string bookGroup)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserViewEdit", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, (50));
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, (50));
        paramFunctionPath.Value = functionPath;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, (10));
        paramBookGroup.Value = bookGroup;

        SqlParameter paramMayView = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
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

        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
        paramUserId.Value = userId;

        SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
        paramFunctionPath.Value = functionPath;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, (10));
        paramBookGroup.Value = bookGroup;

        SqlParameter paramMayEdit = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
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

    public void UserSet(string userId, string shortName, string password, string email, string group, string comment, string actUserId, bool isActive)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spUserSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
        paramUserId.Value = userId;

        SqlParameter paramShortName = dbCmd.Parameters.Add("@ShortName", SqlDbType.VarChar, 15);
        if (!shortName.Equals(""))
        {
            paramShortName.Value = shortName;
        }

        SqlParameter paramPassword = dbCmd.Parameters.Add("@Password", SqlDbType.VarChar, 50);
        paramPassword.Value = password;

        SqlParameter paramEmail = dbCmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
        if (!email.Equals(""))
        {
            paramEmail.Value = email;
        }

        SqlParameter paramGroup = dbCmd.Parameters.Add("@Group", SqlDbType.VarChar, 50);
        if (!group.Equals(""))
        {
            paramGroup.Value = group;
        }

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

    public void RoleSet(string roleCode, string role, string comment, string actUserId, bool delete)
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

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
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

    public void UserRoleSet(string userId, string roleCode, string comment, string actUserId, bool delete)
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

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
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

    public DataSet UserRolesGet(int utcOffset)
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

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.UserRolesGet]", Log.Error, 1);
      }

      return dataSet;
    }

    public DataSet UserRoleFunctionsGet(int utcOffset)
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

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.UserRoleFunctionsGet]", Log.Error, 1);
      }

      return dataSet;
    }

    public DataSet HolidaysGet(short utcOffSet)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spHolidayGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

		SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
		paramUtcOffset.Value = utcOffSet;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Holidays");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.HolidaysGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

	  public DataSet HolidaysGet(string bizDate, short utcOffSet)
	  {
		  SqlConnection dbCn = new SqlConnection(dbCnStr);
		  DataSet dataSet = new DataSet();

		  try
		  {
			  SqlCommand dbCmd = new SqlCommand("spHolidayGet", dbCn);
			  dbCmd.CommandType = CommandType.StoredProcedure;

			  SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
			  paramUtcOffset.Value = utcOffSet;

			  SqlParameter paramHolidayDate = dbCmd.Parameters.Add("@HolidayDate", SqlDbType.DateTime);
			  paramHolidayDate.Value = bizDate;

			  SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
			  dataAdapter.Fill(dataSet, "Holidays");

			  dataSet.RemotingFormat = SerializationFormat.Binary;
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
	  string description,
      bool isBankHoliday,
      bool isExchangeHoliday,
	  bool isBizDateHoliday,
	  string actUserId,
      bool isActive)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spHolidaySet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramDateTime = dbCmd.Parameters.Add("@HolidayDate", SqlDbType.DateTime);
        paramDateTime.Value = date;

        SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
        paramCountryCode.Value = countryCode;

		SqlParameter paramDescription = dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 30);
		paramDescription.Value = description;

        SqlParameter paramIsBankHoliday = dbCmd.Parameters.Add("@IsBankHoliday", SqlDbType.Bit);
        paramIsBankHoliday.Value = isBankHoliday;

        SqlParameter paramIsExhangeHoliday = dbCmd.Parameters.Add("@IsExchangeHoliday", SqlDbType.Bit);
        paramIsExhangeHoliday.Value = isExchangeHoliday;

		SqlParameter paramIsBizDateHoliday = dbCmd.Parameters.Add("@IsBizDateHoliday", SqlDbType.Bit);
		paramIsBizDateHoliday.Value = isBizDateHoliday;

		SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
		paramActUserId.Value = actUserId;

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

    public DataSet BookGet(
		string bookGroup,
		string book)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        if (!bookGroup.Equals(""))
        {
          paramBookGroup.Value = bookGroup;
        }

		SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
		if (!book.Equals(""))
		{
			paramBook.Value = book;
		}

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Books");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }


    public void BookSet(
        string bookGroup,
        string book,
        string bookParent,
        string bookName,
        string addressLine1,
        string addressLine2,
        string addressLine3,
        string phoneNumber,
        string faxNumber,
        string marginBorrow,
        string marginLoan,
        string markRoundHouse,
        string markRoundInstitution,
        string rateStockBorrow,
        string rateStockLoan, 
        string rateBondBorrow,
        string rateBondLoan,
        string countryCode,
		string fundDefault,		
        string actUserId,
        bool isActive)
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

        SqlParameter paramBookParent = dbCmd.Parameters.Add("@BookParent", SqlDbType.VarChar, 10);
        paramBookParent.Value = bookParent;


        SqlParameter paramBookName = dbCmd.Parameters.Add("@BookName", SqlDbType.VarChar, 50);
        if (!bookName.Equals(""))
        {
          paramBookName.Value = bookName;
        }

        SqlParameter paramAddressLine1 = dbCmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 50);
        if (!addressLine1.Equals(""))
        {
          paramAddressLine1.Value = addressLine1;
        }
        
        SqlParameter paramAddressLine2 = dbCmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 50);
        if (!addressLine2.Equals(""))
        {
          paramAddressLine2.Value = addressLine2;
        }
        
        SqlParameter paramAddressLine3 = dbCmd.Parameters.Add("@AddressLine3", SqlDbType.VarChar, 50);
        if (!addressLine3.Equals(""))
        {
          paramAddressLine3.Value = addressLine3;
        }

        SqlParameter paramPhoneNumber = dbCmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 25);
        if (!phoneNumber.Equals(""))
        {
          paramPhoneNumber.Value = phoneNumber;
        }
       
        SqlParameter paramFaxNumber = dbCmd.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 25);
        if (!faxNumber.Equals(""))
        {
          paramFaxNumber.Value = faxNumber;
        }

        SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
        if (!countryCode.Equals(""))
        {
          paramCountryCode.Value = countryCode;
        }

        SqlParameter paramMarginBorrow = dbCmd.Parameters.Add("@MarginBorrow", SqlDbType.Float);
        if (!marginBorrow.Equals(""))
        {
          paramMarginBorrow.Value = marginBorrow;
        }

        SqlParameter paramMarginLoan = dbCmd.Parameters.Add("@MarginLoan", SqlDbType.Float);
        if (!marginLoan.Equals(""))
        {
          paramMarginLoan.Value = marginLoan;
        }

        SqlParameter paramRateStockBorrow = dbCmd.Parameters.Add("@RateStockBorrow", SqlDbType.Decimal);
        if (!rateStockBorrow.Equals(""))
        {
          paramRateStockBorrow.Value = rateStockBorrow;
        }

        SqlParameter paramRateStockLoan = dbCmd.Parameters.Add("@RateStockLoan", SqlDbType.Decimal);
        if (!rateStockLoan.Equals(""))
        {
          paramRateStockLoan.Value = rateStockLoan;
        }


        SqlParameter paramRateBondBorrow = dbCmd.Parameters.Add("@RateBondBorrow", SqlDbType.Decimal);
        if (!rateBondBorrow.Equals(""))
        {
          paramRateBondBorrow.Value = rateBondBorrow;
        }

        SqlParameter paramRateBondLoan = dbCmd.Parameters.Add("@RateBondLoan", SqlDbType.Decimal);
        if (!rateBondLoan.Equals(""))
        {
          paramRateBondLoan.Value = rateBondLoan;
        }

		SqlParameter paramFundDefault = dbCmd.Parameters.Add("@FundDefault", SqlDbType.VarChar, 6);
		if (!fundDefault.Equals(""))
		{
			paramFundDefault.Value = fundDefault;
		}

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;

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

    public void BookCreditLimitsSet(  
      string bookGroup,
      string bookParent,
      string book,
      string borrowCreditLimit,
      string loanCreditLimit,
      string actUserId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookCreditLimitSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        paramBookGroup.Value = bookGroup;

        SqlParameter paramBookParent = dbCmd.Parameters.Add("@BookParent", SqlDbType.VarChar, 10);
        paramBookParent.Value = bookParent;

        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        paramBook.Value = book;

		if (!borrowCreditLimit.Equals(""))
		{
			SqlParameter paramBookAmountLimits = dbCmd.Parameters.Add("@BorrowLimitAmount", SqlDbType.Decimal);
			paramBookAmountLimits.Value = borrowCreditLimit;
		}

		if (!loanCreditLimit.Equals(""))
		{
            SqlParameter paramLoanAmountLimits = dbCmd.Parameters.Add("@LoanLimitAmount", SqlDbType.Decimal);
			paramLoanAmountLimits.Value = loanCreditLimit;
		}

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookCreditLimitsSet]", Log.Error, 1);
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

    public DataSet BookCreditLimitsGet(
      string bizdate,
      string bookGroup,
      string bookParent,
      string book,
      short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookCreditLimitsGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        
        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        if (!bizdate.Equals(""))
        {
          paramBizDate.Value = bizdate;
        }

          
        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        if (!bookGroup.Equals(""))
        {
          paramBookGroup.Value = bookGroup;
        }

          
        SqlParameter paramBookParent = dbCmd.Parameters.Add("@BookParent", SqlDbType.VarChar, 10);
        if (!bookParent.Equals(""))
        {
          paramBookParent.Value = bookParent;
        }

          
        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        if (!book.Equals(""))
        {
          paramBook.Value = book;
        }

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "CreditLimits");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookCreditLimitsGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }    

   
    public void BookDepoSet(
      string bookGroup,
      string book,
      string depo,
      string deliveryBook,
      string markBook,
      string actor)
    { }

    public DataSet BookContactGet(
    string bookGroup,
    string book)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookContactsGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        paramBookGroup.Value = bookGroup;

        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        paramBook.Value = book;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "BookContacts");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookContactGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    
    public void BookContactSet(
      string bookGroup,
      string book,
      string firstName,
      string lastName,
      string function,
      string phoneNumber,
      string faxNumber,
      string comment,
      string actor,
      bool isActive)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookContactSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        paramBookGroup.Value = bookGroup;

        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        paramBook.Value = book;

        SqlParameter paramFirstName = dbCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50);
        paramFirstName.Value = firstName;

        SqlParameter paramLastName = dbCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50);
        paramLastName.Value = lastName;

        SqlParameter paramFunction = dbCmd.Parameters.Add("@Function", SqlDbType.VarChar, 50);
        if (!function.Equals(""))
        {
          paramFunction.Value = function;
        }

        SqlParameter paramPhoneNumber = dbCmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50);
        if (!phoneNumber.Equals(""))
        {
          paramPhoneNumber.Value = phoneNumber;
        }

        SqlParameter paramFaxNumber = dbCmd.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 50);
        if (!faxNumber.Equals(""))
        {
          paramFaxNumber.Value = faxNumber;
        }

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
        if (!comment.Equals(""))
        {
          paramComment.Value = comment;
        }

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actor;

        SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookContactSet]", Log.Error, 1);
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


    public DataSet BookClearingInstructionGet(
      string bookGroup,
      string book,
      string system)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookClearingInstructionsGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        paramBookGroup.Value = bookGroup;

        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        paramBook.Value = book;

        if (!system.Equals(""))
        {
            SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 100);
            paramSystem.Value = system;
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "BookInstructions");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookClearingInstructionGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }


    public void BookClearingInstructionSet(
       string bookGroup,
       string bookGroupAlias,
       string book,
       string bookAlias,
       string system,
        string exchange,
       string settlement,       
       string routeName,
       string entity,
       bool doReturn,
       string routeReturn)
    { 
      SqlConnection dbCn = new SqlConnection(dbCnStr);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookClearingInstructionSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.CommandTimeout = 300;

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        paramBookGroup.Value = bookGroup;

        SqlParameter paramBookGroupAlias = dbCmd.Parameters.Add("@BookGroupAlias", SqlDbType.VarChar, 10);
        paramBookGroupAlias.Value = bookGroupAlias;

        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        paramBook.Value = book;

        SqlParameter paramBookAlias = dbCmd.Parameters.Add("@BookAlias", SqlDbType.VarChar, 10);
        paramBookAlias.Value = bookAlias;

        SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 10);
        paramSystem.Value = system;

        SqlParameter paramExchange = dbCmd.Parameters.Add("@Exchange", SqlDbType.VarChar, 15);
        paramExchange.Value = exchange;

        SqlParameter paramRouteName = dbCmd.Parameters.Add("@RouteName", SqlDbType.VarChar, 100);
        paramRouteName.Value = routeName;

        SqlParameter paramEntity = dbCmd.Parameters.Add("@Entity", SqlDbType.VarChar, 10);
        paramEntity.Value = entity;

        SqlParameter paramSettlement = dbCmd.Parameters.Add("@Settlement", SqlDbType.VarChar, 10);
        paramSettlement.Value = settlement;

          SqlParameter paramDoReturn = dbCmd.Parameters.Add("@DoReturn", SqlDbType.Bit);
        paramDoReturn.Value = doReturn;

        SqlParameter paramRouteReturn = dbCmd.Parameters.Add("@RouteReturn", SqlDbType.VarChar, 100);
        paramRouteReturn.Value = routeReturn;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [AdminAgent.BookClearingInstructionSet]", Log.Error, 1);
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


    public void BookOtherSet(
      string bookGroup,
      string book,
      string other,
      string comment,
      string actor)
    { }


	  public DataSet BookFundInstructionGet(
		string bookGroup,
		string book,
		string currencyIso,
		short utcOffset)
	  {
		  SqlConnection dbCn = new SqlConnection(dbCnStr);
		  DataSet dataSet = new DataSet();

		  try
		  {
			  SqlCommand dbCmd = new SqlCommand("spBookFundGet", dbCn);
			  dbCmd.CommandType = CommandType.StoredProcedure;

			  SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
			  paramBookGroup.Value = bookGroup;

			  if (!book.Equals(""))
			  {
				  SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				  paramBook.Value = book;
			  }

			  if (!currencyIso.Equals(""))
			  {
				  SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
				  paramCurrencyIso.Value = currencyIso;
			  }

			  SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
			  dataAdapter.Fill(dataSet, "BookFunds");

              dataSet.RemotingFormat = SerializationFormat.Binary;
		  }
		  catch (Exception e)
		  {
			  Log.Write(e.Message + " [AdminAgent.BookFundInstructionGet]", Log.Error, 1);
			  throw;
		  }

		  return dataSet;
	  }


	  public void BookFundInstructionSet(
		  string bookGroup,
		  string book,
		  string currencyIso,
		  string fund,
		  string actor,
		  bool isActive)
	  {
		  SqlConnection dbCn = new SqlConnection(dbCnStr);

		  try
		  {
			  SqlCommand dbCmd = new SqlCommand("spBookFundSet", dbCn);
			  dbCmd.CommandType = CommandType.StoredProcedure;
			  dbCmd.CommandTimeout = 300;

			  SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
			  paramBookGroup.Value = bookGroup;

			  SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
			  paramBook.Value = book;
		
			  SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
			  paramCurrencyCode.Value = currencyIso;
			  
			  SqlParameter paramFund = dbCmd.Parameters.Add("@Fund", SqlDbType.VarChar, 6);
			  paramFund.Value = fund;

			  SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
			  paramActUserId.Value = actor;

			  SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
			  paramIsActive.Value = isActive;

			  dbCn.Open();
			  dbCmd.ExecuteNonQuery();
		  }
		  catch (Exception e)
		  {
			  Log.Write(e.Message + " [AdminAgent.BookFundInstructionSet]", Log.Error, 1);
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
