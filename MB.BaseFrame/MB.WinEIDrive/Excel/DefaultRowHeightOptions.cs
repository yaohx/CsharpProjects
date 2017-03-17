namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum DefaultRowHeightOptions : ushort
    {
        // Fields
        AllZeroHeight = 2,
        SpaceAbove = 4,
        SpaceBelow = 8,
        Unsynced = 1
    }
}

