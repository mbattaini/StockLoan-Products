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
	public class SysWindow
	{				
		public static void Show(string message, string callingClass)
		{
            
			C1Window _window;

			_window = new C1Window();

            _window.Style = (Style)UserInformation.ResourceGet("TransparantWindow");
			_window.UpdateLayout();
                       

            TextBlock txt = new TextBlock();
            txt.Text = message + " " + callingClass;

            _window.Content = txt;
            _window.Show();
            _window.CenterOnScreen();
		}
	}
}