using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200072D RID: 1837
	[Obsolete]
	[Guid("0000010b-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIPersistFile
	{
		// Token: 0x06004213 RID: 16915
		void GetClassID(out Guid pClassID);

		// Token: 0x06004214 RID: 16916
		[PreserveSig]
		int IsDirty();

		// Token: 0x06004215 RID: 16917
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x06004216 RID: 16918
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x06004217 RID: 16919
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x06004218 RID: 16920
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
