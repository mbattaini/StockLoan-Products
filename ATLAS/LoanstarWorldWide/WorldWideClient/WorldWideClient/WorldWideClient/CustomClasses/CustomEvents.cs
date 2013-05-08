using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WorldWideClient
{
	public struct ListItem
	{
		public string SecId;
		public long Quantity;
	}
	
    public class CustomEvents
    {
		public static event EventHandler<ToolBarChangeEventArgs> ToolBarInformationChanged = delegate {};
		
		public static void UpdateToolBarChangeInformation(string _bizDate,string _bookGroup, string _book, int _controlHndle)
		{
            EventHandler<ToolBarChangeEventArgs> informationEvent = ToolBarInformationChanged;

            informationEvent(null, new ToolBarChangeEventArgs(_bizDate, _bookGroup, _book, _controlHndle));
		}

        public static event EventHandler<ContractChangeEventArgs> ContractInformationChanged = delegate { };

        public static void UpdateContractChangeInformation(string _bizDate, string _bookGroup, string _book, string _secId, string _functionPath, int _controlHndle)
        {
            EventHandler<ContractChangeEventArgs> informationEvent = ContractInformationChanged;

            informationEvent(null, new ContractChangeEventArgs(_bizDate, _bookGroup, _book, _secId, _functionPath, _controlHndle));
        }
		
        public static event EventHandler<UserBookGroupEventArgs> UserBookGroupInformationChanged = delegate { };

        public static void UpdateUserBookGroupInformation(string _bookGroup, string _book, int _controlHndle)
        {
            EventHandler<UserBookGroupEventArgs> informationEvent = UserBookGroupInformationChanged;

            informationEvent(null, new UserBookGroupEventArgs(_bookGroup, _book, _controlHndle));
        }

        public static event EventHandler<UserEventArgs> UserInformationChanged = delegate { };

        public static void UpdateUserInformation(string _userId, bool _logOffUser, int _controlHndle)
        {
            EventHandler<UserEventArgs> informationEvent = UserInformationChanged;

            informationEvent(null, new UserEventArgs(_userId, _logOffUser, _controlHndle));
        }
	

        public static event EventHandler<SecIdEventArgs> SecIdChanged = delegate { };

        public static void UpdateSecIdInformation(string _secId, int _controlHndle)
        {
            EventHandler<SecIdEventArgs> informationEvent = SecIdChanged;

            informationEvent(null, new SecIdEventArgs(_secId, _controlHndle));
        }
    }


    public class ContractChangeEventArgs : EventArgs
    {
        private string bizDate;
        private string bookGroup;
        private string book;
        private string secId;
        private string functionPath;
        private int controlHndle;

        public ContractChangeEventArgs(string _bizDate, string _bookGroup, string _book, string _secId, string _functionPath, int _controlHndle)
        {
            this.bizDate = _bizDate;
            this.bookGroup = _bookGroup;
            this.book = _book;
            this.secId = _secId;
            this.functionPath = _functionPath;
            this.controlHndle = _controlHndle;
        }

        public int ControlHandle
        {
            get { return controlHndle; }       
        }

        public string BizDate
        {
            get { return bizDate; }
        }

        public string BookGroup
        {
            get { return bookGroup; }
        }

        public string Book
        {
            get { return book; }
        }

        public string SecId
        {
            get { return secId; }
        }

        public string FunctionPath
        {
            get { return functionPath; }
        }
    }

    public class UserBookGroupEventArgs : EventArgs
    {
        private string bookGroup;
        private string book;
        private int controlHndle;


        public UserBookGroupEventArgs(string _bookGroup, string _book, int _controlHndle)
        {
            this.bookGroup = _bookGroup;
            this.book = _book;
            this.controlHndle = _controlHndle;
        }

        public int ControlHandle
        {
            get { return controlHndle; }
        }

        public string BookGroup
        {
            get { return bookGroup; }
        }

        public string Book
        {
            get { return book; }
        }
    }
	
	 public class ToolBarChangeEventArgs : EventArgs
    {
         private string bizDate;
        private string bookGroup;       
        private string book;
        private int controlHndle;

        public ToolBarChangeEventArgs(string _bizDate, string _bookGroup, string _book, int _controlHndle)
        {
            this.bizDate = _bizDate;
            this.bookGroup = _bookGroup;
            this.book = _book;
            this.controlHndle = _controlHndle;
        }

        public int ControlHandle
        {
            get { return controlHndle; }
        }


        public string BizDate
        {
            get { return bizDate; }
        }

        public string BookGroup
        {
            get { return bookGroup; }
        }

        public string Book
        {
            get { return book; }
        }
    }


    public class UserEventArgs : EventArgs
    {
        private string userId;
        private bool logOffUser;
        private int controlHndle;

        public UserEventArgs(string userId, bool logOffUser, int _controlHndle)
        {
            this.userId = userId;
            this.logOffUser = logOffUser;
            this.controlHndle = _controlHndle;
        }

        public int ControlHandle
        {
            get { return controlHndle; }
        }

        public string UserId
        {
            get { return userId; }
        }

        public bool LogOffUser
        {
            get { return logOffUser; }
        }
    }

    public class SecIdEventArgs : EventArgs
    {
        private string secId;
        private int controlHndle;

        public SecIdEventArgs(string secId, int _controlHndle)
        {
            this.secId = secId;
            this.controlHndle = _controlHndle;
        }

        public int ControlHandle
        {
            get { return controlHndle; }
        }


        public string SecId
        {
            get { return secId; }
        }
    } 
}
