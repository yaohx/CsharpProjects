namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum RowOptions : ushort
    {
        // Fields
        Collapsed = 0x10,
        Default = 0,
        GhostDirty = 0x80,
        Unsynced = 0x40,
        ZeroHeight = 0x20
    }
}

