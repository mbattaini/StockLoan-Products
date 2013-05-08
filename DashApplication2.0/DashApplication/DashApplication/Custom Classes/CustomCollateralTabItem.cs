using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight;


namespace DashApplication.CustomClasses
{
    public class CustomCollateralTabItem
    {
        public static C1TabItem Create(FrameworkElement element, string tabHeader, bool canUserClose)
        {
            C1TabItem _item = new C1TabItem();
            _item.Style = (Style)Application.Current.Resources["C1TabItemStyle"];
            _item.Content = element;            
            _item.Header = tabHeader;
            _item.CanUserClose = canUserClose;
            _item.Focus();
            return _item;
        }
    }
}
