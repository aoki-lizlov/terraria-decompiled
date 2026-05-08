using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200073E RID: 1854
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CE1A3BF5-975E-30CC-97C9-1EF70F8F3993")]
	[TypeLibImportClass(typeof(FieldBuilder))]
	public interface _FieldBuilder
	{
		// Token: 0x060042D1 RID: 17105
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060042D2 RID: 17106
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060042D3 RID: 17107
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060042D4 RID: 17108
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
