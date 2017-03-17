
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;
using System.ServiceModel.Security;
using System.ServiceModel.Description;
using System.Reflection;

namespace MB.GZipEncoder {
    
    /// <summary>
    /// 负责创建一个自定义的 GZipMessageEncoder 编码器。
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough()]
    internal class GZipMessageEncoderFactory : MessageEncoderFactory {
        private MessageEncoder _Encoder;

        
        /// <summary>
        //根据内部的消息编码器创建自
        /// </summary>
        /// <param name="messageEncoderFactory"></param>
        public GZipMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory) {
            if (messageEncoderFactory == null)
                throw new ArgumentNullException("messageEncoderFactory", "A valid message encoder factory must be passed to the GZipEncoder");
            _Encoder = new GZipMessageEncoder(messageEncoderFactory.Encoder);

        }

        /// <summary>
        /// 框架利用这个属性从编码工厂中获取一个编码器。
        /// </summary>
        public override MessageEncoder Encoder {
            get { return _Encoder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override MessageVersion MessageVersion {
            get { return _Encoder.MessageVersion; }
        }

        /// <summary>
        /// 实际的WCF GZIP 编码器。
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough()]
        class GZipMessageEncoder : MessageEncoder {
           
            static string GZipContentType = "application/x-gzip";
            private static string SOAP2_NAME_SPACE_PREFIX = "http://schemas.xmlsoap.org/ws/2005/02";

            //压缩转换处理，并把结果存储在innerEncoder 中
            MessageEncoder innerEncoder;

            /// <summary>
            /// 检测性能指标的对象
            /// </summary>
            private object _CurrentWcfProcessMonitorInfo;
            
            internal GZipMessageEncoder(MessageEncoder messageEncoder)
                : base() {
                if (messageEncoder == null)
                    throw new ArgumentNullException("messageEncoder", "A valid message encoder must be passed to the GZipEncoder");
                innerEncoder = messageEncoder;
            }

            //public override string CharSet
            //{
            //    get { return ""; }
            //}

            public override string ContentType {
                get { return GZipContentType; }
            }

            public override string MediaType {
                get { return GZipContentType; }
            }
 
            public override MessageVersion MessageVersion {
                get { return innerEncoder.MessageVersion; }
            }

            //压缩一个字节数组
            static ArraySegment<byte> CompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager, int messageOffset) {

                MemoryStream memoryStream = new MemoryStream();
                memoryStream.Write(buffer.Array, 0, messageOffset);

                using (GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Compress, true)) {
                    gzStream.Write(buffer.Array, messageOffset, buffer.Count);
                }


                byte[] compressedBytes = memoryStream.ToArray();
                byte[] bufferedBytes = bufferManager.TakeBuffer(compressedBytes.Length);

                Array.Copy(compressedBytes, 0, bufferedBytes, 0, compressedBytes.Length);

                bufferManager.ReturnBuffer(buffer.Array);
                ArraySegment<byte> byteArray = new ArraySegment<byte>(bufferedBytes, messageOffset, bufferedBytes.Length - messageOffset);


                return byteArray;
            }

            //解压缩一个字节数组。
            static ArraySegment<byte> DecompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager) {
                MemoryStream memoryStream = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count - buffer.Offset);
                MemoryStream decompressedStream = new MemoryStream();
                int totalRead = 0;
                int blockSize = 1024;
                byte[] tempBuffer = bufferManager.TakeBuffer(blockSize);
                using (GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Decompress)) {
                    while (true) {
                        int bytesRead = gzStream.Read(tempBuffer, 0, blockSize);
                        if (bytesRead == 0)
                            break;
                        decompressedStream.Write(tempBuffer, 0, bytesRead);
                        totalRead += bytesRead;
                    }
                }
                bufferManager.ReturnBuffer(tempBuffer);

                byte[] decompressedBytes = decompressedStream.ToArray();
                byte[] bufferManagerBuffer = bufferManager.TakeBuffer(decompressedBytes.Length + buffer.Offset);
                Array.Copy(buffer.Array, 0, bufferManagerBuffer, 0, buffer.Offset);
                Array.Copy(decompressedBytes, 0, bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);

                ArraySegment<byte> byteArray = new ArraySegment<byte>(bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);
                bufferManager.ReturnBuffer(buffer.Array);
                
                return byteArray;
            }


            //One of the two main entry points into the encoder. Called by WCF to decode a buffered byte array into a Message.
            public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType) {
                //Decompress the buffer
                ArraySegment<byte> decompressedBuffer = DecompressBuffer(buffer, bufferManager);
                //Use the inner encoder to decode the decompressed buffer
                Message returnMessage = innerEncoder.ReadMessage(decompressedBuffer, bufferManager);
                returnMessage.Properties.Encoder = this;
                if (!returnMessage.Headers.Action.Contains(SOAP2_NAME_SPACE_PREFIX))
                {
                    SetCompressedResponseSize(buffer.Count);
                    SetResponseSize(decompressedBuffer.Count);
                }
                return returnMessage;
            }

            //One of the two main entry points into the encoder. Called by WCF to encode a Message into a buffered byte array.
            public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset) {
                //Use the inner encoder to encode a Message into a buffered byte array
                ArraySegment<byte> buffer = innerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
                //Compress the resulting byte array
                ArraySegment<byte> compressedBuffer = CompressBuffer(buffer, bufferManager, messageOffset);
                if (!message.Headers.Action.Contains(SOAP2_NAME_SPACE_PREFIX))
                {
                    SetRequestSize(buffer.Count);
                    SetCompressedRequestSize(compressedBuffer.Count);
                }
                return compressedBuffer;
            }

            public override Message ReadMessage(System.IO.Stream stream, int maxSizeOfHeaders, string contentType) {
                GZipStream gzStream = new GZipStream(stream, CompressionMode.Decompress, true);
                return innerEncoder.ReadMessage(gzStream, maxSizeOfHeaders);
            }

            public override void WriteMessage(Message message, System.IO.Stream stream) {
                using (GZipStream gzStream = new GZipStream(stream, CompressionMode.Compress, true)) {
                    innerEncoder.WriteMessage(message, gzStream);
                }

                // innerEncoder.WriteMessage(message, gzStream) depends on that it can flush data by flushing 
                // the stream passed in, but the implementation of GZipStream.Flush will not flush underlying
                // stream, so we need to flush here.
                stream.Flush();
            }

            #region 性能检测需要的方法,监测网络中传输和传入的数据量，并且进行压缩前后的对比

            /// <summary>
            /// 返回当前的WCF性能监控的指标对象
            /// </summary>
            /// <returns></returns>
            private object GetCurrentWcfMonitorInfo()
            {
                Type wcfPerformaceMonitorContext = Type.GetType("MB.Util.Monitors.WcfPerformaceMonitorContext,MB.Util");
                if (wcfPerformaceMonitorContext != null)
                {
                    PropertyInfo pCurrentContext = wcfPerformaceMonitorContext.GetProperty("Current");
                    object currentContext = pCurrentContext.GetValue(null, null);
                    if (currentContext != null)
                    {
                        PropertyInfo pCurrentWcfProcessMonitorInfo = wcfPerformaceMonitorContext.GetProperty("CurrentWcfProcessMonitorInfo");
                        object currentWcfProcessMonitorInfo = pCurrentWcfProcessMonitorInfo.GetValue(currentContext, null);
                        if (currentWcfProcessMonitorInfo != null)
                            return currentWcfProcessMonitorInfo;
                    }
                }
                return null;
            }

            private void SetRequestSize(int requestSize)
            {
                this._CurrentWcfProcessMonitorInfo = GetCurrentWcfMonitorInfo();
                if (_CurrentWcfProcessMonitorInfo != null)
                {
                    PropertyInfo pRequestSize = this._CurrentWcfProcessMonitorInfo.GetType().GetProperty("RequestSize");
                    pRequestSize.SetValue(this._CurrentWcfProcessMonitorInfo, requestSize, null);
                }
            }

            private void SetCompressedRequestSize(int compressedRequestSize)
            {
                this._CurrentWcfProcessMonitorInfo = GetCurrentWcfMonitorInfo();
                if (_CurrentWcfProcessMonitorInfo != null)
                {
                    PropertyInfo pRequestSize = this._CurrentWcfProcessMonitorInfo.GetType().GetProperty("CompressedRequestSize");
                    pRequestSize.SetValue(this._CurrentWcfProcessMonitorInfo, compressedRequestSize, null);
                }
            }

            private void SetCompressedResponseSize(int compressedResponseSize)
            {
                this._CurrentWcfProcessMonitorInfo = GetCurrentWcfMonitorInfo();
                if (_CurrentWcfProcessMonitorInfo != null)
                {
                    PropertyInfo pRequestSize = this._CurrentWcfProcessMonitorInfo.GetType().GetProperty("CompressedResponseSize");
                    pRequestSize.SetValue(this._CurrentWcfProcessMonitorInfo, compressedResponseSize, null);
                }
            }

            private void SetResponseSize(int responseSize)
            {
                this._CurrentWcfProcessMonitorInfo = GetCurrentWcfMonitorInfo();
                if (_CurrentWcfProcessMonitorInfo != null)
                {
                    PropertyInfo pRequestSize = this._CurrentWcfProcessMonitorInfo.GetType().GetProperty("ResponseSize");
                    pRequestSize.SetValue(this._CurrentWcfProcessMonitorInfo, responseSize, null);
                }
            }
            #endregion
        }
    }
}
