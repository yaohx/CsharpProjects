namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    ///<summary>
    ///Formula token for holding binary operator.
    ///</summary>
    internal class BinaryOperatorFormulaToken : FormulaToken
    {
        // Methods
        ///<summary>
        ///Initializes the <see cref="MB.WinEIDrive.Excel.BinaryOperatorFormulaToken" /> class.
        ///</summary>
        static BinaryOperatorFormulaToken()
        {
            BinaryOperatorFormulaToken.CodesToStrings = new Hashtable();
            BinaryOperatorFormulaToken.StringsToCodes = new Hashtable();
            BinaryOperatorFormulaToken.BinaryOperatorsList = new ArrayList();
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Add] = "+";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Sub] = "-";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Mul] = "*";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Div] = "/";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Power] = "^";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Concat] = "&";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Lt] = "<";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Le] = "<=";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Eq] = "=";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Ge] = ">=";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Gt] = ">";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Ne] = "<>";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Isect] = " ";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.List] = ",";
            BinaryOperatorFormulaToken.CodesToStrings[FormulaTokenCode.Range] = ":";
            BinaryOperatorFormulaToken.StringsToCodes["+"] = FormulaTokenCode.Add;
            BinaryOperatorFormulaToken.StringsToCodes["-"] = FormulaTokenCode.Sub;
            BinaryOperatorFormulaToken.StringsToCodes["*"] = FormulaTokenCode.Mul;
            BinaryOperatorFormulaToken.StringsToCodes["/"] = FormulaTokenCode.Div;
            BinaryOperatorFormulaToken.StringsToCodes["^"] = FormulaTokenCode.Power;
            BinaryOperatorFormulaToken.StringsToCodes["&"] = FormulaTokenCode.Concat;
            BinaryOperatorFormulaToken.StringsToCodes["<"] = FormulaTokenCode.Lt;
            BinaryOperatorFormulaToken.StringsToCodes["<="] = FormulaTokenCode.Le;
            BinaryOperatorFormulaToken.StringsToCodes["="] = FormulaTokenCode.Eq;
            BinaryOperatorFormulaToken.StringsToCodes[">="] = FormulaTokenCode.Ge;
            BinaryOperatorFormulaToken.StringsToCodes[">"] = FormulaTokenCode.Gt;
            BinaryOperatorFormulaToken.StringsToCodes["<>"] = FormulaTokenCode.Ne;
            BinaryOperatorFormulaToken.StringsToCodes[" "] = FormulaTokenCode.Isect;
            BinaryOperatorFormulaToken.StringsToCodes[","] = FormulaTokenCode.List;
            BinaryOperatorFormulaToken.StringsToCodes[":"] = FormulaTokenCode.Range;
            string[] textArray1 = new string[] { "+", "-", "*", "/", "^", "&", "<", "<=", "=", ">", ">=", "<>", " ", ",", ":" } ;
            BinaryOperatorFormulaToken.BinaryOperatorsList.AddRange(textArray1);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.BinaryOperatorFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The FormulaTokenCode code.</param>
        public BinaryOperatorFormulaToken(FormulaTokenCode code) : base(code, 1, FormulaTokenType.Binary)
        {
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return (BinaryOperatorFormulaToken.CodesToStrings[(FormulaTokenCode) base.Code] as string);
        }


        // Fields
        public static readonly ArrayList BinaryOperatorsList;
        public static readonly Hashtable CodesToStrings;
        public static readonly Hashtable StringsToCodes;
    }
}

