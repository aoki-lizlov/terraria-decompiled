using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200097C RID: 2428
	[ComVisible(true)]
	[Serializable]
	public sealed class DriveInfo : ISerializable
	{
		// Token: 0x0600582B RID: 22571 RVA: 0x0012A7A7 File Offset: 0x001289A7
		private DriveInfo(string path, string fstype)
		{
			this.drive_format = fstype;
			this.path = path;
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x0012A7C0 File Offset: 0x001289C0
		public DriveInfo(string driveName)
		{
			if (!Environment.IsUnix)
			{
				if (driveName == null || driveName.Length == 0)
				{
					throw new ArgumentException("The drive name is null or empty", "driveName");
				}
				if (driveName.Length >= 2 && driveName[1] != ':')
				{
					throw new ArgumentException("Invalid drive name", "driveName");
				}
				driveName = char.ToUpperInvariant(driveName[0]).ToString() + ":\\";
			}
			DriveInfo[] drives = DriveInfo.GetDrives();
			Array.Sort<DriveInfo>(drives, (DriveInfo di1, DriveInfo di2) => string.Compare(di2.path, di1.path, true));
			foreach (DriveInfo driveInfo in drives)
			{
				if (driveName.StartsWith(driveInfo.path, StringComparison.OrdinalIgnoreCase))
				{
					this.path = driveInfo.path;
					this.drive_format = driveInfo.drive_format;
					return;
				}
			}
			throw new ArgumentException("The drive name does not exist", "driveName");
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x0012A8B0 File Offset: 0x00128AB0
		private static void GetDiskFreeSpace(string path, out ulong availableFreeSpace, out ulong totalSize, out ulong totalFreeSpace)
		{
			MonoIOError monoIOError;
			if (!DriveInfo.GetDiskFreeSpaceInternal(path, out availableFreeSpace, out totalSize, out totalFreeSpace, out monoIOError))
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x0012A8D4 File Offset: 0x00128AD4
		public long AvailableFreeSpace
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				if (num <= 9223372036854775807UL)
				{
					return (long)num;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600582F RID: 22575 RVA: 0x0012A90C File Offset: 0x00128B0C
		public long TotalFreeSpace
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				if (num3 <= 9223372036854775807UL)
				{
					return (long)num3;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06005830 RID: 22576 RVA: 0x0012A944 File Offset: 0x00128B44
		public long TotalSize
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				if (num2 <= 9223372036854775807UL)
				{
					return (long)num2;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06005831 RID: 22577 RVA: 0x0012A979 File Offset: 0x00128B79
		// (set) Token: 0x06005832 RID: 22578 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Currently get only works on Mono/Unix; set not implemented")]
		public string VolumeLabel
		{
			get
			{
				return this.path;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06005833 RID: 22579 RVA: 0x0012A981 File Offset: 0x00128B81
		public string DriveFormat
		{
			get
			{
				return this.drive_format;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06005834 RID: 22580 RVA: 0x0012A989 File Offset: 0x00128B89
		public DriveType DriveType
		{
			get
			{
				return (DriveType)DriveInfo.GetDriveTypeInternal(this.path);
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06005835 RID: 22581 RVA: 0x0012A979 File Offset: 0x00128B79
		public string Name
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06005836 RID: 22582 RVA: 0x0012A996 File Offset: 0x00128B96
		public DirectoryInfo RootDirectory
		{
			get
			{
				return new DirectoryInfo(this.path);
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06005837 RID: 22583 RVA: 0x0012A9A3 File Offset: 0x00128BA3
		public bool IsReady
		{
			get
			{
				return Directory.Exists(this.Name);
			}
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x0012A9B0 File Offset: 0x00128BB0
		[MonoTODO("In windows, alldrives are 'Fixed'")]
		public static DriveInfo[] GetDrives()
		{
			string[] logicalDrives = Environment.GetLogicalDrives();
			DriveInfo[] array = new DriveInfo[logicalDrives.Length];
			int num = 0;
			foreach (string text in logicalDrives)
			{
				array[num++] = new DriveInfo(text, DriveInfo.GetDriveFormat(text));
			}
			return array;
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x000174FB File Offset: 0x000156FB
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x0012A9F7 File Offset: 0x00128BF7
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600583B RID: 22587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool GetDiskFreeSpaceInternal(char* pathName, int pathName_length, out ulong freeBytesAvail, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes, out MonoIOError error);

		// Token: 0x0600583C RID: 22588 RVA: 0x0012AA00 File Offset: 0x00128C00
		private unsafe static bool GetDiskFreeSpaceInternal(string pathName, out ulong freeBytesAvail, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes, out MonoIOError error)
		{
			char* ptr = pathName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return DriveInfo.GetDiskFreeSpaceInternal(ptr, (pathName != null) ? pathName.Length : 0, out freeBytesAvail, out totalNumberOfBytes, out totalNumberOfFreeBytes, out error);
		}

		// Token: 0x0600583D RID: 22589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint GetDriveTypeInternal(char* rootPathName, int rootPathName_length);

		// Token: 0x0600583E RID: 22590 RVA: 0x0012AA34 File Offset: 0x00128C34
		private unsafe static uint GetDriveTypeInternal(string rootPathName)
		{
			char* ptr = rootPathName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return DriveInfo.GetDriveTypeInternal(ptr, (rootPathName != null) ? rootPathName.Length : 0);
		}

		// Token: 0x0600583F RID: 22591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern string GetDriveFormatInternal(char* rootPathName, int rootPathName_length);

		// Token: 0x06005840 RID: 22592 RVA: 0x0012AA64 File Offset: 0x00128C64
		private unsafe static string GetDriveFormat(string rootPathName)
		{
			char* ptr = rootPathName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return DriveInfo.GetDriveFormatInternal(ptr, (rootPathName != null) ? rootPathName.Length : 0);
		}

		// Token: 0x0400350D RID: 13581
		private string drive_format;

		// Token: 0x0400350E RID: 13582
		private string path;

		// Token: 0x0200097D RID: 2429
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06005841 RID: 22593 RVA: 0x0012AA93 File Offset: 0x00128C93
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06005842 RID: 22594 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06005843 RID: 22595 RVA: 0x0012AA9F File Offset: 0x00128C9F
			internal int <.ctor>b__3_0(DriveInfo di1, DriveInfo di2)
			{
				return string.Compare(di2.path, di1.path, true);
			}

			// Token: 0x0400350F RID: 13583
			public static readonly DriveInfo.<>c <>9 = new DriveInfo.<>c();

			// Token: 0x04003510 RID: 13584
			public static Comparison<DriveInfo> <>9__3_0;
		}
	}
}
