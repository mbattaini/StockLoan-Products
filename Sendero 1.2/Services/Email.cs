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
  [System.Web.Services.WebServiceBindingAttribute(Name="EmailSoap", Namespace="http://stockloan.net/")]
  public class Email : System.Web.Services.Protocols.SoapHttpClientProtocol 
  {
    public Anetics.Services.AuthHeader AuthHeader;
    
    public Email(Anetics.Services.AuthHeader authHeader, string uri) 
    {
      this.Url = uri + "email.asmx";
      this.AuthHeader = authHeader;
    }
    
    [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/HeadersGet", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
    public EmailHeader[] HeadersGet(string host, string userId, string password) 
    {
      object[] results = this.Invoke("HeadersGet", new object[] {host, userId, password});
      return ((EmailHeader[])(results[0]));
    }
    
    public System.IAsyncResult BeginHeadersGet(string host, string userId, string password, System.AsyncCallback callback, object asyncState) 
    {
      return this.BeginInvoke("HeadersGet", new object[] {host, userId, password}, callback, asyncState);
    }
    
    public EmailHeader[] EndHeadersGet(System.IAsyncResult asyncResult) 
    {
      object[] results = this.EndInvoke(asyncResult);
      return ((EmailHeader[])(results[0]));
    }
    
    [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Send", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void Send(string host, string userId, string password, string to, string from, string subject, string content) 
    {
      this.Invoke("Send", new object[] {host, userId, password, to, from, subject, content});
    }
    
    public System.IAsyncResult BeginSend(string host, string userId, string password, string to, string from, string subject, string content, System.AsyncCallback callback, object asyncState) 
    {
      return this.BeginInvoke("Send", new object[] {host, userId, password, to, from, subject, content}, callback, asyncState);
    }
    
    public void EndSend(System.IAsyncResult asyncResult) 
    {
      this.EndInvoke(asyncResult);
    }
    
    [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Get", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string Get(string host, string userId, string password, int index) 
    {
      object[] results = this.Invoke("Get", new object[] {host, userId, password, index});
      return ((string)(results[0]));
    }
    
    public System.IAsyncResult BeginGet(string host, string userId, string password, int index, System.AsyncCallback callback, object asyncState) 
    {
      return this.BeginInvoke("Get", new object[] {host, userId, password, index}, callback, asyncState);
    }
    
    public string EndGet(System.IAsyncResult asyncResult) 
    {
      object[] results = this.EndInvoke(asyncResult);
      return ((string)(results[0]));
    }
    
    [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Purge", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void Purge(string host, string userId, string password, int count) 
    {
      this.Invoke("Purge", new object[] {host, userId, password, count});
    }
    
    public System.IAsyncResult BeginPurge(string host, string userId, string password, int count, System.AsyncCallback callback, object asyncState) 
    {
      return this.BeginInvoke("Purge", new object[] {host, userId, password, count}, callback, asyncState);
    }
    
    public void EndPurge(System.IAsyncResult asyncResult) 
    {
      this.EndInvoke(asyncResult);
    }
    
    [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Delete", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void Delete(string host, string userId, string password, int index) 
    {
      this.Invoke("Delete", new object[] {host, userId, password, index});
    }
    
    public System.IAsyncResult BeginDelete(string host, string userId, string password, int index, System.AsyncCallback callback, object asyncState) 
    {
      return this.BeginInvoke("Delete", new object[] {host, userId, password, index}, callback, asyncState);
    }
    
    public void EndDelete(System.IAsyncResult asyncResult) 
    {
      this.EndInvoke(asyncResult);
    }
  }

  [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://stockloan.net/")]
  public class EmailHeader 
  {
    public string From;
    public string Subject;
    public string Date;
    public bool OkToDelete;
  }
