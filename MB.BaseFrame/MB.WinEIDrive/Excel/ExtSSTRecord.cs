namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class ExtSSTRecord : XLSRecord
    {
        // Methods
        static ExtSSTRecord()
        {
            ExtSSTRecord.staticDescriptor = XLSDescriptors.GetByName("ExtSST");
        }

        public ExtSSTRecord(int stringsInBucket, int offset, AbsXLSRec sstRecord) : base(ExtSSTRecord.staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
            this.stringsInBucket = stringsInBucket;
            this.offset = offset;
            this.sstRecord = sstRecord;
        }

        public ExtSSTRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(ExtSSTRecord.staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((bodySize - 2) / 8);
        }

        protected override void InitializeDelayed()
        {
            object[] objArray1 = new object[2];
            objArray1[0] = (ushort) this.stringsInBucket;
            object[] objArray2 = new object[] { (uint) (this.sstRecord.Address + this.offset), (ushort) this.offset } ;
            objArray1[1] = objArray2;
            base.InitializeDelayed(objArray1);
        }


        // Properties
        protected override int BodySize
        {
            get
            {
                if (this.Body != null)
                {
                    return this.Body.Length;
                }
                return 10;
            }
        }


        // Fields
        private int offset;
        private AbsXLSRec sstRecord;
        private static XLSDescriptor staticDescriptor;
        private int stringsInBucket;
    }
}

