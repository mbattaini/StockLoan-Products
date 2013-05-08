using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;

using WorldWideClient.ServiceBooks;

namespace WorldWideClient
{
	public partial class PositionBooksDetailControl : UserControl
	{
		private string bookGroup;
		private string book;

        private DataTable dtBook;

		private BooksServiceClient booksClient;

        public PositionBooksDetailControl(string bookGroup, string book)
        {
            InitializeComponent();

            this.bookGroup = bookGroup;
            this.book = book;

            booksClient = new BooksServiceClient();
            booksClient.BooksGetCompleted += new EventHandler<BooksGetCompletedEventArgs>(booksClient_BooksGetCompleted);
            booksClient.BookCreditLimitsGetCompleted += new EventHandler<BookCreditLimitsGetCompletedEventArgs>(booksClient_BookCreditLimitsGetCompleted);

            booksClient.BooksGetAsync(bookGroup, book, UserInformation.UserId, UserInformation.Password, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);
        }

        void booksClient_BooksGetCompleted(object sender, BooksGetCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    SystemEventWindow.Show(e.Error.InnerException.InnerException.Message);
                }
                else
                {
                    dtBook = Functions.ConvertToDataTable(e.Result, "Books");

                    if (dtBook.Rows.Count == 1)
                    {
                        BookTextBox.Text = book;
                        BookParentTextBox.Text = dtBook.Rows[0]["BookParent"].ToString();
                        BookNameTextBox.Text = dtBook.Rows[0]["BookName"].ToString();
                        BookAddress1TextBox.Text = dtBook.Rows[0]["AddressLine1"].ToString();
                        BookAddress2TextBox.Text = dtBook.Rows[0]["AddressLine2"].ToString();
                        BookAddress3TextBox.Text = dtBook.Rows[0]["AddressLine3"].ToString();
                        StockHouseBorrowRateTexBox.Text = dtBook.Rows[0]["RateStockBorrow"].ToString();
                        StockHouseLoanRateTexBox.Text = dtBook.Rows[0]["RateStockLoan"].ToString();
                        BondHouseBorrowRateTexBox.Text = dtBook.Rows[0]["RateBondBorrow"].ToString();
                        BondHouseLoanRateTexBox.Text = dtBook.Rows[0]["RateBondLoan"].ToString();
                        MarginBorrowTextBox.Text = dtBook.Rows[0]["MarginBorrow"].ToString();
                        MarginLoanTextBox.Text = dtBook.Rows[0]["MarginLoan"].ToString();
                    }
                }
            }
            catch (FaultException<Exception> faultException)
            {
                SystemEventWindow.Show(faultException.Message);
            }            
        }

        void booksClient_BookCreditLimitsGetCompleted(object sender, BookCreditLimitsGetCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
	}
}