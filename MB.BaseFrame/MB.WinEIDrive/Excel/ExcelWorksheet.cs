namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Excel worksheet is a table with additional properties, identified by a unique name.
    ///</summary>
    ///<remarks>
    ///<p>
    ///Worksheet in Microsoft Excel has limited size.
    ///Number of rows (<see cref="MB.WinEIDrive.Excel.ExcelRow">ExcelRow</see>) is limited
    ///to <see cref="MB.WinEIDrive.Excel.ExcelFile.MaxRows">ExcelFile.MaxRows</see>.
    ///Number of columns (<see cref="MB.WinEIDrive.Excel.ExcelColumn">ExcelColumn</see>) is limited
    ///to <see cref="MB.WinEIDrive.Excel.ExcelFile.MaxColumns">ExcelFile.MaxColumns</see>.
    ///A specific cell (<see cref="MB.WinEIDrive.Excel.ExcelCell">ExcelCell</see>) can be accessed either trough
    ///<see cref="MB.WinEIDrive.Excel.ExcelRow.Cells">ExcelRow.Cells</see>,
    ///<see cref="MB.WinEIDrive.Excel.ExcelColumn.Cells">ExcelColumn.Cells</see> or
    ///<see cref="MB.WinEIDrive.Excel.ExcelWorksheet.Cells">ExcelWorksheet.Cells</see> property.
    ///Whichever property used, there are two distinct methods of getting a cell reference; using <b>name</b>
    ///and using <b>index</b>. For example, full name of cell in top left corner of a worksheet is "A1". Translated
    ///to indexes, same cell would be 0,0 (zero row and zero column). If using
    ///<see cref="MB.WinEIDrive.Excel.ExcelRow.Cells">ExcelRow.Cells</see> or
    ///<see cref="MB.WinEIDrive.Excel.ExcelColumn.Cells">ExcelColumn.Cells</see> to access a
    ///specific cell, only partial name or partial index must be used, providing unknown column or row information.
    ///</p>
    ///</remarks>
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
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelRow" />
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumn" />
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell" />
    internal sealed class ExcelWorksheet
    {
        // Methods
        internal ExcelWorksheet(string name, ExcelWorksheetCollection parent)
        {
            this.defaultColumnWidth = 0x924;
            this.outlineRowButtonsBelow = true;
            this.outlineColumnButtonsRight = true;
            this.pageBreakViewZoom = 60;
            this.zoom = 100;
            this.windowOptions = WorksheetWindowOptions.ShowOutlineSymbols | (WorksheetWindowOptions.DefaultGridLineColor | (WorksheetWindowOptions.ShowGridLines | (WorksheetWindowOptions.ShowZeroValues | WorksheetWindowOptions.ShowSheetHeaders)));
            this.paperSize = 0;
            this.scalingFactor = 0xff;
            this.startPageNumber = 1;
            this.fitWorksheetWidthToPages = 0;
            this.fitWorksheetHeightToPages = 0;
            this.setupOptions = SetupOptions.Landscape;
            this.printResolution = 0;
            this.verticalPrintResolution = 0;
            this.headerMargin = 0.5;
            this.footerMargin = 0.5;
            this.numberOfCopies = 1;
            this.name = name;
            this.parent = parent;
            this.rows = new ExcelRowCollection(this);
            this.columns = new ExcelColumnCollection(this);
            this.mergedRanges = new MergedCellRanges(this);
            this.horizontalPageBreaks = new HorizontalPageBreakCollection();
            this.verticalPageBreaks = new VerticalPageBreakCollection();
        }

        internal ExcelWorksheet(string name, ExcelWorksheetCollection parent, ExcelWorksheet sourceWorksheet)
        {
            this.defaultColumnWidth = 0x924;
            this.outlineRowButtonsBelow = true;
            this.outlineColumnButtonsRight = true;
            this.pageBreakViewZoom = 60;
            this.zoom = 100;
            this.windowOptions = WorksheetWindowOptions.ShowOutlineSymbols | (WorksheetWindowOptions.DefaultGridLineColor | (WorksheetWindowOptions.ShowGridLines | (WorksheetWindowOptions.ShowZeroValues | WorksheetWindowOptions.ShowSheetHeaders)));
            this.paperSize = 0;
            this.scalingFactor = 0xff;
            this.startPageNumber = 1;
            this.fitWorksheetWidthToPages = 0;
            this.fitWorksheetHeightToPages = 0;
            this.setupOptions = SetupOptions.Landscape;
            this.printResolution = 0;
            this.verticalPrintResolution = 0;
            this.headerMargin = 0.5;
            this.footerMargin = 0.5;
            this.numberOfCopies = 1;
            this.name = name;
            this.parent = parent;
            this.protectedWorksheet = sourceWorksheet.protectedWorksheet;
            this.rows = new ExcelRowCollection(this, sourceWorksheet.rows);
            this.columns = new ExcelColumnCollection(this, sourceWorksheet.columns);
            this.defaultColumnWidth = sourceWorksheet.defaultColumnWidth;
            this.mergedRanges = new MergedCellRanges(this, sourceWorksheet.mergedRanges);
            this.outlineRowButtonsBelow = sourceWorksheet.outlineRowButtonsBelow;
            this.outlineColumnButtonsRight = sourceWorksheet.outlineColumnButtonsRight;
            if (sourceWorksheet.PreservedWorksheetRecords != null)
            {
                this.PreservedWorksheetRecords = new PreservedRecords(sourceWorksheet.PreservedWorksheetRecords);
            }
            this.windowOptions = sourceWorksheet.windowOptions & ((WorksheetWindowOptions) (-1537));
            this.firstVisibleRow = sourceWorksheet.firstVisibleRow;
            this.firstVisibleColumn = sourceWorksheet.firstVisibleColumn;
            this.pageBreakViewZoom = sourceWorksheet.pageBreakViewZoom;
            this.zoom = sourceWorksheet.zoom;
            this.horizontalPageBreaks = new HorizontalPageBreakCollection(sourceWorksheet.horizontalPageBreaks);
            this.verticalPageBreaks = new VerticalPageBreakCollection(sourceWorksheet.verticalPageBreaks);
            this.paperSize = sourceWorksheet.paperSize;
            this.scalingFactor = sourceWorksheet.scalingFactor;
            this.startPageNumber = sourceWorksheet.startPageNumber;
            this.fitWorksheetWidthToPages = sourceWorksheet.fitWorksheetWidthToPages;
            this.fitWorksheetHeightToPages = sourceWorksheet.fitWorksheetHeightToPages;
            this.setupOptions = sourceWorksheet.setupOptions;
            this.printResolution = sourceWorksheet.printResolution;
            this.verticalPrintResolution = sourceWorksheet.verticalPrintResolution;
            this.headerMargin = sourceWorksheet.headerMargin;
            this.footerMargin = sourceWorksheet.footerMargin;
            this.numberOfCopies = sourceWorksheet.numberOfCopies;
            this.namedRanges = new NamedRangeCollection(this, sourceWorksheet.NamedRanges);
        }

        ///<summary>
        ///Deletes this worksheet from the workbook.
        ///</summary>
        public void Delete()
        {
            this.parent.DeleteInternal(this);
        }

        private bool GetWindowOption(WorksheetWindowOptions option)
        {
            return ((this.windowOptions & option) != ((WorksheetWindowOptions) ((short) 0)));
        }

        ///<summary>
        ///Inserts a copy of an existing worksheet before the current worksheet.
        ///</summary>
        ///<param name="destinationWorksheetName">Name of the new worksheet.</param>
        ///<param name="sourceWorksheet">Source worksheet.</param>
        ///<returns>Newly created worksheet.</returns>
        public ExcelWorksheet InsertCopy(string destinationWorksheetName, ExcelWorksheet sourceWorksheet)
        {
            return this.parent.InsertCopyInternal(destinationWorksheetName, this.parent.IndexOf(this), sourceWorksheet);
        }

        ///<summary>
        ///Inserts an empty worksheet before the current worksheet.
        ///</summary>
        ///<param name="worksheetName">Worksheet name.</param>
        ///<returns>Newly created worksheet.</returns>
        public ExcelWorksheet InsertEmpty(string worksheetName)
        {
            return this.parent.InsertInternal(worksheetName, this.parent.IndexOf(this));
        }

        private void SetWindowOption(bool val, WorksheetWindowOptions option)
        {
            this.windowOptions &= ~option;
            if (val)
            {
                this.windowOptions |= option;
            }
        }


        // Properties
        ///<summary>
        ///Scaling factor for automatic page breaks.
        ///</summary>
        ///<remarks>
        ///<p>Unit is one percent. Value must be between 10 and 400.</p>
        ///<p>Default value for this property is 255.</p>
        ///<p>MS Excel inserts automatic page breaks depending on this scaling factor.
        ///Smaller it gets, bigger will be the distance between the two automatic page breaks.</p>
        ///</remarks>
        ///<exception cref="System.ArgumentOutOfRangeException">Thrown if value is out of 10 to 400 range.</exception>
        internal int AutomaticPageBreakScalingFactor
        {
            get
            {
                return this.scalingFactor;
            }
            set
            {
                if ((value < 10) || (value > 400))
                {
                    throw new ArgumentOutOfRangeException("value", value, "AutomaticPageBreakScalingFactor must be in range from 10 to 400.");
                }
                this.scalingFactor = value;
            }
        }

        ///<summary>
        ///Gets <see cref="MB.WinEIDrive.Excel.CellRange">CellRange</see> with all the cells
        ///(<see cref="MB.WinEIDrive.Excel.ExcelCell">ExcelCell</see>)
        ///in the worksheet.
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
        public CellRange Cells
        {
            get
            {
                if (this.cells == null)
                {
                    this.cells = new CellRange(this);
                }
                return this.cells;
            }
        }

        ///<summary>
        ///Gets collection of all columns (<see cref="MB.WinEIDrive.Excel.ExcelColumn">ExcelColumn</see>) in the worksheet.
        ///</summary>
        public ExcelColumnCollection Columns
        {
            get
            {
                return this.columns;
            }
        }

        ///<summary>
        ///Gets or sets default column width.
        ///</summary>
        ///<remarks>
        ///Unit is 1/256th of the width of the zero character in default font. This value is used as width for columns
        ///which don't have <see cref="MB.WinEIDrive.Excel.ExcelColumn.Width">ExcelColumn.Width</see> property explicitly set.
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumn.Width">ExcelColumn.Width</seealso>
        public int DefaultColumnWidth
        {
            get
            {
                return this.defaultColumnWidth;
            }
            set
            {
                this.defaultColumnWidth = value;
            }
        }

        ///<summary>
        ///Index of the first visible column in the worksheet.
        ///</summary>
        ///<remarks>
        ///Default value for this property is 0.
        ///</remarks>
        public int FirstVisibleColumn
        {
            get
            {
                return this.firstVisibleColumn;
            }
            set
            {
                this.firstVisibleColumn = value;
            }
        }

        ///<summary>
        ///Index of the first visible row in the worksheet.
        ///</summary>
        ///<remarks>
        ///Default value for this property is 0.
        ///</remarks>
        public int FirstVisibleRow
        {
            get
            {
                return this.firstVisibleRow;
            }
            set
            {
                this.firstVisibleRow = value;
            }
        }

        ///<summary>
        ///Gets collection of all horizontal page breaks
        ///(<see cref="MB.WinEIDrive.Excel.HorizontalPageBreak">HorizontalPageBreak</see>) in the worksheet.
        ///</summary>
        public HorizontalPageBreakCollection HorizontalPageBreaks
        {
            get
            {
                return this.horizontalPageBreaks;
            }
        }

        internal MergedCellRanges MergedRanges
        {
            get
            {
                return this.mergedRanges;
            }
            set
            {
                this.mergedRanges = value;
            }
        }

        ///<summary>
        ///Gets or sets worksheet name.
        ///</summary>
        ///<remarks>
        ///If not unique (worksheet with that name already exists in
        ///<see cref="MB.WinEIDrive.Excel.ExcelFile.Worksheets">ExcelFile.Worksheets</see> collection) exception is thrown.
        ///</remarks>
        ///<exception cref="System.ArgumentException">Thrown if worksheet name is not unique.</exception>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.parent.ExceptionIfNotUnique(value);
                this.name = value;
            }
        }

        ///<summary>
        ///Gets <seealso cref="MB.WinEIDrive.Excel.NamedRangeCollection">NamedRangeCollection</seealso>
        ///containing descriptive names which are used to represent cells, ranges of cells,
        ///formulas, or constant values.
        ///</summary>
        ///<remarks>
        ///You can use the labels of columns and rows on a worksheet to refer to the cells within
        ///those columns and rows. Or you can create descriptive names to represent cells, ranges of cells,
        ///formulas, or constant values. Labels can be used in formulas that refer to data on the same
        ///worksheet; if you want to represent a range on another worksheet, use a name.
        ///You can also create 3-D names that represent the same cell or range of cells across multiple worksheets.
        ///</remarks>
        ///<example>Following code demonstrates how to use formulas and named ranges. It shows next features:
        ///cell references (both absolute and relative), unary and binary operators, constand operands (integer and floating point),
        ///functions and named cell ranges.
        ///<code lang="Visual Basic">
        ///ws.Cells("A1").Value = 5
        ///ws.Cells("A2").Value = 6
        ///ws.Cells("A3").Value = 10
        ///
        ///ws.Cells("C1").Formula = "=A1+A2"
        ///ws.Cells("C2").Formula = "=$A$1-A3"
        ///ws.Cells("C3").Formula = "=COUNT(A1:A3)"
        ///ws.Cells("C4").Formula = "=AVERAGE($A$1:$A$3)"
        ///ws.Cells("C5").Formula = "=SUM(A1:A3,2,3)"
        ///ws.Cells("C7").Formula = "= 123 - (-(-(23.5)))"
        ///
        ///ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"))
        ///ws.Cells("C8").Formula = "=MAX(DataRange)"
        ///
        ///Dim cr As CellRange = ws.Cells.GetSubrange("B9","C10")
        ///cr.Merged = True
        ///cr.Formula = "=A1*25"
        ///</code>
        ///<code lang="C#">
        ///ws.Cells["A1"].Value = 5;
        ///ws.Cells["A2"].Value = 6;
        ///ws.Cells["A3"].Value = 10;
        ///
        ///ws.Cells["C1"].Formula = "=A1+A2";
        ///ws.Cells["C2"].Formula = "=$A$1-A3";
        ///ws.Cells["C3"].Formula = "=COUNT(A1:A3)";
        ///ws.Cells["C4"].Formula = "=AVERAGE($A$1:$A$3)";
        ///ws.Cells["C5"].Formula = "=SUM(A1:A3,2,3)";
        ///ws.Cells["C7"].Formula = "= 123 - (-(-(23.5)))";
        ///
        ///ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"));
        ///ws.Cells["C8"].Formula = "=MAX(DataRange)";
        ///
        ///CellRange cr = ws.Cells.GetSubrange("B9", "C10");
        ///cr.Merged = true;
        ///cr.Formula = "=A1*25";
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell.Formula">ExcelCell.Formula</seealso>
        public NamedRangeCollection NamedRanges
        {
            get
            {
                if (this.namedRanges == null)
                {
                    this.namedRanges = new NamedRangeCollection(this);
                }
                return this.namedRanges;
            }
        }

        ///<summary>
        ///Gets or sets whether outline column buttons are displayed on the right side of groups.
        ///</summary>
        ///<remarks>
        ///This property is simply written to Excel file and has no effect on behavior of this library.
        ///For more information on worksheet protection, consult Microsoft Excel documentation.
        ///</remarks>
        ///<example> Following code creates two horizontal groups and one vertical group. Horizontal groups have
        ///outline button above (default is below), while vertical group is collapsed.
        ///<code lang="Visual Basic">
        ///Sub GroupingSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Grouping and outline example:"
        ///
        ///<font color="Green">' Vertical grouping.</font>
        ///ws.Cells(2, 0).Value = "GroupA Start"
        ///ws.Rows(2).OutlineLevel = 1
        ///ws.Cells(3, 0).Value = "A"
        ///ws.Rows(3).OutlineLevel = 1
        ///ws.Cells(4, 1).Value = "GroupB Start"
        ///ws.Rows(4).OutlineLevel = 2
        ///ws.Cells(5, 1).Value = "B"
        ///ws.Rows(5).OutlineLevel = 2
        ///ws.Cells(6, 1).Value = "GroupB End"
        ///ws.Rows(6).OutlineLevel = 2
        ///ws.Cells(7, 0).Value = "GroupA End"
        ///ws.Rows(7).OutlineLevel = 1
        ///<font color="Green">' Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = False
        ///
        ///<font color="Green">' Horizontal grouping (collapsed).</font>
        ///ws.Cells("E2").Value = "Gr.C Start"
        ///ws.Columns("E").OutlineLevel = 1
        ///ws.Columns("E").Collapsed = True
        ///ws.Cells("F2").Value = "C"
        ///ws.Columns("F").OutlineLevel = 1
        ///ws.Columns("F").Collapsed = True
        ///ws.Cells("G2").Value = "Gr.C End"
        ///ws.Columns("G").OutlineLevel = 1
        ///ws.Columns("G").Collapsed = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void GroupingSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Grouping and outline example:";
        ///
        ///<font color="Green">// Vertical grouping.</font>
        ///ws.Cells[2,0].Value = "GroupA Start";
        ///ws.Rows[2].OutlineLevel = 1;
        ///ws.Cells[3,0].Value = "A";
        ///ws.Rows[3].OutlineLevel = 1;
        ///ws.Cells[4,1].Value = "GroupB Start";
        ///ws.Rows[4].OutlineLevel = 2;
        ///ws.Cells[5,1].Value = "B";
        ///ws.Rows[5].OutlineLevel = 2;
        ///ws.Cells[6,1].Value = "GroupB End";
        ///ws.Rows[6].OutlineLevel = 2;
        ///ws.Cells[7,0].Value = "GroupA End";
        ///ws.Rows[7].OutlineLevel = 1;
        ///<font color="Green">// Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = false;
        ///
        ///<font color="Green">// Horizontal grouping (collapsed).</font>
        ///ws.Cells["E2"].Value = "Gr.C Start";
        ///ws.Columns["E"].OutlineLevel = 1;
        ///ws.Columns["E"].Collapsed = true;
        ///ws.Cells["F2"].Value = "C";
        ///ws.Columns["F"].OutlineLevel = 1;
        ///ws.Columns["F"].Collapsed = true;
        ///ws.Cells["G2"].Value = "Gr.C End";
        ///ws.Columns["G"].OutlineLevel = 1;
        ///ws.Columns["G"].Collapsed = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelWorksheet.OutlineRowButtonsBelow">ExcelWorksheet.OutlineRowButtonsBelow</seealso>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumnRowBase.Collapsed" />
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumnRowBase.OutlineLevel" />
        public bool OutlineColumnButtonsRight
        {
            get
            {
                return this.outlineColumnButtonsRight;
            }
            set
            {
                this.outlineColumnButtonsRight = value;
            }
        }

        ///<summary>
        ///Gets or sets whether outline row buttons are displayed below groups.
        ///</summary>
        ///<remarks>
        ///This property is simply written to Excel file and has no effect on behavior of this library.
        ///For more information on worksheet protection, consult Microsoft Excel documentation.
        ///</remarks>
        ///<example> Following code creates two horizontal groups and one vertical group. Horizontal groups have
        ///outline button above (default is below), while vertical group is collapsed.
        ///<code lang="Visual Basic">
        ///Sub GroupingSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Grouping and outline example:"
        ///
        ///<font color="Green">' Vertical grouping.</font>
        ///ws.Cells(2, 0).Value = "GroupA Start"
        ///ws.Rows(2).OutlineLevel = 1
        ///ws.Cells(3, 0).Value = "A"
        ///ws.Rows(3).OutlineLevel = 1
        ///ws.Cells(4, 1).Value = "GroupB Start"
        ///ws.Rows(4).OutlineLevel = 2
        ///ws.Cells(5, 1).Value = "B"
        ///ws.Rows(5).OutlineLevel = 2
        ///ws.Cells(6, 1).Value = "GroupB End"
        ///ws.Rows(6).OutlineLevel = 2
        ///ws.Cells(7, 0).Value = "GroupA End"
        ///ws.Rows(7).OutlineLevel = 1
        ///<font color="Green">' Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = False
        ///
        ///<font color="Green">' Horizontal grouping (collapsed).</font>
        ///ws.Cells("E2").Value = "Gr.C Start"
        ///ws.Columns("E").OutlineLevel = 1
        ///ws.Columns("E").Collapsed = True
        ///ws.Cells("F2").Value = "C"
        ///ws.Columns("F").OutlineLevel = 1
        ///ws.Columns("F").Collapsed = True
        ///ws.Cells("G2").Value = "Gr.C End"
        ///ws.Columns("G").OutlineLevel = 1
        ///ws.Columns("G").Collapsed = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void GroupingSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Grouping and outline example:";
        ///
        ///<font color="Green">// Vertical grouping.</font>
        ///ws.Cells[2,0].Value = "GroupA Start";
        ///ws.Rows[2].OutlineLevel = 1;
        ///ws.Cells[3,0].Value = "A";
        ///ws.Rows[3].OutlineLevel = 1;
        ///ws.Cells[4,1].Value = "GroupB Start";
        ///ws.Rows[4].OutlineLevel = 2;
        ///ws.Cells[5,1].Value = "B";
        ///ws.Rows[5].OutlineLevel = 2;
        ///ws.Cells[6,1].Value = "GroupB End";
        ///ws.Rows[6].OutlineLevel = 2;
        ///ws.Cells[7,0].Value = "GroupA End";
        ///ws.Rows[7].OutlineLevel = 1;
        ///<font color="Green">// Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = false;
        ///
        ///<font color="Green">// Horizontal grouping (collapsed).</font>
        ///ws.Cells["E2"].Value = "Gr.C Start";
        ///ws.Columns["E"].OutlineLevel = 1;
        ///ws.Columns["E"].Collapsed = true;
        ///ws.Cells["F2"].Value = "C";
        ///ws.Columns["F"].OutlineLevel = 1;
        ///ws.Columns["F"].Collapsed = true;
        ///ws.Cells["G2"].Value = "Gr.C End";
        ///ws.Columns["G"].OutlineLevel = 1;
        ///ws.Columns["G"].Collapsed = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelWorksheet.OutlineColumnButtonsRight">ExcelWorksheet.OutlineColumnButtonsRight</seealso>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumnRowBase.Collapsed" />
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumnRowBase.OutlineLevel" />
        public bool OutlineRowButtonsBelow
        {
            get
            {
                return this.outlineRowButtonsBelow;
            }
            set
            {
                this.outlineRowButtonsBelow = value;
            }
        }

        ///<summary>
        ///Magnification factor in page break view.
        ///</summary>
        ///<remarks>
        ///<p>Unit is one percent. Value must be between 10 and 400.</p>
        ///<p>Default value for this property is 60.</p>
        ///</remarks>
        ///<exception cref="System.ArgumentOutOfRangeException">Thrown if value is out of 10 to 400 range.</exception>
        public int PageBreakViewZoom
        {
            get
            {
                return this.pageBreakViewZoom;
            }
            set
            {
                if ((value < 10) || (value > 400))
                {
                    throw new ArgumentOutOfRangeException("value", value, "PageBreakViewZoom must be in range from 10 to 400.");
                }
                this.pageBreakViewZoom = value;
            }
        }

        internal ExcelWorksheetCollection Parent
        {
            get
            {
                return this.parent;
            }
        }

        internal ExcelFile ParentExcelFile
        {
            get
            {
                return this.Parent.Parent;
            }
        }

        ///<summary>
        ///Gets or sets the worksheet protection flag.
        ///</summary>
        ///<remarks>
        ///This property is simply written to Excel file and has no effect on the behavior of this library.
        ///For more information on worksheet protection, consult Microsoft Excel documentation.
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.Protected">ExcelFile.Protected</seealso>
        public bool Protected
        {
            get
            {
                return this.protectedWorksheet;
            }
            set
            {
                this.protectedWorksheet = value;
            }
        }

        ///<summary>
        ///Gets collection of all rows (<see cref="MB.WinEIDrive.Excel.ExcelRow">ExcelRow</see>) in the worksheet.
        ///</summary>
        public ExcelRowCollection Rows
        {
            get
            {
                return this.rows;
            }
        }

        ///<summary>
        ///If true, MS Excel shows columns from right to left.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <b>false</b>.
        ///</remarks>
        public bool ShowColumnsFromRightToLeft
        {
            get
            {
                return this.GetWindowOption(WorksheetWindowOptions.ColumnsFromRightToLeft);
            }
            set
            {
                this.SetWindowOption(value, WorksheetWindowOptions.ColumnsFromRightToLeft);
            }
        }

        ///<summary>
        ///If true, MS Excel shows formulas. Otherwise, formula results are shown.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <b>false</b>.
        ///</remarks>
        public bool ShowFormulas
        {
            get
            {
                return this.GetWindowOption(WorksheetWindowOptions.ShowFormulas);
            }
            set
            {
                this.SetWindowOption(value, WorksheetWindowOptions.ShowFormulas);
            }
        }

        ///<summary>
        ///If true, MS Excel shows grid lines.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <b>true</b>.
        ///</remarks>
        public bool ShowGridLines
        {
            get
            {
                return this.GetWindowOption(WorksheetWindowOptions.ShowGridLines);
            }
            set
            {
                this.SetWindowOption(value, WorksheetWindowOptions.ShowGridLines);
            }
        }

        ///<summary>
        ///If true, MS Excel shows worksheet in page break preview. Otherwise, normal view is used.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <b>false</b>.
        ///</remarks>
        public bool ShowInPageBreakPreview
        {
            get
            {
                return this.GetWindowOption(WorksheetWindowOptions.ShowInPageBreakPreview);
            }
            set
            {
                this.SetWindowOption(value, WorksheetWindowOptions.ShowInPageBreakPreview);
            }
        }

        ///<summary>
        ///If true, MS Excel shows outline symbols.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <b>true</b>.
        ///</remarks>
        public bool ShowOutlineSymbols
        {
            get
            {
                return this.GetWindowOption(WorksheetWindowOptions.ShowOutlineSymbols);
            }
            set
            {
                this.SetWindowOption(value, WorksheetWindowOptions.ShowOutlineSymbols);
            }
        }

        ///<summary>
        ///If true, MS Excel shows row and column headers.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <b>true</b>.
        ///</remarks>
        public bool ShowSheetHeaders
        {
            get
            {
                return this.GetWindowOption(WorksheetWindowOptions.ShowSheetHeaders);
            }
            set
            {
                this.SetWindowOption(value, WorksheetWindowOptions.ShowSheetHeaders);
            }
        }

        ///<summary>
        ///If true, MS Excel shows zero values. Otherwise, zero values are shown as empty cells.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <b>true</b>.
        ///</remarks>
        public bool ShowZeroValues
        {
            get
            {
                return this.GetWindowOption(WorksheetWindowOptions.ShowZeroValues);
            }
            set
            {
                this.SetWindowOption(value, WorksheetWindowOptions.ShowZeroValues);
            }
        }

        ///<summary>
        ///Gets collection of all vertical page breaks
        ///(<see cref="MB.WinEIDrive.Excel.VerticalPageBreak">VerticalPageBreak</see>) in the worksheet.
        ///</summary>
        public VerticalPageBreakCollection VerticalPageBreaks
        {
            get
            {
                return this.verticalPageBreaks;
            }
        }

        ///<summary>
        ///Magnification factor in normal view.
        ///</summary>
        ///<remarks>
        ///<p>Unit is one percent. Value must be between 10 and 400.</p>
        ///<p>Default value for this property is 100.</p>
        ///</remarks>
        ///<exception cref="System.ArgumentOutOfRangeException">Thrown if value is out of 10 to 400 range.</exception>
        public int Zoom
        {
            get
            {
                return this.zoom;
            }
            set
            {
                if ((value < 10) || (value > 400))
                {
                    throw new ArgumentOutOfRangeException("value", value, "Zoom must be in range from 10 to 400.");
                }
                this.zoom = value;
            }
        }


        // Fields
        private CellRange cells;
        private ExcelColumnCollection columns;
        private int defaultColumnWidth;
        private int firstVisibleColumn;
        private int firstVisibleRow;
        internal ushort fitWorksheetHeightToPages;
        internal ushort fitWorksheetWidthToPages;
        internal double footerMargin;
        internal double headerMargin;
        internal HorizontalPageBreakCollection horizontalPageBreaks;
        private MergedCellRanges mergedRanges;
        private string name;
        private NamedRangeCollection namedRanges;
        internal ushort numberOfCopies;
        private bool outlineColumnButtonsRight;
        private bool outlineRowButtonsBelow;
        private int pageBreakViewZoom;
        internal ushort paperSize;
        private ExcelWorksheetCollection parent;
        internal PreservedRecords PreservedWorksheetRecords;
        internal ushort printResolution;
        private bool protectedWorksheet;
        private ExcelRowCollection rows;
        internal int scalingFactor;
        internal SetupOptions setupOptions;
        internal ushort startPageNumber;
        private VerticalPageBreakCollection verticalPageBreaks;
        internal ushort verticalPrintResolution;
        internal WorksheetWindowOptions windowOptions;
        private int zoom;
    }
}

