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
	public class BankLoan_Pledge_Release_Datagram
	{
		private string dbCnStr;
		private ProcessStatusEventArgs processStatusEventArgs = null;

		public BankLoan_Pledge_Release_Datagram(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}

		public ProcessStatusEventArgs Activity(string message)
		{
			string clientId               = message.Substring(12, 4);			
			string pendMadeFlag						= message.Substring(22, 1);
			string contraClientId					= message.Substring(59, 4);
			string loanDate               = message.Substring(63, 6);
			string secId                  = message.Substring(29, 9);      
			string quantity               = message.Substring(38, 9);												
			string dtcActivityType        = message.Substring(70, 3);
			string dtcPendReason          = message.Substring(73, 1);
			string dtcInputSequence       = message.Substring(74, 5);			
			string comment                = message.Substring(79, 56);
    
			DatagramBankLoanUpdate(
				clientId,
				pendMadeFlag,
				contraClientId,
				loanDate,
				secId,       
				quantity,																
				dtcActivityType,
				dtcPendReason,   
				dtcInputSequence,
				comment);
          
			return processStatusEventArgs;
		}
    
    
		private void  DatagramBankLoanUpdate(
			string clientId,			
			string pendMadeFlag,
			string contraClientId,
			string loanDate,			
			string secId,       
			string quantity,						
			string dtcActivityType,
			string dtcPendReason,   
			string dtcInputSequence,
			string comment)
		{
			string activityType = "";
			string act = "";

			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
		
			switch (dtcActivityType.Trim())
			{
				case "051":
					activityType = "F";
					dtcActivityType = "P";
					
					try
					{
						act = "Free Pledge of " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate;
					}
					catch
					{
						act = "Free Pledge of " + secId + " for " + quantity + " for loan date " + loanDate;
					}
					break;

				case "052":
					activityType = "V";
					dtcActivityType = "P";

					try
					{
						act = "Valued Pledge " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate;
					}
					catch
					{
						act = "Valued Pledge " + secId + " for " + quantity + " for loan date " + loanDate;
					}
					break;

				case "055":
					activityType = "V";
					dtcActivityType = "R";
					
					try
					{
						act = "Valued Release " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate;
					}
					catch
					{
						act = "Valued Release " + secId + " for " + quantity + " for loan date " + loanDate;
					}
					break;
				
				case "056":
					activityType = "F";
					dtcActivityType = "R";

					try
					{
						act = "Free Release " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate;
					}
					catch
					{
						act = "Free Release " + secId + " for " + quantity + " for loan date " + loanDate;
					}
					break;

				case "01":				
					activityType = "F";
					pendMadeFlag = "F";
					dtcActivityType = "P";

					try
					{
						act = "Free Pledge of " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate + " failed.";
					}
					catch
					{
						act = "Free Pledge of " + secId + " for " + quantity + " for loan date " + loanDate + " failed.";
					}
					break;

				case "02":				
					activityType = "V";
					pendMadeFlag = "F";
					dtcActivityType = "P";
					
					try
					{
						act = "Valued Pledge of " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate + " failed.";
					}
					catch
					{
						act = "Valued Pledge of " + secId + " for " + quantity + " for loan date " + loanDate + " failed.";
					}
					break;
				
				case "03":										
					activityType = "F";
					pendMadeFlag = "M";
					dtcActivityType = "P";
					
					try
					{
						act = "Free Release of " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate + " failed.";
					}
					catch
					{
						act = "Free Release of " + secId + " for " + quantity + " for loan date " + loanDate + " failed.";
					}
					break;

				case "04":										
					activityType = "V";
					pendMadeFlag = "M";
					dtcActivityType = "P";
					
					try
					{
						act = "Valued Release of " + secId + " for " + long.Parse(quantity).ToString("#,##0") + " for loan date " + loanDate + " failed.";
					}
					catch
					{
						act = "Valued Release of " + secId + " for " + quantity + " for loan date " + loanDate + " failed.";
					}
					break;

				default:										
					activityType = "E";
					pendMadeFlag = "E";					
					dtcActivityType = "E";
					
					act = "Unrecongized dtc activity type: " + dtcActivityType;
					break;
			}
			
			if (dtcPendReason.Equals("C"))
			{
				dtcPendReason = "M";
			}

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetDatagramBankLoanUpdate", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.Char, 4);
				paramClientId.Value = clientId;

				if (!contraClientId.Equals("0000"))
				{
					SqlParameter paramContraClientId = dbCmd.Parameters.Add("@ContraClientId", SqlDbType.Char, 4);
					paramContraClientId.Value = contraClientId;
				}
			
				SqlParameter paramLoanDate = dbCmd.Parameters.Add("@LoanDate", SqlDbType.DateTime);
				paramLoanDate.Value = DateTime.ParseExact(loanDate, "MMddyy", null).ToString();
				
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
				paramSecId.Value = secId;
            
				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;
        				
				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 2);
				paramStatus.Value = (dtcActivityType + pendMadeFlag);

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);
				paramProcessId.Direction = ParameterDirection.InputOutput;
				paramProcessId.Value	 = Standard.ProcessId();

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
      
				processStatusEventArgs = new ProcessStatusEventArgs(
					Standard.ProcessId(),
					"L",
					"BLD",
					act,
					"",
					"ADMIN",
					false,
					clientId,
					"",
					"",
					"",
					secId,
					"",
					quantity,
					"",
					"[" + dtcActivityType + pendMadeFlag + "]",
					"",
					paramProcessId.Value.ToString());
			}
			catch(Exception e)
			{
				Log.Write(e.Message + " [BankLoan_Pledge_Release_Datagram.DatagramBankLoanUpdate]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
	}
}
