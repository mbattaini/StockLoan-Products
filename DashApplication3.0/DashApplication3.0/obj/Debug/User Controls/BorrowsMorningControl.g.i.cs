﻿#pragma checksum "C:\_dev\DashApplication3.0\DashApplication3.0\User Controls\BorrowsMorningControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0DC473545F5D747ADC1D772920DAEF7C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace DashApplication {
    
    
    public partial class BorrowsMorningControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.UserControl MainUserControl;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal C1.Silverlight.DataGrid.C1DataGrid BorrowsGrid;
        
        internal C1.Silverlight.Toolbar.C1ToolbarStrip MainToolBarStrip;
        
        internal System.Windows.Controls.DatePicker BizDatePicker;
        
        internal C1.Silverlight.Toolbar.C1ToolbarButton LoadListButton;
        
        internal C1.Silverlight.Toolbar.C1ToolbarButton UploadListButton;
        
        internal C1.Silverlight.Toolbar.C1ToolbarButton PurgeListButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DashApplication;component/User%20Controls/BorrowsMorningControl.xaml", System.UriKind.Relative));
            this.MainUserControl = ((System.Windows.Controls.UserControl)(this.FindName("MainUserControl")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.BorrowsGrid = ((C1.Silverlight.DataGrid.C1DataGrid)(this.FindName("BorrowsGrid")));
            this.MainToolBarStrip = ((C1.Silverlight.Toolbar.C1ToolbarStrip)(this.FindName("MainToolBarStrip")));
            this.BizDatePicker = ((System.Windows.Controls.DatePicker)(this.FindName("BizDatePicker")));
            this.LoadListButton = ((C1.Silverlight.Toolbar.C1ToolbarButton)(this.FindName("LoadListButton")));
            this.UploadListButton = ((C1.Silverlight.Toolbar.C1ToolbarButton)(this.FindName("UploadListButton")));
            this.PurgeListButton = ((C1.Silverlight.Toolbar.C1ToolbarButton)(this.FindName("PurgeListButton")));
        }
    }
}

