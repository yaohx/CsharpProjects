namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Drawing;

    internal class MergedCellRanges
    {
        // Methods
        internal MergedCellRanges(ExcelWorksheet parent)
        {
            this.items = new Hashtable();
            this.parent = parent;
        }

        internal MergedCellRanges(ExcelWorksheet parent, MergedCellRanges sourceRanges)
        {
            this.items = new Hashtable();
            this.parent = parent;
            foreach (MergedCellRange range1 in sourceRanges.Values)
            {
                this.Add(new MergedCellRange(this.parent, range1));
            }
        }

        internal void Add(MergedCellRange mergedRange)
        {
            if (mergedRange.IsAnyCellMerged)
            {
                throw new ArgumentException("New merged range can't overlap with existing merged range.");
            }
            this.AddInternal(mergedRange);
            CellStyle style1 = new CellStyle();
            MergedCellRanges.ResolveBorder(style1, mergedRange, IndividualBorder.Top, 0, 0, 0, 1);
            MergedCellRanges.ResolveBorder(style1, mergedRange, IndividualBorder.Left, 0, 0, 1, 0);
            MergedCellRanges.ResolveBorder(style1, mergedRange, IndividualBorder.Bottom, mergedRange.Height - 1, 0, 0, 1);
            MergedCellRanges.ResolveBorder(style1, mergedRange, IndividualBorder.Right, 0, mergedRange.Width - 1, 1, 0);
            bool flag1 = false;
            CellStyle style2 = null;
            foreach (ExcelCell cell1 in mergedRange)
            {
                if (!flag1)
                {
                    if (!cell1.IsStyleDefault)
                    {
                        style2 = cell1.Style;
                    }
                    flag1 = true;
                }
                cell1.AddToMergedRange(mergedRange);
            }
            if ((mergedRange.Value == null) && (style2 != null))
            {
                mergedRange.Style = style2;
            }
            mergedRange.Style.Borders = style1.Borders;
        }

        internal void AddInternal(MergedCellRange mergedRange)
        {
            this.items.Add(mergedRange, mergedRange);
        }

        internal void Remove(MergedCellRange mergedRange)
        {
            this.items.Remove(mergedRange);
            foreach (ExcelCell cell1 in mergedRange)
            {
                cell1.RemoveFromMergedRange();
            }
        }

        private static void ResolveBorder(CellStyle bordersStyle, MergedCellRange mergedRange, IndividualBorder borderId, int row, int column, int rowInc, int colInc)
        {
            CellBorder border1 = bordersStyle.Borders[borderId];
            bool flag1 = true;
            while ((row < mergedRange.Height) && (column < mergedRange.Width))
            {
                CellBorder border2 = mergedRange[row, column].Style.Borders[borderId];
                if (flag1)
                {
                    border1.LineStyle = border2.LineStyle;
                    border1.LineColor = border2.LineColor;
                    flag1 = false;
                }
                else if ((border2.LineStyle != border1.LineStyle) || (border2.LineColor != border1.LineColor))
                {
                    border1.LineStyle = LineStyle.None;
                    border1.LineColor = Color.Empty;
                    return;
                }
                row += rowInc;
                column += colInc;
            }
        }


        // Properties
        public ICollection Values
        {
            get
            {
                return this.items.Values;
            }
        }


        // Fields
        private Hashtable items;
        private ExcelWorksheet parent;
    }
}

