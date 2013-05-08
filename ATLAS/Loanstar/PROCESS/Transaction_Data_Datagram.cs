using System;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
  public class Transaction_Data_Datagram
  {
    private ProcessStatusEventArgs[] processStatusEventArgs;

    private SqlConnection dbCn;
    private SqlCommand dbCmd;
    
    private SqlParameter paramSystemTime;	
    private SqlParameter paramClientId;
    private SqlParameter paramContractId;
    private SqlParameter paramMatchingContractId;
    private SqlParameter paramContractType;
    private SqlParameter paramContraClientId;
    private SqlParameter paramSecId;
    private SqlParameter paramInputTerminal;
    private SqlParameter paramInputTimestamp;
    private SqlParameter paramTransDescription;
    private SqlParameter paramDebitCreditFlag;
    private SqlParameter paramBatchCode;
    private SqlParameter paramDeliverViaCode;
    private SqlParameter paramQuantity;
    private SqlParameter paramAmount;
    private SqlParameter paramOriginalQuantity;
    private SqlParameter paramOriginalAmount;
    private SqlParameter paramCollateralCode;
    private SqlParameter paramValueDate;
    private SqlParameter paramSettleDate;
    private SqlParameter paramTermDate;
    private SqlParameter paramReturnDate;
    private SqlParameter paramInterestFromDate;
    private SqlParameter paramInterestToDate;
    private SqlParameter paramRate;
    private SqlParameter paramRateCode;
    private SqlParameter paramPoolCode;
    private SqlParameter paramDivRate;
    private SqlParameter paramDivCallable;
    private SqlParameter paramIncomeTracked;
    private SqlParameter paramMarginCode;
    private SqlParameter paramMargin;
    private SqlParameter paramCurrencyIso;
    private SqlParameter paramSecurityDepot;
    private SqlParameter paramCashDepot;
    private SqlParameter paramOtherClientId;
    private SqlParameter paramComment;
    private SqlParameter paramFiller;
    private SqlParameter paramAct;
        
    public Transaction_Data_Datagram(string dbCnStr)
    {
      dbCn = new SqlConnection(dbCnStr);
      dbCmd = new SqlCommand("spProcessDatagramTransactionUpdate", dbCn);
      dbCmd.CommandType = CommandType.StoredProcedure;

      paramSystemTime = dbCmd.Parameters.Add("@SystemTime", SqlDbType.DateTime);
      paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.Char, 4);
      paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.Char, 9);
      paramMatchingContractId = dbCmd.Parameters.Add("@MatchingContractId", SqlDbType.Char, 9);
      paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);            
      paramContraClientId = dbCmd.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);			
      paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
      paramInputTerminal = dbCmd.Parameters.Add("@InputTerminal", SqlDbType.VarChar, 2);
      paramInputTimestamp = dbCmd.Parameters.Add("@InputTimestamp", SqlDbType.VarChar, 8);
      paramTransDescription = dbCmd.Parameters.Add("@TransDescription", SqlDbType.VarChar, 11);
      paramDebitCreditFlag = dbCmd.Parameters.Add("@DebitCreditFlag", SqlDbType.Char, 1);
      paramBatchCode = dbCmd.Parameters.Add("@BatchCode", SqlDbType.Char, 1);			
      paramDeliverViaCode = dbCmd.Parameters.Add("@DeliverViaCode", SqlDbType.Char, 1);			
      paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);			
      paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);			
      paramOriginalQuantity = dbCmd.Parameters.Add("@OriginalQuantity", SqlDbType.BigInt);			
      paramOriginalAmount = dbCmd.Parameters.Add("@OriginalAmount", SqlDbType.Money);			
      paramCollateralCode = dbCmd.Parameters.Add("@CollateralCode", SqlDbType.VarChar, 1);			
      paramValueDate = dbCmd.Parameters.Add("@ValueDate", SqlDbType.DateTime);			
      paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);			
      paramTermDate = dbCmd.Parameters.Add("@TermDate", SqlDbType.DateTime);			
      paramReturnDate = dbCmd.Parameters.Add("@ReturnDate", SqlDbType.DateTime);			
      paramInterestFromDate = dbCmd.Parameters.Add("@InterestFromDate", SqlDbType.DateTime);			
      paramInterestToDate = dbCmd.Parameters.Add("@InterestToDate", SqlDbType.DateTime);			
      paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
      paramRateCode = dbCmd.Parameters.Add("@RateCode", SqlDbType.VarChar, 1);
      paramPoolCode = dbCmd.Parameters.Add("@PoolCode", SqlDbType.VarChar, 1);			
      paramDivRate = dbCmd.Parameters.Add("@DivRate", SqlDbType.Decimal);			
      paramDivCallable = dbCmd.Parameters.Add("@DivCallable", SqlDbType.Bit);			
      paramIncomeTracked = dbCmd.Parameters.Add("@IncomeTracked", SqlDbType.Bit);			
      paramMarginCode = dbCmd.Parameters.Add("@MarginCode", SqlDbType.VarChar, 1);			
      paramMargin = dbCmd.Parameters.Add("@Margin", SqlDbType.Decimal);			
      paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);			
      paramSecurityDepot = dbCmd.Parameters.Add("@SecurityDepot", SqlDbType.VarChar, 2);			
      paramCashDepot = dbCmd.Parameters.Add("@CashDepot", SqlDbType.VarChar, 2);			
      paramOtherClientId = dbCmd.Parameters.Add("@OtherClientId", SqlDbType.Char, 4);
      paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 20);			
      paramFiller = dbCmd.Parameters.Add("@Filler", SqlDbType.VarChar, 18);			
      
      paramAct = dbCmd.Parameters.Add("@Act", SqlDbType.VarChar, 255);
      paramAct.Direction = ParameterDirection.Output;
    }
	
    private string CurrencyIso(string ProcessCurrencyCode)
    {
      switch (ProcessCurrencyCode.Trim())
      {
        case "00": 
          return "USD";               

        default:
          return "***";
      }
    }    
    
    public ProcessStatusEventArgs[] Activity(string message)
    {
      if (message.Substring(11, 1).Equals("R"))
      {
        return Block_Rate_Transaction(message);
      }
      else
      {
        return Regular_Transaction(message);              
      }
    }
  
    private ProcessStatusEventArgs[] Block_Rate_Transaction(string message)
    {
      short recordCount = 0;

      string clientId             = message.Substring(12, 4);      
      string blockSequenceNumber  = message.Substring(16, 6);
      string recordSequenceNumber = message.Substring(22, 6);
      string recordsPresent       = message.Substring(28, 2);
      string systemTime           = message.Substring(3, 8);

      try
      {
        recordCount = short.Parse(recordsPresent);
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [Transaction_Data_Datagram.Block_Rate_Transaction]", 2);
        throw;
      }
      
      ProcessStatusEventArgs[] processStatusEventArgs = new ProcessStatusEventArgs[recordCount];

      for(short n = 0; n < recordCount; n += 1)
      {
        processStatusEventArgs[n] = Rate_Change_Record(clientId, systemTime, message.Substring(30 + (n * 22), 22));                  
      }

      return processStatusEventArgs;
    }

    private ProcessStatusEventArgs Rate_Change_Record(string clientId, string systemTime, string rateChangeRecord)
    {
      string contractType     = rateChangeRecord.Substring(0, 1);
      string contractId       = rateChangeRecord.Substring(1, 9);
      string rate             = rateChangeRecord.Substring(10, 3) + "." + rateChangeRecord.Substring(13, 4);
      string rateCode         = rateChangeRecord.Substring(17, 1);
      string transDescription = "Blck Rt Chg";               
  
      try
      {        
        paramSystemTime.Value = DateTime.ParseExact(systemTime, "HHmmssff", null).ToString();
        paramClientId.Value = clientId;
        paramContractId.Value = contractId;       
        paramContractType.Value = contractType;                        
        paramTransDescription.Value = transDescription;               
        paramRate.Value = rate;
        paramRateCode.Value = rateCode;
      
        dbCn.Open();
        dbCmd.ExecuteNonQuery();
    
        ProcessStatusEventArgs processStatusEventArgs = new ProcessStatusEventArgs(
          Standard.ProcessId(), 
          "L", 
          "DTX", 
          paramAct.Value.ToString(), 
          "", 
          "ADMIN", 
          false, 
          clientId, 
          contractId, 
          contractType, 
          "", 
          "",
		  "",
		  "",
          "",
          "", 
          "",
          "");

        return processStatusEventArgs;
      }
      catch (Exception error)
      {       
        Log.Write(error.Message + " [Transaction_Data_Datagram.Rate_Change_Record]", 2);       
        throw;
      }
      finally
      {       
        dbCn.Close();      
      }
    }
    
    private ProcessStatusEventArgs[] Regular_Transaction(string message)
    {      
      string systemTime           = message.Substring(3, 8);
      string clientId             = message.Substring(12, 4);
      string contractId           = message.Substring(49, 9);
      string matchingContractId   = message.Substring(174, 9);
      string contractType         = message.Substring(35, 1);
      string contraClientId       = message.Substring(45, 4);
      string secId                = message.Substring(36, 9);            
      string inputTerminal        = message.Substring(62, 2);
      string inputTimestamp       = message.Substring(64, 6);
      string transDescription     = message.Substring(70, 11).Trim();
      string debitCreditFlag      = message.Substring(81, 1);
      string batchCode            = message.Substring(34, 1);
      string deliverViaCode       = message.Substring(173, 1);
      string quantity             = message.Substring(82, 12); // Ignore the decimal component.
      string amount               = message.Substring(96, 12) + "." + message.Substring(108, 2);
      string originalQuantity     = message.Substring(188, 12);
      string originalAmount       = message.Substring(202, 12) + "." + message.Substring(214, 2);      
      string collateralCode       = message.Substring(110, 1);                        
      string valueDate            = message.Substring(216, 6);
      string settleDate           = message.Substring(115, 6);
      string expiryDate           = message.Substring(127, 6);            
      string returnDate           = message.Substring(161, 6);                  
      string interestFromDate     = message.Substring(121, 6);
      string interestToDate       = message.Substring(167, 6);
      string rate                 = message.Substring(133, 3) + "." + message.Substring(136, 4);
      string rateCode             = message.Substring(140, 1);      
      string poolCode             = message.Substring(183, 1);
      string divRate              = message.Substring(224, 3) + "." + message.Substring(227, 3);
      string divCallable          = message.Substring(230, 1);
      string incomeTracked        = message.Substring(231, 1);
      string marginCode           = message.Substring(184, 1);
      string margin               = message.Substring(185, 3);      
      string currencyIso          = CurrencyIso(message.Substring(32, 2));            
      string securityDepot        = message.Substring(222, 2);     
      string cashDepot            = message.Substring(30, 2);
      string otherClientId        = message.Substring(111, 2);
      string filler               = message.Substring(232, 18);                        
      string comment              = message.Substring(141, 20);
      string actType              = "";

      try
      {
        switch(transDescription.ToLower())
        {
          case "dk mark" :
					case "dk new"	 :
					case "dk rtn"	 :
            return null;
          case "base rt chg" :
            actType = "DTA";
            break;
          default: 
            actType = "DTX";
            break;
        }

        paramSystemTime.Value = DateTime.ParseExact(systemTime, "HHmmssff", null).ToString();
        paramClientId.Value = clientId;                
        paramContractId.Value = contractId;        
        paramMatchingContractId.Value = matchingContractId;
        paramContractType.Value = contractType;                                    
        paramContraClientId.Value = contraClientId;
        paramSecId.Value = secId.Trim();
        paramInputTerminal.Value = inputTerminal.Trim();
        paramInputTimestamp.Value = inputTimestamp.Trim();                        
        paramTransDescription.Value = transDescription;
        paramDebitCreditFlag.Value = debitCreditFlag;
        paramBatchCode.Value = batchCode;
        paramDeliverViaCode.Value = deliverViaCode;        
        paramQuantity.Value = quantity;
        paramAmount.Value = amount;      
        paramOriginalQuantity.Value = originalQuantity;
        paramOriginalAmount.Value = originalAmount;    
        paramCollateralCode.Value = collateralCode.Trim();
       
        if (!valueDate.Equals("000000"))
        {
          paramValueDate.Value = DateTime.ParseExact(valueDate, "MMddyy", null).ToString();
        }        
        
        if (!settleDate.Equals("000000"))
        {
          paramSettleDate.Value = DateTime.ParseExact(settleDate, "MMddyy", null).ToString();
        }        

        if (!expiryDate.Equals("000000"))
        {
          paramTermDate.Value = DateTime.ParseExact(expiryDate, "MMddyy", null).ToString();
        }

        if (!returnDate.Equals("000000"))
        {
          paramReturnDate.Value = DateTime.ParseExact(returnDate, "MMddyy", null).ToString();
        }

        if (!interestFromDate.Equals("000000"))
        {
          paramInterestFromDate.Value = DateTime.ParseExact(interestFromDate, "MMddyy", null).ToString();
        }
        
        if (!interestToDate.Equals("000000"))
        {
          paramInterestToDate.Value = DateTime.ParseExact(interestToDate, "MMddyy", null).ToString();
        }   
            
        paramRate.Value = rate;        
        paramRateCode.Value = rateCode.Trim();
        paramPoolCode.Value = poolCode.Trim();
        paramDivRate.Value = divRate;
        paramDivCallable.Value = divCallable.Equals("C").ToString();
        paramIncomeTracked.Value = (!incomeTracked.Equals("N")).ToString();
        paramMarginCode.Value = marginCode.Trim();
     
        if (!margin.Equals("000"))
        {
          paramMargin.Value = margin;
        }

        paramCurrencyIso.Value = currencyIso;
        paramSecurityDepot.Value = securityDepot.Trim();
        paramCashDepot.Value = cashDepot.Trim();
        paramOtherClientId.Value = otherClientId.Trim();        
        paramComment.Value = comment.Trim();
        paramFiller.Value = filler.Trim();                    
        
        dbCn.Open();
        dbCmd.ExecuteNonQuery();             
           
		
		long tempQuantity = 0;
		decimal tempAmount = 0;

		if (!debitCreditFlag.Equals(""))
		{
			if ((contractType.Equals("B") && debitCreditFlag.Equals("C")) || (contractType.Equals("L") && debitCreditFlag.Equals("D")))
			{
				tempQuantity = long.Parse(quantity) * (-1);
				quantity	 = tempQuantity.ToString();
				
				tempAmount	 = decimal.Parse(amount) * (-1);
				amount		 = tempAmount.ToString();
			}
		}
		  
		processStatusEventArgs = new ProcessStatusEventArgs[1];
        processStatusEventArgs[0] = new ProcessStatusEventArgs(
          Standard.ProcessId(), 
          "L", 
          actType,
          paramAct.Value.ToString(), 
          "", 
          filler.Trim(), 
          false, 
          clientId, 
          contractId, 
          contractType, 
          contraClientId, 
          secId, 
          "",
		  quantity,
		  amount,
          batchCode + " " + inputTerminal.Trim() + "  " + comment.Trim(),
          "",
          "");

        return processStatusEventArgs;
      }
      catch (Exception error)
      {  
        Log.Write(error.Message + " [Transaction_Data_Datagram.Regular_Transaction]", 3);                              
        throw;
      }
      finally
      {           
        dbCn.Close();           
      }
    }
  }
}


