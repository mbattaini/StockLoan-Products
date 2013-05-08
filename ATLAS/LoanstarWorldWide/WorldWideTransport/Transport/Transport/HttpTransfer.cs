using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using StockLoan.Common;

namespace StockLoan.Transport
{
    public class HttpTransfer
    {
        private string dbCnStr;
        private string tempDirectory;

        public enum ServerType
        {
            localHost,
            http,
            https
        }

        public HttpTransfer()
        {
        }


        private bool ValidHost(string uri)
        {
            if (uri.ToLower().Contains("http://") ||
                uri.ToLower().Contains("https://"))
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
            if (remotePath.Contains("https://"))
            {
                return ServerType.https;
            }
            else if (remotePath.Contains("http://"))
            {
                return ServerType.http;
            }
            else
            {
                return ServerType.localHost;
            }
        }


        public FileResponse FileGet(string remotePath, string query,string userName, string password, string localPath, bool deleteLocal)
        {
            FileResponse fileResponse = new FileResponse();

            try
            {
                Log.Write("Attempting File Get from " + remotePath + " to " + localPath, 1);

                if (!RemotePathType(remotePath).Equals(ServerType.localHost))
                {
                    CookieContainer cc = new CookieContainer();
                    
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remotePath + query);
                    httpRequest.Timeout = 50000;
                    httpRequest.Credentials = InformationFunctions.Credentials(userName, password);
                    httpRequest.Proxy = null;
                    httpRequest.CookieContainer = cc;
                    httpRequest.Method = WebRequestMethods.Http.Get;

                    string fileName = localPath + ".temp";

                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);

                    while (!streamReader.EndOfStream)
                    {
                        Console.WriteLine(streamReader.ReadLine());
                    }
                    
                    if (File.Exists(localPath) && deleteLocal)
                    {
                        File.Delete(localPath);
                    }                  

                    StreamWriter streamWriter = new StreamWriter(fileName);
                    streamWriter.Write(streamReader.ReadToEnd());

                    streamReader.Close();
                    streamWriter.Close();                 

                    Log.Write("File Get status code " + httpWebResponse.StatusCode.ToString(), 1);
                    httpWebResponse.Close();

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

    }
}
