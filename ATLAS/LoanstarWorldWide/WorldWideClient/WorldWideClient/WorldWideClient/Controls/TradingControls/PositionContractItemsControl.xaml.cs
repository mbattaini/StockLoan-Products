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
using WorldWideClient.ServiceContracts;

namespace WorldWideClient
{
	public partial class PositionContractItemsControl : UserControl
	{
        private ContractsServiceClient csClient;
        private DataTable dtContracts;

        public PositionContractItemsControl(string bizDate, string bookGroup, string book, string secId, string functionPath)
        {
            InitializeComponent();

            dtContracts = new DataTable();

            csClient = new ContractsServiceClient();
            csClient.ContractsResearchGetCompleted += new EventHandler<ContractsResearchGetCompletedEventArgs>(csClient_ContractsResearchGetCompleted);

            csClient.ContractsResearchGetAsync(bizDate, "", "", bookGroup, "", "", secId, "", "", UserInformation.UserId, UserInformation.Password, functionPath);

            ContractsGrid.IsLoading = true;
        }

        void csClient_ContractsResearchGetCompleted(object sender, ContractsResearchGetCompletedEventArgs e)
        {
            ContractsGrid.IsLoading = false;

            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            dtContracts = Functions.ConvertToDataTable(e.Result, "ContractResearch");

            ContractsGrid.ItemsSource = dtContracts.DefaultView;
        }   
	}
}