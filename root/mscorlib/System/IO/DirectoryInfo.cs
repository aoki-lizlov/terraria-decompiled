using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.IO
{
	// Token: 0x0200094D RID: 2381
	[Serializable]
	public sealed class DirectoryInfo : FileSystemInfo
	{
		// Token: 0x060055B9 RID: 21945 RVA: 0x0012144D File Offset: 0x0011F64D
		public DirectoryInfo(string path)
		{
			this.Init(path, Path.GetFullPath(path), null, true);
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x00121464 File Offset: 0x0011F664
		internal DirectoryInfo(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			this.Init(originalPath, fullPath, fileName, isNormalized);
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x00121478 File Offset: 0x0011F678
		private void Init(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			if (originalPath == null)
			{
				throw new ArgumentNullException("path");
			}
			this.OriginalPath = originalPath;
			fullPath = fullPath ?? originalPath;
			fullPath = (isNormalized ? fullPath : Path.GetFullPath(fullPath));
			this._name = fileName ?? (PathInternal.IsRoot(fullPath) ? fullPath : Path.GetFileName(PathInternal.TrimEndingDirectorySeparator(fullPath.AsSpan()))).ToString();
			this.FullPath = fullPath;
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x060055BC RID: 21948 RVA: 0x001214F8 File Offset: 0x0011F6F8
		public DirectoryInfo Parent
		{
			get
			{
				string directoryName = Path.GetDirectoryName(PathInternal.IsRoot(this.FullPath) ? this.FullPath : PathInternal.TrimEndingDirectorySeparator(this.FullPath));
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName, null, null, false);
			}
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x00121540 File Offset: 0x0011F740
		public DirectoryInfo CreateSubdirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.IsEffectivelyEmpty(path))
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			if (Path.IsPathRooted(path))
			{
				throw new ArgumentException("Second path fragment must not be a drive or UNC name.", "path");
			}
			string fullPath = Path.GetFullPath(Path.Combine(this.FullPath, path));
			ReadOnlySpan<char> readOnlySpan = PathInternal.TrimEndingDirectorySeparator(fullPath.AsSpan());
			ReadOnlySpan<char> readOnlySpan2 = PathInternal.TrimEndingDirectorySeparator(this.FullPath.AsSpan());
			if (readOnlySpan.StartsWith(readOnlySpan2, PathInternal.StringComparison) && (readOnlySpan.Length == readOnlySpan2.Length || PathInternal.IsDirectorySeparator(fullPath[readOnlySpan2.Length])))
			{
				FileSystem.CreateDirectory(fullPath);
				return new DirectoryInfo(fullPath);
			}
			throw new ArgumentException(SR.Format("The directory specified, '{0}', is not a subdirectory of '{1}'.", path, this.FullPath), "path");
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x00121614 File Offset: 0x0011F814
		public void Create()
		{
			FileSystem.CreateDirectory(this.FullPath);
			base.Invalidate();
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x00121627 File Offset: 0x0011F827
		public FileInfo[] GetFiles()
		{
			return this.GetFiles("*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00121639 File Offset: 0x0011F839
		public FileInfo[] GetFiles(string searchPattern)
		{
			return this.GetFiles(searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00121647 File Offset: 0x0011F847
		public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
		{
			return this.GetFiles(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x00121656 File Offset: 0x0011F856
		public FileInfo[] GetFiles(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return ((IEnumerable<FileInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Files, enumerationOptions)).ToArray<FileInfo>();
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x00121670 File Offset: 0x0011F870
		public FileSystemInfo[] GetFileSystemInfos()
		{
			return this.GetFileSystemInfos("*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00121682 File Offset: 0x0011F882
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
		{
			return this.GetFileSystemInfos(searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x00121690 File Offset: 0x0011F890
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			return this.GetFileSystemInfos(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x0012169F File Offset: 0x0011F89F
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Both, enumerationOptions).ToArray<FileSystemInfo>();
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x001216B4 File Offset: 0x0011F8B4
		public DirectoryInfo[] GetDirectories()
		{
			return this.GetDirectories("*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x001216C6 File Offset: 0x0011F8C6
		public DirectoryInfo[] GetDirectories(string searchPattern)
		{
			return this.GetDirectories(searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x001216D4 File Offset: 0x0011F8D4
		public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
		{
			return this.GetDirectories(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x001216E3 File Offset: 0x0011F8E3
		public DirectoryInfo[] GetDirectories(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return ((IEnumerable<DirectoryInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Directories, enumerationOptions)).ToArray<DirectoryInfo>();
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x001216FD File Offset: 0x0011F8FD
		public IEnumerable<DirectoryInfo> EnumerateDirectories()
		{
			return this.EnumerateDirectories("*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0012170F File Offset: 0x0011F90F
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
		{
			return this.EnumerateDirectories(searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x0012171D File Offset: 0x0011F91D
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
		{
			return this.EnumerateDirectories(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x0012172C File Offset: 0x0011F92C
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return (IEnumerable<DirectoryInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Directories, enumerationOptions);
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x00121741 File Offset: 0x0011F941
		public IEnumerable<FileInfo> EnumerateFiles()
		{
			return this.EnumerateFiles("*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x00121753 File Offset: 0x0011F953
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
		{
			return this.EnumerateFiles(searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x00121761 File Offset: 0x0011F961
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
		{
			return this.EnumerateFiles(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x00121770 File Offset: 0x0011F970
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return (IEnumerable<FileInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Files, enumerationOptions);
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x00121785 File Offset: 0x0011F985
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
		{
			return this.EnumerateFileSystemInfos("*", EnumerationOptions.Compatible);
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x00121797 File Offset: 0x0011F997
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
		{
			return this.EnumerateFileSystemInfos(searchPattern, EnumerationOptions.Compatible);
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x001217A5 File Offset: 0x0011F9A5
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			return this.EnumerateFileSystemInfos(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x001217B4 File Offset: 0x0011F9B4
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Both, enumerationOptions);
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x001217C4 File Offset: 0x0011F9C4
		internal static IEnumerable<FileSystemInfo> InternalEnumerateInfos(string path, string searchPattern, SearchTarget searchTarget, EnumerationOptions options)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			FileSystemEnumerableFactory.NormalizeInputs(ref path, ref searchPattern, options);
			switch (searchTarget)
			{
			case SearchTarget.Files:
				return FileSystemEnumerableFactory.FileInfos(path, searchPattern, options);
			case SearchTarget.Directories:
				return FileSystemEnumerableFactory.DirectoryInfos(path, searchPattern, options);
			case SearchTarget.Both:
				return FileSystemEnumerableFactory.FileSystemInfos(path, searchPattern, options);
			default:
				throw new ArgumentException("Enum value was out of legal range.", "searchTarget");
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x060055D8 RID: 21976 RVA: 0x00121829 File Offset: 0x0011FA29
		public DirectoryInfo Root
		{
			get
			{
				return new DirectoryInfo(Path.GetPathRoot(this.FullPath));
			}
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x0012183C File Offset: 0x0011FA3C
		public void MoveTo(string destDirName)
		{
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destDirName");
			}
			string fullPath = Path.GetFullPath(destDirName);
			string text = PathInternal.EnsureTrailingSeparator(fullPath);
			string text2 = PathInternal.EnsureTrailingSeparator(this.FullPath);
			if (string.Equals(text2, text, PathInternal.StringComparison))
			{
				throw new IOException("Source and destination path must be different.");
			}
			string pathRoot = Path.GetPathRoot(text2);
			string pathRoot2 = Path.GetPathRoot(text);
			if (!string.Equals(pathRoot, pathRoot2, PathInternal.StringComparison))
			{
				throw new IOException("Source and destination path must have identical roots. Move will not work across volumes.");
			}
			if (!this.Exists && !FileSystem.FileExists(this.FullPath))
			{
				throw new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", this.FullPath));
			}
			if (FileSystem.DirectoryExists(fullPath))
			{
				throw new IOException(SR.Format("Cannot create '{0}' because a file or directory with the same name already exists.", text));
			}
			FileSystem.MoveDirectory(this.FullPath, fullPath);
			this.Init(destDirName, text, null, true);
			base.Invalidate();
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0012192F File Offset: 0x0011FB2F
		public override void Delete()
		{
			FileSystem.RemoveDirectory(this.FullPath, false);
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x0012193D File Offset: 0x0011FB3D
		public void Delete(bool recursive)
		{
			FileSystem.RemoveDirectory(this.FullPath, recursive);
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x0012194B File Offset: 0x0011FB4B
		private DirectoryInfo(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x00121955 File Offset: 0x0011FB55
		public void Create(DirectorySecurity directorySecurity)
		{
			FileSystem.CreateDirectory(this.FullPath);
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x00121962 File Offset: 0x0011FB62
		public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
		{
			return this.CreateSubdirectory(path);
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x0012196B File Offset: 0x0011FB6B
		public DirectorySecurity GetAccessControl()
		{
			return Directory.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x0012197A File Offset: 0x0011FB7A
		public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
		{
			return Directory.GetAccessControl(this.FullPath, includeSections);
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x00121988 File Offset: 0x0011FB88
		public void SetAccessControl(DirectorySecurity directorySecurity)
		{
			Directory.SetAccessControl(this.FullPath, directorySecurity);
		}
	}
}
