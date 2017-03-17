namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Drawing;

    internal class MergedCellRange : CellRange
    {
        // Methods
        internal MergedCellRange(CellRange cellRange) : base(cellRange.Parent, cellRange.FirstRowIndex, cellRange.FirstColumnIndex, cellRange.LastRowIndex, cellRange.LastColumnIndex)
        {
        }

        internal MergedCellRange(ExcelWorksheet parent, MergedCellRange sourceMRange) : base(parent, sourceMRange.FirstRowIndex, sourceMRange.FirstColumnIndex, sourceMRange.LastRowIndex, sourceMRange.LastColumnIndex)
        {
            this.cellValue = sourceMRange.ValueInternal;
            this.Style = sourceMRange.Style;
        }

        internal void FixRowIndexes(int rowIndex, int offset)
        {
            if (base.FirstRowIndex >= rowIndex)
            {
                base.FixFirstRowIndex(rowIndex, offset);
                base.FixLastRowIndex(rowIndex, offset);
            }
            else if (base.LastRowIndex >= rowIndex)
            {
                base.FixLastRowIndex(rowIndex, offset);
                if (offset > 0)
                {
                    for (int num1 = 0; num1 < offset; num1++)
                    {
                        for (int num2 = base.FirstColumnIndex; num2 <= base.LastColumnIndex; num2++)
                        {
                            base.Parent.Rows[rowIndex + num1].AllocatedCells[num2].AddToMergedRangeInternal(this);
                        }
                    }
                }
            }
        }

        public override void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle)
        {
            this.Style.Borders.SetBorders(multipleBorders, lineColor, lineStyle);
        }

        internal CellStyle StyleResolved(int row, int column)
        {
            if (this.IsStyleDefault)
            {
                return null;
            }
            IndividualBorder[] borderArray1 = new IndividualBorder[4];
            int num1 = 0;
            CellBorder border1 = this.style.Borders[IndividualBorder.Bottom];
            if ((border1.LineStyle != LineStyle.None) && (row < base.LastRowIndex))
            {
                borderArray1[num1++] = IndividualBorder.Bottom;
            }
            border1 = this.style.Borders[IndividualBorder.Top];
            if ((border1.LineStyle != LineStyle.None) && (row > base.FirstRowIndex))
            {
                borderArray1[num1++] = IndividualBorder.Top;
            }
            border1 = this.style.Borders[IndividualBorder.Right];
            if ((border1.LineStyle != LineStyle.None) && (column < base.LastColumnIndex))
            {
                borderArray1[num1++] = IndividualBorder.Right;
            }
            border1 = this.style.Borders[IndividualBorder.Left];
            if ((border1.LineStyle != LineStyle.None) && (column > base.FirstColumnIndex))
            {
                borderArray1[num1++] = IndividualBorder.Left;
            }
            if (num1 == 0)
            {
                return this.style;
            }
            CellStyle style1 = new CellStyle(this.style, this.style.Element.ParentCollection);
            for (int num2 = 0; num2 < num1; num2++)
            {
                style1.Borders[borderArray1[num2]].SetBorder(Color.Empty, LineStyle.None);
            }
            return style1;
        }


        // Properties
        ///<summary>
        ///Gets or sets merged range formula string.
        ///</summary>
        public override string Formula
        {
            get
            {
                CellFormula formula1 = this.ValueInternal as CellFormula;
                if (formula1 != null)
                {
                    return formula1.Formula;
                }
                return null;
            }
            set
            {
                this.ValueInternal = new CellFormula(value, base.Parent);
            }
        }

        internal CellFormula FormulaInternal
        {
            get
            {
                object obj1 = this.ValueInternal;
                if (obj1 is CellFormula)
                {
                    return (CellFormula) obj1;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.ValueInternal = value;
                }
                else
                {
                    this.ValueInternal = this.Value;
                }
            }
        }

        public override bool IsStyleDefault
        {
            get
            {
                if (this.style != null)
                {
                    return this.style.IsDefault;
                }
                return true;
            }
        }

        public override bool Merged
        {
            get
            {
                return true;
            }
            set
            {
                throw new InvalidOperationException("MergedCellRange is always merged.");
            }
        }

        public override CellStyle Style
        {
            get
            {
                if (this.style == null)
                {
                    this.style = new CellStyle(base.Parent.ParentExcelFile.CellStyleCache);
                }
                return this.style;
            }
            set
            {
                this.style = new CellStyle(value, base.Parent.ParentExcelFile.CellStyleCache);
            }
        }

        public override object Value
        {
            get
            {
                object obj1 = this.ValueInternal;
                if (obj1 is CellFormula)
                {
                    obj1 = ((CellFormula) obj1).Value;
                }
                return obj1;
            }
            set
            {
                if (value != null)
                {
                    ExcelFile.ThrowExceptionForUnsupportedType(value.GetType());
                }
                object obj1 = this.ValueInternal;
                if (obj1 is CellFormula)
                {
                    ((CellFormula) obj1).Value = obj1;
                }
                else
                {
                    this.ValueInternal = value;
                    base.CheckMultiline(value);
                }
            }
        }

        internal object ValueInternal
        {
            get
            {
                return this.cellValue;
            }
            set
            {
                this.cellValue = value;
            }
        }


        // Fields
        private object cellValue;
        private CellStyle style;
    }
}

