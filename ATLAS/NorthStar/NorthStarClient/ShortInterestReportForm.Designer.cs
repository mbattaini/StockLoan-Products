namespace NorthStarClient
{
    partial class ShortInterestReportingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortInterestReportingForm));
          this.ShortInterestGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
          this.MpidTextBox = new C1.Win.C1Input.C1TextBox();
          this.MpidLabel = new C1.Win.C1Input.C1Label();
          this.CusipLabel = new C1.Win.C1Input.C1Label();
          this.CusipTextBox = new C1.Win.C1Input.C1TextBox();
          this.QuantityLessThanLabel = new C1.Win.C1Input.C1Label();
          this.QuantityLessThanTextBox = new C1.Win.C1Input.C1TextBox();
          this.QuantityGreaterThanLabel = new C1.Win.C1Input.C1Label();
          this.QuantityGreaterThanTextBox = new C1.Win.C1Input.C1TextBox();
          this.ShortInterestMonthEndGreaterThanLabel = new C1.Win.C1Input.C1Label();
          this.ShortInterestMonthEndGreaterThanTextBox = new C1.Win.C1Input.C1TextBox();
          this.PriceLessThanLabel = new C1.Win.C1Input.C1Label();
          this.PriceLessThanTextBox = new C1.Win.C1Input.C1TextBox();
          this.FindButton = new C1.Win.C1Input.C1Button();
          this.StatusRibbonLabel = new C1.Win.C1Ribbon.RibbonLabel();
          this.ResultStatusBar = new C1.Win.C1Ribbon.C1StatusBar();
          this.ShortInterestMidMonthGreaterThanLabel = new C1.Win.C1Input.C1Label();
          this.ShortInterestMidMonthGreaterThanTextBox = new C1.Win.C1Input.C1TextBox();
          this.ShortInterestDataStatusLabel = new C1.Win.C1Input.C1Label();
          this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
          this.SendToCommandLink = new C1.Win.C1Command.C1CommandLink();
          this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
          this.SendToClipboardCommandLink = new C1.Win.C1Command.C1CommandLink();
          this.SendToClipboardCommand = new C1.Win.C1Command.C1Command();
          this.SendToExcelCommandLink = new C1.Win.C1Command.C1CommandLink();
          this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
          this.SendToMailCommandLink = new C1.Win.C1Command.C1CommandLink();
          this.SendToMailCommand = new C1.Win.C1Command.C1Command();
          this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestGrid)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.MpidTextBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.MpidLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.CusipLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.CusipTextBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityLessThanLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityLessThanTextBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityGreaterThanLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityGreaterThanTextBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMonthEndGreaterThanLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMonthEndGreaterThanTextBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.PriceLessThanLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.PriceLessThanTextBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ResultStatusBar)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMidMonthGreaterThanLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMidMonthGreaterThanTextBox)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestDataStatusLabel)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
          this.SuspendLayout();
          // 
          // ShortInterestGrid
          // 
          this.ShortInterestGrid.AllowColMove = false;
          this.ShortInterestGrid.AllowColSelect = false;
          this.ShortInterestGrid.AllowDrag = true;
          this.ShortInterestGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
          this.ShortInterestGrid.AllowUpdate = false;
          this.ShortInterestGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.ShortInterestGrid.CaptionHeight = 17;
          this.ShortInterestGrid.CellTips = C1.Win.C1TrueDBGrid.CellTipEnum.Floating;
          this.ShortInterestGrid.CellTipsWidth = 100;
          this.ShortInterestGrid.EmptyRows = true;
          this.ShortInterestGrid.ExtendRightColumn = true;
          this.ShortInterestGrid.FilterBar = true;
          this.ShortInterestGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ShortInterestGrid.GroupByCaption = "Drag a column header here to group by that column";
          this.ShortInterestGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ShortInterestGrid.Images"))));
          this.ShortInterestGrid.Location = new System.Drawing.Point(-1, 56);
          this.ShortInterestGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
          this.ShortInterestGrid.Name = "ShortInterestGrid";
          this.ShortInterestGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
          this.ShortInterestGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
          this.ShortInterestGrid.PreviewInfo.ZoomFactor = 75;
          this.ShortInterestGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ShortInterestGrid.PrintInfo.PageSettings")));
          this.ShortInterestGrid.RowDivider.Color = System.Drawing.Color.WhiteSmoke;
          this.ShortInterestGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
          this.ShortInterestGrid.RowHeight = 15;
          this.ShortInterestGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
          this.ShortInterestGrid.Size = new System.Drawing.Size(1525, 639);
          this.ShortInterestGrid.TabAcrossSplits = true;
          this.ShortInterestGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
          this.ShortInterestGrid.TabIndex = 15;
          this.ShortInterestGrid.TabStop = false;
          this.ShortInterestGrid.WrapCellPointer = true;
          this.ShortInterestGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ShortInterestGrid_FormatText);
          this.ShortInterestGrid.FetchCellTips += new C1.Win.C1TrueDBGrid.FetchCellTipsEventHandler(this.ShortInterestGrid_FetchCellTips);
          this.ShortInterestGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ShortInterestGrid_Paint);
          this.ShortInterestGrid.PropBag = resources.GetString("ShortInterestGrid.PropBag");
          // 
          // MpidTextBox
          // 
          this.MpidTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.MpidTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.MpidTextBox.Location = new System.Drawing.Point(110, 7);
          this.MpidTextBox.MaxLength = 4;
          this.MpidTextBox.Name = "MpidTextBox";
          this.MpidTextBox.Size = new System.Drawing.Size(90, 19);
          this.MpidTextBox.TabIndex = 1;
          this.MpidTextBox.Tag = null;
          this.MpidTextBox.TextDetached = true;
          // 
          // MpidLabel
          // 
          this.MpidLabel.AutoSize = true;
          this.MpidLabel.BackColor = System.Drawing.Color.Transparent;
          this.MpidLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.MpidLabel.ForeColor = System.Drawing.Color.Black;
          this.MpidLabel.Location = new System.Drawing.Point(20, 10);
          this.MpidLabel.Name = "MpidLabel";
          this.MpidLabel.Size = new System.Drawing.Size(86, 13);
          this.MpidLabel.TabIndex = 0;
          this.MpidLabel.Tag = null;
          this.MpidLabel.Text = "MPID (Code):";
          this.MpidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          this.MpidLabel.TextDetached = true;
          this.MpidLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // CusipLabel
          // 
          this.CusipLabel.AutoSize = true;
          this.CusipLabel.BackColor = System.Drawing.Color.Transparent;
          this.CusipLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.CusipLabel.ForeColor = System.Drawing.Color.Black;
          this.CusipLabel.Location = new System.Drawing.Point(9, 31);
          this.CusipLabel.Name = "CusipLabel";
          this.CusipLabel.Size = new System.Drawing.Size(97, 13);
          this.CusipLabel.TabIndex = 2;
          this.CusipLabel.Tag = null;
          this.CusipLabel.Text = "CUSIP/Symbol:";
          this.CusipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          this.CusipLabel.TextDetached = true;
          this.CusipLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // CusipTextBox
          // 
          this.CusipTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.CusipTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.CusipTextBox.Location = new System.Drawing.Point(110, 28);
          this.CusipTextBox.MaxLength = 9;
          this.CusipTextBox.Name = "CusipTextBox";
          this.CusipTextBox.Size = new System.Drawing.Size(90, 19);
          this.CusipTextBox.TabIndex = 3;
          this.CusipTextBox.Tag = null;
          this.CusipTextBox.TextDetached = true;
          // 
          // QuantityLessThanLabel
          // 
          this.QuantityLessThanLabel.AutoSize = true;
          this.QuantityLessThanLabel.BackColor = System.Drawing.Color.Transparent;
          this.QuantityLessThanLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.QuantityLessThanLabel.ForeColor = System.Drawing.Color.Black;
          this.QuantityLessThanLabel.Location = new System.Drawing.Point(245, 31);
          this.QuantityLessThanLabel.Name = "QuantityLessThanLabel";
          this.QuantityLessThanLabel.Size = new System.Drawing.Size(121, 13);
          this.QuantityLessThanLabel.TabIndex = 6;
          this.QuantityLessThanLabel.Tag = null;
          this.QuantityLessThanLabel.Text = "Quantity Less Than:";
          this.QuantityLessThanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          this.QuantityLessThanLabel.TextDetached = true;
          this.QuantityLessThanLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // QuantityLessThanTextBox
          // 
          this.QuantityLessThanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.QuantityLessThanTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.QuantityLessThanTextBox.Location = new System.Drawing.Point(370, 28);
          this.QuantityLessThanTextBox.Name = "QuantityLessThanTextBox";
          this.QuantityLessThanTextBox.Size = new System.Drawing.Size(90, 19);
          this.QuantityLessThanTextBox.TabIndex = 7;
          this.QuantityLessThanTextBox.Tag = null;
          this.QuantityLessThanTextBox.TextDetached = true;
          // 
          // QuantityGreaterThanLabel
          // 
          this.QuantityGreaterThanLabel.AutoSize = true;
          this.QuantityGreaterThanLabel.BackColor = System.Drawing.Color.Transparent;
          this.QuantityGreaterThanLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.QuantityGreaterThanLabel.ForeColor = System.Drawing.Color.Black;
          this.QuantityGreaterThanLabel.Location = new System.Drawing.Point(226, 10);
          this.QuantityGreaterThanLabel.Name = "QuantityGreaterThanLabel";
          this.QuantityGreaterThanLabel.Size = new System.Drawing.Size(140, 13);
          this.QuantityGreaterThanLabel.TabIndex = 4;
          this.QuantityGreaterThanLabel.Tag = null;
          this.QuantityGreaterThanLabel.Text = "Quantity Greater Than:";
          this.QuantityGreaterThanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          this.QuantityGreaterThanLabel.TextDetached = true;
          this.QuantityGreaterThanLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // QuantityGreaterThanTextBox
          // 
          this.QuantityGreaterThanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.QuantityGreaterThanTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.QuantityGreaterThanTextBox.Location = new System.Drawing.Point(370, 7);
          this.QuantityGreaterThanTextBox.Name = "QuantityGreaterThanTextBox";
          this.QuantityGreaterThanTextBox.Size = new System.Drawing.Size(90, 19);
          this.QuantityGreaterThanTextBox.TabIndex = 5;
          this.QuantityGreaterThanTextBox.Tag = null;
          this.QuantityGreaterThanTextBox.TextDetached = true;
          // 
          // ShortInterestMonthEndGreaterThanLabel
          // 
          this.ShortInterestMonthEndGreaterThanLabel.AutoSize = true;
          this.ShortInterestMonthEndGreaterThanLabel.BackColor = System.Drawing.Color.Transparent;
          this.ShortInterestMonthEndGreaterThanLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ShortInterestMonthEndGreaterThanLabel.ForeColor = System.Drawing.Color.Black;
          this.ShortInterestMonthEndGreaterThanLabel.Location = new System.Drawing.Point(489, 31);
          this.ShortInterestMonthEndGreaterThanLabel.Name = "ShortInterestMonthEndGreaterThanLabel";
          this.ShortInterestMonthEndGreaterThanLabel.Size = new System.Drawing.Size(261, 13);
          this.ShortInterestMonthEndGreaterThanLabel.TabIndex = 10;
          this.ShortInterestMonthEndGreaterThanLabel.Tag = null;
          this.ShortInterestMonthEndGreaterThanLabel.Text = "Short Interest MonthEnd Greater Than(MIL):";
          this.ShortInterestMonthEndGreaterThanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          this.ShortInterestMonthEndGreaterThanLabel.TextDetached = true;
          this.ShortInterestMonthEndGreaterThanLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // ShortInterestMonthEndGreaterThanTextBox
          // 
          this.ShortInterestMonthEndGreaterThanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.ShortInterestMonthEndGreaterThanTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ShortInterestMonthEndGreaterThanTextBox.Location = new System.Drawing.Point(754, 28);
          this.ShortInterestMonthEndGreaterThanTextBox.Name = "ShortInterestMonthEndGreaterThanTextBox";
          this.ShortInterestMonthEndGreaterThanTextBox.Size = new System.Drawing.Size(90, 19);
          this.ShortInterestMonthEndGreaterThanTextBox.TabIndex = 11;
          this.ShortInterestMonthEndGreaterThanTextBox.Tag = null;
          this.ShortInterestMonthEndGreaterThanTextBox.TextDetached = true;
          // 
          // PriceLessThanLabel
          // 
          this.PriceLessThanLabel.AutoSize = true;
          this.PriceLessThanLabel.BackColor = System.Drawing.Color.Transparent;
          this.PriceLessThanLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.PriceLessThanLabel.ForeColor = System.Drawing.Color.Black;
          this.PriceLessThanLabel.Location = new System.Drawing.Point(870, 10);
          this.PriceLessThanLabel.Name = "PriceLessThanLabel";
          this.PriceLessThanLabel.Size = new System.Drawing.Size(101, 13);
          this.PriceLessThanLabel.TabIndex = 12;
          this.PriceLessThanLabel.Tag = null;
          this.PriceLessThanLabel.Text = "Price Less Than:";
          this.PriceLessThanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          this.PriceLessThanLabel.TextDetached = true;
          this.PriceLessThanLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // PriceLessThanTextBox
          // 
          this.PriceLessThanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.PriceLessThanTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.PriceLessThanTextBox.Location = new System.Drawing.Point(975, 7);
          this.PriceLessThanTextBox.Name = "PriceLessThanTextBox";
          this.PriceLessThanTextBox.Size = new System.Drawing.Size(90, 19);
          this.PriceLessThanTextBox.TabIndex = 13;
          this.PriceLessThanTextBox.Tag = null;
          this.PriceLessThanTextBox.TextDetached = true;
          // 
          // FindButton
          // 
          this.FindButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.FindButton.Image = ((System.Drawing.Image)(resources.GetObject("FindButton.Image")));
          this.FindButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
          this.FindButton.Location = new System.Drawing.Point(1099, 5);
          this.FindButton.Name = "FindButton";
          this.FindButton.Size = new System.Drawing.Size(75, 23);
          this.FindButton.TabIndex = 14;
          this.FindButton.Text = "Find";
          this.FindButton.UseVisualStyleBackColor = true;
          this.FindButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
          // 
          // StatusRibbonLabel
          // 
          this.StatusRibbonLabel.ID = "StatusRibbonLabel";
          // 
          // ResultStatusBar
          // 
          this.ResultStatusBar.LeftPaneItems.Add(this.StatusRibbonLabel);
          this.ResultStatusBar.Name = "ResultStatusBar";
          // 
          // ShortInterestMidMonthGreaterThanLabel
          // 
          this.ShortInterestMidMonthGreaterThanLabel.AutoSize = true;
          this.ShortInterestMidMonthGreaterThanLabel.BackColor = System.Drawing.Color.Transparent;
          this.ShortInterestMidMonthGreaterThanLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ShortInterestMidMonthGreaterThanLabel.ForeColor = System.Drawing.Color.Black;
          this.ShortInterestMidMonthGreaterThanLabel.Location = new System.Drawing.Point(491, 10);
          this.ShortInterestMidMonthGreaterThanLabel.Name = "ShortInterestMidMonthGreaterThanLabel";
          this.ShortInterestMidMonthGreaterThanLabel.Size = new System.Drawing.Size(259, 13);
          this.ShortInterestMidMonthGreaterThanLabel.TabIndex = 8;
          this.ShortInterestMidMonthGreaterThanLabel.Tag = null;
          this.ShortInterestMidMonthGreaterThanLabel.Text = "Short Interest MidMonth Greater Than(MIL):";
          this.ShortInterestMidMonthGreaterThanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          this.ShortInterestMidMonthGreaterThanLabel.TextDetached = true;
          this.ShortInterestMidMonthGreaterThanLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // ShortInterestMidMonthGreaterThanTextBox
          // 
          this.ShortInterestMidMonthGreaterThanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.ShortInterestMidMonthGreaterThanTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ShortInterestMidMonthGreaterThanTextBox.Location = new System.Drawing.Point(754, 7);
          this.ShortInterestMidMonthGreaterThanTextBox.Name = "ShortInterestMidMonthGreaterThanTextBox";
          this.ShortInterestMidMonthGreaterThanTextBox.Size = new System.Drawing.Size(90, 19);
          this.ShortInterestMidMonthGreaterThanTextBox.TabIndex = 9;
          this.ShortInterestMidMonthGreaterThanTextBox.Tag = null;
          this.ShortInterestMidMonthGreaterThanTextBox.TextDetached = true;
          // 
          // ShortInterestDataStatusLabel
          // 
          this.ShortInterestDataStatusLabel.AutoSize = true;
          this.ShortInterestDataStatusLabel.BackColor = System.Drawing.Color.Transparent;
          this.ShortInterestDataStatusLabel.ForeColor = System.Drawing.Color.Black;
          this.ShortInterestDataStatusLabel.Location = new System.Drawing.Point(870, 31);
          this.ShortInterestDataStatusLabel.Name = "ShortInterestDataStatusLabel";
          this.ShortInterestDataStatusLabel.Size = new System.Drawing.Size(0, 13);
          this.ShortInterestDataStatusLabel.TabIndex = 17;
          this.ShortInterestDataStatusLabel.Tag = null;
          this.ShortInterestDataStatusLabel.TextDetached = true;
          this.ShortInterestDataStatusLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
          // 
          // MainContextMenu
          // 
          this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToCommandLink});
          this.MainContextMenu.Name = "MainContextMenu";
          this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
          // 
          // SendToCommandLink
          // 
          this.SendToCommandLink.Command = this.SendToCommand;
          // 
          // SendToCommand
          // 
          this.SendToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToClipboardCommandLink,
            this.SendToExcelCommandLink,
            this.SendToMailCommandLink});
          this.SendToCommand.Name = "SendToCommand";
          this.SendToCommand.Text = "Send To";
          this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
          // 
          // SendToClipboardCommandLink
          // 
          this.SendToClipboardCommandLink.Command = this.SendToClipboardCommand;
          // 
          // SendToClipboardCommand
          // 
          this.SendToClipboardCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToClipboardCommand.Icon")));
          this.SendToClipboardCommand.Name = "SendToClipboardCommand";
          this.SendToClipboardCommand.Text = "Clipboard";
          this.SendToClipboardCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToClipboardCommand_Click);
          // 
          // SendToExcelCommandLink
          // 
          this.SendToExcelCommandLink.Command = this.SendToExcelCommand;
          this.SendToExcelCommandLink.SortOrder = 1;
          // 
          // SendToExcelCommand
          // 
          this.SendToExcelCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToExcelCommand.Icon")));
          this.SendToExcelCommand.Name = "SendToExcelCommand";
          this.SendToExcelCommand.Text = "Excel";
          this.SendToExcelCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToExcelCommand_Click);
          // 
          // SendToMailCommandLink
          // 
          this.SendToMailCommandLink.Command = this.SendToMailCommand;
          this.SendToMailCommandLink.SortOrder = 2;
          // 
          // SendToMailCommand
          // 
          this.SendToMailCommand.Enabled = false;
          this.SendToMailCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToMailCommand.Icon")));
          this.SendToMailCommand.Name = "SendToMailCommand";
          this.SendToMailCommand.Text = "Mail";
          this.SendToMailCommand.Visible = false;
          this.SendToMailCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToMailCommand_Click);
          // 
          // MainCommandHolder
          // 
          this.MainCommandHolder.Commands.Add(this.MainContextMenu);
          this.MainCommandHolder.Commands.Add(this.SendToCommand);
          this.MainCommandHolder.Commands.Add(this.SendToExcelCommand);
          this.MainCommandHolder.Commands.Add(this.SendToMailCommand);
          this.MainCommandHolder.Commands.Add(this.SendToClipboardCommand);
          this.MainCommandHolder.Owner = this;
          // 
          // ShortInterestReportingForm
          // 
          this.AcceptButton = this.FindButton;
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
          this.MainCommandHolder.SetC1ContextMenu(this, this.MainContextMenu);
          this.ClientSize = new System.Drawing.Size(1522, 716);
          this.Controls.Add(this.ShortInterestDataStatusLabel);
          this.Controls.Add(this.ShortInterestMidMonthGreaterThanLabel);
          this.Controls.Add(this.ShortInterestMidMonthGreaterThanTextBox);
          this.Controls.Add(this.ShortInterestMonthEndGreaterThanLabel);
          this.Controls.Add(this.FindButton);
          this.Controls.Add(this.ShortInterestMonthEndGreaterThanTextBox);
          this.Controls.Add(this.PriceLessThanLabel);
          this.Controls.Add(this.PriceLessThanTextBox);
          this.Controls.Add(this.QuantityLessThanLabel);
          this.Controls.Add(this.QuantityLessThanTextBox);
          this.Controls.Add(this.QuantityGreaterThanLabel);
          this.Controls.Add(this.QuantityGreaterThanTextBox);
          this.Controls.Add(this.CusipLabel);
          this.Controls.Add(this.CusipTextBox);
          this.Controls.Add(this.MpidLabel);
          this.Controls.Add(this.MpidTextBox);
          this.Controls.Add(this.ResultStatusBar);
          this.Controls.Add(this.ShortInterestGrid);
          this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Name = "ShortInterestReportingForm";
          this.Text = "Short Interest Reporting";
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShortInterestReportingForm_FormClosed);
          this.Load += new System.EventHandler(this.ShortInterestReportingForm_Load);
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestGrid)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.MpidTextBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.MpidLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.CusipLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.CusipTextBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityLessThanLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityLessThanTextBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityGreaterThanLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.QuantityGreaterThanTextBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMonthEndGreaterThanLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMonthEndGreaterThanTextBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.PriceLessThanLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.PriceLessThanTextBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ResultStatusBar)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMidMonthGreaterThanLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestMidMonthGreaterThanTextBox)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.ShortInterestDataStatusLabel)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ShortInterestGrid;
        private C1.Win.C1Input.C1TextBox MpidTextBox;
        private C1.Win.C1Input.C1Label MpidLabel;
        private C1.Win.C1Input.C1Label CusipLabel;
        private C1.Win.C1Input.C1TextBox CusipTextBox;
        private C1.Win.C1Input.C1Label QuantityLessThanLabel;
        private C1.Win.C1Input.C1TextBox QuantityLessThanTextBox;
        private C1.Win.C1Input.C1Label QuantityGreaterThanLabel;
        private C1.Win.C1Input.C1TextBox QuantityGreaterThanTextBox;
        private C1.Win.C1Input.C1Label ShortInterestMonthEndGreaterThanLabel;
        private C1.Win.C1Input.C1TextBox ShortInterestMonthEndGreaterThanTextBox;
        private C1.Win.C1Input.C1Label PriceLessThanLabel;
        private C1.Win.C1Input.C1TextBox PriceLessThanTextBox;
      private C1.Win.C1Input.C1Button FindButton;
        private C1.Win.C1Ribbon.RibbonLabel StatusRibbonLabel;
      private C1.Win.C1Ribbon.C1StatusBar ResultStatusBar;
        private C1.Win.C1Input.C1Label ShortInterestMidMonthGreaterThanLabel;
        private C1.Win.C1Input.C1TextBox ShortInterestMidMonthGreaterThanTextBox;
        private C1.Win.C1Input.C1Label ShortInterestDataStatusLabel;
      private C1.Win.C1Command.C1ContextMenu MainContextMenu;
      private C1.Win.C1Command.C1CommandLink SendToCommandLink;
      private C1.Win.C1Command.C1CommandMenu SendToCommand;
      private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
      private C1.Win.C1Command.C1Command SendToExcelCommand;
      private C1.Win.C1Command.C1CommandLink SendToMailCommandLink;
      private C1.Win.C1Command.C1Command SendToMailCommand;
      private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
      private C1.Win.C1Command.C1CommandLink SendToClipboardCommandLink;
      private C1.Win.C1Command.C1Command SendToClipboardCommand;
    }
}