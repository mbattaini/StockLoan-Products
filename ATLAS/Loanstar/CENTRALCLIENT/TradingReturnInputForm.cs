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
	public partial class TradingReturnInputForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm;

		private string bookGroup;
		private string book;
		private string secId;
		private string contractId;
		private string contractType;
		
		private long contractQuantity = 0;
		private long returnQuantity = 0;

		public TradingReturnInputForm(MainForm mainForm, string bookGroup, string book, string secId, string contractId, string contractType, long contractQuantity)
		{
			InitializeComponent();

			this.bookGroup = bookGroup;
			this.book = book;

			this.secId = secId;
			this.contractId = contractId;
			this.contractType = contractType;
	
			this.contractQuantity = contractQuantity;

			this.mainForm = mainForm;
		}

		private void TradingReturnInputForm_Load(object sender, EventArgs e)
		{
			try
			{
				SecurityIdTextBox.Text = secId;
				ContractIdTextBox.Text = contractId;
				ContractTypeTextBox.Text = contractType;
				ContractQuantityTextBox.Text = contractQuantity.ToString();				
			}
			catch (Exception error)
			{
				StatusMessageLabel.Text = error.Message;
			}
		}

		public long ReturnQuantity
		{
			get
			{
				return returnQuantity;
			}
		}

        private void ProjectedSettleDateTimeEdit_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Parse(ProjectedSettleDateTimeEdit.Text) < DateTime.Parse(mainForm.ServiceAgent.BizDate()))
            {
                StatusMessageLabel.Text = ProjectedSettleDateTimeEdit.Text + " is a past buisness day, As Of returns must be done in  As Of Actions module";
                ProjectedSettleDateTimeEdit.Text = mainForm.ServiceAgent.BizDate();
            }            
        }

        private void SubmitRibbonButton_Click(object sender, EventArgs e)
        {
            string returnId = "";

            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (long.Parse(QuantityTextBox.Text) > contractQuantity)
                {
                    StatusMessageLabel.Text = "Return Quantity cannot be more then contract quantity.";
                    return;
                }

                returnId = mainForm.PositionAgent.ReturnSet(
                    "",
                    bookGroup,
                    book,
                    contractId,
                    contractType,
                    QuantityTextBox.Text,
                    ProjectedSettleDateTimeEdit.Text,
                    "",
                    mainForm.UserId,
                    true);

                returnQuantity = long.Parse(QuantityTextBox.Text);
                StatusMessageLabel.Text = "Return successfully booked.";
            }
            catch (Exception error)
            {
                StatusMessageLabel.Text = error.Message;
            }

            ReturnIdTextBox.Text = returnId;

            this.Cursor = Cursors.Default;
        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
	}
}