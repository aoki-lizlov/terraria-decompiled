using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200074E RID: 1870
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C281C7F1-4AA9-3517-961A-463CFED57E75")]
	[TypeLibImportClass(typeof(Thread))]
	public interface _Thread
	{
		// Token: 0x06004397 RID: 17303
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004398 RID: 17304
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004399 RID: 17305
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600439A RID: 17306
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
