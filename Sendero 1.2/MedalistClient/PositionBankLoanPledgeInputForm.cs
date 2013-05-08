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
  public class PositionBankLoanPledgeInputForm : System.Windows.Forms.Form
  {    
    private MainForm mainForm;
    
		DataView bookGroupDataView;
		string	 bookGroupRowFilter;

		DataView bookDataView;
		string	 bookRowFilter;

		DataView loanDateDataView;
		string	 loanDateRowFilter;
		
		private DataSet dataSet;
    
    private C1.Win.C1Input.C1Label StatusLabel;
    private C1.Win.C1Input.C1Label StatusMessageLabel;
    
    private System.Windows.Forms.Button SubmitButton;
    private System.Windows.Forms.Button CloseButton;
		private C1.Win.C1Input.C1TextBox ListTextBox;
		private C1.Win.C1List.C1Combo BookGroupCombo;
		private C1.Win.C1Input.C1Label BookGroupLabel;
		private C1.Win.C1Input.C1Label BookLabel;
		private C1.Win.C1List.C1Combo BookCombo;
		private C1.Win.C1Input.C1Label LoanDateLabel;
		private C1.Win.C1List.C1Combo LoanDateCombo;    

    private System.ComponentModel.Container components = null;

    private delegate void SendPledgesDelegate(string bookGroup, string book, string loanDate, string loanType, string activityType, string pledgeList);

		public PositionBankLoanPledgeInputForm(MainForm mainForm)
		{    
			InitializeComponent();
			this.mainForm = mainForm;             
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

      base.Dispose(disposing);
    }

		private void SendPledges(string bookGroup, string book, string loanDate, string loanType, string activityType, string pledgeList)
		{
			int n = 0;
			int errorCount = 0;						

			List list = new List();

			if (list.Parse(pledgeList).Equals("OK"))			
			{											
				for (int index = 0; index < list.Count; index++)
				{										
					try
					{																									
						StatusMessageLabel.Text  = "Processing " + list.ItemGet(index).SecId + "...";						
						
						n++;
						
						mainForm.PositionAgent.BankLoanPledgeSet(
							bookGroup,
							book,
							loanDate,
							"",
							loanType,
							activityType,
							list.ItemGet(index).SecId,
							list.ItemGet(index).Quantity.ToString(),
							"",
							"PR",
							mainForm.UserId);														
					}
					catch (Exception e)
					{          
						Log.Write(e.Message + " [PositionBankLoanPledgeInputForm.SendContracts]", Log.Error, 1);
						mainForm.Alert(e.Message, PilotState.RunFault);

						errorCount ++;
					}
				}
				
				if (n.Equals(list.Count))
				{
					Log.Write("Processed " + (list.Count) + " item[s] for " + BookGroupCombo.Text + " LoanDate: " + loanDate + ". [PositionBankLoanPledgeInputForm.SendPledges]", Log.Information, 2);
					StatusMessageLabel.Text  = "Processed " + (list.Count) + " item[s] for " + BookGroupCombo.Text + " LoanDate: " + loanDate + ".";
				}
				else
				{
					Log.Write("Processed " + n.ToString() + " item[s] with " + errorCount + " error[s].", Log.Information, 2);
					StatusMessageLabel.Text = "Processed " + n.ToString() + " item[s] with " + errorCount + " error[s].";
				}
			}    					
		}

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionBankLoanPledgeInputForm));
			this.SubmitButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
			this.ListTextBox = new C1.Win.C1Input.C1TextBox();
			this.BookGroupCombo = new C1.Win.C1List.C1Combo();
			this.BookGroupLabel = new C1.Win.C1Input.C1Label();
			this.BookLabel = new C1.Win.C1Input.C1Label();
			this.BookCombo = new C1.Win.C1List.C1Combo();
			this.LoanDateLabel = new C1.Win.C1Input.C1Label();
			this.LoanDateCombo = new C1.Win.C1List.C1Combo();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LoanDateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LoanDateCombo)).BeginInit();
			this.SuspendLayout();
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(280, 180);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(84, 24);
			this.SubmitButton.TabIndex = 5;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(280, 232);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(84, 24);
			this.CloseButton.TabIndex = 6;
			this.CloseButton.Text = "Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Location = new System.Drawing.Point(8, 292);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(48, 16);
			this.StatusLabel.TabIndex = 0;
			this.StatusLabel.Tag = null;
			this.StatusLabel.Text = "Status:";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.StatusLabel.TextDetached = true;
			// 
			// StatusMessageLabel
			// 
			this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
			this.StatusMessageLabel.Location = new System.Drawing.Point(64, 292);
			this.StatusMessageLabel.Name = "StatusMessageLabel";
			this.StatusMessageLabel.Size = new System.Drawing.Size(324, 28);
			this.StatusMessageLabel.TabIndex = 0;
			this.StatusMessageLabel.Tag = null;
			this.StatusMessageLabel.TextDetached = true;
			// 
			// ListTextBox
			// 
			this.ListTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.ListTextBox.DateTimeInput = false;
			this.ListTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ListTextBox.ForeColor = System.Drawing.Color.Navy;
			this.ListTextBox.Location = new System.Drawing.Point(0, 0);
			this.ListTextBox.MaxLength = 250000;
			this.ListTextBox.Multiline = true;
			this.ListTextBox.Name = "ListTextBox";
			this.ListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ListTextBox.Size = new System.Drawing.Size(244, 287);
			this.ListTextBox.TabIndex = 4;
			this.ListTextBox.Tag = null;
			this.ListTextBox.TextDetached = true;
			// 
			// BookGroupCombo
			// 
			this.BookGroupCombo.AddItemSeparator = ';';
			this.BookGroupCombo.AutoCompletion = true;
			this.BookGroupCombo.AutoDropDown = true;
			this.BookGroupCombo.AutoSize = false;
			this.BookGroupCombo.Caption = "";
			this.BookGroupCombo.CaptionHeight = 17;
			this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.BookGroupCombo.ColumnCaptionHeight = 17;
			this.BookGroupCombo.ColumnFooterHeight = 17;
			this.BookGroupCombo.ContentHeight = 14;
			this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.BookGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.BookGroupCombo.DropDownWidth = 375;
			this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.BookGroupCombo.EditorHeight = 14;
			this.BookGroupCombo.ExtendRightColumn = true;
			this.BookGroupCombo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookGroupCombo.GapHeight = 2;
			this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.BookGroupCombo.ItemHeight = 15;
			this.BookGroupCombo.LimitToList = true;
			this.BookGroupCombo.Location = new System.Drawing.Point(260, 32);
			this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
			this.BookGroupCombo.MaxDropDownItems = ((short)(5));
			this.BookGroupCombo.MaxLength = 32767;
			this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.BookGroupCombo.Name = "BookGroupCombo";
			this.BookGroupCombo.PartialRightColumn = false;
			this.BookGroupCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BookGroupCombo.Size = new System.Drawing.Size(132, 20);
			this.BookGroupCombo.TabIndex = 1;
			this.BookGroupCombo.RowChange += new System.EventHandler(this.BookGroupCombo_RowChange);
			this.BookGroupCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book Group\"" +
				" DataField=\"BookGroup\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
				"ion=\"Book Name\" DataField=\"BookName\"><ValueItems /></C1DataColumn></DataCols><St" +
				"yles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Bor" +
				"der:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Styl" +
				"e5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:Highlight" +
				"Text;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackCol" +
				"or:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8pt;B" +
				"ackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style" +
				"1{}OddRow{}RecordSelector{AlignImage:Center;}Style13{AlignHorz:Near;}Heading{Wra" +
				"p:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVe" +
				"rt:Center;}Style8{}Style10{}Style11{}Style14{}Style15{AlignHorz:Near;}Style16{Al" +
				"ignHorz:Near;}Style17{}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1" +
				"List.ListBoxView AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaption" +
				"Height=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup" +
				"=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalC" +
				"ols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=" +
				"\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivid" +
				"er><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>110</Width" +
				"><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><Sty" +
				"le>Single</Style></ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayC" +
				"olumn></internalCols><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Heig" +
				"ht>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowS" +
				"tyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><" +
				"GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Styl" +
				"e2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle pare" +
				"nt=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSe" +
				"lectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Select" +
				"ed\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxVi" +
				"ew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" m" +
				"e=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"" +
				"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Se" +
				"lected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"" +
				"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Reco" +
				"rdSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</" +
				"vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidt" +
				"h>16</DefaultRecSelWidth></Blob>";
			// 
			// BookGroupLabel
			// 
			this.BookGroupLabel.Location = new System.Drawing.Point(260, 16);
			this.BookGroupLabel.Name = "BookGroupLabel";
			this.BookGroupLabel.Size = new System.Drawing.Size(92, 16);
			this.BookGroupLabel.TabIndex = 0;
			this.BookGroupLabel.Tag = null;
			this.BookGroupLabel.Text = "Book Group:";
			this.BookGroupLabel.TextDetached = true;
			// 
			// BookLabel
			// 
			this.BookLabel.Location = new System.Drawing.Point(260, 64);
			this.BookLabel.Name = "BookLabel";
			this.BookLabel.Size = new System.Drawing.Size(92, 16);
			this.BookLabel.TabIndex = 0;
			this.BookLabel.Tag = null;
			this.BookLabel.Text = "Book:";
			this.BookLabel.TextDetached = true;
			this.BookLabel.Value = "";
			// 
			// BookCombo
			// 
			this.BookCombo.AddItemSeparator = ';';
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
			this.BookCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.BookCombo.EditorHeight = 14;
			this.BookCombo.ExtendRightColumn = true;
			this.BookCombo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookCombo.GapHeight = 2;
			this.BookCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.BookCombo.ItemHeight = 15;
			this.BookCombo.Location = new System.Drawing.Point(260, 80);
			this.BookCombo.MatchEntryTimeout = ((long)(2000));
			this.BookCombo.MaxDropDownItems = ((short)(16));
			this.BookCombo.MaxLength = 32767;
			this.BookCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.BookCombo.Name = "BookCombo";
			this.BookCombo.PartialRightColumn = false;
			this.BookCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BookCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.BookCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BookCombo.Size = new System.Drawing.Size(132, 20);
			this.BookCombo.TabIndex = 2;
			this.BookCombo.RowChange += new System.EventHandler(this.BookCombo_RowChange);
			this.BookCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Book N" +
				"ame\" DataField=\"Name\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.W" +
				"in.C1List.Design.ContextWrapper\"><Data>Caption{AlignHorz:Center;}Normal{Font:Ver" +
				"dana, 8pt;BackColor:Window;}Selected{ForeColor:HighlightText;BackColor:Highlight" +
				";}Style14{}Style15{AlignHorz:Near;}Style16{AlignHorz:Near;}Style17{}Style10{}Sty" +
				"le11{}OddRow{}Style13{AlignHorz:Near;}Style12{AlignHorz:Near;}Footer{}HighlightR" +
				"ow{ForeColor:HighlightText;BackColor:Highlight;}RecordSelector{AlignImage:Center" +
				";}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{Back" +
				"Color:Aqua;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" +
				"lor:ControlText;AlignVert:Center;}Style4{}Style9{AlignHorz:Near;}Style8{}Style5{" +
				"}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}S" +
				"tyle6{}Style1{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1List.ListBoxVie" +
				"w AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Col" +
				"umnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=\"1\" Horizontal" +
				"ScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayC" +
				"olumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"St" +
				"yle13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkG" +
				"ray</Color><Style>Single</Style></ColumnDivider><Width>110</Width><Height>15</He" +
				"ight><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Styl" +
				"e></ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn></interna" +
				"lCols><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height><" +
				"/HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Ev" +
				"enRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle pare" +
				"nt=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLigh" +
				"tRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" m" +
				"e=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle par" +
				"ent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\"" +
				" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Na" +
				"medStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><" +
				"Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Sty" +
				"le parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Styl" +
				"e parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Sty" +
				"le parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><" +
				"Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><hor" +
				"zSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRe" +
				"cSelWidth></Blob>";
			// 
			// LoanDateLabel
			// 
			this.LoanDateLabel.Location = new System.Drawing.Point(260, 112);
			this.LoanDateLabel.Name = "LoanDateLabel";
			this.LoanDateLabel.Size = new System.Drawing.Size(92, 16);
			this.LoanDateLabel.TabIndex = 0;
			this.LoanDateLabel.Tag = null;
			this.LoanDateLabel.Text = "Loan Date:";
			this.LoanDateLabel.TextDetached = true;
			// 
			// LoanDateCombo
			// 
			this.LoanDateCombo.AddItemSeparator = ';';
			this.LoanDateCombo.AutoCompletion = true;
			this.LoanDateCombo.AutoDropDown = true;
			this.LoanDateCombo.AutoSize = false;
			this.LoanDateCombo.Caption = "";
			this.LoanDateCombo.CaptionHeight = 17;
			this.LoanDateCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.LoanDateCombo.ColumnCaptionHeight = 17;
			this.LoanDateCombo.ColumnFooterHeight = 17;
			this.LoanDateCombo.ColumnWidth = 110;
			this.LoanDateCombo.ContentHeight = 14;
			this.LoanDateCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.LoanDateCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.LoanDateCombo.DropDownWidth = 150;
			this.LoanDateCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.LoanDateCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoanDateCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.LoanDateCombo.EditorHeight = 14;
			this.LoanDateCombo.ExtendRightColumn = true;
			this.LoanDateCombo.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoanDateCombo.GapHeight = 2;
			this.LoanDateCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.LoanDateCombo.ItemHeight = 15;
			this.LoanDateCombo.Location = new System.Drawing.Point(260, 128);
			this.LoanDateCombo.MatchEntryTimeout = ((long)(2000));
			this.LoanDateCombo.MaxDropDownItems = ((short)(14));
			this.LoanDateCombo.MaxLength = 32767;
			this.LoanDateCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.LoanDateCombo.Name = "LoanDateCombo";
			this.LoanDateCombo.PartialRightColumn = false;
			this.LoanDateCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.LoanDateCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.LoanDateCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.LoanDateCombo.Size = new System.Drawing.Size(132, 20);
			this.LoanDateCombo.TabIndex = 3;
			this.LoanDateCombo.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.LoanDateCombo_FormatText);
			this.LoanDateCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"LoanDate\" D" +
				"ataField=\"LoanDate\" NumberFormat=\"FormatText Event\"><ValueItems /></C1DataColumn" +
				"></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{Align" +
				"Vert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Nea" +
				"r;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{Fore" +
				"Color:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCapt" +
				"ionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font" +
				":Verdana, 8pt;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:H" +
				"ighlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;Ba" +
				"ckColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center" +
				";}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Ne" +
				"ar;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Na" +
				"me=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Extend" +
				"RightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect" +
				">0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</S" +
				"tyle></ColumnDivider><Width>110</Width><Height>15</Height><DCIdx>0</DCIdx></C1Di" +
				"splayColumn></internalCols><VScrollBar><Width>16</Width></VScrollBar><HScrollBar" +
				"><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><Ev" +
				"enRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style" +
				"3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me" +
				"=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyl" +
				"e parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><Re" +
				"cordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"" +
				"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.Lis" +
				"tBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Nor" +
				"mal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading" +
				"\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" " +
				"me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal" +
				"\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me" +
				"=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSpli" +
				"ts>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecS" +
				"elWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// PositionBankLoanPledgeInputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(394, 327);
			this.Controls.Add(this.BookGroupCombo);
			this.Controls.Add(this.BookGroupLabel);
			this.Controls.Add(this.BookLabel);
			this.Controls.Add(this.BookCombo);
			this.Controls.Add(this.LoanDateLabel);
			this.Controls.Add(this.LoanDateCombo);
			this.Controls.Add(this.ListTextBox);
			this.Controls.Add(this.StatusMessageLabel);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.SubmitButton);
			this.DockPadding.Bottom = 40;
			this.DockPadding.Right = 150;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PositionBankLoanPledgeInputForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Position - BankLoan - Pledge Input";
			this.Load += new System.EventHandler(this.PositionBankLoanPledgeInputForm_Load);
			this.Closed += new System.EventHandler(this.PositionBankLoanPledgeInputForm_Closed);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionBankLoanPledgeInputForm_Paint);
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LoanDateLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LoanDateCombo)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion
 
    private void PositionBankLoanPledgeInputForm_Load(object sender, System.EventArgs e)
    {      
      this.WindowState = FormWindowState.Normal;
      
			try
			{                               
				dataSet = mainForm.PositionAgent.BankLoanDataGet(mainForm.UtcOffset, mainForm.UserId, "PositionBankLoan");
				
				bookGroupRowFilter = 	"MayEdit = 1 AND MayView = 1";
				bookGroupDataView = new DataView(dataSet.Tables["BookGroups"], bookGroupRowFilter, "", DataViewRowState.CurrentRows);
				
				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = bookGroupDataView;

				bookRowFilter = "BookGroup = ''";
				bookDataView = new DataView(dataSet.Tables["Banks"], bookRowFilter, "", DataViewRowState.CurrentRows);
				
				BookCombo.HoldFields();
				BookCombo.DataSource = bookDataView;

				loanDateRowFilter = "BookGroup = ''";
				loanDateDataView = new DataView(dataSet.Tables["Loans"], loanDateRowFilter, "", DataViewRowState.CurrentRows);
				
				LoanDateCombo.HoldFields();
				LoanDateCombo.DataSource = loanDateDataView;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionBankLoanPledgeInputForm.PositionReturnInputForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
    }

    private void PositionBankLoanPledgeInputForm_Closed(object sender, System.EventArgs e)
    {
      mainForm.Refresh();    
    }
   
    private void ReturnQuantityTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13) && SubmitButton.Enabled)
      {
        SubmitButton_Click(this, new System.EventArgs());
        e.Handled = true;
      }
    }

    private void SubmitButton_Click(object sender, System.EventArgs e)
    {          
			if (ListTextBox.Text.Equals(""))
			{
				StatusMessageLabel.Text = "List area is clear. No data to process.";
				ListTextBox.Focus();
				return;
			}
			
			if (BookGroupCombo.Text.Equals(""))
			{
				StatusMessageLabel.Text = "Please select a Book Group.";
				BookGroupCombo.Focus();
				return;
			}
			
			if (BookCombo.Text.Equals(""))
			{
				StatusMessageLabel.Text = "Please select a Book.";
				BookCombo.Focus();
				return;
			}

			if (LoanDateCombo.Text.Equals(""))
			{
				StatusMessageLabel.Text = "Please select a LoanDate.";
				LoanDateCombo.Focus();
				return;
			}

      try
      {                      
        SendPledgesDelegate sendPledgesDelegate = new SendPledgesDelegate(SendPledges);
				sendPledgesDelegate.BeginInvoke(
					BookGroupCombo.Text, 
					BookCombo.Text, 
					LoanDateCombo.Text,
					loanDateDataView[LoanDateCombo.WillChangeToIndex].Row["LoanType"].ToString(), 
					loanDateDataView[LoanDateCombo.WillChangeToIndex].Row["ActivityType"].ToString(), 
					ListTextBox.Text, 
					null,  
					null);             
				
        SubmitButton.Enabled = false;
    
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionBankLoanPledgeInputForm.SubmitButton_Click]", Log.Error, 1);
        StatusMessageLabel.Text = error.Message;        
      }          
    }

    private void CloseButton_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

		private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
		{
			bookRowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
			bookDataView.RowFilter = bookRowFilter;
		}

		private void BookCombo_RowChange(object sender, System.EventArgs e)
		{
			loanDateRowFilter = bookRowFilter + " AND Book = '" + BookCombo.Text + "'";
			loanDateDataView.RowFilter = loanDateRowFilter;
		}

		private void LoanDateCombo_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			switch (LoanDateCombo.Columns[e.ColIndex].DataField)
			{
				case "LoanDate":
					try
					{
						e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);
					}
					catch {}
					break;

				default:
					break;
			}
		}

		private void PositionBankLoanPledgeInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Pen pen = new Pen(Color.DimGray, 1.8F);
      
			float x1 = ListTextBox.Width + 10;
			float x2 = this.Width - 10;
			
			float y1 = SubmitButton.Location.Y - 10;			

			float y2 = CloseButton.Location.Y  + CloseButton.Height + 10;			

			e.Graphics.DrawLine(pen, x1, y1, x2, y1);   
			e.Graphics.DrawLine(pen, x1, y2, x2, y2);   
			e.Graphics.Dispose();
		}
  }
}
