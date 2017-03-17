using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WcfService {
    /// <summary>
    /// 标记并配置是否需要承载为WCF 服务。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WcfServiceHostAttribute : System.Attribute {
        /// <summary>
        /// 标记并配置是否需要承载为WCF 服务。
        /// </summary>
        /// <param name="relativePath">WCF 服务启动时需要相对相对路径</param>
        /// <param name="bindingType">WCF 服务启动绑定的类型</param>
        public WcfServiceHostAttribute(Type contractType) {
            _ContractType = contractType;
        }
        private Type _ContractType;
        /// <summary>
        /// WCF 服务启动时需要相对相对路径，默认情况下为WCF 接口的完整路径名称。
        /// </summary>
        public Type ContractType {
            get { return _ContractType; }
            set { _ContractType = value; }
        }
        private BindingsType _BindingsType;
        /// <summary>
        /// WCF 服务启动绑定的类型
        /// </summary>
        public BindingsType BindingsType {
            get { return _BindingsType; }
            set { _BindingsType = value; }
        }

    }

    /// <summary>
    /// 服务启动绑定的类型。
    /// </summary>
    public enum BindingsType {
        BasicHttp,
        wsHttp,
        netTcp
    }
}
