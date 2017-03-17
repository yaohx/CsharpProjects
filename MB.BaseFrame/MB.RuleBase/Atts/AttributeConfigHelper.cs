using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.RuleBase.Atts;
namespace MB.RuleBase.Atts {
    /// <summary>
    /// AttributeConfigHelper: 提供基于Attribute 属性配置的公共方法。
    /// </summary>
    public sealed class AttributeConfigHelper {
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
        #region public 成员...

        /// <summary>
       /// 根据业务类获取对应的业务类属性配置信息。
       /// </summary>
       /// <param name="busObj">业务操作类型</param>
       /// <returns>业务类的配置信息。</returns>
        public RuleSettingAttribute GetRuleSettingAtt(MB.RuleBase.IFace.IBaseRule busObj) {
            RuleSettingAttribute att = Attribute.GetCustomAttribute(busObj.GetType(),typeof(RuleSettingAttribute)) as RuleSettingAttribute;
            if (att==null) {
                return null;
            }
            return att;
        }
        /// <summary>
        /// 根据类型获取在单据类数据类型对应的配置。
        /// </summary>
        /// <param name="objTypeValue">在单据中定义的数据类型。</param>
        /// <returns></returns>
        public ObjectDataMappingAttribute GetObjectDataMappingAttribute(object objTypeValue) {
            System.Reflection.MemberInfo[] infos = objTypeValue.GetType().GetMember(objTypeValue.ToString());
            if (infos == null || infos.Length == 0)
                return null;

            ObjectDataMappingAttribute att = Attribute.GetCustomAttribute(infos[0], typeof(ObjectDataMappingAttribute)) as ObjectDataMappingAttribute;
            if (att == null) {
                return null;
            }
            return att;
        }
        /// <summary>
        /// 父子对象关系属性配置。
        /// </summary>
        /// <param name="objTypeValue">用户定义的单据所包含的数据类型。</param>
        /// <returns></returns>
        public ObjectRelationAttribute GetObjectRelationAttByType(object objTypeValue) {
            System.Reflection.MemberInfo[] infos = objTypeValue.GetType().GetMember(objTypeValue.ToString());
            if (infos == null || infos.Length == 0)
                return null;

            ObjectRelationAttribute att = Attribute.GetCustomAttribute(infos[0], typeof(ObjectRelationAttribute)) as ObjectRelationAttribute;
            if (att==null) {
                return null;
            }
            return att;
        }
        /// <summary>
        /// 获取上级对象引用关系配置信息。
        /// </summary>
        /// <param name="objTypeValue"></param>
        /// <returns></returns>
        public ParentProviderAttribute GetParentProviderAttByType(object objTypeValue) {
            System.Reflection.MemberInfo[] infos = objTypeValue.GetType().GetMember(objTypeValue.ToString());
            if (infos == null || infos.Length == 0)
                return null;

            ParentProviderAttribute att = Attribute.GetCustomAttribute(infos[0], typeof(ParentProviderAttribute)) as ParentProviderAttribute;
            if (att == null) {
                return null;
            }
            return att;
        }
        /// <summary>
        /// 获取下级对象引用关系配置信息。
        /// </summary>
        /// <param name="objTypeValue"></param>
        /// <returns></returns>
        public NextOwnAttribute[] GetNextOwnAttByType(object objTypeValue) {
            System.Reflection.MemberInfo[] infos = objTypeValue.GetType().GetMember(objTypeValue.ToString());
            if (infos == null || infos.Length == 0)
                return null;

            List<NextOwnAttribute> otts = new List<NextOwnAttribute>();
            object[] pros = infos[0].GetCustomAttributes(false);
            foreach (object objAtt in pros) {
                if (objAtt.GetType().Equals(typeof(NextOwnAttribute))) {
                    otts.Add(objAtt as NextOwnAttribute);
                }
            }
            return otts.ToArray() ;
        }
        #endregion public 成员...
    }
}
