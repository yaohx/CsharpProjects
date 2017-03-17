namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class XLSFileWriter
    {
        // Methods
        static XLSFileWriter()
        {
            Color[] colorArray1 = new Color[] { Color.FromArgb(0, 0, 0), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0, 0), Color.FromArgb(0, 0xff, 0), Color.FromArgb(0, 0, 0xff), Color.FromArgb(0xff, 0xff, 0), Color.FromArgb(0xff, 0, 0xff), Color.FromArgb(0, 0xff, 0xff) } ;
            XLSFileWriter.defaultColors = colorArray1;
            ExcelFontData data1 = new ExcelFontData();
            data1.ColorIndex = XLSFileWriter.defaultColors.Length;
            AbsXLSRec[] recArray1 = new AbsXLSRec[] { XLSFileWriter.CreateFontRecord(data1), XLSFileWriter.CreateFontRecord(data1), XLSFileWriter.CreateFontRecord(data1), XLSFileWriter.CreateFontRecord(data1) } ;
            XLSFileWriter.defaultFontRecords = recArray1;
            recArray1 = new AbsXLSRec[] { new XLSRecord("Format", "05 00 17 00 00 22 24 22 23 2c 23 23 30 5f 29 3b 5c 28 22 24 22 23 2c 23 23 30 5c 29"), new XLSRecord("Format", "06 00 1c 00 00 22 24 22 23 2c 23 23 30 5f 29 3b 5b 52 65 64 5d 5c 28 22 24 22 23 2c 23 23 30 5c 29"), new XLSRecord("Format", "07 00 1d 00 00 22 24 22 23 2c 23 23 30 2e 30 30 5f 29 3b 5c 28 22 24 22 23 2c 23 23 30 2e 30 30 5c 29"), new XLSRecord("Format", "08 00 22 00 00 22 24 22 23 2c 23 23 30 2e 30 30 5f 29 3b 5b 52 65 64 5d 5c 28 22 24 22 23 2c 23 23 30 2e 30 30 5c 29"), new XLSRecord("Format", "2a 00 32 00 00 5f 28 22 24 22 2a 20 23 2c 23 23 30 5f 29 3b 5f 28 22 24 22 2a 20 5c 28 23 2c 23 23 30 5c 29 3b 5f 28 22 24 22 2a 20 22 2d 22 5f 29 3b 5f 28 40 5f 29"), new XLSRecord("Format", "29 00 29 00 00 5f 28 2a 20 23 2c 23 23 30 5f 29 3b 5f 28 2a 20 5c 28 23 2c 23 23 30 5c 29 3b 5f 28 2a 20 22 2d 22 5f 29 3b 5f 28 40 5f 29"), new XLSRecord("Format", "2c 00 3a 00 00 5f 28 22 24 22 2a 20 23 2c 23 23 30 2e 30 30 5f 29 3b 5f 28 22 24 22 2a 20 5c 28 23 2c 23 23 30 2e 30 30 5c 29 3b 5f 28 22 24 22 2a 20 22 2d 22 3f 3f 5f 29 3b 5f 28 40 5f 29"), new XLSRecord("Format", "2b 00 31 00 00 5f 28 2a 20 23 2c 23 23 30 2e 30 30 5f 29 3b 5f 28 2a 20 5c 28 23 2c 23 23 30 2e 30 30 5c 29 3b 5f 28 2a 20 22 2d 22 3f 3f 5f 29 3b 5f 28 40 5f 29") } ;
            XLSFileWriter.defaultNumberFormatRecords = recArray1;
            recArray1 = new AbsXLSRec[] { 
                new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 00 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "02 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "02 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 f5 ff 20 00 00 f4 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "00 00 00 00 01 00 20 00 00 00 00 00 00 00 00 00 00 00 c0 20"), 
                new XLSRecord("XF", "01 00 2b 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 29 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 2c 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 2a 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20"), new XLSRecord("XF", "01 00 09 00 f5 ff 20 00 00 f8 00 00 00 00 00 00 00 00 c0 20")
             } ;
            XLSFileWriter.defaultXFRecords = recArray1;
            recArray1 = new AbsXLSRec[] { new StyleRecord("10 80 03 ff"), new StyleRecord("11 80 06 ff"), new StyleRecord("12 80 04 ff"), new StyleRecord("13 80 07 ff"), new StyleRecord("00 80 00 ff"), new StyleRecord("14 80 05 ff") } ;
            XLSFileWriter.defaultStyleRecords = recArray1;
        }

        public XLSFileWriter(ExcelFile excelFile, string diagnosticsFileName)
        {
            this.excelFile = excelFile;
            this.diagnosticsFileName = diagnosticsFileName;
        }

        private static CellStyle Combine(CellStyle highPriority, CellStyle lowPriority)
        {
            CellStyle style1;
            if (highPriority == null)
            {
                return lowPriority;
            }
            if (lowPriority == null)
            {
                return highPriority;
            }
            if (highPriority.Element.IsInCache)
            {
                style1 = new CellStyle(highPriority, highPriority.Element.ParentCollection);
            }
            else
            {
                style1 = highPriority;
            }
            style1.CopyIfNotUsed(lowPriority);
            return style1;
        }

        private AbsXLSRec CreateCellRecord(ushort row, ushort column, ushort styleIndex, object cellValue, CellFormula formula, out ArrayList extraRecords)
        {
            string text1;
            object[] objArray1;
            CellRecordHeader header1 = new CellRecordHeader(row, column, styleIndex);
            if (formula != null)
            {
                extraRecords = formula.ExtraFormulaRecords;
                formula.Recalculate();
                objArray1 = new object[] { header1, formula.ResultBytes, formula.Options, (ushort) formula.RpnBytes.Length, formula.RpnBytes } ;
                return new FormulaRecord(objArray1);
            }
            extraRecords = null;
            if ((cellValue == null) || (cellValue is DBNull))
            {
                objArray1 = new object[] { header1 } ;
                return new XLSRecord("Blank", objArray1);
            }
            if (cellValue is Enum)
            {
                cellValue = cellValue.ToString();
            }
            switch (cellValue.GetType().FullName)
            {
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.UInt16":
                {
                    cellValue = Convert.ToInt32(cellValue, CultureInfo.InvariantCulture);
                    break;
                }
                case "System.Int64":
                {
                    long num1 = (long) cellValue;
                    if ((num1 < -536870912) || (num1 > 0x1fffffff))
                    {
                        cellValue = num1.ToString(CultureInfo.InvariantCulture);
                        goto Label_0411;
                    }
                    cellValue = Convert.ToInt32(num1, CultureInfo.InvariantCulture);
                    break;
                }
                case "System.UInt64":
                {
                    ulong num2 = (ulong) cellValue;
                    if (num2 <= 0x1fffffff)
                    {
                        cellValue = Convert.ToInt32(num2, CultureInfo.InvariantCulture);
                        break;
                    }
                    cellValue = num2.ToString(CultureInfo.InvariantCulture);
                    goto Label_0411;
                }
                case "System.UInt32":
                {
                    uint num3 = (uint) cellValue;
                    if (num3 <= 0x1fffffff)
                    {
                        cellValue = Convert.ToInt32(num3, CultureInfo.InvariantCulture);
                        break;
                    }
                    cellValue = num3.ToString(CultureInfo.InvariantCulture);
                    goto Label_0411;
                }
                case "System.Int32":
                {
                    break;
                }
                case "System.Single":
                {
                    return new RKRecord(header1, XLSFileWriter.GetRKValue((float) cellValue));
                }
                case "System.Double":
                {
                    goto Label_0384;
                }
                case "System.Boolean":
                {
                    cellValue = Convert.ToString((bool) cellValue, CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture);
                    goto Label_0411;
                }
                case "System.Char":
                case "System.Text.StringBuilder":
                {
                    cellValue = cellValue.ToString();
                    goto Label_0411;
                }
                case "System.Decimal":
                {
                    cellValue = Convert.ToDouble((decimal) cellValue);
                    goto Label_0384;
                }
                case "System.DateTime":
                {
                    cellValue = XLSFileWriter.GetEncodedDateTime((DateTime) cellValue);
                    goto Label_0384;
                }
                case "System.String":
                {
                    goto Label_0411;
                }
				case "System.Byte[]":
					cellValue = cellValue.ToString();
					goto Label_0411;
                default:
                {
                    throw new Exception("Internal error: Data type not supported as cell value.");
                }
            }
            int num4 = (int) cellValue;
            if ((num4 >= -536870912) && (num4 <= 0x1fffffff))
            {
                return new RKRecord(header1, XLSFileWriter.GetRKValue(num4));
            }
            cellValue = num4.ToString(CultureInfo.InvariantCulture);
            goto Label_0411;
        Label_0384:
            objArray1 = new object[2];
            objArray1[0] = header1;
            objArray1[1] = (double) cellValue;
            return new XLSRecord("Number", objArray1);
        Label_0411:
            text1 = (string) cellValue;
            objArray1 = new object[] { header1, this.GetSSTIndex(text1) } ;
            return new XLSRecord("LabelSST", objArray1);
        }

        private AbsXLSRec CreateCellRecordIfNeeded(ExcelWorksheet worksheet, int row, int column, int currentCellsCount, out ArrayList extraRecords)
        {
            MergedCellRange range1;
            object obj1;
            CellStyle style1;
            CellFormula formula1;
            bool flag1 = false;
            if (column < currentCellsCount)
            {
                ExcelCell cell1 = worksheet.Rows[row].AllocatedCells[column];
                range1 = (MergedCellRange) cell1.MergedRange;
                if (range1 != null)
                {
                    flag1 = true;
                    if ((row == range1.FirstRowIndex) && (column == range1.FirstColumnIndex))
                    {
                        obj1 = range1.Value;
                        formula1 = range1.FormulaInternal;
                    }
                    else
                    {
                        obj1 = null;
                        formula1 = null;
                    }
                    style1 = range1.StyleResolved(row, column);
                }
                else
                {
                    obj1 = cell1.Value;
                    if (obj1 != null)
                    {
                        flag1 = true;
                    }
                    formula1 = cell1.FormulaInternal;
                    if (formula1 != null)
                    {
                        flag1 = true;
                    }
                    if (!cell1.IsStyleDefault)
                    {
                        flag1 = true;
                        style1 = cell1.Style;
                    }
                    else
                    {
                        style1 = null;
                    }
                }
            }
            else
            {
                if (!worksheet.Rows[row].IsStyleDefault && !worksheet.Columns[column].IsStyleDefault)
                {
                    flag1 = true;
                }
                range1 = null;
                obj1 = null;
                style1 = null;
                formula1 = null;
            }
            if (!flag1)
            {
                extraRecords = null;
                return null;
            }
            if (range1 == null)
            {
                CellStyle style2;
                CellStyle style3;
                CellStyle style4;
                if (!worksheet.Rows[row].IsStyleDefault)
                {
                    style2 = worksheet.Rows[row].Style;
                }
                else
                {
                    style2 = null;
                }
                if (!worksheet.Columns[column].IsStyleDefault)
                {
                    style3 = worksheet.Columns[column].Style;
                }
                else
                {
                    style3 = null;
                }
                if (this.excelFile.RowColumnResolutionMethod == RowColumnResolutionMethod.RowOverColumn)
                {
                    style4 = XLSFileWriter.Combine(style2, style3);
                }
                else
                {
                    style4 = XLSFileWriter.Combine(style3, style2);
                }
                style1 = XLSFileWriter.Combine(style1, style4);
            }
            if ((obj1 is DateTime) && (((style1 == null) || (style1.NumberFormat == null)) || (style1.NumberFormat.Length == 0)))
            {
                if (style1 == null)
                {
                    style1 = new CellStyle(this.excelFile.CellStyleCache);
                }
                else if (style1.Element.IsInCache)
                {
                    style1 = new CellStyle(style1, style1.Element.ParentCollection);
                }
                style1.NumberFormat = "yyyy-m-d hh:mm";
            }
            if ((style1 != null) && !style1.Element.IsInCache)
            {
                this.excelFile.CellStyleCache.EmptyAddQueue();
            }
            int num1 = this.GetCellStyleIndex(style1);
            return this.CreateCellRecord((ushort) row, (ushort) column, (ushort) num1, obj1, formula1, out extraRecords);
        }

        private void CreateCellStyleIndexes(CellStyleData element)
        {
            CellStyleDataIndexes indexes1 = new CellStyleDataIndexes();
            indexes1.CellStyleIndex = this.usedCellStyleData.Add(element) + XLSFileWriter.defaultXFRecords.Length;
            indexes1.FontIndex = this.fonts.Add(element.FontData);
            ExcelFontData data1 = (ExcelFontData) this.fonts[indexes1.FontIndex];
            if (data1.ColorIndex == -1)
            {
                data1.ColorIndex = this.colors.AddColor(data1.Color);
            }
            indexes1.FontIndex += 5;
            indexes1.PatternBackgroundColorIndex = this.colors.AddColor(element.PatternBackgroundColor);
            indexes1.PatternForegroundColorIndex = this.colors.AddColor(element.PatternForegroundColor);
            for (int num1 = 0; num1 < 5; num1++)
            {
                indexes1.BorderColorIndex[num1] = this.colors.AddColor(element.BorderColor[num1]);
            }
            if (element.NumberFormat.Length == 0)
            {
                indexes1.NumberFormatIndex = 0;
            }
            else
            {
                indexes1.NumberFormatIndex = this.numberFormats.Add(element.NumberFormat);
            }
            element.Indexes = indexes1;
        }

        private static AbsXLSRec CreateFontRecord(ExcelFontData fontData)
        {
            FontOptions options1 = (FontOptions) ((ushort) 0);
            if (fontData.Italic)
            {
                options1 |= FontOptions.Italic;
            }
            if (fontData.Strikeout)
            {
                options1 |= FontOptions.Strikeout;
            }
            object[] objArray1 = new object[] { (ushort) fontData.Size, options1, (ushort) fontData.ColorIndex, (ushort) fontData.Weight, (ushort) fontData.ScriptPosition, (byte) fontData.UnderlineStyle, new ExcelShortString(fontData.Name) } ;
            return new XLSRecord("Font", objArray1);
        }

        private XLSRecord CreatePaletteRecord()
        {
            object[] objArray1 = new object[this.colors.Count * 4];
            for (int num1 = 0; num1 < this.colors.Count; num1++)
            {
                int num2 = num1 * 4;
                Color color1 = (Color) this.colors[num1];
                objArray1[num2] = color1.R;
                color1 = (Color) this.colors[num1];
                objArray1[num2 + 1] = color1.G;
                color1 = (Color) this.colors[num1];
                objArray1[num2 + 2] = color1.B;
                objArray1[num2 + 3] = (byte) 0;
            }
            object[] objArray2 = new object[] { (ushort) this.colors.Count, objArray1 } ;
            return new PaletteRecord(objArray2);
        }

        private AbsXLSRec CreateRowRecordIfNeeded(ExcelWorksheet worksheet, int row, int currentFirstColumn, int currentLastColumn)
        {
            ushort num1;
            ExcelRow row1 = worksheet.Rows[row];
            if (((currentLastColumn == -1) && (row1.Height == 0xff)) && (((row1.OutlineLevel == 0) && !row1.Collapsed) && row1.IsStyleDefault))
            {
                return null;
            }
            RowOptions options1 = (RowOptions)0x100;
            if (row1.Height != 0xff)
            {
                options1 |= RowOptions.Unsynced;
            }
            if (row1.Collapsed)
            {
                options1 |= (RowOptions.Default | RowOptions.Collapsed);
            }
            if (row1.Collapsed || (row1.Height == 0))
            {
                options1 |= RowOptions.ZeroHeight;
            }
            if (!row1.IsStyleDefault)
            {
                options1 |= RowOptions.GhostDirty;
            }
            options1 |= ((RowOptions) ((ushort) row1.OutlineLevel));
            if (row1.IsStyleDefault)
            {
                num1 = (ushort) this.GetCellStyleIndex(null);
            }
            else
            {
                num1 = (ushort) this.GetCellStyleIndex(row1.Style);
            }
            object[] objArray1 = new object[] { (ushort) row, (ushort) currentFirstColumn, (ushort) (currentLastColumn + 1), (ushort) row1.Height, options1, num1 } ;
            return new XLSRecord("Row", objArray1);
        }

        private AbsXLSRec CreateSSTOrContinueRecord(ref AbsXLSRec sstRecord, ExcelLongStrings excelStrings)
        {
            if (sstRecord == null)
            {
                uint num1 = (uint) this.strings.Count;
                object[] objArray1 = new object[] { num1, num1, excelStrings } ;
                sstRecord = new SSTRecord(objArray1);
                return sstRecord;
            }
            return new ContinueRecord(excelStrings);
        }

        private ExcelWorksheet CreateWarningWorksheet(int maxRowCount, int worksheetCount, bool limitReached)
        {
            string text1;
            ExcelWorksheet worksheet1 = this.excelFile.Worksheets.Add("ELWarnings");
            if (limitReached)
            {
                text1 = "reached";
            }
            else
            {
                text1 = "is near";
            }
            worksheet1.Cells[0, 0].Value = "Size of this workbook " + text1 + " ExcelLite Free limit.";
            worksheet1.Cells[0, 0].Style.Font.Weight = 700;
            worksheet1.Cells[2, 0].Value = "ExcelLite Free is limited to 150 rows per sheet and 5 sheets per workbook.";
            object[] objArray1 = new object[] { "This workbook has maximum of ", maxRowCount, " rows per sheet and ", worksheetCount, " sheets." } ;
            worksheet1.Cells[4, 0].Value = string.Concat(objArray1);
            worksheet1.Cells[6, 0].Value = "Upgrade to full version (ExcelLite Professional) to avoid losing extra rows or sheets. See http://www.GemBoxSoftware.com for more information.";
            worksheet1.Cells[8, 0].Value = "To disable displaying of this warning worksheet (or add your own custom handlers) follow the steps below:";
            worksheet1.Cells[9, 0].Value = "1) Hook to ExcelFile.LimitNear and ExcelFile.LimitReached events.";
            worksheet1.Cells[10, 0].Value = "2) In event handlers set e.WriteWarningWorksheet property to false.";
            return worksheet1;
        }

        private static AbsXLSRec CreateXFRecord(CellStyleData styleData)
        {
            CellStyleDataIndexes indexes1 = styleData.Indexes;
            XFOptions1 options1 = (XFOptions1) ((ushort) 0);
            if (styleData.Locked)
            {
                options1 |= XFOptions1.CellLocked;
            }
            if (styleData.FormulaHidden)
            {
                options1 |= XFOptions1.FormulaHidden;
            }
            byte num1 = (byte) (((byte) styleData.VerticalAlignment) << 4);
            if (styleData.WrapText)
            {
                num1 = (byte) (num1 | 8);
            }
            num1 = (byte) (num1 | ((byte) styleData.HorizontalAlignment));
            int num2 = styleData.Rotation % 0x100;
            if (num2 < 0)
            {
                num2 += 0x100;
            }
            XFOptions2 options2 = (XFOptions2) ((ushort) styleData.Indent);
            if (styleData.ShrinkToFit)
            {
                options2 |= XFOptions2.ShrinkToFit;
            }
            options2 |= XFOptions2.UsedAttributes;
            ushort num3 = (ushort) styleData.BorderStyle[1];
            num3 = (ushort) (num3 << 4);
            num3 = (ushort) (num3 | ((ushort) styleData.BorderStyle[0]));
            num3 = (ushort) (num3 << 4);
            num3 = (ushort) (num3 | ((ushort) styleData.BorderStyle[3]));
            num3 = (ushort) (num3 << 4);
            num3 = (ushort) (num3 | ((ushort) styleData.BorderStyle[2]));
            ushort num4 = (ushort) indexes1.BorderColorIndex[3];
            num4 = (ushort) (num4 << 7);
            num4 = (ushort) (num4 | ((ushort) indexes1.BorderColorIndex[2]));
            if ((styleData.BordersUsed & MultipleBorders.DiagonalDown) != MultipleBorders.None)
            {
                num4 = (ushort) (num4 | 0x4000);
            }
            if ((styleData.BordersUsed & MultipleBorders.DiagonalUp) != MultipleBorders.None)
            {
                num4 = (ushort) (num4 | 0x8000);
            }
            uint num5 = (uint) (((int) styleData.PatternStyle) << 5);
            num5 = (uint) (((LineStyle) num5) | styleData.BorderStyle[4]);
            num5 = num5 << 7;
            num5 |= ((uint) indexes1.BorderColorIndex[4]);
            num5 = num5 << 7;
            num5 |= ((uint) indexes1.BorderColorIndex[1]);
            num5 = num5 << 7;
            num5 |= ((uint) indexes1.BorderColorIndex[0]);
            ushort num6 = (ushort) indexes1.PatternBackgroundColorIndex;
            num6 = (ushort) (num6 << 7);
            num6 = (ushort) (num6 + ((ushort) indexes1.PatternForegroundColorIndex));
            object[] objArray1 = new object[] { (ushort) indexes1.FontIndex, (ushort) indexes1.NumberFormatIndex, options1, num1, (byte) num2, (ushort) options2, num3, num4, num5, num6 } ;
            return new XLSRecord("XF", objArray1);
        }

        private int GetCellStyleIndex(CellStyle cellStyle)
        {
            if (cellStyle == null)
            {
                return 0;
            }
            CellStyleData data1 = cellStyle.Element;
            if (data1.Indexes == null)
            {
                this.CreateCellStyleIndexes(data1);
            }
            return data1.Indexes.CellStyleIndex;
        }

        private static double GetEncodedDateTime(DateTime dateTime)
        {
            TimeSpan span1 = (TimeSpan) (dateTime - new DateTime(0x76c, 1, 1));
            double num1 = 2 + span1.Days;
            return (num1 + ((((dateTime.Hour * 3600) + (dateTime.Minute * 60)) + dateTime.Second) / 86400));
        }

        private static NameRecord[] GetNameRecords(ExcelFile excelFile)
        {
            ArrayList list1 = new ArrayList();
            for (int num1 = 0; num1 < excelFile.Worksheets.Count; num1++)
            {
                ExcelWorksheet worksheet1 = excelFile.Worksheets[num1];
                for (int num2 = 0; num2 < worksheet1.NamedRanges.Count; num2++)
                {
                    NamedRange range1 = worksheet1.NamedRanges[num2];
                    NameRecord record1 = new NameRecord(worksheet1);
                    record1.SheetIndex = worksheet1.Parent.GetSheetIndex(worksheet1.Name);
                    record1.NameValue = range1.Name;
                    record1.Worksheets = worksheet1.Parent;
                    record1.Range = range1.Range;
                    record1.RpnBytes = Utilities.ConvertBytesToObjectArray(NameRecord.ConvertNameRecordRangeToRpnBytes(range1.Range, worksheet1.Name, excelFile.Worksheets));
                    list1.Add(record1);
                }
            }
            return (NameRecord[]) list1.ToArray(typeof(NameRecord));
        }

        public AbsXLSRecords GetRecords()
        {
            this.Initialize();
            this.excelFile.CellStyleCache.EmptyAddQueue();
            this.WriteRecords();
            this.records.SetRecordAddresses();
            return this.records;
        }

        private static uint GetRKValue(int val)
        {
            uint num1 = (uint) val;
            num1 = num1 << 2;
            return (num1 | 2);
        }

        private static uint GetRKValue(float val)
        {
            uint num1;
            using (MemoryStream stream1 = new MemoryStream())
            {
                using (BinaryWriter writer1 = new BinaryWriter(stream1))
                {
                    writer1.Write((double) val);
                    stream1.Position = 0;
                    using (BinaryReader reader1 = new BinaryReader(stream1))
                    {
                        reader1.ReadUInt32();
                        num1 = reader1.ReadUInt32();
                    }
                }
            }
            return (num1 & 0xfffffffc);
        }

        private uint GetSSTIndex(string str)
        {
            return (uint) this.strings.Add(new ExcelLongString(str));
        }

        public static byte[] GetStream(AbsXLSRecords records)
        {
            MemoryStream stream1;
            using (MemoryStream stream2 = (stream1 = new MemoryStream()))
            {
                using (BinaryWriter writer1 = new BinaryWriter(stream1, new UnicodeEncoding()))
                {
                    records.Write(writer1);
                    stream1.Capacity = (int) stream1.Length;
                }
            }
            return stream1.GetBuffer();
        }

        private void Initialize()
        {
            this.records = new AbsXLSRecords();
            this.usedCellStyleData = new ArrayList();
            this.colors = new ColorCollection();
            Color[] colorArray1 = XLSFileWriter.defaultColors;
            for (int num1 = 0; num1 < colorArray1.Length; num1++)
            {
                Color color1 = colorArray1[num1];
                this.colors.AddInternal(color1);
            }
            this.numberFormats = new NumberFormatCollection(false);
            this.fonts = new IndexedHashCollection();
            this.strings = new IndexedHashCollection();
            this.strings.Add(new ExcelLongString(this.excelFile.IDText));
        }

        public static object RKValueToObj(uint rkVal)
        {
            double num1;
            if ((rkVal & 2) != 0)
            {
                num1 = rkVal >> 2;
            }
            else
            {
                using (MemoryStream stream1 = new MemoryStream())
                {
                    using (BinaryWriter writer1 = new BinaryWriter(stream1))
                    {
                        writer1.Write((uint) 0);
                        writer1.Write((uint) (rkVal & 0xfffffffc));
                        stream1.Position = 0;
                        using (BinaryReader reader1 = new BinaryReader(stream1))
                        {
                            num1 = reader1.ReadDouble();
                        }
                    }
                }
            }
            if ((rkVal & 1) != 0)
            {
                num1 /= 100;
            }
            double num2 = num1 - Math.Floor(num1);
            if (((num2 <= 0) && (num1 <= 2147483647)) && (num1 >= -2147483648))
            {
                return (int) num1;
            }
            return num1;
        }

        private static void WriteCellsAndDBCellRecord(AbsXLSRecords records, ArrayList cellRecords, ref DBCellRecord dbCell, IndexRecord indexRecord)
        {
            foreach (AbsXLSRec rec1 in cellRecords)
            {
                records.Add(rec1);
            }
            cellRecords.Clear();
            records.Add(dbCell);
            indexRecord.DBCells.Add(dbCell);
            dbCell = new DBCellRecord();
        }

        private void WriteColumnInfoIfNeeded(AbsXLSRecords records, ExcelWorksheet worksheet, int columnIndex)
        {
            ExcelColumn column1 = worksheet.Columns[columnIndex];
            if (((column1.Width != worksheet.DefaultColumnWidth) || column1.Collapsed) || (((column1.OutlineLevel != 0) || !column1.IsStyleDefault) || column1.Hidden))
            {
                ushort num1;
                ColumnInfoOptions options1 = (ColumnInfoOptions) ((ushort) 0);
                if (column1.Hidden)
                {
                    options1 |= ColumnInfoOptions.Hidden;
                }
                if (column1.Collapsed)
                {
                    options1 |= ColumnInfoOptions.Collapsed;
                    if (worksheet.ShowOutlineSymbols && (worksheet.Columns.MaxOutlineLevel > 0))
                    {
                        options1 |= ColumnInfoOptions.Hidden;
                    }
                }
                options1 |= ((ColumnInfoOptions) ((ushort) (column1.OutlineLevel << 8)));
                if (column1.IsStyleDefault)
                {
                    num1 = (ushort) this.GetCellStyleIndex(null);
                }
                else
                {
                    num1 = (ushort) this.GetCellStyleIndex(column1.Style);
                }
                object[] objArray1 = new object[] { (ushort) columnIndex, (ushort) columnIndex, (ushort) column1.Width, num1, options1 } ;
                records.Add(new XLSRecord("ColumnInfo", objArray1));
            }
        }

        private static void WriteExternsheetSupBookRecords(AbsXLSRecords records, ExcelFile excelFile)
        {
            SupBookRecord record1 = new SupBookRecord();
            record1.SheetsCount = (ushort) excelFile.Worksheets.Count;
            records.Add(record1);
            ExternsheetRecord record2 = new ExternsheetRecord();
            record2.SheetIndexes = excelFile.Worksheets.SheetIndexes;
            records.Add(record2);
        }

        ///<summary>
        ///Writes the global records.
        ///</summary>
        ///<param name="records">The records.</param>
        ///<param name="worksheetRecords">The worksheet records.</param>
        ///<param name="worksheetNames">The worksheet names.</param>
        private void WriteGlobalRecords(AbsXLSRecords records, ArrayList worksheetRecords, ArrayList worksheetNames)
        {
            PreservedRecords records1 = this.excelFile.PreservedGlobalRecords;
            object[] objArray1 = new object[] { BOFSubstreamType.WorkbookGlobals } ;
            records.Add(new XLSRecord("BOF", objArray1));
            if (records1 != null)
            {
                records1.WriteRecords(records, "WRITEPROT");
                records1.WriteRecords(records, "WRITEACCESS");
                records1.WriteRecords(records, "FILESHARING");
                records1.WriteRecords(records, "CODEPAGE");
            }
            if (records1 != null)
            {
                records1.WriteRecords(records, "WINDOWPROTECT");
            }
            objArray1 = new object[] { Utilities.BoolToUshort(this.excelFile.Protected) } ;
            records.Add(new XLSRecord("Protect", objArray1));
            if (records1 != null)
            {
                records1.WriteRecords(records, "OBJECTPROTECT");
            }
            objArray1 = new object[] { (ushort) 120, (ushort) 120, (ushort) 0x3b1f, (ushort) 0x2454, Window1Options.DisplayHScroll | (Window1Options.DisplayVScroll | Window1Options.ShowTabs), (ushort) this.excelFile.Worksheets.GetActiveWorksheetIndex(), (ushort) 0, (ushort) 1, (ushort) 600 } ;
            records.Add(new XLSRecord("Window1", objArray1));
            if (records1 != null)
            {
                records1.WriteRecords(records, "HIDEOBJ");
                records1.WriteRecords(records, "DATEMODE");
                records1.WriteRecords(records, "PRECISION");
                records1.WriteRecords(records, "REFRESHALL");
                records1.WriteRecords(records, "BOOKBOOL");
            }
            AbsXLSRec[] recArray1 = XLSFileWriter.defaultFontRecords;
            int num5 = 0;
            while (num5 < recArray1.Length)
            {
                AbsXLSRec rec1 = recArray1[num5];
                records.Add(rec1);
                num5++;
            }
            foreach (ExcelFontData data1 in this.fonts)
            {
                records.Add(XLSFileWriter.CreateFontRecord(data1));
                data1.ColorIndex = -1;
            }
            recArray1 = XLSFileWriter.defaultNumberFormatRecords;
            num5 = 0;
            while (num5 < recArray1.Length)
            {
                AbsXLSRec rec2 = recArray1[num5];
                records.Add(rec2);
                num5++;
            }
            for (int num1 = 0xa4; num1 < this.numberFormats.Count; num1++)
            {
                string text1 = (string) this.numberFormats[num1];
                objArray1 = new object[] { (ushort) num1, new ExcelLongString(text1) } ;
                records.Add(new XLSRecord("Format", objArray1));
            }
            recArray1 = XLSFileWriter.defaultXFRecords;
            num5 = 0;
            while (num5 < recArray1.Length)
            {
                AbsXLSRec rec3 = recArray1[num5];
                records.Add(rec3);
                num5++;
            }
            if (this.usedCellStyleData.Count > 0xf8b)
            {
                objArray1 = new object[] { "Maximum number of cell styles in Excel file is ", 0xf8b, " and your file uses ", this.usedCellStyleData.Count, " different cell styles." } ;
                throw new Exception(string.Concat(objArray1));
            }
            foreach (CellStyleData data2 in this.usedCellStyleData)
            {
                records.Add(XLSFileWriter.CreateXFRecord(data2));
                data2.Indexes = null;
            }
            recArray1 = XLSFileWriter.defaultStyleRecords;
            for (num5 = 0; num5 < recArray1.Length; num5++)
            {
                AbsXLSRec rec4 = recArray1[num5];
                records.Add(rec4);
            }
            records.Add(this.CreatePaletteRecord());
            if (records1 != null)
            {
                records1.WriteRecords(records, "USESELFS");
            }
            for (int num2 = 0; num2 < worksheetRecords.Count; num2++)
            {
                string text2 = (string) worksheetNames[num2];
                records.Add(new BoundSheetRecord(new ExcelShortString(text2), ((AbsXLSRecords) worksheetRecords[num2])[0]));
            }
            if (records1 != null)
            {
                records1.WriteRecords(records, "COUNTRY");
                records1.WriteRecords(records, "MSODRAWINGGROUP");
            }
            NameRecord[] recordArray1 = XLSFileWriter.GetNameRecords(this.excelFile);
            XLSFileWriter.WriteExternsheetSupBookRecords(records, this.excelFile);
            if (recordArray1.Length > 0)
            {
                for (int num3 = 0; num3 < recordArray1.Length; num3++)
                {
                    records.Add(recordArray1[num3]);
                }
            }
            AbsXLSRec rec5 = null;
            ExcelLongStrings strings1 = new ExcelLongStrings();
            int num4 = 8;
            foreach (ExcelLongString text3 in this.strings)
            {
                if ((num4 + text3.Size) > 0x2020)
                {
                    records.Add(this.CreateSSTOrContinueRecord(ref rec5, strings1));
                    strings1 = new ExcelLongStrings();
                    num4 = 0;
                }
                strings1.Strings.Add(text3);
                num4 += text3.Size;
            }
            if (num4 > 0)
            {
                records.Add(this.CreateSSTOrContinueRecord(ref rec5, strings1));
            }
            records.Add(new ExtSSTRecord(8, 12, rec5));
            records.Add(new XLSRecord("EOF"));
        }

        private void WriteRecords()
        {
            int num2 = this.excelFile.Worksheets.Count;
            int num3 = 0;
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            for (int num1 = 0; num1 < num2; num1++)
            {
                ExcelWorksheet worksheet1 = this.excelFile.Worksheets[num1];
                AbsXLSRecords records1 = new AbsXLSRecords();
                this.WriteWorksheetRecords(records1, worksheet1, ref num3);
                list1.Add(records1);
                list2.Add(worksheet1.Name);
            }
            LimitEventArgs args1 = null;
            ExcelWorksheet worksheet2 = null;
            if (worksheet2 != null)
            {
                AbsXLSRecords records2 = new AbsXLSRecords();
                this.WriteWorksheetRecords(records2, worksheet2, ref num3);
                list1.Add(records2);
                list2.Add(worksheet2.Name);
            }
            this.WriteGlobalRecords(this.records, list1, list2);
            foreach (AbsXLSRecords records3 in list1)
            {
                foreach (AbsXLSRec rec1 in records3)
                {
                    this.records.Add(rec1);
                }
            }
            if (worksheet2 != null)
            {
                worksheet2.Delete();
            }
        }

        private void WriteWorksheetRecords(AbsXLSRecords records, ExcelWorksheet worksheet, ref int maxRowCount)
        {
            ushort num17;
            PreservedRecords records1 = worksheet.PreservedWorksheetRecords;
            object[] objArray1 = new object[] { BOFSubstreamType.WorksheetDialogSheet } ;
            records.Add(new XLSRecord("BOF", objArray1));
            IndexRecord record1 = new IndexRecord();
            records.Add(record1);
            ushort num1 = (ushort) (worksheet.Rows.MaxOutlineLevel + 1);
            ushort num2 = (ushort) (worksheet.Columns.MaxOutlineLevel + 1);
            if (records1 != null)
            {
                records1.WriteRecords(records, "CALCCOUNT");
                records1.WriteRecords(records, "CALCMODE");
                records1.WriteRecords(records, "REFMODE");
                records1.WriteRecords(records, "DELTA");
                records1.WriteRecords(records, "ITERATION");
                records1.WriteRecords(records, "SAVERECALC");
                records1.WriteRecords(records, "PRINTHEADERS");
                records1.WriteRecords(records, "PRINTGRIDLINES");
                records1.WriteRecords(records, "GRIDSET");
            }
            objArray1 = new object[] { (ushort) 0x1d, (ushort) 0x1d, num1, num2 } ;
            records.Add(new XLSRecord("Guts", objArray1));
            objArray1 = new object[] { DefaultRowHeightOptions.Unsynced, (ushort) 0xff } ;
            records.Add(new XLSRecord("DefaultRowHeight", objArray1));
            WSBoolOptions options1 = WSBoolOptions.DspGuts | WSBoolOptions.ShowAutoBreaks;
            if (worksheet.OutlineColumnButtonsRight)
            {
                options1 |= WSBoolOptions.ColGroupRight;
            }
            if (worksheet.OutlineRowButtonsBelow)
            {
                options1 |= WSBoolOptions.RowGroupBelow;
            }
            objArray1 = new object[] { options1 } ;
            records.Add(new XLSRecord("WSBool", objArray1));
            records.Add(new HorizontalPageBreaksRecord(worksheet.HorizontalPageBreaks.GetArgs()));
            records.Add(new VerticalPageBreaksRecord(worksheet.VerticalPageBreaks.GetArgs()));
            if (records1 != null)
            {
                records1.WriteRecords(records, "HEADER");
                records1.WriteRecords(records, "FOOTER");
                records1.WriteRecords(records, "HCENTER");
                records1.WriteRecords(records, "VCENTER");
                records1.WriteRecords(records, "LEFTMARGIN");
                records1.WriteRecords(records, "RIGHTMARGIN");
                records1.WriteRecords(records, "TOPMARGIN");
                records1.WriteRecords(records, "BOTTOMMARGIN");
            }
            objArray1 = new object[] { worksheet.paperSize, (ushort) worksheet.scalingFactor, worksheet.startPageNumber, worksheet.fitWorksheetWidthToPages, worksheet.fitWorksheetHeightToPages, worksheet.setupOptions, worksheet.printResolution, worksheet.verticalPrintResolution, worksheet.headerMargin, worksheet.footerMargin, worksheet.numberOfCopies } ;
            records.Add(new XLSRecord("SETUP", objArray1));
            objArray1 = new object[] { worksheet.Protected ? ((ushort) 1) : ((ushort) 0) } ;
            records.Add(new XLSRecord("Protect", objArray1));
            if (records1 != null)
            {
                records1.WriteRecords(records, "WINDOWPROTECT");
                records1.WriteRecords(records, "OBJECTPROTECT");
                records1.WriteRecords(records, "SCENPROTECT");
                records1.WriteRecords(records, "PASSWORD");
            }
            objArray1 = new object[] { (ushort) (worksheet.DefaultColumnWidth / 0x100) } ;
            records.Add(new XLSRecord("DefaultColumnWidth", objArray1));
            int num4 = worksheet.Columns.Count;
            int num3 = 0;
            while (num3 < num4)
            {
                this.WriteColumnInfoIfNeeded(records, worksheet, num3);
                num3++;
            }
            if (records1 != null)
            {
                records1.WriteRecords(records, "SORT");
            }
            XLSRecord record2 = new XLSRecord("Dimensions", (object[])null);
            records.Add(record2);
            num3 = num4 - 1;
            while ((num3 >= 0) && worksheet.Columns[num3].IsStyleDefault)
            {
                num3--;
            }
            int num5 = num3;
            int num6 = -1;
            int num7 = -1;
            int num8 = 0x7fffffff;
            int num9 = -2147483648;
            DBCellRecord record3 = new DBCellRecord();
            ArrayList list1 = new ArrayList();
            int num10 = worksheet.Rows.Count;
            if (num10 > maxRowCount)
            {
                maxRowCount = num10;
            }
            //num10 = Math.Min(num10, (int) (this.excelFile.HashFactorA - this.excelFile.HashFactorB));
            for (int num11 = 0; num11 < num10; num11++)
            {
                if ((num6 != -1) && (((num11 - num6) % 0x20) == 0))
                {
                    XLSFileWriter.WriteCellsAndDBCellRecord(records, list1, ref record3, record1);
                }
                int num12 = 0;
                int num13 = -1;
                int num14 = worksheet.Rows[num11].AllocatedCells.Count;
                AbsXLSRec rec1 = null;
                for (num3 = 0; (num3 < num14) || (num3 <= num5); num3++)
                {
                    ArrayList list2;
                    AbsXLSRec rec2 = this.CreateCellRecordIfNeeded(worksheet, num11, num3, num14, out list2);
                    if (rec2 != null)
                    {
                        list1.Add(rec2);
                        if (list2 != null)
                        {
                            foreach (XLSRecord record4 in list2)
                            {
                                list1.Add(record4);
                            }
                        }
                        if (num13 == -1)
                        {
                            num12 = num3;
                            rec1 = rec2;
                        }
                        num13 = num3;
                    }
                }
                AbsXLSRec rec3 = this.CreateRowRecordIfNeeded(worksheet, num11, num12, num13);
                if (rec3 != null)
                {
                    records.Add(rec3);
                    if (record3.FirstRow == null)
                    {
                        record3.FirstRow = rec3;
                    }
                    record3.StartingCellsForRows.Add(rec1);
                    if (num6 == -1)
                    {
                        num6 = num11;
                    }
                    num7 = num11;
                }
                if (num13 != -1)
                {
                    if (num12 < num8)
                    {
                        num8 = num12;
                    }
                    if (num13 > num9)
                    {
                        num9 = num13;
                    }
                }
            }
            if ((record3.FirstRow != null) || (record3.StartingCellsForRows.Count > 0))
            {
                XLSFileWriter.WriteCellsAndDBCellRecord(records, list1, ref record3, record1);
            }
            if (num6 == -1)
            {
                num6 = num7 = num8 = num9 = 0;
            }
            record1.FirstRow = num6;
            record1.LastRowPlusOne = num7 + 1;
            if (num8 == 0x7fffffff)
            {
                objArray1 = new object[] { (uint) num6, (uint) (num7 + 1), (ushort) 0, (ushort) 0 } ;
                record2.InitializeDelayed(objArray1);
            }
            else
            {
                objArray1 = new object[] { (uint) num6, (uint) (num7 + 1), (ushort) num8, (ushort) (num9 + 1) } ;
                record2.InitializeDelayed(objArray1);
            }
            if (records1 != null)
            {
                records1.WriteRecords(records, -10);
            }
            WorksheetWindowOptions options2 = worksheet.windowOptions;
            if (worksheet.Parent.ActiveWorksheet == worksheet)
            {
                options2 |= (WorksheetWindowOptions.SheetVisible | WorksheetWindowOptions.SheetSelected);
            }
            ushort num15 = (ushort) worksheet.PageBreakViewZoom;
            if (num15 == 60)
            {
                num15 = 0;
            }
            ushort num16 = (ushort) worksheet.Zoom;
            if (num16 == 60)
            {
                num16 = 0;
            }
            objArray1 = new object[] { options2, (ushort) worksheet.FirstVisibleRow, (ushort) worksheet.FirstVisibleColumn, (ushort) 0x40, num15, num16 } ;
            records.Add(new XLSRecord("Window2", objArray1));
            if (worksheet.ShowInPageBreakPreview)
            {
                num17 = (ushort) worksheet.PageBreakViewZoom;
            }
            else
            {
                num17 = (ushort) worksheet.Zoom;
            }
            objArray1 = new object[] { num17, (ushort) 100 } ;
            records.Add(new XLSRecord("SCL", objArray1));
            if (records1 != null)
            {
                records1.WriteRecords(records, "PANE");
                records1.WriteRecords(records, "SELECTION");
                records1.WriteRecords(records, "STANDARDWIDTH");
            }
            ArrayList list3 = new ArrayList();
            foreach (MergedCellRange range1 in worksheet.MergedRanges.Values)
            {
                objArray1 = new object[] { (ushort) range1.FirstRowIndex, (ushort) range1.LastRowIndex, (ushort) range1.FirstColumnIndex, (ushort) range1.LastColumnIndex } ;
                list3.AddRange(objArray1);
                if (list3.Count == 0x1000)
                {
                    objArray1 = new object[] { (ushort) 0x400, (object[]) list3.ToArray(typeof(object)) } ;
                    records.Add(new MergedCellsRecord(objArray1));
                    list3.Clear();
                }
            }
            if (list3.Count > 0)
            {
                objArray1 = new object[] { (ushort) (list3.Count / 4), (object[]) list3.ToArray(typeof(object)) } ;
                records.Add(new MergedCellsRecord(objArray1));
            }
            if (records1 != null)
            {
                records1.WriteRecords(records, "LABELRANGES");
                records1.WriteRecords(records, -11);
                records1.WriteRecords(records, "HLINK");
                records1.WriteRecords(records, "QUICKTIP");
                records1.WriteRecords(records, "DVAL");
                records1.WriteRecords(records, "DV");
                records1.WriteRecords(records, "SHEETLAYOUT");
                records1.WriteRecords(records, "SHEETPROTECTION");
                records1.WriteRecords(records, "RANGEPROTECTION");
            }
            records.Add(new XLSRecord("EOF"));
        }


        // Fields
        private ColorCollection colors;
        private static readonly Color[] defaultColors;
        private static readonly AbsXLSRec[] defaultFontRecords;
        private static readonly AbsXLSRec[] defaultNumberFormatRecords;
        private static readonly AbsXLSRec[] defaultStyleRecords;
        private static readonly AbsXLSRec[] defaultXFRecords;
        private string diagnosticsFileName;
        private ExcelFile excelFile;
        private IndexedHashCollection fonts;
        private const int MaxRKInt = 0x1fffffff;
        private const int MinRKInt = -536870912;
        private NumberFormatCollection numberFormats;
        private AbsXLSRecords records;
        private const int startFontIndex = 5;
        public const int StartFormatIndex = 0xa4;
        private IndexedHashCollection strings;
        private ArrayList usedCellStyleData;

        // Nested Types
        private class ColorCollection : IndexedHashCollection
        {
            // Methods
            public ColorCollection()
            {
            }

            public override int Add(object item)
            {
                throw new Exception("Internal error: use AddColor() instead.");
            }

            public int AddColor(Color color)
            {
                if (color.ToArgb() == Color.Black.ToArgb())
                {
                    return XLSFileWriter.defaultColors.Length;
                }
                int num1 = base.Add(color);
                if (num1 > 0x38)
                {
                    throw new Exception("Maximum number of colors in Excel file is: " + 0x38);
                }
                return (num1 + XLSFileWriter.defaultColors.Length);
            }

        }
    }
}

