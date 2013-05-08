using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace LocatesClient
{
  public partial class SecMaster : UserControl
  {
    private MainForm mainForm = null;
    private DataSet dsSecMasterItem = null;
    private string secId = "";

    public string SecId
    {

      set
      {
        secId = value;

        SecMasterFill(secId);

        SecIdTextBox.Text = secId;
      }

		get
		{
			return CusipTextBox.Text;
		}
    }
      

    public MainForm LoanstarMainForm
    {
      set
      {
        this.mainForm = value;
      }
    }

    public SecMaster()
    {
      InitializeComponent();
      dsSecMasterItem = new DataSet();
    }   

    public void SecMasterFill(string secId)
    {

      try
      {
        if (!secId.Equals(""))
        {
          dsSecMasterItem = mainForm.TradeAgent.SecMasterItemGet(secId);

          if (dsSecMasterItem.Tables["SecMasterItem"].Rows.Count > 0)
          {
            PriceLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            PriceLabel.DataField = "LastPrice";

            DescriptionLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            DescriptionLabel.DataField = "Description";

            SymbolLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            SymbolLabel.DataField = "Symbol";

            CusipTextBox.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            CusipTextBox.DataField = "SecId";

            BorrrowRateTextBox.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            LoanRateTextBox.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            MarketRateTextBox.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            
            
            DvpFailInSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            DvpFailInTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            DvpFailOutSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            DvpFailOutTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];

            BrokerFailInSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            BrokerFailInTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            BrokerFailOutSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            BrokerFailOutTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];

            ClearingFailInSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            ClearingFailInTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            ClearingFailOutSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            ClearingFailOutTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];

            ExDeficitSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            ExDeficitTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];

            DvpFailInDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            DvpFailOutDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            BrokerFailInDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            BrokerFailOutDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            ClearingFailInDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            ClearingFailOutDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            ExDeficitDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];

            StockBorrowSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            StockLoanSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            SegReqSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];

            NetPositionDayCountLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            NetPositionSettledLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            NetPositionTradedLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];

            DvpFailInSettledLabel.DataField = "DvpFailInSettled";
            DvpFailInTradedLabel.DataField = "DvpFailInTraded";
            DvpFailOutSettledLabel.DataField = "DvpFailOutSettled";
            DvpFailOutTradedLabel.DataField = "DvpFailOutTraded";

            BrokerFailInSettledLabel.DataField = "BrokerFailInSettled";
            BrokerFailInTradedLabel.DataField = "BrokerFailInTraded";
            BrokerFailOutSettledLabel.DataField = "BrokerFailOutSettled";
            BrokerFailOutTradedLabel.DataField = "BrokerFailOutTraded";

            ClearingFailInSettledLabel.DataField = "ClearingFailInSettled";
            ClearingFailInTradedLabel.DataField = "ClearingFailInTraded";
            ClearingFailOutSettledLabel.DataField = "ClearingFailOutSettled";
            ClearingFailOutTradedLabel.DataField = "ClearingFailOutTraded";


            ExDeficitSettledLabel.DataField = "ExDeficitSettled";
            ExDeficitTradedLabel.DataField = "ExDeficitTraded";

            DvpFailInDayCountLabel.DataField = "DvpFailInDayCount";
            DvpFailOutDayCountLabel.DataField = "DvpFailOutDayCount";

            BrokerFailInDayCountLabel.DataField = "BrokerFailInDayCount";
            BrokerFailOutDayCountLabel.DataField = "BrokerFailOutDayCount";

            ClearingFailInDayCountLabel.DataField = "ClearingFailInDayCount";
            ClearingFailOutDayCountLabel.DataField = "ClearingFailOutDayCount";

            SegReqSettledLabel.DataField = "SegReqSettled";
            ExDeficitDayCountLabel.DataField = "DeficitDayCount";

            StockBorrowSettledLabel.DataField = "StockBorrowSettled";
            StockLoanSettledLabel.DataField = "StockLoanSettled";

            NetPositionDayCountLabel.DataField = "NetPositionSettledDayCount";
            NetPositionSettledLabel.DataField = "NetPositionSettled";
            NetPositionTradedLabel.DataField = "NetPositionTraded";

            BorrrowRateTextBox.DataField = "AverageBorrowRate";
            LoanRateTextBox.DataField = "AverageLoanRate";
            MarketRateTextBox.DataField = "Rate";


			BorrowEasyCheckBox.Checked = bool.Parse(dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["IsEasy"].ToString());
			BorrowHardCheckBox.Checked = bool.Parse(dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["IsHard"].ToString());
			BorrowNoCheckBox.Checked = bool.Parse(dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["IsNoLend"].ToString());
			BorrowThresholdCheckBox.Checked = bool.Parse(dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["IsThreshold"].ToString());
			ThresholdDayCountLabel.Text = "[" + dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["ThresholdDayCount"].ToString() + "]";
          }
        }
      }
      catch (Exception error)
      {
        mainForm.Alert(this.Name, error.Message);
      }
    }

    private void SearchButton_Click(object sender, EventArgs e)
    {
      SecMasterFill(SecIdTextBox.Text); 
    }

    private void SecIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13))
      {
        SecMasterFill(SecIdTextBox.Text);   
      }
    }

    private void MarketRateTextBox_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
    {
      try
      {
        e.Text = decimal.Parse(e.Value.ToString()).ToString("##0.00");
      }
      catch { }
    }

    private void NetPositionSettledLabel_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
    {
      try
      {
        e.Text = long.Parse(e.Value.ToString()).ToString("#,##0");
      }
      catch { }
    }
  }
}
