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

namespace DashApplication.CustomClasses
{
    public class CustomEvents
    {
        public static event EventHandler<DetailEventArgs> DetailChanged = delegate { };

        public static void UpdateDetailInformation(string _bizDate, string _classGroup)
        {
            EventHandler<DetailEventArgs> informationEvent = DetailChanged;

            informationEvent(null, new DetailEventArgs(_bizDate, _classGroup));
        }

        public static event EventHandler<PageEventArgs> PageChanged = delegate { };

        public static void UpdatePageNavigation(string uri)
        {
            EventHandler<PageEventArgs> pageEvent = PageChanged;

            pageEvent(null, new PageEventArgs(uri));
        }
    }

    public class PageEventArgs : EventArgs
    {
        private string uri;

        public PageEventArgs(string uri)
        {
            this.uri = uri;

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
