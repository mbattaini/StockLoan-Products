using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;
using System.Data.Common;


namespace BroadRidge.BusinessFiles
{
    class RdSweepParser
    {
        private string filePath;
        private string bizDate;
        private string bizDatePrior;

        public RdSweepParser(string filePath, string bizDate, string bizDatePrior)
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

            if (!line.Equals(""))
            {
                fileHeaderDate = line.Substring(24, 6);
            }

            try
            {
                if (DateTime.ParseExact(fileHeaderDate, "MMddyy", null).ToString("yyyy-MM-dd").Equals(bizDate))
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

        public DataSet LoadRdSweepRecords()
        {
            string  cusip;
            decimal quantity;
            string sign;
            string spincode;
            DateTime settledate;
            
            DataSet dsRdSweep = new DataSet();

            dsRdSweep.Tables.Add("RdSweep");
            dsRdSweep.Tables["RdSweep"].Columns.Add("Cusip", typeof(string));
            dsRdSweep.Tables["RdSweep"].Columns.Add("COD-S", typeof(decimal));
            dsRdSweep.Tables["RdSweep"].Columns.Add("COD-P", typeof(decimal));
            dsRdSweep.Tables["RdSweep"].Columns.Add("COR-S", typeof(decimal));
            dsRdSweep.Tables["RdSweep"].Columns.Add("COR-P", typeof(decimal));
            dsRdSweep.Tables["RdSweep"].Columns.Add("FTD-S", typeof(decimal));
            dsRdSweep.Tables["RdSweep"].Columns.Add("FTD-P", typeof(decimal));
            dsRdSweep.Tables["RdSweep"].Columns.Add("FTR-S", typeof(decimal));
            dsRdSweep.Tables["RdSweep"].Columns.Add("FTR-P", typeof(decimal));
            dsRdSweep.AcceptChanges();


            dsRdSweep.Tables.Add("RdSweepSummary");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("Cusip");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("COD-S");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("COD-P");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("COR-S");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("COR-P");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("FTD-S");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("FTD-P");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("FTR-S");
            dsRdSweep.Tables["RdSweepSummary"].Columns.Add("FTR-P");
            dsRdSweep.AcceptChanges();

            string[] fileContents = File.ReadAllLines(filePath);


            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }

            for (int index = 0; index < fileContents.Length; index++)
            {
                if (fileContents[index].Substring(0,3).Equals("010"))
                {
                    if (fileContents[index].Substring(116, 2).Trim().Equals("U$"))
                    {
                    cusip = fileContents[index].Substring(13, 9);
                    quantity = decimal.Parse(fileContents[index].Substring(57, 12) + "." + fileContents[index].Substring(69,5));                    
                    spincode = fileContents[index].Substring(3, 1);
                    settledate = DateTime.ParseExact(fileContents[index].Substring(23, 8), "yyyyMMdd", null);

                    DataRow dr = dsRdSweep.Tables["RdSweep"].NewRow();
                    dr["Cusip"] = cusip;                    

                    switch (spincode)
                    {
                        case "1":
                            dr["COD-S"] = 0;
                            dr["COD-P"] = 0;
                            dr["COR-S"] = (settledate <= DateTime.Parse(bizDate)) ? quantity : 0;
                            dr["COR-P"] = (settledate > DateTime.Parse(bizDate)) ? quantity : 0;                                                        
                            dr["FTD-S"] = 0;
                            dr["FTD-P"] = 0;
                            dr["FTR-S"] = 0;
                            dr["FTR-P"] = 0;
                            break;

                        case "3":
                            dr["COD-S"] = 0;
                            dr["COD-P"] = 0;
                            dr["COR-S"] = 0;
                            dr["COR-P"] = 0;
                            dr["FTD-S"] = 0;
                            dr["FTD-P"] = 0;
                            dr["FTR-S"] = (settledate <= DateTime.Parse(bizDate)) ? quantity : 0;
                            dr["FTR-P"] = (settledate > DateTime.Parse(bizDate)) ? quantity : 0;
                            
                            break;

                        case "4":
                            dr["COD-S"] = (settledate <= DateTime.Parse(bizDate)) ? quantity : 0;
                            dr["COD-P"] = (settledate > DateTime.Parse(bizDate)) ? quantity : 0;
                            dr["COR-S"] = 0;
                            dr["COR-P"] = 0;
                            dr["FTD-S"] = 0;
                            dr["FTD-P"] = 0;
                            dr["FTR-S"] = 0;
                            dr["FTR-P"] = 0;
                            
                            break;

                        case "6":
                            dr["COD-S"] = 0;
                            dr["COD-P"] = 0;
                            dr["COR-S"] = 0;
                            dr["COR-P"] = 0;
                            dr["FTD-S"] = (settledate <= DateTime.Parse(bizDate)) ? quantity : 0;
                            dr["FTD-P"] = (settledate > DateTime.Parse(bizDate)) ? quantity : 0;
                            dr["FTR-S"] = 0;
                            dr["FTR-P"] = 0;
                            
                            break;
                    }

                    dsRdSweep.Tables["RdSweep"].Rows.Add(dr);
                    dsRdSweep.AcceptChanges();
                    }
                }
            }

            var query =
                from cusipRow in dsRdSweep.Tables["RdSweep"].AsEnumerable()
                group cusipRow by cusipRow.Field<string>("Cusip") into g
                select new
                {
                    Category = g.Key,
                    COD_S = g.Sum(cusipRow => cusipRow.Field<decimal>("COD-S")),
                    COD_P = g.Sum(cusipRow => cusipRow.Field<decimal>("COD-P")),
                    COR_S = g.Sum(cusipRow => cusipRow.Field<decimal>("COR-S")),
                    COR_P = g.Sum(cusipRow => cusipRow.Field<decimal>("COR-P")),
                    FTD_S = g.Sum(cusipRow => cusipRow.Field<decimal>("FTD-S")),
                    FTD_P = g.Sum(cusipRow => cusipRow.Field<decimal>("FTD-P")),
                    FTR_S = g.Sum(cusipRow => cusipRow.Field<decimal>("FTR-S")),
                    FTR_P = g.Sum(cusipRow => cusipRow.Field<decimal>("FTR-P")),
                };

            foreach (var cusipRow in query)
            {                
                DataRow dr = dsRdSweep.Tables["RdSweepSummary"].NewRow();
                
                dr["Cusip"] = (string)cusipRow.Category;
                dr["COD-S"] = cusipRow.COD_S;
                dr["COD-P"] = cusipRow.COD_P;
                dr["COR-S"] = cusipRow.COR_S;
                dr["COR-P"] = cusipRow.COR_P;
                dr["FTD-S"] = cusipRow.FTD_S;
                dr["FTD-P"] = cusipRow.FTD_P;
                dr["FTR-S"] = cusipRow.FTR_S;
                dr["FTR-P"] = cusipRow.FTR_P;

                dsRdSweep.Tables["RdSweepSummary"].Rows.Add(dr);
                dsRdSweep.AcceptChanges();
            }

            dsRdSweep.Tables.Remove("RdSweep");

            return dsRdSweep;
        }
    }
}
