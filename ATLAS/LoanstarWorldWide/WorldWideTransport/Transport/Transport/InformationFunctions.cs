using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace StockLoan.Transport
{
    public enum FileStatus
    {
        Aborted,
        Failed,
        OK
    }

    public struct FileResponse
    {
        public string fileName;
        public string fileContents;
        public string lastWriteTime;
        public string comment;
        public bool isExist;
        public FileStatus status;
    }

    public class InformationFunctions
    {
        public static NetworkCredential Credentials(string userName, string password)
        {
            if (!userName.Equals("") && !password.Equals(""))
            {
                return new NetworkCredential(userName, password);
            }
            else
            {
                return null;
            }
        }
    }
}
