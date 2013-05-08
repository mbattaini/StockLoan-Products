using System;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using Anetics.Common;

namespace Anetics.Medalist
{
    public class PositionBoxSummaryForm : System.Windows.Forms.Form
    {
        private const string TEXT = "Position - Box Summary";

        private DataSet dataSetOriginal;
        private DataSet dataSet;
        private DataView dataView;

        private string secId = "";
        private string dataViewRowFilter = "";

        private MainForm mainForm;

        private C1.Win.C1Input.C1Label BookGroupNameLabel;
        private C1.Win.C1Input.C1Label BookGroupLabel;

        private C1.Win.C1List.C1Combo BookGroupCombo;

        private System.Windows.Forms.ContextMenu MainContextMenu;
        
        private System.Windows.Forms.MenuItem SendToMenuItem;
        private System.Windows.Forms.MenuItem SendToBlotterMenuItem;
        private System.Windows.Forms.MenuItem SendToEmailMenuItem;
        private System.Windows.Forms.MenuItem ShowMenuItem;
        private System.Windows.Forms.MenuItem ShowFailsMenuItem;
        private System.Windows.Forms.MenuItem ShowRecallsMenuItem;
        private System.Windows.Forms.MenuItem ShowBorrowLoanMenuItem;
        private System.Windows.Forms.MenuItem ShowExDeficitMenuItem;
        private System.Windows.Forms.MenuItem ShowPledgeMenuItem;
        private System.Windows.Forms.MenuItem Sep1MenuItem;
        private System.Windows.Forms.MenuItem DockMenuItem;
        private System.Windows.Forms.MenuItem DockTopMenuItem;
        private System.Windows.Forms.MenuItem DockBottomMenuItem;
        private System.Windows.Forms.MenuItem DockSep1MenuItem;
        private System.Windows.Forms.MenuItem DockNoneMenuItem;

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid BoxSummaryGrid;
        private C1.Win.C1Input.C1Label StatusLabel;
        private System.Windows.Forms.CheckBox OptimisticCheckBox;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.MenuItem SendToExcelMenuItem;
        private RadioButton RadioBoxSummaryNormal;
        private RadioButton Radio204;

        private System.ComponentModel.Container components = null;

        public PositionBoxSummaryForm(MainForm mainForm)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PositionBoxSummaryForm));
            this.BoxSummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
            this.BookGroupLabel = new C1.Win.C1Input.C1Label();
            this.BookGroupCombo = new C1.Win.C1List.C1Combo();
            this.MainContextMenu = new System.Windows.Forms.ContextMenu();
            this.SendToMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToBlotterMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowFailsMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowRecallsMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowBorrowLoanMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowExDeficitMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowPledgeMenuItem = new System.Windows.Forms.MenuItem();
            this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.DockMenuItem = new System.Windows.Forms.MenuItem();
            this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
            this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
            this.DockSep1MenuItem = new System.Windows.Forms.MenuItem();
            this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
            this.StatusLabel = new C1.Win.C1Input.C1Label();
            this.OptimisticCheckBox = new System.Windows.Forms.CheckBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.RadioBoxSummaryNormal = new System.Windows.Forms.RadioButton();
            this.Radio204 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.BoxSummaryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
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
            this.BoxSummaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BoxSummaryGrid.ExtendRightColumn = true;
            this.BoxSummaryGrid.FilterBar = true;
            this.BoxSummaryGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoxSummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.BoxSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("BoxSummaryGrid.Images"))));
            this.BoxSummaryGrid.Location = new System.Drawing.Point(1, 32);
            this.BoxSummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.BoxSummaryGrid.Name = "BoxSummaryGrid";
            this.BoxSummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.BoxSummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.BoxSummaryGrid.PreviewInfo.ZoomFactor = 75D;
            this.BoxSummaryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("BoxSummaryGrid.PrintInfo.PageSettings")));
            this.BoxSummaryGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.BoxSummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.BoxSummaryGrid.RowHeight = 15;
            this.BoxSummaryGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
            this.BoxSummaryGrid.Size = new System.Drawing.Size(1656, 381);
            this.BoxSummaryGrid.TabIndex = 0;
            this.BoxSummaryGrid.Text = "BoxSummary";
            this.BoxSummaryGrid.AfterUpdate += new System.EventHandler(this.BoxSummaryGrid_AfterUpdate);
            this.BoxSummaryGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BoxSummaryGrid_BeforeUpdate);
            this.BoxSummaryGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BoxSummaryGrid_SelChange);
            this.BoxSummaryGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.BoxSummaryGrid_BeforeColEdit);
            this.BoxSummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.BoxSummaryGrid_FormatText);
            this.BoxSummaryGrid.FilterChange += new System.EventHandler(this.BoxSummaryGrid_FilterChange);
            this.BoxSummaryGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.BoxSummaryGrid_Paint);
            this.BoxSummaryGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BoxSummaryGrid_KeyPress);
            this.BoxSummaryGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoxSummaryGrid_MouseDown);
            this.BoxSummaryGrid.PropBag = resources.GetString("BoxSummaryGrid.PropBag");
            // 
            // BookGroupNameLabel
            // 
            this.BookGroupNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
            this.BookGroupNameLabel.Location = new System.Drawing.Point(209, 7);
            this.BookGroupNameLabel.Name = "BookGroupNameLabel";
            this.BookGroupNameLabel.Size = new System.Drawing.Size(298, 18);
            this.BookGroupNameLabel.TabIndex = 6;
            this.BookGroupNameLabel.Tag = null;
            this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // BookGroupLabel
            // 
            this.BookGroupLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BookGroupLabel.Location = new System.Drawing.Point(11, 7);
            this.BookGroupLabel.Name = "BookGroupLabel";
            this.BookGroupLabel.Size = new System.Drawing.Size(92, 18);
            this.BookGroupLabel.TabIndex = 4;
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
            this.BookGroupCombo.Caption = "";
            this.BookGroupCombo.CaptionHeight = 17;
            this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.BookGroupCombo.ColumnCaptionHeight = 17;
            this.BookGroupCombo.ColumnFooterHeight = 17;
            this.BookGroupCombo.ContentHeight = 16;
            this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.BookGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.BookGroupCombo.DropDownWidth = 425;
            this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.BookGroupCombo.EditorHeight = 16;
            this.BookGroupCombo.ExtendRightColumn = true;
            this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images"))));
            this.BookGroupCombo.ItemHeight = 15;
            this.BookGroupCombo.KeepForeColor = true;
            this.BookGroupCombo.LimitToList = true;
            this.BookGroupCombo.Location = new System.Drawing.Point(107, 5);
            this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
            this.BookGroupCombo.MaxDropDownItems = ((short)(10));
            this.BookGroupCombo.MaxLength = 15;
            this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
            this.BookGroupCombo.Name = "BookGroupCombo";
            this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookGroupCombo.Size = new System.Drawing.Size(96, 22);
            this.BookGroupCombo.TabIndex = 5;
            this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
            this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SendToMenuItem,
            this.ShowMenuItem,
            this.Sep1MenuItem,
            this.DockMenuItem});
            // 
            // SendToMenuItem
            // 
            this.SendToMenuItem.Index = 0;
            this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SendToBlotterMenuItem,
            this.SendToExcelMenuItem,
            this.SendToEmailMenuItem});
            this.SendToMenuItem.Text = "Send To";
            // 
            // SendToBlotterMenuItem
            // 
            this.SendToBlotterMenuItem.Index = 0;
            this.SendToBlotterMenuItem.Text = "Blotter";
            this.SendToBlotterMenuItem.Click += new System.EventHandler(this.SendToBlotterMenuItem_Click);
            // 
            // SendToExcelMenuItem
            // 
            this.SendToExcelMenuItem.Index = 1;
            this.SendToExcelMenuItem.Text = "Excel";
            this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
            // 
            // SendToEmailMenuItem
            // 
            this.SendToEmailMenuItem.Index = 2;
            this.SendToEmailMenuItem.Text = "Mail Recipient";
            this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
            // 
            // ShowMenuItem
            // 
            this.ShowMenuItem.Index = 1;
            this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ShowFailsMenuItem,
            this.ShowRecallsMenuItem,
            this.ShowBorrowLoanMenuItem,
            this.ShowExDeficitMenuItem,
            this.ShowPledgeMenuItem});
            this.ShowMenuItem.Text = "Show";
            // 
            // ShowFailsMenuItem
            // 
            this.ShowFailsMenuItem.Index = 0;
            this.ShowFailsMenuItem.Text = "Fails";
            this.ShowFailsMenuItem.Click += new System.EventHandler(this.ShowFailsMenuItem_Click);
            // 
            // ShowRecallsMenuItem
            // 
            this.ShowRecallsMenuItem.Index = 1;
            this.ShowRecallsMenuItem.Text = "Recalls";
            this.ShowRecallsMenuItem.Click += new System.EventHandler(this.ShowRecallsMenuItem_Click);
            // 
            // ShowBorrowLoanMenuItem
            // 
            this.ShowBorrowLoanMenuItem.Index = 2;
            this.ShowBorrowLoanMenuItem.Text = "Borrow Loan";
            this.ShowBorrowLoanMenuItem.Click += new System.EventHandler(this.ShowBorrowLoanMenuItem_Click);
            // 
            // ShowExDeficitMenuItem
            // 
            this.ShowExDeficitMenuItem.Index = 3;
            this.ShowExDeficitMenuItem.Text = "Excess Deficit";
            this.ShowExDeficitMenuItem.Click += new System.EventHandler(this.ShowExDeficitMenuItem_Click);
            // 
            // ShowPledgeMenuItem
            // 
            this.ShowPledgeMenuItem.Index = 4;
            this.ShowPledgeMenuItem.Text = "Pledge";
            this.ShowPledgeMenuItem.Click += new System.EventHandler(this.ShowPledgeMenuItem_Click);
            // 
            // Sep1MenuItem
            // 
            this.Sep1MenuItem.Index = 2;
            this.Sep1MenuItem.Text = "-";
            // 
            // DockMenuItem
            // 
            this.DockMenuItem.Index = 3;
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
            this.StatusLabel.Location = new System.Drawing.Point(20, 416);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(1124, 16);
            this.StatusLabel.TabIndex = 7;
            this.StatusLabel.Tag = null;
            this.StatusLabel.TextDetached = true;
            // 
            // OptimisticCheckBox
            // 
            this.OptimisticCheckBox.Location = new System.Drawing.Point(901, 8);
            this.OptimisticCheckBox.Name = "OptimisticCheckBox";
            this.OptimisticCheckBox.Size = new System.Drawing.Size(224, 16);
            this.OptimisticCheckBox.TabIndex = 8;
            this.OptimisticCheckBox.Text = "Optimistic [All deliveries will make]";
            this.OptimisticCheckBox.CheckedChanged += new System.EventHandler(this.OptimisticCheckBox_CheckedChanged);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(749, 6);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(76, 20);
            this.RefreshButton.TabIndex = 9;
            this.RefreshButton.Text = "REFRESH";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // RadioBoxSummaryNormal
            // 
            this.RadioBoxSummaryNormal.AutoSize = true;
            this.RadioBoxSummaryNormal.Location = new System.Drawing.Point(479, 8);
            this.RadioBoxSummaryNormal.Name = "RadioBoxSummaryNormal";
            this.RadioBoxSummaryNormal.Size = new System.Drawing.Size(108, 17);
            this.RadioBoxSummaryNormal.TabIndex = 10;
            this.RadioBoxSummaryNormal.Text = "Normal Layout";
            this.RadioBoxSummaryNormal.UseVisualStyleBackColor = true;
            this.RadioBoxSummaryNormal.CheckedChanged += new System.EventHandler(this.RadioBoxSummaryNormal_CheckedChanged);
            // 
            // Radio204
            // 
            this.Radio204.AutoSize = true;
            this.Radio204.Location = new System.Drawing.Point(593, 8);
            this.Radio204.Name = "Radio204";
            this.Radio204.Size = new System.Drawing.Size(126, 17);
            this.Radio204.TabIndex = 11;
            this.Radio204.Text = "204 Recall Layout";
            this.Radio204.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Radio204.UseVisualStyleBackColor = true;
            this.Radio204.CheckedChanged += new System.EventHandler(this.Radio204_CheckedChanged);
            // 
            // PositionBoxSummaryForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1658, 433);
            this.ContextMenu = this.MainContextMenu;
            this.Controls.Add(this.Radio204);
            this.Controls.Add(this.RadioBoxSummaryNormal);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.OptimisticCheckBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.BookGroupNameLabel);
            this.Controls.Add(this.BookGroupLabel);
            this.Controls.Add(this.BookGroupCombo);
            this.Controls.Add(this.BoxSummaryGrid);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PositionBoxSummaryForm";
            this.Padding = new System.Windows.Forms.Padding(1, 32, 1, 20);
            this.Text = "Position - Box Summary";
            this.Closed += new System.EventHandler(this.PositionBoxSummaryForm_Closed);
            this.Load += new System.EventHandler(this.PositionBoxSummaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BoxSummaryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public string SecId
        {
            set
            {
                if (!secId.Equals(value))
                {
                    BoxSummaryGrid.Columns["SecId"].FilterText = value;
                }
            }

            get
            {
                return secId;
            }
        }

        private void DataLoad()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            mainForm.Alert("Please wait... Loading current box summary - normal.", PilotState.Idle);

            try
            {
                
                dataSet.Tables["BoxSummary"].Columns.Add("AvailableModified", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("AvailableAmount", typeof(decimal));
                dataSet.Tables["BoxSummary"].Columns.Add("PledgeUndo", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("Borrow", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("BorrowAmount", typeof(decimal));
                dataSet.Tables["BoxSummary"].Columns.Add("Recall", typeof(long));                                                
                
                dataView = new DataView(dataSet.Tables["BoxSummary"]);
                dataView.Sort = "SecId";

                BoxSummaryGrid.SetDataBinding(dataView, null, true);
                DataConfig();

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

                mainForm.Alert("Done. Loaded " + dataSet.Tables["BoxSummary"].Rows.Count + " summary items.", PilotState.Idle);
                
                this.Cursor = Cursors.Default;
            }
            catch (Exception ee)
            {
                this.Cursor = Cursors.Default;
                mainForm.Alert(ee.Message, PilotState.RunFault);
                Log.Write(ee.Message + " [PositionBoxSummaryForm.PositionBoxSummaryForm_Load]", Log.Error, 1);
            }
        }


        private void DataLoad204()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            mainForm.Alert("Please wait... Loading current box summary - 204.", PilotState.Idle);

            try
            {
                dataSet.Tables["BoxSummary"].Columns.Add("AvailableModified", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("PledgeUndo", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("Recall", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("Borrow", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("LoanRecallsModified", typeof(long));                                
                dataSet.Tables["BoxSummary"].Columns.Add("CnsCombined", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("CnsProjectedCombined", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("TradeQuantityModified", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("NetPositionModified", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("RecallModified", typeof(long));
                dataSet.Tables["BoxSummary"].Columns.Add("RecallTotal", typeof(long));

                dataView = new DataView(dataSet.Tables["BoxSummary"]);
                dataView.Sort = "SecId";

                BoxSummaryGrid.SetDataBinding(dataView, null, true);
                DataConfig204();

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

                mainForm.Alert("Done. Loaded " + dataSet.Tables["BoxSummary"].Rows.Count + " summary items.", PilotState.Idle);
                
                this.Cursor = Cursors.Default;
            }
            catch (Exception ee)
            {
                this.Cursor = Cursors.Default;
                mainForm.Alert(ee.Message, PilotState.RunFault);
                Log.Write(ee.Message + " [PositionBoxSummaryForm.PositionBoxSummaryForm_Load]", Log.Error, 1);
            }
        }

        private void DataConfig()
        {
            string secId = "";

            long available;
            long recallBalance;
            long deliveryBalance;
            
            double hairCut = 0;

            try
            {
                hairCut = double.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryHairCut", "10"));
                hairCut = hairCut / 100;
            }
            catch
            {
                hairCut = 0;
            }

            try
            {
                foreach (DataRow row in dataSet.Tables["BoxSummary"].Rows)
                {
                    secId = "[" + row["SecId"].ToString().Trim() + "] "; // Value captured here for error reporting only.

                    recallBalance = long.Parse(row["LoanRecalls"].ToString()) - long.Parse(row["BorrowRecalls"].ToString());

                    if (OptimisticCheckBox.Checked)
                    {
                        deliveryBalance = long.Parse(row["ReceiveFails"].ToString());
                    }
                    else
                    {
                        deliveryBalance = 0;
                    }


                    available = long.Parse(row["Available"].ToString());
                    available = available + deliveryBalance;

                    if (available <= 0) // Must move securities in.
                    {
                        if ((available + long.Parse(row["OnPledge"].ToString())) >= 0)
                        {
                            row["PledgeUndo"] = -available;                            
                        }
                        else
                        {
                            row["PledgeUndo"] = row["OnPledge"];                            
                        }

                        if ((bool)row["DoNotRecall"])
                        {
                            row["Recall"] = 0;
                        }
                        else
                        {
                            if ((available + long.Parse(row["Loans"].ToString()) - long.Parse(row["LoanRecalls"].ToString())) >= 0)
                            {
                                row["Recall"] = -(available);                                
                            }
                            else if (available < 0)
                            {
                                row["Recall"] = long.Parse(row["Loans"].ToString()) - long.Parse(row["LoanRecalls"].ToString());                                
                            }
                            else
                            {
                                row["Recall"] = 0;
                            }
                        }

                        row["Borrow"] = -(available);
                    }
                    else // Make returns and/or cancell recalls.
                    {
                        if (available >= long.Parse(row["LoanRecalls"].ToString()))
                        {
                            row["Recall"] = -long.Parse(row["LoanRecalls"].ToString());                            
                        }
                        else if (available < long.Parse(row["LoanRecalls"].ToString()))
                        {
                            row["Recall"] = -available;

                            if (row["BaseType"].ToString().Equals("B"))
                            {
                                row["Recall"] = long.Parse(row["Recall"].ToString()) - (long.Parse(row["Recall"].ToString()) % 100000);                                
                            }
                            else
                            {
                                row["Recall"] = long.Parse(row["Recall"].ToString()) - (long.Parse(row["Recall"].ToString()) % 100);                                
                            }
                        }

                        if (available > long.Parse(row["Borrows"].ToString()))
                        {
                            row["Borrow"] = -long.Parse(row["Borrows"].ToString());                            
                        }
                        else if (available < long.Parse(row["Borrows"].ToString()))
                        {
                            row["Borrow"] = -available;                            
                        }
                        else
                        {
                            row["Borrow"] = 0;
                        }
                    }

                    if (long.Parse(row["Recall"].ToString()) > 0)
                    {
                        if (long.Parse(row["LoanRecalls"].ToString()) >= long.Parse(row["Recall"].ToString()))
                        {
                            row["Recall"] = 0;
                        }
                        else
                        {
                            row["Recall"] = long.Parse(row["Recall"].ToString()) - long.Parse(row["LoanRecalls"].ToString());
                        }
                    }
                    else if (long.Parse(row["Recall"].ToString()) < 0)
                    {
                        if (available > 0)
                        {
                            row["Recall"] = -long.Parse(row["LoanRecalls"].ToString());
                        }
                        else
                        {
                            row["Recall"] = available + long.Parse(row["Recall"].ToString());
                        }
                    }

                    if (long.Parse(row["Borrow"].ToString()) > 0)
                    {
                        if (row["BaseType"].ToString().Equals("B") && ((long.Parse(row["Borrow"].ToString()) % 100000) > 0))
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 100000) + 100000;
                        }
                        else if ((long.Parse(row["Borrow"].ToString()) % 100) >= 0)
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 100) + 100;
                        }
                    }
                    else if (long.Parse(row["Borrow"].ToString()) < 0)
                    {
                        if (row["BaseType"].ToString().Equals("B"))
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 100000);
                        }
                        else
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 100);
                        }
                    }                    
                    

                    row["AvailableModified"] = long.Parse(row["Available"].ToString()) - (long.Parse(row["Available"].ToString()) % 100);                   

                    if (!row["LastPrice"].Equals(DBNull.Value))
                    {
                        if (row["BaseType"].ToString().Equals("B"))
                        {
                            row["AvailableAmount"] = decimal.Parse((long.Parse(row["Available"].ToString()) * double.Parse(row["LastPrice"].ToString()) / 100.0).ToString());
                            row["BorrowAmount"] = decimal.Parse((long.Parse(row["Borrow"].ToString()) * double.Parse(row["LastPrice"].ToString()) / 100.0).ToString());                            
                        }
                        else
                        {

                            row["AvailableAmount"] = decimal.Parse((long.Parse(row["Available"].ToString()) * double.Parse(row["LastPrice"].ToString())).ToString());
                            row["BorrowAmount"] = decimal.Parse((long.Parse(row["Borrow"].ToString()) * double.Parse(row["LastPrice"].ToString())).ToString());
                        }
                    }
                    else
                    {
                        row["AvailableAmount"] = DBNull.Value;
                        row["BorrowAmount"] = DBNull.Value;                        
                    }                                     
                }
            }
            catch (Exception e)
            {
                mainForm.Alert("[" + secId + "] " + e.Message, PilotState.RunFault);
                Log.Write("[" + secId + e.Message + "] " + " [PositionBoxSummaryForm.DataConfig]", Log.Error, 1);
            }
        }

        private void DataConfig204()
        {
            string secId = "";

            long available;
            long recallBalance;
            long deliveryBalance;
            long availableTraded = 0;
            long recall = 0;
            long recallTotal = 0;
            long loanOutStanding = 0;
            long exposure = 0;
            long cnsCombined = 0;

            double hairCut = 0;

            try
            {
                hairCut = double.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryHairCut", "10"));
                hairCut = hairCut / 100;
            }
            catch
            {
                hairCut = 0;
            }

            try
            {
                foreach (DataRow row in dataSet.Tables["BoxSummary"].Rows)
                {
                    secId = "[" + row["SecId"].ToString().Trim() + "] "; // Value captured here for error reporting only.

                    recallBalance = long.Parse(row["LoanRecalls"].ToString()) - long.Parse(row["BorrowRecalls"].ToString());

                    if (OptimisticCheckBox.Checked)
                    {
                        deliveryBalance = long.Parse(row["ReceiveFails"].ToString());
                    }
                    else
                    {
                        deliveryBalance = 0;
                    }


                    available = long.Parse(row["Available"].ToString());
                    available = available + deliveryBalance;

                    row["LoanRecallsModified"] = long.Parse(row["LoanRecalls"].ToString()) - long.Parse(row["LoanRecallsToday"].ToString());
                    row["Borrow"] = 0;

                    if (available <= 0) // Must move securities in.
                    {
                        if ((available + long.Parse(row["OnPledge"].ToString())) >= 0)
                        {
                            row["PledgeUndo"] = -available;
                        }
                        else
                        {
                            row["PledgeUndo"] = row["OnPledge"];
                        }

                        if ((bool)row["DoNotRecall"])
                        {
                            row["Recall"] = 0;
                        }
                        else
                        {
                            if ((available + long.Parse(row["Loans"].ToString()) - long.Parse(row["LoanRecalls"].ToString())) >= 0)
                            {
                                row["Recall"] = -(available);
                            }
                            else if (available < 0)
                            {
                                row["Recall"] = long.Parse(row["Loans"].ToString()) - long.Parse(row["LoanRecalls"].ToString());
                            }
                            else
                            {
                                row["Recall"] = 0;
                            }
                        }

                        row["Borrow"] = -(available);
                    }
                    else // Make returns and/or cancell recalls.
                    {
                        if (available >= long.Parse(row["LoanRecalls"].ToString()))
                        {
                            row["Recall"] = -long.Parse(row["LoanRecalls"].ToString());
                        }
                        else if (available < long.Parse(row["LoanRecalls"].ToString()))
                        {
                            row["Recall"] = -available;

                            if (row["BaseType"].ToString().Equals("B"))
                            {
                                row["Recall"] = long.Parse(row["Recall"].ToString()) - (long.Parse(row["Recall"].ToString()) % 100000);
                            }
                            else
                            {
                                row["Recall"] = long.Parse(row["Recall"].ToString()) - (long.Parse(row["Recall"].ToString()) % 100);
                            }
                        }
                    }

                    if (long.Parse(row["Recall"].ToString()) > 0)
                    {
                        if (long.Parse(row["LoanRecalls"].ToString()) >= long.Parse(row["Recall"].ToString()))
                        {
                            row["Recall"] = 0;
                        }
                        else
                        {
                            row["Recall"] = long.Parse(row["Recall"].ToString()) - long.Parse(row["LoanRecalls"].ToString());
                        }
                    }
                    else if (long.Parse(row["Recall"].ToString()) < 0)
                    {
                        if (available > 0)
                        {
                            row["Recall"] = -long.Parse(row["LoanRecalls"].ToString());
                        }
                        else
                        {
                            row["Recall"] = available + long.Parse(row["Recall"].ToString());
                        }
                    }

                    availableTraded = available - Math.Abs(long.Parse(row["TradeQuantity"].ToString()));
                    recall = long.Parse(row["Recall"].ToString());
                    recallTotal = long.Parse(row["LoanRecalls"].ToString()) + long.Parse(row["Recall"].ToString());
                    loanOutStanding = (long.Parse(row["Loans"].ToString()) - long.Parse(row["LoanRecalls"].ToString()));


                    row["CnsCombined"] = long.Parse(row["ClearingFailOutSettled"].ToString()) - long.Parse(row["ClearingFailInSettled"].ToString());

                    cnsCombined = long.Parse(row["CnsCombined"].ToString());

                    exposure = long.Parse(row["Projected_Cns"].ToString()) - long.Parse(row["CnsCombined"].ToString());

                    if (exposure < 0)
                    {
                        row["CnsProjectedCombined"] = 0;
                    }
                    else
                    {
                        row["CnsProjectedCombined"] = exposure;
                    }

                    /*if (cnsCombined >= 0)
                    {
                        if (long.Parse(row["TradeQuantity"].ToString()) > long.Parse(row["CnsProjectedCombined"].ToString()))
                        {
                            row["TradeQuantityModified"] = long.Parse(row["CnsProjectedCombined"].ToString());
                        }
                        else
                        {
                            row["TradeQuantityModified"] = long.Parse(row["TradeQuantity"].ToString());
                        }
                    }
                    else
                    {
                        long[] values = { long.Parse(row["TradeQuantity"].ToString()), long.Parse(row["Projected_Cns"].ToString()), long.Parse(row["CnsProjectedCombined"].ToString()) };

                        Array.Sort(values);

                        row["TradeQuantityModified"] = values[0];
                    }*/

                    row["TradeQuantityModified"] = long.Parse(row["TradeQuantity"].ToString());

                    row["NetPositionModified"] = long.Parse(row["Available"].ToString()) - long.Parse(row["TradeQuantityModified"].ToString());

                    if (long.Parse(row["NetPositionModified"].ToString()) < 0)
                    {
                        if (Math.Abs(long.Parse(row["NetPositionModified"].ToString())) > long.Parse(row["Loans"].ToString()))
                        {
                            row["RecallModified"] = long.Parse(row["Loans"].ToString()) - long.Parse(row["LoanRecalls"].ToString()) - long.Parse(row["Recall"].ToString());
                        }
                        else
                        {
                            row["RecallModified"] = Math.Abs(long.Parse(row["NetPositionModified"].ToString())) - long.Parse(row["LoanRecalls"].ToString()) - long.Parse(row["Recall"].ToString());
                        }
                    }
                    else
                    {
                        row["RecallModified"] = 0 - long.Parse(row["LoanRecalls"].ToString()) - long.Parse(row["Recall"].ToString());
                    }

                    row["RecallTotal"] = long.Parse(row["RecallModified"].ToString()) + long.Parse(row["Recall"].ToString());
                }
            }
            catch (Exception e)
            {
                mainForm.Alert("[" + secId + "] " + e.Message, PilotState.RunFault);
                Log.Write("[" + secId + e.Message + "] " + " [PositionBoxSummaryForm.DataConfig]", Log.Error, 1);
            }
        }

        private void StatusSet()
        {
            if (BoxSummaryGrid.SelectedRows.Count > 0)
            {
                StatusLabel.Text = "Selected " + BoxSummaryGrid.SelectedRows.Count.ToString("#,##0") + " items of " + dataView.Count.ToString("#,##0") + " shown in grid.";
            }
            else
            {
                StatusLabel.Text = "Showing " + dataView.Count.ToString("#,##0") + " items in grid.";
            }
        }

        private void PositionBoxSummaryForm_Load(object sender, System.EventArgs e)
        {
            int height = mainForm.Height - 275;
            int width = mainForm.Width - 45;

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

            this.Show();
            Application.DoEvents();

            try
            {

                dataSetOriginal = mainForm.PositionAgent.BoxSummaryDataGet(mainForm.UtcOffset, mainForm.UserId, "PositionBoxSummary");

                RadioBoxSummaryNormal.Checked = true;
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
            }
        }

        private void PositionBoxSummaryForm_Closed(object sender, System.EventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.positionBoxSummaryForm = null;
        }

        private void OptimisticCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (Radio204.Checked)
            {
                DataConfig204();
            }
            else
            {
                DataConfig();
            }

            StatusSet();
        }

        private void BoxSummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Equals(""))
            {
                return;
            }

            switch (BoxSummaryGrid.Columns[e.ColIndex].DataField)
            {
                case ("ActTime"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
                    }
                    catch { }
                    break;
                case ("LastPrice"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.000");
                    }
                    catch { }
                    break;

                case ("Available"):
                case ("ExDeficit"):
                case ("RockAvailableModified"):
                case ("PensonAvailableModified"):
                    try
                    {
                        if ((long.Parse(e.Value.ToString()) < 0) && RadioBoxSummaryNormal.Checked)
                        {
                            e.Value = "0";
                        }
                        else
                        {
                            e.Value = long.Parse(e.Value).ToString("#,##0");
                        }
                    }
                    catch { }
                    break;
               
                default:
                    try
                    {
                        e.Value = long.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;
            }
        }

        private void BoxSummaryGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string gridData = "";

            if (e.KeyChar.Equals((char)3) && BoxSummaryGrid.SelectedRows.Count > 0)
            {
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
                {
                    gridData += dataColumn.Caption + "\t";
                }

                gridData += "\n";

                foreach (int row in BoxSummaryGrid.SelectedRows)
                {
                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
                    {
                        gridData += dataColumn.CellText(row) + "\t";
                    }

                    gridData += "\n";

                    if ((row % 100) == 0)
                    {
                        mainForm.Alert("Please wait: " + row.ToString("#,##0") + " rows copied so far...");
                    }
                }

                Clipboard.SetDataObject(gridData, true);
                mainForm.Alert("Copied " + BoxSummaryGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
                e.Handled = true;
            }
        }

        private void BoxSummaryGrid_FilterChange(object sender, System.EventArgs e)
        {
            string gridFilter;

            try
            {
                gridFilter = mainForm.GridFilterGet(ref BoxSummaryGrid);

                if (gridFilter.Equals(""))
                {
                    dataView.RowFilter = dataViewRowFilter;
                }
                else
                {
                    dataView.RowFilter = dataViewRowFilter + " AND " + gridFilter;
                }
            }
            catch (Exception ee)
            {
                mainForm.Alert(ee.Message, PilotState.RunFault);
            }

            StatusSet();
        }

        private void BoxSummaryGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
        {
            if (BoxSummaryGrid.FilterActive)
            {
                e.Cancel = false;
                return;
            }

            switch (e.Column.Name)
            {
                case "No":
                case "Comment":
                    e.Cancel = false;
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        private void BoxSummaryGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            string secId = BoxSummaryGrid.Columns["Security ID"].Text.Trim();

            if (BoxSummaryGrid.Columns["Symbol"].Text.Trim().Length > 0)
            {
                secId += " [" + BoxSummaryGrid.Columns["Symbol"].Text.Trim() + "]";
            }

            try
            {
                mainForm.Alert("Updating record for " + secId + "...");

                mainForm.PositionAgent.BoxSummarySet(
                  BookGroupCombo.Text,
                  BoxSummaryGrid.Columns["Security ID"].Text,
                  bool.Parse(BoxSummaryGrid.Columns["No"].Text),
                  BoxSummaryGrid.Columns["Comment"].Text,
                  mainForm.UserId);

                BoxSummaryGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.TimeFileFormat);
                BoxSummaryGrid.Columns["ActUserShortName"].Text = "me";

                mainForm.Alert("Updating record for " + secId + "... Done!");
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + " [PositionBoxSummaryForm.BoxSummaryGrid_BeforeUpdate]", Log.Error, 1);
            }
        }

        private void BoxSummaryGrid_AfterUpdate(object sender, System.EventArgs e)
        {
            if (Radio204.Checked)
            {
                DataConfig204();
            }
            else
            {
                DataConfig();
            }
        }

        private void BoxSummaryGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            StatusSet();
        }

        private void BoxSummaryGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (!BoxSummaryGrid.Columns["SecId"].Text.Equals(secId))
                {
                    if (BoxSummaryGrid.Splits[0, 0].Rows.Count > 0)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        Application.DoEvents();

                        try
                        {
                            secId = BoxSummaryGrid.Columns["SecId"].Text;
                            mainForm.SecId = secId;

                            if (!BoxSummaryGrid.Columns["Recall"].Value.Equals(DBNull.Value) && (long.Parse(BoxSummaryGrid.Columns["Recall"].Value.ToString()) > 0))
                            {
                                mainForm.RecallQuantity = long.Parse(BoxSummaryGrid.Columns["Recall"].Value.ToString());
                            }
                            else
                            {
                                mainForm.RecallQuantity = 0;
                            }
                            
                            if (!BoxSummaryGrid.Columns["Borrow"].Value.Equals(DBNull.Value) && (long.Parse(BoxSummaryGrid.Columns["Borrow"].Value.ToString()) < 0))
                            {
                                mainForm.ReturnQuantity = -long.Parse(BoxSummaryGrid.Columns["Borrow"].Value.ToString());
                            }
                            else
                            {
                                mainForm.ReturnQuantity = 0;
                            }
                        }
                        catch { }

                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        secId = "";
                        mainForm.SecId = secId;

                        mainForm.RecallQuantity = 0;
                        mainForm.ReturnQuantity = 0;
                    }
                }
            }
            catch { }
        }

        private void BookGroupCombo_TextChanged(object sender, System.EventArgs e)
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
                BoxSummaryGrid.AllowUpdate = false;

                mainForm.GridFilterClear(ref BoxSummaryGrid);

                dataViewRowFilter = "[BookGroup] = '" + BookGroupCombo.Text + "'";
                dataView.RowFilter = dataViewRowFilter;


                if (Radio204.Checked)
                {
                    DataConfig204();
                }
                else
                {
                    DataConfig();
                }
                StatusSet();

                if (bool.Parse(dataSet.Tables["BookGroups"].Rows[row]["MayEdit"].ToString()))
                {
                    BoxSummaryGrid.AllowUpdate = true;
                }
                else
                {
                    mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
                }
            }
            else
            {
                dataViewRowFilter = "[BookGroup] = ''";
                dataView.RowFilter = dataViewRowFilter;
                mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
            }

            RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);
        }

        private void SendToBlotterMenuItem_Click(object sender, System.EventArgs e)
        {
            string gridData = "";
            string borrowLoanFlag = null;

            foreach (int row in BoxSummaryGrid.SelectedRows)
            {
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
                {
                    switch (dataColumn.DataField)
                    {
                        case "Available":
                            borrowLoanFlag = "L";
                            break;
                        case "Borrow":
                            borrowLoanFlag = "B";
                            break;
                        default:
                            borrowLoanFlag = null;
                            break;
                    }

                    gridData += dataColumn.CellText(row).Trim() + "\t";
                }

                gridData += "\n";
            }

            if (borrowLoanFlag == null)
            {
                mainForm.Alert("Second column in selected range is not 'Available' or 'Borrow'.", PilotState.RunFault);
                return;
            }

            try
            {
                mainForm.Alert(
                  mainForm.PositionAgent.DealListSubmit(
                    BookGroupCombo.Text,
                    "",
                    "",
                    borrowLoanFlag,
                    "C",
                    "",
                    gridData,
                    mainForm.UserId)
                  );

                BoxSummaryGrid.SelectedCols.Clear();
                BoxSummaryGrid.SelectedRows.Clear();
            }
            catch (Exception ee)
            {
                mainForm.Alert(ee.Message, PilotState.RunFault);
                Log.Write(ee.Message + " [PositionBoxSummaryForm.SendToBlotterMenuItem_Click]", Log.Error, 1);
            }
        }

        private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
        {
            int textLength;
            int[] maxTextLength;

            int columnIndex = -1;
            string gridData = "\n\n\n";

            if (BoxSummaryGrid.SelectedCols.Count.Equals(0))
            {
                mainForm.Alert("You have not selected any rows.");
                return;
            }

            try
            {
                maxTextLength = new int[BoxSummaryGrid.SelectedCols.Count];

                // Get the caption length for each column.
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
                {
                    maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
                }

                // Get the maximum item length for each row in each column.
                foreach (int rowIndex in BoxSummaryGrid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
                    {
                        if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
                        {
                            maxTextLength[columnIndex] = textLength;
                        }
                    }
                }

                columnIndex = -1;

                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
                {
                    gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
                }
                gridData += "\n";

                columnIndex = -1;

                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
                {
                    gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
                }
                gridData += "\n";

                foreach (int rowIndex in BoxSummaryGrid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.SelectedCols)
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

                mainForm.Alert("Total: " + BoxSummaryGrid.SelectedRows.Count + " items added to e-mail.");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [PositionBoxSummaryForm.SendToEmailMenuItem_Click]", Log.Error, 1);
                mainForm.Alert(error.Message, PilotState.RunFault);
            }
        }

        private void ShowFailsMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowFailsMenuItem.Checked = !ShowFailsMenuItem.Checked;

            BoxSummaryGrid.Splits[0, 0].DisplayColumns["ReceiveFails"].Visible = ShowFailsMenuItem.Checked;
            BoxSummaryGrid.Splits[0, 0].DisplayColumns["DeliveryFails"].Visible = ShowFailsMenuItem.Checked;
        }

        private void ShowRecallsMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowRecallsMenuItem.Checked = !ShowRecallsMenuItem.Checked;

            BoxSummaryGrid.Splits[0, 0].DisplayColumns["BorrowRecalls"].Visible = ShowRecallsMenuItem.Checked;
            BoxSummaryGrid.Splits[0, 0].DisplayColumns["LoanRecalls"].Visible = ShowRecallsMenuItem.Checked;
        }

        private void ShowBorrowLoanMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowBorrowLoanMenuItem.Checked = !ShowBorrowLoanMenuItem.Checked;

            BoxSummaryGrid.Splits[0, 0].DisplayColumns["Borrows"].Visible = ShowBorrowLoanMenuItem.Checked;
            BoxSummaryGrid.Splits[0, 0].DisplayColumns["Loans"].Visible = ShowBorrowLoanMenuItem.Checked;
        }

        private void ShowExDeficitMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowExDeficitMenuItem.Checked = !ShowExDeficitMenuItem.Checked;

            BoxSummaryGrid.Splits[0, 0].DisplayColumns["ExDeficit"].Visible = ShowExDeficitMenuItem.Checked;
        }

        private void ShowPledgeMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowPledgeMenuItem.Checked = !ShowPledgeMenuItem.Checked;

            BoxSummaryGrid.Splits[0, 0].DisplayColumns["OnPledge"].Visible = ShowPledgeMenuItem.Checked;
            BoxSummaryGrid.Splits[0, 0].DisplayColumns["PledgeUndo"].Visible = ShowPledgeMenuItem.Checked;
        }

        private void DockTopMenuItem_Click(object sender, System.EventArgs e)
        {
            RegistryValue.Write(this.Name, "Top", this.Top.ToString());
            RegistryValue.Write(this.Name, "Left", this.Left.ToString());
            RegistryValue.Write(this.Name, "Height", this.Height.ToString());
            RegistryValue.Write(this.Name, "Width", this.Width.ToString());

            this.Height = mainForm.ClientSize.Height / 3;
            this.Dock = DockStyle.Top;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
        {
            RegistryValue.Write(this.Name, "Top", this.Top.ToString());
            RegistryValue.Write(this.Name, "Left", this.Left.ToString());
            RegistryValue.Write(this.Name, "Height", this.Height.ToString());
            RegistryValue.Write(this.Name, "Width", this.Width.ToString());

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

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height"));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width"));
        }

        private void BoxSummaryGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X <= BoxSummaryGrid.RecordSelectorWidth && e.Y <= BoxSummaryGrid.RowHeight)
            {
                if (BoxSummaryGrid.SelectedRows.Count.Equals(0))
                {
                    for (int i = 0; i < BoxSummaryGrid.Splits[0, 0].Rows.Count; i++)
                    {
                        BoxSummaryGrid.SelectedRows.Add(i);
                    }

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BoxSummaryGrid.Columns)
                    {
                        BoxSummaryGrid.SelectedCols.Add(dataColumn);
                    }
                }
                else
                {
                    BoxSummaryGrid.SelectedRows.Clear();
                    BoxSummaryGrid.SelectedCols.Clear();
                }
            }
        }

        private void RefreshButton_Click(object sender, System.EventArgs e)
        {
            DataLoad();
        }

        private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Excel excel = new Excel();
            excel.ExportGridToExcel(ref BoxSummaryGrid);

            this.Cursor = Cursors.Default;
        }

        private void RadioBoxSummaryNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBoxSummaryNormal.Checked)
            {
                BoxSummaryGrid.LoadLayout("./layouts/boxsummary-normal.xml");

                dataSet = new DataSet();
                dataSet = dataSetOriginal.Copy();

                DataLoad();


                BoxSummaryGrid.Splits[0, 0].DisplayColumns["ReceiveFails"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["DeliveryFails"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["BorrowRecalls"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["LoanRecalls"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["Borrows"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["Loans"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["ExDeficit"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["OnPledge"].Visible = false;
                BoxSummaryGrid.Splits[0, 0].DisplayColumns["PledgeUndo"].Visible = false;
            }
        }

        private void Radio204_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio204.Checked)
            {
                BoxSummaryGrid.LoadLayout("./layouts/boxsummary-204.xml");

                dataSet = new DataSet();
                dataSet = dataSetOriginal.Copy();

                DataLoad204();
            }
        }
    }
}
