using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000777 RID: 1911
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumVARIANT
	{
		// Token: 0x060044AE RID: 17582
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] object[] rgVar, IntPtr pceltFetched);

		// Token: 0x060044AF RID: 17583
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060044B0 RID: 17584
		[PreserveSig]
		int Reset();

		// Token: 0x060044B1 RID: 17585
		IEnumVARIANT Clone();
	}
}
