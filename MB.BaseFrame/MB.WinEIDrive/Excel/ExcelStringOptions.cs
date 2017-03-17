namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum ExcelStringOptions : byte
    {
        // Fields
        AsianPhonetic = 4,
        RichText = 8,
        Uncompressed = 1
    }
}

