using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace
{
    public interface IViewDynamicGroupGridForm : IViewGridForm
    {
        /// <summary>
        /// 动态聚组是否被激活
        /// </summary>
        bool IsDynamicGroupIsActive { get; set; }
        /// <summary>
        /// 动态聚组的条件设定
        /// </summary>
        MB.Util.Model.DynamicGroupSetting DynamicGroupSettingForQuery { get; set; }
        /// <summary>
        /// 打开动态聚组设计的界面
        /// </summary>
        void OpenDynamicSettingForm();

    }
}
