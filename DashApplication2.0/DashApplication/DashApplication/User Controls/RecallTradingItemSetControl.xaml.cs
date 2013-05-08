using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using DashApplication.ServicePosition;


namespace DashApplication
{

    public partial class RecallTradingItemSetControl : UserControl
    {
        private string bookGroup;
        private string bizDate;
        private string secId;
        private string comment;

        private PositionClient posClient;

        public RecallTradingItemSetControl(string bizDate, string bookGroup, string secId, string comment)
        {
            InitializeComponent();

            this.bookGroup = bookGroup;
            this.bizDate = bizDate;
            this.secId = secId;
            this.comment = comment;

            BookGroupTextBox.Text = bookGroup;
            BizDateTextBox.Text = bizDate;
            SecIdTextBox.Text = secId;
            CommentTextBox.Text = (comment.Length > 1) ? comment.Substring(0, comment.IndexOf("**")) : "";

            posClient = new PositionClient();
            posClient.RecallTradingSetCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(posClient_RecallTradingSetCompleted);
        }

        void posClient_RecallTradingSetCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Content = "Error processing request";
            }
            else
            {
                StatusLabel.Content = "Succesfully processed request";
            }

        }

        private void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            posClient.RecallTradingSetAsync(bizDate, bookGroup, secId, (CommentTextBox.Text + "**" + UserInformation.UserId + " | " + DateTime.Now.ToString("yyyy-MM-dd")));
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ((C1.Silverlight.C1Window)this.Parent).Close();
        }
    }
}
