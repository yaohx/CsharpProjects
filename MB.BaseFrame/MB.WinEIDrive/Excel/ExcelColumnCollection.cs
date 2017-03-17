namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Reflection;

    ///<summary>
    ///Collection of excel columns (<see cref="MB.WinEIDrive.Excel.ExcelColumn">ExcelColumn</see>).
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumn" />
    internal sealed class ExcelColumnCollection : ExcelRowColumnCollectionBase
    {
        // Methods
        internal ExcelColumnCollection(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelColumnCollection(ExcelWorksheet parent, ExcelColumnCollection sourceColumns) : base(parent)
        {
            base.MaxOutlineLevel = sourceColumns.MaxOutlineLevel;
            foreach (ExcelColumn column1 in sourceColumns)
            {
                base.Items.Add(new ExcelColumn(this, column1));
            }
        }

        private void AdjustArraySize(int index)
        {
            if (index > (base.Items.Count - 1))
            {
                ExcelColumnCollection.ExceptionIfColumnOutOfRange(index);
                int num2 = index - (base.Items.Count - 1);
                for (int num1 = 0; num1 < num2; num1++)
                {
                    base.Items.Add(new ExcelColumn(this, base.Items.Count));
                }
            }
        }

        ///<summary>
        ///Converts column index (0, 1, ...) to column name ("A", "B", ...).
        ///</summary>
        ///<param name="columnIndex">Column index.</param>
        public static string ColumnIndexToName(int columnIndex)
        {
            char ch1 = (char) ((ushort) (0x41 + (columnIndex % 0x1a)));
            columnIndex /= 0x1a;
            if (columnIndex == 0)
            {
                return ch1.ToString();
            }
            char ch2 = (char) ((ushort) (0x40 + columnIndex));
            return (ch2.ToString() + ch1.ToString());
        }

        ///<summary>
        ///Converts column name ("A", "B", ...) to column index (0, 1, ...).
        ///</summary>
        ///<param name="name">Column name.</param>
        public static int ColumnNameToIndex(string name)
        {
            int num1 = -1;
            if (name.Length == 1)
            {
                num1 = ExcelColumnCollection.GetLetterIndex(name[0]);
            }
            else if (name.Length == 2)
            {
                num1 = ((ExcelColumnCollection.GetLetterIndex(name[0]) + 1) * 0x1a) + ExcelColumnCollection.GetLetterIndex(name[1]);
            }
            if ((num1 < 0) || (num1 > 0xff))
            {
                throw new ArgumentOutOfRangeException("name", name, "Column name must be one-letter or two-letter name from A to IV.");
            }
            return num1;
        }

        internal static void ExceptionIfColumnOutOfRange(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be negative.");
            }
            int num1 = 0xff;
            if (index > num1)
            {
                throw new ArgumentOutOfRangeException("index", index, "Index can't be lager than maximum column index (" + num1 + ").");
            }
        }

        private static int GetLetterIndex(char letter)
        {
            int num1 = char.ToUpper(letter) - 'A';
            if ((num1 < 0) || (num1 > 0x19))
            {
                throw new ArgumentOutOfRangeException("letter", letter, "Column name must be made from valid letters of English alphabet.");
            }
            return num1;
        }


        // Properties
        ///<summary>
        ///Gets the column with the specified name.
        ///</summary>
        ///<param name="name">The name of the column.</param>
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
        public ExcelColumn this[string name]
        {
            get
            {
                return this[ExcelColumnCollection.ColumnNameToIndex(name)];
            }
        }

        ///<overloads>Gets the column with the specified index or name.</overloads>
        ///<summary>
        ///Gets the column with the specified index.
        ///</summary>
        ///<param name="index">The zero-based index of the column.</param>
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
        public ExcelColumn this[int index]
        {
            get
            {
                this.AdjustArraySize(index);
                return (ExcelColumn) base.Items[index];
            }
        }

    }
}

