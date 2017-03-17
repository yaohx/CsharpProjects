namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Drawing;
    using System.Reflection;

    ///<summary>
    ///Collection of cell borders (<see cref="MB.WinEIDrive.Excel.CellBorder">CellBorder</see>).
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.CellBorder" />
    internal sealed class CellBorders
    {
        // Methods
        internal CellBorders(CellStyle parent)
        {
            this.parent = parent;
        }

        internal void CopyTo(CellStyle destination)
        {
            CellStyleData data1 = destination.Element;
            CellStyleData data2 = this.parent.Element;
            for (int num1 = 0; num1 < 5; num1++)
            {
                data1.BorderColor[num1] = data2.BorderColor[num1];
                data1.BorderStyle[num1] = data2.BorderStyle[num1];
            }
            data1.BordersUsed = data2.BordersUsed;
        }

        ///<summary>
        ///Sets specific line color and line style on multiple borders.
        ///</summary>
        ///<param name="multipleBorders">Borders to set.</param>
        ///<param name="lineColor">Border line color.</param>
        ///<param name="lineStyle">Border line style.</param>
        public void SetBorders(MultipleBorders multipleBorders, Color lineColor, LineStyle lineStyle)
        {
            for (int num1 = 0; num1 < 6; num1++)
            {
                IndividualBorder border1 = (IndividualBorder) num1;
                if ((multipleBorders & CellBorder.MultipleFromIndividualBorder(border1)) != MultipleBorders.None)
                {
                    this[border1].SetBorder(lineColor, lineStyle);
                }
            }
        }


        // Properties
        ///<summary>
        ///Gets specific border.
        ///</summary>
        ///<param name="individualBorder">Border to get.</param>
        public CellBorder this[IndividualBorder individualBorder]
        {
            get
            {
                return new CellBorder(this.parent, individualBorder);
            }
        }


        // Fields
        private CellStyle parent;
    }
}

