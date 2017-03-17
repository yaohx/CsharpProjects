using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.WinClientDefault.DataImport {
    /// <summary>
    /// 客户端业务类扩展导入导出处理类。
    /// </summary>
    public interface  IDocDataImportProvider{

        /// <summary>
        /// 单据数据导入处理。
        /// </summary>
        /// <param name="viewForm"></param>
        /// <param name="importDataInfo"></param>
        /// <param name="hasImportData">>成功导入的数据实体</param>
        /// <returns></returns>
        bool DataImport(MB.WinBase.IFace.IViewGridForm viewForm,MB.WinClientDefault.DataImport.DataImportInfo importDataInfo,
                        out IList hasImportData);
   
        
    }
}
