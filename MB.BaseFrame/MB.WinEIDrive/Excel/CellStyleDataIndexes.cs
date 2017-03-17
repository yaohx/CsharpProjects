namespace MB.WinEIDrive.Excel
{
    using System;

    internal class CellStyleDataIndexes
    {
        // Methods
        public CellStyleDataIndexes()
        {
            this.CellStyleIndex = -1;
            this.FontIndex = -1;
            this.PatternBackgroundColorIndex = -1;
            this.PatternForegroundColorIndex = -1;
            this.BorderColorIndex = new int[] { -1, -1, -1, -1, -1 } ;
            this.NumberFormatIndex = -1;
        }


        // Fields
        public int[] BorderColorIndex;
        public int CellStyleIndex;
        public int FontIndex;
        public int NumberFormatIndex;
        public int PatternBackgroundColorIndex;
        public int PatternForegroundColorIndex;
    }
}

