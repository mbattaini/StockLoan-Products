using System;
using System.Net.Mail;
using StockLoan.Common;

namespace StockLoan.WorldWideDataService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Email
	{		
		public static void Send(string to, string from, string subject, string content, string dbCnStr)
		{
			Send(to, from, subject, content, "", dbCnStr);
		}

		public static void Send(string to, string from, string subject, string content, string attachments, string dbCnStr)
		{
            string mailServer = KeyValue.Get("SmtpHost", "EXPENVS1.penson.com", dbCnStr);	
            string toAddress = "";
            int index = 0;

            SmtpClient smtpClient = new SmtpClient(mailServer);

            try
            {

                MailMessage mailMessage = new MailMessage();

                while((toAddress = Tools.SplitItem(to, ";", index)) != "")
                {
                    mailMessage.To.Add(toAddress);
                    index++;
                }                
                
                mailMessage.From = new MailAddress(from);
                mailMessage.IsBodyHtml = false;
                mailMessage.Subject = subject;
                mailMessage.Body = content;

                if (!attachments.Equals(""))
                {
                    mailMessage.Attachments.Add(new Attachment(attachments));
                }

                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [Email.Send]", Log.Error, 1);
                throw;
            }
		}
	}
}
