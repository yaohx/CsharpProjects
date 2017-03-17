namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Indexing modes used by <see cref="MB.WinEIDrive.Excel.CellRange">CellRange</see>.
    ///</summary>
    ///<example> Following code creates horizontal, vertical and rectangular cell ranges and demonstrates how
    ///indexing works different in different context. <see cref="MB.WinEIDrive.Excel.CellRange.SetBorders(MB.WinEIDrive.Excel.MultipleBorders,System.Drawing.Color,MB.WinEIDrive.Excel.LineStyle)">SetBorders</see>
    ///method is used to mark outside borders of the rectangular range.
    ///<code lang="Visual Basic">
    ///Dim cr As CellRange = excelFile.Worksheets(0).Rows(1).Cells
    ///
    ///cr(0).Value = cr.IndexingMode
    ///cr(3).Value = "D2"
    ///cr("B").Value = "B2"
    ///
    ///cr = excelFile.Worksheets(0).Columns(4).Cells
    ///
    ///cr(0).Value = cr.IndexingMode
    ///cr(2).Value = "E3"
    ///cr("5").Value = "E5"
    ///
    ///cr = excelFile.Worksheets(0).Cells.GetSubrange("F2", "J8")
    ///cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed)
    ///
    ///cr("I7").Value = cr.IndexingMode
    ///cr(0, 0).Value = "F2"
    ///cr("G3").Value = "G3"
    ///cr(5).Value = "F3" <font color="Green">' Cell range width is 5 (F G H I J).</font>
    ///</code>
    ///<code lang="C#">
    ///CellRange cr = excelFile.Worksheets[0].Rows[1].Cells;
    ///
    ///cr[0].Value = cr.IndexingMode;
    ///cr[3].Value = "D2";
    ///cr["B"].Value = "B2";
    ///
    ///cr = excelFile.Worksheets[0].Columns[4].Cells;
    ///
    ///cr[0].Value = cr.IndexingMode;
    ///cr[2].Value = "E3";
    ///cr["5"].Value = "E5";
    ///
    ///cr = excelFile.Worksheets[0].Cells.GetSubrange("F2", "J8");
    ///cr.SetBorders(MultipleBorders.Outside, Color.Navy, LineStyle.Dashed);
    ///
    ///cr["I7"].Value = cr.IndexingMode;
    ///cr[0,0].Value = "F2";
    ///cr["G3"].Value = "G3";
    ///cr[5].Value = "F3"; <font color="Green">// Cell range width is 5 (F G H I J).</font>
    ///</code>
    ///</example>
    ///<seealso cref="MB.WinEIDrive.Excel.CellRange.IndexingMode" />
    internal enum RangeIndexingMode
    {
        Rectangular,
        Horizontal,
        Vertical
    }
}

