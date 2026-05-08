using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000747 RID: 1863
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D002E9BA-D9E3-3749-B1D3-D565A08B13E7")]
	[TypeLibImportClass(typeof(Module))]
	public interface _Module
	{
		// Token: 0x06004360 RID: 17248
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004361 RID: 17249
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004362 RID: 17250
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004363 RID: 17251
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
