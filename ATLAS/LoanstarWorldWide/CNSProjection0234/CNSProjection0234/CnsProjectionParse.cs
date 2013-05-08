using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNSProjection0234
{
    public class CnsProjectionParse
    {
        private string fileName;
        private string dbCnStr;
      

        public CnsProjectionParse(string fileName, string dbCnStr)
        {
            this.fileName = fileName;
            this.dbCnStr = dbCnStr;            
        }

        public DataSet Load()
        {
            bool ignore = false;

            string []  ignoreItems = {"PROJECTION","PROJECTED", "PROCESS DATE","FOR SETTLEMENT ON:","PARTICIPANT","SUB-ACCOUNT", "----- T O M O R R O W ", "SETTLING","TRADES","PURCAHSED/","SOLD(-)", "LONG/"};

            DataSet dsCns = new DataSet();
            dsCns.Tables.Add("Cns");
            dsCns.Tables["Cns"].Columns.Add("SecId");
            dsCns.Tables["Cns"].Columns.Add("Quantity");


            string [] cnsItems = File.ReadAllLines(fileName);

            foreach (string cnsItem in cnsItems)
            {
                ignore = false;

                foreach (string ignoreItem in ignoreItems)
                {
                    if ((cnsItem.Contains(ignoreItem)) || cnsItem[0] == '1')
                    {
                        ignore = true;                        
                    }
                }

                if (!ignore)
                {
                    if (cnsItem.Trim().Equals(""))
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            if (!cnsItem.Substring(46, 15).Trim().Equals(""))
                            {
                                DataRow drTemp = dsCns.Tables["Cns"].NewRow();
                                drTemp["SecId"] = cnsItem.Substring(90, 9);
                                drTemp["Quantity"] = long.Parse(cnsItem.Substring(80, 1) + cnsItem.Substring(46, 15).Replace(",", "").Trim());
                                dsCns.Tables["Cns"].Rows.Add(drTemp);
                                dsCns.AcceptChanges();


                                /*DataRow drTemp = dsCns.Tables["Cns"].NewRow();
                                drTemp["SecId"] = cnsItem.Substring(90, 9);
                                drTemp["Quantity"] = long.Parse(cnsItem.Substring(16, 1) + cnsItem.Substring(2, 14).Replace(",", "").Trim());
                                dsCns.Tables["Cns"].Rows.Add(drTemp);
                                dsCns.AcceptChanges();*/
                            }
                        }
                        catch
                        {
                            Console.WriteLine(cnsItem);
                        }
                    }
                }
            }

            foreach (DataRow drRow in dsCns.Tables["Cns"].Rows)
            {
                ItemSet(DateTime.Now.ToString("yyyy-MM-dd"), "0234", drRow["SecId"].ToString(), drRow["Quantity"].ToString());
            }


            return dsCns;
        }

        public void ItemSet(string bizDate, string bookGroup, string secId, string quantity)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBoxPositionProjectedClearingFailItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }
    }
}
