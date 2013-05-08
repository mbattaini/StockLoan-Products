using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBAresReport
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet AresReportMasterGet(string bookGroup, string reportName, bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spAresReportMasterGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                if (!reportName.Equals(""))
                {
                    SqlParameter paramXmlReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 100);
                    paramXmlReportName.Value = reportName;
                }
                //if (!bizDate.Equals(""))
                //{
                //    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                //    paramBizDate.Value = bizDate;
                //}
                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "AresReportMaster");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void AresReportMasterSet(string bookGroup, string reportName, string reportDefinitionXml, 
                                               string reportFileType, string reportMakeTime, string lastRunDate, string reportStatus, 
                                               string items, bool useWeekends, string comment, string actUserId, bool isActive )
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {  
                SqlCommand dbCmd = new SqlCommand("dbo.spAresReportMasterSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 100);
                paramReportName.Value = reportName;

                SqlParameter paramReportDefinitionXml = dbCmd.Parameters.Add("@ReportDefinitionXml", SqlDbType.VarChar, 100);
                paramReportDefinitionXml.Value = reportDefinitionXml;

                if (!reportFileType.Equals(""))
                {
                    SqlParameter paramReportFileType = dbCmd.Parameters.Add("@ReportFileType", SqlDbType.VarChar, 20);
                    paramReportFileType.Value = reportFileType;
                }
                if (!reportMakeTime.Equals(""))
                {
                    SqlParameter paramReportMakeTime = dbCmd.Parameters.Add("@ReportMakeTime", SqlDbType.VarChar, 10);
                    paramReportMakeTime.Value = reportMakeTime;
                }
                if (!lastRunDate.Equals(""))
                {
                    SqlParameter paramLastRunDate = dbCmd.Parameters.Add("@LastRunDate", SqlDbType.DateTime);
                    paramLastRunDate.Value = lastRunDate;
                }
                if (!reportStatus.Equals(""))
                {
                    SqlParameter paramReportStatus = dbCmd.Parameters.Add("@ReportStatus", SqlDbType.VarChar, 500);
                    paramReportStatus.Value = reportStatus;
                }
                if (!items.Equals(""))
                {
                    SqlParameter paramItems = dbCmd.Parameters.Add("@Items", SqlDbType.BigInt);
                    paramItems.Value = long.Parse(items);
                }
                if (!useWeekends.Equals(""))
                {
                    SqlParameter paramUseWeekends = dbCmd.Parameters.Add("@UseWeekends", SqlDbType.Bit);
                    paramUseWeekends.Value = useWeekends;
                }
                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 500);
                    paramComment.Value = comment;
                }
                if (!actUserId.Equals(""))
                {
                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = actUserId;
                }
                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
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

        public static DataSet AresReportDeliveryGet(string bookGroup, string reportName, string fileName, string lastDeliveredDate, bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spAresReportDeliveryGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "600"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                if (!reportName.Equals(""))
                {
                    SqlParameter paramXmlReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 100);
                    paramXmlReportName.Value = reportName;
                }
                if (!fileName.Equals(""))
                {
                    SqlParameter paramXmlFileName = dbCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 200);
                    paramXmlFileName.Value = fileName;
                }
                if (!lastDeliveredDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@LastDeliveredDate", SqlDbType.DateTime);
                    paramBizDate.Value = lastDeliveredDate;
                }
                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "AresReportDelivery");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void AresReportDeliverySet(string aresReportDeliveryId, string bookGroup, string reportName, 
                                                 string lastDeliveredDate, string comment, string actUserId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spAresReportDeliverySet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramAresReportDeliveryId = dbCmd.Parameters.Add("@AresReportDeliveryId", SqlDbType.BigInt);
                paramAresReportDeliveryId.Value = long.Parse(aresReportDeliveryId);

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 100);
                paramReportName.Value = reportName;

                if (!lastDeliveredDate.Equals(""))
                {
                    SqlParameter paramLastDeliveredDate = dbCmd.Parameters.Add("@LastDeliveredDate", SqlDbType.DateTime);
                    paramLastDeliveredDate.Value = DateTime.Parse(lastDeliveredDate);
                }
                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 500);
                    paramComment.Value = comment;
                }
                if (!actUserId.Equals(""))
                {
                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = actUserId;
                }

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
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

        public static void AresReportDeliverySet(string aresReportDeliveryId, string bookGroup, string reportName, string fileName, 
                                                 string reportRecipient, string lastDeliveredDate, string deliveryMethod, string mailSubject, 
                                                 string mailContent, bool isAttachFile, bool isEmbedDataInMail, bool isEmbedDisclaimer, 
                                                 bool isUseEncryption, string comment, string actUserId, bool isActive) 
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spAresReportDeliverySet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramAresReportDeliveryId = dbCmd.Parameters.Add("@AresReportDeliveryId", SqlDbType.BigInt);
                paramAresReportDeliveryId.Value = long.Parse(aresReportDeliveryId);

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 100);
                paramReportName.Value = reportName;

                if (!fileName.Equals(""))
                {
                    SqlParameter paramFileName = dbCmd.Parameters.Add("@FileName", SqlDbType.VarChar, 200);
                    paramFileName.Value = fileName;
                }
                if (!reportRecipient.Equals(""))
                {
                    SqlParameter paramReportRecipient = dbCmd.Parameters.Add("@ReportRecipient", SqlDbType.VarChar, 500);
                    paramReportRecipient.Value = reportRecipient;
                }
                if (!lastDeliveredDate.Equals(""))
                {
                    SqlParameter paramLastDeliveredDate = dbCmd.Parameters.Add("@LastDeliveredDate", SqlDbType.DateTime);
                    paramLastDeliveredDate.Value = DateTime.Parse(lastDeliveredDate);
                }
                if (!deliveryMethod.Equals(""))
                {
                    SqlParameter paramDeliveryMethod = dbCmd.Parameters.Add("@DeliveryMethod", SqlDbType.VarChar, 20);
                    paramDeliveryMethod.Value = deliveryMethod;
                }
                if (!mailSubject.Equals(""))
                {
                    SqlParameter paramMailSubject = dbCmd.Parameters.Add("@MailSubject", SqlDbType.VarChar, 200);
                    paramMailSubject.Value = mailSubject;
                }
                if (!mailContent.Equals(""))
                {
                    SqlParameter paramMailContent = dbCmd.Parameters.Add("@MailContent", SqlDbType.VarChar, 500);
                    paramMailContent.Value = mailContent;
                }
                SqlParameter paramIsAttachFile = dbCmd.Parameters.Add("@IsAttachFile", SqlDbType.Bit);
                paramIsAttachFile.Value = isAttachFile;

                SqlParameter paramIsEmbedDataInMail = dbCmd.Parameters.Add("@IsEmbedDataInMail", SqlDbType.Bit);
                paramIsEmbedDataInMail.Value = isEmbedDataInMail;

                SqlParameter paramIsEmbedDisclaimer = dbCmd.Parameters.Add("@IsEmbedDisclaimer", SqlDbType.Bit);
                paramIsEmbedDisclaimer.Value = isEmbedDisclaimer;

                SqlParameter paramIsUseEncryption = dbCmd.Parameters.Add("@IsUseEncryption", SqlDbType.Bit);
                paramIsUseEncryption.Value = isUseEncryption;

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 500);
                    paramComment.Value = comment;
                }
                if (!actUserId.Equals(""))
                {
                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = actUserId;
                }
                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
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

        public static string AresReportKeywordParse(string bookGroup, string keyword, string keywordType, string keywordValue, string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            string returnValue = "";

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spAresReportKeywordParse", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramKeyword = dbCmd.Parameters.Add("@Keyword", SqlDbType.VarChar, 100);
                paramKeyword.Value = keyword;

                SqlParameter paramKeywordType = dbCmd.Parameters.Add("@KeywordType", SqlDbType.VarChar, 20);
                paramKeywordType.Value = keywordType;

                if (!keywordValue.Equals(""))
                {
                    SqlParameter paramKeywordValue = dbCmd.Parameters.Add("@KeywordValue", SqlDbType.VarChar, 100);
                    paramKeywordValue.Value = keywordValue;
                }
                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }

                SqlParameter paramReturnValue = dbCmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 100);
                paramReturnValue.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                returnValue = paramReturnValue.Value.ToString();
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

            return returnValue;
        }

        public static DataSet AresReportDatasetGet(string sqlScript, string bookGroup, string bizDate)
        {
            //DC Returns Dataset based on SQL Script 
            //   Sql Script should frist be parsed/replaced embedded Keyword with literal value using AresReportKeywordParse function) 

            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                if (!sqlScript.Equals(""))
                {
                    SqlCommand dbCmd = new SqlCommand(sqlScript, dbCn);
                    dbCmd.CommandType = CommandType.Text;
                    dbCmd.CommandTimeout = 900;     //int.Parse(Standard.ConfigValue("DatabaseTimeout", "900"));

                    /* ------------------------------------------------------------- 
                    if (!bookGroup.Equals(""))
                    {
                        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                        paramBookGroup.Value = bookGroup;
                    }
                    if (!bizDate.Equals(""))
                    {
                        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                        paramBizDate.Value = bizDate;
                    }
                    ------------------------------------------------------------- */

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                    dataAdapter.Fill(dsTemp, "AresReportDataset");

                }
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }


    }

}
