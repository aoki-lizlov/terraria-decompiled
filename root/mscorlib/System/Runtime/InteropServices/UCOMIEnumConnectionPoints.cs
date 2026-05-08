using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000728 RID: 1832
	[Obsolete]
	[Guid("b196b285-bab4-101a-b69c-00aa00341d07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumConnectionPoints
	{
		// Token: 0x060041EF RID: 16879
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] UCOMIConnectionPoint[] rgelt, out int pceltFetched);

		// Token: 0x060041F0 RID: 16880
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060041F1 RID: 16881
		[PreserveSig]
		int Reset();

		// Token: 0x060041F2 RID: 16882
		void Clone(out UCOMIEnumConnectionPoints ppenum);
	}
}
