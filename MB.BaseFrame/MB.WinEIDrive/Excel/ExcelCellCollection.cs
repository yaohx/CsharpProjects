namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Reflection;

    ///<summary>
    ///Collection of excel cells (<see cref="MB.WinEIDrive.Excel.ExcelCell">ExcelCell</see>).
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell" />
    internal class ExcelCellCollection : ExcelRowColumnCellCollectionBase
    {
        // Methods
        internal ExcelCellCollection(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelCellCollection(ExcelWorksheet parent, ExcelCellCollection sourceCells) : base(parent)
        {
            foreach (ExcelCell cell1 in sourceCells)
            {
                base.Items.Add(new ExcelCell(base.Parent, cell1));
            }
        }

        private void AdjustArraySize(int index)
        {
            if (index > (base.Items.Count - 1))
            {
                ExcelColumnCollection.ExceptionIfColumnOutOfRange(index);
                int num2 = index - (base.Items.Count - 1);
                for (int num1 = 0; num1 < num2; num1++)
                {
                    base.Items.Add(new ExcelCell(base.Parent));
                }
            }
        }


        // Properties
        ///<summary>
        ///Gets the cell with the specified index.
        ///</summary>
        ///<param name="index">The zero-based index of the cell.</param>
        public ExcelCell this[int index]
        {
            get
            {
                this.AdjustArraySize(index);
                return (ExcelCell) base.Items[index];
            }
        }

    }
}

