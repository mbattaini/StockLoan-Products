﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace StockLoan.WebServices.ToolFunctions
{
    public class ToolFunctions
    {
        public static byte[] ConvertDataSet(DataSet dsTable)
        {               
            var mStream = new MemoryStream();

            dsTable.WriteXml(mStream, XmlWriteMode.WriteSchema);

            return mStream.ToArray();           
        }    
    }
}