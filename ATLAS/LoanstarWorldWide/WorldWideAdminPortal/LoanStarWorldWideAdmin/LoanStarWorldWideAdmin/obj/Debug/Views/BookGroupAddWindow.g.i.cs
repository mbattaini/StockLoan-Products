﻿#pragma checksum "C:\_dev\ATLAS\LoanstarWorldWide\WorldWideAdminPortal\LoanStarWorldWideAdmin\LoanStarWorldWideAdmin\Views\BookGroupAddWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2789054DDBAFA3055BAEC32CA2F9D418"
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
    
    
    public partial class BookGroupAddWindow : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.UserControl AddBookGroupWindow;
        
        internal System.Windows.Controls.StackPanel LayoutRoot;
        
        internal System.Windows.Controls.Grid BookGroupGrid;
        
        internal System.Windows.Controls.Label NameLabel;
        
        internal System.Windows.Controls.TextBox NameTextBox;
        
        internal System.Windows.Controls.Label bookLabel;
        
        internal C1.Silverlight.C1ComboBox BookCombo;
        
        internal System.Windows.Controls.Label timeZoneLabel;
        
        internal C1.Silverlight.C1ComboBox TimeZoneCombo;
        
        internal System.Windows.Controls.CheckBox UseWeekendsCheckbox;
        
        internal System.Windows.Controls.Label settlementTypeLabel;
        
        internal System.Windows.Controls.TextBox SettlementTypeTextBox;
        
        internal System.Windows.Controls.Button CancelButton;
        
        internal System.Windows.Controls.Button BookGroupAddButton;
        
        internal System.Windows.Controls.Label statusLabel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LoanStarWorldWideAdmin;component/Views/BookGroupAddWindow.xaml", System.UriKind.Relative));
            this.AddBookGroupWindow = ((System.Windows.Controls.UserControl)(this.FindName("AddBookGroupWindow")));
            this.LayoutRoot = ((System.Windows.Controls.StackPanel)(this.FindName("LayoutRoot")));
            this.BookGroupGrid = ((System.Windows.Controls.Grid)(this.FindName("BookGroupGrid")));
            this.NameLabel = ((System.Windows.Controls.Label)(this.FindName("NameLabel")));
            this.NameTextBox = ((System.Windows.Controls.TextBox)(this.FindName("NameTextBox")));
            this.bookLabel = ((System.Windows.Controls.Label)(this.FindName("bookLabel")));
            this.BookCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("BookCombo")));
            this.timeZoneLabel = ((System.Windows.Controls.Label)(this.FindName("timeZoneLabel")));
            this.TimeZoneCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("TimeZoneCombo")));
            this.UseWeekendsCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("UseWeekendsCheckbox")));
            this.settlementTypeLabel = ((System.Windows.Controls.Label)(this.FindName("settlementTypeLabel")));
            this.SettlementTypeTextBox = ((System.Windows.Controls.TextBox)(this.FindName("SettlementTypeTextBox")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
            this.BookGroupAddButton = ((System.Windows.Controls.Button)(this.FindName("BookGroupAddButton")));
            this.statusLabel = ((System.Windows.Controls.Label)(this.FindName("statusLabel")));
        }
    }
}
