using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class SecMasterControl : System.Windows.Forms.UserControl
  {
    private MainForm mainForm;
    
    private string secId = "";
    private bool isBond = false;

		public DataView DeskQuipDataView;
    private C1.Win.C1Input.C1Label CusipLabel;
    private C1.Win.C1Input.C1TextBox CusipText;
    
    private C1.Win.C1Input.C1Label PriceLabel;
    private C1.Win.C1Input.C1Label RecordDateLabel;
    private C1.Win.C1Input.C1Label DividendLabel;
    
    private C1.Win.C1Input.C1TextBox PriceText;
    private C1.Win.C1Input.C1TextBox RecordDateText;
    private C1.Win.C1Input.C1TextBox DividendText;

    private System.Windows.Forms.Panel MainPanel;
    private System.Windows.Forms.Label Label08;
    private System.Windows.Forms.Label Label13;
    private System.Windows.Forms.Label Label14;
    
    private C1.Win.C1Input.C1Label CustomerLongSettled;
    private C1.Win.C1Input.C1Label CustomerLongTraded;
    private C1.Win.C1Input.C1Label FirmLongTraded;
    
    private C1.Win.C1Input.C1Label StockBorrowSettled;
    private C1.Win.C1Input.C1Label StockLoanSettled;

    private DataTable boxPositionDataTable;
		private DataTable accountHoldsTable;

    private DataView boxPositionDataView = null;
    private DataView boxLocationDataView = null;
    private C1.Win.C1Input.C1Label ExDeficitSettled;
    private C1.Win.C1Input.C1Label ExDeficitTraded;
    private C1.Win.C1Input.C1Label DeficitDayCount;
    private System.Windows.Forms.Label BrokerFailInLabel;
    private System.Windows.Forms.Label LoadDateTime;

    private bool withBox = true;
    private bool withDeskQuips = false;

    private string lastDeskQuipDateTime = "0001-01-01 00:00:000";
		private System.Windows.Forms.Label SegReqLabel;
		private C1.Win.C1Input.C1Label SegReqSettled;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label ThresholdDayCountLabel;
		private System.Windows.Forms.Label label3;
  	private C1.Win.C1List.C1Combo BookGroupCombo;
  	private C1.Win.C1Input.C1Label BookLabel;
  	private System.Windows.Forms.GroupBox CusipGroupBox;
  	private C1.Win.C1Input.C1Label SymbolLabel;
  	private C1.Win.C1Input.C1TextBox SymbolText;
  	private C1.Win.C1Input.C1Label IsinLabel;
  	private C1.Win.C1Input.C1TextBox IsinText;
  	private C1.Win.C1Input.C1TextBox QuickText;
  	private C1.Win.C1Input.C1Label QuickLabel;
  	private C1.Win.C1List.C1Combo LocationCombo;
  	private System.Windows.Forms.CheckBox NoLendCheck;
  	private System.Windows.Forms.CheckBox HardCheck;
  	private System.Windows.Forms.CheckBox ThresholdCheck;
  	private System.Windows.Forms.CheckBox EasyCheck;
  	private C1.Win.C1Input.C1TextBox DescriptionText;
  	private C1.Win.C1List.C1Combo AccountHolds;
  	private C1.Win.C1Input.C1Label OtherFailInTraded;
  	private C1.Win.C1Input.C1Label ClearingFailInTraded;
  	private C1.Win.C1Input.C1Label BrokerFailInTraded;
  	private C1.Win.C1Input.C1Label DvpFailInTraded;
  	private C1.Win.C1Input.C1Label OtherFailInSettled;
  	private C1.Win.C1Input.C1Label ClearingFailInSettled;
  	private C1.Win.C1Input.C1Label BrokerFailInSettled;
  	private C1.Win.C1Input.C1Label DvpFailInSettled;
  	private C1.Win.C1Input.C1Label OtherFailInDayCount;
  	private C1.Win.C1Input.C1Label ClearingFailInDayCount;
  	private C1.Win.C1Input.C1Label BrokerFailInDayCount;
  	private C1.Win.C1Input.C1Label DvpFailInDayCount;
		private C1.Win.C1Input.C1Label c1Label3;
		private C1.Win.C1Input.C1Label c1Label4;
		private C1.Win.C1Input.C1Label c1Label5;
		private C1.Win.C1Input.C1Label c1Label6;
		private C1.Win.C1Input.C1Label OtherFailOutSettled;
		private C1.Win.C1Input.C1Label ClearingFailOutSettled;
		private C1.Win.C1Input.C1Label BrokerFailOutSettled;
		private C1.Win.C1Input.C1Label DvpFailOutSettled;
		private C1.Win.C1Input.C1Label OtherFailOutTraded;
		private C1.Win.C1Input.C1Label ClearingFailOutTraded;
		private C1.Win.C1Input.C1Label BrokerFailOutTraded;
		private C1.Win.C1Input.C1Label DvpFailOutTraded;
		private C1.Win.C1Input.C1Label OtherFailOutDayCount;
		private C1.Win.C1Input.C1Label ClearingFailOutDayCount;
		private C1.Win.C1Input.C1Label BrokerFailOutDayCount;
		private C1.Win.C1Input.C1Label DvpFailOutDayCount;
		private System.Windows.Forms.Label label1;
		private C1.Win.C1Input.C1Label c1Label7;
		private C1.Win.C1Input.C1Label c1Label8;
		private C1.Win.C1Input.C1Label c1Label9;
		private C1.Win.C1Input.C1Label c1Label10;
		private C1.Win.C1Input.C1Label FirmPledgeSettled;
		private C1.Win.C1Input.C1Label CustomerPledgeSettled;
		private C1.Win.C1Input.C1Label FirmPledgeTraded;
		private C1.Win.C1Input.C1Label CustomerPledgeTraded;
		private C1.Win.C1Input.C1Label c1Label11;
		private C1.Win.C1Input.C1Label c1Label12;
		private C1.Win.C1Input.C1Label FirmLongSettled;
		private C1.Win.C1Input.C1Label FirmShortSettled;
		private C1.Win.C1Input.C1Label CustomerShortSettled;
		private System.Windows.Forms.Label Label12;
		private C1.Win.C1Input.C1Label FirmShortTraded;
		private C1.Win.C1Input.C1Label CustomerShortTraded;
		private C1.Win.C1Input.C1Label c1Label13;
		private C1.Win.C1Input.C1Label c1Label14;
		private System.Windows.Forms.Label Label07;
		private C1.Win.C1Input.C1Label c1Label15;
		private C1.Win.C1Input.C1Label c1Label16;
		private C1.Win.C1Input.C1Label c1Label17;
		private C1.Win.C1Input.C1TextBox RateBorrowTextBox;
		private C1.Win.C1Input.C1TextBox RateLoanTextBox;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
		private C1.Win.C1Input.C1Label c1Label31;
		private C1.Win.C1Input.C1Label c1Label32;
		private C1.Win.C1Input.C1Label FindLabel;
		private System.Windows.Forms.TextBox FindTextBox;
		private C1.Win.C1Input.C1Label ReceiveSettled;
		private C1.Win.C1Input.C1Label ReceiveTraded;
		private C1.Win.C1Input.C1Label DeliverSettled;
		private C1.Win.C1Input.C1Label DeliverTraded;
		private C1.Win.C1Input.C1Label PledgedTraded;
		private C1.Win.C1Input.C1Label PledgedSettled;
		private C1.Win.C1Input.C1Label ShortTraded;
		private C1.Win.C1Input.C1Label ShortSettled;
		private C1.Win.C1Input.C1Label LongTraded;
        private C1.Win.C1Input.C1Label LongSettled;
		private System.Windows.Forms.CheckBox DtcCheck;
		private System.Windows.Forms.Panel CusipPanel;
		private C1.Win.C1Input.C1Label c1Label1;
		private C1.Win.C1Input.C1Label SecurityText;
		private C1.Win.C1Input.C1Label c1Label2;
		private C1.Win.C1Input.C1Label c1Label18;
		private C1.Win.C1Input.C1Label NetPositionSettledLabel;
		private C1.Win.C1Input.C1Label NetPositionSettledDayCount;
		private System.Windows.Forms.Label label6;
		public C1.Win.C1List.C1Combo DeskQuipCombo;
		private System.Windows.Forms.Label RateIndicatorLabel;
		private C1.Win.C1Input.C1TextBox RateIndicatorText;
        private C1.Win.C1Input.C1Label LockUpSettled;
        private C1.Win.C1Input.C1Label LockUpTraded;
        private Label label7;
        private C1.Win.C1Input.C1Label TradeSellsSettled;
        private C1.Win.C1Input.C1Label TradeSellsTraded;
        private Label label8;

    private System.ComponentModel.Container components = null;

    public SecMasterControl()
    {
      InitializeComponent();
      
   	
		}

    protected override void Dispose( bool disposing )
    {
      if(disposing)
      {
        if(components != null)
        {
          components.Dispose();
        }
      }

      base.Dispose(disposing);
    }

    public void ComponentsFill(string secId)
    {
      DataSet dataSet;
      string[] secIdList;

      CusipText.Text = "";
      SymbolText.Text = "";

      IsinText.Text = "";
      QuickText.Text = "";

      DescriptionText.Text = "";
      SecurityText.Text = "";
            

      EasyCheck.Checked = false;
      HardCheck.Checked = false;
      NoLendCheck.Checked = false;
      ThresholdCheck.Checked = false;
      ThresholdDayCountLabel.Text = "";
      
      DtcCheck.Checked = false;

      PriceText.Text = "";
      RecordDateText.Text = "";
      DividendText.Text = "";

      this.secId = secId.ToUpper();

      try
      {
        dataSet = mainForm.ServiceAgent.SecMasterLookup(secId, withBox, withDeskQuips, mainForm.UtcOffset, lastDeskQuipDateTime);

        foreach (DataRow row in dataSet.Tables["SecMasterItem"].Rows) // Expect one row.
        {
          secIdList = row["SecIdList"].ToString().Split('|');

          CusipText.Text =  secIdList[1];
          SymbolText.Text =  secIdList[2];
          IsinText.Text =  secIdList[4];
          QuickText.Text =  secIdList[5];

          DescriptionText.Text = row["Description"].ToString();
          SecurityText.Text = row["CountryCode"].ToString() + ":" + row["BaseType"].ToString() + ":" + row["ClassGroup"].ToString();
          
          isBond = row["BaseType"].ToString().Equals("B");
          HardCheck.Checked = (bool)row["IsHard"];
          EasyCheck.Checked = (bool)row["IsEasy"];
          NoLendCheck.Checked = (bool)row["IsNoLend"];
          ThresholdCheck.Checked = (bool)row["IsThreshold"];
          ThresholdDayCountLabel.Text = "[" + row["ThresholdDayCount"].ToString() + "]";					
					
          if (!row["IsDtcEligible"].Equals(DBNull.Value))
          {
            DtcCheck.Checked = (bool) row["IsDtcEligible"];
          }

					if (!row["Rate"].ToString().Equals(""))
					{
						RateIndicatorText.Text = float.Parse(row["Rate"].ToString()).ToString("#0.###");
					}
					else
					{
						RateIndicatorText.Text = "";
					}

          PriceText.Text = row["LastPrice"].ToString() + " | " + Tools.FormatDate(row["LastPriceDate"].ToString(), Standard.DateFormat);
          RecordDateText.Text = Tools.FormatDate(row["RecordDateCash"].ToString(), Standard.DateFormat);
          
					if (Tools.IsNumeric(row["DividendRate"].ToString()))
					{
						DividendText.Text = Decimal.Parse(row["DividendRate"].ToString()).ToString("##0.0000");						
					}
					else
					{					
						DividendText.Text = row["DividendRate"].ToString();
					}
				
					this.secId = row["SecId"].ToString();
        }
          
				if (withDeskQuips && (dataSet.Tables["DeskQuips"].Rows.Count > 0)) // Add the most recent desk quip data.
				{
					lastDeskQuipDateTime = dataSet.Tables["DeskQuips"].Rows[0]["ActTime"].ToString();
    
					DataRow[] rows = dataSet.Tables["DeskQuips"].Select();

					mainForm.DeskQuipDataSet.Tables["DeskQuips"].BeginLoadData();                
					foreach (DataRow row in rows)
					{
						mainForm.DeskQuipDataSet.Tables["DeskQuips"].ImportRow(row);          
					}
					mainForm.DeskQuipDataSet.Tables["DeskQuips"].EndLoadData();        
				}

				DeskQuipCombo.Enabled = dataSet.Tables["SecMasterItem"].Rows.Count.Equals(1); // The sec master did know the security.
				DeskQuipCombo.SelectedIndex = -1;
        
				DeskQuipDataView.RowFilter = "SecId = '" + this.secId + "'";
				DeskQuipDataView.Sort = "ActTime Desc";

				if (DeskQuipDataView.Count > 0)
				{
					DeskQuipCombo.SelectedIndex = 0;
				}

				if (withBox) // Load the box componenets with data.
        {
          BoxPositionInit(dataSet.Tables["BoxPosition"]);          

          boxLocationDataView = new DataView(dataSet.Tables["BoxLocation"]);
          boxLocationDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
        
          LocationCombo.HoldFields();
          LocationCombo.DataSource = boxLocationDataView;
					LocationCombo.DisplayMember = "";
					
					
					if (LocationCombo.ListCount > 0)
					{
						LocationCombo.SelectedIndex = 0;
					}
					else
					{
						LocationCombo.Text = "";
					}

          LoadDateTime.Text = "[" + Tools.FormatDate(dataSet.ExtendedProperties["LoadDateTime"].ToString(), "ddd H:mm", mainForm.UtcOffset) + "]";
        }  
      }
      catch (Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);
      }
    }

    public MainForm MainForm
    {
      set 
      {
        mainForm = value;
      }
    }

    public bool WithBox
    {
      set 
      {
        withBox = value;        
      }
    }

    public bool WithDeskQuips
    {
      set 
      {
        withDeskQuips = value;
      }
    }

    public string SecId
    {
      set 
      {
        if (!secId.Equals(value))
        {
          ComponentsFill(value);
        
          FindTextBox.Text = secId;
          FindTextBox.SelectAll();
        
					if (mainForm.substitutionInputForm != null)
					{
						mainForm.substitutionInputForm.SubstitutionSecId = secId;
					}
				}
      }

      get 
      {
        return secId; 
      }
    }

    public string Symbol
    {
      get 
      {
        return SymbolText.Text; 
      }
    }

    public string Description
    {
      get 
      {
        return DescriptionText.Text; 
      }
    }

    public string Price
    {
      get
      {
        return (PriceText.Text.Split('|'))[0].Trim();
      }
    }

    public bool IsBond
    {
      get
      {
        return isBond;
      }
    }

    #region Component Designer generated code
    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecMasterControl));
        this.MainPanel = new System.Windows.Forms.Panel();
        this.TradeSellsSettled = new C1.Win.C1Input.C1Label();
        this.TradeSellsTraded = new C1.Win.C1Input.C1Label();
        this.label8 = new System.Windows.Forms.Label();
        this.LockUpSettled = new C1.Win.C1Input.C1Label();
        this.LockUpTraded = new C1.Win.C1Input.C1Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.c1Label18 = new C1.Win.C1Input.C1Label();
        this.c1Label2 = new C1.Win.C1Input.C1Label();
        this.NetPositionSettledLabel = new C1.Win.C1Input.C1Label();
        this.NetPositionSettledDayCount = new C1.Win.C1Input.C1Label();
        this.SegReqLabel = new System.Windows.Forms.Label();
        this.SegReqSettled = new C1.Win.C1Input.C1Label();
        this.Label13 = new System.Windows.Forms.Label();
        this.Label14 = new System.Windows.Forms.Label();
        this.StockBorrowSettled = new C1.Win.C1Input.C1Label();
        this.StockLoanSettled = new C1.Win.C1Input.C1Label();
        this.c1Label32 = new C1.Win.C1Input.C1Label();
        this.c1Label31 = new C1.Win.C1Input.C1Label();
        this.ReceiveSettled = new C1.Win.C1Input.C1Label();
        this.ReceiveTraded = new C1.Win.C1Input.C1Label();
        this.DeliverSettled = new C1.Win.C1Input.C1Label();
        this.DeliverTraded = new C1.Win.C1Input.C1Label();
        this.PledgedTraded = new C1.Win.C1Input.C1Label();
        this.PledgedSettled = new C1.Win.C1Input.C1Label();
        this.ShortTraded = new C1.Win.C1Input.C1Label();
        this.ShortSettled = new C1.Win.C1Input.C1Label();
        this.LongTraded = new C1.Win.C1Input.C1Label();
        this.LongSettled = new C1.Win.C1Input.C1Label();
        this.c1Label17 = new C1.Win.C1Input.C1Label();
        this.c1Label15 = new C1.Win.C1Input.C1Label();
        this.c1Label16 = new C1.Win.C1Input.C1Label();
        this.Label07 = new System.Windows.Forms.Label();
        this.c1Label13 = new C1.Win.C1Input.C1Label();
        this.c1Label14 = new C1.Win.C1Input.C1Label();
        this.Label12 = new System.Windows.Forms.Label();
        this.FirmShortSettled = new C1.Win.C1Input.C1Label();
        this.CustomerShortSettled = new C1.Win.C1Input.C1Label();
        this.c1Label11 = new C1.Win.C1Input.C1Label();
        this.c1Label12 = new C1.Win.C1Input.C1Label();
        this.c1Label7 = new C1.Win.C1Input.C1Label();
        this.c1Label8 = new C1.Win.C1Input.C1Label();
        this.c1Label9 = new C1.Win.C1Input.C1Label();
        this.c1Label10 = new C1.Win.C1Input.C1Label();
        this.label1 = new System.Windows.Forms.Label();
        this.c1Label6 = new C1.Win.C1Input.C1Label();
        this.c1Label5 = new C1.Win.C1Input.C1Label();
        this.c1Label4 = new C1.Win.C1Input.C1Label();
        this.c1Label3 = new C1.Win.C1Input.C1Label();
        this.OtherFailInTraded = new C1.Win.C1Input.C1Label();
        this.DvpFailInTraded = new C1.Win.C1Input.C1Label();
        this.BrokerFailInTraded = new C1.Win.C1Input.C1Label();
        this.ClearingFailInTraded = new C1.Win.C1Input.C1Label();
        this.OtherFailInSettled = new C1.Win.C1Input.C1Label();
        this.ClearingFailInSettled = new C1.Win.C1Input.C1Label();
        this.BrokerFailInSettled = new C1.Win.C1Input.C1Label();
        this.DvpFailInSettled = new C1.Win.C1Input.C1Label();
        this.OtherFailInDayCount = new C1.Win.C1Input.C1Label();
        this.DvpFailInDayCount = new C1.Win.C1Input.C1Label();
        this.BrokerFailInDayCount = new C1.Win.C1Input.C1Label();
        this.ClearingFailInDayCount = new C1.Win.C1Input.C1Label();
        this.BrokerFailInLabel = new System.Windows.Forms.Label();
        this.OtherFailOutSettled = new C1.Win.C1Input.C1Label();
        this.ClearingFailOutSettled = new C1.Win.C1Input.C1Label();
        this.BrokerFailOutSettled = new C1.Win.C1Input.C1Label();
        this.DvpFailOutSettled = new C1.Win.C1Input.C1Label();
        this.ClearingFailOutTraded = new C1.Win.C1Input.C1Label();
        this.BrokerFailOutTraded = new C1.Win.C1Input.C1Label();
        this.DvpFailOutTraded = new C1.Win.C1Input.C1Label();
        this.OtherFailOutTraded = new C1.Win.C1Input.C1Label();
        this.DvpFailOutDayCount = new C1.Win.C1Input.C1Label();
        this.BrokerFailOutDayCount = new C1.Win.C1Input.C1Label();
        this.ClearingFailOutDayCount = new C1.Win.C1Input.C1Label();
        this.OtherFailOutDayCount = new C1.Win.C1Input.C1Label();
        this.CustomerPledgeSettled = new C1.Win.C1Input.C1Label();
        this.FirmPledgeSettled = new C1.Win.C1Input.C1Label();
        this.CustomerPledgeTraded = new C1.Win.C1Input.C1Label();
        this.FirmPledgeTraded = new C1.Win.C1Input.C1Label();
        this.CustomerShortTraded = new C1.Win.C1Input.C1Label();
        this.FirmShortTraded = new C1.Win.C1Input.C1Label();
        this.Label08 = new System.Windows.Forms.Label();
        this.CustomerLongSettled = new C1.Win.C1Input.C1Label();
        this.CustomerLongTraded = new C1.Win.C1Input.C1Label();
        this.FirmLongSettled = new C1.Win.C1Input.C1Label();
        this.FirmLongTraded = new C1.Win.C1Input.C1Label();
        this.ExDeficitSettled = new C1.Win.C1Input.C1Label();
        this.ExDeficitTraded = new C1.Win.C1Input.C1Label();
        this.DeficitDayCount = new C1.Win.C1Input.C1Label();
        this.label3 = new System.Windows.Forms.Label();
        this.ThresholdDayCountLabel = new System.Windows.Forms.Label();
        this.DividendLabel = new C1.Win.C1Input.C1Label();
        this.DividendText = new C1.Win.C1Input.C1TextBox();
        this.PriceLabel = new C1.Win.C1Input.C1Label();
        this.PriceText = new C1.Win.C1Input.C1TextBox();
        this.RecordDateLabel = new C1.Win.C1Input.C1Label();
        this.RecordDateText = new C1.Win.C1Input.C1TextBox();
        this.CusipLabel = new C1.Win.C1Input.C1Label();
        this.CusipText = new C1.Win.C1Input.C1TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.LoadDateTime = new System.Windows.Forms.Label();
        this.CusipPanel = new System.Windows.Forms.Panel();
        this.BookGroupCombo = new C1.Win.C1List.C1Combo();
        this.FindTextBox = new System.Windows.Forms.TextBox();
        this.FindLabel = new C1.Win.C1Input.C1Label();
        this.BookLabel = new C1.Win.C1Input.C1Label();
        this.RateIndicatorText = new C1.Win.C1Input.C1TextBox();
        this.RateIndicatorLabel = new System.Windows.Forms.Label();
        this.DeskQuipCombo = new C1.Win.C1List.C1Combo();
        this.DtcCheck = new System.Windows.Forms.CheckBox();
        this.label5 = new System.Windows.Forms.Label();
        this.RateLoanTextBox = new C1.Win.C1Input.C1TextBox();
        this.RateBorrowTextBox = new C1.Win.C1Input.C1TextBox();
        this.NoLendCheck = new System.Windows.Forms.CheckBox();
        this.HardCheck = new System.Windows.Forms.CheckBox();
        this.ThresholdCheck = new System.Windows.Forms.CheckBox();
        this.EasyCheck = new System.Windows.Forms.CheckBox();
        this.CusipGroupBox = new System.Windows.Forms.GroupBox();
        this.SecurityText = new C1.Win.C1Input.C1Label();
        this.c1Label1 = new C1.Win.C1Input.C1Label();
        this.AccountHolds = new C1.Win.C1List.C1Combo();
        this.DescriptionText = new C1.Win.C1Input.C1TextBox();
        this.LocationCombo = new C1.Win.C1List.C1Combo();
        this.QuickText = new C1.Win.C1Input.C1TextBox();
        this.QuickLabel = new C1.Win.C1Input.C1Label();
        this.IsinLabel = new C1.Win.C1Input.C1Label();
        this.IsinText = new C1.Win.C1Input.C1TextBox();
        this.SymbolText = new C1.Win.C1Input.C1TextBox();
        this.SymbolLabel = new C1.Win.C1Input.C1Label();
        this.label4 = new System.Windows.Forms.Label();
        this.MainPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.TradeSellsSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.TradeSellsTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LockUpSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LockUpTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.NetPositionSettledLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.NetPositionSettledDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.SegReqSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.StockBorrowSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.StockLoanSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label32)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label31)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ReceiveSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ReceiveTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeliverSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeliverTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PledgedTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PledgedSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ShortTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ShortSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LongTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LongSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmShortSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerShortSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailInTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailInTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailInTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailInTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailInSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailInSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailInSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailInSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailInDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailInDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailInDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailInDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailOutSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailOutSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailOutSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailOutSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailOutTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailOutTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailOutTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailOutTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailOutDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailOutDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailOutDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailOutDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerPledgeSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmPledgeSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerPledgeTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmPledgeTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerShortTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmShortTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerLongSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerLongTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmLongSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmLongTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ExDeficitSettled)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ExDeficitTraded)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeficitDayCount)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DividendLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DividendText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PriceLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.PriceText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.RecordDateLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.RecordDateText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CusipLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CusipText)).BeginInit();
        this.CusipPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FindLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BookLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.RateIndicatorText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeskQuipCombo)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.RateLoanTextBox)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.RateBorrowTextBox)).BeginInit();
        this.CusipGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.SecurityText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.AccountHolds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.DescriptionText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LocationCombo)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.QuickText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.QuickLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.IsinLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.IsinText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.SymbolText)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.SymbolLabel)).BeginInit();
        this.SuspendLayout();
        // 
        // MainPanel
        // 
        this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(41)))), ((int)(((byte)(107)))));
        this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.MainPanel.Controls.Add(this.TradeSellsSettled);
        this.MainPanel.Controls.Add(this.TradeSellsTraded);
        this.MainPanel.Controls.Add(this.label8);
        this.MainPanel.Controls.Add(this.LockUpSettled);
        this.MainPanel.Controls.Add(this.LockUpTraded);
        this.MainPanel.Controls.Add(this.label7);
        this.MainPanel.Controls.Add(this.label6);
        this.MainPanel.Controls.Add(this.c1Label18);
        this.MainPanel.Controls.Add(this.c1Label2);
        this.MainPanel.Controls.Add(this.NetPositionSettledLabel);
        this.MainPanel.Controls.Add(this.NetPositionSettledDayCount);
        this.MainPanel.Controls.Add(this.SegReqLabel);
        this.MainPanel.Controls.Add(this.SegReqSettled);
        this.MainPanel.Controls.Add(this.Label13);
        this.MainPanel.Controls.Add(this.Label14);
        this.MainPanel.Controls.Add(this.StockBorrowSettled);
        this.MainPanel.Controls.Add(this.StockLoanSettled);
        this.MainPanel.Controls.Add(this.c1Label32);
        this.MainPanel.Controls.Add(this.c1Label31);
        this.MainPanel.Controls.Add(this.ReceiveSettled);
        this.MainPanel.Controls.Add(this.ReceiveTraded);
        this.MainPanel.Controls.Add(this.DeliverSettled);
        this.MainPanel.Controls.Add(this.DeliverTraded);
        this.MainPanel.Controls.Add(this.PledgedTraded);
        this.MainPanel.Controls.Add(this.PledgedSettled);
        this.MainPanel.Controls.Add(this.ShortTraded);
        this.MainPanel.Controls.Add(this.ShortSettled);
        this.MainPanel.Controls.Add(this.LongTraded);
        this.MainPanel.Controls.Add(this.LongSettled);
        this.MainPanel.Controls.Add(this.c1Label17);
        this.MainPanel.Controls.Add(this.c1Label15);
        this.MainPanel.Controls.Add(this.c1Label16);
        this.MainPanel.Controls.Add(this.Label07);
        this.MainPanel.Controls.Add(this.c1Label13);
        this.MainPanel.Controls.Add(this.c1Label14);
        this.MainPanel.Controls.Add(this.Label12);
        this.MainPanel.Controls.Add(this.FirmShortSettled);
        this.MainPanel.Controls.Add(this.CustomerShortSettled);
        this.MainPanel.Controls.Add(this.c1Label11);
        this.MainPanel.Controls.Add(this.c1Label12);
        this.MainPanel.Controls.Add(this.c1Label7);
        this.MainPanel.Controls.Add(this.c1Label8);
        this.MainPanel.Controls.Add(this.c1Label9);
        this.MainPanel.Controls.Add(this.c1Label10);
        this.MainPanel.Controls.Add(this.label1);
        this.MainPanel.Controls.Add(this.c1Label6);
        this.MainPanel.Controls.Add(this.c1Label5);
        this.MainPanel.Controls.Add(this.c1Label4);
        this.MainPanel.Controls.Add(this.c1Label3);
        this.MainPanel.Controls.Add(this.OtherFailInTraded);
        this.MainPanel.Controls.Add(this.DvpFailInTraded);
        this.MainPanel.Controls.Add(this.BrokerFailInTraded);
        this.MainPanel.Controls.Add(this.ClearingFailInTraded);
        this.MainPanel.Controls.Add(this.OtherFailInSettled);
        this.MainPanel.Controls.Add(this.ClearingFailInSettled);
        this.MainPanel.Controls.Add(this.BrokerFailInSettled);
        this.MainPanel.Controls.Add(this.DvpFailInSettled);
        this.MainPanel.Controls.Add(this.OtherFailInDayCount);
        this.MainPanel.Controls.Add(this.DvpFailInDayCount);
        this.MainPanel.Controls.Add(this.BrokerFailInDayCount);
        this.MainPanel.Controls.Add(this.ClearingFailInDayCount);
        this.MainPanel.Controls.Add(this.BrokerFailInLabel);
        this.MainPanel.Controls.Add(this.OtherFailOutSettled);
        this.MainPanel.Controls.Add(this.ClearingFailOutSettled);
        this.MainPanel.Controls.Add(this.BrokerFailOutSettled);
        this.MainPanel.Controls.Add(this.DvpFailOutSettled);
        this.MainPanel.Controls.Add(this.ClearingFailOutTraded);
        this.MainPanel.Controls.Add(this.BrokerFailOutTraded);
        this.MainPanel.Controls.Add(this.DvpFailOutTraded);
        this.MainPanel.Controls.Add(this.OtherFailOutTraded);
        this.MainPanel.Controls.Add(this.DvpFailOutDayCount);
        this.MainPanel.Controls.Add(this.BrokerFailOutDayCount);
        this.MainPanel.Controls.Add(this.ClearingFailOutDayCount);
        this.MainPanel.Controls.Add(this.OtherFailOutDayCount);
        this.MainPanel.Controls.Add(this.CustomerPledgeSettled);
        this.MainPanel.Controls.Add(this.FirmPledgeSettled);
        this.MainPanel.Controls.Add(this.CustomerPledgeTraded);
        this.MainPanel.Controls.Add(this.FirmPledgeTraded);
        this.MainPanel.Controls.Add(this.CustomerShortTraded);
        this.MainPanel.Controls.Add(this.FirmShortTraded);
        this.MainPanel.Controls.Add(this.Label08);
        this.MainPanel.Controls.Add(this.CustomerLongSettled);
        this.MainPanel.Controls.Add(this.CustomerLongTraded);
        this.MainPanel.Controls.Add(this.FirmLongSettled);
        this.MainPanel.Controls.Add(this.FirmLongTraded);
        this.MainPanel.Controls.Add(this.ExDeficitSettled);
        this.MainPanel.Controls.Add(this.ExDeficitTraded);
        this.MainPanel.Controls.Add(this.DeficitDayCount);
        this.MainPanel.Controls.Add(this.label3);
        this.MainPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.MainPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.MainPanel.Location = new System.Drawing.Point(1, 400);
        this.MainPanel.Name = "MainPanel";
        this.MainPanel.Size = new System.Drawing.Size(278, 708);
        this.MainPanel.TabIndex = 6;
        // 
        // TradeSellsSettled
        // 
        this.TradeSellsSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.TradeSellsSettled.DataType = typeof(long);
        this.TradeSellsSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.TradeSellsSettled.ForeColor = System.Drawing.Color.White;
        this.TradeSellsSettled.FormatInfo.CustomFormat = "#,##0";
        this.TradeSellsSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.TradeSellsSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.TradeSellsSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.TradeSellsSettled.Location = new System.Drawing.Point(108, 314);
        this.TradeSellsSettled.Name = "TradeSellsSettled";
        this.TradeSellsSettled.Size = new System.Drawing.Size(78, 18);
        this.TradeSellsSettled.TabIndex = 118;
        this.TradeSellsSettled.Tag = null;
        this.TradeSellsSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // TradeSellsTraded
        // 
        this.TradeSellsTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.TradeSellsTraded.DataType = typeof(long);
        this.TradeSellsTraded.ForeColor = System.Drawing.Color.Silver;
        this.TradeSellsTraded.FormatInfo.CustomFormat = "#,##0";
        this.TradeSellsTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.TradeSellsTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.TradeSellsTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.TradeSellsTraded.Location = new System.Drawing.Point(192, 314);
        this.TradeSellsTraded.Name = "TradeSellsTraded";
        this.TradeSellsTraded.Size = new System.Drawing.Size(78, 18);
        this.TradeSellsTraded.TabIndex = 119;
        this.TradeSellsTraded.Tag = null;
        this.TradeSellsTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label8
        // 
        this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label8.ForeColor = System.Drawing.Color.Goldenrod;
        this.label8.Location = new System.Drawing.Point(6, 314);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(108, 18);
        this.label8.TabIndex = 117;
        this.label8.Text = "Customer Sells";
        this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // LockUpSettled
        // 
        this.LockUpSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.LockUpSettled.DataType = typeof(long);
        this.LockUpSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LockUpSettled.ForeColor = System.Drawing.Color.White;
        this.LockUpSettled.FormatInfo.CustomFormat = "#,##0";
        this.LockUpSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LockUpSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.LockUpSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LockUpSettled.Location = new System.Drawing.Point(108, 147);
        this.LockUpSettled.Name = "LockUpSettled";
        this.LockUpSettled.Size = new System.Drawing.Size(78, 18);
        this.LockUpSettled.TabIndex = 115;
        this.LockUpSettled.Tag = null;
        this.LockUpSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // LockUpTraded
        // 
        this.LockUpTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.LockUpTraded.DataType = typeof(long);
        this.LockUpTraded.ForeColor = System.Drawing.Color.Silver;
        this.LockUpTraded.FormatInfo.CustomFormat = "#,##0";
        this.LockUpTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LockUpTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.LockUpTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LockUpTraded.Location = new System.Drawing.Point(192, 147);
        this.LockUpTraded.Name = "LockUpTraded";
        this.LockUpTraded.Size = new System.Drawing.Size(78, 18);
        this.LockUpTraded.TabIndex = 116;
        this.LockUpTraded.Tag = null;
        this.LockUpTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label7.ForeColor = System.Drawing.Color.Goldenrod;
        this.label7.Location = new System.Drawing.Point(6, 147);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(56, 18);
        this.label7.TabIndex = 114;
        this.label7.Text = "Lockup";
        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label6
        // 
        this.label6.BackColor = System.Drawing.Color.Silver;
        this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(57)))), ((int)(((byte)(173)))));
        this.label6.Location = new System.Drawing.Point(6, 6);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(42, 18);
        this.label6.TabIndex = 113;
        this.label6.Text = "Net";
        this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // c1Label18
        // 
        this.c1Label18.BackColor = System.Drawing.Color.White;
        this.c1Label18.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label18.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label18.Location = new System.Drawing.Point(234, 672);
        this.c1Label18.Name = "c1Label18";
        this.c1Label18.Size = new System.Drawing.Size(12, 6);
        this.c1Label18.TabIndex = 112;
        this.c1Label18.Tag = null;
        this.c1Label18.Text = "L";
        this.c1Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.c1Label18.TextDetached = true;
        this.c1Label18.Click += new System.EventHandler(this.c1Label18_Click);
        // 
        // c1Label2
        // 
        this.c1Label2.BackColor = System.Drawing.Color.White;
        this.c1Label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label2.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label2.Location = new System.Drawing.Point(258, 672);
        this.c1Label2.Name = "c1Label2";
        this.c1Label2.Size = new System.Drawing.Size(12, 6);
        this.c1Label2.TabIndex = 111;
        this.c1Label2.Tag = null;
        this.c1Label2.Text = "R";
        this.c1Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.c1Label2.TextDetached = true;
        this.c1Label2.Click += new System.EventHandler(this.c1Label2_Click);
        // 
        // NetPositionSettledLabel
        // 
        this.NetPositionSettledLabel.BackColor = System.Drawing.Color.White;
        this.NetPositionSettledLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.NetPositionSettledLabel.DataType = typeof(long);
        this.NetPositionSettledLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.NetPositionSettledLabel.ForeColor = System.Drawing.Color.Black;
        this.NetPositionSettledLabel.FormatInfo.CustomFormat = "#,##0";
        this.NetPositionSettledLabel.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.NetPositionSettledLabel.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.NetPositionSettledLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.NetPositionSettledLabel.Location = new System.Drawing.Point(108, 6);
        this.NetPositionSettledLabel.Name = "NetPositionSettledLabel";
        this.NetPositionSettledLabel.Size = new System.Drawing.Size(120, 18);
        this.NetPositionSettledLabel.TabIndex = 83;
        this.NetPositionSettledLabel.Tag = null;
        this.NetPositionSettledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // NetPositionSettledDayCount
        // 
        this.NetPositionSettledDayCount.BackColor = System.Drawing.Color.Silver;
        this.NetPositionSettledDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.NetPositionSettledDayCount.DataType = typeof(long);
        this.NetPositionSettledDayCount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.NetPositionSettledDayCount.ForeColor = System.Drawing.Color.Black;
        this.NetPositionSettledDayCount.FormatInfo.CustomFormat = "[0]";
        this.NetPositionSettledDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.NetPositionSettledDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.NetPositionSettledDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.NetPositionSettledDayCount.Location = new System.Drawing.Point(66, 6);
        this.NetPositionSettledDayCount.Name = "NetPositionSettledDayCount";
        this.NetPositionSettledDayCount.Size = new System.Drawing.Size(36, 18);
        this.NetPositionSettledDayCount.TabIndex = 86;
        this.NetPositionSettledDayCount.Tag = null;
        this.NetPositionSettledDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // SegReqLabel
        // 
        this.SegReqLabel.BackColor = System.Drawing.Color.Silver;
        this.SegReqLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.SegReqLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(57)))), ((int)(((byte)(173)))));
        this.SegReqLabel.Location = new System.Drawing.Point(6, 28);
        this.SegReqLabel.Name = "SegReqLabel";
        this.SegReqLabel.Size = new System.Drawing.Size(66, 18);
        this.SegReqLabel.TabIndex = 79;
        this.SegReqLabel.Text = "Seg Req";
        this.SegReqLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // SegReqSettled
        // 
        this.SegReqSettled.BackColor = System.Drawing.Color.White;
        this.SegReqSettled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.SegReqSettled.DataType = typeof(long);
        this.SegReqSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.SegReqSettled.ForeColor = System.Drawing.Color.Black;
        this.SegReqSettled.FormatInfo.CustomFormat = "#,##0";
        this.SegReqSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.SegReqSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.SegReqSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.SegReqSettled.Location = new System.Drawing.Point(108, 28);
        this.SegReqSettled.Name = "SegReqSettled";
        this.SegReqSettled.Size = new System.Drawing.Size(120, 18);
        this.SegReqSettled.TabIndex = 80;
        this.SegReqSettled.Tag = null;
        this.SegReqSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // Label13
        // 
        this.Label13.BackColor = System.Drawing.Color.Silver;
        this.Label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(57)))), ((int)(((byte)(173)))));
        this.Label13.Location = new System.Drawing.Point(6, 50);
        this.Label13.Name = "Label13";
        this.Label13.Size = new System.Drawing.Size(84, 18);
        this.Label13.TabIndex = 50;
        this.Label13.Text = "Borrow";
        this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.Label13.Click += new System.EventHandler(this.BorrowContractTypeLabel_Click);
        // 
        // Label14
        // 
        this.Label14.BackColor = System.Drawing.Color.Silver;
        this.Label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(57)))), ((int)(((byte)(173)))));
        this.Label14.Location = new System.Drawing.Point(6, 72);
        this.Label14.Name = "Label14";
        this.Label14.Size = new System.Drawing.Size(84, 18);
        this.Label14.TabIndex = 51;
        this.Label14.Text = "Loan";
        this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.Label14.Click += new System.EventHandler(this.LoanContractTypeLabel_Click);
        // 
        // StockBorrowSettled
        // 
        this.StockBorrowSettled.BackColor = System.Drawing.Color.White;
        this.StockBorrowSettled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.StockBorrowSettled.DataType = typeof(long);
        this.StockBorrowSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.StockBorrowSettled.ForeColor = System.Drawing.Color.Black;
        this.StockBorrowSettled.FormatInfo.CustomFormat = "#,##0";
        this.StockBorrowSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.StockBorrowSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.StockBorrowSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.StockBorrowSettled.Location = new System.Drawing.Point(108, 50);
        this.StockBorrowSettled.Name = "StockBorrowSettled";
        this.StockBorrowSettled.Size = new System.Drawing.Size(120, 18);
        this.StockBorrowSettled.TabIndex = 21;
        this.StockBorrowSettled.Tag = null;
        this.StockBorrowSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // StockLoanSettled
        // 
        this.StockLoanSettled.BackColor = System.Drawing.Color.White;
        this.StockLoanSettled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.StockLoanSettled.DataType = typeof(long);
        this.StockLoanSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.StockLoanSettled.ForeColor = System.Drawing.Color.Black;
        this.StockLoanSettled.FormatInfo.CustomFormat = "#,##0";
        this.StockLoanSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.StockLoanSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.StockLoanSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.StockLoanSettled.Location = new System.Drawing.Point(108, 72);
        this.StockLoanSettled.Name = "StockLoanSettled";
        this.StockLoanSettled.Size = new System.Drawing.Size(120, 18);
        this.StockLoanSettled.TabIndex = 22;
        this.StockLoanSettled.Tag = null;
        this.StockLoanSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // c1Label32
        // 
        this.c1Label32.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label32.DataType = typeof(long);
        this.c1Label32.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label32.ForeColor = System.Drawing.Color.Silver;
        this.c1Label32.FormatInfo.CustomFormat = "#,##0";
        this.c1Label32.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.c1Label32.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.c1Label32.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.c1Label32.Location = new System.Drawing.Point(192, 102);
        this.c1Label32.Name = "c1Label32";
        this.c1Label32.Size = new System.Drawing.Size(78, 18);
        this.c1Label32.TabIndex = 110;
        this.c1Label32.Tag = null;
        this.c1Label32.Text = "Traded";
        this.c1Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.c1Label32.TextDetached = true;
        // 
        // c1Label31
        // 
        this.c1Label31.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label31.DataType = typeof(long);
        this.c1Label31.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label31.ForeColor = System.Drawing.Color.White;
        this.c1Label31.FormatInfo.CustomFormat = "#,##0";
        this.c1Label31.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.c1Label31.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.c1Label31.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.c1Label31.Location = new System.Drawing.Point(114, 102);
        this.c1Label31.Name = "c1Label31";
        this.c1Label31.Size = new System.Drawing.Size(72, 18);
        this.c1Label31.TabIndex = 109;
        this.c1Label31.Tag = null;
        this.c1Label31.Text = "Settled";
        this.c1Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.c1Label31.TextDetached = true;
        // 
        // ReceiveSettled
        // 
        this.ReceiveSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ReceiveSettled.DataType = typeof(long);
        this.ReceiveSettled.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ReceiveSettled.ForeColor = System.Drawing.Color.White;
        this.ReceiveSettled.FormatInfo.CustomFormat = "#,##0";
        this.ReceiveSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ReceiveSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ReceiveSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ReceiveSettled.Location = new System.Drawing.Point(108, 548);
        this.ReceiveSettled.Name = "ReceiveSettled";
        this.ReceiveSettled.Size = new System.Drawing.Size(78, 18);
        this.ReceiveSettled.TabIndex = 105;
        this.ReceiveSettled.Tag = null;
        this.ReceiveSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ReceiveTraded
        // 
        this.ReceiveTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ReceiveTraded.DataType = typeof(long);
        this.ReceiveTraded.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ReceiveTraded.ForeColor = System.Drawing.Color.Silver;
        this.ReceiveTraded.FormatInfo.CustomFormat = "#,##0";
        this.ReceiveTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ReceiveTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ReceiveTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ReceiveTraded.Location = new System.Drawing.Point(192, 548);
        this.ReceiveTraded.Name = "ReceiveTraded";
        this.ReceiveTraded.Size = new System.Drawing.Size(78, 18);
        this.ReceiveTraded.TabIndex = 104;
        this.ReceiveTraded.Tag = null;
        this.ReceiveTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DeliverSettled
        // 
        this.DeliverSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DeliverSettled.DataType = typeof(long);
        this.DeliverSettled.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DeliverSettled.ForeColor = System.Drawing.Color.White;
        this.DeliverSettled.FormatInfo.CustomFormat = "#,##0";
        this.DeliverSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DeliverSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DeliverSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DeliverSettled.Location = new System.Drawing.Point(108, 422);
        this.DeliverSettled.Name = "DeliverSettled";
        this.DeliverSettled.Size = new System.Drawing.Size(78, 18);
        this.DeliverSettled.TabIndex = 103;
        this.DeliverSettled.Tag = null;
        this.DeliverSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DeliverTraded
        // 
        this.DeliverTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DeliverTraded.DataType = typeof(long);
        this.DeliverTraded.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DeliverTraded.ForeColor = System.Drawing.Color.Silver;
        this.DeliverTraded.FormatInfo.CustomFormat = "#,##0";
        this.DeliverTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DeliverTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DeliverTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DeliverTraded.Location = new System.Drawing.Point(192, 422);
        this.DeliverTraded.Name = "DeliverTraded";
        this.DeliverTraded.Size = new System.Drawing.Size(78, 18);
        this.DeliverTraded.TabIndex = 102;
        this.DeliverTraded.Tag = null;
        this.DeliverTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // PledgedTraded
        // 
        this.PledgedTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.PledgedTraded.DataType = typeof(long);
        this.PledgedTraded.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.PledgedTraded.ForeColor = System.Drawing.Color.Silver;
        this.PledgedTraded.FormatInfo.CustomFormat = "#,##0";
        this.PledgedTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.PledgedTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.PledgedTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.PledgedTraded.Location = new System.Drawing.Point(192, 344);
        this.PledgedTraded.Name = "PledgedTraded";
        this.PledgedTraded.Size = new System.Drawing.Size(78, 18);
        this.PledgedTraded.TabIndex = 101;
        this.PledgedTraded.Tag = null;
        this.PledgedTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // PledgedSettled
        // 
        this.PledgedSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.PledgedSettled.DataType = typeof(long);
        this.PledgedSettled.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.PledgedSettled.ForeColor = System.Drawing.Color.White;
        this.PledgedSettled.FormatInfo.CustomFormat = "#,##0";
        this.PledgedSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.PledgedSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.PledgedSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.PledgedSettled.Location = new System.Drawing.Point(108, 344);
        this.PledgedSettled.Name = "PledgedSettled";
        this.PledgedSettled.Size = new System.Drawing.Size(78, 18);
        this.PledgedSettled.TabIndex = 100;
        this.PledgedSettled.Tag = null;
        this.PledgedSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ShortTraded
        // 
        this.ShortTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ShortTraded.DataType = typeof(long);
        this.ShortTraded.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ShortTraded.ForeColor = System.Drawing.Color.Silver;
        this.ShortTraded.FormatInfo.CustomFormat = "#,##0";
        this.ShortTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ShortTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ShortTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ShortTraded.Location = new System.Drawing.Point(192, 246);
        this.ShortTraded.Name = "ShortTraded";
        this.ShortTraded.Size = new System.Drawing.Size(78, 18);
        this.ShortTraded.TabIndex = 99;
        this.ShortTraded.Tag = null;
        this.ShortTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ShortSettled
        // 
        this.ShortSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ShortSettled.DataType = typeof(long);
        this.ShortSettled.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ShortSettled.ForeColor = System.Drawing.Color.White;
        this.ShortSettled.FormatInfo.CustomFormat = "#,##0";
        this.ShortSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ShortSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ShortSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ShortSettled.Location = new System.Drawing.Point(108, 246);
        this.ShortSettled.Name = "ShortSettled";
        this.ShortSettled.Size = new System.Drawing.Size(78, 18);
        this.ShortSettled.TabIndex = 98;
        this.ShortSettled.Tag = null;
        this.ShortSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // LongTraded
        // 
        this.LongTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.LongTraded.DataType = typeof(long);
        this.LongTraded.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LongTraded.ForeColor = System.Drawing.Color.Silver;
        this.LongTraded.FormatInfo.CustomFormat = "#,##0";
        this.LongTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LongTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.LongTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LongTraded.Location = new System.Drawing.Point(192, 168);
        this.LongTraded.Name = "LongTraded";
        this.LongTraded.Size = new System.Drawing.Size(78, 18);
        this.LongTraded.TabIndex = 97;
        this.LongTraded.Tag = null;
        this.LongTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // LongSettled
        // 
        this.LongSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.LongSettled.DataType = typeof(long);
        this.LongSettled.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LongSettled.ForeColor = System.Drawing.Color.White;
        this.LongSettled.FormatInfo.CustomFormat = "#,##0";
        this.LongSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LongSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.LongSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.LongSettled.Location = new System.Drawing.Point(108, 168);
        this.LongSettled.Name = "LongSettled";
        this.LongSettled.Size = new System.Drawing.Size(78, 18);
        this.LongSettled.TabIndex = 96;
        this.LongSettled.Tag = null;
        this.LongSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // c1Label17
        // 
        this.c1Label17.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label17.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label17.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label17.Location = new System.Drawing.Point(6, 124);
        this.c1Label17.Name = "c1Label17";
        this.c1Label17.Size = new System.Drawing.Size(54, 18);
        this.c1Label17.TabIndex = 95;
        this.c1Label17.Tag = null;
        this.c1Label17.Text = "Free";
        this.c1Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label17.TextDetached = true;
        // 
        // c1Label15
        // 
        this.c1Label15.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label15.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label15.Location = new System.Drawing.Point(6, 191);
        this.c1Label15.Name = "c1Label15";
        this.c1Label15.Size = new System.Drawing.Size(66, 18);
        this.c1Label15.TabIndex = 94;
        this.c1Label15.Tag = null;
        this.c1Label15.Text = "Customer:";
        this.c1Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label15.TextDetached = true;
        // 
        // c1Label16
        // 
        this.c1Label16.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label16.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label16.Location = new System.Drawing.Point(6, 215);
        this.c1Label16.Name = "c1Label16";
        this.c1Label16.Size = new System.Drawing.Size(54, 18);
        this.c1Label16.TabIndex = 93;
        this.c1Label16.Tag = null;
        this.c1Label16.Text = "Firm:";
        this.c1Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label16.TextDetached = true;
        // 
        // Label07
        // 
        this.Label07.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label07.ForeColor = System.Drawing.Color.Goldenrod;
        this.Label07.Location = new System.Drawing.Point(6, 168);
        this.Label07.Name = "Label07";
        this.Label07.Size = new System.Drawing.Size(42, 18);
        this.Label07.TabIndex = 92;
        this.Label07.Text = "Long";
        this.Label07.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // c1Label13
        // 
        this.c1Label13.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label13.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label13.Location = new System.Drawing.Point(6, 266);
        this.c1Label13.Name = "c1Label13";
        this.c1Label13.Size = new System.Drawing.Size(66, 18);
        this.c1Label13.TabIndex = 91;
        this.c1Label13.Tag = null;
        this.c1Label13.Text = "Customer:";
        this.c1Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label13.TextDetached = true;
        // 
        // c1Label14
        // 
        this.c1Label14.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label14.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label14.Location = new System.Drawing.Point(6, 290);
        this.c1Label14.Name = "c1Label14";
        this.c1Label14.Size = new System.Drawing.Size(54, 18);
        this.c1Label14.TabIndex = 90;
        this.c1Label14.Tag = null;
        this.c1Label14.Text = "Firm:";
        this.c1Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label14.TextDetached = true;
        // 
        // Label12
        // 
        this.Label12.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label12.ForeColor = System.Drawing.Color.Goldenrod;
        this.Label12.Location = new System.Drawing.Point(6, 344);
        this.Label12.Name = "Label12";
        this.Label12.Size = new System.Drawing.Size(84, 18);
        this.Label12.TabIndex = 89;
        this.Label12.Text = "Pledged";
        this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.Label12.Click += new System.EventHandler(this.PledgedLabel_Click);
        // 
        // FirmShortSettled
        // 
        this.FirmShortSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.FirmShortSettled.DataType = typeof(long);
        this.FirmShortSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FirmShortSettled.ForeColor = System.Drawing.Color.White;
        this.FirmShortSettled.FormatInfo.CustomFormat = "#,##0";
        this.FirmShortSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmShortSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.FirmShortSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmShortSettled.Location = new System.Drawing.Point(108, 290);
        this.FirmShortSettled.Name = "FirmShortSettled";
        this.FirmShortSettled.Size = new System.Drawing.Size(78, 18);
        this.FirmShortSettled.TabIndex = 88;
        this.FirmShortSettled.Tag = null;
        this.FirmShortSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // CustomerShortSettled
        // 
        this.CustomerShortSettled.BackColor = System.Drawing.Color.Transparent;
        this.CustomerShortSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CustomerShortSettled.DataType = typeof(long);
        this.CustomerShortSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CustomerShortSettled.ForeColor = System.Drawing.Color.White;
        this.CustomerShortSettled.FormatInfo.CustomFormat = "#,##0";
        this.CustomerShortSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerShortSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.CustomerShortSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerShortSettled.Location = new System.Drawing.Point(108, 266);
        this.CustomerShortSettled.Name = "CustomerShortSettled";
        this.CustomerShortSettled.Size = new System.Drawing.Size(78, 18);
        this.CustomerShortSettled.TabIndex = 87;
        this.CustomerShortSettled.Tag = null;
        this.CustomerShortSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // c1Label11
        // 
        this.c1Label11.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label11.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label11.Location = new System.Drawing.Point(6, 364);
        this.c1Label11.Name = "c1Label11";
        this.c1Label11.Size = new System.Drawing.Size(66, 18);
        this.c1Label11.TabIndex = 86;
        this.c1Label11.Tag = null;
        this.c1Label11.Text = "Customer:";
        this.c1Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label11.TextDetached = true;
        // 
        // c1Label12
        // 
        this.c1Label12.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label12.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label12.Location = new System.Drawing.Point(6, 388);
        this.c1Label12.Name = "c1Label12";
        this.c1Label12.Size = new System.Drawing.Size(54, 18);
        this.c1Label12.TabIndex = 85;
        this.c1Label12.Tag = null;
        this.c1Label12.Text = "Firm:";
        this.c1Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label12.TextDetached = true;
        // 
        // c1Label7
        // 
        this.c1Label7.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label7.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label7.Location = new System.Drawing.Point(6, 493);
        this.c1Label7.Name = "c1Label7";
        this.c1Label7.Size = new System.Drawing.Size(54, 18);
        this.c1Label7.TabIndex = 84;
        this.c1Label7.Tag = null;
        this.c1Label7.Text = "DVP:";
        this.c1Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label7.TextDetached = true;
        // 
        // c1Label8
        // 
        this.c1Label8.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label8.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label8.Location = new System.Drawing.Point(6, 445);
        this.c1Label8.Name = "c1Label8";
        this.c1Label8.Size = new System.Drawing.Size(60, 18);
        this.c1Label8.TabIndex = 83;
        this.c1Label8.Tag = null;
        this.c1Label8.Text = "Clearing:";
        this.c1Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label8.TextDetached = true;
        // 
        // c1Label9
        // 
        this.c1Label9.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label9.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label9.Location = new System.Drawing.Point(6, 469);
        this.c1Label9.Name = "c1Label9";
        this.c1Label9.Size = new System.Drawing.Size(54, 18);
        this.c1Label9.TabIndex = 82;
        this.c1Label9.Tag = null;
        this.c1Label9.Text = "Broker:";
        this.c1Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label9.TextDetached = true;
        // 
        // c1Label10
        // 
        this.c1Label10.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label10.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label10.Location = new System.Drawing.Point(6, 517);
        this.c1Label10.Name = "c1Label10";
        this.c1Label10.Size = new System.Drawing.Size(54, 18);
        this.c1Label10.TabIndex = 81;
        this.c1Label10.Tag = null;
        this.c1Label10.Text = "Other:";
        this.c1Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label10.TextDetached = true;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.ForeColor = System.Drawing.Color.Goldenrod;
        this.label1.Location = new System.Drawing.Point(6, 422);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(84, 18);
        this.label1.TabIndex = 80;
        this.label1.Text = "Deliver";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.label1.Click += new System.EventHandler(this.FailLabel_Click);
        // 
        // c1Label6
        // 
        this.c1Label6.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label6.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label6.Location = new System.Drawing.Point(6, 619);
        this.c1Label6.Name = "c1Label6";
        this.c1Label6.Size = new System.Drawing.Size(54, 18);
        this.c1Label6.TabIndex = 79;
        this.c1Label6.Tag = null;
        this.c1Label6.Text = "DVP:";
        this.c1Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label6.TextDetached = true;
        // 
        // c1Label5
        // 
        this.c1Label5.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label5.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label5.Location = new System.Drawing.Point(6, 571);
        this.c1Label5.Name = "c1Label5";
        this.c1Label5.Size = new System.Drawing.Size(60, 18);
        this.c1Label5.TabIndex = 78;
        this.c1Label5.Tag = null;
        this.c1Label5.Text = "Clearing:";
        this.c1Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label5.TextDetached = true;
        // 
        // c1Label4
        // 
        this.c1Label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label4.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label4.Location = new System.Drawing.Point(6, 595);
        this.c1Label4.Name = "c1Label4";
        this.c1Label4.Size = new System.Drawing.Size(54, 18);
        this.c1Label4.TabIndex = 77;
        this.c1Label4.Tag = null;
        this.c1Label4.Text = "Broker:";
        this.c1Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label4.TextDetached = true;
        // 
        // c1Label3
        // 
        this.c1Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label3.ForeColor = System.Drawing.Color.Goldenrod;
        this.c1Label3.Location = new System.Drawing.Point(6, 643);
        this.c1Label3.Name = "c1Label3";
        this.c1Label3.Size = new System.Drawing.Size(54, 18);
        this.c1Label3.TabIndex = 76;
        this.c1Label3.Tag = null;
        this.c1Label3.Text = "Other:";
        this.c1Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label3.TextDetached = true;
        // 
        // OtherFailInTraded
        // 
        this.OtherFailInTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.OtherFailInTraded.DataType = typeof(long);
        this.OtherFailInTraded.ForeColor = System.Drawing.Color.Silver;
        this.OtherFailInTraded.FormatInfo.CustomFormat = "#,##0";
        this.OtherFailInTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailInTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.OtherFailInTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailInTraded.Location = new System.Drawing.Point(192, 643);
        this.OtherFailInTraded.Name = "OtherFailInTraded";
        this.OtherFailInTraded.Size = new System.Drawing.Size(78, 18);
        this.OtherFailInTraded.TabIndex = 64;
        this.OtherFailInTraded.Tag = null;
        this.OtherFailInTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DvpFailInTraded
        // 
        this.DvpFailInTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DvpFailInTraded.DataType = typeof(long);
        this.DvpFailInTraded.ForeColor = System.Drawing.Color.Silver;
        this.DvpFailInTraded.FormatInfo.CustomFormat = "#,##0";
        this.DvpFailInTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailInTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DvpFailInTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailInTraded.Location = new System.Drawing.Point(192, 619);
        this.DvpFailInTraded.Name = "DvpFailInTraded";
        this.DvpFailInTraded.Size = new System.Drawing.Size(78, 18);
        this.DvpFailInTraded.TabIndex = 61;
        this.DvpFailInTraded.Tag = null;
        this.DvpFailInTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // BrokerFailInTraded
        // 
        this.BrokerFailInTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.BrokerFailInTraded.DataType = typeof(long);
        this.BrokerFailInTraded.ForeColor = System.Drawing.Color.Silver;
        this.BrokerFailInTraded.FormatInfo.CustomFormat = "#,##0";
        this.BrokerFailInTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailInTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.BrokerFailInTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailInTraded.Location = new System.Drawing.Point(192, 595);
        this.BrokerFailInTraded.Name = "BrokerFailInTraded";
        this.BrokerFailInTraded.Size = new System.Drawing.Size(78, 18);
        this.BrokerFailInTraded.TabIndex = 62;
        this.BrokerFailInTraded.Tag = null;
        this.BrokerFailInTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ClearingFailInTraded
        // 
        this.ClearingFailInTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ClearingFailInTraded.DataType = typeof(long);
        this.ClearingFailInTraded.ForeColor = System.Drawing.Color.Silver;
        this.ClearingFailInTraded.FormatInfo.CustomFormat = "#,##0";
        this.ClearingFailInTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailInTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ClearingFailInTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailInTraded.Location = new System.Drawing.Point(192, 571);
        this.ClearingFailInTraded.Name = "ClearingFailInTraded";
        this.ClearingFailInTraded.Size = new System.Drawing.Size(78, 18);
        this.ClearingFailInTraded.TabIndex = 63;
        this.ClearingFailInTraded.Tag = null;
        this.ClearingFailInTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // OtherFailInSettled
        // 
        this.OtherFailInSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.OtherFailInSettled.DataType = typeof(long);
        this.OtherFailInSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.OtherFailInSettled.ForeColor = System.Drawing.Color.White;
        this.OtherFailInSettled.FormatInfo.CustomFormat = "#,##0";
        this.OtherFailInSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailInSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.OtherFailInSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailInSettled.Location = new System.Drawing.Point(108, 643);
        this.OtherFailInSettled.Name = "OtherFailInSettled";
        this.OtherFailInSettled.Size = new System.Drawing.Size(78, 18);
        this.OtherFailInSettled.TabIndex = 66;
        this.OtherFailInSettled.Tag = null;
        this.OtherFailInSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ClearingFailInSettled
        // 
        this.ClearingFailInSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ClearingFailInSettled.DataType = typeof(long);
        this.ClearingFailInSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ClearingFailInSettled.ForeColor = System.Drawing.Color.White;
        this.ClearingFailInSettled.FormatInfo.CustomFormat = "#,##0";
        this.ClearingFailInSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailInSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ClearingFailInSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailInSettled.Location = new System.Drawing.Point(108, 571);
        this.ClearingFailInSettled.Name = "ClearingFailInSettled";
        this.ClearingFailInSettled.Size = new System.Drawing.Size(78, 18);
        this.ClearingFailInSettled.TabIndex = 65;
        this.ClearingFailInSettled.Tag = null;
        this.ClearingFailInSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // BrokerFailInSettled
        // 
        this.BrokerFailInSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.BrokerFailInSettled.DataType = typeof(long);
        this.BrokerFailInSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BrokerFailInSettled.ForeColor = System.Drawing.Color.White;
        this.BrokerFailInSettled.FormatInfo.CustomFormat = "#,##0";
        this.BrokerFailInSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailInSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.BrokerFailInSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailInSettled.Location = new System.Drawing.Point(108, 595);
        this.BrokerFailInSettled.Name = "BrokerFailInSettled";
        this.BrokerFailInSettled.Size = new System.Drawing.Size(78, 18);
        this.BrokerFailInSettled.TabIndex = 64;
        this.BrokerFailInSettled.Tag = null;
        this.BrokerFailInSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DvpFailInSettled
        // 
        this.DvpFailInSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DvpFailInSettled.DataType = typeof(long);
        this.DvpFailInSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DvpFailInSettled.ForeColor = System.Drawing.Color.White;
        this.DvpFailInSettled.FormatInfo.CustomFormat = "#,##0";
        this.DvpFailInSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailInSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DvpFailInSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailInSettled.Location = new System.Drawing.Point(108, 619);
        this.DvpFailInSettled.Name = "DvpFailInSettled";
        this.DvpFailInSettled.Size = new System.Drawing.Size(78, 18);
        this.DvpFailInSettled.TabIndex = 63;
        this.DvpFailInSettled.Tag = null;
        this.DvpFailInSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // OtherFailInDayCount
        // 
        this.OtherFailInDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.OtherFailInDayCount.DataType = typeof(long);
        this.OtherFailInDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.OtherFailInDayCount.FormatInfo.CustomFormat = "[0]";
        this.OtherFailInDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailInDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.OtherFailInDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailInDayCount.Location = new System.Drawing.Point(66, 643);
        this.OtherFailInDayCount.Name = "OtherFailInDayCount";
        this.OtherFailInDayCount.Size = new System.Drawing.Size(36, 18);
        this.OtherFailInDayCount.TabIndex = 74;
        this.OtherFailInDayCount.Tag = null;
        this.OtherFailInDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // DvpFailInDayCount
        // 
        this.DvpFailInDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DvpFailInDayCount.DataType = typeof(long);
        this.DvpFailInDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.DvpFailInDayCount.FormatInfo.CustomFormat = "[0]";
        this.DvpFailInDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailInDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DvpFailInDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailInDayCount.Location = new System.Drawing.Point(66, 619);
        this.DvpFailInDayCount.Name = "DvpFailInDayCount";
        this.DvpFailInDayCount.Size = new System.Drawing.Size(36, 18);
        this.DvpFailInDayCount.TabIndex = 73;
        this.DvpFailInDayCount.Tag = null;
        this.DvpFailInDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // BrokerFailInDayCount
        // 
        this.BrokerFailInDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.BrokerFailInDayCount.DataType = typeof(long);
        this.BrokerFailInDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.BrokerFailInDayCount.FormatInfo.CustomFormat = "[0]";
        this.BrokerFailInDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailInDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.BrokerFailInDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailInDayCount.Location = new System.Drawing.Point(66, 595);
        this.BrokerFailInDayCount.Name = "BrokerFailInDayCount";
        this.BrokerFailInDayCount.Size = new System.Drawing.Size(36, 18);
        this.BrokerFailInDayCount.TabIndex = 74;
        this.BrokerFailInDayCount.Tag = null;
        this.BrokerFailInDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // ClearingFailInDayCount
        // 
        this.ClearingFailInDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ClearingFailInDayCount.DataType = typeof(long);
        this.ClearingFailInDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.ClearingFailInDayCount.FormatInfo.CustomFormat = "[0]";
        this.ClearingFailInDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailInDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ClearingFailInDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailInDayCount.Location = new System.Drawing.Point(66, 571);
        this.ClearingFailInDayCount.Name = "ClearingFailInDayCount";
        this.ClearingFailInDayCount.Size = new System.Drawing.Size(36, 18);
        this.ClearingFailInDayCount.TabIndex = 75;
        this.ClearingFailInDayCount.Tag = null;
        this.ClearingFailInDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // BrokerFailInLabel
        // 
        this.BrokerFailInLabel.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BrokerFailInLabel.ForeColor = System.Drawing.Color.Goldenrod;
        this.BrokerFailInLabel.Location = new System.Drawing.Point(6, 548);
        this.BrokerFailInLabel.Name = "BrokerFailInLabel";
        this.BrokerFailInLabel.Size = new System.Drawing.Size(84, 18);
        this.BrokerFailInLabel.TabIndex = 54;
        this.BrokerFailInLabel.Text = "Receive";
        this.BrokerFailInLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.BrokerFailInLabel.Click += new System.EventHandler(this.FailLabel_Click);
        // 
        // OtherFailOutSettled
        // 
        this.OtherFailOutSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.OtherFailOutSettled.DataType = typeof(long);
        this.OtherFailOutSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.OtherFailOutSettled.ForeColor = System.Drawing.Color.White;
        this.OtherFailOutSettled.FormatInfo.CustomFormat = "#,##0";
        this.OtherFailOutSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailOutSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.OtherFailOutSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailOutSettled.Location = new System.Drawing.Point(108, 517);
        this.OtherFailOutSettled.Name = "OtherFailOutSettled";
        this.OtherFailOutSettled.Size = new System.Drawing.Size(78, 18);
        this.OtherFailOutSettled.TabIndex = 67;
        this.OtherFailOutSettled.Tag = null;
        this.OtherFailOutSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ClearingFailOutSettled
        // 
        this.ClearingFailOutSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ClearingFailOutSettled.DataType = typeof(long);
        this.ClearingFailOutSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ClearingFailOutSettled.ForeColor = System.Drawing.Color.White;
        this.ClearingFailOutSettled.FormatInfo.CustomFormat = "#,##0";
        this.ClearingFailOutSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailOutSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ClearingFailOutSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailOutSettled.Location = new System.Drawing.Point(108, 445);
        this.ClearingFailOutSettled.Name = "ClearingFailOutSettled";
        this.ClearingFailOutSettled.Size = new System.Drawing.Size(78, 18);
        this.ClearingFailOutSettled.TabIndex = 66;
        this.ClearingFailOutSettled.Tag = null;
        this.ClearingFailOutSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // BrokerFailOutSettled
        // 
        this.BrokerFailOutSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.BrokerFailOutSettled.DataType = typeof(long);
        this.BrokerFailOutSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BrokerFailOutSettled.ForeColor = System.Drawing.Color.White;
        this.BrokerFailOutSettled.FormatInfo.CustomFormat = "#,##0";
        this.BrokerFailOutSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailOutSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.BrokerFailOutSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailOutSettled.Location = new System.Drawing.Point(108, 469);
        this.BrokerFailOutSettled.Name = "BrokerFailOutSettled";
        this.BrokerFailOutSettled.Size = new System.Drawing.Size(78, 18);
        this.BrokerFailOutSettled.TabIndex = 65;
        this.BrokerFailOutSettled.Tag = null;
        this.BrokerFailOutSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DvpFailOutSettled
        // 
        this.DvpFailOutSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DvpFailOutSettled.DataType = typeof(long);
        this.DvpFailOutSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DvpFailOutSettled.ForeColor = System.Drawing.Color.White;
        this.DvpFailOutSettled.FormatInfo.CustomFormat = "#,##0";
        this.DvpFailOutSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailOutSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DvpFailOutSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailOutSettled.Location = new System.Drawing.Point(108, 493);
        this.DvpFailOutSettled.Name = "DvpFailOutSettled";
        this.DvpFailOutSettled.Size = new System.Drawing.Size(78, 18);
        this.DvpFailOutSettled.TabIndex = 64;
        this.DvpFailOutSettled.Tag = null;
        this.DvpFailOutSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ClearingFailOutTraded
        // 
        this.ClearingFailOutTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ClearingFailOutTraded.DataType = typeof(long);
        this.ClearingFailOutTraded.ForeColor = System.Drawing.Color.Silver;
        this.ClearingFailOutTraded.FormatInfo.CustomFormat = "#,##0";
        this.ClearingFailOutTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailOutTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ClearingFailOutTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailOutTraded.Location = new System.Drawing.Point(192, 445);
        this.ClearingFailOutTraded.Name = "ClearingFailOutTraded";
        this.ClearingFailOutTraded.Size = new System.Drawing.Size(78, 18);
        this.ClearingFailOutTraded.TabIndex = 64;
        this.ClearingFailOutTraded.Tag = null;
        this.ClearingFailOutTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // BrokerFailOutTraded
        // 
        this.BrokerFailOutTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.BrokerFailOutTraded.DataType = typeof(long);
        this.BrokerFailOutTraded.ForeColor = System.Drawing.Color.Silver;
        this.BrokerFailOutTraded.FormatInfo.CustomFormat = "#,##0";
        this.BrokerFailOutTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailOutTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.BrokerFailOutTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailOutTraded.Location = new System.Drawing.Point(192, 469);
        this.BrokerFailOutTraded.Name = "BrokerFailOutTraded";
        this.BrokerFailOutTraded.Size = new System.Drawing.Size(78, 18);
        this.BrokerFailOutTraded.TabIndex = 63;
        this.BrokerFailOutTraded.Tag = null;
        this.BrokerFailOutTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DvpFailOutTraded
        // 
        this.DvpFailOutTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DvpFailOutTraded.DataType = typeof(long);
        this.DvpFailOutTraded.ForeColor = System.Drawing.Color.Silver;
        this.DvpFailOutTraded.FormatInfo.CustomFormat = "#,##0";
        this.DvpFailOutTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailOutTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DvpFailOutTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailOutTraded.Location = new System.Drawing.Point(192, 493);
        this.DvpFailOutTraded.Name = "DvpFailOutTraded";
        this.DvpFailOutTraded.Size = new System.Drawing.Size(78, 18);
        this.DvpFailOutTraded.TabIndex = 62;
        this.DvpFailOutTraded.Tag = null;
        this.DvpFailOutTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // OtherFailOutTraded
        // 
        this.OtherFailOutTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.OtherFailOutTraded.DataType = typeof(long);
        this.OtherFailOutTraded.ForeColor = System.Drawing.Color.Silver;
        this.OtherFailOutTraded.FormatInfo.CustomFormat = "#,##0";
        this.OtherFailOutTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailOutTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.OtherFailOutTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailOutTraded.Location = new System.Drawing.Point(192, 517);
        this.OtherFailOutTraded.Name = "OtherFailOutTraded";
        this.OtherFailOutTraded.Size = new System.Drawing.Size(78, 18);
        this.OtherFailOutTraded.TabIndex = 65;
        this.OtherFailOutTraded.Tag = null;
        this.OtherFailOutTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DvpFailOutDayCount
        // 
        this.DvpFailOutDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DvpFailOutDayCount.DataType = typeof(long);
        this.DvpFailOutDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.DvpFailOutDayCount.FormatInfo.CustomFormat = "[0]";
        this.DvpFailOutDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailOutDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DvpFailOutDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DvpFailOutDayCount.Location = new System.Drawing.Point(66, 493);
        this.DvpFailOutDayCount.Name = "DvpFailOutDayCount";
        this.DvpFailOutDayCount.Size = new System.Drawing.Size(36, 18);
        this.DvpFailOutDayCount.TabIndex = 68;
        this.DvpFailOutDayCount.Tag = null;
        this.DvpFailOutDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // BrokerFailOutDayCount
        // 
        this.BrokerFailOutDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.BrokerFailOutDayCount.DataType = typeof(long);
        this.BrokerFailOutDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.BrokerFailOutDayCount.FormatInfo.CustomFormat = "[0]";
        this.BrokerFailOutDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailOutDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.BrokerFailOutDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.BrokerFailOutDayCount.Location = new System.Drawing.Point(66, 469);
        this.BrokerFailOutDayCount.Name = "BrokerFailOutDayCount";
        this.BrokerFailOutDayCount.Size = new System.Drawing.Size(36, 18);
        this.BrokerFailOutDayCount.TabIndex = 69;
        this.BrokerFailOutDayCount.Tag = null;
        this.BrokerFailOutDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // ClearingFailOutDayCount
        // 
        this.ClearingFailOutDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ClearingFailOutDayCount.DataType = typeof(long);
        this.ClearingFailOutDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.ClearingFailOutDayCount.FormatInfo.CustomFormat = "[0]";
        this.ClearingFailOutDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailOutDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ClearingFailOutDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ClearingFailOutDayCount.Location = new System.Drawing.Point(66, 445);
        this.ClearingFailOutDayCount.Name = "ClearingFailOutDayCount";
        this.ClearingFailOutDayCount.Size = new System.Drawing.Size(36, 18);
        this.ClearingFailOutDayCount.TabIndex = 70;
        this.ClearingFailOutDayCount.Tag = null;
        this.ClearingFailOutDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // OtherFailOutDayCount
        // 
        this.OtherFailOutDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.OtherFailOutDayCount.DataType = typeof(long);
        this.OtherFailOutDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.OtherFailOutDayCount.FormatInfo.CustomFormat = "[0]";
        this.OtherFailOutDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailOutDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.OtherFailOutDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.OtherFailOutDayCount.Location = new System.Drawing.Point(66, 517);
        this.OtherFailOutDayCount.Name = "OtherFailOutDayCount";
        this.OtherFailOutDayCount.Size = new System.Drawing.Size(36, 18);
        this.OtherFailOutDayCount.TabIndex = 71;
        this.OtherFailOutDayCount.Tag = null;
        this.OtherFailOutDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // CustomerPledgeSettled
        // 
        this.CustomerPledgeSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CustomerPledgeSettled.DataType = typeof(long);
        this.CustomerPledgeSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CustomerPledgeSettled.ForeColor = System.Drawing.Color.White;
        this.CustomerPledgeSettled.FormatInfo.CustomFormat = "#,##0";
        this.CustomerPledgeSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerPledgeSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.CustomerPledgeSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerPledgeSettled.Location = new System.Drawing.Point(108, 364);
        this.CustomerPledgeSettled.Name = "CustomerPledgeSettled";
        this.CustomerPledgeSettled.Size = new System.Drawing.Size(78, 18);
        this.CustomerPledgeSettled.TabIndex = 73;
        this.CustomerPledgeSettled.Tag = null;
        this.CustomerPledgeSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // FirmPledgeSettled
        // 
        this.FirmPledgeSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.FirmPledgeSettled.DataType = typeof(long);
        this.FirmPledgeSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FirmPledgeSettled.ForeColor = System.Drawing.Color.White;
        this.FirmPledgeSettled.FormatInfo.CustomFormat = "#,##0";
        this.FirmPledgeSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmPledgeSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.FirmPledgeSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmPledgeSettled.Location = new System.Drawing.Point(108, 388);
        this.FirmPledgeSettled.Name = "FirmPledgeSettled";
        this.FirmPledgeSettled.Size = new System.Drawing.Size(78, 18);
        this.FirmPledgeSettled.TabIndex = 74;
        this.FirmPledgeSettled.Tag = null;
        this.FirmPledgeSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // CustomerPledgeTraded
        // 
        this.CustomerPledgeTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CustomerPledgeTraded.DataType = typeof(long);
        this.CustomerPledgeTraded.ForeColor = System.Drawing.Color.Silver;
        this.CustomerPledgeTraded.FormatInfo.CustomFormat = "#,##0";
        this.CustomerPledgeTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerPledgeTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.CustomerPledgeTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerPledgeTraded.Location = new System.Drawing.Point(192, 364);
        this.CustomerPledgeTraded.Name = "CustomerPledgeTraded";
        this.CustomerPledgeTraded.Size = new System.Drawing.Size(78, 18);
        this.CustomerPledgeTraded.TabIndex = 69;
        this.CustomerPledgeTraded.Tag = null;
        this.CustomerPledgeTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // FirmPledgeTraded
        // 
        this.FirmPledgeTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.FirmPledgeTraded.DataType = typeof(long);
        this.FirmPledgeTraded.ForeColor = System.Drawing.Color.Silver;
        this.FirmPledgeTraded.FormatInfo.CustomFormat = "#,##0";
        this.FirmPledgeTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmPledgeTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.FirmPledgeTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmPledgeTraded.Location = new System.Drawing.Point(192, 388);
        this.FirmPledgeTraded.Name = "FirmPledgeTraded";
        this.FirmPledgeTraded.Size = new System.Drawing.Size(78, 18);
        this.FirmPledgeTraded.TabIndex = 68;
        this.FirmPledgeTraded.Tag = null;
        this.FirmPledgeTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // CustomerShortTraded
        // 
        this.CustomerShortTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CustomerShortTraded.DataType = typeof(long);
        this.CustomerShortTraded.ForeColor = System.Drawing.Color.Silver;
        this.CustomerShortTraded.FormatInfo.CustomFormat = "#,##0";
        this.CustomerShortTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerShortTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.CustomerShortTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerShortTraded.Location = new System.Drawing.Point(192, 266);
        this.CustomerShortTraded.Name = "CustomerShortTraded";
        this.CustomerShortTraded.Size = new System.Drawing.Size(78, 18);
        this.CustomerShortTraded.TabIndex = 72;
        this.CustomerShortTraded.Tag = null;
        this.CustomerShortTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // FirmShortTraded
        // 
        this.FirmShortTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.FirmShortTraded.DataType = typeof(long);
        this.FirmShortTraded.ForeColor = System.Drawing.Color.Silver;
        this.FirmShortTraded.FormatInfo.CustomFormat = "#,##0";
        this.FirmShortTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmShortTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.FirmShortTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmShortTraded.Location = new System.Drawing.Point(192, 290);
        this.FirmShortTraded.Name = "FirmShortTraded";
        this.FirmShortTraded.Size = new System.Drawing.Size(78, 18);
        this.FirmShortTraded.TabIndex = 73;
        this.FirmShortTraded.Tag = null;
        this.FirmShortTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // Label08
        // 
        this.Label08.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label08.ForeColor = System.Drawing.Color.Goldenrod;
        this.Label08.Location = new System.Drawing.Point(6, 246);
        this.Label08.Name = "Label08";
        this.Label08.Size = new System.Drawing.Size(84, 18);
        this.Label08.TabIndex = 45;
        this.Label08.Text = "Short";
        this.Label08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // CustomerLongSettled
        // 
        this.CustomerLongSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CustomerLongSettled.DataType = typeof(long);
        this.CustomerLongSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CustomerLongSettled.ForeColor = System.Drawing.Color.White;
        this.CustomerLongSettled.FormatInfo.CustomFormat = "#,##0";
        this.CustomerLongSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerLongSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.CustomerLongSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerLongSettled.Location = new System.Drawing.Point(108, 191);
        this.CustomerLongSettled.Name = "CustomerLongSettled";
        this.CustomerLongSettled.Size = new System.Drawing.Size(78, 18);
        this.CustomerLongSettled.TabIndex = 62;
        this.CustomerLongSettled.Tag = null;
        this.CustomerLongSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // CustomerLongTraded
        // 
        this.CustomerLongTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CustomerLongTraded.DataType = typeof(long);
        this.CustomerLongTraded.ForeColor = System.Drawing.Color.Silver;
        this.CustomerLongTraded.FormatInfo.CustomFormat = "#,##0";
        this.CustomerLongTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerLongTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.CustomerLongTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.CustomerLongTraded.Location = new System.Drawing.Point(192, 191);
        this.CustomerLongTraded.Name = "CustomerLongTraded";
        this.CustomerLongTraded.Size = new System.Drawing.Size(78, 18);
        this.CustomerLongTraded.TabIndex = 63;
        this.CustomerLongTraded.Tag = null;
        this.CustomerLongTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // FirmLongSettled
        // 
        this.FirmLongSettled.BackColor = System.Drawing.Color.Transparent;
        this.FirmLongSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.FirmLongSettled.DataType = typeof(long);
        this.FirmLongSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FirmLongSettled.ForeColor = System.Drawing.Color.White;
        this.FirmLongSettled.FormatInfo.CustomFormat = "#,##0";
        this.FirmLongSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmLongSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.FirmLongSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmLongSettled.Location = new System.Drawing.Point(108, 215);
        this.FirmLongSettled.Name = "FirmLongSettled";
        this.FirmLongSettled.Size = new System.Drawing.Size(78, 18);
        this.FirmLongSettled.TabIndex = 68;
        this.FirmLongSettled.Tag = null;
        this.FirmLongSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // FirmLongTraded
        // 
        this.FirmLongTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.FirmLongTraded.DataType = typeof(long);
        this.FirmLongTraded.ForeColor = System.Drawing.Color.Silver;
        this.FirmLongTraded.FormatInfo.CustomFormat = "#,##0";
        this.FirmLongTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmLongTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.FirmLongTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.FirmLongTraded.Location = new System.Drawing.Point(192, 215);
        this.FirmLongTraded.Name = "FirmLongTraded";
        this.FirmLongTraded.Size = new System.Drawing.Size(78, 18);
        this.FirmLongTraded.TabIndex = 69;
        this.FirmLongTraded.Tag = null;
        this.FirmLongTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ExDeficitSettled
        // 
        this.ExDeficitSettled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ExDeficitSettled.DataType = typeof(long);
        this.ExDeficitSettled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ExDeficitSettled.ForeColor = System.Drawing.Color.White;
        this.ExDeficitSettled.FormatInfo.CustomFormat = "#,##0";
        this.ExDeficitSettled.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ExDeficitSettled.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ExDeficitSettled.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ExDeficitSettled.Location = new System.Drawing.Point(108, 124);
        this.ExDeficitSettled.Name = "ExDeficitSettled";
        this.ExDeficitSettled.Size = new System.Drawing.Size(78, 18);
        this.ExDeficitSettled.TabIndex = 60;
        this.ExDeficitSettled.Tag = null;
        this.ExDeficitSettled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // ExDeficitTraded
        // 
        this.ExDeficitTraded.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.ExDeficitTraded.DataType = typeof(long);
        this.ExDeficitTraded.ForeColor = System.Drawing.Color.Silver;
        this.ExDeficitTraded.FormatInfo.CustomFormat = "#,##0";
        this.ExDeficitTraded.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ExDeficitTraded.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.ExDeficitTraded.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.ExDeficitTraded.Location = new System.Drawing.Point(192, 124);
        this.ExDeficitTraded.Name = "ExDeficitTraded";
        this.ExDeficitTraded.Size = new System.Drawing.Size(78, 18);
        this.ExDeficitTraded.TabIndex = 61;
        this.ExDeficitTraded.Tag = null;
        this.ExDeficitTraded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DeficitDayCount
        // 
        this.DeficitDayCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DeficitDayCount.DataType = typeof(long);
        this.DeficitDayCount.ForeColor = System.Drawing.Color.Khaki;
        this.DeficitDayCount.FormatInfo.CustomFormat = "[0]";
        this.DeficitDayCount.FormatInfo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DeficitDayCount.FormatInfo.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
        this.DeficitDayCount.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DeficitDayCount.Location = new System.Drawing.Point(66, 124);
        this.DeficitDayCount.Name = "DeficitDayCount";
        this.DeficitDayCount.Size = new System.Drawing.Size(36, 18);
        this.DeficitDayCount.TabIndex = 56;
        this.DeficitDayCount.Tag = null;
        this.DeficitDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label3
        // 
        this.label3.BackColor = System.Drawing.Color.Silver;
        this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.label3.Dock = System.Windows.Forms.DockStyle.Top;
        this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(57)))), ((int)(((byte)(173)))));
        this.label3.Location = new System.Drawing.Point(0, 0);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(276, 96);
        this.label3.TabIndex = 85;
        // 
        // ThresholdDayCountLabel
        // 
        this.ThresholdDayCountLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ThresholdDayCountLabel.ForeColor = System.Drawing.Color.Goldenrod;
        this.ThresholdDayCountLabel.Location = new System.Drawing.Point(232, 316);
        this.ThresholdDayCountLabel.Name = "ThresholdDayCountLabel";
        this.ThresholdDayCountLabel.Size = new System.Drawing.Size(32, 14);
        this.ThresholdDayCountLabel.TabIndex = 34;
        this.ThresholdDayCountLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // DividendLabel
        // 
        this.DividendLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.DividendLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DividendLabel.ForeColor = System.Drawing.Color.Goldenrod;
        this.DividendLabel.Location = new System.Drawing.Point(4, 268);
        this.DividendLabel.Name = "DividendLabel";
        this.DividendLabel.Size = new System.Drawing.Size(68, 12);
        this.DividendLabel.TabIndex = 29;
        this.DividendLabel.Tag = null;
        this.DividendLabel.Text = "Dividend:";
        this.DividendLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.DividendLabel.TextDetached = true;
        // 
        // DividendText
        // 
        this.DividendText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.DividendText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.DividendText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DividendText.ForeColor = System.Drawing.Color.Black;
        this.DividendText.Location = new System.Drawing.Point(76, 264);
        this.DividendText.MaxLength = 9;
        this.DividendText.Name = "DividendText";
        this.DividendText.Size = new System.Drawing.Size(72, 19);
        this.DividendText.TabIndex = 15;
        this.DividendText.TabStop = false;
        this.DividendText.Tag = null;
        this.DividendText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.DividendText.TextDetached = true;
        this.DividendText.TrimStart = true;
        this.DividendText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.DividendText.WordWrap = false;
        // 
        // PriceLabel
        // 
        this.PriceLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.PriceLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.PriceLabel.ForeColor = System.Drawing.Color.Black;
        this.PriceLabel.Location = new System.Drawing.Point(14, 72);
        this.PriceLabel.Name = "PriceLabel";
        this.PriceLabel.Size = new System.Drawing.Size(44, 12);
        this.PriceLabel.TabIndex = 27;
        this.PriceLabel.Tag = null;
        this.PriceLabel.Text = "Price";
        this.PriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.PriceLabel.TextDetached = true;
        // 
        // PriceText
        // 
        this.PriceText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.PriceText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.PriceText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.PriceText.ForeColor = System.Drawing.Color.Black;
        this.PriceText.Location = new System.Drawing.Point(64, 72);
        this.PriceText.MaxLength = 9;
        this.PriceText.Name = "PriceText";
        this.PriceText.Size = new System.Drawing.Size(128, 19);
        this.PriceText.TabIndex = 13;
        this.PriceText.TabStop = false;
        this.PriceText.Tag = null;
        this.PriceText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.PriceText.TextDetached = true;
        this.PriceText.TrimStart = true;
        this.PriceText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.PriceText.WordWrap = false;
        // 
        // RecordDateLabel
        // 
        this.RecordDateLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.RecordDateLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.RecordDateLabel.ForeColor = System.Drawing.Color.Goldenrod;
        this.RecordDateLabel.Location = new System.Drawing.Point(12, 244);
        this.RecordDateLabel.Name = "RecordDateLabel";
        this.RecordDateLabel.Size = new System.Drawing.Size(60, 12);
        this.RecordDateLabel.TabIndex = 28;
        this.RecordDateLabel.Tag = null;
        this.RecordDateLabel.Text = "Record:";
        this.RecordDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.RecordDateLabel.TextDetached = true;
        // 
        // RecordDateText
        // 
        this.RecordDateText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.RecordDateText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.RecordDateText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.RecordDateText.ForeColor = System.Drawing.Color.Black;
        this.RecordDateText.Location = new System.Drawing.Point(76, 240);
        this.RecordDateText.MaxLength = 9;
        this.RecordDateText.Name = "RecordDateText";
        this.RecordDateText.Size = new System.Drawing.Size(88, 19);
        this.RecordDateText.TabIndex = 14;
        this.RecordDateText.TabStop = false;
        this.RecordDateText.Tag = null;
        this.RecordDateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.RecordDateText.TextDetached = true;
        this.RecordDateText.TrimStart = true;
        this.RecordDateText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.RecordDateText.WordWrap = false;
        // 
        // CusipLabel
        // 
        this.CusipLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.CusipLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CusipLabel.Location = new System.Drawing.Point(10, 24);
        this.CusipLabel.Name = "CusipLabel";
        this.CusipLabel.Size = new System.Drawing.Size(48, 12);
        this.CusipLabel.TabIndex = 22;
        this.CusipLabel.Tag = null;
        this.CusipLabel.Text = "CUSIP:";
        this.CusipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.CusipLabel.TextDetached = true;
        // 
        // CusipText
        // 
        this.CusipText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.CusipText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.CusipText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CusipText.Location = new System.Drawing.Point(64, 21);
        this.CusipText.MaxLength = 9;
        this.CusipText.Name = "CusipText";
        this.CusipText.Size = new System.Drawing.Size(78, 19);
        this.CusipText.TabIndex = 7;
        this.CusipText.TabStop = false;
        this.CusipText.Tag = null;
        this.CusipText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.CusipText.TextDetached = true;
        this.CusipText.TrimStart = true;
        this.CusipText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.CusipText.WordWrap = false;
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(0, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(100, 23);
        this.label2.TabIndex = 0;
        // 
        // LoadDateTime
        // 
        this.LoadDateTime.BackColor = System.Drawing.SystemColors.HighlightText;
        this.LoadDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.LoadDateTime.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LoadDateTime.ForeColor = System.Drawing.Color.Black;
        this.LoadDateTime.Location = new System.Drawing.Point(138, 5);
        this.LoadDateTime.Name = "LoadDateTime";
        this.LoadDateTime.Size = new System.Drawing.Size(122, 20);
        this.LoadDateTime.TabIndex = 68;
        this.LoadDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // CusipPanel
        // 
        this.CusipPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(41)))), ((int)(((byte)(107)))));
        this.CusipPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.CusipPanel.Controls.Add(this.BookGroupCombo);
        this.CusipPanel.Controls.Add(this.LoadDateTime);
        this.CusipPanel.Controls.Add(this.FindTextBox);
        this.CusipPanel.Controls.Add(this.FindLabel);
        this.CusipPanel.Controls.Add(this.BookLabel);
        this.CusipPanel.Controls.Add(this.RateIndicatorText);
        this.CusipPanel.Controls.Add(this.RateIndicatorLabel);
        this.CusipPanel.Controls.Add(this.DeskQuipCombo);
        this.CusipPanel.Controls.Add(this.DtcCheck);
        this.CusipPanel.Controls.Add(this.label5);
        this.CusipPanel.Controls.Add(this.RateLoanTextBox);
        this.CusipPanel.Controls.Add(this.RateBorrowTextBox);
        this.CusipPanel.Controls.Add(this.NoLendCheck);
        this.CusipPanel.Controls.Add(this.HardCheck);
        this.CusipPanel.Controls.Add(this.ThresholdCheck);
        this.CusipPanel.Controls.Add(this.EasyCheck);
        this.CusipPanel.Controls.Add(this.CusipGroupBox);
        this.CusipPanel.Controls.Add(this.ThresholdDayCountLabel);
        this.CusipPanel.Controls.Add(this.RecordDateLabel);
        this.CusipPanel.Controls.Add(this.RecordDateText);
        this.CusipPanel.Controls.Add(this.DividendLabel);
        this.CusipPanel.Controls.Add(this.DividendText);
        this.CusipPanel.Controls.Add(this.label4);
        this.CusipPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.CusipPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.CusipPanel.Location = new System.Drawing.Point(1, 1);
        this.CusipPanel.Name = "CusipPanel";
        this.CusipPanel.Size = new System.Drawing.Size(278, 399);
        this.CusipPanel.TabIndex = 36;
        // 
        // BookGroupCombo
        // 
        this.BookGroupCombo.AddItemSeparator = ';';
        this.BookGroupCombo.AllowColMove = false;
        this.BookGroupCombo.AutoCompletion = true;
        this.BookGroupCombo.AutoDropDown = true;
        this.BookGroupCombo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.BookGroupCombo.Caption = "";
        this.BookGroupCombo.CaptionHeight = 17;
        this.BookGroupCombo.CaptionVisible = false;
        this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.BookGroupCombo.ColumnCaptionHeight = 17;
        this.BookGroupCombo.ColumnFooterHeight = 17;
        this.BookGroupCombo.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
        this.BookGroupCombo.ContentHeight = 17;
        this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
        this.BookGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
        this.BookGroupCombo.DropDownWidth = 400;
        this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.ControlLightLight;
        this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BookGroupCombo.EditorForeColor = System.Drawing.Color.Black;
        this.BookGroupCombo.EditorHeight = 17;
        this.BookGroupCombo.EmptyRows = true;
        this.BookGroupCombo.ExtendRightColumn = true;
        this.BookGroupCombo.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images"))));
        this.BookGroupCombo.ItemHeight = 15;
        this.BookGroupCombo.LimitToList = true;
        this.BookGroupCombo.Location = new System.Drawing.Point(60, 5);
        this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
        this.BookGroupCombo.MaxDropDownItems = ((short)(25));
        this.BookGroupCombo.MaxLength = 32767;
        this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
        this.BookGroupCombo.Name = "BookGroupCombo";
        this.BookGroupCombo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
        this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
        this.BookGroupCombo.Size = new System.Drawing.Size(70, 21);
        this.BookGroupCombo.TabIndex = 35;
        this.BookGroupCombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.BookGroupCombo.ValueMember = "Text";
        this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
        this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
        // 
        // FindTextBox
        // 
        this.FindTextBox.BackColor = System.Drawing.Color.White;
        this.FindTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.FindTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.FindTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FindTextBox.ForeColor = System.Drawing.Color.Black;
        this.FindTextBox.Location = new System.Drawing.Point(62, 45);
        this.FindTextBox.Name = "FindTextBox";
        this.FindTextBox.Size = new System.Drawing.Size(200, 21);
        this.FindTextBox.TabIndex = 83;
        this.FindTextBox.Enter += new System.EventHandler(this.FindTextBox_Enter);
        this.FindTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindTextBox_KeyPress);
        this.FindTextBox.MouseEnter += new System.EventHandler(this.FindTextBox_MouseEnter);
        // 
        // FindLabel
        // 
        this.FindLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
        this.FindLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.FindLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FindLabel.ForeColor = System.Drawing.Color.Goldenrod;
        this.FindLabel.Location = new System.Drawing.Point(4, 40);
        this.FindLabel.Name = "FindLabel";
        this.FindLabel.Size = new System.Drawing.Size(268, 30);
        this.FindLabel.TabIndex = 84;
        this.FindLabel.Tag = null;
        this.FindLabel.Text = "Find:";
        this.FindLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.FindLabel.TextDetached = true;
        // 
        // BookLabel
        // 
        this.BookLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
        this.BookLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.BookLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BookLabel.ForeColor = System.Drawing.Color.Goldenrod;
        this.BookLabel.Location = new System.Drawing.Point(4, 0);
        this.BookLabel.Name = "BookLabel";
        this.BookLabel.Size = new System.Drawing.Size(268, 29);
        this.BookLabel.TabIndex = 36;
        this.BookLabel.Tag = null;
        this.BookLabel.Text = "Book:";
        this.BookLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.BookLabel.TextDetached = true;
        // 
        // RateIndicatorText
        // 
        this.RateIndicatorText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.RateIndicatorText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.RateIndicatorText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.RateIndicatorText.ForeColor = System.Drawing.Color.Black;
        this.RateIndicatorText.FormatType = C1.Win.C1Input.FormatTypeEnum.UseEvent;
        this.RateIndicatorText.Location = new System.Drawing.Point(76, 288);
        this.RateIndicatorText.MaxLength = 9;
        this.RateIndicatorText.Name = "RateIndicatorText";
        this.RateIndicatorText.Size = new System.Drawing.Size(54, 19);
        this.RateIndicatorText.TabIndex = 87;
        this.RateIndicatorText.TabStop = false;
        this.RateIndicatorText.Tag = null;
        this.RateIndicatorText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.RateIndicatorText.TextDetached = true;
        this.RateIndicatorText.TrimStart = true;
        this.RateIndicatorText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.RateIndicatorText.WordWrap = false;
        this.RateIndicatorText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RateIndicatorText_KeyPress);
        // 
        // RateIndicatorLabel
        // 
        this.RateIndicatorLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.RateIndicatorLabel.ForeColor = System.Drawing.Color.Goldenrod;
        this.RateIndicatorLabel.Location = new System.Drawing.Point(8, 290);
        this.RateIndicatorLabel.Name = "RateIndicatorLabel";
        this.RateIndicatorLabel.Size = new System.Drawing.Size(64, 14);
        this.RateIndicatorLabel.TabIndex = 88;
        this.RateIndicatorLabel.Text = "AZTEC:";
        this.RateIndicatorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // DeskQuipCombo
        // 
        this.DeskQuipCombo.AddItemSeparator = ';';
        this.DeskQuipCombo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.DeskQuipCombo.Caption = "";
        this.DeskQuipCombo.CaptionHeight = 17;
        this.DeskQuipCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
        this.DeskQuipCombo.ColumnCaptionHeight = 17;
        this.DeskQuipCombo.ColumnFooterHeight = 17;
        this.DeskQuipCombo.ContentHeight = 17;
        this.DeskQuipCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
        this.DeskQuipCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
        this.DeskQuipCombo.DropDownWidth = 650;
        this.DeskQuipCombo.EditorBackColor = System.Drawing.SystemColors.Window;
        this.DeskQuipCombo.EditorFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DeskQuipCombo.EditorForeColor = System.Drawing.Color.Maroon;
        this.DeskQuipCombo.EditorHeight = 17;
        this.DeskQuipCombo.Enabled = false;
        this.DeskQuipCombo.ExtendRightColumn = true;
        this.DeskQuipCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DeskQuipCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("DeskQuipCombo.Images"))));
        this.DeskQuipCombo.ItemHeight = 15;
        this.DeskQuipCombo.Location = new System.Drawing.Point(4, 356);
        this.DeskQuipCombo.MatchEntryTimeout = ((long)(2000));
        this.DeskQuipCombo.MaxDropDownItems = ((short)(25));
        this.DeskQuipCombo.MaxLength = 32767;
        this.DeskQuipCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
        this.DeskQuipCombo.Name = "DeskQuipCombo";
        this.DeskQuipCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
        this.DeskQuipCombo.RowSubDividerColor = System.Drawing.Color.Gainsboro;
        this.DeskQuipCombo.Size = new System.Drawing.Size(268, 21);
        this.DeskQuipCombo.TabIndex = 86;
        this.DeskQuipCombo.ValueMember = "Text";
        this.DeskQuipCombo.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.DeskQuipCombo_FormatText);
        this.DeskQuipCombo.UnboundColumnFetch += new C1.Win.C1List.UnboundColumnFetchEventHandler(this.DeskQuipCombo_UnboundColumnFetch);
        this.DeskQuipCombo.Close += new C1.Win.C1List.CloseEventHandler(this.DeskQuipCombo_Close);
        this.DeskQuipCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DeskQuipCombo_KeyPress);
        this.DeskQuipCombo.Leave += new System.EventHandler(this.DeskQuipCombo_Leave);
        this.DeskQuipCombo.PropBag = resources.GetString("DeskQuipCombo.PropBag");
        // 
        // DtcCheck
        // 
        this.DtcCheck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DtcCheck.ForeColor = System.Drawing.Color.Goldenrod;
        this.DtcCheck.Location = new System.Drawing.Point(180, 244);
        this.DtcCheck.Name = "DtcCheck";
        this.DtcCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.DtcCheck.Size = new System.Drawing.Size(52, 12);
        this.DtcCheck.TabIndex = 85;
        this.DtcCheck.Text = "DTC";
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.ForeColor = System.Drawing.Color.Goldenrod;
        this.label5.Location = new System.Drawing.Point(180, 268);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(36, 14);
        this.label5.TabIndex = 82;
        this.label5.Text = "B";
        this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // RateLoanTextBox
        // 
        this.RateLoanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.RateLoanTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.RateLoanTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.RateLoanTextBox.ForeColor = System.Drawing.Color.Black;
        this.RateLoanTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.UseEvent;
        this.RateLoanTextBox.Location = new System.Drawing.Point(218, 288);
        this.RateLoanTextBox.MaxLength = 9;
        this.RateLoanTextBox.Name = "RateLoanTextBox";
        this.RateLoanTextBox.Size = new System.Drawing.Size(54, 19);
        this.RateLoanTextBox.TabIndex = 80;
        this.RateLoanTextBox.TabStop = false;
        this.RateLoanTextBox.Tag = null;
        this.RateLoanTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.RateLoanTextBox.TrimStart = true;
        this.RateLoanTextBox.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.RateLoanTextBox.WordWrap = false;
        this.RateLoanTextBox.Formatting += new C1.Win.C1Input.FormatEventHandler(this.RateLoanTextBox_Formatting);
        // 
        // RateBorrowTextBox
        // 
        this.RateBorrowTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.RateBorrowTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.RateBorrowTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.RateBorrowTextBox.ForeColor = System.Drawing.Color.Black;
        this.RateBorrowTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.UseEvent;
        this.RateBorrowTextBox.Location = new System.Drawing.Point(218, 264);
        this.RateBorrowTextBox.MaxLength = 9;
        this.RateBorrowTextBox.Name = "RateBorrowTextBox";
        this.RateBorrowTextBox.Size = new System.Drawing.Size(54, 19);
        this.RateBorrowTextBox.TabIndex = 77;
        this.RateBorrowTextBox.TabStop = false;
        this.RateBorrowTextBox.Tag = null;
        this.RateBorrowTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.RateBorrowTextBox.TrimStart = true;
        this.RateBorrowTextBox.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.RateBorrowTextBox.WordWrap = false;
        this.RateBorrowTextBox.Formatting += new C1.Win.C1Input.FormatEventHandler(this.RateBorrowTextBox_Formatting);
        // 
        // NoLendCheck
        // 
        this.NoLendCheck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.NoLendCheck.ForeColor = System.Drawing.Color.Goldenrod;
        this.NoLendCheck.Location = new System.Drawing.Point(10, 336);
        this.NoLendCheck.Name = "NoLendCheck";
        this.NoLendCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.NoLendCheck.Size = new System.Drawing.Size(80, 12);
        this.NoLendCheck.TabIndex = 72;
        this.NoLendCheck.Text = "NO LEND";
        this.NoLendCheck.CheckedChanged += new System.EventHandler(this.NoLendCheck_CheckedChanged);
        this.NoLendCheck.Click += new System.EventHandler(this.NoLendCheck_Click);
        // 
        // HardCheck
        // 
        this.HardCheck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.HardCheck.ForeColor = System.Drawing.Color.Goldenrod;
        this.HardCheck.Location = new System.Drawing.Point(132, 336);
        this.HardCheck.Name = "HardCheck";
        this.HardCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.HardCheck.Size = new System.Drawing.Size(100, 12);
        this.HardCheck.TabIndex = 71;
        this.HardCheck.Text = "PREMIUM";
        this.HardCheck.CheckedChanged += new System.EventHandler(this.HardCheck_CheckedChanged);
        this.HardCheck.Click += new System.EventHandler(this.HardCheck_Click);
        // 
        // ThresholdCheck
        // 
        this.ThresholdCheck.AutoCheck = false;
        this.ThresholdCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.ThresholdCheck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ThresholdCheck.ForeColor = System.Drawing.Color.Goldenrod;
        this.ThresholdCheck.Location = new System.Drawing.Point(128, 316);
        this.ThresholdCheck.Name = "ThresholdCheck";
        this.ThresholdCheck.Size = new System.Drawing.Size(104, 12);
        this.ThresholdCheck.TabIndex = 74;
        this.ThresholdCheck.TabStop = false;
        this.ThresholdCheck.Text = "THRESHOLD";
        this.ThresholdCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.ThresholdCheck.CheckedChanged += new System.EventHandler(this.ThresholdCheck_CheckedChanged);
        // 
        // EasyCheck
        // 
        this.EasyCheck.AutoCheck = false;
        this.EasyCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.EasyCheck.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.EasyCheck.ForeColor = System.Drawing.Color.Goldenrod;
        this.EasyCheck.Location = new System.Drawing.Point(30, 316);
        this.EasyCheck.Name = "EasyCheck";
        this.EasyCheck.Size = new System.Drawing.Size(60, 12);
        this.EasyCheck.TabIndex = 73;
        this.EasyCheck.TabStop = false;
        this.EasyCheck.Text = "EASY";
        this.EasyCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.EasyCheck.CheckedChanged += new System.EventHandler(this.EasyCheck_CheckedChanged);
        // 
        // CusipGroupBox
        // 
        this.CusipGroupBox.BackColor = System.Drawing.Color.White;
        this.CusipGroupBox.Controls.Add(this.SecurityText);
        this.CusipGroupBox.Controls.Add(this.c1Label1);
        this.CusipGroupBox.Controls.Add(this.AccountHolds);
        this.CusipGroupBox.Controls.Add(this.DescriptionText);
        this.CusipGroupBox.Controls.Add(this.LocationCombo);
        this.CusipGroupBox.Controls.Add(this.QuickText);
        this.CusipGroupBox.Controls.Add(this.QuickLabel);
        this.CusipGroupBox.Controls.Add(this.IsinLabel);
        this.CusipGroupBox.Controls.Add(this.IsinText);
        this.CusipGroupBox.Controls.Add(this.SymbolText);
        this.CusipGroupBox.Controls.Add(this.CusipLabel);
        this.CusipGroupBox.Controls.Add(this.CusipText);
        this.CusipGroupBox.Controls.Add(this.SymbolLabel);
        this.CusipGroupBox.Controls.Add(this.PriceLabel);
        this.CusipGroupBox.Controls.Add(this.PriceText);
        this.CusipGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.CusipGroupBox.Font = new System.Drawing.Font("Verdana", 10F);
        this.CusipGroupBox.Location = new System.Drawing.Point(4, 76);
        this.CusipGroupBox.Name = "CusipGroupBox";
        this.CusipGroupBox.Size = new System.Drawing.Size(268, 152);
        this.CusipGroupBox.TabIndex = 69;
        this.CusipGroupBox.TabStop = false;
        // 
        // SecurityText
        // 
        this.SecurityText.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.SecurityText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.SecurityText.ForeColor = System.Drawing.Color.Black;
        this.SecurityText.Location = new System.Drawing.Point(8, 104);
        this.SecurityText.Name = "SecurityText";
        this.SecurityText.Size = new System.Drawing.Size(256, 16);
        this.SecurityText.TabIndex = 84;
        this.SecurityText.Tag = null;
        this.SecurityText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.SecurityText.TextDetached = true;
        // 
        // c1Label1
        // 
        this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.c1Label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.c1Label1.Location = new System.Drawing.Point(8, 128);
        this.c1Label1.Name = "c1Label1";
        this.c1Label1.Size = new System.Drawing.Size(60, 12);
        this.c1Label1.TabIndex = 83;
        this.c1Label1.Tag = null;
        this.c1Label1.Text = "Custody:";
        this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.c1Label1.TextDetached = true;
        // 
        // AccountHolds
        // 
        this.AccountHolds.AddItemSeparator = ';';
        this.AccountHolds.AllowColMove = false;
        this.AccountHolds.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.AccountHolds.Caption = "";
        this.AccountHolds.CaptionHeight = 17;
        this.AccountHolds.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
        this.AccountHolds.ColumnCaptionHeight = 17;
        this.AccountHolds.ColumnFooterHeight = 17;
        this.AccountHolds.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
        this.AccountHolds.ContentHeight = 17;
        this.AccountHolds.DeadAreaBackColor = System.Drawing.Color.Empty;
        this.AccountHolds.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
        this.AccountHolds.DropDownWidth = 400;
        this.AccountHolds.EditorBackColor = System.Drawing.Color.White;
        this.AccountHolds.EditorFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.AccountHolds.EditorForeColor = System.Drawing.Color.Khaki;
        this.AccountHolds.EditorHeight = 17;
        this.AccountHolds.EmptyRows = true;
        this.AccountHolds.ExtendRightColumn = true;
        this.AccountHolds.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.AccountHolds.Images.Add(((System.Drawing.Image)(resources.GetObject("AccountHolds.Images"))));
        this.AccountHolds.ItemHeight = 15;
        this.AccountHolds.LimitToList = true;
        this.AccountHolds.Location = new System.Drawing.Point(232, 128);
        this.AccountHolds.MatchEntryTimeout = ((long)(2000));
        this.AccountHolds.MaxDropDownItems = ((short)(25));
        this.AccountHolds.MaxLength = 32767;
        this.AccountHolds.MouseCursor = System.Windows.Forms.Cursors.Default;
        this.AccountHolds.Name = "AccountHolds";
        this.AccountHolds.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.AccountHolds.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
        this.AccountHolds.RowSubDividerColor = System.Drawing.Color.DarkGray;
        this.AccountHolds.Size = new System.Drawing.Size(16, 17);
        this.AccountHolds.TabIndex = 82;
        this.AccountHolds.ValueMember = "Text";
        this.AccountHolds.Open += new C1.Win.C1List.OpenEventHandler(this.AccountHolds_Open);
        this.AccountHolds.PropBag = resources.GetString("AccountHolds.PropBag");
        // 
        // DescriptionText
        // 
        this.DescriptionText.BackColor = System.Drawing.Color.Silver;
        this.DescriptionText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.DescriptionText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.DescriptionText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.DescriptionText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
        this.DescriptionText.Location = new System.Drawing.Point(0, 0);
        this.DescriptionText.Name = "DescriptionText";
        this.DescriptionText.Size = new System.Drawing.Size(268, 19);
        this.DescriptionText.TabIndex = 71;
        this.DescriptionText.TabStop = false;
        this.DescriptionText.Tag = null;
        this.DescriptionText.TextDetached = true;
        this.DescriptionText.TrimStart = true;
        this.DescriptionText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.DescriptionText.WordWrap = false;
        // 
        // LocationCombo
        // 
        this.LocationCombo.AddItemSeparator = ';';
        this.LocationCombo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.LocationCombo.Caption = "";
        this.LocationCombo.CaptionHeight = 17;
        this.LocationCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
        this.LocationCombo.ColumnCaptionHeight = 17;
        this.LocationCombo.ColumnFooterHeight = 17;
        this.LocationCombo.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
        this.LocationCombo.ContentHeight = 16;
        this.LocationCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
        this.LocationCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
        this.LocationCombo.DropDownWidth = 385;
        this.LocationCombo.EditorBackColor = System.Drawing.Color.White;
        this.LocationCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LocationCombo.EditorForeColor = System.Drawing.Color.Black;
        this.LocationCombo.EditorHeight = 16;
        this.LocationCombo.ExtendRightColumn = true;
        this.LocationCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LocationCombo.GapHeight = 1;
        this.LocationCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("LocationCombo.Images"))));
        this.LocationCombo.ItemHeight = 15;
        this.LocationCombo.LimitToList = true;
        this.LocationCombo.Location = new System.Drawing.Point(72, 128);
        this.LocationCombo.MatchEntryTimeout = ((long)(2000));
        this.LocationCombo.MaxDropDownItems = ((short)(25));
        this.LocationCombo.MaxLength = 32767;
        this.LocationCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
        this.LocationCombo.Name = "LocationCombo";
        this.LocationCombo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.LocationCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
        this.LocationCombo.RowSubDividerColor = System.Drawing.Color.Gainsboro;
        this.LocationCombo.Size = new System.Drawing.Size(144, 20);
        this.LocationCombo.TabIndex = 55;
        this.LocationCombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.LocationCombo.ValueMember = "Text";
        this.LocationCombo.SelectedValueChanged += new System.EventHandler(this.LocationCombo_SelectedValueChanged);
        this.LocationCombo.PropBag = resources.GetString("LocationCombo.PropBag");
        // 
        // QuickText
        // 
        this.QuickText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.QuickText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.QuickText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.QuickText.Location = new System.Drawing.Point(192, 21);
        this.QuickText.MaxLength = 9;
        this.QuickText.Name = "QuickText";
        this.QuickText.Size = new System.Drawing.Size(42, 19);
        this.QuickText.TabIndex = 28;
        this.QuickText.TabStop = false;
        this.QuickText.Tag = null;
        this.QuickText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.QuickText.TextDetached = true;
        this.QuickText.TrimStart = true;
        this.QuickText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.QuickText.WordWrap = false;
        // 
        // QuickLabel
        // 
        this.QuickLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.QuickLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.QuickLabel.Location = new System.Drawing.Point(144, 24);
        this.QuickLabel.Name = "QuickLabel";
        this.QuickLabel.Size = new System.Drawing.Size(42, 12);
        this.QuickLabel.TabIndex = 29;
        this.QuickLabel.Tag = null;
        this.QuickLabel.Text = "Quick:";
        this.QuickLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.QuickLabel.TextDetached = true;
        // 
        // IsinLabel
        // 
        this.IsinLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.IsinLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.IsinLabel.Location = new System.Drawing.Point(150, 48);
        this.IsinLabel.Name = "IsinLabel";
        this.IsinLabel.Size = new System.Drawing.Size(36, 12);
        this.IsinLabel.TabIndex = 27;
        this.IsinLabel.Tag = null;
        this.IsinLabel.Text = "ISIN:";
        this.IsinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.IsinLabel.TextDetached = true;
        // 
        // IsinText
        // 
        this.IsinText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.IsinText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.IsinText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.IsinText.Location = new System.Drawing.Point(192, 45);
        this.IsinText.MaxLength = 12;
        this.IsinText.Name = "IsinText";
        this.IsinText.Size = new System.Drawing.Size(72, 19);
        this.IsinText.TabIndex = 26;
        this.IsinText.TabStop = false;
        this.IsinText.Tag = null;
        this.IsinText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.IsinText.TextDetached = true;
        this.IsinText.TrimStart = true;
        this.IsinText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.IsinText.WordWrap = false;
        // 
        // SymbolText
        // 
        this.SymbolText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.SymbolText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.SymbolText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.SymbolText.Location = new System.Drawing.Point(64, 45);
        this.SymbolText.MaxLength = 12;
        this.SymbolText.Name = "SymbolText";
        this.SymbolText.Size = new System.Drawing.Size(78, 19);
        this.SymbolText.TabIndex = 24;
        this.SymbolText.TabStop = false;
        this.SymbolText.Tag = null;
        this.SymbolText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.SymbolText.TextDetached = true;
        this.SymbolText.TrimStart = true;
        this.SymbolText.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle;
        this.SymbolText.WordWrap = false;
        // 
        // SymbolLabel
        // 
        this.SymbolLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.SymbolLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.SymbolLabel.Location = new System.Drawing.Point(4, 48);
        this.SymbolLabel.Name = "SymbolLabel";
        this.SymbolLabel.Size = new System.Drawing.Size(54, 12);
        this.SymbolLabel.TabIndex = 25;
        this.SymbolLabel.Tag = null;
        this.SymbolLabel.Text = "Symbol:";
        this.SymbolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.SymbolLabel.TextDetached = true;
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.ForeColor = System.Drawing.Color.Goldenrod;
        this.label4.Location = new System.Drawing.Point(180, 290);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(36, 14);
        this.label4.TabIndex = 81;
        this.label4.Text = "L";
        this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // SecMasterControl
        // 
        this.Controls.Add(this.CusipPanel);
        this.Controls.Add(this.MainPanel);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Name = "SecMasterControl";
        this.Padding = new System.Windows.Forms.Padding(1, 1, 1, 0);
        this.Size = new System.Drawing.Size(280, 1108);
        this.MainPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.TradeSellsSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.TradeSellsTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LockUpSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LockUpTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.NetPositionSettledLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.NetPositionSettledDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.SegReqSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.StockBorrowSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.StockLoanSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label32)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label31)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ReceiveSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ReceiveTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeliverSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeliverTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PledgedTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PledgedSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ShortTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ShortSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LongTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LongSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmShortSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerShortSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailInTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailInTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailInTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailInTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailInSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailInSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailInSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailInSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailInDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailInDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailInDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailInDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailOutSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailOutSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailOutSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailOutSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailOutTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailOutTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailOutTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailOutTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DvpFailOutDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BrokerFailOutDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ClearingFailOutDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.OtherFailOutDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerPledgeSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmPledgeSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerPledgeTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmPledgeTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerShortTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmShortTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerLongSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CustomerLongTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmLongSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FirmLongTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ExDeficitSettled)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ExDeficitTraded)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeficitDayCount)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DividendLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DividendText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PriceLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.PriceText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.RecordDateLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.RecordDateText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CusipLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CusipText)).EndInit();
        this.CusipPanel.ResumeLayout(false);
        this.CusipPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FindLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BookLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.RateIndicatorText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DeskQuipCombo)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.RateLoanTextBox)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.RateBorrowTextBox)).EndInit();
        this.CusipGroupBox.ResumeLayout(false);
        this.CusipGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.SecurityText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.AccountHolds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.DescriptionText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LocationCombo)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.QuickText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.QuickLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.IsinLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.IsinText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.SymbolText)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.SymbolLabel)).EndInit();
        this.ResumeLayout(false);

		}
    #endregion

    public void DeskQuipInit()
    {
      try
      {
        if (mainForm.DeskQuipDataSet.Tables["DeskQuips"].Rows.Count > 0)
        {
          lastDeskQuipDateTime = mainForm.DeskQuipDataSet.Tables["DeskQuips"].Rows[0]["ActTime"].ToString();
        }

        DeskQuipDataView = new DataView(mainForm.DeskQuipDataSet.Tables["DeskQuips"]);
        DeskQuipDataView.RowFilter = "SecId = ''";
        DeskQuipDataView.Sort = "ActTime Desc";

				DeskQuipCombo.HoldFields();
				DeskQuipCombo.DataSource = DeskQuipDataView;
      }      
      catch (Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [SecMasterControl.DeskQuipInit]", Log.Error, 1); 
      }
    }
        
    public void BookGroupsInit()
    {
      try
      {
        BookGroupCombo.HoldFields();
        BookGroupCombo.DataSource =  mainForm.ServiceAgent.BookGroupGet().Tables["BookGroups"];
        BookGroupCombo.DataMember = "BookGroup";

        if (BookGroupCombo.ListCount > 0)
        {
          BookGroupCombo.SelectedIndex = 0;
        }  
      }
      catch (Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [SecMasterControl.BookGroupsInit]", Log.Error, 1); 
      }
    }
     
    private void BoxPositionInit(DataTable boxPosition)
    {
			try
			{											
				boxPositionDataView = new DataView(boxPosition);
				boxPositionDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";

                LockUpSettled.DataSource = boxPositionDataView;
                LockUpTraded.DataSource = boxPositionDataView;

                CustomerLongSettled.DataSource = boxPositionDataView;
				CustomerLongTraded.DataSource = boxPositionDataView;
				CustomerShortSettled.DataSource = boxPositionDataView;;
				CustomerShortTraded.DataSource = boxPositionDataView;
				CustomerPledgeSettled.DataSource = boxPositionDataView;
				CustomerPledgeTraded.DataSource = boxPositionDataView;
    
				FirmLongSettled.DataSource = boxPositionDataView;
				FirmLongTraded.DataSource = boxPositionDataView;
				FirmShortSettled.DataSource = boxPositionDataView;
				FirmShortTraded.DataSource = boxPositionDataView;
				FirmPledgeSettled.DataSource = boxPositionDataView;
				FirmPledgeTraded.DataSource = boxPositionDataView;
    
				DvpFailInSettled.DataSource = boxPositionDataView;
				DvpFailInTraded.DataSource = boxPositionDataView;
				DvpFailOutSettled.DataSource = boxPositionDataView;
				DvpFailOutTraded.DataSource = boxPositionDataView;
    
				BrokerFailInSettled.DataSource = boxPositionDataView;
				BrokerFailInTraded.DataSource = boxPositionDataView;
				BrokerFailOutSettled.DataSource = boxPositionDataView;
				BrokerFailOutTraded.DataSource = boxPositionDataView;
    
				ClearingFailInSettled.DataSource = boxPositionDataView;
				ClearingFailInTraded.DataSource = boxPositionDataView;
				ClearingFailOutSettled.DataSource = boxPositionDataView;
				ClearingFailOutTraded.DataSource = boxPositionDataView;
    
				OtherFailInSettled.DataSource = boxPositionDataView;
				OtherFailInTraded.DataSource = boxPositionDataView;
				OtherFailOutSettled.DataSource = boxPositionDataView;
				OtherFailOutTraded.DataSource = boxPositionDataView;
    
				ExDeficitSettled.DataSource = boxPositionDataView;
				ExDeficitTraded.DataSource = boxPositionDataView;

				DvpFailInDayCount.DataSource = boxPositionDataView;
				DvpFailOutDayCount.DataSource = boxPositionDataView;
				BrokerFailInDayCount.DataSource = boxPositionDataView;
				BrokerFailOutDayCount.DataSource = boxPositionDataView;
				ClearingFailInDayCount.DataSource = boxPositionDataView;
				ClearingFailOutDayCount.DataSource = boxPositionDataView;
				OtherFailInDayCount.DataSource = boxPositionDataView;
				OtherFailOutDayCount.DataSource = boxPositionDataView;
				DeficitDayCount.DataSource = boxPositionDataView;
				
				StockBorrowSettled.DataSource = boxPositionDataView;			
				StockLoanSettled.DataSource = boxPositionDataView;			
				SegReqSettled.DataSource = boxPositionDataView;

				NetPositionSettledDayCount.DataSource = boxPositionDataView;
				NetPositionSettledLabel.DataSource = boxPositionDataView;
				
				DeliverSettled.DataSource = boxPositionDataView;
				DeliverTraded.DataSource = boxPositionDataView;
				
				ReceiveSettled.DataSource = boxPositionDataView;
				ReceiveTraded.DataSource = boxPositionDataView;
				
				ShortSettled.DataSource =  boxPositionDataView;
				ShortTraded.DataSource = boxPositionDataView;
				
				LongSettled.DataSource = boxPositionDataView;
				LongTraded.DataSource = boxPositionDataView;
				
				PledgedSettled.DataSource = boxPositionDataView;
				PledgedTraded.DataSource = boxPositionDataView;

                TradeSellsSettled.DataSource = boxPositionDataView;
                TradeSellsTraded.DataSource = boxPositionDataView;

                TradeSellsSettled.DataField = "TradeSells";
                TradeSellsTraded.DataField = "TradeSells";

                LockUpSettled.DataField = "Lockup";
                LockUpTraded.DataField = "Lockup";

				DeliverSettled.DataField = "DeliverSettled";
				DeliverTraded.DataField = "DeliverTraded";
				
				ReceiveSettled.DataField = "ReceiveSettled";
				ReceiveTraded.DataField = "ReceiveTraded";
				
				ShortSettled.DataField =  "ShortSettled";
				ShortTraded.DataField = "ShortTraded";
				
				LongSettled.DataField = "LongSettled";
				LongTraded.DataField = "LongTraded";
				
				PledgedSettled.DataField = "PledgedSettled";
				PledgedTraded.DataField = "PledgedTraded";				
				
				CustomerLongSettled.DataField = "CustomerLongSettled";
				CustomerLongTraded.DataField = "CustomerLongTraded";
				CustomerShortSettled.DataField = "CustomerShortSettled";
				CustomerShortTraded.DataField = "CustomerShortTraded";
				CustomerPledgeSettled.DataField = "CustomerPledgeSettled";
				CustomerPledgeTraded.DataField = "CustomerPledgeTraded";
    
				FirmLongSettled.DataField = "FirmLongSettled";
				FirmLongTraded.DataField = "FirmLongTraded";
				FirmShortSettled.DataField = "FirmShortSettled";
				FirmShortTraded.DataField = "FirmShortTraded";
				FirmPledgeSettled.DataField = "FirmPledgeSettled";
				FirmPledgeTraded.DataField = "FirmPledgeTraded";
    
				DvpFailInSettled.DataField = "DvpFailInSettled";
				DvpFailInTraded.DataField = "DvpFailInTraded";
				DvpFailOutSettled.DataField = "DvpFailOutSettled";
				DvpFailOutTraded.DataField = "DvpFailOutTraded";
    
				BrokerFailInSettled.DataField = "BrokerFailInSettled";
				BrokerFailInTraded.DataField = "BrokerFailInTraded";
				BrokerFailOutSettled.DataField = "BrokerFailOutSettled";
				BrokerFailOutTraded.DataField = "BrokerFailOutTraded";
    
				ClearingFailInSettled.DataField = "ClearingFailInSettled";
				ClearingFailInTraded.DataField = "ClearingFailInTraded";
				ClearingFailOutSettled.DataField = "ClearingFailOutSettled";
				ClearingFailOutTraded.DataField = "ClearingFailOutTraded";
    
				OtherFailInSettled.DataField = "OtherFailInSettled";
				OtherFailInTraded.DataField = "OtherFailInTraded";
				OtherFailOutSettled.DataField = "OtherFailOutSettled";
				OtherFailOutTraded.DataField = "OtherFailOutTraded";
    
				ExDeficitSettled.DataField = "ExDeficitSettled";
				ExDeficitTraded.DataField = "ExDeficitTraded";				
		
				DvpFailInDayCount.DataField = "DvpFailInDayCount";
				DvpFailOutDayCount.DataField = "DvpFailOutDayCount";

				BrokerFailInDayCount.DataField = "BrokerFailInDayCount";
				BrokerFailOutDayCount.DataField = "BrokerFailOutDayCount";
        
				ClearingFailInDayCount.DataField = "ClearingFailInDayCount";
				ClearingFailOutDayCount.DataField = "ClearingFailOutDayCount";
        
				OtherFailInDayCount.DataField = "OtherFailInDayCount";
				OtherFailOutDayCount.DataField = "OtherFailOutDayCount";
				SegReqSettled.DataField = "SegReqSettled";
				DeficitDayCount.DataField = "DeficitDayCount";

				StockBorrowSettled.DataField = "StockBorrowSettled";		
				StockLoanSettled.DataField = "StockLoanSettled";		

				NetPositionSettledDayCount.DataField = "NetPositionSettledDayCount";
				NetPositionSettledLabel.DataField = "NetPositionSettled";

				RateBorrowTextBox.DataSource  = boxPositionDataView;
				RateLoanTextBox.DataSource = boxPositionDataView;

				RateBorrowTextBox.DataField  = "AverageBorrowRate";
				RateLoanTextBox.DataField = "AverageLoanRate";				
			}   
			catch (Exception e)
			{
				mainForm.Alert(e.Message);
			}
    }

    private void HardCheck_Click(object sender, System.EventArgs e)
    {
      if (!secId.Equals(""))
      {
        try
        {
          mainForm.ServiceAgent.BorrowHardSet(secId, mainForm.UserId, !HardCheck.Checked);
        }
        catch(Exception ee)
        {
          mainForm.Alert(ee.Message);
          Log.Write(ee.Message + "[SecMasterControl.HardCheck_Click]", Log.Error,1); 
        }    
      }
    }

    private void NoLendCheck_Click(object sender, System.EventArgs e)
    {
      if (!secId.Equals(""))
      {
				if (!NoLendCheck.Checked)
				{
					if (!mainForm.AdminAgent.MayEdit(mainForm.UserId, "SecMasterNoLendCheck"))
					{
						  NoLendCheck.Checked = true;
							return;
					}					
				}
												
				try
        {
          mainForm.ServiceAgent.BorrowNoSet(secId, mainForm.UserId, !NoLendCheck.Checked);
        }
        catch(Exception ee)
        {
          mainForm.Alert(ee.Message);
          Log.Write(ee.Message + "[SecMasterControl.NoLendCheck_Click]", Log.Error,1); 
        }    
      }
    }

    private void EasyCheck_CheckedChanged(object sender, System.EventArgs e)
    {
     /* if (EasyCheck.Checked)
      {
        EasyCheck.ForeColor = Color.Navy;
      }
      else
      {
        EasyCheck.ForeColor = Color.Gray;        
      }*/
    }

    private void HardCheck_CheckedChanged(object sender, System.EventArgs e)
    {
     /* if (HardCheck.Checked)
      {
        HardCheck.ForeColor = Color.Maroon;
      }
      else
      {
        HardCheck.ForeColor = Color.Gray;        
      }*/    
    }

    private void NoLendCheck_CheckedChanged(object sender, System.EventArgs e)
    {		
     /* if (NoLendCheck.Checked)
      {
        NoLendCheck.ForeColor = Color.Black;
      }
      else
      {
        NoLendCheck.ForeColor = Color.Gray;        
      }*/        
    }

    private void ThresholdCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      /*if (ThresholdCheck.Checked)
      {
        ThresholdCheck.ForeColor = Color.DarkCyan;
        ThresholdDayCountLabel.ForeColor = Color.DarkCyan;
      }
      else
      {
        ThresholdCheck.ForeColor = Color.Gray;        
        ThresholdDayCountLabel.ForeColor = Color.Gray;
      } */   
    }

    private void FindTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13))
      {
        mainForm.SecId = FindTextBox.Text.Trim();
        e.Handled = true;
      }
    }

    private void FindTextBox_MouseEnter(object sender, System.EventArgs e)
    {
      FindTextBox.SelectAll();
    }

    private void FindTextBox_Enter(object sender, System.EventArgs e)
    {
      FindTextBox.SelectAll();
    }

    private void LocationCombo_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
    {
      if (e.Value.Length == 0)
      {
        return;
      }
  
      try
      {
        switch(LocationCombo.Columns[e.ColIndex].DataField)
        {
          case ("QuantitySettled"):
          case ("QuantityTraded"):
            e.Value = long.Parse(e.Value).ToString("#,##0");
            break;
        }
      }
      catch {}    
    }

    private void BookGroupCombo_TextChanged(object sender, System.EventArgs e)
    {
      if ((boxPositionDataView != null) && (boxLocationDataView != null))
      {
        boxPositionDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
        boxLocationDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";    
      }
    }

    private void FailLabel_Click(object sender, System.EventArgs e)
    {
      PositionBoxFailHistoryForm positionBoxFailHistoryForm = new PositionBoxFailHistoryForm(this.mainForm, BookGroupCombo.Text);
      positionBoxFailHistoryForm.ShowDialog();
    }

  	private void PledgedLabel_Click(object sender, System.EventArgs e)
	  {
			if (!this.secId.Equals("") && (!this.CustomerPledgeSettled.Text.Equals("0") || !this.FirmPledgeSettled.Text.Equals("0")))
			{
				try
				{
					PositionBankLoanReleaseInputForm positionBankLoanReleaseInputForm = new PositionBankLoanReleaseInputForm(
						mainForm, 
						BookGroupCombo.Text,
						secId);

					positionBankLoanReleaseInputForm.MdiParent = mainForm;
					positionBankLoanReleaseInputForm.Show();
				}
				catch (Exception error)
				{
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
			}
    }

		private void LoanContractTypeLabel_Click(object sender, System.EventArgs e)		
		{
			try
			{
				PositionContractRateHistoryForm positionContractRateHistoryForm = new PositionContractRateHistoryForm(mainForm, BookGroupCombo.Text, "L", secId);
				positionContractRateHistoryForm.MdiParent = mainForm;
				positionContractRateHistoryForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void BorrowContractTypeLabel_Click(object sender, System.EventArgs e)
		{		
			try
			{
				PositionContractRateHistoryForm positionContractRateHistoryForm = new PositionContractRateHistoryForm(mainForm, BookGroupCombo.Text, "B", secId);
				positionContractRateHistoryForm.MdiParent = mainForm;
				positionContractRateHistoryForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void AccountHolds_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			switch (AccountHolds.Columns[e.ColIndex].DataField)
			{
				case "Quantity":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;
			}
		}

		private void AccountHolds_Open(object sender, System.EventArgs e)
		{
			if(!secId.Equals(""))
			{
				try
				{
					accountHoldsTable = mainForm.PositionAgent.AccountPositionGet(secId, false).Tables["AccountPosition"];
					AccountHolds.HoldFields();
					AccountHolds.DataSource = accountHoldsTable;
				}
				catch (Exception error)
				{
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
			}
		}

		private void LocationCombo_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if (LocationCombo.SelectedIndex != -1)
			{
				LocationCombo.Text = LocationCombo.Columns["Custodian"].Text + " | " + long.Parse(LocationCombo.Columns["Today"].Text).ToString("#,##0");										
			}
		}

		private void c1Label2_Click(object sender, System.EventArgs e)
		{
			this.Dock = DockStyle.Right;
		}

		private void c1Label18_Click(object sender, System.EventArgs e)
		{
			this.Dock = DockStyle.Left;
		}

		private void DeskQuipCombo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				mainForm.ServiceAgent.DeskQuipSet(secId, DeskQuipCombo.Text.Trim(), mainForm.UserId);
				e.Handled = true;

				FindTextBox.Focus();
			}
		}

		private void DeskQuipCombo_Enter(object sender, System.EventArgs e)
		{
			DeskQuipCombo.Text = "";
			DeskQuipCombo.CharacterCasing = CharacterCasing.Upper;
		}

		private void DeskQuipCombo_Leave(object sender, System.EventArgs e)
		{
			DeskQuipCombo.CharacterCasing = CharacterCasing.Normal;
		}

		private void DeskQuipCombo_Close(object sender, System.EventArgs e)
		{
			if (DeskQuipDataView.Count > 0)
			{
				DeskQuipCombo.SelectedIndex = 0;
			}
		}

		private void DeskQuipCombo_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0) // Nothing to format.
			{
				return;
			}
  
			switch(DeskQuipCombo.Columns[e.ColIndex].DataField)
			{
				case ("ActTime"):
					try
					{
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeFileFormat);
					}
					catch(FormatException ee)
					{
						Log.Write(ee.Message + " [MainForm.DeskQuipCombo_FormatText]", Log.Error, 1);
					}
          
					break;
			}                     
		}

		private void DeskQuipCombo_UnboundColumnFetch(object sender, C1.Win.C1List.UnboundColumnFetchEventArgs e)
		{
			switch(DeskQuipCombo.Columns[e.Col].Caption)
			{
				case ("Text"):
					e.Value = Tools.FormatDate(DeskQuipCombo.Columns["ActTime"].Text, Standard.DateTimeFileFormat) + " [" +
						DeskQuipCombo.Columns["ActUserShortName"].Text + "]  " + DeskQuipCombo.Columns["DeskQuip"].Text;
      
					break;
			}                          
		}

		private void RateIndicatorText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		
			if (e.KeyChar.Equals((char) 13))
			{
				try
				{
					mainForm.ShortSaleAgent.InventoryRateSet(secId,  RateIndicatorText.Text, mainForm.UserId);
					mainForm.Alert("Inventory rate for SecId [" + secId + "] set to " +  float.Parse(RateIndicatorText.Text).ToString("#0.###") + ".", PilotState.Normal);
					
					e.Handled = true;
				}
				catch (Exception error)
				{					
					mainForm.Alert(error.Message, PilotState.RunFault);
				}				
			}		
		}

		private void RateLoanTextBox_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
		{
			try
			{
				if (!RateLoanTextBox.Value.ToString().Equals(""))
				{
					e.Text = decimal.Parse(e.Value.ToString()).ToString("0.00");
				}
			}
			catch {}
		}

		private void RateBorrowTextBox_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
		{
			try
			{
				if (!RateBorrowTextBox.Value.ToString().Equals(""))
				{
					e.Text = decimal.Parse(e.Value.ToString()).ToString("0.00");
				}
			}
			catch {}
		}
  }
}
