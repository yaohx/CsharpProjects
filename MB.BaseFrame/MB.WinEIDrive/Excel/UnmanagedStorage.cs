namespace MB.WinEIDrive.Excel
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;

    internal class UnmanagedStorage : StructuredStorageFileBase
    {
        // Methods
        public UnmanagedStorage(string fileName, bool create)
        {
            OLE_MODE ole_mode1;
            SecurityPermission permission1 = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
            if (create)
            {
                ole_mode1 = OLE_MODE.STGM_CREATE | (OLE_MODE.STGM_SHARE_EXCLUSIVE | OLE_MODE.STGM_READWRITE);
                try
                {
                    permission1.Assert();
                    SafeNativeMethods.StgCreateDocfile(fileName, (int) ole_mode1, 0, out this.storage);
                }
                catch
                {
                    throw new Exception("Need to have unmanaged code rights to execute this code.");
                }
                if (this.storage == null)
                {
                    throw new IOException("Can't create file. Is file with the \"" + fileName + "\" name used by another process?");
                }
            }
            else
            {
                ole_mode1 = OLE_MODE.STGM_SHARE_EXCLUSIVE | OLE_MODE.STGM_READWRITE;
                try
                {
                    permission1.Assert();
                    SafeNativeMethods.StgOpenStorage(fileName, null, (int) ole_mode1, IntPtr.Zero, 0, out this.storage);
                }
                catch
                {
                    throw new Exception("Need to have unmanaged code rights to execute this code.");
                }
                if (this.storage == null)
                {
                    throw new IOException("Can't open file. Maybe file with the \"" + fileName + "\" name doesn't exist, is not in application folder or is used by another process? Only XLS files from Excel 97 and on are supported.");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.storage != null)
            {
                if (disposing)
                {
                    this.storage.Commit(0);
                }
                Marshal.ReleaseComObject(this.storage);
                this.storage = null;
            }
        }

        public override byte[] ReadStream(string name)
        {
            UCOMIStream stream1;
            STATSTG statstg1;
            if (this.Disposed)
            {
                throw new ObjectDisposedException("UnmanagedStorage");
            }
            OLE_MODE ole_mode1 = OLE_MODE.STGM_SHARE_EXCLUSIVE | OLE_MODE.STGM_READWRITE;
            try
            {
                this.storage.OpenStream(name, IntPtr.Zero, (uint) ole_mode1, 0, out stream1);
            }
            catch (COMException)
            {
                throw new Exception("Provided file is not a valid BIFF8 file. Only XLS files from Excel 97 and on are supported.");
            }
            if (stream1 == null)
            {
                throw new Exception("Can't open OLE2 stream.");
            }
            stream1.Stat(out statstg1, 1);
            byte[] buffer1 = new byte[statstg1.cbSize];
            GCHandle handle1 = GCHandle.Alloc(buffer1, GCHandleType.Pinned);
            stream1.Read(buffer1, (int) statstg1.cbSize, IntPtr.Zero);
            handle1.Free();
            return buffer1;
        }

        public override void WriteStream(string name, byte[] buffer)
        {
            UCOMIStream stream1;
            if (this.Disposed)
            {
                throw new ObjectDisposedException("UnmanagedStorage");
            }
            OLE_MODE ole_mode1 = OLE_MODE.STGM_SHARE_EXCLUSIVE | OLE_MODE.STGM_READWRITE;
            this.storage.CreateStream(name, (uint) ole_mode1, 0, 0, out stream1);
            if (stream1 == null)
            {
                throw new Exception("Can't create OLE2 stream.");
            }
            GCHandle handle1 = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            stream1.Write(buffer, buffer.Length, IntPtr.Zero);
            handle1.Free();
            stream1.Commit(0);
            Marshal.ReleaseComObject(stream1);
        }


        // Properties
        protected override bool Disposed
        {
            get
            {
                return (this.storage == null);
            }
        }


        // Fields
        private SafeNativeMethods.IStorage storage;

        // Nested Types
        [Flags]
        private enum OLE_MODE
        {
            // Fields
            STGM_CONVERT = 0x20000,
            STGM_CREATE = 0x1000,
            STGM_DELETEONRELEASE = 0x4000000,
            STGM_DIRECT = 0,
            STGM_DIRECT_SWMR = 0x400000,
            STGM_FAILIFTHERE = 0,
            STGM_NOSCRATCH = 0x100000,
            STGM_NOSNAPSHOT = 0x200000,
            STGM_PRIORITY = 0x40000,
            STGM_READ = 0,
            STGM_READWRITE = 2,
            STGM_SHARE_DENY_NONE = 0x40,
            STGM_SHARE_DENY_READ = 0x30,
            STGM_SHARE_DENY_WRITE = 0x20,
            STGM_SHARE_EXCLUSIVE = 0x10,
            STGM_SIMPLE = 0x8000000,
            STGM_TRANSACTED = 0x10000,
            STGM_WRITE = 1
        }

        private class SafeNativeMethods
        {
            // Methods
            private SafeNativeMethods()
            {
            }

            [DllImport("ole32.dll", CharSet=CharSet.Unicode)]
            internal static extern int StgCreateDocfile(string filename, int mode, int reserved, out IStorage storage);

            [DllImport("ole32.dll", CharSet=CharSet.Unicode)]
            internal static extern int StgOpenStorage(string filename, IStorage lastStorage, int mode, IntPtr pSNB, int reserved, out IStorage newStorage);


            // Nested Types
            [StructLayout(LayoutKind.Sequential)]
            internal struct _FILETIME
            {
                public uint dwHighDateTime;
                public uint dwLowDateTime;
                public _FILETIME(uint dwHighDateTime, uint dwLowDateTime)
                {
                    this.dwHighDateTime = dwHighDateTime;
                    this.dwLowDateTime = dwLowDateTime;
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct _ULARGE_INTEGER
            {
                public ulong QuadPart;
                public _ULARGE_INTEGER(ulong QuadPart)
                {
                    this.QuadPart = QuadPart;
                }
            }

            [Guid("0000000d-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComConversionLoss]
            internal interface IEnumSTATSTG
            {
                int Clone(out UnmanagedStorage.SafeNativeMethods.IEnumSTATSTG ppenum);
                int Next([In] uint celt, out UnmanagedStorage.SafeNativeMethods.tagSTATSTG rgelt, out uint pceltFetched);
                int RemoteNext([In] uint celt, out UnmanagedStorage.SafeNativeMethods.tagSTATSTG rgelt, out uint pceltFetched);
                int Reset();
                int Skip([In] uint celt);
            }

            [ComConversionLoss, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000b-0000-0000-c000-000000000046")]
            internal interface IStorage
            {
                void CreateStream([In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, out UCOMIStream ppstm);
                void OpenStream([In] string pwcsName, [In] IntPtr reserved1, [In] uint grfMode, [In] uint reserved2, out UCOMIStream ppstm);
                void CreateStorage([In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, out UnmanagedStorage.SafeNativeMethods.IStorage ppstg);
                void OpenStorage([In] string pwcsName, [In] UnmanagedStorage.SafeNativeMethods.IStorage pstgPriority, [In] uint grfMode, [In] ref UnmanagedStorage.SafeNativeMethods.tagRemSNB snbExclude, [In] uint reserved, out UnmanagedStorage.SafeNativeMethods.IStorage ppstg);
                void CopyTo([In] uint ciidExclude, [In] ref Guid rgiidExclude, [In] ref UnmanagedStorage.SafeNativeMethods.tagRemSNB snbExclude, [In] UnmanagedStorage.SafeNativeMethods.IStorage pstgDest);
                void MoveElementTo([In] string pwcsName, [In] UnmanagedStorage.SafeNativeMethods.IStorage pstgDest, [In] string pwcsNewName, [In] uint grfFlags);
                void Commit([In] uint grfCommitFlags);
                void Revert();
                int EnumElements([In] int reserved1, [In] IntPtr reserved2, [In] int reserved3, out UnmanagedStorage.SafeNativeMethods.IEnumSTATSTG ppenum);
                void DestroyElement([In] string pwcsName);
                void RenameElement([In] string pwcsOldName, [In] string pwcsNewName);
                void SetElementTimes([In] string pwcsName, [In] ref UnmanagedStorage.SafeNativeMethods._FILETIME pctime, [In] ref UnmanagedStorage.SafeNativeMethods._FILETIME patime, [In] ref UnmanagedStorage.SafeNativeMethods._FILETIME pmtime);
                void SetClass([In] ref Guid clsid);
                void SetStateBits([In] uint grfStateBits, [In] uint grfMask);
                void Stat(out UnmanagedStorage.SafeNativeMethods.tagSTATSTG pstatstg, [In] uint grfStatFlag);
            }

            [StructLayout(LayoutKind.Sequential), ComConversionLoss]
            internal struct tagRemSNB
            {
                [ComConversionLoss]
                public IntPtr rgString;
                public uint ulCntChar;
                public uint ulCntStr;
                public tagRemSNB(IntPtr rgString, uint ulCntChar, uint ulCntStr)
                {
                    this.rgString = rgString;
                    this.ulCntChar = ulCntChar;
                    this.ulCntStr = ulCntStr;
                }
            }

            [StructLayout(LayoutKind.Sequential), ComConversionLoss]
            internal struct tagSTATSTG
            {
                public UnmanagedStorage.SafeNativeMethods._FILETIME atime;
                public UnmanagedStorage.SafeNativeMethods._ULARGE_INTEGER cbSize;
                public Guid clsid;
                public UnmanagedStorage.SafeNativeMethods._FILETIME ctime;
                public uint grfLocksSupported;
                public uint grfMode;
                public uint grfStateBits;
                public UnmanagedStorage.SafeNativeMethods._FILETIME mtime;
                public string pwcsName;
                public uint reserved;
                public uint Type;
                public tagSTATSTG(UnmanagedStorage.SafeNativeMethods._FILETIME atime, UnmanagedStorage.SafeNativeMethods._ULARGE_INTEGER cbSize, Guid clsid, UnmanagedStorage.SafeNativeMethods._FILETIME ctime, uint grfLocksSupported, uint grfMode, uint grfStateBits, UnmanagedStorage.SafeNativeMethods._FILETIME mtime, string pwcsName, uint reserved, uint Type)
                {
                    this.atime = atime;
                    this.cbSize = cbSize;
                    this.clsid = clsid;
                    this.ctime = ctime;
                    this.grfLocksSupported = grfLocksSupported;
                    this.grfMode = grfMode;
                    this.grfStateBits = grfStateBits;
                    this.mtime = mtime;
                    this.pwcsName = pwcsName;
                    this.reserved = reserved;
                    this.Type = Type;
                }
            }
        }
    }
}

