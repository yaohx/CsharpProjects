namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    internal class WeakHashtable
    {
        // Methods
        public WeakHashtable(int addQueueSize)
        {
            this.hashtable = new Hashtable();
            this.removeQueue = new Queue();
            this.addQueue = new Queue();
            this.AddQueueSize = addQueueSize;
        }

        public void Add(HashtableElement el)
        {
            WeekReferenceWithHash hash1 = new WeekReferenceWithHash(this, el);
            this.hashtable.Add(hash1, hash1);
            this.EmptyRemoveQueue();
        }

        internal void AddForRemoval(WeekReferenceWithHash weakRef)
        {
            this.removeQueue.Enqueue(weakRef);
        }

        private void EmptyRemoveQueue()
        {
            while (this.removeQueue.Count > 0)
            {
                this.hashtable.Remove((WeekReferenceWithHash) this.removeQueue.Dequeue());
            }
        }

        public HashtableElement Find(HashtableElement el)
        {
            HashtableElement element1;
            WeekReferenceWithHash hash1 = new WeekReferenceWithHash(this, el);
            hash1 = (WeekReferenceWithHash) this.hashtable[hash1];
            this.EmptyRemoveQueue();
            if (hash1 == null)
            {
                return null;
            }
            try
            {
                element1 = (HashtableElement) hash1.Target;
            }
            catch (InvalidOperationException)
            {
                element1 = null;
            }
            return element1;
        }


        // Properties
        public Queue AddQueue
        {
            get
            {
                return this.addQueue;
            }
        }

        public HashtableElement DefaultElement
        {
            get
            {
                return this.defaultElement;
            }
        }


        // Fields
        private Queue addQueue;
        public int AddQueueSize;
        protected HashtableElement defaultElement;
        private Hashtable hashtable;
        private Queue removeQueue;
    }
}

