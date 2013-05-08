using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace CentralClient
{
    public partial class SecurityMasterForm : System.Windows.Forms.Form
    {
        private const int xBOXPOSITION_SHOW_HEIGHT = 104;
        private const int xBOXPOSITION_HIDE_HEIGHT = 41;
        private string dockingEdgeString = "";

        private MainForm mainForm;
        
        public SecurityMasterForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            MainSecMaster.LoanstarMainForm = mainForm;
        }

        private void SecurityMasterForm_Load(object sender, EventArgs e)
        {      
            try
            {
                bool showBoxPosition = false;

                int screenIndex = 0;
                string deviceName = RegistryValue.Read(this.Name, "DeviceName", ShellAppBar.DockingScreen.DeviceName);

                dockingEdgeString = "Top";
                dockingEdgeString = RegistryValue.Read(this.Name, "DockingEdge", dockingEdgeString);

                showBoxPosition = bool.Parse(RegistryValue.Read(this.Name, "ShowBoxPosition", false.ToString()));
                
                Screen[] screens = Screen.AllScreens;
                char[] remove = { '\\', '.', '\0' };

                deviceName = deviceName.Trim(remove).Substring(0, 8);

                for (int index = 0; index < screens.Length; index++)
                {
                    if (screens[index].DeviceName.Trim(remove).Substring(0, 8).Equals(deviceName))
                    {
                        screenIndex = index;
                        break;
                    }
                }

                Object dockingEdgeObj = null;

                switch (dockingEdgeString)
                {
                    case "Top":
                        dockingEdgeObj = LogicNP.ShellObjects.DockingEdges.Top;
                        break;

                    case "Bottom":
                        dockingEdgeObj = LogicNP.ShellObjects.DockingEdges.Bottom;
                        break;

                    default:
                        dockingEdgeObj = LogicNP.ShellObjects.DockingEdges.Top;
                        break;
                }

                if (showBoxPosition)
                {
                    this.Size = new Size(this.Width, xBOXPOSITION_SHOW_HEIGHT);
                    MainSecMaster.ShowBox = true;
                }
                else
                {
                    this.Size = new Size(this.Width, xBOXPOSITION_HIDE_HEIGHT);
                    MainSecMaster.ShowBox = false;
                }
                
                ShellAppBar.SetDockingEdgeAndScreen((LogicNP.ShellObjects.DockingEdges)dockingEdgeObj, screens[screenIndex]);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void BoxPositionButton_Click(object sender, EventArgs e)
        {
            if (this.Size.Height == xBOXPOSITION_HIDE_HEIGHT)
            {
//                BoxPositionButton.ImageIndex = 1;
                this.Size = new Size(this.Width, xBOXPOSITION_SHOW_HEIGHT);
                MainSecMaster.ShowBox = true;
            }
            else if (this.Size.Height == xBOXPOSITION_SHOW_HEIGHT)
            {
  //              BoxPositionButton.ImageIndex = 0;                
                this.Size = new Size(this.Width, xBOXPOSITION_HIDE_HEIGHT);
                MainSecMaster.ShowBox = false;
            }
        }

        private void SecurityMasterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string dockingEdgeString = "";

            switch (ShellAppBar.DockingEdge)
            {
                case LogicNP.ShellObjects.DockingEdges.Top:
                    dockingEdgeString = "Top";
                    break;

                case LogicNP.ShellObjects.DockingEdges.Bottom:
                    dockingEdgeString = "Bottom";
                    break;

                default:
                    dockingEdgeString = "Top";
                    break;
            }

            if (this.Size.Height == xBOXPOSITION_HIDE_HEIGHT)
            {
                RegistryValue.Write(this.Name, "ShowBoxPosition", false.ToString());
            }
            else if (this.Size.Height == xBOXPOSITION_SHOW_HEIGHT)
            {
                RegistryValue.Write(this.Name, "ShowBoxPosition", true.ToString());
            }
            
            RegistryValue.Write(this.Name, "DeviceName", ShellAppBar.DockingScreen.DeviceName);
            RegistryValue.Write(this.Name, "DockingEdge", dockingEdgeString);
        }

      private void SecurityMasterForm_FormClosed(object sender, FormClosedEventArgs e)
      {
        string dockingEdgeString = "";

        switch (ShellAppBar.DockingEdge)
        {
          case LogicNP.ShellObjects.DockingEdges.Top:
            dockingEdgeString = "Top";
            break;

          case LogicNP.ShellObjects.DockingEdges.Bottom:
            dockingEdgeString = "Bottom";
            break;

          default:
            dockingEdgeString = "Top";
            break;
        }

        if (this.Size.Height == xBOXPOSITION_HIDE_HEIGHT)
        {
          RegistryValue.Write(this.Name, "ShowBoxPosition", false.ToString());
        }
        else if (this.Size.Height == xBOXPOSITION_SHOW_HEIGHT)
        {
          RegistryValue.Write(this.Name, "ShowBoxPosition", true.ToString());
        }

        RegistryValue.Write(this.Name, "DeviceName", ShellAppBar.DockingScreen.DeviceName);
        RegistryValue.Write(this.Name, "DockingEdge", dockingEdgeString);
      }

		public string SecId
		{
			set
			{
				MainSecMaster.SecId = value;
			}

			get
			{
				return MainSecMaster.SecId;
			}
		}

		public string Isin
		{
			get
			{
				return MainSecMaster.Isin;
			}
		}

		public string Sedol
		{
			get
			{
				return MainSecMaster.Sedol;
			}
		}

		public string Symbol
		{
			get
			{
				return MainSecMaster.Symbol;
			}
		}
    }
}