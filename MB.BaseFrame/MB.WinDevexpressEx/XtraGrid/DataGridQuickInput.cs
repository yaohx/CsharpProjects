//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-06-28
// Description	:	数据网格快速输入。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 数据网格快速输入。
    /// </summary>
    public class DataGridQuickInput{
        private DevExpress.XtraGrid.GridControl _XtraGrid;
        //private string[] _InputFields;
        private Dictionary<DataRow, List<string>> _ActiveColumns; 

      /// <summary>
        /// 数据网格快速输入。
      /// </summary>
      /// <param name="xtraGrid"></param>
      /// <param name="inputFields"></param>
      /// <param name="inputFieldTypeName"></param>
        public DataGridQuickInput(DevExpress.XtraGrid.GridControl xtraGrid, Dictionary<DataRow, List<string>> activeColumns) {
            _XtraGrid = xtraGrid;
            _ActiveColumns = activeColumns;
        }
        /// <summary>
        /// 根据比率输入的字符窜更改DataTable 中的值。
        /// </summary>
        /// <param name="rataStr"></param>
        public void FillByRataInput( string rataStr) {

            string errMsg = "不是有效的等比例输入格式,请查看操作提示。";
            //if (_InputFields == null || _InputFields.Length == 0)
            //    return;

            if (rataStr == null || rataStr.Length == 0 || rataStr.IndexOf('-') > -1) {
                MB.WinBase.MessageBoxEx.Show(errMsg);
                return;
            }
            //string uu = @"(^\+?[1-9][0-9]*$)|(^\+?[1-9][0-9]* [s]$)|(^\+?[1-9][0-9]* a$)";
            string rexStr = @"(\d$)";
            Regex re = new Regex(rexStr, RegexOptions.Compiled | RegexOptions.Singleline);
            bool b = re.IsMatch(rataStr);
            if (!b) {
                MB.WinBase.MessageBoxEx.Show(errMsg);
                return;
            }
            string[] cds = rataStr.Split(' ');
            int inputCount = 0;
            try {
                inputCount = MB.Util.MyConvert.Instance.ToInt(cds[0]);
            }
            catch {
                MB.WinBase.MessageBoxEx.Show(errMsg);
                return;
            }
            string fillType = string.Empty;
            int length = cds.Length;

            int[] inputRata = new int[length - 1];
            for (int i = 1; i < length; i++) {
                try {
                    inputRata[i - 1] = MB.Util.MyConvert.Instance.ToInt(cds[i]);
                }
                catch {
                    MB.WinBase.MessageBoxEx.Show(errMsg);
                }
            }
            qaData(inputCount, inputRata, fillType);
        }
        #region 内部函数处理等比例数据输入...
        //分析并把值输入到网格中
        private void qaData(int inputTotalCount, int[] inputRata, string fillType) {
            var gridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

            List<int> datas = new List<int>();
            int rowTotalCount = inputTotalCount;

            qaTotalCount(rowTotalCount, datas, inputRata);

            fillGridViewData(datas);


        }
        //分析输入的数量
        private void qaTotalCount(int totalCount, List<int> datas, int[] inputRata) {
            int inputFieldCount = inputRata.Length;
            double sumRata = 0;
            foreach (int rata in inputRata)
                sumRata += rata;
            if (sumRata == 0)
                return;
            for (int i = 0; i < inputFieldCount; i++) {
                if (i >= inputRata.Length)
                    break;
                int data = MB.Util.MyConvert.Instance.ToInt(totalCount * (inputRata[i] / sumRata));
                datas.Add(data);
            }
        }
        //把分析的值输入到网格中
        private void fillGridViewData(List<int> datas) {
            var gridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
            int count = gridView.RowCount;
            for (int rowHandle  = 0; rowHandle < count; rowHandle++) {
                DataRow dr = gridView.GetDataRow(rowHandle);
                List<string> fields = _ActiveColumns[dr];
                for (int i = 0; i < fields.Count; i++) {
                    if (i >= datas.Count)
                        break;

                    string hFieldName = fields[i];
                    
                    gridView.SetRowCellValue(rowHandle, hFieldName, datas[i]);
                }
            }
        }
      
        #endregion 内部函数处理等比例数据输入...
    }
}
