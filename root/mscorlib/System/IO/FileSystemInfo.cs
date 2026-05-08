using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200095A RID: 2394
	[Serializable]
	public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
	{
		// Token: 0x060056A4 RID: 22180 RVA: 0x00124C52 File Offset: 0x00122E52
		protected FileSystemInfo()
		{
			FileStatus.Initialize(ref this._fileStatus, this is DirectoryInfo);
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x00124C6E File Offset: 0x00122E6E
		internal static FileSystemInfo Create(string fullPath, string fileName, ref FileStatus fileStatus)
		{
			DirectoryInfo directoryInfo = (fileStatus.InitiallyDirectory ? new DirectoryInfo(fullPath, null, fileName, true) : new FileInfo(fullPath, null, fileName, true));
			directoryInfo.Init(ref fileStatus);
			return directoryInfo;
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x00124C93 File Offset: 0x00122E93
		internal void Invalidate()
		{
			this._fileStatus.Invalidate();
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x00124CA0 File Offset: 0x00122EA0
		internal void Init(ref FileStatus fileStatus)
		{
			this._fileStatus = fileStatus;
			this._fileStatus.EnsureStatInitialized(this.FullPath, false);
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060056A8 RID: 22184 RVA: 0x00124CC5 File Offset: 0x00122EC5
		// (set) Token: 0x060056A9 RID: 22185 RVA: 0x00124CE8 File Offset: 0x00122EE8
		public FileAttributes Attributes
		{
			get
			{
				return this._fileStatus.GetAttributes(this.FullPath, this.Name);
			}
			set
			{
				this._fileStatus.SetAttributes(this.FullPath, value);
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060056AA RID: 22186 RVA: 0x00124CFC File Offset: 0x00122EFC
		internal bool ExistsCore
		{
			get
			{
				return this._fileStatus.GetExists(this.FullPath);
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060056AB RID: 22187 RVA: 0x00124D14 File Offset: 0x00122F14
		// (set) Token: 0x060056AC RID: 22188 RVA: 0x00124D2D File Offset: 0x00122F2D
		internal DateTimeOffset CreationTimeCore
		{
			get
			{
				return this._fileStatus.GetCreationTime(this.FullPath, false);
			}
			set
			{
				this._fileStatus.SetCreationTime(this.FullPath, value);
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060056AD RID: 22189 RVA: 0x00124D41 File Offset: 0x00122F41
		// (set) Token: 0x060056AE RID: 22190 RVA: 0x00124D5A File Offset: 0x00122F5A
		internal DateTimeOffset LastAccessTimeCore
		{
			get
			{
				return this._fileStatus.GetLastAccessTime(this.FullPath, false);
			}
			set
			{
				this._fileStatus.SetLastAccessTime(this.FullPath, value);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060056AF RID: 22191 RVA: 0x00124D6E File Offset: 0x00122F6E
		// (set) Token: 0x060056B0 RID: 22192 RVA: 0x00124D87 File Offset: 0x00122F87
		internal DateTimeOffset LastWriteTimeCore
		{
			get
			{
				return this._fileStatus.GetLastWriteTime(this.FullPath, false);
			}
			set
			{
				this._fileStatus.SetLastWriteTime(this.FullPath, value);
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060056B1 RID: 22193 RVA: 0x00124D9B File Offset: 0x00122F9B
		internal long LengthCore
		{
			get
			{
				return this._fileStatus.GetLength(this.FullPath, false);
			}
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00124DB4 File Offset: 0x00122FB4
		public void Refresh()
		{
			this._fileStatus.Refresh(this.FullPath);
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x00124DCC File Offset: 0x00122FCC
		internal static void ThrowNotFound(string path)
		{
			bool flag = !Directory.Exists(Path.GetDirectoryName(PathInternal.TrimEndingDirectorySeparator(path)));
			throw Interop.GetExceptionForIoErrno(new Interop.ErrorInfo(Interop.Error.ENOENT), path, flag);
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060056B4 RID: 22196 RVA: 0x00124DFE File Offset: 0x00122FFE
		internal string NormalizedPath
		{
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x00124E08 File Offset: 0x00123008
		protected FileSystemInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.FullPath = Path.GetFullPathInternal(info.GetString("FullPath"));
			this.OriginalPath = info.GetString("OriginalPath");
			this._name = info.GetString("Name");
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x00124E64 File Offset: 0x00123064
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("OriginalPath", this.OriginalPath, typeof(string));
			info.AddValue("FullPath", this.FullPath, typeof(string));
			info.AddValue("Name", this.Name, typeof(string));
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060056B7 RID: 22199 RVA: 0x00124DFE File Offset: 0x00122FFE
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060056B8 RID: 22200 RVA: 0x00124EC4 File Offset: 0x001230C4
		public string Extension
		{
			get
			{
				int length = this.FullPath.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = this.FullPath[num];
					if (c == '.')
					{
						return this.FullPath.Substring(num, length - num);
					}
					if (PathInternal.IsDirectorySeparator(c) || c == Path.VolumeSeparatorChar)
					{
						break;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060056B9 RID: 22201 RVA: 0x00123D72 File Offset: 0x00121F72
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x00124F20 File Offset: 0x00123120
		public virtual bool Exists
		{
			get
			{
				bool flag;
				try
				{
					flag = this.ExistsCore;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x060056BB RID: 22203
		public abstract void Delete();

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060056BC RID: 22204 RVA: 0x00124F4C File Offset: 0x0012314C
		// (set) Token: 0x060056BD RID: 22205 RVA: 0x00124F67 File Offset: 0x00123167
		public DateTime CreationTime
		{
			get
			{
				return this.CreationTimeUtc.ToLocalTime();
			}
			set
			{
				this.CreationTimeUtc = value.ToUniversalTime();
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060056BE RID: 22206 RVA: 0x00124F78 File Offset: 0x00123178
		// (set) Token: 0x060056BF RID: 22207 RVA: 0x00124F93 File Offset: 0x00123193
		public DateTime CreationTimeUtc
		{
			get
			{
				return this.CreationTimeCore.UtcDateTime;
			}
			set
			{
				this.CreationTimeCore = File.GetUtcDateTimeOffset(value);
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060056C0 RID: 22208 RVA: 0x00124FA4 File Offset: 0x001231A4
		// (set) Token: 0x060056C1 RID: 22209 RVA: 0x00124FBF File Offset: 0x001231BF
		public DateTime LastAccessTime
		{
			get
			{
				return this.LastAccessTimeUtc.ToLocalTime();
			}
			set
			{
				this.LastAccessTimeUtc = value.ToUniversalTime();
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060056C2 RID: 22210 RVA: 0x00124FD0 File Offset: 0x001231D0
		// (set) Token: 0x060056C3 RID: 22211 RVA: 0x00124FEB File Offset: 0x001231EB
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return this.LastAccessTimeCore.UtcDateTime;
			}
			set
			{
				this.LastAccessTimeCore = File.GetUtcDateTimeOffset(value);
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060056C4 RID: 22212 RVA: 0x00124FFC File Offset: 0x001231FC
		// (set) Token: 0x060056C5 RID: 22213 RVA: 0x00125017 File Offset: 0x00123217
		public DateTime LastWriteTime
		{
			get
			{
				return this.LastWriteTimeUtc.ToLocalTime();
			}
			set
			{
				this.LastWriteTimeUtc = value.ToUniversalTime();
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060056C6 RID: 22214 RVA: 0x00125028 File Offset: 0x00123228
		// (set) Token: 0x060056C7 RID: 22215 RVA: 0x00125043 File Offset: 0x00123243
		public DateTime LastWriteTimeUtc
		{
			get
			{
				return this.LastWriteTimeCore.UtcDateTime;
			}
			set
			{
				this.LastWriteTimeCore = File.GetUtcDateTimeOffset(value);
			}
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x00125051 File Offset: 0x00123251
		public override string ToString()
		{
			return this.OriginalPath ?? string.Empty;
		}

		// Token: 0x04003452 RID: 13394
		private FileStatus _fileStatus;

		// Token: 0x04003453 RID: 13395
		protected string FullPath;

		// Token: 0x04003454 RID: 13396
		protected string OriginalPath;

		// Token: 0x04003455 RID: 13397
		internal string _name;
	}
}
