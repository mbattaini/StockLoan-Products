﻿#pragma checksum "C:\_dev\DashApplication2.0\DashApplication\DashApplication\User Controls\PenaltyBoxItemSetControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "70BCADD2E5E087BC267C835E0E4BA001"
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
    
    
    public partial class PenaltyBoxItemSetControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal C1.Silverlight.C1TextBoxBase ProcessIdTextBox;
        
        internal C1.Silverlight.C1TextBoxBase SecIdTextBox;
        
        internal C1.Silverlight.C1TextBoxBase CommentTextBox;
        
        internal C1.Silverlight.Toolbar.C1ToolbarButton SubmitButton;
        
        internal C1.Silverlight.Toolbar.C1ToolbarButton CloseButton;
        
        internal System.Windows.Controls.Label StatusLabel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DashApplication;component/User%20Controls/PenaltyBoxItemSetControl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ProcessIdTextBox = ((C1.Silverlight.C1TextBoxBase)(this.FindName("ProcessIdTextBox")));
            this.SecIdTextBox = ((C1.Silverlight.C1TextBoxBase)(this.FindName("SecIdTextBox")));
            this.CommentTextBox = ((C1.Silverlight.C1TextBoxBase)(this.FindName("CommentTextBox")));
            this.SubmitButton = ((C1.Silverlight.Toolbar.C1ToolbarButton)(this.FindName("SubmitButton")));
            this.CloseButton = ((C1.Silverlight.Toolbar.C1ToolbarButton)(this.FindName("CloseButton")));
            this.StatusLabel = ((System.Windows.Controls.Label)(this.FindName("StatusLabel")));
        }
    }
}

