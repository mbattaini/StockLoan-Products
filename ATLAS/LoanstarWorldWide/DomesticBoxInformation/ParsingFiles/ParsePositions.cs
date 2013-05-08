using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using StockLoan.Common;

namespace StockLoan.ParsingFiles
{
    class ParsePositions
    {
        public event EventHandler<ProgressEventArgs> ProgressChanged = delegate { };

        private string dbCnStr;
        private string exDbCnStr;
        private string bookGroup;
        private long count;
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
                SqlCommand dbCmd = new SqlCommand("spBoxCustomerPositionsGet", dbCn);                
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramRecordCount = dbCmd.Parameters.Add("@RecordCount", SqlDbType.BigInt);
                paramRecordCount.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                count = long.Parse(paramRecordCount.Value.ToString());
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
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
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

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

        public long Count
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
