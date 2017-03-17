namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding 3d reference error.
    ///</summary>
    internal class AreaErr3dFormulaToken : Area3dFormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.AreaErr3dFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The FormulaTokenCode code.</param>
        public AreaErr3dFormulaToken(FormulaTokenCode code) : base(code)
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

