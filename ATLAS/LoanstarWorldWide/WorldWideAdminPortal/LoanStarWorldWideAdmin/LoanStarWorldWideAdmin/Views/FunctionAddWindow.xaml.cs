using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using C1.Silverlight;

using LoanStarWorldWideAdmin;
using LoanStarWorldWideAdmin.SVR_UserAdminService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class FunctionAddWindow : UserControl, IDisposable
    {
        public UserAdminServiceClient userAdminSvc = new UserAdminServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();


        public FunctionAddWindow()
        {
            InitializeComponent();

            userAdminSvc.FunctionSetCompleted += new EventHandler<FunctionSetCompletedEventArgs>(userAdminSvc_FunctionSetCompleted);

        }

        void userAdminSvc_FunctionSetCompleted(object sender, FunctionSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                AddFunctionButton.Visibility = Visibility.Visible;
                statusLabel.Content = "Function Save Failed";
                return;
            }
            else
            {
                AddFunctionButton.Visibility = Visibility.Collapsed;
                statusLabel.Content = "Successfully Saved Function";
                return;
            }
        }

        #region ** properties

        public string DEMO_Content { get; set; }
        public string DEMO_Header { get; set; }
        public int DEMO_Width { get; set; }
        public int DEMO_Height { get; set; }
        public int DEMO_Left { get; set; }
        public int DEMO_Top { get; set; }
        public bool CenterOnScreen { get; set; }

        #endregion

        List<C1Window> windows = new List<C1Window>();
        public void Dispose()
        {
            foreach (var w in windows)
            {
                w.Close();
            }
        }

        private void AddFunctionButton_Click(object sender, RoutedEventArgs e)
        {
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

            string functionPathSet = FunctionPathTextBox.Text;

            bool mayView = bool.Parse(MayViewCheckBox.IsChecked.ToString());
            bool mayEdit = bool.Parse(MayEditCheckBox.IsChecked.ToString()); 

            userAdminSvc.FunctionSetAsync(functionPathSet, mayView, mayEdit, bookGroup, functionPath, userId, userPwd);

        }
    }
}
