using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200073A RID: 1850
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C7BD73DE-9F85-3290-88EE-090B8BDFE2DF")]
	[TypeLibImportClass(typeof(EnumBuilder))]
	public interface _EnumBuilder
	{
		// Token: 0x060042A0 RID: 17056
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060042A1 RID: 17057
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060042A2 RID: 17058
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060042A3 RID: 17059
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
