
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Net;
using System.Text;

//using System.Xml;
//using System.Net.Security;
// using System.Security.Cryptography.X509Certificates;

using StockLoan.Common;
using StockLoan.Inventory.ExchangeWebServices;

namespace StockLoan.Inventory
{
    public class ExchangeClient
    {

        //---------------------------------------------------------------------
        #region Members
        //---------------------------------------------------------------------

        // Set up the binding with credentials and URL.
        ExchangeServiceBinding bindingExchangeService;


        string strExchangeURL = @"https://webmail.penson.com/EWS/Exchange.asmx";


        public string ExchangeURL
        {
            get { return strExchangeURL; }
            set { strExchangeURL = value; }
        }


        #endregion


        #region Static Properties

        public static Mutex mtxImportExchange = new Mutex(false, "InventoryImportExchange");

        #endregion


        //---------------------------------------------------------------------
        #region DelegatesAndEvents
        //---------------------------------------------------------------------
        public delegate void StatusUpdateEventHandler(object sender, StatusChangedEventArgs e);

        public event StatusUpdateEventHandler StartImportEvent;
        public event StatusUpdateEventHandler StatusUpdateEvent;



        public void UpdateStatusMessage(string StatusMessage)
        {
            if (null != StatusUpdateEvent)
            {
                StatusChangedEventArgs NewStatus = new StatusChangedEventArgs(StatusMessage);
                StatusUpdateEvent(this, NewStatus);
            }
        }
        public void RaiseStartImportEvent(string StatusMessage, ImportSpec ImportSpecification)
        {
            if (null != StatusUpdateEvent)
            {
                StatusChangedEventArgs NewStatus = new StatusChangedEventArgs(StatusMessage, ImportSpecification);
                StartImportEvent(this, NewStatus);
            }
        }

        #endregion


        public ExchangeClient()
        {
            try
            {
                bindingExchangeService = new ExchangeServiceBinding();
                string strUseDefaultLogin = StockLoan.Common.Standard.ConfigValue("InventoryImportExchangeLoginAsServiceUser");
                string strURLDomain = StockLoan.Common.Standard.ConfigValue("InventoryImportEmailDomain");

                bool bUseDefaultLogin = bool.Parse(strUseDefaultLogin);
                if (bUseDefaultLogin)
                {
                    bindingExchangeService.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                else
                {
                    string strExchangeUser = StockLoan.Common.Standard.ConfigValue("InventoryImportExchangeUsername");
                    string strExchangePassword = StockLoan.Common.Standard.ConfigValue("InventoryImportExchangePassword");
                    string strNTDomain = StockLoan.Common.Standard.ConfigValue("InventoryImportExchangeNTDomain");

                    //Set up the binding for Exchange impersonation.
                    string strDomainUser = strNTDomain + @"\" + strExchangeUser;
                    bindingExchangeService.Credentials = new NetworkCredential(strDomainUser, strExchangePassword, strNTDomain);
                    bindingExchangeService.ExchangeImpersonation = new ExchangeImpersonationType();
                    bindingExchangeService.ExchangeImpersonation.ConnectingSID = new ConnectingSIDType();
                    bindingExchangeService.ExchangeImpersonation.ConnectingSID.PrimarySmtpAddress = strExchangeUser + "@" + strURLDomain; //"USER2@exampledomain.com";
                }

                strExchangeURL = StockLoan.Common.Standard.ConfigValue("InventoryImportExchangeServiceURL");
                bindingExchangeService.Url = strExchangeURL;
                bindingExchangeService.AllowAutoRedirect = true;

                //System.Net.ServicePointManager.ServerCertificateValidationCallback =
                //   delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                //   {
                //       // Replace this line with code to validate server certificate.
                //       return true;
                //   };
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.Constructor]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);
            }

        }


        public void FindExchangeMessage(EmailImportSpec MessageImportSpec)
        {
            Log.Write("ExchangeClient.FindExchangeMessage Is Starting. ", Log.Information, 3);

            try
            {
                //MessageImportSpec.ImportSpecification.FileCheckTime = DateTime.Now.ToUniversalTime();


                string strMsgStatus = "ExchangeClient.FindExchangeMessage Is Starting. ";
                UpdateStatusMessage(strMsgStatus);

                //-------------------------------------------------------------
                //-------------------------------------------------------------
                // Form the FindItem request.
                //-------------------------------------------------------------
                FindItemType findItemRequest = new FindItemType();
                findItemRequest.Traversal = ItemQueryTraversalType.Shallow;

                //-------------------------------------------------------------
                // Define which item properties are returned in the response.
                ItemResponseShapeType itemProperties = new ItemResponseShapeType();
                itemProperties.BaseShape = DefaultShapeNamesType.AllProperties;

                PathToUnindexedFieldType[] ptuftItemClass = new PathToUnindexedFieldType[1];
                ptuftItemClass[0] = new PathToUnindexedFieldType();
                ptuftItemClass[0].FieldURI = UnindexedFieldURIType.itemItemClass;
                itemProperties.AdditionalProperties = ptuftItemClass;

                //-------------------------------------------------------------
                // Add properties shape to the request.
                findItemRequest.ItemShape = itemProperties;

                //-------------------------------------------------------------
                // Identify which folders to search to find items.
                DistinguishedFolderIdType[] folderIDArray = new DistinguishedFolderIdType[2];
                folderIDArray[0] = new DistinguishedFolderIdType();
                folderIDArray[0].Id = DistinguishedFolderIdNameType.inbox;

                //folderIDArray[1] = new DistinguishedFolderIdType();
                //folderIDArray[1].Id = DistinguishedFolderIdNameType.drafts;
                //-------------------------------------------------------------
                // Add folders to the request.
                findItemRequest.ParentFolderIds = folderIDArray;


                //-------------------------------------------------------------
                //-------------------------------------------------------------
                // Select Sort Order, build FieldOrderType
                //-------------------------------------------------------------
                FieldOrderType[] fieldsSortOrder = new FieldOrderType[3];

                FieldOrderType fieldPrimarySort = new FieldOrderType();
                PathToUnindexedFieldType pathDateCreated = new PathToUnindexedFieldType();
                pathDateCreated.FieldURI = UnindexedFieldURIType.itemDateTimeSent;
                fieldPrimarySort.Item = pathDateCreated;
                fieldPrimarySort.Order = SortDirectionType.Descending;
                fieldsSortOrder[0] = fieldPrimarySort;

                FieldOrderType fieldSecondarySort = new FieldOrderType();
                PathToUnindexedFieldType pathDateSent = new PathToUnindexedFieldType();
                pathDateSent.FieldURI = UnindexedFieldURIType.itemDateTimeReceived;
                fieldSecondarySort.Item = pathDateSent;
                fieldSecondarySort.Order = SortDirectionType.Descending;
                fieldsSortOrder[1] = fieldSecondarySort;

                FieldOrderType fieldTritarySort = new FieldOrderType();
                PathToUnindexedFieldType pathDateRecieved = new PathToUnindexedFieldType();
                pathDateRecieved.FieldURI = UnindexedFieldURIType.itemHasAttachments;
                fieldTritarySort.Item = pathDateRecieved;
                fieldTritarySort.Order = SortDirectionType.Descending;
                fieldsSortOrder[2] = fieldTritarySort;

                //FieldOrderType fieldQuadrinarySort = new FieldOrderType();
                //PathToUnindexedFieldType pathHasAttachments = new PathToUnindexedFieldType();
                //pathHasAttachments.FieldURI = UnindexedFieldURIType.itemHasAttachments;
                //fieldQuadrinarySort.Item = pathHasAttachments;
                //fieldQuadrinarySort.Order = SortDirectionType.Descending;
                //fieldsSortOrder[3] = fieldQuadrinarySort;

                findItemRequest.SortOrder = fieldsSortOrder;


                //-------------------------------------------------------------
                //-------------------------------------------------------------
                // Build Index, Only Return the Top 1 Record
                //-------------------------------------------------------------
                IndexedPageViewType indexedView = new IndexedPageViewType();
                indexedView.BasePoint = IndexBasePointType.Beginning;
                indexedView.MaxEntriesReturned = 1;
                indexedView.MaxEntriesReturnedSpecified = true;
                indexedView.Offset = 0;
                                                
                findItemRequest.Item = indexedView;


                //-------------------------------------------------------------
                //-------------------------------------------------------------
                // Build Multiple Restrictions on the Find for findItemRequest
                //-------------------------------------------------------------
                int nNumRestrictions = 0;


                //-------------------------------------------------------------
                //restrict the returned items to just Messages
                //-------------------------------------------------------------
                PathToUnindexedFieldType MsgClassField = new PathToUnindexedFieldType();
                MsgClassField.FieldURI = UnindexedFieldURIType.itemItemClass;

                ConstantValueType MsgClassToGet = new ConstantValueType();
                MsgClassToGet.Value = "IPM.NOTE";

                FieldURIOrConstantType MsgClassConstant = new FieldURIOrConstantType();
                MsgClassConstant.Item = MsgClassToGet;

                IsEqualToType iettClass = new IsEqualToType();
                iettClass.FieldURIOrConstant = MsgClassConstant;
                iettClass.Item = MsgClassField;

                nNumRestrictions++;



                //-------------------------------------------------------------
                //Restrict Items by Sender (From)
                //-------------------------------------------------------------
                ContainsExpressionType cetSender = new ContainsExpressionType();
                if (!string.IsNullOrEmpty(MessageImportSpec.MailAddress))
                {
                    PathToUnindexedFieldType ftMessageSender = new PathToUnindexedFieldType();
                    ftMessageSender.FieldURI = UnindexedFieldURIType.messageFrom;
                    ConstantValueType cvtSender = new ConstantValueType();
                    cvtSender.Value = MessageImportSpec.MailAddress;
                    FieldURIOrConstantType ctMessageSender = new FieldURIOrConstantType();
                    ctMessageSender.Item = cvtSender;

                    cetSender.Constant = cvtSender;
                    cetSender.Item = ftMessageSender;
                    cetSender.ContainmentComparison = ContainmentComparisonType.IgnoreCase;
                    cetSender.ContainmentComparisonSpecified = true;
                    cetSender.ContainmentMode = ContainmentModeType.Substring;
                    cetSender.ContainmentModeSpecified = true;

                    nNumRestrictions++;
                }


                //-------------------------------------------------------------
                //findItemRequest.Restriction by Subject 
                //-------------------------------------------------------------
                PathToUnindexedFieldType ftMessageSubject = new PathToUnindexedFieldType();
                ftMessageSubject.FieldURI = UnindexedFieldURIType.itemSubject;

                ConstantValueType cvtSubject = new ConstantValueType();
                cvtSubject.Value = MessageImportSpec.MailSubject;

                FieldURIOrConstantType ctMessageSubject = new FieldURIOrConstantType();
                ctMessageSubject.Item = cvtSubject;

                ContainsExpressionType cetSubject = new ContainsExpressionType();
                cetSubject.Constant = cvtSubject;
                cetSubject.Item = ftMessageSubject;
                cetSubject.ContainmentComparison = ContainmentComparisonType.IgnoreCase;
                cetSubject.ContainmentComparisonSpecified = true;
                cetSubject.ContainmentMode = ContainmentModeType.Substring;
                cetSubject.ContainmentModeSpecified = true;
                nNumRestrictions++;



                //-------------------------------------------------------------
                //findItemRequest.Restriction by Date 
                //-------------------------------------------------------------
                PathToUnindexedFieldType ftMessageDate = new PathToUnindexedFieldType();
                ftMessageDate.FieldURI = UnindexedFieldURIType.itemDateTimeSent;

                ConstantValueType cvtDate = new ConstantValueType();
                cvtDate.Value = MessageImportSpec.ImportSpecification.BizDateLastImported.ToString();

                FieldURIOrConstantType ctMessageDate = new FieldURIOrConstantType();
                ctMessageDate.Item = cvtDate;

                IsGreaterThanType gtDate = new IsGreaterThanType();
                gtDate.FieldURIOrConstant = ctMessageDate;
                gtDate.Item = ftMessageDate;
                nNumRestrictions++;





                //-------------------------------------------------------------
                // Build Restriction
                //-------------------------------------------------------------
                List<SearchExpressionType> lstSearchExpressions = new List<SearchExpressionType>(nNumRestrictions);

                lstSearchExpressions.Add(iettClass);
                if (!string.IsNullOrEmpty(MessageImportSpec.MailAddress))
                {
                    lstSearchExpressions.Add(cetSender);
                }
                lstSearchExpressions.Add(cetSubject);
                lstSearchExpressions.Add(gtDate);

                AndType andRestriction = new AndType();
                andRestriction.Items = lstSearchExpressions.ToArray();

                RestrictionType rtSenderAndSubject = new RestrictionType();
                rtSenderAndSubject.Item = andRestriction;
                findItemRequest.Restriction = rtSenderAndSubject;

                //rtSenderAndSubject.Item = iettClass;
                //findItemRequest.Restriction = rtSenderAndSubject;

                //rtSenderAndSubject.Item = cetSubject;
                //findItemRequest.Restriction = rtSenderAndSubject;


                //-------------------------------------------------------------
                //-------------------------------------------------------------
                // Send the request and get the response.
                //-------------------------------------------------------------
                Log.Write("Sending Exchange Request", Log.Information, 3);
                FindItemResponseType findItemResponse = bindingExchangeService.FindItem(findItemRequest);

                // Get the response messages.
                Log.Write("Reading Exchange Reply", Log.Information, 3);
                ResponseMessageType[] rmta = findItemResponse.ResponseMessages.Items;

                if (0 < rmta.Length)
                {
                    foreach (ResponseMessageType rmt in rmta)
                    {
                        // Cast to the correct response message type.
                        FindItemResponseMessageType responseFindItem = (FindItemResponseMessageType)rmt;
                        if (responseFindItem.ResponseClass == ResponseClassType.Success)
                        {
                            int nNumItems = responseFindItem.RootFolder.TotalItemsInView;
                            strMsgStatus = "Recieved Exchange Reply Containing: " + nNumItems.ToString() + " Items.";
                            Log.Write(strMsgStatus, Log.Information, 3);
                            Console.WriteLine(strMsgStatus);

                            if (0 < nNumItems)
                            {
                                ArrayOfRealItemsType arrayRealMailboxItems = (ArrayOfRealItemsType)responseFindItem.RootFolder.Item;
                                strMsgStatus = "Exchange Reply Contains: " + arrayRealMailboxItems.Items.Length + " Real Messages.";
                                Log.Write(strMsgStatus, Log.Information, 3);

                                foreach (MessageType message in arrayRealMailboxItems.Items)
                                {
                                    strMsgStatus = string.Format("Found Message Recieved On: {0}; From Sender: {1}; With Subject: {2}; Has Attachment: {3}",
                                                                            message.DateTimeReceived.ToString(),
                                                                            message.From.Item.Name,
                                                                            message.Subject,
                                                                            message.HasAttachments.ToString()
                                                                       );
                                    Log.Write(strMsgStatus, Log.Information, 3);
                                    UpdateStatusMessage(strMsgStatus);
                                    DownloadMessageContents(message.ItemId.Id, MessageImportSpec.ImportSpecification);
                                }
                            }
                            else
                            {
                                strMsgStatus = String.Format("The Exchange Server Returned 0 Items When Requested Mail From: {0}, with Subject Containing the Substring {1}"
                                                                    , MessageImportSpec.MailAddress
                                                                    , MessageImportSpec.MailSubject
                                                              );
                                Log.Write(strMsgStatus, Log.Information, 3);
                                UpdateStatusMessage(strMsgStatus);
                            }
                        }
                        else
                        {
                            strMsgStatus = string.Format("Reply from Exchange Server Indicates that it was Unable to Locate any Messages from: {0} with Subject: {1}"
                                                                    , MessageImportSpec.MailAddress
                                                                    , MessageImportSpec.MailSubject
                                                                );
                            Log.Write(strMsgStatus, Log.Information, 3);
                            UpdateStatusMessage(strMsgStatus);
                        }
                    }
                }
                else
                {
                    Log.Write("There were no Messages in the Exchange Reply", Log.Information, 3);
                }

                strMsgStatus = "ExchangeClient.FindExchangeMessage is Complete. ";
                Log.Write(strMsgStatus, Log.Information, 3);
                UpdateStatusMessage(strMsgStatus);

            }
            catch (WebException ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.FindExchangeMessage]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.FindExchangeMessage]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemID">ItemID of the MAIL Message</param>
        public void DownloadMessageContents(string itemID, ImportSpec ImportSpecification)
        {
            try
            {
                //first we need to get the attachment IDs for the item so we will need to make a GetItem call first 
                //specify the conetent that we want to retrieve 

                PathToUnindexedFieldType[] ptufta = new PathToUnindexedFieldType[5];

                ptufta[0] = new PathToUnindexedFieldType();
                ptufta[0].FieldURI = UnindexedFieldURIType.itemSubject;

                ptufta[1] = new PathToUnindexedFieldType();
                ptufta[1].FieldURI = UnindexedFieldURIType.itemBody;

                ptufta[2] = new PathToUnindexedFieldType();
                ptufta[2].FieldURI = UnindexedFieldURIType.itemAttachments;

                ptufta[3] = new PathToUnindexedFieldType();
                ptufta[3].FieldURI = UnindexedFieldURIType.itemHasAttachments;

                ptufta[4] = new PathToUnindexedFieldType();
                ptufta[4].FieldURI = UnindexedFieldURIType.itemItemId;


                ItemResponseShapeType irst = new ItemResponseShapeType();
                irst.BaseShape = DefaultShapeNamesType.AllProperties;
                irst.AdditionalProperties = ptufta;

                ItemIdType[] biita = new ItemIdType[1];
                biita[0] = new ItemIdType();
                biita[0].Id = itemID;

                //get the items 
                GetItemType git = new GetItemType();
                git.ItemShape = irst;
                git.ItemIds = biita;

                GetItemResponseType girt = bindingExchangeService.GetItem(git);
                if (girt.ResponseMessages.Items[0].ResponseClass != ResponseClassType.Success)
                {
                    return;
                }


                //now that we have the attachment IDs let's request the atthacments and save them to disk 
                ItemType MsgItem = ((ItemInfoResponseMessageType)girt.ResponseMessages.Items[0]).Items.Items[0];


                switch (ImportSpecification.SubscriptionType)
                {
                    case "EmailAttachment":
                        ScanAttachments(MsgItem, ImportSpecification);
                        break;
                    case "EmailBody":
                        ScanMessageBody(MsgItem, ImportSpecification);
                        break;
                }
            }
            catch (WebException ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.DownloadMessageContents]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.DownloadMessageContents]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);
            }
        }




        public void ScanAttachments(ItemType MsgItem, ImportSpec ImportSpecification)
        {
            if (true == MsgItem.HasAttachments)
            {

                try
                {
                    //create an array of attachment ids that we want to request 
                    AttachmentIdType[] aita = new AttachmentIdType[MsgItem.Attachments.Length];
                    for (int i = 0; i < MsgItem.Attachments.Length; i++)
                    {
                        aita[i] = new AttachmentIdType();
                        aita[i].Id = MsgItem.Attachments[i].AttachmentId.Id;
                    }

                    //create the attachment shape; we want the mime contnet just in case this is an message item so that we can save to disk 
                    AttachmentResponseShapeType arst = new AttachmentResponseShapeType();
                    arst.IncludeMimeContent = true;
                    arst.IncludeMimeContentSpecified = true;

                    //create a GetAttachment object for the GetAttachment operation 
                    GetAttachmentType gat = new GetAttachmentType();
                    gat.AttachmentIds = aita;
                    gat.AttachmentShape = arst;

                    // GET Attachment
                    GetAttachmentResponseType gart = bindingExchangeService.GetAttachment(gat);

                    if (!ImportSpecification.IsDownloadSuccessful)
                    {
                        //attachments can be of type FileAttachmentType or ItemAttachmentType 
                        //so we need to figure out which type we have before we manipulate it 
                        foreach (AttachmentInfoResponseMessageType Attachment in gart.ResponseMessages.Items)
                        {
                            switch (ImportSpecification.SubscriptionType)
                            {
                                case "EmailAttachment":

                                    switch (Attachment.Attachments[0].GetType().Name)
                                    {
                                        case "FileAttachmentType":

                                            // Find Attachment Name we are looking for
                                            FileInfo infoImportAttachment = new FileInfo(ImportSpecification.RemoteFilePath);

                                            // Found File Attachment
                                            if (Attachment.Attachments[0].Name.ToLower() == infoImportAttachment.Name.ToLower())
                                            {
                                                // Check to See if Content Is New
                                                if (ImportSpecification.FileModifiedTime < MsgItem.DateTimeSent)
                                                {
                                                    ImportSpecification.IsDownloadSuccessful = true;

                                                    // This Event Causes the Controller to Generate the Import Execution (ExecutionID)
                                                    Log.Write("Executing Import", Log.Information, 3);
                                                    RaiseStartImportEvent("StartingImport", ImportSpecification);

                                                    // save to disk 
                                                    FileAttachmentType TheFileAttachment = (FileAttachmentType)Attachment.Attachments[0];
                                                    using (Stream FileToDisk = new FileStream(ImportSpecification.LocalDir + Attachment.Attachments[0].Name, FileMode.Create))
                                                    {
                                                        FileToDisk.Write(TheFileAttachment.Content, 0, TheFileAttachment.Content.Length);
                                                        FileToDisk.Flush();
                                                        FileToDisk.Close();
                                                    }

                                                    // Combine Email Message Info with Attachment File Data
                                                    ArrayList alRowText = new ArrayList();
                                                    alRowText.Add(MsgItem.Subject);
                                                    alRowText.Add(string.Format("Attachment: {0}", TheFileAttachment.Name));

                                                    // Read File Into Object and Check the Results
                                                    if (InventoryImportController.ReadInputFile(ImportSpecification, alRowText))
                                                    {
                                                        FileInfo infoImportFile = new FileInfo(ImportSpecification.LocalFilePath);
                                                        ImportSpecification.ImportStatus = ImportSpecification.LocalFilePath + ", " + infoImportFile.Length + " bytes";
                                                        ImportSpecification.FileModifiedTime = MsgItem.DateTimeSent;
                                                    }
                                                    else
                                                    {
                                                        ImportSpecification.IsDownloadSuccessful = false;
                                                    }
                                                }
                                                else
                                                {
                                                    string strMsgStatus = string.Format("SubscriberID: {0}; Desk: {1}; Already Has Email With FileTime: {2}, And Will Not Import Data With FileTime: {3}"
                                                                                            , ImportSpecification.SubscriberID
                                                                                            , ImportSpecification.Desk
                                                                                            , ImportSpecification.FileModifiedTime
                                                                                            , MsgItem.DateTimeSent.ToUniversalTime()
                                                                                        );
                                                    Console.WriteLine(strMsgStatus);
                                                    UpdateStatusMessage(strMsgStatus);
                                                }
                                            } // EndIf
                                            break;


                                        // Did Not Find File, RECURSION HERE!!! :
                                        case "ItemAttachmentType":
                                            ItemType TheItemAttachment = ((ItemAttachmentType)Attachment.Attachments[0]).Item;
                                            ScanAttachments(TheItemAttachment, ImportSpecification);
                                            break;

                                    }  // End Inner Switch

                                    break;


                                case "EmailBody":
                                    if ("ItemAttachmentType" == Attachment.Attachments[0].GetType().Name)
                                    {
                                        // Scan Contents of Message Body 
                                        ItemType TheItemAttachment = ((ItemAttachmentType)Attachment.Attachments[0]).Item;
                                        ScanMessageBody(TheItemAttachment, ImportSpecification);
                                    }
                                    break;

                            } // End Outer Switch

                        }// End ForEach Attachment

                    } // End If Not IsDownloadSuccessful

                } // End Try

                catch (WebException ex)
                {
                    ImportSpecification.ImportStatus = ex.Message;
                    System.Console.Write(ex.Message);
                    Log.Write(ex.Message + " [ExchangeClient.ScanAttachments]", Log.Error, 1);
                    UpdateStatusMessage("Error!: " + ex.Message);
                }
                catch (Exception ex)
                {
                    ImportSpecification.ImportStatus = ex.Message;
                    System.Console.Write(ex.Message);
                    Log.Write(ex.Message + " [ExchangeClient.ScanAttachments]", Log.Error, 1);
                    UpdateStatusMessage("Error!: " + ex.Message);
                }
            }
        }







        /// <summary>
        /// 
        /// </summary>
        /// <param name="MsgItem"></param>
        /// <param name="ImportSpecification"></param>
        public void ScanMessageBody(ItemType MsgItem, ImportSpec ImportSpecification)
        {
            try
            {
                bool bRunRecursiveScan = false;

                if (!string.IsNullOrEmpty(MsgItem.Body.Value))
                {
                    // Combine Email Message Info with Attachment File Data
                    ArrayList alRowText = new ArrayList();
                    alRowText.Add(MsgItem.Subject);

                    // Add Message Body Text Rows
                    char[] arrayChrDelimiters = { '\n' };
                    string[] arrayMessageBodyRows = MsgItem.Body.Value.Split(arrayChrDelimiters, StringSplitOptions.RemoveEmptyEntries);
                    alRowText.AddRange(arrayMessageBodyRows);

                    // Search Text For Header Before Trying to Parse Data
                    if (InventoryImportController.ScanDataForHeaderMatch(ImportSpecification, alRowText))
                    {
                        // Check to See if Content Is New
                        if (ImportSpecification.FileModifiedTime < MsgItem.DateTimeSent)
                        {
                            Log.Write("Executing Import", Log.Information, 3);

                            // This Event Causes the Controller to Generate the Import Execution (ExecutionID)
                            RaiseStartImportEvent("StartingImport", ImportSpecification);

                            ImportSpecification.IsDownloadSuccessful = true;

                            // Read File Into Object and Check the Results
                            if (InventoryImportController.ReadInputData(ImportSpecification, alRowText))
                            {
                                ImportSpecification.FileModifiedTime = MsgItem.DateTimeSent;
                                ImportSpecification.ImportStatus = MsgItem.Subject + "; " + alRowText.Count + " rows; " + MsgItem.Size.ToString() + " bytes";
                            }
                        }
                        else
                        {
                            string strMsg = string.Format("This Message Is Not New. Will Not Execute Import. MessageTime:{0} is not newer than the current FileModifiedTime: {1}"
                                                            , MsgItem.DateTimeSent
                                                            , ImportSpecification.FileModifiedTime
                                                          );
                            Log.Write(strMsg, Log.Information, 3);
                        }
                    }
                    else
                    {
                        bRunRecursiveScan = true;
                    }
                }
                else
                {
                    bRunRecursiveScan = true;
                }


                if ((bRunRecursiveScan) && (!ImportSpecification.IsDownloadSuccessful) && (MsgItem.HasAttachments))
                {
                    // Did Not Find File, RECURSION HERE!!! :
                    ScanAttachments(MsgItem, ImportSpecification);
                }

            }
            catch (WebException ex)
            {
                ImportSpecification.ImportStatus = ex.Message;
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.ScanMessageBody]", Log.Error, 1);
                UpdateStatusMessage("Error! Message: " + ex.Message + " Response: " + ex.Response);
            }
            catch (Exception ex)
            {
                ImportSpecification.ImportStatus = ex.Message;
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.ScanMessageBody]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);
            }
        }

        public void CreateMessage(string Subject, string MessageBodyText)
        {
            try
            {
                string strSupportEmailAddress = StockLoan.Common.Standard.ConfigValue("InventoryImportSupportEmailAddress");
                string[] arrayRecipientAddresses = new string[1];
                arrayRecipientAddresses[0] = strSupportEmailAddress;

                CreateMessage(arrayRecipientAddresses, Subject, MessageBodyText);
            }
            catch (WebException ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.CreateMessage]", Log.Error, 1);
                UpdateStatusMessage("Error! Message: " + ex.Message + " Response: " + ex.Response);
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.CreateMessage]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);
            }
        }



        /// <summary>
        /// UseExchangeWebClientCreate
        /// </summary>
        public void CreateMessage(string[] RecipientAddresses, string MessageSubject, string MessageBodyText)
        {
            try
            {
                string strEmailDomain = StockLoan.Common.Standard.ConfigValue("InventoryImportEmailDomain");
                string strServiceUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
                string strServiceUserEmailAddress = strServiceUserName + "@" + strEmailDomain;

                string strCurrentTime = string.Format("At: {1} UTC {0}    {2} Local", System.Environment.NewLine, DateTime.Now.ToUniversalTime().ToString(), DateTime.Now.ToString());
                string strMessageBody = string.Format("{1}{0}{2}", System.Environment.NewLine, strCurrentTime, MessageBodyText);


                int nNumAddresses = RecipientAddresses.Length;
                EmailAddressType[] arrayToRecipients = new EmailAddressType[nNumAddresses];
                for (int iAddress = 0; iAddress < nNumAddresses; iAddress++)
                {
                    arrayToRecipients[iAddress] = new EmailAddressType();
                    arrayToRecipients[iAddress].EmailAddress = RecipientAddresses[iAddress];
                }

                CreateItemType createEmailRequest = new CreateItemType();
                createEmailRequest.MessageDisposition = MessageDispositionType.SaveOnly;
                createEmailRequest.MessageDispositionSpecified = true;
                createEmailRequest.SavedItemFolderId = new TargetFolderIdType();

                DistinguishedFolderIdType sentitems = new DistinguishedFolderIdType();
                sentitems.Id = DistinguishedFolderIdNameType.sentitems;
                createEmailRequest.SavedItemFolderId.Item = sentitems;
                createEmailRequest.Items = new NonEmptyArrayOfAllItemsType();

                MessageType message = new MessageType();
                message.Subject = MessageSubject;
                message.Body = new BodyType();
                message.Body.BodyType1 = BodyTypeType.Text;
                message.Body.Value = strMessageBody;
                message.Sender = new SingleRecipientType();
                message.Sender.Item = new EmailAddressType();
                message.Sender.Item.EmailAddress = strServiceUserEmailAddress;

                //message.ToRecipients = new EmailAddressType[1];
                //message.ToRecipients[0] = new EmailAddressType();
                //message.ToRecipients[0].EmailAddress = strSupportEmailAddress;
                message.ToRecipients = arrayToRecipients;

                message.Sensitivity = SensitivityChoicesType.Normal;

                createEmailRequest.Items.Items = new ItemType[1];
                createEmailRequest.Items.Items[0] = message;

                CreateItemResponseType createItemResponse = bindingExchangeService.CreateItem(createEmailRequest);
                ArrayOfResponseMessagesType responses = createItemResponse.ResponseMessages;
                ResponseMessageType[] responseMessages = responses.Items;


                foreach (ResponseMessageType respMsg in responseMessages)
                {
                    if (respMsg.ResponseClass == ResponseClassType.Error)
                    {
                        throw new Exception("Error: " + respMsg.MessageText);
                    }
                    else if (respMsg.ResponseClass == ResponseClassType.Warning)
                    {
                        throw new Exception("Warning: " + respMsg.MessageText);
                    }


                    if (respMsg is ItemInfoResponseMessageType)
                    {
                        ItemInfoResponseMessageType createItemResp = (respMsg as ItemInfoResponseMessageType);
                        ArrayOfRealItemsType aorit = createItemResp.Items;
                        foreach (ItemType item in aorit.Items)
                        {
                            if (item is MessageType)
                            {
                                MessageType myMessage = (item as MessageType);
                                Console.WriteLine("Created item: " + myMessage.ItemId.Id);

                                // Create the SendItem request.
                                SendItemType sit = new SendItemType();
                                sit.ItemIds = new BaseItemIdType[1];

                                // Create an item ID type and set the message ID and change key.
                                ItemIdType itemId = new ItemIdType();

                                // Get the ID and change key from the message that you obtained by using FindItem or CreateItem.
                                itemId.Id = myMessage.ItemId.Id;
                                itemId.ChangeKey = myMessage.ItemId.ChangeKey;

                                sit.ItemIds[0] = itemId;

                                // Send the message.
                                SendItemResponseType siResponse = bindingExchangeService.SendItem(sit);

                                // Check the result.
                                if (siResponse.ResponseMessages.Items.Length > 0 &&
                                    siResponse.ResponseMessages.Items[0].ResponseClass == ResponseClassType.Success)
                                {
                                    Console.WriteLine("Message with Id {0} and ChangeKey {1} is sent.", itemId.Id, itemId.ChangeKey);
                                }

                            }
                            // TODO: Add logic to check and cast for all other types.
                        }
                    }
                }

            }
            catch (WebException ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.CreateMessage]", Log.Error, 1);
                UpdateStatusMessage("Error! Message: " + ex.Message + " Response: " + ex.Response);
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExchangeClient.CreateMessage]", Log.Error, 1);
                UpdateStatusMessage("Error!: " + ex.Message);
            }
        }



    }
}




