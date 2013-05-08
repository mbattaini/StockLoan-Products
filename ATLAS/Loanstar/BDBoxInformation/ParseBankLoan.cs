using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StockLoan.ParsingFiles
{
    class ParseBankLoan
    {
        public event EventHandler<ProgressEventArgs> ProgressChanged = delegate { };

        private DataSet dsBankLoan;
        private string dbCnStr;
        private string bookGroup;
        private int interval;

        public ParseBankLoan(string dbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;
            this.bookGroup = bookGroup;
            this.interval = 100;
        }

        public void Load(string filePath, string bizDatePrior, string bizDate)
        {
            BoxBankLoanPurge(bizDate);

            TextReader textReader = new StreamReader(filePath, Encoding.ASCII);
            
            string line = "";
            string signedChar = "";
            string sign = "";

            dsBankLoan = new DataSet();
            dsBankLoan.Tables.Add("BankLoan");
            dsBankLoan.Tables["BankLoan"].Columns.Add("Book");
            dsBankLoan.Tables["BankLoan"].Columns.Add("SecId");
            dsBankLoan.Tables["BankLoan"].Columns.Add("LoanDate");
            dsBankLoan.Tables["BankLoan"].Columns.Add("Quantity");
            dsBankLoan.AcceptChanges();

            line = textReader.ReadLine();

            if (!CheckFileDate(line.Substring(12, 8), bizDatePrior))
            {
                throw new Exception("File is not for today.");                
            }

            while (true)
            {
                line = textReader.ReadLine();

                if (line == null)
                {
                    break;
                }

                if (line.Length >= 118)
                {
                    if (line.Substring(32, 3).Trim().Equals("014"))
                    {
                        DataRow drTemp = dsBankLoan.Tables["BankLoan"].NewRow();
                        drTemp["Book"] = line.Substring(18, 4);
                        drTemp["SecId"] = line.Substring(0, 9);
                        drTemp["LoanDate"] = line.Substring(99, 6);

                        switch (line.Substring(58, 1))
                        {
                            case "{":
                                signedChar = "0";
                                break;
                            case "A":
                                signedChar = "1";
                                break;
                            case "B":
                                signedChar = "2";
                                break;
                            case "C":
                                signedChar = "3";
                                break;
                            case "D":
                                signedChar = "4";
                                break;
                            case "E":
                                signedChar = "5";
                                break;
                            case "F":
                                signedChar = "6";
                                break;
                            case "G":
                                signedChar = "7";
                                break;
                            case "H":
                                signedChar = "8";
                                break;
                            case "I":
                                signedChar = "9";
                                break;
                            case "}":
                                signedChar = "0";
                                sign = "-";
                                break;
                            case "J":
                                signedChar = "1";
                                sign = "-";
                                break;
                            case "K":
                                signedChar = "2";
                                sign = "-";
                                break;
                            case "L":
                                signedChar = "3";
                                sign = "-";
                                break;
                            case "M":
                                signedChar = "4";
                                sign = "-";
                                break;
                            case "N":
                                signedChar = "5";
                                sign = "-";
                                break;
                            case "O":
                                signedChar = "6";
                                sign = "-";
                                break;
                            case "P":
                                signedChar = "7";
                                sign = "-";
                                break;
                            case "Q":
                                signedChar = "8";
                                sign = "-";
                                break;
                            case "R":
                                signedChar = "9";
                                sign = "-";
                                break;
                            default:
                                signedChar = "0";
                                break;
                        }

                        drTemp["Quantity"] = sign + line.Substring(46, 12) + signedChar;
                        dsBankLoan.Tables["BankLoan"].Rows.Add(drTemp);
                    }
                }
            }
            if (dsBankLoan.Tables["BankLoan"].Rows.Count > 0)
            {
                LoadDatabase(bizDate);
            }
        }

        public bool CheckFileDate(string fileDate, string bizDate)
        {
            if (DateTime.ParseExact(fileDate, "MM/dd/yy", null).ToString("yyyy-MM-dd").Equals(bizDate))
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
            int count = 0;

            foreach (DataRow drBankLoanItem in dsBankLoan.Tables["BankLoan"].Rows)
            {
                BoxBankLoanItemSet(
                    bizDate,
                    bookGroup,
                    drBankLoanItem["Book"].ToString(),
                    drBankLoanItem["SecId"].ToString(),
                    drBankLoanItem["LoanDate"].ToString(),
                    drBankLoanItem["Quantity"].ToString());

                count++;

                if ((count % interval) == 0)
                {
                    UpdateProgress(count);
                }
            }
        }


        public void UpdateProgress(long count)
        {
            EventHandler<ProgressEventArgs> progressEvent = ProgressChanged;

            progressEvent(null, new ProgressEventArgs(count));
        }

        private void BoxBankLoanItemSet(string bizDate, string bookGroup, string book, string secId, string loanDate, string quantity)
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxBankLoanItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramLoanDate = dbCmd.Parameters.Add("@LoanDate", SqlDbType.VarChar, 12);
                paramLoanDate.Value = loanDate;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseBankLoan.BoxBankLoanItemSet]");
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        private void BoxBankLoanPurge(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxBankLoanPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message + "[ParseBankLoan.BoxBankLoanPurge]");
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
                return dsBankLoan.Tables["BankLoan"].Rows.Count;
            }
        }
    }
}
