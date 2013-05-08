using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bpsa_DataLoad
{
    class ParseSegregation
    {
        private DataSet dsSegregation;
        private string dbCnStr;

        public ParseSegregation()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = "Zeus";
            sqlBuilder.InitialCatalog = "Sendero";
            sqlBuilder.IntegratedSecurity = true;

            dbCnStr = sqlBuilder.ConnectionString;      
        }

        public void Load(string filePath, string bizDatePrior, string bizDate)
        {
            BoxSegregationPurge(bizDate);

            TextReader textReader = new StreamReader(filePath);
            string line = "";

            dsSegregation = new DataSet();
            dsSegregation.Tables.Add("Seg");
            dsSegregation.Tables["Seg"].Columns.Add("SecId");
            dsSegregation.Tables["Seg"].Columns.Add("SecIdAlias");
            dsSegregation.Tables["Seg"].Columns.Add("Segregation");
            dsSegregation.AcceptChanges();

            line = textReader.ReadLine();

            if (!CheckFileDate(line.Substring(5, 6), bizDatePrior))
            {
                Console.WriteLine("File is not for today.");
                return;
            }

            while (true)
            {
                line = textReader.ReadLine();
                if (line != null)
                {
                    DataRow drTemp = dsSegregation.Tables["Seg"].NewRow();
                    drTemp["SecId"] = line.Substring(5, 9);
                    drTemp["SecIdAlias"] = line.Substring(15, 7);
                    drTemp["Segregation"] = line.Substring(23, 8);
                    dsSegregation.Tables["Seg"].Rows.Add(drTemp);
                }
                else
                {
                    break;
                }
            }           

            if (dsSegregation.Tables["Seg"].Rows.Count > 0)
            {
                LoadDatabase(bizDate);
            }
        }

        public bool CheckFileDate(string fileDate, string bizDate)
        {
            if (DateTime.ParseExact(fileDate, "MMddyy", null).ToString("yyyy-MM-dd").Equals(bizDate))
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
            foreach (DataRow drSegItem in dsSegregation.Tables["Seg"].Rows)
            {
                BoxSegregationItemSet(
                    bizDate,
                    "0158",
                    drSegItem["SecId"].ToString(),
                    drSegItem["SecIdAlias"].ToString(),
                    drSegItem["Segregation"].ToString());

            }
        }

        private void BoxSegregationItemSet(string bizDate, string bookGroup, string secId, string secIdAlias, string segregation)
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxSegregationItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramSecIdAlias = dbCmd.Parameters.Add("@SecIdAlias", SqlDbType.VarChar, 12);
                paramSecIdAlias.Value = secIdAlias;

                SqlParameter paramSegregation = dbCmd.Parameters.Add("@Segregation", SqlDbType.BigInt);
                paramSegregation.Value = segregation;

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
        
        private void BoxSegregationPurge(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxSegregationPurge", dbCn);
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
