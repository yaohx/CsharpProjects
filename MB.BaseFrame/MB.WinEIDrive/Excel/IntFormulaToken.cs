namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding integer.
    ///</summary>
    internal class IntFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.IntFormulaToken" /> class.
        ///</summary>
        public IntFormulaToken() : base(FormulaTokenCode.Int, 3, FormulaTokenType.Operand)
        {
        }

        ///<summary>
        ///Convert formula token to bytes representation.
        ///</summary>
        ///<returns>bytes representation of the formula token</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            BitConverter.GetBytes(this.Value).CopyTo(buffer1, 1);
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.value = (ushort) data[0];
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.value = BitConverter.ToUInt16(rpnBytes, startIndex);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return this.value.ToString();
        }


        // Properties
        public ushort Value
        {
            get
            {
                return this.value;
            }
        }


        // Fields
        private ushort value;
    }
}

