﻿#pragma checksum "C:\_dev\Locates\StockLoan.LocatesClient\StockLoan.LocatesClient\User Controls\LocatesMessageListControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "06A40612D81BB8B32C0EE845AD4A7B1F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17379
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


namespace StockLoan_LocatesClient {
    
    
    public partial class LocatesMessageListControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ListBox MessageListBox;
        
        internal C1.Silverlight.Toolbar.C1ToolbarStrip Tool;
        
        internal C1.Silverlight.Toolbar.C1ToolbarButton SubmitMessageButton;
        
        internal C1.Silverlight.Toolbar.C1ToolbarButton LoadMessageButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/StockLoan.LocatesClient;component/User%20Controls/LocatesMessageListControl.xaml" +
                        "", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MessageListBox = ((System.Windows.Controls.ListBox)(this.FindName("MessageListBox")));
            this.Tool = ((C1.Silverlight.Toolbar.C1ToolbarStrip)(this.FindName("Tool")));
            this.SubmitMessageButton = ((C1.Silverlight.Toolbar.C1ToolbarButton)(this.FindName("SubmitMessageButton")));
            this.LoadMessageButton = ((C1.Silverlight.Toolbar.C1ToolbarButton)(this.FindName("LoadMessageButton")));
        }
    }
}

