using System.Data;

namespace StockLoan.NorthStar
{
  public interface ITrade
  {
    DataSet SecMasterItemGet(string secId);
  }
}
