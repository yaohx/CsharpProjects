namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Options specified when reading XLS files.
    ///</summary>
    [Flags]
    internal enum XlsOptions
    {
        // Fields
        ///<summary>
        ///Do not preserve records. Only records fully supported by ExcelLite API will be loaded.
        ///</summary>
        None = 0,
        ///<summary>
        ///Preserve all possible information.
        ///</summary>
        PreserveAll = 7,
        ///<summary>
        ///Preserve global (workbook) records.
        ///</summary>
        PreserveGlobalRecords = 1,
        ///<summary>
        ///Preserve summaries.
        ///</summary>
        PreserveSummaries = 4,
        ///<summary>
        ///Preserve worksheet records.
        ///</summary>
        PreserveWorksheetRecords = 2
    }
}

