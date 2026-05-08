using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000736 RID: 1846
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("917B14D0-2D9E-38B8-92A9-381ACF52F7C0")]
	[TypeLibImportClass(typeof(Attribute))]
	public interface _Attribute
	{
		// Token: 0x0600426F RID: 17007
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004270 RID: 17008
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004271 RID: 17009
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004272 RID: 17010
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
