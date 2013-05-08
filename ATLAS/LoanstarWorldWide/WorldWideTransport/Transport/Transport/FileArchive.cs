using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using StockLoan.Transport;
using StockLoan.Common;

namespace StockLoan.Transport
{
    public class FileArchive
    {
        public static FileResponse Get(
            string bookGroup, 
            string desk, 
            string lastLoadTime, 
            string remoteFileHost, 
            string remoteFilePath, 
            string userId, 
            string password, 
            string localFileHost, 
            string localFilePath, 
            int lastVersion, 
            string dbCnStr)
        {
            FileResponse fileReponse = new FileResponse();
            string archiveFileName = "";

            FileTransfer fileTransfer = new FileTransfer(dbCnStr);

            try
            {
                fileReponse = fileTransfer.FileTime(remoteFileHost + remoteFilePath, userId, password); 

                if (DateTime.Parse(lastLoadTime) < DateTime.Parse( fileReponse.lastWriteTime))
                {
                    lastVersion++;


                    localFileHost = ArchiveDirectory(bookGroup, desk, DateTime.Parse( fileReponse.lastWriteTime).ToString("yyyy-MM-dd"), localFileHost);
                    
                    if (localFileHost.Equals(""))
                    {
                        throw new Exception("Unable to create archive directory");
                    }

                    archiveFileName = localFileHost + localFilePath + "." + lastVersion;
                    fileTransfer.FileGet(remoteFileHost + remoteFilePath, userId, password, archiveFileName, true);  

                    fileReponse.fileName = archiveFileName;
                    fileReponse.status = FileStatus.OK;

                    Log.Write("Downloaded file " + remoteFileHost + remoteFilePath + " to " + archiveFileName + "; version " + lastVersion.ToString(), 1);
                }
                else
                {

                    fileReponse.fileName = archiveFileName;
                    fileReponse.status = FileStatus.Aborted;
                    fileReponse.comment = "File already downloaded for : " + bookGroup + " : " + desk;

                    Log.Write("Download failed, no new file", 1);
                }
            }
            catch (Exception error)
            {
                fileReponse.fileName = error.Message;
                fileReponse.status = FileStatus.Failed;
            }

            return fileReponse;
        }

        private static string ArchiveDirectory(string bookGroup, string desk, string bizDate, string archivePath)
        {
            string archiveDirectory;

            bizDate = bizDate.Replace("-","");
            bizDate = bizDate.Replace(@"/","");

            archiveDirectory = archivePath + @"\" + bookGroup + @"\" + bizDate + @"\" + desk + @"\";
            Directory.CreateDirectory(archiveDirectory);

            return archiveDirectory;
        }
    }
}
