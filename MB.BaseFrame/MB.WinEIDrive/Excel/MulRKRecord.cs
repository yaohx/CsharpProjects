namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class MulRKRecord : XLSRecord
    {
        // Methods
        static MulRKRecord()
        {
            MulRKRecord.staticDescriptor = XLSDescriptors.GetByName("MulRK");
        }

        public MulRKRecord(object[] args) : base(MulRKRecord.staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public MulRKRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(MulRKRecord.staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((bodySize - 6) / 6);
        }


        // Fields
        private static XLSDescriptor staticDescriptor;
    }
}

