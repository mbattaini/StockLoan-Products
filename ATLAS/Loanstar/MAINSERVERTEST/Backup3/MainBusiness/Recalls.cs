using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StockLoan.MainBusiness
{
    class Recalls
    {
        private string settlementColumnName = "";
        private DataSet dsRecalls;

        private InformationType infoType;
        private SettlementType settlementType;
        private ContractType contractType;

        public InformationType InformationType
        {
            get
            {
                return infoType;
            }

            set
            {
                infoType = value;
            }
        }

        public SettlementType SettlementType
        {
            get
            {
                return settlementType;
            }

            set
            {
                settlementType = value;
            }
        }

        public ContractType ContractType
        {
            get
            {
                return contractType;
            }

            set
            {
                contractType = value;
            }
        }


        public Recalls()
        {
        }
    }
}
