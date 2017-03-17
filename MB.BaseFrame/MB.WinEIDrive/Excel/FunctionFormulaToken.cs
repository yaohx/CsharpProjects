namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding function.
    ///</summary>
    internal class FunctionFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.FunctionFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The code.</param>
        public FunctionFormulaToken(FormulaTokenCode code) : base(code, 3, FormulaTokenType.Function)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            byte[] buffer2 = BitConverter.GetBytes(this.function.Code);
            buffer2.CopyTo(buffer1, 1);
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.function = FormulaFunctionsTable.Instance[data[0] as string];
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            ushort num1 = BitConverter.ToUInt16(rpnBytes, startIndex);
            this.function = FormulaFunctionsTable.Instance[num1];
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return this.function.Name.ToUpper();
        }


        // Properties
        public byte ArgumentsCount
        {
            get
            {
                return this.function.ArgumentsCount;
            }
        }

        public FormulaFunctionInfo Function
        {
            get
            {
                return this.function;
            }
        }


        // Fields
        private FormulaFunctionInfo function;
    }
}

