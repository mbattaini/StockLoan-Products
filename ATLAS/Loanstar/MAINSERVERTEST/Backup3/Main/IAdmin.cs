using System;
using System.Data;

namespace StockLoan.Main
{
	public interface IAdmin
	{
		bool MayView(string userId, string functionPath);
		bool MayEdit(string userId, string functionPath);

		bool MayViewBookGroup(string userId, string functionPath, string bookGroup);
		bool MayEditBookGroup(string userId, string functionPath, string bookGroup);

		DataSet BookGet(string bookGroup, string book);

		bool UserPasswordValidate(string userId, string password);

		void UserSet(
		  string userId,
		  string shortName,
		  string password,
		  string email,
		  string group,
		  string comment,
		  string actUserId,
		  bool isActive);

		void RoleSet(
		  string roleCode,
		  string role,
		  string comment,
		  string actUserId,
		  bool delete);

		void UserRoleSet(
		  string userId,
		  string roleCode,
		  string comment,
		  string actUserId,
		  bool delete);

		void RoleFunctionSet(
		  string roleCode,
		  string functionPath,
		  bool mayView,
		  bool mayEdit,
		  string bookGroupList,
		  string comment,
		  string actUserId);

		DataSet UserRolesGet(int utcOffset);
		DataSet UserRoleFunctionsGet(int utcOffset);

		DataSet HolidaysGet(short utcOffset);
		DataSet HolidaysGet(string bizDate, short utcOffset);

		void HolidaysSet(
		  string date,
		  string countryCode,
		  string description,
		  bool isBankHoliday,
		  bool isExchangeHoliday,
		  bool isBizDateHoliday,
		  string actUserId,
		  bool isActive);

		void BookSet(
			string bookGroup,
			string book,
			string bookParent,
			string bookName,
			string addressLine1,
			string addressLine2,
			string addressLine3,
			string phoneNumber,
			string faxNumber,
			string marginBorrow,
			string marginLoan,
			string markRoundHouse,
			string markRoundInstitution,
			string rateStockBorrow,
			string rateStockLoan,
			string rateBondBorrow,
			string rateBondLoan,
			string countryCode,
			string fundDefault,
			string actUserId,
			bool isActive);



		void BookCreditLimitsSet(
		  string bizdate,
		  string bookGroup,
		  string bookParent,
		  string book,
		  string borrowCreditLimit,
		  string loanCreditLimit,
		  string actor);

		DataSet BookCreditLimitsGet(
		  string bizdate,
		  string bookGroup,
		  string bookParent,
		  string book,
		  short utcOffset);

		DataSet BookContactGet(
		 string bookGroup,
		 string book);

		void BookContactSet(
		  string bookGroup,
		  string book,
		  string firstName,
		  string lastName,
		  string function,
		  string phoneNumber,
		  string faxNumber,
		  string comment,
		  string actor,
		  bool isActive);


		DataSet BookClearingInstructionGet(
		  string bookGroup,
		  string book);

		void BookClearingInstructionSet(
		  string bookGroup,
		  string book,
		  string countryCode,
		  string currencyCode,
		  string divRate,
		  string cashInstructions,
		  string securityInstructions,
		  string actor,
		  bool isActive);

		DataSet BookFundInstructionGet(
			string bookGroup,
			string book,
			string currencyIso,
			short utcOffset);

		void BookFundInstructionSet(
			string bookGroup,
			string book,
			string currencyIso,
			string fund,
			string actor,
			bool isActive);



	}
}
