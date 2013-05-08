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

namespace DashApplication
{
	public class ClientWindow
	{
		private C1Window _window;
		
		public ClientWindow()
		{
			_window = new C1Window();

            //_window.Style = (Style)UserInformation.ResourceGet("TransparantWindow");
			_window.UpdateLayout();
		}

        public void Close()
        {
            _window.Close();            
        }

		public void Show()
		{			
			_window.Show();
		}
		
		public object Content
		{
			set
			{
				_window.Content = value;
			}
		}
		
		public bool Resize
		{
			set
			{
				_window.IsResizable = value;
			}
		}
		
		public string WindowHeader
		{
			set
			{
				_window.Header = value;				
			}
		}
		
		public void CenterOnScreen()
		{
			_window.CenterOnScreen();
		}
	}
}