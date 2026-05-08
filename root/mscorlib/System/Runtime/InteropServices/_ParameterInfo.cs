using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200074A RID: 1866
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("993634C4-E47A-32CC-BE08-85F567DC27D6")]
	[TypeLibImportClass(typeof(ParameterInfo))]
	public interface _ParameterInfo
	{
		// Token: 0x0600436C RID: 17260
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600436D RID: 17261
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600436E RID: 17262
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600436F RID: 17263
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
