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

namespace WorldWideClient
{
	public class SystemEventWindow
	{
		public static void Show(string message)
		{	
			C1Window _window = new C1Window();				
 			//ClientTheme.ApplyTheme(_window);			
			Label label = new Label();
			
			label.Content = message;			
			_window.Content = label;						
			_window.Header=  "System Event Notification!";
			_window.IsResizable = false;
			_window.Show();
			_window.CenterOnScreen();			
			_window.Show();
			_window.BringToFront();
		}
	}
}