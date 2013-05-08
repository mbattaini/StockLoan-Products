// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Outlook = Microsoft.Office.Interop.Outlook;
//using Redemption;


namespace Anetics.Medalist
{
  public class Email
  {
		private Outlook._Application _application;
    private Outlook._NameSpace  _namespace;       
    private Outlook.MAPIFolder  _mapiFolder;				

		private bool available = false;
  
    private delegate void SendDelegate (string recipientList, string ccList, string subject, string body, string attachmentList, bool autoSend);
    private SendDelegate sendDelegate;
	
//		private Redemption.MAPIUtils utils;

		 public Email() : this(null, null, true) {}
    public Email(string profile, string password, bool newSession)
    {
      try
      {
        _application  = new Outlook.Application();    
        _namespace    = _application.GetNamespace("MAPI");
	//			utils = new MAPIUtilsClass();
				
        _namespace.Logon(profile, password, false, newSession);
        _mapiFolder = _namespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderOutbox);        
    
        sendDelegate = new SendDelegate(DoSend);
        available = true;          
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Email.Email]", Log.Error, 1);       
        throw;        
      }
    }
        
    public void Send (string body)
    {
      sendDelegate.BeginInvoke(null, null, RegistryValue.Name, body, null, false, null, null); 
    }

    public void Send (string subject, string body)
    {
      sendDelegate.BeginInvoke(null, null, subject, body, null, false, null, null); 
    }

    public void Send (string recipientList, string ccList, string subject, string body)
    {
      sendDelegate.BeginInvoke(recipientList, ccList, subject, body, null, false, null, null); 
    }

    public void Send (string recipientList, string ccList, string subject, string body, string attachmentList, bool autoSend)
    {
      sendDelegate.BeginInvoke(recipientList, ccList, subject, body, attachmentList, autoSend, null, null); 
    }

    private void DoSend (string recipientList, string ccList, string subject, string body, string attachmentList, bool autoSend)
    {
			try
			{
				if (!available)
				{
					Log.Write("Mail client is unavailable. [Email.DoSend]", Log.Error, 1);
					throw new Exception("Mail client is unavailable.");
				}
       
       	
				
				Outlook._MailItem outlookMsg = (Outlook._MailItem)_application.CreateItem(Outlook.OlItemType.olMailItem);
				
				if ((!autoSend) || recipientList.Equals(""))
				{
					if (recipientList != null && !recipientList.Equals(""))
					{
						foreach (string recipient in recipientList.Split(';'))
						{            
							outlookMsg.Recipients.Add(recipient);
						}
					}   
   
					outlookMsg.BodyFormat = Outlook.OlBodyFormat.olFormatPlain;        
					outlookMsg.Subject    = subject;
					outlookMsg.Body       = body;      
      
					if (attachmentList != null && !attachmentList.Equals(""))
					{
						int n = 0;

						foreach (string file in attachmentList.Split(';'))
						{
							Log.Write(file, 1);
							outlookMsg.Attachments.Add(file,  Outlook.OlAttachmentType.olByValue, 1, "");
							n++;
						}
					}

					outlookMsg.Display(false);
				}
				else
				{
					outlookMsg.Subject = subject;

					if (attachmentList != null && !attachmentList.Equals(""))
					{
						int n = 0;

						foreach (string file in attachmentList.Split(';'))
						{
							outlookMsg.Attachments.Add(file, Outlook.OlAttachmentType.olByValue, 1, "");
							n++;
						}
					}

					//Redemption.SafeMailItem sItem = new SafeMailItemClass();
/*					sItem.Item = outlookMsg;

					if (recipientList != null && !recipientList.Equals(""))
					{
						foreach (string recipient in recipientList.Split(';'))
						{            
							if (!recipient.Equals(""))
							{
								sItem.Recipients.Add(recipient);
							}
						}
					}   
   					
					sItem.Body = body;            			
					sItem.Send();	*/
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Email.DoSend]", Log.Error, 1);
			}
    }
  }
}

