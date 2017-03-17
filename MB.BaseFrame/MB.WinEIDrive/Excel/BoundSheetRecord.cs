namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class BoundSheetRecord : XLSRecord
    {
        // Methods
        static BoundSheetRecord()
        {
            BoundSheetRecord.staticDescriptor = XLSDescriptors.GetByName("BoundSheet");
        }

        public BoundSheetRecord(ExcelShortString sheetName, AbsXLSRec sheetBOFRecord) : base(BoundSheetRecord.staticDescriptor)
        {
            base.InitializeBody((byte[]) null);
            this.SheetName = sheetName;
            this.sheetBOFRecord = sheetBOFRecord;
        }

        public BoundSheetRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(BoundSheetRecord.staticDescriptor, bodyLength, br)
        {
            this.SheetName = (ExcelShortString) base.GetArguments()[3];
        }

        protected override void InitializeDelayed()
        {
            uint num1 = (uint) this.sheetBOFRecord.Address;
            object[] objArray1 = new object[] { num1, BoundSheetVisibility.Visible, BoundSheetSheetType.WorksheetOrDialogSheet, this.SheetName } ;
            base.InitializeDelayed(objArray1);
        }


        // Properties
        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    return (6 + this.SheetName.Size);
                }
                return base.Body.Length;
            }
        }


        // Fields
        private AbsXLSRec sheetBOFRecord;
        public ExcelShortString SheetName;
        private static XLSDescriptor staticDescriptor;
    }
}

