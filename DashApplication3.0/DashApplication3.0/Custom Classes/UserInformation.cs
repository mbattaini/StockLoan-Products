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

namespace DashApplication
{
	public class UserInformation
	{
		private static string userId = "";
        private static string fullName = "";
        private static string title = "";
			
			public static string UserId
			{
				get	{return userId;}
				
				set	{userId = value;}
			}

            public static string FullName
            {
                get { return fullName; }

                set { fullName = value; }
            }

            public static string Title
            {
                get { return title; }

                set { title = value; }
            }
			
				public static object ResourceGet(string key)
		{
            return Application.Current.Resources[key];      
		}
	}
}