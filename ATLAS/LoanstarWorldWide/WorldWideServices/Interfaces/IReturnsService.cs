using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.ReturnsService
{
    [ServiceContract]
    public interface IReturnsService
    {
        [OperationContract]
        bool ReturnAsOfSet(string tradeDate, string bookGroup, string book, string contractId, string contractType,
                string returnId, string quantity, string actUserId, string settleDate, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool ReturnSet(string returnId, string bizDate, string bookGroup, string book, string contractId, string contractType,
                    string quantity, string actUserId, string settleDateProjected, string settleDateActual, bool isActive, 
                    string userId, string userPassword, string functionPath);
        
        [OperationContract]
        byte[] ReturnsGet(string returnId, string bizDate, string bookGroup, string contractId, short utcOffSet, 
                            string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ReturnsSummaryByCashGet(string returnId, string bizDate, string bookGroup, string contractId, short utcOffSet, 
                                        string userId, string userPassword, string functionPath); 

    }
}
