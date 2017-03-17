namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    ///<summary>
    ///Excel file contains one or more worksheets (<see cref="MB.WinEIDrive.Excel.ExcelWorksheet">ExcelWorksheet</see>)
    ///and workbook related properties and methods.
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelWorksheet" />
    [LicenseProvider(typeof(GemBoxLicenseProvider))]
    internal sealed class ExcelFile
    {
        // Events
        ///<summary>
        ///Occurs when the size of XLS / CSV file in reading / writing is near <b>ExcelLite Free</b> limit.
        ///</summary>
        ///<remarks>
        ///<p>This event is fired when 80% of maximum row count per worksheet is reached (150 * 0.8 = 120 rows) or
        ///when 5 worksheets are used.</p>
        ///<p>This event is not fired if <see cref="MB.WinEIDrive.Excel.ExcelFile.LimitReached">LimitReached</see>
        ///event is fired.</p>
        ///<p>You can use this event to detect when your application is close to <b>ExcelLite Free</b> limit.
        ///For example, you can write number of used rows to a log file or send a notification e-mail.</p>
        ///</remarks>
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
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.LimitReached" />
        public event LimitEventHandler LimitNear;
        ///<summary>
        ///Occurs when the size of XLS / CSV file in reading / writing is above <b>ExcelLite Free</b> limit.
        ///</summary>
        ///<remarks>
        ///<p>This event is fired when maximum row count per worksheet is reached (150 rows) or
        ///when more than 5 worksheets are used.</p>
        ///<p>You can use this event to notify a user of your application that data is
        ///only partially read / written.</p>
        ///</remarks>
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
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.LimitNear" />
        public event LimitEventHandler LimitReached;

        // Methods
        public ExcelFile()
        {
            this.rowColumnResolutionMethod = MB.WinEIDrive.Excel.RowColumnResolutionMethod.RowOverColumn;
			//edit by nick 2006-07-04 
            this.groupMethodsAffectedCellsLimit = 0x2710;
            this.HashFactorA = 0x65;
            this.HashFactorB = 0x33;
            this.IDText = "MB.WinEIDrive.Excel Free 2.3 for .NET 1.1, Version=23.1.5000.1000";
            this.Initialize();
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This constructor is obsolete. Use default constructor and LoadXls() method instead.")]
        public ExcelFile(string fileName)
        {
            this.rowColumnResolutionMethod = MB.WinEIDrive.Excel.RowColumnResolutionMethod.RowOverColumn;
			//edit by nick 2006-07-04 
            this.groupMethodsAffectedCellsLimit =  0x2710;
            this.HashFactorA = 0x65;
            this.HashFactorB = 0x33;
            this.IDText = "MB.WinEIDrive.Excel Free 2.3 for .NET 1.1, Version=23.1.5000.1000";
            this.Initialize();
            this.LoadXls(fileName, XlsOptions.PreserveAll);
        }

        [Obsolete("This constructor is obsolete. Use default constructor and LoadCsv() method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public ExcelFile(string fileName, CsvType csvType)
        {
            this.rowColumnResolutionMethod = MB.WinEIDrive.Excel.RowColumnResolutionMethod.RowOverColumn;
            this.groupMethodsAffectedCellsLimit = 0x2710;
            this.HashFactorA = 0x65;
            this.HashFactorB = 0x33;
            this.IDText = "MB.WinEIDrive.Excel Free 2.3 for .NET 1.1, Version=23.1.5000.1000";
            this.Initialize();
            this.LoadCsv(fileName, csvType);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This constructor is obsolete. Use default constructor and LoadXls() method instead.")]
        public ExcelFile(string fileName, XlsOptions xlsOptions)
        {
            this.rowColumnResolutionMethod = MB.WinEIDrive.Excel.RowColumnResolutionMethod.RowOverColumn;
            this.groupMethodsAffectedCellsLimit = 0x2710;
            this.HashFactorA = 0x65;
            this.HashFactorB = 0x33;
            this.IDText = "MB.WinEIDrive.Excel Free 2.3 for .NET 1.1, Version=23.1.5000.1000";
            this.Initialize();
            this.LoadXls(fileName, xlsOptions);
        }

        internal void CopyDrawings(ExcelFile source)
        {
            PreservedRecords records1 = source.PreservedGlobalRecords;
            if (records1 != null)
            {
                if (this.PreservedGlobalRecords == null)
                {
                    this.PreservedGlobalRecords = new PreservedRecords();
                }
                this.PreservedGlobalRecords.CopyRecords(records1, "MSODRAWINGGROUP");
            }
        }

        ///<summary>
        ///Internal.
        ///</summary>
        ///<param name="sourceFileName">Source file name.</param>
        ///<param name="destinationFileName">Destination file name.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void DumpToLowLevelXml(string sourceFileName, string destinationFileName)
        {
            byte[] buffer1 = null;
            AbsXLSRecords records1 = ExcelFile.ReadHelper(sourceFileName, false, ref buffer1, ref buffer1);
            ExcelFile.SaveLowLevelXML(records1, destinationFileName, sourceFileName);
        }

        internal void ExceptionIfOverAffectedCellsLimit(int cellCount)
        {
            int num1 = this.groupMethodsAffectedCellsLimit;
            if (cellCount > num1)
            {
                object[] objArray1 = new object[] { "You are trying to modify ", cellCount, " cells while ExcelFile.GroupMethodsAffectedCellsLimit is set to ", num1, ". If you want to perform this operation, you need to change ExcelFile.GroupMethodsAffectedCellsLimit accordingly." } ;
                throw new InvalidOperationException(string.Concat(objArray1));
            }
        }

        private void Initialize()
        {
            this.license = LicenseManager.Validate(typeof(ExcelFile), this);
            if (this.license.LicenseKey != "Valid")
            {
                throw new LicenseException(typeof(ExcelFile), this, "Invalid license.");
            }
            this.worksheets = new ExcelWorksheetCollection(this);
            this.cellStyleCache = new CellStyleCachedCollection(ExcelFile.QueueSizeFromAffectedCellsLimit(this.groupMethodsAffectedCellsLimit));
        }

        ///<overloads>Loads the existing CSV file</overloads>
        ///<summary>
        ///Loads the existing CSV file with specified format.
        ///</summary>
        ///<param name="fileName">Existing CSV file name (opened for reading).</param>
        ///<param name="csvType">CSV type.</param>
        public void LoadCsv(string fileName, CsvType csvType)
        {
            switch (csvType)
            {
                case CsvType.CommaDelimited:
                {
                    this.LoadCsv(fileName, ',');
                    return;
                }
                case CsvType.SemicolonDelimited:
                {
                    this.LoadCsv(fileName, ';');
                    return;
                }
                case CsvType.TabDelimited:
                {
                    this.LoadCsv(fileName, '\t');
                    return;
                }
            }
            throw new Exception("Internal: Unknown CsvType.");
        }

        ///<summary>
        ///Loads the existing CSV file, using specified character as a delimiter.
        ///</summary>
        ///<param name="fileName">File name.</param>
        ///<param name="separator">Separator used for delimiting data values.</param>
        public void LoadCsv(string fileName, char separator)
        {
            this.Reset();
            using (StreamReader reader1 = new StreamReader(fileName))
            {
                string[] textArray1;
                string text1 = Path.GetFileNameWithoutExtension(fileName);
                ExcelWorksheet worksheet1 = this.worksheets.Add(text1);
                int num1 = 0;
                while (ExcelFile.ReadSplitCsvLine(reader1, separator, out textArray1))
                {
                    if (num1 < (this.HashFactorA - this.HashFactorB))
                    {
                        for (int num2 = 0; num2 < textArray1.Length; num2++)
                        {
                            if (textArray1[num2].Length > 0)
                            {
                                worksheet1.Cells[num1, num2].Value = ExcelFile.ParseCSValue(textArray1[num2]);
                            }
                        }
                    }
                    num1++;
                }
                if (num1 > (this.HashFactorA - this.HashFactorB))
                {
                    this.OnLimitReached(fileName, LimitEventOperation.CsvReading, num1, 1, false);
                }
                else if (num1 > (((this.HashFactorA - this.HashFactorB) * 4) / 5))
                {
                    this.OnLimitNear(fileName, LimitEventOperation.CsvReading, num1, 1, false);
                }
            }
        }

        ///<overloads>Loads the existing XLS file</overloads>
        ///<summary>
        ///Loads the existing XLS file (preserving MS Excel records).
        ///</summary>
        ///<param name="fileName">Existing XLS file name (opened for reading).</param>
        public void LoadXls(string fileName)
        {
            this.LoadXls(fileName, XlsOptions.PreserveAll);
        }

        ///<summary>
        ///Loads the existing XLS file (optionally preserving MS Excel records).
        ///</summary>
        ///<remarks>
        ///<p>If the only purpose of loading the file is to read data values and formatting using
        ///ExcelLite API, you should use <i>xlsOptions</i> set to <see cref="MB.WinEIDrive.Excel.XlsOptions.None">XlsOptions.None</see>
        ///as this will speed up the loading process.</p>
        ///<p>If you load the existing file to use it as template for a new file, you can choose
        ///whether you want to preserve specific MS Excel records not recognized by ExcelLite API.</p>
        ///</remarks>
        ///<param name="fileName">Existing XLS file name (opened for reading).</param>
        ///<param name="xlsOptions">XLS options.</param>
        public void LoadXls(string fileName, XlsOptions xlsOptions)
        {
            this.Reset();
            AbsXLSRecords records1 = ExcelFile.ReadHelper(fileName, (xlsOptions & XlsOptions.PreserveSummaries) != XlsOptions.None, ref this.summaryStream, ref this.documentSummaryStream);
            XLSFileReader reader1 = new XLSFileReader(this, xlsOptions);
            reader1.ImportRecords(records1, fileName);
        }

        internal LimitEventArgs OnLimitNear(string fileName, LimitEventOperation operation, int maxRowCount, int worksheetCount, bool writeWarningWorksheet)
        {
            LimitEventArgs args1 = new LimitEventArgs(fileName, operation, maxRowCount, worksheetCount, writeWarningWorksheet);
            if (this.LimitNear != null)
            {
                this.LimitNear(this, args1);
            }
            return args1;
        }

        internal LimitEventArgs OnLimitReached(string fileName, LimitEventOperation operation, int maxRowCount, int worksheetCount, bool writeWarningWorksheet)
        {
            LimitEventArgs args1 = new LimitEventArgs(fileName, operation, maxRowCount, worksheetCount, writeWarningWorksheet);
            if (this.LimitReached != null)
            {
                this.LimitReached(this, args1);
            }
            return args1;
        }

        private static object ParseCSValue(string item)
        {
            double num1;
            NumberStyles styles1 = NumberStyles.Float | NumberStyles.AllowThousands;
            if (double.TryParse(item, styles1, CultureInfo.CurrentCulture, out num1))
            {
                return num1;
            }
            return item;
        }

        private static int QueueSizeFromAffectedCellsLimit(int affectedCellsLimit)
        {
            return Math.Min((int) (2 * affectedCellsLimit), 0x7a120);
        }

        private static AbsXLSRecords ReadHelper(string fileName, bool readSummaryStreams, ref byte[] ss, ref byte[] dss)
        {
            StructuredStorageFileBase base1 = StructuredStorageFileBase.Open(fileName);
            MemoryStream stream1 = new MemoryStream(base1.ReadStream("Workbook"));
            if (readSummaryStreams)
            {
                try
                {
                    ss = base1.ReadStream("" + '\x0005' + "SummaryInformation");
                }
                catch
                {
                }
                try
                {
                    dss = base1.ReadStream("" + '\x0005' + "DocumentSummaryInformation");
                }
                catch
                {
                }
            }
            base1.Close();
            BinaryReader reader1 = new BinaryReader(stream1, new UnicodeEncoding());
            AbsXLSRecords records1 = new AbsXLSRecords();
            records1.Read(reader1);
            reader1.Close();
            stream1.Close();
            return records1;
        }

        private static bool ReadSplitCsvLine(StreamReader sr, char separator, out string[] values)
        {
            ArrayList list1 = new ArrayList();
            StringBuilder builder1 = new StringBuilder();
            bool flag1 = true;
            char ch1 = separator;
            bool flag2 = true;
            string text1 = null;
            do
            {
                string text2 = sr.ReadLine();
                if (text2 == null)
                {
                    flag2 = false;
                    break;
                }
                text2 = text1 + text2;
                for (int num1 = 0; num1 < text2.Length; num1++)
                {
                    char ch2 = text2[num1];
                    if (flag1)
                    {
                        if (ch2 == separator)
                        {
                            list1.Add(builder1.ToString());
                            builder1 = new StringBuilder();
                            ch1 = ch2;
                        }
                        else if ((ch2 == '"') && (ch1 == separator))
                        {
                            flag1 = false;
                            ch1 = ch2;
                        }
                        else
                        {
                            builder1.Append(ch2);
                            if (!char.IsWhiteSpace(ch2))
                            {
                                ch1 = ch2;
                            }
                        }
                    }
                    else if (ch2 == '"')
                    {
                        if (((num1 + 1) < text2.Length) && (text2[num1 + 1] == '"'))
                        {
                            builder1.Append('"');
                            num1++;
                        }
                        else
                        {
                            flag1 = true;
                        }
                    }
                    else
                    {
                        builder1.Append(ch2);
                    }
                }
                text1 = "\n";
            }
            while (!flag1);
            list1.Add(builder1.ToString());
            values = (string[]) list1.ToArray(typeof(string));
            return flag2;
        }

        private void Reset()
        {
            this.worksheets = new ExcelWorksheetCollection(this);
            this.cellStyleCache = new CellStyleCachedCollection(ExcelFile.QueueSizeFromAffectedCellsLimit(this.groupMethodsAffectedCellsLimit));
            this.protectedMbr = false;
            this.summaryStream = null;
            this.documentSummaryStream = null;
            this.PreservedGlobalRecords = null;
        }

        ///<overloads>Saves all data to a new file in CSV format.</overloads>
        ///<summary>
        ///Saves all data to a new file in a specified CSV format.
        ///</summary>
        ///<param name="fileName">File name.</param>
        ///<param name="csvType">CSV type.</param>
        public void SaveCsv(string fileName, CsvType csvType)
        {
            switch (csvType)
            {
                case CsvType.CommaDelimited:
                {
                    this.SaveCsv(fileName, ',');
                    return;
                }
                case CsvType.SemicolonDelimited:
                {
                    this.SaveCsv(fileName, ';');
                    return;
                }
                case CsvType.TabDelimited:
                {
                    this.SaveCsv(fileName, '\t');
                    return;
                }
            }
            throw new Exception("Internal: Unknown CsvType.");
        }

        ///<summary>
        ///Saves all data to a new CSV file, using specified character as a delimiter.
        ///</summary>
        ///<param name="fileName">File name.</param>
        ///<param name="separator">Separator used for delimiting data values.</param>
        public void SaveCsv(string fileName, char separator)
        {
            char[] chArray2 = new char[] { separator, '"', '\n' } ;
            char[] chArray1 = chArray2;
            using (StreamWriter writer1 = new StreamWriter(fileName))
            {
                ExcelWorksheet worksheet1 = this.Worksheets.ActiveWorksheet;
                int num1 = -1;
                int num2 = -1;
                int num3 = 0;
                for (int num5 = 0; num5 < 2; num5++)
                {
                    if (num5 == 0)
                    {
                        num3 = worksheet1.Rows.Count;
                    }
                    else
                    {
                        num3 = num1 + 1;
                    }
                    for (int num6 = 0; num6 < num3; num6++)
                    {
                        int num4;
                        if (num5 == 0)
                        {
                            num4 = worksheet1.Rows[num6].AllocatedCells.Count;
                        }
                        else
                        {
                            num4 = num2 + 1;
                            if (num6 >= (this.HashFactorA - this.HashFactorB))
                            {
                                goto Label_018A;
                            }
                        }
                        for (int num7 = 0; num7 < num4; num7++)
                        {
                            ExcelCell cell1 = worksheet1.Cells[num6, num7];
                            object obj1 = cell1.Value;
                            if ((obj1 != null) && !(obj1 is DBNull))
                            {
                                CellRange range1 = cell1.MergedRange;
                                if ((range1 == null) || ((range1.FirstRowIndex == num6) && (range1.FirstColumnIndex == num7)))
                                {
                                    if (num5 == 0)
                                    {
                                        if (num7 > num2)
                                        {
                                            num2 = num7;
                                        }
                                    }
                                    else
                                    {
                                        string text1 = obj1.ToString();
                                        if (text1.IndexOfAny(chArray1) != -1)
                                        {
                                            StringBuilder builder1 = new StringBuilder(text1);
                                            builder1.Replace("\"", "\"\"");
                                            text1 = '"' + builder1.ToString() + '"';
                                        }
                                        writer1.Write(text1);
                                    }
                                }
                            }
                            if ((num5 == 1) && ((num7 + 1) < num4))
                            {
                                writer1.Write(separator);
                            }
                        }
                        if (num5 == 0)
                        {
                            if (num6 > num1)
                            {
                                num1 = num6;
                            }
                        }
                        else
                        {
                            writer1.WriteLine();
                        }
                    Label_018A:;
                    }
                }
                if (num3 > (this.HashFactorA - this.HashFactorB))
                {
                    this.OnLimitReached(fileName, LimitEventOperation.CsvWriting, num3, 1, false);
                }
                else if (num3 > (((this.HashFactorA - this.HashFactorB) * 4) / 5))
                {
                    this.OnLimitNear(fileName, LimitEventOperation.CsvWriting, num3, 1, false);
                }
            }
        }

        private static void SaveLowLevelXML(AbsXLSRecords records, string fileName, string sourceFileName)
        {
            StreamWriter writer1 = new StreamWriter(fileName);
            if (sourceFileName != null)
            {
                writer1.WriteLine("<!-- FileName: {0} -->", sourceFileName);
            }
            writer1.WriteLine("<ExcelFile>");
            string text1 = null;
            int num1 = 0;
            foreach (AbsXLSRec rec1 in records)
            {
                if (rec1.Name == text1)
                {
                    num1++;
                }
                else
                {
                    num1 = 0;
                    text1 = rec1.Name;
                }
                writer1.WriteLine(rec1.GetXMLRecord(num1));
            }
            writer1.Write("</ExcelFile>");
            writer1.Close();
        }

        ///<summary>
        ///Saves all data to a new file in XLS format.
        ///</summary>
        ///<param name="fileName">File name.</param>
        public void SaveXls(string fileName)
        {
            XLSFileWriter writer1 = new XLSFileWriter(this, fileName);
            ExcelFile.SaveXLSInternal(writer1.GetRecords(), fileName, this.summaryStream, this.documentSummaryStream);
        }

        private static void SaveXLSInternal(AbsXLSRecords records, string fileName, byte[] ss, byte[] dss)
        {
            StructuredStorageFileBase base1 = StructuredStorageFileBase.Create(fileName);
            base1.WriteStream("Workbook", XLSFileWriter.GetStream(records));
            if (ss != null)
            {
                base1.WriteStream("" + '\x0005' + "SummaryInformation", ss);
            }
            if (dss != null)
            {
                base1.WriteStream("" + '\x0005' + "DocumentSummaryInformation", dss);
            }
            base1.Close();
        }

        ///<summary>
        ///Gets a value indicating whether the objects of specified type can be assigned
        ///to <see cref="MB.WinEIDrive.Excel.ExcelCell.Value">ExcelCell.Value</see> property.
        ///</summary>
        ///<param name="type">Queried type.</param>
        ///<remarks>
        ///Currently supported types are:
        ///<list type="bullet">
        ///<item><description>System.DBNull</description></item>
        ///<item><description>System.Byte</description></item>
        ///<item><description>System.SByte</description></item>
        ///<item><description>System.Int16</description></item>
        ///<item><description>System.UInt16</description></item>
        ///<item><description>System.Int64</description></item>
        ///<item><description>System.UInt64</description></item>
        ///<item><description>System.UInt32</description></item>
        ///<item><description>System.Int32</description></item>
        ///<item><description>System.Single</description></item>
        ///<item><description>System.Double</description></item>
        ///<item><description>System.Boolean</description></item>
        ///<item><description>System.Char</description></item>
        ///<item><description>System.Text.StringBuilder</description></item>
        ///<item><description>System.Decimal</description></item>
        ///<item><description>System.DateTime</description></item>
        ///<item><description>System.String</description></item>
        ///</list>
        ///</remarks>
        ///<returns><b>true</b> if the specified type is supported; otherwise, <b>false</b>.</returns>
        public static bool SupportsType(Type type)
        {
            if (type.IsEnum)
            {
                return true;
            }
            switch (type.FullName)
            {
                case "System.DBNull":
                case "System.Byte":
				case "System.Byte[]":
                case "System.SByte":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int64":
                case "System.UInt64":
                case "System.UInt32":
                case "System.Int32":
                case "System.Single":
                case "System.Double":
                case "System.Boolean":
                case "System.Char":
                case "System.Text.StringBuilder":
                case "System.Decimal":
                case "System.DateTime":
                case "System.String":
                {
                    return true;
                }
            }
            return false;
        }

        internal static void ThrowExceptionForUnsupportedType(Type type)
        {
            if (!ExcelFile.SupportsType(type))
            {
                throw new NotSupportedException("Type " + type.Name + " is not supported.");
            }
        }


        // Properties
        internal CellStyleCachedCollection CellStyleCache
        {
            get
            {
                return this.cellStyleCache;
            }
        }

        ///<summary>
        ///Maximum number of affected cells in group set methods.
        ///</summary>
        ///<remarks>
        ///If user tries to modify all cells in a group which has more cells than specified limit, exception
        ///will be thrown. This property was introduced to prevent users from accidentally modifying millions
        ///of cells which results in a long delay, a large memory allocation and a big resulting file. You can
        ///set this limit to value which suits your needs (minimum is 5).
        ///</remarks>
        public int GroupMethodsAffectedCellsLimit
        {
            get
            {
                return this.groupMethodsAffectedCellsLimit;
            }
            set
            {
                if (value < 5)
                {
                    throw new ArgumentOutOfRangeException("value", value, "GroupMethodsAffectedCellsLimit must be larger than 5.");
                }
                this.groupMethodsAffectedCellsLimit = value;
                this.cellStyleCache.AddQueueSize = ExcelFile.QueueSizeFromAffectedCellsLimit(value);
            }
        }

        ///<summary>
        ///Gets or sets the workbook protection flag.
        ///</summary>
        ///<remarks>
        ///This property is simply written to Excel file and has no effect on the behavior of this library.
        ///For more information on workbook protection, consult Microsoft Excel documentation.
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelWorksheet.Protected">ExcelWorksheet.Protected</seealso>
        public bool Protected
        {
            get
            {
                return this.protectedMbr;
            }
            set
            {
                this.protectedMbr = value;
            }
        }

        public MB.WinEIDrive.Excel.RowColumnResolutionMethod RowColumnResolutionMethod
        {
            get
            {
                return this.rowColumnResolutionMethod;
            }
            set
            {
                this.rowColumnResolutionMethod = value;
            }
        }

        ///<summary>
        ///Collection of all worksheets (<see cref="MB.WinEIDrive.Excel.ExcelWorksheet">ExcelWorksheet</see>) in a workbook.
        ///</summary>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelWorksheet" />
        public ExcelWorksheetCollection Worksheets
        {
            get
            {
                return this.worksheets;
            }
        }


        // Fields
        private CellStyleCachedCollection cellStyleCache;
        private byte[] documentSummaryStream;
        private int groupMethodsAffectedCellsLimit;
        internal int HashFactorA;
        internal int HashFactorB;
        internal string IDText;
        private License license;
        ///<summary>
        ///Maximum number of user-defined cell styles in Microsoft Excel.
        ///</summary>
        public const int MaxCellStyles = 0xf8b;
        ///<summary>
        ///Maximum number of colors in Microsoft Excel.
        ///</summary>
        ///<remarks>
        ///This number includes 8 default colors:
        ///<see cref="System.Drawing.Color.Black">Color.Black</see>,
        ///<see cref="System.Drawing.Color.White">Color.White</see>,
        ///<see cref="System.Drawing.Color.Red">Color.Red</see>,
        ///<see cref="System.Drawing.Color.Green">Color.Green</see>,
        ///<see cref="System.Drawing.Color.Blue">Color.Blue</see>,
        ///<see cref="System.Drawing.Color.Yellow">Color.Yellow</see>,
        ///<see cref="System.Drawing.Color.Magenta">Color.Magenta</see> and
        ///<see cref="System.Drawing.Color.Cyan">Color.Cyan</see>.
        ///</remarks>
        public const int MaxColors = 0x38;
        ///<summary>
        ///Maximum number of columns in <see cref="MB.WinEIDrive.Excel.ExcelWorksheet">ExcelWorksheet</see>.
        ///</summary>
        public const int MaxColumns = 0x150;
        ///<summary>
        ///Maximum number of rows in <see cref="MB.WinEIDrive.Excel.ExcelWorksheet">ExcelWorksheet</see>.
        ///</summary>
        public const int MaxRows = 0x100000;
        internal PreservedRecords PreservedGlobalRecords;
        private bool protectedMbr;
        private MB.WinEIDrive.Excel.RowColumnResolutionMethod rowColumnResolutionMethod;
        private byte[] summaryStream;
        private ExcelWorksheetCollection worksheets;
    }
}

