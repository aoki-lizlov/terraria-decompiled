using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000750 RID: 1872
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7E5678EE-48B3-3F83-B076-C58543498A58")]
	[TypeLibImportClass(typeof(TypeBuilder))]
	public interface _TypeBuilder
	{
		// Token: 0x0600440B RID: 17419
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600440C RID: 17420
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600440D RID: 17421
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600440E RID: 17422
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
