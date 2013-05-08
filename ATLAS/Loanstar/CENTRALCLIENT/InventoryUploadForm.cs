using System;
using System.Collections;
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
	public partial class InventoryUploadForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		private DataSet dsInventory = null;

		public InventoryUploadForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void ParseListButton_Click(object sender, EventArgs e)
		{
			string rate = "";
			Input input = new Input(InputTextBox.Text);

			try
			{
				for (int i = 0; i < input.Count; i++)
				{
					DataRow dataRow = dsInventory.Tables["Input"].NewRow();

					dataRow["SecId"] = input.SecId(i);

					if (Tools.IsNumeric(input.Quantity(i)))
					{
						dataRow["Quantity"] = Tools.ParseLong(input.Quantity(i));
					}
					else
					{
						dataRow["Quantity"] = DBNull.Value;
					}

					if (Tools.IsNumeric(input.Rate(i)))
					{
						dataRow["Rate"] = input.Rate(i);
					}
				
					dsInventory.Tables["Input"].Rows.Add(dataRow);
				}

				InputGrid.Row = InputGrid.FirstRow;
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			InputGrid.SetDataBinding(dsInventory, "Input", true);
		}

		private void CommitButton_Click(object sender, EventArgs e)
		{
			int itemCount = 0;

			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (DeskTextBox.Text.Equals(""))
				{
					mainForm.Alert(this.Name, "Desk cannot be blank!");
					return;
				}

				foreach (DataRow dr in dsInventory.Tables["Input"].Rows)
				{
					mainForm.InventoryAgent.InventoryItemSet(
						mainForm.ServiceAgent.BizDate(),
						DeskTextBox.Text,
						dr["SecId"].ToString(),
						dr["Quantity"].ToString());
				
					itemCount ++;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			mainForm.Alert(this.Name, "Uploaded " + itemCount.ToString("#,##0") + " inventory items for " + DeskTextBox.Text);
			this.Cursor = Cursors.Default;
		}

		private void InventoryUploadForm_LocationChanged(object sender, EventArgs e)
		{

		}

		private void InventoryUploadForm_Load(object sender, EventArgs e)
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


				dsInventory = new DataSet();
				dsInventory.Tables.Add("Input");

				dsInventory.Tables["Input"].Columns.Add("SecId", typeof(string));
				dsInventory.Tables["Input"].Columns.Add("Quantity", typeof(long));
				dsInventory.Tables["Input"].Columns.Add("Rate", typeof(decimal));

				dsInventory.AcceptChanges();

				InputGrid.SetDataBinding(dsInventory, "Input", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
            this.Cursor = Cursors.Default;
		}

        private void InventoryUploadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }
            mainForm.inventoryUploadForm = null;
        }
	}

	public class Input
	{
		private string status = "";

		private ArrayList items;
		private ColumnIndex columnIndex;

		public Input() : this("") { }
		public Input(string list)
		{
			items = new ArrayList();
			columnIndex = new ColumnIndex();

			if (!list.Equals(""))
			{
				Parse(list);
			}
		}

		public string Parse(string list)
		{
			string[] records;
			string[] fields;

			char[] delimiter = new Char[1];

			items.Clear();
			columnIndex.Clear();

			try
			{
				delimiter[0] = '\n';

				records = list.Split(delimiter); // Do the split on new-line character; trim balance of white space later.

				for (int i = 0; i < records.Length; i++) // Parse list items.
				{
					string record = records[i].Trim(); // Taking a copy to trim just once.

					if (record.IndexOf(":") > 0) // Use ':' as delimiter for this record.
					{
						delimiter[0] = ':';
					}
					else if (record.IndexOf(";") > 0) // Use ';' as delimiter for this record.
					{
						delimiter[0] = ';';
					}
					else if (record.IndexOf("|") > 0) // Use '|' as delimiter for this record.
					{
						delimiter[0] = '|';
					}
					else if (record.IndexOf("\t") > 0) // Use tab as delimiter for this record.
					{
						delimiter[0] = '\t';
					}
					else // Must use ' ' as delimiter for this record.
					{
						delimiter[0] = ' ';

						for (int j = 25; j > 0; j--) // Replace multiple instances of space with just one.
						{
							records[i] = records[i].Replace(new String(delimiter[0], j), delimiter[0].ToString());
						}
					}

					fields = records[i].Split(delimiter);

					// ToDo: Any field manipulation.

					if (fields.Length > 3) // Hack to concatenate last two fields.
					{
						fields[2] += fields[3];
					}

					string[] values = new string[5] { "", "", "", "", "" };

					columnIndex.HaveSecId = false;

					for (int j = 0; (j < fields.Length) && (j < 4); j++)
					{
						values[j + 1] = columnIndex.Set(fields[j], j);
					}

					if (columnIndex.HaveSecId)
					{
						items.Add(values);
					}
				}

				return status = "OK";
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Input.Parse]", 2);

				return status = "Error: Unable to parse your list.";
			}
		}

		public string SecId(int index)
		{
			string[] values = (string[])items[index];
			return values[columnIndex.SecId];
		}

		public string Quantity(int index)
		{
			string[] values = (string[])items[index];
			return values[columnIndex.Quantity];
		}

		public string Price(int index)
		{
			string[] values = (string[])items[index];
			return values[columnIndex.Price];
		}

		public string Rate(int index)
		{
			string[] values = (string[])items[index];
			return values[columnIndex.Rate];
		}

		public string Status
		{
			get
			{
				return status;
			}
		}

		public int Count
		{
			get
			{
				return items.Count;
			}
		}

		private class ColumnIndex
		{
			private int[] secIdCount = new int[4] { 0, 0, 0, 0 };
			private int[] quantityCount = new int[4] { 0, 0, 0, 0 };
			private int[] priceCount = new int[4] { 0, 0, 0, 0 };
			private int[] rateCount = new int[4] { 0, 0, 0, 0 };

			private bool haveSecId = false;

			public string Set(string field, int index)
			{
				if (index > 3)
				{
					throw new Exception("Value for index, " + index + ",  must not be greater than 3. [ListStats.Set]");
				}

				if (index < 0)
				{
					throw new Exception("Value for index, " + index + ",  must not be less than 0. [ListStats.Set]");
				}

				field = field.ToUpper().Replace(" ", "").Replace(",", "").Trim();

				if (field.StartsWith("(") && field.EndsWith(")"))
				{
					field = field.Replace("(", "-").Replace(")", "");
				}

				if (Security.IsSedol(field) || Security.IsSymbol(field))
				{
					haveSecId = true;

					secIdCount[index]++;

					return field;
				}

				if (Tools.IsNumeric(field))
				{
					decimal fieldValue = decimal.Parse(field);

					if (fieldValue < 100M)
					{
						rateCount[index]++;
					}
					else
					{
						quantityCount[index]++;
					}

					return field;
				}

				field = field.Replace("M", "000");
				field = field.Replace("K", "000");
				field = field.Replace("C", "00");
				field = field.Replace("H", "00");

				if (Tools.IsNumeric(field))
				{
					quantityCount[index]++;

					return field;
				}

				if (field.StartsWith("NEG") || field.EndsWith("NEG"))
				{
					field = "-" + field.Replace("NEG", "");

					if (Tools.IsNumeric(field))
					{
						rateCount[index]++;

						return field;
					}
				}

				if (field.EndsWith("%"))
				{
					field = field.Replace("%", "");

					if (Tools.IsNumeric(field))
					{
						rateCount[index]++;

						return field;
					}
				}

				if (field.StartsWith("N") || field.EndsWith("N"))
				{
					field = "-" + field.Replace("N", "");

					if (Tools.IsNumeric(field))
					{
						rateCount[index]++;

						return field;
					}
				}

				if (field.StartsWith("P") || field.EndsWith("P"))
				{
					field.Replace("P", "");

					if (Tools.IsNumeric(field))
					{
						priceCount[index]++;
					}
				}

				return "";
			}

			public void Clear()
			{
				for (int i = 0; i < 4; i++)
				{
					secIdCount[i] = 0;
					quantityCount[i] = 0;
					priceCount[i] = 0;
					rateCount[i] = 0;
				}
			}

			public bool HaveSecId
			{
				set
				{
					haveSecId = value;
				}

				get
				{
					return haveSecId;
				}
			}

			public int SecId
			{
				get
				{
					int index = 0;
					int count = 0;

					for (int i = 0; i < 4; i++)
					{
						if (secIdCount[i] > count)
						{
							count = secIdCount[i];
							index = i + 1;
						}
					}

					return index;
				}
			}

			public int Quantity
			{
				get
				{
					int index = 0;
					int count = 0;

					for (int i = 0; i < 4; i++)
					{
						if (quantityCount[i] > count)
						{
							count = quantityCount[i];
							index = i + 1;
						}
					}

					return index;
				}
			}

			public int Price
			{
				get
				{
					int index = 0;
					int count = 0;

					for (int i = 0; i < 4; i++)
					{
						if (priceCount[i] > count)
						{
							count = priceCount[i];
							index = i + 1;
						}
					}

					return index;
				}
			}

			public int Rate
			{
				get
				{
					int index = 0;
					int count = 0;

					for (int i = 0; i < 4; i++)
					{
						if (rateCount[i] > count)
						{
							count = rateCount[i];
							index = i + 1;
						}
					}

					return index;
				}
			}
		}
	}
}