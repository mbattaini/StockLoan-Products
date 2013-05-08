using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using C1.Silverlight;
using C1.Silverlight.Docking;

namespace WorldWideClient
{
	public class ClientItem
	{
        private C1DockTabItem _item;
        private string header = "";

        public ClientItem()
        {
            _item = new C1DockTabItem();
            _item.GotFocus += new RoutedEventHandler(_item_GotFocus);            
            CustomEvents.SecIdChanged += new EventHandler<SecIdEventArgs>(CustomEvents_SecIdChanged);
        }

        void _item_GotFocus(object sender, RoutedEventArgs e)
        {
            ((UserControl)_item.Content).Focus();
        }


        public void Load(string header, FrameworkElement content)
        {
            Load(header, content, true);            
        }

		public void Load(string header, FrameworkElement content, bool showHeader)
		{					
			this.header = header;
            _item.Content = content;								    
			_item.CanUserClose = true;  
			_item.TabShape = C1TabItemShape.Sloped;

            if (showHeader)
            {
                _item.Header = header;
            }
            else
            {
                _item.Header = "";
            }
		}

        public void CustomEvents_SecIdChanged(object sender, SecIdEventArgs e)
        {
            _item.Header = header + " " + e.SecId;
        }

        public object Item
        {
            get
            {
                return _item;
            }
        }
	}
}