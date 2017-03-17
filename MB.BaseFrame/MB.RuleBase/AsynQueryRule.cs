//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-05-21
// Description	:	所有基于异步查询分析业务类必须要继承的抽象基类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;


using MB.RuleBase.Common;
using MB.RuleBase.Atts;
using MB.RuleBase.IFace;
using MB.Util.Model;
namespace MB.RuleBase {
    /// <summary>
    /// 所有基于异步查询分析业务类必须要继承的抽象基类。
    /// </summary>
    public abstract class AsynQueryRule : BaseQueryRule, IAsynQueryRule {
        private byte[] _Buffer_all = null;
       // private byte[] _Buffer_currect = null;
        //每次以100K 作为单位进行传输
        private int _SingleLength =  1024 * 8 * 60;//MB.BaseFrame.SOD.L_SINGLE_PACK_MAX_LENGTH;
        private long _Remain_length;
        //private MemoryStream _CStream;
        private int _BufferCount;
        private long _BuffersLength;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDataDocType"></param>
        public AsynQueryRule(Type objectDataDocType)
            : base(objectDataDocType) {

        }

        #region IAsynQueryRule 成员
        /// <summary>
        /// 初始化数据查询分析。   
        /// </summary>
        /// <param name="dataInDocType">需要进行检查的数据类型</param>
        /// <param name="xmlFilterParams">QueryParameterInfo[] 系列化后的字符窜</param>
        public virtual void BeginRunWorker(int dataInDocType, string xmlFilterParams) {
            DataSet dsData = GetObjectAsDataSet(dataInDocType, xmlFilterParams);
            InitBuffers(dsData);
        }
        /// <summary>
        /// 获取整个数据块的个数。
        /// </summary>
        /// <returns></returns>
        public virtual int GetBufferCount() {
            return _BufferCount;
        }
        /// <summary>
        /// 根据Index 获取数据块     
        /// 这个是反复调用调用的方法没必须记录它的日记。
        /// </summary>
        /// <param name="index">在buffer 中的数据块 index</param>
        /// <returns>byte[] 数组</returns>
        [MB.Aop.InjectionMethodSwitch(false)]
        public virtual byte[] GetBufferByIndex(int index) {
            byte[] tempBuffer;
            if (index >= _BufferCount) return null;
           // _CStream.Seek(index * _SingleLength, SeekOrigin.Begin);
            if (index == _BufferCount - 1) {
                int remainLength = (int)(_BuffersLength - index * _SingleLength);
                tempBuffer = new byte[remainLength];
                //_CStream.Read(tempBuffer, 0, remainLength);
                fillDataBuffer(ref tempBuffer, index * _SingleLength, remainLength);
            }
            else {
                tempBuffer = new byte[_SingleLength];
               // _CStream.Read(tempBuffer, 0, _SingleLength);
                fillDataBuffer(ref tempBuffer, index * _SingleLength, _SingleLength);
            }
            return tempBuffer;
        }

        #endregion

        #region 内部函数处理...
        private void fillDataBuffer(ref byte[] input,int begintPosition, int length) {
            for (int i = 0; i < length ; i++) {
                input[i] = _Buffer_all[begintPosition + i];
            }
        }
        /// <summary>
        /// 初始化压缩查询得到的数据集为字节流。
        /// </summary>
        /// <param name="ds"></param>
        protected virtual void InitBuffers(DataSet ds) {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream tstream = new MemoryStream()) {
                formatter.Serialize(tstream, ds);

                //由于 统一在通道上进行压缩，这里没必要再进行压缩
                byte[] bytes_c = tstream.ToArray();// MB.Util.Compression.Instance.Zip(tstream.ToArray());
               // tstream.Close();

                //_CStream = new MemoryStream(bytes_c);
                _Buffer_all = bytes_c;
                //_CStream.Position = 0;
                int length = _Buffer_all.Length;
                _Remain_length = length;
                _BuffersLength = length;
                long c;
                long re = Math.DivRem(length, (long)_SingleLength, out c);

                _BufferCount = System.Convert.ToInt32(c > 0 ? re + 1 : re);
            }
            
        }
        #endregion 内部函数处理...
    }
}
