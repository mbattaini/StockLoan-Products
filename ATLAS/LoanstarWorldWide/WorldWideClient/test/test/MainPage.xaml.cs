using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Blacklight.Controls;

namespace test
{
	public partial class MainPage : UserControl
	{
		
		private int tempCount;
		
		public MainPage()
		{
			// Required to initialize variables
			InitializeComponent();	
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
		
		}

		private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{					
		}

		private void AddTab_Click(object sender, System.Windows.RoutedEventArgs e)
		{
		/*	C1.Silverlight.C1TabItem test = new C1.Silverlight.C1TabItem();
			test.Header = "Tab Item : " + tempCount.ToString();			
			test.Content = new TestElement();
			TabControlTest.Items.Add(test);
			
			CanvasTest.Children.Add(new TestElement());
			
			tempCount ++;*/
		}

		private void DropGrid_Drop(object sender, System.Windows.DragEventArgs e)
		{
			//DropGrid.Children.Add((UIElement) e.Data);
		}					
	}
}