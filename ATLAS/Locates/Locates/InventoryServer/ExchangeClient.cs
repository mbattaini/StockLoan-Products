
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

        string strURLDomain = "penson.com";
        string strExchangeURL = @"https://webmail.penson.com/EWS/Exchange.asmx";
        string strUserName = "bpritchard";
        string strPassword = "Loanstar2008";
        string strNTDomain = "PENDAL_NT";
        string strEmailAddress = "";

        public string ExchangeURL
        {
            get { return strExchangeURL; }
            set { strExchangeURL = value; }
        }
        public string ExchangeUser
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        public string ExchangePassword
        {
            get { return strPassword; }
            set { strPassword = value; }
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
                strEmailAddress = strUserName + "@" + strURLDomain;


                bindingExchangeService = new ExchangeServiceBinding();
                bindingExchangeService.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                //bindingExchangeService.Credentials = new NetworkCredential(strNTDomain + @"\" + strUserName, strPassword, strURLDomain);
                bindingExchangeService.Url = strExchangeURL;
                bindingExchangeService.AllowAutoRedirect = true;

                // Set up the binding for Exchange impersonation.
                //bindingExchangeService.ExchangeImpersonation = new ExchangeImpersonationType();
                //bindingExchangeService.ExchangeImpersonation.ConnectingSID = new ConnectingSIDType();
                //bindingExchangeService.ExchangeImpersonation.ConnectingSID.PrimarySmtpAddress = strUserName + "@" + strURLDomain; //"USER2@exampledomain.com";


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

            
            try
            {
                //MessageImportSpec.ImportSpecification.FileCheckTime = DateTime.Now.ToUniversalTime();

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
                FieldOrderType[] fieldsSortOrder = new FieldOrderType[2];

                FieldOrderType fieldPrimarySort = new FieldOrderType();
                PathToUnindexedFieldType pathDateSent = new PathToUnindexedFieldType();
                pathDateSent.FieldURI = UnindexedFieldURIType.itemDateTimeSent;
                fieldPrimarySort.Item = pathDateSent;
                fieldPrimarySort.Order = SortDirectionType.Descending;
                fieldsSortOrder[0] = fieldPrimarySort;

                FieldOrderType fieldSecondarySort = new FieldOrderType();
                PathToUnindexedFieldType pathHasAttachments = new PathToUnindexedFieldType();
                pathHasAttachments.FieldURI = UnindexedFieldURIType.itemHasAttachments;
                fieldSecondarySort.Item = pathHasAttachments;
                fieldSecondarySort.Order = SortDirectionType.Descending;
                fieldsSortOrder[1] = fieldSecondarySort;

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
                AndType andRestriction = new AndType();
                List<SearchExpressionType> lstSearchExpressions = new List<SearchExpressionType>(2);

                //-------------------------------------------------------------
                //Restrict Items by Sender (From)
                //-------------------------------------------------------------
                PathToUnindexedFieldType ftMessageSender = new PathToUnindexedFieldType();
                ftMessageSender.FieldURI = UnindexedFieldURIType.messageFrom;
                ConstantValueType cvtSender = new ConstantValueType();
                cvtSender.Value = MessageImportSpec.MailAddress;
                FieldURIOrConstantType ctMessageSender = new FieldURIOrConstantType();
                ctMessageSender.Item = cvtSender;

                ContainsExpressionType cetSender = new ContainsExpressionType();
                cetSender.Constant = cvtSender;
                cetSender.Item = ftMessageSender;
                cetSender.ContainmentComparison = ContainmentComparisonType.IgnoreCase;
                cetSender.ContainmentComparisonSpecified = true;
                cetSender.ContainmentMode = ContainmentModeType.FullString;
                cetSender.ContainmentModeSpecified = true;

                lstSearchExpressions.Add(cetSender);


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

                cetSubject.ContainmentComparison = ContainmentComparisonType.IgnoreCaseAndNonSpacingCharacters;
                cetSubject.ContainmentComparisonSpecified = true;
                cetSubject.ContainmentMode = ContainmentModeType.Substring;
                cetSubject.ContainmentModeSpecified = true;

                lstSearchExpressions.Add(cetSubject);


                //-------------------------------------------------------------
                // Build Restriction
                //-------------------------------------------------------------
                RestrictionType rtSenderAndSubject = new RestrictionType();
                andRestriction.Items = lstSearchExpressions.ToArray();
                rtSenderAndSubject.Item = andRestriction;
                findItemRequest.Restriction = rtSenderAndSubject;


                //-------------------------------------------------------------
                //-------------------------------------------------------------
                // Send the request and get the response.
                //-------------------------------------------------------------
                FindItemResponseType findItemResponse = bindingExchangeService.FindItem(findItemRequest);

                // Get the response messages.
                ResponseMessageType[] rmta = findItemResponse.ResponseMessages.Items;

                foreach (ResponseMessageType rmt in rmta)
                {
                    // Cast to the correct response message type.
                    FindItemResponseMessageType responseFindItem = (FindItemResponseMessageType)rmt;
                    if (responseFindItem.ResponseClass == ResponseClassType.Success)
                    {
                        int nNumItems = responseFindItem.RootFolder.TotalItemsInView;
                        Console.WriteLine("Item found:" + nNumItems.ToString());

                        if (0 < nNumItems)
                        {
                            ArrayOfRealItemsType arrayRealMailboxItems = (ArrayOfRealItemsType)responseFindItem.RootFolder.Item;
                            foreach (MessageType message in arrayRealMailboxItems.Items)
                            {
                                Console.WriteLine(message.DateTimeReceived.ToString() + "; " + message.From.Item.Name + "; " + message.Subject
                                    + "; Has Attachment: " + message.HasAttachments.ToString());

                                DownloadMessageContents(message.ItemId.Id, MessageImportSpec.ImportSpecification);
                            }
                        }
                    }
                }
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
                string folder = ImportSpecification.LocalDir;

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

                                    switch(Attachment.Attachments[0].GetType().Name)
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
                                                    // This Event Causes the Controller to Generate the Import Execution (ExecutionID)
                                                    RaiseStartImportEvent( "StartingImport", ImportSpecification);
                                                    ImportSpecification.IsDownloadSuccessful = true;

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
                                                        ImportSpecification.FileModifiedTime = MsgItem.DateTimeSent;
                                                        ImportSpecification.ImportStatus = ImportSpecification.LocalFilePath + ", " + infoImportFile.Length + " bytes";
                                                    }
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




