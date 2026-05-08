using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000749 RID: 1865
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("36329EBA-F97A-3565-BC07-0ED5C6EF19FC")]
	[TypeLibImportClass(typeof(ParameterBuilder))]
	public interface _ParameterBuilder
	{
		// Token: 0x06004368 RID: 17256
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004369 RID: 17257
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600436A RID: 17258
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600436B RID: 17259
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
