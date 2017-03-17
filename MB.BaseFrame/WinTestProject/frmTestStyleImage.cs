using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestStyleImage : Form {
        public frmTestStyleImage() {
            InitializeComponent();
        }

        private void butGetStyleImage_Click(object sender, EventArgs e) {

        }
    }
    /// <summary>
    /// 商品款式图本地存储处理。
    /// </summary>
    public class StyleImageHelper {
        private static readonly string STYLE_IMAGE_PATH = MB.Util.General.GeApplicationDirectory() + @"MbfsStyleImage\";
        private static readonly string STYLE_IMAGE_EXTENSION = ".jpg";
        private static readonly string STYLE_IMAGE_FILE_NAME = "{0}_{1}" + STYLE_IMAGE_EXTENSION;
        private const int MIN_STYLE_CODE_LENGTH = 6;

        #region Instance...
        private static Object _Obj = new object();
        private static StyleImageHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected StyleImageHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static StyleImageHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new StyleImageHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 根据商品条码判断本地是否存在对应的款式图。
        /// </summary>
        /// <param name="styleCode"></param>
        /// <param name="isOcxCode"></param>
        /// <returns></returns>
        public bool ExistsLocalStyleImage(string styleCode, bool isOcxCode) {
             if(!System.IO.Directory.Exists(STYLE_IMAGE_PATH)) return false;
             if (string.IsNullOrEmpty(styleCode) || styleCode.Length < MIN_STYLE_CODE_LENGTH) return false;

             string[] files = System.IO.Directory.GetFiles(STYLE_IMAGE_PATH);
             if (files.Length == 0) return false;
             foreach (string file in files) {
                 string ex = System.IO.Path.GetExtension(STYLE_IMAGE_FILE_NAME);
                 if (string.Compare(ex, STYLE_IMAGE_EXTENSION, true) != 0) continue;
             
 
                 string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                 if(fileName.Length <  MIN_STYLE_CODE_LENGTH) continue;

                 if (fileName.IndexOf('_') < 0) continue;

                 string cName = isOcxCode? fileName.Replace("_", ""):fileName.Substring(0, MIN_STYLE_CODE_LENGTH);
                 
                 if(string.Compare(styleCode,cName, true) == 0)
                     return true;
             }
             return false;
        }


    }
}
