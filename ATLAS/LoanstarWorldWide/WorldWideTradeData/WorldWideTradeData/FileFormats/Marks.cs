using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StockLoan.Common;
using StockLoan.Transport;

namespace StockLoan.WorldWideTradeDataService
{
    public class Marks
    {
        private FileTransfer fileTransfer;
        private FileResponse fileResponse;
           
        private string clientId = "0000";
        private string bizDate = "";

        private int lineLength = 80;

        private ArrayList markList = null;

        public Marks()
        {
            fileTransfer = new FileTransfer("");
            markList = new ArrayList();
        }

        ~Marks()
        {           
        }

        public void Load(string remotePathName, string userId, string password)
        {
            MarkItem markItem;
            int itemCount = 0;
            int startIndex = 0;

            string c = "";

            fileResponse = fileTransfer.FileContentsGet(remotePathName, userId, password);

            Log.Write("Loading Marks File: " + remotePathName + "; User ID: " + userId + "; Password: " + password + ".", 1);
           
            try
            {
                while (true)
                {
                    c = fileResponse.fileContents.Substring(startIndex, lineLength);

                    if (c[0].Equals('B') || c[0].Equals('L'))
                    {
                        markItem = new MarkItem();

                        markItem.ClientId = clientId;
                        markItem.ContractId = c.Substring(1, 9);
                        markItem.ContractType = c.Substring(0, 1);
                        markItem.Amount = decimal.Parse(c.Substring(10, 12)) / 100;
                        markItem.Direction = c.Substring(22, 1);

                        markList.Add(markItem);
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
                        throw (new Exception("Format error in data file. [Marks.Load]"));
                    }

                    startIndex += lineLength;
                }
            }
            catch
            {
                throw;
            }
        

            if (!itemCount.Equals(markList.Count))
            {
                throw (new Exception("Parity error: Loaded " + markList.Count + " items while expecting " + itemCount + ". [Marks.Load]"));
            }
        }

        public MarkItem MarkItem(int index)
        {
            return (MarkItem)markList[index];
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
                return markList.Count;
            }
        }
    }
}

