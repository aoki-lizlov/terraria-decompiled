using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000775 RID: 1909
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumMoniker
	{
		// Token: 0x060044A6 RID: 17574
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IMoniker[] rgelt, IntPtr pceltFetched);

		// Token: 0x060044A7 RID: 17575
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060044A8 RID: 17576
		void Reset();

		// Token: 0x060044A9 RID: 17577
		void Clone(out IEnumMoniker ppenum);
	}
}
