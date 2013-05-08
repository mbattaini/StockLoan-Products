using System;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DashApplication
{
    public partial class GenericTileCntrl : UserControl
    {        
        public GenericTileCntrl(string header, CustomTypes.MinimizedControlTypes mType)
        {
            InitializeComponent();
            HeaderLabel.Content = header;

            switch (mType)
            {
                case CustomTypes.MinimizedControlTypes.Chart:
                    GenericImage.Source = new BitmapImage(new Uri("Images/Other/MB_0003_Apps.png", UriKind.Relative));
                    break;

                case CustomTypes.MinimizedControlTypes.Grid:
                    GenericImage.Source = new BitmapImage(new Uri("Images/Other/MB_0007_book.png", UriKind.Relative));
                    break;

                case CustomTypes.MinimizedControlTypes.Page:
                    GenericImage.Source = new BitmapImage(new Uri("Images/Other/MB_0029_netw_conn.png", UriKind.Relative));
                    break;
            }

            this.InvalidateMeasure();
        }
    }
}
