// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class Lcor_Borrow_Order_Executed_Datagram
	{
		private string dbCnStr;
		private ProcessStatusEventArgs processStatusEventArgsItem = null;

		public Lcor_Borrow_Order_Executed_Datagram(string dbCnStr)
		{
			 this.dbCnStr = dbCnStr;
		}

		public ProcessStatusEventArgs Activity(string message)
		{ 
			string	clientId = message.Substring(12, 4);
			string	serialId = message.Substring(22, 6);
			string	contraClientId = message.Substring(28, 4);
			string	secId = message.Substring(32, 9);
			string	originalQuantity = message.Substring(41, 9);
			string	executedQuantity = message.Substring(50, 9);
			string	executedAmount = message.Substring(59, 10) + "." + message.Substring(69, 2);
			string	collateralCode = message.Substring(71, 1);
			string	rebateRate = message.Substring(72, 5);
			string	rebateRateCode = message.Substring(77, 1);
			string	incomeTracked = message.Substring(78, 1);
			string	divRate = message.Substring(79, 6);
			string	comments = message.Substring(85, 20);
			string	contractId = message.Substring(106, 9);

			DatagramLcorBorrowOrderExecutedWrite(
				clientId,
				serialId,
				contraClientId,
				secId,
				originalQuantity,
				executedQuantity,
				executedAmount,
				collateralCode,
				rebateRate,
				rebateRateCode,
				incomeTracked,	
				divRate,
				comments,
				contractId);

				return processStatusEventArgsItem;
		}

		private void DatagramLcorBorrowOrderExecutedWrite(
			string	clientId,
			string	serialId,
			string	contraClientId,
			string	secId,
			string	orignalQuantity,
			string	executedQuantity,
			string	executedAmount,
			string	collateralCode,
			string	rebateRate,
			string	rebateRateCode,
			string	incomeTracked,	
			string	divRate,
			string	commnets,
			string	contractId)			
		{
			string act = "";		

			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
      
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetDatagramLcoBorrowOrderExecuted", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
   
				SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.Char, 4);
				paramClientId.Value = clientId;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
				paramSecId.Value = secId;
        
				SqlParameter paramContraClientId = dbCmd.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
				paramContraClientId.Value = contraClientId;
        
				SqlParameter paramQuantity = dbCmd.Parameters.Add("@ExecutedQuantity", SqlDbType.BigInt);
				paramQuantity.Value = executedQuantity;       
				        
				SqlParameter paramSerialId = dbCmd.Parameters.Add("@SerialId", SqlDbType.Char, 6);
				paramSerialId.Value = serialId; 

				dbCn.Open();
				dbCmd.ExecuteNonQuery();

        act = "Borrow of " + executedQuantity + "units  of + " + orignalQuantity + " units for " + secId + " executed.";
				
				processStatusEventArgsItem = new ProcessStatusEventArgs(
					Standard.ProcessId(),
					"L",
					"BOE",
					act,
					"",
					"ADMIN",
					false,
					clientId,
					contractId,
					"",
					contraClientId,
					secId,
					"",
					executedQuantity,
					"",
					"",
					"",
					"");      
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [Lcor_Borrow_Order_Executed_Datagram.DatagramLcorBorrowOrderExecutedWrite]", 3);       
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}		
	}
}
