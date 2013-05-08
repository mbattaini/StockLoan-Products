using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class PenaltyBoxItemSetControl : UserControl
    {
        private PositionClient psClient;

        public PenaltyBoxItemSetControl(string processId, string secId, string comment)
        {
            InitializeComponent();

            ProcessIdTextBox.Text = processId;
            SecIdTextBox.Text = secId;
            CommentTextBox.Text = comment;

            psClient = new PositionClient();
            psClient.PenaltyBoxCommentSetCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(psClient_PenaltyBoxCommentSetCompleted);
        }

        void psClient_PenaltyBoxCommentSetCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Content = "Error processing request.";
            }
            else
            {
                StatusLabel.Content = "Success processing request.";
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            psClient.PenaltyBoxCommentSetAsync(ProcessIdTextBox.Text, CommentTextBox.Text);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ((C1.Silverlight.C1Window)this.Parent).Close();
        }
    }
}
