using System;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
    public class InventoryLookupForm : System.Windows.Forms.Form
    {
        private const string TEXT = "Inventory - Lookup";

        private string secId = "";
        private string tradeDate = "";

        private float splitRatio = 0.40F;
        private DataSet dataSet;

        private MainForm mainForm;
        private DataSet inventoryDataSet = null;

        private System.Windows.Forms.GroupBox FindGroupBox;
        private System.Windows.Forms.TextBox FindTextBox;
        private C1.Win.C1Input.C1Label DescriptionLabel;

        private C1.Win.C1List.C1List InventoryList;
        private System.Windows.Forms.RadioButton InventoryModeRadio;
        private System.Windows.Forms.RadioButton InventorySourceRadio;
        private C1.Win.C1Input.C1DateEdit DateEditor;
        private C1.Win.C1Input.C1Label EffectDateLabel;

        private System.ComponentModel.Container components = null;

        public InventoryLookupForm(MainForm mainForm)
        {
            this.Text = TEXT;
            this.mainForm = mainForm;

            InitializeComponent();

            try
            {
                tradeDate = mainForm.ShortSaleAgent.TradeDate();
            }
            catch (Exception e)
            {
                mainForm.Alert(e.Message, PilotState.RunFault);
            }

            InventoryList.Splits[0, 0].DisplayColumns["ScribeTime"].Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryLookupForm));
            this.InventoryList = new C1.Win.C1List.C1List();
            this.DescriptionLabel = new C1.Win.C1Input.C1Label();
            this.FindGroupBox = new System.Windows.Forms.GroupBox();
            this.FindTextBox = new System.Windows.Forms.TextBox();
            this.InventoryModeRadio = new System.Windows.Forms.RadioButton();
            this.InventorySourceRadio = new System.Windows.Forms.RadioButton();
            this.DateEditor = new C1.Win.C1Input.C1DateEdit();
            this.EffectDateLabel = new C1.Win.C1Input.C1Label();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionLabel)).BeginInit();
            this.FindGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // InventoryList
            // 
            this.InventoryList.AddItemSeparator = ';';
            this.InventoryList.AllowColSelect = false;
            this.InventoryList.BackColor = System.Drawing.Color.Honeydew;
            this.InventoryList.Caption = "Inventory";
            this.InventoryList.CaptionHeight = 17;
            this.InventoryList.ColumnCaptionHeight = 17;
            this.InventoryList.ColumnFooterHeight = 17;
            this.InventoryList.DeadAreaBackColor = System.Drawing.Color.DarkGray;
            this.InventoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InventoryList.EmptyRows = true;
            this.InventoryList.ExtendRightColumn = true;
            this.InventoryList.FetchRowStyles = true;
            this.InventoryList.Images.Add(((System.Drawing.Image)(resources.GetObject("InventoryList.Images"))));
            this.InventoryList.ItemHeight = 15;
            this.InventoryList.Location = new System.Drawing.Point(1, 42);
            this.InventoryList.MatchEntryTimeout = ((long)(2000));
            this.InventoryList.Name = "InventoryList";
            this.InventoryList.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.InventoryList.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.InventoryList.SelectionMode = C1.Win.C1List.SelectionModeEnum.MultiExtended;
            this.InventoryList.Size = new System.Drawing.Size(826, 423);
            this.InventoryList.TabIndex = 5;
            this.InventoryList.TabStop = false;
            this.InventoryList.FetchRowStyle += new C1.Win.C1List.FetchRowStyleEventHandler(this.InventoryList_FetchRowStyle);
            this.InventoryList.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.InventoryList_FormatText);
            this.InventoryList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InventoryList_KeyPress);
            this.InventoryList.PropBag = resources.GetString("InventoryList.PropBag");
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLabel.ForeColor = System.Drawing.Color.Maroon;
            this.DescriptionLabel.Location = new System.Drawing.Point(148, 5);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(204, 28);
            this.DescriptionLabel.TabIndex = 2;
            this.DescriptionLabel.Tag = null;
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DescriptionLabel.TextDetached = true;
            this.DescriptionLabel.UseMnemonic = false;
            // 
            // FindGroupBox
            // 
            this.FindGroupBox.Controls.Add(this.FindTextBox);
            this.FindGroupBox.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindGroupBox.Location = new System.Drawing.Point(12, 3);
            this.FindGroupBox.Name = "FindGroupBox";
            this.FindGroupBox.Size = new System.Drawing.Size(126, 32);
            this.FindGroupBox.TabIndex = 0;
            this.FindGroupBox.TabStop = false;
            this.FindGroupBox.Text = "Find";
            // 
            // FindTextBox
            // 
            this.FindTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.FindTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FindTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.FindTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindTextBox.ForeColor = System.Drawing.Color.Navy;
            this.FindTextBox.Location = new System.Drawing.Point(4, 14);
            this.FindTextBox.Name = "FindTextBox";
            this.FindTextBox.Size = new System.Drawing.Size(118, 15);
            this.FindTextBox.TabIndex = 1;
            this.FindTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FindTextBox.Enter += new System.EventHandler(this.FindTextBox_Enter);
            this.FindTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindTextBox_KeyPress);
            this.FindTextBox.MouseEnter += new System.EventHandler(this.FindTextBox_MouseEnter);
            // 
            // InventoryModeRadio
            // 
            this.InventoryModeRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InventoryModeRadio.Checked = true;
            this.InventoryModeRadio.Location = new System.Drawing.Point(4, 470);
            this.InventoryModeRadio.Name = "InventoryModeRadio";
            this.InventoryModeRadio.Size = new System.Drawing.Size(160, 24);
            this.InventoryModeRadio.TabIndex = 6;
            this.InventoryModeRadio.TabStop = true;
            this.InventoryModeRadio.Text = "Inventory Mode";
            this.InventoryModeRadio.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // InventorySourceRadio
            // 
            this.InventorySourceRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InventorySourceRadio.Location = new System.Drawing.Point(168, 470);
            this.InventorySourceRadio.Name = "InventorySourceRadio";
            this.InventorySourceRadio.Size = new System.Drawing.Size(160, 24);
            this.InventorySourceRadio.TabIndex = 7;
            this.InventorySourceRadio.Text = "Inventory Source Mode";
            this.InventorySourceRadio.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // DateEditor
            // 
            this.DateEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DateEditor.AutoSize = false;
            // 
            // 
            // 
            this.DateEditor.Calendar.DayNameLength = 1;
            this.DateEditor.Calendar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateEditor.CustomFormat = "yyyy-MM-dd";
            this.DateEditor.DateTimeInput = false;
            this.DateEditor.DisplayFormat.CustomFormat = "yyyy-MM-dd";
            this.DateEditor.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DateEditor.DisplayFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
            this.DateEditor.DisplayFormat.TrimStart = true;
            this.DateEditor.DropDownFormAlign = C1.Win.C1Input.DropDownFormAlignmentEnum.Right;
            this.DateEditor.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DateEditor.EditFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat;
            this.DateEditor.ErrorInfo.BeepOnError = true;
            this.DateEditor.ErrorInfo.CanLoseFocus = true;
            this.DateEditor.ErrorInfo.ShowErrorMessage = false;
            this.DateEditor.Location = new System.Drawing.Point(728, 9);
            this.DateEditor.Name = "DateEditor";
            this.DateEditor.Size = new System.Drawing.Size(96, 20);
            this.DateEditor.TabIndex = 120;
            this.DateEditor.Tag = null;
            this.DateEditor.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.DateEditor.TextChanged += new System.EventHandler(this.DateEditor_TextChanged);
            // 
            // EffectDateLabel
            // 
            this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EffectDateLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EffectDateLabel.Location = new System.Drawing.Point(632, 11);
            this.EffectDateLabel.Name = "EffectDateLabel";
            this.EffectDateLabel.Size = new System.Drawing.Size(92, 16);
            this.EffectDateLabel.TabIndex = 119;
            this.EffectDateLabel.Tag = null;
            this.EffectDateLabel.Text = "In Effect For:";
            this.EffectDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EffectDateLabel.TextDetached = true;
            // 
            // InventoryLookupForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(828, 465);
            this.Controls.Add(this.DateEditor);
            this.Controls.Add(this.EffectDateLabel);
            this.Controls.Add(this.InventorySourceRadio);
            this.Controls.Add(this.InventoryModeRadio);
            this.Controls.Add(this.InventoryList);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.FindGroupBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(375, 250);
            this.Name = "InventoryLookupForm";
            this.Padding = new System.Windows.Forms.Padding(1, 42, 1, 0);
            this.Text = "Inventory - Lookup";
            this.Closed += new System.EventHandler(this.InventoryLookupForm_Closed);
            this.Load += new System.EventHandler(this.InventoryLookupForm_Load);
            this.Resize += new System.EventHandler(this.InventoryLookupForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionLabel)).EndInit();
            this.FindGroupBox.ResumeLayout(false);
            this.FindGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public string SecId
        {
            set
            {
                if (!secId.Equals(value))
                {
                    secId = value;

                    DescriptionLabel.Text = mainForm.Description;

                    FindTextBox.Text = mainForm.SecId;
                    FindTextBox.SelectAll();

                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    InventoryListFill(secId);
                    this.Cursor = Cursors.Default;
                }
            }

            get
            {
                return secId;
            }
        }




        private void InventoryListFill(string secId)
        {
            try
            {
                if (inventoryDataSet != null)
                {
                    inventoryDataSet.Tables["Inventory"].Clear();
                    inventoryDataSet.Tables["Inventory"].AcceptChanges(); // Clear list of prior data.         
                }

                if (secId != null)
                {
                    InventoryList.Caption = "Available Inventory [" + secId + "]";

                    if (DateEditor.Text.Equals(mainForm.ServiceAgent.BizDate()))
                    {
                        inventoryDataSet = mainForm.ShortSaleAgent.InventoryGet(secId, mainForm.UtcOffset, false);
                    }
                    else
                    {
                        inventoryDataSet = mainForm.ShortSaleAgent.InventoryHistoryLookupGet(DateEditor.Text, secId);
                    }

                    InventoryList.HoldFields();
                    InventoryList.DataSource = inventoryDataSet;
                    InventoryList.DataMember = "Inventory";
                    InventoryList.Rebind();

                }

                if (inventoryDataSet.Tables["Inventory"].Rows.Count > 0)
                {
                    InventoryList.DeadAreaBackColor = Color.Honeydew;
                }
                else
                {
                    InventoryList.DeadAreaBackColor = Color.DarkGray;
                }
            }
            catch (Exception e)
            {
                InventoryList.DeadAreaBackColor = Color.RosyBrown;

                mainForm.Alert(e.Message);
                Log.Write(e.Message + " [InventoryLookupForm.InventoryListFill]", Log.Error, 1);
            }
        }

        private void InventoryLookupForm_Load(object sender, System.EventArgs e)
        {
            int height = 500;
            int width = 375;

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));


            DateEditor.Text = mainForm.ServiceAgent.BizDate();

            InventoryListFill(""); // This is a hack that sets up the grid for future loads.		
        }

        private void InventoryLookupForm_Closed(object sender, System.EventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.inventoryLookupForm = null;
        }

        private void InventoryLookupForm_Resize(object sender, System.EventArgs e)
        {
            DescriptionLabel.Width = this.ClientSize.Width - DescriptionLabel.Left - 4;
        }


        private void FindTextBox_MouseEnter(object sender, System.EventArgs e)
        {
            FindTextBox.SelectAll();
        }

        private void FindTextBox_Enter(object sender, System.EventArgs e)
        {
            FindTextBox.SelectAll();
        }

        private void FindTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)13))
            {
                mainForm.SecId = FindTextBox.Text.Trim();
                e.Handled = true;
            }
        }

        private void InventoryList_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string gridData = "";

            if (e.KeyChar.Equals((char)3) && InventoryList.SelectedIndices.Count > 0)
            {
                foreach (C1.Win.C1List.C1DataColumn dataColumn in InventoryList.Columns)
                {
                    gridData += dataColumn.Caption + "\t";
                }

                gridData += "\n";

                foreach (int row in InventoryList.SelectedIndices)
                {
                    foreach (C1.Win.C1List.C1DataColumn dataColumn in InventoryList.Columns)
                    {
                        gridData += dataColumn.CellText(row) + "\t";
                    }

                    gridData += "\n";

                    if ((row % 100) == 0)
                    {
                        mainForm.Alert("Please wait: " + row.ToString("#,##0") + " rows copied so far...");
                    }
                }

                Clipboard.SetDataObject(gridData, true);
                mainForm.Alert("Copied " + InventoryList.SelectedIndices.Count.ToString("#,##0") + " rows to the clipboard.");
                e.Handled = true;
            }
        }

        private void InventoryList_FetchRowStyle(object sender, C1.Win.C1List.FetchRowStyleEventArgs e)
        {
            if (DateEditor.Text.Equals(mainForm.ServiceAgent.BizDate()))
            {
                if (InventoryList.GetItemText(e.Row, "For").CompareTo(tradeDate) >= 0)
                {
                    if (long.Parse(InventoryList.Columns["Quantity"].CellValue(e.Row).ToString()) < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Navy;
                    }
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Gray;
                }
            }
            else
            {
                e.CellStyle.ForeColor = Color.DarkGreen;
            }
        }

        private void InventoryList_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
        {
            if (e.Value.Equals(""))
            {
                return;
            }

            switch (InventoryList.Columns[e.ColIndex].DataField)
            {
                case ("ScribeTime"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
                    }
                    catch { }
                    break;
                case ("BizDate"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
                    }
                    catch { }
                    break;
                case ("Quantity"):
                    try
                    {
                        e.Value = long.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;
            }
        }

        private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
        {
            int textLength;
            int[] maxTextLength;

            int columnIndex = -1;
            string gridData = "\n\n\n";

            if (InventoryList.Columns.Count.Equals(0))
            {
                mainForm.Alert("You have not selected any rows.");
                return;
            }

            try
            {
                maxTextLength = new int[InventoryList.Columns.Count];

                // Get the caption length for each column.
                foreach (C1.Win.C1List.C1DataColumn dataColumn in InventoryList.Columns)
                {
                    maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
                }

                // Get the maximum item length for each row in each column.
                foreach (int rowIndex in InventoryList.SelectedIndices)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1List.C1DataColumn dataColumn in InventoryList.Columns)
                    {
                        if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
                        {
                            maxTextLength[columnIndex] = textLength;
                        }
                    }
                }

                columnIndex = -1;

                foreach (C1.Win.C1List.C1DataColumn dataColumn in InventoryList.Columns)
                {
                    gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
                }
                gridData += "\n";

                columnIndex = -1;

                foreach (C1.Win.C1List.C1DataColumn dataColumn in InventoryList.Columns)
                {
                    gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
                }
                gridData += "\n";

                foreach (int rowIndex in InventoryList.SelectedIndices)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1List.C1DataColumn dataColumn in InventoryList.Columns)
                    {
                        if (dataColumn.Value.GetType().Equals(typeof(System.String)))
                        {
                            gridData += dataColumn.CellText(rowIndex).PadRight(maxTextLength[++columnIndex] + 2);
                        }
                        else
                        {
                            gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
                        }
                    }

                    gridData += "\n";
                }

                Email email = new Email();
                email.Send(gridData);

                mainForm.Alert("Total: " + InventoryList.SelectedIndices.Count + " items added to e-mail.");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [PositionBoxSummaryForm.SendToEmailMenuItem_Click]", Log.Error, 1);
                mainForm.Alert(error.Message, PilotState.RunFault);
            }
        }
        
        private void DockLeftMenuItem_Click(object sender, System.EventArgs e)
        {
            this.DockPadding.Top = 1;
            this.Dock = DockStyle.Left;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockRightMenuItem_Click(object sender, System.EventArgs e)
        {
            this.DockPadding.Top = 1;
            this.Dock = DockStyle.Right;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
        {
            this.DockPadding.Top = 42;
            this.Dock = DockStyle.None;
            this.ControlBox = true;
            this.Text = TEXT;
        }

        private void CheckedChanged(object sender, System.EventArgs e)
        {
            // hide inventory mode controls
            FindGroupBox.Visible = !InventorySourceRadio.Checked;
            FindTextBox.Visible = !InventorySourceRadio.Checked;
            InventoryList.Visible = !InventorySourceRadio.Checked;
            DateEditor.Visible = !InventorySourceRadio.Checked;
            EffectDateLabel.Visible = !InventorySourceRadio.Checked;

            if (!InventorySourceRadio.Checked)
            {
                DescriptionLabel.Location = new Point(148, 8);
            }
            else
            {
                DescriptionLabel.Location = new Point(12, 2);
            }
      }


        private void ClearGridMenuItem_Click(object sender, System.EventArgs e)
        {
            dataSet.Tables["Input"].Clear();
        }

        private void DateEditor_TextChanged(object sender, System.EventArgs e)
        {
            InventoryListFill(FindTextBox.Text);
        }
    }
}
