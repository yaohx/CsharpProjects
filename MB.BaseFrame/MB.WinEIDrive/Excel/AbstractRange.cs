namespace MB.WinEIDrive.Excel
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    ///<summary>
    ///Base class for classes representing one or more excel cells.
    ///</summary>
   internal abstract class AbstractRange
    {
        // Methods
        ///<summary>
        ///Internal.
        ///</summary>
        ///<param name="parent"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected AbstractRange(ExcelWorksheet parent)
        {
            this.parent = parent;
        }

        internal void CheckMultiline(object val)
        {
            string text1 = val as string;
            if ((text1 != null) && (text1.IndexOf('\n') != -1))
            {
                this.Style.WrapText = true;
            }
        }

        ///<summary>
        ///Sets borders on one or more excel cells, taking cell position into account.
        ///</summary>
        ///<param name="multipleBorders">Borders to set.</param>
        ///<param name="lineColor">Line color.</param>
        ///<param name="lineStyle">Line style.</param>
        public abstract void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle);


        // Properties
        ///<summary>
        ///Gets or sets formula string.
        ///</summary>
        public abstract string Formula { get; set; }

        ///<summary>
        ///Returns <b>true</b> if all cells in <see cref="MB.WinEIDrive.Excel.AbstractRange">AbstractRange</see> have default
        ///cell style; otherwise, <b>false</b>.
        ///</summary>
        public abstract bool IsStyleDefault { get; }

        internal ExcelWorksheet Parent
        {
            get
            {
                return this.parent;
            }
        }

        ///<summary>
        ///Gets or sets cell style (<see cref="MB.WinEIDrive.Excel.CellStyle">CellStyle</see>) on one or more excel cells.
        ///</summary>
        public abstract CellStyle Style { get; set; }

        ///<summary>
        ///Gets or sets cell value on one or more excel cells.
        ///</summary>
        public abstract object Value { get; set; }


        // Fields
        private ExcelWorksheet parent;
    }
}

