namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding integer.
    ///</summary>
    internal class NumFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.NumFormulaToken" /> class.
        ///</summary>
        public NumFormulaToken() : base(FormulaTokenCode.Num, 9, FormulaTokenType.Operand)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            byte[] buffer2 = BitConverter.GetBytes(this.value);
            buffer2.CopyTo(buffer1, 1);
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.value = (double) data[0];
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.value = BitConverter.ToDouble(rpnBytes, startIndex);
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
        public double Value
        {
            get
            {
                return this.value;
            }
        }


        // Fields
        private double value;
    }
}

