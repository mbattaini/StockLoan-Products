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
[System.Web.Services.WebServiceBindingAttribute(Name="CurrenciesSoap", Namespace="http://stockloan.net/")]
public class Currencies : System.Web.Services.Protocols.SoapHttpClientProtocol 
{
  public Anetics.Services.AuthHeader AuthHeader;
    
  public Currencies(Anetics.Services.AuthHeader authHeader, string uri) 
  {
    this.Url = uri + "currencies.asmx";
    this.AuthHeader = authHeader;
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Get", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
  public CurrencyItem[] Get() 
  {
    object[] results = this.Invoke("Get", new object[0]);
    return ((CurrencyItem[])(results[0]));
  }
    
  public System.IAsyncResult BeginGet(System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Get", new object[0], callback, asyncState);
  }
    
  public CurrencyItem[] EndGet(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
    return ((CurrencyItem[])(results[0]));
  }
}

[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://stockloan.net/")]
public class CurrencyItem
{
  public string CurrencyCode;
  public string Currency;
  public bool IsActive;
}