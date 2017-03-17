using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Atts
{
    /// <summary>
    /// 模块数据过滤窗口配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SimpleDataFilterControlAttribute : Attribute
    {
        private Type _FormControlType;
        public SimpleDataFilterControlAttribute(Type formCtlType)
        {
            _FormControlType = formCtlType;
        }

        public Type FormControlType {
            get {
                return _FormControlType;
            }
            set {
                _FormControlType = value;
            }
        }
    }
}
