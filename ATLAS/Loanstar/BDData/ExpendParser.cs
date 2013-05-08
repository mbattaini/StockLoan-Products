using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;


namespace BroadRidge.BusinessFiles
{
    class ExpendParser
    {
        private string filePath;
        private string bizDate;
        private string bizDatePrior;

        public ExpendParser(string filePath, string bizDate, string bizDatePrior)
        {
            this.filePath = filePath;
            this.bizDate = bizDate;
            this.bizDatePrior = bizDatePrior;
        }

        public bool CheckFileHeaderDate()
        {
            bool successful = false;

            string line;
            string fileHeaderDate = "";

            if (File.GetLastWriteTime(filePath) > DateTime.Parse(bizDatePrior))
            {
                successful = true;
            }
            else
            {
                successful = false;
            }

            return successful;
        }

        public DataSet LoadExpendRecords()
        {
            string excessCusip;
            long excessQuantity;
            string excessSign;

            string pdqCusip;
            long pdqQuantity;
            string pdqSign;

            DataSet dsSiac = new DataSet();

            dsSiac.Tables.Add("Expend");
            dsSiac.Tables["Expend"].Columns.Add("Cusip");
            dsSiac.Tables["Expend"].Columns.Add("Pdq");
            dsSiac.Tables["Expend"].Columns.Add("Excess");
            dsSiac.AcceptChanges();


            string[] fileContents = File.ReadAllLines(filePath);


            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }

            for (int index = 0; index < fileContents.Length; index++)
            {
                excessCusip = fileContents[index].Substring(13, 9);
                excessQuantity = long.Parse(fileContents[index].Substring(168, 13));
                excessSign = fileContents[index].Substring(167, 1);

                pdqCusip = fileContents[index].Substring(13, 9);
                pdqQuantity = long.Parse(fileContents[index].Substring(123, 14));
                pdqSign = fileContents[index].Substring(167, 1);

                if (excessSign.Equals("+"))
                {
                    dr["Excess"] = excessQuantity;
                }
                else
                {
                    dr["Excess"] = excessQuantity * -1;
                }


                DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionClearingOvernightSiacInsert(bizDate, "0158", pdqCusip, pdqQuantity, 
                
                DataRow dr = dsSiac.Tables["Expend"].NewRow();
                dr["Cusip"] = excessCusip;

                if (excessSign.Equals("+"))
                {
                    dr["Excess"] = excessQuantity;
                }
                else
                {
                    dr["Excess"] = excessQuantity * -1;
                }

                dsSiac.Tables["Expend"].Rows.Add(dr);
                dsSiac.AcceptChanges();
            }

            return dsSiac;
        }
    }    
}
