using System;
using System.Collections.Generic;
using System.Text;

namespace LocatesClient
{
  public partial class MessageForm : C1.Win.C1Ribbon.C1RibbonForm
  {
    private C1.Win.C1Input.C1TextBox MessageTextBox;
    private C1.Win.C1Input.C1Button CloseButton;

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
      this.CloseButton = new C1.Win.C1Input.C1Button();
      this.MessageTextBox = new C1.Win.C1Input.C1TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.MessageTextBox)).BeginInit();
      this.SuspendLayout();
      // 
      // CloseButton
      // 
      this.CloseButton.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.CloseButton.Location = new System.Drawing.Point(0, 103);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.Size = new System.Drawing.Size(226, 23);
      this.CloseButton.TabIndex = 0;
      this.CloseButton.Text = "Close";
      this.CloseButton.UseVisualStyleBackColor = true;
      this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
      // 
      // MessageTextBox
      // 
      this.MessageTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
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
      // 
      // MessageForm
      // 
      this.ClientSize = new System.Drawing.Size(226, 126);
      this.Controls.Add(this.MessageTextBox);
      this.Controls.Add(this.CloseButton);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MessageForm";
      ((System.ComponentModel.ISupportInitialize)(this.MessageTextBox)).EndInit();
      this.ResumeLayout(false);

    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
