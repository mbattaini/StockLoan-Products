// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2003, 2004, 2005, 2006, 2007, 2008  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.ServiceModel;
using System.ServiceModel.Description;

using StockLoan.Common;


namespace StockLoan.Locates
{
    public class LocateMain
    {
        private string countryCode;

        private string dbCnStr;
        private SqlConnection connSqlDb = null;

        public ServiceHost hostLocateService = null;

        private static bool isStopped = true;
        private static string tempPath;

        public LocateMain(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;

            try
            {
                connSqlDb = new SqlConnection(dbCnStr);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [LocateMain.LocateMain]", Log.Error, 1);
            }

            countryCode = Standard.ConfigValue("CountryCode", "US");
            Log.Write("Using country code: " + countryCode + " [LocateMain.LocateMain]", 2);

            if (Standard.ConfigValueExists("TempPath"))
            {
                tempPath = Standard.ConfigValue("TempPath");

                if (!Directory.Exists(tempPath))
                {
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [LocateMain.LocateMain]", Log.Error, 1);
                    tempPath = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                Log.Write("A configuration value for TempPath has not been provided. [LocateMain.LocateMain]", Log.Information, 1);
                tempPath = Directory.GetCurrentDirectory();
            }

            Log.Write("Temporary files will be staged at " + tempPath + ". [LocateMain.LocateMain]", 2);
        }

        ~LocateMain()
        {
            if (connSqlDb != null)
            {
                connSqlDb.Dispose();
            }
        }

        public void Start()
        {
            isStopped = false;

            hostLocateService = new ServiceHost(typeof(LocateServer));

                try
                {

                    TimeSpan closeTimeout = hostLocateService.CloseTimeout;
                    TimeSpan openTimeout = hostLocateService.OpenTimeout;

                    ServiceAuthorizationBehavior authorization = hostLocateService.Authorization;
                    ServiceCredentials credentials = hostLocateService.Credentials;
                    ServiceDescription description = hostLocateService.Description;

                    int manualFlowControlLimit = hostLocateService.ManualFlowControlLimit;
                    int newLimit = hostLocateService.IncrementManualFlowControlLimit(100);

                    //NetTcpBinding bindingNetTCP = new NetTcpBinding();
                    //hostLocateService.AddServiceEndpoint(typeof(ILocatesDuplex), bindingNetTCP, "net.tcp://localhost:5000/LocatesWebService");

                    // WSDualHttpBinding bindingDualHTTP = new WSDualHttpBinding();
                    // hostLocateService.AddServiceEndpoint(typeof(ILocatesDuplex), bindingDualHTTP, "http://localhost/LocatesWebService");


                    // Open the ServiceHost to start listening for messages.
                    hostLocateService.Open();

                    // The service can now be accessed.
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();

                    // Close the hostLocateService.
                    hostLocateService.Close();
                }
                catch (TimeoutException timeProblem)
                {
                    Console.WriteLine(timeProblem.Message);
                    Console.ReadLine();
                }
                catch (CommunicationException commProblem)
                {
                    Console.WriteLine(commProblem.Message);
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }

        }

        public void Stop()
        {
            isStopped = true;

            if (hostLocateService != null)
            {
                hostLocateService.Close();
                hostLocateService = null;
            }

        }



        public static string TempPath
        {
            get
            {
                return tempPath;
            }
        }

        public static bool IsStopped
        {
            get
            {
                return isStopped;
            }
        }
    }
}

