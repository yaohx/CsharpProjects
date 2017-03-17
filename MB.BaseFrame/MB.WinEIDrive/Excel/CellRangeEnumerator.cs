namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    ///<summary>
    ///Enumerator used for iterating cells in a <see cref="MB.WinEIDrive.Excel.CellRange">CellRange</see>.
    ///</summary>
    internal class CellRangeEnumerator : IEnumerator
    {
        // Methods
        internal CellRangeEnumerator(CellRange parent, bool onlyAllocated)
        {
            this.parent = parent;
            this.onlyAllocated = onlyAllocated;
            this.rowCollection = this.parent.Parent.Rows;
            if (this.onlyAllocated)
            {
                this.rowLimit = Math.Min((int) (this.rowCollection.Count - 1), this.parent.LastRowIndex);
            }
            else
            {
                this.rowLimit = this.parent.LastRowIndex;
            }
            this.Reset();
        }

        ///<summary>
        ///Advances the enumerator to the next element of the cell range.
        ///</summary>
        ///<returns>
        ///<b>true</b> if the enumerator was successfully advanced to the next element; <b>false</b> if
        ///the enumerator has passed the end of the cell range.
        ///</returns>
        public bool MoveNext()
        {
            while (this.currentRow <= this.rowLimit)
            {
                int num1;
                if (this.onlyAllocated)
                {
                    num1 = Math.Min((int) (this.rowCollection[this.currentRow].AllocatedCells.Count - 1), this.parent.LastColumnIndex);
                }
                else
                {
                    num1 = this.parent.LastColumnIndex;
                }
                this.currentColumn++;
                if (this.currentColumn <= num1)
                {
                    return true;
                }
                this.currentColumn = this.parent.FirstColumnIndex - 1;
                this.currentRow++;
            }
            return false;
        }

        ///<summary>
        ///Sets the enumerator to its initial position, which is one column before
        ///the first cell in the cell range.
        ///</summary>
        public void Reset()
        {
            this.currentRow = this.parent.FirstRowIndex;
            this.currentColumn = this.parent.FirstColumnIndex - 1;
        }


        // Properties
        ///<summary>
        ///Gets the current element in the cell range.
        ///</summary>
        public object Current
        {
            get
            {
                return this.CurrentCell;
            }
        }

        ///<summary>
        ///Gets the current <see cref="MB.WinEIDrive.Excel.ExcelCell">ExcelCell</see> in the cell range.
        ///</summary>
        public ExcelCell CurrentCell
        {
            get
            {
                if (this.parent.IsRowColumnOutOfRange(this.currentRow, this.currentColumn))
                {
                    throw new InvalidOperationException("Enumerator is not pointing to valid cell. Use Reset() and/or MoveNext() methods.");
                }
                return this.rowCollection[this.currentRow].AllocatedCells[this.currentColumn];
            }
        }

        ///<summary>
        ///Current absolute column index in the cell range.
        ///</summary>
        public int CurrentColumn
        {
            get
            {
                return this.currentColumn;
            }
        }

        ///<summary>
        ///Current absolute row index in the cell range.
        ///</summary>
        public int CurrentRow
        {
            get
            {
                return this.currentRow;
            }
        }

        ///<summary>
        ///Parent <see cref="MB.WinEIDrive.Excel.CellRange">CellRange</see>.
        ///</summary>
        public CellRange Parent
        {
            get
            {
                return this.parent;
            }
        }


        // Fields
        private int currentColumn;
        private int currentRow;
        private bool onlyAllocated;
        private readonly CellRange parent;
        private ExcelRowCollection rowCollection;
        private int rowLimit;
    }
}

