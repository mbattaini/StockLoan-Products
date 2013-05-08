using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBusiness
{
	public class Inventory
	{
		public static DataSet InventoryDailyAggreagates(DataSet dsInventory)
		{			
            bool isFound = false;
			
			DataSet dsTarget = new DataSet();
            dsTarget.Tables.Add("InventorySummary");
            
            dsTarget.Tables["InventorySummary"].Columns.Add("BizDate", typeof(string));
            dsTarget.Tables["InventorySummary"].Columns.Add("Quantity", typeof(long));
            dsTarget.AcceptChanges(); 
			
			 if (dsInventory != null)
            {
                foreach (DataRow dr in dsInventory.Tables["Inventory"].Rows)
                {
                    isFound = false;

                    foreach (DataRow dr2 in dsTarget.Tables["InventorySummary"].Rows)
                    {
                        if (dr["BizDate"].ToString().ToUpper().Equals(dr2["BizDate"].ToString().ToUpper()))
                        {
                            dr2["Quantity"] = long.Parse(dr2["Quantity"].ToString()) + long.Parse(dr["Quantity"].ToString());
							isFound = true;							
                        }
                    }

                    if (!isFound)
                    {
                        DataRow tempRecord = dsTarget.Tables["InventorySummary"].NewRow();                      
                        tempRecord["BizDate"] = dr["BizDate"].ToString().ToUpper();
                    	tempRecord["Quantity"] = long.Parse(dr["Quantity"].ToString());
                    	dsTarget.Tables["InventorySummary"].Rows.Add(tempRecord);
                        dsTarget.AcceptChanges();
                    }
                }
			}		
		
			return dsTarget;
		}
	}
}