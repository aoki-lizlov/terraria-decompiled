using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000725 RID: 1829
	[Obsolete]
	[Guid("0000000e-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIBindCtx
	{
		// Token: 0x060041DE RID: 16862
		void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x060041DF RID: 16863
		void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x060041E0 RID: 16864
		void ReleaseBoundObjects();

		// Token: 0x060041E1 RID: 16865
		void SetBindOptions([In] ref BIND_OPTS pbindopts);

		// Token: 0x060041E2 RID: 16866
		void GetBindOptions(ref BIND_OPTS pbindopts);

		// Token: 0x060041E3 RID: 16867
		void GetRunningObjectTable(out UCOMIRunningObjectTable pprot);

		// Token: 0x060041E4 RID: 16868
		void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x060041E5 RID: 16869
		void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x060041E6 RID: 16870
		void EnumObjectParam(out UCOMIEnumString ppenum);

		// Token: 0x060041E7 RID: 16871
		void RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
	}
}
