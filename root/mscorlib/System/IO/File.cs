using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200094F RID: 2383
	public static class File
	{
		// Token: 0x060055F6 RID: 22006 RVA: 0x00121ABC File Offset: 0x0011FCBC
		public static StreamReader OpenText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamReader(path);
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x00121AD2 File Offset: 0x0011FCD2
		public static StreamWriter CreateText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamWriter(path, false);
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x00121AE9 File Offset: 0x0011FCE9
		public static StreamWriter AppendText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamWriter(path, true);
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x00121B00 File Offset: 0x0011FD00
		public static void Copy(string sourceFileName, string destFileName)
		{
			File.Copy(sourceFileName, destFileName, false);
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x00121B0C File Offset: 0x0011FD0C
		public static void Copy(string sourceFileName, string destFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", "File name cannot be null.");
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", "File name cannot be null.");
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			FileSystem.CopyFile(Path.GetFullPath(sourceFileName), Path.GetFullPath(destFileName), overwrite);
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00121B81 File Offset: 0x0011FD81
		public static FileStream Create(string path)
		{
			return File.Create(path, 4096);
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x00121B8E File Offset: 0x0011FD8E
		public static FileStream Create(string path, int bufferSize)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x00121B9A File Offset: 0x0011FD9A
		public static FileStream Create(string path, int bufferSize, FileOptions options)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize, options);
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x00121BA7 File Offset: 0x0011FDA7
		public static void Delete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			FileSystem.DeleteFile(Path.GetFullPath(path));
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x00121BC4 File Offset: 0x0011FDC4
		public static bool Exists(string path)
		{
			try
			{
				if (path == null)
				{
					return false;
				}
				if (path.Length == 0)
				{
					return false;
				}
				path = Path.GetFullPath(path);
				if (path.Length > 0 && PathInternal.IsDirectorySeparator(path[path.Length - 1]))
				{
					return false;
				}
				return FileSystem.FileExists(path);
			}
			catch (ArgumentException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x00121C50 File Offset: 0x0011FE50
		public static FileStream Open(string path, FileMode mode)
		{
			return File.Open(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x00121C62 File Offset: 0x0011FE62
		public static FileStream Open(string path, FileMode mode, FileAccess access)
		{
			return File.Open(path, mode, access, FileShare.None);
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00121C6D File Offset: 0x0011FE6D
		public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(path, mode, access, share);
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x00121C78 File Offset: 0x0011FE78
		internal static DateTimeOffset GetUtcDateTimeOffset(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Unspecified)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
			}
			return dateTime.ToUniversalTime();
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x00121C9C File Offset: 0x0011FE9C
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), creationTime, false);
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x00121CB0 File Offset: 0x0011FEB0
		public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(creationTimeUtc), false);
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x00121CC4 File Offset: 0x0011FEC4
		public static DateTime GetCreationTime(string path)
		{
			return FileSystem.GetCreationTime(Path.GetFullPath(path)).LocalDateTime;
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x00121CE4 File Offset: 0x0011FEE4
		public static DateTime GetCreationTimeUtc(string path)
		{
			return FileSystem.GetCreationTime(Path.GetFullPath(path)).UtcDateTime;
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x00121D04 File Offset: 0x0011FF04
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), lastAccessTime, false);
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x00121D18 File Offset: 0x0011FF18
		public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastAccessTimeUtc), false);
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x00121D2C File Offset: 0x0011FF2C
		public static DateTime GetLastAccessTime(string path)
		{
			return FileSystem.GetLastAccessTime(Path.GetFullPath(path)).LocalDateTime;
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x00121D4C File Offset: 0x0011FF4C
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return FileSystem.GetLastAccessTime(Path.GetFullPath(path)).UtcDateTime;
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x00121D6C File Offset: 0x0011FF6C
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), lastWriteTime, false);
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x00121D80 File Offset: 0x0011FF80
		public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastWriteTimeUtc), false);
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x00121D94 File Offset: 0x0011FF94
		public static DateTime GetLastWriteTime(string path)
		{
			return FileSystem.GetLastWriteTime(Path.GetFullPath(path)).LocalDateTime;
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x00121DB4 File Offset: 0x0011FFB4
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return FileSystem.GetLastWriteTime(Path.GetFullPath(path)).UtcDateTime;
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x00121DD4 File Offset: 0x0011FFD4
		public static FileAttributes GetAttributes(string path)
		{
			return FileSystem.GetAttributes(Path.GetFullPath(path));
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x00121DE4 File Offset: 0x0011FFE4
		public static void SetAttributes(string path, FileAttributes fileAttributes)
		{
			if ((fileAttributes & (FileAttributes)(-2147483648)) == (FileAttributes)0)
			{
				FileSystem.SetAttributes(Path.GetFullPath(path), fileAttributes);
				return;
			}
			Path.Validate(path);
			MonoIOError monoIOError;
			if (!MonoIO.SetFileAttributes(path, fileAttributes, out monoIOError))
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x00121E20 File Offset: 0x00120020
		public static FileStream OpenRead(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x00121E2B File Offset: 0x0012002B
		public static FileStream OpenWrite(string path)
		{
			return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00121E36 File Offset: 0x00120036
		public static string ReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return File.InternalReadAllText(path, Encoding.UTF8);
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x00121E69 File Offset: 0x00120069
		public static string ReadAllText(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return File.InternalReadAllText(path, encoding);
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x00121EA8 File Offset: 0x001200A8
		private static string InternalReadAllText(string path, Encoding encoding)
		{
			string text;
			using (StreamReader streamReader = new StreamReader(path, encoding, true))
			{
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x00121EE4 File Offset: 0x001200E4
		public static void WriteAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x00121F44 File Offset: 0x00120144
		public static void WriteAllText(string path, string contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			using (StreamWriter streamWriter = new StreamWriter(path, false, encoding))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x00121FB4 File Offset: 0x001201B4
		public static byte[] ReadAllBytes(string path)
		{
			byte[] array;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 1))
			{
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new IOException("The file is too long. This operation is currently limited to supporting files less than 2 gigabytes in size.");
				}
				if (length == 0L)
				{
					array = File.ReadAllBytesUnknownLength(fileStream);
				}
				else
				{
					int num = 0;
					int i = (int)length;
					byte[] array2 = new byte[i];
					while (i > 0)
					{
						int num2 = fileStream.Read(array2, num, i);
						if (num2 == 0)
						{
							throw Error.GetEndOfFile();
						}
						num += num2;
						i -= num2;
					}
					array = array2;
				}
			}
			return array;
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x0012204C File Offset: 0x0012024C
		private unsafe static byte[] ReadAllBytesUnknownLength(FileStream fs)
		{
			byte[] array = null;
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)512], 512);
			byte[] array3;
			try
			{
				int num = 0;
				for (;;)
				{
					if (num == span.Length)
					{
						uint num2 = (uint)(span.Length * 2);
						if (num2 > 2147483591U)
						{
							num2 = (uint)Math.Max(2147483591, span.Length + 1);
						}
						byte[] array2 = ArrayPool<byte>.Shared.Rent((int)num2);
						span.CopyTo(array2);
						if (array != null)
						{
							ArrayPool<byte>.Shared.Return(array, false);
						}
						span = (array = array2);
					}
					int num3 = fs.Read(span.Slice(num));
					if (num3 == 0)
					{
						break;
					}
					num += num3;
				}
				array3 = span.Slice(0, num).ToArray();
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return array3;
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x0012212C File Offset: 0x0012032C
		public static void WriteAllBytes(string path, byte[] bytes)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", "Path cannot be null.");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			File.InternalWriteAllBytes(path, bytes);
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x0012217C File Offset: 0x0012037C
		private static void InternalWriteAllBytes(string path, byte[] bytes)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x001221BC File Offset: 0x001203BC
		public static string[] ReadAllLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return File.InternalReadAllLines(path, Encoding.UTF8);
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x001221EF File Offset: 0x001203EF
		public static string[] ReadAllLines(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return File.InternalReadAllLines(path, encoding);
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x0012222C File Offset: 0x0012042C
		private static string[] InternalReadAllLines(string path, Encoding encoding)
		{
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(path, encoding))
			{
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x0012227C File Offset: 0x0012047C
		public static IEnumerable<string> ReadLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return ReadLinesIterator.CreateIterator(path, Encoding.UTF8);
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x001222AF File Offset: 0x001204AF
		public static IEnumerable<string> ReadLines(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			return ReadLinesIterator.CreateIterator(path, encoding);
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x001222EC File Offset: 0x001204EC
		public static void WriteAllLines(string path, string[] contents)
		{
			File.WriteAllLines(path, contents);
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x001222F8 File Offset: 0x001204F8
		public static void WriteAllLines(string path, IEnumerable<string> contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			File.InternalWriteAllLines(new StreamWriter(path), contents);
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x00122345 File Offset: 0x00120545
		public static void WriteAllLines(string path, string[] contents, Encoding encoding)
		{
			File.WriteAllLines(path, contents, encoding);
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x00122350 File Offset: 0x00120550
		public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, encoding), contents);
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x001223B0 File Offset: 0x001205B0
		private static void InternalWriteAllLines(TextWriter writer, IEnumerable<string> contents)
		{
			try
			{
				foreach (string text in contents)
				{
					writer.WriteLine(text);
				}
			}
			finally
			{
				if (writer != null)
				{
					((IDisposable)writer).Dispose();
				}
			}
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x00122410 File Offset: 0x00120610
		public static void AppendAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			using (StreamWriter streamWriter = new StreamWriter(path, true))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x00122470 File Offset: 0x00120670
		public static void AppendAllText(string path, string contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			using (StreamWriter streamWriter = new StreamWriter(path, true, encoding))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x001224E0 File Offset: 0x001206E0
		public static void AppendAllLines(string path, IEnumerable<string> contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			File.InternalWriteAllLines(new StreamWriter(path, true), contents);
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x00122530 File Offset: 0x00120730
		public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			File.InternalWriteAllLines(new StreamWriter(path, true, encoding), contents);
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x0012258D File Offset: 0x0012078D
		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
		{
			File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, false);
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x00122598 File Offset: 0x00120798
		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			FileSystem.ReplaceFile(Path.GetFullPath(sourceFileName), Path.GetFullPath(destinationFileName), (destinationBackupFileName != null) ? Path.GetFullPath(destinationBackupFileName) : null, ignoreMetadataErrors);
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x001225D4 File Offset: 0x001207D4
		public static void Move(string sourceFileName, string destFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", "File name cannot be null.");
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", "File name cannot be null.");
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			string fullPath = Path.GetFullPath(sourceFileName);
			string fullPath2 = Path.GetFullPath(destFileName);
			if (!FileSystem.FileExists(fullPath))
			{
				throw new FileNotFoundException(SR.Format("Could not find file '{0}'.", fullPath), fullPath);
			}
			FileSystem.MoveFile(fullPath, fullPath2);
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x0012266B File Offset: 0x0012086B
		public static void Encrypt(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			throw new PlatformNotSupportedException("File encryption is not supported on this platform.");
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x0012266B File Offset: 0x0012086B
		public static void Decrypt(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			throw new PlatformNotSupportedException("File encryption is not supported on this platform.");
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06005630 RID: 22064 RVA: 0x00122685 File Offset: 0x00120885
		private static Encoding UTF8NoBOM
		{
			get
			{
				Encoding encoding;
				if ((encoding = File.s_UTF8NoBOM) == null)
				{
					encoding = (File.s_UTF8NoBOM = new UTF8Encoding(false, true));
				}
				return encoding;
			}
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x0012269D File Offset: 0x0012089D
		private static StreamReader AsyncStreamReader(string path, Encoding encoding)
		{
			return new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan), encoding, true);
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x001226B9 File Offset: 0x001208B9
		private static StreamWriter AsyncStreamWriter(string path, Encoding encoding, bool append)
		{
			return new StreamWriter(new FileStream(path, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan), encoding);
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x001226DA File Offset: 0x001208DA
		public static Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
		{
			return File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x001226E8 File Offset: 0x001208E8
		public static Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return File.InternalReadAllTextAsync(path, encoding, cancellationToken);
			}
			return Task.FromCanceled<string>(cancellationToken);
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00122744 File Offset: 0x00120944
		private static async Task<string> InternalReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken)
		{
			char[] buffer = null;
			StreamReader sr = File.AsyncStreamReader(path, encoding);
			string text;
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				buffer = ArrayPool<char>.Shared.Rent(sr.CurrentEncoding.GetMaxCharCount(4096));
				StringBuilder sb = new StringBuilder();
				for (;;)
				{
					int num = await sr.ReadAsync(new Memory<char>(buffer), cancellationToken).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					sb.Append(buffer, 0, num);
				}
				text = sb.ToString();
			}
			finally
			{
				sr.Dispose();
				if (buffer != null)
				{
					ArrayPool<char>.Shared.Return(buffer, false);
				}
			}
			return text;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x00122797 File Offset: 0x00120997
		public static Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = default(CancellationToken))
		{
			return File.WriteAllTextAsync(path, contents, File.UTF8NoBOM, cancellationToken);
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x001227A8 File Offset: 0x001209A8
		public static Task WriteAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (string.IsNullOrEmpty(contents))
			{
				new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read).Dispose();
				return Task.CompletedTask;
			}
			return File.InternalWriteAllTextAsync(File.AsyncStreamWriter(path, encoding, false), contents, cancellationToken);
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x00122824 File Offset: 0x00120A24
		public static Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<byte[]>(cancellationToken);
			}
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 1, FileOptions.Asynchronous | FileOptions.SequentialScan);
			bool flag = false;
			Task<byte[]> task;
			try
			{
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					task = Task.FromException<byte[]>(new IOException("The file is too long. This operation is currently limited to supporting files less than 2 gigabytes in size."));
				}
				else
				{
					flag = true;
					task = ((length > 0L) ? File.InternalReadAllBytesAsync(fileStream, (int)length, cancellationToken) : File.InternalReadAllBytesUnknownLengthAsync(fileStream, cancellationToken));
				}
			}
			finally
			{
				if (!flag)
				{
					fileStream.Dispose();
				}
			}
			return task;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x001228AC File Offset: 0x00120AAC
		private static async Task<byte[]> InternalReadAllBytesAsync(FileStream fs, int count, CancellationToken cancellationToken)
		{
			byte[] array;
			try
			{
				int index = 0;
				byte[] bytes = new byte[count];
				for (;;)
				{
					int num = await fs.ReadAsync(new Memory<byte>(bytes, index, count - index), cancellationToken).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					index += num;
					if (index >= count)
					{
						goto Block_3;
					}
				}
				throw Error.GetEndOfFile();
				Block_3:
				array = bytes;
			}
			finally
			{
				if (fs != null)
				{
					((IDisposable)fs).Dispose();
				}
			}
			return array;
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x00122900 File Offset: 0x00120B00
		private static async Task<byte[]> InternalReadAllBytesUnknownLengthAsync(FileStream fs, CancellationToken cancellationToken)
		{
			byte[] rentedArray = ArrayPool<byte>.Shared.Rent(512);
			byte[] array2;
			try
			{
				int bytesRead = 0;
				for (;;)
				{
					if (bytesRead == rentedArray.Length)
					{
						uint num = (uint)(rentedArray.Length * 2);
						if (num > 2147483591U)
						{
							num = (uint)Math.Max(2147483591, rentedArray.Length + 1);
						}
						byte[] array = ArrayPool<byte>.Shared.Rent((int)num);
						Buffer.BlockCopy(rentedArray, 0, array, 0, bytesRead);
						ArrayPool<byte>.Shared.Return(rentedArray, false);
						rentedArray = array;
					}
					int num2 = await fs.ReadAsync(rentedArray.AsMemory(bytesRead), cancellationToken).ConfigureAwait(false);
					if (num2 == 0)
					{
						break;
					}
					bytesRead += num2;
				}
				array2 = rentedArray.AsSpan(0, bytesRead).ToArray();
			}
			finally
			{
				fs.Dispose();
				ArrayPool<byte>.Shared.Return(rentedArray, false);
			}
			return array2;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x0012294C File Offset: 0x00120B4C
		public static Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", "Path cannot be null.");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return File.InternalWriteAllBytesAsync(path, bytes, cancellationToken);
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x001229AC File Offset: 0x00120BAC
		private static async Task InternalWriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken)
		{
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
			{
				await fs.WriteAsync(new ReadOnlyMemory<byte>(bytes), cancellationToken).ConfigureAwait(false);
				await fs.FlushAsync(cancellationToken).ConfigureAwait(false);
			}
			FileStream fs = null;
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x001229FF File Offset: 0x00120BFF
		public static Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
		{
			return File.ReadAllLinesAsync(path, Encoding.UTF8, cancellationToken);
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x00122A10 File Offset: 0x00120C10
		public static Task<string[]> ReadAllLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return File.InternalReadAllLinesAsync(path, encoding, cancellationToken);
			}
			return Task.FromCanceled<string[]>(cancellationToken);
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x00122A6C File Offset: 0x00120C6C
		private static async Task<string[]> InternalReadAllLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken)
		{
			string[] array;
			using (StreamReader sr = File.AsyncStreamReader(path, encoding))
			{
				cancellationToken.ThrowIfCancellationRequested();
				List<string> lines = new List<string>();
				string text;
				while ((text = await sr.ReadLineAsync().ConfigureAwait(false)) != null)
				{
					lines.Add(text);
					cancellationToken.ThrowIfCancellationRequested();
				}
				array = lines.ToArray();
			}
			return array;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x00122ABF File Offset: 0x00120CBF
		public static Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default(CancellationToken))
		{
			return File.WriteAllLinesAsync(path, contents, File.UTF8NoBOM, cancellationToken);
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x00122AD0 File Offset: 0x00120CD0
		public static Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return File.InternalWriteAllLinesAsync(File.AsyncStreamWriter(path, encoding, false), contents, cancellationToken);
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x00122B40 File Offset: 0x00120D40
		private static async Task InternalWriteAllLinesAsync(TextWriter writer, IEnumerable<string> contents, CancellationToken cancellationToken)
		{
			using (writer)
			{
				foreach (string text in contents)
				{
					cancellationToken.ThrowIfCancellationRequested();
					await writer.WriteLineAsync(text).ConfigureAwait(false);
				}
				IEnumerator<string> enumerator = null;
				cancellationToken.ThrowIfCancellationRequested();
				await writer.FlushAsync().ConfigureAwait(false);
			}
			TextWriter textWriter = null;
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x00122B94 File Offset: 0x00120D94
		private static async Task InternalWriteAllTextAsync(StreamWriter sw, string contents, CancellationToken cancellationToken)
		{
			char[] buffer = null;
			try
			{
				buffer = ArrayPool<char>.Shared.Rent(4096);
				int count = contents.Length;
				int batchSize;
				for (int index = 0; index < count; index += batchSize)
				{
					batchSize = Math.Min(4096, count - index);
					contents.CopyTo(index, buffer, 0, batchSize);
					await sw.WriteAsync(new ReadOnlyMemory<char>(buffer, 0, batchSize), cancellationToken).ConfigureAwait(false);
				}
				cancellationToken.ThrowIfCancellationRequested();
				await sw.FlushAsync().ConfigureAwait(false);
			}
			finally
			{
				sw.Dispose();
				if (buffer != null)
				{
					ArrayPool<char>.Shared.Return(buffer, false);
				}
			}
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x00122BE7 File Offset: 0x00120DE7
		public static Task AppendAllTextAsync(string path, string contents, CancellationToken cancellationToken = default(CancellationToken))
		{
			return File.AppendAllTextAsync(path, contents, File.UTF8NoBOM, cancellationToken);
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x00122BF8 File Offset: 0x00120DF8
		public static Task AppendAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (string.IsNullOrEmpty(contents))
			{
				new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read).Dispose();
				return Task.CompletedTask;
			}
			return File.InternalWriteAllTextAsync(File.AsyncStreamWriter(path, encoding, true), contents, cancellationToken);
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x00122C74 File Offset: 0x00120E74
		public static Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default(CancellationToken))
		{
			return File.AppendAllLinesAsync(path, contents, File.UTF8NoBOM, cancellationToken);
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x00122C84 File Offset: 0x00120E84
		public static Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.", "path");
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return File.InternalWriteAllLinesAsync(File.AsyncStreamWriter(path, encoding, true), contents, cancellationToken);
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x00121B9A File Offset: 0x0011FD9A
		public static FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize, options);
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x00122CF2 File Offset: 0x00120EF2
		public static FileSecurity GetAccessControl(string path)
		{
			return File.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x00122CFC File Offset: 0x00120EFC
		public static FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new FileSecurity(path, includeSections);
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x00122D05 File Offset: 0x00120F05
		public static void SetAccessControl(string path, FileSecurity fileSecurity)
		{
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			fileSecurity.PersistModifications(path);
		}

		// Token: 0x0400340B RID: 13323
		private const int MaxByteArrayLength = 2147483591;

		// Token: 0x0400340C RID: 13324
		private static Encoding s_UTF8NoBOM;

		// Token: 0x0400340D RID: 13325
		internal const int DefaultBufferSize = 4096;

		// Token: 0x02000950 RID: 2384
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadAllTextAsync>d__67 : IAsyncStateMachine
		{
			// Token: 0x0600564C RID: 22092 RVA: 0x00122D1C File Offset: 0x00120F1C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				string text;
				try
				{
					if (num != 0)
					{
						buffer = null;
						sr = File.AsyncStreamReader(path, encoding);
					}
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						if (num == 0)
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_00E2;
						}
						cancellationToken.ThrowIfCancellationRequested();
						buffer = ArrayPool<char>.Shared.Rent(sr.CurrentEncoding.GetMaxCharCount(4096));
						sb = new StringBuilder();
						IL_006A:
						configuredValueTaskAwaiter = sr.ReadAsync(new Memory<char>(buffer), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, File.<InternalReadAllTextAsync>d__67>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						IL_00E2:
						int result = configuredValueTaskAwaiter.GetResult();
						if (result != 0)
						{
							sb.Append(buffer, 0, result);
							goto IL_006A;
						}
						text = sb.ToString();
					}
					finally
					{
						if (num < 0)
						{
							sr.Dispose();
							if (buffer != null)
							{
								ArrayPool<char>.Shared.Return(buffer, false);
							}
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					buffer = null;
					sr = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				buffer = null;
				sr = null;
				this.<>t__builder.SetResult(text);
			}

			// Token: 0x0600564D RID: 22093 RVA: 0x00122EE4 File Offset: 0x001210E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400340E RID: 13326
			public int <>1__state;

			// Token: 0x0400340F RID: 13327
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04003410 RID: 13328
			public string path;

			// Token: 0x04003411 RID: 13329
			public Encoding encoding;

			// Token: 0x04003412 RID: 13330
			public CancellationToken cancellationToken;

			// Token: 0x04003413 RID: 13331
			private char[] <buffer>5__2;

			// Token: 0x04003414 RID: 13332
			private StreamReader <sr>5__3;

			// Token: 0x04003415 RID: 13333
			private StringBuilder <sb>5__4;

			// Token: 0x04003416 RID: 13334
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}

		// Token: 0x02000951 RID: 2385
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadAllBytesAsync>d__71 : IAsyncStateMachine
		{
			// Token: 0x0600564E RID: 22094 RVA: 0x00122EF4 File Offset: 0x001210F4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				byte[] array;
				try
				{
					FileStream fileStream;
					if (num != 0)
					{
						fileStream = fs;
					}
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						if (num == 0)
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_00C0;
						}
						index = 0;
						bytes = new byte[count];
						IL_0035:
						configuredValueTaskAwaiter = fs.ReadAsync(new Memory<byte>(bytes, index, count - index), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, File.<InternalReadAllBytesAsync>d__71>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						IL_00C0:
						int result = configuredValueTaskAwaiter.GetResult();
						if (result == 0)
						{
							throw Error.GetEndOfFile();
						}
						index += result;
						if (index < count)
						{
							goto IL_0035;
						}
						array = bytes;
					}
					finally
					{
						if (num < 0 && fileStream != null)
						{
							((IDisposable)fileStream).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(array);
			}

			// Token: 0x0600564F RID: 22095 RVA: 0x00123074 File Offset: 0x00121274
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003417 RID: 13335
			public int <>1__state;

			// Token: 0x04003418 RID: 13336
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x04003419 RID: 13337
			public FileStream fs;

			// Token: 0x0400341A RID: 13338
			public int count;

			// Token: 0x0400341B RID: 13339
			public CancellationToken cancellationToken;

			// Token: 0x0400341C RID: 13340
			private FileStream <>7__wrap1;

			// Token: 0x0400341D RID: 13341
			private int <index>5__3;

			// Token: 0x0400341E RID: 13342
			private byte[] <bytes>5__4;

			// Token: 0x0400341F RID: 13343
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}

		// Token: 0x02000952 RID: 2386
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadAllBytesUnknownLengthAsync>d__72 : IAsyncStateMachine
		{
			// Token: 0x06005650 RID: 22096 RVA: 0x00123084 File Offset: 0x00121284
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				byte[] array2;
				try
				{
					if (num != 0)
					{
						rentedArray = ArrayPool<byte>.Shared.Rent(512);
					}
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						if (num == 0)
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_0121;
						}
						bytesRead = 0;
						IL_002D:
						if (bytesRead == rentedArray.Length)
						{
							uint num3 = (uint)(rentedArray.Length * 2);
							if (num3 > 2147483591U)
							{
								num3 = (uint)Math.Max(2147483591, rentedArray.Length + 1);
							}
							byte[] array = ArrayPool<byte>.Shared.Rent((int)num3);
							Buffer.BlockCopy(rentedArray, 0, array, 0, bytesRead);
							ArrayPool<byte>.Shared.Return(rentedArray, false);
							rentedArray = array;
						}
						configuredValueTaskAwaiter = fs.ReadAsync(rentedArray.AsMemory(bytesRead), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, File.<InternalReadAllBytesUnknownLengthAsync>d__72>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						IL_0121:
						int result = configuredValueTaskAwaiter.GetResult();
						if (result != 0)
						{
							bytesRead += result;
							goto IL_002D;
						}
						array2 = rentedArray.AsSpan(0, bytesRead).ToArray();
					}
					finally
					{
						if (num < 0)
						{
							fs.Dispose();
							ArrayPool<byte>.Shared.Return(rentedArray, false);
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					rentedArray = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				rentedArray = null;
				this.<>t__builder.SetResult(array2);
			}

			// Token: 0x06005651 RID: 22097 RVA: 0x00123280 File Offset: 0x00121480
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003420 RID: 13344
			public int <>1__state;

			// Token: 0x04003421 RID: 13345
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x04003422 RID: 13346
			public FileStream fs;

			// Token: 0x04003423 RID: 13347
			public CancellationToken cancellationToken;

			// Token: 0x04003424 RID: 13348
			private byte[] <rentedArray>5__2;

			// Token: 0x04003425 RID: 13349
			private int <bytesRead>5__3;

			// Token: 0x04003426 RID: 13350
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}

		// Token: 0x02000953 RID: 2387
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalWriteAllBytesAsync>d__74 : IAsyncStateMachine
		{
			// Token: 0x06005652 RID: 22098 RVA: 0x00123290 File Offset: 0x00121490
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					if (num > 1)
					{
						fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						if (num != 0)
						{
							if (num == 1)
							{
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (num2 = -1);
								goto IL_011A;
							}
							configuredValueTaskAwaiter = fs.WriteAsync(new ReadOnlyMemory<byte>(bytes), cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredValueTaskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, File.<InternalWriteAllBytesAsync>d__74>(ref configuredValueTaskAwaiter, ref this);
								return;
							}
						}
						else
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
						}
						configuredValueTaskAwaiter.GetResult();
						configuredTaskAwaiter = fs.FlushAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num = (num2 = 1);
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, File.<InternalWriteAllBytesAsync>d__74>(ref configuredTaskAwaiter, ref this);
							return;
						}
						IL_011A:
						configuredTaskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0 && fs != null)
						{
							((IDisposable)fs).Dispose();
						}
					}
					fs = null;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06005653 RID: 22099 RVA: 0x00123444 File Offset: 0x00121644
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003427 RID: 13351
			public int <>1__state;

			// Token: 0x04003428 RID: 13352
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003429 RID: 13353
			public string path;

			// Token: 0x0400342A RID: 13354
			public byte[] bytes;

			// Token: 0x0400342B RID: 13355
			public CancellationToken cancellationToken;

			// Token: 0x0400342C RID: 13356
			private FileStream <fs>5__2;

			// Token: 0x0400342D RID: 13357
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x0400342E RID: 13358
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000954 RID: 2388
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadAllLinesAsync>d__77 : IAsyncStateMachine
		{
			// Token: 0x06005654 RID: 22100 RVA: 0x00123454 File Offset: 0x00121654
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				string[] array;
				try
				{
					if (num != 0)
					{
						sr = File.AsyncStreamReader(path, encoding);
					}
					try
					{
						ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter;
						if (num == 0)
						{
							ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_00B4;
						}
						cancellationToken.ThrowIfCancellationRequested();
						lines = new List<string>();
						IL_0054:
						configuredTaskAwaiter = sr.ReadLineAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, File.<InternalReadAllLinesAsync>d__77>(ref configuredTaskAwaiter, ref this);
							return;
						}
						IL_00B4:
						string result;
						if ((result = configuredTaskAwaiter.GetResult()) != null)
						{
							lines.Add(result);
							cancellationToken.ThrowIfCancellationRequested();
							goto IL_0054;
						}
						array = lines.ToArray();
					}
					finally
					{
						if (num < 0 && sr != null)
						{
							((IDisposable)sr).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(array);
			}

			// Token: 0x06005655 RID: 22101 RVA: 0x00123594 File Offset: 0x00121794
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400342F RID: 13359
			public int <>1__state;

			// Token: 0x04003430 RID: 13360
			public AsyncTaskMethodBuilder<string[]> <>t__builder;

			// Token: 0x04003431 RID: 13361
			public string path;

			// Token: 0x04003432 RID: 13362
			public Encoding encoding;

			// Token: 0x04003433 RID: 13363
			public CancellationToken cancellationToken;

			// Token: 0x04003434 RID: 13364
			private StreamReader <sr>5__2;

			// Token: 0x04003435 RID: 13365
			private List<string> <lines>5__3;

			// Token: 0x04003436 RID: 13366
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000955 RID: 2389
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalWriteAllLinesAsync>d__80 : IAsyncStateMachine
		{
			// Token: 0x06005656 RID: 22102 RVA: 0x001235A4 File Offset: 0x001217A4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					if (num > 1)
					{
						textWriter = writer;
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
						if (num != 0)
						{
							if (num == 1)
							{
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (num2 = -1);
								goto IL_0158;
							}
							enumerator = contents.GetEnumerator();
						}
						try
						{
							if (num != 0)
							{
								goto IL_00BD;
							}
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							IL_00B6:
							configuredTaskAwaiter.GetResult();
							IL_00BD:
							if (enumerator.MoveNext())
							{
								string text = enumerator.Current;
								cancellationToken.ThrowIfCancellationRequested();
								configuredTaskAwaiter = writer.WriteLineAsync(text).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									num = (num2 = 0);
									configuredTaskAwaiter2 = configuredTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, File.<InternalWriteAllLinesAsync>d__80>(ref configuredTaskAwaiter, ref this);
									return;
								}
								goto IL_00B6;
							}
						}
						finally
						{
							if (num < 0 && enumerator != null)
							{
								enumerator.Dispose();
							}
						}
						enumerator = null;
						cancellationToken.ThrowIfCancellationRequested();
						configuredTaskAwaiter = writer.FlushAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num = (num2 = 1);
							configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, File.<InternalWriteAllLinesAsync>d__80>(ref configuredTaskAwaiter, ref this);
							return;
						}
						IL_0158:
						configuredTaskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0 && textWriter != null)
						{
							((IDisposable)textWriter).Dispose();
						}
					}
					textWriter = null;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06005657 RID: 22103 RVA: 0x001237AC File Offset: 0x001219AC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003437 RID: 13367
			public int <>1__state;

			// Token: 0x04003438 RID: 13368
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003439 RID: 13369
			public TextWriter writer;

			// Token: 0x0400343A RID: 13370
			public IEnumerable<string> contents;

			// Token: 0x0400343B RID: 13371
			public CancellationToken cancellationToken;

			// Token: 0x0400343C RID: 13372
			private TextWriter <>7__wrap1;

			// Token: 0x0400343D RID: 13373
			private IEnumerator<string> <>7__wrap2;

			// Token: 0x0400343E RID: 13374
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000956 RID: 2390
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalWriteAllTextAsync>d__81 : IAsyncStateMachine
		{
			// Token: 0x06005658 RID: 22104 RVA: 0x001237BC File Offset: 0x001219BC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					if (num > 1)
					{
						buffer = null;
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
						if (num != 0)
						{
							if (num != 1)
							{
								buffer = ArrayPool<char>.Shared.Rent(4096);
								count = contents.Length;
								index = 0;
								goto IL_0121;
							}
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_019F;
						}
						else
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
						}
						IL_0107:
						configuredTaskAwaiter.GetResult();
						index += batchSize;
						IL_0121:
						if (index >= count)
						{
							cancellationToken.ThrowIfCancellationRequested();
							configuredTaskAwaiter = sw.FlushAsync().ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								num = (num2 = 1);
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, File.<InternalWriteAllTextAsync>d__81>(ref configuredTaskAwaiter, ref this);
								return;
							}
						}
						else
						{
							batchSize = Math.Min(4096, count - index);
							contents.CopyTo(index, buffer, 0, batchSize);
							configuredTaskAwaiter = sw.WriteAsync(new ReadOnlyMemory<char>(buffer, 0, batchSize), cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, File.<InternalWriteAllTextAsync>d__81>(ref configuredTaskAwaiter, ref this);
								return;
							}
							goto IL_0107;
						}
						IL_019F:
						configuredTaskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							sw.Dispose();
							if (buffer != null)
							{
								ArrayPool<char>.Shared.Return(buffer, false);
							}
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					buffer = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				buffer = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06005659 RID: 22105 RVA: 0x00123A08 File Offset: 0x00121C08
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400343F RID: 13375
			public int <>1__state;

			// Token: 0x04003440 RID: 13376
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003441 RID: 13377
			public string contents;

			// Token: 0x04003442 RID: 13378
			public StreamWriter sw;

			// Token: 0x04003443 RID: 13379
			public CancellationToken cancellationToken;

			// Token: 0x04003444 RID: 13380
			private char[] <buffer>5__2;

			// Token: 0x04003445 RID: 13381
			private int <count>5__3;

			// Token: 0x04003446 RID: 13382
			private int <index>5__4;

			// Token: 0x04003447 RID: 13383
			private int <batchSize>5__5;

			// Token: 0x04003448 RID: 13384
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
