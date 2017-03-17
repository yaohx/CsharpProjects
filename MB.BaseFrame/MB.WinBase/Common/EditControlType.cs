using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.Atts;
namespace MB.WinBase.Common
{
    /// <summary>
    /// 动态控件创建类型绑定。
    /// </summary>    
    public enum EditControlType
    {
        /// <summary>
        /// 不设置
        /// </summary>
        None,
        /// <summary>
        /// 文本输入框
        /// </summary>
        [EditControlType(typeof(TextBox))]
        TextBox,
        /// <summary>
        /// 下拉可编辑列表框
        /// </summary>
        [EditControlType(typeof(ComboBox))]
        Combox_DropDown,
        /// <summary>
        ///  下拉不可编辑列表框。
        /// </summary>
        [EditControlType(typeof(ComboBox))]
        Combox_DropDownList,
        /// <summary>
        /// 下拉不可编辑并且根据代码查找名称。
        /// </summary>
        [EditControlType(typeof(ComboBox))]
        LookUpEdit,
        /// <summary>
        /// 下拉多选编辑控件
        /// </summary>
        [EditControlType(typeof(MB.WinBase.Ctls.ucComboCheckedListBox))]
        ComboCheckedListBox,
        /// <summary>
        /// 选择输入框
        /// </summary>
        [EditControlType(typeof(CheckBox))]
        CheckBox,
        /// <summary>
        /// 日期过滤查找编辑框。
        /// </summary>
        [EditControlType(typeof(MB.WinBase.Ctls.ucEditDateFilter))]
        DateFilterCtl,

        /// <summary>
        ///  单击选择输入框。
        /// </summary>
        [EditControlType(typeof(MB.WinBase.Ctls.ucClickButtonInput))]
        ClickButtonInput,
        /// <summary>
        /// 图标编辑控件。
        /// </summary>
        [EditControlType(typeof(MB.WinBase.Ctls.ucIamgeIcoEdit))]
        ImageIcoEdit,
        /// <summary>
        /// 图片编辑控件。
        /// </summary>
        [EditControlType(typeof(MB.WinBase.Ctls.ucDbPictureBox))]
        DBPictureBox,
        /// <summary>
        ///  数字输入编辑狂。
        /// </summary>
        [EditControlType(typeof(NumericUpDown))]
        NumericUpDown,
        /// <summary>
        /// 颜色编辑。
        /// </summary>
        ColorEdit,
        /// <summary>
        /// 富文本框编辑类型。
        /// </summary>
        [EditControlType(typeof(MB.WinBase.Ctls.RichTextBoxEx))]
        RichTextBox,

        /// <summary>
        /// 富选择框
        /// </summary>
        [EditControlType(typeof(MB.WinBase.Ctls.ucPopupRegionEdit))]
        PopupRegionEdit,
    }
}
