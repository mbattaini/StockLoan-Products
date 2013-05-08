using System;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace StockLoan.Main
{
  public class ReportAgent : MarshalByRefObject, IReport
  {
    private string dbCnStr;

    public ReportAgent(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;
    }

    public object BillingByBook(string startDate, string stopDate, string bookGroup)
    {
        DataSet dsContracts = new DataSet();
        DataSet dsContractsDetails = new DataSet();
        DataSet dsContractsSummary = new DataSet();

        C1.C1Report.C1Report report = new C1.C1Report.C1Report();
        C1.C1Report.C1Report subReport1 = new C1.C1Report.C1Report();
        C1.C1Report.C1Report subReport2 = new C1.C1Report.C1Report();

        dsContracts = ContractsGet(startDate, stopDate, bookGroup);
        dsContractsDetails = ContractsDetailsGet(startDate, stopDate, bookGroup);
        dsContractsSummary = ContractsSummaryGet(startDate, stopDate, bookGroup);
        
        report.Load(System.Environment.CurrentDirectory + @"\Layouts\BillingContractsTop.xml", "BillingTopLevel");
        subReport1.Load(System.Environment.CurrentDirectory + @"\Layouts\BillingContracts.xml", "BillingTopLevel");
        subReport2.Load(System.Environment.CurrentDirectory + @"\Layouts\BillingContractsDetails.xml", "BillingTopLevel");
        
        report.DataSource.Recordset = dsContractsSummary.Tables["ContractsSummary"];
        subReport1.DataSource.Recordset = dsContracts.Tables["Contracts"];
        subReport2.DataSource.Recordset = dsContractsDetails.Tables["ContractsDetails"];

        //subReport2.ParentReport = subReport1;
        //subReport1.ParentReport = report;
        
        return report;
    }

    public object BillingByContract(string startDate, string stopDate, string bookGroup)
    {
        C1.C1Report.C1Report report = new C1.C1Report.C1Report();

        return report;
    }

    public object BillingBySecurityId(string startDate, string stopDate, string bookGroup)
    {
        C1.C1Report.C1Report report = new C1.C1Report.C1Report();

        return report;
    }

    private DataSet ContractsGet(string startDate, string stopDate, string bookGroup)
    {
        SqlConnection dbCn = null;
        SqlCommand dbCmd = null;

        SqlDataAdapter dataAdapter;

        DataSet dsContracts = new DataSet();

        try
        {
            dbCn = new SqlConnection(dbCnStr);
            dbCmd = new SqlCommand("spBillingContractsGet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            dataAdapter = new SqlDataAdapter(dbCmd);

            SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            paramStartDate.Value = startDate;

            SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
            paramStopDate.Value = stopDate;

            SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
            paramBookGroup.Value = bookGroup;

            dataAdapter.Fill(dsContracts, "Contracts");         
        }
        catch (Exception e)
        {
            Log.Write(e.Message + " [ReportAgent.ContractsGet]", Log.Error, 1);
            throw;
        }

        return dsContracts;
    }

    private DataSet ContractsDetailsGet(string startDate, string stopDate, string bookGroup)
    {
        SqlConnection dbCn = null;
        SqlCommand dbCmd = null;

        SqlDataAdapter dataAdapter;

        DataSet dsContracts = new DataSet();

        try
        {
            dbCn = new SqlConnection(dbCnStr);
            dbCmd = new SqlCommand("spBillingContractsDetailsGet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            dataAdapter = new SqlDataAdapter(dbCmd);

            SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            paramStartDate.Value = startDate;

            SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
            paramStopDate.Value = stopDate;

            SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
            paramBookGroup.Value = bookGroup;

            dataAdapter.Fill(dsContracts, "ContractsDetails");
        }
        catch (Exception e)
        {
            Log.Write(e.Message + " [ReportAgent.ContractsDetailsGet]", Log.Error, 1);
            throw;
        }

        return dsContracts;
    }

    private DataSet ContractsSummaryGet(string startDate, string stopDate, string bookGroup)
    {
        SqlConnection dbCn = null;
        SqlCommand dbCmd = null;

        SqlDataAdapter dataAdapter;

        DataSet dsContracts = new DataSet();

        try
        {
            dbCn = new SqlConnection(dbCnStr);
            dbCmd = new SqlCommand("spBillingContractsSummaryGet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;

            dataAdapter = new SqlDataAdapter(dbCmd);

            SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            paramStartDate.Value = startDate;

            SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
            paramStopDate.Value = stopDate;

            SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
            paramBookGroup.Value = bookGroup;

            dataAdapter.Fill(dsContracts, "ContractsSummary");
        }
        catch (Exception e)
        {
            Log.Write(e.Message + " [ReportAgent.ContractsDetailsGet]", Log.Error, 1);
            throw;
        }

        return dsContracts;
    }
  
    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
