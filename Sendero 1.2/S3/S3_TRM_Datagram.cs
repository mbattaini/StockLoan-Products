using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;
using Anetics.SmartSeg;

namespace Anetics.S3
{
	/// <summary>
	/// Summary description for S3_TRM_Request_Datagram.
	/// </summary>
	/// 		
	public class S3_TRM_Datagram
	{
		public ProcessStatusEventArgs Reponse(string dbCnStr, string XMLMessage, bool isLockup)
		{
			string ResponseString = "";
			string excessQuantity = "";
			string psrQuantity = "";
			string substitutionQuantity = "";
			string marginLuAvailable = "";
			string secId = "";
			string needId = "";

			try
			{
				string delimiter = ",";
				string xmlElement = null;
				

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
							case "RequestId":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "NeedId":
								needId = xmlTextReader.Value;
								break;
							case "ExcessQuantity":
								excessQuantity = (long.Parse(xmlTextReader.Value.ToString()) * ((isLockup)? 1:-1)).ToString();
								break;
							case "PSRQuantity":
								psrQuantity = (long.Parse(xmlTextReader.Value.ToString())* ((isLockup)? 1:-1)).ToString();
								break;
							case "SubstitutionQuantity":
								substitutionQuantity = (long.Parse(xmlTextReader.Value.ToString()) * ((isLockup)? 1:-1)).ToString();
								break;
							case "MarginLUAvailable":
								marginLuAvailable = (long.Parse(xmlTextReader.Value.ToString())).ToString();
								break;
							case "SecurityId":
								secId = xmlTextReader.Value;
								break;
							case "CUSIP":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
						}
							ResponseString = ResponseString + delimiter;
							break;
					}
				}
				xmlTextReader.Close();
			}
			catch (Exception error)
			{			
				Log.Write(error.Message + " [S3.TRM_Response]", 3);
				ResponseString = null;
			}
			
			ExternalInventorySet(dbCnStr, secId, excessQuantity, psrQuantity, substitutionQuantity);
						
			ProcessStatusEventArgs processStatusEventArgs  = new ProcessStatusEventArgs(
				Standard.ProcessId(),
				"S",
				"TRM",
				(isLockup)?"Lockup":"Release",
				"",
				"ADMIN",
				false,
				"",
				needId,
				"",
				"",
				secId,
				"",
				"",
				"",
				"",
				"",
				excessQuantity + "|" + psrQuantity + "|" +  substitutionQuantity);
				
			
			
			return processStatusEventArgs;
		}

		public void ExternalInventorySet(string dbCnStr, string secId, string excessQuantity, string psrQuantity, string marginLuAvailable)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			
			try
			{
				dbCn = new SqlConnection(dbCnStr);

				dbCmd = new SqlCommand("spInventorySubstitutionSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
			
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = Standard.ProcessId("S");

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 15);
				paramSecId.Value = secId;

				SqlParameter paramPsrQuantity = dbCmd.Parameters.Add("@PsrQuantity", SqlDbType.BigInt);
				paramPsrQuantity.Value = psrQuantity;

				SqlParameter paramPotentiallyAvailableQuantity = dbCmd.Parameters.Add("@PotentiallyAvailableQuantity", SqlDbType.BigInt);
				paramPotentiallyAvailableQuantity.Value = marginLuAvailable;

				SqlParameter paramReservedExcessQuantity  = dbCmd.Parameters.Add("@ReservedExcessQuantity ", SqlDbType.BigInt);
				paramReservedExcessQuantity.Value = excessQuantity;			
				
				SqlParameter paramIsIncremental  = dbCmd.Parameters.Add("@IsIncremental ", SqlDbType.BigInt);
				paramIsIncremental.Value = 1;

				dbCn.Open();
					
				dbCmd.ExecuteNonQuery();
		
				dbCn.Close();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [S3_TRM_Datagram.ExternalInventorySet]", 3);
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
