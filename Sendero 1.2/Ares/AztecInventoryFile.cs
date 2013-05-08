using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Anetics.Common;

namespace Anetics.Ares
{
    class AztecInventoryFile
    {
        private string dbCnstr;

        public AztecInventoryFile(string dbCnstr)
        {
            this.dbCnstr = dbCnstr;
        }

        public void Load()
        {
            string date;
            string bookGroup;
            string secid;
            string rate;

            string ftpHost;
            string ftpPath;
            string ftpUserName;
            string ftpPassword;

            string parserType;
            string delimiter;
            string ignoreWords;

            int datePosition;
            int bookgroupPosition;
            int secidPosition;
            int ratePosition;

            long itemCount = 0;

            DataTable dataTable = new DataTable("Inventory");
            dataTable.Locale = CultureInfo.InvariantCulture;
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("BookGroup");
            dataTable.Columns.Add("SecId");
            dataTable.Columns.Add("Rate");

            SqlConnection dbCn = dbCn = new SqlConnection(dbCnstr);
            StreamReader streamReader = null;

            try
            {
                ftpHost = Standard.ConfigValue("AztecFtpHost");
                ftpPath = Standard.ConfigValue("AztecFtpFilePath");
                ftpUserName = Standard.ConfigValue("AztecFtpUserName");
                ftpPassword = Standard.ConfigValue("AztecFtpPassword");

                bookGroup = Standard.ConfigValue("AztecBookGroup");
                parserType = Standard.ConfigValue("AztecParserType");
                delimiter = Standard.ConfigValue("AztecDelimiter");
                ignoreWords = Standard.ConfigValue("AztecIgnoreWords");

                datePosition = int.Parse(Standard.ConfigValue("AztecDatePosition"), CultureInfo.CurrentCulture);
                bookgroupPosition = int.Parse(Standard.ConfigValue("AztecBookGroupPosition"), CultureInfo.CurrentCulture);
                secidPosition = int.Parse(Standard.ConfigValue("AztecSecIdPosition"), CultureInfo.CurrentCulture);
                ratePosition = int.Parse(Standard.ConfigValue("AztecRatePosition"), CultureInfo.CurrentCulture);
            }
            catch
            {
                Log.Write("Application is not properly configured [StockLoan.AztecInventoryFile.Load]", Log.Error, 1);
                throw;
            }

            try
            {
                if (!String.IsNullOrEmpty(bookGroup) && !String.IsNullOrEmpty(parserType) && !String.IsNullOrEmpty(delimiter)
                    && !String.IsNullOrEmpty(ftpHost) && !String.IsNullOrEmpty(ftpPath) && !String.IsNullOrEmpty(ftpUserName) && !String.IsNullOrEmpty(ftpPassword))
                {
                    string line;

                    Filer filer = new Filer(Standard.ConfigValue("TempPath", @"C:\"));
                    streamReader = null;

                    streamReader = new StreamReader(filer.StreamGet(ftpPath, ftpHost, ftpUserName, ftpPassword));
                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                    itemCount = 0;

                    while (!streamReader.EndOfStream)
                    {

                        line = streamReader.ReadLine();

                        if (itemCount > 0)
                        {
                            date = Tools.SplitItem(line, delimiter, 0);
                            bookGroup = Tools.SplitItem(line, delimiter, 1);
                            secid = Tools.SplitItem(line, delimiter, 2);
                            rate = Tools.SplitItem(line, delimiter, 4);

                            secid = secid.Replace("\"", "");

                            DataRow dr = dataTable.NewRow();

                            dr["Date"] = date;
                            dr["BookGroup"] = bookGroup;
                            dr["SecId"] = secid;
                            dr["Rate"] = rate;

                            dataTable.Rows.Add(dr);
                        }

                        itemCount++;
                    }
                    dataTable.AcceptChanges();


                    SqlCommand dbCmd = new SqlCommand("spInventoryRateSet", dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Float);

                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = "ADMIN";

                    dbCn.Open();

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (!dr["Rate"].ToString().Trim().Equals(""))
                        {
                            paramSecId.Value = dr["SecId"].ToString();
                            paramRate.Value = dr["Rate"].ToString().Trim();
                            dbCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [StockLoan.AztecInventoryFile.Load]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Open)
                {
                    dbCn.Close();
                }

                streamReader.Close();
            }

            Log.Write("Loaded: " + itemCount.ToString("#,##0", CultureInfo.CurrentCulture) + " items  [StockLoan.AztecInventoryFile.Load]", Log.Information, 1);
        }
    }
}
