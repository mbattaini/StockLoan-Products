using System.Data;

namespace StockLoan.NorthStar
{
    public interface IShortInterest
    {
        DataSet ShortInterestGet(
            string mpid, 
            string symbol, 
            string quantityGreaterThan,
            string quantityLessThan,
            string priceLessThan,
            string shortInterestMidMonthGreaterThan,
            string shortInterestMonthEndGreaterThan);

        string ShortInterestDataDateGet();
    }
}
