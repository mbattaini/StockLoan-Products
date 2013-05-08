using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LoanStarWorldWideAdmin
{
    public partial class App : Application
    {
        private static string _DefaultBookGroup = "PFSI";
        private static string _DefaultFunctionPath = "AdminUsers";
        private static string _DefaultUserId = "BStone";        // this will change to ADMIN, or something defined when we convert all passwords to encrypted
        private static string _DefaultPassword = "rgj83XyAN3M3DthS1HUOtw==";
        //private  string _DefaultPassword = "K8UWEQTzJL+gaKQkwU0rlQ==";
        private static string _SelectedBookGroup = "";
        private static string _CurrentDesk = "";
        private static string _InventoryType;

        private static string _UserId;
        private static string _ePassword = "";
        private static string _LoggedOnBookGroup;
        private static string _FunctionPath;
        private static string _ScreenNameText;

        private static string _HolidayBookGroup;
        private static string _HolidayDate;
        private static string _HolidayCountry;
        private static string _HolidayExplain;
        private static string _HolidayCountryCode;
        private static string _HolidayFunction;

        private static bool _HolidayIsBank;
        private static bool _HolidayIsExchange;
        private static bool _HolidayIsActive;

        private string _FirmCode;
        private string _FirmName;
        private string _IsActive;

        private string _FileName;
        private string _FileHost;
        private string _FileUser;
        private string _FilePWD;
        private string _EMailAddress;
        private string _EMailSubject;
        private string _SubscribeComment;
        private string _Disable;
        private string _EditSubscription;

        public string EditSubscription
        {
            get { return _EditSubscription; }
            set { _EditSubscription = value; }
        }
        public string Disable
        {
            get { return _Disable; }
            set { _Disable = value; }
        }
        public string SubscribeComment
        {
            get { return _SubscribeComment; }
            set { _SubscribeComment = value; }
        }
        public string InventoryType
        {
            get { return _InventoryType; }
            set { _InventoryType = value; }
        }
        public string EMailSubject
        {
            get { return _EMailSubject; }
            set { _EMailSubject = value; }
        }
        public string EMailAddress
        {
            get { return _EMailAddress; }
            set { _EMailAddress = value; }
        }
        public string FilePWD
        {
            get { return _FilePWD; }
            set { _FilePWD = value; }
        }
        public string FileUser
        {
            get { return _FileUser; }
            set { _FileUser = value; }
        }
        public string FileHost
        {
            get { return _FileHost; }
            set { _FileHost = value; }
        }
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public string FirmCode
        {
            get { return _FirmCode; }
            set { _FirmCode = value; }
        }

        public string FirmName
        {
            get { return _FirmName; }
            set { _FirmName = value; }
        }

        public string IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }


        public string DefaultBookGroup
        {
            get { return _DefaultBookGroup; }
        }

        public string SelectedBookGroup
        {
            get { return _SelectedBookGroup; }
            set { _SelectedBookGroup = value; }
        }

        public string ScreenNameText
        {
            get { return _ScreenNameText; }
            set { _ScreenNameText = value; }
        }

        public string CurrentDesk
        {
            get { return _CurrentDesk; }
            set { _CurrentDesk = value; }
        }

        public string DefaultFunctionPath
        {
            get { return _DefaultFunctionPath; }
        }

        internal string DefaultUserId
        {
            get { return _DefaultUserId; }
        }

        internal  string DefaultPassword
        {
            get { return _DefaultPassword; }
        }

        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public  string LoggedOnBookGroup
        {
            get { return _LoggedOnBookGroup; }
            set { _LoggedOnBookGroup = value; }
        }

        public  string FunctionPath
        {
            get { return _FunctionPath; }
            set { _FunctionPath = value; }
        }

        public  string Password
        {
            get { return _ePassword; }
            set { _ePassword = value; }
        }

        public string HolidayFunction
        {
            get { return _HolidayFunction; }
            set { _HolidayFunction = value; }
        }
        public string HolidayBookGroup
        {
            get { return _HolidayBookGroup; }
            set { _HolidayBookGroup = value; }
        }
        public string HolidayDate
        {
            get { return _HolidayDate; }
            set { _HolidayDate = value; }
        }
        public string HolidayCountry
        {
            get { return _HolidayCountry; }
            set { _HolidayCountry = value; }
        }
        public string HolidayCountryCode
        {
            get { return _HolidayCountryCode; }
            set { _HolidayCountryCode = value; }
        }
        public string HolidayExplain
        {
            get { return _HolidayExplain; }
            set { _HolidayExplain = value; }
        }
        public bool HolidayIsBank
        {
            get { return _HolidayIsBank; }
            set { _HolidayIsBank = value; }
        }
        public bool HolidayIsExchange
        {
            get { return _HolidayIsExchange; }
            set { _HolidayIsExchange = value; }
        }
        public bool HolidayIsActive
        {
            get { return _HolidayIsActive; }
            set { _HolidayIsActive = value; }
        }

        public App()
        {

        this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;
            
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPage();
        }


        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                e.Handled = true;
                ChildWindow errorWin = new ErrorWindow(e.ExceptionObject);
                errorWin.Show();
            }
        }
    }
}