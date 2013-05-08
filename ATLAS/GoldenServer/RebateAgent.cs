using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using StockLoan.Common;

namespace StockLoan.Golden
{
	public class RebateAgent : MarshalByRefObject, IRebate
	{
		private string dbCnStr;

        public RebateAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}

        public string KeyValueGet(string keyId, string keyValueDefault)
        {
            return KeyValue.Get(keyId, keyValueDefault, dbCnStr);
        }

        public void KeyValueSet(string keyId, string keyValueDefault)
        {
            KeyValue.Set(keyId, keyValueDefault, dbCnStr);
        }

        public void ShortSaleBillingSummaryTradingGroupSet(string groupCode, string negativeRebateMarkUp,
            string negativeRebateBill, string userId, string platForm, int iparamEditFlag)
    {

            string sProc;
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            if (platForm.ToLower().Equals("penson"))
            {
                sProc = "spTradingGroupSet";
            }
            else
            {
                sProc = "spTradingGroupBPSSet";
            }

            try
            {
                SqlCommand dbCmd = new SqlCommand(sProc, dbCn);

                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;

                SqlParameter paramRebateBill = dbCmd.Parameters.Add("@NegativeRebateBill", SqlDbType.VarChar, 5);
                paramRebateBill.Value = negativeRebateBill;

                SqlParameter paramNegativeRebateMarkUp = dbCmd.Parameters.Add("@NegativeRebateMarkUp", SqlDbType.Decimal);
                paramNegativeRebateMarkUp.Value = negativeRebateMarkUp;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                SqlParameter paramAccountEditFlag = dbCmd.Parameters.Add("@AccountParametersEdit", SqlDbType.Int);
                paramAccountEditFlag.Value = iparamEditFlag;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                //bResults = true;
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryTradingGroupsSet]", Log.Error, 1);
                throw;
            }
            finally
            {
                dbCn.Close();
            };
    }
        public DataSet ShortSaleBillingSummaryGet(string startDate, string stopDate, string groupCode, string accountNumber, string platForm)
        {
            DataSet dsRebate = new DataSet();
            string spProc = "";

            if (platForm.ToLower().Equals("penson"))
            {
                spProc = "spShortSaleBillingSummaryGet";
            }
            else
            {
                spProc = "spShortSaleBillingBPSSummaryGet";
            }

            try
            {                
                SqlCommand dbCmd = new SqlCommand(spProc, new SqlConnection(dbCnStr));

                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!startDate.Equals(""))
                {
                    SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                    paramStartDate.Value = startDate;
                }

                if (!stopDate.Equals(""))
                {
                    SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                    paramStopDate.Value = stopDate;
                }

                if (!accountNumber.Equals(""))
                {
                    SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);
                    paramAccountNumber.Value = accountNumber;
                }

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = groupCode;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsRebate, "Rebate");

                dsRebate.Tables["Rebate"].PrimaryKey = new DataColumn[1]
			{				
				dsRebate.Tables["Rebate"].Columns["ProcessId"]
			};

            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryGet]", Log.Error, 1);
                throw;
            }

            return dsRebate;
        }

        public string ShortSaleBillingSummaryMasterBillGet(
            string startDate,
            string stopDate,
            string platForm)
        {
            string bill = "";

            ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, startDate, stopDate, "", platForm);

            bill = shortSaleBill.MasterBill();

            return bill;
        }


		public DataSet ShortSaleBillingSummaryMasterBillExcelGet(string startDate, string stopDate, string platForm)
		{	
			DataSet dsMasterBill = new DataSet();

			ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, startDate, stopDate, "", platForm);

			dsMasterBill = shortSaleBill.MasterBillExcel();

			return dsMasterBill;

		}


        public DataSet ShortSaleBillingSummaryCorrespondentBillExcelGet(string startDate, string stopDate, string groupCode, string platForm)
        {
            DataSet dsExcel = null;

            ShortSaleNegativeRebateBillDocument excelCorrespondentBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, startDate, stopDate, groupCode, platForm);

            dsExcel = excelCorrespondentBill.ExcelCorresspondentBill(startDate, stopDate, groupCode, platForm);

            return dsExcel;
        }

        public string ShortSaleBillingSummaryBillGet(
            string startDate,
            string stopDate,
            string groupCode,
            string platForm)
        {
            string bill = "";

            ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, startDate, stopDate, groupCode, platForm);

            bill = shortSaleBill.CorresspondentBill(true, true);

            return bill;
        }


        public void ShortSaleBillingSummaryBillingReportGet(
            string startDate,
            string stopDate,
            string groupCode,
            ref long groupCodeCount,
            ref long secIdCount,
            ref decimal totalCharges,
            string platForm)
        {
            string spProc = "";
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingSummaryBillingReportGet";
                }
                else
                {
                    spProc = "spShortSaleBillingSummaryBPSBillingReportGet";
                }

                SqlCommand dbCmd = new SqlCommand(spProc, dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramStartDate.Value = startDate;

                SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = groupCode;
                }

                SqlParameter paramGroupCodeCount = dbCmd.Parameters.Add("@GroupCodeCount", SqlDbType.BigInt);
                paramGroupCodeCount.Direction = ParameterDirection.Output;

                SqlParameter paramSecIdCount = dbCmd.Parameters.Add("@SecIdCount", SqlDbType.BigInt);
                paramSecIdCount.Direction = ParameterDirection.Output;

                SqlParameter paramTotalCharges = dbCmd.Parameters.Add("@TotalCharges", SqlDbType.Decimal);
                paramTotalCharges.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                groupCodeCount = long.Parse(paramGroupCodeCount.Value.ToString());
                secIdCount = long.Parse(paramSecIdCount.Value.ToString());

                totalCharges = decimal.Parse(paramTotalCharges.Value.ToString());
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryBillingReportGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                dbCn.Close();
            }
        }

        public DataSet ShortSaleBillingCorrespondentSummaryGet(
            string startDate,
            string stopDate,
            string groupCode,
            string platForm)
        {
            DataSet dsSummary = new DataSet();
            dsSummary.RemotingFormat = SerializationFormat.Binary;

            string spProc = "";

            if (platForm.ToLower().Equals("penson"))
            {
                spProc = "spShortSaleBillingCorrespondentSummaryGet";
            }
            else
            {
                spProc = "spShortSaleBillingCorrespondentBPSSummaryGet";
            }

            try
            {
                SqlCommand dbCmd = new SqlCommand(spProc, new SqlConnection(dbCnStr));
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramStartDate.Value = startDate;

                SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = groupCode;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsSummary, "CorrespondentSummary");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingCorrespondentSummaryGet]", Log.Error, 1);
            }

            return dsSummary;
        }

        public DataSet ShortSaleBillingSummaryTradingGroupGet(string groupCode, string platForm)
        {
            DataSet dsGroups = new DataSet();
            dsGroups.RemotingFormat = SerializationFormat.Binary;

            string spProc = "";

            if (platForm.ToLower().Equals("penson"))
            {
                spProc = "spTradingGroupGet";
            }
            else
            {
                spProc = "spTradingGroupBPSGet";
            }

            try
            {
                SqlCommand dbCmd = new SqlCommand(spProc, new SqlConnection(dbCnStr));
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = groupCode;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsGroups, "TradingGroups");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.ShortSaleTradingGroupsGet]", Log.Error, 1);
            }

            return dsGroups;
        }


		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
