using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;

namespace DIYReport.Common
{
   public class EnumPrintersHelperEx
    {
        private static PrintDocument fPrintDocument = new PrintDocument();
        /// <summary>
        /// 获取本机默认打印机名称 
        /// </summary>
        public static String DefaultPrinter {
            get {
                return fPrintDocument.PrinterSettings.PrinterName;
            }
        }
       /// <summary>
       /// 判断打印机是否存在
       /// </summary>
       /// <param name="printerName"></param>
       /// <returns></returns>
        public static bool CheckExistsPrinter(string printerName) {

            if (string.IsNullOrEmpty(printerName)) return false;
            var printers = GetLocalPrinters();
            return printers != null && printers.Contains(printerName);

        }
        /// <summary>
        /// 获取本机的打印机列表。列表中的第一项就是默认打印机。
        /// </summary>
        /// <returns></returns>
        public static List<String> GetLocalPrinters() {
            try {
                List<String> fPrinters = new List<string>();
                fPrinters.Add(DefaultPrinter);
                foreach (String fPrinterName in PrinterSettings.InstalledPrinters) {
                    if (!fPrinters.Contains(fPrinterName))
                        fPrinters.Add(fPrinterName);
                }
                return fPrinters;
            }
            catch (Exception ex) {
                return new List<string>();
            }
        }
    }
}
