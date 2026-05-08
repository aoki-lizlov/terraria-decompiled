using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.IO.Enumeration
{
	// Token: 0x0200099A RID: 2458
	public ref struct FileSystemEntry
	{
		// Token: 0x060059C8 RID: 22984 RVA: 0x00130908 File Offset: 0x0012EB08
		internal unsafe static FileAttributes Initialize(ref FileSystemEntry entry, Interop.Sys.DirectoryEntry directoryEntry, ReadOnlySpan<char> directory, ReadOnlySpan<char> rootDirectory, ReadOnlySpan<char> originalRootDirectory, Span<char> pathBuffer)
		{
			entry._directoryEntry = directoryEntry;
			entry.Directory = directory;
			entry.RootDirectory = rootDirectory;
			entry.OriginalRootDirectory = originalRootDirectory;
			entry._pathBuffer = pathBuffer;
			entry._fullPath = ReadOnlySpan<char>.Empty;
			entry._fileName = ReadOnlySpan<char>.Empty;
			bool flag = false;
			bool flag2 = false;
			Interop.Sys.FileStatus fileStatus;
			if (directoryEntry.InodeType == Interop.Sys.NodeType.DT_DIR)
			{
				flag = true;
			}
			else if ((directoryEntry.InodeType == Interop.Sys.NodeType.DT_LNK || directoryEntry.InodeType == Interop.Sys.NodeType.DT_UNKNOWN) && (Interop.Sys.Stat(entry.FullPath, out fileStatus) >= 0 || Interop.Sys.LStat(entry.FullPath, out fileStatus) >= 0))
			{
				flag = (fileStatus.Mode & 61440) == 16384;
			}
			Interop.Sys.FileStatus fileStatus2;
			if (directoryEntry.InodeType == Interop.Sys.NodeType.DT_LNK)
			{
				flag2 = true;
			}
			else if (directoryEntry.InodeType == Interop.Sys.NodeType.DT_UNKNOWN && Interop.Sys.LStat(entry.FullPath, out fileStatus2) >= 0)
			{
				flag2 = (fileStatus2.Mode & 61440) == 40960;
			}
			entry._status = default(FileStatus);
			FileStatus.Initialize(ref entry._status, flag);
			FileAttributes fileAttributes = (FileAttributes)0;
			if (flag2)
			{
				fileAttributes |= FileAttributes.ReparsePoint;
			}
			if (flag)
			{
				fileAttributes |= FileAttributes.Directory;
			}
			if (*directoryEntry.Name == 46)
			{
				fileAttributes |= FileAttributes.Hidden;
			}
			if (fileAttributes == (FileAttributes)0)
			{
				fileAttributes = FileAttributes.Normal;
			}
			entry._initialAttributes = fileAttributes;
			return fileAttributes;
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x060059C9 RID: 22985 RVA: 0x00130A30 File Offset: 0x0012EC30
		private ReadOnlySpan<char> FullPath
		{
			get
			{
				if (this._fullPath.Length == 0)
				{
					int num;
					Path.TryJoin(this.Directory, this.FileName, this._pathBuffer, out num);
					this._fullPath = this._pathBuffer.Slice(0, num);
				}
				return this._fullPath;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x00130A84 File Offset: 0x0012EC84
		public unsafe ReadOnlySpan<char> FileName
		{
			get
			{
				if (this._directoryEntry.NameLength != 0 && this._fileName.Length == 0)
				{
					fixed (char* ptr = &this._fileNameBuffer.FixedElementField)
					{
						char* ptr2 = ptr;
						Span<char> span = new Span<char>((void*)ptr2, 256);
						this._fileName = this._directoryEntry.GetName(span);
					}
				}
				return this._fileName;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x060059CB RID: 22987 RVA: 0x00130AE2 File Offset: 0x0012ECE2
		// (set) Token: 0x060059CC RID: 22988 RVA: 0x00130AEA File Offset: 0x0012ECEA
		public ReadOnlySpan<char> Directory
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Directory>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Directory>k__BackingField = value;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x060059CD RID: 22989 RVA: 0x00130AF3 File Offset: 0x0012ECF3
		// (set) Token: 0x060059CE RID: 22990 RVA: 0x00130AFB File Offset: 0x0012ECFB
		public ReadOnlySpan<char> RootDirectory
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<RootDirectory>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RootDirectory>k__BackingField = value;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x00130B04 File Offset: 0x0012ED04
		// (set) Token: 0x060059D0 RID: 22992 RVA: 0x00130B0C File Offset: 0x0012ED0C
		public ReadOnlySpan<char> OriginalRootDirectory
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<OriginalRootDirectory>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OriginalRootDirectory>k__BackingField = value;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x00130B15 File Offset: 0x0012ED15
		public FileAttributes Attributes
		{
			get
			{
				return this._initialAttributes | (this._status.IsReadOnly(this.FullPath, true) ? FileAttributes.ReadOnly : ((FileAttributes)0));
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x060059D2 RID: 22994 RVA: 0x00130B36 File Offset: 0x0012ED36
		public long Length
		{
			get
			{
				return this._status.GetLength(this.FullPath, true);
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x060059D3 RID: 22995 RVA: 0x00130B4A File Offset: 0x0012ED4A
		public DateTimeOffset CreationTimeUtc
		{
			get
			{
				return this._status.GetCreationTime(this.FullPath, true);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x00130B5E File Offset: 0x0012ED5E
		public DateTimeOffset LastAccessTimeUtc
		{
			get
			{
				return this._status.GetLastAccessTime(this.FullPath, true);
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x00130B72 File Offset: 0x0012ED72
		public DateTimeOffset LastWriteTimeUtc
		{
			get
			{
				return this._status.GetLastWriteTime(this.FullPath, true);
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x00130B86 File Offset: 0x0012ED86
		public bool IsDirectory
		{
			get
			{
				return this._status.InitiallyDirectory;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x00130B93 File Offset: 0x0012ED93
		public unsafe bool IsHidden
		{
			get
			{
				return *this._directoryEntry.Name == 46;
			}
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x00130BA5 File Offset: 0x0012EDA5
		public FileSystemInfo ToFileSystemInfo()
		{
			return FileSystemInfo.Create(this.ToFullPath(), new string(this.FileName), ref this._status);
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x00130BC3 File Offset: 0x0012EDC3
		public string ToFullPath()
		{
			return new string(this.FullPath);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x00130BD0 File Offset: 0x0012EDD0
		public string ToSpecifiedFullPath()
		{
			ReadOnlySpan<char> readOnlySpan = this.Directory.Slice(this.RootDirectory.Length);
			if (PathInternal.EndsInDirectorySeparator(this.OriginalRootDirectory) && PathInternal.StartsWithDirectorySeparator(readOnlySpan))
			{
				readOnlySpan = readOnlySpan.Slice(1);
			}
			return Path.Join(this.OriginalRootDirectory, readOnlySpan, this.FileName);
		}

		// Token: 0x0400358B RID: 13707
		internal Interop.Sys.DirectoryEntry _directoryEntry;

		// Token: 0x0400358C RID: 13708
		private FileStatus _status;

		// Token: 0x0400358D RID: 13709
		private Span<char> _pathBuffer;

		// Token: 0x0400358E RID: 13710
		private ReadOnlySpan<char> _fullPath;

		// Token: 0x0400358F RID: 13711
		private ReadOnlySpan<char> _fileName;

		// Token: 0x04003590 RID: 13712
		[FixedBuffer(typeof(char), 256)]
		private FileSystemEntry.<_fileNameBuffer>e__FixedBuffer _fileNameBuffer;

		// Token: 0x04003591 RID: 13713
		private FileAttributes _initialAttributes;

		// Token: 0x04003592 RID: 13714
		[CompilerGenerated]
		private ReadOnlySpan<char> <Directory>k__BackingField;

		// Token: 0x04003593 RID: 13715
		[CompilerGenerated]
		private ReadOnlySpan<char> <RootDirectory>k__BackingField;

		// Token: 0x04003594 RID: 13716
		[CompilerGenerated]
		private ReadOnlySpan<char> <OriginalRootDirectory>k__BackingField;

		// Token: 0x0200099B RID: 2459
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <_fileNameBuffer>e__FixedBuffer
		{
			// Token: 0x04003595 RID: 13717
			public char FixedElementField;
		}
	}
}
