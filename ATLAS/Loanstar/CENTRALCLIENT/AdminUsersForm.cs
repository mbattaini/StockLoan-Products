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
    public partial class AdminUsersForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm = null;

        private DataSet dsUsers = null;
        private DataView dvUserRoles = null;
        private DataSet dsRoles = null;


        public AdminUsersForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void AdminUsersForm_Load(object sender, EventArgs e)
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

                dsUsers = mainForm.AdminAgent.UserRolesGet((short)mainForm.UtcOffset);      

                dvUserRoles = new DataView(dsUsers.Tables["UserRoles"], "UserId = '" + UsersGrid.Columns["UserId"].Text + "'", "", DataViewRowState.CurrentRows); 

                UsersGrid.SetDataBinding(dsUsers, "Users", true);
                UserRolesGrid.SetDataBinding(dvUserRoles, "", true);                     
               
                dsRoles = mainForm.AdminAgent.UserRoleFunctionsGet((short)mainForm.UtcOffset);

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void AdminUsersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.adminUsersForm = null;
        }

        private void Grid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {

            switch (e.Column.DataField)     
            {
                case "ActTime":
                case "LastAccess":
                    try
                    {
                        e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
                    }
                    catch { }
                    break;

                case "UsageCount":
                    try
                    {
                        e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
                    }
                    catch { }
                    break;

                default:
                    break;
            }

        }
        
        private void UsersGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
        {
              if (dvUserRoles != null)
            {   
                dvUserRoles.RowFilter = "UserId = '" + UsersGrid.Columns["UserId"].Text + "'";
            }
        }

        private void UsersGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
       
            if (e.KeyChar == 13) 
            {
                if (UsersGrid.DataChanged)
                {
                    UsersGrid.UpdateData();
                }
		
				if (UsersGrid.RowCount > UsersGrid.Row)
                {
                    UsersGrid.Col = 0;  
                }

                return;
            }
 
        }

        private void UsersGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (UsersGrid.Columns["UserId"].Text.Equals(""))
            {
                mainForm.Alert(this.Name, "User ID may not be blank.");
                UsersGrid.Col = 0;  //UserId
                e.Cancel = true;
                return;
            }

            if (UsersGrid.Columns["ShortName"].Text.Equals("") )
            {
                mainForm.Alert(this.Name, "User Short Name may not be blank.");
                UsersGrid.Col = (int)UsersGrid.Columns["ShortName"].Value;  
                UsersGrid.Col = 1;  //ShortName
                e.Cancel = true;
                return;
            }

            try
            {   
                mainForm.AdminAgent.UserSet(
                    UsersGrid.Columns["UserId"].Text,              
                    UsersGrid.Columns["ShortName"].Text,       
                    UsersGrid.Columns["Password"].Text,         
                    UsersGrid.Columns["Email"].Text,              
                    UsersGrid.Columns["Group"].Text,             
                    UsersGrid.Columns["Comment"].Text,       
                    mainForm.UserId,                                
                    true);                                          

                mainForm.Alert(this.Name, "User " + UsersGrid.Columns["UserId"].Text + " has been updated.");

                UsersGrid.Columns["Actor"].Text = mainForm.UserId;
                UsersGrid.Columns["ActTime"].Text = DateTime.Now.ToString();

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
               return;
            }

        }
        
        private void UsersGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
        {
  
            if ((UsersGrid.Columns[e.ColIndex].DataField.Equals("UserId")))
            {
                if (!UsersGrid.Columns[e.ColIndex].Text.Equals(""))
                {
                    UsersGrid.Col = e.ColIndex + 1;    //change Cell focus from UserId to next column 
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void UsersGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            try
            {   
                mainForm.AdminAgent.UserSet(
                    UsersGrid.Columns["UserId"].Text,          
                    UsersGrid.Columns["ShortName"].Text,   
                    UsersGrid.Columns["Password"].Text,     
                    UsersGrid.Columns["Email"].Text,          
                    UsersGrid.Columns["Group"].Text,         
                    UsersGrid.Columns["Comment"].Text + "[Deactivated by + " + mainForm.UserId + " on " + DateTime.Now.ToString(Standard.DateTimeShortFormat) + "]",              //Comment 
                    mainForm.UserId,                               
                    false);                                         

                mainForm.Alert(this.Name, "User: " + UsersGrid.Columns["UserId"].Text + " has been deactivated.");

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);                
                e.Cancel = true;
                return; 
            }

        }

        private void UserRolesGrid_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {            
            UserRolesGrid.Columns["RoleCode"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
            UserRolesGrid.Columns["RoleCode"].ValueItems.Translate = false;

        }
        
        private void UserRolesGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
        {
            try
            {
                if (UserRolesGrid.Columns[e.ColIndex].DataField.Equals("RoleCode"))
                {
                    if (UserRolesGrid.Columns[e.ColIndex].Text.Equals(""))
                    {
                        UserRolesGrid.Columns["RoleCode"].ValueItems.Values.Clear();

                        foreach (DataRow row in dsRoles.Tables["Roles"].Rows)
                        {
                            C1.Win.C1TrueDBGrid.ValueItem temp = new C1.Win.C1TrueDBGrid.ValueItem(row[0].ToString(), row[0].ToString());
                            UserRolesGrid.Columns[e.ColIndex].ValueItems.Values.Add(temp);
                        }

                        foreach (DataRowView dvRow in dvUserRoles)
                        {
                            for (int i = 0; i < (UserRolesGrid.Columns[e.ColIndex].ValueItems.Values.Count); i++)
                            {
                                if (dvRow["RoleCode"].ToString() == UserRolesGrid.Columns[e.ColIndex].ValueItems.Values[i].Value.ToString())
                                {
                                    UserRolesGrid.Columns[e.ColIndex].ValueItems.Values.RemoveAt(i);
                                }
                            }
                        }

                        if (!(UserRolesGrid.Columns[e.ColIndex].ValueItems.Values.Count == 0))
                        {
                            UserRolesGrid.Columns["RoleCode"].ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.SortedComboBox;
                            UserRolesGrid.Columns["RoleCode"].ValueItems.DefaultItem = -1;  
                            UserRolesGrid.Columns["RoleCode"].ValueItems.Validate = true;
                        }
                        else
                        {
                            mainForm.Alert(this.Name, "No additional Roles available to assign to User " + UserRolesGrid.Columns["UserId"].Text + ".");
                            UserRolesGrid.Delete(); 
                            e.Cancel = true;      
                            return;
                        }

                    }
                    else
                    {
                        UserRolesGrid.Col += 1;
                        e.Cancel = true;
                        return;
                    }

                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                Log.Write(error.Message + " [AdminUsersForm.UserRolesGrid_BeforeColEdit]", 1);
                e.Cancel = true;
                return;
            }

        }

        private void UserRolesGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {     
            try
            {
                mainForm.AdminAgent.UserRoleSet(
                    UserRolesGrid.Columns["UserId"].Text,
                    UserRolesGrid.Columns["RoleCode"].Text,
                    UserRolesGrid.Columns["Comment"].Text,
                    mainForm.UserId,
                    true
                    );

                mainForm.Alert(this.Name, "User " + UserRolesGrid.Columns["UserId"].Text + " no longer has " + UserRolesGrid.Columns["RoleCode"].Text + " privledges");              
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                Log.Write(error.Message + " [AdminUsersForm.UserRolesGrid_BeforeDelete]", 1);
                e.Cancel = true;
                return;
            }
        }

        private void UserRolesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            try
            {   
                if (UserRolesGrid.Row == UserRolesGrid.RowCount)
                {
                    UserRolesGrid.Col = 0;
                    UserRolesGrid.Row += 1;
                    e.Cancel = true;
                    return;
                }
                
                if (UserRolesGrid.Columns["RoleCode"].Text.Equals(""))
                {   
                    if (UserRolesGrid.Columns["RoleCode"].ValueItems.Values.Count > 0)
                    {
                        UserRolesGrid.Col = 1;    
                        mainForm.Alert(this.Name, "Role code cannot be left blank");  
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        UserRolesGrid.Col = 1;
                        UserRolesGrid.Row = 0;   
                        e.Cancel = true;  // 
                        return;
                    }
                }

                mainForm.AdminAgent.UserRoleSet(
                  UserRolesGrid.Columns["UserId"].Text,
                  UserRolesGrid.Columns["RoleCode"].Text,
                  UserRolesGrid.Columns["Comment"].Text,
                  mainForm.UserId,
                  false
                  );

                UserRolesGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
                UserRolesGrid.Columns["ActTime"].Text = DateTime.Now.ToString();

                mainForm.Alert(this.Name, "User " + UserRolesGrid.Columns["UserId"].Text + " has " + UserRolesGrid.Columns["RoleCode"].Text + " privledges");                            
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                Log.Write(error.Message + " [AdminUsersForm.UserRolesGrid_BeforeUpdate]", 1);
                e.Cancel = true;
                return;
            }

        }

        private void UserRolesGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && UserRolesGrid.DataChanged)
            {
                UserRolesGrid.UpdateData();

           
                UserRolesGrid.MoveLast();  
                UserRolesGrid.Row += 1;    
                UserRolesGrid.Col = 1;

                e.Handled = true;
            }
        }

        private void UserRolesGrid_OnAddNew(object sender, EventArgs e)
        {            
            UserRolesGrid.Columns["UserId"].Text = UsersGrid.Columns["UserId"].Text;
        }


        private void SendToCommand_Popup(object sender, EventArgs e)
        {
            try
            {
                if (UserRolesGrid.Focused)
                {
                    SendToClipboardCommand.Enabled = true;
                    SendToExcelCommand.Enabled = false;
                    SendToEmailCommand.Enabled = false;

                }
                else if (UsersGrid.Focused)
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
                if (UserRolesGrid.Focused)   
                {
                    mainForm.SendToClipboard(ref UserRolesGrid);
                }
                else if (UsersGrid.Focused)  
                {
                    mainForm.SendToClipboard(ref UsersGrid);
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
                if (UserRolesGrid.Focused)       
                {
                    mainForm.SendToExcel(ref UserRolesGrid, true);
                }
                else if (UsersGrid.Focused)      
                {
                    mainForm.SendToExcel(ref UsersGrid, true);
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
                if (UsersGrid.Focused && !(UserRolesGrid.Visible))
                {
                    mainForm.SendToEmail(ref UsersGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }


    }

}
