namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Reflection;

    ///<summary>
    ///Collection of vertical page breaks (<see cref="MB.WinEIDrive.Excel.VerticalPageBreak">VerticalPageBreak</see>).
    ///</summary>
    internal class VerticalPageBreakCollection : PageBreakCollection
    {
        // Methods
        internal VerticalPageBreakCollection()
        {
        }

        internal VerticalPageBreakCollection(VerticalPageBreakCollection source)
        {
            foreach (VerticalPageBreak break1 in source.items)
            {
                base.items.Add(new VerticalPageBreak(break1.Column, break1.FirstRow, break1.LastRow));
            }
        }

        ///<overloads>Ads a new vertical page break.</overloads>
        ///<summary>
        ///Ads a new vertical page break left to the specified column.
        ///</summary>
        ///<param name="column">The zero-based index of the column.</param>
        public void Add(int column)
        {
            base.Add(new VerticalPageBreak(column, 0, 0xffff));
        }

        ///<summary>
        ///Ads a new vertical page break left to the specified column and within specified rows.
        ///</summary>
        ///<param name="column">The zero-based index of the column.</param>
        ///<param name="firstRow">The zero-based index of the first row.</param>
        ///<param name="lastRow">The zero-based index of the last row.</param>
        public void Add(int column, int firstRow, int lastRow)
        {
            base.Add(new VerticalPageBreak(column, firstRow, lastRow));
        }

        internal override PageBreak InstanceCreator(int breakIndex, int firstLimit, int lastLimit)
        {
            return new VerticalPageBreak(breakIndex, firstLimit, lastLimit);
        }


        // Properties
        ///<summary>
        ///Gets or sets the vertical page break at the specified index.
        ///</summary>
        public VerticalPageBreak this[int index]
        {
            get
            {
                return (VerticalPageBreak) base.items[index];
            }
            set
            {
                base.items[index] = value;
            }
        }

    }
}

