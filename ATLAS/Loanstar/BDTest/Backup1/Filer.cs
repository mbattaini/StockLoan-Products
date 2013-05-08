using System;
using System.IO;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Common
{
  /// <summary>
  /// Tool for implementing certain file handling and functions.
  /// </summary>
  public class Filer
	{
    const string TEMP_EXTENSION = ".filer";

    private string tempFile = "";

    private Rebex.Net.Ftp ftp;

    public Filer() : this("") {}
    public Filer(string tempFile)
    {
      if (!tempFile.Equals("")) // This instance can be expected to use a temporary file.
      {
        if (tempFile.EndsWith(@"\") || tempFile.EndsWith("/")) // Only a directory has been provided.
        {
          if (!Directory.Exists(tempFile)) // The directory specified does not exist.
          {
            throw new Exception("The directory '" + tempFile + "' does not exist.");
          }
          else // Add a generic file name for this instance.
          {
            lock(this) // To defend agains two instances getting the same file name assignment.
            {
              this.tempFile = tempFile + DateTime.UtcNow.ToString("HHmmssff") + TEMP_EXTENSION;
              Thread.Sleep(25);
            }          
          }
        }
        else // We have an explicit file to use.
        {
          if (Directory.Exists(FilePath(tempFile)))  // The directory specified exists.
          {
            this.tempFile = tempFile;
          }
          else // The directory specified does not exist.
          {
            throw new Exception("The directory for '" + tempFile + "' does not exist.");
          }
        }
      }

      ftp = new Rebex.Net.Ftp();

      if (Log.Level.CompareTo("4") >= 0)
      {
        ftp.CommandSent += new Rebex.Net.FtpCommandSentEventHandler(CommandSent);
        ftp.ResponseRead += new Rebex.Net.FtpResponseReadEventHandler(ResponseRead);
        ftp.StateChanged += new Rebex.Net.FtpStateChangedEventHandler(StateChanged);
        ftp.TransferProgress += new Rebex.Net.FtpTransferProgressEventHandler(TransferProgress);
      }
    }

    ~Filer()
    {
      if (ftp != null)
      {
        ftp.Dispose();
      }
      
      if (!tempFile.Equals(""))
      {
        if (Directory.Exists(FilePath(tempFile)))
        {
          if (File.Exists(tempFile) && tempFile.EndsWith(TEMP_EXTENSION))
          {
            Log.Write("Deleting temporary file: " + tempFile + " [~Filer.Filer]", 3);
            File.Delete(tempFile);
          }
        }
      }
    }

    /// <summary>
    /// Gets the file path for the temporary file stream that may be used by this instance.
    /// </summary>
    public string TempFile
    {
      get
      {
        return tempFile;
      }
    }
        
    /// <summary>
    /// Gets or sets the timeout property.
    /// </summary>
    public int Timeout
    {
      set
      {
        if (value < 10000)
        {
          value = 10000;
        }

        ftp.Timeout = value;
      }

      get
      {
        return ftp.Timeout;
      }
    }
    
    /// <summary>
    /// Returns the file time known on host named for the file at remote path.
    /// </summary>
    public string FileTime(string remotePathName, string hostName, string userName, string password)
    {
      if (hostName.ToLower().Trim().Equals("localhost")) // LocalHost so do not use FTP.
      {
        return File.GetLastWriteTime(remotePathName).ToString(Standard.DateTimeFileFormat);
      }

      string remoteDirectory = FilePath(remotePathName);
      string remoteFileName = FileName(remotePathName);

      Rebex.Net.FtpList list;
      Rebex.Net.FtpItem item;

      try
      {
        DoConnect(hostName, userName, password);
        ftp.ChangeDirectory (remoteDirectory);
        list = ftp.GetList();
        
        for (int i=0; i < list.Count; i++)
        {
          item = list[i];
          if (remoteFileName.Equals(item.Name))
          {
            return item.Modified.ToString(Standard.DateTimeFileFormat);
          }
        }
				
        return "";
      }
      catch
      {
        throw;
      }
      finally
      {
        if (!ftp.State.Equals(Rebex.Net.FtpState.Disconnected))
        {
          DoDisconnect();
        }
      }
    }

		public string[] DirectoryListGet (string remotePathName, string hostName, string userName, string password)
		{
			string [] fileList;

			string remoteDirectory = FilePath(remotePathName);

			if (hostName.ToLower().Trim().Equals("localhost")) // LocalHost so do not use FTP.
			{
				return Directory.GetFiles(FilePath(remoteDirectory));
			}					

			try
			{
				DoConnect(hostName, userName, password);
				ftp.ChangeDirectory (remoteDirectory);
				fileList = ftp.GetNameList();							
			}
			catch
			{
				throw;
			}
			finally
			{
				DoDisconnect();
			}

			return fileList;
		}

		public bool FileExists(string remotePathName, string hostName, string userName, string password)
    {
			if (hostName.ToLower().Trim().Equals("localhost")) // LocalHost so do not use FTP.
			{
				return File.Exists(remotePathName);
			}

      string remoteDirectory = FilePath(remotePathName);
      string remoteFileName = FileName(remotePathName);

      Rebex.Net.FtpList list;
      Rebex.Net.FtpItem item;

      try
      {
        DoConnect(hostName, userName, password);
        ftp.ChangeDirectory (remoteDirectory);
        list = ftp.GetList();
        
        for (int i=0; i < list.Count; i++)
        {
          item = list[i];
          if (remoteFileName.Equals(item.Name))
          {
            return true;
          }
        }

        return false;
      }
      catch
      {
        throw;
      }
      finally
      {
        DoDisconnect();
      }
    }

    /// <summary>
    /// Returns a local file stream for the file at remote path on host named.
    /// </summary>
    public FileStream StreamGet(string remotePathName, string hostName, string userName, string password)
    {
      FileStream fileStream = null;

      if (hostName.ToLower().Trim().Equals("localhost")) // LocalHost so do not use FTP. 
      {
        try
        {
          fileStream = new FileStream(remotePathName, FileMode.Open);
          return fileStream;
        }
        catch
        {
          if (fileStream != null)
          {
            fileStream.Close();
          }

          throw;
        }
      }

      if (hostName.ToLower().IndexOf("www.") > -1) // Anticipates HTTP so do not use FTP. 
      {
        try
        {
          HttpFileGet(hostName + remotePathName);
          fileStream = new FileStream(tempFile, FileMode.Open);
          return fileStream;
        }
        catch
        {
          if (fileStream != null)
          {
            fileStream.Close();
          }

          throw;
        }
      }

      try
      {
        DoConnect(hostName, userName, password);
        ftp.SetTransferType(Rebex.Net.FtpTransferType.Binary);
        fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.ReadWrite);
        ftp.GetFile(remotePathName, fileStream);
        return fileStream;
      }
      catch
      {
        if (fileStream != null)
        {
          fileStream.Close();
        }

        throw;
      }
      finally
      {
        DoDisconnect();
      }
    }

    /// <summary>
    /// Transfers a file from remote path on host named to local path on local host.
    /// </summary>
    public void FileGet(string remotePathName, string hostName, string userName, string password, string localPathName)
    {
      if (hostName.ToLower().Trim().Equals("localhost")) // LocalHost so do not use FTP.
      {
        File.Copy(remotePathName, localPathName, true);
      }
      else
      {
        try
        {
          DoConnect(hostName, userName, password);
          ftp.SetTransferType(Rebex.Net.FtpTransferType.Binary);
          ftp.GetFile(remotePathName, localPathName);
        }
        catch
        {
          throw;
        }
        finally
        {
          DoDisconnect();
        }
      }
    }

    /// <summary>
    /// Transfers a file to remote path on host named from the temporary stream path on local host.
    /// </summary>
    public void StreamPut(string remotePathName, string hostName, string userName, string password)
    {
      FilePut(remotePathName, hostName, userName, password, tempFile);
    }

    /// <summary>
    /// Transfers a file to remote path on host named from local path on local host.
    /// </summary>
    public void FilePut(string remotePathName, string hostName, string userName, string password, string localPathName)
    {
      if (hostName.ToLower().Trim().Equals("localhost")) // LocalHost so do not use FTP.
      {
        File.Copy(localPathName, remotePathName + TEMP_EXTENSION, true);
        File.Delete(remotePathName);
        File.Move(remotePathName + TEMP_EXTENSION,  remotePathName);
      }
      else
      {
        try
        {
          DoConnect(hostName, userName, password);
          ftp.SetTransferType(Rebex.Net.FtpTransferType.Binary);
          ftp.PutFile(localPathName, remotePathName + TEMP_EXTENSION);
					ftp.Proxy.Host = "wmpen01.penson.com:8080";
					ftp.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
					
          try
          {
            ftp.DeleteFile(remotePathName);
          }
          catch {} // We don't care if this fails.

          ftp.Rename(remotePathName + TEMP_EXTENSION, remotePathName);
        }
        catch
        {
          throw;
        }
        finally
        {
          DoDisconnect();
        }
      }
    }

    /// <summary>
    /// Deletes the file at remote path on host named.
    /// </summary>
    public void FileKill(string remotePathName, string hostName, string userName, string password)
    {
      if (hostName.ToLower().Trim().Equals("localhost")) // LocalHost so do not use FTP.
      {
        File.Delete(remotePathName);
        return;
      }

      try
      {
        DoConnect(hostName, userName, password);
        ftp.DeleteFile(remotePathName);
      }
      catch (Exception e)
      {
        if (e.Message.IndexOf("The system cannot find the file specified").Equals(-1))
        {
          throw;
        }
      }
      finally
      {
        DoDisconnect();
      }
    }

    public void Dispose()
    {
      if (ftp != null)
      {
        ftp.Dispose();
      }
      
      if (!tempFile.Equals(""))
      {
        if (Directory.Exists(FilePath(tempFile)))
        {
          if (File.Exists(tempFile))
          {            
            Log.Write("Deleting temporary file stream: " + tempFile + " [~Filer.Filer]", 2);
            File.Delete(tempFile);
          }
        }
      }
    }
    
    private void DoConnect(string hostName, string userName, string password)
    {

      // Code to use an FTP proxy server would go here.


      ftp.Connect(hostName);
      ftp.Login(userName, password);
    }
    
    private void DoDisconnect()
    {
      if (!ftp.State.Equals(Rebex.Net.FtpState.Disconnected))
      {
          ftp.Disconnect();
      }
    }
    
    private string FilePath(string pathName)
    {
      int i;

      if ((i = pathName.LastIndexOf(@"\")) == -1)
      {
        i = pathName.LastIndexOf("/");
      }

      if (i > -1)
      {
        return pathName.Substring(0, i + 1);
      }
      else
      {
        return "";
      }
    }

    private string FileName(string pathName)
    {
      int i;

      if ((i = pathName.LastIndexOf(@"\")) == -1)
      {
        i = pathName.LastIndexOf("/");
      }
      
      if ((i > -1) && ((i + 1) < pathName.Length ))
      {
        return pathName.Substring(i + 1, pathName.Length - i - 1);
      }
      else
      {
        return "";
      }
    }

    private void CommandSent (object sender, Rebex.Net.FtpCommandSentEventArgs e)
    {
      Log.Write("FTP CommandSent: " + e.Command, 4);
    }

    private void ResponseRead (object sender, Rebex.Net.FtpResponseReadEventArgs e)
    {
      Log.Write("FTP ResponseRead: " + e.Response, 4);
    }

    private void StateChanged (object sender, Rebex.Net.FtpStateChangedEventArgs e)
    {
      Log.Write("FTP StateChanged from " + e.OldState + " to " + e.NewState + ".", 4);
    }

    private void TransferProgress (object sender, Rebex.Net.FtpTransferProgressEventArgs e)
    {
      Log.Write("FTP TransferProgress: " + e.BytesTransfered + ".", 4);
    }

    private void HttpFileGet(string url)
    {
      string webProxy = Standard.ConfigValue("HttpWebProxy");

      Stream stream = null;
      StreamReader streamReader = null;
      StreamWriter streamWriter = null;

      Log.Write("Attempting an HTTP GET from " + url + " using web proxy "
        + Tools.ZeroLengthNull(webProxy) + ". [Filer.HttpFileGet]", 3);

      try
      {
        System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://" + url);
        
        if (!webProxy.Equals(""))
        {
          httpWebRequest.Proxy = new System.Net.WebProxy(webProxy, true);
          httpWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        }
         
        httpWebRequest.KeepAlive = false; // Need only one request/reply.

        System.Net.HttpWebResponse httpWebResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();

        Log.Write("HTTP status code: " + httpWebResponse.StatusCode + " [Filer.HttpFileGet]", 3);

        if (httpWebResponse.StatusCode.ToString().ToUpper().Equals("OK"))
        {
          stream = httpWebResponse.GetResponseStream();
          streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
          streamWriter = new StreamWriter(tempFile, false, System.Text.Encoding.ASCII);
          streamWriter.Write(streamReader.ReadToEnd());
          streamWriter.Flush();
        }
        else
        {
          throw (new Exception(httpWebResponse.StatusDescription));
        }
      }
      catch
      {
        throw;
      }
      finally
      {
        if (stream != null)
        {
          stream.Close();
        }

        if (streamReader != null)
        {
          streamReader.Close();
        }

        if (streamWriter != null)
        {
          streamWriter.Close();
        }
      }
    }
  }
}
