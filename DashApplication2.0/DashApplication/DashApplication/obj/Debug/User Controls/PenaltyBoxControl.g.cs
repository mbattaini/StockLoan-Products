﻿#pragma checksum "C:\_dev\DashApplication2.0\DashApplication\DashApplication\User Controls\PenaltyBoxControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5D5C5857E9687C732DFE9D9CA2D0DBE7"
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
    
    
    public partial class PenaltyBoxControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal C1.Silverlight.C1TextBoxBase SecIdTextBox;
        
        internal C1.Silverlight.C1TextBoxBase CommentTextBox;
        
        internal System.Windows.Controls.Button ButtonAdd;
        
        internal System.Windows.Controls.CheckBox CheckBoxShowHistory;
        
        internal C1.Silverlight.DataGrid.C1DataGrid PenaltyBoxGrid;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn ProcessId;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn SecId;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn Symbol;
        
        internal C1.Silverlight.DataGrid.DataGridDateTimeColumn StartDate;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn StartDateActUserId;
        
        internal C1.Silverlight.DataGrid.DataGridDateTimeColumn EndDate;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn EndDateActUserId;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn Comment;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DashApplication;component/User%20Controls/PenaltyBoxControl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.SecIdTextBox = ((C1.Silverlight.C1TextBoxBase)(this.FindName("SecIdTextBox")));
            this.CommentTextBox = ((C1.Silverlight.C1TextBoxBase)(this.FindName("CommentTextBox")));
            this.ButtonAdd = ((System.Windows.Controls.Button)(this.FindName("ButtonAdd")));
            this.CheckBoxShowHistory = ((System.Windows.Controls.CheckBox)(this.FindName("CheckBoxShowHistory")));
            this.PenaltyBoxGrid = ((C1.Silverlight.DataGrid.C1DataGrid)(this.FindName("PenaltyBoxGrid")));
            this.ProcessId = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("ProcessId")));
            this.SecId = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("SecId")));
            this.Symbol = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("Symbol")));
            this.StartDate = ((C1.Silverlight.DataGrid.DataGridDateTimeColumn)(this.FindName("StartDate")));
            this.StartDateActUserId = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("StartDateActUserId")));
            this.EndDate = ((C1.Silverlight.DataGrid.DataGridDateTimeColumn)(this.FindName("EndDate")));
            this.EndDateActUserId = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("EndDateActUserId")));
            this.Comment = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("Comment")));
        }
    }
}
