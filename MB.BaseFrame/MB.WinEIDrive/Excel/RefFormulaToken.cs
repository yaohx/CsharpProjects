namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Text.RegularExpressions;

    ///<summary>
    ///Formula token for holding reference.
    ///</summary>
    internal class RefFormulaToken : FormulaToken
    {
        // Methods
        static RefFormulaToken()
        {
            RefFormulaToken.regexOptions = RegexOptions.Compiled;
            RefFormulaToken.IsColumnRegex = new Regex(@"(?<Column>[\$]?[A-Z][A-Z]?)", RefFormulaToken.regexOptions);
            RefFormulaToken.IsCellRegex = new Regex(@"(?<Column>[\$]?[A-Z][A-Z]?)(?<Row>[\$]?\d+)", RefFormulaToken.regexOptions);
            RefFormulaToken.AbsoluteCellMark = '$';
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.RefFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The code.</param>
        public RefFormulaToken(FormulaTokenCode code) : base(code, 5, FormulaTokenType.Operand)
        {
        }

        protected RefFormulaToken(FormulaTokenCode code, int size) : base(code, size, FormulaTokenType.Operand)
        {
        }

        public static byte CellToColumn(string value)
        {
            return (byte) ExcelColumnCollection.ColumnNameToIndex(RefFormulaToken.IsCellRegex.Match(value).Groups["Column"].Value);
        }

        public static ushort CellToRow(string value)
        {
            return (ushort) ExcelRowCollection.RowNameToIndex(RefFormulaToken.IsCellRegex.Match(value).Groups["Row"].Value);
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            byte[] buffer2 = BitConverter.GetBytes(this.row);
            buffer2.CopyTo(buffer1, 1);
            buffer1[3] = this.column;
            buffer1[4] = this.options;
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.SetCell(data[0] as string);
        }

        public static bool IsCell(string match)
        {
            bool flag1 = false;
            Match match1 = RefFormulaToken.IsCellRegex.Match(match);
            if (match1.Success && (match1.Value == match))
            {
                flag1 = RefFormulaToken.IsValidCell(match1);
            }
            return flag1;
        }

        public static bool IsCellRange(string match)
        {
            bool flag1 = false;
            Match match1 = AreaFormulaToken.IsCellRangeRegex.Match(match);
            if (match1.Success)
            {
                flag1 = RefFormulaToken.IsColumnValid(match1.Groups["Column1"].Value);
                flag1 = flag1 ? RefFormulaToken.IsColumnValid(match1.Groups["Column2"].Value) : flag1;
            }
            return flag1;
        }

        public static bool IsColumnValid(int match)
        {
            if (match >= 0)
            {
                return (match <= 0xff);
            }
            return false;
        }

        public static bool IsColumnValid(string match)
        {
            return RefFormulaToken.IsColumnRegex.Match(match).Success;
        }

        public static bool IsRowValid(int match)
        {
            if (match >= 0)
            {
                return (match <= 0x10000);
            }
            return false;
        }

        public static bool IsRowValid(string match)
        {
            return RefFormulaToken.IsRowValid(NumbersParser.StrToInt(match));
        }

        private static bool IsValidCell(Match regexMatch)
        {
            string text1 = regexMatch.Groups["Row"].Value;
            if (text1[0] == RefFormulaToken.AbsoluteCellMark)
            {
                text1 = text1.Remove(0, 1);
            }
            string text2 = regexMatch.Groups["Column"].Value;
            if (text2[0] == RefFormulaToken.AbsoluteCellMark)
            {
                text2 = text2.Remove(0, 1);
            }
            if (RefFormulaToken.IsColumnValid(ExcelColumnCollection.ColumnNameToIndex(text2)))
            {
                return RefFormulaToken.IsRowValid(ExcelRowCollection.RowNameToIndex(text1));
            }
            return false;
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.row = BitConverter.ToUInt16(rpnBytes, startIndex);
            this.column = rpnBytes[startIndex + 2];
            this.options = rpnBytes[startIndex + 3];
        }

        private void SetCell(string cell)
        {
            Match match1 = RefFormulaToken.IsCellRegex.Match(cell);
            string text1 = match1.Groups["Row"].Value;
            string text2 = match1.Groups["Column"].Value;
            this.SetCell(text1, text2);
        }

        protected void SetCell(string row, string column)
        {
            this.SetRow(row);
            this.SetColumn(column);
        }

        private void SetColumn(string column)
        {
            if (column[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsColumnRelative = false;
                column = column.Substring(1);
            }
            else
            {
                this.IsColumnRelative = true;
            }
            this.column = (byte) ExcelColumnCollection.ColumnNameToIndex(column);
        }

        private void SetRow(string row)
        {
            if (row[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsRowRelative = false;
                row = row.Substring(1);
            }
            else
            {
                this.IsRowRelative = true;
            }
            this.row = (ushort) ExcelRowCollection.RowNameToIndex(row);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            string text1 = this.IsRowRelative ? ExcelRowCollection.RowIndexToName(this.row) : (RefFormulaToken.AbsoluteCellMark + ExcelRowCollection.RowIndexToName(this.row));
            string text2 = this.IsColumnRelative ? ExcelColumnCollection.ColumnIndexToName(this.column) : (RefFormulaToken.AbsoluteCellMark + ExcelColumnCollection.ColumnIndexToName(this.column));
            return (text2 + text1);
        }


        // Properties
        public byte Column
        {
            get
            {
                return this.column;
            }
        }

        public bool IsColumnRelative
        {
            get
            {
                return Utilities.IsBitSetted(this.options, 0x40);
            }
            set
            {
                this.options = Utilities.SetBit(this.options, 0x40, value);
            }
        }

        public bool IsRowRelative
        {
            get
            {
                return Utilities.IsBitSetted(this.options, 0x80);
            }
            set
            {
                this.options = Utilities.SetBit(this.options, 0x80, value);
            }
        }

        public ushort Row
        {
            get
            {
                return this.row;
            }
        }


        // Fields
        ///<summary>
        ///Absolute preffix row\height symbol
        ///</summary>
        public static readonly char AbsoluteCellMark;
        protected byte column;
        ///<summary>
        ///Bit mask for column options.
        ///</summary>
        public const byte ColumnBitMask = 0x40;
        ///<summary>
        ///Regular expression used to determinate whether the input string is cell or not
        ///</summary>
        public static readonly Regex IsCellRegex;
        ///<summary>
        ///Regular expression used to determinate whether the input string is column or not
        ///</summary>
        public static readonly Regex IsColumnRegex;
        protected byte options;
        ///<summary>
        ///Regular expression default options
        ///</summary>
        private static RegexOptions regexOptions;
        protected ushort row;
        ///<summary>
        ///Bit mask for row options.
        ///</summary>
        public const byte RowBitMask = 0x80;
    }
}

