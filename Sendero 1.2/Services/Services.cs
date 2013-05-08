using System;
using System.IO;
using Anetics.Common;

namespace Anetics.Services
{
  /// <summary>
  /// Summary description for Services.
  /// </summary>
  public class Services
  {
    private static AuthHeader authHeader;
    private string host;
    private string desk;
 
    public Services(string desk, string passCode, string host, string path)
    {
      authHeader = new AuthHeader();
      authHeader.Desk = desk;
      authHeader.PassCode = passCode;
      this.desk = desk;
      this.host = host + path;
    }

    public long FaxSend(string faxNumber, string content)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Fax fax = new Fax(authHeader, host);
        if (!webProxy.Equals(""))
        {
          fax.Proxy = new System.Net.WebProxy(webProxy, true);
          fax.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return fax.FaxSend(faxNumber, content);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FaxSend]", 3);
          throw;
      }
    }

    public string FaxStatus(long faxId)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Fax fax = new Fax(authHeader, host);
        if (!webProxy.Equals(""))
        {
          fax.Proxy = new System.Net.WebProxy(webProxy, true);
          fax.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return fax.FaxStatus(faxId);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FaxStatus]", 3);
          throw;
      }
    }

    public void FaxCancel(long faxId)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Fax fax = new Fax(authHeader, host);
        if (!webProxy.Equals(""))
        {
          fax.Proxy = new System.Net.WebProxy(webProxy, true);
          fax.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        fax.FaxCancel(faxId);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FaxCancel]", 3);
          throw;
      }
    }

    public FirmItem [] FirmsGet()
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Firms firms = new Firms(authHeader, host);
        if (!webProxy.Equals(""))
        {
          firms.Proxy = new System.Net.WebProxy(webProxy, true);
          firms.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return firms.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FirmsGet]", 3);
          throw;
      }
    }

    public DeskTypeItem [] DeskTypesGet()
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        DeskTypes deskTypes = new DeskTypes(authHeader, host);
        if (!webProxy.Equals(""))
        {
          deskTypes.Proxy = new System.Net.WebProxy(webProxy, true);
          deskTypes.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return deskTypes.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.DeskTypesGet]", 3);
          throw;
      }
    }

    public CurrencyItem [] CurrenciesGet()
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Currencies currencies = new Currencies(authHeader, host);
        if (!webProxy.Equals(""))
        {
          currencies.Proxy = new System.Net.WebProxy(webProxy, true);
          currencies.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return currencies.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.CurrenciesGet]", 3);
          throw;
      }
    }

    public CountryItem [] CountriesGet()
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Countries countries = new Countries(authHeader, host);
        if (!webProxy.Equals(""))
        {
          countries.Proxy = new System.Net.WebProxy(webProxy, true);
          countries.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return countries.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.CountriesGet]", 3);
          throw;
      }
    }

    public HolidayItem[] HolidaysGet()
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Holidays holidays = new Holidays(authHeader, host);
        if (!webProxy.Equals(""))
        {
          holidays.Proxy = new System.Net.WebProxy(webProxy, true);
          holidays.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return holidays.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.HolidaysGet]", 3);
          throw;
      }
    }

    public EmailHeader[] HeadersGet(string popServer, string userId, string password)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Email email = new Email(authHeader, host);
        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return email.HeadersGet(popServer, userId, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.HeadersGet]", 3);
          throw;
      }
    }

    public void MessageSend(string smtpServer, string userId, string password, string to, string from, string subject, string content)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Email email = new Email(authHeader, host);
        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        email.MessageSend(smtpServer, userId, password, to, from, subject, content);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.MessageSend]", 3);
          throw;
      }
    }

    public string MessageGet(string popServer, string userId, string password, int index)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Email email = new Email(authHeader, host);
        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        return email.MessageGet(popServer, userId, password, index);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.MessageGet]", 3);
          throw;
      }
    }

    public void MessageDelete(string popServer, string userId, string password, int messageIndex, int messageCount)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Email email = new Email(authHeader, host);
        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        email.MessageDelete(popServer, userId, password, messageIndex, messageCount);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.MessageDelete]", 3);
          throw;
      }
    }

    public void MessagePurge(string popServer, string userId, string password, int purgeCount)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
      try
      {
        Email email = new Email(authHeader, host);
        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        email.MessagePurge(popServer, userId, password, purgeCount);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.MessagePurge]", 3);
          throw;
      }
    }

    public void FileGet(string remotePath, string hostName, string userName, string password, string localPath)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");
     
      try
      {
        Ftp ftp = new Ftp(authHeader, host);
        if(!webProxy.Equals(""))
        {
          ftp.Proxy = new System.Net.WebProxy(webProxy, true);
          ftp.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        string stream = ftp.FileGet(remotePath, hostName, userName, password);

        StreamWriter file =  null;
				
        if(File.Exists(localPath))
        {
          File.Delete(localPath);
        }

        file = new StreamWriter(localPath, false, System.Text.Encoding.Unicode, stream.Length);

        file.Write(stream, 0, stream.Length);
        
        file.Close();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FileGet]", 3);
        throw;
      }
    }

    public void FilePut(string remotePath, string hostName, string userName, string password, string localPath)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");

      try
      {
        Ftp ftp = new Ftp(authHeader, host);
        if(!webProxy.Equals(""))
        {
          ftp.Proxy = new System.Net.WebProxy(webProxy, true);
          ftp.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        StreamReader streamReader = new StreamReader(localPath);

        string content =  streamReader.ReadToEnd();

        ftp.FilePut(remotePath, hostName, userName, password, content);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FileGet]", 3);
        throw;
      }
    }

    public void FileKill(string remotePath, string hostName, string userName, string password)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");

      try
      {
        Ftp ftp = new Ftp(authHeader, host);
        if(!webProxy.Equals(""))
        {
          ftp.Proxy = new System.Net.WebProxy(webProxy, true);
          ftp.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        ftp.FileKill(remotePath, hostName, userName, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FileKill]", 3);
        throw;
      }
    }

    public string FileTime(string remotePath, string hostName, string userName, string password)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");

      try
      {
        Ftp ftp = new Ftp(authHeader, host);
        if(!webProxy.Equals(""))
        {
          ftp.Proxy = new System.Net.WebProxy(webProxy, true);
          ftp.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        return ftp.FileTime(remotePath, hostName, userName, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FileTime]", 3);
        throw;
      }
      return null;
    }

    public bool FileExists(string remotePath, string hostName, string userName, string password)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");

      try
      {
        Ftp ftp = new Ftp(authHeader, host);
        if(!webProxy.Equals(""))
        {
          ftp.Proxy = new System.Net.WebProxy(webProxy, true);
          ftp.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        return ftp.FileExists(remotePath, hostName, userName, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [Services.FileTime]", 3);
        throw;
      }
      return false;
    }
  }
}
