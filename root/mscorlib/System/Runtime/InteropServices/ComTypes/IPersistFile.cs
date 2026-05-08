using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200077B RID: 1915
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersistFile
	{
		// Token: 0x060044CA RID: 17610
		void GetClassID(out Guid pClassID);

		// Token: 0x060044CB RID: 17611
		[PreserveSig]
		int IsDirty();

		// Token: 0x060044CC RID: 17612
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x060044CD RID: 17613
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x060044CE RID: 17614
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x060044CF RID: 17615
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
