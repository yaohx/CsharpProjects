namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class ExcelLongString : ExcelStringBase
    {
        // Methods
        public ExcelLongString(BinaryReader br)
        {
            int num1 = 0x2020;
            int num2 = br.ReadUInt16();
            base.ReadOptionsAndString(br, ref num1, ref num2);
        }

        public ExcelLongString(string str) : base(str)
        {
        }

        public ExcelLongString(BinaryReader br, ref int remainingSize, ref int charsRemaining)
        {
            if (charsRemaining == 0)
            {
                charsRemaining = br.ReadUInt16();
                remainingSize -= 2;
            }
            base.ReadOptionsAndString(br, ref remainingSize, ref charsRemaining);
        }

        public override string ToString()
        {
            return ("ExcelLongString(" + base.GetFormattedStr() + ")");
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write((ushort) base.Str.Length);
            base.Write(bw);
        }


        // Properties
        public override int Size
        {
            get
            {
                return (2 + base.Size);
            }
        }

    }
}

