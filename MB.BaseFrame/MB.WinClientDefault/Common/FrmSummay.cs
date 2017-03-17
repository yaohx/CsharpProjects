using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MB.WinBase.IFace;
using MB.WinBase;
using MB.WinBase.Common;
using System.Linq;
using System.Collections;
using System.Reflection;
using MB.Util.DynamicLinq;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MB.WinClientDefault.Common {
    /// <summary>
    /// 分级汇总查询窗口
    /// </summary>
    public partial class FrmSummay : AbstractBaseForm {
        //private IClientRuleQueryBase _ClientRule;
        private IList _DataSource;
        private string _XmlFile;

        private SummaryInfo _SummaryInfo; //Form_Load初始化，汇总信息
        private Dictionary<string, ColumnPropertyInfo> _ColProps; //Form_Load初始化，初始列信息

        public FrmSummay(string clientXmlFile, object dataSource) {
            _DataSource = dataSource as IList;
            if (_DataSource == null)
                throw new MB.Util.APPException("汇总数据源只支持 IList", Util.APPMessageType.DisplayToUser);
            //if (_ClientRule == null)
            //    throw new MB.Util.APPException("业务类不能为空", Util.APPMessageType.DisplayToUser);
            _XmlFile = clientXmlFile;
            if (string.IsNullOrEmpty(_XmlFile))
                throw new MB.Util.APPException("客户端UI配置不能为空", Util.APPMessageType.DisplayToUser);
            InitializeComponent();
            
        }

        #region UI事件响应函数

        private void FrmSummay_Load(object sender, EventArgs e) {
            try {
                myFormLoad();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void cbSummaryLevel_SelectedValueChanged(object sender, EventArgs e) {
            try {
                bindToGrid();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 测试用的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInit_Click(object sender, EventArgs e) {
            myFormLoad();
        }

        #endregion

        #region 内部处理函数
        /// <summary>
        /// FormLoad是调用的函数
        /// </summary>
        private void myFormLoad() {

            //if (_ClientRule != null)
            //    _XmlFile = _ClientRule.ClientLayoutAttribute.UIXmlConfigFile;

            _ColProps = LayoutXmlConfigHelper.Instance.GetColumnPropertys(_XmlFile);
            _SummaryInfo = LayoutXmlConfigHelper.Instance.GetSummaryInfo(_XmlFile);
            if (_SummaryInfo == null)
                throw new MB.Util.APPException("请检查<Summary>节点是否配置，读不到配置信息", Util.APPMessageType.DisplayToUser);

            bindToSummaryLevel();
        }

        /// <summary>
        /// 绑定汇总级别
        /// </summary>
        private void bindToSummaryLevel() {
            DataTable dtSummaryLevel = new DataTable();
            dtSummaryLevel.Columns.Add("Description");
            dtSummaryLevel.Columns.Add("Name");
            foreach (var summaryCols in _SummaryInfo.ColumnSummaryInfos.Values) {
                if (summaryCols.IsSummaryCondition) {
                    if (!_ColProps.ContainsKey(summaryCols.ColumnName)) {
                        throw new MB.Util.APPException(string.Format(
                            "汇总列{0}在ColumnPropertyInfo中没有正确配置，或两边的NAME不一致", summaryCols.ColumnName));
                    }

                    DataRow drSummaryLevel = dtSummaryLevel.NewRow();
                    drSummaryLevel["Description"] = summaryCols.Description;
                    drSummaryLevel["Name"] = summaryCols.Name;
                    dtSummaryLevel.Rows.Add(drSummaryLevel);
                }
            }

            this.cbSummaryLevel.DataSource = dtSummaryLevel;
            this.cbSummaryLevel.DisplayMember = "Description";
            this.cbSummaryLevel.ValueMember = "Name";
        }


        /// <summary>
        /// 绑定到Grid
        /// </summary>
        private void bindToGrid() {
            IList dataSource = _DataSource as IList;

            string selectedSummaryLevel = this.cbSummaryLevel.SelectedValue as string;
            if (string.IsNullOrEmpty(selectedSummaryLevel)) {
                return;
            }
            List<GroupByColumn> notSummaryCols = _SummaryInfo.ColumnSummaryInfos[selectedSummaryLevel].IncludeGroupByColumns;
            if (notSummaryCols != null && dataSource.Count > 0) {
                object sampleData = dataSource[0];//当前分类后的对象，取第一个
                Type typeObject = sampleData.GetType(); //得到要创建的对象类型

                Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs = createDynamicProperyAccess(typeObject);
                IList dataSourceTemp = createTempSource(dataSource);
                //IList dataSourceTemp = dataSource;
                handleNoSummaryColumns(notSummaryCols, dAccs, ref dataSourceTemp);
                IQueryable afterGroupBy = groupBy(selectedSummaryLevel, notSummaryCols, dataSourceTemp);
                DataTable dtResult = convertGrouByToDataTable(typeObject, dAccs, afterGroupBy);
                MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(this.gridSummary, dtResult, _XmlFile);

            }

        }

        #region 为绑定到Grid做的数据处理
        /// <summary>
        /// 创建动态对象的属性访问器，以便快速的动态访问
        /// </summary>
        /// <param name="typeObject">对象的类型</param>
        /// <returns>属性访问器，以字段存储</returns>
        private static Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> createDynamicProperyAccess(Type typeObject) {
            PropertyInfo[] infos = typeObject.GetProperties();
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (PropertyInfo info in infos) {
                object[] atts = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (atts == null || atts.Length == 0) continue;

                dAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(typeObject, info));
            }
            return dAccs;
        }

        /// <summary>
        /// 创建一个临时的dataSource
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        private IList createTempSource(IList dataSource) {
            
            #region 复制一个临时的dataSource用于做处理
            IList tempSource = null;
            using (MemoryStream ms = new MemoryStream()) {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, dataSource);

                ms.Position = 0;
                tempSource = bf.Deserialize(ms) as IList;
            }
            #endregion
            return tempSource;
        }

        /// <summary>
        /// 对分组的列进行处理，现在提供的处理时截取字符串的长度
        /// </summary>
        /// <param name="notSummaryCols">分组列的配置</param>
        /// <param name="dAccs"></param>
        /// <param name="tempSource">要处理的集合</param>
        private void handleNoSummaryColumns(List<GroupByColumn> notSummaryCols, Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs,ref IList tempSource) {
            for (int i = 0; i < tempSource.Count; i++) {
                //对每一个对象，和每一个配置做处理
                string groupByString = string.Empty;
                foreach (GroupByColumn noSumCol in notSummaryCols) {
                    if (!string.IsNullOrEmpty(noSumCol.SubString)) {
                        object data = tempSource[i];
                        substringOneColumn(ref data, noSumCol, dAccs);  //处理截取列,一个个列处理
                    }
                }
            }

        }

        /// <summary>
        /// 对临时数据进行分组
        /// </summary>
        /// <param name="selectedSummaryLevel">汇总级别</param>
        /// <param name="notSummaryCols">分组列的配置信息</param>
        /// <param name="dataSourceTemp">临时数据源</param>
        /// <returns>IQueryable临时对象</returns>
        private IQueryable groupBy(string selectedSummaryLevel, List<GroupByColumn> notSummaryCols, IList dataSourceTemp) {
            #region 通过动态LINQ做汇总

            string groupBy = prepareGroupByString(notSummaryCols);  //准备group by 字符串
            string groupByForDynamicLinq = string.Format("new ({0})", groupBy);
            string agg = "KEY as KEY, ";                            //准备汇总字符串，先取出KEY，再汇总字段
            string summaryColString = _SummaryInfo.ColumnSummaryInfos[selectedSummaryLevel].IncludeSummaryColumns;
            agg = agg + prepareAggregationString(summaryColString);
            agg = agg.TrimEnd(',');
            string aggByForDynamicLinq = string.Format("new ({0})", agg);

            IQueryable afterGroupBy = DynamicQueryable.GroupBy(
                dataSourceTemp.AsQueryable(), groupByForDynamicLinq, "it").Select(
                aggByForDynamicLinq);

            #endregion
            return afterGroupBy;
        }


        /// <summary>
        /// 将汇总以后的数据动态的赋值回最终呈现的数据源
        /// </summary>
        /// <param name="typeObject">动态对象的类型</param>
        /// <param name="dAccs">动态对象属性访问的缓存集合</param>
        /// <param name="afterGroupBy">groupBy以后的数据源</param>
        /// <returns>动态DataTable</returns>
        private static DataTable convertGrouByToDataTable(Type typeObject, Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs, IQueryable afterGroupBy) {
            var myReflection = MB.Util.MyReflection.Instance;
            var instanceCreationTool = MB.Util.DllFactory.Instance;
            DataTable dtResult = new DataTable();
            bool isFirstLoop = true;//如果是第一次进入循环，要先动态创建DataTable的列
            foreach (object afterGroupByEntity in afterGroupBy) {
                object dataSourceResult = instanceCreationTool.CreateInstance(typeObject);
                object keys = myReflection.InvokePropertyForGet(afterGroupByEntity, "KEY");
                PropertyInfo[] keyProInfos = keys.GetType().GetProperties();
                PropertyInfo[] valueProInfos = afterGroupByEntity.GetType().GetProperties();
                if (isFirstLoop) {
                    #region 创建DataColumn
                    foreach (PropertyInfo keyProInfo in keyProInfos) {
                        if (dAccs.Keys.Contains(keyProInfo.Name)) {
                            if (!dtResult.Columns.Contains(keyProInfo.Name)) {
                                dtResult.Columns.Add(keyProInfo.Name);
                            }
                        }
                    }

                    foreach (PropertyInfo valueProInfo in valueProInfos) {
                        if (dAccs.Keys.Contains(valueProInfo.Name)) {
                            if (!dtResult.Columns.Contains(valueProInfo.Name)) {
                                dtResult.Columns.Add(valueProInfo.Name);
                            }
                        }
                    }

                    #endregion
                    isFirstLoop = false;
                }
                #region 赋值到DataTable
                DataRow dr = dtResult.NewRow();
                foreach (PropertyInfo keyProInfo in keyProInfos) {
                    if (dAccs.Keys.Contains(keyProInfo.Name)) {
                        object value = myReflection.InvokePropertyForGet(keys, keyProInfo.Name);
                        dr[keyProInfo.Name] = value;
                    }
                }
                foreach (PropertyInfo valueProInfo in valueProInfos) {
                    if (dAccs.Keys.Contains(valueProInfo.Name)) {
                        object value = myReflection.InvokePropertyForGet(afterGroupByEntity, valueProInfo.Name);
                        dr[valueProInfo.Name] = value;
                    }
                }
                #endregion
                dtResult.Rows.Add(dr);
            }
            return dtResult;
        }

        #endregion


        /// <summary>
        /// 对单条数据源的，截取列进行处理，以后可以通过方法的形式扩展
        /// 可以在调用出循环数据源调用
        /// </summary>
        /// <param name="dataEntity">需要做处理的对象</param>
        /// <param name="noSumCol">非聚组列的配置信息</param>
        /// <param name="dAccs">属性访问器</param>
        private void substringOneColumn(ref object dataEntity, GroupByColumn noSumCol, 
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs) {
            if (!_ColProps.ContainsKey(noSumCol.Name)) {
                        throw new MB.Util.APPException(string.Format(
                            "汇总列{0}在ColumnPropertyInfo中没有正确配置，或两边的NAME不一致", noSumCol.Name));
                    }

            if (!dAccs.Keys.Contains(noSumCol.Name)) {
                return;
            }

            if (string.IsNullOrEmpty(noSumCol.SubString))
                return;

            var myReflection = MB.Util.MyReflection.Instance;
            if (!string.IsNullOrEmpty(noSumCol.SubString)) {
                if (_ColProps[noSumCol.Name].DataType.CompareTo("System.String") != 0)
                    throw new MB.Util.APPException(string.Format("截取列NotSummaryColumnInfo:{0}的类型只能是System.String", noSumCol.Name), Util.APPMessageType.DisplayToUser);
            }
            string[] subStringParas = noSumCol.SubString.Split(',');
            if (subStringParas.Length != 2)
                throw new MB.Util.APPException(string.Format("截取列NotSummaryColumnInfo:{0}SubString设置的不正确，正确格式是 [起始位置,长度]", noSumCol.Name), Util.APPMessageType.DisplayToUser);
            int startPosition = 0;
            int subLength = 0;

            bool hasStartPosition = Int32.TryParse(subStringParas[0], out startPosition);
            bool hasSubLength = Int32.TryParse(subStringParas[1], out subLength);

            if (hasStartPosition && hasSubLength) {
                string orgValue = dAccs[noSumCol.Name].Get(dataEntity) as string;
                if (string.IsNullOrEmpty(orgValue) || orgValue.Length < subLength)
                    return;

                string subValue = string.Empty;
                if (subLength <= 0)
                    subValue = orgValue.Substring(startPosition);
                else
                    subValue = orgValue.Substring(startPosition, subLength);
                dAccs[noSumCol.Name].Set(dataEntity, subValue);
            }
            else
                throw new MB.Util.APPException(string.Format("截取列NotSummaryColumnInfo:{0}SubString设置的不正确，正确格式是 [起始位置,长度]", noSumCol.Name), Util.APPMessageType.DisplayToUser);
        }

        /// <summary>
        /// 准备group by的类似SQL的字符串, 列名的集合以逗号隔开
        /// </summary>
        /// <param name="notSummaryCols">非汇总列的配置信息</param>
        /// <returns></returns>
        private string prepareGroupByString(List<GroupByColumn> notSummaryCols) {
            string result = string.Empty;
            foreach (GroupByColumn noSumCol in notSummaryCols) {
                result = result + noSumCol.Name + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }

        /// <summary>
        /// 准备好要取出的AGG的类似SQL的字符串
        /// </summary>
        /// <param name="summaryColString">以逗号隔开的汇总列集合字符串</param>
        /// <returns></returns>
        private string prepareAggregationString(string summaryColString) {
            string agg = string.Empty;
            string[] sumCols = summaryColString.Split(',');
            foreach (string sumColName in sumCols) {
                SummaryItemType sumItem = _SummaryInfo.ColumnSummaryInfos[sumColName].SummaryItemType;

                switch (sumItem) {
                    case SummaryItemType.Sum:
                        agg = agg + string.Format("sum({0}) as {0},", sumColName);
                        break;
                    case SummaryItemType.Average:
                        agg = agg + string.Format("avg({0}) as {0},", sumColName);
                        break;
                    case SummaryItemType.Count:
                        agg = agg + string.Format("count({0}) as {0},", sumColName);
                        break;
                    case SummaryItemType.Max:
                        agg = agg + string.Format("max({0}) as {0},", sumColName);
                        break;
                    case SummaryItemType.Min:
                        agg = agg + string.Format("min({0}) as {0},", sumColName);
                        break;
                    default:
                        break;
                }
            }
            agg = agg.TrimEnd(',');
            return agg;
        }

        #endregion

    }
}