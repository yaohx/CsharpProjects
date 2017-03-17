namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Drawing;

    internal class CellStyleData : HashtableElement
    {
        // Methods
        public CellStyleData(CellStyleCachedCollection parentCollection, bool isDefault) : base(parentCollection, isDefault)
        {
            this.HorizontalAlignment = HorizontalAlignmentStyle.General;
            this.VerticalAlignment = VerticalAlignmentStyle.Bottom;
            this.PatternStyle = FillPatternStyle.None;
            this.PatternBackgroundColor = Color.White;
            this.PatternForegroundColor = Color.Black;
            this.Locked = true;
            this.NumberFormat = string.Empty;
            this.FontData = new ExcelFontData();
            Color[] colorArray1 = new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black } ;
            this.BorderColor = colorArray1;
            LineStyle[] styleArray1 = new LineStyle[5];
            this.BorderStyle = styleArray1;
            this.BordersUsed = MultipleBorders.None;
        }

        public override HashtableElement Clone(WeakHashtable parentCollection)
        {
            CellStyleData data1 = new CellStyleData((CellStyleCachedCollection) parentCollection, false);
            data1.HorizontalAlignment = this.HorizontalAlignment;
            data1.VerticalAlignment = this.VerticalAlignment;
            data1.PatternStyle = this.PatternStyle;
            data1.PatternBackgroundColor = this.PatternBackgroundColor;
            data1.PatternForegroundColor = this.PatternForegroundColor;
            data1.Indent = this.Indent;
            data1.Rotation = this.Rotation;
            data1.Locked = this.Locked;
            data1.FormulaHidden = this.FormulaHidden;
            data1.WrapText = this.WrapText;
            data1.ShrinkToFit = this.ShrinkToFit;
            data1.NumberFormat = this.NumberFormat;
            data1.FontData = new ExcelFontData(this.FontData);
            data1.BorderColor = (Color[]) this.BorderColor.Clone();
            data1.BorderStyle = (LineStyle[]) this.BorderStyle.Clone();
            data1.BordersUsed = this.BordersUsed;
            return data1;
        }

        public override bool Equals(object obj)
        {
            CellStyleData data1 = (CellStyleData) obj;
            if (((((data1.HorizontalAlignment != this.HorizontalAlignment) || (data1.VerticalAlignment != this.VerticalAlignment)) || ((data1.PatternStyle != this.PatternStyle) || (data1.PatternBackgroundColor.ToArgb() != this.PatternBackgroundColor.ToArgb()))) || (((data1.PatternForegroundColor.ToArgb() != this.PatternForegroundColor.ToArgb()) || (data1.Indent != this.Indent)) || ((data1.Rotation != this.Rotation) || (data1.Locked != this.Locked)))) || (((data1.FormulaHidden != this.FormulaHidden) || (data1.WrapText != this.WrapText)) || ((data1.ShrinkToFit != this.ShrinkToFit) || (data1.NumberFormat != this.NumberFormat))))
            {
                return false;
            }
            if (!data1.FontData.Equals(this.FontData))
            {
                return false;
            }
            if (data1.BordersUsed != this.BordersUsed)
            {
                return false;
            }
            for (int num1 = 0; num1 < 4; num1++)
            {
                if ((this.BordersUsed & CellBorder.MultipleFromIndividualBorder((IndividualBorder) num1)) != MultipleBorders.None)
                {
                    if (data1.BorderColor[num1].ToArgb() != this.BorderColor[num1].ToArgb())
                    {
                        return false;
                    }
                    if (data1.BorderStyle[num1] != this.BorderStyle[num1])
                    {
                        return false;
                    }
                }
            }
            if ((this.BordersUsed & MultipleBorders.Diagonal) != MultipleBorders.None)
            {
                if (data1.BorderColor[4].ToArgb() != this.BorderColor[4].ToArgb())
                {
                    return false;
                }
                if (data1.BorderStyle[4] != this.BorderStyle[4])
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int num1 = 0;
            num1 ^= this.HorizontalAlignment.GetHashCode();
            num1 ^= this.VerticalAlignment.GetHashCode();
            num1 ^= this.PatternStyle.GetHashCode();
            num1 ^= this.PatternBackgroundColor.GetHashCode();
            num1 ^= this.PatternForegroundColor.GetHashCode();
            num1 = Utilities.RotateLeft(num1, (byte) 8);
            num1 ^= this.Indent;
            num1 ^= this.Rotation;
            num1 ^= this.Locked.GetHashCode();
            num1 ^= this.FormulaHidden.GetHashCode();
            num1 ^= this.WrapText.GetHashCode();
            num1 ^= this.ShrinkToFit.GetHashCode();
            num1 = Utilities.RotateLeft(num1, (byte) 8);
            num1 ^= this.NumberFormat.GetHashCode();
            num1 ^= this.FontData.GetHashCode();
            num1 ^= this.BordersUsed.GetHashCode();
            for (int num2 = 0; num2 < 4; num2++)
            {
                if ((this.BordersUsed & CellBorder.MultipleFromIndividualBorder((IndividualBorder) num2)) != MultipleBorders.None)
                {
                    num1 ^= this.BorderStyle[num2].GetHashCode();
                    num1 ^= this.BorderColor[num2].GetHashCode();
                }
            }
            if ((this.BordersUsed & MultipleBorders.Diagonal) != MultipleBorders.None)
            {
                num1 ^= this.BorderStyle[4].GetHashCode();
                num1 ^= this.BorderColor[4].GetHashCode();
            }
            return num1;
        }


        // Fields
        public Color[] BorderColor;
        public LineStyle[] BorderStyle;
        public MultipleBorders BordersUsed;
        public ExcelFontData FontData;
        public bool FormulaHidden;
        public HorizontalAlignmentStyle HorizontalAlignment;
        public int Indent;
        public CellStyleDataIndexes Indexes;
        public bool Locked;
        public string NumberFormat;
        public Color PatternBackgroundColor;
        public Color PatternForegroundColor;
        public FillPatternStyle PatternStyle;
        public int Rotation;
        public bool ShrinkToFit;
        public VerticalAlignmentStyle VerticalAlignment;
        public bool WrapText;

        // Nested Types
        [Flags]
        public enum Properties
        {
            // Fields
            All = 0xfffff,
            FontColor = 0x2000,
            FontItalic = 0x10000,
            FontName = 0x1000,
            FontRelated = 0xff000,
            FontScriptPosition = 0x40000,
            FontSize = 0x8000,
            FontStrikeout = 0x20000,
            FontUnderlineStyle = 0x80000,
            FontWeight = 0x4000,
            FormulaHidden = 0x100,
            HorizontalAlignment = 1,
            Indent = 0x20,
            Locked = 0x80,
            None = 0,
            NumberFormat = 0x800,
            PatternBackgroundColor = 8,
            PatternForegroundColor = 0x10,
            PatternRelated = 0x1c,
            PatternStyle = 4,
            Rotation = 0x40,
            ShrinkToFit = 0x400,
            VerticalAlignment = 2,
            WrapText = 0x200
        }
    }
}

