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
    public partial class TradingBookCreditForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private DataSet dsBookGroups;
        private DataSet dsBooks;
        private DataSet dsBooksCredit;
        private DataSet dsContracts;

        private MainForm mainForm;

        public TradingBookCreditForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void AdminBookCreditForm_Load(object sender, EventArgs e)
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

                DateTimePicker.Value = mainForm.ServiceAgent.BizDate();

                dsBookGroups = mainForm.ServiceAgent.BookGroupGet(mainForm.UserId);
                BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];

                BookGroupCombo.SelectedIndex = -1;

                if (dsBookGroups.Tables["BookGroups"].Rows.Count > 0)
                {
                    BookGroupCombo.SelectedIndex = 0;
                }

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;         
        }


        private void DataLoad()
        {
            if (!DateTimePicker.Text.Equals("") && !BookGroupCombo.Text.Equals(""))
            {
                dsBooksCredit = mainForm.AdminAgent.BookCreditLimitsGet(DateTimePicker.Text, BookGroupCombo.Text, "", "", mainForm.UtcOffset);
                CreditGrid.SetDataBinding(dsBooksCredit, "CreditLimits", true);
            }
        }

        private void DateTimePicker_TextChanged(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void CreditGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value.ToString());
        }

        private void TradingBookCreditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.tradingBookCreditForm = null;
        }

        private void TradingBookCreditForm_KeyPress(object sender, KeyPressEventArgs e)
        {
 
        }

        private void CreditGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)13))
            {
                mainForm.AdminAgent.BookCreditLimitsSet(
                    CreditGrid.Columns["BookGroup"].Text,
                    CreditGrid.Columns["Book"].Text,
                    CreditGrid.Columns["Book"].Text,
                    CreditGrid.Columns["BorrowLimitAmount"].Text,
                    CreditGrid.Columns["LoanLimitAmount"].Text,
                    mainForm.UserId);
            }
        }    
    }
}
