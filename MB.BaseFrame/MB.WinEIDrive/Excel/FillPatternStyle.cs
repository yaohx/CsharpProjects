namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Fill pattern styles used for
    ///<see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternStyle">ExcelFillPattern.PatternStyle</see>.
    ///</summary>
    ///<remarks>
    ///<p>To see names of Microsoft Excel patterns, start Microsoft Excel and go to "Format" menu &gt; "Cells..." submenu &gt;
    ///"Patterns" tab &gt; "Pattern" drop-down. When hovering over a pattern, Microsoft Excel name is displayed in tooltip
    ///text.</p>
    ///<p><b>None</b> fill pattern uses no colors.</p>
    ///<p><b>Solid</b> fill pattern uses <see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternForegroundColor">
    ///ExcelFillPattern.PatternForegroundColor</see>.</p>
    ///<p>All other paterns use both <see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternForegroundColor">
    ///ExcelFillPattern.PatternForegroundColor</see> and
    ///<see cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternBackgroundColor">ExcelFillPattern.PatternBackgroundColor</see></p>
    ///</remarks>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternStyle">ExcelFillPattern.PatternStyle</seealso>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternForegroundColor">ExcelFillPattern.PatternForegroundColor</seealso>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelFillPattern.PatternBackgroundColor">ExcelFillPattern.PatternBackgroundColor</seealso>
    internal enum FillPatternStyle
    {
        None,
        Solid,
        Gray50,
        Gray75,
        Gray25,
        HorizontalStripe,
        VerticalStripe,
        ReverseDiagonalStripe,
        DiagonalStripe,
        DiagonalCrosshatch,
        ThickDiagonalCrosshatch,
        ThinHorizontalStripe,
        ThinVerticalStripe,
        ThinReverseDiagonalStripe,
        ThinDiagonalStripe,
        ThinHorizontalCrosshatch,
        ThinDiagonalCrosshatch,
        Gray12,
        Gray6
    }
}

