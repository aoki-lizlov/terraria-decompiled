using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200073B RID: 1851
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AADABA99-895D-3D65-9760-B1F12621FAE8")]
	[TypeLibImportClass(typeof(EventBuilder))]
	public interface _EventBuilder
	{
		// Token: 0x060042A4 RID: 17060
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060042A5 RID: 17061
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060042A6 RID: 17062
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060042A7 RID: 17063
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
