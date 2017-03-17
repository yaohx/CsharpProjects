namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Runtime.CompilerServices;

    ///<summary>
    ///Delegate for handling the <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitNear">ExcelFile.LimitNear</see>
    ///and <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitReached">ExcelFile.LimitReached</see> events.
    ///</summary>
    internal delegate void LimitEventHandler(object sender, LimitEventArgs e);

}

