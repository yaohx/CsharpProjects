using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using System.IO;

namespace MB.Util.WcfCompress
{
    public class MessageCompressor
    {
        public MessageCompressor(CompressionAlgorithm algorithm) {
            this.Algorithm = algorithm;
        }
        public Message CompressMessage(Message sourceMessage) {
            byte[] buffer;
            using (XmlDictionaryReader reader1 = sourceMessage.GetReaderAtBodyContents()) {
                buffer = Encoding.UTF8.GetBytes(reader1.ReadOuterXml());
            }
            if (buffer.Length == 0) {
                Message emptyMessage = Message.CreateMessage(sourceMessage.Version, (string)null);
                sourceMessage.Headers.CopyHeadersFrom(sourceMessage);
                sourceMessage.Properties.CopyProperties(sourceMessage.Properties);
                emptyMessage.Close();
                return emptyMessage;
            }
            byte[] compressedData = DataCompressor.Compress(buffer, this.Algorithm);
            string copressedBody = CompressionUtil.CreateCompressedBody(compressedData);
            XmlTextReader reader = new XmlTextReader(new StringReader(copressedBody), new NameTable());
            Message message2 = Message.CreateMessage(sourceMessage.Version, null, (XmlReader)reader);
            message2.Headers.CopyHeadersFrom(sourceMessage);
            message2.Properties.CopyProperties(sourceMessage.Properties);
            message2.AddCompressionHeader(this.Algorithm);
            sourceMessage.Close();
            return message2;
        }

        public Message DecompressMessage(Message sourceMessage) {
            if (!sourceMessage.IsCompressed()) {
                return sourceMessage;
            }
            CompressionAlgorithm algorithm = sourceMessage.GetCompressionAlgorithm();
            sourceMessage.RemoveCompressionHeader();
            byte[] compressedBody = sourceMessage.GetCompressedBody();
            byte[] decompressedBody = DataCompressor.Decompress(compressedBody, algorithm);
            string newMessageXml = Encoding.UTF8.GetString(decompressedBody);
            XmlTextReader reader2 = new XmlTextReader(new StringReader(newMessageXml));
            Message newMessage = Message.CreateMessage(sourceMessage.Version, null, reader2);
            newMessage.Headers.CopyHeadersFrom(sourceMessage);
            newMessage.Properties.CopyProperties(sourceMessage.Properties);
            return newMessage;
        }

        public CompressionAlgorithm Algorithm { get; private set; }
    }
}
