using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class Recalls
    {

        public static DataSet RecallsGet(string bizDate, string recallId, string bookGroup, short utcOffset)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                dsTemp = DBRecalls.RecallsGet(bizDate, recallId, bookGroup, utcOffset);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void RecallSet(
            string recallId, 
            string bizDate, 
            string bookGroup, 
            string contractId,
            string contractType,
            string book, 
            string secId,
            string quantity,
            string openDateTime,
            string reasonCode,
            string status,
            string actUserId,
            string sequenceNumber, 
            string moveToDate, 
            string buyInDate, 
            string comment, 
            bool isActive)
        {
            try
            {
                if (recallId.Equals(""))
                {
                    throw new Exception("Recall ID is required");
                }

                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
            
                if (book.Equals(""))
                {
                    throw new Exception("Book is required");
                }

                if (contractId.Equals(""))
                {
                    throw new Exception("Contract ID is required");
                }

                if (secId.Equals(""))
                {
                    throw new Exception("Security ID is required");
                }

                if (quantity.Equals(""))
                {
                    throw new Exception("Quantity is required");
                }
                
                if (openDateTime.Equals(""))
                {
                    throw new Exception("Open Date is required");
                }
                
                if (reasonCode.Equals(""))
                {
                    throw new Exception("Reason Code is required");
                }
                
                if (status.Equals(""))
                {
                    throw new Exception("Status is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }

                DBRecalls.RecallSet(
                    recallId, 
                    bizDate, 
                    bookGroup, 
                    contractId, 
                    contractType, 
                    book, 
                    secId,
                    quantity, 
                    openDateTime,
                    moveToDate,
                    buyInDate, 
                    reasonCode, 
                    sequenceNumber, 
                    comment, 
                    status,
                    actUserId,
                    isActive);                
            }
            catch 
            {
                throw;
            }
        }
    }
}
