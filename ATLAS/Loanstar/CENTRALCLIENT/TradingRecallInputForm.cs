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
	public partial class TradingRecallInputForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm;

		private string bookGroup;
		private string book;
		private string secId;
		private string contractId;
		private string contractType;

		private long contractQuantity = 0;
		private long recallQuantity = 0;

		public TradingRecallInputForm(MainForm mainForm, string bookGroup, string book, string secId, string contractId, string contractType, long contractQuantity)
		{
			InitializeComponent();

			this.bookGroup = bookGroup;
			this.book = book;

			this.secId = secId;
			this.contractId = contractId;

			this.contractQuantity = contractQuantity;
			this.contractType = contractType;

			this.mainForm = mainForm;
		}

		private void TradingRecallInputForm_Load(object sender, EventArgs e)
		{
			try
			{
				SecurityIdTextBox.Text = secId;
				ContractIdTextBox.Text = contractId;
				ContractQuantityTextBox.Text = contractQuantity.ToString();
			}
			catch (Exception error)
			{
				StatusMessageLabel.Text = error.Message;
			}
		}

        private void SubmitRibbonButton_Click(object sender, EventArgs e)
        {
            string recallId = "";

            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (long.Parse(QuantityTextBox.Text) > contractQuantity)
                {
                    StatusMessageLabel.Text = "Recall Quantity cannot be more then contract quantity.";
                    return;
                }

                recallId = mainForm.PositionAgent.RecallSet(
                    "",
                    bookGroup,
                    contractId,
                    contractType,
                    book,
                    secId,
                    QuantityTextBox.Text,
                    mainForm.ServiceAgent.BizDate(),
                    "",
                    "",
                    ReasonCodeTextBox.Text,
                    "",
                    "",
                    "O",
                    mainForm.UserId,
                    true);

                recallQuantity = long.Parse(QuantityTextBox.Text);
                StatusMessageLabel.Text = "Recall successfully booked.";
            }
            catch (Exception error)
            {
                StatusMessageLabel.Text = error.Message;
            }

            RecallIdTextBox.Text = recallId;

            this.Cursor = Cursors.Default;
        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}