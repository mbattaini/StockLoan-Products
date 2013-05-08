using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsApplicationMQ
{
    public class DtcDeliveryOutputMessage
    {
        public struct DtcActivity
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
        }
        
        private string message;

        public DtcDeliveryOutputMessage(string message)
        {
            this.message = message;               
        }

        public DtcActivity Parse()
        {
            DtcActivity tmpDtcActivity;

            tmpDtcActivity.messageType = message.Substring(153, 3);
            tmpDtcActivity.cusip = message.Substring(106, 9);
            tmpDtcActivity.quantity = (message.Substring(165, 3).Equals("926")) ? "-" + message.Substring(519, 9):message.Substring(519, 9);
            tmpDtcActivity.settleDate = message.Substring(260, 6);
            tmpDtcActivity.reasonCode = message.Substring(247, 3);
            tmpDtcActivity.contraParty = message.Substring(100, 4);
            tmpDtcActivity.pendMadeFlag = Functions.Status(message.Substring(258, 1));
            tmpDtcActivity.method = message.Substring(181, 1);
            tmpDtcActivity.messageCopy = message.Substring(104, 1);
            tmpDtcActivity.message = message;
            tmpDtcActivity.reclaimCode = message.Substring(165, 3);

            return tmpDtcActivity;
        }
    }
}
