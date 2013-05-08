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
	public partial class AdminLoginForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		private string userId = "";

		public AdminLoginForm(MainForm mainForm, string userId)
		{
			InitializeComponent();

			this.mainForm = mainForm;
			this.userId = RegistryValue.Read(this.Name, "Username", userId);
		}

		private void AdminLoginForm_Load(object sender, EventArgs e)
		{
			LoginTextBox.Text = userId;
		}

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (e.KeyChar.Equals((char)13))
            {
                Login();
            }
		}

        private void LoginRibbonButton_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            
            PasswordTextBox.UpdateValueWithCurrentText();

            if (!PasswordTextBox.Text.Equals(""))
            {
                RegistryValue.Write(this.Name, "Username", LoginTextBox.Text);
                mainForm.UserId = LoginTextBox.Text;

                if (mainForm.AdminAgent.UserPasswordValidate(LoginTextBox.Text, PasswordTextBox.Value.ToString()))
                {
                    StatusMessageTextBox.Text = "Login successful!";

                    mainForm.PermissionsSet(userId);

                    this.Close();
                }
                else
                {
                    StatusMessageTextBox.Text = "Username / Password is unknown or incorrect! Username / Password are case sensitive.";
                }
            }
        }

        private void CancelRibbonButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasswordChange_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (!LoginTextBox.Text.Equals(""))
                {
                    AdminLoginPasswordChangeForm adminPwdChangeForm = new AdminLoginPasswordChangeForm(mainForm, LoginTextBox.Text);
                    adminPwdChangeForm.Show();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }
	}
}