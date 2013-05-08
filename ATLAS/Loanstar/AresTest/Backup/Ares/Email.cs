using System;
using System.Web.Mail;
using Anetics.Common;

namespace Anetics.Ares
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Email
	{
		private string mailServer;		
		
		public Email(string dbCnStr)
		{
			this.mailServer =  KeyValue.Get("SmtpHost", "EXPENVS1.penson.com", dbCnStr);			
		}

		public void Send(string to, string from, string subject, string content)
		{
			Send(to, from, subject, content, "");
		}

		public void Send(string to, string from, string subject, string content, string attachments)
		{
			try
			{
				MailMessage mailMessage = new MailMessage();

				mailMessage.BodyFormat = MailFormat.Text;
				SmtpMail.SmtpServer = mailServer;
        
				mailMessage.To = to;
				mailMessage.From = from;
				mailMessage.Subject = subject;
				mailMessage.Body = content;
				
				if (!attachments.Equals(""))
				{
					MailAttachment mailAttachment = new MailAttachment(attachments);
					mailMessage.Attachments.Add(mailAttachment);
				}

				SmtpMail.Send(mailMessage);		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Email.Send]", Log.Error, 1);				
				throw;
			}
		}
	}
}
