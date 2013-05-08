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
    public partial class RoleAddWindow : UserControl, IDisposable
    {
        public UserAdminServiceClient userAdminSvc = new UserAdminServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        public RoleAddWindow()
        {
            InitializeComponent();

            userAdminSvc.RoleSetCompleted +=new EventHandler<RoleSetCompletedEventArgs>(userAdminSvc_RoleSetCompleted);            
        }

        void  userAdminSvc_RoleSetCompleted(object sender, RoleSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                AddRoleButton.Visibility = Visibility.Visible;
                statusLabel.Content = "Add Role Failed";
                return;
            }
            else
            {
                AddRoleButton.Visibility = Visibility.Collapsed;
                statusLabel.Content = "Role Successfully Added";
                return;
            }

        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = ((App)App.Current).UserId;
            string userPwd = ((App)App.Current).Password;
            string functionPath = ((App)App.Current).FunctionPath;
            string bookGroup = ((App)App.Current).LoggedOnBookGroup;

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

            string roleNameSet = RoleNameTextBox.Text;
            string roleComment = RoleCommentTextBox.Text;

            bool delInd = false;

            userAdminSvc.RoleSetAsync("", roleNameSet, roleComment, delInd, userId, userPwd, bookGroup, functionPath);

        }


        List<C1Window> windows = new List<C1Window>();

        public void Dispose()
        {
            foreach (var w in windows)
            {
                w.Close();
            }
        }
    }
}
