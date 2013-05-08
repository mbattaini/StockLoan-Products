using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using	System.Data;
using Anetics.Common;

namespace Anetics.Medalist
{
	/// <summary>
	/// Summary description for FundingRatesHistoryDropDown.
	/// </summary>
	public class InventoryFundingRatesHistoryDropDown : C1.Win.C1Input.DropDownForm
	{
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid FundingRatesGrid;
		private MainForm mainForm;

		private System.ComponentModel.Container components = null;

		public InventoryFundingRatesHistoryDropDown()
		{			
				InitializeComponent();
					
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InventoryFundingRatesHistoryDropDown));
			this.FundingRatesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.FundingRatesGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// FundingRatesGrid
			// 
			this.FundingRatesGrid.AllowColSelect = false;
			this.FundingRatesGrid.AlternatingRows = true;
			this.FundingRatesGrid.CaptionHeight = 17;
			this.FundingRatesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FundingRatesGrid.ExtendRightColumn = true;
			this.FundingRatesGrid.FetchRowStyles = true;
			this.FundingRatesGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FundingRatesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.FundingRatesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.FundingRatesGrid.Location = new System.Drawing.Point(0, 0);
			this.FundingRatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.FundingRatesGrid.Name = "FundingRatesGrid";
			this.FundingRatesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.FundingRatesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.FundingRatesGrid.PreviewInfo.ZoomFactor = 75;
			this.FundingRatesGrid.RecordSelectors = false;
			this.FundingRatesGrid.RecordSelectorWidth = 16;
			this.FundingRatesGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.FundingRatesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.FundingRatesGrid.RowHeight = 16;
			this.FundingRatesGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.FundingRatesGrid.Size = new System.Drawing.Size(485, 300);
			this.FundingRatesGrid.TabIndex = 1;
			this.FundingRatesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FundingRatesGrid_FormatText);
			this.FundingRatesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BizDate\" Da" +
				"taField=\"BizDate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Fed Funds Rate\" DataField=\"FedFundi" +
				"ngOpenRate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Libor Funds Rate\" DataField=\"LiborFunding" +
				"Rate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn>" +
				"<C1DataColumn Level=\"0\" Caption=\"ActUserId\" DataField=\"ActUserId\"><ValueItems />" +
				"<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField" +
				"=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>R" +
				"ecordSelector{AlignImage:Center;}Caption{AlignHorz:Center;}Style27{}Normal{Font:" +
				"Verdana, 8.25pt;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}S" +
				"tyle18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;}Style16{}Style1" +
				"7{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style47{}Style7{}Style38{}" +
				"Style36{AlignHorz:Near;}Style37{AlignHorz:Near;}OddRow{BackColor:White;}Style29{" +
				"}Style28{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{}Sty" +
				"le25{}Footer{}Style23{}Style22{}Style21{AlignHorz:Far;}Style20{AlignHorz:Near;}G" +
				"roup{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{Fo" +
				"reColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:LightCy" +
				"an;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:Contr" +
				"olText;BackColor:Control;}Style24{}Style9{}Style6{}Style1{}Style3{}Style41{}Styl" +
				"e40{}Style43{AlignHorz:Near;}Style42{AlignHorz:Near;}Style45{}Style44{}Style4{}S" +
				"tyle46{}Style8{}Style39{}FilterBar{BackColor:SeaShell;}Style5{}Style34{}Style35{" +
				"}Style32{}Style33{}Style30{AlignHorz:Near;}Style31{AlignHorz:Far;}Style2{}</Data" +
				"></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Alw" +
				"ays\" AllowColSelect=\"False\" Name=\"\" AlternatingRowStyle=\"True\" CaptionHeight=\"17" +
				"\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" Fetc" +
				"hRowStyles=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRec" +
				"SelWidth=\"16\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" HorizontalScrollGr" +
				"oup=\"1\"><SplitSize>4</SplitSize><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Ed" +
				"itorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style" +
				"8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Foot" +
				"er\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent" +
				"=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" />" +
				"<InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"" +
				"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedS" +
				"tyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><inter" +
				"nalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style par" +
				"ent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorS" +
				"tyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style1" +
				"9\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><Co" +
				"lumnDivider>Gainsboro,Single</ColumnDivider><Height>16</Height><Locked>True</Loc" +
				"ked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24" +
				"\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width" +
				">120</Width><Height>16</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style30\" /><Style paren" +
				"t=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"Style3\" me=\"Style32\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style33\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style35\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style34\" /><Visible>True</Visible><Colu" +
				"mnDivider>Gainsboro,Single</ColumnDivider><Width>120</Width><Height>16</Height><" +
				"Locked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style36\" /><Style parent=\"Style1\" me=\"Style37\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style38\" /><EditorStyle parent=\"Style5\" me=\"Style39\" " +
				"/><GroupHeaderStyle parent=\"Style1\" me=\"Style41\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style40\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Width>120</Width><Height>16</Height><Locked>True</Locked><DCIdx>3</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style42\"" +
				" /><Style parent=\"Style1\" me=\"Style43\" /><FooterStyle parent=\"Style3\" me=\"Style4" +
				"4\" /><EditorStyle parent=\"Style5\" me=\"Style45\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style47\" /><GroupFooterStyle parent=\"Style1\" me=\"Style46\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>120</Width><Heigh" +
				"t>16</Height><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn></internalCo" +
				"ls><ClientRect>0, 0, 481, 296</ClientRect><BorderSide>0</BorderSide></C1.Win.C1T" +
				"rueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style " +
				"parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pare" +
				"nt=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style paren" +
				"t=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"N" +
				"ormal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"" +
				"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style paren" +
				"t=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><" +
				"vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Def" +
				"aultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 481, 296</ClientArea><P" +
				"rintPageHeaderStyle parent=\"\" me=\"Style28\" /><PrintPageFooterStyle parent=\"\" me=" +
				"\"Style29\" /></Blob>";
			// 
			// InventoryFundingRatesHistoryDropDown
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(485, 300);
			this.Controls.Add(this.FundingRatesGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "InventoryFundingRatesHistoryDropDown";
			this.Text = "FundingRatesHistoryDropDown";
			this.Open += new System.EventHandler(this.FundingRatesHistoryDropDown_Open);
			this.Load += new System.EventHandler(this.FundingRatesHistoryDropDown_Load);
			((System.ComponentModel.ISupportInitialize)(this.FundingRatesGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FundingRatesHistoryDropDown_Load(object sender, System.EventArgs e)
		{
			try
			{							
				if (mainForm == null)
				{				
					mainForm = (MainForm) this.OwnerControl.Parent.Parent.Parent;				
				}
	
				FundingRatesGrid.Splits[0].DisplayColumns["FedFundingOpenRate"].Visible = mainForm.ShowFedFunds;
				FundingRatesGrid.Splits[0].DisplayColumns["LiborFundingRate"].Visible = mainForm.ShowLiborFunds;
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void FundingRatesHistoryDropDown_Open(object sender, System.EventArgs e)
		{
			try
			{
				if (mainForm == null)
				{				
					mainForm = (MainForm) this.OwnerControl.Parent.Parent.Parent;				
				}

				DataSet ds = mainForm.ShortSaleAgent.InventoryFundingRatesHistoryGet(int.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryFundingRatesHistoryDayCount", "10")), mainForm.UtcOffset);
				FundingRatesGrid.SetDataBinding(ds, "InventoryFundingRatesHistory", true);
			
				this.Height = FundingRatesGrid.CaptionHeight + (FundingRatesGrid.Splits[0].Rows.Count * FundingRatesGrid.RowHeight)+ 8;			
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void FundingRatesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{
				case "FedFundingOpenRate":	
				case "LiborFundingRate":
					try
					{						
						e.Value = decimal.Parse(e.Value.ToString()).ToString("##0.000");
					}
					catch {}
					break;

				case "BizDate":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch {}
					break;

				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
					}
					catch {}
					break;
								
			}
		}
	}
}
