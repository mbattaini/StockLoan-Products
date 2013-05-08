using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{

    public class ProcessStatusDropdownForm : C1.Win.C1Input.DropDownForm
    {
        private MainForm mainForm = null;

        private DataSet dataSet;
        private DataView dataView;

        private bool isReady = false;

        delegate void setTextOwnerControl(string text, bool newString);
        delegate void refreshOwnerControl();

        private ArrayList processStatusEventArgsArray;

        private ProcessStatusEventWrapper processStatusEventWrapper;
        private ProcessStatusEventHandler processStatusEventHandler;

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ProcessStatusGrid;
        private System.Windows.Forms.MenuItem ShowMenuItem;
        private System.Windows.Forms.MenuItem ShowQuantityMenuItem;
        private System.Windows.Forms.MenuItem ShowAmountMenuItem;
        private System.Windows.Forms.MenuItem ShowContractIdMenuItem;
        private System.Windows.Forms.MenuItem ShowBookMenuItem;
        private System.Windows.Forms.MenuItem ShowSecurityIdMenuItem;
        private System.Windows.Forms.MenuItem ShowSymbolMenuItem;
        private System.Windows.Forms.MenuItem ShowSystemCodeMenuItem;
        private System.Windows.Forms.ContextMenu MainContextMenu;

        private System.ComponentModel.Container components = null;

        public ProcessStatusDropdownForm()
        {
            InitializeComponent();

            ProcessStatusGrid.Splits[0, 0].DisplayColumns["Quantity"].Visible = false;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["Amount"].Visible = false;
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProcessStatusDropdownForm));
            this.ProcessStatusGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.MainContextMenu = new System.Windows.Forms.ContextMenu();
            this.ShowMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowSystemCodeMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowContractIdMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowBookMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowSecurityIdMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowSymbolMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowQuantityMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowAmountMenuItem = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessStatusGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ProcessStatusGrid
            // 
            this.ProcessStatusGrid.AllowColSelect = false;
            this.ProcessStatusGrid.AllowFilter = false;
            this.ProcessStatusGrid.AllowRowSelect = false;
            this.ProcessStatusGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.ProcessStatusGrid.AllowUpdate = false;
            this.ProcessStatusGrid.AllowUpdateOnBlur = false;
            this.ProcessStatusGrid.AlternatingRows = true;
            this.ProcessStatusGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProcessStatusGrid.CaptionHeight = 17;
            this.ProcessStatusGrid.ContextMenu = this.MainContextMenu;
            this.ProcessStatusGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.ProcessStatusGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessStatusGrid.EmptyRows = true;
            this.ProcessStatusGrid.ExtendRightColumn = true;
            this.ProcessStatusGrid.FilterBar = true;
            this.ProcessStatusGrid.GroupByAreaVisible = false;
            this.ProcessStatusGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.ProcessStatusGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ProcessStatusGrid.Location = new System.Drawing.Point(0, 0);
            this.ProcessStatusGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.NoMarquee;
            this.ProcessStatusGrid.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.Simple;
            this.ProcessStatusGrid.Name = "ProcessStatusGrid";
            this.ProcessStatusGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ProcessStatusGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ProcessStatusGrid.PreviewInfo.ZoomFactor = 75;
            this.ProcessStatusGrid.RecordSelectors = false;
            this.ProcessStatusGrid.RecordSelectorWidth = 16;
            this.ProcessStatusGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.ProcessStatusGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.ProcessStatusGrid.RowHeight = 15;
            this.ProcessStatusGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.ProcessStatusGrid.Size = new System.Drawing.Size(1148, 176);
            this.ProcessStatusGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.ProcessStatusGrid.TabIndex = 1;
            this.ProcessStatusGrid.Text = "ProcessStatusGrid";
            this.ProcessStatusGrid.WrapCellPointer = true;
            this.ProcessStatusGrid.FilterChange += new System.EventHandler(this.ProcessStatusGrid_FilterChange);
            this.ProcessStatusGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ProcessStatusGrid_FormatText);
            this.ProcessStatusGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
              "ield=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
              "ption=\"Security ID\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn>" +
              "<C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><Group" +
              "Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Status\" DataField=\"Status" +
              "\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Typ" +
              "e\" DataField=\"ActCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
              "evel=\"0\" Caption=\"Action\" DataField=\"Act\"><ValueItems /><GroupInfo /></C1DataCol" +
              "umn><C1DataColumn Level=\"0\" Caption=\"S\" DataField=\"SystemCode\"><ValueItems /><Gr" +
              "oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Book Group\" DataField=" +
              "\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
              "ption=\"Act At\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /" +
              "><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act User\" DataFiel" +
              "d=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
              "el=\"0\" Caption=\"E\" DataField=\"HasError\"><ValueItems Presentation=\"CheckBox\" /><G" +
              "roupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Contract ID\" DataFiel" +
              "d=\"ContractId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
              " Caption=\"T\" DataField=\"ContractType\"><ValueItems /><GroupInfo /></C1DataColumn>" +
              "<C1DataColumn Level=\"0\" Caption=\"Status At\" DataField=\"StatusTime\" NumberFormat=" +
              "\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
              "=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><Va" +
              "lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Amount\" " +
              "DataField=\"Amount\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></" +
              "C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"" +
              "><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style85{}Inacti" +
              "ve{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style100{AlignHorz:C" +
              "enter;}Style78{}Style79{}Selected{ForeColor:HighlightText;BackColor:Highlight;}S" +
              "tyle73{}Style72{}Style110{}Style70{AlignHorz:Near;}Style71{AlignHorz:Far;}Style7" +
              "6{AlignHorz:Near;}Style77{AlignHorz:Near;}Style74{}Style75{}Style84{}Style87{}St" +
              "yle86{}Style81{}Style80{}Style83{AlignHorz:Far;}Style82{AlignHorz:Near;}Footer{}" +
              "Style89{AlignHorz:Center;}Style88{AlignHorz:Center;}Style108{}Style109{}Style104" +
              "{}Style105{}Style106{AlignHorz:Near;}Style107{AlignHorz:Far;}Editor{}Style101{Al" +
              "ignHorz:Center;}Style102{}Style103{}Style94{AlignHorz:Near;}Style95{AlignHorz:Ne" +
              "ar;}Style96{}Style97{}Style90{}Style91{}Style92{}Style93{}RecordSelector{AlignIm" +
              "age:Center;}Style98{}Style99{}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
              "1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19{}Style14{}S" +
              "tyle15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}" +
              "Style11{}Style12{}Style13{}Style27{}Style28{AlignHorz:Near;}Style9{}Style26{}Sty" +
              "le25{}Style29{AlignHorz:Near;}Style5{}Style4{}Style7{}Style6{}Style24{}Style23{A" +
              "lignHorz:Near;}Style3{}Style22{AlignHorz:Near;}Style21{}Style20{}OddRow{}Style38" +
              "{}Style39{}Style36{}FilterBar{BackColor:SeaShell;}Style37{}Style34{AlignHorz:Nea" +
              "r;}Style35{AlignHorz:Near;}Style32{}Style30{}Style49{}Style48{}Style31{}Style33{" +
              "}Normal{Font:Verdana, 8.25pt;}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;}St" +
              "yle43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Center;}Style46{AlignHorz:Ne" +
              "ar;}EvenRow{BackColor:LightCyan;}Style8{}Style58{AlignHorz:Center;}Style59{Align" +
              "Horz:Center;}Style2{}Style50{}Style51{}Style52{AlignHorz:Near;}Style53{AlignHorz" +
              ":Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}St" +
              "yle68{}Style1{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{Ali" +
              "gnHorz:Near;}Style64{AlignHorz:Near;}Style111{}Group{BackColor:ControlDark;Borde" +
              "r:None,,0, 0, 0, 0;AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGri" +
              "d.MergeView VBarStyle=\"Always\" AllowColSelect=\"False\" AllowRowSelect=\"False\" Nam" +
              "e=\"\" AllowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" ColumnC" +
              "aptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"Tr" +
              "ue\" MarqueeStyle=\"NoMarquee\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" Record" +
              "Selectors=\"False\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyl" +
              "e parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><Eve" +
              "nRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"" +
              "Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\"" +
              " me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle " +
              "parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\"" +
              " /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"Recor" +
              "dSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style " +
              "parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle paren" +
              "t=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle par" +
              "ent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHe" +
              "aderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
              "tyle62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><" +
              "Width>20</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayC" +
              "olumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"St" +
              "yle47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5" +
              "\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterSt" +
              "yle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGra" +
              "y,Single</ColumnDivider><Width>35</Width><Height>15</Height><DCIdx>4</DCIdx></C1" +
              "DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Sty" +
              "le parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><E" +
              "ditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"" +
              "Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visib" +
              "le><ColumnDivider>DarkGray,Single</ColumnDivider><Width>300</Width><Height>15</H" +
              "eight><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
              "tyle2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=" +
              "\"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeader" +
              "Style parent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
              "74\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Widt" +
              "h>65</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColum" +
              "n><HeadingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style7" +
              "7\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me" +
              "=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle " +
              "parent=\"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Si" +
              "ngle</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>9</DCIdx></C1Disp" +
              "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style88\" /><Style p" +
              "arent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" me=\"Style90\" /><Edito" +
              "rStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
              "e93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><Visible>True</Visible><" +
              "ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</Height" +
              "><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
              "2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Sty" +
              "le3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyl" +
              "e parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" " +
              "/><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>80" +
              "</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
              "eadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Style1\" me=\"Style95\" /" +
              "><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle parent=\"Style5\" me=\"St" +
              "yle97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><GroupFooterStyle pare" +
              "nt=\"Style1\" me=\"Style98\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
              "</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>11</DCIdx></C1Display" +
              "Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style100\" /><Style par" +
              "ent=\"Style1\" me=\"Style101\" /><FooterStyle parent=\"Style3\" me=\"Style102\" /><Edito" +
              "rStyle parent=\"Style5\" me=\"Style103\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
              "le105\" /><GroupFooterStyle parent=\"Style1\" me=\"Style104\" /><Visible>True</Visibl" +
              "e><ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Width><Height>15</Hei" +
              "ght><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
              "yle2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"" +
              "Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderS" +
              "tyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style2" +
              "0\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
              ">80</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn" +
              "><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23" +
              "\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=" +
              "\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle p" +
              "arent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sin" +
              "gle</ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>1</DCIdx></C1Displ" +
              "ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style pa" +
              "rent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><Editor" +
              "Style parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
              "33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><C" +
              "olumnDivider>DarkGray,Single</ColumnDivider><Width>65</Width><Height>15</Height>" +
              "<DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
              " me=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style" +
              "3\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle " +
              "parent=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" />" +
              "<ColumnDivider>DarkGray,Single</ColumnDivider><Width>118</Width><Height>15</Heig" +
              "ht><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
              "le2\" me=\"Style106\" /><Style parent=\"Style1\" me=\"Style107\" /><FooterStyle parent=" +
              "\"Style3\" me=\"Style108\" /><EditorStyle parent=\"Style5\" me=\"Style109\" /><GroupHead" +
              "erStyle parent=\"Style1\" me=\"Style111\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
              "yle110\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCId" +
              "x>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
              "\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" m" +
              "e=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle pare" +
              "nt=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Vis" +
              "ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>65</Widt" +
              "h><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
              "gStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><Foo" +
              "terStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37" +
              "\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"S" +
              "tyle1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Col" +
              "umnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCols><" +
              "ClientRect>0, 0, 1146, 174</ClientRect><BorderSide>0</BorderSide></C1.Win.C1True" +
              "DBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style par" +
              "ent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=" +
              "\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"" +
              "Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Norm" +
              "al\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Nor" +
              "mal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"" +
              "Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><ver" +
              "tSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Defaul" +
              "tRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1146, 174</ClientArea><Pri" +
              "ntPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"S" +
              "tyle15\" /></Blob>";
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.ShowMenuItem});
            // 
            // ShowMenuItem
            // 
            this.ShowMenuItem.Index = 0;
            this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                 this.ShowSystemCodeMenuItem,
                                                                                 this.ShowContractIdMenuItem,
                                                                                 this.ShowBookMenuItem,
                                                                                 this.ShowSecurityIdMenuItem,
                                                                                 this.ShowSymbolMenuItem,
                                                                                 this.ShowQuantityMenuItem,
                                                                                 this.ShowAmountMenuItem});
            this.ShowMenuItem.Text = "Show";
            // 
            // ShowSystemCodeMenuItem
            // 
            this.ShowSystemCodeMenuItem.Checked = true;
            this.ShowSystemCodeMenuItem.Index = 0;
            this.ShowSystemCodeMenuItem.Text = "System Code";
            this.ShowSystemCodeMenuItem.Click += new System.EventHandler(this.ShowSystemCodeMenuItem_Click);
            // 
            // ShowContractIdMenuItem
            // 
            this.ShowContractIdMenuItem.Checked = true;
            this.ShowContractIdMenuItem.Index = 1;
            this.ShowContractIdMenuItem.Text = "Contract ID";
            this.ShowContractIdMenuItem.Click += new System.EventHandler(this.ShowContractIdMenuItem_Click);
            // 
            // ShowBookMenuItem
            // 
            this.ShowBookMenuItem.Checked = true;
            this.ShowBookMenuItem.Index = 2;
            this.ShowBookMenuItem.Text = "Book";
            this.ShowBookMenuItem.Click += new System.EventHandler(this.ShowBookMenuItem_Click);
            // 
            // ShowSecurityIdMenuItem
            // 
            this.ShowSecurityIdMenuItem.Checked = true;
            this.ShowSecurityIdMenuItem.Index = 3;
            this.ShowSecurityIdMenuItem.Text = "Security ID";
            this.ShowSecurityIdMenuItem.Click += new System.EventHandler(this.ShowSecurityIdMenuItem_Click);
            // 
            // ShowSymbolMenuItem
            // 
            this.ShowSymbolMenuItem.Checked = true;
            this.ShowSymbolMenuItem.Index = 4;
            this.ShowSymbolMenuItem.Text = "Symbol";
            this.ShowSymbolMenuItem.Click += new System.EventHandler(this.ShowSymbolMenuItem_Click);
            // 
            // ShowQuantityMenuItem
            // 
            this.ShowQuantityMenuItem.Index = 5;
            this.ShowQuantityMenuItem.Text = "Quantity";
            this.ShowQuantityMenuItem.Click += new System.EventHandler(this.ShowQuantityMenuItem_Click);
            // 
            // ShowAmountMenuItem
            // 
            this.ShowAmountMenuItem.Index = 6;
            this.ShowAmountMenuItem.Text = "Amount";
            this.ShowAmountMenuItem.Click += new System.EventHandler(this.ShowAmountMenuItem_Click);
            // 
            // ProcessStatusDropdownForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1148, 176);
            this.Controls.Add(this.ProcessStatusGrid);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Name = "ProcessStatusDropdownForm";
            this.Options = C1.Win.C1Input.DropDownFormOptionsFlags.Focusable;
            this.Text = "ProcessStatusDropdownForm";
            this.Open += new System.EventHandler(this.ProcessStatusDropdownForm_Open);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProcessStatusDropdownForm_Closing);
            this.Load += new System.EventHandler(this.ProcessStatusDropdownForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProcessStatusGrid)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private bool IsReady
        {
            get
            {
                return isReady;
            }

            set
            {
                ProcessStatusEventArgs processStatusEventArgs;

                try
                {
                    lock (this)
                    {
                        if (value && (processStatusEventArgsArray.Count > 0))
                        {
                            isReady = false;

                            processStatusEventArgs = (ProcessStatusEventArgs)processStatusEventArgsArray[0];
                            processStatusEventArgsArray.RemoveAt(0);

                            processStatusEventHandler.BeginInvoke(processStatusEventArgs, null, null);
                        }
                        else
                        {
                            isReady = value;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [ProcessStatusDropDownForm.IsReady(set)]", Log.Error, 1);
                }
            }
        }

        public void ProcessStatusOnEvent(ProcessStatusEventArgs processStatusEventArgs)
        {
            lock (this)
            {
                processStatusEventArgsArray.Add(processStatusEventArgs);

                if (this.IsReady) // Force reset to trigger handling of event.
                {
                    this.IsReady = true;
                }
            }
        }


        private void OwnerControlSetText(string text, bool newString)
        {
            if (OwnerControl.InvokeRequired)
            {
                setTextOwnerControl ownerControlText = new setTextOwnerControl(OwnerControlSetText);
                this.OwnerControl.Invoke(ownerControlText, new object[] { text, newString });
            }
            else
            {
                if (newString)
                {
                    this.OwnerControl.Text = text;
                }
                else
                {
                    this.OwnerControl.Text += text;
                }
            }
        }

        private void OwnerControlRefresh()
        {
            if (OwnerControl.InvokeRequired)
            {
                refreshOwnerControl refresh = new refreshOwnerControl(OwnerControlRefresh);
                this.OwnerControl.Invoke(refresh);
            }
            else
            {
                this.OwnerControl.Refresh();
            }
        }

        private void ProcessStatusDoEvent(ProcessStatusEventArgs processStatusEventArgs)
        {
            //this.OwnerControl.Text = processStatusEventArgs.BookGroup + " : " + processStatusEventArgs.Act;
            OwnerControlSetText(processStatusEventArgs.BookGroup + " : " + processStatusEventArgs.Act, true);
            
            if (!processStatusEventArgs.Book.Length.Equals(0))
            {
                OwnerControlSetText(" with " + processStatusEventArgs.Book, false);
                //this.OwnerControl.Text += " with " + processStatusEventArgs.Book;
            }

            if (!processStatusEventArgs.SecId.Length.Equals(0))
            {
                OwnerControlSetText(" for " + processStatusEventArgs.SecId, false);
                //this.OwnerControl.Text += " for " + processStatusEventArgs.SecId;
            }

            if (!processStatusEventArgs.Symbol.Length.Equals(0))
            {
                OwnerControlSetText( "[" + processStatusEventArgs.Symbol + "]", false);
                //this.OwnerControl.Text += " [" + processStatusEventArgs.Symbol + "]";
            }

            if (processStatusEventArgs.HasError)
            {
                this.OwnerControl.ForeColor = Color.Maroon;
            }
            else
            {
                this.OwnerControl.ForeColor = Color.MidnightBlue;
            }

            OwnerControlRefresh();
            
            //this.OwnerControl.Refresh();

            try
            {
                lock (this)
                {
                    dataSet.Tables["ProcessStatus"].BeginLoadData();

                    processStatusEventArgs.UtcOffset = mainForm.UtcOffset;
                    dataSet.Tables["ProcessStatus"].LoadDataRow(processStatusEventArgs.Values, true);

                    dataSet.Tables["ProcessStatus"].EndLoadData();

                    ProcessStatusGrid.Row = 0;
                    IsReady = true;

                    if (processStatusEventArgs.HasError && (mainForm.positionOpenContractsForm != null))
                    {
                        mainForm.positionOpenContractsForm.StatusFlagSet(
                            processStatusEventArgs.BookGroup,
                            processStatusEventArgs.ContractId,
                            processStatusEventArgs.ContractType,
                            "E");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ProcessStatusDropDownForm.ProcessStatusDoEvent]", Log.Error, 1);
            }
        }

        private void ProcessStatusDropdownForm_Load(object sender, System.EventArgs e)
        {
            processStatusEventArgsArray = new ArrayList();

            processStatusEventWrapper = new ProcessStatusEventWrapper();
            processStatusEventWrapper.ProcessStatusEvent += new ProcessStatusEventHandler(ProcessStatusOnEvent);

            processStatusEventHandler = new ProcessStatusEventHandler(ProcessStatusDoEvent);


            try
            {
                mainForm.Alert("Please wait... Loading current process status data...", PilotState.Unknown);

                mainForm.PositionAgent.ProcessStatusEvent += new ProcessStatusEventHandler(processStatusEventWrapper.DoEvent);
                dataSet = mainForm.ServiceAgent.ProcessStatusGet(mainForm.UtcOffset);

                dataView = new DataView(dataSet.Tables["ProcessStatus"]);
                dataView.Sort = "ActTime DESC";

                ProcessStatusGrid.SetDataBinding(dataView, null, true);

                mainForm.Alert("Loading current process status data... Done!", PilotState.Normal);
                this.IsReady = true;

                this.OwnerControl.BackColor = SystemColors.Window;
            }
            catch (Exception ee)
            {
                mainForm.Alert(ee.Message, PilotState.RunFault);
                Log.Write(ee.Message + " [ProcessStatusDropDownForm.MainProcessStatusForm_Load]", Log.Error, 1);
            }
        }

        private void ProcessStatusDropdownForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.IsReady = false;
                mainForm.PositionAgent.ProcessStatusEvent -= new ProcessStatusEventHandler(processStatusEventWrapper.DoEvent);
            }
            catch { }
        }

        private void ProcessStatusDropdownForm_Open(object sender, System.EventArgs e)
        {
            if (mainForm == null)
            {
                mainForm = (MainForm)this.OwnerControl.Parent.Parent;
            }

            this.Height = mainForm.ClientRectangle.Height / 3;
            this.Width = mainForm.ClientRectangle.Width - 2;
        }

        private void ProcessStatusGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Equals(""))
            {
                return;
            }

            switch (ProcessStatusGrid.Columns[e.ColIndex].DataField)
            {
                case ("ActTime"):
                case ("StatusTime"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
                    }
                    catch { }
                    break;

                case ("Quantity"):
                    try
                    {
                        e.Value = long.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;

                case ("Amount"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
                    }
                    catch { }
                    break;
            }
        }

        private void ProcessStatusGrid_FilterChange(object sender, System.EventArgs e)
        {
            try
            {
                dataView.RowFilter = mainForm.GridFilterGet(ref ProcessStatusGrid);
            }
            catch (Exception ee)
            {
                mainForm.Alert(ee.Message, PilotState.RunFault);
            }
        }

        private void ShowSystemCodeMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowSystemCodeMenuItem.Checked = !ShowSystemCodeMenuItem.Checked;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["SystemCode"].Visible = ShowSystemCodeMenuItem.Checked;
        }

        private void ShowContractIdMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowContractIdMenuItem.Checked = !ShowContractIdMenuItem.Checked;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["ContractId"].Visible = ShowContractIdMenuItem.Checked;
        }

        private void ShowBookMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowBookMenuItem.Checked = !ShowBookMenuItem.Checked;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["Book"].Visible = ShowBookMenuItem.Checked;
        }

        private void ShowSecurityIdMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowSecurityIdMenuItem.Checked = !ShowSecurityIdMenuItem.Checked;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["SecId"].Visible = ShowSecurityIdMenuItem.Checked;
        }

        private void ShowSymbolMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowSymbolMenuItem.Checked = !ShowSymbolMenuItem.Checked;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["Symbol"].Visible = ShowSymbolMenuItem.Checked;
        }

        private void ShowQuantityMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowQuantityMenuItem.Checked = !ShowQuantityMenuItem.Checked;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["Quantity"].Visible = ShowQuantityMenuItem.Checked;
        }

        private void ShowAmountMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowAmountMenuItem.Checked = !ShowAmountMenuItem.Checked;
            ProcessStatusGrid.Splits[0, 0].DisplayColumns["Amount"].Visible = ShowAmountMenuItem.Checked;
        }
    }
}
