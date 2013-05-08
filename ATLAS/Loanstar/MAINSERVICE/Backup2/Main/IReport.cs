using System;
using System.Data;

namespace StockLoan.Main
{
	public interface IReport
	{
       object BillingByBook(string startDate, string stopDate, string bookGroup);
       object BillingByContract(string startDate, string stopDate, string bookGroup);       
       object BillingBySecurityId(string startDate, string stopDate, string bookGroup);
	}
}
