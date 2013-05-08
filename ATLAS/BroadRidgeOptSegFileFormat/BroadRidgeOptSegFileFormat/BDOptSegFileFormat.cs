using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BroadRidgeOptSegFileFormat
{
    public class BDOptSegFileFormat
    {
        public static string Format(string bizDate, DataSet dsContracts, string bookGroupList)
        {
            
            string file = "";

            string header = "010DATE=" + DateTime.Parse(bizDate).ToString("yyMMdd") + "\r\n";

            string body = "";

            string trailer = "";


            DataSet dsRates = new DataSet();
            
            dsRates.Tables.Add("Rates");
            dsRates.Tables["Rates"].Columns.Add("ID");
            dsRates.Tables["Rates"].Columns.Add("BpsID", typeof(string));
            dsRates.Tables["Rates"].Columns.Add("Rate", typeof(decimal));
            dsRates.Tables["Rates"].AcceptChanges();

            bool isFound = false;

            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                if (bookGroupList.Contains(dr["BookGroup"].ToString()) & decimal.Parse(dr["Rate"].ToString()) < 0)
                {
                    isFound = false;

                    foreach (DataRow dr2 in dsRates.Tables["Rates"].Rows)
                    {
                        if (dr["BpsID"].ToString().Equals(dr2["BpsID"].ToString()))
                        {
                            isFound = true;

                            decimal rate1, rate2;

                            rate1 = decimal.Parse(dr["Rate"].ToString());
                            rate2 = decimal.Parse(dr2["Rate"].ToString());

                            if (rate1 < rate2)
                            {
                                dr2["Rate"] = dr["Rate"];
                            }

                        }
                    }

                    if (!isFound)
                    {
                        DataRow newRow = dsRates.Tables["Rates"].NewRow();
                        newRow["BpsID"] = dr["BpsID"].ToString();
                        newRow["Rate"] = dr["Rate"].ToString();

                        dsRates.Tables["Rates"].Rows.Add(newRow);
                    }
                }
            }

            int count = dsRates.Tables["Rates"].Rows.Count;
            trailer = "010REC-CNT=" + count.ToString("0000000000") + "\r\n";
            foreach (DataRow dr in dsRates.Tables["Rates"].Select("", "Rate ASC"))
            {

                file += "010-" + dr["BpsID"].ToString() + "-" + count.ToString("0000000") + "\r\n";
                count--;
            }



            return header + file + trailer;
        }
    }
}
