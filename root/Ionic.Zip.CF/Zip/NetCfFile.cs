using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000022 RID: 34
	internal class NetCfFile
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00008B6C File Offset: 0x00006D6C
		public static int SetTimes(string filename, DateTime ctime, DateTime atime, DateTime mtime)
		{
			IntPtr intPtr = (IntPtr)NetCfFile.CreateFileCE(filename, 1073741824U, 2U, 0, 3U, 0U, 0);
			if ((int)intPtr == -1)
			{
				return Marshal.GetLastWin32Error();
			}
			NetCfFile.SetFileTime(intPtr, BitConverter.GetBytes(ctime.ToFileTime()), BitConverter.GetBytes(atime.ToFileTime()), BitConverter.GetBytes(mtime.ToFileTime()));
			NetCfFile.CloseHandle(intPtr);
			return 0;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public static int SetLastWriteTime(string filename, DateTime mtime)
		{
			IntPtr intPtr = (IntPtr)NetCfFile.CreateFileCE(filename, 1073741824U, 2U, 0, 3U, 0U, 0);
			if ((int)intPtr == -1)
			{
				return Marshal.GetLastWin32Error();
			}
			NetCfFile.SetFileTime(intPtr, null, null, BitConverter.GetBytes(mtime.ToFileTime()));
			NetCfFile.CloseHandle(intPtr);
			return 0;
		}

		// Token: 0x06000148 RID: 328
		[DllImport("coredll.dll", EntryPoint = "CreateFile", SetLastError = true)]
		internal static extern int CreateFileCE(string lpFileName, uint dwDesiredAccess, uint dwShareMode, int lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

		// Token: 0x06000149 RID: 329
		[DllImport("coredll", EntryPoint = "GetFileAttributes", SetLastError = true)]
		internal static extern uint GetAttributes(string lpFileName);

		// Token: 0x0600014A RID: 330
		[DllImport("coredll", EntryPoint = "SetFileAttributes", SetLastError = true)]
		internal static extern bool SetAttributes(string lpFileName, uint dwFileAttributes);

		// Token: 0x0600014B RID: 331
		[DllImport("coredll", SetLastError = true)]
		internal static extern bool SetFileTime(IntPtr hFile, byte[] lpCreationTime, byte[] lpLastAccessTime, byte[] lpLastWriteTime);

		// Token: 0x0600014C RID: 332
		[DllImport("coredll.dll", SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x0600014D RID: 333 RVA: 0x00008C23 File Offset: 0x00006E23
		public NetCfFile()
		{
		}
	}
}
