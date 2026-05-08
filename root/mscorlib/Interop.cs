using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000004 RID: 4
internal static class Interop
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
	private static void ThrowExceptionForIoErrno(Interop.ErrorInfo errorInfo, string path, bool isDirectory, Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter)
	{
		if (errorRewriter != null)
		{
			errorInfo = errorRewriter(errorInfo);
		}
		throw Interop.GetExceptionForIoErrno(errorInfo, path, isDirectory);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000206E File Offset: 0x0000026E
	internal static void CheckIo(Interop.Error error, string path = null, bool isDirectory = false, Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
	{
		if (error != Interop.Error.SUCCESS)
		{
			Interop.ThrowExceptionForIoErrno(error.Info(), path, isDirectory, errorRewriter);
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002081 File Offset: 0x00000281
	internal static long CheckIo(long result, string path = null, bool isDirectory = false, Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
	{
		if (result < 0L)
		{
			Interop.ThrowExceptionForIoErrno(Interop.Sys.GetLastErrorInfo(), path, isDirectory, errorRewriter);
		}
		return result;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002096 File Offset: 0x00000296
	internal static int CheckIo(int result, string path = null, bool isDirectory = false, Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
	{
		Interop.CheckIo((long)result, path, isDirectory, errorRewriter);
		return result;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
	internal static IntPtr CheckIo(IntPtr result, string path = null, bool isDirectory = false, Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null)
	{
		Interop.CheckIo((long)result, path, isDirectory, errorRewriter);
		return result;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020B6 File Offset: 0x000002B6
	internal static TSafeHandle CheckIo<TSafeHandle>(TSafeHandle handle, string path = null, bool isDirectory = false, Func<Interop.ErrorInfo, Interop.ErrorInfo> errorRewriter = null) where TSafeHandle : SafeHandle
	{
		if (handle.IsInvalid)
		{
			Interop.ThrowExceptionForIoErrno(Interop.Sys.GetLastErrorInfo(), path, isDirectory, errorRewriter);
		}
		return handle;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020D4 File Offset: 0x000002D4
	internal static Exception GetExceptionForIoErrno(Interop.ErrorInfo errorInfo, string path = null, bool isDirectory = false)
	{
		Interop.Error error = errorInfo.Error;
		if (error <= Interop.Error.ECANCELED)
		{
			if (error <= Interop.Error.EAGAIN)
			{
				if (error != Interop.Error.EACCES)
				{
					if (error != Interop.Error.EAGAIN)
					{
						goto IL_0196;
					}
					if (string.IsNullOrEmpty(path))
					{
						return new IOException("The process cannot access the file because it is being used by another process.", errorInfo.RawErrno);
					}
					return new IOException(SR.Format("The process cannot access the file '{0}' because it is being used by another process.", path), errorInfo.RawErrno);
				}
			}
			else if (error != Interop.Error.EBADF)
			{
				if (error != Interop.Error.ECANCELED)
				{
					goto IL_0196;
				}
				return new OperationCanceledException();
			}
		}
		else if (error <= Interop.Error.EFBIG)
		{
			if (error != Interop.Error.EEXIST)
			{
				if (error != Interop.Error.EFBIG)
				{
					goto IL_0196;
				}
				return new ArgumentOutOfRangeException("value", "Specified file length was too large for the file system.");
			}
			else
			{
				if (!string.IsNullOrEmpty(path))
				{
					return new IOException(SR.Format("The file '{0}' already exists.", path), errorInfo.RawErrno);
				}
				goto IL_0196;
			}
		}
		else if (error != Interop.Error.ENAMETOOLONG)
		{
			if (error != Interop.Error.ENOENT)
			{
				if (error != Interop.Error.EPERM)
				{
					goto IL_0196;
				}
			}
			else if (isDirectory)
			{
				if (string.IsNullOrEmpty(path))
				{
					return new DirectoryNotFoundException("Could not find a part of the path.");
				}
				return new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", path));
			}
			else
			{
				if (string.IsNullOrEmpty(path))
				{
					return new FileNotFoundException("Unable to find the specified file.");
				}
				return new FileNotFoundException(SR.Format("Could not find file '{0}'.", path), path);
			}
		}
		else
		{
			if (string.IsNullOrEmpty(path))
			{
				return new PathTooLongException("The specified file name or path is too long, or a component of the specified path is too long.");
			}
			return new PathTooLongException(SR.Format("The path '{0}' is too long, or a component of the specified path is too long.", path));
		}
		Exception ioexception = Interop.GetIOException(errorInfo);
		if (string.IsNullOrEmpty(path))
		{
			return new UnauthorizedAccessException("Access to the path is denied.", ioexception);
		}
		return new UnauthorizedAccessException(SR.Format("Access to the path '{0}' is denied.", path), ioexception);
		IL_0196:
		return Interop.GetIOException(errorInfo);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000227D File Offset: 0x0000047D
	internal static Exception GetIOException(Interop.ErrorInfo errorInfo)
	{
		return new IOException(errorInfo.GetErrorMessage(), errorInfo.RawErrno);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002294 File Offset: 0x00000494
	internal static bool CallStringMethod<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, StringBuilder, Interop.Globalization.ResultCode> interopCall, TArg1 arg1, TArg2 arg2, TArg3 arg3, out string result)
	{
		StringBuilder stringBuilder = StringBuilderCache.Acquire(80);
		for (int i = 0; i < 5; i++)
		{
			Interop.Globalization.ResultCode resultCode = interopCall(arg1, arg2, arg3, stringBuilder);
			if (resultCode == Interop.Globalization.ResultCode.Success)
			{
				result = StringBuilderCache.GetStringAndRelease(stringBuilder);
				return true;
			}
			if (resultCode != Interop.Globalization.ResultCode.InsufficentBuffer)
			{
				break;
			}
			stringBuilder.EnsureCapacity(stringBuilder.Capacity * 2);
		}
		StringBuilderCache.Release(stringBuilder);
		result = null;
		return false;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000022EC File Offset: 0x000004EC
	internal unsafe static void GetRandomBytes(byte* buffer, int length)
	{
		Interop.Sys.GetNonCryptographicallySecureRandomBytes(buffer, length);
	}

	// Token: 0x02000005 RID: 5
	internal enum Error
	{
		// Token: 0x04000002 RID: 2
		SUCCESS,
		// Token: 0x04000003 RID: 3
		E2BIG = 65537,
		// Token: 0x04000004 RID: 4
		EACCES,
		// Token: 0x04000005 RID: 5
		EADDRINUSE,
		// Token: 0x04000006 RID: 6
		EADDRNOTAVAIL,
		// Token: 0x04000007 RID: 7
		EAFNOSUPPORT,
		// Token: 0x04000008 RID: 8
		EAGAIN,
		// Token: 0x04000009 RID: 9
		EALREADY,
		// Token: 0x0400000A RID: 10
		EBADF,
		// Token: 0x0400000B RID: 11
		EBADMSG,
		// Token: 0x0400000C RID: 12
		EBUSY,
		// Token: 0x0400000D RID: 13
		ECANCELED,
		// Token: 0x0400000E RID: 14
		ECHILD,
		// Token: 0x0400000F RID: 15
		ECONNABORTED,
		// Token: 0x04000010 RID: 16
		ECONNREFUSED,
		// Token: 0x04000011 RID: 17
		ECONNRESET,
		// Token: 0x04000012 RID: 18
		EDEADLK,
		// Token: 0x04000013 RID: 19
		EDESTADDRREQ,
		// Token: 0x04000014 RID: 20
		EDOM,
		// Token: 0x04000015 RID: 21
		EDQUOT,
		// Token: 0x04000016 RID: 22
		EEXIST,
		// Token: 0x04000017 RID: 23
		EFAULT,
		// Token: 0x04000018 RID: 24
		EFBIG,
		// Token: 0x04000019 RID: 25
		EHOSTUNREACH,
		// Token: 0x0400001A RID: 26
		EIDRM,
		// Token: 0x0400001B RID: 27
		EILSEQ,
		// Token: 0x0400001C RID: 28
		EINPROGRESS,
		// Token: 0x0400001D RID: 29
		EINTR,
		// Token: 0x0400001E RID: 30
		EINVAL,
		// Token: 0x0400001F RID: 31
		EIO,
		// Token: 0x04000020 RID: 32
		EISCONN,
		// Token: 0x04000021 RID: 33
		EISDIR,
		// Token: 0x04000022 RID: 34
		ELOOP,
		// Token: 0x04000023 RID: 35
		EMFILE,
		// Token: 0x04000024 RID: 36
		EMLINK,
		// Token: 0x04000025 RID: 37
		EMSGSIZE,
		// Token: 0x04000026 RID: 38
		EMULTIHOP,
		// Token: 0x04000027 RID: 39
		ENAMETOOLONG,
		// Token: 0x04000028 RID: 40
		ENETDOWN,
		// Token: 0x04000029 RID: 41
		ENETRESET,
		// Token: 0x0400002A RID: 42
		ENETUNREACH,
		// Token: 0x0400002B RID: 43
		ENFILE,
		// Token: 0x0400002C RID: 44
		ENOBUFS,
		// Token: 0x0400002D RID: 45
		ENODEV = 65580,
		// Token: 0x0400002E RID: 46
		ENOENT,
		// Token: 0x0400002F RID: 47
		ENOEXEC,
		// Token: 0x04000030 RID: 48
		ENOLCK,
		// Token: 0x04000031 RID: 49
		ENOLINK,
		// Token: 0x04000032 RID: 50
		ENOMEM,
		// Token: 0x04000033 RID: 51
		ENOMSG,
		// Token: 0x04000034 RID: 52
		ENOPROTOOPT,
		// Token: 0x04000035 RID: 53
		ENOSPC,
		// Token: 0x04000036 RID: 54
		ENOSYS = 65591,
		// Token: 0x04000037 RID: 55
		ENOTCONN,
		// Token: 0x04000038 RID: 56
		ENOTDIR,
		// Token: 0x04000039 RID: 57
		ENOTEMPTY,
		// Token: 0x0400003A RID: 58
		ENOTSOCK = 65596,
		// Token: 0x0400003B RID: 59
		ENOTSUP,
		// Token: 0x0400003C RID: 60
		ENOTTY,
		// Token: 0x0400003D RID: 61
		ENXIO,
		// Token: 0x0400003E RID: 62
		EOVERFLOW,
		// Token: 0x0400003F RID: 63
		EPERM = 65602,
		// Token: 0x04000040 RID: 64
		EPIPE,
		// Token: 0x04000041 RID: 65
		EPROTO,
		// Token: 0x04000042 RID: 66
		EPROTONOSUPPORT,
		// Token: 0x04000043 RID: 67
		EPROTOTYPE,
		// Token: 0x04000044 RID: 68
		ERANGE,
		// Token: 0x04000045 RID: 69
		EROFS,
		// Token: 0x04000046 RID: 70
		ESPIPE,
		// Token: 0x04000047 RID: 71
		ESRCH,
		// Token: 0x04000048 RID: 72
		ESTALE,
		// Token: 0x04000049 RID: 73
		ETIMEDOUT = 65613,
		// Token: 0x0400004A RID: 74
		ETXTBSY,
		// Token: 0x0400004B RID: 75
		EXDEV,
		// Token: 0x0400004C RID: 76
		ESOCKTNOSUPPORT = 65630,
		// Token: 0x0400004D RID: 77
		EPFNOSUPPORT = 65632,
		// Token: 0x0400004E RID: 78
		ESHUTDOWN = 65644,
		// Token: 0x0400004F RID: 79
		EHOSTDOWN = 65648,
		// Token: 0x04000050 RID: 80
		ENODATA,
		// Token: 0x04000051 RID: 81
		EOPNOTSUPP = 65597,
		// Token: 0x04000052 RID: 82
		EWOULDBLOCK = 65542
	}

	// Token: 0x02000006 RID: 6
	internal struct ErrorInfo
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000022F5 File Offset: 0x000004F5
		internal ErrorInfo(int errno)
		{
			this._error = Interop.Sys.ConvertErrorPlatformToPal(errno);
			this._rawErrno = errno;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000230A File Offset: 0x0000050A
		internal ErrorInfo(Interop.Error error)
		{
			this._error = error;
			this._rawErrno = -1;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000231A File Offset: 0x0000051A
		internal Interop.Error Error
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002324 File Offset: 0x00000524
		internal int RawErrno
		{
			get
			{
				if (this._rawErrno != -1)
				{
					return this._rawErrno;
				}
				return this._rawErrno = Interop.Sys.ConvertErrorPalToPlatform(this._error);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002355 File Offset: 0x00000555
		internal string GetErrorMessage()
		{
			return Interop.Sys.StrError(this.RawErrno);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002362 File Offset: 0x00000562
		public override string ToString()
		{
			return string.Format("RawErrno: {0} Error: {1} GetErrorMessage: {2}", this.RawErrno, this.Error, this.GetErrorMessage());
		}

		// Token: 0x04000053 RID: 83
		private Interop.Error _error;

		// Token: 0x04000054 RID: 84
		private int _rawErrno;
	}

	// Token: 0x02000007 RID: 7
	internal static class Sys
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000238A File Offset: 0x0000058A
		internal static Interop.Error GetLastError()
		{
			return Interop.Sys.ConvertErrorPlatformToPal(Marshal.GetLastWin32Error());
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002396 File Offset: 0x00000596
		internal static Interop.ErrorInfo GetLastErrorInfo()
		{
			return new Interop.ErrorInfo(Marshal.GetLastWin32Error());
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023A4 File Offset: 0x000005A4
		internal unsafe static string StrError(int platformErrno)
		{
			int num = 1024;
			byte* ptr = stackalloc byte[(UIntPtr)num];
			byte* ptr2 = Interop.Sys.StrErrorR(platformErrno, ptr, num);
			if (ptr2 == null)
			{
				ptr2 = ptr;
			}
			return Marshal.PtrToStringAnsi((IntPtr)((void*)ptr2));
		}

		// Token: 0x06000016 RID: 22
		[DllImport("System.Native", EntryPoint = "SystemNative_ConvertErrorPlatformToPal")]
		internal static extern Interop.Error ConvertErrorPlatformToPal(int platformErrno);

		// Token: 0x06000017 RID: 23
		[DllImport("System.Native", EntryPoint = "SystemNative_ConvertErrorPalToPlatform")]
		internal static extern int ConvertErrorPalToPlatform(Interop.Error error);

		// Token: 0x06000018 RID: 24
		[DllImport("System.Native", EntryPoint = "SystemNative_StrErrorR")]
		private unsafe static extern byte* StrErrorR(int platformErrno, byte* buffer, int bufferSize);

		// Token: 0x06000019 RID: 25
		[DllImport("System.Native", EntryPoint = "SystemNative_GetNonCryptographicallySecureRandomBytes")]
		internal unsafe static extern void GetNonCryptographicallySecureRandomBytes(byte* buffer, int length);

		// Token: 0x0600001A RID: 26
		[DllImport("System.Native", EntryPoint = "SystemNative_GetReadDirRBufferSize")]
		internal static extern int GetReadDirRBufferSize();

		// Token: 0x0600001B RID: 27
		[DllImport("System.Native", EntryPoint = "SystemNative_ReadLink", SetLastError = true)]
		private static extern int ReadLink(string path, byte[] buffer, int bufferSize);

		// Token: 0x0600001C RID: 28 RVA: 0x000023D8 File Offset: 0x000005D8
		public static string ReadLink(string path)
		{
			int num = 256;
			string text;
			for (;;)
			{
				byte[] array = ArrayPool<byte>.Shared.Rent(num);
				try
				{
					int num2 = Interop.Sys.ReadLink(path, array, array.Length);
					if (num2 < 0)
					{
						text = null;
						break;
					}
					if (num2 < array.Length)
					{
						text = Encoding.UTF8.GetString(array, 0, num2);
						break;
					}
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(array, false);
				}
				num *= 2;
			}
			return text;
		}

		// Token: 0x0600001D RID: 29
		[DllImport("System.Native", EntryPoint = "SystemNative_FStat2", SetLastError = true)]
		internal static extern int FStat(SafeFileHandle fd, out Interop.Sys.FileStatus output);

		// Token: 0x0600001E RID: 30
		[DllImport("System.Native", EntryPoint = "SystemNative_Stat2", SetLastError = true)]
		internal static extern int Stat(string path, out Interop.Sys.FileStatus output);

		// Token: 0x0600001F RID: 31
		[DllImport("System.Native", EntryPoint = "SystemNative_LStat2", SetLastError = true)]
		internal static extern int LStat(string path, out Interop.Sys.FileStatus output);

		// Token: 0x06000020 RID: 32
		[DllImport("System.Native", EntryPoint = "SystemNative_Symlink", SetLastError = true)]
		internal static extern int Symlink(string target, string linkPath);

		// Token: 0x06000021 RID: 33
		[DllImport("System.Native", EntryPoint = "SystemNative_ChMod", SetLastError = true)]
		internal static extern int ChMod(string path, int mode);

		// Token: 0x06000022 RID: 34
		[DllImport("System.Native", EntryPoint = "SystemNative_CopyFile", SetLastError = true)]
		internal static extern int CopyFile(SafeFileHandle source, SafeFileHandle destination);

		// Token: 0x06000023 RID: 35
		[DllImport("System.Native", EntryPoint = "SystemNative_GetEGid")]
		internal static extern uint GetEGid();

		// Token: 0x06000024 RID: 36
		[DllImport("System.Native", EntryPoint = "SystemNative_GetEUid")]
		internal static extern uint GetEUid();

		// Token: 0x06000025 RID: 37
		[DllImport("System.Native", EntryPoint = "SystemNative_LChflags", SetLastError = true)]
		internal static extern int LChflags(string path, uint flags);

		// Token: 0x06000026 RID: 38
		[DllImport("System.Native", EntryPoint = "SystemNative_LChflagsCanSetHiddenFlag")]
		private static extern int LChflagsCanSetHiddenFlag();

		// Token: 0x06000027 RID: 39
		[DllImport("System.Native", EntryPoint = "SystemNative_Link", SetLastError = true)]
		internal static extern int Link(string source, string link);

		// Token: 0x06000028 RID: 40
		[DllImport("System.Native", EntryPoint = "SystemNative_MkDir", SetLastError = true)]
		internal static extern int MkDir(string path, int mode);

		// Token: 0x06000029 RID: 41
		[DllImport("System.Native", EntryPoint = "SystemNative_Rename", SetLastError = true)]
		internal static extern int Rename(string oldPath, string newPath);

		// Token: 0x0600002A RID: 42
		[DllImport("System.Native", EntryPoint = "SystemNative_RmDir", SetLastError = true)]
		internal static extern int RmDir(string path);

		// Token: 0x0600002B RID: 43
		[DllImport("System.Native", EntryPoint = "SystemNative_Stat2", SetLastError = true)]
		internal static extern int Stat(ref byte path, out Interop.Sys.FileStatus output);

		// Token: 0x0600002C RID: 44 RVA: 0x00002448 File Offset: 0x00000648
		internal unsafe static int Stat(ReadOnlySpan<char> path, out Interop.Sys.FileStatus output)
		{
			byte* ptr = stackalloc byte[(UIntPtr)256];
			ValueUtf8Converter valueUtf8Converter = new ValueUtf8Converter(new Span<byte>((void*)ptr, 256));
			int num = Interop.Sys.Stat(MemoryMarshal.GetReference<byte>(valueUtf8Converter.ConvertAndTerminateString(path)), out output);
			valueUtf8Converter.Dispose();
			return num;
		}

		// Token: 0x0600002D RID: 45
		[DllImport("System.Native", EntryPoint = "SystemNative_LStat2", SetLastError = true)]
		internal static extern int LStat(ref byte path, out Interop.Sys.FileStatus output);

		// Token: 0x0600002E RID: 46 RVA: 0x0000248C File Offset: 0x0000068C
		internal unsafe static int LStat(ReadOnlySpan<char> path, out Interop.Sys.FileStatus output)
		{
			byte* ptr = stackalloc byte[(UIntPtr)256];
			ValueUtf8Converter valueUtf8Converter = new ValueUtf8Converter(new Span<byte>((void*)ptr, 256));
			int num = Interop.Sys.LStat(MemoryMarshal.GetReference<byte>(valueUtf8Converter.ConvertAndTerminateString(path)), out output);
			valueUtf8Converter.Dispose();
			return num;
		}

		// Token: 0x0600002F RID: 47
		[DllImport("System.Native", EntryPoint = "SystemNative_UTime", SetLastError = true)]
		internal static extern int UTime(string path, ref Interop.Sys.UTimBuf time);

		// Token: 0x06000030 RID: 48
		[DllImport("System.Native", EntryPoint = "SystemNative_UTimes", SetLastError = true)]
		internal static extern int UTimes(string path, ref Interop.Sys.TimeValPair times);

		// Token: 0x06000031 RID: 49
		[DllImport("System.Native", EntryPoint = "SystemNative_Unlink", SetLastError = true)]
		internal static extern int Unlink(string pathname);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int DoubleToString(double value, byte* format, byte* buffer, int bufferLength);

		// Token: 0x06000033 RID: 51
		[DllImport("System.Native", EntryPoint = "SystemNative_OpenDir", SetLastError = true)]
		internal static extern IntPtr OpenDir_native(string path);

		// Token: 0x06000034 RID: 52
		[DllImport("System.Native", EntryPoint = "SystemNative_ReadDirR")]
		internal unsafe static extern int ReadDirR_native(IntPtr dir, byte* buffer, int bufferSize, out Interop.Sys.DirectoryEntry outputEntry);

		// Token: 0x06000035 RID: 53
		[DllImport("System.Native", EntryPoint = "SystemNative_CloseDir", SetLastError = true)]
		internal static extern int CloseDir_native(IntPtr dir);

		// Token: 0x06000036 RID: 54 RVA: 0x000024D0 File Offset: 0x000006D0
		internal static IntPtr OpenDir(string path)
		{
			IntPtr intPtr;
			do
			{
				intPtr = Interop.Sys.OpenDir_native(path);
			}
			while (intPtr == IntPtr.Zero && Marshal.GetLastWin32Error() == 65563);
			return intPtr;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002500 File Offset: 0x00000700
		internal static int CloseDir(IntPtr dir)
		{
			int num;
			do
			{
				num = Interop.Sys.CloseDir_native(dir);
			}
			while (num < 0 && Marshal.GetLastWin32Error() == 65563);
			return num;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002528 File Offset: 0x00000728
		internal unsafe static int ReadDirR(IntPtr dir, byte* buffer, int bufferSize, out Interop.Sys.DirectoryEntry outputEntry)
		{
			int num;
			do
			{
				num = Interop.Sys.ReadDirR_native(dir, buffer, bufferSize, out outputEntry);
			}
			while (num == 65563);
			return num;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002548 File Offset: 0x00000748
		// Note: this type is marked as 'beforefieldinit'.
		static Sys()
		{
		}

		// Token: 0x04000055 RID: 85
		internal static readonly bool CanSetHiddenFlag = Interop.Sys.LChflagsCanSetHiddenFlag() != 0;

		// Token: 0x04000056 RID: 86
		private const int StackBufferSize = 256;

		// Token: 0x02000008 RID: 8
		internal enum NodeType
		{
			// Token: 0x04000058 RID: 88
			DT_UNKNOWN,
			// Token: 0x04000059 RID: 89
			DT_FIFO,
			// Token: 0x0400005A RID: 90
			DT_CHR,
			// Token: 0x0400005B RID: 91
			DT_DIR = 4,
			// Token: 0x0400005C RID: 92
			DT_BLK = 6,
			// Token: 0x0400005D RID: 93
			DT_REG = 8,
			// Token: 0x0400005E RID: 94
			DT_LNK = 10,
			// Token: 0x0400005F RID: 95
			DT_SOCK = 12,
			// Token: 0x04000060 RID: 96
			DT_WHT = 14
		}

		// Token: 0x02000009 RID: 9
		internal struct DirectoryEntry
		{
			// Token: 0x0600003A RID: 58 RVA: 0x00002558 File Offset: 0x00000758
			internal unsafe ReadOnlySpan<char> GetName(Span<char> buffer)
			{
				ReadOnlySpan<byte> readOnlySpan = ((this.NameLength == -1) ? new ReadOnlySpan<byte>((void*)this.Name, new ReadOnlySpan<byte>((void*)this.Name, 256).IndexOf(0)) : new ReadOnlySpan<byte>((void*)this.Name, this.NameLength));
				int chars = Encoding.UTF8.GetChars(readOnlySpan, buffer);
				return buffer.Slice(0, chars);
			}

			// Token: 0x04000061 RID: 97
			internal unsafe byte* Name;

			// Token: 0x04000062 RID: 98
			internal int NameLength;

			// Token: 0x04000063 RID: 99
			internal Interop.Sys.NodeType InodeType;

			// Token: 0x04000064 RID: 100
			internal const int NameBufferSize = 256;
		}

		// Token: 0x0200000A RID: 10
		internal struct FileStatus
		{
			// Token: 0x04000065 RID: 101
			internal Interop.Sys.FileStatusFlags Flags;

			// Token: 0x04000066 RID: 102
			internal int Mode;

			// Token: 0x04000067 RID: 103
			internal uint Uid;

			// Token: 0x04000068 RID: 104
			internal uint Gid;

			// Token: 0x04000069 RID: 105
			internal long Size;

			// Token: 0x0400006A RID: 106
			internal long ATime;

			// Token: 0x0400006B RID: 107
			internal long ATimeNsec;

			// Token: 0x0400006C RID: 108
			internal long MTime;

			// Token: 0x0400006D RID: 109
			internal long MTimeNsec;

			// Token: 0x0400006E RID: 110
			internal long CTime;

			// Token: 0x0400006F RID: 111
			internal long CTimeNsec;

			// Token: 0x04000070 RID: 112
			internal long BirthTime;

			// Token: 0x04000071 RID: 113
			internal long BirthTimeNsec;

			// Token: 0x04000072 RID: 114
			internal long Dev;

			// Token: 0x04000073 RID: 115
			internal long Ino;

			// Token: 0x04000074 RID: 116
			internal uint UserFlags;
		}

		// Token: 0x0200000B RID: 11
		internal static class FileTypes
		{
			// Token: 0x04000075 RID: 117
			internal const int S_IFMT = 61440;

			// Token: 0x04000076 RID: 118
			internal const int S_IFIFO = 4096;

			// Token: 0x04000077 RID: 119
			internal const int S_IFCHR = 8192;

			// Token: 0x04000078 RID: 120
			internal const int S_IFDIR = 16384;

			// Token: 0x04000079 RID: 121
			internal const int S_IFREG = 32768;

			// Token: 0x0400007A RID: 122
			internal const int S_IFLNK = 40960;

			// Token: 0x0400007B RID: 123
			internal const int S_IFSOCK = 49152;
		}

		// Token: 0x0200000C RID: 12
		[Flags]
		internal enum FileStatusFlags
		{
			// Token: 0x0400007D RID: 125
			None = 0,
			// Token: 0x0400007E RID: 126
			HasBirthTime = 1
		}

		// Token: 0x0200000D RID: 13
		[Flags]
		internal enum UserFlags : uint
		{
			// Token: 0x04000080 RID: 128
			UF_HIDDEN = 32768U
		}

		// Token: 0x0200000E RID: 14
		[Flags]
		internal enum Permissions
		{
			// Token: 0x04000082 RID: 130
			Mask = 511,
			// Token: 0x04000083 RID: 131
			S_IRWXU = 448,
			// Token: 0x04000084 RID: 132
			S_IRUSR = 256,
			// Token: 0x04000085 RID: 133
			S_IWUSR = 128,
			// Token: 0x04000086 RID: 134
			S_IXUSR = 64,
			// Token: 0x04000087 RID: 135
			S_IRWXG = 56,
			// Token: 0x04000088 RID: 136
			S_IRGRP = 32,
			// Token: 0x04000089 RID: 137
			S_IWGRP = 16,
			// Token: 0x0400008A RID: 138
			S_IXGRP = 8,
			// Token: 0x0400008B RID: 139
			S_IRWXO = 7,
			// Token: 0x0400008C RID: 140
			S_IROTH = 4,
			// Token: 0x0400008D RID: 141
			S_IWOTH = 2,
			// Token: 0x0400008E RID: 142
			S_IXOTH = 1
		}

		// Token: 0x0200000F RID: 15
		internal struct UTimBuf
		{
			// Token: 0x0400008F RID: 143
			internal long AcTime;

			// Token: 0x04000090 RID: 144
			internal long ModTime;
		}

		// Token: 0x02000010 RID: 16
		internal struct TimeValPair
		{
			// Token: 0x04000091 RID: 145
			internal long ASec;

			// Token: 0x04000092 RID: 146
			internal long AUSec;

			// Token: 0x04000093 RID: 147
			internal long MSec;

			// Token: 0x04000094 RID: 148
			internal long MUSec;
		}
	}

	// Token: 0x02000011 RID: 17
	internal static class Libraries
	{
		// Token: 0x04000095 RID: 149
		internal const string GlobalizationNative = "System.Globalization.Native";

		// Token: 0x04000096 RID: 150
		internal const string SystemNative = "System.Native";
	}

	// Token: 0x02000012 RID: 18
	internal static class Globalization
	{
		// Token: 0x0600003B RID: 59
		[DllImport("System.Globalization.Native", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetTimeZoneDisplayName")]
		internal static extern Interop.Globalization.ResultCode GetTimeZoneDisplayName(string localeName, string timeZoneId, Interop.Globalization.TimeZoneDisplayNameType type, [Out] StringBuilder result, int resultLength);

		// Token: 0x02000013 RID: 19
		internal enum ResultCode
		{
			// Token: 0x04000098 RID: 152
			Success,
			// Token: 0x04000099 RID: 153
			UnknownError,
			// Token: 0x0400009A RID: 154
			InsufficentBuffer,
			// Token: 0x0400009B RID: 155
			OutOfMemory
		}

		// Token: 0x02000014 RID: 20
		internal enum TimeZoneDisplayNameType
		{
			// Token: 0x0400009D RID: 157
			Generic,
			// Token: 0x0400009E RID: 158
			Standard,
			// Token: 0x0400009F RID: 159
			DaylightSavings
		}
	}

	// Token: 0x02000015 RID: 21
	internal class Advapi32
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000025BE File Offset: 0x000007BE
		public Advapi32()
		{
		}

		// Token: 0x02000016 RID: 22
		internal class RegistryOptions
		{
			// Token: 0x0600003D RID: 61 RVA: 0x000025BE File Offset: 0x000007BE
			public RegistryOptions()
			{
			}

			// Token: 0x040000A0 RID: 160
			internal const int REG_OPTION_NON_VOLATILE = 0;

			// Token: 0x040000A1 RID: 161
			internal const int REG_OPTION_VOLATILE = 1;

			// Token: 0x040000A2 RID: 162
			internal const int REG_OPTION_CREATE_LINK = 2;

			// Token: 0x040000A3 RID: 163
			internal const int REG_OPTION_BACKUP_RESTORE = 4;
		}

		// Token: 0x02000017 RID: 23
		internal class RegistryView
		{
			// Token: 0x0600003E RID: 62 RVA: 0x000025BE File Offset: 0x000007BE
			public RegistryView()
			{
			}

			// Token: 0x040000A4 RID: 164
			internal const int KEY_WOW64_64KEY = 256;

			// Token: 0x040000A5 RID: 165
			internal const int KEY_WOW64_32KEY = 512;
		}

		// Token: 0x02000018 RID: 24
		internal class RegistryOperations
		{
			// Token: 0x0600003F RID: 63 RVA: 0x000025BE File Offset: 0x000007BE
			public RegistryOperations()
			{
			}

			// Token: 0x040000A6 RID: 166
			internal const int KEY_QUERY_VALUE = 1;

			// Token: 0x040000A7 RID: 167
			internal const int KEY_SET_VALUE = 2;

			// Token: 0x040000A8 RID: 168
			internal const int KEY_CREATE_SUB_KEY = 4;

			// Token: 0x040000A9 RID: 169
			internal const int KEY_ENUMERATE_SUB_KEYS = 8;

			// Token: 0x040000AA RID: 170
			internal const int KEY_NOTIFY = 16;

			// Token: 0x040000AB RID: 171
			internal const int KEY_CREATE_LINK = 32;

			// Token: 0x040000AC RID: 172
			internal const int KEY_READ = 131097;

			// Token: 0x040000AD RID: 173
			internal const int KEY_WRITE = 131078;

			// Token: 0x040000AE RID: 174
			internal const int SYNCHRONIZE = 1048576;

			// Token: 0x040000AF RID: 175
			internal const int READ_CONTROL = 131072;

			// Token: 0x040000B0 RID: 176
			internal const int STANDARD_RIGHTS_READ = 131072;

			// Token: 0x040000B1 RID: 177
			internal const int STANDARD_RIGHTS_WRITE = 131072;
		}

		// Token: 0x02000019 RID: 25
		internal class RegistryValues
		{
			// Token: 0x06000040 RID: 64 RVA: 0x000025BE File Offset: 0x000007BE
			public RegistryValues()
			{
			}

			// Token: 0x040000B2 RID: 178
			internal const int REG_NONE = 0;

			// Token: 0x040000B3 RID: 179
			internal const int REG_SZ = 1;

			// Token: 0x040000B4 RID: 180
			internal const int REG_EXPAND_SZ = 2;

			// Token: 0x040000B5 RID: 181
			internal const int REG_BINARY = 3;

			// Token: 0x040000B6 RID: 182
			internal const int REG_DWORD = 4;

			// Token: 0x040000B7 RID: 183
			internal const int REG_DWORD_LITTLE_ENDIAN = 4;

			// Token: 0x040000B8 RID: 184
			internal const int REG_DWORD_BIG_ENDIAN = 5;

			// Token: 0x040000B9 RID: 185
			internal const int REG_LINK = 6;

			// Token: 0x040000BA RID: 186
			internal const int REG_MULTI_SZ = 7;

			// Token: 0x040000BB RID: 187
			internal const int REG_QWORD = 11;
		}
	}
}
