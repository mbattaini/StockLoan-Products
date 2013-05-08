﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockLoanPlatformTester.DealsService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DealsService.IDealsService")]
    public interface IDealsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDealsService/GetSourceIP", ReplyAction="http://tempuri.org/IDealsService/GetSourceIPResponse")]
        string GetSourceIP();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDealsService/DealSet", ReplyAction="http://tempuri.org/IDealsService/DealSetResponse")]
        bool DealSet(
                    string dealId, 
                    string bookGroup, 
                    string dealType, 
                    string book, 
                    string bookContact, 
                    string contractId, 
                    string secId, 
                    string quantity, 
                    string amount, 
                    string collateralCode, 
                    string valueDate, 
                    string settleDate, 
                    string termDate, 
                    string rate, 
                    string rateCode, 
                    string poolCode, 
                    string divRate, 
                    bool divCallable, 
                    bool incomeTracked, 
                    string marginCode, 
                    string margin, 
                    string currencyIso, 
                    string securityDepot, 
                    string cashDepot, 
                    string comment, 
                    string fund, 
                    string dealStatus, 
                    bool isActive, 
                    string actUserId, 
                    bool returnData, 
                    string feeAmount, 
                    string feeCurrencyIso, 
                    string feeType, 
                    string userId, 
                    string userPassword, 
                    string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDealsService/DealsGet", ReplyAction="http://tempuri.org/IDealsService/DealsGetResponse")]
        byte[] DealsGet(string bizDate, string dealId, string dealIdPrefix, bool isActive, short utcOffSet, string userId, string userPassword, string bookGroup, string functionPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDealsService/DealToContract", ReplyAction="http://tempuri.org/IDealsService/DealToContractResponse")]
        bool DealToContract(string dealId, string bizDate, string userId, string userPassword, string bookGroup, string functionPath);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDealsServiceChannel : StockLoanPlatformTester.DealsService.IDealsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DealsServiceClient : System.ServiceModel.ClientBase<StockLoanPlatformTester.DealsService.IDealsService>, StockLoanPlatformTester.DealsService.IDealsService {
        
        public DealsServiceClient() {
        }
        
        public DealsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DealsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DealsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DealsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetSourceIP() {
            return base.Channel.GetSourceIP();
        }
        
        public bool DealSet(
                    string dealId, 
                    string bookGroup, 
                    string dealType, 
                    string book, 
                    string bookContact, 
                    string contractId, 
                    string secId, 
                    string quantity, 
                    string amount, 
                    string collateralCode, 
                    string valueDate, 
                    string settleDate, 
                    string termDate, 
                    string rate, 
                    string rateCode, 
                    string poolCode, 
                    string divRate, 
                    bool divCallable, 
                    bool incomeTracked, 
                    string marginCode, 
                    string margin, 
                    string currencyIso, 
                    string securityDepot, 
                    string cashDepot, 
                    string comment, 
                    string fund, 
                    string dealStatus, 
                    bool isActive, 
                    string actUserId, 
                    bool returnData, 
                    string feeAmount, 
                    string feeCurrencyIso, 
                    string feeType, 
                    string userId, 
                    string userPassword, 
                    string functionPath) {
            return base.Channel.DealSet(dealId, bookGroup, dealType, book, bookContact, contractId, secId, quantity, amount, collateralCode, valueDate, settleDate, termDate, rate, rateCode, poolCode, divRate, divCallable, incomeTracked, marginCode, margin, currencyIso, securityDepot, cashDepot, comment, fund, dealStatus, isActive, actUserId, returnData, feeAmount, feeCurrencyIso, feeType, userId, userPassword, functionPath);
        }
        
        public byte[] DealsGet(string bizDate, string dealId, string dealIdPrefix, bool isActive, short utcOffSet, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.DealsGet(bizDate, dealId, dealIdPrefix, isActive, utcOffSet, userId, userPassword, bookGroup, functionPath);
        }
        
        public bool DealToContract(string dealId, string bizDate, string userId, string userPassword, string bookGroup, string functionPath) {
            return base.Channel.DealToContract(dealId, bizDate, userId, userPassword, bookGroup, functionPath);
        }
    }
}
