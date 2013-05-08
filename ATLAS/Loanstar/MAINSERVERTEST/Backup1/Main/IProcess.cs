using System;
using System.Data;

namespace StockLoan.Main
{
	public interface IProcess
	{
		DataSet ProcessStatusGet(string bizDate, short utcOffset);
		DataSet ProcessMessageGet(string bizDate, short utcOffset);
	}
}
