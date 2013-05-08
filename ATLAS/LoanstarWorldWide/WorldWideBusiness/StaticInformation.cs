using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class StaticInformation
    {
        private DataSet dsBookGroups;
        
        public DataTable BookGroupData
        {
            get
            {
                dsBookGroups = Books.BookGroupsGet("", "");
                return dsBookGroups.Tables["BookGroups"];
            }
        }

        public static string BizDate(string bookGroup)
        {
            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDate";
                
                DateTime bizDate;

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }

        }

        public static string BizDateNext(string bookGroup)
        {
            try
            {
                DateTime bizDateNext;
    
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDateNext";

                bizDateNext = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDateNext.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDatePrior(string bookGroup)
        {
            try
            {
                
                DateTime bizDatePrior;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDatePrior";

                bizDatePrior = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDatePrior.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDateBank(string bookGroup)
        {
            try
            {
                DateTime bizDate;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDateBank";

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDateNextBank(string bookGroup)
        {
            try
            {
                DateTime bizDate;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDateNextBank";

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDatePriorBank(string bookGroup)
        {
            try
            {
                DateTime bizDate;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDatePriorBank";

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDateExchange(string bookGroup)
        {
            try
            {
                DateTime bizDate;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDateExchange";

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDateNextExchange(string bookGroup)
        {
            try
            {
                DateTime bizDate;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDateNextExchange";

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDatePriorExchange(string bookGroup)
        {
            try
            {
                DateTime bizDate;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDatePriorExchange";

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string BizDateContract(string bookGroup)
        {
            try
            {
                DateTime bizDate;

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required to get the correct date");
                }

                string dateType = "BizDateContract";

                bizDate = DBStandardFunctions.BizDateGet(bookGroup, dateType);

                return bizDate.ToString();
            }
            catch 
            {
                throw;
            }
        }

        public static string ProcessId(string prefix)
        {
            if (prefix.Length > 16)
            {
                prefix = prefix.Substring(0, 16);
            }

            return prefix + DateTime.UtcNow.ToString("yyyyMMddHHmmssff").Substring(prefix.Length, 16 - prefix.Length);
        }
        
        public static string ProcessId()
        {
            return ProcessId("");
        }
    }

}
