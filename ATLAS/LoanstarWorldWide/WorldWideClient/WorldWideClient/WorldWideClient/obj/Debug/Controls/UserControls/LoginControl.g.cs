﻿#pragma checksum "C:\_dev\ATLAS\LoanstarWorldWide\WorldWideClient\WorldWideClient\WorldWideClient\Controls\UserControls\LoginControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AEAC5DC41F51534943BAF384EC802FE1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
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


namespace WorldWideClient {
    
    
    public partial class LoginControl : System.Windows.Controls.UserControl {
        
        internal C1.Silverlight.C1TextBoxBase UserIdTextBox;
        
        internal System.Windows.Controls.Button LoginButton;
        
        internal System.Windows.Controls.Label StatusLabel;
        
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        internal System.Windows.Controls.Label StatusLabel_Copy;
        
        internal System.Windows.Controls.Label StatusLabel_Copy1;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WorldWideClient;component/Controls/UserControls/LoginControl.xaml", System.UriKind.Relative));
            this.UserIdTextBox = ((C1.Silverlight.C1TextBoxBase)(this.FindName("UserIdTextBox")));
            this.LoginButton = ((System.Windows.Controls.Button)(this.FindName("LoginButton")));
            this.StatusLabel = ((System.Windows.Controls.Label)(this.FindName("StatusLabel")));
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(this.FindName("PasswordTextBox")));
            this.StatusLabel_Copy = ((System.Windows.Controls.Label)(this.FindName("StatusLabel_Copy")));
            this.StatusLabel_Copy1 = ((System.Windows.Controls.Label)(this.FindName("StatusLabel_Copy1")));
        }
    }
}

