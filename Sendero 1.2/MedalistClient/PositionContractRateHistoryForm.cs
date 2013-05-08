using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class PositionContractRateHistoryForm : System.Windows.Forms.Form
	{
		private DataSet dataSet;
		private DataView dataView;

		private string bookGroup;
		private string contractType;
		private string secId;
		
		private MainForm mainForm;
		private C1.Win.C1List.C1List RateHistoryList;

		private System.ComponentModel.Container components = null;

		public PositionContractRateHistoryForm(MainForm mainForm, string bookGroup, string contractType, string secId)
		{
			this.mainForm = mainForm;
			this.bookGroup = bookGroup;
			this.contractType = contractType;
			this.secId = secId;

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionContractRateHistoryForm));
			this.RateHistoryList = new C1.Win.C1List.C1List();
			((System.ComponentModel.ISupportInitialize)(this.RateHistoryList)).BeginInit();
			this.SuspendLayout();
			// 
			// RateHistoryList
			// 
			this.RateHistoryList.AddItemSeparator = ';';
			this.RateHistoryList.AllowColMove = false;
			this.RateHistoryList.AllowColSelect = false;
			this.RateHistoryList.AllowSort = false;
			this.RateHistoryList.AlternatingRows = true;
			this.RateHistoryList.Caption = "";
			this.RateHistoryList.CaptionHeight = 17;
			this.RateHistoryList.ColumnCaptionHeight = 17;
			this.RateHistoryList.ColumnFooterHeight = 17;
			this.RateHistoryList.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
			this.RateHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RateHistoryList.EmptyRows = true;
			this.RateHistoryList.ExtendRightColumn = true;
			this.RateHistoryList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.RateHistoryList.ItemHeight = 15;
			this.RateHistoryList.Location = new System.Drawing.Point(1, 1);
			this.RateHistoryList.MatchEntryTimeout = ((long)(2000));
			this.RateHistoryList.Name = "RateHistoryList";
			this.RateHistoryList.PartialRightColumn = false;
			this.RateHistoryList.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.RateHistoryList.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.RateHistoryList.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.RateHistoryList.Size = new System.Drawing.Size(400, 248);
			this.RateHistoryList.TabIndex = 0;
			this.RateHistoryList.Text = "RateHistory";
			this.RateHistoryList.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.RateHistoryList_FormatText);
			this.RateHistoryList.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BizDate\" Da" +
				"taField=\"BizDate\" NumberFormat=\"FormatText Event\"><ValueItems /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"Pool Code\" DataField=\"PoolCode\" NumberFormat=\"Fo" +
				"rmatText Event\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Av" +
				"erage Rate\" DataField=\"AverageRate\" NumberFormat=\"FormatText Event\"><ValueItems " +
				"/></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><" +
				"Data>Style11{}Style12{AlignHorz:Near;}Style13{AlignHorz:Near;}Style5{}Style4{}St" +
				"yle7{}Style6{}EvenRow{BackColor:LightCyan;}Selected{ForeColor:HighlightText;Back" +
				"Color:Highlight;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;Fo" +
				"reColor:ControlText;BackColor:Control;}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Style39{AlignHorz:Near;}Footer{}Caption{AlignHorz:Cente" +
				"r;}Style41{}Style40{Font:Verdana, 8.25pt, style=Bold;AlignHorz:Far;}Normal{Font:" +
				"Verdana, 8.25pt;}Style10{}HighlightRow{ForeColor:HighlightText;BackColor:Highlig" +
				"ht;}Style17{}OddRow{}RecordSelector{AlignImage:Center;}Style9{AlignHorz:Near;}St" +
				"yle8{}Style3{}Style2{}Style14{}Style15{AlignHorz:Near;}Style16{AlignHorz:Far;}Gr" +
				"oup{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style1{}</Da" +
				"ta></Styles><Splits><C1.Win.C1List.ListBoxView AllowColMove=\"False\" AllowColSele" +
				"ct=\"False\" Name=\"\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" ColumnCaptionHe" +
				"ight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=\"" +
				"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 396, 244</ClientRect><internalCol" +
				"s><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"S" +
				"tyle1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider" +
				"><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>85</Width><H" +
				"eight>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style2\" me=\"Style39\" /><Style parent=\"Style1\" me=\"Style40\" /><FooterSt" +
				"yle parent=\"Style3\" me=\"Style41\" /><ColumnDivider><Color>DarkGray</Color><Style>" +
				"Single</Style></ColumnDivider><Width>90</Width><Height>15</Height><FetchStyle>Tr" +
				"ue</FetchStyle><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><Style>Si" +
				"ngle</Style></ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>2</DCIdx>" +
				"</C1DisplayColumn></internalCols><VScrollBar><Width>16</Width></VScrollBar><HScr" +
				"ollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\"" +
				" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=" +
				"\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Headi" +
				"ng\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><Inacti" +
				"veStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\"" +
				" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle pa" +
				"rent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1Li" +
				"st.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style paren" +
				"t=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"H" +
				"eading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"No" +
				"rmal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"" +
				"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Headi" +
				"ng\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><ve" +
				"rtSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Defau" +
				"ltRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// PositionContractRateHistoryForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(402, 250);
			this.Controls.Add(this.RateHistoryList);
			this.DockPadding.All = 1;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PositionContractRateHistoryForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Rate History ";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.PositionContractRateHistoryForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.RateHistoryList)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ContractRateHistoryGet()
		{
			try
			{
				mainForm.Alert("Please wait... Loading current rate history...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				dataSet = mainForm.PositionAgent.ContractRateHistoryGet(bookGroup, contractType, secId);
  
				dataView = new DataView(dataSet.Tables["RateHistory"]);
				dataView.Sort = "BizDate DESC";

				RateHistoryList.HoldFields();
				RateHistoryList.DataSource = dataView;
 
				mainForm.Alert("Loading current rate history... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [PositionContractRateHistoryForm.ContractRateHistoryGet]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}

		private void PositionContractRateHistoryForm_Load(object sender, System.EventArgs e)
		{						
			this.Text = "Rate History for " + mainForm.SecId + " [" + mainForm.Symbol + "]";  
			
			if (contractType.Equals("B"))
			{
				this.Text += " Borrows";
			}
			else
			{
				this.Text += " Loans";
			}
			
			this.Show();

			if (!secId.Equals(""))
			{
				ContractRateHistoryGet();
			}
		}

		private void RateHistoryList_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}
  
			try
			{
				switch(RateHistoryList.Columns[e.ColIndex].DataField)
				{
					case ("BizDate"):
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
						break;

					case ("AverageRate"):
						e.Value = decimal.Parse(e.Value).ToString("00.000");
						break;
				}
			}
			catch {}
		}    
	}
}
