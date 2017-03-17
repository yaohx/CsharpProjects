namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Specifies a vertical position where the new page begins when the worksheet is printed.
    ///</summary>
    internal class VerticalPageBreak : PageBreak
    {
        // Methods
        internal VerticalPageBreak(int column, int firstRow, int lastRow) : base(column, firstRow, lastRow)
        {
        }


        // Properties
        ///<summary>
        ///Index of the first column of the new page.
        ///</summary>
        public int Column
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

        ///<summary>
        ///Index of the first row of the new page.
        ///</summary>
        ///<remarks>
        ///Use 0 (first row) if you don't care.
        ///</remarks>
        public int FirstRow
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
        ///Index of the last row of the new page.
        ///</summary>
        ///<remarks>
        ///Use 65535 (last row) if you don't care.
        ///</remarks>
        public int LastRow
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

    }
}

