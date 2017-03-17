namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Text.RegularExpressions;

    ///<summary>
    ///Formula token for holding 3d reference on internal cell range.
    ///</summary>
    internal class Area3dFormulaToken : AreaFormulaToken
    {
        // Methods
        static Area3dFormulaToken()
        {
            Area3dFormulaToken.regexOptions = RegexOptions.Compiled;
            Area3dFormulaToken.IsCellRange3DRegex = new Regex(@"(?<Sheet>[\S ]+)[\!](?<Column1>[\$]?[A-Z]+)(?<Row1>[\$]?\d+):(?<Column2>[\$]?[A-Z]+)(?<Row2>[\$]?\d+)", Area3dFormulaToken.regexOptions);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.Area3dFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The FormulaTokenCode code.</param>
        public Area3dFormulaToken(FormulaTokenCode code) : base(code, 11)
        {
            this.refIndex = 0;
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = base.ConvertToBytes();
            byte[] buffer2 = new byte[this.Size];
            buffer2[0] = buffer1[0];
            Array.Copy(buffer1, 1, buffer2, 3, 8);
            if (this.sheet != null)
            {
                this.SetSheet(this.sheet);
            }
            BitConverter.GetBytes(this.refIndex).CopyTo(buffer2, 1);
            return buffer2;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.workbook = data[1] as ExcelWorksheetCollection;
            this.SetAred3dCell(data[0] as string);
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.refIndex = BitConverter.ToUInt16(rpnBytes, startIndex);
            base.firstRow = BitConverter.ToUInt16(rpnBytes, startIndex + 2);
            base.lastRow = BitConverter.ToUInt16(rpnBytes, startIndex + 4);
            base.firstColumn = rpnBytes[startIndex + 6];
            base.firstOptions = rpnBytes[startIndex + 7];
            base.lastColumn = rpnBytes[startIndex + 8];
            base.lastOptions = rpnBytes[startIndex + 9];
        }

        private void SetAred3dCell(string cell)
        {
            Match match1 = Area3dFormulaToken.IsCellRange3DRegex.Match(cell);
            this.sheet = match1.Groups["Sheet"].Value;
            string text1 = match1.Groups["Column1"].Value;
            string text2 = match1.Groups["Row1"].Value;
            string text3 = match1.Groups["Column2"].Value;
            string text4 = match1.Groups["Row2"].Value;
            base.SetArea(text2, text4, text1, text3);
            this.SetSheet(this.sheet);
        }

        private void SetSheet(string sheet)
        {
            this.refIndex = this.workbook.AddSheetReference(sheet);
        }

        ///<summary>
        ///Convert formula token to string representation.
        ///</summary>
        ///<returns>formula token string representation</returns>
        public override string ToString()
        {
            return (this.sheet + "!" + base.ToString());
        }


        // Fields
        ///<summary>
        ///Regular expression used to determinate whether the input string is 3d cell range( 1t case ) or not
        ///</summary>
        public static readonly Regex IsCellRange3DRegex;
        ///<summary>
        ///REF entry' index on EXTERNSHEET record( see the Link Table ).
        ///</summary>
        private ushort refIndex;
        ///<summary>
        ///Regular expression default options
        ///</summary>
        private static RegexOptions regexOptions;
        private string sheet;
        private ExcelWorksheetCollection workbook;
    }
}

