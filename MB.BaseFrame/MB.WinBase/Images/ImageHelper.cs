using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MB.WinBase.Images {
    class ImageHelper {

        public static ImageHelper Instance {
            get {
                return MB.Util.SingletonProvider<ImageHelper>.Instance;  
            }
        }

        private static readonly string IAMGE_RESOURCE_BASE_PATH = "MB.WinBase.Images.";
        /// <summary>
        /// 从资源中创建一个位图。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Bitmap CreateBitmapFromResources(string fileName) {
            string fileFullName = IAMGE_RESOURCE_BASE_PATH + fileName; 
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream stream = asm.GetManifestResourceStream(fileFullName);
            if (stream == null)
                throw new MB.Util.APPException(string.Format("文件名:{0} 在资源文件中找不到,请确认是否已经添加该文件作为嵌入资源?", fileFullName));

            Bitmap image = (Bitmap)Bitmap.FromStream(stream);
            return image;
        }
    }
}
