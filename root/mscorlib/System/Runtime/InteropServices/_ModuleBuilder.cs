using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000748 RID: 1864
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D05FFA9A-04AF-3519-8EE1-8D93AD73430B")]
	[TypeLibImportClass(typeof(ModuleBuilder))]
	public interface _ModuleBuilder
	{
		// Token: 0x06004364 RID: 17252
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004365 RID: 17253
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004366 RID: 17254
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004367 RID: 17255
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
