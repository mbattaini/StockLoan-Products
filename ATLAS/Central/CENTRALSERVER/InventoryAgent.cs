using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using StockLoan.Common;
using StockLoan.Central;

namespace StockLoan.Central
{
	public class InventoryAgent : MarshalByRefObject, IInventory
	{
		private string dbCnStr;

        public InventoryAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr; 
		}

     
		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
