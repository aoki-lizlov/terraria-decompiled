using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x02000959 RID: 2393
	internal static class FileSystem
	{
		// Token: 0x0600568C RID: 22156 RVA: 0x00124420 File Offset: 0x00122620
		private static bool CopyDanglingSymlink(string sourceFullPath, string destFullPath)
		{
			Interop.Sys.FileStatus fileStatus;
			if (Interop.Sys.Stat(sourceFullPath, out fileStatus) >= 0 || Interop.Sys.LStat(sourceFullPath, out fileStatus) != 0)
			{
				return false;
			}
			string text = Interop.Sys.ReadLink(sourceFullPath);
			if (text == null)
			{
				throw Interop.GetExceptionForIoErrno(Interop.Sys.GetLastErrorInfo(), sourceFullPath, false);
			}
			if (Interop.Sys.Symlink(text, destFullPath) == 0)
			{
				return true;
			}
			throw Interop.GetExceptionForIoErrno(Interop.Sys.GetLastErrorInfo(), destFullPath, false);
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x00124470 File Offset: 0x00122670
		public static void CopyFile(string sourceFullPath, string destFullPath, bool overwrite)
		{
			if (FileSystem.DirectoryExists(destFullPath))
			{
				destFullPath = Path.Combine(destFullPath, Path.GetFileName(sourceFullPath));
			}
			if (FileSystem.CopyDanglingSymlink(sourceFullPath, destFullPath))
			{
				return;
			}
			using (FileStream fileStream = new FileStream(sourceFullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None))
			{
				using (FileStream fileStream2 = new FileStream(destFullPath, overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.None))
				{
					Interop.CheckIo(Interop.Sys.CopyFile(fileStream.SafeFileHandle, fileStream2.SafeFileHandle), null, false, null);
				}
			}
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x00124514 File Offset: 0x00122714
		private static void LinkOrCopyFile(string sourceFullPath, string destFullPath)
		{
			if (FileSystem.CopyDanglingSymlink(sourceFullPath, destFullPath))
			{
				return;
			}
			if (Interop.Sys.Link(sourceFullPath, destFullPath) >= 0)
			{
				return;
			}
			Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
			if (lastErrorInfo.Error == Interop.Error.EXDEV || lastErrorInfo.Error == Interop.Error.EACCES || lastErrorInfo.Error == Interop.Error.EPERM || lastErrorInfo.Error == Interop.Error.ENOTSUP || lastErrorInfo.Error == Interop.Error.EMLINK || lastErrorInfo.Error == Interop.Error.ENOSYS)
			{
				FileSystem.CopyFile(sourceFullPath, destFullPath, false);
				return;
			}
			string text = null;
			bool flag = false;
			if (lastErrorInfo.Error == Interop.Error.ENOENT)
			{
				if (!Directory.Exists(Path.GetDirectoryName(destFullPath)))
				{
					text = destFullPath;
					flag = true;
				}
				else
				{
					text = sourceFullPath;
				}
			}
			else if (lastErrorInfo.Error == Interop.Error.EEXIST)
			{
				text = destFullPath;
			}
			throw Interop.GetExceptionForIoErrno(lastErrorInfo, text, flag);
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x001245DC File Offset: 0x001227DC
		public static void ReplaceFile(string sourceFullPath, string destFullPath, string destBackupFullPath, bool ignoreMetadataErrors)
		{
			Interop.Sys.FileStatus fileStatus;
			if (destBackupFullPath != null)
			{
				if (Interop.Sys.Unlink(destBackupFullPath) != 0)
				{
					Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
					if (lastErrorInfo.Error != Interop.Error.ENOENT)
					{
						throw Interop.GetExceptionForIoErrno(lastErrorInfo, destBackupFullPath, false);
					}
				}
				FileSystem.LinkOrCopyFile(destFullPath, destBackupFullPath);
			}
			else if (Interop.Sys.Stat(destFullPath, out fileStatus) != 0)
			{
				Interop.ErrorInfo lastErrorInfo2 = Interop.Sys.GetLastErrorInfo();
				if (lastErrorInfo2.Error == Interop.Error.ENOENT)
				{
					throw Interop.GetExceptionForIoErrno(lastErrorInfo2, destBackupFullPath, false);
				}
			}
			Interop.CheckIo(Interop.Sys.Rename(sourceFullPath, destFullPath), null, false, null);
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x00124654 File Offset: 0x00122854
		public static void MoveFile(string sourceFullPath, string destFullPath)
		{
			Interop.Sys.FileStatus fileStatus;
			Interop.Sys.FileStatus fileStatus2;
			if (Interop.Sys.LStat(sourceFullPath, out fileStatus) == 0 && (Interop.Sys.LStat(destFullPath, out fileStatus2) != 0 || (fileStatus.Dev == fileStatus2.Dev && fileStatus.Ino == fileStatus2.Ino)) && Interop.Sys.Rename(sourceFullPath, destFullPath) == 0)
			{
				return;
			}
			FileSystem.LinkOrCopyFile(sourceFullPath, destFullPath);
			FileSystem.DeleteFile(sourceFullPath);
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x001246A8 File Offset: 0x001228A8
		public static void DeleteFile(string fullPath)
		{
			if (Interop.Sys.Unlink(fullPath) < 0)
			{
				Interop.ErrorInfo errorInfo = Interop.Sys.GetLastErrorInfo();
				Interop.Error error = errorInfo.Error;
				if (error != Interop.Error.EISDIR)
				{
					if (error == Interop.Error.ENOENT)
					{
						return;
					}
					if (error == Interop.Error.EROFS)
					{
						Interop.ErrorInfo errorInfo2;
						if (!FileSystem.FileExists(PathInternal.TrimEndingDirectorySeparator(fullPath), 32768, out errorInfo2) && errorInfo2.Error == Interop.Error.ENOENT)
						{
							return;
						}
					}
				}
				else
				{
					errorInfo = Interop.Error.EACCES.Info();
				}
				throw Interop.GetExceptionForIoErrno(errorInfo, fullPath, false);
			}
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x00124724 File Offset: 0x00122924
		public static void CreateDirectory(string fullPath)
		{
			int num = fullPath.Length;
			if (num >= 2 && PathInternal.EndsInDirectorySeparator(fullPath))
			{
				num--;
			}
			if (num == 2 && PathInternal.IsDirectorySeparator(fullPath[1]))
			{
				throw new IOException(SR.Format("The specified directory '{0}' cannot be created.", fullPath));
			}
			if (FileSystem.DirectoryExists(fullPath))
			{
				return;
			}
			bool flag = false;
			Stack<string> stack = new Stack<string>();
			int rootLength = PathInternal.GetRootLength(fullPath);
			if (num > rootLength)
			{
				int num2 = num - 1;
				while (num2 >= rootLength && !flag)
				{
					string text = fullPath.Substring(0, num2 + 1);
					if (!FileSystem.DirectoryExists(text))
					{
						stack.Push(text);
					}
					else
					{
						flag = true;
					}
					while (num2 > rootLength && !PathInternal.IsDirectorySeparator(fullPath[num2]))
					{
						num2--;
					}
					num2--;
				}
			}
			if (stack.Count == 0 && !flag)
			{
				if (!FileSystem.DirectoryExists(Directory.InternalGetDirectoryRoot(fullPath)))
				{
					throw Interop.GetExceptionForIoErrno(Interop.Error.ENOENT.Info(), fullPath, true);
				}
				return;
			}
			else
			{
				int num3 = 0;
				Interop.ErrorInfo errorInfo = default(Interop.ErrorInfo);
				string text2 = fullPath;
				while (stack.Count > 0)
				{
					string text3 = stack.Pop();
					num3 = Interop.Sys.MkDir(text3, 511);
					if (num3 < 0 && errorInfo.Error == Interop.Error.SUCCESS)
					{
						Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
						if (lastErrorInfo.Error != Interop.Error.EEXIST)
						{
							errorInfo = lastErrorInfo;
						}
						else if (FileSystem.FileExists(text3) || (!FileSystem.DirectoryExists(text3, out lastErrorInfo) && lastErrorInfo.Error == Interop.Error.EACCES))
						{
							errorInfo = lastErrorInfo;
							text2 = text3;
						}
					}
				}
				if (num3 < 0 && errorInfo.Error != Interop.Error.SUCCESS)
				{
					throw Interop.GetExceptionForIoErrno(errorInfo, text2, true);
				}
				return;
			}
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x001248C4 File Offset: 0x00122AC4
		public static void MoveDirectory(string sourceFullPath, string destFullPath)
		{
			if (FileSystem.FileExists(sourceFullPath))
			{
				if (PathInternal.EndsInDirectorySeparator(sourceFullPath))
				{
					throw new IOException(SR.Format("Could not find a part of the path '{0}'.", sourceFullPath));
				}
				destFullPath = PathInternal.TrimEndingDirectorySeparator(destFullPath);
				if (FileSystem.FileExists(destFullPath))
				{
					throw new IOException("Cannot create a file when that file already exists.");
				}
			}
			if (Interop.Sys.Rename(sourceFullPath, destFullPath) >= 0)
			{
				return;
			}
			Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
			if (lastErrorInfo.Error == Interop.Error.EACCES)
			{
				throw new IOException(SR.Format("Access to the path '{0}' is denied.", sourceFullPath), lastErrorInfo.RawErrno);
			}
			throw Interop.GetExceptionForIoErrno(lastErrorInfo, sourceFullPath, true);
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x0012495B File Offset: 0x00122B5B
		public static void RemoveDirectory(string fullPath, bool recursive)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
			if (!directoryInfo.Exists)
			{
				throw Interop.GetExceptionForIoErrno(Interop.Error.ENOENT.Info(), fullPath, true);
			}
			FileSystem.RemoveDirectoryInternal(directoryInfo, recursive, true);
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x00124984 File Offset: 0x00122B84
		private static void RemoveDirectoryInternal(DirectoryInfo directory, bool recursive, bool throwOnTopLevelDirectoryNotFound)
		{
			Exception ex = null;
			if ((directory.Attributes & FileAttributes.ReparsePoint) != (FileAttributes)0)
			{
				FileSystem.DeleteFile(directory.FullName);
				return;
			}
			if (recursive)
			{
				try
				{
					foreach (string text in Directory.EnumerateFileSystemEntries(directory.FullName))
					{
						if (!FileSystem.ShouldIgnoreDirectory(Path.GetFileName(text)))
						{
							try
							{
								DirectoryInfo directoryInfo = new DirectoryInfo(text);
								if (directoryInfo.Exists)
								{
									FileSystem.RemoveDirectoryInternal(directoryInfo, recursive, false);
								}
								else
								{
									FileSystem.DeleteFile(text);
								}
							}
							catch (Exception ex2)
							{
								if (ex != null)
								{
									ex = ex2;
								}
							}
						}
					}
				}
				catch (Exception ex3)
				{
					if (ex != null)
					{
						ex = ex3;
					}
				}
				if (ex != null)
				{
					throw ex;
				}
			}
			if (Interop.Sys.RmDir(directory.FullName) < 0)
			{
				Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
				Interop.Error error = lastErrorInfo.Error;
				if (error <= Interop.Error.EISDIR)
				{
					if (error != Interop.Error.EACCES && error != Interop.Error.EISDIR)
					{
						goto IL_0106;
					}
				}
				else if (error != Interop.Error.ENOENT)
				{
					if (error != Interop.Error.EPERM && error != Interop.Error.EROFS)
					{
						goto IL_0106;
					}
				}
				else
				{
					if (!throwOnTopLevelDirectoryNotFound)
					{
						return;
					}
					goto IL_0106;
				}
				throw new IOException(SR.Format("Access to the path '{0}' is denied.", directory.FullName));
				IL_0106:
				throw Interop.GetExceptionForIoErrno(lastErrorInfo, directory.FullName, true);
			}
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x00124AD0 File Offset: 0x00122CD0
		public static bool DirectoryExists(ReadOnlySpan<char> fullPath)
		{
			Interop.ErrorInfo errorInfo;
			return FileSystem.DirectoryExists(fullPath, out errorInfo);
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x00124AE5 File Offset: 0x00122CE5
		private static bool DirectoryExists(ReadOnlySpan<char> fullPath, out Interop.ErrorInfo errorInfo)
		{
			return FileSystem.FileExists(fullPath, 16384, out errorInfo);
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x00124AF4 File Offset: 0x00122CF4
		public static bool FileExists(ReadOnlySpan<char> fullPath)
		{
			Interop.ErrorInfo errorInfo;
			return FileSystem.FileExists(PathInternal.TrimEndingDirectorySeparator(fullPath), 32768, out errorInfo);
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x00124B14 File Offset: 0x00122D14
		private static bool FileExists(ReadOnlySpan<char> fullPath, int fileType, out Interop.ErrorInfo errorInfo)
		{
			errorInfo = default(Interop.ErrorInfo);
			Interop.Sys.FileStatus fileStatus;
			if (Interop.Sys.Stat(fullPath, out fileStatus) < 0 && Interop.Sys.LStat(fullPath, out fileStatus) < 0)
			{
				errorInfo = Interop.Sys.GetLastErrorInfo();
				return false;
			}
			return fileType == 16384 == ((fileStatus.Mode & 61440) == 16384);
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x00124B68 File Offset: 0x00122D68
		private static bool ShouldIgnoreDirectory(string name)
		{
			return name == "." || name == "..";
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x00124B84 File Offset: 0x00122D84
		public static FileAttributes GetAttributes(string fullPath)
		{
			FileAttributes attributes = new FileInfo(fullPath, null, null, false).Attributes;
			if (attributes == (FileAttributes)(-1))
			{
				FileSystemInfo.ThrowNotFound(fullPath);
			}
			return attributes;
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x00124B9E File Offset: 0x00122D9E
		public static void SetAttributes(string fullPath, FileAttributes attributes)
		{
			new FileInfo(fullPath, null, null, false).Attributes = attributes;
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x00124BAF File Offset: 0x00122DAF
		public static DateTimeOffset GetCreationTime(string fullPath)
		{
			return new FileInfo(fullPath, null, null, false).CreationTime;
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x00124BC4 File Offset: 0x00122DC4
		public static void SetCreationTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			(asDirectory ? new DirectoryInfo(fullPath, null, null, false) : new FileInfo(fullPath, null, null, false)).CreationTimeCore = time;
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x00124BE3 File Offset: 0x00122DE3
		public static DateTimeOffset GetLastAccessTime(string fullPath)
		{
			return new FileInfo(fullPath, null, null, false).LastAccessTime;
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x00124BF8 File Offset: 0x00122DF8
		public static void SetLastAccessTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			(asDirectory ? new DirectoryInfo(fullPath, null, null, false) : new FileInfo(fullPath, null, null, false)).LastAccessTimeCore = time;
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x00124C17 File Offset: 0x00122E17
		public static DateTimeOffset GetLastWriteTime(string fullPath)
		{
			return new FileInfo(fullPath, null, null, false).LastWriteTime;
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x00124C2C File Offset: 0x00122E2C
		public static void SetLastWriteTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			(asDirectory ? new DirectoryInfo(fullPath, null, null, false) : new FileInfo(fullPath, null, null, false)).LastWriteTimeCore = time;
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00124C4B File Offset: 0x00122E4B
		public static string[] GetLogicalDrives()
		{
			return DriveInfoInternal.GetLogicalDrives();
		}

		// Token: 0x04003451 RID: 13393
		internal const int DefaultBufferSize = 4096;
	}
}
