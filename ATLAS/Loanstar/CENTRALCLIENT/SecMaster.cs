using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace CentralClient
{
	public partial class SecMaster : UserControl
	{
		private MainForm mainForm = null;
		private DataSet dsSecMasterItem = null;
		private DataSet dsBoxPositionItem = null;
		private bool showBox = false;
		private string secId = "";
		private string isin = "";
		private string sedol = "";
		private string symbol = "";

		public string SecId
		{

			set
			{
				secId = value;

				SecMasterFill(secId);

				if (showBox)
				{
					BoxPositionFill(SecIdTextBox.Text);
				}

				SecIdTextBox.Text = secId;
			}

			get
			{
				return secId;
			}
		}

		public string Sedol
		{
			get
			{
				return sedol;
			}
		}

		public string Isin
		{
			get
			{
				return isin;
			}
		}

		public string Symbol
		{
			get
			{
				return symbol;
			}
		}

		public bool ShowBox
		{
			set
			{
				showBox = value;

				if (showBox)
				{
					BoxPositionFill(SecIdTextBox.Text);
				}
			}
		}

		public MainForm LoanstarMainForm
		{
			set
			{
				this.mainForm = value;
			}
		}

		public SecMaster()
		{
			InitializeComponent();
			dsSecMasterItem = new DataSet();
		}


		public void BoxPositionFill(string secId)
		{
			try
			{
				if (!secId.Equals(""))
				{
					dsBoxPositionItem = mainForm.PositionAgent.BoxPositionGet(mainForm.ServiceAgent.BizDate(), "1423", secId);

					if (dsBoxPositionItem.Tables["BoxPosition"].Rows.Count > 0)
					{
						MainNetPositionSettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						MainNetPositionSettledLabel.DataField = "NetPositionSettled";

						NetPositionSettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						NetPositionSettledLabel.DataField = "NetPositionSettled";

						NetPositionTradedLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						NetPositionTradedLabel.DataField = "NetPositionTraded";

						NetPositionUnsettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						NetPositionUnsettledLabel.DataField = "NetPositionUnsettled";

						TotalLongsSettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						TotalLongsSettledLabel.DataField = "TotalLongsSettled";

						TotalLongsTradedLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						TotalLongsTradedLabel.DataField = "TotalLongsTraded";

						TotalLongsUnsettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						TotalLongsUnsettledLabel.DataField = "TotalLongsUnsettled";

						TotalShortsSettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						TotalShortsSettledLabel.DataField = "TotalShortsSettled";

						TotalShortsTradedLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						TotalShortsTradedLabel.DataField = "TotalShortsTraded";

						TotalShortsUnsettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						TotalShortsUnsettledLabel.DataField = "TotalShortsUnsettled";

						ExDeficitSettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						ExDeficitSettledLabel.DataField = "ExDeficitSettled";

						ExDeficitTradedLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						ExDeficitTradedLabel.DataField = "ExDeficitTraded";

						ExDeficitUnsettledLabel.DataSource = dsBoxPositionItem.Tables["BoxPosition"];
						ExDeficitUnsettledLabel.DataField = "ExDeficitUnSettled";
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		public void SecMasterFill(string secId)
		{

			try
			{
				if (!secId.Equals(""))
				{
					dsSecMasterItem = mainForm.ServiceAgent.SecMasterLookup(secId);

					if (dsSecMasterItem.Tables["SecMasterItem"].Rows.Count > 0)
					{
						PriceLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						PriceLabel.DataField = "Price";

						DescriptionLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						DescriptionLabel.DataField = "Description";

						SedolLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						SedolLabel.DataField = "Sedol";

						SymbolLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						SymbolLabel.DataField = "Symbol";

						IsinLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						IsinLabel.DataField = "Isin";

						QuickLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						QuickLabel.DataField = "Quick";

						CusipLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						CusipLabel.DataField = "Cusip";

						BorrowRateLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						BorrowRateLabel.DataField = "BorrowRate";

						LoanRateLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						LoanRateLabel.DataField = "LoanRate";

						CurrencyCodeLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						CurrencyCodeLabel.DataField = "CurrencyCode";

						PriceDateLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						PriceDateLabel.DataField = "PriceDate";

						CountryCodeLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						CountryCodeLabel.DataField = "CountryCode";

						BaseTypeLabel.DataSource = dsSecMasterItem.Tables["SecMasterItem"];
						BaseTypeLabel.DataField = "BaseType";

						isin = IsinLabel.Text;
						sedol = SedolLabel.Text;
						symbol = SymbolLabel.Text;						
					}
					else
					{
						isin = "";
						sedol = "";
						symbol = "";

						SecMasterResultsForm secMasterResultsForm = new SecMasterResultsForm(mainForm, secId);
						secMasterResultsForm.Show();
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			SecMasterFill(SecIdTextBox.Text);

			if (showBox)
			{
				BoxPositionFill(SecIdTextBox.Text);
			}
		}

		private void SecIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				SecMasterFill(SecIdTextBox.Text);

				if (showBox)
				{
					BoxPositionFill(SecIdTextBox.Text);
				}
			}
		}

		private void PriceDateLabel_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
		{
			try
			{
				e.Text = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
			}
			catch { }
		}

		private void BorrowRateLabel_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
		{
			try
			{
				e.Text = decimal.Parse(e.Value.ToString()).ToString(Formats.Rate);
			}
			catch { }
		}

		private void PriceLabel_Formatted(object sender, C1.Win.C1Input.FormatEventArgs e)
		{
			try
			{
				e.Text = float.Parse(e.Value.ToString()).ToString(Formats.Price);
			}
			catch { }
		}
	}
}
