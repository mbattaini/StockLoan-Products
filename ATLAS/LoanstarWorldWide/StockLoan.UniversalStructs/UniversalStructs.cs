using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StockLoan.UniversalStructs
{
    public struct UserPermissions
    {
        public bool mayView;
        public bool mayEdit;

        public DataSet dsInformation;
    }
}
