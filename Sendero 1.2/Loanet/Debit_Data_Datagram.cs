// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class Debit_Data_Datagram
	{
	  private string dbCnStr;
    private ProcessStatusEventArgs processStatusEventArgsItem = null;

    public Debit_Data_Datagram(string dbCnStr)
		{
      this.dbCnStr = dbCnStr;
		}

    public ProcessStatusEventArgs Activity(string message)
    {
      string clientId        = message.Substring(12, 4);
      string sequence        = message.Substring(16, 6);
      string pendMadeFlag    = message.Substring(22, 1);
      string matchFlag       = message.Substring(23, 1);
      string reasonCode      = message.Substring(24, 3);
      string dtcTimestamp    = message.Substring(27, 6);
      string secId           = message.Substring(33, 9);
      string contraDtcId     = message.Substring(42, 4);
      string quantity        = message.Substring(46, 9);
      string amount          = message.Substring(55, 10) + "." + message.Substring(65, 2);
      string contractType    = message.Substring(67, 1);
      string contractId      = message.Substring(68, 9);
      string otherBook       = message.Substring(77, 4);
      string poolCode        = message.Substring(81, 1);
      string sequenceRelated = message.Substring(82, 6);
     
      DatagramDebitWrite(
        clientId, 
        sequence, 
        pendMadeFlag, 
        matchFlag, 
        reasonCode, 
        dtcTimestamp, 
        secId, 
        contraDtcId, 
        quantity, 
        amount, 
        contractType, 
        contractId, 
        otherBook, 
        poolCode, 
        sequenceRelated);      
    
      return processStatusEventArgsItem;
    }    
    
    private void DatagramDebitWrite(
      string clientId,
      string sequence,
      string pendMadeFlag,
      string matchFlag,
      string reasonCode,
      string dtcTimestamp,
      string secId,
      string contraDtcId,
      string quantity,
      string amount,
      string contractType,
      string contractId,
      string otherBook,
      string poolCode,
      string sequenceRelated)
    {
      string actType = "";
      string processId = Standard.ProcessId();

			string reasonCodes = KeyValue.Get("LoanetDebitRecallReasonCodes", "RTN;020;PTL;270;", dbCnStr);

      SqlConnection dbCn = null;
      SqlCommand dbCmd = null;
      
		try
		{
			dbCn = new SqlConnection(dbCnStr);
			dbCmd = new SqlCommand("spLoanetDatagramDebitUpdate", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;
   
			SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 4);
			paramClientId.Value = clientId;

			SqlParameter paramSequenece = dbCmd.Parameters.Add("@Sequence", SqlDbType.Int);
			paramSequenece.Value = sequence;

			SqlParameter paramPendMadeFlag = dbCmd.Parameters.Add("@PendMadeFlag", SqlDbType.Char, 1);
			paramPendMadeFlag.Value = pendMadeFlag;
        
			SqlParameter paramMatchFlag = dbCmd.Parameters.Add("@MatchFlag", SqlDbType.Char, 1);
			paramMatchFlag.Value = matchFlag;
        
			SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 3);
			paramReasonCode.Value = reasonCode;
        
			SqlParameter paramDtcTimestamp = dbCmd.Parameters.Add("@DtcTimestamp", SqlDbType.Char, 6);
			paramDtcTimestamp.Value = dtcTimestamp;
        
			SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
			paramSecId.Value = secId;
        
			SqlParameter paramContraDtcId = dbCmd.Parameters.Add("@ContraDtcId", SqlDbType.VarChar, 4);
			paramContraDtcId.Value = contraDtcId;
        
			SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
			paramQuantity.Value = quantity;
        
			SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);
			paramAmount.Value = amount;
        
			if (!contractType.Equals(""))
			{
				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
				paramContractType.Value = contractType;
			}

			if (!contractId.Equals(""))
			{
				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.Char, 9);
				paramContractId.Value = contractId;
			}

			SqlParameter paramOtherBook = dbCmd.Parameters.Add("@OtherBook", SqlDbType.VarChar, 4);
			paramOtherBook.Value = otherBook;
        
			SqlParameter paramPoolCode = dbCmd.Parameters.Add("@PoolCode", SqlDbType.VarChar, 1);
			paramPoolCode.Value = poolCode;
        
			SqlParameter paramSequenceRelated = dbCmd.Parameters.Add("@SequenceRelated", SqlDbType.Int);
			paramSequenceRelated.Value = sequenceRelated;

			SqlParameter paramAct = dbCmd.Parameters.Add("@Act", SqlDbType.VarChar, 255);
			paramAct.Direction = ParameterDirection.Output;

			SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);
			paramProcessId.Value = "C" + processId.Remove(0, 1);

			dbCn.Open();
			dbCmd.ExecuteNonQuery();
         
			if ((pendMadeFlag.Equals("M") || pendMadeFlag.Equals("C")) && matchFlag.Equals("M") && (reasonCodes.IndexOf(reasonCode + ";") > 0)) 
			{
				actType = "DBT";
			}
			else if ((pendMadeFlag.Equals("M") || pendMadeFlag.Equals("C")) && matchFlag.Equals("U"))
			{
				actType = "DBU";
			}
			else
			{
				actType = "DBA";
			}	
        
			processStatusEventArgsItem = new ProcessStatusEventArgs(
				processId,
				"L",
				actType,
				paramAct.Value.ToString(),
				"",
				"ADMIN",
				false,
				clientId,
				contractId,
				contractType,
				contraDtcId,
				secId,
				"",
				quantity,
				amount,
				"",
				"",
				quantity);      
		}
		catch (Exception error)
		{
			Log.Write(error.Message + " [Debit_Data_Datagram.DatagramDebitWrite]", 3);       
			throw;
		}
		finally
		{
			dbCn.Close();
		}
    }
	}
}
