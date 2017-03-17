namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;
    using System.Text;

    internal class StyleRecord : XLSRecord
    {
        // Methods
        static StyleRecord()
        {
            StyleRecord.staticDescriptor = XLSDescriptors.GetByName("Style");
        }

        public StyleRecord(string bodyHex) : base(StyleRecord.staticDescriptor.Name, bodyHex)
        {
        }

        public StyleRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(StyleRecord.staticDescriptor, bodyLength, br)
        {
        }


        // Properties
        public override string FormattedBody
        {
            get
            {
                StringBuilder builder1 = new StringBuilder();
                using (MemoryStream stream1 = new MemoryStream(this.Body))
                {
                    using (BinaryReader reader1 = new BinaryReader(stream1, new UnicodeEncoding()))
                    {
                        ushort num4 = reader1.ReadUInt16();
                        int num1 = num4 & 0xfff;
                        bool flag1 = (num4 & 0x8000) == 0;
                        builder1.Append("indexXF:" + num1);
                        builder1.Append(" userDefined:" + flag1);
                        if (flag1)
                        {
                            string text1 = new ExcelShortString(reader1).Str;
                            builder1.Append(" name:" + text1);
                        }
                        else
                        {
                            int num2 = reader1.ReadByte();
                            int num3 = reader1.ReadByte();
                            builder1.Append(" builtInID:" + num2);
                            builder1.Append(" outlineLevel:" + num3);
                        }
                    }
                }
                return builder1.ToString();
            }
        }


        // Fields
        private static XLSDescriptor staticDescriptor;
    }
}

