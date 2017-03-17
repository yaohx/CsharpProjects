namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class MergedCellsRecord : XLSRecord
    {
        // Methods
        static MergedCellsRecord()
        {
            MergedCellsRecord.staticDescriptor = XLSDescriptors.GetByName("MergedCells");
        }

        public MergedCellsRecord(object[] args) : base(MergedCellsRecord.staticDescriptor)
        {
            base.InitializeDelayed(args);
        }

        public MergedCellsRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(MergedCellsRecord.staticDescriptor, bodyLength, br)
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

