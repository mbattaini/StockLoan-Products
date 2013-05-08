using System;
using System.Data;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI.EventBroker;
using StockLoan.Locates;

namespace StockLoan.Locates
{
    public class LocatesWebClient
    {
        public event EventHandler<LocatesUpdateEventArgs> ServerUpdated;

        InstanceContext site;
        LocatesDuplexClient clientDuplex;


        public LocatesWebClient()
        {
            CallbackHandler handlerUpdate = new CallbackHandler();
            handlerUpdate.ServerUpdated += new EventHandler<LocatesUpdateEventArgs>(handlerUpdate_InventoryUpdated);

            site = new InstanceContext(handlerUpdate);
            //clientDuplex = new LocatesDuplexClient(site, "WSDualHttpBinding_ILocatesDuplex");
            clientDuplex = new LocatesDuplexClient(site, "NetTcpBinding_ILocatesDuplex");


            ////create a unique callback address so multiple clients can run on one machine
            //WSDualHttpBinding binding = (WSDualHttpBinding)clientDuplex.Endpoint.Binding;
            //NetTcpBinding binding = (NetTcpBinding)clientDuplex.Endpoint.Binding;

            //string clientcallbackaddress = binding.ClientBaseAddress.AbsoluteUri;
            //clientcallbackaddress += Guid.NewGuid().ToString();
            //binding.ClientBaseAddress = new Uri(clientcallbackaddress);
        }


        public CommunicationState State
        {
            get { return clientDuplex.State; }
        }




        public void Subscribe()
        {
            Console.WriteLine("Subscribing");
            clientDuplex.Subscribe();
        }

        public void Unsubscribe()
        {
            Console.WriteLine("Unsubscribing");
            clientDuplex.Unsubscribe();

            //Closing the client gracefully closes the connection and cleans up resources
            clientDuplex.Close();
        }

        public string EchoData(int DataValue)
        {
            Console.WriteLine("Get Data");
            return clientDuplex.EchoData(DataValue);
        }

        public string SubmitLocateList(string clientId, string groupCode, string clientComment, string locateList)
        {
            string strReturn = "";
            clientDuplex.SubmitLocateList(clientId, groupCode, clientComment, locateList);
            return strReturn;
        }


        public string BizDate()
        {
            return clientDuplex.BizDate();
        }
        public string BizDateExchange()
        {
            return clientDuplex.BizDateExchange();
        }
        public DataSet TradeDates()
        {
            return clientDuplex.TradeDates();
        }
        public DataSet TradingGroups()
        {
            return clientDuplex.TradingGroups();
        }
        public DataSet Locates(short OffsetUTC, string LocateId, string TradeDateMin, string TradeDateMax, string GroupCode, string SecId, string Status)
        {
            return clientDuplex.Locates(OffsetUTC, LocateId, TradeDateMin, TradeDateMax, GroupCode, SecId, Status);
        }
        public DataSet Inventory(string GroupCode, string SecId, short OffsetUtc)
        {
            return clientDuplex.Inventory(GroupCode, SecId, OffsetUtc);
        }

        public void LocatePreBorrowSubmit(long LocateId, string GroupCode, string SecId, string Quantity, string Rate, string ActUserId)
        {
            clientDuplex.LocatePreBorrowSubmit(LocateId, GroupCode, SecId, Quantity, Rate, ActUserId);
        }
        public void LocateItemSet(long LocateId, string Quantity, string Source, string FeeRate, string PreBorrow, string Comment, string ActUserId)
        {
            clientDuplex.LocateItemSet(LocateId, Quantity, Source, FeeRate, PreBorrow, Comment, ActUserId);
        }
        public DataSet LocateItemGet(string locateId, short utcOffset)
        {
            return clientDuplex.LocateItemGet(locateId, utcOffset);
        }

        public void LocatesPreBorrowSet(string bizDate, string groupCode, string secId, string quantity, string rebateRate, string actUserId)
        {
            clientDuplex.LocatesPreBorrowSet(bizDate, groupCode, secId, quantity, rebateRate, actUserId);
        }
        public DataSet LocatesPreBorrowGet(string bizDate, string groupCode, short utcOffset)
        {
            return clientDuplex.LocatesPreBorrowGet(bizDate, groupCode, utcOffset);
        }



        public void PublishInventoryUpdate(string Desk)
        {
            if ((CommunicationState.Created == clientDuplex.State) || (CommunicationState.Opened == clientDuplex.State))
            {
                clientDuplex.PublishInventoryUpdate(Desk);
            }
        }



        void handlerUpdate_InventoryUpdated(object sender, LocatesUpdateEventArgs e)
        {
            OnInventoryUpdated(e);
        }
        protected virtual void OnInventoryUpdated(LocatesUpdateEventArgs eventArgs)
        {
            if (ServerUpdated != null)
            {
                ServerUpdated(this, eventArgs);
            }
        }




    }


    public class CallbackHandler : ILocatesDuplexCallback
    {
        public event EventHandler<LocatesUpdateEventArgs> ServerUpdated;

        public void RecieveInventoryUpdate(RecieveInventoryUpdate request)
        {
            RecieveInventoryUpdate(request.Desk);
        }
        public void RecieveInventoryUpdate(string Desk)
        {
            string strResult = string.Format("Update From ({0})", Desk);
            Console.WriteLine(strResult);
            LocatesUpdateEventArgs argsUpdate = new LocatesUpdateEventArgs();
            argsUpdate.Message = Desk;
            ServerUpdated(this, argsUpdate);
        }
    }


    public class LocatesUpdateEventArgs : EventArgs
    {
        private string strMessage = "";

        public string Message
        {
            get { return strMessage; }
            set { strMessage = value; }
        }
    }

}
