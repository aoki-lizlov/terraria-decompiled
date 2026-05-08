using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000746 RID: 1862
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C2323C25-F57F-3880-8A4D-12EBEA7A5852")]
	[TypeLibImportClass(typeof(MethodRental))]
	public interface _MethodRental
	{
		// Token: 0x0600435C RID: 17244
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600435D RID: 17245
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600435E RID: 17246
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600435F RID: 17247
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
