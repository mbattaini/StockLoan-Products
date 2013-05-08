using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace StockLoan.TGTradeFormats
{
    public class StarsStockLoan
    {
        public static string StockLoanTrade(
           string correspondentCode,
           string tradeReference,
           string isin,
           string sedol,
           string exchangeCode,
           string book,
           string counterParty,
           string counterPartyCode,
           string buySellCode,
           string quantity,
           string price,
           string fidessaCode,
           string cancelledFlag,
           string capacity,
           string clientMarketIndicator,
           string dealingCurrency,
           string countryOfSettlement,
           string placeOfSettlement,
           string businessReason,
           string sourceEntity,
           string marginRate,
           string startDate,
           string endDate,
           string fixedInterestRate,
           string collateralised)
        {
            string xmlMessage = "";

            xmlMessage = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlMessage += "<pen:TradeMessages xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:pen=\"http://www.pensonuk.com/2_0/schema/\"><TradeMessage>";
            xmlMessage += "<CorrespondentCode>" + correspondentCode + "</CorrespondentCode>";
            xmlMessage += "<TradeReference>" + tradeReference + "</TradeReference>";

            if (isin.Equals(""))
            {
                xmlMessage += "<SEDOL>" + sedol + "</SEDOL>";
            }
            else
            {
                xmlMessage += "<ISIN>" + isin + "</ISIN>";
            }

            xmlMessage += "<ExchangeCode>" + exchangeCode + "</ExchangeCode>";
            xmlMessage += "<EntryDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</EntryDate>";
            xmlMessage += "<EntryTime>" + DateTime.Now.ToString("HH:mm:ss") + "</EntryTime>";
            xmlMessage += "<TradeDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</TradeDate>";
            xmlMessage += "<TradeTime>" + DateTime.Now.ToString("HH:mm:ss") + "</TradeTime>";
            xmlMessage += "<Book>" + book + "</Book>";
            xmlMessage += "<Counterparty>" + counterParty + "</Counterparty>";
            xmlMessage += "<CounterpartyCodeType>" + counterPartyCode + "</CounterpartyCodeType>";
            xmlMessage += "<BuySellFlag>" + buySellCode + "</BuySellFlag>";
            xmlMessage += "<Quantity>" + quantity + "</Quantity>";
            xmlMessage += "<Price>" + price + "</Price>";
            xmlMessage += "<CancelledFlag>" + cancelledFlag + "</CancelledFlag>";
            xmlMessage += "<Capacity>" + capacity + "</Capacity>";
            xmlMessage += "<ClientMarketIndicator>" + clientMarketIndicator + "</ClientMarketIndicator>";
            if (!placeOfSettlement.Equals(""))
            {
                xmlMessage += "<PlaceOfSettlement>" + placeOfSettlement + "</PlaceOfSettlement>";
            }

            xmlMessage += "<DealingCurrency>" + dealingCurrency + "</DealingCurrency>";
            xmlMessage += "<CountryOfSettlement>" + countryOfSettlement + "</CountryOfSettlement>";
            
           
            xmlMessage += "<BusinessReason>" + businessReason + "</BusinessReason>";
            xmlMessage += "<SourceEntity>" + sourceEntity + "</SourceEntity>";
            xmlMessage += "<MarginRate>" + marginRate + "</MarginRate>";
            xmlMessage += "<StartDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</StartDate>";
            xmlMessage += "<EndDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</EndDate>";
            xmlMessage += "<FixedInterestRate>" + fixedInterestRate + "</FixedInterestRate>";
            xmlMessage += "<Collateralised>" + collateralised + "</Collateralised>";
            xmlMessage += "</TradeMessage></pen:TradeMessages>";

           TradeFunctions.XsdValidation(xmlMessage, "StarsTrade");

            return xmlMessage;
        }

        public static TradeResponse StockLoanTradeResponse(string xmlMessage)
        {
            string xmlElement = "";

            TradeResponse tradeResponse = new TradeResponse();
            
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
                            case "Status":
                                tradeResponse.status = xmlTextReader.Value.ToString();
                                break;

                            case "TradeReference":
                                tradeResponse.tradeNumber = xmlTextReader.Value.ToString();
                                break;

                            case "Reason":
                                tradeResponse.statusDescription = xmlTextReader.Value.ToString();
                                break;
                        }
                        break;
                }
            }

            return tradeResponse;
        }
    }
}
