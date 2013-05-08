using System;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.Inventory
{
    /// <summary>
    /// Email searching criteria 
    /// </summary>
    public class EmailSearchCriteria
    {
        /// <summary>
        /// Email adderss of From
        /// for example: john@domain.com
        /// </summary>
        private String _from;
        public String From
        {
            get { return _from; }
            set { _from = value.Trim(); }
        }

        /// <summary>
        /// Email subject
        /// </summary>
        private String _subject;
        public String Subject
        {
            get { return _subject; }
            set { _subject = value.Trim(); }
        }

        /// <summary>
        /// Date of email in header disregarding time and timezone.
        /// </summary>
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        /// <summary>
        /// The direction of Email Date;
        /// </summary>
        private EmailDateSearchOption _option;
        public EmailDateSearchOption EmailDateOption
        {
            get { return _option; }
            set { _option = value;}
        }

        public enum EmailDateSearchOption
        {
            // Don't use Email data in searching process
            NOSEARCH,
            // Messages whose [RFC-2822] Date: header (disregarding time and timezone) 
            // is within or later than the specified date.
            SENTSINCE,
            // Messages whose [RFC-2822] Date: header (disregarding time and timezone) 
            // is earlier than the specified date.
            SENTBEFORE
        }
    }
}
