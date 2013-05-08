using System;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using Anetics.Common;

namespace Anetics.Medalist
{
    public class SubstitutionInputForm : System.Windows.Forms.Form
    {
        private const string TEXT = "Substitution - Lookup";

        private string secId = "";
        private DataSet dataSet;

        private MainForm mainForm;
        private ArrayList substitutionEventArgsArray;
        private C1.Win.C1Input.C1TextBox SecurityIdTextBox;
        private C1.Win.C1Input.C1TextBox RateOverrideText;
        private C1.Win.C1Input.C1Label SecurityIdLabel;
        private C1.Win.C1Input.C1Label RequestedQuantityLabel;
        private C1.Win.C1Input.C1Label RateOverrideLabel;
        private System.Windows.Forms.Button SubmitButton;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid SubstitutionGrid;
        private C1.Win.C1Input.C1TextBox RequestedQuantityTextBox;

        private System.ComponentModel.Container components = null;

        private SubstitutionActivityEventWrapper substitutionEventWrapper;
        private SubstitutionActivityEventHandler substitutionEventHandler;

        private DataView dataView = null;
        private string dataViewRowFilter = "";
        private System.Windows.Forms.CheckBox SubstitutionCheckBox;
        private TextBox StatusTextBox;

        private bool isReady;


        public string SubstitutionSecId
        {
            set
            {
                SecurityIdTextBox.Text = value;
            }
        }


        public SubstitutionInputForm(MainForm mainForm)
        {
            this.Text = TEXT;
            this.mainForm = mainForm;

            InitializeComponent();


            try
            {                
                substitutionEventArgsArray = new ArrayList();

                substitutionEventWrapper = new SubstitutionActivityEventWrapper();
                substitutionEventWrapper.SubstitutionActivityEvent += new SubstitutionActivityEventHandler(SubstitutionOnEvent);

                substitutionEventHandler = new SubstitutionActivityEventHandler(SubstitutionDoEvent);

                
                this.isReady = true;
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message + " [SubstitutionInputForm.SubstitutionInputForm]");
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubstitutionInputForm));
            this.SecurityIdTextBox = new C1.Win.C1Input.C1TextBox();
            this.RequestedQuantityTextBox = new C1.Win.C1Input.C1TextBox();
            this.RateOverrideText = new C1.Win.C1Input.C1TextBox();
            this.SecurityIdLabel = new C1.Win.C1Input.C1Label();
            this.RequestedQuantityLabel = new C1.Win.C1Input.C1Label();
            this.RateOverrideLabel = new C1.Win.C1Input.C1Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.SubstitutionGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.SubstitutionCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestedQuantityTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateOverrideText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityIdLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestedQuantityLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateOverrideLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubstitutionGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SecurityIdTextBox
            // 
            this.SecurityIdTextBox.Location = new System.Drawing.Point(108, 9);
            this.SecurityIdTextBox.Name = "SecurityIdTextBox";
            this.SecurityIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.SecurityIdTextBox.TabIndex = 0;
            this.SecurityIdTextBox.Tag = null;
            this.SecurityIdTextBox.TextDetached = true;
            // 
            // RequestedQuantityTextBox
            // 
            this.RequestedQuantityTextBox.Location = new System.Drawing.Point(356, 9);
            this.RequestedQuantityTextBox.Name = "RequestedQuantityTextBox";
            this.RequestedQuantityTextBox.Size = new System.Drawing.Size(100, 20);
            this.RequestedQuantityTextBox.TabIndex = 1;
            this.RequestedQuantityTextBox.Tag = null;
            this.RequestedQuantityTextBox.TextDetached = true;
            // 
            // RateOverrideText
            // 
            this.RateOverrideText.Location = new System.Drawing.Point(580, 9);
            this.RateOverrideText.Name = "RateOverrideText";
            this.RateOverrideText.Size = new System.Drawing.Size(100, 20);
            this.RateOverrideText.TabIndex = 2;
            this.RateOverrideText.Tag = null;
            this.RateOverrideText.TextDetached = true;
            // 
            // SecurityIdLabel
            // 
            this.SecurityIdLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SecurityIdLabel.Location = new System.Drawing.Point(4, 8);
            this.SecurityIdLabel.Name = "SecurityIdLabel";
            this.SecurityIdLabel.Size = new System.Drawing.Size(100, 23);
            this.SecurityIdLabel.TabIndex = 3;
            this.SecurityIdLabel.Tag = null;
            this.SecurityIdLabel.Text = "Security ID:";
            this.SecurityIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SecurityIdLabel.TextDetached = true;
            // 
            // RequestedQuantityLabel
            // 
            this.RequestedQuantityLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RequestedQuantityLabel.Location = new System.Drawing.Point(228, 8);
            this.RequestedQuantityLabel.Name = "RequestedQuantityLabel";
            this.RequestedQuantityLabel.Size = new System.Drawing.Size(124, 23);
            this.RequestedQuantityLabel.TabIndex = 4;
            this.RequestedQuantityLabel.Tag = null;
            this.RequestedQuantityLabel.Text = "Requested Quantity:";
            this.RequestedQuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RequestedQuantityLabel.TextDetached = true;
            // 
            // RateOverrideLabel
            // 
            this.RateOverrideLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RateOverrideLabel.Location = new System.Drawing.Point(476, 8);
            this.RateOverrideLabel.Name = "RateOverrideLabel";
            this.RateOverrideLabel.Size = new System.Drawing.Size(100, 23);
            this.RateOverrideLabel.TabIndex = 5;
            this.RateOverrideLabel.Tag = null;
            this.RateOverrideLabel.Text = "Rate Override:";
            this.RateOverrideLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RateOverrideLabel.TextDetached = true;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(700, 7);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(120, 24);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // SubstitutionGrid
            // 
            this.SubstitutionGrid.AllowUpdate = false;
            this.SubstitutionGrid.AlternatingRows = true;
            this.SubstitutionGrid.CaptionHeight = 17;
            this.SubstitutionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubstitutionGrid.EmptyRows = true;
            this.SubstitutionGrid.ExtendRightColumn = true;
            this.SubstitutionGrid.FetchRowStyles = true;
            this.SubstitutionGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.SubstitutionGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SubstitutionGrid.Images"))));
            this.SubstitutionGrid.Location = new System.Drawing.Point(1, 42);
            this.SubstitutionGrid.Name = "SubstitutionGrid";
            this.SubstitutionGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.SubstitutionGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.SubstitutionGrid.PreviewInfo.ZoomFactor = 75D;
            this.SubstitutionGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("SubstitutionGrid.PrintInfo.PageSettings")));
            this.SubstitutionGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.SubstitutionGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.SubstitutionGrid.RowHeight = 15;
            this.SubstitutionGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
            this.SubstitutionGrid.Size = new System.Drawing.Size(1220, 292);
            this.SubstitutionGrid.TabIndex = 7;
            this.SubstitutionGrid.Text = "S3EventsGrid";
            this.SubstitutionGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SubstitutionGrid_FormatText);
            this.SubstitutionGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.SubstitutionGrid_FetchRowStyle);
            this.SubstitutionGrid.PropBag = resources.GetString("SubstitutionGrid.PropBag");
            // 
            // SubstitutionCheckBox
            // 
            this.SubstitutionCheckBox.Location = new System.Drawing.Point(868, 7);
            this.SubstitutionCheckBox.Name = "SubstitutionCheckBox";
            this.SubstitutionCheckBox.Size = new System.Drawing.Size(124, 24);
            this.SubstitutionCheckBox.TabIndex = 8;
            this.SubstitutionCheckBox.Text = "Do Subsititution";
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusTextBox.Location = new System.Drawing.Point(1012, 12);
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.Size = new System.Drawing.Size(100, 14);
            this.StatusTextBox.TabIndex = 9;
            // 
            // SubstitutionInputForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1222, 335);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.SubstitutionCheckBox);
            this.Controls.Add(this.SubstitutionGrid);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.RateOverrideLabel);
            this.Controls.Add(this.RequestedQuantityLabel);
            this.Controls.Add(this.SecurityIdLabel);
            this.Controls.Add(this.RateOverrideText);
            this.Controls.Add(this.RequestedQuantityTextBox);
            this.Controls.Add(this.SecurityIdTextBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubstitutionInputForm";
            this.Padding = new System.Windows.Forms.Padding(1, 42, 1, 1);
            this.Text = "S3 - Input Control";
            this.Closed += new System.EventHandler(this.SubstitutionInputForm_Closed);
            this.Load += new System.EventHandler(this.SubstitutionInputForm_Load);
            this.DoubleClick += new System.EventHandler(this.SubstitutionInputForm_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestedQuantityTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateOverrideText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityIdLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestedQuantityLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateOverrideLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubstitutionGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public string SecId
        {
            set
            {
                if (!secId.Equals(value))
                {
                    secId = value;

                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    this.Cursor = Cursors.Default;
                }
            }

            get
            {
                return secId;
            }
        }

        private bool IsReady
        {
            get
            {
                return isReady;
            }

            set
            {
                try
                {
                    if (value && (substitutionEventArgsArray.Count > 0))
                    {
                        isReady = false;

                        substitutionEventHandler.BeginInvoke((SubstitutionActivityEventArgs)substitutionEventArgsArray[0], null, null);
                        substitutionEventArgsArray.RemoveAt(0);
                    }
                    else
                    {
                        isReady = value;
                    }
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [SubstitutionInputForm.IsReady(set)]", Log.Error, 1);
                }
            }
        }


        private void SubstitutionOnEvent(SubstitutionActivityEventArgs substitutionActivityEventArgs)
        {
            int i;


            i = substitutionEventArgsArray.Add(substitutionActivityEventArgs);
            Log.Write("Substitution event queued at " + i + " for deal ID: " + substitutionActivityEventArgs + " [SubstitutionInputForm.SubstitutionDoEvent]", 3);

            if (this.IsReady) // Force reset to trigger handling of event.
            {
                this.IsReady = true;
            }
        }


        private void SubstitutionDoEvent(SubstitutionActivityEventArgs substitutionActivityEventArgs)
        {
            try
            {
                Log.Write("Deal event being handled for deal ID: " + substitutionActivityEventArgs.ProcessId + " [SubstitutionInputForm.SubstitutionDoEvent]", 3);

                dataSet.Tables[0].BeginLoadData();

                substitutionActivityEventArgs.UtcOffset = this.mainForm.UtcOffset;

                dataSet.Tables[0].LoadDataRow(substitutionActivityEventArgs.Values, true);
                dataSet.Tables[0].EndLoadData();
                dataSet.Tables[0].AcceptChanges();

                this.IsReady = true;
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [SubstitutionInputForm.SubstitutionDoEvent]", Log.Error, 1);
            }
        }

        private void SubstitutionInputForm_Load(object sender, System.EventArgs e)
        {
            int height = 500;
            int width = 375;

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

            try
            {
                dataSet= mainForm.SubstitutionAgent.SubstitutionGet("","","",mainForm.UtcOffset);
                dataViewRowFilter = "ActUserId = '" + mainForm.UserId + "'";
                dataView = new DataView(dataSet.Tables["Substitutions"], dataViewRowFilter, "ActTime DESC", DataViewRowState.CurrentRows);

                SubstitutionGrid.SetDataBinding(dataView, "", true);
                
                mainForm.SubstitutionAgent.SubstitutionActivityEvent += new SubstitutionActivityEventHandler(substitutionEventWrapper.DoEvent);


                if (mainForm.ServiceAgent.IsSubstitutionActive())
                {
                    StatusTextBox.Text = "Online";                    
                    StatusTextBox.BackColor = Color.LightGreen;
                }
                else
                {
                    StatusTextBox.Text = "Offline";
                    StatusTextBox.BackColor = Color.PaleVioletRed;
                }

                this.IsReady = true;
            }
            catch (Exception error)
            {
                Log.Write(error.Message, Log.Error, 3);
            }

        }

        private void SubstitutionInputForm_Closed(object sender, System.EventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }
        }

        private void SubmitButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                mainForm.SubstitutionAgent.SubstitutionSet(
                    DateTime.UtcNow.ToString("ddHHmmssff"),
                    "0234",
                    SecurityIdTextBox.Text,
                    RequestedQuantityTextBox.Text,
                    RequestedQuantityTextBox.Text,
                    RateOverrideText.Text,
                    "",
                    "",
                    "",
                    ((SubstitutionCheckBox.Checked) ? "S" : "W"),
                    "S",
                    mainForm.UserId);
            }
            catch (Exception error)
            {
                Log.Write(error.Message, Log.Error, 3);
            }
        }

        private void SubstitutionGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            switch (e.Column.DataField)
            {
                case "RequestedQuantity":
                case "Quantity":
                case "SubstitutionQuantity":
                case "TotalQuantity":
                case "ExcessQuantity":
                case "PsrQuantity":
                    try
                    {
                        e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
                    }
                    catch { }
                    break;

                case "OverrideRate":
                    try
                    {
                        e.Value = decimal.Parse(e.Value.ToString()).ToString("##0.000");
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

        private void SubstitutionInputForm_DoubleClick(object sender, EventArgs e)
        {
            if (this.Dock != DockStyle.Top)
            {                
                this.Dock = DockStyle.Top;
                this.ControlBox = false;
            }
            else
            {
                this.Dock = DockStyle.None;
                this.ControlBox = true;
            }
        }

        private void SubstitutionGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (SubstitutionGrid[e.Row, "Status"].ToString().Equals("E"))
            {
                e.CellStyle.ForeColor = Color.LightGray;
            }
        }
    }
}
