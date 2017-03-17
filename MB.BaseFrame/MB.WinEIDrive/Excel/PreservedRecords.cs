namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    internal class PreservedRecords
    {
        // Methods
        internal PreservedRecords()
        {
            this.typeGroupedRecords = new Hashtable();
        }

        internal PreservedRecords(PreservedRecords source)
        {
            this.typeGroupedRecords = new Hashtable();
            this.typeGroupedRecords = (Hashtable) source.typeGroupedRecords.Clone();
        }

        public void Add(XLSRecord record)
        {
            this.Add(record, record.RecordCode);
        }

        public void Add(XLSRecord record, int recordCode)
        {
            ArrayList list1 = (ArrayList) this.typeGroupedRecords[recordCode];
            if (list1 != null)
            {
                list1.Add(record);
            }
            else
            {
                list1 = new ArrayList();
                list1.Add(record);
                this.typeGroupedRecords[recordCode] = list1;
            }
        }

        public void CopyRecords(PreservedRecords source, int recordCode)
        {
            ArrayList list1 = (ArrayList) source.typeGroupedRecords[recordCode];
            if (list1 != null)
            {
                foreach (XLSRecord record1 in list1)
                {
                    this.Add(record1, recordCode);
                }
            }
        }

        public void CopyRecords(PreservedRecords source, string recordName)
        {
            int num1 = XLSDescriptors.GetByName(recordName).Code;
            this.CopyRecords(source, num1);
        }

        public void WriteRecords(AbsXLSRecords destination, int recordCode)
        {
            ArrayList list1 = (ArrayList) this.typeGroupedRecords[recordCode];
            if (list1 != null)
            {
                foreach (XLSRecord record1 in list1)
                {
                    destination.Add(record1);
                }
            }
        }

        public void WriteRecords(AbsXLSRecords destination, string recordName)
        {
            int num1 = XLSDescriptors.GetByName(recordName).Code;
            this.WriteRecords(destination, num1);
        }


        // Fields
        private Hashtable typeGroupedRecords;
    }
}

