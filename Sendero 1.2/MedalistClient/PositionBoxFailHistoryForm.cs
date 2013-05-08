using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class PositionBoxFailHistoryForm : System.Windows.Forms.Form
  {
    private DataSet dataSet;
    private DataView dataView;

    private string bookGroup;

    private MainForm mainForm;
    private C1.Win.C1List.C1List FailHistoryList;

    private System.ComponentModel.Container components = null;

    public PositionBoxFailHistoryForm(MainForm mainForm, string bookGroup)
    {
      this.mainForm = mainForm;
      this.bookGroup = bookGroup;
      InitializeComponent();
    }

    protected override void Dispose( bool disposing )
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionBoxFailHistoryForm));
      this.FailHistoryList = new C1.Win.C1List.C1List();
      ((System.ComponentModel.ISupportInitialize)(this.FailHistoryList)).BeginInit();
      this.SuspendLayout();
      // 
      // FailHistoryList
      // 
      this.FailHistoryList.AddItemSeparator = ';';
      this.FailHistoryList.AllowColMove = false;
      this.FailHistoryList.AllowColSelect = false;
      this.FailHistoryList.AllowSort = false;
      this.FailHistoryList.AlternatingRows = true;
      this.FailHistoryList.Caption = "";
      this.FailHistoryList.CaptionHeight = 17;
      this.FailHistoryList.ColumnCaptionHeight = 17;
      this.FailHistoryList.ColumnFooterHeight = 17;
      this.FailHistoryList.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
      this.FailHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FailHistoryList.EmptyRows = true;
      this.FailHistoryList.ExtendRightColumn = true;
      this.FailHistoryList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
      this.FailHistoryList.ItemHeight = 15;
      this.FailHistoryList.Location = new System.Drawing.Point(1, 1);
      this.FailHistoryList.MatchEntryTimeout = ((long)(2000));
      this.FailHistoryList.Name = "FailHistoryList";
      this.FailHistoryList.PartialRightColumn = false;
      this.FailHistoryList.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.FailHistoryList.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
      this.FailHistoryList.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.FailHistoryList.Size = new System.Drawing.Size(910, 248);
      this.FailHistoryList.TabIndex = 0;
      this.FailHistoryList.Text = "Fails";
      this.FailHistoryList.FetchCellStyle += new C1.Win.C1List.FetchCellStyleEventHandler(this.FailHistoryList_FetchCellStyle);
      this.FailHistoryList.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.FailHistoryList_FormatText);
      this.FailHistoryList.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Settle Date" +
        "\" DataField=\"BizDate\" NumberFormat=\"FormatText Event\"><ValueItems /></C1DataColu" +
        "mn><C1DataColumn Level=\"0\" Caption=\"Ex Deficit\" DataField=\"ExDeficitSettled\" Num" +
        "berFormat=\"FormatText Event\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0" +
        "\" Caption=\"DVP Recv\" DataField=\"DvpFailInSettled\" NumberFormat=\"FormatText Event" +
        "\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"DVP Dlvr\" DataFi" +
        "eld=\"DvpFailOutSettled\" NumberFormat=\"FormatText Event\"><ValueItems /></C1DataCo" +
        "lumn><C1DataColumn Level=\"0\" Caption=\"Broker Recv\" DataField=\"BrokerFailInSettle" +
        "d\" NumberFormat=\"FormatText Event\"><ValueItems /></C1DataColumn><C1DataColumn Le" +
        "vel=\"0\" Caption=\"Broker Dlvr\" DataField=\"BrokerFailOutSettled\" NumberFormat=\"For" +
        "matText Event\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Cle" +
        "aring Recv\" DataField=\"ClearingFailInSettled\" NumberFormat=\"FormatText Event\"><V" +
        "alueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Clearing Dlvr\" DataF" +
        "ield=\"ClearingFailOutSettled\" NumberFormat=\"FormatText Event\"><ValueItems /></C1" +
        "DataColumn><C1DataColumn Level=\"0\" Caption=\"Other Recv\" DataField=\"OtherFailInSe" +
        "ttled\" NumberFormat=\"FormatText Event\"><ValueItems /></C1DataColumn><C1DataColum" +
        "n Level=\"0\" Caption=\"Other Dlvr\" DataField=\"OtherFailOutSettled\" NumberFormat=\"F" +
        "ormatText Event\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1" +
        "List.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor" +
        ":Highlight;}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{Align" +
        "Horz:Far;}Style24{AlignHorz:Near;}Style18{AlignHorz:Near;}Style19{AlignHorz:Far;" +
        "}Style14{}Style15{AlignHorz:Near;}Style16{AlignHorz:Far;}Style17{}Style10{}Style" +
        "11{}OddRow{}Style13{AlignHorz:Near;}Style12{AlignHorz:Near;}Style29{}Style28{Ali" +
        "gnHorz:Far;}Style27{AlignHorz:Near;}Style26{}RecordSelector{AlignImage:Center;}F" +
        "ooter{}Style23{}Style22{AlignHorz:Far;}Style21{AlignHorz:Near;}Style20{}Group{Al" +
        "ignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Inactive{ForeColor" +
        ":InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:LightCyan;}Hea" +
        "ding{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText" +
        ";AlignVert:Center;}Style3{}Style7{}Style6{}Style5{}Style41{}Style40{Font:Verdana" +
        ", 8.25pt, style=Bold;AlignHorz:Far;}Style8{}Style1{}Selected{ForeColor:Highlight" +
        "Text;BackColor:Highlight;}Style4{}Style9{AlignHorz:Near;}Style38{}Style39{AlignH" +
        "orz:Near;}Style36{AlignHorz:Near;}Style37{AlignHorz:Far;}Style34{AlignHorz:Far;}" +
        "Style35{}Style32{}Style33{AlignHorz:Near;}Style30{AlignHorz:Near;}Style31{AlignH" +
        "orz:Far;}Style2{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColMove" +
        "=\"False\" AllowColSelect=\"False\" Name=\"\" AlternatingRowStyle=\"True\" CaptionHeight" +
        "=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" " +
        "VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 906, 244</Cl" +
        "ientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style1" +
        "2\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
        "e14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivide" +
        "r><Width>85</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1Displ" +
        "ayColumn><HeadingStyle parent=\"Style2\" me=\"Style39\" /><Style parent=\"Style1\" me=" +
        "\"Style40\" /><FooterStyle parent=\"Style3\" me=\"Style41\" /><ColumnDivider><Color>Da" +
        "rkGray</Color><Style>Single</Style></ColumnDivider><Width>90</Width><Height>15</" +
        "Height><FetchStyle>True</FetchStyle><DCIdx>1</DCIdx></C1DisplayColumn><C1Display" +
        "Column><HeadingStyle parent=\"Style2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"S" +
        "tyle16\" /><FooterStyle parent=\"Style3\" me=\"Style17\" /><ColumnDivider><Color>Dark" +
        "Gray</Color><Style>Single</Style></ColumnDivider><Width>90</Width><Height>15</He" +
        "ight><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
        "yle2\" me=\"Style18\" /><Style parent=\"Style1\" me=\"Style19\" /><FooterStyle parent=\"" +
        "Style3\" me=\"Style20\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Styl" +
        "e></ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>3</DCIdx></C1Displa" +
        "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style21\" /><Style par" +
        "ent=\"Style1\" me=\"Style22\" /><FooterStyle parent=\"Style3\" me=\"Style23\" /><ColumnD" +
        "ivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>90</Wi" +
        "dth><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
        "ngStyle parent=\"Style2\" me=\"Style24\" /><Style parent=\"Style1\" me=\"Style25\" /><Fo" +
        "oterStyle parent=\"Style3\" me=\"Style26\" /><ColumnDivider><Color>DarkGray</Color><" +
        "Style>Single</Style></ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>5" +
        "</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
        "le27\" /><Style parent=\"Style1\" me=\"Style28\" /><FooterStyle parent=\"Style3\" me=\"S" +
        "tyle29\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDiv" +
        "ider><Width>90</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1Di" +
        "splayColumn><HeadingStyle parent=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" " +
        "me=\"Style31\" /><FooterStyle parent=\"Style3\" me=\"Style32\" /><ColumnDivider><Color" +
        ">DarkGray</Color><Style>Single</Style></ColumnDivider><Width>90</Width><Height>1" +
        "5</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
        "t=\"Style2\" me=\"Style33\" /><Style parent=\"Style1\" me=\"Style34\" /><FooterStyle par" +
        "ent=\"Style3\" me=\"Style35\" /><ColumnDivider><Color>DarkGray</Color><Style>Single<" +
        "/Style></ColumnDivider><Width>90</Width><Height>15</Height><DCIdx>8</DCIdx></C1D" +
        "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style36\" /><Styl" +
        "e parent=\"Style1\" me=\"Style37\" /><FooterStyle parent=\"Style3\" me=\"Style38\" /><Co" +
        "lumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>9" +
        "0</Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn></internalCols><VS" +
        "crollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollB" +
        "ar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me" +
        "=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group" +
        "\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle" +
        " parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4" +
        "\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"Reco" +
        "rdSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style" +
        " parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles" +
        "><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style par" +
        "ent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent" +
        "=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=" +
        "\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent" +
        "=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style par" +
        "ent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1" +
        "</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth" +
        "></Blob>";
      // 
      // PositionBoxFailHistoryForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(912, 250);
      this.Controls.Add(this.FailHistoryList);
      this.DockPadding.All = 1;
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "PositionBoxFailHistoryForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Fail History ";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.PositionBoxFailHistoryForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.FailHistoryList)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    private void BoxFailHistoryGet()
    {
      try
      {
        mainForm.Alert("Please wait... Loading current fail history...", PilotState.Unknown);
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();
        
        dataSet = mainForm.PositionAgent.BoxFailHistoryGet(mainForm.SecId);
        
        dataView = new DataView(dataSet.Tables["FailHistory"], "BookGroup='" + bookGroup + "'","", DataViewRowState.CurrentRows);
        dataView.Sort = "BizDate DESC";

        FailHistoryList.HoldFields();
        FailHistoryList.DataSource = dataView;
 
        mainForm.Alert("Loading current fail history... Done!", PilotState.Normal);
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [PositionBoxFailHistoryForm.BoxFailsGet]", Log.Error, 1); 
      }

      this.Cursor = Cursors.Default;
    }

    private void PositionBoxFailHistoryForm_Load(object sender, System.EventArgs e)
    {
      this.Text = "Fail History for " + mainForm.SecId + " [" + mainForm.Symbol + "]";    
      this.Show();

      BoxFailHistoryGet();
    }

    private void FailHistoryList_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
    {
      if (e.Value.Length == 0)
      {
        return;
      }
  
      try
      {
        switch(FailHistoryList.Columns[e.ColIndex].DataField)
        {
          case ("BizDate"):
            e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
            break;

          case ("ExDeficitSettled"):
          case ("DvpFailInSettled"):
          case ("DvpFailOutSettled"):
          case ("BrokerFailInSettled"):
          case ("BrokerFailOutSettled"):
          case ("ClearingFailInSettled"):
          case ("ClearingFailOutSettled"):
          case ("OtherFailInSettled"):
          case ("OtherFailOutSettled"):
            e.Value = long.Parse(e.Value).ToString("#,##0");
            break;
        }
      }
      catch {}
    }

    private void FailHistoryList_FetchCellStyle(object sender, C1.Win.C1List.FetchCellStyleEventArgs e)
    {
      if (FailHistoryList.Columns[e.Col].Text.Equals(""))
      {
        return;
      }

      try
      {
        switch(FailHistoryList.Columns[e.Col].DataField)
        {
          case ("ExDeficitSettled"):
            if (long.Parse(FailHistoryList.Columns[e.Col].CellValue(e.Row).ToString()) < 0)
            {
              e.CellStyle.ForeColor = Color.Maroon;
            }
            else
            {
              e.CellStyle.ForeColor = Color.MidnightBlue;
            }
            break;
        }
      }
      catch {}
    }
  }
}
