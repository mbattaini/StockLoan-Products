using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class InventorySubscriberForm : System.Windows.Forms.Form
	{
		private MainForm mainForm;

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid SubscriberGrid;
		private C1.Win.C1TrueDBGrid.C1TrueDBDropdown DeskDropDown;
    
		private C1.Win.C1Input.C1Label FilePathLabel;
		private C1.Win.C1Input.C1Label FileHostLabel;
		private C1.Win.C1Input.C1Label FileUsernameLabel;
		private C1.Win.C1Input.C1Label FilePasswordLabel;

		private C1.Win.C1Input.C1TextBox FilePathTextBox;
		private C1.Win.C1Input.C1TextBox FileHostTextBox;
		private C1.Win.C1Input.C1TextBox FileUserNameTextBox;
		private C1.Win.C1Input.C1TextBox FilePasswordTextBox;

		private C1.Win.C1Input.C1Label MailAddressLabel;
		private C1.Win.C1Input.C1Label MailSubjectLabel;
		private C1.Win.C1Input.C1Label LoadExPgpLabel;
    
		private C1.Win.C1Input.C1TextBox MailAddressTextBox;
		private C1.Win.C1Input.C1TextBox MailSubjectTextBox;
		private C1.Win.C1Input.C1TextBox LoadExPgpTextBox;
		private C1.Win.C1Input.C1Label UsePgpLabel;
    
		private C1.Win.C1Input.C1Label CommentLabel;
		private C1.Win.C1Input.C1Label LastUpdateLabel;
		private C1.Win.C1Input.C1Label LastUpdateInfoLabel;
    
		private C1.Win.C1Input.C1TextBox CommentTextBox;
    
		private System.Windows.Forms.CheckBox UsePgpCheck;

		private System.Windows.Forms.Button ShowDataMaskButton;
		private System.Windows.Forms.Button SaveChangesButton;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.MenuItem InventorySendToMenuItem;
		private System.Windows.Forms.MenuItem SubscriptionSendToMenuItem;
		private System.Windows.Forms.MenuItem SubscriptionSendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SubscriptionSendToEmailMenuItem;
    
		private delegate void SendToEmailDelegate(string bizDate, string desk);
		private delegate void SendToClipboardDelegate(string bizDate, string desk);

		private System.ComponentModel.Container components = null;
    
		private void SendToEmail(string bizDate, string desk)
		{
			Email email = new Email();

			email.Send(RegistryValue.Name + " inventory from " + desk + " for " + bizDate + ".",
				DeskListGet(bizDate, desk));
		}

		private void SendToClipboard(string bizDate, string desk)
		{
			Clipboard.SetDataObject(DeskListGet(bizDate, desk), true);
		}
		
		
		public InventorySubscriberForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
			bool editable = mainForm.AdminAgent.MayEdit(mainForm.UserId, "InventorySubscriber");

			try
			{
				SubscriberGrid.AllowAddNew    = editable;
				SubscriberGrid.AllowUpdate    = editable;
        
				FilePathTextBox.ReadOnly = !editable;
				FileHostTextBox.ReadOnly = !editable;
				FileUserNameTextBox.ReadOnly = !editable;
				FilePasswordTextBox.ReadOnly = !editable;
        
				MailAddressTextBox.ReadOnly   = !editable;
				MailSubjectTextBox.ReadOnly   = !editable;
				LoadExPgpTextBox.ReadOnly     = !editable;
        
				CommentTextBox.ReadOnly       = !editable;
        
				SaveChangesButton.Visible     = editable;
        
				if(!editable)
				{
					FileUserNameTextBox.PasswordChar = '*';
					FilePasswordTextBox.PasswordChar = '*';
				}
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InventorySubscriberForm));
			this.SubscriberGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.SaveChangesButton = new System.Windows.Forms.Button();
			this.UsePgpCheck = new System.Windows.Forms.CheckBox();
			this.ShowDataMaskButton = new System.Windows.Forms.Button();
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
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.InventorySendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.SubscriptionSendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SubscriptionSendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SubscriptionSendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.SubscriberGrid)).BeginInit();
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
			// SubscriberGrid
			// 
			this.SubscriberGrid.AllowDelete = true;
			this.SubscriberGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.SubscriberGrid.CaptionHeight = 18;
			this.SubscriberGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.SubscriberGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SubscriberGrid.ExtendRightColumn = true;
			this.SubscriberGrid.GroupByAreaVisible = false;
			this.SubscriberGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.SubscriberGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.SubscriberGrid.Location = new System.Drawing.Point(0, 0);
			this.SubscriberGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.SubscriberGrid.Name = "SubscriberGrid";
			this.SubscriberGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.SubscriberGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.SubscriberGrid.PreviewInfo.ZoomFactor = 75;
			this.SubscriberGrid.RecordSelectorWidth = 16;
			this.SubscriberGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.SubscriberGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.SubscriberGrid.RowHeight = 15;
			this.SubscriberGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.SubscriberGrid.Size = new System.Drawing.Size(1000, 293);
			this.SubscriberGrid.TabAcrossSplits = true;
			this.SubscriberGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.SubscriberGrid.TabIndex = 0;
			this.SubscriberGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.SubscriberGrid_RowColChange);
			this.SubscriberGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.SubscriberGrid_BeforeColEdit);
			this.SubscriberGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.SubscriberGrid_BeforeDelete);
			this.SubscriberGrid.OnAddNew += new System.EventHandler(this.SubscriberGrid_OnAddNew);
			this.SubscriberGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SubscriberGrid_FormatText);
			this.SubscriberGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Desk\" DataF" +
				"ield=\"Desk\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Business Date\" DataField=\"BizDate\" NumberFormat=\"FormatText Event\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Load Time\" " +
				"DataField=\"LoadTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"File Time\" DataField=\"FileTime\" " +
				"NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
				"aColumn Level=\"0\" Caption=\"File Check Time\" DataField=\"FileCheckTime\" NumberForm" +
				"at=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Use Pgp\" DataField=\"UsePgp\"><ValueItems Presentation=\"CheckBox\"" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataFi" +
				"eld=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"Is Active\" DataField=\"IsActive\"><ValueI" +
				"tems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
				"0\" Caption=\"File Status\" DataField=\"FileStatus\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"Load Status\" DataField=\"LoadStatus\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\"" +
				" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"File Host\" DataField=\"FileHost\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"File Path Name\" DataField=\"F" +
				"ilePathName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"File User Name\" DataField=\"FileUserName\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"File Password\" DataField=\"FilePasswo" +
				"rd\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"L" +
				"oadExtensionPGP\" DataField=\"LoadExtensionPgp\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItem" +
				"s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"MailAddress\" Da" +
				"taField=\"MailAddress\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"MailSubject\" DataField=\"MailSubject\"><ValueItems /><GroupInfo /" +
				"></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Load Count\" DataField=\"LoadCoun" +
				"t\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"BP\" DataField=\"IsBizDatePrior\"><ValueItems Present" +
				"ation=\"CheckBox\" /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C" +
				"1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;Ba" +
				"ckColor:Highlight;}Style328{}Style329{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Style324{}Style325{AlignHorz:Near;}Style326{AlignHorz:N" +
				"ear;}Style327{}Style320{AlignHorz:Near;}Style321{}Style322{}Style323{}Style396{}" +
				"Style395{}Style394{}Style393{}Style392{AlignHorz:Near;}Style391{AlignHorz:Near;}" +
				"Style390{}Style76{}Style77{}Style74{}Style75{}Style399{}Style398{AlignHorz:Near;" +
				"}Style319{AlignHorz:Near;}Style318{}Style317{}Style316{}Style315{}Style314{Align" +
				"Horz:Near;}Style313{AlignHorz:Near;}Style312{}Style311{}Style386{AlignHorz:Near;" +
				"}Style387{}Style384{}Style385{AlignHorz:Near;}Editor{}Style383{}Style380{AlignHo" +
				"rz:Near;}Style381{}Style397{AlignHorz:Near;}Style388{}Style389{}FilterBar{}Style" +
				"308{AlignHorz:Near;}Style309{}Style306{}Style307{AlignHorz:Near;}Style304{}Style" +
				"305{}Style302{AlignHorz:Far;}Style303{}Style300{}Style379{AlignHorz:Near;}Style3" +
				"78{}Style346{}Style371{}Style370{}Style373{AlignHorz:Near;}Style372{}Style375{}S" +
				"tyle374{AlignHorz:Near;}Style377{}Style376{}Style310{}Style368{AlignHorz:Near;}S" +
				"tyle369{}Style5{}Style4{}EvenRow{BackColor:Aqua;}Style6{}Style1{AlignHorz:Center" +
				";}Style360{}Style2{AlignHorz:Center;}Style362{AlignHorz:Near;}Style363{}Style364" +
				"{}Style365{}Style366{}Style367{AlignHorz:Near;}Style293{}OddRow{}Style291{}Style" +
				"294{}Style295{AlignHorz:Near;}Style292{}Style382{}Style290{AlignHorz:Near;}Style" +
				"297{}Style298{}Style299{}Style296{AlignHorz:Near;}Style359{}Style358{}Style353{}" +
				"Style352{}Style351{}Style350{AlignHorz:Near;}Style357{}Style356{AlignHorz:Near;}" +
				"Style355{AlignHorz:Near;}Style354{}Heading{Wrap:True;BackColor:Control;Border:Ra" +
				"ised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style337{AlignHorz:Near" +
				";}Style361{AlignHorz:Near;}Style401{}Style400{}Style403{}Style402{}Selected{Fore" +
				"Color:HighlightText;BackColor:Highlight;}Style348{}Style349{AlignHorz:Near;}Norm" +
				"al{Font:Verdana, 8.25pt;}Style342{}Style343{AlignHorz:Near;}Style340{}Style341{}" +
				"RecordSelector{AlignImage:Center;}Style347{}Style344{AlignHorz:Near;}Style345{}S" +
				"tyle278{}Footer{}Style272{}Style273{}Style274{}Style275{}Style276{}Style277{}Sty" +
				"le279{}Style339{}Style338{AlignHorz:Center;}Style335{}Style334{}Caption{AlignHor" +
				"z:Center;}Style336{}Style331{AlignHorz:Near;}Style330{}Style333{}Style332{AlignH" +
				"orz:Center;}Style282{}Style281{}Style3{}Style301{AlignHorz:Near;}Style271{}Style" +
				"286{}Style285{}Style284{AlignHorz:Near;}Style283{AlignHorz:Near;}Style289{AlignH" +
				"orz:Near;}Style288{}Style280{AlignHorz:Near;}Style287{}Group{AlignVert:Center;Bo" +
				"rder:None,,0, 0, 0, 0;BackColor:ControlDark;}</Data></Styles><Splits><C1.Win.C1T" +
				"rueDBGrid.MergeView HBarStyle=\"None\" Name=\"Split[0,0]\" AllowRowSizing=\"None\" Cap" +
				"tionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColu" +
				"mn=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth" +
				"=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"3\"><CaptionStyle parent=\"He" +
				"ading\" me=\"Style280\" /><EditorStyle parent=\"Editor\" me=\"Style272\" /><EvenRowStyl" +
				"e parent=\"EvenRow\" me=\"Style278\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style4" +
				"03\" /><FooterStyle parent=\"Footer\" me=\"Style274\" /><GroupStyle parent=\"Group\" me" +
				"=\"Style282\" /><HeadingStyle parent=\"Heading\" me=\"Style273\" /><HighLightRowStyle " +
				"parent=\"HighlightRow\" me=\"Style277\" /><InactiveStyle parent=\"Inactive\" me=\"Style" +
				"276\" /><OddRowStyle parent=\"OddRow\" me=\"Style279\" /><RecordSelectorStyle parent=" +
				"\"RecordSelector\" me=\"Style281\" /><SelectedStyle parent=\"Selected\" me=\"Style275\" " +
				"/><Style parent=\"Normal\" me=\"Style271\" /><internalCols><C1DisplayColumn><Heading" +
				"Style parent=\"Style273\" me=\"Style283\" /><Style parent=\"Style271\" me=\"Style284\" /" +
				"><FooterStyle parent=\"Style274\" me=\"Style285\" /><EditorStyle parent=\"Style272\" m" +
				"e=\"Style286\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style288\" /><GroupFooterS" +
				"tyle parent=\"Style271\" me=\"Style287\" /><Visible>True</Visible><ColumnDivider>Whi" +
				"teSmoke,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style289\" /><Style paren" +
				"t=\"Style271\" me=\"Style290\" /><FooterStyle parent=\"Style274\" me=\"Style291\" /><Edi" +
				"torStyle parent=\"Style272\" me=\"Style292\" /><GroupHeaderStyle parent=\"Style271\" m" +
				"e=\"Style294\" /><GroupFooterStyle parent=\"Style271\" me=\"Style293\" /><Visible>True" +
				"</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>90</Width><Heig" +
				"ht>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style273\" me=\"Style295\" /><Style parent=\"Style271\" me=\"Style296\" /><Foote" +
				"rStyle parent=\"Style274\" me=\"Style297\" /><EditorStyle parent=\"Style272\" me=\"Styl" +
				"e298\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style300\" /><GroupFooterStyle pa" +
				"rent=\"Style271\" me=\"Style299\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke" +
				",Single</ColumnDivider><Width>130</Width><Height>15</Height><DCIdx>2</DCIdx></C1" +
				"DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style301\" /><" +
				"Style parent=\"Style271\" me=\"Style302\" /><FooterStyle parent=\"Style274\" me=\"Style" +
				"303\" /><EditorStyle parent=\"Style272\" me=\"Style304\" /><GroupHeaderStyle parent=\"" +
				"Style271\" me=\"Style306\" /><GroupFooterStyle parent=\"Style271\" me=\"Style305\" /><V" +
				"isible>True</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>74</" +
				"Width><Height>15</Height><DCIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style273\" me=\"Style307\" /><Style parent=\"Style271\" me=\"Style3" +
				"08\" /><FooterStyle parent=\"Style274\" me=\"Style309\" /><EditorStyle parent=\"Style2" +
				"72\" me=\"Style310\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style312\" /><GroupFo" +
				"oterStyle parent=\"Style271\" me=\"Style311\" /><Visible>True</Visible><ColumnDivide" +
				"r>WhiteSmoke,Single</ColumnDivider><Width>160</Width><Height>15</Height><DCIdx>9" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"S" +
				"tyle1\" /><Style parent=\"Style271\" me=\"Style2\" /><FooterStyle parent=\"Style274\" m" +
				"e=\"Style3\" /><EditorStyle parent=\"Style272\" me=\"Style4\" /><GroupHeaderStyle pare" +
				"nt=\"Style271\" me=\"Style6\" /><GroupFooterStyle parent=\"Style271\" me=\"Style5\" /><V" +
				"isible>True</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>30</" +
				"Width><Height>15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style273\" me=\"Style313\" /><Style parent=\"Style271\" me=\"Style3" +
				"14\" /><FooterStyle parent=\"Style274\" me=\"Style315\" /><EditorStyle parent=\"Style2" +
				"72\" me=\"Style316\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style318\" /><GroupFo" +
				"oterStyle parent=\"Style271\" me=\"Style317\" /><Visible>True</Visible><ColumnDivide" +
				"r>WhiteSmoke,Single</ColumnDivider><Width>130</Width><Height>15</Height><DCIdx>3" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"S" +
				"tyle319\" /><Style parent=\"Style271\" me=\"Style320\" /><FooterStyle parent=\"Style27" +
				"4\" me=\"Style321\" /><EditorStyle parent=\"Style272\" me=\"Style322\" /><GroupHeaderSt" +
				"yle parent=\"Style271\" me=\"Style324\" /><GroupFooterStyle parent=\"Style271\" me=\"St" +
				"yle323\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDivider" +
				"><Width>130</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style273\" me=\"Style325\" /><Style parent=\"Style271" +
				"\" me=\"Style326\" /><FooterStyle parent=\"Style274\" me=\"Style327\" /><EditorStyle pa" +
				"rent=\"Style272\" me=\"Style328\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style330" +
				"\" /><GroupFooterStyle parent=\"Style271\" me=\"Style329\" /><Visible>True</Visible><" +
				"ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>160</Width><Height>15</Hei" +
				"ght><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le273\" me=\"Style331\" /><Style parent=\"Style271\" me=\"Style332\" /><FooterStyle par" +
				"ent=\"Style274\" me=\"Style333\" /><EditorStyle parent=\"Style272\" me=\"Style334\" /><G" +
				"roupHeaderStyle parent=\"Style271\" me=\"Style336\" /><GroupFooterStyle parent=\"Styl" +
				"e271\" me=\"Style335\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>64<" +
				"/Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style273\" me=\"Style337\" /><Style parent=\"Style271\" me=\"Style3" +
				"38\" /><FooterStyle parent=\"Style274\" me=\"Style339\" /><EditorStyle parent=\"Style2" +
				"72\" me=\"Style340\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style342\" /><GroupFo" +
				"oterStyle parent=\"Style271\" me=\"Style341\" /><ColumnDivider>WhiteSmoke,Single</Co" +
				"lumnDivider><Width>59</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style273\" me=\"Style343\" /><Style parent" +
				"=\"Style271\" me=\"Style344\" /><FooterStyle parent=\"Style274\" me=\"Style345\" /><Edit" +
				"orStyle parent=\"Style272\" me=\"Style346\" /><GroupHeaderStyle parent=\"Style271\" me" +
				"=\"Style348\" /><GroupFooterStyle parent=\"Style271\" me=\"Style347\" /><ColumnDivider" +
				">WhiteSmoke,Single</ColumnDivider><Width>150</Width><Height>15</Height><AllowFoc" +
				"us>False</AllowFocus><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style273\" me=\"Style349\" /><Style parent=\"Style271\" me=\"Style350\" " +
				"/><FooterStyle parent=\"Style274\" me=\"Style351\" /><EditorStyle parent=\"Style272\" " +
				"me=\"Style352\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style354\" /><GroupFooter" +
				"Style parent=\"Style271\" me=\"Style353\" /><ColumnDivider>WhiteSmoke,Single</Column" +
				"Divider><Width>120</Width><Height>15</Height><AllowFocus>False</AllowFocus><DCId" +
				"x>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style273\" me" +
				"=\"Style355\" /><Style parent=\"Style271\" me=\"Style356\" /><FooterStyle parent=\"Styl" +
				"e274\" me=\"Style357\" /><EditorStyle parent=\"Style272\" me=\"Style358\" /><GroupHeade" +
				"rStyle parent=\"Style271\" me=\"Style360\" /><GroupFooterStyle parent=\"Style271\" me=" +
				"\"Style359\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</Height>" +
				"<DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"73\" me=\"Style361\" /><Style parent=\"Style271\" me=\"Style362\" /><FooterStyle parent" +
				"=\"Style274\" me=\"Style363\" /><EditorStyle parent=\"Style272\" me=\"Style364\" /><Grou" +
				"pHeaderStyle parent=\"Style271\" me=\"Style366\" /><GroupFooterStyle parent=\"Style27" +
				"1\" me=\"Style365\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>15</H" +
				"eight><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style273\" me=\"Style367\" /><Style parent=\"Style271\" me=\"Style368\" /><FooterStyle " +
				"parent=\"Style274\" me=\"Style369\" /><EditorStyle parent=\"Style272\" me=\"Style370\" /" +
				"><GroupHeaderStyle parent=\"Style271\" me=\"Style372\" /><GroupFooterStyle parent=\"S" +
				"tyle271\" me=\"Style371\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height" +
				">15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style273\" me=\"Style373\" /><Style parent=\"Style271\" me=\"Style374\" /><Footer" +
				"Style parent=\"Style274\" me=\"Style375\" /><EditorStyle parent=\"Style272\" me=\"Style" +
				"376\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style378\" /><GroupFooterStyle par" +
				"ent=\"Style271\" me=\"Style377\" /><ColumnDivider>WhiteSmoke,Single</ColumnDivider><" +
				"Height>15</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style273\" me=\"Style379\" /><Style parent=\"Style271\" me=\"Style380\" /><" +
				"FooterStyle parent=\"Style274\" me=\"Style381\" /><EditorStyle parent=\"Style272\" me=" +
				"\"Style382\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style384\" /><GroupFooterSty" +
				"le parent=\"Style271\" me=\"Style383\" /><ColumnDivider>WhiteSmoke,Single</ColumnDiv" +
				"ider><Height>15</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style273\" me=\"Style385\" /><Style parent=\"Style271\" me=\"Style38" +
				"6\" /><FooterStyle parent=\"Style274\" me=\"Style387\" /><EditorStyle parent=\"Style27" +
				"2\" me=\"Style388\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style390\" /><GroupFoo" +
				"terStyle parent=\"Style271\" me=\"Style389\" /><ColumnDivider>WhiteSmoke,Single</Col" +
				"umnDivider><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style273\" me=\"Style391\" /><Style parent=\"Style271\" me=\"S" +
				"tyle392\" /><FooterStyle parent=\"Style274\" me=\"Style393\" /><EditorStyle parent=\"S" +
				"tyle272\" me=\"Style394\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style396\" /><Gr" +
				"oupFooterStyle parent=\"Style271\" me=\"Style395\" /><ColumnDivider>WhiteSmoke,Singl" +
				"e</ColumnDivider><Height>15</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style273\" me=\"Style397\" /><Style parent=\"Style271\"" +
				" me=\"Style398\" /><FooterStyle parent=\"Style274\" me=\"Style399\" /><EditorStyle par" +
				"ent=\"Style272\" me=\"Style400\" /><GroupHeaderStyle parent=\"Style271\" me=\"Style402\"" +
				" /><GroupFooterStyle parent=\"Style271\" me=\"Style401\" /><ColumnDivider>WhiteSmoke" +
				",Single</ColumnDivider><Height>15</Height><DCIdx>18</DCIdx></C1DisplayColumn></i" +
				"nternalCols><ClientRect>0, 0, 996, 289</ClientRect><BorderSide>Right</BorderSide" +
				"></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Norm" +
				"al\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" " +
				"/><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /" +
				"><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><St" +
				"yle parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><S" +
				"tyle parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /" +
				"><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></" +
				"NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified" +
				"</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 996, 289</" +
				"ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style76\" /><PrintPageFooterStyle " +
				"parent=\"\" me=\"Style77\" /></Blob>";
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
			this.UsePgpCheck.CheckedChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// ShowDataMaskButton
			// 
			this.ShowDataMaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ShowDataMaskButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ShowDataMaskButton.Location = new System.Drawing.Point(760, 380);
			this.ShowDataMaskButton.Name = "ShowDataMaskButton";
			this.ShowDataMaskButton.Size = new System.Drawing.Size(120, 21);
			this.ShowDataMaskButton.TabIndex = 10;
			this.ShowDataMaskButton.Text = "Show Data Mask";
			this.ShowDataMaskButton.Click += new System.EventHandler(this.ShowDataMaskButton_Click);
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
			this.DeskDropDown.ExtendRightColumn = true;
			this.DeskDropDown.FetchRowStyles = false;
			this.DeskDropDown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.DeskDropDown.Location = new System.Drawing.Point(72, 56);
			this.DeskDropDown.Name = "DeskDropDown";
			this.DeskDropDown.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.DeskDropDown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.DeskDropDown.RowHeight = 15;
			this.DeskDropDown.RowSubDividerColor = System.Drawing.Color.DarkGray;
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
				"t;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert" +
				":Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Filte" +
				"rBar{}Style5{}Style4{}Style9{}Style8{}Style12{}Group{BackColor:ControlDark;Borde" +
				"r:None,,0, 0, 0, 0;AlignVert:Center;}Style7{}Style6{}Style1{}Style3{}Style2{}</D" +
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
				"er>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style pare" +
				"nt=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorSt" +
				"yle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><Col" +
				"umnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C" +
				"1DisplayColumn></internalCols><ClientRect>0, 0, 244, 196</ClientRect><BorderSide" +
				">0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style p" +
				"arent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Head" +
				"ing\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading" +
				"\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" " +
				"me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" " +
				"me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"" +
				"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption" +
				"\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits" +
				"><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea" +
				">0, 0, 244, 196</ClientArea></Blob>";
			// 
			// FilePathTextBox
			// 
			this.FilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.FilePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FilePathTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FilePathTextBox.Location = new System.Drawing.Point(112, 308);
			this.FilePathTextBox.Name = "FilePathTextBox";
			this.FilePathTextBox.Size = new System.Drawing.Size(200, 19);
			this.FilePathTextBox.TabIndex = 1;
			this.FilePathTextBox.Tag = null;
			this.FilePathTextBox.TextDetached = true;
			this.FilePathTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// FileHostTextBox
			// 
			this.FileHostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.FileHostTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FileHostTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FileHostTextBox.Location = new System.Drawing.Point(112, 332);
			this.FileHostTextBox.Name = "FileHostTextBox";
			this.FileHostTextBox.Size = new System.Drawing.Size(200, 19);
			this.FileHostTextBox.TabIndex = 2;
			this.FileHostTextBox.Tag = null;
			this.FileHostTextBox.TextDetached = true;
			this.FileHostTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// FileUserNameTextBox
			// 
			this.FileUserNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.FileUserNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FileUserNameTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FileUserNameTextBox.Location = new System.Drawing.Point(112, 356);
			this.FileUserNameTextBox.Name = "FileUserNameTextBox";
			this.FileUserNameTextBox.Size = new System.Drawing.Size(200, 19);
			this.FileUserNameTextBox.TabIndex = 3;
			this.FileUserNameTextBox.Tag = null;
			this.FileUserNameTextBox.TextDetached = true;
			this.FileUserNameTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// FilePasswordTextBox
			// 
			this.FilePasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.FilePasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FilePasswordTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FilePasswordTextBox.Location = new System.Drawing.Point(112, 380);
			this.FilePasswordTextBox.Name = "FilePasswordTextBox";
			this.FilePasswordTextBox.Size = new System.Drawing.Size(200, 19);
			this.FilePasswordTextBox.TabIndex = 4;
			this.FilePasswordTextBox.Tag = null;
			this.FilePasswordTextBox.TextDetached = true;
			this.FilePasswordTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// LoadExPgpTextBox
			// 
			this.LoadExPgpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LoadExPgpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.LoadExPgpTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LoadExPgpTextBox.Location = new System.Drawing.Point(472, 356);
			this.LoadExPgpTextBox.Name = "LoadExPgpTextBox";
			this.LoadExPgpTextBox.Size = new System.Drawing.Size(200, 19);
			this.LoadExPgpTextBox.TabIndex = 7;
			this.LoadExPgpTextBox.Tag = null;
			this.LoadExPgpTextBox.TextDetached = true;
			this.LoadExPgpTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// CommentTextBox
			// 
			this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CommentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CommentTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.CommentTextBox.Location = new System.Drawing.Point(792, 308);
			this.CommentTextBox.Name = "CommentTextBox";
			this.CommentTextBox.Size = new System.Drawing.Size(200, 19);
			this.CommentTextBox.TabIndex = 9;
			this.CommentTextBox.Tag = null;
			this.CommentTextBox.TextDetached = true;
			this.CommentTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// MailAddressTextBox
			// 
			this.MailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.MailAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MailAddressTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.MailAddressTextBox.Location = new System.Drawing.Point(472, 308);
			this.MailAddressTextBox.Name = "MailAddressTextBox";
			this.MailAddressTextBox.Size = new System.Drawing.Size(200, 19);
			this.MailAddressTextBox.TabIndex = 5;
			this.MailAddressTextBox.Tag = null;
			this.MailAddressTextBox.TextDetached = true;
			this.MailAddressTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
			// 
			// MailSubjectTextBox
			// 
			this.MailSubjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.MailSubjectTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MailSubjectTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.MailSubjectTextBox.Location = new System.Drawing.Point(472, 332);
			this.MailSubjectTextBox.Name = "MailSubjectTextBox";
			this.MailSubjectTextBox.Size = new System.Drawing.Size(200, 19);
			this.MailSubjectTextBox.TabIndex = 6;
			this.MailSubjectTextBox.Tag = null;
			this.MailSubjectTextBox.TextDetached = true;
			this.MailSubjectTextBox.TextChanged += new System.EventHandler(this.SubscriptionValues_Changed);
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
			this.MailAddressLabel.Location = new System.Drawing.Point(344, 308);
			this.MailAddressLabel.Name = "MailAddressLabel";
			this.MailAddressLabel.Size = new System.Drawing.Size(128, 21);
			this.MailAddressLabel.TabIndex = 138;
			this.MailAddressLabel.Tag = null;
			this.MailAddressLabel.Text = "Mail Address:";
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
			this.CommentLabel.Location = new System.Drawing.Point(712, 308);
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
			this.MailSubjectLabel.Location = new System.Drawing.Point(344, 332);
			this.MailSubjectLabel.Name = "MailSubjectLabel";
			this.MailSubjectLabel.Size = new System.Drawing.Size(128, 21);
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
			this.LastUpdateLabel.Location = new System.Drawing.Point(712, 332);
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
			this.LastUpdateInfoLabel.Location = new System.Drawing.Point(792, 332);
			this.LastUpdateInfoLabel.Name = "LastUpdateInfoLabel";
			this.LastUpdateInfoLabel.Size = new System.Drawing.Size(200, 21);
			this.LastUpdateInfoLabel.TabIndex = 145;
			this.LastUpdateInfoLabel.Tag = null;
			this.LastUpdateInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LastUpdateInfoLabel.TextDetached = true;
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.SubscriptionSendToMenuItem,
																																										this.InventorySendToMenuItem,
																																										this.Sep1MenuItem,
																																										this.ExitMenuItem});
			// 
			// InventorySendToMenuItem
			// 
			this.InventorySendToMenuItem.Index = 1;
			this.InventorySendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																														this.SendToClipboardMenuItem,
																																														this.SendToEmailMenuItem});
			this.InventorySendToMenuItem.Text = "Inventory Send To";
			// 
			// SendToClipboardMenuItem
			// 
			this.SendToClipboardMenuItem.Index = 0;
			this.SendToClipboardMenuItem.Text = "Clipboard";
			this.SendToClipboardMenuItem.Click += new System.EventHandler(this.SendToClipboardMenuItem_Click);
			// 
			// SendToEmailMenuItem
			// 
			this.SendToEmailMenuItem.Index = 1;
			this.SendToEmailMenuItem.Text = "Mail Recipient";
			this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
			// 
			// Sep1MenuItem
			// 
			this.Sep1MenuItem.Index = 2;
			this.Sep1MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 3;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// SubscriptionSendToMenuItem
			// 
			this.SubscriptionSendToMenuItem.Index = 0;
			this.SubscriptionSendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																															 this.SubscriptionSendToClipboardMenuItem,
																																															 this.SubscriptionSendToEmailMenuItem});
			this.SubscriptionSendToMenuItem.Text = "Subscription Send To";
			// 
			// SubscriptionSendToClipboardMenuItem
			// 
			this.SubscriptionSendToClipboardMenuItem.Index = 0;
			this.SubscriptionSendToClipboardMenuItem.Text = "Clipboard";
			this.SubscriptionSendToClipboardMenuItem.Click += new System.EventHandler(this.SubscriptionSendToClipboardMenuItem_Click);
			// 
			// SubscriptionSendToEmailMenuItem
			// 
			this.SubscriptionSendToEmailMenuItem.Index = 1;
			this.SubscriptionSendToEmailMenuItem.Text = "Mail Recipient";
			this.SubscriptionSendToEmailMenuItem.Click += new System.EventHandler(this.SubscriptionSendToEmailMenuItem_Click);
			// 
			// InventorySubscriberForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1000, 413);
			this.ContextMenu = this.MainContextMenu;
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
			this.Controls.Add(this.ShowDataMaskButton);
			this.Controls.Add(this.UsePgpCheck);
			this.Controls.Add(this.SaveChangesButton);
			this.Controls.Add(this.SubscriberGrid);
			this.DockPadding.Bottom = 120;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.Color.Navy;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InventorySubscriberForm";
			this.Text = "Inventory Subscriber";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.InventorySubscriberForm_Closing);
			this.Load += new System.EventHandler(this.InventorySubscriberForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.SubscriberGrid)).EndInit();
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
	
		private void SubscriberGridLoad()
		{
			DataSet ds = new DataSet();
			try
			{

				ds = mainForm.ServiceAgent.SubscriberListGet(mainForm.UtcOffset);
				SubscriberGrid.SetDataBinding(ds, "SubscriberList", true);
          
				FilePathTextBox.Text     = SubscriberGrid.Columns["FilePathName"].Text;
				FileHostTextBox.Text     = SubscriberGrid.Columns["FileHost"].Text;
				FileUserNameTextBox.Text = SubscriberGrid.Columns["FileUserName"].Text;
				FilePasswordTextBox.Text = SubscriberGrid.Columns["FilePassword"].Text;
				LoadExPgpTextBox.Text    = SubscriberGrid.Columns["LoadExtensionPgp"].Text;
				CommentTextBox.Text      = SubscriberGrid.Columns["Comment"].Text;
				MailAddressTextBox.Text  = SubscriberGrid.Columns["MailAddress"].Text;
				MailSubjectTextBox.Text  = SubscriberGrid.Columns["MailSubject"].Text;
				UsePgpCheck.Checked   = bool.Parse(SubscriberGrid.Columns["UsePgp"].Text.Trim());
			}
			catch(Exception error)
			{
				Log.Write(error.Message + "[" + this.Name + ".SubscriberGridLoad]", 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SubscriptionValues_Changed(object sender, System.EventArgs e)
		{
			SaveChangesButton.Enabled = true;
		}

		private void SubscriberGrid_RowColChange(object sender,  C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			if((e.LastRow != SubscriberGrid.Row)  && SaveChangesButton.Enabled)
			{
				mainForm.Alert("Subscription for: " + SubscriberGrid.Columns["Desk"].CellText(e.LastRow) + " was not modified.", PilotState.RunFault);
			}

			if(e.LastRow != SubscriberGrid.Row) 
			{
				FilePathTextBox.Text     = SubscriberGrid.Columns["FilePathName"].Text;
				FileHostTextBox.Text     = SubscriberGrid.Columns["FileHost"].Text;
				FileUserNameTextBox.Text = SubscriberGrid.Columns["FileUserName"].Text;
				FilePasswordTextBox.Text = SubscriberGrid.Columns["FilePassword"].Text;
				LoadExPgpTextBox.Text    = SubscriberGrid.Columns["LoadExtensionPgp"].Text;
				CommentTextBox.Text      = SubscriberGrid.Columns["Comment"].Text;
				MailAddressTextBox.Text  = SubscriberGrid.Columns["MailAddress"].Text;
				MailSubjectTextBox.Text  = SubscriberGrid.Columns["MailSubject"].Text;
				UsePgpCheck.Checked   = (SubscriberGrid.Columns["usePgp"].Text.Equals("")?false: bool.Parse(SubscriberGrid.Columns["usePgp"].Text));

				LastUpdateInfoLabel.Text = SubscriberGrid.Columns["Actor"].Text + " " + Tools.FormatDate(SubscriberGrid.Columns["ActTime"].Text, Standard.DateTimeFileFormat);
        
				SaveChangesButton.Enabled = false;
        
				SubscriberGrid.Columns["Desk"].DropDown = null; //If editng has taken place and then a row change, remove the dropdown menu.
			}
		}

		private void SaveChangesButton_Click(object sender,  System.EventArgs e)
		{
			try
			{
				SubscriberGrid.Columns["FilePathName"].Text      = FilePathTextBox.Text;
				SubscriberGrid.Columns["FileHost"].Text          = FileHostTextBox.Text;
				SubscriberGrid.Columns["FileUserName"].Text      = FileUserNameTextBox.Text;
				SubscriberGrid.Columns["FilePassword"].Text      = FilePasswordTextBox.Text;
				SubscriberGrid.Columns["LoadExtensionPgp"].Text  = LoadExPgpTextBox.Text;
				SubscriberGrid.Columns["Comment"].Text           = CommentTextBox.Text;
				SubscriberGrid.Columns["MailAddress"].Text       = MailAddressTextBox.Text;
				SubscriberGrid.Columns["MailSubject"].Text       = MailSubjectTextBox.Text;
				SubscriberGrid.Columns["UsePgp"].Value           = UsePgpCheck.Checked;      
               
				mainForm.ServiceAgent.SubscriberListSet(
					SubscriberGrid.Columns["Desk"].Text, 
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
					bool.Parse(SubscriberGrid.Columns["IsBizDatePrior"].Value.ToString()),
					mainForm.UserId
					);    
  
				SubscriberGrid.Columns["Actor"].Text = mainForm.UserId;
				SubscriberGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
        
				mainForm.Alert("Subscription for: " + SubscriberGrid.Columns["Desk"].Text + " has been sucessfully modified.", PilotState.Normal);
				Log.Write("Subscription for: " + SubscriberGrid.Columns["Desk"].Text + " has been sucessfully modified. [" + this.Name + ".SaveChangesButton_Click]", 4);
        
				SubscriberGrid.Columns["Desk"].DropDown = null; 
        
				SubscriberGrid.UpdateData();
      
				SaveChangesButton.Enabled = false;
			}
			catch(Exception error)
			{
				Log.Write(error.Message + "[InventorySubscriberForm.SaveChangesButton_Click]", 1);
				return;
			}
		}

		private void InventorySubscriberForm_Closing(object sender,  System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());   
			}
		}

		private void ShowDataMaskButton_Click(object sender,  System.EventArgs e)
		{
			try
			{
				InventoryDataMaskForm inventoryDataMasksForm = new InventoryDataMaskForm(mainForm, SubscriberGrid.Columns["Desk"].Text);
				inventoryDataMasksForm.MdiParent = mainForm;
				inventoryDataMasksForm.Show();
			}
			catch(Exception error)
			{
				Log.Write(error.Message + "[InventorySubscriberForm.ShowDataMaskButton_Click]", 1);
				mainForm.Alert(error.Message, PilotState.RunFault);        
			}
		}

		private void InventorySubscriberForm_Load(object sender, System.EventArgs e)
		{    
			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "440"));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1008"));
    
			SubscriberGridLoad();
			SaveChangesButton.Enabled = false;

			ToolTip toolTip = new ToolTip();

			toolTip.AutoPopDelay = 15000;
			toolTip.InitialDelay = 1000;
			toolTip.ReshowDelay  = 500;

			toolTip.ShowAlways = true;

			toolTip.SetToolTip(this.FilePathTextBox,
				"Enter the path of the file on the server.");

			toolTip.SetToolTip(this.FileHostTextBox,
				"Enter the address of the server where the file is located.");

			toolTip.SetToolTip(this.FileUserNameTextBox,
				"Enter the username to access the server.");
     
			toolTip.SetToolTip(this.FilePasswordTextBox,
				"Enter the password for the username used to access the server.");

			toolTip.SetToolTip(this.LoadExPgpTextBox,
				"Enter the path to the pgp dll you wish to use, leave blank to use defualt.");
      
			toolTip.SetToolTip(this.CommentTextBox,
				"Enter a comment for the subscription.");

			toolTip.SetToolTip(this.MailAddressTextBox,
				"Enter the email address of the sender of the file.");

			toolTip.SetToolTip(this.MailSubjectTextBox,
				"Enter the email subject.");

			toolTip.SetToolTip(this.UsePgpCheck,
				"Check if the file is pgp encrypted, Uncheck otherwise.");
		}

		private void SubscriberGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{      
			switch(SubscriberGrid.Columns[e.ColIndex].DataField)
			{
				case("BizDate"):          
					e.Value =  Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);                    
					break;      
          
				case ("LoadCount"):
					e.Value = Tools.FormatPadNumber(e.Value.ToString(), "#,###,###");
					break;
  
				case("LoadTime"):
				case("FileCheckTime"):        
				case("FileTime"):
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeFileFormat);
					break;
      
				case("ActTime"):
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);                                   
					break;            
			}
		}

		private void SubscriberGrid_OnAddNew(object sender, System.EventArgs e)
		{
			FilePathTextBox.Text = "";
			FileHostTextBox.Text = ""; 
			FileUserNameTextBox.Text = ""; 
			FilePasswordTextBox.Text = ""; 
			LoadExPgpTextBox.Text = "";
			CommentTextBox.Text = "";                             
			MailAddressTextBox.Text = "";
			MailSubjectTextBox.Text = ""; 
			UsePgpCheck.Checked = false;
		}

		private void SubscriberGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			DataSet deskSet;
      
			if((SubscriberGrid.Columns[e.ColIndex].DataField.Equals("Desk") && SubscriberGrid.Columns[e.ColIndex].Text.Length < 1) || SubscriberGrid.DataChanged)
			{ 
				try
				{
					deskSet = mainForm.ServiceAgent.DeskGet(true);
					SubscriberGrid.Columns["Desk"].DropDown = DeskDropDown;
					DeskDropDown.SetDataBinding(deskSet, "Desks", true);
					DeskDropDown.DataField = "Firm";
					DeskDropDown.ListField = "Desk";
					DeskDropDown.ValueTranslate = true;                            					
				}
				catch(Exception error)
				{
					Log.Write(error.Message + "[InventorySubscriberForm.SubscriberGrid_BeforeColEdit]", 3);
					SubscriberGrid.Columns["Desk"].DropDown = null;
					return;
				}
			}			
			else if (e.Column.DataColumn.DataField.Equals("IsBizDatePrior"))
			{
				SaveChangesButton.Enabled = true;
			}
			else if (!e.Column.DataColumn.DataField.Equals("IsBizDatePrior"))
			{
				mainForm.Alert(SubscriberGrid.Columns[e.ColIndex].DataField);
				SubscriberGrid.Columns["Desk"].DropDown = null;
				mainForm.Alert("You cannot edit current cell.", PilotState.RunFault);
				e.Cancel = true;        
				return ;
			}
		}

		private void SubscriberGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				SubscriberGrid.Columns["FilePathName"].Text      = FilePathTextBox.Text;
				SubscriberGrid.Columns["FileHost"].Text          = FileHostTextBox.Text;
				SubscriberGrid.Columns["FileUserName"].Text      = FileUserNameTextBox.Text;
				SubscriberGrid.Columns["FilePassword"].Text      = FilePasswordTextBox.Text;
				SubscriberGrid.Columns["LoadExtensionPgp"].Text  = LoadExPgpTextBox.Text;
				SubscriberGrid.Columns["Comment"].Text           = CommentTextBox.Text;
				SubscriberGrid.Columns["MailAddress"].Text       = MailAddressTextBox.Text;
				SubscriberGrid.Columns["MailSubject"].Text       = MailSubjectTextBox.Text;
				SubscriberGrid.Columns["UsePgp"].Value           = UsePgpCheck.Checked;
               
				mainForm.ServiceAgent.SubscriberListSet(
					SubscriberGrid.Columns["Desk"].Text, 
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
					bool.Parse(SubscriberGrid.Columns["IsBizDatePrior"].Value.ToString()),
					mainForm.UserId
					); 
        
				mainForm.Alert("Subscription for: " + SubscriberGrid.Columns["Desk"].Text + " has been sucessfully deleted.", PilotState.Normal);
				Log.Write("Subscription for: " + SubscriberGrid.Columns["Desk"].Text + " has been sucessfully deleted. [" + this.Name + ".SubscriberGrid_BeforeDelete]", 3);
        
				SubscriberGrid.Columns["Desk"].DropDown = null; 
        
				SubscriberGrid.UpdateData();
			}
			catch(Exception error)
			{
				Log.Write(error.Message + "[InventorySubscriberForm.SubscriberGrid_BeforeDelete]", 3);
				return;
			}
		}

		private string DeskListGet(string bizDate, string desk)
		{
			DataTable deskList;

			mainForm.Alert("Please wait... Loading inventory for " + desk + " for " + bizDate + ".", PilotState.Unknown);

			try
			{
				deskList = mainForm.ShortSaleAgent.InventoryDeskListGet(bizDate,desk);
      
				StringBuilder listData = new StringBuilder("1 " + bizDate.PadRight(13, ' ') + desk.PadRight(12, ' ') + "\r\n");

				foreach (DataRow dataRow in deskList.Rows)
				{
					listData.Append("2 " + dataRow["SecId"].ToString().PadRight(13, ' ')
						+ dataRow["Symbol"].ToString().PadRight(13, ' ')
						+ dataRow["Quantity"].ToString().PadRight(15, ' ')
						+ "\r\n");
				}

				listData.Append("3 " + deskList.Rows.Count.ToString().PadRight(6, ' ') + "\r\n");

				mainForm.Alert("Done. Loaded " + deskList.Rows.Count + " inventory items for "
					+ SubscriberGrid.Columns["Desk"].Text + " for "
					+ SubscriberGrid.Columns["BizDate"].Text + ".", PilotState.Normal);

				return listData.ToString();
			}      
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}

			return "Sorry... Error loading the list.";
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			SendToClipboardDelegate sendToClipboardDelegate = new SendToClipboardDelegate(SendToClipboard);
			sendToClipboardDelegate.BeginInvoke(SubscriberGrid.Columns["BizDate"].Text, SubscriberGrid.Columns["Desk"].Text, null, null);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			SendToEmailDelegate sendToEmailDelegate = new SendToEmailDelegate(SendToEmail);			
			sendToEmailDelegate.BeginInvoke(SubscriberGrid.Columns["BizDate"].Text, SubscriberGrid.Columns["Desk"].Text, null, null);
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();   
		}

		private void SubscriptionSendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubscriberGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in SubscriberGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubscriberGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + SubscriberGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);	
		}

		private void SubscriptionSendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (SubscriberGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[SubscriberGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubscriberGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in SubscriberGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubscriberGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubscriberGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubscriberGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in SubscriberGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubscriberGrid.SelectedCols)
					{
						if (dataColumn.Value.GetType().Equals(typeof(System.String)))
						{
							gridData += dataColumn.CellText(rowIndex).PadRight(maxTextLength[++columnIndex] + 2);
						}
						else
						{
							gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
						}
					}
  
					gridData += "\n";
				}
                
				Email email = new Email();
				email.Send(gridData);

				mainForm.Alert("Total: " + SubscriberGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.Normal);
			}
		}
	}
}
