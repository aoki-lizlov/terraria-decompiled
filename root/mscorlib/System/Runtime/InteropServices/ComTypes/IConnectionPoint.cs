using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200076C RID: 1900
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IConnectionPoint
	{
		// Token: 0x06004489 RID: 17545
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x0600448A RID: 17546
		void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

		// Token: 0x0600448B RID: 17547
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x0600448C RID: 17548
		void Unadvise(int dwCookie);

		// Token: 0x0600448D RID: 17549
		void EnumConnections(out IEnumConnections ppEnum);
	}
}
