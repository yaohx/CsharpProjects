namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    ///<summary>
    ///Formula token for holding unary operator.
    ///</summary>
    internal class UnaryOperatorFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes the <see cref="MB.WinEIDrive.Excel.UnaryOperatorFormulaToken" /> class.
        ///</summary>
        static UnaryOperatorFormulaToken()
        {
            UnaryOperatorFormulaToken.CodesToStrings = new Hashtable();
            UnaryOperatorFormulaToken.UnaryOperatorsList = new ArrayList();
            UnaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Uplus] = "+";
            UnaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Uminus] = "-";
            UnaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Percent] = "%";
            UnaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Parentheses] = "(";
            UnaryOperatorFormulaToken.UnaryOperatorsList.AddRange(new char[] { '+', '-', '%', '(', ')' } );
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.UnaryOperatorFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The code.</param>
        public UnaryOperatorFormulaToken(FormulaTokenCode code) : base(code, 1, FormulaTokenType.Unary)
        {
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return (UnaryOperatorFormulaToken.CodesToStrings[(FormulaTokenCode) base.Code] as string);
        }


        // Fields
        public static readonly Hashtable CodesToStrings;
        public static readonly ArrayList UnaryOperatorsList;
    }
}

