using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFFunctions
{
    public class Functions
    {
        public static byte[] ConvertDataSet(DataSet dsTable)
        {
            var mStream = new MemoryStream();

            dsTable.WriteXml(mStream, XmlWriteMode.WriteSchema);

            return mStream.ToArray();
        }
    }
}
