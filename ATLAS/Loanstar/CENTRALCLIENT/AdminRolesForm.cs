using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.Main;

namespace CentralClient
{
	public partial class AdminRolesForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;

		private DataSet dsRoles = null;             
		private DataView dvRoleFunctions = null;

		public AdminRolesForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;

		}

		private void AdminRolesForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
                int height = this.Height;
                int width = this.Width;

                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
               
				dsRoles = mainForm.AdminAgent.UserRoleFunctionsGet(mainForm.UtcOffset);      

				dvRoleFunctions = new DataView(dsRoles.Tables["RoleFunctions"], "RoleCode = '" + RolesGrid.Columns["RoleCode"].Text + "'", "", DataViewRowState.CurrentRows);     
				
                RolesGrid.SetDataBinding(dsRoles, "Roles", true);                             
				RolesFunctionGrid.SetDataBinding(dvRoleFunctions, "", true);             

			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

        private void AdminRolesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }
            mainForm.adminRolesForm = null;
        }
        
        private void Grid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{        
			switch (e.Column.DataField)
			{
				case ("ActTime"):
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
					}
					catch { }
					break;
			}
		}

        private void RolesGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
        {
            if (RolesGrid.Columns[e.ColIndex].DataField.Equals("RoleCode"))
            {
                if (!RolesGrid.Columns[e.ColIndex].Text.Equals(""))
                {
                    RolesGrid.Col = e.ColIndex + 1;    
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void RolesGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
        {            
            if (dvRoleFunctions != null)
            {
                dvRoleFunctions.RowFilter = "RoleCode = '" + RolesGrid.Columns["RoleCode"].Text + "'";
            }
        }

        private void RolesGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13))  
            {
                if (RolesGrid.DataChanged)
                {
                    RolesGrid.UpdateData();
                }

                if (RolesGrid.RowCount > RolesGrid.Row) 
                {
                    RolesGrid.Col = 0;
                }

                return;
            }
        }

        private void RolesGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            try
            { 
                mainForm.AdminAgent.RoleSet(
                    RolesGrid.Columns["RoleCode"].Text,  
                    "",                                  
                    "",                                  
                    mainForm.UserId,           
                    true                               
                    );

                mainForm.Alert(this.Name, "Role " + RolesGrid.Columns["RoleCode"].Text + " removed");
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);                
                e.Cancel = true;
            }

        }

        private void RolesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
         if (RolesGrid.Columns["RoleCode"].Text.Equals(""))
            {
                mainForm.Alert(this.Name, "RoleCode may not be blank.");
                RolesGrid.Col = 0;
                e.Cancel = true;
                return;
            }

            if (RolesGrid.Columns["RoleCode"].Text.Length > 5)
            {
                mainForm.Alert(this.Name, "RoleCode may not exceed five characters.");
                RolesGrid.Col = 0;  
                e.Cancel = true;
                return;
            }

            try
            {  
				mainForm.AdminAgent.RoleSet(
                    RolesGrid.Columns["RoleCode"].Text, 
                    RolesGrid.Columns["Role"].Text,        
                    RolesGrid.Columns["Comment"].Text,
                    mainForm.UserId,                    
                    false);                                 

                mainForm.Alert(this.Name, "Role " + RolesGrid.Columns["RoleCode"].Text + " has been updated.");

                dsRoles = mainForm.AdminAgent.UserRoleFunctionsGet(mainForm.UtcOffset);     
                dvRoleFunctions = new DataView(dsRoles.Tables["RoleFunctions"], "RoleCode = '" + RolesGrid.Columns["RoleCode"].Text + "'", "", DataViewRowState.CurrentRows);  
                RolesGrid.SetDataBinding(dsRoles, "Roles", true);                 
                RolesFunctionGrid.SetDataBinding(dvRoleFunctions, "", true);      

                RolesGrid.Columns["Actor"].Text = mainForm.UserId;
                RolesGrid.Columns["ActTime"].Text = DateTime.Now.ToString();

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);                
                return;
            }

        }

        private void RolesFunctionGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
     
            if ((e.KeyChar == 13))
            {
                if (RolesFunctionGrid.DataChanged)
                {
                    RolesFunctionGrid.UpdateData();
                }

                if (RolesFunctionGrid.RowCount > RolesFunctionGrid.Row )
                {
                    RolesFunctionGrid.Col = 0;
                }

                return;
            }
        }

        private void RolesFunctionGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            try
            {  
                mainForm.AdminAgent.RoleFunctionSet(
                    RolesFunctionGrid.Columns["RoleCode"].Text, 
                    RolesFunctionGrid.Columns["FunctionPath"].Text, 
                    (RolesFunctionGrid.Columns["MayView"].Value != DBNull.Value) ? bool.Parse(RolesFunctionGrid.Columns["MayView"].Text) : false,   //MayView
                    (RolesFunctionGrid.Columns["MayEdit"].Value != DBNull.Value) ? bool.Parse(RolesFunctionGrid.Columns["MayEdit"].Text) : false,   //MayEdit
                    null,                                                           
                    null,                                                           
                    mainForm.UserId                                        
                    );

                 mainForm.Alert(this.Name, "Role " + RolesFunctionGrid.Columns["RoleCode"].Text + " Function " + RolesFunctionGrid.Columns["FunctionPath"].Text + " was updated.");

            
				RolesFunctionGrid.Columns["Actor"].Text = mainForm.UserId;
                RolesFunctionGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                Log.Write(error.Message + " [AdminRolesForm.RolesFunctionGrid_BeforeUpdate]", 1);
                return;
            }

        }

        private void RolesFunctionGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
        {     
            if ((RolesFunctionGrid.Columns[e.ColIndex].DataField.Equals("FunctionPath")))
            {
                    RolesFunctionGrid.Col = e.ColIndex + 1;
                    e.Cancel = true;
                    return;
            }

        }

        private void SendToCommand_Popup(object sender, EventArgs e)
        {
            try
            {
                if (RolesFunctionGrid.Focused )
                {
                    SendToClipboardCommand.Enabled = true;
                    SendToExcelCommand.Enabled = false;
                    SendToEmailCommand.Enabled = false;

                }
                else if (RolesGrid.Focused)
                {
                    SendToClipboardCommand.Enabled = true;
                    SendToExcelCommand.Enabled = true;
                    SendToEmailCommand.Enabled = true;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (RolesFunctionGrid.Focused)
                {
                    mainForm.SendToClipboard(ref RolesFunctionGrid);
                }
                else if (RolesGrid.Focused)
                {
                    mainForm.SendToClipboard(ref RolesGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {          
                if (RolesFunctionGrid.Focused)
                {
                    mainForm.SendToExcel(ref RolesFunctionGrid, true);
                }
                else if(RolesGrid.Focused)
                {
                    mainForm.SendToExcel(ref RolesGrid, true);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (RolesGrid.Focused && !(RolesFunctionGrid.Visible))
                {
                    mainForm.SendToEmail(ref RolesGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }


	}

}