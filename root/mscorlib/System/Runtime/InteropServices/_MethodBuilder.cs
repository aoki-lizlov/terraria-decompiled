using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000744 RID: 1860
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("007D8A14-FDF3-363E-9A0B-FEC0618260A2")]
	[TypeLibImportClass(typeof(MethodBuilder))]
	public interface _MethodBuilder
	{
		// Token: 0x06004332 RID: 17202
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004333 RID: 17203
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004334 RID: 17204
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004335 RID: 17205
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
