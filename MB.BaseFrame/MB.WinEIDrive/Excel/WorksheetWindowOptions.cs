namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum WorksheetWindowOptions : short
    {
        // Fields
        ///<summary>
        ///If set, MS Excel shows columns from right to left.
        ///</summary>
        ColumnsFromRightToLeft = 0x40,
        ///<summary>
        ///If set, MS Excel uses default grid line color.
        ///</summary>
        DefaultGridLineColor = 0x20,
        ///<summary>
        ///If set, MS Excel removes splits if pane freeze is removed.
        ///</summary>
        FrozenNoSplit = 0x100,
        ///<summary>
        ///If set, panes are frozen in MS Excel.
        ///</summary>
        FrozenPanes = 8,
        ///<summary>
        ///Set if sheet is selected in MS Excel.
        ///</summary>
        SheetSelected = 0x200,
        ///<summary>
        ///Set if sheet is visible in MS Excel.
        ///</summary>
        SheetVisible = 0x400,
        ///<summary>
        ///If set, MS Excel shows formulas. Otherwise, formula results are shown.
        ///</summary>
        ShowFormulas = 1,
        ///<summary>
        ///If set, MS Excel shows grid lines.
        ///</summary>
        ShowGridLines = 2,
        ///<summary>
        ///If set, MS Excel shows worksheet in page break preview. Otherwise, normal view is used.
        ///</summary>
        ShowInPageBreakPreview = 0x800,
        ///<summary>
        ///If set, MS Excel shows outline symbols.
        ///</summary>
        ShowOutlineSymbols = 0x80,
        ///<summary>
        ///If set, MS Excel shows row and column headers.
        ///</summary>
        ShowSheetHeaders = 4,
        ///<summary>
        ///If set, MS Excel shows zero values. Otherwise, zero values are shown as empty cells.
        ///</summary>
        ShowZeroValues = 0x10
    }
}

