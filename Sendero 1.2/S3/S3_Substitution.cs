using System;
using System.IO;
using System.Xml;
using Anetics.Common;
using Anetics.SmartSeg;

namespace Anetics.S3
{		
	public class S3_Substitution
	{		
		string xmlMessage = "";

		public string Message
		{
			get
			{
				return xmlMessage;
			}
		}
		
		public ProcessStatusEventArgs Request(	
			string		requestId,
			string		requestType,
			string		secId,
			string		secIdType,
			string		quantity,
			string		minQuantity,
			string		overrideRate,
			string		maxProcessingTime,
			string		sendDTCMemoSeg
			)
		{	
			xmlMessage = "<SubstitutionRequest>" ;
			xmlMessage += "<RequestId>" + requestId + "</RequestId>";
			xmlMessage += "<RequestType>" + requestType + "</RequestType>";
			xmlMessage += "<SecurityId>" + secId + "</SecurityId>";
			xmlMessage += "<SecurityIdType>" + secIdType + "</SecurityIdType>";
			xmlMessage += "<Quantity>" + quantity + "</Quantity>";
			xmlMessage += "<MinQuantity>" + minQuantity + "</MinQuantity>";
			xmlMessage += "<OverrideRate>" + overrideRate + "</OverrideRate>";
			xmlMessage += "<MaxProcessingTime>" + maxProcessingTime + "</MaxProcessingTime>";
			xmlMessage += "<SendDTCMemoSeg>" + sendDTCMemoSeg + "</SendDTCMemoSeg>";
			xmlMessage += "</SubstitutionRequest>";

			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				requestId,
				"S",
				"SR",
				"Substitution request (" + requestType+")",
				DateTime.UtcNow.ToString(),
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
				"Pending",
				"",
				"");

			return processStatusEventArgsItem;
		}

		public ProcessStatusEventArgs Response(string XMLMessage)
		{
			Errors error = new Errors();


			string ResponseString = null;
			string responseId = "", 
				requestId = "", 
				requestRejected = "", 
				needId = "", 
				excessQuantity = "", 
				psrQuantity = "", 
				substitutionQuantity = "", 
				marginLUAvailable = "", 
				securityId = "", 
				securityIdType = "", 
				warning = "", 
				rejectedReason = "", 
				errors = "";
				
			
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
							case "ResponseId":
								responseId  = xmlTextReader.Value;
								break;
							case "RequestId":
								requestId = xmlTextReader.Value;
								break;
							case "RequestRejected":
								requestRejected = xmlTextReader.Value;
								break;
							case "NeedId":
								needId  = xmlTextReader.Value;
								break;
							case "ExcessQuantity":
								excessQuantity  = xmlTextReader.Value;
								break;
							case "PSRQuantity":
								psrQuantity  = xmlTextReader.Value;
								break;
							case "SubstitutionQuantity":
								substitutionQuantity  = xmlTextReader.Value;
								break;
							case "MarginLUAvailable":
								marginLUAvailable  = xmlTextReader.Value;
								break;
							case "SecurityId":
								securityId  = xmlTextReader.Value;
								break;
							case "SecurityIdType":
								securityIdType  = xmlTextReader.Value;
								break;
							case "Warning":
								warning   = xmlTextReader.Value;
								break;
							case "RejectedReason":
								rejectedReason  = xmlTextReader.Value;
								break;
							case "Error":
								errors  += error.Lookup(xmlTextReader.Value);
								break;
						}
							ResponseString = ResponseString + delimiter;
							break;
					}
				}
				xmlTextReader.Close();
			}
			catch (Exception errorItem)
			{
				Log.Write(errorItem.Message + " [S3.S3_Substitution]", 3);
				ResponseString = null;
			}
			
			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				requestId,
				"S",
				"SR",
				"",
				"",
				"ADMIN",
				((errors.Equals(""))? false:true),
				"",
				needId,
				"",
				"",
				"",
				"",
				"",
				"",
				((errors.Equals(""))? "OK":errors),
				DateTime.UtcNow.ToString(),
				excessQuantity + "|" + psrQuantity + "|" + substitutionQuantity);
		
			return processStatusEventArgsItem;
		}
	}
}
