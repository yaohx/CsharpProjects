namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    ///<summary>
    ///Base class for row, column and cell collections.
    ///</summary>
    internal abstract class ExcelRowColumnCellCollectionBase : IEnumerable
    {
        // Methods
        ///<summary>
        ///Internal.
        ///</summary>
        ///<param name="parent"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ExcelRowColumnCellCollectionBase(ExcelWorksheet parent)
        {
            this.parent = parent;
            this.items = new ArrayList();
        }

        ///<summary>
        ///Returns an enumerator for the <see cref="MB.WinEIDrive.Excel.ExcelRowColumnCellCollectionBase">
        ///ExcelRowColumnCellCollectionBase</see>.
        ///</summary>
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }


        // Properties
        ///<summary>
        ///Gets the number of currently allocated elements (dynamically changes when worksheet is modified).
        ///</summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        ///<summary>
        ///Internal.
        ///</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ArrayList Items
        {
            get
            {
                return this.items;
            }
        }

        internal ExcelWorksheet Parent
        {
            get
            {
                return this.parent;
            }
        }


        // Fields
        private ArrayList items;
        private ExcelWorksheet parent;
    }
}

