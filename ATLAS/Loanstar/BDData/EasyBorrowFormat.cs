using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using DatabaseFunctions;

using StockLoan.Common;

namespace StockLoan.BDData
{
    public class EasyBorrowFormat
    {
         private string dbCnStr;
         private string bizDate;
         private string ftpHost;
         private string ftpFilePath;
         private string ftpUserId;
         private string ftpPassword;
         private string correspondentId;

        public EasyBorrowFormat(string bizDate, string ftpHost, string ftpFilePath, string ftpUserId, string ftpPassword, string correspondentId, string dbCnStr)
        {
            this.bizDate = bizDate;
            this.ftpHost = ftpHost;
            this.ftpPassword = ftpPassword;
            this.ftpUserId = ftpUserId;
            this.ftpFilePath = ftpFilePath;
            this.correspondentId = correspondentId;
            this.dbCnStr = dbCnStr;
        }

        public void EasyBorrowBroadRidgeFileList()
        {
            DataSet dsEasyBorrow = new DataSet();

            try
            {
                dsEasyBorrow = SenderoDatabaseFunctions.BorrowEasyListGet(bizDate, dbCnStr);

                string fileName = StockLoan.BDData.BDDataMain.TempPath + EasyBorrowFileName(correspondentId, bizDate);

                StreamWriter streamWriter = null;

                try
                {
                    streamWriter =  new StreamWriter(fileName);
                    streamWriter.WriteLine(EasyBorrowBodyCreate(dsEasyBorrow, correspondentId, bizDate));
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [EasyBorrowBroadRidgeFileDo]", 1);
                    throw;
                }
                finally
                {
                    streamWriter.Close();
                }

                Filer filer = new Filer();
                filer.FilePut("/" + EasyBorrowFileName(correspondentId, bizDate), ftpHost, ftpUserId, ftpPassword, fileName);

                StockLoan.BDData.Email.Send(KeyValue.Get("BroadRigdeETBEmailTo", "mbattaini@penson.com;bhall@penson.com;", dbCnStr),
                                    KeyValue.Get("BroadRigdeETBEmailFrom", "stockloan@penson.com", dbCnStr),
                                    KeyValue.Get("BroadRigdeETBEmailSubject", "Broad Ridge ETB File Upload", dbCnStr),
                                    "Uploaded etb list with " + dsEasyBorrow.Tables["EasyBorrowList"].Rows.Count.ToString("#,##0") + "items",
                                    dbCnStr);

            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [EasyBorrowBroadRidgeFileList]", 1);
                throw;
            }
        }


        public string EasyBorrowFileName(string correspondentId, string bizDate)
        {
            return "eb_" + correspondentId + "_" + DateTime.Now.ToString("MMddyy") + ".dat";
        }

        public  string EasyBorrowBodyCreate(DataSet dsEasyBorrowList, string strCorrespondentId, string bizDate)
        {
            string body = "";

            try
            {                			
                int rowCount = 0;


                body = "DATE=" + DateTime.Now.ToString("MMddyyhhmmss") + DateTime.Parse(bizDate).ToString("MMddyy") + "01AVL" + strCorrespondentId + new string(' ', 48) + "\r\n";
              
                foreach (DataRow drItem in dsEasyBorrowList.Tables["EasyBorrowList"].Rows)
                {
                    body += drItem["SecId"].ToString().Trim().PadRight(16) + drItem["Quantity"].ToString().Trim().PadLeft(10) + drItem["Symbol"].ToString().Trim().PadLeft(16) + new string(' ', 38) + "\r\n";
                    rowCount++;
                }
              
                body += "REC-CNT=" + rowCount.ToString().Trim().PadLeft(11, '0') + DateTime.Now.ToString("MMddyyhhmmss") + new string(' ', 49) + "\r\n";
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [EasyBorrowBroadRidgeFileDo]", 1);
                throw;
            }

            return body;
        }
    }
}



