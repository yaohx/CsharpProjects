using System;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;
using MB.Json.Helpers;
namespace MB.Json
{
    /// <summary>
    /// JsonDeserializer
    /// </summary>
    public class JsonDeserializer
    {
        private static readonly Type _IListType = typeof(IList);
        private readonly JsonReader _reader;
        private readonly string _fieldPrefix;
         
        /// <summary>
        /// JsonDeserializer
        /// </summary>
        /// <param name="reader"></param>
        public JsonDeserializer(JsonReader reader) : this(reader, string.Empty){}
        /// <summary>
        /// JsonDeserializer
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldPrefix"></param>
        public JsonDeserializer(JsonReader reader, string fieldPrefix)
        {
            _reader = reader;
            _fieldPrefix = fieldPrefix;
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T Deserialize<T>(JsonReader reader)
        {
            return Deserialize<T>(reader, string.Empty);
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="fieldPrefix"></param>
        /// <returns></returns>
        public static T Deserialize<T>(JsonReader reader, string fieldPrefix)
        {
            return (T) new JsonDeserializer(reader, fieldPrefix).deserializeValue(typeof(T));
        }
 

        #region 内部函数处理...
        private object deserializeValue(Type type)
        {
            _reader.SkipWhiteSpaces();            
            if (type == typeof(int))
            {
                return _reader.ReadInt32();                
            }
            else if (type == typeof(string))
            {
                return _reader.ReadString();                
            }
            else if (type == typeof(double))
            {
                return _reader.ReadDouble();
            }
            else if (type == typeof(DateTime))
            {
                return _reader.ReadDateTime();
            }
            else if (_IListType.IsAssignableFrom(type))
            {
                return deserializeList(type);
            }            
            else if (type == typeof(char))
            {
                return _reader.ReadChar();
            }
            else if (type.IsEnum)
            {
                return _reader.ReadEnum();
            }
            else if (type == typeof(long))
            {
                return _reader.ReadInt64();
            }        
            else if (type == typeof(float))
            {
                return _reader.ReadFloat();
            }
            else if (type == typeof(short))
            {
                return _reader.ReadInt16();
            }
            else if (type == typeof(decimal))
            {
                return _reader.ReadDecimal(); 
            }
            else if (type == typeof(byte))
            {
                return _reader.ReadByte();
            }
            return parseObject(type);            
        }
        private object deserializeList(Type listType)
        {
            _reader.SkipWhiteSpaces();
            _reader.AssertAndConsume(JsonTokens.StartArrayCharacter);            
            Type itemType = ListHelper.GetListItemType(listType);
            bool isReadonly;
            IList container = ListHelper.CreateContainer(listType, itemType, out isReadonly);
            while(true)
            {
                _reader.SkipWhiteSpaces();
                container.Add(deserializeValue(itemType));
                _reader.SkipWhiteSpaces();                
                if (_reader.AssertNextIsDelimiterOrSeparator(JsonTokens.EndArrayCharacter))
                {
                    break;
                }                
            }
            if (listType.IsArray)
            {
                return ListHelper.ToArray((List<object>)container, itemType);
            }
            if (isReadonly)
            {
                return listType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] { container.GetType() }, null).Invoke(new object[] { container });
            }
            return container;
        }
        private object parseObject(Type type)
        {           
            _reader.AssertAndConsume(JsonTokens.StartObjectLiteralCharacter);
            ConstructorInfo constructor = ReflectionHelper.GetDefaultConstructor(type);
            object instance = constructor.Invoke(null);
            while (true)
            {
                _reader.SkipWhiteSpaces();
                string name = _reader.ReadString();
                if (!name.StartsWith(_fieldPrefix))
                {
                    name = _fieldPrefix + name;
                }
                PropertyInfo proInfo = ReflectionHelper.FindProperty(type, name);
                _reader.SkipWhiteSpaces();
                _reader.AssertAndConsume(JsonTokens.PairSeparator);                
                _reader.SkipWhiteSpaces();
                proInfo.SetValue(instance, deserializeValue(proInfo.PropertyType),null);
                _reader.SkipWhiteSpaces();
                if (_reader.AssertNextIsDelimiterOrSeparator(JsonTokens.EndObjectLiteralCharacter))
                {
                    break;
                } 
            }
            return instance;
        }
        #endregion
    }
}