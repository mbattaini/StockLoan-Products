using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;
using Anetics.SmartSeg;

namespace Anetics.S3
{	

	public class S3_SegEntry_Datagram
	{

	private string dbCnStr = "";

	public S3_SegEntry_Datagram (string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}
		
		public ProcessStatusEventArgs Response(string XMLMessage)
		{									
			string xmlElement = "";
			string accountNumber = "";
			string secId = "";
			string quantity = "";
			string indicator = "";
			string indicatorDescription = "";
			string segEntryId = "";
			string timeOfDay = "";
			string timeOfDayDescription = "";

			try
			{

				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(XMLMessage));

				while (xmlTextReader.Read())
				{
					switch (xmlTextReader.NodeType)
					{
						case XmlNodeType.Element:
							xmlElement = xmlTextReader.Name;
							break;

						case XmlNodeType.Text:
						switch (xmlElement)
						{	
							case "SegEntryId":
								segEntryId = xmlTextReader.Value.ToString();
								break;
							case "SecurityId":
								secId = xmlTextReader.Value.ToString();
								break;
							case "AccountId":
								accountNumber = xmlTextReader.Value.ToString();
								break;
							case "Quantity":
								quantity =  xmlTextReader.Value.ToString();
								break;
							
							case "TransactionDate":								
							case "TransactionTime":								
								break;

							case "TimeOfDay":
								timeOfDay = xmlTextReader.Value.ToString();
								break;
								
							case "LockupOrReleaseIndicator":
								indicator =  xmlTextReader.Value.ToString();
								break;
						}
							break;
					}
				}
				
				xmlTextReader.Close();

			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [S3.S3_SegEntry_Datagram]", 3);				
			}


			switch (timeOfDay)
			{
				case "S":
					timeOfDayDescription = "Start of Day";
					break;

				case "I":
					timeOfDayDescription = "Intraday";
					break;
					
				case "E":
					timeOfDayDescription = "End of Day";
					break;

				default:
					timeOfDayDescription = "";
					break;
			}
			
			switch (indicator)
			{
				case "I":
					indicatorDescription = "Increase in requirement";
					break;
					
				case "D":
					indicatorDescription = "Decrease in requirement";
					break;

				default:
					indicatorDescription = "";
					break;
			}
							
			
			MemoSegEntrySet(
				Standard.ProcessId("M"),
				secId,
				quantity,
				indicator,
				false,
				false);
			
			ProcessStatusEventArgs processStatusEventArgs  = new ProcessStatusEventArgs(
				Standard.ProcessId(),
				"S",
				"SED",
				timeOfDayDescription + " " + indicatorDescription,
				"",
				"ADMIN",
				false,
				"",
				"",
				"",
				"",
				secId,
				"",
				quantity,
				"",
				"",
				"",
				accountNumber + "|" + indicator);
				
			
			
			return processStatusEventArgs;
		}

		private void MemoSegEntrySet(string processId, string secId, string quantity, string indicator, bool isRequested, bool isProcessed)
		{						
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			SqlCommand dbCmd  = null;

			try
			{
				switch(indicator)
				{
					case "I":						
						break;
					
					case "D":
						quantity = "-" + quantity;
						break;

					default:
						quantity = "0";
						break;
				}

				dbCmd = new SqlCommand("spMemoSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processId;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);				
				paramQuantity.Value = quantity;

				SqlParameter paramIsRequested = dbCmd.Parameters.Add("@IsRequested", SqlDbType.Bit);
				paramIsRequested.Value = isRequested;

				SqlParameter paramIsProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit);
				paramIsProcessed.Value = isProcessed;
		
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
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
