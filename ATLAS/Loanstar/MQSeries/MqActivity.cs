using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

using StockLoan.Common;

using IBM.WMQ;

namespace StockLoan.MqSeries
{
    public class MqActivity
    {       
        private MQQueueManager queueManager;
        private MQQueue queueResponse;        
        private MQMessage message;
        private DataSet dsContent = new DataSet();

        private string dbCnStr;

        public MqActivity(string dbCnStr)
        {        
            MQEnvironment.Hostname = Standard.ConfigValue("MQHostName");
            MQEnvironment.Channel = Standard.ConfigValue("MQChannel");
            MQEnvironment.Port = int.Parse(Standard.ConfigValue("MQPort"));

            if (!Standard.ConfigValue("MQUserId").Equals(""))
            {
                MQEnvironment.UserId = Standard.ConfigValue("MQUserId");
                MQEnvironment.Password = Standard.ConfigValue("MQPassword");
            }

            string mqQueueManagerName = Standard.ConfigValue("MQQueueMangerName");            
            string mqQueueNameResponse = Standard.ConfigValue("MQQueueName");

            int openOptions = MQC.MQOO_INPUT_AS_Q_DEF;

            try
            {
                queueManager = new MQQueueManager(mqQueueManagerName);
                
                queueResponse = queueManager.AccessQueue(mqQueueNameResponse, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);                              
            }
            catch (MQException error)
            {
                Log.Write(error.Message + "[" + error.ReasonCode + "]", 1);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, 1);
            }
        }


        public string PeekMessage()
        {
            MQGetMessageOptions messageOptions = new MQGetMessageOptions();
            messageOptions.Options = MQC.MQGMO_NO_SYNCPOINT + MQC.MQGMO_BROWSE_FIRST + MQC.MQGMO_FAIL_IF_QUIESCING + MQC.MQGMO_WAIT;

            message = new MQMessage();
            message.Format = MQC.MQFMT_STRING;

            try
            {
                queueResponse.Get(message, messageOptions);
            }
            catch (IBM.WMQ.MQException exception)
            {
                throw new Exception(MQReasonCodeDescription(exception.ReasonCode, exception.Message));
            }

            return message.ReadString(message.MessageLength);
        }

        public string PullMessage()
        {            
            MQGetMessageOptions messageOptions = new MQGetMessageOptions();
            messageOptions.Options = MQC.MQGMO_NO_SYNCPOINT;

            message = new MQMessage();
            message.Format = MQC.MQFMT_STRING;

            try
            {                
                queueResponse.Get(message, messageOptions);
            }
            catch (IBM.WMQ.MQException exception)
            {
                throw new Exception(MQReasonCodeDescription(exception.ReasonCode, exception.Message));
            }

            return message.ReadString(message.MessageLength);
        }

        private string MQReasonCodeDescription(int reasonCode, string message)
        {
            string _message = "";

            switch (reasonCode)
            {
                case 2033:
                    _message = "No new message";
                    break;

                default:
                    _message = message;
                    break;
            }

            return _message;
        }

        public void MqActivityClose()
        {
            if (queueManager.IsOpen || queueManager.IsConnected)
            {
                queueManager.Close();
                queueManager.Disconnect();                
            }
        }
    }
}
