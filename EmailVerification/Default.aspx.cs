using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string emailAddress = "";

        LogWriter lgWriter = new LogWriter();

        try
        {
            emailAddress = ClientQueryString.Substring(ClientQueryString.IndexOf('=') + 1);
            emailAddress = emailAddress.Replace("%40", "@");
            lgWriter.Write(emailAddress);

            LabelEmailAddr.Text = "Email Address " + emailAddress + " has successsfully opted in to continue recieiving reports via email.";
        }
        catch
        {
            LabelEmailAddr.Text = "There was a error processing your request, Please Contact helpdesk@apexclearing.com";
        }
    }
}