using System;
using System.Collections.Generic;
using System.Text;
using DIYReport.ReportModel.RptObj;

namespace DIYReport.Barcode
{
    class ShareLib
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static BarcodeLib.TYPE Convert(BarCodeType codeType)
        {
            switch (codeType) {
                case BarCodeType.Code128:
                    return BarcodeLib.TYPE.CODE128;
                case BarCodeType.Code128A:
                    return BarcodeLib.TYPE.CODE128A;
                case BarCodeType.Code128B:
                    return BarcodeLib.TYPE.CODE128B;
                case BarCodeType.Code128C:
                    return BarcodeLib.TYPE.CODE128C;
                case BarCodeType.Code39:
                    return BarcodeLib.TYPE.CODE39;
                case BarCodeType.EAN13:
                    return BarcodeLib.TYPE.EAN13;
                case BarCodeType.EAN8:
                    return BarcodeLib.TYPE.EAN8;
                default:
                    throw new Exception(string.Format("类型{0} 当前还不支持",codeType));

            }
        }
    }
}
