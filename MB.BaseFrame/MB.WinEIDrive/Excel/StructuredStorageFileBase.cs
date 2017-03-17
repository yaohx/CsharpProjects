namespace MB.WinEIDrive.Excel
{
    using System;

    internal abstract class StructuredStorageFileBase : IDisposable
    {
        // Methods
        protected StructuredStorageFileBase()
        {
        }

        public void Close()
        {
            this.Dispose();
        }

        public static StructuredStorageFileBase Create(string fileName)
        {
            return new UnmanagedStorage(fileName, true);
        }

        public void Dispose()
        {
            if (!this.Disposed)
            {
                GC.SuppressFinalize(this);
                this.Dispose(true);
            }
        }

        protected abstract void Dispose(bool disposing);

        ~StructuredStorageFileBase()
        {
            this.Dispose(false);
        }

        public static StructuredStorageFileBase Open(string fileName)
        {
            return new UnmanagedStorage(fileName, false);
        }

        public abstract byte[] ReadStream(string name);

        public abstract void WriteStream(string name, byte[] buffer);


        // Properties
        protected abstract bool Disposed { get; }

    }
}

