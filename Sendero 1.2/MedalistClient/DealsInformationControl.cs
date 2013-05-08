//
// Modified by Yasir Bashir on 8/3/2007
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class DealsInformationControl : System.Windows.Forms.UserControl
	{
		#region Declarations
		private MainForm mainForm;
		private DataSet dataSet;
		private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid DealsInformationGrid;
		private const decimal E = 0.000000000000000001M;
		#endregion

		public DealsInformationControl()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DealsInformationControl));
			this.DealsInformationGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.DealsInformationGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// DealsInformationGrid
			// 
			this.DealsInformationGrid.AllowUpdate = false;
			this.DealsInformationGrid.CaptionHeight = 17;
			this.DealsInformationGrid.ColumnFooters = true;
			this.DealsInformationGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DealsInformationGrid.EmptyRows = true;
			this.DealsInformationGrid.ExtendRightColumn = true;
			this.DealsInformationGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.DealsInformationGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.DealsInformationGrid.Location = new System.Drawing.Point(0, 0);
			this.DealsInformationGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.DealsInformationGrid.Name = "DealsInformationGrid";
			this.DealsInformationGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.DealsInformationGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.DealsInformationGrid.PreviewInfo.ZoomFactor = 75;
			this.DealsInformationGrid.RecordSelectorWidth = 16;
			this.DealsInformationGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.DealsInformationGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.DealsInformationGrid.RowHeight = 15;
			this.DealsInformationGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.DealsInformationGrid.Size = new System.Drawing.Size(476, 160);
			this.DealsInformationGrid.TabIndex = 1;
			this.DealsInformationGrid.OnAddNew += new System.EventHandler(this.DealsInformationGrid_OnAddNew);
			this.DealsInformationGrid.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrappe" +
				"r\"><Data>Style11{}Style12{}Style13{}Style5{}Style4{}Style7{}Style6{}EvenRow{Back" +
				"Color:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inacti" +
				"ve{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}C" +
				"aption{AlignHorz:Center;}Editor{}Normal{Font:Verdana, 8.25pt;}HighlightRow{ForeC" +
				"olor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImag" +
				"e:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor" +
				":ControlText;BackColor:Control;}Style8{}Style10{AlignHorz:Near;}Style2{}Style14{" +
				"}Style15{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}" +
				"Style9{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" " +
				"VBarStyle=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoo" +
				"terHeight=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSe" +
				"lectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGro" +
				"up=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\"" +
				" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle pare" +
				"nt=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupS" +
				"tyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" />" +
				"<HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"In" +
				"active\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelector" +
				"Style parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me" +
				"=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><ClientRect>0, 0, 472, 156</Cli" +
				"entRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><Name" +
				"dStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><St" +
				"yle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style" +
				" parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style " +
				"parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style " +
				"parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style paren" +
				"t=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style" +
				" parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSpli" +
				"ts>1</horzSplits><Layout>None</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth" +
				"><ClientArea>0, 0, 472, 156</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Styl" +
				"e14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// DealsInformationControl
			// 
			this.Controls.Add(this.DealsInformationGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "DealsInformationControl";
			this.Size = new System.Drawing.Size(476, 160);
			((System.ComponentModel.ISupportInitialize)(this.DealsInformationGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public MainForm MainForm
		{
			set
			{
				mainForm = value;
			}
		}

		private void DealsInformationGrid_OnAddNew(object sender, System.EventArgs e)
		{
			
		}

		public void LoadData(string BookGroup, string SecId)
		{
			try
			{
				decimal amount = 0M;
				decimal rate = 0M;
				decimal sumfrequency = 0M;
				long	quantity = 0;
				int		count = 1;
				int		sumcount = 0;
				bool	loopstatus = true;

				if (!SecId.Equals(""))					
				{
					dataSet = mainForm.PositionAgent.DealsDetailDataGet(BookGroup, SecId , mainForm.UtcOffset);
					
					DealsInformationGrid.DataSource = dataSet.Tables["Deals"];
					
					
					foreach (DataRow dr in dataSet.Tables["Deals"].Rows)
					{
						if (dr["DealType"].ToString() == "L")
						{
							quantity -= long.Parse(dr["Quantity"].ToString());
							amount-= decimal.Parse(dr["Amount"].ToString());
						}
						else if (dr["DealType"].ToString() == "B")
						{
							quantity += long.Parse(dr["Quantity"].ToString());
							amount+= decimal.Parse(dr["Amount"].ToString());
						}
					}

					foreach (DataRow dr in dataSet.Tables["Deals"].Select("", "Rate Desc"))
					{
						if (dr["Rate"].ToString() != "")
						{
							if (decimal.Parse(dr["Rate"].ToString()) != rate)
							{
								if (loopstatus == true)
								{
									rate = decimal.Parse(dr["Rate"].ToString());
									loopstatus = false;
								}
								
								rate = decimal.Parse(dr["Rate"].ToString());
								count = 1;
								sumcount = sumcount + count;
								sumfrequency = sumfrequency + (rate * count);
							}
							else if (decimal.Parse(dr["Rate"].ToString()) == rate)
							{
								sumcount = sumcount + count;
								sumfrequency = sumfrequency + (rate * count);
								count = count + 1;
							}
						}
					}

					DealsInformationGrid.Columns["SecId"].FooterText = Convert.ToString(SecId);
					DealsInformationGrid.Columns["Symbol"].FooterText = dataSet.Tables["Deals"].Rows[0].ItemArray[2].ToString();
					DealsInformationGrid.Columns["Rate"].FooterText =  (sumfrequency/sumcount).ToString("#0.000");
					DealsInformationGrid.Columns["Quantity"].FooterText = quantity.ToString("#,##0");
					DealsInformationGrid.Columns["Amount"].FooterText = amount.ToString("#,##0.00");

					DealsInformationGrid.Splits[0].DisplayColumns["SecId"].Width = 80;
					DealsInformationGrid.Splits[0].DisplayColumns["DealType"].Width = 60;
					DealsInformationGrid.Splits[0].DisplayColumns["Symbol"].Width = 60;
					DealsInformationGrid.Splits[0].DisplayColumns["Quantity"].Width = 60;
					DealsInformationGrid.Splits[0].DisplayColumns["Rate"].Width = 60;

					DealsInformationGrid.Columns["Rate"].NumberFormat = "#0.000";
					DealsInformationGrid.Columns["Quantity"].NumberFormat = "#,##0";
					DealsInformationGrid.Columns["Amount"].NumberFormat = "#,##0.00";

					DealsInformationGrid.Columns["SecId"].Caption = "Security ID";
					DealsInformationGrid.Columns["DealType"].Caption = "Deal Type";
					DealsInformationGrid.Columns["Symbol"].Caption = "Symbol";
					DealsInformationGrid.Columns["Quantity"].Caption = "Quantity";
					DealsInformationGrid.Columns["Rate"].Caption = "Rate";

					DealsInformationGrid.AllowRowSelect = false;
					DealsInformationGrid.AllowUpdate = false;
					DealsInformationGrid.AllowColMove = false;
					DealsInformationGrid.AllowDrag = false;
					DealsInformationGrid.AllowUpdate = false;

					if (quantity <= 0)
					{
						DealsInformationGrid.FooterStyle.ForeColor = Color.Blue;
						DealsInformationGrid.FooterStyle.BackColor = Color.White;
					}
					else if (quantity > 0)
					{
						DealsInformationGrid.FooterStyle.ForeColor = Color.Red;
						DealsInformationGrid.FooterStyle.BackColor = Color.White;
					}
				}
				else
				{
					DealsInformationGrid.ClearFields();
				}
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [DealsInformationControl.LoadData]", Log.Error, 1);
			}
		}
	}
}
