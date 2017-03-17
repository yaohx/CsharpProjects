namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class MulBlankRecord : XLSRecord
    {
        // Methods
        static MulBlankRecord()
        {
            MulBlankRecord.staticDescriptor = XLSDescriptors.GetByName("MulBlank");
        }

        public MulBlankRecord(object[] args) : base(MulBlankRecord.staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public MulBlankRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(MulBlankRecord.staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((bodySize - 6) / 2);
        }


        // Fields
        private static XLSDescriptor staticDescriptor;
    }
}

