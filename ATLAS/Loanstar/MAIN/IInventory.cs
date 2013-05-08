using System;
using System.Data;

namespace StockLoan.Main
{
	public interface IInventory
	{
		DataSet InventoryGet(string desk, string secId, short utcOffset);
		DataSet InventoryControlGet(string bizDate);

		void InventoryItemSet(string bizDate, string desk, string secId, string quantity);		
	}
}
