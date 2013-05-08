// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;


namespace Anetics.Medalist
{ 
  public class PositionContractBlotterForm : System.Windows.Forms.Form
  {
    private const string TEXT = "Position - Contract Blotter";
    private const string DEAL_ID_PREFIX = "C";
    
    private bool isReady = false;    
    private string secId = "";

    private MainForm mainForm;
    
    private DataSet dataSet;    
    private DataView bookDataView, contractDataView;

    private string contractRowFilter;
    
    private C1.Win.C1Input.C1Label BookGroupLabel;
    private C1.Win.C1List.C1Combo BookGroupCombo;
    
    private C1.Win.C1Input.C1Label BookGroupNameLabel;
    
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid ContractBlotterGrid;
    private C1.Win.C1TrueDBGrid.C1TrueDBDropdown BooksDropDown;

    private System.Windows.Forms.ContextMenu MainContextMenu;
    
    private System.Windows.Forms.MenuItem SendToMenuItem;
    private System.Windows.Forms.MenuItem SendToProcessAgentMenuItem;
    private System.Windows.Forms.MenuItem SendToEmailMenuItem;
    private System.Windows.Forms.MenuItem SendToTrashBinMenuItem;
    
    private System.Windows.Forms.MenuItem ShowMenuItem;
    private System.Windows.Forms.MenuItem ShowOtherBookMenuItem;
    private System.Windows.Forms.MenuItem ShowDivRateMenuItem;
    private System.Windows.Forms.MenuItem ShowIncomeTrackedMenuItem;
    
    private System.Windows.Forms.MenuItem Sep1MenuItem;
    
    private System.Windows.Forms.MenuItem DockMenuItem;    
    private System.Windows.Forms.MenuItem DockTopMenuItem;
    private System.Windows.Forms.MenuItem DockBottomMenuItem;    
    private System.Windows.Forms.MenuItem Sep3MenuItem;
    private System.Windows.Forms.MenuItem DockNoneMenuItem;
    
    private System.Windows.Forms.MenuItem Sep2MenuItem;
        
    private System.Windows.Forms.MenuItem ExitMenuItem;
    
    private C1.Win.C1Input.C1Label StatusLabel;
    
    private DealEventWrapper dealEventWrapper = null;
    private DealEventHandler dealEventHandler = null;
    
    private ArrayList dealEventArgsArray;

    private System.Windows.Forms.RadioButton LoansRadio;
    private System.Windows.Forms.RadioButton BorrowsRadio;
    
    private System.Windows.Forms.CheckBox ShowContractsCheckBox;
    
    private System.ComponentModel.Container components = null;

    string  _dealId;
    string  _bookGroup;
    string  _dealType;
    string  _book = "";
    string  _secId;
    string  _quantity;
    string  _amount;
    string  _collateralCode;
    string  _valueDate = "";
    string  _settleDate = "";
    string  _termDate = "";
    string  _rate;
    string  _rateCode;
    string  _poolCode = "";
    string  _divRate;
    string  _divCallable = "False";
    string  _incomeTracked;
    string  _margin;
    string  _marginCode = "%";
    string  _currencyIso = "USD";
    string  _securityDepot = "  ";
    string  _cashDepot = "  ";
    string  _comment;
    string  _dealStatus;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
    
    bool mayView = false;
    
    private delegate void SendDealsDelegate(ArrayList dealRows);

    public PositionContractBlotterForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      InitializeComponent();    
      
      dealEventArgsArray = new ArrayList();      
      
      try
      {        
        ContractBlotterGrid.Splits[0,0].DisplayColumns["DealId"].Visible = false;
        ContractBlotterGrid.Splits[0,0].DisplayColumns["BookGroup"].Visible = false;
        ContractBlotterGrid.Splits[0,0].DisplayColumns["DealType"].Visible = false;
        ContractBlotterGrid.Splits[0,0].DisplayColumns["IncomeTracked"].Visible = false;
        ContractBlotterGrid.Splits[0,0].DisplayColumns["DivRate"].Visible = false;
        ContractBlotterGrid.Splits[0,0].DisplayColumns["OtherBook"].Visible = false;        
        ContractBlotterGrid.Splits[0,0].DisplayColumns["IsActive"].Visible = false;        
        ContractBlotterGrid.Splits[0,0].DisplayColumns["MarginCode"].Visible = false;        

        dealEventWrapper = new DealEventWrapper(); 
        dealEventWrapper.DealEvent += new DealEventHandler(DealOnEvent);       
      
        dealEventHandler = new DealEventHandler(DealDoEvent);
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }

      base.Dispose(disposing);
    }

    private bool IsReady
    {
      get
      {
        return isReady;
      }

      set
      {
        try
        {
          if (value && (dealEventArgsArray.Count > 0))
          {          
            isReady = false;

            dealEventHandler.BeginInvoke((DealEventArgs)dealEventArgsArray[0], null, null);            
            dealEventArgsArray.RemoveAt(0);
          }
          else
          {
            isReady = value;
          }
        }
        catch (Exception e)
        {
          Log.Write(e.Message + " [PositionContractsBlotterForm.IsReady(set)]", Log.Error, 1); 
        }
      }
    }

    private void DealOnEvent(DealEventArgs dealEventArgs)
    {
      int i;
      
      if (dealEventArgs.DealId.StartsWith(DEAL_ID_PREFIX))
      {
        i = dealEventArgsArray.Add(dealEventArgs);
        Log.Write("Deal event queued at " + i + " for deal ID: " + dealEventArgs.DealId + " [PositionDealBlotterForm.DealOnEvent]" , 3);
      
        if (this.IsReady) // Force reset to trigger handling of event.
        {
          this.IsReady = true;
        }
      }
      else
      {
        Log.Write("Deal event being discarded for deal ID: " + dealEventArgs.DealId + " [PositionContractBlotterForm.DealOnEvent]" , 3);
      }
    }

    private void DealDoEvent(DealEventArgs dealEventArgs)
    {
      try
      { 
        Log.Write("Deal event being handled for deal ID: " + dealEventArgs.DealId + " [PositionContractBlotterForm.DealDoEvent]" , 3);

        dataSet.Tables["Deals"].BeginLoadData();

        dealEventArgs.UtcOffset = mainForm.UtcOffset;
        dataSet.Tables["Deals"].LoadDataRow(dealEventArgs.Values, true);        
        
        dataSet.Tables["Deals"].EndLoadData();               

        FormStatusSet();

        this.IsReady = true;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PositionContractBlotterForm.DealDoEvent]", Log.Error, 1);          
      }
    }		

    public void AmountSet()
    {                
      decimal price;
      long    quantity;
      decimal margin;
      string  markRoundHouse;
      decimal markRoundInstitution;
      decimal priceMinimum;
      decimal amountMinimum;
      decimal amount;
   
      try
      {        				
				if (mainForm.Price.Equals(""))
        {
          ContractBlotterGrid.Columns["Amount"].Text = "";
          return;
        }
      
        if (ContractBlotterGrid.Columns["Quantity"].Text.Trim().Equals(""))
        {
          ContractBlotterGrid.Columns["Amount"].Text = "";
          return;
        }
      
				price = decimal.Parse(mainForm.Price);				
			
        quantity = long.Parse(ContractBlotterGrid.Columns["Quantity"].Value.ToString());

        if (ContractBlotterGrid.Columns["Margin"].Text.Trim().Equals(""))
        {
          margin = 1.00M;
        }
        else
        {
          margin = decimal.Parse(ContractBlotterGrid.Columns["Margin"].Text);
        }

        DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
        {
          BookGroupCombo.Text, 
          ContractBlotterGrid.Columns["Book"].Text
        });

        if (bookRecord != null)
        {
          markRoundHouse = bookRecord["MarkRoundHouse"].ToString().Trim();
          markRoundInstitution = 1M; //decimal.Parse(bookRecord["MarkRoundInstitution"].ToString()) / 100;
          priceMinimum = decimal.Parse(bookRecord["PriceMinimum"].ToString());
          amountMinimum = decimal.Parse(bookRecord["AmountMinimum"].ToString());
        }
        else
        {
          markRoundHouse = "U";
          markRoundInstitution = 1M;
          priceMinimum = 1M;
          amountMinimum = 100M;
        }

				if (price < priceMinimum)
				{
					price = priceMinimum;
				}
				else
				{
					price = price * margin;           
				}

        switch (markRoundHouse)
        {
          case "U" : 
            if ((price % markRoundInstitution) != 0)
            {
              price = (decimal.Floor(price / markRoundInstitution) + 1) * markRoundInstitution;
            }                                                        
            break;
        
          case "D" :
            if ((price % markRoundInstitution) != 0)
            {
              price = (decimal.Floor(price / markRoundInstitution)) * markRoundInstitution;
            }                                                        
            break;
        
          case "N" :
            break;      
        }     
  
				if (mainForm.IsBond)
				{
					price = price / 100;
				}				
      
        amount =  decimal.Round(price * quantity, 2);

        if (amount < amountMinimum)
        {
          amount = amountMinimum;
        }

        ContractBlotterGrid.Columns["Amount"].Text = amount.ToString("#,##0");
      }
      catch (Exception e)
      {
        mainForm.Alert(e.Message);
      }
    }

    private void BookMarginSet()
    {
      try
      {
        DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
        {
          BookGroupCombo.Text, 
          ContractBlotterGrid.Columns["Book"].Text
        });
      
        if (bookRecord != null)
        {
          ContractBlotterGrid.Columns["Margin"].Value = LoansRadio.Checked ? bookRecord["MarginLoan"] : bookRecord["MarginBorrow"];      
          ContractBlotterGrid.Columns["MarginCode"].Text = "%";
        }
        else
        {
          ContractBlotterGrid.Columns["Margin"].Value = DBNull.Value;      
          ContractBlotterGrid.Columns["MarginCode"].Text = "%";
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionContractBlotterForm.BookMarginSet]", Log.Error, 1);
      }
    }
    
    private void BookTableRateSet()
    {
      try
      {
        DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
        {
          BookGroupCombo.Text, 
          ContractBlotterGrid.Columns["Book"].Text
        });

        if (bookRecord == null)
        {
          return;
        }
      
        if (mainForm.IsBond)
        {
          if (BorrowsRadio.Checked && !bookRecord["RateBondLoan"].Equals(DBNull.Value))
          {
            ContractBlotterGrid.Columns["Rate"].Value = bookRecord["RateBondLoan"];
            ContractBlotterGrid.Columns["RateCode"].Value = "T";
          }
          else if (LoansRadio.Checked && !bookRecord["RateBondBorrow"].Equals(DBNull.Value))
          {
            ContractBlotterGrid.Columns["Rate"].Value = bookRecord["RateBondBorrow"];
            ContractBlotterGrid.Columns["RateCode"].Value = "T";
          }
          else
          {
            ContractBlotterGrid.Columns["Rate"].Text = "";
            ContractBlotterGrid.Columns["RateCode"].Text = " ";
          }
        }
        else
        {
          if (BorrowsRadio.Checked && !bookRecord["RateStockLoan"].Equals(DBNull.Value))
          {
            ContractBlotterGrid.Columns["Rate"].Value = bookRecord["RateStockLoan"];
            ContractBlotterGrid.Columns["RateCode"].Value = "T";
          }
          else if (LoansRadio.Checked && !bookRecord["RateStockBorrow"].Equals(DBNull.Value))
          {
            ContractBlotterGrid.Columns["Rate"].Value = bookRecord["RateStockBorrow"];
            ContractBlotterGrid.Columns["RateCode"].Value = "T";
          }
          else
          {
            ContractBlotterGrid.Columns["Rate"].Text = "";
            ContractBlotterGrid.Columns["RateCode"].Text = " ";
          }
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionContractBlotterForm.BookTableRateSet]", Log.Error, 1);
      }
    }

    private void FormStatusSet()
    {
      try
      {
        if (ContractBlotterGrid.SelectedRows.Count > 0)
        {
          StatusLabel.Text = "Selected " + ContractBlotterGrid.SelectedRows.Count.ToString("#,##0") + " items of " + contractDataView.Count.ToString("#,##0") + " shown in grid.";
        }
        else
        {
          StatusLabel.Text = "Showing " + contractDataView.Count.ToString("#,##0") + " items in grid.";
        }
      }
      catch {}
    }

    private void DeleteDeals()
    {
      int n = 0;
      ArrayList dealIdList = new ArrayList();

      try
      {
        if (ContractBlotterGrid.SelectedRows.Count == 0)
        {
          ContractBlotterGrid.SelectedRows.Add(ContractBlotterGrid.Row);
        }

        ContractBlotterGrid.Row = ContractBlotterGrid.SelectedRows[0];

        foreach (int i in ContractBlotterGrid.SelectedRows)
        {          
          string dealId = new String(ContractBlotterGrid.Columns["DealId"].CellText(i).ToCharArray());
          dealIdList.Add(dealId);            
        }
           
        foreach (string dealId in dealIdList)
        {
          try
          {
            mainForm.PositionAgent.DealSet(dealId, "", mainForm.UserId, false);
          }
          catch (Exception e)
          {
            n++;
            mainForm.Alert(e.Message);
          }
        }

        mainForm.Alert("Trashed " + dealIdList.Count + " deal[s] of " + ContractBlotterGrid.SelectedRows.Count + " with " + n + " error[s].");

        ContractBlotterGrid.SelectedCols.Clear();
        ContractBlotterGrid.SelectedRows.Clear();
      }
      catch (Exception e)
      {
        mainForm.Alert(e.Message);
        Log.Write(e.Message + " [PositionContractBlotterForm.DeleteDeals]", Log.Error, 1);
      }    
    }

    private void SendDeals(ArrayList dealArray)
    {
      foreach (DataRow row in dealArray)
      {
        try
        {
          Log.Write("Sending Deal ID " + row["DealId"].ToString() + " in " + row["BookGroup"].ToString() + " for " +
            row["Quantity"].ToString() + " of " + row["SecId"] + " to Contract. [PositionContractBlotterForm.SendDeals]", 3);

          mainForm.PositionAgent.DealSet(row["DealId"].ToString(), "S", mainForm.UserId, true);
        }
        catch (Exception e)
        {
          Log.Write("Error sending Deal ID " + row["DealId"].ToString() + ": " + e.Message + " [PositionContractBlotterForm.SendDeals]", Log.Error, 1);
        }
      }
    }

    #region Windows Form Designer generated code  
    private void InitializeComponent()
    {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionContractBlotterForm));
			this.ContractBlotterGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToProcessAgentMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToTrashBinMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowOtherBookMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowDivRateMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowIncomeTrackedMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.DockMenuItem = new System.Windows.Forms.MenuItem();
			this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
			this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep3MenuItem = new System.Windows.Forms.MenuItem();
			this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupLabel = new C1.Win.C1Input.C1Label();
			this.LoansRadio = new System.Windows.Forms.RadioButton();
			this.BorrowsRadio = new System.Windows.Forms.RadioButton();
			this.BooksDropDown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupCombo = new C1.Win.C1List.C1Combo();
			this.ShowContractsCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.ContractBlotterGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BooksDropDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
			this.SuspendLayout();
			// 
			// ContractBlotterGrid
			// 
			this.ContractBlotterGrid.AllowColSelect = false;
			this.ContractBlotterGrid.AllowFilter = false;
			this.ContractBlotterGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ContractBlotterGrid.AllowUpdate = false;
			this.ContractBlotterGrid.CaptionHeight = 17;
			this.ContractBlotterGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ContractBlotterGrid.EmptyRows = true;
			this.ContractBlotterGrid.ExtendRightColumn = true;
			this.ContractBlotterGrid.FetchRowStyles = true;
			this.ContractBlotterGrid.FilterBar = true;
			this.ContractBlotterGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ContractBlotterGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ContractBlotterGrid.Location = new System.Drawing.Point(1, 32);
			this.ContractBlotterGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ContractBlotterGrid.Name = "ContractBlotterGrid";
			this.ContractBlotterGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ContractBlotterGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ContractBlotterGrid.PreviewInfo.ZoomFactor = 75;
			this.ContractBlotterGrid.RecordSelectorWidth = 16;
			this.ContractBlotterGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ContractBlotterGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ContractBlotterGrid.RowHeight = 15;
			this.ContractBlotterGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.ContractBlotterGrid.Size = new System.Drawing.Size(1282, 473);
			this.ContractBlotterGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.ContractBlotterGrid.TabIndex = 2;
			this.ContractBlotterGrid.Text = "ContractBlotterGrid";
			this.ContractBlotterGrid.WrapCellPointer = true;
			this.ContractBlotterGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ContractBlotterGrid_Paint);
			this.ContractBlotterGrid.AfterUpdate += new System.EventHandler(this.ContractBlotterGrid_AfterUpdate);
			this.ContractBlotterGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.ContractBlotterGrid_BeforeColEdit);
			this.ContractBlotterGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ContractBlotterGrid_SelChange);
			this.ContractBlotterGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ContractBlotterGrid_FetchRowStyle);
			this.ContractBlotterGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContractBlotterGrid_MouseDown);
			this.ContractBlotterGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ContractBlotterGrid_BeforeUpdate);
			this.ContractBlotterGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.ContractBlotterGrid_BeforeColUpdate);
			this.ContractBlotterGrid.FilterChange += new System.EventHandler(this.ContractBlotterGrid_FilterChange);
			this.ContractBlotterGrid.OnAddNew += new System.EventHandler(this.ContractBlotterGrid_OnAddNew);
			this.ContractBlotterGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ContractBlotterGrid_FormatText);
			this.ContractBlotterGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ContractBlotterGrid_KeyPress);
			this.ContractBlotterGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.ContractBlotterGrid_Error);
			this.ContractBlotterGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\" DropDownCtl=\"BooksDropDown\"><ValueItems Translate=\"True\" /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"Quantit" +
				"y\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><ValueItems /><Grou" +
				"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbo" +
				"l\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Am" +
				"ount\" DataField=\"Amount\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Rate\" DataField=\"Rate\" EditM" +
				"askUpdate=\"True\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Margin\" DataField=\"Margin\" EditMaskU" +
				"pdate=\"True\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"MarginCode\" DataField=\"MarginCode\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PC\" DataFi" +
				"eld=\"PoolCode\" EditMask=\"&gt;&amp;\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S\" DataField=\"DealStatus\"" +
				" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
				"taColumn Level=\"0\" Caption=\"T\" DataField=\"DealType\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"RateCode\"><ValueItem" +
				"s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"I\" DataField=\"I" +
				"ncomeTracked\"><ValueItems Presentation=\"CheckBox\" Validate=\"True\" /><GroupInfo /" +
				"></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"DivRate\" DataField=\"DivRate\" Ed" +
				"itMask=\"000.000\" EditMaskUpdate=\"True\" NumberFormat=\"FormatText Event\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Other Book\" Da" +
				"taField=\"OtherBook\" DropDownCtl=\"BooksDropDown\"><ValueItems Translate=\"True\" /><" +
				"GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"IsActive\" DataField=" +
				"\"IsActive\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=" +
				"\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Deal ID\" DataField=\"DealId\" NumberFormat=\"FormatText Event\"><Value" +
				"Items><internalValues><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"-\" " +
				"dispVal=\"-\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"0\" dispVal=" +
				"\"0\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"1\" dispVal=\"1\" /><V" +
				"alueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"2\" dispVal=\"2\" /><ValueItem" +
				" type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"3\" dispVal=\"3\" /><ValueItem type=\"C" +
				"1.Win.C1TrueDBGrid.ValueItem\" Value=\"4\" dispVal=\"4\" /><ValueItem type=\"C1.Win.C1" +
				"TrueDBGrid.ValueItem\" Value=\"5\" dispVal=\"5\" /><ValueItem type=\"C1.Win.C1TrueDBGr" +
				"id.ValueItem\" Value=\"6\" dispVal=\"6\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.Value" +
				"Item\" Value=\"7\" dispVal=\"7\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Va" +
				"lue=\"8\" dispVal=\"8\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"9\" " +
				"dispVal=\"9\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\".\" dispVal=" +
				"\".\" /></internalValues></ValueItems><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"C\" DataField=\"CollateralCode\"><ValueItems CycleOnClick=\"True\" V" +
				"alidate=\"True\"><internalValues><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" V" +
				"alue=\"C\" dispVal=\"C\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"N\"" +
				" dispVal=\"N\" /></internalValues></ValueItems><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Book Group\" DataField=\"BookGroup\"><ValueItems /><Group" +
				"Info /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.Contex" +
				"tWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style8" +
				"5{Font:Verdana, 8.25pt;AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Style78{}Style79{Font:Verdana, 8.25pt;AlignHorz:Near;}S" +
				"tyle72{}Style118{AlignHorz:Near;}Style70{AlignHorz:Near;}Style71{AlignHorz:Near;" +
				"}Style115{Font:Verdana, 8.25pt;AlignHorz:Near;}Style73{Font:Verdana, 8.25pt;Alig" +
				"nHorz:Near;}Style117{}Style116{}Style111{}Style110{}Style113{AlignHorz:Near;}Sty" +
				"le112{AlignHorz:Near;}Style76{AlignHorz:Center;}Style77{AlignHorz:Center;}Style7" +
				"4{}Style75{}Style84{}Style87{}Style86{}Style81{}Style80{}Style83{AlignHorz:Near;" +
				"}Style82{AlignHorz:Near;}Footer{}Style89{AlignHorz:Center;}Style88{AlignHorz:Cen" +
				"ter;}Style108{}Style109{Font:Verdana, 8.25pt;AlignHorz:Near;}Style147{}Style104{" +
				"}Style105{}Style106{AlignHorz:Center;}Style107{AlignHorz:Center;}Editor{}Style10" +
				"1{AlignHorz:Center;}Style102{}Style103{Font:Verdana, 8.25pt;AlignHorz:Near;}Styl" +
				"e94{AlignHorz:Center;}Style95{AlignHorz:Center;}Style96{}Style97{Font:Verdana, 8" +
				".25pt;AlignHorz:Near;}Style90{}Style91{Font:Verdana, 8.25pt;AlignHorz:Near;}Styl" +
				"e92{}Style93{}Style158{}Style98{}Style99{}Heading{Wrap:True;AlignVert:Center;Bor" +
				"der:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19" +
				"{Font:Verdana, 8.25pt;AlignHorz:Near;}Style14{}Style15{}Style16{AlignHorz:Center" +
				";}Style17{AlignHorz:Center;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}S" +
				"tyle159{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style120{}Style12" +
				"1{Font:Verdana, 8.25pt;AlignHorz:Near;}Style153{}Style9{}Style8{}Style28{AlignHo" +
				"rz:Near;}Style29{AlignHorz:Far;}Style27{}Style26{}Style122{}Style123{}Style25{Fo" +
				"nt:Verdana, 8.25pt;AlignHorz:Near;}Style24{}Style23{AlignHorz:Near;}Style22{Alig" +
				"nHorz:Near;}Style21{}Style20{}Style151{Font:Verdana, 8.25pt;AlignHorz:Near;}Styl" +
				"e150{}OddRow{}Style152{}Style155{AlignHorz:Near;}Style154{AlignHorz:Near;}Style1" +
				"57{Font:Verdana, 8.25pt;AlignHorz:Near;}Style156{}Style146{}Style41{AlignHorz:Ne" +
				"ar;}Style40{AlignHorz:Near;}Style38{}Style39{}Style36{}FilterBar{BackColor:SeaSh" +
				"ell;}Style37{Font:Verdana, 8.25pt;AlignHorz:Near;}Style148{AlignHorz:Near;}Style" +
				"149{AlignHorz:Near;}Normal{Font:Verdana, 8.25pt;BackColor:Ivory;}Style34{AlignHo" +
				"rz:Near;}Style49{Font:Verdana, 8.25pt;AlignHorz:Near;}Style48{}Style31{Font:Verd" +
				"ana, 8.25pt;AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style33{}Style142{A" +
				"lignHorz:Near;}Style143{AlignHorz:Near;}Style144{}Style145{Font:Verdana, 8.25pt;" +
				"AlignHorz:Near;}Style43{Font:Verdana, 8.25pt;AlignHorz:Near;}Style42{}Style45{}S" +
				"tyle44{}Style47{AlignHorz:Far;}Style46{AlignHorz:Near;}Style30{}EvenRow{BackColo" +
				"r:Aqua;}Style59{AlignHorz:Far;}Style5{}Style4{}Style7{}Style6{}Style58{AlignHorz" +
				":Near;}RecordSelector{AlignImage:Center;}Style3{}Style2{}Style1{}Style50{}Style5" +
				"1{}Style52{AlignHorz:Near;}Style53{AlignHorz:Far;}Style54{}Style55{Font:Verdana," +
				" 8.25pt;AlignHorz:Near;}Style56{}Style57{}Style160{AlignHorz:Near;}Style164{}Sty" +
				"le165{}Style100{AlignHorz:Near;}Caption{AlignHorz:Center;}Style69{}Style68{}Styl" +
				"e162{}Style163{Font:Verdana, 8.25pt;AlignHorz:Near;}Style60{}Style161{AlignHorz:" +
				"Near;}Style63{}Style62{}Style61{Font:Verdana, 8.25pt;AlignHorz:Near;}Style119{Al" +
				"ignHorz:Far;}Style67{Font:Verdana, 8.25pt;AlignHorz:Near;}Style66{}Style65{Align" +
				"Horz:Near;}Style64{AlignHorz:Near;}Style114{}Group{BackColor:ControlDark;Border:" +
				"None,,0, 0, 0, 0;AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid." +
				"MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"\" All" +
				"owRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeigh" +
				"t=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeSt" +
				"yle=\"SolidCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrol" +
				"lGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" " +
				"/><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"" +
				"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=" +
				"\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle p" +
				"arent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style" +
				"7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\"" +
				" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Sele" +
				"ctedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><" +
				"internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style160\" /><Sty" +
				"le parent=\"Style1\" me=\"Style161\" /><FooterStyle parent=\"Style3\" me=\"Style162\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style163\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style165\" /><GroupFooterStyle parent=\"Style1\" me=\"Style164\" /><Visible>True</" +
				"Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><Locked" +
				">True</Locked><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Height>15</Height><DCIdx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
				"ingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Style1\" me=\"Style95\" /><F" +
				"ooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle parent=\"Style5\" me=\"Style" +
				"97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><GroupFooterStyle parent=" +
				"\"Style1\" me=\"Style98\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>20</Width><Height>15</Height><Locked>True</Locked><DCIdx>11<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>" +
				"True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>70</Width><He" +
				"ight>15</Height><Button>True</Button><DCIdx>0</DCIdx></C1DisplayColumn><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"" +
				"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Styl" +
				"e5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooter" +
				"Style parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkG" +
				"ray,Single</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>2</DCIdx></" +
				"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><S" +
				"tyle parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Vis" +
				"ible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</" +
				"Height><AllowFocus>False</AllowFocus><DCIdx>3</DCIdx></C1DisplayColumn><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"" +
				"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Styl" +
				"e5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooter" +
				"Style parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkG" +
				"ray,Single</ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>1</DCIdx></" +
				"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><S" +
				"tyle parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Vis" +
				"ible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>95</Width><Height>15</" +
				"Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>20</Width><Height>15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Styl" +
				"e53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" " +
				"me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
				"None</ColumnDivider><Width>55</Width><Height>15</Height><HeaderDivider>False</He" +
				"aderDivider><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style104\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>20</Width><Height>15</Height><AllowFocus>False</AllowFocus><DCIdx>" +
				"12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>55</Width>" +
				"<Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style74\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Column" +
				"Divider><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style112\" /><Style parent=\"Style1\" me=\"Style113\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style114\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style115\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style117\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style116\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
				"Single</ColumnDivider><Width>70</Width><Height>15</Height><Button>True</Button><" +
				"DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
				" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Style" +
				"3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle " +
				"parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" />" +
				"<Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>25</" +
				"Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style2\" me=\"Style106\" /><Style parent=\"Style1\" me=\"Style107\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style108\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle109\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style111\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style110\" /><Visible>True</Visible><ColumnDivider>DarkGray,Si" +
				"ngle</ColumnDivider><Width>20</Width><Height>15</Height><DCIdx>13</DCIdx></C1Dis" +
				"playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style118\" /><Style" +
				" parent=\"Style1\" me=\"Style119\" /><FooterStyle parent=\"Style3\" me=\"Style120\" /><E" +
				"ditorStyle parent=\"Style5\" me=\"Style121\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style123\" /><GroupFooterStyle parent=\"Style1\" me=\"Style122\" /><Visible>True</Vi" +
				"sible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>75</Width><Height>15<" +
				"/Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style2\" me=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><FooterStyle pare" +
				"nt=\"Style3\" me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" /><GroupHea" +
				"derStyle parent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
				"yle92\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><W" +
				"idth>20</Width><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Lo" +
				"cked><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle2\" me=\"Style142\" /><Style parent=\"Style1\" me=\"Style143\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style144\" /><EditorStyle parent=\"Style5\" me=\"Style145\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style147\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style146\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>17" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le154\" /><Style parent=\"Style1\" me=\"Style155\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style156\" /><EditorStyle parent=\"Style5\" me=\"Style157\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style159\" /><GroupFooterStyle parent=\"Style1\" me=\"Style158\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>140</W" +
				"idth><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCId" +
				"x>18</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>75</Widt" +
				"h><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style148\" /><Style parent=\"Style1\" me=\"Style149\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style150\" /><EditorStyle parent=\"Style5\" me=\"Style" +
				"151\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style153\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style152\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn></internal" +
				"Cols><ClientRect>0, 0, 1278, 469</ClientRect><BorderSide>0</BorderSide></C1.Win." +
				"C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Sty" +
				"le parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style p" +
				"arent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style pa" +
				"rent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent" +
				"=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style paren" +
				"t=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style pa" +
				"rent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyle" +
				"s><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><" +
				"DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1278, 469</ClientAre" +
				"a><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\"" +
				" me=\"Style15\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.SendToMenuItem,
																																										this.ShowMenuItem,
																																										this.Sep1MenuItem,
																																										this.DockMenuItem,
																																										this.Sep2MenuItem,
																																										this.ExitMenuItem});
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 0;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToExcelMenuItem,
																																									 this.SendToEmailMenuItem,
																																									 this.SendToProcessAgentMenuItem,
																																									 this.SendToTrashBinMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToProcessAgentMenuItem
			// 
			this.SendToProcessAgentMenuItem.Index = 2;
			this.SendToProcessAgentMenuItem.Text = "Process Agent";
			this.SendToProcessAgentMenuItem.Click += new System.EventHandler(this.SendToProcessAgentMenuItem_Click);
			// 
			// SendToEmailMenuItem
			// 
			this.SendToEmailMenuItem.Index = 1;
			this.SendToEmailMenuItem.Text = "Mail Recipient";
			this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
			// 
			// SendToTrashBinMenuItem
			// 
			this.SendToTrashBinMenuItem.Index = 3;
			this.SendToTrashBinMenuItem.Text = "Trash Bin";
			this.SendToTrashBinMenuItem.Click += new System.EventHandler(this.SendToTrashBinMenuItem_Click);
			// 
			// SendToExcelMenuItem
			// 
			this.SendToExcelMenuItem.Index = 0;
			this.SendToExcelMenuItem.Text = "Excel";
			this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
			// 
			// ShowMenuItem
			// 
			this.ShowMenuItem.Index = 1;
			this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.ShowOtherBookMenuItem,
																																								 this.ShowDivRateMenuItem,
																																								 this.ShowIncomeTrackedMenuItem});
			this.ShowMenuItem.Text = "Show";
			// 
			// ShowOtherBookMenuItem
			// 
			this.ShowOtherBookMenuItem.Index = 0;
			this.ShowOtherBookMenuItem.Text = "Other Book";
			this.ShowOtherBookMenuItem.Click += new System.EventHandler(this.ShowOtherBookMenuItem_Click);
			// 
			// ShowDivRateMenuItem
			// 
			this.ShowDivRateMenuItem.Index = 1;
			this.ShowDivRateMenuItem.Text = "Dividend Rate";
			this.ShowDivRateMenuItem.Click += new System.EventHandler(this.ShowDivRateMenuItem_Click);
			// 
			// ShowIncomeTrackedMenuItem
			// 
			this.ShowIncomeTrackedMenuItem.Index = 2;
			this.ShowIncomeTrackedMenuItem.Text = "Income Tracked";
			this.ShowIncomeTrackedMenuItem.Click += new System.EventHandler(this.ShowIncomeTrackedMenuItem_Click);
			// 
			// Sep1MenuItem
			// 
			this.Sep1MenuItem.Index = 2;
			this.Sep1MenuItem.Text = "-";
			// 
			// DockMenuItem
			// 
			this.DockMenuItem.Index = 3;
			this.DockMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.DockTopMenuItem,
																																								 this.DockBottomMenuItem,
																																								 this.Sep3MenuItem,
																																								 this.DockNoneMenuItem});
			this.DockMenuItem.Text = "Dock";
			// 
			// DockTopMenuItem
			// 
			this.DockTopMenuItem.Index = 0;
			this.DockTopMenuItem.Text = "Top";
			this.DockTopMenuItem.Click += new System.EventHandler(this.DockTopMenuItem_Click);
			// 
			// DockBottomMenuItem
			// 
			this.DockBottomMenuItem.Index = 1;
			this.DockBottomMenuItem.Text = "Bottom";
			this.DockBottomMenuItem.Click += new System.EventHandler(this.DockBottomMenuItem_Click);
			// 
			// Sep3MenuItem
			// 
			this.Sep3MenuItem.Index = 2;
			this.Sep3MenuItem.Text = "-";
			// 
			// DockNoneMenuItem
			// 
			this.DockNoneMenuItem.Index = 3;
			this.DockNoneMenuItem.Text = "None";
			this.DockNoneMenuItem.Click += new System.EventHandler(this.DockNoneMenuItem_Click);
			// 
			// Sep2MenuItem
			// 
			this.Sep2MenuItem.Index = 4;
			this.Sep2MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 5;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// BookGroupNameLabel
			// 
			this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
			this.BookGroupNameLabel.Location = new System.Drawing.Point(202, 6);
			this.BookGroupNameLabel.Name = "BookGroupNameLabel";
			this.BookGroupNameLabel.Size = new System.Drawing.Size(298, 18);
			this.BookGroupNameLabel.TabIndex = 8;
			this.BookGroupNameLabel.Tag = null;
			this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// BookGroupLabel
			// 
			this.BookGroupLabel.Location = new System.Drawing.Point(4, 6);
			this.BookGroupLabel.Name = "BookGroupLabel";
			this.BookGroupLabel.Size = new System.Drawing.Size(92, 18);
			this.BookGroupLabel.TabIndex = 7;
			this.BookGroupLabel.Tag = null;
			this.BookGroupLabel.Text = "BookGroup:";
			this.BookGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BookGroupLabel.TextDetached = true;
			// 
			// LoansRadio
			// 
			this.LoansRadio.Enabled = false;
			this.LoansRadio.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoansRadio.Location = new System.Drawing.Point(632, 8);
			this.LoansRadio.Name = "LoansRadio";
			this.LoansRadio.Size = new System.Drawing.Size(68, 20);
			this.LoansRadio.TabIndex = 17;
			this.LoansRadio.Text = "Loans";
			// 
			// BorrowsRadio
			// 
			this.BorrowsRadio.Checked = true;
			this.BorrowsRadio.Enabled = false;
			this.BorrowsRadio.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BorrowsRadio.Location = new System.Drawing.Point(524, 8);
			this.BorrowsRadio.Name = "BorrowsRadio";
			this.BorrowsRadio.Size = new System.Drawing.Size(84, 20);
			this.BorrowsRadio.TabIndex = 16;
			this.BorrowsRadio.TabStop = true;
			this.BorrowsRadio.Text = "Borrows";
			this.BorrowsRadio.CheckedChanged += new System.EventHandler(this.BorrowsRadio_CheckedChanged);
			// 
			// BooksDropDown
			// 
			this.BooksDropDown.AllowColMove = true;
			this.BooksDropDown.AllowColSelect = false;
			this.BooksDropDown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.BooksDropDown.AlternatingRows = false;
			this.BooksDropDown.CaptionHeight = 17;
			this.BooksDropDown.ColumnCaptionHeight = 17;
			this.BooksDropDown.ColumnFooterHeight = 17;
			this.BooksDropDown.Enabled = false;
			this.BooksDropDown.ExtendRightColumn = true;
			this.BooksDropDown.FetchRowStyles = false;
			this.BooksDropDown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.BooksDropDown.Location = new System.Drawing.Point(132, 126);
			this.BooksDropDown.Name = "BooksDropDown";
			this.BooksDropDown.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BooksDropDown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.BooksDropDown.RowHeight = 15;
			this.BooksDropDown.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BooksDropDown.ScrollTips = false;
			this.BooksDropDown.Size = new System.Drawing.Size(492, 186);
			this.BooksDropDown.TabIndex = 21;
			this.BooksDropDown.TabStop = false;
			this.BooksDropDown.ValueTranslate = true;
			this.BooksDropDown.Visible = false;
			this.BooksDropDown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Book Name\" DataField=\"BookName\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Capti" +
				"on{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{ForeColor:Hi" +
				"ghlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{AlignHorz:Nea" +
				"r;}Style15{AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}Od" +
				"dRow{}Style13{}Style12{}Footer{}HighlightRow{ForeColor:HighlightText;BackColor:H" +
				"ighlight;}RecordSelector{AlignImage:Center;}Style24{}Style23{}Style22{}Style21{A" +
				"lignHorz:Near;}Style20{AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Cen" +
				"ter;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}FilterBar" +
				"{}Style4{}Style9{}Style8{}Style5{}Group{BackColor:ControlDark;Border:None,,0, 0," +
				" 0, 0;AlignVert:Center;}Style7{}Style6{}Style1{}Style3{}Style2{}</Data></Styles>" +
				"<Splits><C1.Win.C1TrueDBGrid.DropdownView AllowColSelect=\"False\" Name=\"\" AllowRo" +
				"wSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"1" +
				"7\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=" +
				"\"16\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" Horizon" +
				"talScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle par" +
				"ent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterB" +
				"arStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style" +
				"3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me" +
				"=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyl" +
				"e parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><Re" +
				"cordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"" +
				"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" " +
				"me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>D" +
				"arkGray,Single</ColumnDivider><Width>70</Width><Height>15</Height><DCIdx>0</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" " +
				"/><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True<" +
				"/Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>75</Width><Height>" +
				"15</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 48" +
				"8, 182</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView>" +
				"</Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"" +
				"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Cap" +
				"tion\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selec" +
				"ted\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highlight" +
				"Row\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" " +
				"/><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Filte" +
				"rBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSp" +
				"lits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</" +
				"DefaultRecSelWidth><ClientArea>0, 0, 488, 182</ClientArea></Blob>";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.StatusLabel.Location = new System.Drawing.Point(20, 508);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(1124, 16);
			this.StatusLabel.TabIndex = 22;
			this.StatusLabel.Tag = null;
			this.StatusLabel.TextDetached = true;
			// 
			// BookGroupCombo
			// 
			this.BookGroupCombo.AddItemSeparator = ';';
			this.BookGroupCombo.AutoCompletion = true;
			this.BookGroupCombo.AutoDropDown = true;
			this.BookGroupCombo.AutoSelect = true;
			this.BookGroupCombo.AutoSize = false;
			this.BookGroupCombo.Caption = "";
			this.BookGroupCombo.CaptionHeight = 17;
			this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.BookGroupCombo.ColumnCaptionHeight = 17;
			this.BookGroupCombo.ColumnFooterHeight = 17;
			this.BookGroupCombo.ContentHeight = 14;
			this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.BookGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.BookGroupCombo.DropDownWidth = 425;
			this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.BookGroupCombo.EditorHeight = 14;
			this.BookGroupCombo.ExtendRightColumn = true;
			this.BookGroupCombo.GapHeight = 2;
			this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.BookGroupCombo.ItemHeight = 15;
			this.BookGroupCombo.KeepForeColor = true;
			this.BookGroupCombo.LimitToList = true;
			this.BookGroupCombo.Location = new System.Drawing.Point(104, 4);
			this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
			this.BookGroupCombo.MaxDropDownItems = ((short)(10));
			this.BookGroupCombo.MaxLength = 15;
			this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
			this.BookGroupCombo.Name = "BookGroupCombo";
			this.BookGroupCombo.PartialRightColumn = false;
			this.BookGroupCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BookGroupCombo.Size = new System.Drawing.Size(96, 20);
			this.BookGroupCombo.TabIndex = 23;
			this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
			this.BookGroupCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book Group\"" +
				" DataField=\"BookGroup\" DataWidth=\"100\"><ValueItems /></C1DataColumn><C1DataColum" +
				"n Level=\"0\" Caption=\"Book Group Name\" DataField=\"BookName\" DataWidth=\"350\"><Valu" +
				"eItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWra" +
				"pper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center" +
				";}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackCo" +
				"lor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive" +
				"{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignH" +
				"orz:Center;}Normal{BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackCo" +
				"lor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}" +
				"Style13{Font:Verdana, 8.25pt;AlignHorz:Near;}Heading{Wrap:True;AlignVert:Center;" +
				"Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style" +
				"10{}Style11{}Style14{}Style15{AlignHorz:Near;}Style16{Font:Verdana, 8.25pt;Align" +
				"Horz:Near;}Style17{}Style1{Font:Verdana, 8.25pt;}</Data></Styles><Splits><C1.Win" +
				".C1List.ListBoxView AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCapt" +
				"ionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGr" +
				"oup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><intern" +
				"alCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style pare" +
				"nt=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDi" +
				"vider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>75</Wid" +
				"th><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><S" +
				"tyle>Single</Style></ColumnDivider><Width>250</Width><Height>15</Height><DCIdx>1" +
				"</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>16</Width></VScrollB" +
				"ar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=" +
				"\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Foo" +
				"ter\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle paren" +
				"t=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /" +
				"><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=" +
				"\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><Selected" +
				"Style parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1." +
				"Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Sty" +
				"le parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style p" +
				"arent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style pa" +
				"rent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style " +
				"parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style paren" +
				"t=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSt" +
				"yles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layou" +
				"t><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// ShowContractsCheckBox
			// 
			this.ShowContractsCheckBox.Location = new System.Drawing.Point(816, 8);
			this.ShowContractsCheckBox.Name = "ShowContractsCheckBox";
			this.ShowContractsCheckBox.Size = new System.Drawing.Size(160, 20);
			this.ShowContractsCheckBox.TabIndex = 52;
			this.ShowContractsCheckBox.Text = "Show Booked Contracts";
			this.ShowContractsCheckBox.CheckedChanged += new System.EventHandler(this.ShowContractsCheckBox_CheckedChanged);
			// 
			// PositionContractBlotterForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1284, 525);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.ShowContractsCheckBox);
			this.Controls.Add(this.BookGroupCombo);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.BooksDropDown);
			this.Controls.Add(this.BorrowsRadio);
			this.Controls.Add(this.ContractBlotterGrid);
			this.Controls.Add(this.LoansRadio);
			this.Controls.Add(this.BookGroupNameLabel);
			this.Controls.Add(this.BookGroupLabel);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 32;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionContractBlotterForm";
			this.Text = "Position - Contract Blotter";
			this.Load += new System.EventHandler(this.PositionContractBlotterForm_Load);
			this.Closed += new System.EventHandler(this.PositionContractBlotterForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.ContractBlotterGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BooksDropDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion
    
    private void PositionContractBlotterForm_Load(object sender, System.EventArgs e)
    {
      this.Top    = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
      this.Left   = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height","480"));
      this.Width  = int.Parse(RegistryValue.Read(this.Name, "Width", "1176"));
      
      this.Text = TEXT;

      string bookRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND BookGroup  <> Book";
      
      this.Show();
      this.Cursor = Cursors.WaitCursor;
      Application.DoEvents();
      
      mainForm.Alert("Please wait... Loading the contract blotter.");

      try
      {
        mainForm.PositionAgent.DealEvent += new DealEventHandler(dealEventWrapper.DoEvent); 
        dataSet = mainForm.PositionAgent.DealDataGet(mainForm.UtcOffset, DEAL_ID_PREFIX, mainForm.UserId, "PositionContractBlotter", true.ToString());                    
    
        contractDataView = new DataView(dataSet.Tables["Deals"], "BookGroup = ''", "", DataViewRowState.CurrentRows);                  
        contractDataView.Sort = "DealId";

        ContractBlotterGrid.SetDataBinding(contractDataView, null, true);
    
        bookDataView = new DataView(dataSet.Tables["Books"]);
        bookDataView.Sort = "Book";
                
        BooksDropDown.SetDataBinding(bookDataView, null, true);

        BookGroupCombo.HoldFields();
        BookGroupCombo.DataSource = dataSet.Tables["BookGroups"];
        BookGroupCombo.SelectedIndex = -1;
       
        BookGroupNameLabel.DataSource = dataSet.Tables["BookGroups"];
        BookGroupNameLabel.DataField = "BookName";               

        if(RegistryValue.Read(this.Name, "BookGroup").Equals(""))
        {
          BookGroupCombo.SelectedIndex = 0;
        }
        else
        {
          BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", "");                
        }

        mainForm.Alert("Loading the contract blotter... Done!");
        this.IsReady = true;      
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message);
        mainForm.Alert("Loading the contract blotter... Error!");
        Log.Write(error.Message + ". [PositionContractBlotterForm.PositionContractBlotterForm_Load]", Log.Error, 1);               
      }      
            
      this.Cursor = Cursors.Default;      
    }

    private void PositionContractBlotterForm_Closed(object sender, System.EventArgs e)
    {
      RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);
      
      if(this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
      } 
      
      try
      {
        if (dealEventWrapper != null)
        {      
          mainForm.PositionAgent.DealEvent -= new DealEventHandler(dealEventWrapper.DoEvent);
          dealEventWrapper.DealEvent       -= new DealEventHandler(DealOnEvent);
          dealEventWrapper                  = null; 
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + ". [PositionContractBlotterForm.PositionContractBlotterForm_Closed]", Log.Error, 1); 
      }

      mainForm.positionContractBlotterForm = null;
    }
  
    private void BookGroupCombo_TextChanged(object sender, System.EventArgs e)
    {
      int row = -1;

      foreach(DataRow dataRow in dataSet.Tables["BookGroups"].Rows)
      {
        row ++;

        if(dataRow["BookGroup"].ToString().Equals(BookGroupCombo.Text))
        {
          break;
        }        
      }

      if (bool.Parse(dataSet.Tables["BookGroups"].Rows[row]["MayView"].ToString()))
      {
        mayView = true;
        ContractBlotterGrid.AllowUpdate     = false;
        ContractBlotterGrid.AllowAddNew     = false;
        SendToProcessAgentMenuItem.Enabled  = false;
        SendToTrashBinMenuItem.Enabled      = false;
				SendToExcelMenuItem.Enabled					= false;
        BooksDropDown.Enabled               = false;
        BorrowsRadio.Enabled                = true;
        LoansRadio.Enabled                  = true;    
        bookDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND BookGroup  <> Book";
  
        mainForm.GridFilterClear(ref ContractBlotterGrid); 
    
        if (BorrowsRadio.Checked)
        {
          contractRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'B' AND IsActive = 1";
        }
        else
        {
          contractRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'L' AND IsActive = 1";
        }

        contractDataView.RowFilter = contractRowFilter;
    
        FormStatusSet();
      
        if (bool.Parse(dataSet.Tables["BookGroups"].Rows[row]["MayEdit"].ToString()))
        {
          ContractBlotterGrid.AllowUpdate     = true;
          ContractBlotterGrid.AllowAddNew     = true;
          SendToProcessAgentMenuItem.Enabled  = true;
          SendToTrashBinMenuItem.Enabled      = true;
					SendToExcelMenuItem.Enabled					= true;
          BooksDropDown.Enabled               = true;
          BorrowsRadio.Enabled                = true;
          LoansRadio.Enabled                  = true;    
          bookDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND BookGroup  <> Book";
  
          mainForm.GridFilterClear(ref ContractBlotterGrid);           
        }
        else
        {
          ContractBlotterGrid.AllowUpdate     = false;
          ContractBlotterGrid.AllowAddNew     = false;
          SendToProcessAgentMenuItem.Enabled  = false;
          SendToTrashBinMenuItem.Enabled      = false;
					SendToExcelMenuItem.Enabled					= false;
          BooksDropDown.Enabled               = false;
          BorrowsRadio.Enabled                = true;
          LoansRadio.Enabled                  = true;  
      
          mainForm.Alert("UserId: " + mainForm.UserId+ ", Permission EDIT denied.");
        }     
      }
      else
      {
        mayView = false;
        ContractBlotterGrid.AllowUpdate     = false;
        ContractBlotterGrid.AllowAddNew     = false;
        SendToProcessAgentMenuItem.Enabled  = false;
        SendToTrashBinMenuItem.Enabled      = false;
				SendToExcelMenuItem.Enabled					= false;
        BooksDropDown.Enabled               = false;
        BorrowsRadio.Enabled                = false;
        LoansRadio.Enabled                  = false;  
      
        contractDataView.RowFilter  = "BookGroup = ''";
      
        mainForm.Alert("UserId: " + mainForm.UserId+ ", Permission VIEW denied.");
      }
    }

    private void BorrowsRadio_CheckedChanged(object sender, System.EventArgs e)
    {
      mainForm.GridFilterClear(ref ContractBlotterGrid);

      if (BorrowsRadio.Checked)
      {
        contractRowFilter = (mayView) ? "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'B' AND IsActive = 1" : "BookGroup = ''";
      }
      else
      {
        contractRowFilter = (mayView) ? "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'L' AND IsActive = 1" : "BookGroup = ''";
      }

      if (!ShowContractsCheckBox.Checked)
      {
        contractRowFilter += " And DealStatus <> 'C'";
      }

      contractDataView.RowFilter = contractRowFilter;
      ContractBlotterGrid.Style.BackColor = BorrowsRadio.Checked ? Color.Ivory : Color.Honeydew;

      FormStatusSet();
    }

    private void ShowContractsCheckBox_CheckedChanged(object sender, System.EventArgs e)
    {
      BorrowsRadio_CheckedChanged(sender, e);
    }
    
    private void ContractBlotterGrid_FilterChange(object sender, System.EventArgs e)
    {
      string gridFilter;

      try
      {
        gridFilter = mainForm.GridFilterGet(ref ContractBlotterGrid);

        if (gridFilter.Equals(""))
        {
          contractDataView.RowFilter = contractRowFilter;
        }
        else
        {
          contractDataView.RowFilter = contractRowFilter + " And " + gridFilter;
        }
      }
      catch (Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);
      }

      FormStatusSet();
    }

    private void ContractBlotterGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      if (e.Value.Length == 0)
      {
        return;
      }

      switch (e.Column.DataField)
      {    
        case "Quantity" :
        case "Amount" : 
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0");
          }
          catch {}
          break;

        case "FixedInvestmentRate":
        case "Rate" :
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("0.000");                      
          }
          catch {}
          break;

        case "DivRate" :
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("0.000");                      
          }
          catch {}
          break;
       
        case "Margin" : 
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("0.00");                      
          }
          catch {}
          break;
       
        case "ActTime" :
          try
          {
            e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeFileFormat);                                     
          }
          catch {}
          break;       
      }
    }
    
    private void ContractBlotterGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      FormStatusSet();
    }

    private void ContractBlotterGrid_OnAddNew(object sender, System.EventArgs e)
    {
      ContractBlotterGrid.Columns["DealId"].Value = mainForm.ServiceAgent.NewProcessId(DEAL_ID_PREFIX);
      ContractBlotterGrid.Columns["BookGroup"].Value = BookGroupCombo.Text;
      ContractBlotterGrid.Columns["DealType"].Value = BorrowsRadio.Checked ? "B" : "L";
      
      ContractBlotterGrid.Columns["Book"].Value = _book;
      ContractBlotterGrid.Columns["CollateralCode"].Value = _collateralCode.Trim().Equals("") ? "C" : _collateralCode;
      ContractBlotterGrid.Columns["IncomeTracked"].Value = true;
      ContractBlotterGrid.Columns["DivRate"].Value = 100.000;
      ContractBlotterGrid.Columns["DealStatus"].Value = "N";

      BookMarginSet();
    }

    private void ContractBlotterGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {      
      if (ContractBlotterGrid.FilterActive)
      {
        e.Cancel = false;
        return;
      }
      
      if (ContractBlotterGrid.Columns["DealStatus"].Value.ToString().Equals("P"))
      {
        e.Cancel = true;
        return;
      }
    }

    private void ContractBlotterGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
    {
      try
      {
        switch (e.Column.DataColumn.DataField)
        {
          case "SecId" :
            mainForm.SecId = ContractBlotterGrid.Columns["SecId"].Text;
            
            ContractBlotterGrid.Columns["SecId"].Text = mainForm.SecId;       
            ContractBlotterGrid.Columns["Symbol"].Text = mainForm.Symbol;
						
						if (mainForm.PositionAgent.BlockedSecId(mainForm.SecId) && LoansRadio.Checked)
						{
							ContractBlotterGrid.Columns["DealStatus"].Text = "H";
						}

            BookTableRateSet();
            BookMarginSet();
            AmountSet();
            break;

          case "Rate" :
            if (ContractBlotterGrid.Columns["Rate"].Text.Trim().Equals(""))
            {
              BookTableRateSet();
            }
            else
            {
              ContractBlotterGrid.Columns["RateCode"].Text = " ";
            }
            break;

          case "Book" :							
						if (mainForm.PositionAgent.BlockedSecId(mainForm.SecId) && !mainForm.SecId.Equals("") && LoansRadio.Checked)
						{
							ContractBlotterGrid.Columns["DealStatus"].Text = "H";
						}

            BookTableRateSet();
            BookMarginSet();
            AmountSet();
            break;

          case "Quantity" :
            AmountSet();
            break;

          case "Amount" :
            string amount = ContractBlotterGrid.Columns["Amount"].Text.ToUpper();

            if (amount.StartsWith("P") || amount.EndsWith("P"))
            {
              amount = amount.Trim('P');

              if (Tools.IsNumeric(amount))
              {
                ContractBlotterGrid.Columns["Amount"].Value = decimal.Parse(amount) * (long)ContractBlotterGrid.Columns["Quantity"].Value;
              }
            }
            break;

          case "Margin" :
            if (ContractBlotterGrid.Columns["Margin"].Text.Trim().Equals(""))
            {
              BookMarginSet();
            }
            else
            {
              ContractBlotterGrid.Columns["MarginCode"].Text = "%";
            }
            
            AmountSet();
            break;
        }
      }
      catch (Exception ee)
      {
        mainForm.Alert(ee.Message);
      }
    }

    private void ContractBlotterGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      string gridData = "";

      try
      {
        if (e.KeyChar.Equals((char)13) && (ContractBlotterGrid.Col > 5) && ContractBlotterGrid.DataChanged)
        {
          ContractBlotterGrid.UpdateData();

          ContractBlotterGrid.MoveNext();
          ContractBlotterGrid.Col = 4;

          e.Handled = true;
        }

        if (e.KeyChar.Equals((char)13) && !ContractBlotterGrid.DataChanged)
        {

          ContractBlotterGrid.MoveLast();
          ContractBlotterGrid.Row += 1;
          ContractBlotterGrid.Col = 4;

          e.Handled = true;
        }

        if (e.KeyChar.Equals((char)27))
        {
          if (!ContractBlotterGrid.EditActive && ContractBlotterGrid.DataChanged)
          {
            ContractBlotterGrid.DataChanged = false;
          }
        }

        if (e.KeyChar.Equals((char)3) && ContractBlotterGrid.SelectedRows.Count > 0)
        {
          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.SelectedCols)
          {
            gridData += dataColumn.Caption + "\t";
          }

          gridData += "\n";

          foreach (int row in ContractBlotterGrid.SelectedRows)
          {
            foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.SelectedCols)
            {
              gridData += dataColumn.CellText(row) + "\t";
            }

            gridData += "\n";

            if ((row % 100) == 0)
            {
              mainForm.Alert("Please wait: " + row.ToString("#,##0") + " rows copied so far...");
            }
          }

          Clipboard.SetDataObject(gridData, true);
          mainForm.Alert("Copied " + ContractBlotterGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
          e.Handled = true;
        }

      }
      catch (Exception ee)
      {
        mainForm.Alert(ee.Message);
      }
    }

    private void ContractBlotterGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        if (ContractBlotterGrid.Columns["SecId"].Text.Equals("") ||
          ContractBlotterGrid.Columns["Quantity"].Text.Equals(""))          
        {
          mainForm.Alert("Both a Security ID and Quantity are required.");
          e.Cancel = true;
        }
        else
        {
          _dealId = ContractBlotterGrid.Columns["DealId"].Text;
          _bookGroup = ContractBlotterGrid.Columns["BookGroup"].Text;
          _dealType = ContractBlotterGrid.Columns["DealType"].Text;
          _book = ContractBlotterGrid.Columns["Book"].Text;
          _secId = ContractBlotterGrid.Columns["SecId"].Text.Trim();
          _quantity = ContractBlotterGrid.Columns["Quantity"].Value.ToString();
          _amount = ContractBlotterGrid.Columns["Amount"].Value.ToString();
          _collateralCode = ContractBlotterGrid.Columns["CollateralCode"].Text;
          _rate = ContractBlotterGrid.Columns["Rate"].Value.ToString();
          _rateCode = ContractBlotterGrid.Columns["RateCode"].Text;
          _poolCode = ContractBlotterGrid.Columns["PoolCode"].Text;
          _divRate = ContractBlotterGrid.Columns["DivRate"].Value.ToString();
          _incomeTracked = ContractBlotterGrid.Columns["IncomeTracked"].Value.ToString();
          _margin = ContractBlotterGrid.Columns["Margin"].Value.ToString();
          _marginCode = ContractBlotterGrid.Columns["MarginCode"].Value.ToString();
          _comment = ContractBlotterGrid.Columns["Comment"].Text;          																																																						
					_dealStatus = ContractBlotterGrid.Columns["DealStatus"].Text;

          ContractBlotterGrid.Columns["ActUserShortName"].Text = mainForm.UserShortName;
          ContractBlotterGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateTimeFileFormat);

          mainForm.Alert("Updating " + _secId + " for " + _quantity + "..."); 
        }
      }
      catch (Exception ee)
      {
        mainForm.Alert("Updating " + _secId + " for " + _quantity + "... Error!"); 
        Log.Write(ee.Message + " [PositionContractBlotterForm.ContractBlotterGrid_BeforeUpdate]", Log.Error, 1);

        e.Cancel = true;
      }
    }

    private void ContractBlotterGrid_AfterUpdate(object sender, System.EventArgs e)
    {
      try
      {     
        dataSet.Tables["Deals"].AcceptChanges();

        if (!_dealStatus.Equals("S"))  // OK to save and broadcast the update.
        {
          mainForm.PositionAgent.DealSet(
            _dealId,
            _bookGroup,
            _dealType,
            _book,
            "",
            _secId,
            _quantity,
            _amount,
            _collateralCode,
            _valueDate,
            _settleDate,
            _termDate,
            _rate,
            _rateCode,
            _poolCode,
            _divRate,
            _divCallable,
            _incomeTracked,
            _margin,
            _marginCode,
            _currencyIso,
            _securityDepot,
            _cashDepot,
            _comment,
            (_dealStatus.Equals("H"))? "H": "D",
            mainForm.UserId,
            true
            );

          mainForm.Alert("Updating " + _secId + " for " + _quantity + "... Done!"); 
        }      

      }
      catch (Exception error)
      {
        mainForm.Alert("Updating " + _secId + " for " + _quantity + "... Error!"); 
        Log.Write(error.Message + " [PositionContractBlotterForm.ContractBlotterGrid_AfterUpdate]", Log.Error, 1);
      }
    }

    private void ContractBlotterGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
    {
      e.Handled = true;
    }

    private void ContractBlotterGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      try
      {
        if (!ContractBlotterGrid.Columns["SecId"].Text.Equals(secId))
        {
          secId = ContractBlotterGrid.Columns["SecId"].Text;

          if (!ContractBlotterGrid.EditActive) // Editing is done.
          {
            this.Cursor = Cursors.WaitCursor;
            mainForm.SecId = secId;
            this.Cursor = Cursors.Default;
          }
        }
      }
      catch 
      {
        secId = "";

        this.Cursor = Cursors.WaitCursor;
        mainForm.SecId = "";
        this.Cursor = Cursors.Default;
      }    
    }

    private void ContractBlotterGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= ContractBlotterGrid.RecordSelectorWidth && e.Y <= ContractBlotterGrid.RowHeight)
      {
        if (ContractBlotterGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < ContractBlotterGrid.Splits[0,0].Rows.Count; i++)
          {
            ContractBlotterGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.Columns)
          {
            ContractBlotterGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          ContractBlotterGrid.SelectedRows.Clear();
          ContractBlotterGrid.SelectedCols.Clear();
        }
      }
    }

    private void ContractBlotterGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
    {
      try
      {
        switch (ContractBlotterGrid.Columns["DealStatus"].CellText(e.Row))
        {
          case "S" : //Sent deal, booked Contract
            e.CellStyle.BackColor = Color.LightSteelBlue;
            break;
          case "E" : //Error
            e.CellStyle.BackColor = Color.LightCoral;
            break;
					case "H" :
						e.CellStyle.BackColor = Color.LightGoldenrodYellow;
						break;
					case "L" : //Exceeds Credit Limit
						e.CellStyle.BackColor = Color.Crimson;
						break;
					default :
            e.CellStyle.BackColor = BorrowsRadio.Checked ? Color.Ivory : Color.Honeydew;
            break;
        }        
      }
      catch {}
    }


    private void SendToProcessAgentMenuItem_Click(object sender, System.EventArgs e)
    {
      ArrayList dataRows = new ArrayList();

      DataRowView dataRowView;
      DataRow dataRow;

      try
      {
        if (ContractBlotterGrid.SelectedRows.Count == 0)
        {
          ContractBlotterGrid.SelectedRows.Add(ContractBlotterGrid.Row);
        }

        ContractBlotterGrid.Row = ContractBlotterGrid.SelectedRows[0];

        foreach (int i in ContractBlotterGrid.SelectedRows)
        {
          switch (ContractBlotterGrid.Columns["DealStatus"].CellText(i))
          {
            case "S" :
              mainForm.Alert("Deal in " + ContractBlotterGrid.Columns["SecId"].CellText(i) + " for " + ContractBlotterGrid.Columns["Quantity"].CellText(i) + " has already been sent.");
              continue;
            //case "P" :
            //  mainForm.Alert("Deal in " + ContractBlotterGrid.Columns["SecId"].CellText(i) + " for " + ContractBlotterGrid.Columns["Quantity"].CellText(i) + " is already pending.");
            //  continue;
						
						case "H" :
							mainForm.Alert("Deal in " + ContractBlotterGrid.Columns["SecId"].CellText(i) + " for " + ContractBlotterGrid.Columns["Quantity"].CellText(i) + " is being blocked.");
							continue;
						
						case "C" :
              mainForm.Alert("Deal in " + ContractBlotterGrid.Columns["SecId"].CellText(i) + " for " + ContractBlotterGrid.Columns["Quantity"].CellText(i) + " is already a contract.");
              continue;
          }

          if (ContractBlotterGrid.Columns["Book"].CellText(i).Equals(""))
          {
            mainForm.Alert("Deal in " + ContractBlotterGrid.Columns["SecId"].CellText(i) + " for " + ContractBlotterGrid.Columns["Quantity"].CellText(i) + " needs a Book.");
            continue;
          }

          if (ContractBlotterGrid.Columns["Rate"].CellText(i).Equals(""))
          {
            mainForm.Alert("Deal in " + ContractBlotterGrid.Columns["SecId"].CellText(i) + " for " + ContractBlotterGrid.Columns["Quantity"].CellText(i) + " needs a Rate.");
            continue;
          }
          
          if (ContractBlotterGrid.Columns["Amount"].CellText(i).Equals(""))
          {
            mainForm.Alert("Deal in " + ContractBlotterGrid.Columns["SecId"].CellText(i) + " for " + ContractBlotterGrid.Columns["Quantity"].CellText(i) + " needs an Amount.");
            continue;
          }									
					
					
					dataRowView = (DataRowView)ContractBlotterGrid[i];
          dataRow = dataRowView.Row.Table.NewRow(); 												
          dataRow.ItemArray = dataRowView.Row.ItemArray;

          dataRows.Add(dataRow);

          dataRowView.BeginEdit();
          dataRowView["DealStatus"] = "S";
          dataRowView.EndEdit();
        }



        mainForm.Alert("Sending " + dataRows.Count + " deal[s] of " + ContractBlotterGrid.SelectedRows.Count + " to Contract.");
      
        ContractBlotterGrid.SelectedRows.Clear();
        ContractBlotterGrid.SelectedCols.Clear();

        SendDealsDelegate sendDealsDelegate = new SendDealsDelegate(SendDeals);
        sendDealsDelegate.BeginInvoke(dataRows, null, null);
      }
      catch (Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);
        Log.Write(ee.Message + " [PositionContractBlotterForm.SendToProcessAgentMenuItem_Click]", Log.Error, 1);
      }    
    }

    private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
    {
      int textLength;
      int [] maxTextLength;

      int columnIndex = -1;
      string gridData = "\n\n";

      if (ContractBlotterGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows.");
        return;
      }

      try
      {
        maxTextLength = new int[ContractBlotterGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in ContractBlotterGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in ContractBlotterGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ContractBlotterGrid.SelectedCols)
          {
            if (dataColumn.Value.GetType().Equals(typeof(System.String)))
            {
              gridData += dataColumn.CellText(rowIndex).PadRight(maxTextLength[++columnIndex] + 2);
            }
            else
            {
              gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
            }
          }
  
          gridData += "\n";
        }
                
        Email email = new Email();
        email.Send(gridData);

        mainForm.Alert("Total: " + ContractBlotterGrid.SelectedRows.Count + " items added to e-mail.");
      }
      catch (Exception error)
      {       
        Log.Write(error.Message + ". [PositionBoxSummaryForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
        mainForm.Alert(error.Message, PilotState.RunFault);
      }
    }

    private void SendToTrashBinMenuItem_Click(object sender, System.EventArgs e)
    {
      DeleteDeals();
    }
  
    private void ShowOtherBookMenuItem_Click(object sender, System.EventArgs e)
    {
      ShowOtherBookMenuItem.Checked = !ShowOtherBookMenuItem.Checked;

      ContractBlotterGrid.Splits[0,0].DisplayColumns["OtherBook"].Visible = ShowOtherBookMenuItem.Checked;
    }

    private void ShowDivRateMenuItem_Click(object sender, System.EventArgs e)
    {
      ShowDivRateMenuItem.Checked = !ShowDivRateMenuItem.Checked;

      ContractBlotterGrid.Splits[0,0].DisplayColumns["DivRate"].Visible = ShowDivRateMenuItem.Checked;
    }

    private void ShowIncomeTrackedMenuItem_Click(object sender, System.EventArgs e)
    {
      ShowIncomeTrackedMenuItem.Checked = !ShowIncomeTrackedMenuItem.Checked;

      ContractBlotterGrid.Splits[0,0].DisplayColumns["IncomeTracked"].Visible = ShowIncomeTrackedMenuItem.Checked;
    }

    private void DockTopMenuItem_Click(object sender, System.EventArgs e)
    {
      RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
      RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
      RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
      RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
      
      this.Height = mainForm.ClientSize.Height / 3;
      this.Dock = DockStyle.Top;
      this.ControlBox = false;
      this.Text = "";
    }

    private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
    {
      RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
      RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
      RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
      RegistryValue.Write(this.Name, "Width", this.Width.ToString());    

      this.Height = mainForm.ClientSize.Height / 3;
      this.Dock = DockStyle.Bottom;
      this.ControlBox = false;
      this.Text = "";
    }

    private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
    {
      this.Dock = DockStyle.None;
      this.ControlBox = true;
      this.Text = TEXT;

      this.Top = int.Parse(RegistryValue.Read(this.Name, "Top"));
      this.Left = int.Parse(RegistryValue.Read(this.Name, "Left"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height"));
      this.Width = int.Parse(RegistryValue.Read(this.Name, "Width"));
    }

    private void ExitMenuItem_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();
			excel.ExportGridToExcel(ref ContractBlotterGrid);

			this.Cursor = Cursors.Default;
		}
  }
}
