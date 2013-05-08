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

    public class Functions
    {
        public AppInformation appInfo = new AppInformation();
        
        public static DataTable ConvertToDataTable(byte[] e, string tableName)
        {
            DataSet dsTemp = new DataSet();

            var ms = new System.IO.MemoryStream(e);
            dsTemp = new DataSet();
            dsTemp.ReadXml(ms);

            return dsTemp.Tables[tableName];
        }

        
        public void SetFunctionPath(string temp)
        {
            switch (temp.ToUpper())
            {
                case "INVENTORYFILELAYOUTS":
                    appInfo.FunctionPath = "InventorySubscriptions";
                    ((App)App.Current).ScreenNameText = "Inventory File Layouts";
                    break;
                case "FIRMMAINTENANCE":
                    appInfo.FunctionPath = "InventorySubscriptions";
                    ((App)App.Current).ScreenNameText = "Firm Administration";
                    break;
                case "SUBSCRIPTIONINFORMATION":
                    appInfo.FunctionPath = "InventorySubscriptions";
                    ((App)App.Current).ScreenNameText = "Subscription Information";
                    break;
                case "SUBSCRIPTIONMAINTENANCE":
                    appInfo.FunctionPath = "InventorySubscriptions";
                    ((App)App.Current).ScreenNameText = "Subscription Administration";
                    break;
                case "ADMINPORTALMAIN":
                    appInfo.FunctionPath = "AdminUsers";
                    ((App)App.Current).ScreenNameText = "Administrator Portal Login";
                    break;
                case "PASSWORDCHANGE":
                    appInfo.FunctionPath = "AdminUsers";
                    ((App)App.Current).ScreenNameText = "Change Password";
                    break;
                case "PASSWORDRESET":
                    appInfo.FunctionPath = "AdminUsers";
                    ((App)App.Current).ScreenNameText = "Reset Password";
                    break;
                case "MAINTAINROLES":
                    appInfo.FunctionPath = "AdminUsers";
                    ((App)App.Current).ScreenNameText = "Role Functions Administration";
                    break;
                case "MAINTAINUSERS":
                    appInfo.FunctionPath = "AdminUsers";
                    ((App)App.Current).ScreenNameText = "User Administration";
                    break;
                case "COUNTRYCURRENCYMAINTENANCE":
                    appInfo.FunctionPath = "AdminCountries";
                    ((App)App.Current).ScreenNameText = "Country Currency Administration";
                    break;
                case "HOLIDAYMAINTENANCE":
                    appInfo.FunctionPath = "AdminHolidays";
                    ((App)App.Current).ScreenNameText = "Holidays Administration";
                    break;
                default:
                    appInfo.FunctionPath = "AdminUsers";
                    ((App)App.Current).ScreenNameText = "Administrator Portal Login";
                    break;

            }


        }
    }

}