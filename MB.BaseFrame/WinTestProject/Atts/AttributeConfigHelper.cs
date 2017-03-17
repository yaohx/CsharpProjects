using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinTestProject.Atts {
    /// <summary>
    /// AttributeConfigHelper: 提供基于Attribute 属性配置的公共方法。
    /// </summary>
    public sealed class AttributeConfigHelper {

        #region Instance...
        private static Object _Obj = new object();
        private static AttributeConfigHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected AttributeConfigHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static AttributeConfigHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new AttributeConfigHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region public 成员...
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public EditControlTypeAttribute GetEditControlTypeAtt(object enumValue) {
            System.Reflection.MemberInfo[] infos = enumValue.GetType().GetMember(enumValue.ToString());
            if (infos == null || infos.Length == 0)
                return null;

            EditControlTypeAttribute att = Attribute.GetCustomAttribute(infos[0], typeof(EditControlTypeAttribute)) as EditControlTypeAttribute;
            if (att == null) {
                return null;
            }
            return att;
        }

        /// <summary>
        /// 根据业务类获取对应的业务类属性配置信息。
        /// </summary>
        /// <param name="clientRule">客户端业务</param>
        /// <returns>业务类的配置信息。</returns>
        public RuleClientLayoutAttribute GetClientRuleSettingAtt(MB.WinBase.IFace.IClientRuleConfig clientRule) {
            RuleClientLayoutAttribute att = Attribute.GetCustomAttribute(clientRule.GetType(), typeof(RuleClientLayoutAttribute)) as RuleClientLayoutAttribute;
            if (att == null) {
                return null;
            }
            return att;
        }
        #endregion public 成员...
    }
}
