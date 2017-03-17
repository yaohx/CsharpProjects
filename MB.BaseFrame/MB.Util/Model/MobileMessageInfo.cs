using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Model
{
    /// <summary>
    ///短信消息描述信息
    /// </summary>
    public class MobileMessageInfo : ICloneable
    {
        private string _Autor;
        private string _Content;
        private string _Mobile;
        private string _Pri;
        private string _ToName;

        private static readonly string MSG_FORMATE_STR = "\"autor\":\"{0}\",\"content\":\"{1}\",\"mobile\":\"{2}\",\"pri\":\"{3}\",\"toname\":\"{4}\"";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">短信内容</param>
        /// <param name="mobile">电话号码</param>
        public MobileMessageInfo(string content, string mobile) {
            _Content = content;
            _Mobile = mobile;
            _Autor = "";
            _Pri = "0";
            _ToName = "";
        }
        /// <summary>
        /// 转换为
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            string val = string.Format(MSG_FORMATE_STR, _Autor, _Content, _Mobile, _Pri, _ToName);
            return "{" + val + "}";
        }

        #region public 属性...
        /// <summary>
        /// 作者
        /// </summary>
        public string Autor {
            get {
                return _Autor;
            }
            set {
                _Autor = value;
            }
        }
        /// <summary>
        /// 短信内容。
        /// </summary>
        public string Content {
            get {
                return _Content;
            }
            set {
                _Content = value;
            }
        }
        /// <summary>
        /// 手机电话。
        /// </summary>
        public string Mobile {
            get {
                return _Mobile;
            }
            set {
                _Mobile = value;
            }
        }
        /// <summary>
        /// 优先级。
        /// </summary>
        public string Pri {
            get {
                return _Pri;
            }
            set {
                _Pri = value;
            }
        }
        /// <summary>
        /// 对方名称
        /// </summary>
        public string ToName {
            get {
                return _ToName;
            }
            set {
                _ToName = value;
            }
        }
        #endregion public 属性...

        public object Clone() {
            return this.MemberwiseClone();
        }
    }
}
