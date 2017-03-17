namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class FormulaRecord : XLSRecord
    {
        // Methods
        static FormulaRecord()
        {
            FormulaRecord.staticDescriptor = XLSDescriptors.GetByName("Formula");
        }

        public FormulaRecord(object[] args) : base(FormulaRecord.staticDescriptor)
        {
            base.InitializeBody(args);
        }

        public FormulaRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(FormulaRecord.staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            if (loadedArgs[1] != null)
            {
                return (ushort) loadedArgs[3];
            }
            return 8;
        }


        // Fields
        private static XLSDescriptor staticDescriptor;
    }
}

