using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CentralClient
{

  public partial class MainServerStatusEventsForm : C1.Win.C1Ribbon.C1RibbonForm
  {
    private MainForm mainForm = null;
    private Str
    public MainServerStatusEventsForm(MainForm mainForm)
    {
      InitializeComponent();

      this.mainForm = mainForm;
    }

    private void BookContactsGrid_OwnerDrawCell(object sender, C1.Win.C1TrueDBGrid.OwnerDrawCellEventArgs e)
    {
      
      
      if (StatusGrid.Columns[e.Col].DataField.Equals("Status") && e.Row > 0)
      {
        // get song
        DataRowView drv = (DataRowView)StatusGrid.Rows[e.Row].Table;
        if (drv == null) return;
        DataRow dr = drv.Row;

        // see if we're copying the song
        /*if (!_son.ContainsKey(dr))
          return;*/

        // calculate how much is done
        DateTime start = (DateTime)_songs[dr];
        TimeSpan elapsed = DateTime.Now - start;
        TimeSpan length = (TimeSpan)dr["Length"];
        int pct = (length.TotalSeconds > 0)
          ? (int)(elapsed.TotalSeconds / length.TotalSeconds * 100f * 20f)
          : 100;

        // song is done? remove from list
        if (pct >= 100)
        {
          _songs.Remove(dr);
          dr["Status"] = "Completed";
          return;
        }

        // draw background
        e.Style = StatusGrid.Styles.Highlight;
        e.DrawCell(DrawCellFlags.Background);

        // progress bar outline
        Rectangle rc = e.Bounds;
        rc.Width--;
        rc.Height--;
        e.Graphics.DrawRectangle(_pen, rc);

        // fill progress bar
        rc = e.Bounds;
        rc.Inflate(-2, -2);
        rc.Width = rc.Width * pct / 100;
        e.Graphics.DrawImage(_bmp, rc);

        // draw text       
        e.Text = "Copying " + pct + "% done";
        e.DrawCell(DrawCellFlags.Content);
        e.Handled = true;
      }
    }
  }
}