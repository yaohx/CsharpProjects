namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Excel row contains row options and cell range with row cells.
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumn" />
    internal sealed class ExcelRow : ExcelColumnRowBase
    {
        // Methods
        internal ExcelRow(ExcelRowCollection parent, ExcelRow sourceRow) : base(parent, sourceRow)
        {
            this.height = 0xff;
            this.height = sourceRow.height;
            this.cellCollection = new ExcelCellCollection(parent.Parent, sourceRow.cellCollection);
        }

        internal ExcelRow(ExcelRowCollection parent, int index) : base(parent, index)
        {
            this.height = 0xff;
            this.cellCollection = new ExcelCellCollection(parent.Parent);
        }

        ///<summary>
        ///Deletes this row from the worksheet.
        ///</summary>
        public void Delete()
        {
            ((ExcelRowCollection) base.Parent).DeleteInternal(base.Index);
        }

        ///<summary>
        ///Inserts specified number of copied rows before the current row.
        ///</summary>
        ///<param name="rowCount">Number of rows to insert.</param>
        ///<param name="sourceRow">Source row to copy.</param>
        public void InsertCopy(int rowCount, ExcelRow sourceRow)
        {
            ((ExcelRowCollection) base.Parent).InsertInternal(base.Index, rowCount, sourceRow);
        }

        ///<summary>
        ///Inserts specified number of empty rows before the current row.
        ///</summary>
        ///<param name="rowCount">Number of rows to insert.</param>
        public void InsertEmpty(int rowCount)
        {
            ((ExcelRowCollection) base.Parent).InsertInternal(base.Index, rowCount, null);
        }


        // Properties
        ///<summary>
        ///Gets only currently allocated cells for this row.
        ///</summary>
        ///<remarks>
        ///<p>Use this collection if you are reading entire Excel file (you don't know exact position of
        ///cells with data). If writing values, using <see cref="MB.WinEIDrive.Excel.ExcelRow.Cells">Cells</see>
        ///property is recommended.</p>
        ///<p>This collection contains only allocated cells so it is faster as you avoid
        ///checking every single cell in a row. You still need to check if a specific cell contains
        ///any value (it can be empty).</p>
        ///</remarks>
        ///<example> Following code reads entire XLS file and displays all cells containing any data.
        ///Data types are also displayed.
        ///<code lang="Visual Basic">
        ///Dim ef As ExcelFile = New ExcelFile("..\TestWorkbook.xls")
        ///Dim sheet As ExcelWorksheet
        ///Dim row As ExcelRow
        ///Dim cell As ExcelCell
        ///
        ///For Each sheet In ef.Worksheets
        ///Console.WriteLine("--------- {0} ---------", sheet.Name)
        ///
        ///For Each row In sheet.Rows
        ///For Each cell In row.AllocatedCells
        ///If Not cell.Value Is Nothing Then
        ///Console.Write("{0}({1})", cell.Value, cell.Value.GetType().Name)
        ///End If
        ///
        ///Console.Write(vbTab)
        ///Next
        ///
        ///Console.WriteLine()
        ///Next
        ///Next
        ///</code>
        ///<code lang="C#">
        ///ExcelFile ef = new ExcelFile("..\\..\\TestWorkbook.xls");
        ///
        ///foreach(ExcelWorksheet sheet in ef.Worksheets)
        ///{
        ///Console.WriteLine("--------- {0} ---------", sheet.Name);
        ///
        ///foreach(ExcelRow row in sheet.Rows)
        ///{
        ///foreach(ExcelCell cell in row.AllocatedCells)
        ///{
        ///if(cell.Value != null)
        ///Console.Write("{0}({1})", cell.Value, cell.Value.GetType().Name);
        ///
        ///Console.Write("\t");
        ///}
        ///
        ///Console.WriteLine();
        ///}
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelRow.Cells" />
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell" />
        public ExcelCellCollection AllocatedCells
        {
            get
            {
                return this.cellCollection;
            }
        }

        ///<summary>
        ///Gets cell range with row cells.
        ///</summary>
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
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelRow.AllocatedCells" />
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell" />
        public CellRange Cells
        {
            get
            {
                if (this.cells == null)
                {
                    this.cells = new CellRange(base.Parent.Parent, base.Index, 0, base.Index, 0xff);
                }
                return this.cells;
            }
        }

        ///<summary>
        ///Gets or sets row height.
        ///</summary>
        ///<remarks>
        ///Unit is twip (1/20th of a point).
        ///</remarks>
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }


        // Fields
        private ExcelCellCollection cellCollection;
        private CellRange cells;
        internal const int DefaultRowHeight = 0xff;
        private int height;
    }
}

