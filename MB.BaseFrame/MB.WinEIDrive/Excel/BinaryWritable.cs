namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal abstract class BinaryWritable
    {
        // Methods
        protected BinaryWritable()
        {
        }

        public abstract void Write(BinaryWriter bw);


        // Properties
        public abstract int Size { get; }

    }
}

