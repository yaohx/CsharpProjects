namespace MB.WinEIDrive.Excel
{
    using System;

    [Flags]
    internal enum SetupOptions : ushort
    {
        // Fields
        AutomaticPageNumbers = 0x80,
        DefaultPrintQuality = 0x10,
        DoNotPrintCellNotes = 0x20,
        Landscape = 2,
        PaperOrientationValid = 0x40,
        PrintColoured = 8,
        PrintErrorOptions = 0xc00,
        PrintNotesAsDisplayed = 0x200,
        PrintPagesInColumns = 1,
        SomeNotInitialised = 4
    }
}

