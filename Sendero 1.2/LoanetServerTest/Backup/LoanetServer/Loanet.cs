// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Anetics.Common;

namespace Anetics.Loanet
{
  public class Loanet
  {
    private SqlConnection dbCn;
    private Filer filer = null;

    public Loanet(string dbCnStr) : this(new SqlConnection(dbCnStr)) {}
    public Loanet(SqlConnection dbCn)
    {
      this.dbCn = dbCn;
      filer = new Filer(LoanetMain.TempPath);
    }

    ~Loanet()
    {
      filer.Dispose();
    }

    public void MarksDo(string clientId)
    {
      string bizDate;
      string fileStatus = "";
      string fileFileTime = "";

      if (KeyValue.Get("LoanetMarksBizDate" + clientId, "0001-01-01", dbCn).Equals(Master.BizDate)) // Done for today.
      {
        Log.Write("Mark data is current for " + clientId + " for " + Master.BizDate + ". [Loanet.MarksDo]", 2);
        return;
      }

      if (!Master.ContractsBizDate.Equals(Master.BizDate)) // Contracts not ready.
      {
        Log.Write("Waiting for contracts to role to " + Master.BizDate + ". [Loanet.MarksDo]", 2);
        return;
      }

      try
      {
        fileFileTime = filer.FileTime(
          Standard.ConfigValue("MarksRemotePath") + "dlo" + clientId,
          Standard.ConfigValue("FileHost"),
          Standard.ConfigValue("FileUserId"),
          Standard.ConfigValue("FilePassword"));
        
        if (fileFileTime.Length.Equals(0))
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

      if (fileStatus.Equals("OK") // Good host connection and file exists.
        &&(!fileFileTime.Equals(KeyValue.Get("LoanetMarksFileTime" + clientId, "", dbCn)))) // File is new.
      {
        Log.Write("Loading mark data for " + clientId + " for " + Master.BizDate + ". [Loanet.MarksDo]", 2);

        try
        {
          bizDate = MarksLoad(
            Standard.ConfigValue("MarksRemotePath") + "dlo" + clientId,
            Standard.ConfigValue("FileHost"),
            Standard.ConfigValue("FileUserId"),
            Standard.ConfigValue("FilePassword"));

          if (!bizDate.Equals(Master.BizDate))
          {
            Log.Write("Mark data for " + clientId + " is dated " + bizDate + ". [Loanet.MarksDo]", Log.Error, 1);
            return;
          }

          fileStatus = "OK";

          int updateCount = MarkContractUpdate(bizDate, clientId);
          
          KeyValue.Set("LoanetMarksBizDate" + clientId, bizDate, dbCn);
          KeyValue.Set("LoanetMarksFileTime" + clientId, fileFileTime, dbCn);

          Log.Write("Done processing mark data for " + clientId
            + " (updated " + updateCount.ToString("#,##0") + " contracts). [Loanet.MarksDo]", 2);
        }
        catch (Exception e)
        {
          fileStatus = e.Message;
          Log.Write(e.Message + " [Loanet.MarksDo]", 2);
        }
      }
      else
      {
        if (fileStatus.Equals("OK"))
        {
          Log.Write("Mark data for " + clientId + " awaiting new data file for " + Master.BizDate + ". [Loanet.MarksDo]", 2);
        }
        else
        {
          Log.Write("Mark data for " + clientId + " file status: " + fileStatus + " [Loanet.MarksDo]", 2);
        }
      }
    }

    public string MarksLoad(string remotePathName, string hostName, string userId, string password)
    {
      decimal direction = 0.0M;
      int n = 0;

      MarkItem markItem;
      Marks marks;
      
      SqlCommand sqlCommand;
              
      try
      {
        marks = new Marks();

        Log.Write("Loading data from filestream //" + hostName + remotePathName + ". [Loanet.MarksLoad]", 2);        
        marks.Load(remotePathName, hostName, userId, password);

        sqlCommand = new SqlCommand("spLoanetMarkPurge", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = marks.BizDate;

        Log.Write("Purging existing mark records for " + marks.BizDate + " from database. [Loanet.MarksLoad]", 2);
        dbCn.Open();
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "spLoanetMarkInsert";
       
        SqlParameter paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.Char, 4);
        SqlParameter paramContractId = sqlCommand.Parameters.Add("@ContractId", SqlDbType.Char, 9);
        SqlParameter paramContractType = sqlCommand.Parameters.Add("@ContractType", SqlDbType.Char, 1);
        SqlParameter paramAmount = sqlCommand.Parameters.Add("@Amount", SqlDbType.Money);

        Log.Write("Inserting " + marks.Count.ToString("#,##0") + " markItem[s] into database. [Loanet.MarksLoad]", 2);        
        for (n = 0; n < marks.Count; n++)
        {
          markItem = marks.MarkItem(n);

          if ((markItem.ContractType.Equals("B") && markItem.Direction.Equals("D"))
            || (markItem.ContractType.Equals("L") && markItem.Direction.Equals("C")))
          {
            direction = 1.0M;
          }
          
          if ((markItem.ContractType.Equals("B") && markItem.Direction.Equals("C"))
            || (markItem.ContractType.Equals("L") && markItem.Direction.Equals("D")))
          {
            direction = -1.0M;
          }
          
          paramClientId.Value = markItem.ClientId;
          paramContractId.Value = markItem.ContractId;
          paramContractType.Value = markItem.ContractType;
          paramAmount.Value = (markItem.Amount * direction);
    
          sqlCommand.ExecuteNonQuery();
        }

        if (n.Equals(marks.Count))
        {
          Log.Write("Inserted " + n.ToString("#,##0") + " markItem[s]. [Loanet.MarksLoad]", 2);
        }
        else
        {
          throw (new Exception("Inserted " + n.ToString("#,##0") + " markItem[s] out of an expected "
            + marks.Count.ToString("#,##0") + " for " + marks.BizDate + ". [Loanet.MarksLoad]"));
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
      }

      return marks.BizDate;
    }

    private int MarkContractUpdate(string bizDate, string clientId)
    {
      SqlCommand sqlCommand;
      
      try
      {
        sqlCommand = new SqlCommand("spLoanetMarkUpdate", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.CommandTimeout = 300;

        SqlParameter  paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = bizDate;

        SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
        paramClientId.Value = clientId;
          
        SqlParameter paramCount = sqlCommand.Parameters.Add("@Count", SqlDbType.Int);
        paramCount.Direction = ParameterDirection.ReturnValue;
        
        Log.Write("Updating main contract records with LAMS marks for " + clientId + " for " + bizDate + ". [Loanet.MarkContractUpdate]", 2);
          
        dbCn.Open();
        sqlCommand.ExecuteNonQuery();

        return (int)paramCount.Value;
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
    }
    
    public void PositionDo(string clientId, string fileType)
    {
      string bizDate;
      string fileStatus = "";
      string fileFileTime = "";

      if (KeyValue.Get("LoanetPositionBizDate" + clientId, "0001-01-01", dbCn).Equals(Master.BizDatePrior)) // Done for today.
      {
        Log.Write("Position data is current for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.PositionDo]", 2);
        return;
      }

      try
      {
        fileFileTime = filer.FileTime(
          Standard.ConfigValue("PositionRemotePath") + fileType + clientId,
          Standard.ConfigValue("FileHost"),
          Standard.ConfigValue("FileUserId"),
          Standard.ConfigValue("FilePassword"));
        
        if (fileFileTime.Length.Equals(0))
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

      if (fileStatus.Equals("OK") // Good host connection and file exists.
        &&(!fileFileTime.Equals(KeyValue.Get("LoanetPositionFileTime" + clientId, "", dbCn)))) // File is new.
      {
        Log.Write("Loading position data for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.PositionDo]", 2);

        try
        {
          bizDate = PositionLoad(
            Standard.ConfigValue("PositionRemotePath") + fileType + clientId,
            Standard.ConfigValue("FileHost"),
            Standard.ConfigValue("FileUserId"),
            Standard.ConfigValue("FilePassword"));

          if (!bizDate.Equals(Master.BizDatePrior))
          {
            Log.Write("Position data for " + clientId + " is dated " + bizDate + ". [Loanet.PositionDo]", Log.Error, 1);
            return;
          }

          fileStatus = "OK";

          PositionContractUpdate(bizDate, clientId);
          
          KeyValue.Set("LoanetPositionBizDate" + clientId, bizDate, dbCn);
          KeyValue.Set("LoanetPositionFileTime" + clientId, fileFileTime, dbCn);

          Log.Write("Done processing position data for " + clientId + ". [Loanet.PositionDo]", 2);
        }
        catch (Exception e)
        {
          fileStatus = e.Message;
          Log.Write(e.Message + " [Loanet.PositionDo]", 2);
        }
      }
      else
      {
        if (fileStatus.Equals("OK"))
        {
          Log.Write("Position data for " + clientId + " awaiting new data file for " + Master.BizDatePrior + ". [Loanet.PositionDo]", 2);
        }
        else
        {
          Log.Write("Position data for " + clientId + " file status: " + fileStatus + " [Loanet.PositionDo]", 2);
        }
      }
    }

    public string PositionLoad(string remotePathName, string hostName, string userId, string password)
    {
      int n = 0;

      Contract contract;
      Contracts contracts;
      
      SqlCommand sqlCommand;
              
      try
      {
        contracts = new Contracts();

        Log.Write("Loading data from filestream //" + hostName + remotePathName + ". [Loanet.PositionLoad]", 2);        
        contracts.Load(remotePathName, hostName, userId, password);

        sqlCommand = new SqlCommand("spLoanetContractPurge", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = contracts.BizDate;

        SqlParameter paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.Char, 4);
        paramClientId.Value = contracts.ClientId;

        Log.Write("Purging existing contract records for " + contracts.ClientId + " for " + contracts.BizDate + " from database. [Loanet.PositionLoad]", 2);
        dbCn.Open();
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "spLoanetContractInsert";
       
        SqlParameter paramContractId = sqlCommand.Parameters.Add("@ContractId", SqlDbType.Char, 9);
        SqlParameter paramContractType = sqlCommand.Parameters.Add("@ContractType", SqlDbType.Char, 1);
        SqlParameter paramContraClientId = sqlCommand.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
        SqlParameter paramSecId = sqlCommand.Parameters.Add("@SecId", SqlDbType.VarChar, 9);
        SqlParameter paramQuantity = sqlCommand.Parameters.Add("@Quantity", SqlDbType.BigInt);
        SqlParameter paramAmount = sqlCommand.Parameters.Add("@Amount", SqlDbType.Money);
        SqlParameter paramCollateralCode = sqlCommand.Parameters.Add("@CollateralCode", SqlDbType.VarChar, 1);
        SqlParameter paramValueDate = sqlCommand.Parameters.Add("@ValueDate", SqlDbType.DateTime);
        SqlParameter paramSettleDate = sqlCommand.Parameters.Add("@SettleDate", SqlDbType.DateTime);
        SqlParameter paramTermDate = sqlCommand.Parameters.Add("@TermDate", SqlDbType.DateTime);
        SqlParameter paramRate = sqlCommand.Parameters.Add("@Rate", SqlDbType.Decimal);
        SqlParameter paramRateCode = sqlCommand.Parameters.Add("@RateCode", SqlDbType.VarChar, 1);
        SqlParameter paramStatusFlag = sqlCommand.Parameters.Add("@StatusFlag", SqlDbType.Char, 1);
        SqlParameter paramPoolCode = sqlCommand.Parameters.Add("@PoolCode", SqlDbType.VarChar, 1);
        SqlParameter paramDivRate = sqlCommand.Parameters.Add("@DivRate", SqlDbType.Decimal);
        SqlParameter paramDivCallable = sqlCommand.Parameters.Add("@DivCallable", SqlDbType.Bit);
        SqlParameter paramIncomeTracked = sqlCommand.Parameters.Add("@IncomeTracked", SqlDbType.Bit);
        SqlParameter paramMarginCode = sqlCommand.Parameters.Add("@MarginCode", SqlDbType.VarChar, 1);
        SqlParameter paramMargin = sqlCommand.Parameters.Add("@Margin", SqlDbType.Decimal);
        SqlParameter paramCurrencyIso = sqlCommand.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);
        SqlParameter paramSecurityDepot = sqlCommand.Parameters.Add("@SecurityDepot", SqlDbType.VarChar, 2);
        SqlParameter paramCashDepot = sqlCommand.Parameters.Add("@CashDepot", SqlDbType.VarChar, 2);
        SqlParameter paramOtherClientId = sqlCommand.Parameters.Add("@OtherClientId", SqlDbType.Char, 4);
        SqlParameter paramComment = sqlCommand.Parameters.Add("@Comment", SqlDbType.VarChar, 20);

        Log.Write("Inserting " + contracts.Count.ToString("#,##0") + " contract[s] for " + contracts.ClientId + " into database. [Loanet.PositionLoad]", 2);        
        for (n = 0; n < contracts.Count; n++)
        {
          contract = contracts.Contract(n);

          paramContractId.Value = contract.ContractId;
          paramContractType.Value = contract.ContractType;
          paramContraClientId.Value = contract.ContraClientId;
          paramSecId.Value = contract.SecId;
          paramQuantity.Value = contract.Quantity;
          paramAmount.Value = contract.Amount;
          paramCollateralCode.Value = contract.CollateralCode;
 
          if (contract.ValueDate.Equals("0000-00-00")||contract.ValueDate.Equals("    -  -  "))
          {
            paramValueDate.Value = DBNull.Value;
          }
          else
          {
            paramValueDate.Value = contract.ValueDate;
          }
          
          if (contract.SettleDate.Equals("0000-00-00")||contract.SettleDate.Equals("    -  -  "))
          {
            paramSettleDate.Value = DBNull.Value;
          }
          else
          {
            paramSettleDate.Value = contract.SettleDate;
          }
          
          if (contract.TermDate.Equals("0000-00-00")||contract.TermDate.Equals("    -  -  "))
          {
            paramTermDate.Value = DBNull.Value;
          }
          else
          {
            paramTermDate.Value = contract.TermDate;
          }
          
          paramRate.Value = contract.Rate;
          paramRateCode.Value = contract.RateCode;
          paramStatusFlag.Value = contract.StatusFlag;
          paramPoolCode.Value = contract.PoolCode;
          paramDivRate.Value = contract.DivRate;
          paramDivCallable.Value = contract.DivCallable;
          paramIncomeTracked.Value = contract.IncomeTracked;
          paramMarginCode.Value = contract.MarginCode;
          paramMargin.Value = contract.Margin;
          paramCurrencyIso.Value = contract.CurrencyIso;
          paramSecurityDepot.Value = contract.SecurityDepot;
          paramCashDepot.Value = contract.CashDepot;
          paramOtherClientId.Value = contract.OtherClientId;
          paramComment.Value = contract.Comment;
        
          sqlCommand.ExecuteNonQuery();
        }

        if (n.Equals(contracts.Count))
        {
          Log.Write("Inserted " + n.ToString("#,##0") + " contract[s]. [Loanet.PositionLoad]", 2);

          sqlCommand.Parameters.Remove(paramContractId);
          sqlCommand.Parameters.Remove(paramContractType);
          sqlCommand.Parameters.Remove(paramContraClientId);
          sqlCommand.Parameters.Remove(paramSecId);
          sqlCommand.Parameters.Remove(paramQuantity);
          sqlCommand.Parameters.Remove(paramAmount);
          sqlCommand.Parameters.Remove(paramCollateralCode);
          sqlCommand.Parameters.Remove(paramValueDate);
          sqlCommand.Parameters.Remove(paramSettleDate);
          sqlCommand.Parameters.Remove(paramTermDate);
          sqlCommand.Parameters.Remove(paramRate);
          sqlCommand.Parameters.Remove(paramRateCode);
          sqlCommand.Parameters.Remove(paramStatusFlag);
          sqlCommand.Parameters.Remove(paramPoolCode);
          sqlCommand.Parameters.Remove(paramDivRate);
          sqlCommand.Parameters.Remove(paramDivCallable);
          sqlCommand.Parameters.Remove(paramIncomeTracked);
          sqlCommand.Parameters.Remove(paramMarginCode);
          sqlCommand.Parameters.Remove(paramMargin);
          sqlCommand.Parameters.Remove(paramCurrencyIso);
          sqlCommand.Parameters.Remove(paramSecurityDepot);
          sqlCommand.Parameters.Remove(paramCashDepot);
          sqlCommand.Parameters.Remove(paramOtherClientId);
          sqlCommand.Parameters.Remove(paramComment);

          sqlCommand.CommandText = "spLoanetContractControlSet";
       
          SqlParameter paramRecordCount = sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int);
          paramRecordCount.Value = contracts.Count;

          SqlParameter paramFundingRate = sqlCommand.Parameters.Add("@FundingRate", SqlDbType.Decimal);
          paramFundingRate.Value = contracts.FundingRate;

          sqlCommand.ExecuteNonQuery();
          dbCn.Close();
        }
        else
        {
          throw (new Exception("Inserted " + n.ToString("#,##0") + " contract[s] out of an expected "
            + contracts.Count.ToString("#,##0") + " for " + contracts.ClientId + " for " + contracts.BizDate + ". [Loanet.PositionLoad]"));
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
      }

      return contracts.BizDate;
    }

    private void PositionContractUpdate(string bizDate, string clientId)
    {
      SqlCommand sqlCommand;
      
      try
      {
        sqlCommand = new SqlCommand("spLoanetContractUpdate", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.CommandTimeout = 300;

        SqlParameter  paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = bizDate;

        SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
        paramClientId.Value = clientId;
          
        Log.Write("Updating main contract records for " + clientId + " for " + bizDate + ". [Loanet.PositionContractUpdate]", 2);
          
        dbCn.Open();
        sqlCommand.ExecuteNonQuery();
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
    }
    
    public void CollateralDo(string clientId)
    {
      string bizDate;
      string fileStatus = "";
      string fileFileTime = "";

      if (KeyValue.Get("LoanetCollateralBizDate" + clientId, "0001-01-01", dbCn).Equals(Master.BizDatePrior)) // Done for today.
      {
        Log.Write("Collateral data is current for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.CollateralDo]", 2);
        return;
      }

      try
      {
        fileFileTime = filer.FileTime(
          Standard.ConfigValue("CollateralRemotePath") + "pco" + clientId,
          Standard.ConfigValue("FileHost"),
          Standard.ConfigValue("FileUserId"),
          Standard.ConfigValue("FilePassword"));
        
        if (fileFileTime.Length.Equals(0))
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

      if (fileStatus.Equals("OK") // Good host connection and file exists.
        &&(!fileFileTime.Equals(KeyValue.Get("LoanetCollateralFileTime" + clientId, "", dbCn)))) // File is new.
      {
        Log.Write("Loading collateral data for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.CollateralDo]", 2);

        try
        {
          bizDate = CollateralLoad(
            Standard.ConfigValue("CollateralRemotePath") + "pco" + clientId,
            Standard.ConfigValue("FileHost"),
            Standard.ConfigValue("FileUserId"),
            Standard.ConfigValue("FilePassword"));
         
          if (!bizDate.Equals(Master.BizDatePrior))
          {
            Log.Write("Collateral data for " + clientId + " is dated " + bizDate + ". [Loanet.CollateralDo]", Log.Error, 1);
            return;
          }

          fileStatus = "OK";
          
          KeyValue.Set("LoanetCollateralBizDate" + clientId, bizDate, dbCn);
          KeyValue.Set("LoanetCollateralFileTime" + clientId, fileFileTime, dbCn);

          Log.Write("Done processing collateral data for " + clientId + ". [Loanet.CollateralDo]", 2);
        }
        catch (Exception e)
        {
          fileStatus = e.Message;
          Log.Write(e.Message + " [Loanet.CollateralDo]", 2);
        }
      }
      else
      {
        if (fileStatus.Equals("OK"))
        {
          Log.Write("Collateral data for " + clientId + " awaiting new data file for " + Master.BizDatePrior + ". [Loanet.CollateralDo]", 2);
        }
        else
        {
          Log.Write("Collateral data for " + clientId + " file status: " + fileStatus + " [Loanet.CollateralDo]", 2);
        }
      }
    }

    public string CollateralLoad(string remotePathName, string hostName, string userId, string password)
    {
      int n = 0;
      string clientId = "";

      CollateralItem collateralItem;
      Collateral collateral;

      SqlCommand sqlCommandPurge;
      SqlCommand sqlCommandLoad;
              
      try
      {
        collateral = new Collateral();
 
        Log.Write("Loading data from filestream //" + hostName + remotePathName + ". [Loanet.CollateralLoad]", 2);        
        collateral.Load(remotePathName, hostName, userId, password);

        sqlCommandPurge = new SqlCommand("spLoanetCollateralPurge", dbCn);
        sqlCommandPurge.CommandType = CommandType.StoredProcedure;

        SqlParameter paramPurgeBizDate = sqlCommandPurge.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramPurgeBizDate.Value = collateral.BizDate;
        
        SqlParameter paramPurgeClientId = sqlCommandPurge.Parameters.Add("@ClientId", SqlDbType.Char, 4);
        
        sqlCommandLoad = new SqlCommand("spLoanetCollateralInsert", dbCn);
        sqlCommandLoad.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = sqlCommandLoad.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = collateral.BizDate;
        
        SqlParameter paramClientId = sqlCommandLoad.Parameters.Add("@ClientId", SqlDbType.Char, 4);
        SqlParameter paramContraClientId = sqlCommandLoad.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
        SqlParameter paramContractType = sqlCommandLoad.Parameters.Add("@ContractType", SqlDbType.Char, 1);
        SqlParameter paramCollateralType = sqlCommandLoad.Parameters.Add("@CollateralType", SqlDbType.Char, 1);
        SqlParameter paramSecId = sqlCommandLoad.Parameters.Add("@SecId", SqlDbType.VarChar, 9);
        SqlParameter paramQuantity = sqlCommandLoad.Parameters.Add("@Quantity", SqlDbType.BigInt);
        SqlParameter paramAmount = sqlCommandLoad.Parameters.Add("@Amount", SqlDbType.Money);
        SqlParameter paramCurrencyIso = sqlCommandLoad.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);
        SqlParameter paramContractId = sqlCommandLoad.Parameters.Add("@ContractId", SqlDbType.Char, 9);
        SqlParameter paramExpiryDate = sqlCommandLoad.Parameters.Add("@ExpiryDate", SqlDbType.DateTime);

        dbCn.Open();

        Log.Write("Inserting " + collateral.Count.ToString("#,##0") + " collateral item[s] for " + collateral.BizDate + " into database. [Loanet.CollateralLoad]", 2);        
        for (n = 0; n < collateral.Count; n++)
        {
          collateralItem = collateral.CollateralItem(n);

          if (!clientId.Equals(collateralItem.ClientId)) // New ClientId.
          {
            clientId = collateralItem.ClientId;
            Log.Write("Purging existing collateral records for " + clientId + " for " + collateral.BizDate + " from database. [Loanet.CollateralLoad]", 2);
            
            paramPurgeClientId.Value = clientId;
            sqlCommandPurge.ExecuteNonQuery();
            
            paramClientId.Value = clientId;
          }
            
          paramContraClientId.Value = collateralItem.ContraClientId;
          paramContractType.Value = collateralItem.ContractType;
          paramCollateralType.Value = collateralItem.CollateralType;
          paramSecId.Value = collateralItem.SecId;
          paramQuantity.Value = collateralItem.Quantity;
          paramAmount.Value = collateralItem.Amount;
          paramCurrencyIso.Value = collateralItem.CurrencyIso;
          paramContractId.Value = collateralItem.ContractId;
          
          if (collateralItem.ExpiryDate.Equals("2000-00-00")||collateralItem.ExpiryDate.Equals("20  -  -  "))
          {
            paramExpiryDate.Value = DBNull.Value;
          }
          else
          {
            paramExpiryDate.Value = collateralItem.ExpiryDate;
          }

          sqlCommandLoad.ExecuteNonQuery();
        }

        if (n.Equals(collateral.Count))
        {
          Log.Write("Inserted " + n.ToString("#,##0") + " collateral data record[s]. [Loanet.CollateralLoad]", 2);
        }
        else
        {
          throw (new Exception("Inserted " + n.ToString("#,##0") + " collateral data record[s] out of an expected "
            + collateral.Count.ToString("#,##0") + " for " + collateral.BizDate + ". [Loanet.CollateralLoad]"));
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
      }

      return collateral.BizDate;
    }

    public void RecallsDo(string clientId)
    {
      string bizDate;
      string fileStatus = "";
      string fileFileTime = "";

      if (KeyValue.Get("LoanetRecallsBizDate" + clientId, "0001-01-01", dbCn).Equals(Master.BizDatePrior)) // Done for today.
      {
        Log.Write("Recall data is current for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.RecallsDo]", 2);
        return;
      }

      try
      {
        fileFileTime = filer.FileTime(
          Standard.ConfigValue("RecallsRemotePath") + "pro" + clientId,
          Standard.ConfigValue("FileHost"),
          Standard.ConfigValue("FileUserId"),
          Standard.ConfigValue("FilePassword"));          
        
        if (fileFileTime.Length.Equals(0))
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

      if (fileStatus.Equals("OK") // Good host connection and file exists.
        &&(!fileFileTime.Equals(KeyValue.Get("LoanetRecallsFileTime" + clientId, "", dbCn)))) // File is new.
      {
        Log.Write("Loading recall data for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.RecallsDo]", 2);

        try
        {
          bizDate = RecallsLoad(
            Standard.ConfigValue("RecallsRemotePath") + "pro" + clientId,
            Standard.ConfigValue("FileHost"),
            Standard.ConfigValue("FileUserId"),
            Standard.ConfigValue("FilePassword"));
        
          if (!bizDate.Equals(Master.BizDatePrior))
          {
            Log.Write("Recall data for " + clientId + " is dated " + bizDate + ". [Loanet.RecallsDo]", Log.Error, 1);
            return;
          }

          fileStatus = "OK";

          RecallUpdate(bizDate, clientId);
          
          KeyValue.Set("LoanetRecallsBizDate" + clientId, bizDate, dbCn);
          KeyValue.Set("LoanetRecallsFileTime" + clientId, fileFileTime, dbCn);

          Log.Write("Done processing recall data for " + clientId + ". [Loanet.RecallsDo]", 2);
        }
        catch (Exception e)
        {
          fileStatus = e.Message;
          Log.Write(e.Message + " [Loanet.RecallsDo]", 1);
        }
      }
      else
      {
        if (fileStatus.Equals("OK"))
        {
          Log.Write("Recall data for " + clientId + " awaiting new data file for " + Master.BizDatePrior + ". [Loanet.RecallsDo]", 2);
        }
        else
        {
          Log.Write("Recall data for " + clientId + " file status: " + fileStatus + " [Loanet.RecallsDo]", 2);
        }
      }
    }

    private string RecallsLoad(string remotePathName, string hostName, string userId, string password)
    {
      int n = 0;
      
      Recalls recalls;
      RecallItem recallItem;
      
      SqlCommand sqlCommand;
              
      try
      {
        recalls = new Recalls();
 
        Log.Write("Loading data from filestream //" + hostName + remotePathName + ". [Loanet.RecallsLoad]", 2);        
        recalls.Load(remotePathName, hostName, userId, password);

        sqlCommand = new SqlCommand("spLoanetRecallPurge", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = recalls.BizDate;
		  
		SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
		paramClientId.Value = recalls.ClientId;
        
        Log.Write("Purging existing recall records for " + recalls.BizDate + ". [Loanet.RecallsLoad]", 2);

        dbCn.Open();
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText = "spLoanetRecallInsert";
       
        //SqlParameter paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.Char, 4);
        SqlParameter paramContractId = sqlCommand.Parameters.Add("@ContractId", SqlDbType.Char, 9);
        SqlParameter paramContractType = sqlCommand.Parameters.Add("@ContractType", SqlDbType.Char, 1);
        SqlParameter paramContraClientId = sqlCommand.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
        SqlParameter paramSecId = sqlCommand.Parameters.Add("@SecId", SqlDbType.VarChar, 9);
        SqlParameter paramQuantity = sqlCommand.Parameters.Add("@Quantity", SqlDbType.BigInt);
        SqlParameter paramRecallDate = sqlCommand.Parameters.Add("@RecallDate", SqlDbType.DateTime);
        SqlParameter paramBuyInDate = sqlCommand.Parameters.Add("@BuyInDate", SqlDbType.DateTime);
        SqlParameter paramStatus = sqlCommand.Parameters.Add("@Status", SqlDbType.Char, 1);
        SqlParameter paramReasonCode = sqlCommand.Parameters.Add("@ReasonCode", SqlDbType.Char, 2);
        SqlParameter paramRecallId = sqlCommand.Parameters.Add("@RecallId", SqlDbType.Char, 16);
        SqlParameter paramSequenceNumber = sqlCommand.Parameters.Add("@SequenceNumber", SqlDbType.SmallInt);
        SqlParameter paramComment = sqlCommand.Parameters.Add("@Comment", SqlDbType.VarChar, 11);
        
        Log.Write("Inserting " + recalls.Count.ToString("#,##0") + " recall item[s] into database.", 2);        
        
        for (n = 0; n < recalls.Count; n++)
        {
          recallItem = recalls.RecallItem(n);

          paramClientId.Value = recallItem.ClientId;
          paramContractId.Value = recallItem.ContractId;
          paramContractType.Value = recallItem.ContractType;
          paramContraClientId.Value = recallItem.ContraClientId;
          paramSecId.Value = recallItem.SecId;
          paramQuantity.Value = recallItem.Quantity;
          
          if (recallItem.RecallDate.Equals("2000-00-00") || recallItem.RecallDate.Equals("20  -  -  "))
          {
            paramRecallDate.Value = DBNull.Value;
          }
          else
          {
            paramRecallDate.Value = recallItem.RecallDate;
          }
          
          if (recallItem.BuyInDate.Equals("2000-00-00") || recallItem.BuyInDate.Equals("20  -  -  "))
          {
            paramBuyInDate.Value = DBNull.Value;
          }
          else
          {
            paramBuyInDate.Value = recallItem.BuyInDate;
          }
          
          paramStatus.Value = recallItem.Status;
          paramReasonCode.Value = recallItem.ReasonCode;
          paramRecallId.Value = recallItem.RecallId;
          paramSequenceNumber.Value = recallItem.SequenceNumber;
          paramComment.Value = recallItem.Comment;
          
          sqlCommand.ExecuteNonQuery();
        }

        if (n.Equals(recalls.Count))
        {
          Log.Write("Inserted " + n.ToString("#,##0") + " recall item[s].", 2);
        }
        else
        {
          throw (new Exception("Inserted " + n.ToString("#,##0") + " recall item[s] out of an expected "
            + recalls.Count.ToString("#,##0") + " for " + recalls.BizDate + ". [Loanet.RecallsLoad]"));
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
      }

      return recalls.BizDate;
    }
    
    private void RecallUpdate(string bizDate, string clientId)
    {
      SqlCommand sqlCommand;
      
      try
      {
        sqlCommand = new SqlCommand("spLoanetRecallUpdate", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.CommandTimeout = 300;

        SqlParameter  paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = bizDate;    

		SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
		paramClientId.Value = clientId;
          
        Log.Write("Updating main contract records for " + clientId + " for " + bizDate + ". [Loanet.RecallUpdate]", 2);
          
        dbCn.Open();
        sqlCommand.ExecuteNonQuery();
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
    }

    public void ClientsDo(string clientId)
    {
      string bizDate = "";
      string fileStatus = "";
      string fileTime = "";

      if (KeyValue.Get("LoanetClientsBizDate" + clientId, "0001-01-01", dbCn).Equals(Master.BizDatePrior)) // Done for today.
      {
        Log.Write("Client data is current for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.ClientsDo]", 2);
        return;
      }

      try
      {
        fileTime = filer.FileTime(
          Standard.ConfigValue("ClientsRemotePath") + "nao" + clientId,
          Standard.ConfigValue("FileHost"),
          Standard.ConfigValue("FileUserId"),
          Standard.ConfigValue("FilePassword"));         
        
       if (fileTime.Length.Equals(0))
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

      if (fileStatus.Equals("OK") // Good host connection and file exists.
        &&(!fileTime.Equals(KeyValue.Get("LoanetClientsFileTime" + clientId, "", dbCn)))) // File is new.
      {
        Log.Write("Loading client data for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.ClientsDo]", 2);

        try
        {
          bizDate = ClientsLoad(
            Standard.ConfigValue("ClientsRemotePath") + "nao" + clientId,
            Standard.ConfigValue("FileHost"),
            Standard.ConfigValue("FileUserId"),
            Standard.ConfigValue("FilePassword"));
                
          if (!bizDate.Equals(Master.BizDatePrior))
          {
            Log.Write("Client data for " + clientId + " is dated " + bizDate + ". [Loanet.ClientsDo]", Log.Error, 1);
            return;
          }

          fileStatus = "OK";

          ClientBookUpdate(bizDate, clientId);
          
          KeyValue.Set("LoanetClientsBizDate" + clientId, bizDate, dbCn);
          KeyValue.Set("LoanetClientsFileTime" + clientId, fileTime, dbCn);

          Log.Write("Done processing client data for " + clientId + ". [Loanet.ClientsDo]", 2);
        }
        catch (Exception e)
        {
          fileStatus = e.Message;
          Log.Write(e.Message + " [Loanet.ClientsDo]", 1);
        }
      }
      else
      {
        if (fileStatus.Equals("OK"))
        {
          Log.Write("Client data for " + clientId + " awaiting new data file for " + Master.BizDatePrior + ". [Loanet.ClientsDo]", 2);
        }
        else
        {
          Log.Write("Client data for " + clientId + " file status: " + fileStatus + " [Loanet.ClientsDo]", 2);
        }
      }
    }

    private string ClientsLoad(string remotePathName, string hostName, string userId, string password)
    {
      int n = 0;
      
      Clients clients;
      ClientItem clientItem;

      SqlCommand sqlCommand;
      
      try
      {
        clients = new Clients();

        Log.Write("Loading data from filestream //" + hostName + remotePathName + ". [Loanet.ClientsLoad]", 2);        
        clients.Load(remotePathName, hostName, userId, password);
        
        if(clients.Count > 0)
        {
          sqlCommand = new SqlCommand("spLoanetClientPurge", dbCn);
          sqlCommand.CommandType = CommandType.StoredProcedure;

          SqlParameter  paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
          paramBizDate.Value = clients.BizDate;

          SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
          paramClientId.Value = clients.ClientId;
          
          Log.Write("Purging existing client records for " + clients.ClientId + " for " + clients.BizDate + ". [Loanet.ClientsLoad]", 2);
          
          dbCn.Open();
          sqlCommand.ExecuteNonQuery();
          
          sqlCommand.CommandText = "spLoanetClientInsert";
        
          SqlParameter	paramContraClientId = sqlCommand.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
          SqlParameter	paramContraClientDtc = sqlCommand.Parameters.Add("@ContraClientDtc", SqlDbType.Char,4);
          SqlParameter	paramMinMarkAmount = sqlCommand.Parameters.Add("@MinMarkAmount", SqlDbType.Char, 4);
          SqlParameter	paramMinMarkPrice = sqlCommand.Parameters.Add("@MinMarkPrice", SqlDbType.Char, 1);
          SqlParameter	paramMarkRoundHouse = sqlCommand.Parameters.Add("@MarkRoundHouse ", SqlDbType.Char, 3);
          SqlParameter	paramMarkRoundInstitution = sqlCommand.Parameters.Add("@MarkRoundInstitution", SqlDbType.Char, 3);
          SqlParameter	paramAccountName= sqlCommand.Parameters.Add("@AccountName", SqlDbType.VarChar, 30);
          SqlParameter	paramAddressLine2 = sqlCommand.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 30);
          SqlParameter	paramParamsApply = sqlCommand.Parameters.Add("@ParamsApply", SqlDbType.Char, 1) ;
          SqlParameter	paramBorrowMarkCode = sqlCommand.Parameters.Add("@BorrowMarkCode", SqlDbType.Char, 1) ;
          SqlParameter	paramBorrowCollateralCode = sqlCommand.Parameters.Add("@BorrowCollateralCode", SqlDbType.Char, 3);
          SqlParameter	paramLoanMarkCode = sqlCommand.Parameters.Add("@LoanMarkCode", SqlDbType.Char, 1);
          SqlParameter	paramLoanCollateralCode = sqlCommand.Parameters.Add("@LoanCollateralCode", SqlDbType.Char, 3);
          SqlParameter	paramAddressLine3 = sqlCommand.Parameters.Add("@AddressLine3", SqlDbType.VarChar, 30);
          SqlParameter	paramAddressLine4 = sqlCommand.Parameters.Add("@AddressLine4", SqlDbType.VarChar, 30);
          SqlParameter	paramRelatedAccount = sqlCommand.Parameters.Add("@RelatedAccount", SqlDbType.VarChar, 4);
          SqlParameter	paramBorrowLimit = sqlCommand.Parameters.Add("@BorrowLimit", SqlDbType.Char, 10);
          SqlParameter	paramBorrowDateChange = sqlCommand.Parameters.Add("@BorrowDateChange", SqlDbType.Char, 6);
          SqlParameter	paramLoanLimit= sqlCommand.Parameters.Add("@LoanLimit", SqlDbType.Char, 10);
          SqlParameter	paramLoanDateChange= sqlCommand.Parameters.Add("@LoanDateChange", SqlDbType.Char, 6);
          SqlParameter	paramBorrowSecLimit = sqlCommand.Parameters.Add("@BorrowSecLimit", SqlDbType.Char, 10);
          SqlParameter	paramBorrowSecDateChange= sqlCommand.Parameters.Add("@BorrowSecDateChange", SqlDbType.Char, 6);
          SqlParameter	paramLoanSecLimit = sqlCommand.Parameters.Add("@LoanSecLimit", SqlDbType.Char, 10);
          SqlParameter	paramLoanSecDateChange = sqlCommand.Parameters.Add("@LoanSecDateChange", SqlDbType.Char, 6);
          SqlParameter	paramTaxId = sqlCommand.Parameters.Add("@TaxId", SqlDbType.VarChar, 9);
          SqlParameter	paramStockBorrowRate = sqlCommand.Parameters.Add("@StockBorrowRate", SqlDbType.Char, 5);
          SqlParameter	paramStockLoanRate = sqlCommand.Parameters.Add("@StockLoanRate", SqlDbType.Char, 5);
          SqlParameter	paramBondBorrowRate = sqlCommand.Parameters.Add("@BondBorrowRate", SqlDbType.Char, 5);
          SqlParameter	paramBondLoanRate = sqlCommand.Parameters.Add("@BondLoanRate", SqlDbType.Char, 5);
          SqlParameter	paramDtcMarkNumber = sqlCommand.Parameters.Add("@DtcMarkNumber", SqlDbType.Char, 4);
          SqlParameter	paramCreditLimitAccount = sqlCommand.Parameters.Add("@CreditLimitAccount", SqlDbType.VarChar, 4);
          SqlParameter	paramCallbackAccount = sqlCommand.Parameters.Add("@CallbackAccount", SqlDbType.VarChar, 4);
          SqlParameter	paramThirdPartyInstructions = sqlCommand.Parameters.Add("@ThirdPartyInstructions", SqlDbType.VarChar, 17);
          SqlParameter	paramAdditionalAddress= sqlCommand.Parameters.Add("@AdditionalAddress", SqlDbType.VarChar, 25);
          SqlParameter	paramOccAccountFlag = sqlCommand.Parameters.Add("@OccAccountFlag", SqlDbType.VarChar, 1); 
        
          Log.Write("Inserting " + clients.Count.ToString("#,##0") + " client records into database. [Loanet.ClientsLoad]", 2);        
          
          for (n = 0; n < clients.Count; n++)
          {
            clientItem = clients.ClientItem(n);
            
            paramContraClientId.Value = clientItem.ContraClientId;
            paramContraClientDtc.Value = clientItem.ContraClientDtc;
            paramMinMarkAmount.Value = clientItem.MinMarkAmount;
            paramMinMarkPrice.Value = clientItem.MinMarkPrice;
            paramMarkRoundHouse.Value = clientItem.MarkRoundHouse;
            paramMarkRoundInstitution.Value = clientItem.MarkRoundInstitution;
            paramAccountName.Value= clientItem.AccountName;
            paramAddressLine2.Value = clientItem.AddressLine2;
            paramParamsApply.Value = clientItem.ParamsApply;
            paramBorrowMarkCode.Value = clientItem.BorrowMarkCode;
            paramBorrowCollateralCode.Value = clientItem.BorrowCollateralCode;
            paramLoanMarkCode.Value = clientItem.LoanMarkCode;
            paramLoanCollateralCode.Value = clientItem.LoanCollateralCode;
            paramAddressLine3.Value = clientItem.AddressLine3;
            paramAddressLine4.Value = clientItem.AddressLine4;
            paramRelatedAccount.Value = clientItem.RelatedAccount;
            paramBorrowLimit.Value = clientItem.BorrowLimit;
            paramBorrowDateChange.Value = clientItem.BorrowDateChange;
            paramLoanLimit.Value= clientItem.LoanLimit;
            paramLoanDateChange.Value= clientItem.LoanDateChange;
            paramBorrowSecLimit.Value = clientItem.BorrowSecLimit;
            paramBorrowSecDateChange.Value= clientItem.BorrowSecDateChange;
            paramLoanSecLimit.Value = clientItem.LoanSecLimit;
            paramLoanSecDateChange.Value = clientItem.LoanSecDateChange;
            paramTaxId.Value = clientItem.TaxId;
            paramStockBorrowRate.Value = clientItem.StockBorrowRate;
            paramStockLoanRate.Value = clientItem.StockLoanRate;
            paramBondBorrowRate.Value = clientItem.BondBorrowRate;
            paramBondLoanRate.Value = clientItem.BondLoanRate;
            paramDtcMarkNumber.Value = clientItem.DtcMarkNumber;
            paramCreditLimitAccount.Value  = clientItem.CreditLimitAccount;
            paramCallbackAccount.Value = clientItem.CallbackAccount;
            paramThirdPartyInstructions.Value = clientItem.ThirdPartyInstructions;
            paramAdditionalAddress.Value= clientItem.AdditionalAddress;
            paramOccAccountFlag.Value = clientItem.OccAccountFlag;
         
            sqlCommand.ExecuteNonQuery();
          }
        }
        
        if (n.Equals(clients.Count))
        {
          Log.Write("Inserted " + n.ToString("#,##0") + " client data record[s]. [Loanet.ClientsLoad]", 2);
        }
        else
        {
          throw (new Exception("Inserted " + n.ToString("#,##0") + " client data record[s] out of an expected "
            + clients.Count.ToString("#,##0") + " for " + clients.BizDate + " for " + clients.ClientId + ". [Loanet.ClientsLoad]"));
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
      }
  
      return clients.BizDate;
    }

    private void ClientBookUpdate(string bizDate, string clientId)
    {
      SqlCommand sqlCommand;
      
      try
      {
        sqlCommand = new SqlCommand("spLoanetBookUpdate", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter  paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = bizDate;

        SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
        paramClientId.Value = clientId;
          
        Log.Write("Updating main book records for " + clientId + " for " + bizDate + ". [Loanet.ClientBookUpdate]", 2);
          
        dbCn.Open();
        sqlCommand.ExecuteNonQuery();
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
    }
 
    public void ClientsLongDo(string clientId)
    {
      string bizDate = "";
      string fileStatus = "";
      string fileTime = "";

      if (KeyValue.Get("LoanetClientsBizDate" + clientId, "0001-01-01", dbCn).Equals(Master.BizDatePrior)) // Done for today.
      {
        Log.Write("Client data is current for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.ClientsLongDo]", 2);
        return;
      }

      try
      {
        fileTime = filer.FileTime(
          Standard.ConfigValue("ClientsLongRemotePath") + "pno" + clientId,
          Standard.ConfigValue("FileHost"),
          Standard.ConfigValue("FileUserId"),
          Standard.ConfigValue("FilePassword"));         
        
        if (fileTime.Length.Equals(0))
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

      if (fileStatus.Equals("OK") // Good host connection and file exists.
        &&(!fileTime.Equals(KeyValue.Get("LoanetClientsFileTime" + clientId, "", dbCn)))) // File is new.
      {
        Log.Write("Loading client data for " + clientId + " for " + Master.BizDatePrior + ". [Loanet.ClientsLongDo]", 2);

        try
        {
          bizDate = ClientsLongLoad(
            Standard.ConfigValue("ClientsLongRemotePath") + "pno" + clientId,
            Standard.ConfigValue("FileHost"),
            Standard.ConfigValue("FileUserId"),
            Standard.ConfigValue("FilePassword"));
                
          if (!bizDate.Equals(Master.BizDatePrior))
          {
            Log.Write("Client data for " + clientId + " is dated " + bizDate + ". [Loanet.ClientsLongDo]", Log.Error, 1);
            return;
          }

          fileStatus = "OK";

          ClientLongBookUpdate(bizDate, clientId);
          
          KeyValue.Set("LoanetClientsBizDate" + clientId, bizDate, dbCn);
          KeyValue.Set("LoanetClientsFileTime" + clientId, fileTime, dbCn);

          Log.Write("Done processing client data for " + clientId + ". [Loanet.ClientsLongDo]", 2);
        }
        catch (Exception e)
        {
          fileStatus = e.Message;
          Log.Write(e.Message + " [Loanet.ClientsDo]", 1);
        }
      }
      else
      {
        if (fileStatus.Equals("OK"))
        {
          Log.Write("Client data for " + clientId + " awaiting new data file for " + Master.BizDatePrior + ". [Loanet.ClientsLongDo]", 2);
        }
        else
        {
          Log.Write("Client data for " + clientId + " file status: " + fileStatus + " [Loanet.ClientsLongDo]", 2);
        }
      }
    }

    private string ClientsLongLoad(string remotePathName, string hostName, string userId, string password)
    {
      int n = 0;
      
      ClientsLong clientsLong;
      ClientLongItem clientLongItem;

      SqlCommand sqlCommand;

      try
      {
        clientsLong = new ClientsLong();

        Log.Write("Loading data from filestream //" + hostName + remotePathName + ". [Loanet.ClientsLongLoad]", 2);        
        clientsLong.Load(remotePathName, hostName, userId, password);
        
        if(clientsLong.Count > 0)
        {
          sqlCommand = new SqlCommand("spLoanetClientLongPurge", dbCn);
          sqlCommand.CommandType = CommandType.StoredProcedure;

          SqlParameter  paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
          paramBizDate.Value = clientsLong.BizDate;

          SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
          paramClientId.Value = clientsLong.ClientId;
          
          Log.Write("Purging existing client records for " + clientsLong.ClientId + " for " + clientsLong.BizDate + ". [Loanet.ClientsLongLoad]", 2);
          
          dbCn.Open();
          sqlCommand.ExecuteNonQuery();
          
          sqlCommand.CommandText = "spLoanetClientLongInsert";
        
          SqlParameter	paramContraClientId = sqlCommand.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
          SqlParameter	paramAccountName = sqlCommand.Parameters.Add("@AccountName", SqlDbType.VarChar, 30);
          SqlParameter	paramAddressLine1 = sqlCommand.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 30);
          SqlParameter	paramAddressLine2 = sqlCommand.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 30);
          SqlParameter	paramAddressLine3 = sqlCommand.Parameters.Add("@AddressLine3", SqlDbType.VarChar, 30);
          SqlParameter	paramAddressLine4 = sqlCommand.Parameters.Add("@AddressLine4", SqlDbType.VarChar, 30);
          SqlParameter	paramAddressLine5 = sqlCommand.Parameters.Add("@AddressLine5", SqlDbType.VarChar, 30);
          
          SqlParameter	paramPhone = sqlCommand.Parameters.Add("@Phone", SqlDbType.Char, 14);
          SqlParameter	paramTaxId = sqlCommand.Parameters.Add("@TaxId", SqlDbType.VarChar, 9);
          
          SqlParameter	paramContraClientDtc = sqlCommand.Parameters.Add("@ContraClientDtc", SqlDbType.Char,4);         
          SqlParameter	paramThirdPartyInstructions = sqlCommand.Parameters.Add("@ThirdPartyInstructions", SqlDbType.VarChar, 17);
          SqlParameter	paramDeliveryInstructions = sqlCommand.Parameters.Add("@DeliveryInstructions", SqlDbType.VarChar, 30);
          SqlParameter	paramMarkDtc = sqlCommand.Parameters.Add("@MarkDtc", SqlDbType.Char, 4);
          SqlParameter	paramMarkInstructions = sqlCommand.Parameters.Add("@MarkInstructions", SqlDbType.VarChar, 30);
          SqlParameter	paramRecallDtc = sqlCommand.Parameters.Add("@RecallDtc", SqlDbType.Char, 4);
          SqlParameter	paramCdxCuId = sqlCommand.Parameters.Add("@CdxCuId", SqlDbType.Char, 4);
          SqlParameter	paramOccDelivery = sqlCommand.Parameters.Add("@OccDelivery", SqlDbType.Char, 1);
     
          SqlParameter	paramParentAccount = sqlCommand.Parameters.Add("@ParentAccount", SqlDbType.VarChar, 4);
          SqlParameter	paramAssociatedAccount = sqlCommand.Parameters.Add("@AssociatedAccount", SqlDbType.VarChar, 4);
          SqlParameter	paramCreditLimitAccount = sqlCommand.Parameters.Add("@CreditLimitAccount", SqlDbType.VarChar, 4);
          SqlParameter	paramAssociatedCbAccount = sqlCommand.Parameters.Add("@AssociatedCbAccount", SqlDbType.VarChar, 4);

          SqlParameter	paramBorrowLimit = sqlCommand.Parameters.Add("@BorrowLimit", SqlDbType.Char, 14);
          SqlParameter	paramBorrowDateChange = sqlCommand.Parameters.Add("@BorrowDateChange", SqlDbType.Char, 8);
          SqlParameter	paramBorrowSecLimit = sqlCommand.Parameters.Add("@BorrowSecLimit", SqlDbType.Char, 14);
          SqlParameter	paramBorrowSecDateChange= sqlCommand.Parameters.Add("@BorrowSecDateChange", SqlDbType.Char, 8);
          SqlParameter	paramLoanLimit= sqlCommand.Parameters.Add("@LoanLimit", SqlDbType.Char, 14);
          SqlParameter	paramLoanDateChange= sqlCommand.Parameters.Add("@LoanDateChange", SqlDbType.Char, 8);
          SqlParameter	paramLoanSecLimit = sqlCommand.Parameters.Add("@LoanSecLimit", SqlDbType.Char, 14);
          SqlParameter	paramLoanSecDateChange = sqlCommand.Parameters.Add("@LoanSecDateChange", SqlDbType.Char, 8);
          
          SqlParameter	paramMinMarkAmount = sqlCommand.Parameters.Add("@MinMarkAmount", SqlDbType.Char, 4);
          SqlParameter	paramMinMarkPrice = sqlCommand.Parameters.Add("@MinMarkPrice", SqlDbType.Char, 1);

          SqlParameter	paramMarkRoundHouse = sqlCommand.Parameters.Add("@MarkRoundHouse ", SqlDbType.Char, 3);
          SqlParameter	paramMarkRoundInstitution = sqlCommand.Parameters.Add("@MarkRoundInstitution", SqlDbType.Char, 3);
          SqlParameter	paramMarkValueHouse = sqlCommand.Parameters.Add("@MarkValueHouse ", SqlDbType.Char, 4);
          SqlParameter	paramMarkValueInstitution = sqlCommand.Parameters.Add("@MarkValueInstitution", SqlDbType.Char, 4);
          
          SqlParameter	paramBorrowMarkCode = sqlCommand.Parameters.Add("@BorrowMarkCode", SqlDbType.Char, 1) ;
          SqlParameter	paramBorrowCollateral = sqlCommand.Parameters.Add("@BorrowCollateral", SqlDbType.Char, 3);
          SqlParameter	paramLoanMarkCode = sqlCommand.Parameters.Add("@LoanMarkCode", SqlDbType.Char, 1);
          SqlParameter	paramLoanCollateral = sqlCommand.Parameters.Add("@LoanCollateral", SqlDbType.Char, 3);
          
          SqlParameter	paramIncludeAccrued = sqlCommand.Parameters.Add("@IncludeAccrued", SqlDbType.Char, 1);

          SqlParameter	paramStandardMark = sqlCommand.Parameters.Add("@StandardMark", SqlDbType.Char, 1);
          SqlParameter	paramOmnibusMark = sqlCommand.Parameters.Add("@OmnibusMark", SqlDbType.Char, 1);
          
          SqlParameter	paramStockBorrowRate = sqlCommand.Parameters.Add("@StockBorrowRate", SqlDbType.Char, 5);
          SqlParameter	paramStockLoanRate = sqlCommand.Parameters.Add("@StockLoanRate", SqlDbType.Char, 5);
          SqlParameter	paramBondBorrowRate = sqlCommand.Parameters.Add("@BondBorrowRate", SqlDbType.Char, 5);
          SqlParameter	paramBondLoanRate = sqlCommand.Parameters.Add("@BondLoanRate", SqlDbType.Char, 5);
          
          SqlParameter	paramBusinessIndex = sqlCommand.Parameters.Add("@BusinessIndex", SqlDbType.VarChar, 2);
          SqlParameter	paramBusinessAmount = sqlCommand.Parameters.Add("@BusinessAmount", SqlDbType.VarChar, 18);
          SqlParameter	paramInstitutionalCashPool = sqlCommand.Parameters.Add("@InstitutionalCashPool", SqlDbType.Char, 1);
          SqlParameter	paramInstitutionalFeeType = sqlCommand.Parameters.Add("@InstitutionalFeeType", SqlDbType.Char, 1);          
          SqlParameter	paramInstitutionalFeeRate = sqlCommand.Parameters.Add("@InstitutionalFeeRate", SqlDbType.Char, 5);
          
          SqlParameter	paramLoanEquity = sqlCommand.Parameters.Add("@LoanEquity", SqlDbType.Char, 1);
          SqlParameter	paramLoanDebt = sqlCommand.Parameters.Add("@LoanDebt", SqlDbType.Char, 1);
          SqlParameter	paramReturnEquity = sqlCommand.Parameters.Add("@ReturnEquity", SqlDbType.Char, 1);
          SqlParameter	paramReturnDebt = sqlCommand.Parameters.Add("@ReturnDebt", SqlDbType.Char, 1);
          SqlParameter	paramIncomeEquity = sqlCommand.Parameters.Add("@IncomeEquity", SqlDbType.Char, 1);
          SqlParameter	paramIncomeDebt = sqlCommand.Parameters.Add("@IncomeDebt", SqlDbType.Char, 1);
          
          SqlParameter	paramDayBasis = sqlCommand.Parameters.Add("@DayBasis", SqlDbType.Char, 1);
          SqlParameter	paramAccountClass = sqlCommand.Parameters.Add("@AccountClass", SqlDbType.Char, 3);
        
          Log.Write("Inserting " + clientsLong.Count.ToString("#,##0") + " client records into database. [Loanet.ClientsLongLoad]", 2);        
          
          for (n = 0; n < clientsLong.Count; n++)
          {
            clientLongItem = clientsLong.ClientLongItem(n);
            
            paramContraClientId.Value = clientLongItem.ContraClientId;
            paramAccountName.Value = clientLongItem.AccountName;
            paramAddressLine1.Value = clientLongItem.AddressLine1;
            paramAddressLine2.Value = clientLongItem.AddressLine2;
            paramAddressLine3.Value = clientLongItem.AddressLine3;
            paramAddressLine4.Value = clientLongItem.AddressLine4;
            paramAddressLine5.Value = clientLongItem.AddressLine5;
          
            paramPhone.Value = clientLongItem.Phone;
            paramTaxId.Value = clientLongItem.TaxId;
          
            paramContraClientDtc.Value = clientLongItem.ContraClientDtc;         
            paramThirdPartyInstructions.Value = clientLongItem.ThirdPartyInstructions;
            paramDeliveryInstructions.Value = clientLongItem.DeliveryInstructions;
            paramMarkDtc.Value = clientLongItem.MarkDtc;
            paramMarkInstructions.Value = clientLongItem.MarkInstructions;
            paramRecallDtc.Value = clientLongItem.RecallDtc;
            paramCdxCuId.Value = clientLongItem.CdxCuId;
            paramOccDelivery.Value = clientLongItem.OccDelivery;
     
            paramParentAccount.Value = clientLongItem.ParentAccount;
            paramAssociatedAccount.Value = clientLongItem.AssociatedAccount;
            paramCreditLimitAccount.Value = clientLongItem.CreditLimitAccount;
            paramAssociatedCbAccount.Value = clientLongItem.AssociatedCbAccount;

            paramBorrowLimit.Value = clientLongItem.BorrowLimit;
            paramBorrowDateChange.Value = clientLongItem.BorrowDateChange;
            paramBorrowSecLimit.Value = clientLongItem.BorrowSecLimit;
            paramBorrowSecDateChange.Value = clientLongItem.BorrowSecDateChange;
            paramLoanLimit.Value = clientLongItem.LoanLimit;
            paramLoanDateChange.Value = clientLongItem.LoanDateChange;
            paramLoanSecLimit.Value = clientLongItem.LoanSecLimit;
            paramLoanSecDateChange.Value = clientLongItem.LoanSecDateChange;
          
            paramMinMarkAmount.Value = clientLongItem.MinMarkAmount;
            paramMinMarkPrice.Value = clientLongItem.MinMarkPrice;

            paramMarkRoundHouse.Value = clientLongItem.MarkRoundHouse;
            paramMarkRoundInstitution.Value = clientLongItem.MarkRoundInstitution;
            paramMarkValueHouse.Value = clientLongItem.MarkValueHouse;
            paramMarkValueInstitution.Value = clientLongItem.MarkValueInstitution;
          
            paramBorrowMarkCode.Value = clientLongItem.BorrowMarkCode;
            paramBorrowCollateral.Value = clientLongItem.BorrowCollateral;
            paramLoanMarkCode.Value = clientLongItem.LoanMarkCode;
            paramLoanCollateral.Value = clientLongItem.LoanCollateral;
          
            paramIncludeAccrued.Value = clientLongItem.IncludeAccrued;

            paramStandardMark.Value = clientLongItem.StandardMark;
            paramOmnibusMark.Value = clientLongItem.OmnibusMark;
          
            paramStockBorrowRate.Value = clientLongItem.StockBorrowRate;
            paramStockLoanRate.Value = clientLongItem.StockLoanRate;
            paramBondBorrowRate.Value = clientLongItem.BondBorrowRate;
            paramBondLoanRate.Value = clientLongItem.BondLoanRate;
          
            paramBusinessIndex.Value = clientLongItem.BusinessIndex;
            paramBusinessAmount.Value = clientLongItem.BusinessAmount;
            paramInstitutionalCashPool.Value = clientLongItem.InstitutionalCashPool;
            paramInstitutionalFeeType.Value = clientLongItem.InstitutionalFeeType;          
            paramInstitutionalFeeRate.Value = clientLongItem.InstitutionalFeeRate;
          
            paramLoanEquity.Value = clientLongItem.LoanEquity;
            paramLoanDebt.Value = clientLongItem.LoanDebt;
            paramReturnEquity.Value = clientLongItem.ReturnEquity;
            paramReturnDebt.Value = clientLongItem.ReturnDebt;
            paramIncomeEquity.Value = clientLongItem.IncomeEquity;
            paramIncomeDebt.Value = clientLongItem.IncomeDebt;
          
            paramDayBasis.Value = clientLongItem.DayBasis;
            paramAccountClass.Value = clientLongItem.AccountClass;
         
            sqlCommand.ExecuteNonQuery();
          }
        }
        
        if (n.Equals(clientsLong.Count))
        {
          Log.Write("Inserted " + n.ToString("#,##0") + " client data record[s]. [Loanet.ClientsLongLoad]", 2);
        }
        else
        {
          throw (new Exception("Inserted " + n.ToString("#,##0") + " client data record[s] out of an expected "
            + clientsLong.Count.ToString("#,##0") + " for " + clientsLong.BizDate + " for " + clientsLong.ClientId + ". [Loanet.ClientsLongLoad]"));
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
      }
  
      return clientsLong.BizDate;
    }

    private void ClientLongBookUpdate(string bizDate, string clientId)
    {
      SqlCommand sqlCommand;
      
      try
      {
        sqlCommand = new SqlCommand("spLoanetBookLongUpdate", dbCn);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter  paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = bizDate;

        SqlParameter  paramClientId = sqlCommand.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
        paramClientId.Value = clientId;
          
        Log.Write("Updating main book records for " + clientId + " for " + bizDate + ". [Loanet.ClientBookUpdate]", 2);
          
        dbCn.Open();
        sqlCommand.ExecuteNonQuery();
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
    }
  }
}