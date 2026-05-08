using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x02000984 RID: 2436
	internal static class MonoIO
	{
		// Token: 0x0600589A RID: 22682 RVA: 0x0012BFC2 File Offset: 0x0012A1C2
		public static Exception GetException(MonoIOError error)
		{
			if (error == MonoIOError.ERROR_ACCESS_DENIED)
			{
				return new UnauthorizedAccessException("Access to the path is denied.");
			}
			if (error != MonoIOError.ERROR_FILE_EXISTS)
			{
				return MonoIO.GetException(string.Empty, error);
			}
			return new IOException("Cannot create a file that already exist.", -2147024816);
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x0012BFF8 File Offset: 0x0012A1F8
		public static Exception GetException(string path, MonoIOError error)
		{
			if (error <= MonoIOError.ERROR_FILE_EXISTS)
			{
				if (error <= MonoIOError.ERROR_NOT_SAME_DEVICE)
				{
					switch (error)
					{
					case MonoIOError.ERROR_FILE_NOT_FOUND:
						return new FileNotFoundException(string.Format("Could not find file \"{0}\"", path), path);
					case MonoIOError.ERROR_PATH_NOT_FOUND:
						return new DirectoryNotFoundException(string.Format("Could not find a part of the path \"{0}\"", path));
					case MonoIOError.ERROR_TOO_MANY_OPEN_FILES:
						if (MonoIO.dump_handles)
						{
							MonoIO.DumpHandles();
						}
						return new IOException("Too many open files", (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_ACCESS_DENIED:
						return new UnauthorizedAccessException(string.Format("Access to the path \"{0}\" is denied.", path));
					case MonoIOError.ERROR_INVALID_HANDLE:
						return new IOException(string.Format("Invalid handle to path \"{0}\"", path), (int)((MonoIOError)(-2147024896) | error));
					default:
						if (error == MonoIOError.ERROR_INVALID_DRIVE)
						{
							return new DriveNotFoundException(string.Format("Could not find the drive  '{0}'. The drive might not be ready or might not be mapped.", path));
						}
						if (error == MonoIOError.ERROR_NOT_SAME_DEVICE)
						{
							return new IOException("Source and destination are not on the same device", (int)((MonoIOError)(-2147024896) | error));
						}
						break;
					}
				}
				else
				{
					switch (error)
					{
					case MonoIOError.ERROR_WRITE_FAULT:
						return new IOException(string.Format("Write fault on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_READ_FAULT:
					case MonoIOError.ERROR_GEN_FAILURE:
						break;
					case MonoIOError.ERROR_SHARING_VIOLATION:
						return new IOException(string.Format("Sharing violation on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					case MonoIOError.ERROR_LOCK_VIOLATION:
						return new IOException(string.Format("Lock violation on path {0}", path), (int)((MonoIOError)(-2147024896) | error));
					default:
						if (error == MonoIOError.ERROR_HANDLE_DISK_FULL)
						{
							return new IOException(string.Format("Disk full. Path {0}", path), (int)((MonoIOError)(-2147024896) | error));
						}
						if (error == MonoIOError.ERROR_FILE_EXISTS)
						{
							return new IOException(string.Format("Could not create file \"{0}\". File already exists.", path), (int)((MonoIOError)(-2147024896) | error));
						}
						break;
					}
				}
			}
			else if (error <= MonoIOError.ERROR_DIR_NOT_EMPTY)
			{
				if (error == MonoIOError.ERROR_CANNOT_MAKE)
				{
					return new IOException(string.Format("Path {0} is a directory", path), (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_INVALID_PARAMETER)
				{
					return new IOException(string.Format("Invalid parameter", Array.Empty<object>()), (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_DIR_NOT_EMPTY)
				{
					return new IOException(string.Format("Directory {0} is not empty", path), (int)((MonoIOError)(-2147024896) | error));
				}
			}
			else
			{
				if (error == MonoIOError.ERROR_FILENAME_EXCED_RANGE)
				{
					return new PathTooLongException(string.Format("Path is too long. Path: {0}", path));
				}
				if (error == MonoIOError.ERROR_DIRECTORY)
				{
					return new IOException("The directory name is invalid", (int)((MonoIOError)(-2147024896) | error));
				}
				if (error == MonoIOError.ERROR_ENCRYPTION_FAILED)
				{
					return new IOException("Encryption failed", (int)((MonoIOError)(-2147024896) | error));
				}
			}
			return new IOException(string.Format("Win32 IO returned {0}. Path: {1}", error, path), (int)((MonoIOError)(-2147024896) | error));
		}

		// Token: 0x0600589C RID: 22684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool CreateDirectory(char* path, out MonoIOError error);

		// Token: 0x0600589D RID: 22685 RVA: 0x0012C264 File Offset: 0x0012A464
		public unsafe static bool CreateDirectory(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.CreateDirectory(ptr, out error);
		}

		// Token: 0x0600589E RID: 22686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool RemoveDirectory(char* path, out MonoIOError error);

		// Token: 0x0600589F RID: 22687 RVA: 0x0012C288 File Offset: 0x0012A488
		public unsafe static bool RemoveDirectory(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.RemoveDirectory(ptr, out error);
		}

		// Token: 0x060058A0 RID: 22688
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetCurrentDirectory(out MonoIOError error);

		// Token: 0x060058A1 RID: 22689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SetCurrentDirectory(char* path, out MonoIOError error);

		// Token: 0x060058A2 RID: 22690 RVA: 0x0012C2AC File Offset: 0x0012A4AC
		public unsafe static bool SetCurrentDirectory(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.SetCurrentDirectory(ptr, out error);
		}

		// Token: 0x060058A3 RID: 22691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool MoveFile(char* path, char* dest, out MonoIOError error);

		// Token: 0x060058A4 RID: 22692 RVA: 0x0012C2D0 File Offset: 0x0012A4D0
		public unsafe static bool MoveFile(string path, string dest, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = dest;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.MoveFile(ptr, ptr2, out error);
		}

		// Token: 0x060058A5 RID: 22693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool CopyFile(char* path, char* dest, bool overwrite, out MonoIOError error);

		// Token: 0x060058A6 RID: 22694 RVA: 0x0012C308 File Offset: 0x0012A508
		public unsafe static bool CopyFile(string path, string dest, bool overwrite, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = dest;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.CopyFile(ptr, ptr2, overwrite, out error);
		}

		// Token: 0x060058A7 RID: 22695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool DeleteFile(char* path, out MonoIOError error);

		// Token: 0x060058A8 RID: 22696 RVA: 0x0012C340 File Offset: 0x0012A540
		public unsafe static bool DeleteFile(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.DeleteFile(ptr, out error);
		}

		// Token: 0x060058A9 RID: 22697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool ReplaceFile(char* sourceFileName, char* destinationFileName, char* destinationBackupFileName, bool ignoreMetadataErrors, out MonoIOError error);

		// Token: 0x060058AA RID: 22698 RVA: 0x0012C364 File Offset: 0x0012A564
		public unsafe static bool ReplaceFile(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors, out MonoIOError error)
		{
			char* ptr = sourceFileName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = destinationFileName;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr3 = destinationBackupFileName;
			if (ptr3 != null)
			{
				ptr3 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.ReplaceFile(ptr, ptr2, ptr3, ignoreMetadataErrors, out error);
		}

		// Token: 0x060058AB RID: 22699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern FileAttributes GetFileAttributes(char* path, out MonoIOError error);

		// Token: 0x060058AC RID: 22700 RVA: 0x0012C3B0 File Offset: 0x0012A5B0
		public unsafe static FileAttributes GetFileAttributes(string path, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.GetFileAttributes(ptr, out error);
		}

		// Token: 0x060058AD RID: 22701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SetFileAttributes(char* path, FileAttributes attrs, out MonoIOError error);

		// Token: 0x060058AE RID: 22702 RVA: 0x0012C3D4 File Offset: 0x0012A5D4
		public unsafe static bool SetFileAttributes(string path, FileAttributes attrs, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.SetFileAttributes(ptr, attrs, out error);
		}

		// Token: 0x060058AF RID: 22703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MonoFileType GetFileType(IntPtr handle, out MonoIOError error);

		// Token: 0x060058B0 RID: 22704 RVA: 0x0012C3FC File Offset: 0x0012A5FC
		public static MonoFileType GetFileType(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			MonoFileType fileType;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				fileType = MonoIO.GetFileType(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return fileType;
		}

		// Token: 0x060058B1 RID: 22705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr FindFirstFile(char* pathWithPattern, out string fileName, out int fileAttr, out int error);

		// Token: 0x060058B2 RID: 22706 RVA: 0x0012C440 File Offset: 0x0012A640
		public unsafe static IntPtr FindFirstFile(string pathWithPattern, out string fileName, out int fileAttr, out int error)
		{
			char* ptr = pathWithPattern;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.FindFirstFile(ptr, out fileName, out fileAttr, out error);
		}

		// Token: 0x060058B3 RID: 22707
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FindNextFile(IntPtr hnd, out string fileName, out int fileAttr, out int error);

		// Token: 0x060058B4 RID: 22708
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FindCloseFile(IntPtr hnd);

		// Token: 0x060058B5 RID: 22709 RVA: 0x0012C466 File Offset: 0x0012A666
		public static bool Exists(string path, out MonoIOError error)
		{
			return MonoIO.GetFileAttributes(path, out error) != (FileAttributes)(-1);
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x0012C478 File Offset: 0x0012A678
		public static bool ExistsFile(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			return fileAttributes != (FileAttributes)(-1) && (fileAttributes & FileAttributes.Directory) == (FileAttributes)0;
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x0012C49C File Offset: 0x0012A69C
		public static bool ExistsDirectory(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			if (error == MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				error = MonoIOError.ERROR_PATH_NOT_FOUND;
			}
			return fileAttributes != (FileAttributes)(-1) && (fileAttributes & FileAttributes.Directory) != (FileAttributes)0;
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x0012C4C8 File Offset: 0x0012A6C8
		public static bool ExistsSymlink(string path, out MonoIOError error)
		{
			FileAttributes fileAttributes = MonoIO.GetFileAttributes(path, out error);
			return fileAttributes != (FileAttributes)(-1) && (fileAttributes & FileAttributes.ReparsePoint) != (FileAttributes)0;
		}

		// Token: 0x060058B9 RID: 22713
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool GetFileStat(char* path, out MonoIOStat stat, out MonoIOError error);

		// Token: 0x060058BA RID: 22714 RVA: 0x0012C4F0 File Offset: 0x0012A6F0
		public unsafe static bool GetFileStat(string path, out MonoIOStat stat, out MonoIOError error)
		{
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.GetFileStat(ptr, out stat, out error);
		}

		// Token: 0x060058BB RID: 22715
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Open(char* filename, FileMode mode, FileAccess access, FileShare share, FileOptions options, out MonoIOError error);

		// Token: 0x060058BC RID: 22716 RVA: 0x0012C518 File Offset: 0x0012A718
		public unsafe static IntPtr Open(string filename, FileMode mode, FileAccess access, FileShare share, FileOptions options, out MonoIOError error)
		{
			char* ptr = filename;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return MonoIO.Open(ptr, mode, access, share, options, out error);
		}

		// Token: 0x060058BD RID: 22717
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Cancel_internal(IntPtr handle, out MonoIOError error);

		// Token: 0x060058BE RID: 22718 RVA: 0x0012C544 File Offset: 0x0012A744
		internal static bool Cancel(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.Cancel_internal(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060058BF RID: 22719
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Close(IntPtr handle, out MonoIOError error);

		// Token: 0x060058C0 RID: 22720
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Read(IntPtr handle, byte[] dest, int dest_offset, int count, out MonoIOError error);

		// Token: 0x060058C1 RID: 22721 RVA: 0x0012C588 File Offset: 0x0012A788
		public static int Read(SafeHandle safeHandle, byte[] dest, int dest_offset, int count, out MonoIOError error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Read(safeHandle.DangerousGetHandle(), dest, dest_offset, count, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060058C2 RID: 22722
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Write(IntPtr handle, [In] byte[] src, int src_offset, int count, out MonoIOError error);

		// Token: 0x060058C3 RID: 22723 RVA: 0x0012C5D0 File Offset: 0x0012A7D0
		public static int Write(SafeHandle safeHandle, byte[] src, int src_offset, int count, out MonoIOError error)
		{
			bool flag = false;
			int num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Write(safeHandle.DangerousGetHandle(), src, src_offset, count, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060058C4 RID: 22724
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long Seek(IntPtr handle, long offset, SeekOrigin origin, out MonoIOError error);

		// Token: 0x060058C5 RID: 22725 RVA: 0x0012C618 File Offset: 0x0012A818
		public static long Seek(SafeHandle safeHandle, long offset, SeekOrigin origin, out MonoIOError error)
		{
			bool flag = false;
			long num;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				num = MonoIO.Seek(safeHandle.DangerousGetHandle(), offset, origin, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x060058C6 RID: 22726
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Flush(IntPtr handle, out MonoIOError error);

		// Token: 0x060058C7 RID: 22727 RVA: 0x0012C65C File Offset: 0x0012A85C
		public static bool Flush(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.Flush(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060058C8 RID: 22728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLength(IntPtr handle, out MonoIOError error);

		// Token: 0x060058C9 RID: 22729 RVA: 0x0012C6A0 File Offset: 0x0012A8A0
		public static long GetLength(SafeHandle safeHandle, out MonoIOError error)
		{
			bool flag = false;
			long length;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				length = MonoIO.GetLength(safeHandle.DangerousGetHandle(), out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return length;
		}

		// Token: 0x060058CA RID: 22730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetLength(IntPtr handle, long length, out MonoIOError error);

		// Token: 0x060058CB RID: 22731 RVA: 0x0012C6E4 File Offset: 0x0012A8E4
		public static bool SetLength(SafeHandle safeHandle, long length, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.SetLength(safeHandle.DangerousGetHandle(), length, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060058CC RID: 22732
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetFileTime(IntPtr handle, long creation_time, long last_access_time, long last_write_time, out MonoIOError error);

		// Token: 0x060058CD RID: 22733 RVA: 0x0012C728 File Offset: 0x0012A928
		public static bool SetFileTime(SafeHandle safeHandle, long creation_time, long last_access_time, long last_write_time, out MonoIOError error)
		{
			bool flag = false;
			bool flag2;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				flag2 = MonoIO.SetFileTime(safeHandle.DangerousGetHandle(), creation_time, last_access_time, last_write_time, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x0012C770 File Offset: 0x0012A970
		public static bool SetFileTime(string path, long creation_time, long last_access_time, long last_write_time, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 0, creation_time, last_access_time, last_write_time, DateTime.MinValue, out error);
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0012C783 File Offset: 0x0012A983
		public static bool SetCreationTime(string path, DateTime dateTime, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 1, -1L, -1L, -1L, dateTime, out error);
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x0012C794 File Offset: 0x0012A994
		public static bool SetLastAccessTime(string path, DateTime dateTime, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 2, -1L, -1L, -1L, dateTime, out error);
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0012C7A5 File Offset: 0x0012A9A5
		public static bool SetLastWriteTime(string path, DateTime dateTime, out MonoIOError error)
		{
			return MonoIO.SetFileTime(path, 3, -1L, -1L, -1L, dateTime, out error);
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0012C7B8 File Offset: 0x0012A9B8
		public static bool SetFileTime(string path, int type, long creation_time, long last_access_time, long last_write_time, DateTime dateTime, out MonoIOError error)
		{
			IntPtr intPtr = MonoIO.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, FileOptions.None, out error);
			if (intPtr == MonoIO.InvalidHandle)
			{
				return false;
			}
			switch (type)
			{
			case 1:
				creation_time = dateTime.ToFileTime();
				break;
			case 2:
				last_access_time = dateTime.ToFileTime();
				break;
			case 3:
				last_write_time = dateTime.ToFileTime();
				break;
			}
			bool flag = MonoIO.SetFileTime(new SafeFileHandle(intPtr, false), creation_time, last_access_time, last_write_time, out error);
			MonoIOError monoIOError;
			MonoIO.Close(intPtr, out monoIOError);
			return flag;
		}

		// Token: 0x060058D3 RID: 22739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lock(IntPtr handle, long position, long length, out MonoIOError error);

		// Token: 0x060058D4 RID: 22740 RVA: 0x0012C834 File Offset: 0x0012AA34
		public static void Lock(SafeHandle safeHandle, long position, long length, out MonoIOError error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				MonoIO.Lock(safeHandle.DangerousGetHandle(), position, length, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x060058D5 RID: 22741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unlock(IntPtr handle, long position, long length, out MonoIOError error);

		// Token: 0x060058D6 RID: 22742 RVA: 0x0012C878 File Offset: 0x0012AA78
		public static void Unlock(SafeHandle safeHandle, long position, long length, out MonoIOError error)
		{
			bool flag = false;
			try
			{
				safeHandle.DangerousAddRef(ref flag);
				MonoIO.Unlock(safeHandle.DangerousGetHandle(), position, length, out error);
			}
			finally
			{
				if (flag)
				{
					safeHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x060058D7 RID: 22743
		public static extern IntPtr ConsoleOutput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x060058D8 RID: 22744
		public static extern IntPtr ConsoleInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x060058D9 RID: 22745
		public static extern IntPtr ConsoleError
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060058DA RID: 22746
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CreatePipe(out IntPtr read_handle, out IntPtr write_handle, out MonoIOError error);

		// Token: 0x060058DB RID: 22747
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DuplicateHandle(IntPtr source_process_handle, IntPtr source_handle, IntPtr target_process_handle, out IntPtr target_handle, int access, int inherit, int options, out MonoIOError error);

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060058DC RID: 22748
		public static extern char VolumeSeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060058DD RID: 22749
		public static extern char DirectorySeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060058DE RID: 22750
		public static extern char AltDirectorySeparatorChar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x060058DF RID: 22751
		public static extern char PathSeparator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060058E0 RID: 22752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DumpHandles();

		// Token: 0x060058E1 RID: 22753 RVA: 0x0012C8BC File Offset: 0x0012AABC
		// Note: this type is marked as 'beforefieldinit'.
		static MonoIO()
		{
		}

		// Token: 0x04003537 RID: 13623
		public const int FileAlreadyExistsHResult = -2147024816;

		// Token: 0x04003538 RID: 13624
		public const FileAttributes InvalidFileAttributes = (FileAttributes)(-1);

		// Token: 0x04003539 RID: 13625
		public static readonly IntPtr InvalidHandle = (IntPtr)(-1L);

		// Token: 0x0400353A RID: 13626
		private static bool dump_handles = Environment.GetEnvironmentVariable("MONO_DUMP_HANDLES_ON_ERROR_TOO_MANY_OPEN_FILES") != null;
	}
}
