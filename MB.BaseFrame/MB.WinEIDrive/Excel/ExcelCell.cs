namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Drawing;

    ///<summary>
    ///Excel cell provides access to a single worksheet cell or to a merged range if the cell is merged.
    ///</summary>
    ///<remarks>
    ///<p>Merged range is created by using <see cref="MB.WinEIDrive.Excel.CellRange.Merged">CellRange.Merged</see> property.
    ///See the property documentation for more information on merging.</p>
    ///</remarks>
    ///<seealso cref="MB.WinEIDrive.Excel.CellRange.Merged">CellRange.Merged</seealso>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell.MergedRange" />
    internal sealed class ExcelCell : AbstractRange
    {
        // Methods
        internal ExcelCell(ExcelWorksheet parent) : base(parent)
        {
        }

        internal ExcelCell(ExcelWorksheet parent, ExcelCell sourceCell) : base(parent)
        {
            this.cellValue = sourceCell.ValueInternal;
            this.Style = sourceCell.Style;
        }

        internal void AddToMergedRange(MergedCellRange mergedRange)
        {
            if ((mergedRange.Value == null) && (this.cellValue != null))
            {
                mergedRange.ValueInternal = this.cellValue;
                if ((this.style != null) && !this.style.IsDefault)
                {
                    mergedRange.Style = this.style;
                }
            }
            this.AddToMergedRangeInternal(mergedRange);
        }

        internal void AddToMergedRangeInternal(MergedCellRange mergedRange)
        {
            this.cellValue = mergedRange;
        }

        ///<summary>
        ///Converts Excel floating-point number to <see cref="System.DateTime">DateTime</see> structure.
        ///</summary>
        ///<remarks>
        ///<p>
        ///Excel file format doesn't have a separate data type for date and time.
        ///<see cref="System.DateTime">DateTime</see> value is
        ///stored as IEEE number encoded in a special way. When reading Excel file,
        ///<see cref="MB.WinEIDrive.Excel.CellStyle.NumberFormat">CellStyle.NumberFormat</see> is examined and if it matches
        ///some of date/time number formats cell value is interpreted as <see cref="System.DateTime">DateTime</see>.</p>
        ///<p>However, if some non-standard date/time number format is used, cell value will not be recognized
        ///as <see cref="System.DateTime">DateTime</see> but as ordinary number. In such cases (when you know that
        ///specific cell holds <see cref="System.DateTime">DateTime</see> value but you get a number when reading
        ///Excel file) use this method to convert IEEE number to <see cref="System.DateTime">DateTime</see>
        ///structure.</p>
        ///</remarks>
        ///<param name="num">Excel floating-point number.</param>
        ///<returns>Converted DateTime structure.</returns>
        public static DateTime ConvertExcelNumberToDateTime(double num)
        {
            DateTime time1 = new DateTime(0x76c, 1, 1);
            if (double.IsNaN(num) || double.IsInfinity(num))
            {
                return time1;
            }
            long num1 = (long) num;
            long num2 = (long) ((num - num1) * 86400);
            DateTime time2 = time1.AddDays((double) (num1 - 2));
            return time2.AddSeconds((double) num2);
        }

        internal void RemoveFromMergedRange()
        {
            MergedCellRange range1 = this.cellValue as MergedCellRange;
            if (range1 == null)
            {
                throw new Exception("Internal error: cell is not merged.");
            }
            this.cellValue = range1.ValueInternal;
            this.style = range1.Style;
        }

        ///<summary>
        ///Sets borders on this cell or on merged range if this cell is merged.
        ///</summary>
        ///<param name="multipleBorders">Borders to set.</param>
        ///<param name="lineColor">Line color.</param>
        ///<param name="lineStyle">Line style.</param>
        ///<seealso cref="MB.WinEIDrive.Excel.CellRange.Merged">CellRange.Merged</seealso>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell.MergedRange" />
        public override void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle)
        {
            this.Style.Borders.SetBorders(multipleBorders, lineColor, lineStyle);
        }


        // Properties
        ///<summary>
        ///Gets or sets cell formula string.
        ///</summary>
        ///<remarks>
        ///<p>ExcelLite can read and write formulas, but can not calculate formula results. When you open a XLS file in
        ///MS Excel, formula results will be calculated automaticaly.</p>
        ///<p>During setting formula string ExcelLite formula parser will use English culture to parse numbers.</p>
        ///<p>Currently supported formula features are:
        ///<list type="bullet">
        ///<item><description>Named cell</description></item>
        ///<item><description>Named range</description></item>
        ///<item><description>Absolute cell/range</description></item>
        ///<item><description>Relative cell/range</description></item>
        ///<item><description>Functions( partly, see the list of supported functions below )</description></item>
        ///<item><description>Missed argument</description></item>
        ///<item><description>Unary operator</description></item>
        ///<item><description>Binary operator</description></item>
        ///<item><description>Parentheses</description></item>
        ///<item><description>3d cell reference</description></item>
        ///<item><description>3d cell range reference</description></item>
        ///<item><description>Boolean</description></item>
        ///<item><description>Integer</description></item>
        ///<item><description>Float</description></item>
        ///<item><description>String</description></item>
        ///<item><description>Error</description></item>
        ///</list>
        ///</p>
        ///<p>
        ///Currently unsupported formula features are:
        ///<list type="bullet">
        ///<item><description>Const array</description></item>
        ///<item><description>Array formula</description></item>
        ///<item><description>R1C1 reference</description></item>
        ///</list>
        ///</p>
        ///<p>
        ///Currently supported functions are:
        ///<list type="bullet">
        ///<item><description>NOW</description></item>
        ///<item><description>SECOND</description></item>
        ///<item><description>MINUTE</description></item>
        ///<item><description>HOUR</description></item>
        ///<item><description>WEEKDAY</description></item>
        ///<item><description>YEAR</description></item>
        ///<item><description>MONTH</description></item>
        ///<item><description>DAY</description></item>
        ///<item><description>TIME</description></item>
        ///<item><description>DATE</description></item>
        ///<item><description>RAND</description></item>
        ///<item><description>TEXT</description></item>
        ///<item><description>VAR</description></item>
        ///<item><description>MOD</description></item>
        ///<item><description>NOT</description></item>
        ///<item><description>OR</description></item>
        ///<item><description>AND</description></item>
        ///<item><description>FALSE</description></item>
        ///<item><description>TRUE</description></item>
        ///<item><description>VALUE</description></item>
        ///<item><description>LEN</description></item>
        ///<item><description>MID</description></item>
        ///<item><description>ROUND</description></item>
        ///<item><description>SIGN</description></item>
        ///<item><description>INT</description></item>
        ///<item><description>ABS</description></item>
        ///<item><description>LN</description></item>
        ///<item><description>EXP</description></item>
        ///<item><description>SQRT</description></item>
        ///<item><description>PI</description></item>
        ///<item><description>COS</description></item>
        ///<item><description>SIN</description></item>
        ///<item><description>COLUMN</description></item>
        ///<item><description>ROW</description></item>
        ///<item><description>MAX</description></item>
        ///<item><description>MIN</description></item>
        ///<item><description>AVERAGE</description></item>
        ///<item><description>SUM</description></item>
        ///<item><description>IF</description></item>
        ///<item><description>COUNT</description></item>
        ///</list>
        ///</p>
        ///<p>
        ///For more information on formulas, consult Microsoft Excel documentation.
        ///</p>
        ///</remarks>
        ///<example>Following code demonstrates how to use formulas and named ranges. It shows next features:
        ///cell references (both absolute and relative), unary and binary operators, constand operands (integer and floating point),
        ///functions and named cell ranges.
        ///<code lang="Visual Basic">
        ///ws.Cells("A1").Value = 5
        ///ws.Cells("A2").Value = 6
        ///ws.Cells("A3").Value = 10
        ///
        ///ws.Cells("C1").Formula = "=A1+A2"
        ///ws.Cells("C2").Formula = "=$A$1-A3"
        ///ws.Cells("C3").Formula = "=COUNT(A1:A3)"
        ///ws.Cells("C4").Formula = "=AVERAGE($A$1:$A$3)"
        ///ws.Cells("C5").Formula = "=SUM(A1:A3,2,3)"
        ///ws.Cells("C7").Formula = "= 123 - (-(-(23.5)))"
        ///
        ///ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"))
        ///ws.Cells("C8").Formula = "=MAX(DataRange)"
        ///
        ///Dim cr As CellRange = ws.Cells.GetSubrange("B9","C10")
        ///cr.Merged = True
        ///cr.Formula = "=A1*25"
        ///</code>
        ///<code lang="C#">
        ///ws.Cells["A1"].Value = 5;
        ///ws.Cells["A2"].Value = 6;
        ///ws.Cells["A3"].Value = 10;
        ///
        ///ws.Cells["C1"].Formula = "=A1+A2";
        ///ws.Cells["C2"].Formula = "=$A$1-A3";
        ///ws.Cells["C3"].Formula = "=COUNT(A1:A3)";
        ///ws.Cells["C4"].Formula = "=AVERAGE($A$1:$A$3)";
        ///ws.Cells["C5"].Formula = "=SUM(A1:A3,2,3)";
        ///ws.Cells["C7"].Formula = "= 123 - (-(-(23.5)))";
        ///
        ///ws.NamedRanges.Add("DataRange", ws.Cells.GetSubrange("A1", "A3"));
        ///ws.Cells["C8"].Formula = "=MAX(DataRange)";
        ///
        ///CellRange cr = ws.Cells.GetSubrange("B9", "C10");
        ///cr.Merged = true;
        ///cr.Formula = "=A1*25";
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.NamedRangeCollection.Add(System.String,MB.WinEIDrive.Excel.CellRange)">NamedRangeCollection.Add</seealso>
        public override string Formula
        {
            get
            {
                CellFormula formula1 = this.ValueInternal as CellFormula;
                if (formula1 != null)
                {
                    return formula1.Formula;
                }
                return null;
            }
            set
            {
                this.ValueInternal = new CellFormula(value, base.Parent);
            }
        }

        internal CellFormula FormulaInternal
        {
            get
            {
                object obj1 = this.ValueInternal;
                if (obj1 is CellFormula)
                {
                    return (CellFormula) obj1;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.ValueInternal = value;
                }
                else
                {
                    this.ValueInternal = this.Value;
                }
            }
        }

        ///<summary>
        ///Returns <b>true</b> if style is default; otherwise, <b>false</b>.
        ///</summary>
        public override bool IsStyleDefault
        {
            get
            {
                if (this.style != null)
                {
                    return this.style.IsDefault;
                }
                return true;
            }
        }

        ///<summary>
        ///Returns associated merged range if the cell is merged; otherwise, <b>null</b>.
        ///</summary>
        ///<seealso cref="MB.WinEIDrive.Excel.CellRange.Merged">CellRange.Merged</seealso>
        public CellRange MergedRange
        {
            get
            {
                if (this.cellValue is MergedCellRange)
                {
                    return (MergedCellRange) this.cellValue;
                }
                return null;
            }
        }

        ///<summary>
        ///Gets or sets cell style (<see cref="MB.WinEIDrive.Excel.CellStyle">CellStyle</see>) of this cell or
        ///of merged range if this cell is merged.
        ///</summary>
        ///<remarks>
        ///Unset style properties will be inherited from corresponding row or column. See
        ///<see cref="MB.WinEIDrive.Excel.ExcelFile.RowColumnResolutionMethod">ExcelFile.RowColumnResolutionMethod</see>
        ///for more details.
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.CellRange.Merged">CellRange.Merged</seealso>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell.MergedRange" />
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.RowColumnResolutionMethod">ExcelFile.RowColumnResolutionMethod</seealso>
        public override CellStyle Style
        {
            get
            {
                if (this.cellValue is MergedCellRange)
                {
                    return ((MergedCellRange) this.cellValue).Style;
                }
                if (this.style == null)
                {
                    this.style = new CellStyle(base.Parent.ParentExcelFile.CellStyleCache);
                }
                return this.style;
            }
            set
            {
                if (this.cellValue is MergedCellRange)
                {
                    ((MergedCellRange) this.cellValue).Style = value;
                }
                else
                {
                    this.style = new CellStyle(value, base.Parent.ParentExcelFile.CellStyleCache);
                }
            }
        }

        ///<summary>
        ///Gets or sets value of this cell or of merged range if this cell is merged.
        ///</summary>
        ///<remarks>
        ///<p>Exception is thrown if value for the set is not of supported type (See
        ///<see cref="MB.WinEIDrive.Excel.ExcelFile.SupportsType(System.Type)">ExcelFile.SupportsType</see> for details).</p>
        ///<p>Note that the fact some type is supported doesn't mean it is written to Excel file in the native format. As
        ///Microsoft Excel has just few basic types, the object of supported type will be converted to a similar excel type.
        ///If similar excel type doesn't exist, value is written as a string value.</p>
        ///<p>If the value of this property is of <see cref="System.DateTime">DateTime</see> type and
        ///<see cref="MB.WinEIDrive.Excel.ExcelCell.Style">Style</see> number format is not set, ISO date/time
        ///format will be used as <see cref="MB.WinEIDrive.Excel.CellStyle.NumberFormat">CellStyle.NumberFormat</see>
        ///value.</p>
        ///</remarks>
        ///<exception cref="System.NotSupportedException">Thrown for unsupported types.</exception>
        ///<seealso cref="MB.WinEIDrive.Excel.CellRange.Merged">CellRange.Merged</seealso>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelCell.MergedRange" />
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.SupportsType(System.Type)">ExcelFile.SupportsType</seealso>
        ///<seealso cref="MB.WinEIDrive.Excel.CellStyle.NumberFormat">CellStyle.NumberFormat</seealso>
        public override object Value
        {
            get
            {
                object obj1 = this.ValueInternal;
                if (obj1 is CellFormula)
                {
                    obj1 = ((CellFormula) obj1).Value;
                }
                return obj1;
            }
            set
            {
                if (value != null)
                {
                    ExcelFile.ThrowExceptionForUnsupportedType(value.GetType());
                }
                object obj1 = this.ValueInternal;
                if (obj1 is CellFormula)
                {
                    ((CellFormula) obj1).Value = obj1;
                }
                else
                {
                    this.ValueInternal = value;
                    base.CheckMultiline(value);
                }
            }
        }

        internal object ValueInternal
        {
            get
            {
                if (this.cellValue is MergedCellRange)
                {
                    return ((MergedCellRange) this.cellValue).ValueInternal;
                }
                return this.cellValue;
            }
            set
            {
                if (this.cellValue is MergedCellRange)
                {
                    ((MergedCellRange) this.cellValue).ValueInternal = value;
                }
                else
                {
                    this.cellValue = value;
                }
            }
        }


        // Fields
        private object cellValue;
        private CellStyle style;
    }
}

