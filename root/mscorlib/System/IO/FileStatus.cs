using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x02000958 RID: 2392
	internal struct FileStatus
	{
		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06005679 RID: 22137 RVA: 0x00123D7A File Offset: 0x00121F7A
		// (set) Token: 0x0600567A RID: 22138 RVA: 0x00123D82 File Offset: 0x00121F82
		internal bool InitiallyDirectory
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<InitiallyDirectory>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<InitiallyDirectory>k__BackingField = value;
			}
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x00123D8B File Offset: 0x00121F8B
		internal static void Initialize(ref FileStatus status, bool isDirectory)
		{
			status.InitiallyDirectory = isDirectory;
			status._fileStatusInitialized = -1;
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x00123D9B File Offset: 0x00121F9B
		internal void Invalidate()
		{
			this._fileStatusInitialized = -1;
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x00123DA4 File Offset: 0x00121FA4
		internal bool IsReadOnly(ReadOnlySpan<char> path, bool continueOnError = false)
		{
			this.EnsureStatInitialized(path, continueOnError);
			Interop.Sys.Permissions permissions;
			Interop.Sys.Permissions permissions2;
			if (this._fileStatus.Uid == Interop.Sys.GetEUid())
			{
				permissions = Interop.Sys.Permissions.S_IRUSR;
				permissions2 = Interop.Sys.Permissions.S_IWUSR;
			}
			else if (this._fileStatus.Gid == Interop.Sys.GetEGid())
			{
				permissions = Interop.Sys.Permissions.S_IRGRP;
				permissions2 = Interop.Sys.Permissions.S_IWGRP;
			}
			else
			{
				permissions = Interop.Sys.Permissions.S_IROTH;
				permissions2 = Interop.Sys.Permissions.S_IWOTH;
			}
			return (this._fileStatus.Mode & (int)permissions) != 0 && (this._fileStatus.Mode & (int)permissions2) == 0;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x00123E18 File Offset: 0x00122018
		public unsafe FileAttributes GetAttributes(ReadOnlySpan<char> path, ReadOnlySpan<char> fileName)
		{
			this.EnsureStatInitialized(path, false);
			if (!this._exists)
			{
				return (FileAttributes)(-1);
			}
			FileAttributes fileAttributes = (FileAttributes)0;
			if (this.IsReadOnly(path, false))
			{
				fileAttributes |= FileAttributes.ReadOnly;
			}
			if ((this._fileStatus.Mode & 61440) == 40960)
			{
				fileAttributes |= FileAttributes.ReparsePoint;
			}
			if (this._isDirectory)
			{
				fileAttributes |= FileAttributes.Directory;
			}
			if (fileName.Length > 0 && (*fileName[0] == 46 || (this._fileStatus.UserFlags & 32768U) == 32768U))
			{
				fileAttributes |= FileAttributes.Hidden;
			}
			if (fileAttributes == (FileAttributes)0)
			{
				return FileAttributes.Normal;
			}
			return fileAttributes;
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x00123EB4 File Offset: 0x001220B4
		public void SetAttributes(string path, FileAttributes attributes)
		{
			if ((attributes & ~(FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System | FileAttributes.Directory | FileAttributes.Archive | FileAttributes.Device | FileAttributes.Normal | FileAttributes.Temporary | FileAttributes.SparseFile | FileAttributes.ReparsePoint | FileAttributes.Compressed | FileAttributes.Offline | FileAttributes.NotContentIndexed | FileAttributes.Encrypted | FileAttributes.IntegrityStream | FileAttributes.NoScrubData)) != (FileAttributes)0)
			{
				throw new ArgumentException("Invalid File or Directory attributes value.", "Attributes");
			}
			this.EnsureStatInitialized(path, false);
			if (!this._exists)
			{
				FileSystemInfo.ThrowNotFound(path);
			}
			if (Interop.Sys.CanSetHiddenFlag)
			{
				if ((attributes & FileAttributes.Hidden) != (FileAttributes)0)
				{
					if ((this._fileStatus.UserFlags & 32768U) == 0U)
					{
						Interop.CheckIo(Interop.Sys.LChflags(path, this._fileStatus.UserFlags | 32768U), path, this.InitiallyDirectory, null);
					}
				}
				else if ((this._fileStatus.UserFlags & 32768U) == 32768U)
				{
					Interop.CheckIo(Interop.Sys.LChflags(path, this._fileStatus.UserFlags & 4294934527U), path, this.InitiallyDirectory, null);
				}
			}
			int num = this._fileStatus.Mode;
			if ((attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
			{
				num &= -147;
			}
			else if ((num & 256) != 0)
			{
				num |= 128;
			}
			if (num != this._fileStatus.Mode)
			{
				Interop.CheckIo(Interop.Sys.ChMod(path, num), path, this.InitiallyDirectory, null);
			}
			this._fileStatusInitialized = -1;
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x00123FCE File Offset: 0x001221CE
		internal bool GetExists(ReadOnlySpan<char> path)
		{
			if (this._fileStatusInitialized == -1)
			{
				this.Refresh(path);
			}
			return this._exists && this.InitiallyDirectory == this._isDirectory;
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x00123FF8 File Offset: 0x001221F8
		internal DateTimeOffset GetCreationTime(ReadOnlySpan<char> path, bool continueOnError = false)
		{
			this.EnsureStatInitialized(path, continueOnError);
			if (!this._exists)
			{
				return DateTimeOffset.FromFileTime(0L);
			}
			if ((this._fileStatus.Flags & Interop.Sys.FileStatusFlags.HasBirthTime) != Interop.Sys.FileStatusFlags.None)
			{
				return this.UnixTimeToDateTimeOffset(this._fileStatus.BirthTime, this._fileStatus.BirthTimeNsec);
			}
			if (this._fileStatus.MTime < this._fileStatus.CTime || (this._fileStatus.MTime == this._fileStatus.CTime && this._fileStatus.MTimeNsec < this._fileStatus.CTimeNsec))
			{
				return this.UnixTimeToDateTimeOffset(this._fileStatus.MTime, this._fileStatus.MTimeNsec);
			}
			return this.UnixTimeToDateTimeOffset(this._fileStatus.CTime, this._fileStatus.CTimeNsec);
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x001240CA File Offset: 0x001222CA
		internal void SetCreationTime(string path, DateTimeOffset time)
		{
			this.SetLastAccessTime(path, time);
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x001240D4 File Offset: 0x001222D4
		internal DateTimeOffset GetLastAccessTime(ReadOnlySpan<char> path, bool continueOnError = false)
		{
			this.EnsureStatInitialized(path, continueOnError);
			if (!this._exists)
			{
				return DateTimeOffset.FromFileTime(0L);
			}
			return this.UnixTimeToDateTimeOffset(this._fileStatus.ATime, this._fileStatus.ATimeNsec);
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x0012410C File Offset: 0x0012230C
		internal void SetLastAccessTime(string path, DateTimeOffset time)
		{
			this.SetAccessWriteTimes(path, new long?(time.ToUnixTimeSeconds()), new long?((time.Ticks - 621355968000000000L) % 10000000L / 10L), null, null);
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x0012415F File Offset: 0x0012235F
		internal DateTimeOffset GetLastWriteTime(ReadOnlySpan<char> path, bool continueOnError = false)
		{
			this.EnsureStatInitialized(path, continueOnError);
			if (!this._exists)
			{
				return DateTimeOffset.FromFileTime(0L);
			}
			return this.UnixTimeToDateTimeOffset(this._fileStatus.MTime, this._fileStatus.MTimeNsec);
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x00124198 File Offset: 0x00122398
		internal void SetLastWriteTime(string path, DateTimeOffset time)
		{
			this.SetAccessWriteTimes(path, null, null, new long?(time.ToUnixTimeSeconds()), new long?((time.Ticks - 621355968000000000L) % 10000000L / 10L));
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x001241EC File Offset: 0x001223EC
		private DateTimeOffset UnixTimeToDateTimeOffset(long seconds, long nanoseconds)
		{
			return DateTimeOffset.FromUnixTimeSeconds(seconds).AddTicks(nanoseconds / 100L).ToLocalTime();
		}

		// Token: 0x06005688 RID: 22152 RVA: 0x00124214 File Offset: 0x00122414
		private void SetAccessWriteTimes(string path, long? accessSec, long? accessUSec, long? writeSec, long? writeUSec)
		{
			this._fileStatusInitialized = -1;
			this.EnsureStatInitialized(path, false);
			Interop.Sys.TimeValPair timeValPair;
			timeValPair.ASec = accessSec ?? this._fileStatus.ATime;
			timeValPair.AUSec = accessUSec ?? (this._fileStatus.ATimeNsec / 1000L);
			timeValPair.MSec = writeSec ?? this._fileStatus.MTime;
			timeValPair.MUSec = writeUSec ?? (this._fileStatus.MTimeNsec / 1000L);
			Interop.CheckIo(Interop.Sys.UTimes(path, ref timeValPair), path, this.InitiallyDirectory, null);
			this._fileStatusInitialized = -1;
		}

		// Token: 0x06005689 RID: 22153 RVA: 0x001242FA File Offset: 0x001224FA
		internal long GetLength(ReadOnlySpan<char> path, bool continueOnError = false)
		{
			this.EnsureStatInitialized(path, continueOnError);
			return this._fileStatus.Size;
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x00124310 File Offset: 0x00122510
		public void Refresh(ReadOnlySpan<char> path)
		{
			this._isDirectory = false;
			path = PathInternal.TrimEndingDirectorySeparator(path);
			if (Interop.Sys.LStat(path, out this._fileStatus) >= 0)
			{
				this._exists = true;
				this._isDirectory = (this._fileStatus.Mode & 61440) == 16384;
				Interop.Sys.FileStatus fileStatus;
				if ((this._fileStatus.Mode & 61440) == 40960 && Interop.Sys.Stat(path, out fileStatus) >= 0)
				{
					this._isDirectory = (fileStatus.Mode & 61440) == 16384;
				}
				this._fileStatusInitialized = 0;
				return;
			}
			Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
			if (lastErrorInfo.Error == Interop.Error.ENOENT || lastErrorInfo.Error == Interop.Error.ENOTDIR)
			{
				this._fileStatusInitialized = 0;
				this._exists = false;
				return;
			}
			this._fileStatusInitialized = lastErrorInfo.RawErrno;
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x001243E2 File Offset: 0x001225E2
		internal void EnsureStatInitialized(ReadOnlySpan<char> path, bool continueOnError = false)
		{
			if (this._fileStatusInitialized == -1)
			{
				this.Refresh(path);
			}
			if (this._fileStatusInitialized != 0 && !continueOnError)
			{
				int fileStatusInitialized = this._fileStatusInitialized;
				this._fileStatusInitialized = -1;
				throw Interop.GetExceptionForIoErrno(new Interop.ErrorInfo(fileStatusInitialized), new string(path), false);
			}
		}

		// Token: 0x04003449 RID: 13385
		private const int NanosecondsPerTick = 100;

		// Token: 0x0400344A RID: 13386
		private const int TicksPerMicrosecond = 10;

		// Token: 0x0400344B RID: 13387
		private const long TicksPerSecond = 10000000L;

		// Token: 0x0400344C RID: 13388
		private Interop.Sys.FileStatus _fileStatus;

		// Token: 0x0400344D RID: 13389
		private int _fileStatusInitialized;

		// Token: 0x0400344E RID: 13390
		[CompilerGenerated]
		private bool <InitiallyDirectory>k__BackingField;

		// Token: 0x0400344F RID: 13391
		internal bool _isDirectory;

		// Token: 0x04003450 RID: 13392
		private bool _exists;
	}
}
