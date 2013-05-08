using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using Anetics.Common;

namespace Anetics.Ares   
{
    public class AvailabilityFileFormat
    {
        public AvailabilityFileFormat()
        {
        }

        public string Create(string bizDate, DataSet dsAvail)
        {
            string body = "";
            int counter = 0;

            try
            {
                body = "0," + DateTime.Parse(bizDate).ToString("yyyyMMdd") + ",\r\n";

                foreach (DataRow dr in dsAvail.Tables["Availability"].Rows)
                {
                    if ((long.Parse(dr["Available"].ToString()) > 0) && (dr["BookGroup"].ToString().Equals("0158")))    //original "0234", changed to "0158" 
                    {
                        body += "2," + dr["SecId"].ToString().Trim() + "," + dr["Available"].ToString() + ",\r\n";
                        counter++;
                    }
                }

                body += "9," + DateTime.Parse(bizDate).ToString("yyyyMMdd") + "," + counter.ToString() + ",\r\n";
            }
            catch (Exception error)
            {
                body = error.Message;
                Log.Write(error.Message + "  [AvailabilityFileFormat.Create]", Log.Error, 1);	//DC
				throw;																			//DC
            }

            return body;
        }
    }
}
