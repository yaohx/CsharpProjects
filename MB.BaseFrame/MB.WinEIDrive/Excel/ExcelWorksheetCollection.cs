namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Reflection;

    ///<summary>
    ///Collection of worksheets (<see cref="MB.WinEIDrive.Excel.ExcelWorksheet">ExcelWorksheet</see>).
    ///</summary>
    ///<seealso cref="MB.WinEIDrive.Excel.ExcelWorksheet" />
    internal sealed class ExcelWorksheetCollection : IEnumerable
    {
        // Methods
        internal ExcelWorksheetCollection(ExcelFile parent)
        {
            this.sheetIndexes = new ArrayList();
            this.parent = parent;
            this.worksheetArray = new ArrayList();
        }

        ///<summary>
        ///Adds an empty worksheet to the end of the collection.
        ///</summary>
        ///<param name="worksheetName">Worksheet name.</param>
        ///<returns>Newly created worksheet.</returns>
        ///<remarks>
        ///If this is the first worksheet added to the collection the
        ///<see cref="MB.WinEIDrive.Excel.ExcelWorksheetCollection.ActiveWorksheet">ActiveWorksheet</see> is set to this worksheet.
        ///</remarks>
        ///<exception cref="System.ArgumentException">Thrown if worksheet name is not unique.</exception>
        public ExcelWorksheet Add(string worksheetName)
        {
            return this.InsertInternal(worksheetName, this.worksheetArray.Count);
        }

        ///<summary>
        ///Adds a copy of an existing worksheet to the end of the collection.
        ///</summary>
        ///<param name="destinationWorksheetName">Name of new worksheet.</param>
        ///<param name="sourceWorksheet">Source worksheet.</param>
        ///<returns>Newly created worksheet.</returns>
        ///<remarks>
        ///If this is the first worksheet added to the collection the
        ///<see cref="MB.WinEIDrive.Excel.ExcelWorksheetCollection.ActiveWorksheet">ActiveWorksheet</see> is set to this worksheet.
        ///</remarks>
        ///<exception cref="System.ArgumentException">Thrown if worksheet name is not unique.</exception>
        public ExcelWorksheet AddCopy(string destinationWorksheetName, ExcelWorksheet sourceWorksheet)
        {
            return this.InsertCopyInternal(destinationWorksheetName, this.worksheetArray.Count, sourceWorksheet);
        }

        internal ushort AddSheetReference(string sheet)
        {
            ushort num1 = this.GetSheetIndex(sheet);
            if (!this.sheetIndexes.Contains(num1))
            {
                this.sheetIndexes.Add(num1);
                return (ushort) (this.sheetIndexes.Count - 1);
            }
            for (int num2 = 0; num2 < this.sheetIndexes.Count; num2++)
            {
                ushort num3 = (ushort) this.sheetIndexes[num2];
                if (num1 == num3)
                {
                    return (ushort) num2;
                }
            }
            return 0;
        }

        internal void DeleteInternal(ExcelWorksheet ws)
        {
            if (this.activeWorksheet == ws)
            {
                this.activeWorksheet = null;
            }
            ushort num1 = this.GetSheetIndex(ws.Name);
            for (int num2 = 0; num2 < this.sheetIndexes.Count; num2++)
            {
                ushort num3 = (ushort) this.sheetIndexes[num2];
                if (num3 > num1)
                {
                    this.sheetIndexes[num2] = (ushort) (num3 - 1);
                }
            }
            this.worksheetArray.Remove(ws);
        }

        internal void ExceptionIfNotUnique(string worksheetName)
        {
            foreach (ExcelWorksheet worksheet1 in this.worksheetArray)
            {
                if (worksheet1.Name == worksheetName)
                {
                    throw new ArgumentException("Provided worksheet name is not unique.", "worksheetName");
                }
            }
        }

        internal int GetActiveWorksheetIndex()
        {
            if (this.Count == 0)
            {
                throw new Exception("Workbook must contain at least one worksheet. Use ExcelFile.Worksheets.Add() method to create a new worksheet.");
            }
            for (int num1 = 0; num1 < this.Count; num1++)
            {
                if (this[num1] == this.activeWorksheet)
                {
                    return num1;
                }
            }
            throw new Exception("Internal: Can't find ActiveWorksheet.");
        }

        ///<summary>
        ///Returns an enumerator for the <see cref="MB.WinEIDrive.Excel.ExcelWorksheetCollection">
        ///ExcelWorksheetCollection</see>.
        ///</summary>
        public IEnumerator GetEnumerator()
        {
            return this.worksheetArray.GetEnumerator();
        }

        internal ushort GetSheetIndex(string sheet)
        {
            ushort num1 = 0;
            foreach (ExcelWorksheet worksheet1 in this.worksheetArray)
            {
                if (worksheet1.Name == sheet)
                {
                    return num1;
                }
                num1 = (ushort) (num1 + 1);
            }
            return 0;
        }

        internal int IndexOf(ExcelWorksheet ws)
        {
            return this.worksheetArray.IndexOf(ws);
        }

        internal ExcelWorksheet InsertCopyInternal(string destinationWorksheetName, int position, ExcelWorksheet sourceWorksheet)
        {
            this.ExceptionIfNotUnique(destinationWorksheetName);
            ExcelWorksheet worksheet1 = new ExcelWorksheet(destinationWorksheetName, this, sourceWorksheet);
            this.worksheetArray.Insert(position, worksheet1);
            if (sourceWorksheet.ParentExcelFile != worksheet1.ParentExcelFile)
            {
                worksheet1.ParentExcelFile.CopyDrawings(sourceWorksheet.ParentExcelFile);
            }
            return worksheet1;
        }

        internal ExcelWorksheet InsertInternal(string worksheetName, int position)
        {
            this.ExceptionIfNotUnique(worksheetName);
            ExcelWorksheet worksheet1 = new ExcelWorksheet(worksheetName, this);
            this.worksheetArray.Insert(position, worksheet1);
            return worksheet1;
        }


        // Properties
        ///<summary>
        ///Gets or sets active worksheet.
        ///</summary>
        ///<remarks>
        ///Active worksheet is the one selected when file is opened with Microsoft Excel. By default active worksheet
        ///is the first one added with <see cref="MB.WinEIDrive.Excel.ExcelWorksheetCollection.Add(System.String)">Add</see> method.
        ///</remarks>
        public ExcelWorksheet ActiveWorksheet
        {
            get
            {
                if ((this.activeWorksheet == null) && (this.worksheetArray.Count > 0))
                {
                    this.activeWorksheet = this[0];
                }
                return this.activeWorksheet;
            }
            set
            {
                this.activeWorksheet = value;
                if (this.GetActiveWorksheetIndex() >= 5)
                {
                    this.activeWorksheet = this[0];
                }
            }
        }

        ///<summary>
        ///Gets the number of elements contained in the <see cref="MB.WinEIDrive.Excel.ExcelWorksheetCollection">
        ///ExcelWorksheetCollection</see>.
        ///</summary>
        public int Count
        {
            get
            {
                return this.worksheetArray.Count;
            }
        }

        ///<summary>
        ///Gets the worksheet with the specified name.
        ///</summary>
        ///<param name="name">The name of the worksheet.</param>
        public ExcelWorksheet this[string name]
        {
            get
            {
                foreach (ExcelWorksheet worksheet1 in this.worksheetArray)
                {
                    if (worksheet1.Name == name)
                    {
                        return worksheet1;
                    }
                }
                throw new ArgumentOutOfRangeException("name", name, "No worksheet with specified name.");
            }
        }

        ///<overloads>Gets the worksheet with the specified index or name.</overloads>
        ///<summary>
        ///Gets the worksheet with the specified index.
        ///</summary>
        ///<param name="index">The zero-based index of the worksheet.</param>
        public ExcelWorksheet this[int index]
        {
            get
            {
                return (ExcelWorksheet) this.worksheetArray[index];
            }
        }

        internal ExcelFile Parent
        {
            get
            {
                return this.parent;
            }
        }

        ///<summary>
        ///Gets the sheet indexes.
        ///</summary>
        ///<value>The sheet indexes.</value>
        internal ushort[] SheetIndexes
        {
            get
            {
                return (ushort[]) this.sheetIndexes.ToArray(typeof(ushort));
            }
        }

        ///<summary>
        ///Gets the sheet names.
        ///</summary>
        ///<value>The sheet names.</value>
        internal string[] SheetNames
        {
            get
            {
                string[] textArray1 = new string[this.worksheetArray.Count];
                int num1 = 0;
                foreach (ExcelWorksheet worksheet1 in this)
                {
                    textArray1[num1++] = worksheet1.Name;
                }
                return textArray1;
            }
        }


        // Fields
        private ExcelWorksheet activeWorksheet;
        private ExcelFile parent;
        private ArrayList sheetIndexes;
        private ArrayList worksheetArray;
    }
}

