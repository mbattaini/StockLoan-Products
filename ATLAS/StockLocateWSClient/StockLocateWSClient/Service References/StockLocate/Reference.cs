﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockLocateWSClient.StockLocate {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://PensonWebServices/StockLocate", ConfigurationName="StockLocate.StockLocateSoap")]
    public interface StockLocateSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/submitStockLocate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string submitStockLocate(string argCustomerID, string argDateTime, string argComment, string argLocateList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/submitStockLocateXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode submitStockLocateXML(string argCustomerID, string argDateTime, string argComment, string argLocateList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/submitStockLocateComplex", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string submitStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argComment, string argLocateList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/viewStockLocate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string viewStockLocate(string argCustomerID, string argDateTime, string argTradeDateMin, string argTradeDateMax, string argFilterSecID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/viewStockLocateXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode viewStockLocateXML(string argCustomerID, string argDateTime, string argTradeDateMin, string argTradeDateMax, string argFilterSecID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/viewStockLocateComplex", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string viewStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argTradeDateMin, string argTradeDateMax, string argFilterSecID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/viewSingleStockLocate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string viewSingleStockLocate(string argCustomerID, string argDateTime, string argLocateID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/viewSinglStockLocateXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode viewSinglStockLocateXML(string argCustomerID, string argDateTime, string argLocateID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/viewSingleStockLocateComplex", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string viewSingleStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argLocateID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/submitSingleStockLocate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string submitSingleStockLocate(string argCustomerID, string argDateTime, string argComment, string argSecID, string argQuantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/submitSingleStockLocateXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode submitSingleStockLocateXML(string argCustomerID, string argDateTime, string argComment, string argSecID, string argQuantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://PensonWebServices/StockLocate/submitSingleStockLocateComplex", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string submitSingleStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argComment, string argSecID, string argQuantity);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface StockLocateSoapChannel : StockLocateWSClient.StockLocate.StockLocateSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockLocateSoapClient : System.ServiceModel.ClientBase<StockLocateWSClient.StockLocate.StockLocateSoap>, StockLocateWSClient.StockLocate.StockLocateSoap {
        
        public StockLocateSoapClient() {
        }
        
        public StockLocateSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StockLocateSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockLocateSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockLocateSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string submitStockLocate(string argCustomerID, string argDateTime, string argComment, string argLocateList) {
            return base.Channel.submitStockLocate(argCustomerID, argDateTime, argComment, argLocateList);
        }
        
        public System.Xml.XmlNode submitStockLocateXML(string argCustomerID, string argDateTime, string argComment, string argLocateList) {
            return base.Channel.submitStockLocateXML(argCustomerID, argDateTime, argComment, argLocateList);
        }
        
        public string submitStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argComment, string argLocateList) {
            return base.Channel.submitStockLocateComplex(argCustomerID, argDateTime, argReturnType, argDelimiter, argComment, argLocateList);
        }
        
        public string viewStockLocate(string argCustomerID, string argDateTime, string argTradeDateMin, string argTradeDateMax, string argFilterSecID) {
            return base.Channel.viewStockLocate(argCustomerID, argDateTime, argTradeDateMin, argTradeDateMax, argFilterSecID);
        }
        
        public System.Xml.XmlNode viewStockLocateXML(string argCustomerID, string argDateTime, string argTradeDateMin, string argTradeDateMax, string argFilterSecID) {
            return base.Channel.viewStockLocateXML(argCustomerID, argDateTime, argTradeDateMin, argTradeDateMax, argFilterSecID);
        }
        
        public string viewStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argTradeDateMin, string argTradeDateMax, string argFilterSecID) {
            return base.Channel.viewStockLocateComplex(argCustomerID, argDateTime, argReturnType, argDelimiter, argTradeDateMin, argTradeDateMax, argFilterSecID);
        }
        
        public string viewSingleStockLocate(string argCustomerID, string argDateTime, string argLocateID) {
            return base.Channel.viewSingleStockLocate(argCustomerID, argDateTime, argLocateID);
        }
        
        public System.Xml.XmlNode viewSinglStockLocateXML(string argCustomerID, string argDateTime, string argLocateID) {
            return base.Channel.viewSinglStockLocateXML(argCustomerID, argDateTime, argLocateID);
        }
        
        public string viewSingleStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argLocateID) {
            return base.Channel.viewSingleStockLocateComplex(argCustomerID, argDateTime, argReturnType, argDelimiter, argLocateID);
        }
        
        public string submitSingleStockLocate(string argCustomerID, string argDateTime, string argComment, string argSecID, string argQuantity) {
            return base.Channel.submitSingleStockLocate(argCustomerID, argDateTime, argComment, argSecID, argQuantity);
        }
        
        public System.Xml.XmlNode submitSingleStockLocateXML(string argCustomerID, string argDateTime, string argComment, string argSecID, string argQuantity) {
            return base.Channel.submitSingleStockLocateXML(argCustomerID, argDateTime, argComment, argSecID, argQuantity);
        }
        
        public string submitSingleStockLocateComplex(string argCustomerID, string argDateTime, string argReturnType, string argDelimiter, string argComment, string argSecID, string argQuantity) {
            return base.Channel.submitSingleStockLocateComplex(argCustomerID, argDateTime, argReturnType, argDelimiter, argComment, argSecID, argQuantity);
        }
    }
}