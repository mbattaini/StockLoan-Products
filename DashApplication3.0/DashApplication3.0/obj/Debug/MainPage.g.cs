﻿#pragma checksum "C:\_dev\DashApplication3.0\DashApplication3.0\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1A4A63BFB98126E2B1A02F0CF5B83D4C"
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
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup VisualStateGroup;
        
        internal System.Windows.VisualState NavigatingState;
        
        internal System.Windows.VisualState NavigatedState;
        
        internal System.Windows.Controls.Border ContentBorder;
        
        internal System.Windows.Controls.Frame ContentFrame;
        
        internal System.Windows.Controls.Grid NavigationGrid;
        
        internal System.Windows.Controls.Border LinksBorder;
        
        internal System.Windows.Controls.StackPanel LinksStackPanel;
        
        internal C1.Silverlight.Imaging.C1Image ImageApp;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DashApplication;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.VisualStateGroup = ((System.Windows.VisualStateGroup)(this.FindName("VisualStateGroup")));
            this.NavigatingState = ((System.Windows.VisualState)(this.FindName("NavigatingState")));
            this.NavigatedState = ((System.Windows.VisualState)(this.FindName("NavigatedState")));
            this.ContentBorder = ((System.Windows.Controls.Border)(this.FindName("ContentBorder")));
            this.ContentFrame = ((System.Windows.Controls.Frame)(this.FindName("ContentFrame")));
            this.NavigationGrid = ((System.Windows.Controls.Grid)(this.FindName("NavigationGrid")));
            this.LinksBorder = ((System.Windows.Controls.Border)(this.FindName("LinksBorder")));
            this.LinksStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("LinksStackPanel")));
            this.ImageApp = ((C1.Silverlight.Imaging.C1Image)(this.FindName("ImageApp")));
        }
    }
}
