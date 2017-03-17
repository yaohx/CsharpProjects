namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;

    internal class XLSFileReader
    {
        // Methods
        static XLSFileReader()
        {
            object[,] objArray1 = new object[0x42, 4];
            objArray1[0, 0] = 0;
            objArray1[0, 1] = 0;
            objArray1[0, 2] = 0;
            objArray1[0, 3] = 0;
            objArray1[1, 0] = 1;
            objArray1[1, 1] = 0xff;
            objArray1[1, 2] = 0xff;
            objArray1[1, 3] = 0xff;
            objArray1[2, 0] = 2;
            objArray1[2, 1] = 0xff;
            objArray1[2, 2] = 0;
            objArray1[2, 3] = 0;
            objArray1[3, 0] = 3;
            objArray1[3, 1] = 0;
            objArray1[3, 2] = 0xff;
            objArray1[3, 3] = 0;
            objArray1[4, 0] = 4;
            objArray1[4, 1] = 0;
            objArray1[4, 2] = 0;
            objArray1[4, 3] = 0xff;
            objArray1[5, 0] = 5;
            objArray1[5, 1] = 0xff;
            objArray1[5, 2] = 0xff;
            objArray1[5, 3] = 0;
            objArray1[6, 0] = 6;
            objArray1[6, 1] = 0xff;
            objArray1[6, 2] = 0;
            objArray1[6, 3] = 0xff;
            objArray1[7, 0] = 7;
            objArray1[7, 1] = 0;
            objArray1[7, 2] = 0xff;
            objArray1[7, 3] = 0xff;
            objArray1[8, 0] = 8;
            objArray1[8, 1] = 0;
            objArray1[8, 2] = 0;
            objArray1[8, 3] = 0;
            objArray1[9, 0] = 9;
            objArray1[9, 1] = 0xff;
            objArray1[9, 2] = 0xff;
            objArray1[9, 3] = 0xff;
            objArray1[10, 0] = 10;
            objArray1[10, 1] = 0xff;
            objArray1[10, 2] = 0;
            objArray1[10, 3] = 0;
            objArray1[11, 0] = 11;
            objArray1[11, 1] = 0;
            objArray1[11, 2] = 0xff;
            objArray1[11, 3] = 0;
            objArray1[12, 0] = 12;
            objArray1[12, 1] = 0;
            objArray1[12, 2] = 0;
            objArray1[12, 3] = 0xff;
            objArray1[13, 0] = 13;
            objArray1[13, 1] = 0xff;
            objArray1[13, 2] = 0xff;
            objArray1[13, 3] = 0;
            objArray1[14, 0] = 14;
            objArray1[14, 1] = 0xff;
            objArray1[14, 2] = 0;
            objArray1[14, 3] = 0xff;
            objArray1[15, 0] = 15;
            objArray1[15, 1] = 0;
            objArray1[15, 2] = 0xff;
            objArray1[15, 3] = 0xff;
            objArray1[0x10, 0] = 0x10;
            objArray1[0x10, 1] = 0x80;
            objArray1[0x10, 2] = 0;
            objArray1[0x10, 3] = 0;
            objArray1[0x11, 0] = 0x11;
            objArray1[0x11, 1] = 0;
            objArray1[0x11, 2] = 0x80;
            objArray1[0x11, 3] = 0;
            objArray1[0x12, 0] = 0x12;
            objArray1[0x12, 1] = 0;
            objArray1[0x12, 2] = 0;
            objArray1[0x12, 3] = 0x80;
            objArray1[0x13, 0] = 0x13;
            objArray1[0x13, 1] = 0x80;
            objArray1[0x13, 2] = 0x80;
            objArray1[0x13, 3] = 0;
            objArray1[20, 0] = 20;
            objArray1[20, 1] = 0x80;
            objArray1[20, 2] = 0;
            objArray1[20, 3] = 0x80;
            objArray1[0x15, 0] = 0x15;
            objArray1[0x15, 1] = 0;
            objArray1[0x15, 2] = 0x80;
            objArray1[0x15, 3] = 0x80;
            objArray1[0x16, 0] = 0x16;
            objArray1[0x16, 1] = 0xc0;
            objArray1[0x16, 2] = 0xc0;
            objArray1[0x16, 3] = 0xc0;
            objArray1[0x17, 0] = 0x17;
            objArray1[0x17, 1] = 0x80;
            objArray1[0x17, 2] = 0x80;
            objArray1[0x17, 3] = 0x80;
            objArray1[0x18, 0] = 0x18;
            objArray1[0x18, 1] = 0x99;
            objArray1[0x18, 2] = 0x99;
            objArray1[0x18, 3] = 0xff;
            objArray1[0x19, 0] = 0x19;
            objArray1[0x19, 1] = 0x99;
            objArray1[0x19, 2] = 0x33;
            objArray1[0x19, 3] = 0x66;
            objArray1[0x1a, 0] = 0x1a;
            objArray1[0x1a, 1] = 0xff;
            objArray1[0x1a, 2] = 0xff;
            objArray1[0x1a, 3] = 0xcc;
            objArray1[0x1b, 0] = 0x1b;
            objArray1[0x1b, 1] = 0xcc;
            objArray1[0x1b, 2] = 0xff;
            objArray1[0x1b, 3] = 0xff;
            objArray1[0x1c, 0] = 0x1c;
            objArray1[0x1c, 1] = 0x66;
            objArray1[0x1c, 2] = 0;
            objArray1[0x1c, 3] = 0x66;
            objArray1[0x1d, 0] = 0x1d;
            objArray1[0x1d, 1] = 0xff;
            objArray1[0x1d, 2] = 0x80;
            objArray1[0x1d, 3] = 0x80;
            objArray1[30, 0] = 30;
            objArray1[30, 1] = 0;
            objArray1[30, 2] = 0x66;
            objArray1[30, 3] = 0xcc;
            objArray1[0x1f, 0] = 0x1f;
            objArray1[0x1f, 1] = 0xcc;
            objArray1[0x1f, 2] = 0xcc;
            objArray1[0x1f, 3] = 0xff;
            objArray1[0x20, 0] = 0x20;
            objArray1[0x20, 1] = 0;
            objArray1[0x20, 2] = 0;
            objArray1[0x20, 3] = 0x80;
            objArray1[0x21, 0] = 0x21;
            objArray1[0x21, 1] = 0xff;
            objArray1[0x21, 2] = 0;
            objArray1[0x21, 3] = 0xff;
            objArray1[0x22, 0] = 0x22;
            objArray1[0x22, 1] = 0xff;
            objArray1[0x22, 2] = 0xff;
            objArray1[0x22, 3] = 0;
            objArray1[0x23, 0] = 0x23;
            objArray1[0x23, 1] = 0;
            objArray1[0x23, 2] = 0xff;
            objArray1[0x23, 3] = 0xff;
            objArray1[0x24, 0] = 0x24;
            objArray1[0x24, 1] = 0x80;
            objArray1[0x24, 2] = 0;
            objArray1[0x24, 3] = 0x80;
            objArray1[0x25, 0] = 0x25;
            objArray1[0x25, 1] = 0x80;
            objArray1[0x25, 2] = 0;
            objArray1[0x25, 3] = 0;
            objArray1[0x26, 0] = 0x26;
            objArray1[0x26, 1] = 0;
            objArray1[0x26, 2] = 0x80;
            objArray1[0x26, 3] = 0x80;
            objArray1[0x27, 0] = 0x27;
            objArray1[0x27, 1] = 0;
            objArray1[0x27, 2] = 0;
            objArray1[0x27, 3] = 0xff;
            objArray1[40, 0] = 40;
            objArray1[40, 1] = 0;
            objArray1[40, 2] = 0xcc;
            objArray1[40, 3] = 0xff;
            objArray1[0x29, 0] = 0x29;
            objArray1[0x29, 1] = 0xcc;
            objArray1[0x29, 2] = 0xff;
            objArray1[0x29, 3] = 0xff;
            objArray1[0x2a, 0] = 0x2a;
            objArray1[0x2a, 1] = 0xcc;
            objArray1[0x2a, 2] = 0xff;
            objArray1[0x2a, 3] = 0xcc;
            objArray1[0x2b, 0] = 0x2b;
            objArray1[0x2b, 1] = 0xff;
            objArray1[0x2b, 2] = 0xff;
            objArray1[0x2b, 3] = 0x99;
            objArray1[0x2c, 0] = 0x2c;
            objArray1[0x2c, 1] = 0x99;
            objArray1[0x2c, 2] = 0xcc;
            objArray1[0x2c, 3] = 0xff;
            objArray1[0x2d, 0] = 0x2d;
            objArray1[0x2d, 1] = 0xff;
            objArray1[0x2d, 2] = 0x99;
            objArray1[0x2d, 3] = 0xcc;
            objArray1[0x2e, 0] = 0x2e;
            objArray1[0x2e, 1] = 0xcc;
            objArray1[0x2e, 2] = 0x99;
            objArray1[0x2e, 3] = 0xff;
            objArray1[0x2f, 0] = 0x2f;
            objArray1[0x2f, 1] = 0xff;
            objArray1[0x2f, 2] = 0xcc;
            objArray1[0x2f, 3] = 0x99;
            objArray1[0x30, 0] = 0x30;
            objArray1[0x30, 1] = 0x33;
            objArray1[0x30, 2] = 0x66;
            objArray1[0x30, 3] = 0xff;
            objArray1[0x31, 0] = 0x31;
            objArray1[0x31, 1] = 0x33;
            objArray1[0x31, 2] = 0xcc;
            objArray1[0x31, 3] = 0xcc;
            objArray1[50, 0] = 50;
            objArray1[50, 1] = 0x99;
            objArray1[50, 2] = 0xcc;
            objArray1[50, 3] = 0;
            objArray1[0x33, 0] = 0x33;
            objArray1[0x33, 1] = 0xff;
            objArray1[0x33, 2] = 0xcc;
            objArray1[0x33, 3] = 0;
            objArray1[0x34, 0] = 0x34;
            objArray1[0x34, 1] = 0xff;
            objArray1[0x34, 2] = 0x99;
            objArray1[0x34, 3] = 0;
            objArray1[0x35, 0] = 0x35;
            objArray1[0x35, 1] = 0xff;
            objArray1[0x35, 2] = 0x66;
            objArray1[0x35, 3] = 0;
            objArray1[0x36, 0] = 0x36;
            objArray1[0x36, 1] = 0x66;
            objArray1[0x36, 2] = 0x66;
            objArray1[0x36, 3] = 0x99;
            objArray1[0x37, 0] = 0x37;
            objArray1[0x37, 1] = 150;
            objArray1[0x37, 2] = 150;
            objArray1[0x37, 3] = 150;
            objArray1[0x38, 0] = 0x38;
            objArray1[0x38, 1] = 0;
            objArray1[0x38, 2] = 0x33;
            objArray1[0x38, 3] = 0x66;
            objArray1[0x39, 0] = 0x39;
            objArray1[0x39, 1] = 0x33;
            objArray1[0x39, 2] = 0x99;
            objArray1[0x39, 3] = 0x66;
            objArray1[0x3a, 0] = 0x3a;
            objArray1[0x3a, 1] = 0;
            objArray1[0x3a, 2] = 0x33;
            objArray1[0x3a, 3] = 0;
            objArray1[0x3b, 0] = 0x3b;
            objArray1[0x3b, 1] = 0x33;
            objArray1[0x3b, 2] = 0x33;
            objArray1[0x3b, 3] = 0;
            objArray1[60, 0] = 60;
            objArray1[60, 1] = 0x99;
            objArray1[60, 2] = 0x33;
            objArray1[60, 3] = 0;
            objArray1[0x3d, 0] = 0x3d;
            objArray1[0x3d, 1] = 0x99;
            objArray1[0x3d, 2] = 0x33;
            objArray1[0x3d, 3] = 0x66;
            objArray1[0x3e, 0] = 0x3e;
            objArray1[0x3e, 1] = 0x33;
            objArray1[0x3e, 2] = 0x33;
            objArray1[0x3e, 3] = 0x99;
            objArray1[0x3f, 0] = 0x3f;
            objArray1[0x3f, 1] = 0x33;
            objArray1[0x3f, 2] = 0x33;
            objArray1[0x3f, 3] = 0x33;
            objArray1[0x40, 0] = 0x40;
            objArray1[0x40, 1] = 0;
            objArray1[0x40, 2] = 0;
            objArray1[0x40, 3] = 0;
            objArray1[0x41, 0] = 0x41;
            objArray1[0x41, 1] = 0xff;
            objArray1[0x41, 2] = 0xff;
            objArray1[0x41, 3] = 0xff;
            XLSFileReader.defaultPalette = objArray1;
        }

        public XLSFileReader(ExcelFile excelFile, XlsOptions xlsOptions)
        {
            this.colorsTable = null;
            this.numberFormats = new NumberFormatCollection(true);
            this.fontsTable = new ArrayList();
            this.cellStylesTable = new ArrayList();
            this.excelFile = excelFile;
            this.xlsOptions = xlsOptions;
        }

        private void CleanAllIndexes()
        {
            foreach (ExcelFontData data1 in this.fontsTable)
            {
                if (data1 != null)
                {
                    data1.ColorIndex = -1;
                }
            }
            foreach (CellStyle style1 in this.cellStylesTable)
            {
                style1.Element.Indexes = null;
            }
        }

        private Color ColorIndexToColor(int colorIndex)
        {
            if (colorIndex > (this.colorsTable.Length - 1))
            {
                colorIndex = 0;
            }
            return this.colorsTable[colorIndex];
        }

        private void ConvertColorIndexesToColors()
        {
            if ((this.fontsTable.Count == 0) || (this.cellStylesTable.Count == 0))
            {
                throw new Exception("Internal: fontsTable or cellStylesTable is empty.");
            }
            foreach (ExcelFontData data1 in this.fontsTable)
            {
                if (data1 != null)
                {
                    data1.Color = this.ColorIndexToColor(data1.ColorIndex);
                }
            }
            foreach (CellStyle style1 in this.cellStylesTable)
            {
                CellStyleData data2 = style1.Element;
                style1.Borders[IndividualBorder.Right].LineColor = this.ColorIndexToColor(data2.Indexes.BorderColorIndex[3]);
                style1.Borders[IndividualBorder.Left].LineColor = this.ColorIndexToColor(data2.Indexes.BorderColorIndex[2]);
                style1.Borders[IndividualBorder.Bottom].LineColor = this.ColorIndexToColor(data2.Indexes.BorderColorIndex[1]);
                style1.Borders[IndividualBorder.Top].LineColor = this.ColorIndexToColor(data2.Indexes.BorderColorIndex[0]);
                data2.BorderColor[4] = this.ColorIndexToColor(data2.Indexes.BorderColorIndex[4]);
                data2.PatternBackgroundColor = this.ColorIndexToColor(data2.Indexes.PatternBackgroundColorIndex);
                data2.PatternForegroundColor = this.ColorIndexToColor(data2.Indexes.PatternForegroundColorIndex);
            }
        }

        public void ImportRecords(AbsXLSRecords records, string diagnosticsFileName)
        {
            int num1 = 0;
            int num2 = 0;
            int num4 = 0;
            string[] textArray1 = null;
            bool flag1 = false;
            int num5 = -1;
            CellFormula formula1 = null;
            int num6 = -1;
            if ((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None)
            {
                this.excelFile.PreservedGlobalRecords = new PreservedRecords();
            }
            else
            {
                this.excelFile.PreservedGlobalRecords = null;
            }
            this.LoadDefaultPalette();
            foreach (AbsXLSRec rec1 in records)
            {
                int num3;
                object[] objArray1;
                ExcelWorksheet worksheet1;
                RKRecord record3;
                int num16;
                string text3;
                string text1 = rec1.Name;
                if (text1 != "Continue")
                {
                    num5 = rec1.RecordCode;
                }
                switch (text1)
                {
                    case "BOF":
                    {
                        num1++;
                        if ((num1 == 1) && (num2 > 0))
                        {
                            worksheet1 = this.excelFile.Worksheets[num2 - 1];
                            if ((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) == XlsOptions.None)
                            {
                                goto Label_07BF;
                            }
                            worksheet1.PreservedWorksheetRecords = new PreservedRecords();
                        }
                        continue;
                    }
                    case "EOF":
                    {
                        goto Label_07CC;
                    }
                    case "BoundSheet":
                    {
                        if ((num1 == 1) && (num2 == 0))
                        {
                            string text2 = ((BoundSheetRecord) rec1).SheetName.Str;
                            this.excelFile.Worksheets.Add(text2);
                        }
                        continue;
                    }
                    case "SST":
                    {
                        if ((num1 != 1) || (num2 != 0))
                        {
                            continue;
                        }
                        SSTRecord record1 = (SSTRecord) rec1;
                        textArray1 = new string[record1.TotalStringCount];
                        this.excelStrings = record1.ExcelStrings;
                        num3 = 0;
                        goto Label_0878;
                    }
                    case "Palette":
                    {
                        if ((num1 == 1) && (num2 == 0))
                        {
                            this.LoadPalette(rec1);
                        }
                        continue;
                    }
                    case "Format":
                    {
                        if ((num1 == 1) && (num2 == 0))
                        {
                            this.LoadNumberFormat(rec1);
                        }
                        continue;
                    }
                    case "Font":
                    {
                        if ((num1 == 1) && (num2 == 0))
                        {
                            this.fontsTable.Add(this.LoadFont(rec1));
                            if (this.fontsTable.Count == 4)
                            {
                                this.fontsTable.Add(null);
                            }
                        }
                        continue;
                    }
                    case "XF":
                    {
                        if ((num1 == 1) && (num2 == 0))
                        {
                            this.cellStylesTable.Add(this.LoadCellStyle(rec1));
                        }
                        continue;
                    }
                    case "WRITEPROT":
                    case "WRITEACCESS":
                    case "FILESHARING":
                    case "CODEPAGE":
                    case "HIDEOBJ":
                    case "DATEMODE":
                    case "PRECISION":
                    case "REFRESHALL":
                    case "BOOKBOOL":
                    case "USESELFS":
                    case "COUNTRY":
                    case "MSODRAWINGGROUP":
                    case "SUPBOOK":
                    {
                        if ((((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None) && (num1 == 1)) && (num2 == 0))
                        {
                            this.excelFile.PreservedGlobalRecords.Add((XLSRecord) rec1);
                        }
                        continue;
                    }
                    case "NAME":
                    {
                        this.LoadName(rec1);
                        continue;
                    }
                    case "EXTERNSHEET":
                    {
                        this.LoadExternsheet(rec1);
                        continue;
                    }
                    case "WINDOWPROTECT":
                    case "OBJECTPROTECT":
                    {
                        if (num1 == 1)
                        {
                            if (num2 != 0)
                            {
                                goto Label_09C4;
                            }
                            if ((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None)
                            {
                                this.excelFile.PreservedGlobalRecords.Add((XLSRecord) rec1);
                            }
                        }
                        continue;
                    }
                    case "Continue":
                    {
                        string[] textArray2;
                        IntPtr ptr1;
                        if (num1 != 1)
                        {
                            continue;
                        }
                        ContinueRecord record2 = (ContinueRecord) rec1;
                        this.excelStrings = record2.ExcelStrings;
                        if (this.excelStrings == null)
                        {
                            goto Label_0ABC;
                        }
                        if (!flag1)
                        {
                            goto Label_0A61;
                        }
                        num4--;
                        (textArray2 = textArray1)[(int) (ptr1 = (IntPtr) num4)] = textArray2[(int) ptr1] + ((ExcelLongString) this.excelStrings.Strings[0]).Str;
                        num3 = 1;
                        goto Label_0A8A;
                    }
                    case "ColumnInfo":
                    {
                        if ((num1 == 1) && (num2 > 0))
                        {
                            this.LoadColumnInfo(rec1, this.excelFile.Worksheets[num2 - 1]);
                        }
                        continue;
                    }
                    case "Row":
                    {
                        if ((num1 == 1) && (num2 > 0))
                        {
                            this.LoadRow(rec1, this.excelFile.Worksheets[num2 - 1]);
                        }
                        continue;
                    }
                    case "MergedCells":
                    {
                        if ((num1 == 1) && (num2 > 0))
                        {
                            this.LoadMergedCells(rec1, this.excelFile.Worksheets[num2 - 1]);
                        }
                        continue;
                    }
                    case "WSBool":
                    {
                        if ((num1 == 1) && (num2 > 0))
                        {
                            this.LoadWSBool(rec1, this.excelFile.Worksheets[num2 - 1]);
                        }
                        continue;
                    }
                    case "LabelSST":
                    case "RK":
                    case "Number":
                    case "Blank":
                    case "Formula":
                    {
                        if ((num1 != 1) || (num2 <= 0))
                        {
                            continue;
                        }
                        worksheet1 = this.excelFile.Worksheets[num2 - 1];
                        goto Label_0C55;
                    }
                    case "MulRK":
                    {
                        goto Label_0DF2;
                    }
                    case "MulBlank":
                    {
                        goto Label_0ECF;
                    }
                    case "Window2":
                    {
                        goto Label_0FA9;
                    }
                    case "HORIZONTALPAGEBREAKS":
                    case "VERTICALPAGEBREAKS":
                    {
                        if ((num1 != 1) || (num2 <= 0))
                        {
                            continue;
                        }
                        worksheet1 = this.excelFile.Worksheets[num2 - 1];
                        objArray1 = ((XLSRecord) rec1).GetArguments();
                        goto Label_10B8;
                    }
                    case "SCL":
                    {
                        if ((num1 == 1) && (num2 > 0))
                        {
                            worksheet1 = this.excelFile.Worksheets[num2 - 1];
                            objArray1 = ((XLSRecord) rec1).GetArguments();
                            num16 = (100 * ((ushort) objArray1[0])) / ((ushort) objArray1[1]);
                            if (!worksheet1.ShowInPageBreakPreview)
                            {
                                goto Label_116A;
                            }
                            worksheet1.PageBreakViewZoom = num16;
                        }
                        continue;
                    }
                    case "SETUP":
                    {
                        if ((num1 == 1) && (num2 > 0))
                        {
                            worksheet1 = this.excelFile.Worksheets[num2 - 1];
                            objArray1 = ((XLSRecord) rec1).GetArguments();
                            worksheet1.paperSize = (ushort) objArray1[0];
                            worksheet1.scalingFactor = (ushort) objArray1[1];
                            worksheet1.startPageNumber = (ushort) objArray1[2];
                            worksheet1.fitWorksheetWidthToPages = (ushort) objArray1[3];
                            worksheet1.fitWorksheetHeightToPages = (ushort) objArray1[4];
                            worksheet1.setupOptions = (SetupOptions) objArray1[5];
                            worksheet1.printResolution = (ushort) objArray1[6];
                            worksheet1.verticalPrintResolution = (ushort) objArray1[7];
                            worksheet1.headerMargin = (double) objArray1[8];
                            worksheet1.footerMargin = (double) objArray1[9];
                            worksheet1.numberOfCopies = (ushort) objArray1[10];
                        }
                        continue;
                    }
                    case "CALCCOUNT":
                    case "CALCMODE":
                    case "REFMODE":
                    case "DELTA":
                    case "ITERATION":
                    case "SAVERECALC":
                    case "PRINTHEADERS":
                    case "PRINTGRIDLINES":
                    case "GRIDSET":
                    case "HEADER":
                    case "FOOTER":
                    case "HCENTER":
                    case "LEFTMARGIN":
                    case "RIGHTMARGIN":
                    case "TOPMARGIN":
                    case "BOTTOMMARGIN":
                    case "SORT":
                    case "PANE":
                    case "SELECTION":
                    case "STANDARDWIDTH":
                    case "LABELRANGES":
                    case "HLINK":
                    case "QUICKTIP":
                    case "DVAL":
                    case "DV":
                    case "SHEETLAYOUT":
                    case "SHEETPROTECTION":
                    case "RANGEPROTECTION":
                    case "PASSWORD":
                    {
                        if ((((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num1 == 1)) && (num2 > 0))
                        {
                            worksheet1 = this.excelFile.Worksheets[num2 - 1];
                            worksheet1.PreservedWorksheetRecords.Add((XLSRecord) rec1);
                        }
                        continue;
                    }
                    case "MSODRAWING":
                    case "OBJ":
                    case "TXO":
                    {
                        if ((((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num1 == 1)) && (num2 > 0))
                        {
                            worksheet1 = this.excelFile.Worksheets[num2 - 1];
                            worksheet1.PreservedWorksheetRecords.Add((XLSRecord) rec1, -10);
                        }
                        continue;
                    }
                    case "CONDFMT":
                    case "CF":
                    {
                        if ((((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num1 == 1)) && (num2 > 0))
                        {
                            worksheet1 = this.excelFile.Worksheets[num2 - 1];
                            worksheet1.PreservedWorksheetRecords.Add((XLSRecord) rec1, -11);
                        }
                        continue;
                    }
                    case "SHRFMLA":
                    case "STRING":
                    {
                        goto Label_1344;
                    }
                    default:
                    {
                        continue;
                    }
                }
            Label_07BF:
                worksheet1.PreservedWorksheetRecords = null;
                continue;
            Label_07CC:
                num1--;
                if (num1 != 0)
                {
                    continue;
                }
                if (num2 == 0)
                {
                    this.ConvertColorIndexesToColors();
                }
                num2++;
                continue;
            Label_0855:
                textArray1[num3] = ((ExcelLongString) this.excelStrings.Strings[num3]).Str;
                num3++;
            Label_0878:
                if (num3 < this.excelStrings.Strings.Count)
                {
                    goto Label_0855;
                }
                if (this.excelStrings.CharsRemaining > 0)
                {
                    flag1 = true;
                }
                num4 = num3;
                continue;
            Label_09C4:
                if ((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None)
                {
                    worksheet1 = this.excelFile.Worksheets[num2 - 1];
                    worksheet1.PreservedWorksheetRecords.Add((XLSRecord) rec1);
                }
                continue;
            Label_0A61:
                num3 = 0;
            Label_0A8A:
                while (num3 < this.excelStrings.Strings.Count)
                {
                    textArray1[num3 + num4] = ((ExcelLongString) this.excelStrings.Strings[num3]).Str;
                    num3++;
                }
                if (this.excelStrings.CharsRemaining > 0)
                {
                    flag1 = true;
                }
                else
                {
                    flag1 = false;
                }
                num4 += num3;
                continue;
            Label_0ABC:
                if (num2 == 0)
                {
                    if ((this.xlsOptions & XlsOptions.PreserveGlobalRecords) != XlsOptions.None)
                    {
                        this.excelFile.PreservedGlobalRecords.Add((XLSRecord) rec1, num5);
                    }
                    continue;
                }
                if ((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) == XlsOptions.None)
                {
                    continue;
                }
                if (((num5 == XLSDescriptors.GetByName("MSODRAWING").Code) || (num5 == XLSDescriptors.GetByName("OBJ").Code)) || (num5 == XLSDescriptors.GetByName("TXO").Code))
                {
                    num5 = -10;
                }
                this.excelFile.Worksheets[num2 - 1].PreservedWorksheetRecords.Add((XLSRecord) rec1, num5);
                continue;
            Label_0C55:
                if ((text3 = text1) == null)
                {
                    goto Label_0D83;
                }
                text3 = string.IsInterned(text3);
                if (text3 != "LabelSST")
                {
                    if (text3 == "RK")
                    {
                        goto Label_0CCE;
                    }
                    if (text3 == "Number")
                    {
                        goto Label_0CF3;
                    }
                    if (text3 == "Blank")
                    {
                        goto Label_0D14;
                    }
                    if (text3 == "Formula")
                    {
                        goto Label_0D32;
                    }
                    goto Label_0D83;
                }
                objArray1 = ((XLSRecord) rec1).GetArguments();
                CellRecordHeader header1 = (CellRecordHeader) objArray1[0];
                object obj1 = textArray1[(int) ((IntPtr) ((uint) objArray1[1]))];
                goto Label_0D8E;
            Label_0CCE:
                record3 = (RKRecord) rec1;
                header1 = record3.Header;
                obj1 = XLSFileWriter.RKValueToObj(record3.Val);
                goto Label_0D8E;
            Label_0CF3:
                objArray1 = ((XLSRecord) rec1).GetArguments();
                header1 = (CellRecordHeader) objArray1[0];
                obj1 = objArray1[1];
                goto Label_0D8E;
            Label_0D14:
                objArray1 = ((XLSRecord) rec1).GetArguments();
                header1 = (CellRecordHeader) objArray1[0];
                obj1 = null;
                goto Label_0D8E;
            Label_0D32:
                objArray1 = ((XLSRecord) rec1).GetArguments();
                header1 = (CellRecordHeader) objArray1[0];
                if ((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None)
                {
                    formula1 = new CellFormula((object[]) objArray1[1], (FormulaOptions) objArray1[2], (object[]) objArray1[4]);
                    obj1 = formula1;
                    goto Label_0D8E;
                }
                obj1 = null;
                goto Label_0D8E;
            Label_0D83:
                throw new Exception("Internal: missing case in reading code.");
            Label_0D8E:
                if (header1.Row > num6)
                {
                    num6 = header1.Row;
                }
                if ((header1.Row < (this.excelFile.HashFactorA - this.excelFile.HashFactorB)) && ((num2 - 1) < 5))
                {
                    this.SetCell(worksheet1, header1.Row, header1.Column, header1.StyleIndex, obj1);
                }
                continue;
            Label_0DF2:
                if ((num1 != 1) || (num2 <= 0))
                {
                    continue;
                }
                worksheet1 = this.excelFile.Worksheets[num2 - 1];
                objArray1 = ((XLSRecord) rec1).GetArguments();
                int num7 = (ushort) objArray1[0];
                int num8 = (ushort) objArray1[1];
                object[] objArray2 = (object[]) objArray1[2];
                int num9 = (ushort) objArray1[3];
                if (num7 > num6)
                {
                    num6 = num7;
                }
                if ((num7 >= (this.excelFile.HashFactorA - this.excelFile.HashFactorB)) || ((num2 - 1) >= 5))
                {
                    continue;
                }
                int num10 = num8;
                num3 = 0;
                while (num10 <= num9)
                {
                    obj1 = XLSFileWriter.RKValueToObj((uint) objArray2[(num3 * 2) + 1]);
                    this.SetCell(worksheet1, num7, num10, (ushort) objArray2[num3 * 2], obj1);
                    num3++;
                    num10++;
                }
                continue;
            Label_0ECF:
                if ((num1 != 1) || (num2 <= 0))
                {
                    continue;
                }
                worksheet1 = this.excelFile.Worksheets[num2 - 1];
                objArray1 = ((XLSRecord) rec1).GetArguments();
                ushort num11 = (ushort) objArray1[0];
                ushort num12 = (ushort) objArray1[1];
                object[] objArray3 = (object[]) objArray1[2];
                ushort num13 = (ushort) objArray1[3];
                if (num11 > num6)
                {
                    num6 = num11;
                }
                if ((num11 >= (this.excelFile.HashFactorA - this.excelFile.HashFactorB)) || ((num2 - 1) >= 5))
                {
                    continue;
                }
                num3 = 0;
                while (num12 <= num13)
                {
                    worksheet1.Cells[num11, num12].Style = (CellStyle) this.cellStylesTable[(ushort) objArray3[num3]];
                    num3++;
                    num12 = (ushort) (num12 + 1);
                }
                continue;
            Label_0FA9:
                if ((num1 != 1) || (num2 <= 0))
                {
                    continue;
                }
                worksheet1 = this.excelFile.Worksheets[num2 - 1];
                objArray1 = ((XLSRecord) rec1).GetArguments();
                worksheet1.windowOptions = (WorksheetWindowOptions) objArray1[0];
                worksheet1.FirstVisibleRow = (ushort) objArray1[1];
                worksheet1.FirstVisibleColumn = (ushort) objArray1[2];
                int num14 = (ushort) objArray1[4];
                if (num14 != 0)
                {
                    worksheet1.PageBreakViewZoom = num14;
                }
                int num15 = (ushort) objArray1[5];
                if (num15 != 0)
                {
                    worksheet1.Zoom = num15;
                }
                if ((worksheet1.windowOptions & WorksheetWindowOptions.SheetSelected) != ((WorksheetWindowOptions) ((short) 0)))
                {
                    this.excelFile.Worksheets.ActiveWorksheet = this.excelFile.Worksheets[num2 - 1];
                }
                continue;
            Label_10B8:
                if ((text3 = text1) == null)
                {
                    continue;
                }
                text3 = string.IsInterned(text3);
                if (text3 != "HORIZONTALPAGEBREAKS")
                {
                    if (text3 == "VERTICALPAGEBREAKS")
                    {
                        goto Label_10F5;
                    }
                    continue;
                }
                worksheet1.HorizontalPageBreaks.LoadArgs(objArray1);
                continue;
            Label_10F5:
                worksheet1.VerticalPageBreaks.LoadArgs(objArray1);
                continue;
            Label_116A:
                worksheet1.Zoom = num16;
                continue;
            Label_1344:
                if ((((this.xlsOptions & XlsOptions.PreserveWorksheetRecords) != XlsOptions.None) && (num1 == 1)) && (num2 > 0))
                {
                    if (formula1.ExtraFormulaRecords == null)
                    {
                        formula1.ExtraFormulaRecords = new ArrayList();
                    }
                    formula1.ExtraFormulaRecords.Add((XLSRecord) rec1);
                    if (text1 == "STRING")
                    {
                        objArray1 = ((XLSRecord) rec1).GetArguments();
                        formula1.Value = ((ExcelLongString) objArray1[0]).Str;
                    }
                }
            }
            if (((num2 - 1) > 5) || (num6 >= (this.excelFile.HashFactorA - this.excelFile.HashFactorB)))
            {
                this.excelFile.OnLimitReached(diagnosticsFileName, LimitEventOperation.XlsReading, num6 + 1, num2 - 1, false);
            }
            else if (((num2 - 1) == 5) || (num6 >= (((this.excelFile.HashFactorA - this.excelFile.HashFactorB) * 4) / 5)))
            {
                this.excelFile.OnLimitNear(diagnosticsFileName, LimitEventOperation.XlsReading, num6 + 1, num2 - 1, false);
            }
            this.CleanAllIndexes();
        }

        private static bool IsDateTime(string numberFormat)
        {
            bool flag1 = false;
            int num1 = 0;
            bool flag2 = false;
            StringBuilder builder1 = null;
            ArrayList list1 = new ArrayList();
            string text2 = numberFormat;
            for (int num2 = 0; num2 < text2.Length; num2++)
            {
                char ch1 = text2[num2];
                char ch2 = ch1;
                if (ch2 != '"')
                {
                    switch (ch2)
                    {
                        case '[':
                        {
                            num1++;
                            flag2 = false;
                            goto Label_01CE;
                        }
                        case '\\':
                        {
                            goto Label_0196;
                        }
                        case ']':
                        {
                            num1--;
                            flag2 = false;
                            goto Label_01CE;
                        }
                    }
                }
                else
                {
                    flag1 = !flag1;
                    flag2 = false;
                    goto Label_01CE;
                }
            Label_0196:
                if (!flag1 && (num1 == 0))
                {
                    if (char.IsLetter(ch1))
                    {
                        if (!flag2)
                        {
                            flag2 = true;
                            if (builder1 != null)
                            {
                                list1.Add(builder1.ToString());
                            }
                            builder1 = new StringBuilder();
                        }
                        builder1.Append(ch1);
                    }
                    else
                    {
                        flag2 = false;
                    }
                }
            Label_01CE:;
            }
            if (builder1 != null)
            {
                list1.Add(builder1.ToString());
            }
            IEnumerator enumerator1 = list1.GetEnumerator();
            try
            {
                while (enumerator1.MoveNext())
                {
                    switch (((string) enumerator1.Current))
                    {
                        case "m":
                        case "mm":
                        case "mmm":
                        case "mmmm":
                        case "mmmmm":
                        case "d":
                        case "dd":
                        case "ddd":
                        case "dddd":
                        case "yy":
                        case "yyyy":
                        case "H":
                        case "h":
                        case "hh":
                        case "s":
                        case "ss":
                        {
                            return true;
                        }
                    }
                }
            }
            finally
            {
                IDisposable disposable1 = enumerator1 as IDisposable;
                if (disposable1 != null)
                {
                    disposable1.Dispose();
                }
            }
            return false;
        }

        private CellStyle LoadCellStyle(AbsXLSRec record)
        {
            if ((this.fontsTable.Count == 0) || (this.numberFormats.Count == 0))
            {
                throw new Exception("Internal: fontsTable or numberFormats is empty.");
            }
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            CellStyle style1 = new CellStyle();
            CellStyleData data1 = style1.Element;
            data1.Indexes = new CellStyleDataIndexes();
            int num1 = (ushort) objArray1[0];
            data1.FontData = (ExcelFontData) this.fontsTable[num1];
            data1.Indexes.NumberFormatIndex = (ushort) objArray1[1];
            data1.NumberFormat = (string) this.numberFormats[data1.Indexes.NumberFormatIndex];
            XFOptions1 options1 = (XFOptions1) objArray1[2];
            if ((options1 & XFOptions1.CellLocked) != ((XFOptions1) ((ushort) 0)))
            {
                data1.Locked = true;
            }
            if ((options1 & XFOptions1.FormulaHidden) != ((XFOptions1) ((ushort) 0)))
            {
                data1.FormulaHidden = true;
            }
            byte num2 = (byte) objArray1[3];
            data1.VerticalAlignment = (VerticalAlignmentStyle) (num2 >> 4);
            if ((num2 & 8) != 0)
            {
                data1.WrapText = true;
            }
            data1.HorizontalAlignment = ((HorizontalAlignmentStyle) num2) & HorizontalAlignmentStyle.Distributed;
            int num3 = (byte) objArray1[4];
            if ((num3 > 90) && (num3 < 0xff))
            {
                data1.Rotation = num3 - 0x100;
            }
            else
            {
                data1.Rotation = num3;
            }
            XFOptions2 options2 = (XFOptions2) objArray1[5];
            data1.Indent = ((int) options2) & 15;
            if ((options2 & XFOptions2.ShrinkToFit) != ((XFOptions2) ((ushort) 0)))
            {
                data1.ShrinkToFit = true;
            }
            ushort num4 = (ushort) objArray1[6];
            style1.Borders[IndividualBorder.Left].LineStyle = ((LineStyle) num4) & (LineStyle.SlantedDashDot | LineStyle.Medium);
            num4 = (ushort) (num4 >> 4);
            style1.Borders[IndividualBorder.Right].LineStyle = ((LineStyle) num4) & (LineStyle.SlantedDashDot | LineStyle.Medium);
            num4 = (ushort) (num4 >> 4);
            style1.Borders[IndividualBorder.Top].LineStyle = ((LineStyle) num4) & (LineStyle.SlantedDashDot | LineStyle.Medium);
            num4 = (ushort) (num4 >> 4);
            style1.Borders[IndividualBorder.Bottom].LineStyle = ((LineStyle) num4) & (LineStyle.SlantedDashDot | LineStyle.Medium);
            ushort num5 = (ushort) objArray1[7];
            if ((num5 & 0x8000) != 0)
            {
                data1.BordersUsed |= MultipleBorders.DiagonalUp;
            }
            if ((num5 & 0x4000) != 0)
            {
                data1.BordersUsed |= MultipleBorders.DiagonalDown;
            }
            data1.Indexes.BorderColorIndex[2] = num5 & 0x3f;
            num5 = (ushort) (num5 >> 7);
            data1.Indexes.BorderColorIndex[3] = num5 & 0x3f;
            uint num6 = (uint) objArray1[8];
            data1.Indexes.BorderColorIndex[0] = ((int) num6) & 0x7f;
            num6 = num6 >> 7;
            data1.Indexes.BorderColorIndex[1] = ((int) num6) & 0x7f;
            num6 = num6 >> 7;
            data1.Indexes.BorderColorIndex[4] = ((int) num6) & 0x7f;
            num6 = num6 >> 7;
            data1.BorderStyle[4] = ((LineStyle) num6) & (LineStyle.SlantedDashDot | LineStyle.Medium);
            num6 = num6 >> 5;
            data1.PatternStyle = ((FillPatternStyle) num6) & ((FillPatternStyle) 0x3f);
            ushort num7 = (ushort) objArray1[9];
            data1.Indexes.PatternForegroundColorIndex = num7 & 0x7f;
            num7 = (ushort) (num7 >> 7);
            data1.Indexes.PatternBackgroundColorIndex = num7 & 0x7f;
            data1.BordersUsed |= MultipleBorders.Outside;
            style1.UseFlags = CellStyleData.Properties.All;
            return style1;
        }

        private void LoadColumnInfo(AbsXLSRec record, ExcelWorksheet ws)
        {
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            ushort num1 = (ushort) objArray1[0];
            ushort num2 = (ushort) objArray1[1];
            if (num2 > 0xff)
            {
                num2 = 0xff;
            }
            ushort num3 = (ushort) objArray1[2];
            ushort num4 = (ushort) objArray1[3];
            ColumnInfoOptions options1 = (ColumnInfoOptions) objArray1[4];
            for (int num5 = num1; num5 <= num2; num5++)
            {
                ExcelColumn column1 = ws.Columns[num5];
                column1.Width = num3;
                column1.Style = (CellStyle) this.cellStylesTable[num4];
                if ((options1 & ColumnInfoOptions.Collapsed) != ((ColumnInfoOptions) ((ushort) 0)))
                {
                    column1.Collapsed = true;
                }
                if ((options1 & ColumnInfoOptions.Hidden) != ((ColumnInfoOptions) ((ushort) 0)))
                {
                    column1.Hidden = true;
                }
                column1.OutlineLevel = (((ushort) options1) >> 8) & 7;
            }
        }

        private void LoadDefaultPalette()
        {
            int num1 = XLSFileReader.defaultPalette.GetLength(0);
            this.colorsTable = new Color[num1];
            for (int num2 = 0; num2 < num1; num2++)
            {
                int num3 = (int) XLSFileReader.defaultPalette[num2, 0];
                int num4 = (int) XLSFileReader.defaultPalette[num2, 1];
                int num5 = (int) XLSFileReader.defaultPalette[num2, 2];
                int num6 = (int) XLSFileReader.defaultPalette[num2, 3];
                this.colorsTable[num3] = Color.FromArgb(num4, num5, num6);
            }
        }

        private void LoadExternsheet(AbsXLSRec record)
        {
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            ushort num2 = (ushort) objArray1[0];
            object[] objArray2 = (object[]) objArray1[1];
            this.sheetIndexes = new ushort[objArray2.Length];
            for (int num1 = 0; num1 < objArray2.Length; num1++)
            {
                this.sheetIndexes[num1] = ((SheetIndexes) objArray2[num1]).SheetIndex;
            }
        }

        private ExcelFontData LoadFont(AbsXLSRec record)
        {
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            ExcelFontData data1 = new ExcelFontData();
            data1.Size = (ushort) objArray1[0];
            FontOptions options1 = (FontOptions) objArray1[1];
            if ((options1 & FontOptions.Italic) != ((FontOptions) ((ushort) 0)))
            {
                data1.Italic = true;
            }
            if ((options1 & FontOptions.Strikeout) != ((FontOptions) ((ushort) 0)))
            {
                data1.Strikeout = true;
            }
            data1.ColorIndex = (ushort) objArray1[2];
            data1.Weight = (ushort) objArray1[3];
            data1.ScriptPosition = (ScriptPosition) ((ushort) objArray1[4]);
            data1.UnderlineStyle = (UnderlineStyle) ((byte) objArray1[5]);
            data1.Name = ((ExcelShortString) objArray1[6]).Str;
            return data1;
        }

        private void LoadMergedCells(AbsXLSRec record, ExcelWorksheet ws)
        {
            object[] objArray1 = ((MergedCellsRecord) record).GetArguments();
            ushort num1 = (ushort) objArray1[0];
            object[] objArray2 = (object[]) objArray1[1];
            for (int num2 = 0; num2 < num1; num2++)
            {
                ushort num3 = (ushort) objArray2[num2 * 4];
                ushort num4 = (ushort) objArray2[(num2 * 4) + 1];
                ushort num5 = (ushort) objArray2[(num2 * 4) + 2];
                ushort num6 = (ushort) objArray2[(num2 * 4) + 3];
                ws.Cells.GetSubrangeAbsolute(num3, num5, num4, num6).Merged = true;
            }
        }

        private void LoadName(AbsXLSRec record)
        {
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            NameRecord record1 = record as NameRecord;
            record1.SheetIndex = (ushort) objArray1[2];
            record1.NameValue = ((ExcelStringWithoutLength) objArray1[3]).Str;
            record1.RpnBytes = (object[]) objArray1[4];
        }

        private void LoadNumberFormat(AbsXLSRec record)
        {
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            int num1 = (ushort) objArray1[0];
            string text1 = ((ExcelLongString) objArray1[1]).Str;
            this.numberFormats.SetNumberFormat(num1, text1);
        }

        private void LoadPalette(AbsXLSRec record)
        {
            object[] objArray1 = ((PaletteRecord) record).GetArguments();
            int num1 = (ushort) objArray1[0];
            object[] objArray2 = (object[]) objArray1[1];
            for (int num2 = 0; num2 < num1; num2++)
            {
                int num3 = num2 * 4;
                byte num4 = (byte) objArray2[num3];
                byte num5 = (byte) objArray2[num3 + 1];
                byte num6 = (byte) objArray2[num3 + 2];
                this.colorsTable[8 + num2] = Color.FromArgb(num4, num5, num6);
            }
        }

        private void LoadRow(AbsXLSRec record, ExcelWorksheet ws)
        {
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            ushort num1 = (ushort) objArray1[0];
            ushort num2 = (ushort) objArray1[3];
            RowOptions options1 = (RowOptions) objArray1[4];
            int num3 = ((ushort) objArray1[5]) & 0xfff;
            ExcelRow row1 = ws.Rows[num1];
            row1.Height = num2;
            if ((options1 & (RowOptions.Default | RowOptions.Collapsed)) != RowOptions.Default)
            {
                row1.Collapsed = true;
            }
            if ((options1 & RowOptions.GhostDirty) != RowOptions.Default)
            {
                row1.Style = (CellStyle) this.cellStylesTable[num3];
            }
            row1.OutlineLevel = ((int) options1) & 7;
        }

        private void LoadWSBool(AbsXLSRec record, ExcelWorksheet ws)
        {
            object[] objArray1 = ((XLSRecord) record).GetArguments();
            WSBoolOptions options1 = (WSBoolOptions) objArray1[0];
            ws.OutlineColumnButtonsRight = (options1 & WSBoolOptions.ColGroupRight) != ((WSBoolOptions) ((ushort) 0));
            ws.OutlineRowButtonsBelow = (options1 & WSBoolOptions.RowGroupBelow) != ((WSBoolOptions) ((ushort) 0));
        }

        private void SetCell(ExcelWorksheet ws, int rowIndex, int columnIndex, int styleIndex, object valInternal)
        {
            ExcelCell cell1 = ws.Cells[rowIndex, columnIndex];
            if (styleIndex != 0)
            {
                object obj1;
                CellStyle style1 = (CellStyle) this.cellStylesTable[styleIndex];
                cell1.Style = style1;
                if (valInternal is CellFormula)
                {
                    obj1 = ((CellFormula) valInternal).Value;
                }
                else
                {
                    obj1 = valInternal;
                }
                if ((obj1 is double) || (obj1 is int))
                {
                    int num1 = style1.Element.Indexes.NumberFormatIndex;
                    if (((num1 >= 14) && (num1 <= 0x16)) || XLSFileReader.IsDateTime(style1.Element.NumberFormat))
                    {
                        if (obj1 is int)
                        {
                            obj1 = (int) obj1;
                        }
                        obj1 = ExcelCell.ConvertExcelNumberToDateTime( System.Convert.ToDouble(obj1));
                        if (valInternal is CellFormula)
                        {
                            ((CellFormula) valInternal).Value = obj1;
                        }
                        else
                        {
                            valInternal = obj1;
                        }
                    }
                }
            }
            cell1.ValueInternal = valInternal;
        }


        // Fields
        private ArrayList cellStylesTable;
        private Color[] colorsTable;
        private static object[,] defaultPalette;
        private ExcelFile excelFile;
        private ExcelLongStrings excelStrings;
        private ArrayList fontsTable;
        private NumberFormatCollection numberFormats;
        private ushort[] sheetIndexes;
        private XlsOptions xlsOptions;
    }
}

