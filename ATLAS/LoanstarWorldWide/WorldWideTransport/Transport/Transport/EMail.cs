using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Net.Mail;

using StockLoan.Common;

namespace StockLoan.Transport
{
    public class Email
    {

        public static void Send(string fromAddress, string toAddress, string subject, string messageBody, string dbCnstr)
        {
            Send(fromAddress, toAddress, subject, messageBody, "", dbCnstr);
        }

        public static void Send(string fromAddress, string toAddress, string subject, string messageBody, string attachments, string dbCnstr)
        {
            string mailServer;

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();
                mailServer = KeyValue.Get("SmtpHost", "EXPENVS1.penson.com", dbCnstr);

                message.From = new MailAddress(fromAddress);

                if (toAddress.Trim().Length > 0)
                {
                    foreach (string addr in toAddress.Split(';'))
                    {
                        message.To.Add(new MailAddress(addr));
                    }
                }             

                if (!attachments.Equals(""))
                {
                    Attachment mailAttachment = new Attachment(attachments);

                    message.Attachments.Add(mailAttachment);
                }

                message.Subject = subject;
                message.Body = messageBody;

                client.Host = mailServer;

                client.Send(message);
            }
            catch (Exception error)
            {
                throw new Exception("Unable to send email message. Please try again in a few seconds : " + error.Message);
            }
        }
    }
}
