using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using StockLoan.Common;

namespace StockLoan.Golden
{
  public class MemoSegAgent : MarshalByRefObject, IMemoSeg
  {
    private string dbCnStr;

    public MemoSegAgent(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;
    }


    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}