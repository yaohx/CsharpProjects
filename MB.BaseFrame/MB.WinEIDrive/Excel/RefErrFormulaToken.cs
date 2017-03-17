namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding reference error.
    ///</summary>
    internal class RefErrFormulaToken : RefFormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.RefErrFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The code.</param>
        public RefErrFormulaToken(FormulaTokenCode code) : base(code)
        {
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return "#REF";
        }

    }
}

