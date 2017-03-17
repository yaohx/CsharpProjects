namespace MB.WinEIDrive.Excel
{
    using System;

    internal class WeekReferenceWithHash : WeakReference
    {
        // Methods
        public WeekReferenceWithHash(WeakHashtable parent, HashtableElement target) : base(target)
        {
            this.parent = parent;
            this.hash = target.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            object obj1;
            object obj2;
            WeekReferenceWithHash hash1 = (WeekReferenceWithHash) obj;
            if (object.ReferenceEquals(this, hash1))
            {
                return true;
            }
            try
            {
                obj1 = this.Target;
            }
            catch (InvalidOperationException)
            {
                obj1 = null;
            }
            if (obj1 == null)
            {
                this.parent.AddForRemoval(this);
                return false;
            }
            try
            {
                obj2 = hash1.Target;
            }
            catch (InvalidOperationException)
            {
                obj2 = null;
            }
            if (obj2 == null)
            {
                this.parent.AddForRemoval(hash1);
                return false;
            }
            return obj1.Equals(obj2);
        }

        public override int GetHashCode()
        {
            return this.hash;
        }


        // Fields
        private int hash;
        private WeakHashtable parent;
    }
}

