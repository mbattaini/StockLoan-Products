using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess; 

namespace StockLoan.Business
{
    public class Funds
    {

        public static DataSet FundingRatesGet(string bizDate, short utcOffset)
        {
            DataSet dsTemp = new DataSet();

            try
            {

                dsTemp = DBFunds.FundingRatesGet(bizDate, utcOffset);
                return dsTemp;

            }
            catch 
            {
                throw;
            }
        }

        public static void FundingRateSet(string bizDate, string fund, string fundingRate, string actUserId )
        {
                
            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                if (fund.Equals(""))
                {
                    throw new Exception("Fund is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }

                DBFunds.FundingRateSet(bizDate, fund, fundingRate, actUserId);

            }
            catch 
            {
                throw;
            }
        }

        public static DataSet FundsGet()
        {
            DataSet dsTemp = new DataSet();
            try
            {
                dsTemp = DBFunds.FundsGet();

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static bool FundingRatesRoll(string bizDate, string bizDatePrior)
        {
            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                if (bizDatePrior.Equals(""))
                {
                    throw new Exception("Prior Biz Date is required");
                }

                DBFunds.FundingRatesRoll(bizDate, bizDatePrior);

                return true;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet FundingRateResearchGet(string startDate, string stopDate, string fund, short utcOffset)
        {
            DataSet dsTemp = new DataSet();

            try
            {
                if (startDate.Equals(""))
                {
                    throw new Exception("Start Date is required");
                }
                
                if (stopDate.Equals(""))
                {
                    throw new Exception("Stop Date is required");
                }
                
                if (fund.Equals(""))
                {
                    throw new Exception("Fund is required");
                }

                dsTemp = DBFunds.FundingRateResearchGet(startDate, stopDate, fund, utcOffset);
                
                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }
    }
}
