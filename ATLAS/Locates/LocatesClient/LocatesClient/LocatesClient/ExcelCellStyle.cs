using System;

namespace LocatesClient
{
    public class ExcelCellStyle
    {
        private string dataField;
        public string DataField
        {
            get { return dataField; }
            set { dataField = value; }
        }

        private string stringFormat;
        public string StringFormat
        {
            get { return stringFormat; }
            set { stringFormat = value; }
        }

        private Type dataType;
        public Type DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
    }
}
