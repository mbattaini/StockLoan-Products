using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace StockLoan.Locates.Infrastructure.Interface
{
    public class ShellFormEventArgs : System.EventArgs
    {
        public FormWindowState WindowState;
        public Rectangle Bounds;
    }
}
