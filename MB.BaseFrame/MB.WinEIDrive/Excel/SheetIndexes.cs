namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;

    ///<summary>
    ///SheetIndexes structure for storing index to SupBook record
    ///</summary>
    internal class SheetIndexes : BinaryWritable
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.SheetIndexes" /> class.
        ///</summary>
        ///<param name="br">The binary reader to read from.</param>
        public SheetIndexes(BinaryReader br)
        {
            br.ReadUInt16();
            this.sheetIndex = br.ReadUInt16();
            br.ReadUInt16();
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.SheetIndexes" /> class.
        ///</summary>
        ///<param name="sheetIndex">The first/last sheet index.</param>
        public SheetIndexes(ushort sheetIndex)
        {
            this.sheetIndex = sheetIndex;
        }

        ///<summary>
        ///Writes the REF' data to the specified binary writer.
        ///</summary>
        ///<param name="bw">The destination binary writer.</param>
        public override void Write(BinaryWriter bw)
        {
            bw.Write((ushort) 0);
            bw.Write(this.SheetIndex);
            bw.Write(this.SheetIndex);
        }


        // Properties
        ///<summary>
        ///Gets or sets the first/last sheet index.
        ///</summary>
        ///<value>The first/last sheet index.</value>
        public ushort SheetIndex
        {
            get
            {
                return this.sheetIndex;
            }
            set
            {
                this.sheetIndex = value;
            }
        }

        ///<summary>
        ///Gets the size of REF' storage.
        ///</summary>
        ///<value>The size of REF' storage.</value>
        public override int Size
        {
            get
            {
                return 6;
            }
        }


        // Fields
        ///<summary>
        ///Index to first/last sheet
        ///</summary>
        private ushort sheetIndex;
    }
}

