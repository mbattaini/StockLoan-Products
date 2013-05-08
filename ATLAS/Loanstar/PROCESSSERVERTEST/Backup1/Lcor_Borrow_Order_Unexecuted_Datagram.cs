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
	public class Lcor_Borrow_Order_Unexecuted_Datagram
	{		
		private string dbCnStr;
		private ProcessStatusEventArgs processStatusEventArgsItem = null;

		public Lcor_Borrow_Order_Unexecuted_Datagram(string dbCnStr)
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
			string	unexecutedQuantity = message.Substring(50, 9);
			string	reason = message.Substring(59, 1);
			string	comments = message.Substring(60, 20);
			
			DatagramLcorBorrowUnexecutedWrite(
				clientId,
				serialId,
				contraClientId,
				secId,
				originalQuantity,
				unexecutedQuantity,
				reason,
				comments);

			return processStatusEventArgsItem;
		}

		private void DatagramLcorBorrowUnexecutedWrite(
			string	clientId,
			string	serialId,
			string	contraClientId,
			string	secId,
			string	orignalQuantity,
			string	unexecutedQuantity,
			string	reason,
			string	commnets)
		{
			string reasonLong = "";
			string act = "";		

			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
      
			try
			{
				switch (reason)
				{
					case "A":
						reasonLong = "Not Available";
						break;
					
					case "T":
						reasonLong = "Timeout";
						break;
					
					case "R":
						reasonLong = "Rate less than minimum";
						break;
					
					case "Q":
						reasonLong = "Quantity less than minimum";
						break;
					
					case "P":
						reasonLong = "Price greater than maximum";
						break;
					
					case "B":
						reasonLong = "No Box Rate";
						break;
					
					case "O":
						reasonLong = "OCC Delivery Error";
						break;
					
					case "F":
						reasonLong = "High Div F/T";
						break;
					
					default:
						reasonLong = "Unknown Error";
						break;
				}
				
				
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetDatagramLcoBorrowOrderUnexecuted", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
   
				SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.Char, 4);
				paramClientId.Value = clientId;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
				paramSecId.Value = secId;
        
				SqlParameter paramContraClientId = dbCmd.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
				paramContraClientId.Value = contraClientId;
        
				SqlParameter paramReason = dbCmd.Parameters.Add("@Reason", SqlDbType.VarChar, 50);
				paramReason.Value = reasonLong;
				
				SqlParameter paramSerialId = dbCmd.Parameters.Add("@SerialId", SqlDbType.Char, 6);
				paramSerialId.Value = serialId; 
        

				dbCn.Open();
				dbCmd.ExecuteNonQuery();

				act = "Borrow of " + unexecutedQuantity + "units  of + " + orignalQuantity + " units for " + secId + " failed. Reason: " + reasonLong;
				
				processStatusEventArgsItem = new ProcessStatusEventArgs(
					Standard.ProcessId(),
					"L",
					"BOU",
					act,
					"",
					"ADMIN",
					false,
					clientId,
					"",
					"",
					contraClientId,
					secId,
					"",
					unexecutedQuantity,
					"",
					"",
					"",
					"");      
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [Lcor_Borrow_Order_Unexecuted_Datagram.DatagramLcorBorrowUnexecutedWrite]", 3);       
				throw;
			}
			finally
			{
				dbCn.Close();
			}	
		}
	}
}
