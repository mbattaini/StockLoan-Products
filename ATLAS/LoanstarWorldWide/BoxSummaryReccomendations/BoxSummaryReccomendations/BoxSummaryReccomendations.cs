using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BoxSummaryReccomendations
{
    class BoxSummaryReccomendations
    {

        public static DataSet Reccomendations(string bizDate)
        {
            DataSet dataSet = new DataSet();

            dataSet = BoxSummaryGet(bizDate);
            dataSet.Tables["BoxSummary"].Columns.Add("AvailableModified", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("PensonAvailable", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("PensonAvailableModified", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("RockAvailableModified", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("AvailableAmount", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("RockAvailableAmount", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("PensonAvailableAmount", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("PledgeUndo", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("Borrow", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("PensonBorrow", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("PensonBorrowModified", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("BorrowAmount", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("PensonBorrowAmount", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("RockBorrowAmount", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("RockBorrowModified", typeof(long));
            dataSet.Tables["BoxSummary"].Columns.Add("Recall", typeof(long));

            DataConfig(ref dataSet);

            return dataSet;
        }

        private static void DataConfig(ref DataSet dataSet)
        {
            string secId = "";

            long available;
            long recallBalance;
            long deliveryBalance;

            double hairCut = 0;


            hairCut = 0;

            foreach (DataRow row in dataSet.Tables["BoxSummary"].Rows)
            {
                secId = "[" + row["SecId"].ToString().Trim() + "] "; // Value captured here for error reporting only.

                recallBalance = (long)row["LoanRecalls"] - (long)row["BorrowRecalls"];

                deliveryBalance = 0;


                available = long.Parse(row["Available"].ToString());
                available = available + deliveryBalance;

                if (available <= 0) // Must move securities in.
                {
                    if ((available + (long)row["OnPledge"]) >= 0)
                    {
                        row["PledgeUndo"] = -available;
                        //available = 0;
                    }
                    else
                    {
                        row["PledgeUndo"] = row["OnPledge"];
                        //available += (long)row["PledgeUndo"];
                    }

                    if ((bool)row["DoNotRecall"])
                    {
                        row["Recall"] = 0;
                    }
                    else
                    {
                        if ((available + (long)row["Loans"] - (long)row["LoanRecalls"]) >= 0)
                        {
                            row["Recall"] = -(available);
                            //available = 0;
                        }
                        else if (available < 0)
                        {
                            row["Recall"] = (long)row["Loans"] - (long)row["LoanRecalls"];
                            //available += (long)row["Recall"]; 
                        }
                        else
                        {
                            row["Recall"] = 0;
                        }
                    }

                    row["Borrow"] = -(available);
                }
                else // Make returns and/or cancell recalls.
                {
                    if (available >= (long)row["LoanRecalls"])
                    {
                        row["Recall"] = -(long)row["LoanRecalls"];
                        //available -= (long)row["LoanRecalls"];
                    }
                    else if (available < (long)row["LoanRecalls"])
                    {
                        row["Recall"] = -available;

                        if (row["BaseType"].ToString().Equals("B"))
                        {
                            row["Recall"] = (long)row["Recall"] - ((long)row["Recall"] % 100000);
                            //available = available % 100000;
                        }
                        else
                        {
                            row["Recall"] = (long)row["Recall"] - ((long)row["Recall"] % 100);
                            //available = available % 100;
                        }
                    }

                    if (available > (long)row["Borrows"])
                    {
                        row["Borrow"] = -(long)row["Borrows"];
                        //available -= (long)row["Borrows"];
                    }
                    else if (available < (long)row["Borrows"])
                    {
                        row["Borrow"] = -available;
                        //available = 0;
                    }
                    else
                    {
                        row["Borrow"] = 0;
                    }
                }

                if ((long)row["Recall"] > 0)
                {
                    if ((long)row["LoanRecalls"] >= (long)row["Recall"])
                    {
                        row["Recall"] = 0;
                    }
                    else
                    {
                        row["Recall"] = (long)row["Recall"] - (long)row["LoanRecalls"];
                    }
                }
                else if ((long)row["Recall"] < 0)
                {
                    if (available > 0)
                    {
                        row["Recall"] = -(long)row["LoanRecalls"];
                    }
                    else
                    {
                        row["Recall"] = available + (long)row["Recall"];
                    }
                }

                if ((long)row["Borrow"] > 0)
                {
                    if (row["BaseType"].ToString().Equals("B") && (((long)row["Borrow"] % 100000) > 0))
                    {
                        row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100000) + 100000;
                    }
                    else if (((long)row["Borrow"] % 100) >= 0)
                    {
                        row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100) + 100;
                    }
                }
                else if ((long)row["Borrow"] < 0)
                {
                    if (row["BaseType"].ToString().Equals("B"))
                    {
                        row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100000);
                    }
                    else
                    {
                        row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100);
                    }
                }

                if ((long)row["RockBorrow"] > 0)
                {
                    if (row["BaseType"].ToString().Equals("B") && (((long)row["RockBorrow"] % 100000) > 0))
                    {
                        row["RockBorrowModified"] = (long)row["RockBorrow"] - ((long)row["RockBorrow"] % 100000) + 100000;
                    }
                    else if (((long)row["Borrow"] % 100) >= 0)
                    {
                        row["RockBorrowModified"] = (long)row["RockBorrow"] - ((long)row["RockBorrow"] % 100) + 100;
                    }
                }
                else if ((long)row["RockBorrow"] < 0)
                {
                    if (row["BaseType"].ToString().Equals("B"))
                    {
                        row["RockBorrowModified"] = (long)row["RockBorrow"] - ((long)row["RockBorrow"] % 100000);
                    }
                    else
                    {
                        row["RockBorrowModified"] = (long)row["RockBorrow"] - ((long)row["RockBorrow"] % 100);
                    }
                }
                else
                {
                    row["RockBorrowModified"] = 0;
                }

                row["RockAvailableModified"] = row["RockAvailable"];

                row["AvailableModified"] = long.Parse(row["Available"].ToString()) - (long.Parse(row["Available"].ToString()) % 100);

                row["PensonBorrow"] = (long)row["Borrow"] - (long)row["RockBorrowModified"];

                row["PensonAvailable"] = ((long.Parse(row["Available"].ToString()) - (long)row["RockAvailableModified"]) - ((long.Parse(row["Available"].ToString()) - (long)row["RockAvailableModified"]) * hairCut));

                if (!row["LastPrice"].Equals(DBNull.Value))
                {
                    if (row["BaseType"].ToString().Equals("B"))
                    {
                        row["PensonAvailableModified"] = (long)row["PensonAvailable"] - ((long)row["PensonAvailable"] % 100000);
                        row["RockAvailableModified"] = (long)row["RockAvailable"] - ((long)row["RockAvailable"] % 100000);

                        row["RockBorrowModified"] = (long)row["RockBorrow"] - ((long)row["RockBorrow"] % 100000);
                        row["PensonBorrowModified"] = (long)row["PensonBorrow"] - ((long)row["PensonBorrow"] % 100000);

                        row["AvailableAmount"] = (long)(long.Parse(row["Available"].ToString()) * (double)row["LastPrice"] / 100.0);
                        row["BorrowAmount"] = (long)((long)row["Borrow"] * (double)row["LastPrice"] / 100.0);

                        row["PensonAvailableAmount"] = (long)((long)row["PensonAvailableModified"] * (double)row["LastPrice"] / 100.0);
                        row["PensonBorrowAmount"] = (long)((long)row["PensonBorrowModified"] * (double)row["LastPrice"] / 100.0);

                        row["RockAvailableAmount"] = (long)((long)row["RockAvailableModified"] * (double)row["LastPrice"] / 100.0);
                        row["RockBorrowAmount"] = (long)((long)row["RockBorrowModified"] * (double)row["LastPrice"] / 100.0);

                    }
                    else
                    {
                        row["PensonAvailableModified"] = (long)row["PensonAvailable"] - ((long)row["PensonAvailable"] % 100);
                        row["RockAvailableModified"] = (long)row["RockAvailable"] - ((long)row["RockAvailable"] % 100);

                        row["RockBorrowModified"] = (long)row["RockBorrow"] - ((long)row["RockBorrow"] % 100);
                        row["PensonBorrowModified"] = (long)row["PensonBorrow"] - ((long)row["PensonBorrow"] % 100);

                        row["AvailableAmount"] = (long)(long.Parse(row["Available"].ToString()) * (double)row["LastPrice"]);
                        row["BorrowAmount"] = (long)((long)row["Borrow"] * (double)row["LastPrice"]);

                        row["PensonAvailableAmount"] = (long)((long)row["PensonAvailableModified"] * (double)row["LastPrice"]);
                        row["PensonBorrowAmount"] = (long)((long)row["PensonBorrowModified"] * (double)row["LastPrice"]);

                        row["RockAvailableAmount"] = (long)((long)row["RockAvailableModified"] * (double)row["LastPrice"]);
                        row["RockBorrowAmount"] = (long)((long)row["RockBorrowModified"] * (double)row["LastPrice"]);
                    }
                }
                else
                {
                    row["AvailableAmount"] = DBNull.Value;
                    row["BorrowAmount"] = DBNull.Value;

                    row["PensonAvailableAmount"] = DBNull.Value;
                    row["PensonBorrowAmount"] = DBNull.Value;

                    row["RockAvailableAmount"] = DBNull.Value;
                    row["RockBorrowAmount"] = DBNull.Value;
                }
            }
        }

        private static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder s = new SqlConnectionStringBuilder();
                s.InitialCatalog = "Sendero";
                s.DataSource = "Zeus";
                s.IntegratedSecurity = true;

                return s.ConnectionString;
            }
        }


        public static DataSet BoxSummaryGet(string bizDate)
        {
            DataSet dataSet = new DataSet();

            SqlConnection dbCn = new SqlConnection(ConnectionString);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxSummaryGet_mrb", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlDataAdapter sDataAdapter = new SqlDataAdapter(dbCmd);
                sDataAdapter.Fill(dataSet, "BoxSummary");
            }
            catch { }

            return dataSet;
        }
    }
}
