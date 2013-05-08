// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Data;

namespace Anetics.Medalist
{
  public interface IRebate
  {
		DataSet ShortSaleBillingSummaryGet(
			string startDate, 
			string stopDate, 
			string groupCode, 
			string accountNumber);
		
    
		DataSet ShortSaleBillingSummaryRecordGet(
			string processId,
			string bizDate);
	

		void ShortSaleBillingSummaryRecordSet(
			string processId,
			string bizDate,
			string rate,
			string price,
			string originalCharge,
			string comment,
			string actUserId);



		string ShortSaleBillingSummaryBillGet(
			string startDate, 
			string stopDate, 
			string groupCode);

		string ShortSaleBillingSummaryAccountsBorrowedBillGet(
			string startDate, 
			string stopDate);

		string ShortSaleBillingSummaryMasterBillGet(
			string startDate, 
			string stopDate);
		

		int ShortSaleBillingSummaryDatesLock(
			string startDate, 
			string stopDate, 
			string groupCode, 
			string accountNumber, 
			bool isLocked, 
			string actUserId);	

	
		
		int ShortSaleBillingSummaryMarkSet(
			string startDate, 
			string stopDate, 
			string groupCode, 
			string accountNumber,
			bool overWrite, 
			string actUserId);
	
		

		void ShortSaleBillingSummaryBillingReportGet(
			string	startDate, 
			string	stopDate, 
			string	groupCode,
			ref long		groupCodeCount,
			ref long		secIdCount,
			ref decimal totalCharges);		


		DataSet ShortSaleBillingSummaryAccountsMethodGet (
			string groupCode,
			int utcOffset);

		void ShortSaleBillingSummaryAccountMethodSet (
			string groupCode,
			string accountNumber,
			bool IsOSIBilling,
			bool IsPaperBilling,			
			string actUserId);

		DataSet ShortSaleBillingSummaryTradingGroupEmailGet (
			string groupCode,
			int utcOffset);

		void ShortSaleBillingSummaryTradingGroupEmailSet (
			string groupCode,
			string emailAddress,
			string actUserId);

		DataSet ShortSaleBillingCorrespondentSummaryGet(
			string startDate,
			string stopDate,
			string groupCode);

		void ShortSaleBillingSummaryTradingGroupsSet (
			string groupCode, 			
			string negativeRebateMarkUp,
			string negativeRebateMarkUpCode,
			string negativeRebateBill,
			string actUserId);


		void ShortSaleBillingSummaryTradingGroupsAccountMarkSet(
			string groupCode,
			string accountNumber,
			string negativeRebateMarkUp,
			string negativeRebateMarkUpCode,
			bool delete,
			string actUserId);

	
		void ShortSaleBillingSummaryTradingGroupsOfficeCodeMarkSet(
			string groupCode,
			string officeCode,
			string negativeRebateMarkUp,
			string negativeRebateMarkUpCode,
			string actUserId);

		DataSet ShortSaleBillingSummaryActivityGet (
			string groupCode,
			int utcOffset);

	}
}
