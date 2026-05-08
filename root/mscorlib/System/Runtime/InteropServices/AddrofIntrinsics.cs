using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000751 RID: 1873
	internal static class AddrofIntrinsics
	{
		// Token: 0x0600440F RID: 17423 RVA: 0x000E34DA File Offset: 0x000E16DA
		internal static IntPtr AddrOf<T>(T ftn)
		{
			return Marshal.GetFunctionPointerForDelegate<T>(ftn);
		}
	}
}
