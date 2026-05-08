using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000770 RID: 1904
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumConnectionPoints
	{
		// Token: 0x0600449A RID: 17562
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IConnectionPoint[] rgelt, IntPtr pceltFetched);

		// Token: 0x0600449B RID: 17563
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600449C RID: 17564
		void Reset();

		// Token: 0x0600449D RID: 17565
		void Clone(out IEnumConnectionPoints ppenum);
	}
}
