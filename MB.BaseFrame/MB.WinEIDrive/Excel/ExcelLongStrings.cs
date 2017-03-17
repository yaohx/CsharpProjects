namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    internal class ExcelLongStrings : BinaryWritable
    {
        // Methods
        public ExcelLongStrings()
        {
            this.Strings = new ArrayList();
        }

        public ExcelLongStrings(BinaryReader br, int remainingSize, int charsLeftFromPrevious)
        {
            this.Strings = new ArrayList();
            while (remainingSize != 0)
            {
                ExcelLongString text1 = new ExcelLongString(br, ref remainingSize, ref charsLeftFromPrevious);
                this.Strings.Add(text1);
            }
            this.CharsRemaining = charsLeftFromPrevious;
        }

        public override string ToString()
        {
            StringBuilder builder1 = new StringBuilder("ExcelLongStrings(");
            for (int num1 = 0; num1 < this.Strings.Count; num1++)
            {
                ExcelLongString text1 = (ExcelLongString) this.Strings[num1];
                if (num1 > 0)
                {
                    builder1.Append(",");
                }
                builder1.Append(text1.GetFormattedStr());
            }
            builder1.Append(")");
            return builder1.ToString();
        }

        public override void Write(BinaryWriter bw)
        {
            foreach (ExcelLongString text1 in this.Strings)
            {
                text1.Write(bw);
            }
        }


        // Properties
        public override int Size
        {
            get
            {
                int num1 = 0;
                foreach (ExcelLongString text1 in this.Strings)
                {
                    num1 += text1.Size;
                }
                return num1;
            }
        }


        // Fields
        public int CharsRemaining;
        public ArrayList Strings;
    }
}

