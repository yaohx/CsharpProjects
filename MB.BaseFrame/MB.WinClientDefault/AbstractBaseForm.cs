using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MB.Util.Serializer;
using MB.WinBase.IFace;
using MB.Util;
using MB.WinBase.Binding;

namespace MB.WinClientDefault {
    public partial class AbstractBaseForm : DevExpress.XtraEditors.XtraForm {

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbstractBaseForm() {
            InitializeComponent();
        }

        /// <summary>
        /// 为当前表单绑定的实体对象注入默认值.
        /// </summary>
        /// <param name="currentEntity">当前表单绑定的实体对象</param>
        /// <param name="clientRuleCfg">配置</param>
        protected void PopulateDefaultValue(object currentEntity, IClientRuleConfig clientRuleCfg)
        {
            var filePath = GetDefaultValueFilePath(currentEntity);
            // 读取需要保存默认值的列
            var defaultValueColumns = clientRuleCfg.UIRuleXmlConfigInfo.GetDefaultColumns().Where(kv => kv.Value.SaveDefaultValue).Select(kv => kv.Value).ToList();

            //读取编辑列的配置信息
            Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editColumnCfg =
                clientRuleCfg.UIRuleXmlConfigInfo.ColumnsCfgEdit;


            if (defaultValueColumns.Count > 0)
            {
                if (File.Exists(filePath))
                {
                    var entity = DataContractSerializeHelper.Deserialize(currentEntity.GetType(), File.ReadAllText(filePath));

                    foreach (var column in defaultValueColumns)
                    {
                        
                        var value = MyReflection.Instance.InvokePropertyForGet(entity, column.Name);

                        if (value != null)
                        {
                            MyReflection.Instance.InvokePropertyForSet(currentEntity, column.Name, value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存默认值
        /// </summary>
        /// <param name="bindingSource">当前编辑窗口数据绑定</param>
        /// <param name="clientRuleCfg">配置</param>
        protected void SaveDefaultValue(BindingSourceEx bindingSource, IClientRuleConfig clientRuleCfg)
        {
            if (bindingSource.Current != null)
            {
                // 保存为默认值
                var defaultValueColumns = clientRuleCfg.UIRuleXmlConfigInfo.GetDefaultColumns().Where(kv => kv.Value.SaveDefaultValue).Select(kv => kv.Value).ToList();

                if (defaultValueColumns.Count > 0)
                {
                    try
                    {
                        File.WriteAllText(GetDefaultValueFilePath(bindingSource.Current), DataContractSerializeHelper.Serialize(bindingSource.Current.GetType(), bindingSource.Current));
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("保存默认值出错." + e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 获取默认值文件名
        /// </summary>
        /// <param name="currentEntity">当前实体类</param>
        /// <returns>文件路径全名</returns>
        private string GetDefaultValueFilePath(object currentEntity)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine("UserSetting", string.Format("{0}_{1}_{2}",
                this.GetType().Name,
                currentEntity.GetType().Name,
                "DefaultValue.xml")));
        }
    }
}
