namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Reflection;

    ///<summary>
    ///Collection of excel rows (<see cref="MB.WinEIDrive.Excel.ExcelRow">ExcelRow</see>).
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelRow" />
    internal sealed class ExcelRowCollection : ExcelRowColumnCollectionBase
    {
        // Methods
        internal ExcelRowCollection(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelRowCollection(ExcelWorksheet parent, ExcelRowCollection sourceRows) : base(parent)
        {
            base.MaxOutlineLevel = sourceRows.MaxOutlineLevel;
            foreach (ExcelRow row1 in sourceRows)
            {
                base.Items.Add(new ExcelRow(this, row1));
            }
        }

        private void AdjustArraySize(int index)
        {
            if (index > (base.Items.Count - 1))
            {
                ExcelRowCollection.ExceptionIfRowOutOfRange(index);
                int num2 = index - (base.Items.Count - 1);
                for (int num1 = 0; num1 < num2; num1++)
                {
                    base.Items.Add(new ExcelRow(this, base.Items.Count));
                }
            }
        }

        internal void DeleteInternal(int rowIndex)
        {
            base.Items.RemoveAt(rowIndex);
            this.FixAllIndexes(rowIndex, -1);
        }

        internal static void ExceptionIfRowOutOfRange(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be negative.");
            }
            int num1 = 0xffff;
            if (index > num1)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be larger than maximum row index (" + num1 + ").");
            }
        }

        private void FixAllIndexes(int rowIndex, int offset)
        {
            this.FixRowIndexes(rowIndex, offset);
            this.FixMergedRegionsIndexes(rowIndex, offset);
            this.FixPageBreaksIndexes(rowIndex, offset);
        }

        private void FixMergedRegionsIndexes(int rowIndex, int offset)
        {
            MergedCellRanges ranges1 = base.Parent.MergedRanges;
            MergedCellRanges ranges2 = new MergedCellRanges(base.Parent);
            foreach (MergedCellRange range1 in ranges1.Values)
            {
                range1.FixRowIndexes(rowIndex, offset);
                if ((range1.Height >= 1) && ((range1.Height != 1) || (range1.Width != 1)))
                {
                    ranges2.AddInternal(range1);
                }
            }
            base.Parent.MergedRanges = ranges2;
        }

        private void FixPageBreaksIndexes(int rowIndex, int offset)
        {
            HorizontalPageBreakCollection collection1 = base.Parent.horizontalPageBreaks;
            HorizontalPageBreakCollection collection2 = new HorizontalPageBreakCollection();
            foreach (HorizontalPageBreak break1 in collection1)
            {
                if (break1.FixRowIndexes(rowIndex, offset))
                {
                    collection2.Add(break1);
                }
            }
            base.Parent.horizontalPageBreaks = collection2;
        }

        private void FixRowIndexes(int rowIndex, int offset)
        {
            for (int num1 = rowIndex; num1 < base.Items.Count; num1++)
            {
                ExcelRow row1 = (ExcelRow) base.Items[num1];
                row1.Index += offset;
            }
        }

        internal void InsertInternal(int rowIndex, int rowCount, ExcelRow sourceRow)
        {
            ArrayList list1 = new ArrayList();
            if (sourceRow != null)
            {
                foreach (ExcelCell cell1 in sourceRow.AllocatedCells)
                {
                    CellRange range1 = cell1.MergedRange;
                    if (((range1 != null) && (range1.FirstRowIndex == sourceRow.Index)) && (range1.LastRowIndex == sourceRow.Index))
                    {
                        list1.Add(range1);
                    }
                }
            }
            this.FixAllIndexes(rowIndex, rowCount);
            for (int num1 = rowCount - 1; num1 >= 0; num1--)
            {
                ExcelRow row1;
                if (sourceRow != null)
                {
                    row1 = new ExcelRow(this, sourceRow);
                    row1.Index = rowIndex + num1;
                    foreach (CellRange range2 in list1)
                    {
                        row1.Cells.GetSubrangeAbsolute(row1.Index, range2.FirstColumnIndex, row1.Index, range2.LastColumnIndex).Merged = true;
                    }
                }
                else
                {
                    row1 = new ExcelRow(this, rowIndex + num1);
                }
                base.Items.Insert(rowIndex, row1);
            }
        }

        ///<summary>
        ///Converts row index (0, 1, ...) to row name ("1", "2", ...).
        ///</summary>
        ///<param name="rowIndex">Row index.</param>
        public static string RowIndexToName(int rowIndex)
        {
            int num1 = rowIndex + 1;
            return num1.ToString();
        }

        ///<summary>
        ///Converts row name ("1", "2", ...) to row index (0, 1, ...).
        ///</summary>
        ///<param name="name">Row name.</param>
        public static int RowNameToIndex(string name)
        {
            return (int.Parse(name) - 1);
        }


        // Properties
        ///<summary>
        ///Gets the row with the specified name.
        ///</summary>
        ///<param name="name">The name of the row.</param>
        ///<example> Look at following code for cell referencing examples:
        ///<code lang="Visual Basic">
        ///Dim ws As ExcelWorksheet = excelFile.Worksheets.ActiveWorksheet
        ///
        ///ws.Cells("B2").Value = "Cell B2."
        ///ws.Cells(6, 0).Value = "Cell in row 7 and column A."
        ///
        ///ws.Rows(2).Cells(0).Value = "Cell in row 3 and column A."
        ///ws.Rows("4").Cells("B").Value = "Cell in row 4 and column B."
        ///
        ///ws.Columns(2).Cells(4).Value = "Cell in column C and row 5."
        ///ws.Columns("AA").Cells("6").Value = "Cell in AA column and row 6."
        ///</code>
        ///<code lang="C#">
        ///ExcelWorksheet ws = excelFile.Worksheets.ActiveWorksheet;
        ///
        ///ws.Cells["B2"].Value = "Cell B2.";
        ///ws.Cells[6,0].Value = "Cell in row 7 and column A.";
        ///
        ///ws.Rows[2].Cells[0].Value = "Cell in row 3 and column A.";
        ///ws.Rows["4"].Cells["B"].Value = "Cell in row 4 and column B.";
        ///
        ///ws.Columns[2].Cells[4].Value = "Cell in column C and row 5.";
        ///ws.Columns["AA"].Cells["6"].Value = "Cell in AA column and row 6.";
        ///</code>
        ///</example>
        public ExcelRow this[string name]
        {
            get
            {
                return this[ExcelRowCollection.RowNameToIndex(name)];
            }
        }

        ///<overloads>Gets the row with the specified index or name.</overloads>
        ///<summary>
        ///Gets the row with the specified index.
        ///</summary>
        ///<param name="index">The zero-based index of the row.</param>
        ///<example> Look at following code for cell referencing examples:
        ///<code lang="Visual Basic">
        ///Dim ws As ExcelWorksheet = excelFile.Worksheets.ActiveWorksheet
        ///
        ///ws.Cells("B2").Value = "Cell B2."
        ///ws.Cells(6, 0).Value = "Cell in row 7 and column A."
        ///
        ///ws.Rows(2).Cells(0).Value = "Cell in row 3 and column A."
        ///ws.Rows("4").Cells("B").Value = "Cell in row 4 and column B."
        ///
        ///ws.Columns(2).Cells(4).Value = "Cell in column C and row 5."
        ///ws.Columns("AA").Cells("6").Value = "Cell in AA column and row 6."
        ///</code>
        ///<code lang="C#">
        ///ExcelWorksheet ws = excelFile.Worksheets.ActiveWorksheet;
        ///
        ///ws.Cells["B2"].Value = "Cell B2.";
        ///ws.Cells[6,0].Value = "Cell in row 7 and column A.";
        ///
        ///ws.Rows[2].Cells[0].Value = "Cell in row 3 and column A.";
        ///ws.Rows["4"].Cells["B"].Value = "Cell in row 4 and column B.";
        ///
        ///ws.Columns[2].Cells[4].Value = "Cell in column C and row 5.";
        ///ws.Columns["AA"].Cells["6"].Value = "Cell in AA column and row 6.";
        ///</code>
        ///</example>
        public ExcelRow this[int index]
        {
            get
            {
                this.AdjustArraySize(index);
                return (ExcelRow) base.Items[index];
            }
        }

    }
}

