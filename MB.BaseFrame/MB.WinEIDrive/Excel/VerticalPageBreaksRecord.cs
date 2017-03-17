namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class VerticalPageBreaksRecord : XLSRecord
    {
        // Methods
        static VerticalPageBreaksRecord()
        {
            VerticalPageBreaksRecord.staticDescriptor = XLSDescriptors.GetByName("VERTICALPAGEBREAKS");
        }

        public VerticalPageBreaksRecord(object[] args) : base(VerticalPageBreaksRecord.staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public VerticalPageBreaksRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(VerticalPageBreaksRecord.staticDescriptor, bodyLength, br)
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

