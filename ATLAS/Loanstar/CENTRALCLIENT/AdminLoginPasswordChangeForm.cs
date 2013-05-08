using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Main;

namespace CentralClient
{
    public partial class AdminLoginPasswordChangeForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm;
        private string userId;

        private bool isOldPassword = false;
        private bool isNewPassword = false;

        public AdminLoginPasswordChangeForm(MainForm mainForm, string userId)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.userId = userId;
        }

        private void OldPasswordTextBox_Leave(object sender, EventArgs e)
        {
            if (!OldPasswordTextBox.Text.Equals(""))
            {
                if (!mainForm.AdminAgent.UserPasswordValidate(userId, OldPasswordTextBox.Text))
                {
                    OldPasswordTextBox.BackColor = System.Drawing.Color.LightPink;
                    StatusMessageTextBox.Text = "Old password does not match current password.";

                    isOldPassword = false;
                }
                else
                {
                    OldPasswordTextBox.BackColor = System.Drawing.Color.LightYellow;
                    StatusMessageTextBox.Text = "";

                    isOldPassword = true;
                }
            }
        }

        private void NewTwoPasswordTextBox_Leave(object sender, EventArgs e)
        {
            if (!NewTwoPasswordTextBox.Text.Equals("") && !NewOnePasswordTextBox.Text.Equals(""))
            {
                if (!NewOnePasswordTextBox.Text.Equals(NewTwoPasswordTextBox.Text))
                {
                    NewOnePasswordTextBox.BackColor = System.Drawing.Color.LightPink;
                    NewTwoPasswordTextBox.BackColor = System.Drawing.Color.LightPink;
                    StatusMessageTextBox.Text = "New password 1 and new password 2 do not match.";

                    isNewPassword = false;
                }
                else
                {
                    NewOnePasswordTextBox.BackColor = System.Drawing.Color.White;
                    NewTwoPasswordTextBox.BackColor = System.Drawing.Color.White;
                    StatusMessageTextBox.Text = "";

                    isNewPassword = true;
                }
            }
        }

        private void SubmitRibbonButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if ((isOldPassword) && (isNewPassword))
                {
                    mainForm.AdminAgent.UserSet(
                        userId,
                        "",
                        NewTwoPasswordTextBox.Text,
                        "",
                        "",
                        "",
                        mainForm.UserId,
                       true);

                    StatusMessageTextBox.Text = "New password applied successfully!";
                }
                else
                {
                    StatusMessageTextBox.Text = "Password change failed!";
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void CancelRibbonButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
