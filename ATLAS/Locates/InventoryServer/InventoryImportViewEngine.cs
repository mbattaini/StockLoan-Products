using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using System.Collections;
using StockLoan.Common;

namespace StockLoan.Inventory
{
    class InventoryImportViewEngine
    {

        InventoryImportModel modelImports;

        // Keep Separate Dataset for Each View to be Rendered
        private DataSet sqlDataSubscriptionSites;
        private DataSet sqlDataImportExecutions;
        private DataSet sqlDataFilePatterns;
        private DataSet sqlDataImportTypes;
        private DataSet sqlDataImportDesks;

        public InventoryImportViewEngine(InventoryImportModel ImportModel)
        {
            modelImports = ImportModel;
            sqlDataSubscriptionSites = new DataSet("Subscriptions");
            sqlDataImportExecutions = new DataSet("Executions");
            sqlDataFilePatterns = new DataSet("Patterns");
            sqlDataImportTypes = new DataSet("SubscriptionTypes");
            sqlDataImportDesks = new DataSet("ImportDesks");
        }

        internal InventoryImportModel InventoryImportDataModel
        {
            get { return modelImports; }
            set { modelImports = value; }
        }


        public DataTable SubscriptionSites
        {
            get
            {
                InventoryImportModel.mtxImportModel.WaitOne();
                try
                {
                    sqlDataSubscriptionSites.Clear();
                    modelImports.sqlAdptrImport.SelectCommand = modelImports.sqlCmdSubscriptionSiteSelect;
                    modelImports.sqlAdptrImport.Fill(sqlDataSubscriptionSites, modelImports.SubscriptionSitesTableName);

                    DataTable sqlTableSubscription = sqlDataSubscriptionSites.Tables[modelImports.SubscriptionSitesTableName];
                    DataColumn[] arrayColumnPrimaryKeySubscriptions = { sqlTableSubscription.Columns["InventorySubscriberID"] };
                    sqlTableSubscription.PrimaryKey = arrayColumnPrimaryKeySubscriptions;

                }// end try

                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportViewEngine.SubscriptionSites]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportViewEngine.SubscriptionSites]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                }

                InventoryImportModel.mtxImportModel.ReleaseMutex();
                return sqlDataSubscriptionSites.Tables[modelImports.SubscriptionSitesTableName];

            }
        }
        public DataTable ImportExecutions
        {
            get
            {
                InventoryImportModel.mtxImportModel.WaitOne();
                try
                {
                    sqlDataImportExecutions.Clear();
                    modelImports.sqlAdptrStatus.SelectCommand = modelImports.sqlCmdInventoryExecutionSelect;
                    modelImports.sqlAdptrStatus.Fill(sqlDataImportExecutions, modelImports.SubscriptionSitesTableName);
                }// end try

                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportViewEngine.ImportExecutions]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportViewEngine.ImportExecutions]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                }

                InventoryImportModel.mtxImportModel.ReleaseMutex();
                return sqlDataImportExecutions.Tables[modelImports.ImportExecutionTableName];

            }
        }
        public DataTable FilePatterns
        {
            get
            {
                InventoryImportModel.mtxImportModel.WaitOne();
                try
                {
                    sqlDataFilePatterns.Clear();
                    modelImports.sqlAdptrImport.SelectCommand = modelImports.sqlCmdFilePatternSelect;
                    modelImports.sqlAdptrImport.Fill(sqlDataFilePatterns, modelImports.FilePatternTableName);
                }// end try

                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportViewEngine.FilePatterns]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportViewEngine.FilePatterns]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                }
                InventoryImportModel.mtxImportModel.ReleaseMutex();
                return sqlDataFilePatterns.Tables[modelImports.FilePatternTableName];
            }
        }
        public DataTable SubscriptionTypes
        {
            get
            {
                InventoryImportModel.mtxImportModel.WaitOne();
                try
                {
                    sqlDataImportTypes.Clear();
                    modelImports.sqlAdptrImport.SelectCommand = modelImports.sqlCmdSubscriptionTypeSelect;
                    modelImports.sqlAdptrImport.Fill(sqlDataImportTypes, modelImports.SubscriptionTypesTableName);
                }// end try

                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportViewEngine.SubscriptionTypes]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportViewEngine.SubscriptionTypes]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                }
                InventoryImportModel.mtxImportModel.ReleaseMutex();
                return sqlDataImportTypes.Tables[modelImports.SubscriptionTypesTableName];
            }
        }

        public DataTable ImportDesks
        {
            get
            {
                InventoryImportModel.mtxImportModel.WaitOne();
                try
                {
                    sqlDataImportDesks.Clear();
                    modelImports.sqlAdptrImport.SelectCommand = modelImports.sqlCmdDeskSelect;
                    modelImports.sqlAdptrImport.Fill(sqlDataImportDesks, modelImports.ImportDesksTableName);
                }
                catch (SqlException sqlExcptn)
                {
                    foreach (SqlError sqlError in sqlExcptn.Errors)
                    {
                        string strSQLErrorNum = sqlError.Number.ToString();
                        Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [InventoryImportViewEngine.ImportDesks]", Log.Error, 1);
                        Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [InventoryImportViewEngine.ImportDesks]", Log.Error, 1);
                    Console.WriteLine(ex.Message);
                }
                InventoryImportModel.mtxImportModel.ReleaseMutex();
                return sqlDataImportDesks.Tables[modelImports.ImportDesksTableName];
            }
        }




        public void Dispose()
        {
            modelImports = null;
            sqlDataImportExecutions.Clear();
            sqlDataFilePatterns.Clear();
            sqlDataSubscriptionSites.Clear();

            sqlDataImportExecutions.Dispose();
            sqlDataFilePatterns.Dispose();
            sqlDataSubscriptionSites.Dispose();
        }



    }
}
