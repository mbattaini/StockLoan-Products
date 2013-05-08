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
    class ProcessDtcAcitivty
    {     
        private string dbCnStr;
        private string bizDate;

        public ProcessDtcAcitivty(string bizDate, string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
            this.bizDate = bizDate;
        }

        public void DtcMessageLoad()
        {
            DtcMessage.DtcMessageInformation dtcMsgInfo = new DtcMessage.DtcMessageInformation();

            DataSet dsMessages = new DataSet();

            long rowCount = 1;
            long counter = 0;

            dsMessages = DtcMessagesGet(bizDate);

            foreach (DataRow drRow in dsMessages.Tables["DtcMessages"].Rows)
            {
                dtcMsgInfo = DtcMessage.Parse(drRow["Message"].ToString());

                if (dtcMsgInfo.isSuccessful)
                {
                    try
                    {
                        DtcActivitySet(dtcMsgInfo);
                        DtcMessageSet(bizDate, drRow["ProcessId"].ToString(), true);

                        counter++;
                    }
                    catch 
                    {
                        DtcMessageSet(bizDate, drRow["ProcessId"].ToString(), false);
                        Log.Write("Process ID : " + drRow["ProcessId"].ToString() + " was invalid. please check message.", 1);
                    }
                }
                else
                {
                    DtcMessageSet(bizDate, drRow["ProcessId"].ToString(), false);
                    Log.Write("Process ID : " + drRow["ProcessId"].ToString() + " was invalid. please check message.", 1);
                }
            }


        }

        private DataSet DtcMessagesGet(string bizDate)
        {
            DataSet dsMessages = new DataSet();

            try
            {
                SqlConnection dbCn = new SqlConnection(dbCnStr);
                SqlCommand dbCmd = new SqlCommand("spDtcActivityMessagesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramTradeDate.Value = bizDate;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsMessages, "DtcMessages");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }

            return dsMessages;
        }

        private void DtcActivitySet(DtcMessage.DtcMessageInformation dtcMessage)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            if (dtcMessage.messageCopy.Trim().Equals("Q"))
            {
                return;
            }

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDtcActivityDomesticInsert", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramMessgaeType = dbCmd.Parameters.Add("@MessageType", SqlDbType.VarChar, 50);
                paramMessgaeType.Value = dtcMessage.messageType;

                SqlParameter paramCusip = dbCmd.Parameters.Add("@Secid", SqlDbType.VarChar, 9);
                paramCusip.Value = dtcMessage.cusip;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = dtcMessage.quantity;

                SqlParameter paramContraParty = dbCmd.Parameters.Add("@ContraParty", SqlDbType.VarChar, 4);
                paramContraParty.Value = dtcMessage.contraParty;

                SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
                paramSettleDate.Value = DateTime.ParseExact(dtcMessage.settleDate, "MMddyy", null).ToString("yyyy-MM-dd");

                SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 3);
                paramReasonCode.Value = dtcMessage.reasonCode;

                SqlParameter paramPendMadeFlag = dbCmd.Parameters.Add("@PendMadeFlag", SqlDbType.VarChar, 50);
                paramPendMadeFlag.Value = dtcMessage.pendMadeFlag;

                SqlParameter paramMethod = dbCmd.Parameters.Add("@Method", SqlDbType.VarChar, 50);
                paramMethod.Value = dtcMessage.method;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        private void DtcMessageSet(string bizDate, string processId, bool isProcessed)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spDtcActivityMessageSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 3600;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;
                
                SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.BigInt);
                paramProcessId.Value = processId;

                SqlParameter paramProcessFlag = dbCmd.Parameters.Add("@ProcessFlag", SqlDbType.VarChar, 1);
                paramProcessFlag.Value = (isProcessed) ? "S" : "E";

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
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
