namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Text;

    internal class FormulaParser
    {
        // Methods
        static FormulaParser()
        {
            string[] textArray1 = new string[] { "TRUE", "FALSE" } ;
            FormulaParser.boolList = new ArrayList(textArray1);
        }

        public FormulaParser(ExcelWorksheet sheet)
        {
            this.tokens = new ArrayList();
            this.isFunctionArgumentsProcessed = false;
            this.worksheet = sheet;
        }

        private void AddArea(string value)
        {
            this.AddToken(FormulaTokenCode.Area1, value);
        }

        private void AddBoolToken(string boolValue)
        {
            if (this.GetNextOnDemand('('))
            {
                this.Match(')');
                this.AddToken(FormulaTokensFactory.CreateFunctionFromName(boolValue, FormulaTokenClass.Reference, 0));
            }
            else
            {
                this.AddToken(FormulaTokenCode.Bool, bool.Parse(boolValue));
            }
        }

        private void AddCellOrRangeToken(string cellValue)
        {
            if (this.buffer.Peek() == RefFormulaToken.AbsoluteCellMark)
            {
                cellValue = this.GetCell();
            }
            if (this.GetNextOnDemand(':'))
            {
                string text1 = this.GetCell();
                if (RefFormulaToken.IsCell(text1))
                {
                    this.AddArea(cellValue + ":" + text1);
                }
                else
                {
                    this.Expected("Area.");
                }
            }
            else if (this.isFunctionArgumentsProcessed)
            {
                this.AddToken(FormulaTokenCode.Ref1, cellValue);
            }
            else
            {
                this.AddToken(FormulaTokenCode.Ref2, cellValue);
            }
        }

        private void AddErrorToken(string errorValue)
        {
            char[] chArray1 = new char[] { '!', '?' } ;
            errorValue = '#' + this.buffer.GetNextString(chArray1) + this.buffer.GetNext();
            if (!ErrFormulaToken.ErrorsList.Contains(errorValue.ToUpper()))
            {
                this.Expected("Error");
            }
            this.AddToken(FormulaTokenCode.Err, errorValue);
        }

        private void AddExpressionToken()
        {
            this.Expression();
            this.Match(')');
            this.AddToken(FormulaTokenCode.Parentheses);
        }

        private void AddFloatOrIntegerToken(string value)
        {
            if (value.Length != 0)
            {
                double num1 = NumbersParser.StrToDouble(value);
                if (NumbersParser.IsUshort(num1))
                {
                    this.AddToken(FormulaTokenCode.Int, (ushort) num1);
                }
                else
                {
                    this.AddToken(FormulaTokenCode.Num, num1);
                }
            }
        }

        private void AddFunctionToken(string functionValue)
        {
            this.isFunctionArgumentsProcessed = true;
            byte num1 = this.ArgumentList();
            this.isFunctionArgumentsProcessed = false;
            this.GetNextOnDemand(')');
            FormulaFunctionInfo info1 = FormulaFunctionsTable.Instance[functionValue];
            byte num2 = info1.ArgumentsCount;
            if (num2 != 0xff)
            {
                string text1 = (num2 == 1) ? " argument." : " arguments.";
                if (num1 != num2)
                {
                    object[] objArray1 = new object[] { "Function: ", FormulaFunctionsTable.Instance[info1.Code].Name, " expects ", num2, text1 } ;
                    this.NotifyError(string.Concat(objArray1));
                }
            }
            this.AddToken(FormulaTokensFactory.CreateFunctionFromName(functionValue, FormulaTokenClass.Variable, num1));
        }

        private void AdditiveExpression()
        {
            this.MultiplicativeExpression();
            while ((this.buffer.Peek() == '+') || (this.buffer.Peek() == '-'))
            {
                char ch1 = this.buffer.Peek();
                this.buffer.GetNext();
                this.ResetCounter();
                this.MultiplicativeExpression();
                this.ResetCounter("Operand for binary operator.");
                if (ch1 == '+')
                {
                    this.AddToken(FormulaTokenCode.Add);
                    continue;
                }
                this.AddToken(FormulaTokenCode.Sub);
            }
        }

        private void AddNamedRange(string namedRange)
        {
            object[] objArray1;
            if (this.isFunctionArgumentsProcessed)
            {
                objArray1 = new object[] { namedRange, this.worksheet } ;
                this.AddToken(FormulaTokenCode.Name1, objArray1);
            }
            else
            {
                objArray1 = new object[] { namedRange, this.worksheet } ;
                this.AddToken(FormulaTokenCode.Name2, objArray1);
            }
        }

        private void AddSheetReferenceToken(string sheet)
        {
            object[] objArray1;
            string text1 = this.GetCell();
            if (text1 == string.Empty)
            {
                this.Expected("3d sheet cell reference.");
            }
            sheet = sheet + "!" + text1;
            if (this.GetNextOnDemand(':'))
            {
                string text2 = this.GetCell();
                if (RefFormulaToken.IsCell(text2))
                {
                    objArray1 = new object[] { sheet + ":" + text2, this.worksheet.Parent } ;
                    this.AddToken(FormulaTokenCode.Area3d2, objArray1);
                }
                else
                {
                    this.Expected("3d area reference.");
                }
            }
            else
            {
                objArray1 = new object[] { sheet, this.worksheet.Parent } ;
                this.AddToken(FormulaTokenCode.Ref3d2, objArray1);
            }
        }

        private void AddStringToken()
        {
            this.AddToken(FormulaTokenCode.Str, this.buffer.GetNextString('"'));
            this.Match('"');
        }

        public void AddToken(FormulaToken token)
        {
            this.tokens.Add(token);
        }

        public void AddToken(FormulaTokenCode code)
        {
            this.tokens.Add(FormulaTokensFactory.CreateFromCode(code));
        }

        public void AddToken(FormulaTokenCode code, object data)
        {
            object[] objArray1 = new object[] { data } ;
            this.AddToken(code, objArray1);
        }

        public void AddToken(FormulaTokenCode code, object[] data)
        {
            FormulaToken token1 = FormulaTokensFactory.CreateFromCode(code);
            this.tokens.Add(token1);
            token1.DelayInitialize(data);
        }

        private byte ArgumentList()
        {
            byte num1 = 0;
            bool flag1 = false;
            while (true)
            {
                flag1 = false;
                this.buffer.SkipWhitespaces();
                int num2 = this.buffer.Pos;
                if (this.GetNextOnDemand(','))
                {
                    this.AddToken(FormulaTokenCode.MissArg);
                    num1 = (byte) (num1 + 1);
                    flag1 = true;
                }
                else
                {
                    this.PrimaryExpression();
                    if (this.buffer.Pos > num2)
                    {
                        num1 = (byte) (num1 + 1);
                    }
                }
                if (!flag1 && !this.GetNextOnDemand(','))
                {
                    return num1;
                }
            }
        }

        private void ConcatExpression()
        {
            this.AdditiveExpression();
            while (this.GetNextOnDemand('&'))
            {
                this.ResetCounter();
                this.AdditiveExpression();
                this.ResetCounter("Operand for binary operator.");
                this.AddToken(FormulaTokenCode.Concat);
            }
        }

        private void Expected(char what)
        {
            this.Expected(what.ToString());
        }

        private void Expected(string what)
        {
            this.NotifyError("Expected: " + what);
        }

        private void ExponentiationExpression()
        {
            this.PercentExpression();
            while (this.GetNextOnDemand('^'))
            {
                this.ResetCounter();
                this.PercentExpression();
                this.ResetCounter("Operand for binary operator.");
                this.AddToken(FormulaTokenCode.Power);
            }
        }

        private void Expression()
        {
            this.ConcatExpression();
        Label_0006:
            if (this.buffer.Peek() == '=')
            {
                this.buffer.GetNext();
                this.ResetCounter();
                this.ConcatExpression();
                this.ResetCounter("Operand for binary operator.");
                this.AddToken(FormulaTokenCode.Eq);
                goto Label_0006;
            }
            if (this.buffer.Peek() == '<')
            {
                char ch1 = '<';
                this.buffer.GetNext();
                if (((this.buffer.Peek() == '>') || (this.buffer.Peek() == '>')) || (this.buffer.Peek() == '='))
                {
                    ch1 = this.buffer.Peek();
                    this.buffer.GetNext();
                }
                this.ResetCounter();
                this.ConcatExpression();
                this.ResetCounter("Operand for binary operator.");
                if (ch1 == '=')
                {
                    this.AddToken(FormulaTokenCode.Le);
                    goto Label_0006;
                }
                if (ch1 == '>')
                {
                    this.AddToken(FormulaTokenCode.Ne);
                    goto Label_0006;
                }
                this.AddToken(FormulaTokenCode.Lt);
                goto Label_0006;
            }
            if (this.buffer.Peek() == '>')
            {
                char ch2 = '>';
                this.buffer.GetNext();
                if (this.buffer.Peek() == '=')
                {
                    ch2 = '=';
                    this.buffer.GetNext();
                }
                this.ResetCounter();
                this.ConcatExpression();
                this.ResetCounter("Operand for binary operator.");
                if (ch2 == '=')
                {
                    this.AddToken(FormulaTokenCode.Ge);
                    goto Label_0006;
                }
                this.AddToken(FormulaTokenCode.Gt);
                goto Label_0006;
            }
        }

        private void Formula()
        {
            this.Match('=');
            if (this.GetNextOnDemand('{'))
            {
                this.NotifyError("We don't support array formula.");
            }
            else
            {
                this.PrimaryExpression();
            }
            if (!this.buffer.IsEOF)
            {
                this.Expected("Operand for primary expression.");
            }
        }

        private FormulaTokenCode GetBinaryOperator()
        {
            if (this.buffer.Peek(1) != '@')
            {
                char ch1 = this.buffer.Peek();
                if (BinaryOperatorFormulaToken.BinaryOperatorsList.Contains(ch1.ToString()))
                {
                    FormulaTokenCode code1 = (FormulaTokenCode) BinaryOperatorFormulaToken.StringsToCodes[ch1.ToString()];
                    this.buffer.GetNext();
                    return code1;
                }
                if (this.buffer.Peek(1) == '@')
                {
                    return FormulaTokenCode.Empty;
                }
                char ch2 = this.buffer.Peek(1);
                int num1 = ch1 + ch2;
                if (BinaryOperatorFormulaToken.BinaryOperatorsList.Contains(num1.ToString()))
                {
                    num1 = ch1 + ch2;
                    FormulaTokenCode code2 = (FormulaTokenCode) BinaryOperatorFormulaToken.StringsToCodes[num1.ToString()];
                    this.buffer.GetNext();
                    this.buffer.GetNext();
                    return code2;
                }
            }
            return FormulaTokenCode.Empty;
        }

        private string GetCell()
        {
            string text1 = string.Empty;
            if (this.GetNextOnDemand(RefFormulaToken.AbsoluteCellMark))
            {
                text1 = RefFormulaToken.AbsoluteCellMark + this.buffer.GetNextString(false);
            }
            else
            {
                text1 = this.buffer.GetNextString(false);
            }
            if (this.GetNextOnDemand(RefFormulaToken.AbsoluteCellMark, false))
            {
                text1 = text1 + RefFormulaToken.AbsoluteCellMark + this.buffer.GetNextString(false);
            }
            if (!RefFormulaToken.IsCell(text1))
            {
                this.Expected("Cell.");
            }
            return text1;
        }

        private string GetInnerString()
        {
            StringBuilder builder1 = new StringBuilder();
            while (char.IsLetterOrDigit(this.buffer.Peek()) && !this.buffer.IsEOF)
            {
                builder1.Append(this.buffer.GetNext());
            }
            return builder1.ToString();
        }

        private ushort GetLastTokenCode()
        {
            if (this.tokens.Count != 0)
            {
                return (this.tokens[this.tokens.Count - 1] as FormulaToken).Code;
            }
            return 0;
        }

        private bool GetNextOnDemand(char[] matches)
        {
            return (this.buffer.GetNextOnDemand(matches) != '@');
        }

        private bool GetNextOnDemand(char match)
        {
            return this.GetNextOnDemand(match, true);
        }

        private bool GetNextOnDemand(char match, bool skipWhitespaces)
        {
            return (this.buffer.GetNextOnDemand(match, skipWhitespaces) != '@');
        }

        private void InitBuffer(string formula)
        {
            this.buffer = new MB.WinEIDrive.Excel.Buffer(formula);
            this.buffer.SkipWhitespaces();
        }

        private void IntersectionExpression()
        {
            this.ReferenceExpression();
            while ((AreaFormulaToken.IsAreaToken(this.GetLastTokenCode()) && !this.isFunctionArgumentsProcessed) && this.GetNextOnDemand(' ', false))
            {
                this.ResetCounter();
                this.ReferenceExpression();
                this.ResetCounter("Operand for intersect operator.");
                this.AddToken(FormulaTokenCode.Isect);
            }
        }

        private bool IsCell(string cellValue)
        {
            if (this.buffer.Peek() != RefFormulaToken.AbsoluteCellMark)
            {
                return RefFormulaToken.IsCell(cellValue);
            }
            return true;
        }

        private bool IsError()
        {
            return this.GetNextOnDemand('#');
        }

        private static bool IsFloatOrInteger(string floatValue)
        {
            if (floatValue.Length <= 0)
            {
                return false;
            }
            if (!char.IsDigit(floatValue[0]))
            {
                return (floatValue[0] == '.');
            }
            return true;
        }

        private bool IsFunction(string name)
        {
            if ((name == null) || (name.Length == 0))
            {
                return false;
            }
            bool flag1 = FormulaFunctionsTable.Instance.IsFunction(name);
            if (flag1)
            {
                this.Match('(', false);
            }
            return flag1;
        }

        private bool IsNamedRange(string namedRange)
        {
            return Utilities.Contains(this.worksheet.NamedRanges.Names, namedRange);
        }

        private bool IsSheetReference(string sheet)
        {
            if (Utilities.Contains(this.worksheet.Parent.SheetNames, sheet))
            {
                return this.GetNextOnDemand('!');
            }
            return false;
        }

        private bool IsString()
        {
            return this.GetNextOnDemand('"');
        }

        private void Match(char match)
        {
            this.Match(match, true);
        }

        private void Match(char match, bool skipWhitespaces)
        {
            if (skipWhitespaces)
            {
                this.buffer.SkipWhitespaces();
            }
            if (this.buffer.Peek() != match)
            {
                this.Expected(match);
            }
            else
            {
                this.buffer.GetNext();
            }
        }

        private void MultiplicativeExpression()
        {
            this.ExponentiationExpression();
            while ((this.buffer.Peek() == '*') || (this.buffer.Peek() == '/'))
            {
                char ch1 = this.buffer.Peek();
                this.buffer.GetNext();
                this.ResetCounter();
                this.ExponentiationExpression();
                this.ResetCounter("Operand for binary operator.");
                if (ch1 == '*')
                {
                    this.AddToken(FormulaTokenCode.Mul);
                    continue;
                }
                this.AddToken(FormulaTokenCode.Div);
            }
        }

        private void NotifyError(string what)
        {
            throw new ArgumentException("Failed to parse: " + this.buffer.Data + ". Error: " + what);
        }

        public FormulaToken[] Parse(string formula)
        {
            this.InitBuffer(formula);
            this.Formula();
            return (FormulaToken[]) this.tokens.ToArray(typeof(FormulaToken));
        }

        private void PercentExpression()
        {
            this.UnaryExpression();
            while (this.GetNextOnDemand('%'))
            {
                this.isProcentOperatorProcessed = true;
                this.AddToken(FormulaTokenCode.Percent);
                this.UnaryExpression();
                this.isProcentOperatorProcessed = false;
            }
        }

        private void PrimaryExpression()
        {
            if (this.GetNextOnDemand('('))
            {
                this.Expression();
                this.Match(')');
                this.AddToken(FormulaTokenCode.Parentheses);
                while (true)
                {
                    FormulaTokenCode code1 = this.GetBinaryOperator();
                    if (code1 == FormulaTokenCode.Empty)
                    {
                        return;
                    }
                    this.ResetCounter();
                    this.PrimaryExpression();
                    this.ResetCounter("Operand for binary operator.");
                    this.AddToken(code1);
                }
            }
            this.Expression();
        }

        private void ProcessReferenceExpressionError(string nextString)
        {
            if (((!this.isFunctionArgumentsProcessed || (nextString.Length != 0)) || (this.buffer.Peek() != ')')) && !this.isProcentOperatorProcessed)
            {
                if (this.buffer.Peek() == '(')
                {
                    this.NotifyError("Unsupported function: " + nextString + ".For list of supported functions consult ExcelLite documentation.");
                }
                else if (this.buffer.Peek() == '@')
                {
                    this.NotifyError("Not expected end of file");
                }
                else if (nextString.Length > 0)
                {
                    this.NotifyError("Not expected: " + nextString);
                }
                else
                {
                    this.NotifyError("Not expected: " + this.buffer.Peek());
                }
            }
        }

        private void ReferenceExpression()
        {
            if (!this.buffer.IsEOF)
            {
                char ch1 = this.buffer.Peek();
                if (ch1 == '{')
                {
                    this.NotifyError("We don't support const array.");
                }
                else if (this.GetNextOnDemand('('))
                {
                    this.AddExpressionToken();
                }
                else
                {
                    string text1 = this.buffer.GetNextString(false);
                    bool flag1 = FormulaParser.boolList.Contains(text1.ToUpper());
                    if (this.IsNamedRange(text1))
                    {
                        this.AddNamedRange(text1);
                    }
                    else if (this.IsSheetReference(text1))
                    {
                        this.AddSheetReferenceToken(text1);
                    }
                    else if (flag1)
                    {
                        this.AddBoolToken(text1);
                    }
                    else if (this.IsFunction(text1))
                    {
                        this.AddFunctionToken(text1);
                    }
                    else if (this.IsCell(text1))
                    {
                        this.AddCellOrRangeToken(text1);
                    }
                    else if (FormulaParser.IsFloatOrInteger(text1))
                    {
                        this.AddFloatOrIntegerToken(text1);
                    }
                    else if (this.IsString())
                    {
                        this.AddStringToken();
                    }
                    else if (this.IsError())
                    {
                        this.AddErrorToken(text1);
                    }
                    else
                    {
                        this.ProcessReferenceExpressionError(text1);
                    }
                }
            }
        }

        private void ResetCounter()
        {
            this.buffer.SkipWhitespaces();
            this.lastPos = this.buffer.Pos;
        }

        private void ResetCounter(string error)
        {
            if (this.buffer.Pos == this.lastPos)
            {
                this.Expected(error);
            }
        }

        private void UnaryExpression()
        {
            if (!this.buffer.IsEOF)
            {
                ArrayList list1 = new ArrayList();
                while (this.UnaryOperator(list1))
                {
                }
                this.ResetCounter();
                this.UnionExpression();
                if (list1.Count > 0)
                {
                    this.ResetCounter("Operand for unary operator.");
                }
                list1.Reverse();
                for (int num1 = 0; num1 < list1.Count; num1++)
                {
                    char ch1 = (char) list1[num1];
                    if (ch1 == '+')
                    {
                        this.AddToken(FormulaTokenCode.Uplus);
                    }
                    else
                    {
                        this.AddToken(FormulaTokenCode.Uminus);
                    }
                }
            }
        }

        private bool UnaryOperator(ArrayList unaryOperators)
        {
            bool flag1 = false;
            if (this.GetNextOnDemand('+'))
            {
                unaryOperators.Add('+');
                return true;
            }
            if (this.GetNextOnDemand('-'))
            {
                unaryOperators.Add('-');
                flag1 = true;
            }
            return flag1;
        }

        private void UnionExpression()
        {
            this.IntersectionExpression();
            while ((AreaFormulaToken.IsAreaToken(this.GetLastTokenCode()) && !this.isFunctionArgumentsProcessed) && this.GetNextOnDemand(','))
            {
                this.ResetCounter();
                this.IntersectionExpression();
                this.ResetCounter("Operand for union operator.");
                this.AddToken(FormulaTokenCode.List);
            }
        }


        // Fields
        private static readonly ArrayList boolList;
        private MB.WinEIDrive.Excel.Buffer buffer;
        private bool isFunctionArgumentsProcessed;
        private bool isProcentOperatorProcessed;
        private int lastPos;
        private ArrayList tokens;
        private ExcelWorksheet worksheet;
    }
}

