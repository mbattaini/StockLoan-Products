//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The FormShell class represent the main form of your application.
// 
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/03-01-010-How_to_Create_Smart_Client_Solutions.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System.Windows.Forms;

using Microsoft.Practices.CompositeUI.EventBroker;

using StockLoan.Locates.Infrastructure.Interface;
using StockLoan.Locates.Infrastructure.Interface.Constants;


namespace StockLoan.Locates.Infrastructure.Shell
{
    /// <summary>
    /// Main application shell view.
    /// </summary>
    public partial class ShellForm : Form
    {
        [EventPublication(EventTopicNames.ShellFormDeactivate, PublicationScope.Global)]
        public event System.EventHandler<ShellFormEventArgs> ShellFormDeactivate;
    
        [EventPublication(EventTopicNames.ShellFormClosing, PublicationScope.Global)]
        public event System.EventHandler<ShellFormEventArgs> ShellFormClosing;
    
        [EventPublication(EventTopicNames.ShellFormResize, PublicationScope.Global)]
        public event System.EventHandler<ShellFormEventArgs> ShellFormResize;



        /// <summary>
        /// Default class initializer.
        /// </summary>
        public ShellForm()
        {
            InitializeComponent();

            _layoutWorkspace.Name = WorkspaceNames.LayoutWorkspace;
        }


        // WinForm Local Event: Resize
        private void ShellForm_Resize(object sender, System.EventArgs e)
        {
            ShellFormEventArgs argsFormEvent = GetFormEventArgs();
            OnShellFormResize(argsFormEvent);
        }
        // CAB Global Event
        protected virtual void OnShellFormResize(ShellFormEventArgs eventArgs)
        {
            if (ShellFormResize != null)
            {
                ShellFormResize(this, eventArgs);
            }
        }


        // WinForm Local Event: FormClosing
        private void ShellForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShellFormEventArgs argsFormEvent = GetFormEventArgs();
            OnShellFormClosing(argsFormEvent);
        }
        // CAB Global Event
        protected virtual void OnShellFormClosing(ShellFormEventArgs eventArgs)
        {
            if (ShellFormClosing != null)
            {
                ShellFormClosing(this, eventArgs);
            }
        }


        // WinForm Local Event: Deactivate
        private void ShellForm_Deactivate(object sender, System.EventArgs e)
        {
            ShellFormEventArgs argsFormEvent = GetFormEventArgs();
            OnShellFormDeactivate(argsFormEvent);
        }
        // CAB Global Event
        protected virtual void OnShellFormDeactivate(ShellFormEventArgs eventArgs)
        {
            if (ShellFormDeactivate != null)
            {
                ShellFormDeactivate(this, eventArgs);
            }
        }


        private ShellFormEventArgs GetFormEventArgs()
        {
            ShellFormEventArgs argsFormEvent = new ShellFormEventArgs();

            argsFormEvent.WindowState = this.WindowState;
            argsFormEvent.Bounds = this.Bounds;

            return argsFormEvent;
        }

        
    }




}

