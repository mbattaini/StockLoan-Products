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
	public class PositionBankLoanReleaseReportsDataForm : System.Windows.Forms.Form
	{
		private const string TEXT = "Position - BankLoan - Release Report";		
		private MainForm mainForm;					
		private System.ComponentModel.Container components = null;		
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ReleaseReportGrid;
		private string bookGroup;
		
		private DataSet dataSet;
		private DataView dataView;

		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem ReleaseItemsMenuItem;
		private System.Windows.Forms.MenuItem SeperatorMenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.MenuItem DataMasksMenuItem;
		private System.Windows.Forms.MenuItem SelectAllMenuItems;
		private string dataViewRowFilter;

		public PositionBankLoanReleaseReportsDataForm(MainForm mainForm, string bookGroup)
		{
			try
			{
				this.mainForm = mainForm;
				InitializeComponent();		

				this.bookGroup = bookGroup;				
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanReleaseReportsDataForm.PositionBankLoanReleaseReportsDataForm]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionBankLoanReleaseReportsDataForm));
			this.ReleaseReportGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.ReleaseItemsMenuItem = new System.Windows.Forms.MenuItem();
			this.SelectAllMenuItems = new System.Windows.Forms.MenuItem();
			this.DataMasksMenuItem = new System.Windows.Forms.MenuItem();
			this.SeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.ReleaseReportGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ReleaseReportGrid
			// 
			this.ReleaseReportGrid.AllowColSelect = false;
			this.ReleaseReportGrid.AllowFilter = false;
			this.ReleaseReportGrid.AllowRowSelect = false;
			this.ReleaseReportGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ReleaseReportGrid.AllowUpdate = false;
			this.ReleaseReportGrid.Caption = "Report Data";
			this.ReleaseReportGrid.CaptionHeight = 17;
			this.ReleaseReportGrid.ContextMenu = this.MainContextMenu;
			this.ReleaseReportGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReleaseReportGrid.EmptyRows = true;
			this.ReleaseReportGrid.ExtendRightColumn = true;
			this.ReleaseReportGrid.FetchRowStyles = true;
			this.ReleaseReportGrid.FilterBar = true;
			this.ReleaseReportGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ReleaseReportGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ReleaseReportGrid.Location = new System.Drawing.Point(0, 0);
			this.ReleaseReportGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ReleaseReportGrid.Name = "ReleaseReportGrid";
			this.ReleaseReportGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ReleaseReportGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ReleaseReportGrid.PreviewInfo.ZoomFactor = 75;
			this.ReleaseReportGrid.RecordSelectorWidth = 16;
			this.ReleaseReportGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ReleaseReportGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ReleaseReportGrid.RowHeight = 15;
			this.ReleaseReportGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ReleaseReportGrid.Size = new System.Drawing.Size(656, 709);
			this.ReleaseReportGrid.TabIndex = 11;
			this.ReleaseReportGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ReleaseReportGrid_FetchRowStyle);
			this.ReleaseReportGrid.FilterChange += new System.EventHandler(this.ReleaseReportGrid_FilterChange);
			this.ReleaseReportGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ReleaseReportGrid_FormatText);
			this.ReleaseReportGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BookGroup\" " +
				"DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Report Name\" DataField=\"ReportName\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol" +
				"\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Activi" +
				"ty\" DataField=\"Activity\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Report Type\" DataField=\"ReportType\"><ValueItems /><GroupInfo" +
				" /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Price\" DataField=\"Price\" Numb" +
				"erFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"Status\" DataField=\"Status\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\">" +
				"<Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeCol" +
				"or:InactiveCaptionText;BackColor:InactiveCaption;}Selected{ForeColor:HighlightTe" +
				"xt;BackColor:Highlight;}Editor{}FilterBar{}Heading{Wrap:True;BackColor:Control;B" +
				"order:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style1" +
				"9{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{Ali" +
				"gnHorz:Near;}Style11{}Style12{}Style13{BackColor:SeaShell;}Style27{}Style29{Alig" +
				"nHorz:Near;BackColor:WhiteSmoke;}Style28{AlignHorz:Near;}Style26{}Style25{}Style" +
				"9{}Style8{}Style24{}Style23{AlignHorz:Near;}Style5{}Style4{}Style7{}Style6{}Styl" +
				"e1{}Style22{AlignHorz:Near;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}S" +
				"tyle39{}Style36{}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;BackCol" +
				"or:WhiteSmoke;}Style32{}Style33{}Style30{}Style49{}Style48{}Style31{}Normal{Font" +
				":Verdana, 8.25pt;}Style41{AlignHorz:Far;}Style40{AlignHorz:Near;}Style43{}Style4" +
				"2{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Near;}EvenRow{Bac" +
				"kColor:Aqua;}Style59{AlignHorz:Far;}Style58{AlignHorz:Near;}RecordSelector{Align" +
				"Image:Center;}Style51{}Style50{}Footer{}Style52{AlignHorz:Near;}Style53{AlignHor" +
				"z:Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}S" +
				"tyle68{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:" +
				"Near;}Style64{AlignHorz:Near;}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;Bac" +
				"kColor:ControlDark;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarS" +
				"tyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" AllowRowSelect=\"False\" Nam" +
				"e=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoo" +
				"terHeight=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" M" +
				"arqueeStyle=\"SolidCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" Verti" +
				"calScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"S" +
				"tyle10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenR" +
				"ow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle" +
				" parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><Headin" +
				"gStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" m" +
				"e=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=" +
				"\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\"" +
				" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Sty" +
				"le1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\"" +
				" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style1" +
				"8\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style paren" +
				"t=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1" +
				"DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Sty" +
				"le parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><E" +
				"ditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"" +
				"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visib" +
				"le><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28" +
				"\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCI" +
				"dx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Hei" +
				"ght><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height" +
				">15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" " +
				"/><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style68\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Heigh" +
				"t>15</Height><DCIdx>8</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 17," +
				" 652, 688</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView>" +
				"</Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"" +
				"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Cap" +
				"tion\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selec" +
				"ted\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highlight" +
				"Row\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" " +
				"/><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Filte" +
				"rBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSp" +
				"lits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</" +
				"DefaultRecSelWidth><ClientArea>0, 0, 652, 705</ClientArea><PrintPageHeaderStyle " +
				"parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.ReleaseItemsMenuItem,
																																										this.SelectAllMenuItems,
																																										this.DataMasksMenuItem,
																																										this.SeperatorMenuItem,
																																										this.ExitMenuItem});
			// 
			// ReleaseItemsMenuItem
			// 
			this.ReleaseItemsMenuItem.Index = 0;
			this.ReleaseItemsMenuItem.Text = "Release item(s)";
			this.ReleaseItemsMenuItem.Click += new System.EventHandler(this.ReleaseItemsMenuItem_Click);
			// 
			// SelectAllMenuItems
			// 
			this.SelectAllMenuItems.Index = 1;
			this.SelectAllMenuItems.Text = "Select All";
			this.SelectAllMenuItems.Click += new System.EventHandler(this.SelectAllMenuItems_Click);
			// 
			// DataMasksMenuItem
			// 
			this.DataMasksMenuItem.Index = 2;
			this.DataMasksMenuItem.Text = "DataMasks";
			this.DataMasksMenuItem.Click += new System.EventHandler(this.DataMasksMenuItem_Click);
			// 
			// SeperatorMenuItem
			// 
			this.SeperatorMenuItem.Index = 3;
			this.SeperatorMenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 4;
			this.ExitMenuItem.Text = "Exit";
			// 
			// PositionBankLoanReleaseReportsDataForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(656, 709);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.ReleaseReportGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionBankLoanReleaseReportsDataForm";
			this.Text = "Position - BankLoan - Release Report Data";
			this.Load += new System.EventHandler(this.PositionBankLoanReleaseReportsDataForm_Load);
			this.Closed += new System.EventHandler(this.PositionBankLoanReleaseReportsDataForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.ReleaseReportGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		

		private void PositionBankLoanReleaseReportsDataForm_Load(object sender, System.EventArgs e)
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

			mainForm.Alert("Please wait ... loading current bank loan release report data.", PilotState.Idle);			

			try
			{
				dataSet = mainForm.PositionAgent.BankLoanReportsDataGet();
			
				dataViewRowFilter = "BookGroup = " + bookGroup;
				dataView = new DataView(dataSet.Tables["BankLoanReportsData"], dataViewRowFilter, "SecId", DataViewRowState.CurrentRows);				
				
				ReleaseReportGrid.SetDataBinding(dataView, "", true);
				mainForm.Alert("Please wait ... loading current bank loan release report data ... Done!.", PilotState.Idle);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionBankLoanReleaseReportsDataForm.PositionBankLoanReleaseReportsDataForm_Load]", Log.Error, 1); 				
				mainForm.Alert("Please wait ... loading current bank loan release report data ... Error!.", PilotState.RunFault);
			}			
			
			this.Cursor = Cursors.Default;
		}

		private void PositionBankLoanReleaseReportsDataForm_Closed(object sender, System.EventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			}
		}

		private void ReleaseReportGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{
				case "Price":
					try
					{
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
					}
					catch {}
					break;

				case "Quantity":
					try
					{
						e.Value = long.Parse(e.Value).ToString("#,##0");
					}
					catch {}
					break;
			}
		}

		private void ReleaseItemsMenuItem_Click(object sender, System.EventArgs e)
		{
			long releaseQuantity = 0;
			long currentQuantity = 0;
			DataSet dataSetReleaseSummary  = new DataSet();			

			foreach (int index in ReleaseReportGrid.SelectedRows)
			{				
				releaseQuantity = long.Parse(ReleaseReportGrid[index, "Quantity"].ToString());
				dataSetReleaseSummary = mainForm.PositionAgent.BankLoanReleaseSummaryGet(ReleaseReportGrid[index, "SecId"].ToString());

				foreach (DataRow dataRow in dataSetReleaseSummary.Tables["BankLoanReleaseSummary"].Rows)			
				{
										
					if (releaseQuantity > 0)
					{
						try
						{											
							if (dataRow["BookGroup"].ToString().Equals(ReleaseReportGrid[index, "BookGroup"].ToString()) 
								&& dataRow["SecId"].ToString().Equals(ReleaseReportGrid[index, "SecId"].ToString()))
							{							
								currentQuantity = long.Parse(dataRow["Quantity"].ToString());

								if ((dataRow["LoanType"].ToString().Equals(ReleaseReportGrid[index, "ReportType"].ToString())) ||
									(dataRow["LoanType"].ToString().Equals("S") && ReleaseReportGrid[index, "ReportType"].ToString().Equals("F")))
								{
									if (currentQuantity > releaseQuantity)
									{
										currentQuantity = releaseQuantity;
									}							

									if (currentQuantity > 0)
									{
										mainForm.PositionAgent.BankLoanReleaseSet(
											dataRow["BookGroup"].ToString(),
											dataRow["Book"].ToString(),
											"",
											dataRow["LoanDate"].ToString(),
											dataRow["LoanType"].ToString(),
											dataRow["ActivityType"].ToString(),
											dataRow["SecId"].ToString(),
											currentQuantity.ToString(),
											"",
											"RR",
											mainForm.UserId);
									}

									releaseQuantity -= currentQuantity;								
								
									ReleaseReportGrid[index, "Status"] = "S";
								}																		
							}
						}
						catch (Exception error)
						{          
							ReleaseReportGrid[index, "Status"] = "F";
							Log.Write(error.Message + " [PositionBankLoanReleaseInputForm.ReleaseItemsMenuItem_Click]", Log.Error, 1);
							mainForm.Alert(error.Message, PilotState.RunFault);							
						}						
					}
				}
			}
		}

		private void ReleaseReportGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref ReleaseReportGrid);

				if (gridFilter.Equals(""))
				{
					dataView.RowFilter = dataViewRowFilter;
				}
				else
				{
					dataView.RowFilter = dataViewRowFilter + " AND " + gridFilter;
				}				
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void DataMasksMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				PositionBankLoanReleaseReportForm positionBankLoanReleaseReportForm = new PositionBankLoanReleaseReportForm(mainForm, bookGroup);
				positionBankLoanReleaseReportForm.MdiParent = mainForm;
				positionBankLoanReleaseReportForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SelectAllMenuItems_Click(object sender, System.EventArgs e)
		{
			for( int count = 0; count <= ReleaseReportGrid.Splits[0].Rows.Count; count++)
			{
				ReleaseReportGrid.SelectedRows.Add(count);
			}
		}

		private void ReleaseReportGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			if (ReleaseReportGrid.Columns["Status"].CellText(e.Row).Equals("F"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.LightSalmon;
			}
			else if (ReleaseReportGrid.Columns["Status"].CellText(e.Row).Equals("S"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.LightCyan;
			}			
		}
	}
}
