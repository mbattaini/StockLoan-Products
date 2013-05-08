using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;

using StockLoan.Common;
using StockLoan.Business;
using StockLoan.WebServices;

namespace StockLoan.WebServices.AdminService
{
    public partial class AdminService : IAdminService
    {                
        public byte[] CountriesGet(string countryCode, string userId, string userPassword, string bookGroup, string functionPath)
        {             
            DataSet dsTemp = new DataSet();
            
            try
            {
                string sourceAddr = ToolFunctions.ToolFunctions.GetSourceIP();
                
                if (Business.Security.mayView(userId, userPassword, bookGroup, functionPath)) 
                {
                    dsTemp = Admin.CountriesGet(countryCode);
                    
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                }
            
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);            
            }
            catch
            {
                throw;
            }
        }

        public byte[] CountryCodeIsoConversionsGet(string userId,  string userPassword, string bookGroup, string functionPath)
        {
             DataSet dsTemp = new DataSet();
            
            try
            {                
                if (Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Admin.CountryCodeIsoConversionsGet();
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                }
                
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool CountrySet(string countryCode, string country, string settleDays, bool isActive,
                        string userId,  string userPassword, string bookGroup, string functionPath)
        {
            bool blnResults = false;
            
            try
            {
                if (Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Admin.CountrySet(countryCode, country, settleDays, isActive);
                    
                    blnResults = true;

                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                }

                return blnResults;
            }
            catch
            {
                return blnResults;
                throw;
            }
        }

        public byte[] CurrenciesGet(string currencyIso, string userId,  string userPassword, string bookGroup, string functionPath)
        {
           DataSet dsTemp = new DataSet();
            
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Admin.CurrenciesGet(currencyIso);

                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                }
      
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool CurrencyConversionSet(string currencyIsoFrom, string currencyIsoTo, string currencyConvertRate,
                                    string userId,  string userPassword, string bookGroup, string functionPath)
        {
  
            bool blnResults = false;
            
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Admin.currencyConversionSet(currencyIsoFrom, currencyIsoTo, currencyConvertRate);
                    blnResults = true;

                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                }

                return blnResults;
            }
            catch
            {
                return blnResults;
                throw;
            }
        }

        public byte[] CurrencyConversionsGet(string currencyIsoFrom, string userId,  string userPassword, string bookGroup, string functionPath)
        {
            
            DataSet dsTemp = new DataSet();
            try
            {
            
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Admin.CurrencyConversionsGet(currencyIsoFrom);

                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                } 
                
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool CurrencySet(string currencyIso, string currency, bool isActive, string userId,  string userPassword, string bookGroup, string functionPath)
        {
           
            bool blnResults = false;
            try
            {
           
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Admin.CurrencySet(currencyIso, currency, isActive);
                    blnResults = true;

                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                }

                return blnResults;
            }
            catch
            {
                return blnResults;
                throw;
            }
        }

        public byte[] DeliveryTypesGet(string userId,  string userPassword, string bookGroup, string functionPath)
        { 
            DataSet dsTemp = new DataSet();
            try
            {
         
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Admin.DeliveryTypesGet();

                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Accept), System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    Log.Write(ToolFunctions.ToolFunctions.EventMessage(userId, MethodInfo.GetCurrentMethod().Name, ToolFunctions.ToolFunctions.GetSourceIP(), ToolFunctions.ToolFunctions.StatusType.Deny), System.Diagnostics.EventLogEntryType.Error);
                }

                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }
    }
}
