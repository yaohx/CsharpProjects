namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum Window1Options : ushort
    {
        // Fields
        DisplayHScroll = 8,
        DisplayVScroll = 0x10,
        Hidden = 1,
        Iconic = 2,
        ShowTabs = 0x20
    }
}

