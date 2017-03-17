namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Represents a named range in the worksheet.
    ///</summary>
    internal class NamedRange
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.NamedRange" /> class.
        ///</summary>
        ///<param name="parent">Parent collection.</param>
        ///<param name="index">Index in the parrent collection.</param>
        ///<param name="name">The cell range name.</param>
        ///<param name="range">The named cell range.</param>
        internal NamedRange(NamedRangeCollection parent, int index, string name, CellRange range)
        {
            this.parent = parent;
            this.index = index;
            this.name = name;
            this.range = range;
        }

        ///<summary>
        ///Deletes this named range from the named ranges collection.
        ///</summary>
        public void Delete()
        {
            this.parent.DeleteInternal(this.index);
        }


        // Properties
        ///<summary>
        ///Gets the named range name.
        ///</summary>
        ///<value>The named range name.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        ///<summary>
        ///Gets the named cell range.
        ///</summary>
        ///<value>The named cell range.</value>
        public CellRange Range
        {
            get
            {
                return this.range;
            }
        }


        // Fields
        private int index;
        private string name;
        private NamedRangeCollection parent;
        private CellRange range;
    }
}

