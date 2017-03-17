namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding array.
    ///</summary>
    internal class ArrayFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.ArrayFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The FormulaTokenCode code.</param>
        public ArrayFormulaToken(FormulaTokenCode code) : base(code, 8, FormulaTokenType.Operand)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            buffer1[1] = 0;
            return buffer1;
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.columnsAmount = rpnBytes[startIndex];
            this.rowsAmount = BitConverter.ToUInt16(rpnBytes, startIndex + 1);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return string.Empty;
        }


        // Properties
        public byte ColumnsAmount
        {
            get
            {
                return this.columnsAmount;
            }
        }

        public ushort RowsAmount
        {
            get
            {
                return this.rowsAmount;
            }
        }


        // Fields
        private byte columnsAmount;
        private ushort rowsAmount;
    }
}

