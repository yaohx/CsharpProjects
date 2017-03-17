using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MB.WinBase {
    /// <summary>
    /// 当前应用程序的
    /// </summary>
    public class AppEnvironmentSetting {
        #region Instance...
        private static Object _Obj = new object();
        private static AppEnvironmentSetting _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        public AppEnvironmentSetting() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static AppEnvironmentSetting Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new AppEnvironmentSetting();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        private MB.Util.Model.SysLoginUserInfo _CurrentLoginUserInfo;
        private List<MB.Util.Model.SysUserPrivInfo> _CurrentUserPrivs;

        /// <summary>
        /// 当前登录用户的信息。
        /// </summary>
        public MB.Util.Model.SysLoginUserInfo CurrentLoginUserInfo {
            get {
                return _CurrentLoginUserInfo;
            }
            set {
                _CurrentLoginUserInfo = value;
            }
        }
        /// <summary>
        /// 当前登录用户的权限信息。
        /// </summary>
        public List<MB.Util.Model.SysUserPrivInfo> CurrentUserPrivs {
            get {
                return _CurrentUserPrivs;
            }
            set {
                _CurrentUserPrivs = value;
            }
        }
        /// <summary>
        /// 转换系统参数值。
        /// </summary>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public string ConvertSystemParamValue(string paramValue) {
             //判断是否为系统当前用户
            if (string.Compare(paramValue, MB.BaseFrame.SOD.PARAM_CURRENT_USER_ID, true) == 0)
                return _CurrentLoginUserInfo.USER_ID;
            else
                return paramValue;
        }
    }
}
