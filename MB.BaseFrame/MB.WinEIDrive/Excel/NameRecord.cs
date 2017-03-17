namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    ///<summary>
    ///Name record for holding information about name which can be used in named cell\range
    ///</summary>
    internal class NameRecord : XLSRecord
    {
        // Methods
        static NameRecord()
        {
            NameRecord.staticDescriptor = XLSDescriptors.GetByName("NAME");
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.NameRecord" /> class.
        ///</summary>
        ///<param name="worksheet">The worksheet.</param>
        public NameRecord(ExcelWorksheet worksheet) : base(NameRecord.staticDescriptor)
        {
            this.worksheet = worksheet;
            base.InitializeBody((byte[]) null);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.NameRecord" /> class.
        ///</summary>
        ///<param name="bodyLength">Length of the body.</param>
        ///<param name="br">The binary readed to read from.</param>
        ///<param name="previousRecord">The previous record.</param>
        public NameRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord) : base(NameRecord.staticDescriptor, bodyLength, br)
        {
        }

        ///<summary>
        ///Converts the name record range to RPN bytes.
        ///</summary>
        ///<param name="range">The range to be converted.</param>
        ///<param name="sheetName">Sheet' name.</param>
        ///<param name="worksheets">The worksheets collection.</param>
        public static byte[] ConvertNameRecordRangeToRpnBytes(CellRange range, string sheetName, ExcelWorksheetCollection worksheets)
        {
            FormulaToken token1 = null;
            string text1 = string.Empty;
            if ((range.Width == 1) && (range.Height == 1))
            {
                Match match1 = RefFormulaToken.IsCellRegex.Match(range.ToString());
                string text2 = NameRecord.ConvertToAbsolute(match1.Groups["Row"].Value);
                string text3 = NameRecord.ConvertToAbsolute(match1.Groups["Column"].Value);
                text1 = sheetName + "!" + text3 + text2;
                token1 = new Ref3dFormulaToken(FormulaTokenCode.Ref3d1);
            }
            else
            {
                Match match2 = AreaFormulaToken.IsCellRangeRegex.Match(range.ToString());
                string text4 = NameRecord.ConvertToAbsolute(match2.Groups["Row1"].Value);
                string text5 = NameRecord.ConvertToAbsolute(match2.Groups["Column1"].Value);
                string text6 = NameRecord.ConvertToAbsolute(match2.Groups["Row2"].Value);
                string text7 = NameRecord.ConvertToAbsolute(match2.Groups["Column2"].Value);
                string[] textArray1 = new string[] { sheetName, "!", text5, text4, ":", text7, text6 } ;
                text1 = string.Concat(textArray1);
                token1 = new Area3dFormulaToken(FormulaTokenCode.Area3d1);
            }
            object[] objArray1 = new object[] { text1, worksheets } ;
            token1.DelayInitialize(objArray1);
            return token1.ConvertToBytes();
        }

        private static string ConvertToAbsolute(string data)
        {
            if (data[0] != RefFormulaToken.AbsoluteCellMark)
            {
                data = data.Insert(0, RefFormulaToken.AbsoluteCellMark.ToString());
            }
            return data;
        }

        protected override byte GetStringLength(object[] loadedArgs)
        {
            return (byte) loadedArgs[0];
        }

        protected override int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            return (ushort) loadedArgs[1];
        }

        protected override void InitializeDelayed()
        {
            object[] objArray1 = new object[] { this.nameLength, (ushort) this.rpnBytes.Length, (ushort) (this.SheetIndex + 1), new ExcelStringWithoutLength(this.nameValue), this.rpnBytes } ;
            base.InitializeDelayed(objArray1);
        }


        // Properties
        protected override int BodySize
        {
            get
            {
                if (this.Body != null)
                {
                    return this.Body.Length;
                }
                return ((15 + (this.nameValue.Length * 2)) + this.rpnBytes.Length);
            }
        }

        ///<summary>
        ///Gets or sets the name value.
        ///</summary>
        ///<value>The name value.</value>
        public string NameValue
        {
            get
            {
                return this.nameValue;
            }
            set
            {
                this.nameValue = value;
                this.nameLength = (byte) value.Length;
            }
        }

        ///<summary>
        ///Gets or sets the range to be associated with the user-defined name.
        ///</summary>
        ///<value>The range to be associated with the user-defined name.</value>
        public CellRange Range
        {
            get
            {
                return this.range;
            }
            set
            {
                this.range = value;
            }
        }

        ///<summary>
        ///Gets or sets the RPN bytes of formula used for referencing 3d cell or area.
        ///</summary>
        ///<value>The RPN bytes of formula used for referencing 3d cell or area.</value>
        public object[] RpnBytes
        {
            get
            {
                return this.rpnBytes;
            }
            set
            {
                this.rpnBytes = value;
            }
        }

        ///<summary>
        ///Gets or sets the index for the sheet which contain named cell\range.
        ///</summary>
        ///<value>The index for the sheet which contain named cell\range.</value>
        public ushort SheetIndex
        {
            get
            {
                return this.sheetIndex;
            }
            set
            {
                this.sheetIndex = value;
            }
        }

        ///<summary>
        ///Gets or sets the workbook\worksheets collection.
        ///</summary>
        ///<value>The workbook\worksheets collection.</value>
        public ExcelWorksheetCollection Worksheets
        {
            get
            {
                return this.worksheets;
            }
            set
            {
                this.worksheets = value;
            }
        }


        // Fields
        private byte nameLength;
        private string nameValue;
        private CellRange range;
        private object[] rpnBytes;
        private ushort sheetIndex;
        private static XLSDescriptor staticDescriptor;
        private ExcelWorksheet worksheet;
        private ExcelWorksheetCollection worksheets;
    }
}

