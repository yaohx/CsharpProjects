namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class ExcelStringWithoutLength : ExcelStringBase
    {
        // Methods
        public ExcelStringWithoutLength(string str) : base(str)
        {
        }

        public ExcelStringWithoutLength(BinaryReader br, int charCount)
        {
            int num1 = 0x2020;
            base.ReadOptionsAndString(br, ref num1, ref charCount);
        }

        public override string ToString()
        {
            return ("ExcelStringWithoutLength(" + base.GetFormattedStr() + ")");
        }

    }
}

