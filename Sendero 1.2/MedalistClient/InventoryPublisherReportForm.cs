using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;
using Anetics.Common;
using Anetics.Medalist;

namespace Anetics.Medalist
{
	/// <summary>
	/// Summary description for InventoryPublisherReportForm.
	/// </summary>
	public class InventoryPublisherReportForm : System.Windows.Forms.Form
	{
		private	MainForm mainForm;
		private DataSet dataSet;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid PublisherReportsGrid;
		
		private System.ComponentModel.Container components = null;

		public InventoryPublisherReportForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InventoryPublisherReportForm));
			this.PublisherReportsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.PublisherReportsGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// PublisherReportsGrid
			// 
			this.PublisherReportsGrid.AllowAddNew = true;
			this.PublisherReportsGrid.AllowColSelect = false;
			this.PublisherReportsGrid.AllowDelete = true;
			this.PublisherReportsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.PublisherReportsGrid.CaptionHeight = 18;
			this.PublisherReportsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.PublisherReportsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PublisherReportsGrid.EmptyRows = true;
			this.PublisherReportsGrid.ExtendRightColumn = true;
			this.PublisherReportsGrid.GroupByAreaVisible = false;
			this.PublisherReportsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.PublisherReportsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.PublisherReportsGrid.Location = new System.Drawing.Point(0, 0);
			this.PublisherReportsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.PublisherReportsGrid.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None;
			this.PublisherReportsGrid.Name = "PublisherReportsGrid";
			this.PublisherReportsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.PublisherReportsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.PublisherReportsGrid.PreviewInfo.ZoomFactor = 75;
			this.PublisherReportsGrid.RecordSelectorWidth = 16;
			this.PublisherReportsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.PublisherReportsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.PublisherReportsGrid.RowHeight = 15;
			this.PublisherReportsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.PublisherReportsGrid.Size = new System.Drawing.Size(816, 365);
			this.PublisherReportsGrid.TabAcrossSplits = true;
			this.PublisherReportsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.PublisherReportsGrid.TabIndex = 1;
			this.PublisherReportsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.PublisherReportsGrid_BeforeUpdate);
			this.PublisherReportsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Report Name" +
				"\" DataField=\"ReportName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Report Stored Proc\" DataField=\"ReportStoredProc\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Report Descripti" +
				"on\" DataField=\"ReportDescription\"><ValueItems /><GroupInfo /></C1DataColumn></Da" +
				"taCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style12{}S" +
				"tyle278{}Style279{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Select" +
				"ed{ForeColor:HighlightText;BackColor:Highlight;}Style271{}Style272{}Style273{}St" +
				"yle274{}Style275{}Style276{}Style277{}Style18{}Style14{AlignHorz:Near;}Style15{}" +
				"Style16{}Style17{}Style10{}Style11{}OddRow{}Style13{AlignHorz:Near;}Style282{}St" +
				"yle281{}Style280{AlignHorz:Near;}HighlightRow{ForeColor:HighlightText;BackColor:" +
				"Highlight;}RecordSelector{AlignImage:Center;}Footer{}Inactive{ForeColor:Inactive" +
				"CaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True" +
				";BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Cen" +
				"ter;}Style76{}Style77{}Style74{}Style75{}Style7{AlignHorz:Near;}FilterBar{}Style" +
				"4{}Style9{}Style8{AlignHorz:Near;}Style403{}Style5{}Group{AlignVert:Center;Borde" +
				"r:None,,0, 0, 0, 0;BackColor:ControlDark;}Editor{}Style6{}Style1{AlignHorz:Near;" +
				"}Style3{}Style2{AlignHorz:Near;BackColor:WhiteSmoke;}</Data></Styles><Splits><C1" +
				".Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" AllowColSelect=\"False\" Name=\"Split[" +
				"0,0]\" AllowRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFo" +
				"oterHeight=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSe" +
				"lectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGro" +
				"up=\"3\"><CaptionStyle parent=\"Heading\" me=\"Style280\" /><EditorStyle parent=\"Edito" +
				"r\" me=\"Style272\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style278\" /><FilterBarStyl" +
				"e parent=\"FilterBar\" me=\"Style403\" /><FooterStyle parent=\"Footer\" me=\"Style274\" " +
				"/><GroupStyle parent=\"Group\" me=\"Style282\" /><HeadingStyle parent=\"Heading\" me=\"" +
				"Style273\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style277\" /><InactiveSt" +
				"yle parent=\"Inactive\" me=\"Style276\" /><OddRowStyle parent=\"OddRow\" me=\"Style279\"" +
				" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style281\" /><SelectedStyle p" +
				"arent=\"Selected\" me=\"Style275\" /><Style parent=\"Normal\" me=\"Style271\" /><interna" +
				"lCols><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style1\" /><Style pare" +
				"nt=\"Style271\" me=\"Style2\" /><FooterStyle parent=\"Style274\" me=\"Style3\" /><Editor" +
				"Style parent=\"Style272\" me=\"Style4\" /><GroupHeaderStyle parent=\"Style271\" me=\"St" +
				"yle6\" /><GroupFooterStyle parent=\"Style271\" me=\"Style5\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,None</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style7\" /" +
				"><Style parent=\"Style271\" me=\"Style8\" /><FooterStyle parent=\"Style274\" me=\"Style" +
				"9\" /><EditorStyle parent=\"Style272\" me=\"Style10\" /><GroupHeaderStyle parent=\"Sty" +
				"le271\" me=\"Style12\" /><GroupFooterStyle parent=\"Style271\" me=\"Style11\" /><Visibl" +
				"e>True</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Width>228</Width><H" +
				"eight>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style273\" me=\"Style13\" /><Style parent=\"Style271\" me=\"Style14\" /><Foot" +
				"erStyle parent=\"Style274\" me=\"Style15\" /><EditorStyle parent=\"Style272\" me=\"Styl" +
				"e16\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style18\" /><GroupFooterStyle pare" +
				"nt=\"Style271\" me=\"Style17\" /><Visible>True</Visible><ColumnDivider>DarkGray,None" +
				"</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn></internalC" +
				"ols><ClientRect>0, 0, 812, 361</ClientRect><BorderSide>Right</BorderSide></C1.Wi" +
				"n.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><S" +
				"tyle parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style" +
				" parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style " +
				"parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style pare" +
				"nt=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style par" +
				"ent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style " +
				"parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSty" +
				"les><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout" +
				"><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 812, 361</ClientAr" +
				"ea><PrintPageHeaderStyle parent=\"\" me=\"Style76\" /><PrintPageFooterStyle parent=\"" +
				"\" me=\"Style77\" /></Blob>";
			// 
			// InventoryPublisherReportForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(816, 365);
			this.Controls.Add(this.PublisherReportsGrid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InventoryPublisherReportForm";
			this.Text = "Inventory - Publisher - Reports";
			this.Load += new System.EventHandler(this.InventoryPublisherReportForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.PublisherReportsGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void InventoryPublisherReportForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				dataSet = mainForm.ServiceAgent.PublisherReportsGet("");

				PublisherReportsGrid.SetDataBinding(dataSet, "Reports", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void PublisherReportsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.ServiceAgent.PublisherReportSet(
						PublisherReportsGrid.Columns["ReportName"].Text,
						PublisherReportsGrid.Columns["ReportStoredProc"].Text,
						PublisherReportsGrid.Columns["ReportDescription"].Text);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}
	}
}
