using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace CentralClient
{
  public partial class AdminHolidaysForm : C1.Win.C1Ribbon.C1RibbonForm
  {
    private MainForm mainForm = null;

    public AdminHolidaysForm(MainForm mainForm)
    {
      InitializeComponent();

      this.mainForm = mainForm;
    }

	  private void AdminHolidaysForm_Load(object sender, EventArgs e)
	  {
		  int height = this.Height;
		  int width = this.Width;

		  this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
		  this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
		  this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
		  this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

		  HolidaysLoad();
	  }

	  private void HolidaysLoad()
	  {
		  DataSet dsHolidays = new DataSet();

		  try
		  {
			  dsHolidays = mainForm.AdminAgent.HolidaysGet(mainForm.UtcOffset);

			  foreach (DataRow drHoliday in dsHolidays.Tables["Holidays"].Rows)
			  {
				  C1.C1Schedule.Appointment appItem = new C1.C1Schedule.Appointment();
				  
				  appItem.AllDayEvent = true;				  
				  appItem.Start = DateTime.Parse(drHoliday["HolidayDate"].ToString());
				  appItem.Subject =  "Holiday For : " + drHoliday["CountryCode"].ToString();
				  appItem.Body = drHoliday["Description"].ToString();				  

				  HolidayCalandar.DataStorage.AppointmentStorage.Appointments.Add(appItem);
			  }
		  }
		  catch (Exception error)
		  {
			  mainForm.Alert(this.Name, error.Message);
		  }

	  }

      private void AdminHolidaysForm_FormClosed(object sender, FormClosedEventArgs e)
      {
          if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
          {
              RegistryValue.Write(this.Name, "Top", this.Top.ToString());
              RegistryValue.Write(this.Name, "Left", this.Left.ToString());
              RegistryValue.Write(this.Name, "Height", this.Height.ToString());
              RegistryValue.Write(this.Name, "Width", this.Width.ToString());
          }

          mainForm.adminHolidaysForm = null;
      }

	  private void AddHolidayCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
	  {
		  this.Cursor = Cursors.WaitCursor;

		  try
		  {
			  AdminHolidaysInputForm adminHolidaysInputForm = new AdminHolidaysInputForm(mainForm, HolidayCalandar.CurrentDate.ToString(Standard.DateFormat), "US");
			  adminHolidaysInputForm.Show();
		  }
		  catch (Exception error)
		  {
			  mainForm.Alert(this.Name, error.Message);
		  }

		  this.Cursor = Cursors.Default;
	  }

	  private void HolidayCalandar_AppointmentLinkClicked(object sender, C1.Win.C1Schedule.AppointmentLinkClickedEventArgs e)
	  {
		  this.Cursor = Cursors.WaitCursor;

		  try
		  {
			  AdminHolidaysInputForm adminHolidaysInputForm = new AdminHolidaysInputForm(mainForm, HolidayCalandar.CurrentDate.ToString(Standard.DateFormat), Tools.SplitItem(e.Appointment.Subject, ":", 1));
			  adminHolidaysInputForm.Show();
		  }
		  catch (Exception error)
		  {
			  mainForm.Alert(this.Name, error.Message);
		  }

		  this.Cursor = Cursors.Default;	 	  
	  }


  }
}