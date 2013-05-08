using System;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.BDData
{
    class BoxPositionCalculation
    {
        private string dbCnStr;

        public BoxPositionCalculation(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;
        }


        public string CalculateColumn(string table, string bookGroup, string bizDate, string columnName, string locMemo, string locLocation, bool isPositive, string accountRange, string accountParticular, string accountTypes, string pairOffLocMemo)
        {
            string query;

            query = "

        }

    }
}
