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
    /// Data Model Builds ImportSpec Objects to Represent Import Jobs
    /// Objets are accessed through the collections:
    ///     FTPImportSpecs, EmailBodyImportSpecs, EmailAttachmentImportSpecs 
    /// </summary>
    internal class InventoryImportModel
    {
        #region Internal Members

        private ImportMode enumModeImport;

        private ArrayList alImports;
        private Hashtable htImportIndex;

        private Hashtable htImportsFTP;
        private Hashtable htImportsEmailAttachment;
        private Hashtable htImportsEmailBody;

        private string strDBHost = "";
        private string strDBCatalog = "";
        private string strDBConn = "";
        private string strDBSelect = "";

        internal string strSubscriptionSitesTable = "";
        internal string strSubscriptionTypesTable = "";
        internal string strImportExecutionTable = "";
        internal string strInventoryFilePatternTable = "";
        internal string strInventoryDataEntryTable = "";
        internal string strImportKeyValueTable = "";
        internal string strImportDeskTable = "";

        internal string strOperationDescription = "";

        internal DataSet sqlDataModelImportSites;
        internal SqlConnection sqlConnLocates;

        internal SqlDataAdapter sqlAdptrImport;
        internal SqlCommandBuilder builderImportCommand;

        internal SqlDataAdapter sqlAdptrStatus;
        internal SqlCommandBuilder builderStatusCommand;

        internal SqlCommand sqlCmdInsertInventoryData;
        internal SqlCommand sqlCmdSubscriptionSiteSelect;
        internal SqlCommand sqlCmdSubscriptionTypeSelect;
        internal SqlCommand sqlCmdSubscriptionMaskSelect;
        internal SqlCommand sqlCmdInventoryExecutionSelect;
        internal SqlCommand sqlCmdDeskSelect;
        internal SqlCommand sqlCmdKeyValueSelect;

        internal bool bHasImportSiteSpecs = false;
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
                TimeSpan timeWait = TimeSpan.Parse("00:00:05");
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
                TimeSpan timeWait = TimeSpan.Parse("00:00:15");
                if (htImportKeyValues.ContainsKey("InventoryMainLoopRecycleIntervalNonBizDay"))
                {
                    string strWaitTime = htImportKeyValues["InventoryMainLoopRecycleIntervalNonBizDay"].ToString();
                    timeWait = TimeSpan.Parse(strWaitTime);
                }
                return timeWait;
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
        public Hashtable EmailAttachmentImportSpecs
        {
            get { return htImportsEmailAttachment; }
        }
        public Hashtable EmailBodyImportSpecs
        {
            get { return htImportsEmailBody; }
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
                    strInventoryFilePatternTable = StockLoan.Common.Standard.ConfigValue("InventoryMaskSelectFrom");
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
        public void InitializeComponents()
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

                        BuildSubscriptionPrimaryKey();
                        BuildSubscriptionTypes();
                        BuildFilePatterns();
                        BuildExecutions();
                        BuildDesks();
                        BuildKeyValues();
                        BuildRelationships();
                        BuildCollections();
                    }

                }// end if conn.open


            }// end try

            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.InitializeComponents]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
                ContinueConstruction = false;
                RaiseErrorEvent(sqlExcptn);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportModel.InitializeComponents]", Log.Error, 1);
                Console.WriteLine(ex.Message);
                ContinueConstruction = false;
                RaiseErrorEvent(ex);
            }
            finally
            {
                InventoryImportModel.mtxImportModel.ReleaseMutex();
            }


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
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " InventoryImportModel.BuildInsertSQL", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildInsertSQL]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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
                    string strSQLSubscriberFields = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectFields");
                    string strSQLSubscriberFrom = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectFrom");
                    string strSQLSubscriberWhereBatch = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectWhereBatch");
                    string strSQLSubscriberOrderBatch = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectOrderBatch");
                    string strSQLSubscriberWhereService = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectWhereService");
                    string strSQLSubscriberOrderService = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectOrderService");

                    strSubscriptionSitesTable = strSQLSubscriberFrom;

                    switch (ModeImport)
                    {
                        case ImportMode.Batch:
                            strDBSelect = string.Format("Select {0} From {1} (NOLOCK) Order By {2}",
                                                            strSQLSubscriberFields,
                                                            strSQLSubscriberFrom,
                                                            strSQLSubscriberOrderBatch
                                                        );
                            break;
                        case ImportMode.Service:
                            strDBSelect = string.Format("Select Top 1 {0} From {1} (NOLOCK) Where {2} Order By {3}",
                                                            strSQLSubscriberFields,
                                                            strSQLSubscriberFrom,
                                                            strSQLSubscriberWhereService,
                                                            strSQLSubscriberOrderService
                                                        );
                            break;
                        case ImportMode.Test:
                            strDBSelect = string.Format("Select Top 1 {0} From {1} (NOLOCK) Where {2} Order By {3}",
                                                            strSQLSubscriberFields,
                                                            strSQLSubscriberFrom,
                                                            strSQLSubscriberWhereService,
                                                            strSQLSubscriberOrderService
                                                        );
                            break;
                        default:
                            strDBSelect = string.Format("Select {0} From {1} (NOLOCK) Where {2} Order By {3}",
                                                            strSQLSubscriberFields,
                                                            strSQLSubscriberFrom,
                                                            strSQLSubscriberWhereService,
                                                            strSQLSubscriberOrderService
                                                        );
                            break;
                    }

                    OperationDescription += string.Format("{0}{0} SQL: {1}", System.Environment.NewLine, strDBSelect);

                    sqlCmdSubscriptionSiteSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdSubscriptionSiteSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdSubscriptionSiteSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strSubscriptionSitesTable);

                    if (0 < sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Rows.Count)
                    {
                        ContinueConstruction = true;
                        bHasImportSiteSpecs = true;
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
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " InventoryImportModel.BuildImportSpecs", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildImportSpecs]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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
                    string strSQLSubscriptionTypeFrom = StockLoan.Common.Standard.ConfigValue("InventorySubscriptionTypeSelectFrom");
                    string strSQLSubscriptionTypeFields = StockLoan.Common.Standard.ConfigValue("InventorySubscriptionTypeSelectFields");
                    strSubscriptionTypesTable = strSQLSubscriptionTypeFrom;

                    strDBSelect = "Select " + strSQLSubscriptionTypeFields + " From " + strSQLSubscriptionTypeFrom + " (NOLOCK);"; // +" Where " + strSQLSubscriptionTypeWhere + ";";
                    OperationDescription += string.Format("{0}{0} SQL: {1}", System.Environment.NewLine, strDBSelect);

                    sqlCmdSubscriptionTypeSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdSubscriptionTypeSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdSubscriptionTypeSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strSQLSubscriptionTypeFrom);
                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " InventoryImportModel.BuildSubscriptionTypes", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildSubscriptionTypes]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
                }
            }
        }

        private void BuildSubscriptionPrimaryKey()
        {
            if (ContinueConstruction)
            {
                try
                {
                    OperationDescription = "Adding Primary Key to tbInventorySubscriber (ImportSpec) Table";
                    DataTable sqlTableSubscription = sqlDataModelImportSites.Tables[strSubscriptionSitesTable];
                    DataColumn[] arrayColumnPrimaryKeySubscriptions = { sqlTableSubscription.Columns["InventorySubscriberID"] };
                    sqlTableSubscription.PrimaryKey = arrayColumnPrimaryKeySubscriptions;
                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " InventoryImportModel.BuildSubscriptionPrimaryKey", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildSubscriptionPrimaryKey]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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
                    string strSQLSubscriptionMaskFrom = StockLoan.Common.Standard.ConfigValue("InventoryMaskSelectFrom");
                    string strSQLSubscriptionMaskFields = StockLoan.Common.Standard.ConfigValue("InventoryMaskSelectFields");
                    strInventoryFilePatternTable = strSQLSubscriptionMaskFrom;

                    strDBSelect = "Select " + strSQLSubscriptionMaskFields + " From " + strSQLSubscriptionMaskFrom + " (NOLOCK);"; // +" Where " + strSQLSubscriptionMaskWhere + ";";
                    OperationDescription += string.Format("{0}{0} SQL: {1}", System.Environment.NewLine, strDBSelect);

                    sqlCmdSubscriptionMaskSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdSubscriptionMaskSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdSubscriptionMaskSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strSQLSubscriptionMaskFrom);

                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " InventoryImportModel.BuildFilePatterns", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildFilePatterns]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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
                    string strSQLInventoryExecutionFields = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectFields");
                    string strSQLInventoryExecutionFrom = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectFrom");
                    string strSQLInventoryExecutionOrderBy = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectOrderBy");
                    string strSQLInventoryExecutionWhereBatch = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectWhereBatch");
                    string strSQLInventoryExecutionWhereService = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectWhereService");

                    strImportExecutionTable = strSQLInventoryExecutionFrom;
                    long nSubscriberID = (long)sqlDataModelImportSites.Tables[strSubscriptionSitesTable].Rows[0]["InventorySubscriberID"];

                    switch (ModeImport)
                    {
                        case ImportMode.Batch:
                            strDBSelect = string.Format("Select {0} From {1} (NOLOCK) Where {2} Order By {3}",
                                                            strSQLInventoryExecutionFields,
                                                            strSQLInventoryExecutionFrom,
                                                            strSQLInventoryExecutionWhereBatch,
                                                            strSQLInventoryExecutionOrderBy
                                                        );
                            break;
                        case ImportMode.Service:

                            strDBSelect = string.Format("Select Top 1 {0} From {1} (NOLOCK) Where {2}{3} Order By {4}",
                                                            strSQLInventoryExecutionFields,
                                                            strSQLInventoryExecutionFrom,
                                                            strSQLInventoryExecutionWhereService,
                                                            nSubscriberID,
                                                            strSQLInventoryExecutionOrderBy
                                                        );
                            break;
                        case ImportMode.Test:
                            strDBSelect = string.Format("Select Top 1 {0} From {1} (NOLOCK) Where {2}{3} Order By {4}",
                                                            strSQLInventoryExecutionFields,
                                                            strSQLInventoryExecutionFrom,
                                                            strSQLInventoryExecutionWhereService,
                                                            nSubscriberID,
                                                            strSQLInventoryExecutionOrderBy
                                                        );
                            break;
                        default:
                            strDBSelect = string.Format("Select {0} From {1} (NOLOCK) Where {2} Order By {3}",
                                                            strSQLInventoryExecutionFields,
                                                            strSQLInventoryExecutionFrom,
                                                            strSQLInventoryExecutionWhereBatch,
                                                            strSQLInventoryExecutionOrderBy
                                                        );
                            break;
                    }


                    OperationDescription += string.Format("{0}{0} SQL: {1}", System.Environment.NewLine, strDBSelect);

                    sqlCmdInventoryExecutionSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdInventoryExecutionSelect.CommandType = CommandType.Text;

                    sqlAdptrStatus.SelectCommand = sqlCmdInventoryExecutionSelect;
                    sqlAdptrStatus.UpdateCommand = builderStatusCommand.GetUpdateCommand(true);
                    sqlAdptrStatus.Fill(sqlDataModelImportSites, strImportExecutionTable);

                    DataTable sqlTableExecutions = sqlDataModelImportSites.Tables[strSQLInventoryExecutionFrom];
                    DataColumn[] arrayColumnPrimaryKeyExecutions = { sqlTableExecutions.Columns["InventoryImportExecutionID"] };
                    sqlTableExecutions.PrimaryKey = arrayColumnPrimaryKeyExecutions;



                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.BuildExecutions]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildExecutions]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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
                    string strSQLDeskSelectFrom = StockLoan.Common.Standard.ConfigValue("InventoryDeskSelectFrom");
                    string strSQLDeskSelectFields = StockLoan.Common.Standard.ConfigValue("InventoryDeskSelectFields");
                    string strSQLDeskSelectWhere = StockLoan.Common.Standard.ConfigValue("InventoryDeskSelectWhere");

                    strImportDeskTable = strSQLDeskSelectFrom;

                    strDBSelect = "Select " + strSQLDeskSelectFields + " From " + strSQLDeskSelectFrom + " (NOLOCK);"; //+ " Where " + strSQLDeskSelectWhere + ";";
                    OperationDescription += string.Format("{0}{0} SQL: {1}", System.Environment.NewLine, strDBSelect);

                    sqlCmdDeskSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdDeskSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdDeskSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strImportDeskTable);

                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.BuildDesks]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildDesks]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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
                    string strSQLKeyValueSelectFrom = StockLoan.Common.Standard.ConfigValue("InventoryKeyValueSelectFrom");
                    string strSQLKeyValueSelectFields = StockLoan.Common.Standard.ConfigValue("InventoryKeyValueSelectFields");
                    string strSQLKeyValueSelectWhere = StockLoan.Common.Standard.ConfigValue("InventoryKeyValueSelectWhere");

                    strImportKeyValueTable = strSQLKeyValueSelectFrom;

                    strDBSelect = "Select " + strSQLKeyValueSelectFields + " From " + strSQLKeyValueSelectFrom + " (NOLOCK) ;";// + " Where " + strSQLKeyValueSelectWhere + ";";
                    OperationDescription += string.Format("{0}{0} SQL: {1}", System.Environment.NewLine, strDBSelect);

                    sqlCmdKeyValueSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                    sqlCmdKeyValueSelect.CommandType = CommandType.Text;

                    sqlAdptrImport.SelectCommand = sqlCmdKeyValueSelect;
                    sqlAdptrImport.Fill(sqlDataModelImportSites, strImportKeyValueTable);
                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.BuildKeyValues]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildKeyValues]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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

                    string strImportMaskRelationName = StockLoan.Common.Standard.ConfigValue("ImportMaskRelationName");
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

                    string strDeskSubscriberRelationName = StockLoan.Common.Standard.ConfigValue("InventoryDeskSubscriberRelationName");
                    DataRelation relationDeskSubscriptions = new DataRelation(strDeskSubscriberRelationName, columnParentDesk, columnChildSubscriberDesk);
                    sqlDataModelImportSites.Relations.Add(relationDeskSubscriptions);

                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.BuildRelationships]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildRelationships]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
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
                                    htImportsEmailBody.Add(nID, CurrentSpecEmailBody);
                                    break;
                                case "EmailAttachment":
                                    EmailImportSpec CurrentSpecEmailAttachment = new EmailImportSpec(ActiveSpec);
                                    htImportsEmailAttachment.Add(nID, CurrentSpecEmailAttachment);
                                    break;
                            }
                        }
                    }

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
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.BuildCollections]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                    RaiseErrorEvent(sqlExcptn);
                    ContinueConstruction = false;
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportModel.BuildCollections]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                    RaiseErrorEvent(ex);
                    ContinueConstruction = false;
                }
            }
        }


        //------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------
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
                Log.Write(ex.Message + " [InventoryImportModel.IndexHoliday]", Log.Error, 1);
                Console.WriteLine(ex.Message);
                RaiseErrorEvent(ex);
            }
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
            sqlCmdSubscriptionMaskSelect.Dispose();
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

        #endregion





        public void UpdateImportSpecs()
        {
            try
            {
                OperationDescription = "Updating InventorySubscriber (ImportSpecs)";

                if ((null == strSubscriptionSitesTable) || ("" == strSubscriptionSitesTable))
                {
                    strSubscriptionSitesTable = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectFrom");
                }
                sqlAdptrImport.SelectCommand = sqlCmdSubscriptionSiteSelect;
                sqlAdptrImport.UpdateCommand = builderImportCommand.GetUpdateCommand(true);
                int nImportRowsUpdated = sqlAdptrImport.Update(sqlDataModelImportSites, strSubscriptionSitesTable);

                OperationDescription = "Updating ImportExecution (Historical Runs)";
                if ((null == strImportExecutionTable) || ("" == strImportExecutionTable))
                {
                    strImportExecutionTable = StockLoan.Common.Standard.ConfigValue("InventoryExecutionSelectFrom");
                }
                int nStatusRowsUpdated = sqlAdptrStatus.Update(sqlDataModelImportSites, strImportExecutionTable);


            }
            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.UpdateImportSpecs]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
                RaiseErrorEvent(sqlExcptn);

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportModel.UpdateImportSpecs]", Log.Error, 1);
                Console.WriteLine(ex.Message);
                RaiseErrorEvent(ex);
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
                ImportSpecification.LocalDir += nExecutionID.ToString() + "\\";

                // Update Whole Model Dataset to Retrieve New Execution
                if ((null == strSubscriptionSitesTable) || ("" == strSubscriptionSitesTable))
                {
                    strImportExecutionTable = StockLoan.Common.Standard.ConfigValue("InventorySubscriberSelectFrom");
                }
                sqlAdptrStatus.SelectCommand = sqlCmdInventoryExecutionSelect;
                sqlAdptrStatus.Fill(sqlDataModelImportSites, strImportExecutionTable);

                // Use ID returned by StoredProcedure to Fetch the Whole Row
                ImportSpecification.ImportExecutionRow = sqlDataModelImportSites.Tables[strImportExecutionTable].Rows.Find(nExecutionID);
            }
            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.GenerateImportExecution]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
                RaiseErrorEvent(sqlExcptn);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportModel.GenerateImportExecution]", Log.Error, 1);
                Console.WriteLine(ex.Message);
                RaiseErrorEvent(ex);
            }
        }


        public void ImportInventoryData(ImportSpec ImportSpecification)
        {
            OperationDescription = "Importing Final Data into Inventory Table from ImportSpec";
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

            }

            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportModel.ImportInventoryData]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
                RaiseErrorEvent(sqlExcptn);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportModel.ImportInventoryData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
                RaiseErrorEvent(ex);
            }

            InventoryImportController.mtxImportDataEntry.ReleaseMutex();
        }



    }





    //-------------------------------------------------------------------------
    /// <summary>
    /// data class for storing Import Data
    /// </summary>
    public class ImportSpec
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
        private string strLocalDir = "";
        private string strLocalFilePath = "";
        private string strRemoteFilePath = "";
        private string strImportStatus = "";

        private DateTime dtImportDateTime;
        private DateTime dtFileCheckTime;
        private DateTime dtFileModifiedTime;
        private DateTime dtBizDateSpecified;

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

        public DateTime BizDateSpecified
        {
            get { return dtBizDateSpecified; }
            set { dtBizDateSpecified = value; }
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
                dtBizDateSpecified = DateTime.Parse("1-1-1900");
                dtFileModifiedTime = (DateTime)ImportSpecRow["FileTime"];

                LocalDir = StockLoan.Common.Standard.ConfigValue("InventoryLocalDestination");

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

                string strImportMaskRelationName = StockLoan.Common.Standard.ConfigValue("ImportMaskRelationName");
                DataRelation relationMask = relations[strImportMaskRelationName];
                rowImportMask = rowImportSpec.GetParentRow(relationMask);

                string strImportDeskRelationName = StockLoan.Common.Standard.ConfigValue("InventoryDeskSubscriberRelationName");
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
                importSpec.LocalDir += StockLoan.Common.Standard.ConfigValue("InventoryFTPSubDir");

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
                importSpec.LocalDir += StockLoan.Common.Standard.ConfigValue("InventoryEmailSubDir");

                strMailAddress = importSpec.ImportSpecRow["MailAddress"].ToString();
                strMailSubject = importSpec.ImportSpecRow["MailSubject"].ToString();
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
