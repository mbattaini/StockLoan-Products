using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StockLoan.Common;
using StockLoan.Transport;

namespace StockLoan.WorldWideTradeDataService
{
    public class Recalls
    {
        private FileTransfer fileTransfer;
        private FileResponse fileResponse;

        private string bizDate = "0001-01-01";
        private string clientId = "0000";
        private int lineLength = 80;

        private ArrayList recallList = null;

        public Recalls()
        {
            fileTransfer = new FileTransfer("");
            recallList = new ArrayList();
        }

        ~Recalls()
        {            
        }

        public void Load(string remotePathName, string userId, string password)
        {
            RecallItem recallItem;
            int itemCount = 0;
            int startIndex = 0;

            string c = "";

            fileResponse = fileTransfer.FileContentsGet(remotePathName, userId, password);

            Log.Write("Loading Recalls File: " + remotePathName + "; User ID: " + userId + "; Password: " + password + ".", 1);

     
            try
            {
                while (true)
                {
                    c = fileResponse.fileContents.Substring(startIndex, lineLength);

                    if (c[0].Equals('B') || c[0].Equals('L'))
                    {
                        recallItem = new RecallItem();

                        recallItem.ClientId = clientId;
                        recallItem.ContractId = c.Substring(20, 9);
                        recallItem.ContractType = c.Substring(0, 1);
                        recallItem.ContraClientId = c.Substring(1, 4);
                        recallItem.SecId = c.Substring(5, 9);
                        recallItem.Quantity = long.Parse(c.Substring(29, 9));
                        recallItem.RecallDate = "20" + c.Substring(18, 2) + "-" + c.Substring(14, 2) + "-" + c.Substring(16, 2);
                        recallItem.BuyInDate = "20" + c.Substring(42, 2) + "-" + c.Substring(38, 2) + "-" + c.Substring(40, 2);
                        recallItem.Status = c.Substring(44, 1);
                        recallItem.ReasonCode = c.Substring(45, 2);
                        recallItem.RecallId = c.Substring(47, 16);
                        recallItem.SequenceNumber = short.Parse(c.Substring(63, 6));
                        recallItem.Comment = c.Substring(69, 11).Trim();

                        recallList.Add(recallItem);
                    }
                    else if (c[0].Equals('*') && c[1].Equals('H'))
                    {
                        clientId = c.Substring(4, 4);
                        bizDate = "20" + c.Substring(12, 2) + "-" + c.Substring(8, 2) + "-" + c.Substring(10, 2);
                    }
                    else if (c[0].Equals('*') && c[1].Equals('T'))
                    {
                        itemCount += int.Parse(c.Substring(8, 6));
                        break;
                    }
                    else
                    {
                        throw (new Exception("Format error in data file. [Recalls.Load]"));
                    }

                    startIndex += lineLength;
                }
            }
            catch
            {
                throw;
            }
         

            if (!itemCount.Equals(recallList.Count))
            {
                throw (new Exception("Parity error: Loaded " + recallList.Count + " items while expecting " + itemCount + ". [Recalls.Load]"));
            }
        }

        public RecallItem RecallItem(int index)
        {
            return (RecallItem)recallList[index];
        }

        public string BizDate
        {
            get
            {
                return bizDate;
            }
        }

        public string ClientId
        {
            get
            {
                return clientId;
            }
        }
        public int Count
        {
            get
            {
                return recallList.Count;
            }
        }
    }
}
