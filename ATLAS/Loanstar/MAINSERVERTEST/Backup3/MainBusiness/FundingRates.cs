using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StockLoan.Main;

namespace StockLoan.MainBusiness
{
    public class FundingRates
    {
        private bool dataLoaded = false;
        private string lastTimeUpdated; 

        private IService serviceAgent;
        private IPosition positionAgent;

        private DataSet dsFunds;
        private DataSet dsFundingRates;

        private string bizDate;

        public FundingRates(IService serviceAgent, IPosition positionAgent)
        {
            this.serviceAgent = serviceAgent;
            this.positionAgent = positionAgent;

            this.BizDate = serviceAgent.BizDate();
        }


        public void Load()
        {
            try
            {
                dsFunds = positionAgent.FundsGet();
                dsFundingRates = positionAgent.FundingRatesGet(bizDate);
            }
            catch { }
        }

        public void Refresh()
        {
            Load();
        }

        public DataTable FundsData
        {
            get
            {
                return dsFunds.Tables["Funds"];
            }
        }

        public DataTable FundingRatesData
        {
            get
            {
                return dsFundingRates.Tables["FundingRates"];
            }
        }

        public string BizDate
        {
            get
            {
                return bizDate;
            }

            set
            {
                bizDate = value;
                Load();
            }
        }
    }
}
