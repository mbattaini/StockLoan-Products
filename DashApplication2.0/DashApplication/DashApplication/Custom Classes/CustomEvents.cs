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

using C1.Silverlight.Data;

namespace DashApplication.CustomClasses
{
    public class CustomEvents
    {

        public static event EventHandler<UserIdEventArgs> UserIdChanged = delegate { };

        public static void UpdateUserIdInformation(string userId, string password)
        {
            EventHandler<UserIdEventArgs> userEvent = UserIdChanged;

            userEvent(null, new UserIdEventArgs(userId, password));
        }


        public static event EventHandler<RemoveControlEventArgs> RemoveControlChanged = delegate { };

        public static void UpdateRemoveControlInformation(UIElement item)
        {
            EventHandler<RemoveControlEventArgs> informationEvent = RemoveControlChanged;

            informationEvent(null, new RemoveControlEventArgs(item));
        }
       

        public static event EventHandler<CheckEventArgs> CheckChanged = delegate { };

        public static void UpdateCheckInformation()
        {
            EventHandler<CheckEventArgs> informationEvent = CheckChanged;

            informationEvent(null, new CheckEventArgs());
        }
        
        public static event EventHandler<DetailEventArgs> DetailChanged = delegate { };

        public static void UpdateDetailInformation(string _bizDate, string _classGroup)
        {
            EventHandler<DetailEventArgs> informationEvent = DetailChanged;

            informationEvent(null, new DetailEventArgs(_bizDate, _classGroup));
        }

        public static event EventHandler<CollateralDetailEventArgs> CollateralDetailChanged = delegate { };

        public static void UpdateCollateralDetailInformation(string header, DataTable dtCollateral)
        {
            EventHandler<CollateralDetailEventArgs> detailEvent = CollateralDetailChanged;

            detailEvent(null, new CollateralDetailEventArgs(header, dtCollateral));
        }
        
        public static event EventHandler<PageEventArgs> PageChanged = delegate { };

        public static void UpdatePageNavigation(string title, string uri)
        {
            EventHandler<PageEventArgs> pageEvent = PageChanged;

            pageEvent(null, new PageEventArgs(title, uri));
        }
    }

    
    public class UserIdEventArgs : EventArgs
    {
        private string userId;
        private string password;

        public UserIdEventArgs(string userId, string password)
        {
            this.userId = userId;
            this.password = password;
        }

        public string UserId
        {
            get
            {
                return userId;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
        }
    }

    public class RemoveControlEventArgs : EventArgs
    {
        private UIElement item;

        public RemoveControlEventArgs(UIElement item)
        {
            this.item = item;
        }

        public UIElement Item
        {
            get
            {
                return item;
            }
        }
    }



    public class CheckEventArgs : EventArgs
    {
        public CheckEventArgs()
        {
        }
    }

    public class CollateralDetailEventArgs : EventArgs
    {
        private string header;
        private DataTable dtCollateral;

        public CollateralDetailEventArgs(string header, DataTable dtCollateral)
        {
            this.header = header;
            this.dtCollateral = dtCollateral;
        }


        public string Header
        {
            get
            {
                return header;
            }
        }

        public DataTable CollateralDetail
        {
            get
            {
                return dtCollateral;
            }
        }
    }

    public class PageEventArgs : EventArgs
    {
        private string title;
        private string uri;

        public PageEventArgs(string title, string uri)
        {
            this.title = title;
            this.uri = uri;

        }

        public string Title
        {
            get
            {
                return title;
            }
        }


        public string Uri
        {
            get
            {
                return uri;
            }
        }
    }

    

    public class DetailEventArgs : EventArgs
    {
        private string bizDate;
        private string classGroup;

        public DetailEventArgs(string _bizDate, string _classGroup)
        {
            this.bizDate = _bizDate;
            this.classGroup = _classGroup;
        }

        public string BizDate
        {
            get
            {
                return bizDate;
            }
        }

        public string ClassGroup
        {
            get
            {
                return classGroup;
            }
        }
    }	
}
