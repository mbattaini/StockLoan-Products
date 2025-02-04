// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Data;

namespace Anetics.Medalist
{
  public interface IAdmin
  {    
		bool MayView(string userId, string functionPath);
    bool MayEdit(string userId, string functionPath);

    bool MayViewBookGroup(string userId, string functionPath, string bookGroup);
    bool MayEditBookGroup(string userId, string functionPath, string bookGroup);

    string UserEmailGet(string userId);

    void UserSet(
      string userId, 
      string shortName, 
      string email, 
      string comment, 
      string actUserId, 
      bool   isActive);
   
    void RoleSet(
      string roleCode,
      string role,      
      string comment,
      string actUserId,
      bool   delete);

    void UserRoleSet(
      string userId,
      string roleCode,
      string comment,
      string actUserId,
      bool   delete);
    
    void RoleFunctionSet(
      string roleCode, 
      string functionPath,
      bool   mayView,
      bool   mayEdit,
      string bookGroupList, 
      string comment, 
      string actUserId);
    
    DataSet UserRolesGet(short utcOffset);
    DataSet UserRoleFunctionsGet(short utcOffset);

    DataSet HolidaysGet();
    
    void HolidaysSet(
      string date,
      string countryCode,
      bool   isBankHoliday,
      bool   isExchangeHoliday,
      bool   isActive);     
  
    bool HolidayAutoUpdate();

    DataSet BookDataGet(short utcOffset);

    void BookDataSet(string bookGroup,
      string book,
      long   amountLimitBorrow,
      long   amountLimitLoan,
      string faxNumber,
      string firm,
      string country,
      string desktype,
      string actUserId,
      string comments);
  }
}
