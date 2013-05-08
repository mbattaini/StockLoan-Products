using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Atlas_Schedule
{
	public partial class AtlasScheduleForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		public AtlasScheduleForm()
		{
			InitializeComponent();
		
			
		}

		private void TasksGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{

		}

		private DataSet TasksGet()
		{

		}
	}
}