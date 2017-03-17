using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    ///  参数说明当前正在处理的情况。
    /// </summary>
    public class WorkWaitDialogArgs {
        private object _CurrentProcessContent;
        private bool _Processed;
        private bool _Cancel;

        public WorkWaitDialogArgs() {
        }
        /// <summary>
        ///  参数说明当前正在处理的情况。
        /// </summary>
        /// <param name="_CurrentProcessContent"></param>
        /// <param name="processed"></param>
        public WorkWaitDialogArgs(object currentProcessContent, bool processed) {
            _CurrentProcessContent = currentProcessContent;
            _Processed = processed;
        }
        /// <summary>
        /// 当前正在处理的内容。
        /// </summary>
        public object CurrentProcessContent {
            get {
                return _CurrentProcessContent;
            }
            set {
                _CurrentProcessContent = value;

                System.Threading.Thread.Sleep(100);
            }
        }
        /// <summary>
        /// 判断是否已处理完成。
        /// </summary>
        public bool Processed {
            get {
                return _Processed;
            }
            set {
                _Processed = value;
            }

        }
        /// <summary>
        /// 判断是否为取消。
        /// </summary>
        public bool Cancel {
            get {
                return _Cancel;
            }
            set {
                _Cancel = value;
            }
        }
    }
}
