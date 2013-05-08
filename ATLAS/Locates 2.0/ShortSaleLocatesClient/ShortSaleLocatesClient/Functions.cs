using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;

namespace ShortSaleLocatesClient
{
    public class Functions
    {

        public static DataTable ConvertToDataTable(byte[] e, string tableName)
        {
            DataSet dsTemp = new DataSet();

            var ms = new System.IO.MemoryStream(e);
            dsTemp = new DataSet();
            dsTemp.ReadXml(ms);

            return dsTemp.Tables[tableName];
        }
    }
}
