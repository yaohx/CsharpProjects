using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 创建LookUpEditItem 编辑项
    /// </summary>
    class XtraRepositoryItemLookUpEditHelper {
       /// <summary>
       /// 
       /// </summary>
        public static XtraRepositoryItemLookUpEditHelper Instance {
            get {
                return MB.Util.SingletonProvider<XtraRepositoryItemLookUpEditHelper>.Instance; 
            }
        }

        /// <summary>
        ///  创建LookUP 编辑项
        /// </summary>
        /// <param name="lookUpItem"></param>
        /// <param name="editInfo"></param>
        public void CreateLookUpEditItems(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpItem, MB.WinBase.Common.ColumnEditCfgInfo editInfo, string colDataType) {
            if (editInfo.DataSource == null) return;;
            DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(editInfo.DataSource, string.Empty);
            

            if (dtData == null)
                return;
            
            //Copy一个新的DataTable作为数据源，这样就不影响原来的数据源
            DataTable newData = dtData.Copy();

            DevExpress.XtraEditors.Controls.LookUpColumnInfo[] lookUpCols = null;
            if (editInfo.LookUpColumns != null && editInfo.LookUpColumns.Count > 0) {
                lookUpCols = new DevExpress.XtraEditors.Controls.LookUpColumnInfo[editInfo.LookUpColumns.Count];
                for (int i = 0; i < editInfo.LookUpColumns.Count; i++) {
                    MB.WinBase.Common.LookUpColumnInfo cInfo = editInfo.LookUpColumns[i] as MB.WinBase.Common.LookUpColumnInfo;
                    lookUpCols[i] = new DevExpress.XtraEditors.Controls.LookUpColumnInfo(cInfo.FieldName, cInfo.Description,
                                                    cInfo.ShowWidth, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near);
                }
            }
            else {
                lookUpCols = new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
																				new DevExpress.XtraEditors.Controls.LookUpColumnInfo(editInfo.TextFieldName ,
                                                                                "请选择", 38, DevExpress.Utils.FormatType.None, "", true, 
                                                                                    DevExpress.Utils.HorzAlignment.Near)};
                lookUpItem.ShowHeader = false;
            }
            lookUpItem.AutoHeight = true;
            lookUpItem.NullText = "";
            lookUpItem.PopupSizeable = true;
            lookUpItem.ShowLines = true;
            lookUpItem.ShowPopupShadow = true;
            lookUpItem.Columns.AddRange(lookUpCols);
            lookUpItem.ValueMember = editInfo.ValueFieldName;
            lookUpItem.DisplayMember = editInfo.TextFieldName;
            //lookUpItem.ReadOnly = true;
            // lookUpItem.DropDownRows = 12;
            lookUpItem.PopupWidth = 100;

            //第二个判断是为了支持向下兼容的API，因为以前的API中是不会穿入colDataType的，在向下兼容的接口中传入默认值
            //XtraGridEditHelper.COL_UNKNOW_DATE_TYPE_FOR_CREATING,在这种情况下，不支持空值
            if (editInfo.InsertNullItem && 
                string.Compare(colDataType, XtraGridEditHelper.COL_UNKNOW_DATE_TYPE_FOR_CREATING) != 0) 
            {
                DataRow emptyRow = newData.NewRow();
                if (colDataType.IndexOf('?') >= 0)
                    emptyRow[editInfo.ValueFieldName] = null;
                if (string.Compare(colDataType, "System.String", true) == 0)
                    emptyRow[editInfo.ValueFieldName] = string.Empty;
                else if (string.Compare(colDataType, "System.Int32", true) == 0 || string.Compare(colDataType, "System.Decimal", true) == 0)
                    emptyRow[editInfo.ValueFieldName] = 0;

                newData.Rows.InsertAt(emptyRow, 0);
                lookUpItem.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                lookUpItem.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            }

            lookUpItem.DataSource = newData.DefaultView;
        }
    }
}
