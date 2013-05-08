using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Anetics.Medalist
{
  public enum PilotState
  {
    Idle,
    Normal,
    RunFault,
    Unknown
  }
    
  public class Pilot : System.Windows.Forms.UserControl
  {
    private double heartbeatInterval = 0D;
    private DateTime heartbeatTimestamp;

    private System.ComponentModel.Container components = null;
    delegate void refreshPilotState();

    public Pilot()
    {      
      InitializeComponent();
    }

    private PilotState state = PilotState.Idle;

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

    #region Component Designer generated code
    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      // 
      // Pilot
      // 
      this.BackColor = System.Drawing.Color.Gray;
      this.Name = "Pilot";
      this.Size = new System.Drawing.Size(9, 9);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.Pilot_Paint);

    }
    #endregion

    public double HeartbeatInterval
    {
      set
      {
        heartbeatInterval = value;
      }
    }
          
    public PilotState State
    {
      get
      {
          if (!heartbeatInterval.Equals(0D) &&
            heartbeatTimestamp.AddMilliseconds(2.5 * heartbeatInterval).CompareTo(DateTime.UtcNow).Equals(-1))
          {
              State = PilotState.Unknown;
          }
        
        return state;
      }

        set
        {
            heartbeatTimestamp = DateTime.UtcNow;

            state = value;

            switch (state)
            {
                case PilotState.Idle:
                    this.BackColor = Color.Gray;
                    break;
                case PilotState.Normal:
                    this.BackColor = Color.Lime;
                    break;
                case PilotState.RunFault:
                    this.BackColor = Color.Coral;
                    break;
                case PilotState.Unknown:
                    this.BackColor = Color.Yellow;
                    break;
            }

            PilotRefresh();
        }
    }

    private void PilotRefresh()
    {
        if (this.InvokeRequired)
        {
            refreshPilotState refresh = new refreshPilotState(PilotRefresh);
            this.Invoke(refresh);
        }
        else
        {
            this.Refresh();
        }
    }


    private void Pilot_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Pen pen = new System.Drawing.Pen(Color.Black);      
      Rectangle border = new Rectangle( 0, 0, this.Width - 1,  this.Height - 1);

      graphics.DrawRectangle(pen, border);

      pen.Dispose();
      graphics.Dispose();    
    }
  }
}
