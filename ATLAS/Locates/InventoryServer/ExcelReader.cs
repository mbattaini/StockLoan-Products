using System;
using System.Data;
using System.Data.OleDb;

namespace StockLoan.Inventory
{



    /// <summary>
    /// Summary description for ExcelReader.
    /// </summary>
    public class ExcelReader : IDisposable
    {
        #region Variables
        private int[] arrayPKCols;
        private string strExcelFilename;
        private bool bMixedData = true;
        private bool bHeaders = false;
        private string strSheetName;
        private string strSheetRange;
        private bool bKeepConnectionOpen = false;
        private OleDbConnection oleDbConn;
        private OleDbCommand oleCmdSelect;
        private OleDbCommand oleCmdUpdate;
        #endregion

        #region properties

        public int[] PKCols
        {
            get { return arrayPKCols; }
            set { arrayPKCols = value; }
        }

        public string ColName(int intCol)
        {
            string sColName = "";
            if (intCol < 26)
                sColName = Convert.ToString(Convert.ToChar((Convert.ToByte((char)'A') + intCol)));
            else
            {
                int intFirst = ((int)intCol / 26);
                int intSecond = ((int)intCol % 26);
                sColName = Convert.ToString(Convert.ToByte((char)'A') + intFirst);
                sColName += Convert.ToString(Convert.ToByte((char)'A') + intSecond);
            }
            return sColName;
        }

        public int ColNumber(string strCol)
        {
            strCol = strCol.ToUpper();
            int intColNumber = 0;
            if (strCol.Length > 1)
            {
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[1]) - 65);
                intColNumber += Convert.ToInt16(Convert.ToByte(strCol[1]) - 64) * 26;
            }
            else
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[0]) - 65);
            return intColNumber;
        }



        public String[] GetExcelSheetNames()
        {

            System.Data.DataTable dt = null;

            try
            {
                if (oleDbConn == null) Open();

                // Get the data table containing the schema
                dt = oleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString();
                    excelSheets[i] = strSheetTableName.Substring(0, strSheetTableName.Length - 1);
                    i++;
                }


                return excelSheets;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (this.KeepConnectionOpen == false)
                {
                    this.Close();
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        public string ExcelFilename
        {
            get { return strExcelFilename; }
            set { strExcelFilename = value; }
        }

        public string SheetName
        {
            get { return strSheetName; }
            set { strSheetName = value; }
        }

        public string SheetRange
        {
            get { return strSheetRange; }
            set
            {
                if (value.IndexOf(":") == -1) throw new Exception("Invalid range length");
                strSheetRange = value;
            }
        }

        public bool KeepConnectionOpen
        {
            get { return bKeepConnectionOpen; }
            set { bKeepConnectionOpen = value; }
        }

        public bool Headers
        {
            get { return bHeaders; }
            set { bHeaders = value; }
        }

        public bool MixedData
        {
            get { return bMixedData; }
            set { bMixedData = value; }
        }
        #endregion

        #region Methods



        #region Excel Connection
        private string ExcelConnectionOptions()
        {
            string strOpts = "";
            
            if (true == this.MixedData) 
                    { strOpts += "Imex=1;"; }
            else    { strOpts += "Imex=2;"; }

            if (true == this.Headers)
                    { strOpts += "HDR=Yes;"; }
            else    { strOpts += "HDR=No;"; }
            
            return strOpts;
        }



        private string ExcelConnection()
        {
            return
                @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data Source=" + strExcelFilename + ";" +
                @"Extended Properties=" + Convert.ToChar(34).ToString() +
                @"Excel 8.0;" + ExcelConnectionOptions() + Convert.ToChar(34).ToString();
        }
        #endregion


        #region Open / Close
        public void Open()
        {
            try
            {
                if (oleDbConn != null)
                {
                    if (oleDbConn.State == ConnectionState.Open)
                    {
                        oleDbConn.Close();
                    }
                    oleDbConn = null;
                }

                if (System.IO.File.Exists(strExcelFilename) == false)
                {
                    throw new Exception("Excel file " + strExcelFilename + "could not be found.");
                }
                oleDbConn = new OleDbConnection(ExcelConnection());
                oleDbConn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            if (oleDbConn != null)
            {
                if (oleDbConn.State != ConnectionState.Closed)
                    oleDbConn.Close();
                oleDbConn.Dispose();
                oleDbConn = null;
            }
        }
        #endregion

        #region Command Select
        private bool SetSheetQuerySelect()
        {
            try
            {
                if (oleDbConn == null)
                {
                    throw new Exception("Connection is unassigned or closed.");
                }

                if (strSheetName.Length == 0)
                {
                    //throw new Exception("Sheetname was not assigned."); 
                    strSheetName = "Sheet1";
                }

                if (String.IsNullOrEmpty(strSheetRange))
                {
                    oleCmdSelect = new OleDbCommand(@"SELECT * FROM ["+ strSheetName + "$]", oleDbConn);
                }
                else
                {
                    oleCmdSelect = new OleDbCommand(@"SELECT * FROM ["+ strSheetName+ "$" + strSheetRange+ "]", oleDbConn);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region simple utilities
        private string AddWithComma(string strSource, string strAdd)
        {
            if (strSource != "") strSource = strSource += ", ";
            return strSource + strAdd;
        }

        private string AddWithAnd(string strSource, string strAdd)
        {
            if (strSource != "") strSource = strSource += " and ";
            return strSource + strAdd;
        }
        #endregion

        private OleDbDataAdapter SetSheetQueryAdapter(DataTable dt)
        {
            // Deleting in Excel workbook is not possible
            //So this command is not defined
            try
            {
                if (oleDbConn == null)
                {
                    throw new Exception("Connection is unassigned or closed.");
                }

                if (strSheetName.Length == 0)
                    throw new Exception("Sheetname was not assigned.");

                if (PKCols == null)
                    throw new Exception("Cannot update excel sheet with no primarykey set.");
                if (PKCols.Length < 1)
                    throw new Exception("Cannot update excel sheet with no primarykey set.");

                OleDbDataAdapter oleda = new OleDbDataAdapter(oleCmdSelect);
                string strUpdate = "";
                string strInsertPar = "";
                string strInsert = "";
                string strWhere = "";


                for (int iPK = 0; iPK < PKCols.Length; iPK++)
                {
                    strWhere = AddWithAnd(strWhere, dt.Columns[PKCols[iPK]].ColumnName + "=?");
                }
                strWhere = " Where " + strWhere;

                for (int iCol = 0; iCol < dt.Columns.Count; iCol++)
                {
                    strInsert = AddWithComma(strInsert, dt.Columns[iCol].ColumnName);
                    strInsertPar = AddWithComma(strInsertPar, "?");
                    strUpdate = AddWithComma(strUpdate, dt.Columns[iCol].ColumnName) + "=?";
                }

                string strTable = "[" + this.SheetName + "$" + this.SheetRange + "]";
                strInsert = "INSERT INTO " + strTable + "(" + strInsert + ") Values (" + strInsertPar + ")";
                strUpdate = "Update " + strTable + " Set " + strUpdate + strWhere;


                oleda.InsertCommand = new OleDbCommand(strInsert, oleDbConn);
                oleda.UpdateCommand = new OleDbCommand(strUpdate, oleDbConn);
                OleDbParameter oleParIns = null;
                OleDbParameter oleParUpd = null;
                for (int iCol = 0; iCol < dt.Columns.Count; iCol++)
                {
                    oleParIns = new OleDbParameter("?", dt.Columns[iCol].DataType.ToString());
                    oleParUpd = new OleDbParameter("?", dt.Columns[iCol].DataType.ToString());
                    oleParIns.SourceColumn = dt.Columns[iCol].ColumnName;
                    oleParUpd.SourceColumn = dt.Columns[iCol].ColumnName;
                    oleda.InsertCommand.Parameters.Add(oleParIns);
                    oleda.UpdateCommand.Parameters.Add(oleParUpd);
                    oleParIns = null;
                    oleParUpd = null;
                }

                for (int iPK = 0; iPK < PKCols.Length; iPK++)
                {
                    oleParUpd = new OleDbParameter("?", dt.Columns[PKCols[iPK]].DataType.ToString()); 
                    oleParUpd.SourceColumn = dt.Columns[PKCols[iPK]].ColumnName; 
                    oleParUpd.SourceVersion = DataRowVersion.Original; 
                    oleda.UpdateCommand.Parameters.Add(oleParUpd);
                }
                return oleda;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region command Singe Value Update
        private bool SetSheetQuerySingelValUpdate(string strVal)
        {
            try
            {
                if (oleDbConn == null)
                {
                    throw new Exception("Connection is unassigned or closed.");
                }

                if (strSheetName.Length == 0)
                    throw new Exception("Sheetname was not assigned.");

                oleCmdUpdate = new OleDbCommand(
                    @" Update ["
                    + strSheetName
                    + "$" + strSheetRange
                    + "] set F1=" + strVal, oleDbConn);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion



        public void SetPrimaryKey(int intCol)
        {
            arrayPKCols = new int[1] { intCol };
        }

        public DataTable GetTable()
        {
            return GetTable("ExcelTable");
        }

        private void SetPrimaryKey(DataTable dt)
        {
            try
            {
                if (PKCols != null)
                {
                    //set the primary key
                    if (PKCols.Length > 0)
                    {
                        DataColumn[] dc;
                        dc = new DataColumn[PKCols.Length];
                        for (int i = 0; i < PKCols.Length; i++)
                        {
                            dc[i] = dt.Columns[PKCols[i]];
                        }


                        dt.PrimaryKey = dc;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTable(string strTableName)
        {
            try
            {
                //Open and query
                if (oleDbConn == null) Open();
                if (oleDbConn.State != ConnectionState.Open)
                    throw new Exception("Connection cannot open error.");
                if (SetSheetQuerySelect() == false) return null;

                //Fill table
                OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                oleAdapter.SelectCommand = oleCmdSelect;
                DataTable dt = new DataTable(strTableName);
                oleAdapter.FillSchema(dt, SchemaType.Source);
                oleAdapter.Fill(dt);
                if (Headers == false)
                {
                    if ((!string.IsNullOrEmpty(strSheetRange)) && (strSheetRange.IndexOf(":") > 0))
                    {
                        string FirstCol = strSheetRange.Substring(0, strSheetRange.IndexOf(":") - 1);
                        int intCol = this.ColNumber(FirstCol);
                        for (int intI = 0; intI < dt.Columns.Count; intI++)
                        {
                            dt.Columns[intI].Caption = ColName(intCol + intI);
                        }
                    }
                }
                SetPrimaryKey(dt);
                //Cannot delete rows in Excel workbook
                dt.DefaultView.AllowDelete = false;

                //Clean up
                oleCmdSelect.Dispose();
                oleCmdSelect = null;
                oleAdapter.Dispose();
                oleAdapter = null;
                if (KeepConnectionOpen == false) Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CheckPKExists(DataTable dt)
        {
            if (dt.PrimaryKey.Length == 0)
                if (this.PKCols != null)
                {
                    SetPrimaryKey(dt);
                }
                else
                    throw new Exception("Provide an primary key to the datatable");
        }
        public DataTable SetTable(DataTable dt)
        {
            try
            {
                DataTable dtChanges = dt.GetChanges();
                if (dtChanges == null) throw new Exception("There are no changes to be saved!");
                CheckPKExists(dt);
                //Open and query
                if (oleDbConn == null) Open();
                if (oleDbConn.State != ConnectionState.Open)
                    throw new Exception("Connection cannot open error.");
                if (SetSheetQuerySelect() == false) return null;

                //Fill table
                OleDbDataAdapter oleAdapter = SetSheetQueryAdapter(dtChanges);

                oleAdapter.Update(dtChanges);
                //Clean up
                oleCmdSelect.Dispose();
                oleCmdSelect = null;
                oleAdapter.Dispose();
                oleAdapter = null;
                if (KeepConnectionOpen == false) Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Get/Set Single Value

        public void SetSingleCellRange(string strCell)
        {
            strSheetRange = strCell + ":" + strCell;
        }

        public object GetValue(string strCell)
        {
            SetSingleCellRange(strCell);
            object objValue = null;
            //Open and query
            if (oleDbConn == null) Open();
            if (oleDbConn.State != ConnectionState.Open)
                throw new Exception("Connection is not open error.");

            if (SetSheetQuerySelect() == false) return null;
            objValue = oleCmdSelect.ExecuteScalar();

            oleCmdSelect.Dispose();
            oleCmdSelect = null;
            if (KeepConnectionOpen == false) Close();
            return objValue;
        }

        public void SetValue(string strCell, object objValue)
        {

            try
            {

                SetSingleCellRange(strCell);
                //Open and query
                if (oleDbConn == null) Open();
                if (oleDbConn.State != ConnectionState.Open)
                    throw new Exception("Connection is not open error.");

                if (SetSheetQuerySingelValUpdate(objValue.ToString()) == false) return;
                objValue = oleCmdUpdate.ExecuteNonQuery();

                oleCmdUpdate.Dispose();
                oleCmdUpdate = null;
                if (KeepConnectionOpen == false) Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oleCmdUpdate != null)
                {
                    oleCmdUpdate.Dispose();
                    oleCmdUpdate = null;
                }
            }

        }
        #endregion


        #endregion

        public

        #region Dispose / Destructor
 void Dispose()
        {
            if (oleDbConn != null)
            {
                oleDbConn.Dispose();
                oleDbConn = null;
            }
            if (oleCmdSelect != null)
            {
                oleCmdSelect.Dispose();
                oleCmdSelect = null;
            }
            // Dispose of remaining objects.
        }
        #endregion

        #region CTOR
        public ExcelReader()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion
    }
}
