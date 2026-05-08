using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000772 RID: 1906
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumConnections
	{
		// Token: 0x0600449E RID: 17566
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, IntPtr pceltFetched);

		// Token: 0x0600449F RID: 17567
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060044A0 RID: 17568
		void Reset();

		// Token: 0x060044A1 RID: 17569
		void Clone(out IEnumConnections ppenum);
	}
}
