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

namespace WorldWideClient
{
	public class UserInformation
	{	        
        private static string _userId;
		private static string _password;
		private static string _bookGroup;
		
		private static bool _allowUser;
		
		private static bool _useGlobalFilterSecId;
		private static string _globalFilterSecId;

		public static string UserId
		{
			get
			{
				return _userId;
			}
			
			set
			{
				_userId = value;
			}
		}
		
		public static string Password
		{
			get
			{
				return _password;
			}
			
			set
			{
				_password = value;
			}
		}
		
		public static string BookGroup
		{
			get
			{
				return _bookGroup;
			}
			
			set
			{
				_bookGroup = value;
			}
		}
		
		public static bool AllowUser
		{
			get
			{
				return _allowUser;
			}
			
			set
			{
				_allowUser = value;
			}
		}
		
		
		public static bool UseGlobalFilterSecId
		{
			get
			{
				return _useGlobalFilterSecId;
			}
			
			set
			{
				_useGlobalFilterSecId = value;
			}
		}
				
		public static string GlobalFilterSecId
		{
			get
			{
				return _globalFilterSecId;
			}
			
			set
			{
				_globalFilterSecId = value;
			}
		}			
	}
}