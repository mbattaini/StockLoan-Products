using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.TradingData
{
    class PirumContractCompare
    {
        private DataSet dsContracts;
        private DataSet dsReturns;
        private string bizDate = "";

        public PirumContractCompare(string bizDate, DataSet dsContracts, DataSet dsReturns)
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

            header = DateTime.Parse(bizDate).ToString("yyyyMMdd") + "\t" + (dsContracts.Tables["Contracts"].Rows.Count + dsReturns.Tables["Returns"].Rows.Count).ToString().PadLeft(15, '0') + "\r\n";

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


            #region Contracts

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
                            if (Standard.ConfigValue("OpenTradesTranslation[" + index + "]").Equals("Y"))
                            {
                                row += dsContracts.Tables["Contracts"].Rows[count][Standard.ConfigValue("OpenTradesDestination[" + index + "]", "")].ToString();
                            }
                            else
                            {
                                row += Standard.ConfigValue("OpenTradesTranslationValue[" + index + "]_" + dsContracts.Tables["Contracts"].Rows[count][Standard.ConfigValue("OpenTradesDestination[" + index + "]", "")].ToString().Trim());
                            }
                        }
                        catch
                        {
                            row += Standard.ConfigValue("OpenTradesDestination[" + index + "]", "");
                        }
                    }

                    fileBody += row + "\r\n";
                }

            #endregion


            #region Returns
            for (int count = 0; count < dsReturns.Tables["Returns"].Rows.Count; count++)
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
                        if (Standard.ConfigValue("OpenReturnsTranslation[" + index + "]").Equals("Y"))
                        {
                            row += dsReturns.Tables["Returns"].Rows[count][Standard.ConfigValue("OpenReturnsDestination[" + index + "]", "")].ToString();
                        }
                        else
                        {
                            row += Standard.ConfigValue("OpenReturnsTranslationValue[" + index + "]_" + dsReturns.Tables["Returns"].Rows[count][Standard.ConfigValue("OpenReturnsDestination[" + index + "]", "")].ToString().Trim());
                        }
                    }
                    catch
                    {
                        row += Standard.ConfigValue("OpenReturnsDestination[" + index + "]", "");
                    }
                }

                fileBody += row + "\r\n";
            }

            #endregion


            return header + fileHeader + fileBody;
        }
    }
}
