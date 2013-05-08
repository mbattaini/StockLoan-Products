using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using StockLoan.DataAccess;
using System.Collections.ObjectModel;       //for ReadOnlyCollection <T>
using System.Net;                           //for hostName and IP address 
using System.IO;                            //DChen for StreamReader class 


/*using System;
using System.IO;
using System.Web.Mail;
using System.Collections;
using System.Collections.ObjectModel;
using StockLoan.Common;
using StockLoan.DataAccess;
using StockLoan.Transport;         
*/

namespace StockLoan.InventoryService 
{

    public class InventoryItem
    {
        //Attributes / Data Memebrs
        private static string itemBizDate = "";             //DC  passed-in from BookGroup.BizDate for imported inventory items
        private static string itemBookGroup = "";           //DC    
        private static string itemDesk = "";                //DC
        private static string itemSecId = "";               //DC
        private static string itemQuantity = "";            //DC  long 0 
        private static string itemRate = "";                //DC  decimal  0.0M
        private static string itemSource = "";              //DC  "System"
        private static string itemSourceActor = "";         //DC  "System" or userId

        //Methods 
        private static void ItemSet()
        {   //DC Debug 
            try
            {
                DBInventory.InventoryItemSet(itemBizDate, itemBookGroup, itemDesk, itemSecId, itemQuantity.ToString(), itemRate.ToString(), itemSource, itemSourceActor);
            }
            catch (Exception err)
            {
                //Log.Write("ERROR: " + err.Message + "   [LoadData.ItemSet]", Log.Information, 2);
                //MessageBox.Show(err.Message, "Inventory Item ItemSet");
            }
        }

        private static void ItemInitialize(string bizDateBookGroup)
        {   //DC debug 
            try
            {
                itemBizDate = bizDateBookGroup;      // ItemInitialize to whatever passed-in as bizDate, this is the current Bookgroup's itemBizDate
                //itemBookGroup = "";
                //itemDesk = "";
                itemSecId = "";
                itemQuantity = "";          // 0
                itemRate = "";              // 0.0M;
                //itemSource = "";
                //itemSourceActor = "";
            }
            catch (Exception err)
            {
                //Log.Write("ERROR: " + err.Message + "  [LoadData.ItemInitialize]", Log.Error, 2);
                //MessageBox.Show(err.Message, "Inventory Item value ItemInitialize");
            }
        }

        public static int LoadData(string bizDateBookGroup, string bookGroup, string desk, string inventoryType, string filePath)
        {
            string bizDateFileName = "";                    //DC BizDate as part of the file name: {yyyyMMdd}
            string bizDateDataRow = "";                     //DC BizDate within the file data content: part of Header, Data or Trailer row
            int itemCountListed = 0;                        //DC Total items Listed in file, listed within Trailer row (or Header row)
            int itemCountDataRow = 0;                       //DC Total items actually imported for the file
            int rowCountInventoryFileLayout = 0;            //DC Dataset InventoryFileLayout row count 

            DataSet dsInventoryFileLayout = new DataSet();
            StreamReader streamReader = null;
            //filePath is in local drive by now 

            short recordLength;
            char headerFlag;
            char dataFlag;
            char trailerFlag;
            char delimiter;
            short accountLocale;
            short accountOrdinal;
            short accountPosition;
            short accountLength;
            short secIdOrdinal;
            short secIdPosition;
            short secIdLength;
            short quantityOrdinal;
            short quantityPosition;
            short quantityLength;
            short rateOrdinal;
            short ratePosition;
            short rateLength;
            short recordCountOrdinal;
            short recordCountPosition;
            short recordCountLength;
            short bizDateDD;
            short bizDateMM;
            short bizDateYY;

            try
            {   //DC at this point we have dataset: InventorySubscriptions:  has itemBookGroup, itemDesk, and InventoryType

                dsInventoryFileLayout = DBInventory.InventoryFileLayoutGet(bookGroup, desk, inventoryType);

                rowCountInventoryFileLayout = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows.Count;

                //DChen ********* Load Inventory File based on InventoryFileLayouts *************************** DEBUG 
                if (rowCountInventoryFileLayout.Equals(0))
                {   //No FileLayout found for this Inventory file, thus cannot load file. 
                    return itemCountDataRow;
                }

                //Check for valid dsInventoryFileLayouts to be returned **************************************** 
                bookGroup = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["BookGroup"].ToString();
                desk = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["Desk"].ToString();
                inventoryType = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["InventoryType"].ToString();
                recordLength = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RecordLength"].ToString());
                headerFlag = char.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["HeaderFlag"].ToString());
                dataFlag = char.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["DataFlag"].ToString());
                trailerFlag = char.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["TrailerFlag"].ToString());
                delimiter = char.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["Delimiter"].ToString());
                accountLocale = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["AccountLocale"].ToString());
                accountOrdinal = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["AccountOrdinal"].ToString());
                accountPosition = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["AccountPosition"].ToString());
                accountLength = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["AccountLength"].ToString());
                secIdOrdinal = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["SecIdOrdinal"].ToString());
                secIdPosition = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["SecIdPosition"].ToString());
                secIdLength = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["SecIdLength"].ToString());
                quantityOrdinal = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["QuantityOrdinal"].ToString());
                quantityPosition = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["QuantityPosition"].ToString());
                quantityLength = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["QuantityLength"].ToString());
                rateOrdinal = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RateOrdinal"].ToString());
                ratePosition = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RatePosition"].ToString());
                rateLength = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RateLength"].ToString());
                recordCountOrdinal = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RecordCountOrdinal"].ToString());
                recordCountPosition = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RecordCountPosition"].ToString());
                recordCountLength = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RecordCountLength"].ToString());
                bizDateDD = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["BizDateDD"].ToString());
                bizDateMM = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["BizDateMM"].ToString());
                bizDateYY = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["BizDateYY"].ToString());

                //DChen Debug -- to be replace with Transport class methods... 
                streamReader = new StreamReader(filePath);
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                //InventoryItem item = new InventoryItem();
                itemBizDate = bizDateBookGroup;    //Passied-in BizDate from specific itemBookGroup's current itemBizDate 
                itemBookGroup = bookGroup;
                itemDesk = desk;
                itemSource = "System";
                itemSourceActor = "InventoryService";

                //**************************************************************************************************

                if (recordLength.Equals(-1))    // Record is Variable-Length (NOT Fixed-Length) (terminated with control character[s])
                {
                    if (delimiter.Equals('0'))  // No Delimiter defiend, Each field is defined by position and length.
                    {
                        char[] c;
                        itemCountDataRow = 0;

                        while (streamReader.Peek() > -1)    // more character to read (return -1 if there are no characters to be read)
                        {
                            c = streamReader.ReadLine().Trim().ToCharArray();
                            ItemInitialize(bizDateBookGroup);   //DC reset only itemSecId, itemQuantity, itemRate

                            try
                            {
                                if (c[0].Equals(headerFlag))    // Header row 
                                {
                                    if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                                    {
                                        bizDateDataRow = "20" + new String(c, bizDateYY, 2).Replace(" ", "0")
                                                         + "-" + new String(c, bizDateMM, 2).Replace(" ", "0")
                                                         + "-" + new String(c, bizDateDD, 2).Replace(" ", "0");

                                        // Compare bizDateBookGroup to bizDateDataRow (Header Row Date value) 
                                        // If these 2 dates are different then do NOT import this whole file. it's considered error in data.
                                        if (!bizDateDataRow.Equals("") && (!bizDateDataRow.Equals(bizDateFileName) || !bizDateDataRow.Equals(bizDateBookGroup)))
                                        {
                                            //MessageBox.Show("Skip Inventory Data Import due to bizDate discrepancy:\n Inventory Data listed itemBizDate: " + bizDateDataRow + ", which differs from [" + bookGroup + "].itemBizDate: " + bizDateBookGroup, "Data Date Discrepancy");
                                        }
                                    }
                                }
                                else if (c[0].Equals(trailerFlag))  // Trailer row
                                {
                                    if (recordCountPosition > -1)
                                    {
                                        itemCountListed = int.Parse(new String(c, recordCountPosition, recordCountLength));

                                        if (!itemCountDataRow.Equals(itemCountListed))
                                        {
                                            //MessageBox.Show("Record count parity fails with " + itemCountDataRow.ToString() + " items loaded while Trailer record anticipates " + itemCountListed.ToString() + " items.  [InventoryItems.Load]", "Inventory Load Item Count Parity");
                                        }
                                    }
                                }
                                else if (c[0].Equals(dataFlag) || (dataFlag.Equals('=')))   //Data row 
                                {
                                    itemSecId = new String(c, secIdPosition, secIdLength).Replace("\"", "").Trim().ToUpper();
                                    if (inventoryType.Equals("I")) { itemQuantity = long.Parse(new String(c, quantityPosition, quantityLength).ToUpper().Replace("\"", "").Replace("K", "000").Replace("M", "000000").Replace("MM", "000000").ToUpper().Trim()).ToString(); }
                                    if (inventoryType.Equals("R")) { itemRate = Decimal.Parse(new String(c, ratePosition, rateLength).Replace("%", "").Replace("\"", "").Trim()).ToString(); }
                                    ItemSet();      //DC write to database via DBInventory.InventoryItemSet method

                                    itemCountDataRow++;     //DC 
                                }
                            }
                            catch { }
                        }
                    }
                    else
                    {   // Delimiter character Exists. Each field is variable length and separated by the delimiter.
                        string line;
                        string[] fields;
                        itemCountDataRow = 0;

                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.TrimEnd();             //DC
                            fields = line.Trim().Split(delimiter);
                            ItemInitialize(bizDateBookGroup);   //DC ItemInitialize only itemSecId, itemQuantity, itemRate

                            if (fields.Length > 1)
                            {
                                try
                                {
                                    if (fields[0].Equals(headerFlag.ToString()))    // Header row 
                                    {
                                        if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                                        {
                                            //DC bizDate = MakeDate(line.Substring(bizDateYY, 2), line.Substring(bizDateMM, 2), line.Substring(bizDateDD, 2));
                                            bizDateDataRow = "20" + line.Substring(bizDateYY, 2).Replace(" ", "0")
                                                            + "-" + line.Substring(bizDateMM, 2).Replace(" ", "0")
                                                            + "-" + line.Substring(bizDateDD, 2).Replace(" ", "0");

                                            // Compare bizDateBookGroup to bizDateDataRow (Header Row Date value) 
                                            // If these 2 dates are different then do NOT import this whole file. it's considered error in data.
                                            if (!bizDateDataRow.Equals("") && (!bizDateDataRow.Equals(bizDateFileName) || !bizDateDataRow.Equals(bizDateBookGroup)))
                                            {
                                                //MessageBox.Show("Skip Inventory Data Import due to bizDate discrepancy:\n Inventory Data listed itemBizDate: " + bizDateDataRow + ", which differs from [" + bookGroup + "].itemBizDate: " + bizDateBookGroup, "Data Date Discrepancy");
                                            }
                                        }
                                    }
                                    else if (fields[0].Equals(trailerFlag.ToString()))      // Trailer row 
                                    {
                                        if (recordCountOrdinal > -1)
                                        {
                                            itemCountListed = int.Parse(fields[recordCountOrdinal].Trim());

                                            if (!itemCountDataRow.Equals(itemCountListed))
                                            {
                                                //MessageBox.Show("Record count parity fails with " + itemCountDataRow.ToString() + " items loaded while Trailer record anticipates " + itemCountListed.ToString() + " items.  [InventoryItems.Load]", "Inventory Load Item Count Parity");
                                            }
                                        }
                                    }
                                    else if (fields[0].Equals(dataFlag.ToString()) || dataFlag.Equals('='))     // Data row 
                                    {
                                        itemSecId = fields[secIdOrdinal].Replace("\"", "").Trim().ToUpper();
                                        if (inventoryType.Equals("I")) { itemQuantity = long.Parse(fields[quantityOrdinal].ToUpper().Replace("\"", "").Replace("K", "000").Replace("M", "000000").Replace("MM", "000000").Trim()).ToString(); }
                                        if (inventoryType.Equals("R")) { itemRate = Decimal.Parse(fields[rateOrdinal].Replace("%", "").Replace("\"", "").Trim()).ToString(); }
                                        ItemSet();     //DC write to database via DBInventory.InventoryItemSet method 

                                        itemCountDataRow++;         //DC 
                                    }
                                }
                                catch { }
                            }
                        }
                    }

                }
                else
                {   // FIXED-LENGTH Record  (All records has the same Fixed-Length, and fields are Fixed-Length too) 
                    // MessageBox.Show("Each Data Record is FIXED-LENGTH =" + recordLength.ToString());
                    // No delimiter needed for fixed-length record. 

                    char[] c;
                    itemCountDataRow = 0;

                    while (streamReader.Peek() > -1)    // more character to read (return -1 if there are no characters to be read)
                    {
                        c = streamReader.ReadLine().Trim().ToCharArray();
                        ItemInitialize(bizDateBookGroup);   //DC reset only itemSecId, itemQuantity, itemRate

                        try
                        {
                            if (c[0].Equals(headerFlag))    // Header row 
                            {
                                if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                                {
                                    bizDateDataRow = "20" + new String(c, bizDateYY, 2).Replace(" ", "0")
                                                     + "-" + new String(c, bizDateMM, 2).Replace(" ", "0")
                                                     + "-" + new String(c, bizDateDD, 2).Replace(" ", "0");

                                    // Compare bizDateBookGroup to bizDateDataRow (Header Row Date value) 
                                    // If these 2 dates are different then do NOT import this whole file. it's considered error in data.
                                    if (!bizDateDataRow.Equals("") && (!bizDateDataRow.Equals(bizDateFileName) || !bizDateDataRow.Equals(bizDateBookGroup)))
                                    {
                                        //MessageBox.Show("Skip Inventory Data Import due to bizDate discrepancy:\n Inventory Data listed itemBizDate: " + bizDateDataRow + ", which differs from [" + bookGroup + "].itemBizDate: " + bizDateBookGroup, "Data Date Discrepancy");
                                    }
                                }
                            }
                            else if (c[0].Equals(trailerFlag))  // Trailer row
                            {
                                if (recordCountPosition > -1)
                                {
                                    itemCountListed = int.Parse(new String(c, recordCountPosition, recordCountLength));

                                    if (!itemCountDataRow.Equals(itemCountListed))
                                    {
                                        //MessageBox.Show("Record count parity fails with " + itemCountDataRow.ToString() + " items loaded while Trailer record anticipates " + itemCountListed.ToString() + " items.  [InventoryItems.Load]", "Inventory Load Item Count Parity");
                                    }
                                }
                            }
                            else if (c[0].Equals(dataFlag) || (dataFlag.Equals('=')))   //Data row 
                            {
                                itemSecId = new String(c, secIdPosition, secIdLength).Replace("\"", "").Trim().ToUpper();
                                if (inventoryType.Equals("I")) { itemQuantity = long.Parse(new String(c, quantityPosition, quantityLength).ToUpper().Replace("\"", "").Replace("K", "000").Replace("M", "000000").Replace("MM", "000000").Trim()).ToString(); }
                                if (inventoryType.Equals("R")) { itemRate = Decimal.Parse(new String(c, ratePosition, rateLength).Replace("%", "").Replace("\"", "").Trim()).ToString(); }
                                ItemSet();     //DC write to database via DBInventory.InventoryItemSet method

                                itemCountDataRow++;     //DC 
                            }
                        }
                        catch { }
                    }

                }   //else FIXED-LENGTH Inventory Data

                return itemCountDataRow;    //Upon successful import Inventory file, return the number of data rows actually imported to database.
            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, "Inventory Load Process");
                //Log.Write("ERROR: " + err.Message + "   [LoadData.LoadData]", Log.Information, 2);
                //throw;

                return -1;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }

        } // method: LoadData

    } //class: InventoryItem

} //namespace 
