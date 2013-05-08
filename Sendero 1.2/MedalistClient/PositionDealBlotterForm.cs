using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

using System.Threading;

namespace Anetics.Medalist
{
	public class PositionDealBlotterForm : System.Windows.Forms.Form
	{
		private const string TEXT = "Position - Deals Blotter";
		private const string DEAL_ID_PREFIX = "D";
    
		private bool isReady = false;    

		MainForm mainForm;

		private DataSet dataSet;    
		private DataView bookDataView, borrowsDataView, loansDataView;
    
		private string borrowsRowFilter = "DealType='B' And IsActive = 1";
		private string loansRowFilter = "DealType='L' And IsActive = 1";
    
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid BorrowsGrid;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid LoansGrid;
    
		private System.Windows.Forms.Splitter GridSplitter;
		
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
		private System.Windows.Forms.MenuItem DockNoneMenuItem;
    
		private System.Windows.Forms.MenuItem Sep2MenuItem;
    
		private System.Windows.Forms.MenuItem ExitMenuItem;
    
		private C1.Win.C1Input.C1Label StatusLabel;
    
		private DealEventWrapper dealEventWrapper = null;
		private DealEventHandler dealEventHandler = null;
    
		private ArrayList dealEventArgsArray;

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
    
		int     _firstRowBorrows;
		int     _firstRowLoans;

		private C1.Win.C1TrueDBGrid.C1TrueDBDropdown BookGroupDropDown;
		private C1.Win.C1TrueDBGrid.C1TrueDBDropdown BooksDropDown;
		private System.Windows.Forms.CheckBox ShowContractsCheckBox;

		// -- Added by Yasir Bashir on 7/24/2007
		private Anetics.Medalist.DealsInformationControl dealsInformationControl;
		private Anetics.Medalist.CreditControl DealCreditControl;
		// --

		private delegate void SendDealsDelegate(ArrayList dealArray);

		private System.ComponentModel.Container components = null;

		public PositionDealBlotterForm(MainForm mainForm)
		{
			try
			{
				this.mainForm = mainForm;
				InitializeComponent();

				dealEventArgsArray = new ArrayList();      
      

				BorrowsGrid.Splits[0,0].DisplayColumns["DealId"].Visible = false;
				//BorrowsGrid.Splits[0,0].DisplayColumns["BookGroup"].Visible = false;
				BorrowsGrid.Splits[0,0].DisplayColumns["DealType"].Visible = false;
				BorrowsGrid.Splits[0,0].DisplayColumns["IncomeTracked"].Visible = false;
				BorrowsGrid.Splits[0,0].DisplayColumns["DivRate"].Visible = false;
				BorrowsGrid.Splits[0,0].DisplayColumns["OtherBook"].Visible = false;        
				BorrowsGrid.Splits[0,0].DisplayColumns["IsActive"].Visible = false;        
				BorrowsGrid.Splits[0,0].DisplayColumns["MarginCode"].Visible = false;        

				LoansGrid.Splits[0,0].DisplayColumns["DealId"].Visible = false;
				//LoansGrid.Splits[0,0].DisplayColumns["BookGroup"].Visible = false;
				LoansGrid.Splits[0,0].DisplayColumns["DealType"].Visible = false;
				LoansGrid.Splits[0,0].DisplayColumns["IncomeTracked"].Visible = false;
				LoansGrid.Splits[0,0].DisplayColumns["DivRate"].Visible = false;
				LoansGrid.Splits[0,0].DisplayColumns["OtherBook"].Visible = false;        
				LoansGrid.Splits[0,0].DisplayColumns["IsActive"].Visible = false;        
				LoansGrid.Splits[0,0].DisplayColumns["MarginCode"].Visible = false;        

				dealEventWrapper = new DealEventWrapper(); 
				dealEventWrapper.DealEvent += new DealEventHandler(DealOnEvent);       
      
				dealEventHandler = new DealEventHandler(DealDoEvent);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message);
			}
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

		private bool IsReady
		{
			get
			{
				return isReady;
			}

			set
			{
				DealEventArgs dealEventArgs;

				try
				{
					lock (this)
					{
						if (value && (dealEventArgsArray.Count > 0))
						{          
							isReady = false;

							dealEventArgs = (DealEventArgs)dealEventArgsArray[0];
							dealEventArgsArray.RemoveAt(0);

							dealEventHandler.BeginInvoke(dealEventArgs, null, null);            
						}
						else
						{
							isReady = value;
						}
					}
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [PositionDealBlotterForm.IsReady(set)]", Log.Error, 1); 
				}
			}
		}

		private void DealOnEvent(DealEventArgs dealEventArgs)
		{
			try
			{
				int i;
	      
				if (dealEventArgs.DealId.StartsWith(DEAL_ID_PREFIX))
				{
					lock (this)
					{
						i = dealEventArgsArray.Add(dealEventArgs);
						Log.Write("Deal event queued at " + i + " for deal ID: " + dealEventArgs.DealId + " [PositionDealBlotterForm.DealOnEvent]" , 3);

						if (this.IsReady) // Force reset to trigger handling of event.
						{
							this.IsReady = true;
						}
					}
				}
				else
				{
					Log.Write("Deal event being discarded for deal ID: " + dealEventArgs.DealId + " [PositionDealBlotterForm.DealOnEvent]" , 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionDealBlotterForm.DealOnEvent]", Log.Error, 1); 
			}

		}

		private void DealDoEvent(DealEventArgs dealEventArgs)
		{
			int firstRowBorrows;
			int firstRowLoans;

			try
			{ 
				lock (this)
				{

					Log.Write("Deal event being handled for deal ID: " + dealEventArgs.DealId + " [PositionDealBlotterForm.DealDoEvent]" , 3);

					firstRowBorrows = BorrowsGrid.FirstRow;
					firstRowLoans = LoansGrid.FirstRow;

					dataSet.Tables["Deals"].BeginLoadData();

					dealEventArgs.UtcOffset = mainForm.UtcOffset;
					dataSet.Tables["Deals"].LoadDataRow(dealEventArgs.Values, true);        
					dataSet.Tables["Deals"].EndLoadData();   

					FormStatusSet();

					BorrowsGrid.FirstRow = firstRowBorrows;  
					LoansGrid.FirstRow = firstRowLoans;
  
					this.IsReady = true;
				}
			}
			catch (Exception e)
			{
				Log.Write(e + " [PositionDealBlotterForm.DealDoEvent]", Log.Error, 1);          
			}
		}

		private void FormStatusSet()
		{
			try
			{

				if ((BorrowsGrid.SelectedRows.Count > 0) && BorrowsGrid.Focused)
				{
					StatusLabel.Text = "Selected " + BorrowsGrid.SelectedRows.Count.ToString("#,##0") + " item[s] of " + borrowsDataView.Count.ToString("#,##0") + " shown in grid.";
					return;
				}

				if ((LoansGrid.SelectedRows.Count > 0) && BorrowsGrid.Focused)
				{
					StatusLabel.Text = "Selected " + LoansGrid.SelectedRows.Count.ToString("#,##0") + " item[s] of " + loansDataView.Count.ToString("#,##0") + " shown in grid.";
					return;
				}

				StatusLabel.Text = "Showing " + borrowsDataView.Count.ToString("#,##0") + " borrow item[s] and " + loansDataView.Count.ToString("#,##0") + " loan item[s] in grids.";
			}
			catch (Exception e)
			{
				Log.Write(e + " [FormStatusSet]", Log.Error, 1);          
			}
		}

		public void AmountSet(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid)
		{     
			      
			try
			{
				decimal price;
				long    quantity;
				decimal margin;
				string  markRoundHouse;
				decimal markRoundInstitution;
				decimal priceMinimum;
				decimal amountMinimum;
				decimal amount;
	   
				if (mainForm.Price.Equals(""))
				{
					dealsGrid.Columns["Amount"].Text = "";
					return;
				}
	      
				if (dealsGrid.Columns["Quantity"].Text.Trim().Equals(""))
				{
					dealsGrid.Columns["Amount"].Text = "";
					return;
				}

				price = decimal.Parse(mainForm.Price);
				quantity = long.Parse(dealsGrid.Columns["Quantity"].Value.ToString());

				if (dealsGrid.Columns["Margin"].Text.Trim().Equals(""))
				{
					margin = 1.00M;
				}
				else
				{
					margin = decimal.Parse(dealsGrid.Columns["Margin"].Text);
				}

				DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
				{
					dealsGrid.Columns["BookGroup"].Text,
					dealsGrid.Columns["Book"].Text
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
      
				price = price * margin;                
      
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
      
				amount =  decimal.Round(price * quantity, 2);

				if (amount < amountMinimum)
				{
					amount = amountMinimum;
				}

				dealsGrid.Columns["Amount"].Text = amount.ToString("#,##0");
			}
			catch (Exception e)
			{
				mainForm.Alert(e.Message);
			}
		}

		private void BookMarginSet(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid, string dealType)
		{
			try
			{
				DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
				{
					dealsGrid.Columns["BookGroup"].Text,
					dealsGrid.Columns["Book"].Text
				});
      
				if (bookRecord != null)
				{
					dealsGrid.Columns["Margin"].Value = bookRecord["Margin" + dealType];      
					dealsGrid.Columns["MarginCode"].Text = "%";
				}
				else
				{
					dealsGrid.Columns["Margin"].Value = DBNull.Value;      
					dealsGrid.Columns["MarginCode"].Text = "%";
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionDealBlotterForm.BookMarginSet]", Log.Error, 1);
			}
		}
    
		private void BookTableRateSet(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid, string dealType)
		{
			try
			{
				string s = dealsGrid.Name;
				DataRow bookRecord = dataSet.Tables["Books"].Rows.Find(new object[2] 
				{
					dealsGrid.Columns["BookGroup"].Text,
					dealsGrid.Columns["Book"].Text
				});

				if (bookRecord == null)
				{
					return;
				}
      
				if (mainForm.IsBond)
				{
					if (!bookRecord["RateBond" + dealType].Equals(DBNull.Value))
					{
						dealsGrid.Columns["Rate"].Value = bookRecord["RateBond + dealType"];
						dealsGrid.Columns["RateCode"].Value = "T";
					}
					else
					{
						dealsGrid.Columns["Rate"].Text = "";
						dealsGrid.Columns["RateCode"].Text = " ";
					}
				}
				else
				{
					if (!bookRecord["RateStock" + dealType].Equals(DBNull.Value))
					{
						dealsGrid.Columns["Rate"].Value = bookRecord["RateStock + dealType"];
						dealsGrid.Columns["RateCode"].Value = "T";
					}
					else
					{
						dealsGrid.Columns["Rate"].Text = "";
						dealsGrid.Columns["RateCode"].Text = " ";
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionDealBlotterForm.BookTableRateSet]", Log.Error, 1);
			}
		}

		private void DeleteDeals(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid)
		{
			try
			{  

				this.IsReady = false;

				int n = 0;
				ArrayList dealArray = new ArrayList();
	      
				if (dealsGrid.SelectedRows.Count > 0)
				{
					dealsGrid.Row = dealsGrid.SelectedRows[0];
				}
      
				foreach (int i in dealsGrid.SelectedRows)
				{          
					if (dealsGrid.Columns["DealStatus"].CellText(i).Equals("P"))
					{
						mainForm.Alert("Deal in " + dealsGrid.Columns["SecId"].CellText(i) + " for " + dealsGrid.Columns["Quantity"].CellText(i) + " is pending.");
						continue;
					}

					dealArray.Add(dealsGrid.Columns["DealId"].CellText(i));            
				}

				// -- 
				dealsGrid.SelectedRows.Clear();
				dealsGrid.SelectedCols.Clear();
				
				// -- Added by Yasir Bashir on 8/13/2007
				dealsGrid.Row -= 1;
				// -- 

				foreach (string dealId in dealArray)
				{
					try
					{
						mainForm.PositionAgent.DealSet(dealId, "", mainForm.UserId, false);
					}
					catch (Exception e)
					{
						n += 1;
						mainForm.Alert(e.Message);
					}
				}

				mainForm.Alert("Trashed " + dealArray.Count + " deal[s] of " + dealsGrid.SelectedRows.Count + " with " + n + " error[s].");
				
				
				//dealsGrid.Row -= 1;

				this.IsReady = true;
			}
			catch (Exception e)
			{
				mainForm.Alert(e.Message);
				Log.Write(e + " [PositionDealBlotterForm.DeleteDeals]", Log.Error, 1);
			}    
		}

		private void EmailDeals(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid)
		{
			
			try
			{
				int textLength;
				int [] maxTextLength;

				int columnIndex = -1;
				string gridData = "\n\n\n";

				if (dealsGrid.SelectedCols.Count.Equals(0))
				{
					mainForm.Alert("You have not selected any rows.");
					return;
				}

				maxTextLength = new int[dealsGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in dealsGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in dealsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in dealsGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in dealsGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in dealsGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in dealsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in dealsGrid.SelectedCols)
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

				mainForm.Alert("Total: " + dealsGrid.SelectedRows.Count + " items added to e-mail.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionDealBlotterForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}    
		}

		private void ProcessDeals(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid)
		{
			try
			{   
				int startingRow = dealsGrid.Row; 
				int firstRow = dealsGrid.FirstRow;

				ArrayList dealArray = new ArrayList();
	      
				foreach (int i in dealsGrid.SelectedRows)
				{          
					dealsGrid.Row = i;

					switch (dealsGrid.Columns["DealStatus"].Text)
					{
						case "S" :
							mainForm.Alert("Deal in " + dealsGrid.Columns["SecId"].Text + " for " + dealsGrid.Columns["Quantity"].Text + " has already been sent.");
							continue;
						case "P" :
							mainForm.Alert("Deal in " + dealsGrid.Columns["SecId"].Text + " for " + dealsGrid.Columns["Quantity"].Text + " is already pending.");
							continue;
						case "C" :
							mainForm.Alert("Deal in " + dealsGrid.Columns["SecId"].Text + " for " + dealsGrid.Columns["Quantity"].Text + " is already a contract.");
							continue;
					}

					if (dealsGrid.Columns["Book"].CellText(i).Equals(""))
					{
						mainForm.Alert("Deal in " + dealsGrid.Columns["SecId"].CellText(i) + " for " + dealsGrid.Columns["Quantity"].CellText(i) + " needs a Book.");
						continue;
					}

					if (dealsGrid.Columns["Rate"].CellText(i).Equals(""))
					{
						mainForm.Alert("Deal in " + dealsGrid.Columns["SecId"].CellText(i) + " for " + dealsGrid.Columns["Quantity"].CellText(i) + " needs a Rate.");
						continue;
					}
          
					if (dealsGrid.Columns["Amount"].CellText(i).Equals(""))
					{
						mainForm.Alert("Deal in " + dealsGrid.Columns["SecId"].CellText(i) + " for " + dealsGrid.Columns["Quantity"].CellText(i) + " needs an Amount.");
						continue;
					}
          
					dealArray.Add(dealsGrid[dealsGrid.Row]);            
      
					dealsGrid.Columns["DealStatus"].Text = "S";
				}

				dealsGrid.MoveLast();
				dealsGrid.Row += 1;
				dealsGrid.Row = startingRow;
				dealsGrid.FirstRow = firstRow;

				mainForm.Alert("Sent " + dealArray.Count + " deal[s] of " + dealsGrid.SelectedRows.Count + " to Contract.");
				
				dealsGrid.SelectedRows.Clear();
				dealsGrid.SelectedCols.Clear();

				SendDealsDelegate sendDealsDelegate = new SendDealsDelegate(SendDeals);
				sendDealsDelegate.BeginInvoke(dealArray, null, null);
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message);
				Log.Write(ee.Message + " [PositionDealBlotterForm.SendToProcessAgentMenuItem_Click]", Log.Error, 1);
			}    
		}

		private void SendDeals(ArrayList dealArray)
		{
			try
			{
				foreach (DataRowView row in dealArray)
				{

					Log.Write("Sending Deal ID " + row["DealId"].ToString() + " in " + row["BookGroup"].ToString() + " for " +
						row["Quantity"].ToString() + " of " + row["SecId"] + " to Contract. [PositionDealBlotterForm.SendDeals]", 3);

					mainForm.PositionAgent.DealSet(row["DealId"].ToString(), "S", mainForm.UserId, true);
				}
			}
			catch (Exception e)
			{
				Log.Write("Error : " + e.Message + " [PositionDealBlotterForm.SendDeals]", Log.Error, 1);
			}
		}

		private void GridRowFilterSet()
		{
			try
			{
				borrowsRowFilter = "DealType = 'B' And IsActive = 1";
				loansRowFilter = "DealType = 'L' And IsActive = 1";

				if (!ShowContractsCheckBox.Checked)
				{
					borrowsRowFilter += " And DealStatus <> 'C' And IsActive = 1";
					loansRowFilter += " And DealStatus <> 'C' And IsActive = 1";        
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message);
				Log.Write(ee.Message + " [PositionDealBlotterForm.GridRowFilterSet]", Log.Error, 1);
			}    
			
		}

		private void DealSync(string secId)
		{
			//
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionDealBlotterForm));
			this.LoansGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.GridSplitter = new System.Windows.Forms.Splitter();
			this.BorrowsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.DealCreditControl = new Anetics.Medalist.CreditControl();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToProcessAgentMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToTrashBinMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowOtherBookMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowDivRateMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowIncomeTrackedMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.DockMenuItem = new System.Windows.Forms.MenuItem();
			this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
			this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
			this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.BookGroupDropDown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
			this.BooksDropDown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
			this.ShowContractsCheckBox = new System.Windows.Forms.CheckBox();
			this.dealsInformationControl = new Anetics.Medalist.DealsInformationControl();
			((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BorrowsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupDropDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BooksDropDown)).BeginInit();
			this.SuspendLayout();
			// 
			// LoansGrid
			// 
			this.LoansGrid.AllowColMove = false;
			this.LoansGrid.AllowColSelect = false;
			this.LoansGrid.AllowFilter = false;
			this.LoansGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.LoansGrid.AllowUpdate = false;
			this.LoansGrid.CaptionHeight = 17;
			this.LoansGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LoansGrid.EmptyRows = true;
			this.LoansGrid.ExtendRightColumn = true;
			this.LoansGrid.FetchRowStyles = true;
			this.LoansGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.LoansGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.LoansGrid.Location = new System.Drawing.Point(1, 337);
			this.LoansGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.LoansGrid.Name = "LoansGrid";
			this.LoansGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.LoansGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.LoansGrid.PreviewInfo.ZoomFactor = 75;
			this.LoansGrid.RecordSelectorWidth = 16;
			this.LoansGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.LoansGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.LoansGrid.RowHeight = 15;
			this.LoansGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.LoansGrid.Size = new System.Drawing.Size(1050, 200);
			this.LoansGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.LoansGrid.TabIndex = 3;
			this.LoansGrid.Text = "Loans";
			this.LoansGrid.WrapCellPointer = true;
			this.LoansGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LoansGrid_Paint);
			this.LoansGrid.AfterUpdate += new System.EventHandler(this.Grid_AfterUpdate);
			this.LoansGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.BorrowsGrid_BeforeColEdit);
			this.LoansGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.SelChange);
			this.LoansGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.LoansGrid_FetchRowStyle);
			this.LoansGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoansGrid_MouseDown);
			this.LoansGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LoansGrid_BeforeUpdate);
			this.LoansGrid.ColResize += new C1.Win.C1TrueDBGrid.ColResizeEventHandler(this.LoansGrid_ColResize);
			this.LoansGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.LoansGrid_BeforeColUpdate);
			this.LoansGrid.OnAddNew += new System.EventHandler(this.LoansGrid_OnAddNew);
			this.LoansGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.LoansGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoansGrid_KeyPress);
			this.LoansGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.Grid_Error);
			this.LoansGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\" DropDownCtl=\"BooksDropDown\"><ValueItems Translate=\"True\" /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"Quantit" +
				"y\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><ValueItems /><Grou" +
				"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbo" +
				"l\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Am" +
				"ount\" DataField=\"Amount\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Rate\" DataField=\"Rate\" EditM" +
				"askUpdate=\"True\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Margin\" DataField=\"Margin\" EditMask=" +
				"\"0.00\" EditMaskUpdate=\"True\" NumberFormat=\"FormatText Event\"><ValueItems /><Grou" +
				"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"MarginCode\" DataField=\"M" +
				"arginCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"PC\" DataField=\"PoolCode\" EditMask=\"&gt;&amp;\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S\" DataFi" +
				"eld=\"DealStatus\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" DataField=\"DealType\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"Rate" +
				"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=" +
				"\"I\" DataField=\"IncomeTracked\"><ValueItems Presentation=\"CheckBox\" Validate=\"True" +
				"\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"DivRate\" DataFi" +
				"eld=\"DivRate\" EditMask=\"000.000\" EditMaskUpdate=\"True\" NumberFormat=\"FormatText " +
				"Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Other Book\" DataField=\"OtherBook\" DropDownCtl=\"BooksDropDown\"><ValueItems Tran" +
				"slate=\"True\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"IsAc" +
				"tive\" DataField=\"IsActive\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTim" +
				"e\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Deal ID\" DataField=\"DealId\" NumberFormat=\"FormatTe" +
				"xt Event\"><ValueItems><internalValues><ValueItem type=\"C1.Win.C1TrueDBGrid.Value" +
				"Item\" Value=\"-\" dispVal=\"-\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Va" +
				"lue=\"0\" dispVal=\"0\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"1\" " +
				"dispVal=\"1\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"2\" dispVal=" +
				"\"2\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"3\" dispVal=\"3\" /><V" +
				"alueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"4\" dispVal=\"4\" /><ValueItem" +
				" type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"5\" dispVal=\"5\" /><ValueItem type=\"C" +
				"1.Win.C1TrueDBGrid.ValueItem\" Value=\"6\" dispVal=\"6\" /><ValueItem type=\"C1.Win.C1" +
				"TrueDBGrid.ValueItem\" Value=\"7\" dispVal=\"7\" /><ValueItem type=\"C1.Win.C1TrueDBGr" +
				"id.ValueItem\" Value=\"8\" dispVal=\"8\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.Value" +
				"Item\" Value=\"9\" dispVal=\"9\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Va" +
				"lue=\".\" dispVal=\".\" /></internalValues></ValueItems><GroupInfo /></C1DataColumn>" +
				"<C1DataColumn Level=\"0\" Caption=\"C\" DataField=\"CollateralCode\"><ValueItems Cycle" +
				"OnClick=\"True\" Validate=\"True\"><internalValues><ValueItem type=\"C1.Win.C1TrueDBG" +
				"rid.ValueItem\" Value=\"C\" dispVal=\"C\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.Valu" +
				"eItem\" Value=\"N\" dispVal=\"N\" /></internalValues></ValueItems><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Book Group\" DataField=\"BookGroup\" Drop" +
				"DownCtl=\"BookGroupDropDown\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols" +
				"><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{For" +
				"eColor:HighlightText;BackColor:Highlight;}Style85{Font:Verdana, 8.25pt;AlignHorz" +
				":Near;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78" +
				"{}Style79{Font:Verdana, 8.25pt;AlignHorz:Near;}Style72{}Style118{AlignHorz:Near;" +
				"}Style70{AlignHorz:Near;}Style71{AlignHorz:Near;}Style115{Font:Verdana, 8.25pt;A" +
				"lignHorz:Near;}Style73{Font:Verdana, 8.25pt;AlignHorz:Near;}Style117{}Style116{}" +
				"Style111{}Style110{}Style113{AlignHorz:Near;}Style112{AlignHorz:Near;}Style76{Al" +
				"ignHorz:Center;}Style77{AlignHorz:Center;}Style74{}Style75{}Style84{}Style87{}St" +
				"yle86{}Style81{}Style80{}Style83{AlignHorz:Near;}Style82{AlignHorz:Near;}Style89" +
				"{AlignHorz:Center;}Style88{AlignHorz:Center;}Style108{}Style109{Font:Verdana, 8." +
				"25pt;AlignHorz:Near;}Style104{}Style105{}Style106{AlignHorz:Center;}Style107{Ali" +
				"gnHorz:Center;}Editor{}Style101{AlignHorz:Center;}Style102{}Style103{Font:Verdan" +
				"a, 8.25pt;AlignHorz:Near;}Style94{AlignHorz:Center;}Style95{AlignHorz:Center;}St" +
				"yle96{}Style97{Font:Verdana, 8.25pt;AlignHorz:Near;}Style90{}Style91{Font:Verdan" +
				"a, 8.25pt;AlignHorz:Near;}Style92{}Style93{}RecordSelector{AlignImage:Center;}St" +
				"yle98{}Style99{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;Fo" +
				"reColor:ControlText;AlignVert:Center;}Style18{}Style19{Font:Verdana, 8.25pt;Alig" +
				"nHorz:Near;}Style14{}Style15{}Style16{AlignHorz:Center;}Style17{AlignHorz:Center" +
				";}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style159{}Selected{ForeColo" +
				"r:HighlightText;BackColor:Highlight;}Style122{}Style153{}Style23{AlignHorz:Near;" +
				"}Style24{}Style9{}Style8{}Style25{Font:Verdana, 8.25pt;AlignHorz:Near;}Style28{A" +
				"lignHorz:Near;}Style29{AlignHorz:Far;}Style27{}Style26{}Style123{}Style120{}Styl" +
				"e121{Font:Verdana, 8.25pt;AlignHorz:Near;}Style158{}Style22{AlignHorz:Near;}Styl" +
				"e21{}Style20{}Style151{Font:Verdana, 8.25pt;AlignHorz:Near;}Style150{}OddRow{}St" +
				"yle152{}Style155{AlignHorz:Near;}Style154{AlignHorz:Near;}Style157{Font:Verdana," +
				" 8.25pt;AlignHorz:Near;}Style156{}Style144{}Style145{Font:Verdana, 8.25pt;AlignH" +
				"orz:Near;}Style43{Font:Verdana, 8.25pt;AlignHorz:Near;}Style42{}Style38{}Style39" +
				"{}Style36{}FilterBar{BackColor:SeaShell;}Style37{Font:Verdana, 8.25pt;AlignHorz:" +
				"Near;}Style148{AlignHorz:Near;}Style149{AlignHorz:Near;}Normal{Font:Verdana, 8pt" +
				";BackColor:Ivory;}Style34{AlignHorz:Near;}Style49{Font:Verdana, 8.25pt;AlignHorz" +
				":Near;}Style48{}Style31{Font:Verdana, 8.25pt;AlignHorz:Near;}Style35{AlignHorz:N" +
				"ear;}Style32{}Style33{}Style142{AlignHorz:Near;}Style143{AlignHorz:Near;}Style41" +
				"{AlignHorz:Near;}Style40{AlignHorz:Near;}Style146{}Style147{}Style45{}Style44{}S" +
				"tyle47{AlignHorz:Far;}Style46{AlignHorz:Near;}Style30{}EvenRow{BackColor:Aqua;}S" +
				"tyle51{}Style163{Font:Verdana, 8.25pt;AlignHorz:Near;}Style5{}Style4{}Style7{}St" +
				"yle6{}Style58{AlignHorz:Near;}Style59{AlignHorz:Far;}Style3{}Style2{}Style50{}Fo" +
				"oter{}Style52{AlignHorz:Near;}Style53{AlignHorz:Far;}Style54{}Style55{Font:Verda" +
				"na, 8.25pt;AlignHorz:Near;}Style56{}Style57{}Style119{AlignHorz:Far;}Style67{Fon" +
				"t:Verdana, 8.25pt;AlignHorz:Near;}Caption{AlignHorz:Center;}Style61{Font:Verdana" +
				", 8.25pt;AlignHorz:Near;}Style60{}Style69{}Style68{}Style162{}Style1{BackColor:H" +
				"oneydew;}Style160{AlignHorz:Near;}Style161{AlignHorz:Near;}Style63{}Style62{}Sty" +
				"le164{}Style165{}Style100{AlignHorz:Near;}Style66{}Style65{AlignHorz:Near;}Style" +
				"64{AlignHorz:Near;}Style114{}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;Back" +
				"Color:ControlDark;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarSt" +
				"yle=\"None\" VBarStyle=\"Always\" AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"" +
				"\" AllowRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooter" +
				"Height=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" MarqueeStyle=\"SolidCe" +
				"llBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" H" +
				"orizontalScrollGroup=\"1\"><Caption>Loans</Caption><CaptionStyle parent=\"Style2\" m" +
				"e=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"E" +
				"venRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterS" +
				"tyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><He" +
				"adingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRo" +
				"w\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle par" +
				"ent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Styl" +
				"e11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=" +
				"\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e160\" /><Style parent=\"Style1\" me=\"Style161\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style162\" /><EditorStyle parent=\"Style5\" me=\"Style163\" /><GroupHeaderStyle paren" +
				"t=\"Style1\" me=\"Style165\" /><GroupFooterStyle parent=\"Style1\" me=\"Style164\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</He" +
				"ight><Locked>True</Locked><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Width>85</Width><Height>15</Height><Button>True</Button><DCIdx>" +
				"21</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle94\" /><Style parent=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style96\" /><EditorStyle parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style99\" /><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width>" +
				"<Height>15</Height><Locked>True</Locked><DCIdx>11</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" " +
				"me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>D" +
				"arkGray,Single</ColumnDivider><Width>85</Width><Height>15</Height><Button>True</" +
				"Button><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>95</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style" +
				"41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>75</Width><Height>15</Height><AllowFocus>False</Allo" +
				"wFocus><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>90</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style" +
				"47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>95</Width><Height>15</Height><AllowFocus>False</Allo" +
				"wFocus><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
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
				"Cols><ClientRect>0, 0, 1046, 196</ClientRect><BorderSide>0</BorderSide></C1.Win." +
				"C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Sty" +
				"le parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style p" +
				"arent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style pa" +
				"rent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent" +
				"=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style paren" +
				"t=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style pa" +
				"rent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyle" +
				"s><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><" +
				"DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1046, 196</ClientAre" +
				"a><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\"" +
				" me=\"Style15\" /></Blob>";
			// 
			// GridSplitter
			// 
			this.GridSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.GridSplitter.Location = new System.Drawing.Point(1, 537);
			this.GridSplitter.MinExtra = 186;
			this.GridSplitter.MinSize = 21;
			this.GridSplitter.Name = "GridSplitter";
			this.GridSplitter.Size = new System.Drawing.Size(1050, 4);
			this.GridSplitter.TabIndex = 4;
			this.GridSplitter.TabStop = false;
			// 
			// BorrowsGrid
			// 
			this.BorrowsGrid.AllowColMove = false;
			this.BorrowsGrid.AllowColSelect = false;
			this.BorrowsGrid.AllowFilter = false;
			this.BorrowsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.BorrowsGrid.AllowUpdate = false;
			this.BorrowsGrid.CaptionHeight = 17;
			this.BorrowsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BorrowsGrid.EmptyRows = true;
			this.BorrowsGrid.ExtendRightColumn = true;
			this.BorrowsGrid.FetchRowStyles = true;
			this.BorrowsGrid.FilterBar = true;
			this.BorrowsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.BorrowsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.BorrowsGrid.Location = new System.Drawing.Point(1, 132);
			this.BorrowsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.BorrowsGrid.Name = "BorrowsGrid";
			this.BorrowsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.BorrowsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.BorrowsGrid.PreviewInfo.ZoomFactor = 75;
			this.BorrowsGrid.RecordSelectorWidth = 16;
			this.BorrowsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.BorrowsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.BorrowsGrid.RowHeight = 15;
			this.BorrowsGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.BorrowsGrid.Size = new System.Drawing.Size(1050, 205);
			this.BorrowsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.BorrowsGrid.TabIndex = 5;
			this.BorrowsGrid.Text = "Borrows";
			this.BorrowsGrid.WrapCellPointer = true;
			this.BorrowsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.BorrowsGrid_Paint);
			this.BorrowsGrid.AfterUpdate += new System.EventHandler(this.Grid_AfterUpdate);
			this.BorrowsGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.BorrowsGrid_BeforeColEdit);
			this.BorrowsGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.SelChange);
			this.BorrowsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.BorrowsGrid_FetchRowStyle);
			this.BorrowsGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorrowsGrid_MouseDown);
			this.BorrowsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BorrowsGrid_BeforeUpdate);
			this.BorrowsGrid.ColResize += new C1.Win.C1TrueDBGrid.ColResizeEventHandler(this.BorrowsGrid_ColResize);
			this.BorrowsGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.BorrowsGrid_BeforeColUpdate);
			this.BorrowsGrid.FilterChange += new System.EventHandler(this.BorrowsGrid_FilterChange);
			this.BorrowsGrid.OnAddNew += new System.EventHandler(this.BorrowsGrid_OnAddNew);
			this.BorrowsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.BorrowsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BorrowsGrid_KeyPress);
			this.BorrowsGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.Grid_Error);
			this.BorrowsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\" DropDownCtl=\"BooksDropDown\"><ValueItems Translate=\"True\" /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"Quantit" +
				"y\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><ValueItems /><Grou" +
				"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbo" +
				"l\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Am" +
				"ount\" DataField=\"Amount\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Rate\" DataField=\"Rate\" EditM" +
				"askUpdate=\"True\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Margin\" DataField=\"Margin\" EditMask=" +
				"\"0.00\" EditMaskUpdate=\"True\" NumberFormat=\"FormatText Event\"><ValueItems /><Grou" +
				"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"MarginCode\" DataField=\"M" +
				"arginCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"PC\" DataField=\"PoolCode\" EditMask=\"&gt;&amp;\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S\" DataFi" +
				"eld=\"DealStatus\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" DataField=\"DealType\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"Rate" +
				"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=" +
				"\"I\" DataField=\"IncomeTracked\"><ValueItems Presentation=\"CheckBox\" Validate=\"True" +
				"\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"DivRate\" DataFi" +
				"eld=\"DivRate\" EditMask=\"000.000\" EditMaskUpdate=\"True\" NumberFormat=\"FormatText " +
				"Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Other Book\" DataField=\"OtherBook\" DropDownCtl=\"BooksDropDown\"><ValueItems Tran" +
				"slate=\"True\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"IsAc" +
				"tive\" DataField=\"IsActive\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTim" +
				"e\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Deal ID\" DataField=\"DealId\" NumberFormat=\"FormatTe" +
				"xt Event\"><ValueItems><internalValues><ValueItem type=\"C1.Win.C1TrueDBGrid.Value" +
				"Item\" Value=\"-\" dispVal=\"-\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Va" +
				"lue=\"0\" dispVal=\"0\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"1\" " +
				"dispVal=\"1\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"2\" dispVal=" +
				"\"2\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"3\" dispVal=\"3\" /><V" +
				"alueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"4\" dispVal=\"4\" /><ValueItem" +
				" type=\"C1.Win.C1TrueDBGrid.ValueItem\" Value=\"5\" dispVal=\"5\" /><ValueItem type=\"C" +
				"1.Win.C1TrueDBGrid.ValueItem\" Value=\"6\" dispVal=\"6\" /><ValueItem type=\"C1.Win.C1" +
				"TrueDBGrid.ValueItem\" Value=\"7\" dispVal=\"7\" /><ValueItem type=\"C1.Win.C1TrueDBGr" +
				"id.ValueItem\" Value=\"8\" dispVal=\"8\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.Value" +
				"Item\" Value=\"9\" dispVal=\"9\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.ValueItem\" Va" +
				"lue=\".\" dispVal=\".\" /></internalValues></ValueItems><GroupInfo /></C1DataColumn>" +
				"<C1DataColumn Level=\"0\" Caption=\"C\" DataField=\"CollateralCode\"><ValueItems Cycle" +
				"OnClick=\"True\" Validate=\"True\"><internalValues><ValueItem type=\"C1.Win.C1TrueDBG" +
				"rid.ValueItem\" Value=\"C\" dispVal=\"C\" /><ValueItem type=\"C1.Win.C1TrueDBGrid.Valu" +
				"eItem\" Value=\"N\" dispVal=\"N\" /></internalValues></ValueItems><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Book Group\" DataField=\"BookGroup\" Drop" +
				"DownCtl=\"BookGroupDropDown\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols" +
				"><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{For" +
				"eColor:HighlightText;BackColor:Highlight;}Style85{Font:Verdana, 8.25pt;AlignHorz" +
				":Near;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78" +
				"{}Style79{Font:Verdana, 8.25pt;AlignHorz:Near;}Style72{}Style118{AlignHorz:Near;" +
				"}Style70{AlignHorz:Near;}Style71{AlignHorz:Near;}Style115{Font:Verdana, 8.25pt;A" +
				"lignHorz:Near;}Style73{Font:Verdana, 8.25pt;AlignHorz:Near;}Style117{}Style116{}" +
				"Style111{}Style110{}Style113{AlignHorz:Near;}Style112{AlignHorz:Near;}Style76{Al" +
				"ignHorz:Center;}Style77{AlignHorz:Center;}Style74{}Style75{}Style84{}Style87{}St" +
				"yle86{}Style81{}Style80{}Style83{AlignHorz:Near;}Style82{AlignHorz:Near;}Style89" +
				"{AlignHorz:Center;}Style88{AlignHorz:Center;}Style108{}Style109{Font:Verdana, 8." +
				"25pt;AlignHorz:Near;}Style104{}Style105{}Style106{AlignHorz:Center;}Style107{Ali" +
				"gnHorz:Center;}Editor{}Style101{AlignHorz:Center;}Style102{}Style103{Font:Verdan" +
				"a, 8.25pt;AlignHorz:Near;}Style94{AlignHorz:Center;}Style95{AlignHorz:Center;}St" +
				"yle96{}Style97{Font:Verdana, 8.25pt;AlignHorz:Near;}Style90{}Style91{Font:Verdan" +
				"a, 8.25pt;AlignHorz:Near;}Style92{}Style93{}RecordSelector{AlignImage:Center;}St" +
				"yle98{}Style99{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;Fo" +
				"reColor:ControlText;AlignVert:Center;}Style18{}Style19{Font:Verdana, 8.25pt;Alig" +
				"nHorz:Near;}Style14{}Style15{}Style16{AlignHorz:Center;}Style17{AlignHorz:Center" +
				";}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style159{}Selected{ForeColo" +
				"r:HighlightText;BackColor:Highlight;}Style122{}Style153{}Style23{AlignHorz:Near;" +
				"}Style24{}Style9{}Style8{}Style25{Font:Verdana, 8.25pt;AlignHorz:Near;}Style28{A" +
				"lignHorz:Near;}Style29{AlignHorz:Far;}Style27{}Style26{}Style123{}Style120{}Styl" +
				"e121{Font:Verdana, 8.25pt;AlignHorz:Near;}Style158{}Style22{AlignHorz:Near;}Styl" +
				"e21{}Style20{}Style151{Font:Verdana, 8.25pt;AlignHorz:Near;}Style150{}OddRow{}St" +
				"yle152{}Style155{AlignHorz:Near;}Style154{AlignHorz:Near;}Style157{Font:Verdana," +
				" 8.25pt;AlignHorz:Near;}Style156{}Style144{}Style145{Font:Verdana, 8.25pt;AlignH" +
				"orz:Near;}Style43{Font:Verdana, 8.25pt;AlignHorz:Near;}Style42{}Style38{}Style39" +
				"{}Style36{}FilterBar{BackColor:SeaShell;}Style37{Font:Verdana, 8.25pt;AlignHorz:" +
				"Near;}Style148{AlignHorz:Near;}Style149{AlignHorz:Near;}Normal{Font:Verdana, 8pt" +
				";BackColor:Ivory;}Style34{AlignHorz:Near;}Style49{Font:Verdana, 8.25pt;AlignHorz" +
				":Near;}Style48{}Style31{Font:Verdana, 8.25pt;AlignHorz:Near;}Style35{AlignHorz:N" +
				"ear;}Style32{}Style33{}Style142{AlignHorz:Near;}Style143{AlignHorz:Near;}Style41" +
				"{AlignHorz:Near;}Style40{AlignHorz:Near;}Style146{}Style147{}Style45{}Style44{}S" +
				"tyle47{AlignHorz:Far;}Style46{AlignHorz:Near;}Style30{}EvenRow{BackColor:Aqua;}S" +
				"tyle51{}Style163{Font:Verdana, 8.25pt;AlignHorz:Near;}Style5{}Style4{}Style7{}St" +
				"yle6{}Style58{AlignHorz:Near;}Style59{AlignHorz:Far;}Style3{}Style2{}Style50{}Fo" +
				"oter{}Style52{AlignHorz:Near;}Style53{AlignHorz:Far;}Style54{}Style55{Font:Verda" +
				"na, 8.25pt;AlignHorz:Near;}Style56{}Style57{}Style119{AlignHorz:Far;}Style67{Fon" +
				"t:Verdana, 8.25pt;AlignHorz:Near;}Caption{AlignHorz:Center;}Style61{Font:Verdana" +
				", 8.25pt;AlignHorz:Near;}Style60{}Style69{}Style68{}Style162{}Style1{}Style160{A" +
				"lignHorz:Near;}Style161{AlignHorz:Near;}Style63{}Style62{}Style164{}Style165{}St" +
				"yle100{AlignHorz:Near;}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near;}" +
				"Style114{}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}" +
				"</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyl" +
				"e=\"Always\" AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"\" AllowRowSizing=\"N" +
				"one\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendR" +
				"ightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"SolidCell" +
				"Border\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Hor" +
				"izontalScrollGroup=\"1\"><Caption>Borrows</Caption><CaptionStyle parent=\"Style2\" m" +
				"e=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"E" +
				"venRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterS" +
				"tyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><He" +
				"adingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRo" +
				"w\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle par" +
				"ent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Styl" +
				"e11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=" +
				"\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e160\" /><Style parent=\"Style1\" me=\"Style161\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style162\" /><EditorStyle parent=\"Style5\" me=\"Style163\" /><GroupHeaderStyle paren" +
				"t=\"Style1\" me=\"Style165\" /><GroupFooterStyle parent=\"Style1\" me=\"Style164\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</He" +
				"ight><Locked>True</Locked><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Width>85</Width><Height>15</Height><Button>True</Button><DCIdx>" +
				"21</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle94\" /><Style parent=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style96\" /><EditorStyle parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style99\" /><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width>" +
				"<Height>15</Height><Locked>True</Locked><DCIdx>11</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" " +
				"me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>D" +
				"arkGray,Single</ColumnDivider><Width>85</Width><Height>15</Height><Button>True</" +
				"Button><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>95</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style" +
				"41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>75</Width><Height>15</Height><AllowFocus>False</Allo" +
				"wFocus><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>90</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style" +
				"47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>95</Width><Height>15</Height><AllowFocus>False</Allo" +
				"wFocus><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
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
				"Cols><ClientRect>0, 0, 1046, 201</ClientRect><BorderSide>0</BorderSide></C1.Win." +
				"C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Sty" +
				"le parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style p" +
				"arent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style pa" +
				"rent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent" +
				"=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style paren" +
				"t=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style pa" +
				"rent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyle" +
				"s><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><" +
				"DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1046, 201</ClientAre" +
				"a><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\"" +
				" me=\"Style15\" /></Blob>";
			// 
			// DealCreditControl
			// 
			this.DealCreditControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DealCreditControl.Location = new System.Drawing.Point(680, 0);
			this.DealCreditControl.Name = "DealCreditControl";
			this.DealCreditControl.Size = new System.Drawing.Size(348, 108);
			this.DealCreditControl.TabIndex = 6;
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.StatusLabel.Location = new System.Drawing.Point(20, 544);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(1124, 16);
			this.StatusLabel.TabIndex = 23;
			this.StatusLabel.Tag = null;
			this.StatusLabel.TextDetached = true;
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
																						   this.SendToProcessAgentMenuItem,
																						   this.SendToEmailMenuItem,
																						   this.SendToTrashBinMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToProcessAgentMenuItem
			// 
			this.SendToProcessAgentMenuItem.Index = 0;
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
			this.SendToTrashBinMenuItem.Index = 2;
			this.SendToTrashBinMenuItem.Text = "Trash Bin";
			this.SendToTrashBinMenuItem.Click += new System.EventHandler(this.SendToTrashBinMenuItem_Click);
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
			// DockNoneMenuItem
			// 
			this.DockNoneMenuItem.Index = 2;
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
			// BookGroupDropDown
			// 
			this.BookGroupDropDown.AllowColMove = true;
			this.BookGroupDropDown.AllowColSelect = true;
			this.BookGroupDropDown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
			this.BookGroupDropDown.AlternatingRows = false;
			this.BookGroupDropDown.CaptionHeight = 17;
			this.BookGroupDropDown.ColumnCaptionHeight = 17;
			this.BookGroupDropDown.ColumnFooterHeight = 17;
			this.BookGroupDropDown.ExtendRightColumn = true;
			this.BookGroupDropDown.FetchRowStyles = false;
			this.BookGroupDropDown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.BookGroupDropDown.Location = new System.Drawing.Point(80, 208);
			this.BookGroupDropDown.Name = "BookGroupDropDown";
			this.BookGroupDropDown.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BookGroupDropDown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.BookGroupDropDown.RowHeight = 15;
			this.BookGroupDropDown.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BookGroupDropDown.ScrollTips = false;
			this.BookGroupDropDown.Size = new System.Drawing.Size(350, 304);
			this.BookGroupDropDown.TabIndex = 50;
			this.BookGroupDropDown.TabStop = false;
			this.BookGroupDropDown.Visible = false;
			this.BookGroupDropDown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book Group\"" +
				" DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Book Name\" DataField=\"BookName\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\">" +
				"<Data>Caption{AlignHorz:Center;}Normal{Font:Verdana, 8pt;}Style25{}Selected{Fore" +
				"Color:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{Align" +
				"Horz:Near;}Style15{AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:Near;}Sty" +
				"le11{}OddRow{}Style13{}Style12{}Footer{}HighlightRow{ForeColor:HighlightText;Bac" +
				"kColor:Highlight;}RecordSelector{AlignImage:Center;}Style24{}Style23{}Style22{}S" +
				"tyle21{AlignHorz:Near;}Style20{AlignHorz:Near;}Inactive{ForeColor:InactiveCaptio" +
				"nText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;Align" +
				"Vert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}F" +
				"ilterBar{}Style4{}Style9{}Style8{}Style5{}Group{BackColor:ControlDark;Border:Non" +
				"e,,0, 0, 0, 0;AlignVert:Center;}Style7{}Style6{}Style1{}Style3{}Style2{}</Data><" +
				"/Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView Name=\"\" CaptionHeight=\"17\" Col" +
				"umnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" MarqueeSt" +
				"yle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" RecordSelect" +
				"ors=\"False\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle pare" +
				"nt=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowSt" +
				"yle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style1" +
				"3\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"S" +
				"tyle12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent" +
				"=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><Od" +
				"dRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelec" +
				"tor\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent" +
				"=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>" +
				"85</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle pa" +
				"rent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Width>150</Width><Height>15</Height><DCIdx>1</DCIdx></C1Displ" +
				"ayColumn></internalCols><ClientRect>0, 0, 346, 300</ClientRect><BorderSide>0</Bo" +
				"rderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent=" +
				"\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" m" +
				"e=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"" +
				"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Ed" +
				"itor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"Ev" +
				"enRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Record" +
				"Selector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"" +
				"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layo" +
				"ut>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0," +
				" 346, 300</ClientArea></Blob>";
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
			this.BooksDropDown.ExtendRightColumn = true;
			this.BooksDropDown.FetchRowStyles = false;
			this.BooksDropDown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.BooksDropDown.Location = new System.Drawing.Point(456, 208);
			this.BooksDropDown.Name = "BooksDropDown";
			this.BooksDropDown.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BooksDropDown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.BooksDropDown.RowHeight = 15;
			this.BooksDropDown.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BooksDropDown.ScrollTips = false;
			this.BooksDropDown.Size = new System.Drawing.Size(350, 304);
			this.BooksDropDown.TabIndex = 49;
			this.BooksDropDown.TabStop = false;
			this.BooksDropDown.ValueTranslate = true;
			this.BooksDropDown.Visible = false;
			this.BooksDropDown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Book Name\" DataField=\"BookName\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Capti" +
				"on{AlignHorz:Center;}Normal{Font:Verdana, 8pt;}Style25{}Selected{ForeColor:Highl" +
				"ightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{AlignHorz:Near;}" +
				"Style15{AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}OddRo" +
				"w{}Style13{}Style12{}Footer{}HighlightRow{ForeColor:HighlightText;BackColor:High" +
				"light;}RecordSelector{AlignImage:Center;}Style24{}Style23{}Style22{}Style21{Alig" +
				"nHorz:Near;}Style20{AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;BackC" +
				"olor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center" +
				";Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}FilterBar{}S" +
				"tyle4{}Style9{}Style8{}Style5{}Group{BackColor:ControlDark;Border:None,,0, 0, 0," +
				" 0;AlignVert:Center;}Style7{}Style6{}Style1{}Style3{}Style2{}</Data></Styles><Sp" +
				"lits><C1.Win.C1TrueDBGrid.DropdownView AllowColSelect=\"False\" Name=\"\" AllowRowSi" +
				"zing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" " +
				"ExtendRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16" +
				"\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" Horizontal" +
				"ScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent" +
				"=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarS" +
				"tyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" " +
				"/><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"S" +
				"tyle2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle p" +
				"arent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><Recor" +
				"dSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Sel" +
				"ected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=" +
				"\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>Dark" +
				"Gray,Single</ColumnDivider><Width>85</Width><Height>15</Height><DCIdx>0</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><" +
				"Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /" +
				"><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Vi" +
				"sible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>150</Width><Height>15" +
				"</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 346," +
				" 300</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView></" +
				"Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"He" +
				"ading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Capti" +
				"on\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selecte" +
				"d\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRo" +
				"w\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" />" +
				"<Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterB" +
				"ar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSpli" +
				"ts><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</De" +
				"faultRecSelWidth><ClientArea>0, 0, 346, 300</ClientArea></Blob>";
			// 
			// ShowContractsCheckBox
			// 
			this.ShowContractsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ShowContractsCheckBox.Location = new System.Drawing.Point(712, 108);
			this.ShowContractsCheckBox.Name = "ShowContractsCheckBox";
			this.ShowContractsCheckBox.Size = new System.Drawing.Size(160, 24);
			this.ShowContractsCheckBox.TabIndex = 51;
			this.ShowContractsCheckBox.Text = "Show Booked Contracts";
			// 
			// dealsInformationControl
			// 
			this.dealsInformationControl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dealsInformationControl.Location = new System.Drawing.Point(16, 8);
			this.dealsInformationControl.Name = "dealsInformationControl";
			this.dealsInformationControl.Size = new System.Drawing.Size(592, 116);
			this.dealsInformationControl.TabIndex = 52;
			// 
			// PositionDealBlotterForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 13);
			this.ClientSize = new System.Drawing.Size(1052, 561);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.dealsInformationControl);
			this.Controls.Add(this.ShowContractsCheckBox);
			this.Controls.Add(this.BookGroupDropDown);
			this.Controls.Add(this.BooksDropDown);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.DealCreditControl);
			this.Controls.Add(this.BorrowsGrid);
			this.Controls.Add(this.LoansGrid);
			this.Controls.Add(this.GridSplitter);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 132;
			this.Font = new System.Drawing.Font("Verdana", 8F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionDealBlotterForm";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PositionDealBlotterForm_Closing);
			this.Load += new System.EventHandler(this.PositionDealBlotterForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BorrowsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupDropDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BooksDropDown)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void PositionDealBlotterForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.Top    = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
				this.Left   = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height","480"));
				this.Width  = int.Parse(RegistryValue.Read(this.Name, "Width", "1176"));
	      
				this.Text = TEXT;
	      
				//string bookRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND BookGroup  <> Book";
	      
				this.Show();
				this.Cursor = Cursors.WaitCursor;
				Application.DoEvents();
	      
				mainForm.Alert("Please wait... Loading the deal blotter.", PilotState.Idle);

				BorrowsGrid.AllowUpdate     = true;
				BorrowsGrid.AllowAddNew     = true;

				LoansGrid.AllowUpdate       = true;
				LoansGrid.AllowAddNew       = true;

				//SendToProcessAgentMenuItem.Enabled  = false;
				//SendToTrashBinMenuItem.Enabled      = false;
				//BooksDropDown.Enabled               = false;

				mainForm.PositionAgent.DealEvent += new DealEventHandler(dealEventWrapper.DoEvent); 
				
				dataSet = mainForm.PositionAgent.DealDataGet(mainForm.UtcOffset, "", mainForm.UserId, "PositionDealBlotter", true.ToString());                    
				// dataSet = mainForm.PositionAgent.DealDataGet(mainForm.UtcOffset, DEAL_ID_PREFIX, mainForm.UserId, "PositionDealBlotter", true.ToString());                    
    
				borrowsDataView = new DataView(dataSet.Tables["Deals"], borrowsRowFilter, "", DataViewRowState.CurrentRows);  
				borrowsDataView.Sort = "DealId";

				BorrowsGrid.SetDataBinding(borrowsDataView, null, true);

				loansDataView = new DataView(dataSet.Tables["Deals"], loansRowFilter, "", DataViewRowState.CurrentRows);                  
				loansDataView.Sort = "DealId";

				LoansGrid.SetDataBinding(loansDataView, null, true);
				// -----------------------

				bookDataView = new DataView(dataSet.Tables["Books"]);
				bookDataView.Sort = "Book";
                
				BooksDropDown.SetDataBinding(bookDataView, null, true);

				BookGroupDropDown.SetDataBinding(dataSet.Tables["BookGroups"], null, true);

				mainForm.Alert("Loading the deal blotter... Done!", PilotState.Normal);
				
				// -- Updated by Yasir Bashir 7/24/2007
				dealsInformationControl.MainForm = mainForm;
				DealCreditControl.InitializeCreditControl(mainForm);
				// --
				
				this.IsReady = true;      
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				mainForm.Alert("Loading the deal blotter... Error!", PilotState.RunFault);

				Log.Write(error.Message + ". [PositionDealBlotterForm.PositionDealBlotterForm_Load]", Log.Error, 1);               
			}      
            
			this.Cursor = Cursors.Default;          
		}

		private void PositionDealBlotterForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				if(this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
				{
					RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
					RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
					RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
					RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
				} 

				if (dealEventWrapper != null)
				{      
					mainForm.PositionAgent.DealEvent -= new DealEventHandler(dealEventWrapper.DoEvent);
					dealEventWrapper.DealEvent       -= new DealEventHandler(DealOnEvent);
					dealEventWrapper                  = null; 
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionDealBlotterForm.PositionDealBlotterForm_Closing]", Log.Error, 1); 
			}

			mainForm.positionDealBlotterForm = null;
		}

		private void BorrowsGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref BorrowsGrid);

				if (gridFilter.Equals(""))
				{
					borrowsDataView.RowFilter = borrowsRowFilter;
					loansDataView.RowFilter = loansRowFilter;
				}
				else
				{
					borrowsDataView.RowFilter = borrowsRowFilter + " And " + gridFilter;
					loansDataView.RowFilter = loansRowFilter + " And " + gridFilter;
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}

			FormStatusSet();
		}

		private void LoansGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref LoansGrid);

				if (gridFilter.Equals(""))
				{
					borrowsDataView.RowFilter = borrowsRowFilter;
					loansDataView.RowFilter = loansRowFilter;
				}
				else
				{
					borrowsDataView.RowFilter = borrowsRowFilter + " And " + gridFilter;
					loansDataView.RowFilter = loansRowFilter + " And " + gridFilter;
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}

			FormStatusSet();
		}

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			try
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
			catch (Exception ee)
			{
				Log.Write(ee.Message + ". [PositionDealBlotterForm.FormatText]", Log.Error, 1);
			}

		}
    
		private void SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			FormStatusSet();
		}

		private void BorrowsGrid_OnAddNew(object sender, System.EventArgs e)
		{
			try
			{
				BorrowsGrid.Columns["DealId"].Value = mainForm.ServiceAgent.NewProcessId(DEAL_ID_PREFIX);
				BorrowsGrid.Columns["BookGroup"].Value = _bookGroup;
				BorrowsGrid.Columns["DealType"].Value = "B";
	      
				BorrowsGrid.Columns["Book"].Value = _book;
				BorrowsGrid.Columns["CollateralCode"].Value = _collateralCode.Trim().Equals("") ? "C" : _collateralCode;
				BorrowsGrid.Columns["PoolCode"].Value = _poolCode;
				BorrowsGrid.Columns["IncomeTracked"].Value = true;
				BorrowsGrid.Columns["DivRate"].Value = 100.000;
				BorrowsGrid.Columns["DealStatus"].Value = "N";

				BookMarginSet(ref BorrowsGrid, "Borrow");
			}
			catch (Exception ee)
			{
				Log.Write(ee.Message + ". [PositionDealBlotterForm.BorrowsGrid_OnAddNew]", Log.Error, 1);
			}
		}

		private void LoansGrid_OnAddNew(object sender, System.EventArgs e)
		{
			try
			{
				this.isReady = false;

				LoansGrid.Columns["DealId"].Value = mainForm.ServiceAgent.NewProcessId(DEAL_ID_PREFIX);
				LoansGrid.Columns["BookGroup"].Value = _bookGroup;
				LoansGrid.Columns["DealType"].Value = "L";
	      
				LoansGrid.Columns["Book"].Value = _book;
				LoansGrid.Columns["CollateralCode"].Value = _collateralCode.Trim().Equals("") ? "C" : _collateralCode;
				LoansGrid.Columns["PoolCode"].Value = _poolCode;
				LoansGrid.Columns["IncomeTracked"].Value = true;
				LoansGrid.Columns["DivRate"].Value = 100.000;
				LoansGrid.Columns["DealStatus"].Value = "N";

				BookMarginSet(ref LoansGrid, "Loan");
			}
			catch (Exception ee)
			{
				Log.Write(ee.Message + ". [PositionDealBlotterForm.LoansGrid_OnAddNew]", Log.Error, 1);
			}
		}

		private void BorrowsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			KeyPressEvent(ref BorrowsGrid, ref e);
		}

		private void LoansGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			KeyPressEvent(ref LoansGrid, ref e);
		}

		private void KeyPressEvent(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid, ref System.Windows.Forms.KeyPressEventArgs e)
		{
			string gridData = "";

			try
			{
				if (e.KeyChar.Equals((char)13) && (dealsGrid.Col > 5) && dealsGrid.DataChanged)
				{
					dealsGrid.UpdateData();
					dealsGrid.MoveNext();
					// -- Updated by Yasir Bashir on 8/2/2007
					dealsGrid.Col = 1;
					// -- 

					e.Handled = true;
				}

				if (e.KeyChar.Equals((char)13) && !dealsGrid.DataChanged)
				{
					// -- Updated by Yasir Bashir on 8/2/2007
					dealsGrid.MoveLast();
					dealsGrid.Row += 1;
					dealsGrid.Col = 2;
					dealsGrid.Select();
					dealsGrid.Columns["BookGroup"].Value = _bookGroup;
					dealsGrid.Columns["Book"].Value = null;
					// -- 

					e.Handled = true;
				}

				if (e.KeyChar.Equals((char)27))
				{
					if (!dealsGrid.EditActive && dealsGrid.DataChanged)
					{
						dealsGrid.DataChanged = false;
					}
				}

				if (e.KeyChar.Equals((char)3) && dealsGrid.SelectedRows.Count > 0)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in dealsGrid.SelectedCols)
					{
						gridData += dataColumn.Caption + "\t";
					}

					gridData += "\n";

					foreach (int row in dealsGrid.SelectedRows)
					{
						foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in dealsGrid.SelectedCols)
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
					mainForm.Alert("Copied " + dealsGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
					e.Handled = true;
				}

			}
			catch (Exception ee)
			{
				Log.Write(ee.Message + ". [PositionDealBlotterForm.KeyPressEvent]", Log.Error, 1);
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}
		}

		private void BorrowsGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			try
			{
				this.isReady = false;

				bookDataView.RowFilter = "BookGroup = '" + BorrowsGrid.Columns["BookGroup"].Text + "'";  

				if (BorrowsGrid.FilterActive)
				{
					e.Cancel = false;
					return;
				}
	      
				if (BorrowsGrid.Columns["DealStatus"].Value.ToString().Equals("P"))
				{
					e.Cancel = true;
				}
			}
			catch (Exception ee)
			{
				Log.Write(ee.Message + ". [PositionDealBlotterForm.BorrowsGrid_BeforeColEdit]", Log.Error, 1);
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}
		}

		private void LoansGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			try
			{

				bookDataView.RowFilter = "BookGroup = '" + LoansGrid.Columns["BookGroup"].Text + "'";  

				if (LoansGrid.FilterActive)
				{
					e.Cancel = false;
					return;
				}
	      
				if (LoansGrid.Columns["DealStatus"].Value.ToString().Equals("P"))
				{
					e.Cancel = true;
					return;
				}
			}
			catch (Exception ee)
			{
				Log.Write(ee.Message + ". [PositionDealBlotterForm.LoansGrid_BeforeColEdit]", Log.Error, 1);
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}
		}

		private void BorrowsGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			BeforeColUpdate(ref BorrowsGrid, ref e, "Borrow");
		}

		private void LoansGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			BeforeColUpdate(ref LoansGrid, ref e, "Loan");
		}

		private void BeforeColUpdate(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid dealsGrid, ref C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e, string dealType)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				switch (e.Column.DataColumn.DataField)
				{
					case "SecId" :
						mainForm.SecId = dealsGrid.Columns["SecId"].Text;
            
						dealsGrid.Columns["SecId"].Text = mainForm.SecId;       
						dealsGrid.Columns["Symbol"].Text = mainForm.Symbol;
       
						BookTableRateSet(ref dealsGrid, dealType);
						BookMarginSet(ref dealsGrid, dealType);
						AmountSet(ref dealsGrid);
						break;

					case "Rate" :
						if (dealsGrid.Columns["Rate"].Text.Trim().Equals(""))
						{
							BookTableRateSet(ref dealsGrid, dealType);
						}
						else
						{
							string rate = dealsGrid.Columns["Rate"].Text.ToUpper().Replace(" ", "");

							if (rate.EndsWith("NEG"))
							{
								rate = "-" + rate.Replace("NEG", "").Replace("-", "");
							}

							if (rate.EndsWith("N"))
							{
								rate = "-" + rate.Replace("N", "").Replace("-", "");
							}

							rate = rate.Replace("NEG", "-");
							rate = rate.Replace("N", "-");
              
							rate = rate.Replace("7/8", ".875");
							rate = rate.Replace("7/", ".875");
							rate = rate.Replace("3/4", ".75");
							rate = rate.Replace("5/8", ".625");
							rate = rate.Replace("5/", ".625");
							rate = rate.Replace("1/2", ".5");
							rate = rate.Replace("3/8", ".375");
							rate = rate.Replace("1/4", ".25");
							rate = rate.Replace("1/8", ".125");

							dealsGrid.Columns["Rate"].Text = rate;

							dealsGrid.Columns["RateCode"].Text = " ";
						}
						break;

					case "Book" :
						BookTableRateSet(ref dealsGrid, dealType);
						BookMarginSet(ref dealsGrid, dealType);
						AmountSet(ref dealsGrid);
						break;

					case "Quantity" :
						string quantity = dealsGrid.Columns["Quantity"].Text.ToUpper().Replace(" ", "");

						quantity = quantity.Replace("M", "000").Replace("K", "000").Replace("T", "000").Replace("G", "000");
						quantity = quantity.Replace("C", "00").Replace("H", "00");
            
						dealsGrid.Columns["Quantity"].Text = quantity;

						AmountSet(ref dealsGrid);
						break;

					case "Amount" :
						string amount = dealsGrid.Columns["Amount"].Text.ToUpper();

						if (amount.StartsWith("P") || amount.EndsWith("P"))
						{
							amount = amount.Trim('P');

							if (Tools.IsNumeric(amount))
							{
								dealsGrid.Columns["Amount"].Value = decimal.Parse(amount) * (long)dealsGrid.Columns["Quantity"].Value;
							}
						}
						break;

					case "Margin" :
						if (dealsGrid.Columns["Margin"].Text.Trim().Equals(""))
						{
							BookMarginSet(ref dealsGrid, dealType);
						}
						else
						{
							dealsGrid.Columns["MarginCode"].Text = "%";
						}
            
						AmountSet(ref dealsGrid);
						break;
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
				Log.Write(ee.Message + ". [PositionDealBlotterForm.BeforeColUpdate]", Log.Error, 1);
			}

			this.Cursor = Cursors.Default;
		}

		private void BorrowsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				if (BorrowsGrid.Columns["SecId"].Text.Equals("") || BorrowsGrid.Columns["Quantity"].Text.Equals(""))          
				{
					mainForm.Alert("Values for a Security ID and Quantity are required.", PilotState.RunFault);
					e.Cancel = true;
				}
				else
				{
					_firstRowBorrows = BorrowsGrid.FirstRow;
					_firstRowLoans = LoansGrid.FirstRow;

					_dealId = BorrowsGrid.Columns["DealId"].Text;
					_bookGroup = BorrowsGrid.Columns["BookGroup"].Text;
					_dealType = BorrowsGrid.Columns["DealType"].Text;
					_book = BorrowsGrid.Columns["Book"].Text;
					_secId = BorrowsGrid.Columns["SecId"].Text.Trim();
					_quantity = BorrowsGrid.Columns["Quantity"].Value.ToString();
					_amount = BorrowsGrid.Columns["Amount"].Value.ToString();
					_collateralCode = BorrowsGrid.Columns["CollateralCode"].Text;
					_rate = BorrowsGrid.Columns["Rate"].Value.ToString();
					_rateCode = BorrowsGrid.Columns["RateCode"].Text;
					_poolCode = BorrowsGrid.Columns["PoolCode"].Text;
					_divRate = BorrowsGrid.Columns["DivRate"].Value.ToString();
					_incomeTracked = BorrowsGrid.Columns["IncomeTracked"].Value.ToString();
					_margin = BorrowsGrid.Columns["Margin"].Value.ToString();
					_marginCode = BorrowsGrid.Columns["MarginCode"].Value.ToString();
					_comment = BorrowsGrid.Columns["Comment"].Text;
					_dealStatus = BorrowsGrid.Columns["DealStatus"].Text;

					BorrowsGrid.Columns["ActUserShortName"].Text = "me";
					BorrowsGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateTimeFileFormat);

					mainForm.Alert("Updating borrow deal in " + _secId + " for quantity of " + _quantity + "...", PilotState.Unknown); 
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
				Log.Write(ee.Message + ". [PositionDealBlotterForm.BorrowsGrid_BeforeUpdate]", Log.Error, 1);
			}
		}

		private void LoansGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				if (LoansGrid.Columns["SecId"].Text.Equals("") || LoansGrid.Columns["Quantity"].Text.Equals(""))          
				{
					mainForm.Alert("Values for a Security ID and Quantity are required.", PilotState.RunFault);
					e.Cancel = true;
				}
				else
				{
					_firstRowBorrows = BorrowsGrid.FirstRow;
					_firstRowLoans = LoansGrid.FirstRow;

					_dealId = LoansGrid.Columns["DealId"].Text;
					_bookGroup = LoansGrid.Columns["BookGroup"].Text;
					_dealType = LoansGrid.Columns["DealType"].Text;
					_book = LoansGrid.Columns["Book"].Text;
					_secId = LoansGrid.Columns["SecId"].Text.Trim();
					_quantity = LoansGrid.Columns["Quantity"].Value.ToString();
					_amount = LoansGrid.Columns["Amount"].Value.ToString();
					_collateralCode = LoansGrid.Columns["CollateralCode"].Text;
					_rate = LoansGrid.Columns["Rate"].Value.ToString();
					_rateCode = LoansGrid.Columns["RateCode"].Text;
					_poolCode = LoansGrid.Columns["PoolCode"].Text;
					_divRate = LoansGrid.Columns["DivRate"].Value.ToString();
					_incomeTracked = LoansGrid.Columns["IncomeTracked"].Value.ToString();
					_margin = LoansGrid.Columns["Margin"].Value.ToString();
					_marginCode = LoansGrid.Columns["MarginCode"].Value.ToString();
					_comment = LoansGrid.Columns["Comment"].Text;
					_dealStatus = LoansGrid.Columns["DealStatus"].Text;

					LoansGrid.Columns["ActUserShortName"].Text = "me";
					LoansGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateTimeFileFormat);

					mainForm.Alert("Updating loan deal in " + _secId + " for quantity of " + _quantity + "...", PilotState.Unknown);
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
				Log.Write(ee.Message + ". [PositionDealBlotterForm.BorrowsGrid_BeforeUpdate]", Log.Error, 1);
			}
		}

		private void Grid_AfterUpdate(object sender, System.EventArgs e)
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
						"D",
						mainForm.UserId,
						true
						);

					// -- Added by Yasir Bashir on 8/2/2007
					DealEventArgs dealEventArgs = new DealEventArgs(
						_dealId,
						_bookGroup,
						(_dealType.Equals("B") ? "B" : "L"),
						"",
						"",
						_secId,
						mainForm.Symbol,
						_quantity,
						_amount,
						_collateralCode,
						"",
						"",
						"",
						"",
						"",
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
						"D",
						mainForm.UserId,
						DateTime.Now.ToString(Standard.DateTimeFileFormat),
						true
						);
					 dataSet.Tables["Deals"].BeginLoadData();
					 dataSet.Tables["Deals"].LoadDataRow(dealEventArgs.Values, true);
					 dataSet.Tables["Deals"].EndLoadData(); 
					// ----

					mainForm.Alert("Deal update is done!"); 
				}      
			}
			catch (Exception error)
			{
				mainForm.Alert("Deal update error: " + error.Message, PilotState.RunFault); 
				Log.Write(error.Message + " [PositionDealBlotterForm.Grid_AfterUpdate]", Log.Error, 1);
			}

			BorrowsGrid.FirstRow = _firstRowBorrows;
			LoansGrid.FirstRow = _firstRowLoans;
			
			this.isReady = (!this.isReady) ? true : false;
		}

		private void Grid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
		{
			e.Handled = true;
		}

		private void BorrowsGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				if (e.X <= BorrowsGrid.RecordSelectorWidth && e.Y <= BorrowsGrid.RowHeight)
				{
					if (BorrowsGrid.SelectedRows.Count.Equals(0))
					{
						for (int i = 0; i < BorrowsGrid.Splits[0,0].Rows.Count; i++)
						{
							BorrowsGrid.SelectedRows.Add(i);
						}

						foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BorrowsGrid.Columns)
						{
							BorrowsGrid.SelectedCols.Add(dataColumn);
						}
					}
					else
					{
						BorrowsGrid.SelectedRows.Clear();
						BorrowsGrid.SelectedCols.Clear();
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert("Deal update error: " + error.Message, PilotState.RunFault); 
				Log.Write(error.Message + " [PositionDealBlotterForm.BorrowsGrid_MouseDown]", Log.Error, 1);
			}

		}

		private void LoansGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				if (e.X <= LoansGrid.RecordSelectorWidth && e.Y <= LoansGrid.RowHeight)
				{
					if (LoansGrid.SelectedRows.Count.Equals(0))
					{
						for (int i = 0; i < LoansGrid.Splits[0,0].Rows.Count; i++)
						{
							LoansGrid.SelectedRows.Add(i);
						}

						foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LoansGrid.Columns)
						{
							LoansGrid.SelectedCols.Add(dataColumn);
						}
					}
					else
					{
						LoansGrid.SelectedRows.Clear();
						LoansGrid.SelectedCols.Clear();
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert("Deal update error: " + error.Message, PilotState.RunFault); 
				Log.Write(error.Message + " [PositionDealBlotterForm.LoansGrid_MouseDown]", Log.Error, 1);
			}
		}

		private void BorrowsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			try
			{
				switch (BorrowsGrid.Columns["DealStatus"].CellText(e.Row))
				{
					case "S" :
						e.CellStyle.BackColor = Color.LightSteelBlue;
						break;
					case "E" :
						e.CellStyle.BackColor = Color.LightCoral;
						break;
					default :
						e.CellStyle.BackColor = Color.Ivory;
						break;
				} 
			}
			catch (Exception error)
			{
				mainForm.Alert("Deal update error: " + error.Message, PilotState.RunFault); 
				Log.Write(error.Message + " [PositionDealBlotterForm.BorrowsGrid_FetchRowStyle]", Log.Error, 1);
			}
		}

		private void LoansGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			try
			{
				switch (LoansGrid.Columns["DealStatus"].CellText(e.Row))
				{
					case "S" :
						e.CellStyle.BackColor = Color.LightSteelBlue;
						break;
					case "E" :
						e.CellStyle.BackColor = Color.LightCoral;
						break;
					default :
						e.CellStyle.BackColor = Color.Honeydew;
						break;
				} 
			}
			catch (Exception error)
			{
				mainForm.Alert("Deal update error: " + error.Message, PilotState.RunFault); 
				Log.Write(error.Message + " [PositionDealBlotterForm.LoansGrid_FetchRowStyle]", Log.Error, 1);
			}

		}

		private void BorrowsGrid_ColResize(object sender, C1.Win.C1TrueDBGrid.ColResizeEventArgs e)
		{
			LoansGrid.Splits[0,0].DisplayColumns[e.ColIndex].Width = BorrowsGrid.Splits[0,0].DisplayColumns[e.ColIndex].Width;
		}

		private void LoansGrid_ColResize(object sender, C1.Win.C1TrueDBGrid.ColResizeEventArgs e)
		{
			BorrowsGrid.Splits[0,0].DisplayColumns[e.ColIndex].Width = LoansGrid.Splits[0,0].DisplayColumns[e.ColIndex].Width;    
		}

		private void SendToProcessAgentMenuItem_Click(object sender, System.EventArgs e)
		{
			if (BorrowsGrid.Focused)
			{
				ProcessDeals(ref BorrowsGrid);
			}

			if (LoansGrid.Focused)
			{
				ProcessDeals(ref LoansGrid);
			}    
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			if (BorrowsGrid.Focused)
			{
				EmailDeals(ref BorrowsGrid);
			}

			if (LoansGrid.Focused)
			{
				EmailDeals(ref LoansGrid);
			}
		}

		private void SendToTrashBinMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (BorrowsGrid.Focused)
				{
					DeleteDeals(ref BorrowsGrid);
				}

				if (LoansGrid.Focused)
				{
					DeleteDeals(ref LoansGrid);
				}
			}
			catch (Exception error)
			{
				mainForm.Alert("Deal update error: " + error.Message, PilotState.RunFault); 
				Log.Write(error.Message + " [PositionDealBlotterForm.SendToTrashBinMenuItem_Click]", Log.Error, 1);
			}

		}

		private void ShowOtherBookMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowOtherBookMenuItem.Checked = !ShowOtherBookMenuItem.Checked;
			BorrowsGrid.Splits[0,0].DisplayColumns["OtherBook"].Visible = ShowOtherBookMenuItem.Checked;    
			LoansGrid.Splits[0,0].DisplayColumns["OtherBook"].Visible = ShowOtherBookMenuItem.Checked;    
		}

		private void ShowDivRateMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowDivRateMenuItem.Checked = !ShowDivRateMenuItem.Checked;
			BorrowsGrid.Splits[0,0].DisplayColumns["DivRate"].Visible = ShowDivRateMenuItem.Checked;    
			LoansGrid.Splits[0,0].DisplayColumns["DivRate"].Visible = ShowDivRateMenuItem.Checked;    
		}

		private void ShowIncomeTrackedMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowIncomeTrackedMenuItem.Checked = !ShowIncomeTrackedMenuItem.Checked;
			BorrowsGrid.Splits[0,0].DisplayColumns["IncomeTracked"].Visible = ShowIncomeTrackedMenuItem.Checked;    
			LoansGrid.Splits[0,0].DisplayColumns["IncomeTracked"].Visible = ShowIncomeTrackedMenuItem.Checked;    
		}

		private void DockTopMenuItem_Click(object sender, System.EventArgs e)
		{
			RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
			RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
			RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
			RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
      
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

		private void BorrowsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			GridPaint();
		}

		private void LoansGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			GridPaint();
		}

		private void GridPaint()
		{
			try
			{
				if (BorrowsGrid.Focused)
				{
					if (!BorrowsGrid.Columns["SecId"].Text.Equals(""))
					{
						// -- Added by Yasir Bashir on 7/24/2007
						dealsInformationControl.LoadData(BorrowsGrid.Columns["BookGroup"].Text, BorrowsGrid.Columns["SecId"].Text);
						// -- Added by Yasir Bashir on 8/10/2007
						DealCreditControl.BookGroup = BorrowsGrid.Columns["BookGroup"].Text;
						DealCreditControl.Book = BorrowsGrid.Columns["Book"].Text;
						DealCreditControl.CalculateTotals();
						// --
					}
				}
				else if (LoansGrid.Focused)
				{
					if (!LoansGrid.Columns["SecId"].Text.Equals(""))
					{
						// -- Added by Yasir Bashir on 7/24/2007
						dealsInformationControl.LoadData(LoansGrid.Columns["BookGroup"].Text, LoansGrid.Columns["SecId"].Text);
						// -- Added by Yasir Bashir on 8/10/2007
						DealCreditControl.BookGroup = LoansGrid.Columns["BookGroup"].Text;
						DealCreditControl.Book = LoansGrid.Columns["Book"].Text;
						DealCreditControl.CalculateTotals();
						// -- 
					}
				}

				if (BorrowsGrid.Focused)
				{
					mainForm.SecId = BorrowsGrid.Columns["SecId"].Text;
				}
				else if (LoansGrid.Focused)
				{
					mainForm.SecId = LoansGrid.Columns["SecId"].Text;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault); 
				Log.Write(error.Message + " [PositionDealBlotterForm.GridPaint]", Log.Error, 1);

			}
		}

	}
}
