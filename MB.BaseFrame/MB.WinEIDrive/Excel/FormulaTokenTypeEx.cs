namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///It is wrapper arodung FormulaTokenType enum to provide high-level bool methods
    ///</summary>
    internal class FormulaTokenTypeEx
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.FormulaTokenTypeEx" /> class.
        ///</summary>
        ///<param name="type">The type.</param>
        public FormulaTokenTypeEx(FormulaTokenType type)
        {
            this.type = type;
        }


        // Properties
        ///<summary>
        ///Gets a value indicating whether this instance is binary.
        ///</summary>
        ///<value><c>true</c> if this instance is binary; otherwise, <c>false</c>.</value>
        public bool IsBinary
        {
            get
            {
                return (this.Type == FormulaTokenType.Binary);
            }
        }

        ///<summary>
        ///Gets a value indicating whether this instance is control.
        ///</summary>
        ///<value>
        ///<c>true</c> if this instance is control; otherwise, <c>false</c>.
        ///</value>
        public bool IsControl
        {
            get
            {
                return (this.Type == FormulaTokenType.Control);
            }
        }

        ///<summary>
        ///Gets a value indicating whether this instance is function.
        ///</summary>
        ///<value>
        ///<c>true</c> if this instance is function; otherwise, <c>false</c>.
        ///</value>
        public bool IsFunction
        {
            get
            {
                return (this.Type == FormulaTokenType.Function);
            }
        }

        ///<summary>
        ///Gets a value indicating whether this instance is operand.
        ///</summary>
        ///<value>
        ///<c>true</c> if this instance is operand; otherwise, <c>false</c>.
        ///</value>
        public bool IsOperand
        {
            get
            {
                return (this.Type == FormulaTokenType.Operand);
            }
        }

        ///<summary>
        ///Gets a value indicating whether this instance is unary.
        ///</summary>
        ///<value><c>true</c> if this instance is unary; otherwise, <c>false</c>.</value>
        public bool IsUnary
        {
            get
            {
                return (this.Type == FormulaTokenType.Unary);
            }
        }

        ///<summary>
        ///Gets the formula token type.
        ///</summary>
        ///<value>The formula token type.</value>
        public FormulaTokenType Type
        {
            get
            {
                return this.type;
            }
        }


        // Fields
        private FormulaTokenType type;
    }
}

