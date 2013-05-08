using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNSProjection0234
{
    public class CnsProjectionDBLoad
    {
        private string fileName;
        private string dbCnStr;
        private string dbCnStrExtended;

        public CnsProjectionDBLoad(string dbCnStr, string dbCnStrExtended)
        {            
            this.dbCnStr = dbCnStr;
            this.dbCnStrExtended = dbCnStrExtended;
        }

        public DataSet Load()
        {
            DataSet dsTemp = ItemsGet("0158", DateTime.Now.ToString("yyyy-MM-dd"));

            foreach (DataRow drRow in dsTemp.Tables["Items"].Rows)
            {
                ItemSet(DateTime.Now.ToString("yyyy-MM-dd"), drRow["BooKGroup"].ToString(), drRow["SecId"].ToString(), drRow["ClearingFTDTraded"].ToString());
            }

            return dsTemp;
        }

        public DataSet ItemsGet(string bookGroup, string bizDate)
        {
            DataSet dsTemp = new DataSet();
            SqlConnection dbCn = new SqlConnection(dbCnStrExtended);

            string sql ="use loanstar\r\n" +
                        "select	BizDate,\r\n" +
		                "        BookGroup,\r\n" +
		                "        SecId,\r\n" +
                        "        (ClearingFTDTraded * -1) As ClearingFTDTraded\r\n" +
                        "from	dbo.tbboxfails\r\n" +
                        "where	bizdate = '" + bizDate +"'\r\n" +
                        "and		bookgroup = '0158'\r\n" +
                        "and		ClearingFTDTraded <> 0";

            try
            {
                SqlCommand dbCmd = new SqlCommand(sql, dbCn);
                dbCmd.CommandType = CommandType.Text;

                SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
                dAdapter.Fill(dsTemp, "Items");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            return dsTemp;
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
