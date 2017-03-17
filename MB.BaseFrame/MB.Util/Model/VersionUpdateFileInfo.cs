//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	版本更新处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary>
    /// 版本更新的文件描述信息。
    /// </summary>
    [DataContract]
    public class VersionUpdateFileInfo {
        private string _FileName;
        private string _Description;
        private string _FileExtension;
        private long _FileLength;
        private long _HasDownLoad;
        private bool _Completed;
        private string _ChildDirectoryName;
        private string _Remark;

        /// <summary>
        ///  版本更新的文件描述信息。
        /// </summary>
        public VersionUpdateFileInfo() {
                         
        }
        /// <summary>
        /// 需要下载的文件名称。
        /// </summary>
        [DataMember] 
        public string FileName {
            get {
                return _FileName;
            }
            set {
                _FileName = value;
            }
        }
        /// <summary>
        /// 文件描述。
        /// </summary>
        [DataMember]
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        /// <summary>
        /// 文件扩展名称。
        /// </summary>
        [DataMember]
        public string FileExtension {
            get {
                return _FileExtension;
            }
            set {
                _FileExtension = value;
            }
        }
        /// <summary>
        /// 文件总长度。
        /// </summary>
        [DataMember]
        public long FileLength {
            get {
                return _FileLength;
            }
            set {
                _FileLength = value;
            }
        }
        /// <summary>
        /// 已经下载的文件大小。
        /// </summary>
        [DataMember]
        public long HasDownLoad {
            get {
                return _HasDownLoad;
            }
            set {
                _HasDownLoad = value;
            }
        }
        /// <summary>
        /// 判断是否已下载完成。
        /// </summary>
        [DataMember]
        public bool Completed {
            get {
                return _Completed;
            }
            set {
                _Completed = value;
            }
        }
        /// <summary>
        /// 文件存储的路径。
        /// 如果为空表示存储在当前目录下。
        /// </summary>
        [DataMember]
        public string ChildDirectoryName {
            get {
                return _ChildDirectoryName;
            }
            set {
                _ChildDirectoryName = value;
            }
        }
        /// <summary>
        /// 版本文件描述的备注信息。
        /// </summary>
        [DataMember]
        public string Remark {
            get {
                return _Remark;
            }
            set {
                _Remark = value;
            }
        }
    }
}
