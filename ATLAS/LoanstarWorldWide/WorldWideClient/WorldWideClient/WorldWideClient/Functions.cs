using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using C1.Silverlight;
using C1.Silverlight.Data;

namespace WorldWideClient
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