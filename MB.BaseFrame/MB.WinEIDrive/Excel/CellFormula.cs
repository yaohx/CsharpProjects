namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Text;

    internal class CellFormula
    {
        // Methods
        internal CellFormula(string formula, ExcelWorksheet sheet)
        {
            this.formula = formula;
            this.Parse(formula, sheet);
        }

        internal CellFormula(object[] resultBytes, FormulaOptions options, object[] rpnBytes)
        {
            this.ResultBytes = resultBytes;
            this.Options = options;
            this.RpnBytes = rpnBytes;
        }

        private object[] ConvertToBytes(FormulaToken[] tokens)
        {
            ArrayList list1 = new ArrayList();
            int num1 = 0;
            for (int num2 = 0; num2 < tokens.Length; num2++)
            {
                byte[] buffer1 = tokens[num2].ConvertToBytes();
                list1.Add(buffer1);
                num1 += buffer1.Length;
            }
            object[] objArray1 = new object[num1];
            int num3 = 0;
            for (int num4 = 0; num4 < list1.Count; num4++)
            {
                byte[] buffer2 = list1[num4] as byte[];
                Array.Copy(buffer2, 0, objArray1, num3, buffer2.Length);
                num3 += buffer2.Length;
            }
            return objArray1;
        }

        private void ConvertToInternalStorage(object[] formula)
        {
            this.ResultBytes = new object[8];
            Array.Copy(formula, 0, this.ResultBytes, 0, 8);
            this.Value = this.DecodeValue();
            byte[] buffer1 = new byte[2];
            Array.Copy(formula, 8, buffer1, 0, 2);
            this.Options = (FormulaOptions) ((ushort) BitConverter.ToUInt32(buffer1, 0));
            int num1 = formula.Length - 0x10;
            this.RpnBytes = new object[num1];
            Array.Copy(formula, 0x10, buffer1, 0, num1);
        }

        private string ConvertToString(FormulaToken[] tokens)
        {
            Stack stack1 = new Stack();
            bool flag1 = false;
            if (tokens != null)
            {
                this.TokensToString(stack1, tokens);
            }
            else
            {
                flag1 = this.TokenBytesToString(stack1);
            }
            Trace.Assert(flag1 || (stack1.Count == 1), "In operands stack must be 1 string: human representation of rpn tokens.");
            Trace.Assert(flag1 || (stack1.Peek().GetType() == typeof(string)), "In operands stack must be 1 string: human representation of rpn tokens.");
            if (stack1.Count != 0)
            {
                return stack1.Peek().ToString();
            }
            return string.Empty;
        }

        private bool DecodeBoolValue()
        {
            return (((byte) this.ResultBytes[2]) == 1);
        }

        private double DecodeDoubleValue()
        {
            byte[] buffer1 = new byte[8];
            Array.Copy(this.ResultBytes, 0, buffer1, 0, 8);
            return BitConverter.ToDouble(buffer1, 0);
        }

        private string DecodeErrorValue()
        {
            byte num1 = (byte) this.ResultBytes[2];
            if (num1 <= 15)
            {
                if (num1 == 0)
                {
                    return "#NULL!";
                }
                if (num1 == 7)
                {
                    return "#DIV/0!";
                }
                if (num1 == 15)
                {
                    return "#VALUE!";
                }
            }
            else if (num1 <= 0x1d)
            {
                if (num1 == 0x17)
                {
                    return "#REF!";
                }
                if (num1 == 0x1d)
                {
                    return "#NAME?";
                }
            }
            else
            {
                if (num1 == 0x24)
                {
                    return "#NUM!";
                }
                if (num1 == 0x2a)
                {
                    return "#N/A!";
                }
            }
            return "#ERROR!";
        }

        internal object DecodeValue()
        {
            if ((((byte) this.ResultBytes[6]) == 0xff) && (((byte) this.ResultBytes[7]) == 0xff))
            {
                switch (((byte) this.ResultBytes[0]))
                {
                    case 0:
                    case 3:
                    {
                        return null;
                    }
                    case 1:
                    {
                        return this.DecodeBoolValue();
                    }
                    case 2:
                    {
                        return this.DecodeErrorValue();
                    }
                }
            }
            return this.DecodeDoubleValue();
        }

        private void Parse(string formula, ExcelWorksheet sheet)
        {
            FormulaParser parser1 = new FormulaParser(sheet);
            FormulaToken[] tokenArray1 = parser1.Parse(formula);
            this.tokens = tokenArray1;
            this.SetDefaultResultAndOptions();
        }

        private static void ProcessBinaryOperator(FormulaToken token, Stack operandsStack)
        {
            string text1 = operandsStack.Pop() as string;
            string text2 = operandsStack.Pop() as string;
            operandsStack.Push(text2 + token.ToString() + text1);
        }

        private static void ProcessFunction(FormulaToken token, Stack operandsStack)
        {
            byte num1 = (token is FunctionFormulaToken) ? (token as FunctionFormulaToken).ArgumentsCount : (token as FunctionVarFormulaToken).ArgumentsCount;
            if (!(token is FunctionFormulaToken))
            {
                ushort num4 = (token as FunctionVarFormulaToken).Function.Code;
            }
            else
            {
                ushort num5 = (token as FunctionFormulaToken).Function.Code;
            }
            StringBuilder builder1 = new StringBuilder();
            builder1.Append(token.ToString());
            builder1.Append("(");
            string[] textArray1 = new string[num1];
            for (byte num2 = 0; num2 < num1; num2 = (byte) (num2 + 1))
            {
                textArray1[num2] = operandsStack.Pop() as string;
            }
            for (byte num3 = num1; num3 > 0; num3 = (byte) (num3 - 1))
            {
                string text1 = textArray1[num3 - 1];
                builder1.Append(text1);
                if (num3 != 1)
                {
                    builder1.Append(";");
                }
            }
            builder1.Append(")");
            operandsStack.Push(builder1.ToString());
        }

        private static void ProcessOperand(FormulaToken token, Stack operandsStack)
        {
            operandsStack.Push(token.ToString());
        }

        private static void ProcessToken(FormulaToken token, Stack operandsStack)
        {
            if (token.Type.IsUnary)
            {
                CellFormula.ProcessUnaryOperator(token, operandsStack);
            }
            else if (token.Type.IsBinary)
            {
                CellFormula.ProcessBinaryOperator(token, operandsStack);
            }
            else if (token.Type.IsOperand)
            {
                CellFormula.ProcessOperand(token, operandsStack);
            }
            else if (token.Type.IsFunction)
            {
                CellFormula.ProcessFunction(token, operandsStack);
            }
            else if (!token.Type.IsControl)
            {
                throw new ArgumentException("Invalid RPN token code.");
            }
        }

        private static void ProcessUnaryOperator(FormulaToken token, Stack operandsStack)
        {
            string text1 = operandsStack.Pop() as string;
            if (token.Code == 20)
            {
                operandsStack.Push(text1 + token.ToString());
            }
            else if (token.Code == 0x15)
            {
                operandsStack.Push("(" + text1 + ")");
            }
            else
            {
                operandsStack.Push(token.ToString() + text1);
            }
        }

        ///<summary>
        ///Recalculate formula based on saved tokens.
        ///It need to be done for changing some data which can be changed after setting formula
        ///and before saving xls file.
        ///</summary>
        internal void Recalculate()
        {
            if (this.tokens != null)
            {
                this.RpnBytes = this.ConvertToBytes(this.tokens);
            }
        }

        private void SetDefaultResultAndOptions()
        {
            this.Value = 0;
            byte num1 = 0;
            object[] objArray1 = new object[] { num1, num1, num1, num1, num1, num1, num1, num1 } ;
            this.ResultBytes = objArray1;
            this.Options = FormulaOptions.CalculateOnLoad;
        }

        private bool TokenBytesToString(Stack operandsStack)
        {
            byte[] buffer1 = new byte[this.RpnBytes.Length];
            Array.Copy(this.RpnBytes, 0, buffer1, 0, this.RpnBytes.Length);
            int num1 = 0;
            bool flag1 = false;
            bool flag2 = false;
            while (num1 < this.RpnBytes.Length)
            {
                FormulaToken token1 = FormulaTokensFactory.CreateFrom(buffer1, num1);
                if (token1.Type.IsControl)
                {
                    flag2 = true;
                }
                else
                {
                    flag1 = true;
                }
                CellFormula.ProcessToken(token1, operandsStack);
                num1 += token1.Size;
            }
            if (!flag1)
            {
                return flag2;
            }
            return false;
        }

        private void TokensToString(Stack operandsStack, FormulaToken[] tokens)
        {
            for (int num1 = 0; num1 < tokens.Length; num1++)
            {
                FormulaToken token1 = tokens[num1];
                CellFormula.ProcessToken(token1, operandsStack);
            }
        }


        // Properties
        internal string Formula
        {
            get
            {
                if (this.formula == null)
                {
                    this.formula = this.ConvertToString(null);
                }
                return this.formula;
            }
        }


        // Fields
        private const int bytesCountBeforeTokens = 0x10;
        internal ArrayList ExtraFormulaRecords;
        private string formula;
        internal FormulaOptions Options;
        internal object[] ResultBytes;
        internal object[] RpnBytes;
        private FormulaToken[] tokens;
        internal object Value;
    }
}

