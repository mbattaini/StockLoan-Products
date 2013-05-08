using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockLoan.ParsingFiles
{
    public class DtcMessage
    {
        public struct DtcMessageInformation
        {
            public string messageType;
            public string cusip;
            public string quantity;
            public string contraParty;
            public string settleDate;
            public string reasonCode;
            public string pendMadeFlag;
            public string method;
            public string messageCopy;
            public string message;
            public string reclaimCode;
            public bool isSuccessful;
        }
        
        public static DtcMessageInformation Parse(string message)
        {
            DtcMessageInformation tmpDtcActivity = new DtcMessageInformation();

            try
            {
                tmpDtcActivity.messageType = message.Substring(153, 3);
                tmpDtcActivity.cusip = message.Substring(106, 9);
                tmpDtcActivity.quantity = (message.Substring(165, 3).Equals("926")) ? "-" + message.Substring(519, 9) : message.Substring(519, 9);
                tmpDtcActivity.settleDate = message.Substring(260, 6);
                tmpDtcActivity.reasonCode = message.Substring(247, 3);
                tmpDtcActivity.contraParty = message.Substring(100, 4);
                tmpDtcActivity.pendMadeFlag = Status(message.Substring(258, 1));
                tmpDtcActivity.method = message.Substring(181, 1);
                tmpDtcActivity.messageCopy = message.Substring(104, 1);
                tmpDtcActivity.message = message;
                tmpDtcActivity.reclaimCode = message.Substring(165, 3);
                tmpDtcActivity.isSuccessful = true;
            }
            catch
            {
                tmpDtcActivity.isSuccessful = false;
            }

            return tmpDtcActivity;
        }

        public static string Status(string pendMadeFlag)
        {
            string flag = "";

            switch (pendMadeFlag)
            {
                case (""):
                case ("X"):
                    flag = "Made";
                    break;

                case ("P"):
                    flag = "Pending";
                    break;

                case ("C"):
                case ("D"):
                case ("K"):
                    flag = "Dropped";
                    break;

                default:
                    flag = pendMadeFlag;
                    break;
            }

            return flag;
        }
    }
}
