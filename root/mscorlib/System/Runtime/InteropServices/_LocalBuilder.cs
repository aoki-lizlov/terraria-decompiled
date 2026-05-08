using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000741 RID: 1857
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("4E6350D1-A08B-3DEC-9A3E-C465F9AEEC0C")]
	[TypeLibImportClass(typeof(LocalBuilder))]
	public interface _LocalBuilder
	{
		// Token: 0x060042FC RID: 17148
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060042FD RID: 17149
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060042FE RID: 17150
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060042FF RID: 17151
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
