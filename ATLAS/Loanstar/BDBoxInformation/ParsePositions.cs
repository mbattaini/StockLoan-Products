using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StockLoan.ParsingFiles
{
    class ParsePositions
    {
        public event EventHandler<ProgressEventArgs> ProgressChanged = delegate { };

        private string dbCnStr;
        private string exDbCnStr;
        private string bookGroup;
        private int count;
        private int interval;

        public ParsePositions(string dbCnStr, string extDbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;
            this.exDbCnStr = extDbCnStr;
            this.bookGroup = bookGroup;
            this.count = 0;
            this.interval = 100;
        }

        public void Load(string positionSql, string bizDate)
        {
            
            SqlConnection dbCn = new SqlConnection(exDbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand(positionSql, dbCn);
                dbCmd.CommandType = CommandType.Text;

                dbCn.Open();
                SqlDataReader sqlDataReader = dbCmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    BoxPositionItemSet(
                        bizDate,
                        bookGroup,
                        sqlDataReader.GetValue(0).ToString(),
                        sqlDataReader.GetValue(1).ToString(),
                        sqlDataReader.GetValue(2).ToString(),
                        sqlDataReader.GetValue(3).ToString(),
                        sqlDataReader.GetValue(4).ToString(),
                        sqlDataReader.GetValue(6).ToString(),
                        sqlDataReader.GetValue(5).ToString());

                    count++;

                    if ((count % interval) == 0)
                    {
                        UpdateProgress(count);
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }     

        
        private void BoxPositionItemSet(
            string bizDate, 
            string bookGroup, 
            string secId,
            string secIdAlias,
            string countryCode,
            string positionAccount,
            string positionType,
            string position,
            string settleDate)
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionsItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramSecIdAlias = dbCmd.Parameters.Add("@SecIdAlias", SqlDbType.VarChar, 12);
                paramSecIdAlias.Value = secIdAlias;

                SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                paramCountryCode.Value = countryCode;

                SqlParameter paramPositionAccount = dbCmd.Parameters.Add("@PositionAccount", SqlDbType.VarChar, 50);
                paramPositionAccount.Value = positionAccount;

                SqlParameter paramPositionType = dbCmd.Parameters.Add("@PositionType", SqlDbType.Int);
                paramPositionType.Value = int.Parse(positionType);

                SqlParameter paramPosition = dbCmd.Parameters.Add("@Position", SqlDbType.BigInt);
                paramPosition.Value = position;

                SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
                paramSettleDate.Value = settleDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParsePositions.BoxPositionItemSet]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }


        public void UpdateProgress(long count)
        {
            EventHandler<ProgressEventArgs> progressEvent = ProgressChanged;

            progressEvent(null, new ProgressEventArgs(count));
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public int Interval
        {
            set
            {
                interval = value;
            }
        }

    }
}
