using System.Globalization;
using System.IO;

namespace MB.Json
{
    /// <summary>
    /// Converter
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="output"></param>
        /// <param name="instance"></param>
        public static void Serialize(Stream output, object instance)
        {
            Serialize(output, instance, string.Empty);
        }
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="output"></param>
        /// <param name="instance"></param>
        /// <param name="fieldPrefix"></param>
        public static void Serialize(Stream output, object instance, string fieldPrefix)
        {
            using (JsonWriter writer = new JsonWriter(output))
            {
                JsonSerializer.Serialize(writer, instance, fieldPrefix);
            }   
        }
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="file"></param>
        /// <param name="instance"></param>
        public static void Serialize(string file, object instance)
        {
            Serialize(file, instance, string.Empty);
        }
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="file"></param>
        /// <param name="instance"></param>
        /// <param name="fieldPrefix"></param>
        public static void Serialize(string file, object instance, string fieldPrefix)
        {
            using (JsonWriter writer = new JsonWriter(file))
            {
                JsonSerializer.Serialize(writer, instance, fieldPrefix);                
            }              
        }
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string Serialize(object instance)
        {
            return Serialize(instance, string.Empty);
        }
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldPrefix"></param>
        /// <returns></returns>
        public static string Serialize(object instance, string fieldPrefix)
        {
            using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
            using (JsonWriter writer = new JsonWriter(sw))
            {
                JsonSerializer.Serialize(writer, instance, fieldPrefix);
                return sw.ToString();
            }               
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T Deserialize<T>(Stream input)
        {
            return Deserialize<T>(input, string.Empty);
        }
        public static T Deserialize<T>(Stream input, string fieldPrefix)
        {
            using (JsonReader reader = new JsonReader(input))
            {
                return JsonDeserializer.Deserialize<T>(reader);
            }
        }
        /// <summary>
        /// DeserializeFromFile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string file)
        {
            return DeserializeFromFile<T>(file, string.Empty);
        }
        /// <summary>
        /// DeserializeFromFile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="fieldPrefix"></param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string file, string fieldPrefix)
        {
            using (JsonReader reader = new JsonReader(new FileStream(file, FileMode.Open, FileAccess.Read)))
            {
                return JsonDeserializer.Deserialize<T>(reader);
            }  
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return Deserialize<T>(json, string.Empty);
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="fieldPrefix"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json, string fieldPrefix)
        {
            using (StringReader sr = new StringReader(json))
            using (JsonReader reader = new JsonReader(sr))
            {
                return JsonDeserializer.Deserialize<T>(reader, fieldPrefix);                
            }
        }
    }
}
