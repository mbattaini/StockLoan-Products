using System;
using System.Collections.Generic;
using System.Text;

namespace BroadRidge.MqSeries
{
    public class UndefinedMessage
    {
        public struct UFOMessage
        {
            public string messageType;            
            public string content;
        }

        public string message;

        public UndefinedMessage(string message)
        {
            this.message = message;
        }

        public UFOMessage Parse()
        {
            UFOMessage tmpUfoMessage;

            tmpUfoMessage.messageType = message.Substring(153, 3);
            tmpUfoMessage.content = message;

            return tmpUfoMessage;
        }
    }
}
