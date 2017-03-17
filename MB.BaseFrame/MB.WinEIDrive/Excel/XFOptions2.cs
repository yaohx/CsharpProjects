namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum XFOptions2 : ushort
    {
        // Fields
        IndentLevel = 15,
        ShrinkToFit = 0x10,
        TextDirection = 0xc0,
        UsedAttributes = 0xfc00
    }
}

