namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Hold information about function( name, code, expected arguments count. )
    ///</summary>
    internal class FormulaFunctionInfo
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.FormulaFunctionInfo" /> class.
        ///</summary>
        ///<param name="code">The function code.</param>
        ///<param name="name">The function name.</param>
        public FormulaFunctionInfo(ushort code, string name) : this(code, name, 0xff)
        {
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.FormulaFunctionInfo" /> class.
        ///</summary>
        ///<param name="code">The function code.</param>
        ///<param name="name">The function name.</param>
        ///<param name="argumentsCount">The function's arguments count.</param>
        public FormulaFunctionInfo(ushort code, string name, byte argumentsCount)
        {
            this.argumentsCount = 0xff;
            this.code = code;
            this.name = name;
            this.argumentsCount = argumentsCount;
        }


        // Properties
        ///<summary>
        ///Arguments count value, by default it is initilized with not fixed( variable ) argument count mark.
        ///</summary>
        public byte ArgumentsCount
        {
            get
            {
                return this.argumentsCount;
            }
        }

        ///<summary>
        ///Gets function code.
        ///</summary>
        ///<value>The function code.</value>
        public ushort Code
        {
            get
            {
                return this.code;
            }
        }

        ///<summary>
        ///Gets a value indicating whether function has fixed argument count.
        ///</summary>
        ///<value>
        ///<c>true</c> if this function has fixed argument count; otherwise, <c>false</c>.
        ///</value>
        public bool IsFixedArgumentCount
        {
            get
            {
                return (this.ArgumentsCount != 0xff);
            }
        }

        ///<summary>
        ///Gets function name.
        ///</summary>
        ///<value>Function name.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }


        // Fields
        ///<summary>
        ///Arguments count value, by default it is initilized with not fixed( variable ) argument count mark.
        ///</summary>
        private byte argumentsCount;
        private ushort code;
        private string name;
        ///<summary>
        ///Is used to the specify for appropriate functins the variable count of arguments
        ///</summary>
        public const byte VariableArgumentAmountMark = 0xff;
    }
}

