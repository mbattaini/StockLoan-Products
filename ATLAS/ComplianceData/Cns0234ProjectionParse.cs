using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class Cns0234ProjectionParse
    {
        private string fileName;
        private string dbCnStr;
        private string currentDate;
        private string bookGroup;

        public Cns0234ProjectionParse(string fileName, string bookGroup, string currentDate, string dbCnStr)
        {
            this.fileName = fileName;
            this.dbCnStr = dbCnStr;
            this.currentDate = currentDate;
            this.bookGroup = bookGroup;
        }

        public void Load()
        {
            string fileDate = "";
            bool ignore = false;

            string[] ignoreItems = { "PROJECTION", "PROJECTED", "PROCESS DATE", "FOR SETTLEMENT ON:", "PARTICIPANT", "SUB-ACCOUNT", "----- T O M O R R O W ", "SETTLING", "TRADES", "PURCAHSED/", "SOLD(-)", "LONG/" };

            DataSet dsCns = new DataSet();
            dsCns.Tables.Add("Cns");
            dsCns.Tables["Cns"].Columns.Add("SecId");
            dsCns.Tables["Cns"].Columns.Add("Quantity");


            string[] cnsItems = File.ReadAllLines(fileName);

            fileDate = cnsItems[0].Substring(81, 8);

            if (DateTime.Parse(fileDate).ToString("yyyy-MM-dd") != currentDate)
            {
                Log.Write("File is not for today. [StockLoan.ComplianceData.Cns0234ProjectionParse.Load]", 1);
                throw new Exception("File is not for today. [StockLoan.ComplianceData.Cns0234ProjectionParse.Load]");
            }
            else
            {
                Log.Write("File is for today " + Master.BizDate + ". [StockLoan.ComplianceData.Cns0234ProjectionParse.Load]", 1);
            }



            foreach (string cnsItem in cnsItems)
            {
                ignore = false;

                foreach (string ignoreItem in ignoreItems)
                {
                    if ((cnsItem.Contains(ignoreItem)) || cnsItem[0] == '1')
                    {
                        ignore = true;
                    }
                }

                if (!ignore)
                {
                    if (cnsItem.Trim().Equals(""))
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            if (!cnsItem.Substring(46, 15).Trim().Equals(""))
                            {
                                DataRow drTemp = dsCns.Tables["Cns"].NewRow();
                                drTemp["SecId"] = cnsItem.Substring(90, 9);
                                drTemp["Quantity"] = long.Parse(cnsItem.Substring(80, 1) + cnsItem.Substring(46, 15).Replace(",", "").Trim());
                                dsCns.Tables["Cns"].Rows.Add(drTemp);
                                dsCns.AcceptChanges();
                            }
                        }
                        catch (Exception error)
                        {
                            Log.Write(error.Message + " [StockLoan.ComplianceData.Cns0234ProjectionParse.Load]", 1);
                        }
                    }
                }
            }

            foreach (DataRow drRow in dsCns.Tables["Cns"].Rows)
            {
                DatabaseFunctions.CnsProjectionItemSet(Master.BizDate, bookGroup, drRow["SecId"].ToString(), drRow["Quantity"].ToString(), dbCnStr);
            }
        }  
    }
}
