using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;

using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;



namespace StockLoan.Inventory
{
    public enum ImportMode { Batch, Service, Test };

    public delegate void ReadyToImportEventHandler(object sender, EventArgs e);
    public delegate void StatusUpdateEventHandler(object sender, StatusChangedEventArgs e);
    public delegate void ImportErrorEventHandler(object sender, ImportErrorEventArgs e);

    public delegate void ModelUpdateEventHandler();
    public delegate void ModelErrorEventHandler(object sender, ModelErrorEventArgs e);


    /// <summary>
    /// Primary Controller for Running Imports
    /// </summary>
    public class InventoryImportController
    {

        #region Members

        private ImportMode enumModeImport;

        private InventoryImportModel modelImportSpec;
        private InventoryImportViewEngine engineImportViews;
        private InventoryImportMessageEngine engineImportMessages;
        private ExchangeClient clientExchange;
        private InventoryDataParser parserInventoryData;

        private static ImportSpec specActiveImport;
        private static bool bRefuseImportOldDates = true;
        private static bool bContinueServiceLoop = true;

        private bool bIsPaused = false;

        private string strLoggerLevel = "1";
        private string strLoggerDir = @"C:\";



        #endregion


        //---------------------------------------------------------------------
        #region Public & Static Properties

        public static RegexOptions RegExOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled;

        public static Mutex mtxImportDataEntry = new Mutex(false, "InventoryDataEntry");
        public static Mutex mtxImportFTP = new Mutex(false, "InventoryImportFTP");
        public static Mutex mtxContinueServiceLoop = new Mutex(false, "ContinueServiceLoop");

        private static ManualResetEvent syncImportNextSubscriber = new ManualResetEvent(false);
        private static AutoResetEvent syncServiceStop = new AutoResetEvent(false);

        public static ManualResetEvent ImportNextSubscriber
        {
            get { return InventoryImportController.syncImportNextSubscriber; }
            set { InventoryImportController.syncImportNextSubscriber = value; }
        }
        public static AutoResetEvent ServiceStop
        {
            get { return InventoryImportController.syncServiceStop; }
            set { InventoryImportController.syncServiceStop = value; }
        }


        public static ImportSpec ActiveImportSpec
        {
            get { return InventoryImportController.specActiveImport; }
            set { InventoryImportController.specActiveImport = value; }
        }
        public static bool ContinueServiceLoop
        {
            get
            {
                bool bReturn;
                mtxContinueServiceLoop.WaitOne();
                bReturn = bContinueServiceLoop;
                mtxContinueServiceLoop.ReleaseMutex();
                return bReturn;
            }
            set
            {
                try
                {
                    mtxContinueServiceLoop.WaitOne();
                    bContinueServiceLoop = value;
                    Log.Write("ContinueServiceLoop_Set: " + bContinueServiceLoop.ToString(), Log.Information, 3);
                    if ((false == bContinueServiceLoop) && (null != specActiveImport) && (specActiveImport.IsRunning))
                    {
                        specActiveImport.IsRunning = false;
                        specActiveImport.ImportStatus = "Import Canceled !!!";
                        Log.Write("Import Cancelled ID:" + specActiveImport.SubscriberID.ToString(), Log.Information, 3);
                    }
                }
                catch
                {
                }
                finally
                {
                    mtxContinueServiceLoop.ReleaseMutex();
                }
            }
        }
        public bool IsPaused
        {
            get { return bIsPaused; }
        }
        public bool HasImportSiteSpecs
        {
            get { return modelImportSpec.HasImportSiteSpecs; }
        }
        public DataTable SubscriptionSites
        {
            get { return engineImportViews.SubscriptionSites; }
        }
        public DataTable ImportExecutions
        {
            get { return engineImportViews.ImportExecutions; }
        }
        public DataTable FilePatterns
        {
            get { return engineImportViews.FilePatterns; }
        }
        public static bool RefuseImportOldDates
        {
            get { return bRefuseImportOldDates; }
            set { bRefuseImportOldDates = value; }
        }
        public string SubscriptionSitesTableName
        {
            get { return modelImportSpec.SubscriptionSitesTableName; }
        }
        public string LoggerLevel
        {
            get { return strLoggerLevel; }
            set { strLoggerLevel = value; }
        }

        public string LoggerDir
        {
            get { return strLoggerDir; }
            set { strLoggerDir = value; }
        }

        #endregion


        //---------------------------------------------------------------------
        #region Delegates And Events

        public event ReadyToImportEventHandler ReadyToImportEvent;
        public event StatusUpdateEventHandler StatusUpdateEvent;
        public event StatusUpdateEventHandler ImportCompleteEvent;
        public event ModelUpdateEventHandler ModelUpdateEvent;


        public void RaiseReadyToImportEvent(object sender, EventArgs e)
        {
            if (null != ReadyToImportEvent)
            {
                ReadyToImportEvent(sender, e);
            }
        }
        public void RaiseStatusUpdateEvent(object sender, StatusChangedEventArgs e)
        {
            if (null != StatusUpdateEvent)
            {
                StatusUpdateEvent(sender, e);
            }
        }
        public void RaiseImportCompleteEvent(object sender, StatusChangedEventArgs e)
        {
            if (null != ImportCompleteEvent)
            {
                ImportCompleteEvent(sender, e);
            }
        }
        public void RaiseModelUpdateEvent()
        {
            if (null != ModelUpdateEvent)
            {
                ModelUpdateEvent();
            }
        }


        #endregion


        //---------------------------------------------------------------------
        #region Constructor and Destructor
        /// <summary>
        /// Constructs an Inventory Controller
        /// </summary>
        /// <param name="Mode"> 
        /// Batch Mode runs all Imports Once
        /// Service Mode Continually Runs Each Import In Sequence
        /// Test Mode Allows the GUI to Simulate the Service
        /// </param>
        public InventoryImportController(ImportMode Mode)
        {
            enumModeImport = Mode;

            switch (enumModeImport)
            {
                case ImportMode.Batch:
                    break;
                case ImportMode.Service:
                    InitializeComponents();
                    //ImportSubscriptionData();
                    break;
                case ImportMode.Test:
                    break;
                default:
                    break;
            }
        }

        public void Dispose()
        {
            Log.Write("Dispose Called; ContinueServiceLoop= " + bContinueServiceLoop.ToString(), Log.Information, 3);

            bContinueServiceLoop = false;
            syncImportNextSubscriber.Reset();

            engineImportViews.Dispose();
            modelImportSpec.Dispose();
            Log.Write("Dispose Finished; ContinueServiceLoop= " + bContinueServiceLoop.ToString(), Log.Information, 3);

            ServiceStop.Set();
        }

        #endregion


        //---------------------------------------------------------------------
        #region Public Methods and Properties

        public void StartImports()
        {
            InventoryImportController.ContinueServiceLoop = true;
            if (ImportMode.Service == enumModeImport || ImportMode.Test == enumModeImport)
            {
                InventoryImportController.ImportNextSubscriber.Set();
            }
            ImportSubscriptionData();
        }
        public void PauseImports()
        {
            InventoryImportController.ImportNextSubscriber.Reset();
            bIsPaused = true;
        }
        public void ResumeImports()
        {
            InventoryImportController.ImportNextSubscriber.Set();
            bIsPaused = false;
        }
        public void StopImports()
        {
            ContinueServiceLoop = false;
            if (bIsPaused)
            {
                InventoryImportController.ImportNextSubscriber.Set();
            }
        }

        public void SaveImportChanges()
        {
            modelImportSpec.UpdateImportSpecs();
        }

        /// <summary>
        /// Modifies an ImportSpec from Outside the Controller
        /// So User Can Change Import Settings From the GUI
        /// </summary>
        /// <param name="RecordKey"></param>
        /// <param name="FieldName"></param>
        /// <param name="Value"></param>
        public void ChangeImportSpecData(long RecordKey, string FieldName, object Value)
        {
            DataTable tableSource = modelImportSpec.ImportDataModel.Tables[SubscriptionSitesTableName];
            DataRow rowSource = tableSource.Rows.Find(RecordKey);
            rowSource[FieldName] = Value;
        }

        public void InitializeComponents()
        {
            try
            {
                if ((null == modelImportSpec) || (!modelImportSpec.HasImportSiteSpecs))
                {
                    RefuseImportOldDates = bool.Parse(StockLoan.Common.Standard.ConfigValue("RefuseImportOldDates"));

                    LoggerDir = StockLoan.Common.Standard.ConfigValue("InventoryLocalDestination");
                    LoggerLevel = StockLoan.Common.Standard.ConfigValue("LoggerLevel");

                    Log.Level = LoggerLevel;
                    Log.FilePath = LoggerDir;
                    Log.Name = Assembly.GetExecutingAssembly().GetName().Name;

                    CreateNewMessageEngine();
                    CreateNewExchangeClient();
                    CreateNewImportModel();

                    if (modelImportSpec.HasImportSiteSpecs)
                    {
                        CreateNewViewEngine();
                        CreateNewDataParser();

                        UpdateStatusMessage("Ready to Import");
                        RaiseReadyToImportEvent(this, new EventArgs());
                    }
                    else
                    {
                        //UpdateStatusMessage("Error Initializing Import Controller. Please Contact Technical Support.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.InitializeComponents]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }

        }



        /// <summary>
        /// Primary Worker Method for Starting and Running an Import
        /// </summary>
        public void ImportSubscriptionData()
        {
            try
            {
                switch (enumModeImport)
                {
                    case ImportMode.Batch:
                        ImportBatchSubscriptionData();
                        break;
                    case ImportMode.Service:
                        ContinueServiceLoop = true;
                        ImportServiceSubscriptionData();
                        break;
                    case ImportMode.Test:
                        ContinueServiceLoop = true;
                        ImportServiceSubscriptionData();
                        break;
                    default:
                        break;
                }
                Log.Write("ImportSubscriptionData Now Complete. Setting syncServiceStop", Log.Information, 3);
                ServiceStop.Set();

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.ImportSubscriptionData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        private void ImportBatchSubscriptionData()
        {
            try
            {
                if (HasImportSiteSpecs)
                {
                    ImportFTPData();
                    ImportEmailBodyData();
                    ImportEmailAttachmentData();
                    SendSupportEmail("Inventory Data Import Complete.");
                    FinishImport();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.ImportBatchSubscriptionData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }
        private void ImportServiceSubscriptionData()
        {
            try
            {
                Log.Write("Starting ImportServiceSubscriptionData " + " [InventoryImportController.ImportServiceSubscriptionData]", Log.Information, 3);

                while (ContinueServiceLoop && HasImportSiteSpecs)
                {
                    ImportFTPData();
                    ImportEmailBodyData();
                    ImportEmailAttachmentData();

                    WaitThrottle();
                    syncImportNextSubscriber.WaitOne();

                    if (ContinueServiceLoop)
                    {
                        CreateNewImportModel();
                        CreateNewViewEngine();
                        RaiseModelUpdateEvent();
                    }
                }

                modelImportSpec.UpdateImportSpecs();
                FinishImport();
                Log.Write("Finishing ImportServiceSubscriptionData " + " [InventoryImportController.ImportServiceSubscriptionData]", Log.Information, 3);

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.ImportServiceSubscriptionData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }



        #endregion


        //---------------------------------------------------------------------
        #region Internal Methods

        internal void CreateNewImportModel()
        {
            //InventoryImportModel.mtxImportModel.WaitOne();
            try
            {
                if (null != modelImportSpec) { modelImportSpec.Dispose(); }

                UpdateStatusMessage("Building Import Model");
                modelImportSpec = new InventoryImportModel(enumModeImport);
                modelImportSpec.StatusUpdate += new InventoryImportModel.StatusUpdateEventHandler(modelImportSpec_StatusUpdate);
                modelImportSpec.ModelError += new ModelErrorEventHandler(modelImportSpec_ModelError);
                modelImportSpec.InitializeComponents();
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.CreateNewImportModel]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //InventoryImportModel.mtxImportModel.ReleaseMutex();
            }

        }
        internal void CreateNewViewEngine()
        {
            if (null != engineImportViews) { engineImportViews.Dispose(); }
            UpdateStatusMessage("Building Import View Engine");
            engineImportViews = new InventoryImportViewEngine(modelImportSpec);
        }

        internal void CreateNewMessageEngine()
        {
            UpdateStatusMessage("Building Import Message Engine");
            engineImportMessages = new InventoryImportMessageEngine(this);
        }

        internal void CreateNewDataParser()
        {
            UpdateStatusMessage("Building Import Data Parser");
            parserInventoryData = new InventoryDataParser();
            parserInventoryData.ImportErrorEvent += new ImportErrorEventHandler(parserInventoryData_ImportError);
        }

        internal void CreateNewExchangeClient()
        {
            UpdateStatusMessage("Building Exchange Client");
            clientExchange = new ExchangeClient();
            clientExchange.StatusUpdateEvent += new ExchangeClient.StatusUpdateEventHandler(clientExchange_StatusUpdate);
            clientExchange.StartImportEvent += new ExchangeClient.StatusUpdateEventHandler(clientExchange_StartImport);
        }

        internal void WaitThrottle()
        {
            DateTime dtToday = DateTime.Now.Date;
            TimeSpan timeWait = TimeSpan.Parse("00:00:10");

            int nWaitMilliseconds = 5000;
            if (dtToday == InventoryImportModel.BizDate)
            {
                timeWait = InventoryImportModel.WaitTimeBizDate;
                nWaitMilliseconds = (int)timeWait.TotalMilliseconds;

            }
            else
            {
                timeWait = InventoryImportModel.WaitTimeNonBizDate;
                nWaitMilliseconds = (int)timeWait.TotalMilliseconds;
            }

            Thread.Sleep(nWaitMilliseconds);
        }

        internal void FinishImport()
        {
            try
            {
                StatusChangedEventArgs NewStatus = new StatusChangedEventArgs("OK");
                RaiseImportCompleteEvent(this, NewStatus);
                RaiseReadyToImportEvent(this, new EventArgs());
                Log.Write("ImportController is Finished Importing." + " [InventoryImportController.FinishImport]", Log.Information, 3);

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.FinishImport]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        internal void modelImportSpec_ModelError(object sender, ModelErrorEventArgs e)
        {
            UpdateStatusMessage("Error!: " + e.Exception.Message);
            SendErrorEmail(engineImportMessages.GenerateErrorMessage(e));
        }

        internal void modelImportSpec_StatusUpdate(object sender, StatusChangedEventArgs e)
        {
            RaiseStatusUpdateEvent(this, e);
        }

        internal void parserInventoryData_ImportError(object sender, ImportErrorEventArgs e)
        {
            SendErrorEmail(engineImportMessages.GenerateErrorMessage(e.ImportSpecification, e.Exception));
        }

        internal void clientExchange_StartImport(object sender, StatusChangedEventArgs e)
        {
            if (null != e.ImportSpecification)
            {
                GenerateImportExecution(e.ImportSpecification);
            }
        }
        internal void clientExchange_StatusUpdate(object sender, StatusChangedEventArgs e)
        {
            RaiseStatusUpdateEvent(this, e);
        }

        internal void UpdateStatusMessage(string StatusMessage)
        {
            try
            {
                Log.Write(StatusMessage, Log.Information, 3);
                StatusChangedEventArgs NewStatus = new StatusChangedEventArgs(StatusMessage);
                RaiseStatusUpdateEvent(this, NewStatus);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.UpdateStatusMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }
        internal void UpdateStatusMessage(string StatusMessage, string MethodName)
        {
            try
            {
                Log.Write(StatusMessage + MethodName, Log.Information, 3);
                StatusChangedEventArgs NewStatus = new StatusChangedEventArgs(StatusMessage);
                RaiseStatusUpdateEvent(this, NewStatus);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.UpdateStatusMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }



        private void ImportFTPData()
        {
            string strMethodName = "[InventoryImportController.ImportFTPData]";
            try
            {
                foreach (DictionaryEntry entry in modelImportSpec.FTPImportSpecs)
                {
                    FTPImportSpec importSpecFTP = (FTPImportSpec)entry.Value;
                    if ((ContinueServiceLoop) && (importSpecFTP.ImportSpecification.IsEnabled))
                    {
                        try
                        {
                            specActiveImport = importSpecFTP.ImportSpecification;

                            string strMsg = engineImportMessages.GenerateStatusMessage("Opening FTP", importSpecFTP);
                            UpdateStatusMessage(strMsg, strMethodName);

                            importSpecFTP.ImportSpecification.IsRunning = true;
                            importSpecFTP.ImportSpecification.FileCheckTime = DateTime.Now.ToUniversalTime();
                            modelImportSpec.UpdateImportSpecs();

                            DownloadFTP(importSpecFTP);
                            modelImportSpec.UpdateImportSpecs();

                            if (importSpecFTP.ImportSpecification.IsDownloadSuccessful)
                            {
                                FileInfo infoAttachment = new FileInfo(importSpecFTP.ImportSpecification.LocalFilePath);
                                if (infoAttachment.Exists)
                                {
                                    strMsg = engineImportMessages.GenerateStatusMessage("Parsing Text", importSpecFTP);
                                    UpdateStatusMessage(strMsg, strMethodName);

                                    parserInventoryData.ParseData(importSpecFTP.ImportSpecification);
                                    modelImportSpec.UpdateImportSpecs();

                                    if (ScanSpecForDateMatch(importSpecFTP.ImportSpecification))
                                    {
                                        strMsg = engineImportMessages.GenerateStatusMessage("Executing SQL Insert Into Inventory Table", importSpecFTP);
                                        UpdateStatusMessage(strMsg, strMethodName);

                                        modelImportSpec.ImportInventoryData(importSpecFTP.ImportSpecification);
                                        modelImportSpec.UpdateImportSpecs();

                                        VerifyNumRecordsImported(importSpecFTP.ImportSpecification);
                                        modelImportSpec.UpdateImportSpecs();

                                        strMsg = engineImportMessages.GenerateStatusMessage("Import Complete", importSpecFTP);
                                        UpdateStatusMessage(strMsg, strMethodName);
                                    }
                                    else
                                    {
                                        string strMsgDate = engineImportMessages.GenerateDateMismatchMessage(importSpecFTP.ImportSpecification);
                                        SendErrorEmail(strMsgDate);

                                        string strMsgErr = engineImportMessages.GenerateErrorMessage(importSpecFTP, strMsgDate);
                                        SendErrorEmail(strMsgErr);
                                    }
                                }
                            }
                            specActiveImport = null;
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex.Message + strMethodName, Log.Error, 1);
                            Console.WriteLine(ex.Message);
                            SendErrorEmail(engineImportMessages.GenerateErrorMessage(importSpecFTP, ex));
                        }
                        finally
                        {
                            importSpecFTP.ImportSpecification.IsRunning = false;
                            modelImportSpec.UpdateImportSpecs();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + strMethodName, Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


        private void ImportEmailBodyData()
        {
            try
            {
                string strMessagePrefix = "Importing Email In-Line (Message Text) Data";

                foreach (DictionaryEntry entry in modelImportSpec.EmailBodyImportSpecs)
                {
                    EmailImportSpec importSpecEmail = (EmailImportSpec)entry.Value;
                    if ((ContinueServiceLoop) && (importSpecEmail.ImportSpecification.IsEnabled))
                    {
                        try
                        {
                            specActiveImport = importSpecEmail.ImportSpecification;
                            UpdateStatusMessage(engineImportMessages.GenerateStatusMessage(strMessagePrefix, importSpecEmail));
                            UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Scanning Exchange Email Messages", importSpecEmail));

                            importSpecEmail.ImportSpecification.IsRunning = true;
                            importSpecEmail.ImportSpecification.FileCheckTime = DateTime.Now.ToUniversalTime();
                            modelImportSpec.UpdateImportSpecs();

                            clientExchange.FindExchangeMessage(importSpecEmail);
                            modelImportSpec.UpdateImportSpecs();
                            if (importSpecEmail.ImportSpecification.IsDownloadSuccessful)
                            {
                                UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Parsing Text", importSpecEmail));
                                parserInventoryData.ParseData(importSpecEmail.ImportSpecification);
                                modelImportSpec.UpdateImportSpecs();

                                if (ScanSpecForDateMatch(importSpecEmail.ImportSpecification))
                                {
                                    UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Executing SQL Insert Into Inventory Table", importSpecEmail));
                                    modelImportSpec.ImportInventoryData(importSpecEmail.ImportSpecification);
                                    modelImportSpec.UpdateImportSpecs();

                                    VerifyNumRecordsImported(importSpecEmail.ImportSpecification);
                                    modelImportSpec.UpdateImportSpecs();
                                    UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Import Complete", importSpecEmail));
                                }
                                else
                                {
                                    string strMsgDate = engineImportMessages.GenerateDateMismatchMessage(importSpecEmail.ImportSpecification);
                                    string strMsgErr = engineImportMessages.GenerateErrorMessage(importSpecEmail, strMsgDate);
                                    SendErrorEmail(strMsgErr);
                                }
                            }
                            else
                            {
                                string strErr = string.Format("Unable to Locate Email Body from Sender: {0} with Subject: {2}",
                                                                    importSpecEmail.MailAddress,
                                                                    importSpecEmail.MailSubject
                                                             );
                                SendErrorEmail(engineImportMessages.GenerateErrorMessage(importSpecEmail, strErr));

                            }
                            specActiveImport = null;
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex.Message + " [InventoryImportController.ImportEmailBodyData]", Log.Error, 1);
                            Console.WriteLine(ex.Message);
                            SendErrorEmail(engineImportMessages.GenerateErrorMessage(importSpecEmail, ex));
                        }
                        finally
                        {
                            importSpecEmail.ImportSpecification.IsRunning = false;
                            modelImportSpec.UpdateImportSpecs();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.ImportEmailBodyData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


        private void ImportEmailAttachmentData()
        {
            try
            {
                string strMessagePrefix = "Importing Email Attachments (Files) Data";

                foreach (DictionaryEntry entry in modelImportSpec.EmailAttachmentImportSpecs)
                {
                    EmailImportSpec importSpecEmail = (EmailImportSpec)entry.Value;
                    if ((ContinueServiceLoop) && (importSpecEmail.ImportSpecification.IsEnabled))
                    {
                        try
                        {
                            specActiveImport = importSpecEmail.ImportSpecification;
                            UpdateStatusMessage(engineImportMessages.GenerateStatusMessage(strMessagePrefix, importSpecEmail));
                            UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Scanning Exchange Email Messages", importSpecEmail));

                            importSpecEmail.ImportSpecification.IsRunning = true;
                            importSpecEmail.ImportSpecification.FileCheckTime = DateTime.Now.ToUniversalTime();
                            modelImportSpec.UpdateImportSpecs();

                            clientExchange.FindExchangeMessage(importSpecEmail);
                            modelImportSpec.UpdateImportSpecs();

                            if (importSpecEmail.ImportSpecification.IsDownloadSuccessful)
                            {
                                //Check for XL spreadsheet
                                FileInfo infoAttachment = new FileInfo(importSpecEmail.ImportSpecification.RemoteFilePath);
                                if (importSpecEmail.ImportSpecification.HasImportData)
                                {
                                    UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Parsing Text", importSpecEmail));
                                    parserInventoryData.ParseData(importSpecEmail.ImportSpecification);
                                    modelImportSpec.UpdateImportSpecs();
                                    if (ScanSpecForDateMatch(importSpecEmail.ImportSpecification))
                                    {
                                        UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Executing SQL Insert Into Inventory Table", importSpecEmail));
                                        modelImportSpec.ImportInventoryData(importSpecEmail.ImportSpecification);
                                        modelImportSpec.UpdateImportSpecs();

                                        VerifyNumRecordsImported(importSpecEmail.ImportSpecification);
                                        modelImportSpec.UpdateImportSpecs();
                                        UpdateStatusMessage(engineImportMessages.GenerateStatusMessage("Import Complete", importSpecEmail));
                                    }
                                    else
                                    {
                                        string strMsgDate = engineImportMessages.GenerateDateMismatchMessage(importSpecEmail.ImportSpecification);
                                        string strMsgErr = engineImportMessages.GenerateErrorMessage(importSpecEmail, strMsgDate);
                                        SendErrorEmail(strMsgErr);
                                    }
                                }
                            }
                            specActiveImport = null;
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex.Message + " [InventoryImportController.ImportEmailAttachmentData]", Log.Error, 1);
                            Console.WriteLine(ex.Message);
                            SendErrorEmail(engineImportMessages.GenerateErrorMessage(importSpecEmail, ex));
                        }
                        finally
                        {
                            importSpecEmail.ImportSpecification.IsRunning = false;
                            modelImportSpec.UpdateImportSpecs();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.ImportEmailAttachmentData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


        private void GenerateImportExecution(ImportSpec ImportSpecification)
        {
            modelImportSpec.GenerateImportExecution(ImportSpecification);
            ImportSpecification.NumRecordsImported = 0;
            ImportSpecification.ImportStatus = "";
            ImportSpecification.AssertLocalDir();
        }

        /// <summary>
        /// Uses Rebex FTP client to Download Files to Specified Directory
        /// </summary>
        /// <param name="ImportFTP">FTP Import Specification Which Contains FTP Settings</param>
        internal void DownloadFTP(FTPImportSpec ImportFTP)
        {
            mtxImportFTP.WaitOne();
            try
            {
                StockLoan.Common.Filer FTPFileHandler = new Filer(ImportFTP.ImportSpecification.LocalDir);
                try
                {
                    string strFileTime = FTPFileHandler.FileTime(ImportFTP.ImportSpecification.RemoteFilePath,
                                                                   ImportFTP.FTPServer,
                                                                   ImportFTP.FTPUserName,
                                                                   ImportFTP.FTPPassword
                                                                 );
                    if (!string.IsNullOrEmpty(strFileTime))
                    {
                        DateTime dtFileTime = DateTime.Parse(strFileTime);
                        if ((null == ImportFTP.ImportSpecification.FileModifiedTime) || (dtFileTime > ImportFTP.ImportSpecification.FileModifiedTime))
                        {

                            GenerateImportExecution(ImportFTP.ImportSpecification);

                            FTPFileHandler.FileGet(ImportFTP.ImportSpecification.RemoteFilePath,
                                                        ImportFTP.FTPServer,
                                                        ImportFTP.FTPUserName,
                                                        ImportFTP.FTPPassword,
                                                        ImportFTP.ImportSpecification.LocalFilePath
                                                    );

                            FileInfo RecievedFileInfo = new FileInfo(ImportFTP.ImportSpecification.LocalFilePath);
                            if (RecievedFileInfo.Exists && 1 < RecievedFileInfo.Length)
                            {
                                ImportFTP.ImportSpecification.IsDownloadSuccessful = true;

                                if (ReadInputFile(ImportFTP.ImportSpecification))
                                {
                                    ImportFTP.ImportSpecification.FileModifiedTime = dtFileTime.ToUniversalTime();
                                    ImportFTP.ImportSpecification.ImportStatus = ImportFTP.ImportSpecification.LocalFilePath + ", " + RecievedFileInfo.Length + " bytes";
                                }
                                else
                                {
                                    ImportFTP.ImportSpecification.ImportStatus = FTPFileHandler.Response;
                                }
                            }
                            else
                            {
                                ImportFTP.ImportSpecification.ImportStatus = FTPFileHandler.Response;
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    else
                    {

                        Thread.Sleep(3000);
                    }
                }
                catch (System.Net.WebException ex)
                {
                    Log.Write(ex.Message + " [InventoryImportController.ImportEmailBodyData]", Log.Error, 1);
                    Console.WriteLine(ex.ToString());
                    ImportFTP.ImportSpecification.ImportStatus = FTPFileHandler.Response;
                    SendErrorEmail(engineImportMessages.GenerateErrorMessage(ImportFTP.ImportSpecification, ex));
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportController.DownloadFTP]", Log.Error, 1);
                    Console.WriteLine(ex.ToString());
                    ImportFTP.ImportSpecification.ImportStatus = FTPFileHandler.Response;
                    SendErrorEmail(engineImportMessages.GenerateErrorMessage(ImportFTP.ImportSpecification, ex));
                }
                finally
                {
                    FTPFileHandler.Dispose();
                }

                mtxImportFTP.ReleaseMutex();

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.DownloadFTP]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// Compare BizDate Expected against BizDate Specified in File Data
        /// </summary>
        /// <param name="ImportSpecification">Import Specification Object Which Should Hold the Parsed Text Data</param>
        /// <returns>True if Date in File matches Date Expected (Today or Yesterday)</returns>
        internal bool ScanSpecForDateMatch(ImportSpec ImportSpecification)
        {
            bool bReturnMatchSuccess = true;

            if (RefuseImportOldDates)
            {
                try
                {
                    if (!string.IsNullOrEmpty(ImportSpecification.RegexDate))
                    {

                        DateTime dtSystemBizDate = GetBizDate();
                        if (ImportSpecification.IsBizDatePrior)
                        {
                            dtSystemBizDate = GetPriorBizDate();
                        }
                        if (dtSystemBizDate.Date != ImportSpecification.BizDateSpecified.Date)
                        {
                            bReturnMatchSuccess = false;
                            string strMsgErrDate = engineImportMessages.GenerateDateExpectedMessage(ImportSpecification);
                            ImportSpecification.ImportStatus = strMsgErrDate + ImportSpecification.ImportStatus;
                            modelImportSpec.UpdateImportSpecs();
                        }
                        else
                        {
                            bReturnMatchSuccess = true;
                        }
                    }
                    else
                    {
                        bReturnMatchSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportController.ScanSpecForDateMatch]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                }
            }
            return bReturnMatchSuccess;
        }


        #endregion


        //---------------------------------------------------------------------
        #region StaticMethods

        public static DateTime GetBizDate()
        {
            return InventoryImportModel.BizDate;
        }

        public static DateTime GetPriorBizDate()
        {
            return InventoryImportModel.BizDatePrior;
        }


        public static string ParseImportFileDate(string RemoteFilePath, bool IsBizDatePrior)
        {
            string strFTPRemoteFilePath = RemoteFilePath;
            if (RemoteFilePath.Contains("{") && RemoteFilePath.Contains("}"))
            {
                int nDateStartLocation = 1 + RemoteFilePath.IndexOf("{");
                int nDateEndLocation = RemoteFilePath.IndexOf("}");
                if (nDateStartLocation < nDateEndLocation)
                {
                    int nDateFormatLength = nDateEndLocation - nDateStartLocation;
                    string strDateFormat = RemoteFilePath.Substring(nDateStartLocation, nDateFormatLength);

                    DateTime dtBiz = GetBizDate();
                    if (IsBizDatePrior)
                    {
                        dtBiz = GetPriorBizDate();
                    }

                    string strToday = dtBiz.ToString(strDateFormat);
                    strFTPRemoteFilePath = RemoteFilePath.Replace("{" + strDateFormat + "}", strToday);
                }
            }
            return strFTPRemoteFilePath;

        }




        public static bool ReadInputData(ImportSpec ImportSpecification, ArrayList RowData)
        {
            bool bReturnSuccess = false;

            ImportSpecification.InventoryImportText.AddRange(RowData);

            if (0 < ImportSpecification.InventoryImportText.Count)
            {
                ImportSpecification.HasImportData = true;
                bReturnSuccess = true;
            }

            return bReturnSuccess;

        }

        public static bool ReadInputFile(ImportSpec ImportSpecification)
        {
            bool bReturnSuccess = false;

            ArrayList RowData = new ArrayList();
            FileInfo infoImportFile = new FileInfo(ImportSpecification.LocalFilePath);
            if ((infoImportFile.Exists) && (0 < infoImportFile.Length))
            {
                // Parse Data
                if (".xls" == infoImportFile.Extension.ToLower())
                {
                    RowData.AddRange(ExcelClient.ReadWorkbook(ImportSpecification));
                }
                else
                {
                    RowData.AddRange(File.ReadAllLines(ImportSpecification.LocalFilePath));
                }

                ImportSpecification.InventoryImportText.AddRange(RowData);

                if (0 < ImportSpecification.InventoryImportText.Count)
                {
                    ImportSpecification.HasImportData = true;
                    bReturnSuccess = true;
                }

            }
            return bReturnSuccess;

        }

        public static bool ReadInputFile(ImportSpec ImportSpecification, ArrayList HeaderData)
        {
            bool bReturnSuccess = false;

            ArrayList RowData = HeaderData;
            FileInfo infoImportFile = new FileInfo(ImportSpecification.LocalFilePath);
            if ((infoImportFile.Exists) && (0 < infoImportFile.Length))
            {
                // Parse Data
                if (".xls" == infoImportFile.Extension.ToLower())
                {
                    RowData.AddRange(ExcelClient.ReadWorkbook(ImportSpecification));
                }
                else
                {
                    RowData.AddRange(File.ReadAllLines(ImportSpecification.LocalFilePath));
                }
                if (ScanDataForHeaderMatch(ImportSpecification, RowData))
                {
                    ImportSpecification.InventoryImportText.AddRange(RowData);

                    if (0 < ImportSpecification.InventoryImportText.Count)
                    {
                        ImportSpecification.HasImportData = true;
                        bReturnSuccess = true;
                    }
                }

            }
            return bReturnSuccess;

        }

        public static bool ScanDataForHeaderMatch(ImportSpec ImportSpecification, ArrayList RowData)
        {
            bool bReturnMatchSuccess = false;
            try
            {
                if (!string.IsNullOrEmpty(ImportSpecification.RegexHeader))
                {
                    Regex rgxHeaderRow = new Regex(ImportSpecification.RegexHeader, RegExOptions);
                    foreach (string strDataRow in RowData)
                    {
                        bReturnMatchSuccess = rgxHeaderRow.IsMatch(strDataRow);
                        if (bReturnMatchSuccess)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    bReturnMatchSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.ScanDataForHeaderMatch]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return bReturnMatchSuccess;
        }


        internal bool VerifyNumRecordsImported(ImportSpec ImportSpecification)
        {
            bool bReturnImportedAllRecords = true;
            try
            {
                if (!string.IsNullOrEmpty(ImportSpecification.RegexRowCount))
                {
                    if (ImportSpecification.NumRecordsImported == ImportSpecification.NumRecordsExpected)
                    {
                        bReturnImportedAllRecords = true;
                    }
                    else
                    {
                        bReturnImportedAllRecords = false;
                        string strMsgStatus = engineImportMessages.GenerateRecordCountMismatchStatusMessage(ImportSpecification) + ImportSpecification.ImportStatus;
                        ImportSpecification.ImportStatus = strMsgStatus;
                        string strMsgMismatch = engineImportMessages.GenerateRecordCountMismatchErrorMessage(ImportSpecification);
                        string strMsgErr = engineImportMessages.GenerateErrorMessage(ImportSpecification, strMsgMismatch);
                        strMsgErr += engineImportMessages.GenerateSQLRowErrorMessage(ImportSpecification);
                        SendErrorEmail(strMsgErr);

                        string strMsgErrSqlRows = engineImportMessages.GenerateSQLRowErrorMessage(ImportSpecification);
                    }
                }
                else
                {
                    bReturnImportedAllRecords = true;
                }

                if (bReturnImportedAllRecords)
                {
                    ImportSpecification.ImportStatus = "OK: " + ImportSpecification.ImportStatus;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.VerifyNumRecordsImported]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return bReturnImportedAllRecords;
        }

        #endregion


        internal void SendSupportEmail(string Message)
        {
            try
            {
                clientExchange.CreateMessage("Locates Inventory Import Status", Message);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.SendSupportEmail]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        internal void SendErrorEmail(string Message)
        {
            try
            {
                clientExchange.CreateMessage("Locates Inventory Import Error", Message);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportController.SendErrorEmail]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


    } // End Class






    #region EventArgs Classes

    public class StatusChangedEventArgs : System.EventArgs
    {
        private string strStatusMessage = "";
        private ImportSpec specImport;

        public string Message
        {
            get { return strStatusMessage; }
            set { strStatusMessage = value; }
        }
        public ImportSpec ImportSpecification
        {
            get { return specImport; }
            set { specImport = value; }
        }

        public StatusChangedEventArgs(string StatusMessage)
        {
            strStatusMessage = StatusMessage;
        }
        public StatusChangedEventArgs(string StatusMessage, ImportSpec ImportSpecification)
        {
            strStatusMessage = StatusMessage;
            specImport = ImportSpecification;
        }

    }

    public class ImportErrorEventArgs : System.EventArgs
    {
        private ImportSpec specImport;
        private Exception ex;

        public ImportErrorEventArgs(ImportSpec ImportSpecification, System.Exception Ex)
        {
            specImport = ImportSpecification;
            ex = Ex;
        }
        public ImportSpec ImportSpecification
        {
            get { return specImport; }
            set { specImport = value; }
        }
        public Exception Exception
        {
            get { return ex; }
            set { ex = value; }
        }
        public string Message
        {
            get { return ex.Message; }
        }
        public string StackTrace
        {
            get { return ex.StackTrace; }
        }
    }

    public class ModelErrorEventArgs : System.EventArgs
    {
        string strOperation = "";
        Exception ex;

        public ModelErrorEventArgs(Exception Ex, string OperationDescription)
        {
            strOperation = OperationDescription;
            ex = Ex;
        }
        public string OperationDescription
        {
            get { return strOperation; }
            set { strOperation = value; }
        }
        public Exception Exception
        {
            get { return ex; }
            set { ex = value; }
        }
        public string Message
        {
            get { return ex.Message; }
        }
        public string StackTrace
        {
            get { return ex.StackTrace; }
        }
    }


    #endregion


}








#region Get FTP Using Native .Net Controls without Rebex FTP Client

//// Build the URI
//UriBuilder ResourceBuilder = new UriBuilder();
//ResourceBuilder.Scheme = "ftp";
//ResourceBuilder.Host = strFTPServer;
//ResourceBuilder.Path = strFTPRemoteFilePath;
//ResourceBuilder.UserName = strFTPUserName;
//ResourceBuilder.Password = strFTPPassword;
//Uri uriLocateInventoryDownload = ResourceBuilder.Uri;

//// Create an FTP request
//FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uriLocateInventoryDownload);
//request.Method = WebRequestMethods.Ftp.DownloadFile;
//request.Credentials = new NetworkCredential(strFTPUserName, strFTPPassword);

//// Build Local File Path
//string strLocalFilePath = strLocalDir + uriLocateInventoryDownload.LocalPath;

//// Get FTP Data From remote Server
//FtpWebResponse response = (FtpWebResponse)request.GetResponse();
//Stream responseStream = response.GetResponseStream();
//StreamReader responseReader = new StreamReader(responseStream, Encoding.ASCII);
//StreamWriter responseWriter = new StreamWriter(strLocalFilePath);
//responseWriter.Write(responseReader.ReadToEnd());

//responseReader.Close();
//responseWriter.Close();

#endregion
