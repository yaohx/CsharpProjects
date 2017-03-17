namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.IO;

    internal class IndexRecord : XLSRecord
    {
        // Methods
        static IndexRecord()
        {
            IndexRecord.staticDescriptor = XLSDescriptors.GetByName("Index");
        }

        public IndexRecord() : base(IndexRecord.staticDescriptor)
        {
            this.FirstRow = -1;
            this.LastRowPlusOne = -1;
            this.DBCells = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public IndexRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(IndexRecord.staticDescriptor, bodyLength, br)
        {
            this.FirstRow = -1;
            this.LastRowPlusOne = -1;
            this.DBCells = new ArrayList();
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((this.BodySize - 0x10) / 4);
        }

        protected override void InitializeDelayed()
        {
            object[] objArray1 = new object[this.DBCells.Count];
            for (int num1 = 0; num1 < this.DBCells.Count; num1++)
            {
                objArray1[num1] = (uint) ((DBCellRecord) this.DBCells[num1]).Address;
            }
            object[] objArray2 = new object[] { (uint) this.FirstRow, (uint) this.LastRowPlusOne, objArray1 } ;
            base.InitializeDelayed(objArray2);
        }


        // Properties
        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    return (0x10 + (this.DBCells.Count * 4));
                }
                return this.Body.Length;
            }
        }


        // Fields
        public ArrayList DBCells;
        public int FirstRow;
        public int LastRowPlusOne;
        private static XLSDescriptor staticDescriptor;
    }
}

