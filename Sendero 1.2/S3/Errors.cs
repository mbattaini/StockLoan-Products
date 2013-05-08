using System;
using System.IO;
using System.Xml;
using System.Data;
using Anetics.Common;

namespace Anetics.S3
{
	public class Errors
	{
		private string	REQUEST_ID_INVALID = "Invalid request id";
		private string	REQUEST_TYPE_INVALID = "Invalid request type";
		private string	SECURITY_ID_INVALID = "Invalid security id";
		private string	SECURITY_TYPE_INVALID = "Invalid security type";
		private string	REQUESTED_QUANTITY_INVALID = "Invalid quantity";
		private string	REQUESTED_MINIMUM_INVALID = "Invalid minimum quantity";
		private string	REQUESTED_OVERRIDE_INVALID = "Invalid override quantity";
		private string	MAXPROCESSING_TIME_ELAPSED = "MaxProcessing time elapsed";
		private string	SENDTO_MEMOSEG_INVALID = "Invalid send to memo seg";
		private string	REQUESTED_SECURITY_UNAVAILABLE = "Security unavailable";
		private string	ACCOUNT_ID_INVALID = "Invalid account id";
		private string	ERROR_OCCURRED = "Error occurred*";
		private string	S3_DOWN = "S3 down";
		private string  EVENING_MAINTENANCE = "Evening maintenance";
		private string	MPD_UNAVAILABLE = "mpd unavailable";
		private string	CRITICAL_ERROR = "Critical error";
		private string  ERROR_SEPERATOR = "; ";				
		
		public Errors()
		{		
		}

		public string Lookup(string code)
		{
			string error;

			switch(code.Trim())
			{
				case "10000":
					error = REQUEST_ID_INVALID;
					break;

				case "11000": 
					error = REQUEST_TYPE_INVALID;
					break;

				case "20000":
					error = SECURITY_ID_INVALID;
					break;

				case "21000":
					error = SECURITY_TYPE_INVALID;
					break;

				case "30000":
					error = REQUESTED_QUANTITY_INVALID;
					break;

				case "31000":
					error = REQUESTED_MINIMUM_INVALID;
					break;

				case "32000":
					error = REQUESTED_OVERRIDE_INVALID;
					break;

				case "33000":
					error = MAXPROCESSING_TIME_ELAPSED;
					break;

				case "40000":
					error = SENDTO_MEMOSEG_INVALID;
					break;

				case "50000":
					error = MAXPROCESSING_TIME_ELAPSED;
					break;

				case "51000":
					error = REQUESTED_SECURITY_UNAVAILABLE;
					break;

				case "60000":
					error = ACCOUNT_ID_INVALID;
					break;

				case "90000":
					error = ERROR_OCCURRED;
					break;
  
				case "90100":
					error = S3_DOWN;
					break;

				case "90200":
					error = EVENING_MAINTENANCE;
					break;

				case "90300":
					error = MPD_UNAVAILABLE;
					break;

				case "99999":
					error = CRITICAL_ERROR;
					break;
			
				default:
					error = "Unkonwn error: " + code;
					break;
			}

			return error + ERROR_SEPERATOR;
		}
	}
}
