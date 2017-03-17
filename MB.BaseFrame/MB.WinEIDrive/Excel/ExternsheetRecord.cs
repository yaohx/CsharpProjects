namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    ///<summary>
    ///Externsheet record for holding information REF' structures
    ///</summary>
    internal class ExternsheetRecord : XLSRecord
    {
        // Methods
        static ExternsheetRecord()
        {
            ExternsheetRecord.staticDescriptor = XLSDescriptors.GetByName("EXTERNSHEET");
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.ExternsheetRecord" /> class.
        ///</summary>
        public ExternsheetRecord() : base(ExternsheetRecord.staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.ExternsheetRecord" /> class.
        ///</summary>
        ///<param name="bodyLength">Length of the body.</param>
        ///<param name="br">The binary reader to read from.</param>
        ///<param name="previousRecord">The previous record.</param>
        public ExternsheetRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(ExternsheetRecord.staticDescriptor, bodyLength, br)
        {
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((bodySize - 6) / 6);
        }

        protected override void InitializeDelayed()
        {
            MB.WinEIDrive.Excel.SheetIndexes[] indexesArray1 = new MB.WinEIDrive.Excel.SheetIndexes[this.sheetIndexes.Length];
            for (int num1 = 0; num1 < this.sheetIndexes.Length; num1++)
            {
                indexesArray1[num1] = new MB.WinEIDrive.Excel.SheetIndexes(this.sheetIndexes[num1]);
            }
            object[] objArray1 = new object[] { (ushort) this.sheetIndexes.Length, indexesArray1 } ;
            base.InitializeDelayed(objArray1);
        }


        // Properties
        protected override int BodySize
        {
            get
            {
                if (this.Body != null)
                {
                    return this.Body.Length;
                }
                return ((this.sheetIndexes.Length * 6) + 2);
            }
        }

        ///<summary>
        ///Gets the sheet indexes.
        ///</summary>
        ///<value>The sheet indexes.</value>
        internal ushort[] SheetIndexes
        {
            get
            {
                return this.sheetIndexes;
            }
            set
            {
                this.sheetIndexes = value;
            }
        }


        // Fields
        private ushort[] sheetIndexes;
        private static XLSDescriptor staticDescriptor;
    }
}

