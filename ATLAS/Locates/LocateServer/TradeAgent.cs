
using System;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace StockLoan.Locates
{
  public class TradeAgent : MarshalByRefObject, ITrade
  {
    private string dbCnStr;

    public TradeAgent(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;
    }

    public DataSet SecMasterItemGet(string secId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      Log.Write("Doing a security data lookup on " + secId, 3);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spSecMasterItemGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        paramSecId.Value = secId;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "SecMasterItem");

        if (dataSet.Tables["SecMasterItem"].Rows.Count.Equals(1))
        {
          paramSecId.Value = dataSet.Tables["SecMasterItem"].Select()[0]["SecId"];
        }

        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = Master.BizDate;

        dbCmd.CommandText = "spBoxPositionGet";
        dataAdapter.Fill(dataSet, "BoxPosition");

        dbCmd.Parameters.Remove(paramBizDate);

        dataSet.ExtendedProperties.Add("LoadDateTime",
          KeyValue.Get("BoxPositionLoadDateTime", "0001-01-01 00:00:00", dbCn));
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [TradeAgent.SecMasterItemGet]", Log.Error, 1);
      }

      return dataSet;
    }


    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
