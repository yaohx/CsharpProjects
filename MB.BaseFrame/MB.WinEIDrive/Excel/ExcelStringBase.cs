namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;
    using System.Text;

    internal class ExcelStringBase : BinaryWritable
    {
        // Methods
        public ExcelStringBase()
        {
        }

        public ExcelStringBase(string str)
        {
            this.Str = str;
            this.Options = ExcelStringOptions.Uncompressed;
        }

        public override bool Equals(object obj)
        {
            ExcelLongString text1 = (ExcelLongString) obj;
            if (this.Str == text1.Str)
            {
                return (this.Options == text1.Options);
            }
            return false;
        }

        public string GetFormattedStr()
        {
            StringBuilder builder1 = new StringBuilder(this.Str);
            for (int num1 = 0; num1 < builder1.Length; num1++)
            {
                char ch1 = builder1[num1];
                if (!char.IsLetterOrDigit(ch1) && (ch1 != ' '))
                {
                    builder1[num1] = 'X';
                }
            }
            return builder1.ToString();
        }

        public override int GetHashCode()
        {
            return (this.Str.GetHashCode() ^ this.Options.GetHashCode());
        }

        protected void ReadOptionsAndString(BinaryReader br, ref int remainingSize, ref int charsRemaining)
        {
            int num1;
            ushort num2;
            uint num3;
            this.Options = (ExcelStringOptions) br.ReadByte();
            remainingSize -= 1;
            if ((this.Options & ExcelStringOptions.RichText) != ((ExcelStringOptions) ((byte) 0)))
            {
                num2 = br.ReadUInt16();
                remainingSize -= 2;
            }
            else
            {
                num2 = 0;
            }
            if ((this.Options & ExcelStringOptions.AsianPhonetic) != ((ExcelStringOptions) ((byte) 0)))
            {
                num3 = br.ReadUInt32();
                remainingSize -= 4;
            }
            else
            {
                num3 = 0;
            }
            char[] chArray1 = new char[charsRemaining];
            if ((this.Options & ExcelStringOptions.Uncompressed) != ((ExcelStringOptions) ((byte) 0)))
            {
                for (num1 = 0; (charsRemaining > 0) && (remainingSize > 0); num1++)
                {
                    chArray1[num1] = br.ReadChar();
                    remainingSize -= 2;
                    charsRemaining -= 1;
                }
            }
            else
            {
                for (num1 = 0; (charsRemaining > 0) && (remainingSize > 0); num1++)
                {
                    chArray1[num1] = (char) br.ReadByte();
                    remainingSize -= 1;
                    charsRemaining -= 1;
                }
            }
            this.Str = new string(chArray1, 0, chArray1.Length - charsRemaining);
            num1 = 0;
            while ((num1 < num2) && (remainingSize > 0))
            {
                br.ReadUInt32();
                remainingSize -= 4;
                num1++;
            }
            for (num1 = 0; (num1 < num3) && (remainingSize > 0); num1++)
            {
                br.ReadByte();
                remainingSize -= 1;
            }
        }

        public override void Write(BinaryWriter bw)
        {
            string text1;
            int num1;
            bw.Write((byte) this.Options);
            if ((this.Options & ExcelStringOptions.Uncompressed) != ((ExcelStringOptions) ((byte) 0)))
            {
                text1 = this.Str;
                for (num1 = 0; num1 < text1.Length; num1++)
                {
                    char ch1 = text1[num1];
                    bw.Write(ch1);
                }
            }
            else
            {
                text1 = this.Str;
                for (num1 = 0; num1 < text1.Length; num1++)
                {
                    char ch2 = text1[num1];
                    bw.Write((byte) ch2);
                }
            }
        }


        // Properties
        public override int Size
        {
            get
            {
                int num1 = 1;
                if ((this.Options & ExcelStringOptions.Uncompressed) != ((ExcelStringOptions) ((byte) 0)))
                {
                    return (num1 + (this.Str.Length * 2));
                }
                return (num1 + this.Str.Length);
            }
        }


        // Fields
        public ExcelStringOptions Options;
        public string Str;
    }
}

