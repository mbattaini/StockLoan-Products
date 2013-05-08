using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using StockLoan.Common;

namespace StockLoan.Inventory
{
    internal class SmtpMailClient //: EmailClient
    {

        private SmtpClient _client = null;
        public Boolean Send(MailMessage msg)
        {
            Boolean bStatus = false;
            try
            {
                _client.Send(msg);
                bStatus = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), Log.Error, 1);
            }
            return bStatus;
        }

        public SmtpMailClient(String host, String username, String password)
        {
            _client = new SmtpClient();
            _client.Host = host;
            _client.Credentials = new NetworkCredential(username, password);
        }
    }
}
