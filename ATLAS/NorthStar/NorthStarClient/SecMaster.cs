using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using StockLoan.Common;

namespace NorthStarClient
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
    }


    public MainForm MainForm
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

            PriceLabel.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["LastPrice"].ToString() + "|" + DateTime.Parse(dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["LastPriceDate"].ToString()).ToString(Standard.DateFormat);

            DescriptionLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            DescriptionLabel.DataField = "Description";

            SymbolLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            SymbolLabel.DataField = "Symbol";

            CusipTextBox.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            CusipTextBox.DataField = "SecId";

            BorrowsRateLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            LoansRateLabel.DataSource = dsSecMasterItem.Tables["BoxPosition"];
            MarketRateLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
            
            
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

            BorrowsRateLabel.DataField = "AverageBorrowRate";
            LoansRateLabel.DataField = "AverageLoanRate";
            MarketRateLabel.DataField = "Rate";
          }
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message, 1);
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

    private void Label_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
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
