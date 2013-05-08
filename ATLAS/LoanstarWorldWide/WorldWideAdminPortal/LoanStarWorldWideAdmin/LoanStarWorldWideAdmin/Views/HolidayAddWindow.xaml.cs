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
using C1.Silverlight;
using C1.Silverlight.Data;

using LoanStarWorldWideAdmin;
using LoanStarWorldWideAdmin.SVR_FunctionsService;
using LoanStarWorldWideAdmin.SVR_AdminService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class HolidayAddWindow : UserControl
    {
        public FunctionsServiceClient functionsSvc = new FunctionsServiceClient();
        public AdminServiceClient adminSvc = new AdminServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        private static string _DateToMatch;
        
        public static string DateToMatch
        {
            get { return _DateToMatch; }

            set { _DateToMatch = value; }
        }

        public string holidayBookGroup;
        public string holidayDate;
        public string holidayCountry;
        public string holidayExplain;
        public string holidayCountryCode;

        public bool holidayIsBank;
        public bool holidayIsExchange;
        public bool holidayIsActive;

        public string holidayFunction;  //BS; Either Add or Update - depending on which button used to call this

        public HolidayAddWindow()
        {
            InitializeComponent();
        
            try
            {
                functions.SetFunctionPath(this.Name.Trim());

                this.Visibility = Visibility.Visible;

                functionsSvc.HolidaySetCompleted += new EventHandler<HolidaySetCompletedEventArgs>(functionsSvc_HolidaySetCompleted);
                adminSvc.CountriesGetCompleted += new EventHandler<CountriesGetCompletedEventArgs>(adminSvc_CountriesGetCompleted);

                appInfo.UserId = ((App)App.Current).UserId;
                appInfo.Password = ((App)App.Current).Password;
                appInfo.FunctionPath = ((App)App.Current).FunctionPath;
                appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

                holidayFunction = ((App)App.Current).HolidayFunction;

                holidayCountryCode = ((App)App.Current).HolidayCountryCode;
                holidayBookGroup = ((App)App.Current).HolidayBookGroup;
                holidayCountry = ((App)App.Current).HolidayCountry;
                holidayExplain = ((App)App.Current).HolidayExplain;
                holidayDate = ((App)App.Current).HolidayDate;
                
                holidayIsBank = ((App)App.Current).HolidayIsBank; 
                holidayIsExchange = ((App)App.Current).HolidayIsExchange;
                holidayIsActive = true;

                FillCountriesCombo();
                FillForm();

            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        void adminSvc_CountriesGetCompleted(object sender, CountriesGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Countries");

            string countryCode = ((App)App.Current).HolidayCountryCode;
            int countryIndex = 0;
            int ctr = 0;
            if (dtTemp == null)
            {
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    if (!dr["CountryCode"].ToString().Trim().Equals("**"))
                    {
                        if(dr["CountryCode"].ToString().Trim().Equals(countryCode))
                        {
                            countryIndex = ctr;
                        }
                        string temp = dr["CountryCode"].ToString().Trim() + " | " + dr["Country"].ToString().Trim();
                        //string temp = dr["Country"].ToString().Trim();
                        this.CountryCombo.Items.Add(temp);
                    }
                    ctr++;
                }
                CountryCombo.SelectedIndex = countryIndex - 1;
            }
        }

        private void functionsSvc_HolidaySetCompleted(object sender, HolidaySetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                HolidaySaveButton.Visibility = Visibility.Visible;
                statusLabel.Content = "Add/Update Holiday Failed";
                return;
            }
            else
            {
                HolidaySaveButton.Visibility = Visibility.Collapsed;
                statusLabel.Content = "Holiday Successfully Added";
                return;

            }
        }

        private void HolidaySaveButton_Click(object sender, RoutedEventArgs e)
        {
            HolidaySave();
        }

        private void HolidaySave()
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

            string bookGroupSet = BookGroupNameTextBox.Text;

            string date = DateTextBox.Text;
            string countryCode = CountryCodeLabel.Content.ToString();
            string description = ExplainTextBox.Text.Trim();
            bool isBankHoliday = BankCheckBox.IsChecked.Value;
            bool isExchangeHoliday = ExchangeCheckBox.IsChecked.Value;
            bool isActive = ActiveCheckBox.IsChecked.Value;

            functionsSvc.HolidaySetAsync(bookGroupSet, date, countryCode, description, isBankHoliday, isExchangeHoliday, userId, isActive,
                                            bookGroup, userId, userPwd, functionPath);


        }

        private void FillForm()
        {
            ClearFields();

            BookGroupNameTextBox.Text = holidayBookGroup;
            DateTextBox.Text = holidayDate;
            
            if (holidayFunction.ToLower().Equals("edit"))
            {
                CountryTextBox.Text = holidayCountry;
                CountryCodeLabel.Content = holidayCountryCode;
                ExplainTextBox.Text = holidayExplain;
                //if(holidayIsBank.Equals("True"))  { 
                BankCheckBox.IsChecked = holidayIsBank;
                ExchangeCheckBox.IsChecked = holidayIsExchange;
            }

        }
        private void ClearFields()
        {
            BookGroupNameTextBox.Text = "";
            DateTextBox.Text = "";
            CountryTextBox.Text = "";
            CountryCodeLabel.Content = "";
            CountryCombo.SelectedIndex = -1;
            ExplainTextBox.Text = "";
            BankCheckBox.IsChecked = false;
            ExchangeCheckBox.IsChecked = false;
            ActiveCheckBox.IsChecked = true;
            statusLabel.Content = "";
        }

        private void FillCountriesCombo()
        {
            CountryCombo.Items.Clear();
            string holidayFunction = ((App)App.Current).HolidayFunction;
            
            
            string countryCode = ((App)App.Current).HolidayCountryCode;

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

            adminSvc.CountriesGetAsync("", userId, userPwd, bookGroup, functionPath);
        }

        private void CountryCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            string countryCode = "";
            string country = "";
            
            int startIndex = 0;
            int pos = CountryCombo.Text.IndexOf("|", startIndex);
            int len = int.Parse(CountryCombo.Text.Length.ToString());
            if (len > 0)
            {
                country = CountryCombo.Text.Substring(pos + 2, len - (pos + 2)).Trim();
                CountryTextBox.Text = country;

                countryCode = CountryCombo.Text.Substring(0, pos).Trim();
                CountryCodeLabel.Content = countryCode;
            }
       
        }
    }
}
