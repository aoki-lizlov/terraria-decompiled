using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200074B RID: 1867
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("15F9A479-9397-3A63-ACBD-F51977FB0F02")]
	[TypeLibImportClass(typeof(PropertyBuilder))]
	public interface _PropertyBuilder
	{
		// Token: 0x06004370 RID: 17264
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004371 RID: 17265
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004372 RID: 17266
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004373 RID: 17267
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
