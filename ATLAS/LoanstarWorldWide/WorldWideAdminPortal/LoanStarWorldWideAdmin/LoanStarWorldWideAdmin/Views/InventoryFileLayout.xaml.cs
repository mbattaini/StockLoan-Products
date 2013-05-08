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
using C1.Silverlight;
using C1.Silverlight.Data;

using LoanStarWorldWideAdmin.SVR_InventoryService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class InventoryFileLayout : Page
    {
        public InventoryServiceClient inventorySvc = new InventoryServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        public InventoryFileLayout()
        {
            InitializeComponent();
            inventorySvc.InventoryFileLayoutGetCompleted += new EventHandler<InventoryFileLayoutGetCompletedEventArgs>(inventorySvc_InventoryFileLayoutGetCompleted);
            inventorySvc.InventoryFileLayoutSetCompleted += new EventHandler<InventoryFileLayoutSetCompletedEventArgs>(inventorySvc_InventoryFileLayoutSetCompleted);

            appInfo.UserId = ((App)App.Current).UserId;
            appInfo.Password = ((App)App.Current).Password;
            appInfo.FunctionPath = ((App)App.Current).FunctionPath;
            appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;
            appInfo.SelectedBookGroup = ((App)App.Current).SelectedBookGroup;
            appInfo.CurrentDesk = ((App)App.Current).CurrentDesk;
            appInfo.InventoryType = ((App)App.Current).InventoryType;

            string desk = appInfo.CurrentDesk;
            string userId = appInfo.UserId;
            string userPwd = appInfo.Password;
            string inventoryType = appInfo.InventoryType;
            string bookGroup = appInfo.LoggedOnBookGroup;
            string bookGroupGet = appInfo.SelectedBookGroup;

            functions.SetFunctionPath(this.Name.Trim());

            StatusLabel.Visibility = Visibility.Collapsed;

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

            inventorySvc.InventoryFileLayoutGetAsync(bookGroupGet, desk, inventoryType, bookGroup, userId, userPwd, functionPath);            

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        void inventorySvc_InventoryFileLayoutGetCompleted(object sender, InventoryFileLayoutGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "InventoryFileLayouts");
            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {
                string actor = "";
                string actTime = "";
                string updateMessage = "";
                string inventoryType = "";

                if (dtTemp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        BookGroupTextBox.Text = dr["BookGroup"].ToString();
                        DeskTextBox.Text = dr["Desk"].ToString();
                        if (dr["InventoryType"].ToString().Equals("I"))
                        {
                            inventoryType = "I:  Inventory File";
                        }
                        else
                        {
                            inventoryType = "R:  Rate File";
                        }
                        InventoryTypeLabel.Content = inventoryType;

                        HeaderTextBox.Text = dr["HeaderFlag"].ToString();
                        DataFlagTextBox.Text = dr["DataFlag"].ToString();
                        TrailerFlagTextBox.Text = dr["TrailerFlag"].ToString();
                        DelimiterBox.Text = dr["Delimiter"].ToString();
                        RecordLengthText.Text = dr["RecordLength"].ToString();
                        LocaleText.Text = dr["AccountLocale"].ToString();
                        AccountStartText.Text = dr["AccountPosition"].ToString();
                        AccountLengthText.Text = dr["AccountLength"].ToString();
                        AccountOrdinalText.Text = dr["AccountOrdinal"].ToString();

                        SecurityStartText.Text = dr["SecIdPosition"].ToString();
                        SecurityLengthText.Text = dr["SecIdLength"].ToString();
                        SecurityOrdinalText.Text = dr["SecIdOrdinal"].ToString();

                        QuantityStartText.Text = dr["QuantityPosition"].ToString();
                        QuantityLengthText.Text = dr["QuantityLength"].ToString();
                        QuantityOrdinalText.Text = dr["QuantityOrdinal"].ToString();

                        RateStartText.Text = dr["RatePosition"].ToString();
                        RateLengthText.Text = dr["RateLength"].ToString();
                        RateOrdinalText.Text = dr["RateOrdinal"].ToString();

                        RecordCountStartText.Text = dr["RecordCountPosition"].ToString();
                        RecordCountLengthText.Text = dr["RecordCountLength"].ToString();
                        RecordCountOrdinalText.Text = dr["RecordCountOrdinal"].ToString();

                        DateDDText.Text = dr["BizDateDD"].ToString();
                        DateMMTextBox.Text = dr["BizDateMM"].ToString();
                        DateYYText.Text = dr["BizDateYY"].ToString();

                        actor = dr["Actor"].ToString();
                        actTime = dr["ActTime"].ToString();

                        updateMessage = " by: " + actor + " - " + actTime;

                        MessageLabel.Content = updateMessage;

                    }
                }
                else
                {
                    BookGroupTextBox.Text = appInfo.SelectedBookGroup;
                    DeskTextBox.Text = appInfo.CurrentDesk;
                    if (appInfo.SubscriptionUpdatesReq)
                    {
                    }
                }
                inventoryType = appInfo.InventoryType;

                if (inventoryType.Equals("I"))
                {
                    InventoryTypeLabel.Content = "I:  Inventory File";
                }
                else
                {
                    InventoryTypeLabel.Content = "R:  Rate File";
                }
                appInfo.LayoutsLoaded = true;
            }
        }

        void inventorySvc_InventoryFileLayoutSetCompleted(object sender, InventoryFileLayoutSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            appInfo.LayoutsValid = true;
            SaveButton.Visibility = Visibility.Collapsed;

        }
        private bool ValidateLayouts()
        {
            bool isDelimited = false;

            try
            {
                if (HeaderTextBox.Text.Trim().Equals("") || DataFlagTextBox.Text.Trim().Equals("") ||
                        TrailerFlagTextBox.Text.Trim().Equals("") || DelimiterBox.Text.Trim().Equals(""))
                {
                    isDelimited = false;
                }
                else
                { isDelimited = true; }

                if (isDelimited)
                {
                    return true;
                }
                if (!DateDDText.Text.Trim().Equals("")  && (!DateMMTextBox.Text.Trim().Equals("")) & (!DateYYText.Text.Trim().Equals("")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            { 
                return false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateLayouts();
            string temp = InventoryTypeLabel.Content.ToString();
 
            string inventoryType = temp.Substring(0, 1);
            string userId = appInfo.UserId;
            string userPassword = appInfo.Password;
            string functionPath = appInfo.FunctionPath;
            string bookGroup = appInfo.LoggedOnBookGroup;
            string bookGroupGet = appInfo.SelectedBookGroup;

            if ((functionPath == null) || (functionPath.Equals("")))
            {
                functionPath = appInfo.DefaultFunctionPath;
                appInfo.FunctionPath = functionPath;
            }

            inventorySvc.InventoryFileLayoutSetAsync(bookGroupGet, DeskTextBox.Text.Trim(), inventoryType,
                RecordLengthText.Text.Trim(), HeaderTextBox.Text.Trim(), DataFlagTextBox.Text.Trim(), TrailerFlagTextBox.Text.Trim(),
                DelimiterBox.Text.Trim(), LocaleText.Text.Trim(),
                AccountOrdinalText.Text.Trim(), AccountStartText.Text.Trim(), AccountLengthText.Text.Trim(),
                SecurityOrdinalText.Text.Trim(), SecurityStartText.Text.Trim(), SecurityLengthText.Text.Trim(),
                QuantityOrdinalText.Text.Trim(), QuantityStartText.Text.Trim(), QuantityLengthText.Text.Trim(),
                RateOrdinalText.Text.Trim(), RateStartText.Text.Trim(), RateLengthText.Text.Trim(),
                RecordCountOrdinalText.Text.Trim(), RecordCountStartText.Text.Trim(), RecordCountLengthText.Text.Trim(),
                DateDDText.Text.Trim(), DateMMTextBox.Text.Trim(), DateYYText.Text.Trim(), userId, bookGroup, userId, userPassword, functionPath);


        }


    }
}
