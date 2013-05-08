using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Threading;
using StockLoan.Common;




namespace StockLoan.Main
{
	public class ProcessAgent : MarshalByRefObject, IProcess
	{
		private string dbCnStr = "";
        //private WebServiceProcess.TradeGeneric tradeGeneric;
		
        
        
        public ProcessAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
            //tradeGeneric = new WebServiceProcess.TradeGeneric();            
		}

        public void ProcessContarctSubmit(
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
            bool isActive,             
            string feeAmount,
            string feeCurrencyIso,
            string feeType,
            string fund,
            string tradeRefId)
        {
            //Load contract clearing instructions
            
            /*string buySellCode
            
            
            tradeGeneric.TradeNewRequest(
                "", //CorrespondentCode,
                contractId,
                "", // System
                "", //ISIN
                secId,
                "", //ExchangeCode
                book,
                "", //CounterParty
                "", //CounterPartyCode
                contractType,
                quantity,
                "" ,//price,
                "false",
                "" ,// capacity
                "", //clientmarkindicator
                currencyIso,
                "",
                "Stock Loan",
                "PFSL",
                margin,
                valueDate,
                termDate,
                "", // fixed interest date
                "true",
                ""); //clearingSystem*/
        }

        

      /* public DataSet ProcessSystemsGet()
        {
            return tradeGeneric.TradeSystemsGet();
        }

        public DataSet ProcessSystemCounterPartiesGet(string system)
        {
            return tradeGeneric.TradeCounterPartiesGet(system);
        }*/
        


		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
