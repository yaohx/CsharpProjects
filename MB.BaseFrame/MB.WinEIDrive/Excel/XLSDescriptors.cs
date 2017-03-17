namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal sealed class XLSDescriptors
    {
        // Methods
        static XLSDescriptors()
        {
            XLSDescriptors.name2Descriptor = new Hashtable();
            XLSDescriptors.code2Descriptor = new Hashtable();
            XLSDescriptor[] descriptorArray1 = new XLSDescriptor[0x5c];
            object[] objArray1 = new object[] { "00 06", typeof(BOFSubstreamType), "BB 0D", "CC 07", "00 00 00 00", "00 06 00 00" } ;
            descriptorArray1[0] = new XLSDescriptor("BOF", 0x809, 0x10, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort) } ;
            descriptorArray1[1] = new XLSDescriptor("Protect", 0x12, 2, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(DefaultRowHeightOptions), typeof(ushort) } ;
            descriptorArray1[2] = new XLSDescriptor("DefaultRowHeight", 0x225, 4, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort) } ;
            descriptorArray1[3] = new XLSDescriptor("DefaultColumnWidth", 0x55, 2, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(Window1Options), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort) } ;
            descriptorArray1[4] = new XLSDescriptor("Window1", 0x3d, 0x12, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(FontOptions), typeof(ushort), typeof(ushort), typeof(ushort), typeof(byte), "00", "00", "00", typeof(ExcelShortString) } ;
            descriptorArray1[5] = new XLSDescriptor("Font", 0x31, -1, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ExcelLongString) } ;
            descriptorArray1[6] = new XLSDescriptor("Format", 0x41e, -1, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ushort), typeof(XFOptions1), typeof(byte), typeof(byte), typeof(ushort), typeof(ushort), typeof(ushort), typeof(uint), typeof(ushort) } ;
            descriptorArray1[7] = new XLSDescriptor("XF", 0xe0, 20, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(byte), typeof(byte) } ;
            descriptorArray1[8] = new XLSDescriptor("Style", 0x293, -1, typeof(StyleRecord), objArray1);
            objArray1 = new object[2];
            objArray1[0] = typeof(ushort);
            object[] objArray2 = new object[] { typeof(byte), typeof(byte), typeof(byte), typeof(byte) } ;
            objArray1[1] = objArray2;
            descriptorArray1[9] = new XLSDescriptor("Palette", 0x92, -1, typeof(PaletteRecord), objArray1);
            objArray1 = new object[5];
            objArray1[0] = "00 00 00 00";
            objArray1[1] = typeof(uint);
            objArray1[2] = typeof(uint);
            objArray1[3] = "00 00 00 00";
            objArray2 = new object[] { typeof(uint) } ;
            objArray1[4] = objArray2;
            descriptorArray1[10] = new XLSDescriptor("Index", 0x20b, -1, typeof(IndexRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ColumnInfoOptions), "00 00" } ;
            descriptorArray1[11] = new XLSDescriptor("ColumnInfo", 0x7d, 12, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(uint), typeof(uint), typeof(ushort), typeof(ushort), "00 00" } ;
            descriptorArray1[12] = new XLSDescriptor("Dimensions", 0x200, 14, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), "00 00", "00 00", typeof(RowOptions), typeof(ushort) } ;
            descriptorArray1[13] = new XLSDescriptor("Row", 520, 0x10, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(CellRecordHeader), typeof(uint) } ;
            descriptorArray1[14] = new XLSDescriptor("LabelSST", 0xfd, 10, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(CellRecordHeader) } ;
            descriptorArray1[15] = new XLSDescriptor("Blank", 0x201, 6, typeof(XLSRecord), objArray1);
            objArray1 = new object[4];
            objArray1[0] = typeof(ushort);
            objArray1[1] = typeof(ushort);
            objArray2 = new object[] { typeof(ushort) } ;
            objArray1[2] = objArray2;
            objArray1[3] = typeof(ushort);
            descriptorArray1[0x10] = new XLSDescriptor("MulBlank", 190, -1, typeof(MulBlankRecord), objArray1);
            objArray1 = new object[2];
            objArray1[0] = typeof(uint);
            objArray2 = new object[] { typeof(ushort) } ;
            objArray1[1] = objArray2;
            descriptorArray1[0x11] = new XLSDescriptor("DBCell", 0xd7, -1, typeof(DBCellRecord), objArray1);
            objArray1 = new object[] { typeof(CellRecordHeader), typeof(uint) } ;
            descriptorArray1[0x12] = new XLSDescriptor("RK", 0x27e, 10, typeof(RKRecord), objArray1);
            objArray1 = new object[4];
            objArray1[0] = typeof(ushort);
            objArray1[1] = typeof(ushort);
            objArray2 = new object[] { typeof(ushort), typeof(uint) } ;
            objArray1[2] = objArray2;
            objArray1[3] = typeof(ushort);
            descriptorArray1[0x13] = new XLSDescriptor("MulRK", 0xbd, -1, typeof(MulRKRecord), objArray1);
            objArray1 = new object[] { typeof(CellRecordHeader), typeof(double) } ;
            descriptorArray1[20] = new XLSDescriptor("Number", 0x203, 14, typeof(XLSRecord), objArray1);
            objArray1 = new object[6];
            objArray1[0] = typeof(CellRecordHeader);
            objArray2 = new object[] { typeof(byte) } ;
            objArray1[1] = objArray2;
            objArray1[2] = typeof(FormulaOptions);
            objArray1[3] = "00 00 00 00";
            objArray1[4] = typeof(ushort);
            objArray2 = new object[] { typeof(byte) } ;
            objArray1[5] = objArray2;
            descriptorArray1[0x15] = new XLSDescriptor("Formula", 6, -1, typeof(FormulaRecord), objArray1);
            objArray1 = new object[] { typeof(uint), typeof(BoundSheetVisibility), typeof(BoundSheetSheetType), typeof(ExcelShortString) } ;
            descriptorArray1[0x16] = new XLSDescriptor("BoundSheet", 0x85, -1, typeof(BoundSheetRecord), objArray1);
            objArray1 = new object[] { typeof(uint), typeof(uint), typeof(ExcelLongStrings) } ;
            descriptorArray1[0x17] = new XLSDescriptor("SST", 0xfc, -1, typeof(SSTRecord), objArray1);
            objArray1 = new object[] { typeof(ExcelLongStrings) } ;
            descriptorArray1[0x18] = new XLSDescriptor("Continue", 60, -1, typeof(ContinueRecord), objArray1);
            objArray1 = new object[2];
            objArray1[0] = typeof(ushort);
            objArray2 = new object[] { typeof(uint), typeof(ushort), "00 00" } ;
            objArray1[1] = objArray2;
            descriptorArray1[0x19] = new XLSDescriptor("ExtSST", 0xff, -1, typeof(ExtSSTRecord), objArray1);
            objArray1 = new object[] { typeof(WorksheetWindowOptions), typeof(ushort), typeof(ushort), typeof(ushort), "00 00", typeof(ushort), typeof(ushort), "00 00 00 00" } ;
            descriptorArray1[0x1a] = new XLSDescriptor("Window2", 0x23e, 0x12, typeof(XLSRecord), objArray1);
            objArray1 = new object[2];
            objArray1[0] = typeof(ushort);
            objArray2 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort) } ;
            objArray1[1] = objArray2;
            descriptorArray1[0x1b] = new XLSDescriptor("MergedCells", 0xe5, -1, typeof(MergedCellsRecord), objArray1);
            objArray1 = new object[] { typeof(WSBoolOptions) } ;
            descriptorArray1[0x1c] = new XLSDescriptor("WSBool", 0x81, 2, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort) } ;
            descriptorArray1[0x1d] = new XLSDescriptor("Guts", 0x80, 8, typeof(XLSRecord), objArray1);
            objArray1 = new object[2];
            objArray1[0] = typeof(ushort);
            objArray2 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort) } ;
            objArray1[1] = objArray2;
            descriptorArray1[30] = new XLSDescriptor("HORIZONTALPAGEBREAKS", 0x1b, -1, typeof(HorizontalPageBreaksRecord), objArray1);
            objArray1 = new object[2];
            objArray1[0] = typeof(ushort);
            objArray2 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort) } ;
            objArray1[1] = objArray2;
            descriptorArray1[0x1f] = new XLSDescriptor("VERTICALPAGEBREAKS", 0x1a, -1, typeof(VerticalPageBreaksRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ushort) } ;
            descriptorArray1[0x20] = new XLSDescriptor("SCL", 160, 4, typeof(XLSRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ushort), typeof(SetupOptions), typeof(ushort), typeof(ushort), typeof(double), typeof(double), typeof(ushort) } ;
            descriptorArray1[0x21] = new XLSDescriptor("SETUP", 0xa1, 0x22, typeof(XLSRecord), objArray1);
            objArray1 = new object[2];
            objArray1[0] = typeof(ushort);
            objArray2 = new object[] { typeof(SheetIndexes) } ;
            objArray1[1] = objArray2;
            descriptorArray1[0x22] = new XLSDescriptor("EXTERNSHEET", 0x17, -1, typeof(ExternsheetRecord), objArray1);
            objArray1 = new object[8];
            objArray1[0] = "00 00 00";
            objArray1[1] = typeof(byte);
            objArray1[2] = typeof(ushort);
            objArray1[3] = "00 00";
            objArray1[4] = typeof(ushort);
            objArray1[5] = "00 00 00 00";
            objArray1[6] = typeof(ExcelStringWithoutLength);
            objArray2 = new object[] { typeof(byte) } ;
            objArray1[7] = objArray2;
            descriptorArray1[0x23] = new XLSDescriptor("NAME", 0x18, -1, typeof(NameRecord), objArray1);
            objArray1 = new object[] { typeof(ushort), "01 04" } ;
            descriptorArray1[0x24] = new XLSDescriptor("SUPBOOK", 430, 4, typeof(SupBookRecord), objArray1);
            descriptorArray1[0x25] = new XLSDescriptor("EOF", 10, 0, typeof(XLSRecord), null);
            descriptorArray1[0x26] = new XLSDescriptor("WRITEPROT", 0x86, -2, typeof(XLSRecord), null);
            descriptorArray1[0x27] = new XLSDescriptor("WRITEACCESS", 0x5c, -2, typeof(XLSRecord), null);
            descriptorArray1[40] = new XLSDescriptor("FILESHARING", 0x5b, -2, typeof(XLSRecord), null);
            descriptorArray1[0x29] = new XLSDescriptor("CODEPAGE", 0x42, -2, typeof(XLSRecord), null);
            descriptorArray1[0x2a] = new XLSDescriptor("WINDOWPROTECT", 0x19, -2, typeof(XLSRecord), null);
            descriptorArray1[0x2b] = new XLSDescriptor("OBJECTPROTECT", 0x63, -2, typeof(XLSRecord), null);
            descriptorArray1[0x2c] = new XLSDescriptor("HIDEOBJ", 0x8d, -2, typeof(XLSRecord), null);
            descriptorArray1[0x2d] = new XLSDescriptor("DATEMODE", 0x22, -2, typeof(XLSRecord), null);
            descriptorArray1[0x2e] = new XLSDescriptor("PRECISION", 14, -2, typeof(XLSRecord), null);
            descriptorArray1[0x2f] = new XLSDescriptor("REFRESHALL", 0x1b7, -2, typeof(XLSRecord), null);
            descriptorArray1[0x30] = new XLSDescriptor("BOOKBOOL", 0xda, -2, typeof(XLSRecord), null);
            descriptorArray1[0x31] = new XLSDescriptor("USESELFS", 0x160, -2, typeof(XLSRecord), null);
            descriptorArray1[50] = new XLSDescriptor("COUNTRY", 140, -2, typeof(XLSRecord), null);
            descriptorArray1[0x33] = new XLSDescriptor("CALCCOUNT", 12, -2, typeof(XLSRecord), null);
            descriptorArray1[0x34] = new XLSDescriptor("CALCMODE", 13, -2, typeof(XLSRecord), null);
            descriptorArray1[0x35] = new XLSDescriptor("REFMODE", 15, -2, typeof(XLSRecord), null);
            descriptorArray1[0x36] = new XLSDescriptor("DELTA", 0x10, -2, typeof(XLSRecord), null);
            descriptorArray1[0x37] = new XLSDescriptor("ITERATION", 0x11, -2, typeof(XLSRecord), null);
            descriptorArray1[0x38] = new XLSDescriptor("SAVERECALC", 0x5f, -2, typeof(XLSRecord), null);
            descriptorArray1[0x39] = new XLSDescriptor("PRINTHEADERS", 0x2a, -2, typeof(XLSRecord), null);
            descriptorArray1[0x3a] = new XLSDescriptor("PRINTGRIDLINES", 0x2b, -2, typeof(XLSRecord), null);
            descriptorArray1[0x3b] = new XLSDescriptor("GRIDSET", 130, -2, typeof(XLSRecord), null);
            descriptorArray1[60] = new XLSDescriptor("HEADER", 20, -2, typeof(XLSRecord), null);
            descriptorArray1[0x3d] = new XLSDescriptor("FOOTER", 0x15, -2, typeof(XLSRecord), null);
            descriptorArray1[0x3e] = new XLSDescriptor("HCENTER", 0x83, -2, typeof(XLSRecord), null);
            descriptorArray1[0x3f] = new XLSDescriptor("VCENTER", 0x84, -2, typeof(XLSRecord), null);
            descriptorArray1[0x40] = new XLSDescriptor("LEFTMARGIN", 0x26, -2, typeof(XLSRecord), null);
            descriptorArray1[0x41] = new XLSDescriptor("RIGHTMARGIN", 0x27, -2, typeof(XLSRecord), null);
            descriptorArray1[0x42] = new XLSDescriptor("TOPMARGIN", 40, -2, typeof(XLSRecord), null);
            descriptorArray1[0x43] = new XLSDescriptor("BOTTOMMARGIN", 0x29, -2, typeof(XLSRecord), null);
            descriptorArray1[0x44] = new XLSDescriptor("SORT", 0x90, -2, typeof(XLSRecord), null);
            descriptorArray1[0x45] = new XLSDescriptor("PANE", 0x41, -2, typeof(XLSRecord), null);
            descriptorArray1[70] = new XLSDescriptor("SELECTION", 0x1d, -2, typeof(XLSRecord), null);
            descriptorArray1[0x47] = new XLSDescriptor("STANDARDWIDTH", 0x99, -2, typeof(XLSRecord), null);
            descriptorArray1[0x48] = new XLSDescriptor("LABELRANGES", 0x15f, -2, typeof(XLSRecord), null);
            descriptorArray1[0x49] = new XLSDescriptor("CONDFMT", 0x1b0, -2, typeof(XLSRecord), null);
            descriptorArray1[0x4a] = new XLSDescriptor("CF", 0x1b1, -2, typeof(XLSRecord), null);
            descriptorArray1[0x4b] = new XLSDescriptor("HLINK", 440, -2, typeof(XLSRecord), null);
            descriptorArray1[0x4c] = new XLSDescriptor("QUICKTIP", 0x800, -2, typeof(XLSRecord), null);
            descriptorArray1[0x4d] = new XLSDescriptor("DVAL", 0x1b2, -2, typeof(XLSRecord), null);
            descriptorArray1[0x4e] = new XLSDescriptor("DV", 0x1be, -2, typeof(XLSRecord), null);
            descriptorArray1[0x4f] = new XLSDescriptor("SHEETLAYOUT", 0x862, -2, typeof(XLSRecord), null);
            descriptorArray1[80] = new XLSDescriptor("SHEETPROTECTION", 0x867, -2, typeof(XLSRecord), null);
            descriptorArray1[0x51] = new XLSDescriptor("RANGEPROTECTION", 0x868, -2, typeof(XLSRecord), null);
            descriptorArray1[0x52] = new XLSDescriptor("SCENPROTECT", 0xdd, -2, typeof(XLSRecord), null);
            descriptorArray1[0x53] = new XLSDescriptor("PASSWORD", 0x13, -2, typeof(XLSRecord), null);
            descriptorArray1[0x54] = new XLSDescriptor("OBJ", 0x5d, -2, typeof(XLSRecord), null);
            descriptorArray1[0x55] = new XLSDescriptor("MSODRAWING", 0xec, -2, typeof(XLSRecord), null);
            descriptorArray1[0x56] = new XLSDescriptor("MSODRAWINGGROUP", 0xeb, -2, typeof(XLSRecord), null);
            descriptorArray1[0x57] = new XLSDescriptor("TXO", 0x1b6, -2, typeof(XLSRecord), null);
            descriptorArray1[0x58] = new XLSDescriptor("SHRFMLA", 0x4bc, -2, typeof(XLSRecord), null);
            objArray1 = new object[] { typeof(ExcelLongString) } ;
            descriptorArray1[0x59] = new XLSDescriptor("STRING", 0x207, -1, typeof(XLSRecord), objArray1);
            descriptorArray1[90] = new XLSDescriptor("FILEPASS", 0x2f, -2, typeof(XLSRecord), null);
            descriptorArray1[0x5b] = new XLSDescriptor("MSODRAWINGSELECTION", 0xed, -2, typeof(XLSRecord), null);
            XLSDescriptors.List = descriptorArray1;
            descriptorArray1 = XLSDescriptors.List;
            for (int num1 = 0; num1 < descriptorArray1.Length; num1++)
            {
                XLSDescriptor descriptor1 = descriptorArray1[num1];
                XLSDescriptors.name2Descriptor.Add(descriptor1.Name, descriptor1);
                XLSDescriptors.code2Descriptor.Add(descriptor1.Code, descriptor1);
            }
        }

        private XLSDescriptors()
        {
        }

        public static byte[] Format(object[] format, object[] args)
        {
            MemoryStream stream1;
            int num1 = 0;
            using (MemoryStream stream2 = (stream1 = new MemoryStream()))
            {
                using (BinaryWriter writer1 = new BinaryWriter(stream1, new UnicodeEncoding()))
                {
                    XLSDescriptors.FormatHelper(writer1, format, args, ref num1);
                    stream1.Capacity = (int) stream1.Length;
                }
            }
            return stream1.GetBuffer();
        }

        private static void FormatHelper(BinaryWriter bw, object[] format, object[] args, ref int currentArgIndex)
        {
            object[] objArray2 = format;
            for (int num2 = 0; num2 < objArray2.Length; num2++)
            {
                object obj1 = objArray2[num2];
                if (obj1 is string)
                {
                    bw.Write(Utilities.HexStr2ByteArr((string) obj1));
                }
                else if (obj1 is Type)
                {
                    object obj2 = args[currentArgIndex];
                    Type type1 = (Type) obj1;
                    if (type1.IsEnum)
                    {
                        type1 = Enum.GetUnderlyingType(type1);
                    }
                    switch (type1.FullName)
                    {
                        case "System.Int16":
                        {
                            bw.Write((short) obj2);
                            break;
                        }
                        case "System.UInt16":
                        {
                            bw.Write((ushort) obj2);
                            break;
                        }
                        case "System.UInt32":
                        {
							bw.Write(UInt32.Parse(obj2.ToString()));
							break;
                        }
                        case "System.UInt64":
                        {
                            bw.Write((ulong) obj2);
                            break;
                        }
                        case "System.Byte":
                        {
                            bw.Write((byte) obj2);
                            break;
                        }
                        case "System.Char":
                        {
                            bw.Write((char) obj2);
                            break;
                        }
                        case "System.Single":
                        {
                            bw.Write((float) obj2);
                            break;
                        }
                        case "System.Double":
                        {
                            bw.Write((double) obj2);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelShortString":
                        {
                            ((ExcelShortString) obj2).Write(bw);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelLongString":
                        {
                            ((ExcelLongString) obj2).Write(bw);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelStringWithoutLength":
                        {
                            ((ExcelStringWithoutLength) obj2).Write(bw);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelLongStrings":
                        {
                            ((ExcelLongStrings) obj2).Write(bw);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.CellRecordHeader":
                        {
                            ((CellRecordHeader) obj2).Write(bw);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.SheetIndexes":
                        {
                            ((SheetIndexes) obj2).Write(bw);
                            break;
                        }
                        default:
                        {
                            throw new Exception("Internal error: unsupported type in format.");
                        }
                    }
                    currentArgIndex += 1;
                }
                else
                {
                    if (!(obj1 is object[]))
                    {
                        throw new Exception("Internal error: wrong format in descriptor.");
                    }
                    object[] objArray1 = args[currentArgIndex] as object[];
                    int num1 = 0;
                    while (num1 < objArray1.Length)
                    {
                        XLSDescriptors.FormatHelper(bw, (object[]) obj1, objArray1, ref num1);
                    }
                    currentArgIndex += 1;
                }
            }
        }

        public static XLSDescriptor GetByCode(int code)
        {
            return (XLSDescriptor) XLSDescriptors.code2Descriptor[code];
        }

        public static XLSDescriptor GetByName(string name)
        {
            return (XLSDescriptor) XLSDescriptors.name2Descriptor[name];
        }

        public static object[] Parse(object[] format, byte[] body, VariableArrayCountDelegate vaCount, StringLengthDelegate lastStringLength)
        {
            if (format == null)
            {
                return new object[0];
            }
            object[] objArray1 = new object[format.Length];
            int num1 = 0;
            using (MemoryStream stream1 = new MemoryStream(body))
            {
                using (BinaryReader reader1 = new BinaryReader(stream1, new UnicodeEncoding()))
                {
                    XLSDescriptors.ParseHelper(reader1, format, ref objArray1, ref num1, vaCount, lastStringLength);
                }
            }
            object[] objArray2 = new object[num1];
            Array.Copy(objArray1, objArray2, num1);
            return objArray2;
        }

        public static void ParseHelper(BinaryReader br, object[] format, ref object[] loadedArgs, ref int currentArgIndex, VariableArrayCountDelegate vaCount, StringLengthDelegate lastStringLength)
        {
            object[] objArray4 = format;
            for (int num5 = 0; num5 < objArray4.Length; num5++)
            {
                object obj1 = objArray4[num5];
                if (obj1 is string)
                {
                    Stream stream1 = br.BaseStream;
                    stream1.Position += Utilities.GetByteArrLengthFromHexStr((string) obj1);
                }
                else if (obj1 is Type)
                {
                    Type type1 = (Type) obj1;
                    if (type1.IsEnum)
                    {
                        type1 = Enum.GetUnderlyingType(type1);
                    }
                    switch (type1.FullName)
                    {
                        case "System.Int16":
                        {
                            loadedArgs[currentArgIndex] = br.ReadInt16();
                            break;
                        }
                        case "System.UInt16":
                        {
                            loadedArgs[currentArgIndex] = br.ReadUInt16();
                            break;
                        }
                        case "System.UInt32":
                        {
                            loadedArgs[currentArgIndex] = br.ReadUInt32();
                            break;
                        }
                        case "System.UInt64":
                        {
                            loadedArgs[currentArgIndex] = br.ReadUInt64();
                            break;
                        }
                        case "System.Byte":
                        {
                            loadedArgs[currentArgIndex] = br.ReadByte();
                            break;
                        }
                        case "System.Char":
                        {
                            loadedArgs[currentArgIndex] = br.ReadChar();
                            break;
                        }
                        case "System.Single":
                        {
                            loadedArgs[currentArgIndex] = br.ReadSingle();
                            break;
                        }
                        case "System.Double":
                        {
                            loadedArgs[currentArgIndex] = br.ReadDouble();
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelShortString":
                        {
                            loadedArgs[currentArgIndex] = new ExcelShortString(br);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelLongString":
                        {
                            loadedArgs[currentArgIndex] = new ExcelLongString(br);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelStringWithoutLength":
                        {
                            loadedArgs[currentArgIndex] = new ExcelStringWithoutLength(br, lastStringLength(loadedArgs));
                            break;
                        }
                        case "MB.WinEIDrive.Excel.ExcelLongStrings":
                        {
                            int num1 = vaCount(loadedArgs, null, (int) br.BaseStream.Length);
                            loadedArgs[currentArgIndex] = new ExcelLongStrings(br, num1, 0);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.CellRecordHeader":
                        {
                            loadedArgs[currentArgIndex] = new CellRecordHeader(br);
                            break;
                        }
                        case "MB.WinEIDrive.Excel.SheetIndexes":
                        {
                            loadedArgs[currentArgIndex] = new SheetIndexes(br);
                            break;
                        }
                        default:
                        {
                            throw new Exception("Internal error: unsupported type in format.");
                        }
                    }
                    currentArgIndex += 1;
                }
                else
                {
                    if (!(obj1 is object[]))
                    {
                        throw new Exception("Internal error: wrong format in descriptor.");
                    }
                    object[] objArray1 = (object[]) obj1;
                    int num2 = vaCount(loadedArgs, objArray1, (int) br.BaseStream.Length);
                    object[] objArray2 = new object[objArray1.Length * num2];
                    int num3 = 0;
                    for (int num4 = 0; num4 < num2; num4++)
                    {
                        XLSDescriptors.ParseHelper(br, objArray1, ref objArray2, ref num3, vaCount, lastStringLength);
                    }
                    object[] objArray3 = new object[num3];
                    Array.Copy(objArray2, objArray3, num3);
                    loadedArgs[currentArgIndex] = objArray3;
                    currentArgIndex += 1;
                }
            }
        }

        public static bool ValidBodySize(XLSDescriptor des, int size, bool exception)
        {
            if (((des == null) || !des.IsFixedSize) || (des.BodySize == size))
            {
                return true;
            }
            object[] objArray1 = new object[] { "Record should have size ", des.BodySize, " and not ", size, "." } ;
            string text1 = string.Concat(objArray1);
            if (exception)
            {
                throw new Exception("Internal error: " + text1);
            }
            return false;
        }


        // Fields
        private static Hashtable code2Descriptor;
        public static XLSDescriptor[] List;
        private static Hashtable name2Descriptor;

        // Nested Types
        public delegate byte StringLengthDelegate(object[] loadedArgs);


        public delegate int VariableArrayCountDelegate(object[] loadedArgs, object[] varArr, int bodySize);

    }
}

