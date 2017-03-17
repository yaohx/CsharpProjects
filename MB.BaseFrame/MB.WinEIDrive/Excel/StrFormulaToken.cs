namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Text;

    ///<summary>
    ///Formula token for holding string.
    ///</summary>
    internal class StrFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.StrFormulaToken" /> class.
        ///</summary>
        public StrFormulaToken() : base(FormulaTokenCode.Str, 9, FormulaTokenType.Operand)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = new byte[(this.value.Length * 2) + 3];
            buffer1[0] = base.Code;
            buffer1[1] = (byte) this.value.Length;
            buffer1[2] = 1;
            byte[] buffer2 = Encoding.Unicode.GetBytes(this.value);
            buffer2.CopyTo(buffer1, 3);
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.value = (string) data[0];
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.isCompressed = rpnBytes[startIndex + 1] == 1;
            byte num1 = rpnBytes[startIndex];
            this.value = Utilities.ReadString(this.isCompressed, rpnBytes, startIndex + 2, num1);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return this.value;
        }


        // Properties
        public bool IsCompressed
        {
            get
            {
                return this.isCompressed;
            }
        }

        public override int Size
        {
            get
            {
                int num1 = this.value.Length;
                if (!this.isCompressed)
                {
                    return (num1 + 3);
                }
                return ((num1 * 2) + 3);
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
        }


        // Fields
        private bool isCompressed;
        public const char StartMark = '"';
        private string value;
    }
}

