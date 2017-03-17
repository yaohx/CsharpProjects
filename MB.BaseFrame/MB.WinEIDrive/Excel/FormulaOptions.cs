namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum FormulaOptions : ushort
    {
        // Fields
        All = 11,
        CalculateOnLoad = 2,
        RecalculateAlways = 1,
        SharedFormula = 8
    }
}

