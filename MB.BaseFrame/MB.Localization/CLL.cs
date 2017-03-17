//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	ResourceLib 多语言资源文件处理。 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.Globalization;


#region 通过代码编写的中文获取当前运行的语言...
namespace MB.BaseFrame {
    /// <summary>
    /// 通过指定的文本转换为系统当前运行需要的语言版本。
    /// 针对没有文本ID,直接文本翻译处理相关。
    /// </summary>
    public class CLL {

        /// <summary>
        /// add private construct function to prevent instance.
        /// </summary>
        private CLL() {
        }
        #region 扩展的public 静态方法...
        /// <summary>
        /// 通过指定的文本转换为系统当前运行需要的语言。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Convert(string text, params string[] paramValues) {
            return MB.Localization.TextResource.Convert(text, paramValues);
        }
        /// <summary>
        /// 通过指定的文本转换为系统当前运行需要的语言。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public static string Message(string text, params string[] paramValues) {
            return MB.Localization.MessagesResource.Instance.Convert(text, paramValues);
        }
        /// <summary>
        /// 通过指定的文本转换为系统当前运行需要的语言。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public static string ExceptionMessage(string text, params string[] paramValues) {
            return MB.Localization.MessagesResource.Instance.Convert(text, paramValues);
        }
        /// <summary>
        /// 根据指定的语言设置指定控件的文本描述。
        /// </summary>
        /// <param name="ctls"></param>
        public static void SetControlTextLanguage(System.Windows.Forms.Control.ControlCollection ctls) {
            bool isCn = CultureInfoIsCN();
            if (isCn) return;
            string txt = string.Empty;
            foreach (Control ctl in ctls) {
                txt = ctl.Text;
                if (ctl is System.Windows.Forms.LinkLabel) {
                    ctl.Text = getIncludeBracketText(txt);
                }
                else if (ctl is System.Windows.Forms.Label || ctl is System.Windows.Forms.GroupBox) {
                    ctl.Text = getIncludeLeftSignText(txt);
                }
                else if (ctl is System.Windows.Forms.Button) {
                    ctl.Text = getIncludeShortcutText(txt);
                }
                else if (ctl is System.Windows.Forms.TabControl) {
                    System.Windows.Forms.TabControl tabCtl = ctl as System.Windows.Forms.TabControl;
                    foreach (System.Windows.Forms.TabPage page in tabCtl.TabPages)
                        page.Text = getIncludeShortcutText(page.Text);
                }
                else if (ctl is System.Windows.Forms.ToolBar) {
                    System.Windows.Forms.ToolBar tBar = ctl as System.Windows.Forms.ToolBar;
                    foreach (System.Windows.Forms.ToolBarButton butBar in tBar.Buttons) {
                        if (butBar.Text == "-" || butBar.Text.Length == 0) continue;
                        butBar.Text = getIncludeShortcutText(butBar.Text);
                    }
                }
                else if (ctl is System.Windows.Forms.MenuStrip) {
                    System.Windows.Forms.MenuStrip mScrip = ctl as System.Windows.Forms.MenuStrip;
                    foreach (System.Windows.Forms.ToolStripMenuItem item in mScrip.Items) {
                        if (item.Text == "-" || item.Text.Length == 0) continue;
                        item.Text = getIncludeShortcutText(item.Text);

                        foreach (object cItem in item.DropDownItems) {
                            System.Windows.Forms.ToolStripMenuItem childItem = cItem as System.Windows.Forms.ToolStripMenuItem;
                            if (childItem == null) continue;
                            if (childItem.Text == "-" || childItem.Text.Length == 0) continue;
                            childItem.Text = getIncludeShortcutText(childItem.Text);
                        }
                    }
                }
                else if (ctl is System.Windows.Forms.ToolStrip) {
                    System.Windows.Forms.ToolStrip tsStrip = ctl as System.Windows.Forms.ToolStrip;
                    foreach (object obj in tsStrip.Items) {
                        System.Windows.Forms.ToolStripButton sBut = obj as System.Windows.Forms.ToolStripButton;
                        if (sBut == null) continue;
                        if (sBut.ToolTipText == "-" || sBut.ToolTipText.Length == 0) continue;
                        sBut.ToolTipText = getIncludeShortcutText(sBut.ToolTipText);
                    }
                }
                else if (ctl is Panel || ctl is TableLayoutPanel || ctl is GroupBox) {
                    SetControlTextLanguage(ctl.Controls);
                }

            }
        }
        private static void setMenuText(Control ctl) {

        }
        /// <summary>
        /// 判断当前运行语言是否为中文。
        /// </summary>
        /// <returns></returns>
        public static bool CultureInfoIsCN() {
            System.Globalization.CultureInfo curInfo = System.Threading.Thread.CurrentThread.CurrentUICulture;
            string cName = curInfo.Name;
            return cName.Equals(MB.Localization.ResourcesHelper.CHINESS_NODE_NAME); //如果是中文的话直接获取默认值就可以。

        }
        #region 特殊内部函数处理...
        //获取包含特殊符号的文本
        private static string getIncludeBracketText(string bracketText) {
            string txt = bracketText;

            if (txt.IndexOf("(&") > -1) {
                txt = getIncludeShortcutText(txt);
            }
            else {
                txt = MB.Localization.TextResource.Convert(txt);
            }
            return txt;
        }
        //获取包含快捷方式的文本
        private static string getIncludeShortcutText(string shortcutText) {
            string txt = shortcutText;
            if (txt.IndexOf("(&") > -1) {
                int index = txt.IndexOf("(&");
                string st = txt.Substring(index, txt.Length - index);
                txt = txt.Substring(0, index);
                txt = MB.Localization.TextResource.Convert(txt) + st;
            }
            else {
                txt = MB.Localization.TextResource.Convert(txt);
            }
            return txt;
        }
        private static string getIncludeLeftSignText(string leftSignText) {
            string txt = leftSignText;  //: 有双字节和单字节的情况。
            if (txt.IndexOf(":", txt.Length - 1, 1) > -1 || txt.IndexOf("：", txt.Length - 1, 1) > -1) {
                //特殊处理 判断字符窜的后缀是否带有":"
                txt = txt.Replace(":", string.Empty);
                txt = txt.Replace("：", string.Empty);
                txt = MB.Localization.TextResource.Convert(txt) + "：";
            }
            else {
                txt = MB.Localization.TextResource.Convert(txt);
            }
            return txt;
        }
        #endregion 特殊内部函数处理...

        #endregion 扩展的public 静态方法...

    }
}

#endregion 通过代码编写的中文获取当前运行的语言...

namespace MB.Localization {

    #region TextResource...
    /// <summary>
    /// TextResource 多语言文本资源处理。 
    /// </summary>
    public class TextResource {
        #region 变量定义...
        private static readonly string DEFAULT_XML_NAME = "MB.Localization.Text.Resource.xml";
        private static readonly string DEFAULT_PATH = "/Entity/Language/Data";
        private const string LEFT_NAME = "TextLangid_";
        #endregion 变量定义...

        private TextResource() {

        }

        #region 静态方法...
        /// <summary>
        /// 通过指定的文本转换为系统当前运行需要的语言。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Convert(string text, params string[] paramValues) {
            return MB.Localization.ResourcesHelper.GetStringByCN(DEFAULT_XML_NAME, DEFAULT_PATH, text, paramValues);
        }
        /// <summary>
        /// 得到多语言版本中字符描述
        /// </summary>
        /// <param name="textLandid"></param>
        /// <returns></returns>
        public static string GetString(string textLandid) {
            return GetString(textLandid, new string[0] { });
        }

        /// <summary>
        /// 得到多语言版本中字符描述
        /// </summary>
        /// <param name="pKeyName"></param>
        /// <param name="pDefaultValue"></param>
        /// <returns></returns>
        public static string GetString(string textLandid, params string[] paramValues) {
            string id = textLandid;
            if (id.IndexOf(LEFT_NAME) < 0) {
                return id;
            }
            else {
                id = id.Replace(LEFT_NAME, string.Empty);
            }
            return ResourcesHelper.GetString(DEFAULT_XML_NAME, DEFAULT_PATH, id, id, paramValues);
        }
        #endregion 静态方法...
    }
    #endregion TextResource...

    #region MessageResource...
    /// <summary>
    /// MessageResource 系统消息资源处理。
    /// </summary>
    public class MessagesResource {
        #region 变量定义...
        private static readonly string DEFAULT_XML_NAME = "MB.Messages.Resource";
        private static readonly string DEFAULT_PATH = "/Entity/Language/Data";

        private static MessagesResource _Instance;
        #endregion 变量定义...

        public MessagesResource() {
        }
        /// <summary>
        /// 获取消息资源文件。
        /// </summary>
        public static MessagesResource Instance {
            get {
                if (_Instance == null)
                    _Instance = new MessagesResource();

                return _Instance;
            }
        }
        #region 实现接口的方法...
        /// <summary>
        /// 通过指定的文本转换为系统当前运行需要的语言。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Convert(string text, params string[] paramValues) {
            return MB.Localization.ResourcesHelper.GetStringByCN(DEFAULT_XML_NAME, DEFAULT_PATH, text, paramValues);
        }
        #endregion 实现接口的方法...
    }
    #endregion MessageResource...

}
