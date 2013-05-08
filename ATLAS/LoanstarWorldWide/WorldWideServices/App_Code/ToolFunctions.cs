using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.ToolFunctions
{
    public class ToolFunctions
    {
        public enum StatusType
        {
            Accept,
            Deny
        }

        public static byte[] ConvertDataSet(DataSet dsTable)
        {
            var mStream = new MemoryStream();

            dsTable.WriteXml(mStream, XmlWriteMode.WriteSchema);

            return mStream.ToArray();
        }

        public static string GetSourceIP()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties messageProperties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpointProperty =
                messageProperties[RemoteEndpointMessageProperty.Name]
                as RemoteEndpointMessageProperty;

            string sourceAddr = endpointProperty.Address.ToString();
            return sourceAddr;
        }

        public static string EventMessage(string userId, string moduleName, string ipAddress, StatusType status)
        {
            string message = "";

            if (status == StatusType.Accept)
            {
                message = "User " + userId + " View output from " + moduleName + " from IP: " + ipAddress;
            }
            else
            {
                message = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + ipAddress;
            }

            return message;
        }
    }
}