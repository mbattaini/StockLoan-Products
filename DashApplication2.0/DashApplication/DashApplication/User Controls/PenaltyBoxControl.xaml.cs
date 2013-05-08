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
using System.Globalization;

using C1.Silverlight;
using C1.Silverlight.Data;
using DashApplication.ServicePosition;

namespace DashApplication
{    
    public partial class PenaltyBoxControl : UserControl
    {
        public PositionClient posCLient;

        public PenaltyBoxControl()
        {
            InitializeComponent();

            posCLient = new PositionClient();
            posCLient.PenaltyBoxGetCompleted += new EventHandler<PenaltyBoxGetCompletedEventArgs>(posCLient_PenaltyBoxGetCompleted);
            posCLient.PenaltyBoxItemSetCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(posCLient_PenaltyBoxItemSetCompleted);

            posCLient.PenaltyBoxGetAsync((bool)CheckBoxShowHistory.IsChecked);
        }

        void posCLient_PenaltyBoxItemSetCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            posCLient.PenaltyBoxGetAsync((bool)CheckBoxShowHistory.IsChecked);
        }

        void posCLient_PenaltyBoxGetCompleted(object sender, PenaltyBoxGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            PenaltyBoxGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "PenaltyBox").DefaultView;
        }

        private void ButtonAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            posCLient.PenaltyBoxItemSetAsync(
                SecIdTextBox.Text,
                UserInformation.UserId,
                CommentTextBox.Text,
                false);
        }

        private void CheckBoxShowHistory_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            posCLient.PenaltyBoxGetAsync((bool)CheckBoxShowHistory.IsChecked);
        }

        private void PenaltyBoxGrid_LoadedCellPresenter(object sender, C1.Silverlight.DataGrid.DataGridCellEventArgs e)
        {
            C1MouseHelper mouseHelperInfoGrid = new C1MouseHelper(e.Cell.Presenter);
            mouseHelperInfoGrid.MouseDoubleClick += new MouseButtonEventHandler(mouseHelperInfoGrid_MouseDoubleClick);
        }

        void mouseHelperInfoGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClientWindow clntWindow = new ClientWindow();
            clntWindow.Content = new PenaltyBoxItemSetControl(PenaltyBoxGrid[PenaltyBoxGrid.SelectedIndex, 0].Text,
                                                              PenaltyBoxGrid[PenaltyBoxGrid.SelectedIndex, 1].Text,
                                                              PenaltyBoxGrid[PenaltyBoxGrid.SelectedIndex, 7].Text);
            clntWindow.Show();
            clntWindow.CenterOnScreen();
        }        
    }
}
