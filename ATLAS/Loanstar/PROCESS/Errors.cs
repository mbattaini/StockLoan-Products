using System;

namespace StockLoan.Process
{
	public class Errors
	{    
		public const string MESSAGE_TYPE_ERROR           = "Invalid Message Type";
		public const string VERSION_ERROR								 = "Invalid Version";
		public const string BOOK_GROUP_ERROR             = "Invalid Book Group"; 
		public const string BOOK_ERROR                   = "Invalid Book";
		public const string OTHER_BOOK_ERROR             = "Other Book Error";
		public const string ACCOUNT_CREDITOR_ERROR       = "Account Creditor Error";
		public const string CONTRACT_TYPE_ERROR          = "Invalid Contract Type";
		public const string CONTRACT_ID_ERROR            = "Contract ID Error";    
		public const string TABLE_RATE_ERROR             = "New Box/Table Rate Error";
		public const string INTEREST_RATE_ERROR          = "Interest Rate Error";
		public const string RATE_ERROR                   = "Rate Error";    
		public const string RATE_CODE_ERROR              = "Rate Code Error";
		public const string NEGOTIATED_RATE_ERROR        = "Negotiated Rate Error";
		public const string FIXED_INVESTMENT_RATE_ERROR  = "Fixed Investment Rate Error";
		public const string MARGIN_CODE_ERROR            = "Margin Code Error";   
		public const string MARGIN_ERROR                 = "Margin Error";
		public const string DIV_RATE_ERROR               = "Div Rate Error";    
		public const string CCF_ERROR                    = "Past CCF Cutoff Error";
		public const string QUANTITY_ERROR               = "Quantity Error";
		public const string BATCH_CODE_ERROR             = "Batch Code Error";
		public const string DELIVERY_CODE_ERROR          = "Del Via Code Error";    
		public const string AMOUNT_ERROR                 = "Amount Error";
		public const string CALLBACK_ERROR               = "Callback Required Error";
		public const string RECORD_TYPE_ERROR						 = "Record Type Error";
		public const string BANK_NUMBER_ERROR						 = "Bank Number Error";
		public const string RECDEL_ERROR                 = "Rec/Del Location Error";
		public const string MONEY_SETTLEMENT_ERROR       = "Money Settlement Location Error";
		public const string POOL_CODE_ERROR              = "Invalid Pool Code";
		public const string EFFECTIVE_DATE_ERROR         = "Effective Date Error";
		public const string SECURITY_TYPE_ERROR          = "Security Type Error";
		public const string RECALL_TERMINATION_ERROR     = "Recall/Termination Error";
		public const string LOAN_DATE_ERROR				       = "Loan Date Error";
		public const string LOAN_PURPOSE_ERROR			     = "Loan Purpose Error";
		public const string RELEASE_TYPE_ERROR			     = "Release Type Error";
		public const string HYPOTHECATION_ERROR			     = "Hypothecation Error";
		public const string REASON_CODE_ERROR            = "Reason Code Error";
		public const string LENDER_REFERENCE_ERROR       = "Lender Reference Error";
		public const string SEQUENCE_NUMBER_ERROR        = "Sequence Number Error";    
		public const string CUSIP_ERROR                  = "Cusip Error";
		public const string COLLATERAL_CODE_ERROR        = "Collateral Code Error";
		public const string EXPIRY_DATE_ERROR            = "Expiry Date Error";
		public const string DELIVERY_DATE_ERROR          = "Delivery Date Error";
		public const string RECALL_DATE_ERROR            = "Recall Date Error";    
		public const string BUYIN_DATE_ERROR             = "BuyIn Date Error";    
		public const string ZEROINTEREST_DATE_ERROR      = "Zero Interest Date Error";
		public const string DELIVERY_LOCATION_ERROR      = "Delivery Location Error";
		public const string INCOME_TRACKED_ERROR         = "Income Tracked Error";
		public const string CURRENCY_CODE_ERROR          = "Currency Code Error";
		public const string EXCHANGE_CODE_ERROR          = "Exchange Code Error";
		public const string NEGATIVE_ERROR               = "Contract Already Negative Error";
		public const string PREVENT_PEND_ERROR					 = "Prevent Pend Indicator Error";
		public const string CNS_ERROR										 = "CNS Indicator Error";
		public const string IPO_ERROR					           = "IPO Indicator Error";
		public const string PTA_ERROR					           = "PTA Indicator Error";
		public const string OCC_PARTICIPENT_ERROR		     = "OCC Participant Error";
		public const string OCC_NUMBER_ERROR			       = "OCC Number Error";
		public const string COMMENT_ERROR				         = "Comment Error";
		public const string INPUT_SEQUENCE_ERROR		     = "Input Sequence Error";
		public const string PRICE_ERROR					         = "Price Error";
		public const string TIME_LIMIT_ERROR			       = "Time Limit Error";
		public const string FILLER_ERROR				         = "Filler Error";
		public const string ADD_INDICATOR_ERROR			     = "Add Indicator Error";		
		public const string CONTRACT_NOT_FOUND_ERROR     = "Contract Not Found";
		public const string FUTURE_DATE_ERROR            = "Future Delivery Date Error";
		public const string NO_UPDATES_ERROR             = "No Updated Requested";
		public const string INVALID_ACTION_CODE          = "Invalid Action Code";
		public const string RECALL_DUE_DATE_ERROR				 = "Invalid Recall Due Date";
		public const string CONTRACT_SOURCE_ERROR				 = "Contract Source Error";
		public const string SECURITY_VIOLATION					 = "Security Violation";
		public const string ARMS_CLIENT_ERROR						 = "Client no Sungard ARMS Participant";
		
		//ARMS ERROR MESSAGES
		public const string ARMS_CONTRA_NOT_PARTICIPANT				  = "Client no SunGard ARMS Particapant";
		public const string ARMS_CONTRACT_NOT_FILE_ERROR				= "Contract not on file";
		public const string ARMS_RECALL_DATE_ERROR							= "Recall date after next business date";
		public const string ARMS_BUYIN_RECALL_DATE_ERROR				= "BuyIn date prior to recall date";
		public const string ARMS_ZERO_INTEREST_RECALL_ERROR			= "Zero interest date prior to recall date";
		public const string ARMS_RECALLDUE_RECALL_DATE_ERROR		= "Recall due prior to recall date";
		public const string ARMS_RECALLDATE_DELIVERYDATE_ERROR	= "Recall date prior to delivery date";
		public const string ARMS_ASOF_RECALL_ERROR							= "Contra does not accept as-of recalls";
		public const string ARMS_LATE_RECALL_ERROR							= "Contra does not accept late recalls";
		public const string ARMS_DTC_CONTRA_ERROR								= "No valid DTC number for contra";
		public const string ARMS_LENDER_REF_UNIQUE_ERROR				= "Lender Reference not unique";
		public const string ARMS_CONTRACT_CONTRA_ERROR					= "Contract contra does not match recall contra";
		public const string ARMS_RECALL_QUANTITY_CONTRACT_ERROR = "Recall quantity exceeds contract quantity";
		public const string ARMS_RECALL_QUANTITY_AVAILABLE_ERROR= "Recall quantity exceeds available quantity";

		public const string ERROR_SEPERATOR              = "; ";

		public Errors()
		{
			
		}
	}
}
