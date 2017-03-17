using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DIYReport.Common
{
    #region 预定义类型

    [FlagsAttribute]
    public enum PrinterEnumFlags
    {
        PRINTER_ENUM_DEFAULT = 0x00000001,
        PRINTER_ENUM_LOCAL = 0x00000002,
        PRINTER_ENUM_CONNECTIONS = 0x00000004,
        PRINTER_ENUM_FAVORITE = 0x00000004,
        PRINTER_ENUM_NAME = 0x00000008,
        PRINTER_ENUM_REMOTE = 0x00000010,
        PRINTER_ENUM_SHARED = 0x00000020,
        PRINTER_ENUM_NETWORK = 0x00000040,
        PRINTER_ENUM_EXPAND = 0x00004000,
        PRINTER_ENUM_CONTAINER = 0x00008000,
        PRINTER_ENUM_ICONMASK = 0x00ff0000,
        PRINTER_ENUM_ICON1 = 0x00010000,
        PRINTER_ENUM_ICON2 = 0x00020000,
        PRINTER_ENUM_ICON3 = 0x00040000,
        PRINTER_ENUM_ICON4 = 0x00080000,
        PRINTER_ENUM_ICON5 = 0x00100000,
        PRINTER_ENUM_ICON6 = 0x00200000,
        PRINTER_ENUM_ICON7 = 0x00400000,
        PRINTER_ENUM_ICON8 = 0x00800000,
        PRINTER_ENUM_HIDE = 0x01000000
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PRINTER_INFO_2
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pServerName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrinterName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pShareName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPortName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDriverName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pComment;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pLocation;
        public IntPtr pDevMode;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pSepFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrintProcessor;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDatatype;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pParameters;
        public IntPtr pSecurityDescriptor;
        public uint Attributes;
        public uint Priority;
        public uint DefaultPriority;
        public uint StartTime;
        public uint UntilTime;
        public uint Status;
        public uint cJobs;
        public uint AveragePPM;
    }

    #endregion

    /// <summary>
    /// 获取本地打印机处理相关。
    /// </summary>
    public class EnumPrintersHelper
    {
        #region 引用 WindowsAPI
        //引用API声明
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumPrinters(
                                        PrinterEnumFlags Flags,
                                        string Name,
                                        uint Level,
                                        IntPtr pPrinterEnum,
                                        uint cbBuf,
                                        ref uint pcbNeeded,
                                        ref uint pcReturned
                                                );

        #endregion

        #region public static 函数...
        /// <summary>
        /// 遍历打印机
        /// </summary>
        /// <param name="Flags"></param>
        /// <returns></returns>
        public static PRINTER_INFO_2[] EnumPrinters(PrinterEnumFlags Flags) {
            PRINTER_INFO_2[] Info2 = null;

            uint cbNeeded = 0;
            uint cReturned = 0;
            try {
                bool ret = EnumPrinters(Flags, null, 2, IntPtr.Zero, 0, ref cbNeeded, ref cReturned);
                IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
                ret = EnumPrinters(Flags, null, 2, pAddr, cbNeeded, ref cbNeeded, ref cReturned);

                if (ret) {
                    Info2 = new PRINTER_INFO_2[cReturned];
                    int offset = pAddr.ToInt32();
                    for (int i = 0; i < cReturned; i++) {
                        Info2[i] = (PRINTER_INFO_2)Marshal.PtrToStructure(new IntPtr(offset), typeof(PRINTER_INFO_2));
                        offset += Marshal.SizeOf(typeof(PRINTER_INFO_2));
                    }

                    Marshal.FreeHGlobal(pAddr);
                }

                return Info2;
            }
            catch (Exception ex) {
                throw new Exception("获取本机打印机信息出错" + ex.Message);
            }
        }
        /// <summary>
        /// 根据打印机名称判断在本地是否存在该指定的打印机。
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public static bool CheckExistsPrinter(string printerName) {
            if (string.IsNullOrEmpty(printerName)) return false;

            var printers = EnumPrinters(PrinterEnumFlags.PRINTER_ENUM_LOCAL | PrinterEnumFlags.PRINTER_ENUM_NETWORK);

            if (printers == null || printers.Length == 0) return false;

            bool b = Array.Exists<PRINTER_INFO_2>(printers, o => string.Compare(o.pPrinterName,printerName,true)==0 );
            return b;
        }
        #endregion public static 函数...

        #region unit test...
        private  static void testEnumPrinters() {
            PRINTER_INFO_2[] printInfo;

            printInfo = EnumPrinters(PrinterEnumFlags.PRINTER_ENUM_LOCAL | PrinterEnumFlags.PRINTER_ENUM_NETWORK);

            if (printInfo != null && printInfo.Length >= 0) {
                for (int i = 0; i < printInfo.Length; i++) {
                    Console.WriteLine(printInfo[i].pPrinterName);
                }
            }

            Console.Read();
        }
        #endregion unit test...
    }
}
