using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.UserAdminService
{

    [ServiceContract]
    public interface IUserAdminService
    {
        [OperationContract]
        bool FunctionSet(string functionPathSet, bool mayView, bool mayEdit, string bookGroup, string functionPath, string userId, string userPassword); 

        [OperationContract]
        byte[] FunctionsGet(string functionPathGet, string bookGroup, string functionPath, string userId, string userPassword);

        [OperationContract]
        byte[] RoleFunctionsBookGroupGet(string userIdGet, string bookGroupGet, string bookGroup, string functionPath, string userId, string userPassword);

        [OperationContract]
        bool RoleFunctionsBookGroupSet(string roleNameSet, string functionPathSet, string bookGroupSet, string actUserId, short isActive, short delete, 
                       string bookGroup, string functionPath, string userId, string userPassword);

        [OperationContract]
        bool RoleFunctionSet(string roleName, string functionPathSet, bool mayView, bool mayEdit, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool RoleSet(string roleId, string roleName, string comment, bool delete, 
                        string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] RolesGet(string roleCode, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] UserGet(string userIdGet, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] RoleFunctionsGet(string roleName, string functionPathGet, short utcOffSet, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] UserRolesGet(string userIdGet, string roleName, short utcOffset, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool UserRolesSet(string userIdSet, string roleName, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool UserSet(string userIdSet, string shortName, string newPassword, string email, string title, string comment, bool isLocked, 
            bool isActive, bool isLoggedIn, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] UserBookGroupsGet(string userId, string functionPath);

    }
}
