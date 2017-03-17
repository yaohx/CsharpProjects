using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace MB.Json
{
    /// <summary>
    /// 
    /// </summary>
     public class JsonReader : IDisposable
    {
        private readonly TextReader _reader;
        private bool _disposed;

        #region construct function...
        /// <summary>
         /// 
         /// </summary>
         /// <param name="input"></param>
        public JsonReader(TextReader input)
        {
            _reader = input;
        }
         /// <summary>
         /// 
         /// </summary>
         /// <param name="input"></param>
        public JsonReader(Stream input) : this(new StreamReader(input, Encoding.UTF8)) { }        
         /// <summary>
         /// 
         /// </summary>
         /// <param name="input"></param>
        public JsonReader(string input) : this(new StringReader(input)) { }
        #endregion 

         /// <summary>
         /// 
         /// </summary>
        public virtual void SkipWhiteSpaces()
        {            
            while(true)
            {
                char c = Peek();
                if (!char.IsWhiteSpace(c))
                {
                    break;
                }
                _reader.Read();
            }
        }
        public virtual int ReadInt32()
        {
            string value = ReadNumericValue();
            return value == null ? 0 : Convert.ToInt32(value);
        }
        public virtual string ReadString()
        {
            AssertAndConsume(JsonTokens.StringDelimiter);            
            StringBuilder sb = new StringBuilder(25);
            bool isEscaped = false;

            while(true)
            {
                char c = Read();
                if ((c == '\\') && !isEscaped)
                {
                    isEscaped = true;
                    continue;
                }
                if (isEscaped)
                {
                    sb.Append(FromEscaped(c));
                    isEscaped = false;
                    continue;
                }
                if (c == '"')
                {                 
                    break;
                }
                sb.Append(c);
            }            
            string str = sb.ToString().Replace("\\r\\n","\r\n");
            return str == "null" ? null : str;
        }
        public virtual double ReadDouble()
        {
            string value = ReadNumericValue();            
            return value == null ? 0 : Convert.ToDouble(value);
        }
        public virtual DateTime ReadDateTime()
        {
            string str = ReadString();

            return ReadJsonDateTime(str); //modify by aifang //return str == null ? DateTime.MinValue : DateTime.ParseExact(str, "G", CultureInfo.InvariantCulture);
        }

        public virtual DateTime ReadJsonDateTime(string jsonDate)
        {            
            string value = jsonDate.Substring(6, jsonDate.Length - 8);

            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);

            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);

            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:

                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }

            return dateTime;
        }

        public virtual char ReadChar()
        {
            string str = ReadString();            
            if (str == null)
            {
                return (char) 0;
            }
            if (str.Length > 1)
            {
                throw new JsonException("Expecting a character, but got a string");
            }                        
            return str[0];
        }
        public virtual int ReadEnum()
        {
            return ReadInt32();            
        }
        public virtual long ReadInt64()
        {
            string value = ReadNumericValue();
            return value == null ? 0 : Convert.ToInt64(value);
        }
        public virtual float ReadFloat()
        {
            string value = ReadNumericValue();
            return value == null ? 0 : Convert.ToSingle(value);
        }
        public virtual short ReadInt16()
        {
            string value = ReadNumericValue();
            return value == null ? (short)0 : Convert.ToInt16(value);
        }
        public virtual decimal ReadDecimal()
        {
            string value = ReadNumericValue();
            return value == null ? (decimal)0 : Convert.ToDecimal(value);
        }
        public virtual string ReadNumericValue()
        {
            return ReadNonStringValue('0');
        }
        public virtual string ReadNonStringValue(char offset)
        {
            StringBuilder sb = new StringBuilder(10);
            while (true)
            {
                char c = Peek();
                if (IsDelimiter(c))
                {
                    break;
                }
                int read = _reader.Read();
                if (read >= '0' && read <= '9')
                {
                    sb.Append(read - offset);
                }
                else
                {
                    sb.Append((char) read);
                }                
            }
            string str = sb.ToString();
            return str == "null" ? null : str;
        }
        public virtual byte ReadByte()
        {
            byte value = new byte();
            string sValue = ReadNumericValue();
            if (byte.TryParse(sValue, out value))
                return value;
            else throw new JsonException("Unrecognized byte character: " + sValue);
        }

        public virtual bool IsDelimiter(char c)
        {
            return (c == JsonTokens.EndObjectLiteralCharacter || c == JsonTokens.EndArrayCharacter || c == JsonTokens.ElementSeparator || IsWhiteSpace(c));
        }
        public virtual bool IsWhiteSpace(char c)
        {
            return char.IsWhiteSpace(c);
        }

        public virtual char Peek()
        {
            int c = _reader.Peek();
            return ValidateChar(c);
        }
        public virtual char Read()
        {
            int c = _reader.Read();
            return ValidateChar(c);
        }
        private char ValidateChar(int c)
        {
            if (c == -1)
            {
                throw new JsonException("End of data");
            }
            return (char)c;
        }
        
        public virtual string FromEscaped(char c)
        {
            switch (c)
            {
                case '"':
                    return "\"";
                case '\\':
                    return "\\";
                case 'b':
                    return "\b";
                case 'f':
                    return "\f";
                case 'r':
                    return "\r";
                case 'n':
                    return "\n";
                case 't':
                    return "\t";
                case '/':
                    return "/";
                default:
                    throw new ArgumentException("Unrecognized escape character: " + c);
            }
        }

        protected internal virtual void AssertAndConsume(char character)
        {
            char c = Read();
            if (c != character)
            {
                throw new JsonException(string.Format("Expected character '{0}', but got: '{1}'", character, c));
            }
        }
        protected internal bool AssertNextIsDelimiterOrSeparator(char endDelimiter)
        {
            char delimiter = Read();
            if (delimiter == endDelimiter)
            {
                return true;
            }
            if (delimiter == ',')
            {
                return false;                
            }
            throw new JsonException("Expected array separator or end of array, got: " + delimiter);            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _reader.Close();
                }
                _disposed = true;
            }
        }
    }
}
