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
    public class CheckListCreation
    {
        private string fileName;
        private string dbCnStr;
        private string currentDate;
        private string bookGroup;

        public CheckListCreation(string currentDate, string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.currentDate = currentDate;
        }

        public string Load()
        {
            DataSet dsCheckList = new DataSet();
            string message = "";

            dsCheckList = DatabaseFunctions.CheckListGet(currentDate, dbCnStr);

            foreach (DataRow item in dsCheckList.Tables[0].Rows)
            {
                message += item["Item"].ToString().PadRight(50);

                DateTime temp;

                if (DateTime.TryParse(item["DateRun"].ToString(), out temp))
                {
                    message += temp.ToString("yyyy-MM-dd").PadRight(20);
                }

                if (bool.Parse(item["IsError"].ToString()))
                {
                    message += Standard.ConfigValue(item["Item"].ToString(), "").PadRight(50);
                }
                else
                {
                    message += item["Notice"].ToString().PadRight(50);
                }

                message += "\n";
            }

            bool isReady = true;

            foreach (DataRow item in dsCheckList.Tables[0].Rows)
            {
                if (bool.Parse(item["IsError"].ToString()) && bool.Parse(item["IsCritical"].ToString()))
                {
                    isReady = false;
                }
            }

            if (isReady)
            {
                message += "\n\n\n Both 0234 and 0158 are ready for trading!";
            }
            else
            {
                message += "\n\n\n Both 0234 and 0158 are not ready for trading!";
            }

            return message;
        }
    }
}
