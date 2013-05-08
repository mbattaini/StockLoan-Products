using System;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using StockLoan.Common;

namespace CentralClient
{
  public class TradingBoxSummaryForm : C1.Win.C1Ribbon.C1RibbonForm
  {
    private const string TEXT = "Position - Box Summary";

    private DataSet dataSet;
    private DataView dataView;

    private string secId = "";
    private string dataViewRowFilter = "";

    private MainForm mainForm;

    private C1.Win.C1Input.C1Label BookGroupNameLabel;
    private C1.Win.C1Input.C1Label BookGroupLabel;

    private C1.Win.C1List.C1Combo BookGroupCombo;

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid BoxSummaryGrid;
    private C1.Win.C1Input.C1Label StatusLabel;
    private System.Windows.Forms.CheckBox OptimisticCheckBox;
    private C1.Win.C1Input.C1Button RefreshButton;
    private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
    private C1.Win.C1Command.C1ContextMenu MainCOntextMenu;
    private C1.Win.C1Command.C1CommandLink c1CommandLink1;
    private C1.Win.C1Command.C1CommandMenu ExportToCommand;
    private C1.Win.C1Command.C1CommandLink c1CommandLink2;
    private C1.Win.C1Command.C1Command ExportToExcelCommand;
    private C1.Win.C1Command.C1CommandLink c1CommandLink3;
    private C1.Win.C1Command.C1Command ExportToClipboardCommand;
    private C1.Win.C1Command.C1CommandLink c1CommandLink4;
    private C1.Win.C1Command.C1Command ExportToEmailCommand;
    private C1.Win.C1Sizer.C1Sizer c1Sizer1;

    private System.ComponentModel.Container components = null;

    public TradingBoxSummaryForm(MainForm mainForm)
    {
      InitializeComponent();

      this.Text = TEXT;
      this.mainForm = mainForm;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradingBoxSummaryForm));
		this.BoxSummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
		this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
		this.BookGroupLabel = new C1.Win.C1Input.C1Label();
		this.BookGroupCombo = new C1.Win.C1List.C1Combo();
		this.StatusLabel = new C1.Win.C1Input.C1Label();
		this.OptimisticCheckBox = new System.Windows.Forms.CheckBox();
		this.RefreshButton = new C1.Win.C1Input.C1Button();
		this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
		this.MainCOntextMenu = new C1.Win.C1Command.C1ContextMenu();
		this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
		this.ExportToCommand = new C1.Win.C1Command.C1CommandMenu();
		this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
		this.ExportToExcelCommand = new C1.Win.C1Command.C1Command();
		this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
		this.ExportToClipboardCommand = new C1.Win.C1Command.C1Command();
		this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
		this.ExportToEmailCommand = new C1.Win.C1Command.C1Command();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		((System.ComponentModel.ISupportInitialize)(this.BoxSummaryGrid)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
		this.c1Sizer1.SuspendLayout();
		this.SuspendLayout();
		// 
		// BoxSummaryGrid
		// 
		this.BoxSummaryGrid.AllowColSelect = false;
		this.BoxSummaryGrid.AllowFilter = false;
		this.BoxSummaryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
		this.BoxSummaryGrid.AllowUpdate = false;
		this.BoxSummaryGrid.AllowUpdateOnBlur = false;
		this.BoxSummaryGrid.CaptionHeight = 17;
		this.BoxSummaryGrid.EmptyRows = true;
		this.BoxSummaryGrid.ExtendRightColumn = true;
		this.BoxSummaryGrid.FilterBar = true;
		this.BoxSummaryGrid.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.BoxSummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
		this.BoxSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("BoxSummaryGrid.Images"))));
		this.BoxSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("BoxSummaryGrid.Images1"))));
		this.BoxSummaryGrid.Location = new System.Drawing.Point(4, 29);
		this.BoxSummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
		this.BoxSummaryGrid.Name = "BoxSummaryGrid";
		this.BoxSummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
		this.BoxSummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
		this.BoxSummaryGrid.PreviewInfo.ZoomFactor = 75;
		this.BoxSummaryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("BoxSummaryGrid.PrintInfo.PageSettings")));
		this.BoxSummaryGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
		this.BoxSummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
		this.BoxSummaryGrid.RowHeight = 15;
		this.BoxSummaryGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
		this.BoxSummaryGrid.Size = new System.Drawing.Size(1516, 495);
		this.BoxSummaryGrid.TabIndex = 0;
		this.BoxSummaryGrid.Text = "BoxSummary";
		this.BoxSummaryGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
		this.BoxSummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.BoxSummaryGrid_FormatText);
		this.BoxSummaryGrid.FilterChange += new System.EventHandler(this.BoxSummaryGrid_FilterChange);
		this.BoxSummaryGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BoxSummaryGrid_BeforeUpdate);
		this.BoxSummaryGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoxSummaryGrid_MouseDown);
		this.BoxSummaryGrid.AfterUpdate += new System.EventHandler(this.BoxSummaryGrid_AfterUpdate);
		this.BoxSummaryGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BoxSummaryGrid_SelChange);
		this.BoxSummaryGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.BoxSummaryGrid_BeforeColEdit);
		this.BoxSummaryGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.BoxSummaryGrid_Paint);
		this.BoxSummaryGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BoxSummaryGrid_KeyPress);
		this.BoxSummaryGrid.PropBag = resources.GetString("BoxSummaryGrid.PropBag");
		// 
		// BookGroupNameLabel
		// 
		this.BookGroupNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
		this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.BookGroupNameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.BookGroupNameLabel.Location = new System.Drawing.Point(213, 4);
		this.BookGroupNameLabel.Name = "BookGroupNameLabel";
		this.BookGroupNameLabel.Size = new System.Drawing.Size(300, 21);
		this.BookGroupNameLabel.TabIndex = 6;
		this.BookGroupNameLabel.Tag = null;
		this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.BookGroupNameLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
		// 
		// BookGroupLabel
		// 
		this.BookGroupLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
		this.BookGroupLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.BookGroupLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.BookGroupLabel.Location = new System.Drawing.Point(17, 4);
		this.BookGroupLabel.Name = "BookGroupLabel";
		this.BookGroupLabel.Size = new System.Drawing.Size(92, 21);
		this.BookGroupLabel.TabIndex = 4;
		this.BookGroupLabel.Tag = null;
		this.BookGroupLabel.Text = "Book Group:";
		this.BookGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.BookGroupLabel.TextDetached = true;
		this.BookGroupLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
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
		this.BookGroupCombo.ContentHeight = 15;
		this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
		this.BookGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
		this.BookGroupCombo.DropDownWidth = 425;
		this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
		this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
		this.BookGroupCombo.EditorHeight = 15;
		this.BookGroupCombo.ExtendRightColumn = true;
		this.BookGroupCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images"))));
		this.BookGroupCombo.ItemHeight = 15;
		this.BookGroupCombo.KeepForeColor = true;
		this.BookGroupCombo.LimitToList = true;
		this.BookGroupCombo.Location = new System.Drawing.Point(113, 4);
		this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
		this.BookGroupCombo.MaxDropDownItems = ((short)(10));
		this.BookGroupCombo.MaxLength = 15;
		this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
		this.BookGroupCombo.Name = "BookGroupCombo";
		this.BookGroupCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
		this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.BookGroupCombo.Size = new System.Drawing.Size(96, 21);
		this.BookGroupCombo.TabIndex = 5;
		this.BookGroupCombo.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Black;
		this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
		this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
		// 
		// StatusLabel
		// 
		this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
		this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.StatusLabel.ForeColor = System.Drawing.Color.White;
		this.StatusLabel.Location = new System.Drawing.Point(17, 528);
		this.StatusLabel.Name = "StatusLabel";
		this.StatusLabel.Size = new System.Drawing.Size(896, 27);
		this.StatusLabel.TabIndex = 7;
		this.StatusLabel.Tag = null;
		this.StatusLabel.TextDetached = true;
		this.StatusLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
		// 
		// OptimisticCheckBox
		// 
		this.OptimisticCheckBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.OptimisticCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.OptimisticCheckBox.Location = new System.Drawing.Point(689, 4);
		this.OptimisticCheckBox.Name = "OptimisticCheckBox";
		this.OptimisticCheckBox.Size = new System.Drawing.Size(224, 21);
		this.OptimisticCheckBox.TabIndex = 8;
		this.OptimisticCheckBox.Text = "Optimistic [All deliveries will make]";
		this.OptimisticCheckBox.CheckedChanged += new System.EventHandler(this.OptimisticCheckBox_CheckedChanged);
		// 
		// RefreshButton
		// 
		this.RefreshButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.RefreshButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.RefreshButton.Location = new System.Drawing.Point(474, 4);
		this.RefreshButton.Name = "RefreshButton";
		this.RefreshButton.Size = new System.Drawing.Size(68, 21);
		this.RefreshButton.TabIndex = 9;
		this.RefreshButton.Text = "Refresh";
		this.RefreshButton.UseVisualStyleBackColor = true;
		this.RefreshButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
		// 
		// MainCommandHolder
		// 
		this.MainCommandHolder.Commands.Add(this.MainCOntextMenu);
		this.MainCommandHolder.Commands.Add(this.ExportToCommand);
		this.MainCommandHolder.Commands.Add(this.ExportToExcelCommand);
		this.MainCommandHolder.Commands.Add(this.ExportToClipboardCommand);
		this.MainCommandHolder.Commands.Add(this.ExportToEmailCommand);
		this.MainCommandHolder.Owner = this;
		// 
		// MainCOntextMenu
		// 
		this.MainCOntextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
		this.MainCOntextMenu.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.MainCOntextMenu.Name = "MainCOntextMenu";
		this.MainCOntextMenu.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Black;
		this.MainCOntextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Black;
		// 
		// c1CommandLink1
		// 
		this.c1CommandLink1.Command = this.ExportToCommand;
		// 
		// ExportToCommand
		// 
		this.ExportToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink2,
            this.c1CommandLink3,
            this.c1CommandLink4});
		this.ExportToCommand.Name = "ExportToCommand";
		this.ExportToCommand.Text = "Export To";
		this.ExportToCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Black;
		this.ExportToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Black;
		// 
		// c1CommandLink2
		// 
		this.c1CommandLink2.Command = this.ExportToExcelCommand;
		// 
		// ExportToExcelCommand
		// 
		this.ExportToExcelCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ExportToExcelCommand.Icon")));
		this.ExportToExcelCommand.Name = "ExportToExcelCommand";
		this.ExportToExcelCommand.Text = "Excel";
		// 
		// c1CommandLink3
		// 
		this.c1CommandLink3.Command = this.ExportToClipboardCommand;
		this.c1CommandLink3.SortOrder = 1;
		// 
		// ExportToClipboardCommand
		// 
		this.ExportToClipboardCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ExportToClipboardCommand.Icon")));
		this.ExportToClipboardCommand.Name = "ExportToClipboardCommand";
		this.ExportToClipboardCommand.Text = "Clipboard";
		// 
		// c1CommandLink4
		// 
		this.c1CommandLink4.Command = this.ExportToEmailCommand;
		this.c1CommandLink4.SortOrder = 2;
		// 
		// ExportToEmailCommand
		// 
		this.ExportToEmailCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ExportToEmailCommand.Icon")));
		this.ExportToEmailCommand.Name = "ExportToEmailCommand";
		this.ExportToEmailCommand.Text = "Email";
		// 
		// c1Sizer1
		// 
		this.c1Sizer1.Controls.Add(this.StatusLabel);
		this.c1Sizer1.Controls.Add(this.RefreshButton);
		this.c1Sizer1.Controls.Add(this.BoxSummaryGrid);
		this.c1Sizer1.Controls.Add(this.OptimisticCheckBox);
		this.c1Sizer1.Controls.Add(this.BookGroupLabel);
		this.c1Sizer1.Controls.Add(this.BookGroupNameLabel);
		this.c1Sizer1.Controls.Add(this.BookGroupCombo);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = resources.GetString("c1Sizer1.GridDefinition");
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(1524, 559);
		this.c1Sizer1.TabIndex = 10;
		this.c1Sizer1.Text = "c1Sizer1";
		// 
		// TradingBoxSummaryForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
		this.MainCommandHolder.SetC1Command(this, this.MainCOntextMenu);
		this.MainCommandHolder.SetC1ContextMenu(this, this.MainCOntextMenu);
		this.ClientSize = new System.Drawing.Size(1524, 559);
		this.Controls.Add(this.c1Sizer1);
		this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "TradingBoxSummaryForm";
		this.Text = "Trading - Box Summary";
		this.Closed += new System.EventHandler(this.PositionBoxSummaryForm_Closed);
		this.Load += new System.EventHandler(this.PositionBoxSummaryForm_Load);
		((System.ComponentModel.ISupportInitialize)(this.BoxSummaryGrid)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		this.ResumeLayout(false);

    }
    #endregion


    private void PositionBoxSummaryForm_Load(object sender, System.EventArgs e)
    {
    }

    private void PositionBoxSummaryForm_Closed(object sender, System.EventArgs e)
    {
      mainForm.tradingBoxSummaryForm = null;
    }

    private void OptimisticCheckBox_CheckedChanged(object sender, System.EventArgs e)
    {
    }

    private void BoxSummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
    }

    private void BoxSummaryGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
    }

    private void BoxSummaryGrid_FilterChange(object sender, System.EventArgs e)
    {
    }

    private void BoxSummaryGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {
    }

    private void BoxSummaryGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
    }

    private void BoxSummaryGrid_AfterUpdate(object sender, System.EventArgs e)
    {

    }

    private void BoxSummaryGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {

    }

    private void BoxSummaryGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
    }

    private void BookGroupCombo_TextChanged(object sender, System.EventArgs e)
    {
    }

    private void SendToBlotterMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void ShowFailsMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void ShowRecallsMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void ShowBorrowLoanMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void ShowExDeficitMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void ShowPledgeMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void DockTopMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
    {
    }

    private void BoxSummaryGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
    }

    private void RefreshButton_Click(object sender, System.EventArgs e)
    {
    }

    private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
    {
    }
  }
}
