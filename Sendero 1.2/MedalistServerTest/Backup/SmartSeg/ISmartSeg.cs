using System;

namespace Anetics.SmartSeg
{
	public delegate void HeartbeatEventHandler(HeartbeatEventArgs e); 
	public delegate void ProcessStatusEventHandler(ProcessStatusEventArgs e);
	
	public enum HeartbeatStatus
	{
		Alert,
		Normal,
		Stopping,
		Unknown
	};

	public interface ISmartSeg
	{
		event HeartbeatEventHandler HeartbeatEvent;
		event ProcessStatusEventHandler ProcessStatusEvent;

		void SubstitutionRequest (	
			string	processId,
			string	securityId,
			string	securityIdType,
			string	requestType,
			string	quantity,
			string	minQuantity,
			string	overrideRate,
			string	maxProcessingTime,
			string	sendDTCMemoSeg);

		void MarginPositonDeleteRequest (			
			string		processId,
			string		secId,
			string		secIdType,
			string		accountNumber);
	}

	[Serializable]
	public class HeartbeatEventArgs : EventArgs
	{
		private HeartbeatStatus status;
		private string alert;

		public HeartbeatEventArgs(HeartbeatStatus status, string alert)
		{
			this.status = status;
			this.alert = alert;
		}

		public HeartbeatStatus Status
		{
			get
			{
				return status;
			}
		}

		public string Alert
		{
			get
			{
				return alert;
			}
		}
	}
  
	public class HeartbeatEventWrapper : MarshalByRefObject
	{
		public event HeartbeatEventHandler HeartbeatEvent;
  
		public void DoEvent(HeartbeatEventArgs e)
		{
			HeartbeatEvent(e);      
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}

	[Serializable]
	public class ProcessStatusEventArgs : EventArgs
	{
		private string  processId;
		private string  systemCode;
		private string  actCode;
		private string  act;
		private string  actTime;
		private string  actUser;
		private bool    hasError;
		private string  bookGroup;
		private string  contractId;
		private string  contractType;
		private string  book;
		private string  secId;
		private string  symbol;
		private string	quantity;
		private string	amount;
		private string  status;
		private string  statusTime;
		private string  tag;
		private short   utcOffset = 0;
  
		public ProcessStatusEventArgs(
			string  processId,
			string  systemCode,
			string  actCode,
			string  act,
			string  actTime,
			string  actUser,
			bool    hasError,
			string  bookGroup,
			string  contractId,
			string  contractType,
			string  book,
			string  secId,
			string  symbol,
			string  quantity,
			string  amount,
			string  status,
			string  statusTime,
			string  tag
			)
		{ 
			this.processId    = processId;
			this.systemCode   = systemCode; 
			this.actCode      = actCode; 
			this.act          = act;   
			this.actTime      = actTime; 
			this.actUser      = actUser; 
			this.hasError     = hasError; 
			this.bookGroup    = bookGroup; 
			this.contractId   = contractId;
			this.contractType = contractType;
			this.book         = book;
			this.secId        = secId; 
			this.symbol       = symbol;
			this.quantity		= quantity;
			this.amount		= amount;
			this.status       = status; 
			this.statusTime   = statusTime; 
			this.tag          = tag;
		}
  
		public string ProcessId
		{
			get
			{
				return processId;
			}

			set
			{
				processId = value;
			}
		}

		public string SystemCode
		{
			get
			{
				return systemCode;
			}
		}
   
		public string ActCode
		{
			get
			{
				return actCode;
			}
		}
    
		public string Act
		{
			get
			{
				return act;
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
      
		public string ActUser
		{
			get
			{
				return actUser;
			}
		}

		public bool HasError
		{
			get
			{
				return hasError;
			}
		}

		public string BookGroup
		{
			get
			{
				return bookGroup;
			}
		}

		public string ContractId
		{
			get
			{
				return contractId;
			}
		}

		public string ContractType
		{
			get
			{
				return contractType;
			}
		}

		public string Book
		{
			get
			{
				return book;
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

		public string Quantity
		{
			get
			{
				return quantity;
			}
		}

		public string Amount
		{
			get
			{
				return amount;
			}
		}

		public string Status
		{
			get
			{
				return status;
			}
		}
    
		public string StatusTime
		{
			get
			{
				try
				{
					return DateTime.Parse(statusTime).AddMinutes((double)utcOffset).ToString("yyyy-MM-dd HH:mm:ss.fff");
				}
				catch
				{
					return statusTime;        
				}
			}
		}

		public string Tag
		{
			get
			{
				return tag;
			}
		}

		public short UtcOffset
		{
			set
			{
				utcOffset = value;
			}
		}

		public object[] Values
		{
			get
			{
				object[] values = new object[18];
          
				values[0] = ProcessId;
				values[1] = SystemCode;          
				values[2] = ActCode;
				values[3] = Act;
 
				if (actTime.Equals(""))
				{
					values[4] = DBNull.Value;
				}
				else
				{
					values[4] = ActTime;
				}
          
				values[5] = ActUser;
				values[6] = HasError;
				values[7] = BookGroup;          
				values[8] = ContractId;
				values[9] = ContractType;
				values[10] = Book;
				values[11] = SecId;
				values[12] = Symbol;
		
				if (quantity.Equals(""))
				{
					values[13] = DBNull.Value;
				}
				else
				{
					values[13] = Quantity;
				}
		  
	    
				if (amount.Equals(""))
				{
					values[14] = DBNull.Value;
				}
				else
				{
					values[14] = Amount;
				}
		          		
				values[15] = Status;
        
				if (statusTime.Equals(""))
				{
					values[16] = DBNull.Value;
				}
				else
				{
					values[16] = StatusTime;
				}

				values[17] = Tag;
        
				return values;
			}
		}
	}

	public class ProcessStatusEventWrapper : MarshalByRefObject
	{
		public event ProcessStatusEventHandler ProcessStatusEvent;
  
		public void DoEvent(ProcessStatusEventArgs e)
		{
			ProcessStatusEvent(e);
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
