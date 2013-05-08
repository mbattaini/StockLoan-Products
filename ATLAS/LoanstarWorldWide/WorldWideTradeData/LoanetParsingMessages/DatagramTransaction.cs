using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StockLoan.DataAccess;

namespace LoanetParsingMessages
{
    public class DatagramTransaction
    {
        public DatagramTransaction(string message)
        {
            string systemTime = message.Substring(3, 8);
            string clientId = message.Substring(12, 4);
            string contractId = message.Substring(49, 9);
            string matchingContractId = message.Substring(174, 9);
            string contractType = message.Substring(35, 1);
            string contraClientId = message.Substring(45, 4);
            string secId = message.Substring(36, 9);
            string inputTerminal = message.Substring(62, 2);
            string inputTimestamp = message.Substring(64, 6);
            string transDescription = message.Substring(70, 11).Trim();
            string debitCreditFlag = message.Substring(81, 1);
            string batchCode = message.Substring(34, 1);
            string deliverViaCode = message.Substring(173, 1);
            string quantity = message.Substring(82, 12); 
            string amount = message.Substring(96, 12) + "." + message.Substring(108, 2);
            string originalQuantity = message.Substring(188, 12);
            string originalAmount = message.Substring(202, 12) + "." + message.Substring(214, 2);
            string collateralCode = message.Substring(110, 1);
            string valueDate = message.Substring(216, 6);
            string settleDate = message.Substring(115, 6);
            string expiryDate = message.Substring(127, 6);
            string returnDate = message.Substring(161, 6);
            string interestFromDate = message.Substring(121, 6);
            string interestToDate = message.Substring(167, 6);
            string rate = message.Substring(133, 3) + "." + message.Substring(136, 4);
            string rateCode = message.Substring(140, 1);
            string poolCode = message.Substring(183, 1);
            string divRate = message.Substring(224, 3) + "." + message.Substring(227, 3);
            string divCallable = message.Substring(230, 1);
            string incomeTracked = message.Substring(231, 1);
            string marginCode = message.Substring(184, 1);
            string margin = message.Substring(185, 3);
            string currencyIso = Functions.CurrencyIso(message.Substring(32, 2));
            string securityDepot = message.Substring(222, 2);
            string cashDepot = message.Substring(30, 2);
            string otherClientId = message.Substring(111, 2);
            string filler = message.Substring(232, 18);
            string comment = message.Substring(141, 20);
            string actType = "";

           /* DBContracts.ContractSet(
                "",
                clientId,
                contractId,
                contractType,
                contraClientId,
                secId,
                quantity,
                quantity,
                amount,
                amount,
                collateralCode,
                valueDate,
                settleDate,
                "",
                rate,
                rateCode,
                "",
                poolCode,
                divRate,
                divCallable,
                bool.Parse(incomeTracked),
                marginCode,
                margin);*/
                


           
        }
    }
}
