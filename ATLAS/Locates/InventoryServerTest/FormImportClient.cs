using System;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Printing;

using C1.Win;
using C1.Win.C1FlexGrid;
using C1.C1DataExtender;

using StockLoan.Common;



namespace StockLoan.Inventory
{

    public delegate void ImportWorkerReadyDelegate();
    public delegate void ImportCompleteDelegate();
    public delegate void FormRefreshDelegate();
    public delegate void StatusUpdateDelegate(string StatusMessage);

    public partial class FormImportClient : Form
    {
        ImportMode modeImport = ImportMode.Batch;
        InventoryImportController importer;
        string strCellStyleLabelSuccess = "Success";
        string strCellStyleLabelFailure = "Failure";
        string strCellStyleLabelDisabled = "Disabled";

        string strPreviewFilePath = "";
        string strReportFilePath = "";

        bool bIsInitialized = false;

        StringCollection clnStrPreviewRows = new StringCollection();
        int nPreviewFirstMatch = 0;
        private Font fontPlainText = new Font("Times New Roman", 12);
        StockLoan.Inventory.StatusUpdateDelegate delegateUpdateStatus;

        // GUI Thread Event Handlers
        ImportWorkerReadyDelegate delegateImportWorkerReady;
        ImportCompleteDelegate delegateCompletedImport;
        FormRefreshDelegate delegateRefreshForm;


        public FormImportClient()
        {
            InitializeComponent();

            btnBeginImport.Enabled = true;

            C1.Win.C1FlexGrid.CellStyle styleSuccess = flexgridImport.Styles.Add(strCellStyleLabelSuccess);
            styleSuccess.BackColor = Color.LightGreen;
            styleSuccess.ForeColor = Color.Black;

            C1.Win.C1FlexGrid.CellStyle styleFailure = flexgridImport.Styles.Add(strCellStyleLabelFailure);
            styleFailure.Font = new Font(FontFamily.GenericSansSerif, (float)8.25, FontStyle.Bold);
            styleFailure.BackColor = Color.DarkRed;
            styleFailure.ForeColor = Color.White;

            C1.Win.C1FlexGrid.CellStyle styleDisabled = flexgridImport.Styles.Add(strCellStyleLabelDisabled);
            styleDisabled.BackColor = Color.Blue;
            styleDisabled.ForeColor = Color.Black;

            delegateUpdateStatus = new StatusUpdateDelegate(UpdateLocalStatusText);
            delegateCompletedImport = new ImportCompleteDelegate(FinishImport);
            delegateImportWorkerReady = new ImportWorkerReadyDelegate(PrepareFormToImport);
            delegateRefreshForm = new FormRefreshDelegate(RefreshDataGrids);

            // Open File Dialog
            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Filter = ("Text files (*.txt)|*.txt|Excel files (*.xls)|*.xls|All files (*.*)|*.* ");
            openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);

            //Open Form to get Window Handle
            this.Visible = true;

            tabdockImportPages.SelectedTab = tabpageImport;

            cbxImportMode.Items.Add("Batch");
            cbxImportMode.Items.Add("Service");
            cbxImportMode.SelectedItem = cbxImportMode.Items[0];
            cbxImportMode.SelectedIndexChanged += new System.EventHandler(this.cbxImportMode_SelectedIndexChanged);

            BuildImporter();
            RefreshDataGrids();

            IsInitialized = true;

        }



        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------


        public bool IsInitialized
        {
            get { return bIsInitialized; }
            set { bIsInitialized = value; }
        }

        public InventoryImportController InventoryImportEngine
        {
            get { return importer; }
            set { importer = value; }
        }


        internal void BuildImporter()
        {
            if (null != importer) { importer.Dispose(); }

            importer = new InventoryImportController(modeImport);
            importer.ReadyToImportEvent += new ReadyToImportEventHandler(importer_ReadyToImport);
            importer.ImportCompleteEvent += new StatusUpdateEventHandler(importer_ImportComplete);
            importer.StatusUpdateEvent += new StatusUpdateEventHandler(importer_StatusUpdate);
            importer.ModelUpdateEvent += new ModelUpdateEventHandler(importer_ModelUpdate);
            importer.InitializeComponents();
        }

        public void RefreshDataGrids()
        {
            try
            {
                if (importer.HasImportSiteSpecs)
                {
                    RefreshGridImport();
                    RefreshGridSubscriptions();
                    RefreshGridExecutions();
                    RefreshGridPatterns();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_RefreshDataGrids]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


        private void RefreshGridImport()
        {
            flexgridImport.DataSource = importer.SubscriptionSites;
            flexgridImport.AutoResize = true;

            flexgridImport.Cols["FileTime"].Format = "G";
            AutoSizeWidthToText(flexgridImport, "FileTime");

            flexgridImport.Cols["FileCheckTime"].Format = "G";
            AutoSizeWidthToText(flexgridImport, "FileCheckTime");

            flexgridImport.Cols["BizDate"].Format = "G";
            AutoSizeWidthToText(flexgridImport, "BizDate");

            flexgridImport.Refresh();
        }
        private void RefreshGridExecutions()
        {
            flexgridExecutions.DataSource = importer.ImportExecutions;
            flexgridExecutions.Refresh();
        }


        private void RefreshGridSubscriptions()
        {
            DataTable dtSubscriptions = importer.SubscriptionSites;
            gridviewSubscriptions.DataSource = dtSubscriptions;

            DataGridViewColumn colSubscriberID = gridviewSubscriptions.Columns["InventorySubscriberID"];
            colSubscriberID.ReadOnly = true;

            DataTable dtFilePatterns = importer.FilePatterns;
            if (!dtFilePatterns.Columns.Contains("FilePattern"))
            {
                DataColumn dc = new DataColumn("FilePattern");

                //dc.Expression = "HeaderRegEx + ' ; ' + DataRegEx";
                dc.Expression = "HeaderRegEx";
                dtFilePatterns.Columns.Add(dc);

                DataGridViewComboBoxColumn colFilePatterns = new DataGridViewComboBoxColumn();
                colFilePatterns.DataSource = dtFilePatterns;

                colFilePatterns.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                colFilePatterns.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                colFilePatterns.Resizable = DataGridViewTriState.True;
                colFilePatterns.Name = "File Pattern";
                colFilePatterns.HeaderText = "File Pattern";
                colFilePatterns.DisplayMember = "FilePattern";
                colFilePatterns.DataPropertyName = "InventoryFilePatternID";
                colFilePatterns.ValueMember = "InventoryFilePatternID";


                if (gridviewSubscriptions.Columns.Contains("InventoryFilePatternID"))
                {
                    //Remove Old TextBox
                    DataGridViewColumn colFilePatternID = gridviewSubscriptions.Columns["InventoryFilePatternID"];
                    int nColIndex = gridviewSubscriptions.Columns.IndexOf(colFilePatternID);
                    gridviewSubscriptions.Columns.Remove(colFilePatternID);
                    
                    // Add New ComboBox
                    gridviewSubscriptions.Columns.Insert(nColIndex, colFilePatterns);
                }

            }

            DataTable dtImportTypes = importer.SubscriptionTypes;
            if (!dtImportTypes.Columns.Contains("SubscriptionTypes"))
            {
                DataColumn dc = new DataColumn("SubscriptionTypes");
                dc.Expression = "InventorySubscriptionTypeID + ', ' + InventorySubscriptionTypeName";
                dtImportTypes.Columns.Add(dc);

                DataGridViewComboBoxColumn colSubscriptionTypes = new DataGridViewComboBoxColumn();
                colSubscriptionTypes.DataSource = dtImportTypes;

                colSubscriptionTypes.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                colSubscriptionTypes.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                colSubscriptionTypes.Resizable = DataGridViewTriState.True;
                colSubscriptionTypes.Name = "Import Type";
                colSubscriptionTypes.HeaderText = "Import Type";
                colSubscriptionTypes.DisplayMember = "SubscriptionTypes";
                colSubscriptionTypes.DataPropertyName = "SubscriptionTypeID";
                colSubscriptionTypes.ValueMember = "InventorySubscriptionTypeID";


                if (gridviewSubscriptions.Columns.Contains("SubscriptionTypeID"))
                {
                    //Remove Old TextBox
                    DataGridViewColumn colSubscriptionTypeID = gridviewSubscriptions.Columns["SubscriptionTypeID"];
                    int nColIndex = gridviewSubscriptions.Columns.IndexOf(colSubscriptionTypeID);
                    gridviewSubscriptions.Columns.Remove(colSubscriptionTypeID);

                    // Add New ComboBox
                    gridviewSubscriptions.Columns.Insert(nColIndex, colSubscriptionTypes);
                }
            }

            DataTable dtImportDesks = importer.ImportDesks;
            if (!gridviewSubscriptions.Columns.Contains("Import Desk"))
            {
                DataGridViewComboBoxColumn colImportDesks = new DataGridViewComboBoxColumn();

                colImportDesks.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                colImportDesks.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                colImportDesks.Resizable = DataGridViewTriState.True;
                colImportDesks.Name = "Import Desk";
                colImportDesks.DataSource = dtImportDesks;
                colImportDesks.HeaderText = "Import Desk";
                colImportDesks.DisplayMember = "Desk";
                colImportDesks.DataPropertyName = "Desk";
                colImportDesks.ValueMember = "Desk";

                if (gridviewSubscriptions.Columns.Contains("Desk"))
                {
                    //Remove Old TextBox
                    DataGridViewColumn colDesk = gridviewSubscriptions.Columns["Desk"];
                    int nColIndex = gridviewSubscriptions.Columns.IndexOf(colDesk);
                    gridviewSubscriptions.Columns.Remove(colDesk);

                    // Add New ComboBox
                    gridviewSubscriptions.Columns.Insert(nColIndex, colImportDesks);
                }
            }

            AutoSizeWidthToText(gridviewSubscriptions, "FileTime");
            AutoSizeWidthToText(gridviewSubscriptions, "FileCheckTime");

            gridviewSubscriptions.Refresh();
        }


        private void RefreshGridPatterns()
        {
            gridviewPatterns.DataSource = importer.FilePatterns;

            DataTable dtImportDesks = importer.ImportDesks;
            if (!gridviewPatterns.Columns.Contains("Import Desk"))
            {
                DataGridViewComboBoxColumn colImportDesks = new DataGridViewComboBoxColumn();

                colImportDesks.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                colImportDesks.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                colImportDesks.Resizable = DataGridViewTriState.True;
                colImportDesks.Name = "Import Desk";
                colImportDesks.DataSource = dtImportDesks;
                colImportDesks.HeaderText = "Import Desk";
                colImportDesks.DisplayMember = "Desk";
                colImportDesks.DataPropertyName = "Desk";
                colImportDesks.ValueMember = "Desk";


                if (gridviewPatterns.Columns.Contains("Desk"))
                {
                    //Remove Old TextBox
                    DataGridViewColumn colDesk = gridviewPatterns.Columns["Desk"];
                    int nColIndex = gridviewPatterns.Columns.IndexOf(colDesk);
                    gridviewPatterns.Columns.Remove(colDesk);

                    // Add New ComboBox
                    gridviewPatterns.Columns.Insert(nColIndex, colImportDesks);
                }
                if (gridviewPatterns.Columns.Contains("FilePattern"))
                {
                    DataGridViewColumn colFilePattern = gridviewPatterns.Columns["FilePattern"];
                    gridviewPatterns.Columns.Remove(colFilePattern);
                }
            }

            DataGridViewColumn colPatternID = gridviewPatterns.Columns["InventoryFilePatternID"];
            colPatternID.ReadOnly = true;
            colPatternID.HeaderText = "Pattern ID";

            AutoSizeWidthToText(gridviewPatterns, "Import Desk");
            AutoSizeWidthToText(gridviewPatterns, "InventoryFilePatternID");
            AutoSizeWidthToText(gridviewPatterns, "HeaderRegEx");
            AutoSizeWidthToText(gridviewPatterns, "DataRegEx");
            AutoSizeWidthToText(gridviewPatterns, "TrailerRegEx");
            AutoSizeWidthToText(gridviewPatterns, "AccountRegEx");
            AutoSizeWidthToText(gridviewPatterns, "DateRegEx");
            AutoSizeWidthToText(gridviewPatterns, "RowCountRegEx");

            gridviewPatterns.Refresh();
        }



        private void ImportData(Object stateInfo)
        {
            try
            {
                if (null != importer)
                {
                    importer.StartImports();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_CreateImporter]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        void importer_ModelUpdate()
        {
            try
            {
                this.Invoke(delegateRefreshForm);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_OnModelUpdate]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        void importer_ReadyToImport(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(delegateImportWorkerReady);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_OnModelReadyToImport]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }
        void importer_ImportComplete(object sender, StatusChangedEventArgs e)
        {
            try
            {
                this.Invoke(delegateCompletedImport);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_OnModelImportComplete]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        void importer_StatusUpdate(object sender, StatusChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Message))
                {
                    string strNewStatusMessage = e.Message;
                    if (null != delegateUpdateStatus)
                    {
                        this.Invoke(delegateUpdateStatus, strNewStatusMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_OnModelStatusUpdate]", Log.Error, 1);
                Log.Write(ex.StackTrace, Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        private void PrepareFormToImport()
        {
            RefreshDataGrids();
            btnBeginImport.Enabled = true;
            btnCancelImport.Enabled = false;
        }

        private void FinishImport()
        {
            try
            {
                btnCancelImport.Enabled = false;
                if ((null != importer) && (importer.HasImportSiteSpecs))
                {
                    RefreshDataGrids();
                }

                cbxImportMode.Enabled = true;
                btnBeginImport.Enabled = true;
                btnPause.Enabled = false;
                btnResume.Enabled = false;
                btnApply.Enabled = true;

                SetDateText();
                SetCellStyles();

                UpdateLocalStatusText("Import Processing Complete.");

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_FinishImport]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdateLocalStatusText(string StatusMessage)
        {
            try
            {
                statuslabelCurrentState.Text = StatusMessage;
                statusbarCurrentState.Refresh();
                Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [Master.Master]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }
        private void AutoSizeWidthToText(C1FlexGrid FlexGrid, string ColumnName)
        {
            if (null != flexgridImport.Cols[ColumnName].UserData)
            {
                FlexGrid.Cols[ColumnName].Width = ((string)FlexGrid.Cols[ColumnName].UserData).Length * (int)FlexGrid.Cols[ColumnName].Style.Font.SizeInPoints;
            }
        }
        private void AutoSizeWidthToText(DataGridView GridView, string ColumnName)
        {
            GridView.Columns[ColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }


        private void flexgridSubscriptions_CellChanged(object sender, RowColEventArgs e)
        {
            C1FlexGrid gridDisplay = (C1FlexGrid)sender;
            if ((0 < e.Row) && (0 < e.Col))
            {
                string strFieldName = gridDisplay.Cols[e.Col].Name;
                long nPrimaryKey = (long)gridDisplay.Rows[e.Row]["InventorySubscriberID"];
                object oValue = gridDisplay.GetData(e.Row, e.Col);

                importer.ChangeImportSpecData(nPrimaryKey, strFieldName, oValue);
            }
        }

        private void btnGetImportFiles_Click(object sender, EventArgs e)
        {
            try
            {
                cbxImportMode.Enabled = false;
                btnApply.Enabled = false;
                if (null == importer)
                {
                    BuildImporter();
                    RefreshDataGrids();
                }
                if ((null != importer) && (importer.HasImportSiteSpecs))
                {
                    btnBeginImport.Enabled = false;
                    btnCancelImport.Enabled = true;
                    btnPause.Enabled = true;
                    RefreshDataGrids();

                    InventoryImportController.ContinueServiceLoop = true;
                    InventoryImportController.ImportNextSubscriber.Set();

                    try
                    {
                        WaitCallback wcImport = new WaitCallback(ImportData);
                        System.Threading.ThreadPool.QueueUserWorkItem(wcImport);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_Constructor]", Log.Error, 1);
                        Console.WriteLine(ex.Message);
                        MessageBox.Show(this, ex.Message, "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_btnGetImportFiles_Click]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }
        private void btnCancelImport_Click(object sender, EventArgs e)
        {
            try
            {
                importer.StopImports();
                Log.Write(" Import Cancelled by User Control. Waiting on Service Loop to Close; ", Log.Information, 3);

                btnBeginImport.Enabled = true;
                btnCancelImport.Enabled = false;

                importer.Dispose();
                importer = null;

                cbxImportMode.Enabled = true;
                btnApply.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_btnCancelImport_Click]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            importer.SaveImportChanges();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            importer.PauseImports();
            btnResume.Enabled = true;
            btnPause.Enabled = false;
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            importer.ResumeImports();
            btnResume.Enabled = false;
            btnPause.Enabled = true;
        }

        private void cbxImportMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxImportMode.SelectedItem.ToString())
            {
                case "Service":
                    modeImport = ImportMode.Test;
                    break;
                case "Batch":
                    modeImport = ImportMode.Batch;
                    break;
                default:
                    modeImport = ImportMode.Test;
                    break;
            }

            BuildImporter();
            RefreshDataGrids();
        }


        private void SetCellStyles()
        {
            try
            {
                for (int iRow = 1; iRow < flexgridImport.Rows.Count; iRow++)
                {

                    // Determine Cells Style
                    string strCellStyleName = "";
                    bool bIsActive = (bool)flexgridImport.GetData(iRow, "IsEnabled");
                    if (bIsActive)
                    {
                        string strFileStatus = flexgridImport.GetData(iRow, "FileStatus").ToString();
                        if ((0 < strFileStatus.Length) && ("OK" == strFileStatus.Substring(0, 2)))
                        {
                            strCellStyleName = strCellStyleLabelSuccess;
                        }
                        else
                        {
                            strCellStyleName = strCellStyleLabelFailure;
                        }
                    }
                    else
                    {
                        strCellStyleName = strCellStyleLabelDisabled;
                    }

                    // Set Cells Style
                    for (int iCol = 0; iCol < flexgridImport.Cols.Count; iCol++)
                    {
                        flexgridImport.SetCellStyle(iRow, iCol, strCellStyleName);
                    }
                }

                flexgridImport.Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_SetCellStyles]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


        private void SetDateText()
        {
            try
            {
                for (int iRow = 1; iRow < flexgridImport.Rows.Count; iRow++)
                {
                    object objFilePathName = flexgridImport.GetData(iRow, "FilePathName");
                    if ("System.String" == objFilePathName.GetType().ToString())
                    {
                        string strFilePathName = (string)objFilePathName;
                        bool bIsBizDatePrior = (bool)flexgridImport.GetData(iRow, "IsBizDatePrior");
                        strFilePathName = InventoryImportController.ParseImportFileDate(strFilePathName, bIsBizDatePrior);

                        flexgridImport.SetData(iRow, "FilePathName", strFilePathName);
                    }
                }

                flexgridImport.Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.FormImportClient_SetCellStyles]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        private void tabpageSubscriptions_Enter(object sender, EventArgs e)
        {
            AssertApplyButton();
        }
        private void tabpageSubscriptions_Leave(object sender, EventArgs e)
        {
            AssertApplyButton();
        }

        private void tabpagePatterns_Enter(object sender, EventArgs e)
        {
            AssertApplyButton();
        }
        private void tabpagePatterns_Leave(object sender, EventArgs e)
        {
            AssertApplyButton();
        }

        private void tabdockImportPages_Enter(object sender, EventArgs e)
        {
            AssertApplyButton();
        }
        private void tabdockImportPages_Leave(object sender, EventArgs e)
        {
            AssertApplyButton();
        }

        private void AssertApplyButton()
        {
            if ((tabdockImportPages.SelectedTab == tabpageSubscriptions) || (tabdockImportPages.SelectedTab == tabpagePatterns))
            {
                btnApply.Visible = true;
            }
            else
            {
                btnApply.Visible = false;
            }
        }

        private void flexgridFilePatterns_CellChanged(object sender, RowColEventArgs e)
        {
            try
            {
                C1FlexGrid gridDisplay = (C1FlexGrid)sender;
                if ((0 < e.Row) && (0 < e.Col))
                {
                    string strFieldName = gridDisplay.Cols[e.Col].Name;
                    long nPrimaryKey = (long)gridDisplay.Rows[e.Row]["InventoryFilePatternID"];
                    object oValue = gridDisplay.GetData(e.Row, e.Col);

                    importer.ChangeFilePatternData(nPrimaryKey, strFieldName, oValue);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.flexgridFilePatterns_CellChanged]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            strPreviewFilePath = openFileDialog1.FileName;
            FileInfo infoPreviewFile = new FileInfo(strPreviewFilePath);
            string strFileName = infoPreviewFile.Name.Replace(infoPreviewFile.Extension, ".html");

            strReportFilePath = infoPreviewFile.Directory + @"\" + strFileName;

            PreviewFile();
        }


        private void PreviewFile()
        {
            try
            {
                if (!string.IsNullOrEmpty(strPreviewFilePath))
                {
                    FileInfo infoPreviewFile = new FileInfo(strPreviewFilePath);
                    if (infoPreviewFile.Exists)
                    {
                        if (".xls" == infoPreviewFile.Extension)
                        {
                            clnStrPreviewRows.AddRange(ExcelClient.ReadWorkbook(strPreviewFilePath, false));
                        }
                        else
                        {
                            StreamReader sr = File.OpenText(strPreviewFilePath);
                            while (!sr.EndOfStream)
                            {
                                clnStrPreviewRows.Add(sr.ReadLine());
                            }
                        }

                        bool bWriteHTML = true;
                        string strPreviewHTML = GenerateHTML(bWriteHTML);

                        Uri uriPreview = new Uri(strReportFilePath);
                        browserPreview.Url = uriPreview;
                        browserPreview.Refresh();
                    }
                    else
                    {
                        Console.WriteLine("{0} does not exist.", strPreviewFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.PreviewFile]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        private void FormImportClient_Load(object sender, EventArgs e)
        {

        }


        public string GenerateHTML(bool OutputToFile)
        {
            string strReturnHTML = "<HTML>";
            try
            {
                bool bPerformPatternMatch = false;
                Regex rgxPreviewPattern = null;
                if (!string.IsNullOrEmpty(tbPatternPreview.Text))
                {
                    rgxPreviewPattern = new Regex(tbPatternPreview.Text);
                    bPerformPatternMatch = true;
                }

                for (int iTextRow = 0; iTextRow < clnStrPreviewRows.Count; iTextRow++)
                {
                    string strTextRow = clnStrPreviewRows[iTextRow];
                    Color colorNextMatch = Color.Blue;

                    if (bPerformPatternMatch)
                    {
                        if (!rgxPreviewPattern.IsMatch(strTextRow))
                        {
                            strReturnHTML += TextWithLineBreak(strTextRow);
                        }
                        else
                        {
                            if (0 == nPreviewFirstMatch) { nPreviewFirstMatch = iTextRow; }

                            Match matchPreview = rgxPreviewPattern.Match(strTextRow);

                            int nStringEnd = strTextRow.Length - 1;


                            string strFormattedRow = "";
                            colorNextMatch = NextColor(colorNextMatch);
                            string strRGB = ColorToRGB(colorNextMatch);

                            if (1 < matchPreview.Groups.Count)
                            {
                                int nMatchesBegin = (matchPreview.Groups[1]).Index;
                                string strBeforeMatch = strTextRow.Substring(0, nMatchesBegin);
                                strFormattedRow += strBeforeMatch;

                                for (int iMatch = 1; iMatch < matchPreview.Groups.Count; iMatch++)
                                {
                                    string strMatch = matchPreview.Groups[iMatch].Value;

                                    int nMatchBegin = matchPreview.Groups[iMatch].Index;

                                    //ToDo: Check Index Math (-1)
                                    int nMatchEnd = (nMatchBegin + (matchPreview.Groups[iMatch].Length));

                                    string strAfterMatch = "";
                                    if (nMatchEnd != nStringEnd)
                                    {
                                        bool bIsLastMatchOnRow = (iMatch == (matchPreview.Groups.Count - 1));
                                        if (bIsLastMatchOnRow)
                                        {
                                            strAfterMatch = strTextRow.Substring(nMatchEnd, (nStringEnd - (nMatchEnd - 1)));
                                        }
                                        else
                                        {
                                            int nNextMatchBeginsAt = (matchPreview.Groups[iMatch + 1]).Index;
                                            strAfterMatch = strTextRow.Substring(nMatchEnd, (nNextMatchBeginsAt - nMatchEnd));
                                        }
                                    }

                                    colorNextMatch = NextColor(colorNextMatch);
                                    strRGB = ColorToRGB(colorNextMatch);

                                    string strFormattedMatch = string.Format("<font style=\"color:rgb{0}\"><b>{1}</b></font>{2}", strRGB, strMatch, strAfterMatch);
                                    strFormattedRow += strFormattedMatch;
                                }
                            }
                            else
                            {
                                int nMatchBegin = matchPreview.Index;

                                //ToDo: Check Index Math (-1)
                                int nMatchEnd = (nMatchBegin + matchPreview.Length);
                                if (nMatchEnd == strTextRow.Length ){ nMatchEnd--; }

                                string strMatch = matchPreview.Value;
                                string strAfterMatch = strTextRow.Substring(nMatchEnd, (strTextRow.Length - nMatchEnd));
                                string strBeforeMatch = strTextRow.Substring(0, nMatchBegin);
                                strFormattedRow += strBeforeMatch;

                                string strFormattedMatch = string.Format("<font style=\"color:rgb{0}\"><b>{1}</b></font>{2}", strRGB, strMatch, strAfterMatch);
                                strFormattedRow += strFormattedMatch;
                            }

                            strReturnHTML += TextWithLineBreak(strFormattedRow);
                        }

                    }
                    else
                    {
                        strReturnHTML += TextWithLineBreak(strTextRow);
                    }
                }

                strReturnHTML += "</HTML>";

                if (OutputToFile)
                {
                    StreamWriter writerOutputText = new StreamWriter(strReportFilePath, false);
                    writerOutputText.Write(strReturnHTML);
                    writerOutputText.Flush();
                    writerOutputText.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.GenerateHTML]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strReturnHTML;
        }

        private string TextWithLineBreak(string RowText)
        {
            string strReturnFormattedRow = "";
            int nRowEndIndex = RowText.Length - 1;
            if (RowText.Contains(Environment.NewLine))
            {
                // strReturnFormattedRow = RowText.Replace(Environment.NewLine, ("<br>" + Environment.NewLine));
                strReturnFormattedRow = RowText.Replace(Environment.NewLine, ("<br>"));
            }
            else
            {
                //strReturnFormattedRow = (RowText + ("<br> " + Environment.NewLine));
                strReturnFormattedRow = (RowText + "<br>");
            }
            return strReturnFormattedRow;
        }




        private void tbPatternPreview_Leave(object sender, EventArgs e)
        {

        }

        private void tbPatternPreview_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PreviewFile();
            }
        }

        private string ColorToRGB(Color color)
        {
            return string.Format("({0},{1},{2})", color.R, color.G, color.B);
        }
        private Color NextColor(Color color)
        {
            Color colorReturn;
            switch (color.Name.ToUpper())
            {
                case "RED":
                    colorReturn = Color.DarkGreen;
                    break;
                case "DARKGREEN":
                    colorReturn = Color.Blue;
                    break;
                case "BLUE":
                    colorReturn = Color.Red;
                    break;
                default:
                    colorReturn = Color.Orange;
                    break;
            }
            return colorReturn;
        }


        private void gridviewPatterns_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void gridviewPatterns_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
        }

        private void gridviewPatterns_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (IsInitialized)
                {
                    DataGridView gridDisplay = (DataGridView)sender;
                    if ((0 < e.RowIndex) && (0 < e.ColumnIndex))
                    {
                        DataGridViewRow CurrentRow = gridDisplay.Rows[e.RowIndex];

                        if ("System.DBNull" == CurrentRow.Cells["InventoryFilePatternID"].Value.GetType().ToString())
                        {
                            string strDesk = (string)CurrentRow.Cells["Import Desk"].Value;

                            if (string.IsNullOrEmpty(strDesk))
                            {
                                MessageBox.Show("Please First Select a Desk to which this File Pattern will Belong.");
                            }
                            else
                            {
                                importer.GenerateFilePattern(strDesk);
                                IsInitialized = false;
                                RefreshDataGrids();
                                IsInitialized = true;
                            }
                            int nIndex = CurrentRow.Cells["Import Desk"].RowIndex;
                            gridviewPatterns.Select();
                            CurrentRow.Cells[1 + nIndex].Selected = true;
                        }
                        else
                        {
                            string strFieldName = gridDisplay.Columns[e.ColumnIndex].Name;
                            long nPrimaryKey = (long)gridDisplay.Rows[e.RowIndex].Cells["InventoryFilePatternID"].Value;
                            object oValue = gridDisplay.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                            importer.ChangeFilePatternData(nPrimaryKey, strFieldName, oValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryServiceTest.flexgridFilePatterns_CellChanged]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }




    }



}

