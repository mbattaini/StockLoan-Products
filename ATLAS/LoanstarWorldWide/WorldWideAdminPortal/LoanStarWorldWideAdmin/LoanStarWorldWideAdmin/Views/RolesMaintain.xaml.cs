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
using LoanStarWorldWideAdmin.SVR_UserAdminService;
using LoanStarWorldWideAdmin.SVR_FunctionsService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class RolesMaintain : UserControl
    {
        public BooksServiceClient booksSvc = new BooksServiceClient();
        public UserAdminServiceClient adminSvc = new UserAdminServiceClient();
        public FunctionsServiceClient functionsSvc = new FunctionsServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();
        
        public RolesMaintain()
        {
            InitializeComponent();

            try
            {
                functions.SetFunctionPath(this.Name.Trim());

                this.Visibility = Visibility.Visible;

                StatusLabel.Visibility = Visibility.Collapsed;

                booksSvc.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(booksSvc_BookGroupsGetAllCompleted);
                booksSvc.BookGroupSetCompleted += new EventHandler<BookGroupSetCompletedEventArgs>(booksSvc_BookGroupSetCompleted);

                //booksSvc.BooksGetCompleted += new EventHandler<BooksGetCompletedEventArgs>(booksSvc_BooksGetCompleted);

                //functionsSvc.TimeZonesGetCompleted += new EventHandler<TimeZonesGetCompletedEventArgs>(functionsSvc_TimeZonesGetCompleted);
                adminSvc.RolesGetCompleted += new EventHandler<RolesGetCompletedEventArgs>(adminSvc_RolesGetCompleted);
                adminSvc.RoleSetCompleted += new EventHandler<RoleSetCompletedEventArgs>(adminSvc_RoleSetCompleted);

                adminSvc.FunctionsGetCompleted += new EventHandler<FunctionsGetCompletedEventArgs>(adminSvc_FunctionsGetCompleted);
                adminSvc.FunctionSetCompleted += new EventHandler<FunctionSetCompletedEventArgs>(adminSvc_FunctionSetCompleted);

                adminSvc.RoleFunctionsGetCompleted += new EventHandler<RoleFunctionsGetCompletedEventArgs>(adminSvc_RoleFunctionsGetCompleted);
                adminSvc.RoleFunctionSetCompleted += new EventHandler<RoleFunctionSetCompletedEventArgs>(adminSvc_RoleFunctionSetCompleted);

                adminSvc.RoleFunctionsBookGroupGetCompleted += new EventHandler<RoleFunctionsBookGroupGetCompletedEventArgs>(adminSvc_RoleFunctionsBookGroupGetCompleted);
                adminSvc.RoleFunctionsBookGroupSetCompleted += new EventHandler<RoleFunctionsBookGroupSetCompletedEventArgs>(adminSvc_RoleFunctionsBookGroupSetCompleted);

                appInfo.UserId = ((App)App.Current).UserId;
                appInfo.Password = ((App)App.Current).Password;
                appInfo.FunctionPath = ((App)App.Current).FunctionPath;
                appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

                FillBookGroupCombo();

                //BookGroupGrid.Visibility = Visibility.Collapsed;
                //RoleAddGrid.Visibility = Visibility.Collapsed;
                //FunctionAddGrid.Visibility = Visibility.Collapsed;

                StatusLabel.Visibility = Visibility.Collapsed;
                AssignButton.IsEnabled = false;

            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

#region SetsCompleted
        
        void booksSvc_BookGroupSetCompleted(object sender, BookGroupSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " BookGroup Set did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;

            FillBookGroupCombo();
        }

        void adminSvc_RoleSetCompleted(object sender, RoleSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " Role Set Up did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;
        }

        void adminSvc_FunctionSetCompleted(object sender, FunctionSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " Function Set up did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;
        }

        void adminSvc_RoleFunctionSetCompleted(object sender, RoleFunctionSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " RoleFunctionsBookGroup Set did not complete successfully. Please check information and try again";

                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;
            SetRoleFunctionBookGroup();
        }

        void adminSvc_RoleFunctionsBookGroupSetCompleted(object sender, RoleFunctionsBookGroupSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " RoleFunctionsBookGroup Set did not complete successfully. Please check information and try again";

                return;
            }

            UpdateLabel.Visibility = Visibility.Collapsed;

            FillRoleFunctionsBookGroup();
        }

#endregion 


#region GetsCompleted

        void booksSvc_BookGroupsGetAllCompleted(object sender, BookGroupsGetAllCompletedEventArgs e)
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

        }

        //void functionsSvc_TimeZonesGetCompleted(object sender, TimeZonesGetCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        return;
        //    }

        //    if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
        //    {
        //        return;
        //    }
        //    DataTable dtTemp = new DataTable();
        //    dtTemp = Functions.ConvertToDataTable(e.Result, "TimeZones");

        //    if (dtTemp == null)
        //    {
        //        StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
        //        StatusLabel.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        foreach (DataRow dr in dtTemp.Rows)
        //        {
        //            string temp = dr["TimeZoneName"].ToString() + " |    " + dr["TimeZoneId"];

        //            TimeZoneCombo.Items.Add(temp);

        //        }
        //    }

        //}

        //void booksSvc_BooksGetCompleted(object sender, BooksGetCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        return;
        //    }

        //    if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
        //    {
        //        return;
        //    }
        //    DataTable dtTemp = new DataTable();
        //    dtTemp = Functions.ConvertToDataTable(e.Result, "Books");

        //    if (dtTemp == null)
        //    {
        //        StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
        //        StatusLabel.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        foreach (DataRow dr in dtTemp.Rows)
        //        {
        //            string temp = dr["BookName"].ToString();
        //            BookCombo.Items.Add(temp);
        //        }
        //    }

        //}

        void adminSvc_RolesGetCompleted(object sender, RolesGetCompletedEventArgs e)
        {
            RoleCombo.Items.Clear();
            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "RoleNames");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["RoleName"].ToString();
                    RoleCombo.Items.Add(temp);
                }
            }
        }

        void adminSvc_FunctionsGetCompleted(object sender, FunctionsGetCompletedEventArgs e)
        {
            FunctionCombo.Items.Clear();

            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Functions");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["FunctionPath"].ToString();
                    FunctionCombo.Items.Add(temp);
                }
            }
        }

        void adminSvc_RoleFunctionsGetCompleted(object sender, RoleFunctionsGetCompletedEventArgs e)
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
            dtTemp = Functions.ConvertToDataTable(e.Result, "RoleFunctions");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["FunctionPath"].ToString();
                    FunctionCombo.Items.Add(temp);
                }
            }
        }

        void adminSvc_RoleFunctionsBookGroupGetCompleted(object sender, RoleFunctionsBookGroupGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            if (StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                DataTable dtTemp = new DataTable();
                dtTemp = Functions.ConvertToDataTable(e.Result, "RoleFunctionsBookGroup");

                if (dtTemp == null)
                {
                }
                else
                {
                    BookGroupRolesGrid.ItemsSource = dtTemp.DefaultView;
                }
            }
        }

#endregion

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

        private void FillRoleCombo()
        {
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
            adminSvc.RolesGetAsync("", userId, userPwd, appInfo.DefaultBookGroup, functionPath);
        }

        private void FillFunctionCombo()
        {
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

            string roleName = RoleCombo.Text;

            adminSvc.FunctionsGetAsync("", bookGroup, functionPath, userId, userPwd);
        }

        private void FillRoleFunctionsBookGroup()
        {   
            
            RoleCombo.Visibility = Visibility.Visible;

            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            if ((bookGroup == null) || (bookGroup.Equals("")))
            {
                bookGroup = appInfo.DefaultBookGroup;
                appInfo.LoggedOnBookGroup = bookGroup;
            }

            if (FunctionCombo.Text == null)
            {
                if ((functionPath == null) || (functionPath.Equals("")))
                {
                    functionPath = appInfo.DefaultFunctionPath;
                    appInfo.FunctionPath = functionPath;
                }
            }
            else
            {
            functionPath = FunctionCombo.Text;
            }

            string bookGroupGet = BookGroupCombo.SelectedItem.ToString();
            string userIdGet = "";

            adminSvc.RoleFunctionsBookGroupGetAsync(userIdGet, bookGroupGet, appInfo.DefaultBookGroup, appInfo.DefaultFunctionPath, userId, userPwd);

        }


        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {

            SetRoleFunction();

        }

        private void SetRoleFunction()
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

            if (FunctionCombo.Text == null)
            {
                if ((functionPath == null) || (functionPath.Equals("")))
                {
                    functionPath = appInfo.DefaultFunctionPath;
                    appInfo.FunctionPath = functionPath;
                }
            }

            string bookGroupSet = BookGroupCombo.Text;
            string roleNameSet = RoleCombo.Text;
            string functionPathSet = FunctionCombo.Text;

            adminSvc.RoleFunctionSetAsync(roleNameSet, functionPathSet, false, false, "", false, userId, userPwd, bookGroup, functionPath);

        }

        private void SetRoleFunctionBookGroup()
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

            if (FunctionCombo.Text == null)
            {
                if ((functionPath == null) || (functionPath.Equals("")))
                {
                    functionPath = appInfo.DefaultFunctionPath;
                    appInfo.FunctionPath = functionPath;
                }
            }

            string bookGroupSet = BookGroupCombo.Text;
            string roleNameSet = RoleCombo.Text;
            string functionPathSet = FunctionCombo.Text;

            adminSvc.RoleFunctionsBookGroupSetAsync(roleNameSet, functionPathSet, bookGroupSet, userId, 1, 0, bookGroup, functionPath, userId, userPwd);

        }

        private void BookGroupCombo_SelectedItemChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            AssignButton.IsEnabled = false;

            FillRoleCombo();
            FillRoleFunctionsBookGroup();

        }

        private void RoleCombo_SelectedItemChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            AssignButton.IsEnabled = false;

            FillFunctionCombo();
        }

        private void BookGroupRolesGrid_DeletingRows(object sender, C1.Silverlight.DataGrid.DataGridDeletingRowsEventArgs e)
        {
            bool deletingAccepted = false;
            if (!deletingAccepted)
            {
                int bookGrpCol = 0;
                int roleCol = 1;
                int functionCol = 2;
                int currRow = 0;

                string uid = "";
                string bookGrp = "";
                string role = "";
                string functn = "";
                string msg = "";

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

                currRow = BookGroupRolesGrid.CurrentRow.Index;

                bookGrp = BookGroupRolesGrid[currRow, bookGrpCol].Text.Trim();
                role = BookGroupRolesGrid[currRow, roleCol].Text.Trim();
                functn = BookGroupRolesGrid[currRow, functionCol].Text.Trim();

                msg = "You have pressed Delete. This will delete the " + functn.Trim() + " function for the " + role.Trim() +
                    " Role from the " + bookGrp.Trim() + " Book Group. Are you sure this is what you want to do?";
                e.Cancel = true;
                C1MessageBox.Show(msg, "DELETE NOTICE", C1MessageBoxButton.YesNo, C1MessageBoxIcon.Question, (result) =>
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        adminSvc.RoleFunctionsBookGroupSetAsync(role, functn, bookGrp, userId, 1, 1, bookGroup, functionPath, userId, userPwd);
                        deletingAccepted = true;
                        deletingAccepted = false;
                    }
                });
            }

        }

        private void AddNewBookGroupButton_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            C1Window win = new C1Window();

            win.CenterOnScreen();
            win.Content = new BookGroupAddWindow();

            win.ShowModal();
            win.Closed += new EventHandler(bookGroupWin_Closed);
        }

        void bookGroupWin_Closed(object sender, EventArgs e)
        {
            FillBookGroupCombo();
        }

        private void AddNewRoleButton_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            C1Window win = new C1Window();

            win.CenterOnScreen();
            win.Content = new RoleAddWindow();

            win.ShowModal();
            win.Closed +=new EventHandler(RoleWin_Closed);
        }

        void RoleWin_Closed(object sender, EventArgs e)
        {
            FillRoleCombo();
        }

        private void AddNewFunctionButton_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            C1Window win = new C1Window();

            win.CenterOnScreen();
            win.Content = new FunctionAddWindow();

            win.ShowModal();
            win.Closed += new EventHandler(FunctionWin_Closed);
        }

        void FunctionWin_Closed(object sender, EventArgs e)
        {
            FillFunctionCombo();
        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
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

            //string roleNameSet = RoleNameTextBox.Text;
            //string roleComment = RoleCommentTextBox.Text;

            bool delInd = false;

            //adminSvc.RoleSetAsync("", roleNameSet, roleComment, delInd, userId, userPwd, bookGroup, functionPath);
            //FillRoleCombo();

            FillRoleFunctionsBookGroup();

            //RoleAddGrid.Visibility = Visibility.Collapsed;
        }
        
        private void AddFunctionButton_Click(object sender, RoutedEventArgs e)
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
                
            //string functionPathSet = FunctionPathTextBox.Text;
            
            //bool mayView = bool.Parse(MayViewCheckBox.IsChecked.ToString());
            //bool mayEdit = bool.Parse(MayEditCheckBox.IsChecked.ToString()); 

            //adminSvc.FunctionSetAsync(functionPathSet, mayView, mayEdit, bookGroup, functionPath, userId, userPwd);

            int cnt = FunctionCombo.Items.Count; 

            FunctionCombo.Items.Clear();
            
            FillFunctionCombo();
            //FunctionAddGrid.Visibility = Visibility.Collapsed;
        }
        
        private void CancelRoleButton_Click(object sender, RoutedEventArgs e)
        {
            //RoleAddGrid.Visibility = Visibility.Collapsed;
            //FunctionAddGrid.Visibility = Visibility.Collapsed;
        }

        private void CancelFuncButton_Click(object sender, RoutedEventArgs e)
        {
            //RoleAddGrid.Visibility = Visibility.Collapsed;
            //FunctionAddGrid.Visibility = Visibility.Collapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //BookGroupGrid.Visibility = Visibility.Collapsed;
        }

        private void FunctionCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            AssignButton.IsEnabled = true;
        }

        List<C1Window> windows = new List<C1Window>();
        public void Dispose()
        {
            foreach (var w in windows)
            {
                w.Close();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //BookGroupCombo.SelectedIndex = -1;
            RoleCombo.SelectedIndex = -1;
            FunctionCombo.SelectedIndex = -1;
            BookGroupRolesGrid.ItemsSource = "";
        }

    }
}