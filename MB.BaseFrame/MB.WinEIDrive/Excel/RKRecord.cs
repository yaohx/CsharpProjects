namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class RKRecord : AbsXLSRec
    {
        // Methods
        static RKRecord()
        {
            RKRecord.staticDescriptor = XLSDescriptors.GetByName("RK");
        }

        public RKRecord(CellRecordHeader header, uint val)
        {
            this.Header = header;
            this.Val = val;
        }

        public RKRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord)
        {
            this.Header = new CellRecordHeader(br);
            this.Val = br.ReadUInt32();
        }

        protected override void WriteBody(BinaryWriter bw)
        {
            this.Header.Write(bw);
            bw.Write(this.Val);
        }


        // Properties
        protected override int BodySize
        {
            get
            {
                return RKRecord.staticDescriptor.BodySize;
            }
        }

        public override string FormattedBody
        {
            get
            {
                return (this.Header.ToString() + " Val:" + this.Val);
            }
        }

        public override string Name
        {
            get
            {
                return RKRecord.staticDescriptor.Name;
            }
        }

        internal override int RecordCode
        {
            get
            {
                return RKRecord.staticDescriptor.Code;
            }
        }


        // Fields
        public CellRecordHeader Header;
        private static XLSDescriptor staticDescriptor;
        public uint Val;
    }
}

