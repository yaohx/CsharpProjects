namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Formula token for holding 3d reference error on internal cell range.
    ///</summary>
    internal class RefErr3dFormulaToken : RefFormulaToken
    {
        // Methods
        public RefErr3dFormulaToken(FormulaTokenCode code) : base(code)
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

