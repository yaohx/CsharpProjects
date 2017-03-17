namespace MB.WinEIDrive.Excel
{
    using System;

    internal sealed class FormulaTokensFactory
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.FormulaTokensFactory" /> class.
        ///</summary>
        private FormulaTokensFactory()
        {
        }

        ///<summary>
        ///Creates formula token from rpn bytes and the code read from that bytes.
        ///</summary>
        ///<param name="rpnBytes">The RPN bytes.</param>
        ///<param name="startIndex">The start index to read code from the RPN bytes.</param>
        ///<returns>created formula token</returns>
        public static FormulaToken CreateFrom(byte[] rpnBytes, int startIndex)
        {
            byte num1 = rpnBytes[startIndex];
            FormulaToken token1 = FormulaTokensFactory.CreateFromCode(num1);
            token1.Read(rpnBytes, startIndex + 1);
            return token1;
        }

        ///<summary>
        ///Creates formula token from code.
        ///</summary>
        ///<param name="tokenCode">The token code.</param>
        ///<returns>created formula token</returns>
        public static FormulaToken CreateFromCode(FormulaTokenCode tokenCode)
        {
            FormulaToken token1 = null;
            switch (tokenCode)
            {
                case FormulaTokenCode.Exp:
                case FormulaTokenCode.Tbl:
                case FormulaTokenCode.Attr:
                {
                    return new ControlFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Add:
                case FormulaTokenCode.Sub:
                case FormulaTokenCode.Mul:
                case FormulaTokenCode.Div:
                case FormulaTokenCode.Power:
                case FormulaTokenCode.Concat:
                case FormulaTokenCode.Lt:
                case FormulaTokenCode.Le:
                case FormulaTokenCode.Eq:
                case FormulaTokenCode.Ge:
                case FormulaTokenCode.Gt:
                case FormulaTokenCode.Ne:
                case FormulaTokenCode.Isect:
                case FormulaTokenCode.List:
                case FormulaTokenCode.Range:
                {
                    return new BinaryOperatorFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Uplus:
                case FormulaTokenCode.Uminus:
                case FormulaTokenCode.Percent:
                case FormulaTokenCode.Parentheses:
                {
                    return new UnaryOperatorFormulaToken(tokenCode);
                }
                case FormulaTokenCode.MissArg:
                {
                    return new MissArgFormulaToken();
                }
                case FormulaTokenCode.Str:
                {
                    return new StrFormulaToken();
                }
                case FormulaTokenCode.Err:
                {
                    return new ErrFormulaToken();
                }
                case FormulaTokenCode.Bool:
                {
                    return new BoolFormulaToken();
                }
                case FormulaTokenCode.Int:
                {
                    return new IntFormulaToken();
                }
                case FormulaTokenCode.Num:
                {
                    return new NumFormulaToken();
                }
                case FormulaTokenCode.Array1:
                case FormulaTokenCode.Array2:
                case FormulaTokenCode.Array3:
                {
                    return new ArrayFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Func1:
                case FormulaTokenCode.Func2:
                case FormulaTokenCode.Func3:
                {
                    return new FunctionFormulaToken(tokenCode);
                }
                case FormulaTokenCode.FuncVar1:
                case FormulaTokenCode.FuncVar2:
                case FormulaTokenCode.FuncVar3:
                {
                    return new FunctionVarFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Name1:
                case FormulaTokenCode.Name2:
                case FormulaTokenCode.Name3:
                {
                    return new NameFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Ref1:
                case FormulaTokenCode.Ref2:
                case FormulaTokenCode.Ref3:
                {
                    return new RefFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Area1:
                case FormulaTokenCode.Area2:
                case FormulaTokenCode.Area3:
                {
                    return new AreaFormulaToken(tokenCode);
                }
                case FormulaTokenCode.RefErr1:
                case FormulaTokenCode.RefErr2:
                case FormulaTokenCode.RefErr3:
                {
                    return new RefErrFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Ref3d1:
                case FormulaTokenCode.Ref3d2:
                case FormulaTokenCode.Ref3d3:
                {
                    return new Ref3dFormulaToken(tokenCode);
                }
                case FormulaTokenCode.Area3d1:
                case FormulaTokenCode.Area3d2:
                case FormulaTokenCode.Area3d3:
                {
                    return new Area3dFormulaToken(tokenCode);
                }
                case FormulaTokenCode.RefErr3d1:
                case FormulaTokenCode.RefErr3d2:
                case FormulaTokenCode.RefErr3d3:
                {
                    return new RefErr3dFormulaToken(tokenCode);
                }
                case FormulaTokenCode.AreaErr3d1:
                case FormulaTokenCode.AreaErr3d2:
                case FormulaTokenCode.AreaErr3d3:
                {
                    return new AreaErr3dFormulaToken(tokenCode);
                }
            }
            throw new ArgumentException("We don't support specified formula token: " + tokenCode);
        }

        ///<summary>
        ///Creates formula token from byte code.
        ///</summary>
        ///<param name="code">The byte code.</param>
        ///<returns>created formula token</returns>
        public static FormulaToken CreateFromCode(byte code)
        {
            return FormulaTokensFactory.CreateFromCode((FormulaTokenCode) code);
        }

        ///<summary>
        ///Creates formula token form the name of the function.
        ///</summary>
        ///<param name="name">The name of the function.</param>
        ///<param name="tokenClass">The token class.</param>
        ///<param name="argumentsCount">The arguments count for the function.</param>
        ///<returns>created formula token</returns>
        public static FormulaToken CreateFunctionFromName(string name, FormulaTokenClass tokenClass, byte argumentsCount)
        {
            FormulaToken token1 = null;
            FormulaTokenClass class1;
            object[] objArray1;
            if (!FormulaFunctionsTable.Instance[name].IsFixedArgumentCount)
            {
                class1 = tokenClass;
                switch (class1)
                {
                    case FormulaTokenClass.Reference:
                    {
                        token1 = new FunctionVarFormulaToken(FormulaTokenCode.FuncVar1);
                        goto Label_008C;
                    }
                    case FormulaTokenClass.Variable:
                    {
                        token1 = new FunctionVarFormulaToken(FormulaTokenCode.FuncVar2);
                        goto Label_008C;
                    }
                    case FormulaTokenClass.Array:
                    {
                        token1 = new FunctionVarFormulaToken(FormulaTokenCode.FuncVar3);
                        goto Label_008C;
                    }
                }
            }
            else
            {
                class1 = tokenClass;
                switch (class1)
                {
                    case FormulaTokenClass.Reference:
                    {
                        token1 = new FunctionFormulaToken(FormulaTokenCode.Func1);
                        break;
                    }
                    case FormulaTokenClass.Variable:
                    {
                        token1 = new FunctionFormulaToken(FormulaTokenCode.Func2);
                        break;
                    }
                    case FormulaTokenClass.Array:
                    {
                        token1 = new FunctionFormulaToken(FormulaTokenCode.Func3);
                        break;
                    }
                }
                objArray1 = new object[] { name } ;
                token1.DelayInitialize(objArray1);
                return token1;
            }
        Label_008C:
            objArray1 = new object[2];
            objArray1[0] = name;
            objArray1[1] = argumentsCount;
            token1.DelayInitialize(objArray1);
            return token1;
        }

    }
}

