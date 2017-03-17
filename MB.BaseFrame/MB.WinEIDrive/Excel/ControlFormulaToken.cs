namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding control value.
    ///</summary>
    internal class ControlFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.ControlFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The FormulaTokenCode code.</param>
        public ControlFormulaToken(FormulaTokenCode code) : base(code, 5, FormulaTokenType.Control)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            if (this.options != null)
            {
                this.options.CopyTo(buffer1, 1);
                return buffer1;
            }
            byte[] buffer2 = BitConverter.GetBytes(this.row);
            buffer2.CopyTo(buffer1, 1);
            byte[] buffer3 = BitConverter.GetBytes(this.Column);
            buffer3.CopyTo(buffer1, 3);
            return buffer1;
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            if (base.Code == 0x19)
            {
                this.options = new byte[] { rpnBytes[startIndex], rpnBytes[startIndex + 1], rpnBytes[startIndex + 2] } ;
            }
            else
            {
                this.row = BitConverter.ToUInt16(rpnBytes, startIndex);
                this.column = BitConverter.ToUInt16(rpnBytes, startIndex + 2);
            }
        }


        // Properties
        public ushort Column
        {
            get
            {
                return this.column;
            }
        }

        public ushort Row
        {
            get
            {
                return this.row;
            }
        }

        public override int Size
        {
            get
            {
                if (base.Code != 0x19)
                {
                    return 5;
                }
                return 4;
            }
        }


        // Fields
        private ushort column;
        private byte[] options;
        private ushort row;
    }
}

