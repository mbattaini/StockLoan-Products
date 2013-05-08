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

namespace StockLoan_LocatesClient
{
	public struct ListItem
	{
		public string SecId;
		public long Quantity;
	}
	
    public class CustomEvents
    {
        public static event EventHandler<LocateCountChangeEventArgs> LocateCountChanged = delegate { };

        public static void UpdateLocateCountChangeInformation(int index)
        {
            EventHandler<LocateCountChangeEventArgs> informationEvent = LocateCountChanged;

            informationEvent(null, new LocateCountChangeEventArgs(index));
        }
		
		public static event EventHandler<ToolBarChangeEventArgs> ToolBarInformationChanged = delegate {};
		
		public static void UpdateToolBarChangeInformation(string _bizDate,string _bookGroup, string _book)
		{
            EventHandler<ToolBarChangeEventArgs> informationEvent = ToolBarInformationChanged;

            informationEvent(null, new ToolBarChangeEventArgs(_bizDate, _bookGroup, _book));
		}
		
        public static event EventHandler<UserBookGroupEventArgs> UserBookGroupInformationChanged = delegate { };

        public static void UpdateUserBookGroupInformation(string _bookGroup, string _book)
        {
            EventHandler<UserBookGroupEventArgs> informationEvent = UserBookGroupInformationChanged;

            informationEvent(null, new UserBookGroupEventArgs(_bookGroup, _book));
        }

        public static event EventHandler<UserEventArgs> UserInformationChanged = delegate { };

        public static void UpdateUserInformation(string _userId, bool _logOffUser)
        {
            EventHandler<UserEventArgs> informationEvent = UserInformationChanged;

            informationEvent(null, new UserEventArgs(_userId, _logOffUser));
        }
	

        public static event EventHandler<SecIdEventArgs> SecIdChanged = delegate { };

        public static void UpdateSecIdInformation(string _secId)
        {
            EventHandler<SecIdEventArgs> informationEvent = SecIdChanged;

            informationEvent(null, new SecIdEventArgs(_secId));
        }
		
		 public static event EventHandler<CustomerEventArgs> CustomerChanged = delegate { };

         public static void UpdateCustomerInformation(string _groupCode, string _secId)
         {
             if (!_groupCode.Equals("") && !_secId.Equals(""))
             {
                 EventHandler<CustomerEventArgs> informationEvent = CustomerChanged;

                 informationEvent(null, new CustomerEventArgs(_groupCode, _secId));
             }
         }

         public static event EventHandler<GroupCodeFilterEventArgs> GroupCodeFilterChanged = delegate { };

         public static void UpdateGroupCodeFilterInformation(string _groupCode)
         {
             if (!_groupCode.Equals(""))
             {
                 EventHandler<GroupCodeFilterEventArgs> informationEvent = GroupCodeFilterChanged;

                 informationEvent(null, new GroupCodeFilterEventArgs(_groupCode));
             }
         }
    }

    public class LocateCountChangeEventArgs : EventArgs
    {
        private int index;


        public LocateCountChangeEventArgs(int _index)
        {
            this.index = _index;
        }


        public int Index
        {
            get { return index; }
        }
    }
	

    public class GroupCodeFilterEventArgs : EventArgs
    {
        private string groupCode;


        public GroupCodeFilterEventArgs(string _groupCode)
        {
            this.groupCode = _groupCode;            
        }


        public string GroupCode
        {
            get { return groupCode; }
        }
    }
	

	   public class CustomerEventArgs : EventArgs
    {
        private string groupCode;
        private string secId;

        public CustomerEventArgs(string _groupCode, string _secId)
        {
            this.groupCode = _groupCode;
            this.secId = _secId;
        }


        public string GroupCode
        {
            get { return groupCode; }
        }

        public string SecId
        {
            get { return secId; }
        }
    }
	
    public class UserBookGroupEventArgs : EventArgs
    {
        private string bookGroup;
        private string book;        

        public UserBookGroupEventArgs(string _bookGroup, string _book)
        {
            this.bookGroup = _bookGroup;
            this.book = _book;
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

        public ToolBarChangeEventArgs(string _bizDate, string _bookGroup, string _book)
        {
            this.bizDate = _bizDate;
            this.bookGroup = _bookGroup;
            this.book = _book;
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

        public UserEventArgs(string userId, bool logOffUser)
        {
            this.userId = userId;
            this.logOffUser = logOffUser;
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

        public SecIdEventArgs(string secId)
        {
            this.secId = secId;
        }

        public string SecId
        {
            get { return secId; }
        }
    } 
}
