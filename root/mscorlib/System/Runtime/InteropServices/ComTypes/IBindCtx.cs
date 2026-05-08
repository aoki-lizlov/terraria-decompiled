using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200076F RID: 1903
	[Guid("0000000e-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IBindCtx
	{
		// Token: 0x06004490 RID: 17552
		void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06004491 RID: 17553
		void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06004492 RID: 17554
		void ReleaseBoundObjects();

		// Token: 0x06004493 RID: 17555
		void SetBindOptions([In] ref BIND_OPTS pbindopts);

		// Token: 0x06004494 RID: 17556
		void GetBindOptions(ref BIND_OPTS pbindopts);

		// Token: 0x06004495 RID: 17557
		void GetRunningObjectTable(out IRunningObjectTable pprot);

		// Token: 0x06004496 RID: 17558
		void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

		// Token: 0x06004497 RID: 17559
		void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

		// Token: 0x06004498 RID: 17560
		void EnumObjectParam(out IEnumString ppenum);

		// Token: 0x06004499 RID: 17561
		[PreserveSig]
		int RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
	}
}
