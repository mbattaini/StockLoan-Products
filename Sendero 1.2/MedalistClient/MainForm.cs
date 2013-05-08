// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Net;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Anetics.Common;

namespace Anetics.Medalist
{
    /// <summary>
    /// Main form and object for the Medalist client application.
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        const short ALERT_MAX_ITEM = 150;

        private bool eventsEnabled;
        private string userShortName;
        private string userEmailAddress;
        private string applicationName;
        private DateTime dateToday;

        public IService ServiceAgent = null;
        public IAdmin AdminAgent = null;
        public IShortSale ShortSaleAgent = null;
        public IPosition PositionAgent = null;
        public IRebate RebateAgent = null;
        public ISubstitution SubstitutionAgent = null;

        delegate void updatePilotState(PilotState state);

        delegate void removeAtAlertCombo(int index);
        delegate void insertIntoAlertCombo(int index, object text);
        delegate void selectedIndexAlertCombo(int index);
        delegate void selectAlertCombo(int start, int length);
        delegate void refreshAlertCombo();

        internal InventoryLookupForm inventoryLookupForm = null;
        internal PositionBoxSummaryForm positionBoxSummaryForm = null;
        internal PositionDealBlotterForm positionDealBlotterForm = null;
        internal PositionContractBlotterForm positionContractBlotterForm = null;
        internal PositionOpenContractsForm positionOpenContractsForm = null;
        internal PositionRecallsForm positionRecallsForm = null;
        internal PositionBankLoanForm positionBankLoanForm = null;
        internal ShortSaleLocateForm shortSaleLocateForm = null;
        internal PositionDeficitBuyInsAccountsBorrowedForm positionDeficitBuyInsAccountsBorrowedForm = null;
        public SubstitutionInputForm substitutionInputForm = null;
        internal SubstitutionInventoryForm substitutionInventoryForm;

        internal PositionABRForm positionABRForm = null;

        public DataSet DeskQuipDataSet;
        private DataView DeskQuipDataView;
        private ArrayList deskQuipEventArgsArray;

        private DeskQuipEventWrapper deskQuipEventWrapper;
        private DeskQuipEventHandler deskQuipEventHandler;
        private bool deskQuipIsReady = false;

        private HeartbeatEventWrapper heartbeatEventWrapper;

        private System.Timers.Timer AlertTimer;
        private System.Timers.Timer HeartbeatTimer;

        private Anetics.Medalist.Pilot AlertPilot;

        private Anetics.Medalist.Pilot ProcessPilot;
        private Anetics.Medalist.Pilot ServerPilot;

        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.ImageList ImageList;

        private C1.Win.C1Command.C1MainMenu MainMenu;

        private C1.Win.C1Command.C1CommandMenu AdminCommand;
        private C1.Win.C1Command.C1CommandMenu AdminUserCommand;
        private C1.Win.C1Command.C1CommandMenu InventoryCommand;
        private C1.Win.C1Command.C1CommandMenu PositionCommand;
        private C1.Win.C1Command.C1CommandMenu PositionAutoCommand;
        private C1.Win.C1Command.C1CommandMenu ShortSaleCommand;
        private C1.Win.C1Command.C1CommandMenu ShortSaleListsCommand;
        private C1.Win.C1Command.C1CommandMenu HelpCommand;

        private C1.Win.C1Command.C1CommandHolder CommandHolder;

        private C1.Win.C1Command.C1Command AdminHolidaysCommand;
        private C1.Win.C1Command.C1Command AdminKeyValuesCommand;
        private C1.Win.C1Command.C1Command AdminBooksCommand;
        private C1.Win.C1Command.C1Command AdminUserPersonnelCommand;
        private C1.Win.C1Command.C1Command AdminUserRolesCommand;
        private C1.Win.C1Command.C1Command InventorySubscriberCommand;
        private C1.Win.C1Command.C1Command InventoryPublisherCommand;
        private C1.Win.C1Command.C1Command InventoryLookupCommand;
        private C1.Win.C1Command.C1Command InventoryDeskInputCommand;
        private C1.Win.C1Command.C1Command PositionBoxSummaryCommand;
        private C1.Win.C1Command.C1Command PositionAutoBorrowCommand;
        private C1.Win.C1Command.C1Command PositionAutoLoanCommand;
        private C1.Win.C1Command.C1Command PositionOpenContractsCommand;
        private C1.Win.C1Command.C1Command PositionDealBlotterCommand;
        private C1.Win.C1Command.C1Command PositionContractBlotterCommand;
        private C1.Win.C1Command.C1Command PositionRecallsCommand;
        private C1.Win.C1Command.C1Command PositionBankLoanCommand;
        private C1.Win.C1Command.C1Command ShortSaleListsEasyBorrowCommand;
        private C1.Win.C1Command.C1Command ShortSaleListsHardBorrrowCommand;
        private C1.Win.C1Command.C1Command ShortSaleListsNoLendCommand;
        private C1.Win.C1Command.C1Command ShortSaleListsThresholdCommand;
        private C1.Win.C1Command.C1Command ShortSaleLocatesCommand;
        private C1.Win.C1Command.C1Command HelpContentsCommand;
        private C1.Win.C1Command.C1Command HelpIndexCommand;
        private C1.Win.C1Command.C1Command HelpAboutCommand;
        private C1.Win.C1Command.C1Command ExternalInventoryCommand;

        private C1.Win.C1Command.C1CommandLink AdminMenuLink;
        private C1.Win.C1Command.C1CommandLink AdminUserMenuLink;
        private C1.Win.C1Command.C1CommandLink InventoryMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionAutoMenuLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleMenuLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsMenuLink;
        private C1.Win.C1Command.C1CommandLink HelpMenuLink;

        private C1.Win.C1Command.C1CommandLink AdminHolidaysMenuLink;
        private C1.Win.C1Command.C1CommandLink AdminKeyValuesMenuLink;
        private C1.Win.C1Command.C1CommandLink AdminBooksMenuLink;
        private C1.Win.C1Command.C1CommandLink AdminUserPersonnelMenuLink;
        private C1.Win.C1Command.C1CommandLink AdminUserRolesMenuLink;
        private C1.Win.C1Command.C1CommandLink InventorySubscriberMenuLink;
        private C1.Win.C1Command.C1CommandLink InventoryPublisherMenuLink;
        private C1.Win.C1Command.C1CommandLink InventoryLookupMenuLink;
        private C1.Win.C1Command.C1CommandLink InventoryDeskInputMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionBoxSummaryMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionAutoBorrowMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionAutoLoanMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionOpenContractsMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionDealBlotterMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionContractBlotterMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionRecallsMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionBankLoanMenuLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsEasyBorrowMenuLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsHardBorrrowMenuLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsNoLendMenuLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsThresholdMenuLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleLocatesMenuLink;
        private C1.Win.C1Command.C1CommandLink HelpContentsMenuLink;
        private C1.Win.C1Command.C1CommandLink HelpIndexMenuLink;
        private C1.Win.C1Command.C1CommandLink HelpAboutMenuLink;

        private C1.Win.C1Command.C1ToolBar MainToolBar;

        private C1.Win.C1Command.C1CommandLink AdminKeyValuesToolLink;
        private C1.Win.C1Command.C1CommandLink AdminBooksToolLink;
        private C1.Win.C1Command.C1CommandLink InventorySubscriberToolLink;
        private C1.Win.C1Command.C1CommandLink InventoryPublisherToolLink;
        private C1.Win.C1Command.C1CommandLink InventoryAvailabilityToolLink;
        private C1.Win.C1Command.C1CommandLink InventoryDeskInputToolLink;
        private C1.Win.C1Command.C1CommandLink PositionBoxSummaryToolLink;
        private C1.Win.C1Command.C1CommandLink PositionAutoBorrowToolLink;
        private C1.Win.C1Command.C1CommandLink PositionAutoLoanToolLink;
        private C1.Win.C1Command.C1CommandLink PositionOpenContractsToolLink;
        private C1.Win.C1Command.C1CommandLink PositionDealBlotterToolLink;
        private C1.Win.C1Command.C1CommandLink PositionContractBlotterToolLink;
        private C1.Win.C1Command.C1CommandLink PositionRecallsToolLink;
        private C1.Win.C1Command.C1CommandLink PositionBankLoanToolLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsEasyBorrowToolLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsHardBorrowToolLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsNoLendToolLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleListsThresholdToolLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleLocatesToolLink;

        private C1.Win.C1List.C1Combo DeskQuipCombo;
        public Anetics.Medalist.SecMasterControl MainSecMasterControl;

        private long recallQuantity;
        private long returnQuantity;

        private bool showFedFunds;
        private bool showLiborFunds;

        private C1.Win.C1Input.C1DropDownControl ProcessStatusDropDown;
        private System.Windows.Forms.ComboBox AlertCombo;
        private C1.Win.C1Command.C1Command ShortSaleTradingGroupsCommand;
        private C1.Win.C1Command.C1CommandLink ShortSaleTradingGroupsTookLink;
        private C1.Win.C1Command.C1CommandLink ShortSaleTradingGroupsMenuLink;
        private C1.Win.C1Command.C1Command PositionAccountCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1Command InventoryRatesCommand;
        private C1.Win.C1Command.C1CommandLink InventoryRatesMenuLink;
        private C1.Win.C1Command.C1CommandLink InventoryRatesToolLink;
        private C1.Win.C1Command.C1CommandMenu ShortSaleBillingCommand;
        private C1.Win.C1Command.C1Command CorresspondentValuesCommand;
        private C1.Win.C1Command.C1Command CorresspondentBillingCommand;
        private C1.Win.C1Command.C1CommandLink ShortSaleBillingMenulink;
        private C1.Win.C1Command.C1CommandLink ShortSaleBillingSummaryMenuLink;
        private C1.Win.C1Command.C1Command ShortSaleBillingSummaryCommand;
        private C1.Win.C1Command.C1Command ShortSaleLocatesChargesCommand;
        private C1.Win.C1Command.C1CommandLink HelpWebMenuLink;
        private C1.Win.C1Command.C1Command HelpWebCommand;
        private C1.Win.C1Command.C1CommandLink BillingLocateChargesMenuLink;
        private C1.Win.C1Command.C1CommandLink PositionDeficitBuyInsMenuLink;
        private C1.Win.C1Command.C1CommandMenu PositionDeficitBuyInsCommand;
        private C1.Win.C1Command.C1CommandLink PositionDeficitBuyInsSummaryMenuLink;
        private C1.Win.C1Command.C1Command PositionDeficitBuyInsSummaryCommand;
        private C1.Win.C1Command.C1CommandLink PositionAccountsBorrowedMenuItem;
        private C1.Win.C1Command.C1Command PositionAccountsBorrowedCommand;
        private C1.Win.C1Command.C1CommandLink ShortSaleBillingSummaryToolLink;
        private C1.Win.C1Command.C1Command ShortSaleBillingPositiveRebateCommand;
        private C1.Win.C1Command.C1Command PositionBorrowIncomeCommand;
        private C1.Win.C1Command.C1CommandLink PositionContractRateComparisonMenuLink;
        private C1.Win.C1Command.C1Command PositionContractRateComparisonCommand;
        private C1.Win.C1Command.C1CommandLink PositionContractRateComparisonToolLink;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1Command AdminFunctionsCommand;

        private C1.Win.C1Command.C1Command PositionABRCommand;
        private C1.Win.C1Command.C1CommandMenu SubstitutionCommand;
        private C1.Win.C1Command.C1CommandLink SubstitutionInventoryMenuLink;
        private C1.Win.C1Command.C1Command SubstitutionInventoryCommand;
        private C1.Win.C1Command.C1CommandLink SubstitutionLookupMenuLink;
        private C1.Win.C1Command.C1Command SubstitutionLookupCommand;
        private C1.Win.C1Command.C1CommandLink SubstitutionMenuLink;
        private C1.Win.C1Command.C1CommandLink SubstitutionLookupToolLink;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1CommandLink SubstitutionSegEntriesMenuLink;
        private C1.Win.C1Command.C1Command SubstitutionSegEntriesCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Command.C1Command SubstitutionUpdatedDeficitExcessCommand;
        private BackgroundWorker bwIdle = new BackgroundWorker();
        private C1.Win.C1Command.C1CommandLink c1CommandLink5;
        private C1.Win.C1Command.C1Command PositionBoxSummaryExpandedCommand;

        private System.ComponentModel.IContainer components;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                dateToday = DateTime.Today;
                eventsEnabled = bool.Parse(Standard.ConfigValue("EventsEnabled", "True"));

                applicationName = Standard.ConfigValue("ApplicationName", "Medalist");
                RegistryValue.Name = applicationName;
                this.Text = RegistryValue.Name;

                Log.Level = Standard.ConfigValue("LogLevel");
                Log.FilePath = Standard.ConfigValue("LogFilePath");

                Log.Write("", 1); // Adds blank line to log indicating new session.
                Log.Write("User " + this.UserId + " running version " + Application.ProductVersion + " [MainForm.MainForm]", Log.SuccessAudit, 1);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MainForm.MainForm]", Log.Error);
            }

            SplashForm splashForm = new SplashForm(Standard.ConfigValue("Licensee"), RegistryValue.Name);
            splashForm.Show();

            deskQuipEventArgsArray = new ArrayList();

            deskQuipEventWrapper = new DeskQuipEventWrapper();
            deskQuipEventWrapper.DeskQuipEvent += new DeskQuipEventHandler(DeskQuipOnEvent);

            deskQuipEventHandler = new DeskQuipEventHandler(DeskQuipDoEvent);

            heartbeatEventWrapper = new HeartbeatEventWrapper();
            heartbeatEventWrapper.HeartbeatEvent += new HeartbeatEventHandler(HeartbeatOnEvent);

            MainSecMasterControl.Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainMenu = new C1.Win.C1Command.C1MainMenu();
            this.CommandHolder = new C1.Win.C1Command.C1CommandHolder();
            this.AdminCommand = new C1.Win.C1Command.C1CommandMenu();
            this.AdminHolidaysMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.AdminHolidaysCommand = new C1.Win.C1Command.C1Command();
            this.AdminKeyValuesMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.AdminKeyValuesCommand = new C1.Win.C1Command.C1Command();
            this.AdminBooksMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.AdminBooksCommand = new C1.Win.C1Command.C1Command();
            this.AdminUserMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.AdminUserCommand = new C1.Win.C1Command.C1CommandMenu();
            this.AdminUserPersonnelMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.AdminUserPersonnelCommand = new C1.Win.C1Command.C1Command();
            this.AdminUserRolesMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.AdminUserRolesCommand = new C1.Win.C1Command.C1Command();
            this.InventoryCommand = new C1.Win.C1Command.C1CommandMenu();
            this.InventorySubscriberMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.InventorySubscriberCommand = new C1.Win.C1Command.C1Command();
            this.InventoryPublisherMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryPublisherCommand = new C1.Win.C1Command.C1Command();
            this.InventoryRatesMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryRatesCommand = new C1.Win.C1Command.C1Command();
            this.InventoryLookupMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryLookupCommand = new C1.Win.C1Command.C1Command();
            this.InventoryDeskInputMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryDeskInputCommand = new C1.Win.C1Command.C1Command();
            this.PositionCommand = new C1.Win.C1Command.C1CommandMenu();
            this.PositionAccountCommand = new C1.Win.C1Command.C1Command();
            this.PositionBoxSummaryMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionBoxSummaryCommand = new C1.Win.C1Command.C1Command();
            this.PositionAutoMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionAutoCommand = new C1.Win.C1Command.C1CommandMenu();
            this.PositionAutoBorrowMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionAutoBorrowCommand = new C1.Win.C1Command.C1Command();
            this.PositionAutoLoanMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionAutoLoanCommand = new C1.Win.C1Command.C1Command();
            this.PositionOpenContractsMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionOpenContractsCommand = new C1.Win.C1Command.C1Command();
            this.PositionContractRateComparisonMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionContractRateComparisonCommand = new C1.Win.C1Command.C1Command();
            this.PositionDealBlotterMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionDealBlotterCommand = new C1.Win.C1Command.C1Command();
            this.PositionContractBlotterMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionContractBlotterCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.PositionABRCommand = new C1.Win.C1Command.C1Command();
            this.PositionRecallsMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionRecallsCommand = new C1.Win.C1Command.C1Command();
            this.PositionBankLoanMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionBankLoanCommand = new C1.Win.C1Command.C1Command();
            this.PositionDeficitBuyInsMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionDeficitBuyInsCommand = new C1.Win.C1Command.C1CommandMenu();
            this.PositionAccountsBorrowedMenuItem = new C1.Win.C1Command.C1CommandLink();
            this.PositionAccountsBorrowedCommand = new C1.Win.C1Command.C1Command();
            this.PositionDeficitBuyInsSummaryMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionDeficitBuyInsSummaryCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleCommand = new C1.Win.C1Command.C1CommandMenu();
            this.ShortSaleBillingMenulink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleBillingCommand = new C1.Win.C1Command.C1CommandMenu();
            this.BillingLocateChargesMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleLocatesChargesCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleBillingSummaryMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleBillingSummaryCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleListsMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsCommand = new C1.Win.C1Command.C1CommandMenu();
            this.ShortSaleListsEasyBorrowMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsEasyBorrowCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleListsHardBorrrowMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsHardBorrrowCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleListsNoLendMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsNoLendCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleListsThresholdMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsThresholdCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleTradingGroupsMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleTradingGroupsCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleLocatesMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleLocatesCommand = new C1.Win.C1Command.C1Command();
            this.ExternalInventoryCommand = new C1.Win.C1Command.C1Command();
            this.HelpCommand = new C1.Win.C1Command.C1CommandMenu();
            this.HelpContentsMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.HelpContentsCommand = new C1.Win.C1Command.C1Command();
            this.HelpIndexMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.HelpIndexCommand = new C1.Win.C1Command.C1Command();
            this.HelpWebMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.HelpWebCommand = new C1.Win.C1Command.C1Command();
            this.HelpAboutMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.HelpAboutCommand = new C1.Win.C1Command.C1Command();
            this.CorresspondentValuesCommand = new C1.Win.C1Command.C1Command();
            this.CorresspondentBillingCommand = new C1.Win.C1Command.C1Command();
            this.ShortSaleBillingPositiveRebateCommand = new C1.Win.C1Command.C1Command();
            this.PositionBorrowIncomeCommand = new C1.Win.C1Command.C1Command();
            this.AdminFunctionsCommand = new C1.Win.C1Command.C1Command();
            this.SubstitutionCommand = new C1.Win.C1Command.C1CommandMenu();
            this.SubstitutionInventoryMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.SubstitutionInventoryCommand = new C1.Win.C1Command.C1Command();
            this.SubstitutionLookupMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.SubstitutionLookupCommand = new C1.Win.C1Command.C1Command();
            this.SubstitutionSegEntriesMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.SubstitutionSegEntriesCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.SubstitutionUpdatedDeficitExcessCommand = new C1.Win.C1Command.C1Command();
            this.AdminMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.SubstitutionMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.HelpMenuLink = new C1.Win.C1Command.C1CommandLink();
            this.MainToolBar = new C1.Win.C1Command.C1ToolBar();
            this.AdminKeyValuesToolLink = new C1.Win.C1Command.C1CommandLink();
            this.AdminBooksToolLink = new C1.Win.C1Command.C1CommandLink();
            this.InventorySubscriberToolLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryRatesToolLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryPublisherToolLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryAvailabilityToolLink = new C1.Win.C1Command.C1CommandLink();
            this.InventoryDeskInputToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionBoxSummaryToolLink = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.PositionAutoBorrowToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionAutoLoanToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionOpenContractsToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionContractRateComparisonToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionDealBlotterToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionContractBlotterToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionRecallsToolLink = new C1.Win.C1Command.C1CommandLink();
            this.PositionBankLoanToolLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsEasyBorrowToolLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsHardBorrowToolLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsNoLendToolLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleListsThresholdToolLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleLocatesToolLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleTradingGroupsTookLink = new C1.Win.C1Command.C1CommandLink();
            this.ShortSaleBillingSummaryToolLink = new C1.Win.C1Command.C1CommandLink();
            this.SubstitutionLookupToolLink = new C1.Win.C1Command.C1CommandLink();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.AlertCombo = new System.Windows.Forms.ComboBox();
            this.ProcessStatusDropDown = new C1.Win.C1Input.C1DropDownControl();
            this.AlertTimer = new System.Timers.Timer();
            this.HeartbeatTimer = new System.Timers.Timer();
            this.DeskQuipCombo = new C1.Win.C1List.C1Combo();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
            this.PositionBoxSummaryExpandedCommand = new C1.Win.C1Command.C1Command();
            this.MainSecMasterControl = new Anetics.Medalist.SecMasterControl();
            this.AlertPilot = new Anetics.Medalist.Pilot();
            this.ProcessPilot = new Anetics.Medalist.Pilot();
            this.ServerPilot = new Anetics.Medalist.Pilot();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
            this.StatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessStatusDropDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlertTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeartbeatTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskQuipCombo)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "");
            this.ImageList.Images.SetKeyName(1, "");
            this.ImageList.Images.SetKeyName(2, "");
            this.ImageList.Images.SetKeyName(3, "");
            this.ImageList.Images.SetKeyName(4, "");
            this.ImageList.Images.SetKeyName(5, "");
            this.ImageList.Images.SetKeyName(6, "");
            this.ImageList.Images.SetKeyName(7, "");
            this.ImageList.Images.SetKeyName(8, "");
            this.ImageList.Images.SetKeyName(9, "");
            this.ImageList.Images.SetKeyName(10, "");
            this.ImageList.Images.SetKeyName(11, "");
            this.ImageList.Images.SetKeyName(12, "");
            this.ImageList.Images.SetKeyName(13, "");
            this.ImageList.Images.SetKeyName(14, "");
            this.ImageList.Images.SetKeyName(15, "");
            this.ImageList.Images.SetKeyName(16, "");
            this.ImageList.Images.SetKeyName(17, "");
            this.ImageList.Images.SetKeyName(18, "");
            this.ImageList.Images.SetKeyName(19, "");
            this.ImageList.Images.SetKeyName(20, "");
            this.ImageList.Images.SetKeyName(21, "");
            this.ImageList.Images.SetKeyName(22, "");
            this.ImageList.Images.SetKeyName(23, "");
            this.ImageList.Images.SetKeyName(24, "");
            this.ImageList.Images.SetKeyName(25, "");
            this.ImageList.Images.SetKeyName(26, "");
            this.ImageList.Images.SetKeyName(27, "");
            this.ImageList.Images.SetKeyName(28, "");
            this.ImageList.Images.SetKeyName(29, "");
            this.ImageList.Images.SetKeyName(30, "");
            this.ImageList.Images.SetKeyName(31, "");
            this.ImageList.Images.SetKeyName(32, "");
            this.ImageList.Images.SetKeyName(33, "");
            this.ImageList.Images.SetKeyName(34, "");
            this.ImageList.Images.SetKeyName(35, "");
            this.ImageList.Images.SetKeyName(36, "");
            // 
            // MainMenu
            // 
            this.MainMenu.AccessibleName = "Menu Bar";
            this.MainMenu.CommandHolder = this.CommandHolder;
            this.MainMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.AdminMenuLink,
            this.InventoryMenuLink,
            this.PositionMenuLink,
            this.ShortSaleMenuLink,
            this.SubstitutionMenuLink,
            this.HelpMenuLink});
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainMenu.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1276, 18);
            this.MainMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // CommandHolder
            // 
            this.CommandHolder.Commands.Add(this.AdminCommand);
            this.CommandHolder.Commands.Add(this.AdminHolidaysCommand);
            this.CommandHolder.Commands.Add(this.AdminKeyValuesCommand);
            this.CommandHolder.Commands.Add(this.AdminBooksCommand);
            this.CommandHolder.Commands.Add(this.AdminUserCommand);
            this.CommandHolder.Commands.Add(this.AdminUserPersonnelCommand);
            this.CommandHolder.Commands.Add(this.AdminUserRolesCommand);
            this.CommandHolder.Commands.Add(this.InventoryCommand);
            this.CommandHolder.Commands.Add(this.InventorySubscriberCommand);
            this.CommandHolder.Commands.Add(this.InventoryPublisherCommand);
            this.CommandHolder.Commands.Add(this.InventoryLookupCommand);
            this.CommandHolder.Commands.Add(this.InventoryDeskInputCommand);
            this.CommandHolder.Commands.Add(this.PositionCommand);
            this.CommandHolder.Commands.Add(this.PositionBoxSummaryCommand);
            this.CommandHolder.Commands.Add(this.PositionABRCommand);
            this.CommandHolder.Commands.Add(this.PositionAutoCommand);
            this.CommandHolder.Commands.Add(this.PositionAutoBorrowCommand);
            this.CommandHolder.Commands.Add(this.PositionAutoLoanCommand);
            this.CommandHolder.Commands.Add(this.PositionOpenContractsCommand);
            this.CommandHolder.Commands.Add(this.PositionDealBlotterCommand);
            this.CommandHolder.Commands.Add(this.PositionContractBlotterCommand);
            this.CommandHolder.Commands.Add(this.PositionRecallsCommand);
            this.CommandHolder.Commands.Add(this.PositionBankLoanCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleListsCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleListsEasyBorrowCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleListsHardBorrrowCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleListsNoLendCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleListsThresholdCommand);
            this.CommandHolder.Commands.Add(this.ExternalInventoryCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleLocatesCommand);
            this.CommandHolder.Commands.Add(this.HelpCommand);
            this.CommandHolder.Commands.Add(this.HelpContentsCommand);
            this.CommandHolder.Commands.Add(this.HelpIndexCommand);
            this.CommandHolder.Commands.Add(this.HelpAboutCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleTradingGroupsCommand);
            this.CommandHolder.Commands.Add(this.PositionAccountCommand);
            this.CommandHolder.Commands.Add(this.InventoryRatesCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleBillingCommand);
            this.CommandHolder.Commands.Add(this.CorresspondentValuesCommand);
            this.CommandHolder.Commands.Add(this.CorresspondentBillingCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleBillingSummaryCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleLocatesChargesCommand);
            this.CommandHolder.Commands.Add(this.HelpWebCommand);
            this.CommandHolder.Commands.Add(this.PositionDeficitBuyInsCommand);
            this.CommandHolder.Commands.Add(this.PositionDeficitBuyInsSummaryCommand);
            this.CommandHolder.Commands.Add(this.PositionAccountsBorrowedCommand);
            this.CommandHolder.Commands.Add(this.ShortSaleBillingPositiveRebateCommand);
            this.CommandHolder.Commands.Add(this.PositionBorrowIncomeCommand);
            this.CommandHolder.Commands.Add(this.PositionContractRateComparisonCommand);
            this.CommandHolder.Commands.Add(this.AdminFunctionsCommand);
            this.CommandHolder.Commands.Add(this.SubstitutionCommand);
            this.CommandHolder.Commands.Add(this.SubstitutionInventoryCommand);
            this.CommandHolder.Commands.Add(this.SubstitutionLookupCommand);
            this.CommandHolder.Commands.Add(this.SubstitutionSegEntriesCommand);
            this.CommandHolder.Commands.Add(this.SubstitutionUpdatedDeficitExcessCommand);
            this.CommandHolder.Commands.Add(this.PositionBoxSummaryExpandedCommand);
            this.CommandHolder.ImageList = this.ImageList;
            this.CommandHolder.Owner = this;
            // 
            // AdminCommand
            // 
            this.AdminCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.AdminHolidaysMenuLink,
            this.AdminKeyValuesMenuLink,
            this.AdminBooksMenuLink,
            this.AdminUserMenuLink});
            this.AdminCommand.Name = "AdminCommand";
            this.AdminCommand.Text = "Admin";
            this.AdminCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // AdminHolidaysMenuLink
            // 
            this.AdminHolidaysMenuLink.Command = this.AdminHolidaysCommand;
            // 
            // AdminHolidaysCommand
            // 
            this.AdminHolidaysCommand.Enabled = false;
            this.AdminHolidaysCommand.ImageIndex = 0;
            this.AdminHolidaysCommand.Name = "AdminHolidaysCommand";
            this.AdminHolidaysCommand.Text = "Holidays";
            this.AdminHolidaysCommand.ToolTipText = "Admin - Holidays";
            this.AdminHolidaysCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.AdminHolidaysCommand_Click);
            // 
            // AdminKeyValuesMenuLink
            // 
            this.AdminKeyValuesMenuLink.Command = this.AdminKeyValuesCommand;
            // 
            // AdminKeyValuesCommand
            // 
            this.AdminKeyValuesCommand.Enabled = false;
            this.AdminKeyValuesCommand.ImageIndex = 1;
            this.AdminKeyValuesCommand.Name = "AdminKeyValuesCommand";
            this.AdminKeyValuesCommand.Text = "KeyValues";
            this.AdminKeyValuesCommand.ToolTipText = "Admin - Key Values";
            this.AdminKeyValuesCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.AdminKeyValuesCommand_Click);
            // 
            // AdminBooksMenuLink
            // 
            this.AdminBooksMenuLink.Command = this.AdminBooksCommand;
            // 
            // AdminBooksCommand
            // 
            this.AdminBooksCommand.Enabled = false;
            this.AdminBooksCommand.ImageIndex = 2;
            this.AdminBooksCommand.Name = "AdminBooksCommand";
            this.AdminBooksCommand.Text = "Books";
            this.AdminBooksCommand.ToolTipText = "Admin - Books";
            this.AdminBooksCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.AdminBooksCommand_Click);
            // 
            // AdminUserMenuLink
            // 
            this.AdminUserMenuLink.Command = this.AdminUserCommand;
            // 
            // AdminUserCommand
            // 
            this.AdminUserCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.AdminUserPersonnelMenuLink,
            this.AdminUserRolesMenuLink});
            this.AdminUserCommand.Enabled = false;
            this.AdminUserCommand.ImageIndex = 3;
            this.AdminUserCommand.Name = "AdminUserCommand";
            this.AdminUserCommand.Text = "User";
            this.AdminUserCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // AdminUserPersonnelMenuLink
            // 
            this.AdminUserPersonnelMenuLink.Command = this.AdminUserPersonnelCommand;
            // 
            // AdminUserPersonnelCommand
            // 
            this.AdminUserPersonnelCommand.Enabled = false;
            this.AdminUserPersonnelCommand.ImageIndex = 4;
            this.AdminUserPersonnelCommand.Name = "AdminUserPersonnelCommand";
            this.AdminUserPersonnelCommand.Text = "Personnel";
            this.AdminUserPersonnelCommand.ToolTipText = "Admin - User - Personnel";
            this.AdminUserPersonnelCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.AdminUserPersonnelCommand_Click);
            // 
            // AdminUserRolesMenuLink
            // 
            this.AdminUserRolesMenuLink.Command = this.AdminUserRolesCommand;
            // 
            // AdminUserRolesCommand
            // 
            this.AdminUserRolesCommand.Enabled = false;
            this.AdminUserRolesCommand.ImageIndex = 5;
            this.AdminUserRolesCommand.Name = "AdminUserRolesCommand";
            this.AdminUserRolesCommand.Text = "Roles";
            this.AdminUserRolesCommand.ToolTipText = "Admin - User - Roles";
            this.AdminUserRolesCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.AdminUserRolesCommand_Click);
            // 
            // InventoryCommand
            // 
            this.InventoryCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.InventorySubscriberMenuLink,
            this.InventoryPublisherMenuLink,
            this.InventoryRatesMenuLink,
            this.InventoryLookupMenuLink,
            this.InventoryDeskInputMenuLink});
            this.InventoryCommand.Name = "InventoryCommand";
            this.InventoryCommand.Text = "Inventory";
            this.InventoryCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // InventorySubscriberMenuLink
            // 
            this.InventorySubscriberMenuLink.Command = this.InventorySubscriberCommand;
            // 
            // InventorySubscriberCommand
            // 
            this.InventorySubscriberCommand.Enabled = false;
            this.InventorySubscriberCommand.ImageIndex = 6;
            this.InventorySubscriberCommand.Name = "InventorySubscriberCommand";
            this.InventorySubscriberCommand.Text = "Subscriber";
            this.InventorySubscriberCommand.ToolTipText = "Inventory - Subscriber";
            this.InventorySubscriberCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.InventorySubscriberCommand_Click);
            // 
            // InventoryPublisherMenuLink
            // 
            this.InventoryPublisherMenuLink.Command = this.InventoryPublisherCommand;
            // 
            // InventoryPublisherCommand
            // 
            this.InventoryPublisherCommand.Enabled = false;
            this.InventoryPublisherCommand.ImageIndex = 7;
            this.InventoryPublisherCommand.Name = "InventoryPublisherCommand";
            this.InventoryPublisherCommand.Text = "Publisher";
            this.InventoryPublisherCommand.ToolTipText = "Inventory - Publisher";
            this.InventoryPublisherCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.InventoryPublisherCommand_Click);
            // 
            // InventoryRatesMenuLink
            // 
            this.InventoryRatesMenuLink.AlwaysRecent = true;
            this.InventoryRatesMenuLink.Command = this.InventoryRatesCommand;
            // 
            // InventoryRatesCommand
            // 
            this.InventoryRatesCommand.Enabled = false;
            this.InventoryRatesCommand.ImageIndex = 29;
            this.InventoryRatesCommand.Name = "InventoryRatesCommand";
            this.InventoryRatesCommand.Text = "Rates";
            this.InventoryRatesCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.InventoryRatesCommand_Click);
            // 
            // InventoryLookupMenuLink
            // 
            this.InventoryLookupMenuLink.Command = this.InventoryLookupCommand;
            // 
            // InventoryLookupCommand
            // 
            this.InventoryLookupCommand.Enabled = false;
            this.InventoryLookupCommand.ImageIndex = 8;
            this.InventoryLookupCommand.Name = "InventoryLookupCommand";
            this.InventoryLookupCommand.Text = "Lookup";
            this.InventoryLookupCommand.ToolTipText = "Inventory - Lookup";
            this.InventoryLookupCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.InventoryLookupCommand_Click);
            // 
            // InventoryDeskInputMenuLink
            // 
            this.InventoryDeskInputMenuLink.Command = this.InventoryDeskInputCommand;
            // 
            // InventoryDeskInputCommand
            // 
            this.InventoryDeskInputCommand.Enabled = false;
            this.InventoryDeskInputCommand.ImageIndex = 9;
            this.InventoryDeskInputCommand.Name = "InventoryDeskInputCommand";
            this.InventoryDeskInputCommand.Text = "Desk Input";
            this.InventoryDeskInputCommand.ToolTipText = "Inventory - Desk Input";
            this.InventoryDeskInputCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.InventoryDeskInputCommand_Click);
            // 
            // PositionCommand
            // 
            this.PositionCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.PositionBoxSummaryMenuLink,
            this.c1CommandLink5,
            this.PositionAutoMenuLink,
            this.PositionOpenContractsMenuLink,
            this.PositionContractRateComparisonMenuLink,
            this.PositionDealBlotterMenuLink,
            this.PositionContractBlotterMenuLink,
            this.c1CommandLink3,
            this.PositionRecallsMenuLink,
            this.PositionBankLoanMenuLink,
            this.PositionDeficitBuyInsMenuLink});
            this.PositionCommand.HideNonRecentLinks = false;
            this.PositionCommand.Name = "PositionCommand";
            this.PositionCommand.Text = "Position";
            this.PositionCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // PositionAccountCommand
            // 
            this.PositionAccountCommand.Enabled = false;
            this.PositionAccountCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("PositionAccountCommand.Icon")));
            this.PositionAccountCommand.Name = "PositionAccountCommand";
            this.PositionAccountCommand.Text = "Account";
            this.PositionAccountCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionAccountCommand_Click);
            // 
            // PositionBoxSummaryMenuLink
            // 
            this.PositionBoxSummaryMenuLink.Command = this.PositionBoxSummaryCommand;
            this.PositionBoxSummaryMenuLink.Text = "Box Summary";
            // 
            // PositionBoxSummaryCommand
            // 
            this.PositionBoxSummaryCommand.Enabled = false;
            this.PositionBoxSummaryCommand.ImageIndex = 10;
            this.PositionBoxSummaryCommand.Name = "PositionBoxSummaryCommand";
            this.PositionBoxSummaryCommand.Text = "Box Summary";
            this.PositionBoxSummaryCommand.ToolTipText = "Position - Box Summary";
            this.PositionBoxSummaryCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionBoxSummaryCommand_Click);
            // 
            // PositionAutoMenuLink
            // 
            this.PositionAutoMenuLink.Command = this.PositionAutoCommand;
            this.PositionAutoMenuLink.SortOrder = 2;
            // 
            // PositionAutoCommand
            // 
            this.PositionAutoCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.PositionAutoBorrowMenuLink,
            this.PositionAutoLoanMenuLink});
            this.PositionAutoCommand.Enabled = false;
            this.PositionAutoCommand.HideNonRecentLinks = false;
            this.PositionAutoCommand.ImageIndex = 11;
            this.PositionAutoCommand.Name = "PositionAutoCommand";
            this.PositionAutoCommand.Text = "Auto";
            this.PositionAutoCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // PositionAutoBorrowMenuLink
            // 
            this.PositionAutoBorrowMenuLink.Command = this.PositionAutoBorrowCommand;
            // 
            // PositionAutoBorrowCommand
            // 
            this.PositionAutoBorrowCommand.Enabled = false;
            this.PositionAutoBorrowCommand.ImageIndex = 12;
            this.PositionAutoBorrowCommand.Name = "PositionAutoBorrowCommand";
            this.PositionAutoBorrowCommand.Text = "Borrow";
            this.PositionAutoBorrowCommand.ToolTipText = "Position - Auto - Borrow";
            this.PositionAutoBorrowCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionAutoBorrowCommand_Click);
            // 
            // PositionAutoLoanMenuLink
            // 
            this.PositionAutoLoanMenuLink.Command = this.PositionAutoLoanCommand;
            // 
            // PositionAutoLoanCommand
            // 
            this.PositionAutoLoanCommand.Enabled = false;
            this.PositionAutoLoanCommand.ImageIndex = 13;
            this.PositionAutoLoanCommand.Name = "PositionAutoLoanCommand";
            this.PositionAutoLoanCommand.Text = "Loan";
            this.PositionAutoLoanCommand.ToolTipText = "Position - Auto - Loan";
            this.PositionAutoLoanCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionAutoLoanCommand_Click);
            // 
            // PositionOpenContractsMenuLink
            // 
            this.PositionOpenContractsMenuLink.Command = this.PositionOpenContractsCommand;
            this.PositionOpenContractsMenuLink.SortOrder = 3;
            // 
            // PositionOpenContractsCommand
            // 
            this.PositionOpenContractsCommand.Enabled = false;
            this.PositionOpenContractsCommand.ImageIndex = 14;
            this.PositionOpenContractsCommand.Name = "PositionOpenContractsCommand";
            this.PositionOpenContractsCommand.Text = "Open Contracts";
            this.PositionOpenContractsCommand.ToolTipText = "Position - Open Contracts";
            this.PositionOpenContractsCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionOpenContractsCommand_Click);
            // 
            // PositionContractRateComparisonMenuLink
            // 
            this.PositionContractRateComparisonMenuLink.Command = this.PositionContractRateComparisonCommand;
            this.PositionContractRateComparisonMenuLink.SortOrder = 4;
            // 
            // PositionContractRateComparisonCommand
            // 
            this.PositionContractRateComparisonCommand.Enabled = false;
            this.PositionContractRateComparisonCommand.ImageIndex = 34;
            this.PositionContractRateComparisonCommand.Name = "PositionContractRateComparisonCommand";
            this.PositionContractRateComparisonCommand.Text = "Contract Rate Comparison";
            this.PositionContractRateComparisonCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionContractRateComparisonCommand_Click);
            // 
            // PositionDealBlotterMenuLink
            // 
            this.PositionDealBlotterMenuLink.Command = this.PositionDealBlotterCommand;
            this.PositionDealBlotterMenuLink.SortOrder = 5;
            // 
            // PositionDealBlotterCommand
            // 
            this.PositionDealBlotterCommand.Enabled = false;
            this.PositionDealBlotterCommand.ImageIndex = 15;
            this.PositionDealBlotterCommand.Name = "PositionDealBlotterCommand";
            this.PositionDealBlotterCommand.Text = "Deal Blotter";
            this.PositionDealBlotterCommand.ToolTipText = "Position - Deal Blotter";
            this.PositionDealBlotterCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionDealBlotterCommand_Click);
            // 
            // PositionContractBlotterMenuLink
            // 
            this.PositionContractBlotterMenuLink.Command = this.PositionContractBlotterCommand;
            this.PositionContractBlotterMenuLink.SortOrder = 6;
            this.PositionContractBlotterMenuLink.Text = "Contract Blotter";
            // 
            // PositionContractBlotterCommand
            // 
            this.PositionContractBlotterCommand.Enabled = false;
            this.PositionContractBlotterCommand.ImageIndex = 16;
            this.PositionContractBlotterCommand.Name = "PositionContractBlotterCommand";
            this.PositionContractBlotterCommand.Text = "Contract Blotter";
            this.PositionContractBlotterCommand.ToolTipText = "Position - Contract Blotter";
            this.PositionContractBlotterCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionContractBlotterCommand_Click);
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.Command = this.PositionABRCommand;
            this.c1CommandLink3.SortOrder = 7;
            // 
            // PositionABRCommand
            // 
            this.PositionABRCommand.Enabled = false;
            this.PositionABRCommand.ImageIndex = 10;
            this.PositionABRCommand.Name = "PositionABRCommand";
            this.PositionABRCommand.Text = "ABR";
            this.PositionABRCommand.ToolTipText = "Position - ABR";
            this.PositionABRCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionABRCommand_Click);
            // 
            // PositionRecallsMenuLink
            // 
            this.PositionRecallsMenuLink.Command = this.PositionRecallsCommand;
            this.PositionRecallsMenuLink.SortOrder = 8;
            // 
            // PositionRecallsCommand
            // 
            this.PositionRecallsCommand.Enabled = false;
            this.PositionRecallsCommand.ImageIndex = 17;
            this.PositionRecallsCommand.Name = "PositionRecallsCommand";
            this.PositionRecallsCommand.Text = "Recalls";
            this.PositionRecallsCommand.ToolTipText = "Position - Recalls";
            this.PositionRecallsCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionRecallsCommand_Click);
            // 
            // PositionBankLoanMenuLink
            // 
            this.PositionBankLoanMenuLink.AlwaysRecent = true;
            this.PositionBankLoanMenuLink.Command = this.PositionBankLoanCommand;
            this.PositionBankLoanMenuLink.Delimiter = true;
            this.PositionBankLoanMenuLink.SortOrder = 9;
            // 
            // PositionBankLoanCommand
            // 
            this.PositionBankLoanCommand.Enabled = false;
            this.PositionBankLoanCommand.ImageIndex = 18;
            this.PositionBankLoanCommand.Name = "PositionBankLoanCommand";
            this.PositionBankLoanCommand.Text = "Bank Loan";
            this.PositionBankLoanCommand.ToolTipText = "Position - Bank Loan";
            this.PositionBankLoanCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionBankLoanCommand_Click);
            // 
            // PositionDeficitBuyInsMenuLink
            // 
            this.PositionDeficitBuyInsMenuLink.Command = this.PositionDeficitBuyInsCommand;
            this.PositionDeficitBuyInsMenuLink.SortOrder = 10;
            // 
            // PositionDeficitBuyInsCommand
            // 
            this.PositionDeficitBuyInsCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.PositionAccountsBorrowedMenuItem,
            this.PositionDeficitBuyInsSummaryMenuLink});
            this.PositionDeficitBuyInsCommand.Enabled = false;
            this.PositionDeficitBuyInsCommand.ImageIndex = 30;
            this.PositionDeficitBuyInsCommand.Name = "PositionDeficitBuyInsCommand";
            this.PositionDeficitBuyInsCommand.Text = "Deficit \\ Buy-Ins";
            this.PositionDeficitBuyInsCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // PositionAccountsBorrowedMenuItem
            // 
            this.PositionAccountsBorrowedMenuItem.Command = this.PositionAccountsBorrowedCommand;
            // 
            // PositionAccountsBorrowedCommand
            // 
            this.PositionAccountsBorrowedCommand.Enabled = false;
            this.PositionAccountsBorrowedCommand.ImageIndex = 32;
            this.PositionAccountsBorrowedCommand.Name = "PositionAccountsBorrowedCommand";
            this.PositionAccountsBorrowedCommand.Text = "Accounts Borrowed";
            this.PositionAccountsBorrowedCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionAccountsBorrowedCommand_Click);
            // 
            // PositionDeficitBuyInsSummaryMenuLink
            // 
            this.PositionDeficitBuyInsSummaryMenuLink.Command = this.PositionDeficitBuyInsSummaryCommand;
            // 
            // PositionDeficitBuyInsSummaryCommand
            // 
            this.PositionDeficitBuyInsSummaryCommand.Enabled = false;
            this.PositionDeficitBuyInsSummaryCommand.ImageIndex = 31;
            this.PositionDeficitBuyInsSummaryCommand.Name = "PositionDeficitBuyInsSummaryCommand";
            this.PositionDeficitBuyInsSummaryCommand.Text = "Deficit \\ Buy-Ins Summary";
            // 
            // ShortSaleCommand
            // 
            this.ShortSaleCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ShortSaleBillingMenulink,
            this.ShortSaleListsMenuLink,
            this.ShortSaleTradingGroupsMenuLink,
            this.ShortSaleLocatesMenuLink});
            this.ShortSaleCommand.HideNonRecentLinks = false;
            this.ShortSaleCommand.Name = "ShortSaleCommand";
            this.ShortSaleCommand.Text = "Short Sale";
            this.ShortSaleCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // ShortSaleBillingMenulink
            // 
            this.ShortSaleBillingMenulink.Command = this.ShortSaleBillingCommand;
            // 
            // ShortSaleBillingCommand
            // 
            this.ShortSaleBillingCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.BillingLocateChargesMenuLink,
            this.ShortSaleBillingSummaryMenuLink});
            this.ShortSaleBillingCommand.Enabled = false;
            this.ShortSaleBillingCommand.HideNonRecentLinks = false;
            this.ShortSaleBillingCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ShortSaleBillingCommand.Icon")));
            this.ShortSaleBillingCommand.Name = "ShortSaleBillingCommand";
            this.ShortSaleBillingCommand.Text = "Billing";
            this.ShortSaleBillingCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // BillingLocateChargesMenuLink
            // 
            this.BillingLocateChargesMenuLink.Command = this.ShortSaleLocatesChargesCommand;
            // 
            // ShortSaleLocatesChargesCommand
            // 
            this.ShortSaleLocatesChargesCommand.Enabled = false;
            this.ShortSaleLocatesChargesCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ShortSaleLocatesChargesCommand.Icon")));
            this.ShortSaleLocatesChargesCommand.Name = "ShortSaleLocatesChargesCommand";
            this.ShortSaleLocatesChargesCommand.Text = "Locate Charges";
            // 
            // ShortSaleBillingSummaryMenuLink
            // 
            this.ShortSaleBillingSummaryMenuLink.Command = this.ShortSaleBillingSummaryCommand;
            // 
            // ShortSaleBillingSummaryCommand
            // 
            this.ShortSaleBillingSummaryCommand.Enabled = false;
            this.ShortSaleBillingSummaryCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ShortSaleBillingSummaryCommand.Icon")));
            this.ShortSaleBillingSummaryCommand.Name = "ShortSaleBillingSummaryCommand";
            this.ShortSaleBillingSummaryCommand.Text = "Negative Rebate Billing Summary";
            this.ShortSaleBillingSummaryCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortSaleBillingSummaryCommand_Click);
            // 
            // ShortSaleListsMenuLink
            // 
            this.ShortSaleListsMenuLink.Command = this.ShortSaleListsCommand;
            // 
            // ShortSaleListsCommand
            // 
            this.ShortSaleListsCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ShortSaleListsEasyBorrowMenuLink,
            this.ShortSaleListsHardBorrrowMenuLink,
            this.ShortSaleListsNoLendMenuLink,
            this.ShortSaleListsThresholdMenuLink});
            this.ShortSaleListsCommand.Enabled = false;
            this.ShortSaleListsCommand.ImageIndex = 19;
            this.ShortSaleListsCommand.Name = "ShortSaleListsCommand";
            this.ShortSaleListsCommand.Text = "Lists";
            this.ShortSaleListsCommand.ToolTipText = "Short Sale - Lists";
            this.ShortSaleListsCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // ShortSaleListsEasyBorrowMenuLink
            // 
            this.ShortSaleListsEasyBorrowMenuLink.Command = this.ShortSaleListsEasyBorrowCommand;
            // 
            // ShortSaleListsEasyBorrowCommand
            // 
            this.ShortSaleListsEasyBorrowCommand.Enabled = false;
            this.ShortSaleListsEasyBorrowCommand.ImageIndex = 20;
            this.ShortSaleListsEasyBorrowCommand.Name = "ShortSaleListsEasyBorrowCommand";
            this.ShortSaleListsEasyBorrowCommand.Text = "Easy Borrow";
            this.ShortSaleListsEasyBorrowCommand.ToolTipText = "Short Sale - Lists - Easy Borrow";
            this.ShortSaleListsEasyBorrowCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortSaleListsEasyBorrowCommand_Click);
            // 
            // ShortSaleListsHardBorrrowMenuLink
            // 
            this.ShortSaleListsHardBorrrowMenuLink.Command = this.ShortSaleListsHardBorrrowCommand;
            // 
            // ShortSaleListsHardBorrrowCommand
            // 
            this.ShortSaleListsHardBorrrowCommand.Enabled = false;
            this.ShortSaleListsHardBorrrowCommand.ImageIndex = 21;
            this.ShortSaleListsHardBorrrowCommand.Name = "ShortSaleListsHardBorrrowCommand";
            this.ShortSaleListsHardBorrrowCommand.Text = "Premium Borrow";
            this.ShortSaleListsHardBorrrowCommand.ToolTipText = "Short Sale - Lists - Premium Borrow";
            this.ShortSaleListsHardBorrrowCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortSaleListsHardBorrrowCommand_Click);
            // 
            // ShortSaleListsNoLendMenuLink
            // 
            this.ShortSaleListsNoLendMenuLink.Command = this.ShortSaleListsNoLendCommand;
            // 
            // ShortSaleListsNoLendCommand
            // 
            this.ShortSaleListsNoLendCommand.Enabled = false;
            this.ShortSaleListsNoLendCommand.ImageIndex = 22;
            this.ShortSaleListsNoLendCommand.Name = "ShortSaleListsNoLendCommand";
            this.ShortSaleListsNoLendCommand.Text = "No Lend";
            this.ShortSaleListsNoLendCommand.ToolTipText = "Short Sale - Lists - No Lend";
            this.ShortSaleListsNoLendCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortSaleListsNoLendCommand_Click);
            // 
            // ShortSaleListsThresholdMenuLink
            // 
            this.ShortSaleListsThresholdMenuLink.Command = this.ShortSaleListsThresholdCommand;
            // 
            // ShortSaleListsThresholdCommand
            // 
            this.ShortSaleListsThresholdCommand.Enabled = false;
            this.ShortSaleListsThresholdCommand.ImageIndex = 23;
            this.ShortSaleListsThresholdCommand.Name = "ShortSaleListsThresholdCommand";
            this.ShortSaleListsThresholdCommand.Text = "Threshold";
            this.ShortSaleListsThresholdCommand.ToolTipText = "Short Sale - Lists - Threshold";
            this.ShortSaleListsThresholdCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortSaleListsThresholdCommand_Click);
            // 
            // ShortSaleTradingGroupsMenuLink
            // 
            this.ShortSaleTradingGroupsMenuLink.Command = this.ShortSaleTradingGroupsCommand;
            this.ShortSaleTradingGroupsMenuLink.Text = "Trading Groups";
            // 
            // ShortSaleTradingGroupsCommand
            // 
            this.ShortSaleTradingGroupsCommand.Enabled = false;
            this.ShortSaleTradingGroupsCommand.ImageIndex = 28;
            this.ShortSaleTradingGroupsCommand.Name = "ShortSaleTradingGroupsCommand";
            this.ShortSaleTradingGroupsCommand.Text = "ShortSale - TradingGroups";
            this.ShortSaleTradingGroupsCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortSaleTradingGroupsCommand_Click);
            // 
            // ShortSaleLocatesMenuLink
            // 
            this.ShortSaleLocatesMenuLink.Command = this.ShortSaleLocatesCommand;
            // 
            // ShortSaleLocatesCommand
            // 
            this.ShortSaleLocatesCommand.Enabled = false;
            this.ShortSaleLocatesCommand.ImageIndex = 24;
            this.ShortSaleLocatesCommand.Name = "ShortSaleLocatesCommand";
            this.ShortSaleLocatesCommand.Text = "Locates";
            this.ShortSaleLocatesCommand.ToolTipText = "Short Sale - Locates";
            this.ShortSaleLocatesCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortSaleLocatesCommand_Click);
            // 
            // ExternalInventoryCommand
            // 
            this.ExternalInventoryCommand.Name = "ExternalInventoryCommand";
            // 
            // HelpCommand
            // 
            this.HelpCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.HelpContentsMenuLink,
            this.HelpIndexMenuLink,
            this.HelpWebMenuLink,
            this.HelpAboutMenuLink});
            this.HelpCommand.Name = "HelpCommand";
            this.HelpCommand.Text = "Help";
            this.HelpCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // HelpContentsMenuLink
            // 
            this.HelpContentsMenuLink.Command = this.HelpContentsCommand;
            // 
            // HelpContentsCommand
            // 
            this.HelpContentsCommand.Enabled = false;
            this.HelpContentsCommand.ImageIndex = 25;
            this.HelpContentsCommand.Name = "HelpContentsCommand";
            this.HelpContentsCommand.Text = "Contents";
            this.HelpContentsCommand.ToolTipText = "Help - Contents";
            // 
            // HelpIndexMenuLink
            // 
            this.HelpIndexMenuLink.Command = this.HelpIndexCommand;
            // 
            // HelpIndexCommand
            // 
            this.HelpIndexCommand.Enabled = false;
            this.HelpIndexCommand.ImageIndex = 26;
            this.HelpIndexCommand.Name = "HelpIndexCommand";
            this.HelpIndexCommand.Text = "Index";
            this.HelpIndexCommand.ToolTipText = "Help - Index";
            // 
            // HelpWebMenuLink
            // 
            this.HelpWebMenuLink.Command = this.HelpWebCommand;
            // 
            // HelpWebCommand
            // 
            this.HelpWebCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("HelpWebCommand.Icon")));
            this.HelpWebCommand.Name = "HelpWebCommand";
            this.HelpWebCommand.Text = "Web";
            this.HelpWebCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.HelpWebCommand_Click);
            // 
            // HelpAboutMenuLink
            // 
            this.HelpAboutMenuLink.Command = this.HelpAboutCommand;
            this.HelpAboutMenuLink.Delimiter = true;
            // 
            // HelpAboutCommand
            // 
            this.HelpAboutCommand.Enabled = false;
            this.HelpAboutCommand.ImageIndex = 27;
            this.HelpAboutCommand.Name = "HelpAboutCommand";
            this.HelpAboutCommand.Text = "About";
            this.HelpAboutCommand.ToolTipText = "Help - About";
            // 
            // CorresspondentValuesCommand
            // 
            this.CorresspondentValuesCommand.Name = "CorresspondentValuesCommand";
            this.CorresspondentValuesCommand.Text = "Coresspondent Values";
            // 
            // CorresspondentBillingCommand
            // 
            this.CorresspondentBillingCommand.Name = "CorresspondentBillingCommand";
            this.CorresspondentBillingCommand.Text = "Corresspndent Billing";
            // 
            // ShortSaleBillingPositiveRebateCommand
            // 
            this.ShortSaleBillingPositiveRebateCommand.Name = "ShortSaleBillingPositiveRebateCommand";
            // 
            // PositionBorrowIncomeCommand
            // 
            this.PositionBorrowIncomeCommand.Enabled = false;
            this.PositionBorrowIncomeCommand.ImageIndex = 33;
            this.PositionBorrowIncomeCommand.Name = "PositionBorrowIncomeCommand";
            this.PositionBorrowIncomeCommand.Text = "Borrow Income";
            // 
            // AdminFunctionsCommand
            // 
            this.AdminFunctionsCommand.Name = "AdminFunctionsCommand";
            // 
            // SubstitutionCommand
            // 
            this.SubstitutionCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SubstitutionInventoryMenuLink,
            this.SubstitutionLookupMenuLink,
            this.SubstitutionSegEntriesMenuLink,
            this.c1CommandLink4});
            this.SubstitutionCommand.Name = "SubstitutionCommand";
            this.SubstitutionCommand.Text = "Substitution";
            this.SubstitutionCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // SubstitutionInventoryMenuLink
            // 
            this.SubstitutionInventoryMenuLink.Command = this.SubstitutionInventoryCommand;
            // 
            // SubstitutionInventoryCommand
            // 
            this.SubstitutionInventoryCommand.Enabled = false;
            this.SubstitutionInventoryCommand.Name = "SubstitutionInventoryCommand";
            this.SubstitutionInventoryCommand.Text = "Inventory";
            this.SubstitutionInventoryCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SubstitutionInventoryCommand_Click);
            // 
            // SubstitutionLookupMenuLink
            // 
            this.SubstitutionLookupMenuLink.Command = this.SubstitutionLookupCommand;
            // 
            // SubstitutionLookupCommand
            // 
            this.SubstitutionLookupCommand.Enabled = false;
            this.SubstitutionLookupCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SubstitutionLookupCommand.Icon")));
            this.SubstitutionLookupCommand.Name = "SubstitutionLookupCommand";
            this.SubstitutionLookupCommand.Text = "Lookup";
            this.SubstitutionLookupCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SubstitutionLookupCommand_Click);
            // 
            // SubstitutionSegEntriesMenuLink
            // 
            this.SubstitutionSegEntriesMenuLink.Command = this.SubstitutionSegEntriesCommand;
            // 
            // SubstitutionSegEntriesCommand
            // 
            this.SubstitutionSegEntriesCommand.Enabled = false;
            this.SubstitutionSegEntriesCommand.Name = "SubstitutionSegEntriesCommand";
            this.SubstitutionSegEntriesCommand.Text = "Seg Entries";
            this.SubstitutionSegEntriesCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SubstitutionSegEntriesCommand_Click);
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.SubstitutionUpdatedDeficitExcessCommand;
            // 
            // SubstitutionUpdatedDeficitExcessCommand
            // 
            this.SubstitutionUpdatedDeficitExcessCommand.Enabled = false;
            this.SubstitutionUpdatedDeficitExcessCommand.Name = "SubstitutionUpdatedDeficitExcessCommand";
            this.SubstitutionUpdatedDeficitExcessCommand.Text = "Updated D/E";
            this.SubstitutionUpdatedDeficitExcessCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SubstitutionUpdatedDeficitExcessCommand_Click);
            // 
            // AdminMenuLink
            // 
            this.AdminMenuLink.Command = this.AdminCommand;
            // 
            // InventoryMenuLink
            // 
            this.InventoryMenuLink.Command = this.InventoryCommand;
            // 
            // PositionMenuLink
            // 
            this.PositionMenuLink.Command = this.PositionCommand;
            // 
            // ShortSaleMenuLink
            // 
            this.ShortSaleMenuLink.Command = this.ShortSaleCommand;
            // 
            // SubstitutionMenuLink
            // 
            this.SubstitutionMenuLink.Command = this.SubstitutionCommand;
            // 
            // HelpMenuLink
            // 
            this.HelpMenuLink.Command = this.HelpCommand;
            // 
            // MainToolBar
            // 
            this.MainToolBar.AccessibleName = "Tool Bar";
            this.MainToolBar.AutoSize = false;
            this.MainToolBar.CommandHolder = this.CommandHolder;
            this.MainToolBar.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.AdminKeyValuesToolLink,
            this.AdminBooksToolLink,
            this.InventorySubscriberToolLink,
            this.InventoryRatesToolLink,
            this.InventoryPublisherToolLink,
            this.InventoryAvailabilityToolLink,
            this.InventoryDeskInputToolLink,
            this.PositionBoxSummaryToolLink,
            this.c1CommandLink1,
            this.PositionAutoBorrowToolLink,
            this.PositionAutoLoanToolLink,
            this.PositionOpenContractsToolLink,
            this.PositionContractRateComparisonToolLink,
            this.PositionDealBlotterToolLink,
            this.PositionContractBlotterToolLink,
            this.PositionRecallsToolLink,
            this.PositionBankLoanToolLink,
            this.ShortSaleListsEasyBorrowToolLink,
            this.ShortSaleListsHardBorrowToolLink,
            this.ShortSaleListsNoLendToolLink,
            this.ShortSaleListsThresholdToolLink,
            this.ShortSaleLocatesToolLink,
            this.ShortSaleTradingGroupsTookLink,
            this.ShortSaleBillingSummaryToolLink,
            this.SubstitutionLookupToolLink});
            this.MainToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainToolBar.Location = new System.Drawing.Point(0, 18);
            this.MainToolBar.Movable = false;
            this.MainToolBar.Name = "MainToolBar";
            this.MainToolBar.Size = new System.Drawing.Size(1276, 24);
            this.MainToolBar.Text = "MainToolBar";
            this.MainToolBar.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            this.MainToolBar.VisibleChanged += new System.EventHandler(this.MainToolBar_VisibleChanged);
            // 
            // AdminKeyValuesToolLink
            // 
            this.AdminKeyValuesToolLink.Command = this.AdminKeyValuesCommand;
            // 
            // AdminBooksToolLink
            // 
            this.AdminBooksToolLink.Command = this.AdminBooksCommand;
            this.AdminBooksToolLink.SortOrder = 1;
            // 
            // InventorySubscriberToolLink
            // 
            this.InventorySubscriberToolLink.Command = this.InventorySubscriberCommand;
            this.InventorySubscriberToolLink.SortOrder = 2;
            // 
            // InventoryRatesToolLink
            // 
            this.InventoryRatesToolLink.Command = this.InventoryRatesCommand;
            this.InventoryRatesToolLink.SortOrder = 3;
            // 
            // InventoryPublisherToolLink
            // 
            this.InventoryPublisherToolLink.Command = this.InventoryPublisherCommand;
            this.InventoryPublisherToolLink.SortOrder = 4;
            // 
            // InventoryAvailabilityToolLink
            // 
            this.InventoryAvailabilityToolLink.Command = this.InventoryLookupCommand;
            this.InventoryAvailabilityToolLink.SortOrder = 5;
            // 
            // InventoryDeskInputToolLink
            // 
            this.InventoryDeskInputToolLink.Command = this.InventoryDeskInputCommand;
            this.InventoryDeskInputToolLink.SortOrder = 6;
            // 
            // PositionBoxSummaryToolLink
            // 
            this.PositionBoxSummaryToolLink.Command = this.PositionBoxSummaryCommand;
            this.PositionBoxSummaryToolLink.SortOrder = 7;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.PositionAccountCommand;
            this.c1CommandLink1.SortOrder = 8;
            // 
            // PositionAutoBorrowToolLink
            // 
            this.PositionAutoBorrowToolLink.Command = this.PositionAutoBorrowCommand;
            this.PositionAutoBorrowToolLink.SortOrder = 9;
            // 
            // PositionAutoLoanToolLink
            // 
            this.PositionAutoLoanToolLink.Command = this.PositionAutoLoanCommand;
            this.PositionAutoLoanToolLink.SortOrder = 10;
            // 
            // PositionOpenContractsToolLink
            // 
            this.PositionOpenContractsToolLink.Command = this.PositionOpenContractsCommand;
            this.PositionOpenContractsToolLink.SortOrder = 11;
            // 
            // PositionContractRateComparisonToolLink
            // 
            this.PositionContractRateComparisonToolLink.Command = this.PositionContractRateComparisonCommand;
            this.PositionContractRateComparisonToolLink.SortOrder = 12;
            // 
            // PositionDealBlotterToolLink
            // 
            this.PositionDealBlotterToolLink.Command = this.PositionDealBlotterCommand;
            this.PositionDealBlotterToolLink.SortOrder = 13;
            // 
            // PositionContractBlotterToolLink
            // 
            this.PositionContractBlotterToolLink.Command = this.PositionContractBlotterCommand;
            this.PositionContractBlotterToolLink.SortOrder = 14;
            // 
            // PositionRecallsToolLink
            // 
            this.PositionRecallsToolLink.Command = this.PositionRecallsCommand;
            this.PositionRecallsToolLink.SortOrder = 15;
            // 
            // PositionBankLoanToolLink
            // 
            this.PositionBankLoanToolLink.Command = this.PositionBankLoanCommand;
            this.PositionBankLoanToolLink.SortOrder = 16;
            // 
            // ShortSaleListsEasyBorrowToolLink
            // 
            this.ShortSaleListsEasyBorrowToolLink.Command = this.ShortSaleListsEasyBorrowCommand;
            this.ShortSaleListsEasyBorrowToolLink.SortOrder = 17;
            // 
            // ShortSaleListsHardBorrowToolLink
            // 
            this.ShortSaleListsHardBorrowToolLink.Command = this.ShortSaleListsHardBorrrowCommand;
            this.ShortSaleListsHardBorrowToolLink.SortOrder = 18;
            // 
            // ShortSaleListsNoLendToolLink
            // 
            this.ShortSaleListsNoLendToolLink.Command = this.ShortSaleListsNoLendCommand;
            this.ShortSaleListsNoLendToolLink.SortOrder = 19;
            // 
            // ShortSaleListsThresholdToolLink
            // 
            this.ShortSaleListsThresholdToolLink.Command = this.ShortSaleListsThresholdCommand;
            this.ShortSaleListsThresholdToolLink.SortOrder = 20;
            // 
            // ShortSaleLocatesToolLink
            // 
            this.ShortSaleLocatesToolLink.Command = this.ShortSaleLocatesCommand;
            this.ShortSaleLocatesToolLink.SortOrder = 21;
            // 
            // ShortSaleTradingGroupsTookLink
            // 
            this.ShortSaleTradingGroupsTookLink.Command = this.ShortSaleTradingGroupsCommand;
            this.ShortSaleTradingGroupsTookLink.SortOrder = 22;
            // 
            // ShortSaleBillingSummaryToolLink
            // 
            this.ShortSaleBillingSummaryToolLink.Command = this.ShortSaleBillingSummaryCommand;
            this.ShortSaleBillingSummaryToolLink.SortOrder = 23;
            // 
            // SubstitutionLookupToolLink
            // 
            this.SubstitutionLookupToolLink.Command = this.SubstitutionLookupCommand;
            this.SubstitutionLookupToolLink.SortOrder = 24;
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.AlertCombo);
            this.StatusPanel.Controls.Add(this.ProcessStatusDropDown);
            this.StatusPanel.Controls.Add(this.AlertPilot);
            this.StatusPanel.Controls.Add(this.ProcessPilot);
            this.StatusPanel.Controls.Add(this.ServerPilot);
            this.StatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusPanel.Location = new System.Drawing.Point(0, 861);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(1276, 24);
            this.StatusPanel.TabIndex = 3;
            this.StatusPanel.Resize += new System.EventHandler(this.StatusPanel_Resize);
            // 
            // AlertCombo
            // 
            this.AlertCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlertCombo.DropDownWidth = 600;
            this.AlertCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlertCombo.Location = new System.Drawing.Point(48, 2);
            this.AlertCombo.MaxDropDownItems = 25;
            this.AlertCombo.Name = "AlertCombo";
            this.AlertCombo.Size = new System.Drawing.Size(456, 21);
            this.AlertCombo.TabIndex = 8;
            this.AlertCombo.DropDown += new System.EventHandler(this.AlertCombo_DropDown);
            this.AlertCombo.SelectionChangeCommitted += new System.EventHandler(this.AlertCombo_SelectionChangeCommitted);
            // 
            // ProcessStatusDropDown
            // 
            this.ProcessStatusDropDown.AutoSize = false;
            this.ProcessStatusDropDown.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ProcessStatusDropDown.DropDownFormAlign = C1.Win.C1Input.DropDownFormAlignmentEnum.Right;
            this.ProcessStatusDropDown.DropDownFormClassName = "Anetics.Medalist.ProcessStatusDropdownForm";
            this.ProcessStatusDropDown.Location = new System.Drawing.Point(532, 2);
            this.ProcessStatusDropDown.Name = "ProcessStatusDropDown";
            this.ProcessStatusDropDown.Size = new System.Drawing.Size(702, 21);
            this.ProcessStatusDropDown.TabIndex = 7;
            this.ProcessStatusDropDown.Tag = null;
            this.ProcessStatusDropDown.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.ProcessStatusDropDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProcessStatusDropDown_KeyPress);
            // 
            // AlertTimer
            // 
            this.AlertTimer.Enabled = true;
            this.AlertTimer.Interval = 10000D;
            this.AlertTimer.SynchronizingObject = this;
            this.AlertTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.AlertTimer_Elapsed);
            // 
            // HeartbeatTimer
            // 
            this.HeartbeatTimer.Interval = 20000D;
            this.HeartbeatTimer.SynchronizingObject = this;
            this.HeartbeatTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.HeartbeatTimer_Elapsed);
            // 
            // DeskQuipCombo
            // 
            this.DeskQuipCombo.AddItemSeparator = ';';
            this.DeskQuipCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeskQuipCombo.AutoSize = false;
            this.DeskQuipCombo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeskQuipCombo.Caption = "";
            this.DeskQuipCombo.CaptionHeight = 17;
            this.DeskQuipCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.DeskQuipCombo.ColumnCaptionHeight = 17;
            this.DeskQuipCombo.ColumnFooterHeight = 17;
            this.DeskQuipCombo.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.DeskQuipCombo.ContentHeight = 14;
            this.DeskQuipCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.DeskQuipCombo.EditorBackColor = System.Drawing.SystemColors.ControlLight;
            this.DeskQuipCombo.EditorFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeskQuipCombo.EditorForeColor = System.Drawing.Color.Maroon;
            this.DeskQuipCombo.EditorHeight = 14;
            this.DeskQuipCombo.ExtendRightColumn = true;
            this.DeskQuipCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeskQuipCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("DeskQuipCombo.Images"))));
            this.DeskQuipCombo.ItemHeight = 15;
            this.DeskQuipCombo.Location = new System.Drawing.Point(822, 24);
            this.DeskQuipCombo.MatchEntryTimeout = ((long)(2000));
            this.DeskQuipCombo.MaxDropDownItems = ((short)(25));
            this.DeskQuipCombo.MaxLength = 32767;
            this.DeskQuipCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.DeskQuipCombo.Name = "DeskQuipCombo";
            this.DeskQuipCombo.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.DeskQuipCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.DeskQuipCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.DeskQuipCombo.Size = new System.Drawing.Size(450, 14);
            this.DeskQuipCombo.TabIndex = 56;
            this.DeskQuipCombo.ValueMember = "Text";
            this.DeskQuipCombo.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.DeskQuipCombo_FormatText);
            this.DeskQuipCombo.UnboundColumnFetch += new C1.Win.C1List.UnboundColumnFetchEventHandler(this.DeskQuipCombo_UnboundColumnFetch);
            this.DeskQuipCombo.PropBag = resources.GetString("DeskQuipCombo.PropBag");
            // 
            // c1CommandLink5
            // 
            this.c1CommandLink5.Command = this.PositionBoxSummaryExpandedCommand;
            this.c1CommandLink5.SortOrder = 1;
            // 
            // PositionBoxSummaryExpandedCommand
            // 
            this.PositionBoxSummaryExpandedCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("PositionBoxSummaryExpandedCommand.Icon")));
            this.PositionBoxSummaryExpandedCommand.Name = "PositionBoxSummaryExpandedCommand";
            this.PositionBoxSummaryExpandedCommand.Text = "Box Summary Expanded";
            this.PositionBoxSummaryExpandedCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.PositionBoxSummaryExpandedCommand_Click);
            // 
            // MainSecMasterControl
            // 
            this.MainSecMasterControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainSecMasterControl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainSecMasterControl.Location = new System.Drawing.Point(0, 42);
            this.MainSecMasterControl.Name = "MainSecMasterControl";
            this.MainSecMasterControl.Padding = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.MainSecMasterControl.SecId = "";
            this.MainSecMasterControl.Size = new System.Drawing.Size(282, 819);
            this.MainSecMasterControl.TabIndex = 60;
            // 
            // AlertPilot
            // 
            this.AlertPilot.BackColor = System.Drawing.Color.Gray;
            this.AlertPilot.Location = new System.Drawing.Point(512, 7);
            this.AlertPilot.Name = "AlertPilot";
            this.AlertPilot.Size = new System.Drawing.Size(10, 10);
            this.AlertPilot.State = Anetics.Medalist.PilotState.Idle;
            this.AlertPilot.TabIndex = 6;
            // 
            // ProcessPilot
            // 
            this.ProcessPilot.BackColor = System.Drawing.Color.Gray;
            this.ProcessPilot.Location = new System.Drawing.Point(28, 7);
            this.ProcessPilot.Name = "ProcessPilot";
            this.ProcessPilot.Size = new System.Drawing.Size(10, 10);
            this.ProcessPilot.State = Anetics.Medalist.PilotState.Idle;
            this.ProcessPilot.TabIndex = 4;
            // 
            // ServerPilot
            // 
            this.ServerPilot.BackColor = System.Drawing.Color.Gray;
            this.ServerPilot.Location = new System.Drawing.Point(8, 7);
            this.ServerPilot.Name = "ServerPilot";
            this.ServerPilot.Size = new System.Drawing.Size(10, 10);
            this.ServerPilot.State = Anetics.Medalist.PilotState.Idle;
            this.ServerPilot.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1276, 885);
            this.Controls.Add(this.MainSecMasterControl);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.DeskQuipCombo);
            this.Controls.Add(this.MainToolBar);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "Medalist";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
            this.StatusPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProcessStatusDropDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlertTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeartbeatTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeskQuipCombo)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        [STAThread]
        static void Main()
        {
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MainForm.Main]", Log.Error, 1);
                Log.Write(e.StackTrace + " [MainForm.Main]", 1);
                Log.Write(e.InnerException + " [MainForm.Main]", 1);
            }
        }

        private void AgentConnect()
        {
            if (eventsEnabled)
            {
                try
                {
                    ServiceAgent.HeartbeatEvent -= new HeartbeatEventHandler(heartbeatEventWrapper.DoEvent);
                    ServiceAgent.HeartbeatEvent += new HeartbeatEventHandler(heartbeatEventWrapper.DoEvent);

                    ServiceAgent.DeskQuipEvent -= new DeskQuipEventHandler(deskQuipEventWrapper.DoEvent);
                    ServiceAgent.DeskQuipEvent += new DeskQuipEventHandler(deskQuipEventWrapper.DoEvent);

                    if (ServerPilot.State.Equals(PilotState.Idle))
                    {
                        ServerPilot.State = PilotState.Normal;
                    }
                    else
                    {
                        ServerPilot.State = PilotState.Unknown;
                    }

                    Alert("Connected to the " + applicationName + " main server.");
                }
                catch
                {
                    ServerPilot.State = PilotState.RunFault;
                    ProcessPilot.State = PilotState.RunFault;

                    Alert("The " + applicationName + " main server is unreachable.");
                    Log.Write("The " + applicationName + " main server is unreachable. [MainForm.AgentConnect]", Log.Warning, 1);
                }
            }
        }

        private void AgentDisconnect()
        {
            if (eventsEnabled)
            {
                try
                {
                    ServiceAgent.HeartbeatEvent -= new HeartbeatEventHandler(heartbeatEventWrapper.DoEvent);
                    ServiceAgent.DeskQuipEvent -= new DeskQuipEventHandler(deskQuipEventWrapper.DoEvent);

                    ServerPilot.State = PilotState.Idle;
                    ProcessPilot.State = PilotState.Idle;
                }
                catch
                {
                    ServerPilot.State = PilotState.RunFault;
                    ProcessPilot.State = PilotState.RunFault;

                    Alert("The " + applicationName + " main server is unreachable.");
                    Log.Write("The " + applicationName + " main server is unreachable. [MainForm.AgentDisconnect]", Log.Warning, 1);
                }
            }
        }

        public void HeartbeatOnEvent(HeartbeatEventArgs e)
        {
            switch (e.MainStatus)
            {
                case HeartbeatStatus.Alert:
                    Alert(e.MainAlert, PilotState.Normal);
                    ServerPilot.State = ServerPilot.State;
                    break;
                case HeartbeatStatus.Normal:
                    ServerPilot.State = ServerPilot.State;
                    break;
                case HeartbeatStatus.Stopping:
                    Alert("The " + applicationName + " main server is stopping.");
                    ServerPilot.State = PilotState.Unknown;
                    break;
                case HeartbeatStatus.Unknown:
                    ServerPilot.State = PilotState.RunFault;
                    break;
            }

            switch (e.ProcessStatus)
            {
                case HeartbeatStatus.Alert:
                    Alert(e.ProcessAlert, PilotState.Normal);
                    ProcessPilot.State = ProcessPilot.State;
                    break;
                case HeartbeatStatus.Normal:
                    switch (ProcessPilot.State)
                    {
                        case PilotState.Idle:
                            Alert("Connected to the " + applicationName + " process server.");
                            ProcessPilot.State = PilotState.Normal;
                            break;
                        case PilotState.Normal:
                            ProcessPilot.State = PilotState.Normal;
                            break;
                        case PilotState.Unknown:
                            ProcessPilot.State = PilotState.Unknown;
                            break;
                        case PilotState.RunFault:
                            Alert("Connected to the " + applicationName + " process server.");
                            ProcessPilot.State = PilotState.Unknown;
                            break;
                    }
                    break;
                case HeartbeatStatus.Stopping:
                    Alert("The " + applicationName + " process server is stopping.", PilotState.Normal);
                    ProcessPilot.State = PilotState.Unknown;
                    break;
                case HeartbeatStatus.Unknown:
                    ProcessPilot.State = PilotState.RunFault;
                    break;
            }
        }

        private void HeartbeatTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            HeartbeatTimer.Enabled = false;

            if (ServerPilot.State.Equals(PilotState.Idle) || ServerPilot.State.Equals(PilotState.RunFault))
            {
                AgentConnect();
            }

            if (ProcessPilot.State.Equals(PilotState.RunFault))
            {
                Alert("The " + applicationName + " process server is unreachable.");
            }

            HeartbeatTimer.Enabled = true;

            if (!dateToday.Equals(DateTime.Today)) // Been up past midnight, time for a rest.
            {
                Log.Write("User " + this.UserId + ", sorry... forced close after midnight. [MainForm.HeartbeatTimer_Elapsed]", Log.Information, 1);
                this.Close();
            }
        }

        public void Alert(string alertItem)
        {
            Alert(alertItem, PilotState.Idle);
        }

        public void Alert(string alertItem, PilotState pilotState)
        {
            AlertPilot.State = pilotState;

            while (AlertCombo.Items.Count > ALERT_MAX_ITEM)
            {
                AlertComboRemoveAt(ALERT_MAX_ITEM);
                //AlertCombo.Items.RemoveAt(ALERT_MAX_ITEM);
            }

            if ((AlertCombo.Items.Count > 0) && (AlertCombo.Items[0].ToString().Length == 0))
            {
                AlertComboRemoveAt(0);
                //AlertCombo.Items.RemoveAt(0);
            }

            AlertComboInsert(0, DateTime.Now.ToString("T") + "  " + alertItem.Trim());
            AlertComboSelectedIndex(0);
            AlertComboSelect(0, 0);
            AlertComboRefresh();

            AlertTimer.Enabled = false; // Reset the timer.
            AlertTimer.Enabled = true;
        }

        private void AlertComboRemoveAt(int index)
        {
            if (AlertCombo.InvokeRequired)
            {
                removeAtAlertCombo remove = new removeAtAlertCombo(AlertComboRemoveAt);
                AlertCombo.Invoke(remove, new object[] { index });
            }
            else
            {
                AlertCombo.Items.RemoveAt(index);
            }
        }

        private void AlertComboInsert(int index, object obj)
        {
            if (AlertCombo.InvokeRequired)
            {
                insertIntoAlertCombo insert = new insertIntoAlertCombo(AlertComboInsert);
                AlertCombo.Invoke(insert, new object[] { index, obj });
            }
            else
            {
                AlertCombo.Items.Insert(index, obj);
            }
        }

        private void AlertComboSelectedIndex(int index)
        {
            if (AlertCombo.InvokeRequired)
            {
                selectedIndexAlertCombo selectedIndex = new selectedIndexAlertCombo(AlertComboSelectedIndex);
                AlertCombo.Invoke(selectedIndex, new object[] { index });
            }
            else
            {
                AlertCombo.SelectedIndex = index;
            }

        }

        private void AlertComboSelect(int start, int length)
        {
            if (AlertCombo.InvokeRequired)
            {
                selectAlertCombo select = new selectAlertCombo(AlertComboSelect);
                AlertCombo.Invoke(select, new object[] { start, length });
            }
            else
            {
                AlertCombo.Select(start, length);
            }
        }

        private void AlertComboRefresh()
        {
            if (AlertCombo.InvokeRequired)
            {
                refreshAlertCombo refresh = new refreshAlertCombo(AlertComboRefresh);
                AlertCombo.Invoke(refresh);
            }
            else
            {
                AlertCombo.Refresh();
            }

        }


        private void DeskQuipLoad()
        {
            this.Alert("Please wait... Loading desk quip data...", PilotState.Unknown);

            try
            {
                DeskQuipDataSet = ServiceAgent.DeskQuipGet(UtcOffset);

                DeskQuipDataView = new DataView(DeskQuipDataSet.Tables["DeskQuips"]);
                DeskQuipDataView.Sort = "ActTime Desc";

                DeskQuipCombo.HoldFields();
                DeskQuipCombo.DataSource = DeskQuipDataView;

                if (DeskQuipDataView.Count > 0)
                {
                    DeskQuipCombo.SelectedIndex = 0;
                }

                MainSecMasterControl.DeskQuipInit();

                this.DeskQuipIsReady = true;
                this.Alert("Loading desk quip data... Done!", PilotState.Normal);
            }
            catch (Exception e)
            {
                this.Alert(e.Message, PilotState.RunFault);
                Log.Write(e.Message + " [MainForm.DeskQuipLoad]", Log.Error, 1);
            }
        }

        private bool HasInstance(Type formType)
        {
            if (this.MdiChildren.Length > 0)
            {
                for (int i = 0; i < this.MdiChildren.Length; i++)
                {
                    if (this.MdiChildren[i].Name.Equals(formType.Name))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ActivateForm(Type formType)
        {
            int i = 0;

            if (this.MdiChildren.Length > 0)
            {
                for (i = 0; i < this.MdiChildren.Length; i++)
                {
                    if (this.MdiChildren[i].Name.Equals(formType.Name))
                    {
                        break;
                    }
                }
            }

            if (i < this.MdiChildren.Length) // Found the form.
            {
                switch (this.MdiChildren[i].WindowState)
                {
                    case FormWindowState.Maximized:
                        this.MdiChildren[i].Focus();
                        break;

                    case FormWindowState.Minimized:
                        this.MdiChildren[i].WindowState = FormWindowState.Normal;
                        this.MdiChildren[i].Focus();
                        break;

                    case FormWindowState.Normal:
                        this.MdiChildren[i].Focus();
                        break;
                }
            }
        }

        public string GridFilterGet(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
        {
            int i;
            string quoteMark;
            string filterItem;
            string gridFilter = "";

            foreach (C1.Win.C1TrueDBGrid.C1DataColumn column in grid.Columns)
            {
                if (!column.FilterText.Equals(""))
                {
                    filterItem = "";

                    if ((column.Value.GetType().Equals(typeof(System.String)) && !column.ValueItems.Presentation.Equals(C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox))
                        || column.Value.GetType().Equals(typeof(System.DateTime))
                        || column.Value.GetType().Equals(typeof(System.DBNull))) // Column data type is quoted.
                    {
                        quoteMark = "'";
                    }
                    else
                    {
                        quoteMark = ""; // No quotes.
                    }

                    if ((i = column.FilterText.IndexOf(">=") + 2) > 1) // We have an operator.
                    {
                        if (column.FilterText.Replace(" ", "").Length > i) // We have an argument.
                        {
                            filterItem = column.FilterText.Substring(i, column.FilterText.Length - i).Replace(" ", ""); // The argument.

                            if ((quoteMark.Equals("") && !Tools.IsNumeric(filterItem))) // Column data type not quoted and argument not numeric.
                            {
                                filterItem = "";
                            }
                            else // This should work.
                            {
                                filterItem = "[" + column.DataField + "] >= " + quoteMark + filterItem + quoteMark;
                            }
                        }
                    }
                    else if ((i = column.FilterText.IndexOf("<=") + 2) > 1) // We have an operator.
                    {
                        if (column.FilterText.Replace(" ", "").Length > i) // We have an argument.
                        {
                            filterItem = column.FilterText.Substring(i, column.FilterText.Length - i).Replace(" ", ""); // The argument.

                            if ((quoteMark.Equals("") && !Tools.IsNumeric(filterItem))) // Column data type not quoted and argument not numeric.
                            {
                                filterItem = "";
                            }
                            else // This should work.
                            {
                                filterItem = "[" + column.DataField + "] <= " + quoteMark + filterItem + quoteMark;
                            }
                        }
                    }
                    else if ((i = column.FilterText.IndexOf(">") + 1) > 0) // We have an operator.
                    {
                        if (column.FilterText.Replace(" ", "").Length > i) // We have an argument.
                        {
                            filterItem = column.FilterText.Substring(i, column.FilterText.Length - i).Replace(" ", ""); // The argument.

                            if ((quoteMark.Equals("") && !Tools.IsNumeric(filterItem))) // Column data type not quoted and argument not numeric.
                            {
                                filterItem = "";
                            }
                            else // This should work.
                            {
                                filterItem = "[" + column.DataField + "] > " + quoteMark + filterItem + quoteMark;
                            }
                        }
                    }
                    else if ((i = column.FilterText.IndexOf("<") + 1) > 0) // We have an operator.
                    {
                        if (column.FilterText.Replace(" ", "").Length > i) // We have an argument.
                        {
                            filterItem = column.FilterText.Substring(i, column.FilterText.Length - i).Replace(" ", ""); // The argument.

                            if ((quoteMark.Equals("") && !Tools.IsNumeric(filterItem))) // Column data type not quoted and argument not numeric.
                            {
                                filterItem = "";
                            }
                            else // This should work.
                            {
                                filterItem = "[" + column.DataField + "] < " + quoteMark + filterItem + quoteMark;
                            }
                        }
                    }
                    else if ((i = column.FilterText.IndexOf("!=") + 2) > 1) // We have an operator.
                    {
                        if (column.FilterText.Replace(" ", "").Length > i) // We have an argument.
                        {
                            filterItem = column.FilterText.Substring(i, column.FilterText.Length - i).Replace(" ", ""); // The argument.

                            if ((quoteMark.Equals("") && !Tools.IsNumeric(filterItem))) // Column data type not quoted and argument not numeric.
                            {
                                filterItem = "";
                            }
                            else // This should work.
                            {
                                filterItem = "[" + column.DataField + "] <> " + quoteMark + filterItem + quoteMark;
                            }
                        }
                    }
                    else if ((i = column.FilterText.IndexOf("=") + 1) > 0) // We have an operator.
                    {
                        if (column.FilterText.Replace(" ", "").Length > i) // We have an argument.
                        {
                            filterItem = column.FilterText.Substring(i, column.FilterText.Length - i).Replace(" ", ""); // The argument.

                            if ((quoteMark.Equals("") && !Tools.IsNumeric(filterItem))) // Column data type not quoted and argument not numeric.
                            {
                                filterItem = "";
                            }
                            else // This should work.
                            {
                                filterItem = "[" + column.DataField + "] = " + quoteMark + filterItem + quoteMark;
                            }
                        }
                    }
                    else
                    {
                        if (column.FilterText.Replace(" ", "").Length > 0) // We have an argument.
                        {
                            filterItem = column.FilterText.Replace(" ", ""); // The argument.

                            if (quoteMark.Equals("")) // Not quoted.
                            {
                                if (filterItem.ToLower().Equals("true"))
                                {
                                    filterItem = "[" + column.DataField + "] = 1";
                                }
                                else if (filterItem.ToLower().Equals("false"))
                                {
                                    filterItem = "[" + column.DataField + "] = 0";
                                }
                                else
                                {
                                    filterItem = "";
                                }
                            }
                            else // This should work.
                            {
                                filterItem = "[" + column.DataField + "] Like " + quoteMark + filterItem + "*" + quoteMark;
                            }
                        }
                    }

                    if (gridFilter.Equals("") && !filterItem.Equals(""))
                    {
                        gridFilter = filterItem;
                    }
                    else if (!filterItem.Equals(""))
                    {
                        gridFilter += " AND " + filterItem;
                    }
                }
            }

            return gridFilter;
        }

        public void GridFilterClear(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
        {
            foreach (C1.Win.C1TrueDBGrid.C1DataColumn column in grid.Columns)
            {
                if (!column.FilterText.Equals(""))
                {
                    column.FilterText = "";
                }
            }
        }

        public void GridFooterClear(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
        {
            foreach (C1.Win.C1TrueDBGrid.C1DataColumn column in grid.Columns)
            {
                if (!column.FooterText.Equals(""))
                {
                    column.FooterText = "";
                }
            }
        }

        private void AlertTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            AlertTimer.Enabled = false;
            AlertCombo.Items.Insert(0, "");
            AlertCombo.SelectedIndex = 0;

            AlertPilotStateSet(PilotState.Idle);
        }

        private void AlertPilotStateSet(PilotState state)
        {
            if (AlertPilot.InvokeRequired)
            {
                updatePilotState del = new updatePilotState(AlertPilotStateSet);
                AlertPilot.Invoke(del, new object[] { state });
            }
            else
            {
                AlertPilot.State = state;
            }
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            AgentDisconnect();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "700"));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "950"));


            this.Text += " " + Application.ProductVersion;
            MainSecMasterControl.MainForm = this;

            this.Show();
            Application.DoEvents();

            try
            {
                HeartbeatTimer.Interval = double.Parse(Standard.ConfigValue("HeartbeatInterval", "20000"));
                bwIdle.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwIdle_RunWorkerCompleted);
            }
            catch (Exception ee)
            {
                Log.Write(ee.Message + " [MainForm.MainForm_Load]", Log.Error, 1);
            }

            ServerPilot.HeartbeatInterval = HeartbeatTimer.Interval;
            ProcessPilot.HeartbeatInterval = HeartbeatTimer.Interval;

            Log.Write("Running with heartbeat interval of " +
                HeartbeatTimer.Interval + " milliseconds. [MainForm.MainForm_Load]", Log.Information, 1);

            Alert("Connecting to the " + applicationName + " server...");

            AdminAgent = (IAdmin)RemotingTools.ObjectGet(typeof(IAdmin));
            if (AdminAgent == null)
            {
                Alert("Remoting config values for the admin agent are not correct.", PilotState.RunFault);
                Log.Write("Remoting config values for the admin agent (IAdmin) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
            }

            ServiceAgent = (IService)RemotingTools.ObjectGet(typeof(IService));
            if (ServiceAgent == null)
            {
                Alert("Remoting config values for the service agent are not correct.", PilotState.RunFault);
                Log.Write("Remoting config values for the service agent (IService) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
            }

            ShortSaleAgent = (IShortSale)RemotingTools.ObjectGet(typeof(IShortSale));
            if (ShortSaleAgent == null)
            {
                Alert("Remoting config values for the short-sale agent are not correct.", PilotState.RunFault);
                Log.Write("Remoting config values for the short-sale agent (IShortSale) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
            }

            PositionAgent = (IPosition)RemotingTools.ObjectGet(typeof(IPosition));
            if (PositionAgent == null)
            {
                Alert("Remoting config values for the position agent are not correct.", PilotState.RunFault);
                Log.Write("Remoting config values for the position agent (IPosition) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
            }

            SubstitutionAgent = (ISubstitution)RemotingTools.ObjectGet(typeof(ISubstitution));
            if (SubstitutionAgent == null)
            {
                Alert("Remoting config values for the substitution agent are not correct.", PilotState.RunFault);
                Log.Write("Remoting config values for the substitution agent (ISubstitution) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
            }

            RebateAgent = (IRebate)RemotingTools.ObjectGet(typeof(IRebate));
            if (RebateAgent == null)
            {
                Alert("Remoting config values for the rebate agent are not correct.", PilotState.RunFault);
                Log.Write("Remoting config values for the rebate agent (IRebate) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
            }



            if ((AdminAgent != null) &&
                    (ServiceAgent != null) &&
                    (ShortSaleAgent != null) &&
                    (PositionAgent != null) &&
                    (RebateAgent != null) &&
                    (SubstitutionAgent != null))
            {
                try
                {
                    AdminHolidaysCommand.Enabled = AdminAgent.MayView(UserId, "AdminHolidays");
                    AdminFunctionsCommand.Enabled = AdminAgent.MayView(UserId, "AdminFunctions");
                    AdminKeyValuesCommand.Enabled = AdminAgent.MayView(UserId, "AdminKeyValues");
                    AdminBooksCommand.Enabled = AdminAgent.MayView(UserId, "AdminBooks");
                    AdminUserCommand.Enabled = AdminAgent.MayView(UserId, "AdminUser");
                    AdminUserPersonnelCommand.Enabled = AdminUserCommand.Enabled;
                    AdminUserRolesCommand.Enabled = AdminUserCommand.Enabled;

                    InventorySubscriberCommand.Enabled = AdminAgent.MayView(UserId, "InventorySubscriber");
                    InventoryPublisherCommand.Enabled = AdminAgent.MayView(UserId, "InventoryPublisher");
                    InventoryDeskInputCommand.Enabled = InventorySubscriberCommand.Enabled;
                    InventoryLookupCommand.Enabled = InventorySubscriberCommand.Enabled;
                    InventoryRatesCommand.Enabled = InventorySubscriberCommand.Enabled;


                    PositionAccountCommand.Enabled = AdminAgent.MayView(UserId, "PositionAccounts");
                    PositionBoxSummaryCommand.Enabled = AdminAgent.MayView(UserId, "PositionBoxSummary");
                    PositionAutoCommand.Enabled = AdminAgent.MayView(UserId, "PositionAuto");
                    PositionAutoBorrowCommand.Enabled = PositionAutoCommand.Enabled;
                    PositionAutoLoanCommand.Enabled = PositionAutoCommand.Enabled;
                    PositionContractBlotterCommand.Enabled = AdminAgent.MayView(UserId, "PositionContractBlotter");
                    PositionDealBlotterCommand.Enabled = AdminAgent.MayView(UserId, "PositionDealBlotter");
                    PositionOpenContractsCommand.Enabled = AdminAgent.MayView(UserId, "PositionOpenContracts");
                    PositionContractRateComparisonCommand.Enabled = AdminAgent.MayView(UserId, "PositionContractRateComparison");
                    PositionRecallsCommand.Enabled = AdminAgent.MayView(UserId, "PositionRecalls");
                    PositionBankLoanCommand.Enabled = AdminAgent.MayView(UserId, "PositionBankLoan");
                    PositionDeficitBuyInsCommand.Enabled = AdminAgent.MayView(UserId, "PositionAccountsBorrowed");
                    PositionAccountsBorrowedCommand.Enabled = AdminAgent.MayView(UserId, "PositionAccountsBorrowed");
                    PositionABRCommand.Enabled = AdminAgent.MayView(UserId, "PositionOpenContracts");


                    SubstitutionInventoryCommand.Enabled = AdminAgent.MayView(UserId, "Substitutions");
                    SubstitutionLookupCommand.Enabled = AdminAgent.MayView(UserId, "Substitutions");
                    SubstitutionSegEntriesCommand.Enabled = AdminAgent.MayView(UserId, "Substitutions");
                    SubstitutionUpdatedDeficitExcessCommand.Enabled = AdminAgent.MayView(UserId, "Substitutions");

                    ShortSaleListsCommand.Enabled = AdminAgent.MayView(UserId, "ShortSaleLists");
                    ShortSaleBillingCommand.Enabled = AdminAgent.MayView(UserId, "ShortSaleBilling");
                    ShortSaleBillingSummaryCommand.Enabled = AdminAgent.MayView(UserId, "ShortSaleBilling");
                    ShortSaleBillingPositiveRebateCommand.Enabled = AdminAgent.MayView(UserId, "ShortSaleBilling");
                    ShortSaleTradingGroupsCommand.Enabled = ShortSaleListsCommand.Enabled;
                    ShortSaleListsEasyBorrowCommand.Enabled = ShortSaleListsCommand.Enabled;
                    ShortSaleListsHardBorrrowCommand.Enabled = ShortSaleListsCommand.Enabled;
                    ShortSaleListsNoLendCommand.Enabled = ShortSaleListsCommand.Enabled;
                    ShortSaleListsThresholdCommand.Enabled = ShortSaleListsCommand.Enabled;
                    ShortSaleLocatesCommand.Enabled = AdminAgent.MayView(UserId, "ShortSaleLocates");

                    AgentConnect();
                    DeskQuipLoad();

                    if (PositionBoxSummaryCommand.Enabled) // User may view box position data.
                    {
                        MainSecMasterControl.BookGroupsInit();
                    }
                    else
                    {
                        MainSecMasterControl.WithBox = false;
                        MainSecMasterControl.Height = 84;
                    }

                    if (PositionABRCommand.Enabled)
                    {
                        MainSecMasterControl.BookGroupsInit();
                    }
                    else
                    {
                        MainSecMasterControl.WithBox = false;
                        MainSecMasterControl.Height = 84;
                    }

                    MainSecMasterControl.WithDeskQuips = !eventsEnabled; // If no events, must get desk quips with each security lookup.
                    MainSecMasterControl.Visible = true;

                    DataRow[] dataRows = AdminAgent.UserRolesGet(0).Tables["Users"].Select("UserId = '" + this.UserId + "'");

                    if (dataRows.Length > 0)
                    {
                        userShortName = dataRows[0]["ShortName"].ToString();
                        userEmailAddress = dataRows[0]["Email"].ToString();
                    }
                    else
                    {
                        userShortName = "me";
                    }

                    HeartbeatTimer.Enabled = true;
                }
                catch (Exception ee)
                {
                    Alert("Error: " + ee.Message, PilotState.RunFault);
                    Log.Write(ee.Message + " [MainForm.MainForm_Load]", Log.Error, 1);
                }
            }
            else
            {
                Alert("Sorry, problem connecting to the " + applicationName + " server.", PilotState.RunFault);
            }
        }

        void bwIdle_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.AlertPilot.State = PilotState.Idle;
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            DeskQuipCombo.Width = this.Width - DeskQuipCombo.Left - 50;
            DeskQuipCombo.DropDownWidth = this.Width - DeskQuipCombo.Left;
        }

        private void StatusPanel_Resize(object sender, System.EventArgs e)
        {
            AlertCombo.Width = StatusPanel.Width - (this.Width / 2) - AlertCombo.Left - 15;
            AlertPilot.Left = AlertCombo.Left + AlertCombo.Width + 8;
            ProcessStatusDropDown.Width = AlertCombo.Width + 58;
            ProcessStatusDropDown.Left = AlertPilot.Left + 18;
        }

        private void ProcessStatusDropDown_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void AlertCombo_DropDown(object sender, System.EventArgs e)
        {
            AlertCombo.DropDownWidth = this.ClientSize.Width - AlertCombo.Left - 20;
        }

        private void AlertCombo_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            AlertCombo.SelectedIndex = 0;
        }

        private void AdminHolidaysCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(AdminHolidaysForm)))
            {
                AdminHolidaysForm adminHolidaysForm = new AdminHolidaysForm(this);
                adminHolidaysForm.MdiParent = this;
                adminHolidaysForm.Show();
            }
            else
            {
                ActivateForm(typeof(AdminHolidaysForm));
            }
        }

        private void AdminKeyValuesCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(AdminKeyValuesForm)))
            {
                AdminKeyValuesForm adminKeyValuesForm = new AdminKeyValuesForm(this);
                adminKeyValuesForm.MdiParent = this;
                adminKeyValuesForm.Show();
            }
            else
            {
                ActivateForm(typeof(AdminKeyValuesForm));
            }
        }

        private void AdminBooksCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(AdminBooksForm)))
            {
                AdminBooksForm adminBooksForm = new AdminBooksForm(this);
                adminBooksForm.MdiParent = this;
                adminBooksForm.Show();
            }
            else
            {
                ActivateForm(typeof(AdminBooksForm));
            }
        }

        private void AdminUserPersonnelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(AdminUserPersonnelForm)))
            {
                AdminUserPersonnelForm adminUserPersonnelForm = new AdminUserPersonnelForm(this);
                adminUserPersonnelForm.MdiParent = this;
                adminUserPersonnelForm.Show();
            }
            else
            {
                ActivateForm(typeof(AdminUserPersonnelForm));
            }
        }

        private void AdminUserRolesCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(AdminUserRolesForm)))
            {
                AdminUserRolesForm adminUserRolesForm = new AdminUserRolesForm(this);
                adminUserRolesForm.MdiParent = this;
                adminUserRolesForm.Show();
            }
            else
            {
                ActivateForm(typeof(AdminUserRolesForm));
            }
        }

        private void InventoryLookupCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (inventoryLookupForm == null)
            {
                inventoryLookupForm = new InventoryLookupForm(this);
                inventoryLookupForm.MdiParent = this;
                inventoryLookupForm.Show();
                inventoryLookupForm.Activate();
            }
            else
            {
                inventoryLookupForm.Activate();
            }
        }

        private void InventoryDeskInputCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(InventoryDeskInputForm)))
            {
                InventoryDeskInputForm inventoryDeskSourceForm = new InventoryDeskInputForm(this);
                inventoryDeskSourceForm.MdiParent = this;
                inventoryDeskSourceForm.Show();
            }
            else
            {
                ActivateForm(typeof(InventoryDeskInputForm));
            }
        }

        private void InventorySubscriberCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(InventorySubscriberForm)))
            {
                InventorySubscriberForm inventorySubscriberForm = new InventorySubscriberForm(this);
                inventorySubscriberForm.MdiParent = this;
                inventorySubscriberForm.Show();
            }
            else
            {
                ActivateForm(typeof(InventorySubscriberForm));
            }
        }

        private void InventoryPublisherCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(InventoryPublisherForm)))
            {
                InventoryPublisherForm inventoryPublisherForm = new InventoryPublisherForm(this);
                inventoryPublisherForm.MdiParent = this;
                inventoryPublisherForm.Show();
            }
            else
            {
                ActivateForm(typeof(InventoryPublisherForm));
            }
        }

        private void PositionBoxSummaryCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (positionBoxSummaryForm == null)
            {
                positionBoxSummaryForm = new PositionBoxSummaryForm(this);
                positionBoxSummaryForm.MdiParent = this;
                positionBoxSummaryForm.Show();
                positionBoxSummaryForm.Activate();
            }
            else
            {
                positionBoxSummaryForm.Activate();
            }
        }

        private void PositionABRCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (positionABRForm == null)
            {
                positionABRForm = new PositionABRForm(this);
                positionABRForm.MdiParent = this;
                positionABRForm.Show();
                positionABRForm.Activate();
            }
            else
            {
                positionABRForm.Show();
                positionABRForm.Activate();
            }
        }

        private void PositionAutoBorrowCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
        }

        private void PositionAutoLoanCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {

        }

        private void PositionDealBlotterCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (positionDealBlotterForm == null)
            {
                positionDealBlotterForm = new PositionDealBlotterForm(this);
                positionDealBlotterForm.MdiParent = this;
                positionDealBlotterForm.Show();
                positionDealBlotterForm.Activate();
            }
            else
            {
                positionDealBlotterForm.Activate();
            }
        }

        private void PositionContractBlotterCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (positionContractBlotterForm == null)
            {
                positionContractBlotterForm = new PositionContractBlotterForm(this);
                positionContractBlotterForm.MdiParent = this;
                positionContractBlotterForm.Show();
                positionContractBlotterForm.Activate();
            }
            else
            {
                positionContractBlotterForm.Activate();
            }
        }

        private void PositionOpenContractsCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (positionOpenContractsForm == null)
            {
                positionOpenContractsForm = new PositionOpenContractsForm(this);
                positionOpenContractsForm.MdiParent = this;
                positionOpenContractsForm.Show();
                positionOpenContractsForm.Activate();
            }
            else
            {
                positionOpenContractsForm.Activate();
            }
        }

        private void PositionRecallsCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (positionRecallsForm == null)
            {
                positionRecallsForm = new PositionRecallsForm(this);
                positionRecallsForm.MdiParent = this;
                positionRecallsForm.Show();
                positionRecallsForm.Activate();
            }
            else
            {
                positionRecallsForm.Activate();
            }
        }

        private void PositionBankLoanCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (positionBankLoanForm == null)
            {
                positionBankLoanForm = new PositionBankLoanForm(this);
                positionBankLoanForm.MdiParent = this;
                positionBankLoanForm.Show();
                positionBankLoanForm.Activate();
            }
            else
            {
                positionBankLoanForm.Activate();
            }
        }

        private void ShortSaleListsEasyBorrowCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(ShortSaleListsEasyBorrowForm)))
            {
                ShortSaleListsEasyBorrowForm shortSaleListsEasyBorrowForm = new ShortSaleListsEasyBorrowForm(this);
                shortSaleListsEasyBorrowForm.MdiParent = this;
                shortSaleListsEasyBorrowForm.Show();
            }
            else
            {
                ActivateForm(typeof(ShortSaleListsEasyBorrowForm));
            }
        }

        private void ShortSaleListsHardBorrrowCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(ShortSaleListsHardBorrowForm)))
            {
                ShortSaleListsHardBorrowForm shortSaleListsHardBorrowForm = new ShortSaleListsHardBorrowForm(this);
                shortSaleListsHardBorrowForm.MdiParent = this;
                shortSaleListsHardBorrowForm.Show();
            }
            else
            {
                ActivateForm(typeof(ShortSaleListsHardBorrowForm));
            }
        }

        private void ShortSaleListsNoLendCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(ShortSaleListsNoLendForm)))
            {
                ShortSaleListsNoLendForm shortSaleListsNoLendForm = new ShortSaleListsNoLendForm(this);
                shortSaleListsNoLendForm.MdiParent = this;
                shortSaleListsNoLendForm.Show();
            }
            else
            {
                ActivateForm(typeof(ShortSaleListsNoLendForm));
            }
        }

        private void ShortSaleListsThresholdCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(ShortSaleListsThresholdForm)))
            {
                ShortSaleListsThresholdForm shortSaleListsThresholdForm = new ShortSaleListsThresholdForm(this);
                shortSaleListsThresholdForm.MdiParent = this;
                shortSaleListsThresholdForm.Show();
            }
            else
            {
                ActivateForm(typeof(ShortSaleListsThresholdForm));
            }
        }



        private void ShortSaleLocatesCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (shortSaleLocateForm == null)
            {
                shortSaleLocateForm = new ShortSaleLocateForm(this);
                shortSaleLocateForm.Show();
            }
            else
            {
                shortSaleLocateForm.Activate();
            }
        }

        private void ShortSaleTradingGroupsCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(ShortSaleTradingGroupsForm)))
            {
                ShortSaleTradingGroupsForm shortSaleTradingGroupsForm = new ShortSaleTradingGroupsForm(this);
                shortSaleTradingGroupsForm.MdiParent = this;
                shortSaleTradingGroupsForm.Show();
            }
            else
            {
                ActivateForm(typeof(ShortSaleTradingGroupsForm));
            }
        }


        private void DeskQuipCombo_UnboundColumnFetch(object sender, C1.Win.C1List.UnboundColumnFetchEventArgs e)
        {
            switch (DeskQuipCombo.Columns[e.Col].Caption)
            {
                case ("Text"):
                    e.Value = Tools.FormatDate(DeskQuipCombo.Columns["ActTime"].Text, Standard.DateTimeFileFormat) + " [" +
                        DeskQuipCombo.Columns["ActUserShortName"].Text + "] [" +
                        DeskQuipCombo.Columns["SecId"].Text + "|" +
                        DeskQuipCombo.Columns["Symbol"].Text + "]  " +
                        DeskQuipCombo.Columns["DeskQuip"].Text;

                    break;
            }
        }

        private void DeskQuipCombo_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0) // Nothing to format.
            {
                return;
            }

            switch (DeskQuipCombo.Columns[e.ColIndex].DataField)
            {
                case ("ActTime"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeFileFormat);
                    }
                    catch (FormatException ee)
                    {
                        Log.Write(ee.Message + " [MainForm.DeskQuipCombo_FormatText]", Log.Error, 1);
                    }

                    break;
            }
        }

        public string UserId
        {
            get
            {
                if (Standard.ConfigValue("UseDomainInUserId", @"true").ToLower().Equals("true"))
                {
                    return SystemInformation.UserDomainName.ToUpper() + @"\" + SystemInformation.UserName.ToLower();
                }
                else
                {
                    return SystemInformation.UserName.ToLower();
                }
            }
        }

        public string UserEmailAddress
        {
            get
            {
                return userEmailAddress;
            }
        }

        public string UserShortName
        {
            get
            {
                return userShortName;
            }
        }

        public short UtcOffset
        {
            get
            {
                TimeSpan timeSpan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                return (short)(timeSpan.Hours * 60);
            }
        }

        public bool DeskQuipIsReady
        {
            get
            {
                return deskQuipIsReady;
            }

            set
            {
                DeskQuipEventArgs deskQuipEventArgs;

                try
                {
                    lock (this)
                    {
                        if (value && (deskQuipEventArgsArray.Count > 0))
                        {
                            deskQuipIsReady = false;

                            deskQuipEventArgs = (DeskQuipEventArgs)deskQuipEventArgsArray[0];
                            deskQuipEventArgsArray.RemoveAt(0);

                            deskQuipEventHandler.BeginInvoke(deskQuipEventArgs, null, null);
                        }
                        else
                        {
                            deskQuipIsReady = value;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [MainForm.IsReady(set)]", Log.Error, 1);
                }
            }
        }

        public void DeskQuipOnEvent(DeskQuipEventArgs deskQuipEventArgs)
        {
            lock (this)
            {
                deskQuipEventArgsArray.Add(deskQuipEventArgs);

                if (this.DeskQuipIsReady) // Force reset to trigger handling of event.
                {
                    this.DeskQuipIsReady = true;
                }
            }
        }

        private void DeskQuipDoEvent(DeskQuipEventArgs deskQuipEventArgs)
        {
            try
            {
                lock (this)
                {
                    DeskQuipDataSet.Tables["DeskQuips"].BeginLoadData();

                    deskQuipEventArgs.UtcOffset = this.UtcOffset;
                    DeskQuipDataSet.Tables["DeskQuips"].LoadDataRow(deskQuipEventArgs.Values, true);

                    DeskQuipDataSet.Tables["DeskQuips"].EndLoadData();
                    DeskQuipCombo.SelectedIndex = 0;

                    if (deskQuipEventArgs.SecId.Equals(MainSecMasterControl.SecId))
                    {
                        MainSecMasterControl.DeskQuipCombo.SelectedIndex = 0;
                    }

                    this.DeskQuipIsReady = true;
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MainForm.DeskQuipDoEvent]", Log.Error, 1);
            }
        }

        private void MainToolBar_VisibleChanged(object sender, System.EventArgs e)
        {
            if (!this.Visible)
            {
                this.AutoScrollPosition = new Point(0, 0);
            }
        }

        private void PositionAccountCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(PositionAccountsForm)))
            {
                PositionAccountsForm positionAccountsForm = new PositionAccountsForm(this);
                positionAccountsForm.MdiParent = this;
                positionAccountsForm.Show();
            }
            else
            {
                ActivateForm(typeof(PositionAccountsForm));
            }
        }

        private void InventoryRatesCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(InventoryRatesForm)))
            {
                InventoryRatesForm inventoryRatesForm = new InventoryRatesForm(this);
                inventoryRatesForm.MdiParent = this;
                inventoryRatesForm.Show();
            }
            else
            {
                ActivateForm(typeof(InventoryRatesForm));
            }
        }

        private void HelpWebCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("IExplore.exe", "http://pi.penson.com/sites/PFSI/IS/Sendero/default.aspx");
            }
            catch (Exception error)
            {
                this.Alert(error.Message, PilotState.RunFault);
            }
        }

        private void ShortSaleBillingSummaryCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(ShortSaleBillingForm)))
            {
                ShortSaleBillingForm shortSaleBillingForm = new ShortSaleBillingForm(this);
                shortSaleBillingForm.MdiParent = this;
                shortSaleBillingForm.Show();
            }
            else
            {
                ActivateForm(typeof(ShortSaleBillingForm));
            }
        }

        private void PositionAccountsBorrowedCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(PositionDeficitBuyInsAccountsBorrowedForm)))
            {
                PositionDeficitBuyInsAccountsBorrowedForm positionDeficitBuyInsAccountsBorrowedForm = new PositionDeficitBuyInsAccountsBorrowedForm(this);
                positionDeficitBuyInsAccountsBorrowedForm.MdiParent = this;
                positionDeficitBuyInsAccountsBorrowedForm.Show();
            }
            else
            {
                ActivateForm(typeof(PositionDeficitBuyInsAccountsBorrowedForm));
            }
        }

        private void PositionContractRateComparisonCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(PositionContractRateComparisonForm)))
            {
                PositionContractRateComparisonForm positionContractRateComparisonForm = new PositionContractRateComparisonForm(this);
                positionContractRateComparisonForm.MdiParent = this;
                positionContractRateComparisonForm.Show();
            }
            else
            {
                ActivateForm(typeof(PositionContractRateComparisonForm));
            }
        }

        private void SubstitutionLookupCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(SubstitutionInputForm)))
            {
                substitutionInputForm = new SubstitutionInputForm(this);
                substitutionInputForm.MdiParent = this;
                substitutionInputForm.Show();
            }
            else
            {
                ActivateForm(typeof(SubstitutionInputForm));
            }
        }

        private void SubstitutionInventoryCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(SubstitutionInventoryForm)))
            {
                substitutionInventoryForm = new SubstitutionInventoryForm(this);
                substitutionInventoryForm.MdiParent = this;
                substitutionInventoryForm.Show();
            }
            else
            {
                ActivateForm(typeof(SubstitutionInventoryForm));
            }
        }

        private void SubstitutionSegEntriesCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(SubstitutionSegEntriesForm)))
            {
                SubstitutionSegEntriesForm segEntriesForm = new SubstitutionSegEntriesForm(this);
                segEntriesForm.MdiParent = this;
                segEntriesForm.Show();
            }
            else
            {
                ActivateForm(typeof(SubstitutionSegEntriesForm));
            }
        }

        private void SubstitutionUpdatedDeficitExcessCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(SubstitutionDeficitExcessForm)))
            {
                SubstitutionDeficitExcessForm substitutionDeficitExcessForm = new SubstitutionDeficitExcessForm(this);
                substitutionDeficitExcessForm.MdiParent = this;
                substitutionDeficitExcessForm.Show();
            }
            else
            {
                ActivateForm(typeof(SubstitutionDeficitExcessForm));
            }
        }

        public string SecId
        {
            set
            {
                if (!MainSecMasterControl.SecId.Equals(value))
                {
                    MainSecMasterControl.SecId = value;

                    if (inventoryLookupForm != null)
                    {
                        inventoryLookupForm.SecId = MainSecMasterControl.SecId;
                    }

                    if (positionOpenContractsForm != null)
                    {
                        positionOpenContractsForm.SecId = MainSecMasterControl.SecId;
                    }
                }
            }

            get
            {
                return MainSecMasterControl.SecId;
            }
        }

        public string Symbol
        {
            get
            {
                return MainSecMasterControl.Symbol;
            }
        }

        public string Description
        {
            get
            {
                return MainSecMasterControl.Description;
            }
        }

        public string Price
        {
            get
            {
                return MainSecMasterControl.Price;
            }
        }

        public bool IsBond
        {
            get
            {
                return MainSecMasterControl.IsBond;
            }
        }

        public long RecallQuantity
        {
            set
            {
                recallQuantity = value;
            }

            get
            {
                if (this.IsBond && ((recallQuantity % 100000) > 0))
                {
                    recallQuantity += 100000 - (recallQuantity % 100000);
                }
                else if ((recallQuantity % 100) > 0)
                {
                    recallQuantity += 100 - (recallQuantity % 100);
                }

                return recallQuantity;
            }
        }

        public long ReturnQuantity
        {
            set
            {
                returnQuantity = value;
            }

            get
            {
                if (this.IsBond)
                {
                    returnQuantity -= (returnQuantity % 100000);
                }
                else
                {
                    returnQuantity -= (returnQuantity % 100);
                }

                return returnQuantity;
            }
        }

        public bool EventsEnabled
        {
            get
            {
                return eventsEnabled;
            }
        }

        public bool ShowFedFunds
        {
            set
            {
                showFedFunds = value;
                showLiborFunds = !value;
            }

            get
            {
                return showFedFunds;
            }
        }

        public bool ShowLiborFunds
        {
            set
            {
                showLiborFunds = value;
                showFedFunds = !value;
            }

            get
            {
                return showLiborFunds;
            }
        }

        private void PositionBoxSummaryExpandedCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (!HasInstance(typeof(PositionBoxSummaryExpandedForm)))
            {
                PositionBoxSummaryExpandedForm boxSummaryForm = new PositionBoxSummaryExpandedForm(this);
                boxSummaryForm.MdiParent = this;
                boxSummaryForm.Show();
            }
            else
            {
                ActivateForm(typeof(PositionBoxSummaryExpandedForm));
            }
        }
    }
}
