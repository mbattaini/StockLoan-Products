namespace StockLoan.Inventory
{
    partial class FormEmailClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelTo = new System.Windows.Forms.Label();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.textBoxCc = new System.Windows.Forms.TextBox();
            this.labelCc = new System.Windows.Forms.Label();
            this.textBoxBcc = new System.Windows.Forms.TextBox();
            this.labelBcc = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.textBoxAttachment = new System.Windows.Forms.TextBox();
            this.labelSubject = new System.Windows.Forms.Label();
            this.labelAttachment = new System.Windows.Forms.Label();
            this.groupBoxSmtpServer = new System.Windows.Forms.GroupBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPOP3Password = new System.Windows.Forms.Label();
            this.labelPOP3Username = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelExchangeServer = new System.Windows.Forms.Label();
            this.textBoxExchangeServer = new System.Windows.Forms.TextBox();
            this.buttonRetrieve = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.labelLog = new System.Windows.Forms.Label();
            this.groupBoxRetrieve = new System.Windows.Forms.GroupBox();
            this.comboBoxDateOption = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerSearchDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxSearchSubject = new System.Windows.Forms.TextBox();
            this.textBoxSearchFrom = new System.Windows.Forms.TextBox();
            this.textBoxFileLocation = new System.Windows.Forms.TextBox();
            this.comboBoxPriority = new System.Windows.Forms.ComboBox();
            this.labelPriority = new System.Windows.Forms.Label();
            this.groupBoxSmtpServer.SuspendLayout();
            this.groupBoxRetrieve.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(679, 6);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "&Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(6, 6);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(29, 13);
            this.labelTo.TabIndex = 1;
            this.labelTo.Text = "To...";
            // 
            // textBoxTo
            // 
            this.textBoxTo.Location = new System.Drawing.Point(54, 6);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(392, 20);
            this.textBoxTo.TabIndex = 0;
            // 
            // textBoxCc
            // 
            this.textBoxCc.Location = new System.Drawing.Point(54, 29);
            this.textBoxCc.Name = "textBoxCc";
            this.textBoxCc.Size = new System.Drawing.Size(392, 20);
            this.textBoxCc.TabIndex = 1;
            // 
            // labelCc
            // 
            this.labelCc.AutoSize = true;
            this.labelCc.Location = new System.Drawing.Point(6, 32);
            this.labelCc.Name = "labelCc";
            this.labelCc.Size = new System.Drawing.Size(29, 13);
            this.labelCc.TabIndex = 3;
            this.labelCc.Text = "Cc...";
            // 
            // textBoxBcc
            // 
            this.textBoxBcc.Location = new System.Drawing.Point(54, 52);
            this.textBoxBcc.Name = "textBoxBcc";
            this.textBoxBcc.Size = new System.Drawing.Size(392, 20);
            this.textBoxBcc.TabIndex = 2;
            // 
            // labelBcc
            // 
            this.labelBcc.AutoSize = true;
            this.labelBcc.Location = new System.Drawing.Point(6, 55);
            this.labelBcc.Name = "labelBcc";
            this.labelBcc.Size = new System.Drawing.Size(35, 13);
            this.labelBcc.TabIndex = 5;
            this.labelBcc.Text = "Bcc...";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(54, 76);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(392, 20);
            this.textBoxSubject.TabIndex = 3;
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(8, 129);
            this.textBoxBody.Multiline = true;
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxBody.Size = new System.Drawing.Size(438, 114);
            this.textBoxBody.TabIndex = 5;
            // 
            // textBoxAttachment
            // 
            this.textBoxAttachment.Location = new System.Drawing.Point(54, 100);
            this.textBoxAttachment.Name = "textBoxAttachment";
            this.textBoxAttachment.Size = new System.Drawing.Size(392, 20);
            this.textBoxAttachment.TabIndex = 4;
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(5, 79);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(46, 13);
            this.labelSubject.TabIndex = 9;
            this.labelSubject.Text = "Subject:";
            // 
            // labelAttachment
            // 
            this.labelAttachment.AutoSize = true;
            this.labelAttachment.Location = new System.Drawing.Point(6, 103);
            this.labelAttachment.Name = "labelAttachment";
            this.labelAttachment.Size = new System.Drawing.Size(47, 13);
            this.labelAttachment.TabIndex = 11;
            this.labelAttachment.Text = "Attach...";
            this.labelAttachment.DoubleClick += new System.EventHandler(this.labelAttachment_DoubleClick);
            // 
            // groupBoxSmtpServer
            // 
            this.groupBoxSmtpServer.Controls.Add(this.textBoxPassword);
            this.groupBoxSmtpServer.Controls.Add(this.labelPOP3Password);
            this.groupBoxSmtpServer.Controls.Add(this.labelPOP3Username);
            this.groupBoxSmtpServer.Controls.Add(this.textBoxUsername);
            this.groupBoxSmtpServer.Controls.Add(this.labelExchangeServer);
            this.groupBoxSmtpServer.Controls.Add(this.textBoxExchangeServer);
            this.groupBoxSmtpServer.Location = new System.Drawing.Point(453, 35);
            this.groupBoxSmtpServer.Name = "groupBoxSmtpServer";
            this.groupBoxSmtpServer.Size = new System.Drawing.Size(308, 90);
            this.groupBoxSmtpServer.TabIndex = 13;
            this.groupBoxSmtpServer.TabStop = false;
            this.groupBoxSmtpServer.Text = "SmtpServer";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(72, 64);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 2;
            // 
            // labelPOP3Password
            // 
            this.labelPOP3Password.AutoSize = true;
            this.labelPOP3Password.Location = new System.Drawing.Point(8, 67);
            this.labelPOP3Password.Name = "labelPOP3Password";
            this.labelPOP3Password.Size = new System.Drawing.Size(56, 13);
            this.labelPOP3Password.TabIndex = 13;
            this.labelPOP3Password.Text = "Password:";
            // 
            // labelPOP3Username
            // 
            this.labelPOP3Username.AutoSize = true;
            this.labelPOP3Username.Location = new System.Drawing.Point(8, 43);
            this.labelPOP3Username.Name = "labelPOP3Username";
            this.labelPOP3Username.Size = new System.Drawing.Size(58, 13);
            this.labelPOP3Username.TabIndex = 11;
            this.labelPOP3Username.Text = "Username:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(72, 40);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxUsername.TabIndex = 1;
            // 
            // labelExchangeServer
            // 
            this.labelExchangeServer.AutoSize = true;
            this.labelExchangeServer.Location = new System.Drawing.Point(8, 17);
            this.labelExchangeServer.Name = "labelExchangeServer";
            this.labelExchangeServer.Size = new System.Drawing.Size(58, 13);
            this.labelExchangeServer.TabIndex = 7;
            this.labelExchangeServer.Text = "Exchange:";
            // 
            // textBoxExchangeServer
            // 
            this.textBoxExchangeServer.Location = new System.Drawing.Point(72, 16);
            this.textBoxExchangeServer.Name = "textBoxExchangeServer";
            this.textBoxExchangeServer.Size = new System.Drawing.Size(229, 20);
            this.textBoxExchangeServer.TabIndex = 0;
            // 
            // buttonRetrieve
            // 
            this.buttonRetrieve.Location = new System.Drawing.Point(240, 78);
            this.buttonRetrieve.Name = "buttonRetrieve";
            this.buttonRetrieve.Size = new System.Drawing.Size(61, 23);
            this.buttonRetrieve.TabIndex = 4;
            this.buttonRetrieve.Text = "&Retrieve";
            this.buttonRetrieve.UseVisualStyleBackColor = true;
            this.buttonRetrieve.Click += new System.EventHandler(this.buttonRetrieve_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 262);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(759, 221);
            this.richTextBoxLog.TabIndex = 6;
            this.richTextBoxLog.Text = "";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(7, 246);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(28, 13);
            this.labelLog.TabIndex = 16;
            this.labelLog.Text = "Log:";
            // 
            // groupBoxRetrieve
            // 
            this.groupBoxRetrieve.Controls.Add(this.comboBoxDateOption);
            this.groupBoxRetrieve.Controls.Add(this.label4);
            this.groupBoxRetrieve.Controls.Add(this.label3);
            this.groupBoxRetrieve.Controls.Add(this.label2);
            this.groupBoxRetrieve.Controls.Add(this.label1);
            this.groupBoxRetrieve.Controls.Add(this.dateTimePickerSearchDate);
            this.groupBoxRetrieve.Controls.Add(this.textBoxSearchSubject);
            this.groupBoxRetrieve.Controls.Add(this.textBoxSearchFrom);
            this.groupBoxRetrieve.Controls.Add(this.textBoxFileLocation);
            this.groupBoxRetrieve.Controls.Add(this.buttonRetrieve);
            this.groupBoxRetrieve.Location = new System.Drawing.Point(453, 131);
            this.groupBoxRetrieve.Name = "groupBoxRetrieve";
            this.groupBoxRetrieve.Size = new System.Drawing.Size(307, 108);
            this.groupBoxRetrieve.TabIndex = 17;
            this.groupBoxRetrieve.TabStop = false;
            this.groupBoxRetrieve.Text = "Search";
            // 
            // comboBoxDateOption
            // 
            this.comboBoxDateOption.FormattingEnabled = true;
            this.comboBoxDateOption.Location = new System.Drawing.Point(150, 80);
            this.comboBoxDateOption.Name = "comboBoxDateOption";
            this.comboBoxDateOption.Size = new System.Drawing.Size(80, 21);
            this.comboBoxDateOption.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Subject:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "From:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Folder:";
            // 
            // dateTimePickerSearchDate
            // 
            this.dateTimePickerSearchDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerSearchDate.Location = new System.Drawing.Point(57, 81);
            this.dateTimePickerSearchDate.Name = "dateTimePickerSearchDate";
            this.dateTimePickerSearchDate.Size = new System.Drawing.Size(87, 20);
            this.dateTimePickerSearchDate.TabIndex = 3;
            // 
            // textBoxSearchSubject
            // 
            this.textBoxSearchSubject.Location = new System.Drawing.Point(57, 55);
            this.textBoxSearchSubject.Name = "textBoxSearchSubject";
            this.textBoxSearchSubject.Size = new System.Drawing.Size(244, 20);
            this.textBoxSearchSubject.TabIndex = 2;
            // 
            // textBoxSearchFrom
            // 
            this.textBoxSearchFrom.Location = new System.Drawing.Point(57, 33);
            this.textBoxSearchFrom.Name = "textBoxSearchFrom";
            this.textBoxSearchFrom.Size = new System.Drawing.Size(244, 20);
            this.textBoxSearchFrom.TabIndex = 1;
            // 
            // textBoxFileLocation
            // 
            this.textBoxFileLocation.Location = new System.Drawing.Point(57, 11);
            this.textBoxFileLocation.Name = "textBoxFileLocation";
            this.textBoxFileLocation.Size = new System.Drawing.Size(244, 20);
            this.textBoxFileLocation.TabIndex = 0;
            // 
            // comboBoxPriority
            // 
            this.comboBoxPriority.FormattingEnabled = true;
            this.comboBoxPriority.Location = new System.Drawing.Point(525, 8);
            this.comboBoxPriority.Name = "comboBoxPriority";
            this.comboBoxPriority.Size = new System.Drawing.Size(100, 21);
            this.comboBoxPriority.TabIndex = 18;
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(461, 11);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(41, 13);
            this.labelPriority.TabIndex = 19;
            this.labelPriority.Text = "Priority:";
            // 
            // FormEmailClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 488);
            this.Controls.Add(this.labelPriority);
            this.Controls.Add(this.comboBoxPriority);
            this.Controls.Add(this.groupBoxRetrieve);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.groupBoxSmtpServer);
            this.Controls.Add(this.labelAttachment);
            this.Controls.Add(this.textBoxAttachment);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.textBoxBody);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.textBoxBcc);
            this.Controls.Add(this.labelBcc);
            this.Controls.Add(this.textBoxCc);
            this.Controls.Add(this.labelCc);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.buttonSend);
            this.Name = "FormEmailClient";
            this.Text = "Email Client";
            this.Load += new System.EventHandler(this.FormEmailSend_Load);
            this.groupBoxSmtpServer.ResumeLayout(false);
            this.groupBoxSmtpServer.PerformLayout();
            this.groupBoxRetrieve.ResumeLayout(false);
            this.groupBoxRetrieve.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.TextBox textBoxCc;
        private System.Windows.Forms.Label labelCc;
        private System.Windows.Forms.TextBox textBoxBcc;
        private System.Windows.Forms.Label labelBcc;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.TextBox textBoxAttachment;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelAttachment;
        private System.Windows.Forms.GroupBox groupBoxSmtpServer;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPOP3Password;
        private System.Windows.Forms.Label labelPOP3Username;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonRetrieve;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Label labelExchangeServer;
        private System.Windows.Forms.TextBox textBoxExchangeServer;
        private System.Windows.Forms.GroupBox groupBoxRetrieve;
        private System.Windows.Forms.DateTimePicker dateTimePickerSearchDate;
        private System.Windows.Forms.TextBox textBoxSearchSubject;
        private System.Windows.Forms.TextBox textBoxSearchFrom;
        private System.Windows.Forms.TextBox textBoxFileLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDateOption;
        private System.Windows.Forms.ComboBox comboBoxPriority;
        private System.Windows.Forms.Label labelPriority;
    }
}

