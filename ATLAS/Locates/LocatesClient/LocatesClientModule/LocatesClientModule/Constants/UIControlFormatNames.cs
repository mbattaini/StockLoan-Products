using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockLoan.Locates.LocatesClientModule.Constants
{
    class UIControlFormatNames
    {
        public const string LocateSummaryListFormat = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group\" Data" +
                                                            "Field=\"GroupCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
                                                            "Request\" DataField=\"ClientQuantity\" NumberFormat=\"FormatText Event\"><ValueItems " +
                                                            "/></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Located\" DataField=\"Quantity\" " +
                                                            "NumberFormat=\"FormatText Event\"><ValueItems /></C1DataColumn></DataCols><Styles " +
                                                            "type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:N" +
                                                            "one,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}S" +
                                                            "tyle4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;" +
                                                            "BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:In" +
                                                            "activeCaption;}Style30{AlignHorz:Near;}Style32{}Footer{BackColor:Window;}Caption" +
                                                            "{AlignHorz:Center;}Style31{AlignHorz:Far;}Normal{Font:Verdana, 8.25pt;BackColor:" +
                                                            "Ivory;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}Style23" +
                                                            "{}Style22{AlignHorz:Near;}Style21{AlignHorz:Near;}OddRow{}RecordSelector{AlignIm" +
                                                            "age:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" +
                                                            "lor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{Ali" +
                                                            "gnHorz:Far;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBo" +
                                                            "xView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17" +
                                                            "\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" VerticalScrollGroup=\"1\" Horizon" +
                                                            "talScrollGroup=\"1\"><ClientRect>0, 17, 260, 169</ClientRect><internalCols><C1Disp" +
                                                            "layColumn><HeadingStyle parent=\"Style2\" me=\"Style21\" /><Style parent=\"Style1\" me" +
                                                            "=\"Style22\" /><FooterStyle parent=\"Style3\" me=\"Style23\" /><ColumnDivider><Color>D" +
                                                            "arkGray</Color><Style>Single</Style></ColumnDivider><Width>65</Width><Height>15<" +
                                                            "/Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
                                                            "\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle paren" +
                                                            "t=\"Style3\" me=\"Style32\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</S" +
                                                            "tyle></ColumnDivider><Width>85</Width><Height>15</Height><DCIdx>1</DCIdx></C1Dis" +
                                                            "playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style " +
                                                            "parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><Colu" +
                                                            "mnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>85<" +
                                                            "/Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn></internalCols><VScr" +
                                                            "ollBar><Width>16</Width><Style>Always</Style></VScrollBar><HScrollBar><Height>16" +
                                                            "</Height><Style>None</Style></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style" +
                                                            "9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" m" +
                                                            "e=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Hea" +
                                                            "ding\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><Inac" +
                                                            "tiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style" +
                                                            "8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle " +
                                                            "parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1" +
                                                            "List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style par" +
                                                            "ent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=" +
                                                            "\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"" +
                                                            "Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent" +
                                                            "=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Hea" +
                                                            "ding\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><" +
                                                            "vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Def" +
                                                            "aultRecSelWidth>16</DefaultRecSelWidth></Blob>";

        public const string TradingGroupComboFormat = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group\" Data" +
                                                                "Field=\"GroupCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
                                                                "Group Name\" DataField=\"GroupName\"><ValueItems /></C1DataColumn></DataCols><Style" +
                                                                "s type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border" +
                                                                ":None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{" +
                                                                "}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightTex" +
                                                                "t;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:" +
                                                                "InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;B" +
                                                                "ackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style" +
                                                                "1{}OddRow{}RecordSelector{AlignImage:Center;}Style13{AlignHorz:Center;}Heading{W" +
                                                                "rap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Align" +
                                                                "Vert:Center;}Style8{}Style10{}Style11{}Style14{}Style15{AlignHorz:Near;}Style16{" +
                                                                "AlignHorz:Near;}Style17{}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win." +
                                                                "C1List.ListBoxView AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"\" AllowRowS" +
                                                                "izing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\"" +
                                                                " ExtendRightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Cli" +
                                                                "entRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle " +
                                                                "parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyl" +
                                                                "e parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Si" +
                                                                "ngle</Style></ColumnDivider><Width>65</Width><Height>15</Height><DCIdx>0</DCIdx>" +
                                                                "</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style15\" />" +
                                                                "<Style parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"Style3\" me=\"Style17\" " +
                                                                "/><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Wi" +
                                                                "dth>250</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCo" +
                                                                "ls><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height><Sty" +
                                                                "le>None</Style></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
                                                                "Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
                                                                "<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
                                                                "le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
                                                                "ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
                                                                "electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
                                                                "ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
                                                                "iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
                                                                "me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
                                                                "\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
                                                                "elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
                                                                "\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
                                                                "ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
                                                                "/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
                                                                "th>16</DefaultRecSelWidth></Blob>";


        public const string TradeDateComboFormat = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Trade Dates" +
                                                            "\" DataField=\"TradeDate\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1" +
                                                            ".Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, " +
                                                            "0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}S" +
                                                            "tyle7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColo" +
                                                            "r:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCa" +
                                                            "ption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;BackColor:W" +
                                                            "indow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{" +
                                                            "}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Ra" +
                                                            "ised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style1" +
                                                            "1{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Spli" +
                                                            "ts><C1.Win.C1List.ListBoxView AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"" +
                                                            "\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalSc" +
                                                            "rollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><" +
                                                            "internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Styl" +
                                                            "e parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><Co" +
                                                            "lumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>" +
                                                            "15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>1" +
                                                            "6</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle " +
                                                            "parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><Foot" +
                                                            "erStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" />" +
                                                            "<HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highligh" +
                                                            "tRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle " +
                                                            "parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"S" +
                                                            "tyle10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" " +
                                                            "me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\"" +
                                                            " me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=" +
                                                            "\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"In" +
                                                            "active\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"High" +
                                                            "lightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"Odd" +
                                                            "Row\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=" +
                                                            "\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Lay" +
                                                            "out>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";


        public const string InventoryListFormat = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
                                                        "\" DataField=\"SecId\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
                                                        "=\"Received\" DataField=\"ScribeTime\" NumberFormat=\"FormatText Event\"><ValueItems /" +
                                                        "></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"For\" DataField=\"BizDate\" Number" +
                                                        "Format=\"FormatText Event\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" C" +
                                                        "aption=\"Desk\" DataField=\"Desk\"><ValueItems /></C1DataColumn><C1DataColumn Level=" +
                                                        "\"0\" Caption=\"Book\" DataField=\"Account\"><ValueItems /></C1DataColumn><C1DataColum" +
                                                        "n Level=\"0\" Caption=\"\" DataField=\"ModeCode\"><ValueItems /></C1DataColumn><C1Data" +
                                                        "Column Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatTex" +
                                                        "t Event\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Des" +
                                                        "ign.ContextWrapper\"><Data>Caption{AlignHorz:Center;}Style27{AlignHorz:Near;}Norm" +
                                                        "al{Font:Verdana, 8.25pt;BackColor:Honeydew;}Style25{AlignHorz:Near;}Selected{For" +
                                                        "eColor:HighlightText;BackColor:Highlight;}Style18{AlignHorz:Near;}Style19{AlignH" +
                                                        "orz:Near;}Style14{}Style15{AlignHorz:Near;}Style16{AlignHorz:Near;}Style17{}Styl" +
                                                        "e10{}Style11{}OddRow{}Style13{AlignHorz:Near;}Style12{AlignHorz:Near;}Style32{}S" +
                                                        "tyle31{AlignHorz:Far;}Footer{}Style29{}Style28{AlignHorz:Center;}HighlightRow{Fo" +
                                                        "reColor:HighlightText;BackColor:Highlight;}Style26{}RecordSelector{AlignImage:Ce" +
                                                        "nter;}Style24{AlignHorz:Near;}Style23{}Style22{AlignHorz:Near;}Style21{AlignHorz" +
                                                        ":Near;}Style20{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Ce" +
                                                        "nter;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{" +
                                                        "BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;For" +
                                                        "eColor:ControlText;BackColor:Control;}Style9{AlignHorz:Near;}Style8{}Style5{}Sty" +
                                                        "le4{}Style7{}Style6{}Style1{}Style30{AlignHorz:Near;}Style3{}Style2{}</Data></St" +
                                                        "yles><Splits><C1.Win.C1List.ListBoxView Name=\"\" CaptionHeight=\"17\" ColumnCaption" +
                                                        "Height=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"Tru" +
                                                        "e\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 17, 468, 169" +
                                                        "</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
                                                        "yle12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"" +
                                                        "Style14\" /><Visible>False</Visible><ColumnDivider><Color>DarkGray</Color><Style>" +
                                                        "Single</Style></ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>0</DCId" +
                                                        "x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style15\" " +
                                                        "/><Style parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"Style3\" me=\"Style17" +
                                                        "\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><" +
                                                        "Width>110</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Display" +
                                                        "Column><HeadingStyle parent=\"Style2\" me=\"Style18\" /><Style parent=\"Style1\" me=\"S" +
                                                        "tyle19\" /><FooterStyle parent=\"Style3\" me=\"Style20\" /><ColumnDivider><Color>Dark" +
                                                        "Gray</Color><Style>Single</Style></ColumnDivider><Width>75</Width><Height>15</He" +
                                                        "ight><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
                                                        "yle2\" me=\"Style21\" /><Style parent=\"Style1\" me=\"Style22\" /><FooterStyle parent=\"" +
                                                        "Style3\" me=\"Style23\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Styl" +
                                                        "e></ColumnDivider><Width>85</Width><Height>15</Height><DCIdx>3</DCIdx></C1Displa" +
                                                        "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style24\" /><Style par" +
                                                        "ent=\"Style1\" me=\"Style25\" /><FooterStyle parent=\"Style3\" me=\"Style26\" /><ColumnD" +
                                                        "ivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>60</Wi" +
                                                        "dth><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
                                                        "ngStyle parent=\"Style2\" me=\"Style27\" /><Style parent=\"Style1\" me=\"Style28\" /><Fo" +
                                                        "oterStyle parent=\"Style3\" me=\"Style29\" /><ColumnDivider><Color>DarkGray</Color><" +
                                                        "Style>None</Style></ColumnDivider><Width>20</Width><Height>15</Height><HeaderDiv" +
                                                        "ider>False</HeaderDivider><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
                                                        "adingStyle parent=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" />" +
                                                        "<FooterStyle parent=\"Style3\" me=\"Style32\" /><ColumnDivider><Color>DarkGray</Colo" +
                                                        "r><Style>Single</Style></ColumnDivider><Width>95</Width><Height>15</Height><DCId" +
                                                        "x>6</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>16</Width><Style>" +
                                                        "Always</Style></VScrollBar><HScrollBar><Height>16</Height><Style>None</Style></H" +
                                                        "ScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Even" +
                                                        "Row\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent" +
                                                        "=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightR" +
                                                        "owStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=" +
                                                        "\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle paren" +
                                                        "t=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /" +
                                                        "><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Name" +
                                                        "dStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><St" +
                                                        "yle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style" +
                                                        " parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style " +
                                                        "parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style" +
                                                        " parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><St" +
                                                        "yle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzS" +
                                                        "plits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecS" +
                                                        "elWidth></Blob>";

        public const string LocatesGridFormat = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Locate ID\" " +
                                                        "DataField=\"LocateId\" SortDirection=\"Descending\"><ValueItems /><GroupInfo /></C1D" +
                                                        "ataColumn><C1DataColumn Level=\"0\" Caption=\"ID\" DataField=\"LocateIdTail\" NumberFo" +
                                                        "rmat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
                                                        "Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C" +
                                                        "1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueIt" +
                                                        "ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Open At\" Data" +
                                                        "Field=\"OpenTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
                                                        "DataColumn><C1DataColumn Level=\"0\" Caption=\"From\" DataField=\"ClientId\"><ValueIte" +
                                                        "ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Group\" DataFie" +
                                                        "ld=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
                                                        " Caption=\"Request\" DataField=\"ClientQuantity\" NumberFormat=\"FormatText Event\"><V" +
                                                        "alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Status\"" +
                                                        " DataField=\"Status\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
                                                        "l=\"0\" Caption=\"Located\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><Va" +
                                                        "lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Source\" " +
                                                        "DataField=\"Source\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
                                                        "=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><GroupInfo /></C1DataCo" +
                                                        "lumn><C1DataColumn Level=\"0\" Caption=\"Fee\" DataField=\"FeeRate\"><ValueItems /><Gr" +
                                                        "oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PB\" DataField=\"PreBorr" +
                                                        "ow\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataCol" +
                                                        "umn Level=\"0\" Caption=\"Done At\" DataField=\"ActTime\" NumberFormat=\"FormatText Eve" +
                                                        "nt\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"B" +
                                                        "y\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
                                                        "aColumn Level=\"0\" Caption=\"Client Comment\" DataField=\"ClientComment\"><ValueItems" +
                                                        " /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Group Name\" Data" +
                                                        "Field=\"GroupName\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles t" +
                                                        "ype=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:Hig" +
                                                        "hlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor" +
                                                        ":InactiveCaption;}Style119{}Style118{}Style78{}Style79{}Style85{}Editor{ForeColo" +
                                                        "r:WindowText;}Style117{AlignHorz:Far;BackColor:White;}Style116{AlignHorz:Near;}S" +
                                                        "tyle72{}Style73{}Style70{}Style71{}Style76{}Style77{}Style74{}Style75{}Style84{}" +
                                                        "Style87{}Style86{}Style81{}Style80{}Style83{}Style82{}Footer{}Style89{}Style88{}" +
                                                        "Style104{}Style105{}Style100{}Style101{}Style102{}Style103{}Style94{}Style95{}St" +
                                                        "yle96{}Style97{}Style90{}Style91{}Style92{}Style93{}Style131{}RecordSelector{Ali" +
                                                        "gnImage:Center;}Style98{AlignHorz:Center;}Style99{ForegroundImagePos:LeftOfText;" +
                                                        "AlignHorz:Center;BackColor:WhiteSmoke;}Heading{Wrap:True;BackColor:Control;Borde" +
                                                        "r:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style19{Locked:Fals" +
                                                        "e;AlignHorz:Near;ForeColor:0, 0, 64;BackColor:Honeydew;}Style18{AlignHorz:Near;}" +
                                                        "Style17{}Style14{AlignHorz:Near;}Style15{Locked:False;AlignHorz:Center;ForeColor" +
                                                        ":Black;BackColor:WhiteSmoke;}Style133{}Style132{}Style16{}Style130{}Style10{Alig" +
                                                        "nHorz:Near;}Style11{}Style12{}Style13{}Style126{}Style127{}Style124{}Style120{}S" +
                                                        "tyle121{}Style29{}Style128{AlignHorz:Near;}Style129{AlignHorz:Center;BackColor:I" +
                                                        "vory;}Style28{}Style27{Locked:False;AlignHorz:Far;ForeColor:Black;BackColor:Ivor" +
                                                        "y;}Style26{AlignHorz:Center;}Style125{}Style122{AlignHorz:Near;}Style123{AlignHo" +
                                                        "rz:Near;}Style25{}Style24{}Style23{Locked:False;AlignHorz:Near;ForeColor:0, 0, 6" +
                                                        "4;BackColor:Honeydew;}Style22{AlignHorz:Near;}Style21{}Style20{}OddRow{}Style38{" +
                                                        "AlignHorz:Center;}Style39{Locked:False;AlignHorz:Far;ForeColor:Black;BackColor:I" +
                                                        "vory;}Style36{}FilterBar{BackColor:SeaShell;}Style37{}Style34{AlignHorz:Near;}St" +
                                                        "yle35{Locked:False;AlignHorz:Near;ForeColor:0, 0, 64;BackColor:Ivory;}Style32{}S" +
                                                        "tyle33{}Style49{}Style48{}Style30{AlignHorz:Near;}Style31{AlignHorz:Near;}Normal" +
                                                        "{Font:Verdana, 8.25pt;}Style41{}Style40{}Style43{Locked:False;AlignHorz:Far;Fore" +
                                                        "Color:Black;BackColor:White;}Style42{AlignHorz:Center;}Style45{}Style44{}Style47" +
                                                        "{Locked:False;AlignHorz:Near;ForeColor:0, 0, 64;BackColor:White;}Style46{AlignHo" +
                                                        "rz:Near;}Selected{ForeColor:HighlightText;BackColor:Highlight;}EvenRow{BackColor" +
                                                        ":235, 235, 255;}Style9{}Style8{}Style5{}Style4{}Style7{}Style6{}Style58{}Style59" +
                                                        "{}Style3{}Style2{}Style50{AlignHorz:Center;}Style51{AlignHorz:Far;BackColor:Whit" +
                                                        "eSmoke;}Style52{}Style53{}Style54{AlignHorz:Near;}Style55{AlignHorz:Near;}Style5" +
                                                        "6{}Style57{}Caption{AlignHorz:Center;}Style64{}Style112{}Style69{}Style68{}Group" +
                                                        "{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style1{}Style63" +
                                                        "{AlignHorz:Near;BackColor:White;}Style62{AlignHorz:Near;}Style61{}Style60{}Style" +
                                                        "67{AlignHorz:Near;ForeColor:0, 0, 64;BackColor:WhiteSmoke;}Style66{AlignHorz:Nea" +
                                                        "r;}Style65{}Style115{}Style114{}Style111{AlignHorz:Center;AlignVert:Center;BackC" +
                                                        "olor:White;}Style110{AlignHorz:Center;}Style113{}</Data></Styles><Splits><C1.Win" +
                                                        ".C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"Fals" +
                                                        "e\" Name=\"\" AllowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" C" +
                                                        "olumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterB" +
                                                        "ar=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth" +
                                                        "=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"St" +
                                                        "yle2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle pa" +
                                                        "rent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><" +
                                                        "FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12" +
                                                        "\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"High" +
                                                        "lightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowSt" +
                                                        "yle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" m" +
                                                        "e=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Norm" +
                                                        "al\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
                                                        "e=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"Style3\"" +
                                                        " me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style33\" /><GroupHeaderStyle pa" +
                                                        "rent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><C" +
                                                        "olumnDivider>Maroon,Single</ColumnDivider><Width>75</Width><Height>15</Height><D" +
                                                        "CIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
                                                        "e=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\"" +
                                                        " me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle pa" +
                                                        "rent=\"Style1\" me=\"Style71\" /><GroupFooterStyle parent=\"Style1\" me=\"Style70\" /><V" +
                                                        "isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>55</Wi" +
                                                        "dth><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx" +
                                                        ">1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
                                                        "tyle18\" /><Style parent=\"Style1\" me=\"Style19\" /><FooterStyle parent=\"Style3\" me=" +
                                                        "\"Style20\" /><EditorStyle parent=\"Style5\" me=\"Style21\" /><GroupHeaderStyle parent" +
                                                        "=\"Style1\" me=\"Style73\" /><GroupFooterStyle parent=\"Style1\" me=\"Style72\" /><Visib" +
                                                        "le>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>85</Width" +
                                                        "><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>2<" +
                                                        "/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
                                                        "e22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"St" +
                                                        "yle24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"S" +
                                                        "tyle1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>" +
                                                        "True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>55</Width><He" +
                                                        "ight>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>3</DCI" +
                                                        "dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style26\"" +
                                                        " /><Style parent=\"Style1\" me=\"Style27\" /><FooterStyle parent=\"Style3\" me=\"Style2" +
                                                        "8\" /><EditorStyle parent=\"Style5\" me=\"Style29\" /><GroupHeaderStyle parent=\"Style" +
                                                        "1\" me=\"Style77\" /><GroupFooterStyle parent=\"Style1\" me=\"Style76\" /><Visible>True" +
                                                        "</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>65</Width><Heigh" +
                                                        "t>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>4</DCIdx>" +
                                                        "</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" />" +
                                                        "<Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" " +
                                                        "/><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" " +
                                                        "me=\"Style79\" /><GroupFooterStyle parent=\"Style1\" me=\"Style78\" /><Visible>True</V" +
                                                        "isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>85</Width><Height>1" +
                                                        "5</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>5</DCIdx></C" +
                                                        "1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style128\" /><S" +
                                                        "tyle parent=\"Style1\" me=\"Style129\" /><FooterStyle parent=\"Style3\" me=\"Style130\" " +
                                                        "/><EditorStyle parent=\"Style5\" me=\"Style131\" /><GroupHeaderStyle parent=\"Style1\"" +
                                                        " me=\"Style133\" /><GroupFooterStyle parent=\"Style1\" me=\"Style132\" /><Visible>True" +
                                                        "</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>50</Width><Heigh" +
                                                        "t>18</Height><AllowFocus>False</AllowFocus><DCIdx>6</DCIdx></C1DisplayColumn><C1" +
                                                        "DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1" +
                                                        "\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><EditorStyle parent" +
                                                        "=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style83\" /><Group" +
                                                        "FooterStyle parent=\"Style1\" me=\"Style82\" /><Visible>True</Visible><ColumnDivider" +
                                                        ">DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><AllowFocus>" +
                                                        "False</AllowFocus><Locked>True</Locked><DCIdx>7</DCIdx></C1DisplayColumn><C1Disp" +
                                                        "layColumn><HeadingStyle parent=\"Style2\" me=\"Style98\" /><Style parent=\"Style1\" me" +
                                                        "=\"Style99\" /><FooterStyle parent=\"Style3\" me=\"Style100\" /><EditorStyle parent=\"S" +
                                                        "tyle5\" me=\"Style101\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style103\" /><GroupF" +
                                                        "ooterStyle parent=\"Style1\" me=\"Style102\" /><Visible>True</Visible><ColumnDivider" +
                                                        ">DarkGray,Single</ColumnDivider><Width>65</Width><Height>15</Height><AllowFocus>" +
                                                        "False</AllowFocus><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
                                                        "le parent=\"Style2\" me=\"Style42\" /><Style parent=\"Style1\" me=\"Style43\" /><FooterS" +
                                                        "tyle parent=\"Style3\" me=\"Style44\" /><EditorStyle parent=\"Style5\" me=\"Style45\" />" +
                                                        "<GroupHeaderStyle parent=\"Style1\" me=\"Style85\" /><GroupFooterStyle parent=\"Style" +
                                                        "1\" me=\"Style84\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Column" +
                                                        "Divider><Width>75</Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C" +
                                                        "1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style62\" /><Style parent=\"Style" +
                                                        "1\" me=\"Style63\" /><FooterStyle parent=\"Style3\" me=\"Style64\" /><EditorStyle paren" +
                                                        "t=\"Style5\" me=\"Style65\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style97\" /><Grou" +
                                                        "pFooterStyle parent=\"Style1\" me=\"Style96\" /><Visible>True</Visible><ColumnDivide" +
                                                        "r>Gainsboro,Single</ColumnDivider><Height>15</Height><DCIdx>10</DCIdx></C1Displa" +
                                                        "yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style par" +
                                                        "ent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorS" +
                                                        "tyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style8" +
                                                        "7\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><Co" +
                                                        "lumnDivider>Gainsboro,Single</ColumnDivider><Width>150</Width><Height>15</Height" +
                                                        "><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
                                                        "2\" me=\"Style116\" /><Style parent=\"Style1\" me=\"Style117\" /><FooterStyle parent=\"S" +
                                                        "tyle3\" me=\"Style118\" /><EditorStyle parent=\"Style5\" me=\"Style119\" /><GroupHeader" +
                                                        "Style parent=\"Style1\" me=\"Style121\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
                                                        "e120\" /><Visible>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Wid" +
                                                        "th>40</Width><Height>15</Height><HeaderDivider>False</HeaderDivider><DCIdx>12</D" +
                                                        "CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style1" +
                                                        "10\" /><Style parent=\"Style1\" me=\"Style111\" /><FooterStyle parent=\"Style3\" me=\"St" +
                                                        "yle112\" /><EditorStyle parent=\"Style5\" me=\"Style113\" /><GroupHeaderStyle parent=" +
                                                        "\"Style1\" me=\"Style115\" /><GroupFooterStyle parent=\"Style1\" me=\"Style114\" /><Visi" +
                                                        "ble>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>25</Width" +
                                                        "><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
                                                        "Style parent=\"Style2\" me=\"Style50\" /><Style parent=\"Style1\" me=\"Style51\" /><Foot" +
                                                        "erStyle parent=\"Style3\" me=\"Style52\" /><EditorStyle parent=\"Style5\" me=\"Style53\"" +
                                                        " /><GroupHeaderStyle parent=\"Style1\" me=\"Style89\" /><GroupFooterStyle parent=\"St" +
                                                        "yle1\" me=\"Style88\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Col" +
                                                        "umnDivider><Width>65</Width><Height>18</Height><AllowFocus>False</AllowFocus><DC" +
                                                        "Idx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
                                                        "e=\"Style66\" /><Style parent=\"Style1\" me=\"Style67\" /><FooterStyle parent=\"Style3\"" +
                                                        " me=\"Style68\" /><EditorStyle parent=\"Style5\" me=\"Style69\" /><GroupHeaderStyle pa" +
                                                        "rent=\"Style1\" me=\"Style91\" /><GroupFooterStyle parent=\"Style1\" me=\"Style90\" /><V" +
                                                        "isible>True</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Width>85</Widt" +
                                                        "h><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>1" +
                                                        "5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
                                                        "yle54\" /><Style parent=\"Style1\" me=\"Style55\" /><FooterStyle parent=\"Style3\" me=\"" +
                                                        "Style56\" /><EditorStyle parent=\"Style5\" me=\"Style57\" /><GroupHeaderStyle parent=" +
                                                        "\"Style1\" me=\"Style59\" /><GroupFooterStyle parent=\"Style1\" me=\"Style58\" /><Column" +
                                                        "Divider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>16</DCIdx></C1D" +
                                                        "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style122\" /><Sty" +
                                                        "le parent=\"Style1\" me=\"Style123\" /><FooterStyle parent=\"Style3\" me=\"Style124\" />" +
                                                        "<EditorStyle parent=\"Style5\" me=\"Style125\" /><GroupHeaderStyle parent=\"Style1\" m" +
                                                        "e=\"Style127\" /><GroupFooterStyle parent=\"Style1\" me=\"Style126\" /><ColumnDivider>" +
                                                        "DarkGray,Single</ColumnDivider><Height>18</Height><DCIdx>17</DCIdx></C1DisplayCo" +
                                                        "lumn></internalCols><ClientRect>0, 0, 1210, 230</ClientRect><BorderSide>0</Borde" +
                                                        "rSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=" +
                                                        "\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Foo" +
                                                        "ter\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inacti" +
                                                        "ve\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" " +
                                                        "/><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\"" +
                                                        " /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelect" +
                                                        "or\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\"" +
                                                        " /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Mod" +
                                                        "ified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1210," +
                                                        " 230</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style104\" /><PrintPageFoote" +
                                                        "rStyle parent=\"\" me=\"Style105\" /></Blob>";


    }
}
