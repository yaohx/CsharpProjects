namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Excel column contains column options and cell range with column cells.
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelRow" />
    internal sealed class ExcelColumn : ExcelColumnRowBase
    {
        // Methods
        internal ExcelColumn(ExcelColumnCollection parent, ExcelColumn sourceColumn) : base(parent, sourceColumn)
        {
            this.width = -1;
            this.width = sourceColumn.width;
            this.hidden = sourceColumn.hidden;
        }

        internal ExcelColumn(ExcelColumnCollection parent, int index) : base(parent, index)
        {
            this.width = -1;
        }


        // Properties
        ///<summary>
        ///Gets cell range with column cells.
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
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell" />
        public CellRange Cells
        {
            get
            {
                if (this.cells == null)
                {
                    this.cells = new CellRange(base.Parent.Parent, 0, base.Index, 0xffff, base.Index);
                }
                return this.cells;
            }
        }

        ///<summary>
        ///Gets or sets whether column is hidden.
        ///</summary>
        public bool Hidden
        {
            get
            {
                return this.hidden;
            }
            set
            {
                this.hidden = value;
            }
        }

        ///<summary>
        ///Gets or sets column width.
        ///</summary>
        ///<remarks>
        ///Unit is 1/256th of the width of the zero character in default font.
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelWorksheet.DefaultColumnWidth" />
        public int Width
        {
            get
            {
                if (this.width != -1)
                {
                    return this.width;
                }
                return base.Parent.Parent.DefaultColumnWidth;
            }
            set
            {
                this.width = value;
            }
        }


        // Fields
        private CellRange cells;
        private bool hidden;
        private int width;
    }
}

