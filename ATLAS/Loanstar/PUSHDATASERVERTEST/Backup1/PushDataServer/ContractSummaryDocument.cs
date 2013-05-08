using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.PushData
{
	class ContractSummaryDocument
	{
		private string dbCnStr = "";
		private string emailRecipients = "";

		private string bookGroup = "";
		private string bizDate = "";

		DataSet dsContractSecId;
		DataSet dsContracts;			

		public ContractSummaryDocument(string dbCnStr, string emailRecipients, string bizDate, string bookGroup)
		{
			this.dbCnStr = dbCnStr;

			this.bizDate = bizDate;
			this.bookGroup = bookGroup;
		}

		private void DataGet()
		{
			dsContractSecId = new DataSet();
			dsContracts = new DataSet();
			
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spContractSecIdGet", dbCnStr);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
			paramBizDate.Value = bizDate;

			SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
			dataAdapter.Fill(dsContractSecId, "SecId");

			 dbCmd.CommandText = "spContractGet";
			
			SqlParameter paramBooKGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10)
			paramBooKGroup.Value = bookGroup;

			dataAdapter.SelectCommand = dbCmd;

			dataAdapter.Fill(dsContracts, "Contracts");
		}

		private void ContentGet()
		{
			string spacer = "  ";
			string header = "Contra Party".PadRight(20, ' ') + spacer +
									"Contract#".PadRight(9, ' ') +  spacer +
									"Del-Dt".PadRight(8, ' ') + spacer + 
									"Borr Quantity".PadRight(13, ' ') + spacer +
									"Loan Quantity".PadRight(13, ' ') + spacer + 
									"Borr Amount".PadRight(13, ' ') +  spacer +
									"Loan Amount".PadRight(13, ' ') + spacer +
									"MKT Val \ Diff".PadRight(13, ' ');
		}
	}
}
