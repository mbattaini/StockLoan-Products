using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    class Cns0158ProjectionParse
    {
        public DataSet dsCns;
        private string dbCnStr;
        private string bookGroup;
        private int interval;
        private string fileName;
        private string currentDate;

        public Cns0158ProjectionParse(string fileName, string bookGroup, string currentDate, string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.bookGroup = bookGroup;
            this.fileName = fileName;
            this.currentDate = currentDate;
        }

        public void Load()
        {                        
            string sign = "";
            decimal quantity;

            string signTraded = "";
            decimal quantityTraded;

            dsCns = new DataSet();
            dsCns.Tables.Add("Fails");
            dsCns.Tables["Fails"].Columns.Add("SecId");
            dsCns.Tables["Fails"].Columns.Add("ClearingFTDTraded");
            dsCns.AcceptChanges();

            string[] cnsItems = File.ReadAllLines(fileName);

            if (!CheckFileDate(cnsItems[0].Substring(32, 10), currentDate))
            {
                Log.Write("File is not for today. [StockLoan.ComplianceData.Cns0158ProjectionParse.Load]", 1);
                throw new Exception("File is not for today. [StockLoan.ComplianceData.Cns0158ProjectionParse.Load]");
            }
            else
            {
                Log.Write("File is for today " + Master.BizDate + ". [StockLoan.ComplianceData.Cns0158ProjectionParse.Load]", 1);
            }

            foreach (string line in cnsItems)
            {
                
                if (line != null)
                {
                    if (line[0].Equals('D'))
                    {
                        sign = line.Substring(22, 1);
                        quantity = long.Parse(line.Substring(10, 12));

                        signTraded = line.Substring(32, 1);
                        quantityTraded = long.Parse(line.Substring(23, 9));

                      
                        

                        if (signTraded.Equals("-"))
                        {
                            DataRow drTemp = dsCns.Tables["Fails"].NewRow();

                            drTemp["SecId"] = line.Substring(1, 9);
                            drTemp["ClearingFTDTraded"] = "-" + quantityTraded;
                            dsCns.Tables["Fails"].Rows.Add(drTemp);
                        }


                    }
                }
                else
                {
                    break;
                }
            }

            if (dsCns.Tables["Fails"].Rows.Count > 0)
            {
                foreach (DataRow drRow in dsCns.Tables["Fails"].Rows)
                {
                    DatabaseFunctions.CnsProjectionItemSet(Master.BizDate, bookGroup, drRow["SecId"].ToString(), drRow["ClearingFTDTraded"].ToString(), dbCnStr);
                }
            }
        }

        public bool CheckFileDate(string fileDate, string bizDate)
        {
            if (DateTime.ParseExact(fileDate, "MM-dd-yyyy", null).ToString("yyyy-MM-dd").Equals(bizDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }     
    }
}
