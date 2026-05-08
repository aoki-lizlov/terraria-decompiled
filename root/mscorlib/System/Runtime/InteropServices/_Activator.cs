using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000732 RID: 1842
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("03973551-57A1-3900-A2B5-9083E3FF2943")]
	[TypeLibImportClass(typeof(Activator))]
	public interface _Activator
	{
		// Token: 0x06004237 RID: 16951
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004238 RID: 16952
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004239 RID: 16953
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600423A RID: 16954
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
