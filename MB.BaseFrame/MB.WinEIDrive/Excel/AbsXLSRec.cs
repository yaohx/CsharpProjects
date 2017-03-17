namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;
    using System.Text;

    internal abstract class AbsXLSRec : BinaryWritable
    {
        // Methods
        protected AbsXLSRec()
        {
            this.Address = -1;
        }

        public string GetXMLRecord(int index)
        {
            object[] objArray1;
            StringBuilder builder1 = new StringBuilder();
            string text1 = this.Name;
            if (((text1 == "Font") || (text1 == "Format")) || ((text1 == "XF") || (text1 == "Style")))
            {
                if ((text1 == "Font") && (index > 3))
                {
                    index++;
                }
                if (text1 == "Format")
                {
                    index += 0xa4;
                }
                object obj1 = text1;
                objArray1 = new object[] { obj1, "(", index, ")" } ;
                text1 = string.Concat(objArray1);
            }
            objArray1 = new object[] { text1, this.RecordCode, this.FormattedBody, this.BodySize, Utilities.ByteArr2HexStr(this.Body) } ;
            builder1.AppendFormat("<Record Name=\"{0}\" Code=\"0x{1:X4}\" FormattedBody=\"{2}\" BodySize=\"{3}\" Body=\"{4}\"/>", objArray1);
            return builder1.ToString();
        }

        public static AbsXLSRec Read(BinaryReader br, AbsXLSRec previousRecord)
        {
            int num1 = br.ReadUInt16();
            if (num1 == 0)
            {
                return null;
            }
            int num2 = br.ReadUInt16();
            XLSDescriptor descriptor1 = XLSDescriptors.GetByCode(num1);
            if (!XLSDescriptors.ValidBodySize(descriptor1, num2, false))
            {
                descriptor1 = null;
            }
            if ((descriptor1 != null) && (descriptor1.Name == "FILEPASS"))
            {
                throw new Exception("Current version of ExcelLite can't read encrypted workbooks. You can use only simple password protection against modifying (set in MS Excel 'Save As' dialog).");
            }
            if ((descriptor1 != null) && (descriptor1.HandlerClass != typeof(XLSRecord)))
            {
                object[] objArray1 = new object[] { num2, br, previousRecord } ;
                return (AbsXLSRec) Activator.CreateInstance(descriptor1.HandlerClass, objArray1);
            }
            return new XLSRecord(num1, num2, br);
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write((ushort) this.RecordCode);
            bw.Write((ushort) this.BodySize);
            this.WriteBody(bw);
        }

        protected abstract void WriteBody(BinaryWriter bw);


        // Properties
        protected virtual byte[] Body
        {
            get
            {
                byte[] buffer1 = new byte[this.Size];
                using (MemoryStream stream1 = new MemoryStream(buffer1))
                {
                    using (BinaryWriter writer1 = new BinaryWriter(stream1, new UnicodeEncoding()))
                    {
                        this.WriteBody(writer1);
                        return buffer1;
                    }
                }
            }
        }

        protected abstract int BodySize { get; }

        public abstract string FormattedBody { get; }

        public abstract string Name { get; }

        internal abstract int RecordCode { get; }

        public override int Size
        {
            get
            {
                return (4 + this.BodySize);
            }
        }


        // Fields
        public int Address;
        public const int HeaderSize = 4;
        public const int MaxBodySize = 0x2020;
        public const int MaxSize = 0x2024;
    }
}

