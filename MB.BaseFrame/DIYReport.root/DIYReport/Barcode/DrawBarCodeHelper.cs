using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DIYReport.ReportModel.RptObj;
using DIYReport.Drawing;
using System.IO;
 

namespace DIYReport.Barcode
{
    /// <summary>
    /// 扩展的条形码绘制处理类。
    /// 提供给外部应用程序使用。
    /// </summary>
    public class DrawBarCodeHelper
    {
        /// <summary>
        /// 根据barCode 字符窜创建一 张图片。
        /// </summary>
        /// <param name="codeInfo"></param>
        /// <returns></returns>
        public static Bitmap CreateBarCodeImage(BarCodeInfo codeInfo) {
            var rptCode = createRptCode(codeInfo);
            Bitmap dstImage = new Bitmap(rptCode.Rect.Width, rptCode.Rect.Height);
            Graphics g = System.Drawing.Graphics.FromImage(dstImage);
            DrawRptBarCode draw = new DrawRptBarCode();
            draw.Draw(g, rptCode);
            g.Dispose();
            return dstImage;
        }
        private static RptBarCode createRptCode(BarCodeInfo codeInfo) {
            RptBarCode code = new RptBarCode();
            code.CodeType = codeInfo.CodeType;

            code.Rect = new Rectangle(0, 0, codeInfo.Size.Width, codeInfo.Size.Height);
            code.LinePound = codeInfo.LineBound;
            code.ShowFooter = codeInfo.ShowFooter;
            return code;
        }
    }

    public class BarCodeInfo
    {
        public BarCodeInfo(string code) {
            ShowFooter = true;
            LineBound = 1;
            CodeType = BarCodeType.Code128;
            Size = new Size(200, 80);
        }
        public string Value { get; set; }
        public bool ShowFooter { get; set; }
        public int LineBound { get; set; }
        public Size Size { get; set; }
        public BarCodeType CodeType { get; set; }
    }
}
