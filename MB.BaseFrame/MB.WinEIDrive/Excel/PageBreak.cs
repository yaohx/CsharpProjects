namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Base class for all page breaks.
    ///</summary>
    internal class PageBreak
    {
        // Methods
        internal PageBreak(PageBreak source) : this(source.breakIndex, source.firstLimit, source.lastLimit)
        {
        }

        internal PageBreak(int breakIndex, int firstLimit, int lastLimit)
        {
            this.breakIndex = breakIndex;
            this.firstLimit = firstLimit;
            this.lastLimit = lastLimit;
        }


        // Fields
        internal int breakIndex;
        internal int firstLimit;
        internal int lastLimit;
    }
}

