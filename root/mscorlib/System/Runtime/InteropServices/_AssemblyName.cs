using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000735 RID: 1845
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("B42B6AAC-317E-34D5-9FA9-093BB4160C50")]
	[TypeLibImportClass(typeof(AssemblyName))]
	public interface _AssemblyName
	{
		// Token: 0x0600426B RID: 17003
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600426C RID: 17004
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600426D RID: 17005
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600426E RID: 17006
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
