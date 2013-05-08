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

namespace DashApplication
{
    public class UserInformation
    {
        private static string userId = "";
        private static string fullName = "";
        private static string title = "";
        private static string password = "";

        private static DataTable dtUserProfile = null;

        public static bool ReturnRoleView(string functionPath)
        {
            bool view = false;
                      
            try
            {
                for (int index = 0; index < dtUserProfile.Rows.Count; index++)
                {
                    if (dtUserProfile.Rows[index]["functionPath"].ToString().Equals(functionPath))
                    {
                        view = bool.Parse(dtUserProfile.Rows[index]["View"].ToString());
                    }
                }
            }
            catch
            {
                view = false;
            }

            return view;
        }

        public static DataTable UserProfile
        {
            set { dtUserProfile = value; }
        }

        public static string UserId
        {
            get { return userId; }

            set { userId = value; }
        }

        public static string Password
        {
            get { return password; }
            set { password = value; }
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