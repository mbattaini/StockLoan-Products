using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace Anetics.Ares
{
    class StockBorrowParser
    {
        private string filePath = "";

        public StockBorrowParser(string filePath)
        {
            this.filePath = filePath;
        }

        private bool isCusip(string line)
        {
            bool cusip = false;

            if (line.Contains(' ') || line.Contains('/') || line.Contains('\\'))
            {
                cusip = false;
            }

            if (line.Length.Equals(9))
            {
                cusip = true;
            }

            return cusip;
        }

        public DataSet Parse()
        {
            bool nextLineIsCusip = false;
            bool readInventoryRecord = false;

            string cusip = "";
            string source = "";
            string quantity = "";

            DataSet dsInventoryRecords = new DataSet();
            dsInventoryRecords.Tables.Add("Inventory");
            dsInventoryRecords.Tables["Inventory"].Columns.Add("Source");
            dsInventoryRecords.Tables["Inventory"].Columns.Add("Cusip");
            dsInventoryRecords.Tables["Inventory"].Columns.Add("Quantity");
            dsInventoryRecords.AcceptChanges();

            string[] test = File.ReadAllLines(filePath);

            foreach (string line in test)
            {
                if (line.Equals(""))
                {
                }
                else if (line.Equals("**********"))
                {
                    break;
                }
                else if (line.Equals("------------------------"))
                {
                    //next line is cusip
                    cusip = "";

                    nextLineIsCusip = true;
                    readInventoryRecord = false;
                }
                else if (nextLineIsCusip)
                {
                    nextLineIsCusip = false;
                    readInventoryRecord = true;


                    if (isCusip(line.Substring(0, 9)))
                    {
                        cusip = line.Substring(0, 9);
                    }
                }
                else if (readInventoryRecord)
                {
                    int quantityIndex = line.IndexOf(':');
                    int sourceIndex = line.IndexOf('-');
                    quantity = line.Substring(0, quantityIndex);
                    source = line.Substring(quantityIndex + 1, sourceIndex - quantityIndex - 1);

                    DataRow drNew = dsInventoryRecords.Tables["Inventory"].NewRow();
                    drNew["Cusip"] = cusip;
                    drNew["Source"] = source;
                    drNew["Quantity"] = quantity;

                    dsInventoryRecords.Tables["Inventory"].Rows.Add(drNew);

                    quantity = "";
                    source = "";
                }
            }

            return dsInventoryRecords;
        }
    }
}
