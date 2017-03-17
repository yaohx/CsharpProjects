namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    ///<summary>
    ///Formula token for holding error value.
    ///</summary>
    internal class ErrFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes the <see cref="MB.WinEIDrive.Excel.ErrFormulaToken" /> class.
        ///</summary>
        static ErrFormulaToken()
        {
            ErrFormulaToken.CodesToStrings = new Hashtable();
            ErrFormulaToken.StringsToCodes = new Hashtable();
            ErrFormulaToken.ErrorsList = new ArrayList();
            ErrFormulaToken.CodesToStrings[(byte) 0] = "#NULL!";
            ErrFormulaToken.CodesToStrings[(byte) 7] = "#DIV/0!";
            ErrFormulaToken.CodesToStrings[(byte) 15] = "#VALUE!";
            ErrFormulaToken.CodesToStrings[(byte) 0x17] = "#REF!";
            ErrFormulaToken.CodesToStrings[(byte) 0x1d] = "#NAME?";
            ErrFormulaToken.CodesToStrings[(byte) 0x24] = "#NUM!";
            ErrFormulaToken.CodesToStrings[(byte) 0x2a] = "#N/A!";
            ErrFormulaToken.StringsToCodes["#NULL!"] = (byte) 0;
            ErrFormulaToken.StringsToCodes["#DIV/0!"] = (byte) 7;
            ErrFormulaToken.StringsToCodes["#VALUE!"] = (byte) 15;
            ErrFormulaToken.StringsToCodes["#REF!"] = (byte) 0x17;
            ErrFormulaToken.StringsToCodes["#NAME?"] = (byte) 0x1d;
            ErrFormulaToken.StringsToCodes["#NUM!"] = (byte) 0x24;
            ErrFormulaToken.StringsToCodes["#N/A!"] = (byte) 0x2a;
            string[] textArray1 = new string[] { "#NULL!", "#DIV/0!", "#VALUE!", "#REF!", "#NAME?", "#NUM!", "#N/A!" } ;
            ErrFormulaToken.ErrorsList.AddRange(textArray1);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.ErrFormulaToken" /> class.
        ///</summary>
        public ErrFormulaToken() : base(FormulaTokenCode.Err, 2, FormulaTokenType.Operand)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            buffer1[1] = this.value;
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.value = (byte) ErrFormulaToken.StringsToCodes[(string) data[0]];
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.value = rpnBytes[startIndex];
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return (ErrFormulaToken.CodesToStrings[this.value] as string);
        }


        // Properties
        public byte Value
        {
            get
            {
                return this.value;
            }
        }


        // Fields
        public static readonly Hashtable CodesToStrings;
        public static readonly ArrayList ErrorsList;
        public static readonly Hashtable StringsToCodes;
        private byte value;
    }
}

