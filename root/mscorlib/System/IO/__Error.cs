using System;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000975 RID: 2421
	internal static class __Error
	{
		// Token: 0x060057BB RID: 22459 RVA: 0x00128CF6 File Offset: 0x00126EF6
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(Environment.GetResourceString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x00128D07 File Offset: 0x00126F07
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed file."));
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x00128D19 File Offset: 0x00126F19
		internal static void StreamIsClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed Stream."));
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x00128D2B File Offset: 0x00126F2B
		internal static void MemoryStreamNotExpandable()
		{
			throw new NotSupportedException(Environment.GetResourceString("Memory stream is not expandable."));
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x00128D3C File Offset: 0x00126F3C
		internal static void ReaderClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot read from a closed TextReader."));
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x000A85E2 File Offset: 0x000A67E2
		internal static void ReadNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x000A85D1 File Offset: 0x000A67D1
		internal static void SeekNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x00128D4E File Offset: 0x00126F4E
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(Environment.GetResourceString("IAsyncResult object did not come from the corresponding async method on this type."));
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x00128D5F File Offset: 0x00126F5F
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("EndRead can only be called once for each asynchronous operation."));
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x00128D70 File Offset: 0x00126F70
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("EndWrite can only be called once for each asynchronous operation."));
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x00128D84 File Offset: 0x00126F84
		[SecurityCritical]
		internal static string GetDisplayablePath(string path, bool isInvalidPath)
		{
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (path.Length < 2)
			{
				return path;
			}
			if (PathInternal.IsPartiallyQualified(path) && !isInvalidPath)
			{
				return path;
			}
			bool flag = false;
			try
			{
				if (!isInvalidPath)
				{
					flag = true;
				}
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			if (!flag)
			{
				if (Path.IsDirectorySeparator(path[path.Length - 1]))
				{
					path = Environment.GetResourceString("<Path discovery permission to the specified directory was denied.>");
				}
				else
				{
					path = Path.GetFileName(path);
				}
			}
			return path;
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x00128E20 File Offset: 0x00127020
		[SecurityCritical]
		internal static void WinIOError(int errorCode, string maybeFullPath)
		{
			bool flag = errorCode == 123 || errorCode == 161;
			string displayablePath = __Error.GetDisplayablePath(maybeFullPath, flag);
			if (errorCode <= 80)
			{
				if (errorCode <= 15)
				{
					switch (errorCode)
					{
					case 2:
						if (displayablePath.Length == 0)
						{
							throw new FileNotFoundException(Environment.GetResourceString("Unable to find the specified file."));
						}
						throw new FileNotFoundException(Environment.GetResourceString("Could not find file '{0}'.", new object[] { displayablePath }), displayablePath);
					case 3:
						if (displayablePath.Length == 0)
						{
							throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path."));
						}
						throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path '{0}'.", new object[] { displayablePath }));
					case 4:
						break;
					case 5:
						if (displayablePath.Length == 0)
						{
							throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path is denied."));
						}
						throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path '{0}' is denied.", new object[] { displayablePath }));
					default:
						if (errorCode == 15)
						{
							throw new DriveNotFoundException(Environment.GetResourceString("Could not find the drive '{0}'. The drive might not be ready or might not be mapped.", new object[] { displayablePath }));
						}
						break;
					}
				}
				else if (errorCode != 32)
				{
					if (errorCode == 80)
					{
						if (displayablePath.Length != 0)
						{
							throw new IOException(Environment.GetResourceString("The file '{0}' already exists.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode));
						}
					}
				}
				else
				{
					if (displayablePath.Length == 0)
					{
						throw new IOException(Environment.GetResourceString("The process cannot access the file because it is being used by another process."), Win32Native.MakeHRFromErrorCode(errorCode));
					}
					throw new IOException(Environment.GetResourceString("The process cannot access the file '{0}' because it is being used by another process.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode));
				}
			}
			else if (errorCode <= 183)
			{
				if (errorCode == 87)
				{
					throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode));
				}
				if (errorCode == 183)
				{
					if (displayablePath.Length != 0)
					{
						throw new IOException(Environment.GetResourceString("Cannot create \"{0}\" because a file or directory with the same name already exists.", new object[] { displayablePath }), Win32Native.MakeHRFromErrorCode(errorCode));
					}
				}
			}
			else
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(Environment.GetResourceString("The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters."));
				}
				if (errorCode == 995)
				{
					throw new OperationCanceledException();
				}
			}
			throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x00129040 File Offset: 0x00127240
		internal static void WriteNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support writing."));
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x00129051 File Offset: 0x00127251
		internal static void WriterClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot write to a closed TextWriter."));
		}
	}
}
