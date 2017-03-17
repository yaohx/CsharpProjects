namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    ///<summary>
    ///Base class for page break collections.
    ///</summary>
    internal abstract class PageBreakCollection : IEnumerable
    {
        // Methods
        internal PageBreakCollection()
        {
            this.items = new ArrayList();
        }

        internal void Add(PageBreak pb)
        {
            this.items.Add(pb);
        }

        ///<summary>
        ///Removes all page breaks.
        ///</summary>
        public void Clear()
        {
            this.items.Clear();
        }

        internal object[] GetArgs()
        {
            ArrayList list1 = new ArrayList();
            foreach (PageBreak break1 in this.items)
            {
                object[] objArray1 = new object[] { (ushort) break1.breakIndex, (ushort) break1.firstLimit, (ushort) break1.lastLimit } ;
                list1.AddRange(objArray1);
            }
            return new object[] { ((ushort) this.items.Count), ((object[]) list1.ToArray(typeof(object))) } ;
        }

        ///<summary>
        ///Returns an enumerator for the collection.
        ///</summary>
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        internal abstract PageBreak InstanceCreator(int breakIndex, int firstLimit, int lastLimit);

        internal void LoadArgs(object[] args)
        {
            int num1 = (ushort) args[0];
            object[] objArray1 = (object[]) args[1];
            for (int num2 = 0; num2 < num1; num2++)
            {
                ushort num3 = (ushort) objArray1[num2 * 3];
                ushort num4 = (ushort) objArray1[(num2 * 3) + 1];
                ushort num5 = (ushort) objArray1[(num2 * 3) + 2];
                this.items.Add(this.InstanceCreator(num3, num4, num5));
            }
        }

        ///<summary>
        ///Removes the page break at the specified index.
        ///</summary>
        ///<param name="index">The zero-based index of the page break to remove.</param>
        public void RemoveAt(int index)
        {
            this.items.RemoveAt(index);
        }


        // Properties
        ///<summary>
        ///Gets the number of page breaks contained in the collection.
        ///</summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }


        // Fields
        internal ArrayList items;
    }
}

