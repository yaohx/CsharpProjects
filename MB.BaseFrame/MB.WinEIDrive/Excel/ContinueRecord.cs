namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;
    using System.Text;

    internal class ContinueRecord : SSTRelated
    {
        // Methods
        static ContinueRecord()
        {
            ContinueRecord.staticDescriptor = XLSDescriptors.GetByName("Continue");
        }

        public ContinueRecord(ExcelLongStrings excelStrings) : base(ContinueRecord.staticDescriptor)
        {
            base.ExcelStrings = excelStrings;
            object[] objArray1 = new object[] { excelStrings } ;
            base.InitializeDelayed(objArray1);
        }

        public ContinueRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(ContinueRecord.staticDescriptor, bodyLength, br)
        {
            SSTRelated related1 = previousRecord as SSTRelated;
            if ((related1 != null) && (related1.ExcelStrings != null))
            {
                using (MemoryStream stream1 = new MemoryStream(this.Body))
                {
                    using (BinaryReader reader1 = new BinaryReader(stream1, new UnicodeEncoding()))
                    {
                        base.ExcelStrings = new ExcelLongStrings(reader1, bodyLength, related1.ExcelStrings.CharsRemaining);
                    }
                }
            }
        }


        // Properties
        public override string FormattedBody
        {
            get
            {
                if (base.ExcelStrings != null)
                {
                    return base.ExcelStrings.ToString();
                }
                return "Unknown data";
            }
        }


        // Fields
        private static XLSDescriptor staticDescriptor;
    }
}

