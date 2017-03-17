namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum ColumnInfoOptions : ushort
    {
        // Fields
        Collapsed = 0x1000,
        Hidden = 1
    }
}

