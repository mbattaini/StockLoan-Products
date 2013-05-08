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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StockLoan.Encryption;
using C1.Silverlight;
using C1.Silverlight.Data;
using LoanStarWorldWideAdmin;

using LoanStarWorldWideAdmin.SVR_BooksService;
using LoanStarWorldWideAdmin.SVR_FunctionsService;
using LoanStarWorldWideAdmin.SVR_AdminService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class HolidaysMaintain : Page
    {
        public BooksServiceClient booksSvc = new BooksServiceClient();
        public FunctionsServiceClient functionsSvc = new FunctionsServiceClient();
        public AdminServiceClient adminSvc = new AdminServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        private static string _DateToMatch;
        private static string _HolidayBookGroup;
        private static bool _AddHoliday;

        public static string DateToMatch
        {
            get { return _DateToMatch; }
            set { _DateToMatch = value; }
        }
        public static string HolidayBookGroup
        {
            get { return _HolidayBookGroup; }
            set { _HolidayBookGroup = value; }
        }
        public static bool AddHoliday
        {
            get { return _AddHoliday; }
            set { _AddHoliday = value; }
        }

        public HolidaysMaintain()
        {
            InitializeComponent();

            try
            {
                functions.SetFunctionPath(this.Name.Trim());

                this.Visibility = Visibility.Visible;

                StatusLabel.Visibility = Visibility.Collapsed;

                booksSvc.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(booksSvc_BookGroupsGetAllCompleted);
                
                //adminSvc.CountriesGetCompleted += new EventHandler<CountriesGetCompletedEventArgs>(adminSvc_CountriesGetCompleted);

                //functionsSvc.HolidaysGetCompleted += new EventHandler<HolidaysGetCompletedEventArgs>(functionsSvc_HolidaysGetCompleted);
                functionsSvc.HolidaySetCompleted += new EventHandler<HolidaySetCompletedEventArgs>(functionsSvc_HolidaySetCompleted);
                functionsSvc.HolidaysGetListCompleted += new EventHandler<HolidaysGetListCompletedEventArgs>(functionsSvc_HolidaysGetListCompleted);
                
                appInfo.UserId = ((App)App.Current).UserId;
                appInfo.Password = ((App)App.Current).Password;
                appInfo.FunctionPath = ((App)App.Current).FunctionPath;
                appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;
                
                C1MouseHelper mouseHelperHolidayListGrid = new C1MouseHelper(HolidayListGrid);
                mouseHelperHolidayListGrid.MouseDoubleClick += new MouseEventHandler(mouseHelperHolidayListGrid_MouseDoubleClick);

                FillBookGroupCombo();
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
 
        }

        void mouseHelperHolidayListGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HolidayEdit();
        }

        void functionsSvc_HolidaysGetListCompleted(object sender, HolidaysGetListCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Holidays");

            if (dtTemp == null)
            {
                AddHoliday = true;
            }
            else
            {
                HolidayListGrid.ItemsSource = dtTemp.DefaultView;
                AddHoliday = false;
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StatusLabel.Visibility = Visibility.Collapsed;
            
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            if ((bookGroup == null) || (bookGroup.Equals("")))
            {
                bookGroup = appInfo.DefaultBookGroup;
                appInfo.LoggedOnBookGroup = bookGroup;
            }

            if ((functionPath == null) || (functionPath.Equals("")))
            {
                functionPath = appInfo.DefaultFunctionPath;
                appInfo.FunctionPath = functionPath;
            }
        }

        private void functionsSvc_HolidaySetCompleted(object sender, HolidaySetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " Holiday Set did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;

        }

        void  booksSvc_BookGroupsGetAllCompleted(object sender, BookGroupsGetAllCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "BookGroups");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookGroup"].ToString();
                    BookGroupCombo.Items.Add(temp);
                }
            }
            BookGroupCombo.SelectedIndex = 0;
        }

        private void FillBookGroupCombo()
        {
            BookGroupCombo.Items.Clear();

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            if ((bookGroup == null) || (bookGroup.Equals("")))
            {
                bookGroup = appInfo.DefaultBookGroup;
                appInfo.LoggedOnBookGroup = bookGroup;
            }

            if ((functionPath == null) || (functionPath.Equals("")))
            {
                functionPath = appInfo.DefaultFunctionPath;
                appInfo.FunctionPath = functionPath;
            }

            booksSvc.BookGroupsGetAllAsync(appInfo.DefaultBookGroup, "", userId, userPwd, functionPath);
        }

        private void HolidayAddButton_Click(object sender, RoutedEventArgs e)
        {
            HolidayAdd();
        }

        private void HolidayAdd()
        {
            ((App)App.Current).HolidayFunction = "Add";
            ((App)App.Current).HolidayCountryCode = "";
            ((App)App.Current).HolidayBookGroup = BookGroupCombo.Text;
            ((App)App.Current).HolidayCountry = "";
            ((App)App.Current).HolidayExplain = "";
            ((App)App.Current).HolidayDate = HolidayCalendar.SelectedDate.ToString("yyyy-MM-dd");

            ((App)App.Current).HolidayIsBank = false;
            ((App)App.Current).HolidayIsExchange = false;

            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            C1Window win = new C1Window();

            win.CenterOnScreen();
            win.Content = new HolidayAddWindow();

            win.ShowModal();
            win.Closed += new EventHandler(bookGroupWin_Closed);
        }

        private void HolidayEditButton_Click(object sender, RoutedEventArgs e)
        {
            HolidayEdit();
        }

        private void HolidayEdit()
        {
            if (HolidayListGrid.Selection.SelectedRows.Count >= 1)
            {
                ((App)App.Current).HolidayFunction = "Edit";

                string holidayCountryCode = HolidayListGrid.Selection.SelectedCells[0].Text.ToString();
                string holidayBookGroup = HolidayListGrid.Selection.SelectedCells[1].Text.ToString();
                string holidayCountry = HolidayListGrid.Selection.SelectedCells[2].Text.ToString();
                string holidayDate = HolidayListGrid.Selection.SelectedCells[3].Text.ToString();
                string holidayIsBank = HolidayListGrid.Selection.SelectedCells[4].Text.ToString();
                string holidayIsExchange = HolidayListGrid.Selection.SelectedCells[5].Text.ToString();
                string holidayExplain = HolidayListGrid.Selection.SelectedCells[6].Text.ToString();

                ((App)App.Current).HolidayCountryCode = holidayCountryCode;
                ((App)App.Current).HolidayBookGroup = holidayBookGroup;
                ((App)App.Current).HolidayCountry = holidayCountry;
                ((App)App.Current).HolidayExplain = holidayExplain;
                ((App)App.Current).HolidayDate = holidayDate;

                if (holidayIsBank.Equals("True"))
                { ((App)App.Current).HolidayIsBank = true; }
                else
                { ((App)App.Current).HolidayIsBank = false; }

                if (holidayIsExchange.Equals("True"))
                { ((App)App.Current).HolidayIsExchange = true; }
                else
                { ((App)App.Current).HolidayIsExchange = false; }

                ((App)App.Current).UserId = appInfo.UserId;
                ((App)App.Current).Password = appInfo.Password;
                ((App)App.Current).FunctionPath = appInfo.FunctionPath;
                ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

                C1Window win = new C1Window();

                win.CenterOnScreen();
                win.Content = new HolidayAddWindow();

                win.ShowModal();
                win.Closed += new EventHandler(bookGroupWin_Closed);
            }
            else
            {
                MessageBox.Show("You must select a holiday to edit from the grid");
                return;
            }
            }

        void bookGroupWin_Closed(object sender, EventArgs e)
        {
            FillBookGroupCombo();
            GetHolidayInfo();
        }

        private void BookGroupCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            ((App)App.Current).HolidayBookGroup = BookGroupCombo.Text;
            HolidayBookGroup = BookGroupCombo.Text;
        }


        private void HolidayCalendar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((App)App.Current).HolidayDate = HolidayCalendar.SelectedDate.ToString("yyyy-MM-dd");
            ((App)App.Current).HolidayExplain = "";
            ((App)App.Current).HolidayIsBank = false;
            ((App)App.Current).HolidayIsExchange = false;
            ((App)App.Current).HolidayIsActive = false;

            if (BookGroupCombo.Text.Equals(""))
            {
                UpdateLabel.Content = "You must select both a Book Group for Holiday Processing";
                UpdateLabel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                DateToMatch = HolidayCalendar.SelectedDate.ToString("yyyy-MM-dd");
                GetHolidayInfo(); 
            }
        }

        private void HolidayCalendar_SelectedDateChanged(object sender, C1.Silverlight.Schedule.DateTimePropertyChangeEventArgs e)
        {
            ((App)App.Current).HolidayDate = HolidayCalendar.SelectedDate.ToString("yyyy-MM-dd");
            ((App)App.Current).HolidayExplain = "";
            ((App)App.Current).HolidayIsBank = false;
            ((App)App.Current).HolidayIsExchange = false;
            ((App)App.Current).HolidayIsActive = false;

            if (BookGroupCombo.Text.Equals(""))
            {
                UpdateLabel.Content = "You must select a Book Group for Holiday Processing";
                UpdateLabel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                DateToMatch = HolidayCalendar.SelectedDate.ToString("yyyy-MM-dd");
                GetHolidayInfo(); 
            }
        }

        private void BookGroupCombo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateLabel.Visibility = Visibility.Collapsed;
        }

        private void CountryCombo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateLabel.Visibility = Visibility.Collapsed;
        }
    
        private void GetHolidayInfo()
        {             
            if (!BookGroupCombo.Text.Equals(""))
            {
                string userId = appInfo.UserId.ToString();
                string userPwd = appInfo.Password.ToString();
                string bookGroup = appInfo.LoggedOnBookGroup;
                string functionPath = appInfo.FunctionPath;
                if ((bookGroup == null) || (bookGroup.Equals("")))
                {
                    bookGroup = appInfo.DefaultBookGroup;
                    appInfo.LoggedOnBookGroup = bookGroup;
                }

                if ((functionPath == null) || (functionPath.Equals("")))
                {
                    functionPath = appInfo.DefaultFunctionPath;
                    appInfo.FunctionPath = functionPath;
                }

                DateToMatch = HolidayCalendar.SelectedDate.ToString("yyyy-MM-dd");

                functionsSvc.HolidaysGetListAsync(HolidayBookGroup, DateToMatch, "", "0", userId, userPwd, bookGroup, functionPath);
            }
        }
        private void HolidayCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        //private void CountryCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        //{
        //    string countryCode = "";
        //    string country = "";
            
        //    int startIndex = 0;
        //    int pos = CountryCombo.Text.IndexOf("|", startIndex);
        //    int len = int.Parse(CountryCombo.Text.Length.ToString());
        //    if (len > 0)
        //    {
        //        country = CountryCombo.Text.Substring(pos + 2, len - (pos + 2)).Trim();
        //        CountryTextBox.Text = country;

        //        countryCode = CountryCombo.Text.Substring(0, pos).Trim();
        //        CountryCodeLabel.Content = countryCode;
        //    }
        //}

        //private void FillCountriesCombo()
        //{
        //    CountryCombo.Items.Clear();

        //    if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
        //    {
        //        return;
        //    }
        //    string userId = appInfo.UserId.ToString();
        //    string userPwd = appInfo.Password.ToString();
        //    string bookGroup = appInfo.LoggedOnBookGroup;
        //    string functionPath = appInfo.FunctionPath;

        //    if ((bookGroup == null) || (bookGroup.Equals("")))
        //    {
        //        bookGroup = appInfo.DefaultBookGroup;
        //        appInfo.LoggedOnBookGroup = bookGroup;
        //    }

        //    if ((functionPath == null) || (functionPath.Equals("")))
        //    {
        //        functionPath = appInfo.DefaultFunctionPath;
        //        appInfo.FunctionPath = functionPath;
        //    }

        //    adminSvc.CountriesGetAsync("", userId, userPwd, bookGroup, functionPath);

        //}

        //void FillCountryCombo()
        //{
        //    string userId = appInfo.UserId.ToString();
        //    string userPwd = appInfo.Password.ToString();
        //    string bookGroup = appInfo.LoggedOnBookGroup;
        //    string functionPath = appInfo.FunctionPath;
        //    if ((bookGroup == null) || (bookGroup.Equals("")))
        //    {
        //        bookGroup = appInfo.DefaultBookGroup;
        //        appInfo.LoggedOnBookGroup = bookGroup;
        //    }

        //    if ((functionPath == null) || (functionPath.Equals("")))
        //    {
        //        functionPath = appInfo.DefaultFunctionPath;
        //        appInfo.FunctionPath = functionPath;
        //    }
        //    adminSvc.CountriesGetAsync("", userId, userPwd, bookGroup, functionPath);
        //}

        //private void ClearText()
        //{
        //    CountryTextBox.Text = "";
        //    ExplainTextBox.Text = "";
        //    BankCheckBox.IsChecked = false;
        //    ExchangeCheckBox.IsChecked = false;
        //    ActiveCheckBox.IsChecked = false;
        //}

    }
}
