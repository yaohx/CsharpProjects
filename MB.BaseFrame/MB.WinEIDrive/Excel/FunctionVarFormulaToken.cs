namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding function with variable arguments count.
    ///</summary>
    internal class FunctionVarFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.FunctionVarFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The code.</param>
        public FunctionVarFormulaToken(FormulaTokenCode code) : base(code, 4, FormulaTokenType.Function)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            buffer1[1] = this.argumentsCount;
            byte[] buffer2 = BitConverter.GetBytes(this.function.Code);
            buffer2.CopyTo(buffer1, 2);
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.function = FormulaFunctionsTable.Instance[data[0] as string];
            this.argumentsCount = (byte) data[1];
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.argumentsCount = rpnBytes[startIndex];
            ushort num1 = BitConverter.ToUInt16(rpnBytes, startIndex + 1);
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
                return this.argumentsCount;
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
        private byte argumentsCount;
        private FormulaFunctionInfo function;
    }
}

