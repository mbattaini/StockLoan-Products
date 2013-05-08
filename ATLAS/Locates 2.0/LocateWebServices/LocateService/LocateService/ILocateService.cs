using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LocateService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILocateService
    {
        [OperationContract]
        byte[] LocateGet(string tradeDate, string groupCode, string locateId, string status, short utcOffset);

        [OperationContract]
        void LocateSet(string locateId, string comment, string quantity, string userId);

        [OperationContract]
        void SeucrityIdInformationGet(string secId);

        [OperationContract]
        byte[] LocateResearchGet(string locateId,
                              string startDate,
                              string stopDate,
                              string clientId,
                              string groupCode,
                              string secId,
                              string source,
                              string status,
                              string actor,
                              string comment,
                              string clientQuantity,
                              string clientQuantityOperator,
                              string quantity,
                              string quantityOperator,
                              string openTime,
                              string openTimeOperator,
                              short utcOffset);
    }   
}
