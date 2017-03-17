namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///HashtableElement. All derived classes MUST implement:
    ///1) HashtableElement Clone()
    ///2) int GetHashCode()
    ///3) bool Equals(object obj)
    ///</summary>
    internal class HashtableElement
    {
        // Methods
        public HashtableElement(WeakHashtable parentCollection, bool isInCache)
        {
            this.parentCollection = parentCollection;
            this.isInCache = isInCache;
        }

        public virtual HashtableElement Clone(WeakHashtable parentCollection)
        {
            throw new Exception("Internal: Must override Clone() in derived class");
        }

        public override bool Equals(object obj)
        {
            throw new Exception("Internal: Must override Equals(object) in derived class");
        }

        public HashtableElement FindExistingOrAddToCache()
        {
            HashtableElement element1 = this.parentCollection.Find(this);
            if (element1 != null)
            {
                return element1;
            }
            this.parentCollection.Add(this);
            this.isInCache = true;
            return this;
        }

        public override int GetHashCode()
        {
            throw new Exception("Internal: Must override GetHashCode() in derived class");
        }


        // Properties
        public bool IsInCache
        {
            get
            {
                return this.isInCache;
            }
        }

        public WeakHashtable ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
        }


        // Fields
        private bool isInCache;
        private WeakHashtable parentCollection;
    }
}

