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
using C1.Silverlight.DataGrid;
using WorldWideClient.ServiceUserAdmin;
using WorldWideClient.ServiceBooks;


namespace WorldWideClient
{
	public partial class BookGroupToolBar : UserControl
	{
		private bool _showExportCombo;
		private bool _showBusinessDate;
		private bool _showBookCombo;		
		private string _bookGroup;
		private string _functionName;
        private int _parentControlHndle;
        private C1DataGrid _dataGrid;
        
        public DataTable dtBookGroups;        
        
		
		private UserAdminServiceClient userAdminServiceClient;
		private BooksServiceClient bookServiceClient;
		
        public BookGroupToolBar()
        {
            InitializeComponent();
           
            userAdminServiceClient = new UserAdminServiceClient();
            userAdminServiceClient.UserBookGroupsGetCompleted += new EventHandler<UserBookGroupsGetCompletedEventArgs>(userAdminServiceClient_UserBookGroupsGetCompleted);
			
			bookServiceClient = new BooksServiceClient();
            bookServiceClient.BooksGetCompleted += new EventHandler<BooksGetCompletedEventArgs>(bookServiceClient_BooksGetCompleted);       
			
		}

        void bookServiceClient_BooksGetCompleted(object sender, BooksGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            DataTable dtBooks = Functions.ConvertToDataTable(e.Result, "Books");

            foreach (DataRow drTemp in dtBooks.Rows)
            {
                BookCombo.Items.Add(drTemp["Book"].ToString());
            }
        }

        void userAdminServiceClient_UserBookGroupsGetCompleted(object sender, UserBookGroupsGetCompletedEventArgs e)
        {           	
            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            DataTable dtBookGroups = Functions.ConvertToDataTable(e.Result, "UserBookGroups");

            foreach (DataRow drTemp in dtBookGroups.Rows)
            {
                BookGroupCombo.Items.Add(drTemp["BookGroup"].ToString());
            }			
        }

        public C1DataGrid DataGrid
        {
            set
            {
                _dataGrid = value;
                ExportToCombo.ApplyGrid(_dataGrid);
            }
        }

        public int ParentControlHandle
        {
            get
            {
                return _parentControlHndle;
            }

            set
            {
                _parentControlHndle = value;
            }
        }

        public string BusinessDate
		{
			get
			{
				return BusinessDatePicker.DateTime.Value.ToString("yyyy-MM-dd");
			}
		}
		
		public string BookGroup
		{
			get
			{
				return BookGroupCombo.Text;
			}
		}
		
		public string Book
		{
			get
			{
				return BookCombo.Text;
			}
		}
			
		public  bool ShowBusinessDate
        {
            set
            {
                _showBusinessDate = value;

                if (_showBusinessDate)
                {
                    BusinessDateLabel.Visibility = Visibility.Visible;
                    BusinessDatePicker.Visibility = Visibility.Visible;
                    SeperatorLabel_1.Visibility = Visibility.Visible;  
                }
                else
                {
                    BusinessDateLabel.Visibility = Visibility.Collapsed;
                    BusinessDatePicker.Visibility = Visibility.Collapsed;
                    SeperatorLabel_1.Visibility = Visibility.Collapsed; 
                }
            }
        }
		
		
		
		public bool ShowExportToCombo
		{
			set
			{
				_showExportCombo = value;
				
				if (_showExportCombo)
				{
					ExportToCombo.Visibility = Visibility.Visible;
				}
				else
				{
					ExportToCombo.Visibility = Visibility.Collapsed;
				}
			}
			
		}
		
		public bool ShowBookCombo
		{
			set
			{
				_showBookCombo = value;
				
				if (_showBookCombo)
				{
					BookLabel.Visibility = Visibility.Visible;
					BookCombo.Visibility = Visibility.Visible;
                    BookDetailButton.Visibility = Visibility.Visible;
                    SeperatorLabel_2.Visibility = Visibility.Visible;
				}
				else
				{
					BookLabel.Visibility = Visibility.Collapsed;
					BookCombo.Visibility = Visibility.Collapsed;
                    BookDetailButton.Visibility = Visibility.Collapsed;
                    SeperatorLabel_2.Visibility = Visibility.Collapsed;
				}
			}
		}
		
		public string FunctionName
		{
			set
			{
				_functionName = value;
				userAdminServiceClient.UserBookGroupsGetAsync(UserInformation.UserId, _functionName);		
			}
		}
		
		public string BookGroupSelected
		{
			get
			{
				return BookGroupCombo.Text;				
			}
		}
		
		public string DateTimeSelected
		{
			get
			{
				return BusinessDatePicker.DateTime.ToString();
			}
		}

		private void BusinessDatePicker_DateTimeChanged(object sender, C1.Silverlight.DateTimeEditors.NullablePropertyChangedEventArgs<System.DateTime> e)
		{
            string _bookGroup;
			
            if (BookGroupCombo.Text != null)
            {
                _bookGroup = BookGroupCombo.Text;
            }
            else
            {
                _bookGroup = "";
            }

			CustomEvents.UpdateToolBarChangeInformation(BusinessDatePicker.DateTime.ToString(), _bookGroup, "", _parentControlHndle);
		}

        private void BookGroupCombo_SelectedValueChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
        {
            CustomEvents.UpdateToolBarChangeInformation(BusinessDatePicker.DateTime.ToString(), BookGroupCombo.Text, "", _parentControlHndle);

            if (_showBookCombo)
            {
                bookServiceClient.BooksGetAsync(BookGroupCombo.Text, "", UserInformation.UserId, UserInformation.Password, _functionName);
            }
        }

        private void BookDetailButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (BookGroupCombo.Text != null && BookCombo.Text != null)
            {
                if (!BookGroupCombo.Text.Equals("") && !BookCombo.Text.Equals(""))
                {
                    ClientWindow cntWindow = new ClientWindow();
                    cntWindow.Content = new PositionBooksDetailControl(BookGroupCombo.Text, BookCombo.Text);
                    cntWindow.Show();
                    cntWindow.CenterOnScreen();
                }
            }
        }
	}
}