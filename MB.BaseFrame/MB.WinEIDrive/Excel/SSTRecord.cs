namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class SSTRecord : SSTRelated
    {
        // Methods
        static SSTRecord()
        {
            SSTRecord.staticDescriptor = XLSDescriptors.GetByName("SST");
        }

        public SSTRecord(object[] args) : base(SSTRecord.staticDescriptor)
        {
            base.InitializeDelayed(args);
        }

        public SSTRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(SSTRecord.staticDescriptor, bodyLength, br)
        {
            object[] objArray1 = base.GetArguments();
            this.TotalStringCount = (int) ((uint) objArray1[0]);
            this.UniqueStringCount = (int) ((uint) objArray1[1]);
            base.ExcelStrings = (ExcelLongStrings) objArray1[2];
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (bodySize - 8);
        }


        // Properties
        public override string FormattedBody
        {
            get
            {
                object[] objArray1 = new object[] { "TotalStringCount:", this.TotalStringCount, " UniqueStringCount:", this.UniqueStringCount, base.ExcelStrings.ToString() } ;
                return string.Concat(objArray1);
            }
        }


        // Fields
        private static XLSDescriptor staticDescriptor;
        public int TotalStringCount;
        public int UniqueStringCount;
    }
}

