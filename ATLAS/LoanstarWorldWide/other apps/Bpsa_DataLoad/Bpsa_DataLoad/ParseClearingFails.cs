using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bpsa_DataLoad
{
    class ParseClearingFails
    {
        private DataSet dsFails;
        private string dbCnStr;

        public ParseClearingFails()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = "Zeus";
            sqlBuilder.InitialCatalog = "Sendero";
            sqlBuilder.IntegratedSecurity = true;

            dbCnStr = sqlBuilder.ConnectionString;      
        }

        public void Load(string filePath, string bizDatePrior, string bizDate)
        {
            BoxFailsPurge(bizDate);

            TextReader textReader = new StreamReader(filePath);
            
            string line = "";
            string sign = "";            
            decimal quantity;

            dsFails = new DataSet();
            dsFails.Tables.Add("Fails");
            dsFails.Tables["Fails"].Columns.Add("SecId");            
            dsFails.Tables["Fails"].Columns.Add("ClearingFTDSettled");
            dsFails.Tables["Fails"].Columns.Add("ClearingFTDTraded");
            dsFails.Tables["Fails"].Columns.Add("ClearingFTRSettled");
            dsFails.Tables["Fails"].Columns.Add("ClearingFTRTraded");
            dsFails.AcceptChanges();

            line = textReader.ReadLine();

            if (!CheckFileDate( line.Substring(32, 10), bizDate))
            {
                Console.WriteLine("File is not for today.");
                return;
            }

            while (true)
            {
                line = textReader.ReadLine();
                if (line != null)
                {
                    if (line[0].Equals('D'))
                    {
                        sign = line.Substring(22, 1);                        
                        quantity = long.Parse(line.Substring(10, 12));

                        DataRow drTemp = dsFails.Tables["Fails"].NewRow();

                        drTemp["SecId"] = line.Substring(1, 9);

                        if (sign.Equals("+"))
                        {
                            drTemp["ClearingFTRSettled"] = quantity;
                            drTemp["ClearingFTRTraded"] = 0;
                            drTemp["ClearingFTDSettled"] = 0;
                            drTemp["ClearingFTDTraded"] = 0;
                        }
                        else
                        {
                            drTemp["ClearingFTRSettled"] = 0;
                            drTemp["ClearingFTRTraded"] = 0;
                            drTemp["ClearingFTDSettled"] = quantity;
                            drTemp["ClearingFTDTraded"] = 0;
                        }

                        dsFails.Tables["Fails"].Rows.Add(drTemp);
                    }
                }
                else
                {
                    break;
                }
            }

            if (dsFails.Tables["Fails"].Rows.Count > 0)
            {
                LoadDatabase(bizDate);
            }
        }

        public bool CheckFileDate(string fileDate, string bizDate)
        {
            if (DateTime.ParseExact(fileDate, "MM-dd-yyyy", null).ToString("yyyy-MM-dd").Equals(bizDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadDatabase(string bizDate)
        {
            foreach (DataRow drFailItem in dsFails.Tables["Fails"].Rows)
            {
                BoxFailsItemSet(
                    bizDate,
                    "0158",
                    drFailItem["SecId"].ToString(),
                    drFailItem["ClearingFTDTraded"].ToString(),
                    drFailItem["ClearingFTRTraded"].ToString(),
                    drFailItem["ClearingFTDSettled"].ToString(),
                    drFailItem["ClearingFTRSettled"].ToString());
            }
        }

        private void BoxFailsItemSet(
            string bizDate, 
            string bookGroup, 
            string secId,
            string clearingFTDTraded,
            string clearingFTRTraded,
            string clearingFTDSettled,
            string clearingFTRSettled)
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxFailsItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramClearingFTDTraded = dbCmd.Parameters.Add("@ClearingFTDTraded", SqlDbType.BigInt);
                paramClearingFTDTraded.Value = long.Parse(clearingFTDTraded);

                SqlParameter paramClearingFTRTraded = dbCmd.Parameters.Add("@ClearingFTRTraded", SqlDbType.BigInt);
                paramClearingFTRTraded.Value = long.Parse(clearingFTRTraded);

                SqlParameter paramClearingFTDSettled = dbCmd.Parameters.Add("@ClearingFTDSettled", SqlDbType.BigInt);
                paramClearingFTDSettled.Value = long.Parse(clearingFTDSettled);

                SqlParameter paramClearingFTRSettled = dbCmd.Parameters.Add("@ClearingFTRSettled", SqlDbType.BigInt);
                paramClearingFTRSettled.Value = long.Parse(clearingFTRSettled);
                
                dbCn.Open();
                dbCmd.ExecuteNonQuery();
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
        
        private void BoxFailsPurge(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxFailsPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
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
