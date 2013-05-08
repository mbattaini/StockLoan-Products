using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace PirumWindowsFormsApplication
{
    class PirumContractCompare_old
    {
        private DataSet dsContracts;
        private DataSet dsReturns;
        private string bizDate = "";

        public PirumContractCompare_old(string bizDate, DataSet dsContracts, DataSet dsReturns)
        {
            this.dsContracts = dsContracts;
            this.dsReturns = dsReturns;
            this.bizDate = bizDate;
        }

        public string Parse()
        {
            string header = "";
            string fileHeader = "";
            string fileBody = "";
            string row = "";

            int itemCount = int.Parse(Standard.ConfigValue("SourceDestinationHeaders", "0"));

            header = DateTime.Parse(bizDate).ToString("yyyyMMdd") + "\t" + dsContracts.Tables["Contracts"].Rows.Count.ToString().PadLeft(15, '0') + "\r\n";
            
            for (int index = 0; index < itemCount; index++)
            {
                if (!fileHeader.Equals(""))
                {
                    fileHeader += "\t";
                }

                fileHeader += Standard.ConfigValue("OpenTradesSource[" + index + "]", "");
            }

            fileHeader += "\r\n";
            fileBody = "";

            for (int count = 0; count < dsContracts.Tables["Contracts"].Rows.Count; count++)
            {
                row = "";

                for (int index = 0; index < itemCount; index++)
                {
                    if (!row.Equals(""))
                    {
                        row += "\t";
                    }

                    try
                    {
                        row += dsContracts.Tables["Contracts"].Rows[count][Standard.ConfigValue("OpenTradesDestination[" + index + "]", "")];
                    }
                    catch
                    {
                        row += Standard.ConfigValue("OpenTradesDestination[" + index + "]", "");
                    }
                }

                fileBody += row + "\r\n";
            }



            return header + fileHeader + fileBody;
        }
    }
}
