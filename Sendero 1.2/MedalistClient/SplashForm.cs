using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Anetics.Medalist
{
  public class SplashForm : System.Windows.Forms.Form
  {
    private string licensee;
    private string applicationName;

    private System.Timers.Timer SplashTimer;
    
    private System.ComponentModel.Container components = null;

    public SplashForm(string licensee, string applicationName)
    {
      InitializeComponent();
      
      this.licensee = licensee;
      this.applicationName = applicationName;
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SplashForm));
      this.SplashTimer = new System.Timers.Timer();
      ((System.ComponentModel.ISupportInitialize)(this.SplashTimer)).BeginInit();
      // 
      // SplashTimer
      // 
      this.SplashTimer.Interval = 3750;
      this.SplashTimer.SynchronizingObject = this;
      this.SplashTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.SplashTimer_Elapsed);
      // 
      // SplashForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
      this.BackColor = System.Drawing.SystemColors.Control;
      this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
      this.ClientSize = new System.Drawing.Size(550, 276);
      this.ControlBox = false;
      this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.ForeColor = System.Drawing.Color.Black;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "SplashForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new System.EventHandler(this.Splash_Load);
      ((System.ComponentModel.ISupportInitialize)(this.SplashTimer)).EndInit();

    }
    #endregion

    private void Splash_Load(object sender, System.EventArgs e)
    {
      Graphics graphics = this.CreateGraphics();

      Font font;
      SolidBrush solidBrush;

      this.Show();
      Application.DoEvents();

      solidBrush = new SolidBrush(Color.DimGray);
      font = new Font("Verdana", 10);
      graphics.DrawString("This product licensed to:", font, solidBrush, 160, 45);

      solidBrush = new SolidBrush(Color.MidnightBlue);
      font = new Font("Verdana", 14);
      graphics.DrawString(licensee, font, solidBrush, 375 - (licensee.Length * 5), 60);

      solidBrush = new SolidBrush(Color.FromArgb(135, 95, 105));
      font = new Font("Comic Sans MS", 26);
      graphics.DrawString(applicationName, font, solidBrush, 200, 72);

      solidBrush = new SolidBrush(Color.Black);
      font = new Font("Verdana", 10);

      graphics.DrawString("Based on the Anetics Medalist™ system for Securities Lending ", font, solidBrush, 65, 155);

      graphics.DrawString("Version " + Application.ProductVersion , font, solidBrush, 195, 200);

      font = new Font("Verdana", 8);
      graphics.DrawString("Copyright © 2004 - 2005  Anetics, LLC.  All rights reserved.", font, solidBrush, 115, 245);

      SplashTimer.Enabled = true;
    }

    private void SplashTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      this.Close();
    }
  }
}
