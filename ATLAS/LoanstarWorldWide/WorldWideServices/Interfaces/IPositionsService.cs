using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using StockLoan.Business;
namespace StockLoan.WebServices.PositionsService
{
    [ServiceContract]
    public interface IPositionsService
    {
        [OperationContract]
        byte[] BoxPositionGet(string bizDate, string bookGroup, string secId, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] BoxPositionLookupGet(string boxItems, string bookGroup, bool includeRates, string userId);

        [OperationContract]
        byte[] BoxSummaryDataConfigGet(string bizDate, string bookGroup, bool IsOptimistic, string hairCutUser, string userId, string userPassword, string functionPath);
    }
}
