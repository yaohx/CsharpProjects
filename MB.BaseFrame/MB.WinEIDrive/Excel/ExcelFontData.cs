namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Drawing;

    internal class ExcelFontData
    {
        // Methods
        public ExcelFontData()
        {
            this.ColorIndex = -1;
            this.Name = "Arial";
            this.Color = System.Drawing.Color.Black;
            this.Weight = 400;
            this.Size = 200;
            this.ScriptPosition = MB.WinEIDrive.Excel.ScriptPosition.Normal;
            this.UnderlineStyle = MB.WinEIDrive.Excel.UnderlineStyle.None;
        }

        public ExcelFontData(ExcelFontData source)
        {
            this.ColorIndex = -1;
            this.Name = "Arial";
            this.Color = System.Drawing.Color.Black;
            this.Weight = 400;
            this.Size = 200;
            this.ScriptPosition = MB.WinEIDrive.Excel.ScriptPosition.Normal;
            this.UnderlineStyle = MB.WinEIDrive.Excel.UnderlineStyle.None;
            this.Name = source.Name;
            this.Color = source.Color;
            this.Weight = source.Weight;
            this.Size = source.Size;
            this.Italic = source.Italic;
            this.Strikeout = source.Strikeout;
            this.ScriptPosition = source.ScriptPosition;
            this.UnderlineStyle = source.UnderlineStyle;
        }

        public override bool Equals(object obj)
        {
            ExcelFontData data1 = (ExcelFontData) obj;
            if ((((data1.Name == this.Name) && (data1.Color.ToArgb() == this.Color.ToArgb())) && ((data1.Weight == this.Weight) && (data1.Size == this.Size))) && (((data1.Italic == this.Italic) && (data1.Strikeout == this.Strikeout)) && ((data1.ScriptPosition == this.ScriptPosition) && (data1.UnderlineStyle == this.UnderlineStyle))))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int num1 = 0;
            num1 ^= this.Name.GetHashCode();
            num1 ^= this.Color.GetHashCode();
            num1 ^= this.Weight;
            num1 ^= this.Size;
            num1 ^= this.Italic.GetHashCode();
            num1 = Utilities.RotateLeft(num1, (byte) 6);
            num1 ^= this.Strikeout.GetHashCode();
            num1 ^= this.ScriptPosition.GetHashCode();
            return (num1 ^ this.UnderlineStyle.GetHashCode());
        }


        // Fields
        public System.Drawing.Color Color;
        public int ColorIndex;
        public bool Italic;
        public string Name;
        public MB.WinEIDrive.Excel.ScriptPosition ScriptPosition;
        public int Size;
        public bool Strikeout;
        public MB.WinEIDrive.Excel.UnderlineStyle UnderlineStyle;
        public int Weight;
    }
}

