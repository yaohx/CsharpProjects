namespace MB.WinEIDrive.Excel
{
    using System;

    internal class NumberFormatCollection : IndexedHashCollection
    {
        // Methods
        static NumberFormatCollection()
        {
            object[,] objArray1 = new object[0x23, 2];
            objArray1[0, 0] = 1;
            objArray1[0, 1] = "0";
            objArray1[1, 0] = 2;
            objArray1[1, 1] = "0.00";
            objArray1[2, 0] = 3;
            objArray1[2, 1] = "#,##0";
            objArray1[3, 0] = 4;
            objArray1[3, 1] = "#,##0.00";
            objArray1[4, 0] = 5;
            objArray1[4, 1] = "$#,##0_);($#,##0)";
            objArray1[5, 0] = 6;
            objArray1[5, 1] = "$#,##0_);[Red]($#,##0)";
            objArray1[6, 0] = 7;
            objArray1[6, 1] = "$#,##0.00_);($#,##0.00)";
            objArray1[7, 0] = 8;
            objArray1[7, 1] = "$#,##0.00_);[Red]($#,##0.00)";
            objArray1[8, 0] = 9;
            objArray1[8, 1] = "0%";
            objArray1[9, 0] = 10;
            objArray1[9, 1] = "0.00%";
            objArray1[10, 0] = 11;
            objArray1[10, 1] = "0.00E+00";
            objArray1[11, 0] = 12;
            objArray1[11, 1] = "# ?/?";
            objArray1[12, 0] = 13;
            objArray1[12, 1] = "# ??/??";
            objArray1[13, 0] = 14;
            objArray1[13, 1] = "M/D/YY";
            objArray1[14, 0] = 15;
            objArray1[14, 1] = "D-MMM-YY";
            objArray1[15, 0] = 0x10;
            objArray1[15, 1] = "D-MMM";
            objArray1[0x10, 0] = 0x11;
            objArray1[0x10, 1] = "MMM-YY";
            objArray1[0x11, 0] = 0x12;
            objArray1[0x11, 1] = "h:mm AM/PM";
            objArray1[0x12, 0] = 0x13;
            objArray1[0x12, 1] = "h:mm:ss AM/PM";
            objArray1[0x13, 0] = 20;
            objArray1[0x13, 1] = "h:mm";
            objArray1[20, 0] = 0x15;
            objArray1[20, 1] = "h:mm:ss";
            objArray1[0x15, 0] = 0x16;
            objArray1[0x15, 1] = "M/D/YY h:mm";
            objArray1[0x16, 0] = 0x25;
            objArray1[0x16, 1] = "_(#,##0_);(#,##0)";
            objArray1[0x17, 0] = 0x26;
            objArray1[0x17, 1] = "_(#,##0_);[Red](#,##0)";
            objArray1[0x18, 0] = 0x27;
            objArray1[0x18, 1] = "_(#,##0.00_);(#,##0.00)";
            objArray1[0x19, 0] = 40;
            objArray1[0x19, 1] = "_(#,##0.00_);[Red](#,##0.00)";
            objArray1[0x1a, 0] = 0x29;
            objArray1[0x1a, 1] = "_($* #,##0_);_($* (#,##0);_($* \"-\"_);_(@_)";
            objArray1[0x1b, 0] = 0x2a;
            objArray1[0x1b, 1] = "_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)";
            objArray1[0x1c, 0] = 0x2b;
            objArray1[0x1c, 1] = "_($* #,##0.00_);_($* (#,##0.00);_($* \"-\"??_);_(@_)";
            objArray1[0x1d, 0] = 0x2c;
            objArray1[0x1d, 1] = "_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)";
            objArray1[30, 0] = 0x2d;
            objArray1[30, 1] = "mm:ss";
            objArray1[0x1f, 0] = 0x2e;
            objArray1[0x1f, 1] = "[h]:mm:ss";
            objArray1[0x20, 0] = 0x2f;
            objArray1[0x20, 1] = "mm:ss.0";
            objArray1[0x21, 0] = 0x30;
            objArray1[0x21, 1] = "##0.0E+0";
            objArray1[0x22, 0] = 0x31;
            objArray1[0x22, 1] = "@";
            NumberFormatCollection.builtIn = objArray1;
        }

        public NumberFormatCollection(bool readingOnly)
        {
            int num2;
            for (int num1 = num2 = 0; num1 < 0xa4; num1++)
            {
                if ((num2 < NumberFormatCollection.builtIn.GetLength(0)) && (((int) NumberFormatCollection.builtIn[num2, 0]) == num1))
                {
                    if (readingOnly)
                    {
                        base.AddArrayOnly(NumberFormatCollection.builtIn[num2, 1]);
                    }
                    else
                    {
                        base.AddInternal(NumberFormatCollection.builtIn[num2, 1]);
                    }
                    num2++;
                }
                else
                {
                    base.AddArrayOnly("");
                }
            }
        }

        ///<summary>
        ///This method is designed to be used ONLY for Excel file reading.
        ///</summary>
        ///<param name="index"></param>
        ///<param name="formatString"></param>
        public void SetNumberFormat(int index, string formatString)
        {
            if (index < this.Count)
            {
                this[index] = formatString;
            }
            else
            {
                for (int num1 = this.Count; num1 < index; num1++)
                {
                    base.AddArrayOnly("");
                }
                base.AddArrayOnly(formatString);
            }
        }


        // Fields
        private static object[,] builtIn;
    }
}

