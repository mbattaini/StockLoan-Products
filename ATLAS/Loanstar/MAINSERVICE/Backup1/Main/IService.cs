using System;
using System.Data;

namespace StockLoan.Main
{
    public interface IService
    {
        string BizDate();
        string BizDateNext();
        string BizDatePrior();

        string BizDateBank();
        string BizDateNextBank();
        string BizDatePriorBank();

        string BizDateExchange();
        string BizDateNextExchange();
        string BizDatePriorExchange();

		bool UseSystemSettlementEngine();

        void KeyValueSet(string keyId, string keyValue);
        string KeyValueGet(string keyId,  string keyValueDefault);

        DataSet KeyValueGet();

        DataSet FirmGet();
        DataSet CountriesGet(string countryCode);
        DataSet CurrenciesGet();
        DataSet DeskTypeGet();

		void CountrySet(string countryCode, string country, string settleDays, bool isActive);

        DataSet DeskGet();
        DataSet DeskGet(string desk);
        DataSet DeskGet(bool isNotSubscriber);

        DataSet BookGroupGet();
        DataSet BookGroupGet(string userId, string functionPath);

        DataSet SecMasterSearch(string lookupciteria);

        DataSet SecMasterLookup(string secId);
		DataSet SecMasterGroupGet(string secId);
		DataSet SecMasterGet();

		string ProcessId();
    }
}
