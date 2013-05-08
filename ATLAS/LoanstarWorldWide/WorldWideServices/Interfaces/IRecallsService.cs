using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.RecallsService
{
        
    [ServiceContract]
    public interface IRecallsService
    {
        [OperationContract]
        bool RecallSet(string recallId, string bizDate, string bookGroup, string contractId, string contractType, string book, string secId, 
                string quantity, string openDateTime, string reasonCode, string status, string actUserId, string sequenceNumber, 
                string moveToDate, string buyInDate, string comment, bool isActive, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] RecallsGet(string bizDate, string recallId, short utcOffSet, string bookGroup, string userId, string userPassword, string functionPath);

    }
}
