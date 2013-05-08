using System;
using System.IO;
//using System.Xml;
using StockLoan.Common;
using System.Collections.Generic;
using System.Net.Mail;

namespace StockLoan.Inventory
{
    /// <summary>
    /// ImapClient class implementes IMAP client API
    /// </summary>
    internal class ImapClient : ImapBase
    {
        #region Variables
        /// <summary>
        /// Is login?
        /// </summary>
        Boolean _bLoggedIn = false;

        /// <summary>
        /// Is folder examined?
        /// </summary>
        private Boolean _bFolderExamined = false;

        /// <summary>
        /// used in RFC822 standard DateTime format
        /// Reference: http://tools.ietf.org/html/rfc822#section-5.1
        /// </summary>
        private String[] MonthString = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        /// <summary>
        ///Store attachments information. It should be key/value, such as PartId/Filename 
        /// </summary>
        private Dictionary<String, String> dicAttachments = new Dictionary<string, string>();

        /// <summary>
        /// the part number of message body.
        /// </summary>
        private String bodyPartNumber = String.Empty;

        #endregion

        public ImapClient(String host, String username, String password)
        {
            base._host = host;
            base._username = username;
            base._password = password;
        }

        #region Public Methods
        /// <summary>
        /// Get Mails that match the seraching criteria
        /// </summary>
        /// <param name="criteria">searching criteria</param>
        /// <param name="mailbox">mailbox</param>
        /// <returns>list of mails</returns>
        public List<MailMessage> GetMail(EmailSearchCriteria criteria, String mailbox)
        {
            List<MailMessage> mails = new List<MailMessage>();
            this.Login();
            this.ExamineFolder(mailbox);
            // TODO: Retrieves mails
            List<String> messageList = new List<string>();
            messageList = SearchMessage(criteria, true);
            foreach (String id in messageList)
            {
                MailMessage mail = FetchMessage(id);
                if (null != mail)
                    mails.Add(mail);
            }

            this.LogOut();
            return mails;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Login to server
        /// </summary>
        private void Login()
        {
            // Check whether this user already login to server
            if (base.Connected && _bLoggedIn)
            {
                System.Diagnostics.Debug.WriteLine("User already login");
                return;
            }

            if (false == base.Connected)
            {
                if( false == base.Connect())
                    return;
            }

            _bLoggedIn = false;

            String command = ImapCommand.Login + " \"" + base._username + "\" " + base._password  + IMAP_COMMAND_EOL;

            if (Send(command))
            {
                _bLoggedIn = true;
            }
        }

        /// <summary>
        /// Logout the user: It logout the user and disconnect the connetion from IMAP server.
        /// </summary>
        private void LogOut()
        {
            if (base.Connected && this._bLoggedIn)
            {

                String command = ImapCommand.Logout + IMAP_COMMAND_EOL;
                Send(command);
                Disconnect();
                _bLoggedIn = false;
            }
        }

        /// <summary>
        /// Examine the folder after login
        /// </summary>
        /// <param name="folder">Mailbox folder</param>
        private void ExamineFolder(String folder)
        {
            String command = ImapCommand.Examine + " " + folder + IMAP_COMMAND_EOL;
            if (_bLoggedIn && false == this._bFolderExamined)
            {
                this._bFolderExamined = Send(command);
            }
        }

        /// <summary>
        /// Search the messages by specified criterias
        /// </summary>
        /// <param name="criteria">Search criterias</param>
        /// <param name="matchFlag">Is it exact search</param>
        /// <returns>the list of message ids</returns>
        private  List<String> SearchMessage(EmailSearchCriteria criteria, Boolean matchFlag)
        {
            List<String> lstResult = new List<string>();
            if ( false == _bLoggedIn || false == _bFolderExamined)
            {
                return lstResult;
            }

            String searchingCriteria = "";
            if (criteria.From != String.Empty)
            {
                searchingCriteria = " " + ImapSearch.FROM.ToUpper() + " \"" + criteria.From + "\"";
            }

            if (criteria.Subject != String.Empty)
            {
                searchingCriteria += " " + ImapSearch.SUBJECT.ToUpper() + " \"" + criteria.Subject + "\"";
            }

            if (criteria.EmailDateOption != EmailSearchCriteria.EmailDateSearchOption.NOSEARCH)
            {
                // use RFC822 standard DateTime format
                // Reference: http://tools.ietf.org/html/rfc822#section-5.1
                String date = criteria.Date.Day.ToString() + 
                                "-" + 
                                this.MonthString[criteria.Date.Month - 1] + 
                                "-" +
                                criteria.Date.Year.ToString();

                if (criteria.EmailDateOption == EmailSearchCriteria.EmailDateSearchOption.SENTBEFORE)
                {
                    searchingCriteria += " " + ImapSearch.SENTBEFORE + " " + date;
                }
                else if (criteria.EmailDateOption == EmailSearchCriteria.EmailDateSearchOption.SENTSINCE)
                {
                    searchingCriteria += " " + ImapSearch.SENTSINCE + " " + date;
                }
            }

            String command = ImapCommand.Search + searchingCriteria + IMAP_COMMAND_EOL;

            List<String> lstResponse = new List<String>();
            try
            {
                if (SendAndReceive(command, ref lstResponse))
                {
                    // * SEARCH 105 112 126 136 143 144
                    // IMAP001 OK SEARCH completed.* 
                    // Note: the IMAP search is not exact search. It will return Emails which like the searching criteria
                    String prefix = IMAP_UNTAGGED_RESPONSE_PREFIX + " " + IMAP_SEARCH_RESPONSE;
                    foreach (String line in lstResponse)
                    {
                        System.Diagnostics.Debug.WriteLine(line);
                        if (StringStartsWith(line, IMAP_SERVER_RESPONSE_BAD))
                            continue;

                        int nPos = line.IndexOf(prefix);
                        if (nPos != -1)
                        {
                            nPos += prefix.Length;
                            String suffix = line.Substring(nPos).Trim();
                            if (String.Empty != suffix.Trim())
                            {
                                String[] temp = suffix.Split(' ');
                                lstResult.AddRange(temp);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            if (matchFlag)
            {
                FilterSearchResult(criteria, ref lstResult);
            }

            return lstResult;
        }

        /// <summary>
        /// Since IMAP will return all message which like the search criteria, we will only keep
        /// the message exactly match with search criteria.
        /// </summary>
        /// <param name="criteria">Searching criteria</param>
        /// <param name="lstResult">the message ids need be verified</param>
        private void FilterSearchResult(EmailSearchCriteria criteria, ref List<String> lstResult)
        {
            try
            {
                List<String> lstTemp = new List<string>();
                lstTemp.AddRange(lstResult);
                foreach (String messageId in lstTemp)
                {
                    List<String> lstResponse = new List<string>();

                    // Get message header
                    lstResponse = GetHeader(messageId, "0");
                    // lstResponse should hold pair key/value
                    if( lstResponse.Count % 2 > 0 )
                    {
                        Log.Write("Something is wrong getting header for message" + messageId, 1);
                        return;
                    }

                    for (int i = 0; i < lstResponse.Count / 2; i += 2)
                    {
                        if (0 == String.Compare(lstResponse[i], ImapBodyStructure.IMAP_HEADER_FROM_TAG, true))
                        {
                            if (0 != String.Compare(lstResponse[i + 1], criteria.From, true))
                            {
                                lstResult.Remove(messageId);
                            }                            
                        }
                        else if (0 == String.Compare(lstResponse[i], ImapBodyStructure.IMAP_HEADER_SUBJECT_TAG, true))
                        {
                            if (0 != String.Compare(lstResponse[i + 1], criteria.Subject, true))
                            {
                                lstResult.Remove(messageId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Fetch the full message
        /// </summary>
        /// <param name="messageId">Message id </param>
        /// <returns> the MailMessage object</returns> 
        private MailMessage FetchMessage(String messageId)
        {
            MailMessage mail = new MailMessage();
            if (false == _bLoggedIn || false == _bFolderExamined)
                return mail;

            dicAttachments.Clear();
            this.bodyPartNumber = String.Empty;
            try
            {
                ProcessMailHeader(GetHeader(messageId, "0"), ref mail);
                List<String> lstResult = new List<string>();
                lstResult = GetBodyStructure(messageId);

                mail.Body = GetMessageBody(messageId);

/*                // Get Attachments
                if (dicAttachments.Count > 0)
                {
                    GetAttachments(messageId);
                }*/

                List<Attachment> lstAttachments = GetAttachments(messageId);
                foreach (Attachment att in lstAttachments)
                {
                    mail.Attachments.Add(att);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return mail;
        }

        /// <summary>
        /// based on data in message header filling subject/To/From/CC into mail 
        /// </summary>
        /// <param name="lstHeader">message header</param>
        /// <param name="mail">mail message</param>
        private void ProcessMailHeader(List<String> lstHeader, ref MailMessage mail)
        {
            try
            {
                for (int i = 0; i < lstHeader.Count; i += 2)
                {
                    String address = String.Empty;
                    String displayName = String.Empty;
                    if (0 == String.Compare(lstHeader[i], "Subject", true))
                    {
                        // Subject
                        mail.Subject = lstHeader[i + 1];
                    }
                    else if (0 == String.Compare(lstHeader[i], "From", true))
                    {
                        // From
                        address = lstHeader[i + 1].Substring(lstHeader[i + 1].IndexOf('<') + 1, lstHeader[i + 1].IndexOf('>') - lstHeader[i + 1].IndexOf('<') - 1);
                        displayName = lstHeader[i + 1].Substring(lstHeader[i + 1].IndexOf('"') + 1, lstHeader[i + 1].LastIndexOf('"') - lstHeader[i + 1].IndexOf('"') - 1);
                        mail.From = new MailAddress(address, displayName);

                    }
                    else if (0 == String.Compare(lstHeader[i], "To", true))
                    {
                        // To
                        if (lstHeader[i + 1].Contains(","))
                        {
                            String[] arrString = lstHeader[i + 1].Split(',');
                            foreach (string str in arrString)
                            {
                                address = str.Substring(str.IndexOf('<') + 1, str.IndexOf('>') - str.IndexOf('<') - 1);
                                displayName = str.Substring(str.IndexOf('"') + 1, str.LastIndexOf('"') - str.IndexOf('"') - 1);
                                mail.To.Add(new MailAddress(address, displayName));
                            }
                        }
                        else
                        {
                            address = lstHeader[i + 1].Substring(lstHeader[i + 1].IndexOf('<') + 1, lstHeader[i + 1].IndexOf('>') - lstHeader[i + 1].IndexOf('<') - 1);
                            displayName = lstHeader[i + 1].Substring(lstHeader[i + 1].IndexOf('"') + 1, lstHeader[i + 1].LastIndexOf('"') - lstHeader[i + 1].IndexOf('"') - 1);
                            mail.To.Add(new MailAddress(address, displayName));
                        }
                    }
                    else if (0 == String.Compare(lstHeader[i], "Cc", true))
                    {
                        // Cc
                        if (lstHeader[i + 1].Contains(","))
                        {
                            String[] arrString = lstHeader[i + 1].Split(',');
                            foreach (string str in arrString)
                            {
                                address = str.Substring(str.IndexOf('<') + 1, str.IndexOf('>') - str.IndexOf('<') - 1);
                                displayName = str.Substring(str.IndexOf('"') + 1, str.LastIndexOf('"') - str.IndexOf('"') - 1);
                                mail.CC.Add(new MailAddress(address, displayName));
                            }
                        }
                        else
                        {
                            address = lstHeader[i + 1].Substring(lstHeader[i + 1].IndexOf('<') + 1, lstHeader[i + 1].IndexOf('>') - lstHeader[i + 1].IndexOf('<') - 1);
                            displayName = lstHeader[i + 1].Substring(lstHeader[i + 1].IndexOf('"') + 1, lstHeader[i + 1].LastIndexOf('"') - lstHeader[i + 1].IndexOf('"') - 1);
                            mail.CC.Add(new MailAddress(address, displayName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Get the Body structure of the message.
        /// </summary>
        /// <param name="messageId"> Message ID</param>
        /// <returns>the content of body structure with pair tag/value</returns>
        private List<String> GetBodyStructure(String messageId)
        {
            List<String> lstResult = new List<string>();
            dicAttachments.Clear();
            
            String command = ImapCommand.Fetch + " " +
                            messageId.ToString() + " " + 
                            IMAP_BODYSTRUCTURE_COMMAND + IMAP_COMMAND_EOL;
            try
            {
                List<String> lstResponse = new List<string>();

                if (SendAndReceive(command, ref lstResponse))
                {
                    String bodyStruct = "";
                    Boolean bResult = false;

                    foreach (String line in lstResponse)
                    {
                        if (line.StartsWith(IMAP_UNTAGGED_RESPONSE_PREFIX) &&
                            line.Contains(IMAP_BODYSTRUCTURE_COMMAND))
                        {
                            bodyStruct = line;
                            // right now, we only support FETCH bodystructure for one message only.
                            // IMAP do support 
                            // A1 FETCH 1:2 BODYSTRUCTURE
                            bResult = true;
                            break;
                        }
                    }
                    if (false == bResult)
                    {
                        Log.Write("Can't find bodystructure for message id " + messageId, 1);
                        return lstResult;
                    }
                    else if (bodyStruct.Length == 0)
                    {
                        Log.Write("Bodystructure is empty for message id " + messageId, 1);
                        return lstResult;
                    }

                    Int32 nStart = -1;
                    nStart = bodyStruct.IndexOf(IMAP_BODYSTRUCTURE_COMMAND);
                    bodyStruct = bodyStruct.Substring(nStart + IMAP_BODYSTRUCTURE_COMMAND.Length);
                    int nEnd = bodyStruct.LastIndexOf(")");
                    if (nEnd == -1)
                    {
                        Log.Write("Can't find the end of bodystructure\n" + bodyStruct, 1);
                        return lstResult;
                    }

                    bodyStruct = bodyStruct.Substring(0, nEnd);

                    String partPrefix = "";
                    if (!ParseBodyStructure(messageId, ref bodyStruct, partPrefix, ref lstResult))
                    {
                        Log.Write("Can't parse bodystruct\n" + bodyStruct, 1);
                    }
                }
                else
                {
                    Log.Write("Can't retrieve data for id:" + messageId.ToString(), 1);
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                LogOut();
            }

            return lstResult;
        }

        /// <summary>
        /// Parse the bodystructure and store as XML Element
        /// </summary>
        /// <param name="bodyStruct">Bosy Structure</param>
        /// <param name="partPrefix">Part Prefix</param>
        /// <param name="messageId">message id</param>
        /// <param name="lstResult"> the list of result</param>
        /// <returns>true/false</returns>
        private Boolean ParseBodyStructure(String messageId, ref String bodyStruct, String partPrefix,ref List<String> lstResult)
        {
            Boolean bMultiPart = false;
            String temp = "";
            List<String> lstAttrs = new List<string>();
            bodyStruct = bodyStruct.Trim();

            //Check if this is NIL
            if (IsNilString(ref bodyStruct))
                return true;
            //Look for '('
            if (bodyStruct[0] == '(')
            {
                // Check if multipart or singlepart.
                // Multipart will have another '(' here
                // and single part will not.
                char ch;
                ch = bodyStruct[1];
                if (ch != '(')
                {
                    //Singal part
                    if (ch == ')')
                    {
                        bodyStruct = bodyStruct.Substring(2);
                        return true;
                    }

                    //remove opening paranthesis
                    bodyStruct = bodyStruct.Substring(1);
                    String type = "";
                    String subType = "";

                    // ContentType will be returned in temp
                    if (!GetContentType(ref bodyStruct, ref type, ref subType, ref temp))
                    {
                        return false;
                    }

                    System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_TYPE + "=" + temp);
                    lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_TYPE);
                    lstResult.Add(temp);

                    // Message-id (optional)
                    if (!ParseQuotedString(ref bodyStruct, ref temp))
                    {
                        Log.Write("Failed getting Message Id.", 1);
                        return false;
                    }

                    if (temp.Length > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_ID + "=" + temp);
                        lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_ID);
                        lstResult.Add(temp);
                    }

                    // Content-Description (optional)
                    if (!ParseQuotedString(ref bodyStruct, ref temp))
                    {
                        Log.Write("Failed getting the Content Desc.", 1);
                        return false;
                    }

                    if (temp.Length > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_DESC + "=" + temp);
                        lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_DESC);
                        lstResult.Add(temp);
                    }

                    // Content-Transfer-Encoding
                    if (!ParseQuotedString(ref bodyStruct, ref temp))
                    {
                        Log.Write("Failed getting the Content Encoding.", 1);
                        return false;
                    }

                    System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_ENCODING + "=" + temp);
                    lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_ENCODING);
                    lstResult.Add(temp);

                    // Content Size in bytes
                    if (!ParseString(ref bodyStruct, ref temp))
                    {
                        Log.Write("Failed getting the Content Size.", 1);
                        return false;
                    }

                    System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_SIZE + "=" + temp);
                    lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_SIZE);
                    lstResult.Add(temp);

                    temp = type + "/" + subType;
                    if (temp.ToLower() == ImapBodyStructure.IMAP_MESSAGE_RFC822.ToLower()) //email attachment
                    {
                        if (!ParseEnvelope(ref bodyStruct, ref lstResult))
                        {
                            Log.Write("Failed getting the Message Envelope.", 1);
                            return false;
                        }

                        if (!ParseBodyStructure(messageId, ref bodyStruct, partPrefix, ref lstResult))
                        {
                            Log.Write("Failed getting Attached Message.", 1);
                            return false;
                        }
                        // No of Lines in the message
                        if (!ParseString(ref bodyStruct, ref temp))
                        {
                            Log.Write("Failed getting the Content Lines.", 1);
                            return false;
                        }
                        if (temp.Length > 0)
                        {

                            System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_LINES + "=" + temp);
                            lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_LINES);
                        }
                    }
                    else if (type == "TEXT") //simple text
                    {
                        // No of Lines in the message
                        if (!ParseString(ref bodyStruct, ref temp))
                        {
                            Log.Write("Failed getting the Content Lines.", 1);
                            return false;
                        }
                        if (temp.Length > 0)
                        {
                            System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_LINES + "=" + temp);
                            lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_LINES);
                            lstResult.Add(temp);
                        }
                    }
                    // MD5 CRC Error Check
                    // Don't know what to do with it
                    if (bodyStruct[0] == ' ')
                    {
                        if (!ParseString(ref bodyStruct, ref temp))
                            return false;
                    }
                }
                else // MULTIPART
                {
                    bMultiPart = true;
                    // remove the open paranthesis
                    bodyStruct = bodyStruct.Substring(1);
                    uint nPartNumber = 0;
                    String partNumber = "";
                    do
                    {
                        // prepare next part number
                        nPartNumber++;

                        if (partPrefix.Length > 0)
                        {
                            partNumber = partPrefix + "." + nPartNumber.ToString();
                            if (1 == nPartNumber)
                                bodyPartNumber = partNumber;

                        }
                        else
                        {
                            partNumber = nPartNumber.ToString();
                            if (1 == nPartNumber)
                                bodyPartNumber = partNumber;
                        }

                        // add a new child to the part with
                        // an empty attribute array. This array will be filled
                        // in the "if" condition block.                        
                        for (int i = 0; i < lstResult.Count / 2; i = i + 2)
                        {
                            System.Diagnostics.Debug.WriteLine(lstResult[i] + "=" + lstResult[i + 1]);
                        }

                        // add a new child to the part with
                        // an empty attribute array. This array will be filled
                        // in the "if" condition block.
                        // parse this body part
                        if (!ParseBodyStructure(messageId, ref bodyStruct, partNumber, ref lstResult))
                        {
                            return false;
                        }
                    }
                    while (bodyStruct[0] == '('); // For each body part
                    // Content-type
                    String type = ImapBodyStructure.IMAP_MESSAGE_MULTIPART;
                    String subType = "";
                    if (!GetContentType(ref bodyStruct, ref type, ref subType, ref temp))
                    {
                        return false;
                    }
                    System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_TYPE + "=" + temp);
                    lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_TYPE);
                    lstResult.Add(temp);
                }
                //----------------------------------
                // COMMON FOR SINGLE AND MULTI PART

                // The Content-Disposition Header Field
                // Reference: http://tools.ietf.org/html/rfc2183
                if (bodyStruct[0] == ' ')
                {
                    if (!GetContentDisposition(ref bodyStruct, ref temp))
                    {
                        Log.Write("Failed getting the Content Disp.", 1);
                        return false;
                    }
                    if (temp.Length > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(messageId + ":" + partPrefix + ":" + ImapBodyStructure.IMAP_MESSAGE_CONTENT_DISP + "=" + temp);
                        lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_CONTENT_DISP);
                        lstResult.Add(temp);

                        // Check the attachment disposition type
                        if (temp.Contains("attachment"))
                        {
                            // Find attachment
                            String filename = temp;
                            Int32 nStart, nEnd;
                            nStart = filename.IndexOf("filename");
                            if( nStart != -1)
                            {
                                nEnd = filename.IndexOf(';', nStart);

                                if (nEnd != -1)
                                    filename = filename.Substring(nStart, nEnd - nStart);
                                else
                                    filename = filename.Substring(nStart);
                                nStart = filename.IndexOf('"');
                                nEnd = filename.LastIndexOf('"');
                                filename = filename.Substring(nStart+1, nEnd - nStart-1);
                                this.dicAttachments.Add(partPrefix, filename);
                            }
                        }
                    }
                }
                // Language
                if (bodyStruct[0] == ' ')
                {
                    if (!ParseLanguage(ref bodyStruct, ref temp))
                        return false;
                }
                // Extension data
                while (bodyStruct[0] == ' ')
                    if (!ParseExtension(ref bodyStruct, ref temp))
                        return false;

                // this is the end of the body part
                if (!FindAndRemove(ref bodyStruct, ')'))
                    return false;

                // Finally, set the attribute array to the part
                // EXCEPTION : if multipart and this is the root
                // part, the header is already prepared in the
                // GetBodyStructure function and hence DO NOT set it.

                if (!bMultiPart || partPrefix.Length > 0)
                {
                    int nCount = lstResult.Count;
                    for (int i = 0; i < nCount; i = i + 2)
                    {
                        System.Diagnostics.Debug.WriteLine(lstResult[i] + "=" + lstResult[i + 1]);
                    }

                }
                return true;
            }
            

            Log.Write("Invalid Body Structure", 1);
            return false;
        }

        /// <summary>
        /// Get the header for specific partNumber and Message id
        /// </summary>
        /// <param name="messageId">Unique Identifier of message</param>
        /// <param name="partNumber"> Message part number. Value 0 is meaning the header of message</param>
        ///<returns>Return a list which stores pair of key/value</returns>
        private List<String> GetHeader(String messageId, String partNumber)
        {
            List<String> lstHeader = new List<string>();

            String command = ImapCommand.Fetch;
            if (partNumber == "0")
            {
                command += " " + messageId + " " + "BODY[HEADER]" + IMAP_COMMAND_EOL;
            }
            else
            {
                command += messageId + " " + "BODY[" + partNumber + ".MIME]" + IMAP_COMMAND_EOL;
            }

            try
            {
                List<String> lstResult = new List<string>();
                if (SendAndReceive(command, ref lstResult))
                {
                    String key = "";
                    String value = "";
                    String lastLine = IMAP_SERVER_RESPONSE_OK;
                    foreach (String line in lstResult)
                    {
                        if (line.Length <= 0 ||
                            line.StartsWith(IMAP_UNTAGGED_RESPONSE_PREFIX) ||
                            line == ")")
                        {
                            continue;
                        }
                        else if (line.StartsWith(lastLine))
                        {
                            break;

                        }
                        int nPos = line.IndexOf(" ");
                        if (nPos != -1)
                        {
                            String tempLine = line.Substring(0, nPos);
                            nPos = tempLine.IndexOf(":");
                        }
                        if (nPos != -1)
                        {
                            if (key.Length > 0)
                            {
                                lstHeader.Add(key);
                                lstHeader.Add(value);
                            }
                            key = line.Substring(0, nPos).Trim();
                            value = line.Substring(nPos + 1).Trim();
                        }
                        else
                        {
                            value += line.Trim();
                        }
                    }
                    if (key.Length > 0)
                    {
                        lstHeader.Add(key);
                        lstHeader.Add(value);
                    }
                }              
            }
            catch (Exception ex)
            {
                LogOut();
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return lstHeader;
        }
        
        /// <summary>
        /// Check if this message is multipart
        /// To Identify multipart message, the content-type is either
        /// multipart or rfc822
        /// </summary>
        /// <param name="lstHeader"></param>
        /// <returns>true, multipart; otherwise false.</returns>
        private Boolean IsMultipart(List<String> lstHeader)
        {
            for (int i = 0; i < lstHeader.Count; i = i + 2)
            {
                if (lstHeader[i].ToString().ToLower() == ImapBodyStructure.IMAP_MESSAGE_CONTENT_TYPE.ToLower())
                {
                    String value = lstHeader[i + 1].ToString();
                    value = value.ToLower();
                    if (value.StartsWith(ImapBodyStructure.IMAP_MESSAGE_MULTIPART.ToLower()) ||
                        value.StartsWith(ImapBodyStructure.IMAP_MESSAGE_RFC822.ToLower()))
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if starts with NIL
        /// </summary>
        /// <param name="bodyStruct">Body Structure</param>
        /// <returns>true/false</returns>
        private Boolean IsNilString(ref String bodyStruct)
        {
            String sub = bodyStruct.Substring(0, 3);
            if (sub == IMAP_MESSAGE_NIL)
            {
                bodyStruct = bodyStruct.Substring(3);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the content type
        /// </summary>
        /// <param name="bodyStruct">Body Structure</param>
        /// <param name="type">Content Type</param>
        /// <param name="subType">Sub Type</param>
        /// <param name="contentType">Content Type Value</param>
        /// <returns>True/false</returns>
        private Boolean GetContentType(ref String bodyStruct, ref String type, ref String subType, ref String contentType)
        {
            contentType = ImapBodyStructure.IMAP_PLAIN_TEXT;

            // get the type and the subtype strings from body struct
            // If not found, set it to the default value plain/text.

            if (type.Length < 1)
            {
                if (!ParseQuotedString(ref bodyStruct, ref type))
                {
                    Log.Write("Failed getting Content-Type.", 1);
                    return false;
                }
            }

            subType = "";
            if (!ParseQuotedString(ref bodyStruct, ref subType))
            {
                Log.Write("Failed getting Content-Sub-Type.", 1);
                return false;
            }

            if (type.Length > 0 && subType.Length > 0)
            {
                contentType = type + "/" + subType;
            }

            // Add extra parameters (if any) to the Content-type
            List<String> lstParam = new List<string>();
            if (!ParseParameters(ref bodyStruct, lstParam))
            {
                Log.Write("Failed getting Content-Type Parameters.", 1);
                return false;
            }

            for (int i = 0; i < lstParam.Count; i += 2)
            {
                String temp = "; " + lstParam[i].ToString() + "=\"" + lstParam[i + 1].ToString() + "\"";
                contentType += temp;
            }

            return true;
        }

        /// <summary>
        /// Get Content Disposition
        /// </summary>
        /// <param name="bodyStruct"> Body Structure</param>
        /// <param name="result">Content Disposition</param>
        /// <returns>true if success</returns>
        private Boolean GetContentDisposition(ref String bodyStruct,ref String result)
        {
            result = "";

            // remove any spaces at the beginning and the end
            bodyStruct = bodyStruct.Trim();

            // check if this is NIL
            if (IsNilString(ref bodyStruct))
                return true;

            // find and remove opening paranthesis
            if (!FindAndRemove(ref bodyStruct, '('))
                return false;

            // get the content disposition type (inline/attachment)
            if (!ParseQuotedString(ref bodyStruct, ref result))
            {
                Log.Write("Failed getting Content Disposition.", 1);
                return false;
            }
            // get the associated parameters if any
            List<String> lstParam = new List<string>();
            if (!ParseParameters(ref bodyStruct, lstParam))
            {
                Log.Write("Failed getting Content Disposition params.", 1);
                return false;
            }

            // prepare the content-disposition
            String temp = "";
            for (int i = 0; i < lstParam.Count; i += 2)
            {
                temp = "; " + lstParam[i].ToString() + "=\"" + lstParam[i + 1].ToString() + "\"";
                result += temp;
            }

            // remove the closing paranthesis
            return FindAndRemove(ref bodyStruct, ')');
        }

        /// <summary>
        /// Parse the quoted String in body structure
        /// </summary>
        /// <param name="bodyStruct">Body Structure</param>
        /// <param name="result">"Quoted String</param>
        /// <returns>true is no error </returns>
        private Boolean ParseQuotedString(ref String bodyStruct, ref String result)
        {
            Boolean bResult = false;
            result = "";

            try
            {
                // remove any spaces at the beginning and the end
                bodyStruct = bodyStruct.Trim();

                // check if this is NIL
                if (IsNilString(ref bodyStruct))
                    bResult = true;
                else
                {

                    if (bodyStruct[0] == '"')
                    {
                        // extract the part within quotes
                        char ch;
                        int nEnd;
                        for (nEnd = 1; (ch = bodyStruct[nEnd]) != '"'; nEnd++)
                        {
                            // handle \"
                            if (ch == '\\')
                                nEnd++;
                        }
                        result = bodyStruct.Substring(1, nEnd - 1);
                        bodyStruct = bodyStruct.Substring(nEnd + 1);
                        bResult = true;
                    }
                    else
                    {
                        Log.Write("InValid Body Structure " + bodyStruct + ".", 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return bResult;
        }

        /// <summary>
        /// Parse the String (seperated by spaces or parenthesis)
        /// </summary>
        /// <param name="bodyStruct">Body Struct</param>
        /// <param name="result">return result</param>
        /// <returns>true, no parse error; otherwise, false.</returns>
        private Boolean ParseString(ref String bodyStruct, ref String result)
        {
            Boolean bResult = false;
            result = "";

            try
            {
                // remove any spaces at the beginning and the end
                bodyStruct = bodyStruct.Trim();
                // check if this is NIL
                if (IsNilString(ref bodyStruct))
                    bResult = true;
                else
                {
                    // extract the literal as whole looking for a
                    // space or closing paranthesis character
                    char ch;
                    int nEnd, nLen;
                    nLen = bodyStruct.Length;

                    for (nEnd = 0; nEnd < nLen; nEnd++)
                    {
                        ch = bodyStruct[nEnd];

                        if (ch == ' ' || ch == ')')
                            break;
                    }

                    if (nEnd > 0)
                    {
                        result = bodyStruct.Substring(0, nEnd);
                        bodyStruct = bodyStruct.Substring(nEnd);
                    }

                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return bResult;
        }

        /// <summary>
        /// Parse the language or list of languages in body structure
        /// </summary>
        /// <param name="bodyStruct">Bosy structure</param>
        /// <param name="result">Languages</param>
        /// <returns>true if success</returns>
        private Boolean ParseLanguage(ref String bodyStruct, ref String result)
        {
            result = "";

            // remove any spaces at the beginning and the end
            bodyStruct = bodyStruct.Trim();

            if (bodyStruct[0] == '(')
            { // Language list

                // remove the opening paranthesis
                if (!FindAndRemove(ref bodyStruct, '('))
                    return false;

                // TO DO
                // Logic for parsing language list is not yet
                // written. To be added in the future.

                // remove the closing paranthesis
                if (!FindAndRemove(ref bodyStruct, ')'))
                    return false;
            }
            else
            { // One or no language

                if (!ParseQuotedString(ref bodyStruct, ref result))
                {
                    Log.Write("Failed getting Content Language.", 1);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Parse the parameter in body structure
        /// </summary>
        /// <param name="bodyStruct">Body structure</param>
        /// <param name="lstResult">parameter</param>
        /// <returns>true if success</returns>
        private Boolean ParseParameters(ref String bodyStruct, List<String> lstResult)
        {
            // remove any spaces at the beginning and the end
            bodyStruct = bodyStruct.Trim();

            // check if this is NIL
            if (IsNilString(ref bodyStruct))
                return true;

            // remove the opening paranthesis
            if (!FindAndRemove(ref bodyStruct, '('))
                return false;

            String name = "";
            String value = "";
            while (bodyStruct[0] != ')')
            {

                // Name
                if (!ParseQuotedString(ref bodyStruct, ref name))
                {
                    Log.Write("Invalid Body Parameter Name.", 1);
                    return false;
                }

                // Value
                if (!ParseQuotedString(ref  bodyStruct, ref value))
                {
                    Log.Write("Invalid Body Parameter Value.", 1);
                    return false;
                }
                lstResult.Add(name);
                lstResult.Add(value);
            }

            // remove the closing paranthesis
            return FindAndRemove(ref bodyStruct, ')');
        }

        /// <summary>
        /// Parse the extension in body structure
        /// </summary>
        /// <param name="bodyStruct">body structure</param>
        /// <param name="result">extension</param>
        /// <returns>true if success</returns>
        private Boolean ParseExtension(ref String bodyStruct, ref String result)
        {
            result = "";

            // remove any spaces at the beginning and the end
            bodyStruct = bodyStruct.Trim();

            // check if this is NIL
            if (IsNilString(ref bodyStruct))
                return true;

            // TO DO
            // Dont know what to do with the data.

            return true;
        }

        /// <summary>
        /// Parse the address String
        /// </summary>
        /// <param name="bodyStruct">body structure</param>
        /// <param name="result">address</param>
        /// <returns>true if success</returns>
        private Boolean ParseAddressList(ref String bodyStruct, ref String result)
        {
            result = "";

            // remove any spaces at the beginning and the end
            bodyStruct = bodyStruct.Trim();

            // check if this is NIL
            if (IsNilString(ref bodyStruct))
                return true;

            // remove the opening paranthesis
            if (!FindAndRemove(ref bodyStruct, '('))
                return false;

            // process each address
            String address = "";
            while (bodyStruct[0] == '(')
            {

                // Get each address in the list
                if (!ParseAddress(ref bodyStruct, ref address))
                {
                    return true;
                }

                // prepare the address list (as comma separated
                // list of addresses).
                if (address.Length > 0)
                {
                    if (result.Length > 0)
                        result += ", ";
                    result += address;
                }

                bodyStruct = bodyStruct.Trim();
            }

            // remove the closing paranthesis
            return FindAndRemove(ref bodyStruct, ')');
        }

        /// <summary>
        /// Parse one address and format the String
        /// </summary>
        /// <param name="bodyStruct">body structure</param>
        /// <param name="result">address</param>
        /// <returns>true if success</returns>
        private Boolean ParseAddress(ref String bodyStruct, ref String result)
        {
            result = "";

            // remove any spaces at the beginning and the end
            bodyStruct = bodyStruct.Trim();

            // check if this is NIL
            if (IsNilString(ref bodyStruct))
                return true;

            // remove the opening paranthesis
            if (!FindAndRemove(ref bodyStruct, '('))
                return false;

            String personal = "";
            String emailId = "";
            String emailDomain = "";
            String temp = "";

            // Personal Name
            if (!ParseQuotedString(ref bodyStruct, ref personal))
            {
                Log.Write("Failed getting the Personal Name.", 1);
                return false;
            }
            // At Domain List (Right now, don't know what to do with this)
            if (!ParseQuotedString(ref bodyStruct, ref temp))
            {
                Log.Write("Failed getting the Domain List.", 1);
                return false;
            }
            // Email Id
            if (!ParseQuotedString(ref bodyStruct, ref emailId))
            {
                Log.Write("Failed getting the Email Id.", 1);
                return false;
            }
            // Email Domain
            if (!ParseQuotedString(ref bodyStruct, ref emailDomain))
            {
                Log.Write("Failed getting the Email Domain.", 1);
                return false;
            }

            if (emailId.Length > 0)
            {
                if (personal.Length > 0)
                {
                    if (emailDomain.Length > 0)
                    {
                        result = personal + " <" +
                                  emailId + "@" +
                                  emailDomain + ">";
                    }
                    else
                    {
                        result = personal + " <" +
                                  emailId + ">";
                    }
                }
                else
                {
                    if (emailDomain.Length > 0)
                    {
                        result = emailId + "@" + emailDomain;
                    }
                    else
                    {
                        result = emailId;
                    }
                }
            }

            // remove the closing paranthesis
            return FindAndRemove(ref bodyStruct, ')');
        }

        /// <summary>
        /// Parser RFC822 envelope
        /// Reference: http://tools.ietf.org/html/rfc822
        /// </summary>
        /// <param name="bodyStruct">body structure string</param>
        /// <param name="result"> result of envelope with pair key/value</param>
        /// <returns></returns>
        private Boolean ParseEnvelope(ref String bodyStruct, ref List<String> lstResult)
        {
            // remove any spaces at the beginning and the end
            bodyStruct = bodyStruct.Trim();

            // check if this is NIL
            if (IsNilString(ref bodyStruct))
                return true;

            // look for '(' character
            if (!FindAndRemove(ref bodyStruct, '('))
                return false;

            String temp = "";

            // Date
            if (!ParseQuotedString(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope Date.", 1);
                return false;
            }
            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_DATE_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_DATE_TAG);
                lstResult.Add(temp);
            }

            // Subject
            if (!ParseQuotedString(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope Subject.", 1);
                return false;
            }

            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_SUBJECT_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_SUBJECT_TAG);
                lstResult.Add(temp);
            }

            // From
            if (!ParseAddressList(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope From.", 1);
                return false;
            }

            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_FROM_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_FROM_TAG);
                lstResult.Add(temp);
            }

            // Sender
            if (!ParseAddressList(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope Sender.",1);
                return false;
            }

            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_SENDER_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_SENDER_TAG);
                lstResult.Add(temp);
            }

            // ReplyTo
            if (!ParseAddressList(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope Reply-To.", 1);
                return false;
            }

            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_REPLY_TO_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_REPLY_TO_TAG);
                lstResult.Add(temp);
            }

            // To
            if (!ParseAddressList(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope To.", 1);
                return false;
            }

            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_TO_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_TO_TAG);
                lstResult.Add(temp);
            }

            // Cc
            if (!ParseAddressList(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope CC.", 1);
                return false;
            }

            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_CC_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_CC_TAG);
                lstResult.Add(temp);
            }

            // Bcc
            if (!ParseAddressList(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope BCC.",1);
                return false;
            }
            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_BCC_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_BCC_TAG);
                lstResult.Add(temp);
            }

            // In-Reply-To
            if (!ParseQuotedString(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope In-Reply-To.", 1);
                return false;
            }
            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_HEADER_IN_REPLY_TO_TAG + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_HEADER_IN_REPLY_TO_TAG);
                lstResult.Add(temp);
            }

            // Message Id
            if (!ParseQuotedString(ref bodyStruct, ref temp))
            {
                Log.Write("Invalid Message Envelope Message Id.",1);
                return false;
            }
            if (temp.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(ImapBodyStructure.IMAP_MESSAGE_ID + "=" + temp);
                lstResult.Add(ImapBodyStructure.IMAP_MESSAGE_ID);
                lstResult.Add(temp);
            }

            // remove the closing paranthesis
            return FindAndRemove(ref bodyStruct, ')');
        }

        /// <summary>
        /// find the given character and remove
        /// </summary>
        /// <param name="bodyStruct">body structure</param>
        /// <param name="ch">first character to find and remove</param>
        /// <returns>true if success</returns>
        private Boolean FindAndRemove(ref String bodyStruct, char ch)
        {
            bodyStruct = bodyStruct.Trim();
            if (bodyStruct[0] != ch)
            {
                Log.Write("Invalid Body Structure " + bodyStruct + ".", 1);
                return false;
            }

            // remove character
            bodyStruct = bodyStruct.Substring(1);

            return true;
        }
        /// <summary>
        /// Get the content of Email
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>the string of body</returns>
        private String GetMessageBody(String messageId)
        {

            List<String> lstResult = new List<string>();
            lstResult = GetBodyStructure(messageId);

            String command = ImapCommand.Fetch + " " + messageId + " BODY.PEEK[" + bodyPartNumber + "]" + IMAP_COMMAND_EOL;
//            List<String> lstResult = new List<string>();
            lstResult = new List<string>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (SendAndReceive(command, ref lstResult))
                {
                    foreach (String str in lstResult)
                    {
                        if (str.StartsWith(IMAP_SERVER_RESPONSE_OK) ||
                            str.Contains(IMAP_UNTAGGED_RESPONSE_PREFIX + " " + messageId) ||
                            str == ")")
                            continue;

                        sb.Append(str.Contains("=20") ? str.Replace("=20", "") : str).AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get attachments from exchange server
        /// </summary>
        /// <param name="messageId">the id of message</param>
        /// <returns>list of attachments</returns>
        private List<Attachment> GetAttachments(String messageId)
        {
            List<Attachment> lstAttachments = new List<Attachment>();

            foreach (String key in dicAttachments.Keys)
            {
                //Get Attachment from exchange server
                String command = ImapCommand.Fetch + " " + messageId + " BODY.PEEK[" + key + "]" + IMAP_COMMAND_EOL;
                List<String> lstResult = new List<string>();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (SendAndReceive(command, ref lstResult))
                {
                    foreach (String str in lstResult)
                    {
                        if (str.StartsWith(IMAP_SERVER_RESPONSE_OK) ||
                            str.Contains(IMAP_UNTAGGED_RESPONSE_PREFIX + " " + messageId) ||
                            str == ")")
                            continue;

                        sb.Append(str);
                    }

                    try
                    {
                        byte[] binaryData;
                        binaryData = System.Convert.FromBase64String(sb.ToString());
                        Attachment att = new Attachment(new MemoryStream(binaryData), dicAttachments[key]);
                        att.ContentDisposition.FileName = dicAttachments[key];
                        lstAttachments.Add(att);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Can't get attachemtn [" + dicAttachments[key] + "] from message " + messageId);
                }
            }

            return lstAttachments;
        }
        #endregion

        /*
        private void GetAttachments(String messageId)
        {

            foreach (String key in dicAttachments.Keys)
            {
                //Get Attachment from exchange server
                String command = ImapCommand.Fetch + " " + messageId + " BODY.PEEK[" + key + "]" + IMAP_COMMAND_EOL;
                List<String> lstResult = new List<string>();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (SendAndReceive(command, ref lstResult))
                {
                    foreach (String str in lstResult)
                    {
                        if (str.StartsWith(IMAP_SERVER_RESPONSE_OK) ||
                            str.Contains(IMAP_UNTAGGED_RESPONSE_PREFIX + " " + messageId) ||
                            str == ")")
                            continue;

                        sb.Append(str);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Can't get attachemtn [" + dicAttachments[key] + "] from message " + messageId);
                }

                System.IO.FileStream writer = null;
                try
                {
                    writer = new System.IO.FileStream(dicAttachments[key], System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    byte[] binaryData;
                    binaryData = System.Convert.FromBase64String(sb.ToString());
                    writer.Write(binaryData, 0, binaryData.Length);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    if (null != writer)
                        writer.Close();
                }

            }
        }
*/
    }
}


