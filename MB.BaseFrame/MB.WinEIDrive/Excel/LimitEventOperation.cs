namespace MB.WinEIDrive.Excel
{
    using System;

    ///<summary>
    ///Possible operations that can fire <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitNear">ExcelFile.LimitNear</see>
    ///and <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitReached">ExcelFile.LimitReached</see> events.
    ///</summary>
    ///<example> Following code demonstrates how to handle <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitNear">LimitNear</see>
    ///and <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitReached">LimitReached</see> events in <b>ExcelLite Free</b>.
    ///This sample disables warning worksheet in <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitNear">LimitNear</see> event
    ///handler and displays console messages in
    ///<see cref="MB.WinEIDrive.Excel.ExcelFile.LimitReached">LimitReached</see> event handler.
    ///<code lang="Visual Basic">
    ///Sub Main()
    ///Dim ef As ExcelFile = New ExcelFile
    ///
    ///AddHandler ef.LimitNear, AddressOf ef_LimitNear
    ///AddHandler ef.LimitReached, AddressOf ef_LimitReached
    ///
    ///Dim ws As ExcelWorksheet = ef.Worksheets.Add("Sheet1")
    ///
    ///Dim i As Integer
    ///For i = 0 To 172 - 1 Step i + 1
    ///ws.Cells(i, 0).Value = i
    ///Next
    ///
    ///ef.SaveXls("Test.xls")
    ///End Sub
    ///
    ///Private Sub ef_LimitNear(ByVal sender As Object, ByVal e As LimitEventArgs)
    ///e.WriteWarningWorksheet = False
    ///End Sub
    ///
    ///Private Sub ef_LimitReached(ByVal sender As Object, ByVal e As LimitEventArgs)
    ///Select Case e.Operation
    ///Case LimitEventOperation.XlsReading
    ///Console.WriteLine("Data truncated while reading XLS file: " + e.FileName)
    ///
    ///Case LimitEventOperation.CsvReading
    ///Console.WriteLine("Data truncated while reading CSV file: " + e.FileName)
    ///
    ///Case LimitEventOperation.XlsWriting
    ///Console.WriteLine("Data truncated while writing XLS file: " + e.FileName)
    ///e.WriteWarningWorksheet = False
    ///
    ///Case LimitEventOperation.CsvWriting
    ///Console.WriteLine("Data truncated while writing CSV file: " + e.FileName)
    ///
    ///End Select
    ///End Sub
    ///</code>
    ///<code lang="C#">
    ///static void Main(string[] args)
    ///{
    ///ExcelFile ef = new ExcelFile();
    ///
    ///ef.LimitNear += new LimitEventHandler(ef_LimitNear);
    ///ef.LimitReached += new LimitEventHandler(ef_LimitReached);
    ///
    ///ExcelWorksheet ws = ef.Worksheets.Add("Sheet1");
    ///
    ///for(int i=0; i!=172; i++)
    ///ws.Cells[i, 0].Value = i;
    ///
    ///ef.SaveXls("Test.xls");
    ///}
    ///
    ///private static void ef_LimitNear(object sender, LimitEventArgs e)
    ///{
    ///e.WriteWarningWorksheet = false;
    ///}
    ///
    ///private static void ef_LimitReached(object sender, LimitEventArgs e)
    ///{
    ///switch(e.Operation)
    ///{
    ///case LimitEventOperation.XlsReading:
    ///Console.WriteLine("Data truncated while reading XLS file: " + e.FileName);
    ///break;
    ///
    ///case LimitEventOperation.CsvReading:
    ///Console.WriteLine("Data truncated while reading CSV file: " + e.FileName);
    ///break;
    ///
    ///case LimitEventOperation.XlsWriting:
    ///Console.WriteLine("Data truncated while writing XLS file: " + e.FileName);
    ///e.WriteWarningWorksheet = false;
    ///break;
    ///
    ///case LimitEventOperation.CsvWriting:
    ///Console.WriteLine("Data truncated while writing CSV file: " + e.FileName);
    ///break;
    ///}
    ///}
    ///</code>
    ///</example>
    internal enum LimitEventOperation
    {
        XlsReading,
        CsvReading,
        XlsWriting,
        CsvWriting
    }
}

