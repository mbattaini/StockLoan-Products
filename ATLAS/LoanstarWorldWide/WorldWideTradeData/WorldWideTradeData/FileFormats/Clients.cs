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
    public class Clients
    {
        private FileTransfer fileTransfer;
        private FileResponse fileResponse;

        private string bizDate = "0001-01-01";
        private string clientId = "0000";

        private int lineLength = 540;

        private ArrayList clientList;

        public Clients()
        {
            clientList = new ArrayList();
            fileTransfer = new FileTransfer("");
        }

        ~Clients()
        {
        }

        public void Load(string remotePathName, string userId, string password)
        {
            ClientItem clientLongItem = new ClientItem();
            int itemCount = 0;
            int startIndex = 0;

            string c = "";

            fileResponse = fileTransfer.FileContentsGet(remotePathName, userId, password);

            Log.Write("Loading Clients File: " + remotePathName + "; User ID: " + userId + "; Password: " + password + ".", 1);

            try
            {
                while (true)
                {
                    c = fileResponse.fileContents.Substring(startIndex, lineLength);

                    if (c[0].Equals('1'))
                    {
                        clientId = c.Substring(1, 4);
                        bizDate = c.Substring(5, 4) + "-" + c.Substring(9, 2) + "-" + c.Substring(11, 2);
                    }
                    else if (c[0].Equals('9'))
                    {
                        itemCount = int.Parse(c.Substring(5, 9));
                        break;
                    }
                    else if (c[0].Equals('2'))
                    {
                        clientLongItem = new ClientItem();

                        clientLongItem.ContraClientId = c.Substring(1, 4); 
                         clientLongItem.AccountName = c.Substring(5, 30).Trim();
                        clientLongItem.AddressLine1 = c.Substring(35, 30).Trim();
                        clientLongItem.AddressLine2 = c.Substring(65, 30).Trim();
                        clientLongItem.AddressLine3 = c.Substring(95, 30).Trim();
                        clientLongItem.AddressLine4 = c.Substring(125, 30).Trim();
                        clientLongItem.AddressLine5 = c.Substring(155, 30).Trim();

                        clientLongItem.Phone = c.Substring(185, 14);
                        clientLongItem.TaxId = c.Substring(199, 9).Trim();
                        clientLongItem.ContraClientDtc = c.Substring(208, 4);
                        clientLongItem.ThirdPartyInstructions = c.Substring(213, 17).Trim();
                        clientLongItem.DeliveryInstructions = c.Substring(229, 30).Trim();
                        clientLongItem.MarkDtc = c.Substring(259, 4);
                        clientLongItem.MarkInstructions = c.Substring(263, 30).Trim();
                        clientLongItem.RecallDtc = c.Substring(293, 5);
                        clientLongItem.CdxCuId = c.Substring(298, 4);

                        clientLongItem.OccDelivery = c.Substring(302, 1);

                        clientLongItem.ParentAccount = c.Substring(303, 4);
                        clientLongItem.AssociatedAccount = c.Substring(307, 4);
                        clientLongItem.CreditLimitAccount = c.Substring(311, 4);
                        clientLongItem.AssociatedCbAccount = c.Substring(315, 4);

                        clientLongItem.BorrowLimit = c.Substring(319, 14);
                        clientLongItem.BorrowDateChange = c.Substring(333, 8);
                        clientLongItem.BorrowSecLimit = c.Substring(341, 14);
                        clientLongItem.BorrowSecDateChange = c.Substring(355, 8);
                        clientLongItem.LoanLimit = c.Substring(363, 14);
                        clientLongItem.LoanDateChange = c.Substring(377, 8);
                        clientLongItem.LoanSecLimit = c.Substring(385, 14);
                        clientLongItem.LoanSecDateChange = c.Substring(399, 8);

                        clientLongItem.MinMarkAmount = c.Substring(407, 4);
                        clientLongItem.MinMarkPrice = c.Substring(411, 1);
                        clientLongItem.MarkRoundHouse = c.Substring(412, 1);
                        clientLongItem.MarkValueHouse = c.Substring(413, 4);
                        clientLongItem.MarkRoundInstitution = c.Substring(417, 1);
                        clientLongItem.MarkValueInstitution = c.Substring(418, 4);

                        clientLongItem.BorrowMarkCode = c.Substring(422, 1);
                        clientLongItem.BorrowCollateral = c.Substring(423, 3);
                        clientLongItem.LoanMarkCode = c.Substring(426, 1);
                        clientLongItem.LoanCollateral = c.Substring(427, 3);

                        clientLongItem.IncludeAccrued = c.Substring(430, 1);

                        clientLongItem.StandardMark = c.Substring(431, 1);
                        clientLongItem.OmnibusMark = c.Substring(432, 1);

                        clientLongItem.StockBorrowRate = c.Substring(433, 5);
                        clientLongItem.StockLoanRate = c.Substring(438, 5);
                        clientLongItem.BondBorrowRate = c.Substring(443, 5);
                        clientLongItem.BondLoanRate = c.Substring(448, 5);

                        clientLongItem.BusinessIndex = c.Substring(453, 2);
                        clientLongItem.BusinessAmount = c.Substring(455, 18);
                        clientLongItem.InstitutionalCashPool = c.Substring(473, 1);
                        clientLongItem.InstitutionalFeeType = c.Substring(474, 1);
                        clientLongItem.InstitutionalFeeRate = c.Substring(475, 5);

                        clientLongItem.LoanEquity = c.Substring(480, 1);
                        clientLongItem.LoanDebt = c.Substring(481, 1);
                        clientLongItem.ReturnEquity = c.Substring(482, 1);
                        clientLongItem.ReturnDebt = c.Substring(483, 1);
                        clientLongItem.IncomeEquity = c.Substring(484, 1);
                        clientLongItem.IncomeDebt = c.Substring(485, 1);

                        clientLongItem.DayBasis = c.Substring(486, 1);
                        clientLongItem.AccountClass = c.Substring(487, 3);

                        clientList.Add(clientLongItem);
                    }
                    else
                    {
                        throw (new Exception("Format error in data file. [Clients.Load]"));
                    }

                    startIndex += lineLength;
                }
            }
            catch
            {
                throw;
            }

            if (!itemCount.Equals(clientList.Count))
            {
                throw (new Exception("Parity error: Loaded " + clientList.Count + " items while expecting " + itemCount + ". [Clients.Load]"));
            }
        }

        public ClientItem ClientItem(int index)
        {
            return (ClientItem)clientList[index];
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
                return clientList.Count;
            }
        }
    }
}
