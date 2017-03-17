namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;
    using System.Text;

    internal class XLSRecord : AbsXLSRec
    {
        // Methods
        protected XLSRecord(XLSDescriptor descriptor)
        {
            this.InitializeDescriptorCode(descriptor);
        }

        public XLSRecord(string name)
        {
            this.InitializeDescriptorCode(name);
            this.InitializeBody(new byte[0]);
        }

        public XLSRecord(string name, object[] args)
        {
            this.InitializeDescriptorCode(name);
            if (args == null)
            {
                this.InitializeBody((byte[]) null);
            }
            else
            {
                this.InitializeBody(args);
            }
        }

        public XLSRecord(string name, string bodyHex)
        {
            this.InitializeDescriptorCode(name);
            this.InitializeBody(Utilities.HexStr2ByteArr(bodyHex));
        }

        protected XLSRecord(XLSDescriptor descriptor, int bodySize, BinaryReader br)
        {
            this.recordCode = descriptor.Code;
            this.ReadBody(bodySize, br);
            this.descriptor = descriptor;
        }

        public XLSRecord(int recordCode, int bodySize, BinaryReader br)
        {
            this.recordCode = recordCode;
            this.ReadBody(bodySize, br);
            this.descriptor = XLSDescriptors.GetByCode(recordCode);
        }

        private void FormattedArguments(StringBuilder sb, object[] args)
        {
            int num1 = 0;
            object[] objArray1 = args;
            for (int num2 = 0; num2 < objArray1.Length; num2++)
            {
                object obj1 = objArray1[num2];
                if (num1 > 0)
                {
                    sb.Append(' ');
                }
                if (obj1 is object[])
                {
                    sb.Append('{');
                    this.FormattedArguments(sb, (object[]) obj1);
                    sb.Append('}');
                }
                else
                {
                    sb.AppendFormat("arg{0}:{1}", num1, obj1.ToString());
                }
                num1++;
            }
        }

        public object[] GetArguments()
        {
            return XLSDescriptors.Parse(this.descriptor.BodyFormat, this.body, new XLSDescriptors.VariableArrayCountDelegate(this.GetVariableArraySize), new XLSDescriptors.StringLengthDelegate(this.GetStringLength));
        }

        protected virtual byte GetStringLength(object[] loadedArgs)
        {
            return 0;
        }

        protected virtual int GetVariableArraySize(object[] loadedArgs, object[] varArr, int bodySize)
        {
            throw new Exception("Internal error: Must override GetVariableArraySize() in derived class.");
        }

        protected void InitializeBody(byte[] body)
        {
            this.body = body;
        }

        protected void InitializeBody(object[] args)
        {
            this.InitializeBody(XLSDescriptors.Format(this.descriptor.BodyFormat, args));
        }

        protected virtual void InitializeDelayed()
        {
            throw new Exception("Internal error: can't write delayed record. Call or override InitializeDelayed().");
        }

        public void InitializeDelayed(object[] args)
        {
            this.InitializeBody(args);
        }

        private void InitializeDescriptorCode(XLSDescriptor descriptor)
        {
            this.descriptor = descriptor;
            this.recordCode = this.descriptor.Code;
        }

        private void InitializeDescriptorCode(string name)
        {
            this.InitializeDescriptorCode(XLSDescriptors.GetByName(name));
        }

        protected void ReadBody(int bodySize, BinaryReader br)
        {
            this.body = br.ReadBytes(bodySize);
        }

        protected override void WriteBody(BinaryWriter bw)
        {
            if (this.body == null)
            {
                this.InitializeDelayed();
            }
            bw.Write(this.body);
        }


        // Properties
        protected override byte[] Body
        {
            get
            {
                return this.body;
            }
        }

        protected override int BodySize
        {
            get
            {
                if (this.body == null)
                {
                    return this.descriptor.BodySize;
                }
                return this.body.Length;
            }
        }

        public override string FormattedBody
        {
            get
            {
                StringBuilder builder1 = new StringBuilder();
                if (this.descriptor != null)
                {
                    if (this.body == null)
                    {
                        this.InitializeDelayed();
                    }
                    this.FormattedArguments(builder1, this.GetArguments());
                }
                return builder1.ToString();
            }
        }

        public override string Name
        {
            get
            {
                if (this.descriptor != null)
                {
                    return this.descriptor.Name;
                }
                return "Unknown";
            }
        }

        internal override int RecordCode
        {
            get
            {
                return this.recordCode;
            }
        }


        // Fields
        private byte[] body;
        private XLSDescriptor descriptor;
        private int recordCode;
    }
}

