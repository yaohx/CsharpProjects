namespace MB.WinEIDrive.Excel
{
    using System;

    internal enum BOFSubstreamType : ushort
    {
        // Fields
        Chart = 0x20,
        MacroSheet = 0x40,
        VisualBasic = 6,
        WorkbookGlobals = 5,
        WorksheetDialogSheet = 0x10,
        WorkspaceFile = 0x100
    }
}

