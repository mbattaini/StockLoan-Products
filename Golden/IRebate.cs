using System.Data;

namespace StockLoan.Golden
{
	public interface IRebate
	{
        DataSet ShortSaleBillingSummaryGet(
            string startDate, 
            string stopDate, 
            string groupCode, 
            string accountNumber, 
            string platForm);   

        string ShortSaleBillingSummaryMasterBillGet(
            string startDate,
            string stopDate,
            string platForm);

		DataSet ShortSaleBillingSummaryMasterBillExcelGet(			
			string startDate,
			string stopDate,
			string platForm); 

        string ShortSaleBillingSummaryBillGet(
            string startDate,
            string stopDate,
            string groupCode,
            string platForm);

        void ShortSaleBillingSummaryBillingReportGet(
            string startDate,
            string stopDate,
            string groupCode,
            ref long groupCodeCount,
            ref long secIdCount,
            ref decimal totalCharges,
            string platForm);

        DataSet ShortSaleBillingCorrespondentSummaryGet(
            string startDate,
            string stopDate,
            string groupCode,
            string platForm);

        DataSet ShortSaleBillingSummaryCorrespondentBillExcelGet(        
            string startDate,
            string stopDate,
            string groupCode,
            string platForm);

        DataSet ShortSaleBillingSummaryTradingGroupGet(               
            string groupCode,
            string platForm);

        void ShortSaleBillingSummaryTradingGroupSet(               
            string groupCode,
            string negativeRebateMarkUp,
            string negativeRebateBill,
            string userId,
            string platForm,
            int paramEditFlag);

        string KeyValueGet(string keyId, string keyValueDefault);

        void KeyValueSet(string keyId, string keyValueDefault);

    }
}
