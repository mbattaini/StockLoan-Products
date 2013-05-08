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

using C1.Silverlight.Data;

namespace LoanStarWorldWideAdmin
{
	public class AppInformation
	{
		private static string userId = "";
        private static string fullName = "";
        private static string title = "";
        private static string ePassword = "";
        //private static string adminFlag = "*All*";	
		
		public static string UserId
		{
			get	{return userId;}
				
			set	{userId = value;}
		}

        //public static string AdminFlag
        //{
        //    get { return adminFlag; }
        //}

        public static string FullName
        {
            get { return fullName; }

            set { fullName = value; }
        }

        public static string Password
        {
            get { return ePassword; }

            set { ePassword = value; }
        }

        public static string Title
        {
            get { return title; }

            set { title = value; }
        }
	}		
}