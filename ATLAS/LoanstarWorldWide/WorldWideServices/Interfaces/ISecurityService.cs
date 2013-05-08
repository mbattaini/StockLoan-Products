using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.SecurityService
{

    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        bool UserPasswordChange(string userId, string oldEPassword, string newEPassword);

        [OperationContract]
        bool UserPasswordReset(string userId, string newEPassword);

        [OperationContract]
        int UserPasswordResetCheck(string userId, string ePassword, string sourceAddress, int resetReq);
        
        [OperationContract]
        bool UserValidate(string userId, string ePassword, bool pwdEncrypted);        
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
