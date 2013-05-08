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
  public class InventoryDataMaskForm : System.Windows.Forms.Form
  {
    private MainForm mainForm;
    private DataSet dataSet;

    private System.Windows.Forms.Label DeskLabel;
    private System.Windows.Forms.TextBox Desk;
    
    private System.Windows.Forms.Label HeaderFlagLabel;
    private System.Windows.Forms.TextBox HeaderFlag;
    
    private System.Windows.Forms.Label DataFlagLabel;
    private System.Windows.Forms.TextBox DataFlag;
    
    private System.Windows.Forms.Label TrailerFlagLabel;
    private System.Windows.Forms.TextBox TrailerFlag;
    
    private System.Windows.Forms.Label DelimiterLabel;
    private System.Windows.Forms.TextBox Delimiter;
    
    private C1.Win.C1Input.C1Label PositionLabel;
    private C1.Win.C1Input.C1Label LengthLabel;
    private C1.Win.C1Input.C1Label OrdinalLabel;
    private C1.Win.C1Input.C1Label LocaleLabel;
    
    private C1.Win.C1Input.C1Label recordLengthLabel;
    private C1.Win.C1Input.C1NumericEdit RecordLength;
    
    private C1.Win.C1Input.C1Label RecordCountLabel;
    private C1.Win.C1Input.C1NumericEdit RecordCountLength;
    private C1.Win.C1Input.C1NumericEdit RecordCountPosition;
    private C1.Win.C1Input.C1NumericEdit RecordCountOrdinal;
    
    private C1.Win.C1Input.C1Label AccountLabel;
    private C1.Win.C1Input.C1NumericEdit AccountLength;
    private C1.Win.C1Input.C1NumericEdit AccountPosition;
    private C1.Win.C1Input.C1NumericEdit AccountOrdinal;
    private C1.Win.C1Input.C1NumericEdit AccountLocale;
    
    private C1.Win.C1Input.C1Label SecIdLabel;
    private C1.Win.C1Input.C1NumericEdit SecIdPosition;
    private C1.Win.C1Input.C1NumericEdit SecIdLength;
    private C1.Win.C1Input.C1NumericEdit SecIdOrdinal;
    
    private C1.Win.C1Input.C1Label QuantityLabel;
    private C1.Win.C1Input.C1NumericEdit QuantityPosition;
    private C1.Win.C1Input.C1NumericEdit QuantityLength;
    private C1.Win.C1Input.C1NumericEdit QuantityOrdinal;
    
    private C1.Win.C1Input.C1Label BizDateDDLabel;
    
    private C1.Win.C1Input.C1Label BizDateMMLabel;
    private C1.Win.C1Input.C1NumericEdit BizDateMM;
    
    private C1.Win.C1Input.C1Label BizDateYYLabel;
    private C1.Win.C1Input.C1NumericEdit BizDateYY;
    
    private System.Windows.Forms.Button SaveChangesButton;
    private C1.Win.C1Input.C1NumericEdit BizDateDD;
    private System.Windows.Forms.Button CancelButtoN;
    private System.Windows.Forms.Label LastUpdateLabel;
    
    private System.ComponentModel.Container components = null;
    
    public InventoryDataMaskForm(MainForm mainForm, string desk)
    {
      InitializeComponent();
      
      this.mainForm = mainForm;

      Desk.Text = desk;

      ToolTip toolTip = new ToolTip();

      toolTip.AutoPopDelay = 15000;
      toolTip.InitialDelay = 1000;
      toolTip.ReshowDelay  = 500;

      toolTip.ShowAlways = true;
      
      toolTip.SetToolTip(this.RecordLength,
        "Enter the length of each data record if rows are not one record per line, otherwise enter -1.");

      toolTip.SetToolTip(this.Delimiter,
        "Enter zero [0] if fields are of fixed location and length, otherwise enter the field delimiter.");
      toolTip.SetToolTip(this.HeaderFlag,
        "Enter an equal sign [=] if there is no header record indicator, otherwise enter the Header character.");
      toolTip.SetToolTip(this.DataFlag,
        "Enter an equal sign [=] if there is no data record indicator, otherwise enter the Data character.");
      toolTip.SetToolTip(this.TrailerFlag,
        "Enter an equal sign [=] if there is no trailer indicator, otherwise enter the Trailer character.");

      toolTip.SetToolTip(this.AccountPosition,
        "Enter the starting position of the Account value (first column is 0).");
      toolTip.SetToolTip(this.AccountLength,
        "Enter the length of the Account value.");
      toolTip.SetToolTip(this.AccountOrdinal,
        "Enter the ordinal position of the Account value (or -1 if not used).");
      toolTip.SetToolTip(this.AccountLocale,
        "Enter zero [0] if Account value is in the Header, or one [1] if Account value is in the Data (or -1 if not used).");

      toolTip.SetToolTip(this.SecIdPosition,
        "Enter the starting position of the Security ID value (first column is 0).");
      toolTip.SetToolTip(this.SecIdLength,
        "Enter the length of the Security ID value.");
      toolTip.SetToolTip(this.SecIdOrdinal,
        "Enter the ordinal position of the Security ID value (or -1 if not used).");

      toolTip.SetToolTip(this.QuantityPosition,
        "Enter the starting position of the Quantity value (first column is 0).");
      toolTip.SetToolTip(this.QuantityLength,
        "Enter the length of the Quantity value.");
      toolTip.SetToolTip(this.QuantityOrdinal,
        "Enter the ordinal position of the Quantity value (or -1 if not used).");

      toolTip.SetToolTip(this.RecordCountPosition,
        "Enter the starting position of the Record Count value (first column is 0).");
      toolTip.SetToolTip(this.RecordCountLength,
        "Enter the length of the Record Count value.");
      toolTip.SetToolTip(this.RecordCountOrdinal,
        "Enter the ordinal position of the Record Count value (or -1 if not used).");

      toolTip.SetToolTip(this.BizDateDD,
        "Enter the starting position of the Day component of the date value (first column is 0).");
      toolTip.SetToolTip(this.BizDateMM,
        "Enter the starting position of the Month component of the date value (first column is 0).");
      toolTip.SetToolTip(this.BizDateYY,
        "Enter the starting position of the two-character Year component of the date value (first column is 0).");
    }

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
      this.TrailerFlag = new System.Windows.Forms.TextBox();
      this.TrailerFlagLabel = new System.Windows.Forms.Label();
      this.DataFlag = new System.Windows.Forms.TextBox();
      this.DataFlagLabel = new System.Windows.Forms.Label();
      this.HeaderFlag = new System.Windows.Forms.TextBox();
      this.HeaderFlagLabel = new System.Windows.Forms.Label();
      this.SaveChangesButton = new System.Windows.Forms.Button();
      this.CancelButtoN = new System.Windows.Forms.Button();
      this.RecordCountLength = new C1.Win.C1Input.C1NumericEdit();
      this.RecordCountLabel = new C1.Win.C1Input.C1Label();
      this.RecordCountPosition = new C1.Win.C1Input.C1NumericEdit();
      this.QuantityLength = new C1.Win.C1Input.C1NumericEdit();
      this.QuantityLabel = new C1.Win.C1Input.C1Label();
      this.QuantityPosition = new C1.Win.C1Input.C1NumericEdit();
      this.AccountLength = new C1.Win.C1Input.C1NumericEdit();
      this.AccountLabel = new C1.Win.C1Input.C1Label();
      this.AccountPosition = new C1.Win.C1Input.C1NumericEdit();
      this.LengthLabel = new C1.Win.C1Input.C1Label();
      this.PositionLabel = new C1.Win.C1Input.C1Label();
      this.SecIdLength = new C1.Win.C1Input.C1NumericEdit();
      this.SecIdLabel = new C1.Win.C1Input.C1Label();
      this.SecIdPosition = new C1.Win.C1Input.C1NumericEdit();
      this.RecordCountOrdinal = new C1.Win.C1Input.C1NumericEdit();
      this.QuantityOrdinal = new C1.Win.C1Input.C1NumericEdit();
      this.AccountOrdinal = new C1.Win.C1Input.C1NumericEdit();
      this.OrdinalLabel = new C1.Win.C1Input.C1Label();
      this.SecIdOrdinal = new C1.Win.C1Input.C1NumericEdit();
      this.BizDateYYLabel = new C1.Win.C1Input.C1Label();
      this.BizDateYY = new C1.Win.C1Input.C1NumericEdit();
      this.BizDateMMLabel = new C1.Win.C1Input.C1Label();
      this.BizDateMM = new C1.Win.C1Input.C1NumericEdit();
      this.BizDateDDLabel = new C1.Win.C1Input.C1Label();
      this.BizDateDD = new C1.Win.C1Input.C1NumericEdit();
      this.AccountLocale = new C1.Win.C1Input.C1NumericEdit();
      this.LocaleLabel = new C1.Win.C1Input.C1Label();
      this.Delimiter = new System.Windows.Forms.TextBox();
      this.DelimiterLabel = new System.Windows.Forms.Label();
      this.Desk = new System.Windows.Forms.TextBox();
      this.DeskLabel = new System.Windows.Forms.Label();
      this.recordLengthLabel = new C1.Win.C1Input.C1Label();
      this.RecordLength = new C1.Win.C1Input.C1NumericEdit();
      this.LastUpdateLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountLength)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountPosition)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityLength)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityPosition)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountLength)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountPosition)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.LengthLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PositionLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdLength)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdPosition)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountOrdinal)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityOrdinal)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountOrdinal)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.OrdinalLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdOrdinal)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateYYLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateYY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateMMLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateMM)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateDDLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateDD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountLocale)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.LocaleLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.recordLengthLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordLength)).BeginInit();
      this.SuspendLayout();
      // 
      // TrailerFlag
      // 
      this.TrailerFlag.AutoSize = false;
      this.TrailerFlag.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.TrailerFlag.Location = new System.Drawing.Point(144, 128);
      this.TrailerFlag.MaxLength = 1;
      this.TrailerFlag.Name = "TrailerFlag";
      this.TrailerFlag.Size = new System.Drawing.Size(24, 20);
      this.TrailerFlag.TabIndex = 8;
      this.TrailerFlag.Text = "";
      this.TrailerFlag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.TrailerFlag.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // TrailerFlagLabel
      // 
      this.TrailerFlagLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.TrailerFlagLabel.Location = new System.Drawing.Point(8, 120);
      this.TrailerFlagLabel.Name = "TrailerFlagLabel";
      this.TrailerFlagLabel.Size = new System.Drawing.Size(128, 32);
      this.TrailerFlagLabel.TabIndex = 0;
      this.TrailerFlagLabel.Text = "Trailer Flag:";
      this.TrailerFlagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // DataFlag
      // 
      this.DataFlag.AutoSize = false;
      this.DataFlag.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.DataFlag.Location = new System.Drawing.Point(144, 96);
      this.DataFlag.MaxLength = 1;
      this.DataFlag.Name = "DataFlag";
      this.DataFlag.Size = new System.Drawing.Size(24, 20);
      this.DataFlag.TabIndex = 7;
      this.DataFlag.Text = "";
      this.DataFlag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.DataFlag.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // DataFlagLabel
      // 
      this.DataFlagLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.DataFlagLabel.Location = new System.Drawing.Point(8, 88);
      this.DataFlagLabel.Name = "DataFlagLabel";
      this.DataFlagLabel.Size = new System.Drawing.Size(128, 32);
      this.DataFlagLabel.TabIndex = 0;
      this.DataFlagLabel.Text = "Data Flag:";
      this.DataFlagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // HeaderFlag
      // 
      this.HeaderFlag.AutoSize = false;
      this.HeaderFlag.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.HeaderFlag.Location = new System.Drawing.Point(144, 64);
      this.HeaderFlag.MaxLength = 1;
      this.HeaderFlag.Name = "HeaderFlag";
      this.HeaderFlag.Size = new System.Drawing.Size(24, 20);
      this.HeaderFlag.TabIndex = 6;
      this.HeaderFlag.Text = "";
      this.HeaderFlag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.HeaderFlag.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // HeaderFlagLabel
      // 
      this.HeaderFlagLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.HeaderFlagLabel.Location = new System.Drawing.Point(8, 56);
      this.HeaderFlagLabel.Name = "HeaderFlagLabel";
      this.HeaderFlagLabel.Size = new System.Drawing.Size(128, 32);
      this.HeaderFlagLabel.TabIndex = 0;
      this.HeaderFlagLabel.Text = "Header Flag:";
      this.HeaderFlagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // SaveChangesButton
      // 
      this.SaveChangesButton.Enabled = false;
      this.SaveChangesButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.SaveChangesButton.Location = new System.Drawing.Point(64, 480);
      this.SaveChangesButton.Name = "SaveChangesButton";
      this.SaveChangesButton.Size = new System.Drawing.Size(120, 24);
      this.SaveChangesButton.TabIndex = 92;
      this.SaveChangesButton.Text = "Save Changes";
      this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
      // 
      // CancelButtoN
      // 
      this.CancelButtoN.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.CancelButtoN.Location = new System.Drawing.Point(240, 480);
      this.CancelButtoN.Name = "CancelButtoN";
      this.CancelButtoN.Size = new System.Drawing.Size(120, 24);
      this.CancelButtoN.TabIndex = 93;
      this.CancelButtoN.Text = "Cancel";
      this.CancelButtoN.Click += new System.EventHandler(this.CancelButtoN_Click);
      // 
      // RecordCountLength
      // 
      this.RecordCountLength.AutoSize = false;
      this.RecordCountLength.DataType = typeof(short);
      this.RecordCountLength.Location = new System.Drawing.Point(208, 328);
      this.RecordCountLength.MaxLength = 2;
      this.RecordCountLength.Name = "RecordCountLength";
      this.RecordCountLength.Size = new System.Drawing.Size(40, 20);
      this.RecordCountLength.TabIndex = 107;
      this.RecordCountLength.Tag = null;
      this.RecordCountLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.RecordCountLength.Value = ((short)(-1));
      this.RecordCountLength.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.RecordCountLength.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // RecordCountLabel
      // 
      this.RecordCountLabel.Location = new System.Drawing.Point(40, 328);
      this.RecordCountLabel.Name = "RecordCountLabel";
      this.RecordCountLabel.Size = new System.Drawing.Size(96, 16);
      this.RecordCountLabel.TabIndex = 106;
      this.RecordCountLabel.Tag = null;
      this.RecordCountLabel.Text = "Record Count:";
      this.RecordCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.RecordCountLabel.TextDetached = true;
      // 
      // RecordCountPosition
      // 
      this.RecordCountPosition.AutoSize = false;
      this.RecordCountPosition.DataType = typeof(short);
      this.RecordCountPosition.Location = new System.Drawing.Point(144, 328);
      this.RecordCountPosition.MaxLength = 2;
      this.RecordCountPosition.Name = "RecordCountPosition";
      this.RecordCountPosition.Size = new System.Drawing.Size(40, 20);
      this.RecordCountPosition.TabIndex = 105;
      this.RecordCountPosition.Tag = null;
      this.RecordCountPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.RecordCountPosition.Value = ((short)(-1));
      this.RecordCountPosition.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.RecordCountPosition.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // QuantityLength
      // 
      this.QuantityLength.AutoSize = false;
      this.QuantityLength.DataType = typeof(short);
      this.QuantityLength.Location = new System.Drawing.Point(208, 296);
      this.QuantityLength.MaxLength = 2;
      this.QuantityLength.Name = "QuantityLength";
      this.QuantityLength.Size = new System.Drawing.Size(40, 20);
      this.QuantityLength.TabIndex = 104;
      this.QuantityLength.Tag = null;
      this.QuantityLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.QuantityLength.Value = ((short)(-1));
      this.QuantityLength.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.QuantityLength.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // QuantityLabel
      // 
      this.QuantityLabel.Location = new System.Drawing.Point(40, 296);
      this.QuantityLabel.Name = "QuantityLabel";
      this.QuantityLabel.Size = new System.Drawing.Size(96, 16);
      this.QuantityLabel.TabIndex = 103;
      this.QuantityLabel.Tag = null;
      this.QuantityLabel.Text = "Quantity:";
      this.QuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.QuantityLabel.TextDetached = true;
      // 
      // QuantityPosition
      // 
      this.QuantityPosition.AutoSize = false;
      this.QuantityPosition.DataType = typeof(short);
      this.QuantityPosition.Location = new System.Drawing.Point(144, 296);
      this.QuantityPosition.MaxLength = 2;
      this.QuantityPosition.Name = "QuantityPosition";
      this.QuantityPosition.Size = new System.Drawing.Size(40, 20);
      this.QuantityPosition.TabIndex = 102;
      this.QuantityPosition.Tag = null;
      this.QuantityPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.QuantityPosition.Value = ((short)(-1));
      this.QuantityPosition.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.QuantityPosition.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // AccountLength
      // 
      this.AccountLength.AutoSize = false;
      this.AccountLength.DataType = typeof(short);
      this.AccountLength.Location = new System.Drawing.Point(208, 232);
      this.AccountLength.MaxLength = 2;
      this.AccountLength.Name = "AccountLength";
      this.AccountLength.Size = new System.Drawing.Size(40, 20);
      this.AccountLength.TabIndex = 101;
      this.AccountLength.Tag = null;
      this.AccountLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.AccountLength.Value = ((short)(-1));
      this.AccountLength.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.AccountLength.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // AccountLabel
      // 
      this.AccountLabel.Location = new System.Drawing.Point(40, 232);
      this.AccountLabel.Name = "AccountLabel";
      this.AccountLabel.Size = new System.Drawing.Size(96, 16);
      this.AccountLabel.TabIndex = 100;
      this.AccountLabel.Tag = null;
      this.AccountLabel.Text = "Account:";
      this.AccountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.AccountLabel.TextDetached = true;
      // 
      // AccountPosition
      // 
      this.AccountPosition.AutoSize = false;
      this.AccountPosition.DataType = typeof(short);
      this.AccountPosition.Location = new System.Drawing.Point(144, 232);
      this.AccountPosition.MaxLength = 2;
      this.AccountPosition.Name = "AccountPosition";
      this.AccountPosition.Size = new System.Drawing.Size(40, 20);
      this.AccountPosition.TabIndex = 99;
      this.AccountPosition.Tag = null;
      this.AccountPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.AccountPosition.Value = ((short)(-1));
      this.AccountPosition.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.AccountPosition.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // LengthLabel
      // 
      this.LengthLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.LengthLabel.Location = new System.Drawing.Point(200, 176);
      this.LengthLabel.Name = "LengthLabel";
      this.LengthLabel.Size = new System.Drawing.Size(56, 16);
      this.LengthLabel.TabIndex = 98;
      this.LengthLabel.Tag = null;
      this.LengthLabel.Text = "Length";
      this.LengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.LengthLabel.TextDetached = true;
      // 
      // PositionLabel
      // 
      this.PositionLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.PositionLabel.Location = new System.Drawing.Point(144, 176);
      this.PositionLabel.Name = "PositionLabel";
      this.PositionLabel.Size = new System.Drawing.Size(40, 16);
      this.PositionLabel.TabIndex = 97;
      this.PositionLabel.Tag = null;
      this.PositionLabel.Text = "Start";
      this.PositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.PositionLabel.TextDetached = true;
      // 
      // SecIdLength
      // 
      this.SecIdLength.AutoSize = false;
      this.SecIdLength.DataType = typeof(short);
      this.SecIdLength.Location = new System.Drawing.Point(208, 264);
      this.SecIdLength.MaxLength = 2;
      this.SecIdLength.Name = "SecIdLength";
      this.SecIdLength.Size = new System.Drawing.Size(40, 20);
      this.SecIdLength.TabIndex = 96;
      this.SecIdLength.Tag = null;
      this.SecIdLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.SecIdLength.Value = ((short)(-1));
      this.SecIdLength.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.SecIdLength.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // SecIdLabel
      // 
      this.SecIdLabel.Location = new System.Drawing.Point(40, 264);
      this.SecIdLabel.Name = "SecIdLabel";
      this.SecIdLabel.Size = new System.Drawing.Size(96, 16);
      this.SecIdLabel.TabIndex = 95;
      this.SecIdLabel.Tag = null;
      this.SecIdLabel.Text = "Security ID:";
      this.SecIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.SecIdLabel.TextDetached = true;
      // 
      // SecIdPosition
      // 
      this.SecIdPosition.AutoSize = false;
      this.SecIdPosition.DataType = typeof(short);
      this.SecIdPosition.Location = new System.Drawing.Point(144, 264);
      this.SecIdPosition.MaxLength = 2;
      this.SecIdPosition.Name = "SecIdPosition";
      this.SecIdPosition.Size = new System.Drawing.Size(40, 20);
      this.SecIdPosition.TabIndex = 94;
      this.SecIdPosition.Tag = null;
      this.SecIdPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.SecIdPosition.Value = ((short)(-1));
      this.SecIdPosition.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.SecIdPosition.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // RecordCountOrdinal
      // 
      this.RecordCountOrdinal.AutoSize = false;
      this.RecordCountOrdinal.DataType = typeof(short);
      this.RecordCountOrdinal.Location = new System.Drawing.Point(272, 328);
      this.RecordCountOrdinal.MaxLength = 1;
      this.RecordCountOrdinal.Name = "RecordCountOrdinal";
      this.RecordCountOrdinal.Size = new System.Drawing.Size(40, 20);
      this.RecordCountOrdinal.TabIndex = 112;
      this.RecordCountOrdinal.Tag = null;
      this.RecordCountOrdinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.RecordCountOrdinal.Value = ((short)(-1));
      this.RecordCountOrdinal.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.RecordCountOrdinal.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // QuantityOrdinal
      // 
      this.QuantityOrdinal.AutoSize = false;
      this.QuantityOrdinal.DataType = typeof(short);
      this.QuantityOrdinal.Location = new System.Drawing.Point(272, 296);
      this.QuantityOrdinal.MaxLength = 1;
      this.QuantityOrdinal.Name = "QuantityOrdinal";
      this.QuantityOrdinal.Size = new System.Drawing.Size(40, 20);
      this.QuantityOrdinal.TabIndex = 111;
      this.QuantityOrdinal.Tag = null;
      this.QuantityOrdinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.QuantityOrdinal.Value = ((short)(-1));
      this.QuantityOrdinal.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.QuantityOrdinal.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // AccountOrdinal
      // 
      this.AccountOrdinal.AutoSize = false;
      this.AccountOrdinal.DataType = typeof(short);
      this.AccountOrdinal.Location = new System.Drawing.Point(272, 232);
      this.AccountOrdinal.MaxLength = 1;
      this.AccountOrdinal.Name = "AccountOrdinal";
      this.AccountOrdinal.Size = new System.Drawing.Size(40, 20);
      this.AccountOrdinal.TabIndex = 110;
      this.AccountOrdinal.Tag = null;
      this.AccountOrdinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.AccountOrdinal.Value = ((short)(-1));
      this.AccountOrdinal.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.AccountOrdinal.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // OrdinalLabel
      // 
      this.OrdinalLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.OrdinalLabel.Location = new System.Drawing.Point(264, 176);
      this.OrdinalLabel.Name = "OrdinalLabel";
      this.OrdinalLabel.Size = new System.Drawing.Size(56, 16);
      this.OrdinalLabel.TabIndex = 109;
      this.OrdinalLabel.Tag = null;
      this.OrdinalLabel.Text = "Ordinal";
      this.OrdinalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.OrdinalLabel.TextDetached = true;
      // 
      // SecIdOrdinal
      // 
      this.SecIdOrdinal.AutoSize = false;
      this.SecIdOrdinal.DataType = typeof(short);
      this.SecIdOrdinal.Location = new System.Drawing.Point(272, 264);
      this.SecIdOrdinal.MaxLength = 1;
      this.SecIdOrdinal.Name = "SecIdOrdinal";
      this.SecIdOrdinal.Size = new System.Drawing.Size(40, 20);
      this.SecIdOrdinal.TabIndex = 108;
      this.SecIdOrdinal.Tag = null;
      this.SecIdOrdinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.SecIdOrdinal.Value = ((short)(-1));
      this.SecIdOrdinal.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.SecIdOrdinal.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // BizDateYYLabel
      // 
      this.BizDateYYLabel.Location = new System.Drawing.Point(40, 424);
      this.BizDateYYLabel.Name = "BizDateYYLabel";
      this.BizDateYYLabel.Size = new System.Drawing.Size(96, 16);
      this.BizDateYYLabel.TabIndex = 123;
      this.BizDateYYLabel.Tag = null;
      this.BizDateYYLabel.Text = "Date YY:";
      this.BizDateYYLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.BizDateYYLabel.TextDetached = true;
      // 
      // BizDateYY
      // 
      this.BizDateYY.AutoSize = false;
      this.BizDateYY.DataType = typeof(short);
      this.BizDateYY.Location = new System.Drawing.Point(144, 424);
      this.BizDateYY.MaxLength = 2;
      this.BizDateYY.Name = "BizDateYY";
      this.BizDateYY.Size = new System.Drawing.Size(40, 20);
      this.BizDateYY.TabIndex = 122;
      this.BizDateYY.Tag = null;
      this.BizDateYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.BizDateYY.Value = ((short)(-1));
      this.BizDateYY.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.BizDateYY.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // BizDateMMLabel
      // 
      this.BizDateMMLabel.Location = new System.Drawing.Point(40, 392);
      this.BizDateMMLabel.Name = "BizDateMMLabel";
      this.BizDateMMLabel.Size = new System.Drawing.Size(96, 16);
      this.BizDateMMLabel.TabIndex = 121;
      this.BizDateMMLabel.Tag = null;
      this.BizDateMMLabel.Text = "Date MM:";
      this.BizDateMMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.BizDateMMLabel.TextDetached = true;
      // 
      // BizDateMM
      // 
      this.BizDateMM.AutoSize = false;
      this.BizDateMM.DataType = typeof(short);
      this.BizDateMM.Location = new System.Drawing.Point(144, 392);
      this.BizDateMM.MaxLength = 2;
      this.BizDateMM.Name = "BizDateMM";
      this.BizDateMM.Size = new System.Drawing.Size(40, 20);
      this.BizDateMM.TabIndex = 120;
      this.BizDateMM.Tag = null;
      this.BizDateMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.BizDateMM.Value = ((short)(-1));
      this.BizDateMM.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.BizDateMM.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // BizDateDDLabel
      // 
      this.BizDateDDLabel.Location = new System.Drawing.Point(40, 360);
      this.BizDateDDLabel.Name = "BizDateDDLabel";
      this.BizDateDDLabel.Size = new System.Drawing.Size(96, 16);
      this.BizDateDDLabel.TabIndex = 119;
      this.BizDateDDLabel.Tag = null;
      this.BizDateDDLabel.Text = "Date DD:";
      this.BizDateDDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.BizDateDDLabel.TextDetached = true;
      // 
      // BizDateDD
      // 
      this.BizDateDD.AutoSize = false;
      this.BizDateDD.DataType = typeof(short);
      this.BizDateDD.Location = new System.Drawing.Point(144, 360);
      this.BizDateDD.MaxLength = 2;
      this.BizDateDD.Name = "BizDateDD";
      this.BizDateDD.Size = new System.Drawing.Size(40, 20);
      this.BizDateDD.TabIndex = 118;
      this.BizDateDD.Tag = null;
      this.BizDateDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.BizDateDD.Value = ((short)(-1));
      this.BizDateDD.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.BizDateDD.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // AccountLocale
      // 
      this.AccountLocale.AutoSize = false;
      this.AccountLocale.DataType = typeof(short);
      this.AccountLocale.Location = new System.Drawing.Point(336, 232);
      this.AccountLocale.MaxLength = 1;
      this.AccountLocale.Name = "AccountLocale";
      this.AccountLocale.Size = new System.Drawing.Size(40, 20);
      this.AccountLocale.TabIndex = 124;
      this.AccountLocale.Tag = null;
      this.AccountLocale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.AccountLocale.Value = ((short)(-1));
      this.AccountLocale.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.AccountLocale.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // LocaleLabel
      // 
      this.LocaleLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.LocaleLabel.Location = new System.Drawing.Point(328, 176);
      this.LocaleLabel.Name = "LocaleLabel";
      this.LocaleLabel.Size = new System.Drawing.Size(56, 16);
      this.LocaleLabel.TabIndex = 125;
      this.LocaleLabel.Tag = null;
      this.LocaleLabel.Text = "Locale";
      this.LocaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.LocaleLabel.TextDetached = true;
      // 
      // Delimiter
      // 
      this.Delimiter.AutoSize = false;
      this.Delimiter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.Delimiter.Location = new System.Drawing.Point(336, 128);
      this.Delimiter.MaxLength = 1;
      this.Delimiter.Name = "Delimiter";
      this.Delimiter.Size = new System.Drawing.Size(24, 20);
      this.Delimiter.TabIndex = 127;
      this.Delimiter.Text = "0";
      this.Delimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.Delimiter.TextChanged += new System.EventHandler(this.Delimiter_TextChanged);
      // 
      // DelimiterLabel
      // 
      this.DelimiterLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.DelimiterLabel.Location = new System.Drawing.Point(200, 120);
      this.DelimiterLabel.Name = "DelimiterLabel";
      this.DelimiterLabel.Size = new System.Drawing.Size(128, 32);
      this.DelimiterLabel.TabIndex = 126;
      this.DelimiterLabel.Text = "Field Delimiter:";
      this.DelimiterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // Desk
      // 
      this.Desk.AutoSize = false;
      this.Desk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.Desk.Location = new System.Drawing.Point(144, 16);
      this.Desk.MaxLength = 12;
      this.Desk.Name = "Desk";
      this.Desk.ReadOnly = true;
      this.Desk.Size = new System.Drawing.Size(120, 20);
      this.Desk.TabIndex = 129;
      this.Desk.Text = "";
      this.Desk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // DeskLabel
      // 
      this.DeskLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.DeskLabel.Location = new System.Drawing.Point(8, 8);
      this.DeskLabel.Name = "DeskLabel";
      this.DeskLabel.Size = new System.Drawing.Size(128, 32);
      this.DeskLabel.TabIndex = 128;
      this.DeskLabel.Text = "Desk:";
      this.DeskLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // recordLengthLabel
      // 
      this.recordLengthLabel.Location = new System.Drawing.Point(40, 200);
      this.recordLengthLabel.Name = "recordLengthLabel";
      this.recordLengthLabel.Size = new System.Drawing.Size(96, 16);
      this.recordLengthLabel.TabIndex = 131;
      this.recordLengthLabel.Tag = null;
      this.recordLengthLabel.Text = "Record Length:";
      this.recordLengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.recordLengthLabel.TextDetached = true;
      // 
      // RecordLength
      // 
      this.RecordLength.AutoSize = false;
      this.RecordLength.DataType = typeof(short);
      this.RecordLength.Location = new System.Drawing.Point(208, 200);
      this.RecordLength.MaxLength = 3;
      this.RecordLength.Name = "RecordLength";
      this.RecordLength.Size = new System.Drawing.Size(40, 20);
      this.RecordLength.TabIndex = 130;
      this.RecordLength.Tag = null;
      this.RecordLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.RecordLength.Value = ((short)(-1));
      this.RecordLength.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
      this.RecordLength.TextChanged += new System.EventHandler(this.AnyControl_ValueChanged);
      // 
      // LastUpdateLabel
      // 
      this.LastUpdateLabel.Location = new System.Drawing.Point(8, 528);
      this.LastUpdateLabel.Name = "LastUpdateLabel";
      this.LastUpdateLabel.Size = new System.Drawing.Size(408, 24);
      this.LastUpdateLabel.TabIndex = 132;
      this.LastUpdateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // InventoryDataMaskForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(418, 567);
      this.Controls.Add(this.LastUpdateLabel);
      this.Controls.Add(this.recordLengthLabel);
      this.Controls.Add(this.RecordLength);
      this.Controls.Add(this.Desk);
      this.Controls.Add(this.Delimiter);
      this.Controls.Add(this.HeaderFlag);
      this.Controls.Add(this.DataFlag);
      this.Controls.Add(this.TrailerFlag);
      this.Controls.Add(this.DeskLabel);
      this.Controls.Add(this.DelimiterLabel);
      this.Controls.Add(this.LocaleLabel);
      this.Controls.Add(this.AccountLocale);
      this.Controls.Add(this.BizDateYYLabel);
      this.Controls.Add(this.BizDateYY);
      this.Controls.Add(this.BizDateMMLabel);
      this.Controls.Add(this.BizDateMM);
      this.Controls.Add(this.BizDateDDLabel);
      this.Controls.Add(this.BizDateDD);
      this.Controls.Add(this.RecordCountOrdinal);
      this.Controls.Add(this.QuantityOrdinal);
      this.Controls.Add(this.AccountOrdinal);
      this.Controls.Add(this.OrdinalLabel);
      this.Controls.Add(this.SecIdOrdinal);
      this.Controls.Add(this.RecordCountLength);
      this.Controls.Add(this.RecordCountLabel);
      this.Controls.Add(this.RecordCountPosition);
      this.Controls.Add(this.QuantityLength);
      this.Controls.Add(this.QuantityLabel);
      this.Controls.Add(this.QuantityPosition);
      this.Controls.Add(this.AccountLength);
      this.Controls.Add(this.AccountLabel);
      this.Controls.Add(this.AccountPosition);
      this.Controls.Add(this.LengthLabel);
      this.Controls.Add(this.PositionLabel);
      this.Controls.Add(this.SecIdLength);
      this.Controls.Add(this.SecIdLabel);
      this.Controls.Add(this.SecIdPosition);
      this.Controls.Add(this.CancelButtoN);
      this.Controls.Add(this.SaveChangesButton);
      this.Controls.Add(this.HeaderFlagLabel);
      this.Controls.Add(this.DataFlagLabel);
      this.Controls.Add(this.TrailerFlagLabel);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InventoryDataMaskForm";
      this.Text = "Inventory  - Data Mask";
      this.Load += new System.EventHandler(this.InventoryDataMaskForm_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.InventoryDataMaskForm_Paint);
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountLength)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountPosition)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityLength)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityPosition)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountLength)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountPosition)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.LengthLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PositionLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdLength)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdPosition)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordCountOrdinal)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.QuantityOrdinal)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountOrdinal)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.OrdinalLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecIdOrdinal)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateYYLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateYY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateMMLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateMM)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateDDLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BizDateDD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AccountLocale)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.LocaleLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.recordLengthLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecordLength)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion
    
    private void DataLoad()
    {
      try
      {      
        dataSet = mainForm.ServiceAgent.InventoryDataMaskGet(Desk.Text);
            
        if (dataSet.Tables["InventoryDataMasks"].Rows.Count.Equals(1))
        {        
          Delimiter.Text            = dataSet.Tables["InventoryDataMasks"].Rows[0]["Delimiter"].ToString();
          TrailerFlag.Text          = dataSet.Tables["InventoryDataMasks"].Rows[0]["TrailerFlag"].ToString();
          DataFlag.Text             = dataSet.Tables["InventoryDataMasks"].Rows[0]["DataFlag"].ToString();
          HeaderFlag.Text           = dataSet.Tables["InventoryDataMasks"].Rows[0]["HeaderFlag"].ToString();
          AccountPosition.Value     = dataSet.Tables["InventoryDataMasks"].Rows[0]["AccountPosition"];
          AccountLength.Value       = dataSet.Tables["InventoryDataMasks"].Rows[0]["AccountLength"];
          AccountOrdinal.Value      = dataSet.Tables["InventoryDataMasks"].Rows[0]["AccountOrdinal"];
          AccountLocale.Value       = dataSet.Tables["InventoryDataMasks"].Rows[0]["AccountLocale"];
          RecordCountOrdinal.Value  = dataSet.Tables["InventoryDataMasks"].Rows[0]["RecordCountOrdinal"];
          SecIdOrdinal.Value        = dataSet.Tables["InventoryDataMasks"].Rows[0]["SecIdOrdinal"];
          QuantityOrdinal.Value     = dataSet.Tables["InventoryDataMasks"].Rows[0]["QuantityOrdinal"];
          SecIdLength.Value         = dataSet.Tables["InventoryDataMasks"].Rows[0]["SecIdLength"];
          SecIdPosition.Value       = dataSet.Tables["InventoryDataMasks"].Rows[0]["SecIdPosition"];
          QuantityLength.Value      = dataSet.Tables["InventoryDataMasks"].Rows[0]["QuantityLength"];
          QuantityPosition.Value    = dataSet.Tables["InventoryDataMasks"].Rows[0]["QuantityPosition"];
          RecordCountLength.Value   = dataSet.Tables["InventoryDataMasks"].Rows[0]["RecordCountLength"];
          RecordCountPosition.Value = dataSet.Tables["InventoryDataMasks"].Rows[0]["RecordCountPosition"];
          BizDateDD.Value           = dataSet.Tables["InventoryDataMasks"].Rows[0]["BizDateDD"];
          BizDateMM.Value           = dataSet.Tables["InventoryDataMasks"].Rows[0]["BizDateMM"];
          BizDateYY.Value           = dataSet.Tables["InventoryDataMasks"].Rows[0]["BizDateYY"];

          LastUpdateLabel.Text = "Last Update: By " + dataSet.Tables["InventoryDataMasks"].Rows[0]["ActUserShortName"] + 
            " at " + Tools.FormatDate(dataSet.Tables["InventoryDataMasks"].Rows[0]["ActTime"].ToString(), Standard.DateTimeFileFormat);
        }
        else
        {
          mainForm.Alert("An inventory data mask does not exsist for Desk " + Desk.Text + ".", PilotState.RunFault);        
        }
      }
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);        
        Log.Write(ee.Message + " [InventoryDataMaskForm.InventoryDataMaskForm_Load]", 1);
      }

      SaveChangesButton.Enabled = false;
    }

    private void DataSave()
    {
      try
      {
        mainForm.ServiceAgent.InventoryDataMaskSet(Desk.Text, 
          (short)RecordLength.Value,  
          HeaderFlag.Text[0],  
          DataFlag.Text[0], 
          TrailerFlag.Text[0], 
          (short)AccountLocale.Value,
          Delimiter.Text[0],  
          (short)AccountOrdinal.Value,
          (short)SecIdOrdinal.Value,  
          (short)QuantityOrdinal.Value,  
          (short)RecordCountOrdinal.Value,  
          (short)AccountPosition.Value,   
          (short)AccountLength.Value,   
          (short)BizDateDD.Value,  
          (short)BizDateMM.Value, 
          (short)BizDateYY.Value,  
          (short)SecIdPosition.Value,  
          (short)SecIdLength.Value,
          (short)QuantityPosition.Value,   
          (short)QuantityLength.Value, 
          (short)RecordCountPosition.Value, 
          (short)RecordCountLength.Value,
          mainForm.UserId);
      }
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);
        Log.Write(ee.Message + " [InventoryDataMaskForm.SaveChangesButton_Click]", 1);
      }
    }
    
    private void InventoryDataMaskForm_Load(object sender,  System.EventArgs e)
    {
      DataLoad();
    }

    private void SaveChangesButton_Click(object sender,  System.EventArgs e)
    {
      DataSave();
      DataLoad();
    }
    
    private void CancelButtoN_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

    private void Delimiter_TextChanged(object sender, System.EventArgs e)
    {
      AccountOrdinal.Enabled = !Delimiter.Text.Equals("0");
      SecIdOrdinal.Enabled = !Delimiter.Text.Equals("0");  
      QuantityOrdinal.Enabled = !Delimiter.Text.Equals("0");  
      RecordCountOrdinal.Enabled = !Delimiter.Text.Equals("0"); 
 
      AccountPosition.Enabled = Delimiter.Text.Equals("0"); 
      AccountLength.Enabled = Delimiter.Text.Equals("0");   
      SecIdPosition.Enabled = Delimiter.Text.Equals("0"); 
      SecIdLength.Enabled = Delimiter.Text.Equals("0");
      QuantityPosition.Enabled = Delimiter.Text.Equals("0");   
      QuantityLength.Enabled = Delimiter.Text.Equals("0"); 
      RecordCountPosition.Enabled = Delimiter.Text.Equals("0");
      RecordCountLength.Enabled = Delimiter.Text.Equals("0");

      if (Delimiter.Text.Equals("0"))
      {
        AccountOrdinal.Value = -1;
        SecIdOrdinal.Value = -1;  
        QuantityOrdinal.Value = -1;  
        RecordCountOrdinal.Value = -1; 
      }
      else
      {
        AccountPosition.Value = -1; 
        AccountLength.Value = -1;   
        SecIdPosition.Value = -1; 
        SecIdLength.Value = -1;
        QuantityPosition.Value = -1;   
        QuantityLength.Value = -1; 
        RecordCountPosition.Value = -1;
        RecordCountLength.Value = -1;
      }

      AnyControl_ValueChanged(sender, e);
    }

    private void AnyControl_ValueChanged(object sender, System.EventArgs e)
    {
      SaveChangesButton.Enabled = true;
    }

    private void InventoryDataMaskForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      Pen pen = new Pen(Color.DimGray, 1.8F);
      
      float x1 = 20.0F;
      float x2 = this.Width - 30.0F;
      
      e.Graphics.DrawLine(pen, x1, 50.0F, x2, 50.0F);
      e.Graphics.DrawLine(pen, x1, 162.0F, x2, 162.0F);
      e.Graphics.DrawLine(pen, x1, 462.0F, x2, 462.0F);
      e.Graphics.DrawLine(pen, x1, 520.0F, x2, 520.0F);

      e.Graphics.Dispose();
    }
  }
}
