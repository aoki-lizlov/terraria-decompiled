using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000729 RID: 1833
	[Obsolete]
	[Guid("00000102-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumMoniker
	{
		// Token: 0x060041F3 RID: 16883
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] UCOMIMoniker[] rgelt, out int pceltFetched);

		// Token: 0x060041F4 RID: 16884
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060041F5 RID: 16885
		[PreserveSig]
		int Reset();

		// Token: 0x060041F6 RID: 16886
		void Clone(out UCOMIEnumMoniker ppenum);
	}
}
