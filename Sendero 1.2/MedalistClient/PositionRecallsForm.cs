// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using Anetics.Common;


namespace Anetics.Medalist
{
	public class PositionRecallsForm : System.Windows.Forms.Form
	{
		private const string TEXT = "Position - Recalls";    
    
		private MainForm mainForm;    
		private DataSet dataSet;        
		private ArrayList recallEventArgsArray;
		private RecallEventWrapper recallEventWrapper = null;
		private RecallEventHandler recallEventHandler = null;
		private DataView recallDataView, recallActivityDataView;
		private string recallRowFilter, recallActivityRowFilter;    
		private bool isReady = false, isFaxEnabled = false, isHistory = false;    
		private string secId = "";

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid RecallsGrid;
		private C1.Win.C1Input.C1Label BookGroupNameLabel;
		private C1.Win.C1Input.C1Label BookGroupLabel;
		private C1.Win.C1List.C1Combo BookGroupCombo;
		private System.ComponentModel.Container components = null;    
		private System.Windows.Forms.ContextMenu RecallsContextMenu;
		private C1.Win.C1Input.C1Label BizDateLabel;
		private C1.Win.C1List.C1Combo BizDateCombo;
		private System.Windows.Forms.MenuItem BuyInRecallMenuItem;
		private System.Windows.Forms.MenuItem OpenRecallMenuItem;
		private System.Windows.Forms.MenuItem DockTopMenuItem;
		private System.Windows.Forms.MenuItem DockBottomMenuItem;
		private System.Windows.Forms.MenuItem DockNoneMenuItem;
		private System.Windows.Forms.MenuItem DockMenuItem;
		private System.Windows.Forms.MenuItem FaxSeperatorMenuItem;
		private System.Windows.Forms.MenuItem DockSeperatorMenuItem;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem ShowRecallIdMenuItem;
		private System.Windows.Forms.MenuItem ShowContractIdMenuItem;
		private System.Windows.Forms.MenuItem ShowSequenceNumberMenuItem;
		private System.Windows.Forms.MenuItem ShowFaxIdMenuItem;
		private System.Windows.Forms.MenuItem ShowFaxStatusMenuItem;
		private System.Windows.Forms.MenuItem ShowFaxStatusTimeMenuItem;
		private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.MenuItem ShowMenuItem;
		private System.Windows.Forms.MenuItem CancelRecallMenuItem;
		private System.Windows.Forms.MenuItem MoveRecallMenuItem;
		private System.Windows.Forms.MenuItem ShowMoveToDateMenuItem;
		private System.Windows.Forms.MenuItem ShowDeliveredTodayMenuItem;
		private System.Windows.Forms.MenuItem ShowWillNeedMenuItem;
		private System.Windows.Forms.MenuItem MailRecipientMenuItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.MenuItem SeperatorMenuItem;
		private System.Windows.Forms.MenuItem RecallDocumentPrintMenuItem;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid RecallActivityGrid;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem RecallDocumentFaxMenuItem;   

		private ArrayList	recallArray;

		private delegate void SendRecallsDelegate(ArrayList recallArray, string status);
		private delegate void DeleteRecallsDelegate(ArrayList recallArray);
		
		private void SendRecalls(ArrayList recallArray, string status)
		{
			foreach (DataRow row in recallArray)
			{
				try
				{
					Log.Write("Updating Recall ID " + row["RecallId"].ToString() + " in " + row["BookGroup"].ToString() + " for " +
						row["Quantity"].ToString() + " of " + row["SecId"] + ". [PositionRecallsForm.SendRecalls]", 3);

					mainForm.Alert("Updating Recall ID " + row["RecallId"].ToString() + " in " + row["BookGroup"].ToString() + " for " +
						row["Quantity"].ToString() + " of " + row["SecId"] + ".", PilotState.Normal);
				  				  
					mainForm.PositionAgent.RecallSet(
						row["RecallId"].ToString(),
						"",
						row["MoveToDate"].ToString(), 
						"", 
						"", 
						"", 
						"", 
						"", 
						mainForm.UserId, status);
				}
				catch (Exception e)
				{
					Log.Write("Error sending Recall ID " + row["RecallId"].ToString() + ": " + e.Message + " [PositionContractBlotterForm.SendDeals]", Log.Error, 1);
				}
			}

			recallArray.Clear();
		}

		private void DeleteRecalls(ArrayList recallArray)
		{
			foreach (DataRow row in recallArray)
			{
				try
				{
					Log.Write("Canceling Recall ID " + row["RecallId"].ToString() + " in " + row["BookGroup"].ToString() + " for " +
						row["Quantity"].ToString() + " of " + row["SecId"] + ". [PositionRecallsForm.DeleteRecalls]", 3);
				 
					mainForm.Alert("Canceling Recall ID " + row["RecallId"].ToString() + " in " + row["BookGroup"].ToString() + " for " +
						row["Quantity"].ToString() + " of " + row["SecId"] + ".", PilotState.Normal);

					mainForm.PositionAgent.RecallDelete(          
						row["BookGroup"].ToString(),                                      
						row["ContractType"].ToString(),                                       
						row["Book"].ToString(),                                        
						row["ContractId"].ToString(),                                      
						row["OpenDateTime"].ToString(),
						int.Parse(row["SequenceNumber"].ToString()),
						row["RecallId"].ToString(),
						row["Comment"].ToString(),
						mainForm.UserId);
				}
				catch (Exception e)
				{
					Log.Write("Error canceling Recall ID " + row["RecallId"].ToString() + ": " + e.Message + " [PositionRecallsForm.DeleteRecalls]", Log.Error, 1);
				}
			}

			recallArray.Clear();
		}

		public PositionRecallsForm(MainForm mainForm)
		{
			try
			{    
				this.mainForm = mainForm;
        
				recallArray = new ArrayList();

				recallEventHandler   = new RecallEventHandler(DoRecallEvent);
				recallEventArgsArray = new ArrayList();
    
				InitializeComponent();
        
				recallEventWrapper = new RecallEventWrapper();
				recallEventWrapper.RecallEvent += new RecallEventHandler(OnRecallEvent);
      
				RecallsGrid.Splits[0,0].DisplayColumns["BookGroup"].Visible        = false;
				RecallsGrid.Splits[0,0].DisplayColumns["RecallId"].Visible         = false;
				RecallsGrid.Splits[0,0].DisplayColumns["ContractId"].Visible       = false;
				RecallsGrid.Splits[0,0].DisplayColumns["MoveToDate"].Visible       = false;        
				RecallsGrid.Splits[0,0].DisplayColumns["SequenceNumber"].Visible   = false;        
				RecallsGrid.Splits[0,0].DisplayColumns["FaxId"].Visible            = false;        
				RecallsGrid.Splits[0,0].DisplayColumns["FaxStatus"].Visible        = false;        
				RecallsGrid.Splits[0,0].DisplayColumns["FaxStatusTime"].Visible    = false;              
				RecallsGrid.Splits[0,0].DisplayColumns["DeliveredToday"].Visible   = false;              
				RecallsGrid.Splits[0,0].DisplayColumns["WillNeed"].Visible         = false;              
      
				isFaxEnabled = mainForm.PositionAgent.FaxEnabled();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		protected override void Dispose( bool disposing )
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public bool IsReady
		{
			get
			{
				return isReady;
			}

			set
			{
				if (value && (recallEventArgsArray.Count > 0))
				{
					isReady = false;

					recallEventHandler.BeginInvoke((RecallEventArgs)recallEventArgsArray[0], null, null);
					recallEventArgsArray.RemoveAt(0);
				}
				else
				{
					isReady = value;
				}       
			}
		}

		private void OnRecallEvent(RecallEventArgs recallEventArgs)
		{
			recallEventArgsArray.Add(recallEventArgs);    
      
			if (IsReady)
			{
				this.IsReady = true;
			}
		}

		private void DoRecallEvent(RecallEventArgs recallEventArgs)
		{  
			DataConfig(recallEventArgs);

			this.IsReady = true;
		}

		public void DataConfig(RecallEventArgs recallEventArgs)
		{
			try
			{
				dataSet.Tables["Recalls"].BeginLoadData();
				recallEventArgs.UtcOffset = mainForm.UtcOffset;                
				dataSet.Tables["Recalls"].LoadDataRow(recallEventArgs.Values, true);              
				dataSet.Tables["Recalls"].EndLoadData();      
              
				dataSet.Tables["RecallActivity"].BeginLoadData();              								
				if (recallEventArgs.RecallActivity != null)
				{
					if (recallEventArgs.RecallActivity.Length > 0)
					{
						for (int i = 0; i < recallEventArgs.RecallActivity.Length; i += 2)
						{
							object[] recallActivityRow = new object[5];
    
							recallActivityRow[0] = recallEventArgs.RecallId;
							recallActivityRow[1] = recallEventArgs.RecallActivity[i];
							recallActivityRow[2] = recallEventArgs.RecallActivity[i + 1];
							recallActivityRow[3] = recallEventArgs.ActUserShortName;
							recallActivityRow[4] = recallEventArgs.ActTime;

							dataSet.Tables["RecallActivity"].LoadDataRow(recallActivityRow, true);
						}
					}
					dataSet.Tables["RecallActivity"].EndLoadData();        
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.DataConfig]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionRecallsForm));
			this.RecallsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.RecallActivityGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.RecallsContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.MailRecipientMenuItem = new System.Windows.Forms.MenuItem();
			this.OpenRecallMenuItem = new System.Windows.Forms.MenuItem();
			this.BuyInRecallMenuItem = new System.Windows.Forms.MenuItem();
			this.MoveRecallMenuItem = new System.Windows.Forms.MenuItem();
			this.CancelRecallMenuItem = new System.Windows.Forms.MenuItem();
			this.FaxSeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.RecallDocumentPrintMenuItem = new System.Windows.Forms.MenuItem();
			this.RecallDocumentFaxMenuItem = new System.Windows.Forms.MenuItem();
			this.DockSeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowRecallIdMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowContractIdMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowMoveToDateMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowSequenceNumberMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowFaxIdMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowFaxStatusMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowFaxStatusTimeMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowDeliveredTodayMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowWillNeedMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.DockMenuItem = new System.Windows.Forms.MenuItem();
			this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
			this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
			this.SeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupCombo = new C1.Win.C1List.C1Combo();
			this.BizDateLabel = new C1.Win.C1Input.C1Label();
			this.BizDateCombo = new C1.Win.C1List.C1Combo();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.RecallsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RecallActivityGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BizDateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BizDateCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// RecallsGrid
			// 
			this.RecallsGrid.AllowColSelect = false;
			this.RecallsGrid.AllowFilter = false;
			this.RecallsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.RecallsGrid.AllowUpdate = false;
			this.RecallsGrid.Caption = "Recalls";
			this.RecallsGrid.CaptionHeight = 17;
			this.RecallsGrid.CellTips = C1.Win.C1TrueDBGrid.CellTipEnum.Floating;
			this.RecallsGrid.CellTipsDelay = 1;
			this.RecallsGrid.ChildGrid = this.RecallActivityGrid;
			this.RecallsGrid.Cursor = System.Windows.Forms.Cursors.Default;
			this.RecallsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.RecallsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RecallsGrid.EmptyRows = true;
			this.RecallsGrid.ExtendRightColumn = true;
			this.RecallsGrid.FetchRowStyles = true;
			this.RecallsGrid.FilterBar = true;
			this.RecallsGrid.ForeColor = System.Drawing.SystemColors.ControlText;
			this.RecallsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.RecallsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.RecallsGrid.Location = new System.Drawing.Point(0, 40);
			this.RecallsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.RecallsGrid.Name = "RecallsGrid";
			this.RecallsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.RecallsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.RecallsGrid.PreviewInfo.ZoomFactor = 75;
			this.RecallsGrid.RecordSelectorWidth = 16;
			this.RecallsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.RecallsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.RecallsGrid.RowHeight = 15;
			this.RecallsGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.RecallsGrid.Size = new System.Drawing.Size(1148, 665);
			this.RecallsGrid.TabIndex = 1;
			this.RecallsGrid.Text = "Position - Recalls";
			this.RecallsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.RecallsGrid_Paint);
			this.RecallsGrid.FetchCellTips += new C1.Win.C1TrueDBGrid.FetchCellTipsEventHandler(this.RecallsGrid_FetchCellTips);
			this.RecallsGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RecallsGrid_SelChange);
			this.RecallsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.RecallsGrid_FetchRowStyle);
			this.RecallsGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RecallsGrid_MouseDown);
			this.RecallsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RecallsGrid_BeforeUpdate);
			this.RecallsGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.RecallsGrid_BeforeColUpdate);
			this.RecallsGrid.FilterChange += new System.EventHandler(this.RecallsGrid_FilterChange);
			this.RecallsGrid.BeforeOpen += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RecallsGrid_BeforeOpen);
			this.RecallsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.RecallsGrid_FormatText);
			this.RecallsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecallsGrid_KeyPress);
			this.RecallsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Recall Id\" " +
				"DataField=\"RecallId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"BookGroup\" DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Contract Id\" DataField=\"ContractId\">" +
				"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" Da" +
				"taField=\"ContractType\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Relation=\"True\" Caption=\"Book\" DataField=\"Book\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\" DataField=\"SecI" +
				"d\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Sy" +
				"mbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatText Even" +
				"t\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Mo" +
				"ved To Date\" DataField=\"MoveToDate\" EditMaskUpdate=\"True\" NumberFormat=\"FormatTe" +
				"xt Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
				"ion=\"Recall Date\" DataField=\"OpenDateTime\" NumberFormat=\"FormatText Event\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" D" +
				"ataField=\"Comment\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" Numbe" +
				"rFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"R\" DataField=\"ReasonCode\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"E\" DataField=\"IsEasy\"><ValueItems Pre" +
				"sentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
				"on=\"H\" DataField=\"IsHard\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" DataField=\"IsThreshold\"><ValueIt" +
				"ems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"N\" DataField=\"IsNoLend\"><ValueItems Presentation=\"CheckBox\" /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Days\" DataField=\"DayCount\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S\" D" +
				"ataField=\"Status\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Action\" DataField=\"Action\"><ValueItems /><GroupInfo /></C1DataColum" +
				"n><C1DataColumn Level=\"0\" Caption=\"Sequence Number\" DataField=\"SequenceNumber\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Buy In" +
				" Date\" DataField=\"BaseDueDate\" NumberFormat=\"FormatText Event\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Fax Id\" DataField=\"Fax" +
				"Id\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"F" +
				"ax Status\" DataField=\"FaxStatus\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
				"taColumn Level=\"0\" Caption=\"Fax Status Time\" DataField=\"FaxStatusTime\" NumberFor" +
				"mat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Delivered Today\" DataField=\"DeliveredToday\" NumberFormat=\"Form" +
				"atText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Will Need\" DataField=\"WillNeed\" NumberFormat=\"FormatText Event\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Book Contact" +
				"\" DataField=\"BookContact\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><" +
				"Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style22{AlignHorz:" +
				"Near;}Style109{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style73{}C" +
				"aption{AlignHorz:Center;}Style71{}Style638{AlignHorz:Center;}Style639{}Style630{" +
				"}Style637{AlignHorz:Near;}Normal{Font:Verdana, 8.25pt;}Style94{AlignHorz:Center;" +
				"}Style95{}Style96{}Style97{}Style93{AlignHorz:Near;}Style98{}OddRow{}Group{BackC" +
				"olor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style619{AlignHorz:Ne" +
				"ar;}Style612{}Style613{AlignHorz:Center;}Style610{}Style611{}Style616{}Style617{" +
				"}Style614{AlignHorz:Center;}Style615{}EvenRow{BackColor:235, 232, 255;}Editor{}S" +
				"tyle629{}Style628{}Style621{}Style620{AlignHorz:Far;}Style623{}Style622{}Style62" +
				"5{AlignHorz:Near;}Footer{}Style627{}Style626{AlignHorz:Center;}FilterBar{}Style4" +
				"9{}Style48{}Style45{AlignHorz:Near;}Style47{}Style46{AlignHorz:Near;}Style559{Al" +
				"ignHorz:Near;}Style558{}Style555{}Style554{AlignHorz:Near;}Style557{}Style556{}S" +
				"tyle551{}Style550{AlignHorz:Near;}Style58{AlignHorz:Center;}Style59{}Style609{}S" +
				"tyle608{AlignHorz:Center;}Style603{}Style602{AlignHorz:Center;}Style601{AlignHor" +
				"z:Center;}Style600{}Style607{AlignHorz:Center;}Style606{}Style605{}Style57{Align" +
				"Horz:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCo" +
				"lor:ControlText;BackColor:Control;}Style548{}Style549{}Style544{}Style545{}Style" +
				"546{}Style547{}Style541{}Style542{}Style543{}Style69{AlignHorz:Near;}Style658{}S" +
				"tyle659{}Style656{AlignHorz:Near;}Style657{}Style654{}Style655{AlignHorz:Near;}S" +
				"tyle652{}Style653{}Style650{AlignHorz:Near;}Style651{}Style110{}Style579{}Style5" +
				"78{AlignHorz:Near;}Style577{AlignHorz:Near;}Style576{}Style575{}Style574{}Style5" +
				"73{}Style572{AlignHorz:Center;}Style571{AlignHorz:Center;}Style570{}Style72{}Sty" +
				"le664{}Style70{AlignHorz:Near;}Style666{}Style661{AlignHorz:Near;}Style660{}Styl" +
				"e108{}Style662{AlignHorz:Near;}Style105{AlignHorz:Near;}Style106{AlignHorz:Near;" +
				"}Style107{}Style568{}Style569{}Style566{AlignHorz:Far;}Style567{}Style564{}Style" +
				"565{AlignHorz:Near;}Style562{}Style563{}Style560{AlignHorz:Near;}Style561{}Style" +
				"553{AlignHorz:Near;}Style552{}Style591{}Style590{AlignHorz:Near;}Style593{}Style" +
				"592{}Style595{AlignHorz:Center;}Style594{}Style597{}Style596{AlignHorz:Center;}S" +
				"tyle599{}Style598{}Style19{}Style649{AlignHorz:Near;}Style648{}Style647{}Style50" +
				"{}Style645{}Style644{AlignHorz:Center;}Style643{AlignHorz:Center;}Style642{}Styl" +
				"e641{}Style640{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style5" +
				"80{}Style581{}Style582{}Style583{AlignHorz:Near;}Style584{AlignHorz:Near;}Style5" +
				"85{}Style586{}Style587{}Style588{}Style589{AlignHorz:Near;}Style29{}Style28{Alig" +
				"nHorz:Far;}Style27{AlignHorz:Near;}Style26{}Style25{}Style62{}Style61{}Style60{}" +
				"Style21{AlignHorz:Near;}Style20{}Style618{}RecordSelector{Wrap:False;AlignImage:" +
				"Center;}Style38{}Style36{}Style37{}Style34{AlignHorz:Near;}Style35{}Style32{}Sty" +
				"le33{AlignHorz:Near;}Style30{}Style31{}Style74{}Inactive{ForeColor:InactiveCapti" +
				"onText;BackColor:InactiveCaption;}Style665{}Style663{}Style9{}Style8{AlignHorz:C" +
				"enter;}Style624{}Style5{}Style4{}Style7{AlignHorz:Center;}Style6{}Style1{AlignHo" +
				"rz:Near;}Style3{}Style2{AlignHorz:Near;}Style18{}Style673{}Style14{}Style15{}Sty" +
				"le16{AlignHorz:Far;}Style17{}Style10{}Style11{}Style12{}Style13{AlignHorz:Near;}" +
				"Style646{}Style604{}Style24{}Style23{}</Data></Styles><Splits><C1.Win.C1TrueDBGr" +
				"id.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"Sp" +
				"lit[0,0]\" AllowRowSizing=\"None\" AllowVerticalSizing=\"False\" CaptionHeight=\"17\" C" +
				"olumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRo" +
				"wStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWid" +
				"th=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"3\"><C" +
				"aptionStyle parent=\"Heading\" me=\"Style550\" /><EditorStyle parent=\"Editor\" me=\"St" +
				"yle542\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style548\" /><FilterBarStyle parent=" +
				"\"FilterBar\" me=\"Style673\" /><FooterStyle parent=\"Footer\" me=\"Style544\" /><GroupS" +
				"tyle parent=\"Group\" me=\"Style552\" /><HeadingStyle parent=\"Heading\" me=\"Style543\"" +
				" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style547\" /><InactiveStyle paren" +
				"t=\"Inactive\" me=\"Style546\" /><OddRowStyle parent=\"OddRow\" me=\"Style549\" /><Recor" +
				"dSelectorStyle parent=\"RecordSelector\" me=\"Style551\" /><SelectedStyle parent=\"Se" +
				"lected\" me=\"Style545\" /><Style parent=\"Normal\" me=\"Style541\" /><internalCols><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Style559\" /><Style parent=\"Sty" +
				"le541\" me=\"Style560\" /><FooterStyle parent=\"Style544\" me=\"Style561\" /><EditorSty" +
				"le parent=\"Style542\" me=\"Style562\" /><GroupHeaderStyle parent=\"Style541\" me=\"Sty" +
				"le564\" /><GroupFooterStyle parent=\"Style541\" me=\"Style563\" /><Visible>True</Visi" +
				"ble><ColumnDivider>DarkGray,Single</ColumnDivider><Width>70</Width><Height>15</H" +
				"eight><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style543\" me=\"Style577\" /><Style parent=\"Style541\" me=\"Style" +
				"578\" /><FooterStyle parent=\"Style544\" me=\"Style579\" /><EditorStyle parent=\"Style" +
				"542\" me=\"Style580\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style582\" /><GroupF" +
				"ooterStyle parent=\"Style541\" me=\"Style581\" /><Visible>True</Visible><ColumnDivid" +
				"er>DarkGray,Single</ColumnDivider><Width>60</Width><Height>15</Height><Locked>Tr" +
				"ue</Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style543\" me=\"Style1\" /><Style parent=\"Style541\" me=\"Style2\" /><FooterStyle " +
				"parent=\"Style544\" me=\"Style3\" /><EditorStyle parent=\"Style542\" me=\"Style4\" /><Gr" +
				"oupHeaderStyle parent=\"Style541\" me=\"Style6\" /><GroupFooterStyle parent=\"Style54" +
				"1\" me=\"Style5\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDi" +
				"vider><Width>124</Width><Height>15</Height><DCIdx>28</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><DropDownList>True</DropDownList><HeadingStyle parent=\"Style543\" " +
				"me=\"Style553\" /><Style parent=\"Style541\" me=\"Style554\" /><FooterStyle parent=\"St" +
				"yle544\" me=\"Style555\" /><EditorStyle parent=\"Style542\" me=\"Style556\" /><GroupHea" +
				"derStyle parent=\"Style541\" me=\"Style558\" /><GroupFooterStyle parent=\"Style541\" m" +
				"e=\"Style557\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Width>136</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style543\" me=\"Style565\" /><Style parent=\"Style" +
				"541\" me=\"Style566\" /><FooterStyle parent=\"Style544\" me=\"Style567\" /><EditorStyle" +
				" parent=\"Style542\" me=\"Style568\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style" +
				"570\" /><GroupFooterStyle parent=\"Style541\" me=\"Style569\" /><Visible>True</Visibl" +
				"e><ColumnDivider>DarkGray,Single</ColumnDivider><Width>90</Width><Height>15</Hei" +
				"ght><Locked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style543\" me=\"Style571\" /><Style parent=\"Style541\" me=\"Style57" +
				"2\" /><FooterStyle parent=\"Style544\" me=\"Style573\" /><EditorStyle parent=\"Style54" +
				"2\" me=\"Style574\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style576\" /><GroupFoo" +
				"terStyle parent=\"Style541\" me=\"Style575\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Width>24</Width><Height>15</Height><Locked>True" +
				"</Locked><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style543\" me=\"Style45\" /><Style parent=\"Style541\" me=\"Style46\" /><FooterStyle " +
				"parent=\"Style544\" me=\"Style47\" /><EditorStyle parent=\"Style542\" me=\"Style48\" /><" +
				"GroupHeaderStyle parent=\"Style541\" me=\"Style50\" /><GroupFooterStyle parent=\"Styl" +
				"e541\" me=\"Style49\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colu" +
				"mnDivider><Height>15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style543\" me=\"Style583\" /><Style parent=\"Style541\" me=\"St" +
				"yle584\" /><FooterStyle parent=\"Style544\" me=\"Style585\" /><EditorStyle parent=\"St" +
				"yle542\" me=\"Style586\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style588\" /><Gro" +
				"upFooterStyle parent=\"Style541\" me=\"Style587\" /><Visible>True</Visible><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Width>95</Width><Height>15</Height><Locked" +
				">True</Locked><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style543\" me=\"Style589\" /><Style parent=\"Style541\" me=\"Style590\" /><Foote" +
				"rStyle parent=\"Style544\" me=\"Style591\" /><EditorStyle parent=\"Style542\" me=\"Styl" +
				"e592\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style594\" /><GroupFooterStyle pa" +
				"rent=\"Style541\" me=\"Style593\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>75</Width><Height>15</Height><Locked>True</Locked><D" +
				"CIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\"" +
				" me=\"Style595\" /><Style parent=\"Style541\" me=\"Style596\" /><FooterStyle parent=\"S" +
				"tyle544\" me=\"Style597\" /><EditorStyle parent=\"Style542\" me=\"Style598\" /><GroupHe" +
				"aderStyle parent=\"Style541\" me=\"Style600\" /><GroupFooterStyle parent=\"Style541\" " +
				"me=\"Style599\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDiv" +
				"ider><Width>16</Width><Height>15</Height><Locked>True</Locked><DCIdx>14</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Style601\" " +
				"/><Style parent=\"Style541\" me=\"Style602\" /><FooterStyle parent=\"Style544\" me=\"St" +
				"yle603\" /><EditorStyle parent=\"Style542\" me=\"Style604\" /><GroupHeaderStyle paren" +
				"t=\"Style541\" me=\"Style606\" /><GroupFooterStyle parent=\"Style541\" me=\"Style605\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>16<" +
				"/Width><Height>15</Height><Locked>True</Locked><DCIdx>15</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Style607\" /><Style parent" +
				"=\"Style541\" me=\"Style608\" /><FooterStyle parent=\"Style544\" me=\"Style609\" /><Edit" +
				"orStyle parent=\"Style542\" me=\"Style610\" /><GroupHeaderStyle parent=\"Style541\" me" +
				"=\"Style612\" /><GroupFooterStyle parent=\"Style541\" me=\"Style611\" /><Visible>True<" +
				"/Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>16</Width><Height>" +
				"15</Height><Locked>True</Locked><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style543\" me=\"Style613\" /><Style parent=\"Style541\" me=" +
				"\"Style614\" /><FooterStyle parent=\"Style544\" me=\"Style615\" /><EditorStyle parent=" +
				"\"Style542\" me=\"Style616\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style618\" /><" +
				"GroupFooterStyle parent=\"Style541\" me=\"Style617\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>16</Width><Height>15</Height><Loc" +
				"ked>True</Locked><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style543\" me=\"Style7\" /><Style parent=\"Style541\" me=\"Style8\" /><Foote" +
				"rStyle parent=\"Style544\" me=\"Style9\" /><EditorStyle parent=\"Style542\" me=\"Style1" +
				"0\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style12\" /><GroupFooterStyle parent" +
				"=\"Style541\" me=\"Style11\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Width>45</Width><Height>15</Height><Locked>True</Locked><DCIdx>" +
				"18</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=" +
				"\"Style643\" /><Style parent=\"Style541\" me=\"Style644\" /><FooterStyle parent=\"Style" +
				"544\" me=\"Style645\" /><EditorStyle parent=\"Style542\" me=\"Style646\" /><GroupHeader" +
				"Style parent=\"Style541\" me=\"Style648\" /><GroupFooterStyle parent=\"Style541\" me=\"" +
				"Style647\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Width>40</Width><Height>15</Height><Locked>True</Locked><DCIdx>13</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Style619\" /><S" +
				"tyle parent=\"Style541\" me=\"Style620\" /><FooterStyle parent=\"Style544\" me=\"Style6" +
				"21\" /><EditorStyle parent=\"Style542\" me=\"Style622\" /><GroupHeaderStyle parent=\"S" +
				"tyle541\" me=\"Style624\" /><GroupFooterStyle parent=\"Style541\" me=\"Style623\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>70</Wid" +
				"th><Height>15</Height><Locked>True</Locked><DCIdx>7</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Style625\" /><Style parent=\"Sty" +
				"le541\" me=\"Style626\" /><FooterStyle parent=\"Style544\" me=\"Style627\" /><EditorSty" +
				"le parent=\"Style542\" me=\"Style628\" /><GroupHeaderStyle parent=\"Style541\" me=\"Sty" +
				"le630\" /><GroupFooterStyle parent=\"Style541\" me=\"Style629\" /><Visible>True</Visi" +
				"ble><ColumnDivider>DarkGray,Single</ColumnDivider><Width>90</Width><Height>15</H" +
				"eight><Locked>True</Locked><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style543\" me=\"Style93\" /><Style parent=\"Style541\" me=\"Style9" +
				"4\" /><FooterStyle parent=\"Style544\" me=\"Style95\" /><EditorStyle parent=\"Style542" +
				"\" me=\"Style96\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style98\" /><GroupFooter" +
				"Style parent=\"Style541\" me=\"Style97\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>22</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Styl" +
				"e637\" /><Style parent=\"Style541\" me=\"Style638\" /><FooterStyle parent=\"Style544\" " +
				"me=\"Style639\" /><EditorStyle parent=\"Style542\" me=\"Style640\" /><GroupHeaderStyle" +
				" parent=\"Style541\" me=\"Style642\" /><GroupFooterStyle parent=\"Style541\" me=\"Style" +
				"641\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>112</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style543\" me=\"Style21\" /><Style parent=\"Style541\" me=\"" +
				"Style22\" /><FooterStyle parent=\"Style544\" me=\"Style23\" /><EditorStyle parent=\"St" +
				"yle542\" me=\"Style24\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style26\" /><Group" +
				"FooterStyle parent=\"Style541\" me=\"Style25\" /><Visible>True</Visible><ColumnDivid" +
				"er>DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx" +
				">23</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me" +
				"=\"Style33\" /><Style parent=\"Style541\" me=\"Style34\" /><FooterStyle parent=\"Style5" +
				"44\" me=\"Style35\" /><EditorStyle parent=\"Style542\" me=\"Style36\" /><GroupHeaderSty" +
				"le parent=\"Style541\" me=\"Style38\" /><GroupFooterStyle parent=\"Style541\" me=\"Styl" +
				"e37\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
				"ght>15</Height><Locked>True</Locked><DCIdx>24</DCIdx></C1DisplayColumn><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style543\" me=\"Style105\" /><Style parent=\"Style541\"" +
				" me=\"Style106\" /><FooterStyle parent=\"Style544\" me=\"Style107\" /><EditorStyle par" +
				"ent=\"Style542\" me=\"Style108\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style110\"" +
				" /><GroupFooterStyle parent=\"Style541\" me=\"Style109\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Width>140</Width><Height>15</Height" +
				"><Locked>True</Locked><DCIdx>25</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style543\" me=\"Style69\" /><Style parent=\"Style541\" me=\"Style70\" /" +
				"><FooterStyle parent=\"Style544\" me=\"Style71\" /><EditorStyle parent=\"Style542\" me" +
				"=\"Style72\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style74\" /><GroupFooterStyl" +
				"e parent=\"Style541\" me=\"Style73\" /><Visible>True</Visible><ColumnDivider>DarkGra" +
				"y,Single</ColumnDivider><Width>127</Width><Height>15</Height><Locked>True</Locke" +
				"d><DCIdx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e543\" me=\"Style13\" /><Style parent=\"Style541\" me=\"Style16\" /><FooterStyle parent" +
				"=\"Style544\" me=\"Style17\" /><EditorStyle parent=\"Style542\" me=\"Style18\" /><GroupH" +
				"eaderStyle parent=\"Style541\" me=\"Style20\" /><GroupFooterStyle parent=\"Style541\" " +
				"me=\"Style19\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Width>110</Width><Height>15</Height><Locked>True</Locked><DCIdx>26</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Style27\" /" +
				"><Style parent=\"Style541\" me=\"Style28\" /><FooterStyle parent=\"Style544\" me=\"Styl" +
				"e29\" /><EditorStyle parent=\"Style542\" me=\"Style30\" /><GroupHeaderStyle parent=\"S" +
				"tyle541\" me=\"Style32\" /><GroupFooterStyle parent=\"Style541\" me=\"Style31\" /><Visi" +
				"ble>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>70</Width" +
				"><Height>15</Height><DCIdx>27</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
				"Style parent=\"Style543\" me=\"Style649\" /><Style parent=\"Style541\" me=\"Style650\" /" +
				"><FooterStyle parent=\"Style544\" me=\"Style651\" /><EditorStyle parent=\"Style542\" m" +
				"e=\"Style652\" /><GroupHeaderStyle parent=\"Style541\" me=\"Style654\" /><GroupFooterS" +
				"tyle parent=\"Style541\" me=\"Style653\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Width>71</Width><Height>15</Height><Locked>True</Lo" +
				"cked><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle543\" me=\"Style655\" /><Style parent=\"Style541\" me=\"Style656\" /><FooterStyle p" +
				"arent=\"Style544\" me=\"Style657\" /><EditorStyle parent=\"Style542\" me=\"Style658\" />" +
				"<GroupHeaderStyle parent=\"Style541\" me=\"Style660\" /><GroupFooterStyle parent=\"St" +
				"yle541\" me=\"Style659\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>140</Width><Height>15</Height><Locked>True</Locked><DCIdx>12" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=\"S" +
				"tyle57\" /><Style parent=\"Style541\" me=\"Style58\" /><FooterStyle parent=\"Style544\"" +
				" me=\"Style59\" /><EditorStyle parent=\"Style542\" me=\"Style60\" /><GroupHeaderStyle " +
				"parent=\"Style541\" me=\"Style62\" /><GroupFooterStyle parent=\"Style541\" me=\"Style61" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>" +
				"40</Width><Height>15</Height><Locked>True</Locked><DCIdx>19</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style543\" me=\"Style661\" /><Style par" +
				"ent=\"Style541\" me=\"Style662\" /><FooterStyle parent=\"Style544\" me=\"Style663\" /><E" +
				"ditorStyle parent=\"Style542\" me=\"Style664\" /><GroupHeaderStyle parent=\"Style541\"" +
				" me=\"Style666\" /><GroupFooterStyle parent=\"Style541\" me=\"Style665\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Width>81</Width><Height" +
				">15</Height><Locked>True</Locked><HeaderDivider>False</HeaderDivider><DCIdx>10</" +
				"DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 17, 1144, 644</ClientRect>" +
				"<BorderSide>Left</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyl" +
				"es><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style p" +
				"arent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style pare" +
				"nt=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style paren" +
				"t=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style paren" +
				"t=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"He" +
				"ading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style pare" +
				"nt=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1<" +
				"/horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth>" +
				"<ClientArea>0, 0, 1144, 661</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Styl" +
				"e14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// RecallActivityGrid
			// 
			this.RecallActivityGrid.AllowColMove = false;
			this.RecallActivityGrid.AllowColSelect = false;
			this.RecallActivityGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.RecallActivityGrid.CaptionHeight = 17;
			this.RecallActivityGrid.ExtendRightColumn = true;
			this.RecallActivityGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.RecallActivityGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.RecallActivityGrid.Location = new System.Drawing.Point(336, 184);
			this.RecallActivityGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.RecallActivityGrid.Name = "RecallActivityGrid";
			this.RecallActivityGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.RecallActivityGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.RecallActivityGrid.PreviewInfo.ZoomFactor = 75;
			this.RecallActivityGrid.RecordSelectorWidth = 16;
			this.RecallActivityGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.RecallActivityGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.RecallActivityGrid.RowHeight = 15;
			this.RecallActivityGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.RecallActivityGrid.Size = new System.Drawing.Size(608, 224);
			this.RecallActivityGrid.TabIndex = 7;
			this.RecallActivityGrid.TabStop = false;
			this.RecallActivityGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.RecallActivityGrid_FormatText);
			this.RecallActivityGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"RecallId\" D" +
				"ataField=\"RecallId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Actor\" DataField=\"ActUserId\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberForma" +
				"t=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Activity\" DataField=\"Activity\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Da" +
				"ta>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Caption{AlignHorz:C" +
				"enter;}Normal{Font:Verdana, 8.25pt;}Style25{}Style24{}Editor{}Style18{}Style19{}" +
				"Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignH" +
				"orz:Near;}Style11{}OddRow{}Style13{}Style12{}Style29{AlignHorz:Near;}Style28{Ali" +
				"gnHorz:Near;}Style27{}Style26{}RecordSelector{AlignImage:Center;}Footer{}Style23" +
				"{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style20{}Group{BackColor:Contr" +
				"olDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{ForeColor:InactiveCapt" +
				"ionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;Ali" +
				"gnVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;" +
				"}Style3{}Style6{}Style1{}Style5{}Style7{}Style8{}FilterBar{}Selected{ForeColor:H" +
				"ighlightText;BackColor:Highlight;}Style4{}Style9{}Style38{}Style39{}Style36{}Sty" +
				"le37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{" +
				"}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView AllowCo" +
				"lMove=\"False\" AllowColSelect=\"False\" Name=\"\" AllowRowSizing=\"None\" CaptionHeight" +
				"=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" " +
				"MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"" +
				"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"Even" +
				"Row\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyl" +
				"e parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><Headi" +
				"ngStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" " +
				"me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent" +
				"=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11" +
				"\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"St" +
				"yle1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16" +
				"\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><ColumnDivid" +
				"er>DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx" +
				">0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>70</Width>" +
				"<Height>15</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" m" +
				"e=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>Da" +
				"rkGray,Single</ColumnDivider><Width>140</Width><Height>15</Height><Locked>True</" +
				"Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
				"ght>15</Height><Locked>True</Locked><DCIdx>3</DCIdx></C1DisplayColumn></internal" +
				"Cols><ClientRect>0, 0, 604, 220</ClientRect><BorderSide>0</BorderSide></C1.Win.C" +
				"1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Styl" +
				"e parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pa" +
				"rent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style par" +
				"ent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=" +
				"\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent" +
				"=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style par" +
				"ent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles" +
				"><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><D" +
				"efaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 604, 220</ClientArea>" +
				"<PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" m" +
				"e=\"Style15\" /></Blob>";
			// 
			// RecallsContextMenu
			// 
			this.RecallsContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																											 this.SendToMenuItem,
																																											 this.OpenRecallMenuItem,
																																											 this.BuyInRecallMenuItem,
																																											 this.MoveRecallMenuItem,
																																											 this.CancelRecallMenuItem,
																																											 this.FaxSeperatorMenuItem,
																																											 this.RecallDocumentPrintMenuItem,
																																											 this.RecallDocumentFaxMenuItem,
																																											 this.DockSeperatorMenuItem,
																																											 this.ShowMenuItem,
																																											 this.menuItem1,
																																											 this.DockMenuItem,
																																											 this.menuItem2,
																																											 this.ExitMenuItem});
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 0;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToClipboardMenuItem,
																																									 this.SendToExcelMenuItem,
																																									 this.MailRecipientMenuItem});
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
			// MailRecipientMenuItem
			// 
			this.MailRecipientMenuItem.Index = 2;
			this.MailRecipientMenuItem.Text = "Mail Recipient";
			this.MailRecipientMenuItem.Click += new System.EventHandler(this.MailRecipientMenuItem_Click);
			// 
			// OpenRecallMenuItem
			// 
			this.OpenRecallMenuItem.Enabled = false;
			this.OpenRecallMenuItem.Index = 1;
			this.OpenRecallMenuItem.Text = "Open";
			this.OpenRecallMenuItem.Click += new System.EventHandler(this.OpenRecallMenuItem_Click);
			// 
			// BuyInRecallMenuItem
			// 
			this.BuyInRecallMenuItem.Enabled = false;
			this.BuyInRecallMenuItem.Index = 2;
			this.BuyInRecallMenuItem.Text = "Buy In";
			this.BuyInRecallMenuItem.Click += new System.EventHandler(this.BuyInRecallMenuItem_Click);
			// 
			// MoveRecallMenuItem
			// 
			this.MoveRecallMenuItem.Enabled = false;
			this.MoveRecallMenuItem.Index = 3;
			this.MoveRecallMenuItem.Text = "Move";
			this.MoveRecallMenuItem.Click += new System.EventHandler(this.MoveRecallMenuItem_Click);
			// 
			// CancelRecallMenuItem
			// 
			this.CancelRecallMenuItem.Enabled = false;
			this.CancelRecallMenuItem.Index = 4;
			this.CancelRecallMenuItem.Text = "Cancel";
			this.CancelRecallMenuItem.Click += new System.EventHandler(this.CancelRecallMenuItem_Click);
			// 
			// FaxSeperatorMenuItem
			// 
			this.FaxSeperatorMenuItem.Index = 5;
			this.FaxSeperatorMenuItem.Text = "-";
			// 
			// RecallDocumentPrintMenuItem
			// 
			this.RecallDocumentPrintMenuItem.Enabled = false;
			this.RecallDocumentPrintMenuItem.Index = 6;
			this.RecallDocumentPrintMenuItem.Text = "Print Recall Document";
			this.RecallDocumentPrintMenuItem.Click += new System.EventHandler(this.RecallDocumentPrintMenuItem_Click);
			// 
			// RecallDocumentFaxMenuItem
			// 
			this.RecallDocumentFaxMenuItem.Enabled = false;
			this.RecallDocumentFaxMenuItem.Index = 7;
			this.RecallDocumentFaxMenuItem.Text = "Fax Recall Document";
			this.RecallDocumentFaxMenuItem.Click += new System.EventHandler(this.RecallDocumentFaxMenuItem_Click);
			// 
			// DockSeperatorMenuItem
			// 
			this.DockSeperatorMenuItem.Index = 8;
			this.DockSeperatorMenuItem.Text = "-";
			// 
			// ShowMenuItem
			// 
			this.ShowMenuItem.Index = 9;
			this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.ShowRecallIdMenuItem,
																																								 this.ShowContractIdMenuItem,
																																								 this.ShowMoveToDateMenuItem,
																																								 this.ShowSequenceNumberMenuItem,
																																								 this.ShowFaxIdMenuItem,
																																								 this.ShowFaxStatusMenuItem,
																																								 this.ShowFaxStatusTimeMenuItem,
																																								 this.ShowDeliveredTodayMenuItem,
																																								 this.ShowWillNeedMenuItem});
			this.ShowMenuItem.Text = "Show";
			// 
			// ShowRecallIdMenuItem
			// 
			this.ShowRecallIdMenuItem.Index = 0;
			this.ShowRecallIdMenuItem.Text = "Recall Id";
			this.ShowRecallIdMenuItem.Click += new System.EventHandler(this.ShowRecallIdMenuItem_Click);
			// 
			// ShowContractIdMenuItem
			// 
			this.ShowContractIdMenuItem.Index = 1;
			this.ShowContractIdMenuItem.Text = "Contract Id";
			this.ShowContractIdMenuItem.Click += new System.EventHandler(this.ShowContractIdMenuItem_Click);
			// 
			// ShowMoveToDateMenuItem
			// 
			this.ShowMoveToDateMenuItem.Index = 2;
			this.ShowMoveToDateMenuItem.Text = "Move To Date";
			this.ShowMoveToDateMenuItem.Click += new System.EventHandler(this.ShowMoveToDateMenuItem_Click);
			// 
			// ShowSequenceNumberMenuItem
			// 
			this.ShowSequenceNumberMenuItem.Index = 3;
			this.ShowSequenceNumberMenuItem.Text = "Sequence Number";
			this.ShowSequenceNumberMenuItem.Click += new System.EventHandler(this.ShowSequenceNumberMenuItem_Click);
			// 
			// ShowFaxIdMenuItem
			// 
			this.ShowFaxIdMenuItem.Index = 4;
			this.ShowFaxIdMenuItem.Text = "Fax Id";
			this.ShowFaxIdMenuItem.Click += new System.EventHandler(this.ShowFaxIdMenuItem_Click);
			// 
			// ShowFaxStatusMenuItem
			// 
			this.ShowFaxStatusMenuItem.Index = 5;
			this.ShowFaxStatusMenuItem.Text = "Fax Status";
			this.ShowFaxStatusMenuItem.Click += new System.EventHandler(this.ShowFaxStatusMenuItem_Click);
			// 
			// ShowFaxStatusTimeMenuItem
			// 
			this.ShowFaxStatusTimeMenuItem.Index = 6;
			this.ShowFaxStatusTimeMenuItem.Text = "Fax Status Time";
			this.ShowFaxStatusTimeMenuItem.Click += new System.EventHandler(this.ShowFaxStatusTimeMenuItem_Click);
			// 
			// ShowDeliveredTodayMenuItem
			// 
			this.ShowDeliveredTodayMenuItem.Index = 7;
			this.ShowDeliveredTodayMenuItem.Text = "Delivered Today";
			this.ShowDeliveredTodayMenuItem.Click += new System.EventHandler(this.ShowDeliveredTodayMenuItem_Click);
			// 
			// ShowWillNeedMenuItem
			// 
			this.ShowWillNeedMenuItem.Index = 8;
			this.ShowWillNeedMenuItem.Text = "Will Need";
			this.ShowWillNeedMenuItem.Click += new System.EventHandler(this.ShowWillNeedMenuItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 10;
			this.menuItem1.Text = "-";
			// 
			// DockMenuItem
			// 
			this.DockMenuItem.Index = 11;
			this.DockMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.DockTopMenuItem,
																																								 this.DockBottomMenuItem,
																																								 this.SeperatorMenuItem,
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
			// SeperatorMenuItem
			// 
			this.SeperatorMenuItem.Index = 2;
			this.SeperatorMenuItem.Text = "-";
			// 
			// DockNoneMenuItem
			// 
			this.DockNoneMenuItem.Index = 3;
			this.DockNoneMenuItem.Text = "None";
			this.DockNoneMenuItem.Click += new System.EventHandler(this.DockNoneMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 12;
			this.menuItem2.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 13;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// BookGroupNameLabel
			// 
			this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
			this.BookGroupNameLabel.Location = new System.Drawing.Point(424, 8);
			this.BookGroupNameLabel.Name = "BookGroupNameLabel";
			this.BookGroupNameLabel.Size = new System.Drawing.Size(348, 20);
			this.BookGroupNameLabel.TabIndex = 6;
			this.BookGroupNameLabel.Tag = null;
			this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BookGroupLabel
			// 
			this.BookGroupLabel.Location = new System.Drawing.Point(232, 8);
			this.BookGroupLabel.Name = "BookGroupLabel";
			this.BookGroupLabel.Size = new System.Drawing.Size(92, 20);
			this.BookGroupLabel.TabIndex = 4;
			this.BookGroupLabel.Tag = null;
			this.BookGroupLabel.Text = "Book Group:";
			this.BookGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BookGroupLabel.TextDetached = true;
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
			this.BookGroupCombo.Location = new System.Drawing.Point(328, 8);
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
			this.BookGroupCombo.TabIndex = 5;
			this.BookGroupCombo.RowChange += new System.EventHandler(this.BookGroupCombo_RowChange);
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
			// BizDateLabel
			// 
			this.BizDateLabel.Location = new System.Drawing.Point(16, 8);
			this.BizDateLabel.Name = "BizDateLabel";
			this.BizDateLabel.Size = new System.Drawing.Size(92, 20);
			this.BizDateLabel.TabIndex = 29;
			this.BizDateLabel.Tag = null;
			this.BizDateLabel.Text = "Business Date:";
			this.BizDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BizDateLabel.TextDetached = true;
			// 
			// BizDateCombo
			// 
			this.BizDateCombo.AddItemSeparator = ';';
			this.BizDateCombo.AutoCompletion = true;
			this.BizDateCombo.AutoDropDown = true;
			this.BizDateCombo.AutoSelect = true;
			this.BizDateCombo.AutoSize = false;
			this.BizDateCombo.Caption = "";
			this.BizDateCombo.CaptionHeight = 17;
			this.BizDateCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.BizDateCombo.ColumnCaptionHeight = 17;
			this.BizDateCombo.ColumnFooterHeight = 17;
			this.BizDateCombo.ContentHeight = 14;
			this.BizDateCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.BizDateCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.BizDateCombo.DropDownWidth = 95;
			this.BizDateCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.BizDateCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BizDateCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.BizDateCombo.EditorHeight = 14;
			this.BizDateCombo.ExtendRightColumn = true;
			this.BizDateCombo.GapHeight = 2;
			this.BizDateCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.BizDateCombo.ItemHeight = 15;
			this.BizDateCombo.KeepForeColor = true;
			this.BizDateCombo.LimitToList = true;
			this.BizDateCombo.Location = new System.Drawing.Point(112, 8);
			this.BizDateCombo.MatchEntryTimeout = ((long)(2000));
			this.BizDateCombo.MaxDropDownItems = ((short)(10));
			this.BizDateCombo.MaxLength = 15;
			this.BizDateCombo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
			this.BizDateCombo.Name = "BizDateCombo";
			this.BizDateCombo.PartialRightColumn = false;
			this.BizDateCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BizDateCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.BizDateCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BizDateCombo.Size = new System.Drawing.Size(96, 20);
			this.BizDateCombo.TabIndex = 30;
			this.BizDateCombo.RowChange += new System.EventHandler(this.BizDateCombo_RowChange);
			this.BizDateCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Date\" DataF" +
				"ield=\"BizDate\" DataWidth=\"75\"><ValueItems /></C1DataColumn></DataCols><Styles ty" +
				"pe=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:Non" +
				"e,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Sty" +
				"le4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;Ba" +
				"ckColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:Inac" +
				"tiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Window;}Highligh" +
				"tRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{Font:Verdana, 8.25pt;}O" +
				"ddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Bor" +
				"der:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}" +
				"Style11{}Style14{}Style13{Font:Verdana, 8.25pt;AlignHorz:Near;}Style9{AlignHorz:" +
				"Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Exte" +
				"ndRightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRe" +
				"ct>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single<" +
				"/Style></ColumnDivider><Width>-1</Width><Height>15</Height><DCIdx>0</DCIdx></C1D" +
				"isplayColumn></internalCols><VScrollBar><Width>16</Width><Style>Always</Style></" +
				"VScrollBar><HScrollBar><Height>16</Height><Style>None</Style></HScrollBar><Capti" +
				"onStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\"" +
				" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Sty" +
				"le11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"" +
				"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddR" +
				"owStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelecto" +
				"r\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"" +
				"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style p" +
				"arent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Head" +
				"ing\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading" +
				"\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" " +
				"me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\"" +
				" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Capt" +
				"ion\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSpl" +
				"its><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.StatusLabel.Location = new System.Drawing.Point(8, 708);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(1124, 16);
			this.StatusLabel.TabIndex = 31;
			this.StatusLabel.Tag = null;
			this.StatusLabel.TextDetached = true;
			// 
			// PositionRecallsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1148, 725);
			this.ContextMenu = this.RecallsContextMenu;
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.BizDateLabel);
			this.Controls.Add(this.BizDateCombo);
			this.Controls.Add(this.RecallActivityGrid);
			this.Controls.Add(this.BookGroupNameLabel);
			this.Controls.Add(this.BookGroupLabel);
			this.Controls.Add(this.BookGroupCombo);
			this.Controls.Add(this.RecallsGrid);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Top = 40;
			this.Enabled = false;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionRecallsForm";
			this.Text = "Position - Recalls";
			this.Load += new System.EventHandler(this.PositionRecallsForm_Load);
			this.Closed += new System.EventHandler(this.PositionRecallsForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.RecallsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RecallActivityGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BizDateLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BizDateCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void PositionRecallsForm_Load(object sender, System.EventArgs e)
		{      
			int  height  = mainForm.Height - 275;
			int  width   = mainForm.Width  - 45;                  
      
			this.Top    = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left   = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
			this.Width  = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));      
    
			this.Show();
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();

			mainForm.Alert("Please wait ... loading current recall data.", PilotState.Idle);

			try
			{                        
				mainForm.PositionAgent.RecallEvent += new RecallEventHandler(recallEventWrapper.DoEvent);        
				dataSet = mainForm.PositionAgent.RecallDataGet(mainForm.UtcOffset, "", mainForm.UserId, "PositionRecalls");                                    
        
				recallRowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
				recallDataView = new DataView(dataSet.Tables["Recalls"], recallRowFilter, "SecId ASC", DataViewRowState.CurrentRows);            
          
				BizDateCombo.HoldFields();
				BizDateCombo.DataSource = dataSet.Tables["BizDates"];      
                
				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dataSet.Tables["BookGroups"];        
				BookGroupCombo.SelectedIndex = -1;                                            
       
				recallActivityRowFilter = "RecallId = '" + RecallsGrid.Columns["RecallId"].Text + "'";
				recallActivityDataView = new DataView(dataSet.Tables["RecallActivity"], recallActivityRowFilter, "ActTime DESC", DataViewRowState.CurrentRows);
              
				RecallsGrid.SetDataBinding(recallDataView, "", true);                                                 
                    
				RecallActivityGrid.SetDataBinding(recallActivityDataView, "", true);                   
        
				if (dataSet.Tables["BizDates"].Rows.Count > 0)
				{
					BizDateCombo.Text = dataSet.Tables["BizDates"].Rows[0]["BizDate"].ToString();                  
				}
    
				BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", "");                        
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionRecallsForm.PositionRecallsForm_Load]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}

			this.IsReady = true;
			Enabled = true;
			this.Cursor = Cursors.Default;
			mainForm.Alert("Please wait ... loading current recall data..... Done!", PilotState.Idle);
		}

		private void PositionRecallsForm_Closed(object sender, System.EventArgs e)
		{ 
			RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);

			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			}
 
			mainForm.positionRecallsForm = null;
		}

		private void RecallsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{           
			if (e.Value.Length == 0)
			{
				return;
			}

			switch(RecallsGrid.Columns[e.ColIndex].DataField)
			{                         
				case "FaxStatusTime":
				case "ActTime":
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeFileFormat);
          
					break;

				case "Quantity":
				case "WillNeed":
				case "DeliveredToday":
					try
					{
						e.Value =  long.Parse(e.Value.ToString()).ToString("#,##0");            
					}
					catch {}
          
					break;

				case "OpenDateTime":
				case "BaseDueDate":
				case "MoveToDate":
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);            
          
					break;       
			}
		}    

		private void RecallsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.PositionAgent.RecallSet(
					RecallsGrid.Columns["RecallId"].Text,
					RecallsGrid.Columns["BookContact"].Text,
					RecallsGrid.Columns["MoveToDate"].Value.ToString(),
					RecallsGrid.Columns["Action"].Text,
					RecallsGrid.Columns["FaxId"].Text,
					RecallsGrid.Columns["FaxStatus"].Text,         
					"",
					RecallsGrid.Columns["WillNeed"].Value.ToString(), 
					mainForm.UserId,
					RecallsGrid.Columns["Status"].Text);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.RecallsGrid_BeforeUpdate]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}

		private void RecallsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string gridData = "";

			if (e.KeyChar.Equals( (char) 13 ))
			{
				try
				{          
					RecallsGrid.UpdateData();       
					e.Handled = true;
				}
				catch (Exception error)
				{
					Log.Write(error.Message + ". [PositionRecallsForm.RecallsGrid_KeyPress]", Log.Error, 1); 
					mainForm.Alert(error.Message, PilotState.RunFault);
					e.Handled = false;
				}
			}

			if (e.KeyChar.Equals((char)3) && RecallsGrid.SelectedRows.Count > 0)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\n";

				foreach (int row in RecallsGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
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
				mainForm.Alert("Copied " + RecallsGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
				e.Handled = true;
			}
		}

		private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
		{
			if (!(BookGroupCombo.Text.Equals("")))
			{      
				mainForm.GridFilterClear(ref RecallsGrid);
      
				if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayView"].ToString()))
				{  
					RecallsGrid.AllowUpdate    = false;        
        
					recallRowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
					recallDataView.RowFilter = recallRowFilter;
        
					FormStatusSet();
      
					BookGroupNameLabel.DataSource = dataSet.Tables["BookGroups"];
					BookGroupNameLabel.DataField = "BookName";            
      
					if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayEdit"].ToString()) && !isHistory)
					{
						RecallsGrid.AllowUpdate    = true;        
					}
					else
					{
						mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
					}      
				}
				else
				{
					recallRowFilter = "BookGroup = ''";
					recallDataView.RowFilter = recallRowFilter;
					mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
				}
			}    
		}
    
		private void RecallActivityGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{      
			if (e.Value.Length == 0)
			{
				return;
			}     
      
			switch (RecallActivityGrid.Columns[e.ColIndex].DataField)
			{                         
				case "ActTime":
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeFileFormat);
        
					break;
			}
		}

		private void RecallsGrid_BeforeOpen(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			string rowFilter = "RecallId = '" + RecallsGrid.Columns["RecallId"].Text + "'";
			recallActivityDataView.RowFilter = rowFilter;
      
			if (recallActivityDataView.Count == 0)
			{
				RecallActivityGrid.BackColor = System.Drawing.Color.FromArgb(255, 255, 155);
				RecallActivityGrid.Height =  RecallActivityGrid.RowHeight * 5;
			}
			else
			{
				RecallActivityGrid.BackColor = System.Drawing.Color.Gray;
				RecallActivityGrid.Height = ((recallActivityDataView.Count + 2) * RecallActivityGrid.RowHeight);
			}
		}    

		private void BizDateCombo_RowChange(object sender, System.EventArgs e)
		{      
			mainForm.GridFilterClear(ref RecallsGrid);

			if (!BizDateCombo.Text.Equals(""))
			{
				try
				{
					DataRow [] rows = mainForm.PositionAgent.RecallDataGet(mainForm.UtcOffset, BizDateCombo.Text, mainForm.UserId, "PositionRecalls").Tables["Recalls"].Select();      
        
					this.Cursor = Cursors.WaitCursor;
					this.IsReady = false;
          
					dataSet.Tables["Recalls"].Clear();                                                        
          
					dataSet.Tables["Recalls"].BeginLoadData();                
        
					foreach (DataRow row in rows)
					{
						dataSet.Tables["Recalls"].ImportRow(row);          
					}

					dataSet.Tables["Recalls"].EndLoadData();                    
          
					this.IsReady = true;
					this.Cursor = Cursors.Default;
        
					if (BizDateCombo.Text.Equals(mainForm.ServiceAgent.ContractsBizDate()))
					{            
						isHistory = false;
						RecallsGrid.FetchRowStyles = true;
          				  
						if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayView"].ToString()))
						{
							recallRowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";          
							recallDataView.RowFilter = recallRowFilter;                         
                  
							RecallsGrid.AllowUpdate  = false;
            
							FormStatusSet();
            
							if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayEdit"].ToString()))
							{
								RecallsGrid.AllowUpdate  = true;
							}
							else
							{
								mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
							}
						}
						else
						{
							recallRowFilter = "BookGroup = ''";
							recallDataView.RowFilter = recallRowFilter;
							RecallsGrid.AllowUpdate  = false;
							mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
						}
					}
					else
					{
						isHistory							= true;
						RecallsGrid.AllowUpdate				= false;
						CancelRecallMenuItem.Enabled		= false;
						OpenRecallMenuItem.Enabled			= false;
						MoveRecallMenuItem.Enabled			= false;
						BuyInRecallMenuItem.Enabled			= false;
						RecallsGrid.FetchRowStyles			= false;
						
						RecallDocumentPrintMenuItem.Enabled = false;
						RecallsGrid.AllowUpdate				= false;
						RecallsGrid.FetchRowStyles			= false;
					}        

         
				}
				catch (Exception error)
				{
					isHistory								= true;
					RecallsGrid.AllowUpdate				= false;
					CancelRecallMenuItem.Enabled			= false;
					OpenRecallMenuItem.Enabled			= false;
					MoveRecallMenuItem.Enabled			= false;
					BuyInRecallMenuItem.Enabled			= false;
					RecallsGrid.FetchRowStyles			= false;
			
					RecallDocumentPrintMenuItem.Enabled	= false;
					RecallsGrid.AllowUpdate				= false;
					RecallsGrid.FetchRowStyles			= false;

        
					Log.Write(error.Message + ". [PositionRecallsForm.BizDateCombo_RowChange]", Log.Error, 1);
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
			}
		}
    
		private void MailRecipientMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (RecallsGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[RecallsGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in RecallsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in RecallsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
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

				mainForm.Alert("Total: " + RecallsGrid.SelectedRows.Count + " items added to e-mail.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionRecallsForm.MailRecipientMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void RecallsGrid_FetchCellTips(object sender, C1.Win.C1TrueDBGrid.FetchCellTipsEventArgs e)
		{
			try
			{
				e.CellTip = "";

				if (e.Column == null)
				{
					return;
				}

				if (e.Column.DataColumn.DataField == "ReasonCode" && e.Column.FilterButton == false)
				{
					foreach (DataRow row in dataSet.Tables["Reasons"].Rows)
					{
						if (row["ReasonCode"].ToString().Equals(RecallsGrid[e.Row, "ReasonCode"].ToString()))
						{
							e.CellTip = row["ReasonName"].ToString();
						}          
					}                    
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.RecallsGrid_FetchCellTips]", Log.Error, 3); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}     
		}

		private void OpenRecallMenuItem_Click(object sender, System.EventArgs e)
		{
			DataRowView dataRowView = null;
			DataRow dataRow = null;
	
			try
			{
				if (RecallsGrid.SelectedRows.Count == 0)
				{
					dataRowView = (DataRowView)RecallsGrid[RecallsGrid.Row];
					dataRow = dataRowView.Row.Table.NewRow(); 												
					dataRow.ItemArray = dataRowView.Row.ItemArray;
					recallArray.Add(dataRow);
				}
				else
				{
					foreach (int i in RecallsGrid.SelectedRows)
					{
						dataRowView = (DataRowView)RecallsGrid[i];
						dataRow = dataRowView.Row.Table.NewRow(); 												
						dataRow.ItemArray = dataRowView.Row.ItemArray;
						recallArray.Add(dataRow);
					}
				}

				SendRecallsDelegate sendRecallsDelegate = new SendRecallsDelegate(SendRecalls);
				sendRecallsDelegate.BeginInvoke(recallArray, "O", null, null);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.OpenRecallMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void BuyInRecallMenuItem_Click(object sender, System.EventArgs e)
		{
			DataRowView dataRowView = null;
			DataRow dataRow = null;
	
			try
			{
				if (RecallsGrid.SelectedRows.Count == 0)
				{
					dataRowView = (DataRowView)RecallsGrid[RecallsGrid.Row];
					dataRow = dataRowView.Row.Table.NewRow(); 												
					dataRow.ItemArray = dataRowView.Row.ItemArray;
					recallArray.Add(dataRow);
				}
				else
				{
					foreach (int i in RecallsGrid.SelectedRows)
					{
						dataRowView = (DataRowView)RecallsGrid[i];
						dataRow = dataRowView.Row.Table.NewRow(); 												
						dataRow.ItemArray = dataRowView.Row.ItemArray;
						recallArray.Add(dataRow);
					}
				}

				SendRecallsDelegate sendRecallsDelegate = new SendRecallsDelegate(SendRecalls);
				sendRecallsDelegate.BeginInvoke(recallArray, "B", null, null);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.BuyInRecallMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void CancelRecallMenuItem_Click(object sender, System.EventArgs e)
		{
			DataRowView dataRowView = null;
			DataRow dataRow = null;
	
			try
			{
				if (RecallsGrid.SelectedRows.Count == 0)
				{
					dataRowView = (DataRowView)RecallsGrid[RecallsGrid.Row];
					dataRow = dataRowView.Row.Table.NewRow(); 												
					dataRow.ItemArray = dataRowView.Row.ItemArray;
					recallArray.Add(dataRow);
				}
				else
				{
					foreach (int i in RecallsGrid.SelectedRows)
					{
						if (!RecallsGrid.Columns["Status"].CellText(i).Equals("C"))
						{
							dataRowView = (DataRowView)RecallsGrid[i];
							dataRow = dataRowView.Row.Table.NewRow(); 												
							dataRow.ItemArray = dataRowView.Row.ItemArray;
							recallArray.Add(dataRow);
						}
					}
				}

				DeleteRecallsDelegate deleteRecallsDelegate = new DeleteRecallsDelegate(DeleteRecalls);
				deleteRecallsDelegate.BeginInvoke(recallArray, null, null);
			}
			catch(Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.CloseRecallMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
   
		/*private void FaxStatusMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				mainForm.PositionAgent.FaxStatusUpdate();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionAgent.FaxStatusMenuItem_Click]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}*/

		/*private void FaxSendMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{  
				mainForm.PositionAgent.RecallSet(
					RecallsGrid.Columns["RecallId"].Text,
					RecallsGrid.Columns["BookContact"].Text,
					RecallsGrid.Columns["MoveToDate"].Value.ToString(),
					RecallsGrid.Columns["Action"].Text,
					RecallsGrid.Columns["FaxId"].Text,
					"Send Pending",
					"",
					"",
					mainForm.UserId,
					RecallsGrid.Columns["Status"].Text);

				mainForm.PositionAgent.RecallSet(
					RecallsGrid.Columns["RecallId"].Text, 
					RecallsGrid.Columns["BookContact"].Text,
					RecallsGrid.Columns["MoveToDate"].Value.ToString(),
					RecallsGrid.Columns["Action"].Text,
					RecallsGrid.Columns["FaxId"].Text,
					"OK",
					"",
					"",
					mainForm.UserId,
					RecallsGrid.Columns["Status"].Text);
			}   
			catch(Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.FaxSendMenuItem_Click]", Log.Error, 1);         
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void FaxCancelMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{                           
				mainForm.PositionAgent.RecallSet(
					RecallsGrid.Columns["RecallId"].Text,
					RecallsGrid.Columns["BookContact"].Text,
					RecallsGrid.Columns["MoveToDate"].Value.ToString(),
					RecallsGrid.Columns["Action"].Text,
					RecallsGrid.Columns["FaxId"].Text,
					"Cancel Pending",
					"",
					"",
					mainForm.UserId,
					RecallsGrid.Columns["Status"].Text);    
			}      
			catch(Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.FaxCancelMenuItem_Click]", Log.Error, 1);         
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}*/

		private void DockTopMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.Top;
			this.ControlBox = false;
			this.Text = "";
		}

		private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.Bottom;
			this.ControlBox = false;
			this.Text = "";
		}

		private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.None;
			this.ControlBox = true;
			this.Text = TEXT;
		}

		private void ShowRecallIdMenuItem_Click(object sender, System.EventArgs e)
		{    
			ShowRecallIdMenuItem.Checked = !ShowRecallIdMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["RecallId"].Visible = ShowRecallIdMenuItem.Checked;
		}

		private void ShowContractIdMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowContractIdMenuItem.Checked = !ShowContractIdMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["ContractId"].Visible = ShowContractIdMenuItem.Checked;      
		}

		private void ShowMoveToDateMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowMoveToDateMenuItem.Checked = !ShowMoveToDateMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["MoveToDate"].Visible = ShowMoveToDateMenuItem.Checked;
		}

		private void ShowSequenceNumberMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowSequenceNumberMenuItem.Checked = !ShowSequenceNumberMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["SequenceNumber"].Visible = ShowSequenceNumberMenuItem.Checked;
		}

		private void ShowFaxIdMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowFaxIdMenuItem.Checked = !ShowFaxIdMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["FaxId"].Visible = ShowFaxIdMenuItem.Checked;
		}

		private void ShowFaxStatusMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowFaxStatusMenuItem.Checked = !ShowFaxStatusMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["FaxStatus"].Visible = ShowFaxStatusMenuItem.Checked;
		}

		private void ShowFaxStatusTimeMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowFaxStatusTimeMenuItem.Checked = !ShowFaxStatusTimeMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["FaxStatusTime"].Visible = ShowFaxStatusTimeMenuItem.Checked;
		}

		private void FormStatusSet()
		{
			if (RecallsGrid.SelectedRows.Count > 0)
			{
				StatusLabel.Text = "Selecting " + RecallsGrid.SelectedRows.Count.ToString("#,##0") + " items of " + recallDataView.Count.ToString("#,##0") + " shown in grid.";
			}
			else
			{
				StatusLabel.Text = "Showing " + recallDataView.Count.ToString("#,##0") + " items in grid.";
			}
		}

		private void RecallsGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			FormStatusSet();
		}

		private void RecallsGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref RecallsGrid);

				if (gridFilter.Equals(""))
				{
					recallDataView.RowFilter = recallRowFilter;
				}
				else
				{
					recallDataView.RowFilter = recallRowFilter + " AND " + gridFilter;
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}

			FormStatusSet();
		}

		private void RecallsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{                       
			if (DateTime.Parse(RecallsGrid.Columns["BaseDueDate"].CellText(e.Row)) <= DateTime.Parse(BizDateCombo.Text))
			{
				e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 155);
			}
                
			if (RecallsGrid.Columns["Status"].CellText(e.Row).Equals("B"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.PeachPuff;
			}                  
		}

		private void MoveRecallMenuItem_Click(object sender, System.EventArgs e)
		{          
			string moveToDate = mainForm.ServiceAgent.BizDateNext();
		  
			try
			{
				if (RecallsGrid.SelectedRows.Count == 0)
				{								  
					mainForm.PositionAgent.RecallSet(
						RecallsGrid.Columns["RecallId"].Text, 
						"",
						moveToDate, 
						"",
						"", 
						"", 
						"", 
						"", 
						mainForm.UserId, 
						"M");
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionRecallsForm.MoveRecallMenuItem_Click]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}      
		}

		private void RecallsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if (!RecallsGrid.Columns["SecId"].Text.Equals(secId))
				{
					secId = RecallsGrid.Columns["SecId"].Text;

					this.Cursor = Cursors.WaitCursor;                
     
					mainForm.SecId = secId;

					this.Cursor = Cursors.Default;
				}
      
				if (RecallsGrid.AllowUpdate)
				{
					switch(RecallsGrid.Columns["Status"].Text)
					{
						case "B":              
							MoveRecallMenuItem.Enabled			= true;
							CancelRecallMenuItem.Enabled			= true;
							OpenRecallMenuItem.Enabled			= true;            
							BuyInRecallMenuItem.Enabled			= false;				
							RecallDocumentPrintMenuItem.Enabled	= true;
							RecallDocumentFaxMenuItem.Enabled = true;
							break;

						case "M":              
							MoveRecallMenuItem.Enabled   = false;
							CancelRecallMenuItem.Enabled = true;
							OpenRecallMenuItem.Enabled   = true;            
							BuyInRecallMenuItem.Enabled  = false;
							RecallDocumentPrintMenuItem.Enabled = true;
							RecallDocumentFaxMenuItem.Enabled = true;
							break;

						case "P":              
							MoveRecallMenuItem.Enabled			= true;
							CancelRecallMenuItem.Enabled			= true;
							OpenRecallMenuItem.Enabled			= true;            
							BuyInRecallMenuItem.Enabled			= true;
							RecallDocumentPrintMenuItem.Enabled	= true;
							RecallDocumentFaxMenuItem.Enabled = true;
							break;
            
						case "O": 
							MoveRecallMenuItem.Enabled			= true;
							CancelRecallMenuItem.Enabled			= true;
							OpenRecallMenuItem.Enabled			= false;            
							BuyInRecallMenuItem.Enabled			= true;
							RecallDocumentPrintMenuItem.Enabled	= true;
							RecallDocumentFaxMenuItem.Enabled = true;
							break;              
                                
						case "F":
						case "C": 
							CancelRecallMenuItem.Enabled			= false;
							MoveRecallMenuItem.Enabled			= false;
							OpenRecallMenuItem.Enabled			= false;            
							BuyInRecallMenuItem.Enabled			= false;
							RecallDocumentPrintMenuItem.Enabled	= false;
							RecallDocumentFaxMenuItem.Enabled = false;
							break;                    
					}
				}

				if (RecallsGrid.SelectedRows.Count > 0)
				{
					RecallDocumentPrintMenuItem.Enabled	= false;
				}
			}
			catch 
			{
				this.Cursor = Cursors.WaitCursor;
  
				mainForm.SecId = "";
        
				CancelRecallMenuItem.Enabled		= false;
				MoveRecallMenuItem.Enabled			= false;
				OpenRecallMenuItem.Enabled			= false;            
				BuyInRecallMenuItem.Enabled			= false;
				RecallDocumentFaxMenuItem.Enabled = false;
				RecallDocumentPrintMenuItem.Enabled = false;
        
				this.Cursor = Cursors.Default;
			}    
		}

		private void ShowDeliveredTodayMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowDeliveredTodayMenuItem.Checked = !ShowDeliveredTodayMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["DeliveredToday"].Visible = ShowDeliveredTodayMenuItem.Checked;
		}

		private void ShowWillNeedMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowWillNeedMenuItem.Checked = !ShowWillNeedMenuItem.Checked;

			RecallsGrid.Splits[0,0].DisplayColumns["WillNeed"].Visible = ShowWillNeedMenuItem.Checked;
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void RecallsGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.X <= RecallsGrid.RecordSelectorWidth && e.Y <= RecallsGrid.RowHeight)
			{
				if (RecallsGrid.SelectedRows.Count.Equals(0))
				{
					for (int i = 0; i < RecallsGrid.Splits[0,0].Rows.Count; i++)
					{
						RecallsGrid.SelectedRows.Add(i);
					}

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.Columns)
					{
						RecallsGrid.SelectedCols.Add(dataColumn);
					}
				}
				else
				{
					RecallsGrid.SelectedRows.Clear();
					RecallsGrid.SelectedCols.Clear();
				}
			}
		}

		private void RecallDocumentPrintMenuItem_Click(object sender, System.EventArgs e)
		{
		
			StreamWriter sr = null;

			try
			{				
				string fileName = Standard.ConfigValue("TempPath") + RecallsGrid.Columns["RecallId"].Text + ".txt";							
				
				sr = File.CreateText(fileName);
				sr.Write(mainForm.PositionAgent.RecallTermNoticeDocument(RecallsGrid.Columns["RecallId"].Text));				
			
				mainForm.Alert("Wrote recall document file to : " + fileName, PilotState.RunFault);

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.EnableRaisingEvents=false;
				proc.StartInfo.FileName="notepad";
				proc.StartInfo.Arguments= fileName;
				proc.Start();		
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanForm.GenerateLoanDocumentMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
				}
			}
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
      this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();
			excel.ExportGridToExcel(ref RecallsGrid);

      this.Cursor = Cursors.Default;
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (RecallsGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[RecallsGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in RecallsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in RecallsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RecallsGrid.SelectedCols)
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
                
				Clipboard.SetDataObject(gridData, true);

				mainForm.Alert("Total: " + RecallsGrid.SelectedRows.Count + " items added to clipboard.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionRecallsForm.SendToClipboardMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void RecallsGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			if (e.Column.DataColumn.DataField.Equals("BookContact"))
			{
				if (!e.OldValue.ToString().Equals(RecallsGrid.Columns["BookContact"].Text))
				{
					mainForm.PositionAgent.RecallBookContactSet(
						BookGroupCombo.Text,
						RecallsGrid.Columns["Book"].Text,
						RecallsGrid.Columns["BookContact"].Text,
						mainForm.UserId);
				}
			}
		}

		private void RecallDocumentFaxMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{			
				Email email = new Email();
				DataTable dataTable = dataSet.Tables["Recalls"].Clone();
				
				if (RecallsGrid.SelectedRows.Count == 0)
				{
					RecallsGrid.SelectedRows.Add(RecallsGrid.Row);
				}				
				foreach(int count in RecallsGrid.SelectedRows)
				{
					foreach (DataRow dr in dataSet.Tables["Recalls"].Rows)
					{
						if (RecallsGrid.Columns["RecallId"].CellText(count).Equals(dr["RecallId"].ToString()))
						{
							dataTable.Rows.Add(dr.ItemArray);
						}
					}
				}
				
				DataView dv = new DataView(dataTable, "", "Book ASC", DataViewRowState.CurrentRows);			
				
				string book = "";
				string recallDocument = "";
				string recipient = "";
				string tempString = "";
				string recallIdList = "";
				string reportName = "";
				int		 counter = 0;
				int		 totalCounter = 0;

				for(int i = 0; i < dv.Count; i++)
				{
					if (dv[i]["Book"].ToString().Equals(book))
					{
						tempString = mainForm.PositionAgent.RecallTermNoticeDocument(dv[i]["RecallId"].ToString());
						counter ++;
						totalCounter ++;
						recallDocument += Tools.SplitItem (tempString, "|", 2);
							
						recallIdList += "Recall Id: " + dv[i]["RecallId"].ToString() + "\r\n";
						recipient = @"[Fax: " + Tools.SplitItem (tempString, "|", 0) + "_" + counter.ToString() + "_" + reportName + "@" + Tools.SplitItem (tempString, "|", 1) + "]";														
							
						if ((i+1 == dv.Count))
						{							
							email.Send(recipient, "", "Recall Notices For Date: " + mainForm.ServiceAgent.BizDate(), recallDocument, "", true);	
						}
					}
					else
					{						
						if (!book.Equals(""))
						{							
							email.Send(recipient, "", "Recall Notices For Date: " + mainForm.ServiceAgent.BizDate(), recallDocument, "", true);		
						}

						counter = 0;							
						counter ++;

						totalCounter ++;

						tempString = mainForm.PositionAgent.RecallTermNoticeDocument(dv[i]["RecallId"].ToString());							
						recallDocument = Tools.SplitItem (tempString, "|", 2);
							
						reportName = "Fax ID_" +  dv.Table.Rows[i]["BookGroup"].ToString() + "_" + dv[i]["Book"].ToString() + "_" + DateTime.Now.Ticks.ToString();
							
						recallIdList += "\r\n\r\n" + reportName + "\r\n";
						recallIdList += "Recall Id: " + dv[i]["RecallId"].ToString() + "\r\n";
						recipient = @"[Fax: " + Tools.SplitItem (tempString, "|", 0) + "_" + counter.ToString() + "_" + reportName + "@" + Tools.SplitItem (tempString, "|", 1) + "]";							
						
						book = dv[i]["Book"].ToString();							

						if (i+1 == dv.Count)
						{								
							email.Send(recipient, "", "Recall Notices For Date: " + mainForm.ServiceAgent.BizDate(), recallDocument, "", true);									
						}
					}						
				}									
								
				mainForm.Alert("Sending " + totalCounter + " recall notices via RightFax.", PilotState.Normal);
				email.Send(mainForm.UserEmailAddress, "", "Recall notices for : " + DateTime.Now.ToString(Standard.DateTimeFormat), recallIdList, "", true);
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionRecallsForm.RecallDocumentFaxMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}		
		}
	}
}
