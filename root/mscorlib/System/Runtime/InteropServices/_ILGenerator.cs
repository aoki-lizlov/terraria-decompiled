using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000740 RID: 1856
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("A4924B27-6E3B-37F7-9B83-A4501955E6A7")]
	[TypeLibImportClass(typeof(ILGenerator))]
	public interface _ILGenerator
	{
		// Token: 0x060042F8 RID: 17144
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060042F9 RID: 17145
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060042FA RID: 17146
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060042FB RID: 17147
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
