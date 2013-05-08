using System;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Collections;
using System.Collections.Generic;

using System.Threading;
using StockLoan.Common;

namespace StockLoan.Inventory
{
    /// <summary>
    /// Data model builds ImportSpec objects to represent Import Jobs
    /// Objects are accessed through the collections:
    ///     FTPImportSpecs, EmailBodyImportSpecs, EmailAttachmentImportSpecs 
    /// </summary>
    internal class InventoryImportModel : IDisposable
    {
        #region Internal Members

        private ImportMode enumModeImport;

        private ArrayList alImports;
        private Hashtable htImportIndex;

        private Hashtable htImportsFTP;
        private Hashtable htImportsEmail;
        private Hashtable htImportsEmailAttachment;
        private Hashtable htImportsEmailBody;

        private string strDBHost = "";
        private string strDBCatalog = "";
        private string strDBConn = "";
        private string strDBSelect = "";

        private string strSubscriptionSitesTable = "";
        private string strSubscriptionTypesTable = "";
        private string strImportExecutionTable = "";
        private string strInventoryFilePatternTable = "";
        private string strImportKeyValueTable = "";
        private string strImportDeskTable = "";

        private string strOperationDescription = "";

        private DataSet sqlDataModelImportSites;
        private SqlConnection sqlConnLocates;

        internal SqlDataAdapter sqlAdptrImport;
        private SqlCommandBuilder builderImportCommand;

        internal SqlDataAdapter sqlAdptrStatus;
        private SqlCommandBuilder builderStatusCommand;

        internal SqlDataAdapter sqlAdptrPatterns;
        private SqlCommandBuilder builderPatternCommand;

        internal SqlCommand sqlCmdInsertInventoryData;
        internal SqlCommand sqlCmdSubscriptionSiteSelect;
        internal SqlCommand sqlCmdSubscriptionTypeSelect;
        internal SqlCommand sqlCmdFilePatternSelect;
        internal SqlCommand sqlCmdInventoryExecutionSelect;
        internal SqlCommand sqlCmdDeskSelect;
        internal SqlCommand sqlCmdKeyValueSelect;

        private bool bHasImportSiteSpecs = false;
        private bool bContinueConstruction = true;

        #endregion



        #region Static Properties

        public static Mutex mtxImportModel = new Mutex(false, "InventoryImportModel");

        private static Hashtable htImportKeyValues;
        public static DateTime BizDate
        {
            get
            {
                DateTime dtBiz = DateTime.Today;
                if (htImportKeyValues.ContainsKey("BizDate"))
                {
                    string strBizDate = htImportKeyValues["BizDate"].ToString();
                    dtBiz = DateTime.Parse(strBizDate);
                }
                return dtBiz.Date;
            }
        }
        public static DateTime BizDatePrior
        {
            get
            {
                DateTime dtBizPrior = DateTime.Today;
                if (htImportKeyValues.ContainsKey("BizDatePrior"))
                {
                    string strBizDatePrior = htImportKeyValues["BizDatePrior"].ToString();
                    dtBizPrior = DateTime.Parse(strBizDatePrior);
                }
                else
                {
                    // If No record exists in KeyValues Table, Try to calculate it, 
                    //  Handle Weekends vs Weekdays
                    switch (dtBizPrior.DayOfWeek)
                    {
                        case DayOfWeek.Monday:  // Friday is downloaded on Monday Morning, so Mondays should point back to Friday
                            dtBizPrior = dtBizPrior.AddDays(-3);
                            break;
                        case DayOfWeek.Sunday:  // Weekends should point back to Thursday (becuase they still dont have data for Friday yet)
                            dtBizPrior = dtBizPrior.AddDays(-3);
                            break;
                        case DayOfWeek.Saturday:
                            dtBizPrior = dtBizPrior.AddDays(-2);
                            break;
                        default:
                            dtBizPrior = dtBizPrior.AddDays(-1);
                            break;
                    }
                }
                return dtBizPrior.Date;
            }
        }
        public static TimeSpan WaitTimeBizDate
        {
            get
            {
                TimeSpan timeWait = TimeSpan.Parse("00:00:10");
                if (htImportKeyValues.ContainsKey("InventoryMainLoopRecycleIntervalBizDay"))
                {
                    string strWaitTime = htImportKeyValues["InventoryMainLoopRecycleIntervalBizDay"].ToString();
                    timeWait = TimeSpan.Parse(strWaitTime);
                }
                return timeWait;
            }
        }
        public static TimeSpan WaitTimeNonBizDate
        {
            get
            {
                TimeSpan timeWait = TimeSpan.Parse("00:02:00");
                if (htImportKeyValues.ContainsKey("InventoryMainLoopRecycleIntervalNonBizDay"))
                {
                    string strWaitTime = htImportKeyValues["InventoryMainLoopRecycleIntervalNonBizDay"].ToString();
                    timeWait = TimeSpan.Parse(strWaitTime);
                }
                return timeWait;
            }
        }

        public static TimeSpan InventoryDataEndurance
        {
            get
            {
                TimeSpan tsEndurance = TimeSpan.Parse("60.00:00:00");
                try
                {
                    if (htImportKeyValues.ContainsKey("InventoryDataEndurance"))
                    {
                        string strEndurance = htImportKeyValues["InventoryDataEndurance"].ToString();
                        tsEndurance = TimeSpan.Parse(strEndurance);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message, Log.Error, 1);
                }

                return tsEndurance;
            }
        }

        #endregion



        #region Public Properties
        public ImportMode ModeImport
        {
            get { return enumModeImport; }
            set { enumModeImport = value; }
        }

        public Hashtable KeyValues
        {
            get { return htImportKeyValues; }
        }
        public Hashtable FTPImportSpecs
        {
            get { return htImportsFTP; }
        }
        public Hashtable EmailImportSpecs
        {
            get { return htImportsEmail; }
        }
        public Hashtable EmailBodyImportSpecs
        {
            get { return htImportsEmailBody; }
        }
        public Hashtable EmailAttachmentImportSpecs
        {
            get { return htImportsEmailAttachment; }
        }

        public DataSet ImportDataModel
        {
            get { return sqlDataModelImportSites; }
        }
        public DataTable SubscriptionSites
        {
            get
            {
                if (string.IsNullOrEmpty(strSubscriptionSitesTable))
                {
                    strSubscriptionSitesTable = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectFrom");
                }
                return sqlDataModelImportSites.Tables[strSubscriptionSitesTable];
            }
        }
        public DataTable ImportExecutions
        {
            get
            {
                if (string.IsNullOrEmpty(strImportExecutionTable))
                {
                    strImportExecutionTable = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectFrom");
                }
                return sqlDataModelImportSites.Tables[strImportExecutionTable];
            }
        }
        public DataTable FilePatterns
        {
            get
            {
                if (string.IsNullOrEmpty(strInventoryFilePatternTable))
                {
                    strInventoryFilePatternTable = StockLoan.Common.Standard.ConfigValue("InventoryFilePatternSelectFrom");
                }
                return sqlDataModelImportSites.Tables[strInventoryFilePatternTable];
            }
        }
        public bool HasImportSiteSpecs
        {
            get { return bHasImportSiteSpecs; }
        }

        public string SubscriptionSitesTableName
        {
            get { return strSubscriptionSitesTable; }
            set { strSubscriptionSitesTable = value; }
        }
        public string FilePatternTableName
        {
            get { return strInventoryFilePatternTable; }
            set { strInventoryFilePatternTable = value; }
        }

        public string ImportExecutionTableName
        {
            get { return strImportExecutionTable; }
            set { strImportExecutionTable = value; }
        }

        public string SubscriptionTypesTableName
        {
            get { return strSubscriptionTypesTable; }
            set { strSubscriptionTypesTable = value; }
        }

        public string ImportDesksTableName
        {
            get { return strImportDeskTable; }
            set { strImportDeskTable = value; }
        }
        #endregion


        #region Internal Properties

        internal string OperationDescription
        {
            get { return strOperationDescription; }
            set { strOperationDescription = value; }
        }

        internal bool ContinueConstruction
        {
            get { return bContinueConstruction; }
            set { bContinueConstruction = value; }
        }


        #endregion




        #region DelegatesAndEvents

        public delegate void StatusUpdateEventHandler(object sender, StatusChangedEventArgs e);
        public event StatusUpdateEventHandler StatusUpdate;
        public event ModelErrorEventHandler ModelError;



        internal void UpdateStatusMessage(string StatusMessage)
        {
            if (null != StatusUpdate)
            {
                StatusChangedEventArgs NewStatus = new StatusChangedEventArgs(StatusMessage);
                StatusUpdate(this, NewStatus);
            }
        }
        internal void RaiseErrorEvent(Exception Ex)
        {
            ModelErrorEventArgs argsErrorEvent = new ModelErrorEventArgs(Ex, OperationDescription);
            if (null != ModelError)
            {
                ModelError(this, argsErrorEvent);
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InventoryImportModel(ImportMode Mode)
        {
            enumModeImport = Mode;
        }

        /// <summary>
        /// Build Data Model
        /// </summary>
        public bool InitializeComponents()
        {

            try
            {
                mtxImportModel.WaitOne();

                OperationDescription = "Building SQL Connection";
                strDBHost = StockLoan.Common.Standard.ConfigValue("MainDatabaseHost");
                strDBCatalog = StockLoan.Common.Standard.ConfigValue("MainDatabaseName");

                strDBConn =
                  "Trusted_Connection=yes; " +
                  "Data Source=" + strDBHost + "; " +
                  "Initial Catalog=" + strDBCatalog + ";";

                sqlConnLocates = new SqlConnection(strDBConn);

                OperationDescription = "Building SQL Adapter for Managing Import Details";
                sqlAdptrImport = new SqlDataAdapter();
                sqlAdptrImport.ContinueUpdateOnError = true;
                builderImportCommand = new SqlCommandBuilder(sqlAdptrImport);

                OperationDescription = "Building SQL Adapter for Managing File Patterns";
                sqlAdptrPatterns = new SqlDataAdapter();
                sqlAdptrPatterns.ContinueUpdateOnError = true;
                builderPatternCommand = new SqlCommandBuilder(sqlAdptrPatterns);

                OperationDescription = "Building SQL Adapter for Managing Status Messages";
                sqlAdptrStatus = new SqlDataAdapter();
                sqlAdptrStatus.ContinueUpdateOnError = true;
                builderStatusCommand = new SqlCommandBuilder(sqlAdptrStatus);

                sqlConnLocates.Open();


                if (ConnectionState.Open != sqlConnLocates.State)
                {
                    ContinueConstruction = false;
                }
                else
                {
                    sqlDataModelImportSites = new DataSet("Subscriptions");

                    BuildInsertCommand();
                    BuildImportSpecs();

                    if (bHasImportSiteSpecs)
                    {
                        bHasImportSiteSpecs = true;

                        BuildKeyValues();
                        BuildSubscriptionTypes();
                        BuildFilePatterns();
                        BuildExecutions();
                        BuildDesks();

                        BuildRelationships();
                        BuildCollections();
                    }

                }// end if conn.open


            }// end try

            catch (SqlException sqlExcptn)
            {
                HandleExceptionSQL(sqlExcptn);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                InventoryImportModel.mtxImportModel.ReleaseMutex();
            }

            return ContinueConstruction;
        }

        private void BuildInsertCommand()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Building SQL Command for Insert of Inventory Data ";

                    sqlCmdInsertInventoryData = new SqlCommand("spInventoryItemSet", sqlConnLocates);
                    sqlCmdInsertInventoryData.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramBizDate = sqlCmdInsertInventoryData.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    SqlParameter paramDesk = sqlCmdInsertInventoryData.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                    SqlParameter paramAccount = sqlCmdInsertInventoryData.Parameters.Add("@Account", SqlDbType.VarChar, 15);
                    SqlParameter paramSecId = sqlCmdInsertInventoryData.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    SqlParameter paramModeCode = sqlCmdInsertInventoryData.Parameters.Add("@ModeCode", SqlDbType.Char, 1);
                    SqlParameter paramQuantity = sqlCmdInsertInventoryData.Parameters.Add("@Quantity", SqlDbType.BigInt);
                    SqlParameter paramExecutionID = sqlCmdInsertInventoryData.Parameters.Add("@ExecutionID", SqlDbType.BigInt);
                    SqlParameter paramIncrementCurrentQuantity = sqlCmdInsertInventoryData.Parameters.Add("@IncrementCurrentQuantity", SqlDbType.Bit);
                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        private void BuildImportSpecs()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Selecting Import Subscribers for ImportSpec objects";

                    string strSQLSubscriberService = StockLoan.Common.Standard.ConfigValue("InventorySubscriberService");
                    string strSQLSubscriberBatch = StockLoan.Common.Standard.ConfigValue("InventorySubscriberBatch");
                    string strSQLSubscriberBatchOrder = StockLoan.Common.Standard.ConfigValue("InventorySubscriberBatchOrder");

                    switch (ModeImport)
                    {
                        case ImportMode.Batch:
                            strSubscriptionSitesTable = strSQLSubscriberService;
                            strDBSelect = string.Format("Select * From {0} Order By {1}", strSQLSubscriberBatch, strSQLSubscriberBatchOrder);
                            break;
                        case ImportMode.Service:
                            strSubscriptionSitesTable = strSQLSubscriberService;
                            strDBSelect = string.Format("Select * From {0}", strSQLSubscriberService);
                            break;
                        case ImportMode.Test:
                            strSubscriptionSitesTable = strSQLSubscriberService;
                            strDBSelect = string.Format("Select * From {0}", strSQLSubscriberService);
                            break;
                        default:
                            strSubscriptionSitesTable = strSQLSubscriberService;
                            strDBSelect = string.Format("Select * From {0}", strSQLSubscriberService);
                            break;
                    }

                    OperationDescription += string.Format(" SQL: {0}", strDBSelect);

                    sqlCmdSubscriptionSiteSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdSubscriptionSiteSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdSubscriptionSiteSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strSubscriptionSitesTable);

                    if (0 < sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Rows.Count)
                    {
                        ContinueConstruction = true;
                        bHasImportSiteSpecs = true;

                        OperationDescription = "Adding Primary Key to tbInventorySubscriber (ImportSpec) Table";
                        DataTable sqlTableSubscription = sqlDataModelImportSites.Tables[strSubscriptionSitesTable];
                        DataColumn[] arrayColumnPrimaryKeySubscriptions = { sqlTableSubscription.Columns["InventorySubscriberID"] };
                        sqlTableSubscription.PrimaryKey = arrayColumnPrimaryKeySubscriptions;
                    }
                    else
                    {
                        ContinueConstruction = false;
                        bHasImportSiteSpecs = false;
                        string strErrMsg = "Error: No Import Subscribers Were Selected from tbInventorySubscriber, Make Sure that IsRunning = False for all Enabled Records";
                        RaiseErrorEvent(new Exception(strErrMsg));
                        UpdateStatusMessage(strErrMsg);
                    }

                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }


        private void BuildSubscriptionTypes()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Selecting Subscription Types";
                    strSubscriptionTypesTable = StockLoan.Common.Standard.ConfigValue("InventorySubscriptionTypeTable");
                    string strSQLSubscriptionTypeView = StockLoan.Common.Standard.ConfigValue("InventorySubscriptionTypeView");

                    strDBSelect = "Select * From " + strSQLSubscriptionTypeView;
                    OperationDescription += string.Format(" SQL: {0}", strDBSelect);

                    sqlCmdSubscriptionTypeSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdSubscriptionTypeSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdSubscriptionTypeSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strSubscriptionTypesTable);
                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        private void BuildFilePatterns()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Selecting RegEx Patterns for Parsing Data";

                    strInventoryFilePatternTable = StockLoan.Common.Standard.ConfigValue("InventoryFilePatternTable");
                    string strSQLFilePatternView = StockLoan.Common.Standard.ConfigValue("InventoryFilePatternView");

                    strDBSelect = "Select * From " + strSQLFilePatternView; 
                    OperationDescription += string.Format(" SQL: {0}", strDBSelect);

                    sqlCmdFilePatternSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdFilePatternSelect.CommandType = CommandType.Text;

                    sqlAdptrPatterns.SelectCommand = sqlCmdFilePatternSelect;
                    sqlAdptrPatterns.Fill(sqlDataModelImportSites, strInventoryFilePatternTable);

                    if (0 < sqlDataModelImportSites.Tables[strInventoryFilePatternTable].Rows.Count)
                    {
                        ContinueConstruction = true;
                        bHasImportSiteSpecs = true;

                        if (null == sqlDataModelImportSites.Tables[strInventoryFilePatternTable].PrimaryKey)
                        {
                            OperationDescription = "Adding Primary Key to tbInventoryFilePatterns (Regex) Table";
                            DataTable sqlTableFilePattern = sqlDataModelImportSites.Tables[strInventoryFilePatternTable];
                            DataColumn[] arrayColumnPrimaryKeyFilePatterns = { sqlTableFilePattern.Columns["InventoryFilePatternID"] };
                            sqlTableFilePattern.PrimaryKey = arrayColumnPrimaryKeyFilePatterns;
                        }
                    }
                    else
                    {
                        ContinueConstruction = false;
                        bHasImportSiteSpecs = false;
                    }
                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }


        private void BuildExecutions()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Selecting Import Execution History Records";
                    int nNumDaysOfExecutions = InventoryDataEndurance.Days;

                    if (string.IsNullOrEmpty(strImportExecutionTable))
                    {
                        string strSQLInventoryExecutionFrom = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectFrom");
                        strImportExecutionTable = strSQLInventoryExecutionFrom;
                    }

                    if (null == sqlCmdInventoryExecutionSelect)
                    {
                        string strSQLInventoryExecutionSelectProc = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectProcedure");
                        sqlCmdInventoryExecutionSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                        sqlCmdInventoryExecutionSelect.CommandType = CommandType.StoredProcedure;
                        sqlCmdInventoryExecutionSelect.CommandText = strSQLInventoryExecutionSelectProc;
                        sqlCmdInventoryExecutionSelect.Parameters.Add("@SubscriberID", SqlDbType.BigInt, 50);
                        sqlCmdInventoryExecutionSelect.Parameters.Add("@Days", SqlDbType.Int);
                    }

                    if ((null != sqlDataModelImportSites.Tables[strImportExecutionTable]) && (0 < sqlDataModelImportSites.Tables[strImportExecutionTable].Rows.Count))
                    {
                        sqlDataModelImportSites.Tables[strImportExecutionTable].Clear();
                    }

                    sqlAdptrStatus.SelectCommand = sqlCmdInventoryExecutionSelect;

                    int nNumRows = sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Rows.Count;
                    for (int iRow = 0; iRow < nNumRows; iRow++)
                    {
                        long nSubscriberID = (long)sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Rows[iRow]["InventorySubscriberID"];

                        sqlCmdInventoryExecutionSelect.Parameters["@SubscriberID"].Value = nSubscriberID;
                        sqlCmdInventoryExecutionSelect.Parameters["@Days"].Value = nNumDaysOfExecutions;
                        sqlAdptrStatus.Fill(sqlDataModelImportSites, strImportExecutionTable);
                    }

                    OperationDescription = "Adding Primary Key to tbInventoryImportExecution (ImportExecution) Table";
                    DataTable sqlTableExecution = sqlDataModelImportSites.Tables[strImportExecutionTable];
                    if ((null == sqlTableExecution.PrimaryKey) || (0 == sqlTableExecution.PrimaryKey.Length))
                    {
                        DataColumn[] arrayColumnPrimaryKeyExecution = { sqlTableExecution.Columns["InventoryImportExecutionID"] };
                        sqlTableExecution.PrimaryKey = arrayColumnPrimaryKeyExecution;
                    }
                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }


        private void BuildDesks()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Selecting Desks";
                    strImportDeskTable = StockLoan.Common.Standard.ConfigValue("InventoryDeskTable");
                    string strSQLDeskView = StockLoan.Common.Standard.ConfigValue("InventoryDeskView");

                    strDBSelect = "Select * From " + strSQLDeskView; 
                    OperationDescription += string.Format(" SQL: {0}", strDBSelect);

                    sqlCmdDeskSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdDeskSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdDeskSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strImportDeskTable);
                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }


        private void BuildKeyValues()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Selecting Subscription Key Values";
                    strImportKeyValueTable = StockLoan.Common.Standard.ConfigValue("InventoryKeyValueTable");

                    string strSQLKeyValueView = StockLoan.Common.Standard.ConfigValue("InventoryKeyValueView");
                    strDBSelect = "Select * From " + strSQLKeyValueView;
                    OperationDescription += string.Format(" SQL: {0}", strDBSelect);

                    sqlCmdKeyValueSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdKeyValueSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdKeyValueSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strImportKeyValueTable);

                    //----------------------------------------------------------------------------------------------
                    OperationDescription = "Building Internal Collection of Key Values";
                    int nNumKeyValues = sqlDataModelImportSites.Tables[strImportKeyValueTable].Rows.Count;

                    if (null == htImportKeyValues)
                    {
                        htImportKeyValues = new Hashtable(nNumKeyValues);
                    }
                    else
                    {
                        htImportKeyValues.Clear();
                    }
                    for (int iKeyValue = 0; iKeyValue < nNumKeyValues; iKeyValue++)
                    {
                        DataRow rowKeyValue = sqlDataModelImportSites.Tables[strImportKeyValueTable].Rows[iKeyValue];
                        IndexKeyValues(rowKeyValue);
                    }
                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }



        private void BuildRelationships()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Establishing SQL Relationship Between Subscriber and Type";
                    DataColumn columnParentSubscriptionType = sqlDataModelImportSites.Tables[strSubscriptionTypesTable].Columns["InventorySubscriptionTypeID"];
                    DataColumn columnChildSubscriberType = sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Columns["SubscriptionTypeID"];
                    OperationDescription += string.Format("{0} {1} to {2}", System.Environment.NewLine, strSubscriptionSitesTable, strSubscriptionSitesTable);

                    string strImportTypeRelationName = StockLoan.Common.Standard.ConfigValue("ImportTypeRelationName");
                    DataRelation relationSubscriptionTypes = new DataRelation(strImportTypeRelationName, columnParentSubscriptionType, columnChildSubscriberType);
                    sqlDataModelImportSites.Relations.Add(relationSubscriptionTypes);


                    //---------------------------------------------------------
                    OperationDescription = "Establishing SQL Relationship Between Subscriber and Data File Pattern (RegEx)";
                    DataColumn columnParentPatternID = sqlDataModelImportSites.Tables[strInventoryFilePatternTable].Columns["InventoryFilePatternID"];
                    DataColumn columnChildSubscriberPatternID = sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Columns["InventoryFilePatternID"];
                    OperationDescription += string.Format("{0} {1} to {2}", System.Environment.NewLine, strInventoryFilePatternTable, strSubscriptionSitesTable);

                    string strImportMaskRelationName = StockLoan.Common.Standard.ConfigValue("ImportFilePatternRelationName");
                    DataRelation relationSubscriptionMasks = new DataRelation(strImportMaskRelationName, columnParentPatternID, columnChildSubscriberPatternID);
                    sqlDataModelImportSites.Relations.Add(relationSubscriptionMasks);


                    //---------------------------------------------------------
                    OperationDescription = "Establishing SQL Relationship Between Subscriber and Execution (Import Run)";
                    DataColumn columnParentSubscriberID = sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Columns["InventorySubscriberID"];
                    DataColumn columnChildExecutionSubscriberID = sqlDataModelImportSites.Tables[strImportExecutionTable].Columns["SubscriberID"];
                    OperationDescription += string.Format("{0} {1} to {2}", System.Environment.NewLine, strSubscriptionSitesTable, strImportExecutionTable);

                    string strImportExecutionRelationName = StockLoan.Common.Standard.ConfigValue("ImportExecutionRelationName");
                    DataRelation relationSubscriptionExecutions = new DataRelation(strImportExecutionRelationName, columnParentSubscriberID, columnChildExecutionSubscriberID);
                    sqlDataModelImportSites.Relations.Add(relationSubscriptionExecutions);


                    //---------------------------------------------------------
                    OperationDescription = "Establishing SQL Relationship Between Subscriber and Desk";
                    DataColumn columnParentDesk = sqlDataModelImportSites.Tables[strImportDeskTable].Columns["Desk"];
                    DataColumn columnChildSubscriberDesk = sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Columns["Desk"];
                    OperationDescription += string.Format("{0} {1} to {2}", System.Environment.NewLine, strSubscriptionSitesTable, strImportKeyValueTable);

                    string strDeskSubscriberRelationName = StockLoan.Common.Standard.ConfigValue("ImportDeskRelationName");
                    DataRelation relationDeskSubscriptions = new DataRelation(strDeskSubscriberRelationName, columnParentDesk, columnChildSubscriberDesk);
                    sqlDataModelImportSites.Relations.Add(relationDeskSubscriptions);

                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }


        private void BuildCollections()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Building Internal Collections of Import Specs - Used outside the class to access the data objects";
                    int nNumSubscriptions = sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Rows.Count;
                    if (null == alImports) { alImports = new ArrayList(nNumSubscriptions); } else { alImports.Clear(); }
                    if (null == htImportIndex) { htImportIndex = new Hashtable(nNumSubscriptions); } else { htImportIndex.Clear(); }
                    if (null == htImportsFTP) { htImportsFTP = new Hashtable(nNumSubscriptions); } else { htImportsFTP.Clear(); }
                    if (null == htImportsEmail) { htImportsEmail = new Hashtable(nNumSubscriptions); } else { htImportsEmail.Clear(); }
                    if (null == htImportsEmailBody) { htImportsEmailBody = new Hashtable(nNumSubscriptions); } else { htImportsEmailBody.Clear(); }
                    if (null == htImportsEmailAttachment) { htImportsEmailAttachment = new Hashtable(nNumSubscriptions); } else { htImportsEmailAttachment.Clear(); }

                    for (int iRowSubscription = 0; iRowSubscription < nNumSubscriptions; iRowSubscription++)
                    {
                        DataRow rowSubscription = sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Rows[iRowSubscription];
                        ImportSpec ActiveSpec = new ImportSpec(rowSubscription, sqlDataModelImportSites.Relations);
                        if (ActiveSpec.IsEnabled)
                        {
                            alImports.Add(ActiveSpec);
                            htImportIndex.Add(ActiveSpec.Desk, ActiveSpec);

                            long nID = ActiveSpec.SubscriberID;
                            // Insert Object into Collection
                            switch (ActiveSpec.SubscriptionType)
                            {
                                case "FTP":
                                    FTPImportSpec CurrentSpecFTP = new FTPImportSpec(ActiveSpec);
                                    htImportsFTP.Add(nID, CurrentSpecFTP);
                                    break;
                                case "EmailBody":
                                    EmailImportSpec CurrentSpecEmailBody = new EmailImportSpec(ActiveSpec);
                                    htImportsEmail.Add(nID, CurrentSpecEmailBody);
                                    htImportsEmailBody.Add(nID, CurrentSpecEmailBody);
                                    break;
                                case "EmailAttachment":
                                    EmailImportSpec CurrentSpecEmailAttachment = new EmailImportSpec(ActiveSpec);
                                    htImportsEmail.Add(nID, CurrentSpecEmailAttachment);
                                    htImportsEmailAttachment.Add(nID, CurrentSpecEmailAttachment);
                                    break;
                            }
                        }
                    }


                }
                catch (SqlException sqlExcptn)
                {
                    HandleExceptionSQL(sqlExcptn);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }






        private void IndexKeyValues(DataRow KeyValueRow)
        {
            try
            {
                string strKey = (string)KeyValueRow["KeyID"];
                string strValue = (string)KeyValueRow["KeyValue"];

                if ((!string.IsNullOrEmpty(strKey)) && (!string.IsNullOrEmpty(strValue)))
                {
                    htImportKeyValues.Add(strKey, strValue);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }







        public void UpdateImportSpecs()
        {
            try
            {
                OperationDescription = "Updating InventorySubscriber (ImportSpecs)";
                if (string.IsNullOrEmpty(strSubscriptionSitesTable))
                {
                    strSubscriptionSitesTable = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectFrom");
                }
                sqlAdptrImport.SelectCommand = sqlCmdSubscriptionSiteSelect;
                sqlAdptrImport.UpdateCommand = builderImportCommand.GetUpdateCommand(true);
                int nImportRowsUpdated = sqlAdptrImport.Update(sqlDataModelImportSites, strSubscriptionSitesTable);


                OperationDescription = "Updating ImportExecution (Historical Runs)";
                if (string.IsNullOrEmpty(strImportExecutionTable))
                {
                    strImportExecutionTable = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectFrom");
                }
                int nStatusRowsUpdated = sqlAdptrStatus.Update(sqlDataModelImportSites, strImportExecutionTable);


                OperationDescription = "Updating File Patterns (Regexes)";
                if (string.IsNullOrEmpty(strInventoryFilePatternTable))
                {
                    strInventoryFilePatternTable = StockLoan.Common.Standard.ConfigValue("InventoryFilePatternSelectFrom");
                }
                int nPatternRowsUpdated = sqlAdptrPatterns.Update(sqlDataModelImportSites, strInventoryFilePatternTable);

            }
            catch (SqlException sqlExcptn)
            {
                HandleExceptionSQL(sqlExcptn);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void GenerateImportExecution(ImportSpec ImportSpecification)
        {
            OperationDescription = "Generating Import Execution, Inserting New Record for This Import Run";

            try
            {
                string strHostName = System.Environment.MachineName;

                // Call StoredProcedure to Insert New Row and get ExecutionID from the New Row
                string strSQLInventoryExecutionInsertProcedure = StockLoan.Common.Standard.ConfigValue("InventoryExecutionInsertProcedure");
                SqlCommand sqlCmdImportExecution = new SqlCommand(strSQLInventoryExecutionInsertProcedure, sqlConnLocates);
                sqlCmdImportExecution.CommandType = CommandType.StoredProcedure;

                SqlParameter paramInSubscriberID = sqlCmdImportExecution.Parameters.Add("@SubscriberID", SqlDbType.BigInt);
                paramInSubscriberID.Direction = ParameterDirection.Input;
                paramInSubscriberID.Value = (long)ImportSpecification.SubscriberID;

                SqlParameter paramInHostName = sqlCmdImportExecution.Parameters.Add("@HostName", SqlDbType.NVarChar);
                paramInHostName.Direction = ParameterDirection.Input;
                paramInHostName.Value = strHostName;

                SqlParameter paramOutExecutionID = sqlCmdImportExecution.Parameters.Add("RetVal", SqlDbType.BigInt);
                paramOutExecutionID.Direction = ParameterDirection.ReturnValue;

                sqlCmdImportExecution.ExecuteScalar();

                // Get PrimaryKey ID of Execution
                int nExecutionID = (int)paramOutExecutionID.Value;
                ImportSpecification.ExecutionID = (long)nExecutionID;
                ImportSpecification.LocalDir = ImportSpecification.RootDir + nExecutionID.ToString() + "\\";

                // Update Whole Model Dataset to Retrieve New Execution
                BuildExecutions();

                // Use ID returned by StoredProcedure to Fetch the Whole Row
                ImportSpecification.ImportExecutionRow = sqlDataModelImportSites.Tables[strImportExecutionTable].Rows.Find(nExecutionID);
            }
            catch (SqlException sqlExcptn)
            {
                HandleExceptionSQL(sqlExcptn);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }


        public long GenerateFilePattern(string Desk)
        {
            long lnPatternID = 0;
            OperationDescription = "Generating New File Pattern, Inserting New Patterns For Parsing Text ";

            try
            {

                // Call StoredProcedure to Insert New Row and get ExecutionID from the New Row
                string strSQLInventoryFilePatternInsertProcedure = StockLoan.Common.Standard.ConfigValue("InventoryFilePatternInsertProcedure");
                SqlCommand sqlCmdFilePattern = new SqlCommand(strSQLInventoryFilePatternInsertProcedure, sqlConnLocates);
                sqlCmdFilePattern.CommandType = CommandType.StoredProcedure;

                SqlParameter paramInDesk = sqlCmdFilePattern.Parameters.Add("@Desk", SqlDbType.VarChar);
                paramInDesk.Direction = ParameterDirection.Input;
                paramInDesk.Value = Desk;

                SqlParameter paramOutPatternID = sqlCmdFilePattern.Parameters.Add("RetVal", SqlDbType.BigInt);
                paramOutPatternID.Direction = ParameterDirection.ReturnValue;

                sqlCmdFilePattern.ExecuteScalar();

                //// Get PrimaryKey ID of File Pattern
                int nPatternID = (int)paramOutPatternID.Value;

                // Update Whole Model Dataset to Retrieve New Pattern
                if ((null == FilePatternTableName) || ("" == FilePatternTableName))
                {
                    FilePatternTableName = StockLoan.Common.Standard.ConfigValue("InventoryFilePatternSelectFrom");
                }
                sqlAdptrPatterns.SelectCommand = sqlCmdFilePattern;
                sqlAdptrPatterns.Fill(sqlDataModelImportSites, FilePatternTableName);

            }
            catch (SqlException sqlExcptn)
            {
                HandleExceptionSQL(sqlExcptn);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return lnPatternID;

        }


        public void ImportInventoryData(ImportSpec ImportSpecification)
        {
            OperationDescription = "Importing Final Data into Inventory Table from ImportSpec";
            Log.Write(OperationDescription, Log.Information, 3);
            InventoryImportController.mtxImportDataEntry.WaitOne();
            try
            {
                int nNumRecords = 0;
                foreach (InventoryDataEntry dataInventoryEntry in ImportSpecification.InventoryEntryObjects)
                {
                    sqlCmdInsertInventoryData.Parameters["@Desk"].Value = dataInventoryEntry.Desk;
                    sqlCmdInsertInventoryData.Parameters["@BizDate"].Value = dataInventoryEntry.BizDate;
                    sqlCmdInsertInventoryData.Parameters["@Account"].Value = dataInventoryEntry.Account;
                    sqlCmdInsertInventoryData.Parameters["@SecId"].Value = dataInventoryEntry.SecId;
                    sqlCmdInsertInventoryData.Parameters["@ModeCode"].Value = dataInventoryEntry.ModeCode;
                    sqlCmdInsertInventoryData.Parameters["@Quantity"].Value = dataInventoryEntry.Quantity;
                    sqlCmdInsertInventoryData.Parameters["@ExecutionID"].Value = ImportSpecification.ExecutionID;
                    sqlCmdInsertInventoryData.Parameters["@IncrementCurrentQuantity"].Value = false;

                    if (InventoryImportController.ContinueServiceLoop)
                    {
                        try
                        {
                            int nRecordStatus = sqlCmdInsertInventoryData.ExecuteNonQuery();
                            if (0 < nRecordStatus) { nNumRecords++; }
                        }
                        catch (SqlException sqlExcptn)
                        {
                            dataInventoryEntry.HasErrors = true;
                            foreach (SqlError sqlError in sqlExcptn.Errors)
                            {
                                dataInventoryEntry.ErrorMsg += string.Format("{0}{1}", sqlError.Message, System.Environment.NewLine);
                            }
                        }
                    }
                }

                ImportSpecification.NumRecordsImported = nNumRecords;
                if (0 < ImportSpecification.NumRecordsImported)
                {
                    ImportSpecification.BizDateLastImported = ImportSpecification.BizDateProposedImport;
                }

                string strMsgStatus = string.Format("SubscriberID: {0}; Desk: {1}; Expected {2} Records; Imported {3} Records",
                                                        ImportSpecification.SubscriberID,
                                                        ImportSpecification.Desk,
                                                        ImportSpecification.NumRecordsExpected,
                                                        ImportSpecification.NumRecordsImported
                                                    );
                Log.Write(strMsgStatus, Log.Information, 3);

            }

            catch (SqlException sqlExcptn)
            {
                HandleExceptionSQL(sqlExcptn);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            InventoryImportController.mtxImportDataEntry.ReleaseMutex();
        }
        #endregion


        //------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------
        private void HandleException(Exception Ex)
        {
            string strErrMsg = string.Format("Exception Thrown From: [{0}.{1}] \nDuring Operation: {2} \nMessage:{3} \nStack Trace: {4}", Ex.Source, Ex.TargetSite.Name, OperationDescription, Ex.Message, Ex.StackTrace);
            Log.Write(strErrMsg, Log.Error, 1);

            RaiseErrorEvent(Ex);
            ContinueConstruction = false;
        }

        private void HandleExceptionSQL(SqlException ExSQL)
        {
            string strErrMsg = string.Format("SQL Exception #:{0} Thrown From: [{1}.{2}] During Operation: {3} \nMessage:{4} \n ", ExSQL.Number.ToString(), ExSQL.Source, ExSQL.TargetSite.Name, OperationDescription, ExSQL.Message);
            Log.Write(strErrMsg, Log.Error, 1);

            foreach (SqlError sqlError in ExSQL.Errors)
            {
                strErrMsg = string.Format("SQL Error #:{0} \nMessage:{1}\n ", sqlError.Number.ToString(), sqlError.Message);
                Log.Write(strErrMsg, Log.Error, 1);
            }

            strErrMsg = string.Format("Stack Trace:{0}", ExSQL.StackTrace);
            Log.Write(strErrMsg, Log.Error, 1);

            RaiseErrorEvent(ExSQL);
            ContinueConstruction = false;
        }



        public void Dispose()
        {
            sqlConnLocates.Close();

            builderImportCommand.Dispose();
            sqlAdptrImport.Dispose();

            builderStatusCommand.Dispose();
            sqlAdptrStatus.Dispose();

            sqlCmdInsertInventoryData.Dispose();
            sqlCmdSubscriptionSiteSelect.Dispose();
            sqlCmdSubscriptionTypeSelect.Dispose();
            sqlCmdFilePatternSelect.Dispose();
            sqlCmdInventoryExecutionSelect.Dispose();

            sqlDataModelImportSites.Clear();
            sqlDataModelImportSites.Dispose();

            sqlConnLocates.Dispose();

            htImportsFTP.Clear();
            htImportsEmailAttachment.Clear();
            htImportsEmailBody.Clear();

            alImports.Clear();
            htImportIndex.Clear();

            htImportKeyValues.Clear();
        }


    }





    //-------------------------------------------------------------------------
    /// <summary>
    /// data class for storing Import Data
    /// </summary>
    public class ImportSpec : IDisposable
    {
        private DataRow rowImportSpec;
        private DataRow rowImportDesk;
        private DataRow rowImportType;
        private DataRow rowImportMask;
        private DataRow rowImportExecution;
        private DataRelationCollection relations;

        private ArrayList alInventoryEntries = null;
        private ArrayList alStrInventoryData = null;

        private long nSubscriberID = 0;
        private long nExecutionID = 0;

        private int nNumRecordsExpected = 0;
        private int nNumRecordsImported = 0;

        private string strDesk = "";
        private string strCountryCode = "";
        private string strSubscriptionType = "";
        private string strRootDir = "";
        private string strLocalDir = "";
        private string strLocalFilePath = "";
        private string strRemoteFilePath = "";
        private string strImportStatus = "";

        private DateTime dtImportDateTime;
        private DateTime dtFileCheckTime;
        private DateTime dtFileModifiedTime;
        private DateTime dtBizDateLastImported;
        private DateTime dtBizDateProposed;

        private bool bIsEnabled = false;
        private bool bIsRunning = false;

        private bool bIsBizDatePrior = false;
        private bool bIsDownloadSuccessful = false;
        private bool bHasImportData = false;

        // Regexes
        private string strRegexHeader = "";
        private string strRegexData = "";
        private string strRegexTrailer = "";
        private string strRegexAccount = "";
        private string strRegexDate = "";
        private string strRegexRowCount = "";


        // Primary Attributes
        public long SubscriberID
        {
            get { return nSubscriberID; }
        }

        public long ExecutionID
        {
            get { return nExecutionID; }
            set { nExecutionID = value; }
        }
        public string SubscriptionType
        {
            get { return strSubscriptionType; }
        }
        public string Desk
        {
            get { return strDesk; }
        }
        public string CountryCode
        {
            get { return strCountryCode; }
            set { strCountryCode = value; }
        }

        // Data Attributes
        public DataRow ImportSpecRow
        { get { return rowImportSpec; } }

        public DataRow ImportTypeRow
        { get { return rowImportType; } }

        public DataRow ImportMaskRow
        { get { return rowImportMask; } }

        public DataRow ImportExecutionRow
        {
            get { return rowImportExecution; }
            set { rowImportExecution = value; }
        }


        public DataRelationCollection ImportRelations
        { get { return relations; } }

        public ArrayList InventoryImportText
        {
            get { return alStrInventoryData; }
            set { alStrInventoryData = value; }
        }
        public ArrayList InventoryEntryObjects
        {
            get { return alInventoryEntries; }
            set { alInventoryEntries = value; }
        }
        public DateTime ImportDateTime
        {
            get { return dtImportDateTime; }
            set { dtImportDateTime = value; }
        }

        public string RootDir
        {
            get { return strRootDir; }
            set { strRootDir = value; }
        }
        public string LocalDir
        {
            get { return strLocalDir; }
            set
            {
                strLocalDir = value;
                if (!string.IsNullOrEmpty(strRemoteFilePath))
                {
                    FileInfo RemoteFileInfo = new FileInfo(strRemoteFilePath);
                    strLocalFilePath = strLocalDir + RemoteFileInfo.Name;
                }
            }
        }
        public string LocalFilePath
        {
            get { return strLocalFilePath; }
            set
            {
                strLocalFilePath = value;
                if (!string.IsNullOrEmpty(LocalFilePath))
                {
                    FileInfo LocalFileInfo = new FileInfo(strLocalFilePath);
                    strLocalDir = LocalFileInfo.DirectoryName;
                }
            }
        }
        public string RemoteFilePath
        {
            get { return strRemoteFilePath; }
            set
            {
                strRemoteFilePath = value;
                if (null != rowImportSpec) { rowImportSpec["FilePathName"] = strRemoteFilePath; }
            }
        }


        // State Bits
        public bool IsEnabled
        {
            get { return bIsEnabled; }
        }

        public bool IsRunning
        {
            get { return bIsRunning; }
            set
            {
                bIsRunning = value;
                if (null != rowImportSpec) { rowImportSpec["IsRunning"] = bIsRunning; }
            }
        }


        public bool IsBizDatePrior
        {
            get { return bIsBizDatePrior; }
            set
            {
                bIsBizDatePrior = value;
                if (null != rowImportSpec) { rowImportSpec["IsBizDatePrior"] = bIsBizDatePrior; }
            }
        }
        public bool HasImportData
        {
            get { return bHasImportData; }
            set
            {
                bHasImportData = value;
            }
        }
        public bool IsDownloadSuccessful
        {
            get { return bIsDownloadSuccessful; }
            set { bIsDownloadSuccessful = value; }
        }


        // State Attributes
        public string ImportStatus
        {
            get { return strImportStatus; }
            set
            {
                strImportStatus = value;
                if (null != rowImportSpec) { rowImportSpec["FileStatus"] = strImportStatus; }
                if (null != rowImportExecution) { rowImportExecution["ExecutionStatus"] = strImportStatus; }
            }
        }

        public int NumRecordsExpected
        {
            get { return nNumRecordsExpected; }
            set { nNumRecordsExpected = value; }
        }
        public int NumRecordsImported
        {
            get { return nNumRecordsImported; }
            set
            {
                nNumRecordsImported = value;

                if (null != rowImportSpec) { rowImportSpec["LoadCount"] = nNumRecordsImported; }
                if (null != rowImportExecution) { rowImportExecution["ExecutionRecordsImported"] = nNumRecordsImported; }
            }
        }
        public DateTime FileCheckTime
        {
            get { return dtFileCheckTime; }
            set
            {
                dtFileCheckTime = value;
                if (null != rowImportSpec) { rowImportSpec["FileCheckTime"] = dtFileCheckTime; }
            }
        }
        public DateTime FileModifiedTime
        {
            get { return dtFileModifiedTime; }
            set
            {
                dtFileModifiedTime = value;
                if (null != rowImportSpec) { rowImportSpec["FileTime"] = dtFileModifiedTime; }
                if (null != rowImportExecution) { rowImportExecution["FileTime"] = dtFileModifiedTime; }
            }
        }

        public DateTime BizDateLastImported
        {
            get { return dtBizDateLastImported; }
            set
            {
                dtBizDateLastImported = value;
                if (null != rowImportSpec) { rowImportSpec["BizDate"] = dtBizDateLastImported; }
                if (null != rowImportExecution) { rowImportExecution["BizDate"] = dtBizDateLastImported; }
            }
        }
        public DateTime BizDateProposedImport
        {
            get { return dtBizDateProposed; }
            set { dtBizDateProposed = value; }
        }

        // Regexes
        public string RegexHeader
        {
            get { return strRegexHeader; }
        }
        public string RegexData
        {
            get { return strRegexData; }
        }
        public string RegexTrailer
        {
            get { return strRegexTrailer; }
        }
        public string RegexAccount
        {
            get { return strRegexAccount; }
        }
        public string RegexDate
        {
            get { return strRegexDate; }
        }
        public string RegexRowCount
        {
            get { return strRegexRowCount; }
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// C'Tor
        /// </summary>
        /// <param name="ImportSpecRow"></param>
        public ImportSpec(DataRow ImportSpecRow, DataRelationCollection Relations)
        {
            try
            {
                // These 2 collections work together to Import the data
                InventoryImportText = new ArrayList();   // Contains Raw Un-Parsed Text
                alInventoryEntries = new ArrayList();    // Contains Parsed data stored in DB objects

                rowImportSpec = ImportSpecRow;
                relations = Relations;

                nSubscriberID = (long)ImportSpecRow["InventorySubscriberID"];
                bIsEnabled = (bool)ImportSpecRow["IsEnabled"];

                strDesk = ImportSpecRow["Desk"].ToString();
                strRemoteFilePath = ImportSpecRow["FilePathName"].ToString();

                bIsBizDatePrior = (bool)ImportSpecRow["IsBizDatePrior"];

                if (typeof(DBNull) != rowImportSpec["BizDate"].GetType())
                { dtBizDateLastImported = (System.DateTime)rowImportSpec["BizDate"]; }
                else
                { dtBizDateLastImported = DateTime.Parse("1-1-1900"); }

                dtBizDateProposed = DateTime.Parse("1-1-1900");
                dtFileModifiedTime = (DateTime)ImportSpecRow["FileTime"];

                RootDir = StockLoan.Common.Standard.ConfigValue("InventoryLocalDestination");

                // Build Remote File Name Including Date
                strRemoteFilePath = InventoryImportController.ParseImportFileDate(strRemoteFilePath, bIsBizDatePrior);

                // Get Local File Path from Remote Name
                if ((null != strRemoteFilePath) && ("" != strRemoteFilePath))
                {
                    FileInfo RemoteFileInfo = new FileInfo(strRemoteFilePath);
                    strLocalFilePath = strLocalDir + RemoteFileInfo.Name;
                }
                string strImportTypeRelationName = StockLoan.Common.Standard.ConfigValue("ImportTypeRelationName");
                DataRelation relationImportType = relations[strImportTypeRelationName];
                rowImportType = rowImportSpec.GetParentRow(relationImportType);
                strSubscriptionType = rowImportType["InventorySubscriptionTypeName"].ToString();

                string strImportMaskRelationName = StockLoan.Common.Standard.ConfigValue("ImportFilePatternRelationName");
                DataRelation relationMask = relations[strImportMaskRelationName];
                rowImportMask = rowImportSpec.GetParentRow(relationMask);

                string strImportDeskRelationName = StockLoan.Common.Standard.ConfigValue("ImportDeskRelationName");
                DataRelation relationDesk = relations[strImportDeskRelationName];
                rowImportDesk = rowImportSpec.GetParentRow(relationDesk);

                strCountryCode = rowImportDesk["CountryCode"].ToString();

                strRegexHeader = rowImportMask["HeaderRegEx"].ToString();
                strRegexData = rowImportMask["DataRegEx"].ToString();
                strRegexTrailer = rowImportMask["TrailerRegEx"].ToString();
                strRegexAccount = rowImportMask["AccountRegEx"].ToString();
                strRegexDate = rowImportMask["DateRegEx"].ToString();
                strRegexRowCount = rowImportMask["RowCountRegEx"].ToString();
            }
            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [ImportSpec.Constructor]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportModel.Master]", Log.Error, 1);
                Log.Write(ex.StackTrace, Log.Error, 1);

                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            alInventoryEntries.Clear();
            alStrInventoryData.Clear();
        }


        public bool AssertLocalDir()
        {
            bool bReturnCreated = false;
            try
            {
                DirectoryInfo DestinationDirInfo = new DirectoryInfo(strLocalDir);
                if (!DestinationDirInfo.Exists)
                {
                    DestinationDirInfo.Create();
                    bReturnCreated = true;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [ImportSpec.AssertLocalDir]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return bReturnCreated;
        }

    }


    //-------------------------------------------------------------------------
    public class FTPImportSpec
    {
        private ImportSpec importSpec;

        internal string strFTPServer;
        internal string strFTPUserName;
        internal string strFTPPassword;

        public ImportSpec ImportSpecification
        {
            get { return importSpec; }
        }

        public string FTPServer
        {
            get { return strFTPServer; }
            set
            {
                strFTPServer = value;
                importSpec.ImportSpecRow["FileHost"] = strFTPServer;
            }
        }
        public string FTPUserName
        {
            get { return strFTPUserName; }
            set
            {
                strFTPUserName = value;
                importSpec.ImportSpecRow["FileUserName"] = strFTPUserName;
            }
        }
        public string FTPPassword
        {
            get { return strFTPPassword; }
            set
            {
                strFTPPassword = value;
                importSpec.ImportSpecRow["FilePassword"] = strFTPPassword;
            }
        }

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="ImportSpecification"></param>
        public FTPImportSpec(ImportSpec ImportSpecification)
        {
            importSpec = ImportSpecification;
            InitializeComponent(importSpec.ImportSpecRow, importSpec.ImportRelations);
        }
        public FTPImportSpec(DataRow ImportSpecRow, DataRelationCollection Relations)
        {
            importSpec = new ImportSpec(ImportSpecRow, Relations);
            InitializeComponent(ImportSpecRow, Relations);
        }
        internal void InitializeComponent(DataRow ImportSpecRow, DataRelationCollection Relations)
        {
            try
            {
                importSpec.RootDir += StockLoan.Common.Standard.ConfigValue("InventoryFTPSubDir");

                strFTPServer = importSpec.ImportSpecRow["FileHost"].ToString();
                strFTPUserName = importSpec.ImportSpecRow["FileUserName"].ToString();
                strFTPPassword = importSpec.ImportSpecRow["FilePassword"].ToString();

            }
            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [FTPImportSpec.InitializeComponent]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [FTPImportSpec.InitializeComponent]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }



    }



    //-------------------------------------------------------------------------
    public class EmailImportSpec
    {
        private ImportSpec importSpec;
        internal string strMailAddress = "";
        internal string strMailSubject = "";

        public ImportSpec ImportSpecification
        {
            get { return importSpec; }
        }
        public string MailAddress
        {
            get { return strMailAddress; }
            set
            {
                strMailAddress = value;
                importSpec.ImportSpecRow["MailAddress"] = strMailAddress;
            }
        }
        public string MailSubject
        {
            get { return strMailSubject; }
            set
            {
                strMailSubject = value;
                importSpec.ImportSpecRow["MailSubject"] = strMailSubject;
            }
        }
        public EmailImportSpec(ImportSpec ImportSpecification)
        {
            importSpec = ImportSpecification;
            InitializeComponent(importSpec.ImportSpecRow, importSpec.ImportRelations);
        }
        public EmailImportSpec(DataRow ImportSpecRow, DataRelationCollection Relations)
        {
            importSpec = new ImportSpec(ImportSpecRow, Relations);
            InitializeComponent(ImportSpecRow, Relations);
        }
        internal void InitializeComponent(DataRow ImportSpecRow, DataRelationCollection Relations)
        {
            try
            {
                importSpec.RootDir += StockLoan.Common.Standard.ConfigValue("InventoryEmailSubDir");

                strMailAddress = importSpec.ImportSpecRow["MailAddress"].ToString();
                strMailSubject = InventoryImportController.ParseImportFileDate(importSpec.ImportSpecRow["MailSubject"].ToString(), importSpec.IsBizDatePrior);
            }
            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " EmailImportSpec.InitializeComponent]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " EmailImportSpec.InitializeComponent]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }


    }


    public class ImportHoliday
    {
        private DataRow rowHoliday;
        private DateTime dtHolidayDate;
        private string strCountryCode = "";
        private string strDescription = "";
        private bool bIsBankHoliday = false;
        private bool bIsExchangeHoliday = false;
        private bool bIsBizDateHoliday = false;
        private string strActUserId = "";
        private DateTime dtActTime;
        private bool bIsActive = false;


        internal DateTime HolidayDate
        {
            get { return dtHolidayDate; }
            set { dtHolidayDate = value; }
        }
        internal string CountryCode
        {
            get { return strCountryCode; }
            set { strCountryCode = value; }
        }
        internal string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        internal bool IsBankHoliday
        {
            get { return bIsBankHoliday; }
            set { bIsBankHoliday = value; }
        }
        internal bool IsBizDateHoliday
        {
            get { return bIsBizDateHoliday; }
            set { bIsBizDateHoliday = value; }
        }
        internal bool IsExchangeHoliday
        {
            get { return bIsExchangeHoliday; }
            set { bIsExchangeHoliday = value; }
        }
        internal string ActUserId
        {
            get { return strActUserId; }
            set { strActUserId = value; }
        }
        internal DateTime ActTime
        {
            get { return dtActTime; }
            set { dtActTime = value; }
        }
        internal bool IsActive
        {
            get { return bIsActive; }
            set { bIsActive = value; }
        }



        public ImportHoliday(DataRow HolidayRow)
        {
            try
            {
                rowHoliday = HolidayRow;

                dtHolidayDate = (DateTime)rowHoliday["HolidayDate"];
                strCountryCode = rowHoliday["CountryCode"].ToString();
                strDescription = rowHoliday["Description"].ToString();
                bIsBankHoliday = (bool)rowHoliday["IsBankHoliday"];
                bIsExchangeHoliday = (bool)rowHoliday["IsExchangeHoliday"];
                bIsBizDateHoliday = (bool)rowHoliday["IsBizDateHoliday"];
                strActUserId = rowHoliday["ActUserId"].ToString();
                dtActTime = (DateTime)rowHoliday["ActTime"];
                bIsActive = (bool)rowHoliday["IsActive"];
            }
            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " ImportSpec.Constructor]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " InventoryImportModel.ImportHoliday_Constructor]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }





    }
}
