﻿#pragma checksum "C:\_dev\ATLAS\LoanstarWorldWide\WorldWideAdminPortal\LoanStarWorldWideAdmin\LoanStarWorldWideAdmin\Views\CountryCurrencyMaintain.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "89C42FE6062E21FC9035A9FEAA0C6EAE"
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
    
    
    public partial class CountryCurrencyMaintain : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Page CountryCurrencyMaintenance;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Label UpdateLabel;
        
        internal System.Windows.Controls.Label StatusLabel;
        
        internal System.Windows.Controls.StackPanel CheckBoxStackPanel;
        
        internal System.Windows.Controls.CheckBox CountryCheckBox;
        
        internal System.Windows.Controls.CheckBox CurrencyCheckBox;
        
        internal System.Windows.Controls.CheckBox ConversionCheckBox;
        
        internal System.Windows.Controls.CheckBox AllCheckBox;
        
        internal System.Windows.Controls.StackPanel CountryMainStackPanel;
        
        internal System.Windows.Controls.StackPanel CountryHeaderStackPanel;
        
        internal System.Windows.Controls.Label CountryHeadLabel;
        
        internal C1.Silverlight.DataGrid.C1DataGrid CountryGrid;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn CountryCode;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn Country;
        
        internal System.Windows.Controls.StackPanel stackPanel1;
        
        internal System.Windows.Controls.Label CountryCodeLabel;
        
        internal System.Windows.Controls.TextBox CountryCodeTextBox;
        
        internal System.Windows.Controls.Label NameLabel;
        
        internal System.Windows.Controls.TextBox CountryNameTextBox;
        
        internal System.Windows.Controls.Label SettleDaysLabel;
        
        internal System.Windows.Controls.TextBox CountrySettleDaysTextBox;
        
        internal System.Windows.Controls.CheckBox ActiveCheckBox;
        
        internal System.Windows.Controls.StackPanel CountryButtonsStackPanel;
        
        internal System.Windows.Controls.Button CountryCancelButton;
        
        internal System.Windows.Controls.Button CountrySaveButton;
        
        internal System.Windows.Controls.StackPanel CurrencyMainStackPanel;
        
        internal System.Windows.Controls.StackPanel CurrencyHeaderStackPanel;
        
        internal System.Windows.Controls.Label CurrencyHeadLabel;
        
        internal C1.Silverlight.DataGrid.C1DataGrid CurrencyGrid;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn CurrencyCode;
        
        internal C1.Silverlight.DataGrid.DataGridBoundColumn Currency;
        
        internal System.Windows.Controls.StackPanel CurrencyStackPanel;
        
        internal System.Windows.Controls.Label CurrencyCodeLabel;
        
        internal System.Windows.Controls.TextBox CurrencyCodeTextBox;
        
        internal System.Windows.Controls.Label CurrencyLabel;
        
        internal System.Windows.Controls.TextBox CurrencyTextBox;
        
        internal System.Windows.Controls.CheckBox CurrencyActiveCheckBox;
        
        internal System.Windows.Controls.StackPanel CurrencyButtonsStackPanel;
        
        internal System.Windows.Controls.Button CurrencyCancelButton;
        
        internal System.Windows.Controls.Button CurrencySaveButton;
        
        internal System.Windows.Controls.StackPanel CurrencyConversionMainStackPanel;
        
        internal System.Windows.Controls.StackPanel CurrencyConversionHeaderStackPanel;
        
        internal System.Windows.Controls.Label CurrencyConversionHeadLabel;
        
        internal System.Windows.Controls.StackPanel CurrencyConversionStackPanel;
        
        internal System.Windows.Controls.Label CurrencyFromLabel;
        
        internal C1.Silverlight.C1ComboBox IsoFromCombo;
        
        internal System.Windows.Controls.Label CurrencyToLabel;
        
        internal C1.Silverlight.C1ComboBox IsoToCombo;
        
        internal System.Windows.Controls.Label ConversionRateLabel;
        
        internal System.Windows.Controls.TextBox ConversionRateTextBox;
        
        internal System.Windows.Controls.StackPanel CurrencyConvertButtonsStackPanel;
        
        internal System.Windows.Controls.Button CurrencyConversionCancelButton;
        
        internal System.Windows.Controls.Button CurrencyConversionSaveButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LoanStarWorldWideAdmin;component/Views/CountryCurrencyMaintain.xaml", System.UriKind.Relative));
            this.CountryCurrencyMaintenance = ((System.Windows.Controls.Page)(this.FindName("CountryCurrencyMaintenance")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.UpdateLabel = ((System.Windows.Controls.Label)(this.FindName("UpdateLabel")));
            this.StatusLabel = ((System.Windows.Controls.Label)(this.FindName("StatusLabel")));
            this.CheckBoxStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CheckBoxStackPanel")));
            this.CountryCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("CountryCheckBox")));
            this.CurrencyCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("CurrencyCheckBox")));
            this.ConversionCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("ConversionCheckBox")));
            this.AllCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("AllCheckBox")));
            this.CountryMainStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CountryMainStackPanel")));
            this.CountryHeaderStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CountryHeaderStackPanel")));
            this.CountryHeadLabel = ((System.Windows.Controls.Label)(this.FindName("CountryHeadLabel")));
            this.CountryGrid = ((C1.Silverlight.DataGrid.C1DataGrid)(this.FindName("CountryGrid")));
            this.CountryCode = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("CountryCode")));
            this.Country = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("Country")));
            this.stackPanel1 = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel1")));
            this.CountryCodeLabel = ((System.Windows.Controls.Label)(this.FindName("CountryCodeLabel")));
            this.CountryCodeTextBox = ((System.Windows.Controls.TextBox)(this.FindName("CountryCodeTextBox")));
            this.NameLabel = ((System.Windows.Controls.Label)(this.FindName("NameLabel")));
            this.CountryNameTextBox = ((System.Windows.Controls.TextBox)(this.FindName("CountryNameTextBox")));
            this.SettleDaysLabel = ((System.Windows.Controls.Label)(this.FindName("SettleDaysLabel")));
            this.CountrySettleDaysTextBox = ((System.Windows.Controls.TextBox)(this.FindName("CountrySettleDaysTextBox")));
            this.ActiveCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("ActiveCheckBox")));
            this.CountryButtonsStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CountryButtonsStackPanel")));
            this.CountryCancelButton = ((System.Windows.Controls.Button)(this.FindName("CountryCancelButton")));
            this.CountrySaveButton = ((System.Windows.Controls.Button)(this.FindName("CountrySaveButton")));
            this.CurrencyMainStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyMainStackPanel")));
            this.CurrencyHeaderStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyHeaderStackPanel")));
            this.CurrencyHeadLabel = ((System.Windows.Controls.Label)(this.FindName("CurrencyHeadLabel")));
            this.CurrencyGrid = ((C1.Silverlight.DataGrid.C1DataGrid)(this.FindName("CurrencyGrid")));
            this.CurrencyCode = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("CurrencyCode")));
            this.Currency = ((C1.Silverlight.DataGrid.DataGridBoundColumn)(this.FindName("Currency")));
            this.CurrencyStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyStackPanel")));
            this.CurrencyCodeLabel = ((System.Windows.Controls.Label)(this.FindName("CurrencyCodeLabel")));
            this.CurrencyCodeTextBox = ((System.Windows.Controls.TextBox)(this.FindName("CurrencyCodeTextBox")));
            this.CurrencyLabel = ((System.Windows.Controls.Label)(this.FindName("CurrencyLabel")));
            this.CurrencyTextBox = ((System.Windows.Controls.TextBox)(this.FindName("CurrencyTextBox")));
            this.CurrencyActiveCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("CurrencyActiveCheckBox")));
            this.CurrencyButtonsStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyButtonsStackPanel")));
            this.CurrencyCancelButton = ((System.Windows.Controls.Button)(this.FindName("CurrencyCancelButton")));
            this.CurrencySaveButton = ((System.Windows.Controls.Button)(this.FindName("CurrencySaveButton")));
            this.CurrencyConversionMainStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyConversionMainStackPanel")));
            this.CurrencyConversionHeaderStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyConversionHeaderStackPanel")));
            this.CurrencyConversionHeadLabel = ((System.Windows.Controls.Label)(this.FindName("CurrencyConversionHeadLabel")));
            this.CurrencyConversionStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyConversionStackPanel")));
            this.CurrencyFromLabel = ((System.Windows.Controls.Label)(this.FindName("CurrencyFromLabel")));
            this.IsoFromCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("IsoFromCombo")));
            this.CurrencyToLabel = ((System.Windows.Controls.Label)(this.FindName("CurrencyToLabel")));
            this.IsoToCombo = ((C1.Silverlight.C1ComboBox)(this.FindName("IsoToCombo")));
            this.ConversionRateLabel = ((System.Windows.Controls.Label)(this.FindName("ConversionRateLabel")));
            this.ConversionRateTextBox = ((System.Windows.Controls.TextBox)(this.FindName("ConversionRateTextBox")));
            this.CurrencyConvertButtonsStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("CurrencyConvertButtonsStackPanel")));
            this.CurrencyConversionCancelButton = ((System.Windows.Controls.Button)(this.FindName("CurrencyConversionCancelButton")));
            this.CurrencyConversionSaveButton = ((System.Windows.Controls.Button)(this.FindName("CurrencyConversionSaveButton")));
        }
    }
}
