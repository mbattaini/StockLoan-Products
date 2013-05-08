using System;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using StockLoan.Common;


namespace StockLoan.Inventory
{
    public class FTPClient
    {
        #region Members
        private string strDBHost = "";
        private string strDBCatalog = "";
        private string strDBConn = "";
        private string strDBSelect = "";

        private Hashtable htSelectFieldIndex;
        private SqlConnection sqlConnLocates;
        private SqlDataAdapter sqlAdptrLocates;
        private SqlCommand sqlCmdFTPSiteSelect;
        private DataSet sqlDataFTPSites;

        private bool bHasFTPSiteData = false;

        #endregion


        //---------------------------------------------------------------------
        #region Public Methods

        public bool HasFTPSiteData
        {
            get { return bHasFTPSiteData; }
        }
        public DataTable FTPSiteData
        {
            get { return sqlDataFTPSites.Tables[0]; }
        }
        #endregion


        //---------------------------------------------------------------------

        #region Private Methods

        public FTPClient()
        {
            try
            {
                htSelectFieldIndex = new Hashtable();

                //Get Config Elements
                strDBHost = StockLoan.Common.Standard.ConfigValue("MainDatabaseHost");
                strDBCatalog = StockLoan.Common.Standard.ConfigValue("MainDatabaseName");

                string strSQLFields = StockLoan.Common.Standard.ConfigValue("InventoryFTPSitesSelectFields");
                string strSQLFrom = StockLoan.Common.Standard.ConfigValue("InventoryFTPSitesSelectFrom");
                string strSQLWhere = StockLoan.Common.Standard.ConfigValue("InventoryFTPSitesSelectWhere");

                // Build SQL Connection
                strDBConn =
                  "Trusted_Connection=yes; " +
                  "Data Source=" + strDBHost + "; " +
                  "Initial Catalog=" + strDBCatalog + ";";

                strDBSelect = "Select " + strSQLFields + " From " + strSQLFrom + " Where " + strSQLWhere + ";";


                sqlConnLocates = new SqlConnection(strDBConn);
                sqlAdptrLocates = new SqlDataAdapter();
                sqlAdptrLocates.TableMappings.Add(strSQLFrom, "FTPSites");
                sqlAdptrLocates.ContinueUpdateOnError = true;
                sqlConnLocates.Open();

                sqlCmdFTPSiteSelect = new SqlCommand(strDBSelect, sqlConnLocates);
                sqlCmdFTPSiteSelect.CommandType = CommandType.Text;

                sqlAdptrLocates.SelectCommand = sqlCmdFTPSiteSelect;
                SqlCommandBuilder Builder = new SqlCommandBuilder(sqlAdptrLocates);

                if (ConnectionState.Open == sqlConnLocates.State)
                {
                    Console.WriteLine("The connection to FTP Subscriber Database is Now Open.");

                    sqlDataFTPSites = new DataSet("FTPSites");

                    sqlAdptrLocates.Fill(sqlDataFTPSites);
                    if (0 < sqlDataFTPSites.Tables[0].Rows.Count) { bHasFTPSiteData = true; }
                }
            }
            catch (SqlException sqlExcptn)
            {
                foreach (SqlError sqlError in sqlExcptn.Errors)
                {
                    string strSQLErrorNum = sqlError.Number.ToString();
                    Log.Write(strSQLErrorNum + "; " + sqlError.Message + " [Master.Master]", Log.Error, 1);
                    Console.WriteLine(strSQLErrorNum + "; " + sqlError.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [Master.Master]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }


        }

        public void GetFTPInventory()
        { }

        public void GetFTPFileData()
        {
            try
            {


                string strLocalDir = Standard.ConfigValue("InventoryFTPLocalDestination");
                DirectoryInfo DestinationDirInfo = new DirectoryInfo(strLocalDir);
                if (!DestinationDirInfo.Exists) { DestinationDirInfo.Create(); }

                if (HasFTPSiteData)
                {
                    for (int iRow = 0; iRow < FTPSiteData.Rows.Count; iRow++)
                    {
                        DataRow FTPRow = FTPSiteData.Rows[iRow];


                        //"Desk, FileHost, FilePathName, FileUserName, FilePassword"
                        string strFTPServer = FTPRow["FileHost"].ToString();
                        string strFTPRemoteFilePath = FTPRow["FilePathName"].ToString();
                        string strFTPUserName = FTPRow["FileUserName"].ToString();
                        string strFTPPassword = FTPRow["FilePassword"].ToString();
                        bool bIsBizDatePrior = (bool)FTPRow["IsBizDatePrior"];

                        // Build Remote File Name Including Date
                        strFTPRemoteFilePath = ParseFTPFileDate(strFTPRemoteFilePath, bIsBizDatePrior);

                        FileInfo RemoteFileInfo = new FileInfo(strFTPRemoteFilePath);

                        // Build Local File Path
                        string strLocalFilePath = strLocalDir + RemoteFileInfo.Name;

                        StockLoan.Common.Filer FTPFileHandler = new Filer(strLocalDir);

                        try
                        {

                            FTPFileHandler.FileGet(strFTPRemoteFilePath, strFTPServer, strFTPUserName, strFTPPassword, strLocalFilePath);

                            // Check for Local File
                            FileInfo RecievedFileInfo = new FileInfo(strLocalFilePath);
                            if (RecievedFileInfo.Exists && 1 < RecievedFileInfo.Length)
                            {
                                FTPRow["FileStatus"] = "OK: " + strLocalFilePath + ", " + RecievedFileInfo.Length + " bytes";
                            }
                            else
                            {
                                FTPRow["FileStatus"] = FTPFileHandler.Response; 
                            }
                            //response.Close();
                        }
                        catch (System.Net.WebException ex)
                        {
                            Log.Write(ex.Message + " [Master.Master]", Log.Error, 1);
                            Console.WriteLine(ex.ToString());
                            FTPRow["FileStatus"] = FTPFileHandler.Response; ;
                        }
                        catch (Exception ex)
                        {
                            Log.Write(ex.Message + " [Master.Master]", Log.Error, 1);
                            Console.WriteLine(ex.ToString());
                            FTPRow["FileStatus"] = FTPFileHandler.Response; ;
                        }

                    }

                    sqlAdptrLocates.Update(sqlDataFTPSites);

                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [Master.Master]", Log.Error, 1);
                Console.WriteLine(ex.ToString());
            }
        }




        public string ParseFTPFileDate(string RemoteFilePath, bool IsBizDatePrior)
        {
            string strFTPRemoteFilePath = RemoteFilePath;
            if (RemoteFilePath.Contains("{") && RemoteFilePath.Contains("}"))
            {
                int nDateStartLocation = 1 + RemoteFilePath.IndexOf("{");
                int nDateEndLocation = RemoteFilePath.IndexOf("}");
                if (nDateStartLocation < nDateEndLocation)
                {
                    int nDateFormatLength = nDateEndLocation - nDateStartLocation;
                    string strDateFormat = RemoteFilePath.Substring(nDateStartLocation, nDateFormatLength);

                    DateTime dtToday = DateTime.Today;
                    if (IsBizDatePrior) { dtToday = dtToday.AddDays(-1); }
                    string strToday = dtToday.ToString(strDateFormat);
                    strFTPRemoteFilePath = RemoteFilePath.Replace("{" + strDateFormat + "}", strToday);
                }
            }
            return strFTPRemoteFilePath;


        }

        #endregion

    }
}
