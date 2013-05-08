using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using StockLoan.Common;
using DashBusiness;

namespace DashBoardWebService
{
    public struct UserProfile
    {
        public string userId;
        public string functionPath;
        public bool view;
        public bool edit;
        public string bookGroupList;
    }

    public class PositionService : IPosition
    {
        public bool UserValidationGet(string userName, string password)
        {
            DataSet dsUser = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            bool result = false;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spWebUsersGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userName;

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(dbCmd);
                sqlAdapter.Fill(dsUser, "User");

                if (bool.Parse(dsUser.Tables["User"].Rows[0]["IsEnabled"].ToString()) && password.Equals(dsUser.Tables["User"].Rows[0]["Password"].ToString()))
                {
                    result = true;
                }

            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.UserValidationGet]", Log.Error, 1);
                result = false;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return result;
        }

        public byte[] ContractDataGet(string bizDate, string bookGroup, string secId, string userId, string functionPath)
        {
            DataSet dsContracts = new DataSet();

            //Returns all deals by book
            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");

                Locale localeType;

                switch (Standard.ConfigValue("BookGroup" + bookGroup + "_Locale", ""))
                {
                    case ("International"):
                        localeType = Locale.International;
                        break;

                    case ("Domestic"):
                        localeType = Locale.Domestic;
                        break;

                    default:
                        localeType = Locale.Domestic;
                        break;
                }


                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");


                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsContracts.Tables["Contracts"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsContracts.AcceptChanges();
                }
                else
                {
                    dsContracts.Tables["Contracts"].Rows.Clear();
                }

            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ContractDataGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsContracts);
        }

        public byte[] HtbProfitabilityGet(string bizDate, HtbLocale htbLocate, int numOfRecords)
        {
            DataSet dsProfit = new DataSet();

            string spCmd = "";

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");

                if (htbLocate == HtbLocale.Bps)
                {
                    spCmd = "spHardToBorrowProfitabilityBPSGet";
                }
                else
                {
                    spCmd = "spHardToBorrowProfitabilityGet";
                }


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand(spCmd, dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramNumOfRecords = dbCmd.Parameters.Add("@NumOfRecords", SqlDbType.Int);
                paramNumOfRecords.Value = numOfRecords;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsProfit, "Profitable");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.HtbProfitabilityGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsProfit);
        }

        public byte[] HtbProfitabilitySpreadsGet(string bizDate, HtbLocale htbLocate)
        {
            DataSet dsProfit = new DataSet();

            string spCmd = "";
            string bookGroup = "";
            string bizDatePrior = "";

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");

                if (htbLocate == HtbLocale.Bps)
                {
                    spCmd = "spHardToBorrowSpreadBPSGet";
                    bookGroup = "0158";
                    bizDatePrior = DateTime.Parse(bizDate).AddDays(-1.0).ToString("yyyy-MM-dd");
                }
                else
                {
                    spCmd = "spHardToBorrowSpreadGet";
                    bookGroup = "0234";
                    bizDatePrior = bizDate;
                }


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand(spCmd, dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsProfit, "ProfitableSpreads");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.HtbProfitabilitySpreadsGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsProfit);
        }

        public byte[] ContractSummaryByCashLoad(string bizDate, string bookGroup, Locale localeType, string userId, string functionPath)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = "";

                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");

                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsContracts.Tables["Contracts"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsContracts.AcceptChanges();
                }
                else
                {
                    dsContracts.Tables["Contracts"].Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ContractSummaryByCashLoad]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(Contracts.ContractsByBookCash(dsContracts, localeType));
        }

        public byte[] ContractSummaryByBooKGroupCashLoad(string bizDate, Locale localeType, string userId, string functionPath)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = "";

                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");

                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsContracts.Tables["Contracts"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsContracts.AcceptChanges();
                }
                else
                {
                    dsContracts.Tables["Contracts"].Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ContractSummaryByCashLoad]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(Contracts.ContractsByBookGroupCash(dsContracts, localeType));
        }

        public byte[] ContractSummaryByBooKGroupSharesCashLoad(string bizDate, Locale localeType, string userId, string functionPath)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = "";

                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");

                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsContracts.Tables["Contracts"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsContracts.AcceptChanges();
                }
                else
                {
                    dsContracts.Tables["Contracts"].Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ContractSummaryByCashLoad]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(Contracts.ContractsByBookGroupSharesCash(dsContracts, localeType));
        }

        public byte[] ContractsExcessCollateralSummaryLoad(string bizDate, string bookGroup, Locale localeType, string userId, string functionPath)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = "";

                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");

                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsContracts.Tables["Contracts"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsContracts.AcceptChanges();
                }
                else
                {
                    dsContracts.Tables["Contracts"].Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ContractsExcessCollateralSummaryLoad]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(Contracts.ContractsExcessCollateralSummary(Contracts.ContractsExcessCollateral(dsContracts)));
        }

        public byte[] ContractsDetailByBookSummaryLoad(string bizDate, string bookGroup, string book, string currencyIso, Locale localeType, string userId, string functionPath)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = "";

                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                if (!bookGroup.Equals("") && !bookGroup.Equals("****"))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");

                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsContracts.Tables["Contracts"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsContracts.AcceptChanges();
                }
                else
                {
                    dsContracts.Tables["Contracts"].Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ContractsDetailByBookSummaryLoad]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(Contracts.ContractsDetailsByBookGet(dsContracts, bookGroup, book, currencyIso));
        }

        public byte[] MarksGet(string bizDate, string bookGroup, string book, string currencyIso, Locale localeType)
        {
            DataSet dsMarks = new DataSet();
            DataSet dsContractSummary = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = "";

                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spMarkGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                if (!bookGroup.Equals("") && !bookGroup.Equals("****"))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = 0;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsMarks, "Marks");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.MarksGet]", Log.Error, 1);
            }

            return Functions.ConvertDataSet(dsMarks);
        }

        public byte[] InventoryHistoryGet(string secId)
        {
            DataSet dsInventory = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spInventoryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsInventory, "Inventory");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.InventoryHistoryGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsInventory);
        }

        public byte[] WebReportsGet()
        {
            DataSet dsInventory = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spWebReportsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsInventory, "WebReports");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.WebReportsGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsInventory);
        }

        public string InventoryRateGet(string secId)
        {
            string rate = "";

            DataSet dsInventory = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spInventoryRateGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Float);
                paramRate.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();

                rate = paramRate.Value.ToString();

                dbCn.Close();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.InventoryHistoryGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return rate;
        }

        public byte[] InventoryRateHistoryGet(string secId)
        {
            DataSet dsInventory = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spInventoryRateHistoryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsInventory, "InventoryRateHistory");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.InventoryRateHistoryGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsInventory);
        }

        public byte[] InventoryHistorySummaryGet(string secId)
        {
            DataSet dsInventory = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spInventoryHistoryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsInventory, "Inventory");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.InventoryHistoryGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(Inventory.InventoryDailyAggreagates(dsInventory));
        }

        public byte[] InventoryDeskGet(string desk, string bizDate)
        {
            DataSet dsInventory = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spInventoryDeskGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
                paramDesk.Value = desk;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsInventory, "InventoryDesk");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.InventoryDeskGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsInventory);
        }

        public byte[] DeskListGet(string bizDate)
        {
            DataSet dsInventory = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spDeskListGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsInventory, "DeskList");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.DeskListGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsInventory);
        }

        public byte[] BookGroupGet(string userId, string functionPath)
        {
            DataSet dsBookGroupDomestic = new DataSet();
            DataSet dsBookGroupInternational = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spBookGroupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsBookGroupDomestic, "BookGroup");

                dbCmd.Connection = new SqlConnection(GetDatabaseConnection("Loanstar"));
                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsBookGroupInternational, "BookGroup");


                foreach (DataRow drBookGroup in dsBookGroupInternational.Tables["BookGroup"].Rows)
                {
                    DataRow drTemp = dsBookGroupDomestic.Tables["BookGroup"].NewRow();

                    drTemp["BookGroup"] = drBookGroup["BookGroup"].ToString();
                    drTemp["BookName"] = drBookGroup["BookGroupName"].ToString();

                    dsBookGroupDomestic.Tables["BookGroup"].Rows.Add(drTemp);
                }

                dsBookGroupDomestic.AcceptChanges();

                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsBookGroupDomestic.Tables["BookGroup"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsBookGroupDomestic.Tables["BookGroup"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsBookGroupDomestic.Tables["BookGroup"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsBookGroupDomestic.AcceptChanges();
                }
                else
                {
                    dsBookGroupDomestic.Tables["BookGroup"].Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.BookGroupGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsBookGroupDomestic);
        }

        public byte[] HolidaysGet()
        {
            DataSet dsHolidays = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Loanstar");

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spHolidayGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsHolidays, "Holidays");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.HolidaysGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsHolidays);
        }

        public bool UserCanView(string userId, string functionPath)
        {
            UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

            return userProfile.view;
        }

        private UserProfile WebSecurityProfileGet(string userId, string functionPath)
        {
            DataSet dsUser = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spWebUserSecurityProfileGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 25);
                paramFunctionPath.Value = functionPath;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsUser, "User");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.WebSecurityProfileGet]", Log.Error, 1);
                throw;
            }

            return UserDataSetParse(dsUser.Tables["User"]);
        }

        public byte[] WebSecurityProfileDataSetGet(string userId)
        {
            DataSet dsUser = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spWebUserSecurityProfileGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsUser, "User");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.WebSecurityProfileDataSetGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsUser);
        }

        private UserProfile UserDataSetParse(DataTable dtUser)
        {

            UserProfile userProfile = new UserProfile();

            try
            {
                userProfile.userId = dtUser.Rows[0]["UserId"].ToString();
                userProfile.functionPath = dtUser.Rows[0]["FunctionPath"].ToString();
                userProfile.view = bool.Parse(dtUser.Rows[0]["View"].ToString());
                userProfile.edit = bool.Parse(dtUser.Rows[0]["Edit"].ToString());
                userProfile.bookGroupList = dtUser.Rows[0]["BookGroupList"].ToString();
            }
            catch
            {
                userProfile.userId = "";
                userProfile.functionPath = "";
                userProfile.view = false;
                userProfile.edit = false;
                userProfile.bookGroupList = "";
            }


            return userProfile;
        }

        private string GetDatabaseConnection(string system)
        {
            switch (system.ToLower())
            {
                case "sendero":
                    return Standard.ConfigValue("SenderoDatabase", "");

                case "loanstar":
                    return Standard.ConfigValue("LoanstarDatabase", "");

                default:
                    return "";

            }
        }

        public byte[] ContractSummaryBySecurity(string bizDate, string bookGroup, Locale localeType, string userId, string functionPath)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = "";

                switch (Standard.ConfigValue("BookGroup" + bookGroup + "_Locale", ""))
                {
                    case ("International"):
                        localeType = Locale.International;
                        break;

                    case ("Domestic"):
                        localeType = Locale.Domestic;
                        break;

                    default:
                        localeType = Locale.Domestic;
                        break;
                }

                switch (localeType)
                {
                    case (Locale.Domestic):
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;

                    case (Locale.International):
                        dbCnStr = GetDatabaseConnection("Loanstar");
                        break;

                    default:
                        dbCnStr = GetDatabaseConnection("Sendero");
                        break;
                }

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                if (!bookGroup.Equals("") && !bookGroup.Equals("****"))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");

                UserProfile userProfile = WebSecurityProfileGet(userId, functionPath);

                if (userProfile.view)
                {
                    for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
                    {
                        if (!userProfile.bookGroupList.Contains(dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString()))
                        {
                            dsContracts.Tables["Contracts"].Rows.RemoveAt(index);
                            index--;
                        }
                    }

                    dsContracts.AcceptChanges();
                }
                else
                {
                    dsContracts.Tables["Contracts"].Rows.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ContractsDetailByBookSummaryLoad]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(Contracts.ContractsBySecurity(dsContracts));
        }

        public bool ReportValueSet(string reportName, string reportRecipient, string format, string justify)
        {
            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            try
            {
                string dbCnStr = "";

                dbCnStr = GetDatabaseConnection("Sendero");

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spReportValueSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
                paramReportName.Value = reportName;

                SqlParameter paramReportRecipient = dbCmd.Parameters.Add("@ReportRecipient", SqlDbType.VarChar, 50);
                paramReportRecipient.Value = reportRecipient;

                if (!format.Equals(""))
                {
                    SqlParameter paramFormat = dbCmd.Parameters.Add("@Format", SqlDbType.VarChar, 50);
                    paramFormat.Value = format;
                }

                if (!justify.Equals(""))
                {
                    SqlParameter paramJustify = dbCmd.Parameters.Add("@Justify", SqlDbType.Char, 10);
                    paramJustify.Value = justify;
                }

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ReportValueSet]", Log.Error, 1);
                return false;
            }
            finally
            {
                if (!dbCn.State.Equals(ConnectionState.Closed))
                {
                    dbCn.Close();
                }
            }

            return true;
        }

        public string FileReader(string path)
        {
            string file = "";

            file = File.ReadAllText(path);

            return file;
        }

        public byte[] StraddlesGet(string accountList, string locMemo, string accountType)
        {
            DataSet dsStraddles = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spStraddlesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramAccountList = dbCmd.Parameters.Add("@AccountList", SqlDbType.VarChar, 50);
                paramAccountList.Value = accountList;

                SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.VarChar, 1);
                paramLocMemo.Value = locMemo;

                SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 1);
                paramAccountType.Value = accountType;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsStraddles, "Straddles");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.StraddlesGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsStraddles);
        }

        public byte[] ShortSaleLocatesSourceGet(string tradeDate, string source)
        {
            DataSet dsLocates = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");


                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spShortSaleLocateSourceGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                paramTradeDate.Value = tradeDate;

                SqlParameter paramSource = dbCmd.Parameters.Add("@Source", SqlDbType.VarChar, 10);
                paramSource.Value = source;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsLocates, "Locates");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ShortSaleLocatesSourceGet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsLocates);
        }

        public byte[] BankLoanReleaseReportGet(string bizDate, string book)
        {
            string sql = "";
         
            SqlConnection localDbCn = new SqlConnection(GetDatabaseConnection("Sendero"));
            DataSet dsBankLoanRelease = new DataSet();

            try
            {
                sql = "Create Table dbo.#BankLoanRelease ( \r\n" +
                    "  BookGroup varchar(10), \r\n" +
                    "  SecId varchar(12), \r\n" +
                    "  Symbol varchar(12), \r\n" +
                    "  OCC_Pledge bigint, \r\n" +
                    "  Pledged  bigint, \r\n" +
                    "  CNS_FTD  bigint, \r\n" +
                    "  BRK_FTD  bigint, \r\n" +
                    "  DVP_FTD  bigint, \r\n" +
                    "  ExDeficit bigint, \r\n" +
                    "  BankLoanRelease bigint, \r\n" +
                    "  BankLoanLeftover bigint, \r\n" +
                    "  OCC_Release bigint, \r\n" +
                    "  OCC_Leftover bigint ) \r\n" +
                    "Insert Into dbo.#BankLoanRelease \r\n" +
                    "Select  B.BookGroup, \r\n" +
                    "  B.SecId, \r\n" +
                    "  (Select Symbol From dbo.SecurityBase With (NOLOCK) Where CUSIP = B.Secid \r\n" +
                    "  ) As Symbol, \r\n" +
                    "  B.Quantity As OCC_Pledge, \r\n" +
                    "  IsNull(( Select Sum(Quantity) From dbo.tbBankLoanPosition With (NOLOCK)  \r\n" +
                    "     Where BookGroup = B.BookGroup And Book <> B.book And SecId = B.SecId \r\n" +
                    "    ), 0) AS Pledged, \r\n" +
                    "  (Select ClearingFailOutSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                    "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                    "     And   BizDate = '" + bizDate + "' \r\n" +
                    "   ) As CNS_FTD, \r\n" +
                    "  (Select BrokerFailOutSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                    "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                    "     And   BizDate = '" + bizDate + "' \r\n" +
                    "   ) As BRK_FTD, \r\n" +
                    "  (Select DvpFailOutSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                    "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                    "     And   BizDate = '" + bizDate + "' \r\n" +
                    "   ) As DVP_FTD, \r\n" +
                    "  (Select ExDeficitSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                    "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                    "     And   BizDate = '" + bizDate + "' \r\n" +
                    "   ) As ExDeficit, \r\n" +
                    "  0 As BankLoanRelease, \r\n" +
                    "  0 As BankLoanLeftover, \r\n" +
                    "  0 AS OCC_Release, \r\n" +
                    "  0 As OCC_Leftover \r\n" +
                    "From  dbo.tbBankLoanPosition B With (NOLOCK) \r\n" +
                    "Where B.Book = '" + book + "' \r\n" +
                    "Order by 2 \r\n" +
                    " \r\n" +
                    "Update  dbo.#BankLoanRelease \r\n" +
                    "Set  BankLoanRelease = (Case When((CNS_FTD + BRK_FTD + DVP_FTD) + (Case When ExDeficit > 0 Then 0 Else ABS(ExDeficit) END) \r\n" +
                    "           ) > Pledged  \r\n" +
                    "        Then Pledged  \r\n" +
                    "        Else ((CNS_FTD + BRK_FTD + DVP_FTD) + (Case when ExDeficit > 0 Then 0 Else ABS(ExDeficit) END) \r\n" +
                    "        )  \r\n" +
                    "         END) \r\n" +
                    "Where  Pledged > 0 \r\n" +
                    " \r\n" +
                    "Update  dbo.#BankLoanRelease \r\n" +
                    "Set  BankLoanLeftover = (CNS_FTD + BRK_FTD + DVP_FTD)  \r\n" +
                    "         + (Case when ExDeficit > 0 Then 0 Else ABS(ExDeficit) END)  \r\n" +
                    "         - BankLoanRelease \r\n" +
                    " \r\n" +
                    "Update  dbo.#BankLoanRelease \r\n" +
                    "Set  OCC_Release = (Case When(BankLoanRelease) > OCC_Pledge  \r\n" +
                    "        Then OCC_Pledge  \r\n" +
                    "        Else BankLoanLeftOver END ) \r\n" +
                    "Where  OCC_Pledge > 0 \r\n" +
                    " \r\n" +
                    "Update  dbo.#BankLoanRelease \r\n" +
                    "Set  OCC_Leftover = OCC_Pledge - OCC_Release \r\n" +
                    " \r\n" +
                    "Update  dbo.#BankLoanRelease \r\n" +
                    "Set  OCC_Release = OCC_Release + OCC_Leftover \r\n" +
                    "Where  OCC_Release > OCC_Pledge \r\n" +
                    " \r\n" +
                    "Select  BookGroup, SecId, Symbol, \r\n" +
                    "  OCC_Pledge, Pledged, CNS_FTD, BRK_FTD, DVP_FTD, \r\n" +
                    "  ExDeficit, BankLoanRelease, OCC_Release \r\n" +
                    "From dbo.#BankLoanRelease With (NOLOCK)  \r\n" +
                    "Order By BookGroup, SecId \r\n" +
                    " \r\n" +
                    "Drop Table dbo.#BankLoanRelease  \r\n";

                SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
                dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandTimeout = 900;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsBankLoanRelease, "BankLoanRelease");
            }

            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.BankLoanReleaseReportGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (localDbCn.State != ConnectionState.Closed)
                {
                    localDbCn.Close();
                }
            }

            return Functions.ConvertDataSet(dsBankLoanRelease);
        }

        public byte[] MemoSegLockupGet(string system)
        {
            DataSet dsLockups = new System.Data.DataSet();
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));

            try
            {
                SqlCommand dbCmd = new SqlCommand("spMemoSegStartOfDayExDeficitGet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

                SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 10);
                paramSystem.Value = system;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsLockups, "Lockups");

                dsLockups.Tables["Lockups"].Columns.Add("ExcessDTC");
                dsLockups.AcceptChanges();

                dsLockups.Tables["Lockups"].Columns.Add("Release");
                dsLockups.AcceptChanges();

                dsLockups.Tables["Lockups"].Columns.Remove("Isin");
                dsLockups.AcceptChanges();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.MemoSegLockupGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return Functions.ConvertDataSet(dsLockups);
        }

        public void MemoSegStartOfDaySet(string bizDate, string system, string secId, string quantity)
        {
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));
            try
            {
                SqlCommand dbCmd = new SqlCommand("spMemoSegStartOfDayExDeficitSet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;


                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 5);
                paramSystem.Value = system;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@ExDeficit", SqlDbType.Decimal);
                paramQuantity.Value = quantity;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.MemoSegLockupGet]", Log.Error, 1);
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

        public byte[] PenaltyBoxGet(string bizDate)
        {
            DataSet dsLockups = new System.Data.DataSet();
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBorrowPenaltyGet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;             

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsLockups, "PenaltyBox");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.PenaltyBoxGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return Functions.ConvertDataSet(dsLockups);
        }

        public void PenaltyBoxItemSet(string bizDate, string secId, bool isDelete)
        {
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));
            try
            {
                SqlCommand dbCmd = new SqlCommand("spBorrowPenaltyItemSet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;
              
                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
                paramDelete.Value = isDelete;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.PenaltyBoxItemSet]", Log.Error, 1);
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

        public byte[] RecallTradingGet(string bizDate, string bookGroup)
        {
            DataSet dsRecalls = new System.Data.DataSet();
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));

            try
            {
                SqlCommand dbCmd = new SqlCommand("spRecallTradingGet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsRecalls, "RecallTrading");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.RecallTradingGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return Functions.ConvertDataSet(dsRecalls);
        }

        public void RecallTradingSet(string bizDate, string bookGroup, string secId, string comment)
        {
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));
            try
            {
                SqlCommand dbCmd = new SqlCommand("spRecallTradingSet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 8000);
                paramComment.Value = comment;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.RecallTradingSet]", Log.Error, 1);
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

        public byte[] ShortSaleBillingBPSItemSet(string bizDate, string accountNumber, string secId, string quantityShorted, string quantityCovered, string settlementDate, string price, string rate, string actUserId)
        {
            DataSet dsSortSaleBillingItem = new DataSet();

            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;

            try
            {
                string dbCnStr = GetDatabaseConnection("Sendero");

                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spShortSaleBillingSummaryBPSItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);
                paramAccountNumber.Value = accountNumber;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantityShorted = dbCmd.Parameters.Add("@QuantityShorted", SqlDbType.BigInt);
                paramQuantityShorted.Value = quantityShorted;

                SqlParameter paramQuantityCovered = dbCmd.Parameters.Add("@QuantityCovered", SqlDbType.BigInt);
                paramQuantityCovered.Value = quantityCovered;

                SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime);
                paramSettleDate.Value = settlementDate;

                SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Decimal);
                paramPrice.Value = price;

                SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
                paramRate.Value = rate;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsSortSaleBillingItem, "Item");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.ShortSaleBillingBPSItemSet]", Log.Error, 1);
                throw;
            }

            return Functions.ConvertDataSet(dsSortSaleBillingItem);
        }

        public byte[] CollateralizationUtilGet(string bizDate)
        {
            DataSet dsCollateral = new System.Data.DataSet();
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));

            try
            {
                SqlCommand dbCmd = new SqlCommand("spCollateralUtilizationGet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 12000;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;          

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsCollateral, "Collateral");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.CollateralizationUtilGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return Functions.ConvertDataSet(dsCollateral);
        }

        public byte[] CollateralizationUtilDetailGet(string bizDate, string classGroup)
        {
            DataSet dsCollateral = new System.Data.DataSet();
            SqlConnection dbCn = new SqlConnection(GetDatabaseConnection("Sendero"));

            try
            {
                SqlCommand dbCmd = new SqlCommand("spCollateralUtilizationDetailGet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 12000;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramClassGroup = dbCmd.Parameters.Add("@ClassGroup", SqlDbType.VarChar, 100);
                paramClassGroup.Value = classGroup;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsCollateral, "CollateralDetail");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [PositionAgent.CollateralizationUtilDetailGet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return Functions.ConvertDataSet(dsCollateral);
        }
    }
}
