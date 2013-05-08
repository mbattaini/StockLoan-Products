using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.Inventory
{
    class InventoryImportMessageEngine
    {

        private InventoryImportController controller;


        public InventoryImportMessageEngine(InventoryImportController Controller)
        {
            controller = Controller;
        }




        //---------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region Error and Status Reporting Methods


        internal string GenerateStatusMessage(string Message, FTPImportSpec importSpecFTP)
        {
            string strStatus = string.Format("Importing ID:{1}; Desk: {2}; Source: {3}; Path: {4}; Status: {5}",
                                                            System.Environment.NewLine,
                                                            importSpecFTP.ImportSpecification.SubscriberID,
                                                            importSpecFTP.ImportSpecification.Desk,
                                                            importSpecFTP.FTPServer,
                                                            importSpecFTP.ImportSpecification.RemoteFilePath,
                                                            Message
                                                          );
            return strStatus;
        }

        internal string GenerateStatusMessage(string Message, EmailImportSpec importSpecEmail)
        {
            string strStatus = "";
            switch (importSpecEmail.ImportSpecification.SubscriptionType)
            {
                case "EmailAttachment":
                    strStatus = string.Format("Importing ID:{0}; Desk:{1}; Message Subject: {2}; Message Sender: {3}; Attachment: {4}; Status: {5}",
                                                importSpecEmail.ImportSpecification.SubscriberID,
                                                importSpecEmail.ImportSpecification.Desk,
                                                importSpecEmail.MailSubject,
                                                importSpecEmail.MailAddress,
                                                importSpecEmail.ImportSpecification.RemoteFilePath,
                                                Message
                                              );

                    break;

                case "EmailBody":
                    strStatus = string.Format("Importing ID:{0}; Desk:{1}; Message Subject: {2}; Message Sender: {3}; Status: {4}",
                                                importSpecEmail.ImportSpecification.SubscriberID,
                                                importSpecEmail.ImportSpecification.Desk,
                                                importSpecEmail.MailSubject,
                                                importSpecEmail.MailAddress,
                                                Message
                                              );

                    break;

                default:
                    strStatus = string.Format("Importing ID:{0}; Desk:{1}; Message Subject: {2}; Message Sender: {3}; Status: {4}",
                                                importSpecEmail.ImportSpecification.SubscriberID,
                                                importSpecEmail.ImportSpecification.Desk,
                                                importSpecEmail.MailSubject,
                                                importSpecEmail.MailAddress,
                                                Message
                                              );
                    break;
            }

            return strStatus;
        }

        internal string GenerateStatusMessage(string Message, ImportSpec importSpecEmail)
        {
            string strStatus = string.Format("Importing ID:{1}; Desk:{2};   {0}.",
                            Message,
                            importSpecEmail.SubscriberID,
                            importSpecEmail.Desk
                            );
            return strStatus;
        }

        internal string GenerateErrorMessage(ImportSpec ImportSpecification, string Message)
        {
            string strStatus = Message;
            try
            {
                strStatus += string.Format("{0}{0}Occurred While Importing {1} Data for SubscriberID: {2}; {0}{0}During ExecutionID: {3}; {0}Desk: {4} {0}File Path: {5}{0}{0}",
                                            System.Environment.NewLine,
                                            ImportSpecification.SubscriptionType,
                                            ImportSpecification.SubscriberID,
                                            ImportSpecification.ExecutionID,
                                            ImportSpecification.Desk,
                                            ImportSpecification.RemoteFilePath
                                          );
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strStatus;
        }



        internal string GenerateErrorMessage(ImportSpec ImportSpec, Exception Ex)
        {
            string strStatus = "";
            try
            {
                strStatus = string.Format("Exception of type {1} Occurred While Importing ID: {2}; {0}During Execution ID:{3}; {0}Desk:{4};  {0}{0}Error Message:{5}{0}{0}Stack Trace:{6}{0}{0}",
                                             System.Environment.NewLine,
                                             Ex.GetType(),
                                             ImportSpec.SubscriberID,
                                             ImportSpec.ExecutionID,
                                             ImportSpec.Desk,
                                             Ex.Message,
                                             Ex.StackTrace
                                          );
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strStatus;
        }



        internal string GenerateErrorMessage(FTPImportSpec ImportSpecFTP, string Message)
        {
            string strStatus = Message;
            try
            {
                strStatus += string.Format("{0}{0}Occurred While Importing ID:{1} {0}Desk: {2} {0}Source: {3} {0}File Path: {4}{0}{0}",
                                            System.Environment.NewLine,
                                            ImportSpecFTP.ImportSpecification.SubscriberID,
                                            ImportSpecFTP.ImportSpecification.Desk,
                                            ImportSpecFTP.FTPServer,
                                            ImportSpecFTP.ImportSpecification.RemoteFilePath
                                          );
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strStatus;
        }
        internal string GenerateErrorMessage(FTPImportSpec ImportSpecFTP, Exception Ex)
        {
            string strStatus = "";
            try
            {
                strStatus = string.Format("Exception of type {1} Occurred While Importing ID:{2} {0}Desk: {3} {0}Source: {4} {0}File Path: {5} {0}{0}Error Message:{6}{0}{0}Stack Trace:{7}{0}{0}",
                                            System.Environment.NewLine,
                                            Ex.GetType(),
                                            ImportSpecFTP.ImportSpecification.SubscriberID,
                                            ImportSpecFTP.ImportSpecification.Desk,
                                            ImportSpecFTP.FTPServer,
                                            ImportSpecFTP.ImportSpecification.RemoteFilePath,
                                            Ex.Message,
                                            Ex.StackTrace
                                          );
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strStatus;
        }

        internal string GenerateErrorMessage(EmailImportSpec ImportSpecEmail, Exception Ex)
        {
            string strStatus = "";
            try
            {
                switch (ImportSpecEmail.ImportSpecification.SubscriptionType)
                {
                    case "EmailAttachment":
                        strStatus = string.Format("Exception of type {1} Occurred While Importing SubscriberID: {2}; {0}{0}During ExecutionID:{3}; {0}{0}Desk:{4}; {0}{0}Message Subject: {5}; {0}{0}Sender: {6}; {0}Attachment: {7}{0}{0}Error Message: {8}{0}{0}Stack Trace:{9}{0}{0}",
                                                    System.Environment.NewLine,
                                                    Ex.GetType(),
                                                    ImportSpecEmail.ImportSpecification.ExecutionID,
                                                    ImportSpecEmail.ImportSpecification.SubscriberID,
                                                    ImportSpecEmail.ImportSpecification.Desk,
                                                    ImportSpecEmail.MailSubject,
                                                    ImportSpecEmail.MailAddress,
                                                    ImportSpecEmail.ImportSpecification.RemoteFilePath,
                                                    Ex.Message,
                                                    Ex.StackTrace
                                                  );
                        break;

                    case "EmailBody":
                        strStatus = string.Format("Exception of type {1} Occurred While Importing SubscriberID: {2}; {0}{0}During ExecutionID:{3}; {0}{0}Desk:{4}; {0}{0}Message Subject: {5}; {0}{0}Sender: {6};{0}{0}Error Message:{7}{0}{0}Stack Trace:{8}{0}{0}",
                                                    System.Environment.NewLine,
                                                    Ex.GetType(),
                                                    ImportSpecEmail.ImportSpecification.ExecutionID,
                                                    ImportSpecEmail.ImportSpecification.SubscriberID,
                                                    ImportSpecEmail.ImportSpecification.Desk,
                                                    ImportSpecEmail.MailSubject,
                                                    ImportSpecEmail.MailAddress,
                                                    Ex.Message,
                                                    Ex.StackTrace
                                                  );
                        break;

                    default:
                        strStatus = string.Format("Exception of type {1} Occurred While Importing SubscriberID: {2}; {0}{0}During ExecutionID:{3}; {0}{0}Desk:{4}; {0}{0}Message Subject: {5}; {0}{0}Sender: {6};{0}{0}Error Message:{7}{0}{0}Stack Trace:{8}{0}{0}",
                                                    System.Environment.NewLine,
                                                    Ex.GetType(),
                                                    ImportSpecEmail.ImportSpecification.ExecutionID,
                                                    ImportSpecEmail.ImportSpecification.SubscriberID,
                                                    ImportSpecEmail.ImportSpecification.Desk,
                                                    ImportSpecEmail.MailSubject,
                                                    ImportSpecEmail.MailAddress,
                                                    Ex.Message,
                                                    Ex.StackTrace
                                                  );
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strStatus;
        }
        internal string GenerateErrorMessage(EmailImportSpec ImportSpecEmail, string Message)
        {
            string strStatus = Message;
            try
            {
                switch (ImportSpecEmail.ImportSpecification.SubscriptionType)
                {
                    case "EmailAttachment":
                        strStatus += string.Format("Error Occurred While Importing SubscriberID: {1}; {0}{0}During ExecutionID:{2}; {0}{0}Desk:{3};  {0}{0}Message Subject: {4}; {0}{0}Sender: {5}; {0}Attachment: {6};{0}{0}",
                                                    System.Environment.NewLine,
                                                    ImportSpecEmail.ImportSpecification.SubscriberID,
                                                    ImportSpecEmail.ImportSpecification.ExecutionID,
                                                    ImportSpecEmail.ImportSpecification.Desk,
                                                    ImportSpecEmail.MailSubject,
                                                    ImportSpecEmail.MailAddress,
                                                    ImportSpecEmail.ImportSpecification.RemoteFilePath
                                                  );
                        break;

                    case "EmailBody":
                        strStatus += string.Format("Error Occurred While Importing SubscriberID: {1}; {0}{0}During ExecutionID:{2}; {0}{0}Desk:{3};{0}{0}Message Subject: {4}; {0}{0}Sender: {5};{0}{0}",
                                                    System.Environment.NewLine,
                                                    ImportSpecEmail.ImportSpecification.SubscriberID,
                                                    ImportSpecEmail.ImportSpecification.ExecutionID,
                                                    ImportSpecEmail.ImportSpecification.Desk,
                                                    ImportSpecEmail.MailSubject,
                                                    ImportSpecEmail.MailAddress
                                                  );
                        break;

                    default:
                        strStatus += string.Format("Error Occurred While Importing SubscriberID: {1}; {0}{0}During ExecutionID:{2}; {0}{0}Desk:{3}; {0}{0}Message Subject: {4}; {0}{0}Sender: {5};{0}{0}",
                                                    System.Environment.NewLine,
                                                    ImportSpecEmail.ImportSpecification.SubscriberID,
                                                    ImportSpecEmail.ImportSpecification.ExecutionID,
                                                    ImportSpecEmail.ImportSpecification.Desk,
                                                    ImportSpecEmail.MailSubject,
                                                    ImportSpecEmail.MailAddress
                                                  );
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strStatus;
        }


        internal string GenerateErrorMessage(ModelErrorEventArgs ModelErrorArguments)
        {
            string strStatus = "";
            try
            {
                strStatus = string.Format("Exception of type {1} {0}Occurred While Performing Operation:{2}; {0}{0}Error Message: {3}{0}{0}",
                                             System.Environment.NewLine,
                                             ModelErrorArguments.Exception.GetType(),
                                             ModelErrorArguments.OperationDescription,
                                             ModelErrorArguments.Exception.Message
                                          );
                if ("System.Data.SqlClient.SqlException" == ModelErrorArguments.Exception.GetType().ToString())
                {
                    strStatus += System.Environment.NewLine;
                    SqlException sqlEx = (SqlException)ModelErrorArguments.Exception;
                    foreach (SqlError sqlError in sqlEx.Errors)
                    {
                        strStatus += ("\t" + sqlError.Message + System.Environment.NewLine);
                    }
                }

                strStatus += string.Format("{0}{0}Stack Trace:{0}{1}",
                                             System.Environment.NewLine,
                                             ModelErrorArguments.Exception.StackTrace
                                          );
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strStatus;
        }


        internal string GenerateDateMismatchMessage(ImportSpec ImportSpecification)
        {
            string strMsgErr = "";
            try
            {
                DateTime dtExpected = InventoryImportController.GetBizDate();
                if (ImportSpecification.IsBizDatePrior)
                {
                    dtExpected =  InventoryImportController.GetPriorBizDate();
                }

                strMsgErr = string.Format("{0}Error: Date Mismatch!!! {0}Cannot Import {1} Data From Subscriber ID:{2}, Desk:{3}{0}{0}",
                                                    System.Environment.NewLine,
                                                    ImportSpecification.SubscriptionType,
                                                    ImportSpecification.SubscriberID,
                                                    ImportSpecification.Desk
                                                );
                strMsgErr += GenerateDateExpectedMessage(ImportSpecification);

                strMsgErr += string.Format("{0}{0}{0}The Setting at App.Config.RefuseImportOldDates = {1}{0} If you wish to Import this data anyway, set the App.Config.RefuseImportOldDates to \"False\"{0}{0}",
                                                    System.Environment.NewLine,
                                                     InventoryImportController.RefuseImportOldDates
                                                );

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateDateMismatchMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strMsgErr;
        }

        internal string GenerateDateExpectedMessage(ImportSpec ImportSpecification)
        {
            string strMsgErr = "";
            try
            {
                DateTime dtExpected = InventoryImportController.GetBizDate();
                if (ImportSpecification.IsBizDatePrior)
                {
                    dtExpected = InventoryImportController.GetPriorBizDate();
                }

                strMsgErr = string.Format("Error: Date Specified: {1}, Does Not Match Date Expected: {2}.   ",
                                                    System.Environment.NewLine,
                                                    ImportSpecification.BizDateSpecified.Date.ToLongDateString(),
                                                    dtExpected.ToLongDateString()
                                                );

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateDateExpectedMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strMsgErr;
        }




        internal string GenerateRecordCountMismatchErrorMessage(ImportSpec ImportSpecification)
        {
            string strMsgErr = "";

            try
            {
                int nDiffNumRecords = 0;
                if (ImportSpecification.NumRecordsExpected > ImportSpecification.NumRecordsImported)
                {
                    nDiffNumRecords = ImportSpecification.NumRecordsExpected - ImportSpecification.NumRecordsImported;
                    strMsgErr = string.Format("{0}Warning: Record-Count Mismatch!!! {0} Unable to Import {1} Records. {0}{0}Expecting {2} Records, Imported: {3} Records{0} Missing: {1} Record(s).{0}{0}",
                                                    System.Environment.NewLine,
                                                    nDiffNumRecords,
                                                    ImportSpecification.NumRecordsExpected,
                                                    ImportSpecification.NumRecordsImported
                                                );

                }
                else
                {
                    nDiffNumRecords = ImportSpecification.NumRecordsImported - ImportSpecification.NumRecordsExpected;
                    strMsgErr = string.Format("{0}Warning: Record-Count Mismatch!!! {0} Found {1} Extra Records. {0}{0}Expecting {2} Records, Imported:{3} Records{0} {1} Extra Record(s).{0}{0}",
                                                    System.Environment.NewLine,
                                                    nDiffNumRecords,
                                                    ImportSpecification.NumRecordsExpected,
                                                    ImportSpecification.NumRecordsImported
                                                );
                }

                strMsgErr += string.Format("{0}Please Verify the Regex Statements in the InventoryFilePatterns Table (tbInventoryFilePatterns.DataRegEx) to Ensure that they Match the Data Accurately.{0}{0}",
                                                System.Environment.NewLine
                                            );
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateRecordCountMismatchErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strMsgErr;
        }

        internal string GenerateRecordCountMismatchStatusMessage(ImportSpec ImportSpecification)
        {
            string strMsgErr = "";

            try
            {
                int nDiffNumRecords = 0;
                if (ImportSpecification.NumRecordsExpected > ImportSpecification.NumRecordsImported)
                {
                    nDiffNumRecords = ImportSpecification.NumRecordsExpected - ImportSpecification.NumRecordsImported;
                    strMsgErr = string.Format("Warning: Record-Count Mismatch! Unable to Import {0} Records. Expected: {1}, Imported: {2}; ",
                                                    nDiffNumRecords,
                                                    ImportSpecification.NumRecordsExpected,
                                                    ImportSpecification.NumRecordsImported
                                                );

                }
                else
                {
                    nDiffNumRecords = ImportSpecification.NumRecordsImported - ImportSpecification.NumRecordsExpected;
                    strMsgErr = string.Format("Warning: Record-Count Mismatch! Found {1} Extra Records. Expected: {1}, Imported: {2}; ",
                                                    nDiffNumRecords,
                                                    ImportSpecification.NumRecordsExpected,
                                                    ImportSpecification.NumRecordsImported
                                                );
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateRecordCountMismatchStatusMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strMsgErr;
        }


        internal string GenerateSQLRowErrorMessage(ImportSpec ImportSpecification)
        {
            string strMsgErr = "";

            try
            {
                for (int iRow = 0; iRow < ImportSpecification.InventoryEntryObjects.Count; iRow++)
                {
                    InventoryDataEntry ActiveEntry = (InventoryDataEntry)ImportSpecification.InventoryEntryObjects[iRow];
                    if (ActiveEntry.HasErrors)
                    {
                        strMsgErr += string.Format("SecID:{1}, Desk:{2}, BizDate:{3},  {0}{4}{0}{0}",
                                                        Environment.NewLine,
                                                        ActiveEntry.SecId,
                                                        ActiveEntry.Desk,
                                                        ActiveEntry.BizDate,
                                                        ActiveEntry.ErrorMsg
                                                    );
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryImportMessageEngine.GenerateSQLRowErrorMessage]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
            return strMsgErr;
        }


        #endregion


    }
}
