namespace MB.WinEIDrive.Excel
{
    using System;
    using System.ComponentModel;

    ///<summary>
    ///Base class for row and column collections.
    ///</summary>
    internal abstract class ExcelRowColumnCollectionBase : ExcelRowColumnCellCollectionBase
    {
        // Methods
        ///<summary>
        ///Internal.
        ///</summary>
        ///<param name="parent"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ExcelRowColumnCollectionBase(ExcelWorksheet parent) : base(parent)
        {
        }


        // Fields
        internal int MaxOutlineLevel;
    }
}

