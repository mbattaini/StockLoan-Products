using System;
using System.Net.Mail;

using StockLoan.Common;

namespace StockLoan.XflNotification
{
	public class Email
	{
		private string mailServer;		
		
		public Email(string dbCnStr)
		{
			this.mailServer = KeyValue.Get("SmtpHost", "EXPENVS1.penson.com", dbCnStr);			
		}

		public void Send(string to, string from, string subject, string content)
		{
			try
			{
				MailMessage mailMessage = new MailMessage();
        mailMessage.To.Add(to);
        mailMessage.From = new MailAddress(from); 
				mailMessage.Subject = subject;
				mailMessage.Body = content;

        SmtpClient smtp = new SmtpClient();
        smtp.Host = mailServer;
        smtp.Send(mailMessage);		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Email.Send]", Log.Error, 1);				
				throw;
			}
		}
	}
}
