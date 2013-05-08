using System;
using System.Collections.Generic;
using System.Text;

namespace CentralClient
{
  public partial class MessageForm : C1.Win.C1Ribbon.C1RibbonForm
  {
      private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
      private C1.Win.C1Ribbon.RibbonButton CloseRibbonButton;
      private C1.Win.C1Input.C1TextBox MessageTextBox;

    public MessageForm()
    {
      InitializeComponent();
    }
    
    public void Set(string formName, string message)
    {
      MessageTextBox.Text = message;
      this.TopMost = true;
      this.Show();
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
        this.MessageTextBox = new C1.Win.C1Input.C1TextBox();
        this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
        this.CloseRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
        ((System.ComponentModel.ISupportInitialize)(this.MessageTextBox)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
        this.SuspendLayout();
        // 
        // MessageTextBox
        // 
        this.MessageTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
        this.MessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.MessageTextBox.Location = new System.Drawing.Point(0, 0);
        this.MessageTextBox.Multiline = true;
        this.MessageTextBox.Name = "MessageTextBox";
        this.MessageTextBox.ReadOnly = true;
        this.MessageTextBox.Size = new System.Drawing.Size(226, 103);
        this.MessageTextBox.TabIndex = 1;
        this.MessageTextBox.Tag = null;
        this.MessageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        this.MessageTextBox.TextDetached = true;
        this.MessageTextBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
        // 
        // c1StatusBar1
        // 
        this.c1StatusBar1.Name = "c1StatusBar1";
        this.c1StatusBar1.RightPaneItems.Add(this.CloseRibbonButton);
        // 
        // CloseRibbonButton
        // 
        this.CloseRibbonButton.Name = "CloseRibbonButton";
        this.CloseRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("CloseRibbonButton.SmallImage")));
        this.CloseRibbonButton.Text = "Close";
        this.CloseRibbonButton.Click += new System.EventHandler(this.CloseRibbonButton_Click);
        // 
        // MessageForm
        // 
        this.ClientSize = new System.Drawing.Size(226, 126);
        this.Controls.Add(this.MessageTextBox);
        this.Controls.Add(this.c1StatusBar1);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "MessageForm";
        this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
        ((System.ComponentModel.ISupportInitialize)(this.MessageTextBox)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private void CloseRibbonButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }
  }
}
