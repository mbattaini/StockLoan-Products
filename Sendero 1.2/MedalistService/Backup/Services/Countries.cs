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
[System.Web.Services.WebServiceBindingAttribute(Name="CountriesSoap", Namespace="http://stockloan.net/")]
public class Countries : System.Web.Services.Protocols.SoapHttpClientProtocol 
{
  public Anetics.Services.AuthHeader AuthHeader;
    
  public Countries(Anetics.Services.AuthHeader authHeader, string uri) 
  {
    this.Url = uri + "countries.asmx";
    this.AuthHeader = authHeader;
  }
    
  [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeader")]
  [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stockloan.net/Get", RequestNamespace="http://stockloan.net/", ResponseNamespace="http://stockloan.net/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
  public CountryItem[] Get() 
  {
    object[] results = this.Invoke("Get", new object[0]);
    return ((CountryItem[])(results[0]));
  }
    
  public System.IAsyncResult BeginGet(System.AsyncCallback callback, object asyncState) 
  {
    return this.BeginInvoke("Get", new object[0], callback, asyncState);
  }
    
  public CountryItem[] EndGet(System.IAsyncResult asyncResult) 
  {
    object[] results = this.EndInvoke(asyncResult);
    return ((CountryItem[])(results[0]));
  }
}

[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://stockloan.net/")]
public class CountryItem
{
  public string CountryCode;
  public string Country;
  public string SettleTerm;
  public bool IsActive;
}
