using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MB.Util.WcfCompress
{
    public class CompressionMessageFormatter : IDispatchMessageFormatter, IClientMessageFormatter
    {
        private const string DataContractSerializerOperationFormatterTypeName = "System.ServiceModel.Dispatcher.DataContractSerializerOperationFormatter, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        public IDispatchMessageFormatter InnerDispatchMessageFormatter { get; private set; }
        public IClientMessageFormatter InnerClientMessageFormatter { get; private set; }
        public MessageCompressor MessageCompressor { get; private set; }

        public CompressionMessageFormatter(CompressionAlgorithm algorithm, OperationDescription description, DataContractFormatAttribute dataContractFormatAttribute, DataContractSerializerOperationBehavior serializerFactory) {
            this.MessageCompressor = new MessageCompressor(algorithm);
            Type innerFormatterType = Type.GetType(DataContractSerializerOperationFormatterTypeName);
            var innerFormatter = Activator.CreateInstance(innerFormatterType, description, dataContractFormatAttribute, serializerFactory);
            this.InnerClientMessageFormatter = innerFormatter as IClientMessageFormatter;
            this.InnerDispatchMessageFormatter = innerFormatter as IDispatchMessageFormatter;
        }

        public void DeserializeRequest(Message message, object[] parameters) {
            message = this.MessageCompressor.DecompressMessage(message);
            this.InnerDispatchMessageFormatter.DeserializeRequest(message, parameters);
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result) {
            var message = this.InnerDispatchMessageFormatter.SerializeReply(messageVersion, parameters, result);
            return this.MessageCompressor.CompressMessage(message);
        }

        public object DeserializeReply(Message message, object[] parameters) {
            message = this.MessageCompressor.DecompressMessage(message);
            return this.InnerClientMessageFormatter.DeserializeReply(message, parameters);
        }

        public Message SerializeRequest(MessageVersion messageVersion, object[] parameters) {
            var message = this.InnerClientMessageFormatter.SerializeRequest(messageVersion, parameters);
            return this.MessageCompressor.CompressMessage(message);
        }
    }
}
