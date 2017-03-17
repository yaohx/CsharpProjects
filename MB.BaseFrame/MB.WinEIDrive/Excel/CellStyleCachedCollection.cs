namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    internal class CellStyleCachedCollection : WeakHashtable
    {
        // Methods
        public CellStyleCachedCollection(int addQueueSize) : base(addQueueSize)
        {
            base.defaultElement = new CellStyleData(this, true);
        }

        public void EmptyAddQueue()
        {
            Queue queue1 = base.AddQueue;
            while (queue1.Count > 0)
            {
                ((CellStyle) queue1.Dequeue()).Consolidate();
            }
        }

    }
}

