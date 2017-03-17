namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum WSBoolOptions : ushort
    {
        // Fields
        AlternateExpEval = 0x4000,
        AlternateForEntry = 0x8000,
        ApplyStyles = 0x20,
        ColGroupRight = 0x80,
        Dialog = 0x10,
        DspGuts = 0x400,
        FitToPage = 0x100,
        RowGroupBelow = 0x40,
        ShowAutoBreaks = 1
    }
}

