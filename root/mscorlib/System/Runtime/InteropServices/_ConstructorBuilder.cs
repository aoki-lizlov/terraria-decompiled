using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000737 RID: 1847
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("ED3E4384-D7E2-3FA7-8FFD-8940D330519A")]
	[TypeLibImportClass(typeof(ConstructorBuilder))]
	public interface _ConstructorBuilder
	{
		// Token: 0x06004273 RID: 17011
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004274 RID: 17012
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004275 RID: 17013
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004276 RID: 17014
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
