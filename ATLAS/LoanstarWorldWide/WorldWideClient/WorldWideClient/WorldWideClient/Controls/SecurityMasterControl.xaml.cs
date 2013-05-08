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
using C1.Silverlight.Chart;

using WorldWideClient.ServiceSecMaster;
using WorldWideClient.ServiceUserAdmin;
using WorldWideClient.ServicePositions;

namespace WorldWideClient
{
	public partial class SecurityMasterControl : UserControl
	{
		public bool isMinimized = false;
        private SecMasterServiceClient secMasterClient;
        private UserAdminServiceClient userAdminClient;
        private PositionsServiceClient positionClient;

		public SecurityMasterControl()
		{
			// Required to initialize variables
			InitializeComponent();
            secMasterClient = new SecMasterServiceClient();
            userAdminClient = new UserAdminServiceClient();
            positionClient = new PositionsServiceClient();

            secMasterClient.SecMasterGetCompleted += new EventHandler<SecMasterGetCompletedEventArgs>(secMasterClient_SecMasterGetCompleted);
            userAdminClient.UserBookGroupsGetCompleted += new EventHandler<UserBookGroupsGetCompletedEventArgs>(userAdminClient_UserBookGroupsGetCompleted);
            positionClient.BoxPositionGetCompleted += new EventHandler<BoxPositionGetCompletedEventArgs>(positionClient_BoxPositionGetCompleted);

            userAdminClient.UserBookGroupsGetAsync(UserInformation.UserId, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);            
        }

        void positionClient_BoxPositionGetCompleted(object sender, BoxPositionGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            DataTable dtBoxPosition = Functions.ConvertToDataTable(e.Result, "BoxPosition");

            if (dtBoxPosition.Rows.Count > 0)
                PopulateBoxPosition(dtBoxPosition.Rows[0]);
        }

        void PopulateBoxPosition(DataRow dr)
        {
            BorrowSettledTextBox.Text = long.Parse(dr["Borrow"].ToString()).ToString("#,##0");
            LoanSettledTextBox.Text = long.Parse(dr["Loan"].ToString()).ToString("#,##0");
            
			LongSettledTextBox.Text = long.Parse(dr["LongsSettled"].ToString()).ToString("#,##0");
            LongTradedTextBox.Text = long.Parse(dr["LongsTraded"].ToString()).ToString("#,##0");
			
			ShortSettledTextBox.Text = long.Parse(dr["ShortsSettled"].ToString()).ToString("#,##0");
            ShortTradedTextBox.Text = long.Parse(dr["ShortsTraded"].ToString()).ToString("#,##0");
			
			PledgeSettledTextBox.Text = long.Parse(dr["PledgeSettled"].ToString()).ToString("#,##0");
			PledgeTradedTextBox.Text = long.Parse(dr["PledgeTraded"].ToString()).ToString("#,##0");
			
            SegSettledTextBox.Text = long.Parse(dr["SegReqSettled"].ToString()).ToString("#,##0");
            SegTradedTextBox.Text = long.Parse(dr["SegReqSettled"].ToString()).ToString("#,##0");
			
			ExcessSettledTextBox.Text = long.Parse(dr["ExDeficitSettled"].ToString()).ToString("#,##0");
            ExcessTradedTextBox.Text = long.Parse(dr["ExDeficitTraded"].ToString()).ToString("#,##0");
			
			ClearingFTDSettledTextBox.Text = long.Parse(dr["ClearingFailOutSettled"].ToString()).ToString("#,##0");
            ClearingFTRSettledTextBox.Text = long.Parse(dr["ClearingFailInSettled"].ToString()).ToString("#,##0");            
			ClearingFTDTradedTextBox.Text = long.Parse(dr["ClearingFailOutTraded"].ToString()).ToString("#,##0");
            ClearingFTRTradedTextBox.Text = long.Parse(dr["ClearingFailInTraded"].ToString()).ToString("#,##0");
            ClearingFTDDayCount.Text = long.Parse(dr["ClearingFailInDayCount"].ToString()).ToString("#,##0");
			ClearingFTRDayCount.Text = long.Parse(dr["ClearingFailOutDayCount"].ToString()).ToString("#,##0");
			
			DvpFTDSettledTextBox.Text = long.Parse(dr["DvpFailOutSettled"].ToString()).ToString("#,##0");
            DvpFTRSettledTextBox.Text = long.Parse(dr["DvpFailInSettled"].ToString()).ToString("#,##0");
            DvpFTDTradedTextBox.Text = long.Parse(dr["DvpFailOutTraded"].ToString()).ToString("#,##0");
            DvpFTRTradedTextBox.Text = long.Parse(dr["DvpFailInTraded"].ToString()).ToString("#,##0");
			DvpFTDDayCount.Text = long.Parse(dr["DvpFailInDayCount"].ToString()).ToString("#,##0");
			DvpFTRDayCount.Text = long.Parse(dr["DvpFailOutDayCount"].ToString()).ToString("#,##0");
			
			BrokerFTDSettledTextBox.Text = long.Parse(dr["BrokerFailOutSettled"].ToString()).ToString("#,##0");
            BrokerFTRSettledTextBox.Text = long.Parse(dr["BrokerFailInSettled"].ToString()).ToString("#,##0");           
			BrokerFTDTradedTextBox.Text = long.Parse(dr["BrokerFailOutTraded"].ToString()).ToString("#,##0");
            BrokerFTRTradedTextBox.Text = long.Parse(dr["BrokerFailInTraded"].ToString()).ToString("#,##0");           
			BrokerFTDDayCount.Text =  long.Parse(dr["BrokerFailInDayCount"].ToString()).ToString("#,##0");
			BrokerFTRDayCount.Text =  long.Parse(dr["BrokerFailOutDayCount"].ToString()).ToString("#,##0");
        }

        void PopulateSecMaster(DataRow dr)
        {
            SecDescTextBox.Text = dr["Description"].ToString();
            IsinTextBox.Text = dr["Isin"].ToString();
            CusipTextBox.Text = dr["Cusip"].ToString();
            SymbolTextBox.Text = dr["Symbol"].ToString();
            PriceTexBox.Text = Decimal.Parse(dr["Price"].ToString()).ToString("#,##0.00") + " | " + DateTime.Parse(dr["PriceDate"].ToString()).ToString("yyyy-MM-dd");
            SecDetailsTextBox.Text = dr["BaseTypeDesc"].ToString() + " | " + dr["ClassGroup"].ToString() + " | " + dr["CountryCode"].ToString();
            RecordDateTextBox.Text = DateTime.Parse(dr["RecordDateCash"].ToString()).ToString("yyyy-MM-dd");
            DivRateTextBox.Text = dr["DividendRate"].ToString();
            BorrowAvgRateTextBox.Text = Decimal.Parse(dr["BorrowRate"].ToString()).ToString("#,##0.000");
            LoanAvgRateTextBox.Text = Decimal.Parse(dr["LoanRate"].ToString()).ToString("#,##0.000");
        }

        void userAdminClient_UserBookGroupsGetCompleted(object sender, UserBookGroupsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            DataTable dtBookGroups = Functions.ConvertToDataTable(e.Result, "UserBookGroups");

            foreach (DataRow drTemp in dtBookGroups.Rows)
            {
                BookGroupComboBox.Items.Add(drTemp["BookGroup"].ToString());
            }
        }

        void secMasterClient_SecMasterGetCompleted(object sender, SecMasterGetCompletedEventArgs e)
        {
            DataTable dtSecMasterItem = new DataTable();
            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            dtSecMasterItem = Functions.ConvertToDataTable(e.Result, "SecMaster");

            if (dtSecMasterItem.Rows.Count > 0)
                PopulateSecMaster(dtSecMasterItem.Rows[0]);
        }
		
		
		public bool IsMinimized
		{
			get
			{
				return isMinimized;
			}
			
			set
			{
				isMinimized = value;
				
				if (!isMinimized)
				{				
					
				}
				else
				{				
				}
			}
		}

        private void FindToolbarButton_Click(object sender, RoutedEventArgs e)
        {
            secMasterClient.SecMasterGetAsync(
                SecurityIdTextBox.Text, 
                "", 
                "", 
                BookGroupComboBox.Text,
                "", 
                UserInformation.UserId, 
                UserInformation.Password, 
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);

            positionClient.BoxPositionGetAsync(
                DateTime.Now.ToString("yyyy-MM-dd"),
                BookGroupComboBox.Text,
                SecurityIdTextBox.Text,
                UserInformation.UserId,
                UserInformation.Password,
                 System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);

        }      
	}
}