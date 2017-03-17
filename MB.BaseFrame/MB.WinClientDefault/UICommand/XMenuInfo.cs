using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace MB.WinClientDefault.UICommand {
    /// <summary>
    /// 菜单项描述信息。
    /// </summary>
    public class XMenuInfo {
        private CommandID _CommandID;
        private string _Description;
        private int _ImageIndex;
        private bool _BeginGroup;
        private XMenuInfo[] _Childs;
        private int _Index;
        private Shortcut _Shortcut;
        private bool _ShowToCommandBar;
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="description"></param>
        public XMenuInfo(CommandID commandID, string description, int index)
            : this(commandID, description, -1, false, index, Shortcut.None) {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="description"></param>
        /// <param name="imageIndex"></param>
        /// <param name="beginGroup"></param>
        /// <param name="index"></param>
        public XMenuInfo(CommandID commandID, string description, int imageIndex, bool beginGroup, int index) : this(
            commandID, description, -1, beginGroup, index, Shortcut.None) {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="description"></param>
        public XMenuInfo(CommandID commandID, string description, int imageIndex,bool beginGroup,int index,Shortcut shortCut) {
            _CommandID = commandID;
            _Description = description;
            _ImageIndex = imageIndex;
            _BeginGroup = beginGroup;
            _Index = index;
            _Shortcut = shortCut;
            if (_ImageIndex < 0)
                _ImageIndex = MB.WinClientDefault.Images.ImageListHelper.Instance.GetImageIndexByCommandID(_CommandID); 
        }
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="childsMenu"></param>
        public XMenuInfo(string description, XMenuInfo[] childsMenu, int index,bool showToCommandBar) {
            _Description = description;
            _Childs = childsMenu;
            _Index = index;
            _ShowToCommandBar = showToCommandBar;
        }
        #region public 属性...
        /// <summary>
        /// 命令标记符。
        /// </summary>
        public CommandID CommandID {
            get {
                return _CommandID;
            }
            set {
                _CommandID = value;
            }
        }
        /// <summary>
        /// 命令描述。
        /// </summary>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        /// <summary>
        /// 命令对应的Image .
        /// </summary>
        public int ImageIndex {
            get {
                return _ImageIndex;
            }
            set {
                _ImageIndex = value;
            }
        }
        /// <summary>
        /// 判断是否从该项开始分组。
        /// </summary>
        public bool BeginGroup {
            get {
                return _BeginGroup;
            }
            set {
                _BeginGroup = value;
            }
        }
        /// <summary>
        /// 子菜单。
        /// </summary>
        public XMenuInfo[] Childs {
            get {
                return _Childs;
            }
            set {
                _Childs = value;
            }
        }
        /// <summary>
        /// 顺序。
        /// </summary>
        public int Index {
            get {
                return _Index;
            }
            set {
                _Index = value;
            }
        }
        /// <summary>
        /// 判断是否显示在工具栏在。
        /// </summary>
        public bool ShowToCommandBar {
            get {
                return _ShowToCommandBar;
            }
            set {
                _ShowToCommandBar = value;
            }
        }
        /// <summary>
        /// 菜单short cut.
        /// </summary>
        public Shortcut Shortcut {
            get {
                
                return _Shortcut;
            }
            set {
                _Shortcut = value;
            }
        }
        #endregion public 属性...
    }
}

