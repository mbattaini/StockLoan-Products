using System;
using System.Data;

namespace StockLoan.Locates
{
	public interface IShortSale
	{
		string TradeDate();
		string BizDatePrior(string bizDate);

		DataSet TradingGroupsGet(string tradeDate, short utcOffset);
		void TradingGroupSet(
		  string groupCode,
		  string groupName,
		  string minPrice,
		  string autoApprovalMax,
		  string premiumMin,
		  string premiumMax,
		  bool autoEmail,
		  string emailAddress,
		  string lastEmailDate,
		  string actUserId);

		string LocateListSubmit(string clientId, string groupCode, string clientComment, string list);

        DataSet LocatesGet(string tradeDate, string groupCode, string secId, short utcOffset);
        DataSet LocatesByStatusGet(string tradeDate, string groupCode, string secId, string status, short utcOffset);
		DataSet LocatesGet(string tradeDateMin, string tradeDateMax, string groupCode, string secId, short utcOffset);
		long LocatesCountGet(string tradeDate, string status);
		
		byte[] LocatesCompressedGet(string tradeDateMin, string tradeDateMax, string groupCode, string secId, short utcOffset);
		DataSet LocatesSecIdSummaryGet(string tradeDate);
		DataSet LocatesSummaryGet(string tradeDate);

		DataSet LocateItemGet(string locateId, short utcOffset);
		DataSet LocateItemGet(string groupCode, string locateId, short utcOffset);

		void LocateItemSet(
		  long locateId,
		  string quantity,
		  string source,
		  string feeRate,
		  string preBorrow,
		  string comment,
		  string actUserId);

		DataSet InventoryGet(string secId, short utcOffset);
		DataSet InventoryGet(string secId, short utcOffset, bool withHistory);
		DataSet InventoryGet(string groupCode, string secId, short utcOffset);
		DataSet InventoryHistoryLookupGet(string bizDate, string secId);

		DataTable InventoryDeskListGet(string bizDate, string desk);

		void LocatesBeginEdit(string locateId, string actUserId);
		void LocatesEndEdit(string locateId, string actUserId);
		DataSet LocatesEditing();

		DataSet LocatesActions();

	}
}
