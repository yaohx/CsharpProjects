using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MB.WinBase.IFace;
using MB.XWinLib.XtraGrid;

namespace MB.XWinLib.Share
{
    /// <summary>
    /// 网格控件布局管理。
    /// </summary>
    internal class GridLayoutManager
    {
        private static readonly string GRID_LAYOUT_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"UserSetting\";
        private static readonly string GRID_LAYOUT_FILE_SETTING_FULLNAME = GRID_LAYOUT_FILE_PATH + "GridLayoutSetting.xml";

        /// <summary>
        /// 保存XtraGrid 控件的UI 操作状态。
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public static  void SaveXtraGridState(DevExpress.XtraGrid.GridControl xtraGrid) {
            var gridLayoutInfo = GetXtraGridActiveLayout(xtraGrid);
            if (gridLayoutInfo == null) return;
            SaveXtraGridState(xtraGrid, gridLayoutInfo);
        }

        /// <summary>
        /// 恢复XtraGrid 控件的UI 操作保存状态。 
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public static void RestoreXtraGridState(DevExpress.XtraGrid.GridControl xtraGrid) {
            var gridLayoutInfo = GetXtraGridActiveLayout(xtraGrid);
            if (gridLayoutInfo == null) return;

            RestoreXtraGridState(xtraGrid, gridLayoutInfo);
        }

        /// <summary>
        /// 保存XtraGrid 控件的UI 操作状态。
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public static void SaveXtraGridState(DevExpress.XtraGrid.GridControl xtraGrid,GridLayoutInfo gridLayOutInfo)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null)
                return;

            string sectionName = GetXtraGridLayoutSectionName(xtraGrid);
            if (string.IsNullOrEmpty(sectionName)) return;

            gridView.SaveLayoutToXml(GRID_LAYOUT_FILE_PATH + sectionName + gridLayOutInfo.Name + ".xml");
        }

        /// <summary>
        /// 恢复XtraGrid 控件的UI 操作保存状态。 
        /// </summary>
        /// <param name="xtraGCtl"></param>
        public static void RestoreXtraGridState(DevExpress.XtraGrid.GridControl xtraGrid, GridLayoutInfo gridLayOutInfo)
        {
            try
            {
                string sectionName = GetXtraGridLayoutSectionName(xtraGrid);
                if (string.IsNullOrEmpty(sectionName)) return;

                if (System.IO.File.Exists(GRID_LAYOUT_FILE_PATH + sectionName + gridLayOutInfo.Name + ".xml"))
                {
                    //add by aifang 2012-08-13 begin
                    //判断状态保存日期是否大于动态列设置日期，如是，则生效，否则不生效。
                    DateTime dt = gridLayOutInfo.CreateTime;
                    var clientRule = getXtraGridClientRule(xtraGrid);
                    var dynamicSetting = XtraGridDynamicHelper.Instance.GetXtraGridDynamicSettingInfo(clientRule);
                    if (dynamicSetting != null)
                    {
                        if (dynamicSetting.LastModifyDate.Subtract(dt).TotalMilliseconds > 0) return;
                    }
                    //end

                    DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                    gridView.OptionsLayout.Columns.RemoveOldColumns = true;
                    gridView.RestoreLayoutFromXml(GRID_LAYOUT_FILE_PATH + sectionName + gridLayOutInfo.Name + ".xml");
                }
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("恢复XtrGrid布局失败，错误信息为：{0}", ex.Message));
            }
        }

        public static void DeleteXtraGridState(DevExpress.XtraGrid.GridControl xtraGrid, GridLayoutInfo gridLayOutInfo)
        {
            string sectionName = GetXtraGridLayoutSectionName(xtraGrid);
            if (string.IsNullOrEmpty(sectionName)) return;

            if (System.IO.File.Exists(GRID_LAYOUT_FILE_PATH + sectionName + gridLayOutInfo.Name + ".xml"))
            {
                System.IO.File.Delete(GRID_LAYOUT_FILE_PATH + sectionName + gridLayOutInfo.Name + ".xml");
            }
        }

        //add by aifang 2012-5-16
        public static string GetXtraGridLayoutSectionName(DevExpress.XtraGrid.GridControl xtraGrid)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null)
                return string.Empty;

            string sectionName = string.Empty;
            MB.XWinLib.XtraGrid.GridControlEx ctlEx = xtraGrid as MB.XWinLib.XtraGrid.GridControlEx;
            if (ctlEx != null && ctlEx.InstanceIdentity != Guid.Empty)
            {
                sectionName = ctlEx.InstanceIdentity.ToString();
            }
            else
            {
                System.Windows.Forms.Form frm = MB.WinBase.ShareLib.Instance.GetControlParentForm(xtraGrid);
                if (frm == null)
                {
                    return string.Empty;
                }
                else
                {
                    sectionName = frm.GetType().FullName + " " + xtraGrid.Name;

                    MB.WinBase.IFace.IForm busFrm = frm as MB.WinBase.IFace.IForm;
                    if (busFrm != null && busFrm.ClientRuleObject != null)
                    {
                        sectionName = frm.GetType().FullName + " " + busFrm.ClientRuleObject.GetType().FullName + " "
                            + busFrm.ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile + " " + xtraGrid.Name;
                    }
                }
            }

            return sectionName;
        }

        private static GridLayoutInfo GetXtraGridActiveLayout(DevExpress.XtraGrid.GridControl xtraGrid)
        {
            try {
                var gridLayoutMainList = new MB.Util.Serializer.DataContractFileSerializer<List<GridLayoutMainInfo>>(GRID_LAYOUT_FILE_SETTING_FULLNAME).Read();
                if (gridLayoutMainList == null) return null;

                string sectionName = GetXtraGridLayoutSectionName(xtraGrid);
                var gridLayoutList = gridLayoutMainList.Find(o => o.GridSectionName.Equals(sectionName));
                if (gridLayoutList == null || gridLayoutList.GridLayoutList.Count == 0) return null;

                return gridLayoutList.GridLayoutList.OrderByDescending(o => o.CreateTime).FirstOrDefault();
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write(ex.Message, Util.APPMessageType.SysErrInfo);
                return null;
            }
        }

        private static IClientRuleQueryBase getXtraGridClientRule(DevExpress.XtraGrid.GridControl xtraGrid)
        {
            try
            {
                System.Windows.Forms.Form frm = MB.WinBase.ShareLib.Instance.GetControlParentForm(xtraGrid);
                if (frm == null)
                {
                    return null;
                }
                else
                {
                    MB.WinBase.IFace.IForm busFrm = frm as MB.WinBase.IFace.IForm;
                    if (busFrm != null && busFrm.ClientRuleObject != null)
                    {
                        return busFrm.ClientRuleObject;
                    }
                }              
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("获取XtraGrid对应的业务类失败，错误信息为：{0}",ex.Message));
            }
            return null;
        }
    }
}
