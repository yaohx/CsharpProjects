//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-09-11
// Description	:	动态列 绑定相关处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MB.WinBase.Common;
namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 动态列 绑定相关处理。
    /// </summary>
    public class DynamicColumnBandGridHelper<T> {

        /// <summary>
        /// 创建动态列并带有动态Caption 的描述信息。
        /// </summary>
        /// <param name="convertObject"></param>
        /// <param name="gridViewLayoutInfo"></param>
        public GridViewLayoutInfo ResetDynamicCaptionColumnViewLayout(MB.WinBase.Data.HViewDataConvert<T> convertObject, GridViewLayoutInfo gridViewLayoutInfo) {
            GridColumnLayoutInfo settingBand = getDynamicBandSetting(gridViewLayoutInfo.GridLayoutColumns);
            if (settingBand == null || settingBand.Childs.Count == 0) return gridViewLayoutInfo;
            var dynamicColumns = convertObject.DynamicColumns;

            GridColumnLayoutInfo settingClone = settingBand.Childs[0].Clone() as GridColumnLayoutInfo;
            settingBand.Childs.Clear();
            for (int index = 0; index < convertObject.DynamicColumnCount; index ++ ) {
                GridColumnLayoutInfo dynamicCol = (GridColumnLayoutInfo)settingClone.CloneWithoutChilds();
                settingBand.Childs.Add(dynamicCol);
                if (string.Compare(dynamicCol.Type, XtraGridViewHelper.BAND_TYPE_NAME, true) == 0) {
                    dynamicCol.Childs = new List<GridColumnLayoutInfo>();
                    foreach (GridColumnLayoutInfo child in settingClone.Childs) {
                        GridColumnLayoutInfo childColumn = child.CloneWithoutChilds();
                        childColumn.Name = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName(childColumn.Name, index);
                        dynamicCol.Childs.Add(childColumn);
                    }
                }
                else {
                    dynamicCol.Name = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName(dynamicCol.Name, index);
                }
                //dynamicCol.Text = colInfo.Caption;
                dynamicCol.Index = dynamicCol.Index + index;
            }
            return gridViewLayoutInfo;
        }
        //
        /// <summary>
        ///  重新设置动态GridLayoutInfo 的信息。
        /// </summary>
        /// <param name="convertObject"></param>
        /// <param name="gridViewLayoutInfo"></param>
        /// <returns></returns>
        public GridViewLayoutInfo ResetDynamicColumnViewLayout(MB.WinBase.Data.HViewDataConvert<T> convertObject, GridViewLayoutInfo gridViewLayoutInfo) {
            List<GridColumnLayoutInfo> settingBands = new List<GridColumnLayoutInfo>();
            getDynamicBandSettings(gridViewLayoutInfo.GridLayoutColumns, ref settingBands);
            //GridColumnLayoutInfo settingBand = getDynamicBandSetting(gridViewLayoutInfo.GridLayoutColumns);

            foreach (GridColumnLayoutInfo settingBand in settingBands) {
                if (settingBand == null || settingBand.Childs.Count == 0) continue;
                var dynamicColumns = convertObject.DynamicColumns;
                int index = 0;
                GridColumnLayoutInfo settingClone = settingBand.Childs[0].Clone() as GridColumnLayoutInfo;
                settingBand.Childs.Clear();
                for (int colIndex = 0; colIndex < dynamicColumns.Count; colIndex++) {
                    MB.WinBase.Data.DynamicColumnInfo colInfo = dynamicColumns[colIndex];
                    GridColumnLayoutInfo dynamicCol = (GridColumnLayoutInfo)settingClone.CloneWithoutChilds();
                    settingBand.Childs.Add(dynamicCol);
                    if (string.Compare(dynamicCol.Type, XtraGridViewHelper.BAND_TYPE_NAME, true) == 0) {
                        dynamicCol.Childs = new List<GridColumnLayoutInfo>();
                        foreach (GridColumnLayoutInfo child in settingClone.Childs) {
                            GridColumnLayoutInfo childColumn = child.CloneWithoutChilds();
                            childColumn.Name = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName(childColumn.Name, colIndex);
                            dynamicCol.Childs.Add(childColumn);
                        }
                    }
                    else {
                        dynamicCol.Name = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName(dynamicCol.Name, colIndex);

                    }
                    dynamicCol.Text = string.IsNullOrEmpty(colInfo.Caption) ? colInfo.ColumnValueCode : colInfo.Caption;
                    dynamicCol.Index = dynamicCol.Index + index;

                    index += 1;
                }
            }
            return gridViewLayoutInfo;

        }

        #region 内部函数处理...
        //获取动态配置的GridBand
        private GridColumnLayoutInfo getDynamicBandSetting(List<GridColumnLayoutInfo> childColumnLayouts) {
            foreach (GridColumnLayoutInfo columnLayoutInfo in childColumnLayouts) {
                if (string.Compare(columnLayoutInfo.Type, XtraGridViewHelper.BAND_TYPE_NAME, true) == 0) {
                    if (columnLayoutInfo.DynamicChild) {
                        if (columnLayoutInfo.Childs.Count > 1)
                            throw new MB.Util.APPException("在获取动态列配置时,DynamicChild 的直系子列数只能有一个,如果存在多个列用一个Band 来包含"); 
                        return columnLayoutInfo;
                    }
                    else {
                        GridColumnLayoutInfo info = getDynamicBandSetting(columnLayoutInfo.Childs);
                        if (info != null)
                            return info;
                    }
                }
            }
            return null;
        }

        //获取动态配置的GridBand
        private void getDynamicBandSettings(List<GridColumnLayoutInfo> childColumnLayouts, ref List<GridColumnLayoutInfo> dynamicBandSettings) {
            if (dynamicBandSettings == null)
                dynamicBandSettings = new List<GridColumnLayoutInfo>();

            foreach (GridColumnLayoutInfo columnLayoutInfo in childColumnLayouts) {
                if (string.Compare(columnLayoutInfo.Type, XtraGridViewHelper.BAND_TYPE_NAME, true) == 0) {
                    if (columnLayoutInfo.DynamicChild) {
                        if (columnLayoutInfo.Childs.Count > 1)
                            throw new MB.Util.APPException("在获取动态列配置时,DynamicChild 的直系子列数只能有一个,如果存在多个列用一个Band 来包含");
                        dynamicBandSettings.Add(columnLayoutInfo);
                    }
                    else {
                        getDynamicBandSettings(columnLayoutInfo.Childs, ref dynamicBandSettings);
                    }
                }
            }
        }
        #endregion 内部函数处理...

    }
}
