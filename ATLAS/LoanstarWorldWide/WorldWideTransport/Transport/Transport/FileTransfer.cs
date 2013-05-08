using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using StockLoan.Common;

namespace StockLoan.Transport
{
    public class FileTransfer
    {
        // sftp:// - Supported
        // ftp:// - Supported
        // localHost - working on it
        //

        private string dbCnStr;
        private string tempDirectory;

        public enum ServerType 
        {
         localHost,
         ftp,
         sftp
        }
        
        public FileTransfer(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
        }

        private bool UseSSL(string remotePathName)
        {
            if (remotePathName.ToLower().Contains("sftp://"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidHost(string uri)
        {
            if (uri.ToLower().Contains("sftp://") ||
                uri.ToLower().Contains("ftp://") ||
                uri.ToLower().Contains("\\"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ServerType RemotePathType(string remotePath)
        {
            if (remotePath.Contains("sftp://"))
            {
                return ServerType.sftp;
            }
            else if (remotePath.Contains("ftp://"))
            {
                return ServerType.ftp;
            }
            else
            {
                return ServerType.localHost;
            }
        }


        public FileResponse FileTime(string remotePath, string userName, string password)
        {
            FileResponse fileResponse = new FileResponse();

            try
            {
                fileResponse.fileName = remotePath;
                
                if (!RemotePathType(remotePath).Equals(ServerType.localHost))
                {
                    FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(remotePath);
                    ftpWebRequest.Credentials = InformationFunctions.Credentials(userName, password);
                    ftpWebRequest.EnableSsl = UseSSL(remotePath);
                    ftpWebRequest.Proxy = null;
                    ftpWebRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                    FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

                    fileResponse.isExist = true;
                    fileResponse.lastWriteTime = ftpWebResponse.LastModified.ToString(Standard.DateTimeFileFormat);

                }
                else
                {
                    fileResponse.isExist = true;
                    fileResponse.lastWriteTime = File.GetLastWriteTime(remotePath).ToString(Standard.DateTimeFileFormat);
                }

                fileResponse.status = FileStatus.OK;
            }
            catch (Exception error)
            {
                fileResponse.status = FileStatus.Failed;
                fileResponse.comment = error.Message;
            }

            return fileResponse;
        }

        public FileResponse FileExists(string remotePath, string userName, string password)
        {
            FileResponse fileResponse = new FileResponse();

            try
            {
                fileResponse.fileName = remotePath;

                if (!RemotePathType(remotePath).Equals(ServerType.localHost))
                {
                    FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(remotePath);
                    ftpWebRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                    ftpWebRequest.Credentials = InformationFunctions.Credentials(userName, password);
                    ftpWebRequest.Proxy = null;

                    FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                    
                    fileResponse.isExist = true;                    
                }
                else
                {
                    fileResponse.isExist = File.Exists(remotePath);             
                }

                fileResponse.status = FileStatus.OK;
            }
            catch (Exception error)
            {
                fileResponse.status = FileStatus.Failed;
                fileResponse.comment = error.Message;
            }

            return fileResponse;
        }

        public FileResponse FileContentsGet(string remotePath, string userName, string password)
        {
            FileResponse fileResponse = new FileResponse();

            try
            {
                Log.Write("Attempting File Contents Get from " + remotePath, 1);

                fileResponse.fileName = remotePath;
                
                if (!RemotePathType(remotePath).Equals(ServerType.localHost))
                {

                    FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(remotePath);

                    ftpWebRequest.Credentials = InformationFunctions.Credentials(userName, password);

                    ftpWebRequest.EnableSsl = UseSSL(remotePath);
                    ftpWebRequest.Proxy = null;
                    ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

                    FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                    Stream responseStream = ftpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);
                    
                    fileResponse.fileContents = streamReader.ReadToEnd();                  

                    streamReader.Close();

                    Log.Write("File Contents Get status code " + ftpWebResponse.StatusCode.ToString(), 1);
                    ftpWebResponse.Close();
                }
                else
                {
                    fileResponse.fileContents = File.ReadAllText(remotePath);
                }

                fileResponse.isExist = true;
                fileResponse.status = FileStatus.OK;
            }
            catch (Exception error)
            {
                fileResponse.status = FileStatus.Failed;
                fileResponse.comment = error.Message;
            }

            return fileResponse;
        }

        public FileResponse FileGet(string remotePath, string userName, string password, string localPath, bool deleteLocal)
        {
            FileResponse fileResponse = new FileResponse();

            try
            {
                Log.Write("Attempting File Get from " + remotePath + " to " + localPath, 1);
                
                if (!RemotePathType(remotePath).Equals(ServerType.localHost))
                {
                    FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(remotePath);

                    ftpWebRequest.Credentials = InformationFunctions.Credentials(userName, password);

                    ftpWebRequest.EnableSsl = UseSSL(remotePath);
                    ftpWebRequest.Proxy = null;
                    ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

                    FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                    Stream responseStream = ftpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);

                    if (File.Exists(localPath) && deleteLocal)
                    {
                        File.Delete(localPath);
                    }

                    string fileName = localPath + ".temp";

                    StreamWriter streamWriter = new StreamWriter(fileName);
                    streamWriter.Write(streamReader.ReadToEnd());

                    streamReader.Close();
                    streamWriter.Close();

                    Log.Write("File Get status code " + ftpWebResponse.StatusCode.ToString(), 1);
                    ftpWebResponse.Close();

                    File.Move(fileName, localPath);
                    File.Delete(fileName);
                }
                else
                {
                    if (File.Exists(localPath) && deleteLocal)
                    {
                        File.Delete(localPath);
                    }

                    File.Copy(remotePath, localPath);
                }

                fileResponse.fileName = localPath;
                fileResponse.isExist = true;
                fileResponse.status = FileStatus.OK;                
            }
            catch (Exception error)
            {

                fileResponse.status = FileStatus.Failed;
                fileResponse.comment = error.Message;                
            }

            return fileResponse;
        }

        public FileResponse FilePut(string remotePath, string userName, string password, string localPath)
        {
            FileResponse fileResponse = new FileResponse();

            try
            {
                Log.Write("Attempting File Put from " + localPath + " to " + remotePath, 1);

                if (!RemotePathType(remotePath).Equals(ServerType.localHost))
                {
                    FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(remotePath);
                    ftpWebRequest.Credentials = InformationFunctions.Credentials(userName, password);
                    ftpWebRequest.EnableSsl = UseSSL(remotePath);
                    ftpWebRequest.Proxy = null;
                    ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;


                    StreamReader streamReader = new StreamReader(localPath);
                    byte[] fileContents = Encoding.UTF8.GetBytes(streamReader.ReadToEnd());
                    streamReader.Close();

                    ftpWebRequest.ContentLength = fileContents.Length;

                    Stream requestStream = ftpWebRequest.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();


                    FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

                    Log.Write("File Put status code " + ftpWebResponse.StatusCode.ToString(), 1);
                    ftpWebResponse.Close();
                }
                else
                {
                    File.Copy(localPath, remotePath);
                }

                fileResponse.fileName = remotePath;
                fileResponse.isExist = true;
                fileResponse.status = FileStatus.OK;                
            }
            catch (Exception error)
            {
                fileResponse.status = FileStatus.Failed;
                fileResponse.comment = error.Message;
            }

            return fileResponse;
        }

    }
}
