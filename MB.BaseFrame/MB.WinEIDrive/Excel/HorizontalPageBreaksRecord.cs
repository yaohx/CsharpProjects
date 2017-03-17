namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class HorizontalPageBreaksRecord : XLSRecord
    {
        // Methods
        static HorizontalPageBreaksRecord()
        {
            HorizontalPageBreaksRecord.staticDescriptor = XLSDescriptors.GetByName("HORIZONTALPAGEBREAKS");
        }

        public HorizontalPageBreaksRecord(object[] args) : base(HorizontalPageBreaksRecord.staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public HorizontalPageBreaksRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(HorizontalPageBreaksRecord.staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (ushort) loadedArgs[0];
        }


        // Fields
        private static XLSDescriptor staticDescriptor;
    }
}

