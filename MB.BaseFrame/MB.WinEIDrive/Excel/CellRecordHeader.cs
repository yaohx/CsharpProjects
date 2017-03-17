namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    internal class CellRecordHeader : BinaryWritable
    {
        // Methods
        public CellRecordHeader(BinaryReader br)
        {
            this.Row = br.ReadUInt16();
            this.Column = br.ReadUInt16();
            this.StyleIndex = br.ReadUInt16();
        }

        public CellRecordHeader(ushort row, ushort column, ushort styleIndex)
        {
            this.Row = row;
            this.Column = column;
            this.StyleIndex = styleIndex;
        }

        public override string ToString()
        {
            object[] objArray1 = new object[] { "Row:", this.Row, " Column:", this.Column, " StyleIndex:", this.StyleIndex } ;
            return string.Concat(objArray1);
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write(this.Row);
            bw.Write(this.Column);
            bw.Write(this.StyleIndex);
        }


        // Properties
        public override int Size
        {
            get
            {
                return 6;
            }
        }


        // Fields
        public ushort Column;
        public ushort Row;
        public ushort StyleIndex;
    }
}

