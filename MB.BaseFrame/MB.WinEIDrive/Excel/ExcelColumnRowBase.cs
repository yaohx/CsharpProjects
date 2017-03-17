namespace MB.WinEIDrive.Excel
{
    using System;
    using System.ComponentModel;

    ///<summary>
    ///Base class for the excel column and row classes.
    ///</summary>
    internal abstract class ExcelColumnRowBase
    {
        // Methods
        ///<summary>
        ///Internal. Copy constructor.
        ///</summary>
        ///<param name="parent"></param>
        ///<param name="source"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ExcelColumnRowBase(ExcelRowColumnCollectionBase parent, ExcelColumnRowBase source)
        {
            this.parent = parent;
            this.index = source.index;
            this.collapsed = source.collapsed;
            this.outlineLevel = source.outlineLevel;
            this.Style = source.Style;
        }

        ///<summary>
        ///Internal.
        ///</summary>
        ///<param name="parent"></param>
        ///<param name="index"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected ExcelColumnRowBase(ExcelRowColumnCollectionBase parent, int index)
        {
            this.parent = parent;
            this.index = index;
        }


        // Properties
        ///<summary>
        ///Gets or sets whether object is collapsed in outlining.
        ///</summary>
        ///<example> Following code creates two horizontal groups and one vertical group. Horizontal groups have
        ///outline button above (default is below), while vertical group is collapsed.
        ///<code lang="Visual Basic">
        ///Sub GroupingSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Grouping and outline example:"
        ///
        ///<font color="Green">' Vertical grouping.</font>
        ///ws.Cells(2, 0).Value = "GroupA Start"
        ///ws.Rows(2).OutlineLevel = 1
        ///ws.Cells(3, 0).Value = "A"
        ///ws.Rows(3).OutlineLevel = 1
        ///ws.Cells(4, 1).Value = "GroupB Start"
        ///ws.Rows(4).OutlineLevel = 2
        ///ws.Cells(5, 1).Value = "B"
        ///ws.Rows(5).OutlineLevel = 2
        ///ws.Cells(6, 1).Value = "GroupB End"
        ///ws.Rows(6).OutlineLevel = 2
        ///ws.Cells(7, 0).Value = "GroupA End"
        ///ws.Rows(7).OutlineLevel = 1
        ///<font color="Green">' Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = False
        ///
        ///<font color="Green">' Horizontal grouping (collapsed).</font>
        ///ws.Cells("E2").Value = "Gr.C Start"
        ///ws.Columns("E").OutlineLevel = 1
        ///ws.Columns("E").Collapsed = True
        ///ws.Cells("F2").Value = "C"
        ///ws.Columns("F").OutlineLevel = 1
        ///ws.Columns("F").Collapsed = True
        ///ws.Cells("G2").Value = "Gr.C End"
        ///ws.Columns("G").OutlineLevel = 1
        ///ws.Columns("G").Collapsed = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void GroupingSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Grouping and outline example:";
        ///
        ///<font color="Green">// Vertical grouping.</font>
        ///ws.Cells[2,0].Value = "GroupA Start";
        ///ws.Rows[2].OutlineLevel = 1;
        ///ws.Cells[3,0].Value = "A";
        ///ws.Rows[3].OutlineLevel = 1;
        ///ws.Cells[4,1].Value = "GroupB Start";
        ///ws.Rows[4].OutlineLevel = 2;
        ///ws.Cells[5,1].Value = "B";
        ///ws.Rows[5].OutlineLevel = 2;
        ///ws.Cells[6,1].Value = "GroupB End";
        ///ws.Rows[6].OutlineLevel = 2;
        ///ws.Cells[7,0].Value = "GroupA End";
        ///ws.Rows[7].OutlineLevel = 1;
        ///<font color="Green">// Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = false;
        ///
        ///<font color="Green">// Horizontal grouping (collapsed).</font>
        ///ws.Cells["E2"].Value = "Gr.C Start";
        ///ws.Columns["E"].OutlineLevel = 1;
        ///ws.Columns["E"].Collapsed = true;
        ///ws.Cells["F2"].Value = "C";
        ///ws.Columns["F"].OutlineLevel = 1;
        ///ws.Columns["F"].Collapsed = true;
        ///ws.Cells["G2"].Value = "Gr.C End";
        ///ws.Columns["G"].OutlineLevel = 1;
        ///ws.Columns["G"].Collapsed = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumnRowBase.OutlineLevel" />
        public bool Collapsed
        {
            get
            {
                return this.collapsed;
            }
            set
            {
                this.collapsed = value;
            }
        }

        internal int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        ///<summary>
        ///Returns <b>true</b> if style is default; otherwise, <b>false</b>.
        ///</summary>
        public bool IsStyleDefault
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
        ///Gets or sets outline level.
        ///</summary>
        ///<remarks>
        ///<p>Exception is thrown if value is out of 0 to 7 range.</p>
        ///<p>Using this property you can create hierarchical groups. Range of consecutive objects (rows or columns)
        ///with the same value of outline level belongs to the same group. Default value is zero, which prevents grouping.
        ///<see cref="MB.WinEIDrive.Excel.ExcelColumnRowBase.Collapsed">Collapsed</see> property determines whether group
        ///is collapsed or expanded in outlining.</p>
        ///</remarks>
        ///<example> Following code creates two horizontal groups and one vertical group. Horizontal groups have
        ///outline button above (default is below), while vertical group is collapsed.
        ///<code lang="Visual Basic">
        ///Sub GroupingSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Grouping and outline example:"
        ///
        ///<font color="Green">' Vertical grouping.</font>
        ///ws.Cells(2, 0).Value = "GroupA Start"
        ///ws.Rows(2).OutlineLevel = 1
        ///ws.Cells(3, 0).Value = "A"
        ///ws.Rows(3).OutlineLevel = 1
        ///ws.Cells(4, 1).Value = "GroupB Start"
        ///ws.Rows(4).OutlineLevel = 2
        ///ws.Cells(5, 1).Value = "B"
        ///ws.Rows(5).OutlineLevel = 2
        ///ws.Cells(6, 1).Value = "GroupB End"
        ///ws.Rows(6).OutlineLevel = 2
        ///ws.Cells(7, 0).Value = "GroupA End"
        ///ws.Rows(7).OutlineLevel = 1
        ///<font color="Green">' Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = False
        ///
        ///<font color="Green">' Horizontal grouping (collapsed).</font>
        ///ws.Cells("E2").Value = "Gr.C Start"
        ///ws.Columns("E").OutlineLevel = 1
        ///ws.Columns("E").Collapsed = True
        ///ws.Cells("F2").Value = "C"
        ///ws.Columns("F").OutlineLevel = 1
        ///ws.Columns("F").Collapsed = True
        ///ws.Cells("G2").Value = "Gr.C End"
        ///ws.Columns("G").OutlineLevel = 1
        ///ws.Columns("G").Collapsed = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void GroupingSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Grouping and outline example:";
        ///
        ///<font color="Green">// Vertical grouping.</font>
        ///ws.Cells[2,0].Value = "GroupA Start";
        ///ws.Rows[2].OutlineLevel = 1;
        ///ws.Cells[3,0].Value = "A";
        ///ws.Rows[3].OutlineLevel = 1;
        ///ws.Cells[4,1].Value = "GroupB Start";
        ///ws.Rows[4].OutlineLevel = 2;
        ///ws.Cells[5,1].Value = "B";
        ///ws.Rows[5].OutlineLevel = 2;
        ///ws.Cells[6,1].Value = "GroupB End";
        ///ws.Rows[6].OutlineLevel = 2;
        ///ws.Cells[7,0].Value = "GroupA End";
        ///ws.Rows[7].OutlineLevel = 1;
        ///<font color="Green">// Put outline row buttons above groups.</font>
        ///ws.OutlineRowButtonsBelow = false;
        ///
        ///<font color="Green">// Horizontal grouping (collapsed).</font>
        ///ws.Cells["E2"].Value = "Gr.C Start";
        ///ws.Columns["E"].OutlineLevel = 1;
        ///ws.Columns["E"].Collapsed = true;
        ///ws.Cells["F2"].Value = "C";
        ///ws.Columns["F"].OutlineLevel = 1;
        ///ws.Columns["F"].Collapsed = true;
        ///ws.Cells["G2"].Value = "Gr.C End";
        ///ws.Columns["G"].OutlineLevel = 1;
        ///ws.Columns["G"].Collapsed = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelColumnRowBase.Collapsed" />
        ///<exception cref="System.ArgumentOutOfRangeException">Thrown if value is out of 0 to 7 range.</exception>
        public int OutlineLevel
        {
            get
            {
                return this.outlineLevel;
            }
            set
            {
                if ((value < 0) || (value > 7))
                {
                    throw new ArgumentOutOfRangeException("value", value, "OutlineLevel must be in range from 0 to 7.");
                }
                this.outlineLevel = value;
                if (this.outlineLevel > this.parent.MaxOutlineLevel)
                {
                    this.parent.MaxOutlineLevel = this.outlineLevel;
                }
            }
        }

        internal ExcelRowColumnCellCollectionBase Parent
        {
            get
            {
                return this.parent;
            }
        }

        ///<summary>
        ///Gets or sets cell style (<see cref="MB.WinEIDrive.Excel.CellStyle">CellStyle</see>) for contained cells.
        ///</summary>
        ///<remarks>
        ///Setting this property will not directly change <see cref="MB.WinEIDrive.Excel.ExcelCell.Style">ExcelCell.Style</see>.
        ///Instead, this style will be used in resolving process when writing Excel file. See
        ///<see cref="MB.WinEIDrive.Excel.ExcelFile.RowColumnResolutionMethod">ExcelFile.RowColumnResolutionMethod</see>
        ///for details.
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.RowColumnResolutionMethod">ExcelFile.RowColumnResolutionMethod</seealso>
        public CellStyle Style
        {
            get
            {
                if (this.style == null)
                {
                    this.style = new CellStyle(this.Parent.Parent.ParentExcelFile.CellStyleCache);
                }
                return this.style;
            }
            set
            {
                this.style = new CellStyle(value, this.parent.Parent.ParentExcelFile.CellStyleCache);
            }
        }


        // Fields
        private bool collapsed;
        private int index;
        private int outlineLevel;
        private ExcelRowColumnCollectionBase parent;
        private CellStyle style;
    }
}

