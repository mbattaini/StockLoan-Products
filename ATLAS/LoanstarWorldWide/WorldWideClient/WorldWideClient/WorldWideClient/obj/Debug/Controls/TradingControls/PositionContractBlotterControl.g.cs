﻿#pragma checksum "C:\_dev\ATLAS\LoanstarWorldWide\WorldWideClient\WorldWideClient\WorldWideClient\Controls\TradingControls\PositionContractBlotterControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "272B9DFC631606BDE454429007E1442B"
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
using WorldWideClient;


namespace WorldWideClient {
    
    
    public partial class PositionContractBlotterControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal C1.Silverlight.DataGrid.C1DataGrid DealGrid;
        
        internal WorldWideClient.BookGroupToolBar contractBlotterBookGroupToolBar;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WorldWideClient;component/Controls/TradingControls/PositionContractBlotterContro" +
                        "l.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.DealGrid = ((C1.Silverlight.DataGrid.C1DataGrid)(this.FindName("DealGrid")));
            this.contractBlotterBookGroupToolBar = ((WorldWideClient.BookGroupToolBar)(this.FindName("contractBlotterBookGroupToolBar")));
        }
    }
}

