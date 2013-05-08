using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StockLoanWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminService" in both code and config file together.
    [ServiceContract]
    public interface IAdmin
    {

        [OperationContract]
        bool MayView(string userId, string functionPath);

        [OperationContract]
        bool MayEdit(string userId, string functionPath);

        [OperationContract]
        string UserEmailGet(string userId);

        [OperationContract]
        bool MayViewBookGroup(string userId, string functionPath, string bookGroup);

        [OperationContract]
        bool MayEditBookGroup(string userId, string functionPath, string bookGroup);

        [OperationContract]
        void UserSet(string userId, string shortName, string email, string comment, string actUserId, bool isActive);

        [OperationContract]
        void RoleSet(string roleCode, string role, string comment, string actUserId, bool delete);

        [OperationContract]
        void UserRoleSet(string userId, string roleCode, string comment, string actUserId, bool delete);

        [OperationContract]
        void RoleFunctionSet(string roleCode, string functionPath, bool mayView, bool mayEdit, string bookGroupList, string comment, string actUserId);

        [OperationContract]
        byte[] UserRolesGet(short utcOffset);

        [OperationContract]
        byte[] UserRoleFunctionsGet(short utcOffset);

        [OperationContract]
        byte[] HolidaysGet();

        [OperationContract]
        void HolidaysSet(string date, string countryCode, bool isBankHoliday, bool isExchangeHoliday, bool isActive);

        [OperationContract]
        bool HolidayAutoUpdate();

        [OperationContract]
        byte[] BookDataGet(short utcOffset);

        [OperationContract]
        void BookDataSet(string bookGroup, string book, long amountLimitBorrow, long amountLimitLoan, string faxNumber, string firm, string country, string deskType, string actUserId, string comment);

        // [OperationContract]
        // override object InitializeLifetimeService();


    }
}
