using System;
using System.Data;

namespace StockLoan.PreBorrow
{
  public interface IPreBorrow
  {
    string BizDate();
    string KeyValueGet(string keyId, string keyValue);
    void KeyValueSet(string keyId, string keyValue);

    DataSet SecMasterLookup(string secId, bool withBox, bool withDeskQuips, short utcOffset, string since);
    DataSet InventoryGet(string secId, short utcOffset, bool withHistory);
    DataSet ContractDataGet(short utcOffset, string bizDate, string userId, string functionPath);
    DataSet TradingGroupsGet(string tradeDate, short utcOffset);

    void PreBorrowRequest(string groupCode, string secId, long requestedQuantity, string contactName, string contactPhoneNumber, string contactEmailAddress, string actUserId);

    void PreBorrowStartOfDaySnapShot();

    DataSet PreBorrowRecordGet(string identity, string startDate, string stopDate, string enterTime, string groupCode, string secId, short utcOffset);
    void PreBorrowRecordSet(
      string identity,
      string bizDate,
      string openTime,
      string groupCode,
      string secId,
      string tradeDateShortQuantity,
      string requestedQuantity,
      string coveredQuantity,
      string coveredAmount,
      string rate,
      string modifiedRate,
      string charge,
      string modifiedCharge,
      string contactName,
      string contactPhoneNumber,
      string contactEmailAddress,
      string actUserId,
      bool isContacted);

    DataSet PreBorrowGroupCodeMarkupGet(string groupCode, short utcOffset);
    void PreBorrowGroupCodeMarkupSet(string groupCode, string markup, string actUserId, bool isActive);

    DataSet PreBorrowContactGet(string groupCode, short utcOffset);
    void PreBorrowContactSet(string groupCode, string firstName, string lastName, string phoneNumber, string emailAddress, string actUserId, bool isActive);

  }     
}
