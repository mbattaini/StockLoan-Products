// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC  2005  All rights reserved.

using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;


[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="FaxSoap", Namespace="http://stockloan.net/")]
public class Fax : System.Web.Services.Protocols.SoapHttpClientProtocol
{
  public Anetics.Services.AuthHeader AuthHeader;
    
  public Fax(Anetics.Services.AuthHeader authHeader, string uri) 
  {
    this.Url = uri + "fax.asmx";
    this.AuthHeader = authHeader;
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Send", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public long Send(string number, string name, string subject, string content, string receipt) 
  {
    object[] results = this.Invoke("Send", new object[] {number, name, subject, content, receipt});
    return ((long)(results[0]));
  }
    
  public System.IAsyncResult BeginSend(string number, string name, string subject, string content, string receipt, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Send", new object[] {number, name, subject, content, receipt}, callback, asyncState);
  }
    
  public long EndSend(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
    return ((long)(results[0]));
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/StatusGet", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public string StatusGet(long faxId) 
  {
    object[] results = this.Invoke("StatusGet", new object[] {faxId});
    return ((string)(results[0]));
  }
    
  public System.IAsyncResult BeginStatusGet(long faxId, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("StatusGet", new object[] {faxId}, callback, asyncState);
  }
    
  public string EndStatusGet(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
    return ((string)(results[0]));
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Cancel", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public void Cancel(long faxId) 
  {
    object[] results = this.Invoke("Cancel", new object[] {faxId});
  }
    
  public System.IAsyncResult BeginCancel(long faxId, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Cancel", new object[] {faxId}, callback, asyncState);
  }
    
  public void EndCancel(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
  }
}
