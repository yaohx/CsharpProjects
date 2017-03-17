namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.IO;

    internal class DBCellRecord : XLSRecord
    {
        // Methods
        static DBCellRecord()
        {
            DBCellRecord.staticDescriptor = XLSDescriptors.GetByName("DBCell");
        }

        public DBCellRecord() : base(DBCellRecord.staticDescriptor)
        {
            this.StartingCellsForRows = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public DBCellRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(DBCellRecord.staticDescriptor, bodyLength, br)
        {
            this.StartingCellsForRows = new ArrayList();
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return ((this.BodySize - 4) / 2);
        }

        protected override void InitializeDelayed()
        {
            object[] objArray2;
            if (this.FirstRow == null)
            {
                objArray2 = new object[2];
                objArray2[0] = 0;
                object[] objArray3 = new object[0];
                objArray2[1] = objArray3;
                base.InitializeDelayed(objArray2);
            }
            else
            {
                uint num1 = (uint) (base.Address - this.FirstRow.Address);
                AbsXLSRec rec1 = this;
                int num2 = this.StartingCellsForRows.Count - 1;
                while (num2 >= 0)
                {
                    if (this.StartingCellsForRows[num2] == null)
                    {
                        this.StartingCellsForRows[num2] = rec1;
                    }
                    else
                    {
                        rec1 = (AbsXLSRec) this.StartingCellsForRows[num2];
                    }
                    num2--;
                }
                object[] objArray1 = new object[this.StartingCellsForRows.Count];
                int num3 = this.FirstRow.Address + 20;
                for (num2 = 0; num2 < this.StartingCellsForRows.Count; num2++)
                {
                    int num4 = ((AbsXLSRec) this.StartingCellsForRows[num2]).Address;
                    objArray1[num2] = (ushort) (num4 - num3);
                    num3 = num4;
                }
                objArray2 = new object[] { num1, objArray1 } ;
                base.InitializeDelayed(objArray2);
            }
        }


        // Properties
        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    return (4 + (this.StartingCellsForRows.Count * 2));
                }
                return base.Body.Length;
            }
        }


        // Fields
        public AbsXLSRec FirstRow;
        public ArrayList StartingCellsForRows;
        private static XLSDescriptor staticDescriptor;
    }
}

