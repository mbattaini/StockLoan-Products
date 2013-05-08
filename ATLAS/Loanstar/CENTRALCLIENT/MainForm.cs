using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using StockLoan.Common;
using StockLoan.Main;
using StockLoan.MainBusiness;

namespace CentralClient
{
    public class MainForm : Form
	{
		public IPosition PositionAgent = null;
		public IService ServiceAgent = null;
		public IAdmin AdminAgent = null;
		public IInventory InventoryAgent = null;
		public IProcess ProcessAgent = null;
        public IReport ReportAgent = null;
        public ITrading TradingAgent = null;

        private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
		private IContainer components;

		public AdminHolidaysForm adminHolidaysForm = null;
		public AdminKeyValuesForm adminKeyValuesForm = null;
		public AdminBooksForm adminBooksForm = null;
		public AdminUsersForm adminUsersForm = null;
		public AdminCurrenciesForm adminCurrenciesForm = null;
		public AdminCountriesForm adminCountriesForm = null;
		public AdminSecMasterForm adminSecMasterForm = null;
        public AdminRolesForm adminRolesForm = null;
        public TradingBookCreditForm tradingBookCreditForm = null;

        public SecurityMasterForm secMasterForm = null;
		
        public InventorySubscriptionsForm inventorySubscriptionForm = null;
        public InventoryUploadForm inventoryUploadForm = null;
        public InventoryLookupForm inventoryLookupForm = null;		
		        
        public TradingContractBlotterForm tradingContractBlotterForm = null;
		public TradingContractSummaryForm tradingContractSummaryForm = null;		
		public TradingDeskInputForm tradingDeskInputForm = null;
        public TradingFundingRatesForm tradingFundingRatesForm = null;        

		public ProcessStatusForm processStatusForm = null;
		public SplashStartUpInfoForm splashStartupInfoForm = null;
		        
        public TradingReportingForm tradingReportingForm = null;
        public TradingReportingBillingForm tradingBillingForm = null;        

        public HelpAboutForm helpAboutForm = null;

		private C1.Win.C1Command.C1NavBarHorizontalRule c1NavBarHorizontalRule1;
		private C1.Win.C1Command.C1ContextMenu MainContextMenu;
		private C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1Command DockLeftCommand;
		private C1.Win.C1Command.C1CommandControl c1CommandControl1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink2;
		private C1.Win.C1Ribbon.C1Ribbon MainRibbon;
		private C1.Win.C1Ribbon.RibbonConfigToolBar ribbonConfigToolBar1;
		private C1.Win.C1Ribbon.RibbonQat ribbonQat1;
		private C1.Win.C1Ribbon.RibbonTab AdminRibbon;
		private C1.Win.C1Ribbon.RibbonGroup AdminSystemSettingsGroup;
		private C1.Win.C1Ribbon.RibbonButton AdminHolidayButton;
		private C1.Win.C1Ribbon.RibbonButton AdminKeyValuesButton;
        private C1.Win.C1Ribbon.RibbonButton AdminSlaContractsButton;
		private C1.Win.C1Ribbon.RibbonButton AdminBookButton;
		private C1.Win.C1Ribbon.RibbonTab InventoryRibbon;
		private C1.Win.C1Ribbon.RibbonGroup InventorySubscriberGroup;
		private C1.Win.C1Ribbon.RibbonTab TradingRibbon;
		private C1.Win.C1Ribbon.RibbonGroup TradingBorrowsLoansGroup;
		private C1.Win.C1Ribbon.RibbonTab AnalysisRibbon;
		private C1.Win.C1Ribbon.RibbonGroup AnalysisReportRibbonGroup;
		private C1.Win.C1Input.C1Button ExitButton;
		private C1.Win.C1Ribbon.RibbonGroup AdminUserSettingsGroup;
        private C1.Win.C1Ribbon.RibbonGroup AdminClientSettingsGroup;
		private C1.Win.C1Ribbon.RibbonGroup InventoryPublisherGroup;
		private C1.Win.C1Ribbon.RibbonButton InventorySubscriptionsButton;
		private C1.Win.C1Ribbon.RibbonButton InventoryUploadButton;
		private C1.Win.C1Ribbon.RibbonButton InventoryPublicationsButton;
		private C1.Win.C1Ribbon.RibbonButton InventoryDownloadButton;
		private C1.Win.C1Ribbon.RibbonGroup InventoryManagementGroup;
		private C1.Win.C1Ribbon.RibbonButton InventoryCentralButton;
		private C1.Win.C1Ribbon.RibbonButton InventorySettingsButton;
		private C1.Win.C1Ribbon.RibbonButton TradingBoxSummaryButton;
		private C1.Win.C1Ribbon.RibbonButton TradingDeskInputButton;
		private C1.Win.C1Ribbon.RibbonButton TradingContractSummaryButton;
		private C1.Win.C1Ribbon.RibbonButton TradingContractBlotterButton;
		private C1.Win.C1Ribbon.RibbonGroup TradingSummaryGroup;
		private LogicNP.ShellObjects.ShellAppBar ShellAppBar;
		private SplashForm splashForm = null;
		private C1.Win.C1Input.C1Label CountLabel;
		private C1.Win.C1Ribbon.RibbonGroup TradingManagementGroup;
		private C1.Win.C1Command.C1Command AvailabilityCommand;
		private C1.Win.C1Ribbon.RibbonApplicationMenu RibbonMainMenu;
		private C1.Win.C1Ribbon.RibbonButton TradingCashManagementButton;
		private C1.Win.C1Ribbon.RibbonGroup TradingToolbarsGroup;
		private C1.Win.C1Ribbon.RibbonButton TradingToolBarAvailabilityForm;
		private C1.Win.C1Ribbon.RibbonButton ribbonButton1;
		private FontDialog MainFontDialog;
		private C1.Win.C1Ribbon.RibbonButton TradingFundingRatesButton;
		private C1.Win.C1Ribbon.RibbonGroup AdminLoginGroup;
		private C1.Win.C1Ribbon.RibbonButton AdminLoginButton;
		private C1.Win.C1Ribbon.RibbonButton InventoryLookupButton;
		private C1.Win.C1Ribbon.RibbonGroup AdminStaticDataGroup;
		private C1.Win.C1Ribbon.RibbonButton AdminCurrenciesButton;
		private C1.Win.C1Ribbon.RibbonButton AdminCountriesButton;
		private C1.Win.C1Ribbon.RibbonButton AdminSecMasterButton;
        private SaveFileDialog SaveFileDialog;
		private C1.Win.C1Ribbon.RibbonTab ProcessRibbon;
		private C1.Win.C1Ribbon.RibbonGroup ProcessMessagesRibbonGroup;
		private C1.Win.C1Ribbon.RibbonButton ProcessStatusButton;
		private C1.Win.C1Ribbon.RibbonButton ProcessMessagesButton;

		private string dockingEdgeString;
        private C1.Win.C1Ribbon.RibbonButton AdminSecMasterBarButton;
		private bool secMasterOpen = false;

        // private datasets , common information
        private DataSet dsBookGroups;
        private bool isBookGroupsLoaded;

        private DataSet dsBooks;
        private bool isBooksLoaded;

        private DataSet dsCountries;
        private bool isCountriesLoaded;

        private DataSet dsCurrencies;
        private bool isCurrenciesLoaded;
        private BackgroundWorker FundingRatesBackgroundWorker;

        public FundingRates FundingRates;
        private C1.Win.C1Ribbon.RibbonButton AdminRolesButton;
        private C1.Win.C1Ribbon.RibbonButton AdminUsersButton;
        private C1.Win.C1Ribbon.RibbonTab HelpRibbon;
        private C1.Win.C1Ribbon.RibbonGroup ribbonGroup1;
        private C1.Win.C1Ribbon.RibbonButton HelpAboutRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton HelpStartupForm;
        public StaticInformation StaticInformation;
        private C1.Win.C1Ribbon.RibbonGroup TradingCreditRibbonGroup;
        private C1.Win.C1Ribbon.RibbonButton TradingBookCreditButton;
        private C1.Win.C1Ribbon.RibbonButton AnalysisReportingTradingRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton AnalysisReportingBillingRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton ribbonButton2;
        private C1.Win.C1Ribbon.RibbonGroup ribbonGroup2;

        private string tempPath;

		public MainForm()
		{			
			InitializeComponent();

			try
			{
				Log.Level = Standard.ConfigValue("LogLevel");
				Log.FilePath = Standard.ConfigValue("LogFilePath");

				Log.Write("", 1);
				Log.Write("Running version " + Application.ProductVersion + " [MainForm.MainForm]", Log.SuccessAudit, 1);

                tempPath = Standard.ConfigValue("TempFilePath");
				RegistryValue.Name = Application.ProductName;				
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}

			splashForm = new SplashForm();
			splashForm.Show();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
            this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.DockLeftCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandControl1 = new C1.Win.C1Command.C1CommandControl();
            this.AvailabilityCommand = new C1.Win.C1Command.C1Command();
            this.c1NavBarHorizontalRule1 = new C1.Win.C1Command.C1NavBarHorizontalRule();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.MainRibbon = new C1.Win.C1Ribbon.C1Ribbon();
            this.RibbonMainMenu = new C1.Win.C1Ribbon.RibbonApplicationMenu();
            this.ribbonConfigToolBar1 = new C1.Win.C1Ribbon.RibbonConfigToolBar();
            this.ribbonQat1 = new C1.Win.C1Ribbon.RibbonQat();
            this.AdminRibbon = new C1.Win.C1Ribbon.RibbonTab();
            this.AdminSystemSettingsGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.AdminHolidayButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminKeyValuesButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminUserSettingsGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.AdminRolesButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminUsersButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminClientSettingsGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.AdminBookButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminSlaContractsButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminLoginGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.AdminLoginButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminStaticDataGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.AdminCurrenciesButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminCountriesButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminSecMasterBarButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AdminSecMasterButton = new C1.Win.C1Ribbon.RibbonButton();
            this.InventoryRibbon = new C1.Win.C1Ribbon.RibbonTab();
            this.InventorySubscriberGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.InventorySubscriptionsButton = new C1.Win.C1Ribbon.RibbonButton();
            this.InventoryUploadButton = new C1.Win.C1Ribbon.RibbonButton();
            this.InventoryLookupButton = new C1.Win.C1Ribbon.RibbonButton();
            this.InventoryPublisherGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.InventoryPublicationsButton = new C1.Win.C1Ribbon.RibbonButton();
            this.InventoryDownloadButton = new C1.Win.C1Ribbon.RibbonButton();
            this.InventoryManagementGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.InventoryCentralButton = new C1.Win.C1Ribbon.RibbonButton();
            this.InventorySettingsButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingRibbon = new C1.Win.C1Ribbon.RibbonTab();
            this.TradingBorrowsLoansGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TradingDeskInputButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingContractBlotterButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingSummaryGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TradingBoxSummaryButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingContractSummaryButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingManagementGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TradingCashManagementButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingFundingRatesButton = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingToolbarsGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TradingToolBarAvailabilityForm = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonButton1 = new C1.Win.C1Ribbon.RibbonButton();
            this.TradingCreditRibbonGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.TradingBookCreditButton = new C1.Win.C1Ribbon.RibbonButton();
            this.ProcessRibbon = new C1.Win.C1Ribbon.RibbonTab();
            this.ProcessMessagesRibbonGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.ProcessStatusButton = new C1.Win.C1Ribbon.RibbonButton();
            this.ProcessMessagesButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AnalysisRibbon = new C1.Win.C1Ribbon.RibbonTab();
            this.AnalysisReportRibbonGroup = new C1.Win.C1Ribbon.RibbonGroup();
            this.AnalysisReportingTradingRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.AnalysisReportingBillingRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.HelpRibbon = new C1.Win.C1Ribbon.RibbonTab();
            this.ribbonGroup1 = new C1.Win.C1Ribbon.RibbonGroup();
            this.HelpAboutRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.HelpStartupForm = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonGroup2 = new C1.Win.C1Ribbon.RibbonGroup();
            this.ribbonButton2 = new C1.Win.C1Ribbon.RibbonButton();
            this.ExitButton = new C1.Win.C1Input.C1Button();
            this.ShellAppBar = new LogicNP.ShellObjects.ShellAppBar(this.components);
            this.CountLabel = new C1.Win.C1Input.C1Label();
            this.MainFontDialog = new System.Windows.Forms.FontDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FundingRatesBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainRibbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShellAppBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // MainCommandHolder
            // 
            this.MainCommandHolder.Commands.Add(this.MainContextMenu);
            this.MainCommandHolder.Commands.Add(this.DockLeftCommand);
            this.MainCommandHolder.Commands.Add(this.c1CommandControl1);
            this.MainCommandHolder.Commands.Add(this.AvailabilityCommand);
            this.MainCommandHolder.Owner = this;
            this.MainCommandHolder.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Black;
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
            this.MainContextMenu.Name = "MainContextMenu";
            this.MainContextMenu.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Black;
            this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Black;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.DockLeftCommand;
            this.c1CommandLink1.Text = "Dock Left";
            // 
            // DockLeftCommand
            // 
            this.DockLeftCommand.Name = "DockLeftCommand";
            this.DockLeftCommand.Text = "Dock Left";
            // 
            // c1CommandControl1
            // 
            this.c1CommandControl1.Name = "c1CommandControl1";
            // 
            // AvailabilityCommand
            // 
            this.AvailabilityCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("AvailabilityCommand.Icon")));
            this.AvailabilityCommand.Name = "AvailabilityCommand";
            this.AvailabilityCommand.Text = "Availability";
            // 
            // c1NavBarHorizontalRule1
            // 
            this.c1NavBarHorizontalRule1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1NavBarHorizontalRule1.Location = new System.Drawing.Point(0, 0);
            this.c1NavBarHorizontalRule1.Name = "c1NavBarHorizontalRule1";
            this.c1NavBarHorizontalRule1.Size = new System.Drawing.Size(0, 2);
            // 
            // MainRibbon
            // 
            this.MainRibbon.ApplicationMenuHolder = this.RibbonMainMenu;
            this.MainRibbon.ConfigToolBarHolder = this.ribbonConfigToolBar1;
            this.MainRibbon.Location = new System.Drawing.Point(1, 1);
            this.MainRibbon.MinimumHeight = 125;
            this.MainRibbon.Name = "MainRibbon";
            this.MainRibbon.QatHolder = this.ribbonQat1;
            this.MainRibbon.Size = new System.Drawing.Size(1469, 156);
            this.MainRibbon.Tabs.Add(this.AdminRibbon);
            this.MainRibbon.Tabs.Add(this.InventoryRibbon);
            this.MainRibbon.Tabs.Add(this.TradingRibbon);
            this.MainRibbon.Tabs.Add(this.ProcessRibbon);
            this.MainRibbon.Tabs.Add(this.AnalysisRibbon);
            this.MainRibbon.Tabs.Add(this.HelpRibbon);
            this.MainRibbon.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
            // 
            // RibbonMainMenu
            // 
            this.RibbonMainMenu.DropDownWidth = 0;
            this.RibbonMainMenu.Name = "RibbonMainMenu";
            this.RibbonMainMenu.SmallImage = ((System.Drawing.Image)(resources.GetObject("RibbonMainMenu.SmallImage")));
            // 
            // ribbonConfigToolBar1
            // 
            this.ribbonConfigToolBar1.Enabled = false;
            this.ribbonConfigToolBar1.Name = "ribbonConfigToolBar1";
            this.ribbonConfigToolBar1.Visible = false;
            // 
            // ribbonQat1
            // 
            this.ribbonQat1.Name = "ribbonQat1";
            // 
            // AdminRibbon
            // 
            this.AdminRibbon.Groups.Add(this.AdminSystemSettingsGroup);
            this.AdminRibbon.Groups.Add(this.AdminUserSettingsGroup);
            this.AdminRibbon.Groups.Add(this.AdminClientSettingsGroup);
            this.AdminRibbon.Groups.Add(this.AdminLoginGroup);
            this.AdminRibbon.Groups.Add(this.AdminStaticDataGroup);
            this.AdminRibbon.Name = "AdminRibbon";
            this.AdminRibbon.Text = "Administration";
            // 
            // AdminSystemSettingsGroup
            // 
            this.AdminSystemSettingsGroup.Items.Add(this.AdminHolidayButton);
            this.AdminSystemSettingsGroup.Items.Add(this.AdminKeyValuesButton);
            this.AdminSystemSettingsGroup.Name = "AdminSystemSettingsGroup";
            this.AdminSystemSettingsGroup.Text = "System Settings";
            // 
            // AdminHolidayButton
            // 
            this.AdminHolidayButton.Description = "Admin-Holidays";
            this.AdminHolidayButton.Enabled = false;
            this.AdminHolidayButton.Name = "AdminHolidayButton";
            this.AdminHolidayButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminHolidayButton.SmallImage")));
            this.AdminHolidayButton.Text = "Holidays";
            this.AdminHolidayButton.ToolTip = "Holidays";
            this.AdminHolidayButton.Click += new System.EventHandler(this.AdminHolidayButton_Click);
            // 
            // AdminKeyValuesButton
            // 
            this.AdminKeyValuesButton.Description = "Admin - KeyValues";
            this.AdminKeyValuesButton.Enabled = false;
            this.AdminKeyValuesButton.Name = "AdminKeyValuesButton";
            this.AdminKeyValuesButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminKeyValuesButton.SmallImage")));
            this.AdminKeyValuesButton.Text = "Key Values";
            this.AdminKeyValuesButton.ToolTip = "Key Values";
            this.AdminKeyValuesButton.Click += new System.EventHandler(this.AdminKeyValuesButton_Click);
            // 
            // AdminUserSettingsGroup
            // 
            this.AdminUserSettingsGroup.Items.Add(this.AdminRolesButton);
            this.AdminUserSettingsGroup.Items.Add(this.AdminUsersButton);
            this.AdminUserSettingsGroup.Name = "AdminUserSettingsGroup";
            this.AdminUserSettingsGroup.Text = "User Settings";
            // 
            // AdminRolesButton
            // 
            this.AdminRolesButton.Enabled = false;
            this.AdminRolesButton.Name = "AdminRolesButton";
            this.AdminRolesButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminRolesButton.SmallImage")));
            this.AdminRolesButton.Text = "Roles";
            this.AdminRolesButton.Click += new System.EventHandler(this.AdminRolesButton_Click);
            // 
            // AdminUsersButton
            // 
            this.AdminUsersButton.Enabled = false;
            this.AdminUsersButton.Name = "AdminUsersButton";
            this.AdminUsersButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminUsersButton.SmallImage")));
            this.AdminUsersButton.Text = "Users";
            // 
            // AdminClientSettingsGroup
            // 
            this.AdminClientSettingsGroup.Items.Add(this.AdminBookButton);
            this.AdminClientSettingsGroup.Items.Add(this.AdminSlaContractsButton);
            this.AdminClientSettingsGroup.Name = "AdminClientSettingsGroup";
            this.AdminClientSettingsGroup.Text = "Client Settings";
            // 
            // AdminBookButton
            // 
            this.AdminBookButton.Description = "Admin - Book Information";
            this.AdminBookButton.Enabled = false;
            this.AdminBookButton.Name = "AdminBookButton";
            this.AdminBookButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminBookButton.SmallImage")));
            this.AdminBookButton.Text = "Book Information";
            this.AdminBookButton.ToolTip = "Book Information";
            this.AdminBookButton.Click += new System.EventHandler(this.AdminCounterPartyButton_Click);
            // 
            // AdminSlaContractsButton
            // 
            this.AdminSlaContractsButton.Enabled = false;
            this.AdminSlaContractsButton.Name = "AdminSlaContractsButton";
            this.AdminSlaContractsButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminSlaContractsButton.SmallImage")));
            this.AdminSlaContractsButton.Text = "SLA Contracts";
            // 
            // AdminLoginGroup
            // 
            this.AdminLoginGroup.Items.Add(this.AdminLoginButton);
            this.AdminLoginGroup.Name = "AdminLoginGroup";
            this.AdminLoginGroup.Text = "Login";
            // 
            // AdminLoginButton
            // 
            this.AdminLoginButton.Name = "AdminLoginButton";
            this.AdminLoginButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminLoginButton.SmallImage")));
            this.AdminLoginButton.Text = "Login";
            this.AdminLoginButton.Click += new System.EventHandler(this.AdminLoginButton_Click);
            // 
            // AdminStaticDataGroup
            // 
            this.AdminStaticDataGroup.Items.Add(this.AdminCurrenciesButton);
            this.AdminStaticDataGroup.Items.Add(this.AdminCountriesButton);
            this.AdminStaticDataGroup.Items.Add(this.AdminSecMasterBarButton);
            this.AdminStaticDataGroup.Items.Add(this.AdminSecMasterButton);
            this.AdminStaticDataGroup.Name = "AdminStaticDataGroup";
            this.AdminStaticDataGroup.Text = "Static Data";
            // 
            // AdminCurrenciesButton
            // 
            this.AdminCurrenciesButton.Enabled = false;
            this.AdminCurrenciesButton.LargeImage = ((System.Drawing.Image)(resources.GetObject("AdminCurrenciesButton.LargeImage")));
            this.AdminCurrenciesButton.Name = "AdminCurrenciesButton";
            this.AdminCurrenciesButton.Text = "Currencies";
            this.AdminCurrenciesButton.Click += new System.EventHandler(this.AdminCurrenciesButton_Click);
            // 
            // AdminCountriesButton
            // 
            this.AdminCountriesButton.Enabled = false;
            this.AdminCountriesButton.Name = "AdminCountriesButton";
            this.AdminCountriesButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminCountriesButton.SmallImage")));
            this.AdminCountriesButton.Text = "Countries";
            this.AdminCountriesButton.Click += new System.EventHandler(this.AdminCountriesButton_Click);
            // 
            // AdminSecMasterBarButton
            // 
            this.AdminSecMasterBarButton.Enabled = false;
            this.AdminSecMasterBarButton.Name = "AdminSecMasterBarButton";
            this.AdminSecMasterBarButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminSecMasterBarButton.SmallImage")));
            this.AdminSecMasterBarButton.Text = "Security Master Bar";
            this.AdminSecMasterBarButton.Click += new System.EventHandler(this.AdminSecMasterBarButton_Click);
            // 
            // AdminSecMasterButton
            // 
            this.AdminSecMasterButton.Enabled = false;
            this.AdminSecMasterButton.Name = "AdminSecMasterButton";
            this.AdminSecMasterButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AdminSecMasterButton.SmallImage")));
            this.AdminSecMasterButton.Text = "Security Master";
            this.AdminSecMasterButton.Click += new System.EventHandler(this.AdminSecMasterButton_Click);
            // 
            // InventoryRibbon
            // 
            this.InventoryRibbon.Groups.Add(this.InventorySubscriberGroup);
            this.InventoryRibbon.Groups.Add(this.InventoryPublisherGroup);
            this.InventoryRibbon.Groups.Add(this.InventoryManagementGroup);
            this.InventoryRibbon.Name = "InventoryRibbon";
            this.InventoryRibbon.Text = "Inventory";
            // 
            // InventorySubscriberGroup
            // 
            this.InventorySubscriberGroup.Items.Add(this.InventorySubscriptionsButton);
            this.InventorySubscriberGroup.Items.Add(this.InventoryUploadButton);
            this.InventorySubscriberGroup.Items.Add(this.InventoryLookupButton);
            this.InventorySubscriberGroup.Name = "InventorySubscriberGroup";
            this.InventorySubscriberGroup.Text = "Subscriber";
            // 
            // InventorySubscriptionsButton
            // 
            this.InventorySubscriptionsButton.Description = "Inventory - Subscriptions";
            this.InventorySubscriptionsButton.Enabled = false;
            this.InventorySubscriptionsButton.Name = "InventorySubscriptionsButton";
            this.InventorySubscriptionsButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("InventorySubscriptionsButton.SmallImage")));
            this.InventorySubscriptionsButton.Text = " Subscriptions";
            this.InventorySubscriptionsButton.ToolTip = "Subscriptions";
            this.InventorySubscriptionsButton.Click += new System.EventHandler(this.InventorySubscriptionsButton_Click);
            // 
            // InventoryUploadButton
            // 
            this.InventoryUploadButton.Enabled = false;
            this.InventoryUploadButton.Name = "InventoryUploadButton";
            this.InventoryUploadButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("InventoryUploadButton.SmallImage")));
            this.InventoryUploadButton.Text = "Upload";
            this.InventoryUploadButton.Click += new System.EventHandler(this.InventoryUploadButton_Click);
            // 
            // InventoryLookupButton
            // 
            this.InventoryLookupButton.Enabled = false;
            this.InventoryLookupButton.Name = "InventoryLookupButton";
            this.InventoryLookupButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("InventoryLookupButton.SmallImage")));
            this.InventoryLookupButton.Text = "Lookup";
            this.InventoryLookupButton.Click += new System.EventHandler(this.InventoryLookupButton_Click);
            // 
            // InventoryPublisherGroup
            // 
            this.InventoryPublisherGroup.Items.Add(this.InventoryPublicationsButton);
            this.InventoryPublisherGroup.Items.Add(this.InventoryDownloadButton);
            this.InventoryPublisherGroup.Name = "InventoryPublisherGroup";
            this.InventoryPublisherGroup.Text = "Publisher";
            // 
            // InventoryPublicationsButton
            // 
            this.InventoryPublicationsButton.Enabled = false;
            this.InventoryPublicationsButton.Name = "InventoryPublicationsButton";
            this.InventoryPublicationsButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("InventoryPublicationsButton.SmallImage")));
            this.InventoryPublicationsButton.Text = "Publications";
            // 
            // InventoryDownloadButton
            // 
            this.InventoryDownloadButton.Enabled = false;
            this.InventoryDownloadButton.Name = "InventoryDownloadButton";
            this.InventoryDownloadButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("InventoryDownloadButton.SmallImage")));
            this.InventoryDownloadButton.Text = "Download";
            // 
            // InventoryManagementGroup
            // 
            this.InventoryManagementGroup.Items.Add(this.InventoryCentralButton);
            this.InventoryManagementGroup.Items.Add(this.InventorySettingsButton);
            this.InventoryManagementGroup.Name = "InventoryManagementGroup";
            this.InventoryManagementGroup.Text = "Management";
            // 
            // InventoryCentralButton
            // 
            this.InventoryCentralButton.Enabled = false;
            this.InventoryCentralButton.Name = "InventoryCentralButton";
            this.InventoryCentralButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("InventoryCentralButton.SmallImage")));
            this.InventoryCentralButton.Text = "Central";
            // 
            // InventorySettingsButton
            // 
            this.InventorySettingsButton.Enabled = false;
            this.InventorySettingsButton.Name = "InventorySettingsButton";
            this.InventorySettingsButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("InventorySettingsButton.SmallImage")));
            this.InventorySettingsButton.Text = "Settings";
            // 
            // TradingRibbon
            // 
            this.TradingRibbon.Groups.Add(this.TradingBorrowsLoansGroup);
            this.TradingRibbon.Groups.Add(this.TradingSummaryGroup);
            this.TradingRibbon.Groups.Add(this.TradingManagementGroup);
            this.TradingRibbon.Groups.Add(this.TradingToolbarsGroup);
            this.TradingRibbon.Groups.Add(this.TradingCreditRibbonGroup);
            this.TradingRibbon.Name = "TradingRibbon";
            this.TradingRibbon.Text = "Trading";
            // 
            // TradingBorrowsLoansGroup
            // 
            this.TradingBorrowsLoansGroup.Items.Add(this.TradingDeskInputButton);
            this.TradingBorrowsLoansGroup.Items.Add(this.TradingContractBlotterButton);
            this.TradingBorrowsLoansGroup.Name = "TradingBorrowsLoansGroup";
            this.TradingBorrowsLoansGroup.Text = "Borrows / Loans";
            // 
            // TradingDeskInputButton
            // 
            this.TradingDeskInputButton.Description = "Trading - Desk Input";
            this.TradingDeskInputButton.Enabled = false;
            this.TradingDeskInputButton.Name = "TradingDeskInputButton";
            this.TradingDeskInputButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingDeskInputButton.SmallImage")));
            this.TradingDeskInputButton.Text = "Desk Input";
            this.TradingDeskInputButton.ToolTip = "Desk Input";
            this.TradingDeskInputButton.Click += new System.EventHandler(this.TradingDeskInputButton_Click);
            // 
            // TradingContractBlotterButton
            // 
            this.TradingContractBlotterButton.Description = "Trading - Contract Blotter";
            this.TradingContractBlotterButton.Enabled = false;
            this.TradingContractBlotterButton.Name = "TradingContractBlotterButton";
            this.TradingContractBlotterButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingContractBlotterButton.SmallImage")));
            this.TradingContractBlotterButton.Text = "Contract Blotter";
            this.TradingContractBlotterButton.ToolTip = "Contract Blotter";
            this.TradingContractBlotterButton.Click += new System.EventHandler(this.TradingContractBlotterButton_Click);
            // 
            // TradingSummaryGroup
            // 
            this.TradingSummaryGroup.Items.Add(this.TradingBoxSummaryButton);
            this.TradingSummaryGroup.Items.Add(this.TradingContractSummaryButton);
            this.TradingSummaryGroup.Name = "TradingSummaryGroup";
            this.TradingSummaryGroup.Text = "Summary";
            // 
            // TradingBoxSummaryButton
            // 
            this.TradingBoxSummaryButton.Description = "Trading - Box Summary";
            this.TradingBoxSummaryButton.Enabled = false;
            this.TradingBoxSummaryButton.Name = "TradingBoxSummaryButton";
            this.TradingBoxSummaryButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingBoxSummaryButton.SmallImage")));
            this.TradingBoxSummaryButton.Text = "Box Summary";
            this.TradingBoxSummaryButton.ToolTip = "Box Summary";
            this.TradingBoxSummaryButton.Click += new System.EventHandler(this.TradingBoxSummaryButton_Click);
            // 
            // TradingContractSummaryButton
            // 
            this.TradingContractSummaryButton.Description = "Trading - Contract Summary";
            this.TradingContractSummaryButton.Enabled = false;
            this.TradingContractSummaryButton.Name = "TradingContractSummaryButton";
            this.TradingContractSummaryButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingContractSummaryButton.SmallImage")));
            this.TradingContractSummaryButton.Text = "Contract Summary";
            this.TradingContractSummaryButton.ToolTip = "Contract Summary";
            this.TradingContractSummaryButton.Click += new System.EventHandler(this.TradingContractSummaryButton_Click);
            // 
            // TradingManagementGroup
            // 
            this.TradingManagementGroup.Items.Add(this.TradingCashManagementButton);
            this.TradingManagementGroup.Items.Add(this.TradingFundingRatesButton);
            this.TradingManagementGroup.Name = "TradingManagementGroup";
            this.TradingManagementGroup.Text = "Management";
            // 
            // TradingCashManagementButton
            // 
            this.TradingCashManagementButton.Description = "Trading - Marks";
            this.TradingCashManagementButton.Enabled = false;
            this.TradingCashManagementButton.Name = "TradingCashManagementButton";
            this.TradingCashManagementButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingCashManagementButton.SmallImage")));
            this.TradingCashManagementButton.Text = "Cash Management";
            this.TradingCashManagementButton.ToolTip = "Cash Management";
            this.TradingCashManagementButton.Click += new System.EventHandler(this.TradingCashManagementButton_Click);
            // 
            // TradingFundingRatesButton
            // 
            this.TradingFundingRatesButton.Description = "Trading - Funding Rates";
            this.TradingFundingRatesButton.Enabled = false;
            this.TradingFundingRatesButton.Name = "TradingFundingRatesButton";
            this.TradingFundingRatesButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingFundingRatesButton.SmallImage")));
            this.TradingFundingRatesButton.Text = "Funding Rates";
            this.TradingFundingRatesButton.ToolTip = "Funding Rates";
            this.TradingFundingRatesButton.Click += new System.EventHandler(this.TradingFundingRatesButton_Click);
            // 
            // TradingToolbarsGroup
            // 
            this.TradingToolbarsGroup.Items.Add(this.TradingToolBarAvailabilityForm);
            this.TradingToolbarsGroup.Items.Add(this.ribbonButton1);
            this.TradingToolbarsGroup.Name = "TradingToolbarsGroup";
            this.TradingToolbarsGroup.Text = "Toolbars";
            // 
            // TradingToolBarAvailabilityForm
            // 
            this.TradingToolBarAvailabilityForm.Enabled = false;
            this.TradingToolBarAvailabilityForm.Name = "TradingToolBarAvailabilityForm";
            this.TradingToolBarAvailabilityForm.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingToolBarAvailabilityForm.SmallImage")));
            this.TradingToolBarAvailabilityForm.Text = "Availability";
            this.TradingToolBarAvailabilityForm.Click += new System.EventHandler(this.TradingToolBarAvailabilityForm_Click);
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Enabled = false;
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "Needs";
            // 
            // TradingCreditRibbonGroup
            // 
            this.TradingCreditRibbonGroup.Items.Add(this.TradingBookCreditButton);
            this.TradingCreditRibbonGroup.Name = "TradingCreditRibbonGroup";
            this.TradingCreditRibbonGroup.Text = "Credit";
            // 
            // TradingBookCreditButton
            // 
            this.TradingBookCreditButton.Enabled = false;
            this.TradingBookCreditButton.Name = "TradingBookCreditButton";
            this.TradingBookCreditButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("TradingBookCreditButton.SmallImage")));
            this.TradingBookCreditButton.Text = "Book Credit";
            this.TradingBookCreditButton.Click += new System.EventHandler(this.TradingBookCreditButton_Click);
            // 
            // ProcessRibbon
            // 
            this.ProcessRibbon.Groups.Add(this.ProcessMessagesRibbonGroup);
            this.ProcessRibbon.Name = "ProcessRibbon";
            this.ProcessRibbon.Text = "Process";
            // 
            // ProcessMessagesRibbonGroup
            // 
            this.ProcessMessagesRibbonGroup.Items.Add(this.ProcessStatusButton);
            this.ProcessMessagesRibbonGroup.Items.Add(this.ProcessMessagesButton);
            this.ProcessMessagesRibbonGroup.Name = "ProcessMessagesRibbonGroup";
            this.ProcessMessagesRibbonGroup.Text = "Messages";
            // 
            // ProcessStatusButton
            // 
            this.ProcessStatusButton.Enabled = false;
            this.ProcessStatusButton.Name = "ProcessStatusButton";
            this.ProcessStatusButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("ProcessStatusButton.SmallImage")));
            this.ProcessStatusButton.Text = "Status";
            this.ProcessStatusButton.Click += new System.EventHandler(this.ProcessStatusButton_Click);
            // 
            // ProcessMessagesButton
            // 
            this.ProcessMessagesButton.Enabled = false;
            this.ProcessMessagesButton.Name = "ProcessMessagesButton";
            this.ProcessMessagesButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("ProcessMessagesButton.SmallImage")));
            this.ProcessMessagesButton.Text = "Messages";
            this.ProcessMessagesButton.Click += new System.EventHandler(this.ProcessMessagesButton_Click);
            // 
            // AnalysisRibbon
            // 
            this.AnalysisRibbon.Groups.Add(this.AnalysisReportRibbonGroup);
            this.AnalysisRibbon.Name = "AnalysisRibbon";
            this.AnalysisRibbon.Text = "Analysis";
            // 
            // AnalysisReportRibbonGroup
            // 
            this.AnalysisReportRibbonGroup.Items.Add(this.AnalysisReportingTradingRibbonButton);
            this.AnalysisReportRibbonGroup.Items.Add(this.AnalysisReportingBillingRibbonButton);
            this.AnalysisReportRibbonGroup.Name = "AnalysisReportRibbonGroup";
            this.AnalysisReportRibbonGroup.Text = "Reports";
            // 
            // AnalysisReportingTradingRibbonButton
            // 
            this.AnalysisReportingTradingRibbonButton.Enabled = false;
            this.AnalysisReportingTradingRibbonButton.Name = "AnalysisReportingTradingRibbonButton";
            this.AnalysisReportingTradingRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AnalysisReportingTradingRibbonButton.SmallImage")));
            this.AnalysisReportingTradingRibbonButton.Text = "Trading";
            this.AnalysisReportingTradingRibbonButton.Click += new System.EventHandler(this.AnalysisReportingTradingRibbonButton_Click);
            // 
            // AnalysisReportingBillingRibbonButton
            // 
            this.AnalysisReportingBillingRibbonButton.Enabled = false;
            this.AnalysisReportingBillingRibbonButton.Name = "AnalysisReportingBillingRibbonButton";
            this.AnalysisReportingBillingRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("AnalysisReportingBillingRibbonButton.SmallImage")));
            this.AnalysisReportingBillingRibbonButton.Text = "Billing";
            this.AnalysisReportingBillingRibbonButton.Click += new System.EventHandler(this.AnalysisReportingBillingRibbonButton_Click);
            // 
            // HelpRibbon
            // 
            this.HelpRibbon.Groups.Add(this.ribbonGroup1);
            this.HelpRibbon.Groups.Add(this.ribbonGroup2);
            this.HelpRibbon.Name = "HelpRibbon";
            this.HelpRibbon.Text = "Help";
            // 
            // ribbonGroup1
            // 
            this.ribbonGroup1.Items.Add(this.HelpAboutRibbonButton);
            this.ribbonGroup1.Items.Add(this.HelpStartupForm);
            this.ribbonGroup1.Name = "ribbonGroup1";
            this.ribbonGroup1.Text = "About";
            // 
            // HelpAboutRibbonButton
            // 
            this.HelpAboutRibbonButton.Name = "HelpAboutRibbonButton";
            this.HelpAboutRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("HelpAboutRibbonButton.SmallImage")));
            this.HelpAboutRibbonButton.Text = "About";
            this.HelpAboutRibbonButton.Click += new System.EventHandler(this.HelpAboutRibbonButton_Click);
            // 
            // HelpStartupForm
            // 
            this.HelpStartupForm.AllowSelection = true;
            this.HelpStartupForm.Name = "HelpStartupForm";
            this.HelpStartupForm.SmallImage = ((System.Drawing.Image)(resources.GetObject("HelpStartupForm.SmallImage")));
            this.HelpStartupForm.Text = "Startup Screen";
            this.HelpStartupForm.Click += new System.EventHandler(this.HelpStartupForm_Click);
            // 
            // ribbonGroup2
            // 
            this.ribbonGroup2.Items.Add(this.ribbonButton2);
            this.ribbonGroup2.Name = "ribbonGroup2";
            this.ribbonGroup2.Text = "Exit";
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.SmallImage")));
            this.ribbonButton2.Text = "Logout";
            this.ribbonButton2.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Image = ((System.Drawing.Image)(resources.GetObject("ExitButton.Image")));
            this.ExitButton.Location = new System.Drawing.Point(1559, 2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(26, 22);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ShellAppBar
            // 
            this.ShellAppBar.AllowedDragDockingEdges = LogicNP.ShellObjects.DragDockingEdges.Top;
            this.ShellAppBar.AllowResize = true;
            this.ShellAppBar.DockingEdge = LogicNP.ShellObjects.DockingEdges.Top;
            this.ShellAppBar.VAnchor = LogicNP.ShellObjects.VAnchor.VAnchorTop;
            this.ShellAppBar.DockingEdgeChanged += new System.EventHandler(this.ShellAppBar_DockingEdgeChanged);
            this.ShellAppBar.HostForm = this;
            // 
            // CountLabel
            // 
            this.CountLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CountLabel.AutoSize = true;
            this.CountLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CountLabel.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountLabel.ForeColor = System.Drawing.Color.Red;
            this.CountLabel.Location = new System.Drawing.Point(1557, 38);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(0, 13);
            this.CountLabel.TabIndex = 5;
            this.CountLabel.Tag = null;
            this.CountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CountLabel.TextDetached = true;
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "xls";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CancelButton = this.ExitButton;
            this.ClientSize = new System.Drawing.Size(1471, 155);
            this.ControlBox = false;
            this.Controls.Add(this.CountLabel);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.MainRibbon);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.Text = "Loan Star";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainRibbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShellAppBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new MainForm());
		}

		public void Alert(string formName, string formError)
		{
			try
			{
				MessageForm msgForm = new MessageForm();
				msgForm.Set(formName, formError);
				msgForm.Show();
			}
			catch { }
		}

		private void AdminHolidaysCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (adminHolidaysForm == null)
				{
					adminHolidaysForm = new AdminHolidaysForm(this);
					adminHolidaysForm.MdiParent = this;
					adminHolidaysForm.Show();
				}
				else
				{
					adminHolidaysForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				AdminAgent = (IAdmin)RemotingTools.ObjectGet(typeof(IAdmin));
				if (AdminAgent == null)
				{
					Log.Write("Remoting config values for the admin agent (IAdmin) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
				}

				ServiceAgent = (IService)RemotingTools.ObjectGet(typeof(IService));
				if (ServiceAgent == null)
				{
					Log.Write("Remoting config values for the service agent (IService) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
				}

				PositionAgent = (IPosition)RemotingTools.ObjectGet(typeof(IPosition));
				if (PositionAgent == null)
				{
					Log.Write("Remoting config values for the position agent (IPosition) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
				}

				ProcessAgent = (IProcess)RemotingTools.ObjectGet(typeof(IProcess));
				if (ProcessAgent == null)
				{
					Log.Write("Remoting config values for the process agent (IProcess) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
				}

				InventoryAgent = (IInventory)RemotingTools.ObjectGet(typeof(IInventory));
				if (InventoryAgent == null)
				{
					Log.Write("Remoting config values for the position agent (IInventory) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
				}

                ReportAgent = (IReport)RemotingTools.ObjectGet(typeof(IReport));
                if (ReportAgent == null)
                {
                    Log.Write("Remoting config values for the position agent (IReport) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
                }


                TradingAgent = (ITrading)RemotingTools.ObjectGet(typeof(ITrading));
                if (TradingAgent == null)
                {
                    Log.Write("Remoting config values for the position agent (ITrading) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
                }


                FundingRates = new FundingRates(ServiceAgent, PositionAgent);
                StaticInformation = new StaticInformation(ServiceAgent, PositionAgent, UserId);
            }
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}


			try
			{

				secMasterOpen = bool.Parse(RegistryValue.Read(this.Name, "SecMasterOpen", false.ToString()));

				int screenIndex = 0;
				string deviceName = RegistryValue.Read(this.Name, "DeviceName", ShellAppBar.DockingScreen.DeviceName);

				dockingEdgeString = "Top";
				dockingEdgeString = RegistryValue.Read(this.Name, "DockingEdge", dockingEdgeString);

				Screen[] screens = Screen.AllScreens;
				char[] remove = { '\\', '.', '\0' };

				deviceName = deviceName.Trim(remove).Substring(0, 8);

				for (int index = 0; index < screens.Length; index++)
				{
					if (screens[index].DeviceName.Trim(remove).Substring(0, 8).Equals(deviceName))
					{
						screenIndex = index;
						break;
					}
				}

				Object dockingEdgeObj = null;

				switch (dockingEdgeString)
				{
					case "Top":
						dockingEdgeObj = LogicNP.ShellObjects.DockingEdges.Top;
						break;

					case "Bottom":
						dockingEdgeObj = LogicNP.ShellObjects.DockingEdges.Bottom;
						break;

					default:
						dockingEdgeObj = LogicNP.ShellObjects.DockingEdges.Top;
						break;
				}

				ShellAppBar.SetDockingEdgeAndScreen((LogicNP.ShellObjects.DockingEdges)dockingEdgeObj, screens[screenIndex]);
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}

			splashForm.Close();
		}

		private void AdminHolidayButton_Click(object sender, EventArgs e)
		{
			if (adminHolidaysForm == null)
			{
				adminHolidaysForm = new AdminHolidaysForm(this);
				adminHolidaysForm.Show();
			}
			else
			{
				adminHolidaysForm.Activate();
			}
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			this.Close();

			if (secMasterForm != null)
			{
				secMasterForm.Close();
			}
		}

		private void AdminCounterPartyButton_Click(object sender, EventArgs e)
		{
			if (adminBooksForm == null)
			{
				adminBooksForm = new AdminBooksForm(this);
				adminBooksForm.Show();
			}
			else
			{
				adminBooksForm.Activate();
			}
		}

		private void AdminKeyValuesButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (adminKeyValuesForm == null)
				{
					adminKeyValuesForm = new AdminKeyValuesForm(this);
					adminKeyValuesForm.Show();
				}
				else
				{
					adminKeyValuesForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void ShellAppBar_DockingEdgeChanged(object sender, EventArgs e)
		{
			this.Size = new Size(this.Size.Width, 140);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			string dockingEdgeString = "";
			
			switch (ShellAppBar.DockingEdge)
			{
				case LogicNP.ShellObjects.DockingEdges.Top:
					dockingEdgeString = "Top";
					break;

				case LogicNP.ShellObjects.DockingEdges.Bottom:
					dockingEdgeString = "Bottom";
					break;

				default:
					dockingEdgeString = "Top";
					break;
			}

			try
			{
				secMasterOpen = secMasterForm.Visible;
			}
			catch
			{
				secMasterOpen = false;
			}

			RegistryValue.Write(this.Name, "DeviceName", ShellAppBar.DockingScreen.DeviceName);
			RegistryValue.Write(this.Name, "DockingEdge", dockingEdgeString);
			RegistryValue.Write(this.Name, "SecMasterOpen", secMasterOpen.ToString());
		}

		private void ErrorMessageButton_Click(object sender, EventArgs e)
		{		
		}

		private void InventorySubscriptionsButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (inventorySubscriptionForm == null)
				{
					inventorySubscriptionForm = new InventorySubscriptionsForm(this);
					inventorySubscriptionForm.Show();
				}
				else
				{
					inventorySubscriptionForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void TradingBoxSummaryButton_Click(object sender, EventArgs e)
		{
		}

		private void TradingContractSummaryButton_Click(object sender, EventArgs e)
		{
            try
            {
                tradingContractSummaryForm = new TradingContractSummaryForm(this);
                tradingContractSummaryForm.Show();
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
		}


		private void TradingAdvancedBorrowsButton_Click(object sender, EventArgs e)
		{
		}

		private void TradingToolBarAvailabilityForm_Click(object sender, EventArgs e)
		{
		}

		private void TradingDeskInputButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (tradingDeskInputForm == null)
				{
					tradingDeskInputForm = new TradingDeskInputForm(this);
					tradingDeskInputForm.Show();
				}
				else
				{
					tradingDeskInputForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void TradingContractBlotterButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (tradingContractBlotterForm == null)
				{
					tradingContractBlotterForm = new TradingContractBlotterForm(this);
					tradingContractBlotterForm.Show();
				}
				else
				{
					tradingContractBlotterForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void AdminUsersButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (adminUsersForm == null)
				{
					adminUsersForm = new AdminUsersForm(this);
					adminUsersForm.Show();
				}
				else
				{
					adminUsersForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void AdminFontButton_Click(object sender, EventArgs e)
		{
			MainFontDialog.ShowDialog();
		}

		public short UtcOffset
		{
			get
			{
				return (short)TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Minutes;
			}
		}

		public string UserId
		{
			get
			{
				if (Standard.ConfigValue("UseDomainInUserId", @"false").ToLower().Equals("true"))
				{
					return SystemInformation.UserDomainName.ToUpper() + @"\" + SystemInformation.UserName.ToLower();
				}
				else
				{
					return SystemInformation.UserName.ToLower();
				}
			}

            set
            {
                
            }
		}

		private void TradingFundingRatesButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (tradingFundingRatesForm == null)
				{
					tradingFundingRatesForm = new TradingFundingRatesForm(this);
					tradingFundingRatesForm.Show();
				}
				else
				{
					tradingFundingRatesForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		public string SecId
		{
			set
			{
				secMasterForm.SecId = value;
			}

			get
			{
				return secMasterForm.SecId;
			}
		}		

		private void AdminLoginButton_Click(object sender, EventArgs e)
		{
			try
			{
				AdminLoginForm adminLoginForm = new AdminLoginForm(this, UserId);
				adminLoginForm.Show();
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		public void PermissionsSet(string userId)
		{
			this.Cursor = Cursors.WaitCursor;

			// Admin security settings
			AdminRolesButton.Enabled = this.AdminAgent.MayView(UserId, "AdminRoles");
			AdminBookButton.Enabled = this.AdminAgent.MayView(UserId, "AdminBooks");
			AdminHolidayButton.Enabled = this.AdminAgent.MayView(UserId, "AdminHolidays");
			AdminKeyValuesButton.Enabled = this.AdminAgent.MayView(UserId, "AdminKeyValues");
			AdminUsersButton.Enabled = this.AdminAgent.MayView(UserId, "AdminUsers");
			AdminCurrenciesButton.Enabled = this.AdminAgent.MayView(UserId, "AdminCurrencies");
			AdminCountriesButton.Enabled = this.AdminAgent.MayView(UserId, "AdminCountries");
			AdminSecMasterButton.Enabled = this.AdminAgent.MayView(UserId, "AdminSecMaster");
			AdminSecMasterBarButton.Enabled = this.AdminAgent.MayView(UserId, "SecMaster");
            
			//Inventory
			InventoryUploadButton.Enabled = this.AdminAgent.MayView(UserId, "InventoryUpload");
			InventorySubscriptionsButton.Enabled = this.AdminAgent.MayView(UserId, "InventorySubscriptions");
			InventoryLookupButton.Enabled = this.AdminAgent.MayView(UserId, "InventoryLookup");

			//Trading
			TradingContractBlotterButton.Enabled = this.AdminAgent.MayView(UserId, "TradingContractBlotter");
			TradingContractSummaryButton.Enabled = this.AdminAgent.MayView(UserId, "TradingContractSummary");
			TradingFundingRatesButton.Enabled = this.AdminAgent.MayView(UserId, "TradingFundingRates");
            TradingBookCreditButton.Enabled = this.AdminAgent.MayView(UserId, "TradingBookCredit");
			
            //Process
			ProcessStatusButton.Enabled = this.AdminAgent.MayView(UserId, "ProcessStatus");
			ProcessMessagesButton.Enabled = this.AdminAgent.MayView(UserId, "ProcessMessages");

            //Analysis
            AnalysisReportingTradingRibbonButton.Enabled = this.AdminAgent.MayView(UserId, "TradingReporting");
            AnalysisReportingBillingRibbonButton.Enabled = this.AdminAgent.MayView(UserId, "TradingBilling");

			if (this.AdminAgent.MayView(UserId, "SecMaster") && secMasterOpen)
			{
				secMasterForm = new SecurityMasterForm(this);
				secMasterForm.Show();
			}


			splashStartupInfoForm = new SplashStartUpInfoForm(this);
			splashStartupInfoForm.Show();

			this.Cursor = Cursors.Default;
		}

		private void AdminRolesButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (adminRolesForm == null)
				{
					adminRolesForm = new AdminRolesForm(this);
					adminRolesForm.Show();
				}
				else
				{
					adminRolesForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void InventoryUploadButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (inventoryUploadForm == null)
				{
					inventoryUploadForm = new InventoryUploadForm(this);
					inventoryUploadForm.Show();
				}
				else
				{
					inventoryUploadForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void InventoryLookupButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (inventoryLookupForm == null)
				{
					inventoryLookupForm = new InventoryLookupForm(this);
					inventoryLookupForm.Show();
				}
				else
				{
					inventoryLookupForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void AdminCurrenciesButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (adminCurrenciesForm == null)
				{
					adminCurrenciesForm = new AdminCurrenciesForm(this);
					adminCurrenciesForm.Show();
				}
				else
				{
					adminCurrenciesForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void AdminCountriesButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (adminCountriesForm == null)
				{
					adminCountriesForm = new AdminCountriesForm(this);
					adminCountriesForm.Show();
				}
				else
				{
					adminCountriesForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void AdminSecMasterButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (adminSecMasterForm == null)
				{
					adminSecMasterForm = new AdminSecMasterForm(this);
					adminSecMasterForm.Show();
				}
				else
				{
					adminSecMasterForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}



		public string GridToText(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
		{
            this.Cursor = Cursors.WaitCursor;
            string gridData = "";

			try
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\r\n";

				foreach (int row in grid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
					{
						gridData += dataColumn.CellText(row) + "\t";
					}

					gridData += "\r\n";
				}

				return gridData;
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}

            this.Cursor = Cursors.Default;
            return "Sorry... Error loading the list.";
		}

        public void SendToClipboard(string data)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if ((data == "") || (data == null))
                {
                    Alert(this.Name, "You have not selected any data.");
                    return;
                }

                Clipboard.SetDataObject(data, true);
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
            this.Cursor = Cursors.Default;
        }
        
        public void SendToClipboard(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
        {
            this.Cursor = Cursors.WaitCursor;
            string data = "";

            try
            {
                if (grid.Focused)
                {
                    data = GridToText(ref grid);
                }

                Clipboard.SetDataObject(data, true);
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
            this.Cursor = Cursors.Default;
        }
        
        public void SendToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid, bool saveDialog)
        {
            this.Cursor = Cursors.WaitCursor;

            string fileName = "";

			try
            {
                if (saveDialog)
                {
                    SaveFileDialog.DefaultExt = "xls";
                    SaveFileDialog.ShowDialog();
                    fileName = SaveFileDialog.FileName;
                }
                else
                {
                    fileName = tempPath + Standard.ProcessId() + ".xls";
                }

				Excel.ExportGridToExcel(ref grid, fileName, 0, null);
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        public void SendToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid, int split, bool saveDialog)
        {
            this.Cursor = Cursors.WaitCursor;

            string fileName = "";

            try
            {
                if (saveDialog)
                {
                    SaveFileDialog.DefaultExt = "xls";
                    SaveFileDialog.ShowDialog();
                    fileName = SaveFileDialog.FileName;
                }
                else
                {
                    fileName = tempPath + Standard.ProcessId() + ".xls";
                }

                Excel.ExportGridToExcel(ref grid, fileName, split, null);
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
            this.Cursor = Cursors.Default;
        }

		public void SendToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid, int split, Dictionary<string, ExcelCellStyle> excelCellStyleDictionary)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				SaveFileDialog.DefaultExt = "xls";
				SaveFileDialog.ShowDialog();
				Excel.ExportGridToExcel(ref grid, SaveFileDialog.FileName, split, excelCellStyleDictionary);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToExcel]", 1);
			}
			this.Cursor = Cursors.Default;
		}


        public void SendToEmail(string body)
        {
            SendToEmail("", "", "", body, "");
        }

        public void SendToEmail(string recipientList, string ccList, string subject, string body, string attachmentList)
        {
            try
            {
                Email email = new Email();
                email.Send(recipientList, ccList, subject, body, attachmentList);
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }

        }

        public void SendToEmail(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
        {
            int textLength;
            int[] maxTextLength;
            int columnIndex = -1;
            string gridData = "\n\n";

            if (grid.SelectedCols.Count.Equals(0))
            {
                Alert(this.Name, "You have not selected any rows to send.");
                return;
            }

            try
            {
                maxTextLength = new int[grid.SelectedCols.Count];
                
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                {
                    maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
                }

                foreach (int rowIndex in grid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                    {
                        if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
                        {
                            maxTextLength[columnIndex] = textLength;
                        }
                    }
                }

                columnIndex = -1;

                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                {
                    gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
                }
                gridData += "\n";

                columnIndex = -1;

                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                {
                    gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
                }
                gridData += "\n";

                foreach (int rowIndex in grid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                    {
                        if (dataColumn.Value.GetType().Equals(typeof(System.String)))
                        {
							gridData += dataColumn.CellText(rowIndex).Trim().PadRight(maxTextLength[++columnIndex] + 2);	
                        }
                        else
                        {
                            gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
                        }
                    }

                    gridData += "\n";
                }

                Email email = new Email();
                email.Send("", "", grid.Text + " _ " + DateTime.Parse(ServiceAgent.BizDate()).ToString("yyyy-MM-dd"), gridData, null);

            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
        }

		private void ProcessStatusButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (processStatusForm == null)
				{
					processStatusForm = new ProcessStatusForm(this);
					processStatusForm.Show();
				}
				else
				{
					processStatusForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

		private void ProcessMessagesButton_Click(object sender, EventArgs e)
		{			
		}

		private void AdminSecMasterBarButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (secMasterForm == null)
				{
					secMasterForm = new SecurityMasterForm(this);
					secMasterForm.Show();
				}
				else
				{
					secMasterForm.Activate();
				}
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}

        private void HelpAboutRibbonButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (helpAboutForm == null)
                {
                    helpAboutForm = new HelpAboutForm(this);
                    helpAboutForm.Show();
                }
                else
                {
                    helpAboutForm.Activate();
                }
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
        }

        private void HelpStartupForm_Click(object sender, EventArgs e)
        {
            try
            {
                if (splashStartupInfoForm == null)
                {
                    splashStartupInfoForm = new SplashStartUpInfoForm(this);
                    splashStartupInfoForm.Show();
                }
                else
                {
                    splashStartupInfoForm.Activate();
                }
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
        }

        public string Format(string dataField, string value)
        {
            string result = "";

            switch (dataField)
            {
                case ("ContractType"):
                    if (value.Equals("B"))
                    {
                        result = "Borrow";
                    }
                    else
                    {
                        result = "Loan";
                    }
                    break;

                case ("Quantity"):
                case ("QuantitySettled"):
                case ("QuantityRecalled"):
                case ("LoanQuantity"):
                case ("BorrowQuantity"):
                case ("NetQuantity"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.Collateral);
                    }
                    catch { }
                    break;

                case ("BizDate"):
                case ("OpenDate"):
                case ("OpenDateTime"):
                case ("MoveToDate"):
                case ("BuyInDate"):
                case ("SettleDate"):
                case ("SettleDateProjected"):
                case ("SettleDateActual"):
                case ("ValueDate"):
                case ("TermDate"):
                    try
                    {
                        result = DateTime.Parse(value).ToString(Standard.DateFormat);
                    }
                    catch { }
                    break;

                case ("ActTime"):
                    try
                    {
                        result = DateTime.Parse(value).ToString(Standard.DateTimeFormat);
                    }
                    catch { }
                    break;

                case ("FeeAmount"):                
                case ("Amount"):
                case ("SettledCash"):
                case ("UnsettledCash"):
                case ("BorrowCashPool"):
                case ("LoanCashPool"):
                case ("BorrowRebate"):
                case ("LoanRebate"):
                case ("TotalRebate"):
                case ("MarkAmount"):
                case ("DebitMarks"):
                case ("CreditMarks"):
                case ("PL"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.MarkCash);
                    }
                    catch { }
                    break;

                case ("RebateAmount"):
                case ("BorrowsPrior"):
                case ("LoansPrior"):
                case ("Debit"):
                case ("Credit"):
                case ("NewBorrows"):
                case ("NewLoans"):
                case ("CreditReturns"):
                case ("DebitReturns"):
                case ("DebitTotal"):
                case ("CreditTotal"):
                case ("BorrowsDiff"):
                case ("LoansDiff"):
                case ("Total"):
                case ("Borrows"):
                case ("Loans"):
                case ("AmountSettled"):
                case ("ContractAmount"):
                case ("NetAmount"):
                case ("BorrowAmount"):
                case ("BorrowLimitAmount"):
                case ("LoanLimitAmount"):
                case ("NewContractAmount"):
                case ("LoanAmount"):
                case ("CashInBalance"):
                case ("CashInRecieveable"):
                case ("CashOutBalance"):
                case ("CashOutPayable"):
                case ("CashNet"):
                case ("CashNetReceivePayable"):
                case ("Income"):
                case ("Value"):
                case ("BorrowValue"):
                case ("LoanValue"):
                case ("TotalValue"):
                case ("CashReturn"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.Cash);
                    }
                    catch { }
                    break;

                case ("Margin"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.Margin);
                    }
                    catch { }
                    break;

                case ("Price"):
                case ("MarkPrice"):
                case ("CurrentPrice"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.MarkPrice);
                    }
                    catch { }
                    break;

                case ("Rate"):                
                case ("BorrowRate"):
                case ("LoanRate"):
                case ("FeeRate"):
                case ("RebateRate"):
                case ("ValueRatio"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.Rate);
                    }
                    catch { }
                    break;

                case ("FundingRate"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.FundingRate);
                    }
                    catch { }
                    break;

                case ("DivRate"):
                    try
                    {
                        result = decimal.Parse(value).ToString(Formats.DividendRate);
                    }
                    catch { }
                    break;

                default:
                    result = value;
                    break;
            }

            return result;
        }

        private void TradingBookCreditButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (tradingBookCreditForm == null)
                {
                    tradingBookCreditForm = new TradingBookCreditForm(this);
                    tradingBookCreditForm.Show();
                }
                else
                {
                    tradingBookCreditForm.Activate();
                }
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
        }

        private void AnalysisReportingTradingRibbonButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (tradingReportingForm == null)
                {
                    tradingReportingForm = new TradingReportingForm(this);
                    tradingReportingForm.Show();
                }
                else
                {
                    tradingReportingForm.Activate();
                }
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
        }

        private void AnalysisReportingBillingRibbonButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (tradingBillingForm == null)
                {
                    tradingBillingForm = new TradingReportingBillingForm(this);
                    tradingBillingForm.Show();
                }
                else
                {
                    tradingBillingForm.Activate();
                }
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
        }

        private void TradingCashManagementButton_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception error)
            {
                Alert(this.Name, error.Message);
            }
        }
	}
}
