namespace PreborrowClient
{
  partial class PreborrowMainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreborrowMainForm));
      this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
      this.PreBorrowInputFormCommand = new C1.Win.C1Command.C1Command();
      this.PreBorrowSummaryFormCommand = new C1.Win.C1Command.C1Command();
      this.PreBorrowGroupCodeMarksFormCommand = new C1.Win.C1Command.C1Command();
      this.c1CommandControl1 = new C1.Win.C1Command.C1CommandControl();
      this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
      this.PreBorrowInputFormCommandLink = new C1.Win.C1Command.C1CommandLink();
      this.PreBorrowSummaryFormCommandLink = new C1.Win.C1Command.C1CommandLink();
      this.PreBorrowGroupCodeMarksFormCommandLink = new C1.Win.C1Command.C1CommandLink();
      this.PreBorrowContactsFormCommandLink = new C1.Win.C1Command.C1CommandLink();
      this.PreBorrowContactsFormCommand = new C1.Win.C1Command.C1Command();
      ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
      this.SuspendLayout();
      // 
      // c1CommandHolder1
      // 
      this.c1CommandHolder1.Commands.Add(this.PreBorrowInputFormCommand);
      this.c1CommandHolder1.Commands.Add(this.PreBorrowSummaryFormCommand);
      this.c1CommandHolder1.Commands.Add(this.PreBorrowGroupCodeMarksFormCommand);
      this.c1CommandHolder1.Commands.Add(this.c1CommandControl1);
      this.c1CommandHolder1.Commands.Add(this.PreBorrowContactsFormCommand);
      this.c1CommandHolder1.Owner = this;
      // 
      // PreBorrowInputFormCommand
      // 
      this.PreBorrowInputFormCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("PreBorrowInputFormCommand.Icon")));
      this.PreBorrowInputFormCommand.Name = "PreBorrowInputFormCommand";
      this.PreBorrowInputFormCommand.Text = "New Command";
      this.PreBorrowInputFormCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PreBorrowInputFormCommand_Click);
      // 
      // PreBorrowSummaryFormCommand
      // 
      this.PreBorrowSummaryFormCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("PreBorrowSummaryFormCommand.Icon")));
      this.PreBorrowSummaryFormCommand.Name = "PreBorrowSummaryFormCommand";
      this.PreBorrowSummaryFormCommand.Text = "PreBorrow - Summary";
      this.PreBorrowSummaryFormCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PreBorrowSummaryFormCommand_Click);
      // 
      // PreBorrowGroupCodeMarksFormCommand
      // 
      this.PreBorrowGroupCodeMarksFormCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("PreBorrowGroupCodeMarksFormCommand.Icon")));
      this.PreBorrowGroupCodeMarksFormCommand.Name = "PreBorrowGroupCodeMarksFormCommand";
      this.PreBorrowGroupCodeMarksFormCommand.Text = "GroupCode Marks";
      this.PreBorrowGroupCodeMarksFormCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PreBorrowGroupCodeMarksFormCommand_Click);
      // 
      // c1CommandControl1
      // 
      this.c1CommandControl1.Name = "c1CommandControl1";
      // 
      // c1ToolBar1
      // 
      this.c1ToolBar1.AccessibleName = "Tool Bar";
      this.c1ToolBar1.AutoSize = false;
      this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
      this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.PreBorrowInputFormCommandLink,
            this.PreBorrowSummaryFormCommandLink,
            this.PreBorrowGroupCodeMarksFormCommandLink,
            this.PreBorrowContactsFormCommandLink});
      this.c1ToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
      this.c1ToolBar1.Location = new System.Drawing.Point(0, 0);
      this.c1ToolBar1.Movable = false;
      this.c1ToolBar1.Name = "c1ToolBar1";
      this.c1ToolBar1.Size = new System.Drawing.Size(1408, 26);
      this.c1ToolBar1.Text = "c1ToolBar1";
      this.c1ToolBar1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
      // 
      // PreBorrowInputFormCommandLink
      // 
      this.PreBorrowInputFormCommandLink.Command = this.PreBorrowInputFormCommand;
      // 
      // PreBorrowSummaryFormCommandLink
      // 
      this.PreBorrowSummaryFormCommandLink.Command = this.PreBorrowSummaryFormCommand;
      this.PreBorrowSummaryFormCommandLink.SortOrder = 1;
      // 
      // PreBorrowGroupCodeMarksFormCommandLink
      // 
      this.PreBorrowGroupCodeMarksFormCommandLink.Command = this.PreBorrowGroupCodeMarksFormCommand;
      this.PreBorrowGroupCodeMarksFormCommandLink.SortOrder = 2;
      // 
      // PreBorrowContactsFormCommandLink
      // 
      this.PreBorrowContactsFormCommandLink.Command = this.PreBorrowContactsFormCommand;
      this.PreBorrowContactsFormCommandLink.SortOrder = 3;
      // 
      // PreBorrowContactsFormCommand
      // 
      this.PreBorrowContactsFormCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("PreBorrowContactsFormCommand.Icon")));
      this.PreBorrowContactsFormCommand.Name = "PreBorrowContactsFormCommand";
      this.PreBorrowContactsFormCommand.Text = "PresBorrow Contacts";
      this.PreBorrowContactsFormCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PreBorrowContactsFormCommand_Click);
      // 
      // PreborrowMainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1408, 951);
      this.Controls.Add(this.c1ToolBar1);
      this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.Name = "PreborrowMainForm";
      this.Text = "Pre Borrows";
      this.Load += new System.EventHandler(this.PreborrowMainForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
    private C1.Win.C1Command.C1Command PreBorrowInputFormCommand;
    private C1.Win.C1Command.C1Command PreBorrowSummaryFormCommand;
    private C1.Win.C1Command.C1Command PreBorrowGroupCodeMarksFormCommand;
    private C1.Win.C1Command.C1CommandControl c1CommandControl1;
    private C1.Win.C1Command.C1ToolBar c1ToolBar1;
    private C1.Win.C1Command.C1CommandLink PreBorrowInputFormCommandLink;
    private C1.Win.C1Command.C1CommandLink PreBorrowSummaryFormCommandLink;
    private C1.Win.C1Command.C1CommandLink PreBorrowGroupCodeMarksFormCommandLink;
    private C1.Win.C1Command.C1Command PreBorrowContactsFormCommand;
    private C1.Win.C1Command.C1CommandLink PreBorrowContactsFormCommandLink;

  }
}

