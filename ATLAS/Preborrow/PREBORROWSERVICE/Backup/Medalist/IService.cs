// Licensed Materials - Property of StockLoan, LLC.
// (C) Copyright StockLoan, LLC. 2003, 2004  All rights reserved.

using System;
using System.Data;

namespace StockLoan.Medalist
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

        void KeyValueSet(string keyId, string keyIdCategory, string keyValue);
        string KeyValueGet(string keyId, string keyIdCategory, string keyValueDefault);

        DataSet KeyValueGet();

        DataSet FirmGet();
        DataSet CountryGet();
        DataSet DeskTypeGet();

        DataSet DeskGet();
        DataSet DeskGet(string desk);
        DataSet DeskGet(bool isNotSubscriber);

        DataSet BookGroupGet();
        DataSet BookGroupGet(string userId, string functionPath);

        DataSet SecMasterLookup(string secId);
        DataSet SecMasterLookup(string secId, bool withBox);
        DataSet SecMasterLookup(string secId, bool withBox, bool withDeskQuips, short utcOffset, string since);
    }
}
