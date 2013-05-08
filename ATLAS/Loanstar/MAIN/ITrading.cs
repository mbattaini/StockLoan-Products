using System;
using System.Collections.Generic;
using System.Data;

namespace StockLoan.Main
{
    public interface ITrading
    {
        DataSet TradingSystemsGet();

        DataSet TradingSystemsCounterPartiesGet(string system);

        void TradeRequest(string bizDate, string bookGroup, string book, string contractId);

        void ReturnRequest(string bizDate, string bookGroup, string book, string contractId);
    }
}
