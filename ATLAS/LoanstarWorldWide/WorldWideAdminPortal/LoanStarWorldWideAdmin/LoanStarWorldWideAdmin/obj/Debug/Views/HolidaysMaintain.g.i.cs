﻿#pragma checksum "C:\_dev\ATLAS\LoanstarWorldWide\WorldWideAdminPortal\LoanStarWorldWideAdmin\LoanStarWorldWideAdmin\Views\HolidaysMaintain.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FA6945543A5D6D7E072617621E98AC7F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using C1.Silverlight;
using C1.Silverlight.DataGrid;
using C1.Silverlight.Schedule;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace LoanStarWorldWideAdmin.Views {
    
    
    public partial class HolidaysMaintain : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Page HolidayMaintenance;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid CaptionGrid;
        
        internal System.Windows.Controls.Label UpdateLabel;
        
        internal System.Windows.Controls.Label StatusLabel;
        
        internal System.Windows.Controls.Label BookGroupHeadLabel;
        
        internal C1.Silverlight.C1ComboBox BookGroupCombo;
        
        internal System.Windows.Controls.StackPanel CalendarMainStackPanel;
        
        internal C1.Silverlight.Schedule.C1Calendar HolidayCalendar;
        
        internal System.Windows.Controls.Button HolidayEditButton;
        
        internal System.Windows.Controls.Button HolidayAddButton;
        
        internal C1.Silverlight.DataGrid.C1DataGrid HolidayListGrid;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn countryCode;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn bookGroup;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn country;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn holidayDate;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn bankHoliday;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn exchangeHoliday;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn holidayExplain;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/LoanStarWorldWideAdmin;component/Views/HolidaysMaintain.xaml", System.UriKind.Relative));
            this.HolidayMaintenance = ((System.Windows.Controls.Page)(this.FindName("HolidayMaintenance")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.CaptionGrid = ((System.Windows.Controls.Grid)(this.FindName("CaptionGrid")));
            this.UpdateLabel = ((System.Windows.Controls.Label)(this.FindName("UpdateLabel")));
            this.StatusLabel = ((System.Windows.Controls.Label)(this.FindName("StatusLabel")));
            this.BookGroupHeadLabel = ((System.Windows.Controls.Label)(this.FindName("BookGroupHeadLabel")));
            this.BookGroupCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("BookGroupCombo")));
            this.CalendarMainStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CalendarMainStackPanel")));
            this.HolidayCalendar = ((C1.Silverlight.Schedule.C1Calendar)(this.FindName("HolidayCalendar")));
            this.HolidayEditButton = ((System.Windows.Controls.Button)(this.FindName("HolidayEditButton")));
            this.HolidayAddButton = ((System.Windows.Controls.Button)(this.FindName("HolidayAddButton")));
            this.HolidayListGrid = ((C1.Silverlight.DataGrid.C1DataGrid)(this.FindName("HolidayListGrid")));
            this.countryCode = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("countryCode")));
            this.bookGroup = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("bookGroup")));
            this.country = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("country")));
            this.holidayDate = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("holidayDate")));
            this.bankHoliday = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("bankHoliday")));
            this.exchangeHoliday = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("exchangeHoliday")));
            this.holidayExplain = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("holidayExplain")));
        }
    }
}

