using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;
using Anetics.Medalist;

namespace Anetics.Medalist
{
	public class InventoryDeskInputForm : System.Windows.Forms.Form
	{
		private MainForm mainForm;
	    
		private DataSet dataSet, contractDataSet;
		private DataView bookView;		
		private bool processList = false;
		private string bookList = "";

		private C1.Win.C1Input.C1Label DeskDisplayLabel;

        private C1.Win.C1Input.C1Label BookGroupLabel;

		private C1.Win.C1Input.C1Label BookContactLabel;
		private C1.Win.C1Input.C1TextBox BookContactTextBox;
	    
		private C1.Win.C1Input.C1Label BookLabel;
		private C1.Win.C1List.C1Combo BookCombo;

        private C1.Win.C1Input.C1Label DeskLabel;
	    
		private C1.Win.C1Input.C1Label RateLabel;
		private C1.Win.C1Input.C1NumericEdit RateEdit;
	    
		private System.Windows.Forms.RadioButton OffersRadio;
		private System.Windows.Forms.RadioButton NeedsRadio;
	    
		private System.Windows.Forms.Button ParseListButton;
	    
		private System.Windows.Forms.Button AddInventoryButton;
		private System.Windows.Forms.Button MakeDealsButton;
		private System.Windows.Forms.Button BookContractsButton;

		private System.Windows.Forms.Button BookGroupLookupButton;
	    
		private C1.Win.C1Input.C1Label ListLabel;
		private C1.Win.C1Input.C1TextBox ListTextBox;
	    
		private System.Windows.Forms.Splitter GridSplitter;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid InputGrid;
	    
		private C1.Win.C1Input.C1Label StatusLabel;
		private C1.Win.C1Input.C1Label StatusMessageLabel;

		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
	    
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem SendToTrashBinMenuItem;
	    
		private System.Windows.Forms.MenuItem ExitMenuItem;
	    
		private string marginCode = "%";
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem CheckAllMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem ClearAllMenuItem;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Timer SubstitutionTimer;

		private double hairCut = 0;
        private System.Windows.Forms.Button SubstitutionButton;
        private C1.Win.C1List.C1Combo BookGroupCombo;
        private C1.Win.C1List.C1Combo DeskCombo;
		private System.Windows.Forms.Button WhatIfButton;		
		
		public InventoryDeskInputForm(MainForm mainForm)
		{
			this.mainForm = mainForm;

			InitializeComponent();
		}	

		protected override void Dispose(bool disposing)
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

		
		private long BookContractQuantityLookUp(string secId, string contractType)
		{	
			long quantity = 0;

			try
			{
				foreach( DataRow dr in contractDataSet.Tables["Deals"].Rows)
				{
					if (dr["DealStatus"].ToString().Equals("C"))
					{						
						if ((bookList.IndexOf(dr["Book"].ToString()) > -1) && (dr["DealType"].ToString().Equals(contractType)) && dr["SecId"].ToString().Equals(secId))
						{
							quantity += (long) dr["Quantity"];
						}
					}
				}
			}
			catch (Exception error)
			{			
				mainForm.Alert(error.Message, PilotState.RunFault);
			}

			return quantity;
		}
		
		private long BoxLookup(string secId)
		{
			long quantity = 0;			
			DataSet ds = new DataSet();

			try
			{
				ds = mainForm.ServiceAgent.SecMasterLookup(secId, true);

				hairCut = double.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryHairCut", "10"));
				hairCut = hairCut / 100;

				if (ds.Tables["BoxPosition"].Rows.Count >= 1)
				{
                    quantity = (long)ds.Tables["BoxPosition"].Rows[0]["ExDeficitSettled"]
                        + (long)ds.Tables["BoxPosition"].Rows[0]["CustomerPledgeSettled"]
                        + (long)ds.Tables["BoxPosition"].Rows[0]["FirmPledgeSettled"]
                        + (long)ds.Tables["BoxPosition"].Rows[0]["Lockup"]
                        - (long)ds.Tables["BoxPosition"].Rows[0]["DvpFailOutSettled"]
                        - (long)ds.Tables["BoxPosition"].Rows[0]["BrokerFailOutSettled"]
                        - (long)ds.Tables["BoxPosition"].Rows[0]["ClearingFailOutSettled"]
                        - (long)ds.Tables["BoxPosition"].Rows[0]["OtherFailOutSettled"]
                        - (long)ds.Tables["BoxPosition"].Rows[0]["TradeSells"];
				}
			}
			catch (Exception error)
			{
			 StatusMessageLabel.Text = error.Message;
			}	
	
			return quantity;
		}
		
		private void BoxLookup(int rowIndex)
		{
			if (InputGrid.Splits[0,0].Rows.Count > rowIndex)
			{
				StatusMessageLabel.Text = "";

				InputGrid.Row = rowIndex;
				
				string  secId = InputGrid.Columns["SecId"].Text;


				long    quantity = 0;  
				long		rockQuantity = 0;	
				long		loanedQuantity = 0;
				double  price = 0.0;
				bool    isBond = false;
				int     i;
			  

				StatusMessageLabel.Text = "Checking: " + secId;
				StatusMessageLabel.Refresh();

				try
				{
					hairCut = double.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryHairCut", "10"));
					hairCut = hairCut / 100;
				}
				catch 
				{
					hairCut = 0;
				}
				
				try
				{					
					DataSet dataSet = mainForm.ServiceAgent.SecMasterLookup(secId, true);

                    if (dataSet.Tables["SecMasterItem"].Rows.Count.Equals(1)) // We have sec master data.
                    {
                        isBond = dataSet.Tables["SecMasterItem"].Rows[0]["BaseType"].ToString().Equals("B");
                        InputGrid.Columns["IsBond"].Value = isBond;

                        if (dataSet.Tables["SecMasterItem"].Rows[0]["LastPrice"] != DBNull.Value)
                        {
                            price = (double)dataSet.Tables["SecMasterItem"].Rows[0]["LastPrice"];
                        }

                        if (dataSet.Tables["BoxPosition"].Rows.Count > 0) // We have box position data.
                        {
                            for (i = 0; i < dataSet.Tables["BoxPosition"].Rows.Count; i++) // Check for book group match.
                            {
                                if (BookGroupCombo.Text.Equals(dataSet.Tables["BoxPosition"].Rows[i]["BookGroup"].ToString()))
                                {
                                    break;
                                }
                            }

                            if (i < dataSet.Tables["BoxPosition"].Rows.Count) // We had a match.
                            {
                                if (mainForm.PositionAgent.BlockedSecId(secId) && NeedsRadio.Checked)
                                {
                                    quantity = 0;
                                    InputGrid.Columns["Flag"].Text = "H";
                                }
                                else if (bool.Parse(dataSet.Tables["SecMasterItem"].Rows[0]["IsNoLend"].ToString()))
                                {
                                    quantity = 0;
                                    InputGrid.Columns["Flag"].Text = "N";
                                }
                                else
                                {
                                    InputGrid.Columns["Flag"].Text = "P";
                                    quantity = (long)dataSet.Tables["BoxPosition"].Rows[i]["ExDeficitSettled"]
                                        + (long)dataSet.Tables["BoxPosition"].Rows[i]["CustomerPledgeSettled"]
                                        + (long)dataSet.Tables["BoxPosition"].Rows[i]["FirmPledgeSettled"]
                                        + (long)dataSet.Tables["BoxPosition"].Rows[i]["Lockup"]
                                        - (long)dataSet.Tables["BoxPosition"].Rows[i]["DvpFailOutSettled"]
                                        - (long)dataSet.Tables["BoxPosition"].Rows[i]["BrokerFailOutSettled"]
                                        - (long)dataSet.Tables["BoxPosition"].Rows[i]["ClearingFailOutSettled"]
                                        - (long)dataSet.Tables["BoxPosition"].Rows[i]["OtherFailOutSettled"]
                                         - (long)dataSet.Tables["BoxPosition"].Rows[i]["TradeSells"];

                                    rockQuantity = (long)dataSet.Tables["BoxPosition"].Rows[i]["RockAvailAble"];

                                    if (BookCombo.Text.Equals(""))
                                    {
                                        quantity -= rockQuantity;
                                        rockQuantity = 0;
                                    }
                                    else if ((bookList.IndexOf(BookCombo.Text) > -1))
                                    {
                                        loanedQuantity = BookContractQuantityLookUp(dataSet.Tables["SecMasterItem"].Rows[i]["SecId"].ToString(), "L");
                                        rockQuantity = rockQuantity - loanedQuantity;

                                        if (rockQuantity <= 0)
                                        {
                                            //quantity += rockQuantity;
                                            quantity -= (long)dataSet.Tables["BoxPosition"].Rows[i]["RockAvailAble"];
                                            rockQuantity = 0;
                                        }
                                        else if (rockQuantity <= quantity)
                                        {
                                            quantity -= rockQuantity;
                                            rockQuantity -= (rockQuantity % 100);
                                        }
                                        else
                                        {
                                            quantity = 0;
                                        }
                                    }
                                    else
                                    {
                                        quantity -= rockQuantity;
                                        rockQuantity = 0;
                                    }


                                    if (rockQuantity < 0)
                                    {
                                        rockQuantity = 0;
                                    }

                                    quantity = quantity - (long)(quantity * hairCut);

                                    if (!isBond && (quantity > 100) && NeedsRadio.Checked)
                                    {
                                        quantity -= (quantity % 100);
                                    }
                                    else if (!isBond && (quantity < -100) && OffersRadio.Checked)
                                    {
                                        if ((quantity % 100) != 0)
                                        {
                                            quantity -= ((quantity % 100) + 100);
                                        }
                                    }
                                    else if (!isBond)
                                    {
                                        quantity = 0;
                                    }

                                    if (isBond && (quantity > 100000) && NeedsRadio.Checked)
                                    {
                                        quantity -= (quantity % 100000);
                                    }
                                    else if (isBond && (quantity < -100000) && OffersRadio.Checked)
                                    {
                                        if ((quantity % 100000) != 0)
                                        {
                                            quantity -= ((quantity % 100000) + 100000);
                                        }
                                    }
                                    else if (isBond)
                                    {
                                        quantity = 0;
                                    }
                                }
                            }
                        }                                            
                    }                    
                }
				catch (Exception e)
				{
					StatusMessageLabel.Text = e.Message;
				}				
				

				InputGrid.Columns["Price"].Value = price;
				InputGrid.Columns["IsChecked"].Value = 0;
				InputGrid.Columns["MyQuantity"].Value = quantity;							
				InputGrid.Columns["RockQuantity"].Value = rockQuantity;							

				BookTableRateSet();
				BookMarginSet();
				BookPriceSet(false);

				this.dataSet.Tables["Input"].AcceptChanges();
				InputGrid.DataChanged = true;

				StatusMessageLabel.Text = "";
				Application.DoEvents();
			}
			else
			{
				StatusMessageLabel.Text = "Row index " + rowIndex + " is outside the range of accepted values.";
			}
		}

		private void BookMarginSet()
		{
			try
			{
				DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
		{
			BookGroupCombo.Text, 
			BookCombo.Text
		});
      
				if (bookRecord != null)
				{
					string s1 =  bookRecord["MarginLoan"].ToString();
					string s2 =  bookRecord["MarginBorrow"].ToString();
          
					InputGrid.Columns["Margin"].Value = (NeedsRadio.Checked ? bookRecord["MarginLoan"] : bookRecord["MarginBorrow"]);      
					marginCode = "%";
				}
				else
				{
					InputGrid.Columns["Margin"].Value = DBNull.Value;      
					marginCode = "%";
				}
			}
			catch (Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}
		}
    
		private void BookTableRateSet()
		{
			try
			{
        
				if (!InputGrid.Columns["Rate"].Value.ToString().Equals(""))
				{
					return;
				}
				
				DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
		{
			BookGroupCombo.Text, 
			BookCombo.Text
		});

				if (bookRecord == null)
				{
					return;
				}
      
				if (mainForm.IsBond)
				{
					if (OffersRadio.Checked && !bookRecord["RateBondLoan"].Equals(DBNull.Value))
					{
						InputGrid.Columns["Rate"].Value = bookRecord["RateBondLoan"];
						InputGrid.Columns["RateCode"].Value = "T";
					}
					else if (NeedsRadio.Checked && !bookRecord["RateBondBorrow"].Equals(DBNull.Value))
					{
						InputGrid.Columns["Rate"].Value = bookRecord["RateBondBorrow"];
						InputGrid.Columns["RateCode"].Value = "T";
					}
					else
					{
						InputGrid.Columns["Rate"].Text = "";
						InputGrid.Columns["RateCode"].Text = " ";
					}
				}
				else
				{
					if (OffersRadio.Checked && !bookRecord["RateStockLoan"].Equals(DBNull.Value))
					{
						InputGrid.Columns["Rate"].Value = bookRecord["RateStockLoan"];
						InputGrid.Columns["RateCode"].Value = "T";
					}
					else if (NeedsRadio.Checked && !bookRecord["RateStockBorrow"].Equals(DBNull.Value))
					{
						InputGrid.Columns["Rate"].Value = bookRecord["RateStockBorrow"];
						InputGrid.Columns["RateCode"].Value = "T";
					}
					else
					{
						InputGrid.Columns["Rate"].Text = "";
						InputGrid.Columns["RateCode"].Text = " ";
					}
				}
			}
			catch (Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}
		}

		public void BookPriceSet(bool useSecmaster)
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
				if (InputGrid.Columns["Price"].Text.Equals("") || useSecmaster)
				{
					mainForm.SecId =InputGrid.Columns["SecId"].Text;

					price = decimal.Parse(mainForm.Price);
				}
				else
				{
					price = decimal.Parse(InputGrid.Columns["Price"].Value.ToString());
				}

				if (InputGrid.Columns["Quantity"].Text.Equals(""))
				{
					quantity = 100;
				}
				else
				{
					quantity = long.Parse(InputGrid.Columns["Quantity"].Value.ToString());
				}

				if (InputGrid.Columns["Margin"].Text.Trim().Equals(""))
				{
					margin = 1.00M;
				}
				else
				{
					margin = decimal.Parse(InputGrid.Columns["Margin"].Text);
				}

				DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
		{
			BookGroupCombo.Text, 
			BookCombo.Text
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
      
				if ((bool)InputGrid.Columns["IsBond"].Value)
				{
					price = price / 100;
				}
				
				amount =  decimal.Round(price * quantity, 2);

				if (amount < amountMinimum)
				{
					price = amountMinimum / quantity;
				}

				InputGrid.Columns["Price"].Text = price.ToString("0.00");
				
				if (!InputGrid.Columns["Quantity"].Text.Equals(""))
				{
					InputGrid.Columns["Amount"].Text = amount.ToString("#,##0");
				}

				if (!InputGrid.Columns["Margin"].Text.Trim().Equals("") && (bookRecord != null)) // Price was based on counterparty profile.
				{
					InputGrid.Splits[0,0].DisplayColumns["Price"].Style.ForeColor = Color.Navy;
				}
				else
				{
					InputGrid.Splits[0,0].DisplayColumns["Price"].Style.ForeColor = Color.DimGray;
				}
			}
			catch (Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryDeskInputForm));
            this.ListLabel = new C1.Win.C1Input.C1Label();
            this.StatusLabel = new C1.Win.C1Input.C1Label();
            this.DeskDisplayLabel = new C1.Win.C1Input.C1Label();
            this.DeskLabel = new C1.Win.C1Input.C1Label();
            this.BookLabel = new C1.Win.C1Input.C1Label();
            this.BookCombo = new C1.Win.C1List.C1Combo();
            this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
            this.NeedsRadio = new System.Windows.Forms.RadioButton();
            this.BookContactLabel = new C1.Win.C1Input.C1Label();
            this.BookContactTextBox = new C1.Win.C1Input.C1TextBox();
            this.BookGroupLabel = new C1.Win.C1Input.C1Label();
            this.OffersRadio = new System.Windows.Forms.RadioButton();
            this.InputGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.GridSplitter = new System.Windows.Forms.Splitter();
            this.ListTextBox = new C1.Win.C1Input.C1TextBox();
            this.ParseListButton = new System.Windows.Forms.Button();
            this.BookGroupLookupButton = new System.Windows.Forms.Button();
            this.BookContractsButton = new System.Windows.Forms.Button();
            this.AddInventoryButton = new System.Windows.Forms.Button();
            this.MakeDealsButton = new System.Windows.Forms.Button();
            this.RateEdit = new C1.Win.C1Input.C1NumericEdit();
            this.RateLabel = new C1.Win.C1Input.C1Label();
            this.MainContextMenu = new System.Windows.Forms.ContextMenu();
            this.CheckAllMenuItem = new System.Windows.Forms.MenuItem();
            this.ClearAllMenuItem = new System.Windows.Forms.MenuItem();
            this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.SendToMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToTrashBinMenuItem = new System.Windows.Forms.MenuItem();
            this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.WhatIfButton = new System.Windows.Forms.Button();
            this.SubstitutionTimer = new System.Windows.Forms.Timer(this.components);
            this.SubstitutionButton = new System.Windows.Forms.Button();
            this.BookGroupCombo = new C1.Win.C1List.C1Combo();
            this.DeskCombo = new C1.Win.C1List.C1Combo();
            ((System.ComponentModel.ISupportInitialize)(this.ListLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskDisplayLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookContactLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookContactTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskCombo)).BeginInit();
            this.SuspendLayout();
            // 
            // ListLabel
            // 
            this.ListLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListLabel.Location = new System.Drawing.Point(12, 36);
            this.ListLabel.Name = "ListLabel";
            this.ListLabel.Size = new System.Drawing.Size(92, 16);
            this.ListLabel.TabIndex = 2;
            this.ListLabel.Tag = null;
            this.ListLabel.Text = "List Items:";
            this.ListLabel.TextDetached = true;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusLabel.Location = new System.Drawing.Point(8, 552);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(48, 16);
            this.StatusLabel.TabIndex = 8;
            this.StatusLabel.Tag = null;
            this.StatusLabel.Text = "Status:";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.StatusLabel.TextDetached = true;
            // 
            // DeskDisplayLabel
            // 
            this.DeskDisplayLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeskDisplayLabel.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeskDisplayLabel.ForeColor = System.Drawing.Color.Navy;
            this.DeskDisplayLabel.Location = new System.Drawing.Point(12, 8);
            this.DeskDisplayLabel.Name = "DeskDisplayLabel";
            this.DeskDisplayLabel.Size = new System.Drawing.Size(584, 16);
            this.DeskDisplayLabel.TabIndex = 1;
            this.DeskDisplayLabel.Tag = null;
            this.DeskDisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DeskLabel
            // 
            this.DeskLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeskLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeskLabel.Location = new System.Drawing.Point(832, 168);
            this.DeskLabel.Name = "DeskLabel";
            this.DeskLabel.Size = new System.Drawing.Size(92, 16);
            this.DeskLabel.TabIndex = 6;
            this.DeskLabel.Tag = null;
            this.DeskLabel.Text = "Desk:";
            this.DeskLabel.TextDetached = true;
            // 
            // BookLabel
            // 
            this.BookLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BookLabel.Location = new System.Drawing.Point(832, 124);
            this.BookLabel.Name = "BookLabel";
            this.BookLabel.Size = new System.Drawing.Size(92, 16);
            this.BookLabel.TabIndex = 5;
            this.BookLabel.Tag = null;
            this.BookLabel.Text = "Book:";
            this.BookLabel.TextDetached = true;
            this.BookLabel.Value = "";
            // 
            // BookCombo
            // 
            this.BookCombo.AddItemSeparator = ';';
            this.BookCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookCombo.AutoCompletion = true;
            this.BookCombo.AutoDropDown = true;
            this.BookCombo.AutoSize = false;
            this.BookCombo.Caption = "";
            this.BookCombo.CaptionHeight = 17;
            this.BookCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.BookCombo.ColumnCaptionHeight = 17;
            this.BookCombo.ColumnFooterHeight = 17;
            this.BookCombo.ContentHeight = 14;
            this.BookCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.BookCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.BookCombo.DropDownWidth = 375;
            this.BookCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.BookCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.BookCombo.EditorHeight = 14;
            this.BookCombo.ExtendRightColumn = true;
            this.BookCombo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookCombo.Images"))));
            this.BookCombo.ItemHeight = 15;
            this.BookCombo.Location = new System.Drawing.Point(832, 140);
            this.BookCombo.MatchEntryTimeout = ((long)(2000));
            this.BookCombo.MaxDropDownItems = ((short)(16));
            this.BookCombo.MaxLength = 32767;
            this.BookCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.BookCombo.Name = "BookCombo";
            this.BookCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.BookCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookCombo.Size = new System.Drawing.Size(132, 20);
            this.BookCombo.TabIndex = 103;
            this.BookCombo.RowChange += new System.EventHandler(this.BookCombo_RowChange);
            this.BookCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BookCombo_KeyPress);
            this.BookCombo.PropBag = resources.GetString("BookCombo.PropBag");
            // 
            // StatusMessageLabel
            // 
            this.StatusMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusMessageLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
            this.StatusMessageLabel.Location = new System.Drawing.Point(56, 552);
            this.StatusMessageLabel.Name = "StatusMessageLabel";
            this.StatusMessageLabel.Size = new System.Drawing.Size(548, 28);
            this.StatusMessageLabel.TabIndex = 9;
            this.StatusMessageLabel.Tag = null;
            this.StatusMessageLabel.TextDetached = true;
            // 
            // NeedsRadio
            // 
            this.NeedsRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NeedsRadio.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NeedsRadio.ForeColor = System.Drawing.Color.Black;
            this.NeedsRadio.Location = new System.Drawing.Point(836, 292);
            this.NeedsRadio.Name = "NeedsRadio";
            this.NeedsRadio.Size = new System.Drawing.Size(128, 24);
            this.NeedsRadio.TabIndex = 108;
            this.NeedsRadio.Text = "&Needs (I Lend)";
            this.NeedsRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // BookContactLabel
            // 
            this.BookContactLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookContactLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BookContactLabel.Location = new System.Drawing.Point(832, 80);
            this.BookContactLabel.Name = "BookContactLabel";
            this.BookContactLabel.Size = new System.Drawing.Size(92, 16);
            this.BookContactLabel.TabIndex = 4;
            this.BookContactLabel.Tag = null;
            this.BookContactLabel.Text = "Book Contact:";
            this.BookContactLabel.TextDetached = true;
            // 
            // BookContactTextBox
            // 
            this.BookContactTextBox.AcceptsTab = true;
            this.BookContactTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookContactTextBox.AutoSize = false;
            this.BookContactTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.BookContactTextBox.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookContactTextBox.Location = new System.Drawing.Point(832, 96);
            this.BookContactTextBox.MaxLength = 15;
            this.BookContactTextBox.Name = "BookContactTextBox";
            this.BookContactTextBox.NumericInput = false;
            this.BookContactTextBox.NumericInputKeys = C1.Win.C1Input.NumericInputKeyFlags.None;
            this.BookContactTextBox.Size = new System.Drawing.Size(132, 20);
            this.BookContactTextBox.TabIndex = 102;
            this.BookContactTextBox.Tag = null;
            this.BookContactTextBox.TextDetached = true;
            this.BookContactTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BookContactTextBox_KeyPress);
            // 
            // BookGroupLabel
            // 
            this.BookGroupLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookGroupLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BookGroupLabel.Location = new System.Drawing.Point(832, 36);
            this.BookGroupLabel.Name = "BookGroupLabel";
            this.BookGroupLabel.Size = new System.Drawing.Size(92, 16);
            this.BookGroupLabel.TabIndex = 3;
            this.BookGroupLabel.Tag = null;
            this.BookGroupLabel.Text = "Book Group:";
            this.BookGroupLabel.TextDetached = true;
            // 
            // OffersRadio
            // 
            this.OffersRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OffersRadio.ForeColor = System.Drawing.Color.Black;
            this.OffersRadio.Location = new System.Drawing.Point(836, 264);
            this.OffersRadio.Name = "OffersRadio";
            this.OffersRadio.Size = new System.Drawing.Size(128, 24);
            this.OffersRadio.TabIndex = 107;
            this.OffersRadio.Text = "&Offers (I Borrow)";
            this.OffersRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // InputGrid
            // 
            this.InputGrid.AllowColSelect = false;
            this.InputGrid.AllowDelete = true;
            this.InputGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.InputGrid.CaptionHeight = 18;
            this.InputGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InputGrid.EmptyRows = true;
            this.InputGrid.ExtendRightColumn = true;
            this.InputGrid.FetchRowStyles = true;
            this.InputGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.InputGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InputGrid.Images"))));
            this.InputGrid.Location = new System.Drawing.Point(8, 306);
            this.InputGrid.Name = "InputGrid";
            this.InputGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.InputGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.InputGrid.PreviewInfo.ZoomFactor = 75D;
            this.InputGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("InputGrid.PrintInfo.PageSettings")));
            this.InputGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.InputGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.InputGrid.RowHeight = 15;
            this.InputGrid.Size = new System.Drawing.Size(812, 228);
            this.InputGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.InputGrid.TabIndex = 114;
            this.InputGrid.Text = "InputGrid";
            this.InputGrid.WrapCellPointer = true;
            this.InputGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.InputGrid_BeforeColUpdate);
            this.InputGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.InputGrid_RowColChange);
            this.InputGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.InputGrid_BeforeColEdit);
            this.InputGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.InputGrid_FormatText);
            this.InputGrid.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.InputGrid_FetchCellStyle);
            this.InputGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.InputGrid_FetchRowStyle);
            this.InputGrid.PropBag = resources.GetString("InputGrid.PropBag");
            // 
            // GridSplitter
            // 
            this.GridSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GridSplitter.Location = new System.Drawing.Point(8, 302);
            this.GridSplitter.MinExtra = 132;
            this.GridSplitter.MinSize = 52;
            this.GridSplitter.Name = "GridSplitter";
            this.GridSplitter.Size = new System.Drawing.Size(812, 4);
            this.GridSplitter.TabIndex = 115;
            this.GridSplitter.TabStop = false;
            // 
            // ListTextBox
            // 
            this.ListTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ListTextBox.DateTimeInput = false;
            this.ListTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListTextBox.ForeColor = System.Drawing.Color.Navy;
            this.ListTextBox.Location = new System.Drawing.Point(8, 52);
            this.ListTextBox.MaxLength = 250000;
            this.ListTextBox.Multiline = true;
            this.ListTextBox.Name = "ListTextBox";
            this.ListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ListTextBox.Size = new System.Drawing.Size(812, 250);
            this.ListTextBox.TabIndex = 106;
            this.ListTextBox.Tag = null;
            this.ListTextBox.TextDetached = true;
            this.ListTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListTextBox_KeyPress);
            // 
            // ParseListButton
            // 
            this.ParseListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ParseListButton.Location = new System.Drawing.Point(832, 328);
            this.ParseListButton.Name = "ParseListButton";
            this.ParseListButton.Size = new System.Drawing.Size(132, 24);
            this.ParseListButton.TabIndex = 109;
            this.ParseListButton.Text = "Parse List";
            this.ParseListButton.Click += new System.EventHandler(this.ParseListButton_Click);
            // 
            // BookGroupLookupButton
            // 
            this.BookGroupLookupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookGroupLookupButton.Location = new System.Drawing.Point(832, 456);
            this.BookGroupLookupButton.Name = "BookGroupLookupButton";
            this.BookGroupLookupButton.Size = new System.Drawing.Size(132, 24);
            this.BookGroupLookupButton.TabIndex = 113;
            this.BookGroupLookupButton.Text = "Book Group Lookup";
            this.BookGroupLookupButton.Click += new System.EventHandler(this.BookGroupLookupButton_Click);
            // 
            // BookContractsButton
            // 
            this.BookContractsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookContractsButton.Enabled = false;
            this.BookContractsButton.Location = new System.Drawing.Point(832, 424);
            this.BookContractsButton.Name = "BookContractsButton";
            this.BookContractsButton.Size = new System.Drawing.Size(132, 24);
            this.BookContractsButton.TabIndex = 112;
            this.BookContractsButton.Text = "Book Contracts";
            this.BookContractsButton.Click += new System.EventHandler(this.BookContractsButton_Click);
            // 
            // AddInventoryButton
            // 
            this.AddInventoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddInventoryButton.Enabled = false;
            this.AddInventoryButton.Location = new System.Drawing.Point(832, 360);
            this.AddInventoryButton.Name = "AddInventoryButton";
            this.AddInventoryButton.Size = new System.Drawing.Size(132, 24);
            this.AddInventoryButton.TabIndex = 110;
            this.AddInventoryButton.Text = "Add Inventory";
            this.AddInventoryButton.Click += new System.EventHandler(this.AddInventoryButton_Click);
            // 
            // MakeDealsButton
            // 
            this.MakeDealsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MakeDealsButton.Enabled = false;
            this.MakeDealsButton.Location = new System.Drawing.Point(832, 392);
            this.MakeDealsButton.Name = "MakeDealsButton";
            this.MakeDealsButton.Size = new System.Drawing.Size(132, 24);
            this.MakeDealsButton.TabIndex = 111;
            this.MakeDealsButton.Text = "Make Deals";
            this.MakeDealsButton.Click += new System.EventHandler(this.MakeDealsButton_Click);
            // 
            // RateEdit
            // 
            this.RateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RateEdit.AutoSize = false;
            this.RateEdit.DisplayFormat.CustomFormat = "0.000";
            this.RateEdit.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.RateEdit.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
                        | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.RateEdit.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RateEdit.ForeColor = System.Drawing.Color.DimGray;
            this.RateEdit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.RateEdit.Label = this.RateLabel;
            this.RateEdit.Location = new System.Drawing.Point(832, 228);
            this.RateEdit.Name = "RateEdit";
            this.RateEdit.Size = new System.Drawing.Size(132, 22);
            this.RateEdit.TabIndex = 105;
            this.RateEdit.Tag = null;
            this.RateEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RateEdit.TrimStart = true;
            this.RateEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            this.RateEdit.ValueChanged += new System.EventHandler(this.RateEdit_ValueChanged);
            this.RateEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RateEdit_KeyPress);
            // 
            // RateLabel
            // 
            this.RateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RateLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RateLabel.Location = new System.Drawing.Point(832, 212);
            this.RateLabel.Name = "RateLabel";
            this.RateLabel.Size = new System.Drawing.Size(92, 16);
            this.RateLabel.TabIndex = 7;
            this.RateLabel.Tag = null;
            this.RateLabel.Text = "Rate:";
            this.RateLabel.TextDetached = true;
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CheckAllMenuItem,
            this.ClearAllMenuItem,
            this.Sep1MenuItem,
            this.SendToMenuItem,
            this.Sep2MenuItem,
            this.ExitMenuItem});
            // 
            // CheckAllMenuItem
            // 
            this.CheckAllMenuItem.Index = 0;
            this.CheckAllMenuItem.Text = "Check All";
            this.CheckAllMenuItem.Click += new System.EventHandler(this.CheckAllMenuItem_Click);
            // 
            // ClearAllMenuItem
            // 
            this.ClearAllMenuItem.Index = 1;
            this.ClearAllMenuItem.Text = "Clear All";
            this.ClearAllMenuItem.Click += new System.EventHandler(this.ClearAllMenuItem_Click);
            // 
            // Sep1MenuItem
            // 
            this.Sep1MenuItem.Index = 2;
            this.Sep1MenuItem.Text = "-";
            // 
            // SendToMenuItem
            // 
            this.SendToMenuItem.Index = 3;
            this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SendToClipboardMenuItem,
            this.SendToExcelMenuItem,
            this.SendToEmailMenuItem,
            this.SendToTrashBinMenuItem});
            this.SendToMenuItem.Text = "Send To";
            // 
            // SendToClipboardMenuItem
            // 
            this.SendToClipboardMenuItem.Index = 0;
            this.SendToClipboardMenuItem.Text = "Clipboard";
            this.SendToClipboardMenuItem.Click += new System.EventHandler(this.SendToClipboardMenuItem_Click);
            // 
            // SendToExcelMenuItem
            // 
            this.SendToExcelMenuItem.Index = 1;
            this.SendToExcelMenuItem.Text = "Excel";
            this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
            // 
            // SendToEmailMenuItem
            // 
            this.SendToEmailMenuItem.Index = 2;
            this.SendToEmailMenuItem.Text = "Mail Recipient";
            this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
            // 
            // SendToTrashBinMenuItem
            // 
            this.SendToTrashBinMenuItem.Index = 3;
            this.SendToTrashBinMenuItem.Text = "Trash Bin";
            this.SendToTrashBinMenuItem.Click += new System.EventHandler(this.SendToTrashBinMenuItem_Click);
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
            // WhatIfButton
            // 
            this.WhatIfButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WhatIfButton.Enabled = false;
            this.WhatIfButton.Location = new System.Drawing.Point(832, 488);
            this.WhatIfButton.Name = "WhatIfButton";
            this.WhatIfButton.Size = new System.Drawing.Size(132, 24);
            this.WhatIfButton.TabIndex = 116;
            this.WhatIfButton.Text = "What-If Lookup";
            this.WhatIfButton.Click += new System.EventHandler(this.WhatIfButton_Click);
            // 
            // SubstitutionTimer
            // 
            this.SubstitutionTimer.Tick += new System.EventHandler(this.SubstitutionTimer_Tick);
            // 
            // SubstitutionButton
            // 
            this.SubstitutionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SubstitutionButton.Enabled = false;
            this.SubstitutionButton.Location = new System.Drawing.Point(832, 520);
            this.SubstitutionButton.Name = "SubstitutionButton";
            this.SubstitutionButton.Size = new System.Drawing.Size(132, 24);
            this.SubstitutionButton.TabIndex = 117;
            this.SubstitutionButton.Text = "Substitution Lookup";
            this.SubstitutionButton.Click += new System.EventHandler(this.SubstitutionButton_Click);
            // 
            // BookGroupCombo
            // 
            this.BookGroupCombo.AddItemSeparator = ';';
            this.BookGroupCombo.Caption = "";
            this.BookGroupCombo.CaptionHeight = 17;
            this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.BookGroupCombo.ColumnCaptionHeight = 17;
            this.BookGroupCombo.ColumnFooterHeight = 17;
            this.BookGroupCombo.ColumnWidth = 100;
            this.BookGroupCombo.ContentHeight = 16;
            this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.White;
            this.BookGroupCombo.DropDownWidth = 300;
            this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.BookGroupCombo.EditorHeight = 16;
            this.BookGroupCombo.ExtendRightColumn = true;
            this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images"))));
            this.BookGroupCombo.ItemHeight = 15;
            this.BookGroupCombo.Location = new System.Drawing.Point(832, 52);
            this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
            this.BookGroupCombo.MaxDropDownItems = ((short)(5));
            this.BookGroupCombo.MaxLength = 32767;
            this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.BookGroupCombo.Name = "BookGroupCombo";
            this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookGroupCombo.Size = new System.Drawing.Size(132, 22);
            this.BookGroupCombo.TabIndex = 118;
            this.BookGroupCombo.VisualStyle = C1.Win.C1List.VisualStyle.System;
            this.BookGroupCombo.RowChange += new System.EventHandler(this.BookGroupCombo_RowChange);
            this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
            this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
            // 
            // DeskCombo
            // 
            this.DeskCombo.AddItemSeparator = ';';
            this.DeskCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeskCombo.AutoCompletion = true;
            this.DeskCombo.AutoDropDown = true;
            this.DeskCombo.AutoSize = false;
            this.DeskCombo.Caption = "";
            this.DeskCombo.CaptionHeight = 17;
            this.DeskCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DeskCombo.ColumnCaptionHeight = 17;
            this.DeskCombo.ColumnFooterHeight = 17;
            this.DeskCombo.ContentHeight = 14;
            this.DeskCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.DeskCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.DeskCombo.DropDownWidth = 375;
            this.DeskCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.DeskCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeskCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.DeskCombo.EditorHeight = 14;
            this.DeskCombo.ExtendRightColumn = true;
            this.DeskCombo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeskCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("DeskCombo.Images"))));
            this.DeskCombo.ItemHeight = 15;
            this.DeskCombo.Location = new System.Drawing.Point(832, 187);
            this.DeskCombo.MatchEntryTimeout = ((long)(2000));
            this.DeskCombo.MaxDropDownItems = ((short)(16));
            this.DeskCombo.MaxLength = 32767;
            this.DeskCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.DeskCombo.Name = "DeskCombo";
            this.DeskCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.DeskCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.DeskCombo.Size = new System.Drawing.Size(132, 20);
            this.DeskCombo.TabIndex = 119;
            this.DeskCombo.TextChanged += new System.EventHandler(this.DeskCombo_TextChanged);
            this.DeskCombo.PropBag = resources.GetString("DeskCombo.PropBag");
            // 
            // InventoryDeskInputForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(976, 574);
            this.ContextMenu = this.MainContextMenu;
            this.Controls.Add(this.DeskCombo);
            this.Controls.Add(this.BookGroupCombo);
            this.Controls.Add(this.SubstitutionButton);
            this.Controls.Add(this.WhatIfButton);
            this.Controls.Add(this.RateLabel);
            this.Controls.Add(this.RateEdit);
            this.Controls.Add(this.MakeDealsButton);
            this.Controls.Add(this.AddInventoryButton);
            this.Controls.Add(this.BookContractsButton);
            this.Controls.Add(this.BookGroupLookupButton);
            this.Controls.Add(this.ParseListButton);
            this.Controls.Add(this.ListTextBox);
            this.Controls.Add(this.GridSplitter);
            this.Controls.Add(this.InputGrid);
            this.Controls.Add(this.OffersRadio);
            this.Controls.Add(this.BookGroupLabel);
            this.Controls.Add(this.BookContactTextBox);
            this.Controls.Add(this.BookContactLabel);
            this.Controls.Add(this.NeedsRadio);
            this.Controls.Add(this.StatusMessageLabel);
            this.Controls.Add(this.BookLabel);
            this.Controls.Add(this.BookCombo);
            this.Controls.Add(this.DeskLabel);
            this.Controls.Add(this.DeskDisplayLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.ListLabel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1020, 612);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(820, 612);
            this.Name = "InventoryDeskInputForm";
            this.Padding = new System.Windows.Forms.Padding(8, 52, 156, 40);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory - Desk Input";
            this.TopMost = true;
            this.Closed += new System.EventHandler(this.InventoryDeskInputForm_Closed);
            this.Load += new System.EventHandler(this.InventoryDeskInputForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.InventoryDeskInputForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.ListLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskDisplayLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookContactLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookContactTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskCombo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
    
		private void InventoryDeskInputForm_Load(object sender, System.EventArgs e)
		{
			dataSet = null;
			contractDataSet = null;

			this.Show();
			Application.DoEvents();

			try
			{
				dataSet = mainForm.ServiceAgent.InventoryDeskInputDataGet();
				contractDataSet = mainForm.PositionAgent.DealDataGet(mainForm.UtcOffset, mainForm.UserId, "PositonContractBlotter", true.ToString());
		
				bookList =	mainForm.ServiceAgent.KeyValueGet("RockBooks", "0064;7369;");		
				dataSet.Tables.Add("Input");

				dataSet.Tables["Input"].Columns.Add("ProcessId", typeof(string));
				dataSet.Tables["Input"].Columns.Add("IsChecked", typeof(bool));
				dataSet.Tables["Input"].Columns.Add("IsBond", typeof(bool));
				dataSet.Tables["Input"].Columns.Add("SecId", typeof(string));
				dataSet.Tables["Input"].Columns.Add("MyQuantity", typeof(long));        
				dataSet.Tables["Input"].Columns.Add("PsrQuantity", typeof(long));        
				dataSet.Tables["Input"].Columns.Add("RockQuantity", typeof(long));        
				dataSet.Tables["Input"].Columns.Add("Quantity", typeof(long));
				dataSet.Tables["Input"].Columns.Add("Price", typeof(decimal));
				dataSet.Tables["Input"].Columns.Add("Amount", typeof(decimal));
				dataSet.Tables["Input"].Columns.Add("Rate", typeof(decimal));
				dataSet.Tables["Input"].Columns.Add("RateCode", typeof(string));
				dataSet.Tables["Input"].Columns.Add("InventoryRate", typeof(decimal));
				dataSet.Tables["Input"].Columns.Add("Margin", typeof(decimal));
				dataSet.Tables["Input"].Columns.Add("PoolCode", typeof(string));
				dataSet.Tables["Input"].Columns.Add("Flag", typeof(string));
				dataSet.Tables["Input"].Columns.Add("SubstitutionProcessId", typeof(string));
				dataSet.Tables["Input"].Columns.Add("SubstitutionProcessIdFlag", typeof(string));
						
				InputGrid.SetDataBinding(dataSet, "Input", true);
        
				bookView = new DataView(dataSet.Tables["Books"]);
        
				DeskCombo.HoldFields();
				DeskCombo.DataSource = dataSet;
				DeskCombo.DataMember = "Desks";

                BookGroupCombo.HoldFields();
                BookGroupCombo.DataSource = dataSet.Tables["BookGroups"].DefaultView;				

				BookCombo.HoldFields();
				BookCombo.DataSource = bookView;

				BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", "");                        

				NeedsRadio.Checked = true;

				Application.DoEvents();
				BookContactTextBox.Focus();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + "[InventoryDeskInputform.InventoryDeskInputForm_Load]", Log.Error, 1); 
			}
		}

		private void InventoryDeskInputForm_Closed(object sender, System.EventArgs e)
		{
			RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);
		}

		private void InventoryDeskInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Pen pen = new Pen(Color.DimGray, 1.8F);
      
			float x1 = 12.0F;
			float x2 = this.ClientSize.Width - 16.0F;

			float x3 = OffersRadio.Left + 16.0F;
			float x4 = OffersRadio.Left + OffersRadio.Width - 16.0F;

			float y1 = BookGroupLookupButton.Location.Y - 12.0F;
			float y2 = AddInventoryButton.Location.Y - 12.0F;
			float y3 = ListLabel.Location.Y - 8.0F;

			e.Graphics.DrawLine(pen, x3, y1, x4, y1);   
			e.Graphics.DrawLine(pen, x3, y2, x4, y2);   
			e.Graphics.DrawLine(pen, x1, y3, x2, y3);   

			e.Graphics.Dispose();
		}

		private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
		{
			bookView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";

            if (BookGroupCombo.Text.Equals("0234"))
            {
                SubstitutionButton.Enabled = true;
                WhatIfButton.Enabled = true;
            }
            else
            {
                SubstitutionButton.Enabled = false;
                WhatIfButton.Enabled = false;
            }

			ListTextBox.Text = "";
			dataSet.Tables["Input"].Rows.Clear();
		}

		private void BookGroupCombo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals('\r'))
			{
				e.Handled = true;
				BookContactTextBox.Focus();
			}    
		}

		private void BookContactTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals('\r'))
			{
				e.Handled = true;
				BookCombo.Focus();
			}
		}

		private void BookCombo_RowChange(object sender, System.EventArgs e)
		{
			if (BookCombo.Row > -1)
			{
				string desk =
					bookView[BookCombo.Row]["Firm"].ToString() + "."
					+ bookView[BookCombo.Row]["Country"].ToString() + "." 
					+ bookView[BookCombo.Row]["DeskType"].ToString();

				if (!desk.Equals(".."))
				{
					DeskCombo.Text = desk;
				}
				else
				{
					DeskCombo.Text = "";
				}
			}
			else
			{
				DeskCombo.Text = "";
			}
		}



		private void BookCombo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals('\r'))
			{
				e.Handled = true;
				DeskCombo.Focus();
			}    
		}

		private void DeskCombo_TextChanged(object sender, System.EventArgs e)
		{  
			string[] s  = null;
 
			try
			{
				s = DeskCombo.Text.Split('.');  

				DeskDisplayLabel.Value = dataSet.Tables["Firms"].Rows.Find(s[0])["Firm"].ToString() + " - "
					+ dataSet.Tables["Countries"].Rows.Find(s[1])["Country"].ToString() + " - "
					+ dataSet.Tables["DeskTypes"].Rows.Find(s[2])["DeskType"].ToString();
			}
			catch
			{
				DeskDisplayLabel.Value = "";
			}

			AddInventoryButton.Enabled = (!DeskCombo.Text.Equals("") && OffersRadio.Checked);
		}

		private void DeskCombo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals('\r'))
			{
				e.Handled = true;
				RateEdit.Focus();
			}            
		}

		private void RateEdit_ValueChanged(object sender, System.EventArgs e)
		{
			if ((decimal)RateEdit.Value > 0M)
			{
				RateEdit.ForeColor = Color.Navy;
				return;
			}
      
			if ((decimal)RateEdit.Value < 0M)
			{
				RateEdit.ForeColor = Color.Maroon;
				return;
			}

			RateEdit.ForeColor = Color.DimGray;
		}

		private void RateEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals('\r'))
			{
				e.Handled = true;
				ListTextBox.Focus();
			}        
		}

		private void Radio_CheckedChanged(object sender, System.EventArgs e)
		{
			if (OffersRadio.Checked)
			{
				OffersRadio.ForeColor = Color.Navy;
				NeedsRadio.ForeColor = Color.Black;
        				
				ListTextBox.ForeColor = Color.Navy;
        				
				InputGrid.Splits[0,0].Style.BackColor = Color.Ivory;

				AddInventoryButton.Enabled = !DeskCombo.Text.Equals("");
				MakeDealsButton.Enabled = true;
				BookContractsButton.Enabled = true;
				SubstitutionButton.Enabled = false;
				WhatIfButton.Enabled = false;
				BookContractsButton.Text = "Book Borrows";						
			}

			if (NeedsRadio.Checked)
			{
				OffersRadio.ForeColor = Color.Black;
				NeedsRadio.ForeColor = Color.Maroon;

				ListTextBox.ForeColor = Color.Maroon;
        
				InputGrid.Splits[0,0].Style.BackColor = Color.Honeydew;
        
				AddInventoryButton.Enabled = false;
				MakeDealsButton.Enabled = true;
				BookContractsButton.Enabled = true;
				SubstitutionButton.Enabled = true;
				WhatIfButton.Enabled = true;
				BookContractsButton.Text = "Book Loans";      
				
			}

			InputGrid.Refresh();
			ListTextBox.Focus();
		}

		private void ParseListButton_Click(object sender, System.EventArgs e)
		{
			string rate = "";
			
			Input input = new Input(ListTextBox.Text);

			StatusMessageLabel.Text = input.Status; 
      
			try
			{
				for (int i = 0; i < input.Count; i++)
				{
					DataRow dataRow = dataSet.Tables["Input"].NewRow();
          
					dataRow["ProcessId"] = Standard.ProcessId();
					dataRow["IsChecked"] = 0;
					dataRow["SecId"] = input.SecId(i);										

					if (Tools.IsNumeric(input.Quantity(i)))
					{
						dataRow["Quantity"] = Tools.ParseLong(input.Quantity(i));
					}
					else
					{
						dataRow["Quantity"] = DBNull.Value;
					}

					if (Tools.IsNumeric(input.Rate(i)))
					{
						dataRow["Rate"] = input.Rate(i);
					}
					else
					{
						dataRow["Rate"] = RateEdit.Value;
					}

					try
					{
						rate = mainForm.ServiceAgent.SecMasterLookup(input.SecId(i), false, false, 0, "00:00").Tables["SecMasterItem"].Rows[0]["Rate"].ToString();
					}
					catch 
					{}
     

					if (!rate.Equals(""))
					{
						dataRow["InventoryRate"] =  rate;
					}
					else
					{
						dataRow["InventoryRate"] =  DBNull.Value;
					}

					dataSet.Tables["Input"].Rows.Add(dataRow);
				}
      
				InputGrid.Row = InputGrid.FirstRow;
			}
			catch (Exception ee)
			{
				StatusMessageLabel.Text = ee.Message; 
			}
		}

		private void AddInventoryButton_Click(object sender, System.EventArgs e)
		{
			string list = "";

			StatusMessageLabel.Text = "Please wait. Submitting your list...";
      
			try
			{
				for (int i = 0; i < InputGrid.Splits[0,0].Rows.Count; i++)
				{
					if ((bool)InputGrid.Columns["IsChecked"].CellValue(i))
					{
						list += InputGrid.Columns["SecId"].CellText(i) + "\t"
							+ InputGrid.Columns["Quantity"].CellText(i) + "\t" 
							+ InputGrid.Columns["Rate"].CellText(i) + "\n";
					}
				}
      
				StatusMessageLabel.Text = mainForm.ShortSaleAgent.InventoryListSubmit(
					DeskCombo.Text,
					BookCombo.Text,
					list,
					"",
					mainForm.UserId);
			}
			catch (Exception ee)
			{
				StatusMessageLabel.Text = ee.Message;
			}
		}

		private void MakeDealsButton_Click(object sender, System.EventArgs e)
		{ 
			try
			{
				for (int i = 0; i < InputGrid.Splits[0,0].Rows.Count; i++)
				{
					if ((bool)InputGrid.Columns["IsChecked"].CellValue(i))
					{				
						StatusMessageLabel.Text = "Sending To Deal: " + InputGrid.Columns["SecId"].CellText(i) + " [" + InputGrid.Columns["Quantity"].CellText(i) + "].";
						StatusMessageLabel.Refresh();

						mainForm.PositionAgent.DealSet(
							Standard.ProcessId("D"),
							BookGroupCombo.Text,
							((OffersRadio.Checked) ? "B" : "L"),
							BookCombo.Text,
							"",
							InputGrid.Columns["SecId"].CellText(i),
							InputGrid.Columns["Quantity"].CellValue(i).ToString(),
							InputGrid.Columns["Amount"].CellValue(i).ToString(),
							"C",
							"",
							"",
							"",
							InputGrid.Columns["Rate"].CellText(i),
							"",
							"",
							"",
							"False",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							(InputGrid.Columns["Flag"].CellText(i).Equals("H") && NeedsRadio.Checked)?"H":"D",
							mainForm.UserId,
							true
							);
					}
				}
			}
			catch (Exception ee)
			{
				StatusMessageLabel.Text = ee.Message;
			}    
		}

		private void BookContractsButton_Click(object sender, System.EventArgs e)
		{
			int i = 0;

			string processId;		  

			try
			{
				while (i < InputGrid.Splits[0,0].Rows.Count)
				{
					InputGrid.Row = i;

					if ((bool)InputGrid.Columns["IsChecked"].CellValue(i))
					{						
						StatusMessageLabel.Text = "Sending: " + InputGrid.Columns["SecId"].CellText(i) + " [" + InputGrid.Columns["Quantity"].CellText(i) + "].";
						StatusMessageLabel.Refresh();

						processId = Standard.ProcessId("C");																	
						
						mainForm.PositionAgent.DealSet(
							processId,
							BookGroupCombo.Text,
							((OffersRadio.Checked) ? "B" : "L"),
							BookCombo.Text,
							"",
							InputGrid.Columns["SecId"].CellText(i),
							InputGrid.Columns["Quantity"].CellValue(i).ToString(),
							InputGrid.Columns["Amount"].CellValue(i).ToString(),
							"C",
							"",
							"",
							"",
							InputGrid.Columns["Rate"].CellText(i),
							InputGrid.Columns["RateCode"].CellText(i),
							InputGrid.Columns["PoolCode"].CellText(i),
							"",
							"False",
							"",
							InputGrid.Columns["Margin"].CellText(i),
							marginCode,
							"",
							"",
							"",
							"",
							(InputGrid.Columns["Flag"].CellText(i).Equals("H") && NeedsRadio.Checked)?"H":"D",
							mainForm.UserId,
							true
							);												
					  
						if (!BookCombo.Text.Equals("") && !InputGrid.Columns["Amount"].CellValue(i).ToString().Equals("") && !InputGrid.Columns["Rate"].CellText(i).Equals("") && !InputGrid.Columns["Margin"].CellText(i).Equals("") && !InputGrid.Columns["Flag"].CellText(i).Equals("H") && !InputGrid.Columns["Quantity"].CellText(i).Equals(""))
						{           														
							mainForm.PositionAgent.DealSet(processId, "S", mainForm.UserId, true);													
							Application.DoEvents();            																																	
						  						
						}

						InputGrid.Delete();
					}
					else
					{
						i++;
					}					
				}

				StatusMessageLabel.Text = "Done booking contracts.";
			}
			catch (Exception ee)
			{
				StatusMessageLabel.Text = ee.Message;
			}
		}
	 

		private void BookGroupLookupButton_Click(object sender, System.EventArgs e)
		{
			processList = true;
			int i = 0;

			for (i = 0; i < InputGrid.Splits[0,0].Rows.Count; i++)
			{
				BoxLookup(i);
			}
      
			StatusMessageLabel.Text = "Done!  Your list of " + i + " item[s] has been checked.";
			processList = false;
		}

		private void ListTextBox_TextChanged(object sender, System.EventArgs e)
		{
			ParseListButton.Enabled = !ListTextBox.Text.Equals("");
		}

		private void ListTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				string secId = "";
				string quantity = "";
        
				string[] items = ListTextBox.Text.Trim().Split('\n');
				string[] fields = items[items.Length - 1].Trim().Split((" \t").ToCharArray());
    
				if (fields.Length.Equals(1))
				{
					if (Security.IsCusip(fields[0]) || Security.IsSymbol(fields[0]))
					{
						secId = fields[0];
					}
				}
        
				if (fields.Length.Equals(2))
				{
					if (Security.IsCusip(fields[0]) || Security.IsSymbol(fields[0]))
					{
						secId = fields[0].Trim();

						if (Tools.IsNumeric(fields[1]))
						{
							quantity = Tools.ParseLong(fields[1]).ToString();
						}
					}
					else if (Security.IsCusip(fields[1]) || Security.IsSymbol(fields[1]))
					{
						secId = fields[1].Trim();

						if (Tools.IsNumeric(fields[0]))
						{
							quantity = Tools.ParseLong(fields[0]).ToString();
						}
					}
				}

				if (!secId.Equals(""))
				{
					string processId = Standard.ProcessId();
					string rate = "";
					int rowIndex = 0;

					this.Cursor = Cursors.WaitCursor;
					Application.DoEvents();
      
					DataRow dr = dataSet.Tables["Input"].NewRow();
					dataSet.Tables["Input"].AcceptChanges();															

					dr["ProcessId"] = processId;
					dr["IsChecked"] = DBNull.Value;
					dr["SecId"] = secId;
					dr["MyQuantity"] = DBNull.Value;
					
					if (!quantity.Equals(""))
					{
						dr["Quantity"]= quantity;
					}
					
					dr["Price"] = DBNull.Value;
					dr["Rate"] = RateEdit.Value;
					dr["Flag"] = "P";
    
					try
					{
						rate = mainForm.ServiceAgent.SecMasterLookup(secId, false, false, 0, "00:00").Tables["SecMasterItem"].Rows[0]["Rate"].ToString();
					}
					catch 
					{}

					if (!rate.Equals(""))
					{
						dr["InventoryRate"] =  rate;
					}
					else
					{
						dr["InventoryRate"] =  DBNull.Value;
					}

					dataSet.Tables["Input"].Rows.Add(dr);
					dataSet.Tables["Input"].AcceptChanges();
					InputGrid.DataChanged = true;

					for(int row = 0; row < InputGrid.Splits[0].Rows.Count; row ++)
					{
						if (InputGrid.Columns["ProcessId"].CellText(row).Equals(processId))
						{
							rowIndex = row;
						}
					}
										
					mainForm.SecId = secId;

					InputGrid.Row = rowIndex;
					BoxLookup(rowIndex);

					BookTableRateSet();
					BookMarginSet();
					BookPriceSet(true);

					dataSet.Tables["Input"].AcceptChanges();
					InputGrid.DataChanged = true;

					mainForm.SecId = InputGrid.Columns["SecId"].Text;
					this.Cursor = Cursors.Default;
				}
			}
		}

		private void InputGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			if (!InputGrid.Row.Equals(e.LastRow) && !processList)
			{
				mainForm.SecId = InputGrid.Columns["SecId"].Text;
			}
		}
		
    
		private void InputGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			{
				if (InputGrid.Columns["Rate"].Text.Trim().Equals(""))
				{
					BookTableRateSet();
				}
				else
				{
					InputGrid.Columns["RateCode"].Text = " ";
				}
			}

			if (e.Column.DataColumn.DataField.Equals("Margin"))
			{
				if (InputGrid.Columns["Margin"].Text.Trim().Equals(""))
				{
					BookMarginSet();
					BookPriceSet(true);
				}
				else
				{
					marginCode = "%";
				}
			}

			if (e.Column.DataColumn.DataField.Equals("Quantity") || e.Column.Name.Equals("Price"))
			{
				BookPriceSet(true);
			}

			if (!e.Column.Name.Equals("Price"))
			{
				InputGrid.Splits[0,0].DisplayColumns["Price"].Style.ForeColor = Color.DimGray;

				BookPriceSet(true);
			}
			else
			{
				InputGrid.Splits[0,0].DisplayColumns["Price"].Style.ForeColor = Color.Navy;
			}
		}

		private void InputGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}

			switch (e.Column.DataField)
			{    
				case "MyQuantity" :
				case "RockQuantity":
				case "PsrQuantity":
				case "Quantity" :
				case "Amount" :
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;

				case "InventoryRate":
				case "Rate" :
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("0.000");                      
					}
					catch {}
					break;

				case "Margin" :
				case "Price" :			
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("0.00");                      
					}
					catch {}
					break;
			}
		}

		private void InputGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
		{
			try
			{
				switch (e.Column.Name)
				{
					case "My Quantity" :
						if ((long)InputGrid.Columns["MyQuantity"].CellValue(e.Row) > 0)
						{
							e.CellStyle.ForeColor = Color.Navy;
							break;
						}

						if ((long)InputGrid.Columns["MyQuantity"].CellValue(e.Row) < 0)
						{
							e.CellStyle.ForeColor = Color.Maroon;
							break;
						}

						e.CellStyle.ForeColor = Color.DimGray;
						break;

					case "Rock Quantity" :
						if ((long)InputGrid.Columns["RockQuantity"].CellValue(e.Row) > 0)
						{
							e.CellStyle.ForeColor = Color.Maroon;
							break;
						}					

						e.CellStyle.ForeColor = Color.DimGray;
						break;


					case "AZTEC Rate" :
						if ((decimal)InputGrid.Columns["InventoryRate"].CellValue(e.Row) > 0M)
						{
							e.CellStyle.ForeColor = Color.Navy;
							break;
						}

						if ((decimal)InputGrid.Columns["InventoryRate"].CellValue(e.Row) < 0M)
						{
							e.CellStyle.ForeColor = Color.Maroon;
							break;
						}

						e.CellStyle.ForeColor = Color.DimGray;
						break;

					case "Rate" :
						if ((decimal)InputGrid.Columns["Rate"].CellValue(e.Row) > 0M)
						{
							e.CellStyle.ForeColor = Color.Navy;
							break;
						}

						if ((decimal)InputGrid.Columns["Rate"].CellValue(e.Row) < 0M)
						{
							e.CellStyle.ForeColor = Color.Maroon;
							break;
						}

						e.CellStyle.ForeColor = Color.DimGray;
						break;
				}				
			}
			catch {}
		}

		private void CheckAllMenuItem_Click(object sender, System.EventArgs e)
		{			
			for (int row = 0; row < InputGrid.Splits[0].Rows.Count; row++)
			{
				InputGrid[row, "IsChecked"] = 1;			
			}
		}

		private void ClearAllMenuItem_Click(object sender, System.EventArgs e)
		{
			ListTextBox.Text = "";

			dataSet.Tables["Input"].Clear();
			dataSet.Tables["Input"].AcceptChanges();
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			StatusMessageLabel.Text = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InputGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in InputGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InputGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			StatusMessageLabel.Text = "Total: " + InputGrid.SelectedRows.Count + " items copied to the clipboard.";
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			StatusMessageLabel.Text = "";

			if (InputGrid.SelectedCols.Count.Equals(0))
			{
				StatusMessageLabel.Text = "You have not selected any rows to copy.";
				return;
			}

			try
			{
				maxTextLength = new int[InputGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InputGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in InputGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InputGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InputGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InputGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in InputGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InputGrid.SelectedCols)
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

				StatusMessageLabel.Text = "Total: " + InputGrid.SelectedRows.Count + " items added to e-mail.";
			}
			catch (Exception error)
			{       
				StatusMessageLabel.Text = error.Message;
			}
		}

		private void SendToTrashBinMenuItem_Click(object sender, System.EventArgs e)
		{
			int n;

			StatusMessageLabel.Text = "";
      
			if ((n = InputGrid.SelectedRows.Count) > 0)
			{
				try
				{ 
					InputGrid.Row = InputGrid.SelectedRows[0];

					while (n-- > 0)
					{
						InputGrid.Delete();
					}
          
					InputGrid.SelectedRows.Clear();
					InputGrid.SelectedCols.Clear();
				}
				catch (Exception ee)
				{
					StatusMessageLabel.Text = ee.Message;
				}   
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();	
			excel.ExportGridToExcel(ref InputGrid);

			this.Cursor = Cursors.Default;
		}

		private void ClearInputAreaMenuItem_Click(object sender, System.EventArgs e)
		{
			ListTextBox.Clear();
		}

		private void InputGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{			
			if (InputGrid.Columns["Flag"].CellText(e.Row).Equals("H") && NeedsRadio.Checked)
			{
				e.CellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			}
			else if (InputGrid.Columns["Flag"].CellText(e.Row).Equals("H") && OffersRadio.Checked)
			{
				e.CellStyle.BackColor = System.Drawing.Color.Ivory;
			}
            else if (InputGrid.Columns["Flag"].CellText(e.Row).Equals("N") && NeedsRadio.Checked)
            {
                e.CellStyle.BackColor = System.Drawing.Color.Bisque;
            }	
			else if (InputGrid.Columns["SubstitutionProcessIdFlag"].CellText(e.Row).Equals("S") && NeedsRadio.Checked)
			{
				e.CellStyle.BackColor = System.Drawing.Color.PeachPuff;
			}
			else if (InputGrid.Columns["SubstitutionProcessIdFlag"].CellText(e.Row).Equals("D") && NeedsRadio.Checked)
			{
				e.CellStyle.BackColor = System.Drawing.Color.AliceBlue;
			}
			else
			{			
				if (OffersRadio.Checked)
				{				
					e.CellStyle.BackColor = System.Drawing.Color.Ivory;					
				}
				else if (NeedsRadio.Checked)
				{			
					e.CellStyle.BackColor = System.Drawing.Color.Honeydew;										       
				}
			}
		}

		private void WhatIfButton_Click(object sender, System.EventArgs e)
		{
			string processId;
			int count = 0;

			Cursor.Current = Cursors.WaitCursor;
			
			try
			{
				StatusMessageLabel.Text = "Submtting what-ifs...";

				foreach(DataRow dr in dataSet.Tables["Input"].Rows)
				{
					dr["SubstitutionProcessId"] = "";

					if (bool.Parse(dr["IsChecked"].ToString()))
					{
						processId = mainForm.SubstitutionAgent.SubstitutionSet(
							"",
							"",
							dr["SecId"].ToString(),
							"0",
							"0",
							"",
							"",
							"",
							"",
							"W",
							"S",
							mainForm.UserId);

						dr["SubstitutionProcessId"] = processId;
						dr["IsChecked"] = false;
						dr["SubstitutionProcessIdFlag"] = "S";								
					
						count ++;
					}
				}
			
				
				InputGrid.Refresh();
				
				StatusMessageLabel.Text = "Done! Submitted " + count.ToString("#,##0") + " what-ifs.";

				SubstitutionTimer.Enabled = true;	
				SubstitutionTimer.Interval = int.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryDeskInputWhatIfTimeout", "5000"));

				Cursor.Current = Cursors.Default;
			}
			catch (Exception error)
			{
				StatusMessageLabel.Text = error.Message;
			}
		}

		private void SubstitutionTimer_Tick(object sender, System.EventArgs e)
		{
			DataSet dsSusbtitutionItem = new DataSet();
			long substitutionQuantity = 0;
			long exDeficitSettled = 0;
			int count = 0;

			StatusMessageLabel.Text = "Getting what-ifs/subsitutions...";
			
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				foreach (DataRow dr in dataSet.Tables["Input"].Rows)
				{
					if (!dr["SubstitutionProcessId"].ToString().Equals(""))
					{
						dsSusbtitutionItem = mainForm.SubstitutionAgent.SubstitutionGet(dr["SubstitutionProcessId"].ToString(), "", mainForm.UserId, mainForm.UtcOffset);
					
						if (dsSusbtitutionItem.Tables["Substitutions"].Rows.Count == 1)
						{																																					
							if (mainForm.PositionAgent.BlockedSecId(dr["SecId"].ToString()) && NeedsRadio.Checked)
							{
								exDeficitSettled = 0;
								InputGrid.Columns["Flag"].Text = "H";
					
							}							
							else if (dsSusbtitutionItem.Tables["Substitutions"].Rows[0]["Status"].ToString().Equals("A"))
							{
								substitutionQuantity = long.Parse(dsSusbtitutionItem.Tables["Substitutions"].Rows[0]["SubstitutionQuantity"].ToString());
							


								exDeficitSettled = BoxLookup(dr["SecId"].ToString());
							}
							else
							{
								substitutionQuantity = 0;
								exDeficitSettled = BoxLookup(dr["SecId"].ToString());
							}
					
												
							if ((exDeficitSettled +  substitutionQuantity) > 0)
							{
								
								hairCut = double.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryHairCut", "10"));
								hairCut = hairCut / 100;
								
								substitutionQuantity = exDeficitSettled + substitutionQuantity;
								
								substitutionQuantity = substitutionQuantity - (long)(substitutionQuantity * hairCut);								
								substitutionQuantity = substitutionQuantity - (substitutionQuantity % 100);

								dr["MyQuantity"] = substitutionQuantity;							
							}
							else
							{
								dr["MyQuantity"] = 0;	
							}						
						
							substitutionQuantity = 0;
						}
				
						dr["SubstitutionProcessIdFlag"] = "D";					
				
						count ++;
					}
				}				

				StatusMessageLabel.Text = "Done! Getting " + count.ToString("#,##0") + " what-ifs/substitutions";
			}
			catch (Exception error)
			{
				StatusMessageLabel.Text = error.Message;
			}
			
			dataSet.Tables["Input"].AcceptChanges();
		
			SubstitutionTimer.Enabled = false;
			Cursor.Current = Cursors.Default;
			InputGrid.Refresh();		
		}

		private void SubstitutionButton_Click(object sender, System.EventArgs e)
		{
			string processId;
			int count = 0;

			Cursor.Current = Cursors.WaitCursor;
			
			try
			{
				StatusMessageLabel.Text = "Submtting substitutions...";

				foreach(DataRow dr in dataSet.Tables["Input"].Rows)
				{			
					if (bool.Parse(dr["IsChecked"].ToString()))
					{
						processId = mainForm.SubstitutionAgent.SubstitutionSet(
							"",
							"",
							dr["SecId"].ToString(),
							dr["MyQuantity"].ToString(),
							"0",
							"",
							"",
							"",
							"",
							"S",
							"S",
							mainForm.UserId);

						dr["SubstitutionProcessId"] = processId;
						dr["IsChecked"] = false;
						dr["SubstitutionProcessIdFlag"] = "S";								
				
						count ++;
					}
				}
			
				
				InputGrid.Refresh();
				
				StatusMessageLabel.Text = "Done! Submitted " + count.ToString("#,##0") + " substitutions.";

				SubstitutionTimer.Enabled = true;	
				SubstitutionTimer.Interval = int.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryDeskInputWhatIfTimeout", "5000"));

				Cursor.Current = Cursors.Default;
			}
			catch (Exception error)
			{
				StatusMessageLabel.Text = error.Message;
			}
		}

        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
        {
            if (BookGroupCombo.Text.Equals("0234"))
            {
                SubstitutionButton.Enabled = true;
                WhatIfButton.Enabled = true;
            }
            else
            {
                SubstitutionButton.Enabled = false;
                WhatIfButton.Enabled = false;
            }
        }

        private void InputGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
        {
            if (e.Column.DataColumn.DataField.Equals("MyQuantity"))
            {
                if (e.Column.DataColumn.Text.Equals("0"))
                {
                    e.Cancel = true;
                }
            }
        }
	}

  public class Input
  {
    private string status = "";
    
    private ArrayList items;
    private ColumnIndex columnIndex;

    public Input() : this("") {}
    public Input(string list)
    {
      items = new ArrayList();
      columnIndex = new ColumnIndex();

      if (!list.Equals(""))
      {
        Parse(list);
      }
    }

    public string Parse(string list)
    {
      string [] records;
      string [] fields;

      char[] delimiter = new Char[1];

      items.Clear();
      columnIndex.Clear();

      try
      {
        delimiter[0] = '\n';
        
        records = list.Split(delimiter); // Do the split on new-line character; trim balance of white space later.
    
        for (int i = 0; i < records.Length; i++) // Parse list items.
        {
          string record = records[i].Trim(); // Taking a copy to trim just once.

          if (record.IndexOf(":") > 0) // Use ':' as delimiter for this record.
          {
            delimiter[0] = ':';
          }
          else if (record.IndexOf(";") > 0) // Use ';' as delimiter for this record.
          {
            delimiter[0] = ';';
          }
          else if (record.IndexOf("|") > 0) // Use '|' as delimiter for this record.
          {
            delimiter[0] = '|';
          }
          else if (record.IndexOf("\t") > 0) // Use tab as delimiter for this record.
          {
            delimiter[0] = '\t';
          }
          else // Must use ' ' as delimiter for this record.
          {
            delimiter[0] = ' ';

            for (int j = 25; j > 0; j--) // Replace multiple instances of space with just one.
            {
              records[i] = records[i].Replace(new String(delimiter[0], j), delimiter[0].ToString());
            }
          }
          
          fields = records[i].Split(delimiter);

          // ToDo: Any field manipulation.

          if (fields.Length > 3) // Hack to concatenate last two fields.
          {
            fields[2] += fields[3];
          }

          string[] values = new string[5] {"", "", "", "", ""};
          
          columnIndex.HaveSecId = false;

          for (int j = 0; (j < fields.Length) && (j < 4); j++)
          {
            values[j + 1] = columnIndex.Set(fields[j], j);  
          }
          
          if (columnIndex.HaveSecId)
          {
            items.Add(values);
          }
        }

        return status = "OK";
      }  
      catch (Exception e)
      {
        Log.Write(e.Message + " [Input.Parse]", 2);

        return status = "Error: Unable to parse your list.";
      }
    }

    public string SecId(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.SecId];
    }

    public string Quantity(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.Quantity];
    }

    public string Price(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.Price];
    }

    public string Rate(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.Rate];
    }

    public string Status
    {
      get 
      {
        return status; 
      }
    }

    public int Count
    {
      get 
      {
        return items.Count;
      }
    }
  
	  private class ColumnIndex
	  {
		  private int[] secIdCount = new int[4] {0, 0, 0, 0};
		  private int[] quantityCount = new int[4] {0, 0, 0, 0};
		  private int[] priceCount = new int[4] {0, 0, 0, 0};
		  private int[] rateCount = new int[4] {0, 0, 0, 0};

		  private bool haveSecId = false;
        
		  public string Set(string field, int index)
		  {
			  if (index > 3)
			  {
				  throw new Exception("Value for index, " + index + ",  must not be greater than 3. [ListStats.Set]");
			  }

			  if (index < 0)
			  {
				  throw new Exception("Value for index, " + index + ",  must not be less than 0. [ListStats.Set]");
			  }

			  field = field.ToUpper().Replace(" ", "").Replace(",", "").Trim();

			  if (field.StartsWith("(") && field.EndsWith(")"))
			  {
				  field = field.Replace("(", "-").Replace(")", "");
			  }

			  if (Security.IsCusip(field) || Security.IsSymbol(field))
			  {
				  haveSecId = true;

				  secIdCount[index]++;

				  return field;
			  }

			  if (Tools.IsNumeric(field))
			  {
				  decimal fieldValue = decimal.Parse(field);

				  if (fieldValue < 100M)
				  {
					  rateCount[index]++;
				  }
				  else
				  {
					  quantityCount[index]++;
				  }

				  return field;
			  }

			  field = field.Replace("M", "000");
			  field = field.Replace("K", "000");
			  field = field.Replace("C", "00");
			  field = field.Replace("H", "00");

			  if (Tools.IsNumeric(field))
			  {
				  quantityCount[index]++;

				  return field;
			  }

			  if (field.StartsWith("NEG") || field.EndsWith("NEG"))
			  {
				  field = "-" + field.Replace("NEG", "");

				  if (Tools.IsNumeric(field))
				  {
					  rateCount[index]++;

					  return field;
				  }
			  }

			  if (field.EndsWith("%"))
			  {
				  field = field.Replace("%", "");

				  if (Tools.IsNumeric(field))
				  {
					  rateCount[index]++;
         
					  return field;
				  }
			  }

			  if (field.StartsWith("N") || field.EndsWith("N"))
			  {
				  field = "-" + field.Replace("N", "");

				  if (Tools.IsNumeric(field))
				  {
					  rateCount[index]++;
         
					  return field;
				  }
			  }

			  if (field.StartsWith("P") || field.EndsWith("P"))
			  {
				  field.Replace("P", "");

				  if (Tools.IsNumeric(field))
				  {
					  priceCount[index]++;
				  }
			  }

			  return "";
		  }

		  public void Clear()
		  {
			  for (int i = 0; i < 4; i++)
			  {
				  secIdCount[i] = 0;
				  quantityCount[i] = 0;
				  priceCount[i] = 0;
				  rateCount[i] = 0;
			  }
		  }

		  public bool HaveSecId
		  {
			  set
			  {
				  haveSecId = value;
			  }

			  get
			  {
				  return haveSecId;
			  }
		  }

		  public int SecId
		  {
			  get
			  {
				  int index = 0;
				  int count = 0;

				  for (int i = 0; i < 4; i++)
				  {
					  if (secIdCount[i] > count)
					  {
						  count = secIdCount[i];
						  index = i + 1;
					  }
				  }

				  return index;
			  }
		  }

		  public int Quantity
		  {
			  get
			  {
				  int index = 0;
				  int count = 0;

				  for (int i = 0; i < 4; i++)
				  {
					  if (quantityCount[i] > count)
					  {
						  count = quantityCount[i];
						  index = i + 1;
					  }
				  }

				  return index;
			  }
		  }

		  public int Price
		  {
			  get
			  {
				  int index = 0;
				  int count = 0;

				  for (int i = 0; i < 4; i++)
				  {
					  if (priceCount[i] > count)
					  {
						  count = priceCount[i];
						  index = i + 1;
					  }
				  }

				  return index;
			  }
		  }

		  public int Rate
		  {
			  get
			  {
				  int index = 0;
				  int count = 0;

				  for (int i = 0; i < 4; i++)
				  {
					  if (rateCount[i] > count)
					  {
						  count = rateCount[i];
						  index = i + 1;
					  }
				  }

				  return index;
			  }
		  }
	  }
  }
}
