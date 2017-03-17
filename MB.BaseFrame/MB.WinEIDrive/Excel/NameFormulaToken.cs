namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding the index to a NAME/EXTERNNAME record.
    ///</summary>
    internal class NameFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.NameFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The code.</param>
        public NameFormulaToken(FormulaTokenCode code) : base(code, 5, FormulaTokenType.Operand)
        {
            this.nameIndex = 0;
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            BitConverter.GetBytes(this.nameIndex).CopyTo(buffer1, 1);
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            string text1 = data[0] as string;
            ExcelWorksheet worksheet1 = data[1] as ExcelWorksheet;
            this.nameIndex = (ushort) (Array.IndexOf(worksheet1.NamedRanges.Names, text1) + 1);
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.nameIndex = BitConverter.ToUInt16(rpnBytes, startIndex + 1);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return "Name";
        }


        // Fields
        ///<summary>
        ///One-based index to ExternName record.
        ///</summary>
        private ushort nameIndex;
    }
}

