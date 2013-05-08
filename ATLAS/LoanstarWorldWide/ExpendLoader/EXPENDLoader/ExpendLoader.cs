using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using StockLoan.Common;    

namespace StockLoan.InventoryService
{
    class ExpendLoader
    {
        private string filePath;
        private string fileName;
        private string bizDate;
        private string bizDatePrior;
        private string dbCnStr;

        public ExpendLoader(string filePath, string fileName, string bizDate, string bizDatePrior)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.dbCnStr = "Trusted_Connection=yes; " +
                            "Data Source=" + Common.Standard.ConfigValue("MainDatabaseHost", "") + "; " +
                            "Initial Catalog=" + Common.Standard.ConfigValue("MainDatabaseName", "") + ";";
            this.bizDate = bizDate;
            this.bizDatePrior = KeyValue.Get("BizDatePrior", "", dbCnStr);
        }

        public bool CheckFileDate()
        {
            bool success = false;

            if (File.GetLastWriteTime(filePath + fileName) > DateTime.Parse(bizDatePrior))
            {
                success = true;
            }

            return success;
        }

        public void PendingExcessSet(string bizDate, string clientNumber, string bpsSecurityNumber, 
                            string cusip, string price, string closingPendingExcess,
                            string todayForTomorrow, string optimizationSeg, string cashManagementSeg, string unpaidCash,
                            string pdq, string siacNightActivity, string legalAdjustments, string cmsControlAdj,
                            string startingCMSPendingExcess, string pendingExcessFinal, string marketValueFinal, string actUserId, string dbCnStr )
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spPendingExcessSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramClientNumber = dbCmd.Parameters.Add("@ClientNumber", SqlDbType.VarChar, 3);
                paramClientNumber.Value = clientNumber;

                SqlParameter paramBPSSecurityNumber = dbCmd.Parameters.Add("@BPSSecurityNumber", SqlDbType.VarChar, 7);
                paramBPSSecurityNumber.Value = bpsSecurityNumber;

                if (!cusip.Equals(""))
                {
                    SqlParameter paramCUSIP = dbCmd.Parameters.Add("@CUSIP", SqlDbType.VarChar, 9);
                    paramCUSIP.Value = cusip;
                }
                if (!price.Equals(""))
                {
                    SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Decimal);
                    paramPrice.Value = decimal.Parse(price);
                }
                if (!closingPendingExcess.Equals(""))
                {
                    SqlParameter paramClosingPendingExcess = dbCmd.Parameters.Add("@ClosingPendingExcess", SqlDbType.BigInt);
                    paramClosingPendingExcess.Value = long.Parse(closingPendingExcess);
                }
                if (!todayForTomorrow.Equals(""))
                {
                    SqlParameter paramTodayForTomorrow = dbCmd.Parameters.Add("@TodayForTomorrow", SqlDbType.BigInt);
                    paramTodayForTomorrow.Value = long.Parse(todayForTomorrow);
                }                
                if (!optimizationSeg.Equals(""))
                {
                    SqlParameter paramOptimizationSeg = dbCmd.Parameters.Add("@OptimizationSeg", SqlDbType.BigInt);
                    paramOptimizationSeg.Value = long.Parse(optimizationSeg);
                }                
                if (!cashManagementSeg.Equals(""))
                {
                    SqlParameter paramCashManagementSeg = dbCmd.Parameters.Add("@CashManagementSeg", SqlDbType.BigInt);
                    paramCashManagementSeg.Value = long.Parse(cashManagementSeg);
                }                
                if (!unpaidCash.Equals(""))
                {
                    SqlParameter paramUnpaidCash = dbCmd.Parameters.Add("@UnpaidCash", SqlDbType.BigInt);
                    paramUnpaidCash.Value = long.Parse(unpaidCash);
                }                
                if (!pdq.Equals(""))
                {
                    SqlParameter paramPDQ = dbCmd.Parameters.Add("@PDQ", SqlDbType.BigInt);
                    paramPDQ.Value = long.Parse(pdq);
                }
                if (!siacNightActivity.Equals(""))
                {
                    SqlParameter paramSiacNightActivity = dbCmd.Parameters.Add("@SiacNightActivity", SqlDbType.BigInt);
                    paramSiacNightActivity.Value = long.Parse(siacNightActivity);
                }
                if (!legalAdjustments.Equals(""))
                {
                    SqlParameter paramLegalAdjustments = dbCmd.Parameters.Add("@LegalAdjustments", SqlDbType.BigInt);
                    paramLegalAdjustments.Value = long.Parse(legalAdjustments);
                }
                if (!cmsControlAdj.Equals(""))
                {
                    SqlParameter paramCMSControlAdj = dbCmd.Parameters.Add("@CMSControlAdj", SqlDbType.BigInt);
                    paramCMSControlAdj.Value = long.Parse(cmsControlAdj);
                }
                if (!startingCMSPendingExcess.Equals(""))
                {
                    SqlParameter paramStartingCMSPendingExcess = dbCmd.Parameters.Add("@StartingCMSPendingExcess", SqlDbType.BigInt);
                    paramStartingCMSPendingExcess.Value = long.Parse(startingCMSPendingExcess);
                }
                if (!pendingExcessFinal.Equals(""))
                {
                    SqlParameter paramPendingExcessFinal = dbCmd.Parameters.Add("@PendingExcessFinal", SqlDbType.BigInt);
                    paramPendingExcessFinal.Value = long.Parse(pendingExcessFinal);
                }
                if (!marketValueFinal.Equals(""))
                {
                    SqlParameter paramMarketValueFinal = dbCmd.Parameters.Add("@MarketValueFinal", SqlDbType.Decimal);
                    paramMarketValueFinal.Value = decimal.Parse(marketValueFinal);
                }
                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch
            {
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


        public int LoadFile()
        {
            int rowCount = 0;
            bool errFlag = false;
            int errCount = 0;
            string errInfo = "";

            StreamReader streamReader = null;
            string line;
            char sign;                       

            string clientNumber;             
            string bpsSecurityNumber;        
            string cusip;
            string price;                    
            string closingPendingExcess;     
            string todayForTomorrow;         
            string optimizationSeg;          
            string cashManagementSeg;        
            string unpaidCash;               
            string pdq;                      
            string siacNightActivity;        
            string legalAdjustments;         
            string cmsControlAdj;            
            string startingCMSPendingExcess; 
            string pendingExcessFinal;       
            string marketValueFinal;         
            string actUserId = "EXPENDLoader";

            if (!CheckFileDate())
            {
                throw new Exception("EXPEND file is not for today.");
            }

            try
            {
                streamReader = new StreamReader(filePath + fileName);
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                while (!streamReader.EndOfStream)       
                {
                    line = streamReader.ReadLine();       

                    clientNumber = "";
                    bpsSecurityNumber = "";
                    cusip = "";
                    price = "";
                    closingPendingExcess = "";
                    todayForTomorrow = "";
                    optimizationSeg = "";
                    cashManagementSeg = "";
                    unpaidCash = "";
                    pdq = "";
                    siacNightActivity = "";
                    legalAdjustments = "";
                    cmsControlAdj = "";
                    startingCMSPendingExcess = "";
                    pendingExcessFinal = "";
                    marketValueFinal = "";
                    sign = ' ';

                    try
                    {
                        clientNumber = line.Substring(3, 3).Trim(); 
                        bpsSecurityNumber = line.Substring(6, 7).Trim();         
                        cusip = line.Substring(13, 9).Trim();
                        price = Decimal.Parse(line.Substring(25, 5).Trim().PadLeft(5, '0') + "." + line.Substring(30, 2).Trim().PadRight(2, '0')).ToString();  
                        sign = char.Parse(line.Substring(32, 1));      
                        closingPendingExcess = long.Parse(line.Substring(33, 13).Trim()).ToString().Trim();
                        closingPendingExcess = (sign.Equals('-')) ? (-1 * long.Parse(closingPendingExcess)).ToString() : closingPendingExcess;
                        todayForTomorrow = long.Parse(line.Substring(46, 14).Trim()).ToString().Trim();
                        optimizationSeg = long.Parse(line.Substring(60, 14).Trim()).ToString().Trim();
                        cashManagementSeg = long.Parse(line.Substring(74, 14).Trim()).ToString().Trim();
                        unpaidCash = long.Parse(line.Substring(88, 14).Trim()).ToString().Trim();
                        pdq = long.Parse(line.Substring(102, 14).Trim()).ToString().Trim();
                        siacNightActivity = long.Parse(line.Substring(116, 14).Trim()).ToString().Trim();
                        legalAdjustments = long.Parse(line.Substring(130, 14).Trim()).ToString().Trim();
                        cmsControlAdj = long.Parse(line.Substring(144, 14).Trim()).ToString().Trim();
                        sign = char.Parse(line.Substring(158, 1));      
                        startingCMSPendingExcess = long.Parse(line.Substring(159, 13).Trim()).ToString().Trim();
                        startingCMSPendingExcess = (sign.Equals('-')) ? (-1 * long.Parse(startingCMSPendingExcess)).ToString() : startingCMSPendingExcess;
                        sign = char.Parse(line.Substring(172, 1));      
                        pendingExcessFinal = long.Parse(line.Substring(173, 13).Trim()).ToString().Trim();
                        pendingExcessFinal = (sign.Equals('-')) ? (-1 * long.Parse(pendingExcessFinal)).ToString() : pendingExcessFinal;
                        sign = char.Parse(line.Substring(186, 1));      
                        marketValueFinal = Decimal.Parse(line.Substring(187, 11).Trim().PadLeft(11, '0') + "." + line.Substring(198, 2).Trim().PadRight(2, '0')).ToString();  
                        marketValueFinal = (sign.Equals('-')) ? (-1 * Decimal.Parse(marketValueFinal)).ToString() : marketValueFinal;

                        PendingExcessSet(bizDate, clientNumber, bpsSecurityNumber, 
                                         cusip, price, closingPendingExcess,
                                         todayForTomorrow, optimizationSeg, cashManagementSeg, unpaidCash,
                                         pdq, siacNightActivity, legalAdjustments, cmsControlAdj,
                                         startingCMSPendingExcess, pendingExcessFinal, marketValueFinal, actUserId, dbCnStr);
                        rowCount++;

                    }
                    catch (Exception err)
                    {
                        errFlag = true;
                        errInfo += (rowCount + 1).ToString() + ", ";
                        errCount++;
                    }

                } 

                //Log.Write("EXPEND Loader parse and load Completed. Loaded: " + rowCount.ToString() + " Excess/Pending items.   [ExpendLoader.LoadFile]", Log.Information, 2);
                return rowCount;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (errFlag.Equals(true) || (errCount > 0))
                {
                    //Log.Write("Expend Loader encountered " + errCount + " error.  [ExpendLoader.LoadFile]", Log.Error, 2);
                    //Log.Write("ERROR encountered in the following line(s): " + errInfo + "  [ExpendLoader.LoadFile]", Log.Error, 2); 
                    throw new Exception("EXPEND file load error, error count: " + errCount.ToString() + ". For Line# " + errInfo);
                }
            }
        }

    }
}
