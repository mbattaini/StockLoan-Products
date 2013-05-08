// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC  2005  All rights reserved.

using System;
using System.IO;
using Anetics.Common;

namespace Anetics.Services
{
  /// <summary>
  /// Wrapper class for all Web services.
  /// </summary>
  public class WebService
  {
    private static AuthHeader authHeader;

    private string uri;
    private string desk;
    private string webProxy;
 
    public WebService(string userCode, string passCode, string uri)
    {
      authHeader = new AuthHeader();
      
      authHeader.UserCode = userCode;
      authHeader.PassCode = passCode;
      
      this.desk = desk;
      this.uri = uri;

      webProxy = Standard.ConfigValue("HttpWebProxy");
    }

    public long FaxSend(string number, string name, string subject, string content, string receipt)
    {
      try
      {
        Fax fax = new Fax(authHeader, uri);
        
        if (!webProxy.Equals(""))
        {
          fax.Proxy = new System.Net.WebProxy(webProxy, true);
          fax.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
      
        return fax.Send(number, name, subject, content, receipt);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FaxSend]", Log.Error, 1);
        throw;
      }
    }

    public string FaxStatusGet(long faxId)
    {
      try
      {
        Fax fax = new Fax(authHeader, uri);
      
        if (!webProxy.Equals(""))
        {
          fax.Proxy = new System.Net.WebProxy(webProxy, true);
          fax.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        return fax.StatusGet(faxId);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FaxStatusGet]", Log.Error, 1);
        throw;
      }
    }

    public void FaxCancel(long faxId)
    {
      try
      {
        Fax fax = new Fax(authHeader, uri);
        
        if (!webProxy.Equals(""))
        {
          fax.Proxy = new System.Net.WebProxy(webProxy, true);
          fax.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        fax.Cancel(faxId);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FaxCancel]", Log.Error, 1);
        throw;
      }
    }

    public FirmItem[] FirmsGet()
    {
      try
      {
        Firms firms = new Firms(authHeader, uri);
        
        if (!webProxy.Equals(""))
        {
          firms.Proxy = new System.Net.WebProxy(webProxy, true);
          firms.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        return firms.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FirmsGet]", Log.Error, 1);
        throw;
      }
    }

    public DeskTypeItem[] DeskTypesGet()
    {
      try
      {
        DeskTypes deskTypes = new DeskTypes(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          deskTypes.Proxy = new System.Net.WebProxy(webProxy, true);
          deskTypes.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        return deskTypes.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.DeskTypesGet]", Log.Error, 1);
        throw;
      }
    }

    public DeskItem[] DesksGet()
    {
      try
      {
        Desks desks = new Desks(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          desks.Proxy = new System.Net.WebProxy(webProxy, true);
          desks.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        return desks.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.DesksGet]", Log.Error, 1);
        throw;
      }
    }

    public CurrencyItem[] CurrenciesGet()
    {
      try
      {
        Currencies currencies = new Currencies(authHeader, uri);
        
        if (!webProxy.Equals(""))
        {
          currencies.Proxy = new System.Net.WebProxy(webProxy, true);
          currencies.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
      
        return currencies.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.CurrenciesGet]", Log.Error, 1);
        throw;
      }
    }

    public CountryItem[] CountriesGet()
    {
      try
      {
        Countries countries = new Countries(authHeader, uri);
        
        if (!webProxy.Equals(""))
        {
          countries.Proxy = new System.Net.WebProxy(webProxy, true);
          countries.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
      
        return countries.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.CountriesGet]", Log.Error, 1);
        throw;
      }
    }

    public Holiday[] HolidaysGet()
    {
      try
      {
        Holidays holidays = new Holidays(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          holidays.Proxy = new System.Net.WebProxy(webProxy, true);
          holidays.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
      
        return holidays.Get();
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.HolidaysGet]", Log.Error, 1);
        throw;
      }
    }

    public EmailHeader[] EmailHeadersGet(string host, string userId, string password)
    {
      try
      {
        Email email = new Email(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        return email.HeadersGet(host, userId, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.EmailHeadersGet]", Log.Error, 1);
        throw;
      }
    }

    public void EmailMessageSend(string host, string userId, string password, string to, string from, string subject, string content)
    {
      try
      {
        Email email = new Email(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        email.Send(host, userId, password, to, from, subject, content);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.EmailMessageSend]", Log.Error, 1);
        throw;
      }
    }

    public string EmailMessageGet(string host, string userId, string password, int index)
    {
      try
      {
        Email email = new Email(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        return email.Get(host, userId, password, index);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.EmailMessageGet]", Log.Error, 1);
        throw;
      }
    }

    public void EmailMessageDelete(string host, string userId, string password, int messageIndex)
    {
      try
      {
        Email email = new Email(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        email.Delete(host, userId, password, messageIndex);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.EmailMessageDelete]", Log.Error, 1);
        throw;
      }
    }

    public void EmailMessagePurge(string host, string userId, string password, int count)
    {
      try
      {
        Email email = new Email(authHeader, uri);

        if (!webProxy.Equals(""))
        {
          email.Proxy = new System.Net.WebProxy(webProxy, true);
          email.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        email.Purge(host, userId, password, count);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.EmailMessagePurge]", Log.Error, 1);
        throw;
      }
    }

    public void FileGet(string path, string host, string userId, string password, string localPathName)
    {
      StreamWriter streamWriter = null;
				
      try
      {
        Filer filer = new Filer(authHeader, uri);

        if(!webProxy.Equals(""))
        {
          filer.Proxy = new System.Net.WebProxy(webProxy, true);
          filer.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        string fileContent = filer.Get(path, host, userId, password);

        streamWriter = new StreamWriter(localPathName, false, System.Text.Encoding.UTF8, fileContent.Length);
        streamWriter.Write(fileContent, 0, fileContent.Length);        
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FileGet]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (streamWriter != null)
        {
          streamWriter.Close();
        }
      }
    }

    public void FilePut(string path, string host, string userId, string password, string localPath)
    {
      StreamReader streamReader = null;

      try
      {
        Filer filer = new Filer(authHeader, uri);

        if(!webProxy.Equals(""))
        {
          filer.Proxy = new System.Net.WebProxy(webProxy, true);
          filer.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
        
        streamReader = new StreamReader(localPath);

        string content =  streamReader.ReadToEnd();

        filer.Put(path, host, userId, password, content);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FilePut]", Log.Error, 1);
        throw;
      }
      finally
      {
        if (streamReader != null)
        {
          streamReader.Close();
        }
      }
    }

    public void FileKill(string path, string host, string userId, string password)
    {
      try
      {
        Filer filer = new Filer(authHeader, uri);

        if(!webProxy.Equals(""))
        {
          filer.Proxy = new System.Net.WebProxy(webProxy, true);
          filer.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        filer.Kill(path, host, userId, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FileKill]", Log.Error, 1);
        throw;
      }
    }

    public string FileTime(string path, string host, string userId, string password)
    {
      try
      {
        Filer filer = new Filer(authHeader, uri);

        if(!webProxy.Equals(""))
        {
          filer.Proxy = new System.Net.WebProxy(webProxy, true);
          filer.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        return filer.TimeGet(path, host, userId, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FileTime]", Log.Error, 1);
        throw;
      }
    }

    public bool FileExists(string path, string host, string userId, string password)
    {
      try
      {
        Filer filer = new Filer(authHeader, uri);

        if(!webProxy.Equals(""))
        {
          filer.Proxy = new System.Net.WebProxy(webProxy, true);
          filer.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        return filer.Exists(path, host, userId, password);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [WebService.FileExists]", Log.Error, 1);
        throw;
      }
    }
  }

  /// <summary>
  /// Authorization class for all Web services.
  /// </summary>
  [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://stockloan.net/")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace="http://stockloan.net/", IsNullable=false)]
  public class AuthHeader : System.Web.Services.Protocols.SoapHeader 
  {
    public string UserCode;
    public string PassCode;
  }
}
