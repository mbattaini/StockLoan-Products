using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace S3_Pending_Trades
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string CreateSql(string dateTime)
        {

            string sql =
                     "select	top 3 trade_dt\r\n" +
                     "into	##trade_dates\r\n" +
                     "from	tprchs_sale_trans\r\n" +
                     "group by trade_dt \r\n" +
                     "order by trade_dt desc\r\n" +
                     
                     "select	(Select cross_Reference_cd from tsec_xref_key where security_adp_nbr = p.security_adp_nbr and type_xref_cd = 'CU' ) AS CUSIP,\r\n" +
                     "(case when p.chck_brch_acct_nbr = 1 then 'CASH'\r\n" +
                     "when p.chck_brch_acct_nbr = 2 then 'MARGIN'\r\n" +
                     "when p.chck_brch_acct_nbr = 5 then 'SHRTSALE'\r\n" +
                     "else 'FIRM' end) as [TYPE-OF-ACCOUNT],\r\n" +
                     "convert(bigint, p.share_trans_qty) as share_trans_qty,\r\n" +
                     "p.transaction_dt,\r\n" +
                     "p.branch_cd + p.account_cd as [ACCT-NO],\r\n" +
                     "p.chck_brch_acct_nbr,\r\n" +
                     "(case when p.debit_credit_cd = 'D' then 'S' else 'B'end ) as [BUY-SELL-IND],\r\n" +
                     "'' as [ACATS-IND]\r\n" +
                     "into	##valid_trades\r\n" +
                     "from	tprchs_sale_trans p \r\n" +
                     "where	p.trade_dt in (select trade_dt from ##trade_dates)\r\n" +
                     "and	p.share_trans_qty <> 0\r\n" +

                     "delete\r\n" +
                     "from	##valid_trades\r\n" +
                     "where	[BUY-SELL-IND] = 'B'\r\n" +
                     "and		chck_brch_acct_nbr = 1 \r\n" +
                     "and		transaction_dt > " + dateTime + "\r\n" +
                     
                     "delete\r\n" +
                     "from ##valid_trades\r\n"+
                     "where CUSIP is null\r\n" +
                     
                     "select	*\r\n" +
                     "from	##valid_trades\r\n";

            return sql;
        }


        private void CreateButton_Click(object sender, EventArgs e)
        {
            DataSet dsTrades = new DataSet();

            dsTrades = PendingTradesGet();
            FormatData(dsTrades);          
        }


        public DataSet PendingTradesGet()
        {
            DataSet dsTrades = new DataSet();
           
            try
            {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource =  Standard.ConfigValue("MainDatabaseHost");
            sqlBuilder.InitialCatalog = Standard.ConfigValue("MainDatabaseCatalog");
            sqlBuilder.IntegratedSecurity = true;

            SqlConnection dbCn = new SqlConnection(sqlBuilder.ConnectionString);
            
            SqlCommand dbCmd = new SqlCommand(CreateSql(FileDateEdit.Value.ToString("yyyy-MM-dd")), dbCn);
            dbCmd.CommandType = CommandType.Text;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
            dataAdapter.Fill(dsTrades, "Trades");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                throw;
            }

            return dsTrades;
        }

        public void FormatData(DataSet dsTrades)
        {
            StreamWriter streamWriter = new StreamWriter(@"C:\Temp\SSSPS.asc");

            string body = "";
            string header = "1SEL PS    " + (FileDateEdit.Value.ToString("yyyyMMdd")) + (new string(' ', 71));

            streamWriter.Write(header);



            foreach (DataRow dr in dsTrades.Tables["Trades"].Rows)
            {
                body = "5" +
                        dr["CUSIP"].ToString().Trim().PadRight(15, ' ') +
                        dr["TYPE-OF-ACCOUNT"].ToString().Trim().PadRight(10, ' ') +
                        long.Parse(dr["share_trans_qty"].ToString().Trim()).ToString().PadLeft(15, '0') +
                        DateTime.Parse(dr["transaction_dt"].ToString()).ToString("yyyyMMdd") +
                        dr["ACCT-NO"].ToString().Trim().PadRight(25, ' ') +
                        dr["chck_brch_acct_nbr"].ToString().Trim().PadRight(3, ' ') +
                        dr["BUY-SELL-IND"].ToString().Trim().PadRight(1, ' ') +
                        dr["ACATS-IND"].ToString().Trim().PadRight(1, ' ') +
                        new string(' ', 11);

                streamWriter.Write(body);
            }

            string trailer = "9" + (dsTrades.Tables["Trades"].Rows.Count + 2).ToString().PadLeft(15, '0') + (new string(' ', 74));
            streamWriter.Write(trailer);

            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
