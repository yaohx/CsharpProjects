namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding missed argument( argument with no value ) in argument list of function.
    ///</summary>
    internal class MissArgFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.MissArgFormulaToken" /> class.
        ///</summary>
        public MissArgFormulaToken() : base(FormulaTokenCode.MissArg, 1, FormulaTokenType.Operand)
        {
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return string.Empty;
        }

    }
}

