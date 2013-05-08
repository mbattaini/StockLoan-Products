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
using C1.Silverlight.Theming;
using C1.Silverlight.Docking;

namespace StockLoan_LocatesClient
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
			this.header = header;
            _item.Content = content;								    
			_item.CanUserClose = true;  
			_item.TabShape = C1TabItemShape.Sloped;
			_item.Header = header;						
		}

        public void CustomEvents_SecIdChanged(object sender, SecIdEventArgs e)
        {            
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