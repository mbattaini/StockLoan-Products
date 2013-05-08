using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.ObjectModel;       
using System.Net;                           
using System.IO;                            
using StockLoan.Common;                     
using StockLoan.Transport;

namespace StockLoan.FileParsing
{
    public class RDSweepItem
    {        
        private static string clientNumber = "";
        private static string fileDate = "";
        private static string spinCode = "";
        private static string securityId = "";
        private static string cusipCountryCode = "";
        private static string cusip = "";
        private static string cusipCheckDigitCode = "";
        private static string cusipInternationalCheckDigit = "";
        private static string settlementDate = "";
        private static string customerAccountBranchPrefixCode = "";
        private static string customerAccountBranchCodeId = "";
        private static string customerAccountID = "";
        private static string customerAccountTypeCode = "";
        private static string customerAccountCheckDigitCode = "";
        private static string customerAccountFillCon = "";
        private static int tagDupNum = 0;
        private static string tagDate = "";
        private static string tagSourceID = "";
        private static int tagSerialNumber = 0;
        private static long remainingQuantity = 0;
        private static float remainingAmount = 0;
        private static string brokerNumber = "";
        private static string clhNum = "";
        private static string tradeDate = "";
        private static string clientStatusFlag = "";
        private static string originCode = "";
        private static string currencyCode = "";
        private static string blotterCode = "";
        private static string regRepNumber = "";
        private static string controlNumberID = "";
        private static string sendDTCFlag = "";
        private static string confirmationInd = "";
        private static string confirmationDate = "";
        private static string affirmationInd = "";
        private static string affirmDate = "";
        private static string securityProductCode = "";
        private static string housePriceInd = "";
        private static string reorgMessageInd = "";
        private static string divdMessageInd = "";
        private static string dueBillInd = "";
        private static string dtcEligableInd = "";
        private static string sameDayFundsInd = "";
        private static string securityDescription1 = "";
        private static string securityDescription2 = "";
        private static float marketPrice = 0;
        private static float mtmAmount = 0;
        private static string markUpDownInd = "";
        private static string flashMessage = "";
        private static int lastReclaim = 0;
        private static int daysToDeliver = 0;
        private static int daysOnFile = 0;
        private static string settleOfficeId = "";
        private static string balordControlNumber = "";
        private static string autoDoInd = "";
        private static string partialExecBreakNumber = "";
        private static string contraExecBreakNumber = "";
        private static string offsetAccountBranchPrefixCode = "";
        private static string offsetAccountBranchCode = "";
        private static string offsetAccountBodyId = "";
        private static string offsetAccountTypeCode = "";
        private static string offsetAccountCheckDigitCode = "";
        private static string tradingAccountBranchPrefixCode = "";
        private static string tradingAccountBranchCode = "";
        private static string tradingAccountBodyId = "";
        private static string tradingAccountTypeCode = "";
        private static string tradingAccountCheckDigitCode = "";
        private static string clientControlID = "";
        private static string symbolId = "";
        private static string cageTransControlNumber = "";
        private static string originalPrice = "";
        private static string actUserId = "";
        private static int fileDateRow = int.Parse(Standard.ConfigValue("FileDateRow"));

        private struct FileColLayout
        {
            public static int fileMMCol = int.Parse(Standard.ConfigValue("FileMMCol"));
            public static int fileDDCol = int.Parse(Standard.ConfigValue("FileDDCol"));
            public static int fileYYYYCol = int.Parse(Standard.ConfigValue("FileYYYYCol"));

            public static int ClientNumberCol = int.Parse(Standard.ConfigValue("ClientNumberCol"));
            public static int SpinCodeCol = int.Parse(Standard.ConfigValue("SpinCodeCol"));
            public static int SecurityIdCol = int.Parse(Standard.ConfigValue("SecurityIdCol"));
            public static int cusipCountryCodeCol = int.Parse(Standard.ConfigValue("cusipCountryCodeCol"));
            public static int cusipCol = int.Parse(Standard.ConfigValue("cusipCol"));
            public static int cusipCheckDigitCodeCol = int.Parse(Standard.ConfigValue("cusipCheckDigitCodeCol"));
            public static int cusipInternationalCheckDigitCol = int.Parse(Standard.ConfigValue("cusipInternationalCheckDigitCol"));
            public static int settlementDateCol = int.Parse(Standard.ConfigValue("settlementDateCol"));
            public static int customerAccountBranchPrefixCodeCol = int.Parse(Standard.ConfigValue("customerAccountBranchPrefixCodeCol"));
            public static int customerAccountBranchCodeIdCol = int.Parse(Standard.ConfigValue("customerAccountBranchCodeIdCol"));
            public static int customerAccountIDCol = int.Parse(Standard.ConfigValue("customerAccountIDCol"));
            public static int customerAccountTypeCodeCol = int.Parse(Standard.ConfigValue("customerAccountTypeCodeCol"));
            public static int customerAccountCheckDigitCodeCol = int.Parse(Standard.ConfigValue("customerAccountCheckDigitCodeCol"));
            public static int customerAccountFillConCol = int.Parse(Standard.ConfigValue("customerAccountFillConCol"));
            public static int tagDupNumCol = int.Parse(Standard.ConfigValue("tagDupNumCol"));
            public static int tagDateCol = int.Parse(Standard.ConfigValue("tagDateCol"));
            public static int tagSourceIDCol = int.Parse(Standard.ConfigValue("tagSourceIDCol"));
            public static int tagSerialNumberCol = int.Parse(Standard.ConfigValue("tagSerialNumberCol"));
            public static int remainingQuantityCol = int.Parse(Standard.ConfigValue("remainingQuantityCol"));
            public static int remainingAmountCol = int.Parse(Standard.ConfigValue("remainingAmountCol"));
            public static int brokerNumberCol = int.Parse(Standard.ConfigValue("brokerNumberCol"));
            public static int clhNumCol = int.Parse(Standard.ConfigValue("clhNumCol"));
            public static int tradeDateCol = int.Parse(Standard.ConfigValue("tradeDateCol"));
            public static int clientStatusFlagCol = int.Parse(Standard.ConfigValue("clientStatusFlagCol"));
            public static int originCodeCol = int.Parse(Standard.ConfigValue("originCodeCol"));
            public static int currencyCodeCol = int.Parse(Standard.ConfigValue("currencyCodeCol"));
            public static int blotterCodeCol = int.Parse(Standard.ConfigValue("blotterCodeCol"));
            public static int regRepNumberCol = int.Parse(Standard.ConfigValue("regRepNumberCol"));
            public static int controlNumberIDCol = int.Parse(Standard.ConfigValue("controlNumberIDCol"));
            public static int sendDTCFlagCol = int.Parse(Standard.ConfigValue("sendDTCFlagCol"));
            public static int confirmationIndCol = int.Parse(Standard.ConfigValue("confirmationIndCol"));
            public static int confirmationDateCol = int.Parse(Standard.ConfigValue("confirmationDateCol"));
            public static int affirmationIndCol = int.Parse(Standard.ConfigValue("affirmationIndCol"));
            public static int affirmDateCol = int.Parse(Standard.ConfigValue("affirmDateCol"));
            public static int securityProductCodeCol = int.Parse(Standard.ConfigValue("securityProductCodeCol"));
            public static int housePriceIndCol = int.Parse(Standard.ConfigValue("housePriceIndCol"));
            public static int reorgMessageIndCol = int.Parse(Standard.ConfigValue("reorgMessageIndCol"));
            public static int divdMessageIndCol = int.Parse(Standard.ConfigValue("divdMessageIndCol"));
            public static int dueBillIndCol = int.Parse(Standard.ConfigValue("dueBillIndCol"));
            public static int dtcEligableIndCol = int.Parse(Standard.ConfigValue("dtcEligableIndCol"));
            public static int sameDayFundsIndCol = int.Parse(Standard.ConfigValue("sameDayFundsIndCol"));
            public static int securityDescription1Col = int.Parse(Standard.ConfigValue("securityDescription1Col"));
            public static int securityDescription2Col = int.Parse(Standard.ConfigValue("securityDescription2Col"));
            public static int marketPriceCol = int.Parse(Standard.ConfigValue("marketPriceCol"));
            public static int mtmAmountCol = int.Parse(Standard.ConfigValue("mtmAmountCol"));
            public static int markUpDownIndCol = int.Parse(Standard.ConfigValue("markUpDownIndCol"));
            public static int flashMessageCol = int.Parse(Standard.ConfigValue("flashMessageCol"));
            public static int lastReclaimCol = int.Parse(Standard.ConfigValue("lastReclaimCol"));
            public static int daysToDeliverCol = int.Parse(Standard.ConfigValue("daysToDeliverCol"));
            public static int daysOnFileCol = int.Parse(Standard.ConfigValue("daysOnFileCol"));
            public static int settleOfficeIdCol = int.Parse(Standard.ConfigValue("settleOfficeIdCol"));
            public static int balordControlNumberCol = int.Parse(Standard.ConfigValue("balordControlNumberCol"));
            public static int autoDoIndCol = int.Parse(Standard.ConfigValue("autoDoIndCol"));
            public static int partialExecBreakNumberCol = int.Parse(Standard.ConfigValue("partialExecBreakNumberCol"));
            public static int contraExecBreakNumberCol = int.Parse(Standard.ConfigValue("contraExecBreakNumberCol"));
            public static int offsetAccountBranchPrefixCodeCol = int.Parse(Standard.ConfigValue("offsetAccountBranchPrefixCodeCol"));
            public static int offsetAccountBranchCodeCol = int.Parse(Standard.ConfigValue("offsetAccountBranchCodeCol"));
            public static int offsetAccountBodyIdCol = int.Parse(Standard.ConfigValue("offsetAccountBodyIdCol"));
            public static int offsetAccountTypeCodeCol = int.Parse(Standard.ConfigValue("offsetAccountTypeCodeCol"));
            public static int offsetAccountCheckDigitCodeCol = int.Parse(Standard.ConfigValue("offsetAccountCheckDigitCodeCol"));
            public static int tradingAccountBranchPrefixCodeCol = int.Parse(Standard.ConfigValue("tradingAccountBranchPrefixCodeCol"));
            public static int tradingAccountBranchCodeCol = int.Parse(Standard.ConfigValue("tradingAccountBranchCodeCol"));
            public static int tradingAccountBodyIdCol = int.Parse(Standard.ConfigValue("tradingAccountBodyIdCol"));
            public static int tradingAccountTypeCodeCol = int.Parse(Standard.ConfigValue("tradingAccountTypeCodeCol"));
            public static int tradingAccountCheckDigitCodeCol = int.Parse(Standard.ConfigValue("tradingAccountCheckDigitCodeCol"));
            public static int clientControlIDCol = int.Parse(Standard.ConfigValue("clientControlIDCol"));
            public static int symbolIdCol = int.Parse(Standard.ConfigValue("symbolIdCol"));
            public static int cageTransControlNumberCol = int.Parse(Standard.ConfigValue("cageTransControlNumberCol"));
            public static int originalPriceCol = int.Parse(Standard.ConfigValue("originalPriceCol"));
        }

        private struct FileLenLayout
        {
            public static int fileMMLen = int.Parse(Standard.ConfigValue("FileMMLen"));
            public static int fileDDLen = int.Parse(Standard.ConfigValue("FileDDLen"));
            public static int fileYYYYLen = int.Parse(Standard.ConfigValue("FileYYYYLen"));

            public static int clientNumberLen = int.Parse(Standard.ConfigValue("ClientNumberLen"));
            public static int spinCodeLen = int.Parse(Standard.ConfigValue("SpinCodeLen"));
            public static int securityIdLen = int.Parse(Standard.ConfigValue("SecurityIdLen"));
            public static int cusipCountryCodeLen = int.Parse(Standard.ConfigValue("cusipCountryCodeLen"));
            public static int cusipLen = int.Parse(Standard.ConfigValue("cusipLen"));
            public static int cusipCheckDigitCodeLen = int.Parse(Standard.ConfigValue("cusipCheckDigitCodeLen"));
            public static int cusipInternationalCheckDigitLen = int.Parse(Standard.ConfigValue("cusipInternationalCheckDigitLen"));
            public static int settlementDateLen = int.Parse(Standard.ConfigValue("settlementDateLen"));
            public static int customerAccountBranchPrefixCodeLen = int.Parse(Standard.ConfigValue("customerAccountBranchPrefixCodeLen"));
            public static int customerAccountBranchCodeIdLen = int.Parse(Standard.ConfigValue("customerAccountBranchCodeIdLen"));
            public static int customerAccountIDLen = int.Parse(Standard.ConfigValue("customerAccountIDLen"));
            public static int customerAccountTypeCodeLen = int.Parse(Standard.ConfigValue("customerAccountTypeCodeLen"));
            public static int customerAccountCheckDigitCodeLen = int.Parse(Standard.ConfigValue("customerAccountCheckDigitCodeLen"));
            public static int customerAccountFillConLen = int.Parse(Standard.ConfigValue("customerAccountFillConLen"));
            public static int tagDupNumLen = int.Parse(Standard.ConfigValue("tagDupNumLen"));
            public static int tagDateLen = int.Parse(Standard.ConfigValue("tagDateLen"));
            public static int tagSourceIDLen = int.Parse(Standard.ConfigValue("tagSourceIDLen"));
            public static int tagSerialNumberLen = int.Parse(Standard.ConfigValue("tagSerialNumberLen"));
            public static int remainingQuantityLen = int.Parse(Standard.ConfigValue("remainingQuantityLen"));
            public static int remainingAmountLen = int.Parse(Standard.ConfigValue("remainingAmountLen"));
            public static int brokerNumberLen = int.Parse(Standard.ConfigValue("brokerNumberLen"));
            public static int clhNumLen = int.Parse(Standard.ConfigValue("clhNumLen"));
            public static int tradeDateLen = int.Parse(Standard.ConfigValue("tradeDateLen"));
            public static int clientStatusFlagLen = int.Parse(Standard.ConfigValue("clientStatusFlagLen"));
            public static int originCodeLen = int.Parse(Standard.ConfigValue("originCodeLen"));
            public static int currencyCodeLen = int.Parse(Standard.ConfigValue("currencyCodeLen"));
            public static int blotterCodeLen = int.Parse(Standard.ConfigValue("blotterCodeLen"));
            public static int regRepNumberLen = int.Parse(Standard.ConfigValue("regRepNumberLen"));
            public static int controlNumberIDLen = int.Parse(Standard.ConfigValue("controlNumberIDLen"));
            public static int sendDTCFlagLen = int.Parse(Standard.ConfigValue("sendDTCFlagLen"));
            public static int confirmationIndLen = int.Parse(Standard.ConfigValue("confirmationIndLen"));
            public static int confirmationDateLen = int.Parse(Standard.ConfigValue("confirmationDateLen"));
            public static int affirmationIndLen = int.Parse(Standard.ConfigValue("affirmationIndLen"));
            public static int affirmDateLen = int.Parse(Standard.ConfigValue("affirmDateLen"));
            public static int securityProductCodeLen = int.Parse(Standard.ConfigValue("securityProductCodeLen"));
            public static int housePriceIndLen = int.Parse(Standard.ConfigValue("housePriceIndLen"));
            public static int reorgMessageIndLen = int.Parse(Standard.ConfigValue("reorgMessageIndLen"));
            public static int divdMessageIndLen = int.Parse(Standard.ConfigValue("divdMessageIndLen"));
            public static int dueBillIndLen = int.Parse(Standard.ConfigValue("dueBillIndLen"));
            public static int dtcEligableIndLen = int.Parse(Standard.ConfigValue("dtcEligableIndLen"));
            public static int sameDayFundsIndLen = int.Parse(Standard.ConfigValue("sameDayFundsIndLen"));
            public static int securityDescription1Len = int.Parse(Standard.ConfigValue("securityDescription1Len"));
            public static int securityDescription2Len = int.Parse(Standard.ConfigValue("securityDescription2Len"));
            public static int marketPriceLen = int.Parse(Standard.ConfigValue("marketPriceLen"));
            public static int mtmAmountLen = int.Parse(Standard.ConfigValue("mtmAmountLen"));
            public static int markUpDownIndLen = int.Parse(Standard.ConfigValue("markUpDownIndLen"));
            public static int flashMessageLen = int.Parse(Standard.ConfigValue("flashMessageLen"));
            public static int lastReclaimLen = int.Parse(Standard.ConfigValue("lastReclaimLen"));
            public static int daysToDeliverLen = int.Parse(Standard.ConfigValue("daysToDeliverLen"));
            public static int daysOnFileLen = int.Parse(Standard.ConfigValue("daysOnFileLen"));
            public static int settleOfficeIdLen = int.Parse(Standard.ConfigValue("settleOfficeIdLen"));
            public static int balordControlNumberLen = int.Parse(Standard.ConfigValue("balordControlNumberLen"));
            public static int autoDoIndLen = int.Parse(Standard.ConfigValue("autoDoIndLen"));
            public static int partialExecBreakNumberLen = int.Parse(Standard.ConfigValue("partialExecBreakNumberLen"));
            public static int contraExecBreakNumberLen = int.Parse(Standard.ConfigValue("contraExecBreakNumberLen"));
            public static int offsetAccountBranchPrefixCodeLen = int.Parse(Standard.ConfigValue("offsetAccountBranchPrefixCodeLen"));
            public static int offsetAccountBranchCodeLen = int.Parse(Standard.ConfigValue("offsetAccountBranchCodeLen"));
            public static int offsetAccountBodyIdLen = int.Parse(Standard.ConfigValue("offsetAccountBodyIdLen"));
            public static int offsetAccountTypeCodeLen = int.Parse(Standard.ConfigValue("offsetAccountTypeCodeLen"));
            public static int offsetAccountCheckDigitCodeLen = int.Parse(Standard.ConfigValue("offsetAccountCheckDigitCodeLen"));
            public static int tradingAccountBranchPrefixCodeLen = int.Parse(Standard.ConfigValue("tradingAccountBranchPrefixCodeLen"));
            public static int tradingAccountBranchCodeLen = int.Parse(Standard.ConfigValue("tradingAccountBranchCodeLen"));
            public static int tradingAccountBodyIdLen = int.Parse(Standard.ConfigValue("tradingAccountBodyIdLen"));
            public static int tradingAccountTypeCodeLen = int.Parse(Standard.ConfigValue("tradingAccountTypeCodeLen"));
            public static int tradingAccountCheckDigitCodeLen = int.Parse(Standard.ConfigValue("tradingAccountCheckDigitCodeLen"));
            public static int clientControlIDLen = int.Parse(Standard.ConfigValue("clientControlIDLen"));
            public static int symbolIdLen = int.Parse(Standard.ConfigValue("symbolIdLen"));
            public static int cageTransControlNumberLen = int.Parse(Standard.ConfigValue("cageTransControlNumberLen"));
            public static int originalPriceLen = int.Parse(Standard.ConfigValue("originalPriceLen"));
        }

        private static void ItemSet()
        {
            try
            {
                RDSweepStagingItemSet(
                                clientNumber,
                                fileDate,
                                spinCode,
                                securityId,
                                cusipCountryCode,
                                cusip,
                                cusipCheckDigitCode,
                                cusipInternationalCheckDigit,
                                settlementDate,
                                customerAccountBranchPrefixCode,
                                customerAccountBranchCodeId,
                                customerAccountID,
                                customerAccountTypeCode,
                                customerAccountCheckDigitCode,
                                customerAccountFillCon,
                                tagDupNum,
                                tagDate,
                                tagSourceID,
                                tagSerialNumber,
                                remainingQuantity,
                                remainingAmount,
                                brokerNumber,
                                clhNum,
                                tradeDate,
                                clientStatusFlag,
                                originCode,
                                currencyCode,
                                blotterCode,
                                regRepNumber,
                                controlNumberID,
                                sendDTCFlag,
                                confirmationInd,
                                confirmationDate,
                                affirmationInd,
                                affirmDate,
                                securityProductCode,
                                housePriceInd,
                                reorgMessageInd,
                                divdMessageInd,
                                dueBillInd,
                                dtcEligableInd,
                                sameDayFundsInd,
                                securityDescription1,
                                securityDescription2,
                                marketPrice,
                                mtmAmount,
                                markUpDownInd,
                                flashMessage,
                                lastReclaim,
                                daysToDeliver,
                                daysOnFile,
                                settleOfficeId,
                                balordControlNumber,
                                autoDoInd,
                                partialExecBreakNumber,
                                contraExecBreakNumber,
                                offsetAccountBranchPrefixCode,
                                offsetAccountBranchCode,
                                offsetAccountBodyId,
                                offsetAccountTypeCode,
                                offsetAccountCheckDigitCode,
                                tradingAccountBranchPrefixCode,
                                tradingAccountBranchCode,
                                tradingAccountBodyId,
                                tradingAccountTypeCode,
                                tradingAccountCheckDigitCode,
                                clientControlID,
                                symbolId,
                                cageTransControlNumber,
                                originalPrice,
                                actUserId);
            }
            catch (Exception err)
            {
                Log.Write("ERROR: " + err.Message + "   [RDSWeepParse.ItemSet]", Log.Error, 1);
            }
        }


        public void ParseRdSweep()
        {


            string filePath = Standard.ConfigValue("FilePath");
            string fileName = Standard.ConfigValue("FileName");
            string fileContent;
            string tempMM = "";
            string tempDD = "";
            string tempYYYY = "";
            string temp = "";
            
            int itemCountImported = 0;                   
            int errCount = 0;            

            filePath = filePath + fileName;
            StreamReader streamReader = null;

            try
            {
                streamReader = new StreamReader(filePath);
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                fileContent = streamReader.ReadLine().ToString();
                tempMM = fileContent.Substring(FileColLayout.fileMMCol, FileLenLayout.fileMMLen);
                tempDD = fileContent.Substring(FileColLayout.fileDDCol, FileLenLayout.fileDDLen);
                tempYYYY = fileContent.Substring(FileColLayout.fileYYYYCol, FileLenLayout.fileYYYYLen);

                if (tempYYYY.Length == 2)
                {
                    fileDate = "20" + tempYYYY + tempMM + tempDD;
                }
                else
                {
                    fileDate = tempYYYY + tempMM + tempDD;
                }

                Log.Write("RDSWEEP Parser Load starting for file date: " + fileDate + "   [RDSWeepParse.ParseRdSweep]", Log.Information, 2);                

                while (!streamReader.EndOfStream)
                {

                    fileContent = streamReader.ReadLine().ToString();

                    try
                    {
                        clientNumber = fileContent.Substring(FileColLayout.ClientNumberCol, FileLenLayout.clientNumberLen).Trim();
                        spinCode = fileContent.Substring(FileColLayout.SpinCodeCol, FileLenLayout.spinCodeLen).Trim();
                        securityId = fileContent.Substring(FileColLayout.SecurityIdCol, FileLenLayout.securityIdLen).Trim();
                        cusipCountryCode = fileContent.Substring(FileColLayout.cusipCountryCodeCol, FileLenLayout.cusipCountryCodeLen).Trim();
                        cusip = fileContent.Substring(FileColLayout.cusipCol, FileLenLayout.cusipLen).Trim();
                        cusipCheckDigitCode = fileContent.Substring(FileColLayout.cusipCheckDigitCodeCol, FileLenLayout.cusipCheckDigitCodeLen).Trim();
                        cusipInternationalCheckDigit = fileContent.Substring(FileColLayout.cusipInternationalCheckDigitCol, FileLenLayout.cusipInternationalCheckDigitLen).Trim();

                        temp = fileContent.Substring(FileColLayout.settlementDateCol, FileLenLayout.settlementDateLen).Trim();
                        if ((int.Parse(temp)) == 0)
                        {
                            settlementDate = "";
                        }
                        else
                        {
                            settlementDate = fileContent.Substring(FileColLayout.settlementDateCol, FileLenLayout.settlementDateLen).Trim();
                        }

                        customerAccountBranchPrefixCode = fileContent.Substring(FileColLayout.customerAccountBranchPrefixCodeCol, FileLenLayout.customerAccountBranchPrefixCodeLen).Trim();
                        customerAccountBranchCodeId = fileContent.Substring(FileColLayout.customerAccountBranchCodeIdCol, FileLenLayout.customerAccountBranchCodeIdLen).Trim();
                        customerAccountID = fileContent.Substring(FileColLayout.customerAccountIDCol, FileLenLayout.customerAccountIDLen).Trim();
                        customerAccountTypeCode = fileContent.Substring(FileColLayout.customerAccountTypeCodeCol, FileLenLayout.customerAccountTypeCodeLen).Trim();
                        customerAccountCheckDigitCode = fileContent.Substring(FileColLayout.customerAccountCheckDigitCodeCol, FileLenLayout.customerAccountCheckDigitCodeLen);
                        customerAccountFillCon = fileContent.Substring(FileColLayout.customerAccountFillConCol, FileLenLayout.customerAccountFillConLen);
                        tagDupNum = int.Parse(fileContent.Substring(FileColLayout.tagDupNumCol, FileLenLayout.tagDupNumLen).ToString().Trim());

                        temp = fileContent.Substring(FileColLayout.tagDateCol, FileLenLayout.tagDateLen).Trim();
                        tempYYYY = "20" + temp.Substring(0, 2);
                        tempMM = temp.Substring(2, 2);
                        tempDD = temp.Substring(4, 2);

                        tagDate = tempYYYY + tempMM + tempDD;

                        tagSourceID = fileContent.Substring(FileColLayout.tagSourceIDCol, FileLenLayout.tagSourceIDLen).Trim();
                        tagSerialNumber = int.Parse(fileContent.Substring(FileColLayout.tagSerialNumberCol, FileLenLayout.tagSerialNumberLen).ToString().Trim());
                        remainingQuantity = long.Parse(fileContent.Substring(FileColLayout.remainingQuantityCol, FileLenLayout.remainingQuantityLen).ToString().Trim());
                        remainingAmount = float.Parse(fileContent.Substring(FileColLayout.remainingAmountCol, FileLenLayout.remainingAmountLen).ToString().Trim());
                        brokerNumber = fileContent.Substring(FileColLayout.brokerNumberCol, FileLenLayout.brokerNumberLen).Trim();
                        clhNum = fileContent.Substring(FileColLayout.clhNumCol, FileLenLayout.clhNumLen).Trim();

                        temp = fileContent.Substring(FileColLayout.tradeDateCol, FileLenLayout.tradeDateLen).Trim();
                        if ((int.Parse(temp)) == 0)
                        {
                            tradeDate = "";
                        }
                        else
                        {
                            tradeDate = fileContent.Substring(FileColLayout.tradeDateCol, FileLenLayout.tradeDateLen).Trim();
                        }

                        clientStatusFlag = fileContent.Substring(FileColLayout.clientStatusFlagCol, FileLenLayout.clientStatusFlagLen).Trim();
                        originCode = fileContent.Substring(FileColLayout.originCodeCol, FileLenLayout.originCodeLen).Trim();
                        currencyCode = fileContent.Substring(FileColLayout.currencyCodeCol, FileLenLayout.currencyCodeLen).Trim();
                        blotterCode = fileContent.Substring(FileColLayout.blotterCodeCol, FileLenLayout.blotterCodeLen).Trim();
                        regRepNumber = fileContent.Substring(FileColLayout.regRepNumberCol, FileLenLayout.regRepNumberLen).Trim();
                        controlNumberID = fileContent.Substring(FileColLayout.controlNumberIDCol, FileLenLayout.controlNumberIDLen).Trim();
                        sendDTCFlag = fileContent.Substring(FileColLayout.sendDTCFlagCol, FileLenLayout.sendDTCFlagLen).Trim();
                        confirmationInd = fileContent.Substring(FileColLayout.confirmationIndCol, FileLenLayout.confirmationIndLen).Trim();

                        temp = fileContent.Substring(FileColLayout.confirmationDateCol, FileLenLayout.confirmationDateLen).Trim();
                        if ((int.Parse(temp)) == 0)
                        {
                            confirmationDate = "";
                        }
                        else
                        {
                            confirmationDate = fileContent.Substring(FileColLayout.confirmationDateCol, FileLenLayout.confirmationDateLen).Trim();
                        }

                        affirmationInd = fileContent.Substring(FileColLayout.affirmationIndCol, FileLenLayout.affirmationIndLen).Trim();

                        temp = fileContent.Substring(FileColLayout.affirmDateCol, FileLenLayout.affirmDateLen).Trim();

                        if ((int.Parse(temp)) == 0)
                        {
                            affirmDate = "";
                        }
                        else
                        {
                            affirmDate = fileContent.Substring(FileColLayout.affirmDateCol, FileLenLayout.affirmDateLen).Trim();
                        }

                        securityProductCode = fileContent.Substring(FileColLayout.securityProductCodeCol, FileLenLayout.securityProductCodeLen).Trim();
                        housePriceInd = fileContent.Substring(FileColLayout.housePriceIndCol, FileLenLayout.housePriceIndLen).Trim();
                        reorgMessageInd = fileContent.Substring(FileColLayout.reorgMessageIndCol, FileLenLayout.reorgMessageIndLen).Trim();
                        divdMessageInd = fileContent.Substring(FileColLayout.divdMessageIndCol, FileLenLayout.divdMessageIndLen).Trim();
                        dueBillInd = fileContent.Substring(FileColLayout.dueBillIndCol, FileLenLayout.dueBillIndLen).Trim();
                        dtcEligableInd = fileContent.Substring(FileColLayout.dtcEligableIndCol, FileLenLayout.dtcEligableIndLen).Trim();
                        sameDayFundsInd = fileContent.Substring(FileColLayout.sameDayFundsIndCol, FileLenLayout.sameDayFundsIndLen).Trim();
                        securityDescription1 = fileContent.Substring(FileColLayout.securityDescription1Col, FileLenLayout.securityDescription1Len).Trim();
                        securityDescription2 = fileContent.Substring(FileColLayout.securityDescription2Col, FileLenLayout.securityDescription2Len).Trim();
                        marketPrice = float.Parse(fileContent.Substring(FileColLayout.marketPriceCol, FileLenLayout.marketPriceLen).ToString());
                        mtmAmount = float.Parse(fileContent.Substring(FileColLayout.mtmAmountCol, FileLenLayout.mtmAmountLen).ToString());
                        markUpDownInd = fileContent.Substring(FileColLayout.markUpDownIndCol, FileLenLayout.markUpDownIndLen).Trim();
                        flashMessage = fileContent.Substring(FileColLayout.flashMessageCol, FileLenLayout.flashMessageLen).Trim();
                        lastReclaim = int.Parse(fileContent.Substring(FileColLayout.lastReclaimCol, FileLenLayout.lastReclaimLen).ToString());
                        daysToDeliver = int.Parse(fileContent.Substring(FileColLayout.daysToDeliverCol, FileLenLayout.daysToDeliverLen).ToString());
                        daysOnFile = int.Parse(fileContent.Substring(FileColLayout.daysOnFileCol, FileLenLayout.daysOnFileLen).ToString());
                        settleOfficeId = fileContent.Substring(FileColLayout.settleOfficeIdCol, FileLenLayout.settleOfficeIdLen).Trim();
                        balordControlNumber = fileContent.Substring(FileColLayout.balordControlNumberCol, FileLenLayout.balordControlNumberLen).Trim();
                        autoDoInd = fileContent.Substring(FileColLayout.autoDoIndCol, FileLenLayout.autoDoIndLen).Trim();
                        partialExecBreakNumber = fileContent.Substring(FileColLayout.partialExecBreakNumberCol, FileLenLayout.partialExecBreakNumberLen).Trim();
                        contraExecBreakNumber = fileContent.Substring(FileColLayout.contraExecBreakNumberCol, FileLenLayout.contraExecBreakNumberLen).Trim();
                        offsetAccountBranchPrefixCode = fileContent.Substring(FileColLayout.offsetAccountBranchPrefixCodeCol, FileLenLayout.offsetAccountBranchPrefixCodeLen).Trim();
                        offsetAccountBranchCode = fileContent.Substring(FileColLayout.offsetAccountBranchCodeCol, FileLenLayout.offsetAccountBranchCodeLen).Trim();
                        offsetAccountBodyId = fileContent.Substring(FileColLayout.offsetAccountBodyIdCol, FileLenLayout.offsetAccountBodyIdLen).Trim();
                        offsetAccountTypeCode = fileContent.Substring(FileColLayout.offsetAccountTypeCodeCol, FileLenLayout.offsetAccountTypeCodeLen).Trim();
                        offsetAccountCheckDigitCode = fileContent.Substring(FileColLayout.offsetAccountCheckDigitCodeCol, FileLenLayout.offsetAccountCheckDigitCodeLen).Trim();
                        tradingAccountBranchPrefixCode = fileContent.Substring(FileColLayout.tradingAccountBranchPrefixCodeCol, FileLenLayout.tradingAccountBranchPrefixCodeLen).Trim();
                        tradingAccountBranchCode = fileContent.Substring(FileColLayout.tradingAccountBranchCodeCol, FileLenLayout.tradingAccountBranchCodeLen).Trim();
                        tradingAccountBodyId = fileContent.Substring(FileColLayout.tradingAccountBodyIdCol, FileLenLayout.tradingAccountBodyIdLen).Trim();
                        tradingAccountTypeCode = fileContent.Substring(FileColLayout.tradingAccountTypeCodeCol, FileLenLayout.tradingAccountTypeCodeLen).Trim();
                        tradingAccountCheckDigitCode = fileContent.Substring(FileColLayout.tradingAccountCheckDigitCodeCol, FileLenLayout.tradingAccountCheckDigitCodeLen).Trim();
                        clientControlID = fileContent.Substring(FileColLayout.clientControlIDCol, FileLenLayout.clientControlIDLen).Trim();
                        symbolId = fileContent.Substring(FileColLayout.symbolIdCol, FileLenLayout.symbolIdLen).Trim();
                        cageTransControlNumber = fileContent.Substring(FileColLayout.cageTransControlNumberCol, FileLenLayout.cageTransControlNumberLen).Trim();
                        originalPrice = fileContent.Substring(FileColLayout.originalPriceCol, FileLenLayout.originalPriceLen).Trim();
                        actUserId = "ADMIN";

                        ItemSet();
                        itemCountImported++;
                    }
                    catch (Exception error)
                    {
                        Log.Write("ERROR: " + error.Message + "   [RDSWeepParse.LoadData]", Log.Information, 2);
                        errCount++;
                    }
                }

                Log.Write("RDSWEEP Parser Loaded " + itemCountImported.ToString() + " records for " + fileDate + "  with " + errCount + "errors. [RDSWeepParse.ParseRdSweep]", 1);                
            }
            catch (Exception err)
            {
                Log.Write("ERROR: " + err.Message + "   [RDSWeepParse.ParseRdSweep]", 1);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }                
            }
        }

        private static void RDSweepStagingItemSet(
            string clientNumber,
            string fileDate,
            string spinCode,
            string securityId,
            string cusipCountryCode,
            string cusip,
            string cusipCheckDigitCode,
            string cusipInternationalCheckDigit,
            string settlementDate,
            string customerAccountBranchPrefixCode,
            string customerAccountBranchCodeId,
            string customerAccountID,
            string customerAccountTypeCode,
            string customerAccountCheckDigitCode,
            string customerAccountFillCon,
            int tagDupNum,
            string tagDate,
            string tagSourceID,
            int tagSerialNumber,
            long remainingQuantity,
            float remainingAmount,
            string brokerNumber,
            string clhNum,
            string tradeDate,
            string clientStatusFlag,
            string originCode,
            string currencyCode,
            string blotterCode,
            string regRepNumber,
            string controlNumberID,
            string sendDTCFlag,
            string confirmationInd,
            string confirmationDate,
            string affirmationInd,
            string affirmDate,
            string securityProductCode,
            string housePriceInd,
            string reorgMessageInd,
            string divdMessageInd,
            string dueBillInd,
            string dtcEligableInd,
            string sameDayFundsInd,
            string securityDescription1,
            string securityDescription2,
            float marketPrice,
            float mtmAmount,
            string markUpDownInd,
            string flashMessage,
            long lastReclaim,
            int daysToDeliver,
            int daysOnFile,
            string settleOfficeId,
            string balordControlNumber,
            string autoDoInd,
            string partialExecBreakNumber,
            string contraExecBreakNumber,
            string offsetAccountBranchPrefixCode,
            string offsetAccountBranchCode,
            string offsetAccountBodyId,
            string offsetAccountTypeCode,
            string offsetAccountCheckDigitCode,
            string tradingAccountBranchPrefixCode,
            string tradingAccountBranchCode,
            string tradingAccountBodyId,
            string tradingAccountTypeCode,
            string tradingAccountCheckDigitCode,
            string clientControlID,
            string symbolId,
            string cageTransControlNumber,
            string originalPrice,
            string actUserId)
        {
            SqlConnection dbCn = new SqlConnection(StockLoan.DataAccess.DBStandardFunctions.DbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRdSweepStagingSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramClientNumber = dbCmd.Parameters.Add("@ClientNumber", SqlDbType.VarChar, 3);
                paramClientNumber.Value = clientNumber;

                SqlParameter paramFileDate = dbCmd.Parameters.Add("@FileDate", SqlDbType.VarChar, 8);
                paramFileDate.Value = fileDate;

                if (!spinCode.Equals(""))
                {
                    SqlParameter paramSpinCode = dbCmd.Parameters.Add("@SpinCode", SqlDbType.VarChar, 1);
                    paramSpinCode.Value = spinCode;
                }

                if (!securityId.Equals(""))
                {
                    SqlParameter paramSecurityId = dbCmd.Parameters.Add("@SecurityId", SqlDbType.VarChar, 7);
                    paramSecurityId.Value = securityId;
                }

                if (!cusipCountryCode.Equals(""))
                {
                    SqlParameter paramCusipCountryCode = dbCmd.Parameters.Add("@CusipCountryCode", SqlDbType.VarChar, 2);
                    paramCusipCountryCode.Value = cusipCountryCode;
                }

                if (!cusip.Equals(""))
                {
                    SqlParameter paramCusip = dbCmd.Parameters.Add("@CUSIP", SqlDbType.VarChar, 8);
                    paramCusip.Value = cusip;
                }

                if (!cusipCheckDigitCode.Equals(""))
                {
                    SqlParameter paramCusipCheckDigitCode = dbCmd.Parameters.Add("@CusipCheckDigitCode", SqlDbType.VarChar, 1);
                    paramCusipCheckDigitCode.Value = cusipCheckDigitCode;
                }

                if (!cusipInternationalCheckDigit.Equals(""))
                {
                    SqlParameter paramCusipInternationalCheckDigit = dbCmd.Parameters.Add("@CusipInternationalCheckDigit", SqlDbType.VarChar, 1);
                    paramCusipInternationalCheckDigit.Value = cusipInternationalCheckDigit;
                }

                if (!settlementDate.Equals(""))
                {
                    SqlParameter paramSettlementDate = dbCmd.Parameters.Add("@SettlementDate", SqlDbType.VarChar, 8);
                    paramSettlementDate.Value = settlementDate;
                }

                if (!customerAccountBranchPrefixCode.Equals(""))
                {
                    SqlParameter paramCustomerAccountBranchPrefixCode = dbCmd.Parameters.Add("@CustomerAccountBranchPrefixCode", SqlDbType.VarChar, 2);
                    paramCustomerAccountBranchPrefixCode.Value = customerAccountBranchPrefixCode;
                }

                if (!customerAccountBranchCodeId.Equals(""))
                {
                    SqlParameter paramCustomerAccountBranchCodeId = dbCmd.Parameters.Add("@CustomerAccountBranchCodeId", SqlDbType.VarChar, 3);
                    paramCustomerAccountBranchCodeId.Value = customerAccountBranchCodeId;
                }

                if (!customerAccountID.Equals(""))
                {
                    SqlParameter paramCustomerAccountID = dbCmd.Parameters.Add("@CustomerAccountID", SqlDbType.VarChar, 5);
                    paramCustomerAccountID.Value = customerAccountID;
                }

                if (!customerAccountTypeCode.Equals(""))
                {
                    SqlParameter paramCustomerAccountTypeCode = dbCmd.Parameters.Add("@CustomerAccountTypeCode", SqlDbType.VarChar, 1);
                    paramCustomerAccountTypeCode.Value = customerAccountTypeCode;
                }

                if (!customerAccountCheckDigitCode.Equals(""))
                {
                    SqlParameter paramCustomerAccountCheckDigitCode = dbCmd.Parameters.Add("@CustomerAccountCheckDigitCode", SqlDbType.VarChar, 1);
                    paramCustomerAccountCheckDigitCode.Value = customerAccountCheckDigitCode;
                }

                if (!customerAccountFillCon.Equals(""))
                {
                    SqlParameter paramCustomerAccountFillCon = dbCmd.Parameters.Add("@CustomerAccountFillCon", SqlDbType.VarChar, 2);
                    paramCustomerAccountFillCon.Value = customerAccountFillCon;
                }

                if (!tagDupNum.Equals(""))
                {
                    SqlParameter paramTagDupNum = dbCmd.Parameters.Add("@TagDupNum", SqlDbType.SmallInt);
                    paramTagDupNum.Value = tagDupNum;
                }

                if (!tagDate.Equals(""))
                {
                    SqlParameter paramTagDate = dbCmd.Parameters.Add("@TagDate", SqlDbType.VarChar, 8);
                    paramTagDate.Value = tagDate;
                }

                if (!tagSourceID.Equals(""))
                {
                    SqlParameter paramTagSourceID = dbCmd.Parameters.Add("@TagSourceID", SqlDbType.VarChar, 1);
                    paramTagSourceID.Value = tagSourceID;
                }

                if (!tagSerialNumber.Equals(""))
                {
                    SqlParameter paramTagSerialNumber = dbCmd.Parameters.Add("@TagSerialNumber", SqlDbType.Int);
                    paramTagSerialNumber.Value = tagSerialNumber;
                }

                if (!remainingQuantity.Equals(""))
                {
                    SqlParameter paramRemainingQuantity = dbCmd.Parameters.Add("@RemainingQuantity", SqlDbType.BigInt);
                    paramRemainingQuantity.Value = remainingQuantity;
                }

                if (!remainingAmount.Equals(""))
                {
                    SqlParameter paramRemainingAmount = dbCmd.Parameters.Add("@RemainingAmount", SqlDbType.Float);
                    paramRemainingAmount.Value = remainingAmount;
                }

                if (!brokerNumber.Equals(""))
                {
                    SqlParameter paramBrokerNumber = dbCmd.Parameters.Add("@BrokerNumber", SqlDbType.VarChar, 8);
                    paramBrokerNumber.Value = brokerNumber;
                }

                if (!clhNum.Equals(""))
                {
                    SqlParameter paramClhNum = dbCmd.Parameters.Add("@ClhNum", SqlDbType.VarChar, 5);
                    paramClhNum.Value = clhNum;
                }

                if (!tradeDate.Equals(""))
                {
                    SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.VarChar, 8);
                    paramTradeDate.Value = tradeDate;
                }

                if (!clientStatusFlag.Equals(""))
                {
                    SqlParameter paramClientStatusFlag = dbCmd.Parameters.Add("@ClientStatusFlag", SqlDbType.VarChar, 1);
                    paramClientStatusFlag.Value = clientStatusFlag;
                }

                if (!originCode.Equals(""))
                {
                    SqlParameter paramOriginCode = dbCmd.Parameters.Add("@OriginCode", SqlDbType.VarChar, 3);
                    paramOriginCode.Value = originCode;
                }

                if (!currencyCode.Equals(""))
                {
                    SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 2);
                    paramCurrencyCode.Value = currencyCode;
                }

                if (!blotterCode.Equals(""))
                {
                    SqlParameter paramBlotterCode = dbCmd.Parameters.Add("@BlotterCode", SqlDbType.VarChar, 3);
                    paramBlotterCode.Value = blotterCode;
                }

                if (!regRepNumber.Equals(""))
                {
                    SqlParameter paramRegRepNumber = dbCmd.Parameters.Add("@RegRepNumber", SqlDbType.VarChar, 3);
                    paramRegRepNumber.Value = regRepNumber;
                }

                if (!controlNumberID.Equals(""))
                {
                    SqlParameter paramControlNumberID = dbCmd.Parameters.Add("@ControlNumberID", SqlDbType.VarChar, 11);
                    paramControlNumberID.Value = controlNumberID;
                }

                if (!sendDTCFlag.Equals(""))
                {
                    SqlParameter paramSendDTCFlag = dbCmd.Parameters.Add("@SendDTCFlag", SqlDbType.VarChar, 1);
                    paramSendDTCFlag.Value = sendDTCFlag;
                }

                if (!confirmationInd.Equals(""))
                {
                    SqlParameter paramConfirmationInd = dbCmd.Parameters.Add("@ConfirmationInd", SqlDbType.VarChar, 1);
                    paramConfirmationInd.Value = confirmationInd;
                }

                if (!confirmationDate.Equals(""))
                {
                    SqlParameter paramConfirmationDate = dbCmd.Parameters.Add("@ConfirmationDate", SqlDbType.VarChar, 8);
                    paramConfirmationDate.Value = confirmationDate;
                }

                if (!affirmationInd.Equals(""))
                {
                    SqlParameter paramAffirmationInd = dbCmd.Parameters.Add("@AffirmationInd", SqlDbType.VarChar, 1);
                    paramAffirmationInd.Value = affirmationInd;
                }

                if (!affirmDate.Equals(""))
                {
                    SqlParameter paramAffirmDate = dbCmd.Parameters.Add("@AffirmDate", SqlDbType.VarChar, 8);
                    paramAffirmDate.Value = affirmDate;
                }

                if (!securityProductCode.Equals(""))
                {
                    SqlParameter paramSecurityProductCode = dbCmd.Parameters.Add("@SecurityProductCode", SqlDbType.VarChar, 3);
                    paramSecurityProductCode.Value = securityProductCode;
                }

                if (!housePriceInd.Equals(""))
                {
                    SqlParameter paramHousePriceInd = dbCmd.Parameters.Add("@HousePriceInd", SqlDbType.VarChar, 1);
                    paramHousePriceInd.Value = housePriceInd;
                }

                if (!reorgMessageInd.Equals(""))
                {
                    SqlParameter paramReorgMessageInd = dbCmd.Parameters.Add("@ReorgMessageInd", SqlDbType.VarChar, 1);
                    paramReorgMessageInd.Value = reorgMessageInd;
                }

                if (!divdMessageInd.Equals(""))
                {
                    SqlParameter paramDivdMessageInd = dbCmd.Parameters.Add("@DivdMessageInd", SqlDbType.VarChar, 1);
                    paramDivdMessageInd.Value = divdMessageInd;
                }

                if (!dueBillInd.Equals(""))
                {
                    SqlParameter paramDueBillInd = dbCmd.Parameters.Add("@DueBillInd", SqlDbType.VarChar, 1);
                    paramDueBillInd.Value = dueBillInd;
                }

                if (!dtcEligableInd.Equals(""))
                {
                    SqlParameter paramDtcEligableInd = dbCmd.Parameters.Add("@DtcEligableInd", SqlDbType.VarChar, 1);
                    paramDtcEligableInd.Value = dtcEligableInd;
                }

                if (!sameDayFundsInd.Equals(""))
                {
                    SqlParameter paramSameDayFundsInd = dbCmd.Parameters.Add("@SameDayFundsInd", SqlDbType.VarChar, 1);
                    paramSameDayFundsInd.Value = sameDayFundsInd;
                }

                if (!securityDescription1.Equals(""))
                {
                    SqlParameter paramSecurityDescription1 = dbCmd.Parameters.Add("@SecurityDescription1", SqlDbType.VarChar, 30);
                    paramSecurityDescription1.Value = securityDescription1;
                }

                if (!securityDescription2.Equals(""))
                {
                    SqlParameter paramSecurityDescription2 = dbCmd.Parameters.Add("@SecurityDescription2", SqlDbType.VarChar, 30);
                    paramSecurityDescription2.Value = securityDescription2;
                }

                if (!marketPrice.Equals(""))
                {
                    SqlParameter paramMarketPrice = dbCmd.Parameters.Add("@MarketPrice", SqlDbType.Float);
                    paramMarketPrice.Value = marketPrice;
                }

                if (!mtmAmount.Equals(""))
                {
                    SqlParameter paramMtmAmount = dbCmd.Parameters.Add("@MtmAmount", SqlDbType.Float);
                    paramMtmAmount.Value = mtmAmount;
                }

                if (!markUpDownInd.Equals(""))
                {
                    SqlParameter paramMarkUpDownInd = dbCmd.Parameters.Add("@MarkUpDownInd", SqlDbType.VarChar, 1);
                    paramMarkUpDownInd.Value = markUpDownInd;
                }

                if (!flashMessage.Equals(""))
                {
                    SqlParameter paramFlashMessage = dbCmd.Parameters.Add("@FlashMessage", SqlDbType.VarChar, 27);
                    paramFlashMessage.Value = flashMessage;
                }

                if (!lastReclaim.Equals(""))
                {
                    SqlParameter paramLastReclaim = dbCmd.Parameters.Add("@LastReclaim", SqlDbType.BigInt);
                    paramLastReclaim.Value = lastReclaim;
                }

                if (!daysToDeliver.Equals(""))
                {
                    SqlParameter paramDaysToDeliver = dbCmd.Parameters.Add("@DaysToDeliver", SqlDbType.Int);
                    paramDaysToDeliver.Value = daysToDeliver;
                }
                if (!daysOnFile.Equals(""))
                {
                    SqlParameter paramDaysOnFile = dbCmd.Parameters.Add("@DaysOnFile", SqlDbType.Int);
                    paramDaysOnFile.Value = daysOnFile;
                }

                if (!settleOfficeId.Equals(""))
                {
                    SqlParameter paramSettleOfficeId = dbCmd.Parameters.Add("@SettleOfficeId", SqlDbType.VarChar, 3);
                    paramSettleOfficeId.Value = settleOfficeId;
                }

                if (!balordControlNumber.Equals(""))
                {
                    SqlParameter paramBalordControlNumber = dbCmd.Parameters.Add("@BalordControlNumber", SqlDbType.VarChar, 10);
                    paramBalordControlNumber.Value = balordControlNumber;
                }

                if (!autoDoInd.Equals(""))
                {
                    SqlParameter paramAutoDoInd = dbCmd.Parameters.Add("@AutoDoInd", SqlDbType.VarChar, 1);
                    paramAutoDoInd.Value = autoDoInd;
                }

                if (!partialExecBreakNumber.Equals(""))
                {
                    SqlParameter paramPartialExecBreakNumber = dbCmd.Parameters.Add("@PartialExecBreakNumber", SqlDbType.VarChar, 4);
                    paramPartialExecBreakNumber.Value = partialExecBreakNumber;
                }

                if (!contraExecBreakNumber.Equals(""))
                {
                    SqlParameter paramContraExecBreakNumber = dbCmd.Parameters.Add("@ContraExecBreakNumber", SqlDbType.VarChar, 4);
                    paramContraExecBreakNumber.Value = contraExecBreakNumber;
                }

                if (!offsetAccountBranchPrefixCode.Equals(""))
                {
                    SqlParameter paramOffsetAccountBranchPrefixCode = dbCmd.Parameters.Add("@OffsetAccountBranchPrefixCode", SqlDbType.VarChar, 2);
                    paramOffsetAccountBranchPrefixCode.Value = offsetAccountBranchPrefixCode;
                }

                if (!offsetAccountBranchCode.Equals(""))
                {
                    SqlParameter paramOffsetAccountBranchCode = dbCmd.Parameters.Add("@OffsetAccountBranchCode", SqlDbType.VarChar, 3);
                    paramOffsetAccountBranchCode.Value = offsetAccountBranchCode;
                }

                if (!offsetAccountBodyId.Equals(""))
                {
                    SqlParameter paramOffsetAccountBodyId = dbCmd.Parameters.Add("@OffsetAccountBodyId", SqlDbType.VarChar, 5);
                    paramOffsetAccountBodyId.Value = offsetAccountBodyId;
                }

                if (!offsetAccountTypeCode.Equals(""))
                {
                    SqlParameter paramOffsetAccountTypeCode = dbCmd.Parameters.Add("@OffsetAccountTypeCode", SqlDbType.VarChar, 1);
                    paramOffsetAccountTypeCode.Value = offsetAccountTypeCode;
                }

                if (!offsetAccountCheckDigitCode.Equals(""))
                {
                    SqlParameter paramOffsetAccountCheckDigitCode = dbCmd.Parameters.Add("@OffsetAccountCheckDigitCode", SqlDbType.VarChar, 1);
                    paramOffsetAccountCheckDigitCode.Value = offsetAccountCheckDigitCode;
                }

                if (!tradingAccountBranchPrefixCode.Equals(""))
                {
                    SqlParameter paramTradingAccountBranchPrefixCode = dbCmd.Parameters.Add("@TradingAccountBranchPrefixCode", SqlDbType.VarChar, 2);
                    paramTradingAccountBranchPrefixCode.Value = tradingAccountBranchPrefixCode;
                }

                if (!tradingAccountBranchCode.Equals(""))
                {
                    SqlParameter paramTradingAccountBranchCode = dbCmd.Parameters.Add("@TradingAccountBranchCode", SqlDbType.VarChar, 3);
                    paramTradingAccountBranchCode.Value = tradingAccountBranchCode;
                }

                if (!tradingAccountBodyId.Equals(""))
                {
                    SqlParameter paramTradingAccountBodyId = dbCmd.Parameters.Add("@TradingAccountBodyId", SqlDbType.VarChar, 5);
                    paramTradingAccountBodyId.Value = tradingAccountBodyId;
                }

                if (!tradingAccountTypeCode.Equals(""))
                {
                    SqlParameter paramTradingAccountTypeCode = dbCmd.Parameters.Add("@TradingAccountTypeCode", SqlDbType.VarChar, 1);
                    paramTradingAccountTypeCode.Value = tradingAccountTypeCode;
                }

                if (!tradingAccountCheckDigitCode.Equals(""))
                {
                    SqlParameter paramTradingAccountCheckDigitCode = dbCmd.Parameters.Add("@TradingAccountCheckDigitCode", SqlDbType.VarChar, 1);
                    paramTradingAccountCheckDigitCode.Value = tradingAccountCheckDigitCode;
                }

                if (!clientControlID.Equals(""))
                {
                    SqlParameter paramClientControlID = dbCmd.Parameters.Add("@ClientControlID", SqlDbType.VarChar, 20);
                    paramClientControlID.Value = clientControlID;
                }

                if (!symbolId.Equals(""))
                {
                    SqlParameter paramSymbolId = dbCmd.Parameters.Add("@SymbolId", SqlDbType.VarChar, 12);
                    paramSymbolId.Value = symbolId;
                }

                if (!cageTransControlNumber.Equals(""))
                {
                    SqlParameter paramCageTransControlNumber = dbCmd.Parameters.Add("@CageTransControlNumber", SqlDbType.VarChar, 25);
                    paramCageTransControlNumber.Value = cageTransControlNumber;
                }

                if (!originalPrice.Equals(""))
                {
                    SqlParameter paramOriginalPrice = dbCmd.Parameters.Add("@OriginalPrice", SqlDbType.VarChar, 16);
                    paramOriginalPrice.Value = originalPrice;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }
    }
}
