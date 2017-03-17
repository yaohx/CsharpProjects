namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class PaletteRecord : XLSRecord
    {
        // Methods
        static PaletteRecord()
        {
            PaletteRecord.staticDescriptor = XLSDescriptors.GetByName("Palette");
        }

        public PaletteRecord(object[] args) : base(PaletteRecord.staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public PaletteRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(PaletteRecord.staticDescriptor, bodyLength, br)
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

