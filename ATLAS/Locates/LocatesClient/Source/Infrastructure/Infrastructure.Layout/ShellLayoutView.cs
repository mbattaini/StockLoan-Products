using System;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using StockLoan.Locates.Infrastructure.Interface.Constants;

namespace StockLoan.Locates.Infrastructure.Layout
{
    public partial class ShellLayoutView : UserControl
    {
        private ShellLayoutViewPresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShellLayoutView"/> class.
        /// </summary>
        public ShellLayoutView()
        {
            InitializeComponent();
            _leftWorkspace.Name = WorkspaceNames.LeftWorkspace;
            _rightWorkspace.Name = WorkspaceNames.RightWorkspace;
        }

        /// <summary>
        /// Sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        [CreateNew]
        public ShellLayoutViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        /// <summary>
        /// Gets the main menu strip.
        /// </summary>
        /// <value>The main menu strip.</value>
        internal MenuStrip MainMenuStrip
        {
            get { return _mainMenuStrip; }
        }

        /// <summary>
        /// Gets the main status strip.
        /// </summary>
        /// <value>The main status strip.</value>
        internal StatusStrip MainStatusStrip
        {
            get { return _mainStatusStrip; }
        }

        /// <summary>
        /// Gets the main toolbar strip.
        /// </summary>
        /// <value>The main toolbar strip.</value>
        internal ToolStrip MainToolbarStrip
        {
            get { return _mainToolStrip; }
        }

        /// <summary>
        /// Close the application.
        /// </summary>
        private void OnFileExit(object sender, EventArgs e)
        {
            _presenter.OnFileExit();
        }

        /// <summary>
        /// Sets the status label.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SetStatusLabel(string text)
        {
            _statusLabel.Text = text;
        }

    }
}
