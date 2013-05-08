using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using StockLoan.Common;

namespace StockLoan.Inventory
{
    internal class ImapBase
    {

        /// <summary>
        /// List available IMAP command for StockLoan project
        /// Reference: http://www.ietf.org/rfc/rfc3501.txt?number=3501 
        /// </summary>
        protected static class ImapCommand
        {
            /// <summary>
            /// The LOGIN command identifies the client to the server and carries
            /// the plaintext password authenticating this user.
            /// 
            /// Arguments:  user name
            ///             password
            ///
            /// Responses:  no specific responses for this command
            ///
            /// Result:     OK - login completed, now in authenticated state
            ///             NO - login failure: user name or password rejected
            ///             BAD - command unknown or arguments invalid
            /// 
            /// Example: A0001 LOGIN "username" password
            /// </summary>
            public const String Login = "LOGIN";

            /// <summary>
            /// The LOGOUT command informs the server that the client is done with
            /// the connection.
            /// 
            /// Arguments:  none
            ///
            /// Responses:  REQUIRED untagged response: BYE
            ///
            /// Result:     OK - logout completed
            ///            BAD - command unknown or arguments invalid
            /// 
            /// Example: A0001 LOGOUT
            /// </summary>
            public const String Logout = "LOGOUT";

            /// <summary>
            /// The SELECT command selects a mailbox so that messages in the
            /// mailbox can be accessed.
            /// 
            /// Arguments:  mailbox name
            ///
            /// Responses:  REQUIRED untagged responses: FLAGS, EXISTS, RECENT
            ///            REQUIRED OK untagged responses:  UNSEEN,  PERMANENTFLAGS,
            ///            UIDNEXT, UIDVALIDITY

            /// Result:     OK - select completed, now in selected state
            ///            NO - select failure, now in authenticated state: no
            ///                 such mailbox, can't access mailbox
            ///            BAD - command unknown or arguments invalid
            /// 
            /// Example: A0001 SELECT "Inbox"
            /// </summary>
            public const String Select = "SELECT";

            /// <summary>
            /// The FETCH command retrieves data associated with a message in the
            /// mailbox.
            /// 
            /// Arguments:  sequence set
            ///             message data item names or macro
            /// 
            /// Responses:  untagged responses: FETCH
            /// 
            /// Result:     OK - fetch completed
            ///             NO - fetch error: can't fetch that data
            ///             BAD - command unknown or arguments invalid
            ///                        
            /// Example:
            ///         A0001 FETCH 104 BODY[TEXT]
            ///         
            /// </summary>
            public  const String Fetch = "FETCH";

            /// <summary>
            /// The SEARCH command searches the mailbox for messages that match
            /// the given searching criteria.
            /// 
            /// Arguments:  OPTIONAL [CHARSET] specification
            ///               searching criteria (one or more)
            ///
            /// Responses:  REQUIRED untagged response: SEARCH
            ///
            /// Result:     OK - search completed
            ///             NO - search error: can't search that [CHARSET] or
            ///                  criteria
            ///             BAD - command unknown or arguments invalid
            ///             
            /// Example:    A0001 SEARCH FROM "Test@domain.com" UNSEEN
            ///             A0002 SEARCH FROM "Test@domain.com" ON 15-Aug-2008
            /// </summary>
            public const String Search = "SEARCH";

            /// <summary>
            /// The EXAMINE command is identical to SELECT and returns the same
            /// output; however, the selected mailbox is identified as read-only.
            /// No changes to the permanent state of the mailbox, including
            /// per-user state, are permitted; in particular, EXAMINE MUST NOT
            /// cause messages to lose the \Recent flag.
            /// 
            /// Arguments:  mailbox name
            /// 
            /// Responses:  REQUIRED untagged responses: FLAGS, EXISTS, RECENT
            /// REQUIRED OK untagged responses:  UNSEEN,  PERMANENTFLAGS,
            /// UIDNEXT, UIDVALIDITY
            /// 
            /// Result:     OK - examine completed, now in selected state
            ///             NO - examine failure, now in authenticated state: no
            ///             such mailbox, can't access mailbox
            ///             BAD - command unknown or arguments invalid
            /// Example:
            ///     A001 EXAMINE Inbox
            /// </summary>
            public const String Examine = "EXAMINE";
        }
        
        /// <summary>
        /// Respone send back from IMAP Server
        /// </summary>
        protected static class ImapResponse
        {
            /// <summary>
            /// The OK response indicates an information message from the server.
            /// When tagged, it indicates successful completion of the associated
            /// command.
            /// </summary>
            public const String OK = "OK";

            /// <summary>
            /// he BAD response indicates an error message from the server.  When
            /// tagged, it reports a protocol-level error in the client's command;
            /// the tag indicates the command that caused the error.
            /// /// </summary>
            public const String BAD = "BAD";

            /// <summary>
            /// The NO response indicates an operational error message from the
            /// server.  When tagged, it indicates unsuccessful completion of the
            /// associated command.
            /// </summary>
            public const String NO = "NO";

            /// <summary>
            /// The BYE response is always untagged, and indicates that the server
            /// is about to close the connection. 
            /// </summary>
            public const String BYE = "BYE";

            /// <summary>
            /// The PREAUTH response is always untagged, and is one of three
            /// possible greetings at connection startup.  It indicates that the
            /// connection has already been authenticated by external means; thus
            /// no LOGIN command is needed.
            /// </summary>
            public const String PREAUTH = "PREAUTH";
        }

        /// <summary>
        /// Imap BODYSTRUCTURE
        /// Reference: http://tools.ietf.org/html/rfc3501#section-7.4.2
        ///            http://tools.ietf.org/html/rfc2045
        /// </summary>
        protected static class ImapBodyStructure
        {
            /// <summary>
            /// Imap message content type : "Content-Type: "
            /// </summary>
            public const String IMAP_MESSAGE_CONTENT_TYPE = "content-type";
            /// <summary>
            /// Imap mesage content type: rfc822
            /// </summary>
            public const String IMAP_MESSAGE_RFC822 = "message/rfc822";
            /// <summary>
            /// Imap message id
            /// </summary>
            public const String IMAP_MESSAGE_ID = "message-id";
            /// <summary>
            /// Imap mesage content type: multipart
            /// </summary>
            public const String IMAP_MESSAGE_MULTIPART = "multipart";
            /// <summary>
            /// Imap content encoding : "Content-Transfer-Encoding: "
            /// </summary>
            public const String IMAP_MESSAGE_CONTENT_ENCODING = "content-transfer-encoding";
            /// <summary>
            /// Imap content description : "Content-Description: "
            /// </summary>
            public const String IMAP_MESSAGE_CONTENT_DESC = "content-description";
            /// <summary>
            /// Imap content disposition : "Content-Disposition: "
            /// </summary>
            public const String IMAP_MESSAGE_CONTENT_DISP = "content-disposition";
            /// <summary>
            /// Imap content size
            /// </summary>
            public const String IMAP_MESSAGE_CONTENT_SIZE = "content-size";
            /// <summary>
            /// Imap content lines
            /// </summary>
            public const String IMAP_MESSAGE_CONTENT_LINES = "content-lines";
            /// <summary>
            /// Imap message base64 encoding : BASE64
            /// </summary>
            public const String IMAP_MESSAGE_BASE64_ENCODING = "base64";
            /// <summary>
            /// Imap header Sender tag
            /// </summary>
            public const String IMAP_HEADER_SENDER_TAG = "sender";
            /// <summary>
            /// Imap header from tag
            /// </summary>
            public const String IMAP_HEADER_FROM_TAG = "from";
            /// <summary>
            /// Imap header in-reply-to tag
            /// </summary>
            public const String IMAP_HEADER_IN_REPLY_TO_TAG = "in-reply-to";
            /// <summary>
            /// IKmap header reply-to tag
            /// </summary>
            public const String IMAP_HEADER_REPLY_TO_TAG = "reply-to";
            /// <summary>
            /// Imap header to tag
            /// </summary>
            public const String IMAP_HEADER_TO_TAG = "to";
            /// <summary>
            /// Imap header cc tag
            /// </summary>
            public const String IMAP_HEADER_CC_TAG = "cc";
            /// <summary>
            /// Imap header bcc tag
            /// </summary>
            public const String IMAP_HEADER_BCC_TAG = "bcc";
            /// <summary>
            /// Imap header subject tag
            /// </summary>
            public const String IMAP_HEADER_SUBJECT_TAG = "subject";
            /// <summary>
            /// Imap header date tag
            /// </summary>
            public const String IMAP_HEADER_DATE_TAG = "date";
            /// <summary>
            /// Imap body type
            /// </summary>
            public const String IMAP_PLAIN_TEXT = "text/plain";
            /// <summary>
            /// Imap audio wave:  audio/wav
            /// </summary>
            public const String IMAP_AUDIO_WAV = "audio/wav";
            /// <summary>
            /// Imap video mpeg4  : video/mpeg4
            /// </summary>
            public const String IMAP_VIDEO_MPEG4 = "video/mpeg4";

        }

        /// <summary>
        /// Reference:http://tools.ietf.org/html/rfc3501#section-6.4.4
        /// </summary>
        protected static class ImapSearch
        {
            // Messages that contain the specified string in the envelope 
            // structure's FROM field.
            public const String FROM = ImapBodyStructure.IMAP_HEADER_FROM_TAG;

            // Messages that contain the specified string in the envelope
            // structure's SUBJECT field.
            public const String SUBJECT = ImapBodyStructure.IMAP_HEADER_SUBJECT_TAG;

            // Messages whose [RFC-2822] Date: header (disregarding time and
            // timezone) is within or later than the specified date.
            public const String SENTSINCE = "sentsince";

            // Messages whose [RFC-2822] Date: header (disregarding time and
            // timezone) is earlier than the specified date.
            public const String SENTBEFORE = "sentbefore";

        }

        protected static class ImapFetchCommand
        {
            public const String Body = "BODY";
            /// <summary>
            /// Message Header
            /// Example: IMAP01 105 BODY[HEADER]
            /// </summary>
            public const String MessageHeader = "BODY[HEADER]";

        }

        /// <summary>
        /// Imap server response result
        /// </summary>
        protected enum ImapResponseEnum
        {
            /// <summary>
            /// Imap Server responded "OK"
            /// </summary>
            IMAP_SUCCESS_RESPONSE,
            /// <summary>
            /// Imap Server responded "NO" or "BAD"
            /// </summary>
            IMAP_FAILURE_RESPONSE,
            /// <summary>
            /// Imap Server responded "*"
            /// </summary>
            IMAP_IGNORE_RESPONSE
        }

        /// <summary>
        /// Imap default port: 143
        /// </summary>
        protected const ushort IMAP_DEFAULT_PORT = 143;

        /// <summary>
        /// command prefix
        /// </summary>
        protected const String IMAP_COMMAND_PREFIX = "IMAP";

        /// <summary>
        /// Imap command terminator: \r\n
        /// </summary>
        protected const String IMAP_COMMAND_EOL = "\r\n";


        #region Attributes

        /// <summary>
        /// Each client command is prefixed with an identifier (typically a short 
        /// alphanumeric string, e.g., A0001, A0002, etc.) called a "tag".  
        /// A different tag is generated by the client for each command. 
        /// </summary>
        protected String IMAP_COMMAND_IDENTIFER
        {
            get
            {
                return IMAP_COMMAND_PREFIX + _commandSeq.ToString("0000") + " ";
            }
        }

        /// <summary>
        /// Imap Server OK response which is combination of 
        /// Imap Identifier and Imap OK response.
        /// eg. IMAP001 OK
        /// </summary>
        protected String IMAP_SERVER_RESPONSE_OK
        {
            get
            {
                return IMAP_COMMAND_IDENTIFER + ImapResponse.OK;
            }
        }

        /// <summary>
        /// Imap Server NO response which is combination of 
        /// Imap Identifier and Imap NO response.
        /// eg. IMAP001 NO
        /// </summary>
        protected String IMAP_SERVER_RESPONSE_NO
        {
            get
            {
                return IMAP_COMMAND_IDENTIFER + ImapResponse.NO;
            }

        }

        /// <summary>
        /// Imap Server BAD response which is combination of
        /// Imap Identifier and Imap BAD response.
        /// eg. IMAP001 BAD
        /// </summary>
        protected String IMAP_SERVER_RESPONSE_BAD
        {
            get
            {
                return IMAP_COMMAND_IDENTIFER + ImapResponse.BAD;
            }
        }

        /// <summary>
        /// Imap ok server response: "* OK"
        /// </summary>
        protected String IMAP_SERVER_CONNECT_OK
        {
            get
            {
                return IMAP_UNTAGGED_RESPONSE_PREFIX + " " + ImapResponse.OK;
            }
        }

        /// <summary>
        /// Get a value indicating whether the connection is created.
        /// </summary>
        /// <returns></returns>
        protected Boolean Connected
        {
            get
            {
                return null == client ? false : client.Connected;
            }
        }
        #endregion

        #region Constant
        /// <summary>
        /// Imap Untagged response prefix: *
        /// </summary>
        protected const String IMAP_UNTAGGED_RESPONSE_PREFIX = "*";


        /// <summary>
        /// Imap BodyStructure command
        /// </summary>
        protected const String IMAP_BODYSTRUCTURE_COMMAND = "BODYSTRUCTURE";


        /// <summary>
        /// Imap search command : SEARCH
        /// </summary>
        protected const String IMAP_SEARCH_RESPONSE = "SEARCH";

        /// <summary>
        /// Imap message nil size : NIL
        /// </summary>
        protected const String IMAP_MESSAGE_NIL = "NIL";

        /// <summary>
        /// Imap message default part : 1
        /// </summary>
        protected  const String IMAP_MSG_DEFAULT_PART = "1";

        #endregion

        #region Variables
        /// <summary>
        /// value of prefix
        /// </summary>
        protected static Int32 _commandSeq = 0;

        /// <summary>
        /// Imap host
        /// </summary>
        protected String _host = "";

        /// <summary>
        /// Imap port : default IMAP_DEFAULT_PORT : 143
        /// </summary>
        protected ushort _port = IMAP_DEFAULT_PORT;

        /// <summary>
        /// User id
        /// </summary>
        protected String _username = "";

        /// <summary>
        /// User Password
        /// </summary>
        protected String _password = "";

        /// <summary>
        /// Tcpclient object
        /// </summary>
        TcpClient client;

        /// <summary>
        /// Network stream object
        /// </summary>
        NetworkStream netWriter;

        /// <summary>
        /// StreamReader object
        /// </summary>
        StreamReader netReader;
        #endregion

        #region Methods
        /// <summary>
        /// Connect to exchange IMAP server
        /// </summary>
        /// <returns>true if the TcpClient connect created; otherwise, false.</returns>
        protected Boolean Connect()
        {
            _commandSeq = 0;
            Boolean bResult = false;
            
            try
            {
                client = new TcpClient(this._host, this._port);
                netWriter = client.GetStream();
                netReader = new StreamReader(client.GetStream());
                String result = netReader.ReadLine();
                if (result.StartsWith(IMAP_SERVER_CONNECT_OK) == true)
                {
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
        /// Disconnect connection with Imap server
        /// </summary>
        protected void Disconnect()
        {
            _commandSeq = 0;
            if( null != client && client.Connected)
            {
                try
                {
                    if (null != netWriter)
                        netWriter.Close();
                    if (null != netReader)
                        netReader.Close();
                }
                catch (Exception ex)
                {
                    Log.Write(ex.ToString(), Log.Error, 1);
                }
                client.Close();
            }
        }

        /// <summary>
        /// Send command to server
        /// </summary>
        /// <param name="command">Command to send Imap Server</param>
        /// <returns>true command sent to server succeed; otherwise, false</returns>
        protected Boolean Send(String command)
        {
            _commandSeq++;
            command = IMAP_COMMAND_IDENTIFER + command;
            Boolean bResult = false;

            System.Diagnostics.Debug.WriteLine("==>" + command.TrimEnd(IMAP_COMMAND_EOL.ToCharArray()));

            byte[] data = Encoding.ASCII.GetBytes(command.ToCharArray());
            try
            {
                netWriter.Write(data, 0, data.Length);
                bool bRead = true;
                while (bRead)
                {
                    String result = netReader.ReadLine();
                    System.Diagnostics.Debug.WriteLine(result);

                    if (result.StartsWith(IMAP_SERVER_RESPONSE_OK))
                    {
                        bRead = false;
                        bResult = true;
                    }
                    else if (result.StartsWith(IMAP_SERVER_RESPONSE_NO))
                    {
                        bRead = false;
                    }
                    else if (result.StartsWith(IMAP_SERVER_RESPONSE_BAD))
                    {
                        bRead = false;
                    }
                    else
                    {
                        Log.Write("Error happend in receiving: " + result, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
            }

            return bResult;
        }

        /// <summary>
        /// Send command to server and retrieve response
        /// </summary>
        /// <param name="command">Command to send Imap Server</param>
        /// <param name="lsgResult">Imap Server response</param>
        /// <returns>true command sent to server succeed; otherwise, false</returns>
        protected Boolean SendAndReceive(String command, ref List<String> lstResult)
        {
            _commandSeq++;
            command = IMAP_COMMAND_IDENTIFER + command;
            Boolean bResult = false;

            System.Diagnostics.Debug.WriteLine("==>" + command.TrimEnd(IMAP_COMMAND_EOL.ToCharArray()));

            byte[] data = Encoding.ASCII.GetBytes(command.ToCharArray());
            try
            {
                netWriter.Write(data, 0, data.Length);
                bool bRead = true;
                while (bRead)
                {
                    String result = netReader.ReadLine();
                    System.Diagnostics.Debug.WriteLine(result);
                    lstResult.Add(result);

                    if (result.StartsWith(IMAP_SERVER_RESPONSE_OK))
                    {
                        bRead = false;
                        bResult = true;
                    }
                    else if (result.StartsWith(IMAP_SERVER_RESPONSE_NO))
                    {
                        bRead = false;
                    }
                    else if (result.StartsWith(IMAP_SERVER_RESPONSE_BAD))
                    {
                        bRead = false;
                    }
                    else 
                    {
                        Log.Write("Error happend in receiving: " + result, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
            }

            return bResult;
        }

        /// <summary>
        /// Determines whether the beginning of this instance matches the specified string. 
        /// This method performs a word case-insensitive comparison
        /// </summary>
        /// <param name="source">the string need be check</param>
        /// <param name="value">the string to compare</param>
        /// <returns>True is value matches the beginning of this string; otherwise, false.</returns>
        protected Boolean StringStartsWith(String source, String value)
        {
            if (null == source || null == value)
                return false;

            return source.ToLower().StartsWith(value.ToLower());
        }

        #endregion

    }
}
