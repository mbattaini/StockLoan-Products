using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StockLoan.Main;

namespace StockLoan.MainBusiness
{
    public class StaticInformation
    {
        private string userId;
        private DataSet dsBookGroups;

        private IService serviceAgent;
        private IPosition positionAgent;
        private IAdmin adminAgent;

        private bool dataLoaded = false;

        public StaticInformation(IService serviceAgent, IPosition positionAgent, string userId)
        {
            this.serviceAgent = serviceAgent;
            this.positionAgent = positionAgent;
            this.userId = userId;
        }

        public void Load()
        {
            try
            {
                dsBookGroups = serviceAgent.BookGroupGet(userId);

                dataLoaded = true;
            }
            catch
            {
                dataLoaded = false;
            }
        }


        public void Refresh ()
        {
            Load();
        }

        public DataTable BookGroupData
        {
            get
            {
                return dsBookGroups.Tables["BookGroups"];
            }
        }
    }
}
