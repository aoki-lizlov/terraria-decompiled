using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Security.AccessControl;

namespace System.IO
{
	// Token: 0x0200094C RID: 2380
	public static class Directory
	{
		// Token: 0x06005583 RID: 21891 RVA: 0x00120E60 File Offset: 0x0011F060
		public static DirectoryInfo GetParent(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(path));
			if (directoryName == null)
			{
				return null;
			}
			return new DirectoryInfo(directoryName);
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x00120EAA File Offset: 0x0011F0AA
		public static DirectoryInfo CreateDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			string fullPath = Path.GetFullPath(path);
			FileSystem.CreateDirectory(fullPath);
			return new DirectoryInfo(fullPath, null, null, false);
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x00120EE8 File Offset: 0x0011F0E8
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
				return FileSystem.DirectoryExists(Path.GetFullPath(path));
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

		// Token: 0x06005586 RID: 21894 RVA: 0x00120F50 File Offset: 0x0011F150
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), creationTime, true);
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00120F64 File Offset: 0x0011F164
		public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(creationTimeUtc), true);
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x00120F78 File Offset: 0x0011F178
		public static DateTime GetCreationTime(string path)
		{
			return File.GetCreationTime(path);
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x00120F80 File Offset: 0x0011F180
		public static DateTime GetCreationTimeUtc(string path)
		{
			return File.GetCreationTimeUtc(path);
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x00120F88 File Offset: 0x0011F188
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), lastWriteTime, true);
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x00120F9C File Offset: 0x0011F19C
		public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastWriteTimeUtc), true);
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x00120FB0 File Offset: 0x0011F1B0
		public static DateTime GetLastWriteTime(string path)
		{
			return File.GetLastWriteTime(path);
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00120FB8 File Offset: 0x0011F1B8
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return File.GetLastWriteTimeUtc(path);
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00120FC0 File Offset: 0x0011F1C0
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), lastAccessTime, true);
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x00120FD4 File Offset: 0x0011F1D4
		public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastAccessTimeUtc), true);
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x00120FE8 File Offset: 0x0011F1E8
		public static DateTime GetLastAccessTime(string path)
		{
			return File.GetLastAccessTime(path);
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x00120FF0 File Offset: 0x0011F1F0
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return File.GetLastAccessTimeUtc(path);
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x00120FF8 File Offset: 0x0011F1F8
		public static string[] GetFiles(string path)
		{
			return Directory.GetFiles(path, "*", EnumerationOptions.Compatible);
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0012100A File Offset: 0x0011F20A
		public static string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x00121018 File Offset: 0x0011F218
		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFiles(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x00121027 File Offset: 0x0011F227
		public static string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Files, enumerationOptions).ToArray<string>();
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x00121037 File Offset: 0x0011F237
		public static string[] GetDirectories(string path)
		{
			return Directory.GetDirectories(path, "*", EnumerationOptions.Compatible);
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x00121049 File Offset: 0x0011F249
		public static string[] GetDirectories(string path, string searchPattern)
		{
			return Directory.GetDirectories(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x00121057 File Offset: 0x0011F257
		public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetDirectories(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x00121066 File Offset: 0x0011F266
		public static string[] GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Directories, enumerationOptions).ToArray<string>();
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x00121076 File Offset: 0x0011F276
		public static string[] GetFileSystemEntries(string path)
		{
			return Directory.GetFileSystemEntries(path, "*", EnumerationOptions.Compatible);
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x00121088 File Offset: 0x0011F288
		public static string[] GetFileSystemEntries(string path, string searchPattern)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x00121096 File Offset: 0x0011F296
		public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x001210A5 File Offset: 0x0011F2A5
		public static string[] GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Both, enumerationOptions).ToArray<string>();
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x001210B8 File Offset: 0x0011F2B8
		internal static IEnumerable<string> InternalEnumeratePaths(string path, string searchPattern, SearchTarget searchTarget, EnumerationOptions options)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			FileSystemEnumerableFactory.NormalizeInputs(ref path, ref searchPattern, options);
			switch (searchTarget)
			{
			case SearchTarget.Files:
				return FileSystemEnumerableFactory.UserFiles(path, searchPattern, options);
			case SearchTarget.Directories:
				return FileSystemEnumerableFactory.UserDirectories(path, searchPattern, options);
			case SearchTarget.Both:
				return FileSystemEnumerableFactory.UserEntries(path, searchPattern, options);
			default:
				throw new ArgumentOutOfRangeException("searchTarget");
			}
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x00121126 File Offset: 0x0011F326
		public static IEnumerable<string> EnumerateDirectories(string path)
		{
			return Directory.EnumerateDirectories(path, "*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x00121138 File Offset: 0x0011F338
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
		{
			return Directory.EnumerateDirectories(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x00121146 File Offset: 0x0011F346
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateDirectories(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x00121155 File Offset: 0x0011F355
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Directories, enumerationOptions);
		}

		// Token: 0x060055A3 RID: 21923 RVA: 0x00121160 File Offset: 0x0011F360
		public static IEnumerable<string> EnumerateFiles(string path)
		{
			return Directory.EnumerateFiles(path, "*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x00121172 File Offset: 0x0011F372
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
		{
			return Directory.EnumerateFiles(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x00121180 File Offset: 0x0011F380
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFiles(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x0012118F File Offset: 0x0011F38F
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Files, enumerationOptions);
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x0012119A File Offset: 0x0011F39A
		public static IEnumerable<string> EnumerateFileSystemEntries(string path)
		{
			return Directory.EnumerateFileSystemEntries(path, "*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x001211AC File Offset: 0x0011F3AC
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
		{
			return Directory.EnumerateFileSystemEntries(path, searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x001211BA File Offset: 0x0011F3BA
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemEntries(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x001211C9 File Offset: 0x0011F3C9
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Both, enumerationOptions);
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x001211D4 File Offset: 0x0011F3D4
		public static string GetDirectoryRoot(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPath = Path.GetFullPath(path);
			return fullPath.Substring(0, PathInternal.GetRootLength(fullPath));
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x00121208 File Offset: 0x0011F408
		internal static string InternalGetDirectoryRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Substring(0, PathInternal.GetRootLength(path));
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x00121221 File Offset: 0x0011F421
		public static string GetCurrentDirectory()
		{
			return Environment.CurrentDirectory;
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x00121228 File Offset: 0x0011F428
		public static void SetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			Environment.CurrentDirectory = Path.GetFullPath(path);
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x0012125C File Offset: 0x0011F45C
		public static void Move(string sourceDirName, string destDirName)
		{
			if (sourceDirName == null)
			{
				throw new ArgumentNullException("sourceDirName");
			}
			if (sourceDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "sourceDirName");
			}
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destDirName");
			}
			string fullPath = Path.GetFullPath(sourceDirName);
			string text = PathInternal.EnsureTrailingSeparator(fullPath);
			string fullPath2 = Path.GetFullPath(destDirName);
			string text2 = PathInternal.EnsureTrailingSeparator(fullPath2);
			StringComparison stringComparison = PathInternal.StringComparison;
			if (string.Equals(text, text2, stringComparison))
			{
				throw new IOException("Source and destination path must be different.");
			}
			string pathRoot = Path.GetPathRoot(text);
			string pathRoot2 = Path.GetPathRoot(text2);
			if (!string.Equals(pathRoot, pathRoot2, stringComparison))
			{
				throw new IOException("Source and destination path must have identical roots. Move will not work across volumes.");
			}
			if (!FileSystem.DirectoryExists(fullPath) && !FileSystem.FileExists(fullPath))
			{
				throw new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", fullPath));
			}
			if (FileSystem.DirectoryExists(fullPath2))
			{
				throw new IOException(SR.Format("Cannot create '{0}' because a file or directory with the same name already exists.", fullPath2));
			}
			FileSystem.MoveDirectory(fullPath, fullPath2);
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x0012135D File Offset: 0x0011F55D
		public static void Delete(string path)
		{
			FileSystem.RemoveDirectory(Path.GetFullPath(path), false);
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x0012136B File Offset: 0x0011F56B
		public static void Delete(string path, bool recursive)
		{
			FileSystem.RemoveDirectory(Path.GetFullPath(path), recursive);
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x00121379 File Offset: 0x0011F579
		public static string[] GetLogicalDrives()
		{
			return FileSystem.GetLogicalDrives();
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x00121380 File Offset: 0x0011F580
		public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
		{
			return Directory.CreateDirectory(path);
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x00121388 File Offset: 0x0011F588
		public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new DirectorySecurity(path, includeSections);
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x00121391 File Offset: 0x0011F591
		public static DirectorySecurity GetAccessControl(string path)
		{
			return Directory.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x0012139C File Offset: 0x0011F59C
		public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
		{
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			string fullPath = Path.GetFullPath(path);
			directorySecurity.PersistModifications(fullPath);
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x001213C8 File Offset: 0x0011F5C8
		internal static string InsecureGetCurrentDirectory()
		{
			MonoIOError monoIOError;
			string currentDirectory = MonoIO.GetCurrentDirectory(out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			return currentDirectory;
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x001213E8 File Offset: 0x0011F5E8
		internal static void InsecureSetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("path string must not be an empty string or whitespace string");
			}
			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException("Directory \"" + path + "\" not found.");
			}
			MonoIOError monoIOError;
			MonoIO.SetCurrentDirectory(path, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}
	}
}
