using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockLoan.ParsingFiles
{
    public class ProgressEventArgs : EventArgs
    {
        private long progress;

        public ProgressEventArgs(long progress)
        {
            this.progress = progress;
        }


        public long Progress
        {
            get { return progress; }
        }
    } 

}
