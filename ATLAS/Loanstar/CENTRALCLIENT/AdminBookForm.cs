using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace CentralClient
{
	public partial class AdminBooksForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		
        private DataSet dsBookGroups;
		private DataSet dsCurrencies;
		private DataSet dsCountries;
		private DataSet dsBookFunds;
		private DataSet dsBookContacts;
		private DataSet dsBookClearingInstructions;
		private DataSet dsBooks;
		private DataSet dsCreditLimits;
		private DataSet dsFunds;
        private DataSet dsClearingCounterParties;
        private DataSet dsClearingSystems;

		private DataView dvBookContacts = null;
		private DataView dvBookContactsDetails = null;
		private DataView dvBookClearingInstructions = null;
		private DataView dvBookClearingInstructionsDetails = null;
		private DataView dvBookFunds = null;
		private DataView dvBooks = null;

		private string firstName = "";
		private string lastName = "";

		private string currencyIso = "";
		private string countryCode = "";

		private string book = "";

		public AdminBooksForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void BookContactsGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (e.KeyChar.Equals((char)13))
				{
					BookContactsGrid.UpdateData();
                    e.Handled = true;
                }
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
                e.Handled = false;
            }

			this.Cursor = Cursors.Default;
		}

		private void BookClearingInstructionsGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;            

			try
			{
				if (e.KeyChar.Equals((char)13))
				{
					BookClearingInstructionsGrid.UpdateData();
                    e.Handled = true;
                }
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
                e.Handled = false;
            }

			this.Cursor = Cursors.Default;
		}

		private void BookOtherGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (e.KeyChar.Equals((char)13))
				{
					BookOtherGrid.UpdateData();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void BookContactsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				mainForm.AdminAgent.BookContactSet(
				  BookGroupCombo.Text,
				  BookContactsGrid.Columns["Book"].Text,
				  BookContactsGrid.Columns["FirstName"].Text,
				  BookContactsGrid.Columns["LastName"].Text,
				  BookContactsGrid.Columns["Function"].Text,
				  BookContactsGrid.Columns["PhoneNumber"].Text,
				  BookContactsGrid.Columns["FaxNumber"].Text,
				  BookContactsGrid.Columns["Comment"].Text,
				  mainForm.UserId,
				  true);

				BookContactsGrid.Columns["Actor"].Text = mainForm.UserId;
				BookContactsGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void BookClearingInstructionsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
                mainForm.AdminAgent.BookClearingInstructionSet(
                  BookGroupCombo.Text,
                  BookClearingInstructionsGrid.Columns["BookGroupAlias"].Text,
                  BookClearingInstructionsGrid.Columns["Book"].Text,
                  BookClearingInstructionsGrid.Columns["BookAlias"].Text,
                  BookClearingInstructionsGrid.Columns["System"].Text,
                  BookClearingInstructionsGrid.Columns["Exchange"].Text,
                  BookClearingInstructionsGrid.Columns["Settlement"].Text,
                  BookClearingInstructionsGrid.Columns["RouteName"].Text,
                  BookClearingInstructionsGrid.Columns["Entity"].Text,
                  bool.Parse(BookClearingInstructionsGrid.Columns["DoReturn"].Value.ToString()),
                  BookClearingInstructionsGrid.Columns["ReturnRoute"].Text);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void BooksGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (!BooksGrid.Columns["Book"].Text.Equals(""))
				{
					mainForm.AdminAgent.BookSet(
					  BookGroupCombo.Text,
					  BooksGrid.Columns["Book"].Text,
					  BooksGrid.Columns["Book"].Text,
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  "",
					  mainForm.UserId,
					  true);
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

        private void RefreshData()
        {
            try
            {
                dsBookGroups = mainForm.ServiceAgent.BookGroupGet(mainForm.UserId);
                dsCurrencies = mainForm.ServiceAgent.CurrenciesGet();
                dsCountries = mainForm.ServiceAgent.CountriesGet("");
                dsFunds = mainForm.PositionAgent.FundsGet();
                dsClearingSystems = mainForm.TradingAgent.TradingSystemsGet();
                dsClearingCounterParties = mainForm.TradingAgent.TradingSystemsCounterPartiesGet("");


                BookGroupNameLabel.DataSource = dsBookGroups.Tables["BookGroups"];
                BookGroupNameLabel.DataField = "BookGroupName";

                BookGroupCombo.HoldFields();
                BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];
                BookGroupCombo.SelectedIndex = -1;

                CountryCodeCombo.HoldFields();
                CountryCodeCombo.DataSource = dsCountries.Tables["Countries"];
                CountryCodeCombo.SelectedIndex = -1;

                FundCombo.HoldFields();
                FundCombo.DataSource = dsFunds.Tables["Funds"];
                FundCombo.SelectedIndex = -1;

                CounterPartyCodeDropdown.SetDataBinding(dsClearingCounterParties, "CounterParties", true);
                SystemDropdown.SetDataBinding(dsClearingSystems, "Systems", true);

                if (dsCountries.Tables["Countries"].Rows.Count > 0)
                {
                    CountryCodeCombo.SelectedIndex = 0;
                }

                if (dsBookGroups.Tables["BookGroups"].Rows.Count > 0)
                {
                    BookGroupCombo.SelectedIndex = 0;
                }

                dsBooks = mainForm.AdminAgent.BookGet(BookGroupCombo.Text, "");

                BooksGrid.SetDataBinding(dsBooks, "Books", true);

                if (DockingTab.SelectedIndex == 0)
                {
                    BookNameTextBox.Text = BooksGrid.Columns["BookName"].Text;
                    BookAddress1TextBox.Text = BooksGrid.Columns["AddressLine1"].Text;
                    BookAddress2TextBox.Text = BooksGrid.Columns["AddressLine2"].Text;
                    BookAddress3TextBox.Text = BooksGrid.Columns["AddressLine3"].Text;
                    BookPhoneNumberTextBox.Text = BooksGrid.Columns["PhoneNumber"].Text;
                    BookFaxNumberTextBox.Text = BooksGrid.Columns["FaxNumber"].Text;
                    BookHouseLoanRateTextBox.Value = BooksGrid.Columns["RateStockLoan"].Text;
                    BookHouseBorrowRateTextBox.Value = BooksGrid.Columns["RateStockBorrow"].Text;
                    BookHouseLoanMarginAmtTextBox.Value = BooksGrid.Columns["MarginLoan"].Text;
                    BookHouseBorrowMarginAmtTextBox.Value = BooksGrid.Columns["MarginBorrow"].Text;
                    FundCombo.Text = BooksGrid.Columns["FundDefault"].Text;
                }

                DockingTab.SelectedIndex = 0;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }


        private void AdminBooksForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            int height = this.Height;
            int width = this.Width;

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

            RefreshData();

            this.Cursor = Cursors.Default;
        }

		private void BooksGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (e.KeyChar.Equals((char)13))
				{
					BooksGrid.UpdateData();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void BookGroupCombo_TextChanged(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsBooks = mainForm.AdminAgent.BookGet(BookGroupCombo.Text, "");

				BooksGrid.SetDataBinding(dsBooks, "Books", true);

				if (DockingTab.SelectedIndex == 0)
				{
					BookNameTextBox.Text = BooksGrid.Columns["BookName"].Text;
					BookAddress1TextBox.Text = BooksGrid.Columns["AddressLine1"].Text;
					BookAddress2TextBox.Text = BooksGrid.Columns["AddressLine2"].Text;
					BookAddress3TextBox.Text = BooksGrid.Columns["AddressLine3"].Text;
					BookPhoneNumberTextBox.Text = BooksGrid.Columns["PhoneNumber"].Text;
					BookFaxNumberTextBox.Text = BooksGrid.Columns["FaxNumber"].Text;
					BookHouseLoanRateTextBox.Value = BooksGrid.Columns["RateStockLoan"].Text;
					BookHouseBorrowRateTextBox.Value = BooksGrid.Columns["RateStockBorrow"].Text;
					BookHouseLoanMarginAmtTextBox.Value = BooksGrid.Columns["MarginLoan"].Text;
					BookHouseBorrowMarginAmtTextBox.Value = BooksGrid.Columns["MarginBorrow"].Text;
					FundCombo.Text = BooksGrid.Columns["FundDefault"].Text;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void BookContactsGrid_OnAddNew(object sender, EventArgs e)
		{
			BookContactsGrid.Columns["BookGroup"].Text = BookGroupCombo.Text;
			BookContactsGrid.Columns["Book"].Text = BooksGrid.Columns["Book"].Text;
		}

		private void BookContactsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{

			switch (BookContactsGrid.Columns[e.ColIndex].DataField)
			{
				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
					}
					catch { }
					break;

				default:
					break;
			}
		}

		private void BookClearingInstructionsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (BookContactsGrid.Columns[e.ColIndex].DataField)
			{
				case "DivRate":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("00.000");
					}
					catch { }
					break;

				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
					}
					catch { }
					break;

				default:
					break;
			}
		}

        private void BookClearingInstructionsGrid_OnAddNew(object sender, EventArgs e)
        {
            BookClearingInstructionsGrid.Columns["BookGroup"].Text = BookGroupCombo.Text;
            BookClearingInstructionsGrid.Columns["BookGroupAlias"].Text = "PXO";
            BookClearingInstructionsGrid.Columns["Book"].Text = BooksGrid.Columns["Book"].Text;
            BookClearingInstructionsGrid.Columns["DoReturn"].Value = false;
            BookClearingInstructionsGrid.Columns["Entity"].Text = "PFSL";
        }

		private void BooksGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				mainForm.AdminAgent.BookSet(
				  BookGroupCombo.Text,
				  BooksGrid.Columns["Book"].Text,
				  BooksGrid.Columns["Book"].Text,
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  "",
				  mainForm.UserId,
				  false);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void BooksGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			if (!BooksGrid.Columns["Book"].Text.Equals(""))
			{
				e.Cancel = true;
			}
        }

		private void BookContactsGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				mainForm.AdminAgent.BookContactSet(
					BookGroupCombo.Text,
					BookContactsGrid.Columns["Book"].Text,
					BookContactsGrid.Columns["FirstName"].Text,
					BookContactsGrid.Columns["LastName"].Text,
					BookContactsGrid.Columns["Function"].Text,
					BookContactsGrid.Columns["PhoneNumber"].Text,
					BookContactsGrid.Columns["FaxNumber"].Text,
					BookContactsGrid.Columns["Comment"].Text,
					mainForm.UserId,
					false);

				BookContactsGrid.Columns["IsActive"].Value = 1;
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		


		private void BookContactsGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!BookContactsGrid.Columns["FirstName"].Text.Equals(firstName) || !BookContactsGrid.Columns["LastName"].Text.Equals(lastName))
			{
				if (dvBookContactsDetails != null)
				{
					dvBookContactsDetails.RowFilter = "FirstName = '" + BookContactsGrid.Columns["FirstName"].Text + "' AND LastName = '" + BookContactsGrid.Columns["LastName"].Text + "'";

					firstName = BookContactsGrid.Columns["FirstName"].Text;
					lastName = BookContactsGrid.Columns["LastName"].Text;
				}
			}
		}

		private void BookClearingInstructionsGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!BookClearingInstructionsGrid.Columns["CountryCode"].Text.Equals(countryCode) && !BookClearingInstructionsGrid.Columns["CurrencyCode"].Text.Equals(currencyIso))
			{
				if (dvBookClearingInstructionsDetails != null)
				{
					dvBookClearingInstructionsDetails.RowFilter = "CurrencyCode = '" + BookClearingInstructionsGrid.Columns["CurrencyCode"].Text + "' AND CountryCode = '" + BookClearingInstructionsGrid.Columns["CountryCode"].Text + "'";

					currencyIso = BookClearingInstructionsGrid.Columns["CurrencyCode"].Text;
					countryCode = BookClearingInstructionsGrid.Columns["CountryCode"].Text;
				}
			}
		}

        private void BookContactsDetailsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.AdminAgent.BookContactSet(
                  BookGroupCombo.Text,
                  BookContactsGrid.Columns["Book"].Text,
                  BookContactsGrid.Columns["FirstName"].Text,
                  BookContactsGrid.Columns["LastName"].Text,
                  BookContactsGrid.Columns["Function"].Text,
                  BookContactsDetailsGrid.Columns["PhoneNumber"].Text,
                  BookContactsDetailsGrid.Columns["FaxNumber"].Text,
                  BookContactsDetailsGrid.Columns["Comment"].Text,
                  mainForm.UserId,
                  true);

                BookContactsGrid.Columns["Actor"].Text = mainForm.UserId;
                BookContactsGrid.Columns["ActTime"].Text = DateTime.Now.ToString();

                BookContactsDetailsGrid.Columns["Actor"].Text = mainForm.UserId;
                BookContactsDetailsGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

		private void BookContactsDetailsGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				BookContactsDetailsGrid.UpdateData();
			}
		}

		private void BooksGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!BooksGrid.Columns["Book"].Text.Equals(book))
			{
				this.Cursor = Cursors.WaitCursor;

				try
				{
					dsBookContacts = mainForm.AdminAgent.BookContactGet(BookGroupCombo.Text, BooksGrid.Columns["Book"].Text);
					dsBookClearingInstructions = mainForm.AdminAgent.BookClearingInstructionGet(BookGroupCombo.Text, BooksGrid.Columns["Book"].Text, "");
					dsCreditLimits = mainForm.AdminAgent.BookCreditLimitsGet(mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text, BooksGrid.Columns["Book"].Text, BooksGrid.Columns["Book"].Text, (short)mainForm.UtcOffset);
					dsBookFunds = mainForm.AdminAgent.BookFundInstructionGet(BookGroupCombo.Text, BooksGrid.Columns["Book"].Text, "", mainForm.UtcOffset);

					if (DockingTab.SelectedIndex == 0)
					{
                        BookTextBox.Text = BooksGrid.Columns["Book"].Text;
						BookNameTextBox.Text = BooksGrid.Columns["BookName"].Text;
						BookAddress1TextBox.Text = BooksGrid.Columns["AddressLine1"].Text;
						BookAddress2TextBox.Text = BooksGrid.Columns["AddressLine2"].Text;
						BookAddress3TextBox.Text = BooksGrid.Columns["AddressLine3"].Text;
						BookPhoneNumberTextBox.Text = BooksGrid.Columns["PhoneNumber"].Text;
						BookFaxNumberTextBox.Text = BooksGrid.Columns["FaxNumber"].Text;
						BookHouseLoanRateTextBox.Value = BooksGrid.Columns["RateStockLoan"].Text;
						BookHouseBorrowRateTextBox.Value = BooksGrid.Columns["RateStockBorrow"].Text;
						BookHouseLoanMarginAmtTextBox.Value = BooksGrid.Columns["MarginLoan"].Text;
						BookHouseBorrowMarginAmtTextBox.Value = BooksGrid.Columns["MarginBorrow"].Text;
						FundCombo.Text = BooksGrid.Columns["FundDefault"].Text;
					
					}					
					
					dvBookContacts = new DataView(dsBookContacts.Tables["BookContacts"], "IsActive = 1", "", DataViewRowState.CurrentRows);
					dvBookContactsDetails = new DataView(dsBookContacts.Tables["BookContacts"], "FirstName = '' AND LastName = ''", "", DataViewRowState.CurrentRows);

					dvBookClearingInstructions = new DataView(dsBookClearingInstructions.Tables["BookInstructions"], "", "", DataViewRowState.CurrentRows);					

					dvBookFunds = new DataView(dsBookFunds.Tables["BookFunds"], "IsActive = 1", "", DataViewRowState.CurrentRows);

					BookContactsGrid.SetDataBinding(dvBookContacts, "", true);
					BookContactsDetailsGrid.SetDataBinding(dvBookContactsDetails, "", true);

					BookClearingInstructionsGrid.SetDataBinding(dsBookClearingInstructions, "BookInstructions", true);
				}
				catch (Exception error)
				{
					mainForm.Alert(this.Name, error.Message);
				}

				book = BooksGrid.Columns["Book"].Text;
				this.Cursor = Cursors.Default;
			}
		}

		private void BookContactsDetailsGrid_OnAddNew(object sender, EventArgs e)
		{
			BookContactsDetailsGrid.Columns["BookGroup"].Text = BookContactsGrid.Columns["BookGroup"].Text;
			BookContactsDetailsGrid.Columns["Book"].Text = BookContactsGrid.Columns["Book"].Text;
		}

		private void AdminBooksForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}

			mainForm.adminBooksForm = null;
		}

		private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{				
				switch (DockingTab.SelectedIndex)
				{
					case 1:
                        mainForm.SendToClipboard(ref BookContactsGrid);
						break;

					case 2:
                        mainForm.SendToClipboard(ref BookClearingInstructionsGrid);
                        break;                    
                    
                    case 3:
                        mainForm.SendToClipboard(ref BookOtherGrid);
                        break;
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
                switch (DockingTab.SelectedIndex)
                {
                    case 1:
                        mainForm.SendToExcel(ref BookContactsGrid, true);
                        break;

                    case 2:
                        mainForm.SendToExcel(ref BookClearingInstructionsGrid, true);
                        break;

                    case 3:                 
                        mainForm.SendToExcel(ref BookOtherGrid, true);
                        break;
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
                switch (DockingTab.SelectedIndex)
                {
                    case 1:
                        mainForm.SendToEmail(ref BookContactsGrid);
                        break;

                    case 2:
                        mainForm.SendToEmail(ref BookClearingInstructionsGrid);
                        break;

                    case 3:
                        mainForm.SendToEmail(ref BookOtherGrid);
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

		private void DockingTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (DockingTab.SelectedIndex)
			{
				case 0:
					SendToExcelCommand.Enabled = false;
                    SaveRibbonButton.Visible = true;
                    CancelRibbonButton.Visible = true;
					break;

				default:
					SendToExcelCommand.Enabled = true;
                    SaveRibbonButton.Visible = false;
                    CancelRibbonButton.Visible = false;					
					break;
			}
		}

        private void SaveRibbonButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.AdminAgent.BookSet(
                  BookGroupCombo.Text,
                  BookTextBox.Text,
                  BookTextBox.Text,
                  BookNameTextBox.Text,
                  BookAddress1TextBox.Text,
                  BookAddress2TextBox.Text,
                  BookAddress3TextBox.Text,
                  BookPhoneNumberTextBox.Text,
                  BookFaxNumberTextBox.Text,
                  BookHouseBorrowMarginAmtTextBox.Text,
                  BookHouseLoanMarginAmtTextBox.Text,
                  "",
                  "",
                  BookHouseBorrowRateTextBox.Text,
                  BookHouseLoanRateTextBox.Text,
                  BookBondsHouseBorrowRateTextBox.Text,
                  BookBondsHouseLoanRateTextBox.Text,
                  CountryCodeCombo.Text,
                  FundCombo.Text,
                  mainForm.UserId,
                  true);

                RefreshData();            
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

        private void RateTextBox_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
        {            
            try
            {
                e.Text = decimal.Parse(e.Value.ToString()).ToString(Formats.Rate);
            }
            catch { }
        }

        private void MarginAmtTextBox_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
        {

            try
            {
                e.Text = decimal.Parse(e.Value.ToString()).ToString(Formats.Margin);
            }
            catch { }
        }

        private void BookAddRibbonButton_Click(object sender, EventArgs e)
        {
            BookTextBox.Text = "";
            BookNameTextBox.Text = "";
            BookAddress1TextBox.Text = "";
            BookAddress2TextBox.Text = "";
            BookAddress3TextBox.Text = "";
            BookPhoneNumberTextBox.Text = "";
            BookFaxNumberTextBox.Text = "";
            BookHouseLoanRateTextBox.Value = "";
            BookHouseBorrowRateTextBox.Value = "";
            BookHouseLoanMarginAmtTextBox.Value = "";
            BookHouseBorrowMarginAmtTextBox.Value = "";
            FundCombo.Text = "";
					
        }  
	}
}