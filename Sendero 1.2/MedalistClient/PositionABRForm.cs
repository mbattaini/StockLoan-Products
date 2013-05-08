using System;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using Anetics.Common;

namespace Anetics.Medalist
{
    public class PositionABRForm : System.Windows.Forms.Form
    {
        #region Declarations

        private const string TEXT = "Position - Automated Borrow Returns";

        private DataSet dataSet;
        private DataSet contractDataSet;
        private string abrDataViewRowFilter;
        private DataView abrDataView;
        private DataView contractDataView;

        private string secId = "";
        private string contractDataViewRowFilter = "";

        private DataTable excessBorrows, borrowContracts;
        private MainForm mainForm;

        private C1.Win.C1Input.C1Label BookGroupNameLabel;
        private C1.Win.C1Input.C1Label BookGroupLabel;

        private C1.Win.C1List.C1Combo BookGroupCombo;

        private System.Windows.Forms.ContextMenu MainContextMenu;

        private System.Windows.Forms.MenuItem SendToMenuItem;
        private System.Windows.Forms.MenuItem SendToEmailMenuItem;
        private System.Windows.Forms.MenuItem ReturnMenuItem;
        private System.Windows.Forms.MenuItem Sep1MenuItem;
        private System.Windows.Forms.MenuItem DockMenuItem;
        private System.Windows.Forms.MenuItem DockTopMenuItem;
        private System.Windows.Forms.MenuItem DockBottomMenuItem;
        private System.Windows.Forms.MenuItem DockSep1MenuItem;
        private System.Windows.Forms.MenuItem DockNoneMenuItem;
        private C1.Win.C1Input.C1Label StatusLabel;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.MenuItem SendToExcelMenuItem;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ABRContractGrid;
        private System.Windows.Forms.CheckBox OptimisticCheckBox;

        private System.ComponentModel.Container components = null;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ABRGrid;


        private ArrayList contractsArray;
        private System.Windows.Forms.MenuItem ActionMenuItem;
        private System.Windows.Forms.MenuItem CheckAllMenuItem;
        private C1.Win.C1Input.C1Label AutomatedReturnLabel;
        private C1.Win.C1Command.C1CommandDock c1CommandDock1;
        private C1.Win.C1Command.C1MainMenu c1MainMenu1;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private System.Windows.Forms.MenuItem UncheckMenuitem;

        #endregion
        public PositionABRForm(MainForm mainForm)
        {
            try
            {
                InitializeComponent();

                this.Text = TEXT;
                this.mainForm = mainForm;

                this.contractsArray = new ArrayList();
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + " [PositionABRForm.PositionABRForm]", 3);
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PositionABRForm));
            this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
            this.BookGroupLabel = new C1.Win.C1Input.C1Label();
            this.BookGroupCombo = new C1.Win.C1List.C1Combo();
            this.MainContextMenu = new System.Windows.Forms.ContextMenu();
            this.ReturnMenuItem = new System.Windows.Forms.MenuItem();
            this.ActionMenuItem = new System.Windows.Forms.MenuItem();
            this.CheckAllMenuItem = new System.Windows.Forms.MenuItem();
            this.UncheckMenuitem = new System.Windows.Forms.MenuItem();
            this.SendToMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
            this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.DockMenuItem = new System.Windows.Forms.MenuItem();
            this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
            this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
            this.DockSep1MenuItem = new System.Windows.Forms.MenuItem();
            this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
            this.StatusLabel = new C1.Win.C1Input.C1Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ABRContractGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.OptimisticCheckBox = new System.Windows.Forms.CheckBox();
            this.ABRGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.AutomatedReturnLabel = new C1.Win.C1Input.C1Label();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            this.c1MainMenu1 = new C1.Win.C1Command.C1MainMenu();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABRContractGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABRGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutomatedReturnLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // BookGroupNameLabel
            // 
            this.BookGroupNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
            this.BookGroupNameLabel.Location = new System.Drawing.Point(202, 8);
            this.BookGroupNameLabel.Name = "BookGroupNameLabel";
            this.BookGroupNameLabel.Size = new System.Drawing.Size(298, 18);
            this.BookGroupNameLabel.TabIndex = 6;
            this.BookGroupNameLabel.Tag = null;
            this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BookGroupLabel
            // 
            this.BookGroupLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BookGroupLabel.Location = new System.Drawing.Point(4, 8);
            this.BookGroupLabel.Name = "BookGroupLabel";
            this.BookGroupLabel.Size = new System.Drawing.Size(92, 18);
            this.BookGroupLabel.TabIndex = 4;
            this.BookGroupLabel.Tag = null;
            this.BookGroupLabel.Text = "Book Group:";
            this.BookGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.BookGroupCombo.EditorHeight = 14;
            this.BookGroupCombo.ExtendRightColumn = true;
            this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images"))));
            this.BookGroupCombo.ItemHeight = 15;
            this.BookGroupCombo.KeepForeColor = true;
            this.BookGroupCombo.LimitToList = true;
            this.BookGroupCombo.Location = new System.Drawing.Point(100, 7);
            this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
            this.BookGroupCombo.MaxDropDownItems = ((short)(10));
            this.BookGroupCombo.MaxLength = 15;
            this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
            this.BookGroupCombo.Name = "BookGroupCombo";
            this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookGroupCombo.Size = new System.Drawing.Size(96, 20);
            this.BookGroupCombo.TabIndex = 5;
            this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
            this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ReturnMenuItem,
            this.ActionMenuItem,
            this.SendToMenuItem,
            this.Sep1MenuItem,
            this.DockMenuItem});
            // 
            // ReturnMenuItem
            // 
            this.ReturnMenuItem.Index = 0;
            this.ReturnMenuItem.Text = "Return";
            this.ReturnMenuItem.Click += new System.EventHandler(this.ReturnMenuItem_Click);
            // 
            // ActionMenuItem
            // 
            this.ActionMenuItem.Index = 1;
            this.ActionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CheckAllMenuItem,
            this.UncheckMenuitem});
            this.ActionMenuItem.Text = "Action";
            // 
            // CheckAllMenuItem
            // 
            this.CheckAllMenuItem.Index = 0;
            this.CheckAllMenuItem.Text = "Check All";
            this.CheckAllMenuItem.Click += new System.EventHandler(this.CheckAllMenuItem_Click);
            // 
            // UncheckMenuitem
            // 
            this.UncheckMenuitem.Index = 1;
            this.UncheckMenuitem.Text = "Uncheck all";
            this.UncheckMenuitem.Click += new System.EventHandler(this.UncheckMenuitem_Click);
            // 
            // SendToMenuItem
            // 
            this.SendToMenuItem.Index = 2;
            this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SendToExcelMenuItem,
            this.SendToEmailMenuItem});
            this.SendToMenuItem.Text = "Send To";
            // 
            // SendToExcelMenuItem
            // 
            this.SendToExcelMenuItem.Index = 0;
            this.SendToExcelMenuItem.Text = "Excel";
            this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
            // 
            // SendToEmailMenuItem
            // 
            this.SendToEmailMenuItem.Index = 1;
            this.SendToEmailMenuItem.Text = "Mail Recipient";
            this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
            // 
            // Sep1MenuItem
            // 
            this.Sep1MenuItem.Index = 3;
            this.Sep1MenuItem.Text = "-";
            // 
            // DockMenuItem
            // 
            this.DockMenuItem.Index = 4;
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
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.StatusLabel.Location = new System.Drawing.Point(20, 460);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(1124, 16);
            this.StatusLabel.TabIndex = 7;
            this.StatusLabel.Tag = null;
            this.StatusLabel.TextDetached = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(732, 7);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(76, 20);
            this.RefreshButton.TabIndex = 9;
            this.RefreshButton.Text = "REFRESH";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ABRContractGrid
            // 
            this.ABRContractGrid.AllowColSelect = false;
            this.ABRContractGrid.AllowFilter = false;
            this.ABRContractGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.ABRContractGrid.AllowUpdateOnBlur = false;
            this.ABRContractGrid.CaptionHeight = 17;
            this.ABRContractGrid.EmptyRows = true;
            this.ABRContractGrid.ExtendRightColumn = true;
            this.ABRContractGrid.FetchRowStyles = true;
            this.ABRContractGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.ABRContractGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ABRContractGrid.Images"))));
            this.ABRContractGrid.Location = new System.Drawing.Point(28, 88);
            this.ABRContractGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
            this.ABRContractGrid.Name = "ABRContractGrid";
            this.ABRContractGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ABRContractGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ABRContractGrid.PreviewInfo.ZoomFactor = 75D;
            this.ABRContractGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ABRContractGrid.PrintInfo.PageSettings")));
            this.ABRContractGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.ABRContractGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.ABRContractGrid.RowHeight = 15;
            this.ABRContractGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
            this.ABRContractGrid.Size = new System.Drawing.Size(1100, 69);
            this.ABRContractGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.ABRContractGrid.TabIndex = 28;
            this.ABRContractGrid.TabStop = false;
            this.ABRContractGrid.Text = "BookGrid";
            this.ABRContractGrid.Visible = false;
            this.ABRContractGrid.WrapCellPointer = true;
            this.ABRContractGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ABRContractGrid_FormatText);
            this.ABRContractGrid.PropBag = resources.GetString("ABRContractGrid.PropBag");
            // 
            // OptimisticCheckBox
            // 
            this.OptimisticCheckBox.Location = new System.Drawing.Point(508, 9);
            this.OptimisticCheckBox.Name = "OptimisticCheckBox";
            this.OptimisticCheckBox.Size = new System.Drawing.Size(224, 16);
            this.OptimisticCheckBox.TabIndex = 30;
            this.OptimisticCheckBox.Text = "Optimistic [All deliveries will make]";
            this.OptimisticCheckBox.CheckedChanged += new System.EventHandler(this.OptimisticCheckBox_CheckedChanged);
            // 
            // ABRGrid
            // 
            this.ABRGrid.AllowColSelect = false;
            this.ABRGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.ABRGrid.AllowUpdateOnBlur = false;
            this.ABRGrid.CaptionHeight = 17;
            this.ABRGrid.ColumnFooters = true;
            this.ABRGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ABRGrid.EmptyRows = true;
            this.ABRGrid.ExtendRightColumn = true;
            this.ABRGrid.FetchRowStyles = true;
            this.ABRGrid.FilterBar = true;
            this.ABRGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ABRGrid.GroupByAreaVisible = false;
            this.ABRGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.ABRGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ABRGrid.Images"))));
            this.ABRGrid.Location = new System.Drawing.Point(1, 56);
            this.ABRGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.ABRGrid.Name = "ABRGrid";
            this.ABRGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ABRGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ABRGrid.PreviewInfo.ZoomFactor = 75D;
            this.ABRGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ABRGrid.PrintInfo.PageSettings")));
            this.ABRGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.ABRGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.ABRGrid.RowHeight = 15;
            this.ABRGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
            this.ABRGrid.Size = new System.Drawing.Size(1314, 401);
            this.ABRGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.ABRGrid.TabIndex = 0;
            this.ABRGrid.TabStop = false;
            this.ABRGrid.Text = "ABR";
            this.ABRGrid.WrapCellPointer = true;
            this.ABRGrid.AfterUpdate += new System.EventHandler(this.ABRGrid_AfterUpdate);
            this.ABRGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.ABRGrid_BeforeColUpdate);
            this.ABRGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ABRGrid_SelChange);
            this.ABRGrid.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.ABRGrid_AfterColEdit);
            this.ABRGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.ABRGrid_BeforeColEdit);
            this.ABRGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ABRGrid_FormatText);
            this.ABRGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ABRGrid_Paint);
            this.ABRGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ABRGrid_MouseDown);
            this.ABRGrid.PropBag = resources.GetString("ABRGrid.PropBag");
            // 
            // AutomatedReturnLabel
            // 
            this.AutomatedReturnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutomatedReturnLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AutomatedReturnLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutomatedReturnLabel.ForeColor = System.Drawing.Color.Navy;
            this.AutomatedReturnLabel.Location = new System.Drawing.Point(864, 8);
            this.AutomatedReturnLabel.Name = "AutomatedReturnLabel";
            this.AutomatedReturnLabel.Size = new System.Drawing.Size(444, 18);
            this.AutomatedReturnLabel.TabIndex = 31;
            this.AutomatedReturnLabel.Tag = null;
            this.AutomatedReturnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutomatedReturnLabel.TextDetached = true;
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1CommandDock1.Id = 1;
            this.c1CommandDock1.Location = new System.Drawing.Point(1, 425);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(1314, 32);
            // 
            // c1MainMenu1
            // 
            this.c1MainMenu1.CommandHolder = this.c1CommandHolder1;
            this.c1MainMenu1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
            this.c1MainMenu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1MainMenu1.Location = new System.Drawing.Point(1, 35);
            this.c1MainMenu1.Name = "c1MainMenu1";
            this.c1MainMenu1.Size = new System.Drawing.Size(1314, 21);
            this.c1MainMenu1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Owner = this;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Text = "New Command";
            // 
            // PositionABRForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1316, 477);
            this.ContextMenu = this.MainContextMenu;
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.AutomatedReturnLabel);
            this.Controls.Add(this.OptimisticCheckBox);
            this.Controls.Add(this.ABRContractGrid);
            this.Controls.Add(this.ABRGrid);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.BookGroupNameLabel);
            this.Controls.Add(this.BookGroupLabel);
            this.Controls.Add(this.BookGroupCombo);
            this.Controls.Add(this.c1MainMenu1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PositionABRForm";
            this.Padding = new System.Windows.Forms.Padding(1, 35, 1, 20);
            this.Text = "Position - Automated Borrow Returns";
            this.Closed += new System.EventHandler(this.PositionABRForm_Closed);
            this.Load += new System.EventHandler(this.PositionABRForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABRContractGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABRGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutomatedReturnLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void SendReturns(ArrayList contractsArray)
        {

            long quantity = 0;
            long returnQuantity = 0;
            decimal amount = 0;
            decimal price = 0;

            try
            {
                foreach (DataRow dr in contractsArray)
                {
                    try
                    {
                        if (bool.Parse(dr["DoReturn"].ToString()))
                        {

                            mainForm.Alert(dr["SecId"].ToString() + " " + dr["Quantity"].ToString() + " " + dr["Amount"].ToString());

                            quantity = long.Parse(dr["Quantity"].ToString());
                            amount = decimal.Parse(dr["Amount"].ToString());

                            price = amount / quantity;

                            returnQuantity = long.Parse(dr["ReturnQuantity"].ToString());

                            if (returnQuantity > 0)
                            {
                                mainForm.PositionAgent.Return(
                                    dr["BookGroup"].ToString(),
                                    "B",
                                    dr["ContractId"].ToString(),
                                    dr["SecId"].ToString(),
                                    returnQuantity,
                                    returnQuantity * price,
                                    "",
                                    "",
                                    "",
                                    mainForm.UserId);

                                dr["Status"] = "A";
                            }

                            mainForm.Alert("Processed return of " + dr["SecId"].ToString() + " [" + dr["Symbol"].ToString() + "] for " + quantity.ToString("#,##0") + " shares.", PilotState.Normal);
                        }
                    }
                    catch (Exception error)
                    {
                        dr["Status"] = "F";

                        Log.Write(error.Message + " [PositionABRForm.ReturnMenuItem_Click]", Log.Error, 1);
                        mainForm.Alert(error.Message, PilotState.RunFault);
                    }

                    borrowContracts.AcceptChanges();
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionABRForm.ReturnMenuItem_Click]", Log.Error, 1);
                mainForm.Alert(error.Message, PilotState.RunFault);
            }

            mainForm.Alert("Refreshing abr summary...", PilotState.RunFault);
           
            StatusSet();
            FooterSet();

            this.Cursor = Cursors.Default;
            mainForm.Alert("Done! Refreshing abr summary.", PilotState.RunFault);
        }

        private void DataLoad()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            mainForm.Alert("Please wait... Loading current abr summary.", PilotState.Idle);

            try
            {

                dataSet = mainForm.PositionAgent.BoxSummaryDataGet(mainForm.UtcOffset, mainForm.UserId, "PositionOpenContracts");
               

                excessBorrows = new DataTable("ExcessBorrows");
                excessBorrows.Clear();

                excessBorrows.Columns.Add("BookGroup", typeof(string));
                excessBorrows.Columns.Add("SecId", typeof(string));
                excessBorrows.Columns.Add("Symbol", typeof(string));
                excessBorrows.Columns.Add("ClassGroup", typeof(string));
                excessBorrows.Columns.Add("BaseType", typeof(string));
                excessBorrows.Columns.Add("Price", typeof(decimal));
                excessBorrows.Columns.Add("Available", typeof(long));
                excessBorrows.Columns.Add("Borrows", typeof(long));
                excessBorrows.Columns.Add("BorrowsAmount", typeof(long));
                excessBorrows.Columns.Add("ReturnBorrows", typeof(long));
                excessBorrows.Columns.Add("ReturnBorrowsAmount", typeof(long));
                excessBorrows.Columns.Add("DoReturn", typeof(bool));
                excessBorrows.Columns.Add("NumberOfContracts", typeof(long));
                excessBorrows.Columns.Add("delete", typeof(bool));

                excessBorrows.AcceptChanges();

                foreach (DataRow dr in dataSet.Tables["BoxSummary"].Rows)
                {
                    if (BookGroupCombo.Text.Equals(dr["BookGroup"].ToString()))
                    {
                        DataRow tempDr = excessBorrows.NewRow();

                        tempDr["BookGroup"] = dr["BookGroup"];
                        tempDr["SecId"] = dr["SecId"];
                        tempDr["Symbol"] = dr["Symbol"];
                        tempDr["BaseType"] = dr["BaseType"];
                        tempDr["ClassGroup"] = dr["ClassGroup"];
                        tempDr["Price"] = dr["LastPrice"];

                        if (OptimisticCheckBox.Checked)
                        {
                            tempDr["Available"] = long.Parse(dr["ReceiveFails"].ToString()) + long.Parse(dr["Available"].ToString());
                        }
                        else
                        {
                            tempDr["Available"] = long.Parse(dr["Available"].ToString());
                        }

                        if (long.Parse(dr["Borrows"].ToString()) > 100)
                        {
                            tempDr["Available"] = long.Parse(tempDr["Available"].ToString()) - (long.Parse(tempDr["Available"].ToString()) % 100);
                            tempDr["Borrows"] = long.Parse(dr["Borrows"].ToString()) - (long.Parse(dr["Borrows"].ToString()) % 100);
                        }
                        else
                        {
                            tempDr["Borrows"] = dr["Borrows"];
                        }

                        if (long.Parse(tempDr["Available"].ToString()) <= long.Parse(tempDr["Borrows"].ToString()))
                        {
                            tempDr["ReturnBorrows"] = long.Parse(tempDr["Available"].ToString());
                        }
                        else
                        {
                            tempDr["ReturnBorrows"] = long.Parse(tempDr["Borrows"].ToString());
                        }

                        tempDr["DoReturn"] = 0;

                        tempDr["BorrowsAmount"] = (long.Parse(tempDr["Borrows"].ToString()) * decimal.Parse(tempDr["Price"].ToString()));
                        tempDr["ReturnBorrowsAmount"] = (long.Parse(tempDr["ReturnBorrows"].ToString()) * decimal.Parse(tempDr["Price"].ToString()));
                        tempDr["NumberOfContracts"] = 0;


                        if (long.Parse(tempDr["Borrows"].ToString()) > 0 && long.Parse(tempDr["ReturnBorrows"].ToString()) >= 100)
                        {
                            excessBorrows.Rows.Add(tempDr);
                        }
                    }
                }


                excessBorrows.AcceptChanges();

                borrowContracts = new DataTable("BorrowContracts");
                borrowContracts.Clear();

                borrowContracts.Columns.Add("BookGroup", typeof(string));
                borrowContracts.Columns.Add("Book", typeof(string));
                borrowContracts.Columns.Add("ContractId", typeof(string));
                borrowContracts.Columns.Add("SecId", typeof(string));
                borrowContracts.Columns.Add("Symbol", typeof(string));
                borrowContracts.Columns.Add("ReturnQuantity", typeof(long));
                borrowContracts.Columns.Add("Quantity", typeof(long));
                borrowContracts.Columns.Add("Amount", typeof(decimal));
                borrowContracts.Columns.Add("Rate", typeof(decimal));
                borrowContracts.Columns.Add("PoolCode", typeof(string));
                borrowContracts.Columns.Add("CashDepot", typeof(string));
                borrowContracts.Columns.Add("DoReturn", typeof(bool));
                borrowContracts.Columns.Add("Status", typeof(string));

                borrowContracts.AcceptChanges();

                string abrPoolCodeIgnore = mainForm.ServiceAgent.KeyValueGet("ABRPoolCodeIgnore", "");
                contractDataSet = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, mainForm.ServiceAgent.BizDate(), mainForm.UserId, "PositionOpenContracts");

                foreach (DataRow tempDrParent in excessBorrows.Rows)
                {
                    if (tempDrParent["BookGroup"].ToString().Equals(BookGroupCombo.Text))
                    {
                        if (tempDrParent["SecId"].ToString().Trim().Equals("000360206"))
                        {
                        }
                        string filter = "BookGroup = '" + tempDrParent["BookGroup"].ToString() + "' AND SecId = '" + tempDrParent["SecId"].ToString().Trim() + "' AND ContractType = 'B' AND QuantitySettled > 0";
                        string sort = "Rate ASC, QuantitySettled ASC";
                        DataRow[] dataRowArray = contractDataSet.Tables["Contracts"].Select(filter, sort);

                        long quantityRemaining = long.Parse(tempDrParent["ReturnBorrows"].ToString());

                        foreach (DataRow contractDr in dataRowArray)
                        {
                            if (contractDr["BookGroup"].ToString().Equals(BookGroupCombo.Text))
                            {
                                if (abrPoolCodeIgnore.IndexOf(contractDr["PoolCode"].ToString()) > 0)
                                {
                                    continue;
                                }
                                else if (DateTime.Parse(contractDr["SettleDate"].ToString()).ToString("yyyy-MM-dd").Equals(mainForm.ServiceAgent.BizDate()))
                                {
                                    continue;
                                }
                                else if (long.Parse(contractDr["Quantity"].ToString()) == 0)
                                {
                                    continue;
                                }
                                else if (decimal.Parse(contractDr["Amount"].ToString()) == 0)
                                {
                                    continue;
                                }
                                else if (long.Parse(contractDr["Quantity"].ToString()) >= quantityRemaining)
                                {
                                    DataRow tempDrRow = borrowContracts.NewRow();

                                    tempDrRow["BookGroup"] = contractDr["BookGroup"];
                                    tempDrRow["Book"] = contractDr["Book"];
                                    tempDrRow["ContractId"] = contractDr["ContractId"];
                                    tempDrRow["SecId"] = contractDr["SecId"];
                                    tempDrRow["Symbol"] = contractDr["Symbol"];
                                    tempDrRow["ReturnQuantity"] = quantityRemaining;
                                    tempDrRow["Quantity"] = contractDr["Quantity"];
                                    tempDrRow["Amount"] = contractDr["Amount"];
                                    tempDrRow["Rate"] = contractDr["Rate"];
                                    tempDrRow["PoolCode"] = contractDr["PoolCode"];
                                    tempDrRow["CashDepot"] = contractDr["CashDepot"];
                                    tempDrRow["DoReturn"] = 0;

                                    borrowContracts.Rows.Add(tempDrRow);
                                    quantityRemaining = 0;
                                    tempDrParent["NumberOfContracts"] = long.Parse(tempDrParent["NumberOfContracts"].ToString()) + 1;

                                }
                                else if (long.Parse(contractDr["Quantity"].ToString()) < quantityRemaining)
                                {
                                    DataRow tempDrRow = borrowContracts.NewRow();

                                    tempDrRow["BookGroup"] = contractDr["BookGroup"];
                                    tempDrRow["Book"] = contractDr["Book"];
                                    tempDrRow["ContractId"] = contractDr["ContractId"];
                                    tempDrRow["SecId"] = contractDr["SecId"];
                                    tempDrRow["Symbol"] = contractDr["Symbol"];
                                    tempDrRow["ReturnQuantity"] = contractDr["Quantity"];
                                    tempDrRow["Quantity"] = contractDr["Quantity"];
                                    tempDrRow["Amount"] = contractDr["Amount"];
                                    tempDrRow["Rate"] = contractDr["Rate"];
                                    tempDrRow["PoolCode"] = contractDr["PoolCode"];
                                    tempDrRow["CashDepot"] = contractDr["CashDepot"];
                                    tempDrRow["DoReturn"] = 0;

                                    borrowContracts.Rows.Add(tempDrRow);

                                    quantityRemaining = quantityRemaining - long.Parse(contractDr["Quantity"].ToString());

                                    tempDrParent["NumberOfContracts"] = long.Parse(tempDrParent["NumberOfContracts"].ToString()) + 1;
                                }

                                if (quantityRemaining == 0)
                                    break;
                            }

                            borrowContracts.AcceptChanges();
                            excessBorrows.AcceptChanges();
                        }
                    }
                }               

      
                excessBorrows.AcceptChanges();

                abrDataView = new DataView(excessBorrows, abrDataViewRowFilter, "Symbol", DataViewRowState.CurrentRows);
                contractDataView = new DataView(borrowContracts, "", "Symbol", DataViewRowState.CurrentRows);

                // -- Grid - DataBinding
                ABRGrid.SetDataBinding(abrDataView, null, true);
                ABRContractGrid.SetDataBinding(contractDataView, null, true);

                // -- Grid - ChildGrid
                ABRGrid.ChildGrid = ABRContractGrid;
           
                mainForm.Alert("Done. Loaded " + abrDataView.Table.Rows.Count.ToString("#,##0") + " abr summary items.", PilotState.Idle);
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + " [PositionABRForm.DataLoad]", 1);
            }

            this.Cursor = Cursors.Default;
        }

        public void BookGroupLoad()
        {
            DataSet dataSet = mainForm.ServiceAgent.BookGroupGet();

            BookGroupCombo.HoldFields();
            BookGroupCombo.DataSource = dataSet.Tables["BookGroups"];
            BookGroupCombo.SelectedIndex = -1;

            BookGroupNameLabel.DataSource = dataSet.Tables["BookGroups"];
            BookGroupNameLabel.DataField = "BookName";

            if (RegistryValue.Read(this.Name, "BookGroup").Equals(""))
            {
                BookGroupCombo.SelectedIndex = 0;
            }
            else
            {
                BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", "");
            }
        }

        private void StatusSet()
        {
            if (ABRGrid.SelectedRows.Count > 0)
            {
                StatusLabel.Text = "Selected " + ABRGrid.SelectedRows.Count.ToString("#,##0") + " items of " + abrDataView.Count.ToString("#,##0") + " shown in grid.";
            }
            else
            {
                StatusLabel.Text = "Showing " + abrDataView.Count.ToString("#,##0") + " items in grid.";
            }
        }


        private void FooterSet()
        {
            decimal borrowsAmount = 0;
            decimal returnBorrowsAmount = 0;

            try
            {
                for (int index = 0; index < ABRGrid.Splits[0].Rows.Count; index++)
                {
                    if (bool.Parse(ABRGrid.Columns["DoReturn"].CellText(index).ToString()))
                    {
                        try
                        {
                            if (!ABRGrid.Columns["BorrowsAmount"].CellValue(index).ToString().Equals(""))
                                borrowsAmount += decimal.Parse(ABRGrid.Columns["BorrowsAmount"].CellValue(index).ToString());
                        }
                        catch { }
                        try
                        {
                            if (!ABRGrid.Columns["ReturnBorrowsAmount"].CellValue(index).ToString().Equals(""))
                                returnBorrowsAmount += decimal.Parse(ABRGrid.Columns["ReturnBorrowsAmount"].CellValue(index).ToString());
                        }
                        catch { }
                    }

                    ABRGrid.Columns["BorrowsAmount"].FooterText = borrowsAmount.ToString("#,##0.00");
                    ABRGrid.Columns["ReturnBorrowsAmount"].FooterText = returnBorrowsAmount.ToString("#,##0.00");
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
            }
        }

        private void PositionABRForm_Load(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                int height = mainForm.Height - 275;
                int width = mainForm.Width - 45;

                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

                this.Show();
                Application.DoEvents();

                if (mainForm.ServiceAgent.KeyValueGet("AutomatedBorrowReturnFileUploadDate", "").Equals(mainForm.ServiceAgent.ContractsBizDate()))
                {
                    AutomatedReturnLabel.ForeColor = System.Drawing.Color.Navy;
                    AutomatedReturnLabel.Text = "Automated Borrow Return File has been uploaded for: " + mainForm.ServiceAgent.ContractsBizDate() + ".";
                }
                else
                {
                    AutomatedReturnLabel.ForeColor = System.Drawing.Color.Maroon;
                    AutomatedReturnLabel.Text = "Automated Borrow Return File has not been uploaded for: " + mainForm.ServiceAgent.ContractsBizDate() + ".";
                }

                BookGroupLoad();
                RecordCheck(true);
                StatusSet();
                FooterSet();
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + " [PositionABRForm.PositionABRForm_Load]", Log.Error, 1);
            }

            this.Cursor = Cursors.Default;
        }

        private void PositionABRForm_Closed(object sender, System.EventArgs e)
        {
            try
            {
                if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
                {
                    RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                    RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                    RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                    RegistryValue.Write(this.Name, "Width", this.Width.ToString());
                }
                mainForm.positionABRForm = null;
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [PositionABRForm.PositionABRForm_Closed]", Log.Error, 1);
                mainForm.Alert(error.Message, PilotState.RunFault);
            }

            this.Cursor = Cursors.Default;
        }

        private void ABRGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
        {
            string filter;

            try
            {
                if (e.Column.DataColumn.DataField.Equals("DoReturn"))
                {
                    filter = "BookGroup = '" + ABRGrid.Columns["BookGroup"].Text + "' AND SecId = '" + ABRGrid.Columns["SecId"].Text.Trim() + "'";

                    foreach (DataRow row in borrowContracts.Select(filter))
                    {
                        row["DoReturn"] = bool.Parse(ABRGrid.Columns["DoReturn"].Value.ToString());
                    }

                    borrowContracts.AcceptChanges();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionABRForm.ABRGrid_BeforeColEdit]", Log.Error, 1);
            }
        }

        private void ABRGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            StatusSet();
            FooterSet();
        }

        private void RecordCheck(bool check)
        {
            try
            {
                foreach (DataRow dr in excessBorrows.Rows)
                {
                    dr["DoReturn"] = check;
                }

                foreach (DataRow dr in borrowContracts.Rows)
                {
                    dr["DoReturn"] = check;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionABRForm.ABRGrid_BeforeColEdit]", Log.Error, 1);
            }
        }


        private void ABRGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (!ABRGrid.Columns["SecId"].Text.Equals(secId))
                {
                    if (ABRGrid.Splits[0].Rows.Count > 0)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        Application.DoEvents();

                        try
                        {
                            secId = ABRGrid.Columns["SecId"].Text;
                            mainForm.SecId = secId;

                            contractDataView.RowFilter = "SecId = '" + ABRGrid.Columns["SecId"].Text.Trim() + "'";
                            StatusSet();
                        }
                        catch { }

                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        secId = "";
                        mainForm.SecId = secId;
                    }
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionABRForm.ABRGrid_Paint]", Log.Error, 1);
            }

        }

        private void ABRGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {

                if (e.X <= ABRGrid.RecordSelectorWidth && e.Y <= ABRGrid.RowHeight)
                {
                    if (ABRGrid.SelectedRows.Count.Equals(0))
                    {
                        for (int i = 0; i < ABRGrid.Splits[0, 0].Rows.Count; i++)
                        {
                            ABRGrid.SelectedRows.Add(i);
                        }

                        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ABRGrid.Columns)
                        {
                            ABRGrid.SelectedCols.Add(dataColumn);
                        }
                    }
                    else
                    {
                        ABRGrid.SelectedRows.Clear();
                        ABRGrid.SelectedCols.Clear();
                    }
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionABRForm.ABRGrid_MouseDown]", Log.Error, 1);
            }
        }

        private void OptimisticCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DataLoad();
            StatusSet();
            FooterSet();

            this.Cursor = Cursors.Default;
        }
        private void RefreshButton_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DataLoad();
            StatusSet();
            FooterSet();

            this.Cursor = Cursors.Default;
        }

        private void BookGroupCombo_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                int row = -1;

                foreach (DataRow dataRow in dataSet.Tables["BookGroups"].Rows)
                {
                    row++;

                    if (dataRow["BookGroup"].ToString().Equals(BookGroupCombo.Text))
                    {
                        break;
                    }
                }

                if (bool.Parse(dataSet.Tables["BookGroups"].Rows[row]["MayView"].ToString()))
                {
                    mainForm.GridFilterClear(ref ABRGrid);

                    abrDataViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                    abrDataView.RowFilter = abrDataViewRowFilter;

                    if (bool.Parse(dataSet.Tables["BookGroups"].Rows[row]["MayEdit"].ToString()))
                    {
                        ReturnMenuItem.Enabled = true;
                    }
                    else
                    {
                        mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
                        ReturnMenuItem.Enabled = false;
                    }
                }
                else
                {
                    abrDataViewRowFilter = "[BookGroup] = ''";
                    abrDataView.RowFilter = abrDataViewRowFilter;
                    mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
                }

                RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionABRForm.BookGroupCombo_TextChanged]", Log.Error, 1);
            }

            DataLoad();
            StatusSet();
            FooterSet();
        }

        private void ReturnMenuItem_Click(object sender, System.EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;

            contractsArray.Clear();

            try
            {
                foreach (DataRow dr in borrowContracts.Rows)
                {
                    if (bool.Parse(dr["DoReturn"].ToString()))
                    {
                        contractsArray.Add(dr);
                    }

                }

                mainForm.Alert("Will attempt to return " + contractsArray.Count.ToString("#,##0") + ".", PilotState.Normal);
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionABRForm.ReturnMenuItem_Click]", Log.Error, 1);
                mainForm.Alert(error.Message, PilotState.RunFault);
            }


            SendReturns(contractsArray);

            this.Cursor = Cursors.Default;
            mainForm.Alert("Done! Refreshing abr summary.", PilotState.RunFault);
        }

        private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
        {
            int textLength;
            int[] maxTextLength;

            int columnIndex = -1;
            string gridData = "\n\n\n";

            if (ABRGrid.SelectedCols.Count.Equals(0))
            {
                mainForm.Alert("You have not selected any rows.");
                return;
            }

            try
            {
                maxTextLength = new int[ABRGrid.SelectedCols.Count];
                
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ABRGrid.SelectedCols)
                {
                    maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
                }

                foreach (int rowIndex in ABRGrid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ABRGrid.SelectedCols)
                    {
                        if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
                        {
                            maxTextLength[columnIndex] = textLength;
                        }
                    }
                }

                columnIndex = -1;

                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ABRGrid.SelectedCols)
                {
                    gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
                }
                gridData += "\n";

                columnIndex = -1;

                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ABRGrid.SelectedCols)
                {
                    gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
                }
                gridData += "\n";

                foreach (int rowIndex in ABRGrid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ABRGrid.SelectedCols)
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

                mainForm.Alert("Total: " + ABRGrid.SelectedRows.Count + " items added to e-mail.");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [PositionABRForm.SendToEmailMenuItem_Click]", Log.Error, 1);
                mainForm.Alert(error.Message, PilotState.RunFault);
            }
        }

        private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Excel excel = new Excel();
            excel.ExportGridToExcel(ref ABRGrid, ABRGrid.SplitIndex);

            this.Cursor = Cursors.Default;
        }

        private void DockTopMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Height = mainForm.ClientSize.Height / 3;
            this.Dock = DockStyle.Top;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Height = mainForm.ClientSize.Height / 3;
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

        private void ABRGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0)
            {
                return;
            }

            switch (ABRGrid.Columns[e.ColIndex].DataField)
            {
                case "Available":
                case "Borrows":
                case "ReturnBorrows":
                    try
                    {
                        e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
                    }
                    catch { }
                    break;

                case "Price":
                case "BorrowsAmount":
                case "ReturnBorrowsAmount":
                    try
                    {
                        e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
                    }
                    catch { }
                    break;
            }
        }

        private void ABRContractGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0)
            {
                return;
            }

            switch (ABRContractGrid.Columns[e.ColIndex].DataField)
            {
                case "ReturnQuantity":
                case "Quantity":
                    try
                    {
                        e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
                    }
                    catch { }
                    break;
                case "Amount":
                    try
                    {
                        e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0");
                    }
                    catch { }
                    break;
                case "Rate":
                    try
                    {
                        e.Value = decimal.Parse(e.Value.ToString()).ToString("0.000");
                    }
                    catch { }
                    break;
            }
        }

        private void ABRGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
        {
            string filter;

            try
            {
                if (e.Column.DataColumn.DataField.Equals("DoReturn"))
                {
                    filter = "BookGroup = '" + ABRGrid.Columns["BookGroup"].Text + "' AND SecId = '" + ABRGrid.Columns["SecId"].Text.Trim() + "'";

                    foreach (DataRow row in borrowContracts.Select(filter))
                    {
                        row["DoReturn"] = bool.Parse(ABRGrid.Columns["DoReturn"].Value.ToString());
                    }

                    borrowContracts.AcceptChanges();
                    ABRGrid.DataChanged = false;

                    FooterSet();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionABRForm.ABRGrid_AfterUpdate]", Log.Error, 1);
            }
        }

        private void CheckAllMenuItem_Click(object sender, System.EventArgs e)
        {
            RecordCheck(true);
            FooterSet();
        }

        private void UncheckMenuitem_Click(object sender, System.EventArgs e)
        {
            RecordCheck(false);
            FooterSet();
        }

        private void ABRGrid_AfterUpdate(object sender, System.EventArgs e)
        {
            FooterSet();
        }

        private void ABRGrid_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            FooterSet();
        }
    }
}
