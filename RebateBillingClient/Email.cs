using System;
using System.Collections.Generic;
using System.Text;
using OutLook = Microsoft.Office.Interop.Outlook;

namespace Golden
{
    class Email       
    {
        private OutLook._Application outLookApp;
        private OutLook._NameSpace ns = null;
        private OutLook.PostItem item = null;
        private OutLook.MAPIFolder inboxFolder = null;
        private OutLook.MAPIFolder subFolder = null;

        private bool available = false;

        public Email() : this(null, null, true) { }    

        public Email(string profile, string password, bool newSession)
        {
            try
            {
                outLookApp = new OutLook.Application();
                ns = outLookApp.GetNamespace("MAPI");

                ns.Logon(profile, password, false, newSession);
                inboxFolder = ns.GetDefaultFolder(OutLook.OlDefaultFolders.olFolderOutbox);

                available = true;
            }
            catch
            {                
                throw;
            }
        }


        public void Send(string recipientList, string ccList, string subject, string body, string attachmentList)
        {
            try
            {   //Allow outlook email to load without (Send To) recipient, let user enter it.

                OutLook._MailItem outlookMsg = (OutLook._MailItem)outLookApp.CreateItem(OutLook.OlItemType.olMailItem);

                if (recipientList != null && !recipientList.Equals(""))
                {
                    foreach (string recipient in recipientList.Split(';'))
                    {
                        outlookMsg.Recipients.Add(recipient);
                    }
                }

                outlookMsg.BodyFormat = OutLook.OlBodyFormat.olFormatPlain;
                outlookMsg.Subject = subject;
                outlookMsg.Body = body;

                if (attachmentList != null && !attachmentList.Equals(""))
                {
                    int n = 0;

                    foreach (string file in attachmentList.Split(';'))
                    {
                        outlookMsg.Attachments.Add(file, OutLook.OlAttachmentType.olByValue, 1, "");
                        n++;
                    }
                }

                outlookMsg.Display(false);
                
            }
            catch
            {
                throw;
            }
        }

    }
}
