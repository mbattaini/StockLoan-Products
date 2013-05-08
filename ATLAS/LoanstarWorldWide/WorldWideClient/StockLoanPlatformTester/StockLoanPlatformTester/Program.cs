using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using StockLoan.Business;
//using StockLoan.DataAccess;

namespace StockLoanPlatformTester
{
    public class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        //   static void Main()
        //   {
        static void Main(string[] args)
        {
            // Database connection string			
            //    String dbCnStr = @"Trusted_Connection=yes; Data Source=WSPEN-SL08W7\SQLEXPRESS; Initial Catalog=LoanStar;";

            DataSet ds = new DataSet();

            ds = Admin.CountriesGet("");

            SetTesterForm frm1 = new SetTesterForm();
                        
            frm1.ShowDialog();
        }

        public static DataSet TestDS(string dbCnStr, int utcOffset = 0)
            {

            DataSet dsTest = new DataSet();
            SqlParameter param1;
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
            DataSet tempDataSet = new DataSet();
            string spProc = "spHolidayGet";

			try
			{
				SqlCommand dbCmd = new SqlCommand(spProc, dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

                //paramT1 = dbCmd.Parameters.Add("@utcOffSet", SqlDbType.Int);
                
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "Books");

                MessageBox.Show("Number of Tables: " + dataSet.Tables.Count.ToString());

                for (int i = 0; i < (dataSet.Tables.Count); i++)
                {

                    MessageBox.Show("Table Name: " + i + " = " + dataSet.Tables[i].TableName.ToString());

                }
                    //MessageBox.Show("Second Table Name: " + dataSet.Tables[1].TableName.ToString());

                dataSet.Tables[1].TableName = "BookGroups";
                dataSet.Tables[2].TableName = "BookContacts";
                dataSet.Tables[3].TableName = "BookFunds";
                dataSet.Tables[4].TableName = "BookClearingInstructions";

                for (int i = 0; i < (dataSet.Tables.Count); i++)
                {

                    MessageBox.Show("Table Name: " + i + " = " + dataSet.Tables[i].TableName.ToString());

                }
                


                //foreach (DataRow dr in dataSet.Tables["Countries"].Rows)
                //{
                //    MessageBox.Show("Record Fields: " + dr["Country"]);

                //}  //Detail page line items foreach loop

                //foreach (DataRow dr in dataSet.Tables["Countries1"].Rows)
                //{
                //    MessageBox.Show("Record Fields: " + dr["Country"]);

                //}  //Detail page line items foreach loop

                // spProc = "spCountryCodeIsoConversionGet";

                //dbCmd = new SqlCommand(spProc, dbCn);
                //dbCmd.CommandType = CommandType.StoredProcedure;
 
                //dataAdapter = new SqlDataAdapter(dbCmd);
                //dataAdapter.Fill(tempDataSet, "IsoCountryConversions");


                //if (tempDataSet.Tables["IsoCountryConversions"].Rows.Count == 0)
                //{
                //        //masterBill += "";
                //}
                //else
                //    {                          
                        //int index = 0;
                        //foreach (DataRow dr in tempDataSet.Tables["IsoCountryConversions"].Rows)  
                        //{
                        //    MessageBox.Show("Record Fields: " + dr["CountryCodeIso"]);

                        //}  //Detail page line items foreach loop

            }

                    //}	//Detail page 
                //}
			catch (Exception e)
			{
				//Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.MasterBill]", Log.Error, 1);
			}

			return dsTest;        }

  
        
        public static DataSet DSTableRenameSample(string countryCode, string dbCnStr)
            {

                DataSet dsTest = new DataSet();
                SqlParameter paramCountryCode;
                SqlConnection dbCn = new SqlConnection(dbCnStr);
                DataSet dataSet = new DataSet();
                DataSet tempDataSet = new DataSet();
                string spProc = "spCountriesGet";

                try
                {
                    SqlCommand dbCmd = new SqlCommand(spProc, dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;

                    paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.Char);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                    dataAdapter.Fill(dataSet, "Countries");

                    MessageBox.Show("Number of Tables: " + dataSet.Tables.Count.ToString());

                    MessageBox.Show("First Table Name: " + dataSet.Tables[0].TableName.ToString());

                    MessageBox.Show("Second Table Name: " + dataSet.Tables[1].TableName.ToString());

                    dataSet.Tables[1].TableName = "IsoCountryConversions";


                    MessageBox.Show("Second Table Name - After Rename =  " + dataSet.Tables[1].TableName.ToString());



                    //foreach (DataRow dr in dataSet.Tables["Countries"].Rows)
                    //{
                    //    MessageBox.Show("Record Fields: " + dr["Country"]);

                    //}  //Detail page line items foreach loop

                    //foreach (DataRow dr in dataSet.Tables["Countries1"].Rows)
                    //{
                    //    MessageBox.Show("Record Fields: " + dr["Country"]);

                    //}  //Detail page line items foreach loop

                    // spProc = "spCountryCodeIsoConversionGet";

                    //dbCmd = new SqlCommand(spProc, dbCn);
                    //dbCmd.CommandType = CommandType.StoredProcedure;

                    //dataAdapter = new SqlDataAdapter(dbCmd);
                    //dataAdapter.Fill(tempDataSet, "IsoCountryConversions");


                    //if (tempDataSet.Tables["IsoCountryConversions"].Rows.Count == 0)
                    //{
                    //        //masterBill += "";
                    //}
                    //else
                    //    {                          
                    //int index = 0;
                    //foreach (DataRow dr in tempDataSet.Tables["IsoCountryConversions"].Rows)  
                    //{
                    //    MessageBox.Show("Record Fields: " + dr["CountryCodeIso"]);

                    //}  //Detail page line items foreach loop

                }

                        //}	//Detail page 
                //}
                catch (Exception e)
                {
                    //Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.MasterBill]", Log.Error, 1);
                }

                return dsTest;
            }

    }
}
