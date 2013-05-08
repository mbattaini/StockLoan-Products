using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using StockLoan.Common;

namespace BroadRidge.BusinessFiles
{
    public class BankLoanApibalParser
    {
        private string filePath = "";
        private string bizDatePrior = "";
        private string bizDate = "";
        private string bookGroup = "";
        private string dbCnStr = "";
        private DataSet dsBankLoan;

        public BankLoanApibalParser(string filePath, string bizDatePrior, string bizDate, string bookGroup, string dbCnStr)
        {
            this.filePath = filePath;
            this.bizDatePrior = bizDatePrior;
            this.bizDate = bizDate;
            this.bookGroup = bookGroup;
            this.dbCnStr = dbCnStr;
        }

        public bool CheckFileHeaderDate()
        {
            bool successful = false;

            string line;
            string fileHeaderDate = "";

            // read one line

            TextReader textReader = new StreamReader(filePath);

            line = textReader.ReadLine();

            if (!line.Equals(""))
            {
                fileHeaderDate = line.Substring(12, 8);
            }

            try
            {
                if (DateTime.ParseExact(fileHeaderDate, "MM/dd/yy", null).ToString("yyyy-MM-dd").Equals(bizDatePrior))
                {
                    successful = true;
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                successful = false;
            }
            finally
            {
                textReader.Close();
            }

            return successful;
        }

        public void Load()
        {
            long counter = 0;
            string line = "-1";
            string sign = "";
            string signedChar = "";
            string pledged = "";

            dsBankLoan = new DataSet();

            dsBankLoan.Tables.Add("Apibal");
            dsBankLoan.Tables["Apibal"].Columns.Add("BookGroup");
            dsBankLoan.Tables["Apibal"].Columns.Add("Book");
            dsBankLoan.Tables["Apibal"].Columns.Add("LoanDate");
            dsBankLoan.Tables["Apibal"].Columns.Add("SecId");
            dsBankLoan.Tables["Apibal"].Columns.Add("Pledged");
            dsBankLoan.Tables["Apibal"].Columns.Add("LastActivityDate");
            dsBankLoan.AcceptChanges();

            if (!CheckFileHeaderDate())
            {
                throw new Exception("File Date is not for today.");
            }


            DatabaseFunctions.SenderoDatabaseFunctions.BankLoanBPSPurge(dbCnStr, "0158");
            Log.Write("Purged bank loan record items. [BankLoanApibalParser.Load]", 1);


            string[] fileContents = File.ReadAllLines(filePath, Encoding.ASCII);
            Log.Write("Will start loading bank loan items for " + bizDate + ". [BankLoanApibalParser.Load]", 1);

            for (long index = 0; index < fileContents.LongLength; index++)
            {
                try
                {
                    if (fileContents[index].Equals(""))
                    {
                        break;
                    }


                    if (fileContents[index].Length >= 119)
                    {
                        if (fileContents[index].Substring(17, 4).Contains("0902"))
                        {
                            DataRow drTemp = dsBankLoan.Tables["Apibal"].NewRow();

                            drTemp["BookGroup"] = "0158";
                            drTemp["Book"] = fileContents[index].Substring(17, 4);
                            drTemp["LoanDate"] = fileContents[index].Substring(100, 6);                            
                            drTemp["SecId"] = fileContents[index].Substring(0, 9);

                            Log.Write(fileContents[index], 1);

                            switch (fileContents[index].Substring(57, 1))
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

                            pledged = sign + fileContents[index].Substring(46, 12) + signedChar;


                            drTemp["Pledged"] = pledged;
                            drTemp["LastActivityDate"] = DateTime.Now.ToString(Standard.DateFormat);
                          

                            dsBankLoan.Tables["Apibal"].Rows.Add(drTemp);

                            counter++;

                            if ((counter % 100) == 0)
                            {
                                Log.Write("Read : " + counter + " bank loan items. [BankLoanApibalParser.Load]", 1);
                            }
                        }
                        
                        
                        else if (fileContents[index].Substring(32, 3).Trim().Equals("014"))
                        {
                            DataRow drTemp = dsBankLoan.Tables["Apibal"].NewRow();

                            drTemp["BookGroup"] = "0158";
                            drTemp["Book"] = fileContents[index].Substring(18, 4);
                            drTemp["LoanDate"] = fileContents[index].Substring(99, 6);
                            drTemp["SecId"] = fileContents[index].Substring(0, 9);

                            switch (fileContents[index].Substring(58, 1))
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

                            pledged = sign + fileContents[index].Substring(46, 12) + signedChar;


                            drTemp["Pledged"] = pledged;
                            if (!fileContents[index].Substring(40, 6).Equals("000000"))
                            {
                                drTemp["LastActivityDate"] = fileContents[index].Substring(40, 6);
                            }
                            else
                            {
                                drTemp["LastActivityDate"] = DateTime.Now.ToString(Standard.DateFormat);
                            }

                            dsBankLoan.Tables["Apibal"].Rows.Add(drTemp);

                            counter++;

                            if ((counter % 100) == 0)
                            {
                                Log.Write("Read : " + counter + " bank loan items. [BankLoanApibalParser.Load]", 1);
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);
                }
            }

            Log.Write("Loaded " + counter.ToString("#,##0") + " bank loan items. [BankLoanApibalParser.Load]", 1);

             
        }

        public void LoadDatabase()
        {         
            foreach (DataRow drTemp in dsBankLoan.Tables["Apibal"].Rows)
            {
                DatabaseFunctions.SenderoDatabaseFunctions.BankLoanBPSPositionInsert(
                    drTemp["BookGroup"].ToString(),
                    drTemp["Book"].ToString(),
                    DateTime.ParseExact(drTemp["LoanDate"].ToString(), "yyMMdd", null).ToString(Standard.DateFormat),
                    drTemp["SecId"].ToString(),
                    drTemp["Pledged"].ToString(),
                    DateTime.Now.ToString(Standard.DateFormat),
                    dbCnStr);
            }


            StockLoan.BDData.Email.Send(KeyValue.Get("BroadRigdeBankLoanApibalEmailTo", "mbattaini@penson.com", dbCnStr),
                                      KeyValue.Get("BroadRigdeBankLoanApibalEmailFrom", "stockloan@penson.com", dbCnStr),
                                      KeyValue.Get("BroadRigdeBankLoanApibalEmailSubject", "Broad Ridge Bank Loan APIBAL Upload", dbCnStr),
                                      "Loaded " + dsBankLoan.Tables["Apibal"].Rows.Count.ToString("#,##0") + " bank loan APIBAL items.",
                                      dbCnStr);
        }
    }
}
