namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Drawing;

    ///<summary>
    ///Contains settings for a single cell border.
    ///</summary>
    ///<remarks>
    ///Note that although diagonal-up (<see cref="MB.WinEIDrive.Excel.IndividualBorder.DiagonalUp">IndividualBorder.DiagonalUp</see>
    ///or <see cref="MB.WinEIDrive.Excel.MultipleBorders.DiagonalUp">MultipleBorders.DiagonalUp</see>) and diagonal-down
    ///(<see cref="MB.WinEIDrive.Excel.IndividualBorder.DiagonalDown">IndividualBorder.DiagonalDown</see> or
    ///<see cref="MB.WinEIDrive.Excel.MultipleBorders.DiagonalDown">MultipleBorders.DiagonalDown</see>) can be individually set,
    ///they share the same color and the same line style. This is a Microsoft Excel limitation.
    ///</remarks>
    ///<seealso cref="MB.WinEIDrive.Excel.CellBorders" />
    internal sealed class CellBorder
    {
        // Methods
        internal CellBorder(CellStyle parent, IndividualBorder borderId)
        {
            this.parent = parent;
            this.borderId = borderId;
            this.borderIndex = CellBorder.IndexFromIndividualBorder(borderId);
        }

        internal static int IndexFromIndividualBorder(IndividualBorder individualBorder)
        {
            int num1 = (int) individualBorder;
            if (num1 == 5)
            {
                num1 = 4;
            }
            return num1;
        }

        internal static MultipleBorders MultipleFromIndividualBorder(IndividualBorder individualBorder)
        {
            return (MultipleBorders) (((int) IndividualBorder.Bottom) << ((int)individualBorder & 0x1f));
        }

        public void SetBorder(Color lineColor, MB.WinEIDrive.Excel.LineStyle lineStyle)
        {
            this.LineColor = lineColor;
            this.LineStyle = lineStyle;
        }

        private void SetUsedIfNotDefault()
        {
            CellStyleData data1 = this.parent.Element;
            if ((data1.BorderStyle[this.borderIndex] != MB.WinEIDrive.Excel.LineStyle.None) || (data1.BorderColor[this.borderIndex].ToArgb() != Color.Black.ToArgb()))
            {
                data1.BordersUsed |= CellBorder.MultipleFromIndividualBorder(this.borderId);
            }
        }


        // Properties
        ///<summary>
        ///Gets or sets border line color.
        ///</summary>
        ///<remarks>
        ///Note that although diagonal-up (<see cref="MB.WinEIDrive.Excel.IndividualBorder.DiagonalUp">IndividualBorder.DiagonalUp</see>
        ///or <see cref="MB.WinEIDrive.Excel.MultipleBorders.DiagonalUp">MultipleBorders.DiagonalUp</see>) and diagonal-down
        ///(<see cref="MB.WinEIDrive.Excel.IndividualBorder.DiagonalDown">IndividualBorder.DiagonalDown</see> or
        ///<see cref="MB.WinEIDrive.Excel.MultipleBorders.DiagonalDown">MultipleBorders.DiagonalDown</see>) can be individually set,
        ///they share the same color and the same line style. This is a Microsoft Excel limitation.
        ///</remarks>
        public Color LineColor
        {
            get
            {
                return this.parent.Element.BorderColor[this.borderIndex];
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.BorderColor[this.borderIndex] = value;
                this.SetUsedIfNotDefault();
            }
        }

        public MB.WinEIDrive.Excel.LineStyle LineStyle
        {
            get
            {
                return this.parent.Element.BorderStyle[this.borderIndex];
            }
            set
            {
                this.parent.BeforeChange();
                this.parent.Element.BorderStyle[this.borderIndex] = value;
                this.SetUsedIfNotDefault();
            }
        }


        // Fields
        private readonly IndividualBorder borderId;
        private readonly int borderIndex;
        private readonly CellStyle parent;
    }
}

