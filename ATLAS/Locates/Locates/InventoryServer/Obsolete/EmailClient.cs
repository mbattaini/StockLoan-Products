using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace StockLoan.Inventory
{
    public class EmailClient
    {
        public EmailClient(String host, String username, String password)
        {
            sendClient = new SmtpMailClient(host, username, password);
            getClient = new ImapClient(host, username, password);
        }

        SmtpMailClient sendClient = null;
        ImapClient getClient = null;

        public Boolean Send(MailMessage massage)
        {
            return sendClient.Send(massage);
        }

        public List<MailMessage> Retrieve(EmailSearchCriteria criteria, String mailbox)
        {
            return getClient.GetMail(criteria, mailbox);
        }

        public Boolean SaveAttachment(MailMessage mail, String path)
        {
            Boolean bResult = false;

            if (mail.Attachments.Count > 0)
            {
                foreach (Attachment att in mail.Attachments)
                {
                    FileStream writer = null;
                    try
                    {
                        if (Directory.Exists(path))
                        {
                            String filename = att.Name;
                            if (path.LastIndexOf('\\') == (path.Length - 1))
                                filename = path + att.Name;
                            else
                                filename = path + "\\" + att.Name;

                            writer = new FileStream(filename, FileMode.Create, FileAccess.Write);
                            Int32 nCount = 1000;
                            byte[] data = new byte[nCount];
                            Int32 nBytesToRead = (Int32)att.ContentStream.Length;
                            
                            Int32 nBytesRead = 0;
                            while (nBytesToRead > 0)
                            {
                                Int32 i = att.ContentStream.Read(data, 0, nCount);
                                if (i == 0)
                                    break;

                                writer.Write(data, 0, data.Length);
                                nBytesRead += i;
                                nBytesToRead -= i;
                            }
                            writer.Close();
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Invalid Path");
                        }
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

            return bResult;
        }
    }
}
