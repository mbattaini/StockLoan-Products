﻿//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The SmartClientApplication class provides a placeholder to add global services and builder strategies
// for the application.
// 
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/02-08-050-Create_Smart_Client_Application_Next_Steps.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.WinForms;
using StockLoan.Locates.Infrastructure.Interface.Services;
using StockLoan.Locates.Infrastructure.Library.Services;

namespace StockLoan.Locates.Infrastructure.Library
{
    public abstract class SmartClientApplication<TWorkItem, TShell> : FormShellApplication<TWorkItem, TShell>
        where TWorkItem : WorkItem, new()
        where TShell : Form
        {


        public SmartClientApplication()
        {
        }


        protected override void AddServices()
        {
            base.AddServices();

            RootWorkItem.Services.AddNew<ProfileCatalogModuleInfoStore, IModuleInfoStore>();
            RootWorkItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
            RootWorkItem.Services.Remove<IModuleEnumerator>();
            RootWorkItem.Services.Remove<IModuleLoaderService>();
            RootWorkItem.Services.AddNew<XmlStreamDependentModuleEnumerator, IModuleEnumerator>();
            RootWorkItem.Services.AddNew<DependentModuleLoaderService, IModuleLoaderService>();
            RootWorkItem.Services.AddOnDemand<ActionCatalogService, IActionCatalogService>();
            RootWorkItem.Services.AddOnDemand<EntityTranslatorService, IEntityTranslatorService>();
        }

        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();
        }




    }
}
