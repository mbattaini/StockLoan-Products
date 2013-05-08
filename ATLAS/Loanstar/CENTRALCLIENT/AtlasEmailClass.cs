using System;
using System.Collections.Generic;
using System.Text;
using OutLook = Microsoft.Office.Interop.Outlook;

namespace CentralClient
{
  class AtlasEmailClass
  {
    private OutLook._Application outLookApp;
    private OutLook._NameSpace ns = null;
    private OutLook.PostItem item = null; 
    private OutLook.MAPIFolder inboxFolder = null;
    private OutLook.MAPIFolder subFolder = null;


    public AtlasEmailClass(string profile, string password, bool newSession)
    {
      outLookApp = new OutLook._Application();
    }


    public string[] GetMails(string from, string to, string subjectSearch, bool isNew)
    {
      try 
      {
        app = new Microsoft.Office.Interop.Outlook.Application();
        ns = app.GetNamespace("MAPI");
        ns.Logon(null,null,false, false);

        inboxFolder = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
        subFolder = inboxFolder.Folders["MySubFolderName"]; //folder.Folders[1]; also works
        Console.WriteLine("Folder Name: {0}, EntryId: {1}", subFolder.Name, subFolder.EntryID);
        Console.WriteLine("Num Items: {0}", subFolder.Items.Count.ToString());

        for(int i=1;i<=subFolder.Items.Count;i++)
        {
          item = (Microsoft.Office.Interop.Outlook.PostItem)subFolder.Items[i];
          "Item: {0}", i.ToString() + " " + item.Subject + " " + item.UnRead
          Console.WriteLine("Categories: {0}", item.Categories);
          Console.WriteLine("Body: {0}", item.Body);
          Console.WriteLine("HTMLBody: {0}", item.HTMLBody); 
        }
      } 
      catch (System.Runtime.InteropServices.COMException ex) 
      {
        Console.WriteLine(ex.ToString());
      }
      finally
      {
        ns = null;
        app = null;
        inboxFolder = null;
      }
    }
    
    
    public void Send(string from, string to, string cc, string bcc, string subject, string body, string attachments)
    {
      try
      {
        
      }
      catch (Exception error)
      {
      }
    }

  }
}
