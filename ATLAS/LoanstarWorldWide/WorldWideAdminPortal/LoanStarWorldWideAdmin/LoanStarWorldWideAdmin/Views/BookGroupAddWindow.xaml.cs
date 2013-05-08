using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using C1.Silverlight.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using C1.Silverlight;

using LoanStarWorldWideAdmin;
using LoanStarWorldWideAdmin.SVR_BooksService;
using LoanStarWorldWideAdmin.SVR_FunctionsService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class BookGroupAddWindow : UserControl, IDisposable
    {
        public BooksServiceClient booksSvc = new BooksServiceClient();
        public FunctionsServiceClient functionsSvc = new FunctionsServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        public BookGroupAddWindow()
        {
            InitializeComponent();

            booksSvc.BooksGetCompleted += new EventHandler<BooksGetCompletedEventArgs>(booksSvc_BooksGetCompleted);
            booksSvc.BookGroupSetCompleted += new EventHandler<BookGroupSetCompletedEventArgs>(booksSvc_BookGroupSetCompleted);

            functionsSvc.TimeZonesGetCompleted += new EventHandler<TimeZonesGetCompletedEventArgs>(functionsSvc_TimeZonesGetCompleted);

        }

        #region ** buttons

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Cancel will lose any changes you may have made. Are you sure this is what you want to do?",
                    "CANCEL NOTICE", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

                this.Dispose();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void BookGroupAddButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = ((App)App.Current).UserId;
            string userPwd = ((App)App.Current).Password;
            string functionPath = ((App)App.Current).FunctionPath;
            string bookGroup = ((App)App.Current).LoggedOnBookGroup;

            string bookGroupSet = "";
            string bookName = "";
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

            if (BookCombo.Text == null)
            {
                return;
            }
            else
            {
                bookGroupSet = NameTextBox.Text;
                bookName = BookCombo.Text;

                bool useWeekends = bool.Parse(UseWeekendsCheckbox.IsChecked.ToString());
                string settlementType = SettlementTypeTextBox.Text;
                string timeZoneName = TimeZoneCombo.Text;
                string id = "";
                int startIndex = 0;
                int timeZoneId = 0;
                int pos = timeZoneName.IndexOf("|", startIndex);
                int len = int.Parse(timeZoneName.Length.ToString());
                if (len > 0)
                {
                    id = timeZoneName.Substring(pos + 2, len - (pos + 2)).Trim();
                    timeZoneId = int.Parse(id);
                }

                booksSvc.BookGroupSetAsync(bookGroupSet, bookName, id, "", "", "", "", "", "", "", "", "", "", useWeekends, settlementType,
                        bookGroup, userId, userPwd, functionPath);

                //BookGroupGrid.Visibility = Visibility.Collapsed;
            
            }


        }


        #endregion

        List<C1Window> windows = new List<C1Window>();
        public void Dispose()
        {
            foreach (var w in windows)
            {
                w.Close();
            }
        }

        private void FillControls(object sender, RoutedEventArgs e)  // On User Control Loaded event
        {
            FillBooksCombo();
            FillTimeZoneCombo();
        }

        private void FillBooksCombo()
        {

            BookCombo.Items.Clear();

            string userId = ((App)App.Current).UserId;
            string userPwd = ((App)App.Current).Password;
            string bookGroup = ((App)App.Current).LoggedOnBookGroup;
            string functionPath = ((App)App.Current).FunctionPath;

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

                booksSvc.BooksGetAsync(appInfo.DefaultBookGroup, "", userId, userPwd, functionPath);

        }

        private void FillTimeZoneCombo()
        {
            TimeZoneCombo.Items.Clear();

            string userId = ((App)App.Current).UserId;
            string userPwd = ((App)App.Current).Password;
            string bookGroup = ((App)App.Current).LoggedOnBookGroup;
            string functionPath = ((App)App.Current).FunctionPath;

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

            functionsSvc.TimeZonesGetAsync("", "", userId, userPwd, bookGroup, functionPath);
        }

        void booksSvc_BooksGetCompleted(object sender, BooksGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Books");

            if (dtTemp == null)
            {

            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookName"].ToString();
                    BookCombo.Items.Add(temp);
                }
            }

        }

        void functionsSvc_TimeZonesGetCompleted(object sender, TimeZonesGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "TimeZones");

            if (dtTemp == null)
            {
                
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["TimeZoneName"].ToString() + " |    " + dr["TimeZoneId"];

                    TimeZoneCombo.Items.Add(temp);

                }
            }

        }

        void booksSvc_BookGroupSetCompleted(object sender, BookGroupSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                BookGroupAddButton.Visibility = Visibility.Visible;
                statusLabel.Content = "Add Book Group Failed";
                return;
            }
            else
            {
                BookGroupAddButton.Visibility = Visibility.Collapsed;
                statusLabel.Content = "Book Group Successfully Added";
                return;
            }
        }

    }
}
