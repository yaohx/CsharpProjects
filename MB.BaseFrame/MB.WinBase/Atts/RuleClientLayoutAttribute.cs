//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-16
// Description	:	UI 层 FORM 窗口 绑定和显示配置处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 

using MB.WinBase.Common;
namespace MB.WinBase.Atts {
    /// <summary>
    /// UI 层 FORM 窗口 绑定和显示配置处理相关。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RuleClientLayoutAttribute : Attribute {
        private string _UIXmlConfigFile;
        private DataBindingType _UIDataBindingType;
        private CommunicationDataType _CommunicationDataType;
        private GeneralOperateMenus _GeneralMenus;
        private string[] _EntityKeys;

        //add by aifang 2012-08-01 增加客户端数据加载方式， Reload表示
        private MB.WinBase.Common.ClientDataLoadType _LoadType;
        private string _MessageHeaderKey;   //消息头键值
        private bool _IsDynamicGroupEnabled; //用于是否启动动态聚组
        private bool _IsCustomFooterSummary = false; //用于是否对列的自动汇总




        //默认情况下都显示
        private MB.WinBase.Common.DataViewStyle _DataViewStyle = DataViewStyle.General | 
                                                                 DataViewStyle.Multi | 
                                                                 DataViewStyle.Chart |
                                                                 DataViewStyle.ModuleComment ;
        private DefaultDataFilter _DefaultFilter = DefaultDataFilter.None;

        #region 构造函数...
        /// <summary>
        /// 创建Form 的控件绑定和显示配置处理。
        /// 默认情况下:
        /// DataBindingType 通过AutoByCtlName,
        /// 数据传递通过 ModelEntity;
        /// </summary>
        /// <param name="uIXmlConfigFile">显示Form 关联对应的XML 配置文件名称。</param>
        public RuleClientLayoutAttribute(string uIXmlConfigFile)
            : this(uIXmlConfigFile, GeneralOperateMenus.None) {

        }
        /// <summary>
        /// 创建Form 的控件绑定和显示配置处理。
        /// </summary>
        /// <param name="uIXmlConfigFile">显示Form 关联对应的XML 配置文件名称。</param>
        /// <param name="uIDataBindingType">Form 控件绑定创建的方式。</param>
        /// <param name="communicationDataType">层之间数据传递的类型。</param>
        public RuleClientLayoutAttribute(string uIXmlConfigFile, GeneralOperateMenus generalMenus) {
            _UIXmlConfigFile = uIXmlConfigFile;
            _CommunicationDataType = CommunicationDataType.ModelEntity;
            _GeneralMenus = generalMenus;

            _EntityKeys = new string[] { "ID" };
        }
 
        #endregion 构造函数...

        #region public 属性...
        /// <summary>
        /// 绑定业务对象UI 层 XML 绑定的文件名称。
        /// </summary>
        public string UIXmlConfigFile {
            get {
                return _UIXmlConfigFile;
            }
            set {
                _UIXmlConfigFile = value;
            }
        }
        /// <summary>
        /// 通用操作菜单项。
        /// </summary>
        public GeneralOperateMenus GeneralMenus {
            get {
                return _GeneralMenus;
            }
            set {
                _GeneralMenus = value;
            }
        }
        /// <summary>
        /// 编辑界面控件绑定的方式。
        /// </summary>
        public DataBindingType UIDataBindingType {
            get {
                return _UIDataBindingType;
            }
            set {
                _UIDataBindingType = value;
            }
        }
        /// <summary>
        /// 层之间数据传递的类型。
        /// </summary>
        public CommunicationDataType CommunicationDataType {
            get {
                return _CommunicationDataType;
            }
            set {
                _CommunicationDataType = value;
            }
        }
        /// <summary>
        /// 数据浏览的样式。
        /// </summary>
        public MB.WinBase.Common.DataViewStyle DataViewStyle {
            get {
                return _DataViewStyle;
            }
            set {
                _DataViewStyle = value;
            }
        }
        /// <summary>
        ///  默认数据过滤条件设置。
        /// </summary>
        public DefaultDataFilter DefaultFilter {
            get {
                return _DefaultFilter;
            }
            set {
                _DefaultFilter = value;
            }
        }

        /// <summary>
        /// 实体主键
        /// </summary>
        public string[] EntityKeys {
            get { return _EntityKeys; }
            set { _EntityKeys = value; }
        }

        /// <summary>
        /// 客户端数据加载方式
        /// add by aifang 2012-08-01
        /// </summary>
        public MB.WinBase.Common.ClientDataLoadType LoadType
        {
            get { return _LoadType; }
            set { _LoadType = value; }
        }

        /// <summary>
        /// 传递动态列时消息头键值
        /// </summary>
        public string MessageHeaderKey
        {
            get { return _MessageHeaderKey; }
            set { _MessageHeaderKey = value; }
        }

        /// <summary>
        /// 动态聚组的配置信息
        /// </summary>
        public bool IsDynamicGroupEnabled
        {
            get { return _IsDynamicGroupEnabled; }
            set { _IsDynamicGroupEnabled = value; }
        }

        /// <summary>
        /// 是否开启Grid Footer自定义汇总
        /// </summary>
        public bool IsCustomFooterSummary {
            get { return _IsCustomFooterSummary; }
            set { _IsCustomFooterSummary = value; }
        }
        #endregion public 属性...
    }


    #region DefaultDataFilter...
    /// <summary>
    /// 默认数据过滤条件设置。
    /// </summary>
    public enum DefaultDataFilter {
        /// <summary>
        /// 默认情况下不显示任何数据。
        /// </summary>
        [Description("不显示任何数据")] 
        None,
        /// <summary>
        /// 显示所有,不做任何设置
        /// </summary>
        [Description("显示所有数据")]
        All,
        /// <summary>
        /// 显示所有正在处理未提交的数据。
        /// </summary>
        [Description("显示所有正在录入中的数据")]
        Process,
        /// <summary>
        /// 显示当天有变动的数据。
        /// </summary>
        [Description("显示当天有变动的数据")]
        Today,
        /// <summary>
        /// 显示一个星期内处理过的数据。
        /// </summary>
        [Description("显示一周之内有变动的数据")]
        WithinOfWeek,
        /// <summary>
        /// 显示一个月内处理过的数据。
        /// </summary>
        [Description("显示一个月之内有变动的数据")]
        WithinOfMonth,
        ///// <summary>
        ///// 正在处理和一个星期内处理过的数据。
        ///// </summary>
        //ProcessAndWeek,
        ///// <summary>
        ///// 正在处理和一个月内处理过的数据。
        ///// </summary>
        //ProcessAndMonth,
        ///// <summary>
        ///// 正在处理和允许连动编辑的数据。
        ///// 注意： 允许连动编辑的设置必须包含主键 ID (目前还没有进行个性化处理)
        ///// </summary>
        //ProcessAndAllowLink
    }
    #endregion DefaultDataFilter...

}
