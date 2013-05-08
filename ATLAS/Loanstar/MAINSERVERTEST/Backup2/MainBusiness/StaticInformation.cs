using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StockLoan.Main;

namespace StockLoan.MainBusiness
{
    public class StaticInformation
    {
        private DataSet dsBookGroups;

        private IService serviceAgent;
        private IPosition positionAgent;
        private IAdmin adminAgent;

        private bool dataLoaded = false;

        public StaticInformation(IService serviceAgent, IPosition positionAgent)
        {
            this.serviceAgent = serviceAgent;
            this.positionAgent = positionAgent;
        }

        public void Load()
        {
            try
            {
                dsBookGroups = serviceAgent.BookGroupGet();

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
