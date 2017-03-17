namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;

    internal class AbsXLSRecords : IEnumerable
    {
        // Methods
        public AbsXLSRecords()
        {
            this.records = new ArrayList();
        }

        public int Add(AbsXLSRec record)
        {
            return this.records.Add(record);
        }

        public IEnumerator GetEnumerator()
        {
            return this.records.GetEnumerator();
        }

        public void Read(BinaryReader br)
        {
            AbsXLSRec rec1 = null;
            while (br.PeekChar() != -1)
            {
                rec1 = AbsXLSRec.Read(br, rec1);
                if (rec1 == null)
                {
                    return;
                }
                this.Add(rec1);
            }
        }

        public void SetRecordAddresses()
        {
            int num1 = 0;
            foreach (AbsXLSRec rec1 in this)
            {
                rec1.Address = num1;
                num1 += rec1.Size;
            }
        }

        public void Write(BinaryWriter bw)
        {
            foreach (AbsXLSRec rec1 in this)
            {
                rec1.Write(bw);
            }
        }


        // Properties
        public AbsXLSRec this[int index]
        {
            get
            {
                return (AbsXLSRec) this.records[index];
            }
        }


        // Fields
        private ArrayList records;
    }
}

