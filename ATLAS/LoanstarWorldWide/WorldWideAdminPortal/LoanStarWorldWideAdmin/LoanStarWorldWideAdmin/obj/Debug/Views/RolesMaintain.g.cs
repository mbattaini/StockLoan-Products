﻿#pragma checksum "C:\_dev\ATLAS\LoanstarWorldWide\WorldWideAdminPortal\LoanStarWorldWideAdmin\LoanStarWorldWideAdmin\Views\RolesMaintain.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B6AC8EDDDB01EFCC8A6E65A2BAB1C1F7"
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
    
    
    public partial class RolesMaintain : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.UserControl MaintainRoles;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid BackgroundGrid;
        
        internal System.Windows.Controls.Label StatusLabel;
        
        internal System.Windows.Controls.Label UpdateLabel;
        
        internal System.Windows.Controls.Label deleteInstructLabel;
        
        internal System.Windows.Controls.Label bookGroupLabel;
        
        internal C1.Silverlight.C1ComboBox BookGroupCombo;
        
        internal System.Windows.Controls.Button AddNewBookGroupButton;
        
        internal System.Windows.Controls.Label roleLabel;
        
        internal C1.Silverlight.C1ComboBox RoleCombo;
        
        internal System.Windows.Controls.Button AddNewRoleButton;
        
        internal System.Windows.Controls.Label functionLabel;
        
        internal C1.Silverlight.C1ComboBox FunctionCombo;
        
        internal System.Windows.Controls.Button AddNewFunctionButton;
        
        internal System.Windows.Controls.Button AssignButton;
        
        internal C1.Silverlight.DataGrid.C1DataGrid BookGroupRolesGrid;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn bookGroup;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn roleName;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn functionPath;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn roleComment;
        
        internal System.Windows.Controls.Button buttonCancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LoanStarWorldWideAdmin;component/Views/RolesMaintain.xaml", System.UriKind.Relative));
            this.MaintainRoles = ((System.Windows.Controls.UserControl)(this.FindName("MaintainRoles")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.BackgroundGrid = ((System.Windows.Controls.Grid)(this.FindName("BackgroundGrid")));
            this.StatusLabel = ((System.Windows.Controls.Label)(this.FindName("StatusLabel")));
            this.UpdateLabel = ((System.Windows.Controls.Label)(this.FindName("UpdateLabel")));
            this.deleteInstructLabel = ((System.Windows.Controls.Label)(this.FindName("deleteInstructLabel")));
            this.bookGroupLabel = ((System.Windows.Controls.Label)(this.FindName("bookGroupLabel")));
            this.BookGroupCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("BookGroupCombo")));
            this.AddNewBookGroupButton = ((System.Windows.Controls.Button)(this.FindName("AddNewBookGroupButton")));
            this.roleLabel = ((System.Windows.Controls.Label)(this.FindName("roleLabel")));
            this.RoleCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("RoleCombo")));
            this.AddNewRoleButton = ((System.Windows.Controls.Button)(this.FindName("AddNewRoleButton")));
            this.functionLabel = ((System.Windows.Controls.Label)(this.FindName("functionLabel")));
            this.FunctionCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("FunctionCombo")));
            this.AddNewFunctionButton = ((System.Windows.Controls.Button)(this.FindName("AddNewFunctionButton")));
            this.AssignButton = ((System.Windows.Controls.Button)(this.FindName("AssignButton")));
            this.BookGroupRolesGrid = ((C1.Silverlight.DataGrid.C1DataGrid)(this.FindName("BookGroupRolesGrid")));
            this.bookGroup = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("bookGroup")));
            this.roleName = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("roleName")));
            this.functionPath = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("functionPath")));
            this.roleComment = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("roleComment")));
            this.buttonCancel = ((System.Windows.Controls.Button)(this.FindName("buttonCancel")));
        }
    }
}

