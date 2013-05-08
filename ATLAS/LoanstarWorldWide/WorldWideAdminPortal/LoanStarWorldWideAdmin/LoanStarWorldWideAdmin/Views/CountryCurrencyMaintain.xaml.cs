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
using System.Windows.Navigation;
using LoanStarWorldWideAdmin.SVR_AdminService;
using C1.Silverlight.Data;
using C1.Silverlight;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class CountryCurrencyMaintain : Page
    {
        public AdminServiceClient adminSvc = new AdminServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        private static bool _GetCountryDetail;
        public static bool GetCountryDetail
        {
            get { return _GetCountryDetail; }
            set { _GetCountryDetail = value; }
        }

        private static bool _GetCurrencyDetail;
        public static bool GetCurrencyDetail
        {
            get { return _GetCurrencyDetail; }
            set { _GetCurrencyDetail = value; }
        }

        private static bool _ConversionCheck;
        public static bool ConversionCheck
        {
            get { return _ConversionCheck; }
            set { _ConversionCheck = value; }
        }

        private static bool _FillConversions;
        public static bool FillConversions
        {
            get { return _FillConversions; }
            set { _FillConversions = value; }
        }

        private static string _FromIso;
        public static string FromIso
        {
            get { return _FromIso; }
            set { _FromIso = value; }
        }

        private static string _ToIso;
        public static string ToIso
        {
            get { return _ToIso; }
            set { _ToIso = value; }
        }

        public CountryCurrencyMaintain()
        {
            InitializeComponent();

            functions.SetFunctionPath(this.Name.Trim());

            adminSvc.CountriesGetCompleted += new EventHandler<CountriesGetCompletedEventArgs>(adminSvc_CountriesGetCompleted);
            adminSvc.CountrySetCompleted += new EventHandler<CountrySetCompletedEventArgs>(adminSvc_CountrySetCompleted);

            adminSvc.CurrenciesGetCompleted += new EventHandler<CurrenciesGetCompletedEventArgs>(adminSvc_CurrenciesGetCompleted);
            adminSvc.CurrencySetCompleted += new EventHandler<CurrencySetCompletedEventArgs>(adminSvc_CurrencySetCompleted);

            adminSvc.CurrencyConversionsGetCompleted += new EventHandler<CurrencyConversionsGetCompletedEventArgs>(adminSvc_CurrencyConversionsGetCompleted);
            adminSvc.CurrencyConversionSetCompleted += new EventHandler<CurrencyConversionSetCompletedEventArgs>(adminSvc_CurrencyConversionSetCompleted);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            appInfo.UserId = ((App)App.Current).UserId;
            appInfo.Password = ((App)App.Current).Password;
            appInfo.FunctionPath = ((App)App.Current).FunctionPath;
            appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

            StatusLabel.Visibility = Visibility.Collapsed;
            DeskLoad();
        }

        private void DeskLoad()
        {
			try
			{
                CountryCheckBox.IsChecked = false;
                CurrencyCheckBox.IsChecked = false;
                ConversionCheckBox.IsChecked = false;
                AllCheckBox.IsChecked = true;
                AllCheckClick();
            }
			catch {}
        }

        #region GetsCompleted

        void adminSvc_CurrencyConversionsGetCompleted(object sender, CurrencyConversionsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            string isoTo = IsoToCombo.Text;

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "CurrencyConversions");

            if (dtTemp == null)
            {
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    if (!dr["CurrencyIsoTo"].ToString().Trim().Equals("**"))
                    {
                        if (dr["CurrencyIsoTo"].ToString().Trim().Equals(isoTo))
                        {
                            ConversionRateTextBox.Text = dr["CurrencyConvertRate"].ToString().Trim();
                        }
                    }
                }
                ConversionCheck = false;
            }

        }

        void adminSvc_CurrenciesGetCompleted(object sender, CurrenciesGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Currencies");

            if ((AllCheckBox.IsChecked.Value == true) || FillConversions || (CurrencyCheckBox.IsChecked.Value == true))
            {
                if (!GetCurrencyDetail)
                {
                    CurrencyGrid.ItemsSource = null;
                    CurrencyCodeTextBox.Text = "";
                    CurrencyTextBox.Text = "";
                    CurrencyActiveCheckBox.IsChecked = false;

                    CurrencyGrid.ItemsSource = dtTemp.DefaultView;

                    if (FillConversions)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            if (!dr["CurrencyCode"].ToString().Trim().Equals("**"))
                            {
                                string temp = dr["CurrencyCode"].ToString().Trim();
                                IsoFromCombo.Items.Add(temp);
                                IsoToCombo.Items.Add(temp);
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        if (!dr["CurrencyCode"].ToString().Trim().Equals("**"))
                        {
                            CurrencyCodeTextBox.Text = dr["CurrencyCode"].ToString().Trim();
                            CurrencyTextBox.Text = dr["Currency"].ToString().Trim();

                            if (dr["IsActive"].ToString().ToLower().Equals("true"))
                            {
                                CurrencyActiveCheckBox.IsChecked = true;
                            }
                            else
                            {
                                CurrencyActiveCheckBox.IsChecked = false;
                            }
                        }
                    }

                    GetCurrencyDetail = false;
                    ConversionCheck = false;
                    FillConversions = false;
                }
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

            if ((AllCheckBox.IsChecked.Value == true) || (CountryCheckBox.IsChecked.Value == true))
            {
                if (!GetCountryDetail)
                {
                    CountryGrid.ItemsSource = null;
                    CountryCodeTextBox.Text = "";
                    CountryNameTextBox.Text = "";
                    CountrySettleDaysTextBox.Text = "";
                    ActiveCheckBox.IsChecked = false;

                    CountryGrid.ItemsSource = dtTemp.DefaultView;
                }
                else
                {
                    if (dtTemp == null)
                    {
                    }
                    else
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            if (!dr["CountryCode"].ToString().Trim().Equals("**"))
                            {
                                CountryCodeTextBox.Text = dr["CountryCode"].ToString().Trim();
                                CountryNameTextBox.Text = dr["Country"].ToString().Trim();
                                CountrySettleDaysTextBox.Text = dr["SettleDays"].ToString().Trim();
                                if (dr["IsActive"].ToString().ToLower().Equals("true"))
                                {
                                    ActiveCheckBox.IsChecked = true;
                                }
                                else
                                {
                                    ActiveCheckBox.IsChecked = false;
                                }
                            }
                        }
                        GetCountryDetail = false;
                    }
                }
            }
        }

        #endregion


        #region SetsCompleted

        void adminSvc_CurrencyConversionSetCompleted(object sender, CurrencyConversionSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " Currency Conversion Set did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;

            FillIsoCombos();
        }

        void adminSvc_CurrencySetCompleted(object sender, CurrencySetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " Currency Set did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;

            CurrencyCancel();
            FillCurrencyGrid();
        }

        void adminSvc_CountrySetCompleted(object sender, CountrySetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " Country Set did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;
            CountryCancel();
            FillCountryGrid();
        }

        #endregion


        private void CountrySaveButton_Click(object sender, RoutedEventArgs e)
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

            string countryCode = CountryCodeTextBox.Text;
            string country = CountryNameTextBox.Text;
            string settleDays = CountrySettleDaysTextBox.Text.ToString();
            bool isActive = ActiveCheckBox.IsChecked.Value;

            adminSvc.CountrySetAsync(countryCode, country, settleDays, isActive, userId, userPwd, bookGroup, functionPath);
            
        }

        private void CountryCancelButton_Click(object sender, RoutedEventArgs e)
        {
            CountryCancel();
        }

        private void CountryCancel()
        {
            CountryCodeTextBox.Text = "";
            CountryNameTextBox.Text = "";
            CountrySettleDaysTextBox.Text = "";
            ActiveCheckBox.IsChecked = false;
        }

        private void CurrencySaveButton_Click(object sender, RoutedEventArgs e)
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

            string currencyIso = CurrencyCodeTextBox.Text;
            string currency = CurrencyTextBox.Text;
            bool isActive = CurrencyActiveCheckBox.IsChecked.Value;

            adminSvc.CurrencySetAsync(currencyIso, currency, isActive, userId, userPwd, bookGroup, functionPath);
            CurrencyCancel();
        }

        private void CurrencyCancelButton_Click(object sender, RoutedEventArgs e)
        {
            CurrencyCancel();
        }

        private void CurrencyCancel()
        {
            CurrencyCodeTextBox.Text = "";
            CurrencyTextBox.Text = "";
            CurrencyActiveCheckBox.IsChecked = false;
        }

        private void CurrencyConversionSaveButton_Click(object sender, RoutedEventArgs e)
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

            string isoFrom = IsoFromCombo.Text;
            string isoTo = IsoToCombo.Text;
            string convertRate = ConversionRateTextBox.Text.ToString();

            adminSvc.CurrencyConversionSetAsync(isoFrom, isoTo, convertRate, userId, userPwd, bookGroup, functionPath);
            ConversionRateTextBox.Text = "";
            IsoFromCombo.Text = "";
            IsoToCombo.Text = "";
        }

        private void CurrencyConversionCancelButton_Click(object sender, RoutedEventArgs e)
        {
            ConversionRateTextBox.Text = "";
            IsoFromCombo.Text = "";
            IsoToCombo.Text = "";
        }

        private void CountryCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CountryCheckClick();
        }
        private void CountryCheckClick()
        {
            FillCountryGrid();

            //CountryCheckBox.IsChecked = true;
            //CurrencyCheckBox.IsChecked = false;
            //ConversionCheckBox.IsChecked = false;
            //AllCheckBox.IsChecked = false;

            CountryMainStackPanel.Visibility = Visibility.Visible;
            
            CurrencyMainStackPanel.Visibility = Visibility.Collapsed;
            
            CurrencyConversionMainStackPanel.Visibility = Visibility.Collapsed;
        }

        private void CurrencyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CurrencyCheckClick();
        }
        
        private void CurrencyCheckClick()
        {
            CurrencyCancel();

            appInfo.ChkBoxUsed = true;

            
            FillCurrencyGrid();

            //CountryCheckBox.IsChecked = false;
            //CurrencyCheckBox.IsChecked = true;
            //ConversionCheckBox.IsChecked = false;
            //AllCheckBox.IsChecked = false;

            CountryMainStackPanel.Visibility = Visibility.Collapsed;
            
            CurrencyMainStackPanel.Visibility = Visibility.Visible;
            
            CurrencyConversionMainStackPanel.Visibility = Visibility.Collapsed;
        }

        private void ConversionCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ConversionCheckClick();
        }
       
        private void ConversionCheckClick()
        {
            //CountryCheckBox.IsChecked = false;
            //CurrencyCheckBox.IsChecked = false;
            //ConversionCheckBox.IsChecked = true;
            //AllCheckBox.IsChecked = false;
            
            FillIsoCombos();

            CountryMainStackPanel.Visibility = Visibility.Collapsed;
            
            CurrencyMainStackPanel.Visibility = Visibility.Collapsed;
            

            CurrencyConversionMainStackPanel.Visibility = Visibility.Visible;
        }

        private void AllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            AllCheckClick();
        }

        private void AllCheckClick()
        {
            FillCountryGrid();
            
            CurrencyCancel();
            FillCurrencyGrid();
            FillIsoCombos();

            //CountryCheckBox.IsChecked = false;
            //CurrencyCheckBox.IsChecked = false;
            //ConversionCheckBox.IsChecked = false;
            //AllCheckBox.IsChecked = true;

            CountryMainStackPanel.Visibility = Visibility.Visible;
            
            CurrencyMainStackPanel.Visibility = Visibility.Visible;
            
            CurrencyConversionMainStackPanel.Visibility = Visibility.Visible;

            CountryCodeTextBox.Text = "";
            CountryNameTextBox.Text = "";
        }

        private void FillCountryGrid()
        {
            CountryGrid.ItemsSource = null;
            CountryGrid.SelectedIndex = -1 ; 

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

        private void FillCurrencyGrid()
        {
            CurrencyCancel();
            CurrencyGrid.SelectedIndex = -1;
            CurrencyGrid.ItemsSource = null;

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
            GetCurrencyDetail = false;
            adminSvc.CurrenciesGetAsync("", userId, userPwd, bookGroup, functionPath);

        }

        private void CountryGrid_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            try
            {
                string countryCode = CountryGrid[CountryGrid.SelectedIndex, 0].Text;
                string country = CountryGrid[CountryGrid.SelectedIndex, 1].Text;

                GetCountryDetail = true;

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
                adminSvc.CountriesGetAsync(countryCode, userId, userPwd, bookGroup, functionPath);

            }
            catch { }

        }

        private void CurrencyGrid_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            try
            {

                string currencyCode = CurrencyGrid[CurrencyGrid.SelectedIndex, 0].Text;
                string currency = CurrencyGrid[CurrencyGrid.SelectedIndex, 1].Text;

                GetCurrencyDetail = true;

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
                adminSvc.CurrenciesGetAsync(currencyCode, userId, userPwd, bookGroup, functionPath);
            }
            catch { }
        }

        private void IsoFromCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            ConversionRateTextBox.Text = "";
            CheckForIsoMatch();
        }

        private void IsoToCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            ConversionRateTextBox.Text = "";
            CheckForIsoMatch();
        }

        private void FillIsoCombos()
        {
            IsoFromCombo.Items.Clear();
            IsoToCombo.Items.Clear();

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
            FillConversions = true;
            adminSvc.CurrenciesGetAsync("", userId, userPwd, bookGroup, functionPath);

        }

        private void CheckForIsoMatch()
        {
            ConversionCheck = true;            
            string fromIso = IsoFromCombo.Text;
            string toIso = IsoToCombo.Text;
            if (fromIso == null)
            { fromIso = ""; }

            if (toIso == null)
            { toIso = ""; }

            if ((!fromIso.Equals("")) && (!toIso.Equals("")))
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
                adminSvc.CurrencyConversionsGetAsync(fromIso, userId, userPwd, bookGroup, functionPath);
            }
        }

    }
}
