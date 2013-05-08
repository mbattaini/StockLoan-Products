namespace CentralClient
{
  partial class AdminHolidaysForm
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
        C1.C1Schedule.Printing.PrintStyle printStyle1 = new C1.C1Schedule.Printing.PrintStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminHolidaysForm));
        C1.C1Schedule.Printing.PrintStyle printStyle2 = new C1.C1Schedule.Printing.PrintStyle();
        C1.C1Schedule.Printing.PrintStyle printStyle3 = new C1.C1Schedule.Printing.PrintStyle();
        C1.C1Schedule.Printing.PrintStyle printStyle4 = new C1.C1Schedule.Printing.PrintStyle();
        C1.C1Schedule.Printing.PrintStyle printStyle5 = new C1.C1Schedule.Printing.PrintStyle();
        this.HolidayCalandar = new C1.Win.C1Schedule.C1Schedule();
        this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
        this.AddCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.AddCommand = new C1.Win.C1Command.C1CommandMenu();
        this.AddHolidayCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.AddHolidayCommand = new C1.Win.C1Command.C1Command();
        this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
        this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
        ((System.ComponentModel.ISupportInitialize)(this.HolidayCalandar)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
        this.SuspendLayout();
        // 
        // HolidayCalandar
        // 
        this.HolidayCalandar.AllowDrop = true;
        this.HolidayCalandar.BorderStyle = System.Windows.Forms.BorderStyle.None;
        // 
        // 
        // 
        this.HolidayCalandar.CalendarInfo.CultureInfo = new System.Globalization.CultureInfo("en-US");
        this.HolidayCalandar.CalendarInfo.DateFormatString = "M/d/yyyy";
        this.HolidayCalandar.CalendarInfo.EndDayTime = System.TimeSpan.Parse("19:00:00");
        this.HolidayCalandar.CalendarInfo.FirstDate = new System.DateTime(2007, 3, 18, 0, 0, 0, 0);
        this.HolidayCalandar.CalendarInfo.Holidays.Add(new System.DateTime(2008, 3, 18, 0, 0, 0, 0));
        this.HolidayCalandar.CalendarInfo.LastDate = new System.DateTime(2025, 3, 17, 0, 0, 0, 0);
        this.HolidayCalandar.CalendarInfo.StartDayTime = System.TimeSpan.Parse("07:00:00");
        this.HolidayCalandar.CalendarInfo.TimeScale = System.TimeSpan.Parse("00:30:00");
        this.HolidayCalandar.CalendarInfo.WeekStart = System.DayOfWeek.Sunday;
        this.HolidayCalandar.CalendarInfo.WorkDays.AddRange(new System.DayOfWeek[] {
            System.DayOfWeek.Monday,
            System.DayOfWeek.Tuesday,
            System.DayOfWeek.Wednesday,
            System.DayOfWeek.Thursday,
            System.DayOfWeek.Friday});
        this.HolidayCalandar.Dock = System.Windows.Forms.DockStyle.Fill;
        this.HolidayCalandar.EditOptions = C1.Win.C1Schedule.EditOptions.None;
        this.HolidayCalandar.FirstVisibleTime = System.TimeSpan.Parse("00:00:00");
        this.HolidayCalandar.Location = new System.Drawing.Point(0, 0);
        this.HolidayCalandar.Name = "HolidayCalandar";
        this.HolidayCalandar.Padding = new System.Windows.Forms.Padding(3);
        printStyle1.Description = "Daily Style";
        printStyle1.PreviewImage = ((System.Drawing.Image)(resources.GetObject("printStyle1.PreviewImage")));
        printStyle1.StyleName = "Daily";
        printStyle1.StyleSource = "day.c1d";
        printStyle2.Description = "Weekly Style";
        printStyle2.PreviewImage = ((System.Drawing.Image)(resources.GetObject("printStyle2.PreviewImage")));
        printStyle2.StyleName = "Week";
        printStyle2.StyleSource = "week.c1d";
        printStyle3.Description = "Monthly Style";
        printStyle3.PreviewImage = ((System.Drawing.Image)(resources.GetObject("printStyle3.PreviewImage")));
        printStyle3.StyleName = "Month";
        printStyle3.StyleSource = "month.c1d";
        printStyle4.Description = "Details Style";
        printStyle4.PreviewImage = ((System.Drawing.Image)(resources.GetObject("printStyle4.PreviewImage")));
        printStyle4.StyleName = "Details";
        printStyle4.StyleSource = "details.c1d";
        printStyle5.Context = C1.C1Schedule.Printing.PrintContextType.Appointment;
        printStyle5.Description = "Memo Style";
        printStyle5.PreviewImage = ((System.Drawing.Image)(resources.GetObject("printStyle5.PreviewImage")));
        printStyle5.StyleName = "Memo";
        printStyle5.StyleSource = "memo.c1d";
        this.HolidayCalandar.PrintInfo.PrintStyles.AddRange(new C1.C1Schedule.Printing.PrintStyle[] {
            printStyle1,
            printStyle2,
            printStyle3,
            printStyle4,
            printStyle5});
        this.HolidayCalandar.ShowWorkTimeOnly = true;
        this.HolidayCalandar.Size = new System.Drawing.Size(1319, 758);
        this.HolidayCalandar.TabIndex = 2;
        this.HolidayCalandar.ViewType = C1.Win.C1Schedule.ScheduleViewEnum.MonthView;
        this.HolidayCalandar.VisualStyle = C1.Win.C1Schedule.UI.VisualStyle.Office2007Blue;
        // 
        // MainContextMenu
        // 
        this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.AddCommandLink});
        this.MainContextMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.MainContextMenu.Name = "MainContextMenu";
        this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
        // 
        // AddCommandLink
        // 
        this.AddCommandLink.Command = this.AddCommand;
        // 
        // AddCommand
        // 
        this.AddCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.AddHolidayCommandLink});
        this.AddCommand.Name = "AddCommand";
        this.AddCommand.Text = "Add";
        this.AddCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2003Silver;
        this.AddCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2003Silver;
        // 
        // AddHolidayCommandLink
        // 
        this.AddHolidayCommandLink.Command = this.AddHolidayCommand;
        // 
        // AddHolidayCommand
        // 
        this.AddHolidayCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("AddHolidayCommand.Icon")));
        this.AddHolidayCommand.Name = "AddHolidayCommand";
        this.AddHolidayCommand.Text = "Holiday";
        this.AddHolidayCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.AddHolidayCommand_Click);
        // 
        // MainCommandHolder
        // 
        this.MainCommandHolder.Commands.Add(this.MainContextMenu);
        this.MainCommandHolder.Commands.Add(this.AddCommand);
        this.MainCommandHolder.Commands.Add(this.AddHolidayCommand);
        this.MainCommandHolder.Owner = this;
        // 
        // c1StatusBar1
        // 
        this.c1StatusBar1.Name = "c1StatusBar1";
        // 
        // AdminHolidaysForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.MainCommandHolder.SetC1ContextMenu(this, this.MainContextMenu);
        this.ClientSize = new System.Drawing.Size(1319, 781);
        this.Controls.Add(this.HolidayCalandar);
        this.Controls.Add(this.c1StatusBar1);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "AdminHolidaysForm";
        this.Text = "Admin - Holidays";
        this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminHolidaysForm_FormClosed);
        this.Load += new System.EventHandler(this.AdminHolidaysForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.HolidayCalandar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private C1.Win.C1Schedule.C1Schedule HolidayCalandar;
	  private C1.Win.C1Command.C1ContextMenu MainContextMenu;
	  private C1.Win.C1Command.C1CommandLink AddCommandLink;
	  private C1.Win.C1Command.C1CommandMenu AddCommand;
	  private C1.Win.C1Command.C1CommandLink AddHolidayCommandLink;
	  private C1.Win.C1Command.C1Command AddHolidayCommand;
	  private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
      private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
  }
}