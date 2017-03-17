namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Specifies a horizontal position where the new page begins when the worksheet is printed.
    ///</summary>
    internal class HorizontalPageBreak : PageBreak
    {
        // Methods
        internal HorizontalPageBreak(int row, int firstColumn, int lastColumn) : base(row, firstColumn, lastColumn)
        {
        }

        internal bool FixRowIndexes(int rowIndex, int offset)
        {
            if (base.breakIndex >= rowIndex)
            {
                base.breakIndex += offset;
                if (base.breakIndex < rowIndex)
                {
                    return false;
                }
            }
            return true;
        }


        // Properties
        ///<summary>
        ///Index of the first column of the new page.
        ///</summary>
        ///<remarks>
        ///Use 0 (first column) if you don't care.
        ///</remarks>
        public int FirstColumn
        {
            get
            {
                return base.firstLimit;
            }
            set
            {
                base.firstLimit = value;
            }
        }

        ///<summary>
        ///Index of the last column of the new page.
        ///</summary>
        ///<remarks>
        ///Use 255 (last column) if you don't care.
        ///</remarks>
        public int LastColumn
        {
            get
            {
                return base.lastLimit;
            }
            set
            {
                base.lastLimit = value;
            }
        }

        ///<summary>
        ///Index of the first row of the new page.
        ///</summary>
        public int Row
        {
            get
            {
                return base.breakIndex;
            }
            set
            {
                base.breakIndex = value;
            }
        }

    }
}

