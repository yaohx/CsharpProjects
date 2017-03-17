namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Globalization;

    ///<summary>
    ///Contains static licensing methods (ExcelLite Professional only) and diagnostic
    ///information about executing ExcelLite assembly.
    ///</summary>
    internal sealed class ExcelLite
    {
        // Methods
        static ExcelLite()
        {
            MB.WinEIDrive.Excel.ExcelLite.LicenseReleaseID = 2 + int.Parse("23", CultureInfo.InvariantCulture);
        }

        private ExcelLite()
        {
        }


        // Fields
        ///<summary>
        ///ExcelLite assembly full version.
        ///</summary>
        public const string FullVersion = "23.1.5000.1000";
        internal static int LicenseReleaseID;
        private const string Name = "MB.WinEIDrive.Excel Free 2.3";
        private const string RevisionStr = "1000";
        ///<summary>
        ///ExcelLite assembly title.
        ///</summary>
        public const string Title = "MB.WinEIDrive.Excel Free 2.3 for .NET 1.1";
        private const string TypeStr = "1";
        private const string VersionLong = "2.3";
        private const string VersionShort = "23";
    }
}

