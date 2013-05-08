using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.MarksService
{

    [ServiceContract]
    public interface IMarksService
    {
        [OperationContract]
        int MarkAsOfSet(string tradeDate, string settleDate, string bookGroup, string book, string contractId, string contractType,
                string price, string markId, string deliveryCode, string actUserId, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool MarkIsExistGet(string bizDate, string bookGroup, string book, string contractId, string contractType, string secId, 
                            string amount, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool MarkSet(string markId, string bizDate, string bookGroup, string book, string contractId, string contractType,
            string secId, string amount, string openDate, string settleDate, string deliveryCode, string actUserId, bool isActive, 
            string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] MarksGet(string markId, string bizDate, string contractId, string bookGroup, short utcOffset, 
                            string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] MarksSummaryByCashGet(string markId, string bizDate, string contractId, string bookGroup, 
                                        string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] MarksSummaryGet(string markId, string bizDate, string bizDateFormat, string contractId, string bookGroup, 
                                string userId, string userPassword, string functionPath);

    }    
}
