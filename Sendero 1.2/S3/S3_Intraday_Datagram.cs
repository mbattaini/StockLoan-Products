using System;
using System.IO;
using System.Xml;
using Anetics.Common;

namespace Anetics.S3
{
	/// <summary>
	/// Summary description for S3_Intraday_Datagram.
	/// </summary>
	/// 		
	public class S3_Intraday_Datagram
	{
		public string Request(
			long		RequestId,
			string		SecurityId,
			string		SecurityTypeId,
			string		AccountId
			)
		{	
			string XMLMessage = "<MarginPositionRequest>" ;
			XMLMessage += "<RequestId>" + RequestId + "</RequestId>";
			XMLMessage += "<SecurityId>" + SecurityId + "</SecurityId>";
			XMLMessage += "<SecurityTypeId>" + SecurityTypeId + "</SecurityTypeId>";
			XMLMessage += "<AccountId>" + AccountId + "</AccountId>";
			XMLMessage += "</MarginPositionRequest>";

			return XMLMessage;
		}

		public string Response(string XMLMessage)
		{
			string ResponseString = null;
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
								ResponseString = ResponseString + xmlTextReader.Value;
								break;
							case "RequestId":
								ResponseString = ResponseString + xmlTextReader.Value;
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
								ResponseString = ResponseString + xmlTextReader.Value;
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
			return ResponseString;
		}

	}
}
