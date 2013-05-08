using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;


namespace BroadRidge.BusinessFiles
{
    class TransferParser
    {
        private string filePath;
        private string bizDate;
        private string bizDatePrior;

        public TransferParser(string filePath, string bizDate, string bizDatePrior)
        {
            this.filePath = filePath;
            this.bizDate = bizDate;
            this.bizDatePrior = bizDatePrior;
        }

        public bool CheckFileHeaderDate()
        {
            bool successful = false;

            string line;
       
            try
            {
                if (File.GetLastWriteTime(filePath) > DateTime.Parse(bizDatePrior))
                {
                    successful = true;
                }
            }
            catch (Exception error)
            {
                successful = false;
            }           

            return successful;
        }

        public DataSet LoadTransferRecords()
        {
            string  cusip;
            long quantity;
            string code;

            DataSet dsTransfer = new DataSet();

            dsTransfer.Tables.Add("Transfer");
            dsTransfer.Tables["Transfer"].Columns.Add("Cusip");
            dsTransfer.Tables["Transfer"].Columns.Add("Quantity");            
            dsTransfer.AcceptChanges();

            string[] fileContents = File.ReadAllLines(filePath);
            ArrayList stringArray = new ArrayList(fileContents);

            for (int index = 0; index < stringArray.Count; index++)
            {
                if (stringArray[index].ToString().Trim().Equals("*** BOOKS CLOSING TOMORROW FOR NEXT SECURITY"))
                {
                    stringArray.RemoveAt(index);
                }
            }

            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }

            for (int index = 0; index < stringArray.Count; index += 2)
            {
                if (stringArray[index + 1].ToString().Length > 14)
                {
                    cusip = stringArray[index + 1].ToString().Substring(42, 9);
                    quantity = long.Parse(stringArray[index].ToString().Substring(22, 15).Replace(",", ""));
                    code = stringArray[index].ToString().Substring(16, 5).Trim();

                    if (!cusip.Trim().Equals("") && code.Equals("TACAT"))
                    {
                        DataRow dr = dsTransfer.Tables["Transfer"].NewRow();
                        dr["Cusip"] = cusip;
                        dr["Quantity"] = quantity;
                        dsTransfer.Tables["Transfer"].Rows.Add(dr);
                        dsTransfer.AcceptChanges();
                    }
                }
            }

            return dsTransfer;
        }
    }
}
