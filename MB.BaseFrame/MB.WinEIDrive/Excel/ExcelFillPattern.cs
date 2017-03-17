namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Drawing;

    ///<summary>
    ///Contains fill pattern settings.
    ///</summary>
    internal sealed class ExcelFillPattern
    {
        // Methods
        internal ExcelFillPattern(CellStyle parent)
        {
            this.parent = parent;
        }

        internal void CopyTo(CellStyle destination)
        {
            destination.Element.PatternStyle = this.parent.Element.PatternStyle;
            destination.Element.PatternForegroundColor = this.parent.Element.PatternForegroundColor;
            destination.Element.PatternBackgroundColor = this.parent.Element.PatternBackgroundColor;
            destination.UseFlags &= ((CellStyleData.Properties) (-29));
            destination.UseFlags |= (this.parent.UseFlags & CellStyleData.Properties.PatternRelated);
        }

        ///<summary>
        ///Sets complex (non-empty and non-solid) pattern.
        ///</summary>
        ///<param name="patternStyle">Pattern style.</param>
        ///<param name="foregroundColor">Foreground color.</param>
        ///<param name="backgroundColor">Background color.</param>
        ///<remarks>
        ///<p>For solid pattern, just use <see cref="MB.WinEIDrive.Excel.ExcelFillPattern.SetSolid(System.Drawing.Color)">SetSolid</see> method.</p>
        ///<p>To clear fill pattern, just set <see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternStyle">PatternStyle</see>
        ///to <see cref="MB.WinEIDrive.Excel.FillPatternStyle.None">FillPatternStyle.None</see></p>
        ///</remarks>
        public void SetPattern(FillPatternStyle patternStyle, Color foregroundColor, Color backgroundColor)
        {
            this.PatternStyle = patternStyle;
            this.PatternForegroundColor = foregroundColor;
            this.PatternBackgroundColor = backgroundColor;
        }

        ///<summary>
        ///Sets solid pattern using specified fill color.
        ///</summary>
        ///<param name="fillColor">Fill color.</param>
        ///<remarks>
        ///This will set <see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternStyle">PatternStyle</see> to
        ///<see cref="MB.WinEIDrive.Excel.FillPatternStyle.Solid">FillPatternStyle.Solid</see> and
        ///<see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternForegroundColor">PatternForegroundColor</see>
        ///to <i>fillColor</i>.
        ///</remarks>
        public void SetSolid(Color fillColor)
        {
            this.PatternStyle = FillPatternStyle.Solid;
            this.PatternForegroundColor = fillColor;
        }


        // Properties
        ///<summary>
        ///Get or sets fill pattern background color.
        ///</summary>
        ///<seealso cref="MB.WinEIDrive.Excel.FillPatternStyle" />
        public Color PatternBackgroundColor
        {
            get
            {
                return this.parent.Element.PatternBackgroundColor;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.PatternBackgroundColor = value;
                this.parent.UseFlags |= CellStyleData.Properties.PatternBackgroundColor;
            }
        }

        ///<summary>
        ///Get or sets fill pattern foreground color.
        ///</summary>
        ///<seealso cref="MB.WinEIDrive.Excel.FillPatternStyle" />
        public Color PatternForegroundColor
        {
            get
            {
                return this.parent.Element.PatternForegroundColor;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.PatternForegroundColor = value;
                this.parent.UseFlags |= CellStyleData.Properties.PatternForegroundColor;
            }
        }

        ///<summary>
        ///Gets or sets fill pattern style.
        ///</summary>
        ///<remarks>
        ///If you set this property to anything else than <see cref="MB.WinEIDrive.Excel.FillPatternStyle.None">
        ///FillPatternStyle.None</see>, <see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternForegroundColor">
        ///PatternForegroundColor</see> and/or <see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternBackgroundColor">
        ///PatternBackgroundColor</see> should also be set (if color is
        ///different from default <see cref="System.Drawing.Color.Black">Color.Black</see>.
        ///</remarks>
        public FillPatternStyle PatternStyle
        {
            get
            {
                return this.parent.Element.PatternStyle;
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.PatternStyle = value;
                this.parent.UseFlags |= CellStyleData.Properties.PatternStyle;
            }
        }


        // Fields
        private CellStyle parent;
    }
}

