using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using StockLoan.Common;

namespace StockLoan.NorthStar
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
      DataSet dsSecMasterItem = new DataSet();

      Log.Write("Doing a security data lookup on " + secId, 3);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spSecMasterItemGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        paramSecId.Value = secId;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dsSecMasterItem, "SecMasterItem");

        if (dsSecMasterItem.Tables["SecMasterItem"].Rows.Count.Equals(1))
        {
          paramSecId.Value = dsSecMasterItem.Tables["SecMasterItem"].Select()[0]["SecId"];
        }

        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = Master.BizDate;

        dbCmd.CommandText = "spBoxPositionGet";
        dataAdapter.Fill(dsSecMasterItem, "BoxPosition");

        dbCmd.Parameters.Remove(paramBizDate);

        dsSecMasterItem.ExtendedProperties.Add("LoadDateTime",
          KeyValue.Get("BoxPositionLoadDateTime", "0001-01-01 00:00:00", dbCn));
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [TradeAgent.SecMasterItemGet]", Log.Error, 1);
      }

      return dsSecMasterItem;
    }


    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}