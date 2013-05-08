using System;
using System.IO;
using System.Xml;
using Anetics.Common;
using Anetics.SmartSeg;

namespace Anetics.S3
{	
	public class S3_Margin_Positon_Delete
	{
		private string xmlMessage;

		public string Message 
		{
			get
			{
				return xmlMessage;
			}
		}
		
		
		public ProcessStatusEventArgs Request(
			string		requestId,
			string		secId,
			string		secIdType,
			string		accountId)
		{	
			xmlMessage = "<MarginPositionRequest>" ;
			xmlMessage += "<RequestId>" + requestId + "</RequestId>";
			xmlMessage += "<SecurityId>" + secId + "</SecurityId>";
			xmlMessage += "<SecurityTypeId>" + secIdType + "</SecurityTypeId>";
			xmlMessage += "<AccountId>" + accountId + "</AccountId>";
			xmlMessage += "</MarginPositionRequest>";

			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				requestId,
				"S",
				"MP",
				"Margin Position Delete [" + accountId +"]",
				DateTime.UtcNow.ToString(),
				"ADMIN",
				false,
				"",
				"",
				"",
				"",
				secId,
				"",
				"",
				"",
				"Pending",
				"",
				"");
									
			return processStatusEventArgsItem;
		}

		public ProcessStatusEventArgs Response(string XMLMessage)
		{
			string ResponseString = null;
			string delimiter = ",";
			string xmlElement = null;
			string requestId = "";
			string error = "";

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
							case "ResponseId":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "RequestId":
								requestId = xmlTextReader.Value;
								break;
							case "RequestRejected":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "FreeQuantity":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "InsegQuantity":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "PSRQuantity":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "ExcessQuantity":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "SecurityId":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "SecurityTypeId":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "AccountId":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "Warning":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "RejectedReason":
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "Error":
								error = xmlTextReader.Value;
								break;
						}
					
							ResponseString = ResponseString + delimiter;
							break;
					}
				}
				xmlTextReader.Close();
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message + " [S3.S3_Intraday_Datagram]", 3);
				ResponseString = null;
			}
			
			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				requestId,
				"S",
				"SR",
				"",
				"",
				"ADMIN",
				((error.Equals(""))? false:true),
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				((error.Equals(""))? "OK":error),
				DateTime.UtcNow.ToString(),
				"ADMIN");
			
			
			
			
			return processStatusEventArgsItem;
		}

	}
}
