using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using C1.Silverlight.Data;

namespace LoanStarWorldWideAdmin
{
	public class AppInformation
	{

        private  string _UserId = "";
        private  string _FullName = "";
        private  string _Title = "";
        private  string _ePassword = "";
        private  string _AdminFlag = "*All*";
        private  string _UserMaintType = "";

        private static string _DefaultBookGroup = "PFSI";
        private static string _DefaultFunctionPath = "AdminUsers";
        private static string _DefaultUserId = "BStone";        // this will change to ADMIN, or something defined when we convert all passwords to encrypted
        private static string _DefaultPassword = "rgj83XyAN3M3DthS1HUOtw==";
        //private  string _DefaultPassword = "K8UWEQTzJL+gaKQkwU0rlQ==";

        private  string _LoggedOnBookGroup;
        private  string _SelectedBookGroup;
        private  string _CurrentDesk;
        private  string _InventoryType;
        private  bool _LayoutsValid;
        private  bool _LayoutsLoaded;
        private  string _FunctionPath;
        private string _ScreenName;
        private  bool _SubscriptionUpdatesReq;
        private  bool _PWChange;
        private  bool _ChkBoxUsed = false;
        private  bool _FillIso;



        internal string DefaultUserId
        {
            get { return _DefaultUserId; }
        }

        internal  string DefaultPassword
        {
            get { return _DefaultPassword; }
        }

        public  string UserMaintType
        {
            get { return _UserMaintType; }

            set { _UserMaintType = value; }
        }

        public string ScreenName
        {
            get { return _ScreenName; }

            set { _ScreenName = value; }
        }

        public string LoggedOnBookGroup
        {
            get { return _LoggedOnBookGroup; }

            set { _LoggedOnBookGroup = value; }
        }

        public  string SelectedBookGroup
        {
            get { return _SelectedBookGroup; }

            set { _SelectedBookGroup = value; }
        }

        public  bool LayoutsValid
        {
            get { return _LayoutsValid; }

            set { _LayoutsValid = value; }
        }
        public  bool SubscriptionUpdatesReq
        {
            get { return _SubscriptionUpdatesReq; }

            set { _SubscriptionUpdatesReq = value; }
        }
        public  bool PWChange
        {
            get { return _PWChange; }

            set { _PWChange = value; }
        }
        public  bool FillIso
        {
            get { return _FillIso; }

            set { _FillIso = value; }
        }
        public  bool ChkBoxUsed
        {
            get { return _ChkBoxUsed; }

            set { _ChkBoxUsed = value; }
        }
        public  bool LayoutsLoaded
        {
            get { return _LayoutsLoaded; }

            set { _LayoutsLoaded = value; }
        }

        public  string FunctionPath
        {
            get { return _FunctionPath; }

            set { _FunctionPath = value; }
        }

        public  string InventoryType
        {
            get { return _InventoryType; }

            set { _InventoryType = value; }
        }
        public  string CurrentDesk
        {
            get { return _CurrentDesk; }

            set { _CurrentDesk = value; }
        }

        public  string UserId
		{
			get	{return _UserId;}
				
			set	{_UserId = value;}
		}

        public  string AdminFlag
        {
            get { return _AdminFlag; }
        }

        public  string DefaultBookGroup
        {
            get { return _DefaultBookGroup; }
        }

        public  string DefaultFunctionPath
        {
            get { return _DefaultFunctionPath; }
        }

        public  string FullName
        {
            get { return _FullName; }

            set { _FullName = value; }
        }

        public  string Password
        {
            get { return _ePassword; }

            set { _ePassword = value; }
        }

        public  string Title
        {
            get { return _Title; }

            set { _Title = value; }
        }
	}		
}