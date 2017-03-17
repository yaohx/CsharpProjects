namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;

    ///<summary>
    ///Contains settings specifying how the cell data will be displayed.
    ///</summary>
    ///<remarks>
    ///Various settings control various display aspects: alignment, patterns and shading, indentation,
    ///rotation, cell protection, text wrapping, number format, font related settings and cell borders. You can
    ///set cell style of a specific Excel through its <b>Style</b> property, or you can create new cell style
    ///with desired properties and apply it to unlimited number of Excel objects. Note, however, that number of
    ///distinct cell styles in Excel file can't exceed <see cref="MB.WinEIDrive.Excel.ExcelFile.MaxCellStyles">
    ///ExcelFile.MaxCellStyles</see>. You don't have to worry about creating duplicate cell styles; internal
    ///caching engine will eliminate duplicates in appropriate moments.
    ///</remarks>
    ///<example> Following code demonstrates various cell style properties:
    ///<code lang="Visual Basic">
    ///Sub StylesSample(ByVal ws As ExcelWorksheet)
    ///ws.Cells(0, 0).Value = "Cell style examples:"
    ///
    ///Dim row As Integer = 0
    ///
    ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
    ///ws.Columns(0).Width = 4 * 256
    ///ws.Columns(1).Width = 30 * 256
    ///ws.Columns(2).Width = 35 * 256
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
    ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
    ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
    ///ws.Cells(row, 2).Value = "Color.Blue"
    ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
    ///ws.Cells(row, 2).Value = "true"
    ///ws.Cells(row, 2).Style.Font.Italic = True
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
    ///ws.Cells(row, 2).Value = "Comic Sans MS"
    ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
    ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
    ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
    ///ws.Cells(row, 2).Value = "18 * 20"
    ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
    ///ws.Cells(row, 2).Value = "true"
    ///ws.Cells(row, 2).Style.Font.Strikeout = True
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
    ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
    ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
    ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
    ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
    ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
    ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Indent"
    ///ws.Cells(row, 2).Value = "five"
    ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
    ///ws.Cells(row, 2).Style.Indent = 5
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
    ///ws.Cells(row, 2).Value = "true"
    ///<font color="Green">' Set row height to 50 points.</font>
    ///ws.Rows(row).Height = 50 * 20
    ///ws.Cells(row, 2).Style.IsTextVertical = True
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
    ///ws.Cells(row, 2).Value = 1234
    ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.Rotation"
    ///ws.Cells(row, 2).Value = "35 degrees up"
    ///ws.Cells(row, 2).Style.Rotation = 35
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
    ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
    ///ws.Cells(row, 2).Style.ShrinkToFit = True
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
    ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
    ///<font color="Green">' Set row height to 30 points.</font>
    ///ws.Rows(row).Height = 30 * 20
    ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
    ///
    ///row = row + 2
    ///ws.Cells(row, 1).Value = ".Style.WrapText"
    ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
    ///ws.Cells(row, 2).Style.WrapText = True
    ///End Sub
    ///</code>
    ///<code lang="C#">
    ///static void StylesSample(ExcelWorksheet ws)
    ///{
    ///ws.Cells[0,0].Value = "Cell style examples:";
    ///
    ///int row = 0;
    ///
    ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
    ///ws.Columns[0].Width = 4 * 256;
    ///ws.Columns[1].Width = 30 * 256;
    ///ws.Columns[2].Width = 35 * 256;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
    ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
    ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
    ///ws.Cells[row,2].Value = "Color.Blue";
    ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
    ///ws.Cells[row,2].Value = "true";
    ///ws.Cells[row,2].Style.Font.Italic = true;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
    ///ws.Cells[row,2].Value = "Comic Sans MS";
    ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
    ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
    ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
    ///ws.Cells[row,2].Value = "18 * 20";
    ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
    ///ws.Cells[row,2].Value = "true";
    ///ws.Cells[row,2].Style.Font.Strikeout = true;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
    ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
    ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
    ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
    ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
    ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
    ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Indent";
    ///ws.Cells[row,2].Value = "five";
    ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
    ///ws.Cells[row,2].Style.Indent = 5;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
    ///ws.Cells[row,2].Value = "true";
    ///<font color="Green">// Set row height to 50 points.</font>
    ///ws.Rows[row].Height = 50 * 20;
    ///ws.Cells[row,2].Style.IsTextVertical = true;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
    ///ws.Cells[row,2].Value = 1234;
    ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
    ///ws.Cells[row,2].Value = "35 degrees up";
    ///ws.Cells[row,2].Style.Rotation = 35;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
    ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
    ///ws.Cells[row,2].Style.ShrinkToFit = true;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
    ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
    ///<font color="Green">// Set row height to 30 points.</font>
    ///ws.Rows[row].Height = 30 * 20;
    ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
    ///
    ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
    ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
    ///ws.Cells[row,2].Style.WrapText = true;
    ///}
    ///</code>
    ///</example>
    internal sealed class CellStyle
    {
        // Methods
        ///<summary>
        ///Creates new cell style with default values.
        ///</summary>
        ///<remarks>
        ///Creating standalone cell style has sense only if you assign it to some Excel objects
        ///by setting <b>Style</b> property. Otherwise, the created cell style will have no effect on the Excel file.
        ///</remarks>
        public CellStyle()
        {
            this.UseFlags = CellStyleData.Properties.None;
            this.element = new CellStyleData(null, false);
        }

        internal CellStyle(CellStyleCachedCollection styleCollection)
        {
            this.UseFlags = CellStyleData.Properties.None;
            this.element = (CellStyleData) styleCollection.DefaultElement;
        }

        internal CellStyle(CellStyle style, WeakHashtable parentCollection)
        {
            this.UseFlags = CellStyleData.Properties.None;
            this.element = style.element;
            if (!this.element.IsInCache || (this.element.ParentCollection != parentCollection))
            {
                this.CloneElement(parentCollection);
                this.AddToQueue();
            }
            this.UseFlags = style.UseFlags;
        }

        private void AddToQueue()
        {
            WeakHashtable hashtable1 = this.element.ParentCollection;
            Queue queue1 = hashtable1.AddQueue;
            if (queue1.Count >= hashtable1.AddQueueSize)
            {
                ((CellStyle) queue1.Dequeue()).Consolidate();
            }
            queue1.Enqueue(this);
        }

        internal void BeforeChange()
        {
            if (this.element.IsInCache)
            {
                this.CloneElement(this.element.ParentCollection);
                this.AddToQueue();
            }
        }

        private void CloneElement(WeakHashtable parentCollection)
        {
            this.element = (CellStyleData) this.element.Clone(parentCollection);
        }

        internal void Consolidate()
        {
            this.element = (CellStyleData) this.element.FindExistingOrAddToCache();
        }

        internal void CopyIfNotUsed(CellStyle lowerPriority)
        {
            CellStyleData data1 = this.element;
            CellStyleData data2 = lowerPriority.element;
            if ((this.UseFlags & CellStyleData.Properties.HorizontalAlignment) == CellStyleData.Properties.None)
            {
                data1.HorizontalAlignment = data2.HorizontalAlignment;
            }
            if ((this.UseFlags & CellStyleData.Properties.VerticalAlignment) == CellStyleData.Properties.None)
            {
                data1.VerticalAlignment = data2.VerticalAlignment;
            }
            if ((this.UseFlags & CellStyleData.Properties.PatternStyle) == CellStyleData.Properties.None)
            {
                data1.PatternStyle = data2.PatternStyle;
            }
            if ((this.UseFlags & CellStyleData.Properties.PatternBackgroundColor) == CellStyleData.Properties.None)
            {
                data1.PatternBackgroundColor = data2.PatternBackgroundColor;
            }
            if ((this.UseFlags & CellStyleData.Properties.PatternForegroundColor) == CellStyleData.Properties.None)
            {
                data1.PatternForegroundColor = data2.PatternForegroundColor;
            }
            if ((this.UseFlags & CellStyleData.Properties.Indent) == CellStyleData.Properties.None)
            {
                data1.Indent = data2.Indent;
            }
            if ((this.UseFlags & CellStyleData.Properties.Rotation) == CellStyleData.Properties.None)
            {
                data1.Rotation = data2.Rotation;
            }
            if ((this.UseFlags & CellStyleData.Properties.Locked) == CellStyleData.Properties.None)
            {
                data1.Locked = data2.Locked;
            }
            if ((this.UseFlags & CellStyleData.Properties.FormulaHidden) == CellStyleData.Properties.None)
            {
                data1.FormulaHidden = data2.FormulaHidden;
            }
            if ((this.UseFlags & CellStyleData.Properties.WrapText) == CellStyleData.Properties.None)
            {
                data1.WrapText = data2.WrapText;
            }
            if ((this.UseFlags & CellStyleData.Properties.ShrinkToFit) == CellStyleData.Properties.None)
            {
                data1.ShrinkToFit = data2.ShrinkToFit;
            }
            if ((this.UseFlags & CellStyleData.Properties.NumberFormat) == CellStyleData.Properties.None)
            {
                data1.NumberFormat = data2.NumberFormat;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontName) == CellStyleData.Properties.None)
            {
                data1.FontData.Name = data2.FontData.Name;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontColor) == CellStyleData.Properties.None)
            {
                data1.FontData.Color = data2.FontData.Color;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontWeight) == CellStyleData.Properties.None)
            {
                data1.FontData.Weight = data2.FontData.Weight;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontSize) == CellStyleData.Properties.None)
            {
                data1.FontData.Size = data2.FontData.Size;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontItalic) == CellStyleData.Properties.None)
            {
                data1.FontData.Italic = data2.FontData.Italic;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontStrikeout) == CellStyleData.Properties.None)
            {
                data1.FontData.Strikeout = data2.FontData.Strikeout;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontScriptPosition) == CellStyleData.Properties.None)
            {
                data1.FontData.ScriptPosition = data2.FontData.ScriptPosition;
            }
            if ((this.UseFlags & CellStyleData.Properties.FontUnderlineStyle) == CellStyleData.Properties.None)
            {
                data1.FontData.UnderlineStyle = data2.FontData.UnderlineStyle;
            }
            for (int num1 = 0; num1 < 4; num1++)
            {
                if ((data1.BordersUsed & CellBorder.MultipleFromIndividualBorder((IndividualBorder) num1)) == MultipleBorders.None)
                {
                    data1.BorderColor[num1] = data2.BorderColor[num1];
                    data1.BorderStyle[num1] = data2.BorderStyle[num1];
                }
            }
            if ((data1.BordersUsed & MultipleBorders.Diagonal) == MultipleBorders.None)
            {
                data1.BorderColor[4] = data2.BorderColor[4];
                data1.BorderStyle[4] = data2.BorderStyle[4];
            }
            data1.BordersUsed |= data2.BordersUsed;
            this.UseFlags |= lowerPriority.UseFlags;
        }


        // Properties
        ///<summary>
        ///Gets or sets cell borders (<see cref="MB.WinEIDrive.Excel.CellBorder">CellBorder</see>).
        ///</summary>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        public CellBorders Borders
        {
            get
            {
                return new CellBorders(this);
            }
            set
            {
                this.BeforeChange();
                value.CopyTo(this);
            }
        }

        internal CellStyleData Element
        {
            get
            {
                return this.element;
            }
        }

        ///<summary>
        ///Get or sets fill pattern.
        ///</summary>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        public ExcelFillPattern FillPattern
        {
            get
            {
                return new ExcelFillPattern(this);
            }
            set
            {
                this.BeforeChange();
                value.CopyTo(this);
            }
        }

        ///<summary>
        ///Gets or sets font related settings.
        ///</summary>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        public ExcelFont Font
        {
            get
            {
                return new ExcelFont(this);
            }
            set
            {
                this.BeforeChange();
                value.CopyTo(this);
            }
        }

        ///<summary>
        ///Gets or sets whether the formula is hidden in the formula bar when the cell is selected.
        ///</summary>
        ///<remarks>
        ///<p>This property has meaning only if <see cref="MB.WinEIDrive.Excel.ExcelFile.Protected">ExcelFile.Protected</see>
        ///is set to <b>true</b>. For more information consult Microsoft Excel documentation.</p>
        ///<p>Default value for this property is <b>false</b>.</p>
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.Protected">ExcelFile.Protected</seealso>
        public bool FormulaHidden
        {
            get
            {
                return this.element.FormulaHidden;
            }
            set
            {
                this.BeforeChange();
                this.element.FormulaHidden = value;
                this.UseFlags |= CellStyleData.Properties.FormulaHidden;
            }
        }

        ///<summary>
        ///Gets or sets horizontal alignment.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <see cref="MB.WinEIDrive.Excel.HorizontalAlignmentStyle.General">
        ///HorizontalAlignmentStyle.General</see>.
        ///</remarks>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.CellStyle.VerticalAlignment" />
        public HorizontalAlignmentStyle HorizontalAlignment
        {
            get
            {
                return this.element.HorizontalAlignment;
            }
            set
            {
                this.BeforeChange();
                this.element.HorizontalAlignment = value;
                this.UseFlags |= CellStyleData.Properties.HorizontalAlignment;
            }
        }

        ///<summary>
        ///Gets or sets cell data indentation.
        ///</summary>
        ///<remarks>
        ///<p>Indents cell contents from any edge of the cell, depending on
        ///<see cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical">IsTextVertical</see> and associated alignment. If
        ///you set this property to non-zero value and <see cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical">
        ///IsTextVertical</see> is <b>false</b>, it is recommended thay you also set
        ///<see cref="MB.WinEIDrive.Excel.CellStyle.HorizontalAlignment">HorizontalAlignment</see> to
        ///<see cref="MB.WinEIDrive.Excel.HorizontalAlignmentStyle.Left">HorizontalAlignmentStyle.Left</see> or
        ///<see cref="MB.WinEIDrive.Excel.HorizontalAlignmentStyle.Right">HorizontalAlignmentStyle.Right</see>.
        ///Otherwise some versions of Microsoft Excel will have problems interpreting Indent value in
        ///"Format Cells..." dialog &gt; "Alignment" tab. In the case where
        ///<see cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical">IsTextVertical</see> is <b>true</b>, you should set
        ///<see cref="MB.WinEIDrive.Excel.CellStyle.VerticalAlignment">VerticalAlignment</see> instead.</p>
        ///<p>Unit is one character. Value must be between 0 and 15.</p>
        ///<p>Default value for this property is 0.</p>
        ///</remarks>
        ///<exception cref="System.ArgumentOutOfRangeException">Thrown if value is out of range.</exception>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        public int Indent
        {
            get
            {
                return this.element.Indent;
            }
            set
            {
                if ((value < 0) || (value > 15))
                {
                    throw new ArgumentOutOfRangeException("value", value, "Indent must be between 0 and 15.");
                }
                this.BeforeChange();
                this.element.Indent = value;
                this.UseFlags |= CellStyleData.Properties.Indent;
            }
        }

        ///<summary>
        ///Returns <b>true</b> if cell style is default; otherwise, <b>false</b>.
        ///</summary>
        public bool IsDefault
        {
            get
            {
                return object.ReferenceEquals(this.element, this.element.ParentCollection.DefaultElement);
            }
        }

        ///<summary>
        ///Gets or sets whether the cell text is displayed in a vertical style.
        ///</summary>
        ///<remarks>
        ///<p>If <b>true</b> letters are stacked top-to-bottom.</p>
        ///<p>Because of Microsoft Excel limitations, this property
        ///and <see cref="MB.WinEIDrive.Excel.CellStyle.Rotation">Rotation</see> property can't be used at the same time.
        ///When set, <see cref="MB.WinEIDrive.Excel.CellStyle.Rotation">Rotation</see> property is set to 0.
        ///If <see cref="MB.WinEIDrive.Excel.CellStyle.Rotation">Rotation</see> property is latter set to some non-zero value,
        ///this property will be set to <b>false</b>.</p>
        ///<p>Default value for this property is <b>false</b>.</p>
        ///</remarks>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.CellStyle.Rotation" />
        public bool IsTextVertical
        {
            get
            {
                return (this.element.Rotation == 0xff);
            }
            set
            {
                if (this.IsTextVertical)
                {
                    if (value)
                    {
                        return;
                    }
                    this.Rotation = 0;
                }
                else if (value)
                {
                    this.BeforeChange();
                    this.element.Rotation = 0xff;
                    this.UseFlags |= CellStyleData.Properties.Rotation;
                }
            }
        }

        ///<summary>
        ///Gets or sets if the cell is locked.
        ///</summary>
        ///<remarks>
        ///<p>This property has meaning only if <see cref="MB.WinEIDrive.Excel.ExcelFile.Protected">ExcelFile.Protected</see>
        ///is set to <b>true</b>. For more information consult Microsoft Excel documentation.</p>
        ///<p>Default value for this property is <b>true</b>.</p>
        ///</remarks>
        ///<seealso cref="MB.WinEIDrive.Excel.ExcelFile.Protected">ExcelFile.Protected</seealso>
        public bool Locked
        {
            get
            {
                return this.element.Locked;
            }
            set
            {
                this.BeforeChange();
                this.element.Locked = value;
                this.UseFlags |= CellStyleData.Properties.Locked;
            }
        }

        ///<summary>
        ///Gets or sets format string that will be used to interpret and display cell value.
        ///</summary>
        ///<remarks>
        ///<p>If the value of this property is <see cref="System.String.Empty">String.Empty</see> and
        ///<see cref="MB.WinEIDrive.Excel.ExcelCell.Value">ExcelCell.Value</see> is of
        ///<see cref="System.DateTime">DateTime</see> type,
        ///ISO date/time format will be used as number format.</p>
        ///<p>For more information on number format strings consult Microsoft Excel documentation.</p>
        ///Default value for this property is <see cref="System.String.Empty">String.Empty</see>.
        ///</remarks>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        public string NumberFormat
        {
            get
            {
                return this.element.NumberFormat;
            }
            set
            {
                this.BeforeChange();
                this.element.NumberFormat = value;
                this.UseFlags |= CellStyleData.Properties.NumberFormat;
            }
        }

        ///<summary>
        ///Gets or sets cell data rotation.
        ///</summary>
        ///<remarks>
        ///<p>Unit is degrees (1/360th of a full circle). Value must be between -90 and 90 and specifies
        ///anticlockwise (counterclockwise [N.Amer]) rotation from the normal position.</p>
        ///<p>Because of Microsoft Excel limitations, this property and
        ///<see cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical">IsTextVertical</see> property can't be used at the
        ///same time. If <see cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical">IsTextVertical</see> is <b>true</b> and
        ///rotation is set, <see cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical">IsTextVertical</see> will be set to
        ///<b>false</b>. When <see cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical">IsTextVertical</see> is set to
        ///<b>true</b>, rotation will be set to 0.</p>
        ///<p>Default value for this property is 0.</p>
        ///</remarks>
        ///<exception cref="System.ArgumentOutOfRangeException">Thrown if value is not between -90 and 90.</exception>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.CellStyle.IsTextVertical" />
        public int Rotation
        {
            get
            {
                if (this.IsTextVertical)
                {
                    return 0;
                }
                return this.element.Rotation;
            }
            set
            {
                if (value != this.Rotation)
                {
                    if ((value < -90) || (value > 90))
                    {
                        throw new ArgumentOutOfRangeException("value", value, "Rotation must be between -90 and 90.");
                    }
                    this.BeforeChange();
                    if (value >= 0)
                    {
                        this.element.Rotation = value;
                    }
                    else
                    {
                        this.element.Rotation = 90 - value;
                    }
                    this.UseFlags |= CellStyleData.Properties.Rotation;
                }
            }
        }

        ///<summary>
        ///Gets or sets if the cell text is shrunk to fit the cell.
        ///</summary>
        ///<remarks>
        ///<p>If set to <b>true</b>, reduces the apparent size of font characters so that all data in a selected
        ///cell fits within the column. For more information consult Microsoft Excel documentation.</p>
        ///Default value for this property is <b>false</b>.
        ///</remarks>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        public bool ShrinkToFit
        {
            get
            {
                return this.element.ShrinkToFit;
            }
            set
            {
                this.BeforeChange();
                this.element.ShrinkToFit = value;
                this.UseFlags |= CellStyleData.Properties.ShrinkToFit;
            }
        }

        ///<summary>
        ///Gets or sets vertical alignment.
        ///</summary>
        ///<remarks>
        ///Default value for this property is <see cref="MB.WinEIDrive.Excel.VerticalAlignmentStyle.Bottom">
        ///VerticalAlignmentStyle.Bottom</see>.
        ///</remarks>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        ///<seealso cref="MB.WinEIDrive.Excel.CellStyle.HorizontalAlignment" />
        public VerticalAlignmentStyle VerticalAlignment
        {
            get
            {
                return this.element.VerticalAlignment;
            }
            set
            {
                this.BeforeChange();
                this.element.VerticalAlignment = value;
                this.UseFlags |= CellStyleData.Properties.VerticalAlignment;
            }
        }

        ///<summary>
        ///Gets or sets if the text is wrapped.
        ///</summary>
        ///<remarks>
        ///<p>If set to <b>true</b>, wraps cell data into multiple lines in a cell. The number of wrapped lines is
        ///dependent on the width of the column and the length of the cell contents.</p>
        ///Default value for this property is <b>false</b>.
        ///</remarks>
        ///<example> Following code demonstrates various cell style properties:
        ///<code lang="Visual Basic">
        ///Sub StylesSample(ByVal ws As ExcelWorksheet)
        ///ws.Cells(0, 0).Value = "Cell style examples:"
        ///
        ///Dim row As Integer = 0
        ///
        ///<font color="Green">' Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns(0).Width = 4 * 256
        ///ws.Columns(1).Width = 30 * 256
        ///ws.Columns(2).Width = 35 * 256
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Borders.SetBorders(...)"
        ///ws.Cells(row, 2).Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.FillPattern.SetPattern(...)"
        ///ws.Cells(row, 2).Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow)
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Color ="
        ///ws.Cells(row, 2).Value = "Color.Blue"
        ///ws.Cells(row, 2).Style.Font.Color = Color.Blue
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Italic ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Italic = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Name ="
        ///ws.Cells(row, 2).Value = "Comic Sans MS"
        ///ws.Cells(row, 2).Style.Font.Name = "Comic Sans MS"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.ScriptPosition ="
        ///ws.Cells(row, 2).Value = "ScriptPosition.Superscript"
        ///ws.Cells(row, 2).Style.Font.ScriptPosition = ScriptPosition.Superscript
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Size ="
        ///ws.Cells(row, 2).Value = "18 * 20"
        ///ws.Cells(row, 2).Style.Font.Size = 18 * 20
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Strikeout ="
        ///ws.Cells(row, 2).Value = "true"
        ///ws.Cells(row, 2).Style.Font.Strikeout = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.UnderlineStyle ="
        ///ws.Cells(row, 2).Value = "UnderlineStyle.Double"
        ///ws.Cells(row, 2).Style.Font.UnderlineStyle = UnderlineStyle.Double
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Font.Weight ="
        ///ws.Cells(row, 2).Value = "ExcelFont.BoldWeight"
        ///ws.Cells(row, 2).Style.Font.Weight = ExcelFont.BoldWeight
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.HorizontalAlignment ="
        ///ws.Cells(row, 2).Value = "HorizontalAlignmentStyle.Center"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Indent"
        ///ws.Cells(row, 2).Value = "five"
        ///ws.Cells(row, 2).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
        ///ws.Cells(row, 2).Style.Indent = 5
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.IsTextVertical = "
        ///ws.Cells(row, 2).Value = "true"
        ///<font color="Green">' Set row height to 50 points.</font>
        ///ws.Rows(row).Height = 50 * 20
        ///ws.Cells(row, 2).Style.IsTextVertical = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.NumberFormat"
        ///ws.Cells(row, 2).Value = 1234
        ///ws.Cells(row, 2).Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]"
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.Rotation"
        ///ws.Cells(row, 2).Value = "35 degrees up"
        ///ws.Cells(row, 2).Style.Rotation = 35
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.ShrinkToFit"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears shrunk."
        ///ws.Cells(row, 2).Style.ShrinkToFit = True
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.VerticalAlignment ="
        ///ws.Cells(row, 2).Value = "VerticalAlignmentStyle.Top"
        ///<font color="Green">' Set row height to 30 points.</font>
        ///ws.Rows(row).Height = 30 * 20
        ///ws.Cells(row, 2).Style.VerticalAlignment = VerticalAlignmentStyle.Top
        ///
        ///row = row + 2
        ///ws.Cells(row, 1).Value = ".Style.WrapText"
        ///ws.Cells(row, 2).Value = "This property is set to true so this text appears broken into multiple lines."
        ///ws.Cells(row, 2).Style.WrapText = True
        ///End Sub
        ///</code>
        ///<code lang="C#">
        ///static void StylesSample(ExcelWorksheet ws)
        ///{
        ///ws.Cells[0,0].Value = "Cell style examples:";
        ///
        ///int row = 0;
        ///
        ///<font color="Green">// Column width of 4, 30 and 35 characters.</font>
        ///ws.Columns[0].Width = 4 * 256;
        ///ws.Columns[1].Width = 30 * 256;
        ///ws.Columns[2].Width = 35 * 256;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Borders.SetBorders(...)";
        ///ws.Cells[row,2].Style.Borders.SetBorders(MultipleBorders.All, Color.FromArgb(252, 1, 1), LineStyle.Thin);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.FillPattern.SetPattern(...)";
        ///ws.Cells[row,2].Style.FillPattern.SetPattern(FillPatternStyle.ThinHorizontalCrosshatch, Color.Green, Color.Yellow);
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Color =";
        ///ws.Cells[row,2].Value = "Color.Blue";
        ///ws.Cells[row,2].Style.Font.Color = Color.Blue;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Italic =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Italic = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Name =";
        ///ws.Cells[row,2].Value = "Comic Sans MS";
        ///ws.Cells[row,2].Style.Font.Name = "Comic Sans MS";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.ScriptPosition =";
        ///ws.Cells[row,2].Value = "ScriptPosition.Superscript";
        ///ws.Cells[row,2].Style.Font.ScriptPosition = ScriptPosition.Superscript;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Size =";
        ///ws.Cells[row,2].Value = "18 * 20";
        ///ws.Cells[row,2].Style.Font.Size = 18 * 20;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Strikeout =";
        ///ws.Cells[row,2].Value = "true";
        ///ws.Cells[row,2].Style.Font.Strikeout = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.UnderlineStyle =";
        ///ws.Cells[row,2].Value = "UnderlineStyle.Double";
        ///ws.Cells[row,2].Style.Font.UnderlineStyle = UnderlineStyle.Double;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Font.Weight =";
        ///ws.Cells[row,2].Value = "ExcelFont.BoldWeight";
        ///ws.Cells[row,2].Style.Font.Weight = ExcelFont.BoldWeight;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.HorizontalAlignment =";
        ///ws.Cells[row,2].Value = "HorizontalAlignmentStyle.Center";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Indent";
        ///ws.Cells[row,2].Value = "five";
        ///ws.Cells[row,2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
        ///ws.Cells[row,2].Style.Indent = 5;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.IsTextVertical = ";
        ///ws.Cells[row,2].Value = "true";
        ///<font color="Green">// Set row height to 50 points.</font>
        ///ws.Rows[row].Height = 50 * 20;
        ///ws.Cells[row,2].Style.IsTextVertical = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.NumberFormat";
        ///ws.Cells[row,2].Value = 1234;
        ///ws.Cells[row,2].Style.NumberFormat = "#.##0,00 [$Krakozhian Money Units]";
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.Rotation";
        ///ws.Cells[row,2].Value = "35 degrees up";
        ///ws.Cells[row,2].Style.Rotation = 35;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.ShrinkToFit";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears shrunk.";
        ///ws.Cells[row,2].Style.ShrinkToFit = true;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.VerticalAlignment =";
        ///ws.Cells[row,2].Value = "VerticalAlignmentStyle.Top";
        ///<font color="Green">// Set row height to 30 points.</font>
        ///ws.Rows[row].Height = 30 * 20;
        ///ws.Cells[row,2].Style.VerticalAlignment = VerticalAlignmentStyle.Top;
        ///
        ///ws.Cells[row+=2,1].Value = ".Style.WrapText";
        ///ws.Cells[row,2].Value = "This property is set to true so this text appears broken into multiple lines.";
        ///ws.Cells[row,2].Style.WrapText = true;
        ///}
        ///</code>
        ///</example>
        public bool WrapText
        {
            get
            {
                return this.element.WrapText;
            }
            set
            {
                this.BeforeChange();
                this.element.WrapText = value;
                this.UseFlags |= CellStyleData.Properties.WrapText;
            }
        }


        // Fields
        private CellStyleData element;
        internal CellStyleData.Properties UseFlags;
    }
}

