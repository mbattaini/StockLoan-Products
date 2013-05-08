using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;
using ShortSaleLocatesClient.ServiceLocate;

namespace ShortSaleLocatesClient
{
	public partial class ResearchControl : UserControl
	{
		private DataSet dsOperators;
        private LocateServiceClient locateServiceClient;

		public ResearchControl()
		{
			// Required to initialize variables
			InitializeComponent();

            this.locateServiceClient = locateServiceClient;

            // initialize operator dataset
            dsOperators  = new DataSet();
			dsOperators.Tables.Add("Operators");
			dsOperators.Tables["Operators"].Columns.Add("Operator");
			dsOperators.AcceptChanges();

            DataRow drNew = dsOperators.Tables["Operators"].NewRow();
            
            drNew["Operator"] = ">";
            dsOperators.Tables["Operators"].Rows.Add(drNew);
            
         /*   drNew["Operator"] = "<";
            dsOperators.Tables["Operators"].Rows.Add(drNew);

            drNew["Operator"] = "=";
            dsOperators.Tables["Operators"].Rows.Add(drNew);*/

            dsOperators.AcceptChanges();

            ClientQuantityCombo.DataContext = dsOperators.Tables["Operators"];
            QuantityCombo.DataContext = dsOperators.Tables["Operators"];
            OpenTimeCombo.DataContext = dsOperators.Tables["Operators"];
        }

        private void LookupButton_Click(object sender, RoutedEventArgs e)
        {
             locateServiceClient.LocateResearchGetAsync(
                (LocateIdTextBox.Text.Equals("Locate ID") ? "" : LocateIdTextBox.Text.Trim()),
                (StartDateTextBox.Text.Equals("Start Date") ? "" : StartDateTextBox.Text.Trim()),
                (StopDateTextBox.Text.Equals("Stop Date") ? "" : StopDateTextBox.Text.Trim()),
                (ClientIdTextBox.Text.Equals("Client ID") ? "" : ClientIdTextBox.Text.Trim()),
                (GroupCodeTextBox.Text.Equals("Group Code") ? "" : GroupCodeTextBox.Text.Trim()),
                (SecIdTextBox.Text.Equals("Security ID") ? "" : SecIdTextBox.Text.Trim()),
                (SourceTextBox.Text.Equals("Source") ? "" : SourceTextBox.Text.Trim()),
                (StatusTextBox.Text.Equals("Status") ? "" : StatusTextBox.Text.Trim()),
                (ActorTextBox.Text.Equals("Actor") ? "" : ActorTextBox.Text.Trim()),
                (CommentTextBox.Text.Equals("Commment") ? "" : CommentTextBox.Text.Trim()),
                (ClientQuantityTextBox.Text.Equals("Client Quantity") ? "" : ClientQuantityTextBox.Text.Trim()),
                "",
                (QuantityTextBox.Text.Equals("Quantity") ? "" : QuantityTextBox.Text.Trim()),
                "",
                (OpenTimeTextBox.Text.Equals("Open Time") ? "" : OpenTimeTextBox.Text.Trim()),
                "",
                0
                );
        }
	}
}