using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200074D RID: 1869
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7D13DD37-5A04-393C-BBCA-A5FEA802893D")]
	[TypeLibImportClass(typeof(SignatureHelper))]
	public interface _SignatureHelper
	{
		// Token: 0x06004393 RID: 17299
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004394 RID: 17300
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004395 RID: 17301
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004396 RID: 17302
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
