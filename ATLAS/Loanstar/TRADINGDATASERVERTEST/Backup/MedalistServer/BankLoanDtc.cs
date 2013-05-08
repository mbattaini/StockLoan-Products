using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class BankLoanDtc
  {
    private SqlConnection dbCn;

    public BankLoanDtc(string dbCnStr) : this(new SqlConnection(dbCnStr)) {}
    public BankLoanDtc(SqlConnection dbCn)
    {
      this.dbCn = dbCn;
    }

    public bool Reset(string bizDatePriorBank, string bookGroup, string path, string host, string userId, string password)
    {
      string headerFlag = "HDR" + bookGroup.PadLeft(8, '0') + "APIBALAPIBAL" + Master.BizDatePriorBank.Replace("-", ""); 
      string trailerFlag = "TRL" + bookGroup.PadLeft(8, '0') + "APIBALAPIBAL" + Master.BizDatePriorBank.Replace("-", ""); 

      string	line;	
			string	fileContents;
      string	signedChar = "";
			string	sign = "";

			int lineCount = 0;
			int stringLength = 980;

			long recordCountExpected = -1; 

      long recordCount = 0;
      long pledgeCount = 0;

      Common.Filer filer = new Common.Filer(MedalistMain.TempPath + "bankLoan" + bookGroup + ".txt");
      filer.Timeout = 90000;

      SqlCommand dbCmd;       
      StreamReader streamReader = null;

      Log.Write("Opening file " + path + " on host " + host + ". [BankLoanDtc.Reset]", 2);

      try
      {
        streamReader = new StreamReader(
          filer.StreamGet(
          path,
          host,
          userId,
          password),
          System.Text.Encoding.ASCII);     
        streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

        dbCmd = new SqlCommand("spBankLoanPositionPurge", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        dbCmd.CommandText = "spBankLoanPositionInsert";

        SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
        paramBookGroup.Value = bookGroup;

        SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
        SqlParameter paramLoanDate = dbCmd.Parameters.Add("@LoanDate", SqlDbType.DateTime);
        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
        SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
        SqlParameter paramLastActivityDate = dbCmd.Parameters.Add("@LastActivityDate", SqlDbType.DateTime);

        fileContents = streamReader.ReadToEnd();
				
				lineCount = 1;

				while ( fileContents.Length > (stringLength * (lineCount-1)))
        {          
					line = fileContents.Substring(stringLength * (lineCount - 1), stringLength);	
					
					switch (line.Substring(0, 3))
					{
						case "HDR" :
							if (line.Substring(0, 31).Equals(headerFlag))
							{
								recordCountExpected = long.Parse(line.Substring(51, 8));
							}
							else
							{
								Log.Write("File is not current for book group " + bookGroup + ". [BankLoanDtc.Reset]", 2);
								return false;
							}

							break;

						case "TRL" :
							break;

						default :
							recordCount++;

							if (line.Substring(32, 3).Equals("014"))
							{
								sign = "";
								pledgeCount++;

								paramBook.Value = line.Substring(18, 4);                
								paramLoanDate.Value = DateTime.ParseExact(line.Substring(99, 6), "yyMMdd", null).ToString();								                																
								paramSecId.Value = line.Substring(0, 9);                																								

								switch(line.Substring(58, 1))
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

								paramQuantity.Value = sign + line.Substring(46, 12) + signedChar;
								paramLastActivityDate.Value = DateTime.ParseExact(line.Substring(40, 6), "yyMMdd", null).ToString();														
        
								dbCmd.ExecuteNonQuery();
							}

							break;
					}

				lineCount++;
        }

        if (recordCountExpected.Equals(-1)) // Header record did not exist.
        {
          Log.Write("Data file does not appear to be APIBAL from the DTCC. [BankLoanDtc.Reset]", Log.Error, 1);
        }
        else if (recordCount.Equals(recordCountExpected))
        {
          Log.Write("Reset " + pledgeCount + " bank loan records out of " + recordCount + " for book group " + bookGroup + ". [BankLoanDtc.Reset]", 2);
          return true;
        }
        else
        {
          Log.Write("Parity error reading data file; processed " + recordCount + " records out of an expected " + recordCountExpected + ". [BankLoanDtc.Reset]", Log.Error, 1);
        }
      }
      catch (Exception e)
      {
        if (e.Message.IndexOf("The system cannot find the file specified") > -1)
        {
          Log.Write("File does not exist for book group " + bookGroup + ". [BankLoanDtc.Reset]", 2);
        }
        else
        {
          throw;
        }
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed)
        {
          dbCn.Close();
        }
          
        if (streamReader != null)
        {
          streamReader.Close(); 
        }
      }

      return false;
    }
  }
}
