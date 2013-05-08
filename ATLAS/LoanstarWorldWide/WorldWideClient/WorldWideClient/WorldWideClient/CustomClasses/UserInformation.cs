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
    public static class UserInformation
    {
     

        private static string _userId;
        private static string _password;
        private static string _bookGroup;
		
        private static bool _allowUser;
		private static bool _logOffUser;
		
        private static bool _useGlobalFilterSecId;
        private static string _globalFilterSecId;
		
		private static ResourceDictionary _resource;

        public static string UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
                CustomEvents.UpdateUserInformation(_userId, _logOffUser, 0);				
            }
        }
		
	
		public static object ResourceGet(string key)
		{
            return Application.Current.Resources[key];      
		}
		
		public static void ResourceSet(ResourceDictionary _temp)
		{
            _resource = _temp;
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
		
		public static bool LogOffUser
		{
			get
			{
				return _logOffUser;
			}
			
			set
			{
				_logOffUser = value;
				
			 	if (_logOffUser) {_userId = "";}
				
				CustomEvents.UpdateUserInformation(_userId, _logOffUser, 0);
			}			
		}

    }

  
}