// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2001-2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
    public class PositionOpenContractsForm : System.Windows.Forms.Form
    {
        private const string TEXT = "Position - Open Contracts";
        private const int MINIMUM_SUMMARY_HEIGHT_BUFFER = 180;
        private const decimal E = 0.000000000000000001M;

        private bool isReady = false;
        private string secId = "";
        private string rowIdentity = "";

        private MainForm mainForm;

        private DataSet dataSet;
        private DataView summaryDataView, borrowsDataView, loansDataView;

        private C1.Win.C1Input.C1Label BookGroupLabel, BookGroupNameLabel, BizDateLabel;
        private C1.Win.C1List.C1Combo BookGroupCombo, BizDateCombo;

        private System.Windows.Forms.CheckBox SummaryCheckBox, ClassCodeCheckBox, PoolCodeCheckBox;

        private System.Windows.Forms.GroupBox PositionGroupBox;
        private System.Windows.Forms.RadioButton SecurityRadioButton, BookRadioButton, BookParentRadioButton;

        private System.Windows.Forms.Panel ContractsPanel;
        private System.Windows.Forms.Splitter ContractsSplitter, SummarySplitter;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid SummaryGrid, BorrowsGrid, LoansGrid;

        private C1.Win.C1Input.C1Label FundingRateLabel;
        private C1.Win.C1Input.C1TextBox FundingRateTextBox;
        private System.Windows.Forms.RadioButton FeeRadioButton, RebateRadioButton;

        private System.Windows.Forms.ContextMenu MainContextMenu;

        private System.Windows.Forms.MenuItem RateChangeMenuItem;
        private System.Windows.Forms.MenuItem ReturnMenuItem;
        private System.Windows.Forms.MenuItem RecallMenuItem;
        private System.Windows.Forms.MenuItem PcChangeMenuItem;
        private System.Windows.Forms.MenuItem ShowMenuItem;
        private System.Windows.Forms.MenuItem DockMenuItem;
        private System.Windows.Forms.MenuItem DockTopMenuItem;
        private System.Windows.Forms.MenuItem DockBottomMenuItem;
        private System.Windows.Forms.MenuItem DockNoneMenuItem;

        private ContractEventWrapper contractEventWrapper = null;
        private ContractEventHandler contractEventHandler = null;

        private ArrayList contractEventArgsArray, bizDateArray;

        private System.Windows.Forms.CheckBox AutoFilterCheckBox;
        private System.Windows.Forms.MenuItem Sep1MenuItem;
        private System.Windows.Forms.MenuItem Sep2MenuItem;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.MenuItem ShowIncomeMenuItem;
        private System.Windows.Forms.MenuItem ShowCurrencyMenuItem;
        private System.Windows.Forms.MenuItem ShowTermDateMenuItem;
        private System.Windows.Forms.MenuItem ShowRecallQuantityMenuItem;
        private System.Windows.Forms.MenuItem ShowDividendReclaimMenuItem;
        private System.Windows.Forms.MenuItem ShowSettlementDetailMenuItem;
        private System.Windows.Forms.MenuItem ShowValueRatioMenuItem;
        private System.Windows.Forms.MenuItem DockSep1MenuItem;
        private System.Windows.Forms.MenuItem SendToMenuItem;
        private System.Windows.Forms.MenuItem SendToExcelMenuItem;

        private System.ComponentModel.Container components = null;

        public PositionOpenContractsForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            bizDateArray = new ArrayList();
            contractEventArgsArray = new ArrayList();

            contractEventWrapper = new ContractEventWrapper();
            contractEventWrapper.ContractEvent += new ContractEventHandler(ContractOnEvent);

            contractEventHandler = new ContractEventHandler(ContractDoEvent);

            ContractsPanel.Tag = ContractsPanel.Height;
        }

        protected override void Dispose(bool disposing)
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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionOpenContractsForm));
            this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
            this.BookGroupLabel = new C1.Win.C1Input.C1Label();
            this.BookGroupCombo = new C1.Win.C1List.C1Combo();
            this.BizDateLabel = new C1.Win.C1Input.C1Label();
            this.BizDateCombo = new C1.Win.C1List.C1Combo();
            this.PositionGroupBox = new System.Windows.Forms.GroupBox();
            this.BookParentRadioButton = new System.Windows.Forms.RadioButton();
            this.BookRadioButton = new System.Windows.Forms.RadioButton();
            this.SecurityRadioButton = new System.Windows.Forms.RadioButton();
            this.SummaryCheckBox = new System.Windows.Forms.CheckBox();
            this.PoolCodeCheckBox = new System.Windows.Forms.CheckBox();
            this.ContractsPanel = new System.Windows.Forms.Panel();
            this.BorrowsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.ContractsSplitter = new System.Windows.Forms.Splitter();
            this.LoansGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.MainContextMenu = new System.Windows.Forms.ContextMenu();
            this.RateChangeMenuItem = new System.Windows.Forms.MenuItem();
            this.ReturnMenuItem = new System.Windows.Forms.MenuItem();
            this.RecallMenuItem = new System.Windows.Forms.MenuItem();
            this.PcChangeMenuItem = new System.Windows.Forms.MenuItem();
            this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.ShowMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowIncomeMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowCurrencyMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowTermDateMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowRecallQuantityMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowDividendReclaimMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowSettlementDetailMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowValueRatioMenuItem = new System.Windows.Forms.MenuItem();
            this.DockMenuItem = new System.Windows.Forms.MenuItem();
            this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
            this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
            this.DockSep1MenuItem = new System.Windows.Forms.MenuItem();
            this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
            this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.SummarySplitter = new System.Windows.Forms.Splitter();
            this.SummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.FundingRateLabel = new C1.Win.C1Input.C1Label();
            this.FeeRadioButton = new System.Windows.Forms.RadioButton();
            this.RebateRadioButton = new System.Windows.Forms.RadioButton();
            this.FundingRateTextBox = new C1.Win.C1Input.C1TextBox();
            this.ClassCodeCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.SendToMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateCombo)).BeginInit();
            this.PositionGroupBox.SuspendLayout();
            this.ContractsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BorrowsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundingRateLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundingRateTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BookGroupNameLabel
            // 
            this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
            this.BookGroupNameLabel.Location = new System.Drawing.Point(200, 6);
            this.BookGroupNameLabel.Name = "BookGroupNameLabel";
            this.BookGroupNameLabel.Size = new System.Drawing.Size(348, 18);
            this.BookGroupNameLabel.TabIndex = 3;
            this.BookGroupNameLabel.Tag = null;
            this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BookGroupLabel
            // 
            this.BookGroupLabel.Location = new System.Drawing.Point(4, 4);
            this.BookGroupLabel.Name = "BookGroupLabel";
            this.BookGroupLabel.Size = new System.Drawing.Size(92, 18);
            this.BookGroupLabel.TabIndex = 0;
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
            this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.BookGroupCombo.ItemHeight = 15;
            this.BookGroupCombo.KeepForeColor = true;
            this.BookGroupCombo.LimitToList = true;
            this.BookGroupCombo.Location = new System.Drawing.Point(100, 4);
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
            this.BookGroupCombo.TabIndex = 1;
            this.BookGroupCombo.TextChanged += new System.EventHandler(this.BizDateBookGroup_TextChanged);
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
            this.BizDateLabel.Location = new System.Drawing.Point(4, 30);
            this.BizDateLabel.Name = "BizDateLabel";
            this.BizDateLabel.Size = new System.Drawing.Size(92, 18);
            this.BizDateLabel.TabIndex = 5;
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
            this.BizDateCombo.ColumnWidth = 100;
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
            this.BizDateCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.BizDateCombo.ItemHeight = 15;
            this.BizDateCombo.KeepForeColor = true;
            this.BizDateCombo.LimitToList = true;
            this.BizDateCombo.Location = new System.Drawing.Point(100, 30);
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
            this.BizDateCombo.TabIndex = 6;
            this.BizDateCombo.TextChanged += new System.EventHandler(this.BizDateBookGroup_TextChanged);
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
                "/Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></in" +
                "ternalCols><VScrollBar><Width>16</Width><Style>Always</Style></VScrollBar><HScro" +
                "llBar><Height>16</Height><Style>None</Style></HScrollBar><CaptionStyle parent=\"S" +
                "tyle2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle p" +
                "arent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingS" +
                "tyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=" +
                "\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"O" +
                "ddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /" +
                "><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style" +
                "1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Norm" +
                "al\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" " +
                "/><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /" +
                "><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\"" +
                " /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><S" +
                "tyle parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /" +
                "></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modif" +
                "ied</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
            // 
            // PositionGroupBox
            // 
            this.PositionGroupBox.Controls.Add(this.BookParentRadioButton);
            this.PositionGroupBox.Controls.Add(this.BookRadioButton);
            this.PositionGroupBox.Controls.Add(this.SecurityRadioButton);
            this.PositionGroupBox.Location = new System.Drawing.Point(560, 8);
            this.PositionGroupBox.Name = "PositionGroupBox";
            this.PositionGroupBox.Size = new System.Drawing.Size(268, 40);
            this.PositionGroupBox.TabIndex = 4;
            this.PositionGroupBox.TabStop = false;
            this.PositionGroupBox.Text = "Group Summary By";
            // 
            // BookParentRadioButton
            // 
            this.BookParentRadioButton.Location = new System.Drawing.Point(160, 20);
            this.BookParentRadioButton.Name = "BookParentRadioButton";
            this.BookParentRadioButton.Size = new System.Drawing.Size(96, 12);
            this.BookParentRadioButton.TabIndex = 2;
            this.BookParentRadioButton.Text = "Book Parent";
            this.BookParentRadioButton.Click += new System.EventHandler(this.SummaryParameterChanged);
            // 
            // BookRadioButton
            // 
            this.BookRadioButton.Location = new System.Drawing.Point(100, 20);
            this.BookRadioButton.Name = "BookRadioButton";
            this.BookRadioButton.Size = new System.Drawing.Size(52, 12);
            this.BookRadioButton.TabIndex = 1;
            this.BookRadioButton.Text = "Book";
            this.BookRadioButton.Click += new System.EventHandler(this.SummaryParameterChanged);
            // 
            // SecurityRadioButton
            // 
            this.SecurityRadioButton.Checked = true;
            this.SecurityRadioButton.Location = new System.Drawing.Point(20, 20);
            this.SecurityRadioButton.Name = "SecurityRadioButton";
            this.SecurityRadioButton.Size = new System.Drawing.Size(72, 12);
            this.SecurityRadioButton.TabIndex = 0;
            this.SecurityRadioButton.TabStop = true;
            this.SecurityRadioButton.Text = "Security";
            this.SecurityRadioButton.Click += new System.EventHandler(this.SummaryParameterChanged);
            // 
            // SummaryCheckBox
            // 
            this.SummaryCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
            this.SummaryCheckBox.Checked = true;
            this.SummaryCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SummaryCheckBox.Location = new System.Drawing.Point(204, 30);
            this.SummaryCheckBox.Name = "SummaryCheckBox";
            this.SummaryCheckBox.Size = new System.Drawing.Size(128, 18);
            this.SummaryCheckBox.TabIndex = 7;
            this.SummaryCheckBox.Text = "Show:  Summary:";
            this.SummaryCheckBox.CheckedChanged += new System.EventHandler(this.SummaryCheckBox_CheckedChanged);
            // 
            // PoolCodeCheckBox
            // 
            this.PoolCodeCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
            this.PoolCodeCheckBox.Checked = true;
            this.PoolCodeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PoolCodeCheckBox.Location = new System.Drawing.Point(344, 30);
            this.PoolCodeCheckBox.Name = "PoolCodeCheckBox";
            this.PoolCodeCheckBox.Size = new System.Drawing.Size(86, 18);
            this.PoolCodeCheckBox.TabIndex = 8;
            this.PoolCodeCheckBox.Text = "Pool Code:";
            this.PoolCodeCheckBox.CheckedChanged += new System.EventHandler(this.SummaryParameterChanged);
            // 
            // ContractsPanel
            // 
            this.ContractsPanel.Controls.Add(this.BorrowsGrid);
            this.ContractsPanel.Controls.Add(this.ContractsSplitter);
            this.ContractsPanel.Controls.Add(this.LoansGrid);
            this.ContractsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ContractsPanel.Location = new System.Drawing.Point(0, 205);
            this.ContractsPanel.Name = "ContractsPanel";
            this.ContractsPanel.Size = new System.Drawing.Size(1604, 204);
            this.ContractsPanel.TabIndex = 9;
            this.ContractsPanel.Resize += new System.EventHandler(this.ContractsPanel_Resize);
            // 
            // BorrowsGrid
            // 
            this.BorrowsGrid.AllowColSelect = false;
            this.BorrowsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.BorrowsGrid.AllowUpdate = false;
            this.BorrowsGrid.CaptionHeight = 17;
            this.BorrowsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BorrowsGrid.EmptyRows = true;
            this.BorrowsGrid.FetchRowStyles = true;
            this.BorrowsGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.BorrowsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.BorrowsGrid.Location = new System.Drawing.Point(0, 0);
            this.BorrowsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.BorrowsGrid.Name = "BorrowsGrid";
            this.BorrowsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.BorrowsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.BorrowsGrid.PreviewInfo.ZoomFactor = 75;
            this.BorrowsGrid.RecordSelectorWidth = 16;
            this.BorrowsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.BorrowsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.BorrowsGrid.RowHeight = 15;
            this.BorrowsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BorrowsGrid.Size = new System.Drawing.Size(1604, 100);
            this.BorrowsGrid.TabIndex = 2;
            this.BorrowsGrid.Visible = false;
            this.BorrowsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.BorrowsGrid_Paint);
            this.BorrowsGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.BorrowsGrid_AfterFilter);
            this.BorrowsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.BorrowsGrid_FetchRowStyle);
            this.BorrowsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ContractsGrid_FormatText);
            this.BorrowsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Parent\" Dat" +
                "aField=\"BookParent\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
                "l=\"0\" Caption=\"Book\" DataField=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn>" +
                "<C1DataColumn Level=\"0\" Caption=\"Security\" DataField=\"SecId\"><ValueItems /><Grou" +
                "pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbo" +
                "l\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Qu" +
                "antity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><ValueItems /><Grou" +
                "pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Amount\" DataField=\"Amoun" +
                "t\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
                "DataColumn Level=\"0\" Caption=\"\" DataField=\"CollateralCode\"><ValueItems /><GroupI" +
                "nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"RateCode\"><Va" +
                "lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PC\" Data" +
                "Field=\"PoolCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
                "0\" Caption=\"Rate\" DataField=\"FeeRate\" NumberFormat=\"FormatText Event\"><ValueItem" +
                "s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Settle Date\" Da" +
                "taField=\"SettleDate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo />" +
                "</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Contract ID\" DataField=\"Contract" +
                "Id\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S" +
                "ettled\" DataField=\"QuantitySettled\" NumberFormat=\"FormatText Event\"><ValueItems " +
                "/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" DataField=\"Bas" +
                "eType\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
                "=\"Class\" DataField=\"ClassGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
                "aColumn Level=\"0\" Caption=\"Rate\" DataField=\"RebateRate\" NumberFormat=\"FormatText" +
                " Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
                "n=\"M%\" DataField=\"Margin\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
                "fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"IT\" DataField=\"IncomeTracke" +
                "d\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColu" +
                "mn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><GroupInfo /></" +
                "C1DataColumn><C1DataColumn Level=\"0\" Caption=\"E\" DataField=\"IsEasy\"><ValueItems " +
                "Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
                "ption=\"H\" DataField=\"IsHard\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo />" +
                "</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"N\" DataField=\"IsNoLend\"><ValueIt" +
                "ems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
                "\" Caption=\"T\" DataField=\"IsThreshold\"><ValueItems Presentation=\"CheckBox\" /><Gro" +
                "upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Book Group\" DataField=\"" +
                "BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
                "tion=\"S\" DataField=\"IsSettledQuantity\"><ValueItems Presentation=\"CheckBox\" /><Gr" +
                "oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Settled\" DataField=\"Am" +
                "ountSettled\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Data" +
                "Column><C1DataColumn Level=\"0\" Caption=\"S\" DataField=\"IsSettledAmount\"><ValueIte" +
                "ms Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
                " Caption=\"Recalled\" DataField=\"QuantityRecalled\" NumberFormat=\"FormatText Event\"" +
                "><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"R\" D" +
                "ataField=\"HasRecall\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1Data" +
                "Column><C1DataColumn Level=\"0\" Caption=\"Value Date\" DataField=\"ValueDate\" Number" +
                "Format=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColum" +
                "n Level=\"0\" Caption=\"Term Date\" DataField=\"TermDate\" NumberFormat=\"FormatText Ev" +
                "ent\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
                "Div%\" DataField=\"DivRate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
                "fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"C\" DataField=\"DivCallable\">" +
                "<ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn " +
                "Level=\"0\" Caption=\"\" DataField=\"CurrencyIso\"><ValueItems /><GroupInfo /></C1Data" +
                "Column><C1DataColumn Level=\"0\" Caption=\"CD\" DataField=\"CashDepot\"><ValueItems />" +
                "<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"SD\" DataField=\"Secu" +
                "rityDepot\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
                "tion=\"\" DataField=\"MarginCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
                "Column Level=\"0\" Caption=\"Income\" DataField=\"Income\" NumberFormat=\"FormatText Ev" +
                "ent\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
                "Value\" DataField=\"Value\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInf" +
                "o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ratio\" DataField=\"ValueRatio" +
                "\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"E\" " +
                "DataField=\"ValueIsEstimate\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /><" +
                "/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"StatusFlag\" DataField=\"StatusFlag" +
                "\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1Tr" +
                "ueDBGrid.Design.ContextWrapper\"><Data>Style270{}Style107{AlignHorz:Far;BackColor" +
                ":Ivory;}Style287{AlignHorz:Near;BackColor:Ivory;}Style159{}Style151{}Style150{}S" +
                "tyle153{}Style152{}Style155{AlignHorz:Center;BackColor:Ivory;}Style154{AlignHorz" +
                ":Center;}Style361{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert" +
                ":Center;}Style148{AlignHorz:Center;}Style418{AlignHorz:Near;}Style142{AlignHorz:" +
                "Center;}Style143{AlignHorz:Center;BackColor:Ivory;}Style144{}Style145{}Style146{" +
                "}Style147{}Style393{}Style121{}Style391{}Style390{}Style399{}Style296{}Style297{" +
                "}Style294{}Style295{}Style292{AlignHorz:Near;}Style293{AlignHorz:Near;BackColor:" +
                "Ivory;}Style290{}Style291{}Style515{AlignHorz:Far;BackColor:Ivory;}Style514{Alig" +
                "nHorz:Near;}Style298{AlignHorz:Near;}Style299{AlignHorz:Near;BackColor:Ivory;}St" +
                "yle171{}Style170{}Style177{}Style342{}Style343{}Style193{}Style192{}Style346{Ali" +
                "gnHorz:Center;}Style347{AlignHorz:Center;BackColor:Ivory;}Style199{}Style198{}St" +
                "yle508{AlignHorz:Near;}Style509{AlignHorz:Far;BackColor:Ivory;}Style388{AlignHor" +
                "z:Center;}Style389{AlignHorz:Near;BackColor:Ivory;}Style502{AlignHorz:Near;}Styl" +
                "e503{AlignHorz:Near;BackColor:Ivory;}Style504{}Style505{}Style162{}Style185{Alig" +
                "nHorz:Far;BackColor:Ivory;}Style379{}Style378{}Style166{AlignHorz:Near;}Style181" +
                "{}Style334{AlignHorz:Center;}Style337{}Style371{AlignHorz:Far;BackColor:Ivory;}S" +
                "tyle370{AlignHorz:Near;}Style373{}Style372{}Style375{}Style374{}Style377{AlignHo" +
                "rz:Center;BackColor:Ivory;}Style376{AlignHorz:Center;}Style279{}Editor{}Style271" +
                "{}Style272{}Style273{}Style274{}Style275{}Style276{}Style277{AlignHorz:Near;}Sty" +
                "le368{}Style369{}Style110{}Style113{AlignHorz:Far;BackColor:Ivory;}Style112{Alig" +
                "nHorz:Center;}Style360{}Style321{}Style362{}Style363{}Style364{AlignHorz:Center;" +
                "}Style365{AlignHorz:Center;BackColor:Ivory;}Style366{}Style367{}Style286{AlignHo" +
                "rz:Near;}Style285{}Style284{}Style283{}Style282{}Style281{AlignHorz:Near;BackCol" +
                "or:Ivory;}Style280{AlignHorz:Near;}Style289{}Style288{}Style359{AlignHorz:Far;Ba" +
                "ckColor:Ivory;}Style358{AlignHorz:Near;}Style318{}Style353{AlignHorz:Far;BackCol" +
                "or:Ivory;}Style352{AlignHorz:Near;}Style351{}Style350{}Style357{}Style356{}Style" +
                "355{}Style354{}Style258{}Style259{}Selected{ForeColor:HighlightText;BackColor:Hi" +
                "ghlight;}Style252{}Style253{}Style250{AlignHorz:Near;}Style251{AlignHorz:Far;Bac" +
                "kColor:Ivory;}Style256{AlignHorz:Near;}Style257{AlignHorz:Far;BackColor:Ivory;}S" +
                "tyle254{}Style255{}Style158{}Style348{}Style349{}Style308{}Style309{}Style306{}S" +
                "tyle307{}Style420{}Style427{}Style340{AlignHorz:Center;}Style341{AlignHorz:Cente" +
                "r;BackColor:Ivory;}Style157{}Style156{}Style344{}Style345{}Style268{Font:Verdana" +
                ", 8.25pt;}Style261{}Style260{}Style263{AlignHorz:Center;BackColor:Ivory;}Style26" +
                "2{AlignHorz:Near;}Style265{}Style264{}Style267{}Style266{}Style149{AlignHorz:Cen" +
                "ter;BackColor:Ivory;}Style129{}Style339{}Style338{}Style140{}Style141{}Style335{" +
                "AlignHorz:Center;BackColor:Ivory;}Style481{}Style480{}Style336{}Style331{}Style3" +
                "30{}Style333{}Style332{}Style486{}Style489{}Style488{}Style238{AlignHorz:Near;}S" +
                "tyle239{AlignHorz:Near;BackColor:Ivory;}Style234{}Style235{}Style236{}Style237{}" +
                "Style230{}Style231{}Style232{AlignHorz:Near;}Style233{AlignHorz:Center;BackColor" +
                ":Ivory;}Style179{AlignHorz:Center;BackColor:Ivory;}Style178{AlignHorz:Near;}Styl" +
                "e328{AlignHorz:Center;}Style329{AlignHorz:Center;BackColor:Ivory;}Style173{Align" +
                "Horz:Far;BackColor:Ivory;}Style172{AlignHorz:Near;}Style324{}Style325{}Style326{" +
                "}Style327{}Style320{}Style174{}Style322{AlignHorz:Near;}Style323{AlignHorz:Near;" +
                "BackColor:Ivory;}Style249{}Style248{}Style243{}Style242{}Style241{}Style240{}Sty" +
                "le247{}Style246{}Style245{AlignHorz:Far;BackColor:Ivory;}Style244{AlignHorz:Near" +
                ";}Style168{}Style169{}Style319{}Style163{}Style317{AlignHorz:Center;BackColor:Iv" +
                "ory;}Style316{AlignHorz:Center;}Style315{}Style314{}Style313{}Style165{}Style311" +
                "{AlignHorz:Center;BackColor:Ivory;}Style310{AlignHorz:Center;}Style218{}Style219" +
                "{}Style216{}Style217{}Style214{AlignHorz:Near;}Style215{AlignHorz:Center;BackCol" +
                "or:Ivory;}Style212{}Style213{}Style210{}Style211{}Style119{AlignHorz:Center;Back" +
                "Color:Ivory;}Style118{AlignHorz:Near;}Style115{}Style114{}Style117{}Style116{}St" +
                "yle111{}Style305{AlignHorz:Near;BackColor:Ivory;}Style302{}Style303{}Style300{}S" +
                "tyle301{}Style229{}Style228{}Style225{}Style224{}Style227{AlignHorz:Center;BackC" +
                "olor:Ivory;}Style226{AlignHorz:Near;}Style221{AlignHorz:Center;BackColor:Ivory;}" +
                "Style220{AlignHorz:Center;}Style223{}Style222{}Style108{}Style109{}Style104{}Sty" +
                "le105{}Style106{AlignHorz:Near;}Normal{Font:Verdana, 8.25pt;}Style100{AlignHorz:" +
                "Near;}Style101{AlignHorz:Near;BackColor:Ivory;}Style102{}Style103{}Style519{}Sty" +
                "le518{}Style511{}Style510{}Style139{}Style138{}Style137{AlignHorz:Center;BackCol" +
                "or:Ivory;}Style136{AlignHorz:Center;}Style517{}Style516{}Style419{AlignHorz:Far;" +
                "BackColor:Ivory;}Style132{}Style131{AlignHorz:Near;}Style130{AlignHorz:Near;}Sty" +
                "le410{}Style411{}Style412{AlignHorz:Near;}Style413{AlignHorz:Far;BackColor:Ivory" +
                ";}Style209{AlignHorz:Far;BackColor:Ivory;}Style208{AlignHorz:Near;}Style207{}Sty" +
                "le206{}Style205{}Style204{}Style203{AlignHorz:Center;BackColor:Ivory;}Style202{A" +
                "lignHorz:Near;}Style201{}Style200{}Style500{}Style501{}Style128{}Inactive{ForeCo" +
                "lor:InactiveCaptionText;BackColor:InactiveCaption;}Style126{}Style127{}Style506{" +
                "}Style507{}Style122{}Style123{}Style120{}Style83{AlignHorz:Far;BackColor:Ivory;}" +
                "Style82{AlignHorz:Near;}Style89{AlignHorz:Far;BackColor:Ivory;}Style88{AlignHorz" +
                ":Near;}Style85{}Style84{}Style87{}Style86{}Style81{}Style80{}Style532{}Style531{" +
                "}Style530{}Style94{AlignHorz:Center;}Style95{AlignHorz:Center;BackColor:Ivory;}S" +
                "tyle96{}Style97{}Style438{}Style439{}Style92{}Style93{}Style432{}Style433{}Style" +
                "430{AlignHorz:Center;}Style99{}Style436{AlignHorz:Near;}Style437{AlignHorz:Near;" +
                "BackColor:Ivory;}Style434{}Style435{}Style528{}Style529{}Style90{}Style91{}Style" +
                "522{}Style523{}Style520{AlignHorz:Center;}Style521{AlignHorz:Center;BackColor:Iv" +
                "ory;}Style526{AlignHorz:Near;}Style527{AlignHorz:Near;}Style524{}Style409{}Style" +
                "408{}Style401{AlignHorz:Center;BackColor:Ivory;}Style400{AlignHorz:Center;}Style" +
                "403{}Style402{}Style405{}Style404{}Style407{AlignHorz:Center;BackColor:Ivory;}St" +
                "yle406{AlignHorz:Center;}Style98{}Style458{}Style459{}Style454{AlignHorz:Near;}S" +
                "tyle455{AlignHorz:Center;BackColor:Ivory;}Style456{}Style457{}Style450{}Style451" +
                "{}Style452{}Style453{}Style49{}Style48{}Style429{}Style428{}Style41{AlignHorz:Fa" +
                "r;BackColor:Ivory;}Style40{AlignHorz:Near;}Style423{}Style42{}Style45{}Style44{}" +
                "Style47{AlignHorz:Far;BackColor:Ivory;}Style46{AlignHorz:Near;}Style425{AlignHor" +
                "z:Near;BackColor:Ivory;}Style424{AlignHorz:Near;}Style58{AlignHorz:Near;}Style59" +
                "{AlignHorz:Near;BackColor:Ivory;}Style50{}Style51{}Style52{AlignHorz:Center;}Sty" +
                "le53{AlignHorz:Near;BackColor:Ivory;}Style54{}Style55{}Style56{}Style57{}Style47" +
                "0{}Style471{}Style43{}Style472{AlignHorz:Center;}Style69{}Style68{}Style449{Alig" +
                "nHorz:Center;BackColor:Ivory;}Style448{AlignHorz:Near;}Style63{}Style62{}Style61" +
                "{}Style60{}Style67{}Style66{}Style65{AlignHorz:Center;BackColor:Ivory;}Style64{A" +
                "lignHorz:Center;}Style443{AlignHorz:Center;BackColor:Ivory;}Style442{AlignHorz:C" +
                "enter;}Style78{}Style79{}Style72{}Style73{}Style70{AlignHorz:Near;}Style71{Align" +
                "Horz:Far;BackColor:Ivory;}Style76{AlignHorz:Near;}Style77{AlignHorz:Center;BackC" +
                "olor:Ivory;}Style74{}Style75{}Style469{}Style468{}Style467{AlignHorz:Far;BackCol" +
                "or:Ivory;}Style466{AlignHorz:Near;}Style465{}Style464{}Style463{}Style462{}Style" +
                "461{AlignHorz:Center;BackColor:Ivory;}Style460{AlignHorz:Near;}Style278{}Style18" +
                "{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;Back" +
                "Color:Ivory;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Footer{}Style398" +
                "{}Style125{AlignHorz:Near;BackColor:Ivory;}Style29{AlignHorz:Near;BackColor:Ivor" +
                "y;}Style28{AlignHorz:Near;}Style27{}Style26{}Style25{}Style24{}Style23{AlignHorz" +
                ":Near;BackColor:Ivory;}Style22{AlignHorz:Near;}Style21{}Style20{}Style175{}Style" +
                "414{}Style415{}Style416{}Style417{}Style38{}Style39{}Style36{}Style37{}Style34{A" +
                "lignHorz:Near;}Style35{AlignHorz:Near;BackColor:Ivory;}Style32{}Style33{}Style30" +
                "{}Style31{}Style183{}Style188{}Style189{}RecordSelector{AlignImage:Center;}Style" +
                "513{}Style512{}Style9{}Style8{}Style5{}Style4{}Style7{}Style6{}Style1{Font:Verda" +
                "na, 8.25pt;}Style3{}Style2{Font:Verdana, 8.25pt;}Style431{AlignHorz:Far;BackColo" +
                "r:Ivory;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style396{}Sty" +
                "le394{AlignHorz:Near;}Style490{AlignHorz:Center;}Style491{AlignHorz:Center;BackC" +
                "olor:Ivory;}Style492{}Style493{}Style494{}Style495{}Style496{AlignHorz:Center;}S" +
                "tyle497{AlignHorz:Center;BackColor:Ivory;}Style498{}Style499{}Style135{}Style134" +
                "{}Style133{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" +
                "or:ControlText;BackColor:Control;}FilterBar{AlignVert:Center;}Style525{}OddRow{}" +
                "Style422{}Style421{}Style124{AlignHorz:Near;}Caption{AlignHorz:Center;}Style426{" +
                "}Style487{}Style195{}Style194{}Style197{AlignHorz:Center;BackColor:Ivory;}Style1" +
                "96{AlignHorz:Near;}Style191{AlignHorz:Center;BackColor:Ivory;}Style190{AlignHorz" +
                ":Near;}Style483{}Style482{}Style485{AlignHorz:Far;BackColor:Ivory;}Style484{Alig" +
                "nHorz:Near;}Style176{}Style479{AlignHorz:Near;BackColor:Ivory;}Style476{}Style47" +
                "7{}Style474{}Style475{}Style269{}Style473{AlignHorz:Center;BackColor:Ivory;}Styl" +
                "e478{AlignHorz:Near;}Style184{AlignHorz:Near;}Style186{}Style187{}Style180{}Styl" +
                "e397{}Style182{}Style395{AlignHorz:Far;BackColor:Ivory;}Style160{AlignHorz:Near;" +
                "}Style161{AlignHorz:Near;BackColor:Ivory;}Style392{}Style167{AlignHorz:Center;Ba" +
                "ckColor:Ivory;}Style164{}Style445{}Style444{}Style447{}Style446{}Style441{}Style" +
                "440{}Style312{}EvenRow{BackColor:Aqua;}Style386{}Style387{}Style384{}Style385{}S" +
                "tyle382{AlignHorz:Near;}Style383{AlignHorz:Far;BackColor:Ivory;}Style380{}Style3" +
                "81{}Style304{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeV" +
                "iew HBarStyle=\"None\" VBarStyle=\"None\" AllowColSelect=\"False\" Name=\"Split[0,0]\" A" +
                "llowRowSizing=\"None\" AllowVerticalSizing=\"False\" CaptionHeight=\"18\" ColumnCaptio" +
                "nHeight=\"18\" ColumnFooterHeight=\"20\" FetchRowStyles=\"True\" MarqueeStyle=\"DottedR" +
                "owBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" M" +
                "inWidth=\"100\" HorizontalScrollGroup=\"2\"><Caption>Borrows</Caption><SplitSize>12<" +
                "/SplitSize><SplitSizeMode>NumberOfColumns</SplitSizeMode><CaptionStyle parent=\"S" +
                "tyle2\" me=\"Style277\" /><EditorStyle parent=\"Editor\" me=\"Style269\" /><EvenRowStyl" +
                "e parent=\"EvenRow\" me=\"Style275\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style5" +
                "32\" /><FooterStyle parent=\"Footer\" me=\"Style271\" /><GroupStyle parent=\"Group\" me" +
                "=\"Style279\" /><HeadingStyle parent=\"Heading\" me=\"Style270\" /><HighLightRowStyle " +
                "parent=\"HighlightRow\" me=\"Style274\" /><InactiveStyle parent=\"Inactive\" me=\"Style" +
                "273\" /><OddRowStyle parent=\"OddRow\" me=\"Style276\" /><RecordSelectorStyle parent=" +
                "\"RecordSelector\" me=\"Style278\" /><SelectedStyle parent=\"Selected\" me=\"Style272\" " +
                "/><Style parent=\"Normal\" me=\"Style268\" /><internalCols><C1DisplayColumn><Heading" +
                "Style parent=\"Style270\" me=\"Style280\" /><Style parent=\"Style268\" me=\"Style281\" /" +
                "><FooterStyle parent=\"Style271\" me=\"Style282\" /><EditorStyle parent=\"Style269\" m" +
                "e=\"Style283\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style285\" /><GroupFooterS" +
                "tyle parent=\"Style268\" me=\"Style284\" /><Visible>True</Visible><ColumnDivider>Gai" +
                "nsboro,Single</ColumnDivider><Width>60</Width><Height>15</Height><DCIdx>23</DCId" +
                "x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style28" +
                "6\" /><Style parent=\"Style268\" me=\"Style287\" /><FooterStyle parent=\"Style271\" me=" +
                "\"Style288\" /><EditorStyle parent=\"Style269\" me=\"Style289\" /><GroupHeaderStyle pa" +
                "rent=\"Style268\" me=\"Style291\" /><GroupFooterStyle parent=\"Style268\" me=\"Style290" +
                "\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width" +
                ">60</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn" +
                "><HeadingStyle parent=\"Style270\" me=\"Style292\" /><Style parent=\"Style268\" me=\"St" +
                "yle293\" /><FooterStyle parent=\"Style271\" me=\"Style294\" /><EditorStyle parent=\"St" +
                "yle269\" me=\"Style295\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style297\" /><Gro" +
                "upFooterStyle parent=\"Style268\" me=\"Style296\" /><Visible>True</Visible><ColumnDi" +
                "vider>Gainsboro,Single</ColumnDivider><Width>60</Width><Height>15</Height><DCIdx" +
                ">1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=" +
                "\"Style298\" /><Style parent=\"Style268\" me=\"Style299\" /><FooterStyle parent=\"Style" +
                "271\" me=\"Style300\" /><EditorStyle parent=\"Style269\" me=\"Style301\" /><GroupHeader" +
                "Style parent=\"Style268\" me=\"Style303\" /><GroupFooterStyle parent=\"Style268\" me=\"" +
                "Style302\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivide" +
                "r><Width>95</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1Displ" +
                "ayColumn><HeadingStyle parent=\"Style270\" me=\"Style304\" /><Style parent=\"Style268" +
                "\" me=\"Style305\" /><FooterStyle parent=\"Style271\" me=\"Style306\" /><EditorStyle pa" +
                "rent=\"Style269\" me=\"Style307\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style309" +
                "\" /><GroupFooterStyle parent=\"Style268\" me=\"Style308\" /><Visible>True</Visible><" +
                "ColumnDivider>Gainsboro,Single</ColumnDivider><Width>75</Width><Height>15</Heigh" +
                "t><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
                "270\" me=\"Style310\" /><Style parent=\"Style268\" me=\"Style311\" /><FooterStyle paren" +
                "t=\"Style271\" me=\"Style312\" /><EditorStyle parent=\"Style269\" me=\"Style313\" /><Gro" +
                "upHeaderStyle parent=\"Style268\" me=\"Style315\" /><GroupFooterStyle parent=\"Style2" +
                "68\" me=\"Style314\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Colu" +
                "mnDivider><Width>25</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style316\" /><Style parent=\"" +
                "Style268\" me=\"Style317\" /><FooterStyle parent=\"Style271\" me=\"Style318\" /><Editor" +
                "Style parent=\"Style269\" me=\"Style319\" /><GroupHeaderStyle parent=\"Style268\" me=\"" +
                "Style321\" /><GroupFooterStyle parent=\"Style268\" me=\"Style320\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>20</Width><Height>1" +
                "5</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
                "nt=\"Style270\" me=\"Style322\" /><Style parent=\"Style268\" me=\"Style323\" /><FooterSt" +
                "yle parent=\"Style271\" me=\"Style324\" /><EditorStyle parent=\"Style269\" me=\"Style32" +
                "5\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style327\" /><GroupFooterStyle paren" +
                "t=\"Style268\" me=\"Style326\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Sin" +
                "gle</ColumnDivider><Width>65</Width><Height>15</Height><DCIdx>14</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style328\" /><Styl" +
                "e parent=\"Style268\" me=\"Style329\" /><FooterStyle parent=\"Style271\" me=\"Style330\"" +
                " /><EditorStyle parent=\"Style269\" me=\"Style331\" /><GroupHeaderStyle parent=\"Styl" +
                "e268\" me=\"Style333\" /><GroupFooterStyle parent=\"Style268\" me=\"Style332\" /><Visib" +
                "le>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</Width><" +
                "Height>15</Height><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
                "yle parent=\"Style270\" me=\"Style334\" /><Style parent=\"Style268\" me=\"Style335\" /><" +
                "FooterStyle parent=\"Style271\" me=\"Style336\" /><EditorStyle parent=\"Style269\" me=" +
                "\"Style337\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style339\" /><GroupFooterSty" +
                "le parent=\"Style268\" me=\"Style338\" /><Visible>True</Visible><ColumnDivider>Gains" +
                "boro,None</ColumnDivider><Width>16</Width><Height>15</Height><DCIdx>20</DCIdx></" +
                "C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style340\" /" +
                "><Style parent=\"Style268\" me=\"Style341\" /><FooterStyle parent=\"Style271\" me=\"Sty" +
                "le342\" /><EditorStyle parent=\"Style269\" me=\"Style343\" /><GroupHeaderStyle parent" +
                "=\"Style268\" me=\"Style345\" /><GroupFooterStyle parent=\"Style268\" me=\"Style344\" />" +
                "<Visible>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</W" +
                "idth><Height>15</Height><DCIdx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
                "dingStyle parent=\"Style270\" me=\"Style346\" /><Style parent=\"Style268\" me=\"Style34" +
                "7\" /><FooterStyle parent=\"Style271\" me=\"Style348\" /><EditorStyle parent=\"Style26" +
                "9\" me=\"Style349\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style351\" /><GroupFoo" +
                "terStyle parent=\"Style268\" me=\"Style350\" /><Visible>True</Visible><ColumnDivider" +
                ">Gainsboro,None</ColumnDivider><Width>16</Width><Height>15</Height><DCIdx>22</DC" +
                "Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style" +
                "352\" /><Style parent=\"Style268\" me=\"Style353\" /><FooterStyle parent=\"Style271\" m" +
                "e=\"Style354\" /><EditorStyle parent=\"Style269\" me=\"Style355\" /><GroupHeaderStyle " +
                "parent=\"Style268\" me=\"Style357\" /><GroupFooterStyle parent=\"Style268\" me=\"Style3" +
                "56\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>90</Width><Height>15<" +
                "/Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
                "\"Style270\" me=\"Style358\" /><Style parent=\"Style268\" me=\"Style359\" /><FooterStyle" +
                " parent=\"Style271\" me=\"Style360\" /><EditorStyle parent=\"Style269\" me=\"Style361\" " +
                "/><GroupHeaderStyle parent=\"Style268\" me=\"Style363\" /><GroupFooterStyle parent=\"" +
                "Style268\" me=\"Style362\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>9" +
                "0</Width><Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
                "<HeadingStyle parent=\"Style270\" me=\"Style364\" /><Style parent=\"Style268\" me=\"Sty" +
                "le365\" /><FooterStyle parent=\"Style271\" me=\"Style366\" /><EditorStyle parent=\"Sty" +
                "le269\" me=\"Style367\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style369\" /><Grou" +
                "pFooterStyle parent=\"Style268\" me=\"Style368\" /><ColumnDivider>DarkGray,Single</C" +
                "olumnDivider><Width>20</Width><Height>15</Height><DCIdx>24</DCIdx></C1DisplayCol" +
                "umn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style370\" /><Style pare" +
                "nt=\"Style268\" me=\"Style371\" /><FooterStyle parent=\"Style271\" me=\"Style372\" /><Ed" +
                "itorStyle parent=\"Style269\" me=\"Style373\" /><GroupHeaderStyle parent=\"Style268\" " +
                "me=\"Style375\" /><GroupFooterStyle parent=\"Style268\" me=\"Style374\" /><ColumnDivid" +
                "er>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>27</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style376\" /><Style " +
                "parent=\"Style268\" me=\"Style377\" /><FooterStyle parent=\"Style271\" me=\"Style378\" /" +
                "><EditorStyle parent=\"Style269\" me=\"Style379\" /><GroupHeaderStyle parent=\"Style2" +
                "68\" me=\"Style381\" /><GroupFooterStyle parent=\"Style268\" me=\"Style380\" /><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</Height><DCIdx" +
                ">28</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me" +
                "=\"Style382\" /><Style parent=\"Style268\" me=\"Style383\" /><FooterStyle parent=\"Styl" +
                "e271\" me=\"Style384\" /><EditorStyle parent=\"Style269\" me=\"Style385\" /><GroupHeade" +
                "rStyle parent=\"Style268\" me=\"Style387\" /><GroupFooterStyle parent=\"Style268\" me=" +
                "\"Style386\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>95</Width><Hei" +
                "ght>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
                "parent=\"Style270\" me=\"Style388\" /><Style parent=\"Style268\" me=\"Style389\" /><Foot" +
                "erStyle parent=\"Style271\" me=\"Style390\" /><EditorStyle parent=\"Style269\" me=\"Sty" +
                "le391\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style393\" /><GroupFooterStyle p" +
                "arent=\"Style268\" me=\"Style392\" /><ColumnDivider>DarkGray,Single</ColumnDivider><" +
                "Width>20</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayC" +
                "olumn><HeadingStyle parent=\"Style270\" me=\"Style394\" /><Style parent=\"Style268\" m" +
                "e=\"Style395\" /><FooterStyle parent=\"Style271\" me=\"Style396\" /><EditorStyle paren" +
                "t=\"Style269\" me=\"Style397\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style399\" /" +
                "><GroupFooterStyle parent=\"Style268\" me=\"Style398\" /><ColumnDivider>DarkGray,Sin" +
                "gle</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>25</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style400\" /><Styl" +
                "e parent=\"Style268\" me=\"Style401\" /><FooterStyle parent=\"Style271\" me=\"Style402\"" +
                " /><EditorStyle parent=\"Style269\" me=\"Style403\" /><GroupHeaderStyle parent=\"Styl" +
                "e268\" me=\"Style405\" /><GroupFooterStyle parent=\"Style268\" me=\"Style404\" /><Colum" +
                "nDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</Height><DCI" +
                "dx>26</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" " +
                "me=\"Style406\" /><Style parent=\"Style268\" me=\"Style407\" /><FooterStyle parent=\"St" +
                "yle271\" me=\"Style408\" /><EditorStyle parent=\"Style269\" me=\"Style409\" /><GroupHea" +
                "derStyle parent=\"Style268\" me=\"Style411\" /><GroupFooterStyle parent=\"Style268\" m" +
                "e=\"Style410\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>35</Width><H" +
                "eight>15</Height><DCIdx>33</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
                "le parent=\"Style270\" me=\"Style412\" /><Style parent=\"Style268\" me=\"Style413\" /><F" +
                "ooterStyle parent=\"Style271\" me=\"Style414\" /><EditorStyle parent=\"Style269\" me=\"" +
                "Style415\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style417\" /><GroupFooterStyl" +
                "e parent=\"Style268\" me=\"Style416\" /><ColumnDivider>DarkGray,Single</ColumnDivide" +
                "r><Width>58</Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1Displ" +
                "ayColumn><HeadingStyle parent=\"Style270\" me=\"Style418\" /><Style parent=\"Style268" +
                "\" me=\"Style419\" /><FooterStyle parent=\"Style271\" me=\"Style420\" /><EditorStyle pa" +
                "rent=\"Style269\" me=\"Style421\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style423" +
                "\" /><GroupFooterStyle parent=\"Style268\" me=\"Style422\" /><ColumnDivider>DarkGray," +
                "Single</ColumnDivider><Width>58</Width><Height>15</Height><DCIdx>15</DCIdx></C1D" +
                "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style424\" /><S" +
                "tyle parent=\"Style268\" me=\"Style425\" /><FooterStyle parent=\"Style271\" me=\"Style4" +
                "26\" /><EditorStyle parent=\"Style269\" me=\"Style427\" /><GroupHeaderStyle parent=\"S" +
                "tyle268\" me=\"Style429\" /><GroupFooterStyle parent=\"Style268\" me=\"Style428\" /><Co" +
                "lumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</Height><" +
                "DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270" +
                "\" me=\"Style430\" /><Style parent=\"Style268\" me=\"Style431\" /><FooterStyle parent=\"" +
                "Style271\" me=\"Style432\" /><EditorStyle parent=\"Style269\" me=\"Style433\" /><GroupH" +
                "eaderStyle parent=\"Style268\" me=\"Style435\" /><GroupFooterStyle parent=\"Style268\"" +
                " me=\"Style434\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>35</Width>" +
                "<Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
                "tyle parent=\"Style270\" me=\"Style436\" /><Style parent=\"Style268\" me=\"Style437\" />" +
                "<FooterStyle parent=\"Style271\" me=\"Style438\" /><EditorStyle parent=\"Style269\" me" +
                "=\"Style439\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style441\" /><GroupFooterSt" +
                "yle parent=\"Style268\" me=\"Style440\" /><ColumnDivider>DarkGray,Single</ColumnDivi" +
                "der><Width>20</Width><Height>15</Height><DCIdx>36</DCIdx></C1DisplayColumn><C1Di" +
                "splayColumn><HeadingStyle parent=\"Style270\" me=\"Style442\" /><Style parent=\"Style" +
                "268\" me=\"Style443\" /><FooterStyle parent=\"Style271\" me=\"Style444\" /><EditorStyle" +
                " parent=\"Style269\" me=\"Style445\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style" +
                "447\" /><GroupFooterStyle parent=\"Style268\" me=\"Style446\" /><ColumnDivider>DarkGr" +
                "ay,Single</ColumnDivider><Width>25</Width><Height>15</Height><DCIdx>17</DCIdx></" +
                "C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style448\" /" +
                "><Style parent=\"Style268\" me=\"Style449\" /><FooterStyle parent=\"Style271\" me=\"Sty" +
                "le450\" /><EditorStyle parent=\"Style269\" me=\"Style451\" /><GroupHeaderStyle parent" +
                "=\"Style268\" me=\"Style453\" /><GroupFooterStyle parent=\"Style268\" me=\"Style452\" />" +
                "<ColumnDivider>DarkGray,Single</ColumnDivider><Width>80</Width><Height>15</Heigh" +
                "t><DCIdx>29</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
                "e270\" me=\"Style454\" /><Style parent=\"Style268\" me=\"Style455\" /><FooterStyle pare" +
                "nt=\"Style271\" me=\"Style456\" /><EditorStyle parent=\"Style269\" me=\"Style457\" /><Gr" +
                "oupHeaderStyle parent=\"Style268\" me=\"Style459\" /><GroupFooterStyle parent=\"Style" +
                "268\" me=\"Style458\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>80</Wi" +
                "dth><Height>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
                "ingStyle parent=\"Style270\" me=\"Style460\" /><Style parent=\"Style268\" me=\"Style461" +
                "\" /><FooterStyle parent=\"Style271\" me=\"Style462\" /><EditorStyle parent=\"Style269" +
                "\" me=\"Style463\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style465\" /><GroupFoot" +
                "erStyle parent=\"Style268\" me=\"Style464\" /><ColumnDivider>DarkGray,Single</Column" +
                "Divider><Width>80</Width><Height>15</Height><DCIdx>30</DCIdx></C1DisplayColumn><" +
                "C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style466\" /><Style parent=\"S" +
                "tyle268\" me=\"Style467\" /><FooterStyle parent=\"Style271\" me=\"Style468\" /><EditorS" +
                "tyle parent=\"Style269\" me=\"Style469\" /><GroupHeaderStyle parent=\"Style268\" me=\"S" +
                "tyle471\" /><GroupFooterStyle parent=\"Style268\" me=\"Style470\" /><ColumnDivider>Da" +
                "rkGray,Single</ColumnDivider><Width>45</Width><Height>15</Height><DCIdx>31</DCId" +
                "x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style47" +
                "2\" /><Style parent=\"Style268\" me=\"Style473\" /><FooterStyle parent=\"Style271\" me=" +
                "\"Style474\" /><EditorStyle parent=\"Style269\" me=\"Style475\" /><GroupHeaderStyle pa" +
                "rent=\"Style268\" me=\"Style477\" /><GroupFooterStyle parent=\"Style268\" me=\"Style476" +
                "\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</H" +
                "eight><DCIdx>32</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
                "Style270\" me=\"Style478\" /><Style parent=\"Style268\" me=\"Style479\" /><FooterStyle " +
                "parent=\"Style271\" me=\"Style480\" /><EditorStyle parent=\"Style269\" me=\"Style481\" /" +
                "><GroupHeaderStyle parent=\"Style268\" me=\"Style483\" /><GroupFooterStyle parent=\"S" +
                "tyle268\" me=\"Style482\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>17" +
                "5</Width><Height>15</Height><DCIdx>18</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
                "<HeadingStyle parent=\"Style270\" me=\"Style484\" /><Style parent=\"Style268\" me=\"Sty" +
                "le485\" /><FooterStyle parent=\"Style271\" me=\"Style486\" /><EditorStyle parent=\"Sty" +
                "le269\" me=\"Style487\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style489\" /><Grou" +
                "pFooterStyle parent=\"Style268\" me=\"Style488\" /><ColumnDivider>DarkGray,Single</C" +
                "olumnDivider><Width>90</Width><Height>15</Height><DCIdx>11</DCIdx></C1DisplayCol" +
                "umn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style490\" /><Style pare" +
                "nt=\"Style268\" me=\"Style491\" /><FooterStyle parent=\"Style271\" me=\"Style492\" /><Ed" +
                "itorStyle parent=\"Style269\" me=\"Style493\" /><GroupHeaderStyle parent=\"Style268\" " +
                "me=\"Style495\" /><GroupFooterStyle parent=\"Style268\" me=\"Style494\" /><ColumnDivid" +
                "er>DarkGray,Single</ColumnDivider><Width>30</Width><Height>15</Height><DCIdx>34<" +
                "/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"St" +
                "yle496\" /><Style parent=\"Style268\" me=\"Style497\" /><FooterStyle parent=\"Style271" +
                "\" me=\"Style498\" /><EditorStyle parent=\"Style269\" me=\"Style499\" /><GroupHeaderSty" +
                "le parent=\"Style268\" me=\"Style501\" /><GroupFooterStyle parent=\"Style268\" me=\"Sty" +
                "le500\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>30</Width><Height>" +
                "15</Height><DCIdx>35</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style270\" me=\"Style502\" /><Style parent=\"Style268\" me=\"Style503\" /><FooterS" +
                "tyle parent=\"Style271\" me=\"Style504\" /><EditorStyle parent=\"Style269\" me=\"Style5" +
                "05\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style507\" /><GroupFooterStyle pare" +
                "nt=\"Style268\" me=\"Style506\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
                "th>75</Width><Height>15</Height><DCIdx>37</DCIdx></C1DisplayColumn><C1DisplayCol" +
                "umn><HeadingStyle parent=\"Style270\" me=\"Style508\" /><Style parent=\"Style268\" me=" +
                "\"Style509\" /><FooterStyle parent=\"Style271\" me=\"Style510\" /><EditorStyle parent=" +
                "\"Style269\" me=\"Style511\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style513\" /><" +
                "GroupFooterStyle parent=\"Style268\" me=\"Style512\" /><ColumnDivider>DarkGray,Singl" +
                "e</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>38</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style514\" /><Style " +
                "parent=\"Style268\" me=\"Style515\" /><FooterStyle parent=\"Style271\" me=\"Style516\" /" +
                "><EditorStyle parent=\"Style269\" me=\"Style517\" /><GroupHeaderStyle parent=\"Style2" +
                "68\" me=\"Style519\" /><GroupFooterStyle parent=\"Style268\" me=\"Style518\" /><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Width>45</Width><Height>15</Height><DCIdx" +
                ">39</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me" +
                "=\"Style520\" /><Style parent=\"Style268\" me=\"Style521\" /><FooterStyle parent=\"Styl" +
                "e271\" me=\"Style522\" /><EditorStyle parent=\"Style269\" me=\"Style523\" /><GroupHeade" +
                "rStyle parent=\"Style268\" me=\"Style525\" /><GroupFooterStyle parent=\"Style268\" me=" +
                "\"Style524\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Hei" +
                "ght>15</Height><DCIdx>40</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
                " parent=\"Style270\" me=\"Style130\" /><Style parent=\"Style268\" me=\"Style131\" /><Foo" +
                "terStyle parent=\"Style271\" me=\"Style132\" /><EditorStyle parent=\"Style269\" me=\"St" +
                "yle133\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style135\" /><GroupFooterStyle " +
                "parent=\"Style268\" me=\"Style134\" /><ColumnDivider>DarkGray,Single</ColumnDivider>" +
                "<Height>15</Height><DCIdx>41</DCIdx></C1DisplayColumn></internalCols><ClientRect" +
                ">0, 0, 548, 96</ClientRect><BorderSide>Right</BorderSide></C1.Win.C1TrueDBGrid.M" +
                "ergeView><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" Allo" +
                "wColSelect=\"False\" Name=\"Split[0,1]\" AllowRowSizing=\"None\" AllowHorizontalSizing" +
                "=\"False\" AllowVerticalSizing=\"False\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\"" +
                " ColumnFooterHeight=\"20\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedRowBorder\"" +
                " RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalSc" +
                "rollGroup=\"1\" HorizontalScrollGroup=\"1\"><Caption>Detail</Caption><CaptionStyle p" +
                "arent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRo" +
                "wStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Sty" +
                "le13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me" +
                "=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle par" +
                "ent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" />" +
                "<OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSe" +
                "lector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style par" +
                "ent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"" +
                "Style2\" me=\"Style160\" /><Style parent=\"Style1\" me=\"Style161\" /><FooterStyle pare" +
                "nt=\"Style3\" me=\"Style162\" /><EditorStyle parent=\"Style5\" me=\"Style163\" /><GroupH" +
                "eaderStyle parent=\"Style1\" me=\"Style165\" /><GroupFooterStyle parent=\"Style1\" me=" +
                "\"Style164\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Width><He" +
                "ight>15</Height><DCIdx>23</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
                "e parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterSt" +
                "yle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><" +
                "GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1" +
                "\" me=\"Style20\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Width" +
                "><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
                "tyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Foote" +
                "rStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" " +
                "/><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Sty" +
                "le1\" me=\"Style26\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Wi" +
                "dth><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
                "ngStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><Fo" +
                "oterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style3" +
                "1\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"" +
                "Style1\" me=\"Style32\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95<" +
                "/Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
                "adingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" />" +
                "<FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
                "le37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle paren" +
                "t=\"Style1\" me=\"Style38\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>" +
                "75</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
                "<HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\"" +
                " /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"" +
                "Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle pa" +
                "rent=\"Style1\" me=\"Style68\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Wid" +
                "th>25</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColu" +
                "mn><HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Style1\" me=\"Style" +
                "95\" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle parent=\"Style5\" m" +
                "e=\"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><GroupFooterStyle" +
                " parent=\"Style1\" me=\"Style98\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><" +
                "Width>20</Width><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1Display" +
                "Column><HeadingStyle parent=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me=\"" +
                "Style101\" /><FooterStyle parent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"St" +
                "yle5\" me=\"Style103\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style105\" /><GroupFo" +
                "oterStyle parent=\"Style1\" me=\"Style104\" /><ColumnDivider>DarkGray,Single</Column" +
                "Divider><Width>65</Width><Height>15</Height><DCIdx>14</DCIdx></C1DisplayColumn><" +
                "C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style136\" /><Style parent=\"Sty" +
                "le1\" me=\"Style137\" /><FooterStyle parent=\"Style3\" me=\"Style138\" /><EditorStyle p" +
                "arent=\"Style5\" me=\"Style139\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style141\" /" +
                "><GroupFooterStyle parent=\"Style1\" me=\"Style140\" /><ColumnDivider>Gainsboro,None" +
                "</ColumnDivider><Width>18</Width><Height>15</Height><DCIdx>19</DCIdx></C1Display" +
                "Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style142\" /><Style par" +
                "ent=\"Style1\" me=\"Style143\" /><FooterStyle parent=\"Style3\" me=\"Style144\" /><Edito" +
                "rStyle parent=\"Style5\" me=\"Style145\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
                "le147\" /><GroupFooterStyle parent=\"Style1\" me=\"Style146\" /><ColumnDivider>Gainsb" +
                "oro,None</ColumnDivider><Width>18</Width><Height>15</Height><DCIdx>20</DCIdx></C" +
                "1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style148\" /><S" +
                "tyle parent=\"Style1\" me=\"Style149\" /><FooterStyle parent=\"Style3\" me=\"Style150\" " +
                "/><EditorStyle parent=\"Style5\" me=\"Style151\" /><GroupHeaderStyle parent=\"Style1\"" +
                " me=\"Style153\" /><GroupFooterStyle parent=\"Style1\" me=\"Style152\" /><ColumnDivide" +
                "r>Gainsboro,None</ColumnDivider><Width>18</Width><Height>15</Height><DCIdx>21</D" +
                "CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style1" +
                "54\" /><Style parent=\"Style1\" me=\"Style155\" /><FooterStyle parent=\"Style3\" me=\"St" +
                "yle156\" /><EditorStyle parent=\"Style5\" me=\"Style157\" /><GroupHeaderStyle parent=" +
                "\"Style1\" me=\"Style159\" /><GroupFooterStyle parent=\"Style1\" me=\"Style158\" /><Colu" +
                "mnDivider>Gainsboro,None</ColumnDivider><Width>18</Width><Height>15</Height><DCI" +
                "dx>22</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
                "=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" " +
                "me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle par" +
                "ent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Vi" +
                "sible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>90</Wi" +
                "dth><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
                "ngStyle parent=\"Style2\" me=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><Fo" +
                "oterStyle parent=\"Style3\" me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style9" +
                "1\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"" +
                "Style1\" me=\"Style92\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</C" +
                "olumnDivider><Width>90</Width><Height>15</Height><DCIdx>12</DCIdx></C1DisplayCol" +
                "umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style232\" /><Style parent" +
                "=\"Style1\" me=\"Style233\" /><FooterStyle parent=\"Style3\" me=\"Style234\" /><EditorSt" +
                "yle parent=\"Style5\" me=\"Style235\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style2" +
                "37\" /><GroupFooterStyle parent=\"Style1\" me=\"Style236\" /><Visible>True</Visible><" +
                "ColumnDivider>Gainsboro,Single</ColumnDivider><Width>25</Width><Height>15</Heigh" +
                "t><DCIdx>35</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
                "e2\" me=\"Style166\" /><Style parent=\"Style1\" me=\"Style167\" /><FooterStyle parent=\"" +
                "Style3\" me=\"Style168\" /><EditorStyle parent=\"Style5\" me=\"Style169\" /><GroupHeade" +
                "rStyle parent=\"Style1\" me=\"Style171\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
                "le170\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><" +
                "Width>15</Width><Height>15</Height><DCIdx>24</DCIdx></C1DisplayColumn><C1Display" +
                "Column><HeadingStyle parent=\"Style2\" me=\"Style184\" /><Style parent=\"Style1\" me=\"" +
                "Style185\" /><FooterStyle parent=\"Style3\" me=\"Style186\" /><EditorStyle parent=\"St" +
                "yle5\" me=\"Style187\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style189\" /><GroupFo" +
                "oterStyle parent=\"Style1\" me=\"Style188\" /><Visible>True</Visible><ColumnDivider>" +
                "Gainsboro,Single</ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>27</D" +
                "CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style1" +
                "90\" /><Style parent=\"Style1\" me=\"Style191\" /><FooterStyle parent=\"Style3\" me=\"St" +
                "yle192\" /><EditorStyle parent=\"Style5\" me=\"Style193\" /><GroupHeaderStyle parent=" +
                "\"Style1\" me=\"Style195\" /><GroupFooterStyle parent=\"Style1\" me=\"Style194\" /><Visi" +
                "ble>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>15</Widt" +
                "h><Height>15</Height><DCIdx>28</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
                "gStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><Foo" +
                "terStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49" +
                "\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"S" +
                "tyle1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Co" +
                "lumnDivider><Width>95</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColum" +
                "n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style220\" /><Style parent=\"" +
                "Style1\" me=\"Style221\" /><FooterStyle parent=\"Style3\" me=\"Style222\" /><EditorStyl" +
                "e parent=\"Style5\" me=\"Style223\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style225" +
                "\" /><GroupFooterStyle parent=\"Style1\" me=\"Style224\" /><Visible>True</Visible><Co" +
                "lumnDivider>Gainsboro,Single</ColumnDivider><Width>35</Width><Height>15</Height>" +
                "<DCIdx>33</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
                "\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Styl" +
                "e3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle" +
                " parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /" +
                "><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>15" +
                "</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
                "eadingStyle parent=\"Style2\" me=\"Style172\" /><Style parent=\"Style1\" me=\"Style173\"" +
                " /><FooterStyle parent=\"Style3\" me=\"Style174\" /><EditorStyle parent=\"Style5\" me=" +
                "\"Style175\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style177\" /><GroupFooterStyle" +
                " parent=\"Style1\" me=\"Style176\" /><Visible>True</Visible><ColumnDivider>Gainsboro" +
                ",Single</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>25</DCIdx></C1" +
                "DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style226\" /><St" +
                "yle parent=\"Style1\" me=\"Style227\" /><FooterStyle parent=\"Style3\" me=\"Style228\" /" +
                "><EditorStyle parent=\"Style5\" me=\"Style229\" /><GroupHeaderStyle parent=\"Style1\" " +
                "me=\"Style231\" /><GroupFooterStyle parent=\"Style1\" me=\"Style230\" /><Visible>True<" +
                "/Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>25</Width><Height" +
                ">15</Height><DCIdx>34</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
                "rent=\"Style2\" me=\"Style178\" /><Style parent=\"Style1\" me=\"Style179\" /><FooterStyl" +
                "e parent=\"Style3\" me=\"Style180\" /><EditorStyle parent=\"Style5\" me=\"Style181\" /><" +
                "GroupHeaderStyle parent=\"Style1\" me=\"Style183\" /><GroupFooterStyle parent=\"Style" +
                "1\" me=\"Style182\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Colum" +
                "nDivider><Width>15</Width><Height>15</Height><DCIdx>26</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style196\" /><Style parent=\"St" +
                "yle1\" me=\"Style197\" /><FooterStyle parent=\"Style3\" me=\"Style198\" /><EditorStyle " +
                "parent=\"Style5\" me=\"Style199\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style201\" " +
                "/><GroupFooterStyle parent=\"Style1\" me=\"Style200\" /><Visible>True</Visible><Colu" +
                "mnDivider>Gainsboro,Single</ColumnDivider><Width>80</Width><Height>15</Height><D" +
                "CIdx>29</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
                "me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3" +
                "\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle p" +
                "arent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><" +
                "Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>58</" +
                "Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
                "dingStyle parent=\"Style2\" me=\"Style106\" /><Style parent=\"Style1\" me=\"Style107\" /" +
                "><FooterStyle parent=\"Style3\" me=\"Style108\" /><EditorStyle parent=\"Style5\" me=\"S" +
                "tyle109\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style111\" /><GroupFooterStyle p" +
                "arent=\"Style1\" me=\"Style110\" /><Visible>True</Visible><ColumnDivider>Gainsboro,S" +
                "ingle</ColumnDivider><Width>58</Width><Height>15</Height><DCIdx>15</DCIdx></C1Di" +
                "splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style" +
                " parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><Edi" +
                "torStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
                "yle63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible" +
                "><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>15</Width><Height>15</Hei" +
                "ght><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
                "le2\" me=\"Style112\" /><Style parent=\"Style1\" me=\"Style113\" /><FooterStyle parent=" +
                "\"Style3\" me=\"Style114\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><GroupHead" +
                "erStyle parent=\"Style1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
                "yle116\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider>" +
                "<Width>35</Width><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1Displa" +
                "yColumn><HeadingStyle parent=\"Style2\" me=\"Style238\" /><Style parent=\"Style1\" me=" +
                "\"Style239\" /><FooterStyle parent=\"Style3\" me=\"Style240\" /><EditorStyle parent=\"S" +
                "tyle5\" me=\"Style241\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style243\" /><GroupF" +
                "ooterStyle parent=\"Style1\" me=\"Style242\" /><Visible>True</Visible><ColumnDivider" +
                ">Gainsboro,Single</ColumnDivider><Width>15</Width><Height>15</Height><DCIdx>36</" +
                "DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
                "244\" /><Style parent=\"Style1\" me=\"Style245\" /><FooterStyle parent=\"Style3\" me=\"S" +
                "tyle246\" /><EditorStyle parent=\"Style5\" me=\"Style247\" /><GroupHeaderStyle parent" +
                "=\"Style1\" me=\"Style249\" /><GroupFooterStyle parent=\"Style1\" me=\"Style248\" /><Vis" +
                "ible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>75</Wid" +
                "th><Height>15</Height><DCIdx>37</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
                "ngStyle parent=\"Style2\" me=\"Style118\" /><Style parent=\"Style1\" me=\"Style119\" /><" +
                "FooterStyle parent=\"Style3\" me=\"Style120\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
                "le121\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style123\" /><GroupFooterStyle par" +
                "ent=\"Style1\" me=\"Style122\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Sin" +
                "gle</ColumnDivider><Width>20</Width><Height>15</Height><DCIdx>17</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style208\" /><Style " +
                "parent=\"Style1\" me=\"Style209\" /><FooterStyle parent=\"Style3\" me=\"Style210\" /><Ed" +
                "itorStyle parent=\"Style5\" me=\"Style211\" /><GroupHeaderStyle parent=\"Style1\" me=\"" +
                "Style213\" /><GroupFooterStyle parent=\"Style1\" me=\"Style212\" /><Visible>True</Vis" +
                "ible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>45</Width><Height>15<" +
                "/Height><DCIdx>31</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
                "=\"Style2\" me=\"Style214\" /><Style parent=\"Style1\" me=\"Style215\" /><FooterStyle pa" +
                "rent=\"Style3\" me=\"Style216\" /><EditorStyle parent=\"Style5\" me=\"Style217\" /><Grou" +
                "pHeaderStyle parent=\"Style1\" me=\"Style219\" /><GroupFooterStyle parent=\"Style1\" m" +
                "e=\"Style218\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDiv" +
                "ider><Width>15</Width><Height>15</Height><DCIdx>32</DCIdx></C1DisplayColumn><C1D" +
                "isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style250\" /><Style parent=\"Style1" +
                "\" me=\"Style251\" /><FooterStyle parent=\"Style3\" me=\"Style252\" /><EditorStyle pare" +
                "nt=\"Style5\" me=\"Style253\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style255\" /><G" +
                "roupFooterStyle parent=\"Style1\" me=\"Style254\" /><Visible>True</Visible><ColumnDi" +
                "vider>Gainsboro,Single</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx" +
                ">38</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
                "Style256\" /><Style parent=\"Style1\" me=\"Style257\" /><FooterStyle parent=\"Style3\" " +
                "me=\"Style258\" /><EditorStyle parent=\"Style5\" me=\"Style259\" /><GroupHeaderStyle p" +
                "arent=\"Style1\" me=\"Style261\" /><GroupFooterStyle parent=\"Style1\" me=\"Style260\" /" +
                "><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>45" +
                "</Width><Height>15</Height><DCIdx>39</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
                "HeadingStyle parent=\"Style2\" me=\"Style262\" /><Style parent=\"Style1\" me=\"Style263" +
                "\" /><FooterStyle parent=\"Style3\" me=\"Style264\" /><EditorStyle parent=\"Style5\" me" +
                "=\"Style265\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style267\" /><GroupFooterStyl" +
                "e parent=\"Style1\" me=\"Style266\" /><Visible>True</Visible><ColumnDivider>Gainsbor" +
                "o,Single</ColumnDivider><Width>15</Width><Height>15</Height><DCIdx>40</DCIdx></C" +
                "1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style76\" /><St" +
                "yle parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /><" +
                "EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" me=" +
                "\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><Visible>True</Visi" +
                "ble><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>80</Width><Height>15</" +
                "Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
                "\"Style2\" me=\"Style202\" /><Style parent=\"Style1\" me=\"Style203\" /><FooterStyle par" +
                "ent=\"Style3\" me=\"Style204\" /><EditorStyle parent=\"Style5\" me=\"Style205\" /><Group" +
                "HeaderStyle parent=\"Style1\" me=\"Style207\" /><GroupFooterStyle parent=\"Style1\" me" +
                "=\"Style206\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivi" +
                "der><Width>80</Width><Height>15</Height><DCIdx>30</DCIdx></C1DisplayColumn><C1Di" +
                "splayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" " +
                "me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"" +
                "Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFo" +
                "oterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>G" +
                "ainsboro,Single</ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>11</DC" +
                "Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12" +
                "4\" /><Style parent=\"Style1\" me=\"Style125\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
                "le126\" /><EditorStyle parent=\"Style5\" me=\"Style127\" /><GroupHeaderStyle parent=\"" +
                "Style1\" me=\"Style129\" /><GroupFooterStyle parent=\"Style1\" me=\"Style128\" /><Visib" +
                "le>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>175</Widt" +
                "h><Height>15</Height><DCIdx>18</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
                "gStyle parent=\"Style2\" me=\"Style526\" /><Style parent=\"Style1\" me=\"Style527\" /><F" +
                "ooterStyle parent=\"Style3\" me=\"Style528\" /><EditorStyle parent=\"Style5\" me=\"Styl" +
                "e529\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style531\" /><GroupFooterStyle pare" +
                "nt=\"Style1\" me=\"Style530\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Heigh" +
                "t>15</Height><DCIdx>41</DCIdx></C1DisplayColumn></internalCols><ClientRect>553, " +
                "0, 1047, 96</ClientRect><BorderSide>Left</BorderSide></C1.Win.C1TrueDBGrid.Merge" +
                "View></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\"" +
                " me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me" +
                "=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"" +
                "Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"High" +
                "lightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"Odd" +
                "Row\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"" +
                "FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</v" +
                "ertSplits><horzSplits>2</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth" +
                ">16</DefaultRecSelWidth><ClientArea>0, 0, 1600, 96</ClientArea><PrintPageHeaderS" +
                "tyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></B" +
                "lob>";
            // 
            // ContractsSplitter
            // 
            this.ContractsSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ContractsSplitter.Location = new System.Drawing.Point(0, 100);
            this.ContractsSplitter.MinExtra = 20;
            this.ContractsSplitter.MinSize = 20;
            this.ContractsSplitter.Name = "ContractsSplitter";
            this.ContractsSplitter.Size = new System.Drawing.Size(1604, 4);
            this.ContractsSplitter.TabIndex = 1;
            this.ContractsSplitter.TabStop = false;
            // 
            // LoansGrid
            // 
            this.LoansGrid.AllowColSelect = false;
            this.LoansGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.LoansGrid.AllowUpdate = false;
            this.LoansGrid.CaptionHeight = 17;
            this.LoansGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LoansGrid.EmptyRows = true;
            this.LoansGrid.FetchRowStyles = true;
            this.LoansGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.LoansGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.LoansGrid.Location = new System.Drawing.Point(0, 104);
            this.LoansGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.LoansGrid.Name = "LoansGrid";
            this.LoansGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.LoansGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.LoansGrid.PreviewInfo.ZoomFactor = 75;
            this.LoansGrid.RecordSelectorWidth = 16;
            this.LoansGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.LoansGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.LoansGrid.RowHeight = 15;
            this.LoansGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.LoansGrid.Size = new System.Drawing.Size(1604, 100);
            this.LoansGrid.TabIndex = 0;
            this.LoansGrid.Visible = false;
            this.LoansGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LoansGrid_Paint);
            this.LoansGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.LoansGrid_AfterFilter);
            this.LoansGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.LoansGrid_FetchRowStyle);
            this.LoansGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ContractsGrid_FormatText);
            this.LoansGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Parent\" Dat" +
                "aField=\"BookParent\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
                "l=\"0\" Caption=\"Book\" DataField=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn>" +
                "<C1DataColumn Level=\"0\" Caption=\"Security\" DataField=\"SecId\"><ValueItems /><Grou" +
                "pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbo" +
                "l\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Qu" +
                "antity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><ValueItems /><Grou" +
                "pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Amount\" DataField=\"Amoun" +
                "t\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
                "DataColumn Level=\"0\" Caption=\"\" DataField=\"CollateralCode\"><ValueItems /><GroupI" +
                "nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"RateCode\"><Va" +
                "lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PC\" Data" +
                "Field=\"PoolCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
                "0\" Caption=\"Rate\" DataField=\"FeeRate\" NumberFormat=\"FormatText Event\"><ValueItem" +
                "s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Settle Date\" Da" +
                "taField=\"SettleDate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo />" +
                "</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Contract ID\" DataField=\"Contract" +
                "Id\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S" +
                "ettled\" DataField=\"QuantitySettled\" NumberFormat=\"FormatText Event\"><ValueItems " +
                "/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" DataField=\"Bas" +
                "eType\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
                "=\"Class\" DataField=\"ClassGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
                "aColumn Level=\"0\" Caption=\"Rate\" DataField=\"RebateRate\" NumberFormat=\"FormatText" +
                " Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
                "n=\"M%\" DataField=\"Margin\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
                "fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"IT\" DataField=\"IncomeTracke" +
                "d\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColu" +
                "mn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><GroupInfo /></" +
                "C1DataColumn><C1DataColumn Level=\"0\" Caption=\"E\" DataField=\"IsEasy\"><ValueItems " +
                "Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
                "ption=\"H\" DataField=\"IsHard\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo />" +
                "</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"N\" DataField=\"IsNoLend\"><ValueIt" +
                "ems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
                "\" Caption=\"T\" DataField=\"IsThreshold\"><ValueItems Presentation=\"CheckBox\" /><Gro" +
                "upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Book Group\" DataField=\"" +
                "BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
                "tion=\"S\" DataField=\"IsSettledQuantity\"><ValueItems Presentation=\"CheckBox\" /><Gr" +
                "oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Settled\" DataField=\"Am" +
                "ountSettled\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Data" +
                "Column><C1DataColumn Level=\"0\" Caption=\"S\" DataField=\"IsSettledAmount\"><ValueIte" +
                "ms Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
                " Caption=\"Recalled\" DataField=\"QuantityRecalled\" NumberFormat=\"FormatText Event\"" +
                "><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"R\" D" +
                "ataField=\"HasRecall\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1Data" +
                "Column><C1DataColumn Level=\"0\" Caption=\"Value Date\" DataField=\"ValueDate\" Number" +
                "Format=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColum" +
                "n Level=\"0\" Caption=\"Term Date\" DataField=\"TermDate\" NumberFormat=\"FormatText Ev" +
                "ent\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
                "Div%\" DataField=\"DivRate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
                "fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"C\" DataField=\"DivCallable\">" +
                "<ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn " +
                "Level=\"0\" Caption=\"\" DataField=\"CurrencyIso\"><ValueItems /><GroupInfo /></C1Data" +
                "Column><C1DataColumn Level=\"0\" Caption=\"CD\" DataField=\"CashDepot\"><ValueItems />" +
                "<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"SD\" DataField=\"Secu" +
                "rityDepot\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
                "tion=\"\" DataField=\"MarginCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
                "Column Level=\"0\" Caption=\"Income\" DataField=\"Income\" NumberFormat=\"FormatText Ev" +
                "ent\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
                "Value\" DataField=\"Value\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInf" +
                "o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ratio\" DataField=\"ValueRatio" +
                "\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1D" +
                "ataColumn Level=\"0\" Caption=\"E\" DataField=\"ValueIsEstimate\"><ValueItems Presenta" +
                "tion=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S" +
                "tatusFlag\" DataField=\"StatusFlag\"><ValueItems /><GroupInfo /></C1DataColumn></Da" +
                "taCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style270{}" +
                "Style107{AlignHorz:Far;BackColor:Honeydew;}Style287{AlignHorz:Near;BackColor:Hon" +
                "eydew;}Style159{}Style151{}Style150{}Style153{}Style152{}Style155{AlignHorz:Cent" +
                "er;BackColor:Honeydew;}Style154{AlignHorz:Center;}Style361{}Group{AlignVert:Cent" +
                "er;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style148{AlignHorz:Center;}Sty" +
                "le418{AlignHorz:Near;}Style142{AlignHorz:Center;}Style143{AlignHorz:Center;BackC" +
                "olor:Honeydew;}Style144{}Style145{}Style146{}Style147{}Style393{}Style121{}Style" +
                "391{}Style390{}Style399{}Style296{}Style297{}Style294{}Style295{}Style292{AlignH" +
                "orz:Near;}Style293{AlignHorz:Near;BackColor:Honeydew;}Style290{}Style291{}Style5" +
                "15{AlignHorz:Far;BackColor:Honeydew;}Style514{AlignHorz:Near;}Style298{AlignHorz" +
                ":Near;}Style299{AlignHorz:Near;BackColor:Honeydew;}Style171{}Style170{}Style177{" +
                "}Style342{}Style343{}Style193{}Style192{}Style346{AlignHorz:Center;}Style347{Ali" +
                "gnHorz:Center;BackColor:Honeydew;}Style199{}Style198{}Style508{AlignHorz:Near;}S" +
                "tyle509{AlignHorz:Far;BackColor:Honeydew;}Style388{AlignHorz:Center;}Style389{Al" +
                "ignHorz:Near;BackColor:Honeydew;}Style502{AlignHorz:Near;}Style503{AlignHorz:Nea" +
                "r;BackColor:Honeydew;}Style504{}Style505{}Style162{}Style185{AlignHorz:Far;BackC" +
                "olor:Honeydew;}Style379{}Style378{}Style166{AlignHorz:Near;}Style181{}Style334{A" +
                "lignHorz:Center;}Style337{}Style371{AlignHorz:Far;BackColor:Honeydew;}Style370{A" +
                "lignHorz:Near;}Style373{}Style372{}Style375{}Style374{}Style377{AlignHorz:Center" +
                ";BackColor:Honeydew;}Style376{AlignHorz:Center;}Style279{}Editor{}Style271{}Styl" +
                "e272{}Style273{}Style274{}Style275{}Style276{}Style277{AlignHorz:Near;}Style368{" +
                "}Style369{}Style110{}Style113{AlignHorz:Far;BackColor:Honeydew;}Style112{AlignHo" +
                "rz:Center;}Style360{}Style321{}Style362{}Style363{}Style364{AlignHorz:Center;}St" +
                "yle365{AlignHorz:Center;BackColor:Honeydew;}Style366{}Style367{}Style286{AlignHo" +
                "rz:Near;}Style285{}Style284{}Style283{}Style282{}Style281{AlignHorz:Near;BackCol" +
                "or:Honeydew;}Style280{AlignHorz:Near;}Style289{}Style288{}Style359{AlignHorz:Far" +
                ";BackColor:Honeydew;}Style358{AlignHorz:Near;}Style318{}Style353{AlignHorz:Far;B" +
                "ackColor:Honeydew;}Style352{AlignHorz:Near;}Style351{}Style350{}Style357{}Style3" +
                "56{}Style355{}Style354{}Style258{}Style259{}Selected{ForeColor:HighlightText;Bac" +
                "kColor:Highlight;}Style252{}Style253{}Style250{AlignHorz:Near;}Style251{AlignHor" +
                "z:Far;BackColor:Honeydew;}Style256{AlignHorz:Near;}Style257{AlignHorz:Far;BackCo" +
                "lor:Honeydew;}Style254{}Style255{}Style158{}Style348{}Style349{}Style308{}Style3" +
                "09{}Style306{}Style307{}Style420{}Style427{}Style340{AlignHorz:Center;}Style341{" +
                "AlignHorz:Center;BackColor:Honeydew;}Style157{}Style156{}Style344{}Style345{}Sty" +
                "le268{Font:Verdana, 8.25pt;}Style261{}Style260{}Style263{AlignHorz:Center;BackCo" +
                "lor:Honeydew;}Style262{AlignHorz:Near;}Style265{}Style264{}Style267{}Style266{}S" +
                "tyle149{AlignHorz:Center;BackColor:Honeydew;}Style129{}Style339{}Style338{}Style" +
                "140{}Style141{}Style335{AlignHorz:Center;BackColor:Honeydew;}Style481{}Style480{" +
                "}Style336{}Style331{}Style330{}Style333{}Style332{}Style486{}Style489{}Style488{" +
                "}Style238{AlignHorz:Near;}Style239{AlignHorz:Near;BackColor:Honeydew;}Style234{}" +
                "Style235{}Style236{}Style237{}Style230{}Style231{}Style232{AlignHorz:Near;}Style" +
                "233{AlignHorz:Center;BackColor:Honeydew;}Style179{AlignHorz:Center;BackColor:Hon" +
                "eydew;}Style178{AlignHorz:Near;}Style328{AlignHorz:Center;}Style329{AlignHorz:Ce" +
                "nter;BackColor:Honeydew;}Style173{AlignHorz:Far;BackColor:Honeydew;}Style172{Ali" +
                "gnHorz:Near;}Style324{}Style325{}Style326{}Style327{}Style320{}Style174{}Style32" +
                "2{AlignHorz:Near;}Style323{AlignHorz:Near;BackColor:Honeydew;}Style249{}Style248" +
                "{}Style243{}Style242{}Style241{}Style240{}Style247{}Style246{}Style245{AlignHorz" +
                ":Far;BackColor:Honeydew;}Style244{AlignHorz:Near;}Style168{}Style169{}Style319{}" +
                "Style163{}Style317{AlignHorz:Center;BackColor:Honeydew;}Style316{AlignHorz:Cente" +
                "r;}Style315{}Style314{}Style313{}Style165{}Style311{AlignHorz:Center;BackColor:H" +
                "oneydew;}Style310{AlignHorz:Center;}Style218{}Style219{}Style216{}Style217{}Styl" +
                "e214{AlignHorz:Near;}Style215{AlignHorz:Center;BackColor:Honeydew;}Style212{}Sty" +
                "le213{}Style210{}Style211{}Style119{AlignHorz:Center;BackColor:Honeydew;}Style11" +
                "8{AlignHorz:Near;}Style115{}Style114{}Style117{}Style116{}Style111{}Style305{Ali" +
                "gnHorz:Near;BackColor:Honeydew;}Style302{}Style303{}Style300{}Style301{}Style229" +
                "{}Style228{}Style225{}Style224{}Style227{AlignHorz:Center;BackColor:Honeydew;}St" +
                "yle226{AlignHorz:Near;}Style221{AlignHorz:Center;BackColor:Honeydew;}Style220{Al" +
                "ignHorz:Center;}Style223{}Style222{}Style108{}Style109{}Style104{}Style105{}Styl" +
                "e106{AlignHorz:Near;}Normal{Font:Verdana, 8.25pt;}Style100{AlignHorz:Near;}Style" +
                "101{AlignHorz:Near;BackColor:Honeydew;}Style102{}Style103{}Style519{}Style518{}S" +
                "tyle511{}Style510{}Style139{}Style138{}Style137{AlignHorz:Center;BackColor:Honey" +
                "dew;}Style136{AlignHorz:Center;}Style517{}Style516{}Style419{AlignHorz:Far;BackC" +
                "olor:Honeydew;}Style132{}Style131{AlignHorz:Near;}Style130{AlignHorz:Near;}Style" +
                "410{}Style411{}Style412{AlignHorz:Near;}Style413{AlignHorz:Far;BackColor:Honeyde" +
                "w;}Style209{AlignHorz:Far;BackColor:Honeydew;}Style208{AlignHorz:Near;}Style207{" +
                "}Style206{}Style205{}Style204{}Style203{AlignHorz:Center;BackColor:Honeydew;}Sty" +
                "le202{AlignHorz:Near;}Style201{}Style200{}Style500{}Style501{}Style128{}Inactive" +
                "{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style126{}Style127{}St" +
                "yle506{}Style507{}Style122{}Style123{}Style120{}Style83{AlignHorz:Far;BackColor:" +
                "Honeydew;}Style82{AlignHorz:Near;}Style89{AlignHorz:Far;BackColor:Honeydew;}Styl" +
                "e88{AlignHorz:Near;}Style85{}Style84{}Style87{}Style86{}Style81{}Style80{}Style5" +
                "32{}Style531{}Style530{}Style94{AlignHorz:Center;}Style95{AlignHorz:Center;BackC" +
                "olor:Honeydew;}Style96{}Style97{}Style438{}Style439{}Style92{}Style93{}Style432{" +
                "}Style433{}Style430{AlignHorz:Center;}Style99{}Style436{AlignHorz:Near;}Style437" +
                "{AlignHorz:Near;BackColor:Honeydew;}Style434{}Style435{}Style528{}Style529{}Styl" +
                "e90{}Style91{}Style522{}Style523{}Style520{AlignHorz:Center;}Style521{AlignHorz:" +
                "Center;BackColor:Honeydew;}Style526{AlignHorz:Near;}Style527{AlignHorz:Near;}Sty" +
                "le524{}Style409{}Style408{}Style401{AlignHorz:Center;BackColor:Honeydew;}Style40" +
                "0{AlignHorz:Center;}Style403{}Style402{}Style405{}Style404{}Style407{AlignHorz:C" +
                "enter;BackColor:Honeydew;}Style406{AlignHorz:Center;}Style98{}Style458{}Style459" +
                "{}Style454{AlignHorz:Near;}Style455{AlignHorz:Center;BackColor:Honeydew;}Style45" +
                "6{}Style457{}Style450{}Style451{}Style452{}Style453{}Style49{}Style48{}Style429{" +
                "}Style428{}Style41{AlignHorz:Far;BackColor:Honeydew;}Style40{AlignHorz:Near;}Sty" +
                "le423{}Style42{}Style45{}Style44{}Style47{AlignHorz:Far;BackColor:Honeydew;}Styl" +
                "e46{AlignHorz:Near;}Style425{AlignHorz:Near;BackColor:Honeydew;}Style424{AlignHo" +
                "rz:Near;}Style58{AlignHorz:Near;}Style59{AlignHorz:Near;BackColor:Honeydew;}Styl" +
                "e50{}Style51{}Style52{AlignHorz:Center;}Style53{AlignHorz:Near;BackColor:Honeyde" +
                "w;}Style54{}Style55{}Style472{AlignHorz:Center;}Style57{}Style470{}Style471{}Sty" +
                "le43{}Style69{}Style68{}Style56{}Style449{AlignHorz:Center;BackColor:Honeydew;}S" +
                "tyle448{AlignHorz:Near;}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}St" +
                "yle65{AlignHorz:Center;BackColor:Honeydew;}Style64{AlignHorz:Center;}Style443{Al" +
                "ignHorz:Center;BackColor:Honeydew;}Style442{AlignHorz:Center;}Style78{}Style79{}" +
                "Style72{}Style73{}Style70{AlignHorz:Near;}Style71{AlignHorz:Far;BackColor:Honeyd" +
                "ew;}Style76{AlignHorz:Near;}Style77{AlignHorz:Center;BackColor:Honeydew;}Style74" +
                "{}Style75{}Style469{}Style468{}Style467{AlignHorz:Far;BackColor:Honeydew;}Style4" +
                "66{AlignHorz:Near;}Style465{}Style464{}Style463{}Style462{}Style461{AlignHorz:Ce" +
                "nter;BackColor:Honeydew;}Style460{AlignHorz:Near;}Style278{}Style18{}Style19{}St" +
                "yle14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;BackColor:Honeyde" +
                "w;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Footer{}Style398{}Style125" +
                "{AlignHorz:Near;BackColor:Honeydew;}Style29{AlignHorz:Near;BackColor:Honeydew;}S" +
                "tyle28{AlignHorz:Near;}Style27{}Style26{}Style25{}Style24{}Style23{AlignHorz:Nea" +
                "r;BackColor:Honeydew;}Style22{AlignHorz:Near;}Style21{}Style20{}Style175{}Style4" +
                "14{}Style415{}Style416{}Style417{}Style38{}Style39{}Style36{}Style37{}Style34{Al" +
                "ignHorz:Near;}Style35{AlignHorz:Near;BackColor:Honeydew;}Style32{}Style33{}Style" +
                "30{}Style31{}Style183{}Style188{}Style189{}RecordSelector{AlignImage:Center;}Sty" +
                "le513{}Style512{}Style9{}Style8{}Style5{}Style4{}Style7{}Style6{}Style1{Font:Ver" +
                "dana, 8.25pt;}Style3{}Style2{Font:Verdana, 8.25pt;}Style431{AlignHorz:Far;BackCo" +
                "lor:Honeydew;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style396" +
                "{}Style394{AlignHorz:Near;}Style490{AlignHorz:Center;}Style491{AlignHorz:Center;" +
                "BackColor:Honeydew;}Style492{}Style493{}Style494{}Style495{}Style496{AlignHorz:C" +
                "enter;}Style497{AlignHorz:Center;BackColor:Honeydew;}Style498{}Style499{}Style13" +
                "5{}Style134{}Style133{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, " +
                "1, 1;ForeColor:ControlText;AlignVert:Center;}FilterBar{AlignVert:Center;}Style52" +
                "5{}OddRow{}Style422{}Style421{}Style124{AlignHorz:Near;}Caption{AlignHorz:Center" +
                ";}Style426{}Style487{}Style195{}Style194{}Style197{AlignHorz:Center;BackColor:Ho" +
                "neydew;}Style196{AlignHorz:Near;}Style191{AlignHorz:Center;BackColor:Honeydew;}S" +
                "tyle190{AlignHorz:Near;}Style483{}Style482{}Style485{AlignHorz:Far;BackColor:Hon" +
                "eydew;}Style484{AlignHorz:Near;}Style176{}Style479{AlignHorz:Near;BackColor:Hone" +
                "ydew;}Style476{}Style477{}Style474{}Style475{}Style269{}Style473{AlignHorz:Cente" +
                "r;BackColor:Honeydew;}Style478{AlignHorz:Near;}Style184{AlignHorz:Near;}Style186" +
                "{}Style187{}Style180{}Style397{}Style182{}Style395{AlignHorz:Far;BackColor:Honey" +
                "dew;}Style160{AlignHorz:Near;}Style161{AlignHorz:Near;BackColor:Honeydew;}Style3" +
                "92{}Style167{AlignHorz:Center;BackColor:Honeydew;}Style164{}Style445{}Style444{}" +
                "Style447{}Style446{}Style441{}Style440{}Style312{}EvenRow{BackColor:Aqua;}Style3" +
                "86{}Style387{}Style384{}Style385{}Style382{AlignHorz:Near;}Style383{AlignHorz:Fa" +
                "r;BackColor:Honeydew;}Style380{}Style381{}Style304{AlignHorz:Near;}</Data></Styl" +
                "es><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"None\" Allo" +
                "wColSelect=\"False\" Name=\"Split[0,0]\" AllowRowSizing=\"None\" AllowVerticalSizing=\"" +
                "False\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"20\" Fetch" +
                "RowStyles=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecS" +
                "elWidth=\"16\" VerticalScrollGroup=\"1\" MinWidth=\"100\" HorizontalScrollGroup=\"2\"><C" +
                "aption>Loans</Caption><SplitSize>12</SplitSize><SplitSizeMode>NumberOfColumns</S" +
                "plitSizeMode><CaptionStyle parent=\"Style2\" me=\"Style277\" /><EditorStyle parent=\"" +
                "Editor\" me=\"Style269\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style275\" /><FilterBa" +
                "rStyle parent=\"FilterBar\" me=\"Style532\" /><FooterStyle parent=\"Footer\" me=\"Style" +
                "271\" /><GroupStyle parent=\"Group\" me=\"Style279\" /><HeadingStyle parent=\"Heading\"" +
                " me=\"Style270\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style274\" /><Inact" +
                "iveStyle parent=\"Inactive\" me=\"Style273\" /><OddRowStyle parent=\"OddRow\" me=\"Styl" +
                "e276\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style278\" /><SelectedSt" +
                "yle parent=\"Selected\" me=\"Style272\" /><Style parent=\"Normal\" me=\"Style268\" /><in" +
                "ternalCols><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style280\" /><Sty" +
                "le parent=\"Style268\" me=\"Style281\" /><FooterStyle parent=\"Style271\" me=\"Style282" +
                "\" /><EditorStyle parent=\"Style269\" me=\"Style283\" /><GroupHeaderStyle parent=\"Sty" +
                "le268\" me=\"Style285\" /><GroupFooterStyle parent=\"Style268\" me=\"Style284\" /><Visi" +
                "ble>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Widt" +
                "h><Height>15</Height><DCIdx>23</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
                "gStyle parent=\"Style270\" me=\"Style286\" /><Style parent=\"Style268\" me=\"Style287\" " +
                "/><FooterStyle parent=\"Style271\" me=\"Style288\" /><EditorStyle parent=\"Style269\" " +
                "me=\"Style289\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style291\" /><GroupFooter" +
                "Style parent=\"Style268\" me=\"Style290\" /><Visible>True</Visible><ColumnDivider>Ga" +
                "insboro,Single</ColumnDivider><Width>60</Width><Height>15</Height><DCIdx>0</DCId" +
                "x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style29" +
                "2\" /><Style parent=\"Style268\" me=\"Style293\" /><FooterStyle parent=\"Style271\" me=" +
                "\"Style294\" /><EditorStyle parent=\"Style269\" me=\"Style295\" /><GroupHeaderStyle pa" +
                "rent=\"Style268\" me=\"Style297\" /><GroupFooterStyle parent=\"Style268\" me=\"Style296" +
                "\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width" +
                ">60</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn" +
                "><HeadingStyle parent=\"Style270\" me=\"Style298\" /><Style parent=\"Style268\" me=\"St" +
                "yle299\" /><FooterStyle parent=\"Style271\" me=\"Style300\" /><EditorStyle parent=\"St" +
                "yle269\" me=\"Style301\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style303\" /><Gro" +
                "upFooterStyle parent=\"Style268\" me=\"Style302\" /><Visible>True</Visible><ColumnDi" +
                "vider>Gainsboro,Single</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx" +
                ">2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=" +
                "\"Style304\" /><Style parent=\"Style268\" me=\"Style305\" /><FooterStyle parent=\"Style" +
                "271\" me=\"Style306\" /><EditorStyle parent=\"Style269\" me=\"Style307\" /><GroupHeader" +
                "Style parent=\"Style268\" me=\"Style309\" /><GroupFooterStyle parent=\"Style268\" me=\"" +
                "Style308\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivide" +
                "r><Width>75</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1Displ" +
                "ayColumn><HeadingStyle parent=\"Style270\" me=\"Style310\" /><Style parent=\"Style268" +
                "\" me=\"Style311\" /><FooterStyle parent=\"Style271\" me=\"Style312\" /><EditorStyle pa" +
                "rent=\"Style269\" me=\"Style313\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style315" +
                "\" /><GroupFooterStyle parent=\"Style268\" me=\"Style314\" /><Visible>True</Visible><" +
                "ColumnDivider>Gainsboro,Single</ColumnDivider><Width>25</Width><Height>15</Heigh" +
                "t><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
                "270\" me=\"Style316\" /><Style parent=\"Style268\" me=\"Style317\" /><FooterStyle paren" +
                "t=\"Style271\" me=\"Style318\" /><EditorStyle parent=\"Style269\" me=\"Style319\" /><Gro" +
                "upHeaderStyle parent=\"Style268\" me=\"Style321\" /><GroupFooterStyle parent=\"Style2" +
                "68\" me=\"Style320\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Colu" +
                "mnDivider><Width>20</Width><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn" +
                "><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style322\" /><Style parent=" +
                "\"Style268\" me=\"Style323\" /><FooterStyle parent=\"Style271\" me=\"Style324\" /><Edito" +
                "rStyle parent=\"Style269\" me=\"Style325\" /><GroupHeaderStyle parent=\"Style268\" me=" +
                "\"Style327\" /><GroupFooterStyle parent=\"Style268\" me=\"Style326\" /><Visible>True</" +
                "Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>65</Width><Height>" +
                "15</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style270\" me=\"Style328\" /><Style parent=\"Style268\" me=\"Style329\" /><FooterS" +
                "tyle parent=\"Style271\" me=\"Style330\" /><EditorStyle parent=\"Style269\" me=\"Style3" +
                "31\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style333\" /><GroupFooterStyle pare" +
                "nt=\"Style268\" me=\"Style332\" /><Visible>True</Visible><ColumnDivider>Gainsboro,No" +
                "ne</ColumnDivider><Width>16</Width><Height>15</Height><DCIdx>19</DCIdx></C1Displ" +
                "ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style334\" /><Style" +
                " parent=\"Style268\" me=\"Style335\" /><FooterStyle parent=\"Style271\" me=\"Style336\" " +
                "/><EditorStyle parent=\"Style269\" me=\"Style337\" /><GroupHeaderStyle parent=\"Style" +
                "268\" me=\"Style339\" /><GroupFooterStyle parent=\"Style268\" me=\"Style338\" /><Visibl" +
                "e>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</Width><H" +
                "eight>15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
                "le parent=\"Style270\" me=\"Style340\" /><Style parent=\"Style268\" me=\"Style341\" /><F" +
                "ooterStyle parent=\"Style271\" me=\"Style342\" /><EditorStyle parent=\"Style269\" me=\"" +
                "Style343\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style345\" /><GroupFooterStyl" +
                "e parent=\"Style268\" me=\"Style344\" /><Visible>True</Visible><ColumnDivider>Gainsb" +
                "oro,None</ColumnDivider><Width>16</Width><Height>15</Height><DCIdx>21</DCIdx></C" +
                "1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style346\" />" +
                "<Style parent=\"Style268\" me=\"Style347\" /><FooterStyle parent=\"Style271\" me=\"Styl" +
                "e348\" /><EditorStyle parent=\"Style269\" me=\"Style349\" /><GroupHeaderStyle parent=" +
                "\"Style268\" me=\"Style351\" /><GroupFooterStyle parent=\"Style268\" me=\"Style350\" /><" +
                "Visible>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</Wi" +
                "dth><Height>15</Height><DCIdx>22</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
                "ingStyle parent=\"Style270\" me=\"Style352\" /><Style parent=\"Style268\" me=\"Style353" +
                "\" /><FooterStyle parent=\"Style271\" me=\"Style354\" /><EditorStyle parent=\"Style269" +
                "\" me=\"Style355\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style357\" /><GroupFoot" +
                "erStyle parent=\"Style268\" me=\"Style356\" /><ColumnDivider>DarkGray,Single</Column" +
                "Divider><Width>90</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C" +
                "1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style358\" /><Style parent=\"St" +
                "yle268\" me=\"Style359\" /><FooterStyle parent=\"Style271\" me=\"Style360\" /><EditorSt" +
                "yle parent=\"Style269\" me=\"Style361\" /><GroupHeaderStyle parent=\"Style268\" me=\"St" +
                "yle363\" /><GroupFooterStyle parent=\"Style268\" me=\"Style362\" /><ColumnDivider>Dar" +
                "kGray,Single</ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>12</DCIdx" +
                "></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style364" +
                "\" /><Style parent=\"Style268\" me=\"Style365\" /><FooterStyle parent=\"Style271\" me=\"" +
                "Style366\" /><EditorStyle parent=\"Style269\" me=\"Style367\" /><GroupHeaderStyle par" +
                "ent=\"Style268\" me=\"Style369\" /><GroupFooterStyle parent=\"Style268\" me=\"Style368\"" +
                " /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</He" +
                "ight><DCIdx>24</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
                "tyle270\" me=\"Style370\" /><Style parent=\"Style268\" me=\"Style371\" /><FooterStyle p" +
                "arent=\"Style271\" me=\"Style372\" /><EditorStyle parent=\"Style269\" me=\"Style373\" />" +
                "<GroupHeaderStyle parent=\"Style268\" me=\"Style375\" /><GroupFooterStyle parent=\"St" +
                "yle268\" me=\"Style374\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15" +
                "</Height><DCIdx>27</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
                "t=\"Style270\" me=\"Style376\" /><Style parent=\"Style268\" me=\"Style377\" /><FooterSty" +
                "le parent=\"Style271\" me=\"Style378\" /><EditorStyle parent=\"Style269\" me=\"Style379" +
                "\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style381\" /><GroupFooterStyle parent" +
                "=\"Style268\" me=\"Style380\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
                ">20</Width><Height>15</Height><DCIdx>28</DCIdx></C1DisplayColumn><C1DisplayColum" +
                "n><HeadingStyle parent=\"Style270\" me=\"Style382\" /><Style parent=\"Style268\" me=\"S" +
                "tyle383\" /><FooterStyle parent=\"Style271\" me=\"Style384\" /><EditorStyle parent=\"S" +
                "tyle269\" me=\"Style385\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style387\" /><Gr" +
                "oupFooterStyle parent=\"Style268\" me=\"Style386\" /><ColumnDivider>DarkGray,Single<" +
                "/ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayCo" +
                "lumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style388\" /><Style par" +
                "ent=\"Style268\" me=\"Style389\" /><FooterStyle parent=\"Style271\" me=\"Style390\" /><E" +
                "ditorStyle parent=\"Style269\" me=\"Style391\" /><GroupHeaderStyle parent=\"Style268\"" +
                " me=\"Style393\" /><GroupFooterStyle parent=\"Style268\" me=\"Style392\" /><ColumnDivi" +
                "der>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</Height><DCIdx>6<" +
                "/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"St" +
                "yle394\" /><Style parent=\"Style268\" me=\"Style395\" /><FooterStyle parent=\"Style271" +
                "\" me=\"Style396\" /><EditorStyle parent=\"Style269\" me=\"Style397\" /><GroupHeaderSty" +
                "le parent=\"Style268\" me=\"Style399\" /><GroupFooterStyle parent=\"Style268\" me=\"Sty" +
                "le398\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>95</Width><Height>" +
                "15</Height><DCIdx>25</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style270\" me=\"Style400\" /><Style parent=\"Style268\" me=\"Style401\" /><FooterS" +
                "tyle parent=\"Style271\" me=\"Style402\" /><EditorStyle parent=\"Style269\" me=\"Style4" +
                "03\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style405\" /><GroupFooterStyle pare" +
                "nt=\"Style268\" me=\"Style404\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
                "th>20</Width><Height>15</Height><DCIdx>26</DCIdx></C1DisplayColumn><C1DisplayCol" +
                "umn><HeadingStyle parent=\"Style270\" me=\"Style406\" /><Style parent=\"Style268\" me=" +
                "\"Style407\" /><FooterStyle parent=\"Style271\" me=\"Style408\" /><EditorStyle parent=" +
                "\"Style269\" me=\"Style409\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style411\" /><" +
                "GroupFooterStyle parent=\"Style268\" me=\"Style410\" /><ColumnDivider>DarkGray,Singl" +
                "e</ColumnDivider><Width>35</Width><Height>15</Height><DCIdx>33</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style412\" /><Style " +
                "parent=\"Style268\" me=\"Style413\" /><FooterStyle parent=\"Style271\" me=\"Style414\" /" +
                "><EditorStyle parent=\"Style269\" me=\"Style415\" /><GroupHeaderStyle parent=\"Style2" +
                "68\" me=\"Style417\" /><GroupFooterStyle parent=\"Style268\" me=\"Style416\" /><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Width>58</Width><Height>15</Height><DCIdx" +
                ">9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=" +
                "\"Style418\" /><Style parent=\"Style268\" me=\"Style419\" /><FooterStyle parent=\"Style" +
                "271\" me=\"Style420\" /><EditorStyle parent=\"Style269\" me=\"Style421\" /><GroupHeader" +
                "Style parent=\"Style268\" me=\"Style423\" /><GroupFooterStyle parent=\"Style268\" me=\"" +
                "Style422\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>58</Width><Heig" +
                "ht>15</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
                "parent=\"Style270\" me=\"Style424\" /><Style parent=\"Style268\" me=\"Style425\" /><Foot" +
                "erStyle parent=\"Style271\" me=\"Style426\" /><EditorStyle parent=\"Style269\" me=\"Sty" +
                "le427\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style429\" /><GroupFooterStyle p" +
                "arent=\"Style268\" me=\"Style428\" /><ColumnDivider>DarkGray,Single</ColumnDivider><" +
                "Width>20</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayC" +
                "olumn><HeadingStyle parent=\"Style270\" me=\"Style430\" /><Style parent=\"Style268\" m" +
                "e=\"Style431\" /><FooterStyle parent=\"Style271\" me=\"Style432\" /><EditorStyle paren" +
                "t=\"Style269\" me=\"Style433\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style435\" /" +
                "><GroupFooterStyle parent=\"Style268\" me=\"Style434\" /><ColumnDivider>DarkGray,Sin" +
                "gle</ColumnDivider><Width>35</Width><Height>15</Height><DCIdx>16</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style436\" /><Styl" +
                "e parent=\"Style268\" me=\"Style437\" /><FooterStyle parent=\"Style271\" me=\"Style438\"" +
                " /><EditorStyle parent=\"Style269\" me=\"Style439\" /><GroupHeaderStyle parent=\"Styl" +
                "e268\" me=\"Style441\" /><GroupFooterStyle parent=\"Style268\" me=\"Style440\" /><Colum" +
                "nDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</Height><DCI" +
                "dx>36</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" " +
                "me=\"Style442\" /><Style parent=\"Style268\" me=\"Style443\" /><FooterStyle parent=\"St" +
                "yle271\" me=\"Style444\" /><EditorStyle parent=\"Style269\" me=\"Style445\" /><GroupHea" +
                "derStyle parent=\"Style268\" me=\"Style447\" /><GroupFooterStyle parent=\"Style268\" m" +
                "e=\"Style446\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>25</Width><H" +
                "eight>15</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
                "le parent=\"Style270\" me=\"Style448\" /><Style parent=\"Style268\" me=\"Style449\" /><F" +
                "ooterStyle parent=\"Style271\" me=\"Style450\" /><EditorStyle parent=\"Style269\" me=\"" +
                "Style451\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style453\" /><GroupFooterStyl" +
                "e parent=\"Style268\" me=\"Style452\" /><ColumnDivider>DarkGray,Single</ColumnDivide" +
                "r><Width>80</Width><Height>15</Height><DCIdx>29</DCIdx></C1DisplayColumn><C1Disp" +
                "layColumn><HeadingStyle parent=\"Style270\" me=\"Style454\" /><Style parent=\"Style26" +
                "8\" me=\"Style455\" /><FooterStyle parent=\"Style271\" me=\"Style456\" /><EditorStyle p" +
                "arent=\"Style269\" me=\"Style457\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style45" +
                "9\" /><GroupFooterStyle parent=\"Style268\" me=\"Style458\" /><ColumnDivider>DarkGray" +
                ",Single</ColumnDivider><Width>80</Width><Height>15</Height><DCIdx>10</DCIdx></C1" +
                "DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style460\" /><" +
                "Style parent=\"Style268\" me=\"Style461\" /><FooterStyle parent=\"Style271\" me=\"Style" +
                "462\" /><EditorStyle parent=\"Style269\" me=\"Style463\" /><GroupHeaderStyle parent=\"" +
                "Style268\" me=\"Style465\" /><GroupFooterStyle parent=\"Style268\" me=\"Style464\" /><C" +
                "olumnDivider>DarkGray,Single</ColumnDivider><Width>80</Width><Height>15</Height>" +
                "<DCIdx>30</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
                "70\" me=\"Style466\" /><Style parent=\"Style268\" me=\"Style467\" /><FooterStyle parent" +
                "=\"Style271\" me=\"Style468\" /><EditorStyle parent=\"Style269\" me=\"Style469\" /><Grou" +
                "pHeaderStyle parent=\"Style268\" me=\"Style471\" /><GroupFooterStyle parent=\"Style26" +
                "8\" me=\"Style470\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>45</Widt" +
                "h><Height>15</Height><DCIdx>31</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
                "gStyle parent=\"Style270\" me=\"Style472\" /><Style parent=\"Style268\" me=\"Style473\" " +
                "/><FooterStyle parent=\"Style271\" me=\"Style474\" /><EditorStyle parent=\"Style269\" " +
                "me=\"Style475\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style477\" /><GroupFooter" +
                "Style parent=\"Style268\" me=\"Style476\" /><ColumnDivider>DarkGray,Single</ColumnDi" +
                "vider><Width>20</Width><Height>15</Height><DCIdx>32</DCIdx></C1DisplayColumn><C1" +
                "DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style478\" /><Style parent=\"Sty" +
                "le268\" me=\"Style479\" /><FooterStyle parent=\"Style271\" me=\"Style480\" /><EditorSty" +
                "le parent=\"Style269\" me=\"Style481\" /><GroupHeaderStyle parent=\"Style268\" me=\"Sty" +
                "le483\" /><GroupFooterStyle parent=\"Style268\" me=\"Style482\" /><ColumnDivider>Dark" +
                "Gray,Single</ColumnDivider><Width>175</Width><Height>15</Height><DCIdx>18</DCIdx" +
                "></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style484" +
                "\" /><Style parent=\"Style268\" me=\"Style485\" /><FooterStyle parent=\"Style271\" me=\"" +
                "Style486\" /><EditorStyle parent=\"Style269\" me=\"Style487\" /><GroupHeaderStyle par" +
                "ent=\"Style268\" me=\"Style489\" /><GroupFooterStyle parent=\"Style268\" me=\"Style488\"" +
                " /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>90</Width><Height>15</He" +
                "ight><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
                "tyle270\" me=\"Style490\" /><Style parent=\"Style268\" me=\"Style491\" /><FooterStyle p" +
                "arent=\"Style271\" me=\"Style492\" /><EditorStyle parent=\"Style269\" me=\"Style493\" />" +
                "<GroupHeaderStyle parent=\"Style268\" me=\"Style495\" /><GroupFooterStyle parent=\"St" +
                "yle268\" me=\"Style494\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>30<" +
                "/Width><Height>15</Height><DCIdx>34</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
                "eadingStyle parent=\"Style270\" me=\"Style496\" /><Style parent=\"Style268\" me=\"Style" +
                "497\" /><FooterStyle parent=\"Style271\" me=\"Style498\" /><EditorStyle parent=\"Style" +
                "269\" me=\"Style499\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style501\" /><GroupF" +
                "ooterStyle parent=\"Style268\" me=\"Style500\" /><ColumnDivider>DarkGray,Single</Col" +
                "umnDivider><Width>30</Width><Height>15</Height><DCIdx>35</DCIdx></C1DisplayColum" +
                "n><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style502\" /><Style parent" +
                "=\"Style268\" me=\"Style503\" /><FooterStyle parent=\"Style271\" me=\"Style504\" /><Edit" +
                "orStyle parent=\"Style269\" me=\"Style505\" /><GroupHeaderStyle parent=\"Style268\" me" +
                "=\"Style507\" /><GroupFooterStyle parent=\"Style268\" me=\"Style506\" /><ColumnDivider" +
                ">DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>37</D" +
                "CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Styl" +
                "e508\" /><Style parent=\"Style268\" me=\"Style509\" /><FooterStyle parent=\"Style271\" " +
                "me=\"Style510\" /><EditorStyle parent=\"Style269\" me=\"Style511\" /><GroupHeaderStyle" +
                " parent=\"Style268\" me=\"Style513\" /><GroupFooterStyle parent=\"Style268\" me=\"Style" +
                "512\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>95</Width><Height>15" +
                "</Height><DCIdx>38</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
                "t=\"Style270\" me=\"Style514\" /><Style parent=\"Style268\" me=\"Style515\" /><FooterSty" +
                "le parent=\"Style271\" me=\"Style516\" /><EditorStyle parent=\"Style269\" me=\"Style517" +
                "\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style519\" /><GroupFooterStyle parent" +
                "=\"Style268\" me=\"Style518\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
                ">45</Width><Height>15</Height><DCIdx>39</DCIdx></C1DisplayColumn><C1DisplayColum" +
                "n><HeadingStyle parent=\"Style270\" me=\"Style520\" /><Style parent=\"Style268\" me=\"S" +
                "tyle521\" /><FooterStyle parent=\"Style271\" me=\"Style522\" /><EditorStyle parent=\"S" +
                "tyle269\" me=\"Style523\" /><GroupHeaderStyle parent=\"Style268\" me=\"Style525\" /><Gr" +
                "oupFooterStyle parent=\"Style268\" me=\"Style524\" /><ColumnDivider>DarkGray,Single<" +
                "/ColumnDivider><Width>20</Width><Height>15</Height><DCIdx>40</DCIdx></C1DisplayC" +
                "olumn><C1DisplayColumn><HeadingStyle parent=\"Style270\" me=\"Style130\" /><Style pa" +
                "rent=\"Style268\" me=\"Style131\" /><FooterStyle parent=\"Style271\" me=\"Style132\" /><" +
                "EditorStyle parent=\"Style269\" me=\"Style133\" /><GroupHeaderStyle parent=\"Style268" +
                "\" me=\"Style135\" /><GroupFooterStyle parent=\"Style268\" me=\"Style134\" /><ColumnDiv" +
                "ider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>41</DCIdx></C1Disp" +
                "layColumn></internalCols><ClientRect>0, 0, 548, 96</ClientRect><BorderSide>Right" +
                "</BorderSide></C1.Win.C1TrueDBGrid.MergeView><C1.Win.C1TrueDBGrid.MergeView HBar" +
                "Style=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"Split[0,1]\" AllowRo" +
                "wSizing=\"None\" AllowHorizontalSizing=\"False\" AllowVerticalSizing=\"False\" Caption" +
                "Height=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"20\" ExtendRightColumn=\"" +
                "True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16" +
                "\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Cap" +
                "tion>Detail</Caption><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle p" +
                "arent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Filte" +
                "rBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Sty" +
                "le3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" " +
                "me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveSt" +
                "yle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><" +
                "RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent" +
                "=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1" +
                "DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style160\" /><Style parent=\"Style" +
                "1\" me=\"Style161\" /><FooterStyle parent=\"Style3\" me=\"Style162\" /><EditorStyle par" +
                "ent=\"Style5\" me=\"Style163\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style165\" /><" +
                "GroupFooterStyle parent=\"Style1\" me=\"Style164\" /><ColumnDivider>Gainsboro,Single" +
                "</ColumnDivider><Width>60</Width><Height>15</Height><DCIdx>23</DCIdx></C1Display" +
                "Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style pare" +
                "nt=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorSt" +
                "yle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21" +
                "\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><ColumnDivider>Gainsboro,Si" +
                "ngle</ColumnDivider><Width>60</Width><Height>15</Height><DCIdx>0</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style p" +
                "arent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><Edito" +
                "rStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
                "e27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><ColumnDivider>Gainsboro" +
                ",Single</ColumnDivider><Width>60</Width><Height>15</Height><DCIdx>1</DCIdx></C1D" +
                "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Styl" +
                "e parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><Ed" +
                "itorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
                "tyle33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><ColumnDivider>Gainsb" +
                "oro,Single</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>2</DCIdx></" +
                "C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><S" +
                "tyle parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" />" +
                "<EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me" +
                "=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><ColumnDivider>Gai" +
                "nsboro,Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>3</DCIdx" +
                "></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /" +
                "><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\"" +
                " /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\"" +
                " me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><ColumnDivider>" +
                "Gainsboro,Single</ColumnDivider><Width>25</Width><Height>15</Height><DCIdx>8</DC" +
                "Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style94" +
                "\" /><Style parent=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=\"Style" +
                "96\" /><EditorStyle parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent=\"Styl" +
                "e1\" me=\"Style99\" /><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><ColumnDivid" +
                "er>Gainsboro,Single</ColumnDivider><Width>20</Width><Height>15</Height><DCIdx>13" +
                "</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
                "le100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterStyle parent=\"Style3\" me=" +
                "\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><GroupHeaderStyle pare" +
                "nt=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1\" me=\"Style104\" /><C" +
                "olumnDivider>DarkGray,Single</ColumnDivider><Width>65</Width><Height>15</Height>" +
                "<DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
                "\" me=\"Style136\" /><Style parent=\"Style1\" me=\"Style137\" /><FooterStyle parent=\"St" +
                "yle3\" me=\"Style138\" /><EditorStyle parent=\"Style5\" me=\"Style139\" /><GroupHeaderS" +
                "tyle parent=\"Style1\" me=\"Style141\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
                "140\" /><ColumnDivider>Gainsboro,None</ColumnDivider><Width>18</Width><Height>15<" +
                "/Height><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
                "=\"Style2\" me=\"Style142\" /><Style parent=\"Style1\" me=\"Style143\" /><FooterStyle pa" +
                "rent=\"Style3\" me=\"Style144\" /><EditorStyle parent=\"Style5\" me=\"Style145\" /><Grou" +
                "pHeaderStyle parent=\"Style1\" me=\"Style147\" /><GroupFooterStyle parent=\"Style1\" m" +
                "e=\"Style146\" /><ColumnDivider>Gainsboro,None</ColumnDivider><Width>18</Width><He" +
                "ight>15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
                "e parent=\"Style2\" me=\"Style148\" /><Style parent=\"Style1\" me=\"Style149\" /><Footer" +
                "Style parent=\"Style3\" me=\"Style150\" /><EditorStyle parent=\"Style5\" me=\"Style151\"" +
                " /><GroupHeaderStyle parent=\"Style1\" me=\"Style153\" /><GroupFooterStyle parent=\"S" +
                "tyle1\" me=\"Style152\" /><ColumnDivider>Gainsboro,None</ColumnDivider><Width>18</W" +
                "idth><Height>15</Height><DCIdx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
                "dingStyle parent=\"Style2\" me=\"Style154\" /><Style parent=\"Style1\" me=\"Style155\" /" +
                "><FooterStyle parent=\"Style3\" me=\"Style156\" /><EditorStyle parent=\"Style5\" me=\"S" +
                "tyle157\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style159\" /><GroupFooterStyle p" +
                "arent=\"Style1\" me=\"Style158\" /><ColumnDivider>Gainsboro,None</ColumnDivider><Wid" +
                "th>18</Width><Height>15</Height><DCIdx>22</DCIdx></C1DisplayColumn><C1DisplayCol" +
                "umn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Styl" +
                "e41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" " +
                "me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyl" +
                "e parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>Gainsboro" +
                ",Single</ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>4</DCIdx></C1D" +
                "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style88\" /><Styl" +
                "e parent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" me=\"Style90\" /><Ed" +
                "itorStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
                "tyle93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><Visible>True</Visibl" +
                "e><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>90</Width><Height>15</He" +
                "ight><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
                "tyle2\" me=\"Style232\" /><Style parent=\"Style1\" me=\"Style233\" /><FooterStyle paren" +
                "t=\"Style3\" me=\"Style234\" /><EditorStyle parent=\"Style5\" me=\"Style235\" /><GroupHe" +
                "aderStyle parent=\"Style1\" me=\"Style237\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
                "Style236\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivide" +
                "r><Width>25</Width><Height>15</Height><DCIdx>35</DCIdx></C1DisplayColumn><C1Disp" +
                "layColumn><HeadingStyle parent=\"Style2\" me=\"Style166\" /><Style parent=\"Style1\" m" +
                "e=\"Style167\" /><FooterStyle parent=\"Style3\" me=\"Style168\" /><EditorStyle parent=" +
                "\"Style5\" me=\"Style169\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style171\" /><Grou" +
                "pFooterStyle parent=\"Style1\" me=\"Style170\" /><Visible>True</Visible><ColumnDivid" +
                "er>Gainsboro,Single</ColumnDivider><Width>15</Width><Height>15</Height><DCIdx>24" +
                "</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
                "le184\" /><Style parent=\"Style1\" me=\"Style185\" /><FooterStyle parent=\"Style3\" me=" +
                "\"Style186\" /><EditorStyle parent=\"Style5\" me=\"Style187\" /><GroupHeaderStyle pare" +
                "nt=\"Style1\" me=\"Style189\" /><GroupFooterStyle parent=\"Style1\" me=\"Style188\" /><V" +
                "isible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>90</W" +
                "idth><Height>15</Height><DCIdx>27</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
                "dingStyle parent=\"Style2\" me=\"Style190\" /><Style parent=\"Style1\" me=\"Style191\" /" +
                "><FooterStyle parent=\"Style3\" me=\"Style192\" /><EditorStyle parent=\"Style5\" me=\"S" +
                "tyle193\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style195\" /><GroupFooterStyle p" +
                "arent=\"Style1\" me=\"Style194\" /><Visible>True</Visible><ColumnDivider>Gainsboro,S" +
                "ingle</ColumnDivider><Width>15</Width><Height>15</Height><DCIdx>28</DCIdx></C1Di" +
                "splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style" +
                " parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><Edi" +
                "torStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
                "yle51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible" +
                "><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><Height>15</Hei" +
                "ght><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
                "le2\" me=\"Style220\" /><Style parent=\"Style1\" me=\"Style221\" /><FooterStyle parent=" +
                "\"Style3\" me=\"Style222\" /><EditorStyle parent=\"Style5\" me=\"Style223\" /><GroupHead" +
                "erStyle parent=\"Style1\" me=\"Style225\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
                "yle224\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider>" +
                "<Width>35</Width><Height>15</Height><DCIdx>33</DCIdx></C1DisplayColumn><C1Displa" +
                "yColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"" +
                "Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Styl" +
                "e5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooter" +
                "Style parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>Gains" +
                "boro,Single</ColumnDivider><Width>15</Width><Height>15</Height><DCIdx>6</DCIdx><" +
                "/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style172\" />" +
                "<Style parent=\"Style1\" me=\"Style173\" /><FooterStyle parent=\"Style3\" me=\"Style174" +
                "\" /><EditorStyle parent=\"Style5\" me=\"Style175\" /><GroupHeaderStyle parent=\"Style" +
                "1\" me=\"Style177\" /><GroupFooterStyle parent=\"Style1\" me=\"Style176\" /><Visible>Tr" +
                "ue</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><Hei" +
                "ght>15</Height><DCIdx>25</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
                " parent=\"Style2\" me=\"Style226\" /><Style parent=\"Style1\" me=\"Style227\" /><FooterS" +
                "tyle parent=\"Style3\" me=\"Style228\" /><EditorStyle parent=\"Style5\" me=\"Style229\" " +
                "/><GroupHeaderStyle parent=\"Style1\" me=\"Style231\" /><GroupFooterStyle parent=\"St" +
                "yle1\" me=\"Style230\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Co" +
                "lumnDivider><Width>25</Width><Height>15</Height><DCIdx>34</DCIdx></C1DisplayColu" +
                "mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style178\" /><Style parent=" +
                "\"Style1\" me=\"Style179\" /><FooterStyle parent=\"Style3\" me=\"Style180\" /><EditorSty" +
                "le parent=\"Style5\" me=\"Style181\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style18" +
                "3\" /><GroupFooterStyle parent=\"Style1\" me=\"Style182\" /><Visible>True</Visible><C" +
                "olumnDivider>Gainsboro,Single</ColumnDivider><Width>15</Width><Height>15</Height" +
                "><DCIdx>26</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
                "2\" me=\"Style196\" /><Style parent=\"Style1\" me=\"Style197\" /><FooterStyle parent=\"S" +
                "tyle3\" me=\"Style198\" /><EditorStyle parent=\"Style5\" me=\"Style199\" /><GroupHeader" +
                "Style parent=\"Style1\" me=\"Style201\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
                "e200\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><W" +
                "idth>80</Width><Height>15</Height><DCIdx>29</DCIdx></C1DisplayColumn><C1DisplayC" +
                "olumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"St" +
                "yle71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5" +
                "\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style75\" /><GroupFooterSt" +
                "yle parent=\"Style1\" me=\"Style74\" /><Visible>True</Visible><ColumnDivider>Gainsbo" +
                "ro,Single</ColumnDivider><Width>58</Width><Height>15</Height><DCIdx>9</DCIdx></C" +
                "1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style106\" /><S" +
                "tyle parent=\"Style1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=\"Style108\" " +
                "/><EditorStyle parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle parent=\"Style1\"" +
                " me=\"Style111\" /><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><Visible>True" +
                "</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>58</Width><Heigh" +
                "t>15</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
                "arent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle" +
                " parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><Gro" +
                "upHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" m" +
                "e=\"Style62\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivi" +
                "der><Width>15</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1Dis" +
                "playColumn><HeadingStyle parent=\"Style2\" me=\"Style112\" /><Style parent=\"Style1\" " +
                "me=\"Style113\" /><FooterStyle parent=\"Style3\" me=\"Style114\" /><EditorStyle parent" +
                "=\"Style5\" me=\"Style115\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style117\" /><Gro" +
                "upFooterStyle parent=\"Style1\" me=\"Style116\" /><Visible>True</Visible><ColumnDivi" +
                "der>Gainsboro,Single</ColumnDivider><Width>35</Width><Height>15</Height><DCIdx>1" +
                "6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
                "yle238\" /><Style parent=\"Style1\" me=\"Style239\" /><FooterStyle parent=\"Style3\" me" +
                "=\"Style240\" /><EditorStyle parent=\"Style5\" me=\"Style241\" /><GroupHeaderStyle par" +
                "ent=\"Style1\" me=\"Style243\" /><GroupFooterStyle parent=\"Style1\" me=\"Style242\" /><" +
                "Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>15</" +
                "Width><Height>15</Height><DCIdx>36</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
                "adingStyle parent=\"Style2\" me=\"Style244\" /><Style parent=\"Style1\" me=\"Style245\" " +
                "/><FooterStyle parent=\"Style3\" me=\"Style246\" /><EditorStyle parent=\"Style5\" me=\"" +
                "Style247\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style249\" /><GroupFooterStyle " +
                "parent=\"Style1\" me=\"Style248\" /><Visible>True</Visible><ColumnDivider>Gainsboro," +
                "Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>37</DCIdx></C1D" +
                "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style118\" /><Sty" +
                "le parent=\"Style1\" me=\"Style119\" /><FooterStyle parent=\"Style3\" me=\"Style120\" />" +
                "<EditorStyle parent=\"Style5\" me=\"Style121\" /><GroupHeaderStyle parent=\"Style1\" m" +
                "e=\"Style123\" /><GroupFooterStyle parent=\"Style1\" me=\"Style122\" /><Visible>True</" +
                "Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>25</Width><Height>" +
                "15</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style2\" me=\"Style208\" /><Style parent=\"Style1\" me=\"Style209\" /><FooterStyle" +
                " parent=\"Style3\" me=\"Style210\" /><EditorStyle parent=\"Style5\" me=\"Style211\" /><G" +
                "roupHeaderStyle parent=\"Style1\" me=\"Style213\" /><GroupFooterStyle parent=\"Style1" +
                "\" me=\"Style212\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Column" +
                "Divider><Width>45</Width><Height>15</Height><DCIdx>31</DCIdx></C1DisplayColumn><" +
                "C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style214\" /><Style parent=\"Sty" +
                "le1\" me=\"Style215\" /><FooterStyle parent=\"Style3\" me=\"Style216\" /><EditorStyle p" +
                "arent=\"Style5\" me=\"Style217\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style219\" /" +
                "><GroupFooterStyle parent=\"Style1\" me=\"Style218\" /><Visible>True</Visible><Colum" +
                "nDivider>Gainsboro,Single</ColumnDivider><Width>20</Width><Height>15</Height><DC" +
                "Idx>32</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
                "e=\"Style250\" /><Style parent=\"Style1\" me=\"Style251\" /><FooterStyle parent=\"Style" +
                "3\" me=\"Style252\" /><EditorStyle parent=\"Style5\" me=\"Style253\" /><GroupHeaderStyl" +
                "e parent=\"Style1\" me=\"Style255\" /><GroupFooterStyle parent=\"Style1\" me=\"Style254" +
                "\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width" +
                ">95</Width><Height>15</Height><DCIdx>38</DCIdx></C1DisplayColumn><C1DisplayColum" +
                "n><HeadingStyle parent=\"Style2\" me=\"Style256\" /><Style parent=\"Style1\" me=\"Style" +
                "257\" /><FooterStyle parent=\"Style3\" me=\"Style258\" /><EditorStyle parent=\"Style5\"" +
                " me=\"Style259\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style261\" /><GroupFooterS" +
                "tyle parent=\"Style1\" me=\"Style260\" /><Visible>True</Visible><ColumnDivider>Gains" +
                "boro,Single</ColumnDivider><Width>45</Width><Height>15</Height><DCIdx>39</DCIdx>" +
                "</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style262\" /" +
                "><Style parent=\"Style1\" me=\"Style263\" /><FooterStyle parent=\"Style3\" me=\"Style26" +
                "4\" /><EditorStyle parent=\"Style5\" me=\"Style265\" /><GroupHeaderStyle parent=\"Styl" +
                "e1\" me=\"Style267\" /><GroupFooterStyle parent=\"Style1\" me=\"Style266\" /><Visible>T" +
                "rue</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>15</Width><He" +
                "ight>15</Height><DCIdx>40</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
                "e parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterSt" +
                "yle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><" +
                "GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1" +
                "\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnD" +
                "ivider><Width>80</Width><Height>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C" +
                "1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style202\" /><Style parent=\"Styl" +
                "e1\" me=\"Style203\" /><FooterStyle parent=\"Style3\" me=\"Style204\" /><EditorStyle pa" +
                "rent=\"Style5\" me=\"Style205\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style207\" />" +
                "<GroupFooterStyle parent=\"Style1\" me=\"Style206\" /><Visible>True</Visible><Column" +
                "Divider>Gainsboro,Single</ColumnDivider><Width>80</Width><Height>15</Height><DCI" +
                "dx>30</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
                "=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style3\" " +
                "me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle par" +
                "ent=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Vi" +
                "sible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>90</Wi" +
                "dth><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
                "ingStyle parent=\"Style2\" me=\"Style124\" /><Style parent=\"Style1\" me=\"Style125\" />" +
                "<FooterStyle parent=\"Style3\" me=\"Style126\" /><EditorStyle parent=\"Style5\" me=\"St" +
                "yle127\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style129\" /><GroupFooterStyle pa" +
                "rent=\"Style1\" me=\"Style128\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Si" +
                "ngle</ColumnDivider><Width>175</Width><Height>15</Height><DCIdx>18</DCIdx></C1Di" +
                "splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style526\" /><Styl" +
                "e parent=\"Style1\" me=\"Style527\" /><FooterStyle parent=\"Style3\" me=\"Style528\" /><" +
                "EditorStyle parent=\"Style5\" me=\"Style529\" /><GroupHeaderStyle parent=\"Style1\" me" +
                "=\"Style531\" /><GroupFooterStyle parent=\"Style1\" me=\"Style530\" /><ColumnDivider>D" +
                "arkGray,Single</ColumnDivider><Height>15</Height><DCIdx>41</DCIdx></C1DisplayCol" +
                "umn></internalCols><ClientRect>553, 0, 1047, 96</ClientRect><BorderSide>Left</Bo" +
                "rderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" " +
                "me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"" +
                "Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Ina" +
                "ctive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Edito" +
                "r\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenR" +
                "ow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSel" +
                "ector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Gro" +
                "up\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>2</horzSplits><Layout>" +
                "Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 16" +
                "00, 96</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFoot" +
                "erStyle parent=\"\" me=\"Style15\" /></Blob>";
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.RateChangeMenuItem,
																																										this.ReturnMenuItem,
																																										this.RecallMenuItem,
																																										this.PcChangeMenuItem,
																																										this.Sep1MenuItem,
																																										this.SendToMenuItem,
																																										this.ShowMenuItem,
																																										this.DockMenuItem,
																																										this.Sep2MenuItem,
																																										this.ExitMenuItem});
            this.MainContextMenu.Popup += new System.EventHandler(this.MainContextMenu_Popup);
            // 
            // RateChangeMenuItem
            // 
            this.RateChangeMenuItem.Index = 0;
            this.RateChangeMenuItem.Text = "Rate Change";
            this.RateChangeMenuItem.Click += new System.EventHandler(this.RateChangeMenuItem_Click);
            // 
            // ReturnMenuItem
            // 
            this.ReturnMenuItem.Index = 1;
            this.ReturnMenuItem.Text = "Return";
            this.ReturnMenuItem.Click += new System.EventHandler(this.ReturnMenuItem_Click);
            // 
            // RecallMenuItem
            // 
            this.RecallMenuItem.Index = 2;
            this.RecallMenuItem.Text = "Recall";
            this.RecallMenuItem.Click += new System.EventHandler(this.RecallMenuItem_Click);
            // 
            // PcChangeMenuItem
            // 
            this.PcChangeMenuItem.Index = 3;
            this.PcChangeMenuItem.Text = "P/C Change";
            this.PcChangeMenuItem.Click += new System.EventHandler(this.PcChangeMenuItem_Click);
            // 
            // Sep1MenuItem
            // 
            this.Sep1MenuItem.Index = 4;
            this.Sep1MenuItem.Text = "-";
            // 
            // ShowMenuItem
            // 
            this.ShowMenuItem.Index = 6;
            this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.ShowIncomeMenuItem,
																																								 this.ShowCurrencyMenuItem,
																																								 this.ShowTermDateMenuItem,
																																								 this.ShowRecallQuantityMenuItem,
																																								 this.ShowDividendReclaimMenuItem,
																																								 this.ShowSettlementDetailMenuItem,
																																								 this.ShowValueRatioMenuItem});
            this.ShowMenuItem.Text = "Show";
            // 
            // ShowIncomeMenuItem
            // 
            this.ShowIncomeMenuItem.Index = 0;
            this.ShowIncomeMenuItem.Text = "Income";
            this.ShowIncomeMenuItem.Click += new System.EventHandler(this.ShowIncomeMenuItem_Click);
            // 
            // ShowCurrencyMenuItem
            // 
            this.ShowCurrencyMenuItem.Index = 1;
            this.ShowCurrencyMenuItem.Text = "Currency";
            this.ShowCurrencyMenuItem.Click += new System.EventHandler(this.ShowCurrencyMenuItem_Click);
            // 
            // ShowTermDateMenuItem
            // 
            this.ShowTermDateMenuItem.Index = 2;
            this.ShowTermDateMenuItem.Text = "Term Date";
            this.ShowTermDateMenuItem.Click += new System.EventHandler(this.ShowTermDateMenuItem_Click);
            // 
            // ShowRecallQuantityMenuItem
            // 
            this.ShowRecallQuantityMenuItem.Index = 3;
            this.ShowRecallQuantityMenuItem.Text = "Recall Quantity";
            this.ShowRecallQuantityMenuItem.Click += new System.EventHandler(this.ShowRecallQuantityMenuItem_Click);
            // 
            // ShowDividendReclaimMenuItem
            // 
            this.ShowDividendReclaimMenuItem.Index = 4;
            this.ShowDividendReclaimMenuItem.Text = "Dividend Reclaim";
            this.ShowDividendReclaimMenuItem.Click += new System.EventHandler(this.ShowDividendReclaimMenuItem_Click);
            // 
            // ShowSettlementDetailMenuItem
            // 
            this.ShowSettlementDetailMenuItem.Index = 5;
            this.ShowSettlementDetailMenuItem.Text = "Settlement Detail";
            this.ShowSettlementDetailMenuItem.Click += new System.EventHandler(this.ShowSettlementDetailMenuItem_Click);
            // 
            // ShowValueRatioMenuItem
            // 
            this.ShowValueRatioMenuItem.Index = 6;
            this.ShowValueRatioMenuItem.Text = "Value Ratio";
            this.ShowValueRatioMenuItem.Click += new System.EventHandler(this.ShowValueRatioMenuItem_Click);
            // 
            // DockMenuItem
            // 
            this.DockMenuItem.Index = 7;
            this.DockMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.DockTopMenuItem,
																																								 this.DockBottomMenuItem,
																																								 this.DockSep1MenuItem,
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
            // DockSep1MenuItem
            // 
            this.DockSep1MenuItem.Index = 2;
            this.DockSep1MenuItem.Text = "-";
            // 
            // DockNoneMenuItem
            // 
            this.DockNoneMenuItem.Index = 3;
            this.DockNoneMenuItem.Text = "None";
            this.DockNoneMenuItem.Click += new System.EventHandler(this.DockNoneMenuItem_Click);
            // 
            // Sep2MenuItem
            // 
            this.Sep2MenuItem.Index = 8;
            this.Sep2MenuItem.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 9;
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // SummarySplitter
            // 
            this.SummarySplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SummarySplitter.Location = new System.Drawing.Point(0, 201);
            this.SummarySplitter.MinExtra = 149;
            this.SummarySplitter.MinSize = 204;
            this.SummarySplitter.Name = "SummarySplitter";
            this.SummarySplitter.Size = new System.Drawing.Size(1604, 4);
            this.SummarySplitter.TabIndex = 10;
            this.SummarySplitter.TabStop = false;
            // 
            // SummaryGrid
            // 
            this.SummaryGrid.AllowColSelect = false;
            this.SummaryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.SummaryGrid.AllowUpdate = false;
            this.SummaryGrid.CaptionHeight = 17;
            this.SummaryGrid.ColumnFooters = true;
            this.SummaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SummaryGrid.EmptyRows = true;
            this.SummaryGrid.FilterBar = true;
            this.SummaryGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.SummaryGrid.GroupByAreaVisible = false;
            this.SummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.SummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.SummaryGrid.Location = new System.Drawing.Point(0, 56);
            this.SummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.SummaryGrid.Name = "SummaryGrid";
            this.SummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.SummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.SummaryGrid.PreviewInfo.ZoomFactor = 75;
            this.SummaryGrid.RecordSelectorWidth = 16;
            this.SummaryGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.SummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.SummaryGrid.RowHeight = 15;
            this.SummaryGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
            this.SummaryGrid.Size = new System.Drawing.Size(1604, 145);
            this.SummaryGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.SummaryGrid.TabIndex = 11;
            this.SummaryGrid.Visible = false;
            this.SummaryGrid.WrapCellPointer = true;
            this.SummaryGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.SummaryGrid_Paint);
            this.SummaryGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.SummaryGrid_AfterFilter);
            this.SummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SummaryGrid_FormatText);
            this.SummaryGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book Group\"" +
                " DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
                "evel=\"0\" Caption=\"Parent\" DataField=\"BookParent\"><ValueItems /><GroupInfo /></C1" +
                "DataColumn><C1DataColumn Level=\"0\" Caption=\"Book\" DataField=\"Book\"><ValueItems /" +
                "><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security\" DataFiel" +
                "d=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
                "ion=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
                "aColumn Level=\"0\" Caption=\"PC\" DataField=\"PoolCode\"><ValueItems /><GroupInfo /><" +
                "/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" DataField=\"BaseType\"><ValueIte" +
                "ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Class\" DataFie" +
                "ld=\"ClassGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
                "\" Caption=\"Quantity\" DataField=\"QuantityBorrowed\" NumberFormat=\"FormatText Event" +
                "\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Amo" +
                "unt\" DataField=\"AmountBorrowed\" NumberFormat=\"FormatText Event\"><ValueItems /><G" +
                "roupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Rate\" DataField=\"Rate" +
                "Borrowed\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataCol" +
                "umn><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"QuantityLent\" NumberFo" +
                "rmat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
                "Level=\"0\" Caption=\"Amount\" DataField=\"AmountLent\" NumberFormat=\"FormatText Event" +
                "\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Rat" +
                "e\" DataField=\"RateLent\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo" +
                " /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Borrows\" DataField=\"AmountLim" +
                "itBorrow\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataCol" +
                "umn><C1DataColumn Level=\"0\" Caption=\"%\" DataField=\"AmountLimitBorrowUsage\" Numbe" +
                "rFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
                "mn Level=\"0\" Caption=\"Loans\" DataField=\"AmountLimitLoan\" NumberFormat=\"FormatTex" +
                "t Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
                "on=\"%\" DataField=\"AmountLimitLoanUsage\" NumberFormat=\"FormatText Event\"><ValueIt" +
                "ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Total\" DataFi" +
                "eld=\"AmountLimitTotal\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo " +
                "/></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"%\" DataField=\"AmountLimitTotal" +
                "Usage\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn" +
                "><C1DataColumn Level=\"0\" Caption=\"Box\" DataField=\"Income\" NumberFormat=\"FormatTe" +
                "xt Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
                "ion=\"Matchbook\" DataField=\"MatchIncome\" NumberFormat=\"FormatText Event\"><ValueIt" +
                "ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Cash\" DataFie" +
                "ld=\"CashIncome\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1D" +
                "ataColumn><C1DataColumn Level=\"0\" Caption=\"Cash Flow\" DataField=\"CashFlow\" Numbe" +
                "rFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
                "mn Level=\"0\" Caption=\"\" DataField=\"Null\"><ValueItems /><GroupInfo /></C1DataColu" +
                "mn><C1DataColumn Level=\"0\" Caption=\"E\" DataField=\"IsEasy\"><ValueItems Presentati" +
                "on=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"H\" " +
                "DataField=\"IsHard\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataCo" +
                "lumn><C1DataColumn Level=\"0\" Caption=\"N\" DataField=\"IsNoLend\"><ValueItems Presen" +
                "tation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=" +
                "\"T\" DataField=\"IsThreshold\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /><" +
                "/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Value\" DataField=\"ValueBorrowed\" " +
                "NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
                "aColumn Level=\"0\" Caption=\"Ratio\" DataField=\"ValueBorrowedRatio\" NumberFormat=\"F" +
                "ormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
                "0\" Caption=\"Value\" DataField=\"ValueLent\" NumberFormat=\"FormatText Event\"><ValueI" +
                "tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ratio\" DataF" +
                "ield=\"ValueLentRatio\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /" +
                "></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapp" +
                "er\"><Data>Style569{}Style566{}Style567{}Style564{AlignHorz:Near;}Style565{AlignH" +
                "orz:Near;}Style562{}Style563{}Style560{}Style561{}Style973{}Style972{}Style971{}" +
                "Style970{}Style977{}Style976{BackColor:Ivory;}Style975{AlignHorz:Far;BackColor:I" +
                "vory;}Style974{AlignHorz:Near;}Style979{}Style978{}Style351{}Style354{AlignHorz:" +
                "Near;}1000{AlignHorz:Near;BackColor:Window;}Style962{AlignHorz:Near;}Style963{Al" +
                "ignHorz:Near;}Style960{}Style961{}Style966{}Style967{}Style964{}Style965{}Style9" +
                "68{AlignHorz:Near;}Style969{AlignHorz:Near;}Style348{AlignHorz:Near;}Style344{}S" +
                "tyle915{AlignHorz:Near;}Style914{AlignHorz:Near;}Style917{}Style916{}Style911{}S" +
                "tyle667{Font:Verdana, 8.25pt;AlignHorz:Near;}Style913{}Style912{}Style919{}Style" +
                "918{}Style339{}Style334{}Style337{}Style787{}Style784{}Style904{}Style905{}Style" +
                "906{}Style907{}Style900{}Style901{}Style902{AlignHorz:Near;}Style903{AlignHorz:N" +
                "ear;}Style150{BackColor:WhiteSmoke;}Style908{AlignHorz:Center;}Style909{AlignHor" +
                "z:Center;BackColor:WhiteSmoke;}Style155{AlignHorz:Far;BackColor:WhiteSmoke;}Styl" +
                "e154{AlignHorz:Near;}Style157{}Style156{BackColor:WhiteSmoke;}Style322{}Style323" +
                "{}Style937{}Style936{}Style935{}Style934{}Style933{AlignHorz:Near;}Style932{Alig" +
                "nHorz:Near;}Style931{}Style930{}Style643{Font:Verdana, 8.25pt;AlignHorz:Near;}St" +
                "yle642{}Style939{AlignHorz:Near;}Style938{AlignHorz:Near;}Style310{AlignHorz:Nea" +
                "r;}Style926{AlignHorz:Near;}Style927{AlignHorz:Near;}Style924{}Style925{}Style92" +
                "2{}Style923{}Style920{AlignHorz:Near;}Style921{AlignHorz:Near;}Style928{}Style92" +
                "9{}Style580{}Style581{}Style582{AlignHorz:Near;}Style300{}Style301{}Style365{Ali" +
                "gnHorz:Near;}Style541{AlignHorz:Far;BackColor:Ivory;}Normal{Font:Verdana, 8.25pt" +
                ";}Style739{AlignHorz:Near;}Style738{}Style730{}Style733{AlignHorz:Near;}Style732" +
                "{}Style735{}Style734{AlignHorz:Near;}Style296{}Style297{}Style294{}Style295{}Sty" +
                "le292{AlignHorz:Near;}Style293{AlignHorz:Near;}Style290{}Style291{}Style298{Alig" +
                "nHorz:Near;}Style299{AlignHorz:Near;}Style134{}Style999{AlignHorz:Near;}Style998" +
                "{AlignHorz:Near;}Style995{}Style994{}Style997{}Style996{}Style278{}Style279{}Sty" +
                "le993{AlignHorz:Near;}Style992{AlignHorz:Near;}Style270{BackColor:Honeydew;}Styl" +
                "e271{}Style272{}Style273{}Style274{Font:Verdana, 8.25pt;AlignHorz:Near;}Style275" +
                "{AlignHorz:Far;BackColor:Honeydew;}Style276{BackColor:Honeydew;}Style277{}Style9" +
                "88{}Style989{}Style984{}Style985{}Style986{AlignHorz:Near;}Style987{AlignHorz:Ne" +
                "ar;}Style980{AlignHorz:Near;}Style981{AlignHorz:Near;}FilterBar{AlignVert:Center" +
                ";}Style983{}Style283{}Style282{BackColor:WhiteSmoke;}Style281{AlignHorz:Far;Back" +
                "Color:WhiteSmoke;}Style280{Font:Verdana, 8.25pt;AlignHorz:Near;}Style289{}Style2" +
                "88{BackColor:WhiteSmoke;}Style779{}Style778{}Style258{BackColor:Ivory;}Style259{" +
                "}Style252{}Style253{}Style250{AlignHorz:Near;}Style251{AlignHorz:Near;}Style256{" +
                "Font:Verdana, 8.25pt;AlignHorz:Near;}Style257{AlignHorz:Far;BackColor:Ivory;}Sty" +
                "le254{}Style255{}Style511{AlignHorz:Near;}Style510{AlignHorz:Near;}Style514{}Sty" +
                "le768{}Style769{AlignHorz:Near;}Style269{AlignHorz:Far;BackColor:Honeydew;}Style" +
                "268{Font:Verdana, 8.25pt;AlignHorz:Near;}Style261{}Style260{}Style263{AlignHorz:" +
                "Far;BackColor:Ivory;}Style262{Font:Verdana, 8.25pt;AlignHorz:Near;}Style265{}Sty" +
                "le264{BackColor:Ivory;}Style267{}Style266{}Style759{}Style758{AlignHorz:Near;}St" +
                "yle757{AlignHorz:Near;}Style238{AlignHorz:Near;}Style239{AlignHorz:Near;}Style23" +
                "4{}Style235{}Style236{}Style237{}Style230{}Style231{}Style232{AlignHorz:Near;}St" +
                "yle233{AlignHorz:Near;}Style533{}Style532{}Style531{}Style530{}Style748{}Style74" +
                "9{}Style746{AlignHorz:Near;}Style747{}Style249{}Style248{}Style243{}Style242{}St" +
                "yle241{}Style240{}Style247{}Style246{}Style245{AlignHorz:Near;}Style244{AlignHor" +
                "z:Near;}Style528{AlignHorz:Near;}Style529{AlignHorz:Near;}Style522{AlignHorz:Nea" +
                "r;}Style523{AlignHorz:Near;}Style520{}Style521{}Style218{}Style219{}Style216{}St" +
                "yle217{}Style214{AlignHorz:Near;}Style215{AlignHorz:Near;}Style212{}Style213{}St" +
                "yle210{}Style211{}Style410{}Style559{AlignHorz:Near;}Style413{AlignHorz:Near;}St" +
                "yle553{AlignHorz:Near;}Style229{}Style228{}Style225{}Style224{}Style227{AlignHor" +
                "z:Near;}Style226{AlignHorz:Near;}Style221{AlignHorz:Near;}Style220{AlignHorz:Nea" +
                "r;}Style223{}Style222{}Style1047{AlignHorz:Near;}Style692{AlignHorz:Near;}Style6" +
                "93{}Style690{}Style691{AlignHorz:Near;}Style696{}Style697{AlignHorz:Near;}Style6" +
                "94{}Style695{}Style910{BackColor:WhiteSmoke;}Selected{ForeColor:HighlightText;Ba" +
                "ckColor:Highlight;}Style699{}Style433{}Style209{AlignHorz:Near;}Style208{AlignHo" +
                "rz:Near;}Style207{}Style206{}Style205{}Style204{}Style203{AlignHorz:Near;}Style2" +
                "02{AlignHorz:Near;}Style201{}Style200{}Style683{}Style682{}Style681{BackColor:Wh" +
                "iteSmoke;}Style680{AlignHorz:Center;BackColor:WhiteSmoke;}Style687{BackColor:Whi" +
                "teSmoke;}Style686{AlignHorz:Near;BackColor:WhiteSmoke;}Style685{Font:Verdana, 8." +
                "25pt;AlignHorz:Near;}Style684{}Style689{}Style688{}Style516{AlignHorz:Near;}Styl" +
                "e394{}Style490{}Style491{}Style492{AlignHorz:Near;}Style493{AlignHorz:Near;}Styl" +
                "e494{}Style495{}Style496{}Style497{}Style498{AlignHorz:Near;}Style499{AlignHorz:" +
                "Near;}Style666{}Style661{Font:Verdana, 8.25pt;AlignHorz:Near;}Style638{}Style639" +
                "{}Style630{}Style631{Font:Verdana, 8.25pt;}Style632{}Style633{}Style634{Font:Ver" +
                "dana, 8.25pt;}Style635{}Style636{}Style637{}Style374{}Style789{AlignHorz:Near;}O" +
                "ddRow{}Style481{}Style480{}Style483{AlignHorz:Near;}Style482{}Style485{}Style618" +
                "{AlignHorz:Near;}Style487{AlignHorz:Near;}Style486{AlignHorz:Near;}Style489{}Sty" +
                "le488{}Style364{}Style612{AlignHorz:Near;}Style613{AlignHorz:Near;}Style610{}Sty" +
                "le611{}Style616{}Style617{}Style614{}Style615{}Style991{}Style990{}Style359{Alig" +
                "nHorz:Near;}Style629{}Style628{}Style621{}Style620{}Style623{}HighlightRow{ForeC" +
                "olor:HighlightText;BackColor:Highlight;}Style625{AlignHorz:Near;}Style624{AlignH" +
                "orz:Near;}Style627{}Style626{}Style982{}Style153{}Style152{}Style678{}Style679{F" +
                "ont:Verdana, 8.25pt;AlignHorz:Center;}Style674{AlignHorz:Center;BackColor:WhiteS" +
                "moke;}Style675{BackColor:WhiteSmoke;}Style676{}Style677{}Style670{}Style671{}Sty" +
                "le672{}Style673{Font:Verdana, 8.25pt;AlignHorz:Center;}Style148{AlignHorz:Near;}" +
                "Style149{AlignHorz:Far;BackColor:WhiteSmoke;}Style140{}Style609{}Style608{}Style" +
                "603{}Style602{}Style601{AlignHorz:Near;}Style600{AlignHorz:Near;}Style607{AlignH" +
                "orz:Near;}Style606{AlignHorz:Near;}Style605{}Style604{}Heading{Wrap:True;BackCol" +
                "or:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Sty" +
                "le658{}Style659{}Style656{AlignHorz:Near;BackColor:WhiteSmoke;}Style657{BackColo" +
                "r:WhiteSmoke;}Style654{}Style655{Font:Verdana, 8.25pt;AlignHorz:Near;}Style652{}" +
                "Style653{}Style650{AlignHorz:Near;BackColor:WhiteSmoke;}Style651{BackColor:White" +
                "Smoke;}Style484{}Style418{}Style419{AlignHorz:Near;}Style669{BackColor:WhiteSmok" +
                "e;}Style668{AlignHorz:Near;BackColor:WhiteSmoke;}Style411{}Style412{}Style665{}S" +
                "tyle664{}Style415{}Style416{}Style417{}Style660{}Style663{BackColor:WhiteSmoke;}" +
                "Style662{AlignHorz:Near;BackColor:WhiteSmoke;}Style74{}Style75{}Group{AlignVert:" +
                "Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style438{AlignHorz:Near;}S" +
                "tyle439{}Style432{AlignHorz:Near;}Style649{Font:Verdana, 8.25pt;AlignHorz:Near;}" +
                "Style648{}Style431{AlignHorz:Near;}Style9{}Style437{AlignHorz:Near;}Style644{Ali" +
                "gnHorz:Near;BackColor:WhiteSmoke;}Style435{}Style5{}Style4{}Style7{}Style6{}Styl" +
                "e1{Font:Verdana, 8.25pt;}Style517{AlignHorz:Near;}Style3{Font:Verdana, 8.25pt;}S" +
                "tyle2{Font:Verdana, 8.25pt;}Style93{}Style409{BackColor:Honeydew;}Style408{Align" +
                "Horz:Far;BackColor:Honeydew;}Style401{Font:Verdana, 8.25pt;AlignHorz:Near;}Style" +
                "400{}Style403{BackColor:Honeydew;}Style402{AlignHorz:Far;BackColor:Honeydew;}Sty" +
                "le405{}Style404{}Style407{Font:Verdana, 8.25pt;AlignHorz:Near;}Style406{}Style45" +
                "8{}Style459{}Style454{}Style455{AlignHorz:Near;}Style456{AlignHorz:Near;}Style45" +
                "7{}Style450{AlignHorz:Near;}Style451{}Style452{}Style453{}Style429{}Style428{}St" +
                "yle423{}Style422{}Style421{}Style420{AlignHorz:Near;}Style427{}Style426{AlignHor" +
                "z:Near;}Style425{AlignHorz:Near;}Style424{}Style850{BackColor:WhiteSmoke;}Style8" +
                "51{}Style852{}Style853{}Style854{AlignHorz:Near;}Style855{AlignHorz:Near;}Style8" +
                "56{}Style857{}Style858{}Style859{}Style478{}Style479{}Style476{}Style477{Font:Ve" +
                "rdana, 8.25pt;}Style474{Font:Verdana, 8.25pt;}Style475{}Style472{}Style473{}Styl" +
                "e470{}Style471{}Style449{AlignHorz:Near;}Style448{}Style445{}Style444{AlignHorz:" +
                "Near;}Style447{}Style446{}Style441{}Style440{}Style443{AlignHorz:Near;}Style442{" +
                "}Style872{AlignHorz:Near;}Style873{AlignHorz:Near;}Style870{}Style871{}Style876{" +
                "}Style877{}Style874{}Style875{}Style878{AlignHorz:Center;}Style879{AlignHorz:Cen" +
                "ter;BackColor:WhiteSmoke;}Style698{AlignHorz:Near;}Style841{}Style840{}Style843{" +
                "AlignHorz:Near;}Style842{AlignHorz:Near;}Style845{}Style844{}Style847{}Style846{" +
                "}Style849{AlignHorz:Center;BackColor:WhiteSmoke;}Style848{AlignHorz:Center;}Styl" +
                "e469{}Style468{AlignHorz:Near;}Style467{AlignHorz:Near;}Style466{}Style465{}Styl" +
                "e464{}Style463{}Style462{AlignHorz:Near;}Style461{AlignHorz:Near;}Style460{}Styl" +
                "e1051{}Style1050{}Style1053{AlignHorz:Near;}Style1052{AlignHorz:Near;}Style1055{" +
                "}Style1054{}Style1057{}Style814{BackColor:WhiteSmoke;}Style815{}Style816{}Style8" +
                "17{}Style810{}Style811{}Style812{AlignHorz:Near;}Style813{AlignHorz:Near;BackCol" +
                "or:WhiteSmoke;}Style818{AlignHorz:Center;}Style819{AlignHorz:Center;BackColor:Wh" +
                "iteSmoke;}Style82{AlignHorz:Near;}Style1048{}Style1049{}Style1040{AlignHorz:Near" +
                ";}Style1041{AlignHorz:Far;BackColor:Honeydew;}Style1042{BackColor:Honeydew;}Styl" +
                "e1043{}Style1044{}Style1045{}Style1046{AlignHorz:Near;}Style863{}Style862{}Style" +
                "861{AlignHorz:Near;}Style860{AlignHorz:Near;}Style867{AlignHorz:Near;}Style866{A" +
                "lignHorz:Near;}Style865{}Style864{}Style869{}Style868{}Style397{BackColor:Honeyd" +
                "ew;}Style396{AlignHorz:Far;BackColor:Honeydew;}Style395{Font:Verdana, 8.25pt;Ali" +
                "gnHorz:Near;}EvenRow{BackColor:Aqua;}Style393{}Style392{}Style391{}Style390{Alig" +
                "nHorz:Near;}Style399{}Style398{}Style836{AlignHorz:Near;}Style837{AlignHorz:Near" +
                ";}Style834{}Style835{}Style832{}Style833{}Style830{AlignHorz:Near;}Style831{Alig" +
                "nHorz:Near;}Style838{}Style839{}Style387{}Style384{AlignHorz:Near;}Style385{}Sty" +
                "le382{}Style383{AlignHorz:Near;}Style380{}Style381{}Style388{}Style389{AlignHorz" +
                ":Near;}Style805{}Style804{}Style806{AlignHorz:Near;}Style801{AlignHorz:Near;}Sty" +
                "le800{AlignHorz:Near;}Style803{}Style802{}Style809{}Style808{BackColor:Ivory;}St" +
                "yle379{}Style378{AlignHorz:Near;}Footer{BackColor:Window;}Style1019{}Style1018{}" +
                "Style371{AlignHorz:Near;}Style370{}Style373{}Style372{AlignHorz:Near;}Style375{}" +
                "Style1016{AlignHorz:Near;}Style377{AlignHorz:Near;}Style376{}Style1013{}Style101" +
                "2{BackColor:Honeydew;}Style32{}Style368{}Style369{}Style1008{}Style1009{}Style36" +
                "0{AlignHorz:Near;}Style361{}Style362{}Style1005{AlignHorz:Near;}Style1006{}Style" +
                "1007{}Style366{AlignHorz:Near;}Style367{}Style1002{}Style827{}Style826{}Style825" +
                "{AlignHorz:Near;}Style824{AlignHorz:Near;}Style823{}Style822{}Style821{}Style820" +
                "{BackColor:WhiteSmoke;}Style829{}Style828{}Style8{}Style358{}Style353{AlignHorz:" +
                "Near;}Style352{}Style1037{}Style350{}Style357{}Style356{}Style355{}Style1032{}St" +
                "yle1031{}Style1030{}Style195{}Style194{}Style197{AlignHorz:Near;}Style196{AlignH" +
                "orz:Near;}Style191{AlignHorz:Near;}Style190{AlignHorz:Near;}Style349{}Style192{}" +
                "Style342{AlignHorz:Near;}Style343{}Style340{}Style341{AlignHorz:Near;}Style346{}" +
                "Style347{AlignHorz:Near;}Style1022{AlignHorz:Near;}Style345{}Style1020{}Style102" +
                "1{}Style184{AlignHorz:Near;}Style185{AlignHorz:Near;}Style186{}Style187{}Style18" +
                "0{}Style181{}Style182{}Style183{}Style338{}Style335{AlignHorz:Near;}Style188{}St" +
                "yle189{}Style336{AlignHorz:Near;}Style331{}Style330{AlignHorz:Near;}Style333{}St" +
                "yle332{}Style898{}Style899{}Style894{}Style895{}Style896{AlignHorz:Near;}Style89" +
                "7{AlignHorz:Near;}Style890{AlignHorz:Near;}Style891{AlignHorz:Near;}Style892{}St" +
                "yle893{}Style328{}Style329{AlignHorz:Near;}Style324{}Style325{}Style326{AlignHor" +
                "z:Near;}Style327{}Style320{Font:Verdana, 8.25pt;}Style321{}Style1011{AlignHorz:F" +
                "ar;BackColor:Honeydew;}Style1010{AlignHorz:Near;}Style89{AlignHorz:Near;}Style88" +
                "{AlignHorz:Near;}Style319{}Style318{}Style317{Font:Verdana, 8.25pt;}Style316{}St" +
                "yle315{}Style314{}Style313{}Style312{}Style311{AlignHorz:Near;}Style1000{}Style1" +
                "001{}Style1003{}Style414{AlignHorz:Near;}Style159{}Style158{}Style151{}Style308{" +
                "}Style309{}Style306{}Style307{}Style304{AlignHorz:Near;}Style305{AlignHorz:Near;" +
                "}Style302{}Style303{}Style1035{AlignHorz:Near;}Style1034{AlignHorz:Near;}Style10" +
                "33{}Style889{}Style888{}Style885{AlignHorz:Near;}Style793{}Style792{}Style791{}S" +
                "tyle790{}Style797{}Style796{}Style795{AlignHorz:Near;}Style794{AlignHorz:Near;}S" +
                "tyle799{}Style798{}Style141{}Style142{AlignHorz:Near;}Style143{AlignHorz:Far;Bac" +
                "kColor:WhiteSmoke;}Style144{BackColor:WhiteSmoke;}Style145{}Style146{}Style147{}" +
                "Style1026{}Style1027{}Style1024{}Style1025{}Style1023{AlignHorz:Near;}Style430{}" +
                "Style436{}Style434{}Style363{}Style782{AlignHorz:Near;}Style783{}Style780{}Style" +
                "781{AlignHorz:Near;}Style786{}Style179{AlignHorz:Near;}Style178{AlignHorz:Near;}" +
                "Style785{}Style788{AlignHorz:Near;}Style173{AlignHorz:Near;}Style172{AlignHorz:N" +
                "ear;}Style171{}Style170{}Style177{}Style176{}Style175{}Style174{}Style40{AlignHo" +
                "rz:Near;}Style43{}Style46{AlignHorz:Near;}Style168{}Style169{AlignHorz:Near;}Sty" +
                "le162{}Style163{Font:Verdana, 8.25pt;}Style160{Font:Verdana, 8.25pt;}Style161{}S" +
                "tyle166{}Style167{}Style164{}Style165{}Style57{}Style647{}Style646{}Style645{Bac" +
                "kColor:WhiteSmoke;}Style119{AlignHorz:Near;}Style118{AlignHorz:Near;}Style641{}S" +
                "tyle640{AlignHorz:Near;}Style115{}Style114{}Style117{}Style116{}Style111{}Style1" +
                "10{}Style113{AlignHorz:Near;}Style112{AlignHorz:Near;}Style108{}Style109{}Style1" +
                "04{}Style105{}Style106{AlignHorz:Near;}Style107{AlignHorz:Near;}Style100{AlignHo" +
                "rz:Near;}Style101{AlignHorz:Near;}Style102{}Style103{}Style1038{}Style1036{}Styl" +
                "e591{}Style590{}Style593{}Style592{}Style595{AlignHorz:Near;}Style594{AlignHorz:" +
                "Near;}Style597{}Style596{}Style599{}Style598{}Style139{}Style138{BackColor:White" +
                "Smoke;}Style137{AlignHorz:Far;BackColor:WhiteSmoke;}Style136{AlignHorz:Near;}Sty" +
                "le135{}RecordSelector{AlignImage:Center;}Style133{}Style132{}Style131{AlignHorz:" +
                "Near;}Style130{AlignHorz:Near;}Style1028{AlignHorz:Near;}Style1029{AlignHorz:Nea" +
                "r;}Style85{}Style84{}Style87{}Style86{}Style81{}Style80{}Style83{AlignHorz:Near;" +
                "}Style583{AlignHorz:Near;}Style584{}Style585{}Style586{}Style587{}Style588{Align" +
                "Horz:Near;}Style589{AlignHorz:Near;}Style731{}Style128{}Style129{}Style126{}Styl" +
                "e127{}Style124{AlignHorz:Near;}Style737{}Style736{}Style123{}Style120{}Style121{" +
                "}Style94{AlignHorz:Near;}Style95{AlignHorz:Near;}Style96{}Style97{}Style90{}Styl" +
                "e91{}Style92{}Style728{AlignHorz:Near;}Style729{}Style98{}Style99{}Style720{}Sty" +
                "le721{AlignHorz:Near;}Style722{AlignHorz:Near;}Style723{}Style724{}Style725{}Sty" +
                "le726{}Style727{AlignHorz:Near;}Style1015{}Style1014{}Style1017{AlignHorz:Near;}" +
                "Style719{}Style718{}Style1056{}Style713{}Style712{}Style711{}Style710{AlignHorz:" +
                "Near;}Style717{}Style716{AlignHorz:Near;}Style715{AlignHorz:Near;}Style714{}Styl" +
                "e1004{AlignHorz:Near;}Style37{}Style708{}Style709{AlignHorz:Near;}Style33{}Style" +
                "30{}Style31{}Style702{}Style703{AlignHorz:Near;}Style700{}Style701{}Style706{}St" +
                "yle707{}Style704{AlignHorz:Near;}Style705{}Style1039{}Style49{}Style48{}Style41{" +
                "AlignHorz:Near;}Style125{AlignHorz:Near;}Style122{}Style42{}Style45{}Style44{}St" +
                "yle47{AlignHorz:Near;}Style775{AlignHorz:Near;}Style774{}Style777{}Style776{Alig" +
                "nHorz:Near;}Style771{}Style770{AlignHorz:Near;}Style773{}Style772{}Style287{Alig" +
                "nHorz:Far;BackColor:WhiteSmoke;}Style286{Font:Verdana, 8.25pt;AlignHorz:Near;}St" +
                "yle285{}Style284{}Style58{AlignHorz:Near;}Style59{AlignHorz:Near;}Style50{}Style" +
                "51{}Style52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}S" +
                "tyle764{AlignHorz:Near;}Style765{}Style766{}Style767{}Style760{}Style761{}Style7" +
                "62{}Style763{AlignHorz:Near;}Style944{AlignHorz:Near;}Style69{}Style68{}Style63{" +
                "}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{Al" +
                "ignHorz:Near;}Style756{}Style755{}Style754{}Style753{}Style752{AlignHorz:Near;}S" +
                "tyle751{AlignHorz:Near;}Style750{}Inactive{ForeColor:InactiveCaptionText;BackCol" +
                "or:InactiveCaption;}Style78{}Style79{}Style519{}Style518{}Style72{}Style73{}Styl" +
                "e70{AlignHorz:Near;}Style71{AlignHorz:Near;}Style76{AlignHorz:Near;}Style77{Alig" +
                "nHorz:Near;}Style513{}Style512{}Style515{}Style744{}Style745{AlignHorz:Near;}Sty" +
                "le742{}Style743{}Style740{AlignHorz:Near;}Style741{}Style508{}Style509{}Style500" +
                "{}Style501{}Style502{}Style503{}Style504{AlignHorz:Near;}Style505{AlignHorz:Near" +
                ";}Style506{}Style507{}Style18{}Style19{}Style539{}Style538{}Style14{}Style15{}St" +
                "yle16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}S" +
                "tyle12{}Style13{}Style537{}Style536{BackColor:Ivory;}Style535{AlignHorz:Far;Back" +
                "Color:Ivory;}Style534{Font:Verdana, 8.25pt;AlignHorz:Near;}Style884{AlignHorz:Ne" +
                "ar;}Style887{}Style886{}Style881{}Style880{BackColor:WhiteSmoke;}Style883{}Style" +
                "882{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27{}Style26{}Style25{}" +
                "Style24{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style20{}Style" +
                "526{}Style527{}Style524{}Style525{}Style38{}Style39{}Style36{}Style558{AlignHorz" +
                ":Near;}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style555{}Style554{}Style" +
                "557{}Style556{}Style551{}Style550{}Style619{AlignHorz:Near;}Style552{AlignHorz:N" +
                "ear;}Style386{}Style548{BackColor:Ivory;}Style549{}Style544{}Style545{}Style546{" +
                "Font:Verdana, 8.25pt;AlignHorz:Near;}Style547{AlignHorz:Far;BackColor:Ivory;}Sty" +
                "le540{Font:Verdana, 8.25pt;AlignHorz:Near;}Caption{AlignHorz:Center;}Style542{Ba" +
                "ckColor:Ivory;}Style543{}Style622{}Style951{AlignHorz:Near;}Style950{AlignHorz:N" +
                "ear;}Style953{}Style952{}Style955{}Style954{}Style957{AlignHorz:Near;}Style956{A" +
                "lignHorz:Near;}Style959{}Style958{}Editor{}Style579{}Style578{}Style577{AlignHor" +
                "z:Near;}Style576{AlignHorz:Near;}Style575{}Style574{}Style573{}Style572{}Style57" +
                "1{AlignHorz:Near;}Style570{AlignHorz:Near;}Style940{}Style941{}Style942{}Style94" +
                "3{}Style193{}Style945{AlignHorz:Far;BackColor:Ivory;}Style946{BackColor:Ivory;}S" +
                "tyle947{}Style948{}Style949{}Style199{}Style198{}Style568{}</Data></Styles><Spli" +
                "ts><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"None\" AllowColSele" +
                "ct=\"False\" Name=\"Split[0,0]\" AllowRowSizing=\"None\" AllowVerticalSizing=\"False\" C" +
                "aptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" FilterBar=\"Tr" +
                "ue\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" " +
                "VerticalScrollGroup=\"1\" MinWidth=\"100\" HorizontalScrollGroup=\"5\"><Caption>Summar" +
                "y</Caption><SplitSize>12</SplitSize><SplitSizeMode>NumberOfColumns</SplitSizeMod" +
                "e><CaptionStyle parent=\"Style2\" me=\"Style640\" /><EditorStyle parent=\"Editor\" me=" +
                "\"Style632\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style638\" /><FilterBarStyle pare" +
                "nt=\"FilterBar\" me=\"Style787\" /><FooterStyle parent=\"Footer\" me=\"Style634\" /><Gro" +
                "upStyle parent=\"Group\" me=\"Style642\" /><HeadingStyle parent=\"Heading\" me=\"Style6" +
                "33\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style637\" /><InactiveStyle pa" +
                "rent=\"Inactive\" me=\"Style636\" /><OddRowStyle parent=\"OddRow\" me=\"Style639\" /><Re" +
                "cordSelectorStyle parent=\"RecordSelector\" me=\"Style641\" /><SelectedStyle parent=" +
                "\"Selected\" me=\"Style635\" /><Style parent=\"Normal\" me=\"Style631\" /><internalCols>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style643\" /><Style parent=\"" +
                "Style631\" me=\"Style644\" /><FooterStyle parent=\"Style634\" me=\"Style645\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style646\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style648\" /><GroupFooterStyle parent=\"Style631\" me=\"Style647\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>0</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style649\" /><Style parent=\"" +
                "Style631\" me=\"Style650\" /><FooterStyle parent=\"Style634\" me=\"Style651\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style652\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style654\" /><GroupFooterStyle parent=\"Style631\" me=\"Style653\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>1</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style655\" /><Style parent=\"" +
                "Style631\" me=\"Style656\" /><FooterStyle parent=\"Style634\" me=\"Style657\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style658\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style660\" /><GroupFooterStyle parent=\"Style631\" me=\"Style659\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>2</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style661\" /><Style parent=\"" +
                "Style631\" me=\"Style662\" /><FooterStyle parent=\"Style634\" me=\"Style663\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style664\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style666\" /><GroupFooterStyle parent=\"Style631\" me=\"Style665\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>3</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style667\" /><Style parent=\"" +
                "Style631\" me=\"Style668\" /><FooterStyle parent=\"Style634\" me=\"Style669\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style670\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style672\" /><GroupFooterStyle parent=\"Style631\" me=\"Style671\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>75</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>4</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style673\" /><Style parent=\"" +
                "Style631\" me=\"Style674\" /><FooterStyle parent=\"Style634\" me=\"Style675\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style676\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style678\" /><GroupFooterStyle parent=\"Style631\" me=\"Style677\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>25</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>5</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style679\" /><Style parent=\"" +
                "Style631\" me=\"Style680\" /><FooterStyle parent=\"Style634\" me=\"Style681\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style682\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style684\" /><GroupFooterStyle parent=\"Style631\" me=\"Style683\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>20</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>6</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style685\" /><Style parent=\"" +
                "Style631\" me=\"Style686\" /><FooterStyle parent=\"Style634\" me=\"Style687\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style688\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style690\" /><GroupFooterStyle parent=\"Style631\" me=\"Style689\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>65</Width><Height>1" +
                "5</Height><FooterDivider>False</FooterDivider><DCIdx>7</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style818\" /><Style parent=\"" +
                "Style631\" me=\"Style819\" /><FooterStyle parent=\"Style634\" me=\"Style820\" /><Editor" +
                "Style parent=\"Style632\" me=\"Style821\" /><GroupHeaderStyle parent=\"Style631\" me=\"" +
                "Style823\" /><GroupFooterStyle parent=\"Style631\" me=\"Style822\" /><Visible>True</V" +
                "isible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</Width><Height>15<" +
                "/Height><FooterDivider>False</FooterDivider><DCIdx>25</DCIdx></C1DisplayColumn><" +
                "C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style848\" /><Style parent=\"S" +
                "tyle631\" me=\"Style849\" /><FooterStyle parent=\"Style634\" me=\"Style850\" /><EditorS" +
                "tyle parent=\"Style632\" me=\"Style851\" /><GroupHeaderStyle parent=\"Style631\" me=\"S" +
                "tyle853\" /><GroupFooterStyle parent=\"Style631\" me=\"Style852\" /><Visible>True</Vi" +
                "sible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</Width><Height>15</" +
                "Height><FooterDivider>False</FooterDivider><DCIdx>26</DCIdx></C1DisplayColumn><C" +
                "1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style878\" /><Style parent=\"St" +
                "yle631\" me=\"Style879\" /><FooterStyle parent=\"Style634\" me=\"Style880\" /><EditorSt" +
                "yle parent=\"Style632\" me=\"Style881\" /><GroupHeaderStyle parent=\"Style631\" me=\"St" +
                "yle883\" /><GroupFooterStyle parent=\"Style631\" me=\"Style882\" /><Visible>True</Vis" +
                "ible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</Width><Height>15</H" +
                "eight><FooterDivider>False</FooterDivider><DCIdx>27</DCIdx></C1DisplayColumn><C1" +
                "DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style908\" /><Style parent=\"Sty" +
                "le631\" me=\"Style909\" /><FooterStyle parent=\"Style634\" me=\"Style910\" /><EditorSty" +
                "le parent=\"Style632\" me=\"Style911\" /><GroupHeaderStyle parent=\"Style631\" me=\"Sty" +
                "le913\" /><GroupFooterStyle parent=\"Style631\" me=\"Style912\" /><Visible>True</Visi" +
                "ble><ColumnDivider>Gainsboro,None</ColumnDivider><Width>16</Width><Height>15</He" +
                "ight><FooterDivider>False</FooterDivider><DCIdx>28</DCIdx></C1DisplayColumn><C1D" +
                "isplayColumn><HeadingStyle parent=\"Style633\" me=\"Style691\" /><Style parent=\"Styl" +
                "e631\" me=\"Style692\" /><FooterStyle parent=\"Style634\" me=\"Style693\" /><EditorStyl" +
                "e parent=\"Style632\" me=\"Style694\" /><GroupHeaderStyle parent=\"Style631\" me=\"Styl" +
                "e696\" /><GroupFooterStyle parent=\"Style631\" me=\"Style695\" /><ColumnDivider>DarkG" +
                "ray,Single</ColumnDivider><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><" +
                "C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style697\" /><Style parent=\"S" +
                "tyle631\" me=\"Style698\" /><FooterStyle parent=\"Style634\" me=\"Style699\" /><EditorS" +
                "tyle parent=\"Style632\" me=\"Style700\" /><GroupHeaderStyle parent=\"Style631\" me=\"S" +
                "tyle702\" /><GroupFooterStyle parent=\"Style631\" me=\"Style701\" /><ColumnDivider>Da" +
                "rkGray,Single</ColumnDivider><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColum" +
                "n><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style703\" /><Style parent" +
                "=\"Style631\" me=\"Style704\" /><FooterStyle parent=\"Style634\" me=\"Style705\" /><Edit" +
                "orStyle parent=\"Style632\" me=\"Style706\" /><GroupHeaderStyle parent=\"Style631\" me" +
                "=\"Style708\" /><GroupFooterStyle parent=\"Style631\" me=\"Style707\" /><ColumnDivider" +
                ">DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>10</DCIdx></C1DisplayC" +
                "olumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style709\" /><Style pa" +
                "rent=\"Style631\" me=\"Style710\" /><FooterStyle parent=\"Style634\" me=\"Style711\" /><" +
                "EditorStyle parent=\"Style632\" me=\"Style712\" /><GroupHeaderStyle parent=\"Style631" +
                "\" me=\"Style714\" /><GroupFooterStyle parent=\"Style631\" me=\"Style713\" /><ColumnDiv" +
                "ider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>11</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style715\" /><Styl" +
                "e parent=\"Style631\" me=\"Style716\" /><FooterStyle parent=\"Style634\" me=\"Style717\"" +
                " /><EditorStyle parent=\"Style632\" me=\"Style718\" /><GroupHeaderStyle parent=\"Styl" +
                "e631\" me=\"Style720\" /><GroupFooterStyle parent=\"Style631\" me=\"Style719\" /><Colum" +
                "nDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>12</DCIdx></C1" +
                "DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style721\" /><" +
                "Style parent=\"Style631\" me=\"Style722\" /><FooterStyle parent=\"Style634\" me=\"Style" +
                "723\" /><EditorStyle parent=\"Style632\" me=\"Style724\" /><GroupHeaderStyle parent=\"" +
                "Style631\" me=\"Style726\" /><GroupFooterStyle parent=\"Style631\" me=\"Style725\" /><C" +
                "olumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>13</DCIdx>" +
                "</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style727\"" +
                " /><Style parent=\"Style631\" me=\"Style728\" /><FooterStyle parent=\"Style634\" me=\"S" +
                "tyle729\" /><EditorStyle parent=\"Style632\" me=\"Style730\" /><GroupHeaderStyle pare" +
                "nt=\"Style631\" me=\"Style732\" /><GroupFooterStyle parent=\"Style631\" me=\"Style731\" " +
                "/><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>14</DC" +
                "Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style" +
                "733\" /><Style parent=\"Style631\" me=\"Style734\" /><FooterStyle parent=\"Style634\" m" +
                "e=\"Style735\" /><EditorStyle parent=\"Style632\" me=\"Style736\" /><GroupHeaderStyle " +
                "parent=\"Style631\" me=\"Style738\" /><GroupFooterStyle parent=\"Style631\" me=\"Style7" +
                "37\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>15" +
                "</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"S" +
                "tyle739\" /><Style parent=\"Style631\" me=\"Style740\" /><FooterStyle parent=\"Style63" +
                "4\" me=\"Style741\" /><EditorStyle parent=\"Style632\" me=\"Style742\" /><GroupHeaderSt" +
                "yle parent=\"Style631\" me=\"Style744\" /><GroupFooterStyle parent=\"Style631\" me=\"St" +
                "yle743\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCId" +
                "x>16</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" m" +
                "e=\"Style745\" /><Style parent=\"Style631\" me=\"Style746\" /><FooterStyle parent=\"Sty" +
                "le634\" me=\"Style747\" /><EditorStyle parent=\"Style632\" me=\"Style748\" /><GroupHead" +
                "erStyle parent=\"Style631\" me=\"Style750\" /><GroupFooterStyle parent=\"Style631\" me" +
                "=\"Style749\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><" +
                "DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style63" +
                "3\" me=\"Style751\" /><Style parent=\"Style631\" me=\"Style752\" /><FooterStyle parent=" +
                "\"Style634\" me=\"Style753\" /><EditorStyle parent=\"Style632\" me=\"Style754\" /><Group" +
                "HeaderStyle parent=\"Style631\" me=\"Style756\" /><GroupFooterStyle parent=\"Style631" +
                "\" me=\"Style755\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heig" +
                "ht><DCIdx>18</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
                "le633\" me=\"Style757\" /><Style parent=\"Style631\" me=\"Style758\" /><FooterStyle par" +
                "ent=\"Style634\" me=\"Style759\" /><EditorStyle parent=\"Style632\" me=\"Style760\" /><G" +
                "roupHeaderStyle parent=\"Style631\" me=\"Style762\" /><GroupFooterStyle parent=\"Styl" +
                "e631\" me=\"Style761\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</" +
                "Height><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
                "\"Style633\" me=\"Style763\" /><Style parent=\"Style631\" me=\"Style764\" /><FooterStyle" +
                " parent=\"Style634\" me=\"Style765\" /><EditorStyle parent=\"Style632\" me=\"Style766\" " +
                "/><GroupHeaderStyle parent=\"Style631\" me=\"Style768\" /><GroupFooterStyle parent=\"" +
                "Style631\" me=\"Style767\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>" +
                "15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style633\" me=\"Style769\" /><Style parent=\"Style631\" me=\"Style770\" /><FooterS" +
                "tyle parent=\"Style634\" me=\"Style771\" /><EditorStyle parent=\"Style632\" me=\"Style7" +
                "72\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style774\" /><GroupFooterStyle pare" +
                "nt=\"Style631\" me=\"Style773\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
                "ght>15</Height><DCIdx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
                " parent=\"Style633\" me=\"Style775\" /><Style parent=\"Style631\" me=\"Style776\" /><Foo" +
                "terStyle parent=\"Style634\" me=\"Style777\" /><EditorStyle parent=\"Style632\" me=\"St" +
                "yle778\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style780\" /><GroupFooterStyle " +
                "parent=\"Style631\" me=\"Style779\" /><ColumnDivider>DarkGray,Single</ColumnDivider>" +
                "<Height>15</Height><DCIdx>22</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
                "tyle parent=\"Style633\" me=\"Style781\" /><Style parent=\"Style631\" me=\"Style782\" />" +
                "<FooterStyle parent=\"Style634\" me=\"Style783\" /><EditorStyle parent=\"Style632\" me" +
                "=\"Style784\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style786\" /><GroupFooterSt" +
                "yle parent=\"Style631\" me=\"Style785\" /><ColumnDivider>DarkGray,Single</ColumnDivi" +
                "der><Height>15</Height><DCIdx>23</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
                "ingStyle parent=\"Style633\" me=\"Style788\" /><Style parent=\"Style631\" me=\"Style789" +
                "\" /><FooterStyle parent=\"Style634\" me=\"Style790\" /><EditorStyle parent=\"Style632" +
                "\" me=\"Style791\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style793\" /><GroupFoot" +
                "erStyle parent=\"Style631\" me=\"Style792\" /><ColumnDivider>DarkGray,Single</Column" +
                "Divider><Height>15</Height><DCIdx>24</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
                "HeadingStyle parent=\"Style633\" me=\"Style938\" /><Style parent=\"Style631\" me=\"Styl" +
                "e939\" /><FooterStyle parent=\"Style634\" me=\"Style940\" /><EditorStyle parent=\"Styl" +
                "e632\" me=\"Style941\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style943\" /><Group" +
                "FooterStyle parent=\"Style631\" me=\"Style942\" /><ColumnDivider>DarkGray,Single</Co" +
                "lumnDivider><Height>15</Height><DCIdx>29</DCIdx></C1DisplayColumn><C1DisplayColu" +
                "mn><HeadingStyle parent=\"Style633\" me=\"Style968\" /><Style parent=\"Style631\" me=\"" +
                "Style969\" /><FooterStyle parent=\"Style634\" me=\"Style970\" /><EditorStyle parent=\"" +
                "Style632\" me=\"Style971\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style973\" /><G" +
                "roupFooterStyle parent=\"Style631\" me=\"Style972\" /><ColumnDivider>DarkGray,Single" +
                "</ColumnDivider><Height>15</Height><DCIdx>30</DCIdx></C1DisplayColumn><C1Display" +
                "Column><HeadingStyle parent=\"Style633\" me=\"Style998\" /><Style parent=\"Style631\" " +
                "me=\"Style999\" /><FooterStyle parent=\"Style634\" me=\"Style1000\" /><EditorStyle par" +
                "ent=\"Style632\" me=\"Style1001\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style100" +
                "3\" /><GroupFooterStyle parent=\"Style631\" me=\"Style1002\" /><ColumnDivider>DarkGra" +
                "y,Single</ColumnDivider><Height>15</Height><DCIdx>31</DCIdx></C1DisplayColumn><C" +
                "1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style1028\" /><Style parent=\"S" +
                "tyle631\" me=\"Style1029\" /><FooterStyle parent=\"Style634\" me=\"Style1030\" /><Edito" +
                "rStyle parent=\"Style632\" me=\"Style1031\" /><GroupHeaderStyle parent=\"Style631\" me" +
                "=\"Style1033\" /><GroupFooterStyle parent=\"Style631\" me=\"Style1032\" /><ColumnDivid" +
                "er>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>32</DCIdx></C1Displa" +
                "yColumn></internalCols><ClientRect>0, 0, 548, 141</ClientRect><BorderSide>Right<" +
                "/BorderSide></C1.Win.C1TrueDBGrid.MergeView><C1.Win.C1TrueDBGrid.MergeView HBarS" +
                "tyle=\"None\" VBarStyle=\"None\" AllowColSelect=\"False\" Name=\"Split[0,1]\" AllowRowSi" +
                "zing=\"None\" AllowVerticalSizing=\"False\" CaptionHeight=\"18\" ColumnCaptionHeight=\"" +
                "18\" ColumnFooterHeight=\"18\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" Reco" +
                "rdSelectorWidth=\"16\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalScrollG" +
                "roup=\"1\" MinWidth=\"100\" HorizontalScrollGroup=\"4\"><Caption>Borrows</Caption><Spl" +
                "itSize>5</SplitSize><SplitSizeMode>NumberOfColumns</SplitSizeMode><CaptionStyle " +
                "parent=\"Style2\" me=\"Style483\" /><EditorStyle parent=\"Editor\" me=\"Style475\" /><Ev" +
                "enRowStyle parent=\"EvenRow\" me=\"Style481\" /><FilterBarStyle parent=\"FilterBar\" m" +
                "e=\"Style630\" /><FooterStyle parent=\"Footer\" me=\"Style477\" /><GroupStyle parent=\"" +
                "Group\" me=\"Style485\" /><HeadingStyle parent=\"Heading\" me=\"Style476\" /><HighLight" +
                "RowStyle parent=\"HighlightRow\" me=\"Style480\" /><InactiveStyle parent=\"Inactive\" " +
                "me=\"Style479\" /><OddRowStyle parent=\"OddRow\" me=\"Style482\" /><RecordSelectorStyl" +
                "e parent=\"RecordSelector\" me=\"Style484\" /><SelectedStyle parent=\"Selected\" me=\"S" +
                "tyle478\" /><Style parent=\"Normal\" me=\"Style474\" /><internalCols><C1DisplayColumn" +
                "><HeadingStyle parent=\"Style476\" me=\"Style486\" /><Style parent=\"Style474\" me=\"St" +
                "yle487\" /><FooterStyle parent=\"Style477\" me=\"Style488\" /><EditorStyle parent=\"St" +
                "yle475\" me=\"Style489\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style491\" /><Gro" +
                "upFooterStyle parent=\"Style474\" me=\"Style490\" /><ColumnDivider>DarkGray,Single</" +
                "ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayCol" +
                "umn><HeadingStyle parent=\"Style476\" me=\"Style492\" /><Style parent=\"Style474\" me=" +
                "\"Style493\" /><FooterStyle parent=\"Style477\" me=\"Style494\" /><EditorStyle parent=" +
                "\"Style475\" me=\"Style495\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style497\" /><" +
                "GroupFooterStyle parent=\"Style474\" me=\"Style496\" /><ColumnDivider>DarkGray,Singl" +
                "e</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Display" +
                "Column><HeadingStyle parent=\"Style476\" me=\"Style498\" /><Style parent=\"Style474\" " +
                "me=\"Style499\" /><FooterStyle parent=\"Style477\" me=\"Style500\" /><EditorStyle pare" +
                "nt=\"Style475\" me=\"Style501\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style503\" " +
                "/><GroupFooterStyle parent=\"Style474\" me=\"Style502\" /><ColumnDivider>DarkGray,Si" +
                "ngle</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1Disp" +
                "layColumn><HeadingStyle parent=\"Style476\" me=\"Style504\" /><Style parent=\"Style47" +
                "4\" me=\"Style505\" /><FooterStyle parent=\"Style477\" me=\"Style506\" /><EditorStyle p" +
                "arent=\"Style475\" me=\"Style507\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style50" +
                "9\" /><GroupFooterStyle parent=\"Style474\" me=\"Style508\" /><ColumnDivider>DarkGray" +
                ",Single</ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1D" +
                "isplayColumn><HeadingStyle parent=\"Style476\" me=\"Style510\" /><Style parent=\"Styl" +
                "e474\" me=\"Style511\" /><FooterStyle parent=\"Style477\" me=\"Style512\" /><EditorStyl" +
                "e parent=\"Style475\" me=\"Style513\" /><GroupHeaderStyle parent=\"Style474\" me=\"Styl" +
                "e515\" /><GroupFooterStyle parent=\"Style474\" me=\"Style514\" /><ColumnDivider>DarkG" +
                "ray,Single</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><" +
                "C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style516\" /><Style parent=\"S" +
                "tyle474\" me=\"Style517\" /><FooterStyle parent=\"Style477\" me=\"Style518\" /><EditorS" +
                "tyle parent=\"Style475\" me=\"Style519\" /><GroupHeaderStyle parent=\"Style474\" me=\"S" +
                "tyle521\" /><GroupFooterStyle parent=\"Style474\" me=\"Style520\" /><ColumnDivider>Da" +
                "rkGray,Single</ColumnDivider><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColum" +
                "n><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style522\" /><Style parent" +
                "=\"Style474\" me=\"Style523\" /><FooterStyle parent=\"Style477\" me=\"Style524\" /><Edit" +
                "orStyle parent=\"Style475\" me=\"Style525\" /><GroupHeaderStyle parent=\"Style474\" me" +
                "=\"Style527\" /><GroupFooterStyle parent=\"Style474\" me=\"Style526\" /><ColumnDivider" +
                ">DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6</DCIdx></C1DisplayCo" +
                "lumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style528\" /><Style par" +
                "ent=\"Style474\" me=\"Style529\" /><FooterStyle parent=\"Style477\" me=\"Style530\" /><E" +
                "ditorStyle parent=\"Style475\" me=\"Style531\" /><GroupHeaderStyle parent=\"Style474\"" +
                " me=\"Style533\" /><GroupFooterStyle parent=\"Style474\" me=\"Style532\" /><ColumnDivi" +
                "der>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style534\" /><Style " +
                "parent=\"Style474\" me=\"Style535\" /><FooterStyle parent=\"Style477\" me=\"Style536\" /" +
                "><EditorStyle parent=\"Style475\" me=\"Style537\" /><GroupHeaderStyle parent=\"Style4" +
                "74\" me=\"Style539\" /><GroupFooterStyle parent=\"Style474\" me=\"Style538\" /><Visible" +
                ">True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>90</Width><" +
                "Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>8</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style540\" /><Style " +
                "parent=\"Style474\" me=\"Style541\" /><FooterStyle parent=\"Style477\" me=\"Style542\" /" +
                "><EditorStyle parent=\"Style475\" me=\"Style543\" /><GroupHeaderStyle parent=\"Style4" +
                "74\" me=\"Style545\" /><GroupFooterStyle parent=\"Style474\" me=\"Style544\" /><Visible" +
                ">True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><" +
                "Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>9</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style546\" /><Style " +
                "parent=\"Style474\" me=\"Style547\" /><FooterStyle parent=\"Style477\" me=\"Style548\" /" +
                "><EditorStyle parent=\"Style475\" me=\"Style549\" /><GroupHeaderStyle parent=\"Style4" +
                "74\" me=\"Style551\" /><GroupFooterStyle parent=\"Style474\" me=\"Style550\" /><Visible" +
                ">True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>55</Width><" +
                "Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>10</DCIdx></C1Displ" +
                "ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style944\" /><Style" +
                " parent=\"Style474\" me=\"Style945\" /><FooterStyle parent=\"Style477\" me=\"Style946\" " +
                "/><EditorStyle parent=\"Style475\" me=\"Style947\" /><GroupHeaderStyle parent=\"Style" +
                "474\" me=\"Style949\" /><GroupFooterStyle parent=\"Style474\" me=\"Style948\" /><Visibl" +
                "e>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width>" +
                "<Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>29</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style974\" /><Styl" +
                "e parent=\"Style474\" me=\"Style975\" /><FooterStyle parent=\"Style477\" me=\"Style976\"" +
                " /><EditorStyle parent=\"Style475\" me=\"Style977\" /><GroupHeaderStyle parent=\"Styl" +
                "e474\" me=\"Style979\" /><GroupFooterStyle parent=\"Style474\" me=\"Style978\" /><Visib" +
                "le>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>45</Width" +
                "><Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>30</DCIdx></C1Dis" +
                "playColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style552\" /><Sty" +
                "le parent=\"Style474\" me=\"Style553\" /><FooterStyle parent=\"Style477\" me=\"Style554" +
                "\" /><EditorStyle parent=\"Style475\" me=\"Style555\" /><GroupHeaderStyle parent=\"Sty" +
                "le474\" me=\"Style557\" /><GroupFooterStyle parent=\"Style474\" me=\"Style556\" /><Colu" +
                "mnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>11</DCIdx></C" +
                "1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style558\" />" +
                "<Style parent=\"Style474\" me=\"Style559\" /><FooterStyle parent=\"Style477\" me=\"Styl" +
                "e560\" /><EditorStyle parent=\"Style475\" me=\"Style561\" /><GroupHeaderStyle parent=" +
                "\"Style474\" me=\"Style563\" /><GroupFooterStyle parent=\"Style474\" me=\"Style562\" /><" +
                "ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>12</DCIdx" +
                "></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style564" +
                "\" /><Style parent=\"Style474\" me=\"Style565\" /><FooterStyle parent=\"Style477\" me=\"" +
                "Style566\" /><EditorStyle parent=\"Style475\" me=\"Style567\" /><GroupHeaderStyle par" +
                "ent=\"Style474\" me=\"Style569\" /><GroupFooterStyle parent=\"Style474\" me=\"Style568\"" +
                " /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>13</D" +
                "CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Styl" +
                "e570\" /><Style parent=\"Style474\" me=\"Style571\" /><FooterStyle parent=\"Style477\" " +
                "me=\"Style572\" /><EditorStyle parent=\"Style475\" me=\"Style573\" /><GroupHeaderStyle" +
                " parent=\"Style474\" me=\"Style575\" /><GroupFooterStyle parent=\"Style474\" me=\"Style" +
                "574\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1" +
                "4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"" +
                "Style576\" /><Style parent=\"Style474\" me=\"Style577\" /><FooterStyle parent=\"Style4" +
                "77\" me=\"Style578\" /><EditorStyle parent=\"Style475\" me=\"Style579\" /><GroupHeaderS" +
                "tyle parent=\"Style474\" me=\"Style581\" /><GroupFooterStyle parent=\"Style474\" me=\"S" +
                "tyle580\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCI" +
                "dx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" " +
                "me=\"Style582\" /><Style parent=\"Style474\" me=\"Style583\" /><FooterStyle parent=\"St" +
                "yle477\" me=\"Style584\" /><EditorStyle parent=\"Style475\" me=\"Style585\" /><GroupHea" +
                "derStyle parent=\"Style474\" me=\"Style587\" /><GroupFooterStyle parent=\"Style474\" m" +
                "e=\"Style586\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height>" +
                "<DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style4" +
                "76\" me=\"Style588\" /><Style parent=\"Style474\" me=\"Style589\" /><FooterStyle parent" +
                "=\"Style477\" me=\"Style590\" /><EditorStyle parent=\"Style475\" me=\"Style591\" /><Grou" +
                "pHeaderStyle parent=\"Style474\" me=\"Style593\" /><GroupFooterStyle parent=\"Style47" +
                "4\" me=\"Style592\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Hei" +
                "ght><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
                "yle476\" me=\"Style594\" /><Style parent=\"Style474\" me=\"Style595\" /><FooterStyle pa" +
                "rent=\"Style477\" me=\"Style596\" /><EditorStyle parent=\"Style475\" me=\"Style597\" /><" +
                "GroupHeaderStyle parent=\"Style474\" me=\"Style599\" /><GroupFooterStyle parent=\"Sty" +
                "le474\" me=\"Style598\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15<" +
                "/Height><DCIdx>18</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
                "=\"Style476\" me=\"Style600\" /><Style parent=\"Style474\" me=\"Style601\" /><FooterStyl" +
                "e parent=\"Style477\" me=\"Style602\" /><EditorStyle parent=\"Style475\" me=\"Style603\"" +
                " /><GroupHeaderStyle parent=\"Style474\" me=\"Style605\" /><GroupFooterStyle parent=" +
                "\"Style474\" me=\"Style604\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height" +
                ">15</Height><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
                "rent=\"Style476\" me=\"Style606\" /><Style parent=\"Style474\" me=\"Style607\" /><Footer" +
                "Style parent=\"Style477\" me=\"Style608\" /><EditorStyle parent=\"Style475\" me=\"Style" +
                "609\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style611\" /><GroupFooterStyle par" +
                "ent=\"Style474\" me=\"Style610\" /><ColumnDivider>DarkGray,Single</ColumnDivider><He" +
                "ight>15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
                "e parent=\"Style476\" me=\"Style612\" /><Style parent=\"Style474\" me=\"Style613\" /><Fo" +
                "oterStyle parent=\"Style477\" me=\"Style614\" /><EditorStyle parent=\"Style475\" me=\"S" +
                "tyle615\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style617\" /><GroupFooterStyle" +
                " parent=\"Style474\" me=\"Style616\" /><ColumnDivider>DarkGray,Single</ColumnDivider" +
                "><Height>15</Height><DCIdx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
                "Style parent=\"Style476\" me=\"Style618\" /><Style parent=\"Style474\" me=\"Style619\" /" +
                "><FooterStyle parent=\"Style477\" me=\"Style620\" /><EditorStyle parent=\"Style475\" m" +
                "e=\"Style621\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style623\" /><GroupFooterS" +
                "tyle parent=\"Style474\" me=\"Style622\" /><ColumnDivider>DarkGray,Single</ColumnDiv" +
                "ider><Height>15</Height><DCIdx>22</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
                "dingStyle parent=\"Style476\" me=\"Style624\" /><Style parent=\"Style474\" me=\"Style62" +
                "5\" /><FooterStyle parent=\"Style477\" me=\"Style626\" /><EditorStyle parent=\"Style47" +
                "5\" me=\"Style627\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style629\" /><GroupFoo" +
                "terStyle parent=\"Style474\" me=\"Style628\" /><ColumnDivider>DarkGray,Single</Colum" +
                "nDivider><Height>15</Height><DCIdx>23</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
                "<HeadingStyle parent=\"Style476\" me=\"Style794\" /><Style parent=\"Style474\" me=\"Sty" +
                "le795\" /><FooterStyle parent=\"Style477\" me=\"Style796\" /><EditorStyle parent=\"Sty" +
                "le475\" me=\"Style797\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style799\" /><Grou" +
                "pFooterStyle parent=\"Style474\" me=\"Style798\" /><ColumnDivider>DarkGray,Single</C" +
                "olumnDivider><Height>15</Height><DCIdx>24</DCIdx></C1DisplayColumn><C1DisplayCol" +
                "umn><HeadingStyle parent=\"Style476\" me=\"Style824\" /><Style parent=\"Style474\" me=" +
                "\"Style825\" /><FooterStyle parent=\"Style477\" me=\"Style826\" /><EditorStyle parent=" +
                "\"Style475\" me=\"Style827\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style829\" /><" +
                "GroupFooterStyle parent=\"Style474\" me=\"Style828\" /><ColumnDivider>DarkGray,Singl" +
                "e</ColumnDivider><Height>15</Height><DCIdx>25</DCIdx></C1DisplayColumn><C1Displa" +
                "yColumn><HeadingStyle parent=\"Style476\" me=\"Style854\" /><Style parent=\"Style474\"" +
                " me=\"Style855\" /><FooterStyle parent=\"Style477\" me=\"Style856\" /><EditorStyle par" +
                "ent=\"Style475\" me=\"Style857\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style859\"" +
                " /><GroupFooterStyle parent=\"Style474\" me=\"Style858\" /><ColumnDivider>DarkGray,S" +
                "ingle</ColumnDivider><Height>15</Height><DCIdx>26</DCIdx></C1DisplayColumn><C1Di" +
                "splayColumn><HeadingStyle parent=\"Style476\" me=\"Style884\" /><Style parent=\"Style" +
                "474\" me=\"Style885\" /><FooterStyle parent=\"Style477\" me=\"Style886\" /><EditorStyle" +
                " parent=\"Style475\" me=\"Style887\" /><GroupHeaderStyle parent=\"Style474\" me=\"Style" +
                "889\" /><GroupFooterStyle parent=\"Style474\" me=\"Style888\" /><ColumnDivider>DarkGr" +
                "ay,Single</ColumnDivider><Height>15</Height><DCIdx>27</DCIdx></C1DisplayColumn><" +
                "C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style914\" /><Style parent=\"S" +
                "tyle474\" me=\"Style915\" /><FooterStyle parent=\"Style477\" me=\"Style916\" /><EditorS" +
                "tyle parent=\"Style475\" me=\"Style917\" /><GroupHeaderStyle parent=\"Style474\" me=\"S" +
                "tyle919\" /><GroupFooterStyle parent=\"Style474\" me=\"Style918\" /><ColumnDivider>Da" +
                "rkGray,Single</ColumnDivider><Height>15</Height><DCIdx>28</DCIdx></C1DisplayColu" +
                "mn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style1004\" /><Style pare" +
                "nt=\"Style474\" me=\"Style1005\" /><FooterStyle parent=\"Style477\" me=\"Style1006\" /><" +
                "EditorStyle parent=\"Style475\" me=\"Style1007\" /><GroupHeaderStyle parent=\"Style47" +
                "4\" me=\"Style1009\" /><GroupFooterStyle parent=\"Style474\" me=\"Style1008\" /><Column" +
                "Divider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>31</DCIdx></C1D" +
                "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style476\" me=\"Style1034\" /><" +
                "Style parent=\"Style474\" me=\"Style1035\" /><FooterStyle parent=\"Style477\" me=\"Styl" +
                "e1036\" /><EditorStyle parent=\"Style475\" me=\"Style1037\" /><GroupHeaderStyle paren" +
                "t=\"Style474\" me=\"Style1039\" /><GroupFooterStyle parent=\"Style474\" me=\"Style1038\"" +
                " /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>32</D" +
                "CIdx></C1DisplayColumn></internalCols><ClientRect>553, 0, 385, 141</ClientRect><" +
                "BorderSide>Left, Right</BorderSide></C1.Win.C1TrueDBGrid.MergeView><C1.Win.C1Tru" +
                "eDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"None\" AllowColSelect=\"False\" Name=" +
                "\"Split[0,2]\" AllowRowSizing=\"None\" AllowVerticalSizing=\"False\" CaptionHeight=\"18" +
                "\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" FilterBar=\"True\" MarqueeStyle" +
                "=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" RecordSelectors=" +
                "\"False\" VerticalScrollGroup=\"1\" MinWidth=\"100\" HorizontalScrollGroup=\"3\"><Captio" +
                "n>Loans</Caption><SplitSize>5</SplitSize><SplitSizeMode>NumberOfColumns</SplitSi" +
                "zeMode><CaptionStyle parent=\"Style2\" me=\"Style326\" /><EditorStyle parent=\"Editor" +
                "\" me=\"Style318\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style324\" /><FilterBarStyle" +
                " parent=\"FilterBar\" me=\"Style473\" /><FooterStyle parent=\"Footer\" me=\"Style320\" /" +
                "><GroupStyle parent=\"Group\" me=\"Style328\" /><HeadingStyle parent=\"Heading\" me=\"S" +
                "tyle319\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style323\" /><InactiveSty" +
                "le parent=\"Inactive\" me=\"Style322\" /><OddRowStyle parent=\"OddRow\" me=\"Style325\" " +
                "/><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style327\" /><SelectedStyle pa" +
                "rent=\"Selected\" me=\"Style321\" /><Style parent=\"Normal\" me=\"Style317\" /><internal" +
                "Cols><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style329\" /><Style par" +
                "ent=\"Style317\" me=\"Style330\" /><FooterStyle parent=\"Style320\" me=\"Style331\" /><E" +
                "ditorStyle parent=\"Style318\" me=\"Style332\" /><GroupHeaderStyle parent=\"Style317\"" +
                " me=\"Style334\" /><GroupFooterStyle parent=\"Style317\" me=\"Style333\" /><ColumnDivi" +
                "der>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style335\" /><Style " +
                "parent=\"Style317\" me=\"Style336\" /><FooterStyle parent=\"Style320\" me=\"Style337\" /" +
                "><EditorStyle parent=\"Style318\" me=\"Style338\" /><GroupHeaderStyle parent=\"Style3" +
                "17\" me=\"Style340\" /><GroupFooterStyle parent=\"Style317\" me=\"Style339\" /><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1Dis" +
                "playColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style341\" /><Sty" +
                "le parent=\"Style317\" me=\"Style342\" /><FooterStyle parent=\"Style320\" me=\"Style343" +
                "\" /><EditorStyle parent=\"Style318\" me=\"Style344\" /><GroupHeaderStyle parent=\"Sty" +
                "le317\" me=\"Style346\" /><GroupFooterStyle parent=\"Style317\" me=\"Style345\" /><Colu" +
                "mnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1" +
                "DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style347\" /><" +
                "Style parent=\"Style317\" me=\"Style348\" /><FooterStyle parent=\"Style320\" me=\"Style" +
                "349\" /><EditorStyle parent=\"Style318\" me=\"Style350\" /><GroupHeaderStyle parent=\"" +
                "Style317\" me=\"Style352\" /><GroupFooterStyle parent=\"Style317\" me=\"Style351\" /><C" +
                "olumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>3</DCIdx><" +
                "/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style353\" " +
                "/><Style parent=\"Style317\" me=\"Style354\" /><FooterStyle parent=\"Style320\" me=\"St" +
                "yle355\" /><EditorStyle parent=\"Style318\" me=\"Style356\" /><GroupHeaderStyle paren" +
                "t=\"Style317\" me=\"Style358\" /><GroupFooterStyle parent=\"Style317\" me=\"Style357\" /" +
                "><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>4</DCId" +
                "x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style35" +
                "9\" /><Style parent=\"Style317\" me=\"Style360\" /><FooterStyle parent=\"Style320\" me=" +
                "\"Style361\" /><EditorStyle parent=\"Style318\" me=\"Style362\" /><GroupHeaderStyle pa" +
                "rent=\"Style317\" me=\"Style364\" /><GroupFooterStyle parent=\"Style317\" me=\"Style363" +
                "\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>5</D" +
                "CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Styl" +
                "e365\" /><Style parent=\"Style317\" me=\"Style366\" /><FooterStyle parent=\"Style320\" " +
                "me=\"Style367\" /><EditorStyle parent=\"Style318\" me=\"Style368\" /><GroupHeaderStyle" +
                " parent=\"Style317\" me=\"Style370\" /><GroupFooterStyle parent=\"Style317\" me=\"Style" +
                "369\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6" +
                "</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"S" +
                "tyle371\" /><Style parent=\"Style317\" me=\"Style372\" /><FooterStyle parent=\"Style32" +
                "0\" me=\"Style373\" /><EditorStyle parent=\"Style318\" me=\"Style374\" /><GroupHeaderSt" +
                "yle parent=\"Style317\" me=\"Style376\" /><GroupFooterStyle parent=\"Style317\" me=\"St" +
                "yle375\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCId" +
                "x>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me" +
                "=\"Style377\" /><Style parent=\"Style317\" me=\"Style378\" /><FooterStyle parent=\"Styl" +
                "e320\" me=\"Style379\" /><EditorStyle parent=\"Style318\" me=\"Style380\" /><GroupHeade" +
                "rStyle parent=\"Style317\" me=\"Style382\" /><GroupFooterStyle parent=\"Style317\" me=" +
                "\"Style381\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><D" +
                "CIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\"" +
                " me=\"Style383\" /><Style parent=\"Style317\" me=\"Style384\" /><FooterStyle parent=\"S" +
                "tyle320\" me=\"Style385\" /><EditorStyle parent=\"Style318\" me=\"Style386\" /><GroupHe" +
                "aderStyle parent=\"Style317\" me=\"Style388\" /><GroupFooterStyle parent=\"Style317\" " +
                "me=\"Style387\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height" +
                "><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style3" +
                "19\" me=\"Style389\" /><Style parent=\"Style317\" me=\"Style390\" /><FooterStyle parent" +
                "=\"Style320\" me=\"Style391\" /><EditorStyle parent=\"Style318\" me=\"Style392\" /><Grou" +
                "pHeaderStyle parent=\"Style317\" me=\"Style394\" /><GroupFooterStyle parent=\"Style31" +
                "7\" me=\"Style393\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Hei" +
                "ght><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
                "yle319\" me=\"Style395\" /><Style parent=\"Style317\" me=\"Style396\" /><FooterStyle pa" +
                "rent=\"Style320\" me=\"Style397\" /><EditorStyle parent=\"Style318\" me=\"Style398\" /><" +
                "GroupHeaderStyle parent=\"Style317\" me=\"Style400\" /><GroupFooterStyle parent=\"Sty" +
                "le317\" me=\"Style399\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</C" +
                "olumnDivider><Width>90</Width><Height>15</Height><FooterDivider>False</FooterDiv" +
                "ider><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
                "tyle319\" me=\"Style401\" /><Style parent=\"Style317\" me=\"Style402\" /><FooterStyle p" +
                "arent=\"Style320\" me=\"Style403\" /><EditorStyle parent=\"Style318\" me=\"Style404\" />" +
                "<GroupHeaderStyle parent=\"Style317\" me=\"Style406\" /><GroupFooterStyle parent=\"St" +
                "yle317\" me=\"Style405\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</" +
                "ColumnDivider><Width>95</Width><Height>15</Height><FooterDivider>False</FooterDi" +
                "vider><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
                "Style319\" me=\"Style407\" /><Style parent=\"Style317\" me=\"Style408\" /><FooterStyle " +
                "parent=\"Style320\" me=\"Style409\" /><EditorStyle parent=\"Style318\" me=\"Style410\" /" +
                "><GroupHeaderStyle parent=\"Style317\" me=\"Style412\" /><GroupFooterStyle parent=\"S" +
                "tyle317\" me=\"Style411\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single<" +
                "/ColumnDivider><Width>55</Width><Height>15</Height><FooterDivider>False</FooterD" +
                "ivider><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
                "\"Style319\" me=\"Style1010\" /><Style parent=\"Style317\" me=\"Style1011\" /><FooterSty" +
                "le parent=\"Style320\" me=\"Style1012\" /><EditorStyle parent=\"Style318\" me=\"Style10" +
                "13\" /><GroupHeaderStyle parent=\"Style317\" me=\"Style1015\" /><GroupFooterStyle par" +
                "ent=\"Style317\" me=\"Style1014\" /><Visible>True</Visible><ColumnDivider>Gainsboro," +
                "Single</ColumnDivider><Width>95</Width><Height>15</Height><FooterDivider>False</" +
                "FooterDivider><DCIdx>31</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
                "parent=\"Style319\" me=\"Style1040\" /><Style parent=\"Style317\" me=\"Style1041\" /><Fo" +
                "oterStyle parent=\"Style320\" me=\"Style1042\" /><EditorStyle parent=\"Style318\" me=\"" +
                "Style1043\" /><GroupHeaderStyle parent=\"Style317\" me=\"Style1045\" /><GroupFooterSt" +
                "yle parent=\"Style317\" me=\"Style1044\" /><Visible>True</Visible><ColumnDivider>Gai" +
                "nsboro,Single</ColumnDivider><Width>45</Width><Height>15</Height><FooterDivider>" +
                "False</FooterDivider><DCIdx>32</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
                "gStyle parent=\"Style319\" me=\"Style413\" /><Style parent=\"Style317\" me=\"Style414\" " +
                "/><FooterStyle parent=\"Style320\" me=\"Style415\" /><EditorStyle parent=\"Style318\" " +
                "me=\"Style416\" /><GroupHeaderStyle parent=\"Style317\" me=\"Style418\" /><GroupFooter" +
                "Style parent=\"Style317\" me=\"Style417\" /><ColumnDivider>DarkGray,Single</ColumnDi" +
                "vider><Height>15</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
                "adingStyle parent=\"Style319\" me=\"Style419\" /><Style parent=\"Style317\" me=\"Style4" +
                "20\" /><FooterStyle parent=\"Style320\" me=\"Style421\" /><EditorStyle parent=\"Style3" +
                "18\" me=\"Style422\" /><GroupHeaderStyle parent=\"Style317\" me=\"Style424\" /><GroupFo" +
                "oterStyle parent=\"Style317\" me=\"Style423\" /><ColumnDivider>DarkGray,Single</Colu" +
                "mnDivider><Height>15</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn" +
                "><HeadingStyle parent=\"Style319\" me=\"Style425\" /><Style parent=\"Style317\" me=\"St" +
                "yle426\" /><FooterStyle parent=\"Style320\" me=\"Style427\" /><EditorStyle parent=\"St" +
                "yle318\" me=\"Style428\" /><GroupHeaderStyle parent=\"Style317\" me=\"Style430\" /><Gro" +
                "upFooterStyle parent=\"Style317\" me=\"Style429\" /><ColumnDivider>DarkGray,Single</" +
                "ColumnDivider><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayCo" +
                "lumn><HeadingStyle parent=\"Style319\" me=\"Style431\" /><Style parent=\"Style317\" me" +
                "=\"Style432\" /><FooterStyle parent=\"Style320\" me=\"Style433\" /><EditorStyle parent" +
                "=\"Style318\" me=\"Style434\" /><GroupHeaderStyle parent=\"Style317\" me=\"Style436\" />" +
                "<GroupFooterStyle parent=\"Style317\" me=\"Style435\" /><ColumnDivider>DarkGray,Sing" +
                "le</ColumnDivider><Height>15</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1Displ" +
                "ayColumn><HeadingStyle parent=\"Style319\" me=\"Style437\" /><Style parent=\"Style317" +
                "\" me=\"Style438\" /><FooterStyle parent=\"Style320\" me=\"Style439\" /><EditorStyle pa" +
                "rent=\"Style318\" me=\"Style440\" /><GroupHeaderStyle parent=\"Style317\" me=\"Style442" +
                "\" /><GroupFooterStyle parent=\"Style317\" me=\"Style441\" /><ColumnDivider>DarkGray," +
                "Single</ColumnDivider><Height>15</Height><DCIdx>18</DCIdx></C1DisplayColumn><C1D" +
                "isplayColumn><HeadingStyle parent=\"Style319\" me=\"Style443\" /><Style parent=\"Styl" +
                "e317\" me=\"Style444\" /><FooterStyle parent=\"Style320\" me=\"Style445\" /><EditorStyl" +
                "e parent=\"Style318\" me=\"Style446\" /><GroupHeaderStyle parent=\"Style317\" me=\"Styl" +
                "e448\" /><GroupFooterStyle parent=\"Style317\" me=\"Style447\" /><ColumnDivider>DarkG" +
                "ray,Single</ColumnDivider><Height>15</Height><DCIdx>19</DCIdx></C1DisplayColumn>" +
                "<C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style449\" /><Style parent=\"" +
                "Style317\" me=\"Style450\" /><FooterStyle parent=\"Style320\" me=\"Style451\" /><Editor" +
                "Style parent=\"Style318\" me=\"Style452\" /><GroupHeaderStyle parent=\"Style317\" me=\"" +
                "Style454\" /><GroupFooterStyle parent=\"Style317\" me=\"Style453\" /><ColumnDivider>D" +
                "arkGray,Single</ColumnDivider><Height>15</Height><DCIdx>20</DCIdx></C1DisplayCol" +
                "umn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style455\" /><Style pare" +
                "nt=\"Style317\" me=\"Style456\" /><FooterStyle parent=\"Style320\" me=\"Style457\" /><Ed" +
                "itorStyle parent=\"Style318\" me=\"Style458\" /><GroupHeaderStyle parent=\"Style317\" " +
                "me=\"Style460\" /><GroupFooterStyle parent=\"Style317\" me=\"Style459\" /><ColumnDivid" +
                "er>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>21</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style461\" /><Style " +
                "parent=\"Style317\" me=\"Style462\" /><FooterStyle parent=\"Style320\" me=\"Style463\" /" +
                "><EditorStyle parent=\"Style318\" me=\"Style464\" /><GroupHeaderStyle parent=\"Style3" +
                "17\" me=\"Style466\" /><GroupFooterStyle parent=\"Style317\" me=\"Style465\" /><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>22</DCIdx></C1Di" +
                "splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style467\" /><St" +
                "yle parent=\"Style317\" me=\"Style468\" /><FooterStyle parent=\"Style320\" me=\"Style46" +
                "9\" /><EditorStyle parent=\"Style318\" me=\"Style470\" /><GroupHeaderStyle parent=\"St" +
                "yle317\" me=\"Style472\" /><GroupFooterStyle parent=\"Style317\" me=\"Style471\" /><Col" +
                "umnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>23</DCIdx></" +
                "C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style800\" /" +
                "><Style parent=\"Style317\" me=\"Style801\" /><FooterStyle parent=\"Style320\" me=\"Sty" +
                "le802\" /><EditorStyle parent=\"Style318\" me=\"Style803\" /><GroupHeaderStyle parent" +
                "=\"Style317\" me=\"Style805\" /><GroupFooterStyle parent=\"Style317\" me=\"Style804\" />" +
                "<ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>24</DCId" +
                "x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Style83" +
                "0\" /><Style parent=\"Style317\" me=\"Style831\" /><FooterStyle parent=\"Style320\" me=" +
                "\"Style832\" /><EditorStyle parent=\"Style318\" me=\"Style833\" /><GroupHeaderStyle pa" +
                "rent=\"Style317\" me=\"Style835\" /><GroupFooterStyle parent=\"Style317\" me=\"Style834" +
                "\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>25</" +
                "DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=\"Sty" +
                "le860\" /><Style parent=\"Style317\" me=\"Style861\" /><FooterStyle parent=\"Style320\"" +
                " me=\"Style862\" /><EditorStyle parent=\"Style318\" me=\"Style863\" /><GroupHeaderStyl" +
                "e parent=\"Style317\" me=\"Style865\" /><GroupFooterStyle parent=\"Style317\" me=\"Styl" +
                "e864\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>" +
                "26</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\" me=" +
                "\"Style890\" /><Style parent=\"Style317\" me=\"Style891\" /><FooterStyle parent=\"Style" +
                "320\" me=\"Style892\" /><EditorStyle parent=\"Style318\" me=\"Style893\" /><GroupHeader" +
                "Style parent=\"Style317\" me=\"Style895\" /><GroupFooterStyle parent=\"Style317\" me=\"" +
                "Style894\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DC" +
                "Idx>27</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style319\"" +
                " me=\"Style920\" /><Style parent=\"Style317\" me=\"Style921\" /><FooterStyle parent=\"S" +
                "tyle320\" me=\"Style922\" /><EditorStyle parent=\"Style318\" me=\"Style923\" /><GroupHe" +
                "aderStyle parent=\"Style317\" me=\"Style925\" /><GroupFooterStyle parent=\"Style317\" " +
                "me=\"Style924\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height" +
                "><DCIdx>28</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
                "319\" me=\"Style950\" /><Style parent=\"Style317\" me=\"Style951\" /><FooterStyle paren" +
                "t=\"Style320\" me=\"Style952\" /><EditorStyle parent=\"Style318\" me=\"Style953\" /><Gro" +
                "upHeaderStyle parent=\"Style317\" me=\"Style955\" /><GroupFooterStyle parent=\"Style3" +
                "17\" me=\"Style954\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</He" +
                "ight><DCIdx>29</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
                "tyle319\" me=\"Style980\" /><Style parent=\"Style317\" me=\"Style981\" /><FooterStyle p" +
                "arent=\"Style320\" me=\"Style982\" /><EditorStyle parent=\"Style318\" me=\"Style983\" />" +
                "<GroupHeaderStyle parent=\"Style317\" me=\"Style985\" /><GroupFooterStyle parent=\"St" +
                "yle317\" me=\"Style984\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15" +
                "</Height><DCIdx>30</DCIdx></C1DisplayColumn></internalCols><ClientRect>943, 0, 3" +
                "85, 141</ClientRect><BorderSide>Left, Right</BorderSide></C1.Win.C1TrueDBGrid.Me" +
                "rgeView><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"None\" AllowCo" +
                "lSelect=\"False\" Name=\"Split[0,3]\" AllowRowSizing=\"None\" AllowVerticalSizing=\"Fal" +
                "se\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" FilterBa" +
                "r=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=" +
                "\"16\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" MinWidth=\"1\" HorizontalScro" +
                "llGroup=\"2\"><Caption>Credit</Caption><SplitSize>6</SplitSize><SplitSizeMode>Numb" +
                "erOfColumns</SplitSizeMode><CaptionStyle parent=\"Style2\" me=\"Style169\" /><Editor" +
                "Style parent=\"Editor\" me=\"Style161\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style16" +
                "7\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style316\" /><FooterStyle parent=\"Foo" +
                "ter\" me=\"Style163\" /><GroupStyle parent=\"Group\" me=\"Style171\" /><HeadingStyle pa" +
                "rent=\"Heading\" me=\"Style162\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Styl" +
                "e166\" /><InactiveStyle parent=\"Inactive\" me=\"Style165\" /><OddRowStyle parent=\"Od" +
                "dRow\" me=\"Style168\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style170\"" +
                " /><SelectedStyle parent=\"Selected\" me=\"Style164\" /><Style parent=\"Normal\" me=\"S" +
                "tyle160\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"St" +
                "yle172\" /><Style parent=\"Style160\" me=\"Style173\" /><FooterStyle parent=\"Style163" +
                "\" me=\"Style174\" /><EditorStyle parent=\"Style161\" me=\"Style175\" /><GroupHeaderSty" +
                "le parent=\"Style160\" me=\"Style177\" /><GroupFooterStyle parent=\"Style160\" me=\"Sty" +
                "le176\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx" +
                ">0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=" +
                "\"Style178\" /><Style parent=\"Style160\" me=\"Style179\" /><FooterStyle parent=\"Style" +
                "163\" me=\"Style180\" /><EditorStyle parent=\"Style161\" me=\"Style181\" /><GroupHeader" +
                "Style parent=\"Style160\" me=\"Style183\" /><GroupFooterStyle parent=\"Style160\" me=\"" +
                "Style182\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DC" +
                "Idx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" " +
                "me=\"Style184\" /><Style parent=\"Style160\" me=\"Style185\" /><FooterStyle parent=\"St" +
                "yle163\" me=\"Style186\" /><EditorStyle parent=\"Style161\" me=\"Style187\" /><GroupHea" +
                "derStyle parent=\"Style160\" me=\"Style189\" /><GroupFooterStyle parent=\"Style160\" m" +
                "e=\"Style188\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height>" +
                "<DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style16" +
                "2\" me=\"Style190\" /><Style parent=\"Style160\" me=\"Style191\" /><FooterStyle parent=" +
                "\"Style163\" me=\"Style192\" /><EditorStyle parent=\"Style161\" me=\"Style193\" /><Group" +
                "HeaderStyle parent=\"Style160\" me=\"Style195\" /><GroupFooterStyle parent=\"Style160" +
                "\" me=\"Style194\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heig" +
                "ht><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
                "e162\" me=\"Style196\" /><Style parent=\"Style160\" me=\"Style197\" /><FooterStyle pare" +
                "nt=\"Style163\" me=\"Style198\" /><EditorStyle parent=\"Style161\" me=\"Style199\" /><Gr" +
                "oupHeaderStyle parent=\"Style160\" me=\"Style201\" /><GroupFooterStyle parent=\"Style" +
                "160\" me=\"Style200\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</H" +
                "eight><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
                "tyle162\" me=\"Style202\" /><Style parent=\"Style160\" me=\"Style203\" /><FooterStyle p" +
                "arent=\"Style163\" me=\"Style204\" /><EditorStyle parent=\"Style161\" me=\"Style205\" />" +
                "<GroupHeaderStyle parent=\"Style160\" me=\"Style207\" /><GroupFooterStyle parent=\"St" +
                "yle160\" me=\"Style206\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15" +
                "</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
                "=\"Style162\" me=\"Style208\" /><Style parent=\"Style160\" me=\"Style209\" /><FooterStyl" +
                "e parent=\"Style163\" me=\"Style210\" /><EditorStyle parent=\"Style161\" me=\"Style211\"" +
                " /><GroupHeaderStyle parent=\"Style160\" me=\"Style213\" /><GroupFooterStyle parent=" +
                "\"Style160\" me=\"Style212\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height" +
                ">15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style162\" me=\"Style214\" /><Style parent=\"Style160\" me=\"Style215\" /><FooterS" +
                "tyle parent=\"Style163\" me=\"Style216\" /><EditorStyle parent=\"Style161\" me=\"Style2" +
                "17\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style219\" /><GroupFooterStyle pare" +
                "nt=\"Style160\" me=\"Style218\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
                "ght>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
                "parent=\"Style162\" me=\"Style220\" /><Style parent=\"Style160\" me=\"Style221\" /><Foot" +
                "erStyle parent=\"Style163\" me=\"Style222\" /><EditorStyle parent=\"Style161\" me=\"Sty" +
                "le223\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style225\" /><GroupFooterStyle p" +
                "arent=\"Style160\" me=\"Style224\" /><ColumnDivider>DarkGray,Single</ColumnDivider><" +
                "Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
                "le parent=\"Style162\" me=\"Style226\" /><Style parent=\"Style160\" me=\"Style227\" /><F" +
                "ooterStyle parent=\"Style163\" me=\"Style228\" /><EditorStyle parent=\"Style161\" me=\"" +
                "Style229\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style231\" /><GroupFooterStyl" +
                "e parent=\"Style160\" me=\"Style230\" /><ColumnDivider>DarkGray,Single</ColumnDivide" +
                "r><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
                "Style parent=\"Style162\" me=\"Style232\" /><Style parent=\"Style160\" me=\"Style233\" /" +
                "><FooterStyle parent=\"Style163\" me=\"Style234\" /><EditorStyle parent=\"Style161\" m" +
                "e=\"Style235\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style237\" /><GroupFooterS" +
                "tyle parent=\"Style160\" me=\"Style236\" /><ColumnDivider>DarkGray,Single</ColumnDiv" +
                "ider><Height>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
                "dingStyle parent=\"Style162\" me=\"Style238\" /><Style parent=\"Style160\" me=\"Style23" +
                "9\" /><FooterStyle parent=\"Style163\" me=\"Style240\" /><EditorStyle parent=\"Style16" +
                "1\" me=\"Style241\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style243\" /><GroupFoo" +
                "terStyle parent=\"Style160\" me=\"Style242\" /><ColumnDivider>DarkGray,Single</Colum" +
                "nDivider><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
                "<HeadingStyle parent=\"Style162\" me=\"Style244\" /><Style parent=\"Style160\" me=\"Sty" +
                "le245\" /><FooterStyle parent=\"Style163\" me=\"Style246\" /><EditorStyle parent=\"Sty" +
                "le161\" me=\"Style247\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style249\" /><Grou" +
                "pFooterStyle parent=\"Style160\" me=\"Style248\" /><ColumnDivider>DarkGray,Single</C" +
                "olumnDivider><Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayCol" +
                "umn><HeadingStyle parent=\"Style162\" me=\"Style250\" /><Style parent=\"Style160\" me=" +
                "\"Style251\" /><FooterStyle parent=\"Style163\" me=\"Style252\" /><EditorStyle parent=" +
                "\"Style161\" me=\"Style253\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style255\" /><" +
                "GroupFooterStyle parent=\"Style160\" me=\"Style254\" /><ColumnDivider>DarkGray,Singl" +
                "e</ColumnDivider><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1Displa" +
                "yColumn><HeadingStyle parent=\"Style162\" me=\"Style806\" /><Style parent=\"Style160\"" +
                " me=\"1000\" /><FooterStyle parent=\"Style163\" me=\"Style808\" /><EditorStyle parent=" +
                "\"Style161\" me=\"Style809\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style811\" /><" +
                "GroupFooterStyle parent=\"Style160\" me=\"Style810\" /><ColumnDivider>DarkGray,None<" +
                "/ColumnDivider><Width>3</Width><Height>15</Height><DCIdx>24</DCIdx></C1DisplayCo" +
                "lumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style256\" /><Style par" +
                "ent=\"Style160\" me=\"Style257\" /><FooterStyle parent=\"Style163\" me=\"Style258\" /><E" +
                "ditorStyle parent=\"Style161\" me=\"Style259\" /><GroupHeaderStyle parent=\"Style160\"" +
                " me=\"Style261\" /><GroupFooterStyle parent=\"Style160\" me=\"Style260\" /><Visible>Tr" +
                "ue</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><Hei" +
                "ght>15</Height><FooterDivider>False</FooterDivider><DCIdx>14</DCIdx></C1DisplayC" +
                "olumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style262\" /><Style pa" +
                "rent=\"Style160\" me=\"Style263\" /><FooterStyle parent=\"Style163\" me=\"Style264\" /><" +
                "EditorStyle parent=\"Style161\" me=\"Style265\" /><GroupHeaderStyle parent=\"Style160" +
                "\" me=\"Style267\" /><GroupFooterStyle parent=\"Style160\" me=\"Style266\" /><Visible>T" +
                "rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>45</Width><Hei" +
                "ght>15</Height><FooterDivider>False</FooterDivider><DCIdx>15</DCIdx></C1DisplayC" +
                "olumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style268\" /><Style pa" +
                "rent=\"Style160\" me=\"Style269\" /><FooterStyle parent=\"Style163\" me=\"Style270\" /><" +
                "EditorStyle parent=\"Style161\" me=\"Style271\" /><GroupHeaderStyle parent=\"Style160" +
                "\" me=\"Style273\" /><GroupFooterStyle parent=\"Style160\" me=\"Style272\" /><Visible>T" +
                "rue</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><He" +
                "ight>15</Height><FooterDivider>False</FooterDivider><DCIdx>16</DCIdx></C1Display" +
                "Column><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style274\" /><Style p" +
                "arent=\"Style160\" me=\"Style275\" /><FooterStyle parent=\"Style163\" me=\"Style276\" />" +
                "<EditorStyle parent=\"Style161\" me=\"Style277\" /><GroupHeaderStyle parent=\"Style16" +
                "0\" me=\"Style279\" /><GroupFooterStyle parent=\"Style160\" me=\"Style278\" /><Visible>" +
                "True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>45</Width><He" +
                "ight>15</Height><FooterDivider>False</FooterDivider><DCIdx>17</DCIdx></C1Display" +
                "Column><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style280\" /><Style p" +
                "arent=\"Style160\" me=\"Style281\" /><FooterStyle parent=\"Style163\" me=\"Style282\" />" +
                "<EditorStyle parent=\"Style161\" me=\"Style283\" /><GroupHeaderStyle parent=\"Style16" +
                "0\" me=\"Style285\" /><GroupFooterStyle parent=\"Style160\" me=\"Style284\" /><Visible>" +
                "True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><H" +
                "eight>15</Height><FooterDivider>False</FooterDivider><DCIdx>18</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style286\" /><Style " +
                "parent=\"Style160\" me=\"Style287\" /><FooterStyle parent=\"Style163\" me=\"Style288\" /" +
                "><EditorStyle parent=\"Style161\" me=\"Style289\" /><GroupHeaderStyle parent=\"Style1" +
                "60\" me=\"Style291\" /><GroupFooterStyle parent=\"Style160\" me=\"Style290\" /><Visible" +
                ">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>45</Width><H" +
                "eight>15</Height><FooterDivider>False</FooterDivider><DCIdx>19</DCIdx></C1Displa" +
                "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style292\" /><Style " +
                "parent=\"Style160\" me=\"Style293\" /><FooterStyle parent=\"Style163\" me=\"Style294\" /" +
                "><EditorStyle parent=\"Style161\" me=\"Style295\" /><GroupHeaderStyle parent=\"Style1" +
                "60\" me=\"Style297\" /><GroupFooterStyle parent=\"Style160\" me=\"Style296\" /><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Width>85</Width><Height>15</Height><DCIdx" +
                ">20</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me" +
                "=\"Style298\" /><Style parent=\"Style160\" me=\"Style299\" /><FooterStyle parent=\"Styl" +
                "e163\" me=\"Style300\" /><EditorStyle parent=\"Style161\" me=\"Style301\" /><GroupHeade" +
                "rStyle parent=\"Style160\" me=\"Style303\" /><GroupFooterStyle parent=\"Style160\" me=" +
                "\"Style302\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>85</Width><Hei" +
                "ght>15</Height><DCIdx>21</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
                " parent=\"Style162\" me=\"Style304\" /><Style parent=\"Style160\" me=\"Style305\" /><Foo" +
                "terStyle parent=\"Style163\" me=\"Style306\" /><EditorStyle parent=\"Style161\" me=\"St" +
                "yle307\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style309\" /><GroupFooterStyle " +
                "parent=\"Style160\" me=\"Style308\" /><ColumnDivider>DarkGray,Single</ColumnDivider>" +
                "<Width>85</Width><Height>15</Height><DCIdx>22</DCIdx></C1DisplayColumn><C1Displa" +
                "yColumn><HeadingStyle parent=\"Style162\" me=\"Style310\" /><Style parent=\"Style160\"" +
                " me=\"Style311\" /><FooterStyle parent=\"Style163\" me=\"Style312\" /><EditorStyle par" +
                "ent=\"Style161\" me=\"Style313\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style315\"" +
                " /><GroupFooterStyle parent=\"Style160\" me=\"Style314\" /><ColumnDivider>DarkGray,S" +
                "ingle</ColumnDivider><Width>85</Width><Height>15</Height><DCIdx>23</DCIdx></C1Di" +
                "splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style836\" /><St" +
                "yle parent=\"Style160\" me=\"Style837\" /><FooterStyle parent=\"Style163\" me=\"Style83" +
                "8\" /><EditorStyle parent=\"Style161\" me=\"Style839\" /><GroupHeaderStyle parent=\"St" +
                "yle160\" me=\"Style841\" /><GroupFooterStyle parent=\"Style160\" me=\"Style840\" /><Col" +
                "umnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>25</DCIdx></" +
                "C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style866\" /" +
                "><Style parent=\"Style160\" me=\"Style867\" /><FooterStyle parent=\"Style163\" me=\"Sty" +
                "le868\" /><EditorStyle parent=\"Style161\" me=\"Style869\" /><GroupHeaderStyle parent" +
                "=\"Style160\" me=\"Style871\" /><GroupFooterStyle parent=\"Style160\" me=\"Style870\" />" +
                "<ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>26</DCId" +
                "x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Style89" +
                "6\" /><Style parent=\"Style160\" me=\"Style897\" /><FooterStyle parent=\"Style163\" me=" +
                "\"Style898\" /><EditorStyle parent=\"Style161\" me=\"Style899\" /><GroupHeaderStyle pa" +
                "rent=\"Style160\" me=\"Style901\" /><GroupFooterStyle parent=\"Style160\" me=\"Style900" +
                "\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>27</" +
                "DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=\"Sty" +
                "le926\" /><Style parent=\"Style160\" me=\"Style927\" /><FooterStyle parent=\"Style163\"" +
                " me=\"Style928\" /><EditorStyle parent=\"Style161\" me=\"Style929\" /><GroupHeaderStyl" +
                "e parent=\"Style160\" me=\"Style931\" /><GroupFooterStyle parent=\"Style160\" me=\"Styl" +
                "e930\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>" +
                "28</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\" me=" +
                "\"Style956\" /><Style parent=\"Style160\" me=\"Style957\" /><FooterStyle parent=\"Style" +
                "163\" me=\"Style958\" /><EditorStyle parent=\"Style161\" me=\"Style959\" /><GroupHeader" +
                "Style parent=\"Style160\" me=\"Style961\" /><GroupFooterStyle parent=\"Style160\" me=\"" +
                "Style960\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DC" +
                "Idx>29</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style162\"" +
                " me=\"Style986\" /><Style parent=\"Style160\" me=\"Style987\" /><FooterStyle parent=\"S" +
                "tyle163\" me=\"Style988\" /><EditorStyle parent=\"Style161\" me=\"Style989\" /><GroupHe" +
                "aderStyle parent=\"Style160\" me=\"Style991\" /><GroupFooterStyle parent=\"Style160\" " +
                "me=\"Style990\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height" +
                "><DCIdx>30</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
                "162\" me=\"Style1016\" /><Style parent=\"Style160\" me=\"Style1017\" /><FooterStyle par" +
                "ent=\"Style163\" me=\"Style1018\" /><EditorStyle parent=\"Style161\" me=\"Style1019\" />" +
                "<GroupHeaderStyle parent=\"Style160\" me=\"Style1021\" /><GroupFooterStyle parent=\"S" +
                "tyle160\" me=\"Style1020\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>" +
                "15</Height><DCIdx>31</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style162\" me=\"Style1046\" /><Style parent=\"Style160\" me=\"Style1047\" /><Foote" +
                "rStyle parent=\"Style163\" me=\"Style1048\" /><EditorStyle parent=\"Style161\" me=\"Sty" +
                "le1049\" /><GroupHeaderStyle parent=\"Style160\" me=\"Style1051\" /><GroupFooterStyle" +
                " parent=\"Style160\" me=\"Style1050\" /><ColumnDivider>DarkGray,Single</ColumnDivide" +
                "r><Height>15</Height><DCIdx>32</DCIdx></C1DisplayColumn></internalCols><ClientRe" +
                "ct>1333, 0, 426, 141</ClientRect><BorderSide>Left, Right</BorderSide></C1.Win.C1" +
                "TrueDBGrid.MergeView><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"" +
                "Always\" AllowColSelect=\"False\" Name=\"Split[0,4]\" AllowRowSizing=\"None\" AllowVert" +
                "icalSizing=\"False\" CaptionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeigh" +
                "t=\"18\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" " +
                "RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalScr" +
                "ollGroup=\"1\" HorizontalScrollGroup=\"1\"><Caption>Income</Caption><SplitSize>0</Sp" +
                "litSize><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor" +
                "\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle par" +
                "ent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><Group" +
                "Style parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /" +
                "><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"I" +
                "nactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelecto" +
                "rStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" m" +
                "e=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn" +
                "><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17" +
                "\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=" +
                "\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle p" +
                "arent=\"Style1\" me=\"Style20\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
                "ght>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
                "parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyl" +
                "e parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><Gr" +
                "oupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" " +
                "me=\"Style26\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height>" +
                "<DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
                " me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style" +
                "3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle " +
                "parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" />" +
                "<ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx" +
                "></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /" +
                "><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\"" +
                " /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\"" +
                " me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><ColumnDivider>" +
                "DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayCol" +
                "umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=" +
                "\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle" +
                " parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /" +
                "><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><ColumnDivider>DarkGray,Single" +
                "</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayC" +
                "olumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"St" +
                "yle47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5" +
                "\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterSt" +
                "yle parent=\"Style1\" me=\"Style50\" /><ColumnDivider>DarkGray,Single</ColumnDivider" +
                "><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
                "tyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><Foote" +
                "rStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" " +
                "/><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Sty" +
                "le1\" me=\"Style56\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</He" +
                "ight><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
                "yle2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"" +
                "Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderS" +
                "tyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style6" +
                "2\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</" +
                "DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
                "64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
                "le66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"St" +
                "yle1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><ColumnDiv" +
                "ider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>8</DCIdx></C1Displ" +
                "ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style pa" +
                "rent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><Editor" +
                "Style parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
                "75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><ColumnDivider>DarkGray,S" +
                "ingle</ColumnDivider><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1Dis" +
                "playColumn><HeadingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" m" +
                "e=\"Style77\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"S" +
                "tyle5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFoo" +
                "terStyle parent=\"Style1\" me=\"Style80\" /><ColumnDivider>DarkGray,Single</ColumnDi" +
                "vider><Height>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
                "adingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" />" +
                "<FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
                "le85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFooterStyle paren" +
                "t=\"Style1\" me=\"Style86\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>" +
                "15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
                "ent=\"Style2\" me=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><FooterStyle p" +
                "arent=\"Style3\" me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" /><Group" +
                "HeaderStyle parent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Style1\" me=" +
                "\"Style92\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DC" +
                "Idx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
                "e=\"Style94\" /><Style parent=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\"" +
                " me=\"Style96\" /><EditorStyle parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle pa" +
                "rent=\"Style1\" me=\"Style99\" /><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><C" +
                "olumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>13</DCIdx>" +
                "</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style100\" /" +
                "><Style parent=\"Style1\" me=\"Style101\" /><FooterStyle parent=\"Style3\" me=\"Style10" +
                "2\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><GroupHeaderStyle parent=\"Styl" +
                "e1\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1\" me=\"Style104\" /><ColumnDiv" +
                "ider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>14</DCIdx></C1Disp" +
                "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style106\" /><Style " +
                "parent=\"Style1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=\"Style108\" /><Ed" +
                "itorStyle parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle parent=\"Style1\" me=\"" +
                "Style111\" /><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><ColumnDivider>Dar" +
                "kGray,Single</ColumnDivider><Height>15</Height><DCIdx>15</DCIdx></C1DisplayColum" +
                "n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style112\" /><Style parent=\"" +
                "Style1\" me=\"Style113\" /><FooterStyle parent=\"Style3\" me=\"Style114\" /><EditorStyl" +
                "e parent=\"Style5\" me=\"Style115\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style117" +
                "\" /><GroupFooterStyle parent=\"Style1\" me=\"Style116\" /><ColumnDivider>DarkGray,Si" +
                "ngle</ColumnDivider><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1Dis" +
                "playColumn><HeadingStyle parent=\"Style2\" me=\"Style118\" /><Style parent=\"Style1\" " +
                "me=\"Style119\" /><FooterStyle parent=\"Style3\" me=\"Style120\" /><EditorStyle parent" +
                "=\"Style5\" me=\"Style121\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style123\" /><Gro" +
                "upFooterStyle parent=\"Style1\" me=\"Style122\" /><ColumnDivider>DarkGray,Single</Co" +
                "lumnDivider><Height>15</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColu" +
                "mn><HeadingStyle parent=\"Style2\" me=\"Style124\" /><Style parent=\"Style1\" me=\"Styl" +
                "e125\" /><FooterStyle parent=\"Style3\" me=\"Style126\" /><EditorStyle parent=\"Style5" +
                "\" me=\"Style127\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style129\" /><GroupFooter" +
                "Style parent=\"Style1\" me=\"Style128\" /><ColumnDivider>DarkGray,Single</ColumnDivi" +
                "der><Height>15</Height><DCIdx>18</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
                "ingStyle parent=\"Style2\" me=\"Style130\" /><Style parent=\"Style1\" me=\"Style131\" />" +
                "<FooterStyle parent=\"Style3\" me=\"Style132\" /><EditorStyle parent=\"Style5\" me=\"St" +
                "yle133\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style135\" /><GroupFooterStyle pa" +
                "rent=\"Style1\" me=\"Style134\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
                "ght>15</Height><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
                " parent=\"Style2\" me=\"Style136\" /><Style parent=\"Style1\" me=\"Style137\" /><FooterS" +
                "tyle parent=\"Style3\" me=\"Style138\" /><EditorStyle parent=\"Style5\" me=\"Style139\" " +
                "/><GroupHeaderStyle parent=\"Style1\" me=\"Style141\" /><GroupFooterStyle parent=\"St" +
                "yle1\" me=\"Style140\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Co" +
                "lumnDivider><Width>85</Width><Height>15</Height><FooterDivider>False</FooterDivi" +
                "der><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
                "yle2\" me=\"Style142\" /><Style parent=\"Style1\" me=\"Style143\" /><FooterStyle parent" +
                "=\"Style3\" me=\"Style144\" /><EditorStyle parent=\"Style5\" me=\"Style145\" /><GroupHea" +
                "derStyle parent=\"Style1\" me=\"Style147\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
                "tyle146\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider" +
                "><Width>85</Width><Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>" +
                "21</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
                "tyle148\" /><Style parent=\"Style1\" me=\"Style149\" /><FooterStyle parent=\"Style3\" m" +
                "e=\"Style150\" /><EditorStyle parent=\"Style5\" me=\"Style151\" /><GroupHeaderStyle pa" +
                "rent=\"Style1\" me=\"Style153\" /><GroupFooterStyle parent=\"Style1\" me=\"Style152\" />" +
                "<Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>85<" +
                "/Width><Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>22</DCIdx><" +
                "/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style154\" />" +
                "<Style parent=\"Style1\" me=\"Style155\" /><FooterStyle parent=\"Style3\" me=\"Style156" +
                "\" /><EditorStyle parent=\"Style5\" me=\"Style157\" /><GroupHeaderStyle parent=\"Style" +
                "1\" me=\"Style159\" /><GroupFooterStyle parent=\"Style1\" me=\"Style158\" /><Visible>Tr" +
                "ue</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>85</Width><Hei" +
                "ght>15</Height><FooterDivider>False</FooterDivider><DCIdx>23</DCIdx></C1DisplayC" +
                "olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style812\" /><Style pare" +
                "nt=\"Style1\" me=\"Style813\" /><FooterStyle parent=\"Style3\" me=\"Style814\" /><Editor" +
                "Style parent=\"Style5\" me=\"Style815\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
                "e817\" /><GroupFooterStyle parent=\"Style1\" me=\"Style816\" /><Visible>True</Visible" +
                "><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>0</Width><Height>15</Heig" +
                "ht><DCIdx>24</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
                "le2\" me=\"Style842\" /><Style parent=\"Style1\" me=\"Style843\" /><FooterStyle parent=" +
                "\"Style3\" me=\"Style844\" /><EditorStyle parent=\"Style5\" me=\"Style845\" /><GroupHead" +
                "erStyle parent=\"Style1\" me=\"Style847\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
                "yle846\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCId" +
                "x>25</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
                "\"Style872\" /><Style parent=\"Style1\" me=\"Style873\" /><FooterStyle parent=\"Style3\"" +
                " me=\"Style874\" /><EditorStyle parent=\"Style5\" me=\"Style875\" /><GroupHeaderStyle " +
                "parent=\"Style1\" me=\"Style877\" /><GroupFooterStyle parent=\"Style1\" me=\"Style876\" " +
                "/><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>26</DC" +
                "Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style90" +
                "2\" /><Style parent=\"Style1\" me=\"Style903\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
                "le904\" /><EditorStyle parent=\"Style5\" me=\"Style905\" /><GroupHeaderStyle parent=\"" +
                "Style1\" me=\"Style907\" /><GroupFooterStyle parent=\"Style1\" me=\"Style906\" /><Colum" +
                "nDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>27</DCIdx></C1" +
                "DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style932\" /><St" +
                "yle parent=\"Style1\" me=\"Style933\" /><FooterStyle parent=\"Style3\" me=\"Style934\" /" +
                "><EditorStyle parent=\"Style5\" me=\"Style935\" /><GroupHeaderStyle parent=\"Style1\" " +
                "me=\"Style937\" /><GroupFooterStyle parent=\"Style1\" me=\"Style936\" /><ColumnDivider" +
                ">DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>28</DCIdx></C1DisplayC" +
                "olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style962\" /><Style pare" +
                "nt=\"Style1\" me=\"Style963\" /><FooterStyle parent=\"Style3\" me=\"Style964\" /><Editor" +
                "Style parent=\"Style5\" me=\"Style965\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
                "e967\" /><GroupFooterStyle parent=\"Style1\" me=\"Style966\" /><ColumnDivider>DarkGra" +
                "y,Single</ColumnDivider><Height>15</Height><DCIdx>29</DCIdx></C1DisplayColumn><C" +
                "1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style992\" /><Style parent=\"Styl" +
                "e1\" me=\"Style993\" /><FooterStyle parent=\"Style3\" me=\"Style994\" /><EditorStyle pa" +
                "rent=\"Style5\" me=\"Style995\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style997\" />" +
                "<GroupFooterStyle parent=\"Style1\" me=\"Style996\" /><ColumnDivider>DarkGray,Single" +
                "</ColumnDivider><Height>15</Height><DCIdx>30</DCIdx></C1DisplayColumn><C1Display" +
                "Column><HeadingStyle parent=\"Style2\" me=\"Style1022\" /><Style parent=\"Style1\" me=" +
                "\"Style1023\" /><FooterStyle parent=\"Style3\" me=\"Style1024\" /><EditorStyle parent=" +
                "\"Style5\" me=\"Style1025\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style1027\" /><Gr" +
                "oupFooterStyle parent=\"Style1\" me=\"Style1026\" /><ColumnDivider>DarkGray,Single</" +
                "ColumnDivider><Height>15</Height><DCIdx>31</DCIdx></C1DisplayColumn><C1DisplayCo" +
                "lumn><HeadingStyle parent=\"Style2\" me=\"Style1052\" /><Style parent=\"Style1\" me=\"S" +
                "tyle1053\" /><FooterStyle parent=\"Style3\" me=\"Style1054\" /><EditorStyle parent=\"S" +
                "tyle5\" me=\"Style1055\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style1057\" /><Grou" +
                "pFooterStyle parent=\"Style1\" me=\"Style1056\" /><ColumnDivider>DarkGray,Single</Co" +
                "lumnDivider><Height>15</Height><DCIdx>32</DCIdx></C1DisplayColumn></internalCols" +
                "><ClientRect>1764, 0, 0, 141</ClientRect><BorderSide>Left</BorderSide></C1.Win.C" +
                "1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Styl" +
                "e parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pa" +
                "rent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style par" +
                "ent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=" +
                "\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent" +
                "=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style par" +
                "ent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles" +
                "><vertSplits>1</vertSplits><horzSplits>5</horzSplits><Layout>Modified</Layout><D" +
                "efaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1600, 141</ClientArea" +
                "><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" " +
                "me=\"Style15\" /></Blob>";
            // 
            // FundingRateLabel
            // 
            this.FundingRateLabel.Location = new System.Drawing.Point(852, 8);
            this.FundingRateLabel.Name = "FundingRateLabel";
            this.FundingRateLabel.Size = new System.Drawing.Size(88, 18);
            this.FundingRateLabel.TabIndex = 12;
            this.FundingRateLabel.Tag = null;
            this.FundingRateLabel.Text = "Funding Rate";
            this.FundingRateLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.FundingRateLabel.TextDetached = true;
            // 
            // FeeRadioButton
            // 
            this.FeeRadioButton.BackColor = System.Drawing.SystemColors.Control;
            this.FeeRadioButton.Location = new System.Drawing.Point(960, 12);
            this.FeeRadioButton.Name = "FeeRadioButton";
            this.FeeRadioButton.Size = new System.Drawing.Size(100, 16);
            this.FeeRadioButton.TabIndex = 14;
            this.FeeRadioButton.Text = "Fee Rate";
            this.FeeRadioButton.Click += new System.EventHandler(this.SummaryParameterChanged);
            // 
            // RebateRadioButton
            // 
            this.RebateRadioButton.Checked = true;
            this.RebateRadioButton.Location = new System.Drawing.Point(960, 32);
            this.RebateRadioButton.Name = "RebateRadioButton";
            this.RebateRadioButton.Size = new System.Drawing.Size(100, 16);
            this.RebateRadioButton.TabIndex = 15;
            this.RebateRadioButton.TabStop = true;
            this.RebateRadioButton.Text = "Rebate Rate";
            this.RebateRadioButton.Click += new System.EventHandler(this.SummaryParameterChanged);
            // 
            // FundingRateTextBox
            // 
            this.FundingRateTextBox.AutoSize = false;
            this.FundingRateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FundingRateTextBox.DataType = typeof(System.Decimal);
            this.FundingRateTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.FundingRateTextBox.Location = new System.Drawing.Point(856, 28);
            this.FundingRateTextBox.Name = "FundingRateTextBox";
            this.FundingRateTextBox.ReadOnly = true;
            this.FundingRateTextBox.Size = new System.Drawing.Size(80, 18);
            this.FundingRateTextBox.TabIndex = 16;
            this.FundingRateTextBox.Tag = null;
            this.FundingRateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FundingRateTextBox.TextDetached = true;
            // 
            // ClassCodeCheckBox
            // 
            this.ClassCodeCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ClassCodeCheckBox.Checked = true;
            this.ClassCodeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ClassCodeCheckBox.Location = new System.Drawing.Point(444, 30);
            this.ClassCodeCheckBox.Name = "ClassCodeCheckBox";
            this.ClassCodeCheckBox.Size = new System.Drawing.Size(92, 18);
            this.ClassCodeCheckBox.TabIndex = 17;
            this.ClassCodeCheckBox.Text = "Class Code:";
            this.ClassCodeCheckBox.CheckedChanged += new System.EventHandler(this.SummaryParameterChanged);
            // 
            // AutoFilterCheckBox
            // 
            this.AutoFilterCheckBox.Location = new System.Drawing.Point(1080, 32);
            this.AutoFilterCheckBox.Name = "AutoFilterCheckBox";
            this.AutoFilterCheckBox.Size = new System.Drawing.Size(84, 16);
            this.AutoFilterCheckBox.TabIndex = 18;
            this.AutoFilterCheckBox.Text = "Auto Filter";
            this.AutoFilterCheckBox.CheckedChanged += new System.EventHandler(this.AutoFilterCheckBox_CheckedChanged);
            // 
            // SendToMenuItem
            // 
            this.SendToMenuItem.Index = 5;
            this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToExcelMenuItem});
            this.SendToMenuItem.Text = "Send To";
            // 
            // SendToExcelMenuItem
            // 
            this.SendToExcelMenuItem.Index = 0;
            this.SendToExcelMenuItem.Text = "Excel";
            this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
            // 
            // PositionOpenContractsForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1604, 409);
            this.ContextMenu = this.MainContextMenu;
            this.Controls.Add(this.AutoFilterCheckBox);
            this.Controls.Add(this.ClassCodeCheckBox);
            this.Controls.Add(this.RebateRadioButton);
            this.Controls.Add(this.PoolCodeCheckBox);
            this.Controls.Add(this.SummaryCheckBox);
            this.Controls.Add(this.SummaryGrid);
            this.Controls.Add(this.FundingRateTextBox);
            this.Controls.Add(this.FeeRadioButton);
            this.Controls.Add(this.FundingRateLabel);
            this.Controls.Add(this.PositionGroupBox);
            this.Controls.Add(this.SummarySplitter);
            this.Controls.Add(this.ContractsPanel);
            this.Controls.Add(this.BizDateLabel);
            this.Controls.Add(this.BizDateCombo);
            this.Controls.Add(this.BookGroupNameLabel);
            this.Controls.Add(this.BookGroupLabel);
            this.Controls.Add(this.BookGroupCombo);
            this.DockPadding.Top = 56;
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(0, 385);
            this.Name = "PositionOpenContractsForm";
            this.Text = "Position - Open Contracts";
            this.Resize += new System.EventHandler(this.PositionOpenContractsForm_Resize);
            this.Load += new System.EventHandler(this.PositionOpenContractsForm_Load);
            this.Closed += new System.EventHandler(this.PositionOpenContractsForm_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateCombo)).EndInit();
            this.PositionGroupBox.ResumeLayout(false);
            this.ContractsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BorrowsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundingRateLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundingRateTextBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public void ClearSelectedRange()
        {
            SummaryGrid.SelectedCols.Clear();
            SummaryGrid.SelectedRows.Clear();

            BorrowsGrid.SelectedCols.Clear();
            BorrowsGrid.SelectedRows.Clear();

            LoansGrid.SelectedCols.Clear();
            LoansGrid.SelectedRows.Clear();
        }

        public void StatusFlagSet(string bookGroup, string contractId, string contractType, string statusFlag)
        {
            foreach (DataRow dataRow in dataSet.Tables["Contracts"].Rows)
            {
                if (dataRow["BookGroup"].ToString().Equals(bookGroup) &&
                  dataRow["ContractId"].ToString().Equals(contractId) &&
                  dataRow["ContractType"].ToString().Equals(contractType))
                {
                    if ((statusFlag.Equals("E") && dataRow["StatusFlag"].ToString().Equals("S")) || !statusFlag.Equals("E"))
                    {
                        dataRow["StatusFlag"] = statusFlag;
                        dataRow.AcceptChanges();
                    }
                    break;
                }
            }
        }

        public string SecId
        {
            set
            {
                if (!secId.Equals(value) && AutoFilterCheckBox.Checked)
                {
                    mainForm.GridFilterClear(ref SummaryGrid);
                    mainForm.GridFilterClear(ref BorrowsGrid);
                    mainForm.GridFilterClear(ref LoansGrid);

                    SummaryGrid.Columns["SecId"].FilterText = value;
                    BorrowsGrid.Columns["SecId"].FilterText = value;
                    LoansGrid.Columns["SecId"].FilterText = value;
                }
            }

            get
            {
                return secId;
            }
        }

        public bool IsReady
        {
            get
            {
                return isReady;
            }

            set
            {
                ContractEventArgs contractEventArgs;

                try
                {
                    lock (this)
                    {
                        if (value && (contractEventArgsArray.Count > 0))
                        {
                            isReady = false;

                            contractEventArgs = (ContractEventArgs)contractEventArgsArray[0];
                            contractEventArgsArray.RemoveAt(0);

                            contractEventHandler.BeginInvoke(contractEventArgs, null, null);
                        }
                        else
                        {
                            isReady = value;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + ". [PositionOpenContractsForm.IsReady(set)]", Log.Error, 1);
                }
            }
        }

        private void ContractOnEvent(ContractEventArgs contractEventArgs)
        {
            lock (this)
            {
                contractEventArgsArray.Add(contractEventArgs);

                if (this.IsReady) // Force reset to trigger handling of event.
                {
                    this.IsReady = true;
                }
            }
        }

        private void ContractDoEvent(ContractEventArgs contractEventArgs)
        {
            Log.Write("Setting a contract for " + contractEventArgs.BookGroup + " on " +
              contractEventArgs.ContractId + "-" + contractEventArgs.ContractType + " for " +
              contractEventArgs.Quantity + " of " + contractEventArgs.SecId + ". [PositionOpenContractsForm.ContractDoEvent]", 3);

            try
            {
                lock (this)
                {
                    DataConfig(contractEventArgs);
                    this.IsReady = true;
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionOpenContractsForm.ContractDoEvent]", Log.Error, 1);
            }
        }

        private void DataConfig(ContractEventArgs contractEventArgs)
        {
            decimal rebateYieldBorrowed = 0M;
            decimal feeYieldBorrowed = 0M;

            decimal rebateYieldLent = 0M;
            decimal feeYieldLent = 0M;

            string rowKey = "";
            string poolCode = "";
            string baseType = "";

            DataRow summaryRow = null;

            dataSet.Tables["Contracts"].BeginLoadData();
            dataSet.Tables["Summary"].BeginLoadData();

            try
            {
                dataSet.Tables["Contracts"].LoadDataRow(contractEventArgs.Values, true);

                if (SecurityRadioButton.Checked)
                {
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select(
                      "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + contractEventArgs.SecId + "'", "SecId, PoolCode, BaseType");

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["SecId"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                            }

                            rowKey = contractRow["SecId"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = "*";
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = contractRow["SecId"];
                            summaryRow["Symbol"] = contractRow["Symbol"];
                            summaryRow["BaseType"] = contractRow["BaseType"];
                            summaryRow["ClassGroup"] = contractRow["ClassGroup"];
                            summaryRow["IsEasy"] = contractRow["IsEasy"];
                            summaryRow["IsHard"] = contractRow["IsHard"];
                            summaryRow["IsNoLend"] = contractRow["IsNoLend"];
                            summaryRow["IsThreshold"] = contractRow["IsThreshold"];

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                    }
                }

                if (BookRadioButton.Checked)
                {
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select(
                      "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + contractEventArgs.Book + "'", "Book, PoolCode, BaseType");

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["Book"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                            }

                            rowKey = contractRow["Book"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["Book"] = contractRow["Book"];
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                    }
                }

                if (BookParentRadioButton.Checked)
                {
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select(
                      "BookGroup = '" + BookGroupCombo.Text + "' AND BookParent = '" + contractEventArgs.BookParent + "'", "BookParent, PoolCode, BaseType");

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["BookParent"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                            }

                            rowKey = contractRow["BookParent"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = contractRow["PoolCode"].ToString();
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                    }
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionOpenContractsForm.DataConfig]", 1);
                return;
            }

            //dataSet.Tables["Summary"].AcceptChanges();

            if (BookParentRadioButton.Checked)
            {
                CreditUsageSet(contractEventArgs.BookGroup, contractEventArgs.BookParent);
            }

            dataSet.Tables["Contracts"].EndLoadData();
            dataSet.Tables["Summary"].EndLoadData();

            CalcFooterSums();
        }

        private void DataConfig()
        {
            decimal rebateYieldBorrowed = 0M;
            decimal feeYieldBorrowed = 0M;

            decimal rebateYieldLent = 0M;
            decimal feeYieldLent = 0M;

            string rowKey = "";
            string poolCode = "";
            string baseType = "";

            string sortOrder;

            DataRow summaryRow = null;

            dataSet.Tables["Summary"].BeginLoadData();
            dataSet.Tables["Summary"].Rows.Clear();

            ScreenConfig();

            try
            {
                if (SecurityRadioButton.Checked)
                {
                    sortOrder = "SecId" + (PoolCodeCheckBox.Checked ? ", PoolCode" : "") + (ClassCodeCheckBox.Checked ? ", BaseType" : "");
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select("BookGroup = '" + BookGroupCombo.Text + "'", sortOrder);

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["SecId"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].Rows.Add(summaryRow);
                            }

                            rowKey = contractRow["SecId"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = "*";
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = contractRow["SecId"];
                            summaryRow["Symbol"] = contractRow["Symbol"];
                            summaryRow["BaseType"] = contractRow["BaseType"];
                            summaryRow["ClassGroup"] = contractRow["ClassGroup"];
                            summaryRow["IsEasy"] = contractRow["IsEasy"];
                            summaryRow["IsHard"] = contractRow["IsHard"];
                            summaryRow["IsNoLend"] = contractRow["IsNoLend"];
                            summaryRow["IsThreshold"] = contractRow["IsThreshold"];

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].Rows.Add(summaryRow);
                    }
                }

                if (BookRadioButton.Checked)
                {
                    sortOrder = "Book" + (PoolCodeCheckBox.Checked ? ", PoolCode" : "") + (ClassCodeCheckBox.Checked ? ", BaseType" : "");
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select("BookGroup = '" + BookGroupCombo.Text + "'", sortOrder);

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["Book"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].Rows.Add(summaryRow);
                            }

                            rowKey = contractRow["Book"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["Book"] = contractRow["Book"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].Rows.Add(summaryRow);
                    }
                }

                if (BookParentRadioButton.Checked)
                {
                    sortOrder = "BookParent" + (PoolCodeCheckBox.Checked ? ", PoolCode" : "") + (ClassCodeCheckBox.Checked ? ", BaseType" : "");
                    DataRow[] contractRows
                      = dataSet.Tables["Contracts"].Select("BookGroup = '" + BookGroupCombo.Text + "'", sortOrder);

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["BookParent"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].Rows.Add(summaryRow);
                            }

                            rowKey = contractRow["BookParent"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].Rows.Add(summaryRow);
                    }
                }

                dataSet.Tables["Summary"].AcceptChanges();

                if (BookParentRadioButton.Checked)
                {
                    CreditUsageSet();
                }

                dataSet.Tables["Summary"].EndLoadData();
            }
            catch (Exception e)
            {
                mainForm.Alert(e.Message, PilotState.RunFault);
                Log.Write(e.Message + " [PositionOpenContractsForm.DataConfig]", 1);
            }

            CalcFooterSums();
        }

        private void ScreenConfig()
        {
            try
            {
                SummaryGrid.Splits[0, 0].DisplayColumns["BookGroup"].Visible = false;
                SummaryGrid.Splits[0, 0].DisplayColumns["BookParent"].Visible = (BookRadioButton.Checked || BookParentRadioButton.Checked);
                SummaryGrid.Splits[0, 0].DisplayColumns["Book"].Visible = BookRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["SecId"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["Symbol"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["ClassGroup"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsEasy"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsHard"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsNoLend"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsThreshold"].Visible = SecurityRadioButton.Checked;

                SummaryGrid.Splits[0, 0].DisplayColumns["PoolCode"].Visible = PoolCodeCheckBox.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["BaseType"].Visible = ClassCodeCheckBox.Checked;

                SummaryGrid.Splits[0, 1].DisplayColumns["QuantityBorrowed"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 1].DisplayColumns["ValueBorrowed"].Visible = !SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 1].DisplayColumns["ValueBorrowedRatio"].Visible = !SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 2].DisplayColumns["QuantityLent"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 2].DisplayColumns["ValueLent"].Visible = !SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 2].DisplayColumns["ValueLentRatio"].Visible = !SecurityRadioButton.Checked;

                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitBorrow"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitBorrowUsage"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitLoan"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitLoanUsage"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitTotal"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitTotalUsage"].Visible = BookParentRadioButton.Checked;

                SummaryGrid.Splits[0, 4].DisplayColumns["MatchIncome"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 4].DisplayColumns["CashIncome"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 4].DisplayColumns["CashFlow"].Visible = SecurityRadioButton.Checked;

                SummaryGrid.Splits[0, 0].SplitSize = 0;
                SummaryGrid.Splits[0, 1].SplitSize = 0;
                SummaryGrid.Splits[0, 2].SplitSize = 0;
                SummaryGrid.Splits[0, 3].SplitSize = 0;

                if (SecurityRadioButton.Checked)
                {
                    SummaryGrid.Splits[0, 0].SplitSize = 9;
                    SummaryGrid.Splits[0, 0].MinWidth = SummaryGrid.Splits[0, 0].DisplayColumns["Security"].Width
                      + SummaryGrid.Splits[0, 0].RecordSelectorWidth;

                    SummaryGrid.Splits[0, 1].SplitSize = 3;
                    SummaryGrid.Splits[0, 1].MinWidth = SummaryGrid.Splits[0, 1].DisplayColumns["QuantityBorrowed"].Width;

                    SummaryGrid.Splits[0, 2].SplitSize = 3;
                    SummaryGrid.Splits[0, 2].MinWidth = SummaryGrid.Splits[0, 2].DisplayColumns["QuantityLent"].Width;

                    SummaryGrid.Splits[0, 3].SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.Exact;
                    SummaryGrid.Splits[0, 3].SplitSize = 0;
                    SummaryGrid.Splits[0, 3].MinWidth = 50;
                }

                if (BookRadioButton.Checked)
                {
                    SummaryGrid.Splits[0, 0].SplitSize = 5;
                    SummaryGrid.Splits[0, 0].MinWidth = SummaryGrid.Splits[0, 0].DisplayColumns["Book"].Width
                      + SummaryGrid.Splits[0, 0].RecordSelectorWidth;

                    SummaryGrid.Splits[0, 1].SplitSize = 4;
                    SummaryGrid.Splits[0, 1].MinWidth = SummaryGrid.Splits[0, 1].DisplayColumns["AmountBorrowed"].Width;

                    SummaryGrid.Splits[0, 2].SplitSize = 4;
                    SummaryGrid.Splits[0, 2].MinWidth = SummaryGrid.Splits[0, 2].DisplayColumns["AmountLent"].Width;

                    SummaryGrid.Splits[0, 3].SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.Exact;
                    SummaryGrid.Splits[0, 3].SplitSize = 0;
                    SummaryGrid.Splits[0, 3].MinWidth = 50;
                }

                if (BookParentRadioButton.Checked)
                {
                    SummaryGrid.Splits[0, 0].SplitSize = 4;
                    SummaryGrid.Splits[0, 0].MinWidth = SummaryGrid.Splits[0, 0].DisplayColumns["BookParent"].Width
                      + SummaryGrid.Splits[0, 0].RecordSelectorWidth;

                    SummaryGrid.Splits[0, 1].SplitSize = 4;
                    SummaryGrid.Splits[0, 1].MinWidth = SummaryGrid.Splits[0, 1].DisplayColumns["AmountBorrowed"].Width;

                    SummaryGrid.Splits[0, 2].SplitSize = 4;
                    SummaryGrid.Splits[0, 2].MinWidth = SummaryGrid.Splits[0, 2].DisplayColumns["AmountLent"].Width;

                    SummaryGrid.Splits[0, 3].SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.NumberOfColumns;
                    SummaryGrid.Splits[0, 3].SplitSize = 6;
                    SummaryGrid.Splits[0, 3].MinWidth = SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitBorrow"].Width;
                }

                BorrowsGrid.Splits[0, 0].DisplayColumns["BookGroup"].Visible = false;
                BorrowsGrid.Splits[0, 0].DisplayColumns["BookParent"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["Book"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["SecId"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["Symbol"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["ClassGroup"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsEasy"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsHard"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsNoLend"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsThreshold"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 1].DisplayColumns["RebateRate"].Visible = RebateRadioButton.Checked;
                BorrowsGrid.Splits[0, 1].DisplayColumns["FeeRate"].Visible = FeeRadioButton.Checked;

                BorrowsGrid.Splits[0, 0].DisplayColumns["PoolCode"].Visible = (!PoolCodeCheckBox.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["BaseType"].Visible = (!ClassCodeCheckBox.Checked || !SummaryCheckBox.Checked);

                LoansGrid.Splits[0, 0].DisplayColumns["BookGroup"].Visible = false;
                LoansGrid.Splits[0, 0].DisplayColumns["BookParent"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["Book"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["SecId"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["Symbol"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["ClassGroup"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsEasy"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsHard"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsNoLend"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsThreshold"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 1].DisplayColumns["FeeRate"].Visible = FeeRadioButton.Checked;
                LoansGrid.Splits[0, 1].DisplayColumns["RebateRate"].Visible = RebateRadioButton.Checked;

                LoansGrid.Splits[0, 0].DisplayColumns["PoolCode"].Visible = (!PoolCodeCheckBox.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["BaseType"].Visible = (!ClassCodeCheckBox.Checked || !SummaryCheckBox.Checked);

                BorrowsGrid.Splits[0, 0].SplitSize = 0;
                LoansGrid.Splits[0, 0].SplitSize = 0;

                if (SummaryCheckBox.Checked)
                {
                    if (SecurityRadioButton.Checked)
                    {
                        BorrowsGrid.Splits[0, 0].SplitSize = 4;
                        LoansGrid.Splits[0, 0].SplitSize = 4;

                        BorrowsGrid.Splits[0, 0].MinWidth = BorrowsGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                        LoansGrid.Splits[0, 0].MinWidth = LoansGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                    }
                    else
                    {
                        BorrowsGrid.Splits[0, 0].SplitSize = 9;
                        LoansGrid.Splits[0, 0].SplitSize = 9;

                        BorrowsGrid.Splits[0, 0].MinWidth = BorrowsGrid.Splits[0, 0].DisplayColumns["Security"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                        LoansGrid.Splits[0, 0].MinWidth = LoansGrid.Splits[0, 0].DisplayColumns["Security"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                    }
                }
                else
                {
                    BorrowsGrid.Splits[0, 0].SplitSize = 11;
                    LoansGrid.Splits[0, 0].SplitSize = 11;

                    BorrowsGrid.Splits[0, 0].MinWidth = BorrowsGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                      + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                    LoansGrid.Splits[0, 0].MinWidth = LoansGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                      + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                }
            }
            catch (Exception e)
            {
                mainForm.Alert(e.Message, PilotState.RunFault);
                Log.Write(e.Message + ". [PositionOpenContractsForm.ScreenConfig]", 1);
            }
        }

        private void CalcsByContract(bool isNewSummaryRow, DataRow contractRow, ref DataRow summaryRow,
          ref decimal rebateYieldBorrowed, ref decimal feeYieldBorrowed, ref decimal rebateYieldLent, ref decimal feeYieldLent)
        {
            if (isNewSummaryRow)
            {
                switch (contractRow["ContractType"].ToString())
                {
                    case "B":
                        summaryRow["QuantityBorrowed"] = contractRow["Quantity"];
                        summaryRow["AmountBorrowed"] = contractRow["Amount"];
                        summaryRow["ValueBorrowed"] = contractRow["Value"];

                        rebateYieldBorrowed = rebateYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldBorrowed = feeYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        summaryRow["QuantityLent"] = 0L;
                        summaryRow["AmountLent"] = 0M;
                        summaryRow["ValueLent"] = 0M;

                        break;
                    case "L":
                        summaryRow["QuantityBorrowed"] = 0L;
                        summaryRow["AmountBorrowed"] = 0M;
                        summaryRow["ValueBorrowed"] = 0M;

                        rebateYieldLent = rebateYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldLent = feeYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        summaryRow["QuantityLent"] = contractRow["Quantity"];
                        summaryRow["AmountLent"] = contractRow["Amount"];
                        summaryRow["ValueLent"] = contractRow["Value"];

                        break;
                }
            }
            else
            {
                switch (contractRow["ContractType"].ToString())
                {
                    case "B":
                        summaryRow["QuantityBorrowed"] = (long)summaryRow["QuantityBorrowed"] + (long)contractRow["Quantity"];
                        summaryRow["AmountBorrowed"] = (decimal)summaryRow["AmountBorrowed"] + (decimal)contractRow["Amount"];
                        summaryRow["ValueBorrowed"] = (decimal)summaryRow["ValueBorrowed"] + (decimal)contractRow["Value"];

                        rebateYieldBorrowed = rebateYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldBorrowed = feeYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        break;
                    case "L":
                        summaryRow["QuantityLent"] = (long)summaryRow["QuantityLent"] + (long)contractRow["Quantity"];
                        summaryRow["AmountLent"] = (decimal)summaryRow["AmountLent"] + (decimal)contractRow["Amount"];
                        summaryRow["ValueLent"] = (decimal)summaryRow["ValueLent"] + (decimal)contractRow["Value"];

                        rebateYieldLent = rebateYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldLent = feeYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        break;
                }
            }
        }

        private void CalcsBySecurity(ref DataRow summaryRow,
          ref decimal rebateYieldBorrowed, ref decimal feeYieldBorrowed, ref decimal rebateYieldLent, ref decimal feeYieldLent)
        {
            decimal quantityBorrowed = (decimal)(long)summaryRow["QuantityBorrowed"];
            decimal amountBorrowed = (decimal)summaryRow["AmountBorrowed"];

            decimal quantityLent = (decimal)(long)summaryRow["QuantityLent"];
            decimal amountLent = (decimal)summaryRow["AmountLent"];

            // Borrow rate.
            if (amountBorrowed == 0M)
            {
                summaryRow["RateBorrowed"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = rebateYieldBorrowed / amountBorrowed;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = feeYieldBorrowed / amountBorrowed;
                }
            }

            // Loan rate.
            if (amountLent == 0M)
            {
                summaryRow["RateLent"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateLent"] = rebateYieldLent / amountLent;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateLent"] = feeYieldLent / amountLent;
                }
            }

            // Income.
            if (quantityBorrowed > quantityLent)
            {
                summaryRow["Income"] = -((quantityBorrowed - quantityLent) / (quantityBorrowed + E)) * amountBorrowed * (feeYieldBorrowed / (amountBorrowed + E)) / 36000M;
            }
            else if (quantityBorrowed < quantityLent)
            {
                summaryRow["Income"] = ((quantityLent - quantityBorrowed) / (quantityLent + E)) * amountLent * (feeYieldLent / (amountLent + E)) / 36000M;
            }
            else if (quantityBorrowed.Equals(0) && quantityLent.Equals(0))
            {
                summaryRow["Income"] = -(amountBorrowed * (feeYieldBorrowed / (amountBorrowed + E)) / 36000M) + (amountLent * (feeYieldLent / (amountLent + E)) / 36000M);
            }
            else
            {
                summaryRow["Income"] = 0D;
            }

            // Match P/L.
            decimal d1 = 0M;
            decimal d2 = 0M;
            decimal d3 = 0M;

            if ((amountLent / (quantityLent + E)) < (amountBorrowed / (quantityBorrowed + E)))
            {
                d1 = amountLent / (quantityLent + E);
            }
            else
            {
                d1 = amountBorrowed / (quantityBorrowed + E);
            }

            if ((quantityBorrowed > 0) && (quantityLent > 0))
            {
                if (quantityBorrowed < quantityLent)
                {
                    d2 = quantityBorrowed;
                }
                else
                {
                    d2 = quantityLent;
                }

                d2 = d2 * (feeYieldLent / (amountLent + E) - feeYieldBorrowed / (amountBorrowed + E)) / 36000M;

                d3 = (feeYieldLent / (amountLent + E) - feeYieldBorrowed / (amountBorrowed + E));
            }
            else
            {
                d2 = 0M;
            }

            summaryRow["MatchIncome"] = d1 * d2;
            //summaryRow["Spread"] = d3 * 100;

            // Cash P/L.
            d1 = 0M;
            d2 = 0M;
            d3 = 0M;

            d1 = ((amountLent / (quantityLent + E)) - (amountBorrowed / (quantityBorrowed + E)));

            if ((quantityBorrowed > 0) && (quantityLent > 0))
            {
                if (quantityBorrowed < quantityLent)
                {
                    d2 = quantityBorrowed;
                }
                else
                {
                    d2 = quantityLent;
                }

                if ((amountLent / (quantityLent + E)) - (amountBorrowed / (quantityBorrowed + E)) < 0)
                {
                    d3 = feeYieldBorrowed / (amountBorrowed + E) / 36000M;
                }
                else
                {
                    d3 = feeYieldLent / (amountLent + E) / 36000M;
                }
            }
            else
            {
                d2 = 0M;
                d3 = 0M;
            }

            summaryRow["CashIncome"] = d1 * d2 * d3;

            // Cash flow.
            d1 = 0M;
            d2 = 0M;
            d3 = 0M;

            d1 = ((amountLent / (quantityLent + E)) - (amountBorrowed / (quantityBorrowed + E)));

            if ((quantityBorrowed > 0) && (quantityLent > 0))
            {
                if (quantityBorrowed < quantityLent)
                {
                    d2 = quantityBorrowed;
                }
                else
                {
                    d2 = quantityLent;
                }
            }
            else
            {
                d2 = 0M;
            }

            summaryRow["CashFlow"] = d1 * d2;

            rebateYieldBorrowed = 0M;
            feeYieldBorrowed = 0M;
            rebateYieldLent = 0M;
            feeYieldLent = 0M;
        }

        private void CalcsByBook(ref DataRow summaryRow,
          ref decimal rebateYieldBorrowed, ref decimal feeYieldBorrowed, ref decimal rebateYieldLent, ref decimal feeYieldLent)
        {
            decimal quantityBorrowed = (decimal)(long)summaryRow["QuantityBorrowed"];
            decimal amountBorrowed = (decimal)summaryRow["AmountBorrowed"];

            decimal quantityLent = (decimal)(long)summaryRow["QuantityLent"];
            decimal amountLent = (decimal)summaryRow["AmountLent"];

            // Borrow rate.
            if (amountBorrowed == 0M)
            {
                summaryRow["RateBorrowed"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = rebateYieldBorrowed / amountBorrowed;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = feeYieldBorrowed / amountBorrowed;
                }
            }

            // Borrow value ratio.
            if ((decimal)summaryRow["ValueBorrowed"] > 0M)
            {
                summaryRow["ValueBorrowedRatio"] = (decimal)summaryRow["AmountBorrowed"] / (decimal)summaryRow["ValueBorrowed"];
            }

            // Loan rate.
            if (amountLent == 0M)
            {
                summaryRow["RateLent"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateLent"] = rebateYieldLent / amountLent;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateLent"] = feeYieldLent / amountLent;
                }
            }

            // Loan value ratio.
            if ((decimal)summaryRow["ValueLent"] > 0M)
            {
                summaryRow["ValueLentRatio"] = (decimal)summaryRow["AmountLent"] / (decimal)summaryRow["ValueLent"];
            }

            // Income.
            summaryRow["Income"] = (amountLent * (feeYieldLent / (amountLent + E)) / 36000M) - (amountBorrowed * (feeYieldBorrowed / (amountBorrowed + E)) / 36000M);

            // Match P/L.
            summaryRow["MatchIncome"] = 0M;

            //summaryRow["Spread"] = d3 * 100;

            // Cash P/L.
            summaryRow["CashIncome"] = 0M;

            // Cash flow.
            summaryRow["CashFlow"] = 0M;

            rebateYieldBorrowed = 0M;
            feeYieldBorrowed = 0M;
            rebateYieldLent = 0M;
            feeYieldLent = 0M;
        }

        private void CreditUsageSet()
        {
            decimal amountBorrowedTotal = 0M;
            decimal amountLentTotal = 0M;

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Rows)
            {
                DataRow[] bookRow = dataSet.Tables["Books"].Select("BookGroup = '" + summaryRow["BookGroup"] + "' AND Book = '" + summaryRow["BookParent"] + "'");

                AmountTotalGet(summaryRow["BookGroup"].ToString(), summaryRow["BookParent"].ToString(), ref amountBorrowedTotal, ref amountLentTotal);

                try
                {
                    summaryRow["AmountLimitBorrow"] = (long)bookRow[0]["AmountLimitBorrow"] * ((decimal)summaryRow["AmountBorrowed"] / amountBorrowedTotal);
                    summaryRow["AmountLimitLoan"] = (long)bookRow[0]["AmountLimitLoan"] * ((decimal)summaryRow["AmountLent"] / amountLentTotal);
                    summaryRow["AmountLimitTotal"] = ((long)bookRow[0]["AmountLimitBorrow"] + (long)bookRow[0]["AmountLimitLoan"])
                      * (((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (amountBorrowedTotal + amountLentTotal));

                    if ((decimal)summaryRow["AmountLimitBorrow"] > 0M)
                    {
                        summaryRow["AmountLimitBorrowUsage"] = 100M * (decimal)summaryRow["AmountBorrowed"] / (decimal)summaryRow["AmountLimitBorrow"];
                    }

                    if ((decimal)summaryRow["AmountLimitLoan"] > 0M)
                    {
                        summaryRow["AmountLimitLoanUsage"] = 100M * (decimal)summaryRow["AmountLent"] / (decimal)summaryRow["AmountLimitLoan"];
                    }

                    if ((decimal)summaryRow["AmountLimitTotal"] > 0M)
                    {
                        summaryRow["AmountLimitTotalUsage"] = 100M * ((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (decimal)summaryRow["AmountLimitTotal"];
                    }

                    summaryRow.AcceptChanges();
                }
                catch { }
            }

            //dataSet.Tables["Summary"].AcceptChanges();
        }

        private void CreditUsageSet(string bookGroup, string bookParent)
        {
            decimal amountBorrowedTotal = 0M;
            decimal amountLentTotal = 0M;

            DataRow[] bookRow = dataSet.Tables["Books"].Select("BookGroup = '" + bookGroup + "' AND Book = '" + bookParent + "'");

            AmountTotalGet(bookGroup, bookParent, ref amountBorrowedTotal, ref amountLentTotal);

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Rows)
            {
                try
                {
                    summaryRow["AmountLimitBorrow"] = (long)bookRow[0]["AmountLimitBorrow"] * ((decimal)summaryRow["AmountBorrowed"] / amountBorrowedTotal);
                    summaryRow["AmountLimitLoan"] = (long)bookRow[0]["AmountLimitLoan"] * ((decimal)summaryRow["AmountLent"] / amountLentTotal);
                    summaryRow["AmountLimitTotal"] = ((long)bookRow[0]["AmountLimitBorrow"] + (long)bookRow[0]["AmountLimitLoan"])
                      * (((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (amountBorrowedTotal + amountLentTotal));

                    if ((decimal)summaryRow["AmountLimitBorrow"] > 0M)
                    {
                        summaryRow["AmountLimitBorrowUsage"] = 100M * (decimal)summaryRow["AmountBorrowed"] / (decimal)summaryRow["AmountLimitBorrow"];
                    }

                    if ((decimal)summaryRow["AmountLimitLoan"] > 0M)
                    {
                        summaryRow["AmountLimitLoanUsage"] = 100M * (decimal)summaryRow["AmountLent"] / (decimal)summaryRow["AmountLimitLoan"];
                    }

                    if ((decimal)summaryRow["AmountLimitTotal"] > 0M)
                    {
                        summaryRow["AmountLimitTotalUsage"] = 100M * ((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (decimal)summaryRow["AmountLimitTotal"];
                    }

                    summaryRow.AcceptChanges();
                }
                catch { }
            }

            //dataSet.Tables["Summary"].AcceptChanges();
        }

        private void AmountTotalGet(string bookParent, ref decimal amountBorrowedTotal, ref decimal amountLentTotal)
        {
            amountBorrowedTotal = 0M;
            amountLentTotal = 0M;

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Select("BookParent = '" + bookParent + "'"))
            {
                amountBorrowedTotal += (decimal)summaryRow["AmountBorrowed"];
                amountLentTotal += (decimal)summaryRow["AmountLent"];
            }
        }

        private void AmountTotalGet(string bookGroup, string bookParent, ref decimal amountBorrowedTotal, ref decimal amountLentTotal)
        {
            amountBorrowedTotal = 0M;
            amountLentTotal = 0M;

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Select("BookGroup = '" + bookGroup + "' AND BookParent = '" + bookParent + "'"))
            {
                amountBorrowedTotal += (decimal)summaryRow["AmountBorrowed"];
                amountLentTotal += (decimal)summaryRow["AmountLent"];
            }
        }

        private void CalcFooterSums()
        {
            decimal rateYieldBorrowed = 0M;
            decimal rateYieldLent = 0M;

            decimal amountBorrowed = 0M;
            decimal amountLent = 0M;

            decimal income = 0M;
            decimal matchIncome = 0M;
            decimal cashIncome = 0M;
            decimal cashFlow = 0M;

            decimal rate;

            foreach (DataRowView summaryRow in summaryDataView)
            {
                rateYieldBorrowed += (decimal)summaryRow["RateBorrowed"] * (decimal)summaryRow["AmountBorrowed"];
                amountBorrowed += (decimal)summaryRow["AmountBorrowed"];

                rateYieldLent += (decimal)summaryRow["RateLent"] * (decimal)summaryRow["AmountLent"];
                amountLent += (decimal)summaryRow["AmountLent"];

                income += (decimal)summaryRow["Income"];
                matchIncome += (decimal)summaryRow["MatchIncome"];
                cashIncome += (decimal)summaryRow["CashIncome"];
                cashFlow += (decimal)summaryRow["CashFlow"];
            }

            rate = rateYieldBorrowed / (amountBorrowed + E);
            SummaryGrid.Columns["RateBorrowed"].FooterText = rate.ToString("0.000");

            rate = rateYieldLent / (amountLent + E);
            SummaryGrid.Columns["RateLent"].FooterText = rate.ToString("0.000");

            SummaryGrid.Columns["AmountBorrowed"].FooterText = amountBorrowed.ToString("#,##0");
            SummaryGrid.Columns["AmountLent"].FooterText = amountLent.ToString("#,##0");
            SummaryGrid.Columns["Income"].FooterText = income.ToString("#,##0.00");
            SummaryGrid.Columns["MatchIncome"].FooterText = matchIncome.ToString("#,##0.00");
            SummaryGrid.Columns["CashIncome"].FooterText = cashIncome.ToString("#,##0.00");
            SummaryGrid.Columns["CashFlow"].FooterText = cashFlow.ToString("#,##0");
        }

        private void FilterConfig()
        {
            string codeFilter = "";

            if (PoolCodeCheckBox.Checked)
            {
                codeFilter = " AND PoolCode = '" + SummaryGrid.Columns["PoolCode"].Text + "'";
            }

            if (ClassCodeCheckBox.Checked)
            {
                codeFilter += " AND BaseType = '" + SummaryGrid.Columns["BaseType"].Text + "'";
            }

            mainForm.GridFilterClear(ref BorrowsGrid);
            mainForm.GridFilterClear(ref LoansGrid);

            if (!SummaryCheckBox.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'";
                borrowsDataView.Sort = "SecId";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'";
                loansDataView.Sort = "SecId";
            }
            else if (SecurityRadioButton.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND SecId = '"
                  + SummaryGrid.Columns["SecId"].Text + "'" + codeFilter;
                borrowsDataView.Sort = "BookParent, Book";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND SecId = '"
                  + SummaryGrid.Columns["SecId"].Text + "'" + codeFilter;
                loansDataView.Sort = "BookParent, Book";

                mainForm.SecId = SummaryGrid.Columns["SecId"].Text;
            }
            else if (BookRadioButton.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND Book = '"
                  + SummaryGrid.Columns["Book"].Text + "'" + codeFilter;
                borrowsDataView.Sort = "SecId";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND Book = '"
                  + SummaryGrid.Columns["Book"].Text + "'" + codeFilter;
                loansDataView.Sort = "SecId";
            }
            else if (BookParentRadioButton.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND BookParent = '"
                  + SummaryGrid.Columns["BookParent"].Text + "'" + codeFilter;
                borrowsDataView.Sort = "SecId, Book";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND BookParent = '"
                  + SummaryGrid.Columns["BookParent"].Text + "'" + codeFilter;
                loansDataView.Sort = "SecId, Book";
            }
        }

        private void PositionOpenContractsForm_Load(object sender, System.EventArgs e)
        {


            int height = this.MinimumSize.Height;
            int width = mainForm.Width - 75;

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["Income"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["Value"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = false;

            LoansGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["Income"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["Value"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = false;

            SummaryGrid.Splits[0, 4].MinWidth = 100;
            BorrowsGrid.Splits[0, 1].MinWidth = 150;
            LoansGrid.Splits[0, 1].MinWidth = 150;

            this.Show();
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            mainForm.Alert("Please wait... loading current contract data...", PilotState.Unknown);

            try
            {
                mainForm.PositionAgent.ContractEvent += new ContractEventHandler(contractEventWrapper.DoEvent);

                dataSet = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, null, mainForm.UserId, "PositionOpenContracts");

                dataSet.Tables.Add("Summary");
                dataSet.Tables["Summary"].Columns.Add("BookGroup", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("BookParent", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("Book", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("PoolCode", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("SecId", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("Symbol", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("BaseType", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("ClassGroup", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("IsEasy", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("IsHard", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("IsNoLend", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("IsThreshold", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("QuantityBorrowed", typeof(long));
                dataSet.Tables["Summary"].Columns.Add("AmountBorrowed", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("RateBorrowed", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("QuantityLent", typeof(long));
                dataSet.Tables["Summary"].Columns.Add("AmountLent", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("RateLent", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitBorrow", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitBorrowUsage", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitLoan", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitLoanUsage", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitTotal", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitTotalUsage", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("Income", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("MatchIncome", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("CashIncome", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("CashFlow", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("ValueBorrowed", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("ValueBorrowedRatio", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("ValueLent", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("ValueLentRatio", typeof(float));

                dataSet.Tables["Summary"].PrimaryKey = new DataColumn[6]
          {
            dataSet.Tables["Summary"].Columns["BookGroup"],
            dataSet.Tables["Summary"].Columns["BookParent"],
            dataSet.Tables["Summary"].Columns["Book"],
            dataSet.Tables["Summary"].Columns["PoolCode"],
            dataSet.Tables["Summary"].Columns["SecId"],
            dataSet.Tables["Summary"].Columns["BaseType"] };

                summaryDataView = new DataView(dataSet.Tables["Summary"]);
                SummaryGrid.SetDataBinding(summaryDataView, null, true);

                borrowsDataView = new DataView(dataSet.Tables["Contracts"]);
                BorrowsGrid.SetDataBinding(borrowsDataView, null, true);

                loansDataView = new DataView(dataSet.Tables["Contracts"]);
                LoansGrid.SetDataBinding(loansDataView, null, true);

                BookGroupCombo.HoldFields();
                BookGroupCombo.ValueMember = "BookGroup";
                BookGroupCombo.DataMember = "BookGroups";
                BookGroupCombo.DataSource = dataSet;

                BookGroupCombo.Text = dataSet.Tables["BookGroups"].Rows[0]["BookGroup"].ToString();

                DataConfig();
                FilterConfig();

                BookGroupNameLabel.DataSource = dataSet.Tables["BookGroup"];
                BookGroupNameLabel.DataField = "BookName";

                BizDateCombo.HoldFields();
                BizDateCombo.ValueMember = "BizDate";
                BizDateCombo.DataMember = "BizDates";
                BizDateCombo.DataSource = dataSet;

                BizDateCombo.Text = dataSet.Tables["BizDates"].Rows[0]["BizDate"].ToString();

                foreach (DataRow dataRow in dataSet.Tables["BizDates"].Rows)
                {
                    bizDateArray.Add(dataRow["BizDate"].ToString());
                }

                SummaryGrid.Visible = true;
                BorrowsGrid.Visible = true;
                LoansGrid.Visible = true;

                Enabled = true;
                this.IsReady = true;

                mainForm.Alert("Loading current contract data... Done!", PilotState.Normal);
                this.Cursor = Cursors.Default;
            }
            catch (Exception error)
            {
                Enabled = true;
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + " [PositionOpenContractsForm.PositionOpenContractsForm_Load]", Log.Error, 1);
            }
        }

        private void PositionOpenContractsForm_Closed(object sender, System.EventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            try
            {
                if (contractEventWrapper != null)
                {
                    mainForm.PositionAgent.ContractEvent -= new ContractEventHandler(contractEventWrapper.DoEvent);
                    contractEventWrapper.ContractEvent -= new ContractEventHandler(ContractOnEvent);
                    contractEventWrapper = null;
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [PositionOpenContractsForm.PositionDealBlotterForm_Closed]", Log.Error, 1);
            }

            mainForm.positionOpenContractsForm = null;
        }

        private void PositionOpenContractsForm_Resize(object sender, System.EventArgs e)
        {
            if ((this.Height - ContractsPanel.Height) < MINIMUM_SUMMARY_HEIGHT_BUFFER)
            {
                ContractsPanel.Height = this.Height - MINIMUM_SUMMARY_HEIGHT_BUFFER;
            }
        }

        private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
        {
            if (!BookGroupCombo.Text.Equals(""))
            {
                DataConfig();
                FilterConfig();
            }
        }

        private void SummaryCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            SummaryGrid.Visible = SummaryCheckBox.Checked;
            SummarySplitter.Visible = SummaryCheckBox.Checked;

            PositionGroupBox.Enabled = SummaryCheckBox.Checked;
            PoolCodeCheckBox.Enabled = SummaryCheckBox.Checked;
            ClassCodeCheckBox.Enabled = SummaryCheckBox.Checked;

            BorrowsGrid.FilterBar = !SummaryCheckBox.Checked;
            LoansGrid.FilterBar = !SummaryCheckBox.Checked;

            if (SummaryCheckBox.Checked)
            {
                ContractsPanel.Dock = DockStyle.Bottom;
            }
            else
            {
                ContractsPanel.Dock = DockStyle.Fill;
            }

            DataConfig();
            FilterConfig();

            this.Cursor = Cursors.Default;
        }

        private void ContractsPanel_Resize(object sender, System.EventArgs e)
        {
            ContractsSplitter.SplitPosition = ContractsSplitter.SplitPosition + ((ContractsPanel.Height - (int)ContractsPanel.Tag) / 2);
            ContractsPanel.Tag = ContractsPanel.Height;
        }

        private void BorrowsGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (borrowsDataView.RowFilter.Equals(""))
            {
                borrowsDataView.RowFilter = "ContractType = 'B' AND BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                borrowsDataView.RowFilter = "ContractType = 'B' AND BookGroup = '" + BookGroupCombo.Text + "' AND " + borrowsDataView.RowFilter;
            }
        }

        private void BorrowsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (BorrowsGrid.Focused)
            {
                try
                {
                    string s = BorrowsGrid.Columns["SecId"].Text;

                    if (!s.Equals(secId))
                    {
                        secId = s;
                        mainForm.SecId = s;
                    }
                }
                catch
                {
                    secId = "";
                }
            }
        }

        private void BorrowsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            switch (BorrowsGrid.Columns["StatusFlag"].CellText(e.Row))
            {
                case "S":
                    e.CellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
                    break;
                case "E":
                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                    break;
                default:
                    e.CellStyle.BackColor = System.Drawing.Color.Ivory;
                    break;
            }
        }

        private void LoansGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (loansDataView.RowFilter.Equals(""))
            {
                loansDataView.RowFilter = "ContractType = 'L' AND BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                loansDataView.RowFilter = "ContractType = 'L' AND BookGroup = '" + BookGroupCombo.Text + "' AND " + loansDataView.RowFilter;
            }
        }

        private void LoansGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (LoansGrid.Focused)
            {
                try
                {
                    string s = LoansGrid.Columns["SecId"].Text;

                    if (!s.Equals(secId))
                    {
                        secId = s;
                        mainForm.SecId = s;
                    }
                }
                catch
                {
                    secId = "";
                }
            }
        }

        private void LoansGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            switch (LoansGrid.Columns["StatusFlag"].CellText(e.Row))
            {
                case "S":
                    e.CellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
                    break;
                case "E":
                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                    break;
                default:
                    e.CellStyle.BackColor = System.Drawing.Color.Honeydew;
                    break;
            }
        }

        private void SummaryParameterChanged(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            mainForm.GridFilterClear(ref SummaryGrid);
            mainForm.GridFooterClear(ref SummaryGrid);

            DataConfig();
            FilterConfig();

            this.Cursor = Cursors.Default;
        }

        private void BizDateCombo_RowChange(object sender, System.EventArgs e)
        {
            if (!BizDateCombo.SelectedText.Equals("")) // Already initialized; will need to load data for a new business date.
            {
                mainForm.Alert("Please wait... Loading contract data for " + BizDateCombo.Text + "...", PilotState.Unknown);
                this.Cursor = Cursors.WaitCursor;

                mainForm.PositionAgent.ContractEvent -= new ContractEventHandler(contractEventWrapper.DoEvent);
                contractEventArgsArray.Clear();
                this.IsReady = false;

                dataSet.Tables["Summary"].Rows.Clear();
                dataSet.Tables["Contracts"].Rows.Clear();

                mainForm.GridFooterClear(ref SummaryGrid);

                mainForm.GridFilterClear(ref SummaryGrid);
                mainForm.GridFilterClear(ref BorrowsGrid);
                mainForm.GridFilterClear(ref LoansGrid);

                Application.DoEvents();

                try
                {
                    if (BizDateCombo.SelectedIndex.Equals(0))
                    {
                        mainForm.PositionAgent.ContractEvent += new ContractEventHandler(contractEventWrapper.DoEvent);
                    }

                    DataRow[] rows = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, BizDateCombo.Text, "", "").Tables["Contracts"].Select();

                    dataSet.Tables["Contracts"].BeginLoadData();
                    foreach (DataRow row in rows)
                    {
                        dataSet.Tables["Contracts"].ImportRow(row);
                    }
                    dataSet.Tables["Contracts"].EndLoadData();

                    int rowIndex = -1;

                    foreach (DataRow dataRowTemp in dataSet.Tables["BookGroups"].Rows)
                    {
                        rowIndex++;

                        if (dataRowTemp["BookGroup"].ToString().Equals(BookGroupCombo.Text))
                        {
                            break;
                        }
                    }

                    if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayView"].ToString()))
                    {

                        RateChangeMenuItem.Enabled = false;
                        PcChangeMenuItem.Enabled = false;
                        ReturnMenuItem.Enabled = false;
                        RecallMenuItem.Enabled = false;

                        if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayEdit"].ToString()))
                        {
                            RateChangeMenuItem.Enabled = true;
                            PcChangeMenuItem.Enabled = true;
                            ReturnMenuItem.Enabled = true;

                            RecallMenuItem.Enabled = mainForm.AdminAgent.MayEditBookGroup(mainForm.UserId, "PositionRecalls", BookGroupCombo.Text);
                        }
                        else
                        {
                            mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
                        }
                    }
                    else
                    {
                        loansDataView.RowFilter = "BookGroup = ''";
                        borrowsDataView.RowFilter = "BookGroup = ''";
                        mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
                    }

                    mainForm.Alert("Loading contract data for " + BizDateCombo.Text + "... Done!", PilotState.Normal);
                }
                catch (Exception error)
                {
                    mainForm.Alert(error.Message, PilotState.RunFault);
                    Log.Write(error.Message + " [PositionOpenContractsForm.BizDateCombo_RowChange]", 1);
                }

                DataConfig();

                this.Cursor = Cursors.Default;
            }
        }

        private void BizDateBookGroup_TextChanged(object sender, System.EventArgs e)
        {
            DataRow[] dataRow = dataSet.Tables["BookGroups"].Select("BookGroup = '" + BookGroupCombo.Text + "'");

            if (dataRow.Length.Equals(1))
            {
                FundingRateTextBox.Text = dataRow[0]["FundingRate"].ToString() + "% [" + dataRow[0]["DayCount"].ToString() + "]";
            }

            int rowIndex = -1;

            foreach (DataRow dataRowTemp in dataSet.Tables["BookGroups"].Rows)
            {
                rowIndex++;

                if (dataRowTemp["BookGroup"].ToString().Equals(BookGroupCombo.Text))
                {
                    break;
                }
            }

            if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayView"].ToString()))
            {

                RateChangeMenuItem.Enabled = false;
                PcChangeMenuItem.Enabled = false;
                ReturnMenuItem.Enabled = false;
                RecallMenuItem.Enabled = false;
                SummaryGrid.FilterBar = true;
                LoansGrid.FilterBar = true;
                BorrowsGrid.FilterBar = true;
                SummaryCheckBox.Enabled = true;
                PoolCodeCheckBox.Enabled = true;
                ClassCodeCheckBox.Enabled = true;
                AutoFilterCheckBox.Enabled = true;

                loansDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                borrowsDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                summaryDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";


                if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayEdit"].ToString()))
                {
                    RateChangeMenuItem.Enabled = true;
                    PcChangeMenuItem.Enabled = true;
                    ReturnMenuItem.Enabled = true;

                    RecallMenuItem.Enabled = mainForm.AdminAgent.MayEditBookGroup(mainForm.UserId, "PositionRecalls", BookGroupCombo.Text);
                }
                else
                {
                    mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
                }
            }
            else
            {
                loansDataView.RowFilter = "BookGroup = ''";
                borrowsDataView.RowFilter = "BookGroup = ''";
                summaryDataView.RowFilter = "BookGroup = ''";
                SummaryGrid.FilterBar = false;
                LoansGrid.FilterBar = false;
                BorrowsGrid.FilterBar = false;
                SummaryCheckBox.Enabled = false;
                PoolCodeCheckBox.Enabled = false;
                ClassCodeCheckBox.Enabled = false;
                AutoFilterCheckBox.Enabled = false;

                mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
            }
        }

        private void AutoFilterCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!AutoFilterCheckBox.Checked)
            {
                mainForm.GridFilterClear(ref SummaryGrid);
                mainForm.GridFilterClear(ref BorrowsGrid);
                mainForm.GridFilterClear(ref LoansGrid);
            }
        }

        private void SummaryGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            string s = SummaryGrid.Columns["BookGroup"].Text + "|"
              + SummaryGrid.Columns["BookParent"].Text + "|"
              + SummaryGrid.Columns["Book"].Text + "|"
              + SummaryGrid.Columns["PoolCode"].Text + "|"
              + SummaryGrid.Columns["SecId"].Text + "|"
              + SummaryGrid.Columns["BaseType"].Text;

            if (!s.Equals(rowIdentity))
            {
                rowIdentity = s;

                FilterConfig();
            }
        }

        private void SummaryGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            CalcFooterSums();
        }

        private void SummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0) // Nothing to format.
            {
                return;
            }

            switch (e.Column.DataField)
            {
                case ("QuantityBorrowed"):
                case ("AmountBorrowed"):
                case ("ValueBorrowed"):
                case ("QuantityLent"):
                case ("AmountLent"):
                case ("ValueLent"):
                case ("AmountLimitBorrow"):
                case ("AmountLimitLoan"):
                case ("AmountLimitTotal"):
                case ("CashFlow"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;

                case ("RateBorrowed"):
                case ("RateLent"):
                case ("ValueBorrowedRatio"):
                case ("ValueLentRatio"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.000");
                    }
                    catch { }
                    break;

                case ("AmountLimitBorrowUsage"):
                case ("AmountLimitLoanUsage"):
                case ("AmountLimitTotalUsage"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.0");
                    }
                    catch { }
                    break;

                case ("Income"):
                case ("MatchIncome"):
                case ("CashIncome"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("#0.00");
                    }
                    catch { }
                    break;
            }
        }

        private void ContractsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0) // Nothing to format.
            {
                return;
            }

            switch (e.Column.DataField)
            {
                case ("Quantity"):
                case ("QuantitySettled"):
                case ("QuantityRecalled"):
                case ("Amount"):
                case ("AmountSettled"):
                case ("Value"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;

                case ("Margin"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.00");
                    }
                    catch { }
                    break;

                case ("Rate"):
                case ("FeeRate"):
                case ("RebateRate"):
                case ("ValueRatio"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.000");
                    }
                    catch { }
                    break;

                case ("Income"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.00");
                    }
                    catch { }
                    break;

                case ("DivRate"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.0");
                    }
                    catch { }
                    break;

                case ("ValueDate"):
                case ("SettleDate"):
                case ("TermDate"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
                    }
                    catch { }
                    break;
            }
        }

        private void RecallMenuItem_Click(object sender, System.EventArgs e)
        {
            string secId = "";
            bool hasError = false;
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.Focused)
            {
                if (BorrowsGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else if (BorrowsGrid.SelectedRows.Count == 1)
                {
                    dataRowView = (DataRowView)BorrowsGrid[0];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    hasError = true;
                }
            }

            if (LoansGrid.Focused)
            {
                if (LoansGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)LoansGrid[LoansGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;
                    dataRows.Add(dataRow);
                }
                else
                {
                    foreach (int i in LoansGrid.SelectedRows)
                    {
                        if (secId.Equals(""))
                        {
                            secId = LoansGrid[i, "SecId"].ToString();
                        }

                        if (secId.Equals(LoansGrid[i, "SecId"].ToString()))
                        {
                            dataRowView = (DataRowView)LoansGrid[i];
                            dataRow = dataRowView.Row.Table.NewRow();
                            dataRow.ItemArray = dataRowView.Row.ItemArray;

                            dataRows.Add(dataRow);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                }
            }

            if (!hasError && (dataRows.Count > 0))
            {
                try
                {
                    PositionRecallInputForm positionRecallInputForm = new PositionRecallInputForm(mainForm, dataRows);
                    positionRecallInputForm.MdiParent = mainForm;
                    positionRecallInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.RecallMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                if ((BorrowsGrid.SelectedRows.Count > 1) && BorrowsGrid.Focused)
                {
                    mainForm.Alert("Must select just one contract for enter a borrow recall.", PilotState.RunFault);
                }
                else if (dataRows.Count == 0)
                {
                    mainForm.Alert("Must select at least one contract for recall.", PilotState.RunFault);
                }
                else
                {
                    mainForm.Alert("Must have just one security in the range for recall.", PilotState.RunFault);
                }
            }
        }

        private void ReturnMenuItem_Click(object sender, System.EventArgs e)
        {
            string secId = "";
            bool hasError = false;
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.SelectedRows.Count == 0)
            {
                dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                dataRow = dataRowView.Row.Table.NewRow();
                dataRow.ItemArray = dataRowView.Row.ItemArray;

                dataRows.Add(dataRow);
            }
            else
            {
                BorrowsGrid.Row = BorrowsGrid.SelectedRows[0];

                foreach (int i in BorrowsGrid.SelectedRows)
                {
                    if (secId.Equals(""))
                    {
                        secId = BorrowsGrid[i, "SecId"].ToString();
                    }

                    if (secId.Equals(BorrowsGrid[i, "SecId"].ToString()))
                    {
                        dataRowView = (DataRowView)BorrowsGrid[i];
                        dataRow = dataRowView.Row.Table.NewRow();
                        dataRow.ItemArray = dataRowView.Row.ItemArray;

                        dataRows.Add(dataRow);
                    }
                    else
                    {
                        hasError = true;
                    }
                }
            }

            if (!hasError && (dataRows.Count > 0))
            {
                try
                {
                    PositionReturnInputForm positionReturnInputForm = new PositionReturnInputForm(mainForm, dataRows);
                    positionReturnInputForm.MdiParent = mainForm;
                    positionReturnInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.ReturnMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                if (dataRows.Count == 0)
                {
                    mainForm.Alert("Must select at least one contract for return.", PilotState.RunFault);
                }
                else
                {
                    mainForm.Alert("Must have just one security in the range for return.", PilotState.RunFault);
                }
            }
        }

        private void RateChangeMenuItem_Click(object sender, System.EventArgs e)
        {
            string book = "";
            bool hasError = false;
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.Focused)
            {
                if (BorrowsGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    BorrowsGrid.Row = BorrowsGrid.SelectedRows[0];

                    foreach (int i in BorrowsGrid.SelectedRows)
                    {
                        if (book.Equals("")) // Initialize.
                        {
                            book = BorrowsGrid[i, "Book"].ToString();
                        }

                        if (book.Equals(BorrowsGrid[i, "Book"].ToString()))
                        {
                            dataRowView = (DataRowView)BorrowsGrid[i];
                            dataRow = dataRowView.Row.Table.NewRow();
                            dataRow.ItemArray = dataRowView.Row.ItemArray;

                            dataRows.Add(dataRow);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                }
            }
            else if (LoansGrid.Focused)
            {
                if (LoansGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)LoansGrid[LoansGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    LoansGrid.Row = LoansGrid.SelectedRows[0];

                    foreach (int i in LoansGrid.SelectedRows)
                    {
                        if (book.Equals(""))
                        {
                            book = LoansGrid[i, "Book"].ToString();
                        }

                        if (book.Equals(LoansGrid[i, "Book"].ToString()))
                        {
                            dataRowView = (DataRowView)LoansGrid[i];
                            dataRow = dataRowView.Row.Table.NewRow();
                            dataRow.ItemArray = dataRowView.Row.ItemArray;

                            dataRows.Add(dataRow);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                }
            }

            if (!hasError && (dataRows.Count > 0))
            {
                try
                {
                    PositionRateChangeInputForm positionRateChangeInputForm = new PositionRateChangeInputForm(mainForm, dataRows, bizDateArray);
                    positionRateChangeInputForm.MdiParent = mainForm;
                    positionRateChangeInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.RateChangeMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                if (dataRows.Count == 0)
                {
                    mainForm.Alert("Must select at least one contract for rate change.", PilotState.RunFault);
                }
                else
                {
                    mainForm.Alert("Must have just one book in the range for rate change.", PilotState.RunFault);
                }
            }
        }

        private void PcChangeMenuItem_Click(object sender, System.EventArgs e)
        {
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.Focused)
            {
                if (BorrowsGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    BorrowsGrid.Row = BorrowsGrid.SelectedRows[0];

                    foreach (int i in BorrowsGrid.SelectedRows)
                    {
                        dataRowView = (DataRowView)BorrowsGrid[i];
                        dataRow = dataRowView.Row.Table.NewRow();
                        dataRow.ItemArray = dataRowView.Row.ItemArray;

                        dataRows.Add(dataRow);
                    }
                }
            }
            else if (LoansGrid.Focused)
            {
                if (LoansGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)LoansGrid[LoansGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    LoansGrid.Row = LoansGrid.SelectedRows[0];

                    foreach (int i in LoansGrid.SelectedRows)
                    {
                        dataRowView = (DataRowView)LoansGrid[i];
                        dataRow = dataRowView.Row.Table.NewRow();
                        dataRow.ItemArray = dataRowView.Row.ItemArray;

                        dataRows.Add(dataRow);
                    }
                }
            }

            if (dataRows.Count > 0)
            {
                try
                {
                    PositionPcChangeInputForm positionPcChangeInputForm = new PositionPcChangeInputForm(mainForm, dataRows, bizDateArray);
                    positionPcChangeInputForm.MdiParent = mainForm;
                    positionPcChangeInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.PcChangeMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                mainForm.Alert("Must select at least one contract for P/C change.", PilotState.RunFault);
            }
        }

        private void ShowIncomeMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowIncomeMenuItem.Checked = !ShowIncomeMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["Income"].Visible = ShowIncomeMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = ShowIncomeMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["Income"].Visible = ShowIncomeMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = ShowIncomeMenuItem.Checked;
        }

        private void ShowCurrencyMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowCurrencyMenuItem.Checked = !ShowCurrencyMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = ShowCurrencyMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = ShowCurrencyMenuItem.Checked;
        }

        private void ShowTermDateMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowTermDateMenuItem.Checked = !ShowTermDateMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = ShowTermDateMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = ShowTermDateMenuItem.Checked;
        }

        private void ShowRecallQuantityMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowRecallQuantityMenuItem.Checked = !ShowRecallQuantityMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = ShowRecallQuantityMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = ShowRecallQuantityMenuItem.Checked;
        }

        private void ShowDividendReclaimMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowDividendReclaimMenuItem.Checked = !ShowDividendReclaimMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = ShowDividendReclaimMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = ShowDividendReclaimMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = ShowDividendReclaimMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = ShowDividendReclaimMenuItem.Checked;
        }

        private void ShowSettlementDetailMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowSettlementDetailMenuItem.Checked = !ShowSettlementDetailMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = ShowSettlementDetailMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = ShowSettlementDetailMenuItem.Checked;
        }

        private void ShowValueRatioMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowValueRatioMenuItem.Checked = !ShowValueRatioMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["Value"].Visible = ShowValueRatioMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = ShowValueRatioMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = ShowValueRatioMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["Value"].Visible = ShowValueRatioMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = ShowValueRatioMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = ShowValueRatioMenuItem.Checked;
        }

        private void DockTopMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.Bottom;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.None;
            this.ControlBox = true;
            this.Text = TEXT;
        }

        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void MainContextMenu_Popup(object sender, System.EventArgs e)
        {
            RateChangeMenuItem.Enabled = ((BorrowsGrid.Focused || LoansGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
            ReturnMenuItem.Enabled = ((BorrowsGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
            RecallMenuItem.Enabled = ((BorrowsGrid.Focused || LoansGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
            PcChangeMenuItem.Enabled = ((BorrowsGrid.Focused || LoansGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
        }

        private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
        {
            if (LoansGrid.Focused)
            {
                this.Cursor = Cursors.WaitCursor;

                Excel excel = new Excel();
                excel.ExportGridToExcel(ref LoansGrid);

                this.Cursor = Cursors.Default;
            }

            if (BorrowsGrid.Focused)
            {
                this.Cursor = Cursors.WaitCursor;

                Excel excel = new Excel();
                excel.ExportGridToExcel(ref BorrowsGrid);

                this.Cursor = Cursors.Default;
            }

            if (SummaryGrid.Focused)
            {
                this.Cursor = Cursors.WaitCursor;

                Excel excel = new Excel();
                excel.ExportGridToExcel(ref SummaryGrid);

                this.Cursor = Cursors.Default;
            }
        }
    }
}