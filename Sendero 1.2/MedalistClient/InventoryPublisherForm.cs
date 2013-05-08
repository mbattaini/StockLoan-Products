using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;
using Anetics.Common;
using Anetics.Medalist;

namespace Anetics.Medalist
{
  public class InventoryPublisherForm : System.Windows.Forms.Form
  {
    private MainForm mainForm;
    private System.Windows.Forms.CheckBox UsePgpCheck;
    private C1.Win.C1TrueDBGrid.C1TrueDBDropdown DeskDropDown;
    private C1.Win.C1Input.C1TextBox FilePathTextBox;
    private C1.Win.C1Input.C1TextBox FileHostTextBox;
    private C1.Win.C1Input.C1TextBox FileUserNameTextBox;
    private C1.Win.C1Input.C1TextBox FilePasswordTextBox;
    private C1.Win.C1Input.C1TextBox LoadExPgpTextBox;
    private C1.Win.C1Input.C1TextBox CommentTextBox;
    private C1.Win.C1Input.C1TextBox MailAddressTextBox;
    private C1.Win.C1Input.C1TextBox MailSubjectTextBox;
    private System.Windows.Forms.Button SaveChangesButton;
    private C1.Win.C1Input.C1Label FilePathLabel;
    private C1.Win.C1Input.C1Label FileHostLabel;
    private C1.Win.C1Input.C1Label FileUsernameLabel;
    private C1.Win.C1Input.C1Label FilePasswordLabel;
    private C1.Win.C1Input.C1Label MailAddressLabel;
    private C1.Win.C1Input.C1Label CommentLabel;
    private C1.Win.C1Input.C1Label UsePgpLabel;
    private C1.Win.C1Input.C1Label MailSubjectLabel;
    private C1.Win.C1Input.C1Label LastUpdateLabel;
    private C1.Win.C1Input.C1Label LastUpdateInfoLabel;
    private C1.Win.C1Input.C1Label LoadExPgpLabel;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid PublisherGrid;
    
    private System.ComponentModel.Container components = null;
    
    public InventoryPublisherForm(MainForm mainForm)
    {
      InitializeComponent();

      this.mainForm = mainForm;
      bool Editable = mainForm.AdminAgent.MayEdit(mainForm.UserId, "InventoryPublisher");

      try
      {
        FilePathTextBox.ReadOnly      = !Editable;
        FileHostTextBox.ReadOnly      = !Editable;
        FileUserNameTextBox.ReadOnly  = !Editable;
        FilePasswordTextBox.ReadOnly  = !Editable;
        LoadExPgpTextBox.ReadOnly     = !Editable;
        CommentTextBox.ReadOnly       = !Editable;
        MailAddressTextBox.ReadOnly   = !Editable;
        MailSubjectTextBox.ReadOnly   = !Editable;
        SaveChangesButton.Visible     = Editable;
        PublisherGrid.AllowAddNew     = Editable;
        PublisherGrid.AllowUpdate     = Editable;

        if(!Editable)
        {
          FilePasswordTextBox.PasswordChar = '*';
        }
      }
      catch(Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
      }
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InventoryPublisherForm));
      this.PublisherGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
      this.SaveChangesButton = new System.Windows.Forms.Button();
      this.UsePgpCheck = new System.Windows.Forms.CheckBox();
      this.DeskDropDown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
      this.FilePathTextBox = new C1.Win.C1Input.C1TextBox();
      this.FileHostTextBox = new C1.Win.C1Input.C1TextBox();
      this.FileUserNameTextBox = new C1.Win.C1Input.C1TextBox();
      this.FilePasswordTextBox = new C1.Win.C1Input.C1TextBox();
      this.LoadExPgpTextBox = new C1.Win.C1Input.C1TextBox();
      this.CommentTextBox = new C1.Win.C1Input.C1TextBox();
      this.MailAddressTextBox = new C1.Win.C1Input.C1TextBox();
      this.MailSubjectTextBox = new C1.Win.C1Input.C1TextBox();
      this.FilePathLabel = new C1.Win.C1Input.C1Label();
      this.FileHostLabel = new C1.Win.C1Input.C1Label();
      this.FileUsernameLabel = new C1.Win.C1Input.C1Label();
      this.FilePasswordLabel = new C1.Win.C1Input.C1Label();
      this.MailAddressLabel = new C1.Win.C1Input.C1Label();
      this.LoadExPgpLabel = new C1.Win.C1Input.C1Label();
      this.CommentLabel = new C1.Win.C1Input.C1Label();
      this.UsePgpLabel = new C1.Win.C1Input.C1Label();
      this.MailSubjectLabel = new C1.Win.C1Input.C1Label();
      this.LastUpdateLabel = new C1.Win.C1Input.C1Label();
      this.LastUpdateInfoLabel = new C1.Win.C1Input.C1Label();
      ((System.ComponentModel.ISupportInitialize)(this.PublisherGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DeskDropDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePathTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileHostTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileUserNameTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePasswordTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.LoadExPgpTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailAddressTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailSubjectTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePathLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileHostLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileUsernameLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePasswordLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailAddressLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.LoadExPgpLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.UsePgpLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailSubjectLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.LastUpdateLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.LastUpdateInfoLabel)).BeginInit();
      this.SuspendLayout();
      // 
      // PublisherGrid
      // 
      this.PublisherGrid.AllowColMove = false;
      this.PublisherGrid.AllowColSelect = false;
      this.PublisherGrid.AllowDelete = true;
      this.PublisherGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
      this.PublisherGrid.CaptionHeight = 18;
      this.PublisherGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
      this.PublisherGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PublisherGrid.EmptyRows = true;
      this.PublisherGrid.ExtendRightColumn = true;
      this.PublisherGrid.GroupByAreaVisible = false;
      this.PublisherGrid.GroupByCaption = "Drag a column header here to group by that column";
      this.PublisherGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
      this.PublisherGrid.Location = new System.Drawing.Point(0, 0);
      this.PublisherGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
      this.PublisherGrid.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None;
      this.PublisherGrid.Name = "PublisherGrid";
      this.PublisherGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
      this.PublisherGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
      this.PublisherGrid.PreviewInfo.ZoomFactor = 75;
      this.PublisherGrid.RecordSelectorWidth = 16;
      this.PublisherGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
      this.PublisherGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
      this.PublisherGrid.RowHeight = 15;
      this.PublisherGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.PublisherGrid.Size = new System.Drawing.Size(1000, 293);
      this.PublisherGrid.TabAcrossSplits = true;
      this.PublisherGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
      this.PublisherGrid.TabIndex = 0;
      this.PublisherGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.PublisherGrid_RowColChange);
      this.PublisherGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.PublisherGrid_BeforeColEdit);
      this.PublisherGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.PublisherGrid_BeforeDelete);
      this.PublisherGrid.OnAddNew += new System.EventHandler(this.PublisherGrid_OnAddNew);
      this.PublisherGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.PublisherGrid_FormatText);
      this.PublisherGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Desk\" DataF" +
        "ield=\"Desk\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
        "ption=\"Business Date\" DataField=\"BizDate\" NumberFormat=\"FormatText Event\"><Value" +
        "Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"File Put Ti" +
        "me\" DataField=\"FilePutTime\" NumberFormat=\"FormatText Event\"><ValueItems /><Group" +
        "Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Mail Send Time\" DataField" +
        "=\"MailSendTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1D" +
        "ataColumn><C1DataColumn Level=\"0\" Caption=\"Mail Status\" DataField=\"MailStatus\"><" +
        "ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Use Pg" +
        "p\" DataField=\"UsePgp\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1Dat" +
        "aColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFor" +
        "mat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
        "evel=\"0\" Caption=\"Is Active\" DataField=\"IsActive\"><ValueItems Presentation=\"Chec" +
        "kBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"File Status" +
        "\" DataField=\"FileStatus\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
        " Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo" +
        " /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"File Host\" DataField=\"FileHos" +
        "t\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Fi" +
        "le Path Name\" DataField=\"FilePathName\"><ValueItems /><GroupInfo /></C1DataColumn" +
        "><C1DataColumn Level=\"0\" Caption=\"File User Name\" DataField=\"FileUserName\"><Valu" +
        "eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"File Passw" +
        "ord\" DataField=\"FilePassword\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
        "olumn Level=\"0\" Caption=\"LoadExtensionPGP\" DataField=\"LoadExtensionPgp\"><ValueIt" +
        "ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" Data" +
        "Field=\"Comment\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
        "\" Caption=\"MailAddress\" DataField=\"MailAddress\"><ValueItems /><GroupInfo /></C1D" +
        "ataColumn><C1DataColumn Level=\"0\" Caption=\"MailSubject\" DataField=\"MailSubject\">" +
        "<ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1True" +
        "DBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackCol" +
        "or:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}" +
        "Style324{}Style320{AlignHorz:Near;}Style321{}Style322{}Style323{}Style396{}Style" +
        "395{}Style394{}Style393{}Style392{AlignHorz:Near;}Style391{AlignHorz:Near;}Style" +
        "390{}Style76{}Style77{}Style74{}Style75{}Style399{}Style398{AlignHorz:Near;}Styl" +
        "e319{AlignHorz:Near;}Style318{}Style317{}Style316{}Style315{}Style314{AlignHorz:" +
        "Near;}Style313{AlignHorz:Near;}Style312{}Style311{}Style386{AlignHorz:Near;}Styl" +
        "e387{}Style384{}Style385{AlignHorz:Near;}Editor{}Style383{}Style380{AlignHorz:Ne" +
        "ar;}Style381{}Style397{AlignHorz:Near;}Style388{}Style389{}FilterBar{}Style308{A" +
        "lignHorz:Near;}Style309{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1," +
        " 1, 1;ForeColor:ControlText;BackColor:Control;}Style307{AlignHorz:Near;}Style300" +
        "{}Style379{AlignHorz:Near;}Style378{}Caption{AlignHorz:Center;}Style346{}Style37" +
        "1{}Style370{}Style373{AlignHorz:Near;}Style372{}Style375{}Style374{AlignHorz:Nea" +
        "r;}Style377{}Style376{}Style310{}Style368{AlignHorz:Near;}Style369{}EvenRow{Back" +
        "Color:Aqua;}Style360{}Style361{AlignHorz:Near;}Style362{AlignHorz:Near;}Style363" +
        "{}Style364{}Style365{}Style366{}Style367{AlignHorz:Near;}Style293{}OddRow{}Style" +
        "291{}Style294{}Style295{AlignHorz:Near;}Style292{}Style382{}Style290{AlignHorz:N" +
        "ear;}Style297{}Style298{}Style299{}Style296{AlignHorz:Near;}Style359{}Style358{}" +
        "Style353{}Style352{}Style351{}Style350{AlignHorz:Near;}Style357{}Style356{AlignH" +
        "orz:Near;}Style355{AlignHorz:Near;}Style354{}Normal{Font:Verdana, 8.25pt;}Style4" +
        "01{}Style400{}Style403{}Style402{}Selected{ForeColor:HighlightText;BackColor:Hig" +
        "hlight;}Style348{}Style349{AlignHorz:Near;}Style342{}Style343{AlignHorz:Near;}St" +
        "yle340{}Style341{}RecordSelector{AlignImage:Center;}Style347{}Style344{AlignHorz" +
        ":Near;}Style345{}Style278{}Footer{}Style272{}Style273{}Style274{}Style275{}Style" +
        "276{}Style277{}Style279{}Style339{}Style338{AlignHorz:Center;}Style335{}Style334" +
        "{}Style337{AlignHorz:Near;}Style336{}Style331{AlignHorz:Near;}Style285{}Style333" +
        "{}Style332{AlignHorz:Center;}Style282{}Style281{}Style280{AlignHorz:Near;}Style2" +
        "71{}Style286{}Style284{AlignHorz:Near;}Style283{AlignHorz:Near;}Style289{AlignHo" +
        "rz:Near;}Style288{}Style287{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0" +
        ";AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarSt" +
        "yle=\"None\" AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"Split[0,0]\" AllowRo" +
        "wSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"1" +
        "7\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"" +
        "16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"3\"><Capti" +
        "onStyle parent=\"Heading\" me=\"Style280\" /><EditorStyle parent=\"Editor\" me=\"Style2" +
        "72\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style278\" /><FilterBarStyle parent=\"Fil" +
        "terBar\" me=\"Style403\" /><FooterStyle parent=\"Footer\" me=\"Style274\" /><GroupStyle" +
        " parent=\"Group\" me=\"Style282\" /><HeadingStyle parent=\"Heading\" me=\"Style273\" /><" +
        "HighLightRowStyle parent=\"HighlightRow\" me=\"Style277\" /><InactiveStyle parent=\"I" +
        "nactive\" me=\"Style276\" /><OddRowStyle parent=\"OddRow\" me=\"Style279\" /><RecordSel" +
        "ectorStyle parent=\"RecordSelector\" me=\"Style281\" /><SelectedStyle parent=\"Select" +
        "ed\" me=\"Style275\" /><Style parent=\"Normal\" me=\"Style271\" /><internalCols><C1Disp" +
        "layColumn><HeadingStyle parent=\"Style273\" me=\"Style283\" /><Style parent=\"Style27" +
        "1\" me=\"Style284\" /><FooterStyle parent=\"Style274\" me=\"Style285\" /><EditorStyle p" +
        "arent=\"Style272\" me=\"Style286\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style28" +
        "8\" /><GroupFooterStyle parent=\"Style271\" me=\"Style287\" /><Visible>True</Visible>" +
        "<ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCI" +
        "dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style2" +
        "89\" /><Style parent=\"Style271\" me=\"Style290\" /><FooterStyle parent=\"Style274\" me" +
        "=\"Style291\" /><EditorStyle parent=\"Style272\" me=\"Style292\" /><GroupHeaderStyle p" +
        "arent=\"Style271\" me=\"Style294\" /><GroupFooterStyle parent=\"Style271\" me=\"Style29" +
        "3\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Wid" +
        "th>90</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColu" +
        "mn><HeadingStyle parent=\"Style273\" me=\"Style295\" /><Style parent=\"Style271\" me=\"" +
        "Style296\" /><FooterStyle parent=\"Style274\" me=\"Style297\" /><EditorStyle parent=\"" +
        "Style272\" me=\"Style298\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style300\" /><G" +
        "roupFooterStyle parent=\"Style271\" me=\"Style299\" /><Visible>True</Visible><Column" +
        "Divider>WhiteSmoke,Single</ColumnDivider><Width>135</Width><Height>15</Height><D" +
        "CIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\"" +
        " me=\"Style307\" /><Style parent=\"Style271\" me=\"Style308\" /><FooterStyle parent=\"S" +
        "tyle274\" me=\"Style309\" /><EditorStyle parent=\"Style272\" me=\"Style310\" /><GroupHe" +
        "aderStyle parent=\"Style271\" me=\"Style312\" /><GroupFooterStyle parent=\"Style271\" " +
        "me=\"Style311\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke,Single</ColumnD" +
        "ivider><Width>160</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C" +
        "1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style313\" /><Style parent=\"St" +
        "yle271\" me=\"Style314\" /><FooterStyle parent=\"Style274\" me=\"Style315\" /><EditorSt" +
        "yle parent=\"Style272\" me=\"Style316\" /><GroupHeaderStyle parent=\"Style271\" me=\"St" +
        "yle318\" /><GroupFooterStyle parent=\"Style271\" me=\"Style317\" /><Visible>True</Vis" +
        "ible><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>135</Width><Height>1" +
        "5</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
        "t=\"Style273\" me=\"Style319\" /><Style parent=\"Style271\" me=\"Style320\" /><FooterSty" +
        "le parent=\"Style274\" me=\"Style321\" /><EditorStyle parent=\"Style272\" me=\"Style322" +
        "\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style324\" /><GroupFooterStyle parent" +
        "=\"Style271\" me=\"Style323\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke,Sin" +
        "gle</ColumnDivider><Width>130</Width><Height>15</Height><DCIdx>4</DCIdx></C1Disp" +
        "layColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style331\" /><Styl" +
        "e parent=\"Style271\" me=\"Style332\" /><FooterStyle parent=\"Style274\" me=\"Style333\"" +
        " /><EditorStyle parent=\"Style272\" me=\"Style334\" /><GroupHeaderStyle parent=\"Styl" +
        "e271\" me=\"Style336\" /><GroupFooterStyle parent=\"Style271\" me=\"Style335\" /><Colum" +
        "nDivider>WhiteSmoke,Single</ColumnDivider><Width>64</Width><Height>15</Height><D" +
        "CIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\"" +
        " me=\"Style337\" /><Style parent=\"Style271\" me=\"Style338\" /><FooterStyle parent=\"S" +
        "tyle274\" me=\"Style339\" /><EditorStyle parent=\"Style272\" me=\"Style340\" /><GroupHe" +
        "aderStyle parent=\"Style271\" me=\"Style342\" /><GroupFooterStyle parent=\"Style271\" " +
        "me=\"Style341\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>59</Width" +
        "><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
        "tyle parent=\"Style273\" me=\"Style343\" /><Style parent=\"Style271\" me=\"Style344\" />" +
        "<FooterStyle parent=\"Style274\" me=\"Style345\" /><EditorStyle parent=\"Style272\" me" +
        "=\"Style346\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style348\" /><GroupFooterSt" +
        "yle parent=\"Style271\" me=\"Style347\" /><ColumnDivider>WhiteSmoke,Single</ColumnDi" +
        "vider><Width>150</Width><Height>15</Height><AllowFocus>False</AllowFocus><DCIdx>" +
        "9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"" +
        "Style349\" /><Style parent=\"Style271\" me=\"Style350\" /><FooterStyle parent=\"Style2" +
        "74\" me=\"Style351\" /><EditorStyle parent=\"Style272\" me=\"Style352\" /><GroupHeaderS" +
        "tyle parent=\"Style271\" me=\"Style354\" /><GroupFooterStyle parent=\"Style271\" me=\"S" +
        "tyle353\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>120</Width><He" +
        "ight>15</Height><AllowFocus>False</AllowFocus><DCIdx>6</DCIdx></C1DisplayColumn>" +
        "<C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style355\" /><Style parent=\"" +
        "Style271\" me=\"Style356\" /><FooterStyle parent=\"Style274\" me=\"Style357\" /><Editor" +
        "Style parent=\"Style272\" me=\"Style358\" /><GroupHeaderStyle parent=\"Style271\" me=\"" +
        "Style360\" /><GroupFooterStyle parent=\"Style271\" me=\"Style359\" /><ColumnDivider>W" +
        "hiteSmoke,Single</ColumnDivider><Height>15</Height><DCIdx>10</DCIdx></C1DisplayC" +
        "olumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style361\" /><Style pa" +
        "rent=\"Style271\" me=\"Style362\" /><FooterStyle parent=\"Style274\" me=\"Style363\" /><" +
        "EditorStyle parent=\"Style272\" me=\"Style364\" /><GroupHeaderStyle parent=\"Style271" +
        "\" me=\"Style366\" /><GroupFooterStyle parent=\"Style271\" me=\"Style365\" /><ColumnDiv" +
        "ider>WhiteSmoke,Single</ColumnDivider><Height>15</Height><DCIdx>11</DCIdx></C1Di" +
        "splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style367\" /><St" +
        "yle parent=\"Style271\" me=\"Style368\" /><FooterStyle parent=\"Style274\" me=\"Style36" +
        "9\" /><EditorStyle parent=\"Style272\" me=\"Style370\" /><GroupHeaderStyle parent=\"St" +
        "yle271\" me=\"Style372\" /><GroupFooterStyle parent=\"Style271\" me=\"Style371\" /><Col" +
        "umnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</Height><DCIdx>12</DCIdx>" +
        "</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style373\"" +
        " /><Style parent=\"Style271\" me=\"Style374\" /><FooterStyle parent=\"Style274\" me=\"S" +
        "tyle375\" /><EditorStyle parent=\"Style272\" me=\"Style376\" /><GroupHeaderStyle pare" +
        "nt=\"Style271\" me=\"Style378\" /><GroupFooterStyle parent=\"Style271\" me=\"Style377\" " +
        "/><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</Height><DCIdx>13</" +
        "DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Sty" +
        "le379\" /><Style parent=\"Style271\" me=\"Style380\" /><FooterStyle parent=\"Style274\"" +
        " me=\"Style381\" /><EditorStyle parent=\"Style272\" me=\"Style382\" /><GroupHeaderStyl" +
        "e parent=\"Style271\" me=\"Style384\" /><GroupFooterStyle parent=\"Style271\" me=\"Styl" +
        "e383\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</Height><DCId" +
        "x>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" m" +
        "e=\"Style385\" /><Style parent=\"Style271\" me=\"Style386\" /><FooterStyle parent=\"Sty" +
        "le274\" me=\"Style387\" /><EditorStyle parent=\"Style272\" me=\"Style388\" /><GroupHead" +
        "erStyle parent=\"Style271\" me=\"Style390\" /><GroupFooterStyle parent=\"Style271\" me" +
        "=\"Style389\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</Height" +
        "><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
        "273\" me=\"Style391\" /><Style parent=\"Style271\" me=\"Style392\" /><FooterStyle paren" +
        "t=\"Style274\" me=\"Style393\" /><EditorStyle parent=\"Style272\" me=\"Style394\" /><Gro" +
        "upHeaderStyle parent=\"Style271\" me=\"Style396\" /><GroupFooterStyle parent=\"Style2" +
        "71\" me=\"Style395\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</" +
        "Height><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
        "\"Style273\" me=\"Style397\" /><Style parent=\"Style271\" me=\"Style398\" /><FooterStyle" +
        " parent=\"Style274\" me=\"Style399\" /><EditorStyle parent=\"Style272\" me=\"Style400\" " +
        "/><GroupHeaderStyle parent=\"Style271\" me=\"Style402\" /><GroupFooterStyle parent=\"" +
        "Style271\" me=\"Style401\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Heigh" +
        "t>15</Height><DCIdx>17</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0," +
        " 996, 289</ClientRect><BorderSide>Right</BorderSide></C1.Win.C1TrueDBGrid.MergeV" +
        "iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
        "me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
        "\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
        "elected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highl" +
        "ightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddR" +
        "ow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"F" +
        "ilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</ve" +
        "rtSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>" +
        "16</DefaultRecSelWidth><ClientArea>0, 0, 996, 289</ClientArea><PrintPageHeaderSt" +
        "yle parent=\"\" me=\"Style76\" /><PrintPageFooterStyle parent=\"\" me=\"Style77\" /></Bl" +
        "ob>";
      // 
      // SaveChangesButton
      // 
      this.SaveChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.SaveChangesButton.Enabled = false;
      this.SaveChangesButton.ForeColor = System.Drawing.SystemColors.ControlText;
      this.SaveChangesButton.Location = new System.Drawing.Point(888, 380);
      this.SaveChangesButton.Name = "SaveChangesButton";
      this.SaveChangesButton.Size = new System.Drawing.Size(104, 21);
      this.SaveChangesButton.TabIndex = 11;
      this.SaveChangesButton.Text = "Save Changes";
      this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
      // 
      // UsePgpCheck
      // 
      this.UsePgpCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.UsePgpCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.UsePgpCheck.ForeColor = System.Drawing.SystemColors.ControlText;
      this.UsePgpCheck.Location = new System.Drawing.Point(472, 380);
      this.UsePgpCheck.Name = "UsePgpCheck";
      this.UsePgpCheck.Size = new System.Drawing.Size(16, 21);
      this.UsePgpCheck.TabIndex = 8;
      this.UsePgpCheck.CheckedChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // DeskDropDown
      // 
      this.DeskDropDown.AllowColMove = true;
      this.DeskDropDown.AllowColSelect = true;
      this.DeskDropDown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
      this.DeskDropDown.AlternatingRows = false;
      this.DeskDropDown.CaptionHeight = 17;
      this.DeskDropDown.ColumnCaptionHeight = 17;
      this.DeskDropDown.ColumnFooterHeight = 17;
      this.DeskDropDown.EmptyRows = true;
      this.DeskDropDown.ExtendRightColumn = true;
      this.DeskDropDown.FetchRowStyles = false;
      this.DeskDropDown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
      this.DeskDropDown.Location = new System.Drawing.Point(72, 56);
      this.DeskDropDown.Name = "DeskDropDown";
      this.DeskDropDown.RowDivider.Color = System.Drawing.Color.Gainsboro;
      this.DeskDropDown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
      this.DeskDropDown.RowHeight = 15;
      this.DeskDropDown.RowSubDividerColor = System.Drawing.Color.WhiteSmoke;
      this.DeskDropDown.ScrollTips = false;
      this.DeskDropDown.Size = new System.Drawing.Size(248, 200);
      this.DeskDropDown.TabIndex = 32;
      this.DeskDropDown.Visible = false;
      this.DeskDropDown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Desks\" Data" +
        "Field=\"Desk\"><ValueItems Validate=\"True\" /><GroupInfo /></C1DataColumn><C1DataCo" +
        "lumn Level=\"0\" Caption=\"Firm Name\" DataField=\"Firm\"><ValueItems /><GroupInfo /><" +
        "/C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper" +
        "\"><Data>Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{}Selected" +
        "{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{" +
        "AlignHorz:Near;}Style15{AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:Near" +
        ";}Style11{}OddRow{}Style13{}Footer{}HighlightRow{ForeColor:HighlightText;BackCol" +
        "or:Highlight;}RecordSelector{AlignImage:Center;}Style24{}Style23{}Style22{}Style" +
        "21{AlignHorz:Near;}Style20{AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionTex" +
        "t;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;BackColor" +
        ":Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Filte" +
        "rBar{}Style5{}Style4{}Style9{}Style8{}Style12{}Group{AlignVert:Center;Border:Non" +
        "e,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}Style6{}Style1{}Style3{}Style2{}</D" +
        "ata></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView HBarStyle=\"None\" VBarStyl" +
        "e=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeigh" +
        "t=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWi" +
        "dth=\"16\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" Hor" +
        "izontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle" +
        " parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Fil" +
        "terBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"S" +
        "tyle3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading" +
        "\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inactive" +
        "Style parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /" +
        "><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle pare" +
        "nt=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><" +
        "C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Styl" +
        "e1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle pare" +
        "nt=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><Gro" +
        "upFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivid" +
        "er>Gainsboro,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1Displa" +
        "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style par" +
        "ent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorS" +
        "tyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style2" +
        "5\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><Co" +
        "lumnDivider>Gainsboro,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx><" +
        "/C1DisplayColumn></internalCols><ClientRect>0, 0, 244, 196</ClientRect><BorderSi" +
        "de>0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style" +
        " parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"He" +
        "ading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Headi" +
        "ng\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal" +
        "\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal" +
        "\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me" +
        "=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Capti" +
        "on\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSpli" +
        "ts><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientAr" +
        "ea>0, 0, 244, 196</ClientArea></Blob>";
      // 
      // FilePathTextBox
      // 
      this.FilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FilePathTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FilePathTextBox.Location = new System.Drawing.Point(112, 308);
      this.FilePathTextBox.Name = "FilePathTextBox";
      this.FilePathTextBox.Size = new System.Drawing.Size(200, 21);
      this.FilePathTextBox.TabIndex = 1;
      this.FilePathTextBox.Tag = null;
      this.FilePathTextBox.TextDetached = true;
      this.FilePathTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // FileHostTextBox
      // 
      this.FileHostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FileHostTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FileHostTextBox.Location = new System.Drawing.Point(112, 332);
      this.FileHostTextBox.Name = "FileHostTextBox";
      this.FileHostTextBox.Size = new System.Drawing.Size(200, 21);
      this.FileHostTextBox.TabIndex = 2;
      this.FileHostTextBox.Tag = null;
      this.FileHostTextBox.TextDetached = true;
      this.FileHostTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // FileUserNameTextBox
      // 
      this.FileUserNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FileUserNameTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FileUserNameTextBox.Location = new System.Drawing.Point(112, 356);
      this.FileUserNameTextBox.Name = "FileUserNameTextBox";
      this.FileUserNameTextBox.Size = new System.Drawing.Size(200, 21);
      this.FileUserNameTextBox.TabIndex = 3;
      this.FileUserNameTextBox.Tag = null;
      this.FileUserNameTextBox.TextDetached = true;
      this.FileUserNameTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // FilePasswordTextBox
      // 
      this.FilePasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FilePasswordTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FilePasswordTextBox.Location = new System.Drawing.Point(112, 380);
      this.FilePasswordTextBox.Name = "FilePasswordTextBox";
      this.FilePasswordTextBox.Size = new System.Drawing.Size(200, 21);
      this.FilePasswordTextBox.TabIndex = 4;
      this.FilePasswordTextBox.Tag = null;
      this.FilePasswordTextBox.TextDetached = true;
      this.FilePasswordTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // LoadExPgpTextBox
      // 
      this.LoadExPgpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LoadExPgpTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LoadExPgpTextBox.Location = new System.Drawing.Point(472, 356);
      this.LoadExPgpTextBox.Name = "LoadExPgpTextBox";
      this.LoadExPgpTextBox.Size = new System.Drawing.Size(200, 21);
      this.LoadExPgpTextBox.TabIndex = 7;
      this.LoadExPgpTextBox.Tag = null;
      this.LoadExPgpTextBox.TextDetached = true;
      this.LoadExPgpTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // CommentTextBox
      // 
      this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.CommentTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.CommentTextBox.Location = new System.Drawing.Point(792, 332);
      this.CommentTextBox.Name = "CommentTextBox";
      this.CommentTextBox.Size = new System.Drawing.Size(200, 21);
      this.CommentTextBox.TabIndex = 9;
      this.CommentTextBox.Tag = null;
      this.CommentTextBox.TextDetached = true;
      this.CommentTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // MailAddressTextBox
      // 
      this.MailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.MailAddressTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.MailAddressTextBox.Location = new System.Drawing.Point(472, 308);
      this.MailAddressTextBox.MaxLength = 255;
      this.MailAddressTextBox.Name = "MailAddressTextBox";
      this.MailAddressTextBox.Size = new System.Drawing.Size(520, 21);
      this.MailAddressTextBox.TabIndex = 5;
      this.MailAddressTextBox.Tag = null;
      this.MailAddressTextBox.TextDetached = true;
      this.MailAddressTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // MailSubjectTextBox
      // 
      this.MailSubjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.MailSubjectTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
      this.MailSubjectTextBox.Location = new System.Drawing.Point(472, 332);
      this.MailSubjectTextBox.Name = "MailSubjectTextBox";
      this.MailSubjectTextBox.Size = new System.Drawing.Size(200, 21);
      this.MailSubjectTextBox.TabIndex = 6;
      this.MailSubjectTextBox.Tag = null;
      this.MailSubjectTextBox.TextDetached = true;
      this.MailSubjectTextBox.TextChanged += new System.EventHandler(this.PublisherValues_Changed);
      // 
      // FilePathLabel
      // 
      this.FilePathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FilePathLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FilePathLabel.Location = new System.Drawing.Point(8, 308);
      this.FilePathLabel.Name = "FilePathLabel";
      this.FilePathLabel.Size = new System.Drawing.Size(104, 21);
      this.FilePathLabel.TabIndex = 134;
      this.FilePathLabel.Tag = null;
      this.FilePathLabel.Text = "File Path:";
      this.FilePathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.FilePathLabel.TextDetached = true;
      // 
      // FileHostLabel
      // 
      this.FileHostLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FileHostLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FileHostLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FileHostLabel.Location = new System.Drawing.Point(8, 332);
      this.FileHostLabel.Name = "FileHostLabel";
      this.FileHostLabel.Size = new System.Drawing.Size(104, 21);
      this.FileHostLabel.TabIndex = 135;
      this.FileHostLabel.Tag = null;
      this.FileHostLabel.Text = "File Host:";
      this.FileHostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.FileHostLabel.TextDetached = true;
      // 
      // FileUsernameLabel
      // 
      this.FileUsernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FileUsernameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FileUsernameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FileUsernameLabel.Location = new System.Drawing.Point(8, 356);
      this.FileUsernameLabel.Name = "FileUsernameLabel";
      this.FileUsernameLabel.Size = new System.Drawing.Size(104, 21);
      this.FileUsernameLabel.TabIndex = 136;
      this.FileUsernameLabel.Tag = null;
      this.FileUsernameLabel.Text = "File Username:";
      this.FileUsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.FileUsernameLabel.TextDetached = true;
      // 
      // FilePasswordLabel
      // 
      this.FilePasswordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.FilePasswordLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FilePasswordLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FilePasswordLabel.Location = new System.Drawing.Point(8, 380);
      this.FilePasswordLabel.Name = "FilePasswordLabel";
      this.FilePasswordLabel.Size = new System.Drawing.Size(104, 21);
      this.FilePasswordLabel.TabIndex = 137;
      this.FilePasswordLabel.Tag = null;
      this.FilePasswordLabel.Text = "File Password:";
      this.FilePasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.FilePasswordLabel.TextDetached = true;
      // 
      // MailAddressLabel
      // 
      this.MailAddressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.MailAddressLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.MailAddressLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.MailAddressLabel.Location = new System.Drawing.Point(360, 308);
      this.MailAddressLabel.Name = "MailAddressLabel";
      this.MailAddressLabel.Size = new System.Drawing.Size(112, 21);
      this.MailAddressLabel.TabIndex = 138;
      this.MailAddressLabel.Tag = null;
      this.MailAddressLabel.Text = "Mail Address(s):";
      this.MailAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.MailAddressLabel.TextDetached = true;
      // 
      // LoadExPgpLabel
      // 
      this.LoadExPgpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LoadExPgpLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.LoadExPgpLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LoadExPgpLabel.Location = new System.Drawing.Point(344, 356);
      this.LoadExPgpLabel.Name = "LoadExPgpLabel";
      this.LoadExPgpLabel.Size = new System.Drawing.Size(128, 21);
      this.LoadExPgpLabel.TabIndex = 140;
      this.LoadExPgpLabel.Tag = null;
      this.LoadExPgpLabel.Text = "Load Extension PGP:";
      this.LoadExPgpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.LoadExPgpLabel.TextDetached = true;
      // 
      // CommentLabel
      // 
      this.CommentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.CommentLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.CommentLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.CommentLabel.Location = new System.Drawing.Point(712, 332);
      this.CommentLabel.Name = "CommentLabel";
      this.CommentLabel.Size = new System.Drawing.Size(80, 21);
      this.CommentLabel.TabIndex = 141;
      this.CommentLabel.Tag = null;
      this.CommentLabel.Text = "Comment:";
      this.CommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CommentLabel.TextDetached = true;
      // 
      // UsePgpLabel
      // 
      this.UsePgpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.UsePgpLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.UsePgpLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.UsePgpLabel.Location = new System.Drawing.Point(344, 380);
      this.UsePgpLabel.Name = "UsePgpLabel";
      this.UsePgpLabel.Size = new System.Drawing.Size(128, 21);
      this.UsePgpLabel.TabIndex = 143;
      this.UsePgpLabel.Tag = null;
      this.UsePgpLabel.Text = "Use PGP:";
      this.UsePgpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.UsePgpLabel.TextDetached = true;
      // 
      // MailSubjectLabel
      // 
      this.MailSubjectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.MailSubjectLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.MailSubjectLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.MailSubjectLabel.Location = new System.Drawing.Point(360, 332);
      this.MailSubjectLabel.Name = "MailSubjectLabel";
      this.MailSubjectLabel.Size = new System.Drawing.Size(112, 21);
      this.MailSubjectLabel.TabIndex = 139;
      this.MailSubjectLabel.Tag = null;
      this.MailSubjectLabel.Text = "Mail Subject:";
      this.MailSubjectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.MailSubjectLabel.TextDetached = true;
      // 
      // LastUpdateLabel
      // 
      this.LastUpdateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LastUpdateLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.LastUpdateLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.LastUpdateLabel.Location = new System.Drawing.Point(712, 356);
      this.LastUpdateLabel.Name = "LastUpdateLabel";
      this.LastUpdateLabel.Size = new System.Drawing.Size(80, 21);
      this.LastUpdateLabel.TabIndex = 144;
      this.LastUpdateLabel.Tag = null;
      this.LastUpdateLabel.Text = "Last Update:";
      this.LastUpdateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.LastUpdateLabel.TextDetached = true;
      // 
      // LastUpdateInfoLabel
      // 
      this.LastUpdateInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LastUpdateInfoLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.LastUpdateInfoLabel.ForeColor = System.Drawing.Color.Navy;
      this.LastUpdateInfoLabel.Location = new System.Drawing.Point(792, 356);
      this.LastUpdateInfoLabel.Name = "LastUpdateInfoLabel";
      this.LastUpdateInfoLabel.Size = new System.Drawing.Size(200, 21);
      this.LastUpdateInfoLabel.TabIndex = 145;
      this.LastUpdateInfoLabel.Tag = null;
      this.LastUpdateInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.LastUpdateInfoLabel.TextDetached = true;
      // 
      // InventoryPublisherForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(1000, 413);
      this.Controls.Add(this.LastUpdateInfoLabel);
      this.Controls.Add(this.LastUpdateLabel);
      this.Controls.Add(this.UsePgpLabel);
      this.Controls.Add(this.CommentLabel);
      this.Controls.Add(this.LoadExPgpLabel);
      this.Controls.Add(this.MailSubjectLabel);
      this.Controls.Add(this.MailAddressLabel);
      this.Controls.Add(this.FilePasswordLabel);
      this.Controls.Add(this.FileUsernameLabel);
      this.Controls.Add(this.FileHostLabel);
      this.Controls.Add(this.FilePathLabel);
      this.Controls.Add(this.MailSubjectTextBox);
      this.Controls.Add(this.MailAddressTextBox);
      this.Controls.Add(this.CommentTextBox);
      this.Controls.Add(this.LoadExPgpTextBox);
      this.Controls.Add(this.FilePasswordTextBox);
      this.Controls.Add(this.FileUserNameTextBox);
      this.Controls.Add(this.FileHostTextBox);
      this.Controls.Add(this.FilePathTextBox);
      this.Controls.Add(this.DeskDropDown);
      this.Controls.Add(this.UsePgpCheck);
      this.Controls.Add(this.SaveChangesButton);
      this.Controls.Add(this.PublisherGrid);
      this.DockPadding.Bottom = 120;
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.ForeColor = System.Drawing.Color.Navy;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "InventoryPublisherForm";
      this.Text = "Inventory - Publisher";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.InventoryPublisherForm_Closing);
      this.Load += new System.EventHandler(this.InventoryPublisherForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.PublisherGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DeskDropDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePathTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileHostTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileUserNameTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePasswordTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.LoadExPgpTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailAddressTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailSubjectTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePathLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileHostLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileUsernameLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FilePasswordLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailAddressLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.LoadExPgpLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.UsePgpLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MailSubjectLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.LastUpdateLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.LastUpdateInfoLabel)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion
  
    private void PublisherGridLoad()
    {
      DataSet ds = new DataSet();
      try
      {

        ds = mainForm.ServiceAgent.PublisherListGet(mainForm.UtcOffset);
        PublisherGrid.SetDataBinding(ds, "PublisherList", true);
          
        FilePathTextBox.Text     = PublisherGrid.Columns["FilePathName"].Text;
        FileHostTextBox.Text     = PublisherGrid.Columns["FileHost"].Text;
        FileUserNameTextBox.Text = PublisherGrid.Columns["FileUserName"].Text;
        FilePasswordTextBox.Text = PublisherGrid.Columns["FilePassword"].Text;
        LoadExPgpTextBox.Text    = PublisherGrid.Columns["LoadExtensionPgp"].Text;
        CommentTextBox.Text      = PublisherGrid.Columns["Comment"].Text;
        MailAddressTextBox.Text  = PublisherGrid.Columns["MailAddress"].Text;
        MailSubjectTextBox.Text  = PublisherGrid.Columns["MailSubject"].Text;
        UsePgpCheck.Checked      = bool.Parse(PublisherGrid.Columns["UsePgp"].Text.Trim());
      }
      catch(Exception error)
      {
        Log.Write(error.Message + "[" + this.Name + ".PublisherGridLoad]", 1);
        mainForm.Alert(error.Message, PilotState.RunFault);
      }
    }

    private void PublisherValues_Changed(object sender, System.EventArgs e)
    {
      SaveChangesButton.Enabled = true;
    }

    private void PublisherGrid_RowColChange(object sender,  C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if((e.LastRow != PublisherGrid.Row)  && SaveChangesButton.Enabled)
      {
        mainForm.Alert("Subscription for: " + PublisherGrid.Columns["Desk"].CellText(e.LastRow) + " was not modified.", PilotState.RunFault);
      }

      if(e.LastRow != PublisherGrid.Row) 
      {
        FilePathTextBox.Text     = PublisherGrid.Columns["FilePathName"].Text;
        FileHostTextBox.Text     = PublisherGrid.Columns["FileHost"].Text;
        FileUserNameTextBox.Text = PublisherGrid.Columns["FileUserName"].Text;
        FilePasswordTextBox.Text = PublisherGrid.Columns["FilePassword"].Text;
        LoadExPgpTextBox.Text    = PublisherGrid.Columns["LoadExtensionPgp"].Text;
        CommentTextBox.Text      = PublisherGrid.Columns["Comment"].Text;
        MailAddressTextBox.Text  = PublisherGrid.Columns["MailAddress"].Text;
        MailSubjectTextBox.Text  = PublisherGrid.Columns["MailSubject"].Text;
        UsePgpCheck.Checked   = (PublisherGrid.Columns["usePgp"].Text.Equals("")?false: bool.Parse(PublisherGrid.Columns["usePgp"].Text));

        LastUpdateInfoLabel.Text = PublisherGrid.Columns["Actor"].Text + " " + Tools.FormatDate(PublisherGrid.Columns["ActTime"].Text, Standard.DateTimeFileFormat);
        
        SaveChangesButton.Enabled = false;
        
        PublisherGrid.Columns["Desk"].DropDown = null; //If editng has taken place and then a row change, remove the dropdown menu.
      }
    }

    private void SaveChangesButton_Click(object sender,  System.EventArgs e)
    {
      try
      {
        PublisherGrid.Columns["FilePathName"].Text      = FilePathTextBox.Text;
        PublisherGrid.Columns["FileHost"].Text          = FileHostTextBox.Text;
        PublisherGrid.Columns["FileUserName"].Text      = FileUserNameTextBox.Text;
        PublisherGrid.Columns["FilePassword"].Text      = FilePasswordTextBox.Text;
        PublisherGrid.Columns["LoadExtensionPgp"].Text  = LoadExPgpTextBox.Text;
        PublisherGrid.Columns["Comment"].Text           = CommentTextBox.Text;
        PublisherGrid.Columns["MailAddress"].Text       = MailAddressTextBox.Text;
        PublisherGrid.Columns["MailSubject"].Text       = MailSubjectTextBox.Text;
        PublisherGrid.Columns["UsePgp"].Value           = UsePgpCheck.Checked;      
               
        /*mainForm.ServiceAgent.PublisherListSet(
          PublisherGrid.Columns["Desk"].Text, 
          FilePathTextBox.Text.Trim(), 
          FileHostTextBox.Text.Trim(), 
          FileUserNameTextBox.Text.Trim(), 
          FilePasswordTextBox.Text.Trim(),  
          LoadExPgpTextBox.Text.Trim(), 
          CommentTextBox.Text.Trim(),                             
          MailAddressTextBox.Text.Trim(), 
          MailSubjectTextBox.Text.Trim(), 
          true.ToString(),
          bool.Parse(UsePgpCheck.Checked.ToString()).ToString(),
          mainForm.UserId
          );    */
  
        PublisherGrid.Columns["Actor"].Text = mainForm.UserId;
        PublisherGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
        
        mainForm.Alert("Subscription for: " + PublisherGrid.Columns["Desk"].Text + " has been sucessfully modified.", PilotState.Normal);
        Log.Write("Subscription for: " + PublisherGrid.Columns["Desk"].Text + " has been sucessfully modified. [" + this.Name + ".SaveChangesButton_Click]", 4);
        
        PublisherGrid.Columns["Desk"].DropDown = null; 
        
        PublisherGrid.UpdateData();
      
        SaveChangesButton.Enabled = false;
      }
      catch(Exception error)
      {
        Log.Write(error.Message + "[InventoryPublisherForm.SaveChangesButton_Click]", 1);
        return;
      }
    }

    private void InventoryPublisherForm_Closing(object sender,  System.ComponentModel.CancelEventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());   
      }
    }

    private void InventoryPublisherForm_Load(object sender, System.EventArgs e)
    {    
      this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
      this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "440"));
      this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1008"));
    
      PublisherGridLoad();
      SaveChangesButton.Enabled = false;

      ToolTip toolTip = new ToolTip();

      toolTip.AutoPopDelay = 15000;
      toolTip.InitialDelay = 1000;
      toolTip.ReshowDelay  = 500;

      toolTip.ShowAlways = true;

      toolTip.SetToolTip(this.FilePathTextBox,
        "Enter the path of the file on the server.");

      toolTip.SetToolTip(this.FileHostTextBox,
        "Enter the address of the server where the file is being published.");

      toolTip.SetToolTip(this.FileUserNameTextBox,
        "Enter the username to access the server.");
     
      toolTip.SetToolTip(this.FilePasswordTextBox,
        "Enter the password for the username used to access the server.");

      toolTip.SetToolTip(this.LoadExPgpTextBox,
        "Enter the path to the pgp dll you wish to use, leave blank to use defualt.");
      
      toolTip.SetToolTip(this.CommentTextBox,
        "Enter a comment for the publication.");

      toolTip.SetToolTip(this.MailAddressTextBox,
        "Enter the email address(s) for the reciever(s) of the file.");

      toolTip.SetToolTip(this.MailSubjectTextBox,
        "Enter the email subject.");

      toolTip.SetToolTip(this.UsePgpCheck,
        "Check if the file is pgp encrypted, Uncheck otherwise.");
    }

    private void PublisherGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {      
      switch(PublisherGrid.Columns[e.ColIndex].DataField)
      {
        case("BizDate"):          
          e.Value =  Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);                    
          break;      
  
        case("MailSendTime"):        
        case("FilePutTime"):
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeFileFormat);
          break;
      
        case("ActTime"):
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);                                   
          break;            
      }
    }

    private void PublisherGrid_OnAddNew(object sender, System.EventArgs e)
    {
      FilePathTextBox.Text     = "";
      FileHostTextBox.Text     = ""; 
      FileUserNameTextBox.Text = ""; 
      FilePasswordTextBox.Text = ""; 
      LoadExPgpTextBox.Text    = "";
      CommentTextBox.Text      = "";                             
      MailAddressTextBox.Text  = "";
      MailSubjectTextBox.Text  = ""; 
      UsePgpCheck.Checked      = false;
    }

    private void PublisherGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {
      DataSet deskSet;
      
      if((PublisherGrid.Columns[e.ColIndex].DataField.Equals("Desk") && PublisherGrid.Columns[e.ColIndex].Text.Length < 1) || PublisherGrid.DataChanged)
      { 
        try
        {
          deskSet = mainForm.ServiceAgent.DeskGet(true);
          PublisherGrid.Columns["Desk"].DropDown = DeskDropDown;
          DeskDropDown.SetDataBinding(deskSet, "Desks", true);
          DeskDropDown.DataField = "Firm";
          DeskDropDown.ListField = "Desk";
          DeskDropDown.ValueTranslate = true;                            
        }
        catch(Exception error)
        {
          Log.Write(error.Message + "[InventoryPublisherForm.PublisherGrid_BeforeColEdit]", 3);
          PublisherGrid.Columns["Desk"].DropDown = null;
          return;
        }
      }
      else
      {
        PublisherGrid.Columns["Desk"].DropDown = null;
        mainForm.Alert("You cannot edit current cell.", PilotState.RunFault);
        e.Cancel = true;        
        return ;
      }
    }

    private void PublisherGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        PublisherGrid.Columns["FilePathName"].Text      = FilePathTextBox.Text;
        PublisherGrid.Columns["FileHost"].Text          = FileHostTextBox.Text;
        PublisherGrid.Columns["FileUserName"].Text      = FileUserNameTextBox.Text;
        PublisherGrid.Columns["FilePassword"].Text      = FilePasswordTextBox.Text;
        PublisherGrid.Columns["LoadExtensionPgp"].Text  = LoadExPgpTextBox.Text;
        PublisherGrid.Columns["Comment"].Text           = CommentTextBox.Text;
        PublisherGrid.Columns["MailAddress"].Text       = MailAddressTextBox.Text;
        PublisherGrid.Columns["MailSubject"].Text       = MailSubjectTextBox.Text;
        PublisherGrid.Columns["UsePgp"].Value           = UsePgpCheck.Checked;
               
        /*mainForm.ServiceAgent.PublisherListSet(
          PublisherGrid.Columns["Desk"].Text, 
          FilePathTextBox.Text.Trim(), 
          FileHostTextBox.Text.Trim(), 
          FileUserNameTextBox.Text.Trim(), 
          FilePasswordTextBox.Text.Trim(),  
          LoadExPgpTextBox.Text.Trim(), 
          CommentTextBox.Text.Trim(),                             
          MailAddressTextBox.Text.Trim(), 
          MailSubjectTextBox.Text.Trim(), 
          false.ToString(),
          bool.Parse(UsePgpCheck.Checked.ToString()).ToString(),
          mainForm.UserId
          ); */
        
        mainForm.Alert("Subscription for: " + PublisherGrid.Columns["Desk"].Text + " has been sucessfully deleted.", PilotState.Normal);
        Log.Write("Subscription for: " + PublisherGrid.Columns["Desk"].Text + " has been sucessfully deleted. [" + this.Name + ".PublisherGrid_BeforeDelete]", 3);
        
        PublisherGrid.Columns["Desk"].DropDown = null; 
        
        PublisherGrid.UpdateData();
      }
      catch(Exception error)
      {
        Log.Write(error.Message + "[InventoryPublisherForm.PublisherGrid_BeforeDelete]", 3);
        return;
      }
    }
  }
}
