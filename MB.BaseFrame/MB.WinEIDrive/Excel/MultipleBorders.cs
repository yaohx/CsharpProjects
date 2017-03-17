namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Flags for borders and border groups that can be set on the excel cell.
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.IndividualBorder" />
    [Flags]
    internal enum MultipleBorders
    {
        // Fields
        ///<summary>
        ///All borders are used.
        ///</summary>
        All = 0x3f,
        ///<summary>
        ///Bottom border.
        ///</summary>
        Bottom = 2,
        ///<summary>
        ///Diagonal borders.
        ///</summary>
        Diagonal = 0x30,
        ///<summary>
        ///Diagonal-down border.
        ///</summary>
        DiagonalDown = 0x20,
        ///<summary>
        ///Diagonal-up border.
        ///</summary>
        DiagonalUp = 0x10,
        ///<summary>
        ///Horizontal borders.
        ///</summary>
        Horizontal = 3,
        ///<summary>
        ///Left border.
        ///</summary>
        Left = 4,
        ///<summary>
        ///None of the borders are used.
        ///</summary>
        None = 0,
        ///<summary>
        ///Outside borders.
        ///</summary>
        Outside = 15,
        ///<summary>
        ///Right border.
        ///</summary>
        Right = 8,
        ///<summary>
        ///Top border.
        ///</summary>
        Top = 1,
        ///<summary>
        ///Vertical borders.
        ///</summary>
        Vertical = 12
    }
}

