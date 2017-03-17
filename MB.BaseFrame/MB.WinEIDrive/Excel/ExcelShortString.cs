namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class ExcelShortString : ExcelStringBase
    {
        // Methods
        public ExcelShortString(BinaryReader br)
        {
            int num1 = 0x2020;
            int num2 = br.ReadByte();
            base.ReadOptionsAndString(br, ref num1, ref num2);
        }

        public ExcelShortString(string str) : base(str)
        {
        }

        public override string ToString()
        {
            return ("ExcelShortString(" + base.GetFormattedStr() + ")");
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write((byte) base.Str.Length);
            base.Write(bw);
        }


        // Properties
        public override int Size
        {
            get
            {
                return (1 + base.Size);
            }
        }

    }
}

