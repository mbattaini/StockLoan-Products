using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using StockLoan.DataAccess;
using System.Collections.ObjectModel;       
using System.Net;                           
using System.IO;                            
using StockLoan.Common;                     
using StockLoan.Transport;                  

namespace StockLoan.InventoryService 
{
    public class InventoryItem
    {
        private static string itemBizDate = "";             
        private static string itemBookGroup = "";           
        private static string itemDesk = "";                
        private static string itemSecId = "";               
        private static string itemQuantity = "";            
        private static string itemRate = "";                
        private static string itemSource = "";              
        private static string itemSourceActor = "";         

        private static void ItemSet()
        {   
            try
            {
                DBInventory.InventoryItemSet(itemBizDate, itemBookGroup, itemDesk, itemSecId, itemQuantity.ToString(), itemRate.ToString(), itemSource, itemSourceActor);
            }
            catch (Exception err)
            {
                Log.Write("ERROR: " + err.Message + "   [InventoryLoad.ItemSet]", Log.Error, 1);
            }
        }

        private static void ItemInitialize(string bizDate)
        {   
            try
            {
                itemBizDate = bizDate;  
                itemSecId = "";
                itemQuantity = "";               
                itemRate = "";                   
            }
            catch (Exception err)
            {
                Log.Write("ERROR: " + err.Message + "  [InventoryLoad.ItemInitialize]", Log.Error, 1);
            }
        }

        public static int LoadData(string bizDateBookGroup, string bizDateDesk, string bookGroup, string desk, string inventoryType, string filePath, string userName, string password, string dbCnStr)
        {
            string bizDateFileName = bizDateBookGroup;      // BizDate as part of the file name: {yyyyMMdd}
            string bizDateDataRow = "";                     // BizDate within the file data content: part of Header, Data or Trailer row
            int itemCountListed = 0;                        // Total items Listed in file, listed within Trailer row (or Header row)
            int itemCountDataRow = 0;                       // Total items actually contained in the file
            int itemCountImported = 0;                      // Total items actually imported successfully 
            int rowCountInventoryFileLayout = 0;            // Dataset InventoryFileLayout row count 
            int errCount = 0;
            bool errFlag = false; 

            DataSet dsInventoryFileLayout = new DataSet();
            StreamReader streamReader = null;
            string fileContent;

            //File Layouts
            short recordLength;
            string headerFlag;  
            string dataFlag;    
            string trailerFlag; 
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
            {   
                dsInventoryFileLayout = DBInventory.InventoryFileLayoutGet(bookGroup, desk, inventoryType);
                rowCountInventoryFileLayout = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows.Count;

                if (rowCountInventoryFileLayout.Equals(0))
                {   
                    return itemCountDataRow;
                }

                recordLength = short.Parse(dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["RecordLength"].ToString());
                headerFlag = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["HeaderFlag"].ToString();
                dataFlag = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["DataFlag"].ToString();
                trailerFlag = dsInventoryFileLayout.Tables["InventoryFileLayouts"].Rows[0]["TrailerFlag"].ToString();
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

                FileTransfer fileTransfer = new FileTransfer("");
                FileResponse fResponse = new FileResponse();
                fResponse = fileTransfer.FileContentsGet(filePath, userName, password);
                fileContent = fResponse.fileContents;

                streamReader = new StreamReader(filePath);
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                itemBizDate = bizDateDesk;  
                itemBookGroup = bookGroup;
                itemDesk = desk;
                itemSource = "System";
                itemSourceActor = "InventoryService";


                if (recordLength.Equals(-1))    // Record is Variable-Length (NOT Fixed-Length) (terminated with control character[s])
                {
                    if (delimiter.Equals('0'))  // No Delimiter defiend, Each field is defined by position and length.
                    {
                        char[] c;
                        itemCountDataRow = 0;   // items contained in the file
                        itemCountImported = 0;  // items successfully imported

                        while (streamReader.Peek() > -1)    // more character to read (return -1 if there are no characters to be read)
                        {
                            c = streamReader.ReadLine().Trim().ToCharArray();
                            ItemInitialize(bizDateDesk);    // (bizDateBookGroup);   

                            try
                            {
                                if (c[0].ToString().Equals(headerFlag))    // Header row       
                                {
                                    if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                                    {
                                        bizDateDataRow = "20" + new String(c, bizDateYY, 2).Replace(" ", "0")
                                                         + "-" + new String(c, bizDateMM, 2).Replace(" ", "0")
                                                         + "-" + new String(c, bizDateDD, 2).Replace(" ", "0");

                                        // Compare bizDate found within file data, to bizDate for this specific Desk (handles BizDatePrior Flag)
                                        if (!bizDateDataRow.Equals("") && !bizDateDataRow.Equals(bizDateDesk))
                                        {
                                            Log.Write("ERROR: File Header Date Discrepancy: Skipped import of Inventory file: " + filePath + "  Due to BizDate discrepancy: Item Data row listed BizDate: " + bizDateDataRow + ", is different from [" + itemBookGroup + "].[" + itemDesk + "].[BizDate: " + bizDateDesk + "].   [InventoryLoad.LoadData]", Log.Error, 2);
                                            Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                                       KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr), 
                                                       "Inventory Service Notification - Error in File Date",
                                                       "ERROR: File Header Date Discrepancy:  Skipped import of Inventory file: " + filePath + "\r\nReason: BizDate Discrepancy: " + "\r\n [" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "] is processing BizDate: " + bizDateDesk + "\r\n File Header listed BizDate: " + bizDateDataRow + "\r\n[InventoryLoad.LoadData]", 
                                                       dbCnStr);
                                            errCount++;
                                            break; 
                                        }
                                    }
                                }
                                else if (c[0].ToString().Equals(trailerFlag))  // Trailer row      
                                {
                                    if (recordCountPosition > -1)
                                    {
                                        itemCountListed = int.Parse(new String(c, recordCountPosition, recordCountLength));

                                        if (!itemCountDataRow.Equals(itemCountListed))
                                        {
                                            Log.Write("ERROR: Record count parity Failed: [" + itemBookGroup + "].[" + itemDesk + "].[" + bizDateDesk + "] with " + itemCountDataRow.ToString() + " items loaded, while Trailer record anticipates " + itemCountListed.ToString() + " items.   [InventoryLoad.LoadData]", Log.Information, 2);
                                            Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                                       KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                                       "Inventory Service Notification - Error in Inventory Item Count",
                                                       "ERROR: Inventory item count parity Failed: [" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[" + bizDateDesk + "] contained " + itemCountDataRow.ToString() + " items,\r\n Successfully loaded " + itemCountImported.ToString() + "items,\r\nTrailer Record listed total Item count: " + itemCountListed.ToString() + " items.\r\n  [InventoryLoad.LoadData]",
                                                       dbCnStr);
                                            errCount++;
                                        }
                                    }
                                }
                                else if (c[0].ToString().Equals(dataFlag) || (dataFlag.Equals("=")))   //Data row     
                                {
                                    itemCountDataRow++;  

                                    itemSecId = new String(c, secIdPosition, secIdLength).Replace("\"", "").Trim().ToUpper();
                                    if (inventoryType.Equals("I")) 
                                    { 
                                        itemQuantity = new String(c, quantityPosition, quantityLength).ToUpper().Replace("\"", "").Replace("K", "000").Replace("M", "000000").Replace("MM", "000000").Trim();
                                        itemQuantity = (itemQuantity.IndexOf('.').Equals(-1)) ? itemQuantity : itemQuantity.Remove(itemQuantity.IndexOf('.'));
                                    }
                                    if (inventoryType.Equals("R")) { itemRate = Decimal.Parse(new String(c, ratePosition, rateLength).Replace("%", "").Replace("\"", "").Trim()).ToString(); }
                                    ItemSet();   

                                    itemCountImported++; 
                                }
                            }
                            catch (Exception err)
                            {
                                if (!errFlag.Equals(true))
                                {   Log.Write("ERROR: Inventory[" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[Row: " + itemCountDataRow.ToString() + "]: " + err.Message + "   [InventoryLoad.LoadData]", Log.Error, 2);
                                    Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                               KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                               "Inventory Service Notification - Error Loading Inventory Data",
                                               "Inventory[" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[" + bizDateDesk + "].[Row: " + itemCountDataRow.ToString() + "]: \r\nERROR: " + err.Message + "\r\n[InventoryLoad.LoadData]",
                                               dbCnStr);
                                    errFlag = true;
                                }
                                errCount++;
                            }
                        }   
                    }
                    else
                    {   // Delimiter character Exists. Each field is variable length and separated by the delimiter.
                        string line;
                        string[] fields;
                        itemCountDataRow = 0;   // items contained in the file
                        itemCountImported = 0;  // items successfully imported

                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.TrimEnd();             
                            fields = line.Trim().Split(delimiter);
                            ItemInitialize(bizDateDesk);    // (bizDateBookGroup); 

                            if (fields.Length > 1)
                            {
                                try
                                {
                                    if (fields[0].Equals(headerFlag.ToString()))    // Header row 
                                    {
                                        if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                                        {
                                            //bizDate = MakeDate(line.Substring(bizDateYY, 2), line.Substring(bizDateMM, 2), line.Substring(bizDateDD, 2));
                                            bizDateDataRow = "20" + line.Substring(bizDateYY, 2).Replace(" ", "0")
                                                            + "-" + line.Substring(bizDateMM, 2).Replace(" ", "0")
                                                            + "-" + line.Substring(bizDateDD, 2).Replace(" ", "0");

                                            // Compare bizDate found within file data, to bizDate for this specific Desk (handles BizDatePrior Flag)
                                            if (!bizDateDataRow.Equals("") && !bizDateDataRow.Equals(bizDateDesk))
                                            {
                                                Log.Write("ERROR: File Header Date Discrepancy: Skipped import of Inventory file: " + filePath + "  Due to BizDate discrepancy: Item Data row listed BizDate: " + bizDateDataRow + ", is different from [" + itemBookGroup + "].[" + itemDesk + "].[BizDate: " + bizDateDesk + "].   [InventoryLoad.LoadData]", Log.Error, 2);
                                                Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                                           KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                                           "Inventory Service Notification - Error in File Date",
                                                           "ERROR: File Header Date Discrepancy:  Skipped import of Inventory file: " + filePath + "\r\nReason: BizDate Discrepancy: " + "\r\n [" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "] is processing BizDate: " + bizDateDesk + "\r\n File Header listed BizDate: " + bizDateDataRow + "\r\n[InventoryLoad.LoadData]", 
                                                           dbCnStr);
                                                errCount++;
                                                break;
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
                                                Log.Write("ERROR: Record count parity Failed: [" + itemBookGroup + "].[" + itemDesk + "].[" + bizDateDesk + "] with " + itemCountDataRow.ToString() + " items loaded, while Trailer record anticipates " + itemCountListed.ToString() + " items.   [InventoryLoad.LoadData]", Log.Information, 2);
                                                Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                                           KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                                           "Inventory Service Notification - Error in Inventory Item Count",
                                                           "ERROR: Inventory item count parity Failed: [" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[" + bizDateDesk + "] contained " + itemCountDataRow.ToString() + " items,\r\n Successfully loaded " + itemCountImported.ToString() + "items,\r\nTrailer Record listed total Item count: " + itemCountListed.ToString() + " items.\r\n  [InventoryLoad.LoadData]",
                                                           dbCnStr);
                                                errCount++;
                                            }
                                        }
                                    }
                                    else if (fields[0].Equals(dataFlag.ToString()) || dataFlag.Equals("="))     // Data row     (fields[0].Equals(dataFlag.ToString()) || dataFlag.Equals('='))
                                    {
                                        itemCountDataRow++;

                                        itemSecId = fields[secIdOrdinal].Replace("\"", "").Trim().ToUpper();
                                        if (inventoryType.Equals("I")) 
                                        {
                                            itemQuantity = fields[quantityOrdinal].ToUpper().Replace("\"", "").Replace("K", "000").Replace("M", "000000").Replace("MM", "000000").Trim();
                                            itemQuantity = (itemQuantity.IndexOf('.').Equals(-1)) ? itemQuantity : itemQuantity.Remove(itemQuantity.IndexOf('.'));
                                        }
                                        if (inventoryType.Equals("R")) { itemRate = Decimal.Parse(fields[rateOrdinal].Replace("%", "").Replace("\"", "").Trim()).ToString(); }
                                        ItemSet();  

                                        itemCountImported++;  
                                    }
                                }
                                catch (Exception err)
                                {
                                    if (!errFlag.Equals(true))
                                    {
                                        Log.Write("ERROR: Inventory[" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[Row: " + itemCountDataRow.ToString() + "]: " + err.Message + "   [InventoryLoad.LoadData]", Log.Error, 2);
                                        Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                                   KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                                   "Inventory Service Notification - Error Loading Inventory Data",
                                                   "Inventory[" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[" + bizDateDesk + "].[Row: " + itemCountDataRow.ToString() + "]: \r\nERROR: " + err.Message + "\r\n[InventoryLoad.LoadData]",
                                                   dbCnStr);
                                        errFlag = true;
                                    }
                                    errCount++;
                                }
                            }
                        }
                    }

                }
                else
                {   // FIXED-LENGTH Record  (All records has the same Fixed-Length, and fields are Fixed-Length too) 

                    char[] c;
                    string tmpDataRow = ""; // Validate if Fixed-Length Data Row contained data to process, or just white-space to skip the row.
                    itemCountDataRow = 0;   // items contained in the file
                    itemCountImported = 0;  // items successfully imported

                    while (streamReader.Peek() > -1)    // more character to read (return -1 if there are no characters to be read)
                    {
                        c = streamReader.ReadLine().Trim().ToCharArray();
                        ItemInitialize(bizDateDesk);    // (bizDateBookGroup); 

                        try
                        {
                            if (c[0].ToString().Equals(headerFlag))    // Header row       
                            {
                                if ((bizDateYY > -1) && (bizDateMM > -1) && (bizDateDD > -1))
                                {
                                    bizDateDataRow = "20" + new String(c, bizDateYY, 2).Replace(" ", "0")
                                                     + "-" + new String(c, bizDateMM, 2).Replace(" ", "0")
                                                     + "-" + new String(c, bizDateDD, 2).Replace(" ", "0");

                                    // Compare bizDate found within file data, to bizDate for this specific Desk (handles BizDatePrior Flag)
                                    if (!bizDateDataRow.Equals("") && !bizDateDataRow.Equals(bizDateDesk))
                                    {
                                        Log.Write("ERROR: File Header Date Discrepancy: Skipped import of Inventory file: " + filePath + "  Due to BizDate discrepancy: Item Data row listed BizDate: " + bizDateDataRow + ", is different from [" + itemBookGroup + "].[" + itemDesk + "].[BizDate: " + bizDateDesk + "].   [InventoryLoad.LoadData]", Log.Error, 2);
                                        Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                                   KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                                   "Inventory Service Notification - Error in File Date",
                                                   "ERROR: File Header Date Discrepancy:  Skipped import of Inventory file: " + filePath + "\r\nReason: BizDate Discrepancy: " + "\r\n [" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "] is processing BizDate: " + bizDateDesk + "\r\n File Header listed BizDate: " + bizDateDataRow + "\r\n[InventoryLoad.LoadData]", 
                                                   dbCnStr);
                                        errCount++;
                                        break;
                                    }
                                }
                            }
                            else if (c[0].ToString().Equals(trailerFlag))  // Trailer row     
                            {
                                if (recordCountPosition > -1)
                                {
                                    itemCountListed = int.Parse(new String(c, recordCountPosition, recordCountLength));

                                    if (!itemCountDataRow.Equals(itemCountListed))
                                    {
                                        Log.Write("ERROR: Record count parity Failed: [" + itemBookGroup + "].[" + itemDesk + "].[" + bizDateDesk + "] with " + itemCountDataRow.ToString() + " items loaded, while Trailer record anticipates " + itemCountListed.ToString() + " items.   [InventoryLoad.LoadData]", Log.Information, 2);
                                        Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                                   KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                                   "Inventory Service Notification - Error in Inventory Item Count",
                                                   "ERROR: Inventory item count parity Failed: [" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[" + bizDateDesk + "] contained " + itemCountDataRow.ToString() + " items,\r\n Successfully loaded " + itemCountImported.ToString() + "items,\r\nTrailer Record listed total Item count: " + itemCountListed.ToString() + " items.\r\n  [InventoryLoad.LoadData]",
                                                   dbCnStr);
                                        errCount++;
                                    }
                                }
                            }
                            else if (c[0].ToString().Equals(dataFlag) || (dataFlag.Equals("=")))   //Data row       
                            {
                                // Check Data Row to skip blank lines (white-space, null character). Any textual data will be processed and error will be log/email if encountered.
                               tmpDataRow = new string(c);
                               if (tmpDataRow.Trim().Trim((char)0).Length > 0)       // AZTEC Rate file has a Null char (character value zero) at EOF, basic Trim() does Not remove this Null char. 
                                {
                                    itemCountDataRow++;
                                
                                    itemSecId = new String(c, secIdPosition, secIdLength).Replace("\"", "").Trim().ToUpper();
                                    if (inventoryType.Equals("I")) 
                                    { 
                                        itemQuantity = new String(c, quantityPosition, quantityLength).ToUpper().Replace("\"", "").Replace("K", "000").Replace("M", "000000").Replace("MM", "000000").Trim();
                                        itemQuantity = (itemQuantity.IndexOf('.').Equals(-1)) ? itemQuantity : itemQuantity.Remove(itemQuantity.IndexOf('.'));
                                    }
                                    if (inventoryType.Equals("R")) { itemRate = Decimal.Parse(new String(c, ratePosition, rateLength).Replace("%", "").Replace("\"", "").Trim()).ToString(); }
                                    ItemSet();

                                    itemCountImported++;
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            if (!errFlag.Equals(true))
                            {
                                Log.Write("ERROR: Inventory[" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[Row: " + itemCountDataRow.ToString() + "]: " + err.Message + "   [InventoryLoad.LoadData]", Log.Error, 2);
                                Email.Send(KeyValue.Get("InventoryServiceMailTo", "support.stockloan@penson.com;dchen@penson.com", dbCnStr),
                                           KeyValue.Get("InventoryServiceMailFrom", "support.stockloan@penson.com", dbCnStr),
                                           "Inventory Service Notification - Error Loading Inventory Data",
                                           "Inventory[" + itemBookGroup + "].[" + itemDesk + "].[" + inventoryType + "].[" + bizDateDesk + "].[Row: " + itemCountDataRow.ToString() + "]: \r\nERROR: " + err.Message + "\r\n[InventoryLoad.LoadData]",
                                           dbCnStr);
                                errFlag = true;
                            }
                            errCount++;
                        }
                    }

                }   

                return itemCountImported; 
            }
            catch (Exception err)
            {
                Log.Write("ERROR: [" + bookGroup + "].[" + desk + "].[" + inventoryType + "] File: " + filePath + "  ERROR: " + err.Message + "   [InventoryLoad.LoadData]", Log.Information, 2);
                return -1;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (errFlag.Equals(true) || (errCount > 0) )
                {
                    Log.Write("ERROR: [" + bookGroup + "].[" + desk + "].[" + inventoryType + "].[BizDate: " + bizDateDesk + "] File: " + filePath + "  encountered " + errCount + " error during Inventory Load process.   [InventoryLoad.LoadData]", Log.Information, 2);
                }
            }

        } 


    } 

}
