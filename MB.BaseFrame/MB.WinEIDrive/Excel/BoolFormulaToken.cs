namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding bool.
    ///</summary>
    internal class BoolFormulaToken : FormulaToken
    {
        // Methods
        static BoolFormulaToken()
        {
            BoolFormulaToken.True = "TRUE";
            BoolFormulaToken.False = "FALSE";
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.BoolFormulaToken" /> class.
        ///</summary>
        public BoolFormulaToken() : base(FormulaTokenCode.Bool, 2, FormulaTokenType.Operand)
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
            this.value = (bool) data[0];
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.value = BitConverter.ToBoolean(rpnBytes, startIndex);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            if (!this.value)
            {
                return BoolFormulaToken.False;
            }
            return BoolFormulaToken.True;
        }


        // Properties
        public bool Value
        {
            get
            {
                return this.value;
            }
        }


        // Fields
        public static string False;
        public static string True;
        private bool value;
    }
}

