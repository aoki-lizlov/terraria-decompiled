using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000739 RID: 1849
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BE9ACCE8-AAFF-3B91-81AE-8211663F5CAD")]
	[TypeLibImportClass(typeof(CustomAttributeBuilder))]
	public interface _CustomAttributeBuilder
	{
		// Token: 0x0600429C RID: 17052
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600429D RID: 17053
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600429E RID: 17054
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600429F RID: 17055
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
