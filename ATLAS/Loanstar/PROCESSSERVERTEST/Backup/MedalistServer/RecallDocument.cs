using System;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Medalist
{
	/// <summary>
	/// Generates a loan Termination notice.
	/// </summary>
  public class RecallDocument
  {
    SqlConnection dbCn = null;

    private string bookGroup;
    private string book;
    
    private string lenderAccountName;
    private string lenderAddressLine1;
    private string lenderAddressLine2;
    private string lenderAddressLine3;
    private string lenderPhone;
    private string lenderDtcId;    

    private string borrowerAccountName;
    private string borrowerAddressLine1;
    private string borrowerAddressLine2;
    private string borrowerAddressLine3;
    private string borrowerDtcId;
    private string borrowerFaxNumber;

    private string recallDate;
    private string defaultDate;
    private string reasonCode;

    private string recallId;
    private string contractId;
    
    private long quantity;
    private long quantitySettled;
    private decimal amountSettled;
    private string currencyIso;
    private string collateralCode;
    
    private string secId;
    private string symbol;
    private string description = "";
    private decimal rate;
    private decimal divRate;
    private string settleDate;
 
    public RecallDocument(string dbCnStr) : this(dbCnStr, null) {}
    public RecallDocument(string dbCnStr, string recallId)
    {
      dbCn = new SqlConnection(dbCnStr);
      RecallId = recallId;
    }

    public string RecallId
    {
      set
      {
        recallId = value;

        if ( (recallId == null) || recallId.Equals("") )
        {
          return;
        }
                
        SqlDataReader dataReader = null;

        SqlCommand dbCmd = new SqlCommand("spRecallsGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        try
        {
          SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.VarChar, 16);			
          paramRecallId.Value = recallId;

          dbCn.Open();
          dataReader = dbCmd.ExecuteReader();
                
          while (dataReader.Read()) // Expect just one row.
          {
            bookGroup = dataReader.GetValue(1).ToString();
            contractId = dataReader.GetValue(2).ToString();
            book = dataReader.GetValue(4).ToString();
            secId = dataReader.GetValue(6).ToString();
            symbol = dataReader.GetValue(7).ToString();
            quantity = (long) dataReader.GetValue(13);
            defaultDate = dataReader.GetValue(14).ToString();
            recallDate = dataReader.GetValue(16).ToString();
            reasonCode = dataReader.GetValue(17).ToString();
          }

          dataReader.Close();
          dbCmd.Parameters.Remove(paramRecallId);

          dbCmd.CommandText = "spRecallReasonsGet";

          SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.Char, 2);
          paramReasonCode.Value = reasonCode;

          dataReader = dbCmd.ExecuteReader();

          while (dataReader.Read()) // Expect just one row.
          {
            reasonCode = dataReader.GetValue(2).ToString();
          }

          dataReader.Close();
          dbCmd.Parameters.Remove(paramReasonCode);

          dbCmd.CommandText = "spContractGet";

          SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
          paramBookGroup.Value = bookGroup;

          SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 15);
          paramContractId.Value = contractId;
        
          SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
          paramContractType.Value = "L";

          dataReader = dbCmd.ExecuteReader();

          while (dataReader.Read()) // Expect just one row.
          {
            quantitySettled = (long) dataReader.GetValue(11);
            amountSettled = (decimal) dataReader.GetValue(16);
            collateralCode = dataReader.GetValue(18).ToString();
            settleDate = dataReader.GetValue(20).ToString();
            rate = (decimal) dataReader.GetValue(24);
            divRate = (decimal) dataReader.GetValue(28);
            currencyIso = dataReader.GetValue(33).ToString();
          }

          switch (collateralCode)
          {
            case "C" :
              collateralCode = "Cash";
              break;

            default :
              collateralCode = "Securities";
              break;
          }

          dataReader.Close();
          dbCmd.Parameters.Remove(paramContractId);
          dbCmd.Parameters.Remove(paramContractType);

          dbCmd.CommandText = "spBookGet";

          SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
          paramBook.Value = bookGroup;

          dataReader = dbCmd.ExecuteReader();

          while (dataReader.Read()) // Expect just one row.
          {
            lenderAccountName = dataReader.GetValue(3).ToString();
            lenderAddressLine1 = dataReader.GetValue(4).ToString();
            lenderAddressLine2 = dataReader.GetValue(5).ToString();
            lenderAddressLine3 = dataReader.GetValue(6).ToString();
            lenderPhone = dataReader.GetValue(7).ToString();
            lenderDtcId = dataReader.GetValue(8).ToString();
          }

          dataReader.Close();

          paramBook.Value = book;

          dataReader = dbCmd.ExecuteReader();

          while (dataReader.Read()) // Expect just one row.
          {
            borrowerAccountName = dataReader.GetValue(3).ToString();
            borrowerAddressLine1 = dataReader.GetValue(4).ToString();
            borrowerAddressLine2 = dataReader.GetValue(5).ToString();
            borrowerAddressLine3 = dataReader.GetValue(6).ToString();
            borrowerDtcId = dataReader.GetValue(8).ToString();
            borrowerFaxNumber = dataReader.GetValue(23).ToString();
          }

          dataReader.Close();
          dbCmd.Parameters.Remove(paramBookGroup);
          dbCmd.Parameters.Remove(paramBook);

          dbCmd.CommandText = "spSecMasterItemGet";

          SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
          paramSecId.Value = secId;

          dataReader = dbCmd.ExecuteReader();

          while (dataReader.Read()) // Expect just one row.
          {
            description = dataReader.GetValue(2).ToString();
          }
        }
        catch (Exception e)
        {        
          Log.Write(e.Message + " [PositionAgent.ContractSet]", Log.Error, 1);
          throw;
        }
        finally
        {
          if (dataReader != null && !dataReader.IsClosed)
          {
            dataReader.Close();
          }

          if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
          {
            dbCn.Close();
          }
        }
      }
    }
  
    public string TermNotice
    {
      get
      {
        if ((recallId == null) || recallId.Equals("")) // We have no recall data.
        {
          return "";
        }
                
        string s = new String(' ', 25) + lenderAccountName + ", DTC #: "	 + bookGroup + "\r\n" +
          new String(' ', 25) + lenderAddressLine1 + "\r\n" +
          new String(' ', 25) + lenderAddressLine2 + "\r\n" +
          new String(' ', 25) + lenderAddressLine3 + "\r\n" +
          new String(' ', 25) + lenderPhone + "\r\n\r\n" +

          borrowerAccountName.PadRight(40) + "Recall Date: " + Tools.FormatDate(recallDate, Standard.DateFormat) + "\r\n" +
          borrowerAddressLine1.PadRight(42) + "Reference: " + recallId.Substring(0, 5) + "-" + recallId.Substring(5, 6) + "-" + recallId.Substring(11, 5) + "\r\n";
          
        if (!borrowerAddressLine2.Equals(""))
        {
          s += borrowerAddressLine2.PadRight(40) + "\r\n";
        }  
        
        s += borrowerAddressLine3.PadRight(40) + "\r\n" +
          
          "DTCC ID: " + borrowerDtcId.PadRight(35) + "Printed: " + DateTime.UtcNow.ToString(Standard.DateTimeFileFormat) + " UTC\r\n\r\n\r\n" +
          
          new String('-', 80) + "\r\n" +
          "                             LOAN TERMINATION NOTICE\r\n" +
          new String('-', 80) + "\r\n\r\n" +

          "This letter is to serve as your notice to return the following securities\r\n" +
          "currently on loan to your firm: " + Tools.SplitItem(description, "|", 0) + "\r\n\r\n" +

          "Quantity To Be Returned: " + quantity.ToString("#,##0").PadRight(21) + "Default Date: " + Tools.FormatDate(defaultDate, Standard.DateFormat) + "\r\n" +
          "          Recall Reason: " + reasonCode.PadRight(22) + "Security ID: " + secId + " [" + symbol + "]\r\n\r\n\r\n" +

          "Current Contract Details\r\n" + 
          new String('-', 55) + "\r\n" +
          "                           Quantity: " + quantity.ToString("#,##0") + " out of " + quantitySettled.ToString("#,##0") + "\r\n" +
          "                  Collateral Amount: " + quantitySettled.ToString("#,##0") + " " + currencyIso + "\r\n" +
          "                    Collateral Type: " + collateralCode + "\r\n" +
          "                               Rate: " + rate.ToString("0.000") + "\r\n" +
          "                  Dividend Tax Rate: " + divRate.ToString("0.000") + "\r\n" +
          "           Original Settlement Date: " + Tools.FormatDate(settleDate, Standard.DateFormat) + "\r\n\r\n" +

          "This contract will become non-interest bearing on the default date referenced\r\n" +
          "above.  Failure to return the quantity requested by the default date will\r\n" +
          "constitute a default.  Without further notice we may execute a buy-in on or\r\n" +
          "after the default date, on all or part of the quantity requested for return.\r\n\r\n" +

          "Please confirm receipt of this notice by signing below.\r\n\r\n" +

          "Respectfully submitted,\r\n\r\n" +

          lenderAccountName + "\r\n\r\n" +
 
          "Received and acknowledged,\r\n\r\n" +


          new String('_', 35) + "\r\n" +
          borrowerAccountName + "\r\n\r\n" +

          new String('-', 80) + "\r\n\r\n" +

          "Please return securities to DTCC clearing number " + lenderDtcId + ".  Thank you.\r\n\r\n\r\n\r\n\r\n";

        return s;        
      }
    }
  
		public string BorrowerAccountName
		{
			get
			{
				return borrowerAccountName;
			}
		}

    public string BorrowerFaxNumber
    {
      get
      {
        return borrowerFaxNumber;
      }
    }
  
  }
}
