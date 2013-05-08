// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Data;

namespace Anetics.Medalist
{
	public delegate void SubstitutionActivityEventHandler(SubstitutionActivityEventArgs e);

	public interface ISubstitution
	{    	
		event SubstitutionActivityEventHandler SubstitutionActivityEvent;

		DataSet SubstitutionGet(
			string	processId,
			string	bookGroup,
			string	actUserId,
			short		utcOffset);
		
		string SubstitutionSet(
			string	processId,
			string	bookGroup,			
			string	secId,
			string	requestedQuantity,
			string	quantity,
			string  overrideRate,
			string  excessQuantity,
			string	psrQuantity,
			string	substitutionQuantity,
			string	type,
			string	status,
			string	actUserId);

		DataSet SubstitutionInventoryDataGet(string effectDate);		
		DataSet SubstitutionInventoryDataGet(string effectDate, string secId);			
		
		DataSet SubstitutionUpdatedDeficitExcessDataGet(string effectDate);

		DataSet SubstitutionSegEntriesDataGet(string bizDate, int utcOffset);
		DataSet SubstitutionMemoSegEntriesDataGet(string bizDate, int utcOffset);

		void SubstitutionSegEntryFlagSet(string processid, bool isrequested, bool isprocessed);		
	}


	[Serializable]
	public class SubstitutionActivityEventArgs : EventArgs
	{
		private	string bizDate;
		private string bookGroup;
		private string processId;				
		private string secId;
		private string symbol;
		private string quantity;
		private string requestedQuantity;
		private string overrideRate;
		private string excessQuantity;
		private string psrQuantity;	
		private string substitutionQuantity;
		private string totalQuantity;
		private string status;
		private string actUserId;
		private string type;
		private string actTime;
		private short  utcOffset = 0;	
  
		public SubstitutionActivityEventArgs(
			string	bizDate,
			string	processId,
			string	bookGroup,			
			string	secId,
			string	symbol,
			string	requestedQuantity,
			string	quantity,
			string  overrideRate,
			string	excessQuantity,
			string	psrQuantity,
			string	substitutionQuantity,
			string	totalQuantity,
			string	type,
			string	status,
			string	actUserId,
			string	actTime)
		{
			this.bizDate							= bizDate;
			this.processId						= processId;
			this.bookGroup						=	bookGroup;
			this.secId								= secId;
			this.symbol								= symbol;
			this.quantity							= quantity;
			this.requestedQuantity		= requestedQuantity;
			this.overrideRate					=	overrideRate;
			this.excessQuantity				= excessQuantity;
			this.psrQuantity					= psrQuantity;
			this.substitutionQuantity = substitutionQuantity;
			this.totalQuantity				= totalQuantity;
			this.type									= type;
			this.status								= status;
			this.actUserId						=	actUserId;
			this.actTime							= actTime;
		}

		public string BizDate
		{
			get
			{
				return bizDate;
			}
		}

		public string BookGroup
		{
			get
			{
				return bookGroup;
			}
		}
		
		public string ProcessId
		{
			get
			{
				return processId;
			}
		}
		
		public string SecId
		{
			get
			{
				return secId;
			}
		}

		
		public string Symbol
		{
			get
			{
				return symbol;
			}
		}

		public string RequestedQuantity
		{
			get
			{
				return requestedQuantity;
			}
		}

		public string Quantity
		{
			get
			{
				return quantity;
			}
		}

		public string ExcessQuantity
		{
			get
			{
				return excessQuantity;
			}
		}
		
		public string PsrQuantity
		{
			get
			{
				return psrQuantity;
			}
		}

		public string SubstitutionQuantity
		{
			get
			{
				return substitutionQuantity;
			}
		}
		
		public string TotalQuantity
		{
			get
			{
				return totalQuantity;
			}
		}

		public string OverrideRate
		{
			get
			{				
				return overrideRate;				
			}
		}

		public string Type
		{
			get
			{
				return type;
			}

			set
			{
				type = value;
			}
		}

		public string Status
		{
			get
			{
				return status;
			}

			set
			{
				status = value;
			}
		}

		public string ActUserId
		{
			get
			{
				return actUserId;
			}
		}
    
		public string ActTime
		{
			get
			{
				try
				{
					return DateTime.Parse(actTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");
				}
				catch
				{
					return actTime;        
				}
			}
		}
    
		public short UtcOffset
		{
			set
			{
				utcOffset = value;
			}
		}	

		public object [] Values
		{
			get
			{
				object[] rowValues = new object[16];
          
				rowValues[0]	= BizDate;
				rowValues[1]	= ProcessId;
				rowValues[2]	= BookGroup;
				rowValues[3]	= SecId;		
				rowValues[4]	= Symbol;
				rowValues[5]	= RequestedQuantity;
				rowValues[6]	= Quantity;
				
				if (OverrideRate.Equals(""))
				{
					rowValues[7] = DBNull.Value;
				}
				else
				{
					rowValues[7]	= OverrideRate;
				}

				if (ExcessQuantity.Equals(""))
				{
					rowValues[8]	= DBNull.Value;
				}
				else
				{
					rowValues[8]	= ExcessQuantity;
				}

				if (PsrQuantity.Equals(""))
				{
					rowValues[9]	= DBNull.Value;
				}
				else
				{
					rowValues[9]	= PsrQuantity;
				}
				
				if (SubstitutionQuantity.Equals(""))
				{
					rowValues[10]	= DBNull.Value;
				}
				else
				{
					rowValues[10]	= SubstitutionQuantity;
				}

				if (TotalQuantity.Equals(""))
				{
					rowValues[11]	= DBNull.Value;
				}
				else
				{
					rowValues[11]	= TotalQuantity;
				}
				
				rowValues[12]	= Type;
				rowValues[13]	= Status;
				rowValues[14]	= ActUserId;
				rowValues[15] = ActTime;
              
				return rowValues;
			}
		}   
	}

	public class SubstitutionActivityEventWrapper : MarshalByRefObject
	{
		public event SubstitutionActivityEventHandler SubstitutionActivityEvent;
  
		public void DoEvent(SubstitutionActivityEventArgs e)
		{
			SubstitutionActivityEvent(e);
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
