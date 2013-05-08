using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using C1.Silverlight.Data;

namespace StockLoan_LocatesClient
{
	public class Functions
	{
	   public static C1.Silverlight.Data.DataTable ConvertToDataTable(byte[] e, string tableName)
        {
            DataSet dsTemp = new DataSet();

            var ms = new System.IO.MemoryStream(e);
            dsTemp = new DataSet();
            dsTemp.ReadXml(ms);

            return dsTemp.Tables[tableName];
        }

       public static C1.Silverlight.Data.DataTable ConvertToDataTable(byte[] e, int index)
       {
           DataSet dsTemp = new DataSet();

           var ms = new System.IO.MemoryStream(e);
           dsTemp = new DataSet();
           dsTemp.ReadXml(ms);

           return dsTemp.Tables[index];
       }		
	}
}


