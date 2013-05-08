using System;
using System.Data;

namespace StockLoan.Locates
{
    public interface ITrade
    {
      DataSet SecMasterItemGet(string secId);
    }
}
