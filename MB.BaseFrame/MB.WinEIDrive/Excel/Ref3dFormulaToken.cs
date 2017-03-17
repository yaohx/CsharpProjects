namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Text.RegularExpressions;

    ///<summary>
    ///Formula token for holding 3d reference on internal cell.
    ///</summary>
    internal class Ref3dFormulaToken : RefFormulaToken
    {
        // Methods
        static Ref3dFormulaToken()
        {
            Ref3dFormulaToken.regexOptions = RegexOptions.Compiled;
            Ref3dFormulaToken.IsCell3DRegex = new Regex(@"(?<Sheet>[\S ]+)[\!](?<Column>[\$]?[A-Z]+)(?<Row>[\$]?\d+)", Ref3dFormulaToken.regexOptions);
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.Ref3dFormulaToken" /> class.
        ///</summary>
        ///<param name="code">The code.</param>
        public Ref3dFormulaToken(FormulaTokenCode code) : base(code, 7)
        {
            this.refIndex = 0;
        }

        ///<summary>
        ///Convert formula token to array of byte representation.
        ///</summary>
        ///<returns>formula token' array of byte representation</returns>
        public override byte[] ConvertToBytes()
        {
            byte[] buffer1 = new byte[this.Size];
            buffer1[0] = base.Code;
            if (this.sheet != null)
            {
                this.SetSheet(this.sheet);
            }
            BitConverter.GetBytes(this.refIndex).CopyTo(buffer1, 1);
            BitConverter.GetBytes(base.row).CopyTo(buffer1, 3);
            buffer1[5] = base.Column;
            buffer1[6] = base.options;
            return buffer1;
        }

        ///<summary>
        ///Make custom delay initialize.
        ///</summary>
        ///<param name="data">The data for initialization which is unique for each formula token.</param>
        public override void DelayInitialize(object[] data)
        {
            this.workbook = data[1] as ExcelWorksheetCollection;
            this.Set3dCell(data[0] as string);
        }

        ///<summary>
        ///Initialize formula token by reading input data from array of bytes
        ///</summary>
        ///<param name="rpnBytes">input data, array of bytes</param>
        ///<param name="startIndex">start position for array of bytes to read from</param>
        public override void Read(byte[] rpnBytes, int startIndex)
        {
            this.refIndex = BitConverter.ToUInt16(rpnBytes, startIndex + 1);
            base.row = BitConverter.ToUInt16(rpnBytes, startIndex + 3);
            base.column = rpnBytes[startIndex++];
            base.options = rpnBytes[startIndex];
        }

        private void Set3dCell(string cell)
        {
            Match match1 = Ref3dFormulaToken.IsCell3DRegex.Match(cell);
            this.sheet = match1.Groups["Sheet"].Value;
            string text1 = match1.Groups["Row"].Value;
            string text2 = match1.Groups["Column"].Value;
            base.SetCell(text1, text2);
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
        ///Regular expression used to determinate whether the input string is 3d cell or not
        ///</summary>
        public static readonly Regex IsCell3DRegex;
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

