namespace MB.WinEIDrive.Excel
{
    using System;
    using System.Collections;
    using System.Text;

    ///<summary>
    ///Buffer class is used as the wrapper aroung input string for FormulaParser providing
    ///additional helpful methods for accessing string buffer.
    ///</summary>
    internal class Buffer
    {
        // Methods
        ///<summary>
        ///Initializes a new instance of the <see cref="MB.WinEIDrive.Excel.Buffer" /> class.
        ///</summary>
        ///<param name="data">The string data to wrap.</param>
        public Buffer(string data)
        {
            this.data = data;
            this.pos = -1;
        }

        ///<summary>
        ///Gets the current ñhar data.
        ///</summary>
        ///<returns>current char data</returns>
        public char GetCurrent()
        {
            return this.data[this.pos];
        }

        ///<summary>
        ///Gets the next char from buffer.
        ///</summary>
        ///<returns>read char or special char indicating no read status</returns>
        public char GetNext()
        {
            if (this.IsEOF)
            {
                return '@';
            }
            this.pos++;
            return this.data[this.pos];
        }

        ///<summary>
        ///Gets the next on demand( if next symbol is peek, than read it and return back ).
        ///</summary>
        ///<param name="peek">The char to peek for.</param>
        ///<returns>read char or special char indicating no read status</returns>
        public char GetNextOnDemand(char peek)
        {
            return this.GetNextOnDemand(peek, true);
        }

        ///<summary>
        ///Gets the next on demand( if next symbol is of the given chars, than read it and return back ).
        ///</summary>
        ///<param name="charsToPeekFor">The char array to peek for.</param>
        ///<returns>
        ///read char or special char indicating no read status
        ///</returns>
        public char GetNextOnDemand(char[] charsToPeekFor)
        {
            char ch1 = '@';
            for (int num1 = 0; num1 < charsToPeekFor.Length; num1++)
            {
                ch1 = this.GetNextOnDemand(charsToPeekFor[num1]);
                if (ch1 != '@')
                {
                    break;
                }
            }
            return ch1;
        }

        ///<summary>
        ///Gets the next on demand( if next symbol is peek, than read it and return back ).
        ///Additionally it can skip whitespaces on demand.
        ///</summary>
        ///<param name="peek">The char to peek for.</param>
        ///<param name="skipWhitespaces">if set to <c>true</c> [skip whitespaces].</param>
        ///<returns>
        ///read char or special char indicating no read status
        ///</returns>
        public char GetNextOnDemand(char peek, bool skipWhitespaces)
        {
            if (skipWhitespaces)
            {
                this.SkipWhitespaces();
            }
            if (this.Peek() != peek)
            {
                return '@';
            }
            return this.GetNext();
        }

        ///<summary>
        ///Gets the next string from buffer with skipping whitespaces.
        ///</summary>
        ///<returns>read string</returns>
        public string GetNextString()
        {
            return this.GetNextString(true);
        }

        ///<summary>
        ///Gets the next string from buffer with optional skipping whitespaces.
        ///</summary>
        ///<returns>read string</returns>
        public string GetNextString(bool skipWhitespaces)
        {
            bool flag1 = false;
            StringBuilder builder1 = new StringBuilder();
            char ch1 = this.Peek();
            bool flag2 = ch1 == '.';
            if (char.IsLetter(ch1))
            {
                do
                {
                    builder1.Append(ch1);
                    this.GetNext();
                    ch1 = this.Peek();
                }
                while (char.IsLetterOrDigit(this.Peek()));
            }
            else if ((char.IsDigit(ch1) || flag2) && !flag1)
            {
                do
                {
                    builder1.Append(ch1);
                    this.GetNext();
                    ch1 = this.Peek();
                    if ((ch1 == '.') && flag2)
                    {
                        break;
                    }
                    flag2 = ch1 == '.';
                }
                while (char.IsDigit(ch1) || flag2);
            }
            if (skipWhitespaces)
            {
                this.SkipWhitespaces();
            }
            return builder1.ToString();
        }

        ///<summary>
        ///Gets the next string from buffer.
        ///</summary>
        ///<param name="endChars">The arrag of chars used as end marks during reading.</param>
        ///<returns>read string</returns>
        public string GetNextString(char[] endChars)
        {
            ArrayList list1 = new ArrayList(endChars);
            StringBuilder builder1 = new StringBuilder();
            while (!this.IsEOF && !list1.Contains(this.Peek()))
            {
                builder1.Append(this.GetNext());
            }
            this.SkipWhitespaces();
            return builder1.ToString();
        }

        ///<summary>
        ///Gets the next string from buffer.
        ///</summary>
        ///<param name="endChar">The char used as end mark during reading.</param>
        ///<returns>read string</returns>
        public string GetNextString(char endChar)
        {
            StringBuilder builder1 = new StringBuilder();
            while ((this.Peek() != endChar) && !this.IsEOF)
            {
                builder1.Append(this.GetNext());
            }
            this.SkipWhitespaces();
            return builder1.ToString();
        }

        ///<summary>
        ///Gets the next on demand( if next symbol is peek, than read it and return back ).
        ///Additionally it always skip whitespaces.
        ///</summary>
        ///<returns>read char or special char indicating no read status</returns>
        public char GetNextWithWhitespaceSkippling()
        {
            char ch1 = this.GetNext();
            this.SkipWhitespaces();
            return ch1;
        }

        ///<summary>
        ///Peeks for the next char.
        ///</summary>
        ///<returns>read char or special char indicating no read status</returns>
        public char Peek()
        {
            if (!this.IsEOF)
            {
                return this.data[this.pos + 1];
            }
            return '@';
        }

        ///<summary>
        ///Peeks for the next char at specified forward-position
        ///</summary>
        ///<returns>read char or special char indicating no read status</returns>
        public char Peek(int peekPos)
        {
            if (((this.pos + 1) + peekPos) < this.data.Length)
            {
                return this.data[(this.pos + 1) + peekPos];
            }
            return '@';
        }

        ///<summary>
        ///Skips the whitespaces.
        ///</summary>
        public void SkipWhitespaces()
        {
            while (!this.IsEOF && (this.Peek() == ' '))
            {
                this.GetNext();
            }
        }


        // Properties
        ///<summary>
        ///Gets the input data string buffer.
        ///</summary>
        ///<value>The input data string buffer.</value>
        public string Data
        {
            get
            {
                return this.data;
            }
        }

        ///<summary>
        ///Gets a value indicating whether we have reached end of input buffer.
        ///</summary>
        ///<value><c>true</c> if we have reached end of input buffer; otherwise, <c>false</c>.</value>
        public bool IsEOF
        {
            get
            {
                return (this.data.Length == (this.pos + 1));
            }
        }

        ///<summary>
        ///Gets the position of input data string buffer.
        ///</summary>
        ///<value>The position of input data string buffer.</value>
        public int Pos
        {
            get
            {
                return this.pos;
            }
        }


        // Fields
        private string data;
        ///<summary>
        ///Unique char to identify that char is empty
        ///</summary>
        public const char Empty = '@';
        ///<summary>
        ///Unique integer to identify that integer is null
        ///</summary>
        public const int EmptyInteger = -2147483648;
        private int pos;
    }
}

