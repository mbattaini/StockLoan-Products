using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;
using C1.Silverlight.DataGrid.Filters;

using WorldWideClient.ServiceBooks;
using WorldWideClient.ServiceUserAdmin;

namespace WorldWideClient
{
	public partial class PositionBooksControl : UserControl
	{
		private BooksServiceClient bookServiceClient;
		private UserAdminServiceClient userAdminServiceClient;
		private DataTable dtBooks;
        private int controlHndle;

		public PositionBooksControl()
		{			
			InitializeComponent();
			
			bookServiceClient = new BooksServiceClient();
			userAdminServiceClient = new UserAdminServiceClient();

            controlHndle = Functions.CreateControlHandle();
            
            bookServiceClient.BooksGetCompleted += new EventHandler<BooksGetCompletedEventArgs>(bookServiceClient_BooksGetCompleted);
        			
			booksBookGroupToolBar.FunctionName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name;
            booksBookGroupToolBar.ParentControlHandle = controlHndle;
		
			CustomEvents.ToolBarInformationChanged += new EventHandler<ToolBarChangeEventArgs>(CustomEvents_ToolBarInformationChanged);
		}
		
		
		  void CustomEvents_ToolBarInformationChanged(object sender, ToolBarChangeEventArgs e)
        {
            if ((!e.BizDate.Equals("") && !e.BookGroup.ToString().Equals("")) && (controlHndle == e.ControlHandle))
            {
                bookServiceClient.BooksGetAsync(e.BookGroup, "", UserInformation.UserId, UserInformation.Password, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);
            	BooksGrid.IsLoading = true;
			}
        }


        void bookServiceClient_BooksGetCompleted(object sender, BooksGetCompletedEventArgs e)
        {
            BooksGrid.IsLoading = false;
			
			if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            dtBooks = Functions.ConvertToDataTable(e.Result, "Books");

           BooksGrid.ItemsSource = dtBooks.DefaultView;						
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {       				
			BooksGrid.TopRows.Clear();
			BooksGrid.TopRows.Add(new DataGridFilterRow());			
			BooksGrid.Reload(false);           
        }
	}
}