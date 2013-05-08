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
[System.Web.Services.WebServiceBindingAttribute(Name="FtpSoap", Namespace="http://stockloan.net/")]
public class Filer : System.Web.Services.Protocols.SoapHttpClientProtocol
{
  public Anetics.Services.AuthHeader AuthHeader;
    
  public Filer(Anetics.Services.AuthHeader authHeader, string uri) 
  {
    this.Url = uri + "filer.asmx";
    this.AuthHeader = authHeader;
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Get", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public string Get(string path, string host, string userId, string password) 
  {
    object[] results = this.Invoke("Get", new object[] {path, host, userId, password});
    return ((string)(results[0]));
  }
    
  public System.IAsyncResult BeginGet(string path, string host, string userId, string password, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Get", new object[] {path, host, userId, password}, callback, asyncState);
  }
    
  public string EndGet(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
    return ((string)(results[0]));
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Put", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public void Put(string path, string host, string userId, string password, string content) 
  {
    this.Invoke("Put", new object[] {path, host, userId, password, content});
  }
    
  public System.IAsyncResult BeginPut(string path, string host, string userId, string password, string content, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Put", new object[] {path, host, userId, password, content}, callback, asyncState);
  }
    
  public void EndPut(System.IAsyncResult asyncResult) 
  {
    this.EndInvoke(asyncResult);
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Kill", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public void Kill(string path, string host, string userId, string password) 
  {
    this.Invoke("Kill", new object[] {path, host, userId, password});
  }
    
  public System.IAsyncResult BeginKill(string path, string host, string userId, string password, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Kill", new object[] {path, host, userId, password}, callback, asyncState);
  }
    
  public void EndKill(System.IAsyncResult asyncResult) 
  {
    this.EndInvoke(asyncResult);
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/TimeGet", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public string TimeGet(string path, string host, string userId, string password) 
  {
    object[] results = this.Invoke("TimeGet", new object[] {path, host, userId, password});
    return ((string)(results[0]));
  }
    
  public System.IAsyncResult BeginTimeGet(string path, string host, string userId, string password, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("TimeGet", new object[] {path, host, userId, password}, callback, asyncState);
  }
    
  public string EndTimeGet(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
    return ((string)(results[0]));
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Exists", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  public bool Exists(string path, string host, string userId, string password) 
  {
    object[] results = this.Invoke("Exists", new object[] {path, host, userId, password});
    return ((bool)(results[0]));
  }
    
  public System.IAsyncResult BeginExists(string path, string host, string userId, string password, System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Exists", new object[] {path, host, userId, password}, callback, asyncState);
  }
    
  public bool EndExists(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
    return ((bool)(results[0]));
  }
}
