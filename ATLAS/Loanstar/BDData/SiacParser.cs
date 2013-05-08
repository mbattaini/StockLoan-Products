using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;


namespace BroadRidge.BusinessFiles
{
    class SiacParser
    {
        private string filePath;
        private string bizDate;
        private string bizDatePrior;


        public SiacParser(string filePath, string bizDate, string bizDatePrior)
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

            if (File.GetLastWriteTime(filePath) < DateTime.Parse(bizDatePrior))
            {
                return false;
            }

            // read one line

            TextReader textReader = new StreamReader(filePath);

            line = textReader.ReadLine();

            if ((!line.Equals("")) && (line[0] == 'H'))
            {
                fileHeaderDate = line.Substring(32, 10);
            }



            try
            {
                if (DateTime.ParseExact(fileHeaderDate, "MM-dd-yyyy", null).ToString("yyyy-MM-dd").Equals(bizDate))
                {
                    successful = true;
                }
            }
            catch (Exception error)
            {
                successful = false;
            }
            finally
            {
                textReader.Close();
            }

            return successful;
        }

        public DataSet LoadSiacReecords()
        {
            string  cusip;
            long quantity;
            string sign;
              
            DataSet dsSiac = new DataSet();

            dsSiac.Tables.Add("Siac");
            dsSiac.Tables["Siac"].Columns.Add("Cusip");
            dsSiac.Tables["Siac"].Columns.Add("CnsFTD");
            dsSiac.Tables["Siac"].Columns.Add("CnsFTR");
            dsSiac.AcceptChanges();


            string[] fileContents = File.ReadAllLines(filePath);


            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }

            for (int index = 0; index < fileContents.Length; index++)
            {
                if (fileContents[index][0] == 'D')
                {
                    cusip = fileContents[index].Substring(1, 9);
                    quantity = long.Parse(fileContents[index].Substring(10, 12));
                    sign = fileContents[index].Substring(22, 1);

                    DataRow dr = dsSiac.Tables["Siac"].NewRow();
                    dr["Cusip"] = cusip;

                    if (sign.Equals("+"))
                    {
                        dr["CnsFTR"] = quantity;
                        dr["CnsFTD"] = 0;
                    }
                    else
                    {
                        dr["CnsFTD"] = quantity;
                        dr["CnsFTR"] = 0;
                    }

                    dsSiac.Tables["Siac"].Rows.Add(dr);
                    dsSiac.AcceptChanges();
                }
            }

            return dsSiac;
        }
    }
}
