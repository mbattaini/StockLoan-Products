using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace StockLoan.TGTradeFormats
{
    public class TradeDataReport
    {

        public static string FormatGet(DataSet dsMessages)
        {
            string xmlElement = null;
            string line = "";
            string header = "";
            string body = "";            

            foreach (DataRow dr in dsMessages.Tables["Messages"].Rows)
            {
                line = "";
                XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(dr["Message"].ToString()));

                while (xmlTextReader.Read())
                {
                    switch (xmlTextReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            xmlElement = xmlTextReader.Name;
                            break;

                        case XmlNodeType.Text:
                            switch (xmlElement)
                            {
                                case "CorrespondentCode":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "SEDOL":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "ISIN":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "Book":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "Counterparty":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "BuySellFlag":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "Quantity":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "Price":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "MarginRate":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "StartDate":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "EndDate":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                                case "TradeReference":
                                    line = line + xmlTextReader.Value + ",";
                                    break;
                            }
                            break;
                    }
                }                
                line = line + "\r\n";
                body = body + line;
            }

            return header + body;
        }
    }
}
