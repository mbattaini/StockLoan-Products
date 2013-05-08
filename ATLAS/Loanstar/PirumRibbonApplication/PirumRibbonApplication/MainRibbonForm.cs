using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PirumRibbonApplication.Properties;
using C1.Win.C1Ribbon;

namespace PirumRibbonApplication
{
    public partial class MainRibbonForm : C1RibbonForm
    {
        public MainRibbonForm()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainRibbonForm_Load(object sender, EventArgs e)
        {
            switch (c1Ribbon1.VisualStyle)
            {
                case VisualStyle.Office2007Blue:
                    blue2007Button.Pressed = true;
                    break;
                case VisualStyle.Office2007Silver:
                    silver2007Button.Pressed = true;
                    break;
                case VisualStyle.Office2007Black:
                    black2007Button.Pressed = true;
                    break;
                case VisualStyle.Office2010Blue:
                    blue2010Button.Pressed = true;
                    break;
                case VisualStyle.Office2010Silver:
                    silver2010Button.Pressed = true;
                    break;
                case VisualStyle.Office2010Black:
                    black2010Button.Pressed = true;
                    break;
                case VisualStyle.Windows7:
                    windows7Button.Pressed = true;
                    break;
            }
        }

        private void visualStyle_PressedButtonChanged(object sender, EventArgs e)
        {
            if (blue2007Button.Pressed)
                c1Ribbon1.VisualStyle = VisualStyle.Office2007Blue;
            else if (silver2007Button.Pressed)
                c1Ribbon1.VisualStyle = VisualStyle.Office2007Silver;
            else if (black2007Button.Pressed)
                c1Ribbon1.VisualStyle = VisualStyle.Office2007Black;
            else if (blue2010Button.Pressed)
                c1Ribbon1.VisualStyle = VisualStyle.Office2010Blue;
            else if (silver2010Button.Pressed)
                c1Ribbon1.VisualStyle = VisualStyle.Office2010Silver;
            else if (black2010Button.Pressed)
                c1Ribbon1.VisualStyle = VisualStyle.Office2010Black;
            else if (windows7Button.Pressed)
                c1Ribbon1.VisualStyle = VisualStyle.Windows7;
        }

        private void c1Ribbon1_MinimizedChanged(object sender, EventArgs e)
        {
            minimizeRibbonToggleButton.Pressed = c1Ribbon1.Minimized;
        }

        private void minimizeRibbonToggleButton_Click(object sender, EventArgs e)
        {
            c1Ribbon1.Minimized = minimizeRibbonToggleButton.Pressed;
        }

        private void rcListPinButton_Click(object sender, EventArgs e)
        {
            RibbonToggleButton pin = (RibbonToggleButton)sender;
            if (pin.Pressed)
                pin.SmallImage = Resources.Pinned;
            else
                pin.SmallImage = Resources.Unpinned;
        }

        #region MDI Window List

        private bool _windowSwitching;

        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);
            if (!_windowSwitching)
            {
                RefreshMdiWindowList();
            }
        }

        private void newWindowButton_Click(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            ChildRibbonForm crf = new ChildRibbonForm();
            crf.MdiParent = this;
            crf.Text = String.Format("MDI Child Form {0:d}", MdiChildren.Length);
            crf.Show();
        }

        private void switchWindowsMenu_Click(object sender, EventArgs e)
        {
            RibbonToggleButton tb = null;
            if (switchWindowsMenu.Items.Count > 0)
            {
                tb = ((RibbonToggleButton)switchWindowsMenu.Items[0]).PressedButton;
            }
            if (tb != null)
            {
                _windowSwitching = true;
                ((Form)tb.Tag).Activate();
                _windowSwitching = false;
            }
        }

        private void RefreshMdiWindowList()
        {
            RibbonItemCollection items = switchWindowsMenu.Items;
            for (int i = items.Count - 1; i >= 0; i--)
                items[i].Dispose();
            items.Clear();
            Form[] forms = MdiChildren;
            Form activeChild = ActiveMdiChild;
            for (int i = 0; i < forms.Length; i++)
            {
                ChildRibbonForm f = forms[i] as ChildRibbonForm;
                if (f != null && !f.BeingDisposed)
                {
                    RibbonToggleButton tb = new RibbonToggleButton();
                    tb.Click += new EventHandler(switchWindowsMenu_Click);
                    tb.CanDepress = false;
                    tb.Pressed = object.ReferenceEquals(f, activeChild);
                    tb.Text = f.Text;
                    tb.Tag = f;
                    tb.ToggleGroupName = "switchWindows";
                    items.Add(tb);
                }
            }
            switchWindowsMenu.Enabled = items.Count > 0;
        }

        #endregion
    }
}
