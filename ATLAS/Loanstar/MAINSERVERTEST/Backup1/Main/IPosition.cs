using System;
using System.Data;

namespace StockLoan.Main
{
	public delegate void SystemNotificationEventHandler(SystemNotificationEventArgs e);

	public enum SystemNotificationType
	{
		Recall,
		Return,
		Deal,
		Contract,
		Mark,
		Shutdown,
		Message
	}

	public interface IPosition
	{
		DataSet CurrenciesGet(string bizDate);
		DataSet FundsGet();
		DataSet FundingRatesGet(string bizDate);

		DataSet ContractDetailsInfo(string bizDate, string bookGroup);

		void FundingRateSet(
		  string fund,
		  string fundingRate,
		  string actUserId);

		DataSet BoxPositionGet(string bizDate, string bookGroup, string secId);

		DataRow DealItemGet(string bizDate, string dealId, short utcOffset);
		DataSet DealDataGet(short utcOffset, string bizDate, string isActive);

		void DealSet(
			string dealId,
			string dealStatus,
			string actUserId,
			bool isActive
			);

		void DealSet(
			string dealId,
			string bookGroup,
			string dealType,
			string book,
			string bookContact,
			string contractId,
			string secId,
			string quantity,
			string amount,
			string collateralCode,
			string valueDate,
			string settleDate,
			string termDate,
			string rate,
			string rateCode,
			string poolCode,
			string divRate,
			string divCallable,
			string incomeTracked,
			string margin,
			string marginCode,
			string currencyIso,
			string securityDepot,
			string cashDepot,
			string comment,
			string mark,
			string dealStatus,
			string actUserId,
			bool isActive
			);

		DataSet ContractDataGet(short utcOffset);
		DataSet ContractDataGet(short utcOffset, string bizDate, string userId, string functionPath);
		DataSet ContractDataGet(short utcOffset, string bookGroup, string contractId);

		DataSet ContractSummaryGet(string bizDate);

		void ContractSet(
			string bizDate,
			string bookGroup,
			string contractId,
			string contractType,
			string book,
			string secId,
			string quantity,
			string quantitySettled,
			string amount,
			string amountSettled,
			string collateralCode,
			string valueDate,
			string settleDate,
			string termDate,
			string rate,
			string rateCode,
			string statusFlag,
			string poolCode,
			string divRate,
			string divCallable,
			string incomeTracked,
			string marginCode,
			string margin,
			string currencyIso,
			string securityDepot,
			string cashDepot,
			string otherBook,
			string comment,
			bool isActive);

		DataSet ReturnsGet(string returnId, string bizDate, string bookGroup, string contractId, short utcOffset);
		string ReturnSet(string returnId, string bookGroup, string book, string contractId, string contractType, string quantity, string settleDateProjected, string settleDateActual, string actUserId, bool isActive);

		DataSet RecallsGet(string recallId, string bizDate, string bookGroup, short utcOffset);
		string RecallSet(string recallId, string bookGroup, string contractId, string contractType, string book, string secId, string quantity, string openDateTime, string moveToDate, string buyInDate, string reasonCode, string sequenceNumber, string comment, string status, string actUserId, bool isActive);

		DataSet ContractBillingGet(string bizDate);

		DataSet MarksGet(string markId, string bizDate, string bookGroup, string contractId, short utcOffset);
		string MarkSet(string markId, string bookGroup, string book, string contractId, string contractType, string secId, string amount, string openDate, string settleDate, string deliveryCode, string actUserId, bool isActive);

		DataSet DeliveryTypesGet();
		DataSet MarkContractsGet(string bizDate);
		DataSet MarkSummaryGet(string bizDate);

		DataSet ContractResearchDataGet(string bizDate, string bookGroup, string book, string contractId, string secId, string amount, string logicId);

		DataSet LogicOperatorsGet();

		string ContractAsOfSet(
			string bookGroup,
			string contractId,
			string contractType,
			string book,
			string secId,
			string quantity,
			string quantitySettled,
			string amount,
			string amountSettled,
			string collateralCode,
			string valueDate,
			string settleDate,
			string termDate,
			string rate,
			string rateCode,
			string statusFlag,
			string poolCode,
			string divRate,
			string divCallable,
			string incomeTracked,
			string marginCode,
			string margin,
			string currencyIso,
			string securityDepot,
			string cashDepot,
			string otherBook,
			string comment,
			bool isActive);

		string RateChangeAsOfSet(string startDate, string endDate, string bookGroup, string book, string contractId, string oldRate, string newRate, string actUserId);

		string MarkAsOfSet(
		string tradeDate,
		string settleDate,
		string bookGroup,
		string book,
		string contractId,
		string contractType,
		string price,
		string markId,
		string deliveryCode,
		string actUserId);

		string ReturnAsOfSet(
		string tradeDate,
		string settleDate,
		string bookGroup,
		string book,
		string contractId,
		string contractType,
		string quantity,
		string returnId,
		string actUserId);

		string Report_ContractByCounterParty(string bizDate, string bookGroup);
		string Report_ContractBySecurity(string bizDate, string bookGroup);
	}


	[Serializable]
	public class SystemNotificationEventArgs : EventArgs
	{
		private string processId;
		private string bookGroup;
		private SystemNotificationType systemType;

		public SystemNotificationEventArgs(
			string processId,
			string bookGroup,
			SystemNotificationType systemType
			)
		{
			this.processId = processId;
			this.bookGroup = bookGroup;
			this.systemType = systemType;
		}

		public string ProcessId
		{
			get
			{
				return processId;
			}
		}

		public string BookGroup
		{
			get
			{
				return bookGroup;
			}
		}

		public SystemNotificationType SystemType
		{
			get
			{
				return systemType;
			}
		}
	}

	public class SystemNotificationEventWrapper : MarshalByRefObject
	{
		public event SystemNotificationEventHandler SystemNotificationEvent;

		public void DoEvent(SystemNotificationEventArgs e)
		{
			SystemNotificationEvent(e);
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
