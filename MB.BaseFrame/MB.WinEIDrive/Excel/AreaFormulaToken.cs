namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    ///<summary>
    ///Formula token for holding reference on cell range.
    ///</summary>
    internal class AreaFormulaToken : FormulaToken
    {
        // Methods
        static AreaFormulaToken()
        {
            AreaFormulaToken.regexOptions = RegexOptions.Compiled;
            AreaFormulaToken.IsCellRangeRegex = new Regex(@"(?<Column1>[\$]?[A-Z][A-Z]?)(?<Row1>[\$]?\d+):(?<Column2>[\$]?[A-Z][A-Z]?)(?<Row2>[\$]?\d+)", AreaFormulaToken.regexOptions);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.AreaFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The FormulaTokenCode code.</param>
        public AreaFormulaToken(FormulaTokenCode code) : base(code, 9, FormulaTokenType.Operand)
        {
        }

        protected AreaFormulaToken(FormulaTokenCode code, int size) : base(code, size, FormulaTokenType.Operand)
        {
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            byte[] buffer2 = BitConverter.GetBytes(this.FirstRow);
            buffer2.CopyTo(buffer1, 1);
            byte[] buffer3 = BitConverter.GetBytes(this.lastRow);
            buffer3.CopyTo(buffer1, 3);
            buffer1[5] = this.FirstColumn;
            buffer1[6] = this.firstOptions;
            buffer1[7] = this.lastColumn;
            buffer1[8] = this.lastOptions;
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.SetArea(data[0] as string);
        }

        public static bool IsAreaToken(ushort codeValue)
        {
            FormulaTokenCode code1 = (FormulaTokenCode) ((byte) codeValue);
            if ((code1 != FormulaTokenCode.Area1) && (code1 != FormulaTokenCode.Area2))
            {
                return (code1 == FormulaTokenCode.Area3);
            }
            return true;
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.firstRow = BitConverter.ToUInt16(rpnBytes, startIndex);
            this.lastRow = BitConverter.ToUInt16(rpnBytes, startIndex + 2);
            this.firstColumn = rpnBytes[startIndex + 4];
            this.firstOptions = rpnBytes[startIndex + 5];
            this.lastColumn = rpnBytes[startIndex + 6];
            this.lastOptions = rpnBytes[startIndex + 7];
        }

        private void SetArea(string cell)
        {
            Match match1 = AreaFormulaToken.IsCellRangeRegex.Match(cell);
            string text1 = match1.Groups["Row1"].Value;
            string text2 = match1.Groups["Column1"].Value;
            string text3 = match1.Groups["Row2"].Value;
            string text4 = match1.Groups["Column2"].Value;
            this.SetArea(text1, text3, text2, text4);
        }

        protected void SetArea(string row1, string row2, string column1, string column2)
        {
            this.SetFirstRow(row1);
            this.SetLastRow(row2);
            this.SetFirstColumn(column1);
            this.SetLastColumn(column2);
        }

        public static void SetAreaColumns(string value, out byte firstColumn, out byte lastColumn)
        {
            firstColumn = 0;
            lastColumn = 0;
            Match match1 = AreaFormulaToken.IsCellRangeRegex.Match(value);
            firstColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(match1.Groups["Column1"].Value);
            lastColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(match1.Groups["Column2"].Value);
        }

        public static void SetAreaRows(string value, out ushort firstRow, out ushort lastRow)
        {
            firstRow = 0;
            lastRow = 0;
            Match match1 = AreaFormulaToken.IsCellRangeRegex.Match(value);
            firstRow = (ushort) ExcelRowCollection.RowNameToIndex(match1.Groups["Row1"].Value);
            lastRow = (ushort) ExcelRowCollection.RowNameToIndex(match1.Groups["Row2"].Value);
        }

        private void SetFirstColumn(string column)
        {
            if (column[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsFirstColumnRelative = false;
                column = column.Substring(1);
            }
            else
            {
                this.IsFirstColumnRelative = true;
            }
            this.firstColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(column);
        }

        private void SetFirstRow(string row)
        {
            if (row[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsFirstRowRelative = false;
                row = row.Substring(1);
            }
            else
            {
                this.IsFirstRowRelative = true;
            }
            this.firstRow = (ushort) ExcelRowCollection.RowNameToIndex(row);
        }

        private void SetLastColumn(string column)
        {
            if (column[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsLastColumnAbsolute = false;
                column = column.Substring(1);
            }
            else
            {
                this.IsLastColumnAbsolute = true;
            }
            this.lastColumn = (byte) ExcelColumnCollection.ColumnNameToIndex(column);
        }

        private void SetLastRow(string row)
        {
            if (row[0] == RefFormulaToken.AbsoluteCellMark)
            {
                this.IsLastRowRelative = false;
                row = row.Substring(1);
            }
            else
            {
                this.IsLastRowRelative = true;
            }
            this.lastRow = (ushort) ExcelRowCollection.RowNameToIndex(row);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            string text1 = this.IsFirstRowRelative ? ExcelRowCollection.RowIndexToName(this.FirstRow) : (RefFormulaToken.AbsoluteCellMark + ExcelRowCollection.RowIndexToName(this.FirstRow));
            string text2 = this.IsLastRowRelative ? ExcelRowCollection.RowIndexToName(this.LastRow) : (RefFormulaToken.AbsoluteCellMark + ExcelRowCollection.RowIndexToName(this.LastRow));
            string text3 = this.IsFirstColumnRelative ? ExcelColumnCollection.ColumnIndexToName(this.FirstColumn) : (RefFormulaToken.AbsoluteCellMark + ExcelColumnCollection.ColumnIndexToName(this.FirstColumn));
            string text4 = this.IsLastColumnAbsolute ? ExcelColumnCollection.ColumnIndexToName(this.LastColumn) : (RefFormulaToken.AbsoluteCellMark + ExcelColumnCollection.ColumnIndexToName(this.LastColumn));
            string[] textArray1 = new string[] { text3, text1, ":", text4, text2 } ;
            return string.Concat(textArray1);
        }


        // Properties
        public byte FirstColumn
        {
            get
            {
                return this.firstColumn;
            }
        }

        ///<summary>
        ///Gets the first row.
        ///</summary>
        ///<value>The first row.</value>
        public ushort FirstRow
        {
            get
            {
                return this.firstRow;
            }
        }

        public bool IsFirstColumnRelative
        {
            get
            {
                return Utilities.IsBitSetted(this.firstOptions, 0x40);
            }
            set
            {
                this.firstOptions = Utilities.SetBit(this.firstOptions, 0x40, value);
            }
        }

        public bool IsFirstRowRelative
        {
            get
            {
                return Utilities.IsBitSetted(this.firstOptions, 0x80);
            }
            set
            {
                this.firstOptions = Utilities.SetBit(this.firstOptions, 0x80, value);
            }
        }

        public bool IsLastColumnAbsolute
        {
            get
            {
                return Utilities.IsBitSetted(this.lastOptions, 0x40);
            }
            set
            {
                this.lastOptions = Utilities.SetBit(this.lastOptions, 0x40, value);
            }
        }

        public bool IsLastRowRelative
        {
            get
            {
                return Utilities.IsBitSetted(this.lastOptions, 0x80);
            }
            set
            {
                this.lastOptions = Utilities.SetBit(this.lastOptions, 0x80, value);
            }
        }

        public byte LastColumn
        {
            get
            {
                return this.lastColumn;
            }
        }

        public ushort LastRow
        {
            get
            {
                return this.lastRow;
            }
        }


        // Fields
        protected byte firstColumn;
        protected byte firstOptions;
        ///<summary>
        ///first row.
        ///</summary>
        protected ushort firstRow;
        ///<summary>
        ///Regula expression used to determinate whether the input string is cell range( area ) or not
        ///</summary>
        public static readonly Regex IsCellRangeRegex;
        protected byte lastColumn;
        protected byte lastOptions;
        protected ushort lastRow;
        ///<summary>
        ///Regular expression default settings
        ///</summary>
        private static RegexOptions regexOptions;
    }
}

