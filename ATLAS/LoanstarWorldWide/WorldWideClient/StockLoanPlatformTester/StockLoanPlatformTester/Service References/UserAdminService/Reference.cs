﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockLoanPlatformTester.UserAdminService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserAdminService.IUserAdminService")]
    public interface IUserAdminService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/GetSourceIP", ReplyAction="http://tempuri.org/IUserAdminService/GetSourceIPResponse")]
        string GetSourceIP();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/FunctionSet", ReplyAction="http://tempuri.org/IUserAdminService/FunctionSetResponse")]
        bool FunctionSet(string functionPathSet, bool mayView, bool mayEdit, string bookGroup, string functionPath, string userId, string userPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/FunctionsGet", ReplyAction="http://tempuri.org/IUserAdminService/FunctionsGetResponse")]
        byte[] FunctionsGet(string functionPathGet, string bookGroup, string functionPath, string userId, string userPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/RoleFunctionSet", ReplyAction="http://tempuri.org/IUserAdminService/RoleFunctionSetResponse")]
        bool RoleFunctionSet(string roleName, string functionPathSet, bool mayView, bool mayEdit, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/RoleSet", ReplyAction="http://tempuri.org/IUserAdminService/RoleSetResponse")]
        bool RoleSet(string roleId, string roleName, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/RolesGet", ReplyAction="http://tempuri.org/IUserAdminService/RolesGetResponse")]
        byte[] RolesGet(string roleCode, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/UserGet", ReplyAction="http://tempuri.org/IUserAdminService/UserGetResponse")]
        byte[] UserGet(string userIdGet, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/RoleFunctionsGet", ReplyAction="http://tempuri.org/IUserAdminService/RoleFunctionsGetResponse")]
        byte[] RoleFunctionsGet(string roleName, string functionPathGet, short utcOffSet, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/UserRolesGet", ReplyAction="http://tempuri.org/IUserAdminService/UserRolesGetResponse")]
        byte[] UserRolesGet(string userIdGet, string roleName, short utcOffset, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/UserRolesSet", ReplyAction="http://tempuri.org/IUserAdminService/UserRolesSetResponse")]
        bool UserRolesSet(string userIdSet, string roleName, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserAdminService/UserSet", ReplyAction="http://tempuri.org/IUserAdminService/UserSetResponse")]
        bool UserSet(string userIdSet, string shortName, string newPassword, string email, string title, string comment, bool isLocked, bool isActive, string userId, string userPassword, string bookGroup, string functionPath);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserAdminServiceChannel : StockLoanPlatformTester.UserAdminService.IUserAdminService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserAdminServiceClient : System.ServiceModel.ClientBase<StockLoanPlatformTester.UserAdminService.IUserAdminService>, StockLoanPlatformTester.UserAdminService.IUserAdminService {
        
        public UserAdminServiceClient() {
        }
        
        public UserAdminServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserAdminServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserAdminServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserAdminServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetSourceIP() {
            return base.Channel.GetSourceIP();
        }
        
        public bool FunctionSet(string functionPathSet, bool mayView, bool mayEdit, string bookGroup, string functionPath, string userId, string userPassword) {
            return base.Channel.FunctionSet(functionPathSet, mayView, mayEdit, bookGroup, functionPath, userId, userPassword);
        }
        
        public byte[] FunctionsGet(string functionPathGet, string bookGroup, string functionPath, string userId, string userPassword) {
            return base.Channel.FunctionsGet(functionPathGet, bookGroup, functionPath, userId, userPassword);
        }
        
        public bool RoleFunctionSet(string roleName, string functionPathSet, bool mayView, bool mayEdit, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.RoleFunctionSet(roleName, functionPathSet, mayView, mayEdit, comment, delete, userId, userPassword, bookGroup, functionPath);
        }
        
        public bool RoleSet(string roleId, string roleName, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.RoleSet(roleId, roleName, comment, delete, userId, userPassword, bookGroup, functionPath);
        }
        
        public byte[] RolesGet(string roleCode, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.RolesGet(roleCode, userId, userPassword, bookGroup, functionPath);
        }
        
        public byte[] UserGet(string userIdGet, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.UserGet(userIdGet, userId, userPassword, bookGroup, functionPath);
        }
        
        public byte[] RoleFunctionsGet(string roleName, string functionPathGet, short utcOffSet, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.RoleFunctionsGet(roleName, functionPathGet, utcOffSet, userId, userPassword, bookGroup, functionPath);
        }
        
        public byte[] UserRolesGet(string userIdGet, string roleName, short utcOffset, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.UserRolesGet(userIdGet, roleName, utcOffset, userId, userPassword, bookGroup, functionPath);
        }
        
        public bool UserRolesSet(string userIdSet, string roleName, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.UserRolesSet(userIdSet, roleName, comment, delete, userId, userPassword, bookGroup, functionPath);
        }
        
        public bool UserSet(string userIdSet, string shortName, string newPassword, string email, string title, string comment, bool isLocked, bool isActive, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.UserSet(userIdSet, shortName, newPassword, email, title, comment, isLocked, isActive, userId, userPassword, bookGroup, functionPath);
        }
    }
}