using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using System.IO;

namespace MB.Util.WcfCompress
{
    public static class CompressionUtil
    {
        public const string CompressionMessageHeader = "Compression";
        public const string CompressionMessageBody = "CompressedBody";
        public const string Namespace = "http://www.artech.com/compression";

        public static bool IsCompressed(this Message message) {
            return message.Headers.FindHeader(CompressionMessageHeader, Namespace) > -1;
        }

        public static void AddCompressionHeader(this Message message, CompressionAlgorithm algorithm) {
            message.Headers.Add(MessageHeader.CreateHeader(CompressionMessageHeader, Namespace, string.Format("algorithm = \"{0}\"", algorithm)));
        }

        public static void RemoveCompressionHeader(this Message message) {
            message.Headers.RemoveAll(CompressionMessageHeader, Namespace);
        }

        public static CompressionAlgorithm GetCompressionAlgorithm(this Message message) {
            if (message.IsCompressed()) {
                var algorithm = message.Headers.GetHeader<string>(CompressionMessageHeader, Namespace);
                algorithm = algorithm.Replace("algorithm =", string.Empty).Replace("\"", string.Empty).Trim();
                if (algorithm == CompressionAlgorithm.Deflate.ToString()) {
                    return CompressionAlgorithm.Deflate;
                }

                if (algorithm == CompressionAlgorithm.GZip.ToString()) {
                    return CompressionAlgorithm.GZip;
                }
                throw new InvalidOperationException("Invalid compression algrorithm!");
            }
            throw new InvalidOperationException("Message is not compressed!");
        }

        public static byte[] GetCompressedBody(this Message message) {
            byte[] buffer;
            using (XmlReader reader1 = message.GetReaderAtBodyContents()) {
                buffer = Convert.FromBase64String(reader1.ReadElementString(CompressionMessageBody, Namespace));
            }
            return buffer;
        }

        public static string CreateCompressedBody(byte[] content) {
            StringWriter output = new StringWriter();
            using (XmlWriter writer2 = XmlWriter.Create(output)) {
                writer2.WriteStartElement(CompressionMessageBody, Namespace);
                writer2.WriteBase64(content, 0, content.Length);
                writer2.WriteEndElement();
            }
            return output.ToString();
        }
    }
}
