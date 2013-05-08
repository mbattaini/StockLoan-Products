// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC  2005, 2006  All rights reserved.

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class ContractInformationControl : System.Windows.Forms.UserControl
	{
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ContractInformationGrid;
		private MainForm mainForm;
    private string secId;
		private string bookGroup;
		private string contractType;
		private DataSet dataSet;
		private System.ComponentModel.Container components = null;
		private const decimal E = 0.000000000000000001M;

		public ContractInformationControl()
		{
			try
			{
				InitializeComponent();				
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContractInformationControl));
			this.ContractInformationGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.ContractInformationGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ContractInformationGrid
			// 
			this.ContractInformationGrid.AllowUpdate = false;
			this.ContractInformationGrid.CaptionHeight = 17;
			this.ContractInformationGrid.ColumnFooters = true;
			this.ContractInformationGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ContractInformationGrid.EmptyRows = true;
			this.ContractInformationGrid.ExtendRightColumn = true;
			this.ContractInformationGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ContractInformationGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ContractInformationGrid.Location = new System.Drawing.Point(0, 0);
			this.ContractInformationGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.ContractInformationGrid.Name = "ContractInformationGrid";
			this.ContractInformationGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ContractInformationGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ContractInformationGrid.PreviewInfo.ZoomFactor = 75;
			this.ContractInformationGrid.RecordSelectorWidth = 16;
			this.ContractInformationGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ContractInformationGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ContractInformationGrid.RowHeight = 15;
			this.ContractInformationGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ContractInformationGrid.Size = new System.Drawing.Size(476, 160);
			this.ContractInformationGrid.TabIndex = 0;
			this.ContractInformationGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ContractInformationGrid_FormatText);
			this.ContractInformationGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BookGroup\" " +
				"DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Book\" DataField=\"Book\"><ValueItems /><GroupInfo /></C1DataColum" +
				"n><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"QuantitySettled\" NumberF" +
				"ormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Amount\" DataField=\"AmountSettled\" NumberFormat=\"FormatText E" +
				"vent\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=" +
				"\"Rate\" DataField=\"Rate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo" +
				" /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PC\" DataField=\"PoolCode\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGr" +
				"id.Design.ContextWrapper\"><Data>RecordSelector{AlignImage:Center;}Style50{}Style" +
				"51{}Caption{AlignHorz:Center;}Style27{}Normal{Font:Verdana, 8.25pt;}Selected{For" +
				"eColor:HighlightText;BackColor:Highlight;}Editor{}Style18{BackColor:255, 251, 24" +
				"2;}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;Bac" +
				"kColor:Snow;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style42{Font:Verd" +
				"ana, 8.25pt, style=Bold;BackColor:255, 251, 242;}Style46{AlignHorz:Center;}Style" +
				"47{AlignHorz:Center;BackColor:FloralWhite;}Style37{}Style29{AlignHorz:Far;BackCo" +
				"lor:GhostWhite;}Style28{AlignHorz:Near;}HighlightRow{ForeColor:HighlightText;Bac" +
				"kColor:Highlight;}Style26{}Style25{}Footer{}Style23{AlignHorz:Near;BackColor:Sno" +
				"w;}Style22{AlignHorz:Near;}Style21{}Style20{}Group{AlignVert:Center;Border:None," +
				",0, 0, 0, 0;BackColor:ControlDark;}Style3{}Inactive{ForeColor:InactiveCaptionTex" +
				"t;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Style6{}Heading{Wrap:True;B" +
				"ackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Cente" +
				"r;}Style49{}Style48{Font:Verdana, 8.25pt, style=Bold;BackColor:255, 251, 242;}St" +
				"yle24{Font:Verdana, 8.25pt, style=Bold;BackColor:255, 251, 242;}Style7{}Style8{}" +
				"Style1{}Style5{}Style41{AlignHorz:Far;BackColor:MintCream;}Style40{AlignHorz:Nea" +
				"r;}Style43{}FilterBar{}Style45{}Style44{}Style4{}Style9{}Style38{}Style39{}Style" +
				"36{Font:Verdana, 8.25pt, style=Bold;BackColor:255, 251, 242;}Style12{}Style34{Al" +
				"ignHorz:Near;}Style35{AlignHorz:Far;BackColor:GhostWhite;}Style32{}Style33{}Styl" +
				"e30{Font:Verdana, 8.25pt, style=Bold;BackColor:255, 251, 242;}Style31{}Style2{}<" +
				"/Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle" +
				"=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight" +
				"=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWid" +
				"th=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><C" +
				"aptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Styl" +
				"e5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Filte" +
				"rBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle pare" +
				"nt=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLigh" +
				"tRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" m" +
				"e=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle par" +
				"ent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\"" +
				" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" " +
				"/><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style20\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Height>15</H" +
				"eight><FooterDivider>False</FooterDivider><DCIdx>0</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\"" +
				" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>" +
				"Gainsboro,Single</ColumnDivider><Height>15</Height><FooterDivider>False</FooterD" +
				"ivider><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e32\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><He" +
				"ight>15</Height><FooterDivider>False</FooterDivider><DCIdx>2</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style paren" +
				"t=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><Colu" +
				"mnDivider>Gainsboro,Single</ColumnDivider><Height>15</Height><FooterDivider>Fals" +
				"e</FooterDivider><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterSt" +
				"yle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><" +
				"GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnD" +
				"ivider><Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>4</DCIdx></" +
				"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><S" +
				"tyle parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Vis" +
				"ible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>40</Width><Height>15</" +
				"Height><DCIdx>5</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 472, 1" +
				"56</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Split" +
				"s><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading" +
				"\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /" +
				"><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" />" +
				"<Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" />" +
				"<Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Styl" +
				"e parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /" +
				"><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><h" +
				"orzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Default" +
				"RecSelWidth><ClientArea>0, 0, 472, 156</ClientArea><PrintPageHeaderStyle parent=" +
				"\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// ContractInformationControl
			// 
			this.Controls.Add(this.ContractInformationGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ContractInformationControl";
			this.Size = new System.Drawing.Size(476, 160);
			((System.ComponentModel.ISupportInitialize)(this.ContractInformationGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FooterSet()
		{
			try
			{
				decimal rateYield = 0M;      			      
				decimal rate;
				decimal amount = 0;
				long		quantity = 0;

				foreach (DataRow dr in dataSet.Tables["Contracts"].Rows)
				{
					rateYield += (decimal)dr["Rate"] * (decimal)dr["AmountSettled"];					
          					
					quantity += long.Parse(dr["QuantitySettled"].ToString());
					amount+= decimal.Parse(dr["AmountSettled"].ToString());
				}
				
				rate = rateYield / (amount + E);
				ContractInformationGrid.Columns["Rate"].FooterText = rate.ToString("#0.000");
				ContractInformationGrid.Columns["Quantity"].FooterText = quantity.ToString("#,##0");
				ContractInformationGrid.Columns["Amount"].FooterText = amount.ToString("#,##0.00");
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ContractInformationGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "QuantitySettled":
					try
					{
						e.Value = long.Parse(ContractInformationGrid.Columns["QuantitySettled"].CellValue(e.Row).ToString()).ToString("#,##0");
					}
					catch {}
					break;

				case "AmountSettled":
					try
					{
						e.Value = decimal.Parse(ContractInformationGrid.Columns["AmountSettled"].CellValue(e.Row).ToString()).ToString("#,##0.00");
					}
					catch {}
					break;

				case "Rate":
					try
					{
						e.Value = decimal.Parse(ContractInformationGrid.Columns["Rate"].CellValue(e.Row).ToString()).ToString("#0.000");
					}
					catch {}
					break;
			}
		}

		public string BookGroup
		{
			get
			{
				return bookGroup;
			}
		
			set
			{
				try
				{
					bookGroup = value.ToString();
				
					if (!bookGroup.Equals("") &&!secId.Equals("") && !contractType.Equals(""))					
					{
					//	dataSet = mainForm.PositionAgent.ContractDataGet(bookGroup, contractType, secId);
						ContractInformationGrid.SetDataBinding(dataSet, "Contracts", true);
						FooterSet();
					}
					else
					{
						ContractInformationGrid.ClearFields();
					}
				}
				catch {}
			}
		}

		public MainForm MainForm
		{
			set
			{
				mainForm = value;
			}
		}

		public string SecId
		{
			get
			{
				return secId;
			}
		
			set
			{
				try
				{
					secId = value.ToString();

					if (!bookGroup.Equals("") &&!secId.Equals("") && !contractType.Equals(""))
					{					
					//	dataSet = mainForm.PositionAgent.ContractDataGet(bookGroup, contractType, secId);
						ContractInformationGrid.SetDataBinding(dataSet, "Contracts", true);
						FooterSet();
					}
					else
					{
						ContractInformationGrid.ClearFields();
					}
				}
				catch {}
			}
		}

		public string ContractType
		{
			get
			{
				return contractType;
			}
		
			set
			{
				try
				{
					contractType = value.ToString();

					if (!bookGroup.Equals("") &&!secId.Equals("") && !contractType.Equals(""))
					{
						if (ContractType.Equals("B"))
						{
							ContractInformationGrid.Caption = "Open Borrows";
						}
						else if (ContractType.Equals("L"))
						{
							ContractInformationGrid.Caption = "Open Loans";
						}						
						
						//dataSet = mainForm.PositionAgent.ContractDataGet(bookGroup, contractType, secId);
						ContractInformationGrid.SetDataBinding(dataSet, "Contracts", true);
						FooterSet();
					}
					else
					{
						ContractInformationGrid.ClearFields();
					}
				}
				catch {}
			}
		}
	
	}
}
