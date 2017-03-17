namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class SSTRelated : XLSRecord
    {
        // Methods
        public SSTRelated(XLSDescriptor descriptor) : base(descriptor)
        {
        }

        public SSTRelated(XLSDescriptor descriptor, int bodySize, BinaryReader br) : base(descriptor, bodySize, br)
        {
        }


        // Fields
        public ExcelLongStrings ExcelStrings;
    }
}

