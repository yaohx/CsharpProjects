namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Reflection;

    ///<summary>
    ///Hold information about all supported functions.
    ///</summary>
    internal class FormulaFunctionsTable
    {
        // Methods
        static FormulaFunctionsTable()
        {
            FormulaFunctionsTable.instance = new FormulaFunctionsTable();
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.FormulaFunctionsTable" /> class.
        ///Constructor is private to allow only creation of FormulaFunctionsTable instances only once.
        ///</summary>
        private FormulaFunctionsTable()
        {
            this.codesToFunctions = new Hashtable();
            this.namesToFunctions = new Hashtable();
            this.names = new ArrayList();
            this.AddFunction(0, "COUNT");
            this.AddFunction(1, "IF");
            this.AddFunction(4, "SUM");
            this.AddFunction(5, "AVERAGE");
            this.AddFunction(6, "MIN");
            this.AddFunction(7, "MAX");
            this.AddFunction(8, "ROW");
            this.AddFunction(9, "COLUMN");
            this.AddFunction(15, "SIN", 1);
            this.AddFunction(0x10, "COS", 1);
            this.AddFunction(0x13, "PI", 0);
            this.AddFunction(20, "SQRT", 1);
            this.AddFunction(0x15, "EXP", 1);
            this.AddFunction(0x16, "LN", 1);
            this.AddFunction(0x18, "ABS", 1);
            this.AddFunction(0x19, "INT", 1);
            this.AddFunction(0x1a, "SIGN", 1);
            this.AddFunction(0x1b, "ROUND", 2);
            this.AddFunction(0x1f, "MID", 3);
            this.AddFunction(0x20, "LEN", 1);
            this.AddFunction(0x21, "VALUE", 1);
            this.AddFunction(0x22, "TRUE", 0);
            this.AddFunction(0x23, "FALSE", 0);
            this.AddFunction(0x24, "AND");
            this.AddFunction(0x25, "OR");
            this.AddFunction(0x26, "NOT", 1);
            this.AddFunction(0x27, "MOD", 2);
            this.AddFunction(0x2e, "VAR");
            this.AddFunction(0x30, "TEXT", 2);
            this.AddFunction(0x3f, "RAND", 0);
            this.AddFunction(0x41, "DATE", 3);
            this.AddFunction(0x42, "TIME", 3);
            this.AddFunction(0x43, "DAY", 1);
            this.AddFunction(0x44, "MONTH", 1);
            this.AddFunction(0x45, "YEAR", 1);
            this.AddFunction(70, "WEEKDAY");
            this.AddFunction(0x47, "HOUR", 1);
            this.AddFunction(0x48, "MINUTE", 1);
            this.AddFunction(0x49, "SECOND", 1);
            this.AddFunction(0x4a, "NOW", 0);
            IEnumerator enumerator1 = this.codesToFunctions.Values.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                FormulaFunctionInfo info1 = enumerator1.Current as FormulaFunctionInfo;
                this.names.Add(info1.Name);
            }
        }

        private void AddFunction(ushort code, string name)
        {
            FormulaFunctionInfo info1 = new FormulaFunctionInfo(code, name);
            this.codesToFunctions[code] = info1;
            this.namesToFunctions[name] = info1;
        }

        private void AddFunction(ushort code, string name, byte argumentsCount)
        {
            FormulaFunctionInfo info1 = new FormulaFunctionInfo(code, name, argumentsCount);
            this.codesToFunctions[code] = info1;
            this.namesToFunctions[name] = info1;
        }

        ///<summary>
        ///Determines whether the specified name is function.
        ///</summary>
        ///<param name="name">The name.</param>
        ///<returns>
        ///<c>true</c> if the specified name is function; otherwise, <c>false</c>.
        ///</returns>
        public bool IsFunction(string name)
        {
            return (this[name] != null);
        }


        // Properties
        ///<summary>
        ///Gets the static FormulaFunctionsTable instance. Used to be shared between FormulaFunctionsTable' users.
        ///</summary>
        ///<value>The singleton FormulaFunctionTable instance.</value>
        public static FormulaFunctionsTable Instance
        {
            get
            {
                return FormulaFunctionsTable.instance;
            }
        }

        ///<summary>
        ///Gets the <see cref="MB.WinEIDrive.Excel.FormulaFunctionInfo" /> at the specified index.
        ///</summary>
        ///<value><see cref="MB.WinEIDrive.Excel.FormulaFunctionInfo" /> instance</value>
        public FormulaFunctionInfo this[string index]
        {
            get
            {
                return (this.namesToFunctions[index.ToUpper()] as FormulaFunctionInfo);
            }
        }

        ///<summary>
        ///Gets the <see cref="MB.WinEIDrive.Excel.FormulaFunctionInfo" /> at the specified index.
        ///</summary>
        ///<value><see cref="MB.WinEIDrive.Excel.FormulaFunctionInfo" /> instance</value>
        public FormulaFunctionInfo this[ushort index]
        {
            get
            {
                return (this.codesToFunctions[index] as FormulaFunctionInfo);
            }
        }

        ///<summary>
        ///Gets the names of predefined Excel functions.
        ///</summary>
        ///<value>The names of prdefined Excel function.</value>
        public ArrayList Names
        {
            get
            {
                return this.names;
            }
        }


        // Fields
        private readonly Hashtable codesToFunctions;
        private static FormulaFunctionsTable instance;
        private readonly ArrayList names;
        private readonly Hashtable namesToFunctions;
    }
}

