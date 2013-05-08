using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using StockLoan.Common;
using IBM.WMQ;

namespace BroadRidge.MqSeries
{
    public class DtcMqActivity
    {
        public struct MessageObject
        {
            public string messageType;
            public object content;
        }

        private MQQueueManager queueManager;
        private MQQueue queue;
        private MQMessage message;
        private DataSet dsContent = new DataSet();

        private string dbCnStr;

        public DtcMqActivity(string dbCnStr)
        {        
            this.dbCnStr = dbCnStr;

            dsContent.Tables.Add("Identified");
            dsContent.Tables["Identified"].Columns.Add("MessageType");
            dsContent.Tables["Identified"].Columns.Add("Cusip");
            dsContent.Tables["Identified"].Columns.Add("Quantity");
            dsContent.Tables["Identified"].Columns.Add("ContraParty");
            dsContent.Tables["Identified"].Columns.Add("SettleDate");
            dsContent.Tables["Identified"].Columns.Add("ReasonCode");
            dsContent.Tables["Identified"].Columns.Add("PendMadeFlag");
            dsContent.Tables["Identified"].Columns.Add("Method");
            dsContent.Tables["Identified"].Columns.Add("MessageCopy");        
            dsContent.AcceptChanges();

            MQEnvironment.Hostname = Standard.ConfigValue("MQHostName");
            MQEnvironment.Channel = Standard.ConfigValue("MQChannel");

            MQEnvironment.UserId = Standard.ConfigValue("MQUserId");
            MQEnvironment.Password = Standard.ConfigValue("MQPassword");
            MQEnvironment.Port = int.Parse(Standard.ConfigValue("MQPort"));

            string mqQueueManagerName = Standard.ConfigValue("MQQueueMangerName");
            string mqQueueName = Standard.ConfigValue("MQQueueName");

            int openOptions = MQC.MQOO_INPUT_AS_Q_DEF;

            try
            {
                queueManager = new MQQueueManager(mqQueueManagerName);
                queue = queueManager.AccessQueue(mqQueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_BROWSE + MQC.MQOO_FAIL_IF_QUIESCING);              
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

        public void DtcRunCycle()
        {
            MessageObject msgObject;
            string message = "";
           
            while (true)
            {
                try
                {
                    msgObject = PullMessage();

                    switch (msgObject.messageType)
                    {
                        case "DTC":
                            message = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).message;

                            DataRow tempRow = dsContent.Tables["Identified"].NewRow();
                            tempRow["MessageType"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).messageType;
                            tempRow["Cusip"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).cusip;
                            tempRow["Quantity"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).quantity;
                            tempRow["ContraParty"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).contraParty;
                            tempRow["SettleDate"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).settleDate;
                            tempRow["ReasonCode"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).reasonCode;
                            tempRow["PendMadeFlag"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).pendMadeFlag;
                            tempRow["Method"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).method;
                            tempRow["MessageCopy"] = ((DtcDeliveryOutputMessage.DtcActivity)msgObject.content).messageCopy;
                            dsContent.Tables["Identified"].Rows.Add(tempRow);
                            dsContent.AcceptChanges();

                            DatabaseFunctions.SenderoDatabaseFunctions.DtcActivityInsert(tempRow["MessageType"].ToString(),
                            tempRow["Cusip"].ToString(),
                            tempRow["Quantity"].ToString(),
                            tempRow["ContraParty"].ToString(),
                            DateTime.ParseExact(tempRow["SettleDate"].ToString(), "MMddyy", null).ToString("yyyy-MM-dd"),
                            tempRow["ReasonCode"].ToString(),
                            tempRow["PendMadeFlag"].ToString(),
                            tempRow["Method"].ToString(),
                            tempRow["MessageCopy"].ToString(),
                            dbCnStr);

                            break;

                        case "None":
                            break;

                        default:
                            message = ((UndefinedMessage.UFOMessage)msgObject.content).content;
                            break;
                    }

                    Log.Write("DTC Message: " + message, 1);
                }
                catch (MQException error)
                {
                    Log.Write(error.Message + "[" + error.ReasonCode + "]", 1);
                    throw;
                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);
                    throw;
                }
            }          
        }


        private MessageObject PullMessage()
        {
            MessageObject msgObject;

            MQGetMessageOptions messageOptions = new MQGetMessageOptions();
            messageOptions.Options = MQC.MQGMO_NO_SYNCPOINT;

            message = new MQMessage();
            message.Format = MQC.MQFMT_STRING;

            queue.Get(message, messageOptions);

            string temp = message.ReadString(message.MessageLength);

            if (temp.Substring(0, 1).Equals("A"))
            {
                switch (temp.Substring(153, 3))
                {
                    case ("CNS"):
                    case ("DOX"):
                    case ("DOA"):
                    case ("PRT"):
                    case ("MDS"):
                    case ("CFS"):
                    case ("NDO"):
                    case ("DOS"):
                    case ("MDH"):
                    case ("ATP"):
                    case ("SYN"):
                    case ("MDD"):
                    case ("MID"):
                        DtcDeliveryOutputMessage dtcMessage = new DtcDeliveryOutputMessage(temp);
                        msgObject.messageType = "DTC";
                        msgObject.content = dtcMessage.Parse();
                        break;

                    default:
                        UndefinedMessage ufo = new UndefinedMessage(temp);
                        msgObject.messageType = "NEW";
                        msgObject.content = ufo.Parse();
                        break;
                }
            }
            else
            {
                msgObject.messageType = "None";
                msgObject.content = null;
            }

            return msgObject;
        }
    }
}
