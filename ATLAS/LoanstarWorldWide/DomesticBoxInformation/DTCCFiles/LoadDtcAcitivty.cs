using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;
using StockLoan.MqSeries;

namespace StockLoan.ParsingFiles
{
    class LoadDtcAcitivty
    {     
        private string dbCnStr;
        private string bizDate;

        public LoadDtcAcitivty(string bizDate, string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.bizDate = bizDate;
        }

        public void Load()
        {            
            MqSeries.MqActivity mqActivity = new MqActivity("", dbCnStr);
            MqMessage mqMsg = new MqMessage();

            while (true)
            {
                try
                {
                    mqMsg = mqActivity.PullMessage();
                    DtcMessageSet(mqMsg.bizDate, mqMsg.message.Replace('\0',' '));

                    Log.Write("Message: " + mqMsg.bizDate + " - " + mqMsg.message, 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);
                    break;
                }
            }

            mqActivity.MqActivityClose();
        }

        public void DtcMessageSet(string bizDate, string message)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spDtcActivityMessageInsert", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "30"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramMessage = dbCmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000);
                paramMessage.Value = message;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[LoadDtcAcitivty.DtcMessageSet]");
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
