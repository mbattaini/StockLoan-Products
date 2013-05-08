using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using StockLoan.Common;

namespace StockLoan.TGTradeFormats
{
    public class TradeFunctions
    {
        public static string StarsTradeReference(string system)
        {
            string tradeReference = "";

            tradeReference = system.ToUpper() + "_" + DateTime.Now.ToString("MMddyyyyHHmmssfff");

            return tradeReference;
        }

        public static void XsdValidation(string xmlMessage, string xmlPropHeader)
        {
           string temp = Standard.ConfigValue(xmlPropHeader + "XmlDocument");
            
            XmlSchema xmlSchema = XmlSchema.Read(new XmlTextReader(Standard.ConfigValue(xmlPropHeader + "XmlDocument")), null);

            XmlReaderSettings xmlSettings = new XmlReaderSettings();


            switch (Standard.ConfigValue(xmlPropHeader + "ConformanceLevel").ToLower())
            {
                case "auto":
                    xmlSettings.ConformanceLevel = ConformanceLevel.Auto;
                    break;

                default:
                    xmlSettings.ConformanceLevel = ConformanceLevel.Auto;
                    break;
            }


            switch (Standard.ConfigValue(xmlPropHeader + "XmlValidationType").ToLower())
            {
                case "schema":
                    xmlSettings.ValidationType = ValidationType.Schema;
                    break;

                default:
                    xmlSettings.ValidationType = ValidationType.Schema;
                    break;
            }

            xmlSettings.Schemas.Add(xmlSchema);
            xmlSettings.ValidationEventHandler += new ValidationEventHandler(xmlSettings_ValidationEventHandler);


            XmlParserContext xmlContext = new XmlParserContext(null, null, "", XmlSpace.None);
            XmlReader xmlReader = XmlReader.Create(new XmlTextReader(xmlMessage, XmlNodeType.Element, xmlContext), xmlSettings);

            while (xmlReader.Read())
            {
            }
        }

        private static void xmlSettings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            throw new Exception(e.Message);
        }

        public static string StarsTradeReferenceGet(string xmlMessage)
        {
            string messageId = "";            
            string xmlElement = "";

            XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xmlMessage));

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
                            case "TradeReference":
                                messageId = xmlTextReader.Value;
                                break;                            
                        }
                        break;
                }
            }

            return messageId;
        }
    }
}
