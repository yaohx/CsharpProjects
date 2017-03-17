namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    ///<summary>
    ///SupBookRecord record is used to provide information about internal 3d references
    ///</summary>
    internal class SupBookRecord : XLSRecord
    {
        // Methods
        static SupBookRecord()
        {
            SupBookRecord.staticDescriptor = XLSDescriptors.GetByName("SUPBOOK");
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.SupBookRecord" /> class.
        ///</summary>
        public SupBookRecord() : base(SupBookRecord.staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.SupBookRecord" /> class.
        ///</summary>
        ///<param name="bodyLength">Length of the body.</param>
        ///<param name="br">The binary reader to read from.</param>
        ///<param name="previousRecord">The previous record.</param>
        public SupBookRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(SupBookRecord.staticDescriptor, bodyLength, br)
        {
        }

        protected override void InitializeDelayed()
        {
            object[] objArray1 = new object[] { this.sheetsCount } ;
            base.InitializeDelayed(objArray1);
        }


        // Properties
        ///<summary>
        ///Gets or sets the sheets count in current workbook.
        ///</summary>
        ///<value>The sheets count in current workbook.</value>
        public ushort SheetsCount
        {
            get
            {
                return this.sheetsCount;
            }
            set
            {
                this.sheetsCount = value;
            }
        }


        // Fields
        private ushort sheetsCount;
        private static XLSDescriptor staticDescriptor;
    }
}

