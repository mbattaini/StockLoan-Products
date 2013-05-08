using System;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.Inventory
{

    /// <summary>
    /// Data Access Object for DB table Locates.tbInventory
    /// </summary>
    class InventoryDataEntry
    {
        private DateTime dtBizDate;
        public DateTime BizDate 
        {
            get { return dtBizDate; }
            set { dtBizDate = value; }
        }

        private string strDesk = "";
        public string Desk
        {
            get { return strDesk; }
            set { strDesk = value; }
        }

        private string strSecId = "";
        public string SecId
        {
            get { return strSecId; }
            set { strSecId = value; }
        }

        private string strAccount = "";
        public string Account
        {
            get { return strAccount; }
            set { strAccount = value; }
        }

        private string strMode = "";
        public string ModeCode
        {
            get { return strMode; }
            set { strMode = value; }
        }

        private long nQuantity = 0;
        public long Quantity
        {
            get { return nQuantity; }
            set { nQuantity = value; }
        }

        private long nExecutionID = 0;
        public long ExecutionID
        {
            get { return nExecutionID; }
            set { nExecutionID = value; }
        }

        private bool bHasErrors = false;
        public bool HasErrors
        {
            get { return bHasErrors; }
            set { bHasErrors = value; }
        }

        private string strErrorMsg;
        public string ErrorMsg
        {
            get { return strErrorMsg; }
            set { strErrorMsg = value; }
        }


        //---------------------------------------------------------------------
        //C'tors
        public InventoryDataEntry()
        {
            InitializeComponent();
        }
        public InventoryDataEntry(string SecId, long Quantity, long ExecutionID )
        {
            InitializeComponent();
            strSecId = SecId;
            nQuantity = Quantity;
            nExecutionID = ExecutionID;
        }
        private void InitializeComponent()
        {
            dtBizDate = DateTime.Parse("1-1-1900");
        }






    }
}
