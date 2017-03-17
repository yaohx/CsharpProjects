namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    internal class IndexedHashCollection : ArrayList
    {
        // Methods
        public IndexedHashCollection()
        {
            this.hashtable = new Hashtable();
        }

        public override int Add(object item)
        {
            object obj1 = this.hashtable[item];
            if (obj1 != null)
            {
                return (int) obj1;
            }
            return this.AddInternal(item);
        }

        protected int AddArrayOnly(object item)
        {
            return base.Add(item);
        }

        public int AddInternal(object item)
        {
            int num1 = this.AddArrayOnly(item);
            this.hashtable[item] = num1;
            return num1;
        }


        // Fields
        private Hashtable hashtable;
    }
}

