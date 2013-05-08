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
    class ParseSecMaster
    {  
        private string dbCnStr;        
        private string bookGroup;
        private int count;
        private int interval;

        public ParseSecMaster(string dbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;            
            this.bookGroup = bookGroup;
            count = 0;            
        }

        public void Load(string secmasterSql, string bizDate)
        {
            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spSecurityDataLoad", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
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

        public int Count
        {
            get
            {
                return count;
            }
        }

        public int Interval
        {
            get
            {
                return interval;
            }

            set
            {
                interval = value;
            }
        }
    }
}
