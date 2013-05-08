using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using StockLoan.Transport;
using StockLoan.Common;

namespace StockLoan.WorldWideTradeDataService
{
    /*  
     * this file is used for loanet contract file format, peo file @ loanet     
     */

    public class Contracts
    {
        private int lineLength = 140;
        private string bizDate = "0001-01-01";
        private string clientId = "0000";
        private float fundingRate = 0F;
        
        private FileTransfer fileTransfer;
        private FileResponse fileResponse;

        private ArrayList contracts;       

        public Contracts()
        {
            contracts = new ArrayList();
            fileTransfer = new FileTransfer("");
        }

        ~Contracts()
        {

        }

        public void Load(string remotePathName, string userId, string password)
        {
            int startIndex = 0;
            ContractItem contractItem;
            int contractCount = 0;

            string c = "";

            fileResponse = fileTransfer.FileContentsGet(remotePathName, userId, password);

            Log.Write("Loading Contracts File: " + remotePathName + "; User ID: " + userId + "; Password: " + password +".", 1);

                                    
            try
            {
                while (true)
                {
                    c = fileResponse.fileContents.Substring(startIndex, lineLength);

                    if (c[0].Equals('B') || c[0].Equals('L'))
                    {
                        contractItem = new ContractItem();

                        contractItem.ContractId = c.Substring(52, 9);
                        contractItem.ContractType = c.Substring(0, 1);
                        contractItem.ContraClientId = c.Substring(1, 4);
                        contractItem.SecId = c.Substring(5, 9).Trim();
                        contractItem.Quantity = long.Parse(c.Substring(18, 9));
                        contractItem.Amount = (double)(long.Parse(c.Substring(30, 12)) / 100D);
                        contractItem.CollateralCode = c.Substring(69, 1).Trim();
                        contractItem.ValueDate = c.Substring(110, 4) + "-" + c.Substring(106, 2) + "-" + c.Substring(108, 2);
                        contractItem.SettleDate = c.Substring(48, 4) + "-" + c.Substring(44, 2) + "-" + c.Substring(46, 2);
                        contractItem.TermDate = c.Substring(74, 4) + "-" + c.Substring(70, 2) + "-" + c.Substring(72, 2);
                        contractItem.Rate = (double)(int.Parse(c.Substring(61, 5)) / 1000D);

                        if (c[68].Equals('N'))
                        {
                            contractItem.Rate = contractItem.Rate * (-1);
                        }

                        contractItem.RateCode = c.Substring(68, 1).Trim();
                        contractItem.StatusFlag = c.Substring(78, 1).Trim();
                        contractItem.PoolCode = c.Substring(79, 1).Trim();
                        contractItem.DivRate = (decimal)(int.Parse(c.Substring(80, 6)) / 1000D);
                        contractItem.DivCallable = c[86].Equals('C');
                        contractItem.IncomeTracked = !c[94].Equals('N');
                        contractItem.MarginCode = c.Substring(95, 1).Trim();

                        if (c[96].Equals(' '))
                        {
                            contractItem.Margin = 1.00D;
                        }
                        else
                        {
                            contractItem.Margin = (double)(int.Parse(c.Substring(96, 3)) / 100D);
                        }

                        if (c[99].Equals(' '))
                        {
                            contractItem.CurrencyIso = "USD";
                        }
                        else
                        {
                            contractItem.CurrencyIso = c.Substring(99, 3);
                        }

                        contractItem.SecurityDepot = c.Substring(102, 2).Trim();
                        contractItem.CashDepot = c.Substring(104, 2).Trim();
                        contractItem.OtherClientId = c.Substring(134, 4).Trim();
                        contractItem.Comment = c.Substring(114, 20).Trim();

                        contracts.Add(contractItem);
                    }
                    else if (c[0].Equals('*') && c[1].Equals('H'))
                    {
                        clientId = c.Substring(4, 4);
                        bizDate = c.Substring(12, 4) + "-" + c.Substring(8, 2) + "-" + c.Substring(10, 2);
                        fundingRate = float.Parse(c.Substring(19, 2) + "." + c.Substring(21, 3));
                    }
                    else if (c[0].Equals('*') && c[1].Equals('T'))
                    {
                        contractCount = int.Parse(c.Substring(8, 6));
                        break;
                    }
                    else
                    {
                        throw (new Exception("Format error in data file. [Position.Load]"));
                    }

                    startIndex += lineLength;
                }
            }
            catch
            {
                throw;
            }            

            if (!contractCount.Equals(contracts.Count))
            {
                throw (new Exception("Parity error: loaded " + contracts.Count + " contracts while expecting " + contractCount + ". [Position.Load]"));
            }
        }

        public ContractItem Contract(int index)
        {
            return (ContractItem)contracts[index];
        }

        public string ClientId
        {
            get
            {
                return clientId;
            }
        }

        public string BizDate
        {
            get
            {
                return bizDate;
            }
        }

        public int Count
        {
            get
            {
                return contracts.Count;
            }
        }

        public float FundingRate
        {
            get
            {
                return fundingRate;
            }
        }
    }
}
